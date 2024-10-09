﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Quotation_Modify.aspx.cs"
    Inherits="Quotation_Modify" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <title>Quotation Modify Form</title>
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
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                    <telerik:AjaxSetting AjaxControlID="RadGridSource">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGridSource" />
                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                            <telerik:AjaxUpdatedControl ControlID="RadGridMRV" />
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
                                QUOTATION MODIFY
                            </div>
                            <div id="Div1" style="height: auto; background: -webkit-linear-gradient(#dbf6e3,Gray);
                                border: 4px inset #d4d4d4; padding: 10px 10px 10px 10px">
                                <table cellspacing="1" cellpadding="1" width="100%" border="0">
                                    <tr>
                                        <td align="left" style="width: 15%;">
                                            <br />
                                            <asp:Label ID="Label2" Visible="false" runat="server" Font-Size="Large" Font-Italic="true"
                                                ForeColor="white" CssClass="otto" Text="<< Quotation Modify >>" />
                                        </td>
                                        <td align="left" style="width: 22%;">
                                            <asp:RadioButtonList ID="rdoButton" Enabled="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdoButton_OnSelectedIndexChanged"
                                                Visible="false" AutoPostBack="true" ForeColor="Black" runat="server" Width="200px"
                                                Font-Size="Small">
                                                <asp:ListItem Text="Non Contract" Value="2" Selected="True" />
                                                <asp:ListItem Text="Contract" Value="1" Selected="False" />
                                            </asp:RadioButtonList>
                                        </td>
                                        <td align="left" style="width: 16%;">
                                            <telerik:RadComboBox ID="cboContractNo" runat="server" Height="200px" Width="150px"
                                                Visible="false" EmptyMessage="CONTRACT NO" DropDownWidth="420px" AutoPostBack="true"
                                                DataValueField="Contract_No" OnItemsRequested="cboContractNo_OnItemsRequested"
                                                EnableLoadOnDemand="true" AppendDataBoundItems="True">
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
                                        </td>
                                        <td align="left" style="width: 20%;">
                                            <asp:FileUpload ID="FlUploadcsv" runat="server" Visible="false" />
                                        </td>
                                        <td align="left" style="width: 10%;">
                                            <%-- <asp:Button ID="btndel" runat="server" Text="Import" OnClick="btnDel_Click" Visible="true"/>--%>
                                        </td>
                                        <td align="left" style="width: 22%;">
                                            <%--<telerik:RadComboBox ID="cbostatus" runat="server" Height="110px" Width="150px" DataTextField="Quot_Status"
                                                    DataValueField="Quot_Status" AppendDataBoundItems="True">
                                                  
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="--Select Status--" ForeColor="Black" />
                                                        <telerik:RadComboBoxItem Text="PENDING" Value="0" />
                                                        <telerik:RadComboBoxItem Text="DISCOUNT" Value="1" />
                                                        <telerik:RadComboBoxItem Text="UNSUCCESSFUL" Value="3" />
                                                    </Items>
                                                </telerik:RadComboBox>--%>
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="1" cellpadding="1" width="100%" border="0">
                                    <tr>
                                        <td align="left" style="width: 13%;">
                                            <asp:Label ID="lblEnquiryidID1" Visible="true" runat="server" Text=" Customer Name :" />
                                        </td>
                                        <td align="left" style="width: 28%;">
                                            <asp:TextBox runat="server" ID="txtCustomer" Width="40px" />
                                            <asp:Button runat="server" ID="btncliksrch" Text=" " Font-Size="Smaller" ForeColor="Red"
                                                AutoPostBack="true" Width="30px" Height="22px" CssClass="search" BorderStyle="None" />
                                            <telerik:RadComboBox ID="cboCustomerId" runat="server" Height="350px" Width="250px"
                                                Skin="Hay" EmptyMessage="Select Customer Name" DropDownWidth="300px" AutoPostBack="true"
                                                DataValueField="Category_ID" OnItemsRequested="cboCustomerId_OnItemsRequested"
                                                EnableLoadOnDemand="true" AppendDataBoundItems="True">
                                                <HeaderTemplate>
                                                    <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 280px;">
                                                                Customer Name
                                                            </td>
                                                            <%-- <td style="width: 100px;">
                                                                    CRM ID
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
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="cboCustomerId"
                                                Font-Size="XX-Small" ErrorMessage="*" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left" style="width: 10%;">
                                            Quotation Date :
                                        </td>
                                        <td align="left" style="width: 35%;">
                                            <telerik:RadDatePicker ID="DtQaotdt" runat="server" AutoPostBack="true" Width="100px"
                                                Skin="Hay" DateInput-EmptyMessage="Date" OnSelectedDateChanged="DtQaotdt_OnSelectedDateChanged"
                                                DbSelectedDate='<%# Bind("Quotation_Date") %>' DateInput-DateFormat="dd/MMM/yyyy">
                                                <Calendar ID="Calendar1" runat="server">
                                                    <SpecialDays>
                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                    </SpecialDays>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                            <asp:Label class="labelstyle" ID="Label11" Width="50px" runat="server" ForeColor="Red"
                                                Visible="true" Font-Bold="true" />
                                            <asp:Label ID="Label1" runat="server" Text="Validity Days :" Width="100px" text-align="Right"></asp:Label>
                                            <telerik:RadNumericTextBox Width="50px" ID="txtValidity" NumberFormat-DecimalDigits="0"
                                                OnTextChanged="txtvald_OnTextChanged" Enabled="true" TextMode="SingleLine" runat="server"
                                                AutoPostBack="true" Visible="true">
                                            </telerik:RadNumericTextBox>
                                            <telerik:RadDatePicker ID="txtValidityDate" runat="server" Width="100px" DateInput-EmptyMessage="Date"
                                                Skin="Hay" DbSelectedDate='<%# Bind("Validity") %>' DateInput-DateFormat="dd/MMM/yyyy">
                                                <Calendar ID="Calendar2" runat="server">
                                                    <SpecialDays>
                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                    </SpecialDays>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                            <%-- <asp:TextBox Width="500px" ID="txtValidity" MaxLength="200" ToolTip="Maximum Length: 200"
                                                    Enabled="true" TextMode="SingleLine" runat="server" Visible="true" Text='<%# Bind("Validity") %>'></asp:TextBox>--%>
                                        </td>
                                        <td align="left" style="width: 14%;">
                                            <telerik:RadNumericTextBox Width="70px" ID="txtDiscountApprov" Visible="false" NumberFormat-DecimalDigits="2"
                                                EmptyMessage="Discount" ToolTip="2 Decimal Points" runat="server" Text='<%# Bind("Discount") %>'>
                                            </telerik:RadNumericTextBox>
                                            <asp:Label runat="server" Text="(%)" ID="Label3" Visible="false"></asp:Label>
                                            <asp:Button ID="btnUpdate" Width="80px" runat="server" Text=" Update " OnClick="btnUpdate_Click"
                                                Visible="true" CssClass="button" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 10%;">
                                            Quotation No :
                                        </td>
                                        <td align="left" style="width: 20%;">
                                            <telerik:RadComboBox ID="cboQuotationNo" runat="server" Height="200px" Width="300px"
                                                Skin="Hay" OnSelectedIndexChanged="cboQuotationNo_SelectedIndexChanged" AutoPostBack="true"
                                                DataValueField="Quotation_Id" OnItemsRequested="cboQuotationNo_OnItemsRequested"
                                                EnableLoadOnDemand="true" AppendDataBoundItems="True" EmptyMessage="Select Quotation No">
                                                <HeaderTemplate>
                                                    <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 150px;">
                                                                Quotation No
                                                            </td>
                                                            <td style="width: 150px;">
                                                                Quotation_Date
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
                                            <asp:Label class="labelstyle" ID="Label4" Width="50px" runat="server" ForeColor="Red"
                                                Visible="false" Font-Bold="true" />
                                            <%-- <asp:TextBox Width="100px" ID="txtQuotationNo1" MaxLength="200" ToolTip="Maximum Length: 200"
                                                    Enabled="false" TextMode="SingleLine" runat="server" Visible="true"></asp:TextBox>--%>
                                        </td>
                                        <td align="left" style="width: 10%;">
                                            Job Duration :
                                        </td>
                                        <td align="left" style="width: 35%;">
                                            <asp:TextBox Width="400px" ID="txtJobDuration" MaxLength="200" ToolTip="Maximum Length: 200"
                                                Enabled="true" TextMode="SingleLine" runat="server" Visible="true" Text='<%# Bind("Job_Duration") %>'></asp:TextBox>
                                            <%-- <telerik:RadComboBox ID="cbojobduration" runat="server" Height="110px" Width="200px"
                                                    AppendDataBoundItems="True">
                                                 
                                                    <Items>
                                                    
                                                        <telerik:RadComboBoxItem Text="Days" Value="0" />
                                                        <telerik:RadComboBoxItem Text="Weeks" Value="1" />
                                                        <telerik:RadComboBoxItem Text="Months" Value="2" />
                                                    </Items>
                                                </telerik:RadComboBox>--%>
                                        </td>
                                        <td align="left" style="width: 14%;">
                                            <asp:Button ID="btndel" runat="server" Text="  Delete   " OnClick="btnDel_Click"
                                                Visible="true" CssClass="button" />
                                            <%--                                                <asp:Button ID="btnDiscount" Width="80px" runat="server" Text="Discount" OnClick="btnDiscount_Click" />
                                            --%>
                                            <%--  <asp:Button ID="btnDiscount" Width="80px" runat="server" Text="Discount" OnClick="btnDiscount_Click" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 13%;">
                                            Contact Name :
                                        </td>
                                        <td align="left" style="width: 28%;">
                                            <telerik:RadComboBox ID="cboContactName" runat="server" Height="90px" Width="300px"
                                                Skin="Hay" EmptyMessage="Select Contact Name" AutoPostBack="true" DataValueField="Contact_Id"
                                                OnItemsRequested="cboContactName_OnItemsRequested" EnableLoadOnDemand="true"
                                                AppendDataBoundItems="True">
                                                <HeaderTemplate>
                                                    <table style="width: 200px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 120px;">
                                                                Contact Name
                                                            </td>
                                                            <td style="width: 80px;">
                                                                Department
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 200px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 120px;" align="left">
                                                                <%# DataBinder.Eval(Container, "Text")%>
                                                            </td>
                                                            <td style="width: 80px;" align="left">
                                                                <%# DataBinder.Eval(Container, "Attributes['Department']")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td align="left" style="width: 10%;">
                                            Terms :
                                        </td>
                                        <td align="left" style="width: 35%;">
                                            <asp:TextBox Width="400px" ID="txtTerms" MaxLength="500" ToolTip="Maximum Length: 500"
                                                Enabled="true" TextMode="SingleLine" runat="server" Visible="true" Text='<%# Bind("Terms") %>'></asp:TextBox>
                                            <%--  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtTerms"
                                                    Font-Size="XX-Small" ErrorMessage="*" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </td>
                                        <td align="left" style="width: 14%;">
                                            <asp:Button ID="btnReport" Width="80px" runat="server" Text="Report" OnClick="btnReport_Click"
                                                Visible="true" CssClass="button" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 13%;">
                                            <%-- Status :--%>
                                        </td>
                                        <td align="left" style="width: 28%;">
                                            <telerik:RadComboBox ID="cbostatus" runat="server" Height="80px" Width="150px" Skin="Hay"
                                                Visible="false" AppendDataBoundItems="True" Text='<%# Bind("Quot_Status") %>'>
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="--Select Status--" ForeColor="Black" />
                                                    <telerik:RadComboBoxItem Text="PENDING" Value="0" />
                                                    <telerik:RadComboBoxItem Text="DISCOUNT" Value="1" />
                                                    <telerik:RadComboBoxItem Text="UNSUCCESSFUL" Value="3" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <telerik:RadComboBox ID="cboCalibrationQut" runat="server" Height="110px" Width="200px"
                                                Visible="false" DataTextField="Calib_type" DataValueField="Calib_type" AppendDataBoundItems="True">
                                                <%-- Text='<%# Bind("Enquiry_type") %>'--%>
                                                <Items>
                                                    <%--<telerik:RadComboBoxItem Text="--Select--" ForeColor="Black" />--%>
                                                    <telerik:RadComboBoxItem Text="Inhouse" Value="0" />
                                                    <telerik:RadComboBoxItem Text="Onsite" Value="1" />
                                                    <telerik:RadComboBoxItem Text="SubContract" Value="2" />
                                                    <telerik:RadComboBoxItem Text="InterBranch" Value="3" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <asp:Label class="labelstyle" ID="lblQuatmasterid" Width="20px" runat="server" ForeColor="Green"
                                                Visible="false" Font-Bold="true" />
                                            <asp:Label class="labelstyle" ID="txtQuotationNo" Width="200px" runat="server" ForeColor="Green"
                                                Visible="true" Font-Bold="true" />
                                        </td>
                                        <td align="left" style="width: 10%;">
                                            <%--Remarks:--%>
                                            <asp:RadioButtonList ID="RadioButtonList1" Enabled="true" RepeatDirection="Vertical"
                                                OnSelectedIndexChanged="rdoButtonfeedback_OnSelectedIndexChanged" AutoPostBack="true"
                                                ForeColor="Black" runat="server" Width="200px" Font-Size="Small" Visible="false">
                                                <asp:ListItem Text="Remarks" Value="1" Selected="True" />
                                                <asp:ListItem Text="Feedback" Value="2" Selected="False" />
                                            </asp:RadioButtonList>
                                            <br />
                                        </td>
                                        <td align="left" style="width: 35%;">
                                            <asp:TextBox Width="400px" ID="txtRemarks" MaxLength="500" ToolTip="Maximum Length: 500"
                                                Visible="false" ForeColor="Red" Enabled="true" TextMode="MultiLine" runat="server"
                                                Text='<%# Bind("Remarks") %>'></asp:TextBox>
                                            <asp:TextBox Width="400px" ID="txtfeedback" MaxLength="500" ToolTip="Maximum Length: 500"
                                                Visible="false" ForeColor="Red" Enabled="false" TextMode="MultiLine" runat="server"
                                                Text='<%# Bind("feedback") %>'></asp:TextBox>
                                        </td>
                                        <td align="left" style="width: 14%;">
                                            <asp:Button ID="btnnewjobcosting" runat="server" Text="Job Costing" OnClick="btnnewjobcosting_Click"
                                                Width="80px" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" />
                            <table cellspacing="1" cellpadding="1" width="100%" border="0">
                                <tr>
                                    <%--                                        <td align="left" style="width: 30%;">
                                            <div id="Div2" runat="server" style="width: 100%; height: 390px; background-color: White;
                                                overflow: auto;">
                                                <table cellspacing="0" cellpadding="0" width="100%" border="1">
                                                    <tr>
                                                        <td align="right" style="width: 95%;">
                                                            <div class="text" style="background-color: #edf9fe; height: 30px; width: 100%; text-align: center;
                                                                overflow: auto;">
                                                                <telerik:RadComboBox ID="cboEnquiryId" runat="server" Height="90px" Width="300px"
                                                                    EmptyMessage="Choose Enquiry No" AutoPostBack="true" DataValueField="Enquiry_Id"
                                                                    OnItemsRequested="cboEnquiryId_OnItemsRequested" OnSelectedIndexChanged="cboEnquiryId_SelectedIndexChanged"
                                                                    EnableLoadOnDemand="true" AppendDataBoundItems="True">
                                                                    <HeaderTemplate>
                                                                        <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td style="width: 160px;">
                                                                                    Enquiry No
                                                                                </td>
                                                                                <td style="width: 140px;">
                                                                                    Enquiry Date
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
                                                                                    <%# DataBinder.Eval(Container, "Attributes['Enquiry_Date']")%>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </telerik:RadComboBox>
                                                            </div>
                                                            <div class="text" style="background-color: #edf9fe; height: 350px; width: 100%; overflow: auto;">
                                                                <telerik:RadToolTipManager ID="RadToolTipManager1" runat="server">
                                                                </telerik:RadToolTipManager>
                                                                <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" OnNeedDataSource="RadGrid1_NeedDataSource"
                                                                    GridLines="Vertical" AllowPaging="True" PagerStyle-AlwaysVisible="true" Width="99%"
                                                                    OnItemCommand="RadGrid1_ItemCommand" PagerStyle-Position="Bottom" AllowAutomaticUpdates="true"
                                                                    AllowAutomaticInserts="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" AllowAutomaticDeletes="true"
                                                                    AllowSorting="true" AllowFilteringByColumn="false">
                                                                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                                                        <Selecting AllowRowSelect="true" />
                                                                    </ClientSettings>
                                                                    <MasterTableView DataKeyNames="Enquiry_Detail_ID" AutoGenerateColumns="false" EditMode="InPlace"
                                                                        CommandItemDisplay="Top" Width="99%">
                                                                        <CommandItemSettings ShowAddNewRecordButton="false" AddNewRecordText="Add New Items"
                                                                            ShowRefreshButton="true" />
                                                                        <Columns>
                                                                            <telerik:GridTemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblEnquiryDetailID" ForeColor="white" Font-Size="1"
                                                                                        AutoPostBack="true" Text='<%# Eval("Enquiry_Detail_ID") %>' Visible="false" />
                                                                                    <asp:Label runat="server" ID="lblEquipment_ID" ForeColor="white" Font-Size="1" AutoPostBack="true"
                                                                                        Text='<%# Eval("Equipment_ID") %>' Visible="false" />
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            
                                                                            <telerik:GridBoundColumn SortExpression="Enquiry_Detail_ID" HeaderText="Enquiry_Detail_ID"
                                                                                HeaderButtonType="TextButton" DataField="Enquiry_Detail_ID" UniqueName="Enquiry_Detail_ID"
                                                                                ReadOnly="true" Visible="false">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn SortExpression="Enquiry_ID" HeaderText="Enquiry_ID" HeaderButtonType="TextButton"
                                                                                DataField="Enquiry_ID" UniqueName="Enquiry_ID" Visible="false">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn SortExpression="Equipment_Name" HeaderText="Equipment_Name"
                                                                                AllowFiltering="true" HeaderButtonType="TextButton" DataField="Equipment_Name"
                                                                                UniqueName="Equipment_Name" Visible="true">
                                                                                <HeaderStyle Width="80%" Font-Bold="true" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn SortExpression="Description" HeaderText="Description" HeaderButtonType="TextButton"
                                                                                DataField="Description" UniqueName="Description" Visible="false">
                                                                            </telerik:GridBoundColumn>
                                                                        
                                                                            <telerik:GridBoundColumn SortExpression="Equipment_ID" HeaderText="Equipment_ID"
                                                                                HeaderButtonType="TextButton" DataField="Equipment_ID" UniqueName="Equipment_ID"
                                                                                Visible="false">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn SortExpression="Qty" HeaderText="Qty" HeaderButtonType="TextButton"
                                                                                DataField="Qty" UniqueName="Qty" Visible="true">
                                                                                <HeaderStyle Width="20%" Font-Bold="true" />
                                                                            </telerik:GridBoundColumn>
                                                                         
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                    <PagerStyle Mode="NextPrev" PageButtonCount="4" />
                                                                </telerik:RadGrid>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>--%>
                                    <td align="left" style="width: 70%;">
                                        <div id="Div_MIV" runat="server" style="width: 100%; height: 400px; background-color: White;
                                            overflow: auto;">
                                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel3" runat="server" />
                                            <telerik:RadGrid runat="server" AllowPaging="True" ID="RadGridQuot" OnNeedDataSource="RadGridQuot_NeedDataSource"
                                                Width="99%" AllowMultiRowSelection="false" GridLines="Both" OnItemDataBound="RadGridQuot_ItemDataBound"
                                                OnInsertCommand="RadGridQuot_InsertCommand" OnUpdateCommand="RadGridQuot_UpdateCommand"
                                                Skin="Hay" ShowFooter="true" OnDeleteCommand="RadGridQuot_DeleteCommand">
                                                <MasterTableView DataKeyNames="Quotation_trans_Id" ClientDataKeyNames="Quotation_trans_Id"
                                                    EditMode="EditForms" AutoGenerateColumns="false" CommandItemDisplay="Top" Width="100%">
                                                    <CommandItemSettings ShowAddNewRecordButton="true" AddNewRecordText="ADD NEW ITEMS"
                                                        ShowRefreshButton="true" />
                                                    <Columns>
                                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditButton">
                                                            <HeaderStyle Width="5%" />
                                                        </telerik:GridEditCommandColumn>
                                                        <telerik:GridBoundColumn DataField="Quotation_trans_Id" DataType="System.Int64" HeaderText="ID"
                                                            Visible="false" ReadOnly="True" SortExpression="Quotation_trans_Id" UniqueName="Quotation_trans_Id"
                                                            AllowSorting="false" AllowFiltering="false">
                                                            <ItemStyle ForeColor="Silver" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Quotation_Id" DataType="System.String" HeaderText="Quotation_Id"
                                                            SortExpression="Quotation_Id" UniqueName="Quotation_Id" Visible="false">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="Enquiry_ID" HeaderText="Enquiry_ID" HeaderButtonType="TextButton"
                                                            DataField="Enquiry_ID" UniqueName="Enquiry_ID" Visible="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="Equipment_Name" HeaderText="Equipment_Name"
                                                            AllowFiltering="true" HeaderButtonType="TextButton" DataField="Equipment_Name"
                                                            UniqueName="Equipment_Name" Visible="true">
                                                            <HeaderStyle Width="25%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="Description" HeaderText="Description" HeaderButtonType="TextButton"
                                                            DataField="Description" UniqueName="Description" Visible="true">
                                                            <HeaderStyle Width="22%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="ApproveDiscount" AllowFiltering="false" Visible="false">
                                                            <HeaderStyle Width="9%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <telerik:RadNumericTextBox Width="80px" ID="txtAprDiscount" NumberFormat-DecimalDigits="2"
                                                                    Visible="false" ToolTip="0" runat="server" Text='<%# Bind("Approve_discount") %>'>
                                                                </telerik:RadNumericTextBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Discount Remarks" AllowFiltering="false"
                                                            Visible="false">
                                                            <HeaderStyle Width="20%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <telerik:RadTextBox Width="200px" ID="txtremarksdiscnt" EmptyMessage="--Type Remarks--"
                                                                    Visible="false" ToolTip="0" runat="server" Text='<%# Bind("Approve_remarks") %>'>
                                                                </telerik:RadTextBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn SortExpression="Qty" HeaderText="Qty" HeaderButtonType="TextButton"
                                                            DataField="Qty" UniqueName="Qty" Visible="true">
                                                            <HeaderStyle Width="7%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Price" DataType="System.Int32" HeaderText="Price"
                                                            SortExpression="Price" UniqueName="UOM">
                                                            <HeaderStyle Width="7%" HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Discount" DataType="System.Int32" HeaderText="Discount(%)"
                                                            SortExpression="Discount" UniqueName="Discount">
                                                            <HeaderStyle Width="7%" HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <%--<telerik:GridBoundColumn DataField="Calib_type" DataType="System.String" HeaderText="Calib Type"
                                                                SortExpression="Calib_type" UniqueName="Calib_type">
                                                                <HeaderStyle Width="13%" HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </telerik:GridBoundColumn>--%>
                                                        <telerik:GridCalculatedColumn HeaderText="Total Price" UniqueName="TotalPrice" DataType="System.Decimal"
                                                            DataFields="Qty, Price" Expression="{0}*{1}" FooterText="Total: " Aggregate="Sum">
                                                            <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                        </telerik:GridCalculatedColumn>
                                                        <telerik:GridButtonColumn CommandName="Delete" Visible="true" UniqueName="DeleteColumn"
                                                            ButtonType="ImageButton" ConfirmText="Are you sure want to delete?">
                                                            <HeaderStyle Width="5%" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <EditFormSettings EditFormType="Template">
                                                        <EditColumn UniqueName="EditCommandColumn1">
                                                        </EditColumn>
                                                        <FormTemplate>
                                                            <table cellspacing="2" cellpadding="1" width="100%" border="0">
                                                                <tr>
                                                                    <td align="left" style="width: 15%;">
                                                                        <b></b>
                                                                    </td>
                                                                    <td align="left" style="width: 35%;">
                                                                        <asp:Label ID="lblQuotationtransId" Visible="false" runat="server" Width="20px" Text='<%# Eval("Quotation_trans_Id")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <%--                                                                    <asp:Label ID="lblEnquiryDetailID" Visible="true" runat="server" Width="20px" Text='<%# Eval("Enquiry_Detail_ID")%>' />
                                                                        --%>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Equipment Name:
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtequpment" Width="40px" Height="18px" />
                                                                        <asp:Button runat="server" ID="btncliksrch" Text=" " Font-Size="Smaller" ForeColor="Red"
                                                                            AutoPostBack="true" Width="30px" Height="22px" CssClass="search" BorderStyle="None" />
                                                                        <telerik:RadComboBox ID="cboEquipmentId" runat="server" Height="250px" Width="250px"
                                                                            Skin="Hay" DropDownWidth="600px" OnSelectedIndexChanged="cboEquipmentId_SelectedIndexChanged"
                                                                            AutoPostBack="true" DataValueField="Equipment_ID" OnItemsRequested="cboEquipmentId_OnItemsRequested"
                                                                            EnableLoadOnDemand="true" AppendDataBoundItems="True">
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
                                                                    <td style="text-align: left; width: 80Px">
                                                                        Desc:
                                                                    </td>
                                                                    <td style="text-align: left; width: 300Px">
                                                                        <asp:TextBox Width="300px" ID="txtDesc" MaxLength="100" ToolTip="Maximum Length: 100"
                                                                            TextMode="SingleLine" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                                                        <%--  <asp:Button ID="btnnewEquipment" runat="server" Text="New Equipment" OnClick="btnnewEquipment_Click" />--%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; width: 80Px">
                                                                        <%-- Calibration :--%>
                                                                    </td>
                                                                    <td style="text-align: left; width: 300Px">
                                                                        <telerik:RadComboBox ID="cboCalibration" runat="server" Height="100px" Width="200px"
                                                                            Skin="Hay" DataTextField="Calib_type" OnSelectedIndexChanged="cboCalibration_OnSelectedIndexChanged"
                                                                            DataValueField="Calib_type" AppendDataBoundItems="True" AutoPostBack="true" Visible="false">
                                                                            <%-- Text='<%# Bind("Enquiry_type") %>'--%>
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="--Select--" ForeColor="Black" />
                                                                                <telerik:RadComboBoxItem Text="Inhouse" Value="0" />
                                                                                <telerik:RadComboBoxItem Text="Onsite" Value="1" />
                                                                                <telerik:RadComboBoxItem Text="SubContract" Value="2" />
                                                                                <telerik:RadComboBoxItem Text="InterBranch" Value="3" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td style="text-align: left; width: 80Px">
                                                                        <%--Remarks :--%>
                                                                    </td>
                                                                    <td style="text-align: left; width: 300Px">
                                                                        <asp:TextBox Width="300px" ID="txtremarksdetails" MaxLength="500" ToolTip="Maximum Length: 500"
                                                                            Visible="false" TextMode="SingleLine" runat="server" Text='<%# Bind("Remarks_Detail") %>'></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table border="0">
                                                                <tr>
                                                                    <td style="text-align: left; width: 40Px">
                                                                        Qty:
                                                                    </td>
                                                                    <td style="text-align: left; width: 100Px">
                                                                        <telerik:RadNumericTextBox Width="50px" ID="txtQty" NumberFormat-DecimalDigits="0"
                                                                            MinValue="1" OnTextChanged="txtQty_OnTextChanged" AutoPostBack="true" ToolTip="No Decimal Points"
                                                                            runat="server" Text='<%# Bind("Qty") %>'>
                                                                        </telerik:RadNumericTextBox>
                                                                    </td>
                                                                    <td style="text-align: left; width: 40Px">
                                                                        Price:
                                                                    </td>
                                                                    <td style="text-align: left; width: 100Px">
                                                                        <telerik:RadNumericTextBox Width="20px" ID="lblprice" NumberFormat-DecimalDigits="2"
                                                                            Visible="false" Enabled="true" ToolTip="Decimal 2 Points" runat="server" Text='<%# Bind("Price") %>'>
                                                                        </telerik:RadNumericTextBox>
                                                                        <telerik:RadNumericTextBox Width="50px" ID="txtPrice" NumberFormat-DecimalDigits="2"
                                                                            MinValue="0" AutoPostBack="true" OnTextChanged="txtPrice_OnTextChanged" Enabled="true"
                                                                            ToolTip="0" runat="server" Text='<%# Bind("Price") %>'>
                                                                            <%-- <NumberFormat AllowRounding="true" DecimalDigits="1" /> --%>
                                                                        </telerik:RadNumericTextBox>
                                                                    </td>
                                                                    <td style="text-align: left; width: 70Px">
                                                                        Additional :
                                                                    </td>
                                                                    <td style="text-align: left; width: 100Px">
                                                                        <telerik:RadNumericTextBox Width="30px" ID="txtadditional" NumberFormat-DecimalDigits="0"
                                                                            MinValue="0" AutoPostBack="true" OnTextChanged="txtadditional_OnTextChanged"
                                                                            Enabled="true" ToolTip="0" Text='<%# Bind("Additional_Range") %>' runat="server">
                                                                        </telerik:RadNumericTextBox>
                                                                        <telerik:RadNumericTextBox Width="30px" ID="lbladdnlprice" NumberFormat-DecimalDigits="2"
                                                                            Visible="true" Enabled="true" ToolTip="Decimal 2 Points" runat="server" Text='<%# Bind("Additional_Price") %>'>
                                                                        </telerik:RadNumericTextBox>
                                                                    </td>
                                                                    <td style="text-align: left; width: 40Px">
                                                                        Discount:(%)
                                                                    </td>
                                                                    <td style="text-align: left; width: 100Px">
                                                                        <telerik:RadNumericTextBox Width="50px" ID="txtDiscount" AutoPostBack="true" NumberFormat-DecimalDigits="2"
                                                                            MinValue="0" ForeColor="Red" OnTextChanged="txtDiscount_OnTextChanged" ToolTip="0"
                                                                            runat="server" Text='<%# Bind("Discount") %>'>
                                                                        </telerik:RadNumericTextBox>
                                                                    </td>
                                                                    <td style="text-align: left; width: 40Px">
                                                                        Total:
                                                                    </td>
                                                                    <td style="text-align: left; width: 150Px">
                                                                        <telerik:RadNumericTextBox Width="80px" ID="txttotalprice" AutoPostBack="true" NumberFormat-DecimalDigits="2"
                                                                            Enabled="false" ToolTip="2 Decimal Points" runat="server">
                                                                        </telerik:RadNumericTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table id="table2" runat="server" border="0">
                                                                <tr>
                                                                    <td align="left" style="width: 10%;">
                                                                        <asp:Button ID="Button1" runat="server" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                            CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                            CssClass="button"></asp:Button>
                                                                    </td>
                                                                    <td align="left" style="width: 10%;">
                                                                        <asp:Button ID="Button2" runat="server" Text="Cancel" CausesValidation="false" CommandName="Cancel"
                                                                            CssClass="button"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FormTemplate>
                                                    </EditFormSettings>
                                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" PageButtonCount="4" />
                                                </MasterTableView>
                                                <ClientSettings>
                                                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </div>
                                        <%--   <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"
                                                OnClientClose="refreshGrid" OnClientPageLoad="OnClientPageLoad">
                                                <Windows>
                                                    <telerik:RadWindow ID="UserListDialog" Behaviors="Close" runat="server" Title="Editing record"
                                                        Modal="true" />
                                                </Windows>
                                            </telerik:RadWindowManager>--%>
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
    </div>
    </form>
</body>
</html>
