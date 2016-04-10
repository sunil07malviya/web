using System;
using System.Web.UI;

using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class ModelMaster : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private ModelInfo myModelInfo = null;
        private void Page_Load(object sender, EventArgs e)
        {
            btnModel.Status = "";

            if (!IsPostBack)
            {
                pDispHeading();

                myModelInfo = SQLServerDAL.Masters.ModelMaster.GetModelInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myModelInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myModelInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new ModelInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnModel.MenuID = MenuID;
                btnModel.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }

        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myModelInfo = (Model.Masters.ModelInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtCode.Text = myModelInfo.Code;
                txtDesc.Text = myModelInfo.Desc;
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myModelInfo = (ModelInfo)ViewState[TRAN_ID_KEY];

            try
            {
                myModelInfo.Code = WebComponents.CleanString.InputText(txtCode.Text, txtCode.MaxLength);
                myModelInfo.Desc = WebComponents.CleanString.InputText(txtDesc.Text, txtDesc.MaxLength);

                ViewState[TRAN_ID_KEY] = myModelInfo;
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
                    btnModel.Status = "Deletion not possible...!";
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

                myModelInfo = (ModelInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.ModelMaster.Insert(myModelInfo);
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

                myModelInfo = (ModelInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.ModelMaster.Update(myModelInfo);
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
                myModelInfo = (ModelInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.ModelMaster.Delete(myModelInfo.SlNo);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myModelInfo = (ModelInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.ModelMaster.blnCheckDelete(myModelInfo.Code))
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
                    myModelInfo = (ModelInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myModelInfo.SlNo == 0)
                    {
                        lblMessage.Text = "ModelInfo not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.ModelMaster.blnCheckModelInfo(myModelInfo))
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
                btnModel.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
