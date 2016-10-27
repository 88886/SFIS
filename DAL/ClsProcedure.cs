using System;
using System.Collections.Generic;
using System.Text;
using Entity;
//using Oracle.DataAccess.Client;
using MySql.Data.MySqlClient;
using System.Data;
namespace DAL
{
    public partial class MsSqlLib : DAL.IMsSqlLib
    {      
       
     /*   #region 料站表操作存储过程
        /// <summary>
        /// 新增料站表头
        /// </summary>
        /// <param name="KPMarster">表头实体</param>
        /// <param name="marsterId">返回的表头ID:如果等于表头实体的masterId则为新增,不相等则为更新操作</param>
        /// <param name="Err">是否有错误</param>
        public void InsertSmtKPMarster(Entity.SMT_KP_MASTER KPMarster, out string marsterId, out string Err)
        {
            marsterId = string.Empty;
            Err = string.Empty;
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("pro_Insert_Smt_kp_master", _conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        #region 输入参数
                        //cmd.Parameters.Add("masterId", OracleDbType.Varchar2);
                        //cmd.Parameters["masterId"].Value = KPMarster.masterId.ToString();

                        cmd.Parameters.Add("V_LINEID", OracleDbType.Varchar2);
                        cmd.Parameters["V_LINEID"].Value = KPMarster.Lineid;

                        cmd.Parameters.Add("V_USERID", OracleDbType.Varchar2);
                        cmd.Parameters["V_USERID"].Value = KPMarster.Userid;

                        cmd.Parameters.Add("V_PARTNUMBER", OracleDbType.Varchar2);
                        cmd.Parameters["V_PARTNUMBER"].Value = KPMarster.partnumber;

                        cmd.Parameters.Add("V_MODELNAME", OracleDbType.Varchar2);
                        cmd.Parameters["V_MODELNAME"].Value = KPMarster.modelname;

                        cmd.Parameters.Add("V_BOMVER", OracleDbType.Varchar2);
                        cmd.Parameters["V_BOMVER"].Value = KPMarster.bomver;

                        cmd.Parameters.Add("V_PCBSIDE", OracleDbType.Varchar2);
                        cmd.Parameters["V_PCBSIDE"].Value = KPMarster.pcbside;

                        cmd.Parameters.Add("V_RESERVE1", OracleDbType.Varchar2);
                        cmd.Parameters["V_RESERVE1"].Value = KPMarster.reserve1;

                        cmd.Parameters.Add("V_RESERVE2", OracleDbType.Varchar2);
                        cmd.Parameters["V_RESERVE2"].Value = KPMarster.reserve2;
                        #endregion
                        #region 输出参数
                        cmd.Parameters.Add("V_OUTMASTERID", OracleDbType.Varchar2, 50);
                        cmd.Parameters["V_OUTMASTERID"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("V_ERROR", OracleDbType.Varchar2, 500);
                        cmd.Parameters["V_ERROR"].Direction = ParameterDirection.Output;
                        #endregion
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();

                        marsterId = cmd.Parameters["V_OUTMASTERID"].Value.ToString();
                        Err = cmd.Parameters["V_ERROR"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }

        /// <summary>
        /// 新增料站表身资料
        /// </summary>
        /// <param name="KPDetalt">料站表身实体</param>
        /// <param name="Err"></param>
        public void InsertSmtKPDetalt(Entity.SMT_KP_DETALT KPDetalt, out string Err)
        {
            string ss = "";
            Err = "";
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {

                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("pro_Insert_Smt_kp_detalt", _conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        ss = "1";
                        cmd.Parameters.Add("STATIONNO", OracleDbType.Varchar2);
                        cmd.Parameters["STATIONNO"].Value = KPDetalt.Stationno;
                        ss = "2";
                        cmd.Parameters.Add("MASTERID", OracleDbType.Varchar2);
                        cmd.Parameters["MASTERID"].Value = KPDetalt.Masterid;
                        ss = "3";
                        cmd.Parameters.Add("KPNUMBER", OracleDbType.Varchar2);
                        cmd.Parameters["KPNUMBER"].Value = KPDetalt.KPNumber;
                        ss = "4";
                        cmd.Parameters.Add("KPDESC", OracleDbType.Varchar2, 500);
                        cmd.Parameters["KPDESC"].Value = KPDetalt.KPDesc;
                        ss = "5";
                        cmd.Parameters.Add("KPDISTINCE", OracleDbType.Varchar2);
                        cmd.Parameters["KPDISTINCE"].Value = KPDetalt.KPDistinct ? 0 : 1;
                        ss = "6";
                        cmd.Parameters.Add("PRIORITYHCLASS", OracleDbType.Int32);
                        cmd.Parameters["PRIORITYHCLASS"].Value = KPDetalt.Priorityclass;
                        ss = "7";
                        if (KPDetalt.Loction.Length != KPDetalt.loctionLen)
                            throw new Exception("零件位置被截断,请联系工程人员处理");
                        cmd.Parameters.Add("LOCTION", OracleDbType.Varchar2);
                        cmd.Parameters["LOCTION"].Value = KPDetalt.Loction;
                        ss = "8";
                        cmd.Parameters.Add("RESERVE1", OracleDbType.Varchar2);
                        cmd.Parameters["RESERVE1"].Value = KPDetalt.reserve1;
                        ss = "9";
                        cmd.Parameters.Add("RESERVE2", OracleDbType.Varchar2);
                        cmd.Parameters["RESERVE2"].Value = KPDetalt.reserve;
                        ss = "10";
                        cmd.Parameters.Add("REPLACEGROUP", OracleDbType.Varchar2);
                        cmd.Parameters["REPLACEGROUP"].Value = KPDetalt.Replacegroup;
                        ss = "11";
                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 500);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();
                        ss = "12";
                        Err = cmd.Parameters["RES"].Value.ToString();
                        ss = "13";
                    }
                }
                catch (Exception ex)
                {
                    Err ="["+ss+"]"+ex.Message;
                  //  throw ex;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }

        #endregion       */

        ///// <summary>
        ///// 新增生产工单信息
        ///// </summary>
        ///// <param name="twi">工单表实体</param>
        ///// <param name="err">错误返回</param>
        //public string InsertWoInfo(Entity.T_WO_INFO twi, string esn, List<Entity.WoSnRule> lswosnrule)
        //{
          
        //}  
        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="product"></param>
        /// <param name="Err"></param>
    /*    public string InsertProcut(Entity.tProduct product, List<Entity.tProductSerialInfo> lsserialinfo,
            Entity.tPackParametersTable tParam)
        {
            string Err = string.Empty;
            using (MySqlConnection _conn = new MySqlConnection(this._strDBString))
            {
                int aa = 0;
                try
                {
                    _conn.Open();
                    #region 产品头
                    using (  cmd = new OracleCommand("pro_InsertProduct", _conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("PARTNUMBER", OracleDbType.Varchar2);
                        cmd.Parameters["PARTNUMBER"].Value = product.partnumber;

                        cmd.Parameters.Add("SORTNAME", OracleDbType.Varchar2);
                        cmd.Parameters["SORTNAME"].Value = product.sortname;

                        cmd.Parameters.Add("PRODUCTNAME", OracleDbType.Varchar2);
                        cmd.Parameters["PRODUCTNAME"].Value = product.pdname;

                        cmd.Parameters.Add("PRODUCTCOLOR", OracleDbType.Varchar2);
                        cmd.Parameters["PRODUCTCOLOR"].Value = product.pdcolor;

                        cmd.Parameters.Add("PRODUCTDESC", OracleDbType.Varchar2);
                        cmd.Parameters["PRODUCTDESC"].Value = product.pddes;

                        cmd.Parameters.Add("OTHER", OracleDbType.Varchar2);
                        cmd.Parameters["OTHER"].Value = product.other;

                        cmd.Parameters.Add("VERSIONCODE", OracleDbType.Varchar2);
                        cmd.Parameters["VERSIONCODE"].Value = tParam.VersionCode;

                        cmd.Parameters.Add("TRAYQTY", OracleDbType.Int32);
                        cmd.Parameters["TRAYQTY"].Value = tParam.TrayQty;

                        cmd.Parameters.Add("CARTONQTY", OracleDbType.Int32);
                        cmd.Parameters["CARTONQTY"].Value = tParam.CartonQty;

                        cmd.Parameters.Add("PALLETQTY", OracleDbType.Int32);
                        cmd.Parameters["PALLETQTY"].Value = tParam.PalletQty;

                        cmd.Parameters.Add("SOTION", OracleDbType.Varchar2);
                        cmd.Parameters["SOTION"].Value = product.solution;

                        cmd.Parameters.Add("BARLEN", OracleDbType.Varchar2);
                        cmd.Parameters["BARLEN"].Value = product.BarcodeLen;

                        cmd.Parameters.Add("PRODSN", OracleDbType.Varchar2);
                        cmd.Parameters["PRODSN"].Value = product.Productsn;

                        cmd.Parameters.Add("NALPREFIX", OracleDbType.Varchar2);
                        cmd.Parameters["NALPREFIX"].Value = product.NALPREFIX;               


                        cmd.Parameters.Add("ERROR", OracleDbType.Varchar2,10);
                        cmd.Parameters["ERROR"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();

                        Err = cmd.Parameters["ERROR"].Value.ToString();
                    }
                    #endregion
                    aa = 1;
                    if (Err!="OK")
                        return Err;
                    #region 对应的序列号类型
                    MySqlCommand SqlCmd = new MySqlCommand();

                    SqlCmd.CommandText = "delete SFCB.b_Product_Serial_Info where partnumber=@sPart";
                    SqlCmd.Parameters.Add("sPart", MySqlDbType.VarChar).Value = product.partnumber;
                    ExecteNonQuery(SqlCmd);
                    aa = 2;
                    foreach (Entity.tProductSerialInfo si in lsserialinfo)
                    {
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.CommandText = " INSERT INTO SFCB.B_PRODUCT_SERIAL_INFO  (PARTNUMBER, SERIALNAME)   VALUES  (@PARTNNO, @sSERIAL)";
                        cmd.Parameters.AddRange(new MySqlParameter[]
                            {
                               new MySqlParameter("PARTNNO",MySqlDbType.VarChar){Value=si.partnumber},
                               new MySqlParameter("sSERIAL",MySqlDbType.VarChar){Value=si.serialname}
                            });
                        ExecteNonQuery(cmd);

                    }
                    #endregion

                    return "OK";
                }
                catch (Exception ex)
                {
                    return "err " + aa.ToString() + "--" + ex.Message;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }

            }
         //   return string.Empty;
        }*/

        /// <summary>
        /// 添加途程工艺参数
        /// </summary>
        /// <param name="routecraftpara"></param>
        /// <param name="Err"></param>
    /*    public string InsertRouteCraftParamerter(Entity.tRoutCraftparameter routecraftpara)
        {
            string Err = "";
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("pro_InsertRouteCraftParamerter", _conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("V_ROUTGROUPID", OracleDbType.Varchar2);
                        cmd.Parameters["V_ROUTGROUPID"].Value = routecraftpara.routgroupId;

                        cmd.Parameters.Add("V_CRAFTID", OracleDbType.Varchar2);
                        cmd.Parameters["V_CRAFTID"].Value = routecraftpara.craftId;

                        cmd.Parameters.Add("V_CRAFTITEM", OracleDbType.Int32);
                        cmd.Parameters["V_CRAFTITEM"].Value = routecraftpara.craftItem;

                        cmd.Parameters.Add("V_CRAFTPARAMETERDES", OracleDbType.Varchar2);
                        cmd.Parameters["V_CRAFTPARAMETERDES"].Value = routecraftpara.craftparameterdes;

                        cmd.Parameters.Add("V_UPPERLIMIT", OracleDbType.Varchar2);
                        cmd.Parameters["V_UPPERLIMIT"].Value = routecraftpara.upperlimit;

                        cmd.Parameters.Add("V_LOWERLIMIT", OracleDbType.Varchar2);
                        cmd.Parameters["V_LOWERLIMIT"].Value = routecraftpara.lowerlimit;

                        cmd.Parameters.Add("V_OTHER", OracleDbType.Varchar2);
                        cmd.Parameters["V_OTHER"].Value = routecraftpara.other;

                        cmd.Parameters.Add("V_URL", OracleDbType.Varchar2);
                        cmd.Parameters["V_URL"].Value = routecraftpara.url;

                        cmd.Parameters.Add("V_ERROR", OracleDbType.Varchar2, 500);
                        cmd.Parameters["V_ERROR"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();

                        Err = cmd.Parameters["V_ERROR"].Value.ToString();
                        if (!string.IsNullOrEmpty(Err))
                            return Err;
                        else
                            return string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }
              
        /// <summary>
        /// 插入TrSn,并记录物料进料数量
        /// </summary>
        public void SP_InsertStorehousehadRecount(Entity.tPartStorehousehad trsn, out string err)
        {
            err = "";
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("pro_InsertStorehousehadRecount", _conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("V_KPNUMBER", OracleDbType.Varchar2);
                        cmd.Parameters["V_KPNUMBER"].Value = trsn.KpNumber;

                        cmd.Parameters.Add("V_VENDERCODE", OracleDbType.Varchar2);
                        cmd.Parameters["V_VENDERCODE"].Value = trsn.VenderCode;

                        cmd.Parameters.Add("V_DATECODE", OracleDbType.Varchar2);
                        cmd.Parameters["V_DATECODE"].Value = trsn.DateCode;

                        cmd.Parameters.Add("V_LOTID", OracleDbType.Varchar2);
                        cmd.Parameters["V_LOTID"].Value = trsn.LotId;

                        //12-09-11加
                        cmd.Parameters.Add("V_STOREHOUSEID", OracleDbType.Varchar2);
                        cmd.Parameters["V_STOREHOUSEID"].Value = trsn.storehouseId;

                        cmd.Parameters.Add("V_QTY", OracleDbType.Int32);
                        cmd.Parameters["V_QTY"].Value = trsn.QTY;

                        cmd.Parameters.Add("V_TRSN", OracleDbType.Varchar2);
                        cmd.Parameters["V_TRSN"].Value = trsn.Tr_Sn;

                        cmd.Parameters.Add("V_REMARK", OracleDbType.Varchar2);
                        cmd.Parameters["V_REMARK"].Value = trsn.Remark;

                        cmd.Parameters.Add("V_STATUS", OracleDbType.Int32);
                        cmd.Parameters["V_STATUS"].Value = trsn.status;

                        cmd.Parameters.Add("V_LOCID", OracleDbType.Varchar2);
                        cmd.Parameters["V_LOCID"].Value = trsn.LocId;

                        cmd.Parameters.Add("V_USERID", OracleDbType.Varchar2);
                        cmd.Parameters["V_USERID"].Value = trsn.UserId;

                        cmd.Parameters.Add("V_OUTQTY", OracleDbType.Varchar2);
                        cmd.Parameters["V_OUTQTY"].Value = trsn.OUTQTY;

                        cmd.Parameters.Add("V_FLAG", OracleDbType.Varchar2);
                        cmd.Parameters["V_FLAG"].Value = trsn.FLAG;

                        cmd.Parameters.Add("V_RECDATE", OracleDbType.Varchar2);
                        cmd.Parameters["V_RECDATE"].Value = trsn.recdate;

                        cmd.Parameters.Add("ERROR", OracleDbType.Varchar2, 500);
                        cmd.Parameters["ERROR"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();
                        err = cmd.Parameters["ERROR"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }*/

        #region 仓库存储过程
        //public string AddStorehouseInfo(Entity.tStorehouseInfo warehouse)
        //{
        //    using (OracleConnection _conn = new OracleConnection(this._strDBString))
        //    {
        //        try
        //        {
        //            _conn.Open();
        //            using (OracleCommand cmd = new OracleCommand("pro_InserttStorehouseInfo", _conn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.Add("storehouseId", OracleDbType.Varchar2);
        //                cmd.Parameters["storehouseId"].Value = warehouse.storehouseId;

        //                cmd.Parameters.Add("storehousename", OracleDbType.Varchar2);
        //                cmd.Parameters["storehousename"].Value = warehouse.storehousename;

        //                cmd.Parameters.Add("storehousedesc", OracleDbType.Varchar2);
        //                cmd.Parameters["storehousedesc"].Value = warehouse.storehousedesc;

        //                cmd.Parameters.Add("storehouseman", OracleDbType.Varchar2);
        //                cmd.Parameters["storehouseman"].Value = warehouse.storehouseman;

        //                cmd.Parameters.Add("storehousetype", OracleDbType.Varchar2);
        //                cmd.Parameters["storehousetype"].Value = warehouse.storehousetype;

        //                cmd.Parameters.Add("remark", OracleDbType.Varchar2);
        //                cmd.Parameters["remark"].Value = warehouse.remark;


        //                cmd.Parameters.Add("error", OracleDbType.Varchar2, 500);
        //                cmd.Parameters["error"].Direction = ParameterDirection.Output;

        //                cmd.ExecuteNonQuery();

        //                if (string.IsNullOrEmpty(cmd.Parameters["error"].Value.ToString()))
        //                {                          
        //                    return null;
        //                }
        //                else
        //                {                     
        //                    return cmd.Parameters["error"].Value.ToString();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return ex.Message;
        //        }
        //        finally
        //        {
        //            _conn.Close();
        //            _conn.Dispose();
        //        }
        //    }
        //}

        //public string AddStorehouseLocationInfo(Entity.tStorehouseLoctionInfo warehouseloc)
        //{
        //    using (OracleConnection _conn = new OracleConnection(this._strDBString))
        //    {
        //        try
        //        {
        //            _conn.Open();
        //            using (OracleCommand cmd = new OracleCommand("pro_InserttStorehouseLocInfo", _conn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.Add("locId", OracleDbType.Varchar2);
        //                cmd.Parameters["locId"].Value = warehouseloc.locId;

        //                cmd.Parameters.Add("uplocId", OracleDbType.Varchar2);
        //                cmd.Parameters["uplocId"].Value = warehouseloc.uplocId;

        //                cmd.Parameters.Add("loctype", OracleDbType.Varchar2);
        //                cmd.Parameters["loctype"].Value = warehouseloc.loctype;

        //                cmd.Parameters.Add("storehouseId", OracleDbType.Varchar2);
        //                cmd.Parameters["storehouseId"].Value = warehouseloc.storehouseId;

        //                cmd.Parameters.Add("locdesc", OracleDbType.Varchar2);
        //                cmd.Parameters["locdesc"].Value = warehouseloc.locdesc;

        //                cmd.Parameters.Add("loctotal", OracleDbType.Int32);
        //                cmd.Parameters["loctotal"].Value = warehouseloc.loctotal;

        //                cmd.Parameters.Add("remark", OracleDbType.Varchar2);
        //                cmd.Parameters["remark"].Value = warehouseloc.remark;

        //                cmd.Parameters.Add("error", OracleDbType.Varchar2, 500);
        //                cmd.Parameters["error"].Direction = ParameterDirection.Output;

        //                cmd.ExecuteNonQuery();

        //                if (string.IsNullOrEmpty(cmd.Parameters["error"].Value.ToString()))
        //                {
                         
        //                    return null;
        //                }
        //                else
        //                {
                          
        //                    return cmd.Parameters["error"].Value.ToString();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return ex.Message;
        //        }
        //        finally
        //        {
        //            _conn.Close();
        //            _conn.Dispose();
        //        }
        //    }
        //}
        #endregion

        /// <summary>
        /// 添加流程编号和对应的Xml内容
        /// </summary>
        /// <param name="routegroupId"></param>
        /// <param name="xmlcontent"></param>
        /// <returns></returns>
     /*   public string InsertRouteAtt(Entity.tRouteAtt routeatt)
        {
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("pro_InsertRouteAtt", _conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("V_ROUTEGROUPID", OracleDbType.Varchar2);
                        cmd.Parameters["V_ROUTEGROUPID"].Value = routeatt.routgroupId;

                        cmd.Parameters.Add("V_ROUTEGROUPDESC", OracleDbType.Varchar2);
                        cmd.Parameters["V_ROUTEGROUPDESC"].Value = routeatt.routgroupdesc;

                        cmd.Parameters.Add("V_XMLCONTENT",OracleDbType.Long, routeatt.routgroupxmlContent.Length);
                        cmd.Parameters["V_XMLCONTENT"].Value = routeatt.routgroupxmlContent;

                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 500);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();

                        if (string.IsNullOrEmpty(cmd.Parameters["RES"].Value.ToString()))
                        {
                       
                            return null;
                        }
                        else
                        {

                            return cmd.Parameters["RES"].Value.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }*/
        /// <summary>
        /// 获取最大的流水序列号
        /// </summary>
        /// <returns></returns>
     /*   public string[] GetMaxSn()
        {
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("pro_GetSnEnd", _conn))
                    {
                        string[] arr = new string[3];

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("MAXSN", OracleDbType.Varchar2, 20);
                        cmd.Parameters["MAXSN"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("WOID", OracleDbType.Varchar2, 20);
                        cmd.Parameters["WOID"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("ROID", OracleDbType.Varchar2, 50);
                        cmd.Parameters["ROID"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();

                        arr[0] = cmd.Parameters["MAXSN"].Value.ToString();
                        arr[1] = cmd.Parameters["WOID"].Value.ToString();
                        arr[2] = cmd.Parameters["ROID"].Value.ToString();
                        return arr;// cmd.Parameters["MaxSn"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }
        */
        //public void InsertSnRule(Entity.tSnRule snrule)
        //{         

        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "  INSERT INTO SFCR.T_SN_RULE (WOID, SNPREFIX, SNPOSTFIX, SNSTART, SNEND, VER, REVE, CURRSN,RECDATE) " +
        //                      " VALUES  (@WOID, @SNPREFIX, @SNPOSTFIX, @SNSTART, @SNEND, @VER, @REVE, @CURRSN,SYSDATE())";
        //    cmd.Parameters.AddRange(new MySqlParameter[]{
        //           new MySqlParameter("WOID",MySqlDbType.VarChar){Value=snrule.woid}, 
        //           new MySqlParameter("SNPREFIX",MySqlDbType.VarChar){Value=snrule.snprefix}, 
        //           new MySqlParameter("SNPOSTFIX",MySqlDbType.VarChar){Value=snrule.snpostfix}, 
        //           new MySqlParameter("SNSTART",MySqlDbType.VarChar){Value=snrule.snstart}, 
        //           new MySqlParameter("SNEND",MySqlDbType.VarChar){Value=snrule.snend}, 
        //           new MySqlParameter("VER",MySqlDbType.VarChar){Value=snrule.ver}, 
        //           new MySqlParameter("REVE",MySqlDbType.VarChar){Value=snrule.reve}, 
        //           new MySqlParameter("CURRSN",MySqlDbType.VarChar){Value=snrule.currsn}
        //        });
        //    //  ((Oracle.DataAccess.Client.MySqlCommand)cmd).BindByName = true;
        //    ExecteNonQuery(cmd);

             
        //}

     /*   public string PublicStationPro(string Storedproc,System.Data.DataTable dt)
        {
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand(Storedproc, _conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            cmd.Parameters.Add(string.Format("{0}", dt.Rows[i][0].ToString()), OracleDbType.Varchar2);
                            cmd.Parameters[string.Format("{0}", dt.Rows[i][0].ToString())].Value = dt.Rows[i][1].ToString();
                        }

                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 500);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();
                        if (string.IsNullOrEmpty(cmd.Parameters["RES"].Value.ToString()))
                        {                          
                            return null;
                        }
                        else
                        {
                        
                            return cmd.Parameters["RES"].Value.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }
        public string ExecuteProcedure(string Storedproc,List<Entity.ProcedureKey> EPK)
        {
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand(Storedproc, _conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        foreach (Entity.ProcedureKey Item in EPK)                       
                        {
                            cmd.Parameters.Add(string.Format("{0}",Item.Variable), OracleDbType.Varchar2);
                            cmd.Parameters[string.Format("{0}", Item.Variable)].Value = Item.Value;
                        }
                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 500);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();
                        if (string.IsNullOrEmpty(cmd.Parameters["RES"].Value.ToString()))
                        {
                            return null;
                        }
                        else
                        {
                            return cmd.Parameters["RES"].Value.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }
        public List<string> PublicStationProParam(string Storedproc, DataTable dt)
        {
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand(Storedproc, _conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr[1].ToString() == "IN")
                            {
                                cmd.Parameters.Add(string.Format("{0}", dr[0].ToString()), OracleDbType.Varchar2);
                                cmd.Parameters[string.Format("{0}", dr[0].ToString())].Value = dr[2].ToString();
                            }
                            else
                            {
                                cmd.Parameters.Add(string.Format("{0}", dr[0].ToString()), OracleDbType.Varchar2, 300);
                                cmd.Parameters[string.Format("{0}", dr[0].ToString())].Direction = ParameterDirection.Output;
                            }
                        }
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();
                        List<string> LsRES = new List<string>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr[1].ToString() == "OUT")
                            {
                                LsRES.Add(dr[0].ToString() + "-" + cmd.Parameters[string.Format("{0}", dr[0].ToString())].Value.ToString());
                            }
                        }

                        return LsRES;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }

        /// <summary>
        /// 添加工单而外用料 
        /// </summary>
        /// <returns></returns>
        public string InsertSmtKpDetaltForWo(SMT_KP_DETALTForWo DETALTFORWO)
        {
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("pro_InsertSmtKpDetaltForWo", _conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("MASTERID", OracleDbType.Varchar2);
                        cmd.Parameters["MASTERID"].Value = DETALTFORWO.MasterId;

                        cmd.Parameters.Add("WOID", OracleDbType.Varchar2);
                        cmd.Parameters["WOID"].Value = DETALTFORWO.WoId;

                        cmd.Parameters.Add("USERID", OracleDbType.Varchar2);
                        cmd.Parameters["USERID"].Value = DETALTFORWO.UserId;

                        cmd.Parameters.Add("STATIONNO", OracleDbType.Varchar2);
                        cmd.Parameters["STATIONNO"].Value = DETALTFORWO.Stationno;

                        cmd.Parameters.Add("KPNUMBER", OracleDbType.Varchar2);
                        cmd.Parameters["KPNUMBER"].Value = DETALTFORWO.Kpnumber;

                        cmd.Parameters.Add("KPDESC", OracleDbType.Varchar2);
                        cmd.Parameters["KPDESC"].Value = DETALTFORWO.Kpdesc;

                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 50);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();

                        return cmd.Parameters["RES"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }
        */
        ///// <summary>
        ///// 添加工单序列号区间
        ///// </summary>
        ///// <param name="wosnrule"></param>
        ///// <returns></returns>
        //public string InsertWoSnRule(List<Entity.WoSnRule> wosnrule)
        //{
        //    try
        //    {
        //        if (wosnrule.Count != 0)
        //        {
        //            List<MySqlCommand> LsCmd = new List<MySqlCommand>();

        //            //添加序列号区间前先将原来的删除
        //            MySqlCommand sqlcmd = null;

        //            sqlcmd = new MySqlCommand();
        //            sqlcmd.CommandText = "DELETE FROM SFCR.T_WO_SN_RULE where woId=@woid";
        //            sqlcmd.Parameters.Add("woid", MySqlDbType.VarChar, 20).Value = wosnrule[0].woid;
        //            LsCmd.Add(sqlcmd);


