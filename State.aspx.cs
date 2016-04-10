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
    public partial class State : BasePage
    {
        private StateInfo myStateInfo = null;

        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                pSetUserControls();

                pDispHeading();

                myStateInfo = SQLServerDAL.Masters.State.GetStateInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myStateInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myStateInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new StateInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
            }
            btnState.MenuID = MenuID;
            btnState.ButtonClicked = ViewState[STATUS_KEY].ToString();
        }
        private void pSetUserControls()
        {
            //LOVCountry.TextWidth = 176;
            LOVCountry.Query = "SELECT M_COUNTRY_NAME [Country Name], M_COUNTRY_SNAME [Short Name], M_COUNTRY_CODE FROM M_COUNTRY";
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myStateInfo = (Model.Masters.StateInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtShortName.Text = myStateInfo.ShortName;
                txtName.Text = myStateInfo.Name;

                if (myStateInfo.CountryInfo == null)
                {
                    LOVCountry.ClearAll(); ;
                }
                else
                {
                    LOVCountry.strFirstColumn = myStateInfo.CountryInfo.Name;
                    LOVCountry.strLastColumn = myStateInfo.CountryInfo.SlNo.ToString();
                }
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myStateInfo = (StateInfo)ViewState[TRAN_ID_KEY];
            try
            {
                myStateInfo.Name = WebComponents.CleanString.InputText(txtName.Text, txtName.MaxLength);
                myStateInfo.ShortName = WebComponents.CleanString.InputText(txtShortName.Text, txtShortName.MaxLength);

                if (LOVCountry.strLastColumn.Length == 0)
                    myStateInfo.CountryInfo = null;
                else
                {
                    CountryInfo myCountryInfo = SQLServerDAL.Masters.Country.GetCountryInfo(Convert.ToInt32(LOVCountry.strLastColumn));
                    myStateInfo.CountryInfo = myCountryInfo;
                }
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
        }
        private void pLockControls()
        {
            txtShortName.ReadOnly = true;
            txtName.ReadOnly = true;
            LOVCountry.ReadOnly = true;
        }
        private void pUnLockControls()
        {
            txtShortName.ReadOnly = false;
            txtName.ReadOnly = false;
            LOVCountry.ReadOnly = false;
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
                    btnState.Status = "Deletion not possible...!";
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
            myStateInfo = (StateInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.State.blnCheckforDelete(myStateInfo.SlNo))
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
                    myStateInfo = (StateInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myStateInfo.SlNo == 0)
                    {
                        lblMessage.Text = "State not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.State.blnCheckState(myStateInfo))
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
                btnState.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
        private void pSave()
        {
            try
            {
                pMapControls();

                myStateInfo = (StateInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.State.Insert(myStateInfo);
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

                myStateInfo = (StateInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.State.Update(myStateInfo);
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
                myStateInfo = (StateInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.State.Delete(myStateInfo.SlNo);
            }
            catch
            {
                throw;
            }
        }
    }
}
