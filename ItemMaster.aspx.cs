using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using ISPL.CSC.Model;
using ISPL.CSC.SQLServerDAL;

namespace ISPL.CSC.Web.Masters
{
    public partial class ItemMaster : BasePage
    {
        private const string TRAN_ID_KEY = "TRAN_ID_KEY";
        private const string STATUS_KEY = "STATUS_KEY";
        private const string BRANCH_KEY = "BRANCH_KEY";

        private Model.Masters.ItemMasterInfo myItemMasterInfo = null;
               
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            btnItem.Status = "";
            if (!IsPostBack)
            {
                pDispHeading();
                pBindDDL();

                myItemMasterInfo = SQLServerDAL.Masters.ItemMaster.GetItemMaster(Convert.ToInt32(Request["ID"].ToString()));

                if (myItemMasterInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = myItemMasterInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new Model.Masters.ItemMasterInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                pSetUserControls();
            }
            btnItem.MenuID = MenuID;
            btnItem.ButtonClicked = ViewState[STATUS_KEY].ToString();
        }
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pSetUserControls()
        {
            LOVUOM.Query = "SELECT UOM, UOMDesc as Description, UomCd FROM M_UOM ";
            LOVUOM.Required = true;

            ddlItemType.TabIndex = 1;
            txtItemName.TabIndex = 2;
            txtPartCode.TabIndex = 3;
            txtGenDesc.TabIndex = 4;
            LOVUOM.TabIndex = 5;
            btnItem.TabIndex = 7;
        } 
        private void pBindDDL()
        {
            ListItem lstItem;

            lstItem = new ListItem("Capital Goods", "CG");
            ddlItemType.Items.Add(lstItem);

            lstItem = new ListItem("Raw Materials", "RM");
            ddlItemType.Items.Add(lstItem);

            lstItem = new ListItem("Consumables", "CB");
            ddlItemType.Items.Add(lstItem);

            lstItem = new ListItem("Spares for Capital Goods", "SC");
            ddlItemType.Items.Add(lstItem);

            if (BranchType != "STP")
            {
                lstItem = new ListItem("Sub Assembly", "SA");
                ddlItemType.Items.Add(lstItem);
            }
        }
        private void pClearControls()
        {
            txtItemName.Text = "";
            txtGenDesc.Text = "";
            txtPartCode.Text = "";
            ddlItemType.ClearSelection();
            LOVUOM.ClearAll();
        }
        private void pLockControls()
        {
            ddlItemType.Enabled = false;
            txtItemName.ReadOnly = true;
            txtGenDesc.ReadOnly = true;
            txtPartCode.ReadOnly = true;
            LOVUOM.ReadOnly = true;
        }
        private void pUnLockControls()
        {
            ddlItemType.Enabled = ViewState[STATUS_KEY].ToString() == "Add" ? true : false;

            txtItemName.ReadOnly = false;
            txtGenDesc.ReadOnly = false;
            txtPartCode.ReadOnly = false;
            LOVUOM.ReadOnly = false;
        }
        private void pBindControls()
        {
            myItemMasterInfo = (Model.Masters.ItemMasterInfo)ViewState[TRAN_ID_KEY];

            txtItemName.Text = myItemMasterInfo.ItemName;
            txtGenDesc.Text = myItemMasterInfo.ItemDesc;
            txtPartCode.Text = myItemMasterInfo.PartCode;

            if (myItemMasterInfo.UOM == null)
                LOVUOM.ClearAll();
            else
            {
                LOVUOM.strFirstColumn = myItemMasterInfo.UOM.Uom;
                LOVUOM.strLastColumn = myItemMasterInfo.UOM.SlNo.ToString();
            }

            ddlItemType.ClearSelection();
            if (myItemMasterInfo.ItemType == "")
                ddlItemType.ClearSelection();
            else
            {
                ListItem lstItem = ddlItemType.Items.FindByValue(myItemMasterInfo.ItemType);
                if (lstItem != null)
                    lstItem.Selected = true;
            }
            ViewState[TRAN_ID_KEY] = myItemMasterInfo;
        }
        private void pMapControls()
        {
            myItemMasterInfo = (Model.Masters.ItemMasterInfo)ViewState[TRAN_ID_KEY];

            myItemMasterInfo.BranchID = LoginUserInfo.BranchID;

            myItemMasterInfo.ItemName = Web.WebComponents.CleanString.InputText(txtItemName.Text, txtItemName.MaxLength);
            myItemMasterInfo.ItemDesc = Web.WebComponents.CleanString.InputText(txtGenDesc.Text, txtGenDesc.MaxLength);
            myItemMasterInfo.PartCode = Web.WebComponents.CleanString.InputText(txtPartCode.Text, txtPartCode.MaxLength);
            myItemMasterInfo.ItemType = ddlItemType.SelectedValue;

            if (LOVUOM.strLastColumn.Length == 0)
                myItemMasterInfo.UOM = null;
            else
            {
                Model.Masters.UOMInfo myUom = SQLServerDAL.Masters.UOM.GetUOMInfo(Convert.ToInt32(LOVUOM.strLastColumn));
                myItemMasterInfo.UOM = myUom;
            }
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
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuId=" + MenuID;
            Response.Redirect(url, true);
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
                    btnItem.Status = "Deletion not possible...!";
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
        private bool fblnValidDelete()
        {
            myItemMasterInfo = (Model.Masters.ItemMasterInfo)ViewState[TRAN_ID_KEY];
            string BranchType = base.BranchType;

            if (SQLServerDAL.Masters.ItemMaster.blnCheckforDelete(myItemMasterInfo.SlNo, BranchType == "" ? "STP" : BranchType))
                return true;
            else
                return false;
        }
        private bool fblnValidEntry()
        {
            bool lblnReturnValue = true;

            if (Page.IsValid)
            {
                if (txtItemName.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "ItemName is required!";
                    lblnReturnValue = false;
                }
                if (txtGenDesc.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "General Description is required!";
                    lblnReturnValue = false;
                }

                if (txtPartCode.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    if (ddlItemType.SelectedValue == "RM" || ddlItemType.SelectedValue == "CB" || ddlItemType.SelectedValue == "SA")
                    {
                        lblMessage.Text = "Part Code is Required";
                        lblnReturnValue = false;
                    }
                }
                if (lblnReturnValue)
                {
                    myItemMasterInfo = (Model.Masters.ItemMasterInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && myItemMasterInfo.SlNo == 0)
                    {
                        btnItem.Status = "Item not found...!";
                        return false;
                    }
                    if (SQLServerDAL.Masters.ItemMaster.blnCheckItem(myItemMasterInfo, LoginUserInfo.BranchID))
                        return true;
                    else
                    {
                        btnItem.Status = "Duplicate Entry...!";
                        return false;
                    }
                }
            }
            else
            {
                btnItem.Status = "Check ur entries...!";
                return false;
            }
            return lblnReturnValue;
        }
        private void pSave()
        {
            try
            {
                pMapControls();

                SQLServerDAL.Masters.ItemMaster.Insert(myItemMasterInfo);
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

                SQLServerDAL.Masters.ItemMaster.Update(myItemMasterInfo);
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
                myItemMasterInfo = (Model.Masters.ItemMasterInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.ItemMaster.Delete(myItemMasterInfo.SlNo);
            }
            catch
            {
                throw;
            }
        }
        protected void imgContinue_Click(object sender, ImageClickEventArgs e)
        {
            myItemMasterInfo = (Model.Masters.ItemMasterInfo)ViewState[TRAN_ID_KEY];

            string url = "General/DocumentAttachment.aspx?MenuID=" + MenuID + "&ID=" + myItemMasterInfo.SlNo.ToString() + "&TranType=ITEM";

            Response.Redirect(url, true);
        }
        protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(3000);

            //if (ddlItemType.SelectedValue.ToString() == "SA")
            //    imgContinue.Visible = true;
            //else
            //    imgContinue.Visible = false;
        }
    }
}
