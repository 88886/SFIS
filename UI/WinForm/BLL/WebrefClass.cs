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

namespace RefWebService_BLL
{
    //public class refWebtCustomerComplaint
    //{
    //    private static WebServices.tCustomerComplaint.tCustomerComplaint instance;

    //    public static WebServices.tCustomerComplaint.tCustomerComplaint Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //                instance =new WebServices.tCustomerComplaint.tCustomerComplaint();
    //            return instance;
    //        }
    //    }
    //    static refWebtCustomerComplaint()
    //    {
    //        instance =new WebServices.tCustomerComplaint.tCustomerComplaint();
    //    }
    //}
    public class refWebtFixtureInfo
    {
        private static WebServices.tFixtureInfo.tFixtureInfo instance;

        public static WebServices.tFixtureInfo.tFixtureInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tFixtureInfo.tFixtureInfo();
                return instance;
            }
        }
        static refWebtFixtureInfo()
        {
            instance = new WebServices.tFixtureInfo.tFixtureInfo();
        }
    }
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
    //public class refWebtStationNoInfo
    //{
    //    private static WebServices.tStationNoInfo.tStationNoInfo instance;

    //    public static WebServices.tStationNoInfo.tStationNoInfo Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //                instance = new WebServices.tStationNoInfo.tStationNoInfo();
    //            return instance;
    //        }
    //    }
    //    static refWebtStationNoInfo()
    //    {
    //        instance = new WebServices.tStationNoInfo.tStationNoInfo();
    //    }
    //}
    public class refWebtSmtKpNormalLog
    {
        private static WebServices.tSmtKpNormalLog .tSmtKpNormalLog instance;

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
    public class tCheckDataTestAte
    {
        private static WebServices.tCheckDataTestAte.tCheckDataTestAte instance;
        public static WebServices.tCheckDataTestAte.tCheckDataTestAte Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tCheckDataTestAte.tCheckDataTestAte();
                return instance;
            }
        }
        static tCheckDataTestAte()
        {
            instance = new WebServices.tCheckDataTestAte.tCheckDataTestAte();
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
    public class refWebtWarehouseWipTracking
    {
        private static WebServices.tWarehouseWipTracking.tWarehouseWipTracking instance;
        public static WebServices.tWarehouseWipTracking.tWarehouseWipTracking Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tWarehouseWipTracking.tWarehouseWipTracking();
                return instance;
            }
        }
        static refWebtWarehouseWipTracking()
        {
            instance = new WebServices.tWarehouseWipTracking.tWarehouseWipTracking();
        }
    }
	
	//QC 
    //public class refWebQcLotInfo
    //{
    //    private static WebServices.QcLotInfo.QcLotInfo instance;

    //    public static WebServices.QcLotInfo.QcLotInfo Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //                instance = new WebServices.QcLotInfo.QcLotInfo();
    //            return instance;
    //        }
    //    }
    //    static refWebQcLotInfo()
    //    {
    //        instance = new WebServices.QcLotInfo.QcLotInfo();
    //    }
    //}
    //public class refWebQcPalletInfo
    //{
    //    private static WebServices.QcPalletInfo.QcPalletInfo instance;

    //    public static WebServices.QcPalletInfo.QcPalletInfo Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //                instance = new WebServices.QcPalletInfo.QcPalletInfo();
    //            return instance;
    //        }
    //    }
    //    static refWebQcPalletInfo()
    //    {
    //        instance = new WebServices.QcPalletInfo.QcPalletInfo();
    //    }
    //}
    //public class refWebQcInspInfo
    //{
    //    private static WebServices.QcInspInfo.QcInspInfo instance;

    //    public static WebServices.QcInspInfo.QcInspInfo Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //                instance = new WebServices.QcInspInfo.QcInspInfo();
    //            return instance;
    //        }
    //    }
    //    static refWebQcInspInfo()
    //    {
    //        instance = new WebServices.QcInspInfo.QcInspInfo();
    //    }
    //}
    public class refWebSapConnector
    {
        private static WebServices.SapConnector.SapConnector instance;

        public static WebServices.SapConnector.SapConnector Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.SapConnector.SapConnector();
                return instance;
            }
        }
        static refWebSapConnector()
        {
            instance = new WebServices.SapConnector.SapConnector();
        }
    }
    public class refWebtStationRec
    {
        private static WebServices.tStationrecount.tStationrecount instance;

        public static WebServices.tStationrecount.tStationrecount Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tStationrecount.tStationrecount();
                return instance;
            }
        }
        static refWebtStationRec()
        {
            instance = new WebServices.tStationrecount.tStationrecount();
        }
    }
    public class refWebtCustomer
    {
        private static WebServices.tCustomer.tCustomer instance;

        public static WebServices.tCustomer.tCustomer Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tCustomer.tCustomer();
                return instance;
            }
        }
        static refWebtCustomer()
        {
            instance = new WebServices.tCustomer.tCustomer();
        }
    }
    public class refWebtReworkDetailInfo
    {
        private static WebServices.tReworkDetailInfo.tReworkDetailInfo instance;

        public static WebServices.tReworkDetailInfo.tReworkDetailInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tReworkDetailInfo.tReworkDetailInfo();
                return instance;
            }
        }
        static refWebtReworkDetailInfo()
        {
            instance = new WebServices.tReworkDetailInfo.tReworkDetailInfo();
        }
    }

    //public class refWebtQc_Rd
    //{
    //    private static WebServices.QD_RD_NEW.QD_RD_NEW instance;

    //    public static WebServices.QD_RD_NEW.QD_RD_NEW Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //                instance = new WebServices.QD_RD_NEW.QD_RD_NEW();
    //            return instance;
    //        }
    //    }
    //    static refWebtQc_Rd()
    //    {
    //        instance = new WebServices.QD_RD_NEW.QD_RD_NEW();
    //    }
    //}

    public class refwebtEditing
    {
        private static WebServices.tEditing.tEditing instance;

        public static  WebServices.tEditing.tEditing Instance
        {
            get
            {
                if (instance == null)
                    instance = new  WebServices.tEditing.tEditing();
                return instance;
            }
        }
        static refwebtEditing()
        {
            instance = new  WebServices.tEditing.tEditing();
        }
    }

    //public class refwebtMaterialsReceive
    //{
    //    private static WebServices.tMaterialsReceive.tMaterialsReceive instance;

    //    public static WebServices.tMaterialsReceive.tMaterialsReceive Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //                instance = new WebServices.tMaterialsReceive.tMaterialsReceive();
    //            return instance;
    //        }

    //    }
    //    static refwebtMaterialsReceive()
    //    {
    //        instance = new WebServices.tMaterialsReceive.tMaterialsReceive();
    //    }

    //}
    public class refwebtPrivliege
    {
        private static WebServices.tPrivliege.tPrivliege instance;

        public static WebServices.tPrivliege.tPrivliege Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tPrivliege.tPrivliege();
                return instance;
            }

        }
        static refwebtPrivliege()
        {
            instance = new WebServices.tPrivliege.tPrivliege();
        }

    }
    //public class refWebtPer_Num
    //{
    //    private static WebServices.Per_Num.Per_Num instance;

    //    public static WebServices.Per_Num.Per_Num Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //                instance = new WebServices.Per_Num.Per_Num();
    //            return instance;
    //        }
    //    }
    //    static refWebtPer_Num()
    //    {
    //        instance = new WebServices.Per_Num.Per_Num();
    //    }
    //}

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


    public class refWebtFQC
    {
        private static WebServices.Web_FQC.Web_FQC instance;

        public static WebServices.Web_FQC.Web_FQC Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.Web_FQC.Web_FQC();
                return instance;
            }
        }
        static refWebtFQC()
        {
            instance = new WebServices.Web_FQC.Web_FQC();
        }
    }
    public class refWebtB_SnRule_PartNumber
    {
        private static WebServices.tB_SnRule_PartNumber.tB_SnRule_PartNumber instance;

        public static WebServices.tB_SnRule_PartNumber.tB_SnRule_PartNumber Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tB_SnRule_PartNumber.tB_SnRule_PartNumber();
                return instance;
            }
        }
        static refWebtB_SnRule_PartNumber()
        {
            instance = new WebServices.tB_SnRule_PartNumber.tB_SnRule_PartNumber();
        }
    }
    public class refWebtB_SnRule_WO
    {
        private static WebServices.tB_SnRule_WO.tB_SNRULE_WO instance;

        public static WebServices.tB_SnRule_WO.tB_SNRULE_WO Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tB_SnRule_WO.tB_SNRULE_WO();
                return instance;
            }
        }
        static refWebtB_SnRule_WO()
        {
            instance = new WebServices.tB_SnRule_WO.tB_SNRULE_WO();
        }
    }

    public class refWebtZ_Whs_SAP_BackFlush
    {
        private static WebServices.tZ_Whs_SAP_BackFlush.tZ_Whs_SAP_BackFlush instance;

        public static WebServices.tZ_Whs_SAP_BackFlush.tZ_Whs_SAP_BackFlush Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tZ_Whs_SAP_BackFlush.tZ_Whs_SAP_BackFlush();
                return instance;
            }
        }
        static refWebtZ_Whs_SAP_BackFlush()
        {
            instance = new WebServices.tZ_Whs_SAP_BackFlush.tZ_Whs_SAP_BackFlush();
        }
    }


    public class refWEB_T_plan_list
    {
        private static WebServices.WEB_T_plan_list.WEB_T_plan_list instance;

        public static WebServices.WEB_T_plan_list.WEB_T_plan_list Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.WEB_T_plan_list.WEB_T_plan_list();
                return instance;
            }
        }
        static refWEB_T_plan_list()
        {
            instance = new WebServices.WEB_T_plan_list.WEB_T_plan_list();
        }
    }


    public class refWEB_t_numtype_info
    {
        private static WebServices.Web_t_numtype_info.Web_t_numtype_info instance;

        public static WebServices.Web_t_numtype_info.Web_t_numtype_info Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.Web_t_numtype_info.Web_t_numtype_info();
                return instance;
            }
        }
        static refWEB_t_numtype_info()
        {
            instance = new WebServices.Web_t_numtype_info.Web_t_numtype_info();
        }
    }


    public class refWebt_woInfo
    {
        private static WebServices.Webt_woInfo.Webt_woInfo instance;

        public static WebServices.Webt_woInfo.Webt_woInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.Webt_woInfo.Webt_woInfo();
                return instance;
            }
        }
        static refWebt_woInfo()
        {
            instance = new WebServices.Webt_woInfo.Webt_woInfo();
        }
    }

    public class refWebtz_whs_move_store
    {
        private static WebServices.tz_whs_move_store.tz_whs_move_store instance;

        public static WebServices.tz_whs_move_store.tz_whs_move_store Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tz_whs_move_store.tz_whs_move_store();
                return instance;
            }
        }
        static refWebtz_whs_move_store()
        {
            instance = new WebServices.tz_whs_move_store.tz_whs_move_store();
        }
    }


    //public class refWebtz_whs_loction_info
    //{
    //    private static WebServices.tz_whs_loction_info.tz_whs_loction_info instance;

    //    public static WebServices.tz_whs_loction_info.tz_whs_loction_info Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //                instance = new WebServices.tz_whs_loction_info.tz_whs_loction_info();
    //            return instance;
    //        }
    //    }
    //    static refWebtz_whs_loction_info()
    //    {
    //        instance = new WebServices.tz_whs_loction_info.tz_whs_loction_info();
    //    }
    //}


    public class refWebT_DS_Out
    {
        private static WebServices.T_DS_Out.T_DS_Out instance;

        public static WebServices.T_DS_Out.T_DS_Out Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.T_DS_Out.T_DS_Out();
                return instance;
            }
        }
        static refWebT_DS_Out()
        {
            instance = new WebServices.T_DS_Out.T_DS_Out();
        }
    }
    public class refWebtR_Tr_Sn
    {
        private static WebServices.tR_Tr_Sn.tR_Tr_Sn instance;

        public static WebServices.tR_Tr_Sn.tR_Tr_Sn Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tR_Tr_Sn.tR_Tr_Sn();
                return instance;
            }
        }
        static refWebtR_Tr_Sn()
        {
            instance = new WebServices.tR_Tr_Sn.tR_Tr_Sn();
        }
    }
    public class refWebHistoryService
    {
        private static WebServices.HistoryServices.HistoryServices instance;

        public static WebServices.HistoryServices.HistoryServices Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.HistoryServices.HistoryServices();
                return instance;
            }
        }
        static refWebHistoryService()
        {
            instance = new WebServices.HistoryServices.HistoryServices();
        }
    }
    public class refWebt_Check_Timeout
    {
        private static WebServices.t_Check_Timeout.t_Check_Timeout instance;

        public static WebServices.t_Check_Timeout.t_Check_Timeout Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.t_Check_Timeout.t_Check_Timeout();
                return instance;
            }
        }
        static refWebt_Check_Timeout()
        {
            instance = new WebServices.t_Check_Timeout.t_Check_Timeout();
        }
    }

    public class refWebt_wo_Info_Erp
    {
        private static WebServices.t_wo_Info_Erp.T_wo_Info_Erp instance;
        public static WebServices.t_wo_Info_Erp.T_wo_Info_Erp Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.t_wo_Info_Erp.T_wo_Info_Erp();
                return instance;
            }
        }
             static refWebt_wo_Info_Erp()
        {
            instance = new WebServices.t_wo_Info_Erp.T_wo_Info_Erp();
        }
    }
    public class refWebtVersion_Mark
    {
        private static WebServices.tVersion_Mark.tVersion_Mark instance;

        public static WebServices.tVersion_Mark.tVersion_Mark Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tVersion_Mark.tVersion_Mark();
                return instance;
            }
        }
        static refWebtVersion_Mark()
        {
            instance = new WebServices.tVersion_Mark.tVersion_Mark();
        }
    }
    public class refWebtPlan_Rate_Report
    {
        private static WebServices.tPlan_Rate_Report.tPlan_Rate_Report instance;

        public static WebServices.tPlan_Rate_Report.tPlan_Rate_Report Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tPlan_Rate_Report.tPlan_Rate_Report();
                return instance;
            }
        }
        static refWebtPlan_Rate_Report()
        {
            instance = new WebServices.tPlan_Rate_Report.tPlan_Rate_Report();
        }
    }
    public class refWebtSmtWoMerge  
    {
        private static WebServices.tSmtWoMerge.tSmtWoMerge instance;

        public static WebServices.tSmtWoMerge.tSmtWoMerge Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tSmtWoMerge.tSmtWoMerge();
                return instance;
            }
        }
        static refWebtSmtWoMerge()
        {
            instance = new WebServices.tSmtWoMerge.tSmtWoMerge();
        }
    }
    public class refWebtStationConfig
    {
        private static WebServices.tStationConfig.tStationConfig instance;

        public static WebServices.tStationConfig.tStationConfig Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tStationConfig.tStationConfig();
                return instance;
            }
        }
        static refWebtStationConfig()
        {
            instance = new WebServices.tStationConfig.tStationConfig();
        }
    }
    public class refWebtWOMaterial
    {
        private static WebServices.tT_WO_Material.tT_WO_Material instance;

        public static WebServices.tT_WO_Material.tT_WO_Material Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tT_WO_Material.tT_WO_Material();
                return instance;
            }
        }
        static refWebtWOMaterial()
        {
            instance = new WebServices.tT_WO_Material.tT_WO_Material();
        }
    }
    public class refWebProcedure
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
        static refWebProcedure()
        {
            instance = new WebServices.tPublicStoredproc.tPublicStoredproc();
        }
    }
    public class refWebtSnUnBind
    {
        private static WebServices.tSnUnBind.tSnUnBind instance;

        public static WebServices.tSnUnBind.tSnUnBind Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.tSnUnBind.tSnUnBind();
                return instance;
            }
        }
        static refWebtSnUnBind()
        {
            instance = new WebServices.tSnUnBind.tSnUnBind();
        }
    }
}
