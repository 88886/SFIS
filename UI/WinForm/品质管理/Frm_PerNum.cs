using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RefWebService_BLL;
using DevComponents.DotNetBar;
using System.Threading;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace SFIS_V2
{
    public partial class Frm_PerNum : Office2007Form
    {
        public Frm_PerNum(MainParent mfm)
        {
            InitializeComponent();
            mFrm = mfm;
        }
        MainParent mFrm;

        #region 变量
        private Thread Thread_CTable;
        string Things;//操作事件
        string P_Model;//机型/模式
        string P_Type;//SIM类型/端口类型
        string Tac_Num;//号段头
        int S_Num;//起始号
        int E_Num;//结束号
        int Num_count;//数量
        string cmd_str;//数据库语句_号段绑定
        string cmd_Sta;//数据库语句_更新Model状态
        DataTable Dt_PhoneModel;//手机表
        DataTable Dt_PerNum;//未使用号段表
        bool Is_Model = false;//是否选用点取了绑定的号段
        DataTable Dt_PhoneModelNoLink;//没有绑定号段的手机型号
        DataTable Dt_NumImei;//IMEI号表
        DataTable Dt_ModeIsAte;//按照数通\移动分类获取的型号表
        string SimType;//Sim卡类型
        string UserID = "";//用户ID
        DataTable Dt_MiddleNum;//已用号段库中相关数据
        bool Is_section = false;//号段是否分段
        int Imei_Count = 0;//IMEI数量
        int Meid_Count = 0;//Meid数量
        int Mac_Count = 0;//Mac数量
        DataTable AllExtracNum;
        public string[] MAC = new string[] { };//导出时Mac数组
        public string[] IMEI = new string[] { };//导出时IMEI数组
        public string[] MEID = new string[] { };//导出时MEID数组

        string Context = "";//导出字符
        #endregion



        private void Frm_PerNum_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.mFrm.gUserInfo.rolecaption == "系统开发员")
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


            UserID = mFrm.gUserInfo.userId;
            Sel_PerNum();

            sel_PhoneModel();
            Bind_PhoneModel();
            Cmb_Model.SelectedIndex = 0;
            Cmb_NumType.SelectedIndex = 0;
            Bind_Customer();

            Rdb_SIM.Checked = true;
            Btn_Out.Visible = false;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabPage1)
            {
                sel_PhoneModel();
                Bind_PhoneModel();
            }
            if (e.TabPage == tabPage2)
            {
                Sel_PerNum();
                Bing_PerNum();
            }
            if (e.TabPage == tabPage3)
            {
                Sel_Num_Imei();
                Bind_LinkPhone();

                Bind_CmbBox();
            }
            if (e.TabPage == tabPage4)
            {

                Get_AllExtracNum();
                Bind_DG_Used();
            }
            if (e.TabPage == tabPage5)
            {
                Rdb_Single.Checked = true;
                Cmb_IsUsedPerNum.SelectedIndex = 0;
                Rdb_Date.Checked = true;
                Get_AllExtracNum();
                Bind_Dg_SelPerNum();
            }
            if (e.TabPage == tabPage6)
            {
                cmb_FuDo.SelectedIndex = 0;
                Rdb_LogDate.Checked = true;
                Sel_AllLog();
            }
        }

        #region 单列导出
        public string Single_OutTxt_Single(string PerType, string Project, string PerHead, int PerSta, int PerCount)
        {
            //类型//起始值//数量
            string New_PerNum = "";
            int Num = 0;
            if (PerType == "MAC")
            {
                Num = 12 - PerHead.Length;
            }
            else
            {
                Num = 6;
            }

            //New_PerNum.PadLeft(Num, '0');
            // Context = "";
            Context = "手机型号：" + Project + "\r\n";
            for (int i = 0; i < PerCount; i++)
            {
                New_PerNum = Get_NextNum(PerType, PerSta, i);
                New_PerNum = New_PerNum.PadLeft(Num, '0');
                if (PerType == "IMEI")
                {
                    New_PerNum = New_PerNum + Get_LastIMEI(PerHead + New_PerNum);
                }
                Context += PerHead + New_PerNum + "\r\n";

            }
            return Context;
        }


        public string Single_OutTxt_Double(string PerType, string Project, string PerHead, int PerSta, int PerCount)
        {
            string New_PerNum_1 = "";
            string New_PerNum_2 = "";
            // Context = "";
            int Num = 0;
            if (PerType == "MAC")
            {
                Num = 12 - PerHead.Length;
            }
            else
            {
                Num = 6;
            }
            Context = "手机型号：" + Project + "\r\n";
            for (int i = 0; i < PerCount; )
            {
                New_PerNum_1 = Get_NextNum(PerType, PerSta, i);
              New_PerNum_1=  New_PerNum_1.PadLeft(Num, '0');
                New_PerNum_2 = Get_NextNum(PerType, PerSta, i + 1);
               New_PerNum_2= New_PerNum_2.PadLeft(Num, '0');
                if (PerType == "IMEI")
                {
                    New_PerNum_1 = New_PerNum_1 + Get_LastIMEI(PerHead + New_PerNum_2);
                    New_PerNum_2 = New_PerNum_2 + Get_LastIMEI(PerHead + New_PerNum_2);
                }
                Context += PerHead + New_PerNum_1 + "," + PerHead + New_PerNum_2 + "\r\n";


                i += 2;
            }
            return Context;
        }

        #endregion



        #region 排列数组按手机卡和网端的数量
        public void Csinglecard(string[] ForMeid, string[] ForMac)
        {
            Context = "";
            string MeidHead = "Meid";
            string BTMacHead = "BTMac";
            string wifiMacHead = "WifiMac";
            MeidHead = MeidHead.PadLeft(14);
            BTMacHead = BTMacHead.PadLeft(12);
            wifiMacHead = wifiMacHead.PadLeft(12);
            Context = "手机型号：" + Cmb_Phone.SelectedValue + "\r\n";
            Context += MeidHead + "," + BTMacHead + "," + wifiMacHead + "\r\n";
            int i = 0;
            int j = 0;
            while (true)
            {

                Context += ForMeid[i] + "," + ForMac[j] + "," + ForMac[j + 1] + "\r\n";
                i++;
                j += 2;
                if (i == ForMeid.Length)
                {
                    break;
                }
            }
            //  return Context;
        }

        public void CGdoublecard(string[] ForImei, string[] ForMeid, string[] ForMac)
        {
            Context = "";
            string ImeiHead = "Imei";
            string MeidHead = "Meid";
            string BTMacHead = "BTMac";
            string wifiMacHead = "WifiMac";
            ImeiHead = ImeiHead.PadLeft(15);
            MeidHead = MeidHead.PadLeft(14);
            BTMacHead = BTMacHead.PadLeft(12);
            wifiMacHead = wifiMacHead.PadLeft(12);
            Context = "手机型号：" + Cmb_Phone.SelectedValue + "\r\n";
            Context += ImeiHead + "," + MeidHead + "," + BTMacHead + "," + wifiMacHead + "\r\n";
            int i = 0;
            int j = 0;
            while (true)
            {

                Context += ForImei[i] + "," + ForMeid[i] + "," + ForMac[j] + "," + ForMac[j + 1] + "\r\n";
                i++;
                j += 2;
                if (i == ForMeid.Length)
                {
                    break;
                }
            }
            //  return Context;
        }

        public void W_G_singleCard(string[] ForImei, string[] ForMac)
        {
            Context = "";
            string ImeiHead = "Imei";
            //string MeidHead = "Meid";
            string BTMacHead = "BTMac";
            string wifiMacHead = "WifiMac";
            ImeiHead = ImeiHead.PadLeft(15);
            // MeidHead = MeidHead.PadLeft(14);
            BTMacHead = BTMacHead.PadLeft(12);
            wifiMacHead = wifiMacHead.PadLeft(12);
            Context = "手机型号：" + Cmb_Phone.SelectedValue + "\r\n";
            Context += ImeiHead + "," + BTMacHead + "," + wifiMacHead + "\r\n";
            int i = 0;
            int j = 0;
            while (true)
            {

                Context += ForImei[i] + "," + ForMac[j] + "," + ForMac[j + 1] + "\r\n";
                i++;
                j += 2;
                if (i == ForImei.Length)
                {
                    break;
                }
            }
            //  return Context;
        }

        public void W_G_doubleCard(string[] ForImei, string[] ForMac)
        {
            Context = "";
            string Imei1Head = "Imei1";
            string Imei2Head = "Imei2";
            //string MeidHead = "Meid";
            string BTMacHead = "BTMac";
            string wifiMacHead = "WifiMac";
            Imei1Head = Imei1Head.PadLeft(15);
            Imei2Head = Imei2Head.PadLeft(15);
            // MeidHead = MeidHead.PadLeft(14);
            BTMacHead = BTMacHead.PadLeft(12);
            wifiMacHead = wifiMacHead.PadLeft(12);
            Context = "手机型号：" + Cmb_Phone.SelectedValue + "\r\n";
            Context += Imei1Head + "," + Imei2Head + "," + BTMacHead + "," + wifiMacHead + "\r\n";
            int i = 0;
            //int j = 0;
            while (true)
            {

                Context += ForImei[i] + "," + ForImei[i + 1] + "," + ForMac[i] + "," + ForMac[i + 1] + "\r\n";
                i += 2;
                // i += 2;
                if (i == ForImei.Length)
                {
                    break;
                }
            }
            // return Context;
        }

        public void ATE_MAC(string[] Mac)
        {
            Context = "";
            int i = 0;
            Context = "型号：" + Cmb_Phone.SelectedValue + "\r\n";
            while (true)
            {

                Context += Mac[i].Substring(0, 2) + "-" + Mac[i].Substring(2, 2) + "-" + Mac[i].Substring(4, 2) + "-" + Mac[i].Substring(6, 2) + "-" + Mac[i].Substring(8, 2) + "-" + Mac[i].Substring(10, 2) + "\r\n";
                i++;
                // i += 2;
                if (i == Mac.Length)
                {
                    break;
                }
            }
            // return Context;
        }
        #endregion

        #region 通用方法
        //获取下一位号码
        public string Get_NextNum(string PerType, int PerNum, int i)
        {
            string NexNum = "";
            if (PerType == "IMEI")
            {
                NexNum = (PerNum + i).ToString();
            }
            else
            {
                NexNum = Convert.ToString(PerNum + i, 16).ToUpper();
            }
            return NexNum;
        }


        //算IMEI第十五位
        public char Get_LastIMEI(string szIMEI)
        {
            int nLen = szIMEI.Length;
            int nTemp1 = 0;
            int nTemp2 = 0;
            char[] arrchar = szIMEI.ToCharArray();
            for (int i = 0; i < 7; i++)
            {
                nTemp1 += arrchar[2 * i] - '0';
                nTemp2 = (arrchar[2 * i + 1] - '0') * 2;
                nTemp1 += (nTemp2 / 10 + nTemp2 % 10);
            }
            int cd = (10 - nTemp1 % 10) % 10 + '0';
            return Convert.ToChar(cd);
        }

        //重定义DataGridView
        private void DisplayCol(DataGridView dgv, String dataPropertyName, String headerText, int width)
        {
            dgv.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn obj = new DataGridViewTextBoxColumn();
            obj.DataPropertyName = dataPropertyName;
            obj.HeaderText = headerText;
            obj.Name = dataPropertyName;
            obj.Width = width;
            obj.Resizable = DataGridViewTriState.True;
            dgv.Columns.AddRange(new DataGridViewColumn[] { obj });
        }



        //计算十六进制两数之间的差
        private int Cal_Num16(string SNum, string ENum)
        {
            int Snum_10 = Convert.ToInt32(SNum, 16);
            int Enum_10 = Convert.ToInt32(ENum, 16);

            return Enum_10 - Snum_10 + 1;
        }
        //创建Log
        private void Inser_Log(string Title, string Things, string User)
        {
            refWebtPer_Num.Instance.Inser_Log(Title, Things, User);
        }
        #endregion

        #region 选项卡《机型模式增加》
        private void Btn_ModelInser_Click(object sender, EventArgs e)
        {
            //refWebtPer_Num.Instance.inser_PhoneModel("","");
            Inser_PhoneModel();
            sel_PhoneModel();
            Bind_PhoneModel();



        }


        //机型模式新增
        public void Inser_PhoneModel()
        {
            char Is_Ate = '0';
            if (string.IsNullOrEmpty(Txt_Model.Text))
            {
                this.mFrm.ShowPrgMsg("机型/数通模式不能为空", MainParent.MsgType.Error);
                return;
            }
            else
            {
                P_Model = Txt_Model.Text;
            }
            if (Rdb_SIM.Checked)
            {
                if (Cmb_Model.SelectedIndex == 0)
                {
                    this.mFrm.ShowPrgMsg("请选择SIM卡类型", MainParent.MsgType.Error);
                    return;
                }
                else
                {
                    P_Type = Cmb_Model.SelectedItem.ToString();
                }
                Is_Ate = '0';
            }
            if (Rdb_PortNum.Checked)
            {
                if (Ud_PortNum.Value <= 0)
                {
                    this.mFrm.ShowPrgMsg("数通产品端口必须大于0", MainParent.MsgType.Error);
                    return;
                }
                else
                {
                    P_Type = "1/" + Ud_PortNum.Value.ToString();
                }
                Is_Ate = '1';
            }
            refWebtPer_Num.Instance.inser_PhoneModel(P_Model, P_Type, Is_Ate);
            Things = "新增模式：产品名称《" + P_Model + "》号段匹配模式：《" + P_Type + "》";
            Inser_Log("新增模式", Things, UserID);
            this.mFrm.ShowPrgMsg("新增成功！", MainParent.MsgType.Incoming);

            Txt_Model.Text = "";
            Cmb_Model.SelectedIndex = 0;
            // Rdb_SIM.Checked = true;
            Ud_PortNum.Value = 0;
        }

        //查询所有模式
        public DataTable sel_PhoneModel()
        {
            Dt_PhoneModel = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.Sel_PhoneModel());
            return Dt_PhoneModel;
        }

        public void Bind_PhoneModel()
        {
            for (int i = 0; i < Dt_PhoneModel.Rows.Count; i++)
            {
                if (Dt_PhoneModel.Rows[i][3].ToString() == "1" ||
                    Dt_PhoneModel.Rows[i][3].ToString() == "数通"
                    )
                {
                    Dt_PhoneModel.Rows[i][3] = "数通";
                }
                else
                {
                    Dt_PhoneModel.Rows[i][3] = "移动";
                }

                if (Dt_PhoneModel.Rows[i][4].ToString() == "1" ||
                    Dt_PhoneModel.Rows[i][4].ToString() == "是")
                {
                    Dt_PhoneModel.Rows[i][4] = "是";
                }
                else
                {
                    Dt_PhoneModel.Rows[i][4] = "否";
                }



            }
            // Dt_PhoneModel.Rows[1][3] = "数通";
            DG_PhoneModel.DataSource = Dt_PhoneModel;
            DG_PhoneModel.Columns.Clear();
            DisplayCol(DG_PhoneModel, "Mode", "手机型号/数通模式", 140);
            DisplayCol(DG_PhoneModel, "describe", "SIM卡类型/端口数量", 140);
            DisplayCol(DG_PhoneModel, "Is_Ate", "产品类型", 140);
            DisplayCol(DG_PhoneModel, "Is_Link", "是否绑有号段", 140);
        }
        #endregion

        #region 选项卡《号段新增》
        private void Btn_InserNum_Click(object sender, EventArgs e)
        {
            Inser_PerNum();
            Sel_PerNum();
            Bing_PerNum();
           

        }
        //插入号段
        public void Inser_PerNum()
        {
            string IsWDK = "";//公司名称
            string PerType;//号段类型
            PerType = Cmb_NumType.SelectedItem.ToString();
            if (Cmb_NumType.SelectedIndex == 0)
            {
                this.mFrm.ShowPrgMsg("请选择相应的号段类型！", MainParent.MsgType.Error);
                return;
            }

            // IMEI
            if (Cmb_NumType.SelectedIndex == 1)
            {
                Tac_Num = Txt_TacNum.Text;
                S_Num = 0;
                E_Num = 999999;
                Num_count = 1000000;
            }
            //MEID/MAC
            if (Cmb_NumType.SelectedIndex == 2 ||
                Cmb_NumType.SelectedIndex == 3)
            {
                Tac_Num = Txt_TacNum.Text;
                S_Num = 0;
                E_Num = 16777215;
                Num_count = 16777216;
            }

            if (Cmb_NumType.SelectedIndex == 1 ||
                Cmb_NumType.SelectedIndex == 2)
            {
                if (Tac_Num.Length != 8)
                {
                    this.mFrm.ShowPrgMsg("IMEI/MEID的号码头为8位，请填写完整！", MainParent.MsgType.Error);
                    return;
                }

            }

            if (Cmb_NumType.SelectedIndex == 3)
            {

                if (Tac_Num.Length < 6)
                {
                    this.mFrm.ShowPrgMsg("MAC的号码头为6位，请填写完整！", MainParent.MsgType.Error);
                    return;
                }
            }
            DataTable Is_Inser = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.Sel_SingelNum(Tac_Num));

            if (Is_Inser.Rows.Count > 0)
            {
                this.mFrm.ShowPrgMsg("这个号段已经新增过", MainParent.MsgType.Error);
                return;
            }

            if (Ck_Isdefault.Checked)
            {
                //if (Cmb_NumType.SelectedIndex == 1)
                //{
                //    S_Num = Txt_SNum.Text;
                //    E_Num = Txt_ENum.Text;
                //}
                if (string.IsNullOrEmpty(Txt_SNum.Text) ||
                    string.IsNullOrEmpty(Txt_ENum.Text))
                {
                    this.mFrm.ShowPrgMsg("请填写你的起始值和结束值", MainParent.MsgType.Error);
                    return;
                }
                if (Cmb_NumType.SelectedIndex == 2 ||
               Cmb_NumType.SelectedIndex == 3)
                {
                    S_Num = Convert.ToInt32(Txt_SNum.Text, 16);
                    E_Num = Convert.ToInt32(Txt_ENum.Text, 16);
                    // Num_count = Cal_Num16(S_Num, E_Num);

                }
                else
                {
                    S_Num = int.Parse(Txt_SNum.Text);
                    E_Num = int.Parse(Txt_ENum.Text);

                }
                Num_count = E_Num - S_Num + 1;
            }
            //if (Rdb_FX.Checked)
            //{
                IsWDK =cmb_Customer.SelectedValue.ToString();
           // }
            refWebtPer_Num.Instance.inser_PerNum(PerType, Tac_Num, S_Num, E_Num, Num_count, IsWDK);

            Things = "新增号段：号段头《" + Tac_Num + "》起始值：《" + S_Num.ToString() + "》结束值：《" + E_Num.ToString() + "》";
            Inser_Log("新增号段", Things, UserID);
            this.mFrm.ShowPrgMsg("新增号段成功", MainParent.MsgType.Incoming);
            Txt_TacNum.Text = "";
        }

        //查询号段
        public DataTable Sel_PerNum()
        {
            Dt_PerNum = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.sel_PerNum());
            return Dt_PerNum;

        }
        //绑定DG_PerNum
        public void Bing_PerNum()
        {
            DG_PerNum.DataSource = Dt_PerNum;
            DG_PerNum.Columns.Clear();
            DisplayCol(DG_PerNum, "PerType", "号段类型", 100);
            DisplayCol(DG_PerNum, "Perhead", "号段头", 100);
            DisplayCol(DG_PerNum, "SNum", "起始值", 100);
            DisplayCol(DG_PerNum, "ENum", "结束值", 100);
            DisplayCol(DG_PerNum, "PerCount", "号段数量", 100);
            DisplayCol(DG_PerNum, "IsWDK", "号段归属", 100);
            DisplayCol(DG_PerNum, "InDate", "记录日期", 100);

        }
        public void Bind_Customer()
        {
            DataTable Db_Customer = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.Sel_CustomerInfo());
            cmb_Customer.DataSource = Db_Customer.Copy();
            cmb_Customer.ValueMember = "customername";
            cmb_Customer.DisplayMember = "customername";
            cmb_Customer.SelectedIndex = 0;


            cmb_Company.DataSource = Db_Customer.Copy();
            cmb_Company.ValueMember = "customername";
            cmb_Company.DisplayMember = "customername";
            cmb_Company.SelectedIndex = 0;
        }
        #endregion

        #region 选项卡《关联手机》



        public void Bind_CmbBox()
        {

            Sel_PhoneModelNoLink();
            //Cmb_Phone1.Items.Clear();
            //Cmb_Phone2.Items.Clear();
            //Cmb_Phone3.Items.Clear();
            //Cmb_Phone4.Items.Clear();
            //Cmb_Phone5.Items.Clear();

            //foreach (DataRow dr in Dt_PhoneModel.Rows)
            //{

            //  Cmb_Phone1.Items.Add(dr[0].ToString());
            Cmb_Phone1.DataSource = Dt_PhoneModelNoLink.Copy();
            Cmb_Phone1.ValueMember = "Mode";
            Cmb_Phone1.DisplayMember = "Mode";

            // Cmb_Phone2.Items.Add(dr[0].ToString());
            Cmb_Phone2.DataSource = Dt_PhoneModelNoLink.Copy();
            Cmb_Phone2.ValueMember = "Mode";
            Cmb_Phone2.DisplayMember = "Mode";

            //  Cmb_Phone3.Items.Add(dr[0].ToString());
            Cmb_Phone3.DataSource = Dt_PhoneModelNoLink.Copy();
            Cmb_Phone3.ValueMember = "Mode";
            Cmb_Phone3.DisplayMember = "Mode";

            // Cmb_Phone4.Items.Add(dr[0].ToString());
            Cmb_Phone4.DataSource = Dt_PhoneModelNoLink.Copy();
            Cmb_Phone4.ValueMember = "Mode";
            Cmb_Phone4.DisplayMember = "Mode";

            // Cmb_Phone5.Items.Add(dr[0].ToString());
            Cmb_Phone5.DataSource = Dt_PhoneModelNoLink.Copy();
            Cmb_Phone5.ValueMember = "Mode";
            Cmb_Phone5.DisplayMember = "Mode";
        }

        //获取未绑定模式
        public void Sel_PhoneModelNoLink()
        {
            Dt_PhoneModelNoLink = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.sel_PhoneModelNoLink());
        }

        //获取所有IMEI号段
        public void Sel_Num_Imei()
        {
            Dt_NumImei = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.Sel_NumwithType("IMEI"));
        }

        // 绑定DG_LinkPhone
        public void Bind_LinkPhone()
        {

            DG_LinkPhone.DataSource = Dt_NumImei;
            DG_LinkPhone.Columns.Clear();
            DisplayCol(DG_LinkPhone, "Perhead", "号段头", 100);
            DisplayCol(DG_LinkPhone, "Model1", "关联手机1", 100);
            DisplayCol(DG_LinkPhone, "Model2", "关联手机2", 100);
            DisplayCol(DG_LinkPhone, "Model3", "关联手机3", 100);
            DisplayCol(DG_LinkPhone, "Model4", "关联手机4", 100);
            DisplayCol(DG_LinkPhone, "Model5", "关联手机5", 100);
            DisplayCol(DG_LinkPhone, "InDate", "记录日期", 100);

        }

        //DG_LinkPhone单击事件
        private void DG_LinkPhone_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            Txt_LinkNum.Text = DG_LinkPhone[0, e.RowIndex].Value.ToString();
            if (!string.IsNullOrEmpty(DG_LinkPhone[1, e.RowIndex].Value.ToString()))
            {
                Cmb_Phone1.SelectedValue = DG_LinkPhone[1, e.RowIndex].Value.ToString();
                Ck_Phone1.Checked = true;
                Ck_Phone1.Enabled = false;
                Cmb_Phone1.Enabled = false;
            }
            else
            {

                Ck_Phone1.Checked = false;
                Ck_Phone1.Enabled = true;
                Cmb_Phone1.Enabled = true;
                Cmb_Phone1.SelectedIndex = 0;
            }

            if (!string.IsNullOrEmpty(DG_LinkPhone[2, e.RowIndex].Value.ToString()))
            {
                Cmb_Phone2.SelectedValue = DG_LinkPhone[2, e.RowIndex].Value.ToString();
                Ck_Phone2.Checked = true;
                Ck_Phone2.Enabled = false;
                Cmb_Phone2.Enabled = false;
            }
            else
            {

                Ck_Phone2.Checked = false;
                Ck_Phone2.Enabled = true;
                Cmb_Phone2.Enabled = true;
                Cmb_Phone2.SelectedIndex = 0;
            }

            if (!string.IsNullOrEmpty(DG_LinkPhone[3, e.RowIndex].Value.ToString()))
            {
                Cmb_Phone3.SelectedValue = DG_LinkPhone[3, e.RowIndex].Value.ToString();
                Ck_Phone3.Checked = true;
                Ck_Phone3.Enabled = false;
                Cmb_Phone3.Enabled = false;

            }
            else
            {

                Ck_Phone3.Checked = false;
                Ck_Phone3.Enabled = true;
                Cmb_Phone3.Enabled = true;
                Cmb_Phone3.SelectedIndex = 0;
            }

            if (!string.IsNullOrEmpty(DG_LinkPhone[4, e.RowIndex].Value.ToString()))
            {
                Cmb_Phone4.SelectedValue = DG_LinkPhone[4, e.RowIndex].Value.ToString();
                Ck_Phone4.Checked = true;
                Ck_Phone4.Enabled = false;
                Cmb_Phone4.Enabled = false;
            }
            else
            {

                Ck_Phone4.Checked = false;
                Ck_Phone4.Enabled = true;
                Cmb_Phone4.Enabled = true;
                Cmb_Phone4.SelectedIndex = 0;
            }

            if (!string.IsNullOrEmpty(DG_LinkPhone[5, e.RowIndex].Value.ToString()))
            {
                Cmb_Phone5.SelectedValue = DG_LinkPhone[5, e.RowIndex].Value.ToString();
                Ck_Phone5.Checked = true;
                Ck_Phone5.Enabled = false;
                Cmb_Phone5.Enabled = false;
            }
            else
            {

                Ck_Phone5.Checked = false;
                Ck_Phone5.Enabled = true;
                Cmb_Phone5.Enabled = true;
                Cmb_Phone5.SelectedIndex = 0;
            }

        }

        //生成绑定语句
        private void Link_Phone()
        {
            int Cmd_Len;
            List<string> ModelList = new List<string>();
            Things = "号段绑定《";
            cmd_str = " UPDATE [t_PerNum]  SET ";
            cmd_Sta = "UPDATE t_PhoneMode SET Is_Link = '1' where ";
            if (Ck_Phone1.Checked && Ck_Phone1.Enabled)
            {
                cmd_str += "Model1='" + Cmb_Phone1.SelectedValue + "',";
                cmd_Sta += "Mode='" + Cmb_Phone1.SelectedValue + "' or ";
                Is_Model = true;
                ModelList.Add(Cmb_Phone1.SelectedValue.ToString());
                Things += "模式1：" + Cmb_Phone1.SelectedValue;
            }

            if (Ck_Phone2.Checked && Ck_Phone2.Enabled)
            {
                cmd_str += "Model2='" + Cmb_Phone2.SelectedValue + "',";
                cmd_Sta += "Mode='" + Cmb_Phone2.SelectedValue + "' or ";
                Is_Model = true;
                ModelList.Add(Cmb_Phone2.SelectedValue.ToString());
                Things += "模式2：" + Cmb_Phone2.SelectedValue;
            }

            if (Ck_Phone3.Checked && Ck_Phone3.Enabled)
            {
                cmd_str += "Model3='" + Cmb_Phone3.SelectedValue + "',";
                cmd_Sta += "Mode='" + Cmb_Phone3.SelectedValue + "' or ";
                Is_Model = true;
                ModelList.Add(Cmb_Phone3.SelectedValue.ToString());
                Things += "模式3：" + Cmb_Phone3.SelectedValue;

            }

            if (Ck_Phone4.Checked && Ck_Phone4.Enabled)
            {
                cmd_str += "Model4='" + Cmb_Phone4.SelectedValue + "',";
                cmd_Sta += "Mode='" + Cmb_Phone4.SelectedValue + "' or ";
                Is_Model = true;
                ModelList.Add(Cmb_Phone4.SelectedValue.ToString());
                Things += "模式4：" + Cmb_Phone4.SelectedValue;

            }

            if (Ck_Phone5.Checked && Ck_Phone5.Enabled)
            {
                cmd_str += "Model5='" + Cmb_Phone5.SelectedValue + "',";
                cmd_Sta += "Mode='" + Cmb_Phone5.SelectedValue + "' or ";
                Is_Model = true;
                ModelList.Add(Cmb_Phone5.SelectedValue.ToString());
                Things += "模式5：" + Cmb_Phone5.SelectedValue;

            }

            Cmd_Len = cmd_str.Length;
            cmd_str = cmd_str.Substring(0, Cmd_Len - 1);
            Cmd_Len = cmd_Sta.Length;
            cmd_Sta = cmd_Sta.Substring(0, Cmd_Len - 3);
            cmd_str += "WHERE Perhead='" + Txt_LinkNum.Text + "'";
            Things += "号段：" + Txt_LinkNum.Text + "》";


            for (int i = 0; i < ModelList.Count; i++)
            {
                for (int j = i + 1; j < ModelList.Count; j++)
                {
                    if (ModelList[i] == ModelList[j])
                    {
                        Is_Model = false;
                        return;
                    }
                }
            }


        }
        private void Btn_LinkPhone_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Txt_LinkNum.Text))
            {
                this.mFrm.ShowPrgMsg("请选择要绑定的号段", MainParent.MsgType.Error);
                return;
            }

            Link_Phone();

            if (!Is_Model)
            {
                this.mFrm.ShowPrgMsg("没有勾选你要绑定的型号或者勾选的型号中存在重复项", MainParent.MsgType.Error);
                return;
            }
            refWebtPer_Num.Instance.Upd_Per_Model(cmd_str);
            refWebtPer_Num.Instance.Upd_Per_Model(cmd_Sta);

            Inser_Log("号段绑定", Things, UserID);
            Sel_Num_Imei();
            Bind_LinkPhone();
            Bind_CmbBox();
            Txt_LinkNum.Text = "";
            Ck_Phone1.Checked = false;
            Ck_Phone1.Enabled = true;
            Cmb_Phone1.Enabled = true;
            Cmb_Phone1.SelectedIndex = 0;

            Ck_Phone2.Checked = false;
            Ck_Phone2.Enabled = true;
            Cmb_Phone2.Enabled = true;
            Cmb_Phone2.SelectedIndex = 0;

            Ck_Phone3.Checked = false;
            Ck_Phone3.Enabled = true;
            Cmb_Phone3.Enabled = true;
            Cmb_Phone3.SelectedIndex = 0;

            Ck_Phone4.Enabled = true;
            Ck_Phone4.Checked = false;
            Cmb_Phone4.Enabled = true;
            Cmb_Phone4.SelectedIndex = 0;

            Ck_Phone5.Checked = false;
            Ck_Phone5.Enabled = true;
            Cmb_Phone5.Enabled = true;
            Cmb_Phone5.SelectedIndex = 0;
            this.mFrm.ShowPrgMsg("号段绑定成功", MainParent.MsgType.Incoming);
        }
        #endregion

        #region 选项卡《抽取号段》
        private void Btn_Setting_Click(object sender, EventArgs e)
        {
            //Inser_Log("Test", "test", "fx001111");
            Is_section = false;
              string sett = @"^[0-9]*$";
            string Count=Txt_PhoCount.Text;
            if (cmb_Company.SelectedItem == null ||
                cmd_IsAte.SelectedItem == null ||
               Cmb_Phone.SelectedValue == null ||
                string.IsNullOrEmpty(Txt_PhoCount.Text)
                )
            {
                this.mFrm.ShowPrgMsg("请设定必要参数", MainParent.MsgType.Error);
                return;
            }
            bool S = Regex.IsMatch(Count, sett);
            if(!S)
            {
                this.mFrm.ShowPrgMsg("数量必须是数值（0~9）", MainParent.MsgType.Error);
                return;
            }

            Bind_CmbNumType();
            Txt_TypeShow.Text = cmb_Company.SelectedItem + "号段——" + cmd_IsAte.SelectedItem + "项目——" + Cmb_Phone.SelectedValue + "制式——" + Dt_ModeIsAte.Rows[Cmb_Phone.SelectedIndex][1].ToString()
                + "——投产数量：" + Count;
            cmb_Company.Enabled = false;
            cmd_IsAte.Enabled = false;
            Cmb_Phone.Enabled = false;
            Txt_PhoCount.Enabled = false;


        }
        //配置不同的号段类型
        public void Bind_CmbNumType()
        {
            Cmb_PerNumType.Items.Clear();
            Cmb_PerNumType.Items.Add("==请选择==");
            //移动
            SimType = Dt_ModeIsAte.Rows[Cmb_Phone.SelectedIndex][1].ToString();
            if (cmd_IsAte.SelectedIndex == 0)
            {

                if (SimType == "C（单卡）")
                {
                    Cmb_PerNumType.Items.Add("MEID");
                    Cmb_PerNumType.Items.Add("MAC");
                }
                if (SimType == "C+G（双卡）")
                {
                    Cmb_PerNumType.Items.Add("IMEI");
                    Cmb_PerNumType.Items.Add("MEID");
                    Cmb_PerNumType.Items.Add("MAC");
                }
                if (SimType == "W（单卡）" || SimType == "W（双卡）" || SimType == "G（单卡）" || SimType == "G（双卡）")
                {
                    Cmb_PerNumType.Items.Add("IMEI");
                    Cmb_PerNumType.Items.Add("MAC");
                }
                if (SimType == "W（双卡）" || SimType == "G（双卡）")
                {
                    Imei_Count = int.Parse(Txt_PhoCount.Text) * 2;
                }
                else
                {
                    Imei_Count = int.Parse(Txt_PhoCount.Text);
                }
                Meid_Count = int.Parse(Txt_PhoCount.Text);
                Mac_Count = int.Parse(Txt_PhoCount.Text) * 2;
            }
            else
            {
                //数通
                Cmb_PerNumType.Items.Add("MAC");
                int multiple = int.Parse(SimType.Substring(2));
                Mac_Count = int.Parse(Txt_PhoCount.Text) * multiple;
            }


            IMEI = new string[Imei_Count];
            MEID = new string[Meid_Count];
            MAC = new string[Mac_Count];


            Cmb_PerNumType.SelectedIndex = 0;
        }

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            cmb_Company.Enabled = true;
            cmd_IsAte.Enabled = true;
            Cmb_Phone.Enabled = true;
            Txt_TypeShow.Text = "";
            Txt_PhoCount.Text = "";
            Txt_PhoCount.Enabled = true;
        }

        private void cmd_IsAte_SelectedIndexChanged(object sender, EventArgs e)
        {
            char IsAte = '0';
            if (cmd_IsAte.SelectedIndex == 1)//= (char);
            {
                IsAte = '1';
            }

            Dt_ModeIsAte = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.sel_PhoneByIsAte(IsAte));
            Cmb_Phone.DataSource = Dt_ModeIsAte;
            Cmb_Phone.ValueMember = "Mode";
            Cmb_Phone.DisplayMember = "Mode"; 
        }
        //绑定剩余号段数据
        public void Bind_NoPerNum()
        {

            //Dt_PerNum
            DataTable dt_SumExtractNum = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.Sel_SumExtractNum());

            DataTable dt_NewPerNum = Cal_PerNum(Dt_PerNum, dt_SumExtractNum);

           
            DG_NoUsed.DataSource = dt_NewPerNum;
            DG_NoUsed.Columns.Clear();
            DisplayCol(DG_NoUsed, "PerType", "号段类型", 100);
            DisplayCol(DG_NoUsed, "Perhead", "号段头", 100);
            DisplayCol(DG_NoUsed, "SNum", "起始值", 100);
            DisplayCol(DG_NoUsed, "ENum", "结束值", 100);
            DisplayCol(DG_NoUsed, "PerCount", "号段数量", 100);
            DisplayCol(DG_NoUsed, "IsWDK", "号段归属", 100);
            DisplayCol(DG_NoUsed, "Model1", "关联手机1", 100);
            DisplayCol(DG_NoUsed, "Model2", "关联手机2", 100);
            DisplayCol(DG_NoUsed, "Model3", "关联手机3", 100);
            DisplayCol(DG_NoUsed, "Model4", "关联手机4", 100);
            DisplayCol(DG_NoUsed, "Model5", "关联手机5", 100);
            DisplayCol(DG_NoUsed, "InDate", "记录日期", 100);

        }

        /// <summary>
        /// 计算剩余号段数量
        /// </summary>
        /// <param name="dt1">原始数据</param>
        /// <param name="dt2">已用数据</param>
        /// <returns></returns>
        public DataTable Cal_PerNum(DataTable dt1, DataTable dt2)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    if (dt1.Rows[i][1].ToString() == dt2.Rows[j][0].ToString())
                    {
                        // Data2.Rows[j][4] = int.Parse(Data2.Rows[j][4].ToString()) - Count;
                        dt1.Rows[i][4] = int.Parse(dt1.Rows[i][4].ToString()) - int.Parse(dt2.Rows[j][1].ToString());
                    }
                }

            }
            return dt1;
        }



        private void Cmb_Phone_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Cmb_PerNumType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //移动
            //数通
            if (Cmb_PerNumType.SelectedIndex == 0)
            {
                return;
            }

            if (cmd_IsAte.SelectedIndex == 0)
            {
                if (Cmb_PerNumType.SelectedItem.ToString() == "IMEI")
                    Dt_PerNum = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.sel_PerNumByTypeMode(Cmb_PerNumType.SelectedItem.ToString(), Cmb_Phone.SelectedValue.ToString()));
                else
                    Dt_PerNum = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.Sel_PerNumByTypeWDK(Cmb_PerNumType.SelectedItem.ToString(), cmb_Company.SelectedValue.ToString()));
                Bind_NoPerNum();
            }
            else
            {

                Dt_PerNum = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.Sel_PerNumByTypeWDK(Cmb_PerNumType.SelectedItem.ToString(), cmb_Company.SelectedValue.ToString()));
                Bind_NoPerNum();
            }
        }

        private void DG_NoUsed_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            int PerNumCount = int.Parse(DG_NoUsed[4, e.RowIndex].Value.ToString());


            //Imei_Count = int.Parse(Txt_PhoCount.Text); Meid_Count = int.Parse(Txt_PhoCount.Text);
            //        Mac_Count = int.Parse(Txt_PhoCount.Text) * 2;
            if (Cmb_PerNumType.SelectedItem.ToString() == "IMEI")
            {
                if (PerNumCount < Imei_Count)
                {
                    this.mFrm.ShowPrgMsg("此号段数量不足请选择其他号段", MainParent.MsgType.Error);
                    return;
                }
            }
            if (Cmb_PerNumType.SelectedItem.ToString() == "MEID")
            {
                if (PerNumCount < Meid_Count)
                {
                    this.mFrm.ShowPrgMsg("此号段数量不足请选择其他号段", MainParent.MsgType.Error);
                    return;
                }
            }

            if (Cmb_PerNumType.SelectedItem.ToString() == "MAC")
            {
                if (PerNumCount < Mac_Count)
                {
                    this.mFrm.ShowPrgMsg("此号段数量不足请选择其他号段", MainParent.MsgType.Error);
                    return;
                }
            }
            Txt_PerNum.Text = DG_NoUsed[1, e.RowIndex].Value.ToString();
        }

        private void Btn_GetPerNum_Click(object sender, EventArgs e)
        {

            if (Imei_Count == 0 &&
             Meid_Count == 0 &&
             Mac_Count == 0)
            {
                this.mFrm.ShowPrgMsg("请先设定号段数量", MainParent.MsgType.Error);
                return;
            }



            if (Cmb_PerNumType.SelectedIndex == 0 ||
                string.IsNullOrEmpty(Txt_PerNum.Text) ||
                string.IsNullOrEmpty(Txt_ToUse.Text))
            {
                this.mFrm.ShowPrgMsg("请选择号段类型并选择点选相应号段//并填写用处", MainParent.MsgType.Error);
                return;
            }
            //查询数据库相应号段已抽取号段

            //在本地比查看是否中间含有空闲号段

            //抽取时悠闲使用空闲号段
            //空闲号段数量大于等于抽取号段数量  直接在空闲号段中抽取
            //空闲号段数量小于抽取号段数量      抽取空闲号段后  再从后面的空闲号段抽取剩余部分
            //



            //W双卡
            //G双卡
            //int Imei_Count;//IMEI数量
            //int Meid_Count;//Meid数量
            //int Mac_Count;//Mac数量
            if (Cmb_PerNumType.SelectedItem.ToString() == "IMEI")
            {
                Get_PerNum(Imei_Count);
            }
            if (Cmb_PerNumType.SelectedItem.ToString() == "MEID")
            {
                Get_PerNum(Meid_Count);
            }
            if (Cmb_PerNumType.SelectedItem.ToString() == "MAC")
            {
                Get_PerNum(Mac_Count);
            }


            if (cmd_IsAte.SelectedIndex == 0)
            {
                if (SimType == "C（单卡）")
                {
                    //Cmb_PerNumType.Items.Add("MEID");
                    //Cmb_PerNumType.Items.Add("MAC");
                    if (!string.IsNullOrEmpty(MEID[0]) && !string.IsNullOrEmpty(MAC[0]))
                    {
                        Btn_Out.Visible = true;
                        Btn_GetPerNum.Enabled = false;
                    }
                }
                if (SimType == "C+G（双卡）")
                {
                    //Cmb_PerNumType.Items.Add("IMEI");
                    //Cmb_PerNumType.Items.Add("MEID");
                    //Cmb_PerNumType.Items.Add("MAC");
                    if (!string.IsNullOrEmpty(IMEI[0]) && !string.IsNullOrEmpty(MEID[0]) && !string.IsNullOrEmpty(MAC[0]))
                    {
                        Btn_Out.Visible = true;
                        Btn_GetPerNum.Enabled = false;
                    }
                }
                if (SimType == "W（单卡）" || SimType == "W（双卡）" || SimType == "G（单卡）" || SimType == "G（双卡）")
                {
                    //Cmb_PerNumType.Items.Add("IMEI");
                    //Cmb_PerNumType.Items.Add("MAC");
                    if (!string.IsNullOrEmpty(IMEI[0]) && !string.IsNullOrEmpty(MAC[0]))
                    {
                        Btn_Out.Visible = true;
                        Btn_GetPerNum.Enabled = false;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(MAC[0]))
                {
                    Btn_Out.Visible = true;
                    Btn_GetPerNum.Enabled = false;
                }
            }

            Txt_PerNum.Text = "";
            Txt_ToUse.Text = "";

            DG_NoUsed.Columns.Clear();
            Get_AllExtracNum();
            Bind_DG_Used();
            this.mFrm.ShowPrgMsg("抽取成功", MainParent.MsgType.Incoming);

        }

        //查询已占用号段并算出其中是否有空闲号段
        public ArrayList Get_MiddleNum()
        {
            ArrayList Mid_List = new ArrayList();

            string Head = Txt_PerNum.Text;
            string str_Sta = "";
            string str_End = "";

            int int_Sta = 0;
            int int_End = 0;

            int Mid_count;
            Dt_MiddleNum = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.Sel_ExtractNumByHead(Head));
            S_Num = int.Parse(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.Sel_SingelNum(Txt_PerNum.Text)).Rows[0][0].ToString());
            if (Dt_MiddleNum.Rows.Count == 0)
            {
                return Mid_List;
            }

            #region 因首次抽取就出错而引起的号段空余
            if (int.Parse(Dt_MiddleNum.Rows[0][4].ToString()) != S_Num)
            {
                int[] Str_MidInfo = new int[3];
                //if (Cmb_PerNumType.SelectedItem.ToString() == "IMEI")
                //{
                str_Sta = "0";
                str_End = Dt_MiddleNum.Rows[0][4].ToString();
                int_Sta = 0;
                int_End = int.Parse(Dt_MiddleNum.Rows[0][4].ToString()) - 1;

                Mid_count = int_End - int_Sta + 1;

                Str_MidInfo[0] = int_Sta;
                Str_MidInfo[1] = int_End;
                Str_MidInfo[2] = Mid_count;

                //}
                //else
                //{
                //    str_Sta = "0";
                //    str_End = Dt_MiddleNum.Rows[0][1].ToString();
                //    int_Sta = 0;
                //    int_End = Convert.ToInt32(Dt_MiddleNum.Rows[0][1].ToString(), 16) - 1;


                //    Mid_count = int_End - int_Sta + 1;

                //    Str_MidInfo[0] = int_Sta;
                //    Str_MidInfo[1] = int_End;
                //    Str_MidInfo[2] = Mid_count;
                //}
                Mid_List.Add(Str_MidInfo);
            }
            #endregion


            #region 抽取出错引起的号段空余
            for (int i = 0; i < Dt_MiddleNum.Rows.Count - 1; i++)
            {
                int[] Str_MidInfo2 = new int[3];
                //if (Cmb_PerNumType.SelectedItem.ToString() == "IMEI")
                //{
                str_Sta = Dt_MiddleNum.Rows[i][5].ToString();//End
                str_End = Dt_MiddleNum.Rows[i + 1][4].ToString();//Sta
                int_Sta = int.Parse(Dt_MiddleNum.Rows[i][5].ToString());
                int_End = int.Parse(Dt_MiddleNum.Rows[i + 1][4].ToString()) - 1;

                //}
                //else
                //{
                //    str_Sta = Dt_MiddleNum.Rows[i][2].ToString();//End
                //    str_End = Dt_MiddleNum.Rows[i + 1][1].ToString();//Sta

                //    int_Sta = Convert.ToInt32(Dt_MiddleNum.Rows[i][2].ToString(), 16);
                //    int_End = Convert.ToInt32(Dt_MiddleNum.Rows[i + 1][1].ToString(), 16) - 1;
                //}

                if (int_Sta != int_End)
                {
                    Mid_count = int_End - int_Sta;

                    //if (Cmb_PerNumType.SelectedItem.ToString() == "IMEI")
                    //{
                    Str_MidInfo2[0] = (int_Sta + 1);
                    Str_MidInfo2[1] = int_End;
                    Str_MidInfo2[2] = Mid_count;
                    //}
                    //else
                    //{
                    //    Str_MidInfo2[0] = Convert.ToString(int_Sta + 1, 16);
                    //    Str_MidInfo2[1] = Convert.ToString(int_End, 16);
                    //    Str_MidInfo2[2] = Mid_count.ToString();
                    //}
                    Mid_List.Add(Str_MidInfo2);
                }

            }
            #endregion

            return Mid_List;
        }

        //抽取号段
        public void Get_PerNum(int NePerCount)
        {
            // String PerType=Cmb_PerNumType.SelectedItem.ToString();
            ArrayList List = Get_MiddleNum();
            int PerCount = 0;


            int count = NePerCount;
            int NewEnd = 0;

            string Project = Cmb_Phone.SelectedValue.ToString();
            string PerType = Cmb_PerNumType.SelectedItem.ToString();
            while (List.Count > 0 & count != 0)
            {
                int[] PerList = (int[])List[0];

                //抽取
                int perListcount = PerList[2];
                if (count >= perListcount)
                {

                    //传入数组内的参数
                    refWebtPer_Num.Instance.inser_PerNumextract(PerType, Project, Txt_PerNum.Text, PerList[0], PerList[1], perListcount, Txt_ToUse.Text, UserID);
                    PerCount++;
                   
                    if (PerType == "MAC" ||
                        PerType == "MEID"
                        )
                    {
                        Things = "抽取号段《号段类型：" + PerType + "号段头：" + Txt_PerNum.Text +
                        "起始值：" + Convert.ToString(int.Parse(PerList[0].ToString()), 16).ToUpper() + "结束值："
                        + Convert.ToString(int.Parse(PerList[1].ToString()), 16).ToUpper() + "数量：" + perListcount.ToString();
                    }
                    else
                    {

                        Things = "抽取号段《号段类型：" + PerType + "号段头：" + Txt_PerNum.Text +
                        "起始值：" + PerList[0].ToString() + "结束值："
                        + PerList[1].ToString() + "数量：" + perListcount.ToString();
                    }

                    //Convert.ToString(int.Parse(PerList[0].ToString()), 16)
                    Inser_Log("抽取号段", Things, UserID);
                    Out_Arry(Txt_PerNum.Text, PerList[0], perListcount);
                    count -= perListcount;
                }
                else if (count < perListcount)
                {
                    //传入数组内起始值和计算所得结束值
                    NewEnd = Get_End(PerList[0], count);
                    refWebtPer_Num.Instance.inser_PerNumextract(PerType, Project, Txt_PerNum.Text, PerList[0], NewEnd, count, Txt_ToUse.Text, UserID);
                    PerCount++;
                    if (PerType == "MAC" ||
                         PerType == "MEID")
                    {
                        Things = "抽取号段《号段类型：" + PerType + "号段头：" + Txt_PerNum.Text +
                        "起始值：" + Convert.ToString(int.Parse(PerList[0].ToString()), 16).ToUpper() + "结束值："
                        + Convert.ToString(int.Parse(NewEnd.ToString()), 16).ToUpper() + "数量：" + count.ToString();
                    }
                    else
                    {

                        Things = "抽取号段《号段类型：" + Cmb_PerNumType.SelectedItem.ToString() + "号段头：" + Txt_PerNum.Text +
                             "起始值：" + PerList[0].ToString() + "结束值：" + NewEnd.ToString() + "数量：" + count.ToString();
                    }
                    Inser_Log("抽取号段", Things, UserID);
                    Out_Arry(Txt_PerNum.Text, PerList[0], count);
                    count = 0;
                }
                List.RemoveAt(0);

            }


            if (count != 0)
            {
                int NewSta;
                if (Dt_MiddleNum.Rows.Count > 0)
                {
                    //if (Cmb_PerNumType.SelectedItem.ToString() == "IMEI")
                    //{
                    //    NewSta = Convert.ToInt32(Dt_MiddleNum.Rows[Dt_MiddleNum.Rows.Count - 1][5].ToString()) + 1;
                    //}
                    //else
                    //{
                    NewSta = Convert.ToInt32(Dt_MiddleNum.Rows[Dt_MiddleNum.Rows.Count - 1][5].ToString()) + 1;
                    //}
                }
                else
                {
                    NewSta = S_Num;
                }
                NewEnd = Get_End(NewSta, count);
                refWebtPer_Num.Instance.inser_PerNumextract(PerType, Project, Txt_PerNum.Text, NewSta, NewEnd, count, Txt_ToUse.Text, UserID);
                PerCount++;
                if (PerType == "MAC" ||
                        PerType == "MEID")
                {
                    Things = "抽取号段《号段类型：" + PerType + "号段头：" + Txt_PerNum.Text +
                    "起始值：" + Convert.ToString(int.Parse(NewSta.ToString()), 16).ToUpper() + "结束值："
                    + Convert.ToString(int.Parse(NewEnd.ToString()), 16).ToUpper() + "数量：" + count.ToString();
                }
                else
                {


                    Things = "抽取号段《号段类型：" + Cmb_PerNumType.SelectedItem.ToString() + "号段头：" + Txt_PerNum.Text +
                            "起始值：" + NewSta.ToString() + "结束值：" + NewEnd.ToString() + "数量：" + count.ToString();
                }
                Inser_Log("抽取号段", Things, UserID);
                Out_Arry(Txt_PerNum.Text, NewSta, count);
            }
            if (PerCount > 1)
            {
                Is_section = true;
            }

        }
        //获取结束值
        public int Get_End(int Sta, int count)
        {
            if (Cmb_PerNumType.SelectedItem.ToString() == "IMEI")
            {
                return (Sta + count - 1);
            }
            else
            {
                return (Sta + count - 1);
            }
        }



        public void Out_Arry(string Head_Per, int Sta_Per, int count)
        {
            //Txt_PerNum.Text, PerList[0], PerList[1], perListcount
            string New_PerNum = "";
            string Next_PerNum = "";
            int j = 0;
            int len = 12 - Head_Per.Length;
            string PerType = Cmb_PerNumType.SelectedItem.ToString();
            for (int i = 0; i < count; )
            {
                if (Cmb_PerNumType.SelectedItem.ToString() == "IMEI")
                {

                    if (!string.IsNullOrEmpty(IMEI[j]))
                    {
                        j++;
                        continue;
                    }
                    Next_PerNum = Get_NextNum(PerType, Sta_Per, i);
                    New_PerNum = Head_Per + Next_PerNum.PadLeft(6, '0');
                    IMEI[j] = New_PerNum + Get_LastIMEI(New_PerNum);
                    i++;
                    j++;
                }

                if (Cmb_PerNumType.SelectedItem.ToString() == "MEID")
                {

                    if (!string.IsNullOrEmpty(MEID[j]))
                    {
                        j++;
                        continue;
                    }
                    Next_PerNum = Get_NextNum(PerType, Sta_Per, i);
                    New_PerNum = Head_Per + Next_PerNum.PadLeft(6, '0');
                    MEID[j] = New_PerNum.ToUpper();
                    i++;
                    j++;
                }
                if (Cmb_PerNumType.SelectedItem.ToString() == "MAC")
                {

                    if (!string.IsNullOrEmpty(MAC[j]))
                    {
                        j++;
                        continue;
                    }
                    Next_PerNum = Get_NextNum(PerType, Sta_Per, i);
                    New_PerNum = Head_Per + Next_PerNum.PadLeft(len, '0');
                    MAC[j] = New_PerNum.ToUpper();
                    i++;
                    j++;
                }
            }


        }

        private void Btn_Out_Click(object sender, EventArgs e)
        {
            if (Is_section)
            {
                this.mFrm.ShowPrgMsg("号段被分为多段请仔细查看", MainParent.MsgType.Incoming);
                MessageBox.Show("号段被分为多段请仔细查看");
            }
            // string OutTxt = "";
            if (cmd_IsAte.SelectedIndex == 0)
            {
                if (SimType == "C（单卡）")
                {
                    Csinglecard(MEID, MAC);
                    // OutTxt = Context;
                }
                if (SimType == "C+G（双卡）")
                {
                    CGdoublecard(IMEI, MEID, MAC);
                    // OutTxt = Context;
                }
                if (SimType == "W（单卡）" || SimType == "G（单卡）")
                {
                    W_G_singleCard(IMEI, MAC);
                    // OutTxt = W_G_singleCard(IMEI, MAC);
                }
                if (SimType == "W（双卡）" || SimType == "G（双卡）")
                {
                    W_G_doubleCard(IMEI, MAC);
                    // OutTxt = W_G_doubleCard(IMEI, MAC);
                }
            }
            else
            {
                ATE_MAC(MAC);
                // OutTxt = ATE_MAC(MAC);
            }

            DoOut(Context);



            IMEI = new string[Imei_Count];
            MEID = new string[Meid_Count];
            MAC = new string[Mac_Count];

            Imei_Count = 0;
            Meid_Count = 0;
            Mac_Count = 0;
            Cmb_PerNumType.SelectedIndex = 0;

            Btn_Out.Visible = false;
            Btn_GetPerNum.Enabled = true;



            cmb_Company.Enabled = true;
            cmd_IsAte.Enabled = true;
            Cmb_Phone.Enabled = true;
            Txt_TypeShow.Text = "";
            Txt_PhoCount.Text = "";
            Txt_PhoCount.Enabled = true;


        }
        //保存成文件
        public bool DoOut(string OutTxt)
        {
            SaveFileDialog saveFile1 = new SaveFileDialog();
            saveFile1.Title = "保存Text文件";
            saveFile1.Filter = "文本文件(.txt)|*.txt";
            saveFile1.FilterIndex = 1;
            if (saveFile1.ShowDialog() == DialogResult.OK)
            {
                string FileName = saveFile1.FileName;
                if (File.Exists(FileName))
                    File.Delete(FileName);
                FileStream objFileStream;
                StreamWriter objStreamWriter;
                objFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
                objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);

                try
                {
                    objStreamWriter.WriteLine(OutTxt);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    objStreamWriter.Close();
                    objFileStream.Close();
                    this.mFrm.ShowPrgMsg("保存成功！~", MainParent.MsgType.Incoming);
                    Is_section = false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }


        public void Get_AllExtracNum()
        {
            // Sel_AllExtracNum
            AllExtracNum = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.Sel_AllExtracNum());
        }

        public void Bind_DG_Used()
        {

            //for (int i = 0; i < AllExtracNum.Rows.Count; i++)
            //{
            //    //if (AllExtracNum.Rows[i][1].ToString() == "MAC"||
            //    //    AllExtracNum.Rows[i][1].ToString() == "MEID"
            //    //    )
            //    //{
            //    //    AllExtracNum.Rows[i][3] = Convert.ToString(int.Parse(AllExtracNum.Rows[i][4].ToString()), 16);
            //    //    AllExtracNum.Rows[i][4] = Convert.ToString(int.Parse(AllExtracNum.Rows[i][5].ToString()), 16);
            //    //    //    AllExtracNum.Rows[i][4]
            //    //}
            //}

            DG_Used.DataSource = AllExtracNum;
            DG_Used.Columns.Clear();
            DisplayCol(Dg_SelPerNum, "PerType", "号段类型", 100);
            DisplayCol(Dg_SelPerNum, "Project", "产品名", 100);
            DisplayCol(DG_Used, "Perhead", "号段头", 100);
            DisplayCol(DG_Used, "SNum", "起始值", 100);
            DisplayCol(DG_Used, "ENum", "结束值", 100);
            DisplayCol(DG_Used, "PerCount", "号段数量", 100);
            DisplayCol(DG_Used, "ToUse", "号段用处", 300);
            DisplayCol(DG_Used, "UserID", "抽取人员ID", 100);
            DisplayCol(DG_Used, "InDate", "抽取时间", 100);
        }
        #endregion

        #region 选项卡《号段库查询》
        private void Btn_SelPerNum_Click(object sender, EventArgs e)
        {
            // AllExtracNum

            if (Cmb_IsUsedPerNum.SelectedIndex == 0)
            {
                if (Rdb_Date.Checked)
                {
                    AllExtracNum = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.Sel_ExtractNumByDate(Dt_Sta.Value, Dt_End.Value));
                }
                else
                {
                    AllExtracNum = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.Sel_ExtractNumByHead(Txt_SelPerNum.Text));
                }
            }
            else
            {
                if (Rdb_Date.Checked)
                {
                    AllExtracNum = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.Sel_PerNumByDate(Dt_Sta.Value, Dt_End.Value));
                }
                else
                {
                    AllExtracNum = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.Sel_PerNumByHead(Txt_SelPerNum.Text));
                }

            }

            Bind_Dg_SelPerNum();
        }

        public void Bind_Dg_SelPerNum()
        {

            //已经使用
            //尚未使用
            if (Cmb_IsUsedPerNum.SelectedIndex == 0)
            {
                Dg_SelPerNum.DataSource = AllExtracNum;
                Dg_SelPerNum.Columns.Clear();
                DisplayCol(Dg_SelPerNum, "PerType", "号段类型", 100);
                DisplayCol(Dg_SelPerNum, "Project", "产品名", 100);
                DisplayCol(Dg_SelPerNum, "Perhead", "号段头", 100);
                DisplayCol(Dg_SelPerNum, "SNum", "起始值", 100);
                DisplayCol(Dg_SelPerNum, "ENum", "结束值", 100);
                DisplayCol(Dg_SelPerNum, "PerCount", "号段数量", 100);
                DisplayCol(Dg_SelPerNum, "ToUse", "号段用处", 300);
                DisplayCol(Dg_SelPerNum, "UserID", "抽取人员ID", 100);
                DisplayCol(Dg_SelPerNum, "InDate", "抽取时间", 100);
            }
            else
            {
                DataTable dt_SumExtractNum = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.Sel_SumExtractNum());

                DataTable dt_NewPerNum = Cal_PerNum(AllExtracNum, dt_SumExtractNum);

               
                Dg_SelPerNum.DataSource = dt_NewPerNum;
                Dg_SelPerNum.Columns.Clear();
                DisplayCol(Dg_SelPerNum, "PerType", "号段类型", 100);
                DisplayCol(Dg_SelPerNum, "Perhead", "号段头", 100);
                DisplayCol(Dg_SelPerNum, "SNum", "起始值", 100);
                DisplayCol(Dg_SelPerNum, "ENum", "结束值", 100);
                DisplayCol(Dg_SelPerNum, "PerCount", "号段数量", 100);
                DisplayCol(Dg_SelPerNum, "IsWDK", "号段归属", 100);
                DisplayCol(Dg_SelPerNum, "Model1", "关联手机1", 100);
                DisplayCol(Dg_SelPerNum, "Model2", "关联手机2", 100);
                DisplayCol(Dg_SelPerNum, "Model3", "关联手机3", 100);
                DisplayCol(Dg_SelPerNum, "Model4", "关联手机4", 100);
                DisplayCol(Dg_SelPerNum, "Model5", "关联手机5", 100);
                DisplayCol(Dg_SelPerNum, "InDate", "记录日期", 100);
            }
        }


        private void Dg_SelPerNum_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DG_NoUsed[1, e.RowIndex].Value.ToString()
            string PerType = Dg_SelPerNum[0, e.RowIndex].Value.ToString();//号段类型
            string Project = Dg_SelPerNum[1, e.RowIndex].Value.ToString();//产品名
            string PerHead = Dg_SelPerNum[2, e.RowIndex].Value.ToString();//号段头
            int PerSta = int.Parse(Dg_SelPerNum[3, e.RowIndex].Value.ToString());//起始值
            int PerCount = int.Parse(Dg_SelPerNum[5, e.RowIndex].Value.ToString());//总数


            if (Rdb_Single.Checked)
            {
                Single_OutTxt_Single(PerType, Project, PerHead, PerSta, PerCount);
            }
            else
            {
                Single_OutTxt_Double(PerType, Project, PerHead, PerSta, PerCount);
            }

            DoOut(Context);
        }
        #endregion

        #region 选项卡《操作记录》
        private void Btn_SelLog_Click(object sender, EventArgs e)
        {
            DataTable Log = Sel_Log(Dt_LogSta.Value, Dt_LogEnd.Value, Txt_LogUserId.Text, cmb_FuDo.SelectedItem.ToString());
            Bind_DG_Log(Log);
        }

        public void Sel_AllLog()
        {
            DataTable Dt_Log = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.Sel_AllLog());
            Bind_DG_Log(Dt_Log);
        }

        public void Bind_DG_Log(DataTable Log)
        {
            DG_Log.DataSource = Log;
            DG_Log.Columns.Clear();
            DisplayCol(DG_Log, "FuDo", "操作类型", 100);
            DisplayCol(DG_Log, "FuThing", "操作内容", 500);
            DisplayCol(DG_Log, "FuUser", "操作人员", 100);
            DisplayCol(DG_Log, "DoDate", "操作日期", 100);
        }

        public DataTable Sel_Log(DateTime Dt_Sta, DateTime Dt_End, string UserID, String FuID)
        {
            DataTable Dt_Log = null;
            if (Rdb_LogDate.Checked)
            {
                Dt_Log = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.sel_LogByDateType(FuID, Dt_Sta, Dt_End));
            }
            else
            {
                Dt_Log = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPer_Num.Instance.sel_LogByUserIdType(FuID, UserID));
            }
            return Dt_Log;
        }
        #endregion


    }
}
