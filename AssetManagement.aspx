<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssetManagement.aspx.cs"
    Inherits="AssetManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" /> 
    <title>Asset Management Form</title>
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
                                ASSET MANAGEMENT
                            </div>
                            <div id="Div1" style="height: auto; background: -webkit-linear-gradient(#dbf6e3,Gray);
                                border: 4px inset #d4d4d4;">
                                <br />
                                <table width="1240px" border="0px" cellpadding="5" cellspacing="2" style="border-bottom-color: Silver;
                                    border-left-color: Silver; border-right-color: Silver; border-right-color: Silver;
                                    background-image: url(Images/main.jpg); background-repeat: repeat; height: 70px">
                                    <tr>
                                        <td style="width: 14%; text-align: left">
                                            <asp:Label ID="lblinvoiceno" runat="server" Visible="true" Text="" ForeColor="Green"></asp:Label>
                                            <asp:Label ID="lblAdvanceno" runat="server" Visible="true" Text=""></asp:Label>
                                            <asp:Label ID="lblJobno" runat="server" Visible="true" Text=""></asp:Label>
                                        </td>
                                        <td style="width: 15%; text-align: left">
                                            Equipment Name:
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtequpment" Width="40px"/>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Button runat="server" ID="btncliksrch" Text=" " Font-Size="Smaller" ForeColor="Red"
                                                            AutoPostBack="true" Width="30px" Height="22px" CssClass="search" BorderStyle="None" />
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cboEquipmentId" runat="server" Height="250px" Width="250px"
                                                            Skin="Hay" DropDownWidth="600px" AutoPostBack="true" DataValueField="Equipment_ID"
                                                            OnItemsRequested="cboEquipmentId_OnItemsRequested" EnableLoadOnDemand="true"
                                                            AppendDataBoundItems="True">
                                                            <HeaderTemplate>
                                                                <table style="width: 560px;" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td style="width: 210px;">
                                                                            Equipment Name
                                                                        </td>
                                                                        <td style="width: 100px;">
                                                                            Model
                                                                        </td>
                                                                        <td style="width: 100px;">
                                                                            Maker
                                                                        </td>
                                                                        <td style="width: 100px;">
                                                                            Range
                                                                        </td>
                                                                        <td style="width: 50px;">
                                                                            Price
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table style="width: 560px" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td style="width: 210px;">
                                                                            <%# DataBinder.Eval(Container, "Text")%>
                                                                        </td>
                                                                        <td style="width: 100px;">
                                                                            <%# DataBinder.Eval(Container, "Attributes['Model']")%>
                                                                        </td>
                                                                        <td style="width: 100px;">
                                                                            <%# DataBinder.Eval(Container, "Attributes['Maker']")%>
                                                                        </td>
                                                                        <td style="width: 100px;">
                                                                            <%# DataBinder.Eval(Container, "Attributes['Ranges']")%>
                                                                        </td>
                                                                        <td style="width: 50px;">
                                                                            <%# DataBinder.Eval(Container, "Attributes['Fee']")%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 10%; text-align: right;">
                                            <asp:Label ID="lblContactId" Visible="true" Text="Date:" runat="server" />
                                        </td>
                                        <td style="text-align: left;">
                                            <telerik:RadDatePicker ID="DtNewDate" runat="server" AutoPostBack="true" Width="100px"
                                                Skin="Hay" DateInput-EmptyMessage="Date" DateInput-DateFormat="dd/MMM/yyyy">
                                                <Calendar ID="Calendar3" runat="server">
                                                    <SpecialDays>
                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                    </SpecialDays>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td style="width: 10%; text-align: left;">
                                        </td>
                                        <td style="width: 10%; text-align: left">
                                        </td>
                                        <td style="width: 25%; text-align: left">
                                        </td>
                                    </tr>
                                     <tr>
                                        <td style="text-align: left">
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="Label2" Text="AssetNumber :" runat="server" Style="color: Black" />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox Width="200px" ID="txtAssetNumber"  ToolTip="Maximum Length: 5"
                                                Visible="true" Enabled="True" EmptyMessage="Enter AssetNumber" TextMode="SingleLine"
                                                runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
                                            SerialNumber
                                        </td>
                                        <td style="text-align: left;">
                                           <asp:TextBox Width="200px" ID="txtSerialNumber"  ToolTip="Maximum Length: 5"
                                                Visible="true" Enabled="True" EmptyMessage="Enter SerialNumber" TextMode="SingleLine"
                                                runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left;">
                                           <%-- <asp:Button ID="Button2" runat="server" Text="  Save  " OnClick="btnSave_OnClick"
                                                CssClass="button" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="Label1" Text="New Price :" runat="server" Style="color: Black" />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox Width="100px" ID="txtNewPrice" MaxLength="10" ToolTip="Maximum Length: 5"
                                                Visible="true" Enabled="True" EmptyMessage="Enter Price" TextMode="SingleLine"
                                                runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
                                            Remarks
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox Width="200px" ID="txtRemarks" MaxLength="5" ToolTip="Maximum Length: 5"
                                                Height="30px" Visible="true" Enabled="True" EmptyMessage="Enter Price" TextMode="MultiLine"
                                                runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Button ID="Button1" runat="server" Text="  Save  " OnClick="btnSave_OnClick"
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
                                            <telerik:RadGrid runat="server" AllowPaging="True" ID="RadGridInvoice" PageSize="10"
                                                Skin="Hay" Width="99%" AllowMultiRowSelection="false" GridLines="Both" OnNeedDataSource="RadGridInvoice_NeedDataSource"
                                                ShowFooter="true" Visible="true">
                                                <MasterTableView DataKeyNames="AssetID" ClientDataKeyNames="AssetID" EditMode="EditForms"
                                                    AutoGenerateColumns="false" CommandItemDisplay="Top" Width="100%">
                                                    <CommandItemSettings ShowAddNewRecordButton="false" AddNewRecordText="ADD NEW ITEMS"
                                                        ShowRefreshButton="true" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="AssetID" DataType="System.Int64" HeaderText="ID"
                                                            Visible="false" ReadOnly="True" SortExpression="AssetID" UniqueName="AssetID"
                                                            AllowSorting="false" AllowFiltering="false">
                                                            <ItemStyle ForeColor="Silver" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="Equipment_Name" HeaderText="Equipment_Name"
                                                            AllowFiltering="true" HeaderButtonType="TextButton" DataField="Equipment_Name"
                                                            UniqueName="Equipment_Name" Visible="true">
                                                            <HeaderStyle Width="10%"  HorizontalAlign="Center" />
                                                             <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="MAKER" HeaderText="Maker" HeaderButtonType="TextButton"
                                                            DataField="MAKER" UniqueName="MAKER" Visible="true">
                                                            <HeaderStyle Width="7%" HorizontalAlign="Center"/>
                                                             <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="MODEL" HeaderText="Model" HeaderButtonType="TextButton"
                                                            DataField="MODEL" UniqueName="MODEL" Visible="true">
                                                            <HeaderStyle Width="7%" HorizontalAlign="Center"/>
                                                             <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="AssetNumber" HeaderText="Asset Number" HeaderButtonType="TextButton"
                                                            DataField="AssetNumber" UniqueName="AssetNumber" Visible="true">
                                                            <HeaderStyle Width="8%" HorizontalAlign="Center"/>
                                                             <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                         <telerik:GridBoundColumn SortExpression="SerialNumber" HeaderText="Serial Number" HeaderButtonType="TextButton"
                                                            DataField="SerialNumber" UniqueName="SerialNumber" Visible="true">
                                                            <HeaderStyle Width="8%" HorizontalAlign="Center"/>
                                                             <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="NewDate" DataType="System.DateTime" HeaderText="Date"
                                                            SortExpression="NewDate" UniqueName="NewDate" DataFormatString="{0:dd/MMM/yyyy}">
                                                            <HeaderStyle Width="6%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="NewPrice" DataType="System.String" HeaderText="New Price"
                                                            SortExpression="NewPrice" UniqueName="NewPrice">
                                                            <HeaderStyle Width="6%" HorizontalAlign="Center" />
                                                           <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Remarks" DataType="System.String" HeaderText="Remarks"
                                                            SortExpression="Remarks" UniqueName="Remarks">
                                                           <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                                           <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
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
