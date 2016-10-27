using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;
using SrvComponent;

namespace BLL
{
    public partial class tPartBlocked
    {
     
        public tPartBlocked()
        {
           
        }
        /// <summary>
        /// 查看是否清仓
        /// </summary>
        /// <param name="sPN">料</param>
        /// <param name="sVC">厂商代码</param>
        /// <param name="sDC">datecode</param>
        /// <param name="sLC">批次</param>
        /// <returns></returns>
        public bool Check_MaterialScrap(string sPN, string sVC, string sDC, string sLC)
        {
            bool sFlag = true;       

            DataTable df = GettPartBlockedByPartNo(sPN).Tables[0];


            if (df.Rows.Count != 0)
            {
                for (int x = 0; x <= df.Rows.Count - 1; x++)
                {
                    switch (df.Rows[x][5].ToString())
                    {
                        case "PN":
                            if (df.Rows[x][1].ToString() == sPN)
                            {
                                sFlag = false;
                            }
                            break;
                        case "PN+DC":
                            string zz = df.Rows[x][1].ToString();
                            string bb = df.Rows[x][2].ToString();
                            if (df.Rows[x][1].ToString() == sPN && df.Rows[x][2].ToString() == sDC)
                            {
                                sFlag = false;
                            }
                            break;
                        case "PN+DC+*":
                            string s = df.Rows[x][2].ToString();
                            string[] sArray = s.Split('*');
                            if (df.Rows[x][1].ToString() == sPN && sArray[0] == sDC.Substring(0, sArray[0].Length))
                            {
                                sFlag = false;
                            }
                            break;
                        case "PN+DC+LN":

                            if (df.Rows[x][1].ToString() == sPN && df.Rows[x][2].ToString() == sDC && df.Rows[x][4].ToString() == sLC)
                            {
                                sFlag = false;
                            }
                            break;
                        case "PN+DC+VC":

                            if (df.Rows[x][1].ToString() == sPN && df.Rows[x][2].ToString() == sDC && df.Rows[x][3].ToString() == sVC)
                            {
                                sFlag = false;
                            }
                            break;
                        case "PN+LN+*":

                            string s1 = df.Rows[x][4].ToString();
                            string[] sArray1 = s1.Split('*');

                            if (df.Rows[x][1].ToString() == sPN && sArray1[0] == sLC.Substring(0, sArray1[0].Length))
                            {
                                sFlag = false;
                            }
                            break;
                        case "PN+LN+~": ///====待测试

                            string s2 = df.Rows[x][4].ToString();
                            string[] sArray2 = s2.Split('~');
                            string sMin_LC = sArray2[0].ToString(); ;
                            string sMax_LC = sArray2[1].ToString(); ;

                            try
                            {
                                int sMinLC = Convert.ToInt32(sArray2[0]);
                                int sMaxLC = Convert.ToInt32(sArray2[1]);
                                int InLC = Convert.ToInt32(sLC);

                                if (df.Rows[x][1].ToString() == sPN && (sMinLC <= InLC && InLC <= sMaxLC))
                                {
                                    sFlag = false;
                                }
                            }

                            catch (Exception)
                            {
                                if ((sMin_LC.Length == sMax_LC.Length) && (sMax_LC.Length == sLC.Length))
                                {
                                    if (df.Rows[x][1].ToString() == sPN && (sMin_LC.CompareTo(sLC) <= 0 && sMax_LC.CompareTo(sLC) >= 0))
                                    {
                                        sFlag = false;
                                    }
                                }
                            }

                            break;
                        case "PN+DC+~": //待测试

                            string s3 = df.Rows[x][2].ToString();
                            string[] sArray3 = s3.Split('~');
                            string sMin_DC = sArray3[0].ToString();
                            string sMax_DC = sArray3[1].ToString();

                            try
                            {
                                int sMinDC = Convert.ToInt32(sArray3[0].ToString());
                                int sMaxDC = Convert.ToInt32(sArray3[1].ToString());
                                int InDC = Convert.ToInt32(sDC);
                                if (df.Rows[x][1].ToString() == sPN && (sMinDC <= InDC && InDC <= sMaxDC))
                                {
                                    sFlag = false;
                                }
                            }
                            catch (Exception)
                            {
                                if ((sMin_DC.Length == sMax_DC.Length) && (sMax_DC.Length == sDC.Length))
                                {
                                    if (df.Rows[x][1].ToString() == sPN && (sMin_DC.CompareTo(sDC) <= 0 && sMax_DC.CompareTo(sDC) >= 0))
                                    {
                                        sFlag = false;
                                    }
                                }
                            }
                            break;
                        case "PN+VC":

                            if (df.Rows[x][1].ToString() == sPN && df.Rows[x][3].ToString() == sVC)
                            {
                                sFlag = false;
                            }
                            break;
                        case "PN+VC+LN":
                            if (df.Rows[x][1].ToString() == sPN && df.Rows[x][3].ToString() == sVC && df.Rows[x][4].ToString() == sLC)
                            {
                                sFlag = false;
                            }
                            break;
                        default:
                            sFlag = true;
                            break;
                    }
                }

            }
            return sFlag;
        }


        public System.Data.DataSet QueryPartBlocked()
        {           
            string fieldlist = "part_no as 料号,date_code as 生产周期,vender_code as 厂商代码,Lot_id as 生产批次,check_type as 检查类型,Used_flag as 是否检查,In_station_time as 进入系统时间,emp_no as 工号".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);          
            return dp.GetData("SFCR.T_PART_BLOCKED", fieldlist, null, out count);
        }
        public void InsertPartBlocked(string dicstring)
        {        
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
           if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.MySql)            
            mst.Add("IN_STATION_TIME", System.DateTime.Now);
            dp.AddData("SFCR.T_PART_BLOCKED", mst);
        }

        public void UpdatePartBlocked(string dicstring)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.MySql)    
            mst.Add("in_station_time".ToUpper(), System.DateTime.Now);
            dp.UpdateData("SFCR.T_PART_BLOCKED", new string[] { "PART_NO" }, mst);
        }
        public void DeletePartBlocked(string dicstring)
        {
            string table = "SFCR.T_PART_BLOCKED";
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
            dp.DeleteData(table, mst);
        }   

        public System.Data.DataSet GettPartBlockedByPartNo(string partno)
        {          
            string fieldlist = "*".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("PART_NO", partno);
            return dp.GetData("SFCR.T_PART_BLOCKED", fieldlist, mst, out count) ;
        }
    }
}
