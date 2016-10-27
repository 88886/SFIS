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
    public partial class Material_obsolete :Office2007Form// Form
    {
        public Material_obsolete(MainParent _mfrm)
        {
            InitializeComponent();
            mFrm = _mfrm;
        }
        MainParent mFrm;
        public string rowid;
       

        private void Material_obsolete_Load(object sender, EventArgs e)
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

            PanleData.Visible = false;

            cbx_check_type.Items.Add("PN");
            cbx_check_type.Items.Add("PN+DC");
            cbx_check_type.Items.Add("PN+DC+*");
            cbx_check_type.Items.Add("PN+DC+~");
            cbx_check_type.Items.Add("PN+DC+LN");
            cbx_check_type.Items.Add("PN+DC+VC");
            cbx_check_type.Items.Add("PN+LN+*");
            cbx_check_type.Items.Add("PN+LN+~");
            cbx_check_type.Items.Add("PN+VC");
            cbx_check_type.Items.Add("PN+VC+LN");
            cbx_used_flag.Items.Add("Y");
            cbx_used_flag.Items.Add("N");

            QueryTable();         
         
        }

        private void QueryTable()
        {
         // string sSQL= " SELECT id AS ROWID,part_no as 料号,date_code as 生产周期,vender_code as 厂商代码,Lot_id as 生产批次,check_type as 检查类型,Used_flag as 是否检查,In_station_time as 进入系统时间,emp_no as 工号 FROM tPartBlocked";
         // dataGridViewX1.DataSource =  refWebExecuteSqlCmd.Instance.ExecuteQuerySQL(sSQL);// BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sSQL);

            dataGridViewX1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartBlocked.Instance.QueryPartBlocked());
           // dataGridViewX1.Columns[0].Visible = false;
        }

        private void btadd_Click(object sender, EventArgs e)
        {
            mmfalg = mflage.新增;
            PanleData.Visible = true;
            btmodify.Enabled = false;
            btdelete.Enabled = false;
            txt_part_no.Text = "";
            txt_date_code.Text = "";
            txt_lot_id.Text = "";
            txt_vender_code.Text = "";
            txt_part_no.Enabled = true;

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            PanleData.Visible = false;
            btadd.Enabled = true;
            btmodify.Enabled = true;
            btdelete.Enabled = true;
         
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                switch (mmfalg)
                {
                    case mflage.删除:
                        break;
                    case mflage.新增:
                        if (string.IsNullOrEmpty(cbx_used_flag.Text) || string.IsNullOrEmpty(cbx_check_type.Text))
                        {
                            mFrm.ShowPrgMsg("确认类型或是否确认栏位为空,请检查", MainParent.MsgType.Error);
                            return;
                        }
                        if (CheckDCLCFormat() == false)
                        {
                            return;
                        }

                        if (!string.IsNullOrEmpty(txt_part_no.Text))
                        {

                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            FrmBLL.publicfuntion.SerializeControl(dic, PanleData);
                            dic.Add("EMP_NO", mFrm.gUserInfo.userId);
                            refWebtPartBlocked.Instance.InsertPartBlocked(FrmBLL.ReleaseData.DictionaryToJson(dic));

                            //refWebtPartBlocked.Instance.InsertPartBlocked(new WebServices.tPartBlocked.tPartBlockedTable()
                            //    {
                            //        Part_No = textpn.Text.Trim(),
                            //        Date_Code = textdc.Text.Trim(),
                            //        VenderCode = textvc.Text.Trim(),
                            //        LotId = txt_lot_id.Text.Trim(),
                            //        CheckType = cbx_check_type.Text.Trim(),
                            //        UseFlag = cbx_used_flag.Text.Trim(),
                            //        UserId = mFrm.gUserInfo.userId

                            //    });

                            buttonX2_Click(null, EventArgs.Empty);
                            QueryTable();
                            mFrm.ShowPrgMsg("插入资料库完成", MainParent.MsgType.Incoming);
                        }
                        else
                        {
                            mFrm.ShowPrgMsg("料号不能为空", MainParent.MsgType.Error);
                        }
                        break;
                    case mflage.修改:

                        if (CheckDCLCFormat() == false)
                        {
                            return;
                        }                  
                        //refWebtPartBlocked.Instance.UpdatePartBlocked(new WebServices.tPartBlocked.tPartBlockedTable()
                        //    {
                        //        Date_Code = txt_date_code.Text.Trim(),
                        //        VenderCode = txt_vender_code.Text.Trim(),
                        //        LotId = txt_lot_id.Text.Trim(),
                        //        CheckType = cbx_check_type.Text.Trim(),
                        //        UseFlag = cbx_used_flag.Text.Trim(),
                        //        UserId = mFrm.gUserInfo.userId,
                        //        Part_No = txt_part_no.Text.Trim()
                        //        //id =rowid
                        //    });
                         Dictionary<string, object> dicModify = new Dictionary<string, object>();
                         FrmBLL.publicfuntion.SerializeControl(dicModify, PanleData);
                         dicModify.Add("EMP_NO", mFrm.gUserInfo.userId);
                         refWebtPartBlocked.Instance.UpdatePartBlocked(FrmBLL.ReleaseData.DictionaryToJson(dicModify));

                        buttonX2_Click(null, EventArgs.Empty);
                        QueryTable();
                        mFrm.ShowPrgMsg("资料修改成功", MainParent.MsgType.Incoming);
                        break;
                    default:
                        break;
                }
                
                if (btadd.Enabled == true)
                {
                    //if (string.IsNullOrEmpty(combflag.Text) || string.IsNullOrEmpty(combtype.Text))
                    //{
                    //    zz.ShowPrgMsg("确认类型或是否确认栏位为空,请检查",Color.Red);
                    //    return;
                    //}

                    //if (!string.IsNullOrEmpty(textpn.Text))
                    //{
                    //    string sSQL=string.Format("insert into tPartBlocked (part_no,date_code,vender_code,lot_id,check_type,used_flag,emp_no)  values ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    //        textpn.Text.Trim(),textdc.Text.Trim(),textvc.Text.Trim(),textlc.Text.Trim(),combtype.Text.Trim(),combflag.Text.Trim(),"aa");
                    //    BLL.MsSqllib.Instance.ExecteSQLNonQuery(sSQL);
                    //    buttonX2_Click(null, EventArgs.Empty);
                    //    QueryTable();
                    //    zz.ShowPrgMsg("插入资料库完成",Color.Green);
                    //}
                    //else
                    //{      
                    //    zz.ShowPrgMsg("料号不能为空",Color.Red);
                    //}
                }
                if (btmodify.Enabled == true)
                {
                    //string sSQL = string.Format("update tPartBlocked set date_code='{0}',vender_code='{1}',lot_id='{2}',check_type='{3}',used_flag='{4}',in_station_time=sysdate,emp_no='{5}' where part_no='{6}' and id='{7}'",
                    //   textdc.Text.Trim(),textvc.Text.Trim(),textlc.Text.Trim(),combtype.Text.Trim(),combflag.Text.Trim(),"aa",textpn.Text.Trim(),rowid );
                    //BLL.MsSqllib.Instance.ExecteSQLNonQuery(sSQL);
                    //buttonX2_Click(null, EventArgs.Empty);
                    //QueryTable();
                    //zz.ShowPrgMsg("资料修改成功", Color.Green);

                }
            }
            catch (Exception ex)
            {
                mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
         }

        private void btmodify_Click(object sender, EventArgs e)
        {
            mmfalg = mflage.修改;
            PanleData.Visible = true;
            btadd.Enabled = false;
            btdelete.Enabled = false;
            txt_part_no.Enabled = false;

        }
       
        private void dataGridViewX1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (btmodify.Enabled == true || btdelete.Enabled == true)
                {
                    //rowid = dataGridViewX1[0, e.RowIndex].Value.ToString();
                    txt_part_no.Text = dataGridViewX1[0, e.RowIndex].Value.ToString();
                    txt_date_code.Text = dataGridViewX1[1, e.RowIndex].Value.ToString();
                    txt_vender_code.Text = dataGridViewX1[2, e.RowIndex].Value.ToString();
                    txt_lot_id.Text = dataGridViewX1[3, e.RowIndex].Value.ToString();
                    cbx_check_type.Text = dataGridViewX1[4, e.RowIndex].Value.ToString();
                    cbx_used_flag.Text = dataGridViewX1[5, e.RowIndex].Value.ToString();
                }
            }
           
        }

        private void btdelete_Click(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(this.rowid))
                //    throw new Exception("没有选择需要删除的资料");
                mmfalg = mflage.删除;
                if (MessageBox.Show("确定要删除此资料吗?\r\n ", "删除信息提示 ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //string sSQL = string.Format("delete tPartBlocked where id='{0}' ", rowid);
                    // refWebExecuteSqlCmd.Instance.ExecteSQLNonQuery(sSQL);

                    //refWebtPartBlocked.Instance.DeletePartBlocked(rowid);
                    //refWebtPartBlocked.Instance.DeletePartBlocked(new WebServices.tPartBlocked.tPartBlockedTable()
                    //{
                    //    Date_Code = txt_date_code.Text.Trim(),
                    //    VenderCode = txt_vender_code.Text.Trim(),
                    //    LotId = txt_lot_id.Text.Trim(),
                    //    Part_No = txt_part_no.Text.Trim()
                    //    //id =rowid
                    //});
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    FrmBLL.publicfuntion.SerializeControl(dic, PanleData);
                    dic.Remove("CHECK_TYPE");
                    dic.Remove("USED_FLAG");
                    refWebtPartBlocked.Instance.DeletePartBlocked(FrmBLL.ReleaseData.DictionaryToJson(dic));

                    buttonX2_Click(null, EventArgs.Empty);
                    QueryTable();
                    mFrm.ShowPrgMsg("资料删除成功!!!", MainParent.MsgType.Incoming);
                }
            }
            catch (Exception ex)
            {
                mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        private enum mflage
        {
            新增,
            删除,
            修改
        }
        mflage mmfalg;
     

        private void dataGridViewX1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (PanleData.Visible == false)
            {
                btmodify_Click(null, EventArgs.Empty);
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

        private void textpn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(txt_part_no.Text))
            {
                txt_date_code.SelectAll();
                txt_date_code.Focus();
            }
        }

        private void textdc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(txt_date_code.Text))
            {
                txt_vender_code.SelectAll();
                txt_vender_code.Focus();
            }
        }

        private void textvc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(txt_vender_code.Text))
            {
                txt_lot_id.SelectAll();
                txt_lot_id.Focus();
            }
        }

        private void textlc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(txt_lot_id.Text))
            {
                bt_ok.Focus();
            }
        }

        private bool CheckDCLCFormat()
        {
            if (cbx_check_type.Text == "PN+DC+~")
            {
                try
                {
                    string s = txt_date_code.Text.Trim();
                    string[] sArray = s.Split('~');
                    if (sArray[0].Length != sArray[1].Length)
                    {
                        mFrm.ShowPrgMsg("输入的 生产周期 起始值位数与结束位数不相等", MainParent.MsgType.Error);
                        return false;
                    }
                }
                catch (Exception)
                {
                    mFrm.ShowPrgMsg("输入的 生产周期 格式错误,请确认", MainParent.MsgType.Error);
                    return false;
                }
            }

            if (cbx_check_type.Text == "PN+LN+~")
            {
                try
                {
                    string s = txt_lot_id.Text.Trim();
                    string[] sArray = s.Split('~');
                    if (sArray[0].Length != sArray[1].Length)
                    {
                        mFrm.ShowPrgMsg("输入的 生产批次 起始位数与结束位数不相等", MainParent.MsgType.Error);
                        return false;
                    }

                }

                catch (Exception)
                {
                    mFrm.ShowPrgMsg("输入的 生产批次 格式错误,请确认", MainParent.MsgType.Error);
                    return false;
                }
            }
            if (cbx_check_type.Text == "PN+DC+*")
            {
                if (txt_date_code.Text.Trim().Substring(txt_date_code.Text.Trim().Length - 1, 1) != "*")
                {
                    mFrm.ShowPrgMsg("生产周期 格式输入错误,最右边必须输入 * 号",MainParent.MsgType.Error);
                    return false;
                }
            }
            if (cbx_check_type.Text == "PN+LN+*")
            {
                if (txt_lot_id.Text.Trim().Substring(txt_lot_id.Text.Trim().Length - 1, 1) != "*")
                {
                    mFrm.ShowPrgMsg("生产批次 格式输入错误,最右边必须输入 * 号", MainParent.MsgType.Error);
                    return false;
                }
            }

            return true;
        }

        private void combtype_DropDown(object sender, EventArgs e)
        {

        }
       
    }
}