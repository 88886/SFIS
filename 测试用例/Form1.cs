using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using System.IO;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;

namespace 测试用例
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 实例化CodeSoft实例
        /// </summary>
        //private LabelManager2.ApplicationClass mlppx = null;//new LabelManager2.ApplicationClass();
        ///// <summary>
        ///// CodeSoft文档
        ///// </summary>
        //private LabelManager2.Document mLibdoc = null;
        private void button1_Click(object sender, EventArgs e)
        {
           
            //string s = string.Format("{0:HHmm}", this.textBox3.Text);
            //this.dateTimePicker1.Text = s;
            //this.dataGridView1.DataSource = this.GetWoKpnumber("7105100","SMT");
           
            //OpenFileDialog();
            //string filePathName;
            //this.mLibdoc = this.mlppx.Documents.Open(filePathName, false);
            //dataGridView1.Rows.Add("sa", "as", "fds");
            //dataGridView1.Columns.AddRange("ds", "s", "sd");
            
            //DataSet ds = SAPConnect.SapConn.Get_Z_RFC_PO("4500013476");
            //DataTable dt = ds.Tables[0];
            //DataTable dttable = ds.Tables[1];
            //return;

            //4500013476

            // c#根据绝对路径获取 带后缀文件名、后缀名、文件名。

             //string str =" F:\test\Default.aspx";
             //string filename = System.IO.Path.GetFileName(str);//文件名 “Default.aspx”
             //string extension = System.IO.Path.GetExtension(str);//扩展名 “.aspx”
             //string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(str);// 没有扩展名的文件名 “Default”

            //string directory=System.IO.Path.GetDirectoryName(physicalPath);

        }
        
        BLL.tPartStorehousehad ps = new BLL.tPartStorehousehad();
        private DataTable GetWoKpnumber(string strwo, string process)
        {
            DataTable dt = ps.GetKpnumberByWoid(strwo, process).Tables[0];
            DataTable mdt = new DataTable("ndt");
            DataRow mdr;
            bool flag = false;
            foreach (DataRow dr in dt.Rows)
            {
                DataTable _dt = ps.GetKpnumberLocation(dr["kpnumber"].ToString(), int.Parse(dr["qty"].ToString())).Tables[0];//料号，领料数
                if (!flag)
                {
                    flag = true;
                    mdt = _dt.Clone();
                }
                float count = 0;
                if (_dt == null || _dt.Rows.Count < 1)
                {
                    //mdr.ItemArray = item.ItemArray;
                    mdt.Rows.Add("NA", dr["kpnumber"].ToString(), "无库存", "无库存", 0);
                }
                else
                {
                    foreach (DataRow item in _dt.Rows)
                    {
                        mdr = mdt.NewRow();
                        count += float.Parse(item["qty"].ToString());
                        if (count < float.Parse(dr["qty"].ToString()))
                        {
                            mdr.ItemArray = item.ItemArray;
                            mdt.Rows.Add(mdr);
                        }
                        else
                        {
                            mdr.ItemArray = item.ItemArray;
                            mdt.Rows.Add(mdr);
                            break;

                        }

                    }
                }
            }
            return mdt;

        }

        public void OpenFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "请选择选择SMT程式";
            ofd.Filter = "(*.CSV 文件)|*.CSV|(*.txt 文本文件)|*.txt|All files (*.*)|*.*";
            ofd.Multiselect = true;
            DialogResult dlr = ofd.ShowDialog();
            DataTable dt=new DataTable();
            if (dlr == DialogResult.Yes || dlr == DialogResult.OK)
            {
                dt = LoadCSV(ofd.FileNames);
                string extension = System.IO.Path.GetExtension(ofd.FileName);//扩展名 “.aspx”
                string filename = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);//文件名
            }
        }
        public List<String[]> ReadCSV(string filePathName)
        {
            List<String[]> ls = new List<String[]>();
            StreamReader fileReader = new StreamReader(filePathName);
            string strLine = "";
            while (strLine != null)
            {
                strLine = fileReader.ReadLine();
                if (strLine != null && strLine.Length > 0)
                {
                    ls.Add(strLine.Split(','));
                }
            }
            fileReader.Close();
            return ls;
        }
        
        public DataTable LoadCSV(string[] filePathNames)//, out DataTable  dtTemp)
        {
            string Err = "";
            DataTable dtTemp = new DataTable("yyy");
            dtTemp.Columns.Add("smtsoftname", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("pcbside", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("LineId", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("stationId", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("kpnumber", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("feeder", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("loction", System.Type.GetType("System.String"));


            foreach (string item in filePathNames)
            {
                string[] stationTemp;
                string strLine = "";
                string[] strArr;
                string stationid = string.Empty;
                string kpnumber = string.Empty;
                string lor = string.Empty;
                string feeder = string.Empty;
                string loc = string.Empty;
                string smtsoftname = string.Empty;
                string pcbside = string.Empty;

                StreamReader sr = new StreamReader(item);
                while (strLine != null)
                {
                    strLine = sr.ReadLine();
                    if (strLine != null && strLine.Length > 0)
                    {
                        strLine = strLine.Trim();
                        if (strLine.Contains("line"))
                        {
                            smtsoftname = System.IO.Path.GetFileNameWithoutExtension(item);
                            pcbside = smtsoftname.Split('-')[smtsoftname.Split('-').Length - 1];

                        }
                        else
                        {
                            strArr = strLine.Split(',');
                            kpnumber = strArr[3];
                            feeder = strArr[5];
                            stationTemp = strArr[2].Split('-');
                            stationid = stationTemp[0] + strArr[0].Replace('#', '0') + stationTemp[1];
                            for (int i = 8; i < strArr.Length; i++)
                            {
                                if (i != strArr.Length - 1)
                                    loc += strArr[i] + ',';
                                else
                                    loc += strArr[i];
                            }

                            dtTemp.Rows.Add(smtsoftname, pcbside, "080101", stationid, kpnumber, feeder, loc);
                            loc = "";
                        }
                    }
                }
            }
            return dtTemp;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            WebServices.tCheckDataTestAte.tCheckDataTestAte zzz = new WebServices.tCheckDataTestAte.tCheckDataTestAte();
           string[] ff= zzz.GetSnMacImeiForAte("A12Y0006RT", "MEID", "7106513",1);
           for (int x = 0; x < ff.Length; x++)
           {
               listBox1.Items.Add(ff[x]);
           }


            //Dis.CreateAccessDb cd = new Dis.CreateAccessDb(@"E:\PhiComm_v2.01\bin\", false);
            ////cd.GetDatatable("select *  from wosnrule where snstart='D842AC7C2409'");
            //cd.GetDatatable("select * from wosnrule where 'D842AC7C240A' between snstart and snend and woid='7103866'");
            ////cd._CreateMDB();
            ////cd._CreateWoSnrule();
            ////cd.ExecuteOracleCommand("insert into wosnrule(woId,sntype,snstart,snend) values( '7103665','SN','000000000000001','000000000000050')");
            ////cd.ExecuteOracleCommand("delete from wosnrule");
            //DataTable dt = new DataTable();
            //return;
            ////SAPConnect.SapConn cnn = new SAPConnect.SapConn();
            //// dt = SAPConnect.SapConn.Get_Z_RFC_LIPS("0080000010");
            ////SAPConnect.SapConn2 c = new SAPConnect.SapConn2();
            ////string str = c.Get_Z_RFC_ZMM011("7102412");
            //dt = SAPConnect.SapConn.Get_Z_RFC_AFPO(this.textBox1.Text);
            ////string err = SAPConnect.SapConn.Get_Z_RFC_MSEG("5000025698", out dt);
            ////string err = SAPConnect.SapConn.Get_Z_RFC_ZPP007("905000003", "2100", "20130101", out dt);
            //this.dataGridView1.DataSource = dt;// SAPConnect.SapConn.Get_Z_RFC_ZMM011(this.textBox1.Text).Tables[0];
            ////this.dataGridView1.DataSource = dt;// SAPConnect.SapConn.Get_Z_RFC_LIPS("0080000010");// dt;

            ////this.dataGridView1.DataSource = SAPConnect.SapConn.Get_Z_RFC_AFPO("7100098 ");//.Get_Z_RFC_ZMM011(this.textBox1.Text.Trim()).Tables[0];
            //return;
            //// //dtt = BLL.tPartStorehousehad.GetAllGangInfo().Tables[0];

            //// DataTableExcel dte = new DataTableExcel();
            ////this.dataGridView1.DataSource =  dte.ReadExcelToDataTable("d:\\222.xls");
            //////dte.DataTableToExcel(this.dataGridView1.DataSource as DataTable, "d:\\222.xls");
            //reftUserInfo.tUserInfo us = new 测试用例.reftUserInfo.tUserInfo();
            //for (int i = 0; i < 500000; i++)
            //{
            //    us.UpdateUserPassword(new 测试用例.reftUserInfo.tUserInfo1()
            //    {
            //        username = "杨胡春",
            //        userId = "K001947",
            //        deptname = "产品技术部",
            //        facId = "B12081300000001",
            //        pwd = "alimama",
            //        rolecaption = "系统开发员",
            //        useremail = "huchun.yang:feixun.com",
            //        userphone = i.ToString(),
            //        userstatus = true
            //    });
            //}
        }
        DataTable dt;
        private void button3_Click(object sender, EventArgs e)
        {
            BLL.tRouteInfo ri = new BLL.tRouteInfo();
            dt = ri.GetRouteStartAndEnd("901000241").Tables[0];
            return;
            TestAte.tCheckDataTestAte tcta = new TestAte.tCheckDataTestAte();
            //List<TestAte.tWipKeyPartTable> lstwkpt = new List<TestAte.tWipKeyPartTable>();
            List<Entity.tWipKeyPartTable> lstwkpt = new List<Entity.tWipKeyPartTable>();
            BLL.tWipTracking twt = new BLL.tWipTracking();



            Entity.tWipKeyPartTable twkpt = new Entity.tWipKeyPartTable();
            twkpt.esn = "S0000001";
            twkpt.sntype = "ESN";
            twkpt.snval = "S0000001";
            twkpt.woId = "7102547";
            lstwkpt.Add(twkpt);
            twkpt = new Entity.tWipKeyPartTable();
            twkpt.esn = "S0000001";
            twkpt.sntype = "SN";
            twkpt.snval = "S0000001";
            twkpt.woId = "7102547";
            lstwkpt.Add(twkpt);
            twkpt = new Entity.tWipKeyPartTable();
            twkpt.esn = "S0000001";
            twkpt.sntype = "MAC";
            twkpt.snval = "D842AC000001";
            twkpt.woId = "7102547";
            lstwkpt.Add(twkpt);

            twkpt = new Entity.tWipKeyPartTable();
            twkpt.esn = "S0000001";
            twkpt.sntype = "KT";
            twkpt.snval = "S1234567";
            twkpt.woId = "7102547";
            lstwkpt.Add(twkpt);

            twt.InsertWipKeyParts(lstwkpt);
            return;
            DataTable mdt;
            SAPConnect.SapConn.Get_Z_RFC_ZPP007("905000069", "2100", "20130101", out mdt);
            this.dataGridView1.DataSource = mdt;
            //BLL.tWipTracking wip = new BLL.tWipTracking();
            //this.dataGridView1.DataSource = wip.GetWoAllSerial("111111");
            //return;
            //wip.GetWipData(new string[] { "7103537", "7300998" });
            MessageBox.Show(RemoveZero("000099008876tr00"));
        }

        private string RemoveZero(string str)
        {
            int x = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str.Substring(i, 1) == "0")
                    x++;
                else
                    break;
            }
            if (x == 0)
                return str;
            else
                return str.Remove(0, x + 1);
        }

        private string RemoveZero1(string str)
        {
            int x = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str.Substring(0, 1) == "0")
                    str = str.Substring(1, str.Length - 1);
                else
                    break;
            }

            return str;
        }

        DataTable deet = new DataTable();
        int i = 0;
        string arr = string.Empty;
        private void button4_Click(object sender, EventArgs e)
        {
            Dis.UserDictionary ud = new Dis.UserDictionary();
            arr = ud.CpuId();
            ////foreach (string item in arr)
            ////{
            ////    MessageBox.Show(item);
            ////}
            // System.Net.Dns
            // i=string.Compare("A00001456", "710371509001");
            /*
            BLL.tWipTracking tw = new BLL.tWipTracking();
            deet = tw.GetWipTrackingList("'a1000865','123k456'", "2555").Tables[0];*/
            
        }

        private void button5_Click(object sender, EventArgs e)
        {


          //  twipdata.tWipTracking ss = new twipdata.tWipTracking();
       //    DataTable dtx=   ReleaseData.arrByteToDataTable(ss.CROSSTAB_WIP("7105839", "903000859", 0));

           DataTable dts = new DataTable();
           dts.Columns.Add("PartNumber", typeof(string));
           dts.Columns.Add("woId", typeof(string));
           dts.Columns.Add("wipstation", typeof(string));
           dts.Columns.Add("QTY", typeof(string));
           //dts.Rows.Add("1111111","PACK_CTN","100");
           //dts.Rows.Add( "1111111", "PACK_PALT", "10");
           //dts.Rows.Add( "1111111", "A-MAC-PRINT", "100");
           //dts.Rows.Add("1111111", "STOCK", "200");
           //dts.Rows.Add( "2222222", "PACK_CTNA", "300");
           //dts.Rows.Add( "2222222", "PACK_PALT", "10");
           //dts.Rows.Add("2222222", "A-MAC-PRINTS", "500");
           //dts.Rows.Add( "2222222", "STOCKA", "2000");

           dts.Rows.Add("aaa","1111111", "PACK_CTN", "100");
           dts.Rows.Add("aaa", "1111111", "PACK_PALT", "10");
           dts.Rows.Add("aaa", "1111111", "A-MAC-PRINT", "100");
           dts.Rows.Add("aaa", "1111111", "STOCK", "200");
           dts.Rows.Add("bbb", "2222222", "PACK_CTNA", "300");
           dts.Rows.Add("bbb", "2222222", "PACK_PALT", "10");
           dts.Rows.Add("bbb", "2222222", "A-MAC-PRINTS", "500");
           dts.Rows.Add("bbb", "2222222", "STOCKA", "2000");
           dataGridView2.DataSource = dts;
           dataGridView3.DataSource = GetCrossTable(dts);
           return;

            BLL.ProPublicStoredproc zz = new BLL.ProPublicStoredproc();
            DataTable dt = new DataTable();
            dt.Columns.Add("sn", typeof(string));
            dt.Columns.Add("dd", typeof(string));
            dt.Columns.Add("ff", typeof(string));
            dt.Rows.Add("DATA", "IN", "TTTT-TTT");
            dt.Rows.Add("RES", "OUT");


         //   zz.SP_PublicStoredprocParam("PRO_CHECKEMP","woId");

            //List<string> dt = new List<string>();
            //dt.Add("12345");
            //dt.Add("22345");
            //dt.Add("32345");
            //dt.Add("42345");
            //dt.Add("52345");
            //SP_TEST_STOCKIN(dt, "GROUPT", "EMP", "EC", "LINE", dt.Count);

          //  BLL.tUserInfo tus = new BLL.tUserInfo();
          //DataTable dt=  tus.GetJurUserInfoById("k001947").Tables[0];
            //bool gg = false;
            //if (textBox4.Text > textBox5.Text)
            //{

            //}
        }

     /// <summary>

        /// 将DataTable行列转换

        /// </summary>

        /// <param name="src">要转换的DataTable</param>

        /// <param name="columnHead">要作为Column的哪列</param>

        /// <returns></returns>

        public static DataTable Col2Row(DataTable src, int columnHead)

        {

            DataTable result = new DataTable();

            DataColumn myHead = src.Columns[columnHead];

            result.Columns.Add(myHead.ColumnName);

            for (int i = 0; i < src.Rows.Count; i++)

            {

                result.Columns.Add(src.Rows[i][myHead].ToString());

            }

            //

            foreach (DataColumn col in src.Columns)

            {

                if (col == myHead)

                    continue;

                object[] newRow = new object[src.Rows.Count + 1];

                newRow[0] = col.ColumnName;

                for (int i = 0; i < src.Rows.Count; i++)

                {

                    newRow[i + 1] = src.Rows[i][col];

                }

              result.Rows.Add(newRow);

           }

            return result;

        }
          public static DataTable Col2Row(DataTable src, string columnHead)

        {

            for (int i = 0; i < src.Columns.Count; i++)

            {

                if (src.Columns[i].ColumnName.ToUpper () == columnHead.ToUpper())

                    return Col2Row(src, i);

            }

            return new DataTable();

        }

        public static DataTable GetCrossTable(DataTable dt)
        {
            //if (dt == null || dt.Columns.Count != 3 || dt.Rows.Count == 0)
            //{
            //    return dt;
            //}
            //else
            {

                DataTable result = new DataTable();
                result.Columns.Add(dt.Columns[0].ColumnName);
                result.Columns.Add(dt.Columns[1].ColumnName);
                DataTable dtColumns = dt.DefaultView.ToTable("dtColumns", true, dt.Columns[2].ColumnName);
                for (int i = 0; i < dtColumns.Rows.Count; i++)
                {
                    string colName;
                    if (dtColumns.Rows[1][0] is DateTime)
                    {
                        colName = Convert.ToDateTime(dtColumns.Rows[i][0]).ToString();
                    }
                    else
                    {
                        colName = dtColumns.Rows[i][0].ToString();
                    }
                    result.Columns.Add(colName);
                    result.Columns[i + 1].DefaultValue = "0";
                }
                DataRow drNew = result.NewRow();
                drNew[0] = dt.Rows[0][1];
                string rowName = drNew[0].ToString();
                foreach (DataRow dr in dt.Rows)
                {
                    string colName = dr[2].ToString();
                    double dValue = Convert.ToDouble(dr[3]);
                    if (dr[0].ToString().Equals(rowName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        drNew[colName] = dValue.ToString();
                    }
                    else
                    {
                        result.Rows.Add(drNew);
                        drNew = result.NewRow();
                        drNew[0] = dr[0];
                        rowName = drNew[0].ToString();
                        drNew[colName] = dValue.ToString();
                    }
                }
                result.Rows.Add(drNew);
                return result;
            }
        }

        #region xxxx


        List<IAsyncResult> ListIasyncresult = new List<IAsyncResult>();
        delegate string runEvent(List<string> ListData, string MYGROUP, string EMP, string EC, string LINE, int Total);
        runEvent deleevnet;
        Dictionary<string, string> errMsg = new Dictionary<string, string>();
        private void CallBackMethod(IAsyncResult ar)
        {
            string msg = string.Empty;
            //runEvent re = (runEvent)ar.AsyncState;
            AsyncResult result = (AsyncResult)ar;
            runEvent re = (runEvent)result.AsyncDelegate;
            msg = re.EndInvoke(ar);
            //将值传递出去
            ErrMsg obj = (ErrMsg)ar.AsyncState;
            obj.errmsg.Add(msg);
            //this.errMsg.Add(obj.ToString(), msg);
        }
        public string SP_TEST_STOCKIN(List<string> ListData, string MYGROUP, string EMP, string EC, string LINE, int Total)
        {
            #region 开启多个线程来处理数据
            int x = 0;
            Dictionary<int, List<string>> diclistesn = new Dictionary<int, List<string>>();
            List<string> esndata = new List<string>();

            for (int i = 0; i < ListData.Count; i++)
            {

                esndata.Add(ListData[i]);
                x++;
                if (x >= 1)
                {
                    diclistesn.Add(i, esndata);
                    x = 0;
                    esndata = new List<string>();
                }
                else
                {
                    if ((i + 1) == ListData.Count)
                    {
                        diclistesn.Add(i, esndata);
                    }
                }
            }
            AsyncCallback re = new AsyncCallback(CallBackMethod);
            ErrMsg em = new ErrMsg();
            int h = 0;
            foreach (int it in diclistesn.Keys)
            {
                if (h == 0)
                    EC = "OK";
                else
                    EC = "KK";
                h++;
                deleevnet = new runEvent(_sp_test_stockin);
                ListIasyncresult.Add(deleevnet.BeginInvoke(diclistesn[it], MYGROUP, EMP, EC, LINE, diclistesn[it].Count, re, em));
            }
            while (true)
            {
                bool flag = true;
                foreach (IAsyncResult item in ListIasyncresult)
                {
                    flag &= item.IsCompleted;

                }
                if (flag)
                    break;
            }
            for (int r = 0; r < em.errmsg.Count; r++)
            {
                MessageBox.Show(em.errmsg[r]);
            }
            #endregion
            return "OK";
        }
        private string _sp_test_stockin(List<string> ListData, string MYGROUP, string EMP, string EC, string LINE, int Total)
        {
            string DATA = "";
            string Msg = "";
            if (ListData.Count != Total)
            {
                return string.Format("传入资料异常,传入ESN数量{0}个,实际应该是{1}", ListData.Count.ToString(), Total.ToString());
            }
            else
            {
                for (int i = 0; i < ListData.Count; i++)
                {
                    DATA = ListData[i].ToString();
                    Msg = SP_TEST_STOCKIN(DATA, MYGROUP, EMP, EC, LINE);
                    if (Msg != "OK")
                    {
                        return " Excute TEST_STOCKIN Failed-->" + DATA + "--" + Msg;
                        break;
                    }
                }

                return Msg;
            }
        }

        public string SP_TEST_STOCKIN(string DATA, string MYGROUP, string EMP, string EC, string LINE)
        {
            if (EC == "OK")
                return "OK";
            else
                return "ERROR";
        }
        #endregion




    }

    public class ErrMsg
    {
        public List<string> errmsg = new List<string>();
    }

    public class DataTableExcel
    {
        public bool DataTableToExcel(System.Data.DataTable dtSource, string filePath)
        {
            try
            {
                //文档仅写入一个sheet
                //建立一个workbook
                HSSFWorkbook workbook = new HSSFWorkbook();
                System.Data.DataTable dt = dtSource;
                //建立sheet
                HSSFSheet sheet = workbook.CreateSheet("sheet1");
                //为避免日期格式被Excel自动替换，所以设定 format 为 『:』 表示一率当成text來看
                HSSFCellStyle textStyle = workbook.CreateCellStyle();
                textStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat(":");
                //用column name 作为列名
                List<string> columns = new List<string>();
                for (int colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
                {
                    string name = dt.Columns[colIndex].ColumnName;
                    HSSFCell cell = sheet.CreateRow(0).CreateCell(colIndex);
                    cell.SetCellValue(name);
                    cell.CellStyle = textStyle;
                    columns.Add(name);
                }
                //建立内容列
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    DataRow dr = dt.Rows[row];
                    for (int col = 0; col < columns.Count; col++)
                    {
                        string data = dr[columns[col]].ToString();
                        HSSFCell cell = sheet.CreateRow(row + 1).CreateCell(col);
                        cell.SetCellValue(data);
                        cell.CellStyle = textStyle;
                    }
                }
                //写Excel
                FileStream file = new FileStream(filePath, FileMode.OpenOrCreate);
                workbook.Write(file);
                file.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public System.Data.DataTable ReadExcelToDataTable(string filePath)
        {
            //打开要读取的Excel
            FileStream file = new FileStream(filePath, FileMode.Open);
            //读入Excel
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            file.Close();
            HSSFSheet sheet = workbook.GetSheetAt(0);
            //建立一个新的table
            DataTable dtNew = new DataTable(); ;
            HSSFRow row = sheet.GetRow(0);
            //读取取第0列作为column name
            for (int columnIndex = 0; columnIndex < row.LastCellNum; columnIndex++)
            {
                DataColumn dc = new DataColumn(row.GetCell(columnIndex).ToString());
                dtNew.Columns.Add(dc);
            }
            int rowId = 1;
            //第一列以后为资料，一直读到最后一行
            while (rowId <= sheet.LastRowNum)
            {
                DataRow newRow = dtNew.NewRow();
                //读取所有column
                for (int colIndex = 0; colIndex < dtNew.Columns.Count; colIndex++)
                {
                    string str = string.Empty;
                    HSSFCell CellVal = sheet.GetRow(rowId).GetCell(colIndex);
                    if (CellVal != null)
                        str = CellVal.ToString();
                    newRow[dtNew.Columns[colIndex]] = str;
                }
                dtNew.Rows.Add(newRow);
                rowId++;
            }
            return dtNew;
        }
    }

    public class ReleaseData
    {
        public static DataTable arrByteToDataTable(byte[] zipBuffer)
        {
            if (zipBuffer == null || zipBuffer.Length < 1)
                return null;
            byte[] buffer = UnZipClass.Decompress(zipBuffer);
            BinaryFormatter ser = new BinaryFormatter();
            DataSetSurrogate dss = ser.Deserialize(new MemoryStream(buffer)) as DataSetSurrogate;
            DataSet dataSet = dss.ConvertToDataSet();

            return dataSet.Tables[0];
        }

        public static DataSet arrByteToDataSet(byte[] zipBuffer)
        {
            if (zipBuffer == null || zipBuffer.Length < 1)
                return null;
            byte[] buffer = UnZipClass.Decompress(zipBuffer);
            BinaryFormatter ser = new BinaryFormatter();
            DataSetSurrogate dss = ser.Deserialize(new MemoryStream(buffer)) as DataSetSurrogate;
            DataSet dataSet = dss.ConvertToDataSet();

            return dataSet;
        }
    }

    public static class UnZipClass
    {
        public static byte[] Decompress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(data);
                Stream zipStream = null;
                zipStream = new GZipStream(ms, CompressionMode.Decompress);
                byte[] dc_data = null;
                dc_data = ExtractBytesFromStream(zipStream, data.Length);
                return dc_data;
            }
            catch
            {
                return null;
            }
        }
        public static byte[] ExtractBytesFromStream(Stream zipStream, int dataBlock)
        {
            byte[] data = null;
            int totalBytesRead = 0;
            try
            {
                while (true)
                {
                    Array.Resize(ref data, totalBytesRead + dataBlock + 1);
                    int bytesRead = zipStream.Read(data, totalBytesRead, dataBlock);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    totalBytesRead += bytesRead;
                }
                Array.Resize(ref data, totalBytesRead);
                return data;
            }
            catch
            {
                return null;
            }
        }
    }
}  


