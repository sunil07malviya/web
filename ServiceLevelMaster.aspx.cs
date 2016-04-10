using System;
using System.Web.UI;

using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class ServiceLevelMaster : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private ServiceLevelCodeInfo myServiceLevelCodeInfo = null;
        private void Page_Load(object sender, EventArgs e)
        {

            btnServiceLevelCode.Status = "";

            if (!IsPostBack)
            {
                pDispHeading();

                myServiceLevelCodeInfo = SQLServerDAL.Masters.ServiceLevel.GetServiceLevelInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myServiceLevelCodeInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myServiceLevelCodeInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new ServiceLevelCodeInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnServiceLevelCode.MenuID = MenuID;
                btnServiceLevelCode.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myServiceLevelCodeInfo = (Model.Masters.ServiceLevelCodeInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtCode.Text = myServiceLevelCodeInfo.ServiceLevelCode;
                txtDesc.Text = myServiceLevelCodeInfo.ServiceLevelDescription;
                
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myServiceLevelCodeInfo = (ServiceLevelCodeInfo)ViewState[TRAN_ID_KEY];

            try
            {
                myServiceLevelCodeInfo.ServiceLevelCode = WebComponents.CleanString.InputText(txtCode.Text, txtCode.MaxLength);
                myServiceLevelCodeInfo.ServiceLevelDescription = WebComponents.CleanString.InputText(txtDesc.Text, txtDesc.MaxLength);

                ViewState[TRAN_ID_KEY] = myServiceLevelCodeInfo;
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
                    btnServiceLevelCode.Status = "Deletion not possible...!";
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

                myServiceLevelCodeInfo = (ServiceLevelCodeInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.ServiceLevel.Insert(myServiceLevelCodeInfo);
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

                myServiceLevelCodeInfo = (ServiceLevelCodeInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.ServiceLevel.Update(myServiceLevelCodeInfo);
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
                myServiceLevelCodeInfo = (ServiceLevelCodeInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.ServiceLevel.Delete(myServiceLevelCodeInfo.ServiceSlNo);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myServiceLevelCodeInfo = (ServiceLevelCodeInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.ServiceLevel.blnCheckDelete(myServiceLevelCodeInfo.ServiceSlNo))
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
                    lblMessage.Text = "Service level code is required!";
                    lblnReturnValue = false;
                }
                if (txtDesc.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Description is required!";
                    lblnReturnValue = false;
                }

                if (lblnReturnValue)
                {
                    myServiceLevelCodeInfo = (ServiceLevelCodeInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myServiceLevelCodeInfo.ServiceSlNo == 0)
                    {
                        lblMessage.Text = "ServiceLevelCodeInfo not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.ServiceLevel.blnCheckServiceLevelInfo(myServiceLevelCodeInfo))
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
                btnServiceLevelCode.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
