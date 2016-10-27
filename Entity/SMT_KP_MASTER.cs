using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class SMT_KP_MASTER
    {
        #region 成员变量
        private string _masterId = "";
        private string _partnumber = "";
        private string _modelname = "";
        private string _bomver = "";
        private string _pcbside = "";
        private DateTime _recdate =DateTime.Now;
        private string _reserve1 = "0";
        private string _reserve2 = "0";
        private string lineid = "";
        private string userid = "";
        
        #endregion
        #region 属性
        public string Auditinguser { get; set; }
        public string masterId
        {
            get { return _masterId; }
            set { _masterId = value; }
        }
        public string partnumber
        {
            get { return _partnumber; }
            set { _partnumber = value; }
        }
        public string modelname
        {
            get { return _modelname; }
            set { _modelname = value; }
        }
        public string bomver
        {
            get { return _bomver; }
            set { _bomver = value; }
        }
        public string pcbside
        {
            get { return _pcbside; }
            set { _pcbside = value; }
        }
        public DateTime recdate
        {
            get { return _recdate; }
            set { _recdate = value; }
        }

        public string reserve1
        {
            get { return _reserve1; }
            set { _reserve1 = value; }
        }
        public string reserve2
        {
            get { return _reserve2; }
            set { _reserve2 = value; }
        }
        public string Userid
        {
            get { return userid; }
            set { userid = value; }
        }
        public string Lineid
        {
            get { return lineid; }
            set { lineid = value; }
        }
        #endregion

    }
}
