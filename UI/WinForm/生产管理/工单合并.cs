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
    public partial class FrmMoMerge : Office2007Form // Form
    {
        public FrmMoMerge(MainParent sInfo)
        {
            InitializeComponent();
            sMain = sInfo;
        }

        MainParent sMain;

        private void btok_Click(object sender, EventArgs e)
        {
            string showmsg = "";
            if (rdtiaoji.Checked)
                showmsg = "工单内调机";
            else
                showmsg = "合并工单";

            if (MessageBox.Show("请确认是 " + showmsg + " 吗?", "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {

                if (rdtiaoji.Checked)
                {
                    if (oldmo != sMO)
                    {
                        sMain.ShowPrgMsg("工单内调机,工单必须是同一工单..... ", MainParent.MsgType.Error);
                        return;
                    }
                }
                else
                {
                    if (oldmo == sMO)
                    {
                        sMain.ShowPrgMsg("合并工单生产,应该是不同工单,请确认..... ", MainParent.MsgType.Error);
                        return;
                    }

                    #region 检查是否能够合并工单生产
                    DataTable dtSmtMorge_Number = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtWoMerge.Instance.Get_Smt_WO_Merge(tboldmo.Text.Split(' ')[1]));//输入旧工单
                    if (dtSmtMorge_Number.Rows.Count > 0)
                    {
                        if (dtSmtMorge_Number.Rows.Count > 1)
                        {

                            sMain.ShowPrgMsg("合并工单编号查询到多笔信息.", MainParent.MsgType.Error);
                            MessageBox.Show("合并工单编号查询到多笔信息.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        Dictionary<string, object> mst = new Dictionary<string, object>();
                        mst.Add("MERGE_NUM", dtSmtMorge_Number.Rows[0]["MERGE_NUM"].ToString());
                        DataTable dtSmtMorge2 = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtWoMerge.Instance.Get_Smt_WO_Merge(FrmBLL.ReleaseData.DictionaryToJson(mst), "NEW_WOID,OLD_WOID"));
                        List<string> Ls_woId = new List<string>();
                        foreach (DataRow dr in dtSmtMorge2.Rows)
                        {
                            if (!Ls_woId.Contains(dr[0].ToString()))
                                Ls_woId.Add(dr[0].ToString());
                            if (!Ls_woId.Contains(dr[1].ToString()))
                                Ls_woId.Add(dr[1].ToString());
                        }
                        if (Ls_woId.Count >= 4)   //如果合并工单大于等于4笔时,并且当前的工单不在4个工单内,就抛出异常,避免最后一个工单只能合并一个机器的问题
                        {
                            if (!(Ls_woId.Contains(tbnewmo.Text.Split(' ')[1]) && Ls_woId.Contains(tboldmo.Text.Split(' ')[1])))
                            {
                                sMain.ShowPrgMsg("合并工单大于[4]笔,不可继续合并生产 ", MainParent.MsgType.Error);
                                MessageBox.Show("合并工单大于[4]笔,不可继续合并生产", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                        }
                    }
                    #endregion
                }

                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebSmtIO.Instance.GetSmtIO(sSEQ, sMO));
                if (dt.Rows.Count == 0)
                {

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("MASTERID", sSEQ);
                    dic.Add("WOID", sMO);
                    dic.Add("USERID", sMain.gUserInfo.userId);
                    dic.Add("MACHINEID", MachineCode);
                    dic.Add("STATUS", "3");
                    dic.Add("SIDE", sSIDE);                 
                    refWebSmtIO.Instance.InserSmtIo(FrmBLL.ReleaseData.DictionaryToJson(dic));
                    dic = new Dictionary<string, object>();
                    dic.Add("MASTERID", dgvsmtio.Rows[0].Cells[0].Value.ToString());
                    dic.Add("WOID", dgvsmtio.Rows[0].Cells[1].Value.ToString());
                    dic.Add("STATUS", "4");
                    refWebSmtIO.Instance.UpdateSmtIOStatus(FrmBLL.ReleaseData.DictionaryToJson(dic));
                }
                else
                {
                    string sSTATUS = dt.Rows[0]["status"].ToString();
                    if (sSTATUS == "4")
                    {
                        sMain.ShowPrgMsg("该料站表已下线,请确认 ", MainParent.MsgType.Error);
                        return;
                    }
                    else
                    if (sSTATUS == "3")
                    {
                        sMain.ShowPrgMsg("该料站表已是生产状态,不需要做[" + showmsg+"]作业", MainParent.MsgType.Error);
                        return;
                    }
                    else
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("MASTERID", sSEQ);
                        dic.Add("WOID", sMO);
                        dic.Add("STATUS", "3");
                        refWebSmtIO.Instance.UpdateSmtIOStatus(FrmBLL.ReleaseData.DictionaryToJson(dic));
                    }
                }
                if (rdtiaoji.Checked)
                {
                    sMain.ShowPrgMsg("工单内调机完成 ", MainParent.MsgType.Incoming);
                    FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "合并工单", "工单调机", "原工单:" + oldmo + " 新工单" + sMO + " --原料站表:" + tboldmo.Text + " 新料站表:" + tbnewmo.Text);
                }
                if (rdchgmo.Checked)
                {
                  


                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("NEW_MASTERID", tbnewmo.Text.Split(' ')[0]);
                    dic.Add("NEW_WOID", tbnewmo.Text.Split(' ')[1]);
                    dic.Add("OLD_MASTERID", tboldmo.Text.Split(' ')[0]);
                    dic.Add("OLD_WOID", tboldmo.Text.Split(' ')[1]);
                    dic.Add("USERID", sMain.gUserInfo.userId);
                    DataTable dt_woMerge = Get_SmtWoMorge(tboldmo.Text.Split(' ')[1]); //读取是否是第二个工单与第一个工单合并生产
                    if (dt_woMerge.Rows.Count > 0)
                        dic.Add("MERGE_NUM", dt_woMerge.Rows[0]["MERGE_NUM"].ToString());
                    else
                    {
                        dt_woMerge = Get_SmtWoMorge_NEW(tboldmo.Text.Split(' ')[1]); //读取第三个工单是否与第二个工单有合并生产
                        if (dt_woMerge.Rows.Count>0)
                            dic.Add("MERGE_NUM", dt_woMerge.Rows[0]["MERGE_NUM"].ToString());
                        else
                            dic.Add("MERGE_NUM", refWebtSmtWoMerge.Instance.Get_Merge_No());
                    }
                       
                   string _StrErr= refWebtSmtWoMerge.Instance.Insert_Smt_WO_Merge(FrmBLL.ReleaseData.DictionaryToJson(dic));
                    if (_StrErr=="OK")                 
                        MessageBox.Show("合并工单完成","成功提示");
                    else
                       // sMain.ShowPrgMsg("记录合并工单信息异常 "+_StrErr, MainParent.MsgType.Error);
                        MessageBox.Show("合并工单失败:" + _StrErr, "失败提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "合并工单", "合并工单", "原工单:" + oldmo + " 新工单" + sMO + " --原料站表:" + tboldmo.Text + " 新料站表:" + tbnewmo.Text);
                    
                }
                

                btok.Enabled = false;
            }
        }

        string oldmo = "";
        string OldMachineCode = "";
        string OldSide = "";
        private void tboldmo_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tboldmo.Text)) && (e.KeyValue == 13))
            {
                 sSEQ = "";
                 sMO = "";
                if (CheckSeqMo(tboldmo.Text.Trim()))
                {
                  

                    DataTable dtsmtio = FrmBLL.ReleaseData.arrByteToDataTable(refWebSmtIO.Instance.GetAllSmtIO(sSEQ,sMO));
                    if (dtsmtio.Rows.Count == 0)
                    {
                        sMain.ShowPrgMsg("原料站表没有备料信息,请确认.... ", MainParent.MsgType.Error);
                        tboldmo.Focus();
                        tboldmo.SelectAll();
                        tbnewmo.Enabled = false;
                        return;

                    }
                    oldmo = sMO;
                    OldMachineCode = dtsmtio.Rows[0]["machineid"].ToString();
                    OldSide = dtsmtio.Rows[0]["side"].ToString();

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("WOID", sMO);
                    dgvmaterial.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtKpNormalLog.Instance.QuerytSmtKpNormallog(FrmBLL.ReleaseData.DictionaryToJson(dic),false));
              
                    if (dtsmtio.Rows.Count != 0)
                    {
                        dgvsmtio.DataSource = dtsmtio;
                        string sStatus = dtsmtio.Rows[0]["status"].ToString();
                        if (sStatus == "0")
                        {
                            sMain.ShowPrgMsg("原料站表状态为正在备料,请确认.... ", MainParent.MsgType.Error);
                            tboldmo.Focus();
                            tboldmo.SelectAll();
                            tbnewmo.Enabled = false;
                            return;
                        }
                        
                        if (sStatus == "1")
                        {
                            sMain.ShowPrgMsg("原料站表状态为备料完成,请确认.... ", MainParent.MsgType.Error);
                            tboldmo.Focus();
                            tboldmo.SelectAll();
                            tbnewmo.Enabled = false;
                            return;
                        }
                       
                        if (sStatus == "2")
                        {
                            sMain.ShowPrgMsg("原料站表状态为正在换线,请确认.... ", MainParent.MsgType.Error);
                            tboldmo.Focus();
                            tboldmo.SelectAll();
                            tbnewmo.Enabled = false;
                            return;
                        }
                    
                        if (sStatus == "4")
                        {
                            sMain.ShowPrgMsg("原料站表状态为下线,请确认.... ", MainParent.MsgType.Error);
                            tboldmo.Focus();
                            tboldmo.SelectAll();
                            tbnewmo.Enabled = false;
                            return;
                        }
                   
                 

                        if (sStatus == "3")
                        {

                            sMain.ShowPrgMsg("原料站表确认正确,请刷入新料表 ", MainParent.MsgType.Incoming);
                            tbnewmo.Focus();
                            tbnewmo.SelectAll();
                            tbnewmo.Enabled =true;
                            return;
                        }

                    }
                    else
                    {
                        sMain.ShowPrgMsg("原料站表未上线使用,请直接找线边仓做首盘 ", MainParent.MsgType.Error);
                        tboldmo.Focus();
                        tboldmo.SelectAll();                    
                        tbnewmo.Enabled = false;
                    }
                }
                else
                {
                    sMain.ShowPrgMsg("原料站表格式错误,请重新刷入原料站表 ", MainParent.MsgType.Error);
                    tboldmo.Focus();
                    tboldmo.SelectAll();
                    tbnewmo.Enabled = false;
                }
                
            }
        }

        string sSEQ = "";
        string sMO = "";
        private bool CheckSeqMo(string sSEQMO)
        {
            try
            {
                string str = sSEQMO;
                int i = str.IndexOf(' ');
                sSEQ = str.Substring(0, i);
                sMO = str.Substring(i + 1, str.Length - i - 1);
                return true;
            }
            catch (Exception)
            {                         
                return false;
            }
        }

        string MachineCode = "";
        string sSIDE = "";
        private void tbnewmo_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tbnewmo.Text)) && (e.KeyValue == 13))
            {
                sSEQ = "";
                sMO = "";
                if (CheckSeqMo(tbnewmo.Text.Trim()))
                {                   
                    DataTable data = FrmBLL.ReleaseData.arrByteToDataTable(refWebSmtKpMaster.Instance.GetSmtKpMaster(sSEQ));

                    if (data.Rows.Count == 0)
                    {
                        sMain.ShowPrgMsg("ECN 错误,请刷入正确的料表", MainParent.MsgType.Error);
                        tbnewmo.Focus();
                        tbnewmo.SelectAll();
                        return;
                    }
                    else
                    {
                        MachineCode = data.Rows[0][1].ToString();
                        sSIDE = data.Rows[0][6].ToString();
                        if (MachineCode != OldMachineCode)
                        {
                            sMain.ShowPrgMsg("原料表与新料表机器编号不一致", MainParent.MsgType.Error);
                            tbnewmo.Focus();
                            tbnewmo.SelectAll();
                            return;
                        }
                        if (sSIDE != OldSide)
                        {
                            sMain.ShowPrgMsg("原料表与新料表面别不一致", MainParent.MsgType.Error);
                            tbnewmo.Focus();
                            tbnewmo.SelectAll();
                            return;
                        }
                        sMain.ShowPrgMsg("新料表确认OK", MainParent.MsgType.Incoming);
                        btok.Enabled = true;
                    }
                }
                else
                {
                    sMain.ShowPrgMsg("新料站表格式错误,请确认..... ", MainParent.MsgType.Error);
                    tbnewmo.Focus();
                    tbnewmo.SelectAll();
                }
            }
        }

        private void FrmMoMerge_Load(object sender, EventArgs e)
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
        }

        private DataTable Get_SmtWoMorge(string woid)
        {
            Dictionary<string, object>  dic = new Dictionary<string, object>();
            dic.Add("OLD_WOID", woid);
            return  FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtWoMerge.Instance.Get_Smt_WO_Merge(FrmBLL.ReleaseData.DictionaryToJson(dic), "MERGE_NUM"));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="woid"></param>
        /// <returns></returns>
        private DataTable Get_SmtWoMorge_NEW(string woid)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("NEW_WOID", woid);
            return FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtWoMerge.Instance.Get_Smt_WO_Merge(FrmBLL.ReleaseData.DictionaryToJson(dic), "MERGE_NUM"));

        }
    }
}
