<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SymptomGroupMaster.aspx.cs" Inherits="ISPL.CSC.Web.Masters.SymptomGroupMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ButtonControl.ascx" TagName="ButtonControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="Controls" Namespace="ISPL.CSC.Web.Controls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Symptom group Masters</title>
    <link href="../_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../_assets/css/round.css" rel="Stylesheet" type="text/css" />  
</head>
<body>
    <form id="frmSymptomGroup" runat="server">
     <asp:ScriptManager ID="smSymptomGroup" runat="server">
    </asp:ScriptManager>
     <div class="rounded" style="width: 500px;">
        <div class="top-outer">
            <div class="top-inner">
                <div class="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <h2>
                                    <span id="lblHeading" runat="server">Symptom group</span>
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
                    <asp:UpdatePanel ID="updSymptomGroup" runat="server">
                        <ContentTemplate>
                            <table cellpadding="2" cellspacing="2" style="font-size: 8.25pt;">
                                <tr>
                                    <td style="width: 25%;">
                                        Short Name <span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtSName" MaxLength="255" Columns="15" runat="server"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvAction" runat="server" ErrorMessage="Code is required!"
                                            ControlToValidate="txtCode">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">
                                        Name <span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtName" MaxLength="255" Columns="15" runat="server"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvAction" runat="server" ErrorMessage="Code is required!"
                                            ControlToValidate="txtCode">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">
                                        Group Definition <span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtGrpDef" MaxLength="255" Columns="15" runat="server"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvAction" runat="server" ErrorMessage="Code is required!"
                                            ControlToValidate="txtCode">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">
                                        BB10Exclusive <span class="mandatory">&nbsp;*</span>
                                        
                                    </td>
                                    <td style="width: 75%;">
                                    <asp:DropDownList ID="ddlBB10Ex" runat="server" width="125px">
                                        <asp:ListItem Text="N" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                       <%-- <asp:TextBox ID="txtDesc" MaxLength="30" Columns="15" runat="server"></asp:TextBox>--%>
                                       <%-- <asp:RequiredFieldValidator ID="rfvDesc" runat="server" ErrorMessage="Description is required!"
                                            ControlToValidate="txtDesc">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                
                                
                                <tr>
                                    <td colspan="2">
                                        <uc1:ButtonControl ID="btnSymptomGroup" runat="server" OnEditButtonClick="Page_EditButton"
                                            OnDeleteButtonClick="Page_DeleteButton" OnSubmitButtonClick="Page_SubmitButton"
                                            OnCancelButtonClick="Page_CancelButton" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:UpdateProgress ID="prgSymptomGroup" runat="server">
                                            <ProgressTemplate>
                                                <img alt="Loading..." src="../_assets/img/loader.gif" />
                                                <span class="loadertext">Loading.....</span>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Label ID="lblMessage" class="mandatory" runat="server"></asp:Label>
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
