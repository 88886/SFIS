using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SystemObject;
using GenericProvider;

namespace BLL
{
    public partial class tWarehouseWipTracking
    {
        public tWarehouseWipTracking()
        {
        }

//        /// <summary>
//        /// 判定条码是否存在仓库
//        /// </summary>
//        /// <param name="ESN"></param>
//        /// <returns></returns>
//        public bool CheckDataInWH(string ESN)
//        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = "select esn from sfcr.Z_WHS_TRACKING where esn=@esn";
//            cmd.Parameters.Add("esn", MySqlDbType.VarChar, 30).Value = ESN;
//            DataTable dt = BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];
//            if (dt.Rows.Count != 0)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }

//        }
//        /// <summary>
//        /// 查询待接收批次信息
//        /// </summary>
//        /// <param name="ESN"></param>
//        /// <returns></returns>

//        public System.Data.DataSet Getlotinfo(int status)
//        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = "select distinct lotin from sfcr.Z_WHS_TRACKING where status=@status ";
//            cmd.Parameters.Add("@status", MySqlDbType.VarChar, 50).Value = status;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

//        }

//        /// <summary>
//        /// 批次列表信息
//        /// </summary>
//        /// <param name="ESN"></param>
//        /// <returns></returns>
//        public System.Data.DataSet GetlotinfoList(string lotin)
//        {
//            MySqlCommand cmd = new MySqlCommand();

////            cmd.CommandText = @" select lotin 入库批次 ,woId 工单号,a.partnumber 料号,case a.palletnumber when 'NA' then '' else isnull(a.palletnumber,'') end 栈板号,
////                                 case a.cartonnumber when 'NA' then '' else isnull(a.cartonnumber,'') end 卡通号, case TRAYNO when 'NA' then '' else isnull(TRAYNO,'') end TrayNo ,COUNT(*) 数量,
////		                        case  when status=0 then '待入库' when status in(1,2,3) then '已在库' else '已出库' end 状态 
////                                from sfcr.Z_WHS_TRACKING a , sfcr.T_WIP_TRACKING_ONLINE  b
////                                 where lotin =@lotin and a.esn =b.esn 
////		                         group by lotin ,woId,a.partnumber,a.palletnumber,a.cartonnumber,TRAYNO,status ";

//            cmd.CommandText = " select lotin 入库批次 ,woId 工单号,a.partnumber 料号,case a.palletnumber when 'NA' then '' else isnull(a.palletnumber,'') end 栈板号,";
//            cmd.CommandText += " case a.cartonnumber when 'NA' then '' else isnull(a.cartonnumber,'') end 卡通号, case TRAYNO when 'NA' then '' else isnull(TRAYNO,'') end TrayNo ,COUNT(*) 数量,";
//            cmd.CommandText += " case  when status=0 then '待入库' when status in(1,2,3) then '已在库' else '已出库' end 状态  ";
//            cmd.CommandText += " from sfcr.Z_WHS_TRACKING a , sfcr.T_WIP_TRACKING_ONLINE  b ";
//            cmd.CommandText += " where lotin =@lotin and a.esn =b.esn ";
//            cmd.CommandText += " group by lotin ,woId,a.partnumber,a.palletnumber,a.cartonnumber,TRAYNO,status	";

//            cmd.Parameters.Add("lotin", MySqlDbType.VarChar, 50).Value = lotin;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

//        }

//        /// <summary>
//        /// 客退品、及状态的判断
//        /// </summary>
//        /// <param name="ESN"></param>
//        /// <returns></returns>
//        public System.Data.DataSet GetReturnList(string RecType, string RecCode)
//        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = " select partnumber  料号,@RecCode 序列号,status,";
//            cmd.CommandText += " case when status in (6,7,8,9,10,11) then '不在库'  when status in (1,2,3) then '已在库'  when status=0 then '待入库' end 状态 ,count(*) 数量 ";
//            cmd.CommandText += " from sfcr.Z_WHS_TRACKING ";
//            if (RecType == "0" || RecType == "1")
//            {
//                cmd.CommandText += " where esn=(select esn from SFCR.Z_WHS_KEYPART where snval=@RecCode) ";
//            }
//            else
//            {
//                cmd.CommandText += " where cartonnumber=@RecCode ";
//            }
//            cmd.CommandText += " group by partnumber,status ";

//            //cmd.Parameters.Add("RecCode1", MySqlDbType.VarChar, 50).Value = RecCode;
//            cmd.Parameters.Add("RecCode", MySqlDbType.VarChar, 50).Value = RecCode;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

//        }


//        /// <summary>
//        /// 接收入库--20140516 即将取消此方法
//        /// </summary>
//        /// <param name="ESN"></param>
//        /// <returns></returns>
//        public string StockReceive(string RecType, string RecStyle, string lotcode, string RecCode, string storeId, string locId, string userid)
//        {
//            MySqlCommand cmd = new MySqlCommand();     
//            string C_RES = "OK";
//            try
//            {
//                if (RecType == "1")
//                {
//                    if (RecStyle == "0")
//                    {
//                        cmd.CommandText = @" UPDATE SFCR.Z_WHS_TRACKING SET STATUS= @C_RECTYPE,STOREHOUSEID = @C_STOREID,LOCID= @C_LOCID,USERID= @C_USERID,
//                                        RECDATE = SYSDATE()  WHERE LOTIN = @C_RECCODE";
//                        cmd.Parameters.AddRange(new MySqlParameter[]
//                                {
//                                new MySqlParameter("C_RECTYPE",MySqlDbType.Int32){Value=RecType},
//                                new MySqlParameter("C_STOREID",MySqlDbType.VarChar){Value=storeId},
//                                new MySqlParameter("C_LOCID",MySqlDbType.VarChar){Value=locId},
//                                new MySqlParameter("C_USERID",MySqlDbType.VarChar){Value=userid},
//                                new MySqlParameter("C_RECCODE",MySqlDbType.VarChar){Value=RecCode}
//                                });
//                    }
//                    else
//                    {
//                        C_RES = "暂时只能使用批次接收入库";
//                    }
//                }
//                else
//                {
//                    C_RES = "暂时只能使用批次接收入库";
//                }
//                BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
//                return C_RES;
//            }
//            catch (Exception ex)
//            {
//                return ex.Message; 
//            }
//           // return BLL.BllMsSqllib.Instance.PublicStationPro("Pro_StockReceive", dt);
//        }

//        /// <summary>
//        /// 接收入库
//        /// </summary>
//        /// <param name="ESN"></param>
//        /// <returns></returns>
//        public string Receiving_Storage(List<Entity.Z_WHS_SAP_BACKFLUSHTable> Lszhsb, string RecType, string storeId, string locId, string userid, string RecCode)
//        {
//            MySqlCommand cmd = new MySqlCommand();
//            List<MySqlCommand> LsCmd = new List<MySqlCommand>();     
//            try
//            {
              
//                cmd.CommandText = @" UPDATE SFCR.Z_WHS_TRACKING SET STOREHOUSEID = @C_STOREID,LOCID= @C_LOCID,USERID= @C_USERID,
//                                        RECDATE1 = SYSDATE(),STATUS= @C_RECTYPE  WHERE LOTIN = @C_RECCODE AND STATUS=0 ";
//                cmd.Parameters.AddRange(new MySqlParameter[]
//                                {
//                                new MySqlParameter("C_RECTYPE",MySqlDbType.Int32){Value=RecType},
//                                new MySqlParameter("C_STOREID",MySqlDbType.VarChar){Value=storeId},
//                                new MySqlParameter("C_LOCID",MySqlDbType.VarChar){Value=locId},
//                                new MySqlParameter("C_USERID",MySqlDbType.VarChar){Value=userid},
//                                new MySqlParameter("C_RECCODE",MySqlDbType.VarChar){Value=RecCode}
//                                });

//                LsCmd.Add(cmd);

////                foreach (Entity.Z_WHS_SAP_BACKFLUSHTable zhsb in Lszhsb)
////                {
////                    cmd = new MySqlCommand();
////                    cmd.CommandText = @"INSERT INTO  SFCR.Z_WHS_SAP_BACKFLUSH  ( WOID,PARTNUMBER,PRODUCTNAME,LOTIN,LOTIN_QTY,LOTOUT,LOTOUT_QTY,PLANT,MOVE_TYPE,UPLOAD_FLAG,UPLOAD_DATE )
////                          VALUES ( @sMO,@PartNo,@Porduct,@LTIN,@LTQTY,@LTOUT,@LTOUTQTY,@sPlan,@sMove,@sUpFlag,sysdate )";
////                    cmd.Parameters.AddRange(new MySqlParameter[]
////                    {
////                        new MySqlParameter("sMO",MySqlDbType.VarChar){Value=zhsb.WOID},            
////                        new MySqlParameter("PartNo",MySqlDbType.VarChar){Value=zhsb.PARTNUMBER},         
////                        new MySqlParameter("Porduct",MySqlDbType.VarChar){Value=zhsb.PRODUCTNAME},
////                        new MySqlParameter("LTIN",MySqlDbType.VarChar){Value=string.IsNullOrEmpty(zhsb.LOTIN)?"NA"@zhsb.LOTIN},
////                        new MySqlParameter("LTQTY",MySqlDbType.Int32){Value=zhsb.LOTIN_QTY},
////                        new MySqlParameter("LTOUT",MySqlDbType.VarChar){Value=string.IsNullOrEmpty(zhsb.LOTOUT)?"NA"@zhsb.LOTOUT},
////                        new MySqlParameter("LTOUTQTY",MySqlDbType.Int32){Value=zhsb.LOTOUT_QTY},
////                        new MySqlParameter("sPlan",MySqlDbType.VarChar){Value=zhsb.PLANT},
////                        new MySqlParameter("sMove",MySqlDbType.VarChar){Value=zhsb.MOVE_TYPE},
////                        new MySqlParameter("sUpFlag",MySqlDbType.VarChar){Value=string.IsNullOrEmpty(zhsb.UPLOAD_FLAG)?"N"@zhsb.UPLOAD_FLAG}         

////                     });
////                    LsCmd.Add(cmd);
////                }
       
//                BLL.BllMsSqllib.Instance.ExecteNonQueryArr(LsCmd);
//                return "OK";
//            }
//            catch (Exception ex)
//            {
//                return ex.Message;
//            }
  
//        }

//        #region  不使用 20131031  michael  杨丹

//        /// <summary>
//        /// 成品出库
//        /// </summary>
//        /// <param name="ESN"></param>
//        /// <returns></returns>
//        public void StockOut(int RecType, string SAPCode, string partnumber, int SAPQty, string CustomerId, string RecStyle, string userid)
//        {
//            //MySqlCommand cmd = new MySqlCommand();
//            //cmd.CommandText = "Pro_StockOut @RecType,@SAPCode,@partnumber,@SAPQty,@CustomerId,@RecStyle,@userid";
//            //cmd.Parameters.Add("@RecType", MySqlDbType.VarChar, 50).Value = RecType;
//            //cmd.Parameters.Add("@SAPCode", MySqlDbType.VarChar, 50).Value = SAPCode;
//            //cmd.Parameters.Add("@partnumber", MySqlDbType.VarChar, 50).Value =partnumber;
//            //cmd.Parameters.Add("@SAPQty", MySqlDbType.VarChar, 50).Value = SAPQty;
//            //cmd.Parameters.Add("@CustomerId", MySqlDbType.VarChar, 50).Value = CustomerId;
//            //cmd.Parameters.Add("@RecStyle", MySqlDbType.VarChar, 50).Value = RecStyle;
//            //cmd.Parameters.Add("@userid", MySqlDbType.VarChar, 50).Value = userid;
//            //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
//            //   BLL.BllMsSqllib.Instance.StockOut(RecType,  SAPCode,  partnumber,  SAPQty,  CustomerId,  RecStyle,  userid);

//        }

//        /// <summary>
//        /// 成品出库列表
//        /// </summary>
//        /// <param name="ESN"></param>
//        /// <returns></returns>
//        public System.Data.DataSet StockOutEnd(string sapcode, int RecType, int Qtype)
//        {
//            //MySqlCommand cmd = new MySqlCommand();
//            //cmd.CommandText = "Pro_StockOutEnd @sapcode,@RecType,@Qtype";
//            //cmd.Parameters.Add("@sapcode", MySqlDbType.VarChar, 50).Value = sapcode;
//            //cmd.Parameters.Add("@RecType", MySqlDbType.VarChar, 50).Value = RecType;
//            //cmd.Parameters.Add("@Qtype", MySqlDbType.VarChar, 50).Value = Qtype;
//            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
//            DataSet ds = new DataSet();
//            return ds;
//        }
//        #endregion

//        /// <summary>
//        /// 根据料号带出库存列表
//        /// </summary>
//        /// <param name="ESN"></param>
//        /// <returns></returns>
//        public System.Data.DataSet GetStockList(string partnumber, string cartonnumber)
//        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = " select woId 工单号,a.partnumber 料号,a.palletnumber 栈板号,a.cartonnumber 卡通号, a.storehouseId 仓库编号,a.locId 储位编号 ,COUNT(*) 数量, ";
//            cmd.CommandText += " Status 状态,case  status when 0 then '待入库' when 1 then '生产入库' when 2 then '客退入库'else '商务客退' end 状态描述  ";
//            cmd.CommandText += " from sfcr.Z_WHS_TRACKING a, sfcr.T_WIP_TRACKING_ONLINE  b ";
//            cmd.CommandText += " where a.partnumber =@partnumber and a.cartonnumber like @cartonnumber and a.esn =b.esn and status in (1,2,3) ";
//            cmd.CommandText += " group by woId,a.partnumber,a.palletnumber,a.cartonnumber,a.storehouseId,a.locId,status";
//            cmd.Parameters.Add("partnumber", MySqlDbType.VarChar, 50).Value = partnumber;
//            cmd.Parameters.Add("cartonnumber", MySqlDbType.VarChar, 50).Value = "%" + cartonnumber + "%";
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

//        }

//        /// <summary>
//        /// 根据卡通箱号带出该箱库存列表
//        /// </summary>
//        /// <param name="ESN"></param>
//        /// <returns></returns>
//        public System.Data.DataSet GetCartonList1(string cartonnumber)
//        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = " select a.esn,a.partnumber,a.palletnumber,a.cartonnumber, storehouseId,locId,substr(cartonnumber,1,7) woid  from  sfcr.Z_WHS_TRACKING a ";
//            cmd.CommandText += " where a.cartonnumber =@cartonnumber  and status in( 1,2,3) ";

//            cmd.Parameters.Add("cartonnumber", MySqlDbType.VarChar, 50).Value = cartonnumber;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
//        }
//        /// <summary>
//        /// 栈板拆分获取卡通信息2013-7-2
//        /// </summary>
//        /// <param name="cartonnumber"></param>
//        /// <returns></returns>
//        public System.Data.DataSet GetCartonList(string cartonnumber)
//        {
//            //MySqlCommand cmd = new MySqlCommand();
//            //cmd.CommandText = " pro_GetCartonList @cartonnumber";

//            //cmd.Parameters.Add("@cartonnumber", MySqlDbType.VarChar, 50).Value = cartonnumber;
//            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
//            //List<Entity.ProcedureKey> LsPdk = new List<Entity.ProcedureKey>();
//            //Entity.ProcedureKey Pdk = new Entity.ProcedureKey();
//            //Pdk.Variable = "V_CARTONNUMBER";
//            //Pdk.Value = cartonnumber;
//            //LsPdk.Add(Pdk);
//            //return BLL.BllMsSqllib.Instance.PublicReurnDataSet("pro_GetCartonList", LsPdk, "RES");
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = @"select a.esn,A.WOID,B.PALLETNUMBER,B.CARTONNUMBER,B.PARTNUMBER,B.STOREHOUSEID,B.LOCID,a.sntype,a.snval from sfcr.z_whs_keypart a,sfcr.z_whs_tracking b where 
//                                a.esn=b.esn and b.cartonnumber=@CTN ";
//            cmd.Parameters.Add("CTN", MySqlDbType.VarChar, 50).Value = cartonnumber;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);


//        }
//        /// <summary>
//        /// 根据 栈板号带出该栈板库存列表
//        /// </summary>
//        /// <param name="ESN"></param>
//        /// <returns></returns>
//        public System.Data.DataSet GetPalletList(string palletnumber)
//        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = " 	select palletnumber 栈板号, cartonnumber 卡通号, storehouseId,locId,COUNT(*) 数量,Status 状态, ";
//            cmd.CommandText += " case  status when 0 then '待入库' when 1 then '生产入库' when 2 then '客退入库'else '商务客退' end 状态描述 ";
//            cmd.CommandText += " from sfcr.Z_WHS_TRACKING where palletnumber =@palletnumber and status in( 1,2,3) group by palletnumber,cartonnumber , storehouseId,locId,Status ";

//            cmd.Parameters.Add("palletnumber", MySqlDbType.VarChar, 50).Value = palletnumber;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
//        }


//        /// <summary>
//        /// 拆分后更新信息
//        /// </summary>
//        /// <param name="lotno"></param>
//        /// <param name="Lot"></param>
//        public void UpdatetWhWipInfo(string esn, string reccode, string storehouseId, string locId, int utype)
//        {
//            MySqlCommand cmd = new MySqlCommand();

//            if (utype == 1)
//            {
//                cmd.CommandText = "update sfcr.Z_WHS_TRACKING set  palletnumber=@reccode  where cartonnumber=@sn ";
//                cmd.Parameters.Add("sn", MySqlDbType.VarChar, 50).Value = esn;
//                cmd.Parameters.Add("reccode", MySqlDbType.VarChar, 50).Value = reccode;
//            }
//            else
//                if (utype == 2)
//                {
//                    cmd.CommandText = " update sfcr.Z_WHS_TRACKING set  cartonnumber=@reccode where esn=@sn";
//                    cmd.Parameters.Add("sn", MySqlDbType.VarChar, 50).Value = esn;
//                    cmd.Parameters.Add("reccode", MySqlDbType.VarChar, 50).Value = reccode;

//                }
//                else
//                    if (locId.Substring(1, 2) == "4")
//                    {
//                        cmd.CommandText = " UPDATE sfcr.z_whs_tracking SET palletnumber=@storehouseId ,cartonnumber=@reccode,storehouseId=@stId ,locId=@slocId  WHERE  esn=@sn  ";
//                        cmd.Parameters.Add("sn", MySqlDbType.VarChar, 50).Value = esn;
//                        cmd.Parameters.Add("reccode", MySqlDbType.VarChar, 50).Value = reccode;
//                        cmd.Parameters.Add("storehouseId", MySqlDbType.VarChar, 50).Value = storehouseId;
//                    }
//                    else
//                    {
//                        cmd.CommandText = " update sfcr.Z_WHS_TRACKING set  cartonnumber=@reccode ,storehouseId=@stId ,locId=@slocId where esn=@sn";
//                        cmd.Parameters.Add("sn", MySqlDbType.VarChar, 50).Value = esn;
//                        cmd.Parameters.Add("reccode", MySqlDbType.VarChar, 50).Value = reccode;
//                        cmd.Parameters.Add("stId", MySqlDbType.VarChar, 50).Value = storehouseId;
//                        cmd.Parameters.Add("slocId", MySqlDbType.VarChar, 50).Value = locId;
//                    }

//            BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

//        }

//        /// <summary>
//        /// 库存报表查询
//        /// </summary>
//        /// <param name="lotno"></param>
//        /// <param name="Lot"></param>
//        public System.Data.DataSet StockQuery(string sdt, string edt, string palletnumber, int qtype, int sumstate)
//        {
//            MySqlCommand cmd = new MySqlCommand();
//            if (qtype == 1)
//            {
//                if (sumstate == 1)
//                {
//                    cmd.CommandText=@" SELECT PARTNUMBER 料号, SUM(CASE INOROUT  WHEN 'IN' THEN   QTY ELSE  0  END) 入库数量,
//                                 SUM(CASE INOROUT WHEN 'OUT' THEN QTY ELSE 0 END) 出库数量 FROM SFCR.Z_WHS_LOT_TOTALINFO
//                                   WHERE PARTNUMBER=@C_PARTNUMBER AND DATE_FORMAT(NOW(), '%Y-%m-%d') BETWEEN @SDT AND @EDT
//                                   GROUP BY PARTNUMBER";          
//                }
//                if (sumstate == 0)
//                {
//                  cmd.CommandText= @"SELECT PARTNUMBER 料号, QTY 数量,STATUS 状态,B.STATUSDESCR 状态描述,TO_CHAR(RECDATE,'YYYY/MM/DD') 日期
//                                FROM SFCR.Z_WHS_LOT_TOTALINFO A ,SFCB.B_LOTSTATUS_FORQUERY B WHERE PARTNUMBER=@C_PARTNUMBER
//                                AND DATE_FORMAT(NOW(), '%Y-%m-%d') BETWEEN @SDT AND @EDT
//                                AND A.STATUS = B.IDX";              
//                }
//                cmd.Parameters.Add("C_PARTNUMBER", MySqlDbType.VarChar).Value = palletnumber;
//                cmd.Parameters.Add("SDT", MySqlDbType.VarChar).Value = sdt;
//                cmd.Parameters.Add("EDT", MySqlDbType.VarChar).Value = edt;
//            }

//                if (qtype == 2)
//                {
//                    cmd.CommandText = @" SELECT A.PARTNUMBER, A.STOREHOUSEID, A.LOCID, B.STATUSDESCR, COUNT(1) QTY
//                                        FROM SFCR.Z_WHS_TRACKING A, SFCB.B_LOTSTATUS_FORQUERY B  WHERE A.PARTNUMBER=@C_PARTNUMBER 
//                                        AND A.STATUS IN (0, 1, 2, 3)   AND A.STATUS = B.IDX
//                                        GROUP BY A.PARTNUMBER, A.STOREHOUSEID, A.LOCID, B.STATUSDESCR";
//                    cmd.Parameters.Add("C_PARTNUMBER", MySqlDbType.VarChar).Value = palletnumber;
//                }
//                if (qtype == 3)
//                {
//                    cmd.CommandText = @" SELECT A.PARTNUMBER,
//                                        SUM(A.CNT) AS INIQTY, SUM(DECODE(STATUS, 'IN', A.CNT, 0)) AS INQTY ,  SUM(DECODE(STATUS, 'OUT', A.CNT, 0)) AS OUTQTY
//                                        FROM (SELECT partnumber,DECODE(status, 1, 'IN', 2, 'IN', 3, 'IN', 6,'OUT',7,'OUT',8,'OUT',9,'OUT',10,'OUT',11,'OUT','') AS STATUS,
//                                        COUNT(1) AS CNT FROM SFCR.Z_WHS_TRACKING  WHERE RECDATE < ADD_MONTHS(TO_DATE(@SDT||'-01','YYYY-MM-DD'),1)
//                                        GROUP BY partnumber, STATUS  ORDER BY PARTNUMBER) A   GROUP BY A.PARTNUMBER";
//                    cmd.Parameters.Add("SDT", MySqlDbType.VarChar).Value = sdt;
//                }        
//                return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
//        }

//        /// <summary>
//        /// 根据snval 带出卡通号
//        /// </summary>
//        /// <param name="ESN"></param>
//        /// <returns></returns>
//        public System.Data.DataSet GetCartonbysn(string snval)
//        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = " 	select cartonnumber,snval from  sfcr.Z_WHS_TRACKING a,SFCR.Z_WHS_KEYPART b where snval=@snval and a.esn=b.esn  ";
//            cmd.Parameters.Add("snval", MySqlDbType.VarChar, 50).Value = snval;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
//        }

