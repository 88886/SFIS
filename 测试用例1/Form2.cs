using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Xml;

using System.Xml.Linq;

namespace 测试用例1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        BLL.Db_Procedure DP = new BLL.Db_Procedure();
        BLL.tUserInfo user = new BLL.tUserInfo();
        BLL.B_SNRULE_PARTNUMBER bbbb = new BLL.B_SNRULE_PARTNUMBER();
        BLL.tCraftInfo ci = new BLL.tCraftInfo();
        BLL.tCustomer cust = new BLL.tCustomer();
        BLL.tDeptInfo di = new BLL.tDeptInfo();
        BLL.tKeyPart kp = new BLL.tKeyPart();
        BLL.tMaterialPreparation_1 mmp = new BLL.tMaterialPreparation_1();
        BLL.tRouteInfo route = new BLL.tRouteInfo();
        BLL.tRepairInfo repair = new BLL.tRepairInfo();
        BLL.tWipTracking twip = new BLL.tWipTracking();

        private void button1_Click(object sender, EventArgs e)
        {
           //string C_RES = "";
           //DP.PRO_TEST_STOCKIN("A16KA0010N", "PACK_StoreIn", "PACK_StoreIn", "FX005563-147", "NA", "SSSS", out C_RES);


         //   DP.PRO_SN_INPUT_WIPFIRST("A16D000097", "ASSY_Input", "A060101", "7108303", "FX003358-123");
          //  route.CHECK_ROUTE_INFO("277", "10G_TEST", "0");
           // DataTable dt = new DataTable("mydt");
           // dt.Columns.Add("Colnum",typeof(string));
           // dt.Columns.Add("DATA", typeof(string));
           // dt.Rows.Add("DATA", "FX003358-123");
           //string SS= DP.PublicStationPro("PRO_CHECKEMP",dt);

          //  mmp.InsertMaterialPreparation_1("S009163", "7106380", "123");

            ////DataSet ds = kp.GetKeyParts();
            ////DataTable dt = ds.Tables[0];
            ////string ss = "Txt_BOX_LAB_NAME";
            ////MessageBox.Show(ss.Substring(4,ss.Length-4));
            //////GetFromCtls(this);
            //string SSS = string.Empty;
          //  IDictionary<string, object> dic = new Dictionary<string, object>();
          //  dic.Add("DATA", "A000000002");
          //  dic.Add("MYGROUP", "SMT_VI");
          //  dic.Add("EMP", "FX005506-123456");
          //  dic.Add("EC", "NA");
          //  dic.Add("LINE", "A060201");
          //  dic.Add("WO", "701000147");            
          ////  dic.Add("TESTFLAG", "1\0");
          //string ss=  MapListConverter.DictionaryToJson(dic);
          //DP.ExecuteProcedure("PRO_INPUT_SN_FIRST", ss);
            //IList<IDictionary<string, object>> zz = new List<IDictionary<string, object>>();

            ////ci.InsertRefCraftInfo(MapListConverter.DictionaryToJson(dic), MapListConverter.ListDictionaryToJson(zz), out SSS);

            //IList<IDictionary<string, object>> LsDic = new List<IDictionary<string, object>>();
            //IDictionary<string, object> mst = null;
            
            //    mst = new Dictionary<string, object>();
            //    mst.Add("ESN", "34BDF9F4FE18");
            //    mst.Add("ERROR_CODE","2222");
            //    mst.Add("REASON_CODE","3333");
            //    mst.Add("WOID", "702000456");
            //    mst.Add("PARTNUMBER", "901002018");
            //    mst.Add("CRAFTID", "REST_TEST");
            //    mst.Add("REUSER", "FX003358");
            //    mst.Add("LINE", "REPAIR");
            //    mst.Add("LOCATION","123TREST");
            //    mst.Add("REMARK","");
            //    mst.Add("OUTCRAFTID", "SSSS");
            //    mst.Add("DUTY", "1");
            //    mst.Add("ID", "0");
            //    LsDic.Add(mst);

            //    string ss = MapListConverter.DictionaryToJson(mst);
            //    mst = new Dictionary<string, object>();
            //    mst.Add("ESN", "34BDF9F4FE18");
            //    mst.Add("ERROR_CODE", "9999");
            //    mst.Add("REASON_CODE", "888");
            //    mst.Add("WOID", "702000456");
            //    mst.Add("PARTNUMBER", "901002018");
            //    mst.Add("CRAFTID", "REST_TEST");
            //    mst.Add("REUSER", "FX003358");
            //    mst.Add("LINE", "REPAIR");
            //    mst.Add("LOCATION", "123TREST");
            //    mst.Add("REMARK", "");
            //    mst.Add("OUTCRAFTID", "SSSS");
            //    mst.Add("DUTY", "1");
            //    mst.Add("ID", "NA");
            //    LsDic.Add(mst);



            //    repair.UpdateRepairInformation(MapListConverter.ListDictionaryToJson(LsDic));
           // string RES = DP.ExecuteProcedure("PRO_TEST_INPUT_ALL", MapListConverter.DictionaryToJson(dic));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="lsfunls"></param>
        /// <returns></returns>
        public Dictionary<string, Dictionary<string, object>> GetFromCtls(System.Windows.Forms.Control ctl)
        {
           Dictionary<string,Dictionary<string,object>> LsDic = new Dictionary<string,Dictionary<string,object>>();
            Dictionary<string,object> dic = new Dictionary<string,object>();


            foreach (Control cl in ctl.Controls)
            {
                dic = new Dictionary<string, object>();

                if (cl is System.Windows.Forms.TextBox)
                {
                    dic.Add(cl.Name, cl.Text);
                }
                if (cl is DevComponents.DotNetBar.ButtonX)
                {
                    dic.Add(cl.Name, cl.Text);
                }

                LsDic.Add(cl.Name, dic);
                 
                    
            }
                //for (int i = 0; i < ctl.Controls.Count; i++)
                //{
                //    if (ctl.Controls[i] is System.Windows.Forms.TextBox)
                //    {
                //        dic.Add(ctl.Name, ctl.Text);
                //    }
                   
                //}

            return LsDic;
            //if (ctl is System.Windows.Forms.Button || ctl is DevComponents.DotNetBar.ButtonX)
            //{
            //    if (!string.IsNullOrEmpty(ctl.Text))
            //    {
            //        //lsfunls.Add(new WebServices.tUserInfo.tFunctionList()
            //        //{
            //        //    funId = ctl.Name,
            //        //    funname = ctl.Text,
            //        //    fundesc = ctl.Text,
            //        //});
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < ctl.Controls.Count; i++)
            //    {
            //        lsfunls = GetFromCtls(ctl.Controls[i]);
            //    }
            //}
           // return lsfunls;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            BLL.Db_Procedure sp = new BLL.Db_Procedure();
            string DATA="006B8E5697DF                     MACH001             C200BT0004          PASS";
            string ss = null;
            sp.PRO_INS_ATE_BACK(DATA, "", "","","SFDS", "A080101", out ss);
           MessageBox.Show(ss);
        }

        MsgNotice.MsgNotice msg = new MsgNotice.MsgNotice();
        private void button2_Click(object sender, EventArgs e)
        {
            msg.ShowMsg("显示我们的信息");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //string RES;
            //DP.PRO_TEST_REWORK_INPUT("34BDF9F4E2C0", "FX003358", "SDFSDF", "701000006", "A080101", "0", out RES);

            //DP.DataTable_To_ListDictionary(twip.GetQueryWipAllInfo("CARTONNUMBER", "702000456C0070").Tables[0]);
        }

        private void button4_Click(object sender, EventArgs e)
        {


            Dictionary<string, object> DIC = new Dictionary<string, object>();
            DIC.Add("DATA", "A16N00001P");
            DIC.Add("MYGROUP", "SMT_VI");
            DIC.Add("EMP", "FX003358-123");
            DIC.Add("EC", "NA");
            DIC.Add("LINE", "A080101");
            DIC.Add("WO", "701000328");
         string _str=   DP.CALL_PRO_INPUT_SN_FIRST(DIC);
         MessageBox.Show(_str);
           // DataSet ds = ConvertXMLToDataSet("<NewDataSet> <Table1> <BEWORKSEG>SMT</BEWORKSEG> <TESTFLAG>2</TESTFLAG> <R_SMT_VI>N</R_SMT_VI> <DX_TEST>0</DX_TEST> <R_DX_TEST>0</R_DX_TEST> <R_SMT_VI_01>0</R_SMT_VI_01> <CHUCE_1>0</CHUCE_1> <SMT_STOCKIN>0</SMT_STOCKIN> <SMT_Input>0</SMT_Input> <SMT_VI>0</SMT_VI> <SMT_IPQC>0</SMT_IPQC> <R_SMT_IPQC>0</R_SMT_IPQC> </Table1> <Table1> <BEWORKSEG>SMT</BEWORKSEG> <TESTFLAG>0</TESTFLAG> <R_SMT_VI>0</R_SMT_VI> <DX_TEST>N</DX_TEST> <R_DX_TEST>0</R_DX_TEST> <R_SMT_VI_01>0</R_SMT_VI_01> <CHUCE_1>0</CHUCE_1> <SMT_STOCKIN>0</SMT_STOCKIN> <SMT_Input>0</SMT_Input> <SMT_VI>0</SMT_VI> <SMT_IPQC>0</SMT_IPQC> <R_SMT_IPQC>0</R_SMT_IPQC> </Table1> <Table1> <BEWORKSEG>SMT</BEWORKSEG> <TESTFLAG>2</TESTFLAG> <R_SMT_VI>0</R_SMT_VI> <DX_TEST>0</DX_TEST> <R_DX_TEST>N</R_DX_TEST> <R_SMT_VI_01>N</R_SMT_VI_01> <CHUCE_1>0</CHUCE_1> <SMT_STOCKIN>0</SMT_STOCKIN> <SMT_Input>0</SMT_Input> <SMT_VI>0</SMT_VI> <SMT_IPQC>0</SMT_IPQC> <R_SMT_IPQC>0</R_SMT_IPQC> </Table1> <Table1> <BEWORKSEG>SMT</BEWORKSEG> <TESTFLAG>0</TESTFLAG> <R_SMT_VI>0</R_SMT_VI> <DX_TEST>0</DX_TEST> <R_DX_TEST>0</R_DX_TEST> <R_SMT_VI_01>0</R_SMT_VI_01> <CHUCE_1>N</CHUCE_1> <SMT_STOCKIN>N</SMT_STOCKIN> <SMT_Input>N</SMT_Input> <SMT_VI>N</SMT_VI> <SMT_IPQC>N</SMT_IPQC> <R_SMT_IPQC>0</R_SMT_IPQC> </Table1> <Table1> <BEWORKSEG>SMT</BEWORKSEG> <TESTFLAG>2</TESTFLAG> <R_SMT_VI>N</R_SMT_VI> <DX_TEST>0</DX_TEST> <R_DX_TEST>0</R_DX_TEST> <R_SMT_VI_01>0</R_SMT_VI_01> <CHUCE_1>0</CHUCE_1> <SMT_STOCKIN>0</SMT_STOCKIN> <SMT_Input>0</SMT_Input> <SMT_VI>0</SMT_VI> <SMT_IPQC>0</SMT_IPQC> <R_SMT_IPQC>N</R_SMT_IPQC> </Table1> </NewDataSet>");
        }
        private DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (Exception ex)
            {
                string strTest = ex.Message;
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //IBaseProvider dp = new BaseProvider();
            ////  IDictionary<string, object> parms = new Dictionary<string, object>();
            //IDictionary<string, object> procedureOutRes = new Dictionary<string, object>();

            ////parms.Add("DATA", "A16N00001P");
            ////parms.Add("MYGROUP", "SMT_VI");
            ////parms.Add("EMP", "FX003358-123");
            ////parms.Add("EC", "NA");
            ////parms.Add("LINE", "A080101");
            ////parms.Add("WO", "701000328");
            //Dictionary<string, object> DIC = new Dictionary<string, object>();
            //DIC.Add("DATA", "A16N00001P");
            //DIC.Add("MYGROUP", "SMT_VI");
            //DIC.Add("EMP", "FX003358-123");
            //DIC.Add("EC", "NA");
            //DIC.Add("LINE", "A080101");
            //DIC.Add("WO", "701000328");
            //procedureOutRes.Add("RES", (object)200);
            //// procedureOutRes.Add("COMMAND", (object)200);

            //dp.StoreProcedureExec("sfcb.PRO_INPUT_SN_FIRST", DIC, procedureOutRes);
            //MessageBox.Show( procedureOutRes["RES"].ToString());
            BLL.Db_Procedure SS = new BLL.Db_Procedure();
         // SS.PRO_TEST_MAIN_ONLY("A16T00ZVDK","BT_C","FX003358-335819","NA"
          
           // SS.PRO_CHECK_ROUTE_RE("A16T010RCG", "PCBA_SNWrite11");
            SS.Insert_Exception_Log("test");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Log.LogInstance.Write("Test Message", MessageType.Information);
            //Log.LogInstance.Write("one", MessageType.Error);
            //Log.LogInstance.Write("two", MessageType.Success);
            //Log.LogInstance.Write("three", MessageType.Warning);  

            //MessageBox.Show(System.Environment.SystemDirectory);

            //return;
            //Random rnd = new Random();
            //int iNum1 = rnd.Next(1000);
            //MessageBox.Show(iNum1.ToString());
            string RES=string.Empty;

            BLL.Db_Procedure DD = new BLL.Db_Procedure();
            string C_MAXSN = string.Empty;
            string C_WOID = string.Empty;
            string C_ROWID = string.Empty;
           // DD.PRO_GETSNEND(out C_MAXSN, out C_WOID, out C_ROWID); 
            BLL.tWipTracking DDD = new BLL.tWipTracking();
            IDictionary<string, object> dic = new Dictionary<string, object>();
            IList<IDictionary<string, object>> lsdic = new List<IDictionary<string, object>>();
            dic.Add("ESN", "CZRLF29SEH02333");
            dic.Add("SNVAL", "006B8E9B0FBF");
            dic.Add("WOID", "702000573");
            dic.Add("SNTYPE", "MAC1");
            dic.Add("STATION", "AA");
            lsdic.Add(dic);
            DDD.InsertWipKeyParts(lsdic);

           // DP.PRO_TEST_REWORK_INPUT("A16KA0010H", "FX003358", "904000157", "7301659", "A080101", "0", out RES);
          //  BLL.tWipKeyPart DD = new BLL.tWipKeyPart();
          //  DD.Update_WIP_KEYPARTS("A16L00002U" ,"A16L000001", "A16L000002", "MAC");
           
          //  DD.Insert_WipKeyParts_Undo("A16W00001B", "KPESN", "A16K00EACM");
           // DP.PRO_TEST_MAIN_AUTO("A16W00001B                2GFT01                     7109003   ", "THROUGHPUT_TEST", "FX003358-123", "A060201");

        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("ESN", typeof(string));
            dt1.Columns.Add("WOID", typeof(string));
            dt1.Columns.Add("SNTYPE", typeof(string));
            dt1.Columns.Add("SNVAL", typeof(string));
            dt1.Rows.Add("123", "7010005", "MAC", "MAC1");
            dt1.Rows.Add("123", "7010005", "SN", "SN1");

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("ESN", typeof(string));
            dt2.Columns.Add("WOID", typeof(string));
            dt2.Columns.Add("SNTYPE", typeof(string));
            dt2.Columns.Add("SNVAL", typeof(string));           
            dt2.Rows.Add("123", "7010005", "SN", "SN1");
            dt2.Rows.Add("123", "7010005", "MAC", "MAC1");

           bool GGGG= Contains(dt1,dt2);
           MessageBox.Show(GGGG.ToString());

        }
        private bool Contains(DataTable dt1, DataTable dt2)
        {
            if (dt1.Rows.Count != dt2.Rows.Count || dt1.Columns.Count != dt2.Columns.Count)
                return false;

            //dt1.AsIEnumerable().Except(dt2.AsIEnumerable());

            //for (int i = 0; i < dt1.Rows.Count; i++)
            //{
            //    for (int j = 0; j < dt1.Columns.Count; j++)
            //    {
            //        if (dt2.Rows[i][j] != dt1.Rows[i][j])
            //        {
            //            return false;
            //        }
            //    }
            //}
            return true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
          //string _str=  string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now);
          //MessageBox.Show(_str);
            //BLL.FileHelper dd = new BLL.FileHelper();
            //dd.Insert_DB_Log("sdfsdf");

            DataTable dt = new DataTable();
            dt.Columns.Add("CO1", typeof(string));
            dt.Columns.Add("CO2", typeof(string));
            dt.Columns.Add("CO3", typeof(string));
            dt.Columns.Add("CO4", typeof(string));
            dt.Rows.Add("AAA", "VVVV", "BBBB", "RRRR");
            dt.Rows.Add("AAA1", "VVVV1", "BBBB1", "RRRR1");
            dt.Rows.Add("AAA2", "VVVV2", "BBBB2", "RRRR2");
            IList<IDictionary<string, object>> d = DataTableToDictionary(dt);
        }


        private IList<IDictionary<string, object>> DataTableToDictionary(DataTable dt)
        {
            IList<IDictionary<string, object>> LsDic = new List<IDictionary<string, object>>();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (DataColumn dc in dt.Columns)
            {
                dic.Add(dc.ColumnName, "NA");
            }

            foreach (DataRow dr in dt.Rows)
            {
                foreach (KeyValuePair<string, object> kvp in dic)
                {
                    dic[kvp.Key] = dr[kvp.Key].ToString();
                }
                LsDic.Add(dic);
            }


            return LsDic;
        }
    }
}
