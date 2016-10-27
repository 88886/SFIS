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

    public class refWebRam
    {
        private static WebServices.OperateRam.OperateRam instance;

        public static WebServices.OperateRam.OperateRam Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebServices.OperateRam.OperateRam();
                return instance;
            }
        }
        static refWebRam()
        {
            instance = new WebServices.OperateRam.OperateRam();
        }
    }
}
