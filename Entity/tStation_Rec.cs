using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class tStation_Rec
    {
        public string woId { get; set; }
        public string CraftId { get; set; }
        public string WorkData { get; set; }
        public string PartNumber { get; set; }
        public string ProductName { get; set; }
        public string WorkSection { get; set; }
        public string Class { get; set; }
        public string ClassDate { get; set; }
        public string Line { get; set; }
        public string PassQTY { get; set; }
        public string FailQTY { get; set; }
        public string RePassQTY { get; set; }
        public string ReFailQTY { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public  bool  Show_woId { get; set; }
        public bool Show_CraftId { get; set; }        
        public bool Show_Line { get; set; }
        public bool Show_PartNumber { get; set; }
        public bool Show_ProductName { get; set; }
        public bool Show_WorkData { get; set; }
        public bool Show_Class { get; set; }
        public bool Show_ClassDate { get; set; }
        public bool Show_ReFailQTY { get; set; }
        public bool Show_RePassQTY { get; set; }  
                       
                  
    }
}
