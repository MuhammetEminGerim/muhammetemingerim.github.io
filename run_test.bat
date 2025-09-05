@echo off
echo Python Backend Test
echo ===================

python test_python.py

if %errorlevel% neq 0 (
    echo.
    echo Test başarısız! Lütfen setup.bat dosyasını çalıştırın.
    pause
    exit /b 1
)

echo.
echo Test başarılı! Sistem kullanıma hazır.
pause