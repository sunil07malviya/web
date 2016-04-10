<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LocationMaster.aspx.cs" Theme="BlueTheme" Inherits="ISPL.CSC.Web.Masters.LocationMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ButtonControl.ascx" TagName="ButtonControl" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/LOVControl.ascx" TagName="LOVControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="Controls" Namespace="ISPL.CSC.Web.Controls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LocationMaster</title>
    <link href="../_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../_assets/css/round.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmLocationMaster" runat="server">
    <asp:ScriptManager ID="scLoaction" runat="server"></asp:ScriptManager>
    <div class="rounded" style="width:45%">
        <div class="top-outer">
            <div class="top-inner">
                <div class="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <h2>
                                    <span id="lblHeading" runat="server">Location</span>
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
                    <asp:UpdatePanel ID="updLocation" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <table cellpadding="2" cellspacing="2" style="font-size: 8.25pt;" width="100%">
                                <tr>
                                    <td>
                                        Location Name<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtName" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>
                                        Address 1
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtAddress1" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Address 2
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtAddress2" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        Address 3
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtAddress3" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        City<span class="mandatory">&nbsp;</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtCity" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>
                                        State<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td>
                                        <uc1:LOVControl ID="LovState" runat="server" />
                                        <asp:Label ID="lblstt" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Phone
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtPhone" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>
                                        E-Mail
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtEmail" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Signatory
                                    </td>
                                    <td>
                                        <uc1:LOVControl ID="LOVSignatory" runat="server"  />
                                        <asp:Label ID="lblsss" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        Receiving Project<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td>
                                        <uc1:LOVControl ID="LOVProject" runat="server"  />
                                        <asp:Label ID="lblppp" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                    <asp:CheckBox ID="ChkDefault" runat="server" />
                                        Default Flag
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TxtSlno" Columns="3" ReadOnly="True" Visible="False" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="TxtParentCode" Columns="3" ReadOnly="True" Visible="False" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                       <asp:ImageButton ID="btnSubmit" ImageUrl="~/_assets/img/buttons/btn_submit.gif" runat="server" />
                                        <asp:ImageButton ID="btnCancel" ImageUrl="~/_assets/img/buttons/btn_cancel.gif" runat="server" /> 
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:UpdateProgress ID="prgLocationMaster" runat="server">
                                            <ProgressTemplate>
                                                <img alt="Loading..." src="../_assets/img/loader.gif" />
                                                <span class="loadertext">Loading.....</span>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>                                                       
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Label ID="lblMessage" CssClass="errortext" runat="server"></asp:Label>
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
