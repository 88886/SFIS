using System;
using System.Collections.Generic;
using System.Text;
using GenericProvider;
using GenericUtil;

namespace BLL
{
    public partial class ExcelToDb
    {

        public ExcelToDb()
        {
        }

        BLL.Db_Procedure mPro = new Db_Procedure();
        /// <summary>
        /// 添加料站表数据
        /// </summary>
        /// <param name="kpmaster">料站表头数据</param>
        /// <param name="lskpdetalt">料站表身数据列表</param>
        public void InsertMaterTable(string kpmaster, string lskpdetalt)
        {
            string outmasteridTemp;
            string Err;
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(kpmaster);
            IList<IDictionary<string, object>> Lsmst = MapListConverter.JsonToListDictionary(lskpdetalt);

            mPro.PRO_INSERT_SMT_KP_MASTER(mst["LINEID"].ToString(), mst["USERID"].ToString(), mst["PARTNUMBER"].ToString(), mst["MODELNAME"].ToString(), mst["BOMVER"].ToString(), mst["PCBSIDE"].ToString(), mst["RESERVE1"].ToString(), "0", out outmasteridTemp, out Err);
 
            if (Err!="OK")
                throw new Exception(Err + " 料站表头数据增加失败,请检查");
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);         
            foreach (Dictionary<string, object> item in Lsmst)
            {
                item["MASTERID"] = outmasteridTemp;
                try
                {               
                   dp.AddData("SFCR.T_SMT_KP_DETALT", item);
                }
                catch (Exception ex)
                {                   
                    throw new Exception(ex.Message + " 料站表身数据增加失败,请检查["+item["STATIONNO"]+"]");
                }      
            }
        }

        /// <summary>
        /// 获取指定产品所有备料的机器列表
        /// </summary>
        public List<string> GetMachineIdList(string partnumber)
        {
            List<string> lsmachine = new List<string>();

            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("partnumber", partnumber);
            System.Data.DataTable  _dt = dp.GetData("SFCR.T_SMT_KP_MASTER", "distinct lineId ", mst, out count).Tables[0];

            foreach (System.Data.DataRow dr in _dt.Rows)
            {
                lsmachine.Add(dr["lineId"].ToString());
            }
            return lsmachine;
        }

        /// <summary>
        /// 获取料站表头的一行信息
        /// </summary>
        /// <param name="partnumber">成品料号</param>
        /// <param name="machineId">机器编号</param>
        /// <param name="side">pcb面</param>
        /// <returns></returns>
        public System.Data.DataSet GetKpMasterInfo(string partnumber, string machineId, string side)
        {           
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("partnumber", partnumber);
            mst.Add("lineId", machineId);
            mst.Add("pcbside", side);
            System.Data.DataSet _dt = dp.GetData("SFCR.T_SMT_KP_MASTER", " masterId,bomver,lineId", mst, out count);             
            if (_dt != null && _dt.Tables[0].Rows.Count > 0)
            {
                if (_dt.Tables[0].Rows.Count > 1)
                {
                    //资料重复
                    throw new Exception("相同的资料存在多比,请联系管理员");
                }
                else
                {
                    return _dt;
                }
            }
            else
            {
                //没有对应的料站表
                throw new Exception(string.Format("成品料号:[{0}]的PCB [{1}]面 在设备:[{2}]上没有料站信息,请重建料站信息",
                    partnumber, side, machineId));
            }
        }    

        public System.Data.DataSet GetSmtKpDetaltByMasterIdNew(string masterId, string woId, out string Err)
        {
            System.Data.DataTable dtMaterial = new System.Data.DataTable();
            System.Data.DataSet _ds = new System.Data.DataSet();
            try
            {
               
                string fieldlist = "STATIONNO,KPNUMBER,KPDESC,KPDISTINCT,REPLACEGROUP,PRIORITYCLASS,LOCTION,RESERVE1,RESERVE";
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("MASTERID", masterId);
                System.Data.DataTable dt1 = dp.GetData("SFCR.T_SMT_KP_DETALT", fieldlist, mst, out count).Tables[0];
                dtMaterial = dt1.Clone();

                mst = new Dictionary<string, object>();
                mst.Add("WOID", woId);
                mst.Add("MASTERID", masterId);
                System.Data.DataTable dt2 = dp.GetData("SFCR.T_SMT_KP_DETALT_FORWO", fieldlist, mst, out count).Tables[0];

                foreach (System.Data.DataRow dr in dt1.Rows)
                {
                    dtMaterial.Rows.Add(dr["STATIONNO"].ToString(), dr["KPNUMBER"].ToString(), dr["KPDESC"].ToString(), dr["KPDISTINCT"].ToString(), dr["REPLACEGROUP"].ToString(), dr["PRIORITYCLASS"].ToString(), dr["LOCTION"].ToString(), dr["RESERVE1"].ToString(), dr["RESERVE"].ToString());
                }

                foreach (System.Data.DataRow dr in dt2.Rows)
                {
                    dtMaterial.Rows.Add(dr["STATIONNO"].ToString(), dr["KPNUMBER"].ToString(), dr["KPDESC"].ToString(), dr["KPDISTINCT"].ToString(), dr["REPLACEGROUP"].ToString(), dr["PRIORITYCLASS"].ToString(), dr["LOCTION"].ToString(), dr["RESERVE1"].ToString(), dr["RESERVE"].ToString());
                }     
                _ds.Tables.Add(dtMaterial);
                Err = "OK";
                return _ds;
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return null;
            }

        }
           

        public string DeleteSmtKPDetaltForWo(string woId, string masterId, string stationno, string kpnumber)
        {   
            try
            {
                    string table = "SFCR.T_SMT_KP_DETALT_FORWO";
                    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);                
                    IDictionary<string, object> parms = new Dictionary<string, object>();
                    parms.Add("WOID", woId);
                    parms.Add("STATIONNO", stationno);
                    parms.Add("MASTERID", masterId);
                    parms.Add("KPNUMBER", kpnumber);
                    dp.DeleteData(table, parms);            

                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
