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
using FrmBLL;

namespace SFIS_V2
{
    public partial class Monitor : Office2007Form// Form
    {
        public Monitor(MainParent sForm)
        {
            InitializeComponent();
            sInfo = sForm;
        }

        MainParent sInfo;

        public readonly bool Check_Mo = true;
        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };
        /// <summary>
        /// 显示消息函数
        /// </summary>
        /// <param name="mLogMsgType"></param>
        /// <param name="msg"></param>
        public void ShowMsg(string msg, mLogMsgType mLogMsgType)
        {
            try
            {
                this.rtbmsg.Invoke(new EventHandler(delegate
                {
                    rtbmsg.TabStop = false;
                    rtbmsg.SelectedText = string.Empty;
                    rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                    rtbmsg.SelectionColor = mLogMsgTypeColor[(int)mLogMsgType];
                    rtbmsg.AppendText(msg + "\n");
                    rtbmsg.ScrollToCaret();
                }));
            }
            catch
            {
            }
        }

        bool sSEQ_MO = false;
        bool Old_Material_tray = false;
        string Old_Tr_sn = string.Empty;
        string sSEQ = "";
        string sMO = "";
        string sKp_No = "";
        string sStation = "";
        string sRowid = "";
      //  DataTable dtlist=null;
        

        System.Windows.Forms.Timer TimeCom = new System.Windows.Forms.Timer();
        private void Monitor_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.sInfo.gUserInfo.rolecaption == "系统开发员")
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

            dataGridViewX1.AutoGenerateColumns = false;
     

            QueryMaterialMonitor();
            QueryLineList();
            Scan_Data.SelectAll();
            Scan_Data.Focus();
            cblistline.Text = "ALL";

        }
           


        private DataTable mdt = null;

        IAsyncResult iasyncresult;
        private delegate void refquerymaterialmonitor();
        refquerymaterialmonitor eventquerymaterialmonitor;
        private void QueryMaterialMonitor()
        {
            dataGridViewX1.Invoke(new EventHandler(delegate
            {
                 mdt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtKpMonitor.Instance.RefreshMaterialMonitor());

                DataTable dt = mdt;               
                DataView  dv = new DataView(dt);
                dv.Sort = "CDATA asc";
                dataGridViewX1.DataSource = dv.ToTable();
                label1.Text = "上次刷新时间: " + DateTime.Now.ToString();

                for (int x = 0; x < this.dataGridViewX1.Rows.Count;x++ )
                {

                    if ((dataGridViewX1.Rows[x].Cells["标志位"].Value.ToString() == "0") && Convert.ToInt32(dataGridViewX1.Rows[x].Cells["未补料时间"].Value.ToString()) > 10)
                    {
                        dataGridViewX1.Rows[x].DefaultCellStyle.BackColor = Color.Red;
                    }
                    if (dataGridViewX1.Rows[x].Cells["标志位"].Value.ToString() == "1")
                    {

                        dataGridViewX1.Rows[x].DefaultCellStyle.BackColor = Color.FromArgb(10, 222, 41);

                        if (int.Parse(dataGridViewX1.Rows[x].Cells["未换料时间"].Value.ToString()) > 10)
                        {
                            dataGridViewX1.Rows[x].DefaultCellStyle.BackColor = Color.LightSalmon;
                        }
                    }
                }

            }));

               
               
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            QueryMaterialMonitor();
          
        }

        private void QueryLineList()
        {
            cblistline.Items.Add("ALL");
            //sSQL = "select distinct lineid from tMachineInfo";
            //DataTable ListLine = refWebExecuteSqlCmd.Instance.ExecuteQuerySQL(sSQL);
            string[] ListLine =refWebtMachineInfo.Instance.GetSMTLineList();
            foreach (string item in ListLine)
            {
                cblistline.Items.Add(item);
            }
        }
    

        private void itemautorefresh_Click(object sender, EventArgs e)
        {
            if (itemautorefresh.Checked)
            {
                itemautorefresh.Checked = false;
                timer1.Enabled = false;
            }
            else
            {
                itemautorefresh.Checked = true;
                timer1.Enabled = true;
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

        private void cblistline_SelectionChangeCommitted(object sender, EventArgs e)
        {
      
            this.dataGridViewX1.DataSource = null;
            if (cblistline.Text == "ALL")
            {
                this.dataGridViewX1.DataSource = this.mdt;
                return;
            }
            //this.dataGridViewX1.Rows[1].DefaultCellStyle.BackColor = Color.Red;
            this.dataGridViewX1.DataSource =  publicfuntion.getNewTable(this.mdt,string.Format("lineId = '{0}'", cblistline.Text));
        }

     

        private void button2_Click(object sender, EventArgs e)
        {

            for (int x = 0; x < dataGridViewX1.RowCount; x++)
            {
                if (dataGridViewX1.Rows[x].Visible == false)
                {
                    dataGridViewX1.Rows[x].Visible = true;
                }
            }
        }       

        

        private void dataGridViewX1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            /*
            //for (int i = 0; i < dataGridViewX1.RowCount; i++)
            //{
            //    if (dataGridViewX1[6, i].Value.ToString() == "0" && Convert.ToInt32(dataGridViewX1[11, i].Value.ToString())>10)
            //    {
            //        dataGridViewX1.Rows[i].DefaultCellStyle.BackColor = Color.Red;                  
            //    }
            //}

            DataGridViewRow dgr = dataGridViewX1.Rows[e.RowIndex];

            if ((dgr.Cells["标志位"].Value.ToString() == "0") && Convert.ToInt32(dgr.Cells["未补料时间"].Value.ToString()) > 10)
            {              
               // dataGridViewX1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                using (SolidBrush soldbrush = new SolidBrush(Color.Red))
                {
                    e.Graphics.FillRectangle(soldbrush, e.RowBounds);
                    e.PaintCellsContent(e.ClipBounds);
                    e.Handled = true;
                }
            }


            if (dgr.Cells["标志位"].Value.ToString() == "1")
            {
                using (SolidBrush soldbrush = new SolidBrush(Color.FromArgb(10, 222, 41)))
                {
                    e.Graphics.FillRectangle(soldbrush, e.RowBounds);
                    e.PaintCellsContent(e.ClipBounds);
                    e.Handled = true;
                }

               // dataGridViewX1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(10, 222, 41); 

                if (int.Parse(dgr.Cells["未换料时间"].Value.ToString()) > 10)
                {
                    using (SolidBrush soldbrush = new SolidBrush(Color.FromArgb(255, 77, 77)))
                 //   using (SolidBrush soldbrush = new SolidBrush(Color.LightSalmon))
                    {
                        e.Graphics.FillRectangle(soldbrush, e.RowBounds);
                        e.PaintCellsContent(e.ClipBounds);
                        e.Handled = true;
                    }
                  ///  dataGridViewX1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightSalmon; 
                }
            }

            //if (dgr.Cells["标志位"].Value.ToString() == "0")
            //{
            //    using (SolidBrush soldbrush = new SolidBrush(Color.FromArgb(198,241,251)))
            //    {
            //        e.Graphics.FillRectangle(soldbrush, e.RowBounds);
            //        e.PaintCellsContent(e.ClipBounds);
            //        e.Handled = true;
            //    }
            //}
      */
        }

        private void dataGridViewX1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void dataGridViewX1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && DeleteMaterialData==false)
            {
                Scan_Data.Text = "";
                labpn.Text = "";
           
                   
                    Scan_Data.Text = this.dataGridViewX1[0,e.RowIndex].Value.ToString() + " " + this.dataGridViewX1[1,e.RowIndex].Value.ToString();
                    sKp_No=this.dataGridViewX1[4,e.RowIndex].Value.ToString();
                    sStation = this.dataGridViewX1[5, e.RowIndex].Value.ToString();
                    //sSQL = string.Format("select * from tMaterialPreparation where masterid='{0}'  and woId='{1}' and stationno='{2}' ", dataGridViewX1[0, e.RowIndex].Value.ToString(), dataGridViewX1[1, e.RowIndex].Value.ToString(), dataGridViewX1[5, e.RowIndex].Value.ToString());
                    //DataTable dtm=  refWebExecuteSqlCmd.Instance.ExecuteQuerySQL(sSQL);
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("MASTERID", dataGridViewX1[0, e.RowIndex].Value.ToString());
                    dic.Add("WOID", dataGridViewX1[1, e.RowIndex].Value.ToString());
                    dic.Add("STATIONNO", dataGridViewX1[5, e.RowIndex].Value.ToString());
                    DataTable dtm = FrmBLL.ReleaseData.arrByteToDataTable(refWebtMaterialPreparation.Instance.GetKpdistinctMaterial(FrmBLL.ReleaseData.DictionaryToJson(dic)));
                    //DataTable dtm = FrmBLL.ReleaseData.arrByteToDataTable(refWebtMaterialPreparation.Instance.GetKpdistinctMaterial(new WebServices.tMaterialPreparation.tMaterialPreparation1()
                    //    {
                    //        masterId = dataGridViewX1[0, e.RowIndex].Value.ToString(),
                    //        woId = dataGridViewX1[1, e.RowIndex].Value.ToString(),
                    //        stationno = dataGridViewX1[5, e.RowIndex].Value.ToString()
                    //    }));

                    for (int z=0;z<dtm.Rows.Count;z++)
                    {
                        labpn.Text = labpn.Text + dtm.Rows[z]["KPNUMBER"].ToString() + ",";

                    }
                    Scan_Data_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    Scan_Data.SelectAll();
                    Scan_Data.Focus();
              
            }

          
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            if (iasyncresult == null || iasyncresult.IsCompleted)
            {
                eventquerymaterialmonitor = new refquerymaterialmonitor(this.QueryMaterialMonitor);
                iasyncresult = eventquerymaterialmonitor.BeginInvoke(null, null);
                Scan_Data.Focus();
                Scan_Data.SelectAll();
            }
        }

        
        private void UndoClearData()
        {
            sSEQ_MO=false;
            Old_Material_tray = false;
            sKp_No = "";
            sStation = "";
            sRowid = "";
            Scan_Data.SelectAll();
          
        }
        private void Scan_Data_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }


        bool DeleteMaterialData = false;
        private void DelMaterial_Click(object sender, EventArgs e)
        {
           
            string[] EmpData = Input.InputBox.ShowInputBox("输入权限", string.Empty);

            if (string.IsNullOrEmpty(EmpData[0]))
            {
                MessageBox.Show("用户名不能为空!!");
                return;
            }
            DataTable _dt =  FrmBLL.ReleaseData.arrByteToDataTable(refWebtUserInfo.Instance.GetUserInfoByUserId(EmpData[0]));
            if (_dt == null || _dt.Rows.Count < 1 || _dt.Rows[0]["pwd"].ToString() != EmpData[1])   
            {
                MessageBox.Show("用户名或密码输入错误\n请重新输入!!");
            }
            else
            {

                if ((_dt.Rows[0]["rolecaption"].ToString() == "线边仓主管") || (_dt.Rows[0]["rolecaption"].ToString() == "系统开发员"))
                {
                    DeleteMaterialData = true;
                    ShowMsg("输入权限正确,双击即可删除物料", mLogMsgType.Incoming);
                }
                else
                {
                    MessageBox.Show("只有【线边仓主管】 权限可以删除\n请重新输入!!");
                }

            }
        }

      

        private void dataGridViewX1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex != -1)
            {
              
                if (DeleteMaterialData == true)
                {
                    if (MessageBox.Show("确定要删除\r\n 料表: " + dataGridViewX1[0, e.RowIndex].Value.ToString() + " " + dataGridViewX1[1, e.RowIndex].Value.ToString() + "\r\n" + "料号: " + dataGridViewX1[4, e.RowIndex].Value.ToString() + "  料站: " + dataGridViewX1[5, e.RowIndex].Value.ToString(),
                        "删除提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        //sSQL = string.Format("update tSmtkPMonnitor set cdata='4'where kpmonitorid='{0}'", dataGridViewX1[13, e.RowIndex].Value.ToString());

                        //refWebExecuteSqlCmd.Instance.ExecteSQLNonQuery(sSQL);                      
                    
                        //refWebtSmtKpMonitor.Instance.UpdateSmtkPMonnitorCdata(new WebServices.tSmtKpMonitor.tSmtKpMonitor1()
                        //    {
                        //        cdata =4,
                        //        kpmonitorId = dataGridViewX1[13, e.RowIndex].Value.ToString()
                        //    });
                        refWebtSmtKpMonitor.Instance.UpdateSmtkPMonnitorCdata(dataGridViewX1[13, e.RowIndex].Value.ToString(), "4");

                        ShowMsg("删除物料完成", mLogMsgType.Incoming);
                        string TrSn = dataGridViewX1["TR_SN", e.RowIndex].Value.ToString();
                        if (!string.IsNullOrEmpty(TrSn))
                            refWebtR_Tr_Sn.Instance.Update_TR_SN(TrSn, "NA", sInfo.gUserInfo.userId, "2", "NA", "NA");
                        DeleteMaterialData = false;
                        QueryMaterialMonitor();
                        cblistline_SelectionChangeCommitted(null, EventArgs.Empty);

                    }
                    
                    
                }

            }
        }

        private void butQuery_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtwo.Text))
            {
                //sSQL = "";

                //if (chkSum.Checked == true)
                //{
                //    sSQL = "select woId as 工单 ,kpnumber as 料号,sum(qty) as 数量 from tSmtkPMonnitor";
                //}
                //else
                //{

                //    sSQL = "select masterid as SEQ,woid as 工单,machineid as 机器代码,kpnumber as 料号,stationno as 料站号码," +
                //         "vendercode as 厂商代码,datecode as 生产周期,lotid as 生产批次,qty as 数量,cdata as 标志位," +
                //         "scarcitytime as 缺料时间,scarcityuser as 刷缺料人员,supplytime as 补料时间,supplyuser as 补料人员 from tSmtkPMonnitor";
                //}
                //sSQL =sSQL+ string.Format("  where woid='{0}'", txtwo.Text);

                //if (!string.IsNullOrEmpty(txtpn.Text))
                //{
                //    sSQL = sSQL + string.Format(" and kpnumber='{0}'",txtpn.Text.Trim());
                //}               

                //sSQL = sSQL + " and (cdata='2' or cdata='3' or cdata='5')";

                //if (chkSum.Checked == true)
                //{
                //    sSQL = sSQL + " group by woId,kpnumber";
                //}
                //dataGridViewX2.DataSource =  refWebExecuteSqlCmd.Instance.ExecuteQuerySQL(sSQL);

                //dataGridViewX2.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtKpMonitor.Instance.QueryMaterialInOutPut(new WebServices.tSmtKpMonitor.tSmtKpMonitor1()
                //    {
                //        woId = txtwo.Text.Trim(),
                //        kpnumber = txtpn.Text.Trim(),
                //        ShowTatol = chkSum.Checked

                //    }));
                dataGridViewX2.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtKpMonitor.Instance.QueryMaterialInOutPut(txtwo.Text.Trim(), txtpn.Text.Trim(), chkSum.Checked));
              
            }
            else
            {
               sInfo.ShowPrgMsg("工单不能为空 ", MainParent.MsgType.Error );
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
                sInfo.ShowPrgMsg("保存EXCEL成功", MainParent.MsgType.Incoming);
            }
        }

        private void ToExcel_Click(object sender, EventArgs e)
        {
           
            if (dataGridViewX2.RowCount != 0)
            {
                DataToExcel(dataGridViewX2);

            }
            else
            {
              sInfo.ShowPrgMsg("没有资料可以导出!!!",MainParent.MsgType.Error  );
            }
        }

        private void tboxmo_KeyPress(object sender, KeyPressEventArgs e)
        {
       
        }

        private void txbarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        string sMchID;
        string sStationID;

        private void btpnback_Click(object sender, EventArgs e)
        {
          /*  if (MessageBox.Show("是否要退料??", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string LabelMaterial = "";

                if (GetTrsnMaterialBack(txbarcode.Text.Trim(), out LabelMaterial))
                {
                    string PartNo = publicfuntion.mGetString(LabelMaterial, 1);
                    string VenderCode = publicfuntion.mGetString(LabelMaterial, 2);
                    string DateCode = publicfuntion.mGetString(LabelMaterial, 3);
                    string LotCode = publicfuntion.mGetString(LabelMaterial, 4);
                    string QTY = FrmBLL.publicfuntion.mGetString(LabelMaterial, 5);


                    if ((PartNo != "") && (VenderCode != "") && (DateCode != "") && (LotCode != "") && (QTY != ""))
                    {

                        DataTable dz = FrmBLL.publicfuntion.getNewTable(dtlist, string.Format("料号='{0}'", PartNo));
                        if (dz.Rows.Count == 0)
                        {
                            ShowMsg(string.Format("该工单未使用过此物料 {0} ", PartNo), mLogMsgType.Error);
                            txbarcode.SelectAll();
                            return;
                        }
                        //   sSQL = string.Format(" insert into tSmtkPMonnitor (masterId,woId,machineId,stationno,cdata,kpnumber,supplyuser,flag,qty, " +
                        //                                                               " venderCode,datecode,lotid) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", sSEQ, sMO,
                        //                                                               sMchID, sStationID, "5", PartNo, sInfo.gUserInfo.userId, "0", "-" + QTY, VenderCode, DateCode, LotCode);
                        //refWebExecuteSqlCmd.Instance.ExecteSQLNonQuery(sSQL);

                        refWebtSmtKpMonitor.Instance.RollbackMaterial(new WebServices.tSmtKpMonitor.tSmtKpMonitor1()
                            {
                                masterId = sSEQ,
                                woId = sMO,
                                machineId = sMchID,
                                stationno = sStationID,
                                cdata = 5,
                                kpnumber = PartNo,
                                supplyuser = sInfo.gUserInfo.userId,
                                flag = 0,
                                qty = Convert.ToInt32("-" + QTY),
                                vendercode = VenderCode,
                                datecode = DateCode,
                                lotId = LotCode
                            });
                        refWebtSmtKpMonitor.Instance.UpdateTrsnStatus(txbarcode.Text.Trim(), "1", sInfo.gUserInfo.userId);
                        MaterialPnback();
                        ShowMsg("退料OK ", mLogMsgType.Incoming);
                        txbarcode.SelectAll();
                    }
                    else
                    {
                        ShowMsg("五合一条码格式错误 ", mLogMsgType.Error);
                        txbarcode.SelectAll();
                    }
                }
                else
                {
                    txbarcode.Focus();
                    txbarcode.SelectAll();
                }
            }*/
        }

        private void showrefreshtime_Click(object sender, EventArgs e)
        {

            if (showrefreshtime.Checked)
            {
                label1.Visible = false;
                showrefreshtime.Checked = false;
            }
            else
            {
                label1.Visible = true;
                showrefreshtime.Checked = true;
            }

        }

        //private void Scan_Data_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyValue == 13 && !string.IsNullOrEmpty(Scan_Data.Text))
        //    {
        //        if (Scan_Data.Text.Trim() == "UNDO")
        //        {
        //            UndoClearData();
        //            ShowMsg("清除完成,请刷入料站表", mLogMsgType.Incoming);

        //        }
        //        else
        //            if (sSEQ_MO == false)
        //            {
        //                try
        //                {
        //                    string str = Scan_Data.Text.Trim();
        //                    int i = str.IndexOf(' ');
        //                    sSEQ = str.Substring(0, i);
        //                    sMO = str.Substring(i + 1, str.Length - i - 1);
        //                }
        //                catch (Exception)
        //                {
        //                    ShowMsg("料站表刷入错误,请重新刷入料站表 ", mLogMsgType.Error);
        //                    Scan_Data.SelectAll();
        //                    sSEQ_MO = false;
        //                    return;
        //                }

        //               // sSQL = string.Format("select * from tSmtkPMonnitor where masterid='{0}'  and woId='{1}' and (cdata='0' or cdata='1') ", sSEQ, sMO);
        //              //  DataTable dt =  refWebExecuteSqlCmd.Instance.ExecuteQuerySQL(sSQL);

        //                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtKpMonitor.Instance.GetQueliaoStationList(sSEQ,sMO));

        //                if (dt.Rows.Count == 0)
        //                {
        //                    ShowMsg("料站表刷入错误,请重新刷入", mLogMsgType.Error);
        //                    Scan_Data.SelectAll();
        //                    return;
        //                }
        //                else
        //                {
        //                    ShowMsg("料站表正确,请刷唯一条码", mLogMsgType.Incoming);
        //                    Scan_Data.SelectAll();
        //                    sSEQ_MO = true;

        //                }
        //            }
        //            else
        //            {
        //                string LabelMaterial="";
        //              if (GetTrsnMaterial(Scan_Data.Text.Trim(), out LabelMaterial))
        //                {

        //                    string sPartNo = publicfuntion.mGetString(LabelMaterial, 1);//.GetString();
        //                    string sVenderCode = publicfuntion.mGetString(LabelMaterial, 2);
        //                    string sDateCode = publicfuntion.mGetString(LabelMaterial, 3);
        //                    string sLotID = publicfuntion.mGetString(LabelMaterial, 4);
        //                    string sQTY = publicfuntion.mGetString(LabelMaterial, 5);
                           

        //                    if (!string.IsNullOrEmpty(sPartNo) && 
        //                        !string.IsNullOrEmpty(sVenderCode)  && 
        //                        !string.IsNullOrEmpty(sDateCode) && 
        //                        !string.IsNullOrEmpty(sLotID) && 
        //                        !string.IsNullOrEmpty(sQTY))
        //                    {
        //                        if (publicfuntion.Check_MaterialScrap(sPartNo, sVenderCode, sDateCode, sLotID))
        //                        {
        //                            //sSQL = string.Format("select a.kpmonitorid from tSmtkPMonnitor a,(select * from tMaterialPreparation where masterId='{0}' and woId='{1}') b " +
        //                            //                    " where a.masterid=b.masterid and a.woid=b.woid and a.stationno=b.stationno and a.kpnumber=b.kpnumber and a.cdata='0' and b.kpnumber='{2}'", sSEQ, sMO, sPartNo);
        //                            //DataTable dt =  refWebExecuteSqlCmd.Instance.ExecuteQuerySQL(sSQL);

        //                            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtKpMonitor.Instance.CheckSupplyMaterial(sSEQ, sMO, sPartNo));

        //                            if (dt.Rows.Count != 0)
        //                            {
        //                                sRowid = dt.Rows[0][0].ToString();

        //                                //sSQL = string.Format("Update tSmtkPMonnitor set kpnumber='{0}',vendercode='{1}',datecode='{2}',lotid='{3}',qty='{4}',cdata='{5}',supplyuser='{6}',supplytime=sysdate  where kpmonitorid='{7}'",
        //                                //                      sPartNo, sVenderCode, sDateCode, sLotID, sQTY, "1", sInfo.gUserInfo.userId, sRowid);
        //                                // refWebExecuteSqlCmd.Instance.ExecteSQLNonQuery(sSQL);

        //                                refWebtSmtKpMonitor.Instance.UpdateSupplyMaterialStatus(new WebServices.tSmtKpMonitor.tSmtKpMonitor1()
        //                                    {
        //                                        kpnumber = sPartNo,
        //                                        vendercode = sVenderCode,
        //                                        datecode = sDateCode,
        //                                        lotId = sLotID,
        //                                        qty = Convert.ToInt32(sQTY),
        //                                        cdata = 1,
        //                                        supplyuser = sInfo.gUserInfo.userId,
        //                                        kpmonitorId = sRowid
        //                                    });
        //                                refWebtSmtKpMonitor.Instance.UpdateTrsnStatus(Scan_Data.Text.Trim(), "2", sInfo.gUserInfo.userId);
        //                                ShowMsg("补料完成,请刷入料表", mLogMsgType.Incoming);
        //                                sSEQ_MO = false;
        //                                QueryMaterialMonitor();
        //                                cblistline_SelectionChangeCommitted(null, EventArgs.Empty);
        //                                Scan_Data.SelectAll();
        //                            }
        //                            else
        //                            {
        //                                ShowMsg("该物料未缺料或不在此料表内", mLogMsgType.Error);
        //                                Scan_Data.SelectAll();
        //                            }

        //                        }
        //                        else
        //                        {
        //                            ShowMsg("清仓物料,静止使用", mLogMsgType.Error);
        //                            Scan_Data.SelectAll();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        ShowMsg("五合一条码格式错误", mLogMsgType.Error);
        //                        Scan_Data.SelectAll();
        //                    }
        //                }
        //            }
        //    }
        //}


        private void Scan_Data_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 && !string.IsNullOrEmpty(Scan_Data.Text))
            {
                if (Scan_Data.Text.Trim() == "UNDO")
                {
                    UndoClearData();
                    ShowMsg("清除完成,请刷入料站表", mLogMsgType.Incoming);

                }
                else
                    if (sSEQ_MO == false)
                    {
                        try
                        {
                            string str = Scan_Data.Text.Trim();
                            int i = str.IndexOf(' ');
                            sSEQ = str.Substring(0, i);
                            sMO = str.Substring(i + 1, str.Length - i - 1);
                        }
                        catch (Exception)
                        {
                            ShowMsg("料站表刷入错误,请重新刷入料站表 ", mLogMsgType.Error);
                            Scan_Data.SelectAll();
                            sSEQ_MO = false;
                            return;
                        }

                        DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtKpMonitor.Instance.GetQueliaoStationList(sSEQ, sMO));

                        if (dt.Rows.Count == 0)
                        {
                            ShowMsg("料站表刷入错误,请重新刷入", mLogMsgType.Error);
                            Scan_Data.SelectAll();
                            return;
                        }
                        else
                        {
                            ShowMsg("料站表正确,请刷唯一条码[原条码]", mLogMsgType.Incoming);
                            Scan_Data.SelectAll();
                            sSEQ_MO = true;

                        }
                    }
                    else
                        if (Old_Material_tray == false)
                        {
                            Scan_Data.SelectAll();                           
                          
                            string _StrErr = string.Empty;
                            DataTable dt_trsn = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.Sel_Tr_Sn_Info(Scan_Data.Text.Trim(), out _StrErr));
                            if (dt_trsn.Rows.Count == 0)
                            {
                                ShowMsg("原料盘刷入错误", mLogMsgType.Error);
                                return;
                            }
                            else
                            {
                                if (dt_trsn.Rows[0]["STATUS"].ToString() != "7")
                                {
                                    ShowMsg("原料盘未使用,请确认...", mLogMsgType.Error);
                                    return;
                                }
                                if (dt_trsn.Rows[0]["KP_NO"].ToString() != sKp_No)
                                {
                                    ShowMsg(string.Format("原料盘料号[{0}]与缺料料号[{1}]不符,请确认...", dt_trsn.Rows[0]["KP_NO"].ToString(), sKp_No), mLogMsgType.Error);
                                    return;
                                }

                                ShowMsg("原条码刷入正确,请刷新料盘", mLogMsgType.Incoming);
                                Scan_Data.SelectAll();
                                Old_Tr_sn = Scan_Data.Text.Trim();
                                Old_Material_tray = true;
                            }

                        }
                        else

                    {
                        string LabelMaterial = "";
                        string trsn = Scan_Data.Text.Trim();
                        if (GetTrsnMaterial(Scan_Data.Text.Trim(), out LabelMaterial))
                        {

                            string sPartNo = publicfuntion.mGetString(LabelMaterial, 1);//.GetString();
                            string sVenderCode = publicfuntion.mGetString(LabelMaterial, 2);
                            string sDateCode = publicfuntion.mGetString(LabelMaterial, 3);
                            string sLotID = publicfuntion.mGetString(LabelMaterial, 4);
                            string sQTY = publicfuntion.mGetString(LabelMaterial, 5);


                            if (!string.IsNullOrEmpty(sPartNo) &&
                                !string.IsNullOrEmpty(sVenderCode) &&
                                !string.IsNullOrEmpty(sDateCode) &&
                                !string.IsNullOrEmpty(sLotID) &&
                                !string.IsNullOrEmpty(sQTY))
                            {
                                if (refWebtPartBlocked.Instance.Check_MaterialScrap(sPartNo, sVenderCode, sDateCode, sLotID))
                                {                               

                                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtKpMonitor.Instance.CheckSupplyMaterial(sSEQ, sMO, sPartNo));

                                    if (dt.Rows.Count != 0)
                                    {
                                        sRowid = dt.Rows[0][0].ToString();
                                        Dictionary<string, object> dic = new Dictionary<string, object>();
                                        dic.Add("KPNUMBER", sPartNo);
                                        dic.Add("VENDERCODE", sVenderCode);
                                        dic.Add("DATECODE", sDateCode);
                                        dic.Add("LOTID", sLotID);
                                        dic.Add("QTY", Convert.ToInt32(sQTY));
                                        dic.Add("CDATA", 1);
                                        dic.Add("SUPPLYUSER", sInfo.gUserInfo.userId);
                                        dic.Add("TRSN", trsn);
                                        dic.Add("ROWID", sRowid);
                                        refWebtSmtKpMonitor.Instance.UpdateSupplyMaterialStatus(FrmBLL.ReleaseData.DictionaryToJson(dic));                                      
                                        string _StrErr= refWebtR_Tr_Sn.Instance.Update_TR_SN(Scan_Data.Text.Trim(), "NA", sInfo.gUserInfo.userId, "3", "NA", "NA");
                                        _StrErr = refWebtR_Tr_Sn.Instance.Update_TR_SN(Old_Tr_sn, "NA", sInfo.gUserInfo.userId, "8", "NA", "NA");//原料盘更改为已下线
                                        ShowMsg("更改状态->" + _StrErr, mLogMsgType.Incoming);
                                        ShowMsg("补料完成,请刷入料表", mLogMsgType.Incoming);
                                        sSEQ_MO = false;
                                        Old_Material_tray = false;
                                        Old_Tr_sn = string.Empty;
                                        sKp_No = string.Empty;
                                        cblistline_SelectionChangeCommitted(null, EventArgs.Empty);
                                        Scan_Data.SelectAll();
                                        QueryMaterialMonitor();
                                    }
                                    else
                                    {
                                        ShowMsg("该物料未缺料或不在此料表内", mLogMsgType.Error);
                                        Scan_Data.SelectAll();
                                    }
                                }
                                else
                                {
                                    ShowMsg("清仓物料,静止使用", mLogMsgType.Error);
                                    Scan_Data.SelectAll();
                                }
                            }
                            else
                            {
                                ShowMsg("五合一条码格式错误", mLogMsgType.Error);
                                Scan_Data.SelectAll();
                            }
                        }
                    }
            }
        }
        private void dataGridViewX1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                this.dataGridViewX1[e.ColumnIndex, e.RowIndex].ToolTipText =
                    string.Format("当前累计:[{0}]笔数据", this.dataGridViewX1.Rows.Count);
            }
        }

        private void dataGridViewX2_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                this.dataGridViewX2[e.ColumnIndex, e.RowIndex].ToolTipText =
                    string.Format("当前累计:[{0}]笔数据", this.dataGridViewX2.Rows.Count);
            }
        }

        private void dataGridViewX3_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
           
        }



        public bool GetTrsnMaterial(string trsn, out string MaterialLabel)
        {
            Scan_Data.SelectAll();
            MaterialLabel = "";
            string _StrErr = string.Empty;
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.Sel_Tr_Sn_Info(trsn, out _StrErr));
            try
            {
                if (dt.Rows[0]["STATUS"].ToString() == "3")
                {
                    ShowMsg("此盘物料已经备好在生产线,请确认......", mLogMsgType.Outgoing);
                    return false;
                }

                if (dt.Rows[0]["STATUS"].ToString() != "2")
                {
                    ShowMsg("此盘物料状态不在待备料,请确认......", mLogMsgType.Error);
                    return false;
                }
                if (Check_Mo)
                {
                    if (dt.Rows[0]["WOID"].ToString() != sMO)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("NEW_WOID", sMO);
                        DataTable dtSmtMoMerge = FrmBLL.ReleaseData.arrByteToDataTable( refWebtSmtWoMerge.Instance.Get_Smt_WO_Merge( FrmBLL.ReleaseData.DictionaryToJson(dic),"OLD_WOID" ));

                        if (dtSmtMoMerge.Rows.Count == 0 || dtSmtMoMerge.Rows[0]["OLD_WOID"].ToString() != dt.Rows[0]["WOID"].ToString())
                        {
                            ShowMsg(string.Format("物料工单不同,{0}≠{1}", dt.Rows[0]["WOID"].ToString(), sMO), mLogMsgType.Error);
                            return false;
                        }

                    }
                }


                MaterialLabel = dt.Rows[0]["KP_NO"].ToString() + "|" + dt.Rows[0]["VENDER_ID"].ToString() + "|" + dt.Rows[0]["DATE_CODE"].ToString() + "|" + dt.Rows[0]["LOT_CODE"].ToString() + "|" + dt.Rows[0]["QTY"].ToString();

                return true;
            }
            catch
            {
                ShowMsg("唯一条码错误,请刷正确的唯一条码", mLogMsgType.Error);
                return false;
            }

        }
    }
}