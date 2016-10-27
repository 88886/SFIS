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
    public partial class EditKpMarster :Office2007Form// Form
    {
        public EditKpMarster(MainParent frm)
        {
            InitializeComponent();
            mFrm = frm;
        }
        private readonly string strFunName = "KpMasterEdit";
        private MainParent mFrm;
        private List<Dictionary<string, object>> lskpmaster = new List<Dictionary<string, object>>();
        private Dictionary<string, object> kpmaster = new Dictionary<string, object>();
        private string mStrTemp = string.Empty;
        private string mStrMasterIdTemp = string.Empty;
        private string doubleclickmasterId = string.Empty;
        private int masterIdrowindex = -1;
        private string mkpdetatltemp = string.Empty;
        public List<string> LsIp = FrmBLL.publicfuntion.GetIPList();
        private void InitVar()
        {
            mStrTemp = string.Empty;
            mStrMasterIdTemp = string.Empty;
            doubleclickmasterId = string.Empty;
            masterIdrowindex = -1;
            mkpdetatltemp = string.Empty;
        }
        private void EditKpMarster_Load(object sender, EventArgs e)
        {
            try
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
                InitVar();
                this.ShowKpMaster(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebSmtKpMaster.Instance.GetAllKpMaster()));
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void ShowKpMaster(DataTable dt)
        {
            this.dgv_ShowKpMaster.Invoke(new EventHandler(delegate {
                this.dgv_ShowKpMaster.DataSource = dt;
            }));
        }
        private void ShowKpDetalt(DataTable dt)
        {
            this.dgv_ShowKPDetalt.Invoke(new EventHandler(delegate {
                if (dt == null || dt.Rows.Count < 1)
                {
                    this.dgv_ShowKPDetalt.Rows.Clear();
                }
                else
                {
                    this.dgv_ShowKPDetalt.DataSource = dt;
                }
            }));
        }
        private DataGridViewRow RecDr = null;
        private void dgv_ShowKpMaster_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1)
                {
                    RecDr = this.dgv_ShowKpMaster.Rows[e.RowIndex];
                    if (!string.IsNullOrEmpty(this.mStrMasterIdTemp) && this.dgv_ShowKpMaster["masterId", e.RowIndex].Value.ToString() != this.mStrMasterIdTemp)
                    {
                        this.mFrm.ShowPrgMsg("已经进入修改状态,且一次只能修改一个料站的内容,请先提交或重新加载", MainParent.MsgType.Warning);
                        return;
                    }
                    #region 检查是否已经被编辑

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("FUNNAME", this.dgv_ShowKpMaster["masterId", e.RowIndex].Value.ToString());
                     dic.Add("PRJ",this.strFunName);
                     dic.Add("USERID", this.mFrm.gUserInfo.userId);
                     dic.Add("USERNAME", this.mFrm.gUserInfo.username);
                     dic.Add("PC_NAME",FrmBLL.publicfuntion.HostName + " IP:" + ((LsIp.Count > 1) ? LsIp[1] : LsIp[0]));
                     dic.Add("MAC_ADDRESS", FrmBLL.publicfuntion.getMacList()[0]);
                     string err = RefWebService_BLL.refwebtEditing.Instance.ChktEditing(FrmBLL.ReleaseData.DictionaryToJson(dic));
                    //string err = RefWebService_BLL.refwebtEditing.Instance.ChktEditing(new WebServices.tEditing.tEditing1()
                    //{
                    //    prj = this.strFunName,
                    //    funname = this.dgv_ShowKpMaster["masterId", e.RowIndex].Value.ToString(),
                    //    userId = this.mFrm.gUserInfo.userId,
                    //    username = this.mFrm.gUserInfo.username,
                    //    PC_NAME = FrmBLL.publicfuntion.HostName + " IP:" + ((LsIp.Count > 1) ? LsIp[1] : LsIp[0]),
                    //    MAC_ADD = FrmBLL.publicfuntion.getMacList()[0]
                    //});
                    if (err != "OK")
                    {
                        if (err.IndexOf("ERROR") != -1)
                        {
                            this.mFrm.ShowPrgMsg(err, MainParent.MsgType.Error);
                            return;
                        }
                        else
                        {
                            MessageBoxEx.Show(string.Format("用户:[{0}]/[{1}]--{2}正在编辑中,您现在不能进行修改!!", err.Split(':')[1].Split(',')[0], err.Split(':')[1].Split(',')[1], err.Split(':')[1].Split(',')[2]), "提示!!");
                            return;
                        }
                    }
                    #endregion

                    masterIdrowindex = e.RowIndex;
                    this.ShowKpDetalt(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebSmtKpMaster.Instance.GetKpDetalt(this.doubleclickmasterId = this.dgv_ShowKpMaster["masterId", e.RowIndex].Value.ToString())));
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }

        }
        private void dgv_ShowKpMaster_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= this.dgv_ShowKpMaster.Rows.Count)
                return;
            DataGridViewRow dgr = dgv_ShowKpMaster.Rows[e.RowIndex];
            try
            {
                if (dgr.Cells["reserve2"].Value.ToString() == "待审核")
                {
                    this.dgv_ShowKpMaster.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 160);
                }
                if (dgr.Cells["reserve2"].Value.ToString() == "审核通过")
                {
                    this.dgv_ShowKpMaster.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(61, 170, 49);
                }
                if (dgr.Cells["reserve2"].Value.ToString() == "审核失败")
                {
                    this.dgv_ShowKpMaster.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 142, 142);
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        private void dgv_ShowKpMaster_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    if (this.dgv_ShowKpMaster[e.ColumnIndex, e.RowIndex].Value.ToString().Trim() == this.mStrTemp.Trim())
                        return;
                    if (!string.IsNullOrEmpty(this.mStrMasterIdTemp) &&
                        this.dgv_ShowKpMaster["masterId", e.RowIndex].Value.ToString().Trim() != this.mStrMasterIdTemp)
                    {
                        this.mFrm.ShowPrgMsg("一次只能修改一个料站的内容,请先提交后再进行修改", MainParent.MsgType.Warning);
                        this.dgv_ShowKpMaster[e.ColumnIndex, e.RowIndex].Value = this.mStrTemp;
                        return;
                    }
                    #region xxx
                    //this.lskpmaster.Add(new Entity.tSmtKPMasterTable()
                    //{
                    //    MasterId = this.dgv_ShowKpMaster["masterId", e.RowIndex].Value.ToString().Trim(),
                    //    LineId = this.dgv_ShowKpMaster["machine", e.RowIndex].Value.ToString().Trim(),
                    //    kpnumber = this.dgv_ShowKpMaster["partnumber", e.RowIndex].Value.ToString().Trim(),
                    //    BomRev = this.dgv_ShowKpMaster["bomver", e.RowIndex].Value.ToString().Trim(),
                    //    ModelName = this.dgv_ShowKpMaster["modelname", e.RowIndex].Value.ToString().Trim(),
                    //    UserId = this.mFrm.gUserInfo.userId,// this.dgv_ShowKpMaster["userId", e.RowIndex].Value.ToString().Trim()
                    //    PcbSide = "T",
                    //    Reserve1 = this.dgv_ShowKpMaster["reserve1", e.RowIndex].Value.ToString().Trim(),
                    //    Reserve2 = "0"
                    //});
                    //this.dgv_ShowKpMaster["updateedit", e.RowIndex].Value = true;
                    //this.dgv_ShowKpMaster["reserve2", e.RowIndex].Value = "等待提交";
                    //this.mStrMasterIdTemp = this.dgv_ShowKpMaster["masterId", e.RowIndex].Value.ToString().Trim();
                    //this.dgv_ShowKpMaster.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                    #endregion
                    this.RecodeNewValue(e.RowIndex);
                    this.ShowKpDetalt(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebSmtKpMaster.Instance.GetKpDetalt(this.dgv_ShowKpMaster["masterId", e.RowIndex].Value.ToString())));
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        /// <summary>
        /// 记录更新的值
        /// </summary>
        /// <param name="rowindex"></param>
        private void RecodeNewValue(int rowindex)
        {
            this.kpmaster.Clear();
            kpmaster.Add("MASTERID", this.dgv_ShowKpMaster["masterId", rowindex].Value.ToString().Trim());
            kpmaster.Add("LINEID", this.dgv_ShowKpMaster["machine", rowindex].Value.ToString().Trim());
            kpmaster.Add("KPNUMBER", this.dgv_ShowKpMaster["partnumber", rowindex].Value.ToString().Trim());
            kpmaster.Add("BOMVER",this.dgv_ShowKpMaster["bomver", rowindex].Value.ToString().Trim());
            kpmaster.Add("MODELNAME",this.dgv_ShowKpMaster["modelname", rowindex].Value.ToString().Trim());
            kpmaster.Add("USERID",this.mFrm.gUserInfo.userId);
            kpmaster.Add("PCBSIDE","T");
             kpmaster.Add("RESERVER1", this.dgv_ShowKpMaster["reserve1", rowindex].Value.ToString().Trim());
             kpmaster.Add("RESERVER","0");
           
            //this.lskpmaster.Add(new WebServices.ExcelToDb.SMT_KP_MASTER
            //{               

            //    masterId = this.dgv_ShowKpMaster["masterId", rowindex].Value.ToString().Trim(),
            //    Lineid = this.dgv_ShowKpMaster["machine", rowindex].Value.ToString().Trim(),

            //    partnumber = this.dgv_ShowKpMaster["partnumber", rowindex].Value.ToString().Trim(),
                
            //    bomver = this.dgv_ShowKpMaster["bomver", rowindex].Value.ToString().Trim(),
            //    modelname = this.dgv_ShowKpMaster["modelname", rowindex].Value.ToString().Trim(),
            //    Userid = this.mFrm.gUserInfo.userId,
            //    pcbside = "T",
            //    reserve1 = this.dgv_ShowKpMaster["reserve1", rowindex].Value.ToString().Trim(),
            //    reserve2 = "0"
            //});
            this.dgv_ShowKpMaster["updateedit", rowindex].Value = true;
            this.dgv_ShowKpMaster["reserve2", rowindex].Value = "等待提交";
            this.mStrMasterIdTemp = this.dgv_ShowKpMaster["masterId", rowindex].Value.ToString().Trim();
            this.dgv_ShowKpMaster.Rows[rowindex].DefaultCellStyle.BackColor = Color.White;
        }
        private void dgv_ShowKpMaster_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    this.mStrTemp = this.dgv_ShowKpMaster[e.ColumnIndex, e.RowIndex].Value.ToString();
                }
            }
            catch
            {
                this.mStrTemp = string.Empty;
            }
        }
        private void dgv_ShowKpMaster_Leave(object sender, EventArgs e)
        {
            this.mStrTemp = string.Empty;
        }
        private void bt_select_Click(object sender, EventArgs e)
        {
            try
            {
                FrmBLL.publicfuntion.SelectDataGridViewRows(this.tb_partnumber.Text.Trim(), this.dgv_ShowKpMaster, 4);
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        private void dgv_ShowKPDetalt_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.mkpdetatltemp != this.dgv_ShowKPDetalt[e.ColumnIndex, e.RowIndex].Value.ToString().Trim())
                {
                    this.dgv_ShowKPDetalt.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(222, 238, 26);
                    this.dgv_ShowKPDetalt.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.FromArgb(232, 112, 108);

                    this.RecodeNewValue(masterIdrowindex);
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        private void dgv_ShowKPDetalt_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgv_ShowKPDetalt.Rows.Count < 2)
                return;
            this.mkpdetatltemp = this.dgv_ShowKPDetalt[e.ColumnIndex, e.RowIndex].Value.ToString().Trim();
        }
        private void dgv_ShowKPDetalt_Leave(object sender, EventArgs e)
        {
            this.mkpdetatltemp = string.Empty;
        }
        private void dgv_ShowKPDetalt_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBoxEx.Show("数据格式错误,或数据类型不正确","提示");
        }
        private void bt_submit_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBoxEx.Show("是否确认提交修改?");
                IList<IDictionary<string, object>> lskpdetalt = new List<IDictionary<string, object>>();
                IDictionary<string, object> Dic = null;
                for (int i = 0; i < this.dgv_ShowKPDetalt.Rows.Count - 1; i++)
                {
                    Dic = new Dictionary<string, object>();
                    //lskpdetalt.Add(new WebServices.ExcelToDb.SMT_KP_DETALT()
                    //{
                    //    KPDesc = this.dgv_ShowKPDetalt["kpdesc", i].Value.ToString().Trim(),
                    //    KPDistinct = int.Parse(this.dgv_ShowKPDetalt["priorityclass", i].Value.ToString()) > 1 ? false : true,
                    //    KPNumber = this.dgv_ShowKPDetalt["kpnumber", i].Value.ToString().Trim(),
                    //    Loction = this.dgv_ShowKPDetalt["loction", i].Value.ToString().Trim(),
                    //    loctionLen = this.dgv_ShowKPDetalt["loction", i].Value.ToString().Trim().Length, --未添加20150203
                    //    Priorityclass = int.Parse(this.dgv_ShowKPDetalt["priorityclass", i].Value.ToString()),
                    //    Stationno = this.dgv_ShowKPDetalt["stationno", i].Value.ToString().Trim(),
                    //    Replacegroup = this.dgv_ShowKPDetalt["replacegroup", i].Value.ToString().Trim(),
                    //    reserve = this.dgv_ShowKPDetalt["_reserve", i].Value.ToString().Trim(),
                    //    reserve1 = this.dgv_ShowKPDetalt["_reserve1", i].Value.ToString().Trim()
                    //});
                    Dic.Add("KP_DESC",this.dgv_ShowKPDetalt["kpdesc", i].Value.ToString().Trim());
                    Dic.Add("KPDISTINCT", int.Parse(this.dgv_ShowKPDetalt["priorityclass", i].Value.ToString()) > 1 ? false : true);
                    Dic.Add("KPNUMBER", this.dgv_ShowKPDetalt["kpnumber", i].Value.ToString().Trim());
                    Dic.Add("LOCTION", this.dgv_ShowKPDetalt["loction", i].Value.ToString().Trim());
                    Dic.Add("PRIORITYCLASS",int.Parse(this.dgv_ShowKPDetalt["priorityclass", i].Value.ToString()));
                    Dic.Add("STATIONNO", this.dgv_ShowKPDetalt["stationno", i].Value.ToString().Trim());
                    Dic.Add("REPLACEGROUP", this.dgv_ShowKPDetalt["replacegroup", i].Value.ToString().Trim());
                    Dic.Add("RESERVE1", this.dgv_ShowKPDetalt["_reserve1", i].Value.ToString().Trim());
                    Dic.Add("RESERVE", this.dgv_ShowKPDetalt["_reserve", i].Value.ToString().Trim());
                    Dic.Add("MASTERID", "NA");
                    lskpdetalt.Add(Dic);
                
                }
                RefWebService_BLL.refWebExcelToDb.Instance.InsertMaterTable(FrmBLL.ReleaseData.DictionaryToJson(kpmaster), FrmBLL.ReleaseData.ListDictionaryToJson(lskpdetalt));    

                FrmBLL.publicfuntion.InserSystemLog(mFrm.gUserInfo.userId, "EditKpMaster", "EditKpMaster", "KpMaster:" + kpmaster["MASTERID"]);
                EditKpMarster_Load(null, null);
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void bt_expkpmaster_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.RecDr == null)
                {
                    this.mFrm.ShowPrgMsg("请双击需要打印的料表编号", MainParent.MsgType.Warning);
                    return;
                }

                if (!File.Exists(string.Format("{0}Excel\\KpMaster.xlt", System.AppDomain.CurrentDomain.BaseDirectory)))
                {
                    FrmBLL.Ftp_MyFtp ftp = new FrmBLL.Ftp_MyFtp();
                    ftp.DownloadFile("KpMaster.xlt", System.AppDomain.CurrentDomain.BaseDirectory + "Excel", "KpMaster.xlt");
                }
                string FileNameTemp = string.Format("{0}Excel\\待审核_{1}_{2}_{3}_{4}.xls",
                    System.AppDomain.CurrentDomain.BaseDirectory,
                    this.RecDr.Cells["masterId"].Value.ToString(),
                    this.RecDr.Cells["partnumber"].Value.ToString(),
                    this.RecDr.Cells["machine"].Value.ToString(),
                    this.RecDr.Cells["pcbside"].Value.ToString());
                
                FrmBLL.ClsAllExcel excel = new FrmBLL.ClsAllExcel();
                this.mFrm.ShowPrgMsg("正在生成报表", MainParent.MsgType.Outgoing);
                excel.OpenFileName = string.Format("{0}Excel\\KpMaster.xlt", System.AppDomain.CurrentDomain.BaseDirectory);// FileNameTemp;
                excel.OpenExcelFile();//SMT料站表
                excel.setOneCellValue(1, 1, this.RecDr.Cells["reserve2"].Value.ToString() != "审核通过" ? "SMT料站表--待审核" : "SMT料站表");
                //excel.setOneCellValue(2, 7, this.RecDr.Cells["userId"].Value.ToString());//制作人
                excel.setOneCellValue(2, 2, this.RecDr.Cells["masterId"].Value.ToString());//料表编号
                excel.setOneCellValue(3, 2, this.RecDr.Cells["machine"].Value.ToString());//机器编号
                excel.setOneCellValue(4, 2, this.RecDr.Cells["partnumber"].Value.ToString());//产品料号
                excel.setOneCellValue(5, 2, this.RecDr.Cells["bomver"].Value.ToString());//BOM版本
                excel.setOneCellValue(5, 7, this.RecDr.Cells["reserve1"].Value.ToString());//SMT程式
                excel.setOneCellValue(4, 4, this.RecDr.Cells["modelname"].Value.ToString());//产品型号
                excel.setOneCellValue(5, 4, this.RecDr.Cells["pcbside"].Value.ToString());//PCB面

                for (int i = 0; i < this.dgv_ShowKPDetalt.Rows.Count - 1; i++)
                {
                    excel.setOneCellValue(8 + i, 1, this.dgv_ShowKPDetalt["stationno", i].Value.ToString());
                    excel.setOneCellValue(8 + i, 2, this.dgv_ShowKPDetalt["kpnumber", i].Value.ToString());
                    excel.setOneCellValue(8 + i, 3, this.dgv_ShowKPDetalt["kpdesc", i].Value.ToString());
                    excel.setOneCellValue(8 + i, 4, this.dgv_ShowKPDetalt["kpdistinct", i].Value.ToString());
                    excel.setOneCellValue(8 + i, 5, this.dgv_ShowKPDetalt["replacegroup", i].Value.ToString());
                    excel.setOneCellValue(8 + i, 6, this.dgv_ShowKPDetalt["priorityclass", i].Value.ToString());
                    excel.setOneCellValue(8 + i, 7, this.dgv_ShowKPDetalt["loction", i].Value.ToString());
                    excel.setOneCellValue(8 + i, 8, this.dgv_ShowKPDetalt["_reserve", i].Value.ToString());
                }
                excel.CellsDrawFrame(8, 1, this.dgv_ShowKPDetalt.Rows.Count - 2 + 8, 8, true,
                    true, true, true, true, true , false, false ,
                    Excel.XlLineStyle.xlDashDot, Excel.XlBorderWeight.xlHairline,
                    Excel.XlColorIndex.xlColorIndexNone);
                    excel.SaveFileName = FileNameTemp;
                    bool isView = false;
                    if (MessageBoxEx.Show("是否预览文件?\n是 请选择[Yes] 否则请选择[NO]", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        isView = true;
                    excel.SaveAsExcel(isView);
                    
                    excel.CloseExcelApplication();
                    this.mFrm.ShowPrgMsg("报表生成完成", MainParent.MsgType.Outgoing);
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        string sMaterid = "";
        string sMasterStatus = "";
        string sPartNo = "";
        string sMachine = "";

        private void dgv_ShowKpMaster_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                sMaterid = dgv_ShowKpMaster[1, e.RowIndex].Value.ToString();
                sMasterStatus = dgv_ShowKpMaster[10, e.RowIndex].Value.ToString();

                sPartNo = dgv_ShowKpMaster[4, e.RowIndex].Value.ToString();
                sMachine = dgv_ShowKpMaster[2, e.RowIndex].Value.ToString();

            }
        }

        private void bt_deleteMaster_Click(object sender, EventArgs e)
        {
            if (sMasterStatus != "审核通过")
            {
                if (MessageBox.Show("确定删除料站表编号: " + sMaterid + " 吗?", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    RefWebService_BLL.refWebSmtKpMaster.Instance.DeleteBomKpMaster(sMaterid);
                    this.ShowKpMaster(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebSmtKpMaster.Instance.GetAllKpMaster()));
                    mFrm.ShowPrgMsg("删除成功", MainParent.MsgType.Incoming);

                    FrmBLL.publicfuntion.InserSystemLog(mFrm.gUserInfo.userId, "删除料站表", "删除", "料表编号:" + sMaterid + "机器编号:" + sMachine + " 成品料号:" + sPartNo);
                }

            }
            else
            {
                mFrm.ShowPrgMsg("不能删除审核通过的料站表", MainParent.MsgType.Error);
                return;
            }

        }

        private void EditKpMarster_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                RefWebService_BLL.refwebtEditing.Instance.DeletetEditingByUserIdAndPrj(this.mFrm.gUserInfo.userId, this.strFunName);
            }
            catch
            {

            }
        }
    }
}
