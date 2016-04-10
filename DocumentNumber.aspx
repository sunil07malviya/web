<%@ Page Language="C#" AutoEventWireup="true" Theme="BlueTheme" CodeFile="DocumentNumber.aspx.cs"
    Inherits="ISPL.CSC.Web.Masters.DocumentNumber" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagName="LOVControl" Src="~/Controls/LOVControl.ascx" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/ButtonControl.ascx" TagName="ButtonControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="Controls" Namespace="ISPL.CSC.Web.Controls"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AutoNo Page</title>
    <link href="../_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../_assets/css/round.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 31%;
        }
        .style2
        {
            font-family: Tahoma;
            color: #003300;
            font-size: 8pt;
            width: 31%;
        }
    </style>
</head>


<body>
    <form id="frmDocNumber" runat="server">
    <asp:ScriptManager ScriptMode="Release" ID="smDocNumber" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updDocNumber" runat="server" UpdateMode="Always">
        <ContentTemplate>
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
                            <table cellpadding="2" border="0" cellspacing="2" style="font-size: 8.25pt;" width="100%">
                                <tr>
                                    <td class="style1">
                                        Transaction
                                    </td>
                                    <td colspan="3">
                                        <uc1:LOVControl ID="LOVTrans" runat="server"></uc1:LOVControl>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        Format
                                    </td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="ddlFormat" runat="server" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        Field
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlField" runat="server" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                    <td colspan="2">
                                        <asp:CheckBox ID="chkEF" runat="server" Text="Enable Auto Number"></asp:CheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        Prefix
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPrefix" runat="server" MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <asp:CheckBox ID="chkMultiple" runat="server" Text="Multiple"></asp:CheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Counter
                                    </td>
                                    <td style="width: 30%">
                                        <Controls:NumericTextBox ID="txtCounter" runat="server" AllowDecimal="false" BorderWidth="1px" MaxLength="8" Width="56px" Decimals="4"></Controls:NumericTextBox>
                                    </td>
                                    <td class="text" style="width: 20%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 30%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        Suffix
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSuffix" runat="server" MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <%--<tr>
                    <td colspan ="4"> 
                    	<asp:ValidationSummary id="VS" runat="server"></asp:ValidationSummary>
                    </td>
                    </tr>		--%>
                                <tr>
                                    <%--<td class="style1">
                                        Max Length
                                    </td>
                                    <td>
                                        <Controls:NumericTextBox ID="txtLength" runat="server" AllowDecimal="false" BorderWidth="1px" MaxLength="8" Width="56px" Decimals="4"></Controls:NumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>--%>
                                </tr>
                                <tr>
                                    <%--<td class="style1">
                                        Prefix Character (counter)
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChar" runat="server" MaxLength="1" Width="100px">0</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <uc1:ButtonControl ID="bcDocNumber" runat="server"></uc1:ButtonControl>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="prgDocNumber" runat="server">
        <ProgressTemplate>
            <div class="progressBackgroundFilter">
            </div>
            <div class="ProgressMessage">
                <img alt="Loading..." src="_assets/img/loader.gif" />
                <br />
                <span class="loadertext">Please wait while the page is refreshing...</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>
</html>
