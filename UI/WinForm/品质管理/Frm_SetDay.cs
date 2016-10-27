using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
using System.Xml;

namespace SFIS_V2
{
    public partial class Frm_SetDay : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_SetDay(Frm_FQC mfrm)
        {
            InitializeComponent();
            frm = mfrm;
        }
        Frm_FQC frm;
        private void Frm_SetDay_Load(object sender, EventArgs e)
        {
            //加载日期
            DataTable dt2 = CXmlFileToDataSet(@"R_Date.xml").Tables[0];
            Cmb_ReCheckDay.ValueMember = "Day";
            Cmb_ReCheckDay.DisplayMember = "Day";
            Cmb_ReCheckDay.DataSource = dt2;
        }

        /// <summary>
        /// 读取Xml文件信息,并转换成DataSet对象
        /// </summary>
        /// <remarks>
        /// DataSet ds = new DataSet();
        /// ds = CXmlFileToDataSet("/XML/upload.xml");
        /// </remarks>
        /// <param name="xmlFilePath">Xml文件地址</param>
        /// <returns>DataSet对象</returns>
        public static DataSet CXmlFileToDataSet(string xmlFilePath)
        {
            if (!string.IsNullOrEmpty(xmlFilePath))
            {
                string path = xmlFilePath;
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                try
                {
                    XmlDocument xmldoc = new XmlDocument();
                    //根据地址加载Xml文件
                    xmldoc.Load(path);

                    DataSet ds = new DataSet();
                    //读取文件中的字符流
                    StrStream = new StringReader(xmldoc.InnerXml);
                    //获取StrStream中的数据
                    Xmlrdr = new XmlTextReader(StrStream);
                    //ds获取Xmlrdr中的数据
                    ds.ReadXml(Xmlrdr);
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //释放资源
                    if (Xmlrdr != null)
                    {
                        Xmlrdr.Close();
                        StrStream.Close();
                        StrStream.Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            this.frm.R_Date = Cmb_ReCheckDay.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}