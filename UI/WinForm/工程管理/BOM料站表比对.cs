using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;

namespace SFIS_V2
{
    public partial class FrmBomCompare : Office2007Form// Form
    {
        public FrmBomCompare(MainParent frm)
        {
            InitializeComponent();
            this.mFrm = frm;
        }
        private MainParent mFrm;
        public DataTable KpDetaltNewTable = new DataTable();

        public DataTable BomDataTable = new DataTable();
        public DataTable SmtSoftKpList = new DataTable();
        public DataTable SmtKpTable = new DataTable();

        public string mPartnumber { get; set; }
        public string mProductname { get; set; }
        public string mLineId { get; set; }

        private enum LogMsgType { Incoming, Outgoing, Normal, Warning, Error } //文字输出类型
        private Color[] LogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };//文字颜色

        private delegate void delegaterundata(DataTable mdt);
        private delegate void delegatesavesmtsoftkplist(string[] arrStr, string machine);
        private delegate void delegateRunkpdetalt(string partnumber, string lineid, string pcbside, DataTable dt);
        delegateRunkpdetalt eventrunkpdetalt;
        delegatesavesmtsoftkplist eventsavesmtsoftkplist;
        delegaterundata eventrundata;
        IAsyncResult iasyncresult1;
        IAsyncResult iasyncresult;

        private delegate void delegatesaveExcelsmtsoftkplist(string[] arrStr);
        delegatesaveExcelsmtsoftkplist eventsaveExcelsmtsoftkplist;
        IAsyncResult iasyncresultExcel;

