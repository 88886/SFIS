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
    public partial class fmParMap :Office2007Form// Form
    {
        public fmParMap(MainParent info)
        {
            InitializeComponent();
            sFinfo = info;
        }
        MainParent sFinfo;
        private void fmParMap_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            List<WebServices.tUserInfo.tFunctionList> lsfunls = new List<WebServices.tUserInfo.tFunctionList>();
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button || this.Controls[i] is DevComponents.DotNetBar.ButtonX)
                {
                    lsfunls.Add(new WebServices.tUserInfo.tFunctionList()
                    {
                        funId = this.Controls[i].Name,
                        funname = this.Controls[i].Text,
                        fundesc = this.Controls[i].Text,
                        progid = this.Name
                    });
                }
            }
            FrmBLL.publicfuntion.AddProgInfo(new WebServices.tUserInfo.tProgInfo()
            {
                progid = this.Name,
                progname = this.Text,
                progdesc = this.Text

            }, lsfunls);
            #endregion
            AllParMap();
            panel1.Visible = false;
         //   edqrypn.Focus();
         //   edqrypn.SelectAll();
            edqrypn.Select();
        }

       

        private void AllParMap()
        {
            dataGridViewX1.DataSource =  FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartMap.Instance.AllPartMapsData_s());
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

      private  enum flag 
        {
            新增,
            修改
        }
      flag sFlag;
   
        private void butadd_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            edtpn.Text = "";
            edtoldpn.Text = "";
            edpndesc.Text = "";
            txtvenkp.Text = "";
            edtpn.Enabled = true;
            butmodify.Enabled = false;
            sFlag = flag.新增;

        }

        private void butmodify_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            edtpn.Text = "";
            edtoldpn.Text = "";
            edpndesc.Text = "";
            txtvenkp.Text = "";
            edtpn.Enabled = false;
            butadd.Enabled = false;
            sFlag = flag.修改;
        }

        private void dataGridViewX1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && butadd.Enabled == false)
            {
                edtpn.Text = dataGridViewX1["kpnumber", e.RowIndex].Value.ToString();
                edtoldpn.Text = dataGridViewX1["selfkpnumber", e.RowIndex].Value.ToString();
                txtvenkp.Text = dataGridViewX1["venderkpnumber", e.RowIndex].Value.ToString();
                edpndesc.Text = dataGridViewX1["kpdesc", e.RowIndex].Value.ToString();
            }
        }

       
        private void butok_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(edtpn.Text) && !string.IsNullOrEmpty(edtoldpn.Text) && !string.IsNullOrEmpty(edpndesc.Text) && !string.IsNullOrEmpty(txtvenkp.Text))
            {
                switch (sFlag)
                {
                    case flag.新增:
                        refWebtPartMap.Instance.InsertPartMaps_s(new WebServices.tPartMap.tPartMap1() 
                        {
                            kpnumber = edtpn.Text.Trim(),
                            selfkpnumber = edtoldpn.Text.Trim(),
                            kpdesc = edpndesc.Text.Trim(),
                            venkp = txtvenkp.Text.Trim()
                        });
                        
                        AllParMap();
                        sFinfo.ShowPrgMsg("新增完成", MainParent.MsgType.Incoming);
                        butcancel_Click(null, EventArgs.Empty);

                        FrmBLL.publicfuntion.InserSystemLog(sFinfo.gUserInfo.userId, "PartMaps", "ADD", edtpn.Text.Trim() + "-" + edpndesc.Text.Trim());

                        break;
                    case flag.修改:

                         refWebtPartMap.Instance.UpdatePartMaps_s(new WebServices.tPartMap.tPartMap1() 
                        {
                            kpnumber = edtpn.Text.Trim(),
                            selfkpnumber = edtoldpn.Text.Trim(),
                            kpdesc = edpndesc.Text.Trim(),
                            venkp = txtvenkp.Text.Trim()
                        });
                        AllParMap();
                        sFinfo.ShowPrgMsg("修改完成", MainParent.MsgType.Incoming);
                        butcancel_Click(null, EventArgs.Empty);
                        FrmBLL.publicfuntion.InserSystemLog(sFinfo.gUserInfo.userId, "PartMaps", "Modify", edtpn.Text.Trim() + "-" + edpndesc.Text.Trim());
                        break;

                    default:
                        break;

                }
            }
            else
            {
                sFinfo.ShowPrgMsg("物料料号、原厂料号与规格描述不能为空", MainParent.MsgType.Error);
            }
        }

         

        private void butcancel_Click(object sender, EventArgs e)
         {
            panel1.Visible = false;
            butadd.Enabled = true;
            butmodify.Enabled = true;
         }
           
        private void chmodify_MouseClick(object sender, MouseEventArgs e)
        {
            chmodify.Checked = !chmodify.Checked;
            if (chmodify.Checked==true)
            {
                edtoldpn.Enabled = false;
                chmodify.Checked = false;
            }
            else
            {
                edtoldpn.Enabled = true;
                chmodify.Checked = true;
            }
        }

        private void edtpn_Leave(object sender, EventArgs e)
        {
            edtoldpn.Text = edtpn.Text;
        }

      

        private void edqrypn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(edqrypn.Text) && e.KeyChar == 13)
            {
                for (int i = 0; i < dataGridViewX1.Rows.Count; i++)
                {
                    if (dataGridViewX1[0, i].Value.ToString() == edqrypn.Text.Trim())
                    {
                         dataGridViewX1[0, i].Selected = true;

                        dataGridViewX1.CurrentCell = dataGridViewX1[0, i];
                       // dataGridViewX1.Rows[i].Selected = true;
                        dataGridViewX1.FirstDisplayedScrollingRowIndex = i;
                    }
                }
                edqrypn.SelectAll();
            }
        }

       

    }
}
