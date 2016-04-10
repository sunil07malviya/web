using System;
using System.Web.UI;

using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class CHACharges : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private CHAChargesInfo myCHAChargesInfo = null;

        private void Page_Load(object sender, System.EventArgs e)
        {
            btnCHACharges.Status = "";

            if (!IsPostBack)
            {
                pDispHeading();

                myCHAChargesInfo = SQLServerDAL.Masters.CHACharges.GetCHAChargesInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myCHAChargesInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myCHAChargesInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new CHAChargesInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnCHACharges.MenuID = MenuID;
                btnCHACharges.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myCHAChargesInfo = (Model.Masters.CHAChargesInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtName.Text = myCHAChargesInfo.Name;
                txtShortName.Text = myCHAChargesInfo.ShortName;
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myCHAChargesInfo = (CHAChargesInfo)ViewState[TRAN_ID_KEY];

            try
            {
                myCHAChargesInfo.ShortName = WebComponents.CleanString.InputText(txtShortName.Text, txtShortName.MaxLength);
                myCHAChargesInfo.Name = WebComponents.CleanString.InputText(txtName.Text, txtName.MaxLength);

                ViewState[TRAN_ID_KEY] = myCHAChargesInfo;
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
        }
        private void pLockControls()
        {
            txtShortName.ReadOnly = true;
            txtName.ReadOnly = true;
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
                    btnCHACharges.Status = "Deletion not possible...!";
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

                myCHAChargesInfo = (CHAChargesInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.CHACharges.Insert(myCHAChargesInfo);
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

                myCHAChargesInfo = (CHAChargesInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.CHACharges.Update(myCHAChargesInfo);
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
                myCHAChargesInfo = (CHAChargesInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.CHACharges.Delete(myCHAChargesInfo.SlNo);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myCHAChargesInfo = (CHAChargesInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.CHACharges.blnCheckforDelete(myCHAChargesInfo.SlNo))
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
                    myCHAChargesInfo = (CHAChargesInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myCHAChargesInfo.SlNo == 0)
                    {
                        lblMessage.Text = "CHACharges not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.CHACharges.blnCheckCHACharges(myCHAChargesInfo))
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
                btnCHACharges.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
