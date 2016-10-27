using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;
using SystemObject;
using System.Data.Common;
using SrvComponent;

namespace BLL
{
    public partial class tProduct
    {
        
        public tProduct()
        {
           
        }

        #region 产品分类
        /// <summary>
        /// 增加一种产品分类
        /// </summary>
        /// <param name="product"></param>
        public void InsertProdutsort(string sortname)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "insert into SFCB.b_productsort (sortname) values(@sTname)";

            //cmd.Parameters.Add("sTname", MySqlDbType.VarChar, 30).Value = productsort.sortname;
            // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("sortname", sortname);
            dp.AddData("SFCB.B_PRODUCTSORT", mst);
        }

        /// <summary>
        /// 查询产品分类是否存在
        /// </summary>
        /// <param name="productsort"></param>
        /// <returns></returns>
        public bool GetProductSortByName(string sortname)
        {
            string _name = string.Empty;

            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select sortname from SFCB.b_productsort where sortname=@sTname";
            //cmd.Parameters.Add("sTname", MySqlDbType.VarChar, 30).Value = sortname;
            //DataSet _ds =  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCB.B_PRODUCTSORT";
            string fieldlist = "SORTNAME";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("SORTNAME", sortname);
            DataSet _ds = dp.GetData(table, fieldlist, mst, out count);
            foreach (DataRow dr in _ds.Tables[0].Rows)
            {
                if (!string.IsNullOrEmpty(dr["sortname"].ToString()))
                {
                    _name = dr["sortname"].ToString();
                    break;
                }
            }
            if (string.IsNullOrEmpty(_name))
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取指定成品料号所对应的产品序列号，serialname
        /// </summary>
        /// <param name="partnumber">成品料号</param>
        /// <returns></returns>
        public DataSet GetProductSnType(string partnumber)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select serialname from SFCB.b_Product_Serial_Info where partnumber=@PN";

            //cmd.Parameters.Add("PN", MySqlDbType.VarChar, 30).Value = partnumber;

            //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            string table = "SFCB.B_PRODUCT_SERIAL_INFO";
            string fieldlist = "SERIALNAME";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("PARTNUMBER", partnumber);
            return dp.GetData(table, fieldlist, mst, out count);
        }

        /// <summary>
        /// 添加序列号类型
        /// </summary>
        /// <param name="lname"></param>
        public void AddLableName(string serialname)
        {
            try
            {
                //MySqlCommand cmd = new MySqlCommand();
                //cmd.CommandText = "insert into SFCB.B_SERIAL_TYPES (serialname) values(@sName)";

                //cmd.Parameters.Add("sName", MySqlDbType.VarChar, 30).Value = sn.serialname;
                // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("SERIALNAME", serialname);
                dp.AddData("SFCB.B_SERIAL_TYPES", mst);
            }
            catch
            {
                throw new Exception("序列号名称重复");
            }
        }

        /// <summary>
        /// 获取产品类型列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetProductSort()
        {
            List<string> productsort = new List<string>();

            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select sortname from SFCB.b_productsort ";
            //DataTable _dt =  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];
            string table = "SFCB.B_PRODUCTSORT";
            string fieldlist = "SORTNAME";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);           
            DataTable _dt = dp.GetData(table, fieldlist, null, out count).Tables[0];