        public System.Data.DataSet ReworkWipQuery(string Colnum, string Data) ///20130322
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = string.Format("select a.esn as 唯一条码,a.routgroupId as 途程代码 from sfcr.T_WIP_TRACKING_ONLINE a, sfcr.Z_WHS_TRACKING b where a.esn=b.esn and status='9' and b.{0}=@data", Colnum);
            //cmd.Parameters.Add("data", MySqlDbType.VarChar, Data.Length).Value = Data;
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            int count = 0;
            string table = "sfcr.T_WIP_TRACKING_ONLINE a, sfcr.Z_WHS_TRACKING b";
            string fieldlist = "a.esn as 唯一条码,a.routgroupId as 途程代码";
            string filter = "a.esn=b.esn and status='9' and b." + Colnum + "={0}";           
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add(Colnum, Data);
           return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);

        }
//        /// <summary>
//        /// 以卡通箱获取待入库成品信息
//        /// </summary>
//        /// <param name="ESN"></param>
//        /// <returns></returns>
//        public System.Data.DataSet GetCarinfoByCarton(string cartonnumber)
//        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = " select lotin 入库批次 ,SUBSTRING(cartonnumber,1,7) 工单号,a.partnumber 料号,c.productname 型号,case a.palletnumber when 'NA' then '' else isnull(a.palletnumber,'') end 栈板号,";
//            cmd.CommandText += " case a.cartonnumber when 'NA' then '' else isnull(a.cartonnumber,'') end 卡通号, case TRAYNO when 'NA' then '' else isnull(TRAYNO,'') end TrayNo ,COUNT(*) 数量,";
//            cmd.CommandText += " case  when status=0 then '待入库' end 状态 ";
//            cmd.CommandText += " from sfcr.Z_WHS_TRACKING a,SFCB.B_PRODUCT c ";
//            cmd.CommandText += " where a.cartonnumber =@cartonnumber  and a.status=0 and a.partnumber=c.partnumber";
//            cmd.CommandText += " group by lotin ,a.partnumber,a.palletnumber,a.cartonnumber,TRAYNO,status,c.productname	";

