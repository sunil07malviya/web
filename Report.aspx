<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="ISPL.CSC.Web.Report" %>

<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Report</title>
    <link href="_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .text
        {
            background-color: aliceblue; /*background-color: #ffffff;  */
            padding: 5px 0px 0px 10px;
        }
    </style>
</head>
<body style="margin: 0px;">
    <form id="frmReport" runat="server">
    <div id="divPrint" runat="server" class="text" visible="true">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    Printer
                </td>
                <td style="width: 10px;">
                </td>
                <td>
                    <asp:DropDownList ID="ddlPrinter" runat="server" Width="300px">
                    </asp:DropDownList>
                </td>
                <td width="30">
                </td>
                <td>
                    <asp:ImageButton runat="server" ID="imgPaperSize" ImageUrl="~/_assets/img/buttons/btn_submit.gif"
                        onmouseover="this.src=this.src.replace('btn_submit.gif','btn_submit_over.gif');"
                        onmouseout="this.src=this.src.replace('btn_submit_over.gif','btn_submit.gif');"
                        OnClick="imgPaperSize_Click" />
                </td>
                <td width="10">
                </td>
                <td>
                    Paper Size
                </td>
                <td style="width: 10px;">
                </td>
                <td>
                    <asp:DropDownList ID="ddlPaper" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td width="30">
                </td>
                <td>
                    <asp:ImageButton runat="server" ID="imgPrint" ImageUrl="~/_assets/img/buttons/btn_print.gif"
                        onmouseover="this.src=this.src.replace('btn_print.gif','btn_print_over.gif');"
                        onmouseout="this.src=this.src.replace('btn_print_over.gif','btn_print.gif');"
                        OnClick="imgPrint_Click" />
                </td>
            </tr>
        </table>
    </div>
    <cr:CrystalReportViewer ID="rptViewer" DisplayGroupTree="False" HasCrystalLogo="False"
        PrintMode="ActiveX" runat="server" AutoDataBind="true" EnableToolTips="true"
        HasRefreshButton="True" HasToggleGroupTreeButton="False" HasViewList="False"
        ShowAllPageIds="true" Height="50px" Width="350px" />
    <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