            if (_dt != null && _dt.Rows.Count > 0)
            {
                foreach (DataRow item in _dt.Rows)
                {
                    productsort.Add(item["sortname"].ToString());
                }
            }
            return productsort;
        }
        #endregion

        #region 产品
        /// <summary>
        /// 获取指定料号的产品信息
        /// </summary>
        /// <param name="partnumber"></param>
        /// <returns></returns>
        public DataSet GetProduct(string partnumber, string productname)
        {          
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select partnumber,sortname,productname,productcolor,productdesc,other,productsn,solution,barcode_len,nal_prefix from SFCB.B_PRODUCT where partnumber=@PN";
            //cmd.Parameters.Add("PN", MySqlDbType.VarChar, 30).Value = partnumber;
            //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            string table = "SFCB.B_PRODUCT";
            string fieldlist = "partnumber,sortname,productname,productcolor,productdesc,other,productsn,solution,barcode_len,nal_prefix,CUSTOMER,STAGE".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(partnumber))
            mst.Add("PARTNUMBER", partnumber);
            if (!string.IsNullOrEmpty(productname))
                mst.Add("PRODUCTNAME", partnumber);
            return dp.GetData(table, fieldlist, mst, out count);
        }

        ///// <summary>
        ///// 获取指定产品名称的产品信息
        ///// </summary>
        ///// <param name="productname"></param>
        ///// <returns></returns>
        //public DataSet GetProductByName(string productname)
        //{      
        //    //MySqlCommand cmd = new MySqlCommand();
        //    //cmd.CommandText = "select * from SFCB.B_PRODUCT where productname like @PdName";
        //    //cmd.Parameters.Add("PdName", MySqlDbType.VarChar, 30).Value = productname + "%";
        //    //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);


        //    string table = "SFCB.B_PRODUCT";
        //    string fieldlist = "partnumber,sortname,productname,productcolor,productdesc,other,productsn,solution,barcode_len,nal_prefix".ToUpper();
        //    int count = 0;
        //    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
        //    IDictionary<string, object> mst = new Dictionary<string, object>();
        //    mst.Add("PRODUCTNAME", productname);
        //    return dp.GetData(table, fieldlist, null, out count);
        //}
        ///// <summary>
        ///// 获取所有的产品
        ///// </summary>
        //public DataSet GetProduct()
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "select  partnumber,sortname,productname,productcolor,productdesc,other,productsn,solution,barcode_len,nal_prefix from SFCB.B_PRODUCT";
        //    return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //}

        /// <summary>
        /// 返回所有产品信息
        /// </summary>
        /// <param name="partnumber"></param>
        /// <returns></returns>
        public System.Data.DataSet GetProductInfo(string partnumber, string productName)
        {
            //string sql = string.Empty;
            //MySqlCommand cmd = new MySqlCommand();
            //if (!string.IsNullOrEmpty(partnumber))
            //{
            //    sql = " and p.partnumber = @PN";
            //    cmd.Parameters.Add("PN", MySqlDbType.VarChar, partnumber.Length + 5).Value = partnumber;
            //}

            //if (!string.IsNullOrEmpty(productName))
            //{
            //    sql += " and p.productname = @PdName ";
            //    cmd.Parameters.Add("PdName", MySqlDbType.VarChar, productName.Length + 5).Value = productName ;
            //}

            //cmd.CommandText = string.Format("select p.partnumber,p.sortname,p.productname,p.productcolor,p.productdesc,m.VersionCode,m.TrayQty,m.CartonQty,m.PalletQty,p.other,p.PRODUCTSN,p.SOLUTION,p.BARCODE_LEN,p.nal_prefix from SFCB.B_PRODUCT p,SFCB.B_PACK_PARAMETERS m where p.partnumber=m.PartNumber {0}",
            //    sql);

            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            IDictionary<string, object> mst = new Dictionary<string, object>();   
            int count = 0;
            string table = "SFCB.B_PRODUCT p,SFCB.B_PACK_PARAMETERS m";
            string fieldlist = "p.partnumber,p.sortname,p.productname,p.productcolor,p.productdesc,m.VersionCode,m.TrayQty,m.CartonQty,m.PalletQty,p.other,p.PRODUCTSN,p.SOLUTION,p.BARCODE_LEN,p.nal_prefix,P.CUSTOMER,P.STAGE";
            string filter = "p.partnumber=m.PartNumber ";
            int x = -1;
            if (!string.IsNullOrEmpty(partnumber))
            {
                x++;
                filter+=" and p.partnumber ={"+x.ToString()+"}";
                mst.Add("partnumber", partnumber);
            }
            if (!string.IsNullOrEmpty(productName))
            {
                x++;
                filter +=" and p.productname ={"+x.ToString()+"}";
                mst.Add("productname", productName);
            }
       
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);

        }

     
        /// <summary>
        /// 增加产品所对应的标签
        /// </summary>
        /// <param name="partnumber"></param>
        /// <param name="lslablenames"></param>
        /// <param name="Err"></param>
        public void InsertProductLable(string partnumber, List<string> lslablenames, out string Err)
        {
            Err = string.Empty;        
            try
            {
                //foreach (string item in lslablenames)
                //{
                //    MySqlCommand cmd = new MySqlCommand();
                //    cmd.CommandText = "insert into SFCB.b_Product_Serial_Info (partnumber,serialname) values(@PN,@sName)";
                //    cmd.Parameters.Add("PN", MySqlDbType.VarChar, 30).Value = partnumber;
                //    cmd.Parameters.Add("sName", MySqlDbType.VarChar, 30).Value = item;
                //     BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                //}

                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IList<IDictionary<string, object>> list = new List<IDictionary<string, object>>();
                 IDictionary<string, object> mst = null;
                foreach (string item in lslablenames)
                {                  
                    mst = new Dictionary<string, object>();
                    mst.Add("PARTNUMBER", partnumber);
                    mst.Add("SERIALNAME", item);
                    list.Add(mst);
                }
                dp.AddListData("SFCB.B_PRODUCT_SERIAL_INFO", list);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
            }
        }
        #endregion

        #region 标签
        /// <summary>
        /// 获取所有的标签类型
        /// </summary>
        public List<string> GetLableList()
        {
            List<string> lslablename = new List<string>();
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select serialname from SFCB.B_SERIAL_TYPES";

            //DataTable _dt =  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];

            string table = "SFCB.B_SERIAL_TYPES";
            string fieldlist = "SERIALNAME";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);         
            DataTable _dt= dp.GetData(table, fieldlist, null, out count).Tables[0];

            DataView dv = _dt.DefaultView;
            dv.Sort = "SERIALNAME Asc";
            DataTable dt2 = dv.ToTable();

            foreach (DataRow item in dt2.Rows)
            {
                lslablename.Add(item["serialname"].ToString());
            }
            return lslablename;
        }

        /// <summary>
        /// 获取指定产品所对应的序列号类型列表
        /// </summary>
        /// <param name="partnumber">产品料号</param>
        /// <returns>序列号类型列表</returns>
        public List<string> GetProductLableNames(string partnumber)
        {
            List<string> names = new List<string>();          
            string table = "SFCB.B_PRODUCT_SERIAL_INFO";
            string fieldlist = "serialname";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("PARTNUMBER", partnumber);
           DataTable _dt= dp.GetData(table, fieldlist,  mst, out count).Tables[0];
            foreach (DataRow item in _dt.Rows)
            {
                names.Add(item["serialname"].ToString());
            }
            return names;
        }
        /// <summary>
        /// 通过工单号查询产品信息
        /// </summary>
        /// <param name="woid"></param>
        /// <returns></returns>
        public DataSet GetProductInfoByWoId(string woid)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select p.partnumber,p.sortname,p.productname,p.productcolor,p.productdesc,p.other,p.productsn from SFCB.B_PRODUCT p,SFCR.T_WO_INFO w where p.partnumber=w.partnumber and w.woId=@MO";
            //cmd.Parameters.Add("MO", MySqlDbType.VarChar, woid.Length).Value = woid;
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            int count = 0;
            string table = "SFCB.B_PRODUCT p,SFCR.T_WO_INFO w";
            string fieldlist = "p.partnumber,p.sortname,p.productname,p.productcolor,p.productdesc,p.other,p.productsn";
            string filter = "p.partnumber=w.partnumber and w.woId={0}";           
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("woId", woid);
           return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }

        //public System.Data.DataSet GetAllProduct()
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "select partnumber,sortname,productname,productcolor,productdesc,other from SFCB.B_PRODUCT";
        //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //}
        #endregion
    }
}
