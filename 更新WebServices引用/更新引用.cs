using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Web.Services.Description;
using System.Xml;
using System.CodeDom;
using System.Xml.Serialization;
using System.CodeDom.Compiler;

namespace WindowsFormsApplication1
{
    public partial class 更新引用 : Form
    {
        public 更新引用()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        string dllname = "getWebServices.dll";
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string err;
                if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\" + dllname))
                {
                    //如果存在则删除
                    File.Delete(System.IO.Directory.GetCurrentDirectory() + "\\" + dllname);
                }
                MessageBox.Show(string.IsNullOrEmpty(err = CreateWebServiceDll()) ? "DLL 生成成功" : err);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        int x = 0;

        /// <summary>
        /// 产生WebService引用DLL
        /// </summary>
        /// <returns></returns>
        public string CreateWebServiceDll()
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
           // string dllname = "getWebServices.dll";   
            doc.Load(XmlName);

            //string ServerURL = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("URLAddres")).GetAttribute("URL");
            if ((((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("NewIP")).GetAttribute("IP") !=
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ServerIP")).GetAttribute("IP")) ||
                !File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + dllname))
            {
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ServerIP")).SetAttribute("IP",((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("NewIP")).GetAttribute("IP"));

                string ServerURL = string.Format("http://{0}/{1}/",((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ServerIP")).GetAttribute("IP"),
                    ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ServerPath")).GetAttribute("PATH"));

                CodeCompileUnit unit = new CodeCompileUnit();

                foreach (XmlNode xn in doc.SelectSingleNode("AutoCreate").SelectSingleNode("FileList").ChildNodes)
                {
                    string _temp = ((XmlElement)xn).GetAttribute("Name");
                    try
                    {
                        if (!string.IsNullOrEmpty(_temp))
                        {
                            dicdescription.Add(string.Format("WebServices.{0}", _temp.Split('.')[0]),
                                ServiceDescription.Read(web.OpenRead(string.Format("{0}{1}?WSDL", ServerURL, _temp))));
                        }
                    }
                    catch(Exception ex)
                    {
                        throw new Exception( string.Format("读取类[{0}]错误,Msg:{1}", _temp,ex.Message));
                    }
                }
                foreach (string str in dicdescription.Keys)
                {
                    x++;
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
