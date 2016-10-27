using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using LabelManager2;

namespace SFIS_V2
{
    public partial class Frm_PrintDataPartision : Office2007Form//Form
    {
        public Frm_PrintDataPartision(DataPartition frm, DataTable mdata)
        {
            InitializeComponent();
            this.mFdp = frm;
            this.mDatatable = mdata;
        }

        DataPartition mFdp = null;
        private DataTable mDatatable = null;
        private CheckBox[] mCheckBox = null;
        /// <summary>
        /// 实例化CodeSoft实例
        /// </summary>
        private LabelManager2.ApplicationClass lbl = null;
        /// <summary>
        /// CodeSoft文档
        /// </summary>
        private LabelManager2.Document mLibdoc = null;
        /// <summary>
        /// 存放标签类型名
        /// </summary>
        List<string> list = new List<string>();

        private void Frm_PrintDataPartision_Load(object sender, EventArgs e)
        {

            //DynamicControl();
            GetSerialType();
        }
        /// <summary>
        /// 获取该产品的序列类型添加到clb_serialtype
        /// </summary>
        private void GetSerialType()
        {
            this.ip_serialtype.Items.Clear();
            if (mDatatable.Rows.Count > 0)
            {
                for (int i = 0; i < mDatatable.Columns.Count; i++)
                {
                    if (mDatatable.Columns[i].ColumnName.ToUpper() != "WOID" && mDatatable.Columns[i].ColumnName.ToUpper() != "ESN")
                    {
                        list.Add(mDatatable.Columns[i].ColumnName.ToUpper());
                    }
                }
            }
            foreach (string item in list)
            {
                DevComponents.DotNetBar.CheckBoxItem cbi = null;
                this.ip_serialtype.Items.Add(cbi = new DevComponents.DotNetBar.CheckBoxItem
                {
                    Text = item,
                    Name = item
                });
            }
            this.ip_serialtype.Refresh();//刷新
        }
        /// <summary>
        /// 打开模板文件
        /// </summary>
        /// <returns></returns>
        private void OpenLabFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择模板文件";
            ofd.Filter = "(*.lab)|*.lab";
            ofd.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.lbi_path.Text = ofd.FileName;
                lbl = new LabelManager2.ApplicationClass();
                mLibdoc = lbl.Documents.Open(ofd.FileName, false);
            }
            else
            {
                return;
            }
            return;
        }

        private void bti_opentemplate_Click(object sender, EventArgs e)
        {
            try
            {
                OpenLabFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //    if (((CheckBox)sender).Checked == true && mLibdoc == null)
            //    {
            //        MessageBox.Show("请选择模板!");
            //        ((CheckBox)sender).Checked = false;
            //    }
            //    if (((CheckBox)sender).Checked)
            //    {
            //        string snType = ((CheckBox)sender).Text.Trim();
            //        PrintCartonBox(mDatatable, snType);
            //    }
        }
        /// <summary>
        /// 填充模板变量病打印标签
        /// </summary>
        /// <param name="dtPrint"></param>
        /// <param name="printNum"></param>
        /// <param name="printUserCartonId"></param>
        private string PrintLabel(DataTable dtPrint)
        {
            if (dtPrint == null || dtPrint.Rows.Count < 1)
                return "错误:没有需要打印的内容1,请检查..";
            if (this.mLibdoc == null)
                return "模板文件没有初始化";
            this.mLibdoc.ViewMode = enumViewMode.lppxViewModeSize;
            try
            {
                #region 处理填充器下的变量内容
                //使用需要打印的内容去填充模板变量
                int varCount = 0;
                varCount = dtPrint.Rows.Count;
                string sntype;
                if (varCount > this.mLibdoc.Variables.FormVariables.Count)
                    return "模板文件需要填充变量个数小于数据填充数量,请重新设置..";
                bool mflag = true;

                for (int i = 0; i < this.ip_serialtype.Items.Count; i++)
                {
                    if (((DevComponents.DotNetBar.CheckBoxItem)this.ip_serialtype.Items[i]).Checked)
                    {
                        sntype = this.ip_serialtype.Items[i].ToString();
                        DataTable dt=null;
                        if (mflag)
                        {
                            dtPrint.DefaultView.Sort = string.Format("{0} asc", sntype);
                            dt = dtPrint.DefaultView.ToTable();
                            mflag = false;
                        }

                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                            try
                            {
                                this.mLibdoc.Variables.FormVariables.Item(string.Format("{0}{1}", sntype, (x + 1).ToString())).Length = dt.Rows[x][sntype].ToString().Length; //_dicPrintContent[str].Rows[x]["snval"].ToString().Length;
                                this.mLibdoc.Variables.FormVariables.Item(string.Format("{0}{1}", sntype, (x + 1).ToString())).Value = dt.Rows[x][sntype].ToString();//_dicPrintContent[str].Rows[x]["snval"].ToString();
                            }
                            catch
                            {
                                return "模板有问题，请确认模板再打印...";
                            }
                        }
                    }
                }
                //开始打印
                this.mLibdoc.PrintDocument(1);
                this.ShowMsg(LogMsgType.Outgoing, "打印成功!!");
                return string.Empty;
                #endregion
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                //模板内容
                for (int y = 0; y < this.mLibdoc.Variables.Formulas.Count; y++)
                {
                    mLibdoc.Variables.Formulas.Item(y + 1).Prefix = string.Empty;
                    mLibdoc.Variables.Formulas.Item(y + 1).Expression = string.Format("upper(\"{0}\")", 0);
                }
                for (int z = 0; z < this.mLibdoc.Variables.FormVariables.Count; z++)
                {
                    this.mLibdoc.Variables.FormVariables.Item(z + 1).Prefix = string.Empty;
                    this.mLibdoc.Variables.FormVariables.Item(z + 1).Value = string.Empty;
                }
            }
        }

        private void bti_print_Click(object sender, EventArgs e)
        {
            string errStr = string.Empty;
            if (this.mLibdoc == null)
            {
                this.ShowMsg(LogMsgType.Warning, "请选择模板...");
                return;
            }
            if (this.ip_serialtype.Items.Count < 1)
            {
                this.ShowMsg(LogMsgType.Warning, "请勾选要打印的序列号类型...");
                return;
            }
            errStr = PrintLabel(mDatatable);
            if (!string.IsNullOrEmpty(errStr))
            {
                this.ShowMsg(LogMsgType.Error, errStr);
            }
        }

        #region
        private enum LogMsgType { Incoming, Outgoing, Normal, Warning, Error } //文字输出类型
        private Color[] LogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };//文字颜色 
        private void ShowMsg(LogMsgType msgtype, string msg)
        {
            this.rtb_msg.Invoke(new EventHandler(delegate
            {
                rtb_msg.TabStop = false;
                rtb_msg.SelectedText = string.Empty;
                rtb_msg.SelectionFont = new Font(rtb_msg.SelectionFont, FontStyle.Bold);
                //将枚举参数对应的基本类型值(默认为int，并从0开始)赋给数组的下标
                rtb_msg.SelectionColor = LogMsgTypeColor[(int)msgtype];
                rtb_msg.AppendText(msg + "\n");
                rtb_msg.ScrollToCaret();
            }));
        }

        #endregion
    }
}
