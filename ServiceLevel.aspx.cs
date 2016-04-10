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
    public partial class ServiceLevel : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private Model.Masters.ServiceInfo myService = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            bcService.Status = "";

            bcService.EditButtonClick += new EventHandler(this.bcActivity_EditButton);
            bcService.CancelButtonClick += new EventHandler(this.bcActivity_CancelButton);
            bcService.DeleteButtonClick += new EventHandler(this.bcActivity_DeleteButton);
            bcService.SubmitButtonClick += new EventHandler(this.bcActivity_SubmitButton);

            if (!IsPostBack)
            {
                pDispHeading();

                myService = SQLServerDAL.Masters.Service.GetService(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myService != null)
                {
                    ViewState[TRAN_ID_KEY] = myService;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new Model.Masters.ServiceInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                bcService.MenuID = MenuID;
                bcService.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myService = (Model.Masters.ServiceInfo)ViewState[TRAN_ID_KEY];
            try
            {
                txtName.Text = myService.Name;
                txtShortName.Text = myService.ShortName;
            }
            catch
            {
                throw;
            }
        }

        private void pMapControls()
        {
            myService = (Model.Masters.ServiceInfo)ViewState[TRAN_ID_KEY];

            myService.Name = WebComponents.CleanString.InputText(txtName.Text, txtName.MaxLength);
            myService.ShortName = WebComponents.CleanString.InputText(txtShortName.Text, txtShortName.MaxLength);

            ViewState[TRAN_ID_KEY] = myService;
        }

        private void pClearControls()
        {
            txtName.Text = "";
            txtShortName.Text = "";
        }
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuId=" + MenuID;
            Response.Redirect(url, true);
        }
        private void pUnLockControls()
        {
            txtName.ReadOnly = false;
            txtShortName.ReadOnly = false;
        }
        private void pLockControls()
        {
            txtName.ReadOnly = true;
            txtShortName.ReadOnly = true;
        }
        private void bcActivity_EditButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Modify";
            pUnLockControls();
        }
        private void bcActivity_DeleteButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Delete";
            pLockControls();
        }
        private void bcActivity_CancelButton(object sender, EventArgs e)
        {
            pBacktoGrid();
        }
        private void bcActivity_SubmitButton(object sender, EventArgs e)
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
                    bcService.Status = "Deletion not possible...!";
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
                myService = (Model.Masters.ServiceInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Service.Insert(myService);
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
                myService = (Model.Masters.ServiceInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Service.Update(myService);
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
                myService = (Model.Masters.ServiceInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Service.Delete(myService.SlNo);
            }
            catch
            {
                throw;
            }
        }

        private bool fblnValidEntry()
        {
            if (Page.IsValid)
            {
                myService = (Model.Masters.ServiceInfo)ViewState[TRAN_ID_KEY];

                if (ViewState[STATUS_KEY].Equals("Modify") && myService.SlNo == 0)
                {
                    bcService.Status = "Activity not found...!";
                    return false;
                }
                if (SQLServerDAL.Masters.Service.blnCheckActivity(myService))
                    return true;
                else
                {
                    bcService.Status = "Duplicate Entry...!";
                    return false;
                }
            }
            else
            {
                bcService.Status = "Check ur entries...!";
                return false;
            }
        }
        private bool fblnValidDelete()
        {
            myService = (Model.Masters.ServiceInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.Service.blnCheckDelete(myService.SlNo))
                return true;
            else
                return false;
        }
    }
}