        private void FrmBomCompare_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.mFrm.gUserInfo.rolecaption == "系统开发员")
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
            this.rbtnBsCom.Checked = true;
            this.checkBoxX1.Checked = true;
            this.btInputBomData.Image = this.imageList1.Images[6];
            this.btsmtsoft.Image = this.imageList1.Images[10];
            this.btcadT.Image = this.imageList1.Images[5];
            this.btcadB.Image = this.imageList1.Images[5];
            this.btcompare.Image = this.imageList1.Images[7];
            this.cbpcbside.Items.AddRange(new string[] { "T+B", "T", "B" });
            this.cbpcbside.SelectedIndex = 0;
            this.cbpartnumber.Focus();
        }
        private bool gelidip = false;
        private void btInputBomData_Click(object sender, EventArgs e)
        {
            try
            {
                if (iasyncresult1 == null || iasyncresult1.IsCompleted)
                {
                    string temp = "";//string.Empty;
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Title = "选择BOM数据";
                    ofd.Filter = "(*.xls Excel 2003)|*.xls";
                    //ofd.InitialDirectory = "c:\\";
                    DialogResult dlr = ofd.ShowDialog();
                    if (dlr == DialogResult.Yes || dlr == DialogResult.OK)
                    {
                        string filename = ofd.SafeFileName;
                        filename = filename.Split('.')[0];
                        if (filename.Split('-').Length != 2)
                            throw new Exception("BOM文件名称格式错误,请确认是否为BOM文件..");
                        DataTable mdt = FrmBLL.ClsReadExcel.getTableForSql(ofd.FileName,
                            string.Format("select 子件号,子件描述,替代组,'{1}'as Bom版本,排序字符串,用量,长文本（位号） from [{0}] where{2} 长文本（位号）<>''",
                            FrmBLL.ClsReadExcel.GetTableNames(ofd.FileName)[0], filename.Split('-')[1], temp));
                        this.progressBarItem1.Maximum = mdt.Rows.Count;

                        eventrundata = new delegaterundata(this.RunData);
                        iasyncresult1 = eventrundata.BeginInvoke(mdt, null, null);
                    }
                }
                else
                {
                    MessageBoxEx.Show("程序还在运作中,请稍后....");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RunData(DataTable mdt)
        {
            FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
            ass.ExecuteOracleCommand("delete from bomdata");
            int i = 0;
            this.ShowMsg(LogMsgType.Warning, "正在加载BOM数据");
            foreach (DataRow dr in mdt.Rows)
            {
                i++;
                string sql = string.Format("insert into bomdata(kpnumber,kpdesc,loction,kpgroup,bomver,kptype,qty) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    dr[0].ToString(), dr[1].ToString(), dr[6].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
                ass.ExecuteOracleCommand(sql);
                this.Showpb(i);
            }
            string str = string.Empty;
            if (this.gelidip)
                str = " where kptype<>'DIP'";
            this.ShowData(this.BomDataTable = ass.GetDatatable(string.Format("select kpnumber,kpdesc,kpgroup,bomver,qty,loction from bomdata {0}", str)));
            this.ShowMsg(LogMsgType.Outgoing, "BOM数据加载完成");
        }

        private void ShowData(DataTable dt)
        {
            this.dgvdata.Invoke(new EventHandler(delegate
            {
                this.dgvdata.DataSource = dt;
            }));
        }

        private void ShowData1(DataTable dt)
        {
            this.dataGridView1.Invoke(new EventHandler(delegate
            {
                this.dataGridView1.DataSource = dt;
            }));
        }
        private void ShowData2(DataTable dt)
        {
            this.dataGridView2.Invoke(new EventHandler(delegate
            {
                this.dataGridView2.DataSource = dt;
            }));
        }
        private void Showpb(int vlu)
        {
            this.progressBarItem1.Invoke(new EventHandler(delegate
            {
                this.progressBarItem1.Value = vlu;
            }));
        }

        private void SetPb2MaxValue(int vlu)
        {
            this.progressBarItem2.Invoke(new EventHandler(delegate
            {
                this.progressBarItem2.Maximum = vlu;            
            }));
        }
        private void Showpb1(int vlu)
        {
            this.progressBarItem2.Invoke(new EventHandler(delegate
            {
                this.progressBarItem2.Value = vlu;
              
            }));
        }
        private void ShowMsg(LogMsgType msgtype, string msg)
        {
            this.rtbmsg.Invoke(new EventHandler(delegate
            {
                this.rtbmsg.TabStop = false;
                rtbmsg.SelectedText = string.Empty;
                rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                rtbmsg.SelectionColor = LogMsgTypeColor[(int)msgtype];
                rtbmsg.AppendText(msg + "\n");
                rtbmsg.ScrollToCaret();
            }));
        }

        private void btcompare_Click(object sender, EventArgs e)
        {
            try
            {
                if ((iasyncresult != null && !iasyncresult.IsCompleted) || iasyncresult1 != null && !iasyncresult1.IsCompleted)
                {
                    MessageBoxEx.Show("程序还在运作中,请稍后....");
                    return;
                }
                this.rtbmsg.Clear();
                if (this.BomDataTable == null || this.BomDataTable.Rows.Count < 1)
                {
                    MessageBoxEx.Show("请先加载BOM数据");
                    return;
                }

                if (this.rbtnPsCom.Checked)
                {//SMT程式料站表与系统料站表比对
                    //判断是否加载SMT料站表
                    if (this.SmtSoftKpList == null || this.SmtSoftKpList.Rows.Count < 1)
                    {
                        MessageBoxEx.Show("请先加载SMT料站表");
                        return;
                    }
                    this.SmtSoftKpListCompareBomData("NO", null, null, null);

                    eventrundata = new delegaterundata(this.CompareSmtKpSfisKp);
                    iasyncresult = eventrundata.BeginInvoke(null, null, null);
                    return;
                }

                if (rbtnBsCom.Checked)
                {
                    //判断是否加载SMT料站表
                    if (this.SmtSoftKpList == null || this.SmtSoftKpList.Rows.Count < 1)
                    {
                        MessageBoxEx.Show("请先加载SMT料站表");
                        return;
                    }
                    this.SmtSoftKpListCompareBomData("NO", null, null, null);
                    RunKpdetalt_New(null, null, null, SmtKpTable);
                }

                if (rbbomsfccom.Checked)
                {
                    //判断是否加载系统料站表
                    if (this.KpDetaltNewTable == null || this.KpDetaltNewTable.Rows.Count < 1)
                    {
                        MessageBoxEx.Show("没有可以进行比较的数据,请先加载系统料站表");
                        return;
                    }
                }

                eventrundata = new delegaterundata(CompareKp);
                iasyncresult = eventrundata.BeginInvoke(KpDetaltNewTable, null, null);
            }
            catch (Exception ex)
            {
                this.ShowMsg(LogMsgType.Error, ex.Message);
            }
        }
        private void CompareSmtKpSfisKp(DataTable smtkpdatatable)
        {
            try
            {
                DataTable mdt = SmtKpTable.DefaultView.ToTable(true, "partnumber", "lineId", "pcbside");
                this.ShowMsg(LogMsgType.Outgoing, string.Format("累计有{0}组料表", mdt.Rows.Count));
                int x = 0;
                foreach (DataRow dr in mdt.Rows)
                {
                    x++;
                    this.ShowMsg(LogMsgType.Outgoing, String.Format("正在比对第[{0}]组料站表", x));
                    this.ShowMsg(LogMsgType.Normal, "正在加载系统料站表...");
                    DataTable _dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebSmtKpMaster.Instance.GetPartnumberAData(
                          dr["partnumber"].ToString(),
                          dr["lineId"].ToString(),
                          dr["pcbside"].ToString()));

                    if (_dt == null || _dt.Rows.Count < 1)
                        throw new Exception(string.Format("料号:[{0}]+线别:[{1}]+面别:[{2}]在系统中没有找到数据,请核对..", dr["partnumber"].ToString(),
                          dr["lineId"].ToString(),
                          dr["pcbside"].ToString()));
                    this.ShowMsg(LogMsgType.Incoming, "系统料站表加载完成");
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        DataRow[] arrDr = this.SmtKpTable.Select(string.Format("stationno='{0}' and kpnumber='{1}' and pcbside='{2}'", _dt.Rows[i]["stationno"].ToString(), _dt.Rows[i]["kpnumber"].ToString(), _dt.Rows[i]["pcbside"].ToString()));
                        if (arrDr.Length < 1)
                        {
                            this.ShowMsg(LogMsgType.Error, string.Format("料站表存在差异,料站:[{0}] + 料号:[{1}]在新的料站表中不存在!!",
                                _dt.Rows[i]["stationno"].ToString(), _dt.Rows[i]["kpnumber"].ToString()));
                        }
                        foreach (DataRow idr in arrDr)
                        {
                            string newloction = idr["loction"].ToString();
                            string oldloction = _dt.Rows[i]["loction"].ToString();
                            if (newloction == oldloction)
                                continue;
                            string ltemp = string.Empty;
                            string stemp = string.Empty;
                            if (newloction.Length < oldloction.Length)
                            {
                                ltemp = oldloction;
                                stemp = newloction;
                            }
                            else
                            {
                                ltemp = newloction;
                                stemp = oldloction;
                            }
                            foreach (string str in ltemp.Split(','))
                            {
                                if (!string.IsNullOrEmpty(str))
                                {
                                    if (!this.FindList(str, stemp.Split(',')))
                                    {
                                        this.ShowMsg(LogMsgType.Error,
                                            string.Format("位号:[{0}]不存在与列表中..\n新料站表[{3}][{4}]:[{1}]\n原有料表[{5}][{6}]:[{2}]",
                                            str, newloction, oldloction,
                                            _dt.Rows[i]["stationno"].ToString(),
                                            _dt.Rows[i]["kpnumber"].ToString(),
                                            idr["stationno"].ToString(),
                                            idr["kpnumber"].ToString()));
                                    }
                                }
                            }
                        }
                    }
                    this.ShowMsg(LogMsgType.Outgoing, String.Format("第[{0}]组料站表,比对完成", x));
                }
                this.ShowMsg(LogMsgType.Incoming, "全部完成");
            }
            catch (Exception ex)
            {
                this.ShowMsg(LogMsgType.Error, ex.Message);
            }
        }
        private void CompareKp(DataTable dt)
        {
            foreach (DataRow dr in this.BomDataTable.Rows)
            {
                DataRow[] arrDr = this.KpDetaltNewTable.Select(string.Format("kpnumber='{0}'", dr["kpnumber"].ToString()));
                if (arrDr.Length < 1)
                {
                    ShowMsg(LogMsgType.Warning, string.Format("BOM中的物料[{0}],在料站表中不存在  FAIL", dr["kpnumber"].ToString()));
                    continue;
                }
                else
                {
                    ShowMsg(LogMsgType.Incoming, string.Format("在料站表中找到物料[{0}] PASS", dr["kpnumber"].ToString()));
                }

                ShowMsg(LogMsgType.Normal, "正在比对位置..");

                string[] bomloc = dr["loction"].ToString().Substring(dr["loction"].ToString().Length - 1, 1) == "," ? dr["loction"].ToString().Substring(0, dr["loction"].ToString().Length - 1).Split(',') : dr["loction"].ToString().Split(',');
                string[] kpdetalloc = arrDr[0]["loction"].ToString().Substring(arrDr[0]["loction"].ToString().Length - 1, 1) == "," ? arrDr[0]["loction"].ToString().Substring(0, arrDr[0]["loction"].ToString().Length - 1).Split(',') : arrDr[0]["loction"].ToString().Split(',');

                if (bomloc.Length != kpdetalloc.Length)
                    this.ShowMsg(LogMsgType.Error, string.Format("BOM里的器件位置和料站表中的器件位置,不一致\nBOM:[{0}]\n料站:[{1}]",
                        dr["loction"].ToString(), arrDr[0]["loction"].ToString()));

                foreach (string str in bomloc) //如果料站表的器件位置多于Bom里器件位置时？
                {
                    if (!this.FindList(str, kpdetalloc))
                    {
                        ShowMsg(LogMsgType.Warning, string.Format("BOM中物料[{0}],的位置[{1}]与站表中物料[{2}]的位置不符[{3}]",
                            dr["kpnumber"].ToString(), str, arrDr[0]["kpnumber"].ToString(), arrDr[0]["loction"].ToString()));
                    }
                }
            }
            ShowMsg(LogMsgType.Incoming, "完成-----BOM物料累计[" + this.BomDataTable.Rows.Count + "]颗物料-,-料站表累计[" + this.KpDetaltNewTable.DefaultView.ToTable(true, "kpnumber").Rows.Count + "]颗物料");
        }

        private void CompareKp_New(DataTable dt)
        {
            foreach (DataRow dr in this.BomDataTable.Rows)
            {
                DataRow[] arrDr = dt.Select(string.Format("kpnumber='{0}'", dr["kpnumber"].ToString()));
                if (arrDr.Length < 1)
                {
                    ShowMsg(LogMsgType.Warning, string.Format("BOM中的物料[{0}],在料站表中不存在  FAIL", dr["kpnumber"].ToString()));
                    continue;
                }
                else
                {
                    ShowMsg(LogMsgType.Incoming, string.Format("在料站表中找到物料[{0}] PASS", dr["kpnumber"].ToString()));
                }

                ShowMsg(LogMsgType.Normal, "正在比对位置..");

                string[] bomloc = dr["loction"].ToString().Substring(dr["loction"].ToString().Length - 1, 1) == "," ? dr["loction"].ToString().Substring(0, dr["loction"].ToString().Length - 1).Split(',') : dr["loction"].ToString().Split(',');
                string[] kpdetalloc = arrDr[0]["loction"].ToString().Substring(arrDr[0]["loction"].ToString().Length - 1, 1) == "," ? arrDr[0]["loction"].ToString().Substring(0, arrDr[0]["loction"].ToString().Length - 1).Split(',') : arrDr[0]["loction"].ToString().Split(',');

                if (bomloc.Length != kpdetalloc.Length)
                    this.ShowMsg(LogMsgType.Error, string.Format("BOM里的器件位置和料站表中的器件位置,不一致\nBOM:[{0}]\n料站:[{1}]",
                        dr["loction"].ToString(), arrDr[0]["loction"].ToString()));

                foreach (string str in bomloc) //如果料站表的器件位置多于Bom里器件位置时？
                {
                    if (!this.FindList(str, kpdetalloc))
                    {
                        ShowMsg(LogMsgType.Warning, string.Format("BOM中物料[{0}],的位置[{1}]与站表中物料[{2}]的位置不符[{3}]",
                            dr["kpnumber"].ToString(), str, arrDr[0]["kpnumber"].ToString(), arrDr[0]["loction"].ToString()));
                    }
                }
                ShowMsg(LogMsgType.Outgoing, "位置比对完成..");
            }
            ShowMsg(LogMsgType.Incoming, "完成-----BOM物料累计[" + this.BomDataTable.Rows.Count + "]颗物料-,-料站表累计[" + dt.DefaultView.ToTable(true, "kpnumber").Rows.Count + "]颗物料");
        }
        private bool FindList(string str, string[] lsstr)
        {
            foreach (string item in lsstr)
            {
                if (item.Trim() == str.Trim())
                    return true;
            }
            return false;
        }

        private void RunKpdetalt_New(string partnumber, string lineid, string pcbside, DataTable mdt)
        {
            DataTable groupTemp = FrmBLL.publicfuntion.getNewTable(mdt.DefaultView.ToTable(true, "replacegroup"), "replacegroup<>'' and replacegroup is not null");

            KpDetaltNewTable = new DataTable();
            KpDetaltNewTable = mdt.Clone();

            #region 处理相同的物料组成员
            this.ShowMsg(LogMsgType.Outgoing, "处理相同的物料组成员的物料位置");
            foreach (DataRow dr in groupTemp.Rows)
            {
                DataRow[] arrDr = mdt.Select(string.Format("replacegroup='{0}'", dr["replacegroup"].ToString()));

                string loctionTemp = string.Empty;
                string strTemp = string.Empty;
                List<string> lcTemp = new List<string>();

                for (int x = 0; x < arrDr.Length; x++)
                {
                    strTemp += arrDr[x]["loction"].ToString() + ",";
                }

                foreach (string str in strTemp.Split(','))
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (!this.FindList(str, lcTemp.ToArray()))
                        {
                            loctionTemp += str + ",";
                            lcTemp.Add(str);
                        }
                    }
                }

                for (int i = 0; i < arrDr.Length; i++)
                {
                    KpDetaltNewTable.Rows.Add(
                        arrDr[i]["partnumber"].ToString(),
                        arrDr[i]["lineId"].ToString(),
                        arrDr[i]["pcbside"].ToString(),
                        arrDr[i]["bomver"].ToString(),
                        arrDr[i]["stationno"].ToString(),
                        arrDr[i]["kpnumber"].ToString(),
                        arrDr[i]["kpdesc"].ToString(),
                        arrDr[i]["replacegroup"].ToString(),
                        loctionTemp);
                }

            }
            this.ShowMsg(LogMsgType.Incoming, "存在物料组的数据处理完成");
            #endregion

            #region 处理没有物料组的成员
            DataRow[] arrDrT = mdt.Select("replacegroup='' or replacegroup is null ");

            Dictionary<string, DataRow> lsdr = new Dictionary<string, DataRow>();
            List<string> lskp = new List<string>();
            Dictionary<string, string> dickp = new Dictionary<string, string>();
            this.ShowMsg(LogMsgType.Outgoing, "正在处理没有物料组成员的物料位置");
            foreach (DataRow dr in arrDrT)
            {
                #region 处理不存在物料组成员但是有料号重复的数据(一个物料存在于不同的料站)
                if (!this.FindList(dr["kpnumber"].ToString(), lskp.ToArray()))
                {
                    lskp.Add(dr["kpnumber"].ToString().Trim());
                    dickp.Add(dr["kpnumber"].ToString().Trim(), dr["loction"].ToString().Trim());
                    lsdr.Add(dr["kpnumber"].ToString().Trim(), dr);
                }
                else
                {
                    lsdr.Remove(dr["kpnumber"].ToString().Trim());
                    string s = dr["loction"].ToString();  //当前的
                    string a = dickp[dr["kpnumber"].ToString().Trim()];//原来有的
                    dickp.Remove(dr["kpnumber"].ToString().Trim());

                    string allStr = s + "," + a;
                    List<string> tm = new List<string>();
                    string strTemp = string.Empty;
                    foreach (string sr in allStr.Split(','))
                    {
                        if (!string.IsNullOrEmpty(sr))
                        {
                            if (!FindList(sr, tm.ToArray()))
                            {
                                strTemp += sr + ",";
                                tm.Add(sr);
                            }
                        }
                    }
                    dickp.Add(dr["kpnumber"].ToString().Trim(), strTemp);
                    dr["loction"] = strTemp;
                    lsdr.Add(dr["kpnumber"].ToString().Trim(), dr);
                }
                #endregion
            }
            this.ShowMsg(LogMsgType.Incoming, "没有物料组成员的物料处理完成");
            #endregion
            this.ShowMsg(LogMsgType.Outgoing, "正在生成数据报表");

            foreach (DataRow _dr in lsdr.Values)
            {
                KpDetaltNewTable.Rows.Add(_dr.ItemArray);
            }
            this.ShowMsg(LogMsgType.Incoming, "数据报表处理完毕");
            this.ShowData1(KpDetaltNewTable);
        }

        private void btdownloaddata_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cbpartnumber.Text))
                return;
            if (iasyncresult == null || iasyncresult.IsCompleted)
            {
                this.eventrunkpdetalt = new delegateRunkpdetalt(this.RunKpdetalt_New);
                iasyncresult = this.eventrunkpdetalt.BeginInvoke(this.cbpartnumber.Text.Trim(),
                    this.cbmachine.Text.Trim(), this.cbpcbside.Text.Trim(),
                    FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebSmtKpMaster.Instance.GetPartnumberAData(this.cbpartnumber.Text.Trim(),
                    this.cbmachine.Text.Trim(),
                    this.cbpcbside.Text.Trim() == "T+B" ? string.Empty : this.cbpcbside.Text.Trim())),
                    null, null);
            }
            else
            {
                MessageBoxEx.Show("程序还在运作中,请稍后....");
            }
        }

        private void cbpartnumber_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cbpartnumber.Text))
            {
                this.cbmachine.Items.Clear();
                return;
            }
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebSmtKpMaster.Instance.GetMachineIdAndMasterIdListByPartnumber(this.cbpartnumber.Text));
            this.cbmachine.Items.Clear();
            List<string> linename = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                if (!string.IsNullOrEmpty(dr["lineId"].ToString()))
                {
                    string strline = dr["lineId"].ToString().Substring(0, dr["lineId"].ToString().Length - 2);
                    if (!chklist(strline, linename))
                        linename.Add(strline);
                }
            }
            this.cbmachine.Items.AddRange(linename.ToArray());
        }

        private bool chklist(string str, List<string> lsstr)
        {
            foreach (string item in lsstr)
            {
                if (item == str)
                    return true;
            }
            return false;
        }
        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            // this.dataGridView1[e.ColumnIndex,e.RowIndex].ToolTipText=string.Format()
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            this.ShowMsg(LogMsgType.Outgoing, "请重新加载BOM数据");
            this.BomDataTable = new DataTable();
            if (this.checkBoxX1.Checked)
                this.gelidip = true;
            else
                this.gelidip = false;
        }

        private void btsmtsoft_Click(object sender, EventArgs e)
        {
            try
            {
                if (iasyncresult != null && !iasyncresult.IsCompleted)
                {
                    MessageBoxEx.Show("程序还在运作中,请稍后....");
                    return;
                }
                this.mProductname = string.Empty;
                this.mPartnumber = string.Empty;
                this.mLineId = string.Empty;

                FrmPartInfo fpi = new FrmPartInfo(this);
                if (fpi.ShowDialog() != DialogResult.Yes)
                    return;
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "请选择SMT程式";
                ofd.Filter = "(*.txt 文本文件)|*.txt|(*.CSV 文件)|*.CSV";
                ofd.Multiselect = true;
                //ofd.InitialDirectory = "c:\\";
                DialogResult dlr = ofd.ShowDialog();
                if (dlr == DialogResult.Yes || dlr == DialogResult.OK)
                {
                    //string Err = string.Empty;
                    //this.dataGridView2.DataSource =  FrmBLL.publicfuntion.LoadCSV(ofd.FileNames, "080106",out Err);
                    eventsavesmtsoftkplist = new delegatesavesmtsoftkplist(SaveSmtSoftKpList);
                    iasyncresult = eventsavesmtsoftkplist.BeginInvoke(ofd.FileNames, this.mLineId, null, null);
                }
            }
            catch (Exception ex)
            {
                this.ShowMsg(LogMsgType.Error, ex.Message);
            }
        }

        private void SaveSmtSoftKpList(string[] filenames, string machine)
        {
            try
            {
                this.ShowMsg(LogMsgType.Outgoing, "正在处理SMT料站表......");
                this.SmtSoftKpList = new DataTable();
                string Err="";
                string extension = "";
                foreach (string item in filenames)
                {
                    extension = System.IO.Path.GetExtension(item).ToUpper();//扩展名 “.aspx”
                }
                if (extension.Contains("TXT"))
                    SmtSoftKpList = FrmBLL.publicfuntion.LoadTxt(filenames, machine, out Err);
                if(extension.Contains("CSV"))
                    SmtSoftKpList = FrmBLL.publicfuntion.LoadCSV(filenames, machine, out Err);
                if (!string.IsNullOrEmpty(Err))
                {
                    if (Err.IndexOf("警告!!") != -1)
                    {
                        this.ShowMsg(LogMsgType.Error, Err);
                    }
                    else
                    {
                        this.ShowMsg(LogMsgType.Error, Err);
                        return;
                    }
                }
                this.SetPb2MaxValue(SmtSoftKpList.Rows.Count);
                int i = 0;
                FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
                ass.ExecuteOracleCommand("delete from smtsoftkplist");
                foreach (DataRow dr in SmtSoftKpList.Rows)
                {
                    string sql = string.Format("insert into smtsoftkplist(smtsoftname,pcbside,lineId,stationId,kpnumber,feeder,stationcount,loction) values('{0}','{1}','{2}','{3}','{4}','{5}','0','{6}')",
                        dr["smtsoftname"].ToString(),
                        dr["pcbside"].ToString(),
                        dr["lineId"].ToString(),
                        dr["stationId"].ToString(),
                        dr["kpnumber"].ToString(),
                        dr["feeder"].ToString(),
                        dr["loction"].ToString());
                    ass.ExecuteOracleCommand(sql);
                    this.Showpb1(i++);
                }
                string cmd = "select lineId,count(stationId) as total from smtsoftkplist group by lineId";
                DataTable dt = ass.GetDatatable(cmd);
                foreach (DataRow dr in dt.Rows)
                {
                    cmd = string.Format("update smtsoftkplist set stationcount='{0}' where lineId='{1}'", dr["total"].ToString(),
                        dr["lineId"].ToString());
                    ass.ExecuteOracleCommand(cmd);
                }
                SmtSoftKpList = ass.GetDatatable("select * from smtsoftkplist");
                this.ShowMsg(LogMsgType.Incoming, "SMT料站表处理完成.");
                this.ShowData2(SmtSoftKpList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 产生SMT程式料站表
        /// </summary>
        /// <param name="IsCreateExcel">是否保存Excel文件</param>
        /// <param name="aa">null</param>
        /// <param name="bb">null</param>
        /// <param name="dt">null</param>
        private void SmtSoftKpListCompareBomData(string IsCreateExcel, string aa, string bb, DataTable dt)
        {
            try
            {
                DataTable newtable = new DataTable();
                this.SmtKpTable = new DataTable();
                newtable.Columns.Add("产品料号", System.Type.GetType("System.String"));
                newtable.Columns.Add("产品描述", System.Type.GetType("System.String"));
                newtable.Columns.Add("机器编号", System.Type.GetType("System.String"));
                newtable.Columns.Add("BOM版本", System.Type.GetType("System.String"));
                newtable.Columns.Add("FEEDER类型", System.Type.GetType("System.String"));
                newtable.Columns.Add("料站总数", System.Type.GetType("System.String"));
                newtable.Columns.Add("料站", System.Type.GetType("System.String"));
                newtable.Columns.Add("组件料号", System.Type.GetType("System.String"));
                newtable.Columns.Add("替代组", System.Type.GetType("System.String"));
                newtable.Columns.Add("优先级", System.Type.GetType("System.String"));
                newtable.Columns.Add("品名与规格", System.Type.GetType("System.String"));
                newtable.Columns.Add("数量", System.Type.GetType("System.String"));
                newtable.Columns.Add("面别", System.Type.GetType("System.String"));
                newtable.Columns.Add("程式名", System.Type.GetType("System.String"));
                newtable.Columns.Add("组件位置", System.Type.GetType("System.String"));

                SmtKpTable.Columns.Add("partnumber", System.Type.GetType("System.String"));
                SmtKpTable.Columns.Add("lineId", System.Type.GetType("System.String"));
                SmtKpTable.Columns.Add("pcbside", System.Type.GetType("System.String"));
                SmtKpTable.Columns.Add("bomver", System.Type.GetType("System.String"));
                SmtKpTable.Columns.Add("stationno", System.Type.GetType("System.String"));
                SmtKpTable.Columns.Add("kpnumber", System.Type.GetType("System.String"));
                SmtKpTable.Columns.Add("kpdesc", System.Type.GetType("System.String"));
                SmtKpTable.Columns.Add("replacegroup", System.Type.GetType("System.String"));
                SmtKpTable.Columns.Add("loction", System.Type.GetType("System.String"));

                this.ShowMsg(LogMsgType.Outgoing, "正在处理料站表格式.......");
                int i = 0;
                this.SetPb2MaxValue(this.SmtSoftKpList.Rows.Count);
                foreach (DataRow dr in this.SmtSoftKpList.Rows)
                {
                    i++;
                    DataRow[] arrDr = this.BomDataTable.Select(string.Format("kpnumber='{0}'", dr["kpnumber"].ToString()));
                    //判断料在BOM中是否存在
                    if (arrDr.Length < 1)
                        throw new Exception(string.Format("料站表中的物料[{0}] 在BOM中没有找到", dr["kpnumber"].ToString()));
                    //判断是否存在替代料
                    if (!string.IsNullOrEmpty(arrDr[0]["kpgroup"].ToString().Trim()))
                    {
                        DataRow[] _arrDr = this.BomDataTable.Select(string.Format("kpgroup='{0}'", arrDr[0]["kpgroup"].ToString()));
                        foreach (DataRow drItem in _arrDr)
                        {
                            newtable.Rows.Add(this.mPartnumber, this.mProductname,
                                dr["lineId"].ToString(),
                                drItem["bomver"].ToString(),
                                dr["feeder"].ToString(),
                                dr["stationcount"].ToString(),
                                dr["stationId"].ToString(),
                                drItem["kpnumber"].ToString(),
                                drItem["kpgroup"].ToString(),
                                "0",
                                drItem["kpdesc"].ToString(),
                                drItem["qty"].ToString(),
                                dr["pcbside"].ToString(),
                                dr["smtsoftname"].ToString(),
                                dr["loction"].ToString());

                            this.SmtKpTable.Rows.Add(this.mPartnumber,
                                dr["lineId"].ToString(),
                                dr["pcbside"].ToString(),
                                drItem["bomver"].ToString(),
                                dr["stationId"].ToString(),
                                drItem["kpnumber"].ToString(),
                                drItem["kpdesc"].ToString(),
                                drItem["kpgroup"].ToString(),
                                 dr["loction"].ToString());
                        }
                    }
                    else
                    {
                        newtable.Rows.Add(this.mPartnumber, this.mProductname,
                            dr["lineId"].ToString(),
                            arrDr[0]["bomver"].ToString(),
                            dr["feeder"].ToString(),
                            dr["stationcount"].ToString(),
                            dr["stationId"].ToString(),
                            dr["kpnumber"].ToString(),
                            "",
                            "0",
                            arrDr[0]["kpdesc"].ToString(),
                            arrDr[0]["qty"].ToString(),
                            dr["pcbside"].ToString(),
                            dr["smtsoftname"].ToString(),
                            dr["loction"].ToString());

                        this.SmtKpTable.Rows.Add(this.mPartnumber,
                            dr["lineId"].ToString(),
                            dr["pcbside"].ToString(),
                            arrDr[0]["bomver"].ToString(),
                            dr["stationId"].ToString(),
                            dr["kpnumber"].ToString(),
                            arrDr[0]["kpdesc"].ToString(),
                            "",
                             dr["loction"].ToString());
                    }

                    this.Showpb1(i);
                }
                this.ShowMsg(LogMsgType.Incoming, "料站表格式处理完成。");

                DataTable _dt = newtable.DefaultView.ToTable(true, "面别");
                FrmBLL.ClsAllExcel exl = new FrmBLL.ClsAllExcel();
                this.SetPb2MaxValue(_dt.Rows.Count);

                if (IsCreateExcel == "YES")
                {
                    this.ShowMsg(LogMsgType.Outgoing, "正在输出料站表....");
                    this.ShowMsg(LogMsgType.Outgoing, "累计" + _dt.Rows.Count + "份料站表");
                    i = 0;
                    string filename = string.Empty;
                    foreach (DataRow _itm in _dt.Rows)
                    {
                        i++;
                        this.ShowMsg(LogMsgType.Outgoing, "正在输出第" + i + "份料站表....");
                        DataTable _dta = FrmBLL.publicfuntion.getNewTable(newtable, string.Format("面别='{0}'", _itm["面别"].ToString()));
                        string smtsoftname = _dta.Rows[0]["程式名"].ToString().Trim();
                        _dta.Columns.Remove("面别");
                        _dta.Columns.Remove("程式名");
                        if (File.Exists(string.Format("{0}Excel\\{1}.xls",
                            System.AppDomain.CurrentDomain.BaseDirectory, smtsoftname)))
                        {
                            File.Delete(string.Format("{0}Excel\\{1}.xls",
                                System.AppDomain.CurrentDomain.BaseDirectory, smtsoftname));
                        }
                        exl.ExportExcel(_dta, filename = string.Format("{0}Excel\\{1}.xls",
                            System.AppDomain.CurrentDomain.BaseDirectory, smtsoftname),
                            string.Format("S{2}{3}L-{0}M {1}",
                            _itm["面别"].ToString(), smtsoftname,
                            int.Parse(newtable.Rows[0]["机器编号"].ToString().Substring(0, 2)),
                            int.Parse(newtable.Rows[0]["机器编号"].ToString().Substring(4, 2)).ToString("D2")));
                        this.Showpb1(i);
                        this.ShowMsg(LogMsgType.Incoming, "第" + i + "份料站表输出完成,保存路径:\n" + filename);
                    }
                    this.ShowMsg(LogMsgType.Incoming, "全部完成");
                }
                this.ShowData2(newtable);
            }
            catch (Exception ex)
            {
                this.ShowMsg(LogMsgType.Error, ex.Message);
            }
        }

        private void CreateKpnumberTable(DataTable dt, string filename, string workbookname)
        {

        }

        private void btcadB_Click(object sender, EventArgs e)
        {
            try
            {
                if (iasyncresult != null && !iasyncresult.IsCompleted)
                {
                    MessageBoxEx.Show("程序还在运作中,请稍后....");
                    return;
                }
                if (this.BomDataTable == null || this.BomDataTable.Rows.Count < 1)
                {
                    MessageBoxEx.Show("请先加载BOM数据");
                    return;
                }
                if (this.SmtSoftKpList == null || this.SmtSoftKpList.Rows.Count < 1)
                {
                    MessageBoxEx.Show("请先加载SMT料站表信息");
                    return;
                }
                eventrunkpdetalt = new delegateRunkpdetalt(SmtSoftKpListCompareBomData);
                iasyncresult = eventrunkpdetalt.BeginInvoke("YES", null, null, null, null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rbbomsfccom_CheckedChanged(object sender, EventArgs e)
        {
            this.KpDetaltNewTable = new DataTable();
            if (this.rbbomsfccom.Checked)
                this.groupPanel2.Enabled = true;
            else
                this.groupPanel2.Enabled = false;
        }

        private void bt_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (iasyncresultExcel != null && !iasyncresultExcel.IsCompleted)
                {
                    MessageBoxEx.Show("程序还在运作中,请稍后....");
                    return;
                }

                FrmPartInfo fpi = new FrmPartInfo(this);
                if (fpi.ShowDialog() != DialogResult.Yes)
                    return;

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "请选择转换后的Excel";
                ofd.Filter = "Excel_2003|*.xls";
                ofd.Multiselect = true;
                ofd.ShowDialog();

                eventsaveExcelsmtsoftkplist = new delegatesaveExcelsmtsoftkplist(SaveExcelSmtSoftKpList);
                iasyncresultExcel = eventsaveExcelsmtsoftkplist.BeginInvoke(ofd.FileNames, null, null);
            }
            catch (Exception ex)
            {
                this.ShowMsg(LogMsgType.Error, ex.Message);
            }

        }

        private void SaveExcelSmtSoftKpList(string[] sFileName)
        {
            this.ShowMsg(LogMsgType.Warning, "开始读取Excel.");   
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("IID", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("smtsoftname", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("pcbside", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("LineId", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("stationId", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("kpnumber", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("feeder", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("loction", System.Type.GetType("System.String"));

            foreach (string item in sFileName)
            {
                DataTable dt1 = new DataTable();
                FrmBLL.ClsAllExcel cls = new FrmBLL.ClsAllExcel();
                cls.OpenFileName = item;
                cls.OpenExcelFile();
                dt1 = cls.getAllCellsValue();
                cls.CloseExcelFile();
                foreach (DataRow dr in dt1.Rows)
                {
                    dtTemp.ImportRow(dr);
                }
            }
            this.ShowMsg(LogMsgType.Incoming, "读取Execel完成.");
            this.ShowMsg(LogMsgType.Warning, "开始处理料站表...");
            SmtSoftKpList = dtTemp;


            this.SetPb2MaxValue(SmtSoftKpList.Rows.Count);
            int i = 0;
            FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
            ass.ExecuteOracleCommand("delete from smtsoftkplist");
            foreach (DataRow dr in SmtSoftKpList.Rows)
            {
                string sql = string.Format("insert into smtsoftkplist(smtsoftname,pcbside,lineId,stationId,kpnumber,feeder,stationcount,loction) values('{0}','{1}','{2}','{3}','{4}','{5}','0','{6}')",
                    dr["smtsoftname"].ToString(),
                    dr["pcbside"].ToString(),
                    dr["lineId"].ToString(),
                    dr["stationId"].ToString(),
                    dr["kpnumber"].ToString(),
                    dr["feeder"].ToString(),
                    dr["loction"].ToString());
                ass.ExecuteOracleCommand(sql);
                this.Showpb1(i++);
            }
            string cmd = "select lineId,count(stationId) as total from smtsoftkplist group by lineId";
            DataTable dt = ass.GetDatatable(cmd);
            foreach (DataRow dr in dt.Rows)
            {
                cmd = string.Format("update smtsoftkplist set stationcount='{0}' where lineId='{1}'", dr["total"].ToString(),
                    dr["lineId"].ToString());
                ass.ExecuteOracleCommand(cmd);
            }
            SmtSoftKpList = ass.GetDatatable("select * from smtsoftkplist");
            this.ShowMsg(LogMsgType.Incoming, "Excel料站表处理完成.");
            this.ShowData2(SmtSoftKpList);
        }
    }
}
