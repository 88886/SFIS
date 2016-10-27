using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tz_whs_move_store 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tz_whs_move_store : System.Web.Services.WebService
    {
      /*  BLL.tz_whs_move_store mz_whs_move_store = new BLL.tz_whs_move_store();
        MapListConverter mlc = new MapListConverter();

        /// <summary>
        /// 新增移库单信息
        /// </summary>
        /// <param name="TWI"></param>
        /// <param name="Err"></param>
        [WebMethod]
        public string Insertmove_store_id(Entity.tz_whs_move_storeTable insertmsi)
        {
            return mz_whs_move_store.insertmove_store_id(insertmsi);
        }

        [WebMethod]
        public string Sel_MOVEWH_ID()
        {
            return mz_whs_move_store.Sel_MOVEWH_ID();

        }

        [WebMethod]
        public byte[] Getmovestoreid(string MOVE_STORE_ID, string PARTNUMBER)
        {
            return mlc.GetDataSetSurrogateZipBytes(mz_whs_move_store.Getmove_store_id(MOVE_STORE_ID, PARTNUMBER));
        }

        /// <summary>
        /// 新增移库信息
        /// </summary>
        /// <param name="TWI"></param>
        /// <param name="Err"></param>
        [WebMethod]
        public string insertz_whs_detail(string esn, string userid, string lotcode)
        {
            return mz_whs_move_store.insertz_whs_detail(esn, userid, lotcode);
        }

        /// <summary>
        /// 关闭移库单状态
        /// </summary>
        /// <param name="TWI"></param>
        /// <param name="Err"></param>
        [WebMethod]
        public string move_store_id_status(string move_store_id, string partnumber, string status)
        {
            return mz_whs_move_store.move_store_id_status(move_store_id, partnumber, status);
        }

        [WebMethod]
        public byte[] Getmovestoreinfo(string MOVE_STORE_ID)
        {
            return mlc.GetDataSetSurrogateZipBytes(mz_whs_move_store.Getmove_store_info(MOVE_STORE_ID));
        }

        [WebMethod]
        public byte[] Getstore_info(string MOVE_STORE_ID)
        {
            return mlc.GetDataSetSurrogateZipBytes(mz_whs_move_store.Getstore_info(MOVE_STORE_ID));
        }

        [WebMethod]
        public byte[] Getstorebymoveid(string move_store_id, string immigration_store)
        {
            return mlc.GetDataSetSurrogateZipBytes(mz_whs_move_store.Getstorebymoveid(move_store_id, immigration_store));
        }

        [WebMethod]
        public byte[] Getmove_store_qty(string MOVE_STORE_ID)
        {
            return mlc.GetDataSetSurrogateZipBytes(mz_whs_move_store.Getmove_store_qty(MOVE_STORE_ID));
        }
        [WebMethod]
        public byte[] Get_Z_WHS_MOVE_STORE()
        {
            return mlc.GetDataSetSurrogateZipBytes(mz_whs_move_store.Get_Z_WHS_MOVE_STORE());
        }
        [WebMethod]
        public string Update_Z_WHS_MOVE_STORES(string Rowid)
        {
            return mz_whs_move_store.Update_Z_WHS_MOVE_STORES(Rowid);
        }

         */
    }
}
