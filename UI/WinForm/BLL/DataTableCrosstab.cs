using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace FrmBLL
{
    public class DataTableCrosstab
    {
        DataTableCrosstab()
        {
        }

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



        public static DataTable Dt_TableCross(DataTable dt)
        {
            DataView DT_KEYPARTView = new DataView(dt);
            DataTable ESNList;
            DataView EsnListView=dt.DefaultView;

            ESNList = EsnListView.ToTable(true, "ESN", "PRODUCTNAME", "WOID", "partnumber", "cartonnumber", "mcartonnumber", "palletnumber", "mpalletnumber", "入库日期", "出库日期");
            DataTable examType;
            examType = dt.DefaultView.ToTable(true, "SNTYPE");
            #region 构建表结构
            DataTable crossScoreTable = new DataTable();

            foreach (DataColumn item in ESNList.Columns)
            {
                crossScoreTable.Columns.Add(item.ColumnName, item.DataType);
            }

            foreach (DataRow r in examType.Rows)
            {
                crossScoreTable.Columns.Add(r[0].ToString(), typeof(string));
            }
            #endregion
            DataColumnCollection cols = crossScoreTable.Columns;
            foreach (DataRow r in ESNList.Rows)
            {
                DataRow newRow = crossScoreTable.NewRow();
                //newRow["机型"] = r["PRODUCTNAME"];
                //newRow["ESN"] = r["ESN"];

                for (int j = 0; j < cols.Count - examType.Rows.Count; j++)
                {
                    newRow[cols[j]] = r[cols[j].ToString()];
                }
                for (int i = 10; i < cols.Count; i++)
                {
                    DT_KEYPARTView.RowFilter = String.Format("ESN='{0}' AND SNTYPE='{1}'", r[0], examType.Rows[i - 10][0]);
                    if (DT_KEYPARTView.Count > 0)
                    {
                        newRow[cols[i]] = DT_KEYPARTView[0]["SNVAL"];
                    }
                }
                // 加入新表
                crossScoreTable.Rows.Add(newRow);
            }


            return crossScoreTable;
        }



  


    }
}
