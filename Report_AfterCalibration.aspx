<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report_AfterCalibration.aspx.cs" Inherits="Report_AfterCalibration" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2011.2.1100.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a"
    Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <title>Report AfterCalibration</title>
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
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <table border="0" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td style="border-right: blue thin solid; border-top: blue thin solid; border-left: blue thin solid;
                    border-bottom: blue thin solid; border-width: 0px" align="center">
                    <table border="0" width="100%">
                         <tr>
                            <td align="center" style="width: 80%;">
                                <div>
                                    <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                                </div>
                            </td>
                            <td align="center" style="width: 20%;">
                                <div style="height: 20px;">
                                    <asp:Label class="labelstyle" ID="lblUserInfo" Visible="false" Font-Size="Small"
                                        runat="server" ForeColor="#e4cd87" Font-Bold="true" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;">
                                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                                <div class="text" style="background-color: White; width: 100%; text-align: right;">
                            <asp:Button ID="Button3" Width="60px" runat="server" Text="Email" OnClick="btnEmail_Click" Visible="false" />
                                    <%--                                    <asp:Button ID="btnBack" runat="server" Visible="true" Class="buttonstyle" Text="Back"
                                        Width="100px" OnClick="btnBack_Click" />--%>
                                   <%-- <asp:Button ID="btnPDF" runat="server" Text="Click Here to View/Print" Width="160px"
                                        OnClick="btnPDF_Click" Visible="false" />--%>
                                </div>
                                <div id="Div11" runat="server" style="width: 100%; background-color: transparent ;text-align:center;
                                    ">
                                         
                                    <cc1:StiWebViewer ID="StiWebViewer1" ToolbarAlignment="Center" runat="server" RenderMode="Standard"
                                        Width="1000px" Height="600px"  ScrollBarsMode="true" BackColor="#aeb6f7" Visible="true" />
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
