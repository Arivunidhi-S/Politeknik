<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Master_User.aspx.cs" Inherits="Master_User" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User Master</title>
    <link rel="shortcut icon" href="images/Calib.png" />
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
                        <td align="center" colspan="2" id="tdStatus" runat="server">
                            <div style="height: 20px;">
                            </div>
                        </td>
                    </tr>
                    <tr style="width: 100%; height: 400px;">
                        <td align="left" style="width: 80%; height: 400px;">
                            <telerik:RadScriptManager ID="ScriptManager" runat="server">
                                <%--<Scripts>
                                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
                                </Scripts>--%>
                            </telerik:RadScriptManager>
                            <div id="DivHeader" runat="server" class="otto">
                                USER MASTER DETAILS
                            </div>
                            <div style="height: 20px; text-align: center">
                                <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                            </div>
                            <ajaxToolkit:TabContainer runat="server" ID="TabCon1" Height="140px" ActiveTabIndex="0"
                                CssClass="fancy fancy-green">
                                <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel1" HeaderText="Step 1: User Information">
                                    <ContentTemplate>
                                        <div style="height: 130px; background: -webkit-linear-gradient(#dbf6e3,#f2e9b0);
                                            border: 4px solid Black;">
                                            <br />
                                            <table cellpadding="2" cellspacing="2" border="0" width="100%">
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label runat="server" CssClass="labelstyle_1" ID="lblStaffName" AssociatedControlID="cboStaffId">Name:</asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cboStaffId" runat="server" Height="200px" Width="300px" AutoPostBack="true"
                                                            OnSelectedIndexChanged="cboStaffId_SelectedIndexChanged" DataValueField="Staff_ID"
                                                            OnItemsRequested="cboStaffId_OnItemsRequested" EnableLoadOnDemand="true" AppendDataBoundItems="True">
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
                                                                        <td style="width: 140px;" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                            <%# DataBinder.Eval(Container, "Attributes['Staff_no']")%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
                                                        <asp:RequiredFieldValidator runat="server" ID="NameValidator" ControlToValidate="cboStaffId"
                                                            ErrorMessage="Name is required" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label runat="server" CssClass="labelstyle_1" ID="lblDesignation" AssociatedControlID="txtDesignation">Designation:</asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadTextBox ID="txtDesignation" CssClass="textInput" runat="server" Width="220px"
                                                            MaxLength="100" ToolTip="Max Length:100" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label runat="server" CssClass="labelstyle_1" ID="lblDept" AssociatedControlID="txtDept">Department:</asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadTextBox ID="txtDept" CssClass="textInput" runat="server" Width="220px"
                                                            MaxLength="50" ToolTip="Max Length:50" />
                                                    </td>
                                                    <td align="left">
                                                      <%--  <asp:Label runat="server" CssClass="labelstyle_1" ID="Label1" AssociatedControlID="txtBranch">Branch Name:</asp:Label>--%>
                                                    </td>
                                                    <td align="left">
                                                        <%--<telerik:RadTextBox ID="txtBranch" CssClass="textInput" runat="server" Width="220px"
                                                            MaxLength="50" ToolTip="Max Length:50" />--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 20%">
                                                        <asp:Label runat="server" CssClass="labelstyle_1" ID="lblUserId" AssociatedControlID="txtUserID">User ID:</asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 30%">
                                                        <telerik:RadTextBox ID="txtUserID" CssClass="textInput" runat="server" Width="220px"
                                                            MaxLength="50" ToolTip="Max Length:50">
                                                        </telerik:RadTextBox>
                                                        <%--  <asp:RequiredFieldValidator runat="server" ID="UserValidator" ControlToValidate="txtUserID"
                                                            ErrorMessage="UserID is required" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                    <td align="left" style="width: 15%">
                                                        <asp:Label runat="server" CssClass="labelstyle_1" ID="lblPass" AssociatedControlID="txtPass">Password:</asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 35%">
                                                        <telerik:RadTextBox ID="txtPass" CssClass="textInput" runat="server" Width="140px"
                                                            MaxLength="20" />
                                                        <%--  <asp:RequiredFieldValidator runat="server" ID="passwordValidator" ControlToValidate="txtPass"
                                                            ErrorMessage="Password Needed" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <%--  <asp:CheckBox runat="server" ID="chkApprovalRqrd" OnCheckedChanged="chkAppRqrd_CheckedChanged" 
                                                            AutoPostBack="true" /><asp:Label ID="lblChkAppRqrd" CssClass="labelstyle_1" runat="server" 
                                                                AssociatedControlID="chkApprovalRqrd">Is Approval Required</asp:Label>--%>
                                                    </td>
                                                    <td align="left">
                                                        <%-- <asp:CheckBox runat="server" ID="chkNotifyRqrd" Visible="false" /><asp:Label ID="lblchkNotifyRqrd"
                                                            Visible="false" CssClass="labelstyle_1" runat="server" AssociatedControlID="chkNotifyRqrd">Is Notify Needed</asp:Label>--%>
                                                    </td>
                                                    <td align="left">
                                                        <%--   <asp:Label runat="server" CssClass="labelstyle_1" ID="lblUserType" AssociatedControlID="txtDesignation">UseType:</asp:Label>--%>
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                        </div>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel runat="server" BackColor="#C0D7E8" ID="TabPanel3" HeaderText="Step 2: Add Module Permission"
                                    Enabled="True">
                                    <ContentTemplate>
                                        <div class="text" style="height: 130px; overflow: auto; background: -webkit-linear-gradient(#dbf6e3,#f2e9b0);
                                            border: 4px solid Black;">
                                            <br />
                                            &nbsp;
                                            <telerik:RadListBox ID="listboxModule1" runat="server" Width="300px" Height="100px"
                                                AllowTransfer="true" TransferToID="listboxModule2" AutoPostBackOnTransfer="true"
                                                ButtonSettings-ShowTransferAll="false" AllowReorder="true" AutoPostBackOnReorder="true"
                                                EnableDragAndDrop="true" DataTextField="ModuleName" DataValueField="ModuleId">
                                            </telerik:RadListBox>
                                            <telerik:RadListBox ID="listboxModule2" runat="server" Width="300px" Height="100px"
                                                SelectionMode="Multiple" AllowReorder="true" AutoPostBackOnReorder="true" EnableDragAndDrop="true">
                                            </telerik:RadListBox>
                                        </div>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                            </ajaxToolkit:TabContainer>
                            <div style="text-align: center; height: 30px; vertical-align:baseline">
                                <asp:Button runat="server" ID="btnRegister" CssClass="button" OnClick="btnRegister_Click"
                                    Text="Save" />&nbsp;
                                <asp:Button runat="server" ID="btnClearItem" CssClass="button" Text="Clear" OnClick="btnClearItem_Click" />
                            </div>
                            <telerik:RadAjaxManager ID="RadAjaxManager10" runat="server">
                                <AjaxSettings>
                                    <%--<telerik:AjaxSetting AjaxControlID="RadGrid1">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                            <telerik:AjaxUpdatedControl ControlID="tdStatus" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>--%>
                                    <telerik:AjaxSetting AjaxControlID="RadGrid1">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                                            <%--<telerik:AjaxUpdatedControl ControlID="txtUserID" />
                                            <telerik:AjaxUpdatedControl ControlID="txtPass" />
                                            <telerik:AjaxUpdatedControl ControlID="cboStaffId" />
                                            <telerik:AjaxUpdatedControl ControlID="txtDesignation" />
                                            <telerik:AjaxUpdatedControl ControlID="txtDept" />
                                            <telerik:AjaxUpdatedControl ControlID="txtBranch" />--%>
                                            <telerik:AjaxUpdatedControl ControlID="TabCon1" />
                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                        </UpdatedControls>
                                        
                                    </telerik:AjaxSetting>
                                </AjaxSettings>
                            </telerik:RadAjaxManager>
                            <telerik:RadInputManager ID="RadInputManager1" runat="server">
                                <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior1" Validation-IsRequired="true"
                                    ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ErrorMessage="Invalid Email">
                                    <TargetControls>
                                        <telerik:TargetInput ControlID="txtEmail" />
                                    </TargetControls>
                                </telerik:RegExpTextBoxSetting>
                            </telerik:RadInputManager>
                            <div id="Div3" runat="server">
                                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                                <!-- content start -->
                                <div id="Div10" runat="server" style="width: 100%; overflow: auto;">
                                    <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" OnNeedDataSource="RadGrid1_NeedDataSource"
                                        GridLines="Vertical" AllowPaging="True" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom"
                                        OnDeleteCommand="RadGrid1_DeleteCommand" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                                        Skin="Hay" AllowAutomaticDeletes="true" AllowSorting="true" OnItemCommand="RadGrid1_ItemCommand"
                                        AllowFilteringByColumn="true">
                                        <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <MasterTableView AutoGenerateColumns="false" AllowAutomaticUpdates="false" DataKeyNames="Id"
                                            CommandItemDisplay="Top" CommandItemSettings-AddNewRecordText="Add New User Details">
                                            <CommandItemSettings ShowAddNewRecordButton="false" />
                                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Id" DataType="System.Int64" HeaderText="ID" ReadOnly="True"
                                                    SortExpression="Id" UniqueName="Id" AllowFiltering="false" AllowSorting="false"
                                                    Visible="false">
                                                </telerik:GridBoundColumn>
                                               
                                                <telerik:GridBoundColumn DataField="Staff_Name" DataType="System.String" HeaderText="Staff Name"
                                                    SortExpression="Staff_Name" UniqueName="Staff_Name">
                                                    <HeaderStyle Width="25%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="position" DataType="System.String" HeaderText="Designation"
                                                    SortExpression="position" UniqueName="position">
                                                    <HeaderStyle Width="20%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Dept_Name" DataType="System.String" HeaderText="Dept Name"
                                                    SortExpression="Dept_Name" UniqueName="Dept_Name">
                                                    <HeaderStyle Width="20%" />
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridHyperLinkColumn DataTextField="LoginId" DataType="System.String" HeaderText="Login Id"
                                                    SortExpression="LoginId" UniqueName="LoginId">
                                                    <HeaderStyle Width="20%" />
                                                </telerik:GridHyperLinkColumn>
                                                <telerik:GridBoundColumn DataField="Password" DataType="System.String" HeaderText="Password"
                                                    SortExpression="Branch_Name" UniqueName="Password">
                                                    <HeaderStyle Width="20%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridButtonColumn CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton"
                                                    ConfirmText="Are you sure want to delete?">
                                                    <HeaderStyle Width="5%" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                            <EditFormSettings EditFormType="Template">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
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
