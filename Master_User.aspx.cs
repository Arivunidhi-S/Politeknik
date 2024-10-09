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
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using System.Drawing;

public partial class Master_User : System.Web.UI.Page
{
    public static string g_strSaveFlag;
    public static string g_strUserID;
    public static string g_strLoginID;
    public static string g_strIsApproverAdded;

    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (!(string.IsNullOrEmpty(Session["sesUserID"].ToString())))
            {
                if (!(IsPostBack))
                {
                    clearFields();
                    g_strSaveFlag = "Insert";
                    g_strUserID = "0";
                    g_strLoginID = "0";
                    g_strIsApproverAdded = "N";
                    FillListBox_Module_InsertMode();
                    lblStatus.Text = "";
                }

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

    protected void cboUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(g_strSaveFlag.ToString()))
        {
            g_strSaveFlag = "Insert";
        }
        if (g_strSaveFlag.ToString() == "Insert")
        {
            // ValidateUserType();
        }
    }

    protected void chkAppRqrd_CheckedChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(g_strSaveFlag.ToString()))
        {
            g_strSaveFlag = "Insert";
        }

    }

    protected void clearFields()
    {

        txtUserID.Text = "";
        txtPass.Text = "";
        cboStaffId.Text = "";
        txtDesignation.Text = "";
        txtDept.Text = "";
        //txtBranch.Text = "";
       
        listboxModule1.Items.Clear();
       
        listboxModule2.Items.Clear();



    }

    protected void btnClearItem_Click(object source, EventArgs e)
    {
        clearFields();
        g_strSaveFlag = "Insert";
        g_strUserID = "0";
        g_strLoginID = "0";
       // g_strIsApproverAdded = "N";
        FillListBox_Module_InsertMode();
        lblStatus.Text = "";
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            int intSaveValidationFlag = 0;
            if (!(string.IsNullOrEmpty(g_strSaveFlag.ToString())))
            {
                if (g_strSaveFlag.ToString() == "Update")
                {
                    if (g_strLoginID.ToString() == txtUserID.Text.ToString())
                    {
                        intSaveValidationFlag = 2;
                    }
                    else
                    {
                        intSaveValidationFlag = 1;
                    }
                }
                if (g_strSaveFlag.ToString() == "Insert")
                {
                    intSaveValidationFlag = 1;
                }

                if (intSaveValidationFlag == 1)
                {
                    string strCheckflag = "0";
                    SqlConnection connCheck = BusinessTier.getConnection();
                    connCheck.Open();
                    SqlDataReader reader = BusinessTier.checkUserLoginId(connCheck, txtUserID.Text.ToString());
                    if (reader.Read())
                    {
                        strCheckflag = reader["checkdup"].ToString();
                    }
                    BusinessTier.DisposeReader(reader);
                    BusinessTier.DisposeConnection(connCheck);

                    if (strCheckflag.ToString() == "1")
                    {
                        ShowMessage(13);
                        return;
                    }
                }

            
             
                SqlConnection connSave = BusinessTier.getConnection();
                connSave.Open();
                int intSavedStatusflag = 0;
             
                intSavedStatusflag = BusinessTier.SaveUserMaster(connSave, Convert.ToInt32(cboStaffId.SelectedValue), txtUserID.Text.ToString(), txtPass.Text.ToString(),  Session["sesUserID"].ToString(), g_strSaveFlag, g_strUserID.ToString());
                BusinessTier.DisposeConnection(connSave);
                if (intSavedStatusflag == 1)
                {
                    if (g_strSaveFlag.ToString() == "Insert")
                    {
                        ShowMessage(1);
                    }

                    if (g_strSaveFlag.ToString() == "Update")
                    {
                        ShowMessage(2);
                    }


                    if (TabCon1.Tabs[1].Enabled == true)
                    {
                        SaveMudulePermissionList();
                    }

                    RadGrid1.DataSource = DataSourceHelper();

                    clearFields();
                    g_strSaveFlag = "Insert";
                    g_strUserID = "0";
                    g_strLoginID = "0";
                    g_strIsApproverAdded = "N";
                    FillListBox_Module_InsertMode();
                }
            }

            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterUser", "btnRegister", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(15);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterUser", "btnRegister", ex.ToString(), "Audit");
        }

    }

    protected void DeleteMasterApprover(string strUserId)
    {
        SqlConnection dbConn = BusinessTier.getConnection();
        dbConn.Open();
        SqlDataAdapter sqlDelete = new SqlDataAdapter();
        sqlDelete.DeleteCommand = dbConn.CreateCommand();
        sqlDelete.DeleteCommand.CommandText = "Delete from MasterUserApproval where UserId = '" + strUserId.ToString() + "'";
        sqlDelete.DeleteCommand.ExecuteNonQuery();
    }

    protected void SaveMudulePermissionList()
    {
        string strLoginid = txtUserID.Text.ToString();
        string strgetId = "0";
        string strisAppNeededFlag = "N";
        SqlConnection connCheck = BusinessTier.getConnection();
        connCheck.Open();
        SqlDataReader reader = BusinessTier.getMasterUserByLoginId(connCheck, strLoginid.ToString());
        if (reader.Read())
        {
            strgetId = reader["ID"].ToString();
           // strisAppNeededFlag = reader["IsApprovalRqrd"].ToString();
        }
        BusinessTier.DisposeReader(reader);
        BusinessTier.DisposeConnection(connCheck);

        if (string.IsNullOrEmpty(strgetId.ToString()))
        {
            if (strgetId.ToString() == "0")
            {
                ShowMessage(14);
                return;
            }
        }
        //string strUserIdAvailableinApproval = "0";
        //SqlConnection connectUserAprvl = BusinessTier.getConnection();
        //connectUserAprvl.Open();
        //SqlDataReader readerAprvl = BusinessTier.checkUserApprovalByUserId(connectUserAprvl, Int64.Parse(strgetId.ToString()));
        //if (readerAprvl.Read())
        //{
        //    strUserIdAvailableinApproval = readerAprvl["checkdup"].ToString();
        //}
        //BusinessTier.DisposeReader(readerAprvl);
        //BusinessTier.DisposeConnection(connectUserAprvl);

        if (g_strSaveFlag.ToString() == "Update")
        {
            if (g_strUserID.ToString() == strgetId.ToString())
            {
                DeleteMasterModulePermission(g_strUserID.ToString());
            }
            else
            {
                ShowMessage(14);
                return;
            }
        }

        Int64 intloginid = Int64.Parse(strgetId.ToString());
        string strAppRqrd_Module = "N";
        string strAppRqrd_ModuleValidateFlag = "0";
        string strAppRqrd_ModuleValidateFlag1 = "0";
        int intListbox2_count = listboxModule2.Items.Count;
        SqlConnection connect = BusinessTier.getConnection();
        connect.Open();
        for (int t = 0; t < intListbox2_count; t++)
        {
            Int64 intlinebyline = Convert.ToInt64(listboxModule2.Items[t].Value);

        //    SqlDataReader reader1 = BusinessTier.getMasterModuleById(connect, listboxModule2.Items[t].Value.ToString());
        //    if (reader1.Read())
        //    {
        //        strAppRqrd_Module = reader1["ApprovalNeeded"].ToString();
        //    }
        //    BusinessTier.DisposeReader(reader1);

            //if (strisAppNeededFlag.ToString() == "Y")
            //{
            //    if (strUserIdAvailableinApproval.ToString() == "1")
            //    {
            //        strAppRqrd_ModuleValidateFlag1 = "1";
            //    }
            //    else
            //    {
            //        if (strAppRqrd_Module.ToString() == "Y")
            //        {
            //            strAppRqrd_ModuleValidateFlag = "1";
            //        }
            //        else
            //        {
            //            strAppRqrd_ModuleValidateFlag1 = "1";
            //        }
            //    }
            //}
            //else
            //{
            //    strAppRqrd_ModuleValidateFlag1 = "1";
            //}

          //  if (strAppRqrd_ModuleValidateFlag1.ToString() == "1")
           // {
                SqlConnection connSave = BusinessTier.getConnection();
                connSave.Open();
               // int intSaveflag = BusinessTier.SaveUserMasterModulePermission(connSave, intloginid, intlinebyline, Session["sesUserID"].ToString());
                 int intSaveflag = BusinessTier.SaveUserMasterModulePermission(connSave, intloginid, intlinebyline, Session["sesUserID"].ToString());
                BusinessTier.DisposeConnection(connSave);
            //    strAppRqrd_ModuleValidateFlag1 = "0";
           // }
       }
        //BusinessTier.DisposeConnection(connect);
        //if (strAppRqrd_ModuleValidateFlag.ToString() == "1")
        //{
        //    ShowMessage(15);
        //    return;
        //}
        RadGrid1.DataSource = DataSourceHelper();
        RadGrid1.Rebind();
    }

    protected void cboStaffId_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            //string sql1 = " select Staff_Id,Staff_Name,Staff_no from Master_Staff where DELETED=0 and Branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and [Staff_Name] LIKE @text + '%' order by Staff_Name";
            string sql1 = " select Staff_Id,Staff_Name,Staff_no from Master_Staff where DELETED=0 and [Staff_Name] LIKE @text + '%' order by Staff_ID desc";
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

    protected void cboStaffId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql = " select Staff_id,Staff_name,Staff_No,position,Dept_Name from VW_Staff_Branch_Department where DELETED=0  and Staff_id='"+cboStaffId.SelectedValue +"'";
            SqlCommand command = new SqlCommand(sql, conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                txtDept.Text = reader["Dept_Name"].ToString();
                txtDesignation.Text = reader["position"].ToString();
               // txtBranch.Text = reader["Branch_Name"].ToString();         

            }
            BusinessTier.DisposeReader(reader);
            BusinessTier.DisposeConnection(conn);     

        }

        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation", "cboEnquiryId_SelectedIndexChanged", ex.ToString(), "Audit");
        }
    }

    protected void DeleteMasterModulePermission(string strUserId)
    {
        SqlConnection dbConn = BusinessTier.getConnection();
        dbConn.Open();
        SqlDataAdapter sqlDelete = new SqlDataAdapter();
        sqlDelete.DeleteCommand = dbConn.CreateCommand();
        sqlDelete.DeleteCommand.CommandText = "Delete from MasterUserModulePermission where UserId = '" + strUserId.ToString() + "'";
        sqlDelete.DeleteCommand.ExecuteNonQuery();
    }

    protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "RowClick" && e.Item is GridDataItem)
        {
            e.Item.Selected = true;
            string strTakenId = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"]).ToString();
            try
            {
                clearFields();
               
                string strUserType = "";
                string strAppRqrd = "";
                g_strUserID = strTakenId.ToString();   //Assign userID to class variable for update.
                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                SqlDataReader reader = BusinessTier.getMasterUserByID(conn, g_strUserID);
                while (reader.Read())
                {
                    txtUserID.Text = (reader["LoginId"].ToString());
                    txtPass.Text = (reader["Password"].ToString());
                    cboStaffId.Text = (reader["Staff_Name"].ToString());
                    cboStaffId.SelectedValue = (reader["Staff_Id"].ToString());
                    txtDesignation.Text = (reader["Position"].ToString());
                    txtDept.Text = (reader["Dept_name"].ToString());
                    //txtBranch.Text = reader["Branch_Name"].ToString();  
                    //cboUserType.Text = (reader["UserType"].ToString());

                    g_strLoginID = (reader["LoginId"]).ToString();  //Assign to Class variable to edit the LoginID
                    strUserType = (reader["UserType"].ToString());
                 //   strAppRqrd = (reader["IsApprovalRqrd"].ToString());
                 //   string strNotifyrqrd = (reader["IsNotifyRqrd"].ToString());


                }
                BusinessTier.DisposeReader(reader);
                BusinessTier.DisposeConnection(conn);
              

                FillListBox_Module_UpdateMode(g_strUserID);
                g_strSaveFlag = "Update"; //Assign to Class Variable
            }
            catch (Exception ex)
            {
                //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterUser", "LoadValuesFromGrid", ex.ToString(), "Audit");
                ShowMessage(8);
            }
        }
    }

    protected void FillListBox_Module_InsertMode()
    {
        listboxModule1.Items.Clear();
        listboxModule2.Items.Clear();

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        SqlDataReader reader = BusinessTier.getMasterModule(conn);
        while (reader.Read())
        {
            string strname = (reader["ModuleName"].ToString());
            string strid = (reader["Id"].ToString());
            listboxModule1.Items.Add(new RadListBoxItem(strname.ToString(), strid.ToString()));

        }
        BusinessTier.DisposeReader(reader);
        BusinessTier.DisposeConnection(conn);
    }

    protected void FillListBox_Module_UpdateMode(string strUserId)
    {
        listboxModule1.Items.Clear();
        listboxModule2.Items.Clear();

        SqlConnection connModulePermission = BusinessTier.getConnection();
        connModulePermission.Open();
        SqlDataReader readerModulePermission = BusinessTier.getMasterModulePermisnByUserId(connModulePermission, strUserId);
        while (readerModulePermission.Read())
        {
            string strname = (readerModulePermission["ModuleName"].ToString());
            string strid = (readerModulePermission["Id"].ToString());
            listboxModule2.Items.Add(new RadListBoxItem(strname.ToString(), strid.ToString()));
        }
        BusinessTier.DisposeReader(readerModulePermission);
        BusinessTier.DisposeConnection(connModulePermission);

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        SqlDataReader reader = BusinessTier.getMasterModule(conn);
        while (reader.Read())
        {
            string strname = (reader["ModuleName"].ToString());
            string strid = (reader["Id"].ToString());
            if (listboxModule2.Items.Count == 0)
            {
                listboxModule1.Items.Add(new RadListBoxItem(strname.ToString(), strid.ToString()));
            }
            else
            {
                int flagValidate = 0;
                for (int t = 0; t < listboxModule2.Items.Count; t++)
                {
                    string strModule2Id = (listboxModule2.Items[t].Value).ToString();
                    if ((strModule2Id.ToString() == strid.ToString()))
                    {
                        flagValidate = 1;
                    }
                }
                if (flagValidate == 0)
                {
                    listboxModule1.Items.Add(new RadListBoxItem(strname.ToString(), strid.ToString()));
                }

            }

        }
        BusinessTier.DisposeReader(reader);
        BusinessTier.DisposeConnection(conn);
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
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterUser", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flg = BusinessTier.DeleteUserGrid(conn, ID.ToString());
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(3);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterUser", "Delete", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(7);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterUser", "Delete", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        string sql = "select * FROM Vw_MasterUser_Staff WHERE Deleted=0 order by Staff_ID desc";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
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