using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Collections;

namespace NetworkManager
{
    public class Request
    {
        private string hostName;
        public string data;
        private string login;
        private string password;
        private bool IsCredentials = false;
 
        public bool IsEnableSSL { get; set; } = false;
        public Request(string hostName) { this.hostName = hostName;}
        public void SetCredential(string login, string password)
        {
            this.login = login;
            this.password = password;
            this.IsCredentials = true;

        }
        public byte[] DownloadFile(string path)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => { return true; };
            string url = "ftp://" + hostName + path;
            var request = (FtpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            if(IsCredentials)
                request.Credentials = new NetworkCredential(login, password);
            if(IsEnableSSL)
                request.EnableSsl = true;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            var bytes = default(byte[]);
            using (var memstream = new MemoryStream())
            {
                reader.BaseStream.CopyTo(memstream);
                bytes = memstream.ToArray();
            }
            response.Close();
            Console.WriteLine("Загрузка завершена");
            return bytes;
 
        }
        public ArrayList CheckDirection(string path)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => { return true; };
            string url = "ftp://" + hostName+path;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
            if (IsCredentials)
                request.Credentials = new NetworkCredential(login, password);
            if (IsEnableSSL)
                request.EnableSsl = true;
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            ArrayList returnedArray = new ArrayList();
            StreamToArray(reader, ref returnedArray);
            reader.Close();
            responseStream.Close();
            response.Close();
            return returnedArray;
        }

        private void StreamToArray(StreamReader sr,ref ArrayList arrayList)
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                arrayList.Add(line);
            }
        }
        

    }
}
