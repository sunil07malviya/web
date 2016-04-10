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

namespace ISPL.CSC.Web
{
    public partial class CustomerServiceCentre : BasePage
    {
        private const string count = "count";

        protected void Page_Load(object sender, EventArgs e)
        {
            string strSecurity = ConfigurationSettings.AppSettings["Security"].ToString();

            try
            {
                string UrlReferrer = Context.Request.UrlReferrer.ToString();
                if (UrlReferrer == null && strSecurity != "LDAP") Response.Redirect("SignIn.aspx", true);
                if (UrlReferrer == null && strSecurity == "LDAP") Response.Redirect("Login.aspx", true);
            }
            catch
            {
                if (strSecurity != "LDAP") Response.Redirect("SignIn.aspx", true);
                if (strSecurity == "LDAP") Response.Redirect("Login.aspx", true);
            }
            session.Value = Session.Timeout.ToString();

            if (!IsPostBack)
            {
                pBindRootNodes();
            }
        }
        protected void pBindRootNodes()
        {
            string sql = string.Empty;

            if (LoginUserInfo.UserID == "Admin")
                sql = "SELECT * FROM G_MENU with (ROWLOCK) WHERE  ISNULL(G_MENU_PARENTCODE,0)=0 AND G_MENU_DISABLE='N' ORDER BY G_MENU_SORTBY asc ";
            else
                sql = "SELECT * FROM G_MENU with (ROWLOCK) WHERE ISNULL(G_MENU_PARENTCODE,0)=0 AND G_MENU_DISABLE='N' AND G_MENU_SLNO IN (SELECT GT_USERS_SLNO FROM GT_USERS with (ROWLOCK) WHERE GT_USERS_ID = '" + LoginUserInfo.UserID.ToString() + "')  ORDER BY G_MENU_SORTBY asc";

            DataTable dt = new DataTable();

            dt = SQLServerDAL.SQLHelper.ExecuteDataTable(SQLServerDAL.SQLHelper.CONN_STRING(), CommandType.Text, sql, null);

            foreach (DataRow dr in dt.Rows)
            {
                TreeNode tn = new TreeNode();
                tn.Text = dr["G_MENU_NAME"].ToString();
                tn.NavigateUrl = dr["G_MENU_PAGENAME"].ToString();

                tn.Value = dr["G_MENU_SLNO"].ToString();
                string lstrSQL = string.Empty;

                if (LoginUserInfo.UserID == "Admin")
                    lstrSQL = "SELECT count(*) cnt FROM G_MENU with (ROWLOCK) WHERE  G_MENU_PARENTCODE=" + tn.Value + " AND G_MENU_DISABLE='N' ";
                else
                    lstrSQL = "SELECT count(*) cnt FROM G_MENU with (ROWLOCK) WHERE  G_MENU_PARENTCODE=" + tn.Value + " AND G_MENU_DISABLE='N' AND G_MENU_SLNO IN (SELECT GT_USERS_SLNO FROM GT_USERS with (ROWLOCK) WHERE GT_USERS_ID = '" + LoginUserInfo.UserID.ToString() + "')";

                DataTable dt1 = SQLHelper.ExecuteDataTable(SQLHelper.CONN_STRING(), System.Data.CommandType.Text, lstrSQL, null);
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow objDr in dt1.Rows)
                    {
                        int count = Convert.ToInt32(objDr["cnt"].ToString());

                        if (count > 0)
                            tn.PopulateOnDemand = true;
                    }
                }
                tvwMenu.Nodes.Add(tn);
            }
        }

        protected void pBindChildNodes(TreeNode node)
        {
            string sql = string.Empty;

            if (LoginUserInfo.UserID == "Admin")
                sql = "SELECT * FROM G_MENU with (ROWLOCK) WHERE  G_MENU_PARENTCODE=" + node.Value + " AND G_MENU_DISABLE='N'  ORDER BY G_MENU_SORTBY asc ";
            else
                sql = "SELECT * FROM G_MENU with (ROWLOCK) WHERE  G_MENU_PARENTCODE=" + node.Value + " AND G_MENU_DISABLE='N' AND G_MENU_SLNO IN (SELECT GT_USERS_SLNO FROM GT_USERS with (ROWLOCK) WHERE GT_USERS_ID = '" + LoginUserInfo.UserID.ToString() + "')  ORDER BY G_MENU_SORTBY asc ";

            DataTable dt = new DataTable();
            dt = SQLServerDAL.SQLHelper.ExecuteDataTable(SQLServerDAL.SQLHelper.CONN_STRING(), CommandType.Text, sql, null);

            if (node.ChildNodes.Count == 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TreeNode cn = new TreeNode();
                    cn.Text = dr["G_MENU_NAME"].ToString();
                    cn.NavigateUrl = dr["G_MENU_PAGENAME"].ToString() + (dr["G_MENU_PAGENAME"].ToString() == string.Empty ? "" : "?MenuID=" + dr["G_MENU_SLNO"].ToString());
                    cn.Value = dr["G_MENU_SLNO"].ToString();
                    string lstrSQL = string.Empty;

                    if (LoginUserInfo.UserID == "Admin")
                        lstrSQL = "SELECT count(*) cnt FROM G_MENU with (ROWLOCK) WHERE  G_MENU_PARENTCODE=" + cn.Value + " AND G_MENU_DISABLE='N' ";
                    else
                        lstrSQL = "SELECT count(*) cnt FROM G_MENU with (ROWLOCK) WHERE  G_MENU_PARENTCODE=" + cn.Value + " AND G_MENU_DISABLE='N' AND G_MENU_SLNO IN (SELECT GT_USERS_SLNO FROM GT_USERS with (ROWLOCK) WHERE GT_USERS_ID = '" + LoginUserInfo.UserID.ToString() + "') ";

                    DataTable dt1 = SQLHelper.ExecuteDataTable(SQLHelper.CONN_STRING(), System.Data.CommandType.Text, lstrSQL, null);
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow objDr in dt1.Rows)
                        {
                            int count = Convert.ToInt32(objDr["cnt"].ToString());

                            if (count > 0)
                                cn.PopulateOnDemand = true;
                        }
                    }

                    node.ChildNodes.Add(cn);
                    node.Expand();
                }
            }
        }
        protected void tvwMenu_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            pBindChildNodes(e.Node);
        }

        protected void tvwMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            pBindChildNodes(tvwMenu.SelectedNode);
        }
    }
}
