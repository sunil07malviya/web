using System;
using System.Web.UI.WebControls;

using ISPL.CSC.Model;
using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class Carrier : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private CarrierInfo myCarrierInfo = null;

        private void Page_Load(object sender, System.EventArgs e)
        {
            this.btnCarrier.CancelButtonClick += new System.EventHandler(this.Page_CancelButton);
            this.btnCarrier.EditButtonClick += new System.EventHandler(this.Page_EditButton);
            this.btnCarrier.DeleteButtonClick += new System.EventHandler(this.Page_DeleteButton);
            this.btnCarrier.SubmitButtonClick += new System.EventHandler(this.Page_SubmitButton);

            this.LOVState.LOVAfterClick += new EventHandler(this.LOVState_AfterClick);

            btnCarrier.Status = "";

            if (!IsPostBack)
            {
                pDispHeading();
                pBindDDL();
                myCarrierInfo = SQLServerDAL.Masters.Carrier.GetCarrierInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myCarrierInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myCarrierInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new CarrierInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                pSetUserControls();
                btnCarrier.MenuID = MenuID;
                btnCarrier.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void LOVState_AfterClick(object sender, EventArgs e)
        {
            if (LOVState.strLastColumn.Length != 0)
            {
                Model.Masters.StateInfo myState = SQLServerDAL.Masters.State.GetStateInfo(Convert.ToInt32(LOVState.strLastColumn));

                if (myState.CountryInfo == null)
                {
                    LOVCountry.ClearAll();
                    LOVCountry.ReadOnly = false;
                }
                else
                {
                    LOVCountry.strFirstColumn = myState.CountryInfo.Name;
                    LOVCountry.strLastColumn = myState.CountryInfo.SlNo.ToString();
                    LOVCountry.ReadOnly = true;
                }
            }
        } 
        private void pSetUserControls()
        {
            LOVCountry.Query = " SELECT M_COUNTRY_NAME [Country Name], M_COUNTRY_SNAME [Short Name], M_Country_Code FROM M_COUNTRY ";
            LOVCountry.Required = true;
            
            LOVState.Query = "SELECT M_STATE_NAME [State Name], M_STATE_SNAME as [Short Name], M_STATE_CODE FROM M_STATE";
            LOVState.Required = false;

            txtName.TabIndex = 1;
            txtShortName.TabIndex = 2;
            ddlType.TabIndex = 3;
            txtAddress1.TabIndex = 4;
            txtAddress2.TabIndex = 5;
            txtAddress3.TabIndex = 6;
            txtCity.TabIndex = 7;
            LOVState.TabIndex = 8;
            LOVCountry.TabIndex = 10;
            txtZipCode.TabIndex = 11;
            txtURL.TabIndex = 12;
            btnCarrier.TabIndex = 13;
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindDDL()
        {
            ListItem lstItem;

            lstItem = new ListItem("Air", "A");
            ddlType.Items.Add(lstItem);

            lstItem = new ListItem("Sea", "O");
            ddlType.Items.Add(lstItem);

            lstItem = new ListItem("Surface", "S");
            ddlType.Items.Add(lstItem);

            lstItem = new ListItem("Multi-Modal", "M");
            ddlType.Items.Add(lstItem);
        }
        private void pBindControls()
        {
            myCarrierInfo = (Model.Masters.CarrierInfo)ViewState[TRAN_ID_KEY];
           
            txtName.Text = myCarrierInfo.Name;
            txtShortName.Text = myCarrierInfo.ShortName;
            txtAddress1.Text = myCarrierInfo.Address1;
            txtAddress2.Text = myCarrierInfo.Address2;
            txtAddress3.Text = myCarrierInfo.Address3;            
            ddlType.ClearSelection();
            if (myCarrierInfo.Type == "")
                ddlType.ClearSelection();
            else
            {
                ListItem lstItem = ddlType.Items.FindByValue(myCarrierInfo.Type);
                if (lstItem != null)
                    lstItem.Selected = true;
            }
            txtCity.Text = myCarrierInfo.City;

            if (myCarrierInfo.State == null)
                LOVState.ClearAll();
            else
            {
                LOVState.strFirstColumn = myCarrierInfo.State.Name;
                LOVState.strLastColumn = myCarrierInfo.State.SlNo.ToString();
            }

            if (myCarrierInfo.Country == null)
                LOVCountry.ClearAll();
            else
            {
                LOVCountry.strFirstColumn = myCarrierInfo.Country.Name;
                LOVCountry.strLastColumn = myCarrierInfo.Country.SlNo.ToString();
            }
            txtZipCode.Text = myCarrierInfo.PinCode;
            txtURL.Text = myCarrierInfo.URL;
        }
        private void pMapControls()
        {
            myCarrierInfo = (CarrierInfo)ViewState[TRAN_ID_KEY];
            
            myCarrierInfo.ShortName = WebComponents.CleanString.InputText(txtShortName.Text, txtShortName.MaxLength);
            myCarrierInfo.Name = WebComponents.CleanString.InputText(txtName.Text, txtName.MaxLength);
            myCarrierInfo.Type = ddlType.SelectedValue;
            myCarrierInfo.Address1 = WebComponents.CleanString.InputText(txtAddress1.Text, txtAddress1.MaxLength);
            myCarrierInfo.Address2 = WebComponents.CleanString.InputText(txtAddress2.Text, txtAddress2.MaxLength);
            myCarrierInfo.Address3 = WebComponents.CleanString.InputText(txtAddress3.Text, txtAddress3.MaxLength);
            myCarrierInfo.City = WebComponents.CleanString.InputText(txtCity.Text, txtCity.MaxLength);

            if (LOVState.strLastColumn.Length == 0)
                myCarrierInfo.State = null;
            else
            {
                myCarrierInfo.State = SQLServerDAL.Masters.State.GetStateInfo(Convert.ToInt32(LOVState.strLastColumn));
            }

            if (LOVCountry.strLastColumn.Length == 0)
            {
                myCarrierInfo.Country = null;
            }
            else
            {
                myCarrierInfo.Country = SQLServerDAL.Masters.Country.GetCountryInfo(Convert.ToInt32(LOVCountry.strLastColumn));
            }            
            myCarrierInfo.PinCode = WebComponents.CleanString.InputText(txtZipCode.Text, txtZipCode.MaxLength);
            myCarrierInfo.URL = WebComponents.CleanString.InputText(txtURL.Text, txtURL.MaxLength);
            
            ViewState[TRAN_ID_KEY] = myCarrierInfo;
        }
        private void pClearControls()
        {
            txtShortName.Text = "";
            txtName.Text = "";
            ddlType.ClearSelection();
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtCity.Text = "";
            LOVState.ClearAll();
            txtZipCode.Text = "";
            txtURL.Text = "";
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
            ddlType.Enabled = true;
            txtAddress1.ReadOnly = false;
            txtAddress2.ReadOnly = false;
            txtAddress3.ReadOnly = false;
            txtCity.ReadOnly = false;
            LOVState.ReadOnly = false;
            txtZipCode.ReadOnly = false;
            txtURL.ReadOnly = false;
        }
        private void pLockControls()
        {
            txtShortName.ReadOnly = true;
            txtName.ReadOnly = true;
            ddlType.Enabled = false;
            txtAddress1.ReadOnly = true;
            txtAddress2.ReadOnly = true;
            txtAddress3.ReadOnly = true;
            txtCity.ReadOnly = true;
            LOVState.ReadOnly = true;
            txtZipCode.ReadOnly = true;
            txtURL.ReadOnly = true;
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
                    btnCarrier.Status = "Deletion not possible...!";
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

                myCarrierInfo = (CarrierInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Carrier.Insert(myCarrierInfo);
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

                myCarrierInfo = (CarrierInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Carrier.Update(myCarrierInfo);
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
                myCarrierInfo = (CarrierInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Carrier.Delete(myCarrierInfo.Code);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myCarrierInfo = (CarrierInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.Carrier.blnValidateDelete(myCarrierInfo.Code))
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
                if (txtShortName.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Short Name is required!";
                    lblnReturnValue = false;
                }
                if (lblnReturnValue)
                {
                    myCarrierInfo = (CarrierInfo)ViewState[TRAN_ID_KEY];

                    if (SQLServerDAL.Masters.Carrier.blnCheckBank(myCarrierInfo))
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
                btnCarrier.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
