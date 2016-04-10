using System;
using System.Web.UI;
using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{

    public partial class VerifyGroupMaster : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private VerifyGroupInfo myVerifyGroupInfo = null;
        private void Page_Load(object sender, EventArgs e)
        {
            btnVerifyGroup.Status = "";

            if (!IsPostBack)
            {
                pDispHeading();

                myVerifyGroupInfo = SQLServerDAL.Masters.VerifyGroup.GetVerifyGroupInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myVerifyGroupInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myVerifyGroupInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new VerifyGroupInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnVerifyGroup.MenuID = MenuID;
                btnVerifyGroup.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myVerifyGroupInfo = (Model.Masters.VerifyGroupInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtCode.Text = myVerifyGroupInfo.VerifyGroupCode;
                txtDesc.Text = myVerifyGroupInfo.VerifyGroupDescription;
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myVerifyGroupInfo = (VerifyGroupInfo)ViewState[TRAN_ID_KEY];

            try
            {
                myVerifyGroupInfo.VerifyGroupCode = WebComponents.CleanString.InputText(txtCode.Text, txtCode.MaxLength);
                myVerifyGroupInfo.VerifyGroupDescription = WebComponents.CleanString.InputText(txtDesc.Text, txtDesc.MaxLength);

                ViewState[TRAN_ID_KEY] = myVerifyGroupInfo;
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
                    btnVerifyGroup.Status = "Deletion not possible...!";
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

                myVerifyGroupInfo = (VerifyGroupInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.VerifyGroup.Insert(myVerifyGroupInfo);
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

                myVerifyGroupInfo = (VerifyGroupInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.VerifyGroup.Update(myVerifyGroupInfo);
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
                myVerifyGroupInfo = (VerifyGroupInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.VerifyGroup.Delete(myVerifyGroupInfo.VerifyGroupSlNo);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myVerifyGroupInfo = (VerifyGroupInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.VerifyGroup.blnCheckDelete(myVerifyGroupInfo.VerifyGroupSlNo))
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
                    lblMessage.Text = "Code is required!";
                    lblnReturnValue = false;
                }
                if (txtDesc.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Desc is required!";
                    lblnReturnValue = false;
                }
                if (lblnReturnValue)
                {
                    myVerifyGroupInfo = (VerifyGroupInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myVerifyGroupInfo.VerifyGroupSlNo == 0)
                    {
                        lblMessage.Text = "VerifyGroupinfo not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.VerifyGroup.blnCheckVerifyGroupInfo(myVerifyGroupInfo))
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
                btnVerifyGroup.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