//            cmd.Parameters.Add("cartonnumber", MySqlDbType.VarChar, 50).Value = cartonnumber;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

//        }

//        /// <summary>
//        /// 修改仓库标志2013.11.11
//        /// </summary>
//        /// <param name="sapcode"></param>
//        /// <returns></returns>
//        public string UpdatetOutputlotrecordNew(string sapcode)
//        {
//            string Err = "OK";
//            try
//            {
//                List<MySqlCommand> LsCmd = new List<MySqlCommand>();
//              MySqlCommand cmd = new MySqlCommand();
//              cmd.CommandText = "SELECT SAPLOTCODE,PARTNUMBER,QTY FROM SFCR.T_OUTPUT_LOT_RECORD_NEW WHERE SAPLOTCODE=@sLOT";
//              cmd.Parameters.Add("sLOT", MySqlDbType.VarChar).Value = sapcode;
//              DataTable dt = BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];
//              if (dt.Rows.Count > 0)
//              {
//                  foreach (DataRow dr in dt.Rows)
//                  {
//                      cmd = new MySqlCommand();
//                      cmd.CommandText=@"insert into sfcr.z_whs_sap_backflush (woid,partnumber,productname,lotin,lotin_qty,lotout,lotout_qty,recdate,PLANT,MOVE_TYPE,UPLOAD_FLAG,UPLOAD_DATE)
//                                      values ('NA',@sPartNo,'NA','NA',0,@saplot,@sQTY,now(),'2100','601','N',SYSDATE())";
//                      cmd.Parameters.AddRange(new MySqlParameter[]
//                                {
//                                new MySqlParameter("sPartNo",MySqlDbType.VarChar){Value=dr["PARTNUMBER"].ToString()},
//                                new MySqlParameter("saplot",MySqlDbType.VarChar){Value=dr["SAPLOTCODE"].ToString()},
//                                new MySqlParameter("sQTY",MySqlDbType.Int32){Value=Convert.ToInt32( dr["QTY"].ToString())}
//                                });
//                      LsCmd.Add(cmd);
//                  }
//                  cmd = new MySqlCommand();
//                  cmd.CommandText = "update sfcr.t_output_lot_record_new set flag='2',recdate=SYSDATE() where saplotcode=@sapcode";
//                  //cmd.Parameters.Add("sapcode", MySqlDbType.VarChar).Value = sapcode;
//                  cmd.Parameters.AddRange(new MySqlParameter[]
//                                {                              
//                                new MySqlParameter("sapcode",MySqlDbType.VarChar){Value=sapcode}
//                                });
//                  LsCmd.Add(cmd);

