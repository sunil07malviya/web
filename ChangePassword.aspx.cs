using System;
using System.Data;
using System.Web.UI;

using ISPL.CSC.SQLServerDAL.Masters;

namespace ISPL.CSC.Web
{
    public partial class ChangePassword : BasePage
    {
        private const string STATUS_KEY = "Status";
        private const string TRAN_ID_KEY = "ID";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.bcChangePassword.EditButtonVisible = false;
            this.bcChangePassword.DeleteButtonVisible = false;

            if (!IsPostBack)
            {
                ViewState[STATUS_KEY] = "Add";

                bcChangePassword.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pMapControls()
        {
            LoginUserInfo.Password = WebComponents.Encryption.fnEncryptString(WebComponents.CleanString.InputText(txtConfirmPassword.Text, txtConfirmPassword.MaxLength), "352EOU1368");
            LoginUserInfo.PasswordUpdate = System.DateTime.Today.Date;

            ViewState[TRAN_ID_KEY] = LoginUserInfo;
        }
        protected void bcChangePassword_SubmitButtonClick(object sender, EventArgs e)
        {
            string lstrStatus = ViewState[STATUS_KEY].ToString();

            pMapControls();

            if (fblnValidEntry())
            {
                if (lstrStatus.Equals("Add"))
                    pUpdate();
            }
            // Response.Redirect("SignIn.aspx");
        }
        private void pUpdate()
        {
            try
            {
                LoginUserInfo = (Model.Masters.UserInfo)ViewState[TRAN_ID_KEY];

                pMapControls();

                SQLServerDAL.Masters.User.Update(Session["DB_KEY"].ToString(), LoginUserInfo);
                ProcessFlow.AccountController accountController = new ProcessFlow.AccountController();
                accountController.LogOut();
                
                //WebComponents.MessageBox.Show("Change Password - Complete.\\nYour password has been changed.\\nYour current session is closed.\\nPlease click ok to login again, using your new password", this);
                Response.Write("<script>alert('Change Password - Complete.\\nYour password has been changed.\\nYour current session is closed.\\nPlease click ok to login again, using your new password');</script>");

                //Response.Write("<script> window.parent.reload(true);</script>");
                Response.Write("<script>window.parent.location.href = 'SignIn.aspx';</script>");

                //Response.Write("<script> window.parent.location.reload(true);</script>");

                //Response.Write("<script> window.parent.close(); </script>");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw;
            }
        }
        private bool fblnValidEntry()
        {
            bool lblnValue = true;

            if (Page.IsValid)
            {
                LoginUserInfo = (Model.Masters.UserInfo)ViewState[TRAN_ID_KEY];

                string lstrQuery = string.Empty;

                lstrQuery = " select gm_users_name,GM_USERS_PWD,gm_users_id"
                    + " from gm_users with (ROWLOCK)"
                    + " where GM_USERS_COMPANY_BRANCH_CODE = " + LoginUserInfo.BranchID + ""
                    + " AND GM_USERS_NAME = '" + LoginUserInfo.Name.ToString() + "'";

                DataTable myTable = SQLServerDAL.General.GetDataTable(lstrQuery);

                if (myTable.Rows.Count > 0)
                {
                    DataRow objDR = myTable.Rows[0];
                    string objUserName = objDR["gm_users_name"].ToString();
                    string objPassWord = objDR["GM_USERS_PWD"].ToString();
                    string objUserId = objDR["gm_users_id"].ToString();

                    string DecryptedPassword = Web.WebComponents.Encryption.fnDecryptString(objPassWord.ToString(), "352EOU1368");

                    if (txtOldPassword.Text != DecryptedPassword)
                    //if(WebComponents.Misc.fnEncryptString(txtExistingPwd.Text, "352EOU1368") != objPassWord.ToString())
                    {
                        bcChangePassword.Status = "Existing Password is Not Correct...!";
                        lblnValue = false;
                    }
                }
                if (txtOldPassword.Text == txtNewPassword.Text)
                {
                    bcChangePassword.Status = "Existing Password and New Password is same, please change the new Password...!";
                    lblnValue = false;
                }
                if (txtNewPassword.Text != txtConfirmPassword.Text)
                {
                    bcChangePassword.Status = "New Password and Conform PassWord Not Matching...!";
                    lblnValue = false;
                }
            }
            else
            {
                bcChangePassword.Status = "Check ur entries...!";
                lblnValue = false;
            }
            return lblnValue;
        }
        protected void bcChangePassword_CancelButtonClick(object sender, EventArgs e)
        {
            txtOldPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";

            bcChangePassword.Status = "";
        }
    }
}
