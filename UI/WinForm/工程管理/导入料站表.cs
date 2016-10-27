using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
//using Entity;
using FrmBLL;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class ExcelToDb :Office2007Form
    {
        public ExcelToDb(MainParent frm)
        {
            InitializeComponent();
            mainfrm = frm;
        }
        #region 成员变量
        /// <summary>
        /// 需要调用父窗体的全局消息显示
        /// </summary>
        private  MainParent mainfrm;
        /// <summary>
        /// 保存Excel文件路径
        /// </summary>
        private string mExcleFile = string.Empty;

        /// <summary>
        /// 保存Excel文件中所有的Sheet的名称
        /// (KEY值保存用来在UI上显示的Sheet名称值,VALUE值用来保存原始的Sheet名称用于数据库语句操作)
        /// </summary>
        private Dictionary<string, string> dicnames= new Dictionary<string, string>();

        private readonly string[] HadTitolListName = new string[] { "产品料号", "产品描述", "机器编号", "BOM版本", "料站总数" };
        #endregion
        #region 控件事件
        private void mbt_OpenExcel_Click(object sender, EventArgs e)
        {
            try
            {
                dicnames = new Dictionary<string, string>();
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "选择料站表";
                ofd.Filter = "(*.xls Excel 2003)|*.xls";
                //ofd.InitialDirectory = "c:\\";
                DialogResult dlr = ofd.ShowDialog();
                if (dlr == DialogResult.Yes || dlr == DialogResult.OK)
                {
                    this.mExcleFile = ofd.FileName;
                    //在打开Excel文档时就去判断文档内容是否符合规则要求
                    List<string> lsnames =  ClsReadExcel.GetTableNames( ClsReadExcel.FileExists(this.mExcleFile));
                    DataTable mdatatable = new DataTable();
                    string side = string.Empty;
                    foreach (string item in lsnames)
                    {
                        side = "T";// item.Split('-')[1].Substring(0, 1);
                        if (item.Split(' ').Length < 2)
                        {
                            throw new Exception("料站表的命名规则不符,请重新修正..");
                        }
                        if (string.IsNullOrEmpty(side))
                            throw new Exception("文档命名不符合规则(\"产品名称-PCB版面+F\")");
                        //if ("T" != side && "B" != side)
                            //throw new Exception("文档命名不符合规则(\"产品名称-PCB版面+F\")");

                        //添加Feeder类型
                        string sql = string.Format("select 产品料号,产品描述,机器编号,BOM版本,料站总数,count(*) as 料站数 from [{0}] group by 产品料号,产品描述,机器编号,BOM版本,料站总数 having 机器编号 is not null",
                            item);
                        //string sql = string.Format("select 产品料号,产品描述,机器编号,BOM版本,料站总数,count(*) as 料站数 from [{0}] group by 产品料号,产品描述,机器编号,BOM版本,料站总数 having 机器编号 is not null",
                        //    item);
                        mdatatable =  ClsReadExcel.getTableForSql( ClsReadExcel.FileExists(this.mExcleFile), sql);

                        this.dicnames.Add(item.Replace('\'', ' ').Replace('$', ' ').Trim(), item);

                        if (mdatatable.Rows.Count > 2)
                        {
                            if (this.ShowMsg("一条生产线的参数设置过多\n默认只支持左边、右边,请重新设置.."))
                            {
                                throw new Exception("一条生产线的参数设置过多,默认只支持左边、右边,请检查料表设置..");
                            }
                        }
                    }

                    //this.dgv_had.DataSource = mdatatable;
                   // this.dgv_had.Columns["料站数"].Visible = false;
                    this.ip_linenamelist.Items.Clear();
                    foreach (string item in this.dicnames.Keys)
                    {
                        this.ip_linenamelist.Items.Add(new DevComponents.DotNetBar.CheckBoxItem(item, item));
                        this.ip_linenamelist.Refresh();
                    }
                    //将料站表上传FTP
                    try
                    {
                        FrmBLL.Ftp_MyFtp ftp = new FrmBLL.Ftp_MyFtp();
                        ftp.PutImage(ofd.FileName);
                    }
                    catch(Exception ex)
                    {
                        this.mainfrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                this.mainfrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //打印预览
             ClsReadExcel.ExcelPreview("d:\\QC-120607132719_20120625.xml");
        }
        private void ip_linenamelist_ItemClick(object sender, EventArgs e)
        {
            try
            {
                List<string> lsValue = new List<string>();
                if (this.ip_linenamelist.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < this.ip_linenamelist.Items.Count; i++)
                    {
                        if ((this.ip_linenamelist.Items[i] as DevComponents.DotNetBar.CheckBoxItem).Checked)
                        {
                            lsValue.Add(this.dicnames[(this.ip_linenamelist.Items[i] as DevComponents.DotNetBar.CheckBoxItem).Text]);
                        }
                    }
                }

                this.dgv_had.DataSource = ClsReadExcel.getTableHad(ClsReadExcel.FileExists(this.mExcleFile), lsValue.ToArray());

                this.dgv_dta.DataSource = ClsReadExcel.getTableDta(ClsReadExcel.FileExists(this.mExcleFile), lsValue.ToArray());
                if (this.dgv_had.Rows.Count > 0)
                    this.dgv_had.Columns["料站数"].Visible = false;
            }
            catch (Exception ex)
            {
                this.mainfrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Warning);
            }
        }
        private void dgv_had_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                this.dgv_had.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 20);
                    this.dgv_had[e.ColumnIndex, e.RowIndex].ToolTipText =
                        string.Format("当前累计:[{0}]笔数据\n鼠标双击每一行可以跟踪每个工单的生产状况", this.dgv_had.Rows.Count);
            }
        }
        private void dgv_had_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                this.dgv_had.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }
        private void dgv_dta_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                this.dgv_dta.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 20);

                this.dgv_dta[e.ColumnIndex, e.RowIndex].ToolTipText =
                        string.Format("当前累计:[{0}]笔数据\n鼠标双击每一行可以跟踪每个工单的生产状况", this.dgv_dta.Rows.Count);

            }
        }
        private void dgv_dta_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                this.dgv_dta.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }
        IAsyncResult iasyncresult;
        private delegate void delegateExportDb(DataTable dgvDtTemp, DataGridView dgvhad);
        delegateExportDb eventexportdb;
        private void ExportDb(DataTable dgvDtTemp, DataGridView dgvhad)
        {
            try
            {
                this.mainfrm.ShowPrgMsg("正在导入数据", MainParent.MsgType.Outgoing);
                #region 20150811导入料站表时,删除整条线体料表
                DataTable dtMachineLine = FrmBLL.ReleaseData.arrByteToDataTable(refWebtMachineInfo.Instance.GetAllMachineInfo());
                string _Machine = null;
                string _PartNumber = string.Empty;
                string _PcbaSide = string.Empty;
                for (int i = 0; i < dgvhad.Rows.Count; i++)
                {
                    _PartNumber = dgvhad["产品料号", i].Value.ToString();
                    _PcbaSide = dgvhad["PCB面", i].Value.ToString();
                    if (i == dgvhad.Rows.Count - 1)
                        _Machine += "'" + dgvhad["机器编号", i].Value.ToString() + "'";
                    else
                        _Machine += "'" + dgvhad["机器编号", i].Value.ToString() + "',";
                }
                DataTable dtLine = ClsReadExcel.getNewTable(dtMachineLine, string.Format("MACHINEID IN ({0})", _Machine));
                DataView dataView = dtLine.DefaultView;
                DataTable dataTableDistinct = dataView.ToTable(true, "LINEID");
                string _SmtLine = null;
                for (int x = 0; x < dataTableDistinct.Rows.Count; x++)
                {
                    if (x == dataTableDistinct.Rows.Count - 1)
                        _SmtLine += "'" + dataTableDistinct.Rows[x]["LINEID"].ToString() + "'";
                    else
                        _SmtLine += "'" + dataTableDistinct.Rows[x]["LINEID"].ToString() + "',";
                }
                DataTable _SmtMachine = ClsReadExcel.getNewTable(dtMachineLine, string.Format("LINEID IN ({0})", _SmtLine));

                List<string> LsMachine = new List<string>();
                for (int y = 0; y < _SmtMachine.Rows.Count; y++)
                {
                    LsMachine.Add(_SmtMachine.Rows[y]["MACHINEID"].ToString());
                }

                string _StrErr = refWebSmtKpMaster.Instance.DeleteSmtKpMaster(LsMachine.ToArray(), _PartNumber, _PcbaSide);
                if (_StrErr != "OK")
                    throw new Exception("删除料站表头信息失败:" + _StrErr);
                #endregion

                for (int i = 0; i < dgvhad.Rows.Count; i++)
                {
                    DataTable dtTemp = ClsReadExcel.getNewTable(dgvDtTemp,
                           string.Format("产品料号='{0}' and 产品描述='{1}' and 机器编号='{2}' and BOM版本='{3}'",
                           dgvhad["产品料号", i].Value.ToString(), dgvhad["产品描述", i].Value.ToString(),
                           dgvhad["机器编号", i].Value.ToString(), dgvhad["BOM版本", i].Value.ToString()));

                    List<IDictionary<string, object>> lskpdetalt = new List<IDictionary<string, object>>();
                    Dictionary<string, object> kpdetalt = null;
                    foreach (DataRow dr in dtTemp.Rows)
                    {
                         kpdetalt = new Dictionary<string, object>();
                         kpdetalt.Add("KPDESC", dr["品名与规格"].ToString());
                         kpdetalt.Add("KPDISTINCT",int.Parse(!string.IsNullOrEmpty(dr["优先级"].ToString()) ? dr["优先级"].ToString() : "0") > 1 ? "0" : "1");
                         kpdetalt.Add("KPNUMBER",dr["组件料号"].ToString());
                         kpdetalt.Add("LOCTION",dr["组件位置"].ToString());
                         kpdetalt.Add("PRIORITYCLASS", int.Parse(!string.IsNullOrEmpty(dr["优先级"].ToString()) ? dr["优先级"].ToString() : "0"));
                         kpdetalt.Add("STATIONNO",dr["料站"].ToString());

                         if (!string.IsNullOrEmpty(dr["替代组"].ToString()))
                         kpdetalt.Add("REPLACEGROUP", dr["替代组"].ToString());

                         if (!string.IsNullOrEmpty(dr["组件位置"].ToString().Trim()))
                         kpdetalt.Add("RESERVE1",GetKpUnit(dr["组件位置"].ToString().Trim()));

                         if (!string.IsNullOrEmpty(dr["FEEDER类型"].ToString()))
                         kpdetalt.Add("RESERVE", dr["FEEDER类型"].ToString());

                        lskpdetalt.Add(kpdetalt);
                        //lskpdetalt.Add(new WebServices.ExcelToDb.SMT_KP_DETALT()
                        //{
                        //    KPDesc = dr["品名与规格"].ToString(),
                        //    KPDistinct = int.Parse(!string.IsNullOrEmpty(dr["优先级"].ToString()) ? dr["优先级"].ToString() : "0") > 1 ? false : true,
                        //    KPNumber = dr["组件料号"].ToString(),
                        //    Loction = dr["组件位置"].ToString(),
                        //    loctionLen = dr["组件位置"].ToString().Length,---未增加此行 20150203 michael
                        //    //Masterid = _masterId,
                        //    Priorityclass = int.Parse(!string.IsNullOrEmpty(dr["优先级"].ToString()) ? dr["优先级"].ToString() : "0"),
                        //    Stationno = dr["料站"].ToString(),
                        //    Replacegroup = dr["替代组"].ToString(),
                        //    reserve1 = GetKpUnit(dr["组件位置"].ToString().Trim()),
                        //    reserve = dr["FEEDER类型"].ToString()  //添加Feeder类型
                        //});
                    }

                    Dictionary<string, object> SmtKpMater = new Dictionary<string, object>();
                     SmtKpMater.Add("BOMVER", dgvhad["BOM版本", i].Value.ToString());
                     SmtKpMater.Add("LINEID",dgvhad["机器编号", i].Value.ToString());
                     SmtKpMater.Add("RESERVE1",dgvhad["SMT程式",i].Value.ToString());
                     SmtKpMater.Add("MODELNAME", dgvhad["产品描述", i].Value.ToString());
                     SmtKpMater.Add("PARTNUMBER",dgvhad["产品料号", i].Value.ToString());
                     SmtKpMater.Add("PCBSIDE",dgvhad["PCB面", i].Value.ToString());
                     SmtKpMater.Add("USERID",this.mainfrm.gUserInfo.userId);                    
                    //refWebExcelToDb.Instance.InsertMaterTable(new WebServices.ExcelToDb.SMT_KP_MASTER()
                    //{
                    //    bomver = dgvhad["BOM版本", i].Value.ToString(),
                    //    Lineid = dgvhad["机器编号", i].Value.ToString(),
                    //    reserve1 = dgvhad["SMT程式",i].Value.ToString(),
                    //    // masterId = _masterId,
                    //    modelname = dgvhad["产品描述", i].Value.ToString(),
                    //    partnumber = dgvhad["产品料号", i].Value.ToString(),
                    //    pcbside = dgvhad["PCB面", i].Value.ToString(),
                    //    Userid = this.mainfrm.gUserInfo.userId
                    //},  lskpdetalt);
            
                     refWebExcelToDb.Instance.InsertMaterTable(ReleaseData.DictionaryToJson(SmtKpMater), ReleaseData.ListDictionaryToJson(lskpdetalt));
                }

             
                FrmBLL.publicfuntion.InserSystemLog(mainfrm.gUserInfo.userId, "导入料站表", "新增", "导入料站表: " + "产品料号:" + dgv_had["产品料号", 0].Value.ToString() + ";机器编号: " + dgv_had["机器编号", 0].Value.ToString());

                this.mainfrm.ShowPrgMsg("数据导入成功..");
                MessageBoxEx.Show("数据导入成功", "提示");
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("FOREIGN KEY") != -1)
                    this.mainfrm.ShowPrgMsg("料站表中的[机器编号]不存在于系统,请先添加机器信息.", MainParent.MsgType.Error);
                else
                    this.mainfrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
                MessageBoxEx.Show("数据导入失败,错误信息如下:\n"+ex.Message, "提示");
            }
        }
        private string GetKpUnit(string StrInput)
        {
            int tl=0;
            if (StrInput.Substring(0, 1) == ",")
                StrInput = StrInput.Substring(1, StrInput.Length-1);
            if (StrInput.Substring(StrInput.Length - 1, 1) == ",")
                StrInput = StrInput.Substring(0, StrInput.Length - 1);
            tl = StrInput.Split(',').Length;
            return tl < 1 ? "1" : tl.ToString();
        }
        private void bt_excelTodb_Click(object sender, EventArgs e)
        {
            try
            {
                if (iasyncresult != null && !iasyncresult.IsCompleted)
                {
                    this.mainfrm.ShowPrgMsg("数据正在导入,请稍后..");
                    return;
                }
                //datagridview有数据存在
                if (this.dgv_dta.Rows.Count < 1 || this.dgv_had.Rows.Count < 1)
                    throw new Exception("不存在可以导入系统的数据或还没有打开料站表,请检查..");
                if (MessageBoxEx.Show("是否确认将料站表信息导入系统?\n\n确认请按[Yes],取消请按[No]", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.Yes)
                    return;
                eventexportdb = new delegateExportDb(ExportDb);
               iasyncresult =  eventexportdb.BeginInvoke(this.dgv_dta.DataSource as DataTable, this.dgv_had, null, null);

                #region 移到多线程处理
                //DataTable dgvDtTemp = this.dgv_dta.DataSource as DataTable;
                //for (int i = 0; i < this.dgv_had.Rows.Count; i++)
                //{
                //    DataTable dtTemp =  ClsReadExcel.getNewTable(dgvDtTemp,
                //           string.Format("产品料号='{0}' and 产品描述='{1}' and 机器编号='{2}' and BOM版本='{3}'",
                //           dgv_had["产品料号", i].Value.ToString(), dgv_had["产品描述", i].Value.ToString(),
                //           dgv_had["机器编号", i].Value.ToString(), dgv_had["BOM版本", i].Value.ToString()));

                //    List< RefWebService_BLL.refWeb_ExcelToDb.SMT_KP_DETALT> lskpdetalt = new List< RefWebService_BLL.refWeb_ExcelToDb.SMT_KP_DETALT>();
                //    foreach (DataRow  dr in dtTemp.Rows)
                //    {
                //        lskpdetalt.Add(new  RefWebService_BLL.refWeb_ExcelToDb.SMT_KP_DETALT()
                //        {
                //            KPDesc = dr["品名与规格"].ToString(),
                //            KPDistinct = int.Parse(dr["优先级"].ToString()) > 1 ? false : true,
                //            KPNumber = dr["组件料号"].ToString(),
                //            Loction = dr["组件位置"].ToString(),
                //            //Masterid = _masterId,
                //            Priorityclass = int.Parse(dr["优先级"].ToString()),
                //            Stationno = dr["料站"].ToString(),
                //            Replacegroup = dr["替代组"].ToString()                       
                //        });                      
                //    }
                //     refWebExcelToDb.Instance.InsertMaterTable(new  RefWebService_BLL.refWeb_ExcelToDb.SMT_KP_MASTER() {
                //        bomver = dgv_had["BOM版本", i].Value.ToString(),
                //        Lineid = dgv_had["机器编号", i].Value.ToString(),
                //        // masterId = _masterId,
                //        modelname = dgv_had["产品描述", i].Value.ToString(),
                //        partnumber = dgv_had["产品料号", i].Value.ToString(),
                //        pcbside = dgv_had["PCB面", i].Value.ToString(),
                //        Userid = this.mainfrm.gUserInfo.userId
                //    }, lskpdetalt.ToArray());
                //}

                // refWebRecodeSystemLog.Instance.InsertSystemLog(new  RefWebService_BLL.refWeb_RecodeSystemLog.T_SYSTEM_LOG()
                //{
                //    userId = mainfrm.gUserInfo.userId,
                //    prg_name = "导入料站表",
                //    action_type = "新增",
                //    action_desc = "导入料站表: " + "产品料号:" + dgv_had["产品料号", 0].Value.ToString() + ";机器编号: " + dgv_had["机器编号", 0].Value.ToString()
                //});

                //this.mainfrm.ShowPrgMsg("数据导入成功..", MainParent.MsgType.Incoming);
                //MessageBoxEx.Show("数据导入成功", "提示");
                #endregion
            }
            catch (Exception ex)
            {
                this.mainfrm.ShowPrgMsg(ex.Message);
                MessageBoxEx.Show("数据导入失败,错误信息如下:\n"+ex.Message, "提示");
            }
        }
        #endregion

        #region 成员方法
        private bool ShowMsg(string msg)
        {
            if (MessageBoxEx.Show(msg, "提示",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Asterisk,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                return true;
            else
                return false;

        }
        #endregion

        private void ExcelToDb_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.mainfrm.gUserInfo.rolecaption == "系统开发员")
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


    }
}
