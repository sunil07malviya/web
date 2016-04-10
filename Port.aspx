<%@ Page Language="C#" AutoEventWireup="true" Theme="BlueTheme" CodeFile="Port.aspx.cs"
    Inherits="ISPL.CSC.Web.Masters.Port" %>

<%@ Register Src="~/Controls/ButtonControl.ascx" TagName="ButtonControl" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/LOVControl.ascx" TagName="LOVControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="Controls" Namespace="ISPL.CSC.Web.Controls"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Port Master</title>
    <link href="../_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../_assets/css/round.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmPort" runat="server">
    <asp:ScriptManager ID="smPort" runat="server">
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
                    <asp:UpdatePanel ID="updPort" runat="server">
                        <ContentTemplate>
                            <table cellpadding="2" cellspacing="2" style="font-size: 8.25pt;">
                                <tr>
                                    <td style="width: 25%;">
                                        Name<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtName" TabIndex="1" runat="server" Columns="40" MaxLength="40"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                            ErrorMessage="Port Name is required!">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">
                                        Port Code<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtShortName" TabIndex="1" runat="server" Columns="6" MaxLength="6"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvShortName" runat="server" ControlToValidate="txtShortName"
                                            ErrorMessage="Port Short Name is required!">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Port Type<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPortType" TabIndex="3" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Country<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td>
                                        <uc1:LOVControl id="LOVCountry" tabIndex="4" runat="server">
                                        </uc1:LOVControl>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        No. of Days
                                    </td>
                                    <td>
                                        <Controls:NumericTextBox ID="txtNoofDays" AllowDecimal="false" AllowNegative="false" runat="server" Columns="15">
                                        &nbsp;&nbsp;
                                        </Controls:NumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <uc1:ButtonControl ID="btnPort" runat="server" OnEditButtonClick="Page_EditButton"
                                            OnDeleteButtonClick="Page_DeleteButton" OnSubmitButtonClick="Page_SubmitButton"
                                            OnCancelButtonClick="Page_CancelButton" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:UpdateProgress ID="prgPort" runat="server">
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
