using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace SFIS_V2.BLL333333
{
   public  class ReadCom
    {
      static  SerialPort Redcom = new SerialPort();
      public ReadCom()
      {
            
      }
      static ReadCom()
      {
          Redcom.PortName = "COM1";
          Redcom.BaudRate = 9600;
          Redcom.DataBits = 8;
          Redcom.Open();
      }

       public  void OpenCom()
       {
           
           
         
       //  Redcom.Parity = Parity.None;
       //  Redcom.StopBits = StopBits.One;
         try
         {
             if (!Redcom.IsOpen)
             {
                 Redcom.Open();
             }

         }
        catch (Exception ex)
         {
             throw new Exception(ex.Message);
         }

       }
      

       public  void GetComData (System.Windows.Forms.TextBox txt)
        {
           string ComData= Redcom.ReadExisting();
           if (ComData!="")
            {
                 ComData = ComData.Substring(0, ComData.Length-1);
                 ComData = ComData.Substring(1, ComData.Length - 1);
                txt.Text = ComData;               
                
             }

         }
    }
}
