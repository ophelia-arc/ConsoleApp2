using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupProqrami
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("==================================");
                Console.WriteLine("   EHTİYAT (BACKUP) PROQRAMI");
                Console.WriteLine("==================================\n");
                Console.Write("Mənbə qovluğunun yolunu daxil edin: ");
                string sourceDir = Console.ReadLine()?.Trim();

                Console.Write("Təyinat qovluğunun yolunu daxil edin: ");
                string targetDir = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(sourceDir) || !Directory.Exists(sourceDir))
                {
                    Console.WriteLine("\nXƏTA: Mənbə qovluğu tapılmadı və ya daxil edilən yol yanlışdır!");
                    return;
                }
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                    Console.WriteLine($"\nTəyinat qovluğu mövcud deyildi və yeni yaradıldı: {targetDir}");
                }
                string[] txtFiles = Directory.GetFiles(sourceDir, "*.txt");
                if (txtFiles.Length == 0)
                {
                    Console.WriteLine("\nMənbə qovluğunda heç bir .txt faylı tapılmadı.");
                    return;
                }
                int copiedCount = 0;
                long totalSizeBytes = 0;
                string datePrefix = DateTime.Now.ToString("yyyy-MM-dd");
                Console.WriteLine($"\nFaylların kopyalanması prosesi başlandı...\n");
                foreach (string filePath in txtFiles)
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    string fileName = fileInfo.Name;
                    string newFileName = $"{datePrefix}_{fileName}";
                    string destFilePath = Path.Combine(targetDir, newFileName);
                    File.Copy(filePath, destFilePath,overwrite: true);
                    copiedCount++;
                    totalSizeBytes += fileInfo.Length;
                    Console.WriteLine($"Kopyalandı: {fileName} -> {newFileName} ({fileInfo.Length} bayt)");
                }
                double totalSizeKb = totalSizeBytes / 1024.0;
                Console.WriteLine("\n==================================");
                Console.WriteLine("ƏMƏLİYYAT UĞURLA BAŞA ÇATDI!");
                Console.WriteLine($"Kopyalanan faylların sayı: {copiedCount}");
                Console.WriteLine($"Ümumi ölçü: {totalSizeBytes} bayt (~{totalSizeKb:F2} KB)");
                Console.WriteLine("==================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nXƏTA: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nProqramdan çıxmaq üçün bir düyməni basın...");
                Console.ReadKey();
            }
        }
    }
}
