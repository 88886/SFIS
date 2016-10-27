using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using FrmBLL;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class FrmMaterialToPD : Office2007Form // Form
    {
        public FrmMaterialToPD(MainParent Msg)
        {
            InitializeComponent();
            _sMsg = Msg;
        }
        MainParent _sMsg;

        //RefWebService_BLL.refWeb_tPartStorehousehad.tPartStorehousehad TrSnInfo = new RefWebService_BLL.refWeb_tPartStorehousehad.tPartStorehousehad();
        private delegate void delegateloadwobominfo(string woid, bool usesapdata);
        private delegateloadwobominfo eventloadbominfo;
        private IAsyncResult iasyncresult;


        private void FrmMaterialToPD_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this._sMsg.gUserInfo.rolecaption == "系统开发员")
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
            imbt_Out.Enabled = false;

        }
        private void 原料发料_Load(object sender, EventArgs e)
        {

        }


        private void imbt_Out_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdtolinehouse.Checked)
                {

                    if (MessageBox.Show("确定要将此盘料: " + TrSnNo + "\r\n 发放到线边仓吗?", "物料发放提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        //增加判断该料是否是该工单的料
                        if (this.chkWo.Checked)
                        {
                            if (string.IsNullOrEmpty(this.tbwoId.Text))
                            {
                                this._sMsg.ShowPrgMsg("请先填写工单号..", MainParent.MsgType.Warning);
                                return;
                            }
                            if (this.SapWoBomData.Select(string.Format("MATNR2='{0}'", this.tb_pn.Text.Trim())).Length < 1)
                            {
                                this._sMsg.ShowPrgMsg(string.Format("物料{0}不存在于SAP工单备料表中,请检查..\n发料失败..", this.tb_pn.Text),
                                    MainParent.MsgType.Warning);
                                return;
                            }
                        }

                        RefWebService_BLL.refWebtPartStorehousehad.Instance.UpdateTrSnStatus("1", _sMsg.gUserInfo.userId, TrSnNo);

                        refWebtPartStorehousehad.Instance.UpdateTrSnWoid(this.tbwoId.Text,TrSnNo);

                        RefWebService_BLL.refWebtPartStorehousehad.Instance.InsertPartStorehousehad(new WebServices.tPartStorehousehad.tPartStorehousehad1()
                        {
                            Tr_Sn = TrSnNo,
                            KpNumber = tb_pn.Text.Trim(),
                            VenderCode = tb_vc.Text.Trim(),
                            DateCode = tb_dc.Text.Trim(),
                            LotId = tb_lc.Text.Trim(),
                            QTY = 0,
                            status = 6,
                            UserId = _sMsg.gUserInfo.userId,
                            LocId = tb_location.Text.Trim(),
                            OUTQTY = Convert.ToInt32(tb_qty.Text.Trim()),
                            FLAG = "OUT"
                        });

                        tb_trsn.SelectAll();
                        tb_trsn.Focus();
                        _sMsg.ShowPrgMsg("已发放至线边仓", MainParent.MsgType.Incoming);
                        tb_trsn.Text = "";
                        imbt_Out.Enabled = false;

                    }
                    else
                    {
                        _sMsg.ShowPrgMsg("物料未发放.", MainParent.MsgType.Incoming);
                        tb_trsn.Focus();
                        tb_trsn.SelectAll();
                    }
                }
                if (rdtowh.Checked)
                {
                    if (MessageBox.Show("确定要将此盘料: " + TrSnNo + "\r\n 收到仓库吗?\n\n当前的入库的库位为[" + this.tb_location.Text + "]   是否确认?", "物料收料提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        RefWebService_BLL.refWebtPartStorehousehad.Instance.UpdateTrSnStatus("6", _sMsg.gUserInfo.userId, TrSnNo);

                        RefWebService_BLL.refWebtPartStorehousehad.Instance.InsertPartStorehousehad(new WebServices.tPartStorehousehad.tPartStorehousehad1()
                        {
                            Tr_Sn = TrSnNo,
                            KpNumber = tb_pn.Text.Trim(),
                            VenderCode = tb_vc.Text.Trim(),
                            DateCode = tb_dc.Text.Trim(),
                            LotId = tb_lc.Text.Trim(),
                            QTY = Convert.ToInt32(tb_qty.Text.Trim()),
                            status = 6,
                            UserId = _sMsg.gUserInfo.userId,
                            LocId = tb_location.Text.Trim(),
                            OUTQTY = 0,
                            FLAG = "IN"
                        });

                        tb_trsn.SelectAll();
                        tb_trsn.Focus();
                        _sMsg.ShowPrgMsg("已将物料收到仓库", MainParent.MsgType.Incoming);
                        tb_trsn.Text = "";
                        imbt_Out.Enabled = false;

                    }
                    else
                    {
                        _sMsg.ShowPrgMsg("物料未收料.", MainParent.MsgType.Incoming);
                        tb_trsn.Focus();
                        tb_trsn.SelectAll();
                    }

                }
                imbt_Out.Enabled = false;
                rdtolinehouse.Enabled = true;
                rdtowh.Enabled = true;
                tb_trsn.Focus();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message.Substring(0, ex.Message.Length < 50 ? ex.Message.Length : 50), "错误提示");
            }
            finally
            {
                imbt_Out.Enabled = false;
            }

        }

        string TrSnNo = "";

        private void tb_trsn_KeyDown(object sender, KeyEventArgs e)
        {

            if ((!string.IsNullOrEmpty(tb_trsn.Text)) & e.KeyCode == Keys.Enter)
            {

                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.QueryTrSn(tb_trsn.Text.Trim()));
                if (dt.Rows.Count > 0)
                {
                    tb_pn.Text = dt.Rows[0]["kpnumber"].ToString();
                    tb_vc.Text = dt.Rows[0]["vendercode"].ToString();
                    tb_dc.Text = dt.Rows[0]["datecode"].ToString();
                    tb_lc.Text = dt.Rows[0]["lotid"].ToString();
                    tb_qty.Text = dt.Rows[0]["qty"].ToString();
                    tb_location.Text = dt.Rows[0]["locid"].ToString();

                    string status = dt.Rows[0]["sstatus"].ToString();
                    if (rdtolinehouse.Checked)
                    {
                        if (status == "1")
                        {
                            _sMsg.ShowPrgMsg("该唯一条码已经发放到线边仓", MainParent.MsgType.Warning);
                            tb_trsn.SelectAll();
                            return;
                        }
                        if (status == "2")
                        {
                            _sMsg.ShowPrgMsg("该唯一条码已经发放到生产线", MainParent.MsgType.Warning);
                            tb_trsn.SelectAll();
                            return;
                        }
                        if (status == "3")
                        {
                            _sMsg.ShowPrgMsg("该唯一条码已经使用完毕", MainParent.MsgType.Warning);
                            tb_trsn.SelectAll();
                            return;
                        }
                        if (status == "4")
                        {
                            _sMsg.ShowPrgMsg("该唯一条码已经下线退库分解", MainParent.MsgType.Warning);
                            tb_trsn.SelectAll();
                            return;
                        }

                        if (!publicfuntion.Check_MaterialScrap(tb_pn.Text, tb_vc.Text, tb_dc.Text, tb_lc.Text))
                        {
                            _sMsg.ShowPrgMsg("该物料为清仓物料,禁止使用...", MainParent.MsgType.Warning);
                            tb_trsn.SelectAll();
                            return;
                        }

                        if (status == "0" || status == "6")
                        {
                            imbt_Out.Enabled = true;
                            TrSnNo = tb_trsn.Text.Trim();
                            ShowKpnumberMoreThanDays(tb_pn.Text.Trim());
                        }




                    }

                    if (rdtowh.Checked)
                    {
                        if (status == "0" || status == "6")
                        {
                            _sMsg.ShowPrgMsg("该唯一条码已在仓库,不需要重复接收...", MainParent.MsgType.Warning);
                            tb_trsn.SelectAll();
                            return;

                        }
                        if (status == "2")
                        {
                            _sMsg.ShowPrgMsg("该唯一条码已经发放到生产线,不能做收料..", MainParent.MsgType.Warning);
                            tb_trsn.SelectAll();
                            return;
                        }
                        if (status == "3")
                        {
                            _sMsg.ShowPrgMsg("该唯一条码已经使用完毕,不能做收料..", MainParent.MsgType.Warning);
                            tb_trsn.SelectAll();
                            return;
                        }
                        if (status == "4")
                        {
                            _sMsg.ShowPrgMsg("该唯一条码已经下线退库分解,不能做收料..", MainParent.MsgType.Warning);
                            tb_trsn.SelectAll();
                            return;
                        }

                        if (status == "1" || status == "5")
                        {
                            imbt_Out.Enabled = true;
                            TrSnNo = tb_trsn.Text.Trim();
                        }

                    }

                    tb_trsn.SelectAll();
                    imbt_Out.Focus();
                    rdtolinehouse.Enabled = false;
                    rdtowh.Enabled = false;

                }
                else
                {
                    _sMsg.ShowPrgMsg("唯一条码输入错误,请确认........", MainParent.MsgType.Error);
                    imbt_Out.Enabled = false;
                    tb_trsn.SelectAll();

                }
            }
        }

        private void rdtowh_Click(object sender, EventArgs e)
        {
            if (rdtowh.Checked)
            {
                this.label8.Enabled = false;
                this.tbwoId.Enabled = false;
                this.chkWo.Enabled = false;
                linkLabel1.Text = "线边仓-------------->仓库";
                imbt_Out.Text = "收料";
                tb_location.Enabled = true;
                tb_location.ReadOnly = false;
                tb_trsn.Focus();
                tb_trsn.SelectAll();
            }
        }

        private void rdtolinehouse_Click(object sender, EventArgs e)
        {
            if (rdtolinehouse.Checked)
            {
                this.label8.Enabled = true;
                this.tbwoId.Enabled = true;
                this.chkWo.Enabled = true;
                linkLabel1.Text = "仓库-------------->线边仓";
                imbt_Out.Text = "发料";
                tb_location.Enabled = false;
                tb_location.ReadOnly = true;
                tb_trsn.Focus();
                tb_trsn.SelectAll();
            }

        }

        private void ChkQryPn_Click(object sender, EventArgs e)
        {
            if (ChkQryPn.Checked)
            {
                tb_pn.ReadOnly = false;
                tb_pn.Enabled = true;
            }
            else
            {
                tb_pn.ReadOnly = true;
                tb_pn.Enabled = false;
            }
        }

        private void ShowKpnumberMoreThanDays(string KpNumber)
        {
            dgvPartStorehousehad.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.QueryKpnumberMoreThanDays(KpNumber));
            if (rdtolinehouse.Checked)
            {
                bool TrsnOk = false;
                for (int x = 0; x < dgvPartStorehousehad.RowCount; x++)
                {
                    if (dgvPartStorehousehad.Rows[x].Cells[0].Value.ToString() == tb_trsn.Text.Trim())
                    {
                        TrsnOk = true;
                    }
                }

                if (!TrsnOk)
                {
                    if (MessageBox.Show("此物料不是最早进入仓库物料,请确认是否要发料", "材料先进先出提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                    {
                        tb_trsn.Focus();
                        tb_trsn.SelectAll();
                    }
                }
            }

        }

        private void tb_pn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_pn.Text) && (e.KeyCode == Keys.Enter))
            {
                ShowKpnumberMoreThanDays(tb_pn.Text.Trim());
            }
        }

        private void dgvPartStorehousehad_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvPartStorehousehad.RowHeadersDefaultCellStyle.ForeColor))
            {
                int linen = 0;
                linen = e.RowIndex + 1;
                string line = linen.ToString();
                e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X, e.RowBounds.Location.Y + 5);
                SolidBrush B = new SolidBrush(Color.Red);
            }
        }

        private void dgvPartStorehousehad_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                this.dgvPartStorehousehad.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 20);

                this.dgvPartStorehousehad[e.ColumnIndex, e.RowIndex].ToolTipText =
                    string.Format("当前累计: [{0}]  笔数据 ", this.dgvPartStorehousehad.Rows.Count);
            }
        }

        private void dgvPartStorehousehad_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                this.dgvPartStorehousehad.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }

        private DataTable SapWoBomData = new DataTable("sapwobomdata");
        private void tbwoId_Validating(object sender, CancelEventArgs e)
        {
            if (this.chkWo.Checked)
            {
                if (string.IsNullOrEmpty(this.tbwoId.Text))
                {
                    this._sMsg.ShowPrgMsg("开启了工单物料检查功能,工单号不能为空", MainParent.MsgType.Warning);
                    return;
                }
                this._sMsg.ShowPrgMsg("正在连接SAP系统,获取备料表信息..", MainParent.MsgType.Warning);
                SapWoBomData = FrmBLL.ReleaseData.arrByteToDataTable(refWebSapConnector.Instance.Get_Z_RFC_ZMM011(this.tbwoId.Text.Trim()));
                this._sMsg.ShowPrgMsg("SAP备料信息获取完成..", MainParent.MsgType.Warning);
            }
        }
       

    }
}
