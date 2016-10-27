using System;
using System.Collections.Generic;
using System.Text;

namespace Notice
{
    public class NoticeCenter
    {
        private static NoticeCfg cfg = new NoticeCfg();
        public static NoticeCfg Cfg
        {
            get
            {
                return cfg;
            }
            set
            {
                cfg = value;
            }
        }

        public static void Show(Notice notice)
        {
            NoticeQueue.EnQueue(notice);
        }
        public static void Show(Notice notice, bool urgency)
        {
            NoticeQueue.EnQueue(notice, urgency);
        }
    }
}
