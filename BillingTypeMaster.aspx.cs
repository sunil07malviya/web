using System;
using System.Web.UI;

using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{

    public partial class BillingTypeMaster : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private BillingTypeInfo myBillingTypeInfo = null;
        private void Page_Load(object sender, EventArgs e)
        {
            btnBillingType.Status = "";


            if (!IsPostBack)
            {
                pDispHeading();

                myBillingTypeInfo = SQLServerDAL.Masters.BillingType.getBillingTypeInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myBillingTypeInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myBillingTypeInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new BillingTypeInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnBillingType.MenuID = MenuID;
                btnBillingType.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }

        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myBillingTypeInfo = (Model.Masters.BillingTypeInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtBillingType.Text = myBillingTypeInfo.BillingType;
                txtCodeName.Text = myBillingTypeInfo.CodeName;
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myBillingTypeInfo = (BillingTypeInfo)ViewState[TRAN_ID_KEY];

            try
            {
                myBillingTypeInfo.BillingType = WebComponents.CleanString.InputText(txtBillingType.Text, txtBillingType.MaxLength);
                myBillingTypeInfo.CodeName = WebComponents.CleanString.InputText(txtCodeName.Text, txtCodeName.MaxLength);

                ViewState[TRAN_ID_KEY] = myBillingTypeInfo;
            }
            catch
            {
                throw;
            }
        }
        private void pClearControls()
        {
            txtBillingType.Text = "";
            txtCodeName.Text = "";
        }
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuId=" + MenuID;
            Response.Redirect(url, true);
        }
        private void pUnLockControls()
        {
            txtBillingType.ReadOnly = false;
            txtCodeName.ReadOnly = false;
        }
        private void pLockControls()
        {
            txtBillingType.ReadOnly = true;
            txtCodeName.ReadOnly = true;
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
                    btnBillingType.Status = "Deletion not possible...!";
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

                myBillingTypeInfo = (BillingTypeInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.BillingType.Insert(myBillingTypeInfo);
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

                myBillingTypeInfo = (BillingTypeInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.BillingType.Update(myBillingTypeInfo);
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
                myBillingTypeInfo = (BillingTypeInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.BillingType.Delete(myBillingTypeInfo.SlNo);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myBillingTypeInfo = (BillingTypeInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.BillingType.blnCheckforDelete(myBillingTypeInfo.BillingType))
                return true;
            else
                return false;
        }
        private bool fblnValidEntry()
        {
            bool lblnReturnValue = true;

            if (Page.IsValid)
            {
                if (txtBillingType.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Billing Type is required!";
                    lblnReturnValue = false;
                }
                if (txtCodeName.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "CodeName is required!";
                    lblnReturnValue = false;
                }
                if (lblnReturnValue)
                {
                    myBillingTypeInfo = (BillingTypeInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myBillingTypeInfo.SlNo == 0)
                    {
                        lblMessage.Text = "BillingTypeInfo not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.BillingType.blnCheckBillingType(myBillingTypeInfo))
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
                btnBillingType.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
