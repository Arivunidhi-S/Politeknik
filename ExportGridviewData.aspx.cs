using System;
using System.Data;
using System.IO;
using System.Web.UI;

public partial class ExportGridviewData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridview();
        }
    }
    protected void BindGridview()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("UserId", typeof(Int32));
        dt.Columns.Add("UserName", typeof(string));
        dt.Columns.Add("Education", typeof(string));
        dt.Columns.Add("Location", typeof(string));
        dt.Rows.Add(1, "SureshDasari", "B.Tech", "Chennai");
        dt.Rows.Add(2, "MadhavSai", "MBA", "Nagpur");
        dt.Rows.Add(3, "MaheshDasari", "B.Tech", "Nuzividu");
        dt.Rows.Add(4, "Rohini", "MSC", "Chennai");
        dt.Rows.Add(5, "Mahendra", "CA", "Guntur");
        dt.Rows.Add(6, "Honey", "B.Tech", "Nagpur");
        gvDetails.DataSource = dt;
        gvDetails.DataBind();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Customers.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvDetails.AllowPaging = false;
            BindGridview();
            //Change the Header Row back to white color
            
            gvDetails.HeaderRow.Style.Add("background-color", "#FFFFFF");
           // Applying stlye to gridview header cells
            for (int i = 0; i < gvDetails.HeaderRow.Cells.Count; i++)
            {
                gvDetails.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
            gvDetails.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
        
        }

    }

    protected void btncheck_Click(object sender, EventArgs e)
    {
        
            try
            {
                System.Data.OleDb.OleDbConnection MyConnection;
                System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand();
                string sql = null;
                
                MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\\Users\\Arivu\\Documents\\Book1.xls';Extended Properties=Excel 8.0;");//HDR=Yes;IMEX=1
                //if (MyConnection.Open()==true)
                //{
                
                //}

                MyConnection.Open();
                 myCommand.Connection = MyConnection;
               sql = "INSERT INTO [Sheet1$] (id, name) values ('111', 'ABC')";
               // sql = "Update [Sheet1$] SET A3='DesiredNumber',A4='DesiredNumber' WHERE A1 = 'id'";
                myCommand.CommandText = sql;
                myCommand.ExecuteNonQuery();
                MyConnection.Close();
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.ToString());
                Response.Write("<script>alert?'Hello'</script>");
            }
        }
       

    }

