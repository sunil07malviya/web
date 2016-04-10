<%@ Page Language="C#" AutoEventWireup="true" Theme="BlueTheme" CodeFile="Authority.aspx.cs" Inherits="ISPL.CSC.Web.Masters.Authority" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ButtonControl.ascx" TagName="ButtonControl" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/LOVControl.ascx" TagName="LOVControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="Controls" Namespace="ISPL.CSC.Web.Controls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Authority</title>
    <link href="../_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../_assets/css/round.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmAuthority" runat="server">
    <asp:ScriptManager ID="smAuthority" runat="server">
    </asp:ScriptManager>
    <div class="rounded" style="width: 500px;">
        <div class="top-outer">
            <div class="top-inner">
                <div class="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <h2>
                                    <span id="lblHeading" runat="server"></span>
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
                    <asp:UpdatePanel ID="updAuthority" runat="server">
                        <ContentTemplate>
                            <table cellpadding="2" cellspacing="2" style="font-size: 8.25pt;">
                                <tr>
                                    <td style="width: 25%;">
                                        Code<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtShortName" MaxLength="20" Columns="20" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvShortName" runat="server" ControlToValidate="txtShortName"
                                            ErrorMessage="Short Name is required!" Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">
                                        Name<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtName" MaxLength="255" Columns="50" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                            ErrorMessage="Name is required!" Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">
                                        Address
                                    </td>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtAddress1" MaxLength="50" Columns="50" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtAddress2" MaxLength="50" Columns="50" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtAddress3" MaxLength="50" Columns="50" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtAddress4" MaxLength="50" Columns="50" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <uc1:ButtonControl ID="btnAuthority" runat="server" OnEditButtonClick="Page_EditButton"
                                            OnDeleteButtonClick="Page_DeleteButton" OnSubmitButtonClick="Page_SubmitButton"
                                            OnCancelButtonClick="Page_CancelButton" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:UpdateProgress ID="prgAuthority" runat="server">
                                            <ProgressTemplate>
                                                <img alt="Loading..." src="../_assets/img/loader.gif" />
                                                <span class="loadertext">Loading.....</span>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Label ID="lblMessage" CssClass="mandatory" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
    </form>
</body>
</html>
