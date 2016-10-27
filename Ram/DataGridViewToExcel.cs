using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace FrmBLL
{
  public  class DataGridViewToExcel
    {
      DataGridViewToExcel()
      {
      }

      public static void DataToExcel(DataGridView m_DataView)
      {
          SaveFileDialog kk = new SaveFileDialog();
          kk.Title = "保存EXECL文件";
          //     kk.Filter = "EXECL文件(*.xls)|*.xls|所有文件(*.*) |*.*";
          kk.Filter = "EXECL 97-2003工作薄|*.xls|所有文件(*.*) |*.*";
          kk.FilterIndex = 1;
          if (kk.ShowDialog() == DialogResult.OK)
          {
              string FileName = kk.FileName;// +".xls";
              if (File.Exists(FileName))
                  File.Delete(FileName);
              FileStream objFileStream;
              StreamWriter objStreamWriter;
              string strLine = "";
              objFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
              objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
              for (int i = 0; i < m_DataView.Columns.Count; i++)
              {
                  if (m_DataView.Columns[i].Visible == true)
                  {
                      strLine = strLine + m_DataView.Columns[i].HeaderText.ToString() + Convert.ToChar(9);
                  }
              }
              objStreamWriter.WriteLine(strLine);
              strLine = "";

              for (int i = 0; i < m_DataView.Rows.Count; i++)
              {
                  if (m_DataView.Columns[0].Visible == true)
                  {
                      if (m_DataView.Rows[i].Cells[0].Value == null)
                          strLine = strLine + " " + Convert.ToChar(9);
                      else
                          strLine = strLine + m_DataView.Rows[i].Cells[0].Value.ToString() + Convert.ToChar(9);
                  }
                  for (int j = 1; j < m_DataView.Columns.Count; j++)
                  {
                      if (m_DataView.Columns[j].Visible == true)
                      {
                          if (m_DataView.Rows[i].Cells[j].Value == null)
                              strLine = strLine + " " + Convert.ToChar(9);
                          else
                          {
                              string rowstr = "";
                              rowstr = m_DataView.Rows[i].Cells[j].Value.ToString();
                              if (rowstr.IndexOf("\r\n") > 0)
                                  rowstr = rowstr.Replace("\r\n", " ");
                              if (rowstr.IndexOf("\t") > 0)
                                  rowstr = rowstr.Replace("\t", " ");
                              strLine = strLine + rowstr + Convert.ToChar(9);
                          }
                      }
                  }
                  objStreamWriter.WriteLine(strLine);
                  strLine = "";
              }
              objStreamWriter.Close();
              objFileStream.Close();

          }
      }
    }
}
