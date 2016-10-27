using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class ServerConfig
    {
        private readonly static ServerConfig instance = new ServerConfig();
        static string connStr = "";
        static string  err = string.Empty;
        static string IniFileName = System.AppDomain.CurrentDomain.BaseDirectory + "config.ini";// System.IO.Directory.GetCurrentDirectory() + "\\config.ini";

        public static string GetIniFileName
        {
            get { return ServerConfig.IniFileName; }
            set { ServerConfig.IniFileName = value; }
        }
        public static ServerConfig Instance
        {
            get { return instance; }
        }

        public string ConnStr
        {
            get { return connStr; }
        }
        public string Error
        {
            get { return err; }
        }

        static ServerConfig()
        {
            try
            {
                connStr = string.Format("server={0};uid={1};pwd={2};database={3}",
                   CfgIniFile.IniReadValue("SERVER_CONFIG", "IPADDRESS", IniFileName),
                   CfgIniFile.IniReadValue("SERVER_CONFIG", "USER", IniFileName),
                   CfgIniFile.IniReadValue("SERVER_CONFIG", "PASSWORD", IniFileName),
                   CfgIniFile.IniReadValue("SERVER_CONFIG", "DATABASE", IniFileName));
            }
            catch
            {
                connStr = string.Empty;
                err = "Config Read Error";
            }
        }
    }
}
