@echo off
echo Görüntü Hata Tespiti Kurulum Scripti
echo ====================================

echo.
echo 1. Python bağımlılıkları kuruluyor...
pip install -r requirements.txt

echo.
echo 2. .NET projesi derleniyor...
dotnet build

echo.
echo 3. Kurulum tamamlandı!
echo.
echo Kullanım:
echo   dotnet run
echo.
echo Veya Visual Studio ile projeyi açın.
echo.
pause