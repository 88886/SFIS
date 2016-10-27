using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Web.Services;
using GenericProvider;
using GenericUtil;

namespace BLL
{
    public partial class tFixtureInfo
    {
        public tFixtureInfo()
        {
        }

        string table = "SFCR.T_FIXTURE_INFO";
        public System.Data.DataSet GetFixtureInfo(string fixtureid)
        {           
            string fieldlist = "fixtureId,Manufacturer,fixturesize,case fixturestate when 0 then '在库' when 1 then '使用' when 2 then '报废' when 3 then '维护' end as fixturestate,fixturebegingdate,fixturemaintaindate,fixtureNote".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(fixtureid))
                mst.Add("fixtureId", fixtureid);
            return dp.GetData(table, fieldlist, mst, out count);
        }
        /// <summary>
        /// 插入设备信息
        /// </summary>
        public string InsertFixtureInfo(string dicFixtureinfo)
        {           
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicFixtureinfo);
                dp.AddData(table, dic);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
        /// <summary>
        /// 插入设备维护信息
        /// </summary>
        public string InsertFixtureRegist(string dicFixtureregist)
        {     
            try
            {                
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicFixtureregist);
                dp.AddData("SFCR.T_FIXTURE_REGIST", dic);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }   

        public System.Data.DataSet GetFixtureRegist(string fixtureid)
        {
          
            string fieldlist = "fixtureId,fixtureItem,fixturecontext,fixturestartdate,fixtureenddate,fixturenote".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(fixtureid))
                mst.Add("fixtureId", fixtureid);
            return dp.GetData("SFCR.T_FIXTURE_REGIST", fieldlist, mst, out count);
        } 

        public void InsertFixtureType(string typeid, string typename)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("typeId", typeid);
            dic.Add("typename", typename);
            dp.AddData("SFCR.T_FIXTURE_TYPE", dic);
        }
        public System.Data.DataSet GetFixtureType(string typename)
        {           

            string fieldlist = "typeId,typename".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(typename))
                mst.Add("typename", typename);
            return dp.GetData("SFCR.T_FIXTURE_TYPE", fieldlist, mst, out count);
        }
    
        public  System.Data.DataSet GetMaxMaintaoindate(string feederid)
        {          
            string fieldlist = "MAX(fixtureenddate) as fixtureenddate".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("fixtureItem", "保养");
            mst.Add("fixtureId", feederid);
            return dp.GetData("SFCR.T_FIXTURE_REGIST", fieldlist, mst, out count);
        }
        public void UpdateFixtureRegist(string dicFixturegist)
        {
          
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicFixturegist);
            dp.UpdateData("SFCR.T_FIXTURE_REGIST", new string[] { "fixtureId" }, mst);
        }
        public  void UpdateFixtureInfo(string dicFixtureinfo)
        {           
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicFixtureinfo);
            dp.UpdateData("SFCR.T_FIXTURE_INFO", new string[] { "fixtureId" }, mst);
        }

        public  void UpdateMaintaindate(string fixtureid, string fixturemaintaindate)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("fixturemaintaindate",fixturemaintaindate);
            mst.Add("fixtureId", fixtureid);
            dp.UpdateData("SFCR.T_FIXTURE_INFO", new string[] { "fixtureId" }, mst);
        }

        public System.Data.DataSet GetFixtureInfoBySizeAndManu(string feedersize, string manufacturer)
        {
            string fieldlist = "fixtureId,Manufacturer,fixturesize,case fixturestate when 0 then '在库' when 1 then '使用' when 2 then '报废' when 3 then '维护' end as fixturestate,fixturebegingdate,fixturemaintaindate,fixtureNote";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(feedersize))
                mst.Add("fixturesize", feedersize);
            mst.Add("manufacturer", manufacturer);
            return dp.GetData("SFCR.T_FIXTURE_INFO", fieldlist,  mst, out count);
        }

        public void DeleteFixtureInfoByFixtureId(string fixtureid)
        {         
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("fixtureId", fixtureid);
            dp.DeleteData("SFCR.T_FIXTURE_INFO", mst);
        }
    }
}
