<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EndPointTest.aspx.cs" Theme="BlueTheme"
    Inherits="ISPL.CSC.Web.EndPointTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    End Point
                    <asp:TextBox ID="txtEndPoint" runat="server" Width="700px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    User
                    <asp:TextBox ID="txtUser" runat="server" Width="150px"></asp:TextBox>
                    Password
                    <asp:TextBox ID="txtPassword" runat="server" Width="150px"></asp:TextBox>
                    <asp:Button ID="btnExecute" runat="server" Text="Click" OnClick="btnExecute_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtXML" runat="server" TextMode="MultiLine" Width="500px" Height="500px"></asp:TextBox>
                    <asp:TextBox ID="txtResponse" runat="server" TextMode="MultiLine" Width="500px" Height="500px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
