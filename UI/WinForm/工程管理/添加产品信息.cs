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
    public partial class createproduct :Office2007Form// Form
    {
        public createproduct(MainParent _mfrm)
        {
            InitializeComponent();
            mFrm = _mfrm;
        }
        MainParent mFrm = null;
   
        private readonly string strFunName = "EditProduct";
        private void bt_addlabletypes_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.tb_insertlabletypename.Text))
                {
                    if (FrmBLL.publicfuntion.WordsIScn(this.tb_insertlabletypename.Text))
                    {
                       MessageBoxEx.Show("类型不能包含中文字符..","提示");
                       return;
                    }
                    refWebtProduct.Instance.AddLableName(this.tb_insertlabletypename.Text.Trim());// BLL.tProduct.AddLableName(new Entity.tSerialTypes() { serialname = this.tb_insertlabletypename.Text.Trim() });
                }
                else
                    throw new Exception("序列类型名称不能为空");

                this.FillType();
                this.tb_insertlabletypename.Text = "";
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void cb_productTypes_DropDown(object sender, EventArgs e)
        {
            try
            {
                this.cb_productTypes.Items.Clear();
               // List<Entity.tProductsort> productsort = BLL.tProduct.GetProductSort;
                
                 //RefWebService_BLL.refWeb_tProduct.tProductsort[] productsort =  refWebtProduct.Instance.GetProductSort();
                string[] productsort =refWebtProduct.Instance.GetProductSort();
                foreach ( string item in productsort)
                {
                    this.cb_productTypes.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void FillDataGridView(DataTable _dt)
        {
            this.dgv_showproduct.Invoke(new EventHandler(delegate{
                this.dgv_showproduct.DataSource=_dt;
            }));
        }
        private void createproduct_Load(object sender, EventArgs e)
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
            try
            {
               // FillDataGridView( FrmBLL.ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetProduct()));
                FillDataGridView(FrmBLL.ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetProductInfo(string.Empty,string.Empty)));
                FillType();
                this.cb_productColor.Items.AddRange(new string[] { "黑色", "白色", "灰色", "粉红红色", "咖啡色" });
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void bt_selectenter_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView(FrmBLL.ReleaseData.arrByteToDataTable(
                    refWebtProduct.Instance.GetProductInfo(this.tb_selectpartnumber.Text.Trim(),
                    this.tb_selectproductname.Text.Trim())));
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }

        }

        /// <summary>
        /// 加载标签类型
        /// </summary>
        private void FillType()
        {
            this.clb_labeltypes.Items.Clear();
            string[] lslablenames =  refWebtProduct.Instance.GetLableList();// BLL.tProduct.GetLableList;
            foreach (string str in lslablenames)
            {
                this.clb_labeltypes.Items.Add(str);
            }
            this.clb_labeltypes.Refresh();
        }

        private void bt_saveproduct_Click(object sender, EventArgs e)
        {
            string Err = string.Empty;
            //保存产品信息
            try
            {

                if (string.IsNullOrEmpty(tb_partnumber.Text))
                {
                    MessageBoxEx.Show("请填写产品料号");
                    this.tb_partnumber.SelectAll();
                    this.tb_partnumber.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(this.tbVersionCode.Text))
                {
                    MessageBoxEx.Show("请填写产品版本");
                    this.tbVersionCode.SelectAll();
                    this.tbVersionCode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.tb_modelname.Text))
                {
                    MessageBoxEx.Show("请填写产品型号..");
                    this.tb_modelname.SelectAll();
                    this.tb_modelname.Focus();
                    return;
                }
                if (this.tb_modelname.Text.IndexOf(" ") >= 0)
                {
                 
                    MessageBoxEx.Show("该字符串有空格");
                    this.tb_modelname.SelectAll();
                    this.tb_modelname.Focus();
                    return;

                }

                if (this.clb_labeltypes.CheckedItems.Count < 1)
                {
                    if (MessageBoxEx.Show("没有勾选产品序列号类型!!\n\n是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
                        return;
                }           
                IList<IDictionary<string, object>> lslabletypes = new List<IDictionary<string, object>>();
                IDictionary<string, object> mst = null;
                for (int i = 0; i < this.clb_labeltypes.CheckedItems.Count; i++)
                {
                    mst = new Dictionary<string, object>();
                    mst.Add("PARTNUMBER", tb_partnumber.Text);
                    mst.Add("SERIALNAME", this.clb_labeltypes.CheckedItems[i].ToString());                    
                    lslabletypes.Add(mst);
                }
                Dictionary<string,object> Product = new Dictionary<string,object>();
                Product.Add("PARTNUMBER",this.tb_partnumber.Text.Trim());
                Product.Add("PRODUCTCOLOR",this.cb_productColor.Text.Trim());
                Product.Add("SORTNAME",this.cb_productTypes.Text.Trim());
                Product.Add("PRODUCTNAME",this.tb_modelname.Text.Trim());
                Product.Add("PRODUCTDESC",string.IsNullOrEmpty(this.rtb_productDesc.Text.Trim()) ? "NA" : this.rtb_productDesc.Text.Trim());
                 Product.Add("OTHER",string.IsNullOrEmpty(this.cb_projecttype.Text.Trim()) ? "NA" : this.cb_projecttype.Text.Trim());
                 Product.Add("PRODUCTSN", string.IsNullOrEmpty(this.tb_productsn.Text.Trim()) ? "NA" : this.tb_productsn.Text.Trim());
                Product.Add("BARCODE_LEN",string.IsNullOrEmpty(this.tb_barcode_len.Text.Trim()) ? "NA" : this.tb_barcode_len.Text.Trim());
                 Product.Add("SOLUTION",string.IsNullOrEmpty(this.tb_solution.Text.Trim()) ? "NA" : this.tb_solution.Text.Trim());
                 Product.Add("NAL_PREFIX",string.IsNullOrEmpty(this.tb_NALPREFIX.Text)?"NA":this.tb_NALPREFIX.Text.Trim());
                    Product.Add("CUSTOMER",this.tb_COUSTOMER.Text.Trim());
                    Product.Add("STAGE",this.tb_STAGE.Text.Trim());

                Dictionary<string,object> Palletinfo = new Dictionary<string,object>();
                Palletinfo.Add("PARTNUMBER",this.tb_partnumber.Text.Trim());
                 Palletinfo.Add("VERSIONCODE",this.tbVersionCode.Text.Trim());
                 Palletinfo.Add("TRAYQTY",numtray.Value);
                 Palletinfo.Add("CARTONQTY",numcarton.Value);
                 Palletinfo.Add("PALLETQTY",numpallet.Value);
                    string sRES = refWebtProduct.Instance.InsertProdctInfo(FrmBLL.ReleaseData.DictionaryToJson(Product), FrmBLL.ReleaseData.ListDictionaryToJson(lslabletypes),
                      FrmBLL.ReleaseData.DictionaryToJson(Palletinfo)  );                  

                if (sRES!="OK")
                {
                    MessageBox.Show(sRES);
                }             

                FrmBLL.publicfuntion.InserSystemLog(this.mFrm.gUserInfo.userId, "包装参数设定", "新增", "产品料号: " + this.tb_partnumber.Text.Trim() + " 产品版本: " + tbVersionCode.Text.Trim());

                if (string.IsNullOrEmpty(Err))
                    this.mFrm.ShowPrgMsg(string.Format("产品:\"{0}\"添加成功..", this.tb_modelname.Text), MainParent.MsgType.Incoming);
                else
                    throw new Exception(Err);
                #region 释放被锁住的资源
                if (refwebtEditing.Instance.DeletetEditingByfunname(this.tb_partnumber.Text.Trim()) != "OK")
                {
                    this.mFrm.ShowPrgMsg("资源释放失败..", MainParent.MsgType.Error);
                }
                #endregion
   
                FillDataGridView(FrmBLL.ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetProductInfo(string.Empty, string.Empty)));
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void cb_productTypes_Leave(object sender, EventArgs e)
        {
            //判定产品大类是否存在，否则保存
            if (string.IsNullOrEmpty(this.cb_productTypes.Text))
                return;
            try
            {
                if ( refWebtProduct.Instance.GetProductSortByName(this.cb_productTypes.Text.Trim()))
                    return;
                else
                {
                    if (MessageBoxEx.Show(string.Format("产品类型:\"{0}\"不存在!! \n\n是否添加该产品类型 ?",
                        this.cb_productTypes.Text), "提示",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
                        return;
                    //BLL.tProduct.InsertProdutsort(new Entity.tProductsort()
                    //{
                    //    sortname = this.cb_productTypes.Text.Trim()
                    //});
                     refWebtProduct.Instance.InsertProdutsort(this.cb_productTypes.Text.Trim());                   
                    this.mFrm.ShowPrgMsg(string.Format("产品类型:\"{0}\" 添加成功..",
                        this.cb_productTypes.Text), MainParent.MsgType.Outgoing);
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void dgv_showproduct_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    this.tb_partnumber.Text = this.dgv_showproduct["partnumber", e.RowIndex].Value.ToString();
                    #region 检查被锁住项目
                    //string err = refwebtEditing.Instance.ChktEditing(new WebServices.tEditing.tEditing1()
                    //{
                    //    prj = this.strFunName,
                    //    funname = this.tb_partnumber.Text.Trim(),
                    //    userId = this.mFrm.gUserInfo.userId,
                    //    username = this.mFrm.gUserInfo.username,
                    //    PC_NAME = FrmBLL.publicfuntion.HostName + " IP:" + ((LsIp.Count > 1) ? LsIp[1] : LsIp[0]),
                    //    MAC_ADD = FrmBLL.publicfuntion.getMacList()[0]
                    //});
                    string err = FrmBLL.publicfuntion.ChktEditing(this.tb_partnumber.Text.Trim(), this.strFunName, this.mFrm.gUserInfo.userId, this.mFrm.gUserInfo.username);
                    if (err != "OK")
                    {
                        if (err.IndexOf("ERROR") != -1)
                            this.mFrm.ShowPrgMsg(err, MainParent.MsgType.Error);
                        else
                            MessageBoxEx.Show(string.Format("用户:[{0}]/[{1}]--{2}正在编辑中,您现在不能进行修改!!",
    err.Split(':')[1].Split(',')[0], err.Split(':')[1].Split(',')[1], err.Split(':')[1].Split(',')[2]), "提示!!");
                        this.tb_partnumber.SelectAll();
                        this.tb_partnumber.Focus();
                        return;
                    }
                    #endregion
                    this.tb_modelname.Text = this.dgv_showproduct["productname", e.RowIndex].Value.ToString();
                    this.cb_productColor.Text = this.dgv_showproduct["productcolor", e.RowIndex].Value.ToString();
                    this.cb_productTypes.Text = this.dgv_showproduct["sortname", e.RowIndex].Value.ToString();
                    this.rtb_productDesc.Text = this.dgv_showproduct["productdesc", e.RowIndex].Value.ToString();
                    this.cb_projecttype.Text = this.dgv_showproduct["other", e.RowIndex].Value.ToString();

                    this.numcarton.Value = int.Parse(this.dgv_showproduct["CartonQty", e.RowIndex].Value.ToString());
                    this.numtray.Value = int.Parse(this.dgv_showproduct["TrayQty", e.RowIndex].Value.ToString());
                    this.numpallet.Value = int.Parse(this.dgv_showproduct["PalletQty", e.RowIndex].Value.ToString());
                    this.tbVersionCode.Text = this.dgv_showproduct["VersionCode", e.RowIndex].Value.ToString();
                    this.tb_solution.Text = this.dgv_showproduct["SOLUTION", e.RowIndex].Value.ToString();
                    this.tb_productsn.Text = this.dgv_showproduct["PRODUCTSN", e.RowIndex].Value.ToString();
                    this.tb_barcode_len.Text = this.dgv_showproduct["BARCODE_LEN", e.RowIndex].Value.ToString();
                    this.tb_NALPREFIX.Text = this.dgv_showproduct["NAL_PREFIX", e.RowIndex].Value.ToString();
                    this.tb_COUSTOMER.Text = this.dgv_showproduct["CUSTOMER", e.RowIndex].Value.ToString();
                    this.tb_STAGE.Text = this.dgv_showproduct["STAGE", e.RowIndex].Value.ToString();
                    DataTable _dt =  FrmBLL.ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetProductSnType(this.dgv_showproduct["partnumber", e.RowIndex].Value.ToString()));

                    for (int i = 0; i < this.clb_labeltypes.Items.Count; i++) //清除之前的选择项
                    {
                        this.clb_labeltypes.SetItemChecked(i, false);
                    }

                    for (int i = 0; i < this.clb_labeltypes.Items.Count; i++)
                    {
                        foreach (DataRow dr in _dt.Rows)
                        {
                            if (this.clb_labeltypes.Items[i].ToString() == dr["serialname"].ToString())
                            {
                                this.clb_labeltypes.SetItemChecked(i, true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void tb_partnumber_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_partnumber.Text))
                return;
            #region 检查被锁住项目
            //string err = refwebtEditing.Instance.ChktEditing(new WebServices.tEditing.tEditing1()
            //{
            //    prj = this.strFunName,
            //    funname = this.tb_partnumber.Text.Trim(),
            //    userId = this.mFrm.gUserInfo.userId,
            //    username = this.mFrm.gUserInfo.username,
            //    PC_NAME = FrmBLL.publicfuntion.HostName + " IP:" + ((LsIp.Count > 1) ? LsIp[1] : LsIp[0]),
            //    MAC_ADD = FrmBLL.publicfuntion.getMacList()[0]
            //});
            string err = FrmBLL.publicfuntion.ChktEditing(this.tb_partnumber.Text.Trim(), this.strFunName, this.mFrm.gUserInfo.userId, this.mFrm.gUserInfo.username);
            if (err != "OK")
            {
                if (err.IndexOf("ERROR") != -1)
                    this.mFrm.ShowPrgMsg(err, MainParent.MsgType.Error);
                else
                    MessageBoxEx.Show(string.Format("用户:[{0}]/[{1}]--{2}正在编辑中,您现在不能进行修改!!",
     err.Split(':')[1].Split(',')[0], err.Split(':')[1].Split(',')[1], err.Split(':')[1].Split(',')[2]), "提示!!");
                this.tb_partnumber.SelectAll();
                this.tb_partnumber.Focus();
                return;
            }
            #endregion
            if (this.tb_partnumber.Text.Trim().Length < 5)
            {
                MessageBoxEx.Show("请输入正确的料号", "提示");
                this.tb_partnumber.SelectAll();
                this.tb_partnumber.Focus();
                return;
            }
            if (FrmBLL.publicfuntion.WordsIScn(this.tb_partnumber.Text))
            {
                MessageBoxEx.Show("料号不能包含中文字符", "提示");
                this.tb_partnumber.SelectAll();
                this.tb_partnumber.Focus();
            }
        }

        private void tb_insertlabletypename_Leave(object sender, EventArgs e)
        {
            if (FrmBLL.publicfuntion.WordsIScn(this.tb_insertlabletypename.Text))
            {
                MessageBoxEx.Show("类型不能包含中文字符","提示");
                this.tb_insertlabletypename.SelectAll();
                this.tb_insertlabletypename.Focus();
            }
        }

        private void tbVersionCode_Leave(object sender, EventArgs e)
        {
            if (FrmBLL.publicfuntion.WordsIScn(this.tbVersionCode.Text))
            {
                MessageBoxEx.Show("版本信息不能包含中文字符", "提示");
                this.tbVersionCode.SelectAll();
                this.tbVersionCode.Focus();
            }
        }

        private void tb_selectpartnumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.bt_selectenter_Click(null, null);
            }
        }

        private void createproduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                refwebtEditing.Instance.DeletetEditingByUserIdAndPrj(this.mFrm.gUserInfo.userId, this.strFunName);
            }
            catch
            {
            }
        }
    }
}
