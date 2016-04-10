using System;
using System.Web.UI;

using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class Authority : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private AuthorityInfo myAuthorityInfo = null;

        private void Page_Load(object sender, System.EventArgs e)
        {
            btnAuthority.Status = "";

            if (!IsPostBack)
            {
                pDispHeading();

                myAuthorityInfo = SQLServerDAL.Masters.Authority.GetAuthorityInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myAuthorityInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myAuthorityInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new AuthorityInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnAuthority.MenuID = MenuID;
                btnAuthority.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myAuthorityInfo = (Model.Masters.AuthorityInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtName.Text = myAuthorityInfo.Name;
                txtShortName.Text = myAuthorityInfo.ShortName;
                txtAddress1.Text = myAuthorityInfo.Address1;
                txtAddress2.Text = myAuthorityInfo.Address2;
                txtAddress3.Text = myAuthorityInfo.Address3;
                txtAddress4.Text = myAuthorityInfo.Address4;
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myAuthorityInfo = (AuthorityInfo)ViewState[TRAN_ID_KEY];

            try
            {
                myAuthorityInfo.ShortName = WebComponents.CleanString.InputText(txtShortName.Text, txtShortName.MaxLength);
                myAuthorityInfo.Name = WebComponents.CleanString.InputText(txtName.Text, txtName.MaxLength);
                myAuthorityInfo.Address1 = WebComponents.CleanString.InputText(txtAddress1.Text, txtAddress1.MaxLength);
                myAuthorityInfo.Address2 = WebComponents.CleanString.InputText(txtAddress2.Text, txtAddress2.MaxLength);
                myAuthorityInfo.Address3 = WebComponents.CleanString.InputText(txtAddress3.Text, txtAddress3.MaxLength);
                myAuthorityInfo.Address4 = WebComponents.CleanString.InputText(txtAddress4.Text, txtAddress4.MaxLength);

                ViewState[TRAN_ID_KEY] = myAuthorityInfo;
            }
            catch
            {
                throw;
            }
        }
        private void pClearControls()
        {
            txtShortName.Text = "";
            txtName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtAddress4.Text = "";
        }
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuId=" + MenuID;
            Response.Redirect(url, true);
        }
        private void pUnLockControls()
        {
            txtShortName.ReadOnly = false;
            txtName.ReadOnly = false;
            txtAddress1.ReadOnly = false;
            txtAddress2.ReadOnly = false;
            txtAddress3.ReadOnly = false;
            txtAddress4.ReadOnly = false;
        }
        private void pLockControls()
        {
            txtShortName.ReadOnly = true;
            txtName.ReadOnly = true;
            txtAddress1.ReadOnly = true;
            txtAddress2.ReadOnly = true;
            txtAddress3.ReadOnly = true;
            txtAddress4.ReadOnly = true;
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
                    btnAuthority.Status = "Deletion not possible...!";
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

                myAuthorityInfo = (AuthorityInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Authority.Insert(myAuthorityInfo);
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

                myAuthorityInfo = (AuthorityInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Authority.Update(myAuthorityInfo);
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
                myAuthorityInfo = (AuthorityInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Authority.Delete(myAuthorityInfo.SlNo);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myAuthorityInfo = (Model.Masters.AuthorityInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.Authority.blnValidateDelete(myAuthorityInfo.SlNo, BranchType == "" ? "STP" : BranchType))
                return true;
            else
                return false;
        }
        private bool fblnValidEntry()
        {
            bool lblnReturnValue = true;

            if (Page.IsValid)
            {
                if (txtName.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Name is required!";
                    lblnReturnValue = false;
                }
                if (txtShortName.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Short Name is required!";
                    lblnReturnValue = false;
                }
                if (lblnReturnValue)
                {
                    myAuthorityInfo = (AuthorityInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myAuthorityInfo.SlNo == 0)
                    {
                        lblMessage.Text = "Authority not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.Authority.blnCheckAuthority(myAuthorityInfo))
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
                btnAuthority.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
