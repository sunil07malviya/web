using System;
using System.Web.UI;

using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class ReasonMaster : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private ReasonInfo myReasonInfo = null;
        private void Page_Load(object sender, EventArgs e)
        {
            btnReason.Status = "";

            if (!IsPostBack)
            {
                pDispHeading();

                myReasonInfo = SQLServerDAL.Masters.ReasonType.GetReasonInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myReasonInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myReasonInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new ReasonInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnReason.MenuID = MenuID;
                btnReason.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myReasonInfo = (Model.Masters.ReasonInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtReason.Text = myReasonInfo.Reason;
                txtDesc.Text = myReasonInfo.Desc;
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myReasonInfo = (ReasonInfo)ViewState[TRAN_ID_KEY];

            try
            {
                myReasonInfo.Reason = WebComponents.CleanString.InputText(txtReason.Text, txtReason.MaxLength);
                myReasonInfo.Desc = WebComponents.CleanString.InputText(txtDesc.Text, txtDesc.MaxLength);

                ViewState[TRAN_ID_KEY] = myReasonInfo;
            }
            catch
            {
                throw;
            }
        }
        private void pClearControls()
        {
            txtReason.Text = "";
            txtDesc.Text = "";
        }
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuId=" + MenuID;
            Response.Redirect(url, true);
        }
        private void pUnLockControls()
        {
            txtReason.ReadOnly = false;
            txtDesc.ReadOnly = false;
        }
        private void pLockControls()
        {
            txtReason.ReadOnly = true;
            txtDesc.ReadOnly = true;
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
                    btnReason.Status = "Deletion not possible...!";
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

                myReasonInfo = (ReasonInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.ReasonType.Insert(myReasonInfo);
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

                myReasonInfo = (ReasonInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.ReasonType.Update(myReasonInfo);
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
                myReasonInfo = (ReasonInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.ReasonType.Delete(myReasonInfo.SlNo);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myReasonInfo = (ReasonInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.ReasonType.blnCheckDelete(myReasonInfo.Reason))
                return true;
            else
                return false;
        }
        private bool fblnValidEntry()
        {
            bool lblnReturnValue = true;

            if (Page.IsValid)
            {
                if (txtReason.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Reason is required!";
                    lblnReturnValue = false;
                }
                if (txtDesc.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Desc is required!";
                    lblnReturnValue = false;
                }
                if (lblnReturnValue)
                {
                    myReasonInfo = (ReasonInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myReasonInfo.SlNo == 0)
                    {
                        lblMessage.Text = "ResponseTypeinfo not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.ReasonType.blnCheckReasonInfo(myReasonInfo))
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
                btnReason.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
