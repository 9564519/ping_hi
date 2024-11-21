# Определяем путь к скрипту
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path

# Указываем имя подпапки и .exe файла
$subFolder = "Ping search disabled"  # замените на имя вашей папки
$exeFile = "Ping search disabled.exe"  # замените на имя вашего .exe файла

# Формируем полный путь к .exe файлу
$exePath = Join-Path $scriptDir $subFolder
$exeFullPath = Join-Path $exePath $exeFile

# Проверяем существует ли файл и запускаем его
if (Test-Path $exeFullPath) {
    Start-Process $exeFullPath
} else {
    Write-Host "Ошибка: Файл $exeFullPath не найден." -ForegroundColor Red
}
