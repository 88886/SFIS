using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;
using SrvComponent;

namespace BLL
{
    public partial class tCraftInfo
    {
       
        public tCraftInfo()
        {
           
        }
      

        /// <summary>
        /// SFCB.B_CRAFT_INFO
        /// </summary>
        string table = "SFCB.B_CRAFT_INFO";
        #region 工艺
        /// <summary>
        /// 获取所有工艺名称，craftId,craftname,craftparamet,beworkseg
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GetAllCraftInfo(IDictionary<string, object> mst)
        {          
            string fieldlist = "craftId,craftname,craftparameterurl,beworkseg,testflag,checktoolsflag".ToUpper();      
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            return dp.GetData(table, fieldlist, mst, out count);
        }
       

        public System.Data.DataSet GetAllCraftInfo2()
        {          
            string fieldlist = "0 as chk, craftId,craftname,craftparameterurl,beworkseg,checktoolsflag".ToUpper();         
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            return dp.GetData(table, fieldlist, null, out count);
        }

        /// <summary>
        /// 获取工艺，craftId,craftname,craftparamet,beworkseg
        /// </summary>
        /// <param name="craftId"></param>
        /// <returns></returns>
        public System.Data.DataSet GetCraftInfoByCraftId(string craftId)
        {           
            string fieldlist = "craftId,craftname,craftparameterurl,beworkseg,checktoolsflag";
        

            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("craftId", craftId);
            return dp.GetData(table, fieldlist, mst, out count);
        }       

        #endregion

        #region 工艺项目
        /// <summary>
        /// 添加工艺项目及参数
        /// </summary>
        /// <param name="craftitem"></param>
        /// <param name="err"></param>
        public void InsertCraftItem(Dictionary<string,object> dic, out string err)
        {         
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                dp.AddData("SFCB.B_CRAFT_ITEM", dic);
                err = "OK";
            }
            catch (Exception ex)
            {
                err= ex.Message;
            }
        }

        /// <summary>
        /// 添加工艺项目及参数(先删除所有存在的工艺项目Id)
        /// </summary>
        /// <param name="lsCraftItem"></param>
        /// <param name="err"></param>
        public void InsertCraftItem(string craftId, string Lsdicstring, out string err)
        {
            err = "";          
            IDictionary<string, object> dicCft = MapListConverter.JsonToDictionary(craftId);
            DeleteCraftItem(dicCft["CRAFTID"].ToString());
            foreach (Dictionary<string,object> dic in MapListConverter.JsonToListDictionary(Lsdicstring))
            {
                InsertCraftItem(dic, out err);               
                if (!string.IsNullOrEmpty(err))
                    throw new Exception(err);
            }
        }
        /// <summary>
        /// 获取所有的工艺项目及参数
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GetAllCraftItme()
        {            
            string fieldlist = "craftId,craftItem,craftparameterdes,upperlimit,lowerlimit,other";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            return dp.GetData("SFCB.B_CRAFT_ITEM", fieldlist, null, out count);
        }

        /// <summary>
        /// 根据工艺Id获取对应的工艺项目
        /// </summary>
        /// <param name="craftId"></param>
        /// <returns></returns>
        public System.Data.DataSet GetCraftItemByCraftId(string craftId)
        {           
            string fieldlist = "craftId,craftItem,craftparameterdes,upperlimit,lowerlimit,other";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("craftId", craftId);
           return dp.GetData("SFCB.B_CRAFT_ITEM", fieldlist, mst, out count);
        }

        /// <summary>
        /// 删除工艺项目
        /// </summary>
        /// <param name="craftId"></param>
        public void DeleteCraftItem(string craftId)
        {         
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("craftId", craftId);
            dp.DeleteData("SFCB.B_CRAFT_ITEM", mst);
        }
        #endregion

        /// <summary>
        /// 添加整个工艺及对应的工艺项目
        /// </summary>
        /// <param name="craftinfo"></param>
        /// <param name="lsCraftItem"></param>
        /// <param name="err"></param>
        public string InsertRefCraftInfo(string craftinfo, string lsCraftItem, out string err)
        {
            err = "";
            try
            {
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(craftinfo);
               
                   DataTable dt = GetCraftInfoByCraftId(mst["CRAFTNAME"].ToString()).Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {                      

                        IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);

                        mst.Remove("CRAFTID");
                        mst.Remove("BEWORKSEG");
                        mst.Remove("CRAFTPARAMETERURL");                       
                        dp.UpdateData(table, new string[] { "CRAFTNAME" }, mst);
                        return "OK";
                    }
                    string RES = InsertCraftInfo_SP(craftinfo);
                    InsertCraftItem(craftinfo, lsCraftItem, out err);
                return RES;
            }
            catch (Exception EX)
            {
                return EX.Message;
            }
        }

        private string InsertCraftInfo_SP(string craftinfo)
        {
            try
            {
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(craftinfo);
                
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst2 = new Dictionary<string, object>();
                mst2.Add("CRAFTID", mst["CRAFTID"]);
                dp.DeleteData("SFCB.B_CRAFT_ITEM", mst2);
                dp.DeleteData("SFCB.B_CRAFT_INFO", mst2);

                dp.AddData("SFCB.B_CRAFT_INFO", mst);               


                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        #region 20120913
        /// <summary>
        /// 获取所有类型的制程段
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllWorksegment()
        {         
            string fieldlist = "beworkseg";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            return dp.GetData("SFCB.B_WORK_SEGMENT", fieldlist, null, out count);
        }


        /// <summary>
        /// 增加制程段
        /// </summary>
        /// <param name="bworksegment"></param>
        /// <returns></returns>
        public void InsertWorkSegment(string bworksegment)
        {        
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);   
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("beworkseg", bworksegment);
            dp.AddData("SFCB.B_WORK_SEGMENT", mst);
        }

        /// <summary>
        /// 获取途程名
        /// </summary>
        /// <param name="bworksegment"></param>
        /// <returns></returns>
        public List<string> GetCraftInfoCraftparameterurl()
        {
            List<string> Craftparameterurl = new List<string>();
       
            System.Data.DataTable _dt = GetAllCraftInfo(null).Tables[0];

            foreach (DataRow item in _dt.Rows)
            {
                Craftparameterurl.Add(item["Craftparameterurl"].ToString());
            }

            return Craftparameterurl;
        }
        #endregion
    }
}
