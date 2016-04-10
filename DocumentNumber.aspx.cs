using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ISPL.CSC.Model.Masters;
using ISPL.CSC.SQLServerDAL.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class DocumentNumber : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private Model.AutoNumberHdrInfo myAutoNumberHdrInfo = null;

        private void Page_Load(object sender, System.EventArgs e)
        {
            //bcDocNumber.UpdProgressClientID = prgDocNumber.ClientID;
            bcDocNumber.Status = "";

            bcDocNumber.EditButtonClick += new EventHandler(this.bcDocNumber_EditButton);
            bcDocNumber.CancelButtonClick += new EventHandler(this.bcDocNumber_CancelButton);
            bcDocNumber.DeleteButtonClick += new EventHandler(this.bcDocNumber_DeleteButton);
            bcDocNumber.SubmitButtonClick += new EventHandler(this.bcDocNumber_SubmitButton);

            LOVTrans.LOVAfterClick += new EventHandler(this.LOVTrans_AfterClick);

            if (!IsPostBack)
            {
                int SlNo = 0;
                if (Request[TRAN_ID_KEY] != null)
                    SlNo = Convert.ToInt32(Request[TRAN_ID_KEY]);

                myAutoNumberHdrInfo = SQLServerDAL.AutoNumberHdr.GetAutoNumberHdrInfo(SlNo);

                pSetUserControls();
                pDispHeading();

                if (myAutoNumberHdrInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myAutoNumberHdrInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new Model.AutoNumberHdrInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pUnLockControls();
                    pClearControls();
                }
                bcDocNumber.MenuID = MenuID;
                bcDocNumber.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pDispHeading()
        {
            LOVViewInfo myLOVView = LOVView.GetLOVViewInfo(MenuID);
            lblHeading.InnerText = myLOVView.Caption;
        }
        private void pUnLockControls()
        {
            chkEF.Enabled = true;
            chkMultiple.Enabled = true;
            txtCounter.ReadOnly = false;
            txtPrefix.ReadOnly = false;
            txtSuffix.ReadOnly = false;
            LOVTrans.ReadOnly = ViewState[STATUS_KEY].ToString() == "Add" ? false : true;
            ddlFormat.Enabled = true;
            ddlField.Enabled = true;
            //txtLength.Enabled = true;
            //txtChar.Enabled = true;
        }
        private void pLockControls()
        {
            chkEF.Enabled = false;
            chkMultiple.Enabled = false;
            txtCounter.ReadOnly = true;
            txtPrefix.ReadOnly = true;
            txtSuffix.ReadOnly = true;
            LOVTrans.ReadOnly = true;
            ddlFormat.Enabled = false;
            ddlField.Enabled = false;
            //txtLength.Enabled = false;
            //txtChar.Enabled = false;
        }
        private void pBindControls()
        {
            myAutoNumberHdrInfo = (Model.AutoNumberHdrInfo)ViewState[TRAN_ID_KEY];

            LOVTrans.ReadOnly = true;
            LOVTrans.strFirstColumn = myAutoNumberHdrInfo.Menu.Name.Replace("&", "");
            LOVTrans.strLastColumn = myAutoNumberHdrInfo.Menu.SlNo.ToString();
            pBindDDLs();

            ddlField.ClearSelection();
            if (myAutoNumberHdrInfo.GFieldDescSlNo != 0)
            {
                ListItem lstItem = ddlField.Items.FindByValue(myAutoNumberHdrInfo.GFieldDescSlNo.ToString());
                if (lstItem != null)
                    lstItem.Selected = true;
            }

            pBindDDLFormat(myAutoNumberHdrInfo.GFieldDescSlNo);

            ddlFormat.ClearSelection();
            if (myAutoNumberHdrInfo.Format.Length != 0)
            {
                ListItem lstItem = ddlFormat.Items.FindByText(myAutoNumberHdrInfo.Format);
                if (lstItem != null)
                    lstItem.Selected = true;
            }

            txtPrefix.Text = myAutoNumberHdrInfo.Prefix;
            txtSuffix.Text = myAutoNumberHdrInfo.Suffix;
            txtCounter.Text = myAutoNumberHdrInfo.Counter.ToString();
            chkEF.Checked = myAutoNumberHdrInfo.EnableFlag == "Y" ? true : false;
            chkMultiple.Checked = myAutoNumberHdrInfo.MultipleFlag == "Y" ? true : false;
           // txtLength.Text = myAutoNumberHdrInfo.Length.ToString();
            //txtChar.Text = myAutoNumberHdrInfo.Character;
        }
        private void pMapControls()
        {
            myAutoNumberHdrInfo = (Model.AutoNumberHdrInfo)ViewState[TRAN_ID_KEY];

            myAutoNumberHdrInfo.BranchID = LoginBranchID;       

            if (LOVTrans.strLastColumn.Length == 0)
                myAutoNumberHdrInfo.Menu = null;
            else
            {
                myAutoNumberHdrInfo.Menu = SQLServerDAL.Masters.Menu.GetMenuInfo(Convert.ToInt32(LOVTrans.strLastColumn));
            }

            myAutoNumberHdrInfo.Prefix = WebComponents.CleanString.InputText(txtPrefix.Text, txtPrefix.MaxLength);
            myAutoNumberHdrInfo.Suffix = WebComponents.CleanString.InputText(txtSuffix.Text, txtSuffix.MaxLength);
            myAutoNumberHdrInfo.Counter = Convert.ToInt32(txtCounter.Text);
            if (chkEF.Checked == true)
                myAutoNumberHdrInfo.EnableFlag = "Y";
            else
                myAutoNumberHdrInfo.EnableFlag = "N";

            if (chkMultiple.Checked == true)
                myAutoNumberHdrInfo.MultipleFlag = "Y";
            else
                myAutoNumberHdrInfo.MultipleFlag = "N";
            //Commented and Modified by saikumar on 01-July-2011 ( for fixed length and prefix char)
            //myAutoNumberHdrInfo.Format = myAutoNumberHdrInfo.Prefix + myAutoNumberHdrInfo.Counter.ToString() + myAutoNumberHdrInfo.Suffix;
            int iPrefix = txtPrefix.Text.Trim().Length;
            int iSuffix = txtSuffix.Text.Trim().Length;
            int iCount = txtCounter.Text.Length;
                       

            //int iDiff = iLength - (iPrefix + iSuffix + iCount);

            //if (iLength > 0 && iDiff > 0)
            //{
            //    char ch = '0';
            //    if (txtChar.Text.Length > 0)
            //        ch = txtChar.Text.ToCharArray()[0];

            //    string strPrefix = txtPrefix.Text.PadRight((iPrefix + iDiff), ch);

            //    myAutoNumberHdrInfo.Format = strPrefix + myAutoNumberHdrInfo.Counter.ToString() + myAutoNumberHdrInfo.Suffix;
            //}
            //else
            //{
            //    myAutoNumberHdrInfo.Format = myAutoNumberHdrInfo.Prefix + myAutoNumberHdrInfo.Counter.ToString() + myAutoNumberHdrInfo.Suffix;
            //}

            //myAutoNumberHdrInfo.Length = Convert.ToInt32(txtLength.Text);
            //myAutoNumberHdrInfo.Character = WebComponents.CleanString.InputText(txtChar.Text, txtChar.MaxLength);

            //Commented and Modification end.

            myAutoNumberHdrInfo.GFieldDescSlNo = Convert.ToInt32(ddlField.SelectedValue.ToString()); //SelectedIndex;

            ViewState[TRAN_ID_KEY] = myAutoNumberHdrInfo;
        }
        private void pClearControls()
        {
            chkEF.Checked = false;
            chkMultiple.Checked = false;
            txtCounter.Text = "";
            txtPrefix.Text = "";
            txtCounter.Text = "";
           // txtLength.Text = "";
           // txtChar.Text = "0";
            LOVTrans.ClearAll();
            ddlField.ClearSelection();
            ddlFormat.ClearSelection();
        }
        private void pSetUserControls()
        {
                LOVTrans.Query = "select replace(g_menu_name, '&', '') as 'Transaction', g_menu_slno from g_menu where "
                              + "  g_menu_disable='N' and G_MENU_FLAG='TR'";

            LOVTrans.TextWidth = 144;

            LOVTrans.TabIndex = 1;
            ddlField.TabIndex = 2;
            ddlField.TabIndex = 3;
            txtPrefix.TabIndex = 4;
            txtCounter.TabIndex = 5;
            txtSuffix.TabIndex = 6;
            chkEF.TabIndex = 7;
            chkMultiple.TabIndex = 8;
          //  txtLength.TabIndex = 9;
          //  txtChar.TabIndex = 10;
            bcDocNumber.TabIndex = 11;
        }

        private void pBindDDLs()
        {
            string lstrQuery = "SELECT g_fielddesc_fldnomen, g_fielddesc_slno FROM g_fielddesc WHERE g_fielddesc_gmenuslno=" + LOVTrans.strLastColumn + " and g_fielddesc_enableflag='Y' Order By g_fielddesc_fldnomen";
            DataTable myTable = SQLServerDAL.General.GetDataTable(lstrQuery);

            ddlField.Items.Clear();
            foreach (DataRow objDR in myTable.Rows)
            {
                ddlField.Items.Add(new ListItem(objDR["g_fielddesc_fldnomen"].ToString(), objDR["g_fielddesc_slno"].ToString()));
            }
        }
        private void pBindDDLFormat(int MenuID)
        {
            myAutoNumberHdrInfo = (Model.AutoNumberHdrInfo)ViewState[TRAN_ID_KEY];

            string lstrQuery = "SELECT TM_JOBFRMT_FORMAT, TM_JOBFRMT_SLNO FROM TM_EOU_JOBFRMT WHERE TM_JOBFRMT_GMENUSLNO = " + myAutoNumberHdrInfo.Menu.SlNo.ToString() + " AND TM_JOBFRMT_GFIELDDESCSLNO = " + MenuID.ToString();

            DataTable myTable = SQLServerDAL.General.GetDataTable(lstrQuery);

            ddlFormat.Items.Clear();
            foreach (DataRow objDR in myTable.Rows)
            {
                ddlFormat.Items.Add(new ListItem(objDR["tm_jobfrmt_format"].ToString(), objDR["tm_jobfrmt_slno"].ToString()));
            }
        }
        private void SetValues(int id)
        {
            if (ddlField.Items.Count > 1)
            {
                string lstrQuery = "SELECT g_fielddesc_fldnomen, g_fielddesc_slno FROM g_fielddesc WHERE g_fielddesc_gmenuslno=" + LOVTrans.strLastColumn + " and g_fielddesc_enableflag='Y' Order By g_fielddesc_fldnomen";
                DataTable myTable = SQLServerDAL.General.GetDataTable(lstrQuery);
                ddlFormat.Items.Clear();
            }
        }
        private void bcDocNumber_EditButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Modify";
            pUnLockControls();
        }
        private void bcDocNumber_DeleteButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Delete";
            pLockControls();
        }
        private void bcDocNumber_CancelButton(object sender, EventArgs e)
        {
            pBacktoGrid();
        }
        private void pSave()
        {
            try
            {
                myAutoNumberHdrInfo = (Model.AutoNumberHdrInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.AutoNumberHdr.Insert(myAutoNumberHdrInfo);
            }
            catch
            {
                throw;
            }
        }
        private void pUpdate()
        {
            try
            {
                myAutoNumberHdrInfo = (Model.AutoNumberHdrInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.AutoNumberHdr.Update(myAutoNumberHdrInfo);
            }
            catch
            {
                throw;
            }
        }

        private void pDelete()
        {
            try
            {
                myAutoNumberHdrInfo = (Model.AutoNumberHdrInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.AutoNumberHdr.DeleteByHdrID(myAutoNumberHdrInfo.SlNo);
            }
            catch
            {
                throw;
            }
        }
        private void bcDocNumber_SubmitButton(object sender, EventArgs e)
            {
            string lstrStatus = ViewState[STATUS_KEY].ToString();

            pMapControls();

            if (lstrStatus.Equals("Delete"))
            {
                pDelete();
                pBacktoGrid();
                return;
            }

            if (fblnValidEntry())
            {
                if ((lstrStatus.Equals("New") || lstrStatus.Equals("Add")))
                    pSave();

                if ((lstrStatus.Equals("Edit") || lstrStatus.Equals("Modify")))
                    pUpdate();

                pBacktoGrid();
            }
        }
        private bool fblnValidEntry()
        {
            if (Page.IsValid)
            {
                myAutoNumberHdrInfo = (Model.AutoNumberHdrInfo)ViewState[TRAN_ID_KEY];

                if (ViewState[STATUS_KEY].Equals("Modify") && myAutoNumberHdrInfo.SlNo == 0)
                {
                    bcDocNumber.Status = "Auto Number not found...!";
                    return false;
                }

                if (SQLServerDAL.AutoNumberHdr.blnCheckAutoNumberHdr(myAutoNumberHdrInfo))
                    return true;
                else
                {
                    bcDocNumber.Status = "At a Time one Format for a transaction should be Enable. check your Entry...!";
                    return false;
                }
            }
            else
            {
                bcDocNumber.Status = "Check ur entries...!";
                return false;
            }
        }
        private void LOVTrans_AfterClick(object sender, EventArgs e)
        {
            if (LOVTrans.strLastColumn.Length != 0)
            {
                pBindDDLs();

                if (ddlField.Items.Count >= 1)
                    SetValues(Convert.ToInt32(LOVTrans.strLastColumn));
            }
        }
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuId=" + MenuID;
            Response.Redirect(url, true);
        }

        private void chkEF_CheckedChanged(object sender, System.EventArgs e)
        {
            if ((chkEF.Checked == true) && LOVTrans.strLastColumn.Length != 0)
            {
                txtCounter.Enabled = true;
                txtPrefix.Enabled = true;
                txtSuffix.Enabled = true;
                chkMultiple.Enabled = true;

            }
        }

        private void txtPrefix_TextChanged(object sender, System.EventArgs e)
        {

            //ddlFormat.Items.Add (txtPrefix.Text  + txtCounter.Text  + txtSuffix.Text  );  
            pFormat();
        }

        private void txtCounter_TextChanged(object sender, System.EventArgs e)
        {
            //ddlFormat.Items.Add (txtPrefix.Text  + txtCounter.Text  + txtSuffix.Text  );  
            pFormat();
        }

        private void txtSuffix_TextChanged(object sender, System.EventArgs e)
        {
            //ddlFormat.Items.Add (txtPrefix.Text  + txtCounter.Text  + txtSuffix.Text );  
            pFormat();
        }

        private void txtLength_TextChanged(object sender, System.EventArgs e)
        {
            pFormat();
        }

        private void txtChar_TextChanged(object sender, System.EventArgs e)
        {
            pFormat();
        }


        private void pFormat()
        {
            int iPrefix = txtPrefix.Text.Trim().Length;
            int iSuffix = txtSuffix.Text.Trim().Length;
            int iCount = txtCounter.Text.Length;
            //if(txtLength.Text!=null && txtLength.Text!="")
            //int iLength = 0;// Convert.ToInt32(txtLength.Text);
            //int iDiff = iLength - (iPrefix + iSuffix + iCount);

            //if (iLength > 0 && iDiff > 0)
            //{
            //    char ch = '0';
            //   // if (txtChar.Text.Length > 0)
            //        //ch = txtChar.Text.ToCharArray()[0];

            //    string strPrefix = txtPrefix.Text.PadRight((iPrefix + iDiff), ch);
            //    ddlFormat.Items.Add(strPrefix + txtCounter.Text + txtSuffix.Text);
            //}
            //else
            //{
                ddlFormat.Items.Add(txtPrefix.Text + txtCounter.Text + txtSuffix.Text);
            //}
        }
    }
}