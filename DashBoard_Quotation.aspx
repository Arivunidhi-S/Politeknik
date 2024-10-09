<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashBoard_Quotation.aspx.cs"
    Inherits="DashBoard_Quotation" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2011.2.1100.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a"
    Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html>
<%--<html >--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<%--<html lang="en" class="no-js">--%>
<head style="margin-top: -20px">
    <title>Sales Module | Dashboard - Quotation</title>
    <%--<meta charset="utf-8"/>--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all"
        rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet"
        type="text/css" />
    <link href="assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet"
        type="text/css" />
    <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet"
        type="text/css" />
    <link href="assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet"
        type="text/css" />
    <link href="assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css"
        rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGIN STYLES -->
    <link href="assets/global/plugins/gritter/css/jquery.gritter.css" rel="stylesheet"
        type="text/css" />
    <link href="assets/global/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css"
        rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/fullcalendar/fullcalendar/fullcalendar.css" rel="stylesheet"
        type="text/css" />
    <link href="assets/global/plugins/jqvmap/jqvmap/jqvmap.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL PLUGIN STYLES -->
    <!-- BEGIN PAGE STYLES -->
    <link href="assets/admin/pages/css/tasks.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE STYLES -->
    <!-- BEGIN THEME STYLES -->
    <link href="assets/global/css/components.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/css/plugins.css" rel="stylesheet" type="text/css" />
    <link href="assets/admin/layout/css/layout.css" rel="stylesheet" type="text/css" />
    <link href="assets/admin/layout/css/themes/darkblue.css" rel="stylesheet" type="text/css"
        id="style_color" />
    <link href="assets/admin/layout/css/custom.css" rel="stylesheet" type="text/css" />
    <!-- END THEME STYLES -->
    <link rel="shortcut icon" href="favicon.ico" />
    <link rel="stylesheet" href="css/menu_core.css" type="text/css" />
    <!--Style Skin (menu widget)-->
    <link rel="stylesheet" href="css/skins/menu_simpleAnimated.css" type="text/css" />
