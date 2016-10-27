using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace SFIS_V2
{
    public partial class testsoap : Form
    {
        public testsoap()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebServices.SOAPTEST.SOAPTEST ds = new WebServices.SOAPTEST.SOAPTEST();
            DateTime dtBegin = DateTime.Now;
            DataSet dataSet = ds.GetNorthwindDataSet();
            this.label1.Text = string.Format("耗时：{0}", DateTime.Now - dtBegin);
            binddata(dataSet);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            WebServices.SOAPTEST.SOAPTEST ds = new WebServices.SOAPTEST.SOAPTEST();
            DateTime dtBegin = DateTime.Now;
            byte[] buffer = ds.GetDataSetBytes();
            BinaryFormatter ser = new BinaryFormatter();
            DataSet dataSet = ser.Deserialize(new MemoryStream(buffer)) as DataSet;
            this.label2.Text = string.Format("耗时：{0}", DateTime.Now - dtBegin) + "  " + buffer.Length;
            binddata(dataSet);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            WebServices.SOAPTEST.SOAPTEST ds = new WebServices.SOAPTEST.SOAPTEST();
            DateTime dtBegin = DateTime.Now;
            byte[] buffer = ds.GetDataSetSurrogateBytes();
            BinaryFormatter ser = new BinaryFormatter();
            DataSetSurrogate dss = ser.Deserialize(new MemoryStream(buffer)) as DataSetSurrogate;
            DataSet dataSet = dss.ConvertToDataSet();
            this.label3.Text = string.Format("耗时：{0}", DateTime.Now - dtBegin) + "  " + buffer.Length;
            binddata(dataSet);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            WebServices.SOAPTEST.SOAPTEST ds = new WebServices.SOAPTEST.SOAPTEST();
            
            DateTime dtBegin = DateTime.Now;
            byte[] zipBuffer = ds.GetDataSetSurrogateZipBytes();
            byte[] buffer = UnZipClass.Decompress(zipBuffer);
            BinaryFormatter ser = new BinaryFormatter();
            DataSetSurrogate dss = ser.Deserialize(new MemoryStream(buffer)) as DataSetSurrogate;
            DataSet dataSet = dss.ConvertToDataSet();
            this.label4.Text = string.Format("耗时：{0}", DateTime.Now - dtBegin) + "  " + zipBuffer.Length;
            binddata(dataSet);
        }
        private void binddata(DataSet dataSet)
        {
            this.dataGridView1.DataSource = dataSet.Tables[0];
            this.label5.Text = "共计：" + dataSet.Tables[0].Rows.Count + "条记录";
        }
        //客户端UnZipClass程序
        public static class UnZipClass
        {
            public static byte[] Decompress(byte[] data)
            {
                try
                {
                    MemoryStream ms = new MemoryStream(data);
                    Stream zipStream = null;
                    zipStream = new GZipStream(ms, CompressionMode.Decompress);
                    byte[] dc_data = null;
                    dc_data = ExtractBytesFromStream(zipStream, data.Length);
                    return dc_data;
                }
                catch
                {
                    return null;
                }
            }
            public static byte[] ExtractBytesFromStream(Stream zipStream, int dataBlock)
            {
                byte[] data = null;
                int totalBytesRead = 0;
                try
                {
                    while (true)
                    {
                        Array.Resize(ref data, totalBytesRead + dataBlock + 1);
                        int bytesRead = zipStream.Read(data, totalBytesRead, dataBlock);
                        if (bytesRead == 0)
                        {
                            break;
                        }
                        totalBytesRead += bytesRead;
                    }
                    Array.Resize(ref data, totalBytesRead);
                    return data;
                }
                catch
                {
                    return null;
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
