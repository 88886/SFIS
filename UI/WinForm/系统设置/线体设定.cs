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
    public partial class line_set :Office2007Form// Form
    {
        public line_set(MainParent showPMinfo)
        {
            InitializeComponent();
            showinfo = showPMinfo;
        }

        MainParent showinfo;

        private enum MMflag
        {
            新增,
            修改
        }
        MMflag mflag;

        public string LineName;      

        private void line_set_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.showinfo.gUserInfo.rolecaption == "系统开发员")
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
            QueryAllLine();
            this.InsertWorkshop();
            panel1.Visible = false;

        }

        private void QueryAllLine()
        {
         //string sSQL = "SELECT lineid as 线别,linedesc as 线别描述,startipaddr as 起始IP,endipaddr as 结束IP,wsid as 车间编号 ,userid as 负责人,plotid as 当前计划 FROM tLineInfo";
         //dataGridViewX1.DataSource = BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sSQL);

            dataGridViewX1.DataSource =  FrmBLL.ReleaseData.arrByteToDataTable(refWebtLineInfo.Instance.GetAllLineInfo());// BLL.tLineInfo.GetAllLineInfo();
         }

        private void butaddline_Click(object sender, EventArgs e)
        {
            mflag = MMflag.新增;            
            panel1.Visible = true;
            butmodify.Enabled = false;
            butDelete.Enabled = false;
            txt_lineId.Enabled = true;
            txt_lineId.Text = "";
            txt_linedesc.Text = "";
            txt_startipaddr.Text = "";
            txt_endipaddr.Text = "";
            txt_wsId.Text = "";
            txt_userid.Text = "";
            txt_plotId.Text = "";
            txt_lineId.SelectAll();
            txt_lineId.Focus();

        }

        private void butmodify_Click(object sender, EventArgs e)
        {
            mflag = MMflag.修改;
            panel1.Visible = true;
            txt_lineId.Enabled = false;
            butaddline.Enabled = false;
            butDelete.Enabled = false;
           
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                switch (mflag)
                {
                    case MMflag.新增:

                        if (!string.IsNullOrEmpty(txt_lineId.Text.Trim()) &&
                            !string.IsNullOrEmpty(txt_linedesc.Text.Trim()) &&
                            !string.IsNullOrEmpty(txt_wsId.Text.Trim()) &&
                            !string.IsNullOrEmpty(txt_userid.Text.Trim()))
                        {
                            //string sSQL = string.Format("insert into tLineInfo (lineId,linedesc,startIpAddr,endIpAddr,wsId,userId,plotId) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}') ",
                            //   tlinename.Text.Trim(), tlinedesc.Text.Trim(), tipstart.Text.Trim(), tendip.Text.Trim(), tWorkshop.Text.Trim(), tb_userid.Text.Trim(), tplan.Text.Trim());
                            //BLL.BllMsSqllib.Instance.ExecteSQLNonQuery(sSQL);
                           //refWebtLineInfo.Instance.InsertLineInfo(new WebServices.tLineInfo.tLineInfo1() 
                           // {
                           //     lineId = txt_lineId.Text.Trim(),
                           //     linedesc = this.txt_linedesc.Text.Trim(),
                           //     plotId = txt_plotId.Text.Trim(),
                           //     startIpAddress = txt_startipaddr.Text.Trim(),
                           //     endIpAddress = txt_endipaddr.Text.Trim(),
                           //     userId = txt_userid.Text.Trim(),
                           //     wsId = this.WsNameAndWsId[txt_wsId.Text.Trim()]
                           // });
                           Dictionary<string, object> dic = new Dictionary<string, object>();
                           FrmBLL.publicfuntion.SerializeControl(dic, panel1);
                           dic["WSID"] = this.WsNameAndWsId[txt_wsId.Text.Trim()];
                           refWebtLineInfo.Instance.InsertLineInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));

                           FrmBLL.publicfuntion.InserSystemLog(showinfo.gUserInfo.userId, "线体设定", "Insert", "线别: " + txt_lineId.Text);


                            QueryAllLine();
                            showinfo.ShowPrgMsg("线别新增完成--" , MainParent.MsgType.Incoming);
                            panel1.Visible = false;
                            butaddline.Enabled = true;
                            butmodify.Enabled = true;
                            butDelete.Enabled = true;
                        }
                        else
                        {
                            showinfo.ShowPrgMsg("线别,线别描述,车间编号或负责人 不能为空", MainParent.MsgType.Error);
                        }
                        break;
                    case MMflag.修改:

                        if (!string.IsNullOrEmpty(txt_linedesc.Text.Trim()) &&
                         !string.IsNullOrEmpty(txt_wsId.Text.Trim()) &&
                         !string.IsNullOrEmpty(txt_userid.Text.Trim()))
                        {
                            //string sSQL = string.Format("Update tLineInfo set linedesc='{0}',stratipaddr='{1}',endipaddr='{2}',wsid='{3}',userid='{4}',plotid='{5}' where lineid='{6}'",
                            //    tlinedesc.Text.Trim(), tipstart.Text.Trim(), tendip.Text.Trim(), tWorkshop.Text.Trim(), tb_userid.Text.Trim(), tplan.Text.Trim(), tlinename.Text.Trim());
                            //BLL.BllMsSqllib.Instance.ExecteSQLNonQuery(sSQL);

                            // refWebtLineInfo.Instance.EditLineInfo(txt_lineId.Text.Trim(),new WebServices.tLineInfo.tLineInfo1()
                            //{
                            //    lineId = txt_lineId.Text.Trim(),
                            //    linedesc = this.txt_linedesc.Text.Trim(),
                            //    plotId = txt_plotId.Text.Trim(),
                            //    startIpAddress = txt_startipaddr.Text.Trim(),
                            //    endIpAddress = txt_endipaddr.Text.Trim(),
                            //    userId = txt_userid.Text.Trim(),
                            //    wsId = this.WsNameAndWsId[txt_wsId.Text.Trim()]
                            //});
                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            FrmBLL.publicfuntion.SerializeControl(dic, panel1);
                            dic["WSID"] = this.WsNameAndWsId[txt_wsId.Text.Trim()];
                            refWebtLineInfo.Instance.EditLineInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));

                            FrmBLL.publicfuntion.InserSystemLog(showinfo.gUserInfo.userId, "线体设定", "Modify", "线别: " + txt_lineId.Text);
                            QueryAllLine();
                            showinfo.ShowPrgMsg("修改线体完成--" , MainParent.MsgType.Incoming);
                            panel1.Visible = false;
                            butaddline.Enabled = true;
                            butmodify.Enabled = true;
                            butDelete.Enabled = true;

                        }
                        else
                        {
                            showinfo.ShowPrgMsg("线别描述,车间编号或负责人 不能为空", MainParent.MsgType.Error);
                        }

                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                this.showinfo.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            butaddline.Enabled = true;
            butmodify.Enabled = true;
            butDelete.Enabled = true;
        }

        private void dataGridViewX1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (butmodify.Enabled == true || butDelete.Enabled == true)
                {
                    if (e.RowIndex != -1)
                    {
                        txt_lineId.Text = LineName = dataGridViewX1[0, e.RowIndex].Value.ToString();
                        txt_linedesc.Text = dataGridViewX1[1, e.RowIndex].Value.ToString();
                        txt_userid.Text = dataGridViewX1[2, e.RowIndex].Value.ToString();

                        this.txt_wsId.Text = dataGridViewX1[3, e.RowIndex].Value.ToString();

                        this.txt_startipaddr.Text = dataGridViewX1[4, e.RowIndex].Value.ToString();
                        this.txt_endipaddr.Text = dataGridViewX1[5, e.RowIndex].Value.ToString();
                        txt_plotId.Text = dataGridViewX1[6, e.RowIndex].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                this.showinfo.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定要删除此线体吗?\r\n  线别=" + LineName, "删除信息提示 ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //string sSQL = string.Format("delete LineInfo where lineid='{0}' ", LineName);
                    //BLL.BllMsSqllib.Instance.ExecteSQLNonQuery(sSQL);
                     refWebtLineInfo.Instance.DeleteLineInfo(LineName);// BLL.tLineInfo.DeleteLineInfo(LineName);

                     FrmBLL.publicfuntion.InserSystemLog(showinfo.gUserInfo.userId, "线体设定", "Delete", "线别: " + txt_lineId.Text);

                    QueryAllLine();
                    showinfo.ShowPrgMsg("线体资料删除成功!!! --" , MainParent.MsgType.Incoming);
                }
            }
            catch (Exception ex)
            {
                this.showinfo.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void dataGridViewX1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (panel1.Visible == false)
            {
                butmodify_Click(null, EventArgs.Empty);
            }
        }

        private void tlinename_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(txt_lineId.Text))
            {
                txt_startipaddr.SelectAll();
                txt_startipaddr.Focus();
            }
        }

        private void tipstart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(txt_startipaddr.Text))
            {
                txt_endipaddr.SelectAll();
                txt_endipaddr.Focus();
            }
        }

        private void tendip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(txt_endipaddr.Text))
            {
                txt_linedesc.SelectAll();
                txt_linedesc.Focus();
            }
        }

        private void tlinedesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(txt_linedesc.Text))
            {
                txt_wsId.SelectAll();
                txt_wsId.Focus();
            }
        }

        private void tWorkshop_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(txt_wsId.Text))
            {
                txt_userid.SelectAll();
                txt_userid.Focus();

            }
        }

        private void tmanager_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(txt_userid.Text))
            {
                txt_plotId.SelectAll();
                txt_plotId.Focus();
            }
        }

        private void tplan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(txt_plotId.Text))
            {
                bt_ok.Focus();
            }
        }

        private void bt_qureyuser_Click(object sender, EventArgs e)
        {
            DataTable dt =FrmBLL.ReleaseData.arrByteToDataTable(refWebtUserInfo.Instance.GetUserInfo());
           for (int i = 0; i < dt.Columns.Count; i++)
            {
                if ((dt.Columns[i].ToString().ToUpper() == "PWD") || (dt.Columns[i].ToString().ToUpper() == "密码"))
                {
                    dt.Columns.Remove(dt.Columns[i].ToString());
                }
            }
            SelectData sd = new SelectData(this,dt/* BLL.tUserInfo.GetUserInfo()*/);
            sd.ShowDialog();
        }

        private Dictionary<string, string> WsNameAndWsId = new Dictionary<string, string>();
        private void tWorkshop_DropDown(object sender, EventArgs e)
        {

        }

        private void InsertWorkshop()
        {
            try
            {
                DataTable _dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWsInfo.Instance.GetAllWsInfo());// BLL.tWsInfo.GetAllWsInfo();
                this.WsNameAndWsId.Clear();
                this.txt_wsId.Items.Clear();
                foreach (DataRow dr in _dt.Rows)
                {
                    this.txt_wsId.Items.Add(dr["车间名称"].ToString());
                    this.WsNameAndWsId.Add(dr["车间名称"].ToString(), dr["车间编号"].ToString());
                }
            }
            catch (Exception ex)
            {
                this.showinfo.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


       
    }
}