//                  BLL.BllMsSqllib.Instance.ExecteNonQueryArr(LsCmd);
//              }
//              else
//              {
//                  Err = "NO SAP LotCode";
//              }
//            }
//            catch (Exception ex)
//            {
//                Err = ex.Message;
//            }
//            return Err;
//        }

        public System.Data.DataSet QueryZ_WIP_TRACKING(string Colnum, string sDATA)
        {
            string table = "sfcr.z_Whs_Tracking";
            string fieldlist = "ESN,WOID,PARTNUMBER,PRODUCTNAME,VERSIONCODE,TYPE,LOCSTATION,STATIONNAME,WIPSTATION,NEXTSTATION,USERID,RECDATE,"+
                            "ERRFLAG,SCRAPFLAG,SN,MAC,IMEI,CARTONNUMBER,TRAYNO,PALLETNUMBER,MCARTONNUMBER,MPALLETNUMBER,LINE,SECTIONNAME,ROUTGROUPID,"+
                            "STORENUMBER,WEIGHTQTY,QA_NO,QA_RESULT,TRACK_NO,ATE_STATION_NO,IN_LINE_TIME,BOMNUMBER,REWORKNO,lotin,storehouseid,locid,lotout,recdate1,status" ;
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add(Colnum, sDATA);
           return dp.GetData(table, fieldlist.ToUpper(), mst, out count);
        }
//        /// <summary>
//        /// 以成品料号取待库存数量
//        /// </summary>
//        /// <param name="ESN"></param>
//        /// <returns></returns>
//        public System.Data.DataSet QueryQtyByPartnumber(string partnumber, string storehouseid)
//        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = " SELECT count(*) qty,productname,partnumber,storehouseid FROM sfcr.z_whs_tracking a WHERE a.partnumber=@partnumber and a.status=1 and a.storehouseid=@storehouseid ";
//            cmd.CommandText += "  group by productname,partnumber,storehouseid  ";
//            cmd.Parameters.Add("partnumber", MySqlDbType.VarChar, 50).Value = partnumber;
//            cmd.Parameters.Add("storehouseid", MySqlDbType.VarChar, 50).Value = storehouseid;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

//        }
//        /// <summary>
//        /// 获取当前库存信息
//        /// </summary>
//        /// <param name="wt"></param>
//        /// <returns></returns>
//        public DataSet Getstoreinfo(string palletnumber)
//        {
//            MySqlCommand cmd = new MySqlCommand();           
//                    cmd.CommandText = "select count(esn),partnumber,palletnumber,storehouseid from sfcr.z_whs_tracking where status='1' ";
//                    cmd.CommandText += " and  palletnumber=@palletnumber group by partnumber,palletnumber,storehouseid ";
//                    cmd.Parameters.Add("palletnumber", MySqlDbType.VarChar).Value = palletnumber;

//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

//        }

//        public string Updatez_whs_tracking_move_status(string palletnumber, string storehouseid)
//        {
//            try
//            {                 
//                    MySqlCommand cmd = new MySqlCommand();
//                    cmd.CommandText = "update sfcr.z_whs_tracking set recdate=SYSDATE(), status='99'  where status='1' and storehouseid=@storehouseid and palletnumber=@palletnumber";
//                    cmd.Parameters.Add("palletnumber", MySqlDbType.VarChar).Value = palletnumber;
//                    cmd.Parameters.Add("storehouseid", MySqlDbType.VarChar).Value = storehouseid;
//                    BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

//                return "OK";
//            }
//            catch (Exception ex)
//            {
//                return ex.Message;
//            }

//        }

//        public DataSet checkz_whs_trackingbyLocIDSHID( string storehouseid,string locid)
//        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = " select * from sfcr.z_whs_tracking a where  a.storehouseid=@storehouseid and a.locid=@locid   ";
//            cmd.Parameters.Add("storehouseid", MySqlDbType.VarChar).Value = storehouseid;
//            cmd.Parameters.Add("locid", MySqlDbType.VarChar).Value = locid;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
//        }
//        public DataSet Getstoreinfobypallet(string palletnumber)
//        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = "select count(esn),partnumber,palletnumber,storehouseid,productname from sfcr.z_whs_tracking where status='99' ";
//            cmd.CommandText += " and  palletnumber=@palletnumber group by partnumber,palletnumber,storehouseid,productname ";
//            cmd.Parameters.Add("palletnumber", MySqlDbType.VarChar).Value = palletnumber;

//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

//        }
//        public string Updatez_whs_tracking_move_store(string palletnumber, string storenumber, string storehouseid, string locid,string status)
//        {
//            try
//            {
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandText = "update sfcr.z_whs_tracking set recdate=SYSDATE(), status=@sstatus , storenumber=@storenumber ,storehouseid=@storehouseid ,locid=@locid   where status='99' and palletnumber=@palletnumber";
//                cmd.Parameters.Add("palletnumber", MySqlDbType.VarChar, 50).Value = palletnumber;
//                cmd.Parameters.Add("storenumber", MySqlDbType.VarChar, 50).Value = storenumber;
//                cmd.Parameters.Add("storehouseid", MySqlDbType.VarChar, 50).Value = storehouseid;
//                cmd.Parameters.Add("locid", MySqlDbType.VarChar, 50).Value = locid;
//                cmd.Parameters.Add("sstatus", MySqlDbType.VarChar, 50).Value = status;

//                BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

//                return "OK";
//            }
//            catch (Exception ex)
//            {
//                return ex.Message;
//            }
//        }
//        /// <summary>
//        /// 获取当前箱号信息
//        /// </summary>
//        /// <param name="wt"></param>
//        /// <returns></returns>
//        public DataSet Getstoreinfobycarton(string cartonnumber)
//        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = "SELECT a.partnumber,a.palletnumber,a.cartonnumber,a.esn,a.storehouseid,a.locid FROM sfcr.z_whs_tracking a WHERE  a.status='1' ";
//            cmd.CommandText += "  and a.cartonnumber=@carton ";
//            cmd.Parameters.Add("carton", MySqlDbType.VarChar).Value = cartonnumber;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
//        }

//        /// <summary>
//        /// 获取当前栈板信息
//        /// </summary>
//        /// <param name="wt"></param>
//        /// <returns></returns>
//        public DataSet Getstorebypallet(string palletnumber)
//        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = "select a.partnumber,a.palletnumber,a.cartonnumber,a.storenumber,a.locid,count(a.esn) from sfcr.z_whs_tracking a where palletnumber=@pallet and a.status='1' ";
//            cmd.CommandText += " group by a.partnumber,a.palletnumber,a.cartonnumber,a.storenumber,a.locid order by a.cartonnumber,a.storenumber,a.locid ";
//            cmd.Parameters.Add("pallet", MySqlDbType.VarChar).Value = palletnumber;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
//        }

//        public void UpdatetWhWipInfobypallet(string palletnumber, string cartonnumber, string esn, string storehouseId, string locId, int utype)
//        {
//            MySqlCommand cmd = new MySqlCommand();

