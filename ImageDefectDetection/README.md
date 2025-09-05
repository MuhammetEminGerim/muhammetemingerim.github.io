# Görüntü Hata Tespiti - Ok Analizi

Bu uygulama, referans bir ok fotoğrafı kullanarak diğer ok fotoğraflarındaki hataları tespit eder.

## Özellikler

- **WinForms Arayüzü**: Kullanıcı dostu Windows Forms arayüzü
- **Python + OpenCV**: Gelişmiş görüntü işleme algoritmaları
- **Çoklu Analiz**: Klasördeki tüm fotoğrafları toplu analiz
- **Detaylı Raporlama**: JSON ve metin formatında sonuçlar
- **Gerçek Zamanlı İlerleme**: Analiz sırasında ilerleme takibi

## Kurulum

### 1. Python Bağımlılıkları

```bash
pip install -r requirements.txt
```

### 2. .NET 6.0

.NET 6.0 Runtime'ın yüklü olduğundan emin olun.

### 3. Uygulamayı Derleme

```bash
dotnet build
```

## Kullanım

### 1. Uygulamayı Başlatma

```bash
dotnet run
```

### 2. Analiz Adımları

1. **Referans Fotoğraf Seç**: Mükemmel bir ok fotoğrafını referans olarak seçin
2. **Test Klasörü Seç**: Analiz edilecek fotoğrafların bulunduğu klasörü seçin
3. **Analizi Başlat**: "Analizi Başlat" butonuna tıklayın
4. **Sonuçları İncele**: Analiz sonuçları otomatik olarak görüntülenecek

## Analiz Algoritması

### 1. Görüntü Ön İşleme
- Gri tonlamaya çevirme
- Gürültü azaltma (Gaussian Blur)
- Kenar tespiti (Canny)

### 2. Özellik Çıkarma
- **Kontur Analizi**: Ok şeklinin sınırlarını belirleme
- **Alan ve Çevre**: Geometrik özellikler
- **Hu Momentleri**: Şekil tanıma için invariant momentler
- **Konveks Hull**: Şekil dışbükeyliği

### 3. Benzerlik Hesaplama
- Alan benzerliği (%30 ağırlık)
- Çevre benzerliği (%30 ağırlık)
- Hu momentleri benzerliği (%40 ağırlık)

### 4. Hata Tespiti
- **Alan Farkı**: %20'den fazla fark
- **Çevre Farkı**: %20'den fazla fark
- **Şekil Bozukluğu**: %30'dan fazla Hu momentleri farkı
- **Konveks Hull Farkı**: Şekil dışbükeyliği değişimi
- **Eksik Parça**: %30'dan fazla alan kaybı

## Sonuç Kategorileri

- **✅ OK**: Referans ile tamamen uyumlu
- **⚠️ WARNING**: Küçük farklılıklar (benzerlik > 0.6)
- **❌ DEFECT**: Önemli farklılıklar (benzerlik ≤ 0.6)
- **🔴 ERROR**: Analiz hatası

## Çıktı Dosyaları

- **Ekran Çıktısı**: Anlık analiz sonuçları
- **JSON Dosyası**: Detaylı sonuçlar (`analysis_results.json`)
- **Rapor**: Özet istatistikler ve detaylı analiz

## Teknik Detaylar

### Desteklenen Formatlar
- JPG/JPEG
- PNG
- BMP
- TIFF/TIF

### Performans
- Tek görüntü analizi: ~1-2 saniye
- 100 görüntü: ~2-3 dakika
- Bellek kullanımı: ~50-100 MB

### Sistem Gereksinimleri
- Windows 10/11
- .NET 6.0 Runtime
- Python 3.7+
- OpenCV 4.8+
- Minimum 4GB RAM

## Sorun Giderme

### Python Bulunamadı Hatası
```bash
# Python'un PATH'te olduğundan emin olun
python --version
```

### OpenCV Kurulum Hatası
```bash
pip install --upgrade pip
pip install opencv-python
```

### Görüntü Yükleme Hatası
- Dosya formatının desteklendiğinden emin olun
- Dosya yolunda özel karakterler olmamasına dikkat edin
- Dosya boyutunun 50MB'den küçük olduğundan emin olun

## Geliştirme

### Proje Yapısı
```
ImageDefectDetection/
├── ImageDefectDetection.csproj
├── Program.cs
├── MainForm.cs
├── image_analyzer.py
├── requirements.txt
└── README.md
```

### Yeni Özellik Ekleme
1. Python script'ini güncelleyin
2. WinForms arayüzünü genişletin
3. Test edin ve dokümante edin

## Lisans

Bu proje eğitim amaçlı geliştirilmiştir.