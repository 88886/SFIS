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
    public partial class FrmBomNo : Office2007Form//   Form
    {
        public FrmBomNo(MainParent sInfo)
        {
            InitializeComponent();
            sMain = sInfo;
        }
        MainParent sMain;


        private void FrmBomNo_Load(object sender, EventArgs e)
        {

            #region 添加应用程序
            if (this.sMain.gUserInfo.rolecaption == "系统开发员")
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


            ListGroup = new Craftparameterurl(GetCraftparameterurl);
            ListGroup.BeginInvoke(null,null);

            ListBom = new BomNumberList(GetBomNumberList);
            ListBom.BeginInvoke(null, null);

            ListKeyParts = new KeyPartsList(GetKeyPartsList);
            ListKeyParts.BeginInvoke(null,null);


            #region 单元格交替颜色
            this.dgvshowlist.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.dgvshowlist.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            #endregion

          
        }

        private delegate void Craftparameterurl();
        Craftparameterurl ListGroup;

        private delegate void BomNumberList();
        BomNumberList ListBom;

        private delegate void KeyPartsList();
        KeyPartsList ListKeyParts;

        private void GetCraftparameterurl()
        {
           string[] item=  refWebtCraftInfo.Instance.GetCraftInfoCraftparameterurl();
           foreach (string Ditem in item)
           {               
               cbstationlist.Invoke( new EventHandler(delegate
                {
                    cbstationlist.Items.Add(Ditem);
                }));
           }
        }

        private void GetBomNumberList()
        {
            dgvbomno.Rows.Clear();

            string[] item = refWebtBomKeyPart.Instance.GetBomNumerList();       
            int i = 0;          

               dgvbomno.Invoke(new EventHandler(delegate
                   {
                       foreach (string Ditem in item)
                       {
                           i = i + 1;
                           dgvbomno.Rows.Add(i, Ditem);
                       }
                   }));

               if (dgvbomno.RowCount != 0)
               {
                   dgvshowlist.Invoke(new EventHandler(delegate
                       {
                           dgvshowlist.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtBomKeyPart.Instance.GetBomNoParts(dgvbomno[1, 0].Value.ToString()));
                       }));
               }
               if (dgvbomno.RowCount != 0)
               {
                   tbaddbom.Invoke(new EventHandler(delegate
                       {
                           tbaddbom.Text = dgvbomno[1, 0].Value.ToString();
                       }));
               }

               if (this.dgvshowlist.Rows.Count > 0)
               {
                   dgvshowlist.Columns[0].HeaderText = "物料料号";
                   dgvshowlist.Columns[1].HeaderText = "物料关系";
                   dgvshowlist.Columns[2].HeaderText = "途程";
               }

        }

        public void GetKeyPartsList()
        {
            dgvkeyparts.Invoke(new EventHandler(delegate
          {
              dgvkeyparts.DataSource =FrmBLL.ReleaseData.arrByteToDataTable( refWebtKeyPart.Instance.GetKeyParts());
             }));
        }

        private void dgvkeyparts_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvkeyparts.RowHeadersDefaultCellStyle.ForeColor))
            {
                int linen = 0;
                linen = e.RowIndex + 1;
                string line = linen.ToString();
                e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X, e.RowBounds.Location.Y + 5);
                SolidBrush B = new SolidBrush(Color.Red);
            }

        }


      
        private void dgvbomno_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
          
            if (((e.RowIndex != -1) && (btadd.Text == "新增") && (btadd.Enabled == true) && (btdelete.Enabled == true)))
            {
                DataTable dt = new DataTable();
                dgvshowlist.DataSource = 0;
             
                string BomNo = dgvbomno[1, e.RowIndex].Value.ToString();
                tbaddbom.Text = BomNo;
                dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtBomKeyPart.Instance.GetBomNoParts(BomNo));
                dgvshowlist.DataSource = dt;
                dgvshowlist.Columns[0].HeaderText = "物料料号";
                dgvshowlist.Columns[1].HeaderText = "物料关系";
                dgvshowlist.Columns[2].HeaderText = "途程";


            }

        }

        private void btadd_Click(object sender, EventArgs e)
        {
            if (btadd.Text == "新增")
            {

               
                btmodify.Enabled = false;
                btdelete.Enabled = false;
                dgvshowlist.DataSource =null;
                btadd.Text = "保存";
                tbaddbom.Enabled = true;
                tbaddbom.Text = "";
                tbaddbom.Focus();
                _dtlist = new DataTable();
                _dtlist.Columns.Add("物料料号", System.Type.GetType("System.String"));
                _dtlist.Columns.Add("物料关系", System.Type.GetType("System.String"));
                _dtlist.Columns.Add("途程", System.Type.GetType("System.String"));

                dgvshowlist.DataSource = _dtlist;
                lbtoleft.Enabled = true;
                lbtoright.Enabled = true;
            }
            else
                if (btadd.Text == "保存")
                {
                    if (!string.IsNullOrEmpty(tbaddbom.Text))
                    {
                        SaveBomNo();
                        FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "创建Bom", "新增", "Bom 编号: " + tbaddbom.Text.Trim());

                        #region 刷新新增后Bom
                        ListBom = new BomNumberList(GetBomNumberList);
                        ListBom.BeginInvoke(null, null);
                        #endregion

                        btmodify.Enabled = true;
                        btdelete.Enabled = true;
                        btadd.Text = "新增";
                        lbtoleft.Enabled = false;
                        lbtoright.Enabled = false;
                        tbaddbom.Enabled = false;
                        sMain.ShowPrgMsg("新增 Bom 『" + tbaddbom.Text.Trim() + "』 完成 ", MainParent.MsgType.Incoming);
                        tbaddbom.Text = "";
                    }
                    else
                    {
                        sMain.ShowPrgMsg("新增 Bom 编号不能为空", MainParent.MsgType.Error);
                        tbaddbom.Focus();

                    }
                }
        }

        private void SaveBomNo()
        {
            for (int z = 0; z < dgvshowlist.Rows.Count; z++)
            {
                //refWebtBomKeyPart.Instance.InsertBomNumber(new WebServices.tBomKeyPart.tBomKeyPartTable()
                //    {
                //        BomNumber = tbaddbom.Text.Trim(),
                //        KpNumber = dgvshowlist[0, z].Value.ToString(),
                //        KpRelation =Convert.ToInt32(dgvshowlist[1, z].Value.ToString()),
                //        Station = dgvshowlist[2, z].Value.ToString()

                //    });

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("BOMNUMBER", tbaddbom.Text.Trim());
                dic.Add("KPNUMBER", dgvshowlist[0, z].Value.ToString());
                dic.Add("KPRELATION", Convert.ToInt32(dgvshowlist[1, z].Value.ToString()));
                dic.Add("STATION", dgvshowlist[2, z].Value.ToString());
                refWebtBomKeyPart.Instance.InsertBomNumber(FrmBLL.ReleaseData.DictionaryToJson(dic));
            }
        }



        private void dgvbomno_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            dgvbomno.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(192, 192, 255);
          
        }

        private void dgvbomno_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            dgvbomno.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
        }

        string KeyParts = "";
        private void dgvkeyparts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dgvkeyparts.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(192, 192, 255);
                KeyParts = dgvkeyparts[0, e.RowIndex].Value.ToString();
            }
      
        }

        private void dgvkeyparts_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            dgvkeyparts.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
       
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tbbom_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (!string.IsNullOrEmpty(tbbom.Text)))
            {
                FrmBLL.publicfuntion.SelectDataGridViewRows(tbbom.Text.Trim(), dgvbomno, 1);
                tbbom.SelectAll();
            }
        }

        private void tbparts_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (!string.IsNullOrEmpty(tbparts.Text)))
            {
              dgvkeyparts.DataSource=FrmBLL.ReleaseData.arrByteToDataTable(refWebtKeyPart.Instance.CheckDupPartsNumber("", tbparts.Text.Trim()));
              //  FrmBLL.publicfuntion.SelectDataGridViewRows(tbparts.Text.Trim(),dgvkeyparts,0);
                tbparts.SelectAll();
            }

        }

        private void dgvshowlist_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvshowlist.RowHeadersDefaultCellStyle.ForeColor))
            {
                int linen = 0;
                linen = e.RowIndex + 1;
                string line = linen.ToString();
                e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X, e.RowBounds.Location.Y + 5);
                SolidBrush B = new SolidBrush(Color.Red);
            }
        }

        private void btaddparts_Click(object sender, EventArgs e)
        {
            FrmKeyParts fkp = new FrmKeyParts(sMain,this);
            fkp.ShowDialog();
        }

        DataTable _dtlist=null;
        private void lbtoleft_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(KeyParts))
            {
                MessageBox.Show("物料料号选择为空,请选择","异常信息提示");
                return;
            }

            if (!string.IsNullOrEmpty(cbstationlist.Text))
            {
                int x = 1;
                if (_dtlist.Rows.Count != 0)
                {

                    for (int i = 0; i < _dtlist.Rows.Count; i++)
                    {
                        if (dgvshowlist[0, i].Value.ToString() == KeyParts)
                        {
                            MessageBox.Show("物料: 『" + KeyParts + "』 已经选择,不能重复添加....","重复提示");
                            return;
                        }

                        if (dgvshowlist[2, i].Value.ToString() == cbstationlist.Text)
                        {
                            x = x + 1;
                        }
                    }
                }
                _dtlist.Rows.Add(KeyParts, x.ToString(), cbstationlist.Text);

                dgvshowlist.DataSource = _dtlist;


                ReorderingDgvList();

            }
            else
            {
                sMain.ShowPrgMsg("途程不能为空",MainParent.MsgType.Error);
            }
        }

        private void lbtoleft_MouseLeave(object sender, EventArgs e)
        {
            lbtoleft.BackColor = Color.FromArgb(255, 192, 192);
        }

        private void lbtoright_MouseLeave(object sender, EventArgs e)
        {
            lbtoright.BackColor = Color.FromArgb(255, 192, 192);
        }

        private void lbtoleft_MouseEnter(object sender, EventArgs e)
        {
            lbtoleft.BackColor = Color.FromArgb(0, 192, 0);
        }

        private void lbtoright_MouseEnter(object sender, EventArgs e)
        {
            lbtoright.BackColor = Color.FromArgb(0, 192, 0);
        }

        string BomParts = "";
        private void dgvshowlist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                BomParts = "";
                BomParts = dgvshowlist[0, e.RowIndex].Value.ToString();
            }
        }

        private void lbtoright_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(BomParts))
            {
                MessageBox.Show("物料料号选择为空,请选择","异常信息提示");
                return;
            }

            for (int y = dgvshowlist.Rows.Count - 1; y >= 0; y--)
            {
                if (dgvshowlist[0, y].Value.ToString() == BomParts)
                {
                    dgvshowlist.Rows.RemoveAt(y);
                    _dtlist.Rows.RemoveAt(y);
                }
            }

            ReorderingDgvList();

        }

        private void btcancel_Click(object sender, EventArgs e)
        {
            if ((btadd.Enabled != true) || (btdelete.Enabled != true) || (btmodify.Enabled != true))
            {
                btmodify.Enabled = true;
                btdelete.Enabled = true;
                btadd.Enabled = true;
                btadd.Text = "新增";
                lbtoleft.Enabled = false;
                lbtoright.Enabled = false;



                dgvshowlist.DataSource = null;
                dgvshowlist.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtBomKeyPart.Instance.GetBomNoParts(dgvbomno[1, 0].Value.ToString()));
                dgvshowlist.Columns[0].HeaderText = "物料料号";
                dgvshowlist.Columns[1].HeaderText = "物料关系";
                dgvshowlist.Columns[2].HeaderText = "途程";
                tbaddbom.Enabled = false;
                tbaddbom.Text = dgvbomno[1, 0].Value.ToString();
            }

        }
        /// <summary>
        /// 重新排序
        /// </summary>
        private void ReorderingDgvList()
        {

            DataTable dz = new DataTable();
            dz.Columns.Add("物料料号", System.Type.GetType("System.String"));
            dz.Columns.Add("物料关系", System.Type.GetType("System.String"));
            dz.Columns.Add("途程", System.Type.GetType("System.String"));

            for (int i = 0; i < dgvshowlist.RowCount; i++)
            {
                int m = 1;

                for (int k = 0; k < dz.Rows.Count; k++)
                {
                    if (dz.Rows[k][2].ToString() == dgvshowlist[2, i].Value.ToString())
                    {
                        m = m + 1;
                    }

                }

                dz.Rows.Add(dgvshowlist[0, i].Value.ToString(), m.ToString(), dgvshowlist[2, i].Value.ToString());


            }

            dgvshowlist.DataSource = null;
            dgvshowlist.DataSource = dz;
        }

        private void btmodify_Click(object sender, EventArgs e)
        {
            if (btmodify.Text == "修改")
            {
                lbtoleft.Enabled = true;
                lbtoright.Enabled = true;
                btadd.Enabled = false;
                btdelete.Enabled = false;
                _dtlist = dgvshowlist.DataSource as DataTable;
                btmodify.Text = "保存";
            }
            else
                if (btmodify.Text == "保存")
                {
                    refWebtBomKeyPart.Instance.DeleteBomNumber(tbaddbom.Text.Trim());
                    SaveBomNo();
                    FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "创建Bom", "修改", "Bom编号: " + tbaddbom.Text.Trim());
                    btadd.Enabled = true;
                    btdelete.Enabled = true;
                    btcancel_Click(null,null);
                    btmodify.Text = "修改";                 
                    lbtoleft.Enabled = false;
                    lbtoright.Enabled = false;
                    sMain.ShowPrgMsg("修改Bom 『"+tbaddbom.Text.Trim()+"』 成功",MainParent.MsgType.Incoming);
                }


        }

        private void btdelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbaddbom.Text))
            {
                string Bom = tbaddbom.Text.Trim();

                if (MessageBox.Show("确定要删除Bom 『" + Bom + "』 吗？ ", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    refWebtBomKeyPart.Instance.DeleteBomNumber(Bom);
                    FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "创建Bom", "删除", "Bom编号: " + Bom);
                    sMain.ShowPrgMsg("删除Bom " + Bom + " 成功",MainParent.MsgType.Incoming);
                    ListBom = new BomNumberList(GetBomNumberList);
                    ListBom.BeginInvoke(null, null);
                }
            }
            else
            {
                sMain.ShowPrgMsg("  未选择Bom编号   \n\r  删除失败", MainParent.MsgType.Error);
            }
        }
    }
}
