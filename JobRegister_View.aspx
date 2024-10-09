<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobRegister_View.aspx.cs"
    Inherits="JobRegister_View" %>

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
 </head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
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
                            <div style="height: 22px; text-transform: capitalize; text-align: center; margin-top: 2px;">
                                <asp:Label class="labelstyle" ID="lblStatus" runat="server" Text="" ForeColor="Red"
                                    Visible="true" Font-Size="Larger" Font-Bold="true" Font-Names="Arial" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="DivHeader" runat="server" class="otto">
                                REGISTRATION VIEW
                            </div>
                            <div id="Div2">
                                <table>
                                    <tr>
                                        <td style="width: 100%; text-align: left;">
                                            <%--  <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel3" runat="server" />--%>
                                            <telerik:RadGrid runat="server" AllowPaging="True" ID="RadGrid1" PageSize="20" PagerStyle-AlwaysVisible="true"
                                                OnNeedDataSource="RadGrid1_NeedDataSource" Skin="Hay" Width="100%" AllowMultiRowSelection="false"
                                                GridLines="Both" ShowFooter="true" AllowFilteringByColumn="true" Visible="true">
                                                <MasterTableView DataKeyNames="Received_Trans_Detail_ID" ClientDataKeyNames="Received_Trans_Detail_ID"
                                                    EditMode="EditForms" AutoGenerateColumns="false" CommandItemDisplay="Top" Width="100%">
                                                    <CommandItemSettings ShowAddNewRecordButton="false" AddNewRecordText="ADD NEW ITEMS"
                                                        ShowRefreshButton="true" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Received_Trans_Detail_ID" DataType="System.String"
                                                            HeaderText="Received_Trans_Detail_ID" SortExpression="Received_Trans_Detail_ID"
                                                            UniqueName="Received_Trans_Detail_ID" Visible="false">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CUSTOMER_NAME" DataType="System.String" HeaderText="Customer Name"
                                                            SortExpression="CUSTOMER_NAME" UniqueName="CUSTOMER_NAME" FilterControlWidth="150px">
                                                            <HeaderStyle Width="8%" HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="QUOTATION_NO" DataType="System.Int32" HeaderText="Quatation No"
                                                            SortExpression="QUOTATION_NO" UniqueName="QUOTATION_NO">
                                                            <HeaderStyle Width="6%" HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="JOBNO" HeaderText="JobNo" HeaderButtonType="TextButton"
                                                            DataField="JOBNO" UniqueName="JOBNO" Visible="true">
                                                            <HeaderStyle Width="6%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="Equipment_Name" HeaderText="Equipment_Name"
                                                            AllowFiltering="true" HeaderButtonType="TextButton" DataField="Equipment_Name"
                                                            UniqueName="Equipment_Name" Visible="true" FilterControlWidth="100px">
                                                            <HeaderStyle Width="12%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="MODEL" HeaderText="Model" HeaderButtonType="TextButton"
                                                            DataField="MODEL" UniqueName="MODEL" Visible="true">
                                                            <HeaderStyle Width="3%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="MAKER" HeaderText="Maker" HeaderButtonType="TextButton"
                                                            DataField="MAKER" UniqueName="MAKER" Visible="true">
                                                            <HeaderStyle Width="6%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Received_Date" DataType="System.DateTime" HeaderText="Register"
                                                            SortExpression="Received_Date" UniqueName="Received_Date" AllowFiltering="false"
                                                            FilterControlWidth="70px" DataFormatString="{0:dd/MMM/yyyy}">
                                                            <HeaderStyle Width="6%" HorizontalAlign="Center" Font-Names="Arial" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Accessories" HeaderText="Accessoriest" SortExpression="Accessories"
                                                            UniqueName="Accessories" AllowFiltering="false">
                                                            <HeaderStyle Width="3%" HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Seriel_no" HeaderText="Seriel No" SortExpression="Seriel_no"
                                                            UniqueName="Seriel_no">
                                                            <HeaderStyle Width="3%" HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <%--   <telerik:GridBoundColumn DataField="RejectRemarks" HeaderText="RejectRemarks" SortExpression="RejectRemarks"
                                                            UniqueName="RejectRemarks">
                                                            <HeaderStyle Width="3%" HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>--%>
                                                    </Columns>
                                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" PageButtonCount="5" />
                                                </MasterTableView>
                                                <ClientSettings>
                                                    <Scrolling AllowScroll="false" UseStaticHeaders="false" />
                                                </ClientSettings>
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
