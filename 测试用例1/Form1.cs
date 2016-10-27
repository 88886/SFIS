using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Reflection;
using System.IO;



namespace 测试用例1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static string strDB = string.Format("server={0};uid={1};pwd={2};database={3};pooling=True",
            "172.16.173.242", "sa", "sa67754400", "PHICOMM_V3");
        MsSqlLib mysql =null; //new MsSqlLib(strDB);
        cdbAccess ass = null; //new cdbAccess();
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dtt;
            dataGridViewX1.Columns.Add("snstart", "snstart");
            dataGridViewX1.Columns.Add("snend", "snend");
            dataGridViewX1.Rows.Add("FTEQ9124588201003", "FTEQ9124588211002");
            dataGridViewX1.Rows.Add("FTEQ9124588221003", "FTEQ9124588221052");

            string tbinput = this.tbinput.Text.Trim();

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "select * from tWOSnRule where woid='7105110'and sntype='KT' order by snstart";
            dtt = mysql.ExecuteDataSet(cmd).Tables[0];

            ass.ExecuteOracleCommand("delete from ktrule");

            for (int i = 0; i < dtt.Rows.Count; i++)
            {
                int index = 0;
                string snstart = dtt.Rows[i]["snstart"].ToString();
                string snend = dtt.Rows[i]["snend"].ToString();
                index = GetIndex(snstart, snend);
                int boxnum = 0;
                int totalkt = Convert.ToInt32(snend.Substring(index)) - Convert.ToInt32(snstart.Substring(index))+1;
                if (totalkt % 20 == 0)
                    boxnum = totalkt / 20;
                else if (totalkt % 20 != 0 && i == dtt.Rows.Count - 1)
                    boxnum = totalkt / 20 + 1;
                else
                    throw new Exception("KT区间，有零数存在...");
                string sql = string.Format("insert into ktrule(woId,sntype,snstart,snend,sid,boxnum) values('{0}','{1}','{2}','{3}','{4}','{5}')",

                    dtt.Rows[i]["woId"].ToString(),
                    dtt.Rows[i]["sntype"].ToString(),
                    dtt.Rows[i]["snstart"].ToString(),
                    dtt.Rows[i]["snend"].ToString(),
                    i + 1,
                    boxnum.ToString()
                   );
             //   ass.ExecuteOracleCommand(sql);
            }
            //bt_open_Click(null,null);

            //List<string> ls = new List<string>();
            //ls.Add("SN");
            //ls.Add("MAC");
            //string[] arr = ls.ToArray();

            //string Sql = "select * from tProduct where partnumber='901000795'";
            //dgvSAPList.DataSource = an.ExecuteQuery(Sql);

            //dgvSAPList.DataSource = SAPConnect.SapConn.Get_Z_RFC_LIPS("80035335", "");

            //LoadSAPInfo();

            //string s = string.Format("{0:HH:mm}",  this.textBox3.Text);
            //this.dateTimePicker1.Text = s;
        }
        private int GetIndex(string strstart, string strend)
        {

            int index = 0;
            for (int s = 0; s < strstart.Length; s++)
            {
                if (strstart[s].CompareTo(strend[s]) != 0)
                {
                    index = s;
                    break;
                }
            }
            return index;
        }
        private void Func()
        {
            string str = "";
            string pattern = "[A-Za-z]";
        }
        private void LoadSAPInfo()
        {

            DataTable mdtable = SAPConnect.SapConn.Get_Z_RFC_LIPS("80028516", "");
            dgvSAPList.DataSource = mdtable;

            #region 向SFIS系统填充出货信息
            List<Entity.tSapLodeInfo> lssap = new List<Entity.tSapLodeInfo>();

            foreach (DataRow dr in mdtable.Rows)
            {
                string SerialCode = "";
                lssap.Add(new Entity.tSapLodeInfo()
                {
                    ContactPerson = dr["KUNNR"].ToString(),
                    CustomerName = dr["NAME1"].ToString(),
                    CustomerId = SerialCode,
                    Partnumber = dr["MATNR"].ToString(),
                    ProductDesc = dr["MAKTX"].ToString(),
                    SAPCode = dr["VBELN"].ToString(),
                    QTY = Convert.ToInt32(dr["LFIMG"].ToString().Split('.')[0]),
                    SapWarehouse = dr["WERKS"].ToString(),
                    UserId = "K001947",
                    SFCcode = ""
                });

            }
   
            //   BLL.tWarehouseWipTracking.InsertOutPutRecordList(lssap);          

            #endregion


        }
        private void button2_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(this.tbinput.Text.Trim()))
                return;
            string sql =string.Format( "select * from ktrule where woid='{0}' and sntype='{1}' and '{2}' between snstart and snend",
                "7105110","KT",tbinput.Text.Trim());
            DataTable dt = ass.GetDatatable(sql);
            if (dt==null||dt.Rows.Count<1)
            {
                throw new Exception("该KT号不在区间内");
            }
            if (dt.Rows.Count>1)
            {
                throw new Exception("该KT号存在多个区间内");
            }
            if (dt.Rows[0]["sid"].ToString()=="1")
            {
                int idx = GetIndex(dt.Rows[0]["snstart"].ToString(), dt.Rows[0]["snend"].ToString());
                this.textBox2.Text = ((Convert.ToInt32(this.tbinput.Text.Trim().Substring(idx)) - 
                    Convert.ToInt32(dt.Rows[0]["snstart"].ToString().Substring(idx))) / 20 + 1).ToString();
            }
            else
            {

            }
            //string sql = "select * from tCustomer where contactperson='100837'";
            //dgvSAPList.DataSource = mysql.ExecuteQuery(sql);
        }
        private void bt_open_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Title = "打开要转换的BOM";
                fd.Filter = "(*.xls Excel 2003)|*.xls";
                DialogResult dr = fd.ShowDialog();
                if (dr == DialogResult.Yes || dr == DialogResult.OK)
                {
                    string filename = fd.FileName;
                    dataGridViewX1.DataSource = ExcelToDS(filename).Tables[0];

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //DataTable mdt = FrmBLL.ClsReadExcel.getTableForSql(ofd.FileName,
        //                    string.Format("select 子件号,子件描述,替代组,'{1}'as Bom版本,排序字符串,用量,长文本（位号） from [{0}] where{2} 长文本（位号）<>''",
        //                    FrmBLL.ClsReadExcel.GetTableNames(ofd.FileName)[0], filename.Split('-')[1], temp));

        /// <summary>
        /// 在Excel中执行SQL命令读取Sheet
        /// </summary>
        public DataSet ExcelToDS(string Path)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            return ds;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //AssemblyName currentAssemblyName = AssemblyName.GetAssemblyName(@"C:\Users\Feixun\Desktop\SFC\SFIS_V3.0.exe");
            //AssemblyName updatedAssemblyName = AssemblyName.GetAssemblyName(@"C:\Users\Feixun\Desktop\SFC\SFIS_V3.0.exe");
            //currentAssemblyName.Version = new Version("5.0.0.0");
            //// 比较版本
            //if (updatedAssemblyName.Version.CompareTo(currentAssemblyName.Version) <= 0)
            //{
            //    // 不需要更新
            //    return;
            //}
            //else
            //{

            //}
            MessageBox.Show(Application.ProductVersion);

            string newVersion = "0.0.0.5";
            if (Application.ProductVersion.CompareTo(newVersion) < 0)
            {
                //string test = "发现更新";
                //return test;
                MessageBox.Show("发现更新");
            }

            // 更新
            //File.Copy(updatedAssemblyPath, currentAssemblyPath, true);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Version V1 = new Version(textBox3.Text);
            //Version V2 = new Version(textBox4.Text);
            //MessageBox.Show(V1.CompareTo(V2).ToString());

            //BLL.Check_Vsersion cv = new BLL.Check_Vsersion();
            //cv.CheckPrgVsersion("SFIS_V3", Application.ProductVersion, null, null, null);

            //BLL.tWoInfo zzz = new BLL.tWoInfo();
            //zzz.GetSnMacImeiForAte("test001", "MAC", "1111111", 2);

            //MessageBox.Show(h2x(1404,36));

           
 



        }
        public static string h2x(long value, int toBase)
        {
            int digitIndex = 0;
            long longPositive = Math.Abs(value);
            int radix = toBase;
            char[] outDigits = new char[63];

            for (digitIndex = 0; digitIndex <= 64; digitIndex++)
            {
                if (longPositive == 0) { break; }

                outDigits[outDigits.Length - digitIndex - 1] =
                    rDigits[longPositive % radix];
                longPositive /= radix;
            }

            return new string(outDigits, outDigits.Length - digitIndex, digitIndex);
        }
        private static char[] rDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(DateTime.Now.ToString("yyyyMMdd"));
        }
    }
}
