using System;
using System.IO;

namespace FileSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Give the path of the sourceFolder: ");
            string sourcePath = Console.ReadLine();
            string targetPath = "pdfs"; 

            CopyPdfsFromDirectoryToAnother(sourcePath, targetPath);
            Console.ReadKey();
        }

        /// <summary>
        /// Kivalogatja a megfelelo kiterjesztesu fileokat az adott gyoker mappaból és a benne levo al mappakbol egyarant.
        /// targetDir eseten ha csak egy mappa nevet adunk a buildel egy heyre kerul. (bin/Debug)
        /// </summary>
        /// <param name="sourceDir">A forras fo konyvtar amiből ki kell valogatni a megfelelo fileokat.</param>
        /// <param name="targetDir">Azon location ahova keszitse az eredmeny mappat</param>
        private static void CopyPdfsFromDirectoryToAnother(string sourceDir, string targetDir)
        {

            try
            {
                if (Directory.Exists(targetDir))
                {
                    Console.WriteLine("That path exists already with result - rewrite.");
                    DeleteAllFilesAndDirectory(targetDir);
                }

                DirectoryInfo di = Directory.CreateDirectory(targetDir);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(targetDir));

                if (Directory.Exists(sourceDir))
                {
                    string[] files = Directory.GetFiles(sourceDir,"*.pdf", SearchOption.AllDirectories);
                    foreach(var s in files)
                    {
                        File.Copy(s, Path.Combine(targetDir, Path.GetFileName(s)), true);
                    }
                }
                else
                {
                    Console.WriteLine("Source path does not exist!");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
        /// <summary>
        /// A megadott mappa teljes tartalmat torli.
        /// </summary>
        /// <param name="targetDir">Adott mappa elerese amibol mindent torolni kell.</param>
        private static void DeleteAllFilesAndDirectory(string targetDir)
        {
            DirectoryInfo di = new DirectoryInfo(targetDir);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }
    }
}
