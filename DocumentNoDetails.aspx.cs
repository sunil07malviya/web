using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISPL.CSC.Web.Masters
{
    public partial class DocumentNoDetails : BasePage
    {        
        private const string STATUS_KEY = "STATUS_KEY";        
        private const string FLAG_KEY = "FLAG_KEY";
        private const string MSLNO = "MSLNO";
        private const string HEADER_KEY = "HEADER_KEY";
        private const string DETAIL_KEY = "DETAIL_KEY";
        private const string TAB_KEY = "tab";

        private Model.AutoNumberHdrInfo myHeader = null;
        private Model.AutoNumberDtlInfo myDetail = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btDocDetails.EditButtonClick += new System.EventHandler(this.Page_EditButton);
            this.btDocDetails.CancelButtonClick += new System.EventHandler(this.Page_CancelButton);
            this.btDocDetails.DeleteButtonClick += new System.EventHandler(this.Page_DeleteButton);
            this.btDocDetails.SubmitButtonClick += new System.EventHandler(this.Page_SubmitButton);

            this.LOVNomenclature.LOVAfterClick += new EventHandler(LOVNomenclature_LOVAfterClick);

            if (!IsPostBack)
            {
                //DocControls.Visible = false;

                ViewState[TAB_KEY] = Request["Tab"].ToString();
                ViewState[MSLNO] = Request["MSLNO"].ToString();

                int MSlNo = 0;
                if (Request["MSLNO"] != null)
                    MSlNo = Convert.ToInt32(Request["MSLNO"].ToString());

                int DSlNo = 0;
                if (Request["ID"] != null)
                    DSlNo = Convert.ToInt32(Request["ID"].ToString());

                myHeader = SQLServerDAL.AutoNumberHdr.GetAutoNumberHdrInfo(MSlNo);
                ViewState[HEADER_KEY] = myHeader;

                myDetail = SQLServerDAL.AutoNumberDtl.GetAutoNumberDtlInfo(DSlNo);

                if (myDetail != null)
                {
                    ViewState[DETAIL_KEY] = myDetail;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindDetailData();
                }
                else
                {
                    ViewState[DETAIL_KEY] = new Model.AutoNumberDtlInfo();
                    ViewState[STATUS_KEY] = "Add";
                    ViewState[HEADER_KEY] = new Model.AutoNumberHdrInfo();
                    pClearControls();
                    pUnLockControls();
                }

               // pBindHeaderData();
                pSetUserControls();
            }
        }
        private void pSetUserControls()
        {
            myHeader = (Model.AutoNumberHdrInfo)ViewState[HEADER_KEY];

            LOVNomenclature.Required = true;
            LOVNomenclature.ReadOnly = true;
            LOVNomenclature.Query = "select g_fielddesc_fldnomen, g_fielddesc_slno from g_fielddesc where g_fielddesc_gmenuslno = " + myHeader.Menu.SlNo.ToString() + " and g_fielddesc_slno <> " + myHeader.GFieldDescSlNo.ToString();

            chkPick.Checked = true;
            chkPick.Enabled = false;
        } 
        private void pBindDetailData()
        {
            //int mdSlNo = Convert.ToInt32(dgDocDetails.SelectedItem.Cells[Convert.ToInt32(ViewState[COLUMN_COUNT_KEY].ToString()) - 1].Text);
            int mdSlNo = Convert.ToInt32(Request["ID"].ToString());
            myDetail = SQLServerDAL.AutoNumberDtl.GetAutoNumberDtlInfo(mdSlNo);

            ViewState[DETAIL_KEY] = myDetail;

            if (myDetail.GFieldDescSlNo == 0)
                LOVNomenclature.ClearAll();
            else
            {
                Model.GFieldDescInfo myGFieldDescInfo = SQLServerDAL.GFieldDesc.GetGFieldDesc(myDetail.GFieldDescSlNo);

                if (myGFieldDescInfo == null)
                {
                    LOVNomenclature.ClearAll();
                }
                else
                {
                    LOVNomenclature.strFirstColumn = myGFieldDescInfo.FieldNomenclature;
                    LOVNomenclature.strLastColumn = myGFieldDescInfo.SlNo.ToString();

                    LOVNomenclature_LOVAfterClick(null, null);
                }
            }

            ListItem lstItem = null;
            lstItem = ddlMappedField.Items.FindByValue(myDetail.GFieldDescVal);
            if (lstItem != null)
                lstItem.Selected = true;
        }

        private void pMapControls()
        {
            myDetail = (Model.AutoNumberDtlInfo)ViewState[DETAIL_KEY];
            myHeader = (Model.AutoNumberHdrInfo)ViewState[HEADER_KEY];

            myDetail.MSlNo = myHeader.SlNo;

            if (LOVNomenclature.strLastColumn.Length == 0)
                myDetail.GFieldDescSlNo = 0;
            else
                myDetail.GFieldDescSlNo = Convert.ToInt32(LOVNomenclature.strLastColumn);

            myDetail.GFieldDescVal = ddlMappedField.SelectedItem.Text;

            ViewState[DETAIL_KEY] = myDetail;
        }

        private void pClearControls()
        {
            //LOVNomenclature.ClearAll();
            //chkPick.Checked = false;
            ddlMappedField.ClearSelection();
        }

        private void pLockControls()
        {
            LOVNomenclature.ReadOnly = true;
            chkPick.Enabled = false;
            ddlMappedField.Enabled = false;
        }
        private void pUnLockControls()
        {
            //LOVNomenclature.ReadOnly = false;
            //chkPick.Enabled = true;
            ddlMappedField.Enabled = true;
        }
        private void pSave()
        {
            try
            {
                pMapControls();

                SQLServerDAL.AutoNumberDtl.Insert(myDetail);
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

                SQLServerDAL.AutoNumberDtl.Update(myDetail);
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
                pMapControls();

                SQLServerDAL.AutoNumberDtl.DeleteByDtlID(myDetail.SlNo);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            return true;
        }
        private bool fblnValidEntry()
        {
            if (Page.IsValid)
            {
                myDetail = (Model.AutoNumberDtlInfo)ViewState[DETAIL_KEY];

                if (ViewState[STATUS_KEY].Equals("Modify") && myDetail.SlNo == 0)
                {
                    btDocDetails.Status = "Details not found...!";
                    return false;
                }
                if (SQLServerDAL.AutoNumberDtl.blnCheckAutoNumberDtl(myDetail))
                    return true;
                else
                {
                    btDocDetails.Status = "Duplicate Entry...!";
                    return false;
                }
            }
            else
            {
                btDocDetails.Status = "Check ur entries...!";
                return false;
            }
        }
        private void Page_EditButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Modify";
            pUnLockControls();
        }
        private void Page_DeleteButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Delete";
            pLockControls();
        }
        private void Page_CancelButton(object sender, EventArgs e)
        {
            pBacktoGrid();
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
                    ViewState[DETAIL_KEY] = null;
                    pBacktoGrid();
                    return;
                }
                else
                {
                    btDocDetails.Status = "Deletion not possible...!";
                    return;
                }
            }

            if (fblnValidEntry())
            {
                if (lstrStatus.Equals("New") || lstrStatus.Equals("Add"))
                    pSave();

                if (lstrStatus.Equals("Edit") || lstrStatus.Equals("Modify"))
                    pUpdate();

                ViewState[DETAIL_KEY] = null;               
            }
            pBacktoGrid();
           
        }
        private void pBacktoGrid()
        {
            string redirectCode = "<script>window.parent.location.href = '../General/TabDetails.aspx?MSLNO=" + ViewState[MSLNO].ToString() + "&tab=" + ViewState[TAB_KEY].ToString() + "&MenuID=" + MenuID + "';</script>";
            Response.Write(redirectCode);
        }
        private void LOVNomenclature_LOVAfterClick(object sender, EventArgs e)
        {
            if (LOVNomenclature.strLastColumn.Length > 0)
            {
                Model.GFieldDescInfo myGFieldDescInfo = SQLServerDAL.GFieldDesc.GetGFieldDesc(Convert.ToInt32(LOVNomenclature.strLastColumn));

                string lstrQuery = string.Empty;
                if (myGFieldDescInfo.FieldType == "LOV")
                {
                    lstrQuery = "SELECT " + myGFieldDescInfo.FieldColName + " FROM " + myGFieldDescInfo.FieldTableName;
                    ddlMappedField.DataTextField = myGFieldDescInfo.FieldColName;
                    ddlMappedField.DataValueField = myGFieldDescInfo.FieldColName;
                }
                else
                {
                    lstrQuery = "SELECT gd_fielddesc_value from g_fielddesc, gd_fielddesc where g_fielddesc_slno=gd_fielddesc_mslno and g_fielddesc_slno = " + myGFieldDescInfo.SlNo.ToString();

                    ddlMappedField.DataTextField = "gd_fielddesc_value";
                    ddlMappedField.DataValueField = "gd_fielddesc_value";
                }
                ddlMappedField.DataSource = SQLServerDAL.General.GetDataTable(lstrQuery);
                ddlMappedField.DataBind();
            }
        }
    }
}