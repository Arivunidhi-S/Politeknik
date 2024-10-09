<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Master_Equipment.aspx.cs"
    Inherits="Master_Equipment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Equipment Master</title>
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
                                    EQUIPMENT MASTER
                                </div>
                                <div style="height: 20px; text-align: center">
                                    <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                                </div>
                                <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" OnNeedDataSource="RadGrid1_NeedDataSource"
                                    GridLines="Vertical" AllowPaging="True" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom"
                                    OnItemDataBound="RadGrid1_ItemDataBound" OnDeleteCommand="RadGrid1_DeleteCommand" PageSize="20"
                                    Skin="Hay" AllowAutomaticUpdates="true" AllowAutomaticInserts="true" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                                    AllowAutomaticDeletes="true" OnInsertCommand="RadGrid1_InsertCommand" AllowSorting="true"
                                    AllowFilteringByColumn="true" OnUpdateCommand="RadGrid1_UpdateCommand">
                                    <ClientSettings EnableRowHoverStyle="true">
                                    </ClientSettings>
                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="EQUIPMENT_ID" CommandItemDisplay="Top"
                                        CommandItemSettings-AddNewRecordText="Add New Equipment Details">
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                <HeaderStyle Width="5%" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn DataField="EQUIPMENT_NAME" DataType="System.String" HeaderText="Equipment Name"
                                                SortExpression="EQUIPMENT_NAME" UniqueName="EQUIPMENT_NAME">
                                                <HeaderStyle Width="18%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Short_Name" DataType="System.String" HeaderText="Lab"
                                                Visible="true" SortExpression="Short_Name" UniqueName="Short_Name">
                                                <HeaderStyle Width="5%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MAKER" DataType="System.String" HeaderText="Maker"
                                                SortExpression="MAKER" UniqueName="MAKER" Visible="true">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Model" DataType="System.String" HeaderText="Model"
                                                SortExpression="Model" UniqueName="Model">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CAL_PROC_NO" DataType="System.Int32" HeaderText="Cal_Proc_No"
                                                Visible="false" SortExpression="CAL_PROC_NO" UniqueName="CAL_PROC_NO">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <%-- <telerik:GridBoundColumn DataField="MU" DataType="System.String" HeaderText="MU"
                                                SortExpression="MU" UniqueName="MU">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn DataField="Ranges" DataType="System.String" HeaderText="Range"
                                                SortExpression="Ranges" UniqueName="Ranges" Visible="true">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="fee" DataType="System.Decimal" HeaderText="Price"
                                                SortExpression="fee" UniqueName="fee" Visible="true">
                                                <HeaderStyle Width="5%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Remarks" DataType="System.String" HeaderText="Remarks"
                                                AllowFiltering="false" SortExpression="Remarks" UniqueName="Remarks">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Flag_price" DataType="System.String" HeaderText="Flag_price"
                                                SortExpression="Flag_price" UniqueName="Flag_price" Visible="false">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
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
                                                                <asp:Label ID="lblEquipID" Visible="true" runat="server" Width="20px" Text='<%# Eval("EQUIPMENT_ID")%>' />
                                                            </b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Equipment Name :
                                                            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red" Visible="true" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtEquipName" MaxLength="150" ToolTip="Maximum Length: 200"
                                                                runat="server" Text='<%# Bind("EQUIPMENT_NAME") %>'></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtEquipName"
                                                                ErrorMessage="Due Date is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            Equipment No:
                                                            <%-- <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red" Visible="true" />--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblExistingname" runat="server" Text='<%# Eval("EQUIPMENT_NO") %>'
                                                                Visible="false" />
                                                            <asp:TextBox Width="200px" ID="txtEquipNo" MaxLength="150" ToolTip="Maximum Length: 200"
                                                                runat="server" Text='<%# Bind("EQUIPMENT_NO") %>'></asp:TextBox>
                                                            <%--  <asp:RequiredFieldValidator runat="server" ID="DeptcodeValidator" ControlToValidate="txtEquipNo"
                                                                ErrorMessage="Instrument Name is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Maker:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtMaker" MaxLength="250" ToolTip="Maximum Length: 250"
                                                                TextMode="SingleLine" runat="server" Text='<%# Bind("Maker") %>'></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Model:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtModel" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Model") %>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Cal_Proc_No:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtCalprocno" MaxLength="150" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Cal_Proc_No") %>'></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <%--MU :--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtMU" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("MU") %>' Visible="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Range:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtRange" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Ranges") %>'></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Class:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtClass" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Class") %>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Price:
                                                        </td>
                                                        <td>
                                                            <telerik:RadNumericTextBox Width="200px" ID="txtNumFee" NumberFormat-DecimalDigits="2"
                                                                ToolTip="Decimal 2 Points" runat="server" Text='<%# Bind("Fee") %>'>
                                                            </telerik:RadNumericTextBox>
                                                            <%--<asp:TextBox Width="200px" ID="txtDescription" MaxLength="20" ToolTip="Maximum Length: 20"
                                                                runat="server" Text='<%# Bind("Instrument_Desc") %>'></asp:TextBox>--%>
                                                        </td>
                                                        <td>
                                                            <%--Category :--%>
                                                        </td>
                                                        <td>
                                                            <%--  <telerik:RadComboBox ID="cboCategoryId" runat="server" Height="90px" Width="300px"
                                                                AutoPostBack="true" DataValueField="Category_ID" OnItemsRequested="cboCategoryId_OnItemsRequested"
                                                                EnableLoadOnDemand="true" AppendDataBoundItems="True">
                                                                <HeaderTemplate>
                                                                    <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 160px;">
                                                                                Category Name
                                                                            </td>
                                                                            <td style="width: 140px;">
                                                                                Remarks
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
                                                                                <%# DataBinder.Eval(Container, "Attributes['Remarks']")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Remarks:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtRemarks" MaxLength="500" ToolTip="Maximum Length: 500"
                                                                TextMode="MultiLine" runat="server" Text='<%# Bind("Remarks") %>'></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Additional Price:
                                                        </td>
                                                        <td>
                                                            <telerik:RadNumericTextBox Width="200px" ID="txtAdditionalprc" NumberFormat-DecimalDigits="2"
                                                                ToolTip="Decimal 2 Points" runat="server" Text='<%# Bind("Additional_Price") %>'>
                                                            </telerik:RadNumericTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table cellspacing="1" cellpadding="1" width="100%" border="0">
                                                    <tr>
                                                        <td width="10%">
                                                            <asp:Label ID="lbllab1" runat="server" Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="ChkLab1" runat="server" BorderColor="Salmon" Visible="false" />
                                                        </td>
                                                        <td width="10%">
                                                            <asp:Label ID="lbllab2" runat="server" Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="ChkLab2" runat="server" BorderColor="Salmon" Visible="false" />
                                                        </td>
                                                        <td width="10%">
                                                            <asp:Label ID="lbllab3" runat="server" Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="ChkLab3" runat="server" BorderColor="Salmon" Visible="false" />
                                                        </td>
                                                        <td width="10%">
                                                            <asp:Label ID="lbllab4" runat="server" Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="ChkLab4" runat="server" BorderColor="Salmon" Visible="false" />
                                                        </td>
                                                        <td width="10%">
                                                            <asp:Label ID="lbllab5" runat="server" Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="ChkLab5" runat="server" BorderColor="Salmon" Visible="false" />
                                                        </td>
                                                        <td width="10%">
                                                            <asp:Label ID="lbllab6" runat="server" Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="ChkLab6" runat="server" BorderColor="Salmon" Visible="false" />
                                                        </td>
                                                        <td width="10%">
                                                            <asp:Label ID="lbllab7" runat="server" Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="ChkLab7" runat="server" BorderColor="Salmon" Visible="false" />
                                                        </td>
                                                        <td width="10%">
                                                            <asp:Label ID="lbllab8" runat="server" Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="ChkLab8" runat="server" BorderColor="Salmon" Visible="false" />
                                                        </td>
                                                        <td width="10%">
                                                            <asp:Label ID="lbllab9" runat="server" Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="ChkLab9" runat="server" BorderColor="Salmon" Visible="false" />
                                                        </td>
                                                        <td width="10%">
                                                            <asp:Label ID="lbllab10" runat="server" Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="ChkLab10" runat="server" BorderColor="Salmon" Visible="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table>
                                                    <tr>
                                                        <td>
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
