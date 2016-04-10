using System;
using System.Web.UI;

using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class FailureTypeMaster : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private FailureTypeInfo myFailureTypeInfo = null;

        private void Page_Load(object sender, EventArgs e)
        {
            btnFailureType.Status = "";


            if (!IsPostBack)
            {
                pDispHeading();

                myFailureTypeInfo = SQLServerDAL.Masters.Failuretype.GetFailureType(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myFailureTypeInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myFailureTypeInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new FailureTypeInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnFailureType.MenuID = MenuID;
                btnFailureType.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myFailureTypeInfo = (Model.Masters.FailureTypeInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtFailureType.Text = myFailureTypeInfo.FailureType;
                txtCodeName.Text = myFailureTypeInfo.CodeName;
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myFailureTypeInfo = (FailureTypeInfo)ViewState[TRAN_ID_KEY];
            
            try
            {
                myFailureTypeInfo.FailureType = WebComponents.CleanString.InputText(txtFailureType.Text, txtFailureType.MaxLength);
                myFailureTypeInfo.CodeName = WebComponents.CleanString.InputText(txtCodeName.Text, txtCodeName.MaxLength);

                ViewState[TRAN_ID_KEY] = myFailureTypeInfo;
            }
            catch
            {
                throw;
            }
        }
        private void pClearControls()
        {
            txtFailureType.Text = "";
            txtCodeName.Text = "";
        }
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuId=" + MenuID;
            Response.Redirect(url, true);
        }
        private void pUnLockControls()
        {
            txtFailureType.ReadOnly = false;
            txtCodeName.ReadOnly = false;
        }
        private void pLockControls()
        {
            txtFailureType.ReadOnly = true;
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
                    btnFailureType.Status = "Deletion not possible...!";
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

                myFailureTypeInfo = (FailureTypeInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Failuretype.Insert(myFailureTypeInfo);
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

                myFailureTypeInfo = (FailureTypeInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Failuretype.Update(myFailureTypeInfo);
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
                myFailureTypeInfo = (FailureTypeInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Failuretype.Delete(myFailureTypeInfo.SlNo);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myFailureTypeInfo = (FailureTypeInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.Failuretype.blnCheckDelete(myFailureTypeInfo.FailureType))
                return true;
            else
                return false;
        }
        private bool fblnValidEntry()
        {
            bool lblnReturnValue = true;

            if (Page.IsValid)
            {
                if (txtFailureType.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    
                    lblMessage.Text = "Failure Type is required!";
                    lblnReturnValue = false;
                }
                
                if (txtCodeName.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    
                    lblMessage.Text = "Code Name is required!";
                    lblnReturnValue = false;
                }
                else
                if (lblnReturnValue)
                {
                    myFailureTypeInfo = (FailureTypeInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myFailureTypeInfo.SlNo == 0)
                    {
                        lblMessage.Text = "FailureTypeInfo not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.Failuretype.blnCheckFailuretype(myFailureTypeInfo))
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
                btnFailureType.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
