using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MetnAnalizatoru
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("==================================");
                Console.WriteLine("       MƏTN ANALİZATORU");
                Console.WriteLine("==================================\n");

                // 1. Oxunacaq faylın yolunun istifadəçidən alınması
                Console.Write("Analiz ediləcək faylın yolunu daxil edin: ");
                string filePath = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                {
                    Console.WriteLine("\nXƏTA: Göstərilən fayl tapılmadı və ya daxil edilən yol yanlışdır!");
                    return;
                }

                int lineCount = 0;
                int totalChars = 0;
                List<string> allWords = new List<string>();

                char[] delimiters = new char[] { ' ', '\t', '\r', '\n', '.', ',', '!', '?', ';', ':', '-', '(', ')', '"', '«', '»' };

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lineCount++;
                        totalChars += line.Length;

                        string[] words = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                        allWords.AddRange(words);
                    }
                }

                int wordCount = allWords.Count;
                string longestWord = "";

                Dictionary<string, int> wordFrequencies = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                foreach (string word in allWords)
                {
                    string cleanWord = word.ToLower();

                    if (cleanWord.Length > longestWord.Length)
                    {
                        longestWord = cleanWord;
                    }

                    if (wordFrequencies.ContainsKey(cleanWord))
                    {
                        wordFrequencies[cleanWord]++;
                    }
                    else
                    {
                        wordFrequencies[cleanWord] = 1;
                    }
                }

                var top3Words = wordFrequencies
                    .OrderByDescending(kvp => kvp.Value)
                    .Take(3)
                    .ToList();

                string reportPath = "hesabat.txt";
                using (StreamWriter sw = new StreamWriter(reportPath))
                {
                    sw.WriteLine("==================================");
                    sw.WriteLine("       MƏTN ANALİZİ HESABATI");
                    sw.WriteLine("==================================");
                    sw.WriteLine($"Analiz olunan fayl: {filePath}");
                    sw.WriteLine($"Sətir sayı: {lineCount}");
                    sw.WriteLine($"Söz sayı: {wordCount}");
                    sw.WriteLine($"Simvol sayı: {totalChars}");
                    sw.WriteLine($"Ən uzun söz: {longestWord} ({longestWord.Length} simvol)");
                    sw.WriteLine("\nƏn çox təkrarlanan 3 söz:");

                    int rank = 1;
                    foreach (var kvp in top3Words)
                    {
                        sw.WriteLine($"{rank}. '{kvp.Key}' - {kvp.Value} dəfə");
                        rank++;
                    }
                }

                Console.WriteLine("\n==================================");
                Console.WriteLine("ANALİZ UĞURLA TAMAMLANDI!");
                Console.WriteLine("==================================");
                Console.WriteLine($"Sətir sayı: {lineCount}");
                Console.WriteLine($"Söz sayı: {wordCount}");
                Console.WriteLine($"Simvol sayı: {totalChars}");
                Console.WriteLine($"Ən uzun söz: {longestWord}");
                Console.WriteLine("\nƏn çox təkrarlanan 3 söz:");

                int r = 1;
                foreach (var kvp in top3Words)
                {
                    Console.WriteLine($"{r}. '{kvp.Key}' -> {kvp.Value} dəfə");
                    r++;
                }

                Console.WriteLine($"\nYekun hesabat '{reportPath}' faylına yazıldı.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nXəta baş verdi: {ex.Message}");
            }

            Console.WriteLine("\nÇıxış üçün bir düyməyə basın...");
            Console.ReadKey();
        }
    }
}