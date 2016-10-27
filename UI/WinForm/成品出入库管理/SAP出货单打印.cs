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
    public partial class Frm_ShippingNotice :Office2007Form //Form
    {
        public Frm_ShippingNotice(MainParent mfm)
        {
            InitializeComponent();
            this.mFrm = mfm;
        }

        MainParent mFrm;
        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };
        /// <summary>
        /// 显示消息函数
        /// </summary>
        /// <param name="msgtype"></param>
        /// <param name="msg"></param>
        public void ShowMsg(mLogMsgType msgtype, string msg)
        {
            try
            {
                this.rtbmsg.Invoke(new EventHandler(delegate
                {
                    rtbmsg.TabStop = false;
                    rtbmsg.SelectedText = string.Empty;
                    //rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                    rtbmsg.SelectionColor = mLogMsgTypeColor[(int)msgtype];
                    rtbmsg.AppendText(msg + "\n");
                    rtbmsg.ScrollToCaret();
                }));
            }
            catch
            {
            }
        }
        private void Frm_ShippingNotice_Load(object sender, EventArgs e)
        {

        }

        private void tb_shipingNotice_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_shipingNotice.Text) && e.KeyCode == Keys.Enter)
            {
                ShowMsg(mLogMsgType.Incoming, "正在从SAP拉取数据...");
                DataTable mdtable = FrmBLL.ReleaseData.arrByteToDataTable(refWebSapConnector.Instance.Get_Z_RFC_LIPS(tb_shipingNotice.Text, ""));
                if (mdtable == null || mdtable.Rows.Count < 1)
                {
                    ShowMsg(mLogMsgType.Error, "该SAP出货单号无数据,请确认...");
                    return;
                }
                dgvshiping.DataSource = mdtable;

            }
        }
    }
}
