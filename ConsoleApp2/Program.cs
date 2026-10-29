using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QeydlerDefteri
{
    class Program
    {
        static string filePath = "qeydler.txt";
        static void Main(string[] args)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\n==================================");
                Console.WriteLine("       QEYDLƏR DƏFTƏRİ");
                Console.WriteLine("==================================");
                Console.WriteLine("1) Yeni qeyd");
                Console.WriteLine("2) Bütün qeydlər");
                Console.WriteLine("3) Qeyd axtar (açar söz ilə)");
                Console.WriteLine("4) Bütün qeydləri sil");
                Console.WriteLine("5) Çıxış");
                Console.Write("Seçiminizi daxil edin (1-5): ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        YeniQeydElaveEt();
                        break;
                    case "2":
                        ButunQeydleriGoster();
                        break;
                    case "3":
                        QeydAxtar();
                        break;
                    case "4":
                        ButunQeydleriSil();
                        break;
                    case "5":
                        isRunning = false;
                        Console.WriteLine("Proqramdan çıxılır...");
                        break;
                    default:
                        Console.WriteLine("Yanlış seçim! Zəhmət olmasa 1-5 arası rəqəm seçin.");
                        break;
                }
            }
        }
        static void YeniQeydElaveEt()
        {
            try
            {
                Console.Write("Yeni qeyd daxil edin: ");
                string yeniQeyd = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(yeniQeyd))
                {
                    Console.WriteLine("Qeyd boş ola bilməz.");
                    return;
                }
                using (StreamWriter sw = new StreamWriter(filePath, append: true))
                {
                    string tarixVer = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    sw.WriteLine($"[{tarixVer}] {yeniQeyd}");
                }
                Console.WriteLine("Qeyd əlavə edildi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta baş verdi: {ex.Message}");
            }
        }
        static void ButunQeydleriGoster()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Heç bir qeyd tapılmadı.");
                    return;
                }
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    Console.WriteLine("\n--- BÜTÜN QEYDLƏR ---");
                    bool varQeyd = false;

                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                        varQeyd = true;
                    }

                    if (!varQeyd)
                    {
                        Console.WriteLine("Fayl boşdur.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fayl oxunarkən xəta baş verdi: {ex.Message}");
            }
        }
        static void QeydAxtar()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Heç bir qeyd tapılmadı.");
                    return;
                }
                Console.Write("Axtarmaq istədiyiniz açar sözü daxil edin: ");
                string keyword = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    Console.WriteLine("Açar söz boş ola bilməz.");
                    return;
                }
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    bool found = false;
                    Console.WriteLine($"\n--- AXTARILAN QEYDLƏR ({keyword}) ---");
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            Console.WriteLine(line);
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        Console.WriteLine("Heç bir uyğun qeyd tapılmadı.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fayl oxunarkən xəta baş verdi: {ex.Message}");
            }
        }
        static void ButunQeydleriSil()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Silinəcək fayl yoxdur.");
                    return;
                }

                Console.Write("Bütün qeydləri silməyə əminsiniz? (y/n): ");
                string confirm = Console.ReadLine().Trim().ToLower();

                if (confirm == "y")
                {
                    File.Delete(filePath);
                    Console.WriteLine("Bütün qeydlər uğurla silindi!");
                }
                else
                {
                    Console.WriteLine("Silmə əməliyyatı ləğv edildi.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fayl silinərkən xəta baş verdi: {ex.Message}");
            }
        }
    }
}
