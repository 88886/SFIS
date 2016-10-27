using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace BLL
{
    public partial class tMaterialsReceive
    {

        public tMaterialsReceive()
        {
        }

        //public void InsertMaterialsReceive(Entity.tMaterialsReceive mr)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "insert into SFCR.T_MATERIALS_RECEIVE_NEW(trsn,PO,kpnumber,qty,status,flag) values (@trsn,@PO,@kptnumber,@qty,@status,@flag)";
        //    cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 30).Value = mr.Trsn;
        //    cmd.Parameters.Add("PO", MySqlDbType.VarChar, 20).Value = mr.PO;
        //    cmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = mr.Kpnumber;
        //    cmd.Parameters.Add("status", MySqlDbType.Int32).Value = mr.Status;
        //    cmd.Parameters.Add("qty", MySqlDbType.Int32).Value = mr.Qty;
        //    cmd.Parameters.Add("flag", MySqlDbType.VarChar, 10).Value = mr.Flag;
        //    BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        //}

        //public DataSet GetMaterialsByPO(string materialpo)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "select trsn,PO,kpnumber,qty, ";
        //    cmd.CommandText += "case status when 0 then '待审核' when 1 then '已审核' when 2 then '审核失败' end status,";
        //    cmd.CommandText += "flag,recdate from SFCR.T_MATERIALS_RECEIVE_NEW where PO=@materialpo ";

        //    cmd.Parameters.Add("materialpo", MySqlDbType.VarChar, 20).Value = materialpo;
        //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //}

        //public DataSet GetMaterialsInfo(string queryvalue, string flag)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "select trsn,PO,kpnumber,qty,flag,recdate, ";
        //    cmd.CommandText += "case status when 0 then '待审核' when 1 then '审核ok' when 2 then '判退料' else '未定义' end status from SFCR.T_MATERIALS_RECEIVE_NEW ";

        //    if (flag == "0")
        //        cmd.CommandText += " where PO=@queryvalue";
        //    if (flag == "1")
        //        cmd.CommandText += " where kpnumber=@queryvalue";
        //    if (flag == "2")
        //        cmd.CommandText += " where trsn=@queryvalue";
        //    cmd.CommandText += " and status=1";

        //    cmd.Parameters.Add("queryvalue", MySqlDbType.VarChar, 20).Value = queryvalue;
        //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //}

        //public void UpdateMaterialStatus(string trsn, string flag)
        //{
        //    MySqlCommand cmd = new MySqlCommand();        
        //    cmd.CommandText = "update SFCR.T_MATERIALS_RECEIVE_NEW set status=@sstatus where trsn=@trsn";
        //    cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 15).Value = trsn;
        //    cmd.Parameters.Add("sstatus", MySqlDbType.VarChar, 15).Value = flag;
        //    BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        //}
        ////需再次确认Oracle 是否能识别语句  20131030  michael
        //public void UpdateMaterialStatusQty(string trsn, int qty)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "update SFCR.T_PART_STOREHOUSE_HAD set qty=@qty where trsn=@trsn;";
        //    cmd.CommandText += " update SFCR.T_MATERIALS_RECEIVE_NEW set qty=@qty,status=1 where trsn=@trsn";
        //    cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 15).Value = trsn;
        //    cmd.Parameters.Add("qty", MySqlDbType.Int32).Value = qty;
        //    BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        //}

        //public DataSet GetMatterialByTrsn(string trsn)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "select trsn,status from SFCR.T_MATERIALS_RECEIVE_NEW where trsn=@trsn";
        //    cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 15).Value = trsn;
        //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //}


    }
}
