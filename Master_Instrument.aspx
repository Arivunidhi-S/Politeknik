<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Master_Instrument.aspx.cs"
    Inherits="Master_Instrument" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Standard Equipment Master</title>
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
                                    STANDARD CALIBRATION MASTER
                                </div>
                                 <div style="height: 20px;">
                                <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                            </div>
                                <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" OnNeedDataSource="RadGrid1_NeedDataSource"
                                    GridLines="Vertical" AllowPaging="True" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom"
                                    OnItemDataBound="RadGrid1_ItemDataBound" OnDeleteCommand="RadGrid1_DeleteCommand" Skin="Hay" PageSize="20"
                                    AllowAutomaticUpdates="true" AllowAutomaticInserts="true" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                                    AllowAutomaticDeletes="true" OnInsertCommand="RadGrid1_InsertCommand" AllowSorting="true"
                                    AllowFilteringByColumn="true" OnUpdateCommand="RadGrid1_UpdateCommand">
                                     <ClientSettings EnableRowHoverStyle="true" >
                                        
                                        </ClientSettings>
                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="Calib_std_Id" CommandItemDisplay="Top"
                                        CommandItemSettings-AddNewRecordText="Add New Instrument Details">
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                <HeaderStyle Width="5%" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn DataField="Calib_std_Id" DataType="System.Int64" HeaderText="ID"
                                                ReadOnly="True" SortExpression="Calib_std_Id" UniqueName="Calib_std_Id" AllowFiltering="false"
                                                AllowSorting="false" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Dept_Name" DataType="System.String" HeaderText="Dept Name"
                                                SortExpression="Dept_Name" UniqueName="Dept_Name">
                                                <HeaderStyle Width="6%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Instrument_Name" DataType="System.String" HeaderText="Instrument Name"
                                                SortExpression="Instrument_Name" UniqueName="Instrument_Name">
                                                <HeaderStyle Width="15%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Serial_No" DataType="System.String" HeaderText="Serial No"
                                                SortExpression="Serial_No" UniqueName="Serial_No" Visible="true">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Certificate_No" DataType="System.String" HeaderText="Certificate No"
                                                SortExpression="Certificate_No" UniqueName="Certificate_No">
                                                <HeaderStyle Width="6%" />
                                            </telerik:GridBoundColumn>                                            
                                           
                                            <telerik:GridBoundColumn DataField="Valid_Duration" DataType="System.Int32" HeaderText="Valid Duration"
                                                Visible="false" SortExpression="Valid_Duration" UniqueName="Valid_Duration">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Traceability" DataType="System.String" HeaderText="Traceability"
                                                SortExpression="Traceability" UniqueName="Traceability">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Instrument_Desc" DataType="System.String" HeaderText="Description"
                                                SortExpression="Instrument_Desc" UniqueName="Instrument_Desc" Visible="true">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Remarks" DataType="System.String" HeaderText="Remarks"
                                                SortExpression="Remarks" UniqueName="Remarks">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                             <telerik:GridDateTimeColumn SortExpression="Due_Date" HeaderText="Due Date" HeaderButtonType="TextButton"
                                                DataField="Due_Date" UniqueName="Due_Date" PickerType="DatePicker" DataType="System.DateTime"
                                                DataFormatString="{0:dd/MMM/yyyy}" Aggregate="Last" FooterAggregateFormatString="Last order dates: {0:d}">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridDateTimeColumn>
                                             <telerik:GridDateTimeColumn SortExpression="CALIB_DATE" HeaderText="Calib Date" HeaderButtonType="TextButton"
                                                DataField="CALIB_DATE" UniqueName="CALIB_DATE" PickerType="DatePicker" DataType="System.DateTime"
                                                DataFormatString="{0:dd/MMM/yyyy}" Aggregate="Last" FooterAggregateFormatString="Last order dates: {0:d}">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridDateTimeColumn>
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
                                                                <asp:Label ID="lblCaliStdID" Visible="true" runat="server" Width="20px" Text='<%# Eval("Calib_std_Id")%>' />
                                                            </b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Instrument Name:
                                                            <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red" Visible="true" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblExistingname" runat="server" Text='<%# Eval("Instrument_Name") %>'
                                                                Visible="false" />
                                                            <asp:TextBox Width="200px" ID="txtInstrmntName" MaxLength="150" ToolTip="Maximum Length: 200"
                                                                runat="server" Text='<%# Bind("Instrument_Name") %>'></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="DeptcodeValidator" ControlToValidate="txtInstrmntName"
                                                                ErrorMessage="Instrument Name is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Department Name:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboDeptId" runat="server" Height="250px" Width="310px" AutoPostBack="true" Skin="Hay"
                                                                DataValueField="Dept_ID" OnItemsRequested="cboDeptId_OnItemsRequested" EnableLoadOnDemand="true"
                                                                AppendDataBoundItems="True">
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
                                                                            <td style="width: 140px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['Dept_Code']")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Certificate No:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtCertificateNo" MaxLength="150" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Certificate_No") %>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Serial No:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtSerialNo" MaxLength="150" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Serial_No") %>'></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Traceability :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtTraceability" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Traceability") %>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Description:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtDescription" MaxLength="20" ToolTip="Maximum Length: 20"
                                                                runat="server" Text='<%# Bind("Instrument_Desc") %>'></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Remarks:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtRemarks" MaxLength="200" ToolTip="Maximum Length: 200"
                                                                runat="server" Text='<%# Bind("Remarks") %>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                     <td>
                                                            Calibration Date:
                                                            <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red" Visible="true" />
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dtCalibrationDate" runat="server" Width="100px" DateInput-EmptyMessage="Date" Skin="Hay"
                                                                DbSelectedDate='<%# Bind("CALIB_DATE") %>' DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar1" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="dtCalibrationDate"
                                                                ErrorMessage="Calibration Date is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            Due Date:
                                                            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red" Visible="true" />
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="DtTestDate" runat="server" Width="100px" DateInput-EmptyMessage="Date" Skin="Hay"
                                                                DbSelectedDate='<%# Bind("Due_Date") %>' DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar2" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="DtTestDate"
                                                                ErrorMessage="Due Date is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
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
