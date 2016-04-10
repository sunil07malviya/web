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
using ISPL.CSC.SQLServerDAL;
using ISPL.CSC.Model;

namespace ISPL.CSC.Web
{
    public partial class DetailsView : BasePage
    {
        private const string PAGE_KEY = "PAGE_KEY";
        private const string LOV_KEY = "LOV_KEY";

        private Model.Masters.LOVViewInfo myLOVViewInfo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LoginUserInfo.UserID.ToUpper().Equals("ADMIN"))
                {
                    if (MenuID != 50048 && MenuID != 50005 && MenuID != 50359 && MenuID != 50361 && MenuID != 50181 && MenuID !=50369)
                        btnAdd.Visible = true;
                    else
                        btnAdd.Visible = false;
                }
                else
                {
                    Model.Masters.UserMenuAccessInfo myAccess = SQLServerDAL.Masters.UserMenuAccess.GetButtonStatus(LoginUserInfo.BranchID, MenuID, LoginUserInfo.UserID);

                    btnAdd.Visible = (myAccess.AddFlag.Equals("Y") ? true : false);
                }

                dtpFromDate.CalendarDate = "01-" + DateTime.Today.ToString("MMM-yyyy");
                dtpToDate.CalendarDate = Convert.ToDateTime("01-" + (DateTime.Today.AddMonths(1)).ToString("MMM-yyyy")).AddDays(-1).ToString("dd-MMM-yyyy");

                pBindGrid(null, null);
            }
        }
        private void pBindGrid(string sortExpression, string direction)
        {
            string[] lstrWidths;

            myLOVViewInfo = SQLServerDAL.Masters.LOVView.GetLOVViewInfo(Convert.ToInt32(MenuID));
            ViewState[LOV_KEY] = myLOVViewInfo;
            lstrWidths = myLOVViewInfo.Widths.Split(',');

            lblHeading.InnerText = myLOVViewInfo.Caption;
            pAddToFavourites();
            string date = myLOVViewInfo.DateFieldName;

            if (myLOVViewInfo.Flag == "TR")
            {
                lblPeriod.Visible = false ;
                lblPeriodTo.Visible = false ;
                dtpFromDate.Visible = false;
                dtpToDate.Visible = false ;

                //SQLServerDAL.Masters.UserFinancialYear userFinancialYear = new ISPL.CSC.SQLServerDAL.Masters.UserFinancialYear();
                //Model.Masters.UserFinancialYear myUserFinancialYear = userFinancialYear.GetFinancialYearbyUserId(LoginUserInfo.BranchID, LoginUserInfo.UserID);
                //if (myUserFinancialYear != null)
                //{
                //    dtpFromDate.MinDate = dtpToDate.MinDate = myUserFinancialYear.FromDate.ToString("dd-MMM-yyyy");
                //    dtpFromDate.MaxDate = dtpToDate.MaxDate = myUserFinancialYear.ToDate.ToString("dd-MMM-yyyy");
                //}
            }
            else
            {
                lblPeriod.Visible = false;
                lblPeriodTo.Visible = false;
                dtpFromDate.Visible = false;
                dtpToDate.Visible = false;
                dtpFromDate.ClearDate();
                dtpToDate.ClearDate();
            }
            ViewState[PAGE_KEY] = myLOVViewInfo.Page;
            //btnAdd.Attributes.Add("onclick", "fnHideMenu(true); fnOpenPage('" + ViewState[PAGE_KEY].ToString() + "0&MenuID=" + MenuID + "');");
            btnAdd.Attributes.Add("onclick", "fnOpenPage('" + ViewState[PAGE_KEY].ToString() + "0&MenuID=" + MenuID + "');");

            DataTable dt = SQLServerDAL.Masters.LOVView.GetResultByMenuID(Convert.ToInt32(MenuID), LoginUserInfo.UserID, LoginUserInfo.BranchID);

            dt.DefaultView.RowFilter = "";
            lblCaption.Text = "Total Record(s): " + dt.Rows.Count.ToString();
            string lstrRowFilter = string.Empty;

            if (txtSearch.Text.Length != 0) txtSearch.Text = txtSearch.Text.Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", "").Replace("'", "").Trim();
            if (txtSearch.Text.Length != 0)
            {
                if (ViewState["SortExpression"] == null)
                    ViewState["SortExpression"] = string.Empty;

                //string lstrRowFilter = string.Empty;
                if (string.IsNullOrEmpty(ViewState["SortExpression"].ToString()))
                {
                    foreach (DataColumn objDC in dt.Columns)
                    {
                        switch (objDC.DataType.ToString())
                        {
                            case "System.String":
                                if (lstrRowFilter.Length == 0)
                                    lstrRowFilter = "([" + objDC.ColumnName + "] LIKE '*" + txtSearch.Text.Trim() + "*'";
                                else
                                    lstrRowFilter += " OR [" + objDC.ColumnName + "] LIKE '*" + txtSearch.Text.Trim() + "*'";
                                break;
                            case "System.Int32":
                            case "System.Double":
                                double ltmpNumber = 0;
                                try
                                {
                                    ltmpNumber = Convert.ToDouble(txtSearch.Text);
                                }
                                catch
                                {
                                    ltmpNumber = 0;
                                }
                                if (ltmpNumber > 0)
                                {
                                    if (lstrRowFilter.Length == 0)
                                        lstrRowFilter = "([" + objDC.ColumnName + "] = " + txtSearch.Text.Trim() + "";
                                    else
                                        lstrRowFilter += " OR [" + objDC.ColumnName + "] = " + txtSearch.Text.Trim() + "";
                                }
                                break;
                        }
                    }
                }
                else
                {
                    int colIndex = -1;
                    foreach (DataControlField tmpDC in gvDetails.Columns)
                    {
                        if (tmpDC.SortExpression == ViewState["SortExpression"].ToString())
                        {
                            colIndex = gvDetails.Columns.IndexOf(tmpDC);
                            break;
                        }
                    }

                    DataColumn objDC = dt.Columns[colIndex];
                    switch (objDC.DataType.ToString())
                    {
                        case "System.String":
                            lstrRowFilter = "([" + objDC.ColumnName + "] LIKE '*" + txtSearch.Text.Trim() + "*'";
                            break;
                        case "System.Int32":
                        case "System.Double":
                            lstrRowFilter = "([" + objDC.ColumnName + "] = " + txtSearch.Text.Trim() + "";
                            break;
                    }
                }
                lstrRowFilter += lstrRowFilter.Length > 0 ? ")" : "";

                dt.DefaultView.RowFilter = lstrRowFilter;
            }
            //if (myLOVViewInfo.Flag == "TR" && txtSearch.Text.Length == 0)
            //{
            //    if (dtpFromDate.CalendarDate != DateTime.MinValue.ToString("dd-MMM-yyyy") && dtpToDate.CalendarDate != DateTime.MinValue.ToString("dd-MMM-yyyy") && date != null)
            //    {
            //        if (lstrRowFilter.Length == 0)
            //            lstrRowFilter = "([" + date + "] >= '" + dtpFromDate.CalendarDate + "' and  " + "[" + date + "] <= '" + dtpToDate.CalendarDate + "')";
            //        else
            //            lstrRowFilter += " and ([" + date + "] >= '" + dtpFromDate.CalendarDate + "' and  " + "[" + date + "] <= '" + dtpToDate.CalendarDate + "')";

            //        dt.DefaultView.RowFilter = lstrRowFilter;
            //    }
            //}

            if (dt.DefaultView.RowFilter.Length != 0)
                lblCaption.Text += "&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;Filtered: " + dt.DefaultView.Count.ToString();

            if (myLOVViewInfo.CheckFieldName.Length > 0 && dt.Columns[dt.Columns.Count - 2].ColumnName == "CNT")
                gvDetails.DataKeyNames = new string[] { dt.Columns[dt.Columns.Count - 1].ColumnName, "CNT" };
            else
                gvDetails.DataKeyNames = new string[] { dt.Columns[dt.Columns.Count - 1].ColumnName };

            gvDetails.Columns.Clear();
            for (int i = 0; i < dt.Columns.Count - 1; i++)
            {
                DataColumn objDC = dt.Columns[i];

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

                if (lstrWidths[i].ToString() == "0" || lstrWidths[i] == null)
                {
                    objBC.Visible = false;
                }

                gvDetails.Columns.Add(objBC);
            }

            gvDetails.SelectedIndex = -1;

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = dt.Columns[dt.Columns.Count - 1].ColumnName;
                direction = " DESC";
            }

            if (!string.IsNullOrEmpty(sortExpression) && direction != null)
                dt.DefaultView.Sort = sortExpression + direction;

            gvDetails.AllowPaging = true;
            gvDetails.AllowSorting = true;

            //gvDetails.PageIndex = Convert.ToInt32(ViewState[PAGE_KEY].ToString());

            if (dt.DefaultView.Count == 0 && myLOVViewInfo.Flag == "TR")
                gvDetails.DataSource = AddDummyData(dt.Clone());

            else if (dt.DefaultView.Count == 0 && myLOVViewInfo.Flag == "RE")
                gvDetails.DataSource = dt.DefaultView;

            else
                gvDetails.DataSource = dt.DefaultView;

            try
            {
                gvDetails.DataBind();
            }
            catch
            {
                gvDetails.PageIndex = 0;
                gvDetails.DataBind();
            }

            lblCaption.Text += "&nbsp;&nbsp;-&nbsp;&nbsp;Page : " + (gvDetails.PageIndex + 1).ToString() + " of " + (gvDetails.PageCount == 0 ? 1 : gvDetails.PageCount).ToString();

            if (myLOVViewInfo.CheckFieldName.Length > 0 && gvDetails.DataKeyNames.Length == 2)
            {
                int CheckCounter = 0;
                foreach (GridViewRow gvRow in gvDetails.Rows)
                {
                    if (gvDetails.DataKeys[gvRow.RowIndex].Values[1].ToString() != "")
                        CheckCounter = Convert.ToInt32(gvDetails.DataKeys[gvRow.RowIndex].Values[1].ToString());
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

        protected void imgAddFavorites_Click(object sender, ImageClickEventArgs e)
        {
            myLOVViewInfo = (Model.Masters.LOVViewInfo)ViewState[LOV_KEY];

            ISPL.CSC.Model.FavouritesInfo myFavouritesInfo = new ISPL.CSC.Model.FavouritesInfo();

            myFavouritesInfo.UserID = LoginUserInfo.UserID;
            myFavouritesInfo.BranchID = LoginUserInfo.BranchID;
            myFavouritesInfo.MenuSlNo = myLOVViewInfo.MenuInfo.SlNo;
            myFavouritesInfo.Name = myLOVViewInfo.Caption;
            myFavouritesInfo.Value = myLOVViewInfo.MenuInfo.PageName + "?MenuId=" + myLOVViewInfo.MenuInfo.SlNo;
            //myFavouritesInfo.Value = Request.RawUrl.Substring(1, Request.RawUrl.Length - 1);

            if (SQLServerDAL.Favourites.blnCheckFavouritesInfo(myFavouritesInfo))
            {
                SQLServerDAL.Favourites.Insert(myFavouritesInfo);
            }

            imgDelFavorites.Visible = true;
            imgAddFavorites.Visible = false;
        }
        protected void imgDelFavorites_Click(object sender, ImageClickEventArgs e)
        {
            myLOVViewInfo = (Model.Masters.LOVViewInfo)ViewState[LOV_KEY];

            ISPL.CSC.Model.FavouritesInfo myFavouritesInfo = new ISPL.CSC.Model.FavouritesInfo();

            myFavouritesInfo.UserID = LoginUserInfo.UserID;
            myFavouritesInfo.BranchID = LoginUserInfo.BranchID;
            myFavouritesInfo.MenuSlNo = myLOVViewInfo.MenuInfo.SlNo;
            myFavouritesInfo.Name = myLOVViewInfo.Caption;
            myFavouritesInfo.Value = Request.RawUrl;

            SQLServerDAL.Favourites.Delete(myFavouritesInfo);

            imgDelFavorites.Visible = false;
            imgAddFavorites.Visible = true;

        }
        private void pAddToFavourites()
        {
            myLOVViewInfo = (Model.Masters.LOVViewInfo)ViewState[LOV_KEY];

            ISPL.CSC.Model.FavouritesInfo myFavouritesInfo = new ISPL.CSC.Model.FavouritesInfo();

            myFavouritesInfo.UserID = LoginUserInfo.UserID;
            myFavouritesInfo.BranchID = LoginUserInfo.BranchID;
            myFavouritesInfo.MenuSlNo = myLOVViewInfo.MenuInfo.SlNo;
            myFavouritesInfo.Name = myLOVViewInfo.Caption;
            myFavouritesInfo.Value = Request.RawUrl;
                        
            bool IsFavoritesAdded = false;
            if (SQLServerDAL.Favourites.blnCheckFavouritesInfo(myFavouritesInfo))
                IsFavoritesAdded = true;

            if (!IsFavoritesAdded)
            {
                imgAddFavorites.Visible = false;
                imgDelFavorites.Visible = true;
            }
            else
            {
                imgAddFavorites.Visible = true;
                imgDelFavorites.Visible = false;
            }

            imgAddFavorites.Attributes.Add("onclick", "fnAddFavouritesList('" + myFavouritesInfo.Name + "','" + myFavouritesInfo.Value + "')");
            imgDelFavorites.Attributes.Add("onclick", "fnDelFavouritesList('" + myFavouritesInfo.Name + "')");  //'" + myFavouritesInfo.Value + "',
        }
        private DataTable AddDummyData(DataTable dt)
        {
            DataRow newRow = dt.NewRow();

            newRow[0] = "No Records Found!";

            dt.Rows.Add(newRow);

            return dt;
        }
        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        { 
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gvDetails.DataKeys[e.Row.RowIndex].Values[0].ToString().Length > 0)
                    e.Row.Attributes.Add("onclick", "fnHideMenu(true); fnOpenPage( '" + ViewState[PAGE_KEY].ToString() + gvDetails.DataKeys[e.Row.RowIndex].Values[0] + "&MenuID=" + MenuID + "');");
            }

            if (ViewState["SortExpression"] != null)
            {
                int cellIndex = -1;
                foreach (DataControlField field in gvDetails.Columns)
                {
                    if (field.SortExpression == ViewState["SortExpression"].ToString())
                    {
                        cellIndex = gvDetails.Columns.IndexOf(field);
                        break;
                    }
                }

                if (cellIndex > -1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        e.Row.Cells[cellIndex].CssClass +=
                            (GridViewSortDirection == SortDirection.Ascending
                            ? " sortasc" : " sortdesc");
                    }
                }
            }
        }

        protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetails.PageIndex = e.NewPageIndex;

            if (ViewState["SortExpression"] == null)
                ViewState["SortExpression"] = string.Empty;

            pBindGrid(ViewState["SortExpression"].ToString(), GridViewSortDirection == SortDirection.Ascending ? " ASC" : " DESC");
        }

        protected void gvDetails_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["SortExpression"] == null)
                ViewState["SortExpression"] = string.Empty;

            pBindGrid(ViewState["SortExpression"].ToString(), GridViewSortDirection == SortDirection.Ascending ? " ASC" : " DESC");
        }
        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            dtpFromDate.ClearDate();
            dtpToDate.ClearDate();
            txtSearch.Text = "";
            ViewState["SortExpression"] = null;
            if (ViewState["SortExpression"] == null)
                ViewState["SortExpression"] = string.Empty;

            pBindGrid(ViewState["SortExpression"].ToString(), GridViewSortDirection == SortDirection.Ascending ? " ASC" : " DESC");
        }
        protected void btnExcel_Click(object sender, ImageClickEventArgs e)
        {
            myLOVViewInfo = (Model.Masters.LOVViewInfo)ViewState[LOV_KEY];
        }
    }
}
