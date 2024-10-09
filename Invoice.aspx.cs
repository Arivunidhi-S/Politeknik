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

public partial class Invoice : System.Web.UI.Page
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
                DtInvoicedt.SelectedDate = DateTime.Now;
                DateTime dt = DateTime.Now;
                DateTime dt1 = dt.AddDays(30);

            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            //txtjobno_load();

            btnReport.Enabled = false;
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

            lblname.Text = "Hi, " + Session["Name"].ToString();

        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }


    // protected void txtjobno_load()
    //{
    //    SqlConnection conn = BusinessTier.getConnection();
    //    conn.Open();
    //    try
    //    {

    //        string sql = "select max(Invoice_Id)as maxval from Invoice";
    //        SqlCommand cmd1 = new SqlCommand(sql, conn);
    //        SqlDataReader rd1 = cmd1.ExecuteReader();
    //        int val = 0;
    //        rd1.Read();
    //        if (string.IsNullOrEmpty(rd1["maxval"].ToString()))
    //        {
    //            val = 1;
    //        }
    //        else
    //        {
    //            val = Convert.ToInt32(rd1["maxval"].ToString()) + 1;
    //        }

    //        lblinvoiceno.Text = "INV/BMCL/" + DateTime.Today.ToString("yy/" + val);
    //        lblinvoiceno.ToolTip = val.ToString();
    //        // txtstickno.Text = "STK/BMCL/" + DateTime.Today.ToString("yy/" + val);
    //        BusinessTier.DisposeReader(rd1);
    //    }
    //    catch (Exception ex)
    //    {
    //        BusinessTier.DisposeConnection(conn);
    //        InsertLogAuditTrail(Session["sesUserID"].ToString(), "OrderForm", "Cbo_Customer_load", ex.ToString(), "Audit");
    //    }
    //}

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

            sql1 = "  select Quotation_Id,Quotation_No,Quotation_Date from Quotation where QUOT_STATUS='PENDING' and DELETED=0 and  Branch_id='" + Session["sesBranchID"].ToString().Trim() + "'  and  Customer_ID='" + cboCustomerId.SelectedValue + "' Group by Quotation_Id,Quotation_No,Quotation_Date  order by Quotation_No";
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

    }

    protected void linkAddNew_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery, true);
    }

    protected void cboJobNo_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "select distinct(jobno) as refno FROM Vw_CRMReceiveEquipmentCustTransDetails WHERE Deleted=0 and invoiceflag<>1 and Delivery=1 and Calibration ='complete' and Quotation_Id = '" + cboQuotationNo.SelectedValue.ToString() + "' group by RECEIVED_TRANS_DETAIL_ID,jobno,runningno";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["refno"].ToString(); //+ "-" + row["runningno"].ToString();
                //item.Value = row["RECEIVED_TRANS_DETAIL_ID"].ToString();
                //item.Attributes.Add("Jobno1", row["Jobno"].ToString());
                //item.ToolTip = row["Jobno"].ToString();
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
        try
        {
            RadGridInvoice.DataSource = DataSourceHelper("T", "0");
            RadGridInvoice.Rebind();

        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invocie", "cboJobNo_SelectedIndexChanged", ex.ToString(), "Audit");
        }
    }

    //protected void Invoicelist()
    //{

    //    try
    //    {

    //        SqlConnection conn = BusinessTier.getConnection();
    //        conn.Open();
    //        string sql1 = " ";
    //        if (rdoButton.SelectedItem.Text.ToString().Trim() == "Quotation")
    //        {
    //            if (Session["sesBranchID"].ToString().Trim() == "8")
    //                sql1 = " select PO_NUMBER from CRM_ReceivePO where DELETED=0  and Branch_id='6' and  Customer_ID='" + cboCustomerId.SelectedValue + "' and Quotation_id='" + cboQuotationNo.SelectedValue + "' group by PO_NUMBER ";
    //            else

    //                sql1 = " select PO_NUMBER from CRM_ReceivePO where DELETED=0  and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and  Customer_ID='" + cboCustomerId.SelectedValue + "' and Quotation_id='" + cboQuotationNo.SelectedValue + "' group by PO_NUMBER ";
    //        }
    //        else
    //        {
    //            if (Session["sesBranchID"].ToString().Trim() == "8")
    //                sql1 = " select PO_NUMBER from CRM_ReceivePO where DELETED=0  and Branch_id='6' and  Customer_ID='" + cboCustomerId.SelectedValue + "' and CONTEQUIP_ID='" + cboContractNo.SelectedValue + "' and BatchNo='" + cboJobNo.Text.ToString().Trim() + "' order by PO_NUMBER ";
    //            else
    //                sql1 = " select PO_NUMBER from CRM_ReceivePO where DELETED=0  and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and  Customer_ID='" + cboCustomerId.SelectedValue + "' and BatchNo='" + cboJobNo.Text.ToString().Trim() + "' and CONTEQUIP_ID='" + cboContractNo.SelectedValue + "' order by PO_NUMBER ";
    //        }

    //        //     sql1 = " select Received_Trans_Id,BatchNo,Received_date Quotation_Id from CRM_ReceiveEquipment_Trans where DELETED=0 and (Status='PARTIAL' or Status='COMPLETED') and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and  Customer_ID='" + cboCustomerId.SelectedValue + "' and Quotation_id='"+cboQuotationNo.SelectedValue +"' ";

    //        SqlCommand command1 = new SqlCommand(sql1, conn);
    //        SqlDataReader reader1 = command1.ExecuteReader();

    //        if (reader1.Read())
    //        {
    //            txtPOno.Text = reader1["PO_NUMBER"].ToString();
    //        }
    //        BusinessTier.DisposeReader(reader1);
    //        BusinessTier.DisposeConnection(conn);
    //        lblQuatmasterid.Text = cboQuotationNo.SelectedValue;
    //        btnReportQuotaion.Visible = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(9);
    //        //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
    //        InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invocie", "cboJobNo_SelectedIndexChanged", ex.ToString(), "Audit");
    //    }
    //}

    protected void cboContactName_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            string sql1 = " select Contact_Id,CONTACT_PERSON,Department from Master_CustomerContact where DELETED=0 and Customer_ID='" + cboCustomerId.SelectedValue + "' and  [CONTACT_PERSON] LIKE @text + '%' order by CONTACT_PERSON";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["CONTACT_PERSON"].ToString();
                item.Value = row["Contact_Id"].ToString();
                item.Attributes.Add("CONTACT_PERSON", row["CONTACT_PERSON"].ToString());
                item.Attributes.Add("Department", row["Department"].ToString());
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

    protected void RadGridInvoice_ItemDataBound(object sender, GridItemEventArgs e)
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

    protected void RadGridInvoice_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            // RadGridQuot.DataSource = DataSourceHelper("T", "0");
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invocie", "RadGridQuot_NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper(string typeMasterDetail, string strMasterId)
    {
        //   TabContainer1.ActiveTab = TabContainer1.Tabs[1];
        lblStatus.Text = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "";


        if (typeMasterDetail == "T")
        {

            sql = "select JOBNO,RunningNo,Quotation_trans_Id,Received_Trans_Detail_ID,Quotation_Id,Equipment_Name,Seriel_No,fee,Remarks FROM Vw_CRMReceiveEquipmentCustTransDetails WHERE Deleted=0 and delivery=1 and Calibration ='complete' and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and Quotation_Id='" + cboQuotationNo.SelectedValue.ToString() + "' and jobno='" + cboJobNo.Text.ToString().Trim() + "' and Orderno=0 and Invoiceflag=0  order by JOBNO";

        }
        if (typeMasterDetail == "Q")
        {

            sql = "select JOBNO,RunningNo,Quotation_trans_Id,Received_Trans_Detail_ID,Quotation_Id,Equipment_Name,Seriel_No,qty,fee,Remarks, Discount,Price FROM Vw_CRMReceiveEquipmentCustTransDetails WHERE Deleted=0  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and Quotation_Id='" + cboQuotationNo.SelectedValue.ToString() + "' and Orderno=0 and Invoiceflag=0  order by JOBNO";

        }
        //else 
        //{
        //    if (Session["sesBranchID"].ToString().Trim() == "8") ,(Qty-QinvcQty) as RemainingQty 
        //        sql = "select * FROM Vw_Invoice_New WHERE Deleted=0  and branch_Id='6' and Quotation_Id='" + lblQuatmasterid.Text + "' and QInvcflag=0  order by Created_Date desc";
        //    else

        //        sql = "select * FROM Vw_Invoice_New WHERE Deleted=0  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and Quotation_Id='" + lblQuatmasterid.Text + "' and QInvcflag=0   order by Created_Date desc";

        //}


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

    protected void generateInvoiceNo()
    {
        try
        {
            string strbrnchcode = "";
            int intbranchid = 0;
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();

            string sql11 = " ";
            if (Session["sesBranchID"].ToString().Trim() == "8")
                sql11 = " select Branch_ID,Short_Name from VW_Staff_Branch_Department where deleted=0 and branch_Id='6' ";
            else
                sql11 = " select Branch_ID,Short_Name from VW_Staff_Branch_Department where deleted=0 and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and Staff_ID='" + Session["sesUserID"].ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(sql11, conn);
            SqlDataReader reader11 = cmd11.ExecuteReader();
            if (reader11.Read())
            {
                intbranchid = Convert.ToInt32(reader11["Branch_ID"].ToString().Trim());
                strbrnchcode = reader11["Short_Name"].ToString().Trim();
            }
            reader11.Close();

            string StrOutRefid = SaveAutoInvoiceTable(strbrnchcode);

            //if (rdoButton.SelectedItem.Text.ToString().Trim() == "Contract")
            //{
            //    if ((cboContractNo.Text.ToString().Trim() == "CONTRACT NO") || (cboContractNo.Text.ToString().Trim() == "NO CONTRACT") || (string.IsNullOrEmpty(cboContractNo.SelectedValue.ToString().Trim())))
            //    {
            //        ShowMessage(86);
            //        // e.Canceled = true;
            //        return;
            //    }

            //}

            //else
            //{
            //    if ((cboContractNo.SelectedValue.ToString().Trim() == "0") || (string.IsNullOrEmpty(cboContractNo.SelectedValue.ToString().Trim())))
            //    {
            //        cboContractNo.SelectedValue = "0";
            //    }

            //}


            lblinvoiceno.Text = StrOutRefid.ToString().Trim();


        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message.ToString();
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invoice", "Generateinvoiceno", ex.ToString(), "Audit");
        }

    }

    protected string SaveAutoInvoiceTable(string strBranchCode)
    {

        //Generate Job Number-----------------------------------------------------
        string generateInvoiceNo = "";

        DateTime CurrDateTime = DateTime.Now;
        string strCurrYear = CurrDateTime.Year.ToString().Trim();

        string strgetID = "0";
        string strgetAutoNo = "0";
        string strgetYear = "0";
        string strLastAutoNo = "0";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "";
        sql = "select * FROM Invoice_AutoNo WHERE BranchId = '" + Session["sesBranchID"].ToString().Trim() + "' and Year_Val = '" + strCurrYear.ToString().Trim() + "'";
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader readergetID = command.ExecuteReader();
        if (readergetID.Read())
        {
            strgetID = readergetID["InvoiceAutoId"].ToString().Trim();
            if (!(string.IsNullOrEmpty(readergetID["AutoNo"].ToString().Trim())))
                strgetAutoNo = readergetID["AutoNo"].ToString().Trim();
            strLastAutoNo = readergetID["AutoNo"].ToString().Trim();
            strgetYear = readergetID["Year_Val"].ToString().Trim();
        }
        BusinessTier.DisposeReader(readergetID);
        BusinessTier.DisposeConnection(conn);

        string stryear = strCurrYear.Substring(2);

        if ((strgetID.ToString() == "0") || (string.IsNullOrEmpty(strgetID.ToString())))
        {
            SaveAutoInvoiceTable1(Session["sesBranchID"].ToString().Trim(), "1", strCurrYear.ToString().Trim(), strLastAutoNo.ToString().Trim(), "Insert");
            // strgeneratingQuotNo = strBranchCode.ToString().Trim() + "/" + "1" + "/" + strCurrYear.ToString().Trim();
            generateInvoiceNo = "INV/BMCL/" + stryear + "/00" + 1;

        }
        else
        {
            if (strgetYear.ToString() == strCurrYear.ToString())
            {
                Int32 intAutono = Int32.Parse(strgetAutoNo.ToString().Trim());
                Int32 intAutoNoInc = intAutono + 1;

                SaveAutoInvoiceTable1(Session["sesBranchID"].ToString().Trim(), intAutoNoInc.ToString().Trim(), strCurrYear.ToString().Trim(), strLastAutoNo.ToString().Trim(), "Update");

                string maxid = intAutoNoInc.ToString().Trim();
                if (maxid.Length == 2)
                {

                    generateInvoiceNo = "INV/BMCL/" + stryear + "/0" + intAutoNoInc;

                }
                else if (maxid.Length == 1)
                {
                    generateInvoiceNo = "INV/BMCL/" + stryear + "/00" + intAutoNoInc;

                }
                else
                {
                    generateInvoiceNo = "INV/BMCL/" + stryear + "/" + intAutoNoInc;
                }
            }
            else
            {
                SaveAutoInvoiceTable1(Session["sesBranchID"].ToString().Trim(), "1", strCurrYear.ToString().Trim(), strLastAutoNo.ToString().Trim(), "Insert");

                generateInvoiceNo = "INV/BMCL/" + stryear + "/00" + 1;
            }

        }

        return generateInvoiceNo;

    }

    protected void SaveAutoInvoiceTable1(string strBranchId, string strAutoNo, string strYear, string strLastAutoNo, string saveFlag)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flagMrvAuto = BusinessTier.saveInvoiceAuto(conn, strBranchId.ToString().Trim(), strAutoNo.ToString().Trim(), strYear.ToString().Trim(), strLastAutoNo.Trim(), saveFlag.ToString().Trim());
            BusinessTier.DisposeConnection(conn);
        }

        catch (Exception ex)
        {
            lblStatus.Text = ex.Message.ToString();
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invoice", "SaveAutoInvoiceTable1", ex.ToString(), "Audit");
        }

    }

    protected void btnInvoice_OnClick(object sender, EventArgs e)
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
            if (string.IsNullOrEmpty(lblinvoiceno.Text))
            {
                generateInvoiceNo();
            }

            String invdt = DtInvoicedt.SelectedDate.ToString();
            DateTime pdt = DateTime.Parse(invdt);
            invdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year + " 00:00:00";

            conn.Open();



            foreach (GridDataItem grdItem in RadGridInvoice.Items)
            {
                CheckBox ChkSelect = (CheckBox)grdItem.FindControl("ChkSelect");

                int intTrackId = Convert.ToInt32(ChkSelect.ToolTip.ToString().Trim());

                string sql = "select * FROM invoice where deleted=0 and RECEIVED_TRANS_DETAIL_ID='" + Convert.ToInt32(ChkSelect.ToolTip.ToString()) + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    reader.Close();
                    lblStatus.Text = "Already Inserted";
                    lblStatus.ForeColor = Color.YellowGreen;
                }
                else
                {
                    if (ChkSelect.Checked)
                    {
                        reader.Close();
                        BusinessTier.Invoice_Save(conn, cboJobNo.Text.ToString(), lblinvoiceno.Text.ToString(), invdt.ToString(), Convert.ToInt32(cboCustomerId.SelectedValue.ToString()), Convert.ToInt32(cboQuotationNo.SelectedValue.ToString()), Convert.ToInt32(ChkSelect.ToolTip.ToString()), Session["sesUserID"].ToString().Trim());
                        lblStatus.Text = "Successfully Invoice Inserted";
                        lblStatus.ForeColor = Color.YellowGreen;
                    }
                    // btnInvoice.Enabled = false;
                    else
                    {
                        lblStatus.Text = "Please Select Any One Job";
                        lblStatus.ForeColor = Color.Orange;
                       // BusinessTier.Invoice_Save(conn, cboJobNo.Text.ToString(), lblinvoiceno.Text.ToString(), invdt.ToString(), Convert.ToInt32(cboCustomerId.SelectedValue.ToString()), Convert.ToInt32(cboQuotationNo.SelectedValue.ToString()), Convert.ToInt32(cboJobNo.SelectedValue.ToString()), Session["sesUserID"].ToString().Trim());
                    }
                }

            }


            //lblStatus.Text = "Successfully Invoice Inserted";
            BusinessTier.DisposeConnection(conn);
            btnReport.Enabled = true;
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message.ToString();
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invoice", "btnInvoice_OnClick", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }


    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboJobNo.Text))
        {
            lblStatus.Text = "JobNo Field Cannot be Empty";
            return;
        }

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Invoice.aspx?param1=" + cboQuotationNo.Text.ToString().Trim() + "&param2=" + lblinvoiceno.Text.ToString().Trim() + "');", true);
    }

}

