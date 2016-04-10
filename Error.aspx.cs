using System;
using System.Data;


namespace ISPL.CSC.Web
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblURL.Text = Request["URL"].ToString();
            lblIP.Text = Request.UserHostAddress + ":" + (Request["ErrorID"] != null ? Request["ErrorID"].ToString() : string.Empty);
            lblException.Text = Cache[Request["UNQ"].ToString()].ToString();
            Cache.Remove(Request["UNQ"].ToString());

            //DataTable dt = SQLServerDAL.General.GetDataTable("SELECT * FROM I_FASOFTERRORS WHERE I_FASOFTERROR_SLNO = " + Request["ErrorID"].ToString());

            //if (dt.Rows.Count > 0)
            //{
            //    eSupportWebReference.RaiseTicket raiseTicket = new eSupportWebReference.RaiseTicket();
            //    raiseTicket.RaiseTicketForEOSOFTSTP(dt.Rows[0]["I_FASoftError_PageName"].ToString(), dt.Rows[0]["I_FASoftError_Exception"].ToString());
            //}
        }
    }
}