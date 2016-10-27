using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class Frm_Crate_MO : Office2007Form //Form
    {
        public Frm_Crate_MO(Office2007Form Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }

        Office2007Form mFrm;
        public string UserId = string.Empty;
        public string UserName = string.Empty;
        DataTable dt_All_wo = null;
        private readonly string strFunName = "WoEdit";

        /// <summary>
        /// true 当用户模糊查询时,指定鼠标点击行; false 默认鼠标点击第一行
        /// </summary>
        bool UserSelect = false;

        /// <summary>
        /// 鼠标点击需要修改的行
        /// </summary>
       public DataGridViewCellEventArgs dgve = null;
   
       System.Windows.Forms.Timer RefreshTimer = new System.Windows.Forms.Timer();
       private void RefreshTimer_Tick(object sender, EventArgs e)
       {
           GetAllWoInfo();
           FillDataGridView_MO();
   
       }
       private void Frm_Crate_MO_Load(object sender, EventArgs e)
       {
        //   this.panelEx3.VerticalScroll.Visible = true;

         
           this.RefreshTimer.Interval = 1000*60*10; //每十分钟刷新一次数据
           this.RefreshTimer.Enabled = true;
           this.RefreshTimer.Tick += new EventHandler(RefreshTimer_Tick);


           UserId = (mFrm as Frm_MO_Manage).UserId;
           UserName = (mFrm as Frm_MO_Manage).UserName;

           GetAllWoInfo();
           FillDataGridView_MO();
           this.dgv_woinfo.RowsDefaultCellStyle.BackColor = Color.Bisque;
           this.dgv_woinfo.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
           rad_Waiting_online_Click(null, null);
           if ((mFrm as Frm_MO_Manage).CreateMO_Flag == 1)
           {
               imbt_MO_NEW.Enabled = false;
               imbt_MO_online.Enabled = false;
               imbt_MO_OnOnline.Enabled = false;
               imbt_MO_Update.Enabled = true;
           }
       }



        /// <summary>
        /// 获取全部工单
        /// </summary>
        private void GetAllWoInfo()
        {
            dt_All_wo = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(null,null,null));
        }
        /// <summary>
        /// 填充工单到DataGridView
        /// </summary>
        private void FillDataGridView_MO()
        {
            DataTable dt = dt_All_wo;
            if (rad_Waiting_online.Checked)
                dgv_woinfo.DataSource = FrmBLL.publicfuntion.getNewTable(dt, string.Format("WOSTATE='{0}'", "0"));
            if (rad_in_line.Checked)
                dgv_woinfo.DataSource = FrmBLL.publicfuntion.getNewTable(dt, string.Format("WOSTATE IN ('{0}','{1}')", "1", "2"));
            if (rad_close.Checked)
                dgv_woinfo.DataSource = FrmBLL.publicfuntion.getNewTable(dt, string.Format("WOSTATE='{0}'", "3"));
            if (rad_hold.Checked)
                dgv_woinfo.DataSource = FrmBLL.publicfuntion.getNewTable(dt, string.Format("WOSTATE='{0}'", "4"));
            if (dgv_woinfo.Rows.Count > 0)
            {
                if (!UserSelect)
                {
                    DataGridViewCellEventArgs dataGridViewCellEventArgs = new DataGridViewCellEventArgs(0, 0);
                    dgv_woinfo_CellClick(dgv_woinfo, dataGridViewCellEventArgs);
                }
            }
        }
        private void FillDataGridView_SN_Rule(DataTable dt)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelX4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelX5_Click(object sender, EventArgs e)
        {

        }

        private void dgv_woinfo_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            FrmBLL.publicfuntion.UpdateFieldName(dgv_woinfo);
        }

        private void rad_Waiting_online_Click(object sender, EventArgs e)
        {
            ButtonEnable();
            FillDataGridView_MO();

            if ((mFrm as Frm_MO_Manage).CreateMO_Flag == 0)
            {               
                imbt_MO_Update.Enabled = true;
                imbt_MO_online.Enabled = true;
            }
            if ((mFrm as Frm_MO_Manage).CreateMO_Flag == 1)
            {
                imbt_MO_NEW.Enabled = false;
                imbt_MO_online.Enabled = false;
                imbt_MO_OnOnline.Enabled = false;
                imbt_MO_Update.Enabled = true;
            }
        }

        private void rad_in_line_Click(object sender, EventArgs e)
        {
            ButtonEnable();
            FillDataGridView_MO();
            if ((mFrm as Frm_MO_Manage).CreateMO_Flag == 0)
            {               
                imbt_MO_OnOnline.Enabled = true;
            }
            if ((mFrm as Frm_MO_Manage).CreateMO_Flag == 1)
            {
                imbt_MO_NEW.Enabled = false;
                imbt_MO_online.Enabled = false;
                imbt_MO_OnOnline.Enabled = false;
                imbt_MO_Update.Enabled = true;
            }
        }

        private void rad_close_Click(object sender, EventArgs e)
        {
            ButtonEnable();
            FillDataGridView_MO();
            if ((mFrm as Frm_MO_Manage).CreateMO_Flag == 0)
            {
               
            }
            if ((mFrm as Frm_MO_Manage).CreateMO_Flag == 1)
            {
                imbt_MO_NEW.Enabled = false;
                imbt_MO_online.Enabled = false;
                imbt_MO_OnOnline.Enabled = false;
                imbt_MO_Update.Enabled = false;
            }
        }

        private void rad_hold_Click(object sender, EventArgs e)
        {
            ButtonEnable();
            FillDataGridView_MO();
            if ((mFrm as Frm_MO_Manage).CreateMO_Flag ==0 )
            {
           
            }
            if ((mFrm as Frm_MO_Manage).CreateMO_Flag == 1)
            {
                imbt_MO_NEW.Enabled = false;
                imbt_MO_online.Enabled = false;
                imbt_MO_OnOnline.Enabled = false;
                imbt_MO_Update.Enabled = false;
            }
        }

        private void dgv_woinfo_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{
            //    dgve = e;
            //    FillDataGridToTextBox(e, groupBoxwoInfo);
            //    GetSnRule(dgv_woinfo["WOID", e.RowIndex].Value.ToString());
            //}
        }
        private void FillDataGridToTextBox(DataGridViewCellEventArgs e,Control Ctrl)
        {
            foreach (Control ctrl in Ctrl.Controls)
            {
                switch (ctrl.GetType().Name)
                {
                    case "TextBox":
                        {
                            ctrl.Text = dgv_woinfo[ctrl.Name.Substring(4, ctrl.Name.Length - 4).ToUpper(), e.RowIndex].Value.ToString();
                            break;
                        }
                    default:
                        break;
                }            
            }
        }
        private void GetSnRule(string woId)
        {
            dgv_snrule.Rows.Clear();
            DataTable dtesn = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.Get_ESN_Rule(woId));
            foreach (DataRow dr in dtesn.Rows)
            {
                dgv_snrule.Rows.Add("ESN",dr["SNPREFIX"].ToString(),dr["SNPOSTFIX"].ToString(),dr["SNSTART"].ToString(),dr["SNEND"].ToString(),dr["SNLENG"].ToString(),dr["CURRSN"].ToString());
            }
            DataTable dt_custsn = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoSnRule(woId,null));
            foreach (DataRow dr in dt_custsn.Rows)
            {
                dgv_snrule.Rows.Add(dr["SNTYPE"].ToString(), dr["SNPREFIX"].ToString(), dr["SNPOSTFIX"].ToString(), dr["SNSTART"].ToString(), dr["SNEND"].ToString(), dr["SNLENG"].ToString(), dr["USENUM"].ToString());
            }
        }

        private void imbt_MO_NEW_Click(object sender, EventArgs e)
        {
            Frm_WO_Update fwu = new Frm_WO_Update(0,this);
            if (fwu.ShowDialog()==DialogResult.OK)
            {
                GetAllWoInfo();
                FillDataGridView_MO();
                rad_Waiting_online_Click(null,null);
            }
        }

        private void imbt_Update_wo_Click(object sender, EventArgs e)
        {
            int Flag=0;
            if ((mFrm as Frm_MO_Manage).CreateMO_Flag == 0)
                Flag = 1; 
            if ((mFrm as Frm_MO_Manage).CreateMO_Flag == 1)
                Flag = 2;
            string Lock_woId = txt_woid.Text;
            string err = FrmBLL.publicfuntion.ChktEditing(Lock_woId, this.strFunName, UserId, UserName);
            if (err != "OK")
            {
                if (err.IndexOf("ERROR") != -1)
                {                  
                    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, err);
                    return;
                }
                else
                {
                    MessageBoxEx.Show(string.Format("用户:[{0}]/[{1}]--{2}正在编辑中,您现在不能进行修改!!",
                        err.Split(':')[1].Split(',')[0], err.Split(':')[1].Split(',')[1], err.Split(':')[1].Split(',')[2]), "提示!!");
                    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, string.Format("用户:[{0}]/[{1}]--{2}正在编辑中,您现在不能进行修改!!",
                        err.Split(':')[1].Split(',')[0], err.Split(':')[1].Split(',')[1], err.Split(':')[1].Split(',')[2]));
                    return;
                }
            }


            Frm_WO_Update fwu = new Frm_WO_Update(Flag, this);
            if (fwu.ShowDialog() == DialogResult.OK)
            {
                GetAllWoInfo();
                FillDataGridView_MO();
            }

            #region 释放被锁住的资源
            if (refwebtEditing.Instance.DeletetEditingByfunname(Lock_woId) != "OK")
            {
                 
                (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, "资源释放失败..");
            }
            #endregion
        }

        private void dgv_woinfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {             
                dgve = e;
                FillDataGridToTextBox(e, PanelShowData);
                GetSnRule(dgv_woinfo["WOID", e.RowIndex].Value.ToString());
            }
        }

        private void imbt_wo_online_Click(object sender, EventArgs e)
        {
            string woId=this.txt_woid.Text;
            if (MessageBox.Show(string.Format("确定要上线工单[{0}] ?", woId), "工单上线提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (FrmBLL.ReadIniFile.IniReadValue("ATESCRIPT", "AutoDload", System.AppDomain.CurrentDomain.BaseDirectory + "Updatecfg.ini") == "1")
                {
                    if (MessageBox.Show(string.Format("是否要下载工单[{0}]脚本 ?", woId), "工单脚本下载", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        Get_ATE_Script(woId);
                    }
                }

                Dictionary<string,object> dic  = new Dictionary<string,object>();
                dic.Add("WOID",woId);
                dic.Add("WOSTATE",1);
                dic.Add("USERID", UserId);
               string _StrErr= refWebtWoInfo.Instance.Insert_Wo_Info(FrmBLL.ReleaseData.DictionaryToJson(dic),null,null,null);
               if (_StrErr == "OK")
               {
                   GetAllWoInfo();
                   FillDataGridView_MO();
                   (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Incoming, "上线成功");
                   MessageBox.Show("上线成功");
               }
               else
                   MessageBox.Show(string.Format("上线失败[{0}]", _StrErr), "上线失败提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void imbt_on_online_Click(object sender, EventArgs e)
        {
            string woId = dgv_woinfo["WOID", dgve.RowIndex].Value.ToString();
            string WOSTATE = dgv_woinfo["WOSTATE", dgve.RowIndex].Value.ToString();

            if (Convert.ToInt32(WOSTATE) != 1)
            {
                (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, "工单已经开始生产,不可退回待上线");
                MessageBox.Show("工单已经开始生产,不可退回待上线", "退回待上线失败提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show(string.Format("确定将工单[{0}],退回待上线 ?", woId), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("WOID", woId);
                dic.Add("WOSTATE", 0);
                dic.Add("USERID", UserId);
                string _StrErr = refWebtWoInfo.Instance.Insert_Wo_Info(FrmBLL.ReleaseData.DictionaryToJson(dic), null, null, null);
                if (_StrErr == "OK")
                {
                    GetAllWoInfo();
                    FillDataGridView_MO();
                    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Incoming, "退回成功");
                    MessageBox.Show("退回成功");
                }
                else
                    MessageBox.Show(string.Format("退回失败[{0}]", _StrErr), "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void ButtonEnable()
        {          
            imbt_MO_OnOnline.Enabled = false;
            imbt_MO_online.Enabled = false;
            imbt_MO_Update.Enabled = false;
        }

        private void txt_MO_select_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_MO_select.Text) && e.KeyCode == Keys.Enter)
            {
                this.txt_MO_select.SelectAll();
                DataTable dtselect = FrmBLL.publicfuntion.getNewTable(dt_All_wo,string.Format( "WOID='{0}'",txt_MO_select.Text));
                if (dtselect.Rows.Count > 0)
                {
                    string WOSTATE = dtselect.Rows[0]["WOSTATE"].ToString();
                    switch (Convert.ToInt32(WOSTATE))
                    {
                        case 0:
                            if (!rad_Waiting_online.Checked) //避免重复刷新
                            {
                                rad_Waiting_online.Checked = true;
                                rad_Waiting_online_Click(null, null);
                            }
                            break;
                        case 1:                         
                        case 2:
                            if (!rad_in_line.Checked) //避免重复刷新
                            {
                                rad_in_line.Checked = true;
                                rad_in_line_Click(null, null);
                            }
                            break;

                        case 3:
                            if (!rad_close.Checked) //避免重复刷新
                            {
                                rad_close.Checked = true;
                                rad_close_Click(null, null);
                            }
                            break;
                        case 4:
                            if (!rad_hold.Checked) //避免重复刷新
                            {
                                rad_hold.Checked = true;
                                rad_hold_Click(null, null);
                            }
                            break;
                    }

                    dgv_woinfo.Refresh();
                    DataGridView _dgv = dgv_woinfo;
                    for (int i = 0; i < _dgv.RowCount; i++)
                    {
                        if (_dgv["WOID", i].Value.ToString() == txt_MO_select.Text.Trim())
                        {
                            _dgv.Rows[i].Selected = true;
                            _dgv.FirstDisplayedScrollingRowIndex = i;
                            UserSelect = true;
                            DataGridViewCellEventArgs dataGridViewCellEventArgs = new DataGridViewCellEventArgs(0, i);
                            dgv_woinfo_CellClick(dgv_woinfo, dataGridViewCellEventArgs);
                            UserSelect = false;
                        }
                        else
                            _dgv.Rows[i].Selected = false;
                    }
                }
                else
                {
                    MessageBox.Show("请确认工单是否正确");
                }
            }
        }

       private void Get_ATE_Script(string woId)
        {
           
                try
                {
                
                    string IniFileName = System.AppDomain.CurrentDomain.BaseDirectory + "Updatecfg.ini";
                    string FtpHost = FrmBLL.ReadIniFile.IniReadValue("ATESCRIPT", "Host", IniFileName);
                    string User = FrmBLL.ReadIniFile.IniReadValue("ATESCRIPT", "User", IniFileName);
                    string UsePwd = FrmBLL.ReadIniFile.IniReadValue("ATESCRIPT", "Password", IniFileName);
                    FrmBLL.Ftp_Socket fm = new FrmBLL.Ftp_Socket(FtpHost, woId, User, UsePwd, 21);            
                    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Incoming, "正在获取脚本....");
                    string StrScript = string.Empty;
                    try
                    {
                        List<string> ss = fm.FileList("*.ts");
                        foreach (string item in ss)
                        {
                            StrScript += item + ",";
                            (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Normal, item);
                        }
                    }
                    catch (Exception ex)
                    {                        
                        (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, "获取脚本异常:" + ex.Message);
                    }
               
                    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Incoming, "获取脚本完成,正在获取条码档....");
                    try
                    {
                        List<string> Lab = fm.FileList("*.Lab");
                        foreach (string item in Lab)
                        {
                            StrScript += item + ",";
                            (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Normal, item);
                        }
                    }
                    catch (Exception ex)
                    {
                        (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, "获取条码档异常:" + ex.Message);
                    }
                    
                    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Incoming, "读取ftp完成...");
                    if (!string.IsNullOrEmpty(StrScript))
                    {
                        (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Incoming, "开始保存脚本...");
                        StrScript = StrScript.Substring(0, StrScript.Length - 1);
                        IDictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("WOID", woId);
                        dic.Add("SCRIPT", StrScript);
                        string JsonStr = FrmBLL.ReleaseData.DictionaryToJson(dic);
                        string _StrErr = refWebtWoInfo.Instance.Insert_Wo_Info(null, JsonStr, null, null);
                        if (_StrErr == "OK")
                        {
                            FrmBLL.publicfuntion.InserSystemLog((mFrm as Frm_MO_Manage).UserId, "工单信息", "AteScript", JsonStr);
                            (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Incoming, "保存脚本完成...");
                        }
                        else
                            (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, "保存脚本失败:" + _StrErr);
                    }
                    
                }
                catch (Exception ex)
                {
                  
                    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, ex.Message);

                }

            
        }

       private void dgv_woinfo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
       {
           using (SolidBrush b = new SolidBrush(dgv_woinfo.RowHeadersDefaultCellStyle.ForeColor))
           {
               int linen = 0;
               linen = e.RowIndex + 1;
               string line = linen.ToString();
               e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X, e.RowBounds.Location.Y + 5);
               SolidBrush B = new SolidBrush(Color.Red);
           }
       }

       private void Frm_Crate_MO_SizeChanged(object sender, EventArgs e)
       {
         
       }

       

        
    }
}
