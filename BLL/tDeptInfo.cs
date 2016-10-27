using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericUtil;
using GenericProvider;
using SystemObject;

namespace BLL
{
   public partial class tDeptInfo
    {
      
      public tDeptInfo()
       {
          
       }
       /// <summary>
      /// SFCB.B_DEPT_INFO
       /// </summary>
      string table = "SFCB.B_DEPT_INFO";
       /// <summary>
       /// 新增部门信息
       /// </summary>
       /// <param name="deptinfo"></param>
       public  void InsertDeptInfo(string deptinfo)
       {          
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> dic = MapListConverter.JsonToDictionary(deptinfo);
           dp.AddData(table, dic);
       }

       /// <summary>
       /// 获取所有部门信息
       /// </summary>
       /// <returns></returns>
       public  System.Data.DataSet GetDeptInfo()
       {          
           int count = 0;
           string table = "SFCB.B_DEPT_INFO d,SFCB.B_FAC_INFO f";
           string fieldlist = "f.facname as 名称,d.deptname as 部门名称,d.userId as 负责人,d.deptdesc as 部门描述,d.facId as 工厂编号";
           string filter = "f.facId=d.facId";           
           return TransactionManager.GetData(table, fieldlist, filter, null, null, null, out count);

       }

 

       /// <summary>
       /// 修改部门信息根据部门名称
       /// </summary>
       /// <param name="deptname"></param>
       /// <param name="deptinfo"></param>
       public  void EditDeptInfo(string deptname)
       {
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> mst = MapListConverter.JsonToDictionary(deptname);
           dp.UpdateData(table, new string[] { "deptname" }, mst);
       }

       /// <summary>
       /// 删除部门
       /// </summary>
       /// <param name="deptname"></param>
       public  void DeleteDeptInfo(string deptname)
       {            
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);       
           IDictionary<string, object> mst = new Dictionary<string, object>();
           mst.Add("deptname", deptname);
           dp.DeleteData(table, mst);
       }
    }
}
