using System;
using System.Web.UI;

using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class ResponseTypeMaster : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private ResponseTypeInfo myResponseTypeInfo = null;

        private void Page_Load(object sender, EventArgs e)
        {
            btnResponseType.Status = "";

            if (!IsPostBack)
            {
                pDispHeading();

                myResponseTypeInfo = SQLServerDAL.Masters.ResponseType.GetResponseType(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myResponseTypeInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myResponseTypeInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new ResponseTypeInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnResponseType.MenuID = MenuID;
                btnResponseType.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myResponseTypeInfo = (Model.Masters.ResponseTypeInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtCode.Text = myResponseTypeInfo.Code;
                txtType.Text = myResponseTypeInfo.Type;
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myResponseTypeInfo = (ResponseTypeInfo)ViewState[TRAN_ID_KEY];

            try
            {
                myResponseTypeInfo.Code = WebComponents.CleanString.InputText(txtCode.Text, txtCode.MaxLength);
                myResponseTypeInfo.Type = WebComponents.CleanString.InputText(txtType.Text, txtType.MaxLength);

                ViewState[TRAN_ID_KEY] = myResponseTypeInfo;
            }
            catch
            {
                throw;
            }
        }
        private void pClearControls()
        {
            txtCode.Text = "";
            txtType.Text = "";
        }
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuId=" + MenuID;
            Response.Redirect(url, true);
        }
        private void pUnLockControls()
        {
            txtCode.ReadOnly = false;
            txtType.ReadOnly = false;
        }
        private void pLockControls()
        {
            txtCode.ReadOnly = true;
            txtType.ReadOnly = true;
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
                    btnResponseType.Status = "Deletion not possible...!";
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

                myResponseTypeInfo = (ResponseTypeInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.ResponseType.Insert(myResponseTypeInfo);
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

                myResponseTypeInfo = (ResponseTypeInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.ResponseType.Update(myResponseTypeInfo);
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
                myResponseTypeInfo = (ResponseTypeInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.ResponseType.Delete(myResponseTypeInfo.SlNo);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myResponseTypeInfo = (ResponseTypeInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.ResponseType.blnCheckDelete(myResponseTypeInfo.Code))
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
                    lblMessage.Text = "Response Code is required!";
                    lblnReturnValue = false;
                }
                if (txtType.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Response Type is required!";
                    lblnReturnValue = false;
                }
                if (lblnReturnValue)
                {
                    myResponseTypeInfo = (ResponseTypeInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myResponseTypeInfo.SlNo == 0)
                    {
                        lblMessage.Text = "ResponseTypeinfo not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.ResponseType.blnCheckResponseType(myResponseTypeInfo))
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
                btnResponseType.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
