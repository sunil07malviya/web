using System;
using System.Web.UI;

using ISPL.CSC.Model;
using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class Currency : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private CurrencyInfo myCurrencyInfo = null;

        private void Page_Load(object sender, System.EventArgs e)
        {
            btnCurrency.Status = "";

            if (!IsPostBack)
            {
                pDispHeading();

                myCurrencyInfo = SQLServerDAL.Masters.Currency.GetCurrencyInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myCurrencyInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myCurrencyInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new CurrencyInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnCurrency.MenuID = MenuID;
                btnCurrency.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myCurrencyInfo = (Model.Masters.CurrencyInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtName.Text = myCurrencyInfo.Name;
                txtShortName.Text = myCurrencyInfo.ShortName;
                txtSubCurrency.Text = myCurrencyInfo.SubCurrency;
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myCurrencyInfo = (CurrencyInfo)ViewState[TRAN_ID_KEY];

            try
            {
                myCurrencyInfo.ShortName = WebComponents.CleanString.InputText(txtShortName.Text, txtShortName.MaxLength);
                myCurrencyInfo.Name = WebComponents.CleanString.InputText(txtName.Text, txtName.MaxLength);
                myCurrencyInfo.SubCurrency = WebComponents.CleanString.InputText(txtSubCurrency.Text, txtSubCurrency.MaxLength);

                ViewState[TRAN_ID_KEY] = myCurrencyInfo;
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
                    btnCurrency.Status = "Deletion not possible...!";
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

                myCurrencyInfo = (CurrencyInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Currency.Insert(myCurrencyInfo);
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

                myCurrencyInfo = (CurrencyInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Currency.Update(myCurrencyInfo);
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
                myCurrencyInfo = (CurrencyInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Currency.Delete(myCurrencyInfo.SlNo);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myCurrencyInfo = (CurrencyInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.Currency.blnCheckforDelete(myCurrencyInfo.SlNo))
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
                if (txtSubCurrency.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Sub Currency is required!";
                    lblnReturnValue = false;
                }
                if (lblnReturnValue)
                {
                    myCurrencyInfo = (CurrencyInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myCurrencyInfo.SlNo == 0)
                    {
                        lblMessage.Text = "Currency not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.Currency.blnCheckCurrency(myCurrencyInfo))
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
                btnCurrency.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
