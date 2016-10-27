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

namespace WindowsFormsApplication1
{
    public class CreateWebProxyCls
    {
        WebClient web = new WebClient();
        bool sss;
        public string test()
        {
            List<Stream> lsstream = new List<Stream>();
            List<ServiceDescription> lsdescription = new List<ServiceDescription>();
            Dictionary<string, ServiceDescription> dicdescription = new Dictionary<string, ServiceDescription>();


            XmlDocument doc = new XmlDocument();

            doc.Load("DllConfig.xml");

            string ServerURL = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("URLAddres")).GetAttribute("URL");



            //CodeNamespace nmspace = new CodeNamespace();
           
            CodeCompileUnit unit = new CodeCompileUnit();
            //unit.Namespaces.Add(nmspace);

            foreach (XmlNode xn in doc.SelectSingleNode("AutoCreate").SelectSingleNode("FileList").ChildNodes)
            {
                string _temp = ((XmlElement)xn).GetAttribute("Name");
                if (!string.IsNullOrEmpty(_temp))
                {
                    //nmspace.Name = string.Format("WebServices.{0}", _temp.Split('.')[0]);
                    //unit.Namespaces.Add(nmspace);

                    dicdescription.Add(string.Format("WebServices.{0}", _temp.Split('.')[0]), ServiceDescription.Read(web.OpenRead(string.Format("{0}{1}?WSDL", ServerURL, _temp))));
                    //lsdescription.Add(ServiceDescription.Read(web.OpenRead(string.Format("{0}{1}?WSDL", ServerURL, _temp))));
                }
            }


            //int x = 0;
            //foreach (ServiceDescription description in lsdescription)
            //{
            //    ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
            //    importer.ProtocolName = "Soap";
            //    importer.Style = ServiceDescriptionImportStyle.Client;
            //    importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;

            //    importer.AddServiceDescription(description, null, null);

            //    //ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);
            //    ServiceDescriptionImportWarnings warning = importer.Import(unit.Namespaces[x], unit);
            //    x++;
            //}

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
            parameter.OutputAssembly = "getWebServices.dll";
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

            return err.ToString();
        }
    }



    public class qCreateWebServices
    {
        WebClient web = new WebClient();
        public void CreateWebServicesDll()
        {
            Stream stream = web.OpenRead("http://172.16.173.76/SFIS_WEBSER/ExecuteSqlCmd.asmx?WSDL");
            Stream stream1 = web.OpenRead("http://172.16.173.76/SFIS_WEBSER/tCraftInfo.asmx?WSDL");

            ServiceDescription description = ServiceDescription.Read(stream);
            ServiceDescription description1 = ServiceDescription.Read(stream1);

            ServiceDescriptionImporter importer = new ServiceDescriptionImporter();

            importer.ProtocolName = "Soap";
            importer.Style = ServiceDescriptionImportStyle.Client;
            importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;
            
            importer.AddServiceDescription(description, null, null);
            importer.AddServiceDescription(description1, null, null);

            CodeNamespace nmspace = new CodeNamespace("MYDLL");
            CodeCompileUnit unit = new CodeCompileUnit();
            unit.Namespaces.Add(nmspace);
            ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);
            

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters parameter = new CompilerParameters();
            parameter.GenerateExecutable = false;
            parameter.OutputAssembly = "test.dll";
            parameter.ReferencedAssemblies.Add("System.dll");
            parameter.ReferencedAssemblies.Add("System.XML.dll");
            parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
            parameter.ReferencedAssemblies.Add("System.Data.dll");

            CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);

        }
    }
}
