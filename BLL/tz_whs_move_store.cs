using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BLL
{
    public class tz_whs_move_store
    {
        public tz_whs_move_store()
        {
        }
      /*  public string insertmove_store_id(Entity.tz_whs_move_storeTable insertmsi)
        {
            string Table = "z_whs_move_store";
            string Fileds = "MOVE_STORE_ID,PARTNUMBER,PRODUCTNAME,QTY,MOVE_STORE,IMMIGRATION_STORE,RECDATE,USERID";
            string sFileds = "@MOVE_ID,@PARTN,@PRODUCT,@QTY,@MOVE_OUT,@IM_STORE,SYSDATE(),@USER_ID";
            MySqlParameter[] SParam = new MySqlParameter[]
             {
                new MySqlParameter("MOVE_ID",MySqlDbType.VarChar){Value=insertmsi.MOVE_STORE_ID},            
                new MySqlParameter("PARTN",MySqlDbType.VarChar){Value=insertmsi.PARTNUMBER}, 
                new MySqlParameter("PRODUCT",MySqlDbType.VarChar){Value=insertmsi.PRODUCTNAME},  
                new MySqlParameter("QTY",MySqlDbType.Int32){Value=insertmsi.QTY},
                new MySqlParameter("MOVE_OUT",MySqlDbType.VarChar){Value=insertmsi.MOVE_STORE},
                new MySqlParameter("IM_STORE",MySqlDbType.VarChar){Value=insertmsi.IMMIGRATION_STORE},
                new MySqlParameter("USER_ID",MySqlDbType.VarChar){Value=insertmsi.USERID}

            };
            return BLL.BllMsSqllib.Instance.PublicInsertExecteNonQuery(Table, Fileds, sFileds, SParam);

        }
        public string Sel_MOVEWH_ID()
        {
            string C_SEQ = string.Empty;
            string PRGNAME = "SEQ_MOVEWH_ID";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select current_value from sfcb.sequence where name=@PRGNAME";
            cmd.Parameters.Add("PRGNAME", MySqlDbType.VarChar).Value = PRGNAME;
            DataTable dtSEQ = BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];
            C_SEQ =  dtSEQ.Rows[0][0].ToString();
            cmd = new MySqlCommand();
            cmd.CommandText = "update sfcb.sequence set current_value=current_value+increment where name=@PRGNAME";
            cmd.Parameters.Add("PRGNAME", MySqlDbType.VarChar).Value = PRGNAME;
            BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

            return DateTime.Now.ToString("yyyyMMdd")+C_SEQ;

            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "SELECT  SEQ_MOVEWH_ID.NEXTVAL FROM DUAL";
            //return DateTime.Now.ToString("yyyyMMdd") + BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 综合查询
        /// </summary>
        /// <param name="wt"></param>
        /// <returns></returns>
        public DataSet Getmove_store_id(string MOVE_STORE_ID, string PARTNUMBER)
        {
            MySqlCommand cmd = new MySqlCommand();
            if (!string.IsNullOrEmpty(PARTNUMBER))
            {             
                cmd.CommandText = "SELECT a.move_store_id 移库单号, a.partnumber 成品料号,a.qty 移库数量, a.move_store 移出库位, a.immigration_store 移入库位,";
                cmd.CommandText += " a.recdate 单号申请时间, a.userid 单号申请人 FROM z_whs_move_store A WHERE move_store_id=@MOVE_ID or PARTNUMBER=@PARTN ";
                cmd.Parameters.Add("MOVE_ID", MySqlDbType.VarChar).Value = MOVE_STORE_ID;
                cmd.Parameters.Add("PARTN", MySqlDbType.VarChar).Value = PARTNUMBER;
            }
            else
            {
                cmd.CommandText = "SELECT a.move_store_id , a.partnumber ,a.qty , a.move_store , a.immigration_store ,";
                cmd.CommandText += " '0'   FROM z_whs_move_store A WHERE move_store_id=@MOVE_ID and status is null   ";
                cmd.Parameters.Add("MOVE_ID", MySqlDbType.VarChar).Value = MOVE_STORE_ID;
            }
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

        }

        public string insertz_whs_detail(string esn, string userid, string lotcode)
        {
            string Table = "sfcr.z_whs_detail";
            string Fileds = "esn,userid,lotcode,sstatus,recdate";
            string sFileds = "@sn,@user_id,@lot_code,0,SYSDATE()";
            MySqlParameter[] SParam = new MySqlParameter[]
             {
                new MySqlParameter("sn",MySqlDbType.VarChar){Value=esn},            
                new MySqlParameter("user_id",MySqlDbType.VarChar){Value=userid},         
                new MySqlParameter("lot_code",MySqlDbType.VarChar){Value=lotcode}
            };
            return BLL.BllMsSqllib.Instance.PublicInsertExecteNonQuery(Table, Fileds, sFileds, SParam);

        }

        public string move_store_id_status(string move_store_id, string partnumber, string status)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                if (string.IsNullOrEmpty(partnumber))
                {
                    cmd.CommandText = "update z_whs_move_store set status=@statuss  where move_store_id=@move_id";
                    cmd.Parameters.Add("move_id", MySqlDbType.VarChar).Value = move_store_id;
                    cmd.Parameters.Add("statuss", MySqlDbType.VarChar).Value = status;
                }
                else
                {
                    cmd.CommandText = "update z_whs_move_store set status=@statuss  where move_store_id=@move_id and partnumber=@partn ";
                    cmd.Parameters.Add("move_id", MySqlDbType.VarChar).Value = move_store_id;
                    cmd.Parameters.Add("partn", MySqlDbType.VarChar).Value = partnumber;
                    cmd.Parameters.Add("statuss", MySqlDbType.VarChar).Value = status;
                }
                BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        /// <summary>
        /// 获取移库详细信息
        /// </summary>
        /// <param name="wt"></param>
        /// <returns></returns>
        public DataSet Getmove_store_info(string MOVE_STORE_ID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = " SELECT c.move_store_id 移库单号,a.partnumber 成品料号,a.esn 栈板号码,c.qty 料号移库总数量,c.move_store 移出库位,c.immigration_store 移入库位,  ";
            cmd.CommandText += " a.userid 移库人,a.recdate 移库时间  FROM sfcr.z_Whs_Detail a,z_whs_move_store c where ";
            cmd.CommandText += "  a.partnumber=c.partnumber and a.lotcode=c.move_store_id and c.move_store_id=@MOVE_ID order by c.partnumber,a.esn ";
            cmd.Parameters.Add("MOVE_ID", MySqlDbType.VarChar).Value = MOVE_STORE_ID;
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

        }

        public DataSet Getstore_info(string lotcode)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "  select a.lotcode,b.partnumber,b.palletnumber,c.move_store,c.immigration_store,count(b.esn) sum_qty,'0' qty ";
            cmd.CommandText += " from sfcr.z_Whs_Detail a,sfcr.z_whs_tracking b,z_whs_move_store c ";
            cmd.CommandText += " where a.esn=b.palletnumber and b.status='99' and a.lotcode=@lot_code  and a.lotcode=c.move_store_id and b.partnumber=c.partnumber  ";
            cmd.CommandText += "  group by a.lotcode,b.partnumber,b.palletnumber,c.immigration_store,c.move_store  ";
            cmd.Parameters.Add("lot_code", MySqlDbType.VarChar).Value = lotcode;
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

        }

        public DataSet Getstorebymoveid(string move_store_id, string immigration_store)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = " select * from z_whs_move_store a where a.immigration_store=@im_store  and a.move_store_id=@move_id ";
            cmd.Parameters.Add("move_id", MySqlDbType.VarChar).Value = move_store_id;
            cmd.Parameters.Add("im_store", MySqlDbType.VarChar).Value = immigration_store;
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

        }
        public DataSet Getmove_store_qty(string MOVE_STORE_ID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT a.move_store_id 移库单号, a.partnumber 成品料号,a.qty 移库数量,";
            cmd.CommandText += " '0' 数量  FROM z_whs_move_store A WHERE move_store_id=@MOVE_ID and status ='W'   ";
            cmd.Parameters.Add("MOVE_ID", MySqlDbType.VarChar).Value = MOVE_STORE_ID;
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }
        public DataSet Get_Z_WHS_MOVE_STORE()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = @"SELECT ROWID,MOVE_STORE_ID,PARTNUMBER,PRODUCTNAME,QTY,MOVE_STORE,IMMIGRATION_STORE,RECDATE,USERID,STATUS,UPLOAD_FLAG,UPLOAD_DATE FROM SFCB.Z_WHS_MOVE_STORE
                              WHERE STATUS='C' AND UPLOAD_FLAG='N'";
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }
        public string Update_Z_WHS_MOVE_STORES(string Rowid)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "UPDATE SFCB.Z_WHS_MOVE_STORE SET UPLOAD_FLAG=@UPLOADFLAG,UPLOAD_DATE=SYSDATE() WHERE ROWID=@sROWID";
                cmd.Parameters.AddRange(new MySqlParameter[]
                {
                new MySqlParameter("UPLOADFLAG",MySqlDbType.VarChar){Value="Y"},
                new MySqlParameter("sROWID",MySqlDbType.VarChar){Value=Rowid}
                });
                 BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                 return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }*/
    }
}
