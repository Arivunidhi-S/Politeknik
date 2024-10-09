<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Master_Staff.aspx.cs" Inherits="Master_Staff" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Staff Master</title>
    <link rel="shortcut icon" href="images/Calib.png" />
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link rel="stylesheet" href="JS/styles.css" />
    <script src="JS/jquery-latest.min.js" type="text/javascript"></script>
    <style type="text/css">
        .otto
        {
            text-align: center;
            font-size: x-large;
            font-family: Arial;
            font-weight: bold;
            color: white;
            height: 30px;
            text-shadow: 2px 1px 5px rgba(0, 0, 0, 1);
        }
        .box
        {
            box-shadow: 0.2px 0.2px 8px 0.2px;
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
                                            <telerik:AjaxUpdatedControl ControlID="cboDeptId" />
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
                                    STAFF MASTER DETAILS
                                </div>
                                <div style="height: 20px; text-align:center" >
                                <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                            </div>
                                <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" OnNeedDataSource="RadGrid1_NeedDataSource"
                                    GridLines="Vertical" AllowPaging="True" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom"
                                    OnDeleteCommand="RadGrid1_DeleteCommand" AllowAutomaticUpdates="true" AllowAutomaticInserts="true"
                                    OnItemDataBound="RadGrid1_ItemDataBound" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="Hay"
                                    AllowAutomaticDeletes="true" OnInsertCommand="RadGrid1_InsertCommand" AllowSorting="true"
                                    AllowFilteringByColumn="true" OnUpdateCommand="RadGrid1_UpdateCommand" PageSize="20">
                                    <ClientSettings EnableRowHoverStyle="true">
                                    </ClientSettings>
                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="Staff_Id" CommandItemDisplay="Top"
                                        CommandItemSettings-AddNewRecordText="Add New Staff Details">
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                <HeaderStyle Width="5%" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn DataField="Staff_Id" DataType="System.Int64" HeaderText="ID"
                                                ReadOnly="True" SortExpression="Staff_Id" UniqueName="Staff_Id" AllowFiltering="false"
                                                AllowSorting="false" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Staff_No" DataType="System.String" HeaderText="Staff No"
                                                SortExpression="Staff_No" UniqueName="Dept_code">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Staff_Name" DataType="System.String" HeaderText="Staff Name"
                                                SortExpression="Staff_Name" UniqueName="Staff_Name">
                                                <HeaderStyle Width="20%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Position" DataType="System.String" HeaderText="Position"
                                                SortExpression="Position" UniqueName="Position" Visible="false">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Contact_No" DataType="System.String" HeaderText="Contact_No"
                                                AllowFiltering="false" SortExpression="Contact_No" UniqueName="Contact_No">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Email" DataType="System.String" HeaderText="Email"
                                                AllowFiltering="false" SortExpression="Email" UniqueName="Email">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Dept_name" DataType="System.String" HeaderText="Dept Name"
                                                SortExpression="Dept_name" UniqueName="Dept_name">
                                                <HeaderStyle Width="15%" />
                                            </telerik:GridBoundColumn>
                                            <%--<telerik:GridBoundColumn DataField="Branch_Name" DataType="System.String" HeaderText="Branch Name"
                                                SortExpression="Branch_Name" UniqueName="Branch_Name">
                                                <HeaderStyle Width="15%" />
                                            </telerik:GridBoundColumn>--%>
                                            <telerik:GridButtonColumn CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton"
                                                ConfirmText="Are you sure want to delete?">
                                                <HeaderStyle Width="5%" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <EditColumn UniqueName="EditCommandColumn1">
                                            </EditColumn>
                                            <FormTemplate>
                                                <table cellspacing="2" cellpadding="1" width="100%" border="0">
                                                    <tr>
                                                        <td colspan="2">
                                                            <b>ID:
                                                                <%--  <%# Eval("Staff_Id")%>--%>
                                                                <asp:Label ID="lblStaffID" Visible="true" runat="server" Width="20px" Text='<%# Eval("Staff_Id")%>' />
                                                            </b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Staff No:
                                                            <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red" Visible="true" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblExistingname" runat="server" Text='<%# Eval("Staff_No") %>' Visible="false" />
                                                            <asp:TextBox Width="200px" ID="txtStaffNo" MaxLength="150" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Staff_No") %>'></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="DeptcodeValidator" ControlToValidate="txtStaffNo"
                                                                ErrorMessage="Staff Code is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            Staff Name:
                                                            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red" Visible="true" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtStaffame" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Staff_Name") %>'></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="DeptNameValidator1" ControlToValidate="txtStaffame"
                                                                ErrorMessage="Staff Name is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Department Name:
                                                        </td>
                                                        <td>
                                                            <%-- <telerik:RadComboBox ID="cboDeptId" runat="server" Height="80px" Width="300px" DataValueField="Dept_ID"
                                                                DataSourceID="SqlDataSourceDept" DataTextField="Dept_Name" AutoPostBack="true"
                                                                AppendDataBoundItems="True" Visible="true">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="---Select Department---" Value="0" ForeColor="Silver" />
                                                                </Items>
                                                            </telerik:RadComboBox>--%>
                                                            <telerik:RadComboBox ID="cboDeptId" runat="server" Height="200px" Width="200px" AutoPostBack="true" DropDownWidth="310px"
                                                                DataValueField="Dept_ID" OnItemsRequested="cboDeptId_OnItemsRequested" EnableLoadOnDemand="true"
                                                                AppendDataBoundItems="True" Text='<%# Eval("Dept_Name") %>'>
                                                                <HeaderTemplate>
                                                                    <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 160px;">
                                                                                Department Name
                                                                            </td>
                                                                            <td style="width: 140px;">
                                                                                Department Code
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
                                                                            <td style="width: 140px;" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                <%# DataBinder.Eval(Container, "Attributes['Dept_Code']")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                           <%-- Branch Name:--%>   Designation:
                                                        </td>
                                                        <td> 
                                                         <asp:TextBox Width="200px" ID="txtDesignation" MaxLength="150" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Position") %>'></asp:TextBox>
                                                        
                                                         <%--<asp:TextBox Width="200px" ID="cboBranchId" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Branch_Name") %>'></asp:TextBox>--%>
                                                          <%--  <telerik:RadComboBox ID="cboBranchId" runat="server" Height="90px" Width="300px" Visible="false"
                                                                AutoPostBack="true" DataValueField="Branch_ID" OnItemsRequested="cboBranchId_OnItemsRequested"
                                                                EnableLoadOnDemand="true" AppendDataBoundItems="True">
                                                                <HeaderTemplate>
                                                                    <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 160px;">
                                                                                Branch Name
                                                                            </td>
                                                                            <td style="width: 140px;">
                                                                                Branch Code
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
                                                                                <%# DataBinder.Eval(Container, "Attributes['Branch_Code']")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                          Phone:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtPhone" MaxLength="50" ToolTip="Maximum Length: 520"
                                                                runat="server" Text='<%# Bind("Contact_no") %>'></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Email :
                                                            <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red" Visible="true" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtEmail" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                                                                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtEmail"
                                                                ErrorMessage="Email is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format" ForeColor="Red">
                                                            </asp:RegularExpressionValidator>
                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                           
                                                        </td>
                                                        <td>
                                                           
                                                            <%-- <asp:CompareValidator ID="cv" runat="server" ControlToValidate="txtPhone" Type="Integer"
                                                                ForeColor="Red" Operator="DataTypeCheck" ErrorMessage="Value must be an integer!" />--%>
                                                        </td>
                                                        <td>
                                                           <%-- HOD :--%>
                                                        </td>
                                                        <td>
                                                            <%--<asp:CheckBox ID="ChkHOD" runat="server" Text="Yes" BorderColor="Salmon" Checked='<%# (DataBinder.Eval(Container.DataItem,"HOD") is DBNull ?false:Eval("HOD")) %>' />--%>
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
