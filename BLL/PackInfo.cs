using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BLL
{
   public partial  class PackInfo
    {
       public PackInfo()
       {
       }

       BLL.tWipTracking twip = new tWipTracking();
       BLL.tPackParameters PackP = new tPackParameters();
       BLL.tWoInfo tWo = new tWoInfo();
       BLL.ProPublicStoredproc ProPubStor = new ProPublicStoredproc();
       BLL.tPalletInfo palt = new tPalletInfo();
       public string CatongPackInfo(Entity.PackInfoParameter Epp)
       {
           string Msg = "";
        

           DataTable dt = twip.GetQueryWip("esn", Epp.DATA).Tables[0];

           if (dt != null && dt.Rows.Count != 0)
           {
               Epp.PartNumber = dt.Rows[0][2].ToString();
               Epp.woId = dt.Rows[0][1].ToString();
               try
               {
                   Epp.VersionCode = tWo.GetALLWoInfoByWoinfo(Epp.woId).Tables[0].Rows[0]["pver"].ToString();

                   try
                   {
                       Epp.CartonCapacity = PackP.GetPackModelParameters(Epp.PartNumber, Epp.VersionCode).Tables[0].Rows[0]["CartonQty"].ToString();




                   }
                   catch
                   {
                       Msg = "Pack Parameters Not Set";
                   }
               }
               catch
               {
                   Msg = "woId not Found";
               }

           }
           else
           {
               Msg = "Esn Not Found";
           }



           return Msg;
       }


       public DataSet GetCartonPackInfo(Entity.PackInfoParameter Epp,int Flag)
       {
           if (Flag == 0)  //0 获取ESNwip
           {
              return twip.GetQueryWip("esn", Epp.ESN);
           }
           if (Flag == 1) //1 获取工单信息
           {
               return tWo.GetALLWoInfoByWoinfo(Epp.woId);
           }
           if (Flag == 2) //2 获取包装容量
           {
               return PackP.GetPackModelParameters(Epp.PartNumber, Epp.VersionCode);
           }
           if (Flag == 3)
           {


           }



           return null;
       }

       public string PackagingSstation(Entity.PackInfoParameter Epp,int Flag)
       {
           if (Flag == 0) //是否有carton未关闭
           {
               DataTable dt = palt.GetPalletInfo(new Entity.tPalletInfoTable
               {
                   Line = Epp.Line,
                   woId = Epp.woId,
                   PartNumber = Epp.PartNumber,
                   CloseFlag = 0,
                   Packtype = 1
               }).Tables[0];
               if (dt != null && dt.Rows.Count != 0)
               {
                   return dt.Rows[0]["palletnumber"].ToString();
               }

               if (dt.Rows.Count == 0)
               {
                 // ProPubStor.SP_GetPalletNumber
               }

           }

           if (Flag == 1)
           {
               DataTable dtchk = new DataTable("mydt");
               dtchk.Columns.Add("Code");
               dtchk.Columns.Add("Param");
               dtchk.Rows.Add("DATA", Epp.ESN);
               dtchk.Rows.Add("MYGROUP", Epp.MyGroup);
               //dtchk.Rows.Add("ROUTECODE", Epp.RouteCode);
               //dtchk.Rows.Add("ERRFLAG", Epp.ErrFlag);
               //dtchk.Rows.Add("LOCGROUP", Epp.LocStation);
               //dtchk.Rows.Add("NEXTSTATION", Epp.NextStation);
               //dtchk.Rows.Add("WO", Epp.woId);
               //dtchk.Rows.Add("ENDGROUP", Epp.EndGroup);
               return ProPubStor.SP_PublicStoredproc("pro_CheckRoute", dtchk);
           }
          if (Flag==2)
           {
               try
               {
                   string msg = palt.SP_TEST_CTN_PALT_TRAY(Epp.ESN, Epp.MyGroup, Epp.EMP, "NA", Epp.Line, Epp.CartonNo, Epp.MCartonNo, 1);
                   return msg;
               }
               catch (Exception ex)
               {
                   return "Call SP_TEST_CTN_PALT_TRAY Fail" + ex.Message;
               }
           }

          return "ERROR";
              
           
       }

    }
}
