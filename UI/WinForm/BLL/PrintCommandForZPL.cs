using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace FrmBLL
{
    public class PrintControls
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct OVERLAPPED
        {
            int Internal;
            int InternalHigh;
            int Offset;
            int OffSetHigh;
            int hEvent;
        }
        [DllImport("kernel32.dll")]
        private static extern int CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode, int lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);

        [DllImport("kernel32.dll")]
        private static extern bool WriteFile(int hFile, byte[] lpBuffer, int nNumberOfBytesToWriter, out int lpNumberOfBytesWriten, out OVERLAPPED lpOverLapped);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(int hObject);

        [DllImport("winspool.drv")]
        private static extern int ReadPrinter(int hPrinter, byte[] pBuf, int cdBuf, out int pNoByteRead);

        private int iHandle;

        private byte[] buf = new byte[10];
        private int byteread;

        private bool Open()
        {
            iHandle = CreateFile("lpt1", 0x40000000, 0, 0, 3, 0, 0);
            if (iHandle != -1)
                return true;
            else
                return false;
        }
        public bool Write(string MyString)
        {
            try
            {
                this.Open();
                if (iHandle != 1)
                {
                    int i;
                    OVERLAPPED x;
                    byte[] mybyte = System.Text.Encoding.Default.GetBytes(MyString);
                    return WriteFile(iHandle, mybyte, mybyte.Length, out i, out x);
                }
                else
                {
                    throw new Exception("Port Not Opened");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Close();
            }

        }

        private bool Close()
        {
            return CloseHandle(iHandle);
        }

        public int readdata()
        {
            ReadPrinter(iHandle, buf, 1024, out byteread);
            int x = byteread;
            return x;
        }
    }

    public class clsCodeSoft
    {
        /// <summary>
        /// 模板文件的完整路径
        /// </summary>
        private string labfile = string.Empty;

        private LabelManager2.ApplicationClass mlppx = null;
        private LabelManager2.Document mDoc = null;
        /// <summary>
        /// 模板文件公式变量集合
        /// </summary>
        public List<varinfo> lsFormulasInfo { get; set; }
        /// <summary>
        /// 模板文件填充器变量集合
        /// </summary>
        public List<varinfo> lsFormVariablesInfo { get; set; }
        /// <summary>
        /// 模板文件任意变量集合
        /// </summary>
        public List<varinfo> lsFreeVariablesInfo { get; set; }

        /// <summary>
        /// 构造函数指定需要操作的模板文件的所在路径
        /// </summary>
        /// <param name="_labfile">模板文件所在路径</param>
        public clsCodeSoft(string _labfile)
        {
            this.labfile = _labfile;
        }
        /// <summary>
        /// 初始化环境
        /// </summary>
        /// <param name="breadonly">模板文件是否只读</param>
        public void InitLppxApp(bool breadonly)
        {
            this.mlppx = new LabelManager2.ApplicationClass();
            mDoc = mlppx.Documents.Open(labfile, breadonly);

            mDoc.Activate();
            mDoc.ViewMode = LabelManager2.enumViewMode.lppxViewModeSize;

            lsFormulasInfo = new List<varinfo>();
            lsFormVariablesInfo = new List<varinfo>();
            lsFreeVariablesInfo = new List<varinfo>();

            for (int x = 1; x <= this.mDoc.Variables.Formulas.Count; x++)
            {        
                lsFormulasInfo.Add(new varinfo()
                {
                    varoutputmask = this.mDoc.Variables.Formulas.Item(x).OutputMask,
                    varprefix=this.mDoc.Variables.Formulas.Item(x).Prefix,
                    varsuffix=this.mDoc.Variables.Formulas.Item(x).Suffix,
                    varlen = this.mDoc.Variables.Formulas.Item(x).Length,
                    varname = this.mDoc.Variables.Formulas.Item(x).Name,
                    varval = this.mDoc.Variables.Formulas.Item(x).Value
                });
            }

            for (int x = 1; x <= this.mDoc.Variables.FormVariables.Count; x++)
            {
                lsFormVariablesInfo.Add(new varinfo()
                {
                    varoutputmask = this.mDoc.Variables.FormVariables.Item(x).OutputMask,
                    varprefix = this.mDoc.Variables.FormVariables.Item(x).Prefix,
                    varsuffix = this.mDoc.Variables.FormVariables.Item(x).Suffix,
                    varlen = this.mDoc.Variables.FormVariables.Item(x).Length,
                    varname = this.mDoc.Variables.FormVariables.Item(x).Name,
                    varval = this.mDoc.Variables.FormVariables.Item(x).Value
                });

            }

            for (int x = 1; x <= this.mDoc.Variables.FreeVariables.Count; x++)
            {
                lsFreeVariablesInfo.Add(new varinfo()
                {
                    varoutputmask = this.mDoc.Variables.FreeVariables.Item(x).OutputMask,
                    varprefix = this.mDoc.Variables.FreeVariables.Item(x).Prefix,
                    varsuffix = this.mDoc.Variables.FreeVariables.Item(x).Suffix,
                    varlen = this.mDoc.Variables.FreeVariables.Item(x).Length,
                    varname = this.mDoc.Variables.FreeVariables.Item(x).Name,
                    varval = this.mDoc.Variables.FreeVariables.Item(x).Value
                });
            }


        }

        /// <summary>
        /// 填充任意变量
        /// </summary>
        public string FillFreeVariable()
        {
            if (mlppx == null)
                return "Lppa Not Init";
            if (mDoc == null)
                return "Lab File Not Init";

            foreach (varinfo item in lsFreeVariablesInfo)
            {
                this.mDoc.Variables.FreeVariables.Item(item.varname).OutputMask = item.varoutputmask;
                this.mDoc.Variables.FreeVariables.Item(item.varname).Prefix = item.varprefix;
                this.mDoc.Variables.FreeVariables.Item(item.varname).Suffix = item.varsuffix;
                this.mDoc.Variables.FreeVariables.Item(item.varname).Length = item.varlen;
                this.mDoc.Variables.FreeVariables.Item(item.varname).Value = item.varval;
            }
            return string.Empty;
        }
        /// <summary>
        /// 填充填充器变量
        /// </summary>
        public string FillFormVariable()
        {
            if (mlppx == null)
                return "Lppa Not Init";
            if (mDoc == null)
                return "Lab File Not Init";
            foreach (varinfo item in lsFormVariablesInfo)
            {
                this.mDoc.Variables.FormVariables.Item(item.varname).OutputMask = item.varoutputmask;
                this.mDoc.Variables.FormVariables.Item(item.varname).Prefix = item.varprefix;
                this.mDoc.Variables.FormVariables.Item(item.varname).Suffix = item.varsuffix;
                this.mDoc.Variables.FormVariables.Item(item.varname).Length = item.varlen;
                this.mDoc.Variables.FormVariables.Item(item.varname).Value = item.varval;
            }
            return string.Empty;
        }
        /// <summary>
        /// 填充公式变量
        /// </summary>
        public string FillFormulas()
        {
            if (mlppx == null)
                return "Lppa Not Init";
            if (mDoc == null)
                return "Lab File Not Init";
            foreach (varinfo item in lsFormulasInfo)
            {
                this.mDoc.Variables.Formulas.Item(item.varname).OutputMask = item.varoutputmask;
                this.mDoc.Variables.Formulas.Item(item.varname).Prefix = item.varprefix;
                this.mDoc.Variables.Formulas.Item(item.varname).Suffix = item.varsuffix;
                this.mDoc.Variables.Formulas.Item(item.varname).Length = item.varlen;
                this.mDoc.Variables.Formulas.Item(item.varname).Expression = string.Format("output(\"{0}\")", item.varval);
            }
            return string.Empty;
        }
        /// <summary>
        /// 打印模板文件
        /// </summary>
        /// <param name="printnum">打印数量</param>
        /// <returns></returns>
        public string PrintLab(int printnum)
        {
            if (mlppx == null)
                return "Lppa Not Init";
            if (mDoc == null)
                return "Lab File Not Init";
            this.mDoc.PrintDocument(printnum);
            return string.Empty;
        }

        /// <summary>
        /// 关闭模板文件并退出Codesoft
        /// </summary>
        /// <param name="isSave">是否保存</param>
        public void CloseLab(bool isSave)
        {
            try
            {
                if (this.mlppx != null)
                {
                    if (this.mDoc != null)
                        this.mDoc.Close(isSave);

                    this.mlppx.Quit();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 模板变量的内容
        /// </summary>
        public class varinfo
        {
            /// <summary>
            /// 变量输出的格式
            /// </summary>
            public string varoutputmask { get; set; }
            /// <summary>
            /// 变量前缀
            /// </summary>
            public string varprefix { get; set; }
            /// <summary>
            /// 变量后缀
            /// </summary>
            public string varsuffix { get; set; }
            /// <summary>
            /// 变量名称
            /// </summary>
            public string varname { get; set; }
            /// <summary>
            /// 变量值
            /// </summary>
            public string varval { get; set; }
            /// <summary>
            /// 变量长度
            /// </summary>
            public int varlen { get; set; }
        }
    }
}
