using System;

using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class ContainerType : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private ContainerInfo myContainerInfo = null;

        private void Page_Load(object sender, System.EventArgs e)
        {
            btnContanierType.Status = "";

            if (!IsPostBack)
            {
                pDispHeading();

                myContainerInfo = SQLServerDAL.Masters.Container.GetContainerInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (myContainerInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myContainerInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new ContainerInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                pSetUserControls();
                btnContanierType.MenuID = MenuID;
                btnContanierType.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
        }
         
        private void pSetUserControls()
        {  
            LOVDimUOM.Query = "SELECT UOM, UOMDesc as Description, UomCd FROM M_UOM ";
            LOVDimUOM.Required = true;

            txtTypeCode.TabIndex = 1;
            txtDescription.TabIndex = 2;
            txtLength.TabIndex = 3;
            txtBreadth.TabIndex = 4;
            txtHeight.TabIndex = 5;
            LOVDimUOM.TabIndex = 6;
            txtTareWeight.TabIndex = 8;
            txtMaxGrossWt.TabIndex = 9;
            txtMaxCbm.TabIndex = 10;
            btnContanierType.TabIndex = 11;
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            myContainerInfo = (Model.Masters.ContainerInfo)ViewState[TRAN_ID_KEY];

            txtTypeCode.Text = myContainerInfo.Type.ToString();
            txtDescription.Text = myContainerInfo.Description;
            txtLength.Text = myContainerInfo.Length.ToString();
            txtBreadth.Text = myContainerInfo.Breadth.ToString();
            txtHeight.Text = myContainerInfo.Height.ToString();
            
            if (myContainerInfo.UOM == null)
                LOVDimUOM.ClearAll();
            else
            {
                LOVDimUOM.strFirstColumn = myContainerInfo.UOM.Uom;
                LOVDimUOM.strLastColumn = myContainerInfo.UOM.SlNo.ToString();
            }

            txtTareWeight.Text = myContainerInfo.TareWeight.ToString();
            txtMaxGrossWt.Text = myContainerInfo.MaxGrossWeight.ToString();
            txtMaxCbm.Text = myContainerInfo.MaxCBM.ToString();
        }
        private void pMapControls()
        {
            myContainerInfo = (ContainerInfo)ViewState[TRAN_ID_KEY];

            myContainerInfo.Type=txtTypeCode.Text;
            myContainerInfo.Description = WebComponents.CleanString.InputText(txtDescription.Text, txtDescription.MaxLength);
            myContainerInfo.Length = Convert.ToDouble(txtLength.Text.ToString());
            myContainerInfo.Breadth = Convert.ToDouble(txtBreadth.Text.ToString());
            myContainerInfo.Height = Convert.ToDouble(txtHeight.Text.ToString());

            if (LOVDimUOM.strLastColumn.Length == 0)
                myContainerInfo.UOM = null;
            else
            {
                myContainerInfo.UOM = SQLServerDAL.Masters.UOM.GetUOMInfo(Convert.ToInt32(LOVDimUOM.strLastColumn));
            }

            myContainerInfo.TareWeight = Convert.ToDouble(txtTareWeight.Text.ToString());
            myContainerInfo.MaxGrossWeight = Convert.ToDouble(txtMaxGrossWt.Text.ToString());
            myContainerInfo.MaxCBM = Convert.ToDouble(txtLength.Text.ToString());

            ViewState[TRAN_ID_KEY] = myContainerInfo;
        }
        private void pClearControls()
        {
            txtTypeCode.Text = "";
            txtDescription.Text = "";
            txtLength.Text = "";
            txtBreadth.Text = "";
            txtHeight.Text = "";
            LOVDimUOM.ClearAll();
            txtTareWeight.Text = "";
            txtMaxGrossWt.Text = "";
            txtMaxCbm.Text = "";
        }
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuId=" + MenuID;
            Response.Redirect(url, true);
        }
        private void pUnLockControls()
        {
            txtTypeCode.ReadOnly = false;
            txtDescription.ReadOnly = false;
            txtLength.ReadOnly = false;
            txtBreadth.ReadOnly = false;
            txtHeight.ReadOnly = false;
            LOVDimUOM.ReadOnly = false;
            txtTareWeight.ReadOnly = false;
            txtMaxGrossWt.ReadOnly = false;
            txtMaxCbm.ReadOnly = false;
        }
        private void pLockControls()
        {
            txtTypeCode.ReadOnly = true;
            txtDescription.ReadOnly = true;
            txtLength.ReadOnly = true;
            txtBreadth.ReadOnly = true;
            txtHeight.ReadOnly = true;
            LOVDimUOM.ReadOnly = true;
            txtTareWeight.ReadOnly = true;
            txtMaxGrossWt.ReadOnly = true;
            txtMaxCbm.ReadOnly = true;
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
                    btnContanierType.Status = "Deletion not possible...!";
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

                myContainerInfo = (ContainerInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Container.Insert(myContainerInfo);
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

                myContainerInfo = (ContainerInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Container.Update(myContainerInfo);
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
                myContainerInfo = (ContainerInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.Container.Delete(myContainerInfo.Code);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            myContainerInfo = (ContainerInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.Container.blnValidateDelete(myContainerInfo.Code))
                return true;
            else
            return false;
        }
        private bool fblnValidEntry()
        {
            bool lblnReturnValue = true;

            if (Page.IsValid)
            {
                if (txtTypeCode.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Name is required!";
                    lblnReturnValue = false;
                }
                if (txtDescription.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Description is required!";
                    lblnReturnValue = false;
                }
                if (lblnReturnValue)
                {
                    myContainerInfo = (ContainerInfo)ViewState[TRAN_ID_KEY];

                    if (SQLServerDAL.Masters.Container.blnCheckBank(myContainerInfo))
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
                btnContanierType.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
    }
}
