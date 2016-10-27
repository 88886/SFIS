using System;
using System.Collections.Generic;

using System.Text;

namespace Notice
{
    public class Node
    {
        public Notice notice;
        public Node prior, next;

        public Node()
        {
            prior = null;
            next = null;
            notice = new Notice();
        }
    }
}
