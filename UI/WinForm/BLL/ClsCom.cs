using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace FrmBLL
{
   public  class ReadComm
    {
        ReadComm()
       {

       }
       static SerialPort mycom;
       private readonly static ReadComm instance = new ReadComm();
       public static ReadComm Instance
       {
           get { return ReadComm.instance; }
       } 
      static ReadComm()
      {
          mycom = new SerialPort();
          mycom.PortName = "COM1";
          mycom.BaudRate = 9600;
          mycom.DataBits = 8;
          //mycom.Open();  
      }


      public SerialPort GetCom
      {
          get
          {
              if (mycom == null)
              {
                  mycom = new SerialPort();
                  mycom.PortName = "COM17";
                  mycom.BaudRate = 9600;
                  mycom.DataBits = 8;
              }
              return mycom;
          }
      }
       //public  void OpenCom()
       //{
           
           
         
       ////  Redcom.Parity = Parity.None;
       ////  Redcom.StopBits = StopBits.One;
       //  try
       //  {
       //      if (!Redcom.IsOpen)
       //      {
       //          Redcom.Open();
       //      }

       //  }
       // catch (Exception ex)
       //  {
       //      throw new Exception(ex.Message);
       //  }

       //}
      

       //public  void GetComData (System.Windows.Forms.TextBox txt)
       // {
       //    string ComData= Redcom.ReadExisting();
       //    if (ComData!="")
       //     {
       //          ComData = ComData.Substring(0, ComData.Length-1);
       //          ComData = ComData.Substring(1, ComData.Length - 1);
       //         txt.Text = ComData;               
                
       //      }

       //  }
    }
}
