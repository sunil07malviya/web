<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ISPL.CSC.Web.ChangePassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ButtonControl.ascx" TagName="ButtonControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ChangePassword</title>
    <link href="_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="_assets/css/round.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmChangePassword" runat="server">
    <asp:ScriptManager ID="smChangePassword" runat="server">
    </asp:ScriptManager>
        <div align="center" style="left: 269px; position: absolute; top: 150px;">
    <div class="rounded" style="width: 400px; ">
        <div class="top-outer">
            <div class="top-inner">
                <div class="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <h2>
                                    Change Password
                                </h2>
                            </td>
                            <td align="right">
                                <span style="color: white; font-weight: bold; font-size: 7.5pt;">* Indicates Mandatory
                                    Entry</span>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="mid-outer">
            <div class="mid-inner">
                <div class="mid">
                    <table cellpadding="2" cellspacing="2" style="font-size: 8.25pt;">
                        <tr>
                            <td>
                                Existing Password<span class="mandatory">&nbsp;*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOldPassword" MaxLength="50" TextMode="Password" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                New Password<span class="mandatory">&nbsp;*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNewPassword" MaxLength="50" TextMode="Password" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Confirm Password<span class="mandatory">&nbsp;*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtConfirmPassword" MaxLength="50" TextMode="Password" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:ButtonControl ID="bcChangePassword" OnSubmitButtonClick="bcChangePassword_SubmitButtonClick"
                                    OnCancelButtonClick="bcChangePassword_CancelButtonClick" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:UpdateProgress ID="prgChangePassword" runat="server">
                                    <ProgressTemplate>
                                        <img alt="Loading..." src="../_assets/img/loader.gif" />
                                        <span class="loadertext">Loading.....</span>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lblMessage" CssClass="mandatory"  Text="Password Expired, Create New Password" runat="server"></asp:Label>
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
        </div>
    </form>
</body>
</html>
