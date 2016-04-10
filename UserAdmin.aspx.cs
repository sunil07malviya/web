using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class UserAdmin : BasePage
    {
        private const string DB_KEY = "DB_KEY";
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";
        private const string MENU_ACCESS_KEY = "MENU_ACCESS_KEY";

        private static UserInfo myUserInfo = null;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                pDispHeading();
                pAddLocations();
                pSetUserControls();
                pDispHeading();

                myUserInfo = SQLServerDAL.Masters.User.GetUserDetails(Session[DB_KEY].ToString(), LoginBranchID, Request["ID"].ToString());

                if (myUserInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myUserInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                    btnUserAdmin.ButtonClicked = ViewState[STATUS_KEY].ToString();
                    btnUserAdmin.MenuID = MenuID;
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new Model.Masters.UserInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                    pUsers();
                    pBindGrid();
                }
                btnUserAdmin.ButtonClicked = ViewState[STATUS_KEY].ToString();
                btnUserAdmin.MenuID = MenuID;
            }
        }
        private void pUsers()
        {
            ViewState[TRAN_ID_KEY] = new Model.Masters.UserInfo();

            int nousers = 0;
            int usercount = 0;
            string lstrQuery = "select M_Company_Branch_NoOfUsers from m_company_branch where m_company_branch_code = " + LoginBranchID;
            DataTable myTable = null;
            myTable = SQLServerDAL.General.GetDataTable(lstrQuery);
            nousers = Convert.ToInt32(myTable.Rows[0]["M_Company_Branch_NoOfUsers"].ToString());

            if (nousers != 0)
            {
                string lstQuery = "select count(*) as cnt from gm_users where GM_USERS_COMPANY_BRANCH_CODE = " + LoginBranchID;
                DataTable myTable1 = null;
                myTable1 = SQLServerDAL.General.GetDataTable(lstQuery);
                usercount = Convert.ToInt32(myTable1.Rows[0]["cnt"].ToString());

                if (usercount >= nousers)
                {
                    pLockControls();
                    // btnUserAdmin.Status = "Current user exceeds Maximum Limit";
                    ////ViewState[STATUS_KEY] = "Add";
                    btnUserAdmin.Visible = false;
                    lblMessage.Text = "Can not create new user exceeds Maximum Limit";
                    //btnUserAdmin.SubmitButtonVisible = false;
                    return;
                }
                else
                {
                    // pBindGrid();
                    btnUserAdmin.ButtonClicked = ViewState[STATUS_KEY].ToString();
                    btnUserAdmin.MenuID = MenuID;
                }
            }
            else
            {
                // pBindGrid();
                btnUserAdmin.ButtonClicked = ViewState[STATUS_KEY].ToString();
                btnUserAdmin.MenuID = MenuID;
            }
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pSetUserControls()
        {
            if (LoginUserInfo.UserID.ToUpper().Equals("ADMIN"))
            {
                lblLicenses.Visible = true;
                lstLicenses.Visible = true;
            }
            else
            {
                lblLicenses.Visible = false;
                lstLicenses.Visible = false;
            }
        }
        private void pAddLocations()
        {
            string DBNames = ConfigurationSettings.AppSettings["DBNames"].ToString();
            string DBValues = ConfigurationSettings.AppSettings["DBValues"].ToString();
            string[] lstrDBNames = DBNames.Split(",".ToCharArray());
            string[] lstrDBValues = DBValues.Split(",".ToCharArray());
                        
            string lstrQuery = "SELECT M_COMPANY_BRANCH_SNAME, M_COMPANY_BRANCH_CODE FROM [{0}]..M_COMPANY_BRANCH ORDER BY M_COMPANY_BRANCH_SNAME ASC";
            for (int i = 0; i < lstrDBNames.Length; i++)
            {
                DataTable dt = SQLServerDAL.General.GetDataTable(String.Format(lstrQuery, lstrDBValues[i].ToString()));
                foreach (DataRow dr in dt.Rows)
                {
                    string lstrText = lstrDBNames[i].ToString() + "  -  " + dr[0].ToString();
                    string lstrValue = lstrDBValues[i].ToString() + "::" + dr[1].ToString();
                    ListItem lstNew = new ListItem(lstrText, lstrValue);
                    lstLicenses.Items.Add(lstNew);
                }
            }
            //string lstrQuery = "SELECT M_COMPANY_BRANCH_SNAME, M_COMPANY_BRANCH_CODE FROM M_COMPANY_BRANCH ORDER BY M_COMPANY_BRANCH_SNAME ASC";
            //DataTable dt = general.GetDataTable(lstrQuery);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    string lstrText = dr[0].ToString() + "  -  " + dr[0].ToString();
            //    string lstrValue = Session[DB_KEY].ToString() + "::" + dr[1].ToString();
            //    ListItem lstNew = new ListItem(lstrText, lstrValue);
            //    lstLicenses.Items.Add(lstNew);
            //}
        }
        private void pBindControls()
        {
            myUserInfo = (UserInfo)ViewState[TRAN_ID_KEY];

            myUserInfo = SQLServerDAL.Masters.User.GetUserDetails(Session["DB_KEY"].ToString(), myUserInfo.BranchID, myUserInfo.UserID);

            txtUserID.Text = myUserInfo.UserID;
            txtUserName.Text = myUserInfo.Name;
            txtPassword.Attributes.Add("value", Web.WebComponents.Encryption.fnDecryptString(myUserInfo.Password, "352EOU1368"));
            txtDesignation.Text = myUserInfo.Designation;
            txtEMail.Text = myUserInfo.EMailID;
            txtEnterpriseID.Text = myUserInfo.EnterpriseID;
            lblpasswordcreated.Text = myUserInfo.PasswordUpdate.ToString("dd-MMM-yyyy");

            ViewState[TRAN_ID_KEY] = myUserInfo;

            pBindLocations();
            pBindGrid();
        }
        private void pBindLocations()
        {
            myUserInfo = (UserInfo)ViewState[TRAN_ID_KEY];

            string lstrQuery = "SELECT GM_USERS_ID, GM_USERS_COMPANY_BRANCH_CODE FROM [{0}]..GM_USERS WHERE GM_USERS_ID = '" + myUserInfo.UserID + "' AND GM_USERS_COMPANY_BRANCH_CODE = {1}";
            foreach (ListItem lstItem in lstLicenses.Items)
            {
                string lstrDBName = lstItem.Value.Substring(0, lstItem.Value.IndexOf("::"));
                string lstrBrnCd = lstItem.Value.Substring(lstItem.Value.IndexOf("::") + 2);

                DataTable dt = SQLServerDAL.General.GetDataTable(String.Format(lstrQuery, lstrDBName, lstrBrnCd));

                if (dt.Rows.Count == 1)
                {
                    lstItem.Selected = true;
                }
            }
        }
        private void pBindGrid()
        {
            myUserInfo = (UserInfo)ViewState[TRAN_ID_KEY];

            StringBuilder lstrQuery = new StringBuilder();
            lstrQuery.Append(" SELECT	G_MENU_NAME, G_MENU_SLNO, G_MENU_PARENTCODE, GT_USERS_ID, 0 G_MENU_LEVEL, ");
            lstrQuery.Append(" 	        CASE ISNULL(GT_USERS_ID, 'N') WHEN 'N' THEN 'False' ELSE 'True' END GT_USERS_STATUSFLAG, ");
            lstrQuery.Append(" 	        CASE ISNULL(GT_USERS_ADDFLAG,'N') WHEN 'N' THEN 'False' ELSE 'True' END GT_USERS_ADDFLAG, ");
            lstrQuery.Append(" 	        CASE ISNULL(GT_USERS_EDITFLAG,'N') WHEN 'N' THEN 'False' ELSE 'True' END GT_USERS_EDITFLAG, ");
            lstrQuery.Append(" 	        CASE ISNULL(GT_USERS_DELETEFLAG,'N') WHEN 'N' THEN 'False' ELSE 'True' END GT_USERS_DELETEFLAG, ");
            lstrQuery.Append(" 	        CASE ISNULL(GT_USERS_PRINTFLAG,'N') WHEN 'N' THEN 'False' ELSE 'True' END GT_USERS_PRINTFLAG ");
            lstrQuery.Append(" FROM     G_MENU ");
            lstrQuery.Append("      	LEFT JOIN GT_USERS ON G_MENU_SLNO = GT_USERS_SLNO AND GT_USERS_ID = '" + myUserInfo.UserID + "' AND GT_USERS_COMPANY_BRANCH_CODE = " + myUserInfo.BranchID.ToString());
            lstrQuery.Append(" WHERE    G_MENU_SLNO >= 0 AND ISNULL(G_MENU_DISABLE,'N') = 'N'");

            DataTable myTable = SQLServerDAL.General.GetDataTable(lstrQuery.ToString());

            DataTable myNewTable = myTable.Clone();
            SetTable(ref myNewTable, myTable, "0");

            gvUsers.DataSource = myNewTable;
            gvUsers.DataBind();
            ViewState[MENU_ACCESS_KEY] = myNewTable;
            // pUsers();
        }
        private void pMapControls()
        {
            myUserInfo = (UserInfo)ViewState[TRAN_ID_KEY];
            DataTable myNewTable = (DataTable)ViewState[MENU_ACCESS_KEY];

            myUserInfo.BranchID = LoginUserInfo.BranchID;
            myUserInfo.GroupID = LoginUserInfo.GroupID;
            myUserInfo.UserID = Web.WebComponents.CleanString.InputText(txtUserID.Text, txtUserID.MaxLength);
            myUserInfo.Name = Web.WebComponents.CleanString.InputText(txtUserName.Text, txtUserName.MaxLength);
            myUserInfo.Password = Web.WebComponents.Encryption.fnEncryptString(Web.WebComponents.CleanString.InputText(txtPassword.Text, txtPassword.MaxLength), "352EOU1368");
            myUserInfo.Designation = Web.WebComponents.CleanString.InputText(txtDesignation.Text, txtDesignation.MaxLength);
            myUserInfo.EMailID = Web.WebComponents.CleanString.InputText(txtEMail.Text, txtEMail.MaxLength);
            myUserInfo.EnterpriseID = Web.WebComponents.CleanString.InputText(txtEnterpriseID.Text, txtEnterpriseID.MaxLength);
            myUserInfo.PasswordUpdate = System.DateTime.Today.Date; //.ToString("dd-MMM-yyyy");

            ViewState[TRAN_ID_KEY] = myUserInfo;

            for (int i = 0; i < gvUsers.Rows.Count; i++)
            {
                CheckBox status = (CheckBox)gvUsers.Rows[i].Cells[3].Controls[1];
                myNewTable.Rows[i]["GT_USERS_STATUSFLAG"] = status.Checked == true ? "Y" : "N";

                CheckBox add = (CheckBox)gvUsers.Rows[i].Cells[5].Controls[1];
                myNewTable.Rows[i]["GT_USERS_ADDFLAG"] = add.Checked == true ? "Y" : "N";

                CheckBox edit = (CheckBox)gvUsers.Rows[i].Cells[6].Controls[1];
                myNewTable.Rows[i]["GT_USERS_EDITFLAG"] = edit.Checked == true ? "Y" : "N";

                CheckBox delete = (CheckBox)gvUsers.Rows[i].Cells[7].Controls[1];
                myNewTable.Rows[i]["GT_USERS_DELETEFLAG"] = delete.Checked == true ? "Y" : "N";

                CheckBox print = (CheckBox)gvUsers.Rows[i].Cells[8].Controls[1];
                myNewTable.Rows[i]["GT_USERS_PRINTFLAG"] = print.Checked == true ? "Y" : "N";
            }
            ViewState[MENU_ACCESS_KEY] = myNewTable;
        }
        private void pClearControls()
        {
            txtUserID.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtDesignation.Text = "";
            txtEnterpriseID.Text = "";
            lblpasswordcreated.Text = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
        }
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuID=" + MenuID;
            Response.Redirect(url);
        }
        private void pUnLockControls()
        {
            string lstrStatus = ViewState[STATUS_KEY].ToString();

            if (lstrStatus == "Modify" && LoginUserInfo.UserID.ToUpper().Equals("ADMIN"))
            {
                lstLicenses.Enabled = true;
            }
            else
            {
                lstLicenses.Enabled = false;
            }
            //txtUserID.ReadOnly = (txtUserID.Text.ToUpper() == "ADMIN" ? true : false);
            txtUserID.ReadOnly = true;
            txtUserName.ReadOnly = false;
            txtPassword.ReadOnly = false;
            txtDesignation.ReadOnly = false;
            txtEnterpriseID.ReadOnly = false;
            gvUsers.Enabled = true;

            //			foreach(TextBox tb in gvUsers.Controls)
            //			{
            //				tb.Enabled = true;
            //			}
        }
        private void pLockControls()
        {
            lstLicenses.Enabled = false;

            txtUserID.ReadOnly = true;
            txtUserName.ReadOnly = true;
            txtPassword.ReadOnly = true;
            txtDesignation.ReadOnly = true;
            txtEnterpriseID.ReadOnly = true;
            gvUsers.Enabled = false;

            //			foreach(TextBox tb in gvUsers.Controls)
            //			{
            //				tb.Enabled = false;
            //			}
        }
        protected void Page_EditButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Modify";
            pUnLockControls();
            pBindGrid();
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
                    btnUserAdmin.Status = "Deletion not possible...!";
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
        private void SetTable(ref DataTable myNewTable, DataTable myOldTable, string instanceID)
        {
            DataRow[] objParent = myNewTable.Select("G_MENU_SLNO = '" + instanceID + "'");

            int LevelNo = 0;
            if (objParent != null && objParent.Length > 0)
            {
                LevelNo = Convert.ToInt32(objParent[0]["G_MENU_LEVEL"].ToString());
                LevelNo++;
            }

            DataView children = new DataView(myOldTable);
            children.RowFilter = "ISNULL(G_MENU_PARENTCODE,'') = '" + instanceID + "'";

            foreach (DataRowView row in children)
            {
                DataRow objDR = myNewTable.NewRow();
                //objDR = row.Row;

                string text = string.Empty;
                for (int i = 1; i <= LevelNo; i++)
                    text += "&nbsp;&nbsp;&nbsp;";

                objDR["G_MENU_LEVEL"] = LevelNo;
                objDR["G_MENU_PARENTCODE"] = Convert.ToInt32(row["G_MENU_PARENTCODE"].ToString());
                objDR["G_MENU_SLNO"] = Convert.ToInt32(row["G_MENU_SLNO"].ToString());

                objDR["GT_USERS_STATUSFLAG"] = row["GT_USERS_STATUSFLAG"].ToString();
                objDR["GT_USERS_ADDFLAG"] = row["GT_USERS_ADDFLAG"].ToString();
                objDR["GT_USERS_EDITFLAG"] = row["GT_USERS_EDITFLAG"].ToString();
                objDR["GT_USERS_DELETEFLAG"] = row["GT_USERS_DELETEFLAG"].ToString();
                objDR["GT_USERS_PRINTFLAG"] = row["GT_USERS_PRINTFLAG"].ToString();
                objDR["G_MENU_NAME"] = text + row["G_MENU_NAME"];

                myNewTable.Rows.Add(objDR);

                SetTable(ref myNewTable, myOldTable, row["G_MENU_SLNO"].ToString());
            }
            //return myNewTable;
        }
        private void pSave()
        {
            try
            {
                myUserInfo = (UserInfo)ViewState[TRAN_ID_KEY];
                DataTable myNewTable = (DataTable)ViewState[MENU_ACCESS_KEY];

                pMapControls();

                string lstrDBName = string.Empty;
                string lstrBrnCd = String.Empty;
                foreach (ListItem lstItem in lstLicenses.Items)
                {
                    if (lstItem.Selected)
                    {
                        lstrDBName = lstItem.Value.Substring(0, lstItem.Value.IndexOf("::"));
                        lstrBrnCd = lstItem.Value.Substring(lstItem.Value.IndexOf("::") + 2);

                        myUserInfo.BranchID = Convert.ToInt32(lstrBrnCd);
                        SQLServerDAL.Masters.UserMenuAccess.Delete(lstrDBName, Convert.ToInt32(lstrBrnCd), txtUserID.Text);
                        SQLServerDAL.Masters.User.Insert(lstrDBName, myUserInfo);
                        SQLServerDAL.Masters.UserMenuAccess.InsertByDataTable(lstrDBName, myUserInfo.GroupID, Convert.ToInt32(lstrBrnCd), txtUserID.Text, myNewTable);
                    }
                }
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
                myUserInfo = (UserInfo)ViewState[TRAN_ID_KEY];
                DataTable myNewTable = (DataTable)ViewState[MENU_ACCESS_KEY];

                //pMapControls();
                string lstrDBName = string.Empty;
                string lstrBrnCd = String.Empty;
                foreach (ListItem lstItem in lstLicenses.Items)
                {
                    lstrDBName = lstItem.Value.Substring(0, lstItem.Value.IndexOf("::"));
                    lstrBrnCd = lstItem.Value.Substring(lstItem.Value.IndexOf("::") + 2);

                    myUserInfo.BranchID = Convert.ToInt32(lstrBrnCd);

                    if (lstItem.Selected)
                    {
                        SQLServerDAL.Masters.UserMenuAccess.Delete(lstrDBName, Convert.ToInt32(lstrBrnCd), txtUserID.Text);
                        // user.Delete(lstrDBName, Convert.ToInt32(lstrBrnCd), txtUserID.Text);
                        //  user.Insert(lstrDBName, myUserInfo);
                        SQLServerDAL.Masters.User.Update(lstrDBName, myUserInfo);
                        SQLServerDAL.Masters.UserMenuAccess.InsertByDataTable(lstrDBName, myUserInfo.GroupID, Convert.ToInt32(lstrBrnCd), txtUserID.Text, myNewTable);
                    }
                    else
                    {
                        SQLServerDAL.Masters.UserMenuAccess.Delete(lstrDBName, Convert.ToInt32(lstrBrnCd), txtUserID.Text);
                        SQLServerDAL.Masters.User.Delete(lstrDBName, Convert.ToInt32(lstrBrnCd), txtUserID.Text);
                    }
                }
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
                myUserInfo = (UserInfo)ViewState[TRAN_ID_KEY];
                DataTable myNewTable = (DataTable)ViewState[MENU_ACCESS_KEY];

                //pMapControls();

                string lstrDBName = string.Empty;
                string lstrBrnCd = String.Empty;
                foreach (ListItem lstItem in lstLicenses.Items)
                {
                    lstrDBName = lstItem.Value.Substring(0, lstItem.Value.IndexOf("::"));
                    lstrBrnCd = lstItem.Value.Substring(lstItem.Value.IndexOf("::") + 2);

                    myUserInfo.BranchID = Convert.ToInt32(lstrBrnCd);

                    SQLServerDAL.Masters.UserMenuAccess.Delete(lstrDBName, Convert.ToInt32(lstrBrnCd), txtUserID.Text);
                    SQLServerDAL.Masters.User.Delete(lstrDBName, Convert.ToInt32(lstrBrnCd), txtUserID.Text);
                }
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myUserInfo = (UserInfo)ViewState[TRAN_ID_KEY];

            return true;

            //			if (uomRead.ValidateDelete(myUom.SlNo))
            //				return true;
            //			else
            //				return false;
        }
        private bool fblnValidEntry()
        {
            return true;
            //if (page.isvalid)
            //{
            //    myuserinfo = (uominfo)viewstate[tran_id_key];

            //    if (viewstate[status_key].equals("modify") && myuom.slno == 0)
            //    {
            //        btnuseradmin.status = "uom not found...!";
            //        return false;
            //    }
            //    uomread uu = new uomread();
            //    if (uu.checkuom(myuom))
            //        return true;
            //    else
            //    {
            //        btnuseradmin.status = "duplicate entry...!";
            //        return false;
            //    }
            //}
            //else
            //{
            //    btnuseradmin.status = "check ur entries...!";
            //    return false;
            //}
        }
    }
}
