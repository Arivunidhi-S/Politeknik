<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportGridviewData.aspx.cs" Inherits="ExportGridviewData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Export Gridview Data to Excel in Asp.net</title>
</head>
<body>
    <form id="form1" runat="server">
   <div>
<asp:GridView ID="gvDetails" AutoGenerateColumns="false" CellPadding="5" runat="server">
<Columns>
<asp:BoundField HeaderText="UserId" DataField="UserId" />
<asp:BoundField HeaderText="UserName" DataField="UserName" />
<asp:BoundField HeaderText="Education" DataField="Education" />
<asp:BoundField HeaderText="Location" DataField="Location" />
</Columns>
<HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />
</asp:GridView>
</div>
<asp:Button ID="btnExport" runat="server" Text="Export to Excel" 
        onclick="btnExport_Click" /><br />
        <asp:Button ID="Button1" runat="server" Text="Test" 
        onclick="btncheck_Click" />
    </form>
</body>
</html>
