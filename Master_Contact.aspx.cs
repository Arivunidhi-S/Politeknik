using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

using System.Globalization;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using System.Drawing;

public partial class Master_Contact : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (!(string.IsNullOrEmpty(Session["sesUserID"].ToString())))
            {
                SqlConnection connMenu = BusinessTier.getConnection();
                connMenu.Open();
                SqlDataReader readerMenu = BusinessTier.getMenuList(connMenu, Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                dtMenuItems.Load(readerMenu);
                BusinessTier.DisposeReader(readerMenu);
                BusinessTier.DisposeConnection(connMenu);

            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(Session["sesUserID"].ToString()))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblStatus.Text = "";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
        lblname.Text = "Hi, " + Session["Name"].ToString();
    }

    protected void cboCustomerId_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select Customer_Id,Customer_Name,Customer_Code from Master_Customer where DELETED=0  and [Customer_Name] LIKE @text + '%' order by Customer_Name";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Customer_Name"].ToString();
                item.Value = row["Customer_Id"].ToString();
                item.Attributes.Add("Customer_Name", row["Customer_Name"].ToString());
                item.Attributes.Add("Customer_Code", row["Customer_Code"].ToString());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);
            // Session["rdobutton"] = "";
        }


        catch (Exception ex)
        {

            //ShowMessage("Please Select the Installation Name", "Red");
        }

    }

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {

        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            // edit item


            GridEditFormItem editedItem = e.Item as GridEditFormItem;
            Label lbl = (Label)editedItem.FindControl("lblcontID");
            RadComboBox cboCustomerId = (RadComboBox)editedItem.FindControl("cboCustomerId");


            if (!(string.IsNullOrEmpty(lbl.Text.ToString())))
            {

                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                string sql = "select Customer_ID,Customer_Name,Customer_Code FROM Vw_Customer_Contact WHERE Deleted = 0 and Contact_ID = '" + lbl.Text.ToString() + "'";
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    cboCustomerId.Text = reader["Customer_Name"].ToString();
                    cboCustomerId.SelectedValue = reader["Customer_ID"].ToString();


                }
                BusinessTier.DisposeReader(reader);
                BusinessTier.DisposeConnection(conn);

            }


        }
    }

    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGrid1.DataSource = DataSourceHelper();
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Contact", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        int delval = 0;
        string sql = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        sql = "select * FROM Vw_Customer_Contact WHERE Deleted='" + delval + "' order by CONTACT_ID";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

    protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CONTACT_ID"].ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flg = BusinessTier.SaveContactMaster(conn, Convert.ToInt32(ID), 1, "", "", "", "", Session["sesUserID"].ToString(), "D");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(61);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Contact", "Delete", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(7);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Contact", "Delete", ex.ToString(), "Audit");
        }
    }

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            conn.Open();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            TextBox txtContactName = (TextBox)editedItem.FindControl("txtContactName");
            TextBox txtContactNo = (TextBox)editedItem.FindControl("txtContactNo");
            TextBox txtemail = (TextBox)editedItem.FindControl("txtemail");
            TextBox txtDepartment = (TextBox)editedItem.FindControl("txtDepartment");
            RadComboBox cboCustomerId = (RadComboBox)editedItem.FindControl("cboCustomerId");

            //string strCheckflag = "0";
            //SqlDataReader reader = BusinessTier.checkcode(conn, txtEquipName.Text.ToString().Trim(), "E");
            //if (reader.Read())
            //{
            //    strCheckflag = reader["checkdup"].ToString();

            //}
            //BusinessTier.DisposeReader(reader);

            //if (strCheckflag.ToString() == "1")
            //{
            //    ShowMessage(39);
            //    return;
            //}

            if (txtContactNo.Text.ToString().Trim() == "") { txtContactNo.Text = "0"; }

            int flg = BusinessTier.SaveContactMaster(conn, 1, Convert.ToInt32(cboCustomerId.SelectedValue), txtContactName.Text.ToString().Trim(), txtContactNo.Text.ToString().Trim(), txtemail.Text.ToString().Trim(), txtDepartment.Text.ToString().Trim(), Session["sesUserID"].ToString(), "N");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(60);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Contact", "Insert", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            // e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Contact", "Insert", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

        RadGrid1.Rebind();
    }

    protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            conn.Open();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CONTACT_ID"].ToString();
            Label lblExistingname = (Label)editedItem.FindControl("lblExistingname");
            TextBox txtContactName = (TextBox)editedItem.FindControl("txtContactName");
            TextBox txtContactNo = (TextBox)editedItem.FindControl("txtContactNo");
            TextBox txtemail = (TextBox)editedItem.FindControl("txtemail");
            TextBox txtDepartment = (TextBox)editedItem.FindControl("txtDepartment");
            RadComboBox cboCustomerId = (RadComboBox)editedItem.FindControl("cboCustomerId");

            int flg = BusinessTier.SaveContactMaster(conn, Convert.ToInt32(ID.ToString()), Convert.ToInt32(cboCustomerId.SelectedValue), txtContactName.Text.ToString().Trim(), txtContactNo.Text.ToString().Trim(), txtemail.Text.ToString().Trim(), txtDepartment.Text.ToString().Trim(), Session["sesUserID"].ToString(), "U");

            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(62);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Contact", "Update", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Contact", "Update", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

        RadGrid1.Rebind();
    }

    private void ShowMessage(int errorNo)
    {
        lblStatus.Text = BusinessTier.g_ErrorMessagesDataTable.Rows[errorNo - 1]["Message"].ToString();
        System.Drawing.ColorConverter colConvert = new ColorConverter();
        string strColor = BusinessTier.g_ErrorMessagesDataTable.Rows[errorNo - 1]["Color"].ToString();
        lblStatus.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString(strColor);
    }

    private void InsertLogAuditTrail(string userid, string module, string activity, string result, string flag)
    {
        SqlConnection connLog = BusinessTier.getConnection();
        connLog.Open();
        BusinessTier.InsertLogAuditTrial(connLog, userid, module, activity, result, flag);
        BusinessTier.DisposeConnection(connLog);
    }
}