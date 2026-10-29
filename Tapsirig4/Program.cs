using System;
using System.IO;

namespace TelebeSiyahisi
{
    class Program
    {
        static string filePath = "telebeler.txt";

        static void Main(string[] args)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n==================================");
                Console.WriteLine("       TƏLƏBƏ SİYAHISI");
                Console.WriteLine("==================================");
                Console.WriteLine("1) Tələbə əlavə et");
                Console.WriteLine("2) Hamısını göstər");
                Console.WriteLine("3) Ada görə axtar");
                Console.WriteLine("4) Çıxış");
                Console.Write("Seçiminizi daxil edin (1-4): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        TelebeElaveEt();
                        break;
                    case "2":
                        HamisiniGoster();
                        break;
                    case "3":
                        AdaGoreAxtar();
                        break;
                    case "4":
                        isRunning = false;
                        Console.WriteLine("Proqramdan çıxılır...");
                        break;
                    default:
                        Console.WriteLine("Yanlış seçim! Zəhmət olmasa 1-4 arası rəqəm seçin.");
                        break;
                }
            }
        }
        static void TelebeElaveEt()
        {
            try
            {
                Console.Write("Tələbə ID-si: ");
                string id = Console.ReadLine()?.Trim();

                Console.Write("Tələbə Adı: ");
                string ad = Console.ReadLine()?.Trim();

                Console.Write("Tələbə Yaşı: ");
                string yasInput = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(ad) || !int.TryParse(yasInput, out int yas))
                {
                    Console.WriteLine("\nXƏTA: Məlumatlar düzgün daxil edilməyib! Yaş rəqəm olmalıdır.");
                    return;
                }

                using (StreamWriter sw = new StreamWriter(filePath, append: true))
                {
                    sw.WriteLine($"{id};{ad};{yas}");
                }

                Console.WriteLine("\nTələbə məlumatı 'telebeler.txt' faylına uğurla yazıldı!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nXəta baş verdi: {ex.Message}");
            }
        }

        static void HamisiniGoster()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("\nHələ heç bir tələbə qeydiyyata alınmayıb (fayl tapılmadı).");
                    return;
                }

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    bool varTelebe = false;

                    Console.WriteLine("\n--------------------------------------------------");
                    Console.WriteLine(" ID  |  AD                  |  YAŞ");
                    Console.WriteLine("--------------------------------------------------");

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] parts = line.Split(';');

                        if (parts.Length == 3)
                        {
                            string id = parts[0];
                            string ad = parts[1];
                            string yas = parts[2];

                            Console.WriteLine($" {id,-3} | {ad,-20} | {yas}");
                            varTelebe = true;
                        }
                    }

                    Console.WriteLine("--------------------------------------------------");

                    if (!varTelebe)
                    {
                        Console.WriteLine("Fayl boşdur.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nXəta baş verdi: {ex.Message}");
            }
        }
        static void AdaGoreAxtar()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("\nHələ heç bir tələbə qeydiyyata alınmayıb (fayl tapılmadı).");
                    return;
                }

                Console.Write("\nAxtarılacaq tələbə adını daxil edin: ");
                string searchName = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(searchName))
                {
                    Console.WriteLine("Axtarış üçün ad boş ola bilməz!");
                    return;
                }

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    bool tapildi = false;

                    Console.WriteLine("\n--- AXTARIŞ NƏTİCƏLƏRİ ---");

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] parts = line.Split(';');

                        if (parts.Length == 3)
                        {
                            string id = parts[0];
                            string ad = parts[1];
                            string yas = parts[2];

                            if (ad.IndexOf(searchName, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                Console.WriteLine($"[+] ID: {id} | Ad: {ad} | Yaş: {yas}");
                                tapildi = true;
                            }
                        }
                    }

                    if (!tapildi)
                    {
                        Console.WriteLine($"'{searchName}' adına uyğun heç bir tələbə tapılmadı.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nXəta baş verdi: {ex.Message}");
            }
        }
    }
}