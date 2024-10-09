<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CRM_ReceiveEquipment_IH.aspx.cs"
    Inherits="CRM_ReceiveEquipment_IH" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <title>Registration Form</title>
    <link rel="shortcut icon" href="images/Calib.png" />
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link rel="stylesheet" href="JS/styles.css" />
    <link rel="stylesheet" href="css/MyCSS.css" />
    <script src="JS/jquery-latest.min.js" type="text/javascript"></script>
    <style type="text/css">
        .search
        {
            background: url(images/search.png) no-repeat;
            background-position: center;
        }
        body
        {
            background-image: url(images/Bg.jpg); /*You will specify your image path here.*/
            -moz-background-size: cover;
            -webkit-background-size: cover;
            background-size: cover;
            background-position: top center !important;
            background-repeat: no-repeat !important;
            background-attachment: fixed;
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
    <script type="text/javascript">
      
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="gridLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                    <telerik:AjaxUpdatedControl ControlID="cboContractNo" />
                    <telerik:AjaxUpdatedControl ControlID="cboQuotationNo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadAsyncUpload1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadAsyncUpload1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboContractNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                    <telerik:AjaxUpdatedControl ControlID="cboContractNo" />
                    <telerik:AjaxUpdatedControl ControlID="cboQuotationNo" />
                    <telerik:AjaxUpdatedControl ControlID="linkDownload" />
                    <telerik:AjaxUpdatedControl ControlID="lblPO" />
                    <telerik:AjaxUpdatedControl ControlID="btnReportQuotaion" />
                    <telerik:AjaxUpdatedControl ControlID="cboPO" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboQuotationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                    <telerik:AjaxUpdatedControl ControlID="cboContractNo" />
                    <telerik:AjaxUpdatedControl ControlID="cboQuotationNo" />
                    <telerik:AjaxUpdatedControl ControlID="linkDownload" />
                    <telerik:AjaxUpdatedControl ControlID="lblPO" />
                    <telerik:AjaxUpdatedControl ControlID="btnReportQuotaion" />
                    <telerik:AjaxUpdatedControl ControlID="txtRemarks" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function ShowEditForm(id, CustID, status, contequipid, contequip, customer, strContQuoteDetailID, strCalibType_SerielNo, strPartialFull, strContractFlag) {
                window.radopen("CRM_ReceiveEquipment_IH_Child.aspx?Equip_ID=" + id + "&CustID=" + CustID + "&Status=" + status + "&ContEquipID=" + contequipid + "&contquip=" + contequip + "&Customer=" + customer + "&ContratQuotationDetailID=" + strContQuoteDetailID + "&CalibType_SerielNo=" + strCalibType_SerielNo + "&PartialFull=" + strPartialFull + "&ContractFlag=" + strContractFlag, "UserListDialog");
                return false;
            }
            //            function RowDblClick(sender, eventArgs) {
            //                var _Status = eventArgs.get_item().get_cell("status").innerHTML;
            //                window.radopen("CRM_ReceiveEquipment_IH_Child.aspx?ContratQuotationDetailID=" + eventArgs.getDataKeyValue("ContratQuotationDetailID") + "&Status=" + _Status, "UserListDialog");
            //            }
            function refreshGrid(arg) {
                //                if (!arg) {
                //                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
                //                }
                //                else {
                //                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("RebindAndNavigate");
                //                }
                var masterTable = $find("<%=RadGrid1.ClientID%>").get_masterTableView();
                masterTable.rebind();
            }
            function OnClientPageLoad(sender, eventArgs) {
                setTimeout(function () { sender.set_status(""); }, 0);
            }
        </script>
    </telerik:RadCodeBlock>
    <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td style="border-right: blue thin solid; border-top: blue thin solid; border-left: blue thin solid;
                border-bottom: blue thin solid; border-width: 0px" align="center">
                <table border="0" width="1250px">
                    <tr>
                        <td id="Td1" align="left" runat="server" colspan="2">
                            <div id='cssmenu'>
                                <ul>
                                    <% for (int a = 0; a < dtMenuItems.Rows.Count; a++)
                                       { %>
                                    <li class="active has-sub"><a href="#"><span>
                                        <%= dtMenuItems.Rows[a][0].ToString()  %></span></a>
                                        <ul>
                                            <% dtSubMenuItems = BusinessTier.getSubMenuItems(dtMenuItems.Rows[a][0].ToString(), Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                                               int aa;

                                               for (aa = 0; aa < dtSubMenuItems.Rows.Count; aa++)
                                               { %>
                                            <li class="has-sub"><a id='<%= dtSubMenuItems.Rows[aa][0].ToString() %>' href='<%= dtSubMenuItems.Rows[aa][1].ToString() %>'>
                                                <span>
                                                    <%= dtSubMenuItems.Rows[aa][2].ToString()%>
                                                </span></a></li>
                                            <% } %>
                                        </ul>
                                    </li>
                                    <% } %>
                                    <li class="last"><a href="Login.aspx" style="border-right-width: 1px;">LOGOUT</a></li>
                                    <div style="text-align: right; vertical-align: middle; font-family: 'Aclonica', serif;
                                        color: #fff; text-shadow: 0px 1px 0px #999, 0px 2px 0px #888, 0px 3px 0px #777, 0px 4px 0px #666, 0px 5px 0px #555, 0px 6px 0px #444, 0px 7px 0px #333, 0px 8px 7px #001135;">
                                        <asp:Label ID="lblname" runat="server" Font-Bold="true" /></div>
                                </ul>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <div style="height: 22px; text-transform: capitalize; text-align: center; margin-top: 2px;">
                                <asp:Label class="labelstyle" ID="lblStatus" runat="server" Text="" ForeColor="Red"
                                    Visible="true" Font-Size="Larger" Font-Bold="true" Font-Names="Arial" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="DivHeader" runat="server" class="otto">
                                REGISTRATION
                            </div>
                            <div id="Div2" runat="server" style="font-size: large; color: Blue; font-weight: bold;
                                width: 1300px; height: 500px; overflow: auto; background-image: url(Images/main.jpg);
                                background-repeat: repeat; margin: auto; text-align: center">
                                <table width="1280px" border="0" cellpadding="5" cellspacing="5" style="border-bottom-color: transparent;
                                    border-left-color: transparent; border-right-color: transparent; border-right-color: transparent">
                                    <tr>
                                        <td style="width: 1280px; text-align: left;">
                                            <ajaxToolkit:TabContainer runat="server" ID="TabContainer1" Height="100px" ActiveTabIndex="0"
                                                CssClass="fancy fancy-green" BackColor="#C0D7E8">
                                                <ajaxToolkit:TabPanel BackColor="Transparent" runat="server" ID="TabPanel1" HeaderText="Receiving From Customer">
                                                    <ContentTemplate>
                                                        <div id="Div1" style="background: -webkit-linear-gradient(#dbf6e3,Gray); border: 4px solid Black;">
                                                            <table width="1240px" border="0px" cellpadding="5" cellspacing="2" style="border-bottom-color: Silver;
                                                                border-left-color: Silver; border-right-color: Silver; border-right-color: Silver;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="rdoButton" Enabled="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdoButton_OnSelectedIndexChanged"
                                                                            AutoPostBack="true" ForeColor="Black" runat="server" Width="10px" Font-Size="11px"
                                                                            Visible="false">
                                                                            <asp:ListItem Text="Quotation" Value="1" Selected="True" />
                                                                            <asp:ListItem Text="Contract" Value="2" Selected="False" />
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <asp:Label ID="Label4" Text="Customer:" runat="server" Style="color: Black" />
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <telerik:RadTextBox runat="server" ID="txtCustomer" AutoPostBack="false" Width="40px" />
                                                                        <%--<asp:Button ID="btnCustomer" runat="server" Text=">" Font-Bold="true" Font-Size="Medium"
                                                                        OnClick="btnCustomer_OnClick" />--%>
                                                                        <asp:Button runat="server" ID="btncliksrch" Text=" " Font-Size="Smaller" ForeColor="Red"
                                                                            AutoPostBack="true" Width="30px" Height="22px" CssClass="search" BorderStyle="None" />
                                                                   <%-- </td>
                                                                    <td style="text-align: left"--%>
                                                                        <telerik:RadComboBox ID="CboCustomer" runat="server" Height="300px" Width="250px"
                                                                            OnSelectedIndexChanged="cboCustomer_OnSelectedIndexChanged" DropDownWidth="300px"
                                                                            EnableLoadOnDemand="true" AutoPostBack="true" AppendDataBoundItems="True" Visible="true"
                                                                            OnItemsRequested="cboCustomer_OnItemsRequested" EmptyMessage="Select Customer Name">
                                                                            <HeaderTemplate>
                                                                                <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td style="width: 280px;">
                                                                                            Customer Name
                                                                                        </td>
                                                                                        <%-- <td style="width: 100px;">
                                                                                            CRM Code
                                                                                        </td>--%>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td style="width: 280px;" align="left">
                                                                                            <%# DataBinder.Eval(Container, "Text")%>
                                                                                        </td>
                                                                                        <%-- <td style="width: 100px;" align="left">
                                                                                            <%# DataBinder.Eval(Container, "Attributes['CRM_ID']")%>
                                                                                        </td>--%>
                                                                                    </tr>
                                                                                </table>
                                                                            </ItemTemplate>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblQuotationOrContract" Text="" runat="server" Style="color: Black" />
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lblContactId" Text="" Visible="false" runat="server" />
                                                                        <telerik:RadComboBox ID="cboContractNo" runat="server" Height="200px" Width="180px"
                                                                            OnItemsRequested="cboContractNo_OnItemsRequested" OnSelectedIndexChanged="cboContract_SelectedIndexChanged"
                                                                            DropDownWidth="420px" AutoPostBack="true" DataValueField="Contract_No" EnableLoadOnDemand="true"
                                                                            AppendDataBoundItems="True">
                                                                            <HeaderTemplate>
                                                                                <table style="width: 400px" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td style="width: 130px;">
                                                                                            Contract No
                                                                                        </td>
                                                                                        <td style="width: 130px;">
                                                                                            Contract_Date
                                                                                        </td>
                                                                                        <td style="width: 140px;">
                                                                                            Expiry_Date
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <table style="width: 400px" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td style="width: 130px;" align="left">
                                                                                            <%# DataBinder.Eval(Container, "Text")%>
                                                                                        </td>
                                                                                        <td style="width: 130px;" align="left">
                                                                                            <%# DataBinder.Eval(Container, "Attributes['Contract_Date']")%>
                                                                                        </td>
                                                                                        <td style="width: 140px;" align="left">
                                                                                            <%# DataBinder.Eval(Container, "Attributes['Expiry_Date']")%>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ItemTemplate>
                                                                        </telerik:RadComboBox>
                                                                        <asp:Label ID="lblQuatmasterid" Text="" Visible="false" runat="server" />
                                                                        <telerik:RadComboBox ID="cboQuotationNo" runat="server" Height="200px" Width="180px"
                                                                            EmptyMessage="Select Quotation No" OnSelectedIndexChanged="cboQuotation_SelectedIndexChanged"
                                                                            DropDownWidth="300px" AutoPostBack="true" DataValueField="Quotation_Id" OnItemsRequested="cboQuotation_OnItemsRequested"
                                                                            EnableLoadOnDemand="true" AppendDataBoundItems="True">
                                                                            <HeaderTemplate>
                                                                                <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td style="width: 150px;">
                                                                                            Quotation No
                                                                                        </td>
                                                                                        <td style="width: 150px;">
                                                                                            Date
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td style="width: 150px;" align="left">
                                                                                            <%# DataBinder.Eval(Container, "Text")%>
                                                                                        </td>
                                                                                        <td style="width: 150px;" align="left">
                                                                                            <%# DataBinder.Eval(Container, "Attributes['Quotation_Date']")%>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ItemTemplate>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblPO" runat="server" Text="" ForeColor="Black" Font-Size="11px" AssociatedControlID="linkDownload" />
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <asp:LinkButton ID="linkDownload" Font-Size="13px" Width="50px" runat="server" OnClick="linkDownload_OnClick"
                                                                            Text="" Visible="true" />
                                                                        <asp:Label ID="lblremark" runat="server" Text="Remark:" ForeColor="Black" />
                                                                    </td>
                                                                    <td style="text-align: left" colspan="1">
                                                                        <asp:TextBox runat="server" ID="txtRemarks" Width="250px"
                                                                            TextMode="MultiLine" />
                                                                            
                                                                        <asp:LinkButton ID="btnReportQuotaion" runat="server" BackColor="#c0c0c0" OnClick="btnQuotReport_Click"
                                                                            Text="View Quotation " Visible="false" />
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Button ID="btnRegister" runat="server" Text=" Register " OnClick="btnRegister_OnClick"
                                                                            CssClass="button" />
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Button ID="btnReport" runat="server" Text=" Report " OnClick="btnReport_Click"
                                                                            CssClass="button" />
                                                                        <asp:RadioButtonList ID="radioButtonlist" Visible="false" Enabled="true" RepeatDirection="Horizontal"
                                                                            AutoPostBack="true" ForeColor="Black" runat="server" Width="150px" Font-Size="11px">
                                                                            <asp:ListItem Text="Partial" Value="1" Selected="True" />
                                                                            <asp:ListItem Text="Full" Value="2" Selected="False" />
                                                                        </asp:RadioButtonList>
                                                                        <telerik:RadComboBox ID="cboPO" runat="server" Height="60px" Width="120px" Text="--Choose PO--"
                                                                            OnSelectedIndexChanged="cboPO_SelectedIndexChanged" EmptyMessage="Choose PO"
                                                                            MarkFirstMatch="true" EnableLoadOnDemand="true" Visible="false" AutoPostBack="true"
                                                                            DropDownWidth="200px">
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <telerik:RadComboBox ID="cboBracnh" runat="server" Height="90px" Width="100px" AutoPostBack="true"
                                                                            Visible="false" DataValueField="Branch_ID" OnItemsRequested="cboBranch_OnItemsRequested"
                                                                            EnableLoadOnDemand="true" AppendDataBoundItems="True" DropDownWidth="350px" EmptyMessage="--Choose Branch--">
                                                                            <HeaderTemplate>
                                                                                <table style="width: 290px" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td style="width: 100px;">
                                                                                            Branch Code
                                                                                        </td>
                                                                                        <td style="width: 180px;">
                                                                                            Branch Name
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <table style="width: 290px" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td style="width: 100px;" align="left">
                                                                                            <%# DataBinder.Eval(Container, "Text")%>
                                                                                        </td>
                                                                                        <td style="width: 180px;" align="left">
                                                                                            <%# DataBinder.Eval(Container, "Attributes['Branch_Name']")%>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ItemTemplate>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <%--<td>
                                                                    </td>--%>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                            </ajaxToolkit:TabContainer>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1290px; text-align: left;">
                                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel3" runat="server" />
                                            <telerik:RadGrid runat="server" AllowPaging="true" ID="RadGrid1" PagerStyle-AlwaysVisible="true"
                                                OnNeedDataSource="RadGrid1_NeedDataSource" Width="1250px" AllowMultiRowSelection="false"
                                                OnItemDataBound="RadGrid1_ItemDataBound" Skin="Hay" AllowFilteringByColumn="true"
                                                PageSize="50" BorderStyle="None" GridLines="Both" Font-Size="11px">
                                                <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                                    <Selecting AllowRowSelect="true" />
                                                    <%--<ClientEvents OnRowDblClick="RowDblClick" />--%>
                                                    <%--<ClientEvents OnRowClick=--%>
                                                </ClientSettings>
                                                <MasterTableView DataKeyNames="ContratQuotationDetailID" ClientDataKeyNames="ContratQuotationDetailID"
                                                    AutoGenerateColumns="false" CommandItemDisplay="None" Width="100%">
                                                    <CommandItemSettings ShowAddNewRecordButton="false" AddNewRecordText="Add New Items"
                                                        ShowRefreshButton="false" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="EQUIPMENT_ID" DataType="System.String" HeaderText="Equipment ID."
                                                            SortExpression="EQUIPMENT_ID" UniqueName="EQUIPMENT_ID" AllowFiltering="false"
                                                            Visible="false" FilterControlWidth="70px">
                                                            <HeaderStyle Width="10%" HorizontalAlign="Left" Font-Bold="true" Font-Names="Verdana"
                                                                Font-Size="10px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" AllowFiltering="false"
                                                            Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEquipmentId" runat="server" Text='<%# Eval("EQUIPMENT_ID") %>'
                                                                    Visible="false" />
                                                                <asp:Label ID="lblCalibType_SerielNo" runat="server" Text='<%# Eval("CalibType_SerielNo") %>'
                                                                    Visible="false" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <%--                                                        <telerik:GridBoundColumn DataType="System.String" HeaderText="CalibType_SerielNo"
                                                            DataField="CalibType_SerielNo" SortExpression="CalibType_SerielNo" UniqueName="CalibType_SerielNo"
                                                            AllowFiltering="false" FilterControlWidth="70px" Visible="false">
                                                            <HeaderStyle Width="10%" HorizontalAlign="Left" Font-Bold="true" Font-Names="Verdana"
                                                                Font-Size="10px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>--%>
                                                        <%-- <telerik:GridBoundColumn DataField="EQUIPMENT_NO" DataType="System.String" HeaderText="Equip. No."
                                                            SortExpression="EQUIPMENT_NO" UniqueName="EQUIPMENT_NO" AllowFiltering="false"
                                                            FilterControlWidth="70px">
                                                            <HeaderStyle Width="7%" HorizontalAlign="Left" Font-Bold="true" Font-Names="Verdana"
                                                                Font-Size="10px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>--%>
                                                        <telerik:GridBoundColumn DataField="EQUIPMENT_NAME" DataType="System.String" HeaderText="Equipment Name"
                                                            SortExpression="EQUIPMENT_NAME" UniqueName="EQUIPMENT_NAME" AllowFiltering="false"
                                                            FilterControlWidth="150px">
                                                            <HeaderStyle Width="23%" HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="MAKER" DataType="System.Int32" HeaderText="Maker"
                                                            SortExpression="MAKER" UniqueName="MAKER" AllowFiltering="false" FilterControlWidth="30px">
                                                            <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="MODEL" DataType="System.String" HeaderText="Model"
                                                            SortExpression="MODEL" UniqueName="MODEL" AllowFiltering="false" FilterControlWidth="30px">
                                                            <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="RANGE" DataType="System.String" HeaderText="Range"
                                                            SortExpression="RANGE" UniqueName="RANGE" AllowFiltering="false" FilterControlWidth="100px">
                                                            <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="OriginalQty" DataType="System.Int32" HeaderText="Original Qty"
                                                            SortExpression="OriginalQty" UniqueName="OriginalQty" AllowFiltering="false"
                                                            FilterControlWidth="100px">
                                                            <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Remaining Qty" AllowFiltering="false" Visible="true"
                                                            UniqueName="Quantity">
                                                            <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtQty" BorderStyle="None" BackColor="Transparent"
                                                                    Text='<%# Eval("qty") %>' ForeColor="Black" Enabled="false" Width="80px" Font-Size="10px"
                                                                    ToolTip="" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Quote/Cont." AllowFiltering="false" UniqueName="Quote/Contract">
                                                            <HeaderStyle Width="8%" HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtQuote_Contract" BorderStyle="None" ForeColor="Black"
                                                                    BackColor="Transparent" Width="100px" />
                                                                <asp:TextBox runat="server" ID="txtModel" BorderStyle="None" ForeColor="Black" BackColor="Transparent"
                                                                    Width="100px" Font-Size="10px" Text='<%# Eval("MODEL") %>' Visible="false" />
                                                                <asp:TextBox runat="server" ID="txtMaker" BtorderStyle="None" ForeColor="Black" BackColor="Transparent"
                                                                    Width="100px" Font-Size="10px" Text='<%# Eval("MAKER") %>' Visible="false" />
                                                                <asp:TextBox runat="server" ID="txtContractFlag" BorderStyle="None" ForeColor="Black"
                                                                    BackColor="Transparent" Width="100px" Font-Size="10px" Visible="false" Text='<%# Eval("ContFlag") %>' />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <%--<telerik:GridBoundColumn DataField="QUOTATION_NO" DataType="System.String" HeaderText="Quote/Contract"
                                                            SortExpression="QUOTATION_NO" UniqueName="QUOTATION_NO">
                                                            <HeaderStyle Width="8%" HorizontalAlign="Left" Font-Bold="true" Font-Names="Verdana"
                                                                Font-Size="11px" />
                                                            <ItemStyle HorizontalAlign="Left" Font-Size="11px" />
                                                        </telerik:GridBoundColumn>--%>
                                                        <%--<telerik:GridTemplateColumn HeaderText="Remarks" AllowFiltering="false">
                                                            <HeaderStyle Width="10%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtRemarks" BorderStyle="None" ForeColor="Black"
                                                                    Width="350px" Font-Size="10" ToolTip='<%# Eval("EQUIPMENT_ID") %>' />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>--%>
                                                        <%--<telerik:GridTemplateColumn HeaderText="PrintCopy" AllowFiltering="false">
                                                            <HeaderStyle Width="5%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtPrintCopy" BorderStyle="None" ForeColor="Black"
                                                                    Width="45px" Text="1" Font-Size="11px" ToolTip='<%# Eval("EQUIPMENT_ID") %>' />
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtQuantity" runat="server"
                                                                    TargetControlID="txtPrintCopy" FilterType="Custom, Numbers" ValidChars="." />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>--%>
                                                        <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" HeaderText="Cal. From"
                                                            AllowFiltering="false" Visible="false">
                                                            <HeaderStyle Width="3%" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCalibrationType" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                    Enabled="false" Text="" Width="100px" />
                                                                <telerik:RadComboBox ID="cboCalibrationType" runat="server" Height="110px" Width="60px"
                                                                    DataTextField="Calib_type" DataValueField="Calib_type" AppendDataBoundItems="True"
                                                                    AutoPostBack="true">
                                                                    <%-- Text='<%# Bind("Enquiry_type") %>'--%>
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Inhouse" Value="0" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" HeaderText="Job Priority"
                                                            AllowFiltering="false" Visible="false">
                                                            <HeaderStyle Width="3%" />
                                                            <ItemTemplate>
                                                                <telerik:RadComboBox ID="cboPriority" runat="server" Height="110px" Width="70px"
                                                                    DataTextField="Calib_type" DataValueField="Calib_type" AppendDataBoundItems="True"
                                                                    AutoPostBack="true">
                                                                    <%-- Text='<%# Bind("Enquiry_type") %>'--%>
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Normal" Value="0" />
                                                                        <telerik:RadComboBoxItem Text="Urgent" Value="1" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" AllowFiltering="false">
                                                            <HeaderStyle Width="6%" />
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="EditLink" runat="server" Text="Create Job"></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Register" AllowFiltering="false">
                                                            <HeaderStyle Width="4%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkSelect" ForeColor="white" Font-Size="11px" AutoPostBack="false"
                                                                    ToolTip='<%# Eval("EQUIPMENT_ID") %>' />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" PageButtonCount="4" />
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                            <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"
                                                OnClientClose="refreshGrid" OnClientPageLoad="OnClientPageLoad">
                                                <Windows>
                                                    <telerik:RadWindow ID="UserListDialog" Behaviors="Close" runat="server" Title="Editing record"
                                                        AutoSize="false" Height="620px" Width="1150px" Left="50px" ReloadOnShow="true"
                                                        ShowContentDuringLoad="false" Modal="true" />
                                                </Windows>
                                            </telerik:RadWindowManager>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
