<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AboutEOSOFT.aspx.cs" Inherits="ISPL.CSC.Web.AboutEOSOFT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>About CSC</title>
    <link href="_assets/css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="_assets/css/grid.css" rel="stylesheet" type="text/css" />
    <link href="_assets/css/round.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .accordionHeaderSelected
        {
            border: 1px solid #2F4F4F;
            color: white;
            background-color: #5078B3;
            font-family: Tahoma;
            font-size: 12px;
            font-weight: bold;
            padding: 2px;
            margin-top: 1px;
            cursor: pointer;
        }
        .accordionHeader
        {
            border: 1px solid #2F4F4F;
            color: white;
            background-color: #2E4d7B;
            font-family: Tahoma;
            font-size: 12px;
            font-weight: bold;
            padding: 2px;
            margin-top: 1px;
            cursor: pointer;
        }
        .accordionContent
        {
            line-height: 20px;
            padding-bottom: 10px;
        }
    </style>

    <script src="_assets/js/General.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmAboutEOSOFT" runat="server">
    <asp:ScriptManager ID="a11" runat="server">
    </asp:ScriptManager>
    <div class="rounded">
        <div class="top-outer">
            <div class="top-inner">
                <div class="top">
                    <h2 style="text-align: center;">
                        CSC Tool
                    </h2>
                </div>
            </div>
        </div>
        <div class="mid-outer">
            <div class="mid-inner">
                <div class="mid">
                    <cc1:Accordion ID="MyAccordion" runat="Server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                        AutoSize="None" ContentCssClass="accordionContent" FadeTransitions="true" TransitionDuration="250"
                        FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                        <Panes>
                            <cc1:AccordionPane runat="server" ID="ap1" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                BorderColor="Aqua" BackColor="Gray">
                                <Header>
                                    CSC - Customer Solution Centre
                                </Header>
                                <Content>
                                    1)  Receiving the devices from the Customer & send an acknowledgement by posting XMLs 
                                    <br />
                                    2)  Triage of Each Device (Inspection) & posting XMLs for each device status to customer
                                    <br />
                                    3)  Symptoms & Actions of each device by updating the Symptoms & Action against the model received and also the component consumptions and posting the XMLs to customer
                                    <br />
                                    4)  Final Disposition Status of each device and posting the XMLs with the Disposition Status and the replaced serial nos. (if any).
                                    <br />
                                    5)  The CSC tool is developed for Tracking and Integrating the Repair Operations with the Blackberyy eRMA.
                                    <br />
                                </Content>
                            </cc1:AccordionPane>
                            
                        </Panes>
                    </cc1:Accordion>
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
<p></p>
</body>
</html>
