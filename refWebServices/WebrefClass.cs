using System;
using System.Collections.Generic;
using System.Text;
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

namespace CreateWebService
{
    public class CreateWebServices
    {
        /// <summary>
        /// 产生WebService引用DLL
        /// </summary>
        /// <returns></returns>
        public static string CreateWebServiceDll()
        {
            //if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "getWebServices.dll"))
            //    return string.Empty;
            WebClient web = new WebClient();
            web.Proxy = null;
            List<Stream> lsstream = new List<Stream>();
            List<ServiceDescription> lsdescription = new List<ServiceDescription>();
            Dictionary<string, ServiceDescription> dicdescription = new Dictionary<string, ServiceDescription>();

            XmlDocument doc = new XmlDocument();

            string XmlName = "DllConfig.xml";
            string dllname = "getWebServices.dll";           
            doc.Load(XmlName);
            
            //string ServerURL = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("URLAddres")).GetAttribute("URL");
            if ((((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("NewIP")).GetAttribute("IP") !=
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ServerIP")).GetAttribute("IP")) ||
                !File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + dllname))
            {
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ServerIP")).SetAttribute("IP",
                    ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("NewIP")).GetAttribute("IP"));

                string ServerURL = string.Format("http://{0}/{1}/",
                    ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ServerIP")).GetAttribute("IP"),
                    ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ServerPath")).GetAttribute("PATH"));

                CodeCompileUnit unit = new CodeCompileUnit();                
                
                foreach (XmlNode xn in doc.SelectSingleNode("AutoCreate").SelectSingleNode("FileList").ChildNodes)
                {
                    string _temp = ((XmlElement)xn).GetAttribute("Name");
                    if (!string.IsNullOrEmpty(_temp))
                    {
                        dicdescription.Add(string.Format("WebServices.{0}", _temp.Split('.')[0]),
                            ServiceDescription.Read(web.OpenRead(string.Format("{0}{1}?WSDL", ServerURL, _temp))));
                    }         
                }

                foreach (string str in dicdescription.Keys)
                {
                    CodeNamespace nmspace = new CodeNamespace();
                    ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
                    importer.ProtocolName = "Soap";
                    importer.Style = ServiceDescriptionImportStyle.Client;
                    importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;
                    nmspace.Name = str;
                    unit.Namespaces.Add(nmspace);
                    importer.AddServiceDescription(dicdescription[str], null, null);
                    ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);
                }

                CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
                CompilerParameters parameter = new CompilerParameters();
                parameter.GenerateExecutable = false;
                parameter.OutputAssembly = dllname;
                parameter.ReferencedAssemblies.Add("System.dll");
                parameter.ReferencedAssemblies.Add("System.XML.dll");
                parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
                parameter.ReferencedAssemblies.Add("System.Data.dll");

                CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);
                StringBuilder err = new StringBuilder(50000);
                if (result.Errors.HasErrors)
                {
                    for (int i = 0; i < result.Errors.Count; i++)
                    {
                        err.AppendLine(result.Errors[i].ErrorText);
                    }
                }
                if (string.IsNullOrEmpty(err.ToString()))
                    doc.Save(XmlName);              
                return err.ToString();
            }
            return null;
        }
    }
}
