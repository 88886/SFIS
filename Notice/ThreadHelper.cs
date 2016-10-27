using System;
using System.Collections.Generic;
using System.Text;

namespace Notice
{
    public class ThreadHelper
    {
        public delegate void methods();
        public static void ThreadStart(methods m)
        {
            System.Threading.ThreadStart threadMethod;
            threadMethod = new System.Threading.ThreadStart(m);
            System.Threading.Thread thread = new System.Threading.Thread(threadMethod);
            thread.Start();
        }
    }
}
