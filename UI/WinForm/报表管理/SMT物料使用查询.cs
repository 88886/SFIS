using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class fmMaterialQuery : Office2007Form // Form
    {
        public fmMaterialQuery(MainParent info)
        {
            InitializeComponent();
            sFinfo = info;
       
        }

        MainParent sFinfo;
    
        private void btQuery_Click(object sender, EventArgs e)
        {
            dataGridViewX1.DataSource = "";
            if (!string.IsNullOrEmpty(edwo.Text) || !string.IsNullOrEmpty(edpn.Text))
            {
                #region  之前的方法
                //if (cksum.Checked==false)
                //{
                //    sSQL = "";                    

                //    if (!string.IsNullOrEmpty(edwo.Text))
                //    {
                //        sSQL = sSQL + string.Format(" and woid='{0}'", edwo.Text.Trim());
                //    }
                //    if (!string.IsNullOrEmpty(edpn.Text))
                //    {
                //        sSQL = sSQL + string.Format(" and kpnumber='{0}'", edpn.Text.Trim());
                //    }
                //    if (!string.IsNullOrEmpty(edvender.Text))
                //    {
                //        sSQL = sSQL + string.Format(" and vendercode='{0}'", edvender.Text.Trim());
                //    }
                //    if (!string.IsNullOrEmpty(eddatecode.Text))
                //    {
                //        sSQL = sSQL + string.Format(" and datecode='{0}'", eddatecode.Text.Trim());
                //    }
                //    if (!string.IsNullOrEmpty(edlotid.Text))
                //    {
                //        sSQL = sSQL + string.Format(" and lotid='{0}'", edlotid.Text.Trim());
                //    }

                //    sSQL = sSQL.Substring(5,sSQL.Length-5);
                //    sSQL = "select userid as 用户,masterid as 料表编号,woid as 工单,machineid as 机器代码,stationid as 料站号,feederid as Feeder,kpnumber as 料号,modelname as 产品型号," +
                //         "vendercode as 厂商代码,datecode as 生产周期,lotid as 生产批次,lotqty as 数量,kp_sn as KP_SN,data as 上料命令,pcbside as PCB面,inputtime as 刷入时间 from tsmtkpnormallog where " +sSQL;
                //    dataGridViewX1.DataSource =refWebExecuteSqlCmd.Instance.ExecuteQuerySQL(sSQL);
                //}
                //else
                //{
                //    sSQL = "";
                //    string sCON = "";
                //    string sConName = "";

                  
                //    if (!string.IsNullOrEmpty(edwo.Text))
                //    {
                //        sCON =sCON+ " ,woid";
                //        sConName = sConName + " ,woid as 工单";
                //        sSQL =sSQL+ string.Format( " and woid='{0}'",edwo.Text.Trim());
                //    }
                //    if (!string.IsNullOrEmpty(edpn.Text))
                //    {
                //        sCON =sCON+" ,kpnumber";
                //        sConName = sConName  + " ,kpnumber as 料号";
                //        sSQL =sSQL+ string.Format(" and kpnumber='{0}'",edpn.Text.Trim());
                //    }
                //    if (!string.IsNullOrEmpty(edvender.Text))
                //    {
                //        sCON = sCON + " ,vendercode";
                //        sConName = sConName + " ,vendercode as 厂商代码";
                //        sSQL = sSQL + string.Format(" and vendercode='{0}'", edvender.Text.Trim());
                //    }
                //    if (!string.IsNullOrEmpty(eddatecode.Text))
                //    {
                //        sCON = sCON + " ,datecode";
                //        sConName = sConName + " ,datecode as 生产周期";
                //        sSQL = sSQL + string.Format(" and datecode='{0}'",eddatecode.Text.Trim());
                //    }
                //    if (!string.IsNullOrEmpty(edlotid.Text))
                //    {
                //        sCON = sCON + " ,lotid";
                //        sConName = sConName + " ,lotid as 生产批次";
                //        sSQL = sSQL + string.Format(" and lotid='{0}'",edlotid.Text.Trim());
                //    }

                //    sCON = sCON.Substring(2, sCON.Length - 2);
                //    sConName = sConName.Substring(2, sConName.Length - 2);
                //    sSQL = sSQL.Substring(5, sSQL.Length - 5);

                //    sSQL = "select " + sConName + ",sum(lotqty) 数量 from tsmtkpnormallog where  " + sSQL + " group by " + sCON;
                //    dataGridViewX1.DataSource = refWebExecuteSqlCmd.Instance.ExecuteQuerySQL(sSQL);

                //}

                #endregion

                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(edwo.Text.Trim()))
                    dic.Add("WOID", edwo.Text.Trim());
                if (!string.IsNullOrEmpty(edpn.Text.Trim()))
                    dic.Add("KPNUMBER", edpn.Text.Trim());
                if (!string.IsNullOrEmpty(edvender.Text.Trim()))
                    dic.Add("VENDERCODE", edvender.Text.Trim());
                if (!string.IsNullOrEmpty(eddatecode.Text.Trim()))
                    dic.Add("DATECODE", eddatecode.Text.Trim());
                if (!string.IsNullOrEmpty(edlotid.Text.Trim()))
                    dic.Add("LOTID", edlotid.Text.Trim());
                dataGridViewX1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtKpNormalLog.Instance.QuerytSmtKpNormallog(FrmBLL.ReleaseData.DictionaryToJson(dic), cksum.Checked));
                //dataGridViewX1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtKpNormalLog.Instance.QuerytSmtKpNormallog(new WebServices.tSmtKpNormalLog.tSmtKpNormalLog1()
                //    {
                //        woId = edwo.Text.Trim(),
                //        kpnumber = edpn.Text.Trim(),
                //        vendercode = edvender.Text.Trim(),
                //        datecode = eddatecode.Text.Trim(),
                //        lotId = edlotid.Text.Trim(),
                //        ShowTotal = cksum.Checked
                //    }));

            }
            else
            {
                sFinfo.ShowPrgMsg("工单或料号不能为空",MainParent.MsgType.Error);
            }
        }

        private void btTo_Excel_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.RowCount != 0)
            {
                DataToExcel(dataGridViewX1);

            }
            else
            {
                sFinfo.ShowPrgMsg("没有资料可以导出!!!", MainParent.MsgType.Error);
            }
        }

        private void DataToExcel(DataGridView m_DataView)
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
                MessageBox.Show(this, "保存EXCEL成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sFinfo.ShowPrgMsg("保存EXCEL成功 ", MainParent.MsgType.Incoming);
            }
        }

        private void dataGridViewX1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                this.dataGridViewX1[e.ColumnIndex, e.RowIndex].ToolTipText =
                    string.Format("当前累计:[{0}]笔数据", this.dataGridViewX1.Rows.Count);
                this.dataGridViewX1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 20); 
            }
        }

        private void dataGridViewX1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                this.dataGridViewX1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void dataGridViewX1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridViewX1.RowHeadersDefaultCellStyle.ForeColor))
            {
                int linen = 0;
                linen = e.RowIndex + 1;
                string line = linen.ToString();
                e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X, e.RowBounds.Location.Y + 5);
                SolidBrush B = new SolidBrush(Color.Red);
            }
        }



        private void fmMaterialQuery_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.sFinfo.gUserInfo.rolecaption == "系统开发员")
            {
                IList<IDictionary<string, object>> lsfunls = new List<IDictionary<string, object>>();
                FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("PROGID", this.Name);
                dic.Add("PROGNAME", this.Text);
                dic.Add("PROGDESC", this.Text);
                FrmBLL.publicfuntion.AddProgInfo(dic, lsfunls);
            }
            #endregion
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
  

        
    }
}
