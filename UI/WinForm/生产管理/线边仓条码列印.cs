using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LabelManager2;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using System.Runtime.InteropServices;

namespace SFIS_V2
{
    public partial class LineHousePrint5in1label :Office2007Form// Form
    {
        public LineHousePrint5in1label(MainParent Msg)
        {
            InitializeComponent();
            ShowMP = Msg;
        }
        MainParent ShowMP;
        //RefWebService_BLL.refWeb_VenderInfo.tVenderInfo venderinfo = new RefWebService_BLL.refWeb_VenderInfo.tVenderInfo();
        //RefWebService_BLL.refWeb_tPartStorehousehad.tPartStorehousehad TrSnInfo = new RefWebService_BLL.refWeb_tPartStorehousehad.tPartStorehousehad();


        ApplicationClass lbl ;// = new ApplicationClass();
        bool isprinter = true;
        private delegate void delegateopenlabel(string filename);
        delegateopenlabel dpl;
        IAsyncResult iasyncresult;
        private void eventopenlablefile(string filename)
        {
            if (!Directory.Exists(System.IO.Directory.GetCurrentDirectory() + @"\Print"))
                Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + @"\Print");
            if (!File.Exists(filename))  //判断条码文件是否存在
            {
                this.isprinter = false;
                ShowMP.ShowPrgMsg("条码档没有找找到,正在从Ftp下载..", MainParent.MsgType.Warning);
                FrmBLL.Ftp_MyFtp ftp = new FrmBLL.Ftp_MyFtp();
                ftp.DownloadFile("FEIXUN_LOT.Lab", System.AppDomain.CurrentDomain.BaseDirectory + "Print", "FEIXUN_LOT.Lab");
                ShowMP.ShowPrgMsg("下载完成", MainParent.MsgType.Outgoing);
                // ShowMP.ShowPrgMsg("条码档没有找找到,请确认当前目录是否存在 FEIXUN_LOT.lab", MainParent.MsgType.Error);
            }

