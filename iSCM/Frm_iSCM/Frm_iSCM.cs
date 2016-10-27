using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms; 
using System.Reflection;
using System.IO;
using System.Net;
using System.Xml;
using System.Diagnostics;
using DevComponents.DotNetBar;

namespace Frm_iSCM
{
    public partial class Frm_iSCM : Office2007Form  //Form
    {
        public Frm_iSCM()
        {
            InitializeComponent();
        }



        private delegate void LoadAllDLL();
        LoadAllDLL LoadDll;
        private void Frm_iSCM_Load(object sender, EventArgs e)
        {

           // string ss= System.AppDomain.CurrentDomain.BaseDirectory();
            Control.CheckForIllegalCrossThreadCalls = false;
            LoadDll = new LoadAllDLL(LoadFrmDLL);
            LoadDll.BeginInvoke(null,null);
                         
        }
        public void LoadFrmDLL()
        {
            LabLoadMsg.Text = "正在加载DLL...";  
            Assembly assembly = null;
            string windowsPath = Path.Combine(Application.StartupPath, "");
            foreach (string dllFile in Directory.GetFiles(windowsPath, "*.dll"))
            {

                assembly = Assembly.LoadFile(dllFile);
                Type[] types = assembly.GetTypes();
                foreach (Type t in types)
                {
                    if (t.BaseType == typeof(Office2007Form))
                    {
                        if (t.FullName.IndexOf("DevComponents") == -1)
                        {
                          //  cmb_prglist.Invoke(new EventHandler(delegate
                         //   {
                                cmb_prglist.Items.Add(t.FullName);
                        //    }));
                           
                        }
                    }
                }
            }
            LabLoadMsg.Text = "加载DLL完成...";  
            cmb_prglist.SelectedIndex = 0;       
            OptionForm();
           
        }
 
         private void CheckDllInfo(Assembly ably)
         {
            //获取版本
             string Dllver = ably.GetName().Version.ToString();
            //获取Dll名称
             string DllName = ably.GetName().Name;
            //获取公司 
            //compant = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute))).Company;
            //获取描述说明 
            //  desc = ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyDescriptionAttribute))).Description;
             DownLoadFile(DllName + ".DLL", System.IO.Directory.GetCurrentDirectory());
           
          }
    

        private void imbt_select_Click(object sender, EventArgs e)
        {           
            OptionForm();
        }
        Office2007Form Frm = null;
        private void OptionForm()
        {
            PanelFrm.Invoke(new EventHandler(delegate
            {        
              
            Frm = (Office2007Form)Assembly.Load(cmb_prglist.Text.Split('.')[0]).CreateInstance(cmb_prglist.Text); 
        
            Frm.TopLevel = false;
            Frm.Dock = System.Windows.Forms.DockStyle.Fill;
            Frm.FormBorderStyle = FormBorderStyle.None;
            Frm.Parent = this.panel1;
            foreach (Control ctl in PanelFrm.Controls)
            {
                if (ctl.Name == Frm.Name)
                {
                    PanelFrm.Controls.SetChildIndex(ctl, 0);
                    GroupBox_Frm.Text = Frm.Text;
                    return;
                }
            }
            this.PanelFrm.Controls.Add(Frm);
            GroupBox_Frm.Text = Frm.Text;
            Frm.Show();
            }));
                 
        }

        private void DownLoadFile(string FileName,string FilePath)
        {
            XmlDocument doc = new XmlDocument();
            string XmlName = "DllConfig.xml";
            doc.Load(XmlName);
            string _Host = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("FTPCONFIG").SelectSingleNode("Host")).GetAttribute("Name").ToString();
            string _DIR = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("FTPCONFIG").SelectSingleNode("DIR")).GetAttribute("Name").ToString();
            string _User = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("FTPCONFIG").SelectSingleNode("User")).GetAttribute("Name").ToString();
            string _Password = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("FTPCONFIG").SelectSingleNode("Password")).GetAttribute("Name").ToString();
            string _Port = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("FTPCONFIG").SelectSingleNode("Port")).GetAttribute("Name").ToString();

            FrmPublic.FtpClient DownFile = new FrmPublic.FtpClient(_Host, _DIR, _User, _Password,int.Parse(_Port));
            try
            {
                DownFile.Connect();
                DownFile.Get(FileName, FilePath, FileName);                
            }
            catch (ExecutionEngineException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DownFile.DisConnect();
            }
        }

        private void Frm_iSCM_FormClosing(object sender, FormClosingEventArgs e)
        {
                         
                Process[] prc = Process.GetProcessesByName("lppa");
                if (prc.Length > 0)
                    foreach (Process pc in prc)
                    {
                        pc.Kill();
                    }
           
        }      

          
    }
}
