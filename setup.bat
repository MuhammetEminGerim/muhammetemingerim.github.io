@echo off
echo Görüntü Hata Tespiti Sistemi Kurulumu
echo =====================================

echo.
echo 1. Python dependencies yükleniyor...
cd python_backend
pip install -r requirements.txt
if %errorlevel% neq 0 (
    echo Python dependencies yüklenirken hata oluştu!
    pause
    exit /b 1
)

echo.
echo 2. Python kurulumu test ediliyor...
python -c "import cv2, numpy, json, PIL, skimage; print('Python dependencies başarıyla yüklendi!')"
if %errorlevel% neq 0 (
    echo Python dependencies test edilirken hata oluştu!
    pause
    exit /b 1
)

cd ..

echo.
echo 3. C# projesi derleniyor...
cd winforms_app
dotnet build
if %errorlevel% neq 0 (
    echo C# projesi derlenirken hata oluştu!
    pause
    exit /b 1
)

cd ..

echo.
echo Kurulum tamamlandı!
echo.
echo Kullanım:
echo 1. winforms_app klasöründeki ImageInspectionApp.sln dosyasını Visual Studio ile açın
echo 2. Projeyi çalıştırın
echo 3. Referans OK fotoğrafını seçin
echo 4. Test fotoğraflarının bulunduğu klasörü seçin
echo 5. Analiz Et butonuna tıklayın
echo.
pause