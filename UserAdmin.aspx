<%@ Page Language="C#" AutoEventWireup="true" Theme="BlueTheme" CodeFile="UserAdmin.aspx.cs" Inherits="ISPL.CSC.Web.Masters.UserAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/Controls/ButtonControl.ascx" TagName="ButtonControl" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/LOVControl.ascx" TagName="LOVControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="Controls" Namespace="ISPL.CSC.Web.Controls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User Admin</title>
    <link href="../_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../_assets/css/round.css" rel="Stylesheet" type="text/css" />
    <link href="../_assets/css/grid.css" rel="Stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
    function fnClickStatus(obj)
    {
        var Add = document.getElementById(obj.replace('Status', 'Add'));
        var Edit = document.getElementById(obj.replace('Status', 'Edit'));
        var Delete = document.getElementById(obj.replace('Status', 'Delete'));
        var Print = document.getElementById(obj.replace('Status', 'Print'));
        
        Add.checked = document.getElementById(obj).checked;
        Edit.checked = document.getElementById(obj).checked;
        Delete.checked = document.getElementById(obj).checked;
        Print.checked = document.getElementById(obj).checked;
      }
     function fnClickHeader(obj, Flag)
     {     
        var gvUsers = document.getElementById('gvUsers');
        
         for (i = 2; i <= gvUsers.rows.length; i++)
         {            
            var Str;
            if (i < 10)
                Str = obj.replace('ctl01', 'ctl0'+i);
            else
                Str = obj.replace('ctl01', 'ctl'+i);

            switch(Flag)
            {
                case 'Status':
                {
                    var Status =  document.getElementById(Str);
                    Status.checked = document.getElementById(obj).checked;
                
                    fnClickStatus(Str);
                }
                break;
                default:
                {
                    var Add =  document.getElementById(Str);
                    Add.checked = document.getElementById(obj).checked;
                }
                break;
            }
        }
        }
    </script>

