using System;
using System.Web.UI;
using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class SymptomGroupMaster : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private SymptomGroupInfo mySymptomGroupInfo = null;
        private void Page_Load(object sender, EventArgs e)
        {
            btnSymptomGroup.Status = "";

            if (!IsPostBack)
            {
                pDispHeading();

                mySymptomGroupInfo = SQLServerDAL.Masters.SymptomGroup.GetSymptomGroupInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (mySymptomGroupInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = mySymptomGroupInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new SymptomGroupInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnSymptomGroup.MenuID = MenuID;
                btnSymptomGroup.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            mySymptomGroupInfo = (Model.Masters.SymptomGroupInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtSName.Text = mySymptomGroupInfo.GroupSName;
                txtName.Text = mySymptomGroupInfo.GroupName;
                txtGrpDef.Text = mySymptomGroupInfo.GroupDefinition;
                if (mySymptomGroupInfo.BB10Exclusive == "Y")
                    ddlBB10Ex.SelectedValue = "1";
                else
                    ddlBB10Ex.SelectedValue = "0";
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            mySymptomGroupInfo = (SymptomGroupInfo)ViewState[TRAN_ID_KEY];

            try
            {
                
                mySymptomGroupInfo.GroupSName = WebComponents.CleanString.InputText(txtSName.Text, txtSName.MaxLength);
                mySymptomGroupInfo.GroupName = WebComponents.CleanString.InputText(txtName.Text, txtName.MaxLength);
                mySymptomGroupInfo.GroupDefinition = WebComponents.CleanString.InputText(txtGrpDef.Text, txtGrpDef.MaxLength);
                mySymptomGroupInfo.BB10Exclusive = WebComponents.CleanString.InputText(ddlBB10Ex.SelectedItem.ToString(), ddlBB10Ex.SelectedValue.Length);

                ViewState[TRAN_ID_KEY] = mySymptomGroupInfo;
            }
            catch
            {
                throw;
            }
        }
        private void pClearControls()
        {
            txtSName.Text = "";
            txtName.Text = "";
            txtGrpDef.Text = "";
            ddlBB10Ex.SelectedValue = "0";
        }
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuId=" + MenuID;
            Response.Redirect(url, true);
        }
        private void pUnLockControls()
        {
            txtSName.ReadOnly = false;
            txtName.ReadOnly = false;
            txtGrpDef.ReadOnly = false;
            ddlBB10Ex.Enabled = true;
        }
        private void pLockControls()
        {
            txtSName.ReadOnly = true;
            txtName.ReadOnly = true;
            txtGrpDef.ReadOnly = true;
            ddlBB10Ex.Enabled = false;
        }
        protected void Page_EditButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Modify";
            pUnLockControls();
        }
        protected void Page_DeleteButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Delete";
            pLockControls();
        }
        protected void Page_CancelButton(object sender, EventArgs e)
        {
            pBacktoGrid();
        }
        protected void Page_SubmitButton(object sender, EventArgs e)
        {
            string lstrStatus = ViewState[STATUS_KEY].ToString();

            pMapControls();

            if (lstrStatus.Equals("Delete"))
            {
                if (fblnValidDelete())
                {
                    pDelete();
                    pBacktoGrid();
                    return;
                }
                else
                {
                    btnSymptomGroup.Status = "Deletion not possible...!";
                    return;
                }
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
        private void pSave()
        {
            try
            {
                pMapControls();

                mySymptomGroupInfo = (SymptomGroupInfo)ViewState[TRAN_ID_KEY];


                SQLServerDAL.Masters.SymptomGroup.Insert(mySymptomGroupInfo);
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
                pMapControls();

                mySymptomGroupInfo = (SymptomGroupInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.SymptomGroup.Update(mySymptomGroupInfo);
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
                mySymptomGroupInfo = (SymptomGroupInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.SymptomGroup.Delete(mySymptomGroupInfo.SLNO);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            mySymptomGroupInfo = (SymptomGroupInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.SymptomGroup.blnCheckDelete(mySymptomGroupInfo.GroupSName))
                return true;
            else
                return false;
        }
        private bool fblnValidEntry()
        {
            bool lblnReturnValue = true;

            if (Page.IsValid)
            {
                if (txtSName.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Short Name is required!";
                    lblnReturnValue = false;
                }
                if (txtName.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Name is required!";
                    lblnReturnValue = false;
                }
                if (txtGrpDef.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Group Definition is required!";
                    lblnReturnValue = false;
                }
                if (ddlBB10Ex.SelectedValue == "" && lblnReturnValue)
                {
                    lblMessage.Text = "BB10Exclusive is required!";
                    lblnReturnValue = false;
                }
                if (lblnReturnValue)
                {
                    mySymptomGroupInfo = (SymptomGroupInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && mySymptomGroupInfo.SLNO == 0)
                    {
                        lblMessage.Text = "SymptomGroupInfo not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.SymptomGroup.blnCheckSymptomGroupInfo(mySymptomGroupInfo))
                        lblnReturnValue = true;
                    else
                    {
                        lblMessage.Text = "Duplicate Entry...!";
                        lblnReturnValue = false;
                    }
                }
            }
            else
            {
                btnSymptomGroup.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
