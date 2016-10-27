using System;
using System.Collections.Generic;
using System.Text;
using WebServices;
using System.IO;
using System.Net;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Web;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.Xml;

namespace SFIS_PRINT_SYSTEM.BLL
{
    public class refWebtWsInfo
    {
        private static WebServices.tWsInfo.tWsInfo instance;
        public static WebServices.tWsInfo.tWsInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tWsInfo.tWsInfo();
                return instance;
            }
        }
        static refWebtWsInfo()
        {
            instance = new WebServices.tWsInfo.tWsInfo();
        }
    }
    public class refWebtWoInfo
    {
        private static WebServices.tWoInfo.tWoInfo instance;

        public static WebServices.tWoInfo.tWoInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tWoInfo.tWoInfo();
                return instance;
            }
        }
        static refWebtWoInfo()
        {
            instance = new WebServices.tWoInfo.tWoInfo();
        }
    }
    public class refWebtWoBomInfo
    {
        private static WebServices.tWoBomInfo.tWoBomInfo instance;

        public static WebServices.tWoBomInfo.tWoBomInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tWoBomInfo.tWoBomInfo();
                return instance;
            }
        }
        static refWebtWoBomInfo()
        {
            instance = new WebServices.tWoBomInfo.tWoBomInfo();
        }
    }
    public class refWebtUserInfo
    {
        private static WebServices.tUserInfo.tUserInfo instance;

        public static WebServices.tUserInfo.tUserInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tUserInfo.tUserInfo();
                return instance;
            }
        }
        static refWebtUserInfo()
        {
            instance = new WebServices.tUserInfo.tUserInfo();
        }
    }
  
    public class refWebtSmtKpNormalLog
    {
        private static WebServices.tSmtKpNormalLog.tSmtKpNormalLog instance;

        public static WebServices.tSmtKpNormalLog.tSmtKpNormalLog Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tSmtKpNormalLog.tSmtKpNormalLog();
                return instance;
            }
        }
        static refWebtSmtKpNormalLog()
        {
            instance = new WebServices.tSmtKpNormalLog.tSmtKpNormalLog();
        }
    }
    public class refWebtSmtKpMonitor
    {
        private static WebServices.tSmtKpMonitor.tSmtKpMonitor instance;

        public static WebServices.tSmtKpMonitor.tSmtKpMonitor Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tSmtKpMonitor.tSmtKpMonitor();
                return instance;
            }
        }
        static refWebtSmtKpMonitor()
        {
            instance = new WebServices.tSmtKpMonitor.tSmtKpMonitor();
        }
    }
    public class refWebtRoleInfo
    {
        private static WebServices.tRoleInfo.tRoleInfo instance;

        public static WebServices.tRoleInfo.tRoleInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tRoleInfo.tRoleInfo();
                return instance;
            }
        }
        static refWebtRoleInfo()
        {
            instance = new WebServices.tRoleInfo.tRoleInfo();
        }
    }
    public class refWebtProduct
    {
        private static WebServices.tProduct.tProduct instance;
        public static WebServices.tProduct.tProduct Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tProduct.tProduct();
                return instance;
            }
        }
        static refWebtProduct()
        {
            instance = new WebServices.tProduct.tProduct();
        }
    }
    public class refWebtPartBlocked
    {
        private static WebServices.tPartBlocked.tPartBlocked instance;

        public static WebServices.tPartBlocked.tPartBlocked Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tPartBlocked.tPartBlocked();
                return instance;
            }
        }
        static refWebtPartBlocked()
        {
            instance = new WebServices.tPartBlocked.tPartBlocked();
        }
    }
    public class refWebtMachineInfo
    {
        private static WebServices.tMachineInfo.tMachineInfo instance;

        public static WebServices.tMachineInfo.tMachineInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tMachineInfo.tMachineInfo();
                return instance;
            }
        }
        static refWebtMachineInfo()
        {
            instance = new WebServices.tMachineInfo.tMachineInfo();
        }
    }
    public class refWebtLineInfo
    {
        private static WebServices.tLineInfo.tLineInfo instance;

        public static WebServices.tLineInfo.tLineInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tLineInfo.tLineInfo();
                return instance;
            }
        }
        static refWebtLineInfo()
        {
            instance = new WebServices.tLineInfo.tLineInfo();
        }
    }
    public class refWebtFacInfo
    {
        private static WebServices.tFacInfo.tFacInfo instance;

        public static WebServices.tFacInfo.tFacInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tFacInfo.tFacInfo();
                return instance;
            }
        }
        static refWebtFacInfo()
        {
            instance = new WebServices.tFacInfo.tFacInfo();
        }
    }
    public class refWebtDeptInfo
    {
        private static WebServices.tDeptInfo.tDeptInfo instance;

        public static WebServices.tDeptInfo.tDeptInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tDeptInfo.tDeptInfo();
                return instance;
            }
        }
        static refWebtDeptInfo()
        {
            instance = new WebServices.tDeptInfo.tDeptInfo();
        }
    }
    public class refWebtCraftInfo
    {
        private static WebServices.tCraftInfo.tCraftInfo instance;

        public static WebServices.tCraftInfo.tCraftInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tCraftInfo.tCraftInfo();
                return instance;
            }
        }
        static refWebtCraftInfo()
        {
            instance = new WebServices.tCraftInfo.tCraftInfo();
        }
    }
    public class refWebRecodeSystemLog
    {
        private static WebServices.RecodeSystemLog.RecodeSystemLog instance;

        public static WebServices.RecodeSystemLog.RecodeSystemLog Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.RecodeSystemLog.RecodeSystemLog();
                return instance;
            }
        }
        static refWebRecodeSystemLog()
        {
            instance = new WebServices.RecodeSystemLog.RecodeSystemLog();
        }
    }
    public class refWebExecuteSqlCmd
    {

        private static WebServices.ExecuteSqlCmd.ExecuteSqlCmd instance;

        public static WebServices.ExecuteSqlCmd.ExecuteSqlCmd Instance
        {
            get
            {

                if (instance == null)
                    instance = new WebServices.ExecuteSqlCmd.ExecuteSqlCmd();
                return instance;
            }
        }
        static refWebExecuteSqlCmd()
        {
            instance = new WebServices.ExecuteSqlCmd.ExecuteSqlCmd();
        }
    }
    public class refWebExcelToDb
    {
        private static WebServices.ExcelToDb.ExcelToDb instance;

        public static WebServices.ExcelToDb.ExcelToDb Instance
        {
            get
            {

                if (instance == null)
                    instance = new WebServices.ExcelToDb.ExcelToDb();
                return instance;
            }
        }
        static refWebExcelToDb()
        {
            instance = new WebServices.ExcelToDb.ExcelToDb();
        }
    }
    public class refWebtPartMap
    {
        private static WebServices.tPartMap.tPartMap instance;

        public static WebServices.tPartMap.tPartMap Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tPartMap.tPartMap();
                return instance;
            }
        }
        static refWebtPartMap()
        {
            instance = new WebServices.tPartMap.tPartMap();
        }
    }
    public class refWebtRouteInfo
    {
        private static WebServices.tRouteInfo.tRouteInfo instance;

        public static WebServices.tRouteInfo.tRouteInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tRouteInfo.tRouteInfo();
                return instance;
            }
        }
        static refWebtRouteInfo()
        {
            instance = new WebServices.tRouteInfo.tRouteInfo();
        }
    }
    public class refWebtVenderCode
    {
        private static WebServices.tVenderCode.tVenderCode instance;

        public static WebServices.tVenderCode.tVenderCode Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tVenderCode.tVenderCode();
                return instance;
            }
        }
        static refWebtVenderCode()
        {
            instance = new WebServices.tVenderCode.tVenderCode();
        }
    }
    public class refWebtStorehouseManage
    {
        private static WebServices.tStorehouseManage.tStorehouseManage instance;

        public static WebServices.tStorehouseManage.tStorehouseManage Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tStorehouseManage.tStorehouseManage();
                return instance;
            }
        }
        static refWebtStorehouseManage()
        {
            instance = new WebServices.tStorehouseManage.tStorehouseManage();
        }
    }
    public class refWebtPartStorehousehad
    {
        private static WebServices.tPartStorehousehad.tPartStorehousehad instance;

        public static WebServices.tPartStorehousehad.tPartStorehousehad Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tPartStorehousehad.tPartStorehousehad();
                return instance;
            }
        }
        static refWebtPartStorehousehad()
        {
            instance = new WebServices.tPartStorehousehad.tPartStorehousehad();
        }
    }
    public class refWebVenderInfo
    {
        private static WebServices.tVenderInfo.tVenderInfo instance;

        public static WebServices.tVenderInfo.tVenderInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tVenderInfo.tVenderInfo();
                return instance;
            }
        }
        static refWebVenderInfo()
        {
            instance = new WebServices.tVenderInfo.tVenderInfo();
        }
    }
    public class refWebSmtKpMaster
    {
        private static WebServices.tSmtKpMaster.tSmtKpMaster instance;

        public static WebServices.tSmtKpMaster.tSmtKpMaster Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tSmtKpMaster.tSmtKpMaster();
                return instance;
            }
        }
        static refWebSmtKpMaster()
        {
            instance = new WebServices.tSmtKpMaster.tSmtKpMaster();
        }
    }
    public class refWebSmtIO
    {
        private static WebServices.tSmtIO.tSmtIO instance;

        public static WebServices.tSmtIO.tSmtIO Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tSmtIO.tSmtIO();
                return instance;
            }
        }
        static refWebSmtIO()
        {
            instance = new WebServices.tSmtIO.tSmtIO();
        }
    }
    public class refWebtMaterialPreparation
    {
        private static WebServices.tMaterialPreparation.tMaterialPreparation instance;

        public static WebServices.tMaterialPreparation.tMaterialPreparation Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tMaterialPreparation.tMaterialPreparation();
                return instance;
            }
        }
        static refWebtMaterialPreparation()
        {
            instance = new WebServices.tMaterialPreparation.tMaterialPreparation();
        }
    }
    public class refWebtGetServersTime
    {
        private static WebServices.tGetServersTime.tGetServersTime instance;

        public static WebServices.tGetServersTime.tGetServersTime Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tGetServersTime.tGetServersTime();
                return instance;
            }
        }
        static refWebtGetServersTime()
        {
            instance = new WebServices.tGetServersTime.tGetServersTime();
        }
    }
    //public class refWebtWorkFunctionInfo
    //{
    //    private static WebServices.tWorkFunctionInfo.tWorkFunctionInfo instance;

    //    public static WebServices.tWorkFunctionInfo.tWorkFunctionInfo Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //                instance = new WebServices.tWorkFunctionInfo.tWorkFunctionInfo();
    //            return instance;
    //        }
    //    }
    //    static refWebtWorkFunctionInfo()
    //    {
    //        instance = new WebServices.tWorkFunctionInfo.tWorkFunctionInfo();
    //    }
    //}

    public class refWebtErrorCode
    {
        private static WebServices.tEerrorCode.tEerrorCode instance;

        public static WebServices.tEerrorCode.tEerrorCode Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tEerrorCode.tEerrorCode();
                return instance;
            }
        }
        static refWebtErrorCode()
        {
            instance = new WebServices.tEerrorCode.tEerrorCode();
        }
    }
    public class refWebtReasonCode
    {
        private static WebServices.tReasonCode.tReasonCode instance;

        public static WebServices.tReasonCode.tReasonCode Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tReasonCode.tReasonCode();
                return instance;
            }
        }
        static refWebtReasonCode()
        {
            instance = new WebServices.tReasonCode.tReasonCode();
        }
    }
    public class refWebtPackParameters
    {
        private static WebServices.tPackParameters.tPackParameters instance;

        public static WebServices.tPackParameters.tPackParameters Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tPackParameters.tPackParameters();
                return instance;
            }
        }
        static refWebtPackParameters()
        {
            instance = new WebServices.tPackParameters.tPackParameters();
        }
    }

    public class refWebtKeyPart
    {
        private static WebServices.tKeyPart.tKeyPart instance;

        public static WebServices.tKeyPart.tKeyPart Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tKeyPart.tKeyPart();
                return instance;
            }
        }
        static refWebtKeyPart()
        {
            instance = new WebServices.tKeyPart.tKeyPart();
        }
    }

    public class refWebtBomKeyPart
    {
        private static WebServices.tBomKeyPart.tBomKeyPart instance;

        public static WebServices.tBomKeyPart.tBomKeyPart Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tBomKeyPart.tBomKeyPart();
                return instance;
            }
        }
        static refWebtBomKeyPart()
        {
            instance = new WebServices.tBomKeyPart.tBomKeyPart();
        }
    }

    public class refWebtPartKeyParts
    {
        private static WebServices.tPartKeyParts.tPartKeyParts instance;

        public static WebServices.tPartKeyParts.tPartKeyParts Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tPartKeyParts.tPartKeyParts();
                return instance;
            }
        }
        static refWebtPartKeyParts()
        {
            instance = new WebServices.tPartKeyParts.tPartKeyParts();
        }
    }

    public class refWebtWipTracking
    {
        private static WebServices.tWipTracking.tWipTracking instance;

        public static WebServices.tWipTracking.tWipTracking Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tWipTracking.tWipTracking();
                return instance;
            }
        }
        static refWebtWipTracking()
        {
            instance = new WebServices.tWipTracking.tWipTracking();
        }
    }

    public class refWebtWipDetail
    {
        private static WebServices.tWipDetail.tWipDetail instance;

        public static WebServices.tWipDetail.tWipDetail Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tWipDetail.tWipDetail();
                return instance;
            }
        }
        static refWebtWipDetail()
        {
            instance = new WebServices.tWipDetail.tWipDetail();
        }
    }

    public class refWebtWipKeyPart
    {
        private static WebServices.tWipKeyPart.tWipKeyPart instance;

        public static WebServices.tWipKeyPart.tWipKeyPart Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tWipKeyPart.tWipKeyPart();
                return instance;
            }
        }
        static refWebtWipKeyPart()
        {
            instance = new WebServices.tWipKeyPart.tWipKeyPart();
        }
    }


    public class refWebtTargetPlan
    {
        private static WebServices.tTargetPlan.tTargetPlan instance;

        public static WebServices.tTargetPlan.tTargetPlan Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tTargetPlan.tTargetPlan();
                return instance;
            }
        }
        static refWebtTargetPlan()
        {
            instance = new WebServices.tTargetPlan.tTargetPlan();
        }
    }
    public class refWebProPublicStoredproc
    {
        private static WebServices.tPublicStoredproc.tPublicStoredproc instance;
        public static WebServices.tPublicStoredproc.tPublicStoredproc Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tPublicStoredproc.tPublicStoredproc();
                return instance;
            }
        }
        static refWebProPublicStoredproc()
        {
            instance = new WebServices.tPublicStoredproc.tPublicStoredproc();
        }
    }
    public class refWebRepairInfo
    {
        private static WebServices.tRepairInfo.tRepairInfo instance;
        public static WebServices.tRepairInfo.tRepairInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tRepairInfo.tRepairInfo();
                return instance;
            }
        }
        static refWebRepairInfo()
        {
            instance = new WebServices.tRepairInfo.tRepairInfo();
        }
    }
    public class refWebCheck_Version
    {
        private static WebServices.Check_Version.Check_Version instance;
        public static WebServices.Check_Version.Check_Version Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.Check_Version.Check_Version();
                return instance;
            }
        }
        static refWebCheck_Version()
        {
            instance = new WebServices.Check_Version.Check_Version();
        }
    }
    public class refWebtPalletInfo
    {
        private static WebServices.tPalletInfo.tPalletInfo instance;
        public static WebServices.tPalletInfo.tPalletInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tPalletInfo.tPalletInfo();
                return instance;
            }
        }
        static refWebtPalletInfo()
        {
            instance = new WebServices.tPalletInfo.tPalletInfo();
        }
    }
    public class refWebtPublicStoredproc
    {
        private static WebServices.tPublicStoredproc.tPublicStoredproc instance;
        public static WebServices.tPublicStoredproc.tPublicStoredproc Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tPublicStoredproc.tPublicStoredproc();
                return instance;
            }
        }
        static refWebtPublicStoredproc()
        {
            instance = new WebServices.tPublicStoredproc.tPublicStoredproc();
        }
    }
}
