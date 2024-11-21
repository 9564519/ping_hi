# ���������� ���� � �������
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path

# ��������� ��� �������� � .exe �����
$subFolder = "Ping search enable"  # �������� �� ��� ����� �����
$exeFile = "Ping search enable.exe"  # �������� �� ��� ������ .exe �����

# ��������� ������ ���� � .exe �����
$exePath = Join-Path $scriptDir $subFolder
$exeFullPath = Join-Path $exePath $exeFile

# ��������� ���������� �� ���� � ��������� ���
if (Test-Path $exeFullPath) {
    Start-Process $exeFullPath
} else {
    Write-Host "������: ���� $exeFullPath �� ������." -ForegroundColor Red
}
