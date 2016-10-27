using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SFIS_PRINT_SYSTEM_WIFI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string strFileName = System.IO.Path.GetFileName(Application.ExecutablePath).ToUpper();
            if (strFileName == "LablePrint_WIFI.EXE".ToUpper())
            {
                Process instance = RunningInstance();

                if (instance == null)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new PrintMain());
                }
                else
                {
                    HandleRunningInstance(instance);
                }
            }
            else
            {
                MessageBox.Show("程序名发生变更,程序不能启动", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }
        #region   只运行一个实例

        public static Process RunningInstance()
        {

            Process current = Process.GetCurrentProcess();//当前新启动的线程

            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            //遍历与当前进程名称相同的进程列表 

            foreach (Process process in processes)
            {

                //process,原来旧的线程与当前新启动的线程ID不一样

                //Ignore the current process 

                if (process.Id != current.Id)
                {

                    //Make sure that the process is running from the exe file. 

                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "//") == current.MainModule.FileName)
                    {

                        //Return the other process instance. 

                        return process;//返回原来旧线程的窗体

                    }

                }

            }

            return null;

        }

        private static void HandleRunningInstance(Process instance)
        {

            MessageBox.Show("该应用系统已经在运行！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL); //调用api函数，正常显示窗口 

            SetForegroundWindow(instance.MainWindowHandle); //将窗口放置最前端。 

        }

        [DllImport("User32.dll")]

        private static extern bool ShowWindowAsync(System.IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]

        private static extern bool SetForegroundWindow(System.IntPtr hWnd);

        private const int WS_SHOWNORMAL = 1;

        #endregion
    }
}
