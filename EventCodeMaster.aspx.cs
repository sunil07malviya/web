using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.UI;

using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class EventCodeMaster : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private EventInfo myEventInfo = null;
        private void Page_Load(object sender, EventArgs e)
        {
            btnEventCode.Status = "";

            if (!IsPostBack)
            {
                pDispHeading();

                myEventInfo = SQLServerDAL.Masters.EventCodes.GetEventInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myEventInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myEventInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new EventInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnEventCode.MenuID = MenuID;
                btnEventCode.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myEventInfo = (Model.Masters.EventInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtValuekey.Text = myEventInfo.ValueKey;
                txtValueText.Text = myEventInfo.Text;
                txtValueType.Text = myEventInfo.ValueType;
           
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            myEventInfo = (EventInfo)ViewState[TRAN_ID_KEY];

            try
            {
                myEventInfo.ValueKey = WebComponents.CleanString.InputText(txtValuekey.Text, txtValuekey.MaxLength);
                myEventInfo.Text = WebComponents.CleanString.InputText(txtValueText.Text, txtValueText.MaxLength);
                myEventInfo.ValueType = WebComponents.CleanString.InputText(txtValueType.Text, txtValueType.MaxLength);

                ViewState[TRAN_ID_KEY] = myEventInfo;
            }
            catch
            {
                throw;
            }
        }
        private void pClearControls()
        {
            txtValuekey.Text = "";
            txtValueText.Text = "";
            txtValueType.Text = "";

        }
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuId=" + MenuID;
            Response.Redirect(url, true);
        }
        private void pUnLockControls()
        {
            txtValuekey.ReadOnly = false;
            txtValueText.ReadOnly = false;
            txtValueType.ReadOnly = false;
        }
        private void pLockControls()
        {
            txtValuekey.ReadOnly = true;
            txtValueText.ReadOnly = true;
            txtValueType.ReadOnly = true;
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
                    btnEventCode.Status = "Deletion not possible...!";
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

                myEventInfo = (EventInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.EventCodes.Insert(myEventInfo);
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

                myEventInfo = (EventInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.EventCodes.Update(myEventInfo);
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
                myEventInfo = (EventInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.EventCodes.Delete(myEventInfo.SlNo);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myEventInfo = (EventInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.EventCodes.blnCheckDelete(myEventInfo.ValueKey))
                return true;
            else
                return false;
        }
        private bool fblnValidEntry()
        {
            bool lblnReturnValue = true;

            if (Page.IsValid)
            {
                if (txtValuekey.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Short Name is required!";
                    lblnReturnValue = false;
                }
                if (txtValueText.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Description is required!";
                    lblnReturnValue = false;
                }
                
                if (lblnReturnValue)
                {
                    myEventInfo = (EventInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myEventInfo.SlNo == 0)
                    {
                        lblMessage.Text = "Eventinfo not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.EventCodes.blnCheckEventInfo(myEventInfo))
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
                btnEventCode.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
