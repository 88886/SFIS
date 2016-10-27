using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericProvider;

namespace BLL
{
  public  class t_Sn_UnBind
    {
      public t_Sn_UnBind()
      {
      }

      public string Insert_Sn_UnBind(IList< IDictionary<string,object>> ListMst)
      {
          try
          {
              IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
              dp.AddListData("SFCR.T_SN_UNBIND", ListMst);
              return "OK";
          }
          catch (Exception ex)
          {
              return ex.Message;
          }

      }

    }
}
