
using System.Diagnostics;

using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;


namespace NetworkPingCheck
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        static void Main(string[] args)
        {
            Console.Write("Ловим ВКЛЮЧЕНИЕ введите IP/ПК: ");
            string hostName = Console.ReadLine();
            int timeout = 200; // Время ожидания для каждого пинга (в миллисекундах)

            while (true)
            {
                try
                {
                    // Получение IP-адреса по сетевому имени
                    IPHostEntry host = Dns.GetHostEntry(hostName);
                    string ipAddress = host.AddressList[0].ToString();

                    Ping ping = new Ping();
                    PingReply reply = ping.Send(ipAddress, timeout);

                    if (reply.Status == IPStatus.Success)
                    {
                        // Пинг успешен
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"Сетевой узел {hostName} ({ipAddress}) доступен");

                        // Мигание окна цветом
                        for (int i = 0; i < 5; i++)
                        {
                            Console.BackgroundColor = Console.BackgroundColor == ConsoleColor.Green ? ConsoleColor.Black : ConsoleColor.Green;
                            Thread.Sleep(100);
                        }

                        // Переключение окна в активное состояние
                        Process currentProcess = Process.GetCurrentProcess();
                        IntPtr mainWindowHandle = currentProcess.MainWindowHandle;
                        if (mainWindowHandle != IntPtr.Zero)
                        {
                            SetForegroundWindow(mainWindowHandle);
                        }
                    }
                    else
                    {
                        // Пинг не удался
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"Сетевой узел {hostName} ({ipAddress}) не доступен");
                        // Выполнение команды "ipconfig /flushdns"
                        Process ipconfigProcess = new Process();
                        ipconfigProcess.StartInfo.FileName = "cmd.exe";
                        ipconfigProcess.StartInfo.Arguments = "/c ipconfig /flushdns";
                        ipconfigProcess.StartInfo.UseShellExecute = false;
                        ipconfigProcess.StartInfo.CreateNoWindow = true;
                        ipconfigProcess.Start();
                        ipconfigProcess.WaitForExit();
                    }
                }
                catch (SocketException)
                {
                    // Ошибка разрешения сетевого имени
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Невозможно разрешить сетевое имя {hostName}");
                    // Выполнение команды "ipconfig /flushdns"
                    Process ipconfigProcess = new Process();
                    ipconfigProcess.StartInfo.FileName = "cmd.exe";
                    ipconfigProcess.StartInfo.Arguments = "/c ipconfig /flushdns";
                    ipconfigProcess.StartInfo.UseShellExecute = false;
                    ipconfigProcess.StartInfo.CreateNoWindow = true;
                    ipconfigProcess.Start();
                    ipconfigProcess.WaitForExit();
                }

                Console.ResetColor();
                Thread.Sleep(400); // Задержка перед следующей проверкой
            }
        }
    }
}
