using System;
using System.Collections.Generic;
using System.Text;

namespace Notice
{
    public class NoticeQueue
    {
        private static Node head, rear;
        private static int length;
        private static readonly object lck = new object();
        static NoticeQueue()
        {
            head = rear = null;
            length = 0;
        }
        //public static int c = 0;
        //public static int d = 0;
        public static int Length()
        {
            return length;
        }
        public static void EnQueue(Notice notice)
        {
            EnQueue(notice, false);
        }
        public static void EnQueue(Notice notice, bool urgency)
        {
            lock (lck)
            {
                if (rear == null)
                {
                    rear = new Node();
                    head = rear;
                    rear.notice = notice;
                    length++;
                }
                else
                {
                    if (head==null)
                    {
                        head = rear;
                    }
                    if (urgency)
                    {
                        Node newHead = new Node();
                        newHead.notice = notice;
                        //Node temp = new Node();
                        //temp = head;
                        newHead.next = head;
                        head = newHead;
                    }
                    else
                    {
                        rear.next = new Node();
                        rear.next.notice = notice;
                        rear = rear.next;
                    }
                    length++;
                }
                //++c;
                NoticeThreading.Instance.sema.Set();
            }
            
        }

        public static Notice DeQueue()
        {
            lock (lck)
            {
                if (length <= 0)
                {
                    rear = head = null;
                    //NoticeThreading.Instance.semaphore.WaitOne();
                    return null;
                }
                Notice notice = head.notice;
                head = head.next;
                length--;
                //++d;
                return notice;
            }
        }

    }
}
