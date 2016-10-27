using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace BLL
{
    public class macPassword
    {

        #region 调用DLL
        [DllImport("macPassword.dll")]
        private static extern void getSSIDAndWepKey(byte[] str, string ssidPrefix, StringBuilder out_ssid, StringBuilder out_wepkey);
        /// <summary>
        /// 计算DEK
        /// </summary>
        /// <param name="macstart">MAC高六位</param>
        /// <param name="macend">MAC低六位</param>
        /// <param name="sbpassword">输出DEK</param>
        [DllImport("macPassword.dll")]
        private static extern void getDEK(Int32 macstart, Int32 macend, StringBuilder sbpassword);

        [DllImport("macPassword.dll")]
        private static extern void getNewPin(string mac, StringBuilder _password);

        #endregion

        #region 公共函数
        /// <summary>
        /// 将16进制的字符串转换为整数
        /// </summary>
        /// <param name="hexstring">16进制字符串</param>
        /// <returns></returns>
        private static int convertHexStringToInt(string hexstring)
        {
            try
            {
                return int.Parse(hexstring, System.Globalization.NumberStyles.HexNumber);
            }
            catch
            {
                throw new Exception("MAC格式有误！！");
            }
        }
        #endregion

        #region SSID WEPKEY
        /// <summary>
        /// SSID and WepKey
        /// </summary>
        /// <param name="macaddress"></param>
        /// <param name="ssidhead"></param>
        /// <param name="ssid"></param>
        /// <param name="wepkey"></param>
        public static void getSSID_WepKey(string macaddress, string ssidhead, ref string ssid, ref string wepkey)
        {
            byte[] ch = new byte[6];

            StringBuilder outssid = new StringBuilder();
            StringBuilder outwepkey = new StringBuilder();

            int x = 0;
            for (int i = 0; i < macaddress.Length; i += 2)
            {
                ch[x] = Convert.ToByte(int.Parse(macaddress.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
                x++;
            }

            getSSIDAndWepKey(ch, ssidhead + "_", outssid, outwepkey);

            ssid = outssid.ToString();
            wepkey = outwepkey.ToString();
        }
        public static void getSSID_WepKey(string macaddress, ref string ssid, ref string wepkey)
        {
            getSSID_WepKey(macaddress, "wifi_", ref ssid, ref wepkey);
        }
        #endregion

        #region PIN算法
        private static uint ComputeChecksum(uint PIN)
        {
            uint digit_s = 0;
            uint accum = 0;

            PIN *= 10; /* PIN值乘以10*/
            accum += 3 * ((PIN / 10000000) % 10);  /*取PIN的第八位数字乘以3*/
            accum += 1 * ((PIN / 1000000) % 10);  /*取PIN的第七位数字乘以1*/
            accum += 3 * ((PIN / 100000) % 10);  /*取PIN的第六位数字乘以3*/
            accum += 1 * ((PIN / 10000) % 10);  /*取PIN的第五位数字乘以1*/
            accum += 3 * ((PIN / 1000) % 10);  /*取PIN的第四位数字乘以3*/
            accum += 1 * ((PIN / 100) % 10);  /*取PIN的第三位数字乘以1*/
            accum += 3 * ((PIN / 10) % 10);  /*取PIN的第二位数字乘以3*/


            digit_s = (accum % 10);   /*取accum的最低位数字赋给digit */
            return ((10 - digit_s) % 10);  /*返回一个只有1位数字的十进制数*/
        }
        /// <summary>
        /// 获取指定MAC地址的PIN码
        /// </summary>
        /// <param name="macaddress">MAC地址(XXXXXXXXXXXX)</param>
        /// <returns>返回32位无符号整数</returns>
        public static string getDefaultPIN(string macaddress)
        {
            uint iPin = 0;
            uint checksum;
            uint iwhile = 10000;
            uint i = 0;
            #region 保留非ASCII算法
            //byte[] apmac = new byte[3];
            //int xx = 0;
            //for (int yy = 6; yy < macaddress.Length; yy += 2)
            //{
            //    apmac[xx] = Convert.ToByte(uint.Parse(macaddress.Substring(yy, 2), System.Globalization.NumberStyles.HexNumber));
            //    xx++;
            //}
            #endregion
            byte[] apmac = GetASCIICode(macaddress.Substring(6, 6));

            while ((iwhile / 10000000) <= 0)
            {

                //iPin = (apmac[0] + i) * 256 * 256 + (apmac[1] + i) * 256 + (apmac[0] + i);
                iPin = Convert.ToUInt32(((apmac[0] * 16 + apmac[1]) + i) * 256 * 256 + ((apmac[2] * 16 + apmac[3]) + i) * 256 + ((apmac[4] * 16 + apmac[5]) + i));
                iPin = iPin % 10000000;
                checksum = ComputeChecksum(iPin);
                iPin = iPin * 10 + checksum;
                iwhile = iPin;
                i++;
            }
            return iPin.ToString();
        }
        private static byte[] GetASCIICode(string str)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] encoding = ascii.GetBytes(str);
            return encoding;

        }
        #endregion

        /// <summary>
        /// 获取MAC对应的所有的key(ssid,webkey,pin,dek)
        /// </summary>
        /// <param name="_strmac"></param>
        /// <returns></returns>
        public static List<string> getMacAllPassword(string _strmac)
        {
            
                List<string> ltemp = new List<string>();
                try
                {
                byte[] ch = new byte[6];

                StringBuilder outssid = new StringBuilder();
                StringBuilder outwebkey = new StringBuilder();
                StringBuilder outdek = new StringBuilder();

                int x = 0;
                for (int i = 0; i < _strmac.Length; i += 2)
                {
                    ch[x] = Convert.ToByte(int.Parse(_strmac.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
                    x++;
                }

                getSSIDAndWepKey(ch, "wifi_", outssid, outwebkey);

                getDEK(convertHexStringToInt(_strmac.Substring(0, 6)),
                    convertHexStringToInt(_strmac.Substring(6, 6)), outdek);
                ltemp.Add(outssid.ToString().Split('_')[1]);
                ltemp.Add(outwebkey.ToString());
                ltemp.Add(getDefaultPIN(_strmac));
                ltemp.Add(outdek.ToString());
                string newmac2 = "00000000" + Convert.ToString((convertHexStringToInt(_strmac.Substring(6, 6)) + 1), 16);
                newmac2 = newmac2.Substring(newmac2.Length - 6, 6);
                ltemp.Add(getNewPin(_strmac.Substring(0, 6) + newmac2));
                //ltemp.Add(getNewPin(_strmac));
            }
            catch
            {
            }
            return ltemp;
        }

        public static string getNewPin(string _mac)
        {

            for (int i = _mac.Length - 2; i > 0; i -= 2)
            {
                _mac = _mac.Insert(i, ":");
            }
            StringBuilder pwd = new StringBuilder();
            getNewPin(_mac, pwd);
            return pwd.ToString();
        }
    }
}
