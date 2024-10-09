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

public partial class Master_Staff : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (!(string.IsNullOrEmpty(Session["sesUserID"].ToString())))
                {
                    SqlConnection connMenu = BusinessTier.getConnection();
                    connMenu.Open();
                    SqlDataReader readerMenu = BusinessTier.getMenuList(connMenu, Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                    dtMenuItems.Load(readerMenu);
                    BusinessTier.DisposeReader(readerMenu);
                    BusinessTier.DisposeConnection(connMenu);

                    //DtQaotdt.SelectedDate = DateTime.Now;
                    //Insert Mode in First time

                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
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
            if (!IsPostBack)
            {
                RadGrid1.MasterTableView.IsItemInserted = true;

                RadGrid1.Rebind();

                if (string.IsNullOrEmpty(Session["sesUserID"].ToString()))
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    lblStatus.Text = "";

                }
            }
            lblname.Text = "Hi, " + Session["Name"].ToString();
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void cboDeptId_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select Dept_ID,Dept_Name,Dept_Code from Master_Department where DELETED=0  and [Dept_Name] LIKE @text + '%' order by Dept_Name";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Dept_Name"].ToString();
                item.Value = row["Dept_ID"].ToString();
                item.Attributes.Add("Dept_Name", row["Dept_Name"].ToString());
                item.Attributes.Add("Dept_Code", row["Dept_Code"].ToString());
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

    protected void cboBranchId_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select Branch_ID,Branch_Name,Branch_Code from Master_Branch where DELETED=0  and [Branch_Name] LIKE @text + '%' order by Branch_Name";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Branch_Name"].ToString();
                item.Value = row["Branch_ID"].ToString();
                item.Attributes.Add("Branch_Name", row["Branch_Name"].ToString());
                item.Attributes.Add("Branch_Code", row["Branch_Code"].ToString());
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
     
        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                Label lbl = (Label)editedItem.FindControl("lblStaffID");
                //Label lblStatus = (Label)editedItem.FindControl("lblStatus");
                RadComboBox cboDeptId = (RadComboBox)editedItem.FindControl("cboDeptId");
                //RadComboBox cboBranchId = (RadComboBox)editedItem.FindControl("cboBranchId");
                //RadComboBox cboStationName = (RadComboBox)editedItem.FindControl("cboStationName");
               
                if (!(string.IsNullOrEmpty(lbl.Text.ToString())))
                {
                    //string strDeptName = "";
                    //int intdeptvalue = 0;
                    SqlConnection conn = BusinessTier.getConnection();
                    conn.Open();
                    string sql = "select Dept_ID,Dept_Name,Dept_Code FROM VW_Staff_Branch_Department WHERE Deleted = 0 and Staff_id = '" + lbl.Text.ToString() + "'";
                    SqlCommand command = new SqlCommand(sql, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                       // lblStatus.Text = string.Empty;

                        cboDeptId.Text = reader["Dept_Name"].ToString();
                        cboDeptId.SelectedValue = reader["Dept_ID"].ToString();
                        //cboBranchId.Text = reader["Branch_Name"].ToString();
                        //cboBranchId.SelectedValue = reader["Branch_Id"].ToString();

                    }
                    BusinessTier.DisposeReader(reader);
                    BusinessTier.DisposeConnection(conn);
                    //cboDeptId.Text = strDeptName;
                    //cboDeptId.SelectedValue =Convert.ToString(intdeptvalue);
                }

            }
        }
        catch (Exception ex)
        {
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
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        int delval = 0;
        string sql = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        sql = "select * FROM VW_Staff_Branch_Department WHERE Deleted='" + delval + "' order by Staff_ID";
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
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Staff_Id"].ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flg = BusinessTier.SaveStaffMaster(conn, Convert.ToInt32(ID), "", "","", "", "",1,"",true, Session["sesUserID"].ToString(), "D");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(41);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff", "Delete", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(7);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff", "Delete", ex.ToString(), "Audit");
        }
    }

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            conn.Open();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            TextBox txtStaffNo = (TextBox)editedItem.FindControl("txtStaffNo");
            TextBox txtStaffame = (TextBox)editedItem.FindControl("txtStaffame");
            //TextBox cboBranchId = (TextBox)editedItem.FindControl("cboBranchId");
            RadComboBox cboDeptId = (RadComboBox)editedItem.FindControl("cboDeptId");
            //RadComboBox cboBranchId = (RadComboBox)editedItem.FindControl("cboBranchId");
            TextBox txtDesignation = (TextBox)editedItem.FindControl("txtDesignation");
            TextBox txtEmail = (TextBox)editedItem.FindControl("txtEmail");
            TextBox txtPhone = (TextBox)editedItem.FindControl("txtPhone");
            CheckBox ChkHOD = (CheckBox)editedItem.FindControl("ChkHOD");
            //string strCheckflag = "0";
            //SqlDataReader reader = BusinessTier.checkcode(conn, txtStaffNo.Text.ToString().Trim(), "S");
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

            int flg = BusinessTier.SaveStaffMaster(conn, 1, txtStaffNo.Text.ToString().Trim(), txtStaffame.Text.ToString().Trim(), txtDesignation.Text.ToString().Trim(), txtPhone.Text.ToString().Trim(), txtEmail.Text.ToString().Trim(), Convert.ToInt32(cboDeptId.SelectedValue), "1", false, Session["sesUserID"].ToString(), "N");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(40);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff", "Insert", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
           // e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff", "Insert", ex.ToString(), "Audit");
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
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Staff_Id"].ToString();
            Label lblExistingname = (Label)editedItem.FindControl("lblExistingname");
            TextBox txtStaffNo = (TextBox)editedItem.FindControl("txtStaffNo");
            TextBox txtStaffame = (TextBox)editedItem.FindControl("txtStaffame");
            //TextBox cboBranchId = (TextBox)editedItem.FindControl("cboBranchId");
            RadComboBox cboDeptId = (RadComboBox)editedItem.FindControl("cboDeptId");
            //RadComboBox cboBranchId = (RadComboBox)editedItem.FindControl("cboBranchId");
            TextBox txtDesignation = (TextBox)editedItem.FindControl("txtDesignation");
            TextBox txtEmail = (TextBox)editedItem.FindControl("txtEmail");
            TextBox txtPhone = (TextBox)editedItem.FindControl("txtPhone");
            CheckBox ChkHOD = (CheckBox)editedItem.FindControl("ChkHOD");
            //string strCheckflag = "0";
            //SqlDataReader reader = BusinessTier.checkcode(conn, txtStaffNo.Text.ToString().Trim(), "S");
            //if (reader.Read())
            //{
            //    strCheckflag = reader["checkdup"].ToString();

            //}
            //BusinessTier.DisposeReader(reader);

            //if (strCheckflag.ToString() == "1")
            //{
            //    if (!(lblExistingname.Text.ToString().Trim() == txtStaffNo.Text.ToString().Trim()))
            //    {
            //        ShowMessage(46);
            //        return;
            //    }
            //}

            int flg = BusinessTier.SaveStaffMaster(conn, Convert.ToInt32(ID.ToString()), txtStaffNo.Text.ToString().Trim(), txtStaffame.Text.ToString().Trim(), txtDesignation.Text.ToString().Trim(), txtPhone.Text.ToString().Trim(), txtEmail.Text.ToString().Trim(), Convert.ToInt32(cboDeptId.SelectedValue), "1", false, Session["sesUserID"].ToString(), "U");

         

            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(42);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff", "Update", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff", "Update", ex.ToString(), "Audit");
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