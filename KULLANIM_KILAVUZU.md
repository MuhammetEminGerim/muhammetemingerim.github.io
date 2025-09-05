# Görüntü Hata Tespiti Sistemi - Kullanım Kılavuzu

## Sistem Gereksinimleri

### Yazılım Gereksinimleri
- **Windows 10/11** (64-bit önerilir)
- **Python 3.8+** (https://python.org adresinden indirin)
- **.NET 6.0 Runtime** (https://dotnet.microsoft.com/download adresinden indirin)
- **Visual Studio 2022** veya **Visual Studio Code** (geliştirme için)

### Python Kütüphaneleri
- opencv-python (4.8.1.78)
- numpy (1.24.3)
- Pillow (10.0.1)
- scikit-image (0.21.0)
- matplotlib (3.7.2)

## Kurulum Adımları

### 1. Otomatik Kurulum (Önerilen)
```bash
# Proje klasöründe setup.bat dosyasını çalıştırın
setup.bat
```

### 2. Manuel Kurulum

#### Python Dependencies
```bash
cd python_backend
pip install -r requirements.txt
```

#### C# Projesi Derleme
```bash
cd winforms_app
dotnet build
```

### 3. Kurulum Testi
```bash
# Python backend test
run_test.bat

# Veya manuel test
python test_python.py
```

## Kullanım

### 1. Uygulamayı Başlatma
- `winforms_app/ImageInspectionApp.sln` dosyasını Visual Studio ile açın
- Projeyi derleyin ve çalıştırın (F5)

### 2. Referans Fotoğraf Seçimi
1. **"Referans OK Fotoğrafını Seçin"** bölümünde **"Seç..."** butonuna tıklayın
2. Kaliteli, net bir OK fotoğrafı seçin
3. Seçilen fotoğraf sağ panelde görüntülenecektir

### 3. Test Fotoğrafları Seçimi
1. **"Test Fotoğrafları"** bölümünde **"Seç..."** butonuna tıklayın
2. Test edilecek fotoğrafların bulunduğu klasörü seçin
3. Sol panelde fotoğraf listesi görüntülenecektir
4. Herhangi bir fotoğrafa tıklayarak sağ panelde önizlemesini görebilirsiniz

### 4. Analiz İşlemi
1. **"Analiz Et"** butonuna tıklayın
2. İşlem sırasında progress bar ve durum mesajları görüntülenecektir
3. Analiz tamamlandığında sonuçlar tabloda görüntülenecektir

### 5. Sonuçları İnceleme
Analiz sonuçları tablosunda şu bilgiler görüntülenir:
- **Dosya Adı**: Test edilen fotoğrafın adı
- **Durum**: OK (başarılı) veya HATA
- **Genel Skor**: 0-100 arası benzerlik skoru
- **Hata Tipi**: Tespit edilen hata türü
- **SSIM Skoru**: Yapısal benzerlik skoru
- **Histogram Korelasyonu**: Renk dağılımı benzerliği
- **SIFT Eşleşme Oranı**: Özellik noktaları eşleşme oranı

### 6. Sonuçları Dışa Aktarma
1. **"Sonuçları Dışa Aktar"** butonuna tıklayın
2. Dosya formatını seçin (CSV veya JSON)
3. Kaydetme konumunu belirleyin

## Algoritma Detayları

### Görüntü Karşılaştırma Yöntemleri

#### 1. SSIM (Structural Similarity Index)
- Yapısal benzerliği ölçer
- -1 ile +1 arasında değer alır
- +1'e yakın değerler yüksek benzerlik gösterir

#### 2. Histogram Karşılaştırması
- Renk dağılımını karşılaştırır
- 0 ile 1 arasında korelasyon katsayısı
- 1'e yakın değerler benzer renk dağılımı gösterir

#### 3. SIFT (Scale-Invariant Feature Transform)
- Ölçek değişmez özellik noktaları
- Köşe ve kenar noktalarını tespit eder
- Eşleşme oranı yüksekse benzer yapı gösterir

#### 4. Genel Skor Hesaplama
```
Genel Skor = (SSIM × 0.4) + (Histogram × 0.3) + (SIFT × 0.3)
```

### Hata Tespiti Eşikleri
- **%70 ve üzeri**: OK (Başarılı)
- **%70 altı**: HATA

### Hata Tipleri
- **Büyük hata**: Ürün deformasyonu (>1000 piksel fark)
- **Orta hata**: Yüzey bozukluğu (500-1000 piksel fark)
- **Küçük hata**: Detay farkı (100-500 piksel fark)
- **Minimal hata**: Renk/parlaklık farkı (<100 piksel fark)

## Sorun Giderme

### Python Script Hatası
```
Hata: Python script bulunamadı
Çözüm: python_backend klasörünün doğru konumda olduğundan emin olun
```

### Import Hatası
```
Hata: No module named 'cv2'
Çözüm: pip install opencv-python
```

### Görüntü Yükleme Hatası
```
Hata: Görüntü yüklenemedi
Çözüm: Desteklenen formatları kullanın (.jpg, .png, .bmp)
```

### Analiz Hatası
```
Hata: Analiz sırasında hata oluştu
Çözüm: Referans ve test görüntülerinin aynı boyutta olmasını sağlayın
```

## Desteklenen Dosya Formatları
- **JPEG** (.jpg, .jpeg)
- **PNG** (.png)
- **BMP** (.bmp)

## Performans İpuçları
- Referans fotoğrafı yüksek kalitede olsun
- Test fotoğrafları aynı açı ve mesafeden çekilmiş olsun
- Aydınlatma koşulları benzer olsun
- Görüntü boyutları makul olsun (çok büyük dosyalar yavaş işlenir)

## Teknik Destek
Sorun yaşadığınızda:
1. `run_test.bat` dosyasını çalıştırarak sistem durumunu kontrol edin
2. Hata mesajlarını not alın
3. Python ve .NET sürümlerinizi kontrol edin
4. Gerekli kütüphanelerin yüklü olduğundan emin olun