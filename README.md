# Görüntü Hata Tespiti Sistemi

Bu proje, bir referans "OK" fotoğrafı kullanarak diğer fotoğraflardaki hataları tespit eden bir sistemdir.

## Proje Yapısı

```
URUN_OK_NOK/
├── python_backend/
│   ├── image_analyzer.py
│   ├── requirements.txt
│   └── test_images/
├── winforms_app/
│   ├── ImageInspectionApp.sln
│   ├── ImageInspectionApp/
│   │   ├── Form1.cs
│   │   ├── Form1.Designer.cs
│   │   └── Program.cs
│   └── ImageInspectionApp.csproj
└── README.md
```

## Kurulum

1. Python dependencies:
```bash
cd python_backend
pip install -r requirements.txt
```

2. C# uygulamasını Visual Studio ile açın ve derleyin.

## Kullanım

1. Referans OK fotoğrafını seçin
2. Test edilecek fotoğrafları seçin
3. Analiz butonuna tıklayın
4. Sonuçları görüntüleyin