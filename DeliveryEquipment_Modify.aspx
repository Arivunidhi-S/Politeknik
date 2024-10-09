<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeliveryEquipment_Modify.aspx.cs" Inherits="DeliveryEquipment_Modify" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <title>Delivery Equipment Modify</title>
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
        .Save
        {
            background: url(images/Save-ic.png) no-repeat;
            background-position: center;
        }
        .down
        {
            background: url(images/Download.png) no-repeat;
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
                                    <telerik:AjaxSetting AjaxControlID="CertificateUpload">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="CertificateUpload" LoadingPanelID="RadAjaxLoadingPanel1" />
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
                                            <telerik:AjaxUpdatedControl ControlID="linkDownload5" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                    <telerik:AjaxSetting AjaxControlID="RadGridQuot">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGridQuot" />
                                            <telerik:AjaxUpdatedControl ControlID="linkDownload5" />
                                            <telerik:AjaxUpdatedControl ControlID="RadAjaxLoadingPanel3" />
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
                                DELIVERY OF EQUIPMENT MODIFY
                            </div>
                            <div id="Div1" style="height: auto; background: -webkit-linear-gradient(#dbf6e3,Gray);
                                border: 4px inset #d4d4d4;">
                                <br />
                                <table cellspacing="1" cellpadding="1" border="0" width="100%">
                                    <tr>
                                        <td style="width: 5%;" align="right">
                                            <asp:Label ID="lblcuseqimaker" Text="Customer:" runat="server" Style="color: Black" />
                                        </td>
                                        <td style="width: 20%; text-align: left">
                                            <asp:TextBox runat="server" ID="txtCustomer" Width="40px" />
                                            <asp:Button runat="server" ID="Button2" Text=" " Font-Size="Smaller" ForeColor="Red"
                                                AutoPostBack="true" Width="30px" Height="22px" CssClass="search" BorderStyle="None" />
                                            <telerik:RadComboBox ID="CboCustomer" runat="server" Height="300px" Width="200px"
                                                OnSelectedIndexChanged="cboCustomer_OnSelectedIndexChanged" DropDownWidth="300px"
                                                EnableLoadOnDemand="true" AutoPostBack="true" AppendDataBoundItems="True" Visible="true"
                                                OnItemsRequested="cboCustomer_OnItemsRequested" EmptyMessage="Select Customer Name">
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
                                        <td style="width: 5%;" align="right">
                                            <asp:Label ID="lblcuseqimodel" Text="Quotation:" Visible="true" runat="server" />
                                        </td>
                                        <td style="width: 10%;">
                                            <telerik:RadComboBox ID="cboQuotationNo" runat="server" Height="200px" Width="180px"
                                                OnSelectedIndexChanged="cboQuotation_SelectedIndexChanged" DropDownWidth="300px"
                                                AutoPostBack="true" DataValueField="Quotation_Id" OnItemsRequested="cboQuotation_OnItemsRequested"
                                                EnableLoadOnDemand="true" AppendDataBoundItems="True" EmptyMessage="Select Quotation No">
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
                                       <td style="width: 5%;" align="right">
                                            <asp:Label ID="Label1" Visible="true" runat="server" Text=" Job No:" />
                                        </td>
                                        <td style="width: 5%;">
                                            <telerik:RadComboBox ID="cbojobno" runat="server" Height="160px" Width="150px" DropDownWidth="150px"
                                                OnItemsRequested="cboJobno_OnItemsRequested" EnableLoadOnDemand="true"
                                                AutoPostBack="true" AppendDataBoundItems="True" EmptyMessage="Select Job No">
                                                <Items>
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="width: 5%;" align="right">
                                            <asp:Label ID="lblRefNo" runat="server" Text=" Ref No:"  Visible="true"/>
                                        </td>
                                        <td style="width: 5%;">
                                         <%-- <asp:TextBox ID="txtRefNo" Width="150px" runat="server" Visible="false"></asp:TextBox>--%>
                                          <telerik:RadComboBox ID="cboRefNo" runat="server" Height="160px" Width="150px" DropDownWidth="150px"
                                                OnItemsRequested="cboRefNo_OnItemsRequested" EnableLoadOnDemand="true" OnSelectedIndexChanged="cboRefNo_SelectedIndexChanged"
                                                AutoPostBack="true" AppendDataBoundItems="True" EmptyMessage="Select Job No">
                                                <Items>
                                                </Items>
                                            </telerik:RadComboBox>
                                            
                                        </td>
                                         <td style="width: 5%;">
                                            <asp:Button ID="btnReport" runat="server" Text=" Report " OnClick="btnReport_Click"
                                                CssClass="button" />
                                        </td>
                                        <td style="width: 5%;">
                                            <asp:Button runat="server" ID="btnRegister" CssClass="button" OnClick="btnRegister_Click"
                                                Text=" Save " />&nbsp;
                                        </td>
                                       
                                    </tr>
                                </table>
                                 <br />
                            </div>
                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" />
                            <table cellspacing="1" cellpadding="1" width="100%" border="0">
                                <tr>
                                    <td align="center" style="width: 100%;">
                                        <div id="Div_MIV" runat="server" style="width: 100%; height: 390px; overflow: auto;">
                                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel3" runat="server" />
                                            <telerik:RadGrid runat="server" AllowPaging="True" ID="RadGridQuot" OnNeedDataSource="RadGridQuot_NeedDataSource"
                                                Width="99%" AllowMultiRowSelection="false" GridLines="Both" OnItemDataBound="RadGridQuot_ItemDataBound"
                                                ShowFooter="true" Skin="Hay" PagerStyle-AlwaysVisible="true" AllowFilteringByColumn="true"
                                                PageSize="10">
                                                <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="false">
                                                    <Selecting AllowRowSelect="true" />
                                                </ClientSettings>
                                                <MasterTableView DataKeyNames="RECEIVED_TRANS_DETAIL_ID" ClientDataKeyNames="RECEIVED_TRANS_DETAIL_ID"
                                                    EditMode="EditForms" AutoGenerateColumns="false" CommandItemDisplay="Top" Width="100%">
                                                    <CommandItemSettings ShowAddNewRecordButton="false" AddNewRecordText="ADD NEW ITEMS"
                                                        ShowRefreshButton="true" />
                                                    <Columns>
                                                     <telerik:GridBoundColumn SortExpression="DeliveryNo" HeaderText="Reference No"
                                                            AllowFiltering="false" HeaderButtonType="TextButton" DataField="DeliveryNo"
                                                            UniqueName="DeliveryNo" Visible="true">
                                                            <HeaderStyle Width="8%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="JobNo" AllowFiltering="false">
                                                            <HeaderStyle Width="12%" Font-Names="Arial" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbljob" runat="server" Visible="false" Text='<%# Bind("JOBNO") %>'></asp:Label>
                                                                <asp:Label ID="lblrunno" runat="server" Visible="false" Text='<%# Bind("RunningNo") %>'
                                                                    ForeColor="Green"></asp:Label>
                                                                <asp:Label ID="lbljobrun" runat="server" Text=""></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn SortExpression="QUOTATION_NO" HeaderText="Quotation No"
                                                            AllowFiltering="false" HeaderButtonType="TextButton" DataField="QUOTATION_NO"
                                                            UniqueName="QUOTATION_NO" Visible="true">
                                                            <HeaderStyle Width="8%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="QUOTATION_DATE" HeaderText="Quotation Date"
                                                            AllowFiltering="false" DataField="QUOTATION_DATE" UniqueName="QUOTATION_DATE"
                                                            Visible="true" DataFormatString="{0:dd/MMM/yyyy}">
                                                            <HeaderStyle Width="3%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="CUSTOMER_NAME" HeaderText="Customer Name"
                                                            DataField="CUSTOMER_NAME" UniqueName="CUSTOMER_NAME" Visible="true" AllowFiltering="false">
                                                            <HeaderStyle Width="25%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EQUIPMENT_NAME" HeaderText="Equipment Name" SortExpression="EQUIPMENT_NAME"
                                                            UniqueName="EQUIPMENT_NAME" AllowFiltering="false">
                                                            <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="MAKER" HeaderText="Maker" DataField="MAKER"
                                                            UniqueName="MAKER" Visible="true" AllowFiltering="false">
                                                            <HeaderStyle Width="8%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="MODEL" HeaderText="Model" DataField="MODEL"
                                                            UniqueName="MODEL" Visible="true" AllowFiltering="false">
                                                            <HeaderStyle Width="8%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                      <%--  <telerik:GridBoundColumn DataField="CalibDate" HeaderText="Calibration Date" SortExpression="CalibDate"
                                                            UniqueName="CalibDate" DataFormatString="{0:dd/MMM/yyyy}" AllowFiltering="false">
                                                            <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="NextCalibDate" HeaderText="NextCalibDate"
                                                            AllowFiltering="false" HeaderButtonType="TextButton" DataField="NextCalibDate"
                                                            UniqueName="NextCalibDate" Visible="true" DataFormatString="{0:dd/MMM/yyyy}">
                                                            <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>--%>
                                                        <telerik:GridTemplateColumn HeaderText="Remove" AllowFiltering="false">
                                                            <HeaderStyle Width="3%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkselect" Font-Size="13px" runat="server" ToolTip='<%# Eval("RECEIVED_TRANS_DETAIL_ID") %>'/>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" PageButtonCount="4" />
                                                </MasterTableView>
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
