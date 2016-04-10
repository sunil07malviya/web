using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISPL.CSC.Web.Masters
{
    public partial class ExchangeRate : BasePage
    {        
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";
        
        private const string HEADER_KEY = "HEADER_KEY";
        private const string COLUMN_COUNT_KEY = "COLUMN_COUNT_KEY";
        private const string GRID_KEY = "GRID_KEY";
        private const string PAGE_KEY = "PageIndex";

        private Model.Masters.ExchangeRateInfo myExchRate;
        private Model.Masters.CurrencyInfo myCurrency;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.bcExchRate.CancelButtonClick += new System.EventHandler(this.Page_CancelButton);
            this.bcExchRate.EditButtonClick += new System.EventHandler(this.Page_EditButton);
            this.bcExchRate.DeleteButtonClick += new System.EventHandler(this.Page_DeleteButon);
            this.bcExchRate.SubmitButtonClick += new System.EventHandler(this.Page_SubmitButton);
                       
            if (!IsPostBack)
            {
                pSetUserControls();
                
                ViewState[PAGE_KEY] = 0;

                pDispHeading();

                int id = Convert.ToInt32(Request[TRAN_ID_KEY].ToString());

                if (id > 0)
                {
                    myCurrency = SQLServerDAL.Masters.Currency.GetCurrencyInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));                    
                    ViewState[HEADER_KEY] = myCurrency;
                    ViewState[TRAN_ID_KEY] = SQLServerDAL.Masters.ExchangeRate.GetEditableExchangeRate(myCurrency.SlNo);
                    ViewState[STATUS_KEY] = "View";
                    pBindControls();
                    pLockControls();
                }
                else
                {
                    ViewState[HEADER_KEY] = null;
                    ViewState[TRAN_ID_KEY] = new Model.Masters.ExchangeRateInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                    pUnLockControls();
                }

                bcExchRate.MenuID = MenuID;
                bcExchRate.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }
            if (ViewState[HEADER_KEY] != null) pBindGrid();
        }
        private void pSetUserControls()
        {
            bcExchRate.Status = "";
            LOVCurrency1.Query = " SELECT M_Currency_SName as [Short Name], M_Currency_Name as Currency, M_Currency_Code FROM M_CURRENCY ";
            dtpFromDate.Required = true;
        } 
        private void pDispHeading()
        {
            Model.Masters.LOVViewInfo myLOVView = SQLServerDAL.Masters.LOVView.GetLOVViewInfo(Convert.ToInt32(MenuID));
            lblHeading.InnerText = myLOVView.Caption;
        }
        private void pBindGrid()
        {
            Model.Masters.LOVViewInfo myLOVViewInfo = SQLServerDAL.Masters.LOVView.GetLOVViewInfo(Convert.ToInt32(MenuID));
            
            myCurrency = (Model.Masters.CurrencyInfo)ViewState[HEADER_KEY];
            DataTable myTable;
            
            if (ViewState[GRID_KEY] == null)
            {
                myTable = SQLServerDAL.Masters.ExchangeRate.GetExchangeRatesDataTable(myCurrency.SlNo);

                ViewState[GRID_KEY] = myTable;
                ViewState[COLUMN_COUNT_KEY] = myTable.Columns.Count - 1;
            }
            myTable = (DataTable)ViewState[GRID_KEY];

            myTable.DefaultView.RowFilter = "";

            int i = 0;
            dgExchRate.Columns.Clear();
            for (i = 0; i < myTable.Columns.Count; i++)
            {
                DataColumn objDC = myTable.Columns[i];
                BoundField objBC = new BoundField();
                objBC.DataField = objDC.ColumnName;
                objBC.HeaderText = objDC.ColumnName;
                objBC.SortExpression = objDC.ColumnName;
                objBC.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                objBC.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                objBC.HeaderStyle.CssClass = (i == 0 ? "first" : "");
                objBC.ItemStyle.CssClass = (i == 0 ? "first" : "row");
                objBC.DataFormatString = WebComponents.FormatString.InputDataType(objDC.DataType.ToString());

                if (i > 0)
                {
                    string lstrDataType = objDC.DataType.ToString();
                    switch (lstrDataType)
                    {
                        case "System.Int32":
                        case "System.Double":
                        case "System.Decimal":
                            objBC.ItemStyle.CssClass = "money";
                            break;
                        case "System.DateTime":
                        case "System.String":
                        default:
                            objBC.ItemStyle.CssClass = "row";
                            break;
                    }
                }
                objBC.HtmlEncode = false;

                if (i == myTable.Columns.Count - 1) objBC.Visible = false;

                dgExchRate.Columns.Add(objBC);
            }
            dgExchRate.SelectedIndex = -1;

            dgExchRate.AllowPaging = true;
            
            if (myTable.DefaultView.Count == 0)
                dgExchRate.DataSource = AddDummyData(myTable.Clone());
            else
                dgExchRate.DataSource = myTable.DefaultView;

            try
            {
                dgExchRate.DataBind();
            }
            catch
            {
                dgExchRate.PageIndex = 0;
                dgExchRate.DataBind();
            }
            if (myLOVViewInfo.CheckFieldName.Length > 0 && dgExchRate.DataKeyNames.Length == 2)
            {
                int CheckCounter = 0;
                foreach (GridViewRow gvRow in dgExchRate.Rows)
                {
                    if (dgExchRate.DataKeys[gvRow.RowIndex].Values[1].ToString() != "")
                        CheckCounter = Convert.ToInt32(dgExchRate.DataKeys[gvRow.RowIndex].Values[1].ToString());
                    else
                        CheckCounter = 0;

                    if (CheckCounter > 0)
                    {
                        gvRow.BackColor = System.Drawing.Color.LightGoldenrodYellow;
                        gvRow.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        protected void dgExchRate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgExchRate.PageIndex = e.NewPageIndex;

            pBindGrid();
        }
        private void pBindControls()
        {
            myExchRate = (Model.Masters.ExchangeRateInfo)ViewState[TRAN_ID_KEY];
            myCurrency = (Model.Masters.CurrencyInfo)ViewState[HEADER_KEY];

            myExchRate = SQLServerDAL.Masters.ExchangeRate.GetEditableExchangeRate(myCurrency.SlNo);

            ViewState[TRAN_ID_KEY] = myExchRate;

            if (myCurrency == null)
                LOVCurrency1.ClearAll();
            else
            {
                LOVCurrency1.strFirstColumn = myCurrency.ShortName;
                LOVCurrency1.strLastColumn = myCurrency.SlNo.ToString();
            }
            if (myExchRate != null)
            {
                dtpFromDate.CalendarDate = myExchRate.FromDate.ToString("dd-MMM-yyyy");
                txtimprate.Text = myExchRate.ImportExchRate.ToString();
                txtexprate.Text = myExchRate.ExportExchRate.ToString();
                bcExchRate.EditButtonVisible = true;
            }
            else
                bcExchRate.EditButtonVisible = false;
        }
        private void pMapControls()
        {
            myExchRate = (Model.Masters.ExchangeRateInfo)ViewState[TRAN_ID_KEY];

            if (LOVCurrency1.strLastColumn.Length == 0)
                myExchRate.Currency = null;
            else
            {   
                myExchRate.Currency = SQLServerDAL.Masters.Currency.GetCurrencyInfo(Convert.ToInt32(LOVCurrency1.strLastColumn));
            }

            myExchRate.FromDate = Convert.ToDateTime(dtpFromDate.CalendarDate);
            myExchRate.ImportExchRate = Convert.ToDouble(txtimprate.Text);
            myExchRate.ExportExchRate = Convert.ToDouble(txtexprate.Text);

            ViewState[TRAN_ID_KEY] = myExchRate;
        }
        private void pClearControls()
        {
            dtpFromDate.ClearDate();
            txtexprate.Text = "";
            txtimprate.Text = "";
            LOVCurrency1.ClearAll();
        }
        private void pBacktoGrid()
        {
            Response.Redirect("../DetailsView.aspx?MenuId=" + MenuID + "");
        }
        private void pUnLockControls()
        {
            dtpFromDate.ReadOnly = false;
            txtimprate.ReadOnly = false;
            txtexprate.ReadOnly = false;
            if (ViewState[STATUS_KEY].ToString() == "Modify")
                LOVCurrency1.ReadOnly = true;
            else
                LOVCurrency1.ReadOnly = false;
        }

        private void pLockControls()
        {
            dtpFromDate.ReadOnly = true;
            txtexprate.ReadOnly = true;
            txtimprate.ReadOnly = true;
            LOVCurrency1.ReadOnly = true;
        }
        private void Page_EditButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Modify";
            pUnLockControls();
        }
        private void Page_DeleteButon(object sender, EventArgs e)
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
                    pBacktoGrid();
                    return;
                }
                else
                {
                    bcExchRate.Status = "Deletion not possible...!";
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
        private void pDelete()
        {
            try
            {
                myExchRate = (Model.Masters.ExchangeRateInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.ExchangeRate.Delete(myExchRate);
            }
            catch
            {
                throw;
            }
        }
        private void pSave()
        {
            try
            {
                myExchRate = (Model.Masters.ExchangeRateInfo)ViewState[TRAN_ID_KEY];                

                pMapControls();

                SQLServerDAL.Masters.ExchangeRate.Insert(myExchRate);
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
                myExchRate = (Model.Masters.ExchangeRateInfo)ViewState[TRAN_ID_KEY];
                
                pMapControls();

                SQLServerDAL.Masters.ExchangeRate.Update(myExchRate);
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
                myExchRate = (Model.Masters.ExchangeRateInfo)ViewState[TRAN_ID_KEY];

                if (ViewState[STATUS_KEY].Equals("Modify") && myExchRate.SlNo == 0)
                {
                    bcExchRate.Status = "ExchRate not found....!";
                    return false;
                }
                if (myExchRate.ImportExchRate == 0 || myExchRate.ExportExchRate == 0)
                {
                    bcExchRate.Status = "Rates not entered....!";
                    return false;
                }
                if (SQLServerDAL.Masters.ExchangeRate.blnCheckExchangeRate(myExchRate))
                    return true;
                else
                {
                    bcExchRate.Status = "Check the Dates....!";
                    return false;
                }
            }
            else
            {
                bcExchRate.Status = "Check ur entries....!";
                return false;
            }
        }
        private DataTable AddDummyData(DataTable dtClone)
        {
            DataRow newRow = dtClone.NewRow();
            string Type = dtClone.Columns[0].DataType.ToString();
            if (Type == "System.String")
                newRow[0] = "No Records Found!";

            dtClone.Rows.Add(newRow);

            return dtClone;
        }        
        protected void dgExchRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            myCurrency = (Model.Masters.CurrencyInfo)ViewState[HEADER_KEY];

            pBindControls();
        }
    }
}