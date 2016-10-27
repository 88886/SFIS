using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace BLL
{
    public class ServerConfig
    {
        private readonly static ServerConfig instance = new ServerConfig();
        static string connStr = "";
        static string  err = string.Empty;
        static string IniFileName = System.AppDomain.CurrentDomain.BaseDirectory + "config.ini";
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
                   connStr = string.Format("Password={2};User ID={1};server={0}",
                     CfgIniFile.IniReadValue("SERVER_CONFIG", "DATABASE", IniFileName),
                     DecryptString(CfgIniFile.IniReadValue("SERVER_CONFIG", "USER", IniFileName)),
                     DecryptString(CfgIniFile.IniReadValue("SERVER_CONFIG", "PASSWORD", IniFileName)));
                //connStr = string.Format("Data Source={0};User ID={1};Password={2}",
                //     CfgIniFile.IniReadValue("SERVER_CONFIG", "DATABASE", IniFileName),
                //     DecryptString(CfgIniFile.IniReadValue("SERVER_CONFIG", "USER", IniFileName)),
                //     DecryptString(CfgIniFile.IniReadValue("SERVER_CONFIG", "PASSWORD", IniFileName)));
            }
            catch
            {
                connStr = string.Empty;
                err = "Config Read Error";
            }
        }

        #region 加密解密
        private static string key = "2013SFCCODE"; //默认密钥

        private static byte[] sKey;
        private static byte[] sIV;
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <param name="keyStr">密码，可以为“”</param>
        /// <returns>输出加密后字符串</returns>
        public static string EncryptString(string inputStr)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();      
            byte[] inputByteArray = Encoding.Default.GetBytes(inputStr);
            byte[] keyByteArray = Encoding.Default.GetBytes(key);
            SHA1 ha = new SHA1Managed();
            byte[] hb = ha.ComputeHash(keyByteArray);
            sKey = new byte[8];
            sIV = new byte[8];
            for (int i = 0; i < 8; i++)
                sKey[i] = hb[i];
            for (int i = 8; i < 16; i++)
                sIV[i - 8] = hb[i];
            des.Key = sKey;
            des.IV = sIV;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            cs.Close();
            ms.Close();
            return ret.ToString();
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="inputStr">要解密的字符串</param>
        /// <param name="keyStr">密钥</param>
        /// <returns>解密后的结果</returns>
        public static string DecryptString(string inputStr)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();        
            byte[] inputByteArray = new byte[inputStr.Length / 2];
            for (int x = 0; x < inputStr.Length / 2; x++)
            {
                int i = (Convert.ToInt32(inputStr.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            byte[] keyByteArray = Encoding.Default.GetBytes(key);
            SHA1 ha = new SHA1Managed();
            byte[] hb = ha.ComputeHash(keyByteArray);
            sKey = new byte[8];
            sIV = new byte[8];
            for (int i = 0; i < 8; i++)
                sKey[i] = hb[i];
            for (int i = 8; i < 16; i++)
                sIV[i - 8] = hb[i];
            des.Key = sKey;
            des.IV = sIV;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }

        #endregion

    }
}
