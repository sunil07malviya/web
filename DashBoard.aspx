<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="ISPL.CSC.Web.DashBoard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dash Board </title>
    <link href="_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="_assets/css/grid.css" rel="stylesheet" type="text/css" />
    <link href="_assets/css/round.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmDashBoard" runat="server">  
    <div class="rounded" runat="server" id="divDashBoard">
        <div class="top-outer">
            <div class="top-inner">
                <div class="top">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border: none;">
                                        <tr>
                                            <td valign="bottom">
                                                <%--<asp:ImageButton ID="imgAddFavorites" OnClick="imgAddFavorites_Click" runat="server"
                                                    ImageUrl="~/_assets/img/addfavorites.gif" />
                                                <asp:ImageButton ID="imgDelFavorites" OnClick="imgDelFavorites_Click" runat="server"
                                                    ImageUrl="~/_assets/img/delfavorites.gif" />&nbsp;--%>
                                            </td>
                                            <td>
                                                <h2>
                                                     Exception Details<span id="lblHeading" runat="server"></span>
                                                </h2>
                                            </td>
                                            <td style="text-align: right; vertical-align: bottom; padding-right: 5px;">
                                                <%--<table border="0" cellpadding="0" cellspacing="0" style="font-size: 8.25pt; font-family: Tahoma;
                                                    font-weight: bold; color: White;">
                                                    <tr visible="false">
                                                        <td>
                                                            <asp:Label ID="lblPeriod" runat="server" Text="Period:" Visible="false"></asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;&nbsp;<%--<uc1:DatePicker ID="dtpFromDate" runat="server" BorderStyle="None" FontSize="8pt"
                                                                Width="70px" Visible="false" />
                                                        </td>
                                                        <td>
                                                            &nbsp;<asp:Label ID="lblPeriodTo" runat="server" Text="&" Visible="false"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td>
                                                            <%--<uc1:DatePicker ID="dtpToDate" runat="server" BorderStyle="None" FontSize="8pt" Width="70px" Visible="false" />
                                                        </td>
                                                    </tr>
                                                </table>--%>
                                            </td>
                                            <td style="font-size: 8.25pt; font-family: Tahoma; font-weight: bold; color: White;
                                                text-align: right; padding-right: 10px; vertical-align: middle;">
                                                Search
                                                <asp:TextBox ID="txtSearch" Font-Names="Tahoma" Font-Size="8.25pt" BorderStyle="None"
                                                    runat="server"></asp:TextBox>
                                                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/_assets/img/zoom.ico"
                                                    ToolTip="Search" Style="vertical-align: middle; cursor: pointer; width: 16px;"
                                                    OnClick="btnSearch_Click" />&nbsp;
                                                    <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/_assets/img/refresh.ico"
                                                    ToolTip="Refresh / Clear" Style="vertical-align: middle; cursor: pointer;" OnClick="btnRefresh_Click" />
                                                &nbsp;
                                                
                                            </td>
                                        </tr>
                                    </table>
                </div>
            </div>
        </div>
        <div class="mid-outer"><asp:Label ID="lblCaption" runat="server" Font-Names="tahoma" Font-Size="8pt" ForeColor="#0066cc"></asp:Label>
            <div class="mid-inner">
                <div class="mid">
                    <div id="divgvResults" runat="server" style="float: left; overflow: auto; width: 100%;
                        height: 490px; border: solid 1px #0066CC;" class="grid">
                        <asp:GridView ID="gvDashBoard" runat="server"  AllowSorting="true" AllowPaging="true" PageSize="15" CellPadding="0"
                            CellSpacing="0" AutoGenerateColumns="true" Style="width: 100%; overflow: auto"
                            CaptionAlign="Left" OnSorting="gvDashBoard_Sorting" 
                            CssClass="datatable" OnPageIndexChanging="gvDashBoard_PageIndexChanged">                                                
                            <RowStyle CssClass="row" />
                            <SelectedRowStyle CssClass="selected" />
                            <PagerStyle CssClass="pager" />
                            <EmptyDataTemplate>
                                <div style="color: Red; font-weight: bold; text-align: center;">
                                    &nbsp;
                                </div>
                            </EmptyDataTemplate>                                                
                        </asp:GridView>
                    </div>
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
