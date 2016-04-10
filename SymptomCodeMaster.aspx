<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SymptomCodeMaster.aspx.cs"
    Inherits="ISPL.CSC.Web.Masters.SymptomCodeMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ButtonControl.ascx" TagName="ButtonControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="Controls" Namespace="ISPL.CSC.Web.Controls" %>
<%@ Register Src="~/Controls/LOVControl.ascx" TagName="LOVControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Symptom code Masters</title>
    <link href="../_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../_assets/css/round.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmSymptomCode" runat="server">
    <asp:ScriptManager ID="smSymptomCode" ScriptMode="Release"
        runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updSymptomCode" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="rounded" style="width: 85%;">
                <div class="top-outer">
                
                    <div class="top-inner">
                        <div class="top">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <h2>
                                            <span id="lblHeading" runat="server">Symptom Code</span>
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
                            <table cellpadding="2" border="0" cellspacing="0" style="font-size: 8.25pt;" width="100%">
                                <tr>
                                    <td style="width: 15%;">
                                        Model<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 35%;">
                                        <uc1:LOVControl ID="LOVModel" Columns="25" runat="server" Title="Model" />
                                        <%--<asp:TextBox ID="txtModel" MaxLength="30" Columns="15" runat="server"></asp:TextBox>--%>
                                        <%--<asp:RequiredFieldValidator ID="rfvModel" runat="server" ErrorMessage="Model is required!"
                                            ControlToValidate="txtModel">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td style="width: 15%;">
                                        ModelDesc
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtModelDesc" MaxLength="255" Columns="30" ReadOnly="true" runat="server"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RFVModelDesc" runat="server" ErrorMessage="ModelDesc is required!"
                                            ControlToValidate="txtModelDesc">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                        Symptom Group<span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 35%;">
                                        <uc1:LOVControl ID="LOVSymptomGroup" runat="server" Columns="25" />
                                    </td>
                                    <td style="width: 15%;">
                                        symptom group Desc
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtSymptomGroupDesc" MaxLength="255" Columns="30" ReadOnly ="true"  runat="server"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RFVSymptomGroupDesc" runat="server" ErrorMessage="symptom group Desc is required!"
                                            ControlToValidate="txtSymptomCode">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                        SymptomCode <span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtSymptomCode" MaxLength="255" Columns="30" runat="server"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ID="RFVSymptomCode" runat="server" ErrorMessage="Symptomcode is required!"
                                            ControlToValidate="txtSymptomCode">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td style="width: 15%;">
                                        Symptom Desc <span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtSymptomDesc" MaxLength="255" Columns="30" runat="server"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ID="RFVSymptomDesc" runat="server" ErrorMessage="Symptom Desc is required!"
                                            ControlToValidate="txtSymptomDesc">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                        Default FailureType <span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 35%;">
                                        <uc1:LOVControl ID="LOVFailureType" runat="server" Columns="25" />
                                    </td>
                                    <td style="width: 15%;">
                                        Default BillingType <span class="mandatory">&nbsp;*</span>
                                    </td>
                                    <td style="width: 35%;">
                                        <uc1:LOVControl ID="LOVBillingType" runat="server" Columns="25" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                        Override BillingAllowed
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtBillAllwd" MaxLength="255" Columns="30" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 15%;">
                                        Specific OverrideValue
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtSpfcOvrdVal" MaxLength="255" Columns="30" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                        TextKey FaultGroup
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtTextKey" MaxLength="255" Columns="30" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 15%;">
                                        CharacteristicValue
                                    </td>
                                    <td style="width: 35%;">
                                        <Controls:NumericTextBox ID="txtCharacteristicValue" MaxLength="30" Columns="30"
                                            runat="server"></Controls:NumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                        TextKey FaultCode
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtTextKey_FaultCode" MaxLength="255" Columns="30" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 15%;">
                                        RPMUseBillingtypedefault
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtRPMUseBillingtypedefault" MaxLength="255" Columns="30" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                        Chargeable
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtChargeable" MaxLength="255" Columns="30" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 15%;">
                                        ADHConsumable
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtADHConsumable" MaxLength="255" Columns="30" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                        Tampering
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtTampering" MaxLength="255" Columns="30" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 15%;">
                                        BBEPPNotApplicable
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtBBEPPNotApplicable" MaxLength="255" Columns="30" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                        BER
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtBER" MaxLength="255" Columns="30" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                        F19
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtF19" MaxLength="255" Columns="30" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                        NFF
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="txtNFF" MaxLength="255" Columns="30" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <%-- <tr>
                  <td colspan="2"><asp:ValidationSummary ID="VS" runat="server"></asp:ValidationSummary>
                  </td>
                  </tr> --%>
                                <tr>
                                    <td colspan="2">
                                        <uc1:ButtonControl ID="btnSymptomCode" runat="server" OnEditButtonClick="Page_EditButton"
                                            OnDeleteButtonClick="Page_DeleteButton" OnSubmitButtonClick="Page_SubmitButton"
                                            OnCancelButtonClick="Page_CancelButton" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <%-- <asp:UpdateProgress ID="prgSymptomCode" runat="server">
                                            <ProgressTemplate>
                                                <img alt="Loading..." src="../_assets/img/loader.gif" />
                                                <span class="loadertext">Loading.....</span>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
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
