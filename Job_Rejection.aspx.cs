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

using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI.Calendar;
//using NCalc;
using System.Text.RegularExpressions;
using System.Data.Common;
using System.Web.Configuration;
using System.Net;

public partial class Job_Rejection : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    DataSet ds;

    DataTable Dt;

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            // if (!IsPostBack)
            //{
            if (!(string.IsNullOrEmpty(Session["sesUserID"].ToString())))
            {
                SqlConnection connMenu = BusinessTier.getConnection();
                connMenu.Open();
                SqlDataReader readerMenu = BusinessTier.getMenuList(connMenu, Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                dtMenuItems.Load(readerMenu);
                BusinessTier.DisposeReader(readerMenu);
                BusinessTier.DisposeConnection(connMenu);
                dtRejection.SelectedDate = DateTime.Now;
                DateTime dt = DateTime.Now;
                DateTime dt1 = dt.AddDays(30);
                btnReport.Enabled = false;
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            //txtjobno_load();
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e1)
    {
        try
        {

            if (string.IsNullOrEmpty(Session["sesUserID"].ToString()))
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {

            }

            lblname.Text = "Hi, " + Session["Name"].ToString();
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void cboCustomerId_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "";
            if (string.IsNullOrEmpty(txtCustomer.Text.ToString().Trim()))

                sql1 = " select Customer_Id,Customer_Name,CRM_ID from Vw_Quotation_Customer where DELETED=0 and (Quot_Status='PENDING' or Quot_Status='DISCOUNT') and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and [Customer_Name] LIKE @text + '%'  group by Customer_Id,Customer_Name,CRM_ID order by Customer_Name";
            else
                sql1 = " select Customer_Id,Customer_Name,CRM_ID from Vw_Quotation_Customer where DELETED=0 and (Quot_Status='PENDING' or Quot_Status='DISCOUNT') and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and [Customer_Name] LIKE '" + txtCustomer.Text.ToString().Trim() + "' + '%'  group by Customer_Id,Customer_Name,CRM_ID order by Customer_Name";

            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Customer_Name"].ToString().Trim();
                item.Value = row["Customer_Id"].ToString().Trim();
                item.Attributes.Add("Customer_Name", row["Customer_Name"].ToString().Trim());
                // item.Attributes.Add("CRM_ID", row["CRM_ID"].ToString().Trim());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);

        }


        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invoice", "cboCustomerid_ItemrequestedChanged", ex.ToString(), "Audit");
            //ShowMessage("Please Select the Installation Name", "Red");
        }

    }

    protected void cboCustomerId_SelectedIndexChanged(object sender, EventArgs e)
    {
        cboQuotationNo.Text = "";
        cboJobNo.Text = "";
        txtequipment.Text = "";
        txtMaker.Text = "";
        txtModel.Text = "";
        dtRejection.SelectedDate = DateTime.Now;
    }

    protected void cboQuotationNo_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            if (string.IsNullOrEmpty(cboCustomerId.SelectedValue.ToString().Trim()))
            {
                lblStatus.Text = "Please Select the Customer Name";
                return;
            }
            //string sql1 = "  select Quotation_Id,Quotation_No,Quotation_Date from Vw_QuotationRecvEqpmtCustomer where DELETED=0 and  Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and Status='COMPLETED' and QinvcMasterflag=0 and  Customer_ID='" + cboCustomerId.SelectedValue + "'  order by Quotation_No";
            string sql1 = " ";
            //if (Session["sesBranchID"].ToString().Trim() == "8")
            //    sql1 = "  select Quotation_Id,Quotation_No,Quotation_Date from Vw_QuotationRecvEqpmtCustomer where DELETED=0 and  Branch_id='6'  and QinvcMasterflag=0 and CertCompletedflag=1  and  Customer_ID='" + cboCustomerId.SelectedValue + "' Group by Quotation_Id,Quotation_No,Quotation_Date    order by Quotation_No";
            // else

            sql1 = "select Quotation_Id,Quotation_No,Quotation_Date from Quotation where QUOT_STATUS='PENDING' and DELETED=0 and  Branch_id='" + Session["sesBranchID"].ToString().Trim() + "'  and  Customer_ID='" + cboCustomerId.SelectedValue + "' Group by Quotation_Id,Quotation_No,Quotation_Date  order by Quotation_No desc";
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
            // Session["rdobutton"] = "";
        }


        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invoice", "cboQuotationNo_ItemRequested", ex.ToString(), "Audit");
            //ShowMessage("Please Select the Installation Name", "Red");
        }

    }

    protected void cboQuotationNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        cboJobNo.Text = "";
        txtequipment.Text = "";
        txtMaker.Text = "";
        txtModel.Text = "";
        dtRejection.SelectedDate = DateTime.Now;
    }

    protected void cboJobNo_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "select RECEIVED_TRANS_DETAIL_ID,jobno,runningno FROM Vw_CRMReceiveEquipmentCustTransDetails WHERE Deleted=0 and orderno=0 and Quotation_Id = '" + cboQuotationNo.SelectedValue.ToString() + "' group by RECEIVED_TRANS_DETAIL_ID,jobno,runningno";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Jobno"].ToString() + "-" + row["runningno"].ToString();
                item.Value = row["RECEIVED_TRANS_DETAIL_ID"].ToString();
                item.Attributes.Add("Jobno1", row["Jobno"].ToString());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);
        }


        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invoice", "cboJobno_ItemrequestedChanged", ex.ToString(), "Audit");
            //ShowMessage("Please Select the Installation Name", "Red");
        }

    }

    protected void cboJobNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string sql = "";
            // string jobno = txtjobno.SelectedItem.Attributes["Jobno1"].ToString();
            sql = "select EQUIPMENT_ID,EQUIPMENT_NAME,maker,model FROM Vw_CRMReceiveEquipmentCustTransDetails WHERE Deleted=0 and RECEIVED_TRANS_DETAIL_ID='" +cboJobNo.SelectedValue.ToString() + "'";
            SqlCommand cmd2 = new SqlCommand(sql, conn);
            SqlDataReader rd2 = cmd2.ExecuteReader();
            if (rd2.Read())
            {
                txtequipment.Text = rd2["EQUIPMENT_NAME"].ToString();
                txtequipment.ToolTip = rd2["EQUIPMENT_ID"].ToString();
                txtMaker.Text = rd2["maker"].ToString();
                txtModel.Text = rd2["model"].ToString();
            }
            rd2.Close();
            BusinessTier.DisposeConnection(conn);
            //RadGridQuot.DataSource = DataSourceHelper("J", "0");
            //RadGridQuot.Rebind();

        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation_Modify", "cboQuotationNo_SelectedIndexChanged", ex.ToString(), "Audit");
        }
    }

    protected void RadGridRejection_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message.ToString();
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invoice", "RadGridQuot_ItemDataBound", ex.ToString(), "Audit");

        }


    }

    protected void RadGridRejection_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGridRejection.DataSource = DataSourceHelper();
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invocie", "RadGridQuot_NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        //   TabContainer1.ActiveTab = TabContainer1.Tabs[1];

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "";
        sql = "select * FROM Vw_CRMReceiveEquipmentCustTransDetails  where Rejectiondate is not null and deleted=0 order by RECEIVED_TRANS_DETAIL_ID desc";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

    protected void RadGridQuot_ItemDataBound(object sender, GridItemEventArgs e)
    {

        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {

            GridEditFormItem editedItem = e.Item as GridEditFormItem;
            Label lbl = (Label)editedItem.FindControl("lblQuotationtransId");
            RadComboBox cboEquipmentId = (RadComboBox)editedItem.FindControl("cboEquipmentId");
            //RadComboBox cboCalibration = (RadComboBox)editedItem.FindControl("cboCalibration");
            RadNumericTextBox txtPrice = (RadNumericTextBox)editedItem.FindControl("txtPrice");
            if (!(string.IsNullOrEmpty(lbl.Text.ToString())))
            {

                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                string sql = "select Equipment_ID,Equipment_Name,Equipment_no,calib_type,Sum(price) as price,Notes FROM vw_Quotation WHERE Deleted = 0 and Quotation_Trans_ID = '" + lbl.Text.ToString() + "' group by Equipment_ID,Equipment_Name,Equipment_no,calib_type,Notes ";
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    cboEquipmentId.Text = reader["Equipment_Name"].ToString();
                    cboEquipmentId.SelectedValue = reader["Equipment_ID"].ToString();
                    //  cboCalibration.SelectedItem.Text = reader["calib_type"].ToString();
                    txtPrice.Value = Convert.ToDouble(reader["price"].ToString());

                }
                BusinessTier.DisposeReader(reader);
                BusinessTier.DisposeConnection(conn);
            }
        }



    }

    protected void RadGridQuot_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();

            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Quotation_trans_Id"].ToString();
            int flg = BusinessTier.SaveInsertQuot_Detail(conn, Convert.ToInt32(ID), 1, "", 1, 1, 1, 1, 1, "", "", 0, 1, Session["sesUserID"].ToString(), "D");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(3);

            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation_Detial", "Delete", "Success", "Log");

        }
        catch (Exception ex)
        {
            ShowMessage(7);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation_Detial", "Delete", ex.ToString(), "Audit");
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

    ////////////////////////////Report////////////////////////////////////////////////////
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboJobNo.Text))
        {
            lblStatus.Text = "JobNo Field Cannot be Empty";
            return;
        }

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_JobRejection.aspx?param1=" + cboJobNo.SelectedValue.ToString().Trim() + "&param2=" + 0 + "');", true);
    }

    protected void btnIReject_OnClick(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            if (string.IsNullOrEmpty(cboCustomerId.Text))
            {
                lblStatus.Text = "Customer Field Cannot be Empty";
                return;
            }

            if (string.IsNullOrEmpty(cboQuotationNo.Text))
            {
                lblStatus.Text = "Quotation Field Cannot be Empty";
                return;
            }

            if (string.IsNullOrEmpty(cboJobNo.Text))
            {
                lblStatus.Text = "JobNo Field Cannot be Empty";
                return;
            }
            if (string.IsNullOrEmpty(dtRejection.SelectedDate.ToString()))
            {
                lblStatus.Text = "Date Field Cannot be Empty";
                return;
            }
            if (string.IsNullOrEmpty(txtto.Text))
            {
                lblStatus.Text = "To Field Cannot be Empty";
                return;
            }
            if (string.IsNullOrEmpty(txtref.Text))
            {
                lblStatus.Text = "YourRef Field Cannot be Empty";
                return;
            }
            if (cboRejectAt.Text == "<--Select-->")
            {
                lblStatus.Text = "Select Reject At Field";
                return;
            }

            String invdt = dtRejection.SelectedDate.ToString();
            DateTime pdt = DateTime.Parse(invdt);
            invdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year + " 00:00:00";

            int invid = 0;
            conn.Open();

            BusinessTier.Job_Reject(conn, Convert.ToInt32(cboJobNo.SelectedValue.ToString()), cboRejectAt.Text.ToString(), invdt.ToString(), txtto.Text.ToString(), txtref.Text.ToString());

           // txtjobno_load();
            lblStatus.Text = "Successfully Job Rejected";
            BusinessTier.DisposeConnection(conn);
            btnReport.Enabled = true;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_JobRejection.aspx?param1=" + cboJobNo.SelectedValue.ToString().Trim() + "&param2=" + 0 + "');", true);
           // cboJobNo.ClearSelection();
                 }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message.ToString();
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Job_Rejection", "btnIReject_OnClick", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }


    }

}