//            if (utype == 1)
//            {
//                cmd.CommandText = " UPDATE sfcr.z_whs_tracking SET palletnumber=@pallet ,cartonnumber=@carton,storehouseId=@stId ,locId=@slocId  WHERE  esn=@sn  ";
//                cmd.Parameters.Add("pallet", MySqlDbType.VarChar, 50).Value = palletnumber;
//                cmd.Parameters.Add("carton", MySqlDbType.VarChar, 50).Value = cartonnumber;
//                cmd.Parameters.Add("sn", MySqlDbType.VarChar, 50).Value = esn;
//                cmd.Parameters.Add("stId", MySqlDbType.VarChar, 50).Value = storehouseId;
//                cmd.Parameters.Add("slocId", MySqlDbType.VarChar, 50).Value = locId;
//            }
//            else
//                if (utype == 2)
//                {
//                    cmd.CommandText = " UPDATE sfcr.z_whs_tracking SET palletnumber=@pallet ,locId=@slocId  WHERE  cartonnumber=@carton  ";
//                    cmd.Parameters.Add("pallet", MySqlDbType.VarChar, 50).Value = palletnumber;
//                    cmd.Parameters.Add("carton", MySqlDbType.VarChar, 50).Value = cartonnumber;
//                    cmd.Parameters.Add("slocId", MySqlDbType.VarChar, 50).Value = locId;
                
//                }
            
//            BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

//        }

//        public System.Data.DataSet GetQueryWip(string ColumnName, string Data)
//        {
//            string sSQL = "select esn as 唯一条码,woId as 工单,partnumber as 产品料号,locstation as 当前站位,wipstation as 下一站,nextstation as 优先途程,userId as 人员权限,recdate as 时间,errflag as 错误标记,scrapflag as 报废标记,cartonnumber as 产品箱号,TrayNO as Tray盘号,palletnumber as 栈板号,SN," +
//                        "MAC,mcartonnumber as 客户箱号,mpalletnumber as 客户栈板号,line as 线别,routgroupId as 途程代码,storenumber as 入库编号 from sfcr.z_whs_tracking where ";
//            sSQL = sSQL + ColumnName + " =@Data ";
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = sSQL;
//            cmd.Parameters.Add("Data", MySqlDbType.VarChar, 50).Value = Data;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
//        }

        public string Update_Z_WHS_TRACKING_STATUS(string StockNo, string woId, string Partnumber, string Status, string UserId)
        {
            try
            {
                StringBuilder ofilter = new StringBuilder();
                ofilter.Append("STOREHOUSEID = {0},LOCID={1},USERID={2},RECDATE1={3},STATUS={4} ");
                IDictionary<string, object> modFields = new Dictionary<string, object>();
                modFields.Add("STOREHOUSEID", "NA");
                modFields.Add("LOCID", "NA");
                modFields.Add("USERID", UserId);
                modFields.Add("RECDATE1", System.DateTime.Now);
                modFields.Add("STATUS", Status);

                string filter = "LOTIN = {0} AND WOID = {1}";
                IDictionary<string, object> keyVals = new Dictionary<string, object>();
                keyVals.Add("LOTIN", StockNo);
                keyVals.Add("WOID", woId);

                TransactionManager.UpdateBatchData("SFCR.Z_WHS_TRACKING", ofilter.ToString(), modFields, filter, keyVals);

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandText = @" UPDATE SFCR.Z_WHS_TRACKING SET STOREHOUSEID = @C_STOREID,LOCID= @C_LOCID,USERID= @C_USERID,
//                                        RECDATE1 = NOW(),STATUS= @C_RECTYPE  WHERE LOTIN = @C_RECCODE AND WOID=@C_WOID";
//                cmd.Parameters.AddRange(new MySqlParameter[]
//                                {
//                                new MySqlParameter("C_RECTYPE",MySqlDbType.Int32){Value=Status},
//                                new MySqlParameter("C_STOREID",MySqlDbType.VarChar){Value="NA"},
//                                new MySqlParameter("C_LOCID",MySqlDbType.VarChar){Value="NA"},
//                                new MySqlParameter("C_USERID",MySqlDbType.VarChar){Value=UserId},
//                                new MySqlParameter("C_RECCODE",MySqlDbType.VarChar){Value=StockNo},
//                                 new MySqlParameter("C_WOID",MySqlDbType.VarChar){Value=woId}
//                                });

//                BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Update_Z_WHS_TRACKING_STATUS_RWK(string sDATA, string sCMD, string Status, string UserId, string SfcLotOut)
        {
            try
            {
                //List<MySqlCommand> LsCmd = new List<MySqlCommand>();
                //MySqlCommand cmd = null;
                string sColnum = string.Empty;
                switch (sCMD)
                {
                    case "ESN":
                        sColnum = "ESN";
                        break;
                    case "TRAY":
                        sColnum = "TRAYNO";
                        break;
                    case "CARTON":
                        sColnum = "CARTONNUMBER";
                        break;
                    case "PALLET":
                        sColnum = "PALLETNUMBER";
                        break;
                    default:
                        break;
                }

                if (sColnum == "ESN")
                {
                    StringBuilder ofilter = new StringBuilder();
                    ofilter.Append("USERID = {0},STATUS={1},LOTOUT={2},RECDATE1={3} ");
                    IDictionary<string, object> modFields = new Dictionary<string, object>();
                    modFields.Add("USERID", UserId);
                    modFields.Add("STATUS", Status);
                    modFields.Add("LOTOUT", SfcLotOut);
                    modFields.Add("RECDATE1",System.DateTime.Now);

                    string filter = "ESN = {0}";
                    IDictionary<string, object> keyVals = new Dictionary<string, object>();
                    keyVals.Add("ESN", sDATA);

                    TransactionManager.UpdateBatchData("SFCR.Z_WHS_TRACKING", ofilter.ToString(), modFields, filter, keyVals);

                    //cmd = new MySqlCommand();
                    //cmd.CommandText = @" UPDATE SFCR.Z_WHS_TRACKING SET  USERID=@C_USERID,STATUS=@C_RECTYPE,LOTOUT=@C_LOTOUT,RECDATE1 = NOW()  WHERE ESN =@C_ESN ";
                    //cmd.Parameters.AddRange(new MySqlParameter[]
                    //            {
                    //            new MySqlParameter("C_USERID",MySqlDbType.VarChar){Value=UserId},
                    //            new MySqlParameter("C_RECTYPE",MySqlDbType.Int32){Value=Convert.ToInt32(Status)},           
                    //            new MySqlParameter("C_LOTOUT",MySqlDbType.VarChar){Value=SfcLotOut},
                    //            new MySqlParameter("C_ESN",MySqlDbType.VarChar){Value=sDATA}
                    //            });
                    //LsCmd.Add(cmd);
                }
                else
                {
                    DataTable dt = QueryZ_WIP_TRACKING(sColnum, sDATA).Tables[0];
                    IDictionary<string, object> keyVals = new Dictionary<string, object>();
                    StringBuilder ofilter = new StringBuilder();
                    foreach (DataRow dr in dt.Rows)
                    {
                        ofilter = new StringBuilder();
                        ofilter.Append("USERID = {0},STATUS={1},LOTOUT={2},RECDATE1={3} ");
                        IDictionary<string, object> modFields = new Dictionary<string, object>();
                        modFields.Add("USERID", UserId);
                        modFields.Add("STATUS", Status);
                        modFields.Add("LOTOUT", SfcLotOut);
                        modFields.Add("RECDATE1", System.DateTime.Now);

                        string filter = "ESN = {0}";
                        keyVals = new Dictionary<string, object>();
                        keyVals.Add("ESN", sDATA);
                        TransactionManager.UpdateBatchData("SFCR.Z_WHS_TRACKING", ofilter.ToString(), modFields, filter, keyVals);
                        //cmd = new MySqlCommand();
                        //cmd.CommandText = @" UPDATE SFCR.Z_WHS_TRACKING SET  USERID=@C_USERID,STATUS=@C_RECTYPE,LOTOUT=@C_LOTOUT,RECDATE1 = SYSDATE()  WHERE ESN =@C_ESN ";
                        //cmd.Parameters.AddRange(new MySqlParameter[]
                        //        {
                        //        new MySqlParameter("C_USERID",MySqlDbType.VarChar){Value=UserId},
                        //        new MySqlParameter("C_RECTYPE",MySqlDbType.Int32){Value=Convert.ToInt32(Status)},           
                        //        new MySqlParameter("C_LOTOUT",MySqlDbType.VarChar){Value=SfcLotOut},
                        //        new MySqlParameter("C_ESN",MySqlDbType.VarChar){Value=dr["ESN"].ToString()}
                        //        });
                        //LsCmd.Add(cmd);
                    }
                }

                //BLL.BllMsSqllib.Instance.ExecteNonQueryArr(LsCmd);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

//        #region pandian
//        public System.Data.DataSet get_Z_WIP_TRACKING(string Colnum, string sDATA, int sFLAG)
//        {
//            //sFLAG@ 1 在库 6 出货 9 重工
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = string.Format(@" select ESN ,PARTNUMBER,RECDATE,CARTONNUMBER,PALLETNUMBER,MCARTONNUMBER,MPALLETNUMBER,STOREHOUSEID,LOCID,STATUS from  sfcr.z_Whs_Tracking where {0}=@sData and status=@sFLAG ", Colnum);
//            cmd.Parameters.Add("sData", MySqlDbType.VarChar).Value = sDATA;
//            cmd.Parameters.Add("sFLAG", MySqlDbType.Int32).Value = sFLAG;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
//        }

//        public string update_Z_WIP_TRACKING(string Colnum, string sDATA)
//        {
//            MySqlCommand cmd = new MySqlCommand();

//            cmd.CommandText = string.Format(@" UPDATE sfcr.z_whs_tracking SET status='12'  WHERE  {0}=@sData and status='1'  ", Colnum);
//            cmd.Parameters.Add("sData", MySqlDbType.VarChar).Value = sDATA;
//            BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
//            return "OK";
//        }
//        public System.Data.DataSet get_keypart(string esn_key)
//        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = "  SELECT esn,woid,sntype,snval FROM SFCR.Z_WHS_KEYPART where esn=@sesn";
//            cmd.Parameters.Add("sesn", MySqlDbType.VarChar).Value = esn_key;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
//        }
//        #endregion

        //        /// <summary>
        //        /// 以批次获取待入库成品信息
        //        /// </summary>
        //        /// <param name="ESN"></param>
        //        /// <returns></returns>
        //        public System.Data.DataSet GetlotinfoByLotin(string lotin)
        //        {
        //            MySqlCommand cmd = new MySqlCommand();
        //            cmd.CommandText = @" select lotin 入库批次 ,a.WOID 工单号,a.partnumber 料号,a.productname 型号, a.palletnumber  栈板号,
        //             a.cartonnumber  卡通号, TRAYNO  TrayNo ,COUNT(1) 数量, case  when status=0 then '待入库' end 状态
        //            from sfcr.Z_WHS_TRACKING a  where lotin =@lotin  and a.status=0 
        //            group by lotin ,a.woid,a.partnumber,a.palletnumber,a.cartonnumber,trayNO,status,a.productname  ";
        //            cmd.Parameters.Add("lotin", MySqlDbType.VarChar, 50).Value = lotin;
        //            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

        //        }

        /// <summary>
        /// 以批次获取待入库成品信息
        /// </summary>
        /// <param name="ESN"></param>
        /// <returns></returns>
        public System.Data.DataSet Get_Warehouse_number_Info(string lotin)
        {
            int count = 0;
            string table = "sfcr.Z_WHS_TRACKING".ToUpper();
            string fieldlist = "lotin,WOID ,partnumber ,productname ,COUNT(1) QTY, case  when status=0 then '待入库' end 状态";
            string filter = "lotin ={0} and status=0 group by lotin ,woid,partnumber,status,productname ";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("lotin", lotin);
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);

//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = @" select lotin,WOID ,partnumber ,productname ,COUNT(1) QTY, case  when status=0 then '待入库' end 状态
//                    from sfcr.Z_WHS_TRACKING where lotin =@slotin and status=0 
//                    group by lotin ,woid,partnumber,status,productname 	";
//            cmd.Parameters.Add("slotin", MySqlDbType.VarChar).Value = lotin;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

        }

        //        /// <summary>
        //        /// 用于成品接收入库查询信息
        //        /// </summary>
        //        public DataSet GetProductAllSerial(string flag, string value)
        //        {

        //            MySqlCommand cmd = new MySqlCommand();
        //            cmd.CommandText = string.Format(@"SELECT a.esn,a.woid,b.sntype,b.snval FROM (select  esn,woid from SFCR.z_Whs_Tracking where  {0}=@sVal) a,SFCR.z_Whs_Keypart B WHERE A.ESN=B.ESN", flag);
        //            cmd.Parameters.Add("sVal", MySqlDbType.VarChar).Value = value;
        //            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);           

        //        }
        /// <summary>
        /// 用于成品查询信息(未经处理)
        /// </summary>
        public DataSet GetProductAllInfo(string flag, string value)
        {



            int count = 0;
            string table = null;
            string fieldlist = null;
            string filter = null;
            IDictionary<string, object> mst = new Dictionary<string, object>();
            switch (flag.ToUpper())
            {
                case "SNVAL":
                    table ="sfcr.z_whs_keypart";
                    fieldlist = "esn,woid,sntype,snval,station,kpno,recdate";
                     filter = "snval={0}";
                    mst.Add("snval".ToUpper(), value);
                    break;
                case "ESN":
                    table = "sfcr.z_whs_keypart";
                    fieldlist = "esn,woid,sntype,snval,station,kpno,recdate";     
                     filter = "esn={0}";
                    mst.Add("esn".ToUpper(), value);
                    break;
                default:
                    table = "sfcr.z_whs_tracking a,sfcr.z_whs_keypart b";
                    fieldlist = "a.esn,a.woId,a.partnumber,a.productname,a.cartonnumber,a.mcartonnumber,a.palletnumber,a.mpalletnumber,b.sntype,b.snval";
                    filter = "a."+flag+"={0} and  a.esn=b.esn";
                    mst.Add(flag.ToUpper(), value);
                    break;
            }            
              
            return TransactionManager.GetData(table.ToUpper(), fieldlist.ToUpper(), filter.ToUpper(), mst, null, null, out count);
        }
        //        public DataSet GetDealWithData(DataTable _dt)
        //        {
        //            DataTable _mdt = _dt.Clone();
        //            DataTable _dtTemp = _dt.DefaultView.ToTable(true, "esn");//筛选出不同esn的行
        //            DataSet mds = new DataSet();
        //            int x = 0;
        //            foreach (DataRow dr in _dtTemp.Rows)
        //            {
        //                Dictionary<string, string> _ser = new Dictionary<string, string>();
        //                DataRow[] arrDr = null;
        //                for (int i = 0; i < _mdt.Columns.Count; i++)
        //                {

        //                    string sql = string.Format("esn='{0}' and {1}<>'{2}'", dr["esn"].ToString(), _mdt.Columns[i].ColumnName, "NA");
        //                    arrDr = _dt.Select(sql);
        //                    if (_mdt.Columns[i].ColumnName.ToUpper() == "ESN" || _mdt.Columns[i].ColumnName.ToUpper() == "WOID"
        //                        || _mdt.Columns[i].ColumnName.ToUpper() == "PALLETNUMBER" || _mdt.Columns[i].ColumnName.ToUpper() == "CARTONNUMBER"
        //                        || _mdt.Columns[i].ColumnName.ToUpper() == "PARTNUMBER" || _mdt.Columns[i].ColumnName.ToUpper() == "LOTIN"
        //                        || _mdt.Columns[i].ColumnName.ToUpper() == "LOCID" || _mdt.Columns[i].ColumnName.ToUpper() == "STOREHOUSEID")
        //                    {
        //                        _ser.Add(_mdt.Columns[i].ColumnName, arrDr[0][_mdt.Columns[i].ColumnName].ToString());
        //                        continue;
        //                    }
        //                    if (arrDr == null || arrDr.Length < 1)
        //                    {
        //                        string sql1 = string.Format(" esn='{0}' and {1}='{2}'", dr["esn"].ToString(), _mdt.Columns[i].ColumnName, "NA");
        //                        arrDr = _dt.Select(sql1);
        //                        _ser.Add(_mdt.Columns[i].ColumnName, arrDr[0][_mdt.Columns[i].ColumnName].ToString());
        //                        continue;
        //                    }
        //                    //return null;
        //                    if (arrDr != null && arrDr.Length > 1)
        //                        return null;
        //                    _ser.Add(_mdt.Columns[i].ColumnName, arrDr[0][_mdt.Columns[i].ColumnName].ToString());
        //                }
        //                _mdt.Rows.Add(arrDr[0].ItemArray);
        //                foreach (string str in _ser.Keys)
        //                {
        //                    _mdt.Rows[x][str] = _ser[str];
        //                }
        //                x++;
        //            }
        //            mds.Tables.Add(_mdt);
        //            return mds;
        //        }
        //        /// <summary>
        //        /// 用于拆分合并打印查询信息
        //        /// </summary>
        //        public DataSet GetProductInstockSerialInfo(string flag, string value)
        //        {         

        //            MySqlCommand cmd = new MySqlCommand();
        //            cmd.CommandText = @"SELECT p.esn,p.woid,p.sntype,p.snval FROM SFCR.T_CARTON_INFO_HAD H,SFCR.T_CARTON_INFO_DTA D,
        //             SFCR.T_WIP_KEYPART_ONLINE P  WHERE H.CARTONID = D.CARTONID   AND P.ESN = D.ESN  AND H.CARTONID = @CTN";
        //            cmd.Parameters.Add("CTN", MySqlDbType.VarChar).Value = value;
        //            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //        }

        //        /// <summary>
        //        /// 获取待分配库位的在库成品
        //        /// </summary>
        //        public DataSet GetProductNotLocId()
        //        {
        //            MySqlCommand cmd = new MySqlCommand();
        //            cmd.CommandText = "select lotin 入库批次,a.partnumber 料号,c.productname 型号,NVL(a.palletnumber, 'NA') 栈板号, ";
        //            cmd.CommandText += "NVL(a.cartonnumber, 'NA') 卡通号,  nvl(Trayno,'NA')  TrayNo ,COUNT(*) 数量, ";
        //            cmd.CommandText += "case  when status in(1,2,3) then '已在库' end 状态,NVL(storehouseId ,'NA')  仓库,locId 库位 ";
        //            cmd.CommandText += "from sfcr.Z_WHS_TRACKING a, SFCB.B_PRODUCT c ";
        //            cmd.CommandText += "where  a.status in(1,2,3)  and a.partnumber=c.partnumber ";
        //            cmd.CommandText += "group by lotin,a.partnumber,a.palletnumber,a.cartonnumber,trayNO,status,storehouseId,locId,c.productname";
        //            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //        }

        //        /// <summary>
        //        /// 获取待分配库位的在库成品
        //        /// </summary>
        //        public DataSet GetProductNotLocId_1()
        //        {
        //            MySqlCommand cmd = new MySqlCommand();
        //            cmd.CommandText = "select lotin 入库批次 ,woId 工单号,a.partnumber 料号,c.productname 型号,case a.palletnumber when 'NA' then '' else isnull(a.palletnumber,'') end 栈板号, ";
        //            cmd.CommandText += "case a.cartonnumber when 'NA' then '' else isnull(a.cartonnumber,'') end 卡通号, case TRAYNO when 'NA' then '' else isnull(TRAYNO,'') end TrayNo ,COUNT(*) 数量, ";
        //            cmd.CommandText += "case  when status in(1,2,3) then '已在库' end 状态,case storehouseId when 'NA' then '' else isnull(storehouseId,'') end 仓库,locId 库位 ";
        //            cmd.CommandText += "from sfcr.Z_WHS_TRACKING a, sfcr.T_WIP_TRACKING_ONLINE b,SFCB.B_PRODUCT c ";
        //            cmd.CommandText += "where a.esn =b.esn and a.status in(1,2,3) and storehouseId='1000' and a.partnumber=c.partnumber ";
        //            cmd.CommandText += "group by lotin ,woId,a.partnumber,a.palletnumber,a.cartonnumber,TRAYNO,status,storehouseId,locId,c.productname";
        //            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //        }

        //        public void UpdateStoreLocId(string rectype, string reccode, string storehouseId, string locId)
        //        {
        //            MySqlCommand cmd = new MySqlCommand();
        //            cmd.CommandText = "update sfcr.Z_WHS_TRACKING set storehouseId=@storehouseId,locId=@locId ";
        //            cmd.CommandText += " where case @rectype when '0' then lotin when '1' then palletnumber ";
        //            cmd.CommandText += "when '2' then cartonnumber when '3' then TRAYNO end=@reccode";
        //            cmd.Parameters.Add("rectype", MySqlDbType.VarChar, 50).Value = rectype;
        //            cmd.Parameters.Add("reccode", MySqlDbType.VarChar, 50).Value = reccode;
        //            cmd.Parameters.Add("storehouseId", MySqlDbType.VarChar, 50).Value = storehouseId;
        //            cmd.Parameters.Add("locId", MySqlDbType.VarChar, 50).Value = locId;
        //            BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        //        }
//        /// <summary>
//        /// 综合查询
//        /// </summary>
//        /// <param name="wt"></param>
//        /// <returns></returns>
//        public DataSet GetProductInfo(Entity.tWarehouseWipTrackingTable wt)
//        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = @"select b.esn,b.woid,a.partnumber,a.productname,a.cartonnumber,a.palletnumber,b.sntype,b.snval from sfcr.z_whs_tracking a,sfcr.z_whs_keypart b,
//                                        sfcr.t_output_lot_record_new c where  c.saplotcode=@sapcode and c.flag>0 and a.lotout=c.sfclotcode
//                                        and a.esn=b.esn(+)";
//            cmd.Parameters.Add("sapcode", MySqlDbType.VarChar).Value = wt.SapCode;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

//        }

        //        //public string CancelSFClotcode(string SAPCode, string partnumber, string sfclotcode)
        //        //{
        //        //    return BLL.BllMsSqllib.Instance.CancelSFClotcode(SAPCode, partnumber, sfclotcode);

        //        //}

        //        public DataSet GetdgvStockList(string partnumber)
        //        {
        //            MySqlCommand cmd = new MySqlCommand();
        //            cmd.CommandText = " select SUBSTRING(cartonnumber,1,7) 工单号,a.partnumber 料号,a.palletnumber 栈板号,a.cartonnumber 卡通号, a.storehouseId 仓库编号,a.locId 储位编号 ,COUNT(*) 数量, ";
        //            cmd.CommandText += " Status 状态,case  status when 0 then '待入库' when 1 then '生产入库' when 2 then '客退入库'else '商务客退' end 状态描述  ";
        //            cmd.CommandText += " from sfcr.Z_WHS_TRACKING a ";
        //            cmd.CommandText += " where a.partnumber =@partnumber  and status in (1,2  ,3)  and (a.lotout is null or a.lotout='')";
        //            cmd.CommandText += " group by a.partnumber,a.palletnumber,a.cartonnumber,a.storehouseId,a.locId,status";

        //            cmd.Parameters.Add("@partnumber", MySqlDbType.VarChar, 20).Value = partnumber;
        //            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //        }
        //        /// <summary>
        //        /// 检测拣货出库信息
        //        /// </summary>
        //        /// <param name="lotout"></param>
        //        /// <returns></returns>
        //        //public DataSet CheckStockList(string lotcode, string flag)
        //        //{
        //        //    List<Entity.ProcedureKey> LsPdk = new List<ProcedureKey>();
        //        //    Entity.ProcedureKey Pdk = null;

        //        //    Pdk = new ProcedureKey();
        //        //    Pdk.Variable = "lotcode";
        //        //    Pdk.Value = lotcode;
        //        //    LsPdk.Add(Pdk);

        //        //    Pdk = new ProcedureKey();
        //        //    Pdk.Variable = "flag";
        //        //    Pdk.Value = flag;
        //        //    LsPdk.Add(Pdk);

        //        //    return BLL.BllMsSqllib.Instance.PublicReurnDataSet("pro_CheckStockList", LsPdk, "RES");
        //        //}
        //        /// <summary>
        //        /// 默认拆分箱
        //        /// </summary>
        //        /// <param name="cartonnumber"></param>
        //        /// <param name="newcarton"></param>
        //        /// <param name="count"></param>
        //        public void DefaultDataPartition(string cartonnumber, string newcarton, string count)
        //        {
        //            MySqlCommand cmd = new MySqlCommand();
        //            cmd.CommandText = " UPDATE SFCR.Z_WHS_TRACKING SET CARTONNUMBER = @newcarton  WHERE CARTONNUMBER = @cartonnumber  AND ROWNUM <= CNT";
        //            cmd.Parameters.Add("cartonnumber", MySqlDbType.VarChar, 20).Value = cartonnumber;
        //            cmd.Parameters.Add("newcarton", MySqlDbType.VarChar, 20).Value = newcarton;
        //            cmd.Parameters.Add("CNT", MySqlDbType.Int32).Value = Convert.ToInt32(count);
        //            BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);


        //        }

        //        public DataSet GetSAPInfo(string saplotcode)
        //        {
        //            MySqlCommand cmd = new MySqlCommand();
        //            cmd.CommandText = "select a.saplotcode as VBELN,a.partnumber as MATNR,c.productdesc as MAKTX, "
        //                + "a.qty as LFIMG,a.sapwarehouse as WERKS,b.contactperson as KUNNR,b.customername as NAME1 "
        //                + "from SFCR.T_OUTPUT_LOT_RECORD_NEW a,SFCB.B_CUSTOMER b,SFCB.B_PRODUCT c "
        //                + "where a.customerId=b.customerId and a.partnumber=c.partnumber and a.saplotcode=@saplotcode";
        //            cmd.Parameters.Add("saplotcode", MySqlDbType.VarChar, 15).Value = saplotcode;
        //            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //        }
        //        public DataSet GetSAP_DN(string Colnum,string DATA)
        //        {
        //            MySqlCommand cmd = new MySqlCommand();
        //            cmd.CommandText =string.Format( @"SELECT SAPLOTCODE,PARTNUMBER,QTY,CUSTOMERID,SAPWAREHOUSE,SFCLOTCODE,USERID,RECDATE,FLAG,CUSTOMERADDRESS,CONTRACTNO,CUSTOMERNAME2 FROM SFCR.T_OUTPUT_LOT_RECORD_NEW WHERE
        //                                             {0}=@sDATA",Colnum);
        //            cmd.Parameters.Add("sDATA", MySqlDbType.VarChar).Value = DATA;
        //            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //        }
        //        //public string InsertOutPutRecordList(List<Entity.tSapLodeInfo> lssap)
        //        //{
        //        //    try
        //        //    {
        //        //        MySqlCommand cmd = new MySqlCommand();
        //        //        cmd.CommandText = "delete from SFCR.T_OUTPUT_LOT_RECORD_NEW where saplotcode=@sapcode and flag=0";
        //        //        cmd.Parameters.Add("sapcode", MySqlDbType.VarChar).Value = lssap[0].SAPCode;

        //        //        BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        //        //        string err = string.Empty;
        //        //        foreach (tSapLodeInfo itemSap in lssap)
        //        //        {                   
        //        //            err = SaveSAPOutInfo(itemSap);
        //        //        }
        //        //        return err;
        //        //    }
        //        //    catch (Exception ex)
        //        //    {
        //        //        return ex.Message;
        //        //    }
        //        //}
        //        //public string SaveSAPOutInfo(Entity.tSapLodeInfo sinfo)
        //        //{
        //        //    return BLL.BllMsSqllib.Instance.SaveSAPOutInfo(sinfo);
        //        //}

        //        //public System.Data.DataSet ProductOutPick(string serialval, string flag, out string Err)
        //        //{     

        //        //    List<Entity.ProcedureKey> LsPdk = new List<Entity.ProcedureKey>();
        //        //    Entity.ProcedureKey Pdk = null;
        //        //    Pdk = new Entity.ProcedureKey();
        //        //    Pdk.Variable = "SERIALVAL";
        //        //    Pdk.Value = serialval;
        //        //    LsPdk.Add(Pdk);

        //        //    Pdk = new Entity.ProcedureKey();
        //        //    Pdk.Variable = "FLAG";
        //        //    Pdk.Value = flag;
        //        //    LsPdk.Add(Pdk);

        //        //    return BLL.BllMsSqllib.Instance.PublicReurnDataSetOutString("PRO_PRODUCTOUTPICKII", LsPdk, "RESGRID", out Err);
        //        //}  

        //        public System.Data.DataSet GetProductOutPickInfo(string keyvalue, string flag)
        //        {
        //            MySqlCommand cmd = new MySqlCommand();
        //            if (flag == "1")
        //                cmd.CommandText = "select esn,cartonnumber,palletnumber,TRAYNO,partnumber,status from SFCR.Z_WHS_TRACKING  where cartonnumber=@serialval and status in(1,2,3) ";
        //            if (flag == "2")
        //                cmd.CommandText = "select esn,cartonnumber,palletnumber,TRAYNO,partnumber,status from SFCR.Z_WHS_TRACKING  where palletnumber=@serialval and status in(1,2,3) ";
        //            if (flag == "3")
        //                cmd.CommandText = "select esn,cartonnumber,palletnumber,TRAYNO,partnumber,status from SFCR.Z_WHS_TRACKING  where TRAYNO=@serialval and status in(1,2,3) ";
        //            cmd.Parameters.Add("serialval", MySqlDbType.VarChar).Value = keyvalue;

        //            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //        }
        //        /// <summary>
        //        /// 输入序列号，查询要出库信息
        //        /// </summary>
        //        /// <param name="snval"></param>
        //        /// <param name="flag"></param>
        //        /// <returns></returns>
        //        public System.Data.DataSet GetProductInfoBySN(string snval, string snvalend, string flag)
        //        {
        //            MySqlCommand cmd = new MySqlCommand();
        //            int ParamFlag = 0;
        //            switch (flag)
        //            {
        //                case "0":
        //                    cmd.CommandText = @" SELECT ESN,CARTONNUMBER,PALLETNUMBER,TRAYNO,PARTNUMBER,STATUS FROM SFCR.Z_WHS_TRACKING
        //                                     WHERE SN=@C_SNVAL";
        //                    ParamFlag = 0;
        //                    break;
        //                case "1":
        //                    if (snvalend == "NA")
        //                    {
        //                        cmd.CommandText = @" SELECT ESN,CARTONNUMBER,PALLETNUMBER,TRAYNO,PARTNUMBER,STATUS FROM SFCR.Z_WHS_TRACKING
        //                                         WHERE CARTONNUMBER>=@C_SNVAL AND CARTONNUMBER<=@C_SNVALEND";
        //                        ParamFlag = 1;
        //                    }
        //                    else
        //                    {
        //                        cmd.CommandText = @"SELECT ESN,CARTONNUMBER,PALLETNUMBER,TRAYNO,PARTNUMBER,STATUS FROM SFCR.Z_WHS_TRACKING WHERE CARTONNUMBER=@C_SNVAL";
        //                        ParamFlag = 0;
        //                    }
        //                    break;
        //                case "2":
        //                    cmd.CommandText = @" SELECT ESN,CARTONNUMBER,PALLETNUMBER,TRAYNO,PARTNUMBER,STATUS FROM SFCR.Z_WHS_TRACKING
        //                                         WHERE CARTONNUMBER =(SELECT  CASE CARTONNUMBER WHEN 'NA' THEN 'NODATA' ELSE CARTONNUMBER END  FROM SFCR.Z_WHS_TRACKING WHERE SN=@C_SNVAL) ";
        //                    ParamFlag = 0;
        //                    break;
        //                case "3":
        //                    cmd.CommandText = @" SELECT ESN,CARTONNUMBER,PALLETNUMBER,TRAYNO,PARTNUMBER,STATUS FROM SFCR.Z_WHS_TRACKING
        //                                         WHERE PALLETNUMBER=@C_SNVAL ";
        //                    ParamFlag = 0;
        //                    break;
        //                case "4":
        //                    cmd.CommandText = @" SELECT ESN,CARTONNUMBER,PALLETNUMBER,TRAYNO,PARTNUMBER,STATUS FROM SFCR.Z_WHS_TRACKING
        //                                         WHERE PALLETNUMBER =(SELECT  CASE  PALLETNUMBER WHEN 'NA' THEN 'NODATA' ELSE PALLETNUMBER END FROM SFCR.Z_WHS_TRACKING WHERE SN=@C_SNVAL)";
        //                    ParamFlag = 0;
        //                    break;
        //                case "5":
        //                    cmd.CommandText = @" SELECT ESN,CARTONNUMBER,PALLETNUMBER,TRAYNO,PARTNUMBER,STATUS FROM SFCR.Z_WHS_TRACKING
        //                                      WHERE TRAYNO =(SELECT case TRAYNO when 'NA' THEN 'NODATA' ELSE TRAYNO END FROM SFCR.Z_WHS_TRACKING WHERE SN=@C_SNVAL)";
        //                    ParamFlag = 0;
        //                    break;
        //                case  "6":
        //                     cmd.CommandText = @" SELECT ESN,CARTONNUMBER,PALLETNUMBER,TRAYNO,PARTNUMBER,STATUS FROM SFCR.Z_WHS_TRACKING
        //                                     WHERE IMEI=@C_SNVAL";
        //                    ParamFlag = 0;
        //                    break;
        //                case "7":
        //                    cmd.CommandText = @" SELECT ESN,CARTONNUMBER,PALLETNUMBER,TRAYNO,PARTNUMBER,STATUS FROM SFCR.Z_WHS_TRACKING
        //                                     WHERE MAC=@C_SNVAL";
        //                    ParamFlag = 0;
        //                    break;
        //                case "8":
        //                    cmd.CommandText = @" SELECT ESN,CARTONNUMBER,PALLETNUMBER,TRAYNO,PARTNUMBER,STATUS FROM SFCR.Z_WHS_TRACKING
        //                                     WHERE ESN=@C_SNVAL";
        //                    ParamFlag = 0;
        //                    break;



        //            }
        //            #region 增加CMD值
        //            if (ParamFlag == 0)
        //            {
        //                cmd.Parameters.AddRange(new MySqlParameter[]  
        //                        {               
        //                        new MySqlParameter("C_SNVAL",MySqlDbType.VarChar){Value=snval}
        //                        });
        //            }
        //            else
        //            {
        //                cmd.Parameters.AddRange(new MySqlParameter[]  
        //                        {               
        //                        new MySqlParameter("C_SNVAL",MySqlDbType.VarChar){Value=snval},
        //                        new MySqlParameter("C_SNVALEND",MySqlDbType.VarChar){Value=snvalend}
        //                        });
        //            }
        //            #endregion

        //            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

        //        }
        //        ///// <summary>
        //        ///// 成品出库2013-09-02
        //        ///// </summary>
        //        ///// <param name="wwip"></param>
        //        ///// <returns></returns>
        //        //public string ProductOut_1(List<tWarehouseWipTrackingTable> wwip)
        //        //{
        //        //    string err = string.Empty;
        //        //    foreach (tWarehouseWipTrackingTable itemWip in wwip)
        //        //    {
        //        //        err = BLL.BllMsSqllib.Instance.ProductOut(itemWip);
        //        //    }
        //        //    return err;
        //        //}
        //        /// <summary>
        //        /// 统计出库数量
        //        /// </summary>
        //        /// <param name="sapcode">SAP单号</param>
        //        /// <returns></returns>
        //        public System.Data.DataSet CountProductOutQTY(string sapcode)
        //        {
        //            MySqlCommand cmd = new MySqlCommand();
        //            cmd.CommandText = " select partnumber,lotout,COUNT(1) qty from SFCR.Z_WHS_TRACKING where lotout=("
        //                + "select  sfclotcode from SFCR.T_OUTPUT_LOT_RECORD_NEW where saplotcode=@sapcode and sfclotcode<>'NA' and rownum=1)  group by lotout,partnumber";
        //            cmd.Parameters.Add("sapcode", MySqlDbType.VarChar).Value = sapcode;
        //            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //        }
    }
}
