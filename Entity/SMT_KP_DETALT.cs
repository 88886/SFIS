using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class SMT_KP_DETALT
    {
        #region 成员变量
        private long _id;
        private string _kpnumber;
        private string _kpdesc;
        private bool _kpdistinct;
        private int _priorityclass;
        private string _loction;
        //private DateTime _recdate;
        private string _reserve1="";
        private string _reserve="";
        private string _stationno;
        private string _masterid;
        private string _replacegroup="";



        #endregion
        #region 属性
        public string Replacegroup
        {
            get { return _replacegroup; }
            set { _replacegroup = value; }
        }
        public string Stationno
        {
            get { return _stationno; }
            set { _stationno = value; }
        }
        public string Masterid
        {
            get { return _masterid; }
            set { _masterid = value; }
        }
        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string KPNumber
        {
            get { return _kpnumber; }
            set { _kpnumber = value; }
        }

        public string KPDesc
        {
            get { return _kpdesc; }
            set { _kpdesc = value; }
        }

        public bool KPDistinct
        {
            get { return _kpdistinct; }
            set { _kpdistinct = value; }
        }

        public int Priorityclass
        {
            get { return _priorityclass; }
            set { _priorityclass = value; }
        }

        public string Loction
        {
            get { return _loction; }
            set { _loction = value; }
        }

        //public DateTime recdate
        //{
        //    get { return _recdate; }
        //    set { value = _recdate; }
        //}

        public string reserve1
        {
            get { return _reserve1; }
            set { _reserve1 = value; }
        }

        public string reserve
        {
            get { return _reserve; }
            set { _reserve = value; }
        }

        public int loctionLen { get; set; }
        #endregion
    }
}