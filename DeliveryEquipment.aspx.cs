using System;
using System.IO;
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
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI.Calendar;
using System.Net.Mail;
using System.Web.Configuration;
//using  Microsoft.Office.Interop.Excel; 
using Excel = Microsoft.Office.Interop.Excel;

public partial class DeliveryEquipment : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    DataSet ds;

    DataTable Dt;

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
            btnReport.Enabled = false;


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

    protected void cboCustomer_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            string sql1 = "";
            if (string.IsNullOrEmpty(txtCustomer.Text.ToString().Trim()))
                //    sql1 = " select Customer_Id,Customer_Name,CRM_ID from Master_Customer where DELETED=0  and [Customer_Name] LIKE @text + '%' order by Customer_Name";
                sql1 = " select Customer_Id,Customer_Name,CRM_ID from Vw_Quotation_Customer where DELETED=0 and (Quot_Status='PENDING' or Quot_Status='DISCOUNT') and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and [Customer_Name] LIKE @text + '%'  group by Customer_Id,Customer_Name,CRM_ID order by Customer_Name";
            else
                sql1 = " select Customer_Id,Customer_Name,CRM_ID from Vw_Quotation_Customer where DELETED=0 and (Quot_Status='PENDING' or Quot_Status='DISCOUNT') and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and [Customer_Name] LIKE '" + txtCustomer.Text.ToString().Trim() + "' + '%'  group by Customer_Id,Customer_Name,CRM_ID order by Customer_Name";

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
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
                item.Attributes.Add("CRM_ID", row["CRM_ID"].ToString());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {

            //ShowMessage(ex.Message.ToString(), "Red");
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingPO", "cboCustomer_OnItemsRequested", ex.ToString(), "Audit");

        }
    }

    protected void cboCustomer_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cboQuotationNo.Text = "";
            cbojobno.Text = "";
            txtRefNo.Text = "";
        }
        catch (Exception ex)
        {
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_IH", "cboCustomer_OnSelectedIndexChanged", ex.ToString(), "Audit");
            //ShowMessage(ex.Message.ToString(), "Red");
        }
    }

    protected void cboQuotation_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string strCustomerID = "0";
            string strCustname = CboCustomer.Text.ToString().Trim();
            int intCustId = 0;
            if (!(string.IsNullOrEmpty(CboCustomer.SelectedValue.ToString().Trim())))
                intCustId = Convert.ToInt32(CboCustomer.SelectedValue.ToString().Trim());

            SqlDataReader reader1 = BusinessTier_CRM.GetCustomerIDbyName(conn, strCustname, intCustId);
            if (reader1.Read())
            {
                if (!(string.IsNullOrEmpty(reader1["CUSTOMER_ID"].ToString())))
                {
                    strCustomerID = (reader1["CUSTOMER_ID"].ToString());
                }
                else
                {
                    ShowMessage(201);
                    return;
                }
            }
            BusinessTier.DisposeReader(reader1);

            string sql1 = " select Quotation_Id,Quotation_No,Quotation_Date from Quotation where DELETED=0  and  Customer_ID='" + strCustomerID + "' and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and  [Quotation_No] LIKE @text + '%' and QUOT_STATUS='PENDING' order by Quotation_No";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Quotation_No"].ToString();
                item.Value = row["Quotation_Id"].ToString();
                item.Attributes.Add("Quotation_No", row["Quotation_No"].ToString());

                DateTime EnquiryDate = (DateTime)row["Quotation_Date"];

                item.Attributes.Add("Quotation_Date", EnquiryDate.ToString());
                // item.Attributes.Add("Customer_name", row["Customer_name"].ToString());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);
        }


        catch (Exception ex)
        {
            // ShowMessage(ex.Message.ToString(), "Red");
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_IH", "cboQuotation_OnItemsRequested", ex.ToString(), "Audit");
        }

    }

    protected void cboQuotation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cbojobno.Text = "";
            txtRefNo.Text = "";
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message.ToString();
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Calibration", "cboQuotationNo_SelectedIndexChanged", ex.ToString(), "Audit");
        }
    }

    protected void cboJobno_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "select distinct(jobno) as refno FROM Vw_CRMReceiveEquipmentCustTransDetails WHERE Deleted=0 and orderno=0 and Delivery=0 and Quotation_Id = '" + cboQuotationNo.SelectedValue.ToString() + "' group by RECEIVED_TRANS_DETAIL_ID,jobno,runningno ";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["refno"].ToString();
                //item.Value = row["RECEIVED_TRANS_DETAIL_ID"].ToString();
                //item.Attributes.Add("Jobno1", row["Jobno"].ToString());
                //txtCustomer.ToolTip = row["Jobno"].ToString();
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);
        }


        catch (Exception ex)
        {

        }

    }

    protected void cbojobNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string refno = cbojobno.Text.ToString();
        try
        {
            RadGridQuot.DataSource = DataSourceHelper(refno);
            RadGridQuot.Rebind();
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation_Modify", "cboQuotationNo_SelectedIndexChanged", ex.ToString(), "Audit");
        }
    }

    protected void cboCustomerId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cbojobno.Text = "";
            //RadGridQuot.DataSource = DataSourceHelper();
            //RadGridQuot.Rebind();

        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Calibration", "cboCustomerId_SelectedIndexChanged", ex.ToString(), "Audit");
        }
    }

    protected void RadGridQuot_ItemDataBound(object sender, GridItemEventArgs e)
    {

        if (e.Item is GridDataItem)
        {
            //Get the instance of the right type
            GridDataItem dataBoundItem = e.Item as GridDataItem;
            Label lbljobrun = (Label)e.Item.FindControl("lbljobrun");
            Label lbljob = (Label)e.Item.FindControl("lbljob");
            Label lblrunno = (Label)e.Item.FindControl("lblrunno");

            lbljobrun.Text = lbljob.Text.ToString().Trim() + "-" + lblrunno.Text.ToString().Trim();
        }


        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {

            GridEditFormItem editedItem = e.Item as GridEditFormItem;
            Label lbl = (Label)editedItem.FindControl("lblQuotationtransId");
            RadComboBox cboEquipmentId = (RadComboBox)editedItem.FindControl("cboEquipmentId");
            RadComboBox cboCalibration = (RadComboBox)editedItem.FindControl("cboCalibration");

            if (!(string.IsNullOrEmpty(lbl.Text.ToString())))
            {

                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                string sql = "select Equipment_ID,Equipment_Name,Equipment_no,calib_type FROM vw_Quotation WHERE Deleted = 0 and Quotation_Trans_ID = '" + lbl.Text.ToString() + "'";
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    cboEquipmentId.Text = reader["Equipment_Name"].ToString();
                    cboEquipmentId.SelectedValue = reader["Equipment_ID"].ToString();
                    cboCalibration.SelectedItem.Text = reader["calib_type"].ToString();
                }
                BusinessTier.DisposeReader(reader);
                BusinessTier.DisposeConnection(conn);
            }
        }



    }

    protected void RadGridQuot_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGridQuot.DataSource = DataSourceHelper("0");
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quoatation", "RadGridQuot_NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper(string refno)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "";

        sql = "select * FROM Vw_CRMReceiveEquipmentCustTransDetails WHERE Deleted=0 and jobno='" + refno.ToString() + "' and Delivery=0 and Invoiceflag=0";

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

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cbojobno.Text))
        {
            lblStatus.Text = "ReferanceNo Field Cannot be Empty";
            return;
        }
        // string jbno = cbojobno.Text.ToString().Trim().Split('-')[0];// +"/" + cboJobNo.Text.ToString().Trim().Split('/')[1];
        string jbno = cbojobno.Text.ToString().Trim();
        // string rnno = txtequipment.ToolTip.ToString();

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_AfterCalibration.aspx?param1=" + jbno.ToString().Trim() + "&param2=" + txtRefNo.Text.ToString().Trim() + "');", true);
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cbojobno.Text.ToString()))
        {
            lblStatus.Text = "Please Select JobNo";
            return;
        }

        if (string.IsNullOrEmpty(txtRefNo.Text.ToString()))
        {
            lblStatus.Text = "Please Enter Ref No";
            return;
        }

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            foreach (GridDataItem grdItem in RadGridQuot.Items)
            {
                CheckBox ChkSelect = (CheckBox)grdItem.FindControl("ChkSelect");

                int intTrackId = Convert.ToInt32(ChkSelect.ToolTip.ToString().Trim());

                if (ChkSelect.Checked)
                {
                    BusinessTier.Delivery_Save(conn, intTrackId, 1, txtRefNo.Text.ToString());
                    lblStatus.Text = "Successfully Delivery Inserted";
                    lblStatus.ForeColor = Color.Yellow;
                }
                else
                {
                    BusinessTier.Delivery_Save(conn, intTrackId, 0, txtRefNo.Text.ToString());
                }
            }


            btnReport.Enabled = true;
            string refno = cbojobno.Text.ToString();
            RadGridQuot.DataSource = DataSourceHelper(refno);
            RadGridQuot.Rebind();
            BusinessTier.DisposeConnection(conn);
        }

        catch (Exception ex)
        {
            ShowMessage(5);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "DeliveryEquipment", "Insert", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

}