<%@ Page Language="C#" AutoEventWireup="true" Theme="BlueTheme" CodeFile="Login.aspx.cs"
    Inherits="ISPL.CSC.Web.Login" %>
<%@ Register TagPrefix="uc1" TagName="NavBar" Src="~/Controls/NavBar.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CSC : Login Page</title>
    <link href="_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="_assets/css/round.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px;">
    <form id="frmLogin" runat="server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2">
                 <uc1:NavBar ID="nbEOSOFT" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 30%; padding-left: 10px;">
                <br />
                <br />
                <br />
                <br />
                <br />
                <div class="rounded" style="width: 300px;">
                    <div class="top-outer">
                        <div class="top-inner">
                            <div class="top">
                                <h2>
                                    &nbsp;&nbsp;Login
                                </h2>
                            </div>
                        </div>
                    </div>
                    <div class="mid-outer">
                        <div class="mid-inner">
                            <div class="mid">
                                <table cellpadding="2" cellspacing="2" style="font-size: 8.25pt;">
                                    <tr>
                                        <td>
                                            <span class="mandatory">* </span>Enterprise ID
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEnterpriseID" Columns="30" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEnterpriseID"
                                                runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="mandatory">* </span>Password
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPassword" TextMode="Password" Columns="30" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPassword"
                                                runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/_assets/img/buttons/btn_submit.gif"
                                                onmouseover="this.src='_assets/img/buttons/btn_submit_over.gif'" onmouseout="this.src='_assets/img/buttons/btn_submit.gif'"
                                                OnClick="btnSubmit_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Label ID="lblMessage" SkinID="ErrorText" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="bottom-outer">
                        <div class="bottom-inner">
                            <div class="bottom">
                            </div>
                        </div>
                    </div>
                </div>
            </td>
            <td style="width: 70%;">
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
