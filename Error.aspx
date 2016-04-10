<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="ISPL.CSC.Web.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="_assets/css/Tabs.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmError" runat="server">
    <table border="0" width="100%">
        <tr>
            <td align="center">
                <h3>
                    An error occurred while processing your request.</h3>
            </td>
        </tr>
        <tr>
            <td align="center">
                The error has been reported. We apologize for any inconvience.
            </td>
        </tr>
        <tr>
            <td align="center">
                Page URL :
                <asp:Label ID="lblURL" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                Visitor IP :
                <asp:Label ID="lblIP" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                Message :
                <asp:Label ID="lblException" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
