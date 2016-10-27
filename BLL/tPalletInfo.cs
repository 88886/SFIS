using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using GenericProvider;
using GenericUtil;
using SystemObject;

namespace BLL
{
    public partial class tPalletInfo
    {
        public tPalletInfo()
        {
        }

        string table = "SFCR.T_PALLET_INFO";
        public void InsertPalletInfo(IDictionary<string,object> dic )
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);           
            dic.Add("RECDATE", System.DateTime.Now);
            dp.AddData(table, dic);
        }

        public DataSet GetPalletInfo(IDictionary<string, object> mst)
        {
            string fieldlist = "woid,palletnumber,line,partnumber,packtype,total,closeflag,computer".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);                     
            return dp.GetData(table, fieldlist, mst, out count);
        }


        public void UpdatePalletCloseFlag(string dicstring)
        {            
            List<string> TableKey = new List<string>();
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
            mst.Add("RECDATE", System.DateTime.Now);
            if (!mst.ContainsKey("WOID") || !mst.ContainsKey("LINE") || !mst.ContainsKey("PARTNUMBER"))
            {
                TableKey.Add("PALLETNUMBER");
            }
            else
            {
                TableKey.Add("WOID");
                TableKey.Add("PARTNUMBER");
                TableKey.Add("PALLETNUMBER");
            }
            dp.UpdateData(table, TableKey.ToArray(), mst);

        }

        public bool CheckCartonOrPalletClosed(string PALLET, int PackType, out int flag)
        {
            flag = 0; //0,正常,1 未找到Carton包装资料 2 Carton找到多笔资料 3 Carton未关闭    

            string fieldlist = "woid,palletnumber,line,partnumber,packtype,total,closeflag,computer".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("PALLETNUMBER", PALLET);
            mst.Add("PACKTYPE", PackType);
            DataTable dt = dp.GetData(table, fieldlist, mst, out count).Tables[0];
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows.Count > 1)
                {
                    flag = 2;
                    return false;
                }

                if ((Convert.ToInt32(dt.Rows[0]["closeflag"].ToString())) == 0)
                {
                    flag = 3;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                flag = 1;
                return false;
            }
        } 

        List<IAsyncResult> ListIasyncresult = new List<IAsyncResult>();
        delegate string runEvent(List<string> ListData, string MYGROUP, string EMP, string EC, string LINE, int Total);
     //   runEvent deleevnet;
        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void CallBackMethod(IAsyncResult ar)
        {
            string msg = string.Empty;
            AsyncResult result = (AsyncResult)ar;
            runEvent re = (runEvent)result.AsyncDelegate;
            msg = re.EndInvoke(ar);
            //将值传递出去
            ErrMsg obj = (ErrMsg)ar.AsyncState;
            obj.errmsg.Add(msg);
        }
     

        public void InsertPalletInfohad(string PALLET, string PartNo, string Model, string Ver, string woId)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("PALLETNUMBER", PALLET);
            dic.Add("PARTNUMBER", PartNo);
            dic.Add("PRODUCTNAME", Model);
            dic.Add("PRODUCTVERSION", Ver);
            dic.Add("WOID", woId);
            dp.AddData("SFCR.T_PALLET_INFO_HAD", dic);
        }
        public string InsertPalletAndMpalletInfo(string PALLET,string MPALLET,string CTN,string MCTN,int QTY,string woId,string Part)
        {
            string _StrErr = string.Empty;
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("PALLETNUMBER", PALLET);
                dic.Add("MPALLETNUMBER", MPALLET);
                dic.Add("CARTONNUMBER", CTN);
                dic.Add("MCARTONNUMBER", MCTN);
                dic.Add("CTNQTY", QTY);
                dic.Add("WOID", woId);
                dic.Add("PARTNUMBER", Part);
                dp.AddData("SFCR.T_PALLET_INFO_DTA", dic);
                _StrErr = "OK";
            }
            catch (Exception ex)
            {
                _StrErr = ex.Message;
            }
            return _StrErr;
        }
        public System.Data.DataSet GetPalletAndMpalletInfo(string PalletNo,int Flag)
        {            

            string fieldlist = "PALLETNUMBER,MPALLETNUMBER,CARTONNUMBER,MCARTONNUMBER,CTNQTY,WOID,PARTNUMBER".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            switch (Flag)
            {
                case 1:
                    mst.Add("MPALLETNUMBER", PalletNo); 
                    break;
                default:
                    mst.Add("PALLETNUMBER", PalletNo); 
                    break;
            }
             
            return dp.GetData("SFCR.T_PALLET_INFO_DTA", fieldlist, mst, out count);
        }

        public string[] GetPalletInfohad(string Pallet)
        {
            List<string> listpalt = new List<string>();        

            string fieldlist = "palletnumber,partnumber,productname,productversion".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("PALLETNUMBER", Pallet);
            DataTable dt = dp.GetData("SFCR.T_PALLET_INFO_HAD", fieldlist, mst, out count).Tables[0];
     
            if (dt.Rows.Count != 0)
            {
                listpalt.Add(dt.Rows[0][0].ToString());
                listpalt.Add(dt.Rows[0][1].ToString());
                listpalt.Add(dt.Rows[0][2].ToString());
                listpalt.Add(dt.Rows[0][3].ToString());
            }
            return listpalt.ToArray();
        }


        // 获取当前表中closedflag=1 并且 palletnumber=刷入的号码
        public System.Data.DataSet GettPalletInfo(string palletnumber)
        {          
            string fieldlist = "woid, palletnumber, line, partnumber, packtype, total, closeflag, recdate, computer".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("PALLETNUMBER", palletnumber);
            mst.Add("CLOSEFLAG", 1);
            return dp.GetData("SFCR.T_PALLET_INFO", fieldlist, mst, out count);
        }

        public DataSet GetPalletNnmberInfoForCarton(string woId,string Pallet)
        {           
            string fieldlist = "woid,palletnumber,line,partnumber,packtype,total,closeflag,computer";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", woId);
            mst.Add("PALLETNUMBER", Pallet);
            return dp.GetData("SFCR.T_PALLET_INFO", fieldlist, mst, out count);
        }      

        public string Get_Carton_No(string woId,string LineCode)
        {            
                string Prefix = woId + "C";
                string cartonno = string.Empty;        

                int count = 0;

                string fieldlist = " MAX(PALLETNUMBER)";
                string filter = "PALLETNUMBER LIKE {0}";                 
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("PALLETNUMBER", Prefix + "%");
                DataTable dt = TransactionManager.GetData("SFCR.T_PALLET_INFO", fieldlist, filter, mst, null, null, out count).Tables[0];


                if (dt.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                    {
                        cartonno = Prefix + "0001";
                    }
                    else
                    {
                        int num = Convert.ToInt32(dt.Rows[0][0].ToString().Substring(Prefix.Length, 4)); ;
                        cartonno = Prefix + (num + 1).ToString().PadLeft(4, '0');
                    }
                }
                else
                {
                    cartonno = Prefix + "0001";
                }
                return cartonno;           
        }
        public DataSet Get_Pallet_Info_bywo(string woId, int packtype, int closeflag)
        {           

            string fieldlist = "woid,palletnumber,line,partnumber,packtype,total,closeflag,computer";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", woId);
            mst.Add("PACKTYPE", packtype);
            mst.Add("CLOSEFLAG", closeflag);
            return dp.GetData("SFCR.T_PALLET_INFO", fieldlist, mst, out count);
        }
    }
    public class ErrMsg
    {
        public List<string> errmsg = new List<string>();
    }
    class SimpleAsyncResult : IAsyncResult, IDisposable
    {
        private readonly object m_stateObj;
        private ManualResetEvent m_event;
        private bool m_isCompleted;
        private Exception m_exception;
        public SimpleAsyncResult(object stateObj)
        {
            m_stateObj = stateObj;
        }
        public object AsyncState
        {
            get { return m_stateObj; }
        }
        /// <summary>
        /// Always try to avoid use this property. It intanstiates the WaitHandle object which is 
        /// expensive. What's more, disposal of the WaitHandle is totally up to the caller or GC.
        /// </summary>
        public WaitHandle AsyncWaitHandle
        {
            get
            {
                //double check for safety
                if (m_event == null)
                {
                    bool isCompeleted = m_isCompleted;
                    lock (this)
                    {
                        if (m_event == null)
                        {
                            // initialized as Set/Unset according to IsCompleted
                            m_event = new ManualResetEvent(isCompeleted);
                        }
                    }
                    // IsCompleted status might changed during the creation of m_event. 
                    // Signal it now; otherwise, caller might wait on it forever.
                    if (!isCompeleted && m_isCompleted)
                    {
                        m_event.Set();
                    }
                }
                return m_event;
            }
        }
        /// <summary>
        /// Always return false.
        /// </summary>
        public bool CompletedSynchronously
        {
            get { return false; }
        }
        public bool IsCompleted
        {
            get { return m_isCompleted; }
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                IDisposable res = this.m_event as IDisposable;
                if (res != null)
                {
                    res.Dispose();
                }
            }
        }
        internal void Complete()
        {
            m_isCompleted = true;
            if (m_event != null) // Don't use this.WaitHandle property here.
            {
                m_event.Set();
            }
        }
        internal void Complete(Exception ex)
        {
            m_exception = ex;
            Complete();
        }
    } 
}
