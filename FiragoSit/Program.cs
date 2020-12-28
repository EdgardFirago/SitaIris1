using System;
using System.Collections;
using System.IO;
using System.Net;
using NetworkManager;
using FileManager;
using System.Threading;
using Newtonsoft.Json;
using ConfigurationManager;
using System.Threading.Tasks;
using System.Threading;

namespace FiragoSit
{
    class Program
    {

        static ArrayList arrayName = new ArrayList();
       
        static void Main(string[] args)
        { 
            ConfigManager configManager = new ConfigManager();
            ConfigClass dataClass = configManager.SetConfig("config.json");
           
            Request ftpRequest = new Request(dataClass.HOST);
            ftpRequest.SetCredential(dataClass.loginFTP, dataClass.passwordFTP);
            ftpRequest.IsEnableSSL = true;
           
            //Я понимаю что это не очень правильно и можно было сделать цикличный таймер, но если это первая часть проекта ,то он всё равно будет меняться
            while (true)
            {
                Monitoring(ftpRequest,dataClass);
                Thread.Sleep(1000);

            }
            static async void Monitoring(Request ftpRequest,ConfigClass dataClass)
            {
                
                ArrayList array = ftpRequest.CheckDirection(dataClass.serverPath);
                foreach (string name in array)
                {
                    string[] temp = name.Split('.');
                    if (arrayName.Contains(temp[0]))
                        continue;
                    arrayName.Add(temp[0]);
                   await Task.Run(() => workWithFileAsync(temp[0],name, ftpRequest, dataClass));

                }

            }
            static void workWithFileAsync(String name,String fullName, Request ftpRequest, ConfigClass dataClass)
            {
                byte[] bytes = ftpRequest.DownloadFile(dataClass.serverPath + fullName);

                var crypthographer = new EncryptionManager();
                byte[] cryptoBytes = crypthographer.CryptXOR(bytes);

                var archiver = new Archiver();

                archiver.Compress(new MemoryStream(cryptoBytes), dataClass.path + name + ".gz");

                archiver.Decompress(dataClass.path + name + ".gz", dataClass.path + name + ".txt");

                byte[] EncryptByte = crypthographer.CryptXOR(File.ReadAllBytes(dataClass.path + name + ".txt"));
                File.WriteAllBytes(dataClass.path + name + "Encrypt" + ".txt", EncryptByte);

            }
            
            
        
           

        }
    }
}

