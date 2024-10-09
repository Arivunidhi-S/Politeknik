<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calibration_Modify.aspx.cs"
    Inherits="Calibration_Modify" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <title>Calibration Modify Form</title>
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
                                            <%-- <telerik:AjaxUpdatedControl ControlID="txtjobno" />--%>
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                    <telerik:AjaxSetting AjaxControlID="RadGridQuot">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGridQuot" />
                                            <telerik:AjaxUpdatedControl ControlID="linkDownload5" />
                                            <telerik:AjaxUpdatedControl ControlID="RadAjaxLoadingPanel3" />
                                            <%--<telerik:AjaxUpdatedControl ControlID="Response" />--%>
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                    <%-- <telerik:AjaxSetting AjaxControlID="txtjobno">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGridQuot" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>--%>
                                </AjaxSettings>
                            </telerik:RadAjaxManager>
                            <telerik:RadInputManager ID="RadInputManager1" runat="server">
                                <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" InitializeOnClient="false"
                                    Validation-IsRequired="true">
                                </telerik:TextBoxSetting>
                            </telerik:RadInputManager>
                            <div id="DivHeader" runat="server" class="otto">
                                CALIBRATION MODIFY
                            </div>
                            <div id="Div1" style="height: auto; background: -webkit-linear-gradient(#dbf6e3,Gray);
                                border: 4px inset #d4d4d4; padding:10px 10px 10px 10px;">
                                <table cellspacing="1" cellpadding="1" border="0">
                                    <tr>
                                        <td align="right" style="width: 18%;">
                                            <br />
                                            <asp:Label ID="Label2" Visible="false" runat="server" Font-Size="Large" Font-Italic="true"
                                                ForeColor="white" CssClass="otto" Text="<< Calibration >>" />
                                            <asp:LinkButton ID="LinkButton1" Text="AddNew" runat="server" OnClick="linkAddNew_OnClick"  Visible="false" ></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="1" cellpadding="1" border="0" width="100%">
                                    <tr>
                                        <td style="width: 200px;">
                                            CertificateNo:
                                        </td>
                                        <td >
                                            <telerik:RadComboBox ID="cboCertificateNo" runat="server" Height="200px" Width="150px"
                                                EmptyMessage="Select Certificate No" OnSelectedIndexChanged="cboCertificateNo_SelectedIndexChanged"
                                                DropDownWidth="200px" Visible="true" AutoPostBack="true" DataValueField="Received_trans_Id"
                                                OnItemsRequested="cboCertificateNo_OnItemsRequested" EnableLoadOnDemand="true"
                                                AppendDataBoundItems="True">
                                                <HeaderTemplate>
                                                    <table style="width: 180px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 150px;">
                                                                Certificate No
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 180px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 150px;" align="left">
                                                                <%# DataBinder.Eval(Container, "Text")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            StickerNo:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtstickno" Width="150px" runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%;">
                                            <asp:Label ID="lblcuseqimaker" Text="Customer:" runat="server" Style="color: Black" />
                                        </td>
                                        <td style="width: 20%; text-align: left">
                                            <asp:TextBox runat="server" ID="txtCustomer" Width="250px" Enabled="false" />
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:Label ID="lblcuseqimodel" Text="Quotation:" Visible="true" runat="server" />
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:TextBox runat="server" ID="txtQuotation" Width="150px" Enabled="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%;">
                                            <asp:Label ID="lbleqiserialno" Visible="true" runat="server" Text=" JobNo :" />
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:TextBox runat="server" ID="txtjobno" Width="150px" Enabled="false" />
                                        </td>
                                        <td>
                                            Equipment :
                                        </td>
                                        <td>
                                            <%-- <asp:Label ID="txtequipment" Visible="true" runat="server" />--%>
                                            <asp:TextBox ID="txtequipment" Width="250px" runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMaker" Visible="true" runat="server" Text=" Maker :" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMaker" Width="150px" runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblModel" Visible="true" runat="server" Text=" Model :" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtModel" Width="150px" runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblEnquiryidID1" Visible="true" runat="server" Text="Calibration Date:" />
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="DtFromDate" runat="server" Width="120px" DateInput-EmptyMessage="Date"
                                                AutoPostBack="true" DbSelectedDate='<%# Bind("Enquiry_Date") %>' DateInput-DateFormat="MMM/dd/yyyy"
                                                OnSelectedDateChanged="DtFromDate_OnSelectedDateChanged">
                                                <Calendar ID="Calendar2" runat="server">
                                                    <SpecialDays>
                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                    </SpecialDays>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label7" Visible="true" runat="server" Text="Next Calibration Date:" />
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="DtToDate" runat="server" Width="120px" DateInput-EmptyMessage="Date"
                                                DbSelectedDate='<%# Bind("Enquiry_Date") %>' DateInput-DateFormat="MMM/dd/yyyy">
                                                <Calendar ID="Calendar1" runat="server">
                                                    <SpecialDays>
                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                    </SpecialDays>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                     <td>
                                            <asp:Label ID="Label1" runat="server" Text=" Select Calibration Sheet:" Visible="false" />
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cboCalibSheet" runat="server" Height="200px" Width="200px" Visible="false"
                                                DropDownWidth="200px" EnableLoadOnDemand="true" AppendDataBoundItems="True" EmptyMessage="Select">
                                                <Items>
                                                </Items>
                                            </telerik:RadComboBox>
                                            <asp:Button runat="server" ID="Button1" Font-Size="Smaller" ForeColor="Red" Text=" " Visible="false"
                                                OnClick="btnCalibSheet_Click" CssClass="down" AutoPostBack="true" Width="30px"
                                                Height="22px" BorderStyle="None" />
                                        </td>
                                     <td>
                                            <asp:Label ID="Label6" Visible="true" runat="server" Text=" Calibration Sheet Upload :" />
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Button runat="server" ID="btncliksrch" Font-Size="Smaller" ForeColor="Red" Text=" "
                                                            OnClick="btnCertificate_Click" CssClass="Save" AutoPostBack="true" Width="30px"
                                                            Height="22px" BorderStyle="None" />
                                                    </td>
                                                    <td>
                                                        <%--  <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" Skin="Hay" runat="server" />--%>
                                                        <telerik:RadAsyncUpload runat="server" ID="CertificateUpload" Enabled="true" AllowedFileExtensions="xlsx,xls"
                                                            ToolTip="Max : 2MB Size/Pic" MaxFileInputsCount="1" MaxFileSize="2097152" AutoAddFileInputs="true"
                                                            ViewStateMode="Enabled">
                                                            <%-- OnFileUploaded="RadAsyncUpload1_FileUploaded"--%>
                                                        </telerik:RadAsyncUpload>
                                                        <%-- <asp:Label ID="lblCertificate" Visible="false" runat="server" Text="" ForeColor="Green" />--%>
                                                        <asp:LinkButton ID="lblCertificate" Font-Size="13px" runat="server" Visible="false"
                                                            Text="" ForeColor="Green" OnClick="linkCertificate_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                       
                                       
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnReport" runat="server" Text="  Report  " OnClick="btnReport_Click"
                                                CssClass="button" />
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnRegister" CssClass="button" OnClick="btnRegister_Click"
                                                Text=" Update " />&nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnDelete" runat="server" Text=" Delete " OnClick="btnDelete_Click"
                                                CssClass="button" Visible="true" />
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
                                                OnItemCommand="RadGridQuot_ItemCommand" PageSize="10">
                                                <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                                    <Selecting AllowRowSelect="true" />
                                                </ClientSettings>
                                                <MasterTableView DataKeyNames="CalibrationAuto_ID" ClientDataKeyNames="CalibrationAuto_ID"
                                                    EditMode="EditForms" AutoGenerateColumns="false" CommandItemDisplay="Top" Width="100%">
                                                    <CommandItemSettings ShowAddNewRecordButton="true" AddNewRecordText="ADD NEW ITEMS"
                                                        ShowRefreshButton="true" />
                                                    <Columns>
                                                        <%-- <telerik:GridBoundColumn DataField="Quotation_trans_Id" DataType="System.Int64" HeaderText="ID"
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
                                                        </telerik:GridBoundColumn>--%>
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
                                                            AllowFiltering="true" HeaderButtonType="TextButton" DataField="QUOTATION_NO"
                                                            UniqueName="QUOTATION_NO" Visible="true">
                                                            <HeaderStyle Width="8%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="QUOTATION_DATE" HeaderText="Quotation Date"
                                                            DataField="QUOTATION_DATE" UniqueName="QUOTATION_DATE" Visible="true" DataFormatString="{0:dd/MMM/yyyy}">
                                                            <HeaderStyle Width="3%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="CUSTOMER_NAME" HeaderText="Customer Name"
                                                            DataField="CUSTOMER_NAME" UniqueName="CUSTOMER_NAME" Visible="true">
                                                            <HeaderStyle Width="25%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EQUIPMENT_NAME" HeaderText="Equipment Name" SortExpression="EQUIPMENT_NAME"
                                                            UniqueName="EQUIPMENT_NAME">
                                                            <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="MAKER" HeaderText="Maker" DataField="MAKER"
                                                            UniqueName="MAKER" Visible="true">
                                                            <HeaderStyle Width="8%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="MODEL" HeaderText="Model" DataField="MODEL"
                                                            UniqueName="MODEL" Visible="true">
                                                            <HeaderStyle Width="8%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CalibDate" HeaderText="Calibration Date" SortExpression="CalibDate"
                                                            UniqueName="CalibDate" DataFormatString="{0:dd/MMM/yyyy}">
                                                            <HeaderStyle Width="3%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="NextCalibDate" HeaderText="NextCalibDate"
                                                            AllowFiltering="true" HeaderButtonType="TextButton" DataField="NextCalibDate"
                                                            UniqueName="NextCalibDate" Visible="true" DataFormatString="{0:dd/MMM/yyyy}">
                                                            <HeaderStyle Width="3%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <%-- <telerik:GridCalculatedColumn HeaderText="Total Price" UniqueName="TotalPrice" DataType="System.Decimal"
                                                            DataFields="Qty, Price" Expression="{0}*{1}" FooterText="Total: " Aggregate="Sum">
                                                            <HeaderStyle Width="8%" HorizontalAlign="Left" />
                                                        </telerik:GridCalculatedColumn>--%>
                                                        <telerik:GridTemplateColumn HeaderText="Calibration Sheet" AllowFiltering="false">
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="linkDownload5" Font-Size="13px" runat="server" Text='<%# Bind("CertificateFile") %>' />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" PageButtonCount="4" />
                                                </MasterTableView>
                                                <%-- <ClientSettings>
                                                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                                </ClientSettings>--%>
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
