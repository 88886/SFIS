using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;
using SystemObject;

namespace BLL
{
    public partial class tStorehouseManage
    {
        
        public tStorehouseManage()
        {
            
        }
        #region 新增 tttttttt
        public string AddStorehouse(string dicwarehouse)
        {          
            try
            {
                //MySqlCommand cmd = new MySqlCommand();
                //cmd.CommandText = "INSERT INTO SFCB.B_STOREHOUSE_INFO (STOREHOUSEID,STOREHOUSENAME,STOREHOUSEDESC,STOREHOUSEMAN,STOREHOUSETYPE,REMARK) " +
                //                  "VALUES  (@STID,@STNAME,@STDESC,@STMAN,@STYPE,@sREMARK)";
                //cmd.Parameters.AddRange(new MySqlParameter[]{
                //        new MySqlParameter("STID",MySqlDbType.VarChar){Value=warehouse.storehouseId} ,
                //        new MySqlParameter("STNAME",MySqlDbType.VarChar){Value=warehouse.storehousename},
                //        new MySqlParameter("STDESC",MySqlDbType.VarChar){Value=warehouse.storehousedesc},
                //        new MySqlParameter("STMAN",MySqlDbType.VarChar){Value=warehouse.storehouseman},
                //        new MySqlParameter("STYPE",MySqlDbType.VarChar){Value=warehouse.storehousetype},
                //        new MySqlParameter("sREMARK",MySqlDbType.VarChar){Value=warehouse.remark},
                //});
                //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicwarehouse);
                dp.AddData("SFCB.B_STOREHOUSE_INFO", mst);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }
        public string AddStorehouseloction(string dicwarehouseloc)
        {           
            try
            {
                //MySqlCommand cmd = new MySqlCommand();
                //cmd.CommandText = "INSERT INTO sfcr.t_storehouse_loction_info (LOCID, UPLOCID, LOCTYPE, STOREHOUSEID, LOCDESC, LOCTOTAL, REMARK) " +
                //                  "VALUES (@sLOCID,@sUPLOCID,@sLOCTYPE,@sSTOREHOUSEID,@sLOCDESC,@sLOCTOTAL,@sREMARK)";
                //cmd.Parameters.AddRange(new MySqlParameter[]{
                //   new MySqlParameter("sLOCID",MySqlDbType.VarChar){Value=warehouseloc.locId}, 
                //   new MySqlParameter("sUPLOCID",MySqlDbType.VarChar){Value=warehouseloc.uplocId}, 
                //   new MySqlParameter("sLOCTYPE",MySqlDbType.VarChar){Value=warehouseloc.loctype}, 
                //   new MySqlParameter("sSTOREHOUSEID",MySqlDbType.VarChar){Value=warehouseloc.storehouseId}, 
                //   new MySqlParameter("sLOCDESC",MySqlDbType.VarChar){Value=warehouseloc.locdesc}, 
                //   new MySqlParameter("sLOCTOTAL",MySqlDbType.VarChar){Value=warehouseloc.loctotal}, 
                //   new MySqlParameter("sREMARK",MySqlDbType.VarChar){Value=warehouseloc.remark}
                //});
                //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicwarehouseloc);
                dp.AddData("sfcr.t_storehouse_loction_info".ToUpper(), mst);
                return null;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public void AddStorehouselocType(string loctype)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "insert into SFCB.B_STOREHOUSE_LOCTION_TYPE (loctype,locdesc) values(@loctype,'NA')";
            //cmd.Parameters.Add("loctype", MySqlDbType.VarChar, 50).Value = loctype;
            // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("LOCTYPE", loctype);
            mst.Add("LOCDESC", "NA");
            dp.AddData("SFCB.B_STOREHOUSE_LOCTION_TYPE", mst);
        }
        #endregion
        #region 修改
        public void UpdateStorehouse(string dicwarehouse)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "update SFCB.T_STOREHOUSE_INFO set storehousename=@storehousename,storehousedesc=@storehousedesc,storehouseman=@storehouseman,storehousetype=@storehousetype,remark=@remark where storehouseId=@storehouseId";
            //cmd.Parameters.Add("storehouseId", MySqlDbType.VarChar, 50).Value = warehouse.storehouseId;
            //cmd.Parameters.Add("storehousename", MySqlDbType.VarChar, 50).Value = warehouse.storehousename;
            //cmd.Parameters.Add("storehousedesc", MySqlDbType.VarChar, 50).Value = warehouse.storehousedesc;
            //cmd.Parameters.Add("storehouseman", MySqlDbType.VarChar, 50).Value = warehouse.storehouseman;
            //cmd.Parameters.Add("storehousetype", MySqlDbType.VarChar, 50).Value = warehouse.storehousetype;
            //cmd.Parameters.Add("remark", MySqlDbType.VarChar, 50).Value = warehouse.remark;
            // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
             
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicwarehouse);
            dp.UpdateData("SFCB.T_STOREHOUSE_INFO", new string[] { "STOREHOUSEID" }, mst);
        }
        public void UpdateStorehouseloction(string  dicwarehouseloc)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText =
            //    "update sfcb.tStorehouseLoctionInfo set uplocId=@uplocId,loctype=@loctype,storehouseId=@storehouseId,locdesc=@locdesc,loctotal=@loctotal,remark=@remark where locId=@locId";
            //cmd.Parameters.Add("uplocId", MySqlDbType.VarChar, 50).Value = warehouseloc.uplocId;
            //cmd.Parameters.Add("loctype", MySqlDbType.VarChar, 50).Value = warehouseloc.loctype;
            //cmd.Parameters.Add("storehouseId", MySqlDbType.VarChar, 50).Value = warehouseloc.storehouseId;
            //cmd.Parameters.Add("locdesc", MySqlDbType.VarChar, 100).Value = warehouseloc.locdesc;
            //cmd.Parameters.Add("loctotal", MySqlDbType.Int32).Value = warehouseloc.loctotal;
            //cmd.Parameters.Add("remark", MySqlDbType.VarChar, 200).Value = warehouseloc.remark;
            //cmd.Parameters.Add("locId", MySqlDbType.VarChar, 50).Value = locid;
            // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicwarehouseloc);
            dp.UpdateData("SFCR.T_STOREHOUSE_LOCTION_INFO", new string[] { "LOCID" }, mst);
        }

      
        #endregion
        #region 删除
        //public void DeleteStorehouse(string warehouseid)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "delete from SFCB.T_STOREHOUSE_INFO where storehouseId=@storehouseId";
        //    cmd.Parameters.Add("storehouseId", MySqlDbType.VarChar, 50).Value = warehouseid;