            this.isprinter = true;
            lbl = new ApplicationClass();
            lbl.Documents.Open(labelfilepatch.Text.Trim(), false);// 调用设计好的label文件
        }
        private void print5in1label_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.ShowMP.gUserInfo.rolecaption == "系统开发员")
            {
                List<WebServices.tUserInfo.tFunctionList> lsfunls = new List<WebServices.tUserInfo.tFunctionList>();
                FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                FrmBLL.publicfuntion.AddProgInfo(new WebServices.tUserInfo.tProgInfo()
                {
                    progid = this.Name,
                    progname = this.Text,
                    progdesc = this.Text

                }, lsfunls);
            }
            #endregion

            labelfilepatch.Text = System.IO.Directory.GetCurrentDirectory() + @"\Print\FEIXUN_LOT.lab";
            dpl = new delegateopenlabel(eventopenlablefile);
            iasyncresult =  dpl.BeginInvoke(labelfilepatch.Text, null, null);
            edt_Scan_Data.Select();

            labelX3.Visible = false;
            tb_vendernumber.Visible = false;
            labelX5.Visible = false;
            cblocal.Visible = false;
            labelX2.Visible = false;
            edt_Scan_Data.Visible = false;
            chkreprint.Visible = false;
            getdc.Visible = false;
            bt_addkpdesc.Visible = false;

            edt_trsn.Focus();

        }
       // DataTable vcdt;
        private void ExcuteQueryVender()
        {
            // edt_vc.Items.Clear();

            // RefWebService_BLL.refWeb_VenderInfo.tVenderInfo vn = new RefWebService_BLL.refWeb_VenderInfo.tVenderInfo();
            // vcdt = vn.QueryVender();
            //for (int i = 0; i < vcdt.Rows.Count; i++)
            //{
            //    edt_vc.Items.Add(vcdt.Rows[i][0].ToString());
            //}
        }

        private void edt_Scan_Data_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(edt_Scan_Data.Text) && e.KeyChar == 13)
            {       
                string s = edt_Scan_Data.Text;
                string[] sArray=s.Split('|');
                if ((sArray.Length - 1) != 4)
                {
                    MessageBox.Show("五合一条码格式错误");
                    return;
                }           
             
                    edt_pn.Text = sArray[0];
                    edt_vc.Text = sArray[1];
                    edt_dc.Text = sArray[2];
                    edt_lot.Text = sArray[3];
                    edt_qty.Text = sArray[4];
                    edt_Scan_Data.SelectAll();
                    edt_Scan_Data.Text = "";
              
            }
        }

        private DataTable _datatable;
        private void chkPartNumber(string kpnumber)
        {
            _datatable = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartMap.Instance.QueryPartMaps(this.edt_pn.Text.Trim()));
            if (_datatable == null || _datatable.Rows.Count < 1)
                throw new Exception("输入的料号非法,输入的料号在系统中没有记录..");
            this.edt_vc.Items.Clear();
            foreach (DataRow dr in _datatable.Rows)
            {
                this.edt_vc.Items.Add(dr["venderId"].ToString());
            }
            if (this.edt_vc.Items.Count == 1)
            {
                this.edt_vc.SelectedIndex = 0;
                this.lb_kpdesc.Text = _datatable.Rows[0]["kpdesc"].ToString();
            }
            //else
            //    this.edt_vc.Text = ""; 

        }
        string TrSn = "";
        string PartQTY = "0";
        private void butprint_Click(object sender, EventArgs e)
        {
            if (iasyncresult != null && !iasyncresult.IsCompleted)
            {
                ShowMP.ShowPrgMsg("打印模板还没有初始化成功,请稍后..", MainParent.MsgType.Warning);
                return;
            }
           

            if (!string.IsNullOrEmpty(edt_pn.Text) && !string.IsNullOrEmpty(edt_dc.Text.Trim())
                && !string.IsNullOrEmpty(edt_lot.Text.Trim()) && !string.IsNullOrEmpty(edt_vc.Text.Trim())
                && !string.IsNullOrEmpty(edt_qty.Text.Trim()))
            {
                //if (this.edt_dc.Text.Length > 8)
                //    throw new Exception("生产周期位数过长,请保持在8位以内");
                //if (this.edt_lot.Text.Length > 8)
                //    throw new Exception("生产批次位数过长,请保持在8位以内");
                if (ChkSplit.Checked)
                {
                    #region 线边仓分盘
                    if (TrSnsTatus != 1)
                    {
                        ShowMP.ShowPrgMsg("此物料不再线边仓,不能分盘", MainParent.MsgType.Error);
                        return;
                    }

                     if (Convert.ToInt32(oldqty) <= Convert.ToInt32(edt_qty.Text))
                        {
                            MessageBox.Show(" 分盘数量不正确 \r 原数量为: " + oldqty + " 分盘数量为: " + edt_qty.Text + " \r 请确认...", "条码数量修改异常提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;

                        }

                    for (int i = 0; i < 2; i++)
                    {
                      
                        if (i == 0)
                        {
                            PartQTY = edt_qty.Text;
                        }
                        else
                        {
                            PartQTY =(Convert.ToInt32(oldqty) - Convert.ToInt32(edt_qty.Text)).ToString();
                        }

                        TrSn = RefWebService_BLL.refWebtPartStorehousehad.Instance.GetSeqTrSn();
                        lb_trsn.Text = "唯一编号:  " + TrSn;

                        RefWebService_BLL.refWebtPartStorehousehad.Instance.InsertPartStorehousehad(new WebServices.tPartStorehousehad.tPartStorehousehad1()
                        {
                            Tr_Sn = TrSn,
                            KpNumber = edt_pn.Text.Trim(),
                            VenderCode = edt_vc.Text.Trim(),
                            DateCode = edt_dc.Text.Trim(),
                            LotId = edt_lot.Text.Trim(),
                            QTY = Convert.ToInt32(PartQTY),
                            status = 1,
                            UserId = ShowMP.gUserInfo.userId,
                            LocId = cblocal.Text.Trim(),
                            OUTQTY = 0,
                            recdate = this.mrecdate,
                            FLAG = "DEPART"
                        });

                        RefWebService_BLL.refWebtPartStorehousehad.Instance.UpdateTrSnStatus("4", ShowMP.gUserInfo.userId, edt_trsn.Text.Trim());

                        if (priner_station_label(this.lb_kpdesc.Text.Trim(), TrSn))
                        {
                          
                        }
                    }

                    edt_pn.Text = "";
                    edt_dc.Text = "";
                    edt_lot.Text = "";
                    edt_vc.Text = "";
                    edt_qty.Text = "";
                    edt_trsn.Text = "";
                    edt_trsn.Focus();
                    edt_trsn.SelectAll();
                    ShowMP.ShowPrgMsg("列印完成", MainParent.MsgType.Incoming);

                    #endregion

                }
                else
                {
                    #region 退料列印和重复列印
                    if (this._datatable.Rows.Count != 0)
                    {
                        if (Convert.ToInt32(oldqty) < Convert.ToInt32(edt_qty.Text))
                        {
                            MessageBox.Show(" 修改数量大于原数量 \r 原数量为: " + oldqty + " 修改后数量为: " + edt_qty.Text + " \r 请确认...", "条码数量修改异常提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;

                        }
                        
                        if (oldqty != edt_qty.Text.Trim())
                        {

                            if (string.IsNullOrEmpty(cbstatus.Text))
                            {
                                MessageBox.Show("未选择状态");
                                return;
                            }

                            if (TrSnsTatus != 3)
                            {
                                ShowMP.ShowPrgMsg("生产线未使用,不可做退料列印条码", MainParent.MsgType.Warning);
                                edt_trsn.Focus();
                                edt_trsn.SelectAll();
                                return;
                            }

                            if (MessageBox.Show(" 此唯一条码: " + TrSn + "\r" + " 数量发生变更,原数量为: " + oldqty + " 更该后数量为: " + edt_qty.Text.Trim() + "\r 列印后将产生新的唯一条码. \r 点击YES将列印新的唯一条码,否则直接列印重号",
                                "条码列印提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                TrSn = RefWebService_BLL.refWebtPartStorehousehad.Instance.GetSeqTrSn();
                                lb_trsn.Text = "唯一编号:  " + TrSn;

                                

                                int sStatus = 5;
                                if (cbstatus.Text == "线边仓")
                                {
                                    sStatus = 1;
                                }

                                RefWebService_BLL.refWebtPartStorehousehad.Instance.InsertPartStorehousehad(new WebServices.tPartStorehousehad.tPartStorehousehad1()
                                {
                                    Tr_Sn = TrSn,
                                    KpNumber = edt_pn.Text.Trim(),
                                    VenderCode = edt_vc.Text.Trim(),
                                    DateCode = edt_dc.Text.Trim(),
                                    LotId = edt_lot.Text.Trim(),
                                    QTY = Convert.ToInt32(edt_qty.Text.Trim()),
                                    status = sStatus,
                                    UserId = ShowMP.gUserInfo.userId,
                                    LocId = cblocal.Text.Trim(),
                                    OUTQTY = 0,
                                    recdate = this.mrecdate,
                                    FLAG = "DEPART"
                                });

                                RefWebService_BLL.refWebtPartStorehousehad.Instance.UpdateTrSnStatus("4", ShowMP.gUserInfo.userId, edt_trsn.Text.Trim());
                            }
                            else
                            {
                                edt_qty.Text = oldqty;
                            }
                        }
                         PartQTY = edt_qty.Text;
                        if (priner_station_label(this.lb_kpdesc.Text.Trim(), TrSn))
                        {
                            edt_pn.Text = "";
                            edt_dc.Text = "";
                            edt_lot.Text = "";
                            edt_vc.Text = "";
                            edt_qty.Text = "";
                            //  edt_Scan_Data.Text = "";
                            //  edt_Scan_Data.SelectAll();

                            edt_trsn.Text = "";
                            edt_trsn.Focus();
                            edt_trsn.SelectAll();
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("没有查询到物料规格信息,请先添加此信息", "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            ShowMP.ShowPrgMsg("请先添加物料规格信息.", MainParent.MsgType.Error);

                        }
                    }

                    #endregion
                }
            }
            else
            {
                ShowMP.ShowPrgMsg("五合一信息输入不完整", MainParent.MsgType.Error);
            }

        }

        private bool priner_station_label(string KpDesc,string Tr_Sn)
        {
            //string filepatch = labelfilepatch.Text.Trim(); ;
            //if (!File.Exists(filepatch))  //判断条码文件是否存在
            //{
            //    ShowMP.ShowPrgMsg("条码档没有找找到,请确认当前目录是否存在 FEIXUN_LOT.lab", MainParent.MsgType.Error);
            //    return false;
            //}

            try
            {
                if (this.isprinter)
                {
                    // lbl.Documents.Open(filepatch, false);// 调用设计好的label文件
                    Document doc = lbl.ActiveDocument;
                    doc.Variables.FormVariables.Item("PN").Value = edt_pn.Text.Trim(); //给参数传值
                    doc.Variables.FormVariables.Item("VENDER_CODE").Value = edt_vc.Text.Trim();
                    doc.Variables.FormVariables.Item("DATECODE").Value = edt_dc.Text.Trim();
                    doc.Variables.FormVariables.Item("LOT_ID").Value = edt_lot.Text.Trim();
                    doc.Variables.FormVariables.Item("UNIT_SIZE").Value =PartQTY;
                    doc.Variables.FormVariables.Item("EMP_NO").Value = ShowMP.gUserInfo.userId;
                    doc.Variables.FormVariables.Item("REMARK").Value = KpDesc;
                    doc.Variables.FormVariables.Item("TR_SN").Value = Tr_Sn;

                    int Num = Convert.ToInt32(print_qty.Text.Trim());        //打印数量
                    doc.PrintDocument(Num);//打印                  
                }
                else
                {
                    throw new Exception("没有发现条码文档..");
                }

                return true;
            }
            catch (Exception ex)
            {
                ShowMP.ShowPrgMsg(ex.Message,MainParent.MsgType.Error);
                return false;
            }
            
        }

        private void edt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void print_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            fmParMap fPM = new fmParMap(ShowMP);
            fPM.ShowDialog();
        }

        private void edt_qty_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void print5in1label_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                lbl.Documents.CloseAll(false);
                lbl.Quit();
            }
            catch
            {
            }
            //退出       
        }
        /// <summary>
        /// 当选择厂商改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void edt_vc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.edt_vc.Text))
                return;
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebVenderInfo.Instance.QueryVenderInfoByVenderId(this.edt_vc.Text.Trim()));

           if (dt == null || dt.Rows.Count < 1)
           {
               this.labshowdesc.ForeColor = Color.Red;
               this.labshowvc.ForeColor = Color.Red;
               this.labshowvc.Text = " 厂商名 : 没有找到厂商信息..";
               this.labshowdesc.Text = "厂商名 : 没有找到厂商信息";

           }
           else
           {
               this.labshowdesc.ForeColor = Color.Black;
               this.labshowvc.ForeColor = Color.Black;
               this.labshowvc.Text = "厂商名: " + dt.Rows[0]["vendername"].ToString();
               this.labshowdesc.Text = "厂商名: " + dt.Rows[0]["vendername2"].ToString();
           }
           this.lb_kpdesc.Text = "";
           foreach (DataRow dr in this._datatable.Rows)
           {
               if (dr["venderId"].ToString().ToUpper() == this.edt_vc.Text.Trim().ToUpper())
               {
                   this.lb_kpdesc.Text = dr["kpdesc"].ToString();
               }
           }

            //for (int y=0;y<vcdt.Rows.Count;y++)
            //{
            //    if (vcdt.Rows[y][0].ToString()==edt_vc.Text.Trim())
            //    {
            //        labshowvc.Text ="厂商名: "+ vcdt.Rows[y][1].ToString();
            //        labshowdesc.Text = "厂商名: " + vcdt.Rows[y][2].ToString();
            //    }
            //}

        }

        public  string checkvender(out string  msg )
        {
          
            for (int z = 0; z < edt_vc.Items.Count; z++)
            {
                if (edt_vc.Items[z].ToString() == edt_vc.Text.Trim())
                {
                  return  msg = "OK";
                   break;
                }
            }
            return msg = "NO";
        }

        private void tb_vendernumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_vendernumber.Text))
                return;
            if (e.KeyValue == 13)
            {
                DataTable _dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartMap.Instance.GetPartMapsBy(this.tb_vendernumber.Text));
               // DataTable _dt = refWebtVenderCode.Instance.GetVendCodeInfo(this.tb_vendernumber.Text);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    this.edt_pn.Items.Clear();
                    foreach (DataRow dr in _dt.Rows)
                    {
                        this.edt_pn.Items.Add(dr["kpnumber"].ToString());
                    }
                    if (_dt.Rows.Count < 2)
                        this.edt_pn.SelectedIndex = 0;
                    else
                        this.edt_pn.Text = "";
                    this.edt_vc.Text = "";
                    // MessageBox.Show(_dt.Rows[0]["venderId"].ToString());
                    this.edt_vc.SelectedText = _dt.Rows[0]["venderId"].ToString();
                    edt_vc_SelectedIndexChanged(null, null);
                }
                else
                {
                    this.edt_vc.Items.Clear();
                    this.edt_pn.Items.Clear();
                    this.edt_vc.Text = "";
                    this.edt_pn.Text = "";
                }
            }
        }

        private void panelEx1_Click(object sender, EventArgs e)
        {

        }

        private void edt_pn_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.edt_pn.Text))
                    return;
                this.chkPartNumber(this.edt_pn.Text.Trim());
            }
            catch (Exception ex)
            {
                this.edt_vc.Items.Clear();
                this.ShowMP.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void getdc_Click(object sender, EventArgs e)
        {

        }

        private void edt_dc_MouseLeave(object sender, EventArgs e)
        {
            if (getdc.Checked)
            {
                edt_dc.Text = System.DateTime.Now.ToString("yyyyMMdd");
            }
        }

        private void edt_lot_MouseLeave(object sender, EventArgs e)
        {
            if (getdc.Checked)
            {
                edt_lot.Text =System.DateTime.Now.ToString("yyyyMMdd");
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Control myc = GetFocusedControl();
                if (myc.GetType().Name.IndexOf("TextBox") != -1)
                {
                    myc.Text = "12345";
                }
            }
            catch
            {
            }
           // return;
            //rd.GetComData(edt_Scan_Data);
            //edt_Scan_Data.SelectAll();
            //edt_Scan_Data.Focus();
        }







        #region xxxx

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr GetFocus();

        /// <summary>
        /// 当前拥有焦点的控件
        /// </summary>
        /// <param name="formControl"></param>
        /// <returns></returns>
        public static Control GetFocusedControl()
        {
            Control focusedControl = null;

            try
            {
                IntPtr focusedHandle = GetFocus();

                if (focusedHandle != IntPtr.Zero)
                {
                    focusedControl = Control.FromChildHandle(focusedHandle);
                }
            }
            catch { }

            return focusedControl;
        }

        #endregion 

        string oldqty = "";
        string mrecdate = string.Empty;
        int TrSnsTatus = 0;
        private void edt_trsn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(edt_trsn.Text) & (e.KeyCode==Keys.Enter))
            {
                DataTable dttrsn = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.QueryTrSn(this.edt_trsn.Text.Trim()));
                if (dttrsn.Rows.Count > 0)
                {
                    this.mrecdate = dttrsn.Rows[0]["recdate"].ToString();
                    edt_pn.Text = dttrsn.Rows[0]["kpnumber"].ToString();
                    edt_dc.Text = dttrsn.Rows[0]["datecode"].ToString();
                    edt_vc.Text = dttrsn.Rows[0]["vendercode"].ToString();
                    edt_lot.Text = dttrsn.Rows[0]["lotId"].ToString();
                    edt_qty.Text = oldqty = dttrsn.Rows[0]["qty"].ToString();
                    cblocal.Text = dttrsn.Rows[0]["locId"].ToString();
                    TrSnsTatus = Convert.ToInt32(dttrsn.Rows[0]["sstatus"].ToString());
                    edt_trsn.Focus();
                    edt_trsn.SelectAll();
                    chkPartNumber(edt_pn.Text);
                    TrSn = edt_trsn.Text.Trim();
                    edt_vc_SelectedIndexChanged(null, null);
                }
                else
                {
                    ShowMP.ShowPrgMsg("唯一条码输入错误,请确认",MainParent.MsgType.Error);
                    edt_trsn.Focus();
                    edt_trsn.SelectAll();
                }

            }
        }

        private void chkreprint_Click(object sender, EventArgs e)
        {
            if (chkreprint.Checked==true)
            {
                chkreprint.Checked = true;
                edt_trsn.Enabled = true;
               
            }            
           else
    
                {
                    chkreprint.Checked = false;
                    edt_trsn.Enabled = false;
                }
          
        }

        private void ChkSplit_Click(object sender, EventArgs e)
        {
            if (ChkSplit.Checked)           
                cbstatus.Enabled = false;           
            else
                cbstatus.Enabled = true;
        }

       
    }
}
