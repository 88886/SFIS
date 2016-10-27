using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Configuration;
using System.Security;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;


namespace SFIS_V2
{
    public static class Common
    {
        /// <summary>
        /// 公用变量
        /// </summary>
        public static int UserID;
        public static string InstituteID;
        public static string AreaCode;
        public static string AccountName; // 用户名 、帐号名
        public static string AccountPwd;
        public static string UserName; // 用户名称
        public static string InstituteCode;
        public static string InstituteName;
        public static int InstituteSort;

        public static string AssessPeriod;

        public static string tmpAccount;  // 用户临时帐号名字
        public static int editStatus;  // 1-查看，2-编辑
        public static int tabIndex;
        public static string DoctorSerialCode;

        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetXmlSerializerString(object obj)
        {

            XmlSerializer mySerializer = new XmlSerializer(obj.GetType());
            System.IO.MemoryStream objIo = new System.IO.MemoryStream();

            mySerializer.Serialize(objIo, obj);

            byte[] buffer = objIo.ToArray();


            return System.Text.Encoding.UTF8.GetString(buffer);
        }
        public static string GetCodeByMobile(string sMobile)
        {
            char[] a1 = sMobile.ToCharArray();
            int a = Convert.ToInt32(a1[4]);
            int b = Convert.ToInt32(a1[7]);
            int c = Convert.ToInt32(a1[9]);
            return ((a + b + c + 1) * 9).ToString() + (a * 5).ToString();
        }       /// <summary>
        /// 判断一个DataColumnsCollection是否包含知道数组的列
        /// </summary>
        /// <param name="dcc"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public static bool ColumnsContains(DataColumnCollection dcc, params string[] cols)
        {
            if (dcc == null || dcc.Count.Equals(0))
                return false;
            foreach (string c in cols)
            {
                if (!dcc.Contains(c))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 判断一个DataSet/DataTable是否是NULL
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsNull(DataTable dt)
        {
            return (dt == null || dt.Rows.Count < 1);
        }
        public static bool IsNull(DataSet dt)
        {
            return (dt == null || dt.Tables.Count < 1 || IsNull(dt.Tables[0]));
        }


        //public static string Md5(string str, int code)
        //{
        //    if (String.IsNullOrEmpty(str))
        //        str = String.Empty;

        //    if (code == 16)
        //    {///////////System.Configuration.FormsAuthentication
        //        return HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
        //    }
        //    else
        //    {
        //  return HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
        //    }
        //}
        public static string qsMD5(string str)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(str);
            b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }

        public static string Md532(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            for (int i = 0; i < s.Length; i++)
            {
                pwd = pwd + s[i].ToString("x");
            }
            return pwd;
        }
        /// <summary>
        /// 获取随机Md5编码
        /// </summary>
        /// <returns></returns>
        public static string RandomSerial()
        {
            return "true";//Md5(Guid.NewGuid().ToString());
        }

        /// <summary>
        /// 半角转全角
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(string input)
        {
            if (String.IsNullOrEmpty(input))
                return null;
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
        /// <summary>
        /// 全角转半角
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            if (String.IsNullOrEmpty(input))
                return null;
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        /// <summary>
        /// 智能移除字符串中以input相隔的字符
        /// </summary>
        /// <param name="Args"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string TrimWords(string Args, char split)
        {
            if (String.IsNullOrEmpty(Args))
                return String.Empty;
            string temp = string.Empty;
            foreach (string subStr in Args.Trim(split).Split(split))
                if (temp.IndexOf(split + subStr + split).Equals(-1))
                    temp += split + subStr + split;
            return temp.Trim(split);
        }

        /// <summary>
        /// 返回一个int值的数据
        /// </summary>
        /// <param name="intstr"></param>
        /// <returns></returns>
        public static int CInt(object intstr)
        {
            if (null == intstr)
                return 0;
            int tointstr = 0;
            try
            {
                string mystr = intstr.ToString().Trim();
                if (System.Text.RegularExpressions.Regex.IsMatch(mystr, @"^-?([0-9]{0,9})$"))
                    tointstr = System.Convert.ToInt32(mystr);
                if (Regex.IsMatch(mystr, "^true$", RegexOptions.IgnoreCase))
                {
                    tointstr = 1;
                }
            }
            catch
            {
                tointstr = 0;
            }
            return tointstr;
        }


        public static string CToRate(int subData, int totalData)
        {
            if (totalData <= 0) return null;

            return string.Format("{0:f2}", subData / (totalData * 1.0) * 100) + "%";
        }


        /// <summary>
        /// 不精确的移除全部的HTML标签
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string RemoveHtml(string args)
        {
            if (String.IsNullOrEmpty(args))
                return "";
            return (new System.Text.RegularExpressions.Regex(@"<[^>]+>|</[^>]+>")).Replace(args, "");
        }
        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        /// ###############
        //public static string GetIP()
        //{
        //    string tempIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] ?? HttpContext.Current.Request.UserHostAddress ?? "0.0.0.0";
        //    string[] args = System.Text.RegularExpressions.Regex.Split(tempIp, :"[^\d\.]");
        //    foreach (string s in args)
        //    {
        //        if (!String.IsNullOrEmpty(s) && !s.ToLower().Equals("unkown"))
        //        {
        //            tempIp = s;
        //            break;
        //        }
        //    }
        //    return tempIp;
        //}
        //#########################
        /// <summary>
        /// 获取服务器端IP
        /// </summary>
        /// <returns></returns>############
        //public static string GetServerIP()
        //{
        //    return HttpContext.Current.Request.ServerVariables["Local_Addr"];
        //}

        /// <summary>
        /// 转化为string
        /// </summary>
        /// <param name="intstr"></param>
        /// <returns></returns>
        public static string CStr(object intstr)
        {
            if (intstr == null)
                return null;

            return intstr.ToString();
        }

        /// <summary>
        /// 剪字
        /// </summary>
        /// <param name="soustrP"></param>
        /// <param name="lenP"></param>
        /// <param name="AppendP"></param>
        /// <returns></returns>
        public static string CutStr(string soustrP, int lenP, string AppendP)
        {
            if (String.IsNullOrEmpty(soustrP))
                return String.Empty;
            if (lenP >= soustrP.Length)
                return soustrP;
            else
            {
                if (String.IsNullOrEmpty(AppendP))
                    return soustrP.Substring(0, lenP);
                else
                    return soustrP.Substring(0, lenP) + AppendP;
            }
        }
        public static string CutStr(string soustrP, int lenP)
        {
            return CutStr(soustrP, lenP, null);
        }
        /// <summary>
        /// 检测一个字符串能否Parse成日期格式
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool IsValidDateTime(string sid)
        {
            bool isTrue = false;
            try
            {
                DateTime Uptime = DateTime.Parse(sid);
                isTrue = true;
            }
            catch
            {
                isTrue = false;
            }
            return isTrue;
        }

        #region 时间字符串处理

        /// <summary>
        ///截取字符串(字符串格式为: yyyy-MM-dd)
        /// </summary>
        /// <param name="datestr"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string CutDateStr(string datestr, int type)
        {
            if (string.IsNullOrEmpty(datestr) || datestr.Length < 4 && !IsValidDateTime(datestr))
            {
                return null;
            }

            DateTime time = Convert.ToDateTime(datestr);
            if (type == 0) //截取年份
            {
                return time.Year.ToString();
            }
            else if (type == 1) //截取月份
            {
                return time.Month.ToString();
            }
            else if (type == 2) //截取月份
            {
                return time.Day.ToString();
            }

            return null;
        }

        /// <summary>
        /// 返回年月字符串的时间对象
        /// </summary>
        /// <param name="YearMonth"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string DateString)
        {
            try
            {
                if (DateString == "") return DateTime.MinValue;
                return Convert.ToDateTime(DateString);
            }
            catch// (Exception ex)
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// 返回年月字符串的时间对象
        /// </summary>
        /// <param name="YearMonth"></param>
        /// <returns></returns>
        public static DateTime YMToDateTime(string YearMonth)
        {
            try
            {
                if (YearMonth == "") return DateTime.MinValue;
                return Convert.ToDateTime(YearMonth + "-01");
            }
            catch //(Exception ex)
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// 返回时间的年月部分
        /// </summary>
        /// <param name="ConvertDate"></param>
        /// <returns></returns>
        public static string MonthString(DateTime ConvertDate)
        {
            if (ConvertDate == DateTime.MinValue) return "";
            return ConvertDate.ToString("yyyy-MM");
        }

        /// <summary>
        /// 返回时间的年月部分
        /// </summary>
        /// <param name="ConvertDate"></param>
        /// <returns></returns>
        public static string MonthText(DateTime ConvertDate)
        {
            if (ConvertDate == DateTime.MinValue) return "";
            return ConvertDate.ToString("yyyy年MM月");
        }

        /// <summary>
        /// 返回时间的日期部分
        /// </summary>
        /// <param name="ConvertDate"></param>
        /// <returns></returns>
        public static string DateString(DateTime ConvertDate)
        {
            if (ConvertDate == DateTime.MinValue) return "";
            return ConvertDate.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 返回时间的日期部分
        /// </summary>
        /// <param name="ConvertDate"></param>
        /// <returns></returns>
        public static string DateText(DateTime ConvertDate)
        {
            if (ConvertDate == DateTime.MinValue) return "";
            return ConvertDate.ToString("yyyy年MM月dd日");
        }

        /// <summary>
        /// 返回时间的时间部分
        /// </summary>
        /// <param name="ConvertDate"></param>
        /// <returns></returns>
        public static string TimeString(DateTime ConvertDate)
        {
            if (ConvertDate == DateTime.MinValue) return "";
            return ConvertDate.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 返回时间的完整部分
        /// </summary>
        /// <param name="ConvertDate"></param>
        /// <returns></returns>
        public static string HourString(DateTime ConvertDate)
        {
            if (ConvertDate == DateTime.MinValue) return "";
            return ConvertDate.ToString("yyyy-MM-dd HH:mm");
        }

        /// <summary>
        /// 返回时间的完整部分
        /// </summary>
        /// <param name="ConvertDate"></param>
        /// <returns></returns>
        public static string DateTimeString(DateTime ConvertDate)
        {
            if (ConvertDate == DateTime.MinValue) return "";
            return ConvertDate.ToString("yyyy-MM-dd HH:mm:ss");
        }
        #endregion End 时间字符串处理

        /// <summary>
        /// 得到一个在GOOGLE地图可识别的地址
        /// </summary>
        /// <param name="args">上海市xx路xx号</param>
        /// <returns></returns>
        public static string GetGoogleSmartyAddress(string args)
        {
            if (String.IsNullOrEmpty(args))
                return String.Empty;
            string pre = "";//FGUtility.AppHelper.CityName;
            if (!args.StartsWith(pre))
                args = pre + " " + args;
            int lastpos = args.LastIndexOf("弄");
            if (lastpos != -1)
                return args.Substring(0, lastpos + 1);
            lastpos = args.LastIndexOf("号");
            if (lastpos != -1)
                return args.Substring(0, lastpos + 1);
            lastpos = args.LastIndexOf("路");
            if (lastpos != -1)
                return args.Substring(0, lastpos + 1);
            return args;
        }
        public static string GetSafeChars(string args)
        {
            if (String.IsNullOrEmpty(args))
                return String.Empty;
            return System.Text.RegularExpressions.Regex.Replace(args, @"\W", "").Trim();
        }
        public static void AppendQueryString(ref string sPath, string sQuery, string sValue)
        {
            if (!String.IsNullOrEmpty(sPath))
            {
                System.Text.RegularExpressions.Regex regex1 = null;
                System.Text.RegularExpressions.Match match1 = null;
                sPath = sPath.Trim('&').Trim('?').Trim();
                regex1 = new System.Text.RegularExpressions.Regex(@"(&|\?)" + sQuery + "=([^&]*)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                match1 = regex1.Match(sPath);
                if (match1.Success)
                {
                    sPath = sPath.Replace(match1.Groups[0].ToString(), match1.Groups[1] + sQuery + "=" + sValue);
                }
                else if (sPath.IndexOf("?") != -1)
                {
                    sPath = sPath.Insert(sPath.IndexOf("?") + 1, sQuery + "=" + sValue + "&");
                }
                else
                {
                    sPath = String.Concat(sPath, "?" + sQuery + "=" + sValue);
                }
            }
        }
        public static string ToHtmlString(string str)
        {
            if (String.IsNullOrEmpty(str))
                return null;
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("'", "’");
            str = str.Replace("\r\n", "<br/>");
            //str = str.Replace("\n", "<br/>");
            return str;
        }

        public static bool IsMobile(string sMobile)
        {
            if (String.IsNullOrEmpty(sMobile))
                return false;
            return Regex.IsMatch(sMobile, @"^[1][3,5,8]\d{9}$") || Regex.IsMatch(sMobile, @"^[0][2][1]\d{8}$");
        }

        public static bool IsPhone(string sPhone)
        {
            if (String.IsNullOrEmpty(sPhone))
                return false;
            return Regex.IsMatch(sPhone, @"^((0\d{2,3})-)?(\d{7,8})$");
        }

        private static string BackHtml(string strRow)
        {
            if (String.IsNullOrEmpty(strRow))
                return String.Empty;
            string str = strRow;
            return str.Replace("<br/>", "\r\n");
        }
        public static bool IsEmail(string input)
        {
            if (String.IsNullOrEmpty(input))
                return false;
            return Regex.IsMatch(input, @"^([a-zA-Z0-9_\-\.]+):((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.Compiled);
        }

        /// <summary>
        /// 根据配置文件给对象赋值(不要改变原对象的值!!)
        /// </summary>
        /// <param name="oSource"></param>
        /// <param name="oDestinate"></param>
        /// <param name="sObjectName"></param>
        public static void CopyObject(object oSource, object oDestinate)
        {
            if (oSource == null || oDestinate == null) return;
            Type typeSource, typeDestinate;
            typeSource = oSource.GetType();
            typeDestinate = oDestinate.GetType();
            PropertyInfo[] sourceProperties = typeSource.GetProperties();
            PropertyInfo[] destinateProperties = typeDestinate.GetProperties();
            foreach (PropertyInfo destinateProperty in destinateProperties)
            {
                foreach (PropertyInfo sourceProperty in sourceProperties)
                {
                    if (sourceProperty.Name.ToLower().Equals(destinateProperty.Name.ToLower()))
                    {
                        if (destinateProperty.CanWrite)
                        {
                            try
                            {
                                object oValue = sourceProperty.GetValue(oSource, null);
                                oValue = ReturnObjectType(destinateProperty.PropertyType, oValue);
                                destinateProperty.SetValue(oDestinate, oValue, null);
                            }
                            catch
                            {
                                //###Log.Write(string.Format("赋值出错，字段名:{0},原类型:{1},目标类型:{2}"
                                // , sourceProperty.Name, sourceProperty.PropertyType.ToString(), destinateProperty.PropertyType.ToString()));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 对象类型进行转换
        /// </summary>
        /// <param name="type"></param>
        /// <param name="oValue"></param>
        /// <returns></returns>
        private static object ReturnObjectType(Type type, object oValue)
        {
            if (oValue == null) return null;
            if (type == (typeof(int)))
            {
                try
                {
                    oValue = Convert.ToInt32(oValue);
                }
                catch
                {
                    oValue = 0;
                }
            }
            else if (type == (typeof(DateTime))) oValue = Convert.ToDateTime(oValue);
            else if (type == (typeof(Guid))) oValue = new Guid(oValue.ToString());
            else if (type == (typeof(double))) oValue = Convert.ToDouble(oValue);
            else if (type == (typeof(Enum))) oValue = Enum.ToObject(type, oValue);
            else if (type == (typeof(string))) oValue = oValue.ToString();
            else if (type == (typeof(bool))) oValue = Convert.ToBoolean(oValue);
            return oValue;
        }

        /// <summary>
        /// 通过datarow给对象动态赋值,dr中不存在的属性不赋值
        /// </summary>
        /// <param name="oSource"></param>
        /// <param name="dr"></param>


        #region 字符串文本截取
        public static string SubString(string OrignText, int Length)
        {
            string temp = OrignText;
            if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= Length)
            {
                return temp;
            }

            for (int i = temp.Length; i >= 0; i--)
            {
                temp = temp.Substring(0, i);
                if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= Length - 3)
                {
                    return temp + "..";
                }
            }

            return "";
        }
        #endregion

        #region 简单长文本编码处理
        /// <summary>
        /// 编码长文本
        /// </summary>
        /// <param name="_Text"></param>
        /// <returns></returns>
        public static string EnCodeLongText(string _Text)
        {
            if (_Text == null) return "";
            string Encode = _Text;
            Encode = Encode.Replace("&", "&amp;");
            Encode = Encode.Replace("\"", "&quot;");
            Encode = Encode.Replace("<", "&lt;");
            Encode = Encode.Replace(">", "&gt;");
            Encode = Encode.Replace(" ", "&nbsp;");
            Encode = Encode.Replace("&nbsp;&nbsp;", "　");
            Encode = Encode.Replace("\r\n", "<br />");
            return Encode;
        }

        /// <summary>
        /// 编码长文本
        /// </summary>
        /// <param name="_Text"></param>
        /// <returns></returns>
        public static string EnCodeLongText(object _Text)
        {
            try { return EnCodeLongText(_Text.ToString()); }
            catch
                //(Exception ex)
            { return ""; }
        }

        /// <summary>
        /// 解码长文本
        /// </summary>
        /// <param name="_Text"></param>
        /// <returns></returns>
        public static string DeCodeLongText(string _Text)
        {
            if (_Text == null) return "";
            string Decode = _Text;
            Decode = Decode.Replace("<br />", "\r\n");
            Decode = Decode.Replace("　", "&nbsp;&nbsp;");
            Decode = Decode.Replace("&nbsp;", " ");
            Decode = Decode.Replace("&gt;", ">");
            Decode = Decode.Replace("&lt;", "<");
            Decode = Decode.Replace("&quot;", "\"");
            Decode = Decode.Replace("&amp;", "&");
            return Decode;
        }

        /// <summary>
        /// 解码长文本
        /// </summary>
        /// <param name="_Text"></param>
        /// <returns></returns>
        public static string DeCodeLongText(object _Text)
        {
            try { return DeCodeLongText(_Text.ToString()); }
            catch //(Exception ex) 
            { return ""; }
        }
        #endregion

        #region 状态文本处理方法
        /// <summary>
        /// 根据状态数组获取状态码
        /// </summary>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static int GetEntityStatus(List<int> StatusList)
        {
            if (StatusList.Count.Equals(0)) return 0;

            string StatusCode = "";
            int Max = 0;
            foreach (int Status in StatusList)
                Max = Math.Max(Max, Status);

            for (int i = 0; i < Max; i++)
                StatusCode += "0";
            foreach (int Status in StatusList)
                if (!Status.Equals(0))
                    StatusCode = StatusCode.Substring(0, Max - Status) + "1" + StatusCode.Substring(Max - Status + 1);

            return Convert.ToInt32(StatusCode, 2);
        }

        /// <summary>
        /// 根据状态数组获取状态码
        /// </summary>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static int GetEntityStatus(int Status)
        {
            List<int> StatusList = new List<int>();
            if (!CInt(Status).Equals(0))
                StatusList.Add(Status);
            return GetEntityStatus(StatusList);
        }

        /// <summary>
        /// 根据状态数组获取状态码
        /// </summary>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static int GetEntityStatus(string Status)
        {
            return GetEntityStatus(CInt(Status));
        }

        /// <summary>
        /// 根据状态编码获取状态
        /// </summary>
        /// <param name="EnCodeStatus"></param>
        /// <returns></returns>
        public static string SetEntityStatus(int EnCodeStatus)
        {
            string StatusCode = Convert.ToString(EnCodeStatus, 2);
            string StatusList = "";
            for (int i = StatusCode.Length - 1; i >= 0; i--)
                if (StatusCode.Substring(i, 1) != "0")
                    StatusList += "," + (StatusCode.Length - i).ToString();
            if (StatusList != "") StatusList = StatusList.Substring(1);
            return StatusList;
        }
        #endregion

        #region 字符串处理
        /// <summary>
        /// 数组字符串转换
        /// </summary>
        /// <param name="ParseString"></param>
        /// <param name="SeperateChar"></param>
        /// <returns></returns>
        public static string ParseArray(string ParseString, char SeperateChar)
        {
            if (String.IsNullOrEmpty(ParseString)) return "";
            if (ParseString.IndexOf(SeperateChar) == -1) return String.Format("'{0}'", ParseString);

            string[] PArray = ParseString.Split(SeperateChar);
            string Returns = "";
            foreach (string s in PArray)
                if (!String.IsNullOrEmpty(s))
                    Returns += String.Format(",'{0}'", s);
            if (Returns != "") Returns = Returns.Substring(1);
            return Returns;
        }

        /// <summary>
        /// 数组字符串转换
        /// </summary>
        /// <param name="ParseString"></param>
        /// <returns></returns>
        public static string ParseArray(string ParseString)
        {
            return ParseArray(ParseString, ',');
        }

        /// <summary>
        /// 获取地区权限
        /// </summary>
        /// <param name="AreaCode"></param>
        /// <returns></returns>
        public static string GetAreaLocate(string AreaCode)
        {
            if (AreaCode.Substring(1) == "000000")
                return AreaCode.Substring(0, 1);
            else if (AreaCode.Substring(3) == "0000")
                return AreaCode.Substring(0, 3);
            else if (AreaCode.Substring(5) == "00")
                return AreaCode.Substring(0, 5);
            else
                return AreaCode;
        }
        #endregion

        #region 密码文本加密
        /// <summary>
        /// 密码文本加密
        /// </summary>
        /// <param name="PwdText"></param>
        /// <param name="ExtText"></param>
        public static string PassWordEncode(string PwdText, string ExtText)
        {
            string AdditionText = "98765321";
            string EncodeText = PwdText + AdditionText + ExtText.ToLower();
            // return System.Security.Cryptography.SHA1(//HashPasswordForStoringInConfigFile(EncodeText, "SHA1");
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] result = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(EncodeText));
            string str = "";
            for (int i = 0; i < result.Length; i++)
            {
                str += string.Format("{0:x2}", result[i]); // 此处的x2，和写x得出的结果不一样，应该用x2就对了。
            }
            str = str.ToUpper();
            return str;
        }

        /// <summary>
        /// 密码文本初始化
        /// </summary>
        /// <param name="PwdText"></param>
        /// <param name="ExtText"></param>
        public static string PassWordEncode(string ExtText)
        {
            return PassWordEncode("123456", ExtText);
        }
        #endregion

        #region get XML List

        public static DataSet GetXmlList(string FileName)
        //public void DataView GetXmlList(string FileName)
        {
            string DefaultPath = System.IO.Directory.GetCurrentDirectory();// System.Configuration.ConfigurationManager.AppSettings["ConfigDataPath"];
            DataSet ds = new DataSet();
            string filePath = DefaultPath + "/data/" + FileName;
            ds.ReadXml(filePath);
            return ds;
        }

        #endregion

        #region 根据时间产生随机字符串
        /// <summary>
        /// 根据时间产生随机字符串
        /// </summary>
        /// <param name="Random"></param>
        /// <returns></returns>
        public static string RandomTimeSerial(DateTime Time, int Random)
        {
            string SerialCode = "";
            SerialCode += DemTo36(Time.ToString("yy"));
            SerialCode += DemTo36(Time.Month);
            SerialCode += DemTo36(Time.Day);

            string Second = "000" + DemTo36(Time.Hour * 3600 + Time.Minute * 60 + Time.Second);
            SerialCode += Second.Substring(Second.Length - 4);

            string MillSecond = "0" + DemTo36(Time.Millisecond);
            SerialCode += MillSecond.Substring(MillSecond.Length - 2);

            string RandomCode = "";
            for (int i = 0; i < Random; i++)
                RandomCode += DemTo36(GetMaxRandom(36));

            return SerialCode + RandomCode;
        }
        #endregion

        #region 进制转化
        /// <summary>
        /// 36进制基础字符
        /// </summary>
        private static string Base36String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// 将十进制整数转化为36进制整数
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static string DemTo36(int Number)
        {
            try
            {
                string Value = "";
                int Len = Base36String.Length, Mod;
                while (Number >= Base36String.Length)
                {
                    Mod = Number % Base36String.Length;
                    Value = Base36String.Substring(Mod, 1) + Value;
                    Number = (Number - Mod) / Len;
                }
                Value = Base36String.Substring(Number, 1) + Value;
                return Value;
            }
            catch // (Exception ex)
            {
                //Log.Write("DemTo36 Error:" + ex.Message);
                return "";
            }
        }

        /// <summary>
        /// 将十进制整数转化为36进制整数
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static string DemTo36(string Number)
        {
            try
            {
                int number = Convert.ToInt32(Number);
                return DemTo36(number);
            }
            catch //(Exception ex)
            {
                //###Log.Write("DemTo36 Error:" + ex.Message);
                return "";
            }
        }
        #endregion

        #region 产生随机数
        /// <summary>
        /// 随机数种子
        /// </summary>
        private static System.Random random = new Random();

        /// <summary>
        /// 获得随机数种子
        /// </summary>
        /// <returns></returns>
        private static int GetNormalRandom()
        {
            return random.Next();
        }

        /// <summary>
        /// 取值范围内随机数
        /// </summary>
        /// <returns></returns>
        public static int GetMaxRandom(int Max)
        {
            return random.Next(Max);
        }

        /// <summary>
        /// 取值范围内随机数
        /// </summary>
        /// <returns></returns>
        public static int GetRangeRandom(int Min, int Max)
        {
            return random.Next(Min, Max);
        }
        #endregion


    }
}
