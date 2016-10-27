using System;
using System.Collections.Generic;
using System.Text;

namespace Notice
{
    public class NoticeCfg
    {
        private int width = 250;
        public int Width { get { return width; } set { width = value; } }
        private int height = 140;
        public int Height { get { return height; } set { height = value; } }
        private int totalShow = 3;
        public int TotalShow { get { return totalShow; } set { totalShow = value; } }
        private bool overlap = false;
        public bool Overlap { get { return overlap; } set { overlap = value; } }
    }
}
