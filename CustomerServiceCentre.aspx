<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerServiceCentre.aspx.cs" Inherits="ISPL.CSC.Web.CustomerServiceCentre" %>
<%@ Register Src="~/Controls/NavBar.ascx" TagName="NavBar" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CSC</title>
    <link href="_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="_assets/css/grid.css" rel="stylesheet" type="text/css" />
    
     <script language="javascript" type="text/javascript">
        document.onkeydown = checkKeys
        function checkKeys() {
            if (event.ctrlKey && event.keyCode == 78) {
                event.keyCode = 0;
                return false;
            }

        }
    </script>
    
     <style type="text/css">
        html, body
        {
            margin: 0px;
            height: 100%;
            font-family: Tahoma;
            font-size: 8.25pt;
            background-color: aliceblue;
        }
        #tblCustomerServiceCentre
        {
            height: 100%;
            width: 100%;
            font-size: 8.25pt;
        }
    </style>
    
     <script language="javascript" type="text/javascript">

        function fnToggleMenu() {
            var img1 = document.getElementById('imgToggle');
            var hdnFlag = document.getElementById('hdnFlag');
            var CanOpen = hdnFlag.value;
            var src = img1.src;
            var blnCondition;

            if (src.indexOf('left') > 0)
                blnCondition = 'right';
            else if (src.indexOf('right') > 0)
                blnCondition = 'left';

            if (CanOpen == 'false') {
                mnuContainer.style.visibility = 'hidden';
                mnuContainer.style.width = '0px';
                mnuContainer.style.display = 'none';
                img1.src = '_assets/img/arrow_' + blnCondition + '.gif';
            }
            else if (CanOpen == 'true') {
                if (mnuContainer.style.width == '0px') {
                    mnuContainer.style.visibility = 'visible';
                    mnuContainer.style.width = '200px';
                    mnuContainer.style.display = '';
                    img1.src = '_assets/img/arrow_' + blnCondition + '.gif';
                }
                else {
                    mnuContainer.style.visibility = 'hidden';
                    mnuContainer.style.width = '0px';
                    mnuContainer.style.display = 'none';
                    img1.src = '_assets/img/arrow_' + blnCondition + '.gif';
                }
            }
        }

        function fnLockMenu() {
            var img1 = document.getElementById('imgToggle');

            mnuContainer.style.visibility = 'hidden';
            mnuContainer.style.width = '0px';
            mnuContainer.style.display = 'none';

            img1.src = '_assets/img/arrow_right.gif';
        }  

    </script>

</head>
<body>
   <form id="frmCustomerServiceCentre" runat="server">
    <asp:ScriptManager ID="smEOSOFT" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="session" runat="server" />
    <table id="tblCustomerServiceCentre" border="0" cellpadding="0" cellspacing="0">
        <tr style="height: 100px;">
            <td colspan="3">
                <uc1:NavBar ID="nbCustomerServiceCentre" runat="server" />
            </td>
        </tr>
        <tr>
            <td id="mnuContainer" style="width: 200px; vertical-align: top;">
                <div style="width: 200px; height: 100%; overflow: auto; scrollbar-face-color: #bbd9ee;
                    scrollbar-shadow-color: #FFFFFF; scrollbar-highlight-color: #FFFFFF; scrollbar-3dlight-color: #FFFFFF;
                    scrollbar-darkshadow-color: #FFFFFF; scrollbar-track-color: #FFFFFF; scrollbar-arrow-color: #FFFFFF;">
                    <asp:UpdatePanel ID="updEOSOFT" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:TreeView ID="tvwMenu" runat="server" ShowLines="true" SkipLinkText="true" Font-Size="8.50pt"
                                ForeColor="#0066cc" ExpandDepth="0" Target="ifrmCustomerServiceCentre" OnTreeNodeExpanded="tvwMenu_TreeNodeExpanded"
                                OnSelectedNodeChanged="tvwMenu_SelectedNodeChanged" SelectedNodeStyle-Font-Bold="True"
                                NodeStyle-Font-Bold="False" LeafNodeStyle-Font-Bold="False">
                            </asp:TreeView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                   <input id="hdnLockMenu" type="hidden" onclick="fnLockMenu();" />
                    <input id="hdnFlag" type="hidden" value="true" />
                </div>
            </td>
            <td style="text-align: center; vertical-align: middle; width: 10px; border: solid 1px #bbd9ee;
                cursor: pointer; border-bottom: none; border-top: none; font-size: 14pt; font-weight: normal;
                font-family: Arial Black; color: #0066cc" onclick="fnToggleMenu();" onmouseover="this.style.backgroundColor='#bbd9ee';"
                onmouseout="this.style.backgroundColor='aliceblue';">
                M<br />
                E<br />
                N<br />
                U<br />
                <br />
                <img id="imgToggle" src="_assets/img/arrow_left.gif" alt="Hide / Show Menu" />
            </td>
              <td valign="top" style="width: 100%; height: 100%;">
                <iframe id="ifrmCustomerServiceCentre" height="500px" runat="server" name="ifrmCustomerServiceCentre" frameborder="0"
                  src="DashBoard.aspx"   width="100%"></iframe>
                   
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