        //     BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        //}
        public void DeleteStorehouseloction(string locid)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "delete from sfcb.tStorehouseLoctionInfo where locId=@locId";
            //cmd.Parameters.Add("locId", MySqlDbType.VarChar, 50).Value = locid;
            // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("LOCID", locid);
            dp.DeleteData("SFCR.T_STOREHOUSE_LOCTION_INFO", mst);
        }
        //public void DeleteStorehouselocType(string loctype)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "delete from SFCB.B_STOREHOUSE_LOCTION_TYPE where loctype=@loctype";
        //    cmd.Parameters.Add("loctype", MySqlDbType.VarChar, 50).Value = loctype;
        //     BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        //}
        #endregion
        #region 查询
        public System.Data.DataSet GetAlltStorehouseInfo()
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select storehouseId,storehousename,storehouseman,storehousetype,storehousedesc,remark from SFCB.B_STOREHOUSE_INFO";

            //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCB.B_STOREHOUSE_INFO";
            string fieldlist = "storehouseId,storehousename,storehouseman,storehousetype,storehousedesc,remark".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);            
            return dp.GetData(table, fieldlist,null, out count);
        }
        /// <summary>
        /// 获取所有库位信息
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GetAlltStorehouseLoctionInfo()
        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = @" select a.locId,a.loctype,a.storehouseId,a.loctotal,a.locdesc,a.uplocId,a.remark from sfcr.t_Storehouse_Loction_Info a, SFCB.B_STOREHOUSE_INFO b
//                               where a.storehouseId=b.storehouseId";

