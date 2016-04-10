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
using ISPL.CSC.Web.ProcessFlow;

namespace ISPL.CSC.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                txtEnterpriseID.Focus();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string PrimaryServer = ConfigurationManager.AppSettings["ldapserverprimary"].ToString();
            string SecondaryServer = ConfigurationManager.AppSettings["ldapserversecondary"].ToString();
            string DomainName = ConfigurationManager.AppSettings["ldapdomainname"].ToString();
            string[] Tmp = DomainName.Split('.');

            string Path = String.Join(",dc=", Tmp);
            Path = "dc=" + Path;

            string adPath = PrimaryServer + "/" + Path;
            LdapAuthentication adAuth = new LdapAuthentication(adPath);

            if (adAuth.IsAuthenticated(DomainName, txtEnterpriseID.Text, txtPassword.Text))
            {
                Response.Redirect("SignIn1.aspx?ID=" + txtEnterpriseID.Text, true);
                return;
            }
            else
            {
                adPath = SecondaryServer + "/" + Path;
                adAuth = new LdapAuthentication(adPath);

                if (adAuth.IsAuthenticated(DomainName, txtEnterpriseID.Text, txtPassword.Text))
                {
                    Response.Redirect("SignIn1.aspx?ID=" + txtEnterpriseID.Text, true);
                    return;
                }

                else
                    lblMessage.Text = "Invalid user credentials!";
            }
        }
    }
}