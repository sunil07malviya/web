using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using ISPL.CSC.Model;
using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class ShipmentStages : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";
        private const string COLUMN_COUNT_KEY = "COLUMN_COUNT_KEY";
        private const string PAGE_INDEX_KEY = "PAGE_INDEX_KEY";
        private const string SORT_KEY = "SORTY_KEY";
        private const string DETAIL_KEY = "DETAIL_KEY";

        private ShipmentStagesInfo myShipmentStagesInfo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnShipmentStatus.EditButtonClick += new EventHandler(btnShipmentStatus_EditButtonClick);
            this.btnShipmentStatus.CancelButtonClick += new EventHandler(btnShipmentStatus_CancelButtonClick);
            this.btnShipmentStatus.SubmitButtonClick += new EventHandler(btnShipmentStatus_SubmitButtonClick);

            if (!IsPostBack)
            {
                ViewState[PAGE_INDEX_KEY] = 0;
                ViewState[SORT_KEY] = "ASC";
               
            }
            pBindGrid(null, null);
            if (gvshipmentstatus.Rows.Count > 0 && !IsPostBack)
            {
                gvshipmentstatus.SelectedIndex = 0;
                gvshipmentstatus_SelectedIndexChanged(sender, e);
            }
        }
         private void PUnlockControls()
        {
            TxtRemarks.ReadOnly = false;
        }
        private void PLockControls()
        {
            TxtRemarks.ReadOnly = true;
        }
        private void pClearControls()
        {
            TxtRemarks.Text = "";
        }
        private void pBindGrid(string sortExpression, string direction)
        {
            DataTable myTable = null;
            string strQuery = "SELECT Date_FirstStage as [Available], Date_SecondSatge as [Not Available],Reamarks as [Status] ,SlNo FROM ShipmentStages ORDER BY slno";
            myTable = SQLServerDAL.General.GetDataTable(strQuery);
            ViewState[COLUMN_COUNT_KEY] = myTable.Columns.Count;

            gvshipmentstatus.DataKeyNames = new string[] { myTable.Columns[myTable.Columns.Count - 1].ColumnName };
            gvshipmentstatus.Columns.Clear();
            for (int i = 0; i < myTable.Columns.Count; i++)
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
                gvshipmentstatus.Columns.Add(objBC);
            }           

            gvshipmentstatus.SelectedIndex = -1;

            if (string.IsNullOrEmpty(sortExpression) && direction == null)
            {
                sortExpression = myTable.Columns[myTable.Columns.Count - 1].ColumnName;
                direction = " ASC";
            }
            if (!string.IsNullOrEmpty(sortExpression) && direction != null)
                myTable.DefaultView.Sort = sortExpression + direction;

            gvshipmentstatus.AllowPaging = true;
            gvshipmentstatus.AllowSorting = true;

            if (myTable.Rows.Count == 0)
            {
                gvshipmentstatus.AllowPaging = false;
                gvshipmentstatus.AllowSorting = false;
            }
            gvshipmentstatus.PageIndex = Convert.ToInt32(ViewState[PAGE_INDEX_KEY].ToString());
            
            gvshipmentstatus.DataSource = myTable;
            //ViewState[DETAIL_KEY] = myTable;
            try
            {
                gvshipmentstatus.DataBind();
                gvshipmentstatus.Columns[gvshipmentstatus.Columns.Count - 1].Visible = false;
            }
            catch
            {
                gvshipmentstatus.PageIndex = 0;
                gvshipmentstatus.DataBind();
            }
        }
        protected void gvshipmentstatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvDetails, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["class"] = e.Row.RowIndex % 2 == 0 ? "datatablerow" : "datatablerow1";
                e.Row.Attributes["onclick"] = "fnSelect('" + gvshipmentstatus.ID + "', " + e.Row.RowIndex + ");";
                e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
            }
            if (ViewState["SortExpression"] != null)
            {
                int cellIndex = -1;
                foreach (DataControlField field in gvshipmentstatus.Columns)
                {
                    if (field.SortExpression == ViewState["SortExpression"].ToString())
                    {
                        cellIndex = gvshipmentstatus.Columns.IndexOf(field);
                        break;
                    }
                }
                if (cellIndex > -1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        e.Row.Cells[cellIndex].CssClass +=
                            (GridViewSortDirection == SortDirection.Ascending ? " sortasc" : " sortdesc");
                    }
                }
            }
        }
        protected void gvshipmentstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvshipmentstatus.SelectedIndex != -1 && gvshipmentstatus.SelectedDataKey.Value.ToString() != "")
            {
                ViewState[STATUS_KEY] = "View";
                btnShipmentStatus.MenuID = MenuID;
                btnShipmentStatus.ButtonClicked = "View";
                ViewState[TRAN_ID_KEY] = gvshipmentstatus.SelectedDataKey.Value.ToString();
                PLockControls();
                pBindDetail(Convert.ToInt32(gvshipmentstatus.SelectedDataKey.Value));
            }
            else
            {
                ViewState.Remove(STATUS_KEY);
                pClearControls();
            }
        }
        //protected void gvshipmentstatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvshipmentstatus.PageIndex = e.NewPageIndex;

        //    pBindGrid(null, null);
        //}
        //protected void gvshipmentstatus_PreRender(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (gvshipmentstatus.PageCount == 1)
        //            gvshipmentstatus.PagerSettings.Visible = true;
        //        else
        //            gvshipmentstatus.PagerSettings.Visible = true;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        //protected void gvshipmentstatus_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    try
        //    {
        //        string sortExpression = e.SortExpression;
        //        ViewState["SortExpression"] = sortExpression;

        //        if (GridViewSortDirection == SortDirection.Ascending)
        //        {
        //            GridViewSortDirection = SortDirection.Descending;
        //            pBindGrid(sortExpression, " DESC");
        //        }
        //        else
        //        {
        //            GridViewSortDirection = SortDirection.Ascending;
        //            pBindGrid(sortExpression, " ASC");
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }
        private void pBindDetail(int slno)
        {
            //string lstrQuery = "UPDATE ShipmentStages SET Reamarks='" + TxtRemarks.Text + "' where SlNo= " + slno + " ";
            //SQLServerDAL.General Gen = new ISPL.CSC.SQLServerDAL.General();
            //Gen.ExecuteNonQuery(lstrQuery);
           
            myShipmentStagesInfo = SQLServerDAL.Masters.ShipmentStages.GetShipmentStagesInfo(slno);
            //ViewState[DETAIL_KEY] = myShipmentStagesInfo;
            TxtRemarks.Text = myShipmentStagesInfo.Remarks;
        }
        private void pMapControls()
        {
            int slno = Convert.ToInt32(ViewState[TRAN_ID_KEY].ToString());
            myShipmentStagesInfo = SQLServerDAL.Masters.ShipmentStages.GetShipmentStagesInfo(slno);
           // Model.Masters.ShipmentStagesInfo myShipmentStagesInfo = null;
            myShipmentStagesInfo.Remarks = TxtRemarks.Text;

           ViewState[DETAIL_KEY] = myShipmentStagesInfo;
        }
        protected void btnShipmentStatus_SubmitButtonClick(object sender, EventArgs e)
        {
            string lstrStatus = ViewState[STATUS_KEY].ToString();

            pMapControls();

            if (fblnValidEntry())
            {  
                if ((lstrStatus.Equals("Edit") || lstrStatus.Equals("Modify")))
                    pUpdate();
               
            }
            pBacktoGrid();
            
        }
        private bool fblnValidEntry()
        {
            return true;
        }
        private void pUpdate()
        {
            try
            {
                //int slno = Convert.ToInt32(ViewState[TRAN_ID_KEY].ToString());
                //SQLServerDAL.Masters.ShipmentStages ShipSt = new ISPL.CSC.SQLServerDAL.Masters.ShipmentStages();
                //myShipmentStagesInfo = ShipSt.GetShipmentStagesInfo(slno);
                myShipmentStagesInfo = (Model.Masters.ShipmentStagesInfo)ViewState[DETAIL_KEY];
                SQLServerDAL.Masters.ShipmentStages.Update(myShipmentStagesInfo);                 
            }
            catch
            {
                throw;
            }
        }
        protected void btnShipmentStatus_CancelButtonClick(object sender, EventArgs e)
        {
            pBacktoGrid();
        }
        protected void btnShipmentStatus_EditButtonClick(object sender, EventArgs e)
        {           
            ViewState[STATUS_KEY] = "Modify";
            PUnlockControls();
           // pBindGrid(null, null);
        }
        private void pBacktoGrid()
        {
            Response.Redirect("../Masters/ShipmentStages.aspx");
            //gvshipmentstatus.SelectedIndex = -1;
            //pBindGrid(null, null);
            ////ViewState[STATUS_KEY] = "View";
            //ViewState.Remove(STATUS_KEY);
            //PLockControls();
        }
    }
}
