﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServiceProvider.aspx.cs" Theme="BlueTheme" Inherits="ISPL.CSC.Web.Masters.ServiceProvider" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ButtonControl.ascx" TagName="ButtonControl" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/LOVControl.ascx" TagName="LOVControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="Controls" Namespace="ISPL.CSC.Web.Controls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ServiceProvider</title>
    <link href="../_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../_assets/css/round.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmServiceProvider" runat="server">
    <asp:ScriptManager ID="smSP" runat="server">
    </asp:ScriptManager>
    <div class="rounded" style="width:99%">
        <div class="top-outer">
            <div class="top-inner">
                <div class="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <h2>
                                    <span id="lblHeading" runat="server">Service Provider</span>
                                </h2>
                            </td>
                            <td align="right">
                                <span style="color: white; font-weight: bold; font-size: 7.5pt;">* Indicates Mandatory Entry</span>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="mid-outer">
            <div class="mid-inner">
                <div class="mid">
                    <asp:UpdatePanel ID="updSP" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <table cellpadding="2" border="0" cellspacing="1" style="font-size: 8.25pt" width="85%">
                                <tr>
                                    <td>
                                        Name<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtName" MaxLength="100" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtName" ErrorMessage="Name is Required .." Text="*" runat="server"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                       Short Name<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtShortName" MaxLength="50" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvShortName" ControlToValidate="txtShortName" ErrorMessage="Short Name Required .." Text="*" runat="server"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        SP Type<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlChaType" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Address 1 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddress1" runat="server" MaxLength="100"></asp:TextBox>
                                    </td>
                                    <td>
                                        Address 2 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddress2" runat="server" MaxLength="100"></asp:TextBox>
                                    </td>                                     
                                </tr>
                                <tr>                                    
                                    <td>
                                        Address 3 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddress3" runat="server" MaxLength="100"></asp:TextBox>
                                    </td>
                                    <td>
                                        City
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCity" MaxLength="50" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        State
                                    </td>
                                    <td>
                                        <uc1:LOVControl ID="LOVState" runat="server" />
                                    </td>
                                    <td>
                                        Country
                                    </td>
                                    <td>
                                        <uc1:LOVControl ID="LOVCountry" runat="server" />
                                    </td>                                    
                                </tr>
                                <tr>                                    
                                    <td>
                                        Zip Code
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtZipCode" MaxLength="15" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        URL For Tracking 
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtURL" Columns="150" runat="server" MaxLength="250" ToolTip="Please Enter This Type Of Format http://www.google.com"></asp:TextBox>
                                    </td>                                    
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <uc1:ButtonControl ID="btnServiceProvider" runat="server" OnEditButtonClick="Page_EditButton"
                                            OnDeleteButtonClick="Page_DeleteButton" OnSubmitButtonClick="Page_SubmitButton"
                                            OnCancelButtonClick="Page_CancelButton" />
                                    </td>                                    
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:UpdateProgress ID="prgServiceProviderMaster" runat="server">
                                            <ProgressTemplate>
                                                <img alt="Loading..." src="../_assets/img/loader.gif" />
                                                <span class="loadertext">Loading.....</span>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
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
