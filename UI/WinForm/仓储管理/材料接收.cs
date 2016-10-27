using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class FrmReceiveMaterials : Office2007Form// Form
    {
        public FrmReceiveMaterials(MainParent frm)
        {
            InitializeComponent();
            this.mFrm = frm;
        }
        MainParent mFrm;
        private delegate void delegateloadunreceive();
        delegateloadunreceive dlur;

        private void bt_query_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.tb_po.Text.Trim()))
                    return;

                this.bt_query.Enabled = false;
                DataSet ds = FrmBLL.ReleaseData.arrByteToDataSet(
                    RefWebService_BLL.refWebSapConnector.Instance.Get_Z_RFC_PO(this.tb_po.Text.Trim()));

                dgv_materialshad.DataSource = ds.Tables[0];
                //this.dgv_materialshad.DataMember = "Header";
                dgv_materialsdta.DataSource = ds.Tables[1];
                //this.dgv_materialsdta.DataMember = "Z_RFC_PO";
                this.bt_query.Enabled = true;

                RefData();
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Normal);
                this.bt_query.Enabled = true;
            }
        }
        /// <summary>
        /// 处理重复的行
        /// </summary>
        /// <param name="_dt"></param>
        /// <returns></returns>
        private DataTable GetDistinctData(DataTable _dt)
        {
            DataTable dt = new DataTable();
            dt = _dt.Clone();
            DataRow mdr;
            DataTable dtTemp = _dt.DefaultView.ToTable(true, "MATNR");
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                string partnumber = dtTemp.Rows[i]["MATNR"].ToString();
                DataTable table = FrmBLL.publicfuntion.getNewTable(_dt, "MATNR='" + partnumber + "'");
                if (table != null && table.Rows.Count == 1)
                {
                    mdr = dt.NewRow();
                    mdr.ItemArray = table.Rows[0].ItemArray;
                    dt.Rows.Add(mdr);
                    continue;
                }
                if (table != null && table.Rows.Count > 1)
                {
                    int sumQty = 0;
                    foreach (DataRow dr in table.Rows)
                    {
                        sumQty += Convert.ToInt32(dr["ERFMG"].ToString().Split('.')[0]);
                    }
                    mdr = dt.NewRow();
                    mdr.ItemArray = table.Rows[0].ItemArray;
                    dt.Rows.Add(mdr);
                    dt.Rows[dt.Rows.Count - 1]["ERFMG"] = sumQty.ToString() + ".000";
                }
            }
            return dt;
        }
        private void tb_year_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                bt_query_Click(null, null);
            }
        }
        private void dgv_materialsdta_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (dgv_materialsdta.Rows[dgv_materialsdta.CurrentCell.RowIndex].DefaultCellStyle.BackColor == Color.LightGray)
                    return;

                string sPart = dgv_materialsdta["MATNR", dgv_materialsdta.CurrentCell.RowIndex].Value.ToString();
                DataTable dt = FrmBLL.publicfuntion.getNewTable(dgv_receive.DataSource as DataTable, "料号='" + sPart + "'");
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.lb_qty.Text = dt.Rows[0]["数量"].ToString();
                }
                else
                {
                    this.lb_qty.Text = "0";
                    this.mFrm.ShowPrgMsg("该物料入库已提交完成,请确认!", MainParent.MsgType.Normal);
                }
            }
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    for (int i = 0; i < dgv_selectmaterials.Rows.Count; i++)
                    {
                        //Random rad = new Random();
                        //string materialid = string.Format("{0:yyMMdd}", DateTime.Now) + rad.Next(1000, 10000).ToString();
                        refwebtMaterialsReceive.Instance.InsertMaterialsReceive(new WebServices.tMaterialsReceive.tMaterialsReceive1()
                        {
                            Trsn  = Guid.NewGuid().ToString(),
                            PO = this.tb_po.Text.Trim(),
                            Kpnumber = dgv_selectmaterials["partnumber", i].Value.ToString(),
                            Qty =Convert.ToInt32( dgv_selectmaterials["qty", i].Value.ToString()),
                            Status = 1
                        });
                    }
                }
                RefData();
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Normal);
            }

        }


        private void bt_receive_Click(object sender, EventArgs e)
        {
            this.bt_receive.Enabled = false;
            this.bt_back.Enabled = false;
            if (dgv_materialsdta.Rows[dgv_materialsdta.CurrentCell.RowIndex].DefaultCellStyle.BackColor == Color.LightGray)
            {
                this.mFrm.ShowPrgMsg("该物料已全部提交，请确定!", MainParent.MsgType.Outgoing);
                this.bt_receive.Enabled = true;
                this.bt_back.Enabled = true;
                return;
            }

            InputQty iq = new InputQty(this);
            iq.ShowDialog();

            if (iq.tb_qty.Text == "0" || string.IsNullOrEmpty(iq.tb_qty.Text.Trim()))
            {
                this.mFrm.ShowPrgMsg("未输入数量或入库数量为零,请确认...", MainParent.MsgType.Normal);
                this.bt_receive.Enabled = true;
                this.bt_back.Enabled = true;
                return;
            }
            else
            {
                //DataTable dt=FrmBLL.publicfuntion.getNewTable(dgv_selectmaterials

                for (int i = 0; i < dgv_selectmaterials.Rows.Count; i++)
                {
                    if (dgv_selectmaterials.Rows[i].Cells["partnumber"].Value.ToString() == dgv_materialsdta["MATNR", dgv_materialshad.CurrentCell.RowIndex].Value.ToString())
                    {
                        int oldQty = Convert.ToInt32(dgv_selectmaterials.Rows[i].Cells["qty"].Value);
                        int newQty = oldQty + Convert.ToInt32(iq.tb_qty.Text.Trim());
                        if (newQty > Convert.ToInt32(this.lb_qty.Text))
                        {
                            this.mFrm.ShowPrgMsg("输入的物料数量大于待收的数量,请确认...", MainParent.MsgType.Normal);
                            this.bt_receive.Enabled = true;
                            this.bt_back.Enabled = true;
                            return;
                        }
                        dgv_selectmaterials.Rows[i].Cells["qty"].Value = newQty;
                        this.bt_receive.Enabled = true;
                        this.bt_back.Enabled = true;
                        return;
                    }
                }
                dgv_selectmaterials.Rows.Add(this.tb_po.Text.Trim(),
                    dgv_materialsdta["MATNR", dgv_materialsdta.CurrentCell.RowIndex].Value.ToString(),
                    iq.tb_qty.Text.Trim());
            }
            FrmMaterialsPrint mp = new FrmMaterialsPrint(mFrm,
                dgv_materialshad["LIFNR", dgv_materialshad.CurrentCell.RowIndex].Value.ToString(),
                dgv_materialshad["NAME1", dgv_materialshad.CurrentCell.RowIndex].Value.ToString(),
                dgv_materialsdta["MATNR", dgv_materialsdta.CurrentCell.RowIndex].Value.ToString(),
                dgv_materialsdta["MAKTX", dgv_materialsdta.CurrentCell.RowIndex].Value.ToString(),
                dgv_materialsdta["MATKL", dgv_materialsdta.CurrentCell.RowIndex].Value.ToString(),               
                iq.tb_qty.Text.Trim(),
                this.tb_po.Text.Trim());
            mp.ShowDialog();

            this.bt_receive.Enabled = true;
            this.bt_back.Enabled = true;

            RefData();
        }

        private void RefData()
        {
            DataTable table = FrmBLL.ReleaseData.arrByteToDataTable(
                RefWebService_BLL.refwebtMaterialsReceive.Instance.GetMaterialsByPO(this.tb_po.Text.Trim()));
            dgv_received.DataSource = table;//已入库数据

            dlur = new delegateloadunreceive(LoadUnreceiveData);//待入库数据
            dlur.BeginInvoke(null, null);

            dgv_selectmaterials.Rows.Clear();

        }

        private void FrmReceiveMaterials_Load(object sender, EventArgs e)
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
        private void LoadUnreceiveData()
        {
            ShowUnreceiveData(GetDealData());
        }
        private void ShowUnreceiveData(DataTable _dt)
        {
            this.dgv_receive.Invoke(new EventHandler(delegate
            {
                dgv_receive.DataSource = _dt;

            }));
        }
        /// <summary>
        /// 获取待入库的物料信息(dgv_receive的数据源)
        /// </summary>
        /// <returns></returns>
        private DataTable GetDealData()
        {
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("PO订单号");
            dtTemp.Columns.Add("料号");
            dtTemp.Columns.Add("数量");

            for (int i = 0; i < dgv_materialsdta.Rows.Count; i++)
            {
                DataTable dt = dgv_received.DataSource as DataTable;
                DataRow[] arrDr = dt.Select(" kpnumber= '" + dgv_materialsdta["MATNR", i].Value.ToString() + "'");
                int allQty = Convert.ToInt32(dgv_materialsdta["MENGE", i].Value.ToString().Split('.')[0]);
                int receivedQty = 0;
                int unreceivedQty = 0;
                foreach (DataRow dr in arrDr)
                {
                    receivedQty += Convert.ToInt32(dr["qty"].ToString().Split('.')[0]);
                }
                unreceivedQty = allQty - receivedQty;
                if (unreceivedQty > 1)
                {
                    dtTemp.Rows.Add(this.tb_po.Text.Trim(), dgv_materialsdta["MATNR", i].Value.ToString(), unreceivedQty);
                }
                else
                {
                    dgv_materialsdta.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                    continue;
                }
            }
            return dtTemp;
        }


        private void bt_back_Click(object sender, EventArgs e)
        {
            if (dgv_selectmaterials.Rows.Count > 0)
            {
                dgv_selectmaterials.Rows.Remove(dgv_selectmaterials.CurrentRow);
            }
        }

        private void tb_po_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.tb_po.Text.Trim()) && e.KeyValue == 13)
                {
                    bt_query_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Normal);
            }
        }




    }
}
