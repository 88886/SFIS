using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Data;
using System.Xml.Serialization;
using GenericUtil;


namespace TestWeserver
{
    /// <summary>
    /// tFixtureInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tFixtureInfo : System.Web.Services.WebService
    {
        BLL.tFixtureInfo fixture = new BLL.tFixtureInfo();
        MapListConverter mlc = new MapListConverter();

        [WebMethod]
        public string InsertFixtureInfo(string dicFixtureinfo)
        {
            return fixture.InsertFixtureInfo(dicFixtureinfo);
        }

        [WebMethod]
        public byte[] GetFixtureInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(fixture.GetFixtureInfo(null));
        }
        [WebMethod]
        public byte[] GetFixtureInfo_WinCE()
        {
            return mlc.GetDataSetZipBytes(fixture.GetFixtureInfo(null));
        }

        [WebMethod]
        public byte[] GetFixtureInfoByFixtureId(string fixtureid)
        {
            return mlc.GetDataSetSurrogateZipBytes(fixture.GetFixtureInfo(fixtureid));
        }
        [WebMethod]
        public byte[] GetFixtureInfoByFixtureId_WinCE(string fixtureid)
        {
            return mlc.GetDataSetZipBytes(fixture.GetFixtureInfo(fixtureid));
        }

        [WebMethod]
        public string InsertFixtureRegist(string dicFixtureregist)
        {
            return fixture.InsertFixtureRegist(dicFixtureregist);
        }
        [WebMethod]
        public byte[] GetFixtureRegist()
        {
            return mlc.GetDataSetSurrogateZipBytes(fixture.GetFixtureRegist(null));
        }
        [WebMethod]
        public byte[] GetFixtureRegist_WinCE()
        {
            return mlc.GetDataSetZipBytes(fixture.GetFixtureRegist(null));
        }

        [WebMethod]
        public byte[] GetFixtureRegistByFixtureId(string fixtureid)
        {
            return mlc.GetDataSetSurrogateZipBytes(fixture.GetFixtureRegist(fixtureid));
        }
        [WebMethod]
        public byte[] GetFixtureRegistByFixtureId_WinCE(string fixtureid)
        {
            return mlc.GetDataSetZipBytes(fixture.GetFixtureRegist(fixtureid));
        }

        [WebMethod]
        public void InsertFixtureType(string typeid, string typename)
        {
            fixture.InsertFixtureType(typeid, typename);
        }
        [WebMethod]
        public byte[] GetFixtureType()
        {
            return mlc.GetDataSetSurrogateZipBytes(fixture.GetFixtureType(null));
        }
        [WebMethod]
        public byte[] GetFixtureType_WinCE()
        {
            return mlc.GetDataSetZipBytes(fixture.GetFixtureType(null));
        }

        [WebMethod]
        public byte[] GetFixtureTypeByTypename(string typename)
        {
            return mlc.GetDataSetSurrogateZipBytes(fixture.GetFixtureType(typename));
        }
        [WebMethod]
        public byte[] GetFixtureTypeByTypename_WinCE(string typename)
        {
            return mlc.GetDataSetZipBytes(fixture.GetFixtureType(typename));
        }

        [WebMethod]
        public byte[] GetMaxMaintaoindate(string feederid)
        {
            return mlc.GetDataSetSurrogateZipBytes(fixture.GetMaxMaintaoindate(feederid));
        }
        [WebMethod]
        public byte[] GetMaxMaintaoindate_WinCE(string feederid)
        {
            return mlc.GetDataSetZipBytes(fixture.GetMaxMaintaoindate(feederid));
        }
        [WebMethod]
        public void UpdateFixtureInfo(string  dicFixtureinfo)
        {
            fixture.UpdateFixtureInfo(dicFixtureinfo);
        }
        [WebMethod]
        public void UpdateMaintaindate(string fixtureid, string fixturemaintaindate)
        {
            fixture.UpdateMaintaindate(fixtureid, fixturemaintaindate);
        }
        [WebMethod]
        public byte[] GetFixtureInfoBySizeAndManu(string feedersize, string manufacturer)
        {
            return mlc.GetDataSetSurrogateZipBytes(fixture.GetFixtureInfoBySizeAndManu(feedersize, manufacturer));
        }
        [WebMethod]
        public byte[] GetFixtureInfoBySizeAndManu_WinCE(string feedersize, string manufacturer)
        {
            return mlc.GetDataSetZipBytes(fixture.GetFixtureInfoBySizeAndManu(feedersize, manufacturer));
        }

        [WebMethod]
        public void DeleteFixtureInfoByFixtureId(string fixtureid)
        {
            fixture.DeleteFixtureInfoByFixtureId(fixtureid);
        }
        [WebMethod]
        public void UpdateFixtureRegist(string dicFixturegist)
        {
            fixture.UpdateFixtureRegist(dicFixturegist);
        }
         
    }
}
