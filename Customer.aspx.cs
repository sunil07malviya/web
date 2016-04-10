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

namespace ISPL.CSC.Web.Masters
{
    public partial class Customer : BasePage
    {
        private Model.Masters.CustomerInfo myCustomer;

        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        protected void Page_Load(object sender, EventArgs e)
        {
            bcCustomer.Status = "";

            this.bcCustomer.CancelButtonClick += new System.EventHandler(this.Page_CancelButton);
            this.bcCustomer.EditButtonClick += new System.EventHandler(this.Page_EditButton);
            this.bcCustomer.DeleteButtonClick += new System.EventHandler(this.Page_DeleteButton);
            this.bcCustomer.SubmitButtonClick += new System.EventHandler(this.Page_SubmitButton);

            this.LOVState.LOVAfterClick += new EventHandler(this.LOVState_AfterClick);

            if (!IsPostBack)
            {
                pDispHeading();
                pSetUserControls();

                Model.Masters.CustomerInfo myCustomer = SQLServerDAL.Masters.Customer.GetCustomer(Convert.ToInt32(Request["ID"].ToString()));

                if (myCustomer != null)
                {
                    ViewState[TRAN_ID_KEY] = myCustomer;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new Model.Masters.CustomerInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
            }
            bcCustomer.MenuID = MenuID;
            bcCustomer.ButtonClicked = ViewState[STATUS_KEY].ToString();
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pSetUserControls()
        {
            LOVState.Query = "SELECT M_STATE_NAME as [State Name], M_STATE_SNAME as [Short Name], M_STATE_CODE FROM M_STATE";
            LOVState.Required = false;

            LOVCountry.Query = " SELECT M_COUNTRY_NAME as Country, M_COUNTRY_SNAME as ShortName, M_Country_Code FROM M_COUNTRY ";
            LOVCurrency.Query = " SELECT M_Currency_SName as [Short Name], M_Currency_Name as Currency, M_Currency_Code FROM M_CURRENCY ";

            LOVSector.Query = "SELECT Sect_Name [NAME],Sect_SName [SHORT NAME],sectcd FROM M_SECTOR WITH (ROWLOCK)";
            LOVSector.Required = false;
        }
        private void LOVState_AfterClick(object sender, EventArgs e)
        {
            if (LOVState.strLastColumn.Length != 0)
            {
                Model.Masters.StateInfo myState = SQLServerDAL.Masters.State.GetStateInfo(Convert.ToInt32(LOVState.strLastColumn));

                LOVCountry.ClearAll();
                if (myState.CountryInfo != null)
                {
                    LOVCountry.strFirstColumn = myState.CountryInfo.Name;
                    LOVCountry.strLastColumn = myState.CountryInfo.SlNo.ToString();
                }
            }
        }
        private void pBindControls()
        {
            myCustomer = (Model.Masters.CustomerInfo)ViewState[TRAN_ID_KEY];

            txtName.Text = myCustomer.Name;
            txtShortName.Text = myCustomer.ShortName;
            txtAdd.Text = myCustomer.Address1;
            txtAdd1.Text = myCustomer.Address2;
            txtAdd2.Text = myCustomer.Address3;
            txtCity.Text = myCustomer.City;
            txtPin.Text = myCustomer.Pin;
            txtPh1.Text = myCustomer.Phone1;
            txtPh2.Text = myCustomer.Phone2;
            txtFax1.Text = myCustomer.Fax1;
            txtFax2.Text = myCustomer.Fax2;
            txtEmail1.Text = myCustomer.EMail1;
            txtEmail2.Text = myCustomer.EMail2;
            txtCPName1.Text = myCustomer.Contact1;
            txtCPDesig1.Text = myCustomer.CPDesignation1;
            txtCPph1.Text = myCustomer.CPPhone1;
            txtCPFax1.Text = myCustomer.CPFax1;
            txtCPEmail1.Text = myCustomer.CPEMail1;
            txtCPName2.Text = myCustomer.Contact2;
            txtCPDesig2.Text = myCustomer.CPDesignation2;
            txtCPPh2.Text = myCustomer.CPPhone2;
            txtCPFax2.Text = myCustomer.CPFax2;
            txtCPEmail2.Text = myCustomer.CPEMail2;
            txtLST.Text = myCustomer.LSTRegnNo;
            txtCST.Text = myCustomer.CSTRegnNo;
            txtST.Text = myCustomer.STNo;
            txtSTP.Text = myCustomer.STPLicenseNo;
            txtCustoms.Text = myCustomer.CustomsLicenseNo;
            txtTinNo.Text = myCustomer.TINNO;

            dtpLSTDt.CalendarDate = myCustomer.LSTRegnDate.ToString("dd-MMM-yyyy");
            dtpCSTDt.CalendarDate = myCustomer.CSTRegnDate.ToString("dd-MMM-yyyy");
            dtpST.CalendarDate = myCustomer.STDate.ToString("dd-MMM-yyyy");
            dtpstpDt.CalendarDate = myCustomer.STPLicenseDate.ToString("dd-MMM-yyyy");
            dtpCustDt.CalendarDate = myCustomer.CustomsLicenseDate.ToString("dd-MMM-yyyy");
            dtpStpVpTo.CalendarDate = myCustomer.STPLicenceDateValid.ToString("dd-MMM-yyyy");
            dtpCustVpTo.CalendarDate = myCustomer.CustomsLicenseValid.ToString("dd-MMM-yyyy");
            dtpTinDate.CalendarDate = myCustomer.TINDATE.ToString("dd-MMM-yyyy");
            dtpTinValidUpto.CalendarDate = myCustomer.TINVALIDUPTO.ToString("dd-MMM-yyyy");

            if (myCustomer.State == null)
                LOVState.ClearAll();
            else
            {
                LOVState.strFirstColumn = myCustomer.State.Name;
                LOVState.strLastColumn = myCustomer.State.SlNo.ToString();
            }

            if (myCustomer.Country == null)
                LOVCountry.ClearAll();
            else
            {
                LOVCountry.strFirstColumn = myCustomer.Country.Name;
                LOVCountry.strLastColumn = myCustomer.Country.SlNo.ToString();
            }

            if (myCustomer.Currency == null)
                LOVCurrency.ClearAll();
            else
            {
                LOVCurrency.strFirstColumn = myCustomer.Currency.ShortName;
                LOVCurrency.strLastColumn = myCustomer.Currency.SlNo.ToString();
            }

            if (myCustomer.Sector == null)
                LOVSector.ClearAll();
            else
            {
                LOVSector.strFirstColumn = myCustomer.Sector.ShortName;
                LOVSector.strLastColumn = myCustomer.Sector.SlNo.ToString();
            }
        }
        private void pMapControls()
        {
            myCustomer = (Model.Masters.CustomerInfo)ViewState[TRAN_ID_KEY];

            myCustomer.BranchID = LoginUserInfo.BranchID;
            myCustomer.Name = WebComponents.CleanString.InputText(txtName.Text, txtName.MaxLength);
            myCustomer.ShortName = WebComponents.CleanString.InputText(txtShortName.Text, txtShortName.MaxLength);
            myCustomer.Address1 = WebComponents.CleanString.InputText(txtAdd.Text, txtAdd.MaxLength);
            myCustomer.Address2 = WebComponents.CleanString.InputText(txtAdd1.Text, txtAdd1.MaxLength);
            myCustomer.Address3 = WebComponents.CleanString.InputText(txtAdd2.Text, txtAdd2.MaxLength);
            myCustomer.City = WebComponents.CleanString.InputText(txtCity.Text, txtCity.MaxLength);
            myCustomer.Pin = WebComponents.CleanString.InputText(txtPin.Text, txtPin.MaxLength);
            myCustomer.Phone1 = WebComponents.CleanString.InputText(txtPh1.Text, txtPh1.MaxLength);
            myCustomer.Phone2 = WebComponents.CleanString.InputText(txtPh2.Text, txtPh2.MaxLength);
            myCustomer.Fax1 = WebComponents.CleanString.InputText(txtFax1.Text, txtFax1.MaxLength);
            myCustomer.Fax2 = WebComponents.CleanString.InputText(txtFax2.Text, txtFax2.MaxLength);
            myCustomer.EMail1 = WebComponents.CleanString.InputText(txtEmail1.Text, txtEmail1.MaxLength);
            myCustomer.EMail2 = WebComponents.CleanString.InputText(txtEmail2.Text, txtEmail2.MaxLength);
            myCustomer.Contact1 = WebComponents.CleanString.InputText(txtCPName1.Text, txtCPName1.MaxLength);
            myCustomer.Contact2 = WebComponents.CleanString.InputText(txtCPName2.Text, txtCPName2.MaxLength);
            myCustomer.CPDesignation1 = WebComponents.CleanString.InputText(txtCPDesig1.Text, txtCPDesig1.MaxLength);
            myCustomer.CPDesignation2 = WebComponents.CleanString.InputText(txtCPDesig2.Text, txtCPDesig2.MaxLength);
            myCustomer.CPPhone1 = WebComponents.CleanString.InputText(txtCPph1.Text, txtCPph1.MaxLength);
            myCustomer.CPPhone2 = WebComponents.CleanString.InputText(txtCPPh2.Text, txtCPPh2.MaxLength);
            myCustomer.CPFax1 = WebComponents.CleanString.InputText(txtCPFax1.Text, txtCPFax1.MaxLength);
            myCustomer.CPFax2 = WebComponents.CleanString.InputText(txtCPFax2.Text, txtCPFax2.MaxLength);
            myCustomer.CPEMail1 = WebComponents.CleanString.InputText(txtCPEmail1.Text, txtCPEmail1.MaxLength);
            myCustomer.CPEMail2 = WebComponents.CleanString.InputText(txtCPEmail2.Text, txtCPEmail2.MaxLength);
            myCustomer.LSTRegnNo = WebComponents.CleanString.InputText(txtLST.Text, txtLST.MaxLength);
            myCustomer.CSTRegnNo = WebComponents.CleanString.InputText(txtCST.Text, txtCST.MaxLength);
            myCustomer.STNo = WebComponents.CleanString.InputText(txtST.Text, txtST.MaxLength);
            myCustomer.STPLicenseNo = WebComponents.CleanString.InputText(txtSTP.Text, txtSTP.MaxLength);
            myCustomer.CustomsLicenseNo = WebComponents.CleanString.InputText(txtCustoms.Text, txtCustoms.MaxLength);
            myCustomer.TINNO = WebComponents.CleanString.InputText(txtTinNo.Text, txtTinNo.MaxLength);

            myCustomer.LSTRegnDate = Convert.ToDateTime(dtpLSTDt.CalendarDate);
            myCustomer.CSTRegnDate = Convert.ToDateTime(dtpCSTDt.CalendarDate);
            myCustomer.STDate = Convert.ToDateTime(dtpST.CalendarDate);
            myCustomer.STPLicenseDate = Convert.ToDateTime(dtpstpDt.CalendarDate);
            myCustomer.CustomsLicenseDate = Convert.ToDateTime(dtpCustDt.CalendarDate);
            myCustomer.STPLicenceDateValid = Convert.ToDateTime(dtpStpVpTo.CalendarDate);
            myCustomer.CustomsLicenseValid = Convert.ToDateTime(dtpCustVpTo.CalendarDate);
            myCustomer.TINDATE = Convert.ToDateTime(dtpTinDate.CalendarDate);
            myCustomer.TINVALIDUPTO = Convert.ToDateTime(dtpTinValidUpto.CalendarDate);

            if (LOVState.strLastColumn.Length == 0)
                myCustomer.State = null;
            else
                myCustomer.State = SQLServerDAL.Masters.State.GetStateInfo(Convert.ToInt32(LOVState.strLastColumn));

            if (LOVCountry.strLastColumn.Length == 0)
                myCustomer.Country = null;
            else
                myCustomer.Country = SQLServerDAL.Masters.Country.GetCountryInfo(Convert.ToInt32(LOVCountry.strLastColumn));

            if (LOVCurrency.strLastColumn.Length == 0)
                myCustomer.Currency = null;
            else
                myCustomer.Currency = SQLServerDAL.Masters.Currency.GetCurrencyInfo(Convert.ToInt32(LOVCurrency.strLastColumn));

            if (LOVSector.strLastColumn.Length == 0)
                myCustomer.Sector = null;
            else
                myCustomer.Sector = SQLServerDAL.Masters.Sector.GetSectorInfo(Convert.ToInt32(LOVSector.strLastColumn));

            ViewState[TRAN_ID_KEY] = myCustomer;
        }
        private void pClearControls()
        {
            txtName.Text = "";
            txtShortName.Text = "";
            txtAdd.Text = "";
            txtAdd1.Text = "";
            txtAdd2.Text = "";
            txtCity.Text = "";
            txtPin.Text = "";
            txtPh1.Text = "";
            txtPh2.Text = "";
            txtFax1.Text = "";
            txtFax2.Text = "";
            txtEmail1.Text = "";
            txtEmail2.Text = "";
            txtCPName1.Text = "";
            txtCPDesig1.Text = "";
            txtCPph1.Text = "";
            txtCPFax1.Text = "";
            txtCPEmail1.Text = "";
            txtCPName2.Text = "";
            txtCPDesig2.Text = "";
            txtCPPh2.Text = "";
            txtCPFax2.Text = "";
            txtCPEmail2.Text = "";
            txtLST.Text = "";
            txtCST.Text = "";
            txtST.Text = "";
            txtSTP.Text = "";
            txtCustoms.Text = "";
            txtTinNo.Text = "";

            dtpLSTDt.ClearDate();
            dtpCSTDt.ClearDate();
            dtpST.ClearDate();
            dtpstpDt.ClearDate();
            dtpStpVpTo.ClearDate();
            dtpCustDt.ClearDate();
            dtpCustVpTo.ClearDate();
            dtpTinDate.ClearDate();
            dtpTinValidUpto.ClearDate();

            LOVState.ClearAll();
            LOVCountry.ClearAll();
            LOVCurrency.ClearAll();
            LOVSector.ClearAll();
        }
        private void pLockControls()
        {
            txtName.ReadOnly = true;
            txtShortName.ReadOnly = true;
            txtAdd.ReadOnly = true;
            txtAdd1.ReadOnly = true;
            txtAdd2.ReadOnly = true;
            txtCity.ReadOnly = true;
            txtPin.ReadOnly = true;
            txtPh1.ReadOnly = true;
            txtPh2.ReadOnly = true;
            txtFax1.ReadOnly = true;
            txtFax2.ReadOnly = true;
            txtEmail1.ReadOnly = true;
            txtEmail2.ReadOnly = true;
            txtCPName1.ReadOnly = true;
            txtCPDesig1.ReadOnly = true;
            txtCPph1.ReadOnly = true;
            txtCPFax1.ReadOnly = true;
            txtCPEmail1.ReadOnly = true;
            txtCPName2.ReadOnly = true;
            txtCPDesig2.ReadOnly = true;
            txtCPPh2.ReadOnly = true;
            txtCPFax2.ReadOnly = true;
            txtCPEmail2.ReadOnly = true;
            txtLST.ReadOnly = true;
            txtCST.ReadOnly = true;
            txtST.ReadOnly = true;
            txtSTP.ReadOnly = true;
            txtCustoms.ReadOnly = true;
            txtTinNo.ReadOnly = true;

            dtpLSTDt.ReadOnly = true;
            dtpCSTDt.ReadOnly = true;
            dtpST.ReadOnly = true;
            dtpstpDt.ReadOnly = true;
            dtpStpVpTo.ReadOnly = true;
            dtpCustDt.ReadOnly = true;
            dtpCustVpTo.ReadOnly = true;
            dtpTinDate.ReadOnly = true;
            dtpTinValidUpto.ReadOnly = true;

            LOVState.ReadOnly = true;
            LOVCountry.ReadOnly = true;
            LOVCurrency.ReadOnly = true;
            LOVSector.ReadOnly = true;
        }
        private void pUnLockControls()
        {
            txtName.ReadOnly = false;
            txtShortName.ReadOnly = false;
            txtAdd.ReadOnly = false;
            txtAdd1.ReadOnly = false;
            txtAdd2.ReadOnly = false;
            txtCity.ReadOnly = false;
            txtPin.ReadOnly = false;
            txtPh1.ReadOnly = false;
            txtPh2.ReadOnly = false;
            txtFax1.ReadOnly = false;
            txtFax2.ReadOnly = false;
            txtEmail1.ReadOnly = false;
            txtEmail2.ReadOnly = false;
            txtCPName1.ReadOnly = false;
            txtCPDesig1.ReadOnly = false;
            txtCPph1.ReadOnly = false;
            txtCPFax1.ReadOnly = false;
            txtCPEmail1.ReadOnly = false;
            txtCPName2.ReadOnly = false;
            txtCPDesig2.ReadOnly = false;
            txtCPPh2.ReadOnly = false;
            txtCPFax2.ReadOnly = false;
            txtCPEmail2.ReadOnly = false;
            txtLST.ReadOnly = false;
            txtCST.ReadOnly = false;
            txtST.ReadOnly = false;
            txtSTP.ReadOnly = false;
            txtCustoms.ReadOnly = false;
            txtTinNo.ReadOnly = false;

            dtpLSTDt.ReadOnly = false;
            dtpCSTDt.ReadOnly = false;
            dtpST.ReadOnly = false;
            dtpstpDt.ReadOnly = false;
            dtpStpVpTo.ReadOnly = false;
            dtpCustDt.ReadOnly = false;
            dtpCustVpTo.ReadOnly = false;
            dtpTinDate.ReadOnly = false;
            dtpTinValidUpto.ReadOnly = false;

            LOVState.ReadOnly = false;
            LOVCountry.ReadOnly = false;
            LOVCurrency.ReadOnly = false;
            LOVSector.ReadOnly = false;
        }
        private void Page_EditButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Modify";
            pUnLockControls();
        }

        private void Page_CancelButton(object sender, EventArgs e)
        {
            pBacktoGrid();
        }

        private void pBacktoGrid()
        {
            Response.Redirect("../DetailsView.aspx?MenuId=" + MenuID, true);
        }
        private void Page_DeleteButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Delete";
            pLockControls();
        }
        private void Page_SubmitButton(object sender, EventArgs e)
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
                    bcCustomer.Status = "Deletion not possible...!";
                    return;
                }
            }

            if (fblnValidEntry())
            {
                if (lstrStatus.Equals("New") || lstrStatus.Equals("Add"))
                    pSave();

                if (lstrStatus.Equals("Edit") || lstrStatus.Equals("Modify"))
                    pUpdate();

                pBacktoGrid();
            }

        }
        private bool fblnValidDelete()
        {
            myCustomer = (Model.Masters.CustomerInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.Customer.blnCheckforDelete(myCustomer.SlNo))
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
                    lblMessage.Text = "Customer Name is required!";
                    lblnReturnValue = false;
                }
                if (txtShortName.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Short Name is required!";
                    lblnReturnValue = false;
                }
                if (txtAdd.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Address is required!";
                    lblnReturnValue = false;
                }
                if (txtCity.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "City is required!";
                    lblnReturnValue = false;
                }
                if (lblnReturnValue)
                {
                    myCustomer = (Model.Masters.CustomerInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myCustomer.SlNo == 0)
                    {
                        bcCustomer.Status = "Customer not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.Customer.blnCheckCustomer(myCustomer))
                        lblnReturnValue = true;
                    else
                    {
                        bcCustomer.Status = "Duplicate Entry...!";
                        lblnReturnValue = false;
                    }
                }
            }
            else
            {
                bcCustomer.Status = "Check your entries...!";
                return false;
            }
            return lblnReturnValue;
        }
        private void pSave()
        {
            try
            {
                myCustomer = (Model.Masters.CustomerInfo)ViewState[TRAN_ID_KEY];

                pMapControls();

                SQLServerDAL.Masters.Customer.Insert(LoginBranchID, myCustomer, LoginUserInfo.UserID, MenuID);
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
                myCustomer = (Model.Masters.CustomerInfo)ViewState[TRAN_ID_KEY];

                pMapControls();

                SQLServerDAL.Masters.Customer.Update(LoginBranchID, myCustomer, LoginUserInfo.UserID, MenuID);
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
                myCustomer = (Model.Masters.CustomerInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Customer.Delete(myCustomer.SlNo, LoginBranchID, myCustomer, LoginUserInfo.UserID, MenuID);
            }
            catch
            {
                throw;
            }
        }
    }
}
