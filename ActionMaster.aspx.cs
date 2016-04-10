using System;
using System.Web.UI;

using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class ActionMaster : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private ActionInfo myActionInfo = null;

        private void Page_Load(object sender, EventArgs e)
        {
            
            btnActionCodes.Status = "";


            if (!IsPostBack)
            {
                pDispHeading();

                myActionInfo = SQLServerDAL.Masters.Action.getActionInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myActionInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myActionInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new ActionInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnActionCodes.MenuID = MenuID;
                btnActionCodes.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myActionInfo = (Model.Masters.ActionInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtAction.Text = myActionInfo.Action;
                txtDesc.Text = myActionInfo.Desc;
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myActionInfo = (ActionInfo)ViewState[TRAN_ID_KEY];

            try
            {
                myActionInfo.Action = WebComponents.CleanString.InputText(txtAction.Text, txtAction.MaxLength);
                myActionInfo.Desc = WebComponents.CleanString.InputText(txtDesc.Text, txtDesc.MaxLength);

                ViewState[TRAN_ID_KEY] = myActionInfo;
            }
            catch
            {
                throw;
            }
        }
        private void pClearControls()
        {
            txtAction.Text = "";
            txtDesc.Text = "";
        }
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuId=" + MenuID;
            Response.Redirect(url, true);
        }
        private void pUnLockControls()
        {
            txtAction.ReadOnly = false;
            txtDesc.ReadOnly = false;
        }
        private void pLockControls()
        {
            txtAction.ReadOnly = true;
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
                    btnActionCodes.Status = "Deletion not possible...!";
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

                myActionInfo = (ActionInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Action.Insert(myActionInfo);
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

                myActionInfo = (ActionInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Action.Update(myActionInfo);
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
                myActionInfo = (ActionInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Action.Delete(myActionInfo.SlNo);
            }
            catch
            {
                throw;
            }
        }
         private bool fblnValidDelete()
        {
            myActionInfo = (ActionInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.Action.blnCheckforDelete(myActionInfo.Action))
                return true;
            else
                return false;
        }
        private bool fblnValidEntry()
        {
            bool lblnReturnValue = true;

            if (Page.IsValid)
            {
                if (txtAction.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Action is required!";
                    lblnReturnValue = false;
                }
                if (txtDesc.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Description is required!";
                    lblnReturnValue = false;
                }
                if (lblnReturnValue)
                {
                    myActionInfo = (ActionInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myActionInfo.SlNo == 0)
                    {
                        lblMessage.Text = "ActionInfo not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.Action.blnCheckAction(myActionInfo))
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
                btnActionCodes.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
   
}
