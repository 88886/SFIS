using System;
using System.Collections.Generic;
using System.Text;

namespace Notice
{
    public sealed class NoticeThreading
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hwnd);

        //public int count = 0;
        private bool[] location = new bool[1] { true };
        private static volatile NoticeThreading instance;
        private static object syncRoot = new Object();
        //private delegate void methods();

        private System.Threading.ThreadStart threadMethod;
        private System.Threading.Thread thread;

        public System.Threading.AutoResetEvent sema { get; set; }

        private System.Threading.AutoResetEvent sema_private { get; set; }
        private NoticeThreading()
        {
            int c = NoticeCenter.Cfg.TotalShow;
            if (location.Length != c && c >= 1)
            {
                location = new bool[c];
                for (int i = 0; i < location.Length; i++)
                {
                    location[i] = true;
                }
            }

            sema_private = new System.Threading.AutoResetEvent(false);
            sema = new System.Threading.AutoResetEvent(false);
            threadMethod = new System.Threading.ThreadStart(Start);
            thread = new System.Threading.Thread(threadMethod);
            thread.Start();
        }

        public static NoticeThreading Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new NoticeThreading();
                    }
                }
                return instance;
            }
        }

        public void Start()
        {
            while (true)
            {
                if (NoticeQueue.Length() <= 0)
                {
                    sema.WaitOne();
                }
                else
                {
                    ThreadHelper.ThreadStart(NoticeDoEvent);
                }
            }
        }

        private void NoticeDoEvent()
        {
            Notice notice = null;
            notice = NoticeQueue.DeQueue();
            if (notice != null)
            {
                //if (new CfgLoader().GetConfig("show_notice") == "0")
                //    return;
                IntPtr activeForm = GetActiveWindow();
                int count = -1;
                while (true)
                {
                    count = GetLocation();
                    if (count == -1)
                        sema_private.WaitOne();
                    else
                        break;
                }
                System.Windows.Forms.Form form = new NoticeWin(notice, count, SetLocation);
                form.Show();
                SetActiveWindow(activeForm);
                while (!form.IsDisposed)
                {
                    System.Windows.Forms.Application.DoEvents();
                }
            }
        }

        private void SetLocation(int count)
        {
            lock (syncRoot)
            {
                sema_private.Set();
                location[count] = true;
            }
        }
        private int GetLocation()
        {
            lock (syncRoot)
            {
                for (int i = 0; i < location.Length; i++)
                {
                    if (location[i])
                    {
                        location[i] = false;
                        if (NoticeCenter.Cfg.Overlap)
                        {
                            return 0;
                        }
                        else
                        {
                            return i;
                        }
                    }
                }
                return -1;
            }
        }

        //private void ThreadStart(methods m)
        //{
        //    System.Threading.ThreadStart threadMethod;
        //    threadMethod = new System.Threading.ThreadStart(m);
        //    System.Threading.Thread thread = new System.Threading.Thread(threadMethod);
        //    thread.Start();

        //}

    }
}
