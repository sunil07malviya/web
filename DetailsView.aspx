<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailsView.aspx.cs" Inherits="ISPL.CSC.Web.DetailsView" %>

<%@ Register TagName="DatePicker" TagPrefix="uc1" Src="~/Controls/DatePicker.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="_assets/css/grid.css" rel="stylesheet" type="text/css" />
    <link href="_assets/css/round.css" rel="stylesheet" type="text/css" />
    <!--[if IE 6]>
    
    <style type="text/css">
        body, table
        {
            behavior: url(_assets/css/csshover.htc);
        }
    </style>

    <![endif]-->

    <script language="javascript" type="text/javascript">
        function fnOpenPage(PageName) {
            window.parent.frames[0].location = PageName;
            return false;
        }
        function fnAddFavouritesList(Name, Value) {
            var SelectBox = window.parent.document.getElementById('nbCustomerServiceCentre_ddlFavourites');
            var imgAdd = document.getElementById('imgAddFavorites');
            var imgDel = document.getElementById('imgDelFavorites');

            var Check = true;
            for (i = 0; i < SelectBox.options.length; i++) {
                if (SelectBox.options[i].text == Name) {
                    Check = false;
                    break;
                }
            }
            if (Check) {
                var newOption = document.createElement('OPTION');
                newOption.text = Name;
                newOption.value = Value;
                SelectBox.options.add(newOption);
            }
        }
        function fnDelFavouritesList(Name) {
            var SelectBox = window.parent.document.getElementById('nbCustomerServiceCentre_ddlFavourites');

            for (i = SelectBox.options.length - 1; i >= 0; i--) {
                if (SelectBox.options[i].text == Name) {
                    SelectBox.remove(i);
                    break;
                }
            }
        }
    </script>

</head>
<body style="background-color: aliceblue;">
    <form id="frmDetailsView" runat="server">
    <asp:ScriptManager ID="smDetailsView" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="grid">
                    <div class="rounded">
                        <div class="top-outer">
                            <div class="top-inner">
                                <div class="top">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border: none;">
                                        <tr>
                                            <td valign="bottom">
                                                <asp:ImageButton ID="imgAddFavorites" OnClick="imgAddFavorites_Click" runat="server"
                                                    ImageUrl="~/_assets/img/addfavorites.gif" />
                                                <asp:ImageButton ID="imgDelFavorites" OnClick="imgDelFavorites_Click" runat="server"
                                                    ImageUrl="~/_assets/img/delfavorites.gif" />&nbsp;
                                            </td>
                                            <td>
                                                <h2>
                                                    <span id="lblHeading" runat="server"></span>
                                                </h2>
                                            </td>
                                            <td style="text-align: right; vertical-align: bottom; padding-right: 5px;">
                                                <table border="0" cellpadding="0" cellspacing="0" style="font-size: 8.25pt; font-family: Tahoma;
                                                    font-weight: bold; color: White;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblPeriod" runat="server" Text="Period:" Visible="false"></asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            <uc1:DatePicker ID="dtpFromDate" runat="server" BorderStyle="None" FontSize="8pt"
                                                                Width="70px" Visible="false" />
                                                        </td>
                                                        <td>
                                                            &nbsp;<asp:Label ID="lblPeriodTo" runat="server" Text="&" Visible="false"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td>
                                                            <uc1:DatePicker ID="dtpToDate" runat="server" BorderStyle="None" FontSize="8pt" Width="70px" Visible="false" />
                                                        </td>
                                                    </tr>
                                                </table>
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
                                                <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/_assets/img/add.ico" ToolTip="Add New"
                                                    Style="vertical-align: middle; cursor: pointer;" />&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="mid-outer">
                            <div class="mid-inner">
                                <div class="mid">
                                    <div style="width: 100%;">
                                        <div style="text-align: left;">
                                            <asp:Label ID="lblCaption" runat="server" Font-Names="tahoma" Font-Size="8pt" ForeColor="#0066cc">Total Records: 100</asp:Label>
                                        </div>
                                    </div>
                                    <div>
                                        <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                            AllowSorting="true" CssClass="datatable" PageSize="15" CellPadding="0" CellSpacing="0"
                                            BorderWidth="0" GridLines="None" SelectedRowStyle-CssClass="selected" OnPageIndexChanging="gvDetails_PageIndexChanging"
                                            OnSorting="gvDetails_Sorting" OnRowDataBound="gvDetails_RowDataBound">
                                            <PagerStyle CssClass="pager" />
                                            <RowStyle CssClass="row" />
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
                        <%--<div style="text-align: center;">
                            <table border="0" style="font-size: 8.25pt;">
                                <tr>
                                    <td style="background-color: #FAFAD2; padding: 2px; color: Red; border: solid 1px #0066cc;">
                                        &nbsp;&nbsp;Pending Transaction&nbsp;&nbsp;
                                    </td>
                                    <td style="border: solid 1px #0066cc; padding: 2px; color: #0066cc;">
                                        &nbsp;&nbsp;Completed Transaction&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>--%>
                        <asp:UpdateProgress ID="prgDetailsView" runat="server" DisplayAfter="1">
                        <ProgressTemplate>
                            <div class="progressBackgroundFilter">
                            </div>
                            <div class="ProgressMessage">
                                <img alt="Loading..." src="../_assets/img/loader.gif" />
                                <br />
                                <span class="loadertext">Please wait while the page is refreshing...</span>
                            </div>
                        </ProgressTemplate>
                     </asp:UpdateProgress>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
