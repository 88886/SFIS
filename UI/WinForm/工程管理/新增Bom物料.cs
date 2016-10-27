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
    public partial class FrmKeyParts : Office2007Form  //Form
    {
        public FrmKeyParts(MainParent sMP,FrmBomNo Fbn)
        {
            InitializeComponent();
            sMain = sMP;
            sFbn = Fbn;
        }

        MainParent sMain;
        FrmBomNo sFbn;
        private void FrmKeyParts_Load(object sender, EventArgs e)
        {
            ListKeyParts = new KeyPartsList(GetKeyPartsList);
            ListKeyParts.BeginInvoke(null, null);
        }

        private delegate void KeyPartsList();
        KeyPartsList ListKeyParts;

        private void GetKeyPartsList()
        {
            dataGridView1.Invoke(new EventHandler(delegate
            {
                dataGridView1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtKeyPart.Instance.GetKeyParts());
            }));
        }

        private void btaddparts_Click(object sender, EventArgs e)
        {
            if (btaddparts.Text == "新增")
            {
                btdeleteparts.Enabled = false;
                btaddparts.Text = "保存";
                PalData.Visible = true;
                tbPartNumber.Focus();
                tbPartNumber.Text = "";
                txt_kpnumber.Text = "";
                txt_kpname.Text = "";
                txt_kpdesc.Text = "";
                tbPartNumber.Enabled = true;
                tbPartNumber.Text = "";

            }
            else
                if (btaddparts.Text == "保存")
                {
                    InsertKeyParts();
                   
                }
        }

        private void InsertKeyParts()
        {
            if ((!string.IsNullOrEmpty(txt_kpnumber.Text)) && (!string.IsNullOrEmpty(txt_kpname.Text)) && (!string.IsNullOrEmpty(txt_kpdesc.Text)))
            {
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtKeyPart.Instance.CheckDupPartsNumber(txt_kpnumber.Text.Trim(),tbPartNumber.Text.Trim()));
                if (dt.Rows.Count > 0)
                {
                    sMain.ShowPrgMsg(" 成品料号: " + tbPartNumber.Text.Trim() + "  物料料号: " + txt_kpnumber.Text.Trim()+" 已经存在,不可重复添加",
                                     MainParent.MsgType.Error);
                    return;
                }

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("KPNUMBER", txt_kpnumber.Text.Trim());
                dic.Add("PARTNUMBER", tbPartNumber.Text.Trim());
                refWebtPartKeyParts.Instance.InsertPartKeyParts(FrmBLL.ReleaseData.DictionaryToJson(dic));
                //refWebtPartKeyParts.Instance.InsertPartKeyParts(new WebServices.tPartKeyParts.tPartKeyPartsTable()
                //    {
                //         KpNumber=txt_kpnumber.Text.Trim(),
                //          PartNumber=tbPartNumber.Text.Trim()
                //    });

                if (refWebtKeyPart.Instance.GetGetKeyPartsCount(txt_kpnumber.Text.Trim()) <= 0)
                {
                    //refWebtKeyPart.Instance.InsertKeyParts(new WebServices.tKeyPart.tKeyPartTable()
                    //    {
                    //        KpNumber = txt_kpnumber.Text.Trim(),
                    //        KpName = txt_kpname.Text.Trim(),
                    //        KpDesc = txt_kpdesc.Text.Trim()

                    //    });

                     dic = new Dictionary<string, object>();
                    FrmBLL.publicfuntion.SerializeControl(dic,panel1);
                    refWebtKeyPart.Instance.InsertKeyParts(FrmBLL.ReleaseData.DictionaryToJson(dic));
                }
                

                FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId,"Bom料号新增","新增","料号: "+txt_kpnumber.Text.Trim());

                btdeleteparts.Enabled = true;
                PalData.Visible = false;
                btaddparts.Text = "新增";
                GetKeyPartsList();
            }
            else
            {
                MessageBox.Show("输入项不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        
             }

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                int linen = 0;
                linen = e.RowIndex + 1;
                string line = linen.ToString();
                e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X, e.RowBounds.Location.Y + 5);
                SolidBrush B = new SolidBrush(Color.Red);
            }
        }

        private void tbkpnumber_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (!string.IsNullOrEmpty(txt_kpnumber.Text)))
            {
                txt_kpname.Focus();
                txt_kpname.SelectAll();
            }
        }

        private void tbkpname_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (!string.IsNullOrEmpty(txt_kpname.Text)))
            {
                txt_kpdesc.Focus();
                txt_kpdesc.SelectAll();
            }
        }

        private void tbkpdesc_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (!string.IsNullOrEmpty(txt_kpdesc.Text)))
            {
                btaddparts.Focus();
            }
        }

        private void tbkpnumber_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void tbqrykpno_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (!string.IsNullOrEmpty(tbqrykpno.Text)))
            {
                dataGridView1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtKeyPart.Instance.CheckDupPartsNumber("",tbqrykpno.Text.Trim()));
              //  FrmBLL.publicfuntion.SelectDataGridViewRows(tbqrykpno.Text.Trim(),dataGridView1,0);
                tbqrykpno.SelectAll();
            }
        }

        private void tbkpnumber_Leave(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }

            //if (tbkpnumber.Enabled == true)
            //{
            //    for (int i = 0; i < dataGridView1.RowCount; i++)
            //    {
            //        if (dataGridView1[0, i].Value.ToString() == tbkpnumber.Text.Trim())
            //        {
            //            MessageBox.Show("此料号: [" + tbkpnumber.Text.Trim() + "] 已经存在");
            //            tbkpnumber.Focus();
            //            tbkpnumber.SelectAll();
            //        }
            //    }
            //}
        }
        string KpNo = "";
        string PartNo = "";
        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)           
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
        }

        private void btdeleteparts_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("   确定要删除物料料号: " + KpNo+"\r\n    成品料号: "+PartNo, "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //refWebtKeyPart.Instance.DeleteKeyParts(new WebServices.tKeyPart.tKeyPartTable()
                //    {
                //        KpNumber = KpNo
                //    });

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("KPNUMBER", txt_kpnumber.Text.Trim());
                dic.Add("PARTNUMBER", tbPartNumber.Text.Trim());
                refWebtPartKeyParts.Instance.DeletePartKeyParts(FrmBLL.ReleaseData.DictionaryToJson(dic));
                //refWebtPartKeyParts.Instance.DeletePartKeyParts(new WebServices.tPartKeyParts.tPartKeyPartsTable()
                //    {
                //         KpNumber=KpNo,
                //         PartNumber = PartNo
                //    });

                FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "Bom料号新增", "删除", "料号: " + txt_kpnumber.Text.Trim());
                GetKeyPartsList();
                MessageBox.Show("删除成功", "删除提示");
            }
            
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (PalData.Visible == false)
            {
                PalData.Visible = true;
                txt_kpnumber.Enabled = false;
                lbModify.Visible = true;
                tbPartNumber.Enabled = false;
            }

        }

        private void lbModify_MouseEnter(object sender, EventArgs e)
        {
            lbModify.BackColor = Color.FromArgb(255, 128, 0);
        }

        private void lbModify_MouseLeave(object sender, EventArgs e)
        {
            lbModify.BackColor = Color.FromArgb(255, 128, 255);
        }

        private void lbModify_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(txt_kpname.Text)) && (!string.IsNullOrEmpty(txt_kpdesc.Text)))
            {
                //refWebtKeyPart.Instance.UpdateKeyParts(new WebServices.tKeyPart.tKeyPartTable()
                //    {
                //        KpNumber = txt_kpnumber.Text.Trim(),
                //        KpName = txt_kpname.Text.Trim(),
                //        KpDesc = txt_kpdesc.Text.Trim()
                //    });
                Dictionary<string, object> dic = new Dictionary<string, object>();
                FrmBLL.publicfuntion.SerializeControl(dic, panel1);
                refWebtKeyPart.Instance.UpdateKeyParts(FrmBLL.ReleaseData.DictionaryToJson(dic));

                FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "Bom料号新增", "修改", "料号: " + txt_kpnumber.Text.Trim());
                GetKeyPartsList();
                txt_kpnumber.Text = "";
                txt_kpname.Text = "";
                txt_kpdesc.Text = "";
                txt_kpnumber.Enabled = true;
                lbModify.Enabled = true;
                PalData.Visible = false;
                lbModify.Visible = false;

            }
        }

        private void btexit_Click(object sender, EventArgs e)
        {
            PalData.Visible = false;
            lbModify.Visible = false;
            btaddparts.Text = "新增";
            btaddparts.Enabled = true;
            btdeleteparts.Enabled = true;
            txt_kpnumber.Enabled = true;
            tbqrykpno.Focus();
        }

        private void FrmKeyParts_FormClosed(object sender, FormClosedEventArgs e)
        {
            sFbn.GetKeyPartsList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txt_kpnumber.Text = KpNo = dataGridView1[0, e.RowIndex].Value.ToString();
                tbPartNumber.Text = PartNo = dataGridView1[1, e.RowIndex].Value.ToString();
                txt_kpname.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                txt_kpdesc.Text = dataGridView1[3, e.RowIndex].Value.ToString();
            }
        }
    }
}
