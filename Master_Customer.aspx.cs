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

public partial class Master_Customer : System.Web.UI.Page
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

        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
        lblname.Text = "Hi, " + Session["Name"].ToString();
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
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Customer", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        int delval = 0;
        string sql = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        sql = "select * FROM Master_Customer WHERE Deleted='" + delval + "' order by CUSTOMER_NAME";
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
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CUSTOMER_ID"].ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flg = BusinessTier.SaveCustomerMaster(conn, Convert.ToInt32(ID), "", 1, "", "", "", "", "", "", 1, "", "", "", "", "", true, true, 1, 1, 1, DateTime.Now, "", "", "", "", "", Session["sesUserID"].ToString(), "D");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(52);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Customer", "Delete", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(7);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Customer", "Delete", ex.ToString(), "Audit");
        }
    }

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            conn.Open();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            TextBox txtCustomercode = (TextBox)editedItem.FindControl("txtCustomercode");
            TextBox txtCustomerName = (TextBox)editedItem.FindControl("txtCustomerName");
            TextBox txtRocNo = (TextBox)editedItem.FindControl("txtRocNo");
            TextBox txtAddress1 = (TextBox)editedItem.FindControl("txtAddress1");
            TextBox txtAddress2 = (TextBox)editedItem.FindControl("txtAddress2");
            TextBox txtAddress3 = (TextBox)editedItem.FindControl("txtAddress3");
            TextBox txtState = (TextBox)editedItem.FindControl("txtState");
            TextBox txtCountry = (TextBox)editedItem.FindControl("txtCountry");
            //RadComboBox txtState = (RadComboBox)editedItem.FindControl("txtState");
            //RadComboBox txtCountry = (RadComboBox)editedItem.FindControl("txtCountry");
            //RadNumericTextBox txtPostalcode = (RadNumericTextBox)editedItem.FindControl("txtPostalcode");
            TextBox txtPostalcode = (TextBox)editedItem.FindControl("txtPostalcode");
            TextBox txtPhone = (TextBox)editedItem.FindControl("txtPhone");
            TextBox txtFax = (TextBox)editedItem.FindControl("txtFax");
            TextBox txtEmail = (TextBox)editedItem.FindControl("txtEmail");
            TextBox txtWebsite = (TextBox)editedItem.FindControl("txtWebsite");
            TextBox txtContractNo = (TextBox)editedItem.FindControl("txtContractNo");
            CheckBox ChkBranch = (CheckBox)editedItem.FindControl("ChkBranch");
            CheckBox ChkContract = (CheckBox)editedItem.FindControl("ChkContract");
            RadNumericTextBox txtPercentage = (RadNumericTextBox)editedItem.FindControl("txtPercentage");
            RadNumericTextBox txtExCompltedDays = (RadNumericTextBox)editedItem.FindControl("txtExCompltedDays");

            RadComboBox cboStaffId = (RadComboBox)editedItem.FindControl("cboStaffId");


            RadDatePicker Dtregdate = (RadDatePicker)editedItem.FindControl("Dtregdate");
            //TextBox txtAddress3 = (TextBox)editedItem.FindControl("txtAddress3");
            TextBox txtCity = (TextBox)editedItem.FindControl("txtCity");
            TextBox txtphonecode = (TextBox)editedItem.FindControl("txtphonecode");
            TextBox txtPhonecode1 = (TextBox)editedItem.FindControl("txtPhonecode1");
            TextBox txtPhone1 = (TextBox)editedItem.FindControl("txtPhone1");

            TextBox txtCRMID = (TextBox)editedItem.FindControl("txtCRMID");

            if (string.IsNullOrEmpty( Convert.ToString(Dtregdate.SelectedDate)))
            {
                Dtregdate.SelectedDate = DateTime.Now;

            }

            //string strCheckflag = "0";
            //SqlDataReader reader = BusinessTier.checkcode(conn, txtCustomercode.Text.ToString().Trim(), "C");
            //if (reader.Read())
            //{
            //    strCheckflag = reader["checkdup"].ToString();

            //}
            //BusinessTier.DisposeReader(reader);

            //if (strCheckflag.ToString() == "1")
            //{
            //    ShowMessage(25);
            //    return;
            //}
            if (txtCustomercode.Text.ToString().Trim() == "") { txtCustomercode.Text = "0"; }
            if (txtPercentage.Text.ToString().Trim() == "") { txtPercentage.Text = "0"; }
            if (txtPostalcode.Text.ToString().Trim() == "") { txtPostalcode.Text = "0"; }
            if (string.IsNullOrEmpty(cboStaffId.Text.ToString().Trim()))
            {
                cboStaffId.SelectedValue = "0";
            }
            //if (string.IsNullOrEmpty(txtState.Text.ToString().Trim()))
            //{
            //    txtState.SelectedValue = "0";
            //}
            //if (string.IsNullOrEmpty(txtCountry.Text.ToString().Trim()))  
            //{
            //    txtCountry.SelectedValue = "0";
            //}
            if (string.IsNullOrEmpty(txtExCompltedDays.Text.ToString().Trim()))
            {
                txtExCompltedDays.Text = "0";
            }
            int flg = BusinessTier.SaveCustomerMaster(conn, 1, txtCRMID.Text.ToString().Trim(), Convert.ToInt32(txtCustomercode.Text.ToString().Trim()), txtCustomerName.Text.ToString().Trim(), txtRocNo.Text.ToString().Trim(), txtAddress1.Text.ToString().Trim(), txtAddress2.Text.ToString().Trim(), txtState.Text.ToString().Trim(), txtCountry.Text.ToString().Trim(), Convert.ToInt32(txtPostalcode.Text.ToString().Trim()), txtPhone.Text.ToString().Trim(), txtFax.Text.ToString().Trim(), txtEmail.Text.ToString().Trim(), txtWebsite.Text.ToString().Trim(), txtContractNo.Text.ToString().Trim(), ChkBranch.Checked, ChkContract.Checked, Convert.ToInt32(txtExCompltedDays.Text.ToString().Trim()), Convert.ToInt32(txtPercentage.Text.ToString().Trim()), Convert.ToInt32(cboStaffId.SelectedValue), Dtregdate.SelectedDate.Value, txtAddress3.Text.ToString().Trim(), txtCity.Text.ToString().Trim(), txtphonecode.Text.ToString().Trim(), txtPhonecode1.Text.ToString().Trim(), txtPhone1.Text.ToString().Trim(), Session["sesUserID"].ToString(), "N");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(51);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Customer", "Insert", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Customer", "Insert", ex.ToString(), "Audit");
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
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CUSTOMER_ID"].ToString();

            Label lblExistingname = (Label)editedItem.FindControl("lblExistingname");
            TextBox txtCustomercode = (TextBox)editedItem.FindControl("txtCustomercode");
            TextBox txtCustomerName = (TextBox)editedItem.FindControl("txtCustomerName");
            TextBox txtRocNo = (TextBox)editedItem.FindControl("txtRocNo");
            TextBox txtAddress1 = (TextBox)editedItem.FindControl("txtAddress1");
            TextBox txtAddress2 = (TextBox)editedItem.FindControl("txtAddress2");
            TextBox txtState = (TextBox)editedItem.FindControl("txtState");
            TextBox txtCountry = (TextBox)editedItem.FindControl("txtCountry");
            //RadComboBox txtState = (RadComboBox)editedItem.FindControl("txtState");
            //RadComboBox txtCountry = (RadComboBox)editedItem.FindControl("txtCountry");
            TextBox txtPostalcode = (TextBox)editedItem.FindControl("txtPostalcode");
            TextBox txtPhone = (TextBox)editedItem.FindControl("txtPhone");
            TextBox txtFax = (TextBox)editedItem.FindControl("txtFax");
            TextBox txtEmail = (TextBox)editedItem.FindControl("txtEmail");
            TextBox txtWebsite = (TextBox)editedItem.FindControl("txtWebsite");
            TextBox txtContractNo = (TextBox)editedItem.FindControl("txtContractNo");
            CheckBox ChkBranch = (CheckBox)editedItem.FindControl("ChkBranch");
            CheckBox ChkContract = (CheckBox)editedItem.FindControl("ChkContract");
            RadNumericTextBox txtExCompltedDays = (RadNumericTextBox)editedItem.FindControl("txtExCompltedDays");

            RadNumericTextBox txtPercentage = (RadNumericTextBox)editedItem.FindControl("txtPercentage");
            RadComboBox cboStaffId = (RadComboBox)editedItem.FindControl("cboStaffId");


            RadDatePicker Dtregdate = (RadDatePicker)editedItem.FindControl("Dtregdate");
            TextBox txtAddress3 = (TextBox)editedItem.FindControl("txtAddress3");
            TextBox txtCity = (TextBox)editedItem.FindControl("txtCity");
            TextBox txtphonecode = (TextBox)editedItem.FindControl("txtphonecode");
            TextBox txtPhonecode1 = (TextBox)editedItem.FindControl("txtPhonecode1");
            TextBox txtPhone1 = (TextBox)editedItem.FindControl("txtPhone1");

            TextBox txtCRMID = (TextBox)editedItem.FindControl("txtCRMID");
            //string strCheckflag = "0";
            //SqlDataReader reader = BusinessTier.checkcode(conn, txtCustomercode.Text.ToString().Trim(), "C");
            //if (reader.Read())
            //{
            //    strCheckflag = reader["checkdup"].ToString();

            //}
            //BusinessTier.DisposeReader(reader);

            //if (strCheckflag.ToString() == "1")
            //{
            //    if (!(lblExistingname.Text.ToString().Trim() == txtCustomercode.Text.ToString().Trim()))
            //    {
            //        ShowMessage(25);
            //        return;
            //    }
            //}
            if (string.IsNullOrEmpty(txtPostalcode.Text.ToString().Trim()))
            {
                txtPostalcode.Text = "0";
            }
            if (string.IsNullOrEmpty(txtPercentage.Text.ToString().Trim()))
            {
                txtPercentage.Text = "0";
            }
            if (string.IsNullOrEmpty(txtCustomercode.Text.ToString().Trim()))
            {
                txtCustomercode.Text = "0";
            }
            if (string.IsNullOrEmpty(cboStaffId.Text.ToString().Trim()))
            {
                cboStaffId.SelectedValue = "0";
            }
            if (string.IsNullOrEmpty(txtExCompltedDays.Text.ToString().Trim()))
            {
                txtExCompltedDays.Text = "0";
            }
            int flg = BusinessTier.SaveCustomerMaster(conn, Convert.ToInt32(ID), txtCRMID.Text.ToString().Trim(), Convert.ToInt32(txtCustomercode.Text.ToString().Trim()), txtCustomerName.Text.ToString().Trim(), txtRocNo.Text.ToString().Trim(), txtAddress1.Text.ToString().Trim(), txtAddress2.Text.ToString().Trim(), txtState.Text.ToString().Trim(), txtCountry.Text.ToString().Trim(), Convert.ToInt32(txtPostalcode.Text.ToString().Trim()), txtPhone.Text.ToString().Trim(), txtFax.Text.ToString().Trim(), txtEmail.Text.ToString().Trim(), txtWebsite.Text.ToString().Trim(), txtContractNo.Text.ToString().Trim(), ChkBranch.Checked, ChkContract.Checked, Convert.ToInt32(txtExCompltedDays.Text.ToString().Trim()), Convert.ToInt32(txtPercentage.Text.ToString().Trim()), Convert.ToInt32(cboStaffId.SelectedValue), Dtregdate.SelectedDate.Value, txtAddress3.Text.ToString().Trim(), txtCity.Text.ToString().Trim(), txtphonecode.Text.ToString().Trim(), txtPhonecode1.Text.ToString().Trim(), txtPhone1.Text.ToString().Trim(), Session["sesUserID"].ToString(), "U");

            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(53);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Customer", "Update", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Customer", "Update", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

        RadGrid1.Rebind();
    }

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {

        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                Label lbl = (Label)editedItem.FindControl("lblCustomerID");
                RadComboBox cboStaffId = (RadComboBox)editedItem.FindControl("cboStaffId");

                 RadComboBox txt = (RadComboBox)editedItem.FindControl("cboStaffId");
                 RadComboBox txtState = (RadComboBox)editedItem.FindControl("txtState");
                RadComboBox txtCountry = (RadComboBox)editedItem.FindControl("txtCountry");
            
                //RadComboBox cboStationName = (RadComboBox)editedItem.FindControl("cboStationName");
                if (!(string.IsNullOrEmpty(lbl.Text.ToString())))
                {
                    //string strDeptName = "";
                    //int intdeptvalue = 0;
                    SqlConnection conn = BusinessTier.getConnection();
                    conn.Open();
                  

                    string sql1 = "select state_code,State_Name,Country_Code,Country_name,Acnt_Mngr from Vw_Customer_state_country WHERE Deleted = 0 and Customer_id = '" + lbl.Text.ToString() + "'";
                    SqlCommand command1 = new SqlCommand(sql1, conn);
                    SqlDataReader reader1 = command1.ExecuteReader();
                    if (reader1.Read())
                    {
                        txtState.Text = reader1["State_Name"].ToString();
                        txtCountry.Text = reader1["Country_name"].ToString();

                        txtState.SelectedValue = reader1["state_code"].ToString();
                        txtCountry.SelectedValue = reader1["Country_Code"].ToString();
                        cboStaffId.SelectedValue = reader1["Acnt_Mngr"].ToString();


                    }
                    BusinessTier.DisposeReader(reader1);


                    if (!(string.IsNullOrEmpty(cboStaffId.SelectedValue)))
                    {

                        string sql = "select Staff_Id,Staff_Name,Staff_no from Master_Staff WHERE Deleted = 0 and Staff_id = '" + cboStaffId.SelectedValue + "'";
                        SqlCommand command = new SqlCommand(sql, conn);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            cboStaffId.Text = reader["Staff_Name"].ToString();
                            cboStaffId.SelectedValue = reader["Staff_Id"].ToString();


                        }
                        BusinessTier.DisposeReader(reader);
                    }
                    else
                    {
                        cboStaffId.SelectedValue = "0";
                    }


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

    protected void cboStaffId_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select Staff_Id,Staff_Name,Staff_no from Master_Staff where DELETED=0 and Branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and [Staff_Name] LIKE @text + '%' order by Staff_Name";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Staff_Name"].ToString();
                item.Value = row["Staff_Id"].ToString();
                item.Attributes.Add("Staff_Name", row["Staff_Name"].ToString());
                item.Attributes.Add("Staff_no", row["Staff_no"].ToString());
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

    protected void txtState_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select State_Id,State_Code,State_Name from Master_State where DELETED=0 and  [State_Name] LIKE @text + '%' order by State_Code desc";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["State_Name"].ToString();
                item.Value = row["State_Code"].ToString();
                item.Attributes.Add("State_Name", row["State_Name"].ToString());
                item.Attributes.Add("State_Code", row["State_Code"].ToString());
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

    protected void txtCountry_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select Country_ID,Country_Code,Country_Name from Master_Country where DELETED=0 and  [Country_Name] LIKE @text + '%' order by Country_Code desc";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Country_Name"].ToString();
                item.Value = row["Country_Code"].ToString();
                item.Attributes.Add("Country_Name", row["Country_Name"].ToString());
                item.Attributes.Add("Country_Code", row["Country_Code"].ToString());
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