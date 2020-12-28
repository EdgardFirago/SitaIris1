using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace FileManager
{
    public class Archiver
    {
        public void Compress(MemoryStream sourceFile, string compressedFile)
        {
           
                using (FileStream targetStream = File.Create(compressedFile))
                {
                
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                    sourceFile.CopyTo(compressionStream); 
                        Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
                            sourceFile, sourceFile.Length.ToString(), targetStream.Length.ToString());
                    }
                }
            
        }

        public void Decompress(string compressedFile, string targetFile)
        {
           
            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
         
                using (FileStream targetStream = File.Create(targetFile))
                {
 
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                        Console.WriteLine("Восстановлен файл: {0}", targetFile);
                    }
                }
            }
        }
    }
}
