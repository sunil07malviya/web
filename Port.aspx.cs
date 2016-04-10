using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ISPL.CSC.Model;
using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class Port : BasePage
    { 
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status"; 

        private PortInfo myPortInfo = null; 

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {   
                pSetUserControls();
                pDispHeading();
                pBindDDL();

                myPortInfo = SQLServerDAL.Masters.Port.GetPortInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myPortInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myPortInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new PortInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
            }
            btnPort.MenuID = MenuID;
            btnPort.ButtonClicked = ViewState[STATUS_KEY].ToString();
        }      
        private void pBindDDL()
        {
            ListItem lstItem;

            lstItem = new ListItem("Air", "A");
            ddlPortType.Items.Add(lstItem);

            lstItem = new ListItem("Sea", "O");
            ddlPortType.Items.Add(lstItem);

            lstItem = new ListItem("Surface", "S");
            ddlPortType.Items.Add(lstItem);

            lstItem = new ListItem("Multi-Modal", "M");
            ddlPortType.Items.Add(lstItem);
        }
        private void pDispHeading()
        { 
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pSetUserControls()
        {
            LOVCountry.TextWidth = 176;
            LOVCountry.Query = " SELECT M_COUNTRY_NAME as Country, M_COUNTRY_SNAME as ShortName , M_Country_Code FROM M_COUNTRY ";

            txtName.TabIndex = 1;
            txtShortName.TabIndex = 2;
            ddlPortType.TabIndex = 3;
            LOVCountry.TabIndex = 4;
            txtNoofDays.TabIndex = 6;
            btnPort.TabIndex = 7;
        } 
        private void pBindControls()
        {
            myPortInfo = (Model.Masters.PortInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtShortName.Text = myPortInfo.ShortName;
                txtName.Text = myPortInfo.Name;
                txtNoofDays.Text = myPortInfo.NoofDays.ToString();

                ddlPortType.ClearSelection();
                if (myPortInfo.PortType == "")
                    ddlPortType.ClearSelection();
                else
                {
                    ListItem lstItem = ddlPortType.Items.FindByValue(myPortInfo.PortType);
                    if (lstItem != null)
                        lstItem.Selected = true;
                }

                if (myPortInfo.Country == null)
                    LOVCountry.ClearAll();
                else
                {
                    LOVCountry.strFirstColumn = myPortInfo.Country.Name;
                    LOVCountry.strLastColumn = myPortInfo.Country.SlNo.ToString();
                }
                ViewState[TRAN_ID_KEY] = myPortInfo;
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myPortInfo = (PortInfo)ViewState[TRAN_ID_KEY];
            try
            {
                myPortInfo.Name = WebComponents.CleanString.InputText(txtName.Text, txtName.MaxLength);
                myPortInfo.ShortName = WebComponents.CleanString.InputText(txtShortName.Text, txtShortName.MaxLength);

                myPortInfo.PortType = ddlPortType.SelectedValue;

                if (LOVCountry.strLastColumn.Length == 0)
                    myPortInfo.Country = null;
                else
                {
                    CountryInfo myCountry = SQLServerDAL.Masters.Country.GetCountryInfo(Convert.ToInt32(LOVCountry.strLastColumn));
                    myPortInfo.Country = myCountry;
                }
                myPortInfo.NoofDays = Convert.ToInt32(txtNoofDays.Text);
            }
            catch
            {
                throw;
            }
        }
        private void pClearControls()
        {
            txtShortName.Text = "";
            txtName.Text = "";
            LOVCountry.ClearAll();
            txtNoofDays.Text = "0";
            ddlPortType.ClearSelection();
        }
        private void pLockControls()
        {
            txtShortName.ReadOnly = true;
            txtName.ReadOnly = true;
            LOVCountry.ReadOnly = true;
            txtNoofDays.ReadOnly = true;
            ddlPortType.Enabled = false;
        }
        private void pUnLockControls()
        {
            txtShortName.ReadOnly = false;
            txtName.ReadOnly = false;
            LOVCountry.ReadOnly = false;
            txtNoofDays.ReadOnly = false;
            ddlPortType.Enabled = true;
        }
        protected void Page_EditButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Modify";
            pUnLockControls();
        }
        protected void Page_CancelButton(object sender, EventArgs e)
        {
            pBacktoGrid();
        }
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuId=" + MenuID;
            Response.Redirect(url, true);
        }
        protected void Page_DeleteButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Delete";
            pLockControls();
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
                    btnPort.Status = "Deletion not possible...!";
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
            myPortInfo = (PortInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.Port.blnCheckforDelete(myPortInfo.SlNo))
                return true;
            else
                return false;
        }
        private bool fblnValidEntry()
        {
            if (Page.IsValid)
            {
                myPortInfo = (PortInfo)ViewState[TRAN_ID_KEY];

                if (ViewState[STATUS_KEY].Equals("Modify") && myPortInfo.SlNo == 0)
                {
                    btnPort.Status = "State not found...!";
                    return false;
                }
                if (SQLServerDAL.Masters.Port.blnCheckPort(myPortInfo))
                    return true;
                else
                {
                    btnPort.Status = "Duplicate Entry...!";
                    return false;
                }
            }
            else
            {
                btnPort.Status = "Check ur entries...!";
                return false;
            }
        }
        private void pSave()
        {
            try
            {
                myPortInfo = (PortInfo)ViewState[TRAN_ID_KEY];                

                pMapControls();

                SQLServerDAL.Masters.Port.Insert(myPortInfo);
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
                myPortInfo = (PortInfo)ViewState[TRAN_ID_KEY];                

                pMapControls();

                SQLServerDAL.Masters.Port.Update(myPortInfo);
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
                myPortInfo = (PortInfo)ViewState[TRAN_ID_KEY];                

                SQLServerDAL.Masters.Port.Delete(myPortInfo.SlNo);
            }
            catch
            {
                throw;
            }
        }
    }
}