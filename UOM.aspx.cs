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
using ISPL.CSC.Model;
using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class UOM : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private UOMInfo myUOMInfo = null;

        private void Page_Load(object sender, System.EventArgs e)
        {
            btnUOM.Status = "";

            if (!IsPostBack)
            {
                pDispHeading();

                myUOMInfo = SQLServerDAL.Masters.UOM.GetUOMInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myUOMInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myUOMInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new UOMInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnUOM.MenuID = MenuID;
                btnUOM.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myUOMInfo = (Model.Masters.UOMInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtName.Text = myUOMInfo.UomDescription;
                txtShortName.Text = myUOMInfo.Uom;
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myUOMInfo = (UOMInfo)ViewState[TRAN_ID_KEY];

            try
            {
                myUOMInfo.Uom = WebComponents.CleanString.InputText(txtShortName.Text, txtShortName.MaxLength);
                myUOMInfo.UomDescription = WebComponents.CleanString.InputText(txtName.Text, txtName.MaxLength);

                ViewState[TRAN_ID_KEY] = myUOMInfo;
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
        }
        private void pLockControls()
        {
            txtShortName.ReadOnly = true;
            txtName.ReadOnly = true;
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
                    btnUOM.Status = "Deletion not possible...!";
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

                myUOMInfo = (UOMInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.UOM.Insert(myUOMInfo);
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

                myUOMInfo = (UOMInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.UOM.Update(myUOMInfo);
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
                myUOMInfo = (UOMInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.UOM.Delete(myUOMInfo.SlNo);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myUOMInfo = (UOMInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.UOM.blnCheckforDelete(myUOMInfo.SlNo))
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
                    myUOMInfo = (UOMInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myUOMInfo.SlNo == 0)
                    {
                        lblMessage.Text = "UOM not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.UOM.blnCheckUOM(myUOMInfo))
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
                btnUOM.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}