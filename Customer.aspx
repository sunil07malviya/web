<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Customer.aspx.cs" Inherits="ISPL.CSC.Web.Masters.Customer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ButtonControl.ascx" TagName="ButtonControl" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/LOVControl.ascx" TagName="LOVControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="Controls" Namespace="ISPL.CSC.Web.Controls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Customer</title>
    <link href="../_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../_assets/css/round.css" rel="Stylesheet" type="text/css" />
    <link href="../_assets/css/Tabs.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmCustomer" runat="server">
    <asp:ScriptManager AsyncPostBackTimeout="0" ScriptMode="Release" ID="smCustomer"
        runat="server">
    </asp:ScriptManager>
    <div class="rounded" style="width: 700px;">
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
                    <asp:UpdatePanel ID="upCustomer" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <table cellpadding="2" cellspacing="2" border="0">
                                <tr>
                                    <td>
                                        <table style="width: 659px">
                                            <tr>
                                                <td style="color: #0066cc;">
                                                    <b>Customer Name</b><span class="mandatory">&nbsp;*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtName" MaxLength="60" Columns="30" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="rfvName" runat="server" Text="*"
                                                        ControlToValidate="txtName"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                </td>
                                                <td style="color: #0066cc;">
                                                    <b>Short Name</b><span class="mandatory">&nbsp;*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtShortName" MaxLength="50" Columns="30" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="rfvShortName" runat="server" Text="*"
                                                        ControlToValidate="txtShortName"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <cc1:TabContainer ID="TabCustomer" runat="server" BackColor="AliceBlue" ActiveTabIndex="0">
                                            <cc1:TabPanel ID="a" runat="server" BackColor="AliceBlue">
                                                <HeaderTemplate>
                                                    Address Details</HeaderTemplate>
                                                <ContentTemplate>
                                                    <div style="width: 100%; font-size: 8.25pt; background-color: aliceblue;">
                                                        <table id="AddressDetails" border="0" cellpadding="2" cellspacing="2" style="background-color: aliceblue;">
                                                            <tr>
                                                                <td>
                                                                    Address <span class="mandatory">&nbsp;*</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtAdd" runat="server" Columns="40" MaxLength="50"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvAdd" runat="server" Text="*" ControlToValidate="txtAdd"
                                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtAdd1" runat="server" Columns="40" MaxLength="50"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtAdd2" runat="server" Columns="40" MaxLength="50"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    City <span class="mandatory">&nbsp;*</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtCity" runat="server" Columns="40" MaxLength="40"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" Text="*" ControlToValidate="txtCity"
                                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    State
                                                                </td>
                                                                <td align="left">
                                                                    <uc1:LOVControl ID="LOVState" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    Country <span class="mandatory">&nbsp;*</span>
                                                                </td>
                                                                <td align="left">
                                                                    <uc1:LOVControl ID="LOVCountry" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    Pin Code
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtPin" runat="server" Columns="10" MaxLength="20"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Sector
                                                                </td>
                                                                <td>
                                                                    <uc1:LOVControl ID="LOVSector" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Currency<span class="mandatory">&nbsp;*</span>
                                                                </td>
                                                                </td>
                                                                <td>
                                                                    <uc1:LOVControl ID="LOVCurrency" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    Phone
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtPh1" runat="server" Columns="30" MaxLength="20"></asp:TextBox>
                                                                    <asp:TextBox ID="txtPh2" runat="server" Columns="30" MaxLength="20"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    Fax
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtFax1" runat="server" Columns="30" MaxLength="20"></asp:TextBox>
                                                                    <asp:TextBox ID="txtFax2" runat="server" Columns="30" MaxLength="20"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    Email
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtEmail1" runat="server" Columns="30" MaxLength="50"></asp:TextBox>
                                                                    <asp:TextBox ID="txtEmail2" runat="server" Columns="30" MaxLength="50"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel ID="b" runat="server" BackColor="AliceBlue">
                                                <HeaderTemplate>
                                                    License Details</HeaderTemplate>
                                                <ContentTemplate>
                                                    <div style="width: 100%; font-size: 8.25pt; background-color: aliceblue;">
                                                        <table id="CustomerDetails" cellpadding="2" cellspacing="2" style="background-color: aliceblue;">
                                                            <tr>
                                                                <td>
                                                                    VAT/LST No &amp; Date
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLST" runat="server" Columns="30" MaxLength="30"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <uc1:DatePicker ID="dtpLSTDt" runat="server" />
                                                                </td>
                                                                <td colspan="2">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    CST No &amp; Date
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCST" runat="server" Columns="30" MaxLength="30"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <uc1:DatePicker ID="dtpCSTDt" runat="server" />
                                                                </td>
                                                                <td colspan="2">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Service Tax No &amp; Date
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtST" runat="server" Columns="30" MaxLength="30"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <uc1:DatePicker ID="dtpST" runat="server" />
                                                                </td>
                                                                <td colspan="2">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    STP License No &amp; Dt
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSTP" runat="server" Columns="30" MaxLength="60"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <uc1:DatePicker ID="dtpstpDt" runat="server" />
                                                                </td>
                                                                <td>
                                                                    Valid Upto
                                                                </td>
                                                                <td>
                                                                    <uc1:DatePicker ID="dtpStpVpTo" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Custom License No &amp; Dt
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCustoms" runat="server" Columns="30" MaxLength="60"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <uc1:DatePicker ID="dtpCustDt" runat="server" />
                                                                </td>
                                                                <td>
                                                                    Valid Upto
                                                                </td>
                                                                <td>
                                                                    <uc1:DatePicker ID="dtpCustVpTo" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    TIN No &amp; Dt
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTinNo" runat="server" Columns="30" MaxLength="50"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <uc1:DatePicker ID="dtpTinDate" runat="server" />
                                                                </td>
                                                                <td>
                                                                    Valid Upto
                                                                </td>
                                                                <td>
                                                                    <uc1:DatePicker ID="dtpTinValidUpto" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel ID="c" runat="server" BackColor="AliceBlue">
                                                <HeaderTemplate>
                                                    Contact Details</HeaderTemplate>
                                                <ContentTemplate>
                                                    <div style="width: 100%; font-size: 8.25pt; background-color: aliceblue;">
                                                        <table id="ContactPersonDetails" cellpadding="2" cellspacing="2" style="background-color: aliceblue;">
                                                            <tr>
                                                                <td align="left">
                                                                    1)Name
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <asp:TextBox ID="txtCPName1" runat="server" Width="367px" MaxLength="30"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    Designation
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <asp:TextBox ID="txtCPDesig1" runat="server" Width="367px" MaxLength="30"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    Phone
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtCPph1" runat="server" Width="196px" MaxLength="20"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    Fax
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCPFax1" runat="server" Width="131px" MaxLength="20"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    Email
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <asp:TextBox ID="txtCPEmail1" runat="server" Width="367px" MaxLength="50"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    2)Name
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <asp:TextBox ID="txtCPName2" runat="server" Width="367px" MaxLength="30"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    Designation
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <asp:TextBox ID="txtCPDesig2" runat="server" Width="367px" MaxLength="30"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    Phone
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtCPPh2" runat="server" Width="196px" MaxLength="20"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    Fax
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCPFax2" runat="server" Width="131px" MaxLength="20"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    Email
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <asp:TextBox ID="txtCPEmail2" runat="server" Width="368px" MaxLength="50"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                        </cc1:TabContainer>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;">
                                    </td>
                                    <tr>
                                        <td>
                                            <uc1:ButtonControl ID="bcCustomer" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblMessage" runat="server" class="mandatory"></asp:Label>
                                        </td>
                                    </tr>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress DisplayAfter="1" ID="prgCustomer" runat="server">
                        <ProgressTemplate>
                            <div class="progressBackgroundFilter">
                            </div>
                            <div class="ProgressMessage">
                                <img alt="Loading..." src="../_assets/img/loader.gif" />
                                <br />
                                <span class="loadertext">Loading.....</span>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
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
