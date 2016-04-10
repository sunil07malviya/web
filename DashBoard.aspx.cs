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

namespace ISPL.CSC.Web
{
    public partial class DashBoard : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Page Madified by Chandrashekar 
            //Added features slno,paging,sorting,Search
            
            //Prvious code start
            //DataTable myTable = new DataTable();
            //string Query = null;

            //Query = "SELECT Transaction_Id [Transaction Id],ADDRESS_NUMBER [Address No],Id,Number,[Message],MessageV1,MessageV2,MessageV3,MessageV4 from V_Exception_Handler ";
            //myTable = SQLServerDAL.General.GetDataTable(Query);
            //ViewState["Event_KEY"] = myTable;
            //gvDashBoard.DataSource = myTable;
            //gvDashBoard.DataBind();
            //end

            pBindGrid(null, null);
            
        }
        protected void gvDashBoard_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)(ViewState["Event_KEY"]);
            gvDashBoard.PageIndex = e.NewPageIndex;
            bindgridview(dt);
            divgvResults.Visible = true;
            
        }
        protected void bindgridview(DataTable dtGrid)
        {
            gvDashBoard.DataSource = dtGrid;
            gvDashBoard.DataBind();           
        
        }//end
        protected void dgDocuments_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                string sortExpression = e.SortExpression;
                ViewState["SortExpression"] = sortExpression;

                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                    //pBindGrid();
                    pBindGrid(sortExpression, " DESC");
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    pBindGrid(sortExpression, " ASC");
                    //pBindGrid();
                }
            }
            catch
            {
                throw;
            }
        }
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
        private void pBindGrid(string sortExpression, string direction)
        {
            DataTable myTable = new DataTable();
            string Query = null;

            Query = "SELECT Transaction_Id [Transaction Id],ADDRESS_NUMBER [Address No],Id,Number,[Message],MessageV1,MessageV2,MessageV3,MessageV4 from V_Exception_Handler ";
            myTable = SQLServerDAL.General.GetDataTable(Query);
            ViewState["Event_KEY"] = myTable;
           // myTable = SQLServerDAL.SQLHelper.GetDataTable(lstrQuery);
            lblCaption.Text = "Total Record(s): " + myTable.Rows.Count.ToString();

            myTable.DefaultView.RowFilter = "";

            //if (dtpFromDate.Visible == true && dtpToDate.Visible == true)
            //    if (dtpFromDate.CalendarDate != DateTime.MinValue.ToString("dd-MMM-yyyy") && dtpToDate.CalendarDate != DateTime.MinValue.ToString("dd-MMM-yyyy"))
            //        myTable.DefaultView.RowFilter = "[EventDate/Time]" + " >= '" + dtpFromDate.CalendarDate + "' and " + "[EventDate/Time]" + " <= '" + dtpToDate.CalendarDate + "'";

            if (txtSearch.Text.Length != 0)
            {
                string lstrRowFilter = string.Empty;
                if (ViewState["SortExpression"] == null)
                    ViewState["SortExpression"] = string.Empty;

                foreach (DataColumn objDC in myTable.Columns)
                {
                    if (objDC.DataType.ToString().Equals("System.String"))
                    {
                        if (lstrRowFilter.Length == 0)
                            lstrRowFilter = "([" + objDC.ColumnName + "] LIKE '*" + txtSearch.Text.Trim() + "*'";
                        else
                            lstrRowFilter += " OR [" + objDC.ColumnName + "] LIKE '*" + txtSearch.Text.Trim() + "*'";
                    }
                }
                lstrRowFilter += lstrRowFilter.Length > 0 ? ")" : "";

                myTable.DefaultView.RowFilter = lstrRowFilter;
            }

            if (myTable.DefaultView.RowFilter.Length != 0)
                lblCaption.Text += "&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;Filtered: " + myTable.DefaultView.Count.ToString();

            myTable.Columns.Add("SlNo").SetOrdinal(0);//SerailNo is 1st Column to display No of Records
            for (int i = 0; i < myTable.Rows.Count; i++)
            {
                myTable.Rows[i]["SlNo"] = (i + 1);

            }
            gvDashBoard.SelectedIndex = -1;
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = myTable.Columns[myTable.Columns.Count - 1].ColumnName;
                direction = " DESC";
            }
            if (!string.IsNullOrEmpty(sortExpression) && direction != null)
                myTable.DefaultView.Sort = sortExpression + direction;

            gvDashBoard.AllowPaging = true;
            gvDashBoard.AllowSorting = true;

            if (myTable.Rows.Count == 0)
            {
                gvDashBoard.AllowPaging = false;
                gvDashBoard.AllowSorting = false;

            }
            gvDashBoard.DataSource = myTable;
            try
            {
                gvDashBoard.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        
        }
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["SortExpression"] == null)
                ViewState["SortExpression"] = string.Empty;

            pBindGrid(ViewState["SortExpression"].ToString(), GridViewSortDirection == SortDirection.Ascending ? " ASC" : " DESC");
        }
        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            txtSearch.Text = "";
            ViewState["SortExpression"] = null;
            if (ViewState["SortExpression"] == null)
                ViewState["SortExpression"] = string.Empty;

            pBindGrid(ViewState["SortExpression"].ToString(), GridViewSortDirection == SortDirection.Ascending ? " ASC" : " DESC");
        }
        protected void gvDashBoard_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                string sortExpression = e.SortExpression;
                ViewState["SortExpression"] = sortExpression;

                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                    pBindGrid(sortExpression, " DESC");
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    pBindGrid(sortExpression, " ASC");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
