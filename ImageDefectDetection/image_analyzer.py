#!/usr/bin/env python3
# -*- coding: utf-8 -*-

import cv2
import numpy as np
import os
import sys
import json
from pathlib import Path
import argparse

class ImageDefectDetector:
    def __init__(self, reference_image_path):
        """
        Görüntü hata tespiti sınıfı
        
        Args:
            reference_image_path (str): Referans ok fotoğrafının yolu
        """
        self.reference_image_path = reference_image_path
        self.reference_image = None
        self.reference_features = None
        self.load_reference_image()
        
    def load_reference_image(self):
        """Referans görüntüyü yükle ve özelliklerini çıkar"""
        try:
            self.reference_image = cv2.imread(self.reference_image_path)
            if self.reference_image is None:
                raise ValueError(f"Referans görüntü yüklenemedi: {self.reference_image_path}")
            
            # Görüntüyü gri tonlamaya çevir
            gray_ref = cv2.cvtColor(self.reference_image, cv2.COLOR_BGR2GRAY)
            
            # Gürültüyü azalt
            gray_ref = cv2.GaussianBlur(gray_ref, (5, 5), 0)
            
            # Kenar tespiti
            edges_ref = cv2.Canny(gray_ref, 50, 150)
            
            # Konturları bul
            contours_ref, _ = cv2.findContours(edges_ref, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
            
            # En büyük konturu al (ok şekli)
            if contours_ref:
                largest_contour = max(contours_ref, key=cv2.contourArea)
                self.reference_features = {
                    'contour': largest_contour,
                    'area': cv2.contourArea(largest_contour),
                    'perimeter': cv2.arcLength(largest_contour, True),
                    'moments': cv2.moments(largest_contour),
                    'hull': cv2.convexHull(largest_contour),
                    'gray_image': gray_ref,
                    'edges': edges_ref
                }
                
                # Hu momentlerini hesapla (şekil tanıma için)
                if self.reference_features['moments']['m00'] != 0:
                    self.reference_features['hu_moments'] = cv2.HuMoments(self.reference_features['moments']).flatten()
                else:
                    self.reference_features['hu_moments'] = np.zeros(7)
            
            print(f"Referans görüntü yüklendi: {self.reference_image_path}")
            print(f"Referans alan: {self.reference_features['area']:.2f}")
            print(f"Referans çevre: {self.reference_features['perimeter']:.2f}")
            
        except Exception as e:
            print(f"Referans görüntü yükleme hatası: {str(e)}")
            sys.exit(1)
    
    def analyze_image(self, image_path):
        """
        Tek bir görüntüyü analiz et
        
        Args:
            image_path (str): Analiz edilecek görüntünün yolu
            
        Returns:
            dict: Analiz sonuçları
        """
        try:
            # Görüntüyü yükle
            test_image = cv2.imread(image_path)
            if test_image is None:
                return {
                    'file': os.path.basename(image_path),
                    'status': 'ERROR',
                    'message': 'Görüntü yüklenemedi',
                    'similarity': 0.0,
                    'defects': []
                }
            
            # Görüntüyü gri tonlamaya çevir
            gray_test = cv2.cvtColor(test_image, cv2.COLOR_BGR2GRAY)
            gray_test = cv2.GaussianBlur(gray_test, (5, 5), 0)
            
            # Kenar tespiti
            edges_test = cv2.Canny(gray_test, 50, 150)
            
            # Konturları bul
            contours_test, _ = cv2.findContours(edges_test, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
            
            if not contours_test:
                return {
                    'file': os.path.basename(image_path),
                    'status': 'ERROR',
                    'message': 'Kontur bulunamadı',
                    'similarity': 0.0,
                    'defects': ['Kontur bulunamadı']
                }
            
            # En büyük konturu al
            largest_contour = max(contours_test, key=cv2.contourArea)
            test_area = cv2.contourArea(largest_contour)
            test_perimeter = cv2.arcLength(largest_contour, True)
            test_moments = cv2.moments(largest_contour)
            
            # Hu momentlerini hesapla
            if test_moments['m00'] != 0:
                test_hu_moments = cv2.HuMoments(test_moments).flatten()
            else:
                test_hu_moments = np.zeros(7)
            
            # Benzerlik hesapla
            similarity = self.calculate_similarity(test_area, test_perimeter, test_hu_moments)
            
            # Hata tespiti
            defects = self.detect_defects(test_area, test_perimeter, test_hu_moments, largest_contour)
            
            # Durum belirleme
            if similarity > 0.8 and len(defects) == 0:
                status = 'OK'
                message = 'Görüntü referans ile uyumlu'
            elif similarity > 0.6:
                status = 'WARNING'
                message = f'Küçük farklılıklar tespit edildi (Benzerlik: {similarity:.2f})'
            else:
                status = 'DEFECT'
                message = f'Önemli farklılıklar tespit edildi (Benzerlik: {similarity:.2f})'
            
            return {
                'file': os.path.basename(image_path),
                'status': status,
                'message': message,
                'similarity': similarity,
                'defects': defects,
                'area': test_area,
                'perimeter': test_perimeter
            }
            
        except Exception as e:
            return {
                'file': os.path.basename(image_path),
                'status': 'ERROR',
                'message': f'Analiz hatası: {str(e)}',
                'similarity': 0.0,
                'defects': [f'Analiz hatası: {str(e)}']
            }
    
    def calculate_similarity(self, test_area, test_perimeter, test_hu_moments):
        """
        Referans ile test görüntüsü arasındaki benzerliği hesapla
        
        Args:
            test_area: Test görüntüsünün alanı
            test_perimeter: Test görüntüsünün çevresi
            test_hu_moments: Test görüntüsünün Hu momentleri
            
        Returns:
            float: Benzerlik skoru (0-1 arası)
        """
        try:
            # Alan benzerliği
            area_similarity = 1.0 - abs(test_area - self.reference_features['area']) / max(test_area, self.reference_features['area'])
            area_similarity = max(0, area_similarity)
            
            # Çevre benzerliği
            perimeter_similarity = 1.0 - abs(test_perimeter - self.reference_features['perimeter']) / max(test_perimeter, self.reference_features['perimeter'])
            perimeter_similarity = max(0, perimeter_similarity)
            
            # Hu momentleri benzerliği
            hu_similarity = 0
            if len(test_hu_moments) == 7 and len(self.reference_features['hu_moments']) == 7:
                for i in range(7):
                    if self.reference_features['hu_moments'][i] != 0:
                        hu_diff = abs(test_hu_moments[i] - self.reference_features['hu_moments'][i]) / abs(self.reference_features['hu_moments'][i])
                        hu_similarity += max(0, 1.0 - hu_diff)
                hu_similarity /= 7
            
            # Ağırlıklı ortalama
            total_similarity = (area_similarity * 0.3 + perimeter_similarity * 0.3 + hu_similarity * 0.4)
            
            return min(1.0, max(0.0, total_similarity))
            
        except Exception as e:
            print(f"Benzerlik hesaplama hatası: {str(e)}")
            return 0.0
    
    def detect_defects(self, test_area, test_perimeter, test_hu_moments, test_contour):
        """
        Hataları tespit et
        
        Args:
            test_area: Test görüntüsünün alanı
            test_perimeter: Test görüntüsünün çevresi
            test_hu_moments: Test görüntüsünün Hu momentleri
            test_contour: Test görüntüsünün konturu
            
        Returns:
            list: Tespit edilen hatalar
        """
        defects = []
        
        try:
            # Alan farkı kontrolü
            area_diff = abs(test_area - self.reference_features['area']) / self.reference_features['area']
            if area_diff > 0.2:  # %20'den fazla fark
                if test_area > self.reference_features['area']:
                    defects.append(f"Alan çok büyük (%{area_diff*100:.1f} fazla)")
                else:
                    defects.append(f"Alan çok küçük (%{area_diff*100:.1f} eksik)")
            
            # Çevre farkı kontrolü
            perimeter_diff = abs(test_perimeter - self.reference_features['perimeter']) / self.reference_features['perimeter']
            if perimeter_diff > 0.2:  # %20'den fazla fark
                defects.append(f"Çevre uzunluğu farklı (%{perimeter_diff*100:.1f} fark)")
            
            # Şekil bozukluğu kontrolü (Hu momentleri)
            if len(test_hu_moments) == 7 and len(self.reference_features['hu_moments']) == 7:
                shape_diff = 0
                for i in range(7):
                    if self.reference_features['hu_moments'][i] != 0:
                        hu_diff = abs(test_hu_moments[i] - self.reference_features['hu_moments'][i]) / abs(self.reference_features['hu_moments'][i])
                        shape_diff += hu_diff
                shape_diff /= 7
                
                if shape_diff > 0.3:  # %30'dan fazla şekil farkı
                    defects.append(f"Şekil bozukluğu tespit edildi (%{shape_diff*100:.1f} fark)")
            
            # Konveks hull kontrolü
            test_hull = cv2.convexHull(test_contour)
            hull_area_ratio = cv2.contourArea(test_hull) / test_area if test_area > 0 else 0
            ref_hull_area_ratio = cv2.contourArea(self.reference_features['hull']) / self.reference_features['area']
            
            if abs(hull_area_ratio - ref_hull_area_ratio) > 0.1:
                defects.append("Konveks şekil farklılığı tespit edildi")
            
            # Eksik parça kontrolü (basit)
            if test_area < self.reference_features['area'] * 0.7:
                defects.append("Eksik parça olabilir")
            
        except Exception as e:
            defects.append(f"Hata tespiti sırasında sorun: {str(e)}")
        
        return defects
    
    def analyze_folder(self, folder_path):
        """
        Klasördeki tüm görüntüleri analiz et
        
        Args:
            folder_path (str): Analiz edilecek klasörün yolu
            
        Returns:
            list: Tüm analiz sonuçları
        """
        results = []
        image_extensions = {'.jpg', '.jpeg', '.png', '.bmp', '.tiff', '.tif'}
        
        try:
            folder_path = Path(folder_path)
            if not folder_path.exists():
                print(f"Klasör bulunamadı: {folder_path}")
                return results
            
            # Klasördeki tüm görüntü dosyalarını bul
            image_files = [f for f in folder_path.iterdir() 
                          if f.is_file() and f.suffix.lower() in image_extensions]
            
            if not image_files:
                print(f"Klasörde görüntü dosyası bulunamadı: {folder_path}")
                return results
            
            print(f"Toplam {len(image_files)} görüntü dosyası bulundu.")
            print("=" * 60)
            
            for i, image_file in enumerate(image_files, 1):
                print(f"Analiz ediliyor ({i}/{len(image_files)}): {image_file.name}")
                result = self.analyze_image(str(image_file))
                results.append(result)
                
                # Sonucu yazdır
                status_emoji = {
                    'OK': '✅',
                    'WARNING': '⚠️',
                    'DEFECT': '❌',
                    'ERROR': '🔴'
                }
                
                emoji = status_emoji.get(result['status'], '❓')
                print(f"  {emoji} {result['status']}: {result['message']}")
                
                if result['defects']:
                    for defect in result['defects']:
                        print(f"    - {defect}")
                
                print(f"  Benzerlik: {result['similarity']:.3f}")
                print("-" * 40)
            
            return results
            
        except Exception as e:
            print(f"Klasör analizi hatası: {str(e)}")
            return results
    
    def generate_report(self, results):
        """
        Analiz raporu oluştur
        
        Args:
            results (list): Analiz sonuçları
            
        Returns:
            str: Rapor metni
        """
        if not results:
            return "Analiz sonucu bulunamadı."
        
        # İstatistikler
        total_images = len(results)
        ok_count = sum(1 for r in results if r['status'] == 'OK')
        warning_count = sum(1 for r in results if r['status'] == 'WARNING')
        defect_count = sum(1 for r in results if r['status'] == 'DEFECT')
        error_count = sum(1 for r in results if r['status'] == 'ERROR')
        
        report = []
        report.append("=" * 60)
        report.append("GÖRÜNTÜ HATA TESPİTİ RAPORU")
        report.append("=" * 60)
        report.append(f"Toplam Görüntü: {total_images}")
        report.append(f"✅ Uyumlu: {ok_count}")
        report.append(f"⚠️ Uyarı: {warning_count}")
        report.append(f"❌ Hatalı: {defect_count}")
        report.append(f"🔴 Hata: {error_count}")
        report.append("=" * 60)
        report.append("")
        
        # Detaylı sonuçlar
        for result in results:
            status_emoji = {
                'OK': '✅',
                'WARNING': '⚠️',
                'DEFECT': '❌',
                'ERROR': '🔴'
            }
            
            emoji = status_emoji.get(result['status'], '❓')
            report.append(f"{emoji} {result['file']}")
            report.append(f"   Durum: {result['status']}")
            report.append(f"   Mesaj: {result['message']}")
            report.append(f"   Benzerlik: {result['similarity']:.3f}")
            
            if 'area' in result and 'perimeter' in result:
                report.append(f"   Alan: {result['area']:.2f}")
                report.append(f"   Çevre: {result['perimeter']:.2f}")
            
            if result['defects']:
                report.append("   Tespit Edilen Hatalar:")
                for defect in result['defects']:
                    report.append(f"     - {defect}")
            
            report.append("")
        
        return "\n".join(report)

def main():
    """Ana fonksiyon"""
    if len(sys.argv) != 3:
        print("Kullanım: python image_analyzer.py <referans_görüntü> <test_klasörü>")
        sys.exit(1)
    
    reference_image = sys.argv[1]
    test_folder = sys.argv[2]
    
    try:
        # Analizörü oluştur
        detector = ImageDefectDetector(reference_image)
        
        # Klasörü analiz et
        results = detector.analyze_folder(test_folder)
        
        # Rapor oluştur
        report = detector.generate_report(results)
        
        # Raporu yazdır
        print("\n" + report)
        
        # JSON formatında da kaydet
        output_file = Path(test_folder) / "analysis_results.json"
        with open(output_file, 'w', encoding='utf-8') as f:
            json.dump(results, f, ensure_ascii=False, indent=2)
        
        print(f"\nDetaylı sonuçlar JSON formatında kaydedildi: {output_file}")
        
    except Exception as e:
        print(f"Program hatası: {str(e)}")
        sys.exit(1)

if __name__ == "__main__":
    main()