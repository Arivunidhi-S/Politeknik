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

public partial class Master_Instrument : System.Web.UI.Page
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
    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {

        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                Label lbl = (Label)editedItem.FindControl("lblCaliStdID");
                RadComboBox cboDeptId = (RadComboBox)editedItem.FindControl("cboDeptId");

                if (!(string.IsNullOrEmpty(lbl.Text.ToString())))
                {

                    SqlConnection conn = BusinessTier.getConnection();
                    conn.Open();
                    string sql = "select Dept_ID,Dept_Name,Dept_Code FROM Vw_Instrument_Department WHERE Deleted = 0 and Calib_std_Id = '" + lbl.Text.ToString() + "'";
                    SqlCommand command = new SqlCommand(sql, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        cboDeptId.Text = reader["Dept_Name"].ToString();
                        cboDeptId.SelectedValue = reader["Dept_ID"].ToString();

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
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_CalibrationStandard", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        int delval = 0;
        string sql = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        sql = "select * FROM Vw_Instrument_Department WHERE Deleted='" + delval + "' order by Instrument_Name";
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
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Calib_std_Id"].ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flg = BusinessTier.SaveInstrumentMaster(conn, Convert.ToInt32(ID), 1, "", "", "", DateTime.Now, "", "", "",DateTime.Now,0,Convert.ToInt32( Session["sesBranchID"].ToString().Trim()), Session["sesUserID"].ToString(), "D");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(44);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_CalibrationStandard", "Delete", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(7);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_CalibrationStandard", "Delete", ex.ToString(), "Audit");
        }
    }
    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            conn.Open();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            TextBox txtInstrmntName = (TextBox)editedItem.FindControl("txtInstrmntName");
            RadDatePicker DtTestDate = (RadDatePicker)editedItem.FindControl("DtTestDate");
            RadComboBox cboDeptId = (RadComboBox)editedItem.FindControl("cboDeptId");
            TextBox txtCertificateNo = (TextBox)editedItem.FindControl("txtCertificateNo");
            TextBox txtSerialNo = (TextBox)editedItem.FindControl("txtSerialNo");
            TextBox txtTraceability = (TextBox)editedItem.FindControl("txtTraceability");
            RadDatePicker dtCalibrationDate = (RadDatePicker)editedItem.FindControl("dtCalibrationDate"); 
            TextBox txtDescription = (TextBox)editedItem.FindControl("txtDescription");
            TextBox txtRemarks = (TextBox)editedItem.FindControl("txtRemarks");

            string strCheckflag = "0";
            SqlDataReader reader = BusinessTier.checkcode(conn, txtInstrmntName.Text.ToString().Trim(), "I");
            if (reader.Read())
            {
                strCheckflag = reader["checkdup"].ToString();

            }
            BusinessTier.DisposeReader(reader);

            if (strCheckflag.ToString() == "1")
            {
                ShowMessage(39);
                return;
            }

            DateTime dayStart = DateTime.Now;
            DateTime dateEnd = Convert.ToDateTime(DtTestDate.SelectedDate);

            TimeSpan ts = dateEnd - dayStart;

            double Years = Convert.ToDouble(ts.TotalDays) / 365;
            double Months = Years * 12;
            double Days = Convert.ToDouble(ts.TotalDays);


            int flg = BusinessTier.SaveInstrumentMaster(conn, 1, Convert.ToInt32(cboDeptId.SelectedValue), txtInstrmntName.Text.ToString().Trim(), txtSerialNo.Text.ToString().Trim(), txtCertificateNo.Text.ToString().Trim(), DtTestDate.SelectedDate.Value, txtTraceability.Text.ToString().Trim(), txtDescription.Text.ToString().Trim(), txtRemarks.Text.ToString().Trim(), dtCalibrationDate.SelectedDate.Value, Convert.ToInt32(Days), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), Session["sesUserID"].ToString(), "N");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(43);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_CalibrationStandard", "Insert", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            // e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_CalibrationStandard", "Insert", ex.ToString(), "Audit");
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
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Calib_std_Id"].ToString();
            Label lblExistingname = (Label)editedItem.FindControl("lblExistingname");
            TextBox txtInstrmntName = (TextBox)editedItem.FindControl("txtInstrmntName");
            RadDatePicker DtTestDate = (RadDatePicker)editedItem.FindControl("DtTestDate");
            RadComboBox cboDeptId = (RadComboBox)editedItem.FindControl("cboDeptId");
            TextBox txtCertificateNo = (TextBox)editedItem.FindControl("txtCertificateNo");
            TextBox txtSerialNo = (TextBox)editedItem.FindControl("txtSerialNo");
            TextBox txtTraceability = (TextBox)editedItem.FindControl("txtTraceability");
            RadDatePicker dtCalibrationDate = (RadDatePicker)editedItem.FindControl("dtCalibrationDate"); 
            TextBox txtDescription = (TextBox)editedItem.FindControl("txtDescription");
            TextBox txtRemarks = (TextBox)editedItem.FindControl("txtRemarks");
            string strCheckflag = "0";
            SqlDataReader reader = BusinessTier.checkcode(conn, txtInstrmntName.Text.ToString().Trim(), "I");
            if (reader.Read())
            {
                strCheckflag = reader["checkdup"].ToString();

            }
            BusinessTier.DisposeReader(reader);

            if (strCheckflag.ToString() == "1")
            {
                if (!(lblExistingname.Text.ToString().Trim() == txtInstrmntName.Text.ToString().Trim()))
                {
                    ShowMessage(46);
                    return;
                }
            }
            DateTime dayStart = DateTime.Now;
            DateTime dateEnd = Convert.ToDateTime(DtTestDate.SelectedDate);

            TimeSpan ts = dateEnd - dayStart;

            double Years = Convert.ToDouble(ts.TotalDays) / 365;
            double Months = Years * 12;
            double Days = Convert.ToDouble(ts.TotalDays);
            int flg = BusinessTier.SaveInstrumentMaster(conn, Convert.ToInt32(ID.ToString()), Convert.ToInt32(cboDeptId.SelectedValue), txtInstrmntName.Text.ToString().Trim(), txtSerialNo.Text.ToString().Trim(), txtCertificateNo.Text.ToString().Trim(), DtTestDate.SelectedDate.Value, txtTraceability.Text.ToString().Trim(), txtDescription.Text.ToString().Trim(), txtRemarks.Text.ToString().Trim(), dtCalibrationDate.SelectedDate.Value, Convert.ToInt32(Days), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), Session["sesUserID"].ToString(), "U");


            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(45);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_CalibrationStandard", "Update", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_CalibrationStandard", "Update", ex.ToString(), "Audit");
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