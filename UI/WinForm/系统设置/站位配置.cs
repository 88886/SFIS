using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class Frm_Station_Config : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Station_Config(Office2007Form Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }
        Office2007Form mFrm;

        int AfterSelect = -1;
        private void Frm_Station_Config_Load(object sender, EventArgs e)
        {
            //TreeNode a = new TreeNode();
            //a.Text = "A080101";
            //a.ImageIndex = 0;
            //a.SelectedImageIndex = 0;

            //TreeNode ab = new TreeNode();
            //ab.Text = "TEST";
            //ab.ImageIndex = 1;
            //ab.SelectedImageIndex = 1;
            //a.Nodes.Add(ab);

            //TreeNode abC = new TreeNode();
            //abC.Text = "PCBA_VI";
            //abC.ImageIndex = 2;
            //abC.SelectedImageIndex = 2;
            //a.Nodes[0].Nodes.Add(abC);

            //TreeNode abCD = new TreeNode();
            //abCD.Text = "PCBA_VI_1";
            //abCD.ImageIndex = 3;
            //abCD.SelectedImageIndex = 3;
            //a.Nodes[0].Nodes[0].Nodes.Add(abCD);

            //TreeNode abCDe = new TreeNode();
            //abCDe.Text = "PCBA_VI_2";
            //abCDe.ImageIndex = 3;
            //abCDe.SelectedImageIndex = 3;
            //a.Nodes[0].Nodes[0].Nodes.Add(abCDe); 

            //this.treeView_StationConfig.Nodes.Add(a);    
            Refresh_TreeView();
            Get_Line_Name();
            Get_Craft_Info();
        }

        private void Get_Line_Name()
        {
            DataTable dt = FrmBLL.publicfuntion.DataTableToSort( FrmBLL.ReleaseData.arrByteToDataTable( refWebtLineInfo.Instance.GetLineInfo(null,"LINEID")),"LINEID");
            foreach (DataRow dr in dt.Rows)
            {
                cbx_Line_name.Items.Add(dr[0].ToString());
            }
        }

        private void Get_Craft_Info()
        {
            List<string> Colnum = new List<string>();
            Colnum.Add("BEWORKSEG");
            DataTable dt1 = FrmBLL.ReleaseData.arrByteToDataTable(refWebtCraftInfo.Instance.GetAllCraftInfo());
            DataTable dt2 = FrmBLL.publicfuntion.DataTableDistinct(dt1, Colnum);
            foreach (DataRow dr in dt2.Rows)
            {
                cbx_section_name.Items.Add(dr[0].ToString());
            }
            DataTable dt3 = FrmBLL.publicfuntion.DataTableToSort(dt1, "CRAFTNAME");
            foreach (DataRow dr in dt3.Rows)
            {
                cbx__group_name.Items.Add(dr["CRAFTNAME"].ToString());
            }

        }

        TreeNode TnLine = null;
        TreeNode TnSection = null;
        TreeNode TnGroup = null;
        TreeNode TnStation = null;
        private void Refresh_TreeView()
        {
            int LineNodes = 0;
            int SectionNodes = 0;
            int GroupNodes = 0;

            DataTable dt =FrmBLL.ReleaseData.arrByteToDataTable( refWebtStationConfig.Instance.Get_StationConfig("0", "LINE_NAME,SECTION_NAME,GROUP_NAME,STATION_NAME"));
            if (dt.Rows.Count > 0)
            {          
                List<string> LsColnum = new List<string>();
                LsColnum.Add("LINE_NAME");
                DataTable dtLine = FrmBLL.publicfuntion.DataTableDistinct(FrmBLL.publicfuntion.DataTableToSort(dt, "LINE_NAME"), LsColnum);

                foreach (DataRow dr in dtLine.Rows)
                {
                    LineNodes++;
                    SectionNodes = 0;
                    TnLine = new TreeNode();//增减线体节点              
                    TnLine.Text = dr["LINE_NAME"].ToString();
                    TnLine.ImageIndex = 0;
                    TnLine.SelectedImageIndex=0;
                    DataTable dtSection = FrmBLL.publicfuntion.getNewTable(dt, string.Format("LINE_NAME='{0}'",dr["LINE_NAME"].ToString()));
                  
                        LsColnum.Clear();
                        LsColnum.Add("SECTION_NAME");
                        DataTable dtSectionDistinct = FrmBLL.publicfuntion.DataTableDistinct(FrmBLL.publicfuntion.DataTableToSort(dtSection, "SECTION_NAME"), LsColnum);
                        foreach (DataRow drSection in dtSectionDistinct.Rows)
                        {
                            SectionNodes++;
                            GroupNodes = 0;
                            TnSection = new TreeNode(); //增加SECTION_NAME节点
                            TnSection.Text = drSection["SECTION_NAME"].ToString();
                            TnSection.ImageIndex = 1;
                            TnSection.SelectedImageIndex = 1;
                            TnLine.Nodes.Add(TnSection);

                            DataTable dtGroupName = FrmBLL.publicfuntion.getNewTable(dt, string.Format("LINE_NAME='{0}' AND SECTION_NAME='{1}'", dr["LINE_NAME"].ToString(), drSection["SECTION_NAME"].ToString()));
                            LsColnum.Clear();
                            LsColnum.Add("GROUP_NAME");
                            DataTable dtGroupNameDistinct = FrmBLL.publicfuntion.DataTableDistinct(FrmBLL.publicfuntion.DataTableToSort(dtGroupName, "GROUP_NAME"), LsColnum);

                            foreach (DataRow drGroupName in dtGroupNameDistinct.Rows)
                            {
                                GroupNodes++;
                                TnGroup = new TreeNode();  //增加GROUP_NAME节点
                                TnGroup.Text = drGroupName["GROUP_NAME"].ToString();
                                TnGroup.ImageIndex = 2;
                                TnGroup.SelectedImageIndex = 2;
                                TnLine.Nodes[SectionNodes - 1].Nodes.Add(TnGroup);


                                DataTable dtStationName = FrmBLL.publicfuntion.getNewTable(dt, string.Format("LINE_NAME='{0}' AND SECTION_NAME='{1}' AND GROUP_NAME='{2}'", dr["LINE_NAME"].ToString(), drSection["SECTION_NAME"].ToString(), drGroupName["GROUP_NAME"].ToString()));
                                DataTable dtStort = FrmBLL.publicfuntion.DataTableToSort(dtStationName, "GROUP_NAME");
                                foreach (DataRow drStationName in dtStort.Rows)
                                {
                                    TnStation = new TreeNode(); //增加站位节点
                                    TnStation.Text = drStationName["STATION_NAME"].ToString();
                                    TnStation.ImageIndex = 3;
                                    TnStation.SelectedImageIndex = 3;
                                    TnLine.Nodes[SectionNodes - 1].Nodes[GroupNodes - 1].Nodes.Add(TnStation);
                                }

                            }
                        }
                        this.treeView_StationConfig.Nodes.Add(TnLine);                    
                }             
            }

            ButtonEnabled();
        }

        private void ButtonEnabled()
        {
            imbt_add.Enabled = false;
            imbt_delete.Enabled = false;
           
        }
        private void treeView_StationConfig_AfterSelect(object sender, TreeViewEventArgs e)
        {           
            txt_station_name.Text = string.Empty;
            ButtonEnabled();


            AfterSelect = e.Node.FullPath.Split('\\').Length;
            switch (AfterSelect)
           {
               case 1:               
                   cbx_Line_name.Text = e.Node.FullPath.Split('\\')[0];
                   imbt_add.Enabled = true;
                   imbt_delete.Enabled = true;
                   break;
               case 2:
                   cbx_Line_name.Text = e.Node.FullPath.Split('\\')[0];
                   cbx_section_name.Text = e.Node.FullPath.Split('\\')[1];
                  
                
                   imbt_delete.Enabled = true;
                
                   break;
               case 3:
                   cbx_Line_name.Text = e.Node.FullPath.Split('\\')[0];
                   cbx_section_name.Text = e.Node.FullPath.Split('\\')[1];
                
                   cbx__group_name.Text = e.Node.FullPath.Split('\\')[2];
                 
                   break;
               case 4:
                   cbx_Line_name.Text = e.Node.FullPath.Split('\\')[0];
                   cbx_section_name.Text = e.Node.FullPath.Split('\\')[1];
                   cbx__group_name.Text = e.Node.FullPath.Split('\\')[2];
                   txt_station_name.Text = e.Node.FullPath.Split('\\')[3];
               
                   break;

           }

        }

        private void treeView_StationConfig_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
           // txt_line_name.Text= e.Node.Text; 
           // txt_section_name.Text=e.
        }

        public List<string> lsData = null;
        public string SelectData = string.Empty;
        private void imbt_addline_Click(object sender, EventArgs e)
        {
          //  lsData = new List<string>();
          //DataTable dt =FrmBLL.ReleaseData.arrByteToDataTable(  refWebtLineInfo.Instance.GetAllLineInfo());
          //List<string> ListLineNode = GetDataNode();
          //  foreach ( DataRow dr in dt.Rows )
          //  {
          //      if (!ListLineNode.Contains(dr[0].ToString()))
          //          lsData.Add(dr[0].ToString());
          //  }
     
          //  Frm_Station_Config_SubForm fscs = new Frm_Station_Config_SubForm(this,"请选择线体");
          //  if (fscs.ShowDialog() == DialogResult.OK)
          //  {
                 
          //      TnLine = new TreeNode();
          //      TnLine.Text = SelectData;
          //      TnLine.ImageIndex = 0;
          //      TnLine.SelectedImageIndex = 0;
          //      this.treeView_StationConfig.Nodes.Add(TnLine);
          //  }
        }

        //private List<string> GetDataNode()
        //{
            
        //    List<string> LsStr = new List<string>();
        //    if ( AfterSelect == 1)
        //    {
        //        for (int x = 0; x < treeView_StationConfig.Nodes.Count; x++)
        //        {
        //            LsStr.Add(treeView_StationConfig.Nodes[x].Text);
        //        }
        //    }
        //    if (AfterSelect == 2)
        //    {
        //        for (int x = 0; x < treeView_StationConfig.Nodes.Count; x++)
        //        {
        //            if (treeView_StationConfig.Nodes[x].Text == txt_line_name.Text)
        //            {
        //                for (int y = 0; y < treeView_StationConfig.Nodes[x].Nodes.Count; y++)
        //                {
        //                    LsStr.Add(treeView_StationConfig.Nodes[x].Nodes[y].Text);
        //                }
        //            }
        //        }
        //    }
        //    if (AfterSelect == 3)
        //    {
        //        for (int x = 0; x < treeView_StationConfig.Nodes.Count; x++)
        //        {
        //            if (treeView_StationConfig.Nodes[x].Text == txt_line_name.Text)
        //            {
        //                for (int y = 0; y < treeView_StationConfig.Nodes[x].Nodes.Count; y++)
        //                {
        //                    for (int z = 0; z < treeView_StationConfig.Nodes[x].Nodes[y].Nodes.Count; z++)
        //                    {
        //                        LsStr.Add(treeView_StationConfig.Nodes[x].Nodes[y].Nodes[z].Text);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    if (AfterSelect == 3)
        //    {
        //        for (int x = 0; x < treeView_StationConfig.Nodes.Count; x++)
        //        {
        //            if (treeView_StationConfig.Nodes[x].Text == txt_line_name.Text)
        //            {
        //                for (int y = 0; y < treeView_StationConfig.Nodes[x].Nodes.Count; y++)
        //                {
        //                    for (int z = 0; z < treeView_StationConfig.Nodes[x].Nodes[y].Nodes.Count; z++)
        //                    {
        //                        for (int m = 0; m < treeView_StationConfig.Nodes[x].Nodes[y].Nodes[z].Nodes.Count; m++)
        //                        {
        //                            LsStr.Add(treeView_StationConfig.Nodes[x].Nodes[y].Nodes[z].Nodes[m].Text);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }


        //    return LsStr;
        //}

        private void imbt_section_name_Click(object sender, EventArgs e)
        {
            //lsData = new List<string>();
            //List<string> Colnum = new List<string>();
            //Colnum.Add("BEWORKSEG");
            //DataTable dt = FrmBLL.publicfuntion.DataTableDistinct(FrmBLL.ReleaseData.arrByteToDataTable(refWebtCraftInfo.Instance.GetAllCraftInfo()), Colnum);
            //List<string> ListLineNode = GetDataNode();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    if (!ListLineNode.Contains(dr[0].ToString()))
            //        lsData.Add(dr[0].ToString());
            //}

            //Frm_Station_Config_SubForm fscs = new Frm_Station_Config_SubForm(this, "请选择制程段");
            //if (fscs.ShowDialog() == DialogResult.OK)
            //{
            //    TreeNode tn = treeView_StationConfig.SelectedNode;
            //    tn.Nodes.Clear();

            //    TnSection = new TreeNode();        
            //    TnSection.Text =SelectData;
            //    TnSection.ImageIndex = 1;
            //    TnSection.SelectedImageIndex = 1;
            //    TnLine.Nodes.Add(TnSection);        
            //}
            
            
           
        }

        private void imbt_groupname_Click(object sender, EventArgs e)
        {
            //lsData = new List<string>();
         
            //DataTable dt = FrmBLL.publicfuntion.DataTableToSort(FrmBLL.ReleaseData.arrByteToDataTable(refWebtCraftInfo.Instance.GetAllCraftInfo()), "CRAFTNAME");
            //List<string> ListLineNode = GetDataNode();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    if (!ListLineNode.Contains(dr["CRAFTNAME"].ToString()))
            //        lsData.Add(dr["CRAFTNAME"].ToString());
            //}

            //Frm_Station_Config_SubForm fscs = new Frm_Station_Config_SubForm(this, "请选择途程");
            //if (fscs.ShowDialog() == DialogResult.OK)
            //{
            //    TreeNode tn = treeView_StationConfig.SelectedNode;
            //    tn.Nodes.Clear();

            //    TnGroup = new TreeNode();
            //    TnGroup.Text = SelectData;
            //    TnGroup.ImageIndex = 2;
            //    TnGroup.SelectedImageIndex = 2;
            //    TnSection.Nodes.Add(TnGroup);
            //}
        }

        private void imbt_stationname_Click(object sender, EventArgs e)
        {
            //lsData = new List<string>();

            //DataTable dt = FrmBLL.publicfuntion.DataTableToSort(FrmBLL.ReleaseData.arrByteToDataTable(refWebtCraftInfo.Instance.GetAllCraftInfo()), "CRAFTNAME");
            //List<string> ListLineNode = GetDataNode();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    if (!ListLineNode.Contains(dr["CRAFTNAME"].ToString()))
            //        lsData.Add(dr["CRAFTNAME"].ToString());
            //}

            //Frm_Station_Config_SubForm fscs = new Frm_Station_Config_SubForm(this, "请选择途程");
            //if (fscs.ShowDialog() == DialogResult.OK)
            //{
                TreeNode tn = treeView_StationConfig.SelectedNode;
                tn.Nodes.Clear();

                TnStation = new TreeNode();
                TnStation.Text = TnGroup.Text + (TnGroup.Nodes.Count+1).ToString();
                TnStation.ImageIndex = 3;
                TnStation.SelectedImageIndex = 3;
                TnGroup.Nodes.Add(TnStation); ;
           // }
        }
    }
}