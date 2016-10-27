using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SAP.Middleware.Connector;

namespace frm_sap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SFIS_WCF.IService1 ss = new SFIS_WCF.Service1();
           MessageBox.Show( ss.getuserid("eee"));


            listBox1.Items.Clear();

            List<string> lszz = new List<string>();
            Z_RFC_AUFNR_MIGO zfc = new Z_RFC_AUFNR_MIGO();
            zfc.EMP_NO = "K001947";
            zfc.EMP_NAME = "TEST";
            zfc.PartNumber = "900000258";
            zfc.STGE_LOC = "1000";
            zfc.QTY = 10;
            zfc.MOVE_STLOC = "1033";   


            lszz = WHS_MOVE_Z_RFC_AUFNR_MIGO(zfc,"");
            for (int x = 0; x < lszz.Count;x++ )
            {
                listBox1.Items.Add(lszz[x]);
                richTextBox1.AppendText(lszz[x]+"\r\n");
            }
        }

        public List<string> WHS_MOVE_Z_RFC_AUFNR_MIGO(Z_RFC_AUFNR_MIGO RFCAufnrMigo,string TestFlag)
        {
            List<string> LsMsg = new List<string>();
            try
            {
                #region GM_CODE是和SAP的T-code相关
                /*01 MB01
                02 MB31
                03 MB1A
                04 MB1B
                05 MB1C
                06 MB11*/
                #endregion


                RfcDestination destination = RfcDestinationManager.GetDestination(this.GetCfgParameters());
                IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_AUFNR_MIGO");

                IRfcStructure IS_HEAD = rfcFunction.GetStructure("IS_HEAD");
                IS_HEAD.SetValue("GM_CODE", "04"); 
                IS_HEAD.SetValue("PSTNG_DATE", string.IsNullOrEmpty(RFCAufnrMigo.PSTNG_DATE) ? DateTime.Now.ToString("yyyyMMdd") : RFCAufnrMigo.PSTNG_DATE);
                IS_HEAD.SetValue("DOC_DATE", string.IsNullOrEmpty(RFCAufnrMigo.DOC_DATE) ? DateTime.Now.ToString("yyyyMMdd") : RFCAufnrMigo.DOC_DATE);
                IS_HEAD.SetValue("HEADER_TXT", RFCAufnrMigo.EMP_NO + RFCAufnrMigo.EMP_NAME);//人员权限
                rfcFunction.SetValue("IS_HEAD", IS_HEAD);   //设置参数         

                IRfcStructure IS_ITEM = rfcFunction.GetStructure("IS_ITEM");
                IS_ITEM.SetValue("MATERIAL", RFCAufnrMigo.PartNumber);
                IS_ITEM.SetValue("PLANT", "2100");
                IS_ITEM.SetValue("STGE_LOC", RFCAufnrMigo.STGE_LOC); //转出仓
                IS_ITEM.SetValue("MOVE_TYPE", "311");//仓库之间转移
                IS_ITEM.SetValue("ENTRY_QNT", RFCAufnrMigo.QTY);
                IS_ITEM.SetValue("MOVE_PLANT", "2100");
                IS_ITEM.SetValue("MOVE_STLOC", RFCAufnrMigo.MOVE_STLOC);        
                rfcFunction.SetValue("IS_ITEM", IS_ITEM);   //设置参数 

                rfcFunction.SetValue("I_TYPE", "3");//1成品入库 2出库  03 移库
                if (!string.IsNullOrEmpty(TestFlag))
                rfcFunction.SetValue("TESTRUN", "X"); //检查入库,不过账

                rfcFunction.Invoke(destination);

                string SAP_STOCKNO = rfcFunction.GetValue("E_MBLNR").ToString().TrimStart('0'); //物料凭证号码      
                IRfcStructure ES_RETURN = rfcFunction.GetStructure("ES_RETURN");
                string SAP_TYPE = ES_RETURN.GetValue("TYPE").ToString(); //是否成功 S 表示成功
                string SAP_E_ID = ES_RETURN.GetValue("ID").ToString();
                string SAP_E_NUM = ES_RETURN.GetValue("NUMBER").ToString();
                string SAP_MSG = ES_RETURN.GetValue("MESSAGE").ToString();


                LsMsg.Add(SAP_STOCKNO);
                LsMsg.Add(SAP_TYPE);
                LsMsg.Add(SAP_E_ID);
                LsMsg.Add(SAP_E_NUM);
                LsMsg.Add(SAP_MSG);
                return LsMsg;
            }
            catch (Exception ex)
            {
                LsMsg.Add(ex.Message);
                return LsMsg;
            }
        }

        private  DataTable Get_Z_RFC_AFPO(string woid)
        {
          //  this._strError = string.Empty;
            try
            {
                DataSet ds = new DataSet();
                System.Data.DataTable mDt = new System.Data.DataTable("Z_RFC_AFPO");
                mDt.Columns.Add("AUFNR", typeof(string));
                mDt.Columns.Add("PLNUM", typeof(string));
                mDt.Columns.Add("MATNR", typeof(string));
                mDt.Columns.Add("PSMNG", typeof(int));
                mDt.Columns.Add("MAKTX", typeof(string));
                mDt.Columns.Add("DAUAT", typeof(string));
                mDt.Columns.Add("DWERK", typeof(string));
                mDt.Columns.Add("GSTRI", typeof(string));
                mDt.Columns.Add("GLTRI", typeof(string));
                mDt.Columns.Add("STTXT", typeof(string));
                RfcDestination destination = RfcDestinationManager.GetDestination(this.GetCfgParameters());
                IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_AFPO");

                rfcFunction.SetValue("AUFNR", woid);

                rfcFunction.Invoke(destination);

                IRfcTable table = rfcFunction.GetTable("ZRFC_AFPO");

                for (int i = 0; i < table.RowCount; i++)
                {
                    mDt.Rows.Add(
                        table[i].GetString("AUFNR").TrimStart('0'),
                        table[i].GetString("PLNUM").TrimStart('0'),
                        table[i].GetString("MATNR").TrimStart('0'),
                        table[i].GetInt("PSMNG"),
                        table[i].GetString("MAKTX").TrimStart('0'),
                        table[i].GetString("DAUAT").TrimStart('0'),
                        table[i].GetString("DWERK").TrimStart('0'),
                        table[i].GetString("GSTRI").TrimStart('0'),
                        table[i].GetString("GLTRI").TrimStart('0'),
                        table[i].GetString("STTXT").TrimStart('0'));
                }
                ds.Tables.Add(mDt);
                return ds.Tables[0];
            }
            catch
            {
                //this._strError = "SAP Connect Error";
                return null;
            }
        }
        private RfcConfigParameters GetCfgParameters()
        {
            RfcConfigParameters rfcCfg = new RfcConfigParameters();
        
            rfcCfg.Add(RfcConfigParameters.Name,"MyConn");
            rfcCfg.Add(RfcConfigParameters.AppServerHost, "172.16.100.51");
            rfcCfg.Add(RfcConfigParameters.Client,"500" );
            rfcCfg.Add(RfcConfigParameters.User, "rfc");
            rfcCfg.Add(RfcConfigParameters.Password, "123456");
            rfcCfg.Add(RfcConfigParameters.SystemNumber, "00");
            rfcCfg.Add(RfcConfigParameters.Language,"ZH" );
            rfcCfg.Add(RfcConfigParameters.PoolSize,"5" );
            rfcCfg.Add(RfcConfigParameters.IdleTimeout,"500" );
            rfcCfg.Add(RfcConfigParameters.LogonGroup,"PUBLIC" );
            return rfcCfg;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            try
            {
                int ofLeft = 15;
                int ofTop = 50; //距顶
                int height = 20; //行距
                int ofWidth = 110;//水平间距
                int x = 3; //距顶,设置列高
                int xx = 0;
                int yy = ofTop * x - 10; //表头上下距离
                int Total = 0;
                //定义表头及格式


                e.Graphics.DrawString("万得凯实业有限公司", new Font("宋体", 18, FontStyle.Bold), Brushes.Black, ofLeft + 240, ofTop - 15);
                e.Graphics.DrawString("入库单", new Font("宋体", 18, FontStyle.Bold), Brushes.Black, ofLeft + 315, 70);
                e.Graphics.DrawString("-----------------------------------------------------------------------------------------------",
                    new Font("宋体", 12, FontStyle.Regular), Brushes.Black, 5, 96);

                e.Graphics.DrawString("-----------------------------------------------------------------------------------------------",
                  new Font("宋体", 12, FontStyle.Regular), Brushes.Black, 5, 125);

                e.Graphics.DrawString("入库单号:" , new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 3 * ofWidth - 8, ofTop * 2 + 10);
                e.Graphics.DrawString("入库日期:" + DateTime.Now.ToString("yyyy.MM.dd HH:mm"), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 6 * ofWidth - 150, ofTop * 2 + 10);

                e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 30, yy);

                e.Graphics.DrawString("序号", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft - 10, yy);

                e.Graphics.DrawString("工单", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 1 * ofWidth - 70, yy);

                e.Graphics.DrawString("料号", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 2 * ofWidth - 90, yy);

                e.Graphics.DrawString("品名/规格", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 3 * ofWidth - 125, yy);

                e.Graphics.DrawString("单位", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 4 * ofWidth + 40, yy);

                e.Graphics.DrawString("数量", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 5 * ofWidth - 15, yy);

                e.Graphics.DrawString("实/收发数量", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 6 * ofWidth + 30 - 100, yy);

                e.Graphics.DrawString("仓库", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 7 * ofWidth - 80, yy);

                e.Graphics.DrawString("备注", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 8 * ofWidth - 140, yy);

                int zz = 10;
                for (int i = 0; i <= zz * 7; i++)
                {
                    try
                    {
                        xx = (ofTop + (i + x + 3) * height) - 10;
                        string woId = "";
                        e.Graphics.DrawString((i + 1).ToString(), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft - 5, xx);
                        //工单
                        e.Graphics.DrawString(woId, new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 30, xx);
                        //料号
                        e.Graphics.DrawString("xxx", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 110, xx);
                        //品名
                        e.Graphics.DrawString("xxx", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 205, xx);

                        e.Graphics.DrawString("PC", new Font("宋体", 10, FontStyle.Regular), Brushes.Black, ofLeft + 485, xx);
                        //数量
                        e.Graphics.DrawString("xxx", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 538, xx);

                        e.Graphics.DrawString("________", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 600, xx - 1);

                        // e.Graphics.DrawString(dtStock.Rows[i][2].ToString(), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 690, xx);
                        e.Graphics.DrawString("_____", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 690, xx);

                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 730, xx);

                        Total = Total + Convert.ToInt32("xxx");
                    }
                    catch (Exception)
                    {

                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft - 5, xx);
                        //工单
                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 30, xx);
                        //料号
                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 110, xx);
                        //品名
                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 205, xx);

                        e.Graphics.DrawString("", new Font("宋体", 10, FontStyle.Regular), Brushes.Black, ofLeft + 485, xx);
                        //数量
                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 538, xx);

                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 600, xx - 1);

                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 690, xx);

                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 730, xx);
                    }
                }

                e.Graphics.DrawString("-----------------------------------------------------------------------------------------------",
                           new Font("宋体", 12, FontStyle.Regular), Brushes.Black, 5, xx + 30);

                e.Graphics.DrawString("合计:", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 4 * ofWidth + 40, xx + 50);
                e.Graphics.DrawString(Total.ToString(), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 4 * ofWidth + 100, xx + 50);
                e.Graphics.DrawString("生产部", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft, xx + 100);
                e.Graphics.DrawString("质量部", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 300, xx + 100);
                e.Graphics.DrawString("仓库", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 600, xx + 100);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }
    }
}