        //            foreach (Entity.WoSnRule Item in wosnrule)
        //            {
        //                sqlcmd = new MySqlCommand();
        //                sqlcmd.CommandText = " INSERT INTO SFCR.T_WO_SN_RULE (WOID,SNTYPE,SNPREFIX,SNPOSTFIX,SNSTART,SNEND,SNLENG,VER,REVE,RECDATE,USENUM)" +
        //                                  "VALUES (@WOID,@SNTYPE,@SNPREFIX,@SNPOSTFIX,@SNSTART,@SNEND,@SNLENG,@VER,@REVE,NOW(),@USENUM)";

        //                sqlcmd.Parameters.AddRange(new MySqlParameter[]{
        //                    new MySqlParameter("WOID",MySqlDbType.VarChar){Value=Item.woid}, 
        //                    new MySqlParameter("SNTYPE",MySqlDbType.VarChar){Value=Item.sntype}, 
        //                    new MySqlParameter("SNPREFIX",MySqlDbType.VarChar){Value=Item.snprefix}, 
        //                    new MySqlParameter("SNPOSTFIX",MySqlDbType.VarChar){Value=Item.snpostfix}, 
        //                    new MySqlParameter("SNSTART",MySqlDbType.VarChar){Value=Item.snstart}, 
        //                    new MySqlParameter("SNEND",MySqlDbType.VarChar){Value=Item.snend}, 
        //                    new MySqlParameter("SNLENG",MySqlDbType.VarChar){Value=Item.snleng},
        //                    new MySqlParameter("VER",MySqlDbType.VarChar){Value=Item.ver},
        //                    new MySqlParameter("REVE",MySqlDbType.VarChar){Value=Item.reve},
        //                    new MySqlParameter("USENUM",MySqlDbType.VarChar){Value=Item.usenum}                                    
        //            });
        //          LsCmd.Add(sqlcmd);
        //        }
        
        //        ExecteNonQueryArr(LsCmd);
        //         }
        //        return "OK";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        /// <summary>
        /// 获取栈板号
        /// </summary>
        /// <param name="Facid"></param>
        /// <returns></returns>
   /*     public string GetPalletNumber(string Facid,string Line)
        {
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("pro_GetSeqPallet", _conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("FACID", OracleDbType.Varchar2);
                        cmd.Parameters["FACID"].Value = Facid;

                        cmd.Parameters.Add("LINE", OracleDbType.Varchar2);
                        cmd.Parameters["LINE"].Value = Line;


                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 500);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        // ((Oracle.DataAccess.Client.MySqlCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();

                        if (string.IsNullOrEmpty(cmd.Parameters["RES"].Value.ToString()))
                        {                      
                            return null;
                        }
                        else
                        {                          
                            return cmd.Parameters["RES"].Value.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }
        */

