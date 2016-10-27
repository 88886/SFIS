using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class tZ_WHS_SAP_BACKFLUSH
    {
        public tZ_WHS_SAP_BACKFLUSH()
        {
        }

      /*  public string Insert_Z_Whs_SAP_BackFlush(Entity.Z_WHS_SAP_BACKFLUSHTable zhsb)
        {
            string Table = " SFCR.Z_WHS_SAP_BACKFLUSH ";
            string Fileds = " WOID,PARTNUMBER,PRODUCTNAME,LOTIN,LOTIN_QTY,LOTOUT,LOTOUT_QTY,PLANT,MOVE_TYPE,UPLOAD_FLAG,UPLOAD_DATE ";
            string sFileds = " @sMO,@PartNo,@Porduct,@LTIN,@LTQTY,@LTOUT,@LTOUTQTY,@sPlan,@sMove,@sUpFlag,SYSDATE() ";
            MySqlParameter[] SParam = new MySqlParameter[]
            {
                new MySqlParameter("sMO",MySqlDbType.VarChar){Value=zhsb.WOID},            
                new MySqlParameter("PartNo",MySqlDbType.VarChar){Value=zhsb.PARTNUMBER},         
                new MySqlParameter("Porduct",MySqlDbType.VarChar){Value=zhsb.PRODUCTNAME},
                new MySqlParameter("LTIN",MySqlDbType.VarChar){Value=string.IsNullOrEmpty(zhsb.LOTIN)?"NA":zhsb.LOTIN},
                new MySqlParameter("LTQTY",MySqlDbType.Int32){Value=zhsb.LOTIN_QTY},
                new MySqlParameter("LTOUT",MySqlDbType.VarChar){Value=string.IsNullOrEmpty(zhsb.LOTOUT)?"NA":zhsb.LOTOUT},
                new MySqlParameter("LTOUTQTY",MySqlDbType.Int32){Value=zhsb.LOTOUT_QTY},
                new MySqlParameter("sPlan",MySqlDbType.VarChar){Value=zhsb.PLANT},
                new MySqlParameter("sMove",MySqlDbType.VarChar){Value=zhsb.MOVE_TYPE},
                new MySqlParameter("sUpFlag",MySqlDbType.VarChar){Value=string.IsNullOrEmpty(zhsb.UPLOAD_FLAG)?"N":zhsb.UPLOAD_FLAG}         

            };
            return BLL.BllMsSqllib.Instance.PublicInsertExecteNonQuery(Table, Fileds, sFileds, SParam);

        }
        public string Update_Z_Whs_SAP_BackFlush_Flag(string sRowid,string sFlag)
        {
            string Table = "SFCR.Z_WHS_SAP_BACKFLUSH";
            string Fileds = "UPLOAD_FLAG=@sUpFlag,UPLOAD_DATE=SYSDATE()";
            string sFileds = "rowid=@srod";
            MySqlParameter[] SParam = new MySqlParameter[]
            {
                new MySqlParameter("srod",MySqlDbType.VarChar){Value=sRowid},             
                new MySqlParameter("sUpFlag",MySqlDbType.VarChar){Value=string.IsNullOrEmpty(sFlag)?"N":sFlag}         

            };
            return BLL.BllMsSqllib.Instance.PublicUpdateExecteNonQuery(Table, Fileds, sFileds, SParam);

        }
        public System.Data.DataSet GetUpload_whs_Sap_Info(string MOVE_TYPE)
        {
            string Fileds = "ROWID,WOID,PARTNUMBER,PRODUCTNAME,LOTIN,LOTIN_QTY,LOTOUT,LOTOUT_QTY,PLANT,MOVE_TYPE,UPLOAD_FLAG";
            string Table = "SFCR.Z_WHS_SAP_BACKFLUSH";
            string Filters = "UPLOAD_FLAG=@sFLAG AND MOVE_TYPE=@sTYPE AND RECDATE>SYSDATE()-7 ";
             MySqlParameter[] SParam = new MySqlParameter[]
            {
                new MySqlParameter("sFLAG",MySqlDbType.VarChar){Value="N"},
                new MySqlParameter("sTYPE",MySqlDbType.VarChar){Value=MOVE_TYPE}           
            };

             return BLL.BllMsSqllib.Instance.TablePublicSelect(Table, Fileds, Filters, SParam, null);  
        }

        public System.Data.DataSet GetUpload_whs_Sap_Info_No(string MOVE_TYPE, string DocNumber,string Colnum)
        {
            string Fileds = "ROWID,WOID,PARTNUMBER,PRODUCTNAME,LOTIN,LOTIN_QTY,LOTOUT,LOTOUT_QTY,PLANT,MOVE_TYPE,UPLOAD_FLAG";
            string Table = "SFCR.Z_WHS_SAP_BACKFLUSH";
            string Filters =string.Format( " {0}=@sData ",Colnum);
            MySqlParameter[] SParam = new MySqlParameter[]
            {
                new MySqlParameter("sData",MySqlDbType.VarChar){Value=DocNumber}       
            };

            return BLL.BllMsSqllib.Instance.TablePublicSelect(Table, Fileds, Filters, SParam, null);  
        }
        */
    }
}
