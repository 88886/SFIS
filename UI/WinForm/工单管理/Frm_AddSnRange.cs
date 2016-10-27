using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SFIS_V2
{
    public partial class Frm_AddSnRange : Office2007Form //Form
    {
        public Frm_AddSnRange(string Key,string woId,Office2007Form Frm)
        {
            InitializeComponent();
            mFrm = Frm;
            mKey = Key;
            mwoId = woId;
        }
        Office2007Form mFrm;
        /// <summary>
        /// 序列号类型
        /// </summary>
        private string mKey;
        private string mwoId;
        private void Frm_AddSnRange_Load(object sender, EventArgs e)
        {
            this.label2.Text = string.Format("{0}区间:", this.mKey);

            this.dgvserialnumberrule.ReadOnly = false;
            this.dgvserialnumberrule.AllowUserToAddRows = true;
            this.dgvserialnumberrule.AllowUserToDeleteRows = true;

            LoadSnRange();

        }

        private void LoadSnRange()
        {
            if (mFrm is Frm_SnRange)
            {
                FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
                DataTable dt = ass.GetDatatable(string.Format("select woId,sntype,snstart,snend,snprefix,snpostfix,ver,reve,snleng,usenum from wosnrule where woId='{0}' and sntype='{1}'", this.mwoId, this.mKey));

                if (dt.Rows.Count > 0)
               {
                   dgvserialnumberrule.DataSource = dt;
               }
               else
               {
                 this.dgvserialnumberrule["woId", 0].Value = this.mwoId;
                this.dgvserialnumberrule["sntype", 0].Value = this.mKey;
                this.dgvserialnumberrule["snstart", 0].Value = "";
                this.dgvserialnumberrule["snend", 0].Value = "";
                this.dgvserialnumberrule["snprefix", 0].Value = "NA";
                this.dgvserialnumberrule["snpostfix", 0].Value = "NA";
                this.dgvserialnumberrule["ver", 0].Value = "A";
                this.dgvserialnumberrule["reve", 0].Value = "NA";
                this.dgvserialnumberrule["snleng", 0].Value = "0";
                this.dgvserialnumberrule["usenum", 0].Value = "1";
               }
            }
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            if (this.dgvserialnumberrule.Rows.Count < 2)
            {
                MessageBoxEx.Show("没有设置序列号区间", "提示");
                return;
            }
            try
            {
                string strTemp = string.Empty;
                string strTemp1 = string.Empty;
                FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
                ass.ExecuteOracleCommand(string.Format("delete from wosnrule where woId='{1}' and  sntype='{0}'", this.mKey, this.mwoId));
                for (int i = 0; i < this.dgvserialnumberrule.Rows.Count - 1; i++)
                {
                    object _snstart = this.dgvserialnumberrule["snstart", i].Value;
                    object _snend = this.dgvserialnumberrule["snend", i].Value;

                    //判断开始序列号是否大于结束序列号
                    if (_snstart == null || _snstart.ToString().Length < 3 || _snend == null || _snend.ToString().Length < 3)
                    {
                       // ass.ExecuteOracleCommand(string.Format("delete from wosnrule where woId='{1}' and  sntype='{0}'", this.mKey, this.mwoid));
                        this.dgvserialnumberrule.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 160);
                        MessageBoxEx.Show("序列号区间存在空值,或长度小于3!!请修正..", "提示");
                        return;
                    }
                    //判断长度是否一致
                    if (_snstart.ToString().Length != _snend.ToString().Length)
                    {
                       // ass.ExecuteOracleCommand(string.Format("delete from wosnrule where woId='{1}' and  sntype='{0}'", this.mKey, this.mwoid));
                        this.dgvserialnumberrule.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 160);
                        MessageBoxEx.Show("起始序列号与结束序列号长度不一致，请修正..");
                        return;
                    }

                    if (string.CompareOrdinal(_snstart.ToString(), _snend.ToString()) > 0)
                    {
                       // ass.ExecuteOracleCommand(string.Format("delete from wosnrule where woId='{1}' and  sntype='{0}'", this.mKey, this.mwoid));
                        this.dgvserialnumberrule.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 160);
                        MessageBoxEx.Show("序列号起始值大于结束值!!,请修正..");
                        return;
                    }

                    DataTable dtt = ass.GetDatatable(string.Format("select * from wosnrule where '{0}' between snstart and snend", this.dgvserialnumberrule["snstart", i].Value.ToString()));
                    bool bflag = true;
                    if (dtt != null && dtt.Rows.Count > 0)
                    {
                        if (dtt.Rows[0]["woId"].ToString().ToUpper() != this.mwoId.ToUpper())
                        {
                            bflag = false;
                        }
                    }
                    if (bflag)
                    {
                        dtt = ass.GetDatatable(string.Format("select * from wosnrule where '{0}' between snstart and snend", this.dgvserialnumberrule["snend", i].Value.ToString()));
                        if (dtt != null && dtt.Rows.Count > 0)
                        {
                            if (dtt.Rows[0]["woId"].ToString().ToUpper() != this.mwoId.ToUpper())
                            {
                                bflag = false;
                            }
                        }
                    }
                    if (!bflag)
                    {
                        this.dgvserialnumberrule.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 160);
                        // ass.ExecuteOracleCommand(string.Format("delete from wosnrule where woId='{1}' and  sntype='{0}'", this.mKey, this.mwoid));
                        MessageBox.Show("序列号区间已经存在于工单" + dtt.Rows[0]["woId"].ToString() + "中,请重新设置..", "提示");
                        return;
                    }
                    //需要剔除重工工单
                    if ((mFrm as Frm_SnRange)._woType != "Rework" && (mFrm as Frm_SnRange)._woType != "RMA")
                    {//如果是重工工单或者是RAM工单

                        //DataTable _dta = new DataTable();

                        string err = RefWebService_BLL.refWebtWoInfo.Instance.ChkSerialNumberRule_New(this.mwoId, _snstart.ToString(), _snend.ToString());
                        if (err.ToUpper() != "OK")
                        {
                            this.dgvserialnumberrule.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 160);
                          //  ass.ExecuteOracleCommand(string.Format("delete from wosnrule where woId='{1}' and  sntype='{0}'", this.mKey, this.mwoid));
                            MessageBox.Show(err, "提示");
                            return;
                        }
                        #region xxx
                        //bool _flag = true;
                        //_dta = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWoInfo.Instance.ChkSerialNumberRule(_snstart.ToString()));
                        //if (_dta != null && _dta.Rows.Count > 0)
                        //{//在数据库中找到了数据
                        //    if (_dta.Rows[0]["woId"].ToString().ToUpper() != this.mwoid.ToUpper()) //但是返回的数据不是同一个工单
                        //        _flag = false;
                        //}
                        //if (_flag)
                        //{
                        //    _dta = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWoInfo.Instance.ChkSerialNumberRule(_snend.ToString()));
                        //    if (_dta != null && _dta.Rows.Count > 0)
                        //    {
                        //        if (_dta.Rows[0]["woId"].ToString().ToUpper() != this.mwoid.ToUpper())
                        //            _flag = false;
                        //    }
                        //}
                        //if (!_flag)
                        //{
                        //    this.dgvserialnumberrule.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 160);
                        //    ass.ExecuteOracleCommand(string.Format("delete from wosnrule where woId='{1}' and  sntype='{0}'", this.mKey, this.mwoid));
                        //    MessageBox.Show("序列号区间已经存在于工单" + _dta.Rows[0]["woId"].ToString() + "中,请重新设置..", "提示");
                        //    return;
                        //}
                        #endregion
                    }

                    string sql = string.Format("insert into wosnrule(woId,sntype,snstart,snend,snprefix,snpostfix,ver,reve,snleng,usenum) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                            this.dgvserialnumberrule["woid", i].Value.ToString(),
                            this.dgvserialnumberrule["sntype", i].Value.ToString(),
                            this.dgvserialnumberrule["snstart", i].Value.ToString(),
                            this.dgvserialnumberrule["snend", i].Value.ToString(),
                            this.dgvserialnumberrule["snprefix", i].Value.ToString(),
                            this.dgvserialnumberrule["snpostfix", i].Value.ToString(),
                            this.dgvserialnumberrule["ver", i].Value.ToString(),
                            this.dgvserialnumberrule["reve", i].Value.ToString(),
                            this.dgvserialnumberrule["snleng", i].Value.ToString(),
                            this.dgvserialnumberrule["usenum", i].Value.ToString());
                    if (!ass.ExecuteOracleCommand(sql))
                    {
                        ass.ExecuteOracleCommand(string.Format("delete from wosnrule where woId='{1}' and  sntype='{0}'", this.mKey, this.mwoId));
                        MessageBox.Show("数据记录失败!!", "错误");
                        return;
                    }
                    this.dgvserialnumberrule.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void btInputdata_Click(object sender, EventArgs e)
        {

        }

        private void dgvserialnumberrule_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

            if (this.dgvserialnumberrule["woId", e.RowIndex].Value == null || this.dgvserialnumberrule["woId", e.RowIndex].Value.ToString().Length < 2)
            {
                this.dgvserialnumberrule["woId", e.RowIndex].Value = this.mwoId;
                this.dgvserialnumberrule["sntype", e.RowIndex].Value = this.mKey;
                this.dgvserialnumberrule["snstart", e.RowIndex].Value = "";
                this.dgvserialnumberrule["snend", e.RowIndex].Value = "";
                this.dgvserialnumberrule["snprefix", e.RowIndex].Value = "NA";
                this.dgvserialnumberrule["snpostfix", e.RowIndex].Value = "NA";
                this.dgvserialnumberrule["ver", e.RowIndex].Value = "A";
                this.dgvserialnumberrule["reve", e.RowIndex].Value = "NA";
                this.dgvserialnumberrule["snleng", e.RowIndex].Value = "0";
                this.dgvserialnumberrule["usenum", e.RowIndex].Value = e.RowIndex > 0 ? this.dgvserialnumberrule["usenum", 0].Value.ToString() : "1";
            }
        }

        private void dgvserialnumberrule_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvserialnumberrule.Columns[e.ColumnIndex].Name == "snstart")
                this.dgvserialnumberrule["snleng", e.RowIndex].Value = this.dgvserialnumberrule["snstart", e.RowIndex].Value == null ? "0" : this.dgvserialnumberrule["snstart", e.RowIndex].Value.ToString().Length.ToString();
        }

        private void dgvserialnumberrule_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvserialnumberrule["woId", e.RowIndex].Value == null || this.dgvserialnumberrule["woId", e.RowIndex].Value.ToString().Length < 2)
                return;
            //检查SNSTART和SNEND是否为空
            object _snstart = this.dgvserialnumberrule["snstart", e.RowIndex].Value;
            object _snend = this.dgvserialnumberrule["snend", e.RowIndex].Value;
            bool _flag = false;

            if (e.RowIndex > 0)
            {
                if (dgvserialnumberrule["usenum", e.RowIndex].Value.ToString() != dgvserialnumberrule["usenum", e.RowIndex - 1].Value.ToString())
                {
                    MessageBoxEx.Show("输入的序列号用量不一致,请修正", "提示");
                    this.dgvserialnumberrule["usenum", e.RowIndex].Style.BackColor = Color.Red;
                    return;
                }
                else
                {
                    this.dgvserialnumberrule["usenum", e.RowIndex].Style.BackColor = Color.White;
                }
                if (dgvserialnumberrule["snstart", e.RowIndex].Value.ToString().Length != dgvserialnumberrule["snstart", e.RowIndex - 1].Value.ToString().Length)
                {
                    MessageBoxEx.Show("输入的序列号长度与上一行的长度不一致", "提示");
                    this.dgvserialnumberrule["snstart", e.RowIndex].Style.BackColor = Color.Red;
                    return;
                }
                else
                {
                    this.dgvserialnumberrule["snstart", e.RowIndex].Style.BackColor = Color.White;
                }
                if (dgvserialnumberrule["snend", e.RowIndex].Value.ToString().Length != dgvserialnumberrule["snend", e.RowIndex - 1].Value.ToString().Length)
                {
                    MessageBoxEx.Show("输入的序列号长度与上一行的长度不一致", "提示");
                    this.dgvserialnumberrule["snend", e.RowIndex].Style.BackColor = Color.Red;
                    return;
                }
                else
                {
                    this.dgvserialnumberrule["snstart", e.RowIndex].Style.BackColor = Color.White;
                }
            }
            if (_snstart == null || string.IsNullOrEmpty(_snstart.ToString()))
            {
                _flag = true;
                this.dgvserialnumberrule["snstart", e.RowIndex].Style.BackColor = Color.Red;
            }
            if (_snend == null || string.IsNullOrEmpty(_snend.ToString()))
            {
                _flag = true;
                this.dgvserialnumberrule["snend", e.RowIndex].Style.BackColor = Color.Red;
            }
            if (_flag)
                this.dgvserialnumberrule.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 160);
        }
    }
}
