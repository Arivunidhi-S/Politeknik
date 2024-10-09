<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration_Modify.aspx.cs" Inherits="Registration_Modify" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <title>Registration Modify Form</title>
    <link rel="shortcut icon" href="images/Calib.png" />
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link rel="stylesheet" href="JS/styles.css" />
    <link rel="stylesheet" href="css/MyCSS.css" />
    <script src="JS/jquery-latest.min.js" type="text/javascript"></script>
    <style type="text/css">
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
                                    <%-- <telerik:AjaxSetting AjaxControlID="RadGridMain">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGridMain" />
                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                            <telerik:AjaxUpdatedControl ControlID="RadGridSource" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                    <telerik:AjaxSetting AjaxControlID="RadGridSource">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGridSource" />
                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                            <telerik:AjaxUpdatedControl ControlID="RadGridMRV" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>--%>
                                    <telerik:AjaxSetting AjaxControlID="RadGridQuot">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGridQuot" />
                                            <telerik:AjaxUpdatedControl ControlID="RadGridSource" />
                                            <telerik:AjaxUpdatedControl ControlID="RadInputManager1" />
                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                            <telerik:AjaxUpdatedControl ControlID="btnDone" />
                                            <telerik:AjaxUpdatedControl ControlID="SqlDataSourceVendor" />
                                            <telerik:AjaxUpdatedControl ControlID="cboVendor" />
                                            <telerik:AjaxUpdatedControl ControlID="txtjobno" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                    <telerik:AjaxSetting AjaxControlID="txtjobno">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGridQuot" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                </AjaxSettings>
                            </telerik:RadAjaxManager>
                            <telerik:RadInputManager ID="RadInputManager1" runat="server">
                                <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" InitializeOnClient="false"
                                    Validation-IsRequired="true">
                                </telerik:TextBoxSetting>
                            </telerik:RadInputManager>
                            <div id="Div1" style="height: 70px; background: -webkit-linear-gradient(#dbf6e3,Gray);
                                border: 4px inset #d4d4d4;">
                                <table cellspacing="1" cellpadding="1" width="100%" border="0">
                                    <tr>
                                        <td align="left" style="width: 13%;">
                                            <asp:Label ID="Label2" Visible="true" runat="server" Font-Size="Large" Font-Italic="true"
                                                ForeColor="white" CssClass="otto" Text="<< Registration Modify >>" />
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="1" cellpadding="1" width="100%" border="0">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" Visible="true" runat="server" Text=" Job No :" />
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="txtjobno" runat="server" Height="160px" Width="150px" DropDownWidth="150px"
                                                OnItemsRequested="cboJobno_OnItemsRequested" EnableLoadOnDemand="true" OnSelectedIndexChanged="cbojobNo_SelectedIndexChanged"
                                                AutoPostBack="true" AppendDataBoundItems="True" EmptyMessage="Select">
                                                <Items>
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEnquiryidID1" Visible="true" runat="server" Text=" Customer Name :" />
                                        </td>
                                        <td align="left" style="width: 28%;">
                                            <asp:TextBox runat="server" ID="txtCustomer" AutoPostBack="true" Width="40px" Height="18px" />
                                            <telerik:RadComboBox ID="cboCustomerId" runat="server" Height="400px" Width="250px"
                                                OnSelectedIndexChanged="cboCustomerId_SelectedIndexChanged" Skin="Hay" DropDownWidth="440px"
                                                AutoPostBack="true" DataValueField="Category_ID" OnItemsRequested="cboCustomerId_OnItemsRequested"
                                                EnableLoadOnDemand="true" AppendDataBoundItems="True">
                                                <HeaderTemplate>
                                                    <table style="width: 400px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 300px;">
                                                                Customer Name
                                                            </td>
                                                            <td style="width: 100px;">
                                                                CRM ID
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 400px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 300px;" align="left">
                                                                <%# DataBinder.Eval(Container, "Text")%>
                                                            </td>
                                                            <td style="width: 100px;" align="left">
                                                                <%# DataBinder.Eval(Container, "Attributes['CRM_ID']")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                           <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="cboCustomerId"
                                                Font-Size="XX-Small" ErrorMessage="*" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="radioButtonlist" Enabled="true" RepeatDirection="Horizontal"
                                                ForeColor="Black" runat="server" Width="150px">
                                                <asp:ListItem Text="Partial" Value="1" Selected="True" />
                                                <asp:ListItem Text="Full" Value="2" Selected="False" />
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnRegister" CssClass="button" OnClick="btnRegister_Click"
                                                Text=" Update " />&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" />
                            <table cellspacing="1" cellpadding="1" width="100%" border="0">
                                <tr>
                                    <td align="left" style="width: 100%;">
                                        <div id="Div_MIV" runat="server" style="width: 100%; height: 390px; background-color: White;
                                            overflow: auto;">
                                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel3" runat="server" />
                                            <telerik:RadGrid runat="server" AllowPaging="True" ID="RadGridQuot" OnNeedDataSource="RadGridQuot_NeedDataSource"
                                                Width="99%" AllowMultiRowSelection="false" GridLines="Both" OnItemDataBound="RadGridQuot_ItemDataBound"
                                                ShowFooter="true" Skin="Hay">
                                                <MasterTableView DataKeyNames="RegisterAuto_ID" ClientDataKeyNames="RegisterAuto_ID" EditMode="EditForms"
                                                    AutoGenerateColumns="false" CommandItemDisplay="Top" Width="100%">
                                                    <CommandItemSettings ShowAddNewRecordButton="true" AddNewRecordText="ADD NEW ITEMS"
                                                        ShowRefreshButton="true" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Quotation_trans_Id" DataType="System.Int64" HeaderText="ID"
                                                            Visible="false" ReadOnly="True" SortExpression="Quotation_trans_Id" UniqueName="Quotation_trans_Id"
                                                            AllowSorting="false" AllowFiltering="false">
                                                            <ItemStyle ForeColor="Silver" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="RegisterAuto_ID" DataType="System.String" HeaderText="RegisterAuto_ID"
                                                            SortExpression="RegisterAuto_ID" UniqueName="RegisterAuto_ID" Visible="false">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="Enquiry_ID" HeaderText="Enquiry_ID" HeaderButtonType="TextButton"
                                                            DataField="Enquiry_ID" UniqueName="Enquiry_ID" Visible="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="Jobno" HeaderText="Job No" AllowFiltering="true"
                                                            HeaderButtonType="TextButton" DataField="Jobno" UniqueName="Jobno" Visible="true">
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="QUOTATION_NO" HeaderText="Quotation No"
                                                            AllowFiltering="true" HeaderButtonType="TextButton" DataField="QUOTATION_NO"
                                                            UniqueName="QUOTATION_NO" Visible="true">
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="QUOTATION_DATE" HeaderText="Quotation Date"
                                                            DataField="QUOTATION_DATE" UniqueName="QUOTATION_DATE" Visible="true" DataFormatString="{0:dd/MMM/yyyy}">
                                                            <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="CUSTOMER_NAME" HeaderText="Customer Name"
                                                            DataField="CUSTOMER_NAME" UniqueName="CUSTOMER_NAME" Visible="true">
                                                            <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CONTACT_PERSON" HeaderText="Contact Person" SortExpression="CONTACT_PERSON"
                                                            UniqueName="CONTACT_PERSON">
                                                            <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                                                            UniqueName="Status">
                                                            <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Accessories" AllowFiltering="false">
                                                            <HeaderStyle Width="11%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Left"  />
                                                            <ItemTemplate>
                                                                <telerik:RadTextBox Width="200px" ID="txtAccessories" Text='<%# Bind("Accessories") %>'  Enabled="true"
                                                                     runat="server" >
                                                                </telerik:RadTextBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <%-- <telerik:GridCalculatedColumn HeaderText="Total Price" UniqueName="TotalPrice" DataType="System.Decimal"
                                                            DataFields="Qty, Price" Expression="{0}*{1}" FooterText="Total: " Aggregate="Sum">
                                                            <HeaderStyle Width="8%" HorizontalAlign="Left" />
                                                        </telerik:GridCalculatedColumn>--%>
                                                        <telerik:GridTemplateColumn HeaderText="Register" AllowFiltering="false">
                                                            <HeaderStyle Width="4%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkSelect" ForeColor="white" Font-Size="1" AutoPostBack="false"
                                                                    Checked="true" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" PageButtonCount="4" />
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
