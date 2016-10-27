using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Net;
using System.Web.Services.Description;
using System.Xml;
using System.IO;
using System.CodeDom;
using System.Xml.Serialization;
using System.CodeDom.Compiler;
using System.Text;

namespace TestWeserver
{
    /// <summary>
    /// CreateWebServieRef 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class CreateWebServieRef : System.Web.Services.WebService
    {

        [WebMethod]
        public byte[] CreateRefDll(string Ipadd)
        {
           
           
            WebClient web = new WebClient();
            web.Proxy = null;
            Dictionary<string, ServiceDescription> dicdescription = new Dictionary<string, ServiceDescription>();
            XmlDocument doc = new XmlDocument();
            string XmlName = "DllConfig.xml";
            string dllname = "getWebServices.dll";
            doc.Load(Server.MapPath("") + "\\" + XmlName);

            if (File.Exists(Server.MapPath("") + "\\" + dllname))
                File.Delete(Server.MapPath("") + "\\" + dllname);
            string ServerURL = string.Format("http://{0}/{1}/", Ipadd,
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ServerPath")).GetAttribute("PATH"));

            CodeCompileUnit unit = new CodeCompileUnit();

            foreach (XmlNode xn in doc.SelectSingleNode("AutoCreate").SelectSingleNode("FileList").ChildNodes)
            {
                string _temp = ((XmlElement)xn).GetAttribute("Name");
                if (!string.IsNullOrEmpty(_temp))
                {
                    dicdescription.Add(string.Format("WebServices.{0}", _temp.Split('.')[0]), ServiceDescription.Read(web.OpenRead(string.Format("{0}{1}?WSDL", ServerURL, _temp))));
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
            parameter.OutputAssembly = Server.MapPath("") + "\\" + dllname;
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
                return File.ReadAllBytes(Server.MapPath("") + "\\" + dllname);
            else
                return null;
        }
    }
}
