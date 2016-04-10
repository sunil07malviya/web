using System;
using System.Web.UI;

using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class Country : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private CountryInfo myCountryInfo = null;

        private void Page_Load(object sender, System.EventArgs e)
        {
            btnCountry.Status = "";

            if (!IsPostBack)
            {
                pDispHeading();

                myCountryInfo = SQLServerDAL.Masters.Country.GetCountryInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myCountryInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myCountryInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new CountryInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnCountry.MenuID = MenuID;
                btnCountry.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myCountryInfo = (Model.Masters.CountryInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtName.Text = myCountryInfo.Name;
                txtShortName.Text = myCountryInfo.ShortName;
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myCountryInfo = (CountryInfo)ViewState[TRAN_ID_KEY];

            try
            {
                myCountryInfo.ShortName = WebComponents.CleanString.InputText(txtShortName.Text, txtShortName.MaxLength);
                myCountryInfo.Name = WebComponents.CleanString.InputText(txtName.Text, txtName.MaxLength);

                ViewState[TRAN_ID_KEY] = myCountryInfo;
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
                    btnCountry.Status = "Deletion not possible...!";
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

                myCountryInfo = (CountryInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Country.Insert(myCountryInfo);
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

                myCountryInfo = (CountryInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Country.Update(myCountryInfo);
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
                myCountryInfo = (CountryInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Country.Delete(myCountryInfo.SlNo);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myCountryInfo = (CountryInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.Country.blnCheckforDelete(myCountryInfo.SlNo))
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
                    myCountryInfo = (CountryInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myCountryInfo.SlNo == 0)
                    {
                        lblMessage.Text = "Country not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.Country.blnCheckCountry(myCountryInfo))
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
                btnCountry.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
