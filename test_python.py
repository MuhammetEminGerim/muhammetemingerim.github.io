#!/usr/bin/env python3
"""
Python backend test scripti
Bu script Python dependencies'lerin doğru yüklendiğini test eder
"""

import sys
import os

def test_imports():
    """Gerekli kütüphanelerin import edilebilirliğini test et"""
    try:
        import cv2
        print(f"✓ OpenCV version: {cv2.__version__}")
    except ImportError as e:
        print(f"✗ OpenCV import hatası: {e}")
        return False
    
    try:
        import numpy as np
        print(f"✓ NumPy version: {np.__version__}")
    except ImportError as e:
        print(f"✗ NumPy import hatası: {e}")
        return False
    
    try:
        import json
        print("✓ JSON module available")
    except ImportError as e:
        print(f"✗ JSON import hatası: {e}")
        return False
    
    try:
        from PIL import Image
        print("✓ PIL/Pillow available")
    except ImportError as e:
        print(f"✗ PIL import hatası: {e}")
        return False
    
    try:
        from skimage.metrics import structural_similarity as ssim
        print("✓ scikit-image available")
    except ImportError as e:
        print(f"✗ scikit-image import hatası: {e}")
        return False
    
    try:
        import matplotlib
        print(f"✓ Matplotlib version: {matplotlib.__version__}")
    except ImportError as e:
        print(f"✗ Matplotlib import hatası: {e}")
        return False
    
    return True

def test_image_analyzer():
    """ImageAnalyzer sınıfının çalışabilirliğini test et"""
    try:
        sys.path.append(os.path.join(os.path.dirname(__file__), 'python_backend'))
        from image_analyzer import ImageAnalyzer
        
        analyzer = ImageAnalyzer()
        print("✓ ImageAnalyzer sınıfı başarıyla oluşturuldu")
        return True
    except Exception as e:
        print(f"✗ ImageAnalyzer test hatası: {e}")
        return False

def main():
    print("Python Backend Test")
    print("==================")
    print()
    
    # Import testleri
    print("1. Kütüphane import testleri:")
    if not test_imports():
        print("\n❌ Import testleri başarısız!")
        sys.exit(1)
    
    print("\n2. ImageAnalyzer sınıf testi:")
    if not test_image_analyzer():
        print("\n❌ ImageAnalyzer testi başarısız!")
        sys.exit(1)
    
    print("\n✅ Tüm testler başarılı!")
    print("\nPython backend hazır. C# uygulaması ile kullanılabilir.")

if __name__ == "__main__":
    main()