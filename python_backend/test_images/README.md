# Test Görüntüleri

Bu klasör test görüntüleri için kullanılır.

## Test Görüntüleri Hazırlama

### Referans OK Fotoğrafı
- Yüksek kaliteli, net bir fotoğraf
- İyi aydınlatma
- Ürünün tamamı görünür olmalı
- Arka plan temiz olmalı

### Test Fotoğrafları
- Aynı açı ve mesafeden çekilmiş
- Benzer aydınlatma koşulları
- Farklı hata tiplerini içeren örnekler:
  - OK fotoğraflar (referans ile benzer)
  - Hatalı fotoğraflar (çizik, leke, deformasyon vb.)

## Örnek Dosya Yapısı
```
test_images/
├── reference_ok.jpg          # Referans OK fotoğrafı
├── test_ok_1.jpg            # OK test fotoğrafı
├── test_ok_2.jpg            # OK test fotoğrafı
├── test_error_scratch.jpg   # Çizik hatası
├── test_error_stain.jpg     # Leke hatası
└── test_error_deform.jpg    # Deformasyon hatası
```

## Test Senaryoları
1. **Başarılı Test**: OK fotoğraflar %70+ skor almalı
2. **Hata Tespiti**: Hatalı fotoğraflar %70 altı skor almalı
3. **Hata Sınıflandırması**: Farklı hata tipleri doğru sınıflandırılmalı