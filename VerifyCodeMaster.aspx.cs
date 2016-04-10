using System;
using System.Web.UI;

using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class VerifyCodeMaster : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private VerifyCodeInfo myVerifyCodeInfo = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            btnVerifyCode.Status = "";
            
           // this.LOVVerifyGroupSN.LOVAfterClick += new EventHandler(LOVVerifyGroupSN_LOVAfterClick);

            if (!IsPostBack)
            {
                
                pDispHeading();
                pSetUserControls();
                myVerifyCodeInfo = SQLServerDAL.Masters.VerifyCode.GetVerifyCodeInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myVerifyCodeInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myVerifyCodeInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new VerifyCodeInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnVerifyCode.MenuID = MenuID;
                btnVerifyCode.ButtonClicked = ViewState[STATUS_KEY].ToString();
                
            }
        }
        private void pSetUserControls()
        {

            LOVVerifyGroupSN.ClearAll();
            LOVVerifyGroupSN.Query = "select VerifyGroupCode [ShortName], VerifyGroupDescription [Description],verifyGroupSlno from VerifyGroup";

            LOVVerifyGroupSN.TextWidth = 100;
        } 
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myVerifyCodeInfo = (Model.Masters.VerifyCodeInfo)ViewState[TRAN_ID_KEY];
            try
            {
                VerifyGroupInfo myVerifyGroupInfo = SQLServerDAL.Masters.VerifyGroup.GetVerifyGroupInfo(myVerifyCodeInfo.VerifyGroupInfo.VerifyGroupSlNo);
                LOVVerifyGroupSN.strLastColumn = myVerifyCodeInfo.VerifyGroupInfo.VerifyGroupSlNo.ToString();
                LOVVerifyGroupSN.strFirstColumn = myVerifyCodeInfo.VerifyGroupInfo.VerifyGroupCode.ToString();
                txtCode.Text = myVerifyCodeInfo.VerifyCodeSName;
                txtDesc.Text = myVerifyCodeInfo.VerifyCodeDescription;
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myVerifyCodeInfo = (VerifyCodeInfo)ViewState[TRAN_ID_KEY];

            try
            {
                myVerifyCodeInfo.VerifyCodeSName = WebComponents.CleanString.InputText(txtCode.Text, txtCode.MaxLength);
                myVerifyCodeInfo.VerifyCodeDescription = WebComponents.CleanString.InputText(txtDesc.Text, txtDesc.MaxLength);
                if (LOVVerifyGroupSN.strLastColumn.Length == 0)
                    myVerifyCodeInfo.VerifyGroupInfo = null;
                else
                {
                    VerifyGroupInfo myVerifyGroupInfo = SQLServerDAL.Masters.VerifyGroup.GetVerifyGroupInfo(Convert.ToInt32(LOVVerifyGroupSN.strLastColumn));
                    myVerifyCodeInfo.VerifyGroupInfo = myVerifyGroupInfo;
                }
                ViewState[TRAN_ID_KEY] = myVerifyCodeInfo;
            }
            catch
            {
                throw;
            }
        }
        private void pClearControls()
        {
            LOVVerifyGroupSN.ClearAll();
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
            LOVVerifyGroupSN.ReadOnly = false;
            txtCode.ReadOnly = false;
            txtDesc.ReadOnly = false;
        }
        private void pLockControls()
        {
            LOVVerifyGroupSN.ReadOnly = true;
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
                    btnVerifyCode.Status = "Deletion not possible...!";
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

                myVerifyCodeInfo = (VerifyCodeInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.VerifyCode.Insert(myVerifyCodeInfo);
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

                myVerifyCodeInfo = (VerifyCodeInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.VerifyCode.Update(myVerifyCodeInfo);
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
                myVerifyCodeInfo = (VerifyCodeInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.VerifyCode.Delete(myVerifyCodeInfo.VerifyCodeSlNo);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myVerifyCodeInfo = (VerifyCodeInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.VerifyCode.blnCheckDelete(myVerifyCodeInfo.VerifyCodeSlNo))
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
                    myVerifyCodeInfo = (VerifyCodeInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myVerifyCodeInfo.VerifyCodeSlNo == 0)
                    {
                        lblMessage.Text = "VerifyGroupinfo not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.VerifyCode.blnCheckVerifyCodeInfo(myVerifyCodeInfo))
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
                btnVerifyCode.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
