using System;
using System.Web.UI;

using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class CourierMaster : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private CourierInfo myCourierInfo = null;

        private void Page_Load(object sender, EventArgs e)
        {
            btnCourier.Status = "";

            if (!IsPostBack)
            {
                pDispHeading();

                myCourierInfo = SQLServerDAL.Masters.Courier.GetActivity(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myCourierInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myCourierInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new CourierInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnCourier.MenuID = MenuID;
                btnCourier.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myCourierInfo = (Model.Masters.CourierInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtCode.Text = myCourierInfo.ShortName;
                txtDesc.Text = myCourierInfo.Description;
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myCourierInfo = (CourierInfo)ViewState[TRAN_ID_KEY];

            try
            {
                myCourierInfo.ShortName = WebComponents.CleanString.InputText(txtCode.Text, txtCode.MaxLength);
                myCourierInfo.Description = WebComponents.CleanString.InputText(txtDesc.Text, txtDesc.MaxLength);

                ViewState[TRAN_ID_KEY] = myCourierInfo;
            }
            catch
            {
                throw;
            }
        }
        private void pClearControls()
        {
            txtCode.Text = "";
            txtDesc.Text = "";
        }
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuId=" + MenuID;
            Response.Redirect(url, true);
        }
        private void pUnLockControls()
        {
            txtCode.ReadOnly = false;
            txtDesc.ReadOnly = false;
        }
        private void pLockControls()
        {
            txtCode.ReadOnly = true;
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
                    btnCourier.Status = "Deletion not possible...!";
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

                myCourierInfo = (CourierInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Courier.Insert(myCourierInfo);
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

                myCourierInfo = (CourierInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Courier.Update(myCourierInfo);
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
                myCourierInfo = (CourierInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Courier.Delete(myCourierInfo.SlNo);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myCourierInfo = (CourierInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.Courier.blnCheckDelete(myCourierInfo.SlNo))
                return true;
            else
                return false;
        }
        private bool fblnValidEntry()
        {
            bool lblnReturnValue = true;

            if (Page.IsValid)
            {
                if (txtCode.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Short Name is required!";
                    lblnReturnValue = false;
                }
                if (txtDesc.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Name is required!";
                    lblnReturnValue = false;
                }
                if (lblnReturnValue)
                {
                    myCourierInfo = (CourierInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myCourierInfo.SlNo == 0)
                    {
                        lblMessage.Text = "CourierInfo not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.Courier.blnCheckActivity(myCourierInfo))
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
                btnCourier.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
