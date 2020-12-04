using System;
using System.Collections;
using System.IO;
using System.Net;
using NetworkManager;
using FileManager;
using System.Threading;

namespace FiragoSit
{
    class Program
    {
        const string path = "/TargetDirectory/";

        static void Main(string[] args)
        {
            Request ftpRequest = new Request("37.140.195.104");
            ftpRequest.SetCredential("ftpuser", "qwerty123");
            ftpRequest.IsEnableSSL = true;
            ArrayList arrayName = new ArrayList();
            //Я понимаю что это не очень правильно и можно было сделать цикличный таймер, но если это первая часть проекта ,то он всё равно будет меняться
            while (true)
            {
                Monitoring(ref ftpRequest,ref arrayName);
                Thread.Sleep(1000);

            }
            static void Monitoring(ref Request ftpRequest,ref ArrayList arrayName)
            {
                ArrayList array = ftpRequest.CheckDirection("/files/");
                foreach (string name in array)
                {
                    string[] temp = name.Split('.');
                    if (arrayName.Contains(temp[0]))
                        continue;
                    arrayName.Add(temp[0]);
                    byte[] bytes = ftpRequest.DownloadFile("/files/" + name);
                    var crypthographer = new EncryptionManager();
                    byte[] cryptoBytes = crypthographer.CryptXOR(bytes);

                    var archiver = new Archiver();
                    archiver.Compress(new MemoryStream(cryptoBytes), "TargetDirectory/" + temp[0] + ".gz");
                    archiver.Decompress("TargetDirectory/" + temp[0] + ".gz", "TargetDirectory/"+ temp[0] + ".txt");
                    byte[] EncryptByte = crypthographer.CryptXOR(File.ReadAllBytes("TargetDirectory/" + temp[0] + ".txt"));
                    File.WriteAllBytes("TargetDirectory/ " + temp[0] + "Encrypt" + ".txt", EncryptByte);
                }


            }


        }
    }
}

