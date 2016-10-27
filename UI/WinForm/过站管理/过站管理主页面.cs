using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SFIS_V2
{
    public partial class Frm_Station_Management : Office2007Form  //Form
    {
        public Frm_Station_Management( MainParent Frm )
        {
            InitializeComponent();
            mFrm = Frm;
        }
        MainParent mFrm;
        public string UserId = string.Empty;
        public string Password = string.Empty;
        public string UserName = string.Empty;
        private void Control_Add(Office2007Form form)
        {
            rtbmsg.Clear();
            foreach (Control ctl in PanelFrm.Controls)
            {
                if (ctl.Name == form.Name)
                {
                    return;
                }
            }
            foreach (Control ctl in PanelFrm.Controls)
            {
                (ctl as Office2007Form).Close();
                PanelFrm.Controls.Remove(ctl);
            }
           
            PanelFrm.Controls.Clear(); //移除所有控件
            form.TopLevel = false;  //设置为非顶级窗体
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;//设置窗体为非边框样式
            form.Dock = System.Windows.Forms.DockStyle.Fill;//设置样式是否填充整个panel
            PanelFrm.Controls.Add(form);//添加窗体
            PanelFrm.Text = form.Text;
            form.Show();//窗体运行
          
        }     

        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error } //文字输出类型
        public Color[] LogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };//文字颜色

        public void ShowMsg(mLogMsgType msgtype, string msg)
        {
            this.rtbmsg.Invoke(new EventHandler(delegate
            {
                this.rtbmsg.TabStop = false;
                rtbmsg.SelectedText = string.Empty;
                rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                rtbmsg.SelectionColor = LogMsgTypeColor[(int)msgtype];
                rtbmsg.AppendText(msg + "\n");
                rtbmsg.ScrollToCaret();
            }));
        }
        private void Frm_Station_Management_Load(object sender, EventArgs e)
        {
            UserId = mFrm.gUserInfo.userId;
            Password = mFrm.gUserInfo.pwd;
            UserName = mFrm.gUserInfo.username;
            rtbmsg.Text = "";
        }

        private void imbt_TestMainOnly_Click(object sender, EventArgs e)
        {      
            Frm_TEST_MAIN_ONLY tmo = new Frm_TEST_MAIN_ONLY(this);
            Control_Add(tmo);
            this.groupPanel1.Text = "TEST_MAIN_ONLY";
        }

        private void imbt_ReworkInput_Click(object sender, EventArgs e)
        {
            Frm_ReworkInput fri = new Frm_ReworkInput(this);
            Control_Add(fri);
            this.groupPanel1.Text = "重工投板";
        }

        private void imbt_InputSnFirst_Click(object sender, EventArgs e)
        {
            
            Frm_INPUT_SN_FIRST tmo = new Frm_INPUT_SN_FIRST(this);
            Control_Add(tmo);
            this.groupPanel1.Text = "INPUT_SN_FIRST";

        }

        private void imbt_Test_Input_Click(object sender, EventArgs e)
        {
            Frm_TEST_INPUT ftt = new Frm_TEST_INPUT(this);
              Control_Add(ftt);
              this.groupPanel1.Text = "TEST_INPUT";
        }

        private void imbt_ColorBoxPrint_Click(object sender, EventArgs e)
        {
            Frm_ColorBoxPrint fcbp = new Frm_ColorBoxPrint(this);
            Control_Add(fcbp);
            this.groupPanel1.Text = "过站打印标签";
        }

        private void imbt_assyfirst_Click(object sender, EventArgs e)
        {
            Frm_AssyFirst faf = new Frm_AssyFirst(this);
            Control_Add(faf);
            this.groupPanel1.Text = "ASSY_FIRST";
        }
    }
}
