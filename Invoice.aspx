﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Invoice.aspx.cs" Inherits="Invoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <title>Invoice Form</title>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td style="border-right: blue thin solid; border-top: blue thin solid; border-left: blue thin solid;
                border-bottom: blue thin solid; border-width: 0px" align="center">
                <table border="0" width="100%">
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
                            <div style="height: 20px;">
                                <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                                <asp:Label class="labelstyle" ID="lblTest" runat="server" ForeColor="Red" Font-Bold="true" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" style="width: 100%;">
                            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                            </telerik:RadCodeBlock>
                            <telerik:RadScriptManager ID="ScriptManager1" runat="server" />
                            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                                <AjaxSettings>
                                    <%--<telerik:AjaxSetting AjaxControlID="TabContainer2">
                                            <UpdatedControls>
                                              <telerik:AjaxUpdatedControl ControlID="TabContainer2" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>--%>
                                    <telerik:AjaxSetting AjaxControlID="RadGridMain">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGridMain" />
                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                            <telerik:AjaxUpdatedControl ControlID="RadGridSource" />
                                            <telerik:AjaxUpdatedControl ControlID="linkDownload" />
                                            <telerik:AjaxUpdatedControl ControlID="lblPO" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                    <telerik:AjaxSetting AjaxControlID="RadGridSource">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGridSource" />
                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                            <telerik:AjaxUpdatedControl ControlID="RadGridMRV" />
                                            <telerik:AjaxUpdatedControl ControlID="linkDownload" />
                                            <telerik:AjaxUpdatedControl ControlID="lblPO" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                    <telerik:AjaxSetting AjaxControlID="RadGridQuot">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGridQuot" />
                                            <telerik:AjaxUpdatedControl ControlID="RadGridSource" />
                                            <telerik:AjaxUpdatedControl ControlID="RadInputManager1" />
                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                            <telerik:AjaxUpdatedControl ControlID="btnDone" />
                                            <telerik:AjaxUpdatedControl ControlID="SqlDataSourceVendor" />
                                            <telerik:AjaxUpdatedControl ControlID="cboVendor" />
                                            <telerik:AjaxUpdatedControl ControlID="linkDownload" />
                                            <telerik:AjaxUpdatedControl ControlID="lblPO" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                    <telerik:AjaxSetting AjaxControlID="cboVendor">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="SqlDataSourceVendor" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                </AjaxSettings>
                            </telerik:RadAjaxManager>
                            <telerik:RadInputManager ID="RadInputManager1" runat="server">
                                <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" InitializeOnClient="false"
                                    Validation-IsRequired="true">
                                </telerik:TextBoxSetting>
                            </telerik:RadInputManager>
                            <div id="DivHeader" runat="server" class="otto">
                                INVOICE
                            </div>
                            <div id="Div1" style="height: auto; background: -webkit-linear-gradient(#dbf6e3,Gray);
                                border: 4px inset #d4d4d4;">
                                <table width="1240px" border="0px" cellpadding="5" cellspacing="2" style="border-bottom-color: Silver;
                                    border-left-color: Silver; border-right-color: Silver; border-right-color: Silver;
                                    background-image: url(Images/main.jpg); background-repeat: repeat; height: 90px">
                                    <tr>
                                        <td style="width: 10%; text-align: right">
                                            <asp:Label ID="Label4" Text="Customer:" runat="server" Style="color: Black" />
                                        </td>
                                        <td style="width: 35%; text-align: left">
                                            <asp:TextBox runat="server" ID="txtCustomer" Width="40px" />
                                            <asp:Button runat="server" ID="Button2" Text=" " Font-Size="Smaller" ForeColor="Red"
                                                AutoPostBack="true" Width="30px" Height="22px" CssClass="search" BorderStyle="None" />
                                            <telerik:RadComboBox ID="cboCustomerId" runat="server" Height="300px" Width="200px"
                                                DropDownWidth="300px" EmptyMessage="Select Customer Name" EnableLoadOnDemand="true"
                                                OnSelectedIndexChanged="cboCustomerId_SelectedIndexChanged" AutoPostBack="true"
                                                AppendDataBoundItems="True" Visible="true" OnItemsRequested="cboCustomerId_OnItemsRequested">
                                                <HeaderTemplate>
                                                    <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 280px;">
                                                                Customer Name
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 280px;" align="left">
                                                                <%# DataBinder.Eval(Container, "Text")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="width: 10%; text-align: right">
                                            <asp:Label ID="lblQuotationOrContract" Text="Quotation:" runat="server" Style="color: Black" />
                                        </td>
                                        <td style="width: 20%; text-align: left;">
                                           
                                            <telerik:RadComboBox ID="cboQuotationNo" runat="server" Height="200px" Width="180px"
                                                EmptyMessage="Select Quotation No" OnSelectedIndexChanged="cboQuotationNo_SelectedIndexChanged"
                                                DropDownWidth="300px" AutoPostBack="true" DataValueField="Quotation_Id" OnItemsRequested="cboQuotationNo_OnItemsRequested"
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
                                        <td style="width: 10%; text-align: right">
                                            <asp:Label ID="Label1" Text="Reference No :" runat="server" Style="color: Black" />
                                        </td>
                                        <td style="width: 25%; text-align: left">
                                            <telerik:RadComboBox ID="cboJobNo" runat="server" Height="200px" Width="150px" EmptyMessage="Select Job No"
                                                OnSelectedIndexChanged="cboJobNo_SelectedIndexChanged" DropDownWidth="200px"
                                                Visible="true" AutoPostBack="true" DataValueField="Received_trans_Id" OnItemsRequested="cboJobNo_OnItemsRequested"
                                                EnableLoadOnDemand="true" AppendDataBoundItems="True">
                                                <HeaderTemplate>
                                                    <table style="width: 200px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 150px;">
                                                                Reference No
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 200px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 150px;" align="left">
                                                                <%# DataBinder.Eval(Container, "Text")%>
                                                            </td>
                                                                  </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="width: 10%; text-align: left;">
                                            <asp:Button ID="btnInvoice" runat="server" Text=" Invoice " OnClick="btnInvoice_OnClick"
                                                CssClass="button" />
                                        </td>
                                    </tr>
                                    <tr>
                                       <td style="width: 10%; text-align: right;">
                                            Date:
                                        </td>
                                        <td style="width: 10%; text-align: left">
                                            <telerik:RadDatePicker ID="DtInvoicedt" runat="server" AutoPostBack="true" Width="100px"
                                                DateInput-EmptyMessage="Date" DbSelectedDate='<%# Bind("Invoice_Date") %>' DateInput-DateFormat="dd/MMM/yyyy">
                                                <Calendar ID="Calendar1" runat="server">
                                                    <SpecialDays>
                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                    </SpecialDays>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                        

                                        
                                        <td style="width: 10%; text-align: right;">
                                            Invoice No:
                                        </td>
                                        <td style="width: 14%; text-align: left">
                                            <asp:Label ID="lblinvoiceno" runat="server" Visible="true" Text="" ForeColor="Yellow"></asp:Label>
                                        </td>
                                        <td style="width: 20%; text-align: right;">
                                            <telerik:RadButton ID="btnAddNew" runat="server" Text="Add New" OnClick="linkAddNew_OnClick">
                                                <Icon SecondaryIconCssClass="rbAdd" SecondaryIconRight="4" SecondaryIconTop="3">
                                                </Icon>
                                            </telerik:RadButton>
                                            <asp:TextBox Width="200px" ID="txtTerms" MaxLength="500" ToolTip="Maximum Length: 500"
                                                Enabled="true" TextMode="MultiLine" runat="server" Visible="false" Text="Cash"></asp:TextBox>
                                        </td>
                                        <td style="width: 10%; text-align: left;">
                                           
                                        </td>
                                        <td style="width: 10%; text-align: left;">
                                            
                                            <asp:Button ID="btnReport" runat="server" Text="  Report  " OnClick="btnReport_Click"
                                                CssClass="button" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" />
                            <table cellspacing="1" cellpadding="1" width="100%" border="0">
                                <tr>
                                    <td align="left" style="width: 60%;">
                                        <div id="Div2" runat="server" style="width: 100%; height: 390px; overflow: auto;">
                                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                                            <telerik:RadGrid runat="server" AllowPaging="True" ID="RadGridInvoice" PageSize="100"
                                                Skin="Hay" Width="99%" AllowMultiRowSelection="false" GridLines="Both" OnItemDataBound="RadGridInvoice_ItemDataBound"
                                                ShowFooter="true">
                                                <MasterTableView DataKeyNames="Quotation_trans_Id" ClientDataKeyNames="Quotation_trans_Id"
                                                    EditMode="EditForms" AutoGenerateColumns="false" CommandItemDisplay="Top" Width="100%">
                                                    <CommandItemSettings ShowAddNewRecordButton="false" AddNewRecordText="ADD NEW ITEMS"
                                                        ShowRefreshButton="true" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Quotation_trans_Id" DataType="System.Int64" HeaderText="ID"
                                                            Visible="false" ReadOnly="True" SortExpression="Quotation_trans_Id" UniqueName="Quotation_trans_Id"
                                                            AllowSorting="false" AllowFiltering="false">
                                                            <ItemStyle ForeColor="Silver" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Quotation_Id" DataType="System.String" HeaderText="Quotation_Id"
                                                            SortExpression="Quotation_Id" UniqueName="Quotation_Id" Visible="false">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Received_Trans_Detail_ID" DataType="System.String"
                                                            HeaderText="Received_Trans_Detail_ID" SortExpression="Received_Trans_Detail_ID"
                                                            UniqueName="Received_Trans_Detail_ID" Visible="false">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="JobNo" AllowFiltering="false">
                                                            <HeaderStyle Width="8%" Font-Bold="true" Font-Names="Arial" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbljob" runat="server" Visible="false" Text='<%# Bind("JOBNO") %>'></asp:Label>
                                                                <asp:Label ID="lblrunno" runat="server" Visible="false" Text='<%# Bind("RunningNo") %>'
                                                                    ForeColor="Green"></asp:Label>
                                                                <asp:Label ID="lbljobrun" runat="server" Text=""></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn SortExpression="Equipment_Name" HeaderText="Equipment_Name"
                                                            AllowFiltering="true" HeaderButtonType="TextButton" DataField="Equipment_Name"
                                                            UniqueName="Equipment_Name" Visible="true">
                                                            <HeaderStyle Width="20%" />
                                                        </telerik:GridBoundColumn>
                                                        <%--  <telerik:GridBoundColumn SortExpression="Description" HeaderText="Description" HeaderButtonType="TextButton"
                                                                DataField="Description" UniqueName="Description" Visible="true">
                                                                <HeaderStyle Width="14%" />
                                                            </telerik:GridBoundColumn>--%>
                                                        <telerik:GridBoundColumn SortExpression="Seriel_No" HeaderText="Serial No" HeaderButtonType="TextButton"
                                                            DataField="Seriel_No" UniqueName="Seriel_No" Visible="true">
                                                            <HeaderStyle Width="6%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="fee" HeaderText="Unit Price" HeaderButtonType="TextButton"
                                                            DataField="fee" UniqueName="fee" Visible="true">
                                                            <HeaderStyle Width="6%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="qty" DataType="System.String" HeaderText="Quantity"
                                                            Visible="false" SortExpression="qty" UniqueName="qty">
                                                            <HeaderStyle Width="6%" HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="QinvcQty" HeaderText="Invoiced Qty" HeaderButtonType="TextButton"
                                                            DataField="QinvcQty" UniqueName="QinvcQty" Visible="false">
                                                            <HeaderStyle Width="6%" />
                                                        </telerik:GridBoundColumn>
                                                        <%--  <telerik:GridBoundColumn SortExpression="RemainingQty" HeaderText="Remaining Qty" HeaderButtonType="TextButton"
                                                                DataField="RemainingQty" UniqueName="RemainingQty" Visible="false">
                                                                <HeaderStyle Width="6%" />
                                                            </telerik:GridBoundColumn>--%>
                                                        <%--  <telerik:GridTemplateColumn HeaderText="Invoice Qty" AllowFiltering="false" Visible="false" >
                                                                <HeaderStyle Width="9%" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <telerik:RadNumericTextBox Width="80px" ID="txtInvQty" NumberFormat-DecimalDigits="2" BorderColor="Red" Enabled="false"
                                                                        ToolTip="0" runat="server" Text='<%# Bind("RemainingQty") %>'>
                                                                    </telerik:RadNumericTextBox>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>--%>
                                                        <telerik:GridBoundColumn DataField="Maker" DataType="System.String" HeaderText="Maker"
                                                            SortExpression="Maker" UniqueName="Maker">
                                                            <HeaderStyle Width="6%" HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Model" DataType="System.String" HeaderText="Model"
                                                            SortExpression="Model" UniqueName="Model">
                                                            <HeaderStyle Width="6%" HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Remarks" AllowFiltering="false" Visible="false">
                                                            <HeaderStyle Width="20%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <telerik:RadTextBox Width="200px" ID="txtremarksdiscnt" EmptyMessage="--Type Remarks--"
                                                                    ToolTip="0" runat="server">
                                                                </telerik:RadTextBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Select" AllowFiltering="false">
                                                            <HeaderStyle Width="3%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkselect" Font-Size="13px" runat="server" ToolTip='<%# Eval("RECEIVED_TRANS_DETAIL_ID") %>' />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" PageButtonCount="5" />
                                                </MasterTableView>
                                                <ClientSettings>
                                                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 100%;" align="right">
                            <div id="Div3" runat="server" style="width: 100%; background-color: White; background-image: url(Images/Untitled.jpg);
                                overflow: auto;">
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
