<%@ Page Language="C#" AutoEventWireup="true" Theme="BlueTheme" CodeFile="Supplier.aspx.cs" Inherits="ISPL.CSC.Web.Masters.Supplier" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ButtonControl.ascx" TagName="ButtonControl" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/LOVControl.ascx" TagName="LOVControl" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register TagPrefix="Controls" Namespace="ISPL.CSC.Web.Controls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Supplier Master</title>
    <link href="../_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../_assets/css/round.css" rel="Stylesheet" type="text/css" />  
    <link href="../_assets/css/Tabs.css" rel="Stylesheet" type="text/css" />  
</head>
<body>
     <form id="frmSupplier" runat="server">
    <asp:ScriptManager AsyncPostBackTimeout="0" ScriptMode="Release" ID="smSupplier"
        runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updSupplier" runat="server" UpdateMode="Always">
        <ContentTemplate>
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
                            <table cellpadding="2" cellspacing="2" style="font-size: 8.25pt;">
                                <tr>
                                    <td style="width: 15%;">
                                        Name<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtName" MaxLength="60" Columns="35" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvName" runat="server" Text="*" Display="Dynamic"
                                            ControlToValidate="txtName"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="width: 15%;">
                                        Supplier Type<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:DropDownList ID="ddlSupplierType" runat="server" Width="176px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                        Short Name<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtShortName" MaxLength="50" Columns="8" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvShortName" runat="server" Text="*"
                                            ControlToValidate="txtShortName"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="width: 15%;">
                                        Category<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:DropDownList ID="ddlCategory" runat="server" Width="176px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                    </td>
                                    <td style="width: 35%;">
                                    </td>
                                    <td style="width: 15%;">
                                        Manufacturer/Dealer
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:DropDownList ID="ddlMfrDlr" runat="server" Width="176px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        <cc1:TabContainer ID="TabSupp" runat="server" BackColor="AliceBlue" ActiveTabIndex="0">
                                            <cc1:TabPanel ID="adr" runat="server" BackColor="AliceBlue">
                                                <HeaderTemplate>
                                                    Address Details</HeaderTemplate>
                                                <ContentTemplate>
                                                    <table id="AddressDetails" border="0" cellpadding="2" cellspacing="2" style="font-size: 8.25pt;
                                                        height: 100%; width: 100%; font-family: Tahoma; background-color: aliceblue;">
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                Address<span class="mandatory">&nbsp;*</span>
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtAdd" MaxLength="50" Columns="40" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvAdd" runat="server" ControlToValidate="txtAdd"
                                                                    Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtAdd1" MaxLength="50" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtAdd2" MaxLength="50" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                City<span class="mandatory">&nbsp;*</span>
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtCity" MaxLength="50" Columns="40" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvCity" runat="server" Text="*" Display="Dynamic"
                                                                    ControlToValidate="txtCity"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                Pin
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtPin" MaxLength="10" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                State
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <uc1:LOVControl ID="LOVState" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                Country<span class="mandatory">&nbsp;*</span>
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <uc1:LOVControl ID="LOVCountry" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                Phone
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtPh" MaxLength="50" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                Fax
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtFax" MaxLength="50" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                E-Mail ID
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtEmail" MaxLength="50" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                Contact
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtCP" MaxLength="50" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel ID="ExTab" runat="server" BackColor="AliceBlue">
                                                <HeaderTemplate>
                                                    Excise Details</HeaderTemplate>
                                                <ContentTemplate>
                                                    <table id="ExciseDetails" cellpadding="2" cellspacing="2" style="font-size: 8.25pt;
                                                        height: 100%; width: 100%; font-family: Tahoma; background-color: aliceblue;">
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                ECC No.
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtECC" MaxLength="30" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                Range
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtRange" MaxLength="30" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                Address
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtRAdd1" MaxLength="30" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr4">
                                                            <td style="width: 30%;">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtRAdd2" MaxLength="30" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                City
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtRCity" MaxLength="30" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                Pin Code
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtRPin" MaxLength="30" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                Division
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtDivision" MaxLength="30" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                Commissionerate
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtCommission" MaxLength="30" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel ID="taxtab" runat="server" BackColor="AliceBlue">
                                                <HeaderTemplate>
                                                    Tax Details</HeaderTemplate>
                                                <ContentTemplate>
                                                    <table id="TaxDetails" cellpadding="2" cellspacing="2" style="font-size: 8.25pt;
                                                        height: 100%; width: 100%; font-family: Tahoma; background-color: aliceblue;">
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                LST Registration No.
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtLSTregnNo" MaxLength="30" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                LST Registration Date
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <uc1:DatePicker ID="dtpLSTLicenseDate" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                CST Registration No.
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtCSTregnNo" MaxLength="30" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                CST Registration Date
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <uc1:DatePicker ID="dtpCSTLicenseDate" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                Service Tax No.
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtST" MaxLength="30" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                Service Tax Date
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <uc1:DatePicker ID="dtpST" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                TIN No.
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <asp:TextBox ID="txtTinNo" MaxLength="20" Columns="40" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                TIN Date
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <uc1:DatePicker ID="dtpTINDate" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                        </cc1:TabContainer>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%;">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <uc1:ButtonControl ID="btnSupplier" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:UpdateProgress DisplayAfter="1" ID="prgSupplier" runat="server">
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
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Label ID="lblMessage" class="mandatory" runat="server"></asp:Label>
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
    </form>
</body>
</html>
