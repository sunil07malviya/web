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

using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class ServiceProvider : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private ServiceProviderInfo myServiceProviderInfo = null;

        private void Page_Load(object sender, System.EventArgs e)
        {
            btnServiceProvider.Status = "";
            this.LOVState.LOVAfterClick += new EventHandler(this.LOVState_AfterClick);
            
            if (!IsPostBack)
            {
                pDispHeading();
                pBindDDLSP();
                myServiceProviderInfo = SQLServerDAL.Masters.ServiceProvider.GetServiceProviderInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myServiceProviderInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myServiceProviderInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new ServiceProviderInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                pSetUserControls();
                btnServiceProvider.MenuID = MenuID;
                btnServiceProvider.ButtonClicked = ViewState[STATUS_KEY].ToString();
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
        private void pBindDDLSP()
        {
            ddlChaType.Items.Clear();

            ListItem item = new ListItem("CHA", "C");
            ddlChaType.Items.Add(item);
            item = new ListItem("Freight Forwarder", "F");
            ddlChaType.Items.Add(item);
            item = new ListItem("Both", "B");
            ddlChaType.Items.Add(item);
            item = new ListItem("Others", "O");
            ddlChaType.Items.Add(item);

            ddlChaType.ClearSelection();
        }
        private void pSetUserControls()
        {
            LOVCountry.Query = " SELECT M_COUNTRY_NAME [Country Name], M_COUNTRY_SNAME [Short Name], M_Country_Code FROM M_COUNTRY ";
            LOVCountry.Required = true;
            
            LOVState.Query = "SELECT M_STATE_NAME [State Name], M_STATE_SNAME as [Short Name], M_STATE_CODE FROM M_STATE";
            LOVState.Required = false;

            txtName.TabIndex = 1;
            txtShortName.TabIndex = 2;
            txtAddress1.TabIndex = 3;
            txtAddress2.TabIndex = 4;
            txtAddress3.TabIndex = 5;
            txtCity.TabIndex = 6;
            LOVState.TabIndex = 7;
            LOVCountry.TabIndex = 9;
            txtZipCode.TabIndex = 10;
            txtURL.TabIndex = 11;
            btnServiceProvider.TabIndex = 12;
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myServiceProviderInfo = (Model.Masters.ServiceProviderInfo)ViewState[TRAN_ID_KEY];

            txtName.Text = myServiceProviderInfo.Name;
            txtShortName.Text = myServiceProviderInfo.ShortName;
            ddlChaType.ClearSelection();
            if (myServiceProviderInfo.Type.Length != 0)
            {
                ListItem lstItem = ddlChaType.Items.FindByValue(myServiceProviderInfo.Type.ToUpper());
                if (lstItem != null)
                    lstItem.Selected = true;
            }
            txtAddress1.Text = myServiceProviderInfo.Address1;
            txtAddress2.Text = myServiceProviderInfo.Address2;
            txtAddress3.Text = myServiceProviderInfo.Address3;

            txtCity.Text = myServiceProviderInfo.City;

            if (myServiceProviderInfo.State == null)
                LOVState.ClearAll();
            else
            {
                LOVState.strFirstColumn = myServiceProviderInfo.State.Name;
                LOVState.strLastColumn = myServiceProviderInfo.State.SlNo.ToString();
            }

            if (myServiceProviderInfo.Country == null)
            {
                LOVCountry.ClearAll();
            }
            else
            {
                LOVCountry.strLastColumn = myServiceProviderInfo.Country.SlNo.ToString();
                LOVCountry.strFirstColumn = myServiceProviderInfo.Country.Name;
            }
            txtZipCode.Text = myServiceProviderInfo.PinCode;
            txtURL.Text = myServiceProviderInfo.URL;
        }
        private void pMapControls()
        {
            myServiceProviderInfo = (ServiceProviderInfo)ViewState[TRAN_ID_KEY];

            myServiceProviderInfo.ShortName = WebComponents.CleanString.InputText(txtShortName.Text, txtShortName.MaxLength);
            myServiceProviderInfo.Name = WebComponents.CleanString.InputText(txtName.Text, txtName.MaxLength);
            myServiceProviderInfo.Type = ddlChaType.SelectedValue.ToString();
            myServiceProviderInfo.Address1 = WebComponents.CleanString.InputText(txtAddress1.Text, txtAddress1.MaxLength);
            myServiceProviderInfo.Address2 = WebComponents.CleanString.InputText(txtAddress2.Text, txtAddress2.MaxLength);
            myServiceProviderInfo.Address3 = WebComponents.CleanString.InputText(txtAddress3.Text, txtAddress3.MaxLength);
            myServiceProviderInfo.City = WebComponents.CleanString.InputText(txtCity.Text, txtCity.MaxLength);

            if (LOVState.strLastColumn.Length == 0)
                myServiceProviderInfo.State = null;
            else
            {
                myServiceProviderInfo.State = SQLServerDAL.Masters.State.GetStateInfo(Convert.ToInt32(LOVState.strLastColumn));
            }

            if (LOVCountry.strLastColumn.Length == 0)
            {
                myServiceProviderInfo.Country = null;
            }
            else
            {
                myServiceProviderInfo.Country = SQLServerDAL.Masters.Country.GetCountryInfo(Convert.ToInt32(LOVCountry.strLastColumn));
            }

            myServiceProviderInfo.PinCode = WebComponents.CleanString.InputText(txtZipCode.Text, txtZipCode.MaxLength);
            myServiceProviderInfo.URL = WebComponents.CleanString.InputText(txtURL.Text, txtURL.MaxLength);

            ViewState[TRAN_ID_KEY] = myServiceProviderInfo;
        }
        private void pClearControls()
        {
            txtShortName.Text = "";
            txtName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtCity.Text = "";
            LOVState.ClearAll();
            LOVCountry.ClearAll();
            txtZipCode.Text = "";
            txtURL.Text = "";
            ddlChaType.ClearSelection();
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
            txtAddress1.ReadOnly = false;
            txtAddress2.ReadOnly = false;
            txtAddress3.ReadOnly = false;
            txtCity.ReadOnly = false;
            LOVState.ReadOnly = false;
            LOVCountry.ReadOnly = false;
            txtZipCode.ReadOnly = false;
            txtURL.ReadOnly = false;
            ddlChaType.Enabled = true;
        }
        private void pLockControls()
        {
            txtShortName.ReadOnly = true;
            txtName.ReadOnly = true;
            txtAddress1.ReadOnly = true;
            txtAddress2.ReadOnly = true;
            txtAddress3.ReadOnly = true;
            txtCity.ReadOnly = true;
            LOVState.ReadOnly = true;
            LOVCountry.ReadOnly = true;
            txtZipCode.ReadOnly = true;
            txtURL.ReadOnly = true;
            ddlChaType.Enabled = false;
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
                    btnServiceProvider.Status = "Deletion not possible...!";
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

                myServiceProviderInfo = (ServiceProviderInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.ServiceProvider.Insert(myServiceProviderInfo);
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

                myServiceProviderInfo = (ServiceProviderInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.ServiceProvider.Update(myServiceProviderInfo);
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
                myServiceProviderInfo = (ServiceProviderInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.ServiceProvider.Delete(myServiceProviderInfo.Code);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myServiceProviderInfo = (ServiceProviderInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.ServiceProvider.blnValidateDelete(myServiceProviderInfo.Code))
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
                    myServiceProviderInfo = (ServiceProviderInfo)ViewState[TRAN_ID_KEY];                                       
                    if (SQLServerDAL.Masters.ServiceProvider.blnCheckBank(myServiceProviderInfo))
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
                btnServiceProvider.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
