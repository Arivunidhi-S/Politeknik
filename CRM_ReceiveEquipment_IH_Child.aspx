<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CRM_ReceiveEquipment_IH_Child.aspx.cs"
    Inherits="CRM_ReceiveEquipment_IH_Child" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CREATE JOB</title>
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link rel="stylesheet" href="css/menu_core.css" type="text/css" />
    <link rel="stylesheet" href="css/skins/menu_simpleAnimated.css" type="text/css" />
    <link rel="stylesheet" type="text/css" href="StyleSheet/stylesRadPanel.css" />
    <link rel="stylesheet" href="JS/styles.css" />
    <link rel="stylesheet" href="css/MyCSS.css" />
    <!--Custom Styles-->
    <style type="text/css">
        .myTitle
        {
            color: #333;
            font-family: arial;
            font-weight: normal;
            font-size: 10px;
            margin: 20px 0px 5px 0px;
        }
        .myTitleTop
        {
            margin: 5px 0px;
        }
        
        .validator
        {
            font-size: 15px;
            padding-left: 3px;
            vertical-align: middle;
        }
        .textboxstyle_1
        {
            font-family: Times New Roman;
            font-size: 1;
            color: Black;
            height: 16px;
            font-weight: normal;
        }
        
        .textboxstyle_new
        {
            border-right: #000000 1px solid;
            border-top: #000000 1px solid;
            font-size: 11px;
            border-left: #000000 1px solid;
            color: #000000;
            border-bottom: #000000 1px solid;
            font-family: Verdana;
            text-decoration: none;
        }
        
        .buttonstyle
        {
            font-family: Times New Roman;
            font-size: Small;
            color: Black;
            width: 80px;
        }
        .labelstyle
        {
            font-family: verdana;
            font-size: 12px;
            color: Black;
            height: 16px;
            font-weight: normal;
            background-attachment: inherit;
        }
        .labelstyle_1
        {
            font-family: verdana;
            font-size: 12px;
            color: Black;
            height: 16px;
            font-weight: normal;
            background-attachment: inherit;
        }
        .fancy-green .ajax__tab_header
        {
            background: url(images/green_bg_Tab.gif) repeat-x;
            cursor: pointer;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer
        {
            background: url(images/green_left_Tab.gif) no-repeat left top;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner
        {
            background: url(images/green_right_Tab.gif) no-repeat right top;
        }
        .fancy .ajax__tab_header
        {
            font-size: 13px;
            font-weight: bold;
            color: #000;
            font-family: sans-serif;
        }
        .fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer
        {
            height: 46px;
        }
        .fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner
        {
            height: 46px;
            margin-left: 16px; /* offset the width of the left image */
        }
        .fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab
        {
            margin: 16px 16px 0px 0px;
        }
        .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab
        {
            color: #fff;
        }
        .fancy .ajax__tab_body
        {
            font-family: Arial;
            font-size: 10pt;
            border-top: 0;
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
        }
    </style>
    <script language="javascript">
        history.go(1); /* undo user navigation (ex: IE Back Button) */
    </script>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }
            function CloseDialog(button) {
                //GetRadWindow().BrowserWindow.refreshGrid(args);

                GetRadWindow().close();
                //__doPostBack(button.name, "");
                //            top.location.href = top.location.href;
            }
            function CloseAndRebind(args) {
                GetRadWindow().BrowserWindow.refreshGrid(args);
                GetRadWindow().close();
            }
            function CancelEdit() {
                GetRadWindow().close();
            }
        </script>
    </telerik:RadCodeBlock>
    <script type="text/javascript">
        var uploadedFilesCount = 0;
        var isEditMode;

        function validateRadUpload(source, e) {
            // When the RadGrid is in Edit mode the user is not obliged to upload file.
            if (isEditMode == null || isEditMode == undefined) {
                e.IsValid = false;

                if (uploadedFilesCount > 0) {
                    e.IsValid = true;
                }
            }
            isEditMode = null;
        }

        function OnClientFileUploaded(sender, eventArgs) {
            uploadedFilesCount++;
        }

    </script>
</head>
<body style="background-image: url('Images/Bg.jpg'); margin: 5px 0px 0px 0px;">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td style="border-right: blue thin solid; border-top: blue thin solid; border-left: blue thin solid;
                    border-bottom: blue thin solid; border-width: 0px" align="center">
                    <table border="0" width="1070px" cellspacing="0">
                        <tr>
                            <td style="width: 95%;" align="center">
                                <div style="height: 22px; text-transform: capitalize; text-align: center; margin-top: 2px;">
                                    <asp:Label class="labelstyle" ID="lblStatus" runat="server" Text="" ForeColor="Red"
                                        Visible="true" Font-Size="Larger" Font-Bold="true" Font-Names="Arial" />
                                </div>
                            </td>
                            <td style="width: 5%; text-align: right">
                                <asp:Button ID="btnSave_Close" runat="server" Text="X" OnClick="btnSave_Close_Click"
                                    Font-Size="Larger" ForeColor="White" BackColor="#e64d1a" Visible="false"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;" colspan="2">
                                <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                                    <Scripts>
                                        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                                        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                                        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
                                    </Scripts>
                                </telerik:RadScriptManager>
                                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                                    <AjaxSettings>
                                        <telerik:AjaxSetting AjaxControlID="RadGridJob">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="RadGridJob" />
                                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                <telerik:AjaxUpdatedControl ControlID="txtJob" />
                                                <telerik:AjaxUpdatedControl ControlID="RadInputManager1" />
                                                <telerik:AjaxUpdatedControl ControlID="btnAdd" />
                                                <telerik:AjaxUpdatedControl ControlID="txtAcessories" />
                                                <telerik:AjaxUpdatedControl ControlID="txtAccesSeriel" />
                                                <telerik:AjaxUpdatedControl ControlID="txtECDDate" />
                                                <telerik:AjaxUpdatedControl ControlID="listAccessories" />
                                                <telerik:AjaxUpdatedControl ControlID="txtBillingAdd" />
                                                <telerik:AjaxUpdatedControl ControlID="txtCertAddress" />
                                                <telerik:AjaxUpdatedControl ControlID="txtDeliveryAdd" />
                                                <telerik:AjaxUpdatedControl ControlID="linkDownload" />
                                                <telerik:AjaxUpdatedControl ControlID="chkLink" />
                                                <telerik:AjaxUpdatedControl ControlID="RadAsyncUpload1" />
                                                <telerik:AjaxUpdatedControl ControlID="chkDiscripency" />
                                                <telerik:AjaxUpdatedControl ControlID="lblDiscrepancy" />
                                                <telerik:AjaxUpdatedControl ControlID="txtSerielNo" />
                                                <telerik:AjaxUpdatedControl ControlID="txtRemarks" />
                                                <telerik:AjaxUpdatedControl ControlID="cboIntervalNo" />
                                                <telerik:AjaxUpdatedControl ControlID="cboIntervalTime" />
                                                <telerik:AjaxUpdatedControl ControlID="txtphysical" />
                                                <telerik:AjaxUpdatedControl ControlID="txtfuctional" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                        <telerik:AjaxSetting AjaxControlID="chkDiscripency">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="chkDiscripency" />
                                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                <telerik:AjaxUpdatedControl ControlID="txtRemarks" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                        <telerik:AjaxSetting AjaxControlID="chkLink">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="cboContractNo" LoadingPanelID="RadAjaxLoadingPanel1" />
                                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                <telerik:AjaxUpdatedControl ControlID="cboQuotationNo" />
                                                <telerik:AjaxUpdatedControl ControlID="RadAsyncUpload1" />
                                                <telerik:AjaxUpdatedControl ControlID="RadGridJob" />
                                                <telerik:AjaxUpdatedControl ControlID="cboContractNo" />
                                                <telerik:AjaxUpdatedControl ControlID="linkDownload" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                        <telerik:AjaxSetting AjaxControlID="btnAdd">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="btnAdd" />
                                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                <telerik:AjaxUpdatedControl ControlID="listAccessories" />
                                                <telerik:AjaxUpdatedControl ControlID="linkDownload" />
                                                <telerik:AjaxUpdatedControl ControlID="RadAsyncUpload1" />
                                                <telerik:AjaxUpdatedControl ControlID="chkLink" />
                                                 <telerik:AjaxUpdatedControl ControlID="txtphysical" />
                                                <telerik:AjaxUpdatedControl ControlID="txtfuctional" />
                                                <telerik:AjaxUpdatedControl ControlID="txtSerielNo" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                        <telerik:AjaxSetting AjaxControlID="btnSave">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="btnSave" />
                                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                 <telerik:AjaxUpdatedControl ControlID="txtphysical" />
                                                <telerik:AjaxUpdatedControl ControlID="txtfuctional" />
                                                <telerik:AjaxUpdatedControl ControlID="txtSerielNo" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                        <telerik:AjaxSetting AjaxControlID="btnSave_Close">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="btnSave" />
                                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                        <telerik:AjaxSetting AjaxControlID="btnGenerate">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="txtSerielNo" />
                                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                 <telerik:AjaxUpdatedControl ControlID="txtphysical" />
                                                <telerik:AjaxUpdatedControl ControlID="txtfuctional" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                    </AjaxSettings>
                                </telerik:RadAjaxManager>
                                <div id="DivHeader" runat="server" class="otto">
                                    CREATE JOB, UPDATE SERIAL AND ACCESSORIES DETAILS
                                </div>
                                <%--<hr style="height: 0.1px; border-color: transparent;">--%>
                                <div style="height: 65px; background: -webkit-linear-gradient(#dbf6e3,Gray); border: 4px inset #d4d4d4;">
                                    <table border="1" cellspacing="3" cellpadding="3" style="width: 1070px;">
                                        <tr>
                                            <td align="left" style="width: 15%">
                                                <asp:Label runat="server" CssClass="labelstyle_1" ID="lblContQuote1" Text=": " />
                                            </td>
                                            <td align="left" style="width: 20%">
                                                <asp:TextBox runat="server" CssClass="labelstyle_1" Enabled="false" BackColor="Transparent"
                                                    ID="lblContQuote2" BorderStyle="None" />
                                            </td>
                                            <td align="left" style="width: 15%">
                                                <asp:Label runat="server" CssClass="labelstyle_1" Text="Customer : " />
                                            </td>
                                            <td align="left" style="width: 25%">
                                                <asp:TextBox runat="server" Width="280px" CssClass="labelstyle_1" BackColor="Transparent"
                                                    Enabled="false" ID="lblCust" BorderStyle="None" />
                                            </td>
                                            <td align="left" style="width: 10%">
                                                <asp:Label runat="server" CssClass="labelstyle_1" ID="lblEquip_ID" Visible="false" />
                                                <asp:Label runat="server" CssClass="labelstyle_1" ID="lblCustID" Visible="false" />
                                                <asp:Label runat="server" CssClass="labelstyle_1" ID="lblContQuoteID" Visible="false" />
                                                <asp:Label runat="server" CssClass="labelstyle_1" ID="lblContQuoteDetailId" Visible="false" />
                                                <asp:Label runat="server" CssClass="labelstyle_1" ID="lblPartialFull" Visible="false" />
                                                <asp:Label runat="server" CssClass="labelstyle_1" ID="lblQuantity_SerielNo" Visible="true"
                                                    Text="Quantity" />
                                            </td>
                                            <td align="left" style="width: 15%">
                                                <asp:TextBox runat="server" Width="100px" CssClass="labelstyle_1" Enabled="false"
                                                    ID="txtQuantity" BackColor="Transparent" BorderStyle="None" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label runat="server" CssClass="labelstyle_1" ID="Label3" Text="Equip. Name: " />
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" Width="280px" CssClass="labelstyle_1" Enabled="false"
                                                    ID="lblEquipment" BackColor="Transparent" BorderStyle="None" />
                                            </td>
                                            <td align="left">
                                                <asp:Label runat="server" CssClass="labelstyle_1" ID="Label2" Text="Model/Maker : "
                                                    Visible="true" />
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" CssClass="labelstyle_1" BackColor="Transparent" Enabled="false"
                                                    BorderStyle="None" ID="txtModelMaker" Width="280px" />
                                            </td>
                                            <td align="left">
                                                <asp:Label runat="server" CssClass="labelstyle_1" ID="Label8" Text="Equip. No: " />
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" CssClass="labelstyle_1" BackColor="Transparent" BorderStyle="None"
                                                    Enabled="false" ID="lblEquipmentNo" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellspacing="1" cellpadding="1" style="width: 1060px;" border="0">
                                        <tr>
                                            <td width="10%">
                                                <asp:Label ID="lbllab1" runat="server" Visible="false"></asp:Label>
                                                <asp:CheckBox ID="ChkLab1" runat="server" Font-Size="11px" AutoPostBack="true" BorderColor="Salmon"
                                                    Visible="false" />
                                            </td>
                                            <td width="10%">
                                                <asp:Label ID="lbllab2" runat="server" Visible="false"></asp:Label>
                                                <asp:CheckBox ID="ChkLab2" runat="server" Font-Size="11px" AutoPostBack="true" BorderColor="Salmon"
                                                    Visible="false" />
                                            </td>
                                            <td width="10%">
                                                <asp:Label ID="lbllab3" runat="server" Visible="false"></asp:Label>
                                                <asp:CheckBox ID="ChkLab3" runat="server" Font-Size="11px" AutoPostBack="true" BorderColor="Salmon"
                                                    Visible="false" />
                                            </td>
                                            <td width="10%">
                                                <asp:Label ID="lbllab4" runat="server" Visible="false"></asp:Label>
                                                <asp:CheckBox ID="ChkLab4" runat="server" Font-Size="11px" AutoPostBack="true" BorderColor="Salmon"
                                                    Visible="false" />
                                            </td>
                                            <td width="10%">
                                                <asp:Label ID="lbllab5" runat="server" Visible="false"></asp:Label>
                                                <asp:CheckBox ID="ChkLab5" runat="server" Font-Size="11px" AutoPostBack="true" BorderColor="Salmon"
                                                    Visible="false" />
                                            </td>
                                            <td width="10%">
                                                <asp:Label ID="lbllab6" runat="server" Visible="false"></asp:Label>
                                                <asp:CheckBox ID="ChkLab6" runat="server" Font-Size="11px" AutoPostBack="true" BorderColor="Salmon"
                                                    Visible="false" />
                                            </td>
                                            <td width="10%">
                                                <asp:Label ID="lbllab7" runat="server" Visible="false"></asp:Label>
                                                <asp:CheckBox ID="ChkLab7" runat="server" Font-Size="11px" AutoPostBack="true" BorderColor="Salmon"
                                                    Visible="false" />
                                            </td>
                                            <td width="10%">
                                                <asp:Label ID="lbllab8" runat="server" Visible="false"></asp:Label>
                                                <asp:CheckBox ID="ChkLab8" runat="server" Font-Size="11px" AutoPostBack="true" BorderColor="Salmon"
                                                    Visible="false" />
                                            </td>
                                            <td width="10%">
                                                <asp:Label ID="lbllab9" runat="server" Visible="false"></asp:Label>
                                                <asp:CheckBox ID="ChkLab9" runat="server" Font-Size="11px" AutoPostBack="true" BorderColor="Salmon"
                                                    Visible="false" />
                                            </td>
                                            <td width="10%">
                                                <asp:Label ID="lbllab10" runat="server" Visible="false"></asp:Label>
                                                <asp:CheckBox ID="ChkLab10" runat="server" Font-Size="11px" AutoPostBack="true" BorderColor="Salmon"
                                                    Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;" colspan="2">
                                <table border="1" cellspacing="0" cellpadding="0" style="width: 1070px;">
                                    <tr>
                                        <td colspan="2">
                                            <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; background-color:GrayText; background-image: url(Images/main.jpg);">
                                                <tr>
                                                    <td style="width: 20%; text-align: center; background-color: GrayText">
                                                        <asp:Label runat="server" ID="lblJobList" Text="JOB LIST" Font-Size="medium" BorderStyle="None"
                                                            Visible="true" ForeColor="Wheat" />
                                                        <telerik:RadTextBox ID="txtQuantityReceived" Text="" Width="40px" EmptyMessage="Received Qty"
                                                            Visible="false" runat="server" />
                                                        <asp:Label runat="server" ID="lblQuantityReceived" Text="" Font-Size="medium" BorderStyle="None"
                                                            Visible="false" ForeColor="Wheat" />
                                                        <asp:Button ID="btnReceivedQty" runat="server" Text="CreateJob" Visible="false" OnClick="btnReceivedQty_OnClick" />
                                                    </td>
                                                    <asp:Label ID="Label11" Text="Job No. : " Font-Size="13px" runat="server" Visible="false" />
                                                    <td style="width: 15%; text-align: left">
                                                        <telerik:RadTextBox ID="txtJob" Text="" Width="150px" Height="20px" Font-Bold="true"
                                                            EmptyMessage="Choose Job No" Enabled="false" EmptyMessageStyle-Font-Italic="true"
                                                            EmptyMessageStyle-ForeColor="YellowGreen" Font-Size="13px" BorderStyle="None"
                                                            BackColor="#fceed1" runat="server" />
                                                    </td>
                                                    <td style="width: 15%; text-align: right">
                                                        <asp:Button ID="btnGenerate" Text="GenerateID" OnClick="btnGenerate_OnClick" runat="server" />
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtSerielNo" Text="" EmptyMessage="Enter Serial No/ID No"
                                                            EmptyMessageStyle-Font-Italic="true" Width="150px" EmptyMessageStyle-ForeColor="Blue"
                                                            Font-Size="13px" runat="server" />
                                                    </td>
                                                    <asp:Label ID="Label5" Text="Seriel No/ID No" Font-Size="13px" runat="server" Visible="false" />
                                                    <td style="width: 4%; text-align: right">
                                                        <asp:CheckBox ID="chkDiscripency" runat="server" Enabled="false" OnCheckedChanged="chkDiscripency_OnCheckedChanged"
                                                            AutoPostBack="true" TextAlign="Right" ForeColor="Black" Visible="false" />
                                                    </td>
                                                    <td style="width: 8%; text-align: left">
                                                        <asp:Label ID="lblDiscrepancy" runat="server" Text="Discrepancy" Font-Size="13px"
                                                            Visible="false" />
                                                            <telerik:RadTextBox ID="txtphysical" Text="" EmptyMessage="Enter Physical Condition"
                                                            EmptyMessageStyle-Font-Italic="true" Width="150px" EmptyMessageStyle-ForeColor="Blue"
                                                            Font-Size="13px" runat="server" Visible="false" />
                                                    </td>
                                                    <td style="width: 32%; text-align: left">
                                                        <telerik:RadTextBox runat="server" CssClass="textboxstyle_new" Enabled="true" Height="26px"
                                                            ID="txtRemarks" TextMode="MultiLine" Width="280px" Visible="false" EmptyMessageStyle-ForeColor="Blue"
                                                            EmptyMessageStyle-Font-Italic="true" EmptyMessage="Enter remarks if any discrepency"
                                                            MaxLength="300" />
                                                             <telerik:RadTextBox ID="txtfuctional" Text="" EmptyMessage="Enter Functional Condition"
                                                            EmptyMessageStyle-Font-Italic="true" Width="150px" EmptyMessageStyle-ForeColor="Blue"
                                                            Font-Size="13px" runat="server" Visible="false"/>
                                                    </td>
                                                    <td style="width: 6%; text-align: right">
                                                        <asp:Button Text="Save" ID="btnSave" OnClick="btnSave_Onclick" Font-Bold="true" runat="server" />
                                                    </td>
                                                     <td style="width: 6%; text-align: right">
                                                        <asp:Button Text="Report" ID="Button1"  Font-Bold="true" runat="server" OnClick="btnReport_OnClick" Visible="true" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;">
                                            <div style="background: -webkit-linear-gradient(#dbf6e3,Gray); border: 4px inset #d4d4d4;">
                                                <div id="Div1" runat="server" style="width: 210px; height: 270px; background-color: transparent;
                                                    margin-top: 0px; overflow: auto; background-image: url(Images/main.jpg);">
                                                    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                                                    <telerik:RadGrid ID="RadGridJob" runat="server" GridLines="None" ShowGroupPanel="false"
                                                        Skin="Hay" AllowPaging="false" OnNeedDataSource="RadGridJob_NeedDataSource" PagerStyle-AlwaysVisible="false"
                                                        PagerStyle-Position="Bottom" PagerStyle-Mode="NextPrevNumericAndAdvanced" AllowSorting="true"
                                                        AllowFilteringByColumn="true" OnItemCommand="RadGridJob_ItemCommand" OnItemDataBound="RadGridJob_OnItemDataBound"
                                                        PageSize="1000" ShowHeader="false">
                                                        <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
                                                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="RECEIVED_TRANS_DETAIL_ID"
                                                            CommandItemDisplay="None">
                                                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                            <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                                            <Columns>
                                                                <telerik:GridDragDropColumn HeaderStyle-Width="18px" Visible="false" />
                                                                <telerik:GridBoundColumn DataField="RECEIVED_TRANS_DETAIL_ID" DataType="System.Int64"
                                                                    HeaderText="ID" ReadOnly="True" SortExpression="RECEIVED_TRANS_DETAIL_ID" UniqueName="RECEIVED_TRANS_DETAIL_ID"
                                                                    Visible="false" AllowSorting="false" AllowFiltering="false">
                                                                    <HeaderStyle Width="20px" ForeColor="Silver" />
                                                                    <ItemStyle ForeColor="Silver" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="EQUIPMENT_ID" DataType="System.Int64" HeaderText="ID"
                                                                    ReadOnly="True" SortExpression="EQUIPMENT_ID" UniqueName="EQUIPMENT_ID" Visible="false"
                                                                    AllowSorting="false" AllowFiltering="false">
                                                                    <HeaderStyle Width="20px" ForeColor="Silver" />
                                                                    <ItemStyle ForeColor="Silver" />
                                                                </telerik:GridBoundColumn>
                                                                <%--<telerik:GridTemplateColumn HeaderText="Seriel No" AllowFiltering="false">
                                                    <HeaderStyle Width="100%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <telerik:RadTextBox runat="server" ID="txtSerielNo" BorderStyle="None" EmptyMessage="--Seriel No--"
                                                            HoveredStyle-Font-Italic="true" HoveredStyle-ForeColor="Blue" Font-Bold="true"
                                                            ForeColor="Red" Width="140px" Font-Size="10px" Height="20px" Font-Names="Verdana"
                                                            ToolTip='<%# Eval("RECEIVED_TRANS_DETAIL_ID") %>' />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>--%>
                                                                <telerik:GridTemplateColumn HeaderText="Job Number" AllowFiltering="false">
                                                                    <HeaderStyle Width="100%" HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" Height="25px" />
                                                                    <ItemTemplate>
                                                                        <telerik:RadButton runat="server" ID="btnJOBNO" ButtonType="LinkButton" BorderStyle="Solid"
                                                                            Font-Bold="true" ForeColor="Wheat" Width="180px" Font-Size="Medium" Height="25px"
                                                                            Font-Names="Times New Roman" Text='<%# Eval("JOBNO") %>' ToolTip='<%# Eval("RECEIVED_TRANS_DETAIL_ID") %>'
                                                                            BackColor="GrayText" BorderColor="Green" CommandName="RowClick" CommandArgument='<%# Eval("RECEIVED_TRANS_DETAIL_ID") %>' />
                                                                        <asp:Label ID="lblRECEIVED_TRANS_ID" Text='<%# Eval("RECEIVED_TRANS_ID") %>' runat="server"
                                                                            Visible="false" />
                                                                        <asp:Label ID="lblSerielNo" Text='<%# Eval("SERIEL_NO") %>' runat="server" Visible="false" />
                                                                        <asp:Label ID="lblRunningNo" Text='<%# Eval("RunningNo") %>' runat="server" Visible="false" />
                                                                        <asp:Label ID="lblRemarks" Text='<%# Eval("REMARKS") %>' runat="server" Visible="false" />
                                                                        <asp:Label ID="lblDiscre" Text='<%# Eval("DISCRIPENCY") %>' runat="server" Visible="false" />
                                                                        <%-- <asp:Label ID="lblphysial" Text='<%# Eval("physical") %>' runat="server" Visible="false" />
                                                                          <asp:Label ID="lblfuntional" Text='<%# Eval("functional") %>' runat="server" Visible="false" />--%>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </div>
                                            </div>
                                        </td>
                                        <td style="width: 80%;">
                                            <div style="background: -webkit-linear-gradient(#dbf6e3,#f2e9b0); border: 4px inset #d4d4d4;">
                                                <div id="Div2" runat="server" style="width: 860px; height: 270px; overflow: auto;
                                                    text-align: left; background-image: url(Images/main.jpg);">
                                                    <ajaxToolkit:TabContainer runat="server" ID="TabContainer1" Height="0px" ActiveTabIndex="0"
                                                        CssClass="fancy fancy-green" BackColor="Transparent">
                                                        <ajaxToolkit:TabPanel BackColor="Transparent" runat="server" ID="TabPanel1" HeaderText="Accessories">
                                                            <ContentTemplate>
                                                                <div style="background: -webkit-linear-gradient(#dbf6e3,Gray); border: 4px solid Black;">
                                                                    <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; height: 180px">
                                                                        <tr>
                                                                            <td style="text-align: left" colspan="3">
                                                                                <div id="Div3" runat="server" style="width: 300px; height: 180px; overflow: auto;
                                                                                    text-align: left; background-image: url(Images/main.jpg);">
                                                                                    <table border="0" cellspacing="1" cellpadding="1">
                                                                                        <tr>
                                                                                            <td style="width: 50%">
                                                                                                <asp:Label ID="Label7" Text="Accessories : " class="labelstyle" Font-Size="12px"
                                                                                                    runat="server" />
                                                                                            </td>
                                                                                            <td style="width: 50%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="2">
                                                                                                <asp:TextBox ID="txtAcessories" Text="" Width="280px" Height="40px" CssClass="textboxstyle_new"
                                                                                                    Font-Size="12px" TextMode="MultiLine" runat="server" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="2">
                                                                                                <asp:Label ID="Label6" Text="Seriel No : " class="labelstyle" runat="server" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="2">
                                                                                                <asp:TextBox ID="txtAccesSeriel" Text="" Width="180px" Height="30px" CssClass="textboxstyle_new"
                                                                                                    Font-Size="12px" runat="server" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="2">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="2">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Button ID="btnAdd" Text="Add" OnClick="btnAdd_OnClick" runat="server" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Button ID="btnDel" Text="Delete" OnClick="btnDelete_OnClick" runat="server" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                            <td style="text-align: left" colspan="3">
                                                                                <div id="Div4" runat="server" style="width: 450px; overflow: auto; text-align: left;
                                                                                    background-image: url(Images/main.jpg);">
                                                                                    <telerik:RadListBox ID="listAccessories" runat="server" Width="450px" Height="200px"
                                                                                        AutoPostBack="true" Font-Italic="true" CheckBoxes="false" AllowTransfer="false"
                                                                                        ButtonSettings-ShowTransferAll="false" AutoPostBackOnTransfer="true" AllowDelete="false"
                                                                                        AllowReorder="true" AutoPostBackOnReorder="true" EnableDragAndDrop="false">
                                                                                    </telerik:RadListBox>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </ContentTemplate>
                                                        </ajaxToolkit:TabPanel>
                                                        <ajaxToolkit:TabPanel BackColor="Transparent" runat="server" ID="TabPanel2" HeaderText="Step 1: Register Address"
                                                            Visible="true" Enabled="false">
                                                            <ContentTemplate>
                                                                <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; background-image: url(Images/main.jpg);">
                                                                    <tr>
                                                                        <td colspan="3">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 33%; text-align: left">
                                                                            <asp:Label ID="Label9" Text="Contact Person :" class="labelstyle" ForeColor="#676767"
                                                                                runat="server" />
                                                                            <asp:TextBox Text="" ID="txtBilContactName" Height="16px" Width="250px" Font-Size="12px"
                                                                                CssClass="textboxstyle_new" runat="server" />
                                                                            <asp:Label ID="Label10" Text="Contact No :" class="labelstyle" ForeColor="#676767"
                                                                                runat="server" />
                                                                            <asp:TextBox Text="" ID="txtBillContactNo" Height="16px" Width="250px" Font-Size="12px"
                                                                                CssClass="textboxstyle_new" runat="server" />
                                                                            <asp:Label ID="Label18" Text="Email Id :" class="labelstyle" ForeColor="#676767"
                                                                                runat="server" />
                                                                            <asp:TextBox Text="" ID="txtBillContactMail" Height="16px" Width="250px" Font-Size="12px"
                                                                                CssClass="textboxstyle_new" runat="server" />
                                                                            <asp:Label ID="Label1" Text="Billing Address :" class="labelstyle" ForeColor="#676767"
                                                                                runat="server" />
                                                                            <asp:TextBox Text="" ID="txtBillingAdd" TextMode="MultiLine" Height="80px" Width="250px"
                                                                                Font-Size="12px" CssClass="textboxstyle_new" runat="server" />
                                                                        </td>
                                                                        <td style="width: 33%; text-align: left">
                                                                            <asp:Label Text="Contact Person :" class="labelstyle" ForeColor="#676767" runat="server" />
                                                                            <asp:TextBox Text="" ID="txtDelContactName" Height="16px" Width="250px" Font-Size="12px"
                                                                                CssClass="textboxstyle_new" runat="server" />
                                                                            <asp:Label Text="Contact No :" class="labelstyle" ForeColor="#676767" runat="server" />
                                                                            <asp:TextBox Text="" ID="txtDelContactNo" Height="16px" Width="250px" Font-Size="12px"
                                                                                CssClass="textboxstyle_new" runat="server" />
                                                                            <asp:Label ID="Label19" Text="Email Id :" class="labelstyle" ForeColor="#676767"
                                                                                runat="server" />
                                                                            <asp:TextBox Text="" ID="txtDelContactMail" Height="16px" Width="250px" Font-Size="12px"
                                                                                CssClass="textboxstyle_new" runat="server" />
                                                                            <asp:Label ID="Label12" class="labelstyle" Text="Delivery Address" ForeColor="#676767"
                                                                                runat="server" />
                                                                            <asp:TextBox ID="txtDeliveryAdd" Text="" TextMode="MultiLine" Width="250px" Height="80px"
                                                                                Font-Size="12px" CssClass="textboxstyle_new" runat="server" />
                                                                        </td>
                                                                        <td style="width: 33%; text-align: left">
                                                                            <asp:Label ID="Label16" Text="Contact Person" class="labelstyle" ForeColor="#676767"
                                                                                runat="server" />
                                                                            <asp:TextBox Text="" ID="txtCertContactName" Height="16px" Width="250px" Font-Size="12px"
                                                                                CssClass="textboxstyle_new" runat="server" />
                                                                            <asp:Label ID="Label17" Text="Contact No" class="labelstyle" ForeColor="#676767"
                                                                                runat="server" />
                                                                            <asp:TextBox Text="" ID="txtCertContactNo" Height="16px" Width="250px" Font-Size="12px"
                                                                                CssClass="textboxstyle_new" runat="server" />
                                                                            <asp:Label ID="Label20" Text="Email Id :" class="labelstyle" ForeColor="#676767"
                                                                                runat="server" />
                                                                            <asp:TextBox Text="" ID="txtCertContactMail" Height="16px" Width="250px" Font-Size="12px"
                                                                                CssClass="textboxstyle_new" runat="server" />
                                                                            <asp:Label ID="Label13" class="labelstyle" Text="Certificate Address" ForeColor="#676767"
                                                                                runat="server" />
                                                                            <asp:TextBox ID="txtCertAddress" Text="" TextMode="MultiLine" Width="250px" Height="80px"
                                                                                CssClass="textboxstyle_new" Font-Size="12px" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3">
                                                                            <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; background-image: url(Images/main.jpg);">
                                                                                <tr>
                                                                                    <td style="width: 8%; text-align: left">
                                                                                        <asp:Label ID="Label4" Text="EC Date:" runat="server" class="labelstyle" />
                                                                                    </td>
                                                                                    <td style="width: 12%; text-align: left">
                                                                                        <telerik:RadDatePicker Width="110px" ID="txtECDDate" Enabled="true" runat="server"
                                                                                            DateInput-EmptyMessage="Click Here" HoveredStyle-Font-Italic="true" DateInput-DateFormat="dd/MMM/yyyy"
                                                                                            HoveredStyle-ForeColor="#acd291" />
                                                                                    </td>
                                                                                    <td style="width: 10%; text-align: right">
                                                                                        <asp:Label ID="Label21" Text="Interval:" runat="server" Visible="true" class="labelstyle" />
                                                                                    </td>
                                                                                    <td style="width: 16%; text-align: left">
                                                                                        <telerik:RadComboBox ID="cboIntervalNo" runat="server" AppendDataBoundItems="True"
                                                                                            Width="60px">
                                                                                            <Items>
                                                                                                <telerik:RadComboBoxItem Text="1" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="--Select--" ForeColor="Silver" />
                                                                                                <telerik:RadComboBoxItem Text="0" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="2" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="3" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="4" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="5" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="6" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="7" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="8" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="9" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="10" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="11" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="12" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="13" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="14" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="15" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="16" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="17" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="18" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="19" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="20" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="21" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="22" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="23" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="24" ForeColor="Black" />
                                                                                            </Items>
                                                                                        </telerik:RadComboBox>
                                                                                        <telerik:RadComboBox ID="cboIntervalTime" runat="server" AppendDataBoundItems="True"
                                                                                            Width="70px">
                                                                                            <Items>
                                                                                                <telerik:RadComboBoxItem Text="Month" ForeColor="Black" />
                                                                                                <telerik:RadComboBoxItem Text="--Select--" ForeColor="Silver" />
                                                                                                <telerik:RadComboBoxItem Text="Year" ForeColor="Black" />
                                                                                            </Items>
                                                                                        </telerik:RadComboBox>
                                                                                    </td>
                                                                                    <td style="width: 12%; text-align: right">
                                                                                        <asp:Label ID="Label14" Text="Upload SN:" Font-Size="Small" runat="server" Style="color: Black" />
                                                                                    </td>
                                                                                    <td style="width: 4%; text-align: left">
                                                                                        <asp:CheckBox ID="chkLink" Text="" Checked="false" Visible="false" AutoPostBack="true"
                                                                                            OnCheckedChanged="chkLink_OnCheckedChanged" runat="server" />
                                                                                    </td>
                                                                                    <td style="width: 28%; text-align: left">
                                                                                        <asp:LinkButton ID="linkDownload" Font-Size="13px" runat="server" OnClick="linkDownload_OnClick"
                                                                                            Text="Download" Visible="false" />
                                                                                        <telerik:RadAsyncUpload runat="server" ID="RadAsyncUpload1" Enabled="true" AllowedFileExtensions="pdf,doc,docx"
                                                                                            ToolTip="Max : 2MB Size/Pic" MaxFileInputsCount="1" Visible="false" MaxFileSize="2097152"
                                                                                            AutoAddFileInputs="true" OnFileUploaded="RadAsyncUpload1_FileUploaded" ViewStateMode="Enabled"
                                                                                            Width="200px">
                                                                                        </telerik:RadAsyncUpload>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxToolkit:TabPanel>
                                                    </ajaxToolkit:TabContainer>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
