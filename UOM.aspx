<%@ Page Language="C#" AutoEventWireup="true" Theme="BlueTheme" CodeFile="UOM.aspx.cs" Inherits="ISPL.CSC.Web.Masters.UOM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ButtonControl.ascx" TagName="ButtonControl" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/LOVControl.ascx" TagName="LOVControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="Controls" Namespace="ISPL.CSC.Web.Controls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UOM</title>
    <link href="../_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../_assets/css/round.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="frmUOM" runat="server">
    <asp:ScriptManager ID="smUOM" runat="server">
    </asp:ScriptManager>
    <div class="rounded" style="width:30%">
        <div class="top-outer">
            <div class="top-inner">
                <div class="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <h2>
                                    <span id="lblHeading" runat="server">UOM </span>
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
                    <asp:UpdatePanel ID="updUom" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <table cellpadding="2" cellspacing="2" style="font-size: 8.25pt;">
                                <tr>
                                    <td style="width: 25%;">
                                        Name<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtName" MaxLength="20" Columns="20" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">
                                        Short Name<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtShortName" MaxLength="3" Columns="8" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <uc1:ButtonControl ID="btnUOM" runat="server" OnEditButtonClick="Page_EditButton"
                                            OnDeleteButtonClick="Page_DeleteButton" OnSubmitButtonClick="Page_SubmitButton"
                                            OnCancelButtonClick="Page_CancelButton" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:UpdateProgress ID="prgUOM" runat="server">
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
