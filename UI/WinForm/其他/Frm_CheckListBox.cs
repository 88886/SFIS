using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SFIS_V2
{
    public partial class Frm_CheckListBox : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_CheckListBox(Office2007Form frm ,object obj)
        {
            InitializeComponent();
            mFrm = frm;
            MyObj = obj;
        }

        Office2007Form mFrm;
        object MyObj;
        private void Frm_CheckListBox_Load(object sender, EventArgs e)
        {
            if (mFrm is Frm_WO_Update)
            {
                this.Text = "选择要清除的序列号类型";
                ChkListData.Items.Clear();
                List<string> LsSerial = (List<string>)MyObj;
                foreach (string str in LsSerial)
                {
                    ChkListData.Items.Add(str);
                }
                for (int i = 0; i < this.ChkListData.Items.Count; i++)
                {
                    foreach (string str in (mFrm as Frm_WO_Update).Serial_Type.Split(','))
                    {
                        if (this.ChkListData.Items[i].ToString() == str)
                        {
                            this.ChkListData.SetItemChecked(i, true);
                        }
                    }
                }
            }
            if (mFrm is Frm_ReworkPD)
            {
                if ((mFrm as Frm_ReworkPD).CheckListBoxFlag == 0)
                {
                    this.Text = "选择站位";
                    ChkListData.Items.Clear();
                    List<string> LsCraft = (List<string>)MyObj;
                    foreach (string str in LsCraft)
                    {
                        ChkListData.Items.Add(str);
                    }
                    for (int i = 0; i < this.ChkListData.Items.Count; i++)
                    {
                        foreach (string str in (mFrm as Frm_ReworkPD).List_CarftInfo.Items)
                        {
                            if (this.ChkListData.Items[i].ToString() == str)
                            {
                                this.ChkListData.SetItemChecked(i, true);
                            }
                        }
                    }
                }
                if ((mFrm as Frm_ReworkPD).CheckListBoxFlag == 1)
                {
                    this.Text = "选择需要清除的类型";
                    ChkListData.Items.Clear();
                    List<string> LsSerial = (List<string>)MyObj;
                    foreach (string str in LsSerial)
                    {
                        ChkListData.Items.Add(str);
                    }
                    for (int i = 0; i < this.ChkListData.Items.Count; i++)
                    {
                        foreach (string str in (mFrm as Frm_ReworkPD).ListKeyParts.Items)
                        {
                            if (this.ChkListData.Items[i].ToString() == str)
                            {
                                this.ChkListData.SetItemChecked(i, true);
                            }
                        }
                    }
                }
            }
        }

        private void imbt_ok_Click(object sender, EventArgs e)
        {
            if (mFrm is Frm_WO_Update)
            {
                string serial = string.Empty;
                for (int i = 0; i < this.ChkListData.CheckedItems.Count; i++)
                {
                    if (i == this.ChkListData.CheckedItems.Count - 1)
                        serial += this.ChkListData.CheckedItems[i].ToString();
                    else
                        serial += this.ChkListData.CheckedItems[i].ToString() + ",";
                }
                (mFrm as Frm_WO_Update).Serial_Type = serial;
                this.DialogResult = DialogResult.OK;
            }
            if (mFrm is Frm_ReworkPD)
            {
                if ((mFrm as Frm_ReworkPD).CheckListBoxFlag == 0)
                {
                    (mFrm as Frm_ReworkPD).List_CarftInfo.Items.Clear();
                    for (int i = 0; i < this.ChkListData.CheckedItems.Count; i++)
                    {
                        (mFrm as Frm_ReworkPD).List_CarftInfo.Items.Add(this.ChkListData.CheckedItems[i].ToString());
                    }
                }
                if ((mFrm as Frm_ReworkPD).CheckListBoxFlag == 1)
                {
                    (mFrm as Frm_ReworkPD).ListKeyParts.Items.Clear();
                    for (int i = 0; i < this.ChkListData.CheckedItems.Count; i++)
                    {
                        (mFrm as Frm_ReworkPD).ListKeyParts.Items.Add(this.ChkListData.CheckedItems[i].ToString());
                    }
                }
                this.DialogResult = DialogResult.OK;
            }

        }

        private void imbt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void calendarView1_ItemClick(object sender, EventArgs e)
        {

        }
    }
}