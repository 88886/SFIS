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
    public partial class formworkshop :Office2007Form// Form
    {
        public formworkshop(MainParent msInfo)
        {
            InitializeComponent();
            FsInfo = msInfo;
        }

        MainParent FsInfo;
        Dictionary<string, object> mst = null;
       private  enum sFlag
       {
           新增,
           修改
       }
        sFlag sMFlag;

        string sWsID;

        private void formworkshop_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.FsInfo.gUserInfo.rolecaption == "系统开发员")
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
            panel1.Visible = false;
            QuertyWsInfo();
            QueryfacidList();

        }
        Dictionary<string, string> mFacNameAndFacId = new Dictionary<string, string>();
        private void QueryfacidList()
        {
            //string sSQL = " select * from tFacInfo";
            //TableFacid = BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sSQL);
            if (this.mFacNameAndFacId.Count < 1)
            {
                DataTable _dt =  FrmBLL.ReleaseData.arrByteToDataTable(refWebtFacInfo.Instance.GetFacInfo());// BLL.tFacInfo.GetFacInfo();
                for (int x = 0; x <= _dt.Rows.Count - 1; x++)
                {
                    textfacid.Items.Add(_dt.Rows[x]["facname"].ToString());
                    this.mFacNameAndFacId.Add(_dt.Rows[x]["facname"].ToString(), _dt.Rows[x]["facId"].ToString());
                }
            }
        }

        private void QuertyWsInfo()
        {
            //string sSQL = "select wsId as 车间编号,facid as 工厂编号,userid as 负责人,wsname as 名称 from twsInfo ";
            //dataGridViewX1.DataSource = BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sSQL);
            this.dataGridViewX1.DataSource =  FrmBLL.ReleaseData.arrByteToDataTable(refWebtWsInfo.Instance.GetAllWsInfo());// BLL.tWsInfo.GetAllWsInfo();
        }

        private void butModify_Click(object sender, EventArgs e)
        {
            sMFlag = sFlag.修改;
            panel1.Visible = true;
            textwsid.Enabled = false;
            butDelete.Enabled = false;
            butAdd.Enabled = false;
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            sMFlag = sFlag.新增;
            panel1.Visible = true;
            textwsid.Text = "";
            textfacid.Text = "";
            textuserid.Text = "";
            textwsname.Text = "";
            textwsid.Enabled = true;
            butModify.Enabled = false;
            butDelete.Enabled = false;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            switch (sMFlag)
            {
                case sFlag.新增:
                    {

                        mst = new Dictionary<string, object>();
                        mst.Add("WSID",textwsid.Text.Trim());
                        mst.Add("FACID",FacNameAndFacId[this.textfacid.Text.Trim()]);
                        mst.Add("USERID",textuserid.Text.Trim());
                        mst.Add("WSNAME", textwsname.Text.Trim());
                        refWebtWsInfo.Instance.InsertWsInfo(FrmBLL.ReleaseData.DictionaryToJson(mst));
                        // refWebtWsInfo.Instance.InsertWsInfo(new WebServices.tWsInfo.tWsInfo1() 
                        // {
                        //    wsId = this.textwsid.Text.Trim(),
                        //    facId = this.FacNameAndFacId[this.textfacid.Text.Trim()],
                        //    userId = textuserid.Text.Trim(),
                        //    wsname = this.textwsname.Text.Trim()
                        //});
                                             
                         FrmBLL.publicfuntion.InserSystemLog(FsInfo.gUserInfo.userId, "车间设定", "新增", "车间:" + textwsid.Text.Trim() + "工厂编号:" + textwsid.Text.Trim() + " 负责人:" + textfacid.Text.Trim() + " 名称:" + textwsname.Text.Trim());

                        QuertyWsInfo();
                        FsInfo.ShowPrgMsg("新增车间完成--" , MainParent.MsgType.Incoming);
                        panel1.Visible = false;
                    }
                    break;

                case sFlag.修改:

                    //string sxSQL=string.Format("Update twsInfo set facId='{0}',userid='{1}',wsname='{2}' where wsid='{3}' ",
                    //    textfacid.Text.Trim(), textuserid.Text.Trim(), textwsname.Text.Trim(),textwsid.Text.Trim());
                    //BLL.BllMsSqllib.Instance.ExecteSQLNonQuery(sxSQL);

                    //BLL.tWsInfo.EditWsInfo(this.textwsid.Text.Trim(), new Entity.tWsInfo()
                    //{
                    //    wsId = this.textwsid.Text.Trim(),
                    //    facId = this.FacNameAndFacId[this.textfacid.Text.Trim()],
                    //    userId = textuserid.Text.Trim(),
                    //    wsname = this.textwsname.Text.Trim()
                    //});
                      mst = new Dictionary<string, object>();
                        mst.Add("WSID",textwsid.Text.Trim());
                        mst.Add("FACID",FacNameAndFacId[this.textfacid.Text.Trim()]);
                        mst.Add("USERID",textuserid.Text.Trim());
                        mst.Add("WSNAME", textwsname.Text.Trim());
                        refWebtWsInfo.Instance.EditWsInfo(FrmBLL.ReleaseData.DictionaryToJson(mst));
                    // refWebtWsInfo.Instance.EditWsInfo(this.textwsid.Text.Trim(),new WebServices.tWsInfo.tWsInfo1()
                    // {
                    //    wsId = this.textwsid.Text.Trim(),
                    //    facId = this.FacNameAndFacId[this.textfacid.Text.Trim()],
                    //    userId = textuserid.Text.Trim(),
                    //    wsname = this.textwsname.Text.Trim()
                    //});
                      
                     FrmBLL.publicfuntion.InserSystemLog(FsInfo.gUserInfo.userId, "车间设定", "修改", "车间:" + textwsid.Text.Trim() + "工厂编号:" + textwsid.Text.Trim() + " 负责人:" + textfacid.Text.Trim() + " 名称:" + textwsname.Text.Trim());
                    QuertyWsInfo();
                    FsInfo.ShowPrgMsg("修改车间完成--", MainParent.MsgType.Incoming);
                    break;

                default:
                    break;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            butAdd.Enabled = true;
            butModify.Enabled = true;
            butDelete.Enabled = true;
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除此车间吗?\r\n  车间=" + sWsID, "删除信息提示 ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                //string sSQL = string.Format("delete twsInfo  where wsid='{0}' ", sWsID);
                //BLL.BllMsSqllib.Instance.ExecteSQLNonQuery(sSQL);

                 refWebtWsInfo.Instance.DeleteWsInfo(sWsID);// BLL.tWsInfo.DeleteWsInfo(sWsID);
               
                 FrmBLL.publicfuntion.InserSystemLog(FsInfo.gUserInfo.userId, "车间设定", "删除", "车间:" + sWsID);
                QuertyWsInfo();
                FsInfo.ShowPrgMsg("修改车间完成--", MainParent.MsgType.Incoming);
            }
        }

        private void dataGridViewX1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (butModify.Enabled == true || butDelete.Enabled == true)
            {
                if (e.RowIndex != -1)
                {
                    textwsid.Text = sWsID = dataGridViewX1[0, e.RowIndex].Value.ToString();
                    textfacid.Text = dataGridViewX1[1, e.RowIndex].Value.ToString();
                    textuserid.Text = dataGridViewX1[2, e.RowIndex].Value.ToString();
                    textwsname.Text = dataGridViewX1[3, e.RowIndex].Value.ToString();
                }
            }

        }

        private void dataGridViewX1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (panel1.Visible == false)
            {
                butModify_Click(null, EventArgs.Empty);
            }
        }

        private void bt_qutryuser_Click(object sender, EventArgs e)
        {
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtUserInfo.Instance.GetUserInfo());
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

        private void bt_addFac_Click(object sender, EventArgs e)
        {
            fFacInfo dt = new fFacInfo(FsInfo);
            dt.ShowDialog();
        }

        Dictionary<string, string> FacNameAndFacId = new Dictionary<string, string>();
        private void textfacid_DropDown(object sender, EventArgs e)
        {
            this.textfacid.Items.Clear();
            this.FacNameAndFacId.Clear();

            DataTable _dt =  FrmBLL.ReleaseData.arrByteToDataTable(refWebtFacInfo.Instance.GetFacInfo());// BLL.tFacInfo.GetFacInfo();
          foreach (DataRow dr in _dt.Rows)
          {
              this.textfacid.Items.Add(dr["facname"].ToString());

              this.FacNameAndFacId.Add(dr["facname"].ToString(), dr["facId"].ToString());
          }
        }

        private void bt_getwsid_Click(object sender, EventArgs e)
        {
            this.textwsid.Text =  refWebExecuteSqlCmd.Instance.GetSeqBasics();// BLL.BllMsSqllib.Instance.GetSeqBasics;
        }
    }
}