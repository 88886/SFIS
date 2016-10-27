using System;
using System.Collections.Generic;
using System.Text;
using GenericProvider;
using SrvComponent;

namespace BLL
{
    public partial class SetTime
    {
    
        public SetTime()
        {
            
        }

        public DateTime GetServersTime()
        {         
            return System.DateTime.Now;
        }
    }
}