</head>
<!--Custom Styles-->
<!-- END HEAD -->
<!-- BEGIN BODY -->
<!-- DOC: Apply "page-header-fixed-mobile" and "page-footer-fixed-mobile" class to body element to force fixed header or footer in mobile devices -->
<!-- DOC: Apply "page-sidebar-closed" class to the body and "page-sidebar-menu-closed" class to the sidebar menu element to hide the sidebar by default -->
<!-- DOC: Apply "page-sidebar-hide" class to the body to make the sidebar completely hidden on toggle -->
<!-- DOC: Apply "page-sidebar-closed-hide-logo" class to the body element to make the logo hidden on sidebar toggle -->
<!-- DOC: Apply "page-sidebar-hide" class to body element to completely hide the sidebar on sidebar toggle -->
<!-- DOC: Apply "page-sidebar-fixed" class to have fixed sidebar -->
<!-- DOC: Apply "page-footer-fixed" class to the body element to have fixed footer -->
<!-- DOC: Apply "page-sidebar-reversed" class to put the sidebar on the right side -->
<!-- DOC: Apply "page-full-width" class to the body element to have full width page without the sidebar menu -->
<body class="page-header-fixed-mobile page-quick-sidebar-over-content">
    <%--   <body style=" margin: -160px 0px 0px 0px;">--%>
    <form id="form1" runat="server">
    <table border="1" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
            <td style="border-right: blue thin solid; border-top: blue thin solid; border-left: blue thin solid;
                border-bottom: blue thin solid; border-width: 0px" align="center">
                <hr style="visibility: hidden;" />
                <hr style="visibility: hidden;" />
                <table border="1" width="100%">
                    <tr>
                        <td id="Td1" align="left" runat="server" colspan="2">
                            <ul id="myMenu" class="nfMain nfPure">
                                <% for (int a = 0; a < dtMenuItems.Rows.Count; a++)
                                   { %>
                                <li class="nfItem"><a class="nfLink" href="#">
                                    <%= dtMenuItems.Rows[a][0].ToString()  %></a>
                                    <div class="nfSubC nfSubS">
                                        <% dtSubMenuItems = BusinessTier.getSubMenuItems(dtMenuItems.Rows[a][0].ToString(), Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                                           int aa;
                                           for (aa = 0; aa < dtSubMenuItems.Rows.Count; aa++)
                                           { %>
                                        <div class="nfItem">
                                            <a class="nfLink" id='<%= dtSubMenuItems.Rows[aa][0].ToString() %>' href='<%= dtSubMenuItems.Rows[aa][1].ToString() %>'>
                                                <%= dtSubMenuItems.Rows[aa][2].ToString()%></a>
                                        </div>
                                        <% } %>
                                    </div>
                                </li>
                                <% } %>
                                <li class="nfItem"><a class="nfLink" href="Login.aspx" style="border-right-width: 1px;">
                                    LOGOUT</a></li>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td id="Td11" align="left" style="width: 80%;" runat="server">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
                                </Scripts>
                            </telerik:RadScriptManager>
                            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                                <AjaxSettings>
                                    <telerik:AjaxSetting AjaxControlID="RadGrid1">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                                            <telerik:AjaxUpdatedControl ControlID="RadInputManager1" />
                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                            <telerik:AjaxUpdatedControl ControlID="cboEnquirytype" />
                                            <telerik:AjaxUpdatedControl ControlID="DtTestDate" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                </AjaxSettings>
                            </telerik:RadAjaxManager>
                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%--  <div class="page-header navbar navbar-fixed-top">--%>
    <!-- BEGIN HEADER INNER -->
    <%--<div class="page-header-inner">--%>
    <!-- BEGIN LOGO -->
    <%-- <div class="page-logo">
               <%-- <img src="Images/logo.png" alt="logo" />
                <div class="menu-toggler sidebar-toggler hide">
               
                </div>
            </div>--%>
    <!-- END LOGO -->
    <!-- BEGIN RESPONSIVE MENU TOGGLER -->
    <!-- END RESPONSIVE MENU TOGGLER -->
    <!-- BEGIN TOP NAVIGATION MENU -->
    <!-- END TOP NAVIGATION MENU -->
    <%--</div>--%>
    <!-- END HEADER INNER -->
    <%-- </div>--%>
    <!-- END HEADER -->
    <!-- BEGIN CONTAINER -->
    <div class="page-container">
        <!-- BEGIN SIDEBAR -->
        <div class="page-sidebar-wrapper">
            <!-- DOC: Set data-auto-scroll="false" to disable the sidebar from auto scrolling/focusing -->
            <!-- DOC: Change data-auto-speed="200" to adjust the sub menu slide up/down speed -->
            <div class="page-sidebar navbar-collapse collapse">
                <!-- BEGIN SIDEBAR MENU -->
                <ul class="page-sidebar-menu " data-auto-scroll="true" data-slide-speed="200">
                    <!-- DOC: To remove the sidebar toggler from the sidebar you just need to completely remove the below "sidebar-toggler-wrapper" LI element -->
                    <li class="start active open"><a href="javascript:;"><i class="icon-home"></i><span
                        class="title">Quotation</span> <span class="selected"></span><span class="arrow open">
                        </span></a></li>
                    <li class="danger"><a href="DashBoard.aspx"><i class="icon-bulb"></i><span class="title">
                        Enquiry</span> </a></li>
                </ul>
                <ul class="sub-menu">
                    <li><i class="icon-basket"></i></li>
                    <%--<li class="active">--%>
                    <a class="btn btn-sm grey-salt btn-circle">
                        <telerik:RadNumericTextBox Width="100px" ID="txtyear" NumberFormat-DecimalDigits="0"
                            NumberFormat-GroupSizes="4" Visible="true" Enabled="true" ToolTip="No Decimal Points"
                            runat="server">
                        </telerik:RadNumericTextBox>
                        <%--<asp:TextBox ID="txtyear" runat="server" Text=" " BackColor="Transparent" Font-Bold="true" ForeColor="Black"  />--%>
                    </a>
                    <%--</li>--%>
                    <li class="active"></li>
                    <li class="active"><a class="btn btn-sm green-haze btn-circle">
                        <asp:Button ID="btnJan" runat="server" Text="JAN" BackColor="Transparent" Font-Bold="true"
                            ForeColor="Black" OnClick="btnJan_OnClick" />
                        <asp:Button ID="btnFeb" runat="server" Text="FEB" BackColor="Transparent" Font-Bold="true"
                            ForeColor="Black" OnClick="btnFeb_OnClick" />
                    </a></li>
                    <li class="active"></li>
                    <li class="active"><a class="btn btn-sm green-haze btn-circle">
                        <asp:Button ID="btnMar" runat="server" Text="MAR" BackColor="Transparent" Font-Bold="true"
                            ForeColor="Black" OnClick="btnMar_OnClick" />
                        <asp:Button ID="btnApr" runat="server" Text="APR" BackColor="Transparent" Font-Bold="true"
                            ForeColor="Black" OnClick="btnApr_OnClick" />
                    </a></li>
                    <li class="active"></li>
                    <li class="active"><a class="btn btn-sm green-haze btn-circle">
                        <asp:Button ID="btnMay" runat="server" Text="MAY" BackColor="Transparent" Font-Bold="true"
                            ForeColor="Black" OnClick="btnMay_OnClick" />
                        <asp:Button ID="btnJun" runat="server" Text="JUN" BackColor="Transparent" Font-Bold="true"
                            ForeColor="Black" OnClick="btnJun_OnClick" />
                    </a></li>
                    <li class="active"></li>
                    <li class="active"><a class="btn btn-sm blue-madison btn-circle">
                        <asp:Button ID="btnJuly" runat="server" Text="JUL" BackColor="Transparent" Font-Bold="true"
                            ForeColor="Black" OnClick="btnJuly_OnClick" />
                        <asp:Button ID="btnAug" runat="server" Text="AUG" BackColor="Transparent" Font-Bold="true"
                            ForeColor="Black" OnClick="btnAug_OnClick" />
                    </a></li>
                    <li class="active"></li>
                    <li class="active"><a class="btn btn-sm blue-madison btn-circle">
                        <asp:Button ID="btnSep" runat="server" Text="SEP" BackColor="Transparent" Font-Bold="true"
                            ForeColor="Black" OnClick="btnSep_OnClick" />
                        <asp:Button ID="btnOct" runat="server" Text="OCT" BackColor="Transparent" Font-Bold="true"
                            ForeColor="Black" OnClick="btnOct_OnClick" />
                    </a></li>
                    <li class="active"></li>
                    <li class="active"><a class="btn btn-sm blue-madison btn-circle">
                        <asp:Button ID="btnNov" runat="server" Text="NOV" BackColor="Transparent" Font-Bold="true"
                            ForeColor="Black" OnClick="btnNov_OnClick" />
                        <asp:Button ID="btnDec" runat="server" Text="DEC" BackColor="Transparent" Font-Bold="true"
                            ForeColor="Black" OnClick="btnDec_OnClick" />
                    </a></li>
                    <li class="active"></li>
                </ul>
                <!-- END SIDEBAR MENU -->
            </div>
        </div>
        <!-- END SIDEBAR -->
        <!-- BEGIN CONTENT -->
        <div class="page-content-wrapper">
            <div class="page-content">
                <div class="page-bar">
                    <div class="page-toolbar">
                        <a class="btn grey-salsa btn-circle btn-sm dropdown-toggle">From :
                            <telerik:RadDatePicker ID="DtFromDate" runat="server" Width="120px" DateInput-EmptyMessage="Date"
                                DbSelectedDate='<%# Bind("Enquiry_Date") %>' DateInput-DateFormat="dd/MMM/yyyy">
                                <Calendar ID="Calendar2" runat="server">
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDatePicker>
                            To :
                            <telerik:RadDatePicker ID="DtToDate" runat="server" Width="120px" DateInput-EmptyMessage="Date"
                                DbSelectedDate='<%# Bind("Enquiry_Date") %>' DateInput-DateFormat="dd/MMM/yyyy">
                                <Calendar ID="Calendar1" runat="server">
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDatePicker>
                        </a><a class="btn btn-sm green-haze btn-circle">
                            <asp:Button ID="btnsubmit" runat="server" Text="Submit" BackColor="Transparent" Font-Bold="true"
                                ForeColor="Black" OnClick="btnsubmit_OnClick" />
                        </a>
                        <div id="dashboard-report-range" class="pull-right tooltips btn btn-fit-height grey-salt ">
                            <%--<a class="btn green-haze btn-circle btn-sm" >--%>
                            <telerik:RadComboBox ID="cboreporttype" runat="server" Height="200px" Width="170px"
                                AutoPostBack="true" OnSelectedIndexChanged="cboreporttype_OnSelectedIndexChanged"
                                AppendDataBoundItems="True">
                                <Items>
                                    <telerik:RadComboBoxItem Text="--Select--" ForeColor="Black" />
                                    <telerik:RadComboBoxItem Text="Today" Value="0" />
                                    <telerik:RadComboBoxItem Text="Yesterday" Value="1" />
                                    <telerik:RadComboBoxItem Text="Last 7 days" Value="2" />
                                    <telerik:RadComboBoxItem Text="This Month" Value="3" />
                                    <telerik:RadComboBoxItem Text="Last Month" Value="4" />
                                    <telerik:RadComboBoxItem Text="This Year" Value="5" />
                                    <telerik:RadComboBoxItem Text="Last Year" Value="6" />
                                </Items>
                            </telerik:RadComboBox>
                            </i>
                            <%--<i class="fa fa-angle-down"></i></a>--%>
                        </div>
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="row">
                    <div class="col-md-6 col-sm-6">
                        <!-- BEGIN SAMPLE TABLE PORTLET-->
                        <div class="portlet box red">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-comments"></i>Quotation
                                </div>
                            </div>
                            <div class="portlet-body">
                                <div class="table-scrollable">
                                    <table class="table table-bordered table-hover">
                                        <tbody>
                                            <tr>
                                                <td style="width: 30%; background-color: #427aaa; color: White" align="center">
                                                    RECEIVED
                                                </td>
                                                <td style="width: 14%; background-color: #427aaa; color: White" align="center">
                                                    Total
                                                </td>
                                                <td style="width: 14%; background-color: #427aaa; color: White" align="center">
                                                    CALB
                                                </td>
                                                <td style="width: 14%; background-color: #427aaa; color: White" align="center">
                                                    INSP
                                                </td>
                                                <td style="width: 14%; background-color: #427aaa; color: White" align="center">
                                                    TRNG
                                                </td>
                                                <td style="width: 14%; background-color: #427aaa; color: White" align="center">
                                                    OTHR
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 30%;">
                                                    NOs.
                                                </td>
                                                <td style="width: 14%;" align="center" class="warning">
                                                    <asp:Label ID="lbltotalrecd" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="warning">
                                                    <asp:Label ID="lblcalib" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="warning">
                                                    <asp:Label ID="lblinspec" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="warning">
                                                    <asp:Label ID="lblTraining" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="warning">
                                                    <asp:Label ID="lblOthers" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 30%;">
                                                    Units.
                                                </td>
                                                <td style="width: 14%;" align="center" class="warning">
                                                    <asp:Label ID="lbltotalrecdunits" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="warning">
                                                    <asp:Label ID="lblcalibunits" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="warning">
                                                    <asp:Label ID="lblinspecunits" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="warning">
                                                    <asp:Label ID="lblTrainingunits" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="warning">
                                                    <asp:Label ID="lblOthersunits" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 30%;">
                                                    RM
                                                </td>
                                                <td style="width: 14%;" align="center" class="warning">
                                                    <asp:Label ID="lbltotalrecdRM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="warning">
                                                    <asp:Label ID="lblcalibunitsRM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="warning">
                                                    <asp:Label ID="lblinspecRM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="warning">
                                                    <asp:Label ID="lblTrainingRM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="warning">
                                                    <asp:Label ID="lblOthersRM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    %
                                                </td>
                                                <td align="center" class="warning">
                                                </td>
                                                <td align="center" class="warning">
                                                    <asp:Label ID="lblcalibPercn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" class="warning">
                                                    <asp:Label ID="lblinspecPercn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" class="warning">
                                                    <asp:Label ID="lblTrainingPercn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" class="warning">
                                                    <asp:Label ID="lblOthersPercn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table class="table table-bordered table-hover">
                                        <tbody>
                                            <tr>
                                                <td align="center" style="width: 30%; background-color: #427aaa; color: White">
                                                    RECEIVED - CAL
                                                </td>
                                                <td align="center" style="width: 14%; background-color: #427aaa; color: White">
                                                    Total
                                                </td>
                                                <td align="center" style="width: 14%; background-color: #427aaa; color: White">
                                                    In Lab
                                                </td>
                                                <td align="center" style="width: 14%; background-color: #427aaa; color: White">
                                                    On Site
                                                </td>
                                                <td align="center" style="width: 14%; background-color: #427aaa; color: White">
                                                    ExtCal
                                                </td>
                                                <td align="center" style="width: 14%; background-color: #427aaa; color: White">
                                                    NC
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 30%;">
                                                    NOs.
                                                </td>
                                                <td align="center" style="width: 14%;" class="danger">
                                                    <asp:Label ID="lbltotrcdcal" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="danger">
                                                    <asp:Label ID="lbltotrcdinlab" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="danger">
                                                    <asp:Label ID="lbltotrcdonsit" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="danger">
                                                    <asp:Label ID="lbltotrcdExtcal" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="danger">
                                                    <asp:Label ID="lbltotrcdNC" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 30%;">
                                                    Units.
                                                </td>
                                                <td style="width: 14%;" align="center" class="danger">
                                                    <asp:Label ID="lbltotrcdcalunits" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="danger">
                                                    <asp:Label ID="lbltotrcdinlabunits" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="danger">
                                                    <asp:Label ID="lbltotrcdonsitunits" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="danger">
                                                    <asp:Label ID="lbltotrcdExtcalunits" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="danger">
                                                    <asp:Label ID="lbltotrcdNCunits" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 30%;">
                                                    RM
                                                </td>
                                                <td style="width: 14%;" align="center" class="danger">
                                                    <asp:Label ID="lbltotrcdcalRM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="danger">
                                                    <asp:Label ID="lbltotrcdinlabRM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="danger">
                                                    <asp:Label ID="lbltotrcdonsitRM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="danger">
                                                    <asp:Label ID="lbltotrcdExtcalRM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="danger">
                                                    <asp:Label ID="lbltotrcdNCRM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    %
                                                </td>
                                                <td align="center" class="danger">
                                                    <asp:Label ID="lbltotrcdcalprcn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" class="danger">
                                                    <asp:Label ID="lbltotrcdinlabprcn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" class="danger">
                                                    <asp:Label ID="lbltotrcdonsitprcn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" class="danger">
                                                    <asp:Label ID="lbltotrcdExtcalprcn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" class="danger">
                                                    <asp:Label ID="lbltotrcdNCprcn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table class="table table-bordered table-hover">
                                        <tbody>
                                            <tr>
                                                <td align="center" style="width: 30%; background-color: #427aaa; color: White">
                                                    STATUS
                                                </td>
                                                <td align="center" style="width: 14%; background-color: #427aaa; color: White">
                                                    Total
                                                </td>
                                                <td align="center" style="width: 14%; background-color: #427aaa; color: White">
                                                    Pend
                                                </td>
                                                <td align="center" style="width: 14%; background-color: #427aaa; color: White">
                                                    UnSu
                                                </td>
                                                <td align="center" style="width: 14%; background-color: #427aaa; color: White">
                                                    Cmpl
                                                </td>
                                                <td align="center" style="width: 14%; background-color: #427aaa; color: White">
                                                    OTHR
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 30%;">
                                                    NOs.
                                                </td>
                                                <td align="center" style="width: 14%;" class="danger">
                                                    <asp:Label ID="lblQuotsttus" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="danger">
                                                    <asp:Label ID="lblQuotPending" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="danger">
                                                    <asp:Label ID="lblQuotComplete" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="danger">
                                                    <asp:Label ID="lblQuotUnsucces" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="danger">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 30%;">
                                                    RM
                                                </td>
                                                <td style="width: 14%;" align="center" class="danger">
                                                    <asp:Label ID="lblQuotsttusRM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="danger">
                                                    <asp:Label ID="lblQuotPendingRM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="danger">
                                                    <asp:Label ID="lblQuotCompleteRM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="danger">
                                                    <asp:Label ID="lblQuotUnsuccesRM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                                <td style="width: 14%;" align="center" class="danger">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    %
                                                </td>
                                                <td align="center" class="danger">
                                                </td>
                                                <td align="center" class="danger">
                                                    <asp:Label ID="lblenqrypendingPercn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" class="danger">
                                                    <asp:Label ID="lblenqryCompletePercn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" class="danger">
                                                    <asp:Label ID="lblenqryunsuccesPercn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" class="danger">
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table class="table table-bordered table-hover">
                                        <tbody>
                                            <tr>
                                                <td align="center" style="width: 30%; background-color: #427aaa; color: White">
                                                    AGEING
                                                </td>
                                                <td align="center" style="width: 14%; background-color: #427aaa; color: White">
                                                    Total
                                                </td>
                                                <td align="center" style="width: 14%; background-color: #427aaa; color: White">
                                                    < 3
                                                </td>
                                                <td align="center" style="width: 14%; background-color: #427aaa; color: White">
                                                    4 - 7
                                                </td>
                                                <td align="center" style="width: 14%; background-color: #427aaa; color: White">
                                                    > 7
                                                </td>
                                                <td align="center" style="width: 14%; background-color: #427aaa; color: White">
                                                    OTHR
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 30%;">
                                                    NOs.
                                                </td>
                                                <td align="center" style="width: 14%;" class="success">
                                                    <asp:Label ID="lblagingtot" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="success">
                                                    <asp:Label ID="lblaging3" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="success">
                                                    <asp:Label ID="lblaging4to7" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="success">
                                                    <asp:Label ID="lblaging7" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="success">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 30%;">
                                                    RM
                                                </td>
                                                <td align="center" style="width: 14%;" class="success">
                                                    <asp:Label ID="lblagingtotRM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="success">
                                                    <asp:Label ID="lblaging3RM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="success">
                                                    <asp:Label ID="lblaging4to7RM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="success">
                                                    <asp:Label ID="lblaging7RM" runat="server" Font-Bold="false" Font-Size="Smaller"
                                                        Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="success">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    %
                                                </td>
                                                <td align="center" style="width: 14%;" class="success">
                                                    <asp:Label ID="lblagingtotpercn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="success">
                                                    <asp:Label ID="lblaging3percn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="success">
                                                    <asp:Label ID="lblaging4to7percn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" style="width: 14%;" class="success">
                                                    <asp:Label ID="lblaging7percn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td align="center" class="success   ">
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <!-- END SAMPLE TABLE PORTLET-->
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <!-- BEGIN SAMPLE TABLE PORTLET-->
                        <div class="portlet box red">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-comments"></i>QUOTATION DEFINITIONS
                                </div>
                                <%--	<div class="tools">
								<a href="javascript:;" class="collapse">
								</a>
								<a href="#portlet-config" data-toggle="modal" class="config">
								</a>
								<a href="javascript:;" class="reload">
								</a>
								<a href="javascript:;" class="remove">
								</a>
							</div>--%>
                            </div>
                            <div class="portlet-body">
                                <div class="table-scrollable">
                                    <table class="table table-bordered table-hover">
                                        <tbody>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    1
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                    CALB
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    Calibration
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    2
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                    INSP
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    Inspection
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    3
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                    TRNG
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    Training
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    4
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                    OTHR
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    OTHERS
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    5
                                                </td>
                                                <td style="width: 30%;" align="center" class="danger">
                                                    Pend
                                                </td>
                                                <td style="width: 60%;" align="center" class="danger">
                                                    Pending.Ageing define as the duration from date Received until Quotation becomes
                                                    Completed or unsuccessful
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    6
                                                </td>
                                                <td style="width: 30%;" align="center" class="danger">
                                                    UnSu
                                                </td>
                                                <td style="width: 60%;" align="center" class="danger">
                                                    UnSuccessful QUotation
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    7
                                                </td>
                                                <td style="width: 30%;" align="center" class="danger">
                                                    CMPL
                                                </td>
                                                <td style="width: 60%;" align="center" class="danger">
                                                    Quotation which turns into orders
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <!-- END SAMPLE TABLE PORTLET-->
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="row">
                    <div class="col-md-6 col-sm-6">
                        <!-- BEGIN SAMPLE TABLE PORTLET-->
                        <div class="portlet box green">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-comments"></i>QUOTATION RECEIVED
                                </div>
                                <%--	<div class="tools">
								<a href="javascript:;" class="collapse">
								</a>
								<a href="#portlet-config" data-toggle="modal" class="config">
								</a>
								<a href="javascript:;" class="reload">
								</a>
								<a href="javascript:;" class="remove">
								</a>
							</div>--%>
                            </div>
                            <div class="portlet-body">
                                <div class="table-scrollable">
                                    <table class="table table-bordered table-hover">
                                        <tbody>
                                            <tr>
                                                <td align="center" style="width: 34%; background-color: #427aaa; color: White">
                                                    TYPE
                                                </td>
                                                <td align="center" style="width: 33%; background-color: #427aaa; color: White">
                                                    %
                                                </td>
                                                <td align="center" style="width: 33%; background-color: #427aaa; color: White">
                                                    NOs
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 34%;">
                                                    CALB
                                                </td>
                                                <td style="width: 33%;" align="center" class="warning">
                                                    <asp:Label ID="lblclibrcdindperc" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 33%;" align="center" class="warning">
                                                    <asp:Label ID="lblclibrcdind" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    INSP
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                    <asp:Label ID="lblInsprcdindperc" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="lblInsprcdind" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    TRNG
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                    <asp:Label ID="lbltrngrcdindperc" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="lbltrngrcdind" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    OTHR
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                    <asp:Label ID="lblOthrrcdindperc" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="lblothrrcdind" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    TOTAL
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="lbltotRecd" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <a class="more" href="#">
                                        <asp:Button ID="btnenqry" runat="server" Text="View More" OnClick="btneQuot_Onclick" />
                                        <i class="m-icon-swapright m-icon-white"></i></a>
                                </div>
                            </div>
                        </div>
                        <!-- END SAMPLE TABLE PORTLET-->
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <cc1:StiWebViewer ID="StiWebViewer1" runat="server" RenderMode="Standard" Width="400px"
                            Height="300" BackColor="#aeb6f7" Visible="true" XpsShowDialog="false" ShowToolBar="false" />
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="row">
                    <div class="col-md-6 col-sm-6">
                        <!-- BEGIN SAMPLE TABLE PORTLET-->
                        <div class="portlet box green">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-comments"></i>QUOTATION RECEIVED - CAL
                                </div>
                                <%--	<div class="tools">
								<a href="javascript:;" class="collapse">
								</a>
								<a href="#portlet-config" data-toggle="modal" class="config">
								</a>
								<a href="javascript:;" class="reload">
								</a>
								<a href="javascript:;" class="remove">
								</a>
							</div>--%>
                            </div>
                            <div class="portlet-body">
                                <div class="table-scrollable">
                                    <table class="table table-bordered table-hover">
                                        <tbody>
                                            <tr>
                                                <td align="center" style="width: 34%; background-color: #427aaa; color: White">
                                                    TYPE
                                                </td>
                                                <td align="center" style="width: 33%; background-color: #427aaa; color: White">
                                                    %
                                                </td>
                                                <td align="center" style="width: 33%; background-color: #427aaa; color: White">
                                                    NOs
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 34%;">
                                                    Inlab
                                                </td>
                                                <td style="width: 33%;" align="center" class="warning">
                                                    <asp:Label ID="lbltotrcdinlabprcn1" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 33%;" align="center" class="warning">
                                                    <asp:Label ID="lbltotrcdinlab1" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    On site
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                    <asp:Label ID="lbltotrcdonsitprcn1" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="lbltotrcdonsit1" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    Ext Cal
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                    <asp:Label ID="lbltotrcdExtcalprcn1" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="lbltotrcdExtcal1" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    NC
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                    <asp:Label ID="lbltotrcdNCprcn1" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="lbltotrcdNC1" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    TOTAL
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="lbltotrcd1" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <a class="more" href="#">
                                        <asp:Button ID="Button1" runat="server" Text="View More" OnClick="btneQuotrecdcal_Onclick" />
                                        <i class="m-icon-swapright m-icon-white"></i></a>
                                </div>
                            </div>
                        </div>
                        <!-- END SAMPLE TABLE PORTLET-->
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <cc1:StiWebViewer ID="StiWebViewer5" runat="server" RenderMode="Standard" Width="400px"
                            Height="300" BackColor="#aeb6f7" Visible="true" XpsShowDialog="false" ShowToolBar="false" />
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="row">
                    <div class="col-md-6 col-sm-6">
                        <!-- BEGIN SAMPLE TABLE PORTLET-->
                        <div class="portlet box purple">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-comments"></i>QUOTATION STATUS
                                </div>
                                <%--	<div class="tools">
								<a href="javascript:;" class="collapse">
								</a>
								<a href="#portlet-config" data-toggle="modal" class="config">
								</a>
								<a href="javascript:;" class="reload">
								</a>
								<a href="javascript:;" class="remove">
								</a>
							</div>--%>
                            </div>
                            <div class="portlet-body">
                                <div class="table-scrollable">
                                    <table class="table table-bordered table-hover">
                                        <tbody>
                                            <tr>
                                                <td align="center" style="width: 34%; background-color: #427aaa; color: White">
                                                    TYPE
                                                </td>
                                                <td align="center" style="width: 33%; background-color: #427aaa; color: White">
                                                    %
                                                </td>
                                                <td align="center" style="width: 33%; background-color: #427aaa; color: White">
                                                    NOs
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 34%;">
                                                    PEND
                                                </td>
                                                <td style="width: 33%;" align="center" class="warning">
                                                    <asp:Label ID="lblpendstspercn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 33%;" align="center" class="warning">
                                                    <asp:Label ID="lblpendsts" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    UnSu
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                    <asp:Label ID="lblUnSustspercn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="lblUnSusts" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    CMPL
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                    <asp:Label ID="lblCmplstspercn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="lblCmplsts" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    TOTAL
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="lblTotstatus" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <a class="more" href="#">
                                        <asp:Button ID="btnenqyststs" runat="server" Text="View More" OnClick="btnenqyststs_Onclick" />
                                        <i class="m-icon-swapright m-icon-white"></i></a>
                                </div>
                            </div>
                        </div>
                        <!-- END SAMPLE TABLE PORTLET-->
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <cc1:StiWebViewer ID="StiWebViewer2" runat="server" RenderMode="Standard" Width="400px"
                            Height="280" BackColor="#aeb6f7" Visible="true" XpsShowDialog="false" ShowToolBar="false" />
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="row">
                    <div class="col-md-6 col-sm-6">
                        <!-- BEGIN SAMPLE TABLE PORTLET-->
                        <div class="portlet box green">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-comments"></i>QUOTATION AGEING
                                </div>
                                <%--	<div class="tools">
								<a href="javascript:;" class="collapse">
								</a>
								<a href="#portlet-config" data-toggle="modal" class="config">
								</a>
								<a href="javascript:;" class="reload">
								</a>
								<a href="javascript:;" class="remove">
								</a>
							</div>--%>
                            </div>
                            <div class="portlet-body">
                                <div class="table-scrollable">
                                    <table class="table table-bordered table-hover">
                                        <tbody>
                                            <tr>
                                                <td align="center" style="width: 34%; background-color: #427aaa; color: White">
                                                    TYPE
                                                </td>
                                                <td align="center" style="width: 33%; background-color: #427aaa; color: White">
                                                    %
                                                </td>
                                                <td align="center" style="width: 33%; background-color: #427aaa; color: White">
                                                    NOs
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 34%;">
                                                    < 3
                                                </td>
                                                <td style="width: 33%;" align="center" class="warning">
                                                    <asp:Label ID="lblaging3IndPercn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 33%;" align="center" class="warning">
                                                    <asp:Label ID="lblaging3Ind" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    4 - 7
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                    <asp:Label ID="lblaging4to7Indpercn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="lblaging4to7Ind" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    > 7
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                    <asp:Label ID="lblaging7Indpercn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="lblaging7Ind" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    TOTAL
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="Label9" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <a class="more" href="#">
                                        <asp:Button ID="btnenqryAging" runat="server" Text="View More" OnClick="btnenqryAging_Onclick"
                                            Visible="false" />
                                        <i class="m-icon-swapright m-icon-white"></i></a>
                                </div>
                            </div>
                        </div>
                        <!-- END SAMPLE TABLE PORTLET-->
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <cc1:StiWebViewer ID="StiWebViewer3" runat="server" RenderMode="Standard" Width="400px"
                            Height="280" BackColor="#aeb6f7" Visible="true" XpsShowDialog="false" ShowToolBar="false" />
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="row">
                    <div class="col-md-6 col-sm-6">
                        <!-- BEGIN SAMPLE TABLE PORTLET-->
                          <div class="portlet box purple">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-comments"></i>QUOTATION DISCOUNT
                                </div>
                                <%--	<div class="tools">
								<a href="javascript:;" class="collapse">
								</a>
								<a href="#portlet-config" data-toggle="modal" class="config">
								</a>
								<a href="javascript:;" class="reload">
								</a>
								<a href="javascript:;" class="remove">
								</a>
							</div>--%>
                            </div>
                            <div class="portlet-body">
                                <div class="table-scrollable">
                                    <table class="table table-bordered table-hover">
                                        <tbody>
                                            <tr>
                                                <td align="center" style="width: 34%; background-color: #427aaa; color: White">
                                                    Discount
                                                </td>
                                                <td align="center" style="width: 33%; background-color: #427aaa; color: White">
                                                    %
                                                </td>
                                                <td align="center" style="width: 33%; background-color: #427aaa; color: White">
                                                    NOs
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 34%;">
                                                    NO Disc
                                                </td>
                                                <td style="width: 33%;" align="center" class="warning">
                                                    <asp:Label ID="lblnodiscprcn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 33%;" align="center" class="warning">
                                                    <asp:Label ID="lblnodis" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                  < 10 %
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                    <asp:Label ID="lbl10percn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="lbldisc10" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    11 to 20 %
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                    <asp:Label ID="lbl11to20percn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="lbldisc11to20" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    > 20 %
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                    <asp:Label ID="lbl20prcn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="lbldisc20" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 10%;">
                                                    TOTAL
                                                </td>
                                                <td style="width: 30%;" align="center" class="warning">
                                                </td>
                                                <td style="width: 60%;" align="center" class="warning">
                                                    <asp:Label ID="lbltotdisc" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <a class="more" href="#">
                                        <asp:Button ID="Button2" runat="server" Text="View More" OnClick="btnenqyststs_Onclick" />
                                        <i class="m-icon-swapright m-icon-white"></i></a>
                                </div>
                            </div>
                        </div>
                        
                        <!-- END SAMPLE TABLE PORTLET-->
                    </div>
                    <div class="col-md-6 col-sm-6">
                   <div class="portlet box green">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-comments"></i>QUOTATION BY STAFF
                                </div>
                                <%--	<div class="tools">
								<a href="javascript:;" class="collapse">
								</a>
								<a href="#portlet-config" data-toggle="modal" class="config">
								</a>
								<a href="javascript:;" class="reload">
								</a>
								<a href="javascript:;" class="remove">
								</a>
							</div>--%>
                            </div>
                            <div class="portlet-body">
                                <div class="table-scrollable">
                                    <asp:GridView ID="Grdbystaff" runat="server" Width="100%">
                                        <HeaderStyle BackColor="#89A0FE" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="row">
                    <div class="col-md-6 col-sm-6">
                        <div class="portlet light bordered">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="icon-share font-blue-steel"></i><span class="caption-subject font-blue-steel ">
                                        Recent Quotation Activities</span>
                                </div>
                            </div>
                            <div class="portlet-body">
                                <div class="scroller" style="height: 300px;" data-always-visible="1" data-rail-visible="0">
                                    <ul class="feeds">
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-info">
                                                            <i class="fa fa-check"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">
                                                            <asp:Label ID="lblrcntactvtsQuot1" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                            --- ><span class="label label-sm label-success ">
                                                                <asp:Label ID="lblrcntstatusQuot1" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                                <i class="fa fa-share"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">
                                                    <asp:Label ID="lblrcntdateQuot1" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-success">
                                                            <i class="fa fa-bar-chart-o"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">
                                                            <asp:Label ID="lblrcntactvtsQuot2" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                            --- ><span class="label label-sm label-success "><asp:Label ID="lblrcntstatusQuot2"
                                                                runat="server" Font-Bold="true" Text=""></asp:Label>
                                                                <i class="fa fa-share"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">
                                                    <asp:Label ID="lblrcntdateQuot2" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-danger">
                                                            <i class="fa fa-user"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">
                                                            <asp:Label ID="lblrcntactvtsQuot3" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                            --- > <span class="label label-sm label-success ">
                                                                <asp:Label ID="lblrcntstatusQuot3" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                                <i class="fa fa-share"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">
                                                    <asp:Label ID="lblrcntdateQuot3" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-info">
                                                            <i class="fa fa-shopping-cart"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">
                                                            <asp:Label ID="lblrcntactvtsQuot4" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                            --- > <span class="label label-sm label-success ">
                                                                <asp:Label ID="lblrcntstatusQuot4" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                                <i class="fa fa-share"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">
                                                    <asp:Label ID="lblrcntdateQuot4" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-success">
                                                            <i class="fa fa-user"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">
                                                            <asp:Label ID="lblrcntactvtsQuot5" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                            --- > <span class="label label-sm label-success ">
                                                                <asp:Label ID="lblrcntstatusQuot5" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                                <i class="fa fa-share"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">
                                                    <asp:Label ID="lblrcntdateQuot5" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                                <div class="scroller-footer">
                                    <div class="btn-arrow-link pull-right">
                                        <a href="#">See All Records</a> <i class="icon-arrow-right"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clearfix">
                </div>
            </div>
        </div>
        <!-- END CONTENT -->
        <!-- BEGIN QUICK SIDEBAR -->
        <a href="javascript:;" class="page-quick-sidebar-toggler"><i class="icon-close"></i>
        </a>
        <!-- END QUICK SIDEBAR -->
    </div>
    <!-- END CONTAINER -->
    <!-- BEGIN FOOTER -->
    <!-- END FOOTER -->
    <!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->
    <!-- BEGIN CORE PLUGINS -->
    <!--[if lt IE 9]>
<script src="assets/global/plugins/respond.min.js"></script>
<script src="assets/global/plugins/excanvas.min.js"></script> 
<![endif]-->
    <script src="C:/inetpub/wwwroot/SirimNew/assets/global/plugins/jquery-1.11.0.min.js"
        type="text/javascript"></script>
    <script src="C:/inetpub/wwwroot/SirimNew/assets/global/plugins/jquery-migrate-1.2.1.min.js"
        type="text/javascript"></script>
    <!-- IMPORTANT! Load jquery-ui-1.10.3.custom.min.js before bootstrap.min.js to fix bootstrap tooltip conflict with jquery ui tooltip -->
    <script src="C:/inetpub/wwwroot/SirimNew/assets/global/plugins/jquery-ui/jquery-ui-1.10.3.custom.min.js"
        type="text/javascript"></script>
    <script src="C:/inetpub/wwwroot/SirimNew/assets/global/plugins/bootstrap/js/bootstrap.min.js"
        type="text/javascript"></script>
    <script src="C:/inetpub/wwwroot/SirimNew/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js"
        type="text/javascript"></script>
    <script src="C:/inetpub/wwwroot/SirimNew/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js"
        type="text/javascript"></script>
    <script src="C:/inetpub/wwwroot/SirimNew/assets/global/plugins/jquery.blockui.min.js"
        type="text/javascript"></script>
    <script src="C:/inetpub/wwwroot/SirimNew/assets/global/plugins/jquery.cokie.min.js"
        type="text/javascript"></script>
    <script src="assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="assets/global/plugins/jqvmap/jqvmap/jquery.vmap.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.russia.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.world.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.europe.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.germany.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.usa.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jqvmap/jqvmap/data/jquery.vmap.sampledata.js"
        type="text/javascript"></script>
    <script src="assets/global/plugins/flot/jquery.flot.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/flot/jquery.flot.resize.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/flot/jquery.flot.categories.min.js" type="text/javascript"></script>
    <script src="C:/inetpub/wwwroot/SirimNew/assets/global/plugins/jquery.pulsate.min.js"
        type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-daterangepicker/moment.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.js"
        type="text/javascript"></script>
    <!-- IMPORTANT! fullcalendar depends on jquery-ui-1.10.3.custom.min.js for drag & drop support -->
    <script src="assets/global/plugins/fullcalendar/fullcalendar/fullcalendar.min.js"
        type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-easypiechart/jquery.easypiechart.min.js"
        type="text/javascript"></script>
    <script src="C:/inetpub/wwwroot/SirimNew/assets/global/plugins/jquery.sparkline.min.js"
        type="text/javascript"></script>
    <script src="assets/global/plugins/gritter/js/jquery.gritter.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="assets/global/scripts/metronic.js" type="text/javascript"></script>
    <script src="assets/admin/layout/scripts/layout.js" type="text/javascript"></script>
    <script src="assets/admin/layout/scripts/quick-sidebar.js" type="text/javascript"></script>
    <script src="assets/admin/layout/scripts/demo.js" type="text/javascript"></script>
    <script src="assets/admin/pages/scripts/index.js" type="text/javascript"></script>
    <script src="assets/admin/pages/scripts/tasks.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <script src="http://code.jquery.com/jquery-1.10.2.js" type="text/javascript">
        jQuery(document).ready(function () {
            Metronic.init(); // init metronic core componets
            Layout.init(); // init layout
            QuickSidebar.init(); // init quick sidebar
            Demo.init(); // init demo features
            Index.init();
            Index.initDashboardDaterange();
            Index.initJQVMAP(); // init index page's custom scripts
            Index.initCalendar(); // init index page's custom scripts
            Index.initCharts(); // init index page's custom scripts
            Index.initChat();
            Index.initMiniCharts();
            Index.initIntro();
            Tasks.initDashboardWidget();
        });
    </script>
    <!-- END JAVASCRIPTS -->
    </form>
</body>
</html>
