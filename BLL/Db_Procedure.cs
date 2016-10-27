using System;
using System.Collections.Generic;
using System.Text;
//using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;
using SrvComponent;
using GenericProvider;
using GenericUtil;
using System.Data.Common;
using SystemObject;
using System.IO;

namespace BLL
{
    public class Db_Procedure
    {
        public Db_Procedure()
        {
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.MySql)
                DB_Flag = 0;
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.Oracle)
                DB_Flag = 1;
        }
        /// <summary>
        /// 数据库标记 0 MySQL;1 Oracle
        /// </summary>
        int DB_Flag = 0;
        /// <summary>
        /// 记录时间
        /// </summary>
        System.Diagnostics.Stopwatch myWatch = System.Diagnostics.Stopwatch.StartNew();
        private Object thisLock = new Object();

        BLL.tWarehouseWipTracking wwt = new tWarehouseWipTracking();
        BLL.tErrorCode tec = new tErrorCode();
        BLL.tUserInfo user = new tUserInfo();
        BLL.tWipTracking twip = new tWipTracking();
        BLL.tWoInfo woinfo = new tWoInfo();
        BLL.tRouteInfo ClsRoute = new tRouteInfo();
        BLL.tWipKeyPart ClsKeyPart = new tWipKeyPart();
        BLL.tWipTracking mWipTracking = new tWipTracking();
        BLL.tBomKeyPart ClsBomKeyPart = new tBomKeyPart();
        BLL.tPalletInfo ClsPalletInfo = new tPalletInfo();
        BLL.tStationrecount tStaR = new tStationrecount();
        BLL.tProduct ClsProduct = new tProduct();
        BLL.T_DS_Out tDS = new T_DS_Out();
        BLL.R_Tr_Sn trsn = new R_Tr_Sn();
        BLL.tPartStorehousehad tPartSH = new tPartStorehousehad();
        BLL.T_EQC_TOOLS _Tools = new T_EQC_TOOLS();
        FileHelper _FileHelper = new FileHelper();

        /// <summary>
        /// 返回一个新的datatable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        private DataTable getNewTable(DataTable dt, string sql)
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

        /// <summary>
        /// 讲纵向数据横向排列
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>
        public DataTable DataTableCross(DataTable dt, int Flag)
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
                        if (dtColumns.Rows[i][0] is DateTime)
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
                else
                    if (Flag == 4)
                    {
                        #region DataTable有四列时使用
                        DataTable result = new DataTable();
                        for (int i = 0; i < (Flag - 2); i++)
                        {
                            result.Columns.Add(dt.Columns[i].ColumnName);
                        }
                        DataTable dtColumns = dt.DefaultView.ToTable("dtColumns", true, dt.Columns[Flag - 2].ColumnName); //以第六列为字段建立交叉表列名
                        for (int i = 0; i < dtColumns.Rows.Count; i++)
                        {
                            string colName;
                            if (dtColumns.Rows[i][0] is DateTime)
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
                            if ((dr[0].ToString().Equals(rowName, StringComparison.CurrentCultureIgnoreCase)) &&
                                (dr[1].ToString().Equals(drNew[1].ToString(), StringComparison.CurrentCultureIgnoreCase)))
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
                    else
                        if (Flag == 5)
                        {
                            #region  DataTable有五列时使用
                            DataTable result = new DataTable();
                            result.Columns.Add(dt.Columns[0].ColumnName);
                            result.Columns.Add(dt.Columns[1].ColumnName);//增加第二列的值
                            result.Columns.Add(dt.Columns[2].ColumnName);//增加第三列的值

                            DataTable dtColumns = dt.DefaultView.ToTable("dtColumns", true, dt.Columns[3].ColumnName); //以第四列为字段建立交叉表列名
                            for (int i = 0; i < dtColumns.Rows.Count; i++)
                            {
                                string colName;
                                if (dtColumns.Rows[i][0] is DateTime)
                                {
                                    colName = Convert.ToDateTime(dtColumns.Rows[i][0]).ToString();
                                }
                                else
                                {
                                    colName = dtColumns.Rows[i][0].ToString();
                                }
                                result.Columns.Add(colName);
                                result.Columns[i + 3].DefaultValue = "0";// result.Columns[i + 1].DefaultValue = "0"; 因为前两列值一致,增加一个默认值
                            }
                            DataRow drNew = result.NewRow();
                            drNew[0] = dt.Rows[0][0];
                            drNew[1] = dt.Rows[0][1]; //增加第二列的值
                            drNew[2] = dt.Rows[0][2]; //增加第三列的值
                            string rowName = drNew[0].ToString();
                            foreach (DataRow dr in dt.Rows)
                            {
                                string colName = dr[3].ToString(); //以第四列为字段建立交叉表列名
                                // double dValue = Convert.ToDouble(dr[4]);//以第五列值添加
                                string dValue = dr[4].ToString();
                                if ((dr[0].ToString().Equals(rowName, StringComparison.CurrentCultureIgnoreCase)) &&
                                    (dr[1].ToString().Equals(drNew[1].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                    (dr[2].ToString().Equals(drNew[2].ToString(), StringComparison.CurrentCultureIgnoreCase)))
                                {

                                    drNew[colName] = dValue.ToString();
                                }
                                else
                                {
                                    result.Rows.Add(drNew);
                                    drNew = result.NewRow();
                                    drNew[0] = dr[0];
                                    drNew[1] = dr[1];//增加第二列的值
                                    drNew[2] = dr[2];//增加第三列的值
                                    rowName = drNew[0].ToString();
                                    drNew[colName] = dValue.ToString();
                                }
                            }
                            result.Rows.Add(drNew);
                            resdt = result;
                            #endregion
                        }
                        else
                            if (Flag == 6)
                            {
                                #region DataTable有六列时使用
                                DataTable result = new DataTable();
                                for (int i = 0; i < (Flag - 2); i++)
                                {
                                    result.Columns.Add(dt.Columns[i].ColumnName);
                                }
                                DataTable dtColumns = dt.DefaultView.ToTable("dtColumns", true, dt.Columns[Flag - 2].ColumnName); //以第六列为字段建立交叉表列名
                                for (int i = 0; i < dtColumns.Rows.Count; i++)
                                {
                                    string colName;
                                    if (dtColumns.Rows[i][0] is DateTime)
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
                                    if ((dr[0].ToString().Equals(rowName, StringComparison.CurrentCultureIgnoreCase)) &&
                                        (dr[1].ToString().Equals(drNew[1].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                        (dr[2].ToString().Equals(drNew[2].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                        (dr[3].ToString().Equals(drNew[3].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                        (dr[4].ToString().Equals(drNew[4].ToString(), StringComparison.CurrentCultureIgnoreCase))
                                        )
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
                            else
                                if (Flag == 7)
                                {
                                    #region DataTable有七列时使用
                                    DataTable result = new DataTable();
                                    for (int i = 0; i < (Flag - 2); i++)
                                    {
                                        result.Columns.Add(dt.Columns[i].ColumnName);
                                    }
                                    DataTable dtColumns = dt.DefaultView.ToTable("dtColumns", true, dt.Columns[Flag - 2].ColumnName); //以第六列为字段建立交叉表列名
                                    for (int i = 0; i < dtColumns.Rows.Count; i++)
                                    {
                                        string colName;
                                        if (dtColumns.Rows[i][0] is DateTime)
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
                                        if ((dr[0].ToString().Equals(rowName, StringComparison.CurrentCultureIgnoreCase)) &&
                                            (dr[1].ToString().Equals(drNew[1].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                             (dr[2].ToString().Equals(drNew[2].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                             (dr[3].ToString().Equals(drNew[3].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                             (dr[4].ToString().Equals(drNew[4].ToString(), StringComparison.CurrentCultureIgnoreCase)))
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
                                else
                                    if (Flag == 8)
                                    {
                                        #region DataTable有八列时使用
                                        DataTable result = new DataTable();
                                        for (int i = 0; i < (Flag - 2); i++)
                                        {
                                            result.Columns.Add(dt.Columns[i].ColumnName);
                                        }
                                        DataTable dtColumns = dt.DefaultView.ToTable("dtColumns", true, dt.Columns[Flag - 2].ColumnName); //以第六列为字段建立交叉表列名
                                        for (int i = 0; i < dtColumns.Rows.Count; i++)
                                        {
                                            string colName;
                                            if (dtColumns.Rows[i][0] is DateTime)
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
                                            if ((dr[0].ToString().Equals(rowName, StringComparison.CurrentCultureIgnoreCase)) &&
                                                (dr[1].ToString().Equals(drNew[1].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                (dr[2].ToString().Equals(drNew[2].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                (dr[3].ToString().Equals(drNew[3].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                (dr[4].ToString().Equals(drNew[4].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                (dr[5].ToString().Equals(drNew[5].ToString(), StringComparison.CurrentCultureIgnoreCase))
                                                )
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
                                    else
                                        if (Flag == 9)
                                        {
                                            #region DataTable有九列时使用
                                            DataTable result = new DataTable();
                                            for (int i = 0; i < (Flag - 2); i++)
                                            {
                                                result.Columns.Add(dt.Columns[i].ColumnName);
                                            }
                                            DataTable dtColumns = dt.DefaultView.ToTable("dtColumns", true, dt.Columns[Flag - 2].ColumnName); //以第六列为字段建立交叉表列名
                                            for (int i = 0; i < dtColumns.Rows.Count; i++)
                                            {
                                                string colName;
                                                if (dtColumns.Rows[i][0] is DateTime)
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
                                                if ((dr[0].ToString().Equals(rowName, StringComparison.CurrentCultureIgnoreCase)) &&
                                                    (dr[1].ToString().Equals(drNew[1].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                    (dr[2].ToString().Equals(drNew[2].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                    (dr[3].ToString().Equals(drNew[3].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                    (dr[4].ToString().Equals(drNew[4].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                    (dr[5].ToString().Equals(drNew[5].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                    (dr[6].ToString().Equals(drNew[6].ToString(), StringComparison.CurrentCultureIgnoreCase))
                                                    )
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
                                        else

                                            if (Flag == 10)
                                            {
                                                #region DataTable有十列时使用
                                                DataTable result = new DataTable();
                                                for (int i = 0; i < (Flag - 2); i++)
                                                {
                                                    result.Columns.Add(dt.Columns[i].ColumnName);
                                                }
                                                DataTable dtColumns = dt.DefaultView.ToTable("dtColumns", true, dt.Columns[Flag - 2].ColumnName); //以第六列为字段建立交叉表列名
                                                for (int i = 0; i < dtColumns.Rows.Count; i++)
                                                {
                                                    string colName;
                                                    if (dtColumns.Rows[i][0] is DateTime)
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
                                                    if ((dr[0].ToString().Equals(rowName, StringComparison.CurrentCultureIgnoreCase)) &&
                                                        (dr[1].ToString().Equals(drNew[1].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                        (dr[2].ToString().Equals(drNew[2].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                        (dr[3].ToString().Equals(drNew[3].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                        (dr[4].ToString().Equals(drNew[4].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                        (dr[5].ToString().Equals(drNew[5].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                        (dr[6].ToString().Equals(drNew[6].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                        (dr[7].ToString().Equals(drNew[7].ToString(), StringComparison.CurrentCultureIgnoreCase))
                                                        )
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
                                            else

                                                if (Flag == 11)
                                                {
                                                    #region DataTable有十一列时使用
                                                    DataTable result = new DataTable();
                                                    for (int i = 0; i < (Flag - 2); i++)
                                                    {
                                                        result.Columns.Add(dt.Columns[i].ColumnName);
                                                    }
                                                    DataTable dtColumns = dt.DefaultView.ToTable("dtColumns", true, dt.Columns[Flag - 2].ColumnName); //以第六列为字段建立交叉表列名
                                                    for (int i = 0; i < dtColumns.Rows.Count; i++)
                                                    {
                                                        string colName;
                                                        if (dtColumns.Rows[i][0] is DateTime)
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
                                                        if ((dr[0].ToString().Equals(rowName, StringComparison.CurrentCultureIgnoreCase)) &&
                                                            (dr[1].ToString().Equals(drNew[1].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                            (dr[2].ToString().Equals(drNew[2].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                            (dr[3].ToString().Equals(drNew[3].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                            (dr[4].ToString().Equals(drNew[4].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                            (dr[5].ToString().Equals(drNew[5].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                            (dr[6].ToString().Equals(drNew[6].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                            (dr[7].ToString().Equals(drNew[7].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                            (dr[8].ToString().Equals(drNew[8].ToString(), StringComparison.CurrentCultureIgnoreCase))
                                                            )
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
                                                else

                                                    if (Flag == 12)
                                                    {
                                                        #region DataTable有十二列时使用
                                                        DataTable result = new DataTable();
                                                        for (int i = 0; i < (Flag - 2); i++)
                                                        {
                                                            result.Columns.Add(dt.Columns[i].ColumnName);
                                                        }
                                                        DataTable dtColumns = dt.DefaultView.ToTable("dtColumns", true, dt.Columns[Flag - 2].ColumnName); //以第六列为字段建立交叉表列名
                                                        for (int i = 0; i < dtColumns.Rows.Count; i++)
                                                        {
                                                            string colName;
                                                            if (dtColumns.Rows[i][0] is DateTime)
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
                                                            if ((dr[0].ToString().Equals(rowName, StringComparison.CurrentCultureIgnoreCase)) &&
                                                                (dr[1].ToString().Equals(drNew[1].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                                (dr[2].ToString().Equals(drNew[2].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                                (dr[3].ToString().Equals(drNew[3].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                                (dr[4].ToString().Equals(drNew[4].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                                (dr[5].ToString().Equals(drNew[5].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                                (dr[6].ToString().Equals(drNew[6].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                                (dr[7].ToString().Equals(drNew[7].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                                (dr[8].ToString().Equals(drNew[8].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                                                                (dr[9].ToString().Equals(drNew[9].ToString(), StringComparison.CurrentCultureIgnoreCase))
                                                                )
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

        /// <summary>
        /// 获取当前时间是第几周
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string GetWeekOfYear()
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekOfYear.ToString().PadLeft(2, '0');
        }

        private const string X36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public string CONVERTHEX10TO36(int val)
        {
            string result = "";
            while (val >= 36)
            {
                result = X36[val % 36] + result;
                val /= 36;
            }
            if (val >= 0) result = X36[val] + result;
            return result;

        }
        public int CONVERTHEX36TO10(string str)
        {

            int result = 0;
            int len = str.Length;
            for (int i = len; i > 0; i--)
            {
                result += X36.IndexOf(str[i - 1]) * Convert.ToInt32(Math.Pow(36, len - i));
            }
            return result;
        }

        private string INSER_TRSN_DETAIL(Dictionary<string, object> dic)
        {
            return INSER_TRSN_DETAIL(dic["TR_SN"].ToString());
        }
        public string INSER_TRSN_DETAIL(string Tr_Sn)
        {
            string _StrErr = "OK";
            try
            {
                string _Msg = string.Empty;
                DataTable dt = trsn.Sel_Tr_Sn_Info(Tr_Sn, out _Msg).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("PO_ID", dt.Rows[0]["PO_ID"].ToString());
                    dic.Add("TR_SN", dt.Rows[0]["TR_SN"].ToString());
                    dic.Add("KP_NO", dt.Rows[0]["KP_NO"].ToString());
                    dic.Add("VENDER_ID", dt.Rows[0]["VENDER_ID"].ToString());
                    dic.Add("DATE_CODE", dt.Rows[0]["DATE_CODE"].ToString());
                    dic.Add("LOT_CODE", dt.Rows[0]["LOT_CODE"].ToString());
                    dic.Add("QTY",Convert.ToInt32( dt.Rows[0]["QTY"].ToString()));
                    dic.Add("STOCK_ID", dt.Rows[0]["STOCK_ID"].ToString());
                    dic.Add("LOC_ID", dt.Rows[0]["LOC_ID"].ToString());
                    dic.Add("TANSFER_NO", dt.Rows[0]["TANSFER_NO"].ToString());
                    dic.Add("URGENT_PN", dt.Rows[0]["URGENT_PN"].ToString());
                    dic.Add("STOCK_NO", dt.Rows[0]["STOCK_NO"].ToString());
                    dic.Add("STOCK_DATE", dt.Rows[0]["STOCK_DATE"].ToString());
                    dic.Add("WOID", dt.Rows[0]["WOID"].ToString());
                    dic.Add("USER_ID", dt.Rows[0]["USER_ID"].ToString());
                    dic.Add("STATUS", dt.Rows[0]["STATUS"].ToString());
                    dic.Add("UPDATE_DATE", dt.Rows[0]["UPDATE_DATE"].ToString());
                    dic.Add("REMARK1", dt.Rows[0]["REMARK1"].ToString());
                    dic.Add("REMARK2", dt.Rows[0]["REMARK2"].ToString());
                   
                    dp.AddData("SFCR.R_TR_SN_DETAIL", dic);

                }
                else
                {
                    _StrErr = "Tr Sn Not Found";
                }
            }
            catch (Exception ex)
            {
                _StrErr = ex.Message;
                Insert_Exception_Log("INSER_TRSN_DETAIL " + _StrErr);
            }

            return _StrErr;
        }

        private string PRO_CHECKEC(IDictionary<string, object> Dic)
        {
            return PRO_CHECKEC(Dic["DATA"].ToString());
        }
        public string PRO_CHECKEC(string DATA)
        {
            string _StrErr = "NO EC";
            if (!string.IsNullOrEmpty(DATA))
            {
                DataTable dt = tec.GetErrorCode(DATA).Tables[0];
                if (dt.Rows.Count > 0)
                    _StrErr = "OK";
            }
            return _StrErr;

        }
        private string PRO_CHECKEMP(IDictionary<string, object> Dic)
        {
            return PRO_CHECKEMP(Dic["DATA"].ToString());
        }
        public string PRO_CHECKEMP(string DATA)
        {
            string _StrErr = "OK";
            try
            {
                string UserId = DATA.Split('-')[0];
                string UserPwd = DATA.Split('-')[1];
                if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(UserPwd))
                {
                    //Dictionary<string, object> mst = new Dictionary<string, object>();
                    //mst.Add("USERID", UserId);
                    //mst.Add("PWD", UserPwd);
                    //mst.Add("USERSTATUS", "1");
                    DataTable dt = user.GetUserInfo(UserId, null, UserPwd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["USERSTATUS"].ToString() == "1")
                        {
                            _StrErr = "OK";
                        }
                        else
                        {
                            _StrErr = "NO EMP";
                        }
                    }
                    else
                    {
                        _StrErr = "NO EMP";
                    }
                }
                else
                {
                    _StrErr = "USERID OR PASSWORD IS NULL";
                }

            }
            catch (Exception ex)
            {
                _StrErr = "Format Error " + ex.Message;
            }


            return _StrErr;

        }

        public string PRO_CHECKEMP_NEW(string DATA, string IPADDRESS, string MACADDRESS, string MYGROUP)
        {
            string _StrErr = "IP ADDRESS EMPTY";
            if (!string.IsNullOrEmpty(IPADDRESS))
            {
                _StrErr = PRO_CHECKEMP(DATA);
                if (_StrErr != "OK")
                    _StrErr += "," + DATA;
            }
            return _StrErr;
        }

        private string PRO_CHECK_ROUTE(IDictionary<string, object> Dic)
        {
            return PRO_CHECK_ROUTE(Dic["DATA"].ToString(), Dic["MYGROUP"].ToString());
        }
        public string PRO_CHECK_ROUTE(string DATA, string MYGROUP)
        {
            myWatch.Restart();
            #region 成员变量
            string C_ROUTECODE = string.Empty;
            string C_ERRFLAG = string.Empty;
            string C_LOCGROUP = string.Empty;
            string C_NEXTSTATION = string.Empty;
            string C_WO = string.Empty;
            string C_ENDGROUP = string.Empty;
            string C_GROUPNEXT = string.Empty;
            #endregion

            string RES = "RTBL ERR";
            if (DB_Flag == 0)
            {
                DataTable dtwip = twip.Get_WIP_TRACKING("ESN", DATA, "WOID,LOCSTATION,NEXTSTATION,ERRFLAG,ROUTGROUPID").Tables[0];
                if (dtwip.Rows.Count > 0)
                {
                    C_ROUTECODE = dtwip.Rows[0]["ROUTGROUPID"].ToString();
                    C_ERRFLAG = dtwip.Rows[0]["ERRFLAG"].ToString();
                    C_LOCGROUP = dtwip.Rows[0]["LOCSTATION"].ToString();
                    C_NEXTSTATION = dtwip.Rows[0]["NEXTSTATION"].ToString();
                    C_WO = dtwip.Rows[0]["WOID"].ToString();

                    if (C_NEXTSTATION == "NA" || string.IsNullOrEmpty(C_NEXTSTATION))
                    {
                        DataTable dtwo = woinfo.GetWoInfo(C_WO, null, "OUTPUTGROUP").Tables[0];
                        C_ENDGROUP = dtwo.Rows[0]["OUTPUTGROUP"].ToString();
                        if (C_ENDGROUP == C_LOCGROUP)
                            RES = "ROUTE END";
                        else
                        {
                            C_GROUPNEXT = ClsRoute.CHECK_ROUTE_INFO(C_ERRFLAG, C_ROUTECODE, C_LOCGROUP, MYGROUP);
                            if (!string.IsNullOrEmpty(C_GROUPNEXT))
                                RES = "OK";
                            else
                            {
                                C_GROUPNEXT = ClsRoute.CHECK_ROUTE_INFO(C_ROUTECODE, C_LOCGROUP, C_ERRFLAG);
                                if (!string.IsNullOrEmpty(C_GROUPNEXT))
                                    RES = C_GROUPNEXT;
                                else
                                    RES = "ROUTE END ERR";
                            }
                        }
                    }
                    else
                    {
                        if (C_NEXTSTATION == MYGROUP)
                            RES = "OK";
                        else
                            RES = C_NEXTSTATION;
                    }
                }
            }
            if (DB_Flag == 1)
            {
                try
                {
                    IBaseProvider dp = new BaseProvider();
                    IDictionary<string, object> parms = new Dictionary<string, object>();
                    IDictionary<string, object> procedureOutRes = new Dictionary<string, object>();
                    parms.Add("DATA", DATA);
                    parms.Add("MYGROUP",MYGROUP);                   
                    procedureOutRes.Add("RES", (object)200);
                    dp.StoreProcedureExec("PRO_CHECKROUTE", parms, procedureOutRes);
                    RES = procedureOutRes["RES"].ToString(); ;
                }
                catch (Exception ex)
                {
                    RES = ex.Message;
                }
            }
            myWatch.Stop();
            Insert_DB_Log("PRO_CHECK_ROUTE-->" + myWatch.ElapsedMilliseconds.ToString());
            return RES;
        }

        public string PRO_CHECK_ROUTE_RE(string DATA, string MYGROUP)
        {
            myWatch.Restart();
            #region 成员变量
            string C_ROUTECODE = string.Empty;
            string C_ERRFLAG = string.Empty;
            string C_LOCGROUP = string.Empty;
            string C_NEXTSTATION = string.Empty;
            string C_WO = string.Empty;
            string C_ENDGROUP = string.Empty;
            string C_GROUPNEXT = string.Empty;
            #endregion

            string RES = "RTBL ERR";
            DataTable dtwip = twip.Get_WIP_TRACKING("ESN", DATA, "WOID,LOCSTATION,NEXTSTATION,ERRFLAG,ROUTGROUPID").Tables[0];
            if (dtwip.Rows.Count > 0)
            {
                C_ROUTECODE = dtwip.Rows[0]["ROUTGROUPID"].ToString();
                C_ERRFLAG = dtwip.Rows[0]["ERRFLAG"].ToString();
                C_LOCGROUP = dtwip.Rows[0]["LOCSTATION"].ToString();
                C_NEXTSTATION = dtwip.Rows[0]["NEXTSTATION"].ToString();
                C_WO = dtwip.Rows[0]["WOID"].ToString();

                if (C_NEXTSTATION == "NA" || string.IsNullOrEmpty(C_NEXTSTATION))
                {
                    try
                    {
                        C_GROUPNEXT = ClsRoute.CHECK_ROUTE_INFO(C_ERRFLAG, C_ROUTECODE, C_LOCGROUP, MYGROUP);
                        if (string.IsNullOrEmpty(C_GROUPNEXT))
                            throw new Exception();
                        C_GROUPNEXT = ClsRoute.CHECK_ROUTE_INFO("1", C_ROUTECODE, MYGROUP, "R_" + MYGROUP);
                        if (string.IsNullOrEmpty(C_GROUPNEXT))
                            throw new Exception();
                        RES = "OK";

                    }
                    catch
                    {
                        C_GROUPNEXT = ClsRoute.CHECK_ROUTE_INFO_RE(C_ROUTECODE, C_LOCGROUP, C_ERRFLAG);
                        if (!string.IsNullOrEmpty(C_ROUTECODE))
                            RES = C_GROUPNEXT;
                        else
                            RES = "ROUTE END";
                    }
                }
                else
                {
                    if (C_NEXTSTATION == MYGROUP)
                        RES = "OK";
                    else
                        RES = C_NEXTSTATION;
                }
            }
            myWatch.Stop();
            Insert_DB_Log("PRO_CHECK_ROUTE_RE-->" + myWatch.ElapsedMilliseconds.ToString());
            return RES;
        }

        private string PRO_CHECK_SN(IDictionary<string,object> Dic)
        {
            return PRO_CHECK_SN(Dic["DATA"].ToString());
        }
        public string PRO_CHECK_SN(string DATA)
        {
            string C_ERRFLAG = string.Empty;
            string C_SCRAPFLAG = string.Empty;
            string RES = "OK";
            DataTable dt = twip.Get_WIP_TRACKING("ESN", DATA, "ERRFLAG,SCRAPFLAG").Tables[0];
            if (dt.Rows.Count > 0)
            {
                C_ERRFLAG = dt.Rows[0]["ERRFLAG"].ToString();
                C_SCRAPFLAG = dt.Rows[0]["SCRAPFLAG"].ToString();
                if (C_ERRFLAG != "0")
                {
                    RES = "SN IN REPAIR";
                }
                if (C_SCRAPFLAG != "0")
                {
                    RES = "SN HAS SCRAP";
                }
            }
            else
            {
                RES = "NO SN";
            }
            return RES;
        }


        /// <summary>
        /// 10转16进制
        /// </summary>
        /// <param name="Data">参数：10进制，int类型</param>
        /// <returns>返回16进制string类型</returns>
        public string ConvertTenToSixteen(int Data)
        {
            try
            {
                return Data.ToString("X");
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 16进制转10进制
        /// </summary>
        /// <param name="Data">参数：16进制，string类型</param>
        /// <returns>返回10进制int类型</returns>
        public int ConvertSixteenToTen(string Data)
        {
            try
            {
                return int.Parse(Data, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            catch
            {
                return 0;
            }
        }

        private string PRO_CHECK_KPS_VALID(IDictionary<string, object> Dic)
        {
            return PRO_CHECK_KPS_VALID(Dic["DATA"].ToString(), Dic["SN"].ToString(), Dic["WO"].ToString());
        }
        public string PRO_CHECK_KPS_VALID(string DATA, string SN, string WO)
        {
            #region 成员变量
            string C_ERRFLAG = string.Empty;
            string C_LOCSTATION = string.Empty;
            string C_WO = string.Empty;
            string C_PARTNUMBER = string.Empty;
            DataTable dtwoInfo = null;
            string C_BOMNO = string.Empty;
            string C_LASTGROUP = string.Empty;
            DataTable dtBom = null;
            int N_COUNT = 0;
            #endregion
            string RES = "";
            try
            {
                if (SN == DATA)
                    throw new Exception("SN = KPS");
                DataTable dtsnval = ClsKeyPart.GetWipKeyParts("SNVAL",DATA).Tables[0];
                if (dtsnval.Rows.Count > 0)
                {
                    //if (dtsnval.Rows.Count == 1 && dtsnval.Rows[0]["ESN"].ToString() == dtsnval.Rows[0]["SNVAL"].ToString())
                    //{
                    //}
                    //else
                    throw new Exception("KPS DUP");
                }
                else
                {
                    if (ClsKeyPart.GetWipKeyParts("ESN",SN).Tables[0].Rows.Count > 0)
                        throw new Exception("SN HAVE KPS");
                }
                DataTable dtWipInfo = twip.Get_WIP_TRACKING("ESN", DATA, "WOID,PARTNUMBER,LOCSTATION,ERRFLAG").Tables[0];
                if (dtWipInfo.Rows.Count == 0)
                    throw new Exception("KPS NOT FOUND");
                C_ERRFLAG = dtWipInfo.Rows[0]["ERRFLAG"].ToString();
                C_LOCSTATION = dtWipInfo.Rows[0]["LOCSTATION"].ToString();
                C_WO = dtWipInfo.Rows[0]["WOID"].ToString();
                C_PARTNUMBER = dtWipInfo.Rows[0]["PARTNUMBER"].ToString();

                if (WO == C_WO)
                    throw new Exception("THE SAME TWO WO");
                if (C_ERRFLAG != "0") //--确认KPS是否在维修
                    throw new Exception("SN IN REPAIR");

                dtwoInfo = woinfo.GetWoInfo(WO, null, "BOMNUMBER").Tables[0];
                if (dtwoInfo.Rows.Count == 0)
                    throw new Exception("WO NOT FOUND");
                C_BOMNO = string.IsNullOrEmpty(dtwoInfo.Rows[0]["BOMNUMBER"].ToString()) ? "NA" : dtwoInfo.Rows[0]["BOMNUMBER"].ToString();

                if (C_BOMNO == "NA")
                    throw new Exception("BOM NOT FOUND");
                DataTable dtkpswoinfo = woinfo.GetWoInfo(C_WO, null, "OUTPUTGROUP").Tables[0];
                if (dtkpswoinfo.Rows.Count == 0)
                    throw new Exception("KPS WO NOT FOUND");
                C_LASTGROUP = dtkpswoinfo.Rows[0]["OUTPUTGROUP"].ToString();
                if (C_LOCSTATION == C_LASTGROUP || C_LOCSTATION == "SC_TEST" || C_LOCSTATION == "SC_SMT")
                    RES = "OK";
                else
                    throw new Exception("KPS ROUTE NOT END");

                N_COUNT = 0;
                dtBom = ClsBomKeyPart.GetBomNoParts(C_BOMNO).Tables[0];
                if (dtBom.Rows.Count == 0)
                    throw new Exception("KPS NOT IN BOM");
                foreach (DataRow dr in dtBom.Rows)
                {
                    if (C_PARTNUMBER == dr["KPNUMBER"].ToString())
                        N_COUNT = N_COUNT + 1;
                }
                if (N_COUNT == 0)
                    throw new Exception("KPS NOT IN BOM");
                else
                    RES = "OK";

            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log("PRO_CHECK_KPS_VALID " + RES);
            }
            return RES;
        }

        public System.Data.DataSet PRO_GETCARTONCONTENT(string V_CARTONID)
        {          
          
            DataSet ds = new DataSet();         

            int count = 0;
            string table = "SFCR.T_CARTON_INFO_HAD H,SFCR.T_CARTON_INFO_DTA D,SFCR.T_WIP_KEYPART_ONLINE P";
            string fieldlist = "P.ESN,P.WOID,P.SNTYPE,P.SNVAL";
            string filter = "H.CARTONID = D.CARTONID AND P.ESN = D.ESN  AND H.CARTONID = {0}";           
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("CARTONID", V_CARTONID);
            DataTable dt = TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ds.Tables.Add(DataTableCross(dt, dt.Rows.Count));
                return ds;
            }
            else
                return null;

        }

        public void PRO_GETEMPNO(string DATA, out string RES, out string REMP)
        {
            RES = string.Empty;
            REMP = string.Empty;
            try
            {
                string UserId = DATA.Split('-')[0];
                string UserPwd = DATA.Split('-')[1];
                RES = PRO_CHECKEMP(DATA);
                if (RES == "OK")
                    REMP = UserId;
            }
            catch
            {
                RES = "EMP Format Error";
            }
        }

        public string PRO_GETMAXCARTONID(string V_WOID, string V_LINEID, string V_PARTNUMBER)
        {
            string RES = string.Empty;
            string C_MAXCARTONID = string.Empty;
            string C_CARTONID = string.Empty;
            string C_LINEID = string.Empty;
            int C_CARTONNUM = 0;
            Dictionary<string,object> dic = null;
            int count=0;
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);

                //MySqlCommand cmd = new MySqlCommand();
                //cmd.CommandText = "  SELECT IFNULL(MAX(CARTONID), 'NA') FROM SFCR.T_CARTON_INFO_HAD WHERE WOID = @V_WOID";
                //cmd.Parameters.Add("V_WOID", MySqlDbType.VarChar).Value = V_WOID;
                dic = new Dictionary<string, object>();
                dic.Add("WOID", V_WOID);

                DataTable dt = dp.GetData("SFCR.T_CARTON_INFO_HAD", "IFNULL(MAX(CARTONID), 'NA')", dic, out count).Tables[0];
                C_MAXCARTONID = dt.Rows[0][0].ToString();
                if (C_MAXCARTONID != "NA")
                {
                    //cmd = new MySqlCommand();
                    //cmd.CommandText = " SELECT CARTONID, LINEID FROM SFCR.T_CARTON_INFO_HAD  WHERE CARTONID = @C_MAXCARTONID ";
                    //cmd.Parameters.Add("C_MAXCARTONID", MySqlDbType.VarChar).Value = C_MAXCARTONID;
                    dic = new Dictionary<string,object>();
                    dic.Add("CARTONID",C_MAXCARTONID);

                    DataTable dtctn = dp.GetData("SFCR.T_CARTON_INFO_HAD", "CARTONID, LINEID", dic, out count).Tables[0]; //BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];
                    C_CARTONID = dtctn.Rows[0]["CARTONID"].ToString();
                    C_LINEID = dtctn.Rows[0]["LINEID"].ToString();
                    RES = C_CARTONID;
                    C_CARTONNUM = Convert.ToInt32(C_CARTONID.Substring(C_CARTONID.Length - 4, 4));

                }
                else
                {
                    C_LINEID = V_LINEID;
                    RES = V_WOID + "C0001";
                    C_CARTONNUM = 0;

                }
                C_CARTONNUM = C_CARTONNUM + 1;
                RES = V_WOID + "C" + C_CARTONNUM.ToString().PadLeft(4, '0');

                //cmd = new MySqlCommand();
                //cmd.CommandText = @"INSERT INTO SFCR.T_CARTON_INFO_HAD (CARTONID, LINEID, WOID, CARTONNUMBER, NUM, REMARK, FLAG) VALUES (@RES, @C_LINEID, @V_WOID, @CTNNUM, 0, 'NA', '0')";
                //cmd.Parameters.AddRange(new MySqlParameter[]
                //{ 
                //    new MySqlParameter("RES", MySqlDbType.VarChar) { Value =RES },
                //    new MySqlParameter("C_LINEID", MySqlDbType.VarChar) { Value =C_LINEID },
                //    new MySqlParameter("V_WOID", MySqlDbType.VarChar) { Value =V_WOID },
                //    new MySqlParameter("CTNNUM", MySqlDbType.VarChar) { Value =C_CARTONNUM.ToString().PadLeft(4, '0') },
                            
                //});
                //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                dic = new Dictionary<string, object>();
                dic.Add("CARTONID",RES);
                 dic.Add("LINEID",C_LINEID);
                 dic.Add("WOID",V_WOID);
                 dic.Add("CARTONNUMBER",C_CARTONNUM.ToString().PadLeft(4, '0'));
                 dic.Add("NUM",0);
                 dic.Add("REMARK","NA");
                 dic.Add("FLAG","0");
                 dp.AddData("SFCR.T_CARTON_INFO_HAD",dic);
                 


                dic = new Dictionary<string, object>();
                dic.Add("WOID", V_WOID);
                dic.Add("PARTNUMBER", RES);
                dic.Add("LINE", C_LINEID);
                dic.Add("PACKTYPE", 1);
                dic.Add("TOTAL", 0);
                dic.Add("CLOSEFLAG", 0);
                dic.Add("COMPUTER", "NA");
                ClsPalletInfo.InsertPalletInfo(dic);          
            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log("PRO_GETMAXCARTONID " + RES);
            }

            return RES;


        }

        public string PRO_GETSEQPALLET(string FACID, string LINE)
        {
            string RES = string.Empty;
            string C_PALLET_PRE = string.Empty;               
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("FACID", FACID);
            DataTable dt = dp.GetData("SFCB.B_FAC_INFO", "FACCODE", mst, out count).Tables[0];

            if (dt.Rows.Count > 0)
            {
                C_PALLET_PRE = "PA" + (DateTime.Now.ToString("yy") + GetWeekOfYear());
                string table = "sfcr.t_pallet_info";
                string fieldlist = "max(palletnumber)";
                string filter = "packtype = 2 and palletnumber like {0}";                
                  mst = new Dictionary<string, object>();
                  mst.Add("palletnumber", C_PALLET_PRE+"%");
               DataTable dtpallet= TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count).Tables[0];
               if (!string.IsNullOrEmpty(dtpallet.Rows[0][0].ToString()))
               {
                   string _RES = dtpallet.Rows[0][0].ToString();
                   RES = C_PALLET_PRE + (Convert.ToInt32(_RES.Substring(_RES.Length - 5, 5)) + 1).ToString().PadLeft(5,'0');
               }
               else
               {
                   RES = C_PALLET_PRE + "00001";
               }

            }
            else
            {
                RES = "ERROR";
            }
            return RES;
        }

        public DateTime PRO_GETSERVERDATETIME()
        {
            return DateTime.Now;
        }
        public string PRO_INSERTROUTEATT(string V_ROUTEGROUPID, string V_ROUTEGROUPDESC, string V_XMLCONTENT)
        {

            string RES = "ERROR";
            string _StrErr = "System Error";
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = null;
            try
            {
                _StrErr = "GetAllRouteAtt Error";

                DataTable dt = ClsRoute.GetAllRouteAtt(V_ROUTEGROUPID).Tables[0];

                if (dt.Rows.Count > 0)
                {


                    mst = new Dictionary<string, object>();
                    mst.Add("ROUTGROUPID", V_ROUTEGROUPID);
                    dp.DeleteData(tx, "SFCB.B_ROUTE_CRAFT_PARAMETER", mst);

                    dp.DeleteData(tx, "SFCB.B_ROUTE_INFO", mst);

                    dp.DeleteData(tx, "SFCB.B_ROUTE_ATT", mst);

                }

                _StrErr = "Insert B_ROUTE_ATT Error";
                mst = new Dictionary<string, object>();
                mst.Add("ROUTGROUPID", V_ROUTEGROUPID);
                mst.Add("ROUTGROUPDESC", V_ROUTEGROUPDESC);
                mst.Add("ROUTGROUPXMLCONTENT", V_XMLCONTENT);
                dp.AddData(tx, "SFCB.B_ROUTE_ATT", mst);
                RES = "OK";
                tx.Commit();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                RES = _StrErr + "_" + ex.Message;
                Insert_Exception_Log("PRO_INSERTROUTEATT " + RES);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return RES;
        }
        public string PRO_INSERTROUTECRAFTPARAMERTER(string V_ROUTGROUPID, string V_CRAFTID, int V_CRAFTITEM, string V_CRAFTPARAMETERDES, string V_UPPERLIMIT, string V_LOWERLIMIT, string V_OTHER, string V_URL)
        {
            string RES = "ERROR";
            try
            {
               
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("ROUTGROUPID", V_ROUTGROUPID);
                mst.Add("CRAFTID", V_CRAFTID);
                mst.Add("CRAFTITEM", V_CRAFTITEM);
                DataSet ds = dp.GetData("SFCB.B_ROUTE_CRAFT_PARAMETER", "ROUTGROUPID,CRAFTID,CRAFTITEM,CRAFTPARAMETERDES,UPPERLIMIT,LOWERLIMIT,OTHER,URL", mst, out count);


                DataTable dt =ds.Tables[0] ;
                if (dt.Rows.Count == 0)
                {

                    mst = new Dictionary<string, object>();
                    mst.Add("ROUTGROUPID", V_ROUTGROUPID);
                    mst.Add("CRAFTID", V_CRAFTID);
                    mst.Add("CRAFTITEM", V_CRAFTITEM);
                    mst.Add("CRAFTPARAMETERDES", V_CRAFTPARAMETERDES);
                    mst.Add("UPPERLIMIT", V_UPPERLIMIT);
                    mst.Add("LOWERLIMIT", V_LOWERLIMIT);
                    mst.Add("OTHER", V_OTHER);
                    mst.Add("URL", V_URL);                    
                    dp.AddData("SFCB.B_ROUTE_CRAFT_PARAMETER",mst);

                }

                RES = "OK";

            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log("PRO_INSERTROUTECRAFTPARAMERTER " + RES);
            }

            return RES;
        }

        private string PRO_INSERT_KEYPARTS(IDictionary<string, object> Dic)
        {
            try
            {
                return PRO_INSERT_KEYPARTS(Dic["DATA"].ToString(), Dic["SN"].ToString(), Dic["MYGROUP"].ToString(), Dic["SECTION_NAME"].ToString(), Dic["STATION_NAME"].ToString(), Dic["EMP"].ToString(), Dic["LINE"].ToString(), Dic["WO"].ToString());
            }
            catch
            {
                return PRO_INSERT_KEYPARTS(Dic["DATA"].ToString(), Dic["SN"].ToString(), Dic["MYGROUP"].ToString(), "NA", Dic["MYGROUP"].ToString() , Dic["EMP"].ToString(), Dic["LINE"].ToString(), Dic["WO"].ToString());
            }
        }
        public string PRO_INSERT_KEYPARTS(string DATA, string SN, string MYGROUP,string SECTION_NAME,string STATION_NAME, string EMP, string LINE, string WO)
        {
      
            #region 成员变量
            string C_MODEL = string.Empty;
            string C_MODELDESC = string.Empty;
            string C_ROUTECODE = string.Empty;
            string C_ERRFLAG = string.Empty;
            string C_LOCGROUP = string.Empty;
            string C_NEXTSTATION = string.Empty;
            string C_WO = string.Empty;
            string C_RES = string.Empty;
            string C_EMP = string.Empty;
            string C_ENDGROUP = string.Empty;
            string RES = "OK";
            #endregion
            try
            {
                PRO_GETEMPNO(EMP, out C_RES, out C_EMP);
                if (C_RES != "OK")
                    throw new Exception(C_RES);
                DataTable dtWip = twip.Get_WIP_TRACKING("ESN", SN, "WOID,PARTNUMBER,PRODUCTNAME,LOCSTATION,NEXTSTATION,ERRFLAG,ROUTGROUPID").Tables[0];
                if (dtWip.Rows.Count == 0)
                    throw new Exception("ESN NOT FOUND");
                C_MODEL = dtWip.Rows[0]["PARTNUMBER"].ToString();
                C_MODELDESC = dtWip.Rows[0]["PRODUCTNAME"].ToString();
                C_ROUTECODE = dtWip.Rows[0]["ROUTGROUPID"].ToString();
                C_ERRFLAG = dtWip.Rows[0]["ERRFLAG"].ToString();
                C_LOCGROUP = dtWip.Rows[0]["LOCSTATION"].ToString();
                C_NEXTSTATION = dtWip.Rows[0]["NEXTSTATION"].ToString();
                C_WO = dtWip.Rows[0]["WOID"].ToString();

                DataTable dtwoInfo = woinfo.GetWoInfo(C_WO, null, "OUTPUTGROUP").Tables[0];
                if (dtwoInfo.Rows.Count == 0)
                    throw new Exception("WO NOT FOUND");
                C_ENDGROUP = dtwoInfo.Rows[0]["OUTPUTGROUP"].ToString();
                C_RES = PRO_CHECK_ROUTE(SN, MYGROUP);
                Dictionary<string, object> mst = null;
                if (C_RES == "OK")
                {
                
                     mst = new Dictionary<string,object>();
                     mst.Add("WOID", C_WO);
                     mst.Add("SNVAL",DATA);
                     mst.Add("ESN",SN);
                     DataTable dtKeyParts = GetData("SFCR.T_WIP_KEYPART_ONLINE", "ESN", mst);
                    if (dtKeyParts.Rows.Count == 0)
                    {                        
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("ESN", SN);
                        dic.Add("WOID", C_WO);
                        dic.Add("SNTYPE", "KPESN");
                        dic.Add("SNVAL", DATA);
                        dic.Add("STATION", MYGROUP);
                        dic.Add("KPNO", "NA");
                        C_RES = mWipTracking.InsertWipKeyPart(dic);
                        if (C_RES != "OK" && !string.IsNullOrEmpty(C_RES))
                            throw new Exception(C_RES);
                        PRO_STNREC(LINE, MYGROUP, C_MODEL, C_MODELDESC, C_WO, SN, 0, out C_RES);
                        RES = C_RES;
                    }
                    else
                    {
                        C_RES = "OK";
                    }

                }
                else
                {
                    RES = C_RES;
                }
                if (C_RES == "OK")
                {
                    PRO_UPDATEWIPSTATION_NEW(LINE, MYGROUP, SECTION_NAME, SN, C_EMP, 0, C_ROUTECODE, C_WO, "NA", STATION_NAME, C_MODEL, out C_RES);
                    if (C_RES != "OK")
                        throw new Exception(C_RES);
                    RES = "OK";
                }
                else
                {
                    RES = C_RES;
                }

                string table = "SFCR.T_WIP_TRACKING_ONLINE";
                string fieldlist = "STORENUMBER=CONCAT(STORENUMBER,'_R')";
                IDictionary<string, object> modFields = new Dictionary<string, object>();
                string filter = " ESN={0}";
                IDictionary<string, object> keyVals = new Dictionary<string, object>();
                //keyVals.Add("WOID", C_WO);
                keyVals.Add("ESN", DATA);
                TransactionManager.UpdateBatchData(table, fieldlist, modFields, filter, keyVals);

                table = "SFCR.Z_WHS_TRACKING";
                modFields = new Dictionary<string, object>();
                fieldlist = "LOTIN=CONCAT(LOTIN,'_R')";
                filter = "ESN = {0}";
                keyVals = new Dictionary<string, object>();           
                keyVals.Add("ESN", DATA);
                TransactionManager.UpdateBatchData(table, fieldlist, modFields, filter, keyVals);

            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log("PRO_INSERT_KEYPARTS " + RES);
            }
            return RES;
        }

        public void PRO_INSERT_SMT_KP_MASTER(string V_LINEID, string V_USERID, string V_PARTNUMBER, string V_MODELNAME, string V_BOMVER, string V_PCBSIDE, string V_RESERVE1, string V_RESERVE2, out string V_OUTMASTERID, out string V_ERROR)
        {
           
                V_OUTMASTERID = string.Empty;
                V_ERROR = string.Empty;
                //if (DB_Flag == 0)
                //{
                string C_MASTERID = string.Empty;
                DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
                DbTransaction tx = ProviderHelper.BeginTransaction(conn);
                try
                {                   
                    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);

                    Dictionary<string, object> mst = new Dictionary<string, object>();                

                    if (DB_Flag == 0)
                    {
                        string PRGNAME = "MASTERID";
                        mst = new Dictionary<string, object>();
                        mst.Add("name", PRGNAME);
                        DataTable dtSEQ = GetData("sfcb.sequence", "current_value", mst);
                        C_MASTERID = "S" + dtSEQ.Rows[0][0].ToString().PadLeft(6, '0');
                        mst.Add("current_value", Convert.ToInt32(dtSEQ.Rows[0][0].ToString()) + 1);
                        dp.UpdateData(tx, "sfcb.sequence", new string[] { "name" }, mst);
                    }
                    if (DB_Flag == 1)
                    {
                        int count = 0;
                        DataSet ds = dp.GetData("DUAL", "'S' || LPAD(TO_CHAR(SEQ_MASTERID.NEXTVAL), 6, '0')", null, out count);
                        C_MASTERID = ds.Tables[0].Rows[0][0].ToString();
                    }


                    mst = new Dictionary<string, object>();
                    mst.Add("MASTERID", C_MASTERID);
                    mst.Add("LINEID", V_LINEID);
                    mst.Add("USERID", V_USERID);
                    mst.Add("PARTNUMBER", V_PARTNUMBER);
                    mst.Add("MODELNAME", V_MODELNAME);
                    mst.Add("BOMVER", V_BOMVER);
                    mst.Add("PCBSIDE", V_PCBSIDE);
                    mst.Add("RESERVE1", V_RESERVE1);
                    mst.Add("RESERVE2", V_RESERVE2);
                    dp.AddData(tx, "SFCR.T_SMT_KP_MASTER", mst);
                    V_OUTMASTERID = C_MASTERID;
                    tx.Commit();
                    V_ERROR = "OK";

                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    V_ERROR = "INSERT SMTKPMASTER ERROR:" + ex.Message;
                    Insert_Exception_Log(V_ERROR);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            //}
            //if (DB_Flag == 1)
            //{
            //    try
            //    {
            //        IBaseProvider dp = new BaseProvider();
            //        IDictionary<string, object> parms = new Dictionary<string, object>();
            //        IDictionary<string, object> procedureOutRes = new Dictionary<string, object>();

            //        parms.Add("V_LINEID", V_LINEID);
            //        parms.Add("V_USERID", V_USERID);
            //        parms.Add("V_PARTNUMBER", V_PARTNUMBER);
            //        parms.Add("V_MODELNAME", V_MODELNAME);
            //        parms.Add("V_BOMVER", V_BOMVER);
            //        parms.Add("V_PCBSIDE", V_PCBSIDE);
            //        parms.Add("V_RESERVE1", V_RESERVE1);
            //        parms.Add("V_RESERVE2", V_RESERVE2);


            //        procedureOutRes.Add("V_OUTMASTERID", (object)200);
            //        procedureOutRes.Add("V_ERROR", (object)200);

            //        dp.StoreProcedureExec("PRO_INSERT_SMT_KP_MASTER", parms, procedureOutRes);
            //        V_ERROR = procedureOutRes["V_ERROR"].ToString();
            //        V_OUTMASTERID = procedureOutRes["V_OUTMASTERID"].ToString();
            //    }
            //    catch (Exception ex)
            //    {
            //        V_ERROR = "INSERT SMTKPMASTER ERROR:" + ex.Message;
            //    }
            //}

        }

        public void PRO_GETSNEND(out string C_MAXSN, out string C_WOID, out string C_ROID)
        {
            string C_MAC = "";
            C_ROID = string.Empty;
            C_WOID = "NO WO";
            C_MAXSN = "NO SN";
            int count = 0;
            string table = "SFCR.T_SN_RULE";
             string fieldlist =null;
             if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.MySql)
               fieldlist = "MAX(right(SNEND, 9)) AS MAC";
             if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.Oracle)
                 fieldlist = "MAX(SUBSTR(SNEND, 2)) AS MAC";

            string filter = "REVE IS NULL OR REVE='NA' ";
            DataTable dt = TransactionManager.GetData(table, fieldlist, filter, null, null, null, out count).Tables[0];
            if (dt.Rows.Count > 0)
            {
                table = "";
                fieldlist = "";
                filter = "";
                C_MAC = dt.Rows[0]["MAC"].ToString();

                table = "( SELECT ROWID AS NUM,WOID,SNEND FROM SFCR.T_SN_RULE WHERE SNEND=CONCAT('A','" + C_MAC + "') ";
                table += " UNION ALL ";
                table += " SELECT ROWID AS NUM,WOID,SNEND FROM SFCR.T_SN_RULE WHERE SNEND=CONCAT('B','" + C_MAC + "') ";
                table += " UNION ALL ";
                table += " SELECT ROWID AS NUM,WOID,SNEND FROM SFCR.T_SN_RULE WHERE SNEND=CONCAT('C','" + C_MAC + "') ";
                table += " UNION ALL ";
                table += " SELECT ROWID AS NUM,WOID,SNEND FROM SFCR.T_SN_RULE WHERE SNEND=CONCAT('D','" + C_MAC + "') ";
                table += " UNION ALL ";
                table += " SELECT ROWID AS NUM,WOID,SNEND FROM SFCR.T_SN_RULE WHERE SNEND=CONCAT('E','" + C_MAC + "')) A ";
                   if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.MySql)
                fieldlist = "A.NUM,A.WOID,LPAD(A.SNEND,10,'0') MAXSN ";
                  if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.Oracle)
                      fieldlist = " A.NUM,A.WOID,LPAD(A.SNEND,10,'0') MAXSN ";
                //filter = " A LIMIT 1";
                DataTable dtmy = TransactionManager.GetData(table, fieldlist, filter, null, null, null, out count).Tables[0];
                if (dtmy.Rows.Count > 0)
                {
                    C_ROID = dtmy.Rows[0]["NUM"].ToString();
                    C_WOID = dtmy.Rows[0]["WOID"].ToString();
                    C_MAXSN = dtmy.Rows[0]["MAXSN"].ToString();
                }
            }

        }
        /// <summary>
        /// C_WORKSECTION 工作时间段,C_CLASS 班别,C_DATE1 当前日期,C_DATE2 当前日期减一天
        /// </summary>
        /// <param name="C_WORKSECTION"></param>
        /// <param name="C_CLASS"></param>
        /// <param name="C_DAY"></param>
        /// <param name="C_DATE1"></param>
        /// <param name="C_DATE2"></param>
        /// <param name="ERROR"></param>
        public void PRO_GETWORKCLASS(out string C_WORKSECTION, out string C_CLASS, out string C_DAY, out string C_DATE1, out string C_DATE2, out string ERROR)
        {
            C_WORKSECTION = string.Empty;
            C_CLASS = string.Empty;
            C_DAY = string.Empty;
            C_DATE1 = string.Empty;
            C_DATE2 = string.Empty;
            ERROR = "OK";
            try
            {
                int count = 0;
                string table = "SFCB.B_WORK_CLASS";
                string fieldlist = "WORKSECTION, CLASS, DAY,date_format(NOW(),'%Y%m%d') AS WORKDATE1, date_format(date_sub(now(),interval 1 day),'%Y%m%d') AS WORKDATE2";
                string filter = "STARTTIME <= TIME_FORMAT(NOW(),'%H%i') AND ENDTIME > TIME_FORMAT(NOW(),'%H%i')";   
                //string filter = "STARTTIME <= TIME_FORMAT({0},'%H%i') AND ENDTIME > TIME_FORMAT({1},'%H%i')";
                //IDictionary<string, object> mst = new Dictionary<string, object>();
                //mst.Add("STARTTIME", DateTime.Now);
                //mst.Add("ENDTIME", DateTime.Now);
                DataTable dt = TransactionManager.GetData(table, fieldlist, filter, null, null, null, out count).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    C_WORKSECTION = dt.Rows[0]["WORKSECTION"].ToString();
                    C_CLASS = dt.Rows[0]["CLASS"].ToString();
                    C_DAY = dt.Rows[0]["DAY"].ToString();
                    C_DATE1 = dt.Rows[0]["WORKDATE1"].ToString();
                    C_DATE2 = dt.Rows[0]["WORKDATE2"].ToString();
                }
                else
                {
                    ERROR = "NO DATA";
                }
            }
            catch
            {
                ERROR = "WORK CLASS EXCEPTION";
            }
        }
        public void PRO_INSERTPALLETINFO(string WOID, string PALLETNUMBER, string LINE, string PARTNUMBER, string PACKTYPE, string TOTAL, string CLOSEFLAG, string COMPUTER, out string V_ERR)
        {       
            Dictionary<string, object> mst = null;
            try
            {
                
                mst = new Dictionary<string, object>();
                mst.Add("WOID", WOID);
                mst.Add("PALLETNUMBER", PALLETNUMBER);
                DataTable dt = GetData("SFCR.T_PALLET_INFO", "*", mst);
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                if (dt.Rows.Count > 0)
                {                   
                    mst = new Dictionary<string, object>();
                    mst.Add("LINE", LINE);
                    mst.Add("PACKTYPE", PACKTYPE);
                    mst.Add("TOTAL", TOTAL);
                    mst.Add("CLOSEFLAG", CLOSEFLAG);
                    mst.Add("COMPUTER", COMPUTER);
                    mst.Add("WOID", WOID);
                    mst.Add("PALLETNUMBER", PALLETNUMBER);
                    dp.UpdateData("SFCR.T_PALLET_INFO", new string[] { "WOID", "PALLETNUMBER" }, mst);
                }
                else
                {

                    mst = new Dictionary<string, object>();
                    mst.Add("WOID", WOID);
                    mst.Add("PALLETNUMBER", PALLETNUMBER);
                    mst.Add("LINE", LINE);
                    mst.Add("PARTNUMBER", PARTNUMBER);
                    mst.Add("PACKTYPE", PACKTYPE);
                    mst.Add("TOTAL", TOTAL);
                    mst.Add("CLOSEFLAG", CLOSEFLAG);
                    mst.Add("COMPUTER", COMPUTER);
                    dp.AddData("SFCR.T_PALLET_INFO", mst);                 
                }
                V_ERR = "OK";
            }
            catch (Exception ex)
            {
                V_ERR = "ERROR "+ex.Message;
                Insert_Exception_Log("PRO_INSERTPALLETINFO " + V_ERR);
            }
        }

        public void PRO_INSERTSNDETAIL(string SN)
        {
            try
            {

                DataTable dtWipInfo = twip.Get_WIP_TRACKING("ESN", SN).Tables[0];
                if (dtWipInfo.Rows.Count > 0)
                {
                    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                    IDictionary<string, object> mst = new Dictionary<string, object>();
                    mst.Add("ESN", dtWipInfo.Rows[0]["ESN"].ToString());
                    mst.Add("WOID", dtWipInfo.Rows[0]["WOID"].ToString());
                    mst.Add("PARTNUMBER", dtWipInfo.Rows[0]["PARTNUMBER"].ToString());
                    mst.Add("LOCSTATION", dtWipInfo.Rows[0]["LOCSTATION"].ToString());
                    mst.Add("STATIONNAME", dtWipInfo.Rows[0]["STATIONNAME"].ToString());
                    mst.Add("WIPSTATION", dtWipInfo.Rows[0]["WIPSTATION"].ToString());
                    mst.Add("NEXTSTATION", dtWipInfo.Rows[0]["NEXTSTATION"].ToString());
                    mst.Add("USERID", dtWipInfo.Rows[0]["USERID"].ToString());
                    mst.Add("RECDATE", dtWipInfo.Rows[0]["RECDATE"].ToString());
                    mst.Add("ERRFLAG", dtWipInfo.Rows[0]["ERRFLAG"].ToString());
                    mst.Add("SCRAPFLAG", dtWipInfo.Rows[0]["SCRAPFLAG"].ToString());
                    mst.Add("SN", dtWipInfo.Rows[0]["SN"].ToString());
                    mst.Add("MAC", dtWipInfo.Rows[0]["MAC"].ToString());
                    mst.Add("CARTONNUMBER", dtWipInfo.Rows[0]["CARTONNUMBER"].ToString());
                    mst.Add("TRAYNO", dtWipInfo.Rows[0]["TRAYNO"].ToString());
                    mst.Add("PALLETNUMBER", dtWipInfo.Rows[0]["PALLETNUMBER"].ToString());
                    mst.Add("MCARTONNUMBER", dtWipInfo.Rows[0]["MCARTONNUMBER"].ToString());
                    mst.Add("MPALLETNUMBER", dtWipInfo.Rows[0]["MPALLETNUMBER"].ToString());
                    mst.Add("LINE", dtWipInfo.Rows[0]["LINE"].ToString());
                    mst.Add("ROUTGROUPID", dtWipInfo.Rows[0]["ROUTGROUPID"].ToString());
                    mst.Add("STORENUMBER", dtWipInfo.Rows[0]["STORENUMBER"].ToString());
                    mst.Add("WEIGHTQTY", dtWipInfo.Rows[0]["WEIGHTQTY"].ToString());
                    mst.Add("QA_NO", dtWipInfo.Rows[0]["QA_NO"].ToString());
                    mst.Add("QA_RESULT", dtWipInfo.Rows[0]["QA_RESULT"].ToString());
                    mst.Add("TRACK_NO", dtWipInfo.Rows[0]["TRACK_NO"].ToString());
                    mst.Add("ATE_STATION_NO", dtWipInfo.Rows[0]["ATE_STATION_NO"].ToString());
                    mst.Add("TYPE", dtWipInfo.Rows[0]["TYPE"].ToString());
                    mst.Add("VERSIONCODE", dtWipInfo.Rows[0]["VERSIONCODE"].ToString());
                    mst.Add("SECTIONNAME", dtWipInfo.Rows[0]["SECTIONNAME"].ToString());
                    mst.Add("IN_LINE_TIME", dtWipInfo.Rows[0]["IN_LINE_TIME"].ToString());
                    mst.Add("PRODUCTNAME", dtWipInfo.Rows[0]["PRODUCTNAME"].ToString());
                    mst.Add("IMEI", dtWipInfo.Rows[0]["IMEI"].ToString());
                    mst.Add("BOMNUMBER", dtWipInfo.Rows[0]["BOMNUMBER"].ToString());
                    mst.Add("REWORKNO", dtWipInfo.Rows[0]["REWORKNO"].ToString());
                    dp.AddData("SFCR.T_WIP_DETAIL_A", mst);
                  
                }
            }
            catch (Exception ex)
            {
                Insert_Exception_Log("PRO_INSERTSNDETAIL " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void PRO_INSERTSTATIONRECOUNT(string WOID, string CRAFTID, string WORKDATE,string PARTNUMBER, string WORKSECTION, string CLASS, string CLASSDATE, string LINEID, int PASSQTY, int FAILQTY, int RPASSQTY, int RFAILQTY, string FLAG)
        {
            DataTable dt = tStaR.GET_STN_REC_2(WORKDATE, WORKSECTION, WOID, CRAFTID, LINEID).Tables[0];
 
            if (dt.Rows.Count > 0)
            {

                tStaR.update_station_recout(dt.Rows[0]["ROWID"].ToString(), WOID, CRAFTID, WORKDATE, WORKSECTION, LINEID, PASSQTY, FAILQTY, RPASSQTY, RFAILQTY, FLAG);
            }
            else
            {
                tStaR.insert_station_recount(WOID, CRAFTID, WORKDATE,PARTNUMBER, WORKSECTION, CLASS, CLASSDATE, LINEID, PASSQTY, FAILQTY, RPASSQTY, RFAILQTY, FLAG);
            }

        }
        public void PRO_INSERTSMTKPDETALTFORWO(string C_MASTERID, string C_WOID, string C_USERID, string C_STATIONNO, string C_KPNUMBER, string C_KPDESC, out string RES)
        {
            //DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            //DbTransaction tx = ProviderHelper.BeginTransaction(conn);
          

          //  MySqlCommand cmd = new MySqlCommand();
            int C_KPDISTINCT = 0;
            string C_REPLACEGROUP = "";
            int C_PRIORITYCLASS = 0;
            string temp_kpdsc = "";
            string C_LOCTION = "";
            Dictionary<string, object> mst = null;
            try
            {
               string sSQL  = @"SELECT IFNULL(S5.REPLACEGROUP,'NA') REPLACEGROUP, S5.PRIORITYCLASS, IFNULL(S5.KPDISTINCT, '') KPDISTINCT
                        FROM (SELECT S3.REPLACEGROUP, S3.PRIORITYCLASS, S3.KPDISTINCT FROM (SELECT S2.REPLACEGROUP,
                        COUNT(S2.KPDISTINCT) AS KPDISTINCT, COUNT(S2.PRIORITYCLASS) AS PRIORITYCLASS
                        FROM (SELECT REPLACEGROUP, KPDISTINCT, PRIORITYCLASS FROM SFCR.T_SMT_KP_DETALT TS
                        WHERE TS.MASTERID = @C_MASTERID AND TS.STATIONNO = @C_STATIONNO
                        UNION ALL
                        SELECT REPLACEGROUP, KPDISTINCT, PRIORITYCLASS FROM SFCR.T_SMT_KP_DETALT_FORWO TS1
                        WHERE TS1.WOID = @C_WOID AND TS1.STATIONNO = @C_STATIONNO) S2 GROUP BY S2.REPLACEGROUP) S3
                        ORDER BY LENGTH(S3.REPLACEGROUP) ASC) S5 LIMIT 1";
        
               Dictionary<string, string> dic = new Dictionary<string, string>();
               dic.Add("C_MASTERID", C_MASTERID);
               dic.Add("C_STATIONNO", C_STATIONNO);
               dic.Add("C_WOID", C_WOID);
               DataTable dt = BLL.BllMsSqllib.Instance.ExecuteDataSet(sSQL, dic, ProConfiguration.GetConfig().DatabaseConnect).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    temp_kpdsc = dt.Rows[0]["KPDISTINCT"].ToString();
                    C_PRIORITYCLASS = Convert.ToInt32(dt.Rows[0]["PRIORITYCLASS"].ToString());
                    C_REPLACEGROUP = dt.Rows[0]["REPLACEGROUP"].ToString();
                }
                if (string.IsNullOrEmpty(temp_kpdsc))
                {
                    C_KPDISTINCT = 1;
                    C_REPLACEGROUP = "";
                    C_PRIORITYCLASS = 0;
                }
                else
                {
                    C_KPDISTINCT = 0;
                    sSQL = @"SELECT B1.LOCTION FROM (SELECT LOCTION FROM SFCR.T_SMT_KP_DETALT TS1
                        WHERE TS1.MASTERID = @C_MASTERID AND TS1.STATIONNO = @C_STATIONNO
                        UNION ALL
                        SELECT LOCTION FROM SFCR.T_SMT_KP_DETALT_FORWO TS2 WHERE TS2.WOID = @C_WOID AND TS2.STATIONNO = @C_STATIONNO
                        ORDER BY LOCTION DESC) B1 LIMIT 1";

                    DataTable dt_loc = BLL.BllMsSqllib.Instance.ExecuteDataSet(sSQL, dic, ProConfiguration.GetConfig().DatabaseConnect).Tables[0];
                    if (dt_loc.Rows.Count > 0)
                    {
                        C_LOCTION = dt_loc.Rows[0]["LOCTION"].ToString();
                    }
                    if (C_REPLACEGROUP == "NA")
                    {                       
                        mst = new Dictionary<string, object>();
                        mst.Add("MASTERID", C_MASTERID);
                        DataTable dt_replace = GetData("SFCR.T_SMT_KP_DETALT", "MAX(REPLACEGROUP)", mst); //BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];
                        C_REPLACEGROUP = CONVERTHEX10TO36(CONVERTHEX36TO10(dt_replace.Rows[0][0].ToString()));                     
                        mst = new Dictionary<string,object>();
                        mst.Add("REPLACEGROUP",C_REPLACEGROUP);
                        mst.Add("WOID",C_WOID);
                        mst.Add("STATIONNO",C_STATIONNO);
                        mst.Add("MASTERID",C_MASTERID);
                        UpdateData("SFCR.T_SMT_KP_DETALT_FORWO", new string[] { "WOID", "STATIONNO", "MASTERID" }, mst);

                    }
                }
             
                mst = new Dictionary<string, object>();
                mst.Add("REPLACEGROUP", C_REPLACEGROUP);
                mst.Add("MASTERID", C_MASTERID);                
                mst.Add("STATIONNO", C_STATIONNO);

                UpdateData("SFCR.T_SMT_KP_DETALT", new string[] { "MASTERID", "STATIONNO" }, mst);

                mst = new Dictionary<string, object>();
                mst.Add("USERID",C_USERID);
                mst.Add("WOID",C_WOID);
                mst.Add("STATIONNO",C_STATIONNO);
                mst.Add("MASTERID",C_MASTERID);
                mst.Add("KPNUMBER",C_KPNUMBER);
                mst.Add("KPDESC",C_KPDESC);
                mst.Add("KPDISTINCT",C_KPDISTINCT);
                mst.Add("REPLACEGROUP",C_REPLACEGROUP);
                mst.Add("PRIORITYCLASS",C_PRIORITYCLASS);
                mst.Add("LOCTION", C_LOCTION);               
                InsertData("SFCR.T_SMT_KP_DETALT_FORWO",mst);
                RES = "OK";
            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log("PRO_INSERTSMTKPDETALTFORWO " + RES);
            }
        }

        public string PRO_SN_INPUT_MOBILE(string DATA, string MYGROUP, string LINE, string WO, string EMP)
        {
            string C_WO = string.Empty;
            string RES = string.Empty;
            string C_DATA = DATA;
            string C_USERID = string.Empty;
            string C_STARTGROUP = string.Empty;
            int C_TARGETQTY = 0;
            int C_INPUTQTY = 0;
            string C_MODEL = string.Empty;
            string C_ROUTE = string.Empty;
            string C_FLAG = string.Empty;
            string C_PRODUCTNAME = string.Empty;
            Dictionary<string, object> mst = null;

            try
            {
                DataTable dtWip = twip.Get_WIP_TRACKING("ESN", DATA, "WOID").Tables[0];
                if (dtWip.Rows.Count == 0)
                {

                    int count = 0;
                    string table = "SFCR.T_SN_RULE";
                    string fieldlist = "WOID";
                    string filter = "({0} BETWEEN SNSTART AND SNEND) AND LENGTH({0})=LENGTH(SNSTART)";
                    mst = new Dictionary<string, object>();
                    mst.Add("SNSTART", C_DATA);
                    DataTable dtsnRule = TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count).Tables[0];
                    if (dtsnRule.Rows.Count == 0)
                        throw new Exception("NO SN RANGE");
                    C_WO = dtsnRule.Rows[0]["WOID"].ToString();
                    if (C_WO != WO)
                        throw new Exception("WO DIFFERENT");
                    C_USERID = EMP.Split('-')[0];
                    DataTable dtwoInfo = woinfo.GetWoInfo(C_WO, null, "INPUTGROUP,QTY,INPUTQTY,PARTNUMBER,ROUTGROUPID,WOSTATE,PRODUCTNAME").Tables[0];
                    if (dtwoInfo.Rows.Count == 0)
                        throw new Exception("WO NOT FOUND");
                    C_STARTGROUP = dtwoInfo.Rows[0]["INPUTGROUP"].ToString();
                    C_TARGETQTY = Convert.ToInt32(dtwoInfo.Rows[0]["QTY"].ToString());
                    C_INPUTQTY = Convert.ToInt32(dtwoInfo.Rows[0]["INPUTQTY"].ToString());
                    C_MODEL = dtwoInfo.Rows[0]["PARTNUMBER"].ToString();
                    C_ROUTE = dtwoInfo.Rows[0]["ROUTGROUPID"].ToString();
                    C_FLAG = dtwoInfo.Rows[0]["WOSTATE"].ToString();
                    C_PRODUCTNAME = dtwoInfo.Rows[0]["PRODUCTNAME"].ToString();
                    switch (Convert.ToInt32(C_FLAG))
                    {
                        case 0:
                            RES = "Waiting Relaese";
                            break;
                        case 1:
                            RES = "OK";
                            break;
                        case 2:
                            RES = "OK";
                            break;
                        case 3:
                            RES = "WO IS CLOSED ";
                            break;
                        case 4:
                            RES = "WO HOLD";
                            break;
                        default:
                            RES = "WO STATU ERROR";
                            break;

                    }
                    if (RES != "OK")
                        throw new Exception(RES);
                    if (MYGROUP == C_STARTGROUP)
                    {
                        if (C_INPUTQTY < C_TARGETQTY)
                        {
                            mst = new Dictionary<string, object>();
                            mst.Add("ESN", DATA);
                            mst.Add("WOID", WO);
                            mst.Add("PARTNUMBER", C_MODEL);
                            mst.Add("PRODUCTNAME", C_PRODUCTNAME);
                            mst.Add("LOCSTATION", MYGROUP);
                            mst.Add("WIPSTATION", MYGROUP);
                            mst.Add("NEXTSTATION", C_STARTGROUP);
                            mst.Add("USERID", C_USERID);
                            mst.Add("LINE", LINE);
                            mst.Add("ROUTGROUPID", C_ROUTE);
                            InsertData("SFCR.T_WIP_TRACKING_ONLINE", mst);
                            RES = "OK";
                        }
                        else
                        {
                            throw new Exception("INPUTQTY > TARGETQTY");
                        }
                    }
                    else
                    {
                        RES = "GO TO " + C_STARTGROUP;
                    }
                }
                else
                {
                    C_WO = dtWip.Rows[0]["WOID"].ToString();
                    if (C_WO != WO)
                    {
                        RES = "SN ERROR";
                    }
                    else
                    {
                        RES = PRO_CHECK_ROUTE(DATA, MYGROUP);
                    }

                }
            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log("PRO_SN_INPUT_MOBILE " + RES);
            }
            finally
            {
               // Insert_DB_Log(string.Format("PRO_SN_INPUT_MOBILE DATA:{0} {1}" ,DATA, RES));
            }
            return RES;
        }
        private string PRO_SN_INPUT_WIPFIRST(IDictionary<string, object> Dic)
        {
            return PRO_SN_INPUT_WIPFIRST(Dic["DATA"].ToString(), Dic["MYGROUP"].ToString(), Dic["LINE"].ToString(), Dic["WO"].ToString(), Dic["EMP"].ToString());
        }
        public string PRO_SN_INPUT_WIPFIRST(string DATA, string MYGROUP, string LINE, string WO, string EMP)
        {
            string C_DATA = DATA;
            string RES = string.Empty;
            string C_EMP = string.Empty;
            string C_WO = string.Empty;
            string C_STARTGROUP = string.Empty;
            int C_TARGETQTY = 0;
            int C_INPUTQTY = 0;
            string C_MODEL = string.Empty;
            string C_ROUTE = string.Empty;
            string C_FLAG = string.Empty;
            string C_PRODUCTNAME = string.Empty;
            Dictionary<string, object> mst = null;
            try
            {
                DataTable dtWip = twip.Get_WIP_TRACKING("ESN", DATA, "WOID").Tables[0];
                if (dtWip.Rows.Count == 0)
                {
                    if (DATA.Length > 5)
                    {
                        PRO_GETEMPNO(EMP, out RES, out C_EMP);
                        if (RES != "OK")
                            throw new Exception(RES);
                        if (!System.Text.RegularExpressions.Regex.IsMatch(DATA, @"^[A-Z0-9]+$"))
                            throw new Exception("SN FORMAT ERR");                       
                        int count = 0;
                        string table = "SFCR.T_SN_RULE";
                        string fieldlist = "WOID";
                        string filter = "({0} BETWEEN SNSTART AND SNEND) AND LENGTH(SNSTART)={1}";
                        mst = new Dictionary<string, object>();
                        mst.Add("SNSTART", C_DATA);
                        mst.Add("LENSNSTART", C_DATA.Length);
                        DataTable dtsnRule = TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count).Tables[0];
                        if (dtsnRule.Rows.Count == 0)
                            throw new Exception("NO SN RANGE");
                        C_WO = dtsnRule.Rows[0]["WOID"].ToString();
                        if (C_WO != WO)
                            throw new Exception("WO DIFFERENT");

                        DataTable dtwoInfo = woinfo.GetWoInfo(C_WO, null, "INPUTGROUP,QTY,INPUTQTY,PARTNUMBER,ROUTGROUPID,WOSTATE,PRODUCTNAME").Tables[0];
                        if (dtwoInfo.Rows.Count == 0)
                            throw new Exception("WO NOT FOUND");
                        C_STARTGROUP = dtwoInfo.Rows[0]["INPUTGROUP"].ToString();
                        C_TARGETQTY = Convert.ToInt32(dtwoInfo.Rows[0]["QTY"].ToString());
                        C_INPUTQTY = Convert.ToInt32(dtwoInfo.Rows[0]["INPUTQTY"].ToString());
                        C_MODEL = dtwoInfo.Rows[0]["PARTNUMBER"].ToString();
                        C_ROUTE = dtwoInfo.Rows[0]["ROUTGROUPID"].ToString();
                        C_FLAG = dtwoInfo.Rows[0]["WOSTATE"].ToString();
                        C_PRODUCTNAME = dtwoInfo.Rows[0]["PRODUCTNAME"].ToString();

                        RES = "WO STATUS ERROR";
                        switch (Convert.ToInt32(C_FLAG))
                        {
                            case 0:
                                RES = "Waiting Relaese";
                                break;
                            case 1:
                                RES = "OK";
                                break;
                            case 2:
                                RES = "OK";
                                break;
                            case 3:
                                RES = "WO IS CLOSED ";
                                break;
                            case 4:
                                RES = "WO HOLD";
                                break;
                            default:
                                RES = "WO STATUS ERROR";
                                break;
                        }
                        if (RES != "OK")
                            throw new Exception(RES);

                        if (MYGROUP == C_STARTGROUP)
                        {
                            if (C_INPUTQTY < C_TARGETQTY)
                            {
                                if (Convert.ToInt32(twip.Get_WIP_TRACKING("WOID", WO, "COUNT(ESN)").Tables[0].Rows[0][0].ToString()) >= C_TARGETQTY)
                                    throw new Exception("WIP QTY >=Target QTY");

                                mst = new Dictionary<string, object>();
                                mst.Add("ESN", DATA);
                                mst.Add("WOID", WO);
                                mst.Add("PARTNUMBER", C_MODEL);
                                mst.Add("PRODUCTNAME", C_PRODUCTNAME);
                                mst.Add("LOCSTATION", MYGROUP);
                                mst.Add("WIPSTATION", MYGROUP);
                                mst.Add("NEXTSTATION", C_STARTGROUP);
                                mst.Add("USERID", C_EMP);
                                mst.Add("SN", "NA");
                                mst.Add("MAC", "NA");
                                mst.Add("IMEI", "NA");
                                mst.Add("LINE", LINE);
                                mst.Add("ROUTGROUPID", C_ROUTE);
                                InsertData("SFCR.T_WIP_TRACKING_ONLINE", mst);

                                UPDATE_WO_FIRST_PCS_TIME(WO);
                                RES = "OK";
                            }
                            else
                            {
                                throw new Exception("INPUTQTY > TARGETQTY");
                            }
                        }
                        else
                        {
                            RES = "GO TO " + C_STARTGROUP + "-" + MYGROUP;
                        }
                    }
                    else
                    {
                        RES = "SN LENGTH ERROR";
                    }
                }
                else
                {
                    if (WO == dtWip.Rows[0]["WOID"].ToString())                                          
                       RES = "OK";                    
                    else
                        RES = "WO Different";
                }
            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log("PRO_SN_INPUT_WIPFIRST " + RES);
            }
            finally
            {
               // Insert_DB_Log(string.Format("PRO_SN_INPUT_WIPFIRST WO:{0} DATA:{1} {2}", WO, DATA, RES));
            }
            return RES;

        }

        public string PRO_SN_INPUT_WIPFIRST_NEW(string DATA, string MYGROUP, string LINE, string WO, string EMP, out string PARTNUMBER, out string MODELDESC, out string ROUTEGROUP, out string ENDGROUP)
        {
            string C_DATA = DATA;
            string C_LINE = LINE;
            string C_WO = string.Empty;
            string C_REVE = string.Empty;
            string C_Version_Code = string.Empty;
            string RES = string.Empty;
            string C_STARTGROUP = string.Empty;
            string C_WOTYPE = string.Empty;
            int C_TARGETQTY = 0;
            int C_INPUTQTY = 0;
            string C_MODEL = string.Empty;
            string C_ROUTE = string.Empty;
            string C_FLAG = string.Empty;
            string C_PRODUCTNAME = string.Empty;
            PARTNUMBER = string.Empty;
            ROUTEGROUP = string.Empty;
            MODELDESC = string.Empty;
            ENDGROUP = string.Empty;
          //  MySqlCommand cmd = null;
            Dictionary<string, object> mst = null;
            try
            {
                if (C_DATA.Length > 5)
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(DATA, @"^[A-Z0-9]+$"))
                        throw new Exception("SN FORMAT ERR");
                    int count = 0;
                    string table = "SFCR.T_SN_RULE";
                    string fieldlist = "WOID,REVE";
                    string filter = "WOID = {0} AND ({1} BETWEEN SNSTART AND SNEND) AND LENGTH({1})=LENGTH(SNSTART)";
                    mst = new Dictionary<string, object>();
                    mst.Add("WOID", WO);
                    mst.Add("SNSTART", C_DATA);
                    DataTable dtsnRule = TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count).Tables[0];
                    if (dtsnRule.Rows.Count == 0)
                        throw new Exception("NO SN RANGE");
                    C_WO = dtsnRule.Rows[0]["WOID"].ToString();
                    C_REVE = dtsnRule.Rows[0]["REVE"].ToString();
                    if (C_WO != WO)
                        throw new Exception("WO DIFFERENT");

                    DataTable dtwoInfo = woinfo.GetWoInfo(C_WO, null, "INPUTGROUP,OUTPUTGROUP,WOTYPE,PVER,QTY,INPUTQTY,PARTNUMBER,ROUTGROUPID,WOSTATE,PRODUCTNAME").Tables[0];
                    if (dtwoInfo.Rows.Count == 0)
                        throw new Exception("WO NOT FOUND");
                    C_STARTGROUP = dtwoInfo.Rows[0]["INPUTGROUP"].ToString();
                    ENDGROUP = dtwoInfo.Rows[0]["OUTPUTGROUP"].ToString();
                    C_WOTYPE = dtwoInfo.Rows[0]["WOTYPE"].ToString();
                    C_TARGETQTY = Convert.ToInt32(dtwoInfo.Rows[0]["QTY"].ToString());
                    C_INPUTQTY = Convert.ToInt32(dtwoInfo.Rows[0]["INPUTQTY"].ToString());
                    C_MODEL = dtwoInfo.Rows[0]["PARTNUMBER"].ToString();
                    C_ROUTE = dtwoInfo.Rows[0]["ROUTGROUPID"].ToString();
                    C_FLAG = dtwoInfo.Rows[0]["WOSTATE"].ToString();
                    C_PRODUCTNAME = dtwoInfo.Rows[0]["PRODUCTNAME"].ToString();
                    C_Version_Code = dtwoInfo.Rows[0]["PVER"].ToString();

                    switch (Convert.ToInt32(C_FLAG))
                    {
                        case 0:
                            RES = "Waiting Relaese";
                            break;
                        case 1:
                            RES = "OK";
                            break;
                        case 2:
                            RES = "OK";
                            break;
                        case 3:
                            RES = "WO IS CLOSED ";
                            break;
                        case 4:
                            RES = "WO HOLD";
                            break;
                        default:
                            RES = "WO STATUS ERROR";
                            break;
                    }
                    if (RES != "OK")
                        throw new Exception(RES);
                    if (MYGROUP == C_STARTGROUP)
                    {
                        if (C_INPUTQTY < C_TARGETQTY)
                        {
                            mst = new Dictionary<string, object>();
                            mst.Add("ESN", DATA);
                            mst.Add("WOID", WO);
                            mst.Add("PARTNUMBER", C_MODEL);
                            mst.Add("PRODUCTNAME", C_PRODUCTNAME);
                            mst.Add("VERSIONCODE", C_Version_Code);
                            mst.Add("LOCSTATION", MYGROUP);
                            mst.Add("WIPSTATION", MYGROUP);
                            mst.Add("NEXTSTATION", C_STARTGROUP);
                            mst.Add("USERID", EMP);
                            mst.Add("SN", "NA");
                            mst.Add("MAC", "NA");
                            mst.Add("IMEI", "NA");
                            mst.Add("LINE", LINE);
                            mst.Add("ROUTGROUPID", C_ROUTE);
                            InsertData("SFCR.T_WIP_TRACKING_ONLINE", mst);
                            PARTNUMBER = C_MODEL;
                            ROUTEGROUP = C_ROUTE;
                            MODELDESC = C_PRODUCTNAME;

                            //if (C_WOTYPE == "Subcontract" && C_REVE == "1")
                            //{
                            //    mst = new Dictionary<string, object>();
                            //    mst.Add("ESN", DATA);
                            //    mst.Add("WOID", WO);
                            //    mst.Add("SNTYPE", "MAC");
                            //    mst.Add("SNVAL", DATA);
                            //    mst.Add("STATION", MYGROUP);
                            //    mst.Add("KPNO", C_WOTYPE);
                            //    InsertData("SFCR.T_WIP_KEYPART_ONLINE", mst);
                            //}
                            UPDATE_WO_FIRST_PCS_TIME(WO);
                            RES = "OK";
                        }
                        else
                        {
                            RES = "INPUTQTY > TARGETQTY";
                        }
                    }
                    else
                    {
                        RES = "GO TO " + C_STARTGROUP + "-" + MYGROUP; ;
                    }
                }
                else
                {
                    RES = "SN LENGTH ERROR";
                }
            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log("PRO_SN_INPUT_WIPFIRST_NEW " + RES);
            }
            finally
            {
                // Insert_DB_Log(string.Format("PRO_SN_INPUT_WIPFIRST_NEW DATA:{0} {1}",DATA ,RES));
            }
            return RES;
        }

        private string PRO_TEST_INPUT_ALL(IDictionary<string,object> Dic)
        {
            string C_RES = string.Empty;
            try
            {
                PRO_TEST_INPUT_ALL(Dic["DATA"].ToString(), Dic["LINE"].ToString(), Dic["MYGROUP"].ToString(), Dic["SECTION_NAME"].ToString(), Dic["STATION_NAME"].ToString(), Dic["WO"].ToString(), Dic["EMP"].ToString(), out C_RES);               
            }
            catch
            {
                PRO_TEST_INPUT_ALL(Dic["DATA"].ToString(), Dic["LINE"].ToString(), Dic["MYGROUP"].ToString(), "NA", Dic["MYGROUP"].ToString(), Dic["WO"].ToString(), Dic["EMP"].ToString(), out C_RES); 
            }
        
            return C_RES;
        }
     
        public void PRO_TEST_INPUT_ALL(string DATA, string LINE, string MYGROUP,string SECTION_NAME,string STATION_NAME, string WO, string EMP, out string RES)
        {
         
            Dictionary<string,object> mst = null;
            string C_EMP = string.Empty;
            try
            {
                PRO_GETEMPNO(EMP, out RES, out C_EMP);
                if (RES != "OK")
                    throw new Exception(RES);
                RES = "SN NOT FOUND";
                DataTable dt_A = twip.Get_WIP_TRACKING("ESN", DATA, "ESN,WOID,PARTNUMBER,LOCSTATION,ERRFLAG,WIPSTATION,SN,MAC,IMEI").Tables[0];
                if (dt_A.Rows.Count > 0)
                {
                    if (dt_A.Rows[0]["WIPSTATION"].ToString() == "1008")
                        throw new Exception("INPUT PRG ERR");

                    if (dt_A.Rows[0]["WOID"].ToString() == WO)
                        throw new Exception("THIS SN HAS INPUT");

                    if (dt_A.Rows[0]["ERRFLAG"].ToString() != "0")
                        throw new Exception("SN IN REPAIR");


                    if (dt_A.Rows[0]["WOID"].ToString() != "6666666")
                    {
                        RES = "OLD WO OR LAST GROUP NOT FOUND";
                        DataTable dt_KPS_WO = woinfo.GetWoInfo(dt_A.Rows[0]["WOID"].ToString(), null, "OUTPUTGROUP").Tables[0];
                        if (dt_KPS_WO.Rows.Count > 0)
                        {
                            if (dt_A.Rows[0]["LOCSTATION"].ToString() != dt_KPS_WO.Rows[0]["OUTPUTGROUP"].ToString())
                                throw new Exception("KPS ROUTE NOT END");
                        }
                        else
                        {
                            throw new Exception(RES);
                        }
                    }

                    DataTable dt_B = woinfo.GetWoInfo(WO, null, "PARTNUMBER,WOSTATE,BOMNUMBER,INPUTGROUP,ROUTGROUPID,QTY,INPUTQTY,PRODUCTNAME").Tables[0];
                    if (dt_B.Rows.Count < 1)
                        throw new Exception("WO NOT FOUND");

                    if (dt_B.Rows[0]["PARTNUMBER"].ToString() != "NA" && !string.IsNullOrEmpty(dt_B.Rows[0]["PARTNUMBER"].ToString()))
                    {
                        if (Convert.ToInt32(dt_B.Rows[0]["INPUTQTY"].ToString()) >= Convert.ToInt32(dt_B.Rows[0]["QTY"].ToString()))
                            throw new Exception("INPUT_QTY > OUTPUT_QTY");


                        if (MYGROUP != dt_B.Rows[0]["INPUTGROUP"].ToString())
                            throw new Exception("GO TO  " + dt_B.Rows[0]["INPUTGROUP"].ToString());

                        switch (Convert.ToInt32(dt_B.Rows[0]["WOSTATE"].ToString()))
                        {
                            case 0:
                                RES = "Waiting Relaese";
                                break;
                            case 1:
                                RES = "OK";
                                break;
                            case 2:
                                RES = "OK";
                                break;
                            case 3:
                                RES = "WO IS CLOSED ";
                                break;
                            case 4:
                                RES = "WO HOLD";
                                break;
                            default:
                                RES = "WO STATUS ERROR";
                                break;
                        }
                        if (RES != "OK")
                            throw new Exception(RES);

                        if (dt_B.Rows[0]["BOMNUMBER"].ToString() == "NA" || string.IsNullOrEmpty(dt_B.Rows[0]["BOMNUMBER"].ToString()))
                            throw new Exception("BOM NOT FOUND");

                        if (dt_B.Rows[0]["INPUTGROUP"].ToString() == "NA" || string.IsNullOrEmpty(dt_B.Rows[0]["INPUTGROUP"].ToString()))
                            throw new Exception("INPUT GROUP IS NULL");

                        RES = "KPS NOT FOUND";
                        mst = new Dictionary<string, object>();
                        mst.Add("BOMNUMBER", dt_B.Rows[0]["BOMNUMBER"].ToString());
                        mst.Add("KPNUMBER", dt_A.Rows[0]["PARTNUMBER"].ToString());
                        DataTable dt_C = GetData("SFCB.B_BOM_KEYPART", "COUNT(KPNUMBER) D_COUNT", mst); //BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];
                        if (Convert.ToInt32(dt_C.Rows[0]["D_COUNT"].ToString()) < 1)
                            throw new Exception("KPS NOT IN BOM");

                        string table = "SFCR.T_WIP_TRACKING_ONLINE";
                        string fieldlist = "WOID = {0},PARTNUMBER ={1},PRODUCTNAME ={2},LOCSTATION= {3},NEXTSTATION = {4},CARTONNUMBER={5},MCARTONNUMBER={6},ROUTGROUPID = {7},STORENUMBER={8}, IN_LINE_TIME =NOW()";
                        string filter = "WOID = {0} AND ESN = {1}";
                        IDictionary<string, object> modFields = new Dictionary<string, object>();
                        modFields.Add("WOID", WO);
                        modFields.Add("PARTNUMBER", dt_B.Rows[0]["PARTNUMBER"].ToString());
                        modFields.Add("PRODUCTNAME", dt_B.Rows[0]["PRODUCTNAME"].ToString());
                        modFields.Add("LOCSTATION", dt_B.Rows[0]["INPUTGROUP"].ToString());
                        modFields.Add("NEXTSTATION", dt_B.Rows[0]["INPUTGROUP"].ToString());
                        modFields.Add("CARTONNUMBER", "NA");
                        modFields.Add("MCARTONNUMBER", "NA");
                        modFields.Add("ROUTGROUPID", dt_B.Rows[0]["ROUTGROUPID"].ToString());
                        modFields.Add("STORENUMBER", "NA");
                        IDictionary<string, object> keyVals = new Dictionary<string, object>();
                        keyVals.Add("WOID1", dt_A.Rows[0]["WOID"].ToString());
                        keyVals.Add("ESN", DATA);
                        TransactionManager.UpdateBatchData(table, fieldlist, modFields, filter, keyVals);

                        mst = new Dictionary<string, object>();
                        mst.Add("WOID", WO);
                        mst.Add("ESN", DATA);
                        UpdateData("SFCR.T_WIP_KEYPART_ONLINE", new string[] { "ESN" }, mst);

                        UPDATE_WO_FIRST_PCS_TIME(WO);

                        PRO_STNREC(LINE, MYGROUP, dt_B.Rows[0]["PARTNUMBER"].ToString(), dt_B.Rows[0]["PRODUCTNAME"].ToString(), WO, DATA, 0, out RES);
                        if (RES == "OK")
                        {
                            PRO_UPDATEWIPSTATION_NEW(LINE, MYGROUP, SECTION_NAME, DATA, C_EMP, 0, dt_B.Rows[0]["ROUTGROUPID"].ToString(), WO, "NA", STATION_NAME, dt_B.Rows[0]["PARTNUMBER"].ToString(), out RES);
                        }
                        else
                        {
                            throw new Exception(RES);
                        }
                    }
                    else
                    {
                        RES = "WO PARTNUMBER IS NULL";
                    }
                }
                else
                {
                    throw new Exception(RES);
                }

            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log("PRO_TEST_INPUT_ALL " + RES);
            }

            //Insert_DB_Log(string.Format("PRO_TEST_INPUT_ALL DATA:{0}" ,DATA, RES));
        }
        public void PRO_UPDATEWIPSTATION(string LINE, string MYGROUP,string SECTION_NAME, string SN,string WOID, string EMP, string FLAG, string STATION_NAME, out string RES)
        {

            myWatch.Restart();
            #region 定义参数
            string C_ROUTEGROUPID = string.Empty;
            string NEXTSTATION = string.Empty;
            string C_WOID = string.Empty;
            string STR = string.Empty;
           // string SERROR = "0";
            string C_OUTPUTGROUP = string.Empty;
            string C_PARTNUMBER = string.Empty;
            string C_CARTONNUMBER = string.Empty;
            string C_TRAYNO = string.Empty;
            string C_PALLETNUMBER = string.Empty;
            string C_SN = string.Empty;
            string C_ESN = SN;
            string C_MAC = string.Empty;
            string C_MCARTONNUMBER = string.Empty;
            string C_MPALLETNUMBER = string.Empty;
            string C_STORENUMBER = string.Empty;
            string C_LINE = LINE;
            int C_ENDCOUNT = 0;
            #endregion
            RES = "ESN Not Found";
            try
            {
                C_WOID = WOID;

                Dictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("LOCSTATION", MYGROUP);
                mst.Add("LINE", C_LINE);
                mst.Add("STATIONNAME", STATION_NAME);
                mst.Add("NEXTSTATION", "NA");
                mst.Add("ERRFLAG", FLAG);
                mst.Add("RECDATE", System.DateTime.Now);
                mst.Add("SECTIONNAME", SECTION_NAME);
                mst.Add("ESN", C_ESN);
                mst.Add("USERID", EMP);
                UpdateData("SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);

                RES = "WIP NOT DATA";
                DataTable dt = twip.Get_WIP_TRACKING("ESN", C_ESN, "WOID,PARTNUMBER,CARTONNUMBER,TRAYNO,PALLETNUMBER,SN,MAC,MCARTONNUMBER,MPALLETNUMBER,ROUTGROUPID,STORENUMBER").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    C_ROUTEGROUPID = dt.Rows[0]["ROUTGROUPID"].ToString();
                    C_WOID = dt.Rows[0]["WOID"].ToString();
                    C_PARTNUMBER = dt.Rows[0]["PARTNUMBER"].ToString();
                    C_CARTONNUMBER = dt.Rows[0]["CARTONNUMBER"].ToString();
                    C_TRAYNO = dt.Rows[0]["TRAYNO"].ToString();
                    C_PALLETNUMBER = dt.Rows[0]["PALLETNUMBER"].ToString();
                    C_SN = dt.Rows[0]["SN"].ToString();
                    C_MAC = dt.Rows[0]["MAC"].ToString();
                    C_MCARTONNUMBER = dt.Rows[0]["MCARTONNUMBER"].ToString();
                    C_MPALLETNUMBER = dt.Rows[0]["MPALLETNUMBER"].ToString();
                    C_STORENUMBER = dt.Rows[0]["STORENUMBER"].ToString();
                }
                else
                {
                    throw new Exception(RES);
                }
                //SERROR = "1";

                //SERROR = "2";
                RES = "WO NOT FOUND ";
                DataTable dt_B = woinfo.GetWoInfo(C_WOID, null, "OUTPUTGROUP").Tables[0];
                if (dt_B.Rows.Count > 0)
                {
                    C_OUTPUTGROUP = dt_B.Rows[0]["OUTPUTGROUP"].ToString();

                    if (C_OUTPUTGROUP == MYGROUP)
                        C_ENDCOUNT = 1;
                    else
                        C_ENDCOUNT = 0;
                }
                else
                {
                    throw new Exception(RES);
                }

                RES = "NEXT ROUTE IS NULL";
                NEXTSTATION = ClsRoute.CHECK_ROUTE_NEXTCRAFTNAME(FLAG, C_ROUTEGROUPID, MYGROUP);
                if (string.IsNullOrEmpty(NEXTSTATION))
                    throw new Exception(RES + string.Format(" -WO[{0}],ROUTEGROUPID[{1}],MYGROUP[{2}],FLAG[{3}]", C_WOID, C_ROUTEGROUPID, MYGROUP, FLAG));

                if (C_ENDCOUNT != 0)
                {
                    if (FLAG == "1")
                        STR = NEXTSTATION;
                    else
                        STR = C_OUTPUTGROUP;
                }
                else
                {
                    STR = NEXTSTATION;
                }

                mst = new Dictionary<string, object>();
                mst.Add("WIPSTATION", STR);
                //  mst.Add("WOID", C_WOID);
                mst.Add("ESN", C_ESN);
                UpdateData("SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);
                PRO_INSERTSNDETAIL(C_ESN);
                RES = "OK";

            }
            catch (Exception ex)
            {
                try
                {
                    Insert_Exception_Log("UPDATE T107 1:" + ex.Message);
                    Dictionary<string, object> mst = new Dictionary<string, object>();
                    if (C_ENDCOUNT != 0)
                    {
                        int count = 0;
                        string table = "sfcr.t_wip_tracking_online A,sfcr.t_wo_info B".ToUpper();
                        string fieldlist = "B.OUTPUTGROUP";
                        string filter = "A.WOID=B.WOID AND A.ESN={0} AND B.OUTPUTGROUP={1}";
                        mst = new Dictionary<string, object>();
                        mst.Add("ESN", C_ESN);
                        mst.Add("OUTPUTGROUP", MYGROUP);
                        DataSet ds = TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
                        if (ds.Tables[0].Rows.Count > 0)
                            STR = ds.Tables[0].Rows[0][0].ToString();
                        else
                            STR = "NA";
                    }
                    else
                    {
                        STR = string.IsNullOrEmpty(NEXTSTATION) ? "NA" : NEXTSTATION; ;
                    }

                    mst = new Dictionary<string, object>();
                    mst.Add("WIPSTATION", STR);
                    //  mst.Add("WOID", C_WOID);
                    mst.Add("ESN", C_ESN);
                    UpdateData("SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);
                    PRO_INSERTSNDETAIL(C_ESN);
                    RES = "OK";
                }
                catch (Exception exx)
                {
                    RES = ex.Message;
                    Insert_Exception_Log("UPDATE T107 2:" + exx.Message);
                }

            }

            myWatch.Stop();
            Insert_DB_Log(string.Format("PRO_UPDATEWIPSTATION-->{0} DATA:{1} {2} " , myWatch.ElapsedMilliseconds.ToString(),SN,RES));

        }
        public string PRO_UPDATEWIPSTATIONPALTCTN(string LINE, string MYGROUP,string SECTION_NAME ,string SN, string EMP, int FLAG, string CARFTID, string WOID, string LOCDATA, string CUTDATA, int UPFLAG)
        {
            myWatch.Restart();
            string RES = string.Empty;
            string C_WOID = WOID;
            string C_SN = SN;
            string C_LINE = LINE;
            string NEXTSTATION = string.Empty;
            string STR = string.Empty;
            string C_TRAY = string.Empty;
            string C_STATIONNAME = MYGROUP + "1";
            string C_CARTON = string.Empty;
            string C_MCARTON = string.Empty;
            string C_PALLET = string.Empty;
            string C_MPALLET = string.Empty;
            Dictionary<string, object> mst = null;         
            try
            {                
                mst = new Dictionary<string, object>();
                mst.Add("ROUTGROUPID",CARFTID);
                mst.Add("CRAFTNAME",MYGROUP);
                mst.Add("STATION_FLAG",FLAG);           
                DataTable dtRoute = GetData("SFCB.B_ROUTE_INFO", "NEXTCRAFTNAME",mst); //BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];
                if (dtRoute.Rows.Count == 0)
                    throw new Exception("NEXT ROUTE IS NULL");
                NEXTSTATION = dtRoute.Rows[0]["NEXTCRAFTNAME"].ToString();

                DataTable dtwoInfo = woinfo.GetWoInfo(C_WOID, null, "OUTPUTGROUP").Tables[0];
                if (dtwoInfo.Rows[0]["OUTPUTGROUP"].ToString() == MYGROUP)
                {
                    if (FLAG == 1)
                        STR = NEXTSTATION;
                    else
                        STR = MYGROUP;
                }
                else
                    STR = NEXTSTATION;

                mst = new Dictionary<string, object>();
                mst.Add("LOCSTATION", MYGROUP);
                mst.Add("STATIONNAME", C_STATIONNAME);
                mst.Add("WIPSTATION", STR);
                mst.Add("NEXTSTATION", "NA");
                mst.Add("USERID", EMP);
                mst.Add("RECDATE", System.DateTime.Now);
                mst.Add("ERRFLAG", FLAG);
                mst.Add("SECTIONNAME", SECTION_NAME);
                switch (UPFLAG)
                {
                    case 0:
                        C_TRAY = LOCDATA;                        
                        mst.Add("TRAYNO", C_TRAY);
                        break;
                    case 1:
                        C_CARTON = LOCDATA;
                        C_MCARTON = CUTDATA;                      
                        mst.Add("CARTONNUMBER",C_CARTON);
                        mst.Add("MCARTONNUMBER", C_MCARTON);
                        break;
                    case 2:
                        C_PALLET = LOCDATA;
                        C_MPALLET = CUTDATA;                       
                        mst.Add("PALLETNUMBER", C_PALLET);
                        mst.Add("MPALLETNUMBER", C_MPALLET);
                        break;
                    case 3:
                      
                        mst.Add("WEIGHTQTY", LOCDATA);
                        break;
                }
                mst.Add("LINE", C_LINE);
              //  mst.Add("WOID", C_WOID);
                mst.Add("ESN", C_SN);
                UpdateData("SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);
                PRO_INSERTSNDETAIL(SN);
                RES = "OK";
            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log(string.Format("PRO_UPDATEWIPSTATIONPALTCTN ESN:{0} [{1}] {2}",C_SN,MYGROUP , RES));
            }

            myWatch.Stop();
            Insert_DB_Log("PRO_UPDATEWIPSTATIONPALTCTN-->" + myWatch.ElapsedMilliseconds.ToString());
            return RES;
        }

        public void PRO_UPDATEWIPSTATIONSTOCKIN(string LINE, string MYGROUP, string SN, string EMP, string FLAG)
        {
            string C_SN = SN;
            string C_LINE = LINE;          

            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("LOCSTATION", MYGROUP);
            mst.Add("STATIONNAME", MYGROUP + "1");
            mst.Add("WIPSTATION", "1008");
            mst.Add("NEXTSTATION", "NA");
            mst.Add("USERID", EMP);
            mst.Add("ERRFLAG", FLAG);
            mst.Add("RECDATE", System.DateTime.Now);
            mst.Add("LINE", C_LINE);
            mst.Add("ESN", C_SN);
            UpdateData("SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);
            PRO_INSERTSNDETAIL(C_SN);
        }
        private string PRO_CHECK_ROUTE_ATE(IDictionary<string, object> Dic)
        {
            string _StrErr = string.Empty;
             PRO_CHECK_ROUTE_ATE(Dic["DATA"].ToString(), Dic["MYGROUP"].ToString(), Dic["WO"].ToString(), out _StrErr);
             return _StrErr;
        }

        public void PRO_CHECK_ROUTE_ATE(string DATA, string MYGROUP, string WO, out string RES)
        {
            string C_WO = string.Empty;
            int C_FLAG;
            string C_RES = string.Empty;
            string ERROR_FLAG = string.Empty;
            string SCRAP_FLAG = string.Empty;
            string C_ENDGROUP = string.Empty;
            string C_ROUTECODE = string.Empty;
            string C_LOCGROUP = string.Empty;
            string C_NEXTSTATION = string.Empty;
            try
            {
                RES = "NO SN";
                DataTable dt = twip.Get_WIP_TRACKING("ESN", DATA, "WOID,LOCSTATION,NEXTSTATION,ERRFLAG,SCRAPFLAG,ROUTGROUPID").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    C_ROUTECODE = dt.Rows[0]["ROUTGROUPID"].ToString();
                    C_LOCGROUP = dt.Rows[0]["LOCSTATION"].ToString();
                    C_NEXTSTATION = dt.Rows[0]["NEXTSTATION"].ToString();
                    C_WO = dt.Rows[0]["WOID"].ToString();
                    ERROR_FLAG = dt.Rows[0]["ERRFLAG"].ToString();
                    SCRAP_FLAG = dt.Rows[0]["SCRAPFLAG"].ToString();
                }
                else
                    throw new Exception(RES);
                if (!string.IsNullOrEmpty(ERROR_FLAG))
                {
                    if (ERROR_FLAG != "0")
                        throw new Exception("SN IN REPAIR");
                    if (SCRAP_FLAG != "0")
                        throw new Exception("SN HAS SCRAP");
                }
                RES = "WO NO FOUNT";
                DataTable dt_B = woinfo.GetWoInfo(C_WO, null, "WOSTATE,OUTPUTGROUP").Tables[0];
                if (dt_B.Rows.Count > 0)
                {
                    C_ENDGROUP = dt_B.Rows[0]["OUTPUTGROUP"].ToString();
                    C_FLAG = Convert.ToInt32(dt_B.Rows[0]["WOSTATE"].ToString());
                }
                else
                    throw new Exception(RES);

                if (WO == C_WO)
                {
                    switch (C_FLAG)
                    {
                        case 0:
                            RES = "Waiting Relaese";
                            break;
                        case 1:
                            RES = "OK";
                            break;
                        case 2:
                            RES = "OK";
                            break;
                        case 3:
                            RES = "WO IS CLOSED ";
                            break;
                        case 4:
                            RES = "WO HOLD";
                            break;
                        default:
                            RES = "WO STATE ERROR";
                            break;
                    }
                    if (RES == "OK")
                    {
                        C_RES = PRO_CHECK_ROUTE(DATA, MYGROUP);
                        RES = C_RES;
                    }
                }
                else
                    throw new Exception("WO DIFFERENT");

            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log(string.Format("PRO_CHECK_ROUTE_ATE {0}  {1}" ,DATA, RES));
            }
        }
        private List<string> PRO_CHECK_ROUTE_MOBILE(IDictionary<string, object> Dic)
        {
            List<string> C_RES = new List<string>();
            string RES = string.Empty;
            string COMMAND = string.Empty;
            PRO_CHECK_ROUTE_MOBILE(Dic["DATA"].ToString(), Dic["MYGROUP"].ToString(), Dic["WO"].ToString(), out RES, out COMMAND);
            C_RES.Add(RES);
            C_RES.Add(COMMAND);
            return C_RES;
        }
        public void PRO_CHECK_ROUTE_MOBILE(string DATA, string MYGROUP, string WO, out string RES, out string COMMAND)
        {
            string C_WO = string.Empty;
            int C_FLAG;
            string C_RES = string.Empty;
            string ERROR_FLAG = string.Empty;
            string SCRAP_FLAG = string.Empty;
            string C_ENDGROUP = string.Empty;
            string C_ROUTECODE = string.Empty;
            string C_LOCGROUP = string.Empty;
            string C_NEXTSTATION = string.Empty;
            string C_DATA = " " + DATA;
            C_DATA = C_DATA.Substring(C_DATA.Length - 25, 25);
            string C_OUT_RS232 = C_DATA + "ERRO";
            try
            {
                RES = "NO SN";
                DataTable dt = twip.Get_WIP_TRACKING("ESN", DATA, "WOID,LOCSTATION,NEXTSTATION,ERRFLAG,SCRAPFLAG,ROUTGROUPID").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    C_ROUTECODE = dt.Rows[0]["ROUTGROUPID"].ToString();
                    C_LOCGROUP = dt.Rows[0]["LOCSTATION"].ToString();
                    C_NEXTSTATION = dt.Rows[0]["NEXTSTATION"].ToString();
                    C_WO = dt.Rows[0]["WOID"].ToString();
                    ERROR_FLAG = dt.Rows[0]["ERRFLAG"].ToString();
                    SCRAP_FLAG = dt.Rows[0]["SCRAPFLAG"].ToString();
                }
                else
                {
                    throw new Exception(RES);
                }
                if (!string.IsNullOrEmpty(ERROR_FLAG))
                {
                    if (ERROR_FLAG != "0")
                        throw new Exception("SN IN REPAIR");
                    if (SCRAP_FLAG != "0")
                        throw new Exception("SN HAS SCRAP");

                }
                RES = "WO NO FOUNT";
                DataTable dt_B = woinfo.GetWoInfo(C_WO, null, "WOSTATE,OUTPUTGROUP").Tables[0];
                if (dt_B.Rows.Count > 0)
                {
                    C_ENDGROUP = dt_B.Rows[0]["OUTPUTGROUP"].ToString();
                    C_FLAG = Convert.ToInt32(dt_B.Rows[0]["WOSTATE"].ToString());
                }
                else
                {
                    throw new Exception(RES);
                }
                if (WO == C_WO)
                {
                    switch (C_FLAG)
                    {
                        case 0:
                            RES = "Waiting Relaese";
                            break;
                        case 1:
                            RES = "OK";
                            break;
                        case 2:
                            RES = "OK";
                            break;
                        case 3:
                            RES = "WO IS CLOSED ";
                            break;
                        case 4:
                            RES = "WO HOLD";
                            break;
                        default:
                            RES = "WO STATE ERROR";
                            break;
                    }
                    if (RES == "OK")
                    {
                        C_RES = PRO_CHECK_ROUTE(DATA, MYGROUP);
                        RES = C_RES;
                        if (C_RES == "OK")
                        {
                            C_OUT_RS232 = C_DATA + "PASS";
                        }
                    }
                }
                else
                {
                    throw new Exception("WO DIFFERENT");
                }
                COMMAND = C_OUT_RS232 + "\r\n";
            }
            catch (Exception ex)
            {
                RES = ex.Message;
                COMMAND = C_OUT_RS232 + "\r\n";
                Insert_Exception_Log("PRO_CHECK_ROUTE_MOBILE " + RES);
            }
        }
        public void PRO_UPDATE_WOINFO(string MO, string MYGROUP, int C_QTY)
        {
            string STARTCAFT = string.Empty;
            string ENDCAFT = string.Empty;
            string err = string.Empty;

            DataTable dt = woinfo.GetWoInfo(MO, null, "INPUTGROUP,OUTPUTGROUP,INPUTQTY").Tables[0];
            if (dt.Rows.Count > 0)
            {
                STARTCAFT = dt.Rows[0]["INPUTGROUP"].ToString();
                ENDCAFT = dt.Rows[0]["OUTPUTGROUP"].ToString();
            }
            if (MYGROUP == STARTCAFT)
            {             
                string table = "SFCR.T_WO_INFO";
                string fieldlist = "INPUTQTY = INPUTQTY + {0}";
                string filter = "WOID ={0}";
                IDictionary<string, object> modFields = new Dictionary<string, object>();
                modFields.Add("INPUTQTY", C_QTY);
                if (Convert.ToInt32(dt.Rows[0]["INPUTQTY"].ToString()) == 0)
                {
                    fieldlist += " ,WOSTATE=2 ";                    
                }

                IDictionary<string, object> keyVals = new Dictionary<string, object>();
                keyVals.Add("WOID", MO);              

                TransactionManager.UpdateBatchData(table, fieldlist, modFields, filter, keyVals);

            }
            if (MYGROUP == ENDCAFT)
            {              
                string table = "SFCR.T_WO_INFO";
                string fieldlist = "OUTPUTQTY = OUTPUTQTY + {0}";
                string filter = "WOID ={0}";
                IDictionary<string, object> modFields = new Dictionary<string, object>();
                modFields.Add("OUTPUTQTY", C_QTY);
                IDictionary<string, object> keyVals = new Dictionary<string, object>();
                keyVals.Add("WOID", MO);
                TransactionManager.UpdateBatchData(table, fieldlist, modFields, filter, keyVals);

                fieldlist = "WOSTATE =3,WO_CLOSE_TIME=NOW()";
                filter = "(OUTPUTQTY + SCRAPQTY) >= QTY AND WOID ={0}";
                modFields = new Dictionary<string, object>();
                keyVals = new Dictionary<string, object>();
                keyVals.Add("WOID", MO);
                TransactionManager.UpdateBatchData(table, fieldlist, modFields, filter, keyVals);

            }
        }


        public void PRO_UPDATEWIPSTATION_NEW(string LINE, string MYGROUP,string SECTION_NAME,string SN, string EMP, int FLAG, string CARFTID, string WOID, string ATESTATIONNO, string STATION_NAME, string PN, out string RES)
        {
            myWatch.Restart();
            string C_NEXTSTATION = string.Empty;
          //  int C_COUNT = 0;
            string C_WOID = WOID;
            string STR = string.Empty;
            string C_CRAFTID = CARFTID;
            string C_RES = string.Empty;
            string C_SN = SN;
            string C_LINE = LINE;
            string C_ROUTEGROUPID = string.Empty;
            string C_OUTPUTGROUP = string.Empty;
            int C_ENDCOUNT = 0;

            Dictionary<string, object> mst = null;

            try
            {
                mst = new Dictionary<string, object>();
                mst.Add("LOCSTATION", MYGROUP);
                mst.Add("STATIONNAME", STATION_NAME);             
                mst.Add("NEXTSTATION", "NA");
                mst.Add("USERID", EMP);
                mst.Add("ERRFLAG", FLAG);
                mst.Add("RECDATE", System.DateTime.Now);
                mst.Add("LINE", C_LINE);
                mst.Add("SECTIONNAME", SECTION_NAME);
                mst.Add("ATE_STATION_NO", ATESTATIONNO);
              //  mst.Add("WOID", C_WOID);
                mst.Add("ESN", C_SN);
                UpdateData("SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);

                C_RES = "WIP NO DATA";
                DataTable dt = twip.Get_WIP_TRACKING("ESN", SN, "WOID,ROUTGROUPID").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    C_ROUTEGROUPID = dt.Rows[0]["ROUTGROUPID"].ToString();
                    C_WOID = dt.Rows[0]["WOID"].ToString();
                    //C_PARTNUMBER = dt.Rows[0]["PARTNUMBER"].ToString();
                    //C_CARTONNUMBER = dt.Rows[0]["CARTONNUMBER"].ToString();
                    //C_TRAYNO = dt.Rows[0]["TRAYNO"].ToString();
                    //C_PALLETNUMBER = dt.Rows[0]["PALLETNUMBER"].ToString();
                    //C_SN = dt.Rows[0]["SN"].ToString();
                    //C_MAC = dt.Rows[0]["MAC"].ToString();
                    //C_MCARTONNUMBER = dt.Rows[0]["MCARTONNUMBER"].ToString();
                    //C_MPALLETNUMBER = dt.Rows[0]["MPALLETNUMBER"].ToString();
                    //C_STORENUMBER = dt.Rows[0]["STORENUMBER"].ToString();
                }
                else
                {
                    throw new Exception(C_RES);
                }

                C_RES = "WO NOT FOUND";
                DataTable dt_B = woinfo.GetWoInfo(C_WOID, null, "OUTPUTGROUP").Tables[0];
                if (dt_B.Rows.Count > 0)
                {
                    C_OUTPUTGROUP = dt_B.Rows[0]["OUTPUTGROUP"].ToString();

                    if (C_OUTPUTGROUP == MYGROUP)
                        C_ENDCOUNT = 1;
                    else
                        C_ENDCOUNT = 0;
                }
                else
                {
                    throw new Exception(C_RES);
                }

                C_RES = "NEXT ROUTE IS NULL";
                C_NEXTSTATION = ClsRoute.CHECK_ROUTE_NEXTCRAFTNAME(FLAG.ToString(), C_ROUTEGROUPID, MYGROUP);
                if (string.IsNullOrEmpty(C_NEXTSTATION))                 
                   throw new Exception(C_RES + string.Format(" -WO[{0}],ROUTEGROUPID[{1}],MYGROUP[{2}],FLAG[{3}]", C_WOID, C_ROUTEGROUPID, MYGROUP, FLAG));
                if (C_ENDCOUNT != 0)
                {
                    if (FLAG == 1)
                        STR = C_NEXTSTATION;
                    else
                        STR = C_OUTPUTGROUP;
                }
                else
                {
                    STR = C_NEXTSTATION;
                }

                mst = new Dictionary<string, object>();
                mst.Add("WIPSTATION", STR);
               // mst.Add("WOID", C_WOID);
                mst.Add("ESN", SN);
                UpdateData("SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);
                PRO_INSERTSNDETAIL(SN);
                RES= "OK";
            }
            catch (Exception ex)
            {
                try
                {
                    Insert_Exception_Log(string.Format("UPDATE T107 NEW1: WOID[{0}] -->{1} ",WOID , ex.Message));             
                    if (C_ENDCOUNT != 0)
                    {
                        int count = 0;
                        string table = "sfcr.t_wip_tracking_online A,sfcr.t_wo_info B".ToUpper();
                        string fieldlist = "B.OUTPUTGROUP";
                        string filter = "A.WOID=B.WOID AND A.ESN={0} AND B.OUTPUTGROUP={1}";
                        mst = new Dictionary<string, object>();
                        mst.Add("ESN", SN);
                        mst.Add("OUTPUTGROUP", MYGROUP);
                        DataSet ds = TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
                        if (ds.Tables[0].Rows.Count > 0)
                            STR = ds.Tables[0].Rows[0][0].ToString();
                        else
                            STR = "NA";
                    }
                    else
                    {
                        STR = string.IsNullOrEmpty(C_NEXTSTATION) ? "NA" : C_NEXTSTATION;
                    }

                    mst = new Dictionary<string, object>();
                    mst.Add("WIPSTATION", STR);
                 //   mst.Add("WOID", C_WOID);
                    mst.Add("ESN", SN);
                    UpdateData("SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);
                    PRO_INSERTSNDETAIL(SN);
                    RES = "OK";
                }
                catch (Exception exx)
                {
                    RES = ex.Message;
                    Insert_Exception_Log(string.Format("UPDATE T107 NEW2: WOID[{0}]  -->{1}",WOID, exx.Message));
                }
            }

            myWatch.Stop();
            Insert_DB_Log(string.Format("PRO_UPDATEWIPSTATION_NEW-->{0} DATA:{1}  {2}" , myWatch.ElapsedMilliseconds.ToString(),SN,RES));
        }

        /// <summary>
        /// 20151006 修改产能报表记录到t_station_recount_temp
        /// </summary>
        /// <param name="MYGROUP"></param>
        /// <param name="MODEL"></param>
        /// <param name="MODELDESC"></param>
        /// <param name="LINE"></param>
        /// <param name="WO"></param>
        /// <param name="FLAG"></param>
        /// <param name="RES"></param>
        public void PRO_INSERTSTATIONREC(string MYGROUP, string MODEL, string MODELDESC, string LINE, string WO, int FLAG, out string RES)
        {

            string C_WORKSECTION = string.Empty;
            string C_CLASS = string.Empty;
            string C_DAY = string.Empty;
            string C_DATE1 = string.Empty;
            string C_DATE2 = string.Empty;
            string C_RES = string.Empty;
            string C_WORKDATE = string.Empty;
            string C_CLASSDATE = string.Empty;
            string C_ROWID = string.Empty;
            string _T_RES = string.Empty;
        
            try
            {
                _T_RES = "PRO_GETWORKCLASS";
                PRO_GETWORKCLASS(out C_WORKSECTION, out C_CLASS, out C_DAY,out C_DATE1,out C_DATE2, out C_RES);
                if (C_RES == "OK")
                {
                   // C_WORKDATE = System.DateTime.Now.ToString("yyyyMMdd");
                    C_WORKDATE = C_DATE1;
                    if (C_DAY == "TOMORROW")
                    {
                        C_CLASSDATE = C_DATE2;//System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd");// TO_CHAR(SYSDATE - 1, 'YYYYMMDD');
                        if (C_WORKSECTION == "23")
                            C_WORKDATE =C_DATE2 ;//System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                    }
                    else
                        C_CLASSDATE = C_WORKDATE;

                    _T_RES = "GET_STN_REC";
                    DataTable dtStnRec = tStaR.GET_STN_REC_2(C_WORKDATE, C_WORKSECTION, WO, MYGROUP, LINE).Tables[0];
                    if (dtStnRec.Rows.Count > 0)
                    {
                        _T_RES = "update_station_recout flag[0]";
                        if (FLAG == 0)
                        {
                            _T_RES = string.Format("update_station_recout flag[0-0] MO:{0},ROUTE:{1},LINE:{2}",WO,MYGROUP,LINE);                  
                            C_RES = tStaR.update_station_recout(dtStnRec.Rows[0]["ROWID"].ToString(), WO, MYGROUP, C_WORKDATE, C_WORKSECTION, LINE, 1, 0, 0, 0, "I");

                        }
                        else
                        {
                            _T_RES = "update_station_recout flag[0-1]";                        
                            C_RES = tStaR.update_station_recout(dtStnRec.Rows[0]["ROWID"].ToString(), WO, MYGROUP, C_WORKDATE, C_WORKSECTION, LINE, 0, 1, 0, 0, "I");

                        }                    
                    }
                    else
                    {
                        _T_RES = "update_station_recout flag[1]";
                        if (FLAG == 0)
                        {                      
                            C_RES=  tStaR.insert_station_recount(WO, MYGROUP, C_WORKDATE, MODEL, C_WORKSECTION, C_CLASS, C_CLASSDATE, LINE, 1 ,0, 0, 0, "I");

                        }
                        else
                        {                         
                         C_RES=  tStaR.insert_station_recount(WO, MYGROUP, C_WORKDATE, MODEL, C_WORKSECTION, C_CLASS, C_CLASSDATE, LINE, 0, 1, 0, 0, "I");

                        }
                   
                    }

                    RES = C_RES;

                }
                else
                {
                    RES = C_RES;
                }
            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log("PRO_INSERTSTATIONREC " + _T_RES + "---" + RES);
            }

        }
    
        public string PRO_TEST_CTN_PALT_TRAY(string DATA, string MYGROUP, string EMP, string EC, string LINE, string LOCDATA, string CUTDATA, int UPFLAG)
        {
            System.Diagnostics.Stopwatch myWatch_Main = System.Diagnostics.Stopwatch.StartNew();
            myWatch_Main.Start();
            string C_WORKSECTION = string.Empty;
            string C_CLASS = string.Empty;
            string C_DAY = string.Empty;
            string C_DATE1 = string.Empty;
            string C_DATE2 = string.Empty;
            string C_WORKDATE = string.Empty;
            string C_RES = string.Empty;
            string RES = string.Empty;
            string C_CLASSDATE = string.Empty;
            string C_EMP = string.Empty;
            string C_WO = string.Empty;
            string C_MODEL = string.Empty;
            string C_MODELDESC = string.Empty;
            string C_LOCGROUP = string.Empty;
            string C_NEXTSTATION = string.Empty;
            string C_ERRFLAG = string.Empty;
            string C_ROUTECODEID = string.Empty;
            int C_FLAG = 0;
            string C_ENDGROUP = string.Empty;
          
            if (DB_Flag == 0)
            { 
                #region MySQL
                try
                {
                    PRO_GETWORKCLASS(out C_WORKSECTION, out C_CLASS, out C_DAY, out C_DATE1, out C_DATE2, out C_RES);
               
                    if (C_RES != "OK")
                        throw new Exception(C_RES);
                    else
                    {
                        //if (C_DAY == "TOMORROW")
                        //    C_CLASSDATE = System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                        //else
                        //    C_CLASSDATE = System.DateTime.Now.ToString("yyyyMMdd");
                        C_WORKDATE = C_DATE1;
                        if (C_DAY == "TOMORROW")
                        {
                            C_CLASSDATE = C_DATE2;
                            if (C_WORKSECTION == "23")
                                C_WORKDATE = C_DATE2;
                        }
                        else
                            C_CLASSDATE = C_WORKDATE;
                    }
            
                    PRO_GETEMPNO(EMP, out C_RES, out  C_EMP);         

                    if (C_RES != "OK")
                        throw new Exception(C_RES);
                    DataTable dtwip = twip.Get_WIP_TRACKING("ESN", DATA, "WOID,PARTNUMBER,PRODUCTNAME,LOCSTATION,NEXTSTATION,ERRFLAG,ROUTGROUPID").Tables[0];
                    if (dtwip.Rows.Count == 0)
                        throw new Exception("No Data");
                    C_WO = dtwip.Rows[0]["WOID"].ToString();
                    C_MODEL = dtwip.Rows[0]["PARTNUMBER"].ToString();
                    C_MODELDESC = dtwip.Rows[0]["PRODUCTNAME"].ToString();
                    C_LOCGROUP = dtwip.Rows[0]["LOCSTATION"].ToString();
                    C_NEXTSTATION = dtwip.Rows[0]["NEXTSTATION"].ToString();
                    C_ERRFLAG = dtwip.Rows[0]["ERRFLAG"].ToString();
                    C_ROUTECODEID = dtwip.Rows[0]["ROUTGROUPID"].ToString();

                    DataTable dtwoInfo = woinfo.GetWoInfo(C_WO, null, "WOSTATE,OUTPUTGROUP").Tables[0];
                    C_FLAG = Convert.ToInt32(dtwoInfo.Rows[0]["WOSTATE"].ToString());
                    C_ENDGROUP = dtwoInfo.Rows[0]["OUTPUTGROUP"].ToString();
                    switch (C_FLAG)
                    {
                        case 0:
                            RES = "Waiting Relaese";
                            break;
                        case 1:
                            RES = "OK";
                            break;
                        case 2:
                            RES = "OK";
                            break;
                        case 3:
                            RES = "WO IS CLOSED ";
                            break;
                        case 4:
                            RES = "WO HOLD";
                            break;
                        default:
                            RES = "WO STATU ERROR";
                            break;
                    }
                    if (RES == "OK")
                    {
                        if (EC == "NA")
                        {
                          
                            C_RES = PRO_CHECK_ROUTE(DATA, MYGROUP);                       
                         
                            if (C_RES == "OK")
                            {                              
                                PRO_STNREC(LINE, MYGROUP, C_MODEL, C_MODELDESC, C_WO, DATA, 0, out C_RES);                            
                                if (C_RES == "OK")
                                {                                 
                                    C_RES = PRO_UPDATEWIPSTATIONPALTCTN(LINE, MYGROUP,"NA", DATA, C_EMP, 0, C_ROUTECODEID, C_WO, LOCDATA, CUTDATA, UPFLAG);
                                    RES = C_RES;                                  
                                }
                                else
                                {
                                    RES = C_RES;
                                }
                            }
                            else
                            {
                                RES = C_RES;
                            }
                        }
                        else
                        {
                            C_RES = PRO_CHECK_ROUTE(DATA, MYGROUP);
                            if (C_RES != "OK")
                                throw new Exception(C_RES);
                            C_RES = PRO_CHECK_ROUTE_RE(DATA, MYGROUP);
                            if (C_RES == "OK")
                            {
                                PRO_STNREC(LINE, MYGROUP, C_MODEL, C_MODELDESC, C_WO, DATA, 1, out C_RES);
                                if (C_RES == "OK")
                                {
                                    PRO_UPDATEWIPSTATION(LINE, MYGROUP,"NA", DATA,C_WO, C_EMP, "1", MYGROUP + "1", out C_RES);
                                    if (C_RES == "OK")
                                    {                                       
                                        Dictionary<string, object> mst = new Dictionary<string, object>();
                                        mst.Add("ERRORCODE", EC);
                                        mst.Add("ESN", DATA);
                                        mst.Add("WOID", C_WO);
                                        mst.Add("PARTNUMBER", C_MODEL);
                                        mst.Add("CRAFTID", MYGROUP);
                                        mst.Add("INPUTUSER", C_EMP);
                                        mst.Add("STATUS", "0");
                                        mst.Add("INPUTDATE", System.DateTime.Now);
                                        mst.Add("LINEID", LINE);
                                        mst.Add("TCLASSDATE", C_CLASSDATE);
                                        mst.Add("TCLASS", C_CLASS);
                                        mst.Add("TWORKSECTION", C_WORKSECTION);
                                        InsertData("SFCR.T_REPAIR_INFO", mst);
                                        RES = "OK";

                                    }
                                    else
                                    {
                                        RES = C_RES;
                                    }


                                }
                                else
                                {
                                    RES = C_RES;
                                }
                            }
                            else
                            {
                                RES = C_RES;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    RES = ex.Message;
                    Insert_Exception_Log("PRO_TEST_CTN_PALT_TRAY " + RES);
                }
                #endregion
            }
            if (DB_Flag == 1)
            {
                try
                {
                    IBaseProvider dp = new BaseProvider();
                    IDictionary<string, object> parms = new Dictionary<string, object>();
                    IDictionary<string, object> procedureOutRes = new Dictionary<string, object>();
                    parms.Add("DATA", DATA);
                    parms.Add("MYGROUP", MYGROUP);
                    parms.Add("EMP", EMP);
                    parms.Add("EC", EC);
                    parms.Add("LINE", LINE);
                    parms.Add("LOCDATA", LOCDATA);
                    parms.Add("CUTDATA", CUTDATA);
                    parms.Add("UPFLAG", UPFLAG);
                    procedureOutRes.Add("RES", (object)200);
                    dp.StoreProcedureExec("PRO_TEST_CTN_PALT_TRAY", parms, procedureOutRes);
                    RES = procedureOutRes["RES"].ToString();
                }
                catch (Exception ex)
                {
                    RES = ex.Message;
                }
            }

            myWatch_Main.Stop();
            Insert_DB_Log(string.Format("PRO_TEST_CTN_PALT_TRAY SN[{0}] MYGROUP[{1}] RES:{2} -->{3} ", DATA, MYGROUP, RES, myWatch_Main.ElapsedMilliseconds.ToString()));
            return RES;
        }
        private string PRO_TEST_MAIN_AUTO(IDictionary<string, object> Dic)
        {
            try
            {
                return PRO_TEST_MAIN_AUTO(Dic["DATA"].ToString(), Dic["MYGROUP"].ToString(), Dic["SECTION_NAME"].ToString(), Dic["STATION_NAME"].ToString(), Dic["EMP"].ToString(), Dic["LINE"].ToString());
            }
            catch
            {
                return PRO_TEST_MAIN_AUTO(Dic["DATA"].ToString(), Dic["MYGROUP"].ToString(),"NA",Dic["MYGROUP"].ToString(), Dic["EMP"].ToString(), Dic["LINE"].ToString());
            }
        }
        public string PRO_TEST_MAIN_AUTO(string DATA, string MYGROUP, string SECTION_NAME,string STATION_NAME,string EMP, string LINE)
        {
            string RES = "EXCPTION INS ATE";
            string C_WORKSECTION = string.Empty;
            string C_EMP = string.Empty;
            string C_CLASS = string.Empty;
            string C_DAY = string.Empty;
            string C_DATE1 = string.Empty;
            string C_DATE2 = string.Empty;
            string C_WORKDATE = string.Empty;
            string C_RES = string.Empty;
            string C_CLASSDATE = string.Empty;
            string C_WO = string.Empty;
            string C_MODEL = string.Empty;
            string C_LOCGROUP = string.Empty;
            string C_NEXTSTATION = string.Empty;
            string C_ERRFLAG = string.Empty;
            string C_ROUTECODEID = string.Empty;
            int C_FLAG = 0;
            string C_ENDGROUP = string.Empty;
            string C_ESN = string.Empty;
            string C_EC = string.Empty;
            string C_MACH = string.Empty;
            string C_MO = string.Empty;
            string C_MODELDESC = string.Empty;
            try
            {
                C_ESN = DATA.Substring(0, 25).Trim();     //RTRIM(LTRIM(SUBSTR(DATA, 1, 25)));
                C_EC = DATA.Substring(25, 8).Trim(); //RTRIM(LTRIM(SUBSTR(DATA, 26, 8)));
                C_MACH = DATA.Substring(33, 20).Trim(); // RTRIM(LTRIM(SUBSTR(DATA, 34, 20)));
                C_MO = DATA.Substring(53, 10).Trim();//RTRIM(LTRIM(SUBSTR(DATA, 54, 10)));
            }
            catch
            {
            }
            try
            {
                PRO_GETWORKCLASS(out C_WORKSECTION, out C_CLASS, out C_DAY, out C_DATE1, out C_DATE2, out C_RES);
                if (C_RES != "OK")
                    throw new Exception(C_RES);

                //if (C_DAY == "TOMORROW")
                //    C_CLASSDATE = System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                //else
                //    C_CLASSDATE = System.DateTime.Now.ToString("yyyyMMdd");
                C_WORKDATE = C_DATE1;
                if (C_DAY == "TOMORROW")
                {
                    C_CLASSDATE = C_DATE2;
                    if (C_WORKSECTION == "23")
                        C_WORKDATE = C_DATE2;
                }
                else
                    C_CLASSDATE = C_WORKDATE;

                if (C_EC != "NA")
                {
                    C_RES = PRO_CHECKEC(C_EC);
                    if (C_RES != "OK")
                        throw new Exception(C_RES);
                }

                PRO_GETEMPNO(EMP, out C_RES, out C_EMP);
                if (C_RES != "OK")
                    throw new Exception(C_RES);

                DataTable dtwip = twip.Get_WIP_TRACKING("ESN", C_ESN, "WOID,PARTNUMBER,PRODUCTNAME,LOCSTATION,NEXTSTATION,ERRFLAG,ROUTGROUPID").Tables[0];
                if (dtwip.Rows.Count == 0)
                    throw new Exception("NO SN");
                C_WO = dtwip.Rows[0]["WOID"].ToString();
                C_MODEL = dtwip.Rows[0]["PARTNUMBER"].ToString();
                C_LOCGROUP = dtwip.Rows[0]["LOCSTATION"].ToString();
                C_NEXTSTATION = dtwip.Rows[0]["NEXTSTATION"].ToString();
                C_ERRFLAG = dtwip.Rows[0]["ERRFLAG"].ToString();
                C_ROUTECODEID = dtwip.Rows[0]["ROUTGROUPID"].ToString();
                C_MODELDESC = dtwip.Rows[0]["PRODUCTNAME"].ToString();
                if (C_WO != C_MO)
                    throw new Exception("WO DIFFERENT");
                DataTable dtwoInfo = woinfo.GetWoInfo(C_WO, null, "WOSTATE,OUTPUTGROUP").Tables[0];
                if (dtwoInfo.Rows.Count == 0)
                    throw new Exception("WO NOT FOUND");
                C_FLAG = Convert.ToInt32(dtwoInfo.Rows[0]["WOSTATE"].ToString());
                C_ENDGROUP = dtwoInfo.Rows[0]["OUTPUTGROUP"].ToString();

                switch (C_FLAG)
                {
                    case 0:
                        RES = "Waiting Relaese";
                        break;
                    case 1:
                        RES = "OK";
                        break;
                    case 2:
                        RES = "OK";
                        break;
                    case 3:
                        RES = "WO IS CLOSED ";
                        break;
                    case 4:
                        RES = "WO HOLD";
                        break;
                }
                if (RES != "OK")
                    throw new Exception(RES);

                        if (C_EC == "NA")
                        {
                            C_RES = PRO_CHECK_ROUTE(C_ESN, MYGROUP);
                            if (C_RES == "OK")
                            {
                                PRO_STNREC(LINE, MYGROUP, C_MODEL, C_MODELDESC, C_WO, C_ESN, 0, out C_RES);
                                if (C_RES == "OK")
                                {
                                    PRO_UPDATEWIPSTATION_NEW(LINE, MYGROUP,"NA", C_ESN, C_EMP, 0, C_ROUTECODEID, C_WO, "NA", MYGROUP + "1", C_MODEL, out C_RES);
                                    RES = C_RES;
                                }
                                else
                                {
                                    RES = C_RES;
                                }
                            }
                            else
                            {
                                RES = C_RES;
                            }
                        }
                        else
                        {

                            C_RES = PRO_CHECK_ROUTE_RE(C_ESN, MYGROUP);
                            if (C_RES == "OK")
                            {
                                PRO_STNREC(LINE, MYGROUP, C_MODEL, C_MODELDESC, C_WO, C_ESN, 1, out C_RES);
                                if (C_RES == "OK")
                                {
                                    PRO_UPDATEWIPSTATION_NEW(LINE, MYGROUP,"NA", C_ESN, C_EMP, 1, C_ROUTECODEID, C_WO, "NA", MYGROUP + "1",C_MODEL, out C_RES);
                                    if (C_RES == "OK")
                                    {                                        
                                        Dictionary<string, object> mst = new Dictionary<string, object>();
                                        mst.Add("ERRORCODE", C_EC);
                                        mst.Add("ESN", C_ESN);
                                        mst.Add("WOID", C_WO);
                                        mst.Add("PARTNUMBER", C_MODEL);
                                        mst.Add("CRAFTID", MYGROUP);
                                        mst.Add("INPUTUSER", C_EMP);
                                        mst.Add("STATUS", "0");
                                        mst.Add("INPUTDATE", System.DateTime.Now);
                                        mst.Add("LINEID", LINE);
                                        mst.Add("TCLASSDATE", C_CLASSDATE);
                                        mst.Add("TCLASS", C_CLASS);
                                        mst.Add("TWORKSECTION", C_WORKSECTION);
                                        InsertData("SFCR.T_REPAIR_INFO", mst);

                                        RES = "OK";

                                    }
                                    else
                                    {
                                        RES = C_RES;
                                    }
                                }
                                else
                                {
                                    RES = C_RES;
                                }
                            }
                            else
                            {
                                RES = C_RES;
                            }
                        }
                 
            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log("PRO_TEST_MAIN_AUTO " + RES);
            }

          //  Insert_DB_Log(string.Format("PRO_TEST_MAIN_AUTO DATA:{0} {1}" ,DATA, RES));
            return RES;
        }


        public string PRO_UPDATESTOCKIN(string LINE, string MYGROUP, string STATION_NAME, string SN, string EMP, string FLAG)
        {
            string C_LINE = LINE;
            string C_SN = SN;
            string RES = "UPDATE ERROR";
            string C_ESN = string.Empty;
            string C_WOID = string.Empty;
            string C_PARTNUMBER = string.Empty;
            string C_LOCSTATION = string.Empty;
            string C_STATIONNAME = string.Empty;
            string C_WIPSTATION = string.Empty;
            string C_NEXTSTATION = string.Empty;
            string C_USERID = string.Empty;
            string C_RECDATE;
            string C_ERRFLAG = string.Empty;
            string C_SCRAPFLAG = string.Empty;
            string C_SSN = string.Empty;
            string C_MAC = string.Empty;
            string C_CARTONNUMBER = string.Empty;
            string C_TRAYNO = string.Empty;
            string C_PALLETNUMBER = string.Empty;
            string C_MCARTONNUMBER = string.Empty;
            string C_MPALLETNUMBER = string.Empty;
            string C_ROUTGROUPID = string.Empty;
            string C_STORENUMBER = string.Empty;
            string C_WEIGHTQTY = string.Empty;
            string C_QANO = string.Empty;
            string C_QARESULT = string.Empty;
            string C_TRACKNO = string.Empty;
            string C_ATESTATIONNO = string.Empty;
            string C_TYPE = string.Empty;
            string C_VERSIONCODE = string.Empty;
            string C_SECTIONNAME = string.Empty;
            string C_INLINETIME;
            string C_PRODUCTNAME = string.Empty;
            string C_IMEI = string.Empty;
            string C_BOMNUMBER = string.Empty;

            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);      
            try
            {             
                Dictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("LOCSTATION", MYGROUP);
                mst.Add("STATIONNAME", STATION_NAME);
                mst.Add("WIPSTATION", "1008");
                mst.Add("NEXTSTATION", "NA");
                mst.Add("USERID", EMP);
                mst.Add("ERRFLAG", FLAG);
                mst.Add("RECDATE", System.DateTime.Now);
                mst.Add("LINE", LINE);
                mst.Add("ESN", C_SN);

                UpdateData("SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);

                RES = "INSERT DETAIL ERROR";
                PRO_INSERTSNDETAIL(C_SN);
                RES = "SELECT ERROR";
                DataTable dtwip = twip.Get_WIP_TRACKING("ESN", C_SN).Tables[0];
                if (dtwip.Rows.Count == 0)
                    throw new Exception(RES);
                C_ESN = dtwip.Rows[0]["ESN"].ToString();
                C_WOID = dtwip.Rows[0]["WOID"].ToString();
                C_PARTNUMBER = dtwip.Rows[0]["PARTNUMBER"].ToString();
                C_LOCSTATION = dtwip.Rows[0]["LOCSTATION"].ToString();
                C_STATIONNAME = dtwip.Rows[0]["STATIONNAME"].ToString();
                C_WIPSTATION = dtwip.Rows[0]["WIPSTATION"].ToString();
                C_NEXTSTATION = dtwip.Rows[0]["NEXTSTATION"].ToString();
                C_USERID = dtwip.Rows[0]["USERID"].ToString();
                C_RECDATE = dtwip.Rows[0]["RECDATE"].ToString();
                C_ERRFLAG = dtwip.Rows[0]["ERRFLAG"].ToString();
                C_SCRAPFLAG = dtwip.Rows[0]["SCRAPFLAG"].ToString();
                C_SSN = dtwip.Rows[0]["SN"].ToString();
                C_MAC = dtwip.Rows[0]["MAC"].ToString();
                C_CARTONNUMBER = dtwip.Rows[0]["CARTONNUMBER"].ToString();
                C_TRAYNO = dtwip.Rows[0]["TRAYNO"].ToString();
                C_PALLETNUMBER = dtwip.Rows[0]["PALLETNUMBER"].ToString();
                C_MCARTONNUMBER = dtwip.Rows[0]["MCARTONNUMBER"].ToString();
                C_MPALLETNUMBER = dtwip.Rows[0]["MPALLETNUMBER"].ToString();
                C_LINE = dtwip.Rows[0]["LINE"].ToString();
                C_ROUTGROUPID = dtwip.Rows[0]["ROUTGROUPID"].ToString();
                C_STORENUMBER = dtwip.Rows[0]["STORENUMBER"].ToString();
                C_WEIGHTQTY = dtwip.Rows[0]["WEIGHTQTY"].ToString();
                C_QANO = dtwip.Rows[0]["QA_NO"].ToString();
                C_QARESULT = dtwip.Rows[0]["QA_RESULT"].ToString();
                C_TRACKNO = dtwip.Rows[0]["TRACK_NO"].ToString();
                C_ATESTATIONNO = dtwip.Rows[0]["ATE_STATION_NO"].ToString();
                C_TYPE = dtwip.Rows[0]["TYPE"].ToString();
                C_VERSIONCODE = dtwip.Rows[0]["VERSIONCODE"].ToString();
                C_SECTIONNAME = dtwip.Rows[0]["SECTIONNAME"].ToString();
                C_INLINETIME = dtwip.Rows[0]["IN_LINE_TIME"].ToString();
                C_PRODUCTNAME = dtwip.Rows[0]["PRODUCTNAME"].ToString();
                C_IMEI = dtwip.Rows[0]["IMEI"].ToString();
                C_BOMNUMBER = dtwip.Rows[0]["BOMNUMBER"].ToString();

                mst = new Dictionary<string, object>();
                mst.Add("ESN", C_ESN);
                mst.Add("WOID", C_WOID);
                mst.Add("PARTNUMBER", C_PARTNUMBER);
                mst.Add("LOCSTATION", C_LOCSTATION);
                mst.Add("STATIONNAME", C_STATIONNAME);
                mst.Add("WIPSTATION", C_WIPSTATION);
                mst.Add("NEXTSTATION", C_NEXTSTATION);
                mst.Add("USERID", C_USERID);
                mst.Add("RECDATE", C_RECDATE);
                mst.Add("ERRFLAG", C_ERRFLAG);
                mst.Add("SCRAPFLAG", C_SCRAPFLAG);
                mst.Add("SN", C_SSN);
                mst.Add("MAC", C_MAC);
                mst.Add("CARTONNUMBER", C_CARTONNUMBER);
                mst.Add("TRAYNO", C_TRAYNO);
                mst.Add("PALLETNUMBER", C_PALLETNUMBER);
                mst.Add("MCARTONNUMBER", C_MCARTONNUMBER);
                mst.Add("MPALLETNUMBER", C_MPALLETNUMBER);
                mst.Add("LINE", C_LINE);
                mst.Add("ROUTGROUPID", C_ROUTGROUPID);
                mst.Add("STORENUMBER", C_STORENUMBER);
                mst.Add("WEIGHTQTY", C_WEIGHTQTY);
                mst.Add("QA_NO", C_QANO);
                mst.Add("QA_RESULT", C_QARESULT);
                mst.Add("TRACK_NO", C_TRACKNO);
                mst.Add("ATE_STATION_NO", C_ATESTATIONNO);
                mst.Add("TYPE", C_TYPE);
                mst.Add("VERSIONCODE", C_VERSIONCODE);
                mst.Add("SECTIONNAME", C_SECTIONNAME);
                mst.Add("IN_LINE_TIME", C_INLINETIME);
                mst.Add("PRODUCTNAME", C_PRODUCTNAME);
                mst.Add("STATUS", 0);
                mst.Add("LOTIN", C_STORENUMBER);
                mst.Add("STOREHOUSEID", "NA");
                mst.Add("IMEI", C_IMEI);
                mst.Add("BOMNUMBER", C_BOMNUMBER);
                mst.Add("RECDATE1", C_RECDATE);
                dp.AddData(tx, "SFCR.Z_WHS_TRACKING", mst);             

                mst = new Dictionary<string, object>();
                mst.Add("ESN", C_ESN);
                DataTable dt = GetData("SFCR.T_WIP_KEYPART_ONLINE", "ESN,WOID,SNTYPE,SNVAL,STATION,KPNO,RECDATE", mst);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        mst = new Dictionary<string, object>();
                        mst.Add("ESN", dr["ESN"].ToString());
                        mst.Add("WOID", dr["WOID"].ToString());
                        mst.Add("SNTYPE", dr["SNTYPE"].ToString());
                        mst.Add("SNVAL", dr["SNVAL"].ToString());
                        mst.Add("STATION", dr["STATION"].ToString());
                        mst.Add("KPNO", dr["KPNO"].ToString());
                        mst.Add("RECDATE", string.IsNullOrEmpty(dr["RECDATE"].ToString()) ? System.DateTime.Now.ToString() : dr["RECDATE"].ToString());
                        dp.AddData(tx, "SFCR.Z_WHS_KEYPART", mst);
                    }
                }
            
                tx.Commit();
                RES = "OK";
            }
            catch (Exception ex)
            {
                tx.Rollback();
                RES = ex.Message;
                Insert_Exception_Log("PRO_UPDATESTOCKIN " + RES);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return RES;

        }

        public string UPDATE_TR_SN(string R_TRSN, string R_WOID, string R_USERID, string R_STATUS, string R_RMAK1, string R_RMAK2)
        {
            string RES = string.Empty;
          if (DB_Flag==0)
            {
                Dictionary<string, object> mst = null;
                try
                {
                    if (R_WOID == "NA")
                    {
                        if (R_STATUS == "0")
                        {
                            mst = new Dictionary<string, object>();
                            mst.Add("WOID", R_WOID);
                            mst.Add("USER_ID", R_USERID);
                            mst.Add("STATUS", R_STATUS);
                            mst.Add("UPDATE_DATE", System.DateTime.Now);
                        }
                        else
                        {
                            mst = new Dictionary<string, object>();
                            mst.Add("USER_ID", R_USERID);
                            mst.Add("STATUS", R_STATUS);
                            mst.Add("REMARK1", R_RMAK1);
                            mst.Add("REMARK2", R_RMAK2);
                            mst.Add("UPDATE_DATE", System.DateTime.Now);
                        }
                    }
                    else
                    {
                        mst = new Dictionary<string, object>();
                        mst.Add("WOID", R_WOID);
                        mst.Add("USER_ID", R_USERID);
                        mst.Add("STATUS", R_STATUS);
                        mst.Add("REMARK1", R_RMAK1);
                        mst.Add("REMARK2", R_RMAK2);
                        mst.Add("UPDATE_DATE", System.DateTime.Now);
                    }
                    mst.Add("TR_SN", R_TRSN);
                    UpdateData("SFCR.R_TR_SN", new string[] { "TR_SN" }, mst);

                    RES = INSER_TRSN_DETAIL(R_TRSN);
                }
                catch (Exception ex)
                {
                    RES = ex.Message;
                    Insert_Exception_Log("UPDATE_TR_SN " + RES);
                }
            }
          if (DB_Flag == 1)
          {
              try
              {
                  IBaseProvider dp = new BaseProvider();
                  IDictionary<string, object> parms = new Dictionary<string, object>();
                  IDictionary<string, object> procedureOutRes = new Dictionary<string, object>();
                  parms.Add("R_TRSN", R_TRSN);
                  parms.Add("R_WOID", R_WOID);
                  parms.Add("R_USERID", R_USERID);
                  parms.Add("R_STATUS", R_STATUS);
                  parms.Add("R_RMAK1", R_RMAK1);
                  parms.Add("R_RMAK2", R_RMAK2);               
                  procedureOutRes.Add("RES", (object)200);
                  dp.StoreProcedureExec("UPDATE_TR_SN", parms, procedureOutRes);
                  RES=procedureOutRes["RES"].ToString();
              }
              catch (Exception ex)
              {
                  RES = ex.Message;
                  Insert_Exception_Log("UPDATE_TR_SN " + RES);
              }
          }
            return RES;
        }
        public string UPDATE_TR_SN(string Json)
        {
            try
            {
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(Json);
                mst.Add("UPDATE_DATE",System.DateTime.Now);
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                dp.UpdateData("SFCR.R_TR_SN", new string[] { "TR_SN" }, mst);
                return INSER_TRSN_DETAIL(mst["TR_SN"].ToString());
               
            }
            catch (Exception ex)
            {
                Insert_Exception_Log("UPDATE_TR_SN " + ex.Message);
                return ex.Message;

            }

        }

        public string PRO_CONVERTMACIMEI(string WO, string SNTYPE)
        {
            int C_SEQ = 0;
            string RES = "ERROR";
            string C_SNTYPE = SNTYPE;
            string C_SNSTART = string.Empty;
            string C_SNEND = string.Empty;
            string C_STARTPRE = string.Empty;
            string C_ENDPRE = string.Empty;
            string C_TEMP = string.Empty;
            int C_MACTEMPSUFFIX = 0;
            int NUM = 1;
            int N_NUM = 1;
            int M_NUM = 0;
            int C_MACSTARTSUFFIX = 0;
            int C_MACENDSUFFIX = 0;      
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID",WO);
            mst.Add("SNTYPE", SNTYPE);
            DataTable dtSnRule = GetData("SFCR.T_WO_SN_RULE", "WOID,SNSTART,SNEND",mst);//BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dtSnRule.Rows)
            {

                if (SNTYPE == "CSN")
                {
                    C_SNSTART = dr["SNSTART"].ToString();
                    C_SNEND = dr["SNEND"].ToString();
                }
                else
                {
                    try
                    {
                        C_SNSTART = dr["SNSTART"].ToString().Substring(0, 14); //SUBSTR(SNSTART, 1, 14);
                        C_SNEND = dr["SNEND"].ToString().Substring(0, 14);  //SUBSTR(SNEND, 1, 14);
                    }
                    catch
                    {
                        C_SNSTART = dr["SNSTART"].ToString(); //SUBSTR(SNSTART, 1, 14);
                        C_SNEND = dr["SNEND"].ToString();  //SUBSTR(SNEND, 1, 14);
                    }
                }
                while (NUM <= dr["SNEND"].ToString().Length)
                {
                    // SUBSTR(C_SNSTART, NUM, 1) <> SUBSTR(C_SNEND, NUM, 1)
                    if (C_SNSTART.Substring(NUM - 1, 1) != C_SNEND.Substring(NUM - 1, 1))
                    {
                        N_NUM = NUM;
                        break;
                    }
                    NUM = NUM + 1;
                }

                //C_STARTPRE := SUBSTR(C_SNSTART, N_NUM, LENGTH(C_SNSTART) - (N_NUM - 1));
                // C_ENDPRE   := SUBSTR(C_SNEND, N_NUM, LENGTH(C_SNEND) - (N_NUM - 1));

                C_STARTPRE = C_SNSTART.Substring(N_NUM - 1, C_SNSTART.Length - (N_NUM - 1));
                C_ENDPRE = C_SNEND.Substring(N_NUM - 1, C_SNEND.Length - (N_NUM - 1));
                if (SNTYPE == "IMEI" || SNTYPE == "CSN")
                {
                    while (M_NUM <= (Convert.ToInt64(C_ENDPRE) - Convert.ToInt64(C_STARTPRE)))
                    {
                        C_TEMP = C_SNSTART.Substring(0, NUM - 1);
                        C_TEMP = C_TEMP + (Convert.ToInt64(C_STARTPRE) + M_NUM).ToString().PadLeft(C_SNSTART.Length - NUM + 1, '0');
                        C_SEQ = C_SEQ + 1;
                        //cmd = new MySqlCommand();
                        //cmd.CommandText = " INSERT INTO SFCR.T_WO_SNLIST (WOID, SNTYPE, SNVAL, STATUS, SEQ)  VALUES   (@C_WO, @C_SNTYPE, @C_TEMP, '0', @C_SEQ)";
                        //cmd.Parameters.AddRange(new MySqlParameter[]
                        //{ 
                        //      new MySqlParameter("C_WO", MySqlDbType.VarChar) { Value = dr["WOID"].ToString() },
                        //    new MySqlParameter("C_SNTYPE", MySqlDbType.VarChar) { Value = C_SNTYPE },
                        //    new MySqlParameter("C_TEMP", MySqlDbType.VarChar) { Value = C_TEMP },
                        //     new MySqlParameter("C_SEQ", MySqlDbType.Int32) { Value = C_SEQ }
                        //});
                        //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                        mst = new Dictionary<string, object>();
                        mst.Add("WOID", dr["WOID"].ToString());
                        mst.Add("SNTYPE", C_SNTYPE);
                        mst.Add("SNVAL", C_TEMP);
                        mst.Add("STATUS", "0");
                        mst.Add("SEQ", C_SEQ);
                        InsertData("SFCR.T_WO_SNLIST", mst);
                        M_NUM = M_NUM + 1;
                    }
                }
                else
                {
                    if (SNTYPE == "MAC" || SNTYPE == "MEID")
                    {
                        C_MACSTARTSUFFIX = ConvertSixteenToTen(C_STARTPRE);
                        C_MACENDSUFFIX = ConvertSixteenToTen(C_ENDPRE);
                        while (M_NUM <= (C_MACENDSUFFIX - C_MACSTARTSUFFIX))
                        {
                            C_MACTEMPSUFFIX = C_MACSTARTSUFFIX + M_NUM;
                            C_TEMP = ConvertTenToSixteen(C_MACTEMPSUFFIX);
                            C_TEMP = C_SNSTART.Substring(0, NUM - 1) + C_TEMP.PadLeft(C_SNSTART.Length - NUM + 1, '0');
                            C_SEQ = C_SEQ + 1;
                        //    cmd = new MySqlCommand();
                        //    cmd.CommandText = " INSERT INTO SFCR.T_WO_SNLIST (WOID, SNTYPE, SNVAL, STATUS, SEQ)  VALUES   (@C_WO, @C_SNTYPE, @C_TEMP, '0', @C_SEQ)";
                        //    cmd.Parameters.AddRange(new MySqlParameter[]
                        //{ 
                        //      new MySqlParameter("C_WO", MySqlDbType.VarChar) { Value = dr["WOID"].ToString() },
                        //    new MySqlParameter("C_SNTYPE", MySqlDbType.VarChar) { Value = C_SNTYPE },
                        //    new MySqlParameter("C_TEMP", MySqlDbType.VarChar) { Value = C_TEMP },
                        //     new MySqlParameter("C_SEQ", MySqlDbType.Int32) { Value = C_SEQ }
                        //});
                        //    BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                            mst = new Dictionary<string, object>();
                            mst.Add("WOID", dr["WOID"].ToString());
                            mst.Add("SNTYPE", C_SNTYPE);
                            mst.Add("SNVAL", C_TEMP);
                            mst.Add("STATUS", "0");
                            mst.Add("SEQ", C_SEQ);
                            InsertData("SFCR.T_WO_SNLIST", mst);
                            M_NUM = M_NUM + 1;
                        }
                    }
                }
            }
            RES = "OK";


            return RES;
        }


        private string INSERT_REPAIR_INFO(string EC, string ESN, string C_WO, string C_MODEL, string MYGROUP, string C_EMP, string LINE, string C_CLASSDATE, string C_CLASS, string C_WORKSECTION)
        {
            try
            {
                Dictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("ERRORCODE", EC);
                mst.Add("ESN", ESN);
                mst.Add("WOID", C_WO);
                mst.Add("PARTNUMBER", C_MODEL);
                mst.Add("CRAFTID", MYGROUP);
                mst.Add("INPUTUSER", C_EMP);
                mst.Add("STATUS", "0");
                mst.Add("INPUTDATE", System.DateTime.Now);
                mst.Add("LINEID", LINE);
                mst.Add("TCLASSDATE", C_CLASSDATE);
                mst.Add("TCLASS", C_CLASS);
                mst.Add("TWORKSECTION", C_WORKSECTION);
                InsertData("SFCR.T_REPAIR_INFO", mst);
                return "OK";
            }
            catch (Exception ex)
            {
                Insert_Exception_Log("INSERT_REPAIR_INFO " + ex.Message);
                return ex.Message;
            }
        }
        private string PRO_INPUT_SN_FIRST(IDictionary<string, object> Dic)
        {
            try
            {
               return PRO_INPUT_SN_FIRST(Dic["DATA"].ToString(), Dic["MYGROUP"].ToString(), Dic["SECTION_NAME"].ToString(), Dic["STATION_NAME"].ToString(), Dic["EMP"].ToString(), Dic["EC"].ToString(), Dic["LINE"].ToString(), Dic["WO"].ToString());
            }
            catch
            {
                return PRO_INPUT_SN_FIRST(Dic["DATA"].ToString(), Dic["MYGROUP"].ToString(), "NA", Dic["MYGROUP"].ToString(), Dic["EMP"].ToString(), Dic["EC"].ToString(), Dic["LINE"].ToString(), Dic["WO"].ToString());
            }
        }
        public string PRO_INPUT_SN_FIRST(string DATA, string MYGROUP, string SECTION_NAME, string STATION_NAME, string EMP, string EC, string LINE, string WO)
        {
            lock (thisLock)
            {
                string C_WORKSECTION = string.Empty;
                string C_CLASS = string.Empty;
                string C_DAY = string.Empty;
                string C_DATE1 = string.Empty;
                string C_DATE2 = string.Empty;
                string C_WORKDATE = string.Empty;
                string C_RES = string.Empty;
                string C_CLASSDATE = string.Empty;
                string C_EC = string.Empty;
                string RES = string.Empty;
                string C_EMP = string.Empty;
                string C_MODEL = string.Empty;
                string C_MODELDESC = string.Empty;
                string C_ROUTECODE = string.Empty;
                string C_ENDGROUP = string.Empty;
                string C_STARTGROUP = string.Empty;
                string C_WO = string.Empty;
                string C_LOCSTATION = string.Empty;
                string C_NEXTSTATION = string.Empty;
                string C_ERRFLAG = string.Empty;
                try
                {
                    PRO_GETWORKCLASS(out C_WORKSECTION, out C_CLASS, out C_DAY, out C_DATE1, out C_DATE2, out C_RES);
                    if (C_RES != "OK")
                        throw new Exception(C_RES);
                    //if (C_DAY == "TOMORROW")
                    //    C_CLASSDATE = DateTime.Now.AddDays(-1).ToString("yyyyMMdd"); //  TO_CHAR(SYSDATE - 1, 'YYYYMMDD');
                    //else
                    //    C_CLASSDATE = DateTime.Now.ToString("yyyyMMdd"); //TO_CHAR(SYSDATE, 'YYYYMMDD'); --C_COUNT=COUNT(*)
                    C_WORKDATE = C_DATE1;
                    if (C_DAY == "TOMORROW")
                    {
                        C_CLASSDATE = C_DATE2;
                        if (C_WORKSECTION == "23")
                            C_WORKDATE = C_DATE2;
                    }
                    else
                        C_CLASSDATE = C_WORKDATE;

                    C_EC = string.IsNullOrEmpty(EC) ? "NA" : EC;

                    PRO_GETEMPNO(EMP, out C_RES, out C_EMP);
                    if (C_RES != "OK")
                        throw new Exception(C_RES);

                    DataTable dtwip = twip.Get_WIP_TRACKING("ESN", DATA, "WOID,PARTNUMBER,LOCSTATION,NEXTSTATION,ERRFLAG,ROUTGROUPID").Tables[0];
                    if (dtwip.Rows.Count == 0)
                    {
                        C_RES = PRO_SN_INPUT_WIPFIRST_NEW(DATA, MYGROUP, LINE, WO, C_EMP, out C_MODEL, out C_MODELDESC, out C_ROUTECODE, out C_ENDGROUP);
                        C_WO = WO;
                        if (C_RES == "OK")
                        {
                            if (EC == "NA")
                            {
                                C_RES = PRO_CHECK_ROUTE(DATA, MYGROUP);
                                if (C_RES == "OK")
                                {
                                    PRO_STNREC(LINE, MYGROUP, C_MODEL, C_MODELDESC, C_WO, DATA, 0, out C_RES);

                                    if (C_RES == "OK")
                                    {
                                        PRO_UPDATEWIPSTATION_NEW(LINE, MYGROUP, SECTION_NAME, DATA, C_EMP, 0, C_ROUTECODE, C_WO, "NA", STATION_NAME, C_MODEL, out C_RES);
                                        RES = C_RES;
                                    }
                                    else
                                    {
                                        RES = C_RES;
                                    }
                                }
                                else
                                {
                                    RES = C_RES;
                                }
                            }
                            else
                            {
                                C_RES = PRO_CHECK_ROUTE_RE(DATA, MYGROUP);
                                if (C_RES == "OK")
                                {
                                    PRO_STNREC(LINE, MYGROUP, C_MODEL, C_MODELDESC, C_WO, DATA, 1, out C_RES);
                                    if (C_RES == "OK")
                                    {
                                        PRO_UPDATEWIPSTATION_NEW(LINE, MYGROUP, SECTION_NAME, DATA, C_EMP, 1, C_ROUTECODE, C_WO, "NA", STATION_NAME, C_MODEL, out C_RES);
                                        if (C_RES == "OK")
                                        {
                                            C_RES = INSERT_REPAIR_INFO(EC, DATA, C_WO, C_MODEL, MYGROUP, C_EMP, LINE, C_CLASSDATE, C_CLASS, C_WORKSECTION);
                                            RES = C_RES;
                                        }
                                        else
                                        {
                                            RES = C_RES;
                                        }
                                    }
                                    else
                                    {
                                        RES = C_RES;
                                    }
                                }
                                else
                                {
                                    RES = C_RES;
                                }

                            }
                        }
                        else
                        {
                            RES = C_RES;
                        }
                    }
                    else
                    {
                        C_WO = dtwip.Rows[0]["WOID"].ToString();
                        C_MODEL = dtwip.Rows[0]["PARTNUMBER"].ToString();
                        C_LOCSTATION = dtwip.Rows[0]["LOCSTATION"].ToString();
                        C_NEXTSTATION = dtwip.Rows[0]["NEXTSTATION"].ToString();
                        C_ERRFLAG = dtwip.Rows[0]["ERRFLAG"].ToString();
                        C_ROUTECODE = dtwip.Rows[0]["ROUTGROUPID"].ToString();
                        if (C_ERRFLAG != "0")
                            throw new Exception("SN IN REPAIR");
                        DataTable dtwoinfo = woinfo.GetWoInfo(C_WO, null, "INPUTGROUP,OUTPUTGROUP").Tables[0];
                        if (dtwoinfo.Rows.Count == 0)
                            throw new Exception("NO FOUND WORKORDER");
                        C_STARTGROUP = dtwoinfo.Rows[0]["INPUTGROUP"].ToString();
                        C_ENDGROUP = dtwoinfo.Rows[0]["OUTPUTGROUP"].ToString();
                        if (MYGROUP == C_STARTGROUP)
                        {
                            if (EC == "NA")
                            {
                                C_RES = PRO_CHECK_ROUTE(DATA, MYGROUP);
                                if (C_RES == "OK")
                                {
                                    PRO_STNREC(LINE, MYGROUP, C_MODEL, C_MODELDESC, C_WO, DATA, 0, out C_RES);
                                    if (C_RES == "OK")
                                    {
                                        PRO_UPDATEWIPSTATION_NEW(LINE, MYGROUP, SECTION_NAME, DATA, C_EMP, 0, C_ROUTECODE, C_WO, "NA", STATION_NAME, C_MODEL, out C_RES);
                                        RES = C_RES;
                                    }
                                    else
                                    {
                                        RES = C_RES;
                                    }
                                }
                                else
                                {
                                    RES = C_RES;
                                }
                            }
                            else
                            {
                                C_RES = PRO_CHECK_ROUTE_RE(DATA, MYGROUP);
                                if (C_RES == "OK")
                                {
                                    PRO_STNREC(LINE, MYGROUP, C_MODEL, C_MODELDESC, C_WO, DATA, 1, out C_RES);
                                    if (C_RES == "OK")
                                    {
                                        PRO_UPDATEWIPSTATION_NEW(LINE, MYGROUP, SECTION_NAME, DATA, C_EMP, 1, C_ROUTECODE, C_WO, "NA", STATION_NAME, C_MODEL, out C_RES);
                                        if (C_RES == "OK")
                                        {
                                            C_RES = INSERT_REPAIR_INFO(EC, DATA, C_WO, C_MODEL, MYGROUP, C_EMP, LINE, C_CLASSDATE, C_CLASS, C_WORKSECTION);
                                            RES = C_RES;
                                        }
                                        else
                                        {
                                            RES = C_RES;
                                        }
                                    }
                                    else
                                    {
                                        RES = C_RES;
                                    }

                                }
                                else
                                {
                                    RES = C_RES;
                                }

                            }

                        }
                        else
                        {
                            RES = "GROUP ERROR ";
                        }

                    }

                }
                catch (Exception ex)
                {
                    RES = ex.Message;
                    Insert_Exception_Log("PRO_INPUT_SN_FIRST " + ex.Message);
                }
                Insert_DB_Log(string.Format("PRO_INPUT_SN_FIRST DATA:{0}  {1} {2}", DATA, WO, RES));
                return RES;
            }
        }

        public void PRO_STNREC(string LINE, string MYGROUP, string MODEL, string MODELDESC, string C_WO, string ESN, int FLAG, out string RES)
        {
            myWatch.Restart();
            string C_ESN = ESN;
            string C_RES = string.Empty;
            int C_COUNT;
            string C_INPUTGROUP = string.Empty;
            try
            {             
               
                string fieldlist = "COUNT(1)";
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("WOID", C_WO);
                mst.Add("ESN", C_ESN);
                mst.Add("LOCSTATION", MYGROUP);               
                DataSet ds = dp.GetData("SFCR.T_WIP_DETAIL_A", fieldlist, mst, out count);

                C_COUNT = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                if (C_COUNT > 0)
                {
                    C_RES = "REINSERTSTATION ERROR";
                    PRO_REINSERTSTATIONREC(MYGROUP, MODEL, MODELDESC, LINE, C_WO, FLAG, out C_RES);
                    RES = C_RES;
                }
                else
                {
                    C_RES = "INSERTSTATION ERROR";
                    PRO_INSERTSTATIONREC(MYGROUP, MODEL, MODELDESC, LINE, C_WO, FLAG, out C_RES);
                    if (C_RES == "OK")
                    {
                        if (C_COUNT == 0)
                            PRO_UPDATE_WOINFO(C_WO, MYGROUP, 1);
                        else
                        {
                            DataTable dtwoinfo = woinfo.GetWoInfo(C_WO, null, "INPUTGROUP").Tables[0];
                            C_INPUTGROUP = dtwoinfo.Rows[0]["INPUTGROUP"].ToString();
                            if (C_INPUTGROUP == MYGROUP)
                                PRO_UPDATE_WOINFO(C_WO, MYGROUP, 1);
                        }
                        RES = "OK";
                    }
                    else
                    {
                        RES = C_RES;
                    }
                }

                myWatch.Stop();
                Insert_DB_Log(string.Format("PRO_STNREC-->{2}  ESN:{0},{3},{1} ", ESN, RES, myWatch.ElapsedMilliseconds.ToString(), MYGROUP));
            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log(string.Format("PRO_STNREC ESN:{0},{1},{2}", ESN,MYGROUP, ex.Message));
            }
           

        }

        /// <summary>
        /// 20151006 修改产能记录到t_station_recount_temp
        /// </summary>
        /// <param name="MYGROUP"></param>
        /// <param name="MODEL"></param>
        /// <param name="MODELDESC"></param>
        /// <param name="LINE"></param>
        /// <param name="WO"></param>
        /// <param name="FLAG"></param>
        /// <param name="RES"></param>
        public void PRO_REINSERTSTATIONREC(string MYGROUP, string MODEL, string MODELDESC, string LINE, string WO, int FLAG, out string RES)
        {
            string C_WORKSECTION = string.Empty;
            string C_CLASS = string.Empty;
            string C_DAY = string.Empty;
            string C_DATE1 = string.Empty;
            string C_DATE2 = string.Empty;      
            string C_RES = string.Empty;
            string C_WORKDATE = string.Empty;
            string C_CLASSDATE = string.Empty;
            string C_ROWID = string.Empty;
            int C_COUNT=0;
            int C_RPASSQTY;
            int C_RFAILQTY;
           
            try
            {
                PRO_GETWORKCLASS(out C_WORKSECTION, out C_CLASS, out C_DAY, out C_DATE1, out C_DATE2, out C_RES);
                if (C_RES == "OK")
                {
                    //C_WORKDATE = System.DateTime.Now.ToString("yyyyMMdd");
                    //if (C_DAY == "TOMORROW")
                    //{
                    //    C_CLASSDATE = System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                    //    if (C_WORKSECTION == "23")
                    //        C_WORKDATE = System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                    //}
                    //else
                    //{
                    //    C_CLASSDATE = C_WORKDATE;
                    //}
                    C_WORKDATE = C_DATE1;
                    if (C_DAY == "TOMORROW")
                    {
                        C_CLASSDATE = C_DATE2;
                        if (C_WORKSECTION == "23")
                            C_WORKDATE = C_DATE2;
                    }
                    else
                        C_CLASSDATE = C_WORKDATE;

                    DataSet ds = tStaR.GET_STN_REC_2(C_WORKDATE, C_WORKSECTION, WO, MYGROUP, LINE);
                    if (ds.Tables[0].Rows.Count > 0)
                        C_COUNT = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                  
                    if (C_COUNT > 0)
                    {
                        if (FLAG == 0)
                        {                        
                           C_RES= tStaR.update_station_recout(ds.Tables[0].Rows[0]["ROWID"].ToString(),WO, MYGROUP, C_WORKDATE, C_WORKSECTION, LINE, 0, 0, 1, 0, "I");
                          //  C_RES = tStaR.Insert_StnRec_Temp(WO, MYGROUP, C_WORKDATE, MODEL, C_WORKSECTION, C_CLASS, C_CLASSDATE, LINE, 0, 0, 1, 0, "I");
                        }
                        else
                        {
                         C_RES=  tStaR.update_station_recout(ds.Tables[0].Rows[0]["ROWID"].ToString(),WO, MYGROUP, C_WORKDATE, C_WORKSECTION, LINE, 0, 0, 0, 1, "I");
                         //   C_RES = tStaR.Insert_StnRec_Temp(WO, MYGROUP, C_WORKDATE, MODEL, C_WORKSECTION, C_CLASS, C_CLASSDATE, LINE, 0, 0, 0, 1, "I");
                        }
                    }
                    else
                    {
                        if (FLAG == 0)
                        {
                            C_RPASSQTY = 1;
                            C_RFAILQTY = 0;
                        }
                        else
                        {
                            C_RPASSQTY = 0;
                            C_RFAILQTY = 1;
                        }

                      //  C_RES = tStaR.Insert_StnRec_Temp(WO, MYGROUP, C_WORKDATE, MODEL, C_WORKSECTION, C_CLASS, C_CLASSDATE, LINE, 0, 0, C_RPASSQTY, C_RFAILQTY, "I");
                        C_RES=  tStaR.insert_station_recount(WO, MYGROUP, C_WORKDATE, MODEL, C_WORKSECTION, C_CLASS, C_CLASSDATE, LINE, 0, 0, C_RPASSQTY, C_RFAILQTY, "I");

                    }
                    RES = C_RES;
                }
                else
                    throw new Exception(C_RES);
            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log("PRO_REINSERTSTATIONREC " + ex.Message);
            }
        }
        private string PRO_TEST_MAIN_ONLY(IDictionary<string, object> Dic)
        {          
            string C_RES = string.Empty;
            try
            {
                PRO_TEST_MAIN_ONLY(Dic["DATA"].ToString(), Dic["MYGROUP"].ToString(), Dic["SECTION_NAME"].ToString(), Dic["STATION_NAME"].ToString(), Dic["EMP"].ToString(), Dic["EC"].ToString(), Dic["LINE"].ToString(), out C_RES);
            }
            catch
            {
                PRO_TEST_MAIN_ONLY(Dic["DATA"].ToString(), Dic["MYGROUP"].ToString(), "NA", Dic["MYGROUP"].ToString() , Dic["EMP"].ToString(), Dic["EC"].ToString(), Dic["LINE"].ToString(), out C_RES);
            }
               return C_RES;
        }      
        public void PRO_TEST_MAIN_ONLY(string DATA, string MYGROUP, string SECTION_NAME, string STATION_NAME, string EMP, string EC, string LINE, out string RES)
        {
            System.Diagnostics.Stopwatch myWatch_Main = System.Diagnostics.Stopwatch.StartNew();
            string C_WO = string.Empty;
            int C_FLAG;
            string C_RES = string.Empty;
            string C_MODEL = string.Empty;
            string C_MODELDESC = string.Empty;
            string C_EMP = string.Empty;
            string C_CLASSDATE = string.Empty;
            string C_CLASS = string.Empty;
            string C_WORKSECTION = string.Empty;
            string C_DAY = string.Empty;
            string C_DATE1 = string.Empty;
            string C_DATE2 = string.Empty;
            string C_WORKDATE = string.Empty;
            string C_LOCGROUP = string.Empty;
            string C_NEXTSTATION = string.Empty;
            string C_ERRFLAG = string.Empty;
            string C_ROUTECODEID = string.Empty;
            string C_ENDGROUP = string.Empty;

            try
            {
                RES = "EXCPTION";
                PRO_GETWORKCLASS(out C_WORKSECTION, out C_CLASS, out C_DAY, out C_DATE1, out C_DATE2, out C_RES);
                if (C_RES != "OK")
                    throw new Exception(C_RES);
                else
                {
                    //if (C_DAY == "TOMORROW")
                    //    C_CLASSDATE = System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                    //else
                    //    C_CLASSDATE = System.DateTime.Now.ToString("yyyyMMdd");
                    C_WORKDATE = C_DATE1;
                    if (C_DAY == "TOMORROW")
                    {
                        C_CLASSDATE = C_DATE2;
                        if (C_WORKSECTION == "23")
                            C_WORKDATE = C_DATE2;
                    }
                    else
                        C_CLASSDATE = C_WORKDATE;
                }
                PRO_GETEMPNO(EMP, out C_RES, out C_EMP);
                if (C_RES != "OK")
                    throw new Exception(C_RES);
                DataTable dt = twip.Get_WIP_TRACKING("ESN", DATA, "WOID,PARTNUMBER,PRODUCTNAME,LOCSTATION,NEXTSTATION,ERRFLAG,ROUTGROUPID").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    C_WO = dt.Rows[0]["WOID"].ToString();
                    C_MODEL = dt.Rows[0]["PARTNUMBER"].ToString();
                    C_MODELDESC = dt.Rows[0]["PRODUCTNAME"].ToString();
                    C_LOCGROUP = dt.Rows[0]["LOCSTATION"].ToString();
                    C_NEXTSTATION = dt.Rows[0]["NEXTSTATION"].ToString();
                    C_ERRFLAG = dt.Rows[0]["ERRFLAG"].ToString();
                    C_ROUTECODEID = dt.Rows[0]["ROUTGROUPID"].ToString();
                }
                else
                {
                    throw new Exception("NO SN FOUNT");
                }

                DataTable dt_B = woinfo.GetWoInfo(C_WO, null, "WOSTATE,OUTPUTGROUP").Tables[0];
                if (dt_B.Rows.Count > 0)
                {
                    C_FLAG = Convert.ToInt32(dt_B.Rows[0]["WOSTATE"].ToString());
                    C_ENDGROUP = dt_B.Rows[0]["OUTPUTGROUP"].ToString();
                }
                else
                {
                    throw new Exception("NO WO FOUNT");
                }
                if (C_FLAG == 1 || C_FLAG == 2)
                {
                    if (EC == "NA")
                    {
                        C_RES = PRO_CHECK_ROUTE(DATA, MYGROUP);
                        if (C_RES == "OK")
                        {
                            PRO_STNREC(LINE, MYGROUP, C_MODEL, C_MODELDESC, C_WO, DATA, 0, out C_RES);
                            if (C_RES == "OK")
                            {
                                PRO_UPDATEWIPSTATION_NEW(LINE, MYGROUP, "NA", DATA, C_EMP, 0, C_ROUTECODEID, C_WO, SECTION_NAME, STATION_NAME, C_MODEL, out C_RES);
                                RES = C_RES;
                            }
                            else
                                throw new Exception(C_RES);
                        }
                        else
                            throw new Exception(C_RES);
                    }
                    else
                    {
                        C_RES = PRO_CHECK_ROUTE_RE(DATA, MYGROUP);
                        if (C_RES == "OK")
                        {
                            PRO_STNREC(LINE, MYGROUP, C_MODEL, C_MODELDESC, C_WO, DATA, 1, out C_RES);
                            if (C_RES == "OK")
                            {
                                PRO_UPDATEWIPSTATION(LINE, MYGROUP, SECTION_NAME, DATA, C_WO, C_EMP, "1", STATION_NAME, out C_RES);
                                if (C_RES == "OK")
                                {
                                    INSERT_REPAIR_INFO(EC, DATA, C_WO, C_MODEL, MYGROUP, C_EMP, LINE, C_CLASSDATE, C_CLASS, C_WORKSECTION);
                                    RES = "OK";
                                }
                                else
                                {
                                    RES = C_RES;
                                }
                            }
                            else
                                throw new Exception(C_RES);
                        }
                        else
                            throw new Exception(C_RES);
                    }

                }
                else
                {
                    switch (C_FLAG)
                    {
                        case 0:
                            RES = "Waiting Relaese";
                            break;
                        case 1:
                            RES = "OK";
                            break;
                        case 2:
                            RES = "OK";
                            break;
                        case 3:
                            RES = "WO IS CLOSED ";
                            break;
                        case 4:
                            RES = "WO HOLD";
                            break;
                        default:
                            RES = "WO STATUS ERROR";
                            break;
                    }
                }
       
            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log("PRO_TEST_MAIN_ONLY " + DATA + " " + ex.Message);
            }
            finally
            {
                myWatch_Main.Stop();
                Insert_DB_Log("PRO_TEST_MAIN_ONLY " + DATA + " -->" + myWatch_Main.ElapsedMilliseconds.ToString());
            }
        }
        public void PRO_TEST_STOCKIN(string DATA, string MYGROUP, string STATION_NAME, string EMP, string EC, string LINE, out string RES)
        {
            string C_WO = string.Empty;
            int C_FLAG;
            string C_RES = string.Empty;
            string C_MODEL = string.Empty;
            string C_MODELDESC = string.Empty;
            string C_EMP = string.Empty;
            string C_ROUTECODE = string.Empty;
            string C_ERRFLAG = string.Empty;
            string C_LOCGROUP = string.Empty;
            string C_NEXTSTATION = string.Empty;
            string C_ENDGROUP = string.Empty;
         
            try
            {
                RES = "EXCPTION";
                PRO_GETEMPNO(EMP, out C_RES, out C_EMP);
                if (C_RES != "OK")
                    throw new Exception(C_RES);
                DataTable dt = twip.Get_WIP_TRACKING("ESN", DATA, "WOID,PARTNUMBER,PRODUCTNAME,ROUTGROUPID,ERRFLAG,LOCSTATION,NEXTSTATION").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    C_WO = dt.Rows[0]["WOID"].ToString();
                    C_MODEL = dt.Rows[0]["PARTNUMBER"].ToString();
                    C_MODELDESC = dt.Rows[0]["PRODUCTNAME"].ToString();
                    C_ROUTECODE = dt.Rows[0]["ROUTGROUPID"].ToString();
                    C_ERRFLAG = dt.Rows[0]["ERRFLAG"].ToString();
                    C_LOCGROUP = dt.Rows[0]["LOCSTATION"].ToString();
                    C_NEXTSTATION = dt.Rows[0]["NEXTSTATION"].ToString();
                }
                else
                {
                    throw new Exception("NO SN FOUNT");
                }
                DataTable dt_B = woinfo.GetWoInfo(C_WO, null, "WOSTATE,OUTPUTGROUP").Tables[0];
                if (dt_B.Rows.Count > 0)
                {
                    C_FLAG = Convert.ToInt32(dt_B.Rows[0]["WOSTATE"].ToString());
                    C_ENDGROUP = dt_B.Rows[0]["OUTPUTGROUP"].ToString();
                }
                else
                {
                    throw new Exception("NO WO FOUNT");
                }
                if (C_FLAG == 1 || C_FLAG==2 )
                {
                    if (EC == "NA")
                    {
                        C_RES = PRO_CHECK_ROUTE(DATA, MYGROUP);
                        if (C_RES == "OK")
                        {
                            PRO_STNREC(LINE, MYGROUP, C_MODEL, C_MODELDESC, C_WO, DATA, 0, out C_RES);
                            if (C_RES == "OK")
                            {
                                C_RES = PRO_UPDATESTOCKIN(LINE, MYGROUP, STATION_NAME, DATA, C_EMP, "0");
                                if (C_RES == "OK")
                                    RES = "OK";
                                else
                                    throw new Exception(C_RES);
                            }
                            else
                                throw new Exception(C_RES);
                        }
                        else
                            throw new Exception(C_RES);
                    }
                    else
                        throw new Exception("NO REPAIR");
                }
                else
                {
                    switch (C_FLAG)
                    {
                        case 0:
                            RES = "Waiting Relaese";
                            break;
                        case 1:
                            RES = "OK";
                            break;
                        case 2:
                            RES = "OK";
                            break;
                        case 3:
                            RES = "WO IS CLOSED ";
                            break;
                        case 4:
                            RES = "WO HOLD";
                            break;
                        default:
                            RES = "WO STATUS ERROR";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log("PRO_TEST_STOCKIN " + ex.Message);
            }
        }
        public List<string> INS_ATE_SN_BACK(IDictionary<string, object> Dic)
        {
            List<string> C_RES = new List<string>();
            string RES = string.Empty;
            string COMMAND = string.Empty;          
            try
            {
                INS_ATE_SN_BACK(Dic["DATA"].ToString(), Dic["MYGROUP"].ToString(), Dic["SECTION_NAME"].ToString(), Dic["STATION_NAME"].ToString(), Dic["EMP"].ToString(), Dic["LINE"].ToString(), out RES, out COMMAND);
            }
            catch
            {
                INS_ATE_SN_BACK(Dic["DATA"].ToString(), Dic["MYGROUP"].ToString(), Dic["EMP"].ToString(), Dic["LINE"].ToString(), out RES, out COMMAND);
            }
            C_RES.Add(RES);
            C_RES.Add(RES);
            return C_RES;
        }
        public void INS_ATE_SN_BACK(string DATA, string MYGROUP,  string EMP, string LINE, out string RES, out string COMMAND)
        {
            INS_ATE_SN_BACK(DATA, MYGROUP,"NA",MYGROUP+"2", EMP, LINE, out RES, out COMMAND);
        }
        public void INS_ATE_SN_BACK(string DATA, string MYGROUP,string SECTION_NAME,string STATION_NAME, string EMP, string LINE, out string RES, out string COMMAND)
        {
            string C_WO = string.Empty;
            int C_FLAG;
            string C_RES = string.Empty;
            string C_MODEL = string.Empty;
            string C_MODELDESC = string.Empty;
            string C_EMP = string.Empty;
            string C_ESN = (DATA.Substring(0, 25)).Trim();
            string C_EC = (DATA.Substring(25, 8)).Trim();
            string C_MACH = (DATA.Substring(33, 20)).Trim();
            string C_CLASSDATE = string.Empty;
            string C_CLASS = string.Empty;
            string C_WORKSECTION = string.Empty;
            string C_DAY = string.Empty;
            string C_DATE1 = string.Empty;
            string C_DATE2 = string.Empty;
            string C_WORKDATE = string.Empty;
            string ERR_RES = string.Empty;
            string C_ROUTECODEID = string.Empty;
            string C_ERRFLAG = string.Empty;
            string C_LOCGROUP = string.Empty;
            string C_NEXTSTATION = string.Empty;
            string C_ENDGROUP = string.Empty;
            string C_DATA = " " + C_ESN;
            C_DATA = C_DATA.Substring(C_DATA.Length - 25, 25);
            string C_OUT_RS232 = C_DATA + "ERRO";
            try
            {
                if (string.IsNullOrEmpty(C_EC))
                {
                    C_EC = "NA";
                }
                PRO_GETWORKCLASS(out C_WORKSECTION, out C_CLASS, out C_DAY, out C_DATE1, out C_DATE2, out C_RES);
                if (C_RES != "OK")
                    throw new Exception(C_RES);              
                
                    //if (C_DAY == "TOMORROW")
                    //    C_CLASSDATE = System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                    //else
                    //    C_CLASSDATE = System.DateTime.Now.ToString("yyyyMMdd");
                    C_WORKDATE = C_DATE1;
                if (C_DAY == "TOMORROW")
                {
                    C_CLASSDATE = C_DATE2;
                    if (C_WORKSECTION == "23")
                        C_WORKDATE = C_DATE2;
                }
                else
                    C_CLASSDATE = C_WORKDATE;

                if (C_EC == "NA" || string.IsNullOrEmpty(C_EC))
                {
                    C_RES = PRO_CHECKEC(C_EC);
                    if (C_RES != "OK")
                        throw new Exception(C_RES);
                }
                PRO_GETEMPNO(EMP, out C_RES, out C_EMP);
                if (C_RES != "OK")
                    throw new Exception(C_RES);

                C_RES = "NO SN";
                DataTable dt = twip.Get_WIP_TRACKING("ESN", C_ESN, "WOID,PARTNUMBER,PRODUCTNAME,LOCSTATION,NEXTSTATION,ERRFLAG,ROUTGROUPID").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    C_WO = dt.Rows[0]["WOID"].ToString();
                    C_MODEL = dt.Rows[0]["PARTNUMBER"].ToString();
                    C_MODELDESC = dt.Rows[0]["PRODUCTNAME"].ToString();
                    C_LOCGROUP = dt.Rows[0]["LOCSTATION"].ToString();
                    C_NEXTSTATION = dt.Rows[0]["NEXTSTATION"].ToString();
                    C_ERRFLAG = dt.Rows[0]["ERRFLAG"].ToString();
                    C_ROUTECODEID = dt.Rows[0]["ROUTGROUPID"].ToString();
                }
                else
                {
                    throw new Exception("C_RES");
                }

                C_RES = "WO NOT FOUND";
                DataTable dt_B = woinfo.GetWoInfo(C_WO, null, "WOSTATE,OUTPUTGROUP").Tables[0];
                if (dt_B.Rows.Count > 0)
                {
                    C_FLAG = Convert.ToInt32(dt_B.Rows[0]["WOSTATE"].ToString());
                    C_ENDGROUP = dt_B.Rows[0]["OUTPUTGROUP"].ToString();
                }
                else
                {
                    throw new Exception("NO WO FOUNT");
                }
                if (C_FLAG == 1 || C_FLAG == 2)
                {
                    if (C_EC == "NA")
                    {
                        C_RES = PRO_CHECK_ROUTE(DATA, MYGROUP);
                        if (C_RES == "OK")
                        {
                            PRO_STNREC(LINE, MYGROUP, C_MODEL, C_MODELDESC, C_WO, DATA, 0, out C_RES);
                            if (C_RES == "OK")
                            {
                                PRO_UPDATEWIPSTATION_NEW(LINE, MYGROUP, SECTION_NAME, C_ESN, C_EMP, 0, C_ROUTECODEID, C_WO, "NA",STATION_NAME, C_MODEL, out C_RES);
                                if (C_RES != "OK")
                                    throw new Exception(C_RES);
                                else
                                    RES = C_RES;
                                C_OUT_RS232 = C_DATA + "PASS";
                            }
                            else
                                throw new Exception(C_RES);
                        }
                        else
                            throw new Exception(C_RES);
                    }
                    else
                    {
                        C_RES = PRO_CHECK_ROUTE(DATA, MYGROUP);
                        if (C_RES == "OK")
                        {
                            PRO_STNREC(LINE, MYGROUP, C_MODEL, C_MODELDESC, C_WO, DATA, 1, out C_RES);
                            if (C_RES == "OK")
                            {
                                PRO_UPDATEWIPSTATION_NEW(LINE, MYGROUP, SECTION_NAME, C_ESN, C_EMP, 1, C_ROUTECODEID, C_WO, "NA",STATION_NAME, C_MODEL, out C_RES);
                                if (C_RES != "OK")
                                    throw new Exception(C_RES);
                                INSERT_REPAIR_INFO(C_EC, C_ESN, C_WO, C_MODEL, MYGROUP, C_EMP, LINE, C_CLASSDATE, C_CLASS, C_WORKSECTION);
                                RES = "OK";
                                C_OUT_RS232 = C_DATA + "PASS";
                            }
                            else
                                throw new Exception(C_RES);
                        }
                        else
                            throw new Exception(C_RES);
                    }

                }
                else
                {
                    switch (C_FLAG)
                    {
                        case 0:
                            RES = " Waiting Relaese";
                            break;
                        case 1:
                            RES = " OK";
                            break;
                        case 2:
                            RES = " OK";
                            break;
                        case 3:
                            RES = " WO IS CLOSED ";
                            break;
                        case 4:
                            RES = " WO HOLD";
                            break;
                        default:
                            RES = " WO STATU ERROR";
                            break;
                    }
                }
                COMMAND = C_OUT_RS232 + "\r\n";
            }
            catch (Exception ex)
            {
                RES = ex.Message;
                COMMAND = C_OUT_RS232 + "\r\n";
                Insert_Exception_Log("INS_ATE_SN_BACK " + ex.Message);
            }
        }
        public void PRO_INSERTCARTONINFO(string CARTONID, string ESN, string LINEID, string WOID, string MCARTONNUMBER, string SN, string MAC, string COMPUTER, out string RES)
        {
            int C_CARTONQTY = 0;
            int C_NUM;
            int TNUM;
            int INUM;
            string C_FLAG = string.Empty;
            string C_PARTNUMBER = string.Empty;
            string NEXTCARTONID = string.Empty;
            string C_CARTONID = CARTONID;
            string C_WOID = WOID;
            string C_RES = string.Empty;
            string C_ESN = ESN;
            string C_MCARTONNUMBER = MCARTONNUMBER;
            string C_LINEID = LINEID;
            string C_SN = SN;
            string C_MAC = MAC;
  
            Dictionary<string, object> mst = null;
            try
            {
           
                mst = new Dictionary<string,object>();
                mst.Add("CARTONID",C_CARTONID);
                DataTable dt = GetData("SFCR.T_CARTON_INFO_HAD", "NUM, FLAG", mst);//BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    C_NUM = Convert.ToInt32(dt.Rows[0]["NUM"].ToString());
                    C_FLAG = dt.Rows[0]["FLAG"].ToString();
                }
                else
                {
                    C_NUM = 0;
                    C_FLAG = "0";
                }
                INUM = C_NUM;

                C_RES = "PACK PARAM NOT DEFINE -" + C_WOID;
            
                int count = 0;
                string table = "SFCR.T_WO_INFO W, SFCB.B_PACK_PARAMETERS P";
                string fieldlist = "W.PARTNUMBER, P.CARTONQTY";
                string filter = "P.PARTNUMBER = W.PARTNUMBER AND W.WOID = {0}";              
                mst = new Dictionary<string, object>();
                mst.Add("WOID", C_WOID);
                DataTable dt_B = TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count).Tables[0];
                if (dt_B.Rows.Count > 0)
                {
                    C_PARTNUMBER = dt_B.Rows[0]["PARTNUMBER"].ToString();
                    C_CARTONQTY = Convert.ToInt32(dt_B.Rows[0]["CARTONQTY"].ToString());
                }
                if (C_NUM >= C_CARTONQTY || C_FLAG == "1")
                {
                    throw new Exception("BOX IS CLOSE");
                }
                if ((C_NUM + 1) >= C_CARTONQTY)
                {
                    C_FLAG = "1";
                }
                C_RES = "UPDATE T_CARTON_INFO ERROR";

                mst = new Dictionary<string, object>();
           
                if (!string.IsNullOrEmpty(C_CARTONID) || C_CARTONID != "NA")
                {                   
                    mst.Add("NUM", C_NUM+1);
                    mst.Add("FLAG", C_FLAG);
                    mst.Add("CARTONID", C_CARTONID);
                    UpdateData("SFCR.T_CARTON_INFO_HAD", new string[] { "CARTONID" }, mst);
                }
                else
                {
                    if (C_ESN == "NA")
                        TNUM = 0;
                    else
                        TNUM = C_NUM + 1;
                    C_RES = "U1";                   
                    mst.Add("CARTONID", C_CARTONID);
                    mst.Add("LINEID", C_LINEID);
                    mst.Add("WOID", C_WOID);
                    mst.Add("CARTONNUMBER", C_MCARTONNUMBER);
                    mst.Add("NUM", TNUM);
                    mst.Add("FLAG", C_FLAG);
                    InsertData("SFCR.T_CARTON_INFO_HAD", mst);
                }

                if (C_ESN != "NA")
                    C_NUM = C_NUM + 1;
                else
                    C_NUM = 0;
            
                mst = new Dictionary<string, object>();
                mst.Add("CARTONID",C_CARTONID);
                mst.Add("ESN",C_ESN.ToUpper());
                mst.Add("SN",C_SN.ToUpper());
                mst.Add("MAC",C_MAC);
                mst.Add("WOID", C_WOID);
                InsertData("SFCR.T_CARTON_INFO_DTA", mst);


                C_RES = "U2";
                PRO_INSERTPALLETINFO(WOID, CARTONID, LINEID, C_PARTNUMBER, "1", C_NUM.ToString(), C_FLAG, COMPUTER, out RES);
                if (RES != "OK")
                {
                    throw new Exception(RES);
                }
                else
                {
                    C_RES = "U3";
                    if (C_FLAG == "1")
                    {                   

                        mst = new Dictionary<string, object>();
                        mst.Add("WOID", C_WOID);
                        NEXTCARTONID = GetData("SFCR.T_CARTON_INFO_HAD", "MAX(CARTONID)", mst).Rows[0][0].ToString();//BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0].Rows[0][0].ToString();
                        NEXTCARTONID = C_WOID + "C" + (Convert.ToInt32(NEXTCARTONID.Substring(NEXTCARTONID.Length - 4, 4)) + 1).ToString().PadLeft(4, '0');
                        C_RES = "U4";

                        mst = new Dictionary<string, object>();
                        mst.Add("CARTONID", NEXTCARTONID);
                        mst.Add("LINEID", C_LINEID);
                        mst.Add("WOID", C_WOID);
                        mst.Add("CARTONNUMBER", NEXTCARTONID.Substring(NEXTCARTONID.Length-4,4));
                        mst.Add("NUM", 0);
                        mst.Add("FLAG", "0");
                        InsertData("SFCR.T_CARTON_INFO_HAD", mst);

                        C_RES = "U5";
                        RES = "BOXCLOSE:" + NEXTCARTONID + ":" + (INUM + 1) + ":" + C_CARTONQTY;
                    }
                    else
                    {
                        RES = "OK:" + (INUM + 1) + ":" + C_CARTONQTY;
                    }
                }
            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log("PRO_INSERTCARTONINFO " + ex.Message);
            }
        }

        public string PRO_UPDATEWIPANDRECCARTONBOX(string LINE, string MYGROUP, string SN, string EMP, string FLAG, string CARTONID, string MCARTIONID, string PALLETNUMBER, string MPALLETNUMBER, string TRAYNO)
        {
            string RES = string.Empty;

            string C_SN = SN;
            string C_MCARTIONID = string.IsNullOrEmpty(MCARTIONID) ? "NA" : MCARTIONID;
            string C_CARTONID = string.IsNullOrEmpty(CARTONID) ? "NA" : CARTONID;
            string C_LINE = LINE;
            string C_EMP = EMP;
            string CAFTID = string.Empty;
            string C_WOID = string.Empty;
            string PARTNUMBERTEMP = string.Empty;
            string C_MODELDESC = string.Empty;
            string LINETEMP = string.Empty;
            string OLDWIPSTATION = string.Empty;
            string SLOCSTATION = string.Empty;
            string C_STORENUMBER = string.Empty;
            string NEXTSTATION = string.Empty;
            string STR = string.Empty;
            string C_MCARTIONNUMBER = string.Empty;

            if (EMP.Split('-').Length > 0)
                C_EMP = EMP.Split('-')[0];

            DataTable dtwip = twip.Get_WIP_TRACKING("ESN", C_SN, "WOID,PARTNUMBER,PRODUCTNAME,LINE,LOCSTATION,WIPSTATION,ROUTGROUPID,STORENUMBER").Tables[0];
            CAFTID = dtwip.Rows[0]["ROUTGROUPID"].ToString();
            C_WOID = dtwip.Rows[0]["WOID"].ToString();
            PARTNUMBERTEMP = dtwip.Rows[0]["PARTNUMBER"].ToString();
            C_MODELDESC = dtwip.Rows[0]["PRODUCTNAME"].ToString();
            LINETEMP = dtwip.Rows[0]["LINE"].ToString();
            OLDWIPSTATION = dtwip.Rows[0]["WIPSTATION"].ToString();
            SLOCSTATION = dtwip.Rows[0]["LOCSTATION"].ToString();
            C_STORENUMBER = dtwip.Rows[0]["STORENUMBER"].ToString();
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("ROUTGROUPID",CAFTID);
            mst.Add("CRAFTNAME",MYGROUP);
            mst.Add("STATION_FLAG", FLAG);
         
            NEXTSTATION = GetData("SFCB.B_ROUTE_INFO", "NEXTCRAFTNAME",mst).Rows[0][0].ToString(); //BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0].Rows[0][0].ToString();

            DataTable dtwoInfo = woinfo.GetWoInfo(C_WOID, null, "OUTPUTGROUP").Tables[0];
            if (dtwoInfo.Rows[0]["OUTPUTGROUP"].ToString() == MYGROUP)
            {
                if (FLAG == "1")
                    STR = NEXTSTATION;
                else
                    STR = MYGROUP;

            }
            else
                STR = NEXTSTATION;

            if (C_MCARTIONID == "NA")
                C_MCARTIONNUMBER = CARTONID.Substring(CARTONID.Length - 4);
            else
                C_MCARTIONNUMBER = C_MCARTIONID;

            PRO_STNREC(LINE, MYGROUP, PARTNUMBERTEMP, C_MODELDESC, C_WOID, SN, Convert.ToInt32(FLAG), out RES);
            if (RES == "OK")
            {               
                mst = new Dictionary<string, object>();
                mst.Add("LOCSTATION", MYGROUP);
                mst.Add("WIPSTATION", STR);
                mst.Add("NEXTSTATION", "NA");
                mst.Add("USERID", C_EMP);              
                mst.Add("RECDATE", System.DateTime.Now);
                mst.Add("CARTONNUMBER", C_CARTONID);
                mst.Add("MCARTONNUMBER", C_MCARTIONNUMBER);
               // mst.Add("WOID", C_WOID);
                mst.Add("ESN", C_SN);
                UpdateData("SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);

                PRO_INSERTSNDETAIL(C_SN);
                PRO_INSERTCARTONINFO(C_CARTONID, C_SN, C_LINE, C_WOID, C_MCARTIONID, "", "", "", out RES);

            }

            return RES;
        }

        private string PRO_INS_ATE_BACK(IDictionary<string, object> Dic)
        {
            string _StrErr = string.Empty;
            try
            {
                PRO_INS_ATE_BACK(Dic["DATA"].ToString(), Dic["MYGROUP"].ToString(), Dic["SECTION_NAME"].ToString(), Dic["STATION_NAME"].ToString(), Dic["EMP"].ToString(), Dic["LINE"].ToString(), out _StrErr);
            }
            catch
            {
                PRO_INS_ATE_BACK(Dic["DATA"].ToString(), Dic["MYGROUP"].ToString(), "NA", Dic["MYGROUP"].ToString() + "2", Dic["EMP"].ToString(), Dic["LINE"].ToString(), out _StrErr);
            }
                return _StrErr;
        }
        public void PRO_INS_ATE_BACK(string DATA, string MYGROUP,string SECTION_NAME,string STATION_NAME, string EMP, string LINE, out string RES)
        {
            if (DATA.Length < 100)
                DATA = DATA.PadRight(100);

            string C_WO = string.Empty;
            int C_FLAG;
            string C_RES = string.Empty;
            string C_MODEL = string.Empty;
            string C_MODELDESC = string.Empty;
            string C_EMP = string.Empty;
            string C_ESN = (DATA.ToUpper().Substring(0, 25)).Trim();
            string C_EC = (DATA.ToUpper().Substring(25, 8)).Trim();
            string C_MACH = (DATA.ToUpper().Substring(33, 20)).Trim();
            string C_TOOLS = (DATA.ToUpper().Substring(53, 20)).Trim();
            string C_TestResults = (DATA.ToUpper().Substring(73, 5)).Trim();
           // string C_MACH = (DATA.ToUpper().Substring(33)).Trim();
            string C_CLASSDATE = string.Empty;
            string C_CLASS = string.Empty;
            string C_WORKSECTION = string.Empty;
            string C_DAY = string.Empty;
            string C_DATE1 = string.Empty;
            string C_DATE2 = string.Empty;
            string C_WORKDATE = string.Empty;
            string ERR_RES = string.Empty;
            string C_ROUTECODEID = string.Empty;
            string C_ERRFLAG = string.Empty;
            string C_LOCGROUP = string.Empty;
            string C_NEXTSTATION = string.Empty;
            string C_ENDGROUP = string.Empty;
            string C_DATA = DATA.ToUpper();
            try
            {
                if (!string.IsNullOrEmpty(C_TOOLS))
                {
                   C_RES= _Tools.UPDATE_TOOLS_Use_Quantity(C_TOOLS);
                   if (C_RES != "OK")
                       throw new Exception("UPDATE TOOLS ERROR:"+C_RES);
                }

                if (C_TestResults == "PASS" || string.IsNullOrEmpty(C_TestResults))
                {
                    if (string.IsNullOrEmpty(C_EC))
                    {
                        C_EC = "NA";
                    }
                    PRO_GETWORKCLASS(out C_WORKSECTION, out C_CLASS, out C_DAY, out C_DATE1, out C_DATE2, out C_RES);
                    if (C_RES != "OK")
                        throw new Exception(C_RES);
                  
                        //if (C_DAY == "TOMORROW")
                        //    C_CLASSDATE = System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                        //else
                        //    C_CLASSDATE = System.DateTime.Now.ToString("yyyyMMdd");
                    C_WORKDATE = C_DATE1;
                    if (C_DAY == "TOMORROW")
                    {
                        C_CLASSDATE = C_DATE2;
                        if (C_WORKSECTION == "23")
                            C_WORKDATE = C_DATE2;
                    }
                    else
                        C_CLASSDATE = C_WORKDATE;

                    if (C_EC != "NA")
                    {
                        C_RES = PRO_CHECKEC(C_EC);
                        if (C_RES != "OK")
                            throw new Exception(C_RES);
                    }
                    PRO_GETEMPNO(EMP, out C_RES, out C_EMP);
                    if (C_RES != "OK")
                        throw new Exception(C_RES);

                    C_RES = "NO SN";
                    DataTable dt = twip.Get_WIP_TRACKING("ESN", C_ESN, "WOID,PARTNUMBER,PRODUCTNAME,LOCSTATION,NEXTSTATION,ERRFLAG,ROUTGROUPID").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        C_WO = dt.Rows[0]["WOID"].ToString();
                        C_MODEL = dt.Rows[0]["PARTNUMBER"].ToString();
                        C_MODELDESC = dt.Rows[0]["PRODUCTNAME"].ToString();
                        C_LOCGROUP = dt.Rows[0]["LOCSTATION"].ToString();
                        C_NEXTSTATION = dt.Rows[0]["NEXTSTATION"].ToString();
                        C_ERRFLAG = dt.Rows[0]["ERRFLAG"].ToString();
                        C_ROUTECODEID = dt.Rows[0]["ROUTGROUPID"].ToString();
                    }
                    else
                    {
                        throw new Exception(C_RES);
                    }

                    C_RES = "WO NOT FOUND";
                    DataTable dt_B = woinfo.GetWoInfo(C_WO, null, "WOSTATE,OUTPUTGROUP").Tables[0];
                    if (dt_B.Rows.Count > 0)
                    {
                        C_FLAG = Convert.ToInt32(dt_B.Rows[0]["WOSTATE"].ToString());
                        C_ENDGROUP = dt_B.Rows[0]["OUTPUTGROUP"].ToString();
                    }
                    else
                    {
                        throw new Exception("NO WO FOUNT");
                    }
                    switch (C_FLAG)
                    {
                        case 0:
                            RES = "Waiting Relaese";
                            break;
                        case 1:
                            RES = "OK";
                            break;
                        case 2:
                            RES = "OK";
                            break;
                        case 3:
                            RES = "WO IS CLOSED ";
                            break;
                        case 4:
                            RES = "WO HOLD";
                            break;
                        default:
                            RES = "WO STATU ERROR";
                            break;
                    }
                    if (RES == "OK")
                    {
                        if (C_EC == "NA")
                        {
                            C_RES = PRO_CHECK_ROUTE(C_ESN, MYGROUP);
                            if (C_RES == "OK")
                            {
                                PRO_STNREC(LINE, MYGROUP, C_MODEL, C_MODELDESC, C_WO, C_ESN, 0, out C_RES);
                                if (C_RES == "OK")
                                {
                                    PRO_UPDATEWIPSTATION_NEW(LINE, MYGROUP, SECTION_NAME, C_ESN, C_EMP, 0, C_ROUTECODEID, C_WO, C_MACH,STATION_NAME, C_MODEL, out C_RES);
                                    if (C_RES != "OK")
                                        throw new Exception(C_RES);
                                    else
                                        RES = C_RES;
                                }
                                else
                                    throw new Exception(C_RES);
                            }
                            else
                                throw new Exception(C_RES);
                        }
                        else
                        {
                            C_RES = PRO_CHECK_ROUTE_RE(C_ESN, MYGROUP);
                            if (C_RES == "OK")
                            {
                                PRO_STNREC(LINE, MYGROUP, C_MODEL, C_MODELDESC, C_WO, C_ESN, 1, out C_RES);
                                if (C_RES == "OK")
                                {
                                    PRO_UPDATEWIPSTATION_NEW(LINE, MYGROUP, SECTION_NAME, C_ESN, C_EMP, 1, C_ROUTECODEID, C_WO, C_MACH, STATION_NAME, C_MODEL, out C_RES);
                                    if (C_RES != "OK")
                                        throw new Exception(C_RES);                                  
                                    INSERT_REPAIR_INFO(C_EC, C_ESN, C_WO, C_MODEL, MYGROUP, C_EMP, LINE, C_CLASSDATE, C_CLASS, C_WORKSECTION);
                                    RES = "OK";
                                }
                                else
                                    throw new Exception(C_RES);
                            }
                            else
                                throw new Exception(C_RES);
                        }
                    }
                }
                else
                {
                    RES = "OK";
                }


            }
            catch (Exception ex)
            {
                RES = ex.Message;
                Insert_Exception_Log(string.Format("PRO_INS_ATE_BACK {0} {1}" ,C_ESN, ex.Message));
            }

           // Insert_DB_Log(string.Format("PRO_INS_ATE_BACK DATA:{0} {1}",DATA , RES));

        }
        public string PRO_TEST_REWORK_INPUT(IDictionary<string, object> Dic)
        {
            string _StrErr = string.Empty;
            try
            {
                PRO_TEST_REWORK_INPUT(Dic["DATA"].ToString(), Dic["MYGROUP"].ToString(), Dic["SECTION_NAME"].ToString(), Dic["STATION_NAME"].ToString(), Dic["EMP"].ToString(), Dic["MODEL"].ToString(), Dic["WO"].ToString(), Dic["LINE"].ToString(), Dic["FLAG"].ToString(), out _StrErr);
            }
            catch
            {
                PRO_TEST_REWORK_INPUT(Dic["DATA"].ToString(),"NA","NA","NA", Dic["EMP"].ToString(), Dic["MODEL"].ToString(), Dic["WO"].ToString(), Dic["LINE"].ToString(), Dic["FLAG"].ToString(), out _StrErr);
            }
                return _StrErr;
        }
        public void PRO_TEST_REWORK_INPUT(string DATA, string MYGROUP,string SECTION_NAME,string STATION_NAME,string EMP, string MODEL, string WO, string LINE, string FLAG, out string RES)
        {
            //FLAG ----- 0 数通产品 1 手机 2一期数据库重工 3 手机重工需重写号码
            //int C_COUNT;
            //int D_COUNT;
            //int WH_COUNT;       
            string C_RES = string.Empty;
            string C_PARTNUMBER = string.Empty;
            string C_BOM = string.Empty;
            string C_BOMGROUP = string.Empty;
            string C_LOCGROUP = string.Empty;
            string C_WO = string.Empty;
            string C_ROUTECODE = string.Empty;
            string C_INPUTGROUP = string.Empty;
            string C_PRODUCTNAME = string.Empty;
            string C_FLAG = string.Empty;
            int C_TARGETQTY;
            int C_INPUTQTY;           
            string Clear_Type = string.Empty;
            string C_EMP = string.Empty;
            DataTable dt_Insert_sfis_kps = new DataTable("mydt");
            dt_Insert_sfis_kps.Columns.Add("ESN", typeof(string));        
            dt_Insert_sfis_kps.Columns.Add("SNTYPE", typeof(string));
            dt_Insert_sfis_kps.Columns.Add("SNVAL", typeof(string));

            if (EMP.Split('-').Length > 1)
            {
                PRO_GETEMPNO(EMP, out C_RES, out C_EMP);
                if (C_RES != "OK")
                    throw new Exception(C_RES);
            }
            else
                C_EMP = EMP;

            Dictionary<string, object> mst = null;
           
            DataTable dt_B = new DataTable();
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);          
            try
            {
                C_RES = "WO OR ROUTE NOT FOUND";
                DataTable dt = woinfo.GetWoInfo(WO, null, "ROUTGROUPID,INPUTGROUP,INPUTQTY,QTY,BOMNUMBER,PRODUCTNAME,WOSTATE,CLEAR_SERIAL_TYPE").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    C_ROUTECODE = dt.Rows[0]["ROUTGROUPID"].ToString();
                    C_INPUTGROUP = dt.Rows[0]["INPUTGROUP"].ToString();
                    C_INPUTQTY = Convert.ToInt32(dt.Rows[0]["INPUTQTY"].ToString());
                    C_TARGETQTY = Convert.ToInt32(dt.Rows[0]["qty"].ToString());
                    C_BOM = dt.Rows[0]["BOMNUMBER"].ToString();
                    C_PRODUCTNAME = dt.Rows[0]["PRODUCTNAME"].ToString();
                    C_FLAG = dt.Rows[0]["WOSTATE"].ToString();
                    Clear_Type = dt.Rows[0]["CLEAR_SERIAL_TYPE"].ToString();
                }
                else
                {
                    throw new Exception("WO NOT ONLINE");
                }
                if (C_TARGETQTY <= C_INPUTQTY)
                {                    
                    throw new Exception(WO+" InputQTY > TargetQTY");
                }
                else
                {
                    switch (Convert.ToInt32(C_FLAG))
                    {
                        case 0:
                            C_RES = "Waiting Relaese";
                            break;
                        case 1:
                            C_RES = "OK";
                            break;
                        case 2:
                            C_RES = "OK";
                            break;
                        case 3:
                            C_RES = "WO IS CLOSED ";
                            break;
                        case 4:
                            C_RES = "WO HOLD";
                            break;
                        default:
                            C_RES = "WO STATUS ERROR";
                            break;
                    }
                    if (C_RES != "OK")
                        throw new Exception(C_RES);
                   
                        dt_B = twip.Get_WIP_TRACKING("ESN", DATA, "WOID,PARTNUMBER,LOCSTATION").Tables[0];
                        DataTable dt_C = tDS.Sel_Product_Info("ESN", DATA).Tables[0];
                        if (dt_B.Rows.Count > 0)
                        {
                            C_PARTNUMBER = dt_B.Rows[0]["PARTNUMBER"].ToString();
                            C_WO = dt_B.Rows[0]["WOID"].ToString();
                            C_LOCGROUP = dt_B.Rows[0]["LOCSTATION"].ToString();
                            if (C_WO == WO)
                                throw new Exception("THIS SN HAS INPUT");

                            if (C_LOCGROUP != "SC_TEST" && C_LOCGROUP != "SC_SMT" && C_LOCGROUP != "SMT_STOCKIN" && C_LOCGROUP != "PCBA_StoreIn")
                            {
                                if (dt_C.Rows.Count < 1 || dt_C.Rows[0]["STATUS"].ToString() != "9")
                                    throw new Exception("NO KPS");                              

                            }
                        }
                        else
                        {
                            C_RES = "NO REWORK SN";
                            if (dt_C.Rows[0]["STATUS"].ToString() == "9")
                                C_PARTNUMBER = dt_C.Rows[0]["PARTNUMBER"].ToString();
                        }
                        C_RES = "NO KPS IN BOM";
                     
                        mst = new Dictionary<string, object>();
                        mst.Add("BOMNUMBER", C_BOM);
                        mst.Add("KPNUMBER", C_PARTNUMBER);
                        DataTable dt_D = GetData("SFCB.B_BOM_KEYPART", "STATION", mst);// BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];
                        if (dt_D.Rows.Count > 0)
                            C_BOMGROUP = dt_D.Rows[0]["STATION"].ToString();                       
                        else
                            throw new Exception(C_RES);
                   


                    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);                                     

                    mst = new Dictionary<string, object>();
                    mst.Add("ESN", DATA);
                    DataTable dt_z_whs_tracking = GetData("SFCR.Z_WHS_TRACKING", "*", mst);
                    if (dt_z_whs_tracking.Rows.Count > 0)
                        dp.AddListData(tx, "SFCR.Z_WHS_TRACKING_UNDO", DataTable_To_ListDictionary(dt_z_whs_tracking));                                      
               
                    dp.DeleteData(tx, "SFCR.Z_WHS_TRACKING", mst);
                                    
                    dp.DeleteData(tx, "SFCR.Z_WHS_KEYPART_UNDO", mst);

                    DataTable dt_Z_WHS_KEYPART = GetData("SFCR.Z_WHS_KEYPART", "*", mst);
                  
                    if (dt_Z_WHS_KEYPART.Rows.Count > 0)
                    {
                        dp.AddListData(tx, "SFCR.Z_WHS_KEYPART_UNDO", DataTable_To_ListDictionary(dt_Z_WHS_KEYPART));
                        foreach (DataRow dr in dt_Z_WHS_KEYPART.Rows)
                        {
                            dt_Insert_sfis_kps.Rows.Add(dr["ESN"].ToString().Trim(), dr["SNTYPE"].ToString().Trim(), dr["SNVAL"].ToString().Trim());
                        }
                    }
                  
                  
                    dp.DeleteData(tx, "SFCR.Z_WHS_KEYPART", mst);
                  
                    dp.DeleteData(tx, "SFCR.Z_WHS_TRACKING_NEW", mst);

                //    DataTable dt_E = ClsKeyPart.GetWipKeyParts("ESN",DATA).Tables[0];
                      

                        DataTable dt_t_wip_keypart_online = GetData("SFCR.T_WIP_KEYPART_ONLINE", "esn,woId,sntype,snval,station,kpno,recdate".ToUpper(), mst);
                      //  dt_t_wip_keypart_online = getNewTable(dt_t_wip_keypart_online, "sntype not like '%MAC'");
                        if (dt_t_wip_keypart_online.Rows.Count > 0)
                        {
                          //  dp.AddListData(tx, "SFCR.T_WIP_KEYPART_UNDO", DataTable_To_ListDictionary(dt_t_wip_keypart_online));

                            foreach (DataRow dr in dt_t_wip_keypart_online.Rows)
                            {
                                dt_Insert_sfis_kps.Rows.Add(dr["ESN"].ToString().Trim(), dr["SNTYPE"].ToString().Trim(), dr["SNVAL"].ToString().Trim());
                            }
                        }


                        DataView dataView = dt_Insert_sfis_kps.DefaultView;
                        DataTable dataTableDistinct = dataView.ToTable(true, "ESN", "SNTYPE", "SNVAL");//注：其中ToTable（）
                        mst = new Dictionary<string, object>();
                        mst.Add("ESN", DATA);
                        dp.DeleteData(tx, "SFCR.T_WIP_KEYPART_ONLINE", mst);
                        #region
                        //if (FLAG.Length == 1 && string.IsNullOrEmpty(Clear_Type))
                        //{
                        //    if (FLAG == "0")
                        //    {
                        //        foreach (DataRow dr in dataTableDistinct.Rows)
                        //        {
                        //            if (dr["SNTYPE"].ToString() == "MAC" || dr["SNTYPE"].ToString() == "KPESN" || dr["SNTYPE"].ToString() == "BTMAC" || dr["SNTYPE"].ToString() == "WIFIMAC")
                        //            {
                        //                mst = new Dictionary<string, object>();
                        //                mst.Add("ESN", DATA);
                        //                mst.Add("WOID", WO);
                        //                mst.Add("SNTYPE", dr["SNTYPE"].ToString());
                        //                mst.Add("SNVAL", dr["SNVAL"].ToString());
                        //                mst.Add("STATION", C_INPUTGROUP);
                        //                mst.Add("KPNO", "NA");
                        //                dp.AddData(tx, "SFCR.T_WIP_KEYPART_ONLINE", mst);
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        foreach (DataRow dr in dataTableDistinct.Rows)
                        //        {
                        //            mst = new Dictionary<string, object>();
                        //            mst.Add("ESN", DATA);
                        //            mst.Add("WOID", WO);
                        //            mst.Add("SNTYPE", dr["SNTYPE"].ToString());
                        //            mst.Add("SNVAL", dr["SNVAL"].ToString());
                        //            mst.Add("STATION", C_INPUTGROUP);
                        //            mst.Add("KPNO", "NA");
                        //            dp.AddData(tx, "SFCR.T_WIP_KEYPART_ONLINE", mst);
                        //        }
                        //    }

                        //}
                        //else
                        //{
                        #endregion
                        List<string> ClsType = new List<string>();
                        if (!string.IsNullOrEmpty(Clear_Type))
                            {
                            
                                foreach (string Str in Clear_Type.Split(','))
                                {
                                    ClsType.Add(Str);
                                }                               
                                foreach (DataRow dr in dataTableDistinct.Rows)
                                {
                                    if (!ClsType.Contains(dr["SNTYPE"].ToString()))
                                    {
                                        mst = new Dictionary<string, object>();
                                        mst.Add("ESN", DATA);
                                        mst.Add("WOID", WO);
                                        mst.Add("SNTYPE", dr["SNTYPE"].ToString());
                                        mst.Add("SNVAL", dr["SNVAL"].ToString());
                                        mst.Add("STATION", C_INPUTGROUP);
                                        mst.Add("KPNO", "NA");
                                        dp.AddData(tx, "SFCR.T_WIP_KEYPART_ONLINE", mst);
                                    }
                                    else
                                    {
                                        DataTable dt_back = getNewTable(dt_t_wip_keypart_online, string.Format("SNTYPE='{0}'", dr["SNTYPE"].ToString()));
                                        foreach (DataRow dr_back in dt_back.Rows)
                                        {
                                            mst = new Dictionary<string, object>();
                                            mst.Add("ESN", dr_back["ESN"].ToString());
                                            mst.Add("WOID", dr_back["WOID"].ToString());
                                            mst.Add("SNTYPE", dr_back["SNTYPE"].ToString());
                                            mst.Add("SNVAL", dr_back["SNVAL"].ToString());
                                            mst.Add("STATION", dr_back["STATION"].ToString());
                                            mst.Add("KPNO", dr_back["KPNO"].ToString());
                                            mst.Add("RECDATE", dr_back["RECDATE"].ToString());
                                            dp.AddData(tx, "SFCR.T_WIP_KEYPART_UNDO", mst);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (DataRow dr in dataTableDistinct.Rows)
                                {
                                    mst = new Dictionary<string, object>();
                                    mst.Add("ESN", DATA);
                                    mst.Add("WOID", WO);
                                    mst.Add("SNTYPE", dr["SNTYPE"].ToString());
                                    mst.Add("SNVAL", dr["SNVAL"].ToString());
                                    mst.Add("STATION", C_INPUTGROUP);
                                    mst.Add("KPNO", "NA");
                                    mst.Add("RECDATE", System.DateTime.Now);
                                    dp.AddData(tx, "SFCR.T_WIP_KEYPART_ONLINE", mst);
                                }
                            }

                     //  }                 

                    if (dt_B.Rows.Count > 0)
                    {
                        mst = new Dictionary<string, object>();
                        mst.Add("ESN", DATA);
                        DataTable dt_t_wip_tracking_online = GetData("SFCR.T_WIP_TRACKING_ONLINE", "*", mst);
                        if (dt_t_wip_tracking_online.Rows.Count > 0)
                            dp.AddListData(tx, "SFCR.T_WIP_UNDO", DataTable_To_ListDictionary(dt_t_wip_tracking_online));
                        mst = new Dictionary<string, object>();
                        mst.Add("WOID", WO);
                        mst.Add("PARTNUMBER", MODEL);
                        mst.Add("PRODUCTNAME", C_PRODUCTNAME);
                        mst.Add("NEXTSTATION", C_INPUTGROUP);
                        mst.Add("USERID", C_EMP);
                        if (ClsType.Contains("SN"))
                        mst.Add("SN", "NA");
                        if (ClsType.Contains("MAC"))
                        mst.Add("MAC", "NA");
                       if (ClsType.Contains("IMEI"))
                        mst.Add("IMEI", "NA");
                        mst.Add("CARTONNUMBER", "NA");
                        mst.Add("MCARTONNUMBER", "NA");
                        mst.Add("PALLETNUMBER", "NA");
                        mst.Add("MPALLETNUMBER", "NA");
                        mst.Add("SCRAPFLAG", 0);
                        mst.Add("STORENUMBER", "NA");
                        mst.Add("QA_NO", "NA");
                        mst.Add("QA_RESULT", "NA");
                        mst.Add("ROUTGROUPID", C_ROUTECODE);
                        mst.Add("IN_LINE_TIME", DateTime.Now);
                        mst.Add("ESN", DATA);
                      
                        dp.UpdateData(tx, "SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);
                    }
                    else
                    {
                        C_RES = "WIP_Tracking Dup";                   
                        mst = new Dictionary<string, object>();
                        mst.Add("ESN", DATA);
                        mst.Add("WOID", WO);
                        mst.Add("PARTNUMBER", MODEL);
                        mst.Add("PRODUCTNAME", C_PRODUCTNAME);
                        mst.Add("LOCSTATION", C_INPUTGROUP);
                        mst.Add("WIPSTATION", C_INPUTGROUP);
                        mst.Add("NEXTSTATION", C_INPUTGROUP);
                        mst.Add("USERID", EMP);
                        mst.Add("LINE", LINE);
                        mst.Add("ROUTGROUPID", C_ROUTECODE);
                        dp.AddData(tx, "SFCR.T_WIP_TRACKING_ONLINE", mst);
                    }
                    tx.Commit();

                    UPDATE_WO_FIRST_PCS_TIME(WO);
                    PRO_STNREC(LINE, C_INPUTGROUP, MODEL, C_PRODUCTNAME, WO, DATA, 0, out C_RES);
                    if (C_RES == "OK")
                    {
                        STATION_NAME = string.IsNullOrEmpty(STATION_NAME) ? C_INPUTGROUP + "1" : STATION_NAME;
                        PRO_UPDATEWIPSTATION_NEW(LINE, C_INPUTGROUP, SECTION_NAME, DATA, C_EMP, 0, C_ROUTECODE, WO, "NA", STATION_NAME, MODEL, out C_RES);
                        RES = C_RES;
                    }
                    else
                        throw new Exception(C_RES);
                }

            }
            catch (Exception ex)
            {
                tx.Rollback();
                if (C_RES == ex.Message)
                    RES = C_RES;
                else
                RES =C_RES+"-"+ ex.Message;
                Insert_Exception_Log("PRO_TEST_REWORK_INPUT " + RES);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }         
           Insert_DB_Log(string.Format("PRO_TEST_REWORK_INPUT DATA:{0} {1}  -->{2}",DATA, RES,myWatch.ElapsedMilliseconds.ToString()));
        }
        public List<string> ExecuteProcedure_Class(string Storedproc, IDictionary<string, object> DicParam)  
        {            
           
            List<string> C_RES = new List<string>();
            switch (Storedproc.ToUpper())
            {
                case "INS_ATE_SN_BACK":
                    C_RES = INS_ATE_SN_BACK(DicParam);
                    break;
                case "PRO_CHECKEC":
                    C_RES.Add(PRO_CHECKEC(DicParam));
                    break;
                case "PRO_CHECKEMP":
                    C_RES.Add(PRO_CHECKEMP(DicParam));
                    break;
                case "PRO_CHECKROUTE":
                    C_RES.Add(PRO_CHECK_ROUTE(DicParam));
                    break;
                case "PRO_CHECKSN":
                    C_RES.Add(PRO_CHECK_SN(DicParam));
                    break;
                case "PRO_CHECK_KPS_VALID":
                    C_RES.Add(PRO_CHECK_KPS_VALID(DicParam));
                    break;
                case "PRO_CHECK_ROUTE_MOBILE":
                    C_RES = PRO_CHECK_ROUTE_MOBILE(DicParam);
                    break;
                case "PRO_INPUT_SN_FIRST":
                    C_RES.Add(PRO_INPUT_SN_FIRST(DicParam));
                    break;
                case "PRO_INSERT_KEYPARTS":
                    C_RES.Add(PRO_INSERT_KEYPARTS(DicParam));
                    break;
                case "PRO_SN_INPUT_WIPFIRST":
                    C_RES.Add(PRO_SN_INPUT_WIPFIRST(DicParam));
                    break;
                case "PRO_TEST_INPUT_ALL":
                    C_RES.Add(PRO_TEST_INPUT_ALL(DicParam));
                    break;
                case "PRO_TEST_MAIN_AUTO":
                    C_RES.Add(PRO_TEST_MAIN_AUTO(DicParam));
                    break;
                case "PRO_TEST_MAIN_ONLY":
                    C_RES.Add(PRO_TEST_MAIN_ONLY(DicParam));
                    break;
                case "PRO_CHECK_ROUTE_ATE":
                    C_RES.Add(PRO_CHECK_ROUTE_ATE(DicParam));
                    break;
                case "PRO_INS_ATE_BACK":
                    C_RES.Add(PRO_INS_ATE_BACK(DicParam));
                    break;
                case "CALL_PRO_INPUT_SN_FIRST":
                    C_RES.Add(CALL_PRO_INPUT_SN_FIRST(DicParam));
                    break;
                case "CHECK_WO":
                    C_RES.Add(CHECK_WO(DicParam));
                       break;
                case "PRO_TEST_REWORK_INPUT":
                    C_RES.Add(PRO_TEST_REWORK_INPUT(DicParam));
                    break;
                default:
                    C_RES.Add("Call SP Error");
                    break;
            }
            return C_RES;

        }

        public string ExecuteProcedure(string Storedproc, string dicstring)
        {
            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicstring);
            return ExecuteProcedure_Class(Storedproc, dic)[0];
        }

        public string PublicStationPro(string Storedproc, System.Data.DataTable dt)
        {            
            IDictionary<string, object> dic = new Dictionary<string, object>();
            foreach (DataRow dr in dt.Rows)
            {
                dic.Add(dr[0].ToString(), dr[1].ToString());
            }
            return ExecuteProcedure_Class(Storedproc, dic)[0];
        }
        public List<string> PublicStationProParam(string Storedproc, string dicstring)
        {           

            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicstring);          

            List<string> LsMsg = new List<string>();
            LsMsg = ExecuteProcedure_Class(Storedproc, dic);
            LsMsg[0] = "RES-" + LsMsg[0];
            if (LsMsg.Count > 1)
               LsMsg[1]="COMMAND-"+LsMsg[1];
            return LsMsg;

        }

        public string Calculation_MacList(string wo, string sntype, int Flag)
        {
            //0 计算序列号  1删除序列号 
            string _StrErr = string.Empty;
            switch (Flag)
            {
                case 0:
                    if (!woinfo.GetWoSnListCount(wo, sntype, Flag))
                        _StrErr = "this woId Have Sn List";
                    else
                        _StrErr = PRO_CONVERTMACIMEI(wo, sntype); //BLL.BllMsSqllib.Instance.ConvertMACIMEI(wo, sntype);

                    break;
                case 1:
                    if (!woinfo.GetWoSnListCount(wo, sntype, Flag))
                        _StrErr = string.Format("This SnType[{0}] Already Use ", sntype);
                    else
                    {
                        try
                        {                           
                            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                            Dictionary<string, object> mst = new Dictionary<string, object>();
                            mst.Add("WOID", wo);
                            mst.Add("SNTYPE", sntype);
                            mst.Add("STATUS", 0);
                            dp.DeleteData("SFCR.T_WO_SNLIST", mst);
                            _StrErr = "OK";
                        }
                        catch (Exception ex)
                        {

                            _StrErr = "Execp: " + ex.Message;
                        }
                    }

                    break;
                default:
                    _StrErr = "Flag Send Error";
                    break;
            }
            return _StrErr;
        }

        public string SP_TEST_CTN_PALT_TRAY_NEW(List<string> DATA, string MYGROUP, string EMP, string EC, string LINE, string LOCDATA, string CUTDATA, int Flag)
        {
            string colnum = "";
            if (Flag == 0)
            {
                colnum = "TrayNo";
            }
            if (Flag == 1)
            {
                colnum = "cartonnumber";
            }
            if (Flag == 2)
            {
                colnum = "palletnumber";
            }
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
        
            IList<IDictionary<string,object>> lsMst = new  List<IDictionary<string,object>>();
            IDictionary<string,object> mst =null;
      
            foreach (string Item in DATA)
            {
                mst = new Dictionary<string,object>();
                mst.Add(colnum.ToUpper(),LOCDATA);
                mst.Add("ESN",Item);
                lsMst.Add(mst);
            }
            string cmdRes = "OK";
            try
            {
                dp.UpdateListData("sfcr.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, lsMst);
            }
            catch (Exception ex)
            {
                cmdRes = ex.Message;
            }        

            if (cmdRes == "OK")
            {
                foreach (string Item in DATA)
                {
                    string spRes = PRO_TEST_CTN_PALT_TRAY(Item, MYGROUP, EMP, EC, LINE, LOCDATA, CUTDATA, Flag);
                  if (spRes != "OK")
                    {
                        return spRes;
                    }
                }
                return "OK";
            }
            else
            {
                return cmdRes;
            }
        }

        /// <summary>
        /// 增加产品信息
        /// </summary>
        /// <param name="product"></param>
        /// <param name="Err"></param>
        public string InsertProdctInfo(string  StrProduct, string LsSerialinfo, string  StrPalletinfo)
        {
            //添加产品

            IDictionary<string, object> Product = MapListConverter.JsonToDictionary(StrProduct);
            IDictionary<string, object> Palletinfo = MapListConverter.JsonToDictionary(StrPalletinfo);
            IList<IDictionary<string, object>> Serialinfo = MapListConverter.JsonToListDictionary(LsSerialinfo);

            string _StrErr = string.Empty;          

            #region 对应的序列号类型

            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            try
            {
                int count=0;

                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst =null;

                #region 添加产品信息
                mst = new Dictionary<string, object>();
                mst.Add("PARTNUMBER",Product["PARTNUMBER"]);
                if (dp.GetData("SFCB.B_PRODUCT", "PARTNUMBER", mst, out count).Tables[0].Rows.Count == 0)               
                    dp.AddData(tx, "SFCB.B_PRODUCT", Product);                
                else               
                  dp.UpdateData(tx, "SFCB.B_PRODUCT", new string[] { "PARTNUMBER" }, Product);
                #endregion
                #region 添加包装容量信息
                mst = new Dictionary<string, object>();
                mst.Add("PARTNUMBER",Palletinfo["PARTNUMBER"]);
                mst.Add("VERSIONCODE",Palletinfo["VERSIONCODE"]);

                Palletinfo.Add("RECDATE",System.DateTime.Now);
                if (dp.GetData("SFCB.B_PACK_PARAMETERS", "PARTNUMBER", mst, out count).Tables[0].Rows.Count == 0)        
                    dp.AddData(tx, "SFCB.B_PACK_PARAMETERS", Palletinfo);              
                else               
                    dp.UpdateData(tx, "SFCB.B_PACK_PARAMETERS", new string[] { "PARTNUMBER", "VERSIONCODE" }, Palletinfo);
                #endregion

                mst = new Dictionary<string, object>();
                mst.Add("PARTNUMBER", Product["PARTNUMBER"].ToString());
                dp.DeleteData(tx,"SFCB.B_PRODUCT_SERIAL_INFO", mst);

                dp.AddListData(tx,"SFCB.B_PRODUCT_SERIAL_INFO", Serialinfo);
                tx.Commit();
                _StrErr = "OK";
            }
            catch (Exception ex)
            {
                tx.Rollback();
                _StrErr = ex.Message;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            //MySqlCommand SqlCmd = new MySqlCommand();

            //SqlCmd.CommandText = "delete from SFCB.b_Product_Serial_Info where partnumber=@sPart";
            //SqlCmd.Parameters.Add("sPart", MySqlDbType.VarChar).Value = Product["PARTNUMBER"].ToString();
            //BLL.BllMsSqllib.Instance.ExecteNonQuery(SqlCmd);

            //foreach (Entity.tProductSerialInfo si in lsserialinfo)
            //{
            //    MySqlCommand cmd = new MySqlCommand();
            //    cmd.CommandText = " INSERT INTO SFCB.B_PRODUCT_SERIAL_INFO  (PARTNUMBER, SERIALNAME)   VALUES  (@PARTNNO, @sSERIAL)";
            //    cmd.Parameters.AddRange(new MySqlParameter[]
            //                {
            //                   new MySqlParameter("PARTNNO",MySqlDbType.VarChar){Value=si.partnumber},
            //                   new MySqlParameter("sSERIAL",MySqlDbType.VarChar){Value=si.serialname}
            //                });
            //    BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
            //}
            #endregion
            return _StrErr;
        }

        public string PRO_UPDATE_CARTON_BOX(string LINE, string MYGROUP, string SN, string EMP, string FLAG, string CARTONID, string MCARTIONID)
        {
            string C_RES = string.Empty;
            string C_SN = SN;
            string C_MCARTIONID = string.IsNullOrEmpty(MCARTIONID) ? "NA" : MCARTIONID;
            string C_CARTONID = string.IsNullOrEmpty(CARTONID) ? "NA" : CARTONID;
            string C_LINE = LINE;
            string C_EMP = string.Empty;
            string C_WOID = string.Empty;
            string C_PARTNUMBER = string.Empty;
            string C_MODELDESC = string.Empty;
            string C_ROUTECODE = string.Empty;
            int C_COUNT = 0;
            int C_NUM = 0;
            int INUM = 0;
            int C_CARTONQTY = 0;
            string C_FLAG = string.Empty;
            string C_MCTN = string.Empty;
            string C_NEXTCARTONID = string.Empty;
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            try
            {
                if (EMP.Split('-').Length > 0)
                {
                    C_EMP = EMP.Split('-')[0];
                }
                else
                {
                    C_EMP = EMP;
                }
                DataTable dtwip = twip.Get_WIP_TRACKING("ESN", C_SN, "WOID,PARTNUMBER,PRODUCTNAME,ROUTGROUPID").Tables[0];
                C_WOID = dtwip.Rows[0]["WOID"].ToString();
                C_PARTNUMBER = dtwip.Rows[0]["PARTNUMBER"].ToString();
                C_MODELDESC = dtwip.Rows[0]["PRODUCTNAME"].ToString();
                C_ROUTECODE = dtwip.Rows[0]["ROUTGROUPID"].ToString();

                //MySqlCommand cmd = new MySqlCommand();
                //cmd.CommandText = "SELECT * FROM SFCR.T_CARTON_INFO_HAD WHERE CARTONID = @C_CARTONID";
                //cmd.Parameters.Add("C_CARTONID", MySqlDbType.VarChar).Value = C_CARTONID;
                //C_COUNT = BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0].Rows.Count;
                string table = "SFCR.T_CARTON_INFO_HAD";
                string fieldlist = "CARTONID,LINEID,WOID,CARTONNUMBER,NUM,REMARK,FLAG";
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("CARTONID", C_CARTONID);
                C_COUNT = dp.GetData(table, fieldlist, mst, out count).Tables[0].Rows.Count;

                if (C_COUNT == 0)
                {
                    C_NUM = 0;
                    C_FLAG = "0";
                }
                else
                {
                    //cmd = new MySqlCommand();
                    //cmd.CommandText = " SELECT NUM, FLAG FROM SFCR.T_CARTON_INFO_HAD WHERE CARTONID = @C_CARTONID";//---查询已经包装数量
                    //cmd.Parameters.Add("C_CARTONID", MySqlDbType.VarChar).Value = C_CARTONID;
                    //DataTable dtctnHad = BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];
                    mst = new Dictionary<string, object>();
                    mst.Add("CARTONID", C_CARTONID);
                    DataTable dtctnHad = dp.GetData("SFCR.T_CARTON_INFO", "NUM,FLAG", mst, out count).Tables[0];
                    C_NUM = Convert.ToInt32(dtctnHad.Rows[0]["NUM"].ToString());
                    C_FLAG = dtctnHad.Rows[0]["FLAG"].ToString();

                }
                INUM = C_NUM;

                //cmd = new MySqlCommand();
                //cmd.CommandText = "  SELECT W.PARTNUMBER, P.CARTONQTY  FROM SFCR.T_WO_INFO W, SFCB.B_PACK_PARAMETERS P  WHERE P.PARTNUMBER = W.PARTNUMBER  AND W.WOID = @C_WOID ";
                //cmd.Parameters.Add("C_WOID", MySqlDbType.VarChar).Value = C_WOID;
                //DataTable dtParam = BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];

                string filter = "P.PARTNUMBER = W.PARTNUMBER  AND W.WOID = {0}";
                mst = new Dictionary<string, object>();
                mst.Add("WOID", C_WOID);
                DataTable dtParam = TransactionManager.GetData("SFCR.T_WO_INFO W, SFCB.B_PACK_PARAMETERS P", "W.PARTNUMBER, P.CARTONQTY", filter, mst, null, null, out count).Tables[0];

                if (dtParam.Rows.Count == 0)
                    throw new Exception("PACK PARAM NOT DEFINE -" + C_WOID);
                C_PARTNUMBER = dtParam.Rows[0]["PARTNUMBER"].ToString();
                C_CARTONQTY = Convert.ToInt32(dtParam.Rows[0]["CARTONQTY"].ToString());

                if ((C_NUM >= C_CARTONQTY) || (C_FLAG == "1"))
                    throw new Exception("BOX IS CLOSE");
                if ((C_NUM + 1) >= C_CARTONQTY)
                    C_FLAG = "1";
                if (C_MCARTIONID == "NA")
                    C_MCTN = CARTONID.Substring(CARTONID.Length - 4, 4);
                else
                    C_MCTN = C_MCARTIONID;
                //cmd = new MySqlCommand();
                //cmd.CommandText = "UPDATE SFCR.T_WIP_TRACKING_ONLINE  SET CARTONNUMBER  = @C_CARTONID, MCARTONNUMBER = @C_MCTN WHERE WOID = @C_WOID AND ESN = @C_SN";
                //cmd.Parameters.AddRange(new MySqlParameter[] {
                //                            new MySqlParameter("C_CARTONID",MySqlDbType.VarChar){Value=C_CARTONID},
                //                            new MySqlParameter("C_MCTN",MySqlDbType.VarChar){Value=C_MCTN},
                //                            new MySqlParameter("C_WOID",MySqlDbType.VarChar){Value=C_WOID},
                //                            new MySqlParameter("C_SN",MySqlDbType.VarChar){Value=C_SN}
                //                            });
                //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

                mst = new Dictionary<string, object>();
                mst.Add("CARTONNUMBER", C_CARTONID);
                mst.Add("MCARTONNUMBER", C_MCTN);
                mst.Add("WOID", C_WOID);
                mst.Add("ESN", C_SN);
                dp.UpdateData("SFCR.T_WIP_TRACKING_ONLINE", new string[] { "WOID", "ESN" }, mst);
                PRO_STNREC(LINE, MYGROUP, C_PARTNUMBER, C_MODELDESC, C_WOID, SN, Convert.ToInt32(FLAG), out C_RES);
                if (C_RES != "OK")
                    throw new Exception(C_RES);
                PRO_UPDATEWIPSTATION_NEW(LINE, MYGROUP, "NA", C_SN, C_EMP, 0, C_ROUTECODE, C_WOID, "NA", MYGROUP + "1", C_PARTNUMBER, out C_RES);
                if (C_RES != "OK")
                    throw new Exception(C_RES);

                // List<MySqlCommand> LsCmd = new List<MySqlCommand>();
                if (C_COUNT == 0) //   ---如果是一箱的第一片产品,插入箱号信息
                {
                    //cmd = new MySqlCommand();
                    //cmd.CommandText = "INSERT INTO SFCR.T_CARTON_INFO_HAD (CARTONID, LINEID, WOID, CARTONNUMBER, NUM, FLAG)  VALUES (@C_CARTONID, @C_LINE, @C_WOID, @C_MCTN, 0, @C_FLAG)";
                    //cmd.Parameters.AddRange(new MySqlParameter[] {
                    //                        new MySqlParameter("C_CARTONID",MySqlDbType.VarChar){Value=C_CARTONID},
                    //                        new MySqlParameter("C_LINE",MySqlDbType.VarChar){Value=C_LINE},
                    //                        new MySqlParameter("C_WOID",MySqlDbType.VarChar){Value=C_WOID},
                    //                        new MySqlParameter("C_MCTN",MySqlDbType.VarChar){Value=C_MCTN},
                    //                        new MySqlParameter("C_FLAG",MySqlDbType.VarChar){Value=C_FLAG}
                    //                        });
                    //LsCmd.Add(cmd);
                    mst = new Dictionary<string, object>();
                    mst.Add("CARTONID", C_CARTONID);
                    mst.Add("LINEID", C_LINE);
                    mst.Add("WOID", C_WOID);
                    mst.Add("CARTONNUMBER", C_MCTN);
                    mst.Add("NUM", 0);
                    mst.Add("FLAG", C_FLAG);
                    dp.AddData(tx, "SFCR.T_CARTON_INFO_HAD", mst);
                }

                //cmd = new MySqlCommand();
                //cmd.CommandText = " UPDATE SFCR.T_CARTON_INFO_HAD SET NUM = @C_NUM + 1, FLAG = @C_FLAG WHERE CARTONID = @C_CARTONID ";
                //cmd.Parameters.AddRange(new MySqlParameter[] {
                //                           new MySqlParameter("C_NUM",MySqlDbType.Int32){Value=C_NUM},
                //                            new MySqlParameter("C_FLAG",MySqlDbType.VarChar){Value=C_FLAG},
                //                            new MySqlParameter("C_CARTONID",MySqlDbType.VarChar){Value=C_CARTONID}
                //});
                //LsCmd.Add(cmd);
                mst = new Dictionary<string, object>();
                mst.Add("NUM", C_NUM + 1);
                mst.Add("FLAG", C_FLAG);
                mst.Add("CARTONID", C_CARTONID);
                dp.UpdateData(tx, "SFCR.T_CARTON_INFO_HAD", new string[] { "CARTONID" }, mst);


                //cmd = new MySqlCommand();
                //cmd.CommandText = "INSERT INTO SFCR.T_CARTON_INFO_DTA(CARTONID, ESN, SN, MAC, WOID) VALUES (@C_CARTONID, @C_SN, 'NA', 'NA', @C_WOID) ";
                //cmd.Parameters.AddRange(new MySqlParameter[] {
                //                            new MySqlParameter("C_CARTONID",MySqlDbType.VarChar){Value=C_CARTONID},
                //                            new MySqlParameter("C_SN",MySqlDbType.VarChar){Value=C_SN.ToUpper()},
                //                            new MySqlParameter("C_WOID",MySqlDbType.VarChar){Value=C_WOID}
                //});
                //LsCmd.Add(cmd);

                // BLL.BllMsSqllib.Instance.ExecteNonQueryArr(LsCmd);
                mst = new Dictionary<string, object>();
                mst.Add("CARTONID", C_CARTONID);
                mst.Add("ESN", C_SN.ToUpper());
                mst.Add("SN", "NA");
                mst.Add("MAC", "NA");
                mst.Add("WOID", C_WOID);
                dp.AddData(tx, "SFCR.T_CARTON_INFO_DTA", mst);


                PRO_INSERTPALLETINFO(C_WOID, CARTONID, C_LINE, C_PARTNUMBER, "1", C_NUM.ToString(), C_FLAG, "NA", out C_RES);

                if (C_FLAG == "1")
                {
                    //cmd = new MySqlCommand();
                    //cmd.CommandText = "  SELECT MAX(CARTONID)  FROM SFCR.T_CARTON_INFO_HAD WHERE WOID = @C_WOID";
                    //cmd.Parameters.Add("C_WOID", MySqlDbType.VarChar).Value = C_WOID;
                    //C_NEXTCARTONID = BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0].Rows[0][0].ToString();
                    mst = new Dictionary<string, object>();
                    mst.Add("WOID", C_WOID);
                    C_NEXTCARTONID = dp.GetData("SFCR.T_CARTON_INFO_HAD", "MAX(CARTONID)", mst, out count).Tables[0].Rows[0][0].ToString();
                    C_NEXTCARTONID = C_WOID + "C" + (Convert.ToInt32(C_NEXTCARTONID.Substring(C_NEXTCARTONID.Length - 4, 4)) + 1).ToString().PadLeft(4, '0');
                    C_RES = "BOXCLOSE:" + C_NEXTCARTONID + ":" + (INUM + 1).ToString() + ":" + C_CARTONQTY.ToString();
                }
                else
                    C_RES = "OK:" + (INUM + 1).ToString() + ":" + C_CARTONQTY.ToString();

                tx.Commit();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                C_RES = ex.Message;
                Insert_Exception_Log("PRO_UPDATE_CARTON_BOX " + ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            return C_RES;

        }


//        public void Get_WO_LOT(string WO,out string RES,out string LOTID)
//        {
//            RES = "OK";
//            LOTID = "NA";
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = "SELECT LOTID,QTY,INPUTQTY,LOTSTATUS FROM MESDB.T_WO_LOT WHERE WOID=@WO ";
//            cmd.Parameters.Add("WO", MySqlDbType.VarChar).Value = WO;
//            DataTable dt = BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];
//            if (dt.Rows.Count > 0)
//            {
//                DataTable dtPrdt = getNewTable(dt, "LOSTATUS='PRDT'");
//                DataTable dtWfrl = getNewTable(dt, "LOSTATUS='WFRL'");
//                if (dtPrdt.Rows.Count > 0)
//                {
//                    LOTID = dtPrdt.Rows[0]["LOTID"].ToString();
//                    return;
//                }
//                else
//                {
//                    if (dt.Rows.Count > 0)
//                    {
//                        LOTID = dtWfrl.Rows[0]["LOTID"].ToString();
//                        return;
//                    }
//                }
//            }
//        }

//        public void UPDATE_WO_LOT(string WO,string LOTID,string C_ESN,string MYGROUP, out string RES)
//        {
//            int C_COUNT;
//            RES = "OK";
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = " SELECT  COUNT(1) FROM SFCR.T_WIP_DETAIL_A WHERE  WOID=@WO AND ESN = @C_ESN   AND LOCSTATION = @MYGROUP";
//            cmd.Parameters.AddRange(new MySqlParameter[] { 
//                                        new MySqlParameter("WO",MySqlDbType.VarChar){Value=WO}, 
//                                        new MySqlParameter("C_ESN",MySqlDbType.VarChar){Value=C_ESN}, 
//                                        new MySqlParameter("MYGROUP",MySqlDbType.VarChar){Value=MYGROUP}, 
//             });
//            C_COUNT = Convert.ToInt32(BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0].Rows[0][0].ToString());
//            if (C_COUNT == 0)
//            {
//                cmd = new MySqlCommand();
//                cmd.CommandText = "UPDATE MESDB.T_WO_LOT SET INPUTQTY=INPUTQTY+1 WHERE LOTID=@LOTID AND WOID=@WOID";
//                cmd.Parameters.AddRange(new MySqlParameter[] { 
//                                        new MySqlParameter("WOID",MySqlDbType.VarChar){Value=WO}, 
//                                        new MySqlParameter("LOTID",MySqlDbType.VarChar){Value=LOTID}                                         
//             });

//                BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

//                cmd = new MySqlCommand();
//                cmd.CommandText = "UPDATE MESDB.T_WO_LOT SET LOTSTATUS='COMP' WHERE  LOTID=@LOTID AND WOID=@WOID AND INPUTQTY=QTY ";
//                cmd.Parameters.AddRange(new MySqlParameter[] { 
//                                        new MySqlParameter("WOID",MySqlDbType.VarChar){Value=WO}, 
//                                        new MySqlParameter("LOTID",MySqlDbType.VarChar){Value=LOTID}                                         
//             });

//                BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
//            }
//        }

        private DataTable GetData(string table, string fieldlist,Dictionary<string,object> mst)
        {       
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);           
            return dp.GetData(table, fieldlist, mst, out count).Tables[0];
        }

        private void UpdateData(string table, string[] fieldlist, Dictionary<string, object> mst)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            dp.UpdateData(table, fieldlist, mst ) ;
        }
        private void InsertData(string table, Dictionary<string, object> mst)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            dp.AddData(table, mst);
        }
        public void PRO_QC_Check(string pn, string craftid, string woid, string esn, string routId)
        {
                int check_qty = 0;
                int sum_qty = 0;
                int count = 0;
                string table = "MESDB.B_FQC_CHECKRULES_SETUP".ToUpper();
                string fieldlist = "LOTQTY".ToUpper();
                string filter = "WOID={0} and STATIONNO={1} AND ROUTE_ID={2}";
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("WOID", woid);
                mst.Add("STATIONNO", craftid);
                mst.Add("ROUTE_ID", routId);
                DataTable _dt = dp.GetData(table, fieldlist, filter, mst, null, null, out count).Tables[0];
                if (_dt.Rows.Count == 0)
                {
                    filter = "PARTNUMBER={0} and STATIONNO={1} AND ROUTE_ID={2}";
                    dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                    mst = new Dictionary<string, object>();
                    mst.Add("PARTNUMBER", pn);
                    mst.Add("STATIONNO", craftid);
                    mst.Add("ROUTE_ID", routId);
                    _dt = dp.GetData(table, fieldlist, filter, mst, null, null, out count).Tables[0];
                    if (_dt.Rows.Count == 0)
                    {
                        table = "sfcb.b_product a,MESDB.B_FQC_CHECKRULES_SETUP b".ToUpper();
                        fieldlist = "B.LOTQTY".ToUpper();
                        filter = "A.PARTNUMBER={0} and B.STATIONNO={1} AND B.ROUTE_ID={2} AND A.PRODUCTNAME=B.PRODUCTNAME ";
                        dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                        mst = new Dictionary<string, object>();
                        mst.Add("PARTNUMBER", pn);
                        mst.Add("STATIONNO", craftid);
                        mst.Add("ROUTE_ID", routId);
                        _dt = dp.GetData(table, fieldlist, filter, mst, null, null, out count).Tables[0];
                    }
                }

                if (_dt.Rows.Count > 0)
                {
                    check_qty =Convert.ToInt32(_dt.Rows[0][0].ToString());
                    table = "sfcr.t_station_recount".ToUpper();
                    fieldlist = "sum(passqty+failqty)".ToUpper();
                    filter = "woid={0} and craftid={1}";
                    dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                    mst = new Dictionary<string, object>();
                    mst.Add("woid", woid);
                    mst.Add("craftid", craftid);
                    sum_qty = Convert.ToInt32(dp.GetData(table, fieldlist, filter, mst, null, null, out count).Tables[0].Rows[0][0].ToString());
                    if (sum_qty % check_qty == 0 && sum_qty!=0)
                    {

                        fieldlist = "WIPSTATION={0},NEXTSTATION={1}";
                        table = "sfcr.t_wip_tracking_online";
                        filter = "WOID = {0} AND ESN={1}";

                        IDictionary<string, object> modFields = new Dictionary<string, object>();
                        modFields.Add("WIPSTATION", "QC_CHECK");
                        modFields.Add("NEXTSTATION", "QC_CHECK");
                        IDictionary<string, object> keyVals = new Dictionary<string, object>();
                        keyVals.Add("WOID", woid);
                        keyVals.Add("ESN", esn);
                        TransactionManager.UpdateBatchData(table, fieldlist, modFields, filter, keyVals);
                    }
                }
        }

        private IList<IDictionary<string, object>> DataTable_To_ListDictionary(DataTable dt)
        {
            IList<IDictionary<string, object>> LsDic = new List<IDictionary<string, object>>();
            Dictionary<string, object> dic = null;
            string Colnum=string.Empty;
            foreach (DataColumn dc  in dt.Columns)
            {
                Colnum += dc.ColumnName + ",";
            }
            foreach (DataRow dr in dt.Rows)
            {
                dic = new Dictionary<string, object>();
                foreach (string Str in Colnum.Split(','))
                {                   
                    if (!string.IsNullOrEmpty(Str))
                    {
                        if (!string.IsNullOrEmpty(dr[Str].ToString()))
                          dic.Add(Str,dr[Str].ToString());
                    }                    
                }
                LsDic.Add(dic);
            }
            return LsDic;
        }

        public string CALL_PRO_INPUT_SN_FIRST(IDictionary<string, object> parms)
        {
            try
            {
                IBaseProvider dp = new BaseProvider();      
                IDictionary<string, object> procedureOutRes = new Dictionary<string, object>();
                procedureOutRes.Add("RES", (object)200);       
                dp.StoreProcedureExec("sfcb.PRO_INPUT_SN_FIRST", parms, procedureOutRes);
                return procedureOutRes["RES"].ToString();
            }
            catch (Exception ex)
            {
               return ex.Message;
            }
        }

        private string CHECK_WO(IDictionary<string, object> Dic)
        {
            string _StrErr = string.Empty;
            CHECK_WO(Dic["DATA"].ToString(), out _StrErr);
            return _StrErr;
        }
        public void CHECK_WO(string DATA,out string RES)
        {
            DataSet ds = woinfo.GetWoInfo(DATA, null,"WOID");
           if (ds.Tables[0].Rows.Count > 0)
           {
               RES = "OK";
           }
           else
           {
               RES = "WO NOT FOUND";
           }
        }
        public void Insert_Exception_Log(string StrLog)
        {
            _FileHelper.Insert_Exception_Log(StrLog);
        }
        public void Insert_DB_Log(string StrLog)
        {
            _FileHelper.Insert_DB_Log(StrLog);
        }

        private void UPDATE_WO_FIRST_PCS_TIME(string WOID)
        {
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", WOID);
            DataTable dt = dp.GetData("SFCR.T_WIP_TRACKING_ONLINE", "COUNT(1)", mst, out count).Tables[0];
            if (Convert.ToInt32(dt.Rows[0][0].ToString()) == 1)
            {
                mst.Add("FSINPUTTIME", System.DateTime.Now);
                dp.UpdateData("SFCR.T_WO_INFO", new string[] { "WOID" }, mst);
            }
        }
    }
}

