using System;
using System.Collections.Generic;

using System.Text;

namespace Notice
{
    public class Notice
    {
        public string msg { get; set; }
        public string detail { get; set; }
        public int wait { set; get; }
        public DateTime CreatTime { get; set; }

        public Notice()
        {
            CreatTime = DateTime.Now;
        }
    }
}
