using System;
using System.Web.UI;

using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class City : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private Model.Masters.CityInfo myCityInfo = null;

        private void Page_Load(object sender, System.EventArgs e)
        {
            btnCity.Status = "";
            this.LOVState.LOVAfterClick += new EventHandler(this.LOVState_AfterClick);
            
            if (!IsPostBack)
            {
                LOVState.Query = "SELECT M_STATE_NAME as [State Name], M_STATE_SNAME as [Short Name], M_STATE_CODE FROM M_STATE";
                LOVState.Required = false;
                LOVCountry.Query = "SELECT M_Country_Name as [Country Name],M_Country_SName as [Short Name],M_Country_Code From M_Country";
                LOVCountry.Required = false;
                pDispHeading();

                myCityInfo = SQLServerDAL.Masters.City.GetCityInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myCityInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myCityInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new CityInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                pSetUserControls();
                btnCity.MenuID = MenuID;
                btnCity.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pSetUserControls()
        {
            //LOVState.Query = "SELECT M_STATE_NAME as [State Name], M_STATE_SNAME as [Short Name], M_STATE_CODE FROM M_STATE";
            //LOVState.Required = false;

            //LOVCountry.Query = " SELECT M_COUNTRY_NAME as Country, M_COUNTRY_SNAME as ShortName, M_Country_Code FROM M_COUNTRY ";
            //LOVCountry.Required = false;

            txtName.TabIndex = 1;
            txtShortName.TabIndex = 2;
            LOVState.TabIndex = 7;
            LOVCountry.TabIndex = 9;
            btnCity.TabIndex = 12;
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myCityInfo = (Model.Masters.CityInfo)ViewState[TRAN_ID_KEY];

            txtName.Text = myCityInfo.Name;
            txtShortName.Text = myCityInfo.ShortName;

            if (myCityInfo.STATECODE == null)
                LOVState.ClearAll();
            else
            {
                LOVState.strFirstColumn = myCityInfo.STATECODE.Name;
                LOVState.strLastColumn = myCityInfo.STATECODE.SlNo.ToString();
            }

            if (myCityInfo.COUNTRYCODE == null)
            {
                LOVCountry.ClearAll();
            }
            else
            {
                LOVCountry.strLastColumn = myCityInfo.COUNTRYCODE.SlNo.ToString();
                LOVCountry.strFirstColumn = myCityInfo.COUNTRYCODE.Name;
            }
        }
        private void pMapControls()
        {
            myCityInfo = (CityInfo)ViewState[TRAN_ID_KEY];

            myCityInfo.ShortName = WebComponents.CleanString.InputText(txtShortName.Text, txtShortName.MaxLength);
            myCityInfo.Name = WebComponents.CleanString.InputText(txtName.Text, txtName.MaxLength);

            if (LOVState.strLastColumn.Length == 0)
                myCityInfo.STATECODE = null;
            else
            {
                myCityInfo.STATECODE = SQLServerDAL.Masters.State.GetStateInfo(Convert.ToInt32(LOVState.strLastColumn));
            }

            if (LOVCountry.strLastColumn.Length == 0)
            {
                myCityInfo.COUNTRYCODE = null;
            }
            else
            {
                myCityInfo.COUNTRYCODE = SQLServerDAL.Masters.Country.GetCountryInfo(Convert.ToInt32(LOVCountry.strLastColumn));
            }

            ViewState[TRAN_ID_KEY] = myCityInfo;
        }
        private void LOVState_AfterClick(object sender, EventArgs e)
        {
            if (LOVState.strLastColumn.Length != 0)
            {
                Model.Masters.StateInfo myState = SQLServerDAL.Masters.State.GetStateInfo(Convert.ToInt32(LOVState.strLastColumn));
                LOVCountry.strFirstColumn = myState.CountryInfo.Name;
                LOVCountry.strLastColumn = myState.CountryInfo.SlNo.ToString();
                LOVCountry.ReadOnly = true;
            }
        }
        private void pClearControls()
        {
            txtShortName.Text = "";
            txtName.Text = "";
            LOVState.ClearAll();
            LOVCountry.ClearAll();
        }
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuId=" + MenuID;
            Response.Redirect(url, true);
        }
        private void pUnLockControls()
        {
            txtShortName.ReadOnly = false;
            txtName.ReadOnly = false;
            LOVState.ReadOnly = false;
            LOVCountry.ReadOnly = false;
        }
        private void pLockControls()
        {
            txtShortName.ReadOnly = true;
            txtName.ReadOnly = true;
            LOVState.ReadOnly = true;
            LOVCountry.ReadOnly = true;
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
                    btnCity.Status = "Deletion not possible...!";
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

                myCityInfo = (CityInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.City.Insert(myCityInfo);
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

                myCityInfo = (CityInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.City.Update(myCityInfo);
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
                myCityInfo = (CityInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.City.Delete(myCityInfo.SLNO);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myCityInfo = (CityInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.City.blnValidateDelete(myCityInfo.SLNO))
                return true;
            else
                return false;
        }
        private bool fblnValidEntry()
        {
            bool lblnReturnValue = true;

            if (Page.IsValid)
            {
                if (txtName.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Name is required!";
                    lblnReturnValue = false;
                }
                if (lblnReturnValue)
                {
                    myCityInfo = (CityInfo)ViewState[TRAN_ID_KEY];

                    //if (ViewState[STATUS_KEY].Equals("Modify") && myCityInfo.SlNo == 0)
                    //{
                    //    lblMessage.Text = "car not found...!";
                    //    lblnReturnValue = false;
                    //}
                    if (SQLServerDAL.Masters.City.blnCheckCity(myCityInfo))
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
                btnCity.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