        /// <summary>
        /// 产品过站使用
        /// </summary>
        /// <param name="Facid"></param>
        /// <returns></returns>
     /*   public string SP_TEST_MAIN_ONLY(string DATA, string MYGROUP, string EMP, string EC, string LINE)
        {
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("PRO_TEST_MAIN_ONLY", _conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("DATA", OracleDbType.Varchar2);
                        cmd.Parameters["DATA"].Value = DATA;

                        cmd.Parameters.Add("MYGROUP", OracleDbType.Varchar2);
                        cmd.Parameters["MYGROUP"].Value = MYGROUP;

                        cmd.Parameters.Add("EMP", OracleDbType.Varchar2);
                        cmd.Parameters["EMP"].Value = EMP;

                        cmd.Parameters.Add("EC", OracleDbType.Varchar2);
                        cmd.Parameters["EC"].Value = EC;

                        cmd.Parameters.Add("LINE", OracleDbType.Varchar2);
                        cmd.Parameters["LINE"].Value = LINE;


                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 500);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        //  ((Oracle.DataAccess.Client.MySqlCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();

                        if (string.IsNullOrEmpty(cmd.Parameters["RES"].Value.ToString()))
                        {                       
                            return null;
                        }
                        else
                        {                        
                            return cmd.Parameters["RES"].Value.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }

        /// <summary>
        /// 产品入库使用
        /// </summary>
        /// <param name="Facid"></param>
        /// <returns></returns>
        public string SP_TEST_STOCKIN(string DATA, string MYGROUP, string EMP, string EC, string LINE)
        {
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("PRO_TEST_STOCKIN", _conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("DATA", OracleDbType.Varchar2);
                        cmd.Parameters["DATA"].Value = DATA;

                        cmd.Parameters.Add("MYGROUP", OracleDbType.Varchar2);
                        cmd.Parameters["MYGROUP"].Value = MYGROUP;

                        cmd.Parameters.Add("EMP", OracleDbType.Varchar2);
                        cmd.Parameters["EMP"].Value = EMP;

                        cmd.Parameters.Add("EC", OracleDbType.Varchar2);
                        cmd.Parameters["EC"].Value = EC;

                        cmd.Parameters.Add("LINE", OracleDbType.Varchar2);
                        cmd.Parameters["LINE"].Value = LINE;

                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 500);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();

                        if (string.IsNullOrEmpty(cmd.Parameters["RES"].Value.ToString()))
                        {                  
                            return null;
                        }
                        else
                        {              

                            return cmd.Parameters["RES"].Value.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }

        /// <summary>
        /// 增加卡通箱记录
        /// </summary>
        /// <param name="cartioninfo">卡通箱信息</param>
        /// <returns></returns>
        public string InsertCartonInfo(Entity.tCartonInfo cartioninfo)
        {
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("PRO_INSERTCARTONINFO", _conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("CARTONID", OracleDbType.Varchar2);
                        cmd.Parameters["CARTONID"].Value = cartioninfo.cartonId;

                        cmd.Parameters.Add("ESN", OracleDbType.Varchar2);
                        cmd.Parameters["ESN"].Value = cartioninfo.esn;

                        cmd.Parameters.Add("LINEID", OracleDbType.Varchar2);
                        cmd.Parameters["LINEID"].Value = cartioninfo.lineId;

                        cmd.Parameters.Add("WOID", OracleDbType.Varchar2);
                        cmd.Parameters["WOID"].Value = cartioninfo.woId;

                        cmd.Parameters.Add("MCARTONNUMBER", OracleDbType.Varchar2);
                        cmd.Parameters["MCARTONNUMBER"].Value = cartioninfo.mcartonnumber;

                        cmd.Parameters.Add("SN", OracleDbType.Varchar2);
                        cmd.Parameters["SN"].Value = cartioninfo.sn;

                        cmd.Parameters.Add("MAC", OracleDbType.Varchar2);
                        cmd.Parameters["MAC"].Value = cartioninfo.mac;

                        cmd.Parameters.Add("COMPUTER", OracleDbType.Varchar2);
                        cmd.Parameters["COMPUTER"].Value = cartioninfo.computer;

                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 200);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();
                        return cmd.Parameters["RES"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }


        /// <summary>
        ///  产品过站和记录卡通箱
        /// </summary>
        /// <param name="cartonId">卡通箱编号</param>
        /// <param name="line">产线编号</param>
        /// <param name="mygroup">当前流程</param>
        /// <param name="esn">产品跟踪序列号</param>
        /// <param name="userid">当前用户</param>
        /// <param name="flag">状态</param>
        /// <returns></returns>
        public string UpdateWipAndRecCartonBox(Entity.tWipTrackingTable wiptrack)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(this._strDBString))
                {
                    try
                    {
                        conn.Open();
                        using (OracleCommand cmd = new OracleCommand("PRO_UPDATEWIPANDRECCARTONBOX", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("LINE", OracleDbType.Varchar2);
                            cmd.Parameters["LINE"].Value = wiptrack.line;

                            cmd.Parameters.Add("MYGROUP", OracleDbType.Varchar2);
                            cmd.Parameters["MYGROUP"].Value = wiptrack.locstation;

                            cmd.Parameters.Add("SN", OracleDbType.Varchar2);
                            cmd.Parameters["SN"].Value = wiptrack.ESN;

                            cmd.Parameters.Add("EMP", OracleDbType.Varchar2);
                            cmd.Parameters["EMP"].Value = wiptrack.userId;

                            cmd.Parameters.Add("FLAG", OracleDbType.Varchar2);
                            cmd.Parameters["FLAG"].Value = wiptrack.errflag;

                            cmd.Parameters.Add("CARTONID", OracleDbType.Varchar2);
                            cmd.Parameters["CARTONID"].Value = wiptrack.cartonnumber;

                            cmd.Parameters.Add("MCARTIONID", OracleDbType.Varchar2);
                            cmd.Parameters["MCARTIONID"].Value = wiptrack.mcartonnumbr;

                            cmd.Parameters.Add("PALLETNUMBER", OracleDbType.Varchar2);
                            cmd.Parameters["PALLETNUMBER"].Value = wiptrack.palletnumber;

                            cmd.Parameters.Add("MPALLETNUMBER", OracleDbType.Varchar2);
                            cmd.Parameters["MPALLETNUMBER"].Value = wiptrack.mpalletnumber;

                            cmd.Parameters.Add("TRAYNO", OracleDbType.Varchar2);
                            cmd.Parameters["TRAYNO"].Value = wiptrack.TrayNO;

                            cmd.Parameters.Add("RES", OracleDbType.Varchar2, 200);
                            cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                            ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                            cmd.ExecuteNonQuery();
                            return cmd.Parameters["RES"].Value.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }      


        /// <summary>
        /// 获取及设置占用最大的卡通箱号
        /// </summary>
        /// <param name="woId">工单号</param>
        /// <param name="lineId">当前的线别</param>
        /// <param name="partnumber">成品料号</param>
        /// <returns>返回箱号 或ERR</returns>
        public string GetMaxCartonId(string woId, string lineId, string partnumber)
        {
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("PRO_GETMAXCARTONID", _conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("V_WOID", OracleDbType.Varchar2);
                        cmd.Parameters["V_WOID"].Value = woId;

                        cmd.Parameters.Add("V_LINEID", OracleDbType.Varchar2);
                        cmd.Parameters["V_LINEID"].Value = lineId;

                        cmd.Parameters.Add("V_PARTNUMBER", OracleDbType.Varchar2);
                        cmd.Parameters["V_PARTNUMBER"].Value = partnumber;

                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 500);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();                  
                        return cmd.Parameters["RES"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }     


        /// <summary>
        /// 栈板CARTON TRAY
        /// </summary>
        /// <param name="Facid"></param>
        /// <returns></returns>
        public string SP_TEST_CTN_PALT_TRAY(string DATA, string MYGROUP, string EMP, string EC, string LINE, string LOCDATA, string CUTDATA, int Flag)
        {
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("PRO_TEST_CTN_PALT_TRAY", _conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("DATA", OracleDbType.Varchar2);
                        cmd.Parameters["DATA"].Value = DATA;

                        cmd.Parameters.Add("MYGROUP", OracleDbType.Varchar2);
                        cmd.Parameters["MYGROUP"].Value = MYGROUP;

                        cmd.Parameters.Add("EMP", OracleDbType.Varchar2);
                        cmd.Parameters["EMP"].Value = EMP;

                        cmd.Parameters.Add("EC", OracleDbType.Varchar2);
                        cmd.Parameters["EC"].Value = EC;

                        cmd.Parameters.Add("LINE", OracleDbType.Varchar2);
                        cmd.Parameters["LINE"].Value = LINE;

                        cmd.Parameters.Add("LOCDATA", OracleDbType.Varchar2);
                        cmd.Parameters["LOCDATA"].Value = LOCDATA;

                        cmd.Parameters.Add("CUTDATA", OracleDbType.Varchar2);
                        cmd.Parameters["CUTDATA"].Value = CUTDATA;

                        cmd.Parameters.Add("UPFLAG", OracleDbType.Int32);
                        cmd.Parameters["UPFLAG"].Value = Flag;

                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 500);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();

                        if (string.IsNullOrEmpty(cmd.Parameters["RES"].Value.ToString()))
                        {                      
                            return null;
                        }
                        else
                        {                         
                            return cmd.Parameters["RES"].Value.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }
        /// <summary>
        /// 撤销出库时，拣货操作
        /// </summary>
        /// <param name="SAPCode"></param>
        /// <param name="partnumber"></param>
        /// <param name="sfclotcode"></param>
        /// <returns></returns>
        public string CancelSFClotcode(string SAPCode, string partnumber, string sfclotcode)
        {
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("PRO_CANCELSFCLOTCODE", _conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("V_SAPCODE", OracleDbType.Varchar2);
                        cmd.Parameters["V_SAPCODE"].Value = SAPCode;

                        cmd.Parameters.Add("V_PARTNUMBER", OracleDbType.Varchar2);
                        cmd.Parameters["V_PARTNUMBER"].Value = partnumber;

                        cmd.Parameters.Add("V_SFCLOTCODE", OracleDbType.Varchar2);
                        cmd.Parameters["V_SFCLOTCODE"].Value = sfclotcode;

                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 500);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();                      
                        return cmd.Parameters["RES"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }

        public void MaterialPrint(Entity.tPartStorehousehad sd, string kpdesc, string partgroup, string vendername, string PO)
        {
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("PRO_MATERIALPRINT", _conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("KPNUMBER", OracleDbType.Varchar2);
                        cmd.Parameters["KPNUMBER"].Value = sd.KpNumber;

                        cmd.Parameters.Add("KPDESC", OracleDbType.Varchar2);
                        cmd.Parameters["KPDESC"].Value = kpdesc;

                        cmd.Parameters.Add("PARTGROUP", OracleDbType.Varchar2);
                        cmd.Parameters["PARTGROUP"].Value = partgroup;

                        cmd.Parameters.Add("VENDERID", OracleDbType.Varchar2);
                        cmd.Parameters["VENDERID"].Value = sd.VenderCode;

                        cmd.Parameters.Add("VENDERNAME", OracleDbType.Varchar2);
                        cmd.Parameters["VENDERNAME"].Value = vendername;

                        cmd.Parameters.Add("TRSN", OracleDbType.Varchar2);
                        cmd.Parameters["TRSN"].Value = sd.Tr_Sn;

                        cmd.Parameters.Add("DATECODE", OracleDbType.Varchar2);
                        cmd.Parameters["DATECODE"].Value = sd.DateCode;

                        cmd.Parameters.Add("LOTID", OracleDbType.Varchar2);
                        cmd.Parameters["LOTID"].Value = sd.LotId;

                        cmd.Parameters.Add("QTY", OracleDbType.Int32);
                        cmd.Parameters["QTY"].Value = sd.QTY;

                        cmd.Parameters.Add("USERID", OracleDbType.Varchar2);
                        cmd.Parameters["USERID"].Value = sd.UserId;

                        cmd.Parameters.Add("PO", OracleDbType.Varchar2);
                        cmd.Parameters["PO"].Value = PO;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();                   
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }
        /// <summary>
        /// 计算条码序列号
        /// </summary>
        /// <param name="Err"></param>
        /// <returns></returns>
        public string ConvertMACIMEI(string woId, string sntype)
        {
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("PRO_CONVERTMACIMEI", _conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("WO", OracleDbType.Varchar2);
                        cmd.Parameters["WO"].Value = woId;

                        cmd.Parameters.Add("SNTYPE", OracleDbType.Varchar2);
                        cmd.Parameters["SNTYPE"].Value = sntype;


                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 500);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();                     
                        return cmd.Parameters["RES"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }

        /// <summary>
        /// 保存SAP出货订单信息
        /// </summary>
        /// <param name="sinfo"></param>
        public string SaveSAPOutInfo(Entity.tSapLodeInfo sinfo)
        {
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("PRO_SAVESAPOUTINFO", _conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("V_CONTACTPERSON", OracleDbType.Varchar2);
                        cmd.Parameters["V_CONTACTPERSON"].Value = sinfo.ContactPerson;

                        cmd.Parameters.Add("V_CUSTOMERNAME", OracleDbType.Varchar2);
                        cmd.Parameters["V_CUSTOMERNAME"].Value = sinfo.CustomerName;

                        cmd.Parameters.Add("V_CUSTOMERID", OracleDbType.Varchar2);
                        cmd.Parameters["V_CUSTOMERID"].Value = sinfo.CustomerId;

                        cmd.Parameters.Add("V_PARTNUMBER", OracleDbType.Varchar2);
                        cmd.Parameters["V_PARTNUMBER"].Value = sinfo.Partnumber;

                        cmd.Parameters.Add("V_PRODUCTDESC", OracleDbType.Varchar2);
                        cmd.Parameters["V_PRODUCTDESC"].Value = sinfo.ProductDesc;

                        cmd.Parameters.Add("V_SAPLOTCODE", OracleDbType.Varchar2);
                        cmd.Parameters["V_SAPLOTCODE"].Value = sinfo.SAPCode;

                        cmd.Parameters.Add("V_QTY", OracleDbType.Int32);
                        cmd.Parameters["V_QTY"].Value = sinfo.QTY;

                        cmd.Parameters.Add("V_SAPWAREHOUSE", OracleDbType.Varchar2);
                        cmd.Parameters["V_SAPWAREHOUSE"].Value = sinfo.SapWarehouse;

                        cmd.Parameters.Add("V_SFCLOTCODE", OracleDbType.Varchar2);
                        cmd.Parameters["V_SFCLOTCODE"].Value = sinfo.SFCcode;

                        cmd.Parameters.Add("V_USERID", OracleDbType.Varchar2);
                        cmd.Parameters["V_USERID"].Value = sinfo.UserId;

                        cmd.Parameters.Add("V_ADDRESS", OracleDbType.Varchar2);
                        cmd.Parameters["V_ADDRESS"].Value = sinfo.Address;

                        cmd.Parameters.Add("V_CUSTOMERNAME2", OracleDbType.Varchar2);
                        cmd.Parameters["V_CUSTOMERNAME2"].Value = sinfo.Customername2;

                        cmd.Parameters.Add("V_CONTRACTNO", OracleDbType.Varchar2);
                        cmd.Parameters["V_CONTRACTNO"].Value = sinfo.Contractno;


                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 50);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();                    
                        return cmd.Parameters["RES"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }

        public string CHECK_ROUTE(Entity.tPublicParamForSP Tpfs)
        {
            string err = "";
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("PRO_CHECKROUTE", _conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("DATA", OracleDbType.Varchar2);
                        cmd.Parameters["DATA"].Value = Tpfs.DATA;

                        cmd.Parameters.Add("MYGROUP", OracleDbType.Varchar2);
                        cmd.Parameters["MYGROUP"].Value = Tpfs.MYGROUP;

                        //cmd.Parameters.Add("ROUTECODE", OracleDbType.Varchar2);
                        //cmd.Parameters["ROUTECODE"].Value = Tpfs.ROUTECODE;

                        //cmd.Parameters.Add("ERRFLAG", OracleDbType.Varchar2);
                        //cmd.Parameters["ERRFLAG"].Value = Tpfs.ERRORFLAG;

                        //cmd.Parameters.Add("LOCGROUP", OracleDbType.Varchar2);
                        //cmd.Parameters["LOCGROUP"].Value = Tpfs.LOCSTATION;

                        //cmd.Parameters.Add("NEXTSTATION", OracleDbType.Varchar2);
                        //cmd.Parameters["NEXTSTATION"].Value = Tpfs.NEXTSTATION;

                        //cmd.Parameters.Add("WO", OracleDbType.Varchar2);
                        //cmd.Parameters["WO"].Value = Tpfs.woId;

                        //cmd.Parameters.Add("ENDGROUP", OracleDbType.Varchar2);
                        //cmd.Parameters["ENDGROUP"].Value = Tpfs.ENDGROUP;

                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 500);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();
                        err = cmd.Parameters["RES"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
                return err;
            }
        }

        public string SP_INS_ATE_BACK(Entity.tPublicParamForSP Tpfs)
        {
            string err = "";
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("PRO_INS_ATE_BACK", _conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("DATA", OracleDbType.Varchar2);
                        cmd.Parameters["DATA"].Value = Tpfs.DATA;

                        cmd.Parameters.Add("MYGROUP", OracleDbType.Varchar2);
                        cmd.Parameters["MYGROUP"].Value = Tpfs.MYGROUP;

                        cmd.Parameters.Add("EMP", OracleDbType.Varchar2);
                        cmd.Parameters["EMP"].Value = Tpfs.EMPNO;

                        cmd.Parameters.Add("LINE", OracleDbType.Varchar2);
                        cmd.Parameters["LINE"].Value = Tpfs.LINE;

                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 500);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();
                        err = cmd.Parameters["RES"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
                return err;
            }
        }       

        public string SP_TEST_INPUT_ALL(Entity.tPublicParamForSP Tpfs)
        {
            string err = "";
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("PRO_TEST_INPUT_ALL", _conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("DATA", OracleDbType.Varchar2);
                        cmd.Parameters["DATA"].Value = Tpfs.DATA;

                        cmd.Parameters.Add("LINE", OracleDbType.Varchar2);
                        cmd.Parameters["LINE"].Value = Tpfs.LINE;

                        cmd.Parameters.Add("MYGROUP", OracleDbType.Varchar2);
                        cmd.Parameters["MYGROUP"].Value = Tpfs.MYGROUP;

                        cmd.Parameters.Add("WO", OracleDbType.Varchar2);
                        cmd.Parameters["WO"].Value = Tpfs.woId;

                        cmd.Parameters.Add("EMP", OracleDbType.Varchar2);
                        cmd.Parameters["EMP"].Value = Tpfs.EMPNO;


                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 500);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();
                        err = cmd.Parameters["RES"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
                return err;
            }
        }
        /// <summary>
        /// 成品出库 2013-09-02
        /// </summary>
        /// <param name="Tpfs"></param>
        /// <returns></returns>
        public string ProductOut(Entity.tWarehouseWipTrackingTable Twwt)
        {
            string err = "";
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("PRO_PRODUCTOUT", _conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("ESN", OracleDbType.Varchar2);
                        cmd.Parameters["ESN"].Value = Twwt.Esn;

                        cmd.Parameters.Add("SAPCODE", OracleDbType.Varchar2);
                        cmd.Parameters["SAPCODE"].Value = Twwt.SapCode;

                        cmd.Parameters.Add("PARTNUMBER", OracleDbType.Varchar2);
                        cmd.Parameters["PARTNUMBER"].Value = Twwt.PartNumber;

                        cmd.Parameters.Add("CUSTOMERID", OracleDbType.Varchar2);
                        cmd.Parameters["CUSTOMERID"].Value = Twwt.CustomerId;

                        cmd.Parameters.Add("USERID", OracleDbType.Varchar2);
                        cmd.Parameters["USERID"].Value = Twwt.userid;

                        cmd.Parameters.Add("STATUS", OracleDbType.Varchar2);
                        cmd.Parameters["STATUS"].Value = Twwt.Status;

                        cmd.Parameters.Add("SAPQTY", OracleDbType.Varchar2);
                        cmd.Parameters["SAPQTY"].Value = Twwt.mFlag;

                        cmd.Parameters.Add("CUSTOMERADDRESS", OracleDbType.Varchar2);
                        cmd.Parameters["CUSTOMERADDRESS"].Value = Twwt.Address;


                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 500);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();
                        err = cmd.Parameters["RES"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
                return err;
            }
        }
        public System.Data.DataSet PublicReurnDataSet(string ProName,List<Entity.ProcedureKey> pdk,string OutName)
        {
            string ss = "";
            using (MySqlConnection _conn = new MySqlConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();

                    MySqlCommand cmd = new MySqlCommand(ProName, _conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (Entity.ProcedureKey item in pdk)
                    {
                        ss = item.Value;
                        bool Result = (item.Variable is string);
                        if (Result)
                        {
                            cmd.Parameters.Add(item.Variable, MySqlDbType.VarChar);
                            cmd.Parameters[item.Variable].Value = item.Value;
                        }
                        else
                        {
                            cmd.Parameters.Add(item.Variable, MySqlDbType.Int32);
                            cmd.Parameters[item.Variable].Value = item.Value;
                        }
                    }
                    //((Oracle.DataAccess.Client.MySqlCommand)cmd).BindByName = true;
                    //MySqlParameter p1 = new MySqlParameter(OutName, MySqlDbType.RefCursor);
                    //p1.Direction = System.Data.ParameterDirection.Output;
                    //cmd.Parameters.Add(p1);
                    //MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    //DataSet ds = new DataSet();
                    //da.Fill(ds);
                    //return ds;
                    return null;

                }
                catch (Exception ex)
                {
                    throw new Exception(ss + "AA--" + ex.Message);
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }


        }

        public System.Data.DataSet PublicReurnDataSetOutString(string ProName, List<Entity.ProcedureKey> pdk, string OutName, out string Err)
        {
            Err = "";
            using (MySqlConnection _conn = new MySqlConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();

                    MySqlCommand cmd = new MySqlCommand(ProName, _conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (Entity.ProcedureKey item in pdk)
                    {
                        bool Result = (item.Variable is string);
                        if (Result)
                            cmd.Parameters.Add(new MySqlParameter(item.Variable, MySqlDbType.VarChar)).Value = item.Value;
                        else
                            cmd.Parameters.Add(new MySqlParameter(item.Variable, MySqlDbType.Int32)).Value = item.intValue;
                    }

                    cmd.Parameters.Add("RES", MySqlDbType.VarChar, 500);
                    cmd.Parameters["RES"].Direction = ParameterDirection.Output;

                    // ((Oracle.DataAccess.Client.MySqlCommand)cmd).BindByName = true;
                    //MySqlParameter p1 = new MySqlParameter(OutName, MySqlDbType.RefCursor);
                    //p1.Direction = System.Data.ParameterDirection.Output;
                    //cmd.Parameters.Add(p1);
                    //MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    //DataSet ds = new DataSet();
                    //da.Fill(ds);

                    //Err = cmd.Parameters["RES"].Value.ToString();
                    // return ds;
                    return null;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }


        }




        /// <summary>
        /// 更新TrSn状态
        /// </summary>
        /// <returns></returns>
        public string Update_TR_SN(string R_Trsn, string R_WOID, string R_USERID, string R_STATUS, string R_RMAK1, string R_RMAK2)
        {
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                try
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand("UPDATE_TR_SN", _conn))
                    {

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("R_TRSN", OracleDbType.Varchar2);
                        cmd.Parameters["R_TRSN"].Value = R_Trsn;

                        cmd.Parameters.Add("R_WOID", OracleDbType.Varchar2);
                        cmd.Parameters["R_WOID"].Value = R_WOID;

                        cmd.Parameters.Add("R_USERID", OracleDbType.Varchar2);
                        cmd.Parameters["R_USERID"].Value = R_USERID;

                        cmd.Parameters.Add("R_STATUS", OracleDbType.Varchar2);
                        cmd.Parameters["R_STATUS"].Value = R_STATUS;

                        cmd.Parameters.Add("R_RMAK1", OracleDbType.Varchar2);
                        cmd.Parameters["R_RMAK1"].Value = R_RMAK1;

                        cmd.Parameters.Add("R_RMAK2", OracleDbType.Varchar2);
                        cmd.Parameters["R_RMAK2"].Value = R_RMAK2;


                        cmd.Parameters.Add("RES", OracleDbType.Varchar2, 500);
                        cmd.Parameters["RES"].Direction = ParameterDirection.Output;
                        ((Oracle.DataAccess.Client.OracleCommand)cmd).BindByName = true;
                        cmd.ExecuteNonQuery();
                        return cmd.Parameters["RES"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
        }
        */
       
    }
}
