<%@ Page Language="C#" AutoEventWireup="true" Theme="BlueTheme" CodeFile="ShipmentStages.aspx.cs" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" Inherits="ISPL.CSC.Web.Masters.ShipmentStages" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ButtonControl.ascx" TagName="ButtonControl" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register TagPrefix="Controls" Namespace="ISPL.CSC.Web.Controls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ShipmentStages</title>
    <link href="../_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../_assets/css/round.css" rel="Stylesheet" type="text/css" />     
    <link href="../_assets/css/grid.css" rel="Stylesheet" type="text/css" />   
    <script language="javascript" type="text/javascript">
    function fnSelect(GridID, RowID)
    {
    __doPostBack(GridID,'Select$' + RowID)
    }
    </script>
</head>
<body>
    <form id="frmShipmentStages" runat="server">
    <asp:ScriptManager ID="smShipmentStages" runat="server">
    </asp:ScriptManager>
    <div class="rounded" style="width:80%">
        <div class="top-outer">
            <div class="top-inner">
                <div class="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <h2>
                                    <span id="lblHeading" runat="server">Shipment Stages</span>
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
                    <asp:UpdatePanel ID="updShipmentstages" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <table cellpadding="2" border="0" cellspacing="1" style="font-size: 8.25pt" width="100%">
                                <tr>
                                    <td>
                                    <div class="grid" style="width:100%">
                                       <asp:GridView ID="gvshipmentstatus" CssClass="datatable" runat="server" AutoGenerateColumns="False"
                                                    SelectedRowStyle-CssClass="selected" Width="99%" Height="100px" 
                                                    AllowPaging="True" PageSize="5" OnSelectedIndexChanged="gvshipmentstatus_SelectedIndexChanged"
                                                     OnRowDataBound="gvshipmentstatus_RowDataBound" >
                                                    <RowStyle CssClass="row" />
                                                    <PagerStyle CssClass="pager" />
                                                </asp:GridView>
                                    </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="DivStages">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                               <%-- <tr>
                                                    <td>
                                                        First Stage 
                                                        OnPageIndexChanging="gvshipmentstatus_PageIndexChanging"
                                                        OnPreRender="gvshipmentstatus_PreRender"
                                                        OnSorting="gvshipmentstatus_Sorting"
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFirstStage" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        First Stage Status
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFirstStageStatus" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td>
                                                    </tr>
                                                <tr>
                                                    <td>
                                                        Second Stage
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSecondStage" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td> 
                                                    <td>
                                                        Second Stage Status
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSecondStageStatus" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td>
                                                    </tr>--%>
                                                <tr>
                                                    <td>
                                                       <span id="lblremarks" runat="server">Status</span>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="TxtRemarks" Columns="100" MaxLength="1000" runat="server" 
                                                            Width="639px"></asp:TextBox>
                                                    </td>                                                   
                                                </tr>                                        
                                            </table>        
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <uc1:ButtonControl ID="btnShipmentStatus" runat="server" />
                                    </td>                                    
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:UpdateProgress ID="prgCarrierMaster" runat="server">
                                            <ProgressTemplate>
                                                <img alt="Loading..." src="../_assets/img/loader.gif" />
                                                <span class="loadertext">Loading.....</span></ProgressTemplate>
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
