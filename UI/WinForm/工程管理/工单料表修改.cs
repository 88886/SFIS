using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using FrmBLL;

namespace SFIS_V2
{
    public partial class fmMaterialManagement : Office2007Form//Form
    {
        public fmMaterialManagement(MainParent Info)
        {
            InitializeComponent();
            sInfo = Info;
           
        }

      private  MainParent sInfo=null;
 
        private void fmMaterialManagement_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.sInfo.gUserInfo.rolecaption == "系统开发员")
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
            txuser.Text = sInfo.gUserInfo.userId;
            txseqmo.Focus();
        }

        string sSEQ;
        string sMO;
        DataTable dt;
        string sMachineCode;

        private void txseqmo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(txseqmo.Text) && e.KeyChar == 13)
            {
                QueryBom();
            }
        }

        private void QueryBom()
        {
            cbstation.Items.Clear();
            try
            {
                string str = txseqmo.Text.Trim();
                int i = str.IndexOf(' ');
                sSEQ = str.Substring(0, i);
                sMO = str.Substring(i + 1, str.Length - i - 1);
            }
            catch (Exception)
            {
                sInfo.ShowPrgMsg("料站表刷入错误,请重新刷入料站表 ", MainParent.MsgType.Error);
                txseqmo.SelectAll();
                sSEQ = "";
                return;
            }

          //  sSQL = string.Format("select b.lineId,b.machineId from tSmtKPMaster a, tMachineInfo b where a.lineId = b.machineId and a.masterId='{0}'", sSEQ);
          //  DataTable dtz = refWebExecuteSqlCmd.Instance.ExecuteQuerySQL(sSQL);
            DataTable dtz = FrmBLL.ReleaseData.arrByteToDataTable(refWebtMaterialPreparation.Instance.CheckNewStationList(sSEQ));


            if (dtz.Rows.Count == 0)
            {
                if (MessageBox.Show("此料表不是最新料表,请确认是否需要修改???", "ECN错误提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                labMsg.Text = "当前料表线别为: " + dtz.Rows[0][0].ToString() + "  机台代码为: " + dtz.Rows[0][1].ToString();
                sMachineCode = dtz.Rows[0][1].ToString();

                //sSQL = string.Format("select * from tMaterialPreparation where masterId='{0}' and woid='{1}' ", sSEQ, sMO);
                //dt = refWebExecuteSqlCmd.Instance.ExecuteQuerySQL(sSQL); 

                dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtMaterialPreparation.Instance.QueryAllStationList(sSEQ, sMO));
       
                DataTable dtdistinct = dt.DefaultView.ToTable(true, "stationno");
   
                for (int i = 0; i < dtdistinct.Rows.Count; i++)
                {
                    cbstation.Items.Add(dtdistinct.Rows[i]["stationno"].ToString());                 
                        
                }
              

                txseqmo.SelectAll();
            }
        }

       

        private void cbstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbstation.Text))
            {
                cbpn.Items.Clear();
                DataTable dz = publicfuntion.getNewTable(this.dt, string.Format("stationno = '{0}'", cbstation.Text.Trim()));
                if (dz.Rows.Count != 0)
                {
                    for (int x = 0; x < dz.Rows.Count; x++)
                    {
                        cbpn.Items.Add(dz.Rows[x]["kpnumber"].ToString());
                    }
                    cbpn.Text = dz.Rows[0]["kpnumber"].ToString();
                    txseq.Text = dz.Rows[0]["masterid"].ToString();
                    txwo.Text = dz.Rows[0]["woid"].ToString();
                    txpartno.Text = dz.Rows[0]["partnumber"].ToString();
                    txid.Text = dz.Rows[0]["mpid"].ToString();
                    txrepgroup.Text = dz.Rows[0]["replacegroup"].ToString();
                    ckdistinct.Checked = Convert.ToBoolean(int.Parse(dz.Rows[0]["kpdistinct"].ToString()));
                    txlocation.Text = dz.Rows[0]["localtion"].ToString();
                    txbomrev.Text = dz.Rows[0]["bomver"].ToString();
                    txside.Text = dz.Rows[0]["side"].ToString();
                    txpndesc.Text = dz.Rows[0]["kpdesc"].ToString();

                }
                else
                {
                    sInfo.ShowPrgMsg("没有数据可以选择", MainParent.MsgType.Error);
                }
            }

        }

        private void cbpn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbpn.Text))
            {
              
                DataTable df = publicfuntion.getNewTable(this.dt, string.Format("stationno = '{0}' and kpnumber='{1}'", cbstation.Text.Trim(),cbpn.Text.Trim()));
                if (df.Rows.Count != 0)
                {
                    
                  //  cbpn.Text = dz.Rows[0]["kpnumber"].ToString();
                    txseq.Text = df.Rows[0]["masterid"].ToString();
                    txwo.Text = df.Rows[0]["woid"].ToString();
                    txpartno.Text = df.Rows[0]["partnumber"].ToString();
                    txid.Text = df.Rows[0]["mpid"].ToString();
                    txrepgroup.Text = df.Rows[0]["replacegroup"].ToString();
                    ckdistinct.Checked = Convert.ToBoolean(int.Parse(df.Rows[0]["kpdistinct"].ToString()));
                    txlocation.Text = df.Rows[0]["localtion"].ToString();
                    txbomrev.Text = df.Rows[0]["bomver"].ToString();
                    txside.Text = df.Rows[0]["side"].ToString();
                    txpndesc.Text = df.Rows[0]["kpdesc"].ToString();

                }
                else
                {
                    sInfo.ShowPrgMsg("没有数据可以选择", MainParent.MsgType.Error);
                }

            }

        }

        private void butadd_Click(object sender, EventArgs e)
        {
            DataTable dtb = publicfuntion.getNewTable(this.dt, string.Format("stationno = '{0}' and kpnumber='{1}'", cbstation.Text.Trim(),cbpn.Text.Trim()));
            if (dtb.Rows.Count == 0)
            {
                WebServices.tWoBomInfo.tMaterialPreparation LsInsertbom = new WebServices.tWoBomInfo.tMaterialPreparation();
                //RefWebService_BLL.refWeb_tWoBomInfo.tMaterialPreparation LsInsertbom =  new RefWebService_BLL.refWeb_tWoBomInfo.tMaterialPreparation();             
                LsInsertbom.woId = txwo.Text.Trim();
                LsInsertbom.partnumber = txpartno.Text.Trim();
                LsInsertbom.userId = sInfo.gUserInfo.userId;
                LsInsertbom.stationno = cbstation.Text.Trim();
                LsInsertbom.masterId = txseq.Text.Trim();
                LsInsertbom.kpnumber = cbpn.Text.Trim();
                LsInsertbom.kpdesc = txpndesc.Text.Trim();
                LsInsertbom.replacegroup = txrepgroup.Text.Trim();
                LsInsertbom.kpdistinct =Convert.ToBoolean(ckdistinct.CheckState);
                LsInsertbom.localtion = txlocation.Text.Trim();
                LsInsertbom.bomver = txbomrev.Text.Trim();
                LsInsertbom.side = txside.Text.Trim();
                LsInsertbom.localtion = txlocation.Text.Trim();
                LsInsertbom.feedertype = "NA";

              string sRes=  refWebtWoBomInfo.Instance.InsertWoBomPrintInfo(LsInsertbom);
                     QueryBom();
                     sInfo.ShowPrgMsg("新增完成-" + sRes, MainParent.MsgType.Incoming);
            }
            else
            {
                sInfo.ShowPrgMsg("料表同一个料号不能在同一个 Feeder 位置出现多次.请确认", MainParent.MsgType.Warning);
            }
        }

        private void butmodify_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbstation.Text) && !string.IsNullOrEmpty(cbpn.Text) && !string.IsNullOrEmpty(txlocation.Text) && !string.IsNullOrEmpty(txpndesc.Text))
            {
                //sSQL = string.Format("update tMaterialPreparation set stationno = '{0}',kpnumber = '{1}' ,kpdesc = '{2}' ,replacegroup = '{3}',kpdistinct = '{4}',localtion = '{5}',recdate = sysdate,userid='{6}' where  mpId ='{7}'",
                //                   cbstation.Text.Trim(), cbpn.Text.Trim(), txpndesc.Text.Trim(), txrepgroup.Text.Trim(), Convert.ToBoolean(ckdistinct.CheckState), txlocation.Text.Trim(), sInfo.gUserInfo.userId, txid.Text.Trim());

                //refWebExecuteSqlCmd.Instance.ExecteSQLNonQuery(sSQL);

                refWebtMaterialPreparation.Instance.UpdateMaterialPreparation(new WebServices.tMaterialPreparation.tMaterialPreparation1()
                    {
                        stationno = cbstation.Text.Trim(),
                        kpnumber = cbpn.Text.Trim(),
                        kpdesc = txpndesc.Text.Trim(),
                        replacegroup = txrepgroup.Text.Trim(),
                        kpdistinct = Convert.ToBoolean(ckdistinct.CheckState),
                        localtion = txlocation.Text.Trim(),
                        userId = sInfo.gUserInfo.userId,
                        mpId = txid.Text.Trim()
                    });

                QueryBom();
                sInfo.ShowPrgMsg("修改完成", MainParent.MsgType.Incoming);
            }
            else
            {
                sInfo.ShowPrgMsg("料站,料号,零件位置和零件描述不能为空", MainParent.MsgType.Error);
            }    
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sMO) && !string.IsNullOrEmpty(sSEQ) && !string.IsNullOrEmpty(sMachineCode))
            {
                publicfuntion.PrintForTable(sInfo.gUserInfo.userId,
                    FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoBomInfo.Instance.GetMaterialPreparation(sMO, sSEQ)), sMachineCode, System.AppDomain.CurrentDomain.BaseDirectory + @"Excel");
            }
            else
            {
                sInfo.ShowPrgMsg("工单,SEQ或机器代码为空值,请确认是否为新料表", MainParent.MsgType.Error);
            }
        }

        private void butQuery_Click(object sender, EventArgs e)
        {
            dataGridViewX1.AutoGenerateColumns = false;

            //string sSQL = string.Format("select * from tWoBomInfo where woId='{0}'", edtmo.Text.Trim());
            //DataTable _dt = refWebExecuteSqlCmd.Instance.ExecuteQuerySQL(sSQL);
            DataTable _dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoBomInfo.Instance.QueryWoBomInfoData(edtmo.Text.Trim()));


            dataGridViewX1.DataSource = _dt;
            if (_dt.Rows.Count != 0)
            {
                for (int i = 0; i < dataGridViewX1.ColumnCount; i++)
                    this.dataGridViewX1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                edtwo.Text = dataGridViewX1[0, 0].Value.ToString();
                edtpartno.Text = dataGridViewX1[2, 0].Value.ToString();
                edtbomrev.Text = dataGridViewX1[7, 0].Value.ToString();
            }
            else
            {
                sInfo.ShowPrgMsg("未找到工单资料,请确认工单是否输入正确",MainParent.MsgType.Error);
            }
          
        }

        private void edtmo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(edtmo.Text) && (e.KeyChar == 13))
            {
                butQuery_Click(null, EventArgs.Empty);
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            edtmo.Focus();
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

        private void textBoxX4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void dataGridViewX1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                edtwo.Text = dataGridViewX1[0, e.RowIndex].Value.ToString();
                edtpartno.Text = dataGridViewX1[2, e.RowIndex].Value.ToString();
                edtbomrev.Text = dataGridViewX1[7, e.RowIndex].Value.ToString();

                butaddpn.Enabled = true;
            }

        }

        private void butaddpn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(edtpnno.Text) && !string.IsNullOrEmpty(edtkpdesc.Text) && !string.IsNullOrEmpty(edtprocess.Text) && !string.IsNullOrEmpty(edtqty.Text))
            {

                //sSQL = string.Format("insert into tWoBomInfo(wbiId,woId,userId,partnumber,kpnumber,kpdesc,qty,process,bomver,recdate)  values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',sysdate)",
                //    Guid.NewGuid(), edtwo.Text.Trim(), sInfo.gUserInfo.userId, edtpartno.Text.Trim(), edtpnno.Text.Trim(), edtkpdesc.Text.Trim(), edtqty.Text.Trim(), edtprocess.Text.Trim(), edtbomrev.Text.Trim());
                //refWebExecuteSqlCmd.Instance.ExecteSQLNonQuery(sSQL);

                refWebtWoBomInfo.Instance.InserWoBomData(new WebServices.tWoBomInfo.tWoBomInfoTable()
                    {                    
                        woId = edtwo.Text.Trim(),
                        UserId = sInfo.gUserInfo.userId,
                        PartNnumber = edtpartno.Text.Trim(),
                        KpNumber = edtpnno.Text.Trim(),
                        KpDesc = edtkpdesc.Text.Trim(),
                        QTY = edtqty.Text.Trim(),
                        Process = edtprocess.Text.Trim(),
                        BomRev = edtbomrev.Text.Trim(),
                        blocked=0
                    });
            
                butQuery_Click(null, EventArgs.Empty);
                sInfo.ShowPrgMsg("新增完成!", MainParent.MsgType.Incoming);
                edtkpdesc.Text = "";
                edtpnno.Text = "";
                edtprocess.Text = "";
                edtqty.Text = "";

            }
            else
            {
                sInfo.ShowPrgMsg("必填项目不能为空,请填写完整!", MainParent.MsgType.Error);
            }

        }

        private void dataGridViewX1_MouseEnter(object sender, EventArgs e)
        {
           
           
        }

        private void dataGridViewX1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex!=-1 && e.RowIndex!=-1)
            dataGridViewX1[e.ColumnIndex,e.RowIndex].ToolTipText = "单击鼠标显示内容后才能新增";
        }

      

       


    }
}