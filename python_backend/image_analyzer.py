import cv2
import numpy as np
import json
import sys
import os
from PIL import Image
import argparse
from skimage.metrics import structural_similarity as ssim
from skimage import measure
import matplotlib.pyplot as plt

class ImageAnalyzer:
    def __init__(self):
        self.reference_image = None
        self.reference_features = None
        
    def load_reference_image(self, image_path):
        """Referans görüntüyü yükle ve özelliklerini çıkar"""
        try:
            self.reference_image = cv2.imread(image_path)
            if self.reference_image is None:
                raise ValueError(f"Görüntü yüklenemedi: {image_path}")
            
            # Görüntüyü gri tonlamaya çevir
            gray_ref = cv2.cvtColor(self.reference_image, cv2.COLOR_BGR2GRAY)
            
            # Özellik çıkarma
            self.reference_features = self.extract_features(gray_ref)
            
            return True
        except Exception as e:
            print(f"Referans görüntü yükleme hatası: {str(e)}")
            return False
    
    def extract_features(self, image):
        """Görüntüden özellik çıkar"""
        features = {}
        
        # 1. Kenar tespiti (Canny)
        edges = cv2.Canny(image, 50, 150)
        features['edges'] = edges
        
        # 2. Köşe tespiti (Harris)
        corners = cv2.cornerHarris(image, 2, 3, 0.04)
        features['corners'] = corners
        
        # 3. SIFT özellikleri
        sift = cv2.SIFT_create()
        keypoints, descriptors = sift.detectAndCompute(image, None)
        features['sift_keypoints'] = keypoints
        features['sift_descriptors'] = descriptors
        
        # 4. Histogram
        hist = cv2.calcHist([image], [0], None, [256], [0, 256])
        features['histogram'] = hist.flatten()
        
        # 5. Görüntü istatistikleri
        features['mean'] = np.mean(image)
        features['std'] = np.std(image)
        features['shape'] = image.shape
        
        return features
    
    def compare_images(self, test_image_path):
        """Test görüntüsünü referans ile karşılaştır"""
        try:
            # Test görüntüsünü yükle
            test_image = cv2.imread(test_image_path)
            if test_image is None:
                raise ValueError(f"Test görüntüsü yüklenemedi: {test_image_path}")
            
            # Gri tonlamaya çevir
            gray_test = cv2.cvtColor(test_image, cv2.COLOR_BGR2GRAY)
            
            # Boyutları eşitle
            if gray_test.shape != self.reference_features['shape']:
                gray_test = cv2.resize(gray_test, 
                                     (self.reference_features['shape'][1], 
                                      self.reference_features['shape'][0]))
            
            # Test görüntüsünden özellik çıkar
            test_features = self.extract_features(gray_test)
            
            # Karşılaştırma sonuçları
            results = {}
            
            # 1. SSIM (Structural Similarity Index)
            ssim_score = ssim(self.reference_features['edges'], 
                            test_features['edges'])
            results['ssim_score'] = float(ssim_score)
            
            # 2. Histogram karşılaştırması
            hist_corr = cv2.compareHist(self.reference_features['histogram'], 
                                      test_features['histogram'], 
                                      cv2.HISTCMP_CORREL)
            results['histogram_correlation'] = float(hist_corr)
            
            # 3. SIFT özellik eşleştirmesi
            if (self.reference_features['sift_descriptors'] is not None and 
                test_features['sift_descriptors'] is not None):
                
                bf = cv2.BFMatcher()
                matches = bf.knnMatch(self.reference_features['sift_descriptors'],
                                    test_features['sift_descriptors'], k=2)
                
                # Lowe's ratio test
                good_matches = []
                for match_pair in matches:
                    if len(match_pair) == 2:
                        m, n = match_pair
                        if m.distance < 0.75 * n.distance:
                            good_matches.append(m)
                
                results['sift_matches'] = len(good_matches)
                results['sift_match_ratio'] = len(good_matches) / len(matches) if matches else 0
            else:
                results['sift_matches'] = 0
                results['sift_match_ratio'] = 0
            
            # 4. Fark haritası
            diff = cv2.absdiff(self.reference_features['edges'], 
                              test_features['edges'])
            results['difference_percentage'] = float(np.sum(diff > 0) / diff.size * 100)
            
            # 5. Genel skor hesaplama
            # SSIM skorunu 0-100 aralığına çevir
            ssim_normalized = max(0, min(100, (ssim_score + 1) * 50))
            
            # Histogram korelasyonunu 0-100 aralığına çevir
            hist_normalized = max(0, min(100, hist_corr * 100))
            
            # SIFT match ratio'yu 0-100 aralığına çevir
            sift_normalized = max(0, min(100, results['sift_match_ratio'] * 100))
            
            # Ağırlıklı ortalama
            overall_score = (ssim_normalized * 0.4 + 
                           hist_normalized * 0.3 + 
                           sift_normalized * 0.3)
            
            results['overall_score'] = float(overall_score)
            results['is_ok'] = overall_score > 70  # %70 üzeri OK kabul edilir
            
            # Hata tespiti
            if not results['is_ok']:
                results['error_type'] = self.detect_error_type(diff, gray_test)
            else:
                results['error_type'] = "OK"
            
            return results
            
        except Exception as e:
            return {"error": str(e), "is_ok": False}
    
    def detect_error_type(self, diff_image, test_image):
        """Hata tipini tespit et"""
        # Fark haritasından hata bölgelerini bul
        contours, _ = cv2.findContours(diff_image, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
        
        if not contours:
            return "Bilinmeyen hata"
        
        # En büyük hata bölgesini bul
        largest_contour = max(contours, key=cv2.contourArea)
        area = cv2.contourArea(largest_contour)
        
        # Hata tipini belirle
        if area > 1000:
            return "Büyük hata - Ürün deformasyonu"
        elif area > 500:
            return "Orta hata - Yüzey bozukluğu"
        elif area > 100:
            return "Küçük hata - Detay farkı"
        else:
            return "Minimal hata - Renk/parlaklık farkı"
    
    def analyze_batch(self, reference_path, test_folder):
        """Toplu analiz yap"""
        if not self.load_reference_image(reference_path):
            return {"error": "Referans görüntü yüklenemedi"}
        
        results = {}
        test_files = [f for f in os.listdir(test_folder) 
                     if f.lower().endswith(('.png', '.jpg', '.jpeg', '.bmp'))]
        
        for test_file in test_files:
            test_path = os.path.join(test_folder, test_file)
            results[test_file] = self.compare_images(test_path)
        
        return results

def main():
    parser = argparse.ArgumentParser(description='Görüntü Hata Tespiti')
    parser.add_argument('--reference', required=True, help='Referans görüntü yolu')
    parser.add_argument('--test', help='Test görüntü yolu (tek dosya)')
    parser.add_argument('--folder', help='Test görüntüleri klasörü')
    parser.add_argument('--output', help='Sonuç dosyası yolu (JSON)')
    
    args = parser.parse_args()
    
    analyzer = ImageAnalyzer()
    
    if args.test:
        # Tek dosya analizi
        if not analyzer.load_reference_image(args.reference):
            sys.exit(1)
        
        result = analyzer.compare_images(args.test)
        print(json.dumps(result, indent=2))
        
    elif args.folder:
        # Toplu analiz
        results = analyzer.analyze_batch(args.reference, args.folder)
        print(json.dumps(results, indent=2))
        
        if args.output:
            with open(args.output, 'w', encoding='utf-8') as f:
                json.dump(results, f, indent=2, ensure_ascii=False)
    
    else:
        print("Test dosyası veya klasör belirtmelisiniz")

if __name__ == "__main__":
    main()