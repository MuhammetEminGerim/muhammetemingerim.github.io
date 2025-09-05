@echo off
echo Görüntü Hata Tespiti Sistemi - Hızlı Başlangıç
echo ==============================================

echo.
echo 1. Sistem gereksinimlerini kontrol ediliyor...

:: Python kontrolü
python --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ❌ Python bulunamadı! Lütfen Python 3.8+ yükleyin.
    echo    https://python.org adresinden indirebilirsiniz.
    pause
    exit /b 1
)
echo ✓ Python bulundu

:: .NET kontrolü
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ❌ .NET bulunamadı! Lütfen .NET 6.0+ yükleyin.
    echo    https://dotnet.microsoft.com/download adresinden indirebilirsiniz.
    pause
    exit /b 1
)
echo ✓ .NET bulundu

echo.
echo 2. Kurulum başlatılıyor...
call setup.bat

if %errorlevel% neq 0 (
    echo.
    echo ❌ Kurulum başarısız!
    pause
    exit /b 1
)

echo.
echo 3. Test çalıştırılıyor...
call run_test.bat

if %errorlevel% neq 0 (
    echo.
    echo ❌ Test başarısız!
    pause
    exit /b 1
)

echo.
echo ✅ Sistem hazır!
echo.
echo Sonraki adımlar:
echo 1. winforms_app\ImageInspectionApp.sln dosyasını Visual Studio ile açın
echo 2. Projeyi çalıştırın (F5)
echo 3. Referans OK fotoğrafını seçin
echo 4. Test fotoğraflarının bulunduğu klasörü seçin
echo 5. Analiz Et butonuna tıklayın
echo.
echo Detaylı kullanım için KULLANIM_KILAVUZU.md dosyasını okuyun.
echo.
pause