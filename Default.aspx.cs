using System;
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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnPO_Click(object sender, EventArgs e)
    {
        Button b = (Button)sender;

        switch (b.CommandArgument)
        {
            case "PO":
                Response.Redirect("~/Transactions/PurchaseOrder/SupplierPOHeader.aspx", true);
                break;
            case "Invoice":
                Response.Redirect("~/Transactions/SupplierInvoice/SupplierInvHeader.aspx", true);
                break;
            case "Shipment":
                Response.Redirect("~/Transactions/ItemMaster.aspx", true);
                break;
        }
    }
}
