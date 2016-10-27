using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;
using System.Management;

namespace Packing_Ctn
{
    public class Function
    {

        public static DataTable DataTableToSort(DataTable dt, string Colnums)
        {
            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} ASC", Colnums);
            return dv.ToTable();
        }
        /// <summary>
        /// 返回一个新的datatable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable getNewTable(DataTable dt, string sql)
        {
            try
            {
                DataTable mydt = new DataTable();
                mydt = dt.Clone();
                DataRow[] arrDr = dt.Select(sql);
                for (int i = 0; i < arrDr.Length; i++)
                {
                    mydt.ImportRow((DataRow)arrDr[i]);
                }
                return mydt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region 获取本机电脑相关信息

        /// <summary> 
        /// 获取主机名 
        /// </summary> 
        /// <returns></returns> 
        public static string HostName
        {
            get
            {
                string hostname = Dns.GetHostName();
                return hostname;
            }
        }
        /// <summary> 
        /// 获取IP地址 
        /// </summary> 
        /// <returns></returns> 
        public static List<string> GetIPList()
        {
            List<string> ipList = new List<string>();
            IPAddress[] addressList = Dns.GetHostEntry(HostName).AddressList;
            for (int i = 0; i < addressList.Length; i++)
            {
                ipList.Add(addressList[i].ToString());
            }
            return ipList;
        }
        /// <summary> 
        /// 获取Mac地址 
        /// </summary> 
        /// <returns></returns> 
        public static List<string> getMacList()
        {
            List<string> macList = new List<string>();
            ManagementClass mc;
            mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["IPEnabled"].ToString() == "True")
                    macList.Add(mo["MacAddress"].ToString());
            }
            return macList;
        }

        /// <summary>
        /// 排序Datable,以某一列为基准
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DataTable DataTableSort(DataTable dt, string val)
        {
            int y = 0;
            for (int x = 0; x < dt.Columns.Count; x++)
            {
                if (dt.Columns[x].ColumnName == val)
                    y++;
            }
            DataTable dtSot = null;
            if (y == 0)
            {
                dtSot = dt;
            }
            else
            {
                DataView dv = new DataView(dt);
                dv.Sort = string.Format("{0} asc", val);
                dtSot = dv.ToTable();
            }
            DataTable dtPrt = new DataTable();
            dtPrt.Columns.Add("Name", Type.GetType("System.String"));
            dtPrt.Columns.Add("val", Type.GetType("System.String"));
            foreach (DataColumn Colnums in dtSot.Columns)
            {
                int x = 0;
                foreach (DataRow dr in dtSot.Rows)
                {
                    x++;
                    dtPrt.Rows.Add(Colnums.ColumnName + x.ToString().PadLeft(2, '0'), dr[Colnums].ToString());
                }
            }
            return dtPrt;
        }

        /// <summary>
        /// 数据排列
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>
        public static DataTable DataTableCross(DataTable dt, int Flag)
        {
            DataTable resdt = dt;
            if (dt.Rows.Count > 0)
            {
                if (Flag == 3)
                {
                    #region DataTable有三列时使用
                    DataTable result = new DataTable();
                    for (int i = 0; i < (Flag - 2); i++)
                    {
                        result.Columns.Add(dt.Columns[i].ColumnName);
                    }
                    DataTable dtColumns = dt.DefaultView.ToTable("dtColumns", true, dt.Columns[Flag - 2].ColumnName); //以第六列为字段建立交叉表列名
                    for (int i = 0; i < dtColumns.Rows.Count; i++)
                    {
                        string colName;
                        if (dtColumns.Rows[i][0] is System.DateTime)
                        {
                            colName = Convert.ToDateTime(dtColumns.Rows[i][0]).ToString();
                        }
                        else
                        {
                            colName = dtColumns.Rows[i][0].ToString();
                        }
                        result.Columns.Add(colName);
                        result.Columns[i + (Flag - 2)].DefaultValue = "0";// result.Columns[i + 1].DefaultValue = "0"; 因为前两列值一致,增加一个默认值
                    }

                    DataRow drNew = result.NewRow();
                    for (int i = 0; i < (Flag - 2); i++)
                    {
                        drNew[i] = dt.Rows[0][i];
                    }
                    string rowName = drNew[0].ToString();
                    foreach (DataRow dr in dt.Rows)
                    {
                        string colName = dr[Flag - 2].ToString(); //以第四列为字段建立交叉表列名
                        // double dValue = Convert.ToDouble(dr[4]);//以第五列值添加
                        string dValue = dr[Flag - 1].ToString();
                        if (dr[0].ToString().Equals(rowName, StringComparison.CurrentCultureIgnoreCase))
                        {
                            drNew[colName] = dValue.ToString();
                        }
                        else
                        {
                            result.Rows.Add(drNew);
                            drNew = result.NewRow();
                            for (int i = 0; i < (Flag - 2); i++)
                            {
                                drNew[i] = dr[i];
                            }
                            rowName = drNew[0].ToString();
                            drNew[colName] = dValue.ToString();
                        }
                    }
                    result.Rows.Add(drNew);
                    resdt = result;
                    #endregion
                }
            }
            return resdt;
        }
        #endregion
    }
}
