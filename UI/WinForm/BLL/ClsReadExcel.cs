using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

using MyCore= Microsoft.Office.Core;
using MyExcel = Excel;
using MyOffice = Microsoft.Office;
using System.Reflection;
using System.IO;


namespace FrmBLL
{
   public class ClsReadExcel
    {
       /// <summary>
       /// 读取Excel文件转换为Datatable
       /// </summary>
       /// <param name="_excelName">文件名称包含绝对路径</param>
       /// <param name="SheetName">表的名称</param>
       /// <returns></returns>
       public static DataTable getTable(string _excelName, string SheetName)
       {
           try
           {
               string conn = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'",
                   _excelName);
               string sql = string.Format("select * from [{0}]", SheetName);
               using (OleDbCommand cmd = new OleDbCommand(sql, new OleDbConnection(conn)))
               {
                   OleDbDataAdapter ad = new OleDbDataAdapter(cmd);
                   DataSet ds = new DataSet();
                   ad.Fill(ds);
                   return ds.Tables[0];
               }
           }
           catch
           {
               throw new Exception("读取Excel文档出错");
           }
       }

       /// <summary>
       /// 在Excel中执行SQL命令读取Sheet
       /// </summary>
       /// <param name="_excelName">文件路径含名称</param>
       /// <param name="_sql">sql命令</param>
       /// <returns>返回一个datatable</returns>
       public static DataTable getTableForSql(string _excelName, string _sql)
       {
           try
           {
               string conn = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'",
                        _excelName);
               using (OleDbCommand cmd = new OleDbCommand(_sql, new OleDbConnection(conn)))
               {
                   OleDbDataAdapter ad = new OleDbDataAdapter(cmd);
                   DataSet ds = new DataSet();
                   ad.Fill(ds);
                   return ds.Tables[0];
               }
           }
           catch
           {
               throw new Exception("读取Excel文档出错");
           }
       }
       /// <summary>
       /// 合并Excel多个Sheet(表身)
       /// </summary>
       /// <param name="_excelName">Excel文件</param>
       /// <param name="_SheetName">Sheet名称</param>
       /// <returns></returns>
       public static DataTable getTableDta(string _excelName,string[] _SheetName)
       {
           if (_SheetName.Length < 1)
               return null;
           string sql = string.Empty;
           string par = "union all";
           foreach (string item in _SheetName)
           {
               sql += string.Format(" select * from [{0}] {1} ", item,par);
           }
           sql = sql.Remove(sql.LastIndexOf(par), par.Length);
           string conn = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'",
               _excelName);
           using (OleDbCommand cmd = new OleDbCommand(sql, new OleDbConnection(conn)))
           {
               OleDbDataAdapter ad = new OleDbDataAdapter(cmd);
               DataSet ds = new DataSet();
               ad.Fill(ds);
               return ds.Tables[0];
           }
       }

       /// <summary>
       /// 合并Excel多个Sheet(表头)(专用)
       /// </summary>
       /// <param name="_excelName">Excel文件</param>
       /// <param name="_SheetName">Sheet名称</param>
       /// <returns></returns>
       public static DataTable getTableHad(string _excelName, string[] _SheetName)
       {
           if (_SheetName.Length < 1)
               return null;
           string sql = string.Empty;
           string softname = string.Empty;
           string par = "union all";
           foreach (string item in _SheetName)
           {
               string side = string.Empty;
               try
               {
                   softname = item.Split(' ')[1].Replace('$',' ').Replace('\'',' ').Trim();
                   //softname = softname.Substring(0, softname.Length - 2);
                   side = item.Split('-')[1].Substring(0, 1);
               }
               catch
               {
                   throw new Exception("料站表的命名规则不符,请重新修正");
               }
               sql += string.Format(" select 产品料号,产品描述,机器编号,BOM版本,料站总数,count(*) as 料站数,'{2}' as PCB面,'{3}' AS SMT程式 from [{0}] group by 产品料号,产品描述,机器编号,BOM版本,料站总数,'{3}'  having 机器编号 is not null {1} ", item, par, side, softname.Trim());
           }
           sql = sql.Remove(sql.LastIndexOf(par), par.Length);
           string conn = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'",
               _excelName);
           using (OleDbCommand cmd = new OleDbCommand(sql, new OleDbConnection(conn)))
           {
               OleDbDataAdapter ad = new OleDbDataAdapter(cmd);
               DataSet ds = new DataSet();
               ad.Fill(ds);
               return ds.Tables[0];
           }
       }

       /// <summary>
       /// 获取一个Excel文件中有多少张表
       /// </summary>
       /// <param name="_excelName">Excel文件路径:包含绝对路径</param>
       /// <returns>返回表的集合</returns>
       public static List<string> GetTableNames(string _excelName)
       {
           try
           {
               List<string> tablenames = new List<string>();
               string conn = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'",
                   _excelName);
               using (OleDbConnection cnn = new OleDbConnection(conn))
               {
                   cnn.Open();
                   DataTable dt = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                   string[] sheets = new string[dt.Rows.Count];
                   for (int i = 0; i < dt.Rows.Count; i++)
                   {
                       tablenames.Add(dt.Rows[i]["TABLE_NAME"].ToString().Trim());
                   }
                   cnn.Close();
                   return tablenames;
               }
           }
           catch
           {
               throw new Exception("资料表获取失败");
           }
       }

       /// <summary>
       /// Excel文件打印预览
       /// </summary>
       /// <param name="ExcelFile">文件路径包含文件名称</param>
       public static void ExcelPreview(string ExcelFile)
       {
           try
           {
               Excel.Application xlsApp = new Excel.Application();
               if (xlsApp == null)
                   throw new Exception("无法创建Excel对象，可能您的计算机未安装Excel");
               Excel.Workbooks xlsWbs = xlsApp.Workbooks;
               Excel.Workbook xlsWb = xlsWbs.Open(
                   ExcelFile, Missing.Value, Missing.Value,
                   Missing.Value, Missing.Value, Missing.Value,
                   Missing.Value, Missing.Value, Missing.Value,
                   Missing.Value, Missing.Value, Missing.Value, Missing.Value);
               xlsApp.Visible = true;
               xlsWb.PrintPreview(false);
               xlsWb = null;
               xlsApp.Quit();
               xlsApp = null;
           }
           catch(Exception ex)
           {
               throw new Exception(ex.Message + "Excel文件操作失败");
           }
       }

       /// <summary>
       /// 判定文件是否存在
       /// </summary>
       /// <param name="FileName">文件名称包含绝对路径</param>
       /// <returns>如果文件存在则返回文件路径和文件名</returns>
       public static string FileExists(string FileName)
       {
           if (File.Exists(FileName))
               return FileName;
           else
               throw new Exception(string.Format("指定的文件{0}不存在", FileName));
       }

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
       /// <summary>
       /// 将datatable的数据导出到excel
       /// </summary>
       /// <param name="mdt"></param>
       private void DataToExcel(DataTable mdt)
       {
           Excel.ApplicationClass oExcel = new Excel.ApplicationClass();
           Excel.Workbooks oBooks = oExcel.Workbooks;

           Excel._Workbook oBook = null;

           oBook = (Excel._Workbook)(oExcel.Workbooks.Add(true));// 引用excel工作薄 

           for (int i = 0; i < mdt.Columns.Count; i++)
           {
               oExcel.Cells[2, i + 1] = mdt.Columns[i].ColumnName.ToString();// m_DataView.Columns[i].HeaderText.ToString();
           }

           for (int i = 0; i < mdt.Rows.Count; i++)
           {
               for (int j = 0; j < mdt.Columns.Count; j++)
               {
                   oExcel.Cells[i + 3, j + 1] = mdt.Rows[i][j].ToString();
               }
           }

           oExcel.Visible = true;
           object Missing = System.Reflection.Missing.Value;
           //  oExcel.Run("Sheet1.printdoc", Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing);

           oBook.Application.DisplayAlerts = false;



       }
       public static DataSet getDataSet(string _excelName, string SheetName)
       {
           try
           {
               string conn = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'",
                   _excelName);
               string sql = string.Format("select * from [{0}]", SheetName);
               using (OleDbCommand cmd = new OleDbCommand(sql, new OleDbConnection(conn)))
               {
                   OleDbDataAdapter ad = new OleDbDataAdapter(cmd);
                   DataSet ds = new DataSet();
                   ad.Fill(ds);
                   return ds;
               }
           }
           catch
           {
               throw new Exception("读取Excel文档出错");
           }
       }
    }
}