</head>
<body>
    <form id="frmUserAdmin" runat="server">
    <asp:ScriptManager ID="smUserAdmin" runat="server">
    </asp:ScriptManager>
       <div class="rounded"  style="width: 700px;">
        <div class="top-outer">
            <div class="top-inner">
                <div class="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <h2>
                                    <span id="lblHeading" runat="server"></span>
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
                <asp:UpdatePanel ID="updUserAdmin" runat="server" UpdateMode="Always">
                <ContentTemplate>                 
                            <div style="font-size: 8.25pt; text-align: left; float: left; width: 175px; height: 375px;">
                                User ID<span class="mandatory">&nbsp;*</span><br />
                                <asp:TextBox ID="txtUserID" runat="server" MaxLength="50" TabIndex="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUserID" runat="server" ErrorMessage="User ID is required!"
                                    ControlToValidate="txtUserID">*</asp:RequiredFieldValidator>
                                <br />
                                <br />
                                User Name<span class="mandatory">&nbsp;*</span><br />
                                <asp:TextBox ID="txtUserName" runat="server" MaxLength="50" TabIndex="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="User Name is required!"
                                    ControlToValidate="txtUserName">*</asp:RequiredFieldValidator>
                                <br />
                                <br />
                                Password<span class="mandatory">&nbsp;*</span><br />
                                <asp:TextBox ID="txtPassword" runat="server" MaxLength="50" TextMode="Password" 
                                    TabIndex="3"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Password is required!"
                                    ControlToValidate="txtPassword">*</asp:RequiredFieldValidator>
                                <ajaxtoolkit:PasswordStrength ID="psPassword" runat="server" TargetControlID="txtPassword"
                                    DisplayPosition="BelowLeft" StrengthIndicatorType="Text" PreferredPasswordLength="8"
                                    PrefixText="Meets Policy? " TextCssClass="TextIndicator" MinimumNumericCharacters="2"
                                    MinimumSymbolCharacters="2" RequiresUpperAndLowerCaseCharacters="true" MinimumLowerCaseCharacters="2"
                                    MinimumUpperCaseCharacters="1" TextStrengthDescriptions="Not at all;Very Low compliance;Low Compliance;Average Compliance;Good Compliance;Very High Compliance;Yes"
                                    HelpHandleCssClass="TextIndicator_Handle" HelpHandlePosition="leftside">
                                </ajaxtoolkit:PasswordStrength>
                                <br />
                                <br />
                                Designation<br />
                                <asp:TextBox ID="txtDesignation" runat="server" TabIndex="4" MaxLength="50"></asp:TextBox>
                                <br />
                                <br />
                                EMail<br />
                                <asp:TextBox ID="txtEMail" runat="server" TabIndex="5"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revEMail" runat="server" ControlToValidate="txtEMail"
                                    ErrorMessage="Not Valid" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator><br />
                                <br />
                                Enterprise ID<br />
                                <asp:TextBox ID="txtEnterpriseID" TabIndex="6" runat="server" MaxLength="50"></asp:TextBox><br />
                                Password Entry Date:
                                <br /><asp:Label ID="lblpasswordcreated" runat="server" Font-Bold="True" 
                                    ForeColor="#0066CC"></asp:Label>
                                <br /><br />
                                <span id="lblLicenses" runat="server">Company & Locations:</span>
                                <br />
                                <asp:ListBox ID="lstLicenses" runat="server" Width="175px" SelectionMode="Multiple">
                                </asp:ListBox>
                                <br />                               
                               
                            </div>
                            <div class="grid" style="width:500px; height: 375px; float: right; overflow: auto;">
                                <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" AllowSorting="true"
                                    CssClass="datatable" CellPadding="0" CellSpacing="0" BorderWidth="0" GridLines="None"
                                    Width="85%" SelectedRowStyle-CssClass="selected">
                                    <RowStyle CssClass="row" />
                                    <Columns>
                                        <asp:BoundField Visible="false" DataField="G_MENU_PARENTCODE"></asp:BoundField>
                                        <asp:BoundField Visible="false" DataField="G_MENU_SLNO"></asp:BoundField>
                                        <asp:BoundField Visible="false" DataField="G_MENU_LEVEL"></asp:BoundField>
                                        <asp:TemplateField ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="first">
                                            <HeaderTemplate>
                                                Status<br />
                                                <asp:CheckBox BackColor="#bbd9ee" onclick="fnClickHeader(this.id,'Status');" ID="Status"
                                                    EnableTheming="false" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ONCLICk="fnClickStatus(this.id);" ID="Status" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.GT_USERS_STATUSFLAG")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HtmlEncode="false" DataField="G_MENU_NAME" HeaderText="Menu Name">
                                        </asp:BoundField>
                                        <asp:TemplateField ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                Add<br />
                                                <asp:CheckBox BackColor="#bbd9ee" onclick="fnClickHeader(this.id,'Add');" ID="Add"
                                                    EnableTheming="false" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="Add" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.GT_USERS_ADDFLAG")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                Edit<br />
                                                <asp:CheckBox BackColor="#bbd9ee" onclick="fnClickHeader(this.id,'Edit');" ID="Edit"
                                                    EnableTheming="false" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="Edit" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.GT_USERS_EDITFLAG")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                Delete<br />
                                                <asp:CheckBox BackColor="#bbd9ee" onclick="fnClickHeader(this.id,'Delete');" ID="Delete"
                                                    EnableTheming="false" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="Delete" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.GT_USERS_DELETEFLAG")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                Print<br />
                                                <asp:CheckBox BackColor="#bbd9ee" onclick="fnClickHeader(this.id,'Print');" ID="Print"
                                                    EnableTheming="false" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="Print" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.GT_USERS_PRINTFLAG")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>   
                                <br />                           
                            </div>
                            <div>
                                <uc1:ButtonControl ID="btnUserAdmin" runat="server" OnEditButtonClick="Page_EditButton"
                                    OnDeleteButtonClick="Page_DeleteButton" OnSubmitButtonClick="Page_SubmitButton"
                                    OnCancelButtonClick="Page_CancelButton" />                              
                                <asp:Label ID="lblMessage" CssClass="mandatory" runat="server"></asp:Label>
                                <asp:UpdateProgress ID="prgUserAdmin" runat="server">
                                    <ProgressTemplate>
                                        <img alt="Loading..." src="../_assets/img/loader.gif" />
                                        <span class="loadertext">Loading.....</span>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                
                            </div>
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
