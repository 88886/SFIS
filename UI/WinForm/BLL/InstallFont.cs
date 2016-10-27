using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Text;
using System.Drawing;

namespace FrmBLL
{
    public class InstallFont
    {
        public InstallFont()
        {
            if (chkFont())
                installFont();
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int WriteProfileString(string lpszSection, string lpszKeyName, string lpszString);

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, // handle to destination window 
        uint Msg, // message 
        int wParam, // first message parameter 
        int lParam // second message parameter 
        );

        [DllImport("gdi32")]
        public static extern int AddFontResource(string lpFileName);

        private void installFont()
        {

            string WinFontDir = System.Environment.GetEnvironmentVariable("WINDIR") + "\\fonts";
            string FontFileName = "3OF9.TTF";
            string FontName = "3 of 9 Barcode";
            int Ret;
            string FontPath;
            //const int WM_FONTCHANGE = 0x001D;
            //const int HWND_BROADCAST = 0xffff;
            FontPath = WinFontDir + "\\" + FontFileName;
            if (!File.Exists(FontPath))
            {
                File.Copy(System.Windows.Forms.Application.StartupPath + "\\3OF9.TTF", FontPath);
                Ret = AddFontResource(FontPath);

                //Res = SendMessage(HWND_BROADCAST, WM_FONTCHANGE, 0, 0);
                //WIN7下编译这句会出错，不知道是不是系统的问题，这里应该是发送一个系统消息关系不大不影响字体安装，所以我注释掉了
                Ret = WriteProfileString("fonts", FontName + "(TrueType)", FontFileName);
            }
        }

        private bool chkFont()
        {
            InstalledFontCollection MyFont = new InstalledFontCollection();
            FontFamily[] MyFontFamilies = MyFont.Families;
            int Count = MyFontFamilies.Length;
            for (int i = 0; i < Count; i++)
            {
                if (MyFontFamilies[i].Name.IndexOf("3 of 9 Barcode") != -1)
                    return false;
            }
            return true;
        }

    }


}
