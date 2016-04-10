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
    public partial class Supplier : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private Model.Masters.SupplierInfo mySupplier;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSupplier.Status = "";
            this.btnSupplier.EditButtonClick += new EventHandler(btnSupplier_EditButtonClick);
            this.btnSupplier.DeleteButtonClick += new EventHandler(btnSupplier_DeleteButtonClick);
            this.btnSupplier.SubmitButtonClick += new EventHandler(btnSupplier_SubmitButtonClick);
            this.btnSupplier.CancelButtonClick += new EventHandler(btnSupplier_CancelButtonClick);
            this.LOVState.LOVAfterClick += new EventHandler(LOVState_LOVAfterClick);

            if (!IsPostBack)
            {
                pDispHeading();
                pSetUserControls();
                pBindDDLSupplier();
                pBindDDLCategory();
                pBindDDLMfrDlr();

                mySupplier = SQLServerDAL.Masters.Supplier.GetSupplier(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (mySupplier != null)
                {
                    ViewState[TRAN_ID_KEY] = mySupplier;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new Model.Masters.SupplierInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
            }
            btnSupplier.MenuID = MenuID;
            btnSupplier.ButtonClicked = ViewState[STATUS_KEY].ToString();
        }
        private void pDispHeading()
        {


            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pSetUserControls()
        {
            LOVCountry.Query = " SELECT M_COUNTRY_NAME [Country Name], M_COUNTRY_SNAME [Short Name], M_Country_Code FROM M_COUNTRY ";
            LOVState.Query = "SELECT M_STATE_NAME [State Name], M_STATE_SNAME as [Short Name], M_STATE_CODE FROM M_STATE";
            LOVState.Required = false;
        }
        private void pClearControls()
        {
            txtName.Text = "";
            txtShortName.Text = "";
            txtAdd.Text = "";
            txtAdd1.Text = "";
            txtAdd2.Text = "";
            txtPh.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtCP.Text = "";
            txtECC.Text = "";
            txtCommission.Text = "";
            txtDivision.Text = "";
            txtRange.Text = "";
            txtRAdd1.Text = "";
            txtRAdd2.Text = "";
            txtRCity.Text = "";
            txtRPin.Text = "";
            txtLSTregnNo.Text = "";
            txtCSTregnNo.Text = "";
            txtST.Text = "";
            txtTinNo.Text = "";

            dtpLSTLicenseDate.ClearDate();
            dtpCSTLicenseDate.ClearDate();
            dtpST.ClearDate();
            dtpTINDate.ClearDate();

            LOVState.ClearAll();
            LOVCountry.ClearAll();
        }
        private void pLockControls()
        {
            txtName.ReadOnly = true;
            txtShortName.ReadOnly = true;
            txtAdd.ReadOnly = true;
            txtAdd1.ReadOnly = true;
            txtAdd2.ReadOnly = true;
            txtPh.ReadOnly = true;
            txtFax.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtCP.ReadOnly = true;
            txtECC.ReadOnly = true;
            txtCommission.ReadOnly = true;
            txtDivision.ReadOnly = true;
            txtRange.ReadOnly = true;
            txtRAdd1.ReadOnly = true;
            txtRAdd2.ReadOnly = true;
            txtRCity.ReadOnly = true;
            txtRPin.ReadOnly = true;
            txtLSTregnNo.ReadOnly = true;
            txtCSTregnNo.ReadOnly = true;
            txtST.ReadOnly = true;
            txtTinNo.ReadOnly = true;

            dtpLSTLicenseDate.ReadOnly = true;
            dtpCSTLicenseDate.ReadOnly = true;
            dtpST.ReadOnly = true;
            dtpTINDate.ReadOnly = true;

            ddlCategory.Enabled = false;
            ddlSupplierType.Enabled = false;
            ddlMfrDlr.Enabled = false;

            LOVState.ReadOnly = true;
            LOVCountry.ReadOnly = true;
        }
        private void pUnLockControls()
        {
            txtName.ReadOnly = false;
            txtShortName.ReadOnly = false;
            txtAdd.ReadOnly = false;
            txtAdd1.ReadOnly = false;
            txtAdd2.ReadOnly = false;
            txtPh.ReadOnly = false;
            txtFax.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtCP.ReadOnly = false;
            txtECC.ReadOnly = false;
            txtCommission.ReadOnly = false;
            txtDivision.ReadOnly = false;
            txtRange.ReadOnly = false;
            txtRAdd1.ReadOnly = false;
            txtRAdd2.ReadOnly = false;
            txtRCity.ReadOnly = false;
            txtRPin.ReadOnly = false;
            txtLSTregnNo.ReadOnly = false;
            txtCSTregnNo.ReadOnly = false;
            txtST.ReadOnly = false;
            txtTinNo.ReadOnly = false;

            dtpLSTLicenseDate.ReadOnly = false;
            dtpCSTLicenseDate.ReadOnly = false;
            dtpST.ReadOnly = false;
            dtpTINDate.ReadOnly = false;

            ddlSupplierType.Enabled = true;
            ddlCategory.Enabled = true;
            ddlMfrDlr.Enabled = true;

            LOVState.ReadOnly = false;
            LOVCountry.ReadOnly = false;
        }
        private void pBindDDLSupplier()
        {
            ddlSupplierType.Items.Clear();

            ListItem item = new ListItem("Vendor", "V");
            ddlSupplierType.Items.Add(item);
            item = new ListItem("Sub Contractor", "S");
            ddlSupplierType.Items.Add(item);
            item = new ListItem("Client", "C");
            ddlSupplierType.Items.Add(item);
            item = new ListItem("Both", "B");
            ddlSupplierType.Items.Add(item);

            ddlSupplierType.ClearSelection();
        }
        private void pBindDDLCategory()
        {
            ddlCategory.Items.Clear();

            ListItem item = new ListItem("Import", "M");
            ddlCategory.Items.Add(item);
            item = new ListItem("Local", "L");
            ddlCategory.Items.Add(item);
            item = new ListItem("IUT", "I");
            ddlCategory.Items.Add(item);

            ddlCategory.ClearSelection();
        }
        private void pBindDDLMfrDlr()
        {
            ddlMfrDlr.Items.Clear();

            ListItem item = new ListItem("Select", "");
            ddlMfrDlr.Items.Add(item);
            item = new ListItem("Manufacturer", "M");
            ddlMfrDlr.Items.Add(item);
            item = new ListItem("Dealer", "D");
            ddlMfrDlr.Items.Add(item);

            ddlMfrDlr.ClearSelection();
        }
        private void pBindControls()
        {
            mySupplier = (Model.Masters.SupplierInfo)ViewState[TRAN_ID_KEY];

            txtName.Text = mySupplier.Name;
            txtShortName.Text = mySupplier.ShortName;
            txtAdd.Text = mySupplier.Address1;
            txtAdd1.Text = mySupplier.Address2;
            txtAdd2.Text = mySupplier.Address3;
            txtCity.Text = mySupplier.City;
            txtPin.Text = mySupplier.Pin;
            txtPh.Text = mySupplier.Phone;
            txtFax.Text = mySupplier.Fax;
            txtEmail.Text = mySupplier.EMail;
            txtCP.Text = mySupplier.Contact;
            txtECC.Text = mySupplier.ExRegnNo;
            txtCommission.Text = mySupplier.Commissionerate;
            txtDivision.Text = mySupplier.Division;
            txtRange.Text = mySupplier.Range;
            txtRAdd1.Text = mySupplier.RangeAddress1;
            txtRAdd2.Text = mySupplier.RangeAddress2;
            txtRCity.Text = mySupplier.RangeCity;
            txtRPin.Text = mySupplier.RangePinCode;
            txtLSTregnNo.Text = mySupplier.LSTRegnNo;
            txtCSTregnNo.Text = mySupplier.CSTRegnNo;
            txtST.Text = mySupplier.STNo;
            txtTinNo.Text = mySupplier.TINNo;

            dtpLSTLicenseDate.CalendarDate = mySupplier.LSTRegnDate.ToString("dd-MMM-yyyy");
            dtpCSTLicenseDate.CalendarDate = mySupplier.CSTRegnDate.ToString("dd-MMM-yyyy");
            dtpST.CalendarDate = mySupplier.STDate.ToString("dd-MMM-yyyy");
            dtpTINDate.CalendarDate = mySupplier.TINDate.ToString("dd-MMM-yyyy");

            ddlSupplierType.ClearSelection();
            if (mySupplier.SupplierType.Length != 0)
            {
                ListItem lstItem = ddlSupplierType.Items.FindByValue(mySupplier.SupplierType.ToUpper());
                if (lstItem != null)
                    lstItem.Selected = true;
            }

            ddlCategory.ClearSelection();
            if (mySupplier.ProcType.Length != 0)
            {
                ListItem lstItem = ddlCategory.Items.FindByValue(mySupplier.ProcType.ToUpper());
                if (lstItem != null)
                    lstItem.Selected = true;
            }

            ddlMfrDlr.ClearSelection();
            if (mySupplier.MfrDlr.Length != 0)
            {
                ListItem lstitem = ddlMfrDlr.Items.FindByValue(mySupplier.MfrDlr.ToString());
                if (lstitem != null)
                    lstitem.Selected = true;
            }

            if (mySupplier.State == null)
                LOVState.ClearAll();
            else
            {
                LOVState.strFirstColumn = mySupplier.State.Name;
                LOVState.strLastColumn = mySupplier.State.SlNo.ToString();
            }

            if (mySupplier.Country == null)
                LOVCountry.ClearAll();
            else
            {
                LOVCountry.strFirstColumn = mySupplier.Country.Name;
                LOVCountry.strLastColumn = mySupplier.Country.SlNo.ToString();
            }
        }
        private void pMapControls()
        {
            mySupplier = (Model.Masters.SupplierInfo)ViewState[TRAN_ID_KEY];


            mySupplier.BranchID = LoginUserInfo.BranchID;
            mySupplier.Name = WebComponents.CleanString.InputText(txtName.Text, txtName.MaxLength);
            mySupplier.ShortName = WebComponents.CleanString.InputText(txtShortName.Text, txtShortName.MaxLength);
            mySupplier.Address1 = WebComponents.CleanString.InputText(txtAdd.Text, txtAdd.MaxLength);
            mySupplier.Address2 = WebComponents.CleanString.InputText(txtAdd1.Text, txtAdd1.MaxLength);
            mySupplier.Address3 = WebComponents.CleanString.InputText(txtAdd2.Text, txtAdd2.MaxLength);
            mySupplier.City = WebComponents.CleanString.InputText(txtCity.Text, txtCity.MaxLength);
            mySupplier.Pin = WebComponents.CleanString.InputText(txtPin.Text, txtPin.MaxLength);
            mySupplier.Phone = WebComponents.CleanString.InputText(txtPh.Text, txtPh.MaxLength);
            mySupplier.Fax = WebComponents.CleanString.InputText(txtFax.Text, txtFax.MaxLength);
            mySupplier.EMail = WebComponents.CleanString.InputText(txtEmail.Text, txtEmail.MaxLength);
            mySupplier.Contact = WebComponents.CleanString.InputText(txtCP.Text, txtCP.MaxLength);
            mySupplier.ExRegnNo = WebComponents.CleanString.InputText(txtECC.Text, txtECC.MaxLength);
            mySupplier.Commissionerate = WebComponents.CleanString.InputText(txtCommission.Text, txtCommission.MaxLength);
            mySupplier.Division = WebComponents.CleanString.InputText(txtDivision.Text, txtDivision.MaxLength);
            mySupplier.Range = WebComponents.CleanString.InputText(txtRange.Text, txtRange.MaxLength);
            mySupplier.RangeAddress1 = WebComponents.CleanString.InputText(txtRAdd1.Text, txtRAdd1.MaxLength);
            mySupplier.RangeAddress2 = WebComponents.CleanString.InputText(txtRAdd2.Text, txtRAdd2.MaxLength);
            mySupplier.RangeCity = WebComponents.CleanString.InputText(txtRCity.Text, txtRCity.MaxLength);
            mySupplier.RangePinCode = WebComponents.CleanString.InputText(txtRPin.Text, txtRPin.MaxLength);
            mySupplier.LSTRegnNo = WebComponents.CleanString.InputText(txtLSTregnNo.Text, txtLSTregnNo.MaxLength);
            mySupplier.CSTRegnNo = WebComponents.CleanString.InputText(txtCSTregnNo.Text, txtCSTregnNo.MaxLength);
            mySupplier.STNo = WebComponents.CleanString.InputText(txtST.Text, txtST.MaxLength);
            mySupplier.TINNo = WebComponents.CleanString.InputText(txtTinNo.Text, txtTinNo.MaxLength);

            mySupplier.LSTRegnDate = Convert.ToDateTime(dtpLSTLicenseDate.CalendarDate);
            mySupplier.CSTRegnDate = Convert.ToDateTime(dtpCSTLicenseDate.CalendarDate);
            mySupplier.STDate = Convert.ToDateTime(dtpST.CalendarDate);
            mySupplier.TINDate = Convert.ToDateTime(dtpTINDate.CalendarDate);

            mySupplier.SupplierType = ddlSupplierType.SelectedValue.ToString();
            mySupplier.ProcType = ddlCategory.SelectedValue.ToString();
            mySupplier.MfrDlr = ddlMfrDlr.SelectedValue.ToString();

            if (LOVState.strLastColumn.Length == 0)
                mySupplier.State = null;
            else
                mySupplier.State = SQLServerDAL.Masters.State.GetStateInfo(Convert.ToInt32(LOVState.strLastColumn));

            if (LOVCountry.strLastColumn.Length == 0)
                mySupplier.Country = null;
            else
                mySupplier.Country = SQLServerDAL.Masters.Country.GetCountryInfo(Convert.ToInt32(LOVCountry.strLastColumn));

            ViewState[TRAN_ID_KEY] = mySupplier;
        }
        private void LOVState_LOVAfterClick(object sender, EventArgs e)
        {
            if (LOVState.strLastColumn.Length != 0)
            {
                Model.Masters.StateInfo myState = SQLServerDAL.Masters.State.GetStateInfo(Convert.ToInt32(LOVState.strLastColumn));

                if (myState.CountryInfo == null)
                    LOVCountry.ClearAll();
                else
                {
                    LOVCountry.strFirstColumn = myState.CountryInfo.Name;
                    LOVCountry.strLastColumn = myState.CountryInfo.SlNo.ToString();
                }
            }
        }
        protected void btnSupplier_CancelButtonClick(object sender, EventArgs e)
        {
            pBacktoGrid();
        }
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuId=" + MenuID;
            Response.Redirect(url, true);
        }

        protected void btnSupplier_SubmitButtonClick(object sender, EventArgs e)
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
                    btnSupplier.Status = "Deletion not possible...!";
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
            mySupplier = (Model.Masters.SupplierInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.Supplier.blnCheckforDelete(mySupplier.SlNo, BranchType == "" ? "STP" : BranchType))
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
                if (txtAdd.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Address is required!";
                    lblnReturnValue = false;
                }
                //if (txtEmail.Text.Trim().Length == 0 && lblnReturnValue)
                //{
                //    lblMessage.Text = "Email is required!";
                //    lblnReturnValue = false;
                //}
                if (lblnReturnValue)
                {
                    mySupplier = (Model.Masters.SupplierInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && mySupplier.SlNo == 0)
                    {
                        lblMessage.Text = "Supplier not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.Supplier.blnCheckSupplier(mySupplier))
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
                btnSupplier.Status = "Check your entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
        private void pSave()
        {
            try
            {
                mySupplier = (Model.Masters.SupplierInfo)ViewState[TRAN_ID_KEY];

                pMapControls();

                SQLServerDAL.Masters.Supplier.Insert(LoginBranchID, mySupplier, LoginUserInfo.UserID, MenuID);
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
                mySupplier = (Model.Masters.SupplierInfo)ViewState[TRAN_ID_KEY];

                pMapControls();

                SQLServerDAL.Masters.Supplier.Update(LoginBranchID, mySupplier, LoginUserInfo.UserID, MenuID);
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
                mySupplier = (Model.Masters.SupplierInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Supplier.Delete(mySupplier.SlNo, LoginBranchID, mySupplier, LoginUserInfo.UserID, MenuID);
            }
            catch
            {
                throw;
            }
        }

        protected void btnSupplier_DeleteButtonClick(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Delete";
            pLockControls();
        }

        protected void btnSupplier_EditButtonClick(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Modify";
            pUnLockControls();
        }
    }
}
