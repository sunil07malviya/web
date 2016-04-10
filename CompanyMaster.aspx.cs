using System;
using System.Web.UI;

using ISPL.CSC.Model;
using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class CompanyMaster : BasePage
    {
        private CompanyInfo myCompanyInfo = null;

        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        protected void Page_Load(object sender, EventArgs e)
        {
            btnCompany.Status = "";
            btnCompany.DeleteButtonVisible = false;

            this.btnCompany.CancelButtonClick += new System.EventHandler(this.Page_CancelButton);
            this.btnCompany.EditButtonClick += new System.EventHandler(this.Page_EditButton);
            this.btnCompany.DeleteButtonClick += new System.EventHandler(this.Page_DeleteButton);
            this.btnCompany.SubmitButtonClick += new System.EventHandler(this.Page_SubmitButton);

            this.LOVState.LOVAfterClick += new EventHandler(this.LOVState_AfterClick);

            if (!IsPostBack)
            {
                pDispHeading();
                pSetUserControls();

                myCompanyInfo = SQLServerDAL.Masters.Company.GetCompanyInfo(LoginUserInfo.GroupID);

                if (myCompanyInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myCompanyInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new CompanyInfo();
                    ViewState[STATUS_KEY] = "Modify";
                    pClearControls();
                }
            }
            btnCompany.MenuID = MenuID;
            btnCompany.ButtonClicked = ViewState[STATUS_KEY].ToString();
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
        private void pSetUserControls()
        {
            LOVState.Query = "SELECT M_STATE_NAME as [State Name], M_STATE_SNAME as [Short Name], M_STATE_CODE FROM M_STATE";
            LOVState.Required = false;

            LOVCountry.Query = " SELECT M_COUNTRY_NAME as Country, M_COUNTRY_SNAME as ShortName, M_Country_Code FROM M_COUNTRY ";            
        }         
        private void pDispHeading()
        {
            //lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);  
            lblHeading.InnerText = "Company Master";
        }
        private void pBindControls()
        {
            myCompanyInfo = (CompanyInfo)ViewState[TRAN_ID_KEY];

            txtName.Text = myCompanyInfo.Name;
            txtShortName.Text = myCompanyInfo.ShortName;
            txtAddress1.Text = myCompanyInfo.HOAddress1;
            txtAddress2.Text = myCompanyInfo.HOAddress2;
            txtAddress3.Text = myCompanyInfo.HOAddress3;
            txtCity.Text = myCompanyInfo.HOCity;

            if (myCompanyInfo.HOState == null)
                LOVState.ClearAll();
            else
            {
                LOVState.strFirstColumn = myCompanyInfo.HOState.Name;
                LOVState.strLastColumn = myCompanyInfo.HOState.SlNo.ToString();
            }
            if (myCompanyInfo.Country == null)
                LOVCountry.ClearAll();
            else
            {
                LOVCountry.strFirstColumn = myCompanyInfo.Country.Name;
                LOVCountry.strLastColumn = myCompanyInfo.Country.SlNo.ToString();
            }

            txtZipCode.Text = myCompanyInfo.HOPinCode;
        }
        private void pMapControls()
        {
            myCompanyInfo = (CompanyInfo)ViewState[TRAN_ID_KEY];

            myCompanyInfo.Name = WebComponents.CleanString.InputText(txtName.Text, txtName.MaxLength);
            myCompanyInfo.ShortName = WebComponents.CleanString.InputText(txtShortName.Text, txtShortName.MaxLength);
            myCompanyInfo.HOAddress1 = WebComponents.CleanString.InputText(txtAddress1.Text, txtAddress1.MaxLength);
            myCompanyInfo.HOAddress2 = WebComponents.CleanString.InputText(txtAddress2.Text, txtAddress2.MaxLength);
            myCompanyInfo.HOAddress3 = WebComponents.CleanString.InputText(txtAddress3.Text, txtAddress3.MaxLength);
            myCompanyInfo.HOCity = WebComponents.CleanString.InputText(txtCity.Text, txtCity.MaxLength);
            if (LOVState.strLastColumn.Length == 0)
                myCompanyInfo.HOState = null;
            else
            {
                myCompanyInfo.HOState = SQLServerDAL.Masters.State.GetStateInfo(Convert.ToInt32(LOVState.strLastColumn));
            }

            if (LOVCountry.strLastColumn.Length == 0)
                myCompanyInfo.Country = null;
            else
            {
                Model.Masters.CountryInfo myCountry = SQLServerDAL.Masters.Country.GetCountryInfo(Convert.ToInt32(LOVCountry.strLastColumn));
                myCompanyInfo.Country = myCountry;
            }
            myCompanyInfo.HOPinCode = WebComponents.CleanString.InputText(txtZipCode.Text, txtZipCode.MaxLength);

            ViewState[TRAN_ID_KEY] = myCompanyInfo;
        }
        protected void Page_EditButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Modify";
            pUnLockControls();
        }
        protected void Page_DeleteButton(object sender, EventArgs e)
        {
            pBacktoGrid();
            btnCompany.Status = "Deletion not Possible..";
        }
        protected void Page_CancelButton(object sender, EventArgs e)
        {
            pBacktoGrid();
        }
        protected void Page_SubmitButton(object sender, EventArgs e)
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
        private void pBacktoGrid()
        {
            pLockControls();
            pBindControls();
            btnCompany.MenuID = MenuID;
            btnCompany.ButtonClicked = "View";
        }
        private void pClearControls()
        {
            txtName.Text = "";
            txtShortName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtCity.Text = "";
            LOVState.ClearAll();
        }
        private void pLockControls()
        {
            txtName.ReadOnly = true;
            txtShortName.ReadOnly = true;
            txtAddress1.ReadOnly = true;
            txtAddress2.ReadOnly = true;
            txtAddress3.ReadOnly = true;
            txtCity.ReadOnly = true;
            LOVState.ReadOnly = true;
        }
        private void pUnLockControls()
        {
            txtName.ReadOnly = false;
            txtShortName.ReadOnly = false;
            txtAddress1.ReadOnly = false;
            txtAddress2.ReadOnly = false;
            txtAddress3.ReadOnly = false;
            txtCity.ReadOnly = false;
            LOVState.ReadOnly = false;
        }
        private bool fblnValidEntry()
        {
            return true;
        }
        private void pUpdate()
        {
            try
            {
                myCompanyInfo = (CompanyInfo)ViewState[TRAN_ID_KEY];

                pMapControls();

                SQLServerDAL.Masters.Company.Update(myCompanyInfo);
            }
            catch
            {
                throw;
            }
        }
    }
}
