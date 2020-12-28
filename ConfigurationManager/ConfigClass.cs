using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationManager
{
    public class ConfigClass
    {
        public string path { get; set; } = "TargetDirectory/";
        public string serverPath { get; set; } = "/files/";
        public string loginFTP { get; set; } = "ftpuser";
        public string passwordFTP { get; set; } = "qwerty123";
        public string HOST { get; set; } = "37.140.195.104";

    }
}
