<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AMS | Main Page</title>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
   
    <link rel="shortcut icon" href="images/Calib.png" />
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link rel="stylesheet" href="css/modal.css" type="text/css" />
    <link rel="stylesheet" href="css/menu_core.css" type="text/css" />
    <link rel="stylesheet" href="css/skins/menu_simpleAnimated.css" type="text/css" />
    <link rel="stylesheet" href="JS/styles.css" />
    <script src="JS/jquery-latest.min.js" type="text/javascript"></script>
    <!-- Start section -->
<%--    <link rel="stylesheet" type="text/css" href="css/default.css" />--%>
    <link rel="stylesheet" type="text/css" href="css/component.css" />
    <script type="text/javascript" src="js/modernizr.custom.js"></script>
    <!-- End section -->
    <%--<script type="text/javascript" src="js/api.js"></script>--%>
    <!--Custom Styles-->
    <style type="text/css">
        .myTitle
        {
            color: #333;
            font-family: arial;
            font-weight: normal;
            font-size: 10px;
            margin: 20px 0px 5px 0px;
        }
        .myTitleTop
        {
            margin: 5px 0px;
        }
        .box
        {
            box-shadow: 0.2px 0.2px 8px 0.2px;
        }
        body
        {
            background-image: url(images/bg.jpg); /*You will specify your image path here.*/
            -moz-background-size: cover;
            -webkit-background-size: cover;
            background-size: cover;
            background-position: top center !important;
            background-repeat: no-repeat !important;
            background-attachment: fixed;
        }
   </style>
</head>
<body class="body">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <table>
        <tr>
            <td>
                <table border="0" width="1355px" align="center">
                    <tr>
                         <td id="Td2" align="left" runat="server" colspan="2">
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

                                      <%-- <li class="last">--%>
                                     <div style="text-align: right; vertical-align: middle; font-family: 'Aclonica', serif;
                                        color: #fff; text-shadow: 0px 1px 0px #999, 0px 2px 0px #888, 0px 3px 0px #777, 0px 4px 0px #666, 0px 5px 0px #555, 0px 6px 0px #444, 0px 7px 0px #333, 0px 8px 7px #001135;">
                                        <asp:Label ID="lblname" runat="server" Font-Bold="true" /></div>
                               <%-- </li>--%>
                                </ul>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="DivHeader" runat="server" Style="text-align:center">
                               
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <!-- Start section-->
                           <div class="container">
        <!-- Codrops top bar -->
       
        <div class="os-phrases" id="os-phrases">
            <h2>
                AMS</h2>
            <h2>
                Asset</h2>
            <h2>
                Management</h2>
            <h2>
                System</h2>
            <%--<h2>
                Asset Management System</h2>--%>
            <!--<h2>This fall</h2>
				<h2>Prepare</h2>
				<h2>Refresh to replay</h2>-->
        </div>
    </div>
    <!-- /container -->
    <script type="text/javascript"  src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery.lettering.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#os-phrases > h2").lettering('words').children("span").lettering().children("span").lettering();
        })
		</script>
                            <!-- End section -->
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
