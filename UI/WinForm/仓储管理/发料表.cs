using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FrmBLL;
using System.IO;
using DevComponents.DotNetBar;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class FrmSentMaterial : Office2007Form// Form
    {
        public FrmSentMaterial(MainParent frm)
        {
            InitializeComponent();
            this.mFrm = frm;
        }

        MainParent mFrm;
        private delegate void DelegateShowInfo(string woid, string process);
        DelegateShowInfo dsi;
        private void ShowInfo(DataTable dt)
        {
            dataGridViewX1.Invoke(new EventHandler(delegate
                {
                    dataGridViewX1.DataSource = dt;
                }));
        }
        private void LoadInfo(string woid, string process)
        {
            mFrm.ShowPrgMsg("正在查询...", MainParent.MsgType.Warning);
            ShowInfo(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.
                    Instance.GetMaterialInfoByWoid(woid)));
            mFrm.ShowPrgMsg("查询完成...", MainParent.MsgType.Outgoing);
        }
        #region 之前
        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.dataGridViewX1.DataSource = Readtxtfile1();

            RefWebService_BLL.refWebtPartStorehousehad.Instance.QueryKpnumberMoreThanDays("");
        }

        private DataTable Readtxtfile1()
        {
            StreamReader sr = new StreamReader("c:\\123.txt", Encoding.Default);
            string line;
            int i = 0;
            DataTable dt = new DataTable("mydt");
            dt.Columns.Add("生产工单号", System.Type.GetType("System.String"));
            dt.Columns.Add("成品料号", System.Type.GetType("System.String"));
            dt.Columns.Add("组件料号", System.Type.GetType("System.String"));
            dt.Columns.Add("组件物料描述", System.Type.GetType("System.String"));
            dt.Columns.Add("数量", System.Type.GetType("System.String"));
            dt.Columns.Add("制程段", System.Type.GetType("System.String"));

            dt.Columns.Add("仓库", System.Type.GetType("System.String"));
            dt.Columns.Add("库位", System.Type.GetType("System.String"));

            while ((line = sr.ReadLine()) != null)
            {
                i++;
                if (i > 6)
                {
                    if (line.IndexOf("----------------------") == -1)
                    {
                        string[] arr = line.Split('|');
                        //MessageBox.Show(arr.Length.ToString());
                        DataTable _dta = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.GetKpnumberLocation(arr[2], int.Parse(arr[8])));
                        string[] arrloc = GetLocList(_dta);
                        dt.Rows.Add(arr[1], arr[2], arr[5], arr[6], arr[8], arr[11], GetLocList(_dta), arrloc[0], arrloc[1]);
                    }
                }
            }
            return dt;

        }
        private string[] GetLocList(DataTable dt)
        {
            //仓库编号+库位编号
            string[] arrStoreLoc = new string[2];

            foreach (DataRow dr in dt.Rows)
            {
                if (arrStoreLoc[0].IndexOf(dr["storehouseId"].ToString()) == -1)
                {
                    arrStoreLoc[0] += ";" + dr["storehouseId"].ToString();
                }
                arrStoreLoc[1] += "," + dr["locId"].ToString();
            }
            return arrStoreLoc;
        }
        #endregion

        private void tb_woid_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_woid.Text))
                return;
            //DataTable dt;
            //dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWoInfo.Instance.GetALLWoInfoByWoinfo(this.tb_woid.Text.Trim()));
            ////dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.GetKpnumberByWoid(this.tb_woid.Text.Trim()));
            //if (dt.Rows.Count < 1)
            //{
            //    MessageBox.Show("该工单信息不存在!");
            //    this.tb_woid.SelectAll();
            //    this.tb_woid.Focus();
            //}

        }
        IAsyncResult iasynresult;
        private void bt_select_Click1(object sender, EventArgs e)
        {
            //this.dataGridViewX1.DataSource = this.GetWoKpnumber(this.tb_woid.Text.Trim(), this.cb_process.Text);

            if (iasynresult != null && !iasynresult.IsCompleted)
            {
                mFrm.ShowPrgMsg("正在查询...", MainParent.MsgType.Warning);
                return;
            }
            dsi = new DelegateShowInfo(LoadInfo);
            iasynresult = dsi.BeginInvoke(this.tb_woid.Text.Trim(), this.cb_process.Text, null, null);

        }
        #region 之前
        /// <summary>
        /// 获取自动发料表
        /// </summary>
        private DataTable GetWoKpnumber(string strwo, string process)
        {
            DataTable dt = ReleaseData.arrByteToDataTable(
                RefWebService_BLL.refWebtPartStorehousehad.Instance.GetKpnumberByWoid(strwo, process));
            DataTable mdt = new DataTable("ndt");
            DataRow mdr;
            bool flag = false;
            foreach (DataRow dr in dt.Rows)
            {
                DataTable _dt = ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.GetKpnumberLocation(dr["kpnumber"].ToString(), int.Parse(dr["qty"].ToString())));//料号，领料数
                if (!flag)
                {
                    flag = true;
                    mdt = _dt.Clone();
                }
                float count = 0;
                if (_dt == null || _dt.Rows.Count < 1)
                {
                    //mdr.ItemArray = item.ItemArray;
                    mdt.Rows.Add("NA", dr["kpnumber"].ToString(), "无库存", "无库存", 0);
                }
                else
                {
                    foreach (DataRow item in _dt.Rows)
                    {
                        mdr = mdt.NewRow();
                        count += float.Parse(item["qty"].ToString());
                        if (count < float.Parse(dr["qty"].ToString()))
                        {
                            mdr.ItemArray = item.ItemArray;
                            mdt.Rows.Add(mdr);
                        }
                        else
                        {
                            mdr.ItemArray = item.ItemArray;
                            mdt.Rows.Add(mdr);
                            break;

                        }

                    }
                }
            }
            return mdt;

        }
        #endregion
        private readonly string[] mBomHadInfo =
            new string[] { "生产工单号", "成品料号", "组件料号", "组件物料描述", "数量", "制程段" };
        private enum eBomHadInfo
        {
            生产工单号,
            成品料号,
            组件料号,
            组件物料描述,
            数量,
            制程段
        }

        private string Readtxtfile()
        {
            //DataSet ds = new DataSet();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择工单料表";
            ofd.Filter = "(*.txt)|*.txt";
            DialogResult dlr = ofd.ShowDialog();
            if (dlr == DialogResult.Yes || dlr == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ofd.FileName, Encoding.Default);
                string line;
                int i = 0;
                DataTable dt = new DataTable("mydt");
                dt.Columns.Add("生产工单号", System.Type.GetType("System.String"));
                dt.Columns.Add("成品料号", System.Type.GetType("System.String"));
                dt.Columns.Add("组件料号", System.Type.GetType("System.String"));
                dt.Columns.Add("组件物料描述", System.Type.GetType("System.String"));
                dt.Columns.Add("数量", System.Type.GetType("System.String"));
                dt.Columns.Add("制程段", System.Type.GetType("System.String"));
                while ((line = sr.ReadLine()) != null)
                {
                    i++;
                    if (i > 6)
                    {
                        if (line.IndexOf("----------------------") == -1)
                        {
                            string[] arr = line.Split('|');
                            dt.Rows.Add(arr[1], arr[2], arr[5], arr[6], arr[8], arr[11]);
                        }
                    }
                }
                ClsAllExcel cs = new ClsAllExcel();

                int len = (System.AppDomain.CurrentDomain.BaseDirectory + "ConvertFile").Length + 1;
                string filename = Path.GetFileName(ofd.FileName).Split('.')[0];
                string excelfilename = string.Empty;
                //System.IO.Directory.GetCurrentDirectory()+ "\\ConvertFileOK" + "\\"+filename+".xls"
                if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "ConvertFileOK\\" + filename + ".xls"))
                {
                    //如果存在则删除
                    File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + "ConvertFileOK\\" + filename + ".xls");
                }
                cs.ExportExcel(dt, excelfilename = System.AppDomain.CurrentDomain.BaseDirectory + "ConvertFileOK\\" + filename + ".xls", dt.Rows[1][0].ToString().Trim() + "-" + dt.Rows[1][1].ToString().Trim() + "-A");
                sr.Close();
                string err = string.Empty;
                if (string.IsNullOrEmpty(err = BomInfoToDB(excelfilename)))
                    return string.Empty;
                else
                    return err;
            }
            return "没有打开备料表文件";
        }
        private string BomInfoToDB(string Excelfilename)
        {
            try
            {
                //获取excel中有几个表
                List<string> lsname = ClsReadExcel.GetTableNames(Excelfilename);// BLL.ClsReadExcel.GetTableNames(ofd.FileName);
                if (lsname.Count > 1)
                    throw new Exception("料表格式错误,请确保Excel文件中只有一张表..");
                if (lsname[0].Split('-').Length != 3)
                    throw new Exception("料表名称规则不符,正确格式 => \"工单号-成品料号-BOM版本\"");

                DataTable _dt = ClsReadExcel.getTable(Excelfilename, lsname[0]);
                if (_dt.Rows.Count < 1)
                    throw new Exception("料表中没有数据,请检查..");

                if (_dt.Columns.Count != this.mBomHadInfo.Length)
                    throw new Exception("料表格式错误,请检查料表的第一行标题信息是否不符合规则");

                for (int x = 0; x < _dt.Columns.Count; x++)
                {
                    bool flag = false;
                    foreach (string item in this.mBomHadInfo)
                    {
                        if (_dt.Columns[x].Caption == item)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                        throw new Exception("料表标题不符合规则,请查看文件");

                    DataTable mdt = _dt.DefaultView.ToTable(true, new string[] { "生产工单号" });
                    int xx = 0;
                    for (int i = 0; i < mdt.Rows.Count; i++)
                    {

                        if (!string.IsNullOrEmpty(mdt.Rows[i][0].ToString()))
                            xx++;
                    }
                    if (xx != 1)
                        throw new Exception("导入的工单料表中存在多个工单号名称,请检查..");


                    DataTable mdtt = _dt.DefaultView.ToTable(true, new string[] { "成品料号" });
                    int xxx = 0;
                    for (int i = 0; i < mdtt.Rows.Count; i++)
                    {

                        if (!string.IsNullOrEmpty(mdtt.Rows[i][0].ToString()))
                            xxx++;
                    }
                    if (xxx != 1)
                        throw new Exception("导入的工单料表中存在多个成品料号,请检查..");
                }

                //查询导入的料表对应的工单号是否在系统中已经建立
                int ic = 0;
                foreach (DataRow dr in _dt.Rows)
                {
                    if (!dr.IsNull("生产工单号") && !dr.IsNull("成品料号") && !dr.IsNull("组件料号"))
                    {
                        RefWebService_BLL.refWebtWoBomInfo.Instance.InsertWoBomInfo(new WebServices.tWoBomInfo.T_WO_BOM_INFO()
                        {
                            kpdesc = dr[eBomHadInfo.组件物料描述.ToString()].ToString(),
                            kpnumber = dr[eBomHadInfo.组件料号.ToString()].ToString(),
                            partnumber = dr[eBomHadInfo.成品料号.ToString()].ToString(),
                            process = dr[eBomHadInfo.制程段.ToString()].ToString(),
                            qty = string.IsNullOrEmpty(dr[eBomHadInfo.数量.ToString()].ToString()) ? 0 : int.Parse(dr[eBomHadInfo.数量.ToString()].ToString()),
                            userId = this.mFrm.gUserInfo.userId,
                            woId = dr[eBomHadInfo.生产工单号.ToString()].ToString(),
                            wbiId = Guid.NewGuid(),
                            bomver = lsname[0].Split('-')[2].Replace("$'", "")
                        });
                        //this.ShowMsg(LogMsgType.Outgoing, string.Format("第[{0}]颗:[{1}]导入成功", ic + 1, dr[eBomHadInfo.组件料号.ToString()].ToString()));
                        ic++;
                    }
                }

                
                FrmBLL.publicfuntion.InserSystemLog(this.mFrm.gUserInfo.userId, "导入备料表", "新增", "导入备料表: " + "生产工单:" + _dt.Rows[0][eBomHadInfo.生产工单号.ToString()].ToString() + ";成品料号: " + _dt.Rows[0][eBomHadInfo.成品料号.ToString()].ToString());
                return string.Empty;
                //this.mFrm.ShowPrgMsg("料件导入完成..", MainParent.MsgType.Incoming);
                // MessageBoxEx.Show(string.Format("料件导入完成\n累计成功导入[{0}]颗物料,请核对", ic));
            }
            catch (Exception ex)
            {
                return ex.Message;
                //this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void bt_selectwobominfo_Click(object sender, EventArgs e)
        {
            if (iasynresult != null && !iasynresult.IsCompleted)
            {
                mFrm.ShowPrgMsg("正在查询,请稍后...", MainParent.MsgType.Warning);
                return;
            }
            string err = string.Empty;
            if (string.IsNullOrEmpty(err = Readtxtfile()))
                this.mFrm.ShowPrgMsg("数据导入成功!", MainParent.MsgType.Incoming);
            else
            {
                this.mFrm.ShowPrgMsg("数据导入失败:\n" + err, MainParent.MsgType.Error);
            }
        }

        private void bt_toexcel_Click(object sender, EventArgs e)
        {

            if (iasynresult != null && !iasynresult.IsCompleted)
            {
                mFrm.ShowPrgMsg("正在查询...", MainParent.MsgType.Warning);
                return;
            }
            if (this.dataGridViewX1.Rows.Count < 1)
            {
                mFrm.ShowPrgMsg("没有可以导出的数据!!", MainParent.MsgType.Warning);
                return;
            }
            FrmBLL.DataGridViewToExcel.DataToExcel(dataGridViewX1);
            this.mFrm.ShowPrgMsg("导出excel成功!", MainParent.MsgType.Incoming);
        }

        private void FrmSentMaterial_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.mFrm.gUserInfo.rolecaption == "系统开发员")
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
        }

        private void bt_select_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_woid.Text.Trim()))
                return;
            if (iasynresult != null && !iasynresult.IsCompleted)
            {
                mFrm.ShowPrgMsg("正在查询...", MainParent.MsgType.Warning);
                return;
            }
            dsi = new DelegateShowInfo(LoadInfo);
            iasynresult = dsi.BeginInvoke(this.tb_woid.Text.Trim(), this.cb_process.Text, null, null);

        }
    }
}
