<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExchangeRate.aspx.cs"  Theme="BlueTheme"
    Inherits="ISPL.CSC.Web.Masters.ExchangeRate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ButtonControl.ascx" TagName="ButtonControl" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/LOVControl.ascx" TagName="LOVControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="Controls" Namespace="ISPL.CSC.Web.Controls"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../_assets/css/grid.css" rel="stylesheet" type="text/css" />
    <link href="../_assets/css/round.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript"></script>
</head>
<body>
    <form id="frmDepreciationRates" runat="server">
    <asp:ScriptManager ID="smDepreciationRates" runat="server">
    </asp:ScriptManager>
    <div class="rounded" style="width: 99%;">
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
                            <td>
                               <%-- <span style="color: white; font-weight: bold; font-size: 7.5pt;">* Indicates Mandatory
                                    Entry</span>--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="mid-outer">
            <div class="mid-inner">
                <div class="mid">
                  
                            <table cellpadding="2" cellspacing="2" style="font-size: 8.25pt; width: 70%;">
                                <tr>
                                    <td>Currency :<span class="mandatory">&nbsp;*</span></td>
                                     <td>
                                        <uc1:LOVControl ID="LOVCurrency1" runat="server" Title="Currency" Columns="15" TabIndex="1" /> 
                                     </td>
                                      <td>Date :<span class="mandatory">&nbsp;*</span></td>
                                       <td>
                                         <uc1:DatePicker ID="dtpFromDate" tabIndex="2" runat="server" />
                                       </td>
                                </tr>
                                 <tr>
                                    <td>Import Rate :<span class="mandatory">&nbsp;*</span></td>
                                     <td> 
                                     <Controls:NumericTextBox id="txtimprate" runat="server" tabIndex="3"></Controls:NumericTextBox>
                                     </td>
                                      <td>Export Rate :<span class="mandatory">&nbsp;*</span></td>
                                       <td>
                                       <Controls:NumericTextBox id="txtexprate" runat="server" tabIndex="4"></Controls:NumericTextBox>
                                     </td>
                                </tr>
                                <tr>
                                    <td class="grid" colspan="4">
                                         <asp:GridView CssClass="datatable" ID="dgExchRate" runat="server" 
                                             AutoGenerateColumns="false" AllowPaging="true"
                                            AllowSorting="true" PageSize="10" CellPadding="0" CellSpacing="0"
                                            BorderWidth="0" GridLines="None" SelectedRowStyle-CssClass="selected"
                                            onpageindexchanging="dgExchRate_PageIndexChanging" Width="97%"  
                                            onselectedindexchanged="dgExchRate_SelectedIndexChanged">
                                            <PagerStyle CssClass="pager" />
                                            <RowStyle CssClass="row" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <uc1:ButtonControl ID="bcExchRate" runat="server"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4">
                                        <asp:UpdateProgress ID="prgExchRates" runat="server">
                                            <ProgressTemplate>
                                                <img alt="Loading..." src="../_assets/img/loader.gif" />
                                                <span class="loadertext">Loading.....</span></ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4">
                                        <asp:Label ID="lblMessage" CssClass="mandatory" runat="server"></asp:Label>
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
    </form>
</body>
</html>
