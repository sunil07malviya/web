<%@ Page Language="C#" AutoEventWireup="true" Theme="BlueTheme" CodeFile="City.aspx.cs" Inherits="ISPL.CSC.Web.Masters.City" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ButtonControl.ascx" TagName="ButtonControl" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/LOVControl.ascx" TagName="LOVControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="Controls" Namespace="ISPL.CSC.Web.Controls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>City</title>
    <link href="../_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../_assets/css/round.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmCity" runat="server">
    <asp:ScriptManager ID="smCity" runat="server">
    </asp:ScriptManager>
    <div class="rounded" style="width:99%">
        <div class="top-outer">
            <div class="top-inner">
                <div class="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <h2>
                                    <span id="lblHeading" runat="server">City Master</span>
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
                    <asp:UpdatePanel ID="updCity" runat="server" UpdateMode="Always">
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
                                    <td colspan="4">
                                        <uc1:ButtonControl ID="btnCity" runat="server" OnEditButtonClick="Page_EditButton"
                                            OnDeleteButtonClick="Page_DeleteButton" OnSubmitButtonClick="Page_SubmitButton"
                                            OnCancelButtonClick="Page_CancelButton" />
                                    </td>                                    
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:UpdateProgress ID="prgCity" runat="server">
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
