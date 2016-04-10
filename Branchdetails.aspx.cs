using System;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using ISPL.CSC.Model;
using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class Branchdetails : BasePage
    {
        private BranchInfo myBranchInfo;

        private const string BRANCH_KEY = "BranchID";
        private const string BRANCH_EXTD_KEY = "BRANCH_EXTD_KEY";
        private const string STATUS_KEY = "Status";

        private const string GROUP_KEY = "GROUP_KEY";

        protected void Page_Load(object sender, EventArgs e)
        {
            bcBranch.DeleteButtonVisible = false;
            this.LOVState.LOVAfterClick += new EventHandler(this.LOVState_AfterClick);
            if (!IsPostBack)
            {
                pDispHeading();
                pSetUserControls();
                ViewState[BRANCH_KEY] = LoginBranchInfo;

                pBindData();
                pLockControls();
                ViewState[STATUS_KEY] = "View";
                bcBranch.MenuID = MenuID;
                bcBranch.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void LOVState_AfterClick(object sender, EventArgs e)
        {
            if (LOVState.strLastColumn.Length != 0)
            {
                Model.Masters.StateInfo myState = SQLServerDAL.Masters.State.GetStateInfo(Convert.ToInt32(LOVState.strLastColumn));
                LOVCountry.strFirstColumn = myState.CountryInfo.Name;
                LOVCountry.strLastColumn = myState.CountryInfo.SlNo.ToString();
            }
        }
        private void pBindData()
        {
            myBranchInfo = (Model.Masters.BranchInfo)ViewState[BRANCH_KEY];
            txtBranchName.Text = myBranchInfo.Name;
            txtBranShortName.Text = myBranchInfo.ShortName;
            txtAddress1.Text = myBranchInfo.HOAddress1;
            txtAddress2.Text = myBranchInfo.HOAddress2;
            txtAddress3.Text = myBranchInfo.HOAddress3;
            txtCity.Text = myBranchInfo.HOCity;

            if (myBranchInfo.HOState == null)
                LOVState.ClearAll();
            else
            {
                LOVState.strFirstColumn = myBranchInfo.HOState.Name;
                LOVState.strLastColumn = myBranchInfo.HOState.SlNo.ToString();
            }

            if (myBranchInfo.Country == null)
                LOVCountry.ClearAll();
            else
            {
                LOVCountry.strFirstColumn = myBranchInfo.Country.Name;
                LOVCountry.strLastColumn = myBranchInfo.Country.SlNo.ToString();
            }

            txtCity.Text = myBranchInfo.HOCity;
            txtZipCode.Text = myBranchInfo.HOPinCode;
        }       
        private void pDispHeading()
        {
            //lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pSetUserControls()
        {
            bcBranch.Status = "";
         
            LOVState.Query = "SELECT M_STATE_NAME as [State Name], M_STATE_SNAME as [Short Name], M_STATE_CODE FROM M_STATE";
            LOVState.Required = false;

            LOVCountry.Query = " SELECT M_COUNTRY_NAME as Country, M_COUNTRY_SNAME as ShortName, M_Country_Code FROM M_COUNTRY ";            
        }
        protected void bcBranch_EditButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Modify";
            pUnLockControls();
        }
        protected void bcBranch_DeleteButton(object sender, EventArgs e)
        {
            pBacktoGrid();
            bcBranch.Status = "Deletion not Possible..";
        }
        protected void bcBranch_CancelButton(object sender, EventArgs e)
        {
            pBacktoGrid();
        }
        private void pBacktoGrid()
        {
            pLockControls();
            pBindData();
            bcBranch.MenuID = MenuID;
            bcBranch.ButtonClicked = "View";
            this.bcBranch.DeleteButtonVisible = false;
        }
        protected void bcBranch_SubmitButton(object sender, EventArgs e)
        {
            string lstrStatus = ViewState[STATUS_KEY].ToString();

            pMapControls();

            if (fblnValidEntry())
            {
                if (lstrStatus.Equals("Edit") || lstrStatus.Equals("Modify"))
                    pUpdate();

                pBacktoGrid();
            }
        }
        private void pLockControls()
        {
            txtBranchName.ReadOnly = true;
            txtBranShortName.ReadOnly = true;
            txtAddress1.ReadOnly = true;
            txtAddress2.ReadOnly = true;
            txtAddress3.ReadOnly = true;
            txtCity.ReadOnly = true;
            LOVState.ReadOnly = true;
            LOVCountry.ReadOnly = true;
            txtZipCode.ReadOnly = true;
        }
        private void pUnLockControls()
        {
            txtBranchName.ReadOnly = false;
            txtBranShortName.ReadOnly = false;
            txtAddress1.ReadOnly = false;
            txtAddress2.ReadOnly = false;
            txtAddress3.ReadOnly = false;
            txtCity.ReadOnly = false;
            LOVState.ReadOnly = false;
            LOVCountry.ReadOnly = false;
            txtZipCode.ReadOnly = false;
        }
        private bool fblnValidEntry()
        {
            return true;
        }
        private void pMapControls()
        {
            myBranchInfo = (Model.Masters.BranchInfo)ViewState[BRANCH_KEY];

            myBranchInfo.Name = WebComponents.CleanString.InputText(txtBranchName.Text, txtBranchName.MaxLength);
            myBranchInfo.ShortName = WebComponents.CleanString.InputText(txtBranShortName.Text, txtBranShortName.MaxLength);
            myBranchInfo.HOAddress1 = WebComponents.CleanString.InputText(txtAddress1.Text, txtAddress1.MaxLength);
            myBranchInfo.HOAddress2 = WebComponents.CleanString.InputText(txtAddress2.Text, txtAddress2.MaxLength);
            myBranchInfo.HOAddress3 = WebComponents.CleanString.InputText(txtAddress3.Text, txtAddress3.MaxLength);
            myBranchInfo.HOCity = WebComponents.CleanString.InputText(txtCity.Text, txtCity.MaxLength);
            
            if (LOVState.strLastColumn.Length == 0)
                myBranchInfo.HOState = null;
            else
            {
                myBranchInfo.HOState = SQLServerDAL.Masters.State.GetStateInfo(Convert.ToInt32(LOVState.strLastColumn));
            }

            if (LOVCountry.strLastColumn.Length == 0)
                myBranchInfo.Country = null;
            else
            {
                Model.Masters.CountryInfo myCountry = SQLServerDAL.Masters.Country.GetCountryInfo(Convert.ToInt32(LOVCountry.strLastColumn));
                myBranchInfo.Country = myCountry;
            }
            myBranchInfo.HOPinCode = WebComponents.CleanString.InputText(txtZipCode.Text, txtZipCode.MaxLength);

            ViewState[BRANCH_KEY] = myBranchInfo;
        }
        private void pUpdate()
        {
            try
            {
                pMapControls();

                SQLServerDAL.Masters.Branch.Update(myBranchInfo);

                HttpContext.Current.Session["BRANCH_KEY"] = myBranchInfo;
            }
            catch
            {
                throw;
            }
        }
    }
}