//            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            int count = 0;
            string table = "sfcr.t_Storehouse_Loction_Info a, SFCB.B_STOREHOUSE_INFO b".ToUpper();
            string fieldlist = "a.locId,a.loctype,a.storehouseId,a.loctotal,a.locdesc,a.uplocId,a.remark".ToUpper();
            string filter = "a.storehouseId=b.storehouseId".ToUpper();            
            return TransactionManager.GetData(table, fieldlist, filter, null, null, null, out count);
        }
        /// <summary>
        /// 获取仓库类型
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GettStorehouseType()
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select distinct storehousetype from SFCB.B_STOREHOUSE_INFO";
            //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCB.B_STOREHOUSE_INFO";
            string fieldlist = "distinct storehousetype".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            return dp.GetData(table, fieldlist, null, out count);
        }
        /// <summary>
        /// 根据仓库编号 获取仓库信息
        /// </summary>
        /// <param name="warehouseid"></param>
        /// <returns></returns>
        public System.Data.DataSet GettStorehouseInfoById(string warehouseid)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select storehouseId,storehousename,storehouseman,storehousetype,storehousedesc,remark from SFCB.B_STOREHOUSE_INFO where storehouseId=@stid";
            //cmd.Parameters.Add("stid", MySqlDbType.VarChar, 50).Value = warehouseid;
            //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCB.B_STOREHOUSE_INFO";
            string fieldlist = "storehouseId,storehousename,storehouseman,storehousetype,storehousedesc,remark".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("STOREHOUSEID", warehouseid);
            return dp.GetData(table, fieldlist, mst, out count);
        }
        /// <summary>
        /// 根据不同字段 获取库位信息
        /// </summary>
        /// <param name="_selname"></param>
        /// <param name="_value"></param>
        /// <returns></returns>
        public System.Data.DataSet GettStorehouseLoctionInfo(string _selname, string _value)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = string.Format("select a.locId,a.loctype,a.storehouseId,a.loctotal,a.locdesc,a.uplocId,a.remark from sfcr.t_storehouse_loction_info a,SFCB.B_STOREHOUSE_INFO b where a.storehouseId=b.storehouseId and  a.{0}=@{0}", _selname);
            //cmd.Parameters.Add(_selname, MySqlDbType.VarChar, 50).Value = _value;
            //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            int count = 0;
            string table = "sfcr.t_storehouse_loction_info a,SFCB.B_STOREHOUSE_INFO b".ToUpper();
            string fieldlist = "a.locId,a.loctype,a.storehouseId,a.loctotal,a.locdesc,a.uplocId,a.remark".ToUpper();
            string filter = string.Format("a.storehouseId=b.storehouseId and  a." + _selname + "={0}");            
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add(_selname, _value);
           return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);

        }
        /// <summary>
        /// 根据库位编号 获取库位信息
        /// </summary>
        /// <param name="locid"></param>
        /// <returns></returns>
        public System.Data.DataSet GettStorehouseLoctionInfoById(string locid)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select locId,loctype,storehouseId,loctotal,locdesc,uplocId,remark from sfcr.t_storehouse_loction_info where locId=@slocId";
            //cmd.Parameters.Add("slocId", MySqlDbType.VarChar, 50).Value = locid;
            //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "sfcr.t_storehouse_loction_info".ToUpper();
            string fieldlist = "locId,loctype,storehouseId,loctotal,locdesc,uplocId,remark".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("LOCID", locid);
            return dp.GetData(table, fieldlist, mst, out count);
        }
        /// <summary>
        /// 获取库位类型
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GettStorehouseLoctionType()
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select loctype from SFCB.B_STOREHOUSE_LOCTION_TYPE";

            //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            string table = "SFCB.B_STOREHOUSE_LOCTION_TYPE";
            string fieldlist = "LOCTYPE".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);            
            return dp.GetData(table, fieldlist, null, out count);
        }
        #endregion


        /// <summary>
        /// 查询仓库库位是否已经存在
        /// </summary>
        /// <param name="store"></param>
        /// <param name="locid"></param>
        /// <returns>存在返回真；不存在返回假</returns>
        public bool ChkStoreLocaltion(string store, string locid)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select locId from SFCR.T_STOREHOUSE_LOCTION_INFO where locId=@locId and storehouseId=@storehouseId";
            //cmd.Parameters.Add("locId", MySqlDbType.VarChar, 30).Value = locid;
            //cmd.Parameters.Add("storehouseId", MySqlDbType.VarChar, 30).Value = store;
            //object obj = BLL.BllMsSqllib.Instance.sqlExecuteScalar(cmd);
            string table = "SFCR.T_STOREHOUSE_LOCTION_INFO";
            string fieldlist = "locId".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("LOCID", locid);
            mst.Add("STOREHOUSEID", store);
            object obj = dp.GetData(table, fieldlist, mst, out count);

            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                return true;
            else
                return false;

        }

        /// <summary>
        /// 获取所有仓库的编号和对应的名称
        /// </summary>
        public System.Data.DataSet GetWarehouseList()
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select storehouseId,storehousename from SFCB.b_Storehouse_Info";
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCB.b_Storehouse_Info".ToUpper();
            string fieldlist = "storehouseId,storehousename".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            return dp.GetData(table, fieldlist, null, out count);
        }
        /// <summary>
        /// 根据参考编号获取该仓库下所有的库位编号列表
        /// </summary>
        /// <param name="storehouseId"></param>
        /// <returns></returns>
        public System.Data.DataSet GetWarehouseLoctionListBystorehouseId(string storehouseId)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select locId,locdesc,loctotal from SFCR.T_STOREHOUSE_LOCTION_INFO where storehouseId=@storehouseId";
            //cmd.Parameters.Add("storehouseId", MySqlDbType.VarChar, 20).Value = storehouseId;
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCR.T_STOREHOUSE_LOCTION_INFO";
            string fieldlist = "locId,locdesc,loctotal".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("STOREHOUSEID", storehouseId);          
            return dp.GetData(table, fieldlist, mst, out count);
        }

        public System.Data.DataSet GetAllWarehouseId()
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select a.storehouseId from sfcb.b_storehouse_info a";
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "sfcb.b_storehouse_info".ToUpper();
            string fieldlist = "storehouseId".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            return dp.GetData(table, fieldlist, null, out count);
        }

        public System.Data.DataSet GetAllLocIdByWarehouseId(string storehouseId)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select locId from SFCR.T_STOREHOUSE_LOCTION_INFO where storehouseId=@storehouseId";
            //cmd.Parameters.Add("storehouseId", MySqlDbType.VarChar, 20).Value = storehouseId;
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCR.T_STOREHOUSE_LOCTION_INFO";
            string fieldlist = "locId".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("STOREHOUSEID", storehouseId);
            return dp.GetData(table, fieldlist, mst, out count);
        }
        public DataSet GetLotIdByStorehouseId(string storehouseid)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select locId 库位编号,loctype 库位类型,storehouseId 仓库编号,loctotal 库位容量,locdesc 库位描述 from SFCR.T_STOREHOUSE_LOCTION_INFO where storehouseId=@storehouseid";

            //cmd.Parameters.Add("storehouseid", MySqlDbType.VarChar, 20).Value = storehouseid;
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCR.T_STOREHOUSE_LOCTION_INFO";
            string fieldlist = "locId 库位编号,loctype 库位类型,storehouseId 仓库编号,loctotal 库位容量,locdesc 库位描述".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("STOREHOUSEID", storehouseid);
            return dp.GetData(table, fieldlist, mst, out count);
        }

        public System.Data.DataSet GetWarehouseListInfo()
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select storehouseId 仓库编号,storehousename 仓库名称 from SFCB.b_Storehouse_Info ";
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCB.b_Storehouse_Info".ToUpper();
            string fieldlist = "storehouseId 仓库编号,storehousename 仓库名称".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            return dp.GetData(table, fieldlist, null, out count);
        }

        //public System.Data.DataSet GetPartNuByVp(string vpid)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "select * from sfcr.t_part_maps where venderkpnumber=@vpid";
        //    cmd.Parameters.Add("vpid", MySqlDbType.VarChar, 20).Value = vpid;
        //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //}
    }
}
