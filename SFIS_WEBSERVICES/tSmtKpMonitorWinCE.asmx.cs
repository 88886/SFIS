using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Data;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;

namespace TestWeserver
{
    /// <summary>
    /// tSmtKpMonitorWinCE 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tSmtKpMonitorWinCE : System.Web.Services.WebService
    {

        [WebMethod]
        public void InsertSmtKpMonitor(Entity.tSmtKpMonitor kpmonitor, out string err)
        {
            BLL.tSmtKpMonitor.InsertSmtKpMonitor(kpmonitor, out err);
            //BLL.BllMsSqllib.Instance.MsSqlLib.InsertSmtKpMonitor(kpmonitor, out err);
        }

        [WebMethod]
        public byte[] GetSmtKpMonitor(string masterId, string woId, string stationno, int cdata)
        {
            return this.GetDataSetSurrogateZipBytes(BLL.tSmtKpMonitor.GetSmtKpMonitor(masterId, woId, stationno, cdata));
            //return BLL.BllMsSqllib.Instance.MsSqlLib.GetSmtKpMonitor(masterId, woId, stationno);
        }
        [WebMethod]
        public void EditSmtKpMonitorFlag(Guid kpmonitorId, BLL.tSmtKpMonitor.CDATA cdata)
        {
            BLL.tSmtKpMonitor.EditSmtKpMonitorFlag(kpmonitorId, cdata);
        }
        [WebMethod]
        public bool Check_MaterialScrap(string pn, string vc, string dc, string lc)
        {
            return BLL.tPartBlocked.Check_MaterialScrap(pn, vc, dc, lc);
        }

        [WebMethod]
        public byte[] GetSmtIO(string masterId, string woId)
        {
            return this.GetDataSetSurrogateZipBytes(BLL.tSmtIO.GetSmtIO(masterId, woId));
        }
        [WebMethod]
        public byte[] GetSmtIOMachineIdStatus(string machineId, BLL.tSmtIO.SmtIOStatus status)
        {
            return this.GetDataSetSurrogateZipBytes(BLL.tSmtIO.GetSmtIOMachineIdStatus(machineId, status));
        }

        [WebMethod]
        public void EditSmtIOStatus(string masterId, string woId, BLL.tSmtIO.SmtIOStatus status)
        {
            BLL.tSmtIO.EditSmtIOStatus(masterId, woId, status);
        }

        [WebMethod]
        public byte[] GetMaterialPreparation(string woId, string masterId)
        {
            return this.GetDataSetSurrogateZipBytes(BLL.tWoBomInfo.GetMaterialPreparation(woId, masterId));
        }

        [WebMethod]
        public bool ChkNormalLogStatus(string masterId, string woId, string pcbside, string machineId, string stationId)
        {
            return BLL.tSmtKpNormalLog.GetNormalLogStatus(masterId, woId, pcbside, machineId, stationId);
        }

        [WebMethod]
        public void InsertSmtKpNormalLogs(List<Entity.tSmtKpNormalLog> lskpnormallog, out string err)
        {
            BLL.tSmtKpNormalLog.InsertSmtKpNormalLogs(lskpnormallog, out err);
        }
        [WebMethod]
        public void InsertSmtKpNormalLog(Entity.tSmtKpNormalLog kpnormallog, out string err)
        {
            BLL.tSmtKpNormalLog.InsertSmtKpNormalLog(kpnormallog, out err);
        }

        [WebMethod]
        public byte[] CmdGetSmtIO(string sqlcmd)
        {
            return this.GetDataSetSurrogateZipBytes(BLL.BllMsSqllib.Instance.ExecuteDataSet(sqlcmd));
        }

        [WebMethod]
        public byte[] GetKpNumberInSEQ(string MasterId, string WoId, string kpnumber, string station)
        {
            return this.GetDataSetSurrogateZipBytes(BLL.tSmtKpMonitor.GetKpNumberInSEQ(MasterId, WoId, kpnumber, station));
        }
        [WebMethod]
        public byte[] CheckKpSupply(string MasterId, string WoId, string kpnumber, string cdata)
        {
            return this.GetDataSetSurrogateZipBytes(BLL.tSmtKpMonitor.CheckKpSupply(MasterId, WoId, kpnumber, cdata));
        }

        [WebMethod]
        public string GetServerDateTime()
        {
            return BLL.BllMsSqllib.Instance.GetServerDateTime();
        }
        [WebMethod]
        public byte[] GetSmtkPMonnitorStation(string sSEQ, string sMO)
        {
            return this.GetDataSetSurrogateZipBytes(BLL.tSmtKpMonitor.GetSmtkPMonnitorStation(sSEQ, sMO));
        }
        [WebMethod]
        public byte[] ChkStationInMonitor(Entity.tSmtKpMonitor skm)
        {
            return this.GetDataSetSurrogateZipBytes(BLL.tSmtKpMonitor.ChkStationInMonitor(skm));
        }
        [WebMethod]
        public void InsertSmtIoData(Entity.tSmtKpMonitor skm)
        {
            BLL.tSmtKpMonitor.InsertSmtIoData(skm);
        }

        [WebMethod]
        public byte[] RefreshMaterialMonitor()
        {
            return this.GetDataSetSurrogateZipBytes(BLL.tSmtKpMonitor.RefreshMaterialMonitor());
        }

        [WebMethod]
        public void UpdateSmtkPMonnitorCdata(Entity.tSmtKpMonitor skm)
        {
            BLL.tSmtKpMonitor.UpdateSmtkPMonnitorCdata(skm);
        }

        [WebMethod]
        public byte[] QueryMaterialInOutPut(Entity.tSmtKpMonitor skm)
        {
            return this.GetDataSetSurrogateZipBytes(BLL.tSmtKpMonitor.QueryMaterialInOutPut(skm));
        }
        [WebMethod]
        public byte[] QueryRollbackMaterial(string woId)
        {
            return this.GetDataSetSurrogateZipBytes(BLL.tSmtKpMonitor.QueryRollbackMaterial(woId));
        }
        [WebMethod]
        public void RollbackMaterial(Entity.tSmtKpMonitor skm)
        {
            BLL.tSmtKpMonitor.RollbackMaterial(skm);
        }
        [WebMethod]
        public byte[] GetQueliaoStationList(string sSEQ, string sMO)
        {
            return this.GetDataSetSurrogateZipBytes(BLL.tSmtKpMonitor.GetQueliaoStationList(sSEQ, sMO));
        }
        [WebMethod]
        public byte[] CheckSupplyMaterial(string sSEQ, string sMO, string sPartNo)
        {
            return this.GetDataSetSurrogateZipBytes( BLL.tSmtKpMonitor.CheckSupplyMaterial(sSEQ, sMO, sPartNo));
        }
        [WebMethod]
        public void UpdateSupplyMaterialStatus(Entity.tSmtKpMonitor skm)
        {
            BLL.tSmtKpMonitor.UpdateSupplyMaterialStatus(skm);
        }


        #region 添加压缩功能
        [WebMethod(Description = "返回 DataSetSurrogate 对象用 Binary 序列化并 Zip 压缩后的字节数组。")]
        public byte[] GetDataSetSurrogateZipBytes(DataSet dataSet)
        {
            XmlSerializer ser = new XmlSerializer(typeof(DataSet));
            MemoryStream ms = new MemoryStream();
            ser.Serialize(ms, dataSet);

            byte[] buffer = ms.ToArray();
            byte[] zipBuffer = Compress(buffer);
            return zipBuffer;
        }

        public byte[] Compress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                Stream zipStream = null;
                zipStream = new GZipStream(ms, CompressionMode.Compress, true);
                zipStream.Write(data, 0, data.Length);
                zipStream.Close();
                ms.Position = 0;
                byte[] compressed_data = new byte[ms.Length];
                ms.Read(compressed_data, 0, int.Parse(ms.Length.ToString()));
                return compressed_data;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}

