<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Master_Customer.aspx.cs"
    Inherits="Master_Customer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Customer Master</title>
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
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 80%;">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
                                </Scripts>
                            </telerik:RadScriptManager>
                            <script type="text/javascript">
                            </script>
                            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                                <AjaxSettings>
                                    <telerik:AjaxSetting AjaxControlID="RadGrid1">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                                            <telerik:AjaxUpdatedControl ControlID="RadInputManager1" />
                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                </AjaxSettings>
                            </telerik:RadAjaxManager>
                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                            <!-- content start -->
                            <div id="Div1" runat="server">
                                <telerik:RadInputManager ID="RadInputManager1" runat="server">
                                    <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" InitializeOnClient="false"
                                        Validation-IsRequired="true">
                                    </telerik:TextBoxSetting>
                                </telerik:RadInputManager>
                                <div id="DivHeader" runat="server" class="otto">
                                    CUSTOMER MASTER DETAILS
                                </div>
                                <div style="height: 20px; text-align: center">
                                    <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                                </div>
                                <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" OnNeedDataSource="RadGrid1_NeedDataSource"
                                    GridLines="Vertical" AllowPaging="True" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom"
                                    OnDeleteCommand="RadGrid1_DeleteCommand" AllowAutomaticUpdates="true" AllowAutomaticInserts="true"
                                    OnItemDataBound="RadGrid1_ItemDataBound" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                                    Skin="Hay" AllowAutomaticDeletes="true" OnInsertCommand="RadGrid1_InsertCommand"
                                    AllowSorting="true" AllowFilteringByColumn="true" OnUpdateCommand="RadGrid1_UpdateCommand">
                                    <ClientSettings EnableRowHoverStyle="true">
                                    </ClientSettings>
                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="CUSTOMER_ID" CommandItemDisplay="Top"
                                        CommandItemSettings-AddNewRecordText="Add New Customer Details">
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                <HeaderStyle Width="2%" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn DataField="CUSTOMER_ID" DataType="System.Int64" HeaderText="ID"
                                                ReadOnly="True" SortExpression="CUSTOMER_ID" UniqueName="CUSTOMER_ID" AllowFiltering="false"
                                                AllowSorting="false" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <%--  <telerik:GridBoundColumn DataField="Customer_code" DataType="System.String" HeaderText="Customer Code"
                                                SortExpression="Customer_code" UniqueName="Customer_code" Visible="true">
                                                <HeaderStyle Width="7%" />
                                            </telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn DataField="Customer_Name" DataType="System.String" HeaderText="Customer Name"
                                                SortExpression="Customer_Name" UniqueName="Customer_Name" FilterControlWidth="150px">
                                                <HeaderStyle Width="15%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Addr_Line1" DataType="System.String" HeaderText="Address Line1"
                                                AllowFiltering="false" SortExpression="Addr_Line1" UniqueName="Addr_Line1">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Addr_Line2" DataType="System.String" HeaderText="Address Line2"
                                                AllowFiltering="false" SortExpression="Addr_Line2" UniqueName="Addr_Line2">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Addr_Line3" DataType="System.String" HeaderText="Address Line3"
                                                AllowFiltering="false" SortExpression="Addr_Line3" UniqueName="Addr_Line3">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            
                                            <telerik:GridBoundColumn DataField="Postal_Code" DataType="System.String" HeaderText="Postal Code"
                                                AllowFiltering="false" SortExpression="Postal_Code" UniqueName="Postal_Code">
                                                <HeaderStyle Width="3%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="City" DataType="System.String" HeaderText="City"
                                                AllowFiltering="false" SortExpression="City" UniqueName="City">
                                                <HeaderStyle Width="7%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="State" DataType="System.String" HeaderText="State"
                                                AllowFiltering="false" SortExpression="State" UniqueName="State">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Country" DataType="System.String" HeaderText="Country"
                                                AllowFiltering="false" SortExpression="Country" UniqueName="Country">
                                                <HeaderStyle Width="4%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Contact_No" DataType="System.String" HeaderText="Contact No"
                                                AllowFiltering="true" SortExpression="Contact_No" UniqueName="Contact_No" FilterControlWidth="50px">
                                                <HeaderStyle Width="6%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Email" DataType="System.String" HeaderText="Email"
                                                AllowFiltering="false" SortExpression="Email" UniqueName="Email">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <%-- <telerik:GridBoundColumn DataField="Contract_No" DataType="System.String" HeaderText="Contract No"
                                                SortExpression="Contract_No" UniqueName="Contract_No">
                                                <HeaderStyle Width="15%" />
                                            </telerik:GridBoundColumn>--%>
                                            <telerik:GridButtonColumn CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton"
                                                ConfirmText="Are you sure want to delete?">
                                                <HeaderStyle Width="2%" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <EditColumn UniqueName="EditCommandColumn1">
                                            </EditColumn>
                                            <FormTemplate>
                                                <table cellspacing="2" cellpadding="1" width="100%" border="0">
                                                    <tr>
                                                        <td>
                                                            <%--CRM ID:--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtCRMID" MaxLength="100" ToolTip="Maximum Length: 100"
                                                                runat="server" Text='<%# Bind("CRM_ID") %>' Visible="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCustomerID" Visible="false" runat="server" Width="20px" Text='<%# Eval("CUSTOMER_ID")%>' />
                                                            <%-- Expected Completed Days:--%>
                                                        </td>
                                                        <td>
                                                            <telerik:RadNumericTextBox Width="200px" ID="txtExCompltedDays" Value="0" NumberFormat-DecimalDigits="0"
                                                                ToolTip="Decimal 0 Points" runat="server" Text='<%# Bind("ExCompltDays") %>'
                                                                Visible="false">
                                                            </telerik:RadNumericTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Customer Name:
                                                            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red" Visible="true" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtCustomerName" MaxLength="100" ToolTip="Maximum Length: 100"
                                                                runat="server" Text='<%# Bind("Customer_Name") %>'></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="BranchNameValidator1" ControlToValidate="txtCustomerName"
                                                                ErrorMessage="Customer Name is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            Customer Code:
                                                            <%--   <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red" Visible="true" />--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblExistingname" runat="server" Text='<%# Eval("Customer_Code") %>'
                                                                Visible="false" />
                                                            <asp:TextBox Width="200px" ID="txtCustomercode" MaxLength="150" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Customer_Code") %>'></asp:TextBox>
                                                            <%--   <asp:RequiredFieldValidator runat="server" ID="BranchcodeValidator" ControlToValidate="txtCustomercode"
                                                                ErrorMessage="Customer Code is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Roc No:
                                                            <%--    <asp:Label ID="Label7" runat="server" Text="*" ForeColor="Red" Visible="true" />--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text='<%# Eval("REG_ROC_NO") %>' Visible="false" />
                                                            <asp:TextBox Width="200px" ID="txtRocNo" MaxLength="150" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("REG_ROC_NO") %>'></asp:TextBox>
                                                            <%--   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtRocNo"
                                                                ErrorMessage="ROC NO is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                        <td>
                                                            Roc Reg Date :
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="Dtregdate" runat="server" Width="100px" DateInput-EmptyMessage="Date"
                                                                DbSelectedDate='<%# Bind("REG_DATE") %>' DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar1" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                            <%--<asp:TextBox Width="200px" ID="TextBox1" MaxLength="150" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("REG_DATE") %>'></asp:TextBox>--%>
                                                            <%--   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtRocNo"
                                                                ErrorMessage="ROC NO is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Address 1:
                                                            <%-- <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red" Visible="true" />--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtAddress1" MaxLength="200" ToolTip="Maximum Length: 200"
                                                                runat="server" Text='<%# Bind("Addr_Line1") %>'></asp:TextBox>
                                                            <%--  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtAddress1"
                                                                ErrorMessage="Address is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                        <td>
                                                            Address 2:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtAddress2" MaxLength="200" ToolTip="Maximum Length: 200"
                                                                runat="server" Text='<%# Bind("Addr_Line2") %>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Address 3:
                                                            <%-- <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red" Visible="true" />--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtAddress3" MaxLength="200" ToolTip="Maximum Length: 200"
                                                                runat="server" Text='<%# Bind("Addr_Line3") %>'></asp:TextBox>
                                                            <%--  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtAddress1"
                                                                ErrorMessage="Address is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                        <td>
                                                            City:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtCity" MaxLength="200" ToolTip="Maximum Length: 200"
                                                                runat="server" Text='<%# Bind("city") %>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            State:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtState" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("State") %>'></asp:TextBox>
                                                            <%--  <telerik:RadComboBox ID="txtState" runat="server" Height="90px" Width="300px" AutoPostBack="true"
                                                                DataValueField="State_Code" OnItemsRequested="txtState_OnItemsRequested" EnableLoadOnDemand="true"
                                                                AppendDataBoundItems="True">
                                                                <HeaderTemplate>
                                                                    <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 160px;">
                                                                                State Name
                                                                            </td>
                                                                            <td style="width: 140px;">
                                                                                State Code
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 160px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Text")%>
                                                                            </td>
                                                                            <td style="width: 140px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['State_Code']")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>--%>
                                                        </td>
                                                        <td>
                                                            Country:
                                                            <%--  <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red" Visible="true" />--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtCountry" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Country") %>'></asp:TextBox>
                                                            <%-- <telerik:RadComboBox ID="txtCountry" runat="server" Height="90px" Width="300px" AutoPostBack="true"
                                                                DataValueField="Country_Code" OnItemsRequested="txtCountry_OnItemsRequested"
                                                                EnableLoadOnDemand="true" AppendDataBoundItems="True">
                                                                <HeaderTemplate>
                                                                    <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 160px;">
                                                                                Country Name
                                                                            </td>
                                                                            <td style="width: 140px;">
                                                                                Country Code
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 160px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Text")%>
                                                                            </td>
                                                                            <td style="width: 140px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['Country_Code']")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Postal Code :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtPostalcode" MaxLength="200" ToolTip="Maximum Length: 200"
                                                                runat="server" Text='<%# Bind("Postal_Code") %>'></asp:TextBox>
                                                            <%-- <telerik:RadNumericTextBox Width="200px" ID="txtPostalcode" 
                                                                ToolTip="Decimal 0 Points" runat="server" Text='<%# Bind("Postal_Code") %>'>
                                                            </telerik:RadNumericTextBox>--%>
                                                        </td>
                                                        <td>
                                                            Contact No:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtPhone" MaxLength="20" ToolTip="Maximum Length: 20"
                                                                runat="server" Text='<%# Bind("Contact_no") %>'></asp:TextBox>
                                                            <asp:CheckBox ID="ChkBranch" runat="server" Text="Branch" BorderColor="Salmon" Visible="false"
                                                                Checked='<%# (DataBinder.Eval(Container.DataItem,"Is_Branch") is DBNull ?false:Eval("Is_Branch")) %>' />
                                                            <asp:CheckBox ID="ChkContract" runat="server" Text="Contract" BorderColor="Salmon"
                                                                Visible="false" Checked='<%# (DataBinder.Eval(Container.DataItem,"Is_Contract") is DBNull ?false:Eval("Is_Contract")) %>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <%-- Phone_Code1:--%>
                                                            <%--    <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red" Visible="true" />--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtphonecode" MaxLength="20" ToolTip="Maximum Length: 20"
                                                                Visible="false" runat="server" Text='<%# Bind("Contact_Code") %>'></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtPhone"
                                                                ErrorMessage="Phone No is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                        <td>
                                                            <%--    <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red" Visible="true" />--%>
                                                        </td>
                                                        <td>
                                                            <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtPhone"
                                                                ErrorMessage="Phone No is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <%-- Phone_Code2:--%>
                                                            <%--    <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red" Visible="true" />--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtPhonecode1" MaxLength="20" ToolTip="Maximum Length: 20"
                                                                Visible="false" runat="server" Text='<%# Bind("Contact_Code_1") %>'></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtPhone"
                                                                ErrorMessage="Phone No is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                        <td>
                                                            <%-- Phone2:--%>
                                                            <%--    <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red" Visible="true" />--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtPhone1" MaxLength="20" ToolTip="Maximum Length: 20"
                                                                Visible="false" runat="server" Text='<%# Bind("Contact_no_1") %>'></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtPhone"
                                                                ErrorMessage="Phone No is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Fax:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtFax" MaxLength="20" ToolTip="Maximum Length: 20"
                                                                runat="server" Text='<%# Bind("Fax_no") %>'></asp:TextBox>
                                                            <%--  <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtFax"
                                                                Type="Integer" ForeColor="Red" Operator="DataTypeCheck" ErrorMessage="Value must be an integer!" />--%>
                                                        </td>
                                                        <td>
                                                            Website:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtWebsite" MaxLength="100" ToolTip="Maximum Length: 100"
                                                                runat="server" Text='<%# Bind("website") %>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Email:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtEmail" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format" ForeColor="Red">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <%--   Contract No:--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="300px" ID="txtContractNo" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                Visible="false" TextMode="SingleLine" runat="server" Text='<%# Bind("Contract_No") %>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <%-- Account Manager:--%>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboStaffId" runat="server" Height="90px" Width="300px" AutoPostBack="true"
                                                                DataValueField="Staff_ID" OnItemsRequested="cboStaffId_OnItemsRequested" EnableLoadOnDemand="true"
                                                                AppendDataBoundItems="True" Visible="false">
                                                                <HeaderTemplate>
                                                                    <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 160px;">
                                                                                Staff Name
                                                                            </td>
                                                                            <td style="width: 140px;">
                                                                                Staff Code
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 160px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Text")%>
                                                                            </td>
                                                                            <td style="width: 140px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['Staff_no']")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>
                                                            <%--    <asp:TextBox Width="300px" ID="txtAccntMngr" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                TextMode="SingleLine" runat="server" Text='<%# Bind("Acnt_Mngr") %>'></asp:TextBox>--%>
                                                            <%--           <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtAccntMngr"
                                                                Type="Integer" ForeColor="Red" Operator="DataTypeCheck" ErrorMessage="Value must be an integer!" />--%>
                                                        </td>
                                                        <td>
                                                            <%-- Discount :--%>
                                                        </td>
                                                        <td>
                                                            <telerik:RadNumericTextBox Width="200px" ID="txtPercentage" Value="0" NumberFormat-DecimalDigits="0"
                                                                Visible="false" ToolTip="Decimal 0 Points" runat="server" Text='<%# Bind("Percentage") %>'>
                                                            </telerik:RadNumericTextBox>
                                                            <%-- <asp:TextBox Width="300px" ID="txtPercentage" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                TextMode="SingleLine" runat="server" Text='<%# Bind("Percentage") %>'></asp:TextBox>
                                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtPercentage"
                                                                Type="Integer" ForeColor="Red" Operator="DataTypeCheck" ErrorMessage="Value must be an integer!" />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Button ID="Button1" runat="server" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                            </asp:Button>
                                                            <asp:Button ID="Button2" runat="server" Text="Cancel" CausesValidation="false" CommandName="Cancel">
                                                            </asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FormTemplate>
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                </telerik:RadGrid>
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
