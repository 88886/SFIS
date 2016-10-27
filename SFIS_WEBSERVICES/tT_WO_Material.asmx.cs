using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tT_WO_Material 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tT_WO_Material : System.Web.Services.WebService
    {
        BLL.Db_Procedure mPro = new BLL.Db_Procedure();
        BLL.tT_WO_Material woMaterial = new BLL.tT_WO_Material();

      
        [WebMethod]
        public string Insert_T_WO_Material_Update_Status(string JsonStr)
        {
            IDictionary<string,object> mst = MapListConverter.JsonToDictionary(JsonStr);

            string _StrErr = string.Empty;
            int Status = Convert.ToInt32(mst["STATUS"].ToString());
            if (Status==2)
                _StrErr = woMaterial.Insert_T_WO_Material(mst["TR_SN"].ToString(), mst["USER_ID"].ToString(), Status.ToString());
            if (Status == 1) //撤销发料
                _StrErr = woMaterial.Delete_T_WO_Material(mst["TR_SN"].ToString(), mst["WOID"].ToString());
              
              if (_StrErr=="OK")
                  _StrErr = mPro.UPDATE_TR_SN(mst["TR_SN"].ToString(), "NA", mst["USER_ID"].ToString(), Status.ToString(), "NA", "NA");
            return _StrErr;          

        }

        [WebMethod(MessageName = "Insert_T_WO_Material")]
        public string Insert_T_WO_Material(string JsonStr)
        {
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(JsonStr);
          return  woMaterial.Insert_T_WO_Material(mst);
        }
         [WebMethod(MessageName = "UPDATE_T_WO_Material")]
        public string UPDATE_T_WO_Material(string JsonStr)
        {
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(JsonStr);
            mst.Add("RECDATE",System.DateTime.Now);
            return woMaterial.UPDATE_T_WO_Material(mst);
        }
    }
}
