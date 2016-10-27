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
    public partial class Frm_StationName : Office2007Form //Form
    {
        public Frm_StationName(Office2007Form Frm)
        {
            InitializeComponent();
            sFrm = Frm;
        }
        Office2007Form sFrm;
        private void Frm_StationName_Load(object sender, EventArgs e)
        {
            cb_line.Items.Clear();
            List<string> LsLine= new List<string>(refWebtLineInfo.Instance.GetLineList());
            foreach (string str in LsLine)
            {
                cb_line.Items.Add(str);
            }
            cb_line.SelectedIndex = 0;

            dgvstation.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvstation.DataSource = Get_All_Station();

            ReadForm();
            chkLine_Click(null,null);

        }

        private void ReadForm()
        {
            if (sFrm is Frm_TEST_MAIN_ONLY)
            {
               cb_line.Text=(sFrm as Frm_TEST_MAIN_ONLY).MyLine ;
               txt_group.Text=(sFrm as Frm_TEST_MAIN_ONLY).MYGROUP;
               txt_station.Text = (sFrm as Frm_TEST_MAIN_ONLY).MyStation;
               txt_section.Text=(sFrm as Frm_TEST_MAIN_ONLY).MySection;              
            }
            if (sFrm is Frm_INPUT_SN_FIRST)
            {
                cb_line.Text = (sFrm as Frm_INPUT_SN_FIRST).MyLine;
                txt_group.Text = (sFrm as Frm_INPUT_SN_FIRST).MYGROUP;
                txt_station.Text = (sFrm as Frm_INPUT_SN_FIRST).MyStation;
                txt_section.Text = (sFrm as Frm_INPUT_SN_FIRST).MySection;
            }
            if (sFrm is Frm_ReworkInput)
            {
                cb_line.Text = (sFrm as Frm_ReworkInput).MyLine;
                txt_group.Text = (sFrm as Frm_ReworkInput).MYGROUP;
                txt_station.Text = (sFrm as Frm_ReworkInput).MyStation;
                txt_section.Text = (sFrm as Frm_ReworkInput).MySection;
            }
            if (sFrm is Frm_TEST_INPUT)
            {
                cb_line.Text = (sFrm as Frm_TEST_INPUT).MyLine;
                txt_group.Text = (sFrm as Frm_TEST_INPUT).MYGROUP;
                txt_station.Text = (sFrm as Frm_TEST_INPUT).MyStation;
                txt_section.Text = (sFrm as Frm_TEST_INPUT).MySection;
            }
            if (sFrm is Frm_ColorBoxPrint)
            {
                cb_line.Text = (sFrm as Frm_ColorBoxPrint).MyLine;
                txt_group.Text = (sFrm as Frm_ColorBoxPrint).MYGROUP;
                txt_station.Text = (sFrm as Frm_ColorBoxPrint).MyStation;
                txt_section.Text = (sFrm as Frm_ColorBoxPrint).MySection;
            }
            if (sFrm is Frm_AssyFirst)
            {
                cb_line.Text = (sFrm as Frm_AssyFirst).MyLine;
                txt_group.Text = (sFrm as Frm_AssyFirst).MYGROUP;
                txt_station.Text = (sFrm as Frm_AssyFirst).MyStation;
                txt_section.Text = (sFrm as Frm_AssyFirst).MySection;
            }
            if (sFrm is Frm_SmtStockIn)
            {
                cb_line.Text = (sFrm as Frm_SmtStockIn).MyLine;
                txt_group.Text = (sFrm as Frm_SmtStockIn).MYGROUP;
                txt_station.Text = (sFrm as Frm_SmtStockIn).MyStation;
                txt_section.Text = (sFrm as Frm_SmtStockIn).MySection;
            }
            if (sFrm is Frm_Packing_Ctn)
            {
                cb_line.Text = (sFrm as Frm_Packing_Ctn).MyLine;
                txt_group.Text = (sFrm as Frm_Packing_Ctn).MYGROUP;
                txt_station.Text = (sFrm as Frm_Packing_Ctn).MyStation;
                txt_section.Text = (sFrm as Frm_Packing_Ctn).MySection;
            }
            if (sFrm is FrmPallet)
            {
                cb_line.Text = (sFrm as FrmPallet).MyLine;
                txt_group.Text = (sFrm as FrmPallet).MYGROUP;
                txt_station.Text = (sFrm as FrmPallet).MyStation;
                txt_section.Text = (sFrm as FrmPallet).MySection;
            }
            
        }
   
        private DataTable Get_All_Station()
        {
            DataTable dt =FrmBLL.ReleaseData.arrByteToDataTable( refWebtCraftInfo.Instance.GetAllCraftInfo()) ;
            DataTable temp = null;
            if (dt.Rows.Count > 0)
            {
                temp = FrmBLL.publicfuntion.getNewTable(dt, string.Format("TESTFLAG<>'{0}' and TESTFLAG<>'{1}'", "1","2"));
            }
            DataTable dtLine = new DataTable();
            dtLine.Columns.Add("SECTION", typeof(string));
            dtLine.Columns.Add("GROUP", typeof(string));
            dtLine.Columns.Add("STATION", typeof(string));
            foreach (DataRow dr in temp.Rows)
            {
                dtLine.Rows.Add(dr["BEWORKSEG"].ToString(), dr["CRAFTNAME"].ToString(), dr["CRAFTPARAMETERURL"].ToString());
            }
            return FrmBLL.publicfuntion.DataTableToSort(dtLine, "GROUP");
        }

        private void dgvstation_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {

            }
        }

        private void dgvstation_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txt_section.Text = dgvstation.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_group.Text = dgvstation.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_station.Text = dgvstation.Rows[e.RowIndex].Cells[2].Value.ToString();
             
            }
        }

        private void imbt_ok_Click(object sender, EventArgs e)
        {
            
                
            if (sFrm is Frm_TEST_MAIN_ONLY)
            {
                (sFrm as Frm_TEST_MAIN_ONLY).MyLine = cb_line.Text;
                (sFrm as Frm_TEST_MAIN_ONLY).MYGROUP = txt_group.Text;
                (sFrm as Frm_TEST_MAIN_ONLY).MyStation = txt_station.Text;
                (sFrm as Frm_TEST_MAIN_ONLY).MySection = txt_section.Text;
                (sFrm as Frm_TEST_MAIN_ONLY).SetStation();
            }
            if (sFrm is Frm_INPUT_SN_FIRST)
            {
                (sFrm as Frm_INPUT_SN_FIRST).MyLine = cb_line.Text;
                (sFrm as Frm_INPUT_SN_FIRST).MYGROUP = txt_group.Text;
                (sFrm as Frm_INPUT_SN_FIRST).MyStation = txt_station.Text;
                (sFrm as Frm_INPUT_SN_FIRST).MySection = txt_section.Text;
                (sFrm as Frm_INPUT_SN_FIRST).SetStation();
            }
            if (sFrm is Frm_ReworkInput)
            {
                (sFrm as Frm_ReworkInput).MyLine = cb_line.Text;
                (sFrm as Frm_ReworkInput).MYGROUP = txt_group.Text;
                (sFrm as Frm_ReworkInput).MyStation = txt_station.Text;
                (sFrm as Frm_ReworkInput).MySection = txt_section.Text;
                (sFrm as Frm_ReworkInput).SetStation();
            }
            if (sFrm is Frm_TEST_INPUT)
            {
                (sFrm as Frm_TEST_INPUT).MyLine = cb_line.Text;
                (sFrm as Frm_TEST_INPUT).MYGROUP = txt_group.Text;
                (sFrm as Frm_TEST_INPUT).MyStation = txt_station.Text;
                (sFrm as Frm_TEST_INPUT).MySection = txt_section.Text;
                (sFrm as Frm_TEST_INPUT).SetStation();
            }
            if (sFrm is Frm_ColorBoxPrint)
            {
                (sFrm as Frm_ColorBoxPrint).MyLine = cb_line.Text;
                (sFrm as Frm_ColorBoxPrint).MYGROUP = txt_group.Text;
                (sFrm as Frm_ColorBoxPrint).MyStation = txt_station.Text;
                (sFrm as Frm_ColorBoxPrint).MySection = txt_section.Text;
                (sFrm as Frm_ColorBoxPrint).SetStation();
            }
            if (sFrm is Frm_AssyFirst)
            {
                (sFrm as Frm_AssyFirst).MyLine = cb_line.Text;
                (sFrm as Frm_AssyFirst).MYGROUP = txt_group.Text;
                (sFrm as Frm_AssyFirst).MyStation = txt_station.Text;
                (sFrm as Frm_AssyFirst).MySection = txt_section.Text;
                (sFrm as Frm_AssyFirst).SetStation();
            }
            if (sFrm is Frm_SmtStockIn)
            {
                (sFrm as Frm_SmtStockIn).MyLine = cb_line.Text;
                (sFrm as Frm_SmtStockIn).MYGROUP = txt_group.Text;
                (sFrm as Frm_SmtStockIn).MyStation = txt_station.Text;
                (sFrm as Frm_SmtStockIn).MySection = txt_section.Text;
                (sFrm as Frm_SmtStockIn).SetStation();
            }
            if (sFrm is Frm_Packing_Ctn)
            {
                (sFrm as Frm_Packing_Ctn).MyLine = cb_line.Text;
                (sFrm as Frm_Packing_Ctn).MYGROUP = txt_group.Text;
                (sFrm as Frm_Packing_Ctn).MyStation = txt_station.Text;
                (sFrm as Frm_Packing_Ctn).MySection = txt_section.Text;
                (sFrm as Frm_Packing_Ctn).SetStation();
            }
            if (sFrm is FrmPallet)
            {
                (sFrm as FrmPallet).MyLine = cb_line.Text;
                (sFrm as FrmPallet).MYGROUP = txt_group.Text;
                (sFrm as FrmPallet).MyStation = txt_station.Text;
                (sFrm as FrmPallet).MySection = txt_section.Text;
                (sFrm as FrmPallet).SetStation();
            }
            this.DialogResult = DialogResult.OK;
        }

        private void imbt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkLine_Click(object sender, EventArgs e)
        {
             
            cb_line.Enabled = chkLine.Checked;
             
         
        }
    }
}
