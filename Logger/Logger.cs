using System;
using System.IO;

namespace Logger
{
    public class Logger
    {
        // Как я всегда считал , что самое главное правило логгеров - Не писать свой логгер. Но так как мы учимся
        //то я думаю что примитивного подхода без обработки состояний хватит.
        public static string path = "log.txt";
       
        public static void Log(string message)
        {
            File.AppendAllText(path, message);
        }

    }
     
}
