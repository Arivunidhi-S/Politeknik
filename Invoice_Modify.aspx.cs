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

public partial class Invoice_Modify : System.Web.UI.Page
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
                //DtInvoicedt.SelectedDate = DateTime.Now;
                //DateTime dt = DateTime.Now;
                //DateTime dt1 = dt.AddDays(30);

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

    protected void cboInvoice_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "select distinct(invoice_no) as refno FROM Vw_Invoice_Details WHERE Deleted=0 and invoiceflag=1 and Delivery=1 and Calibration ='complete' order by refno desc";
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

    protected void cboInvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();

            string sql = "select * FROM Vw_Invoice_Details WHERE Deleted=0 and delivery=1 and Calibration ='complete' and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and invoice_No='" + cboInvoice.Text.ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                txtCustomer.Text = rd["Customer_Name"].ToString();
                txtQuotation.Text = rd["Quotation_No"].ToString();
                txtReference.Text = rd["jobno"].ToString();
                txtQuotation.ToolTip = rd["Quotation_Id"].ToString();
            }
            rd.Close();
            BusinessTier.DisposeConnection(conn);
            RadGridInvoice.DataSource = DataSourceHelper("T", "0");
            RadGridInvoice.Rebind();
            btnReport.Enabled = true;
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invocie", "cboJobNo_SelectedIndexChanged", ex.ToString(), "Audit");
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
    
    protected void RadGridInvoice_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RECEIVED_TRANS_DETAIL_ID"].ToString();
            int flg = BusinessTier.Invoice_Delete(conn, Convert.ToInt32(ID), Convert.ToInt32(txtQuotation.ToolTip.ToString()));
            BusinessTier.DisposeConnection(conn);
            RadGridInvoice.DataSource = DataSourceHelper("T", "0");
            RadGridInvoice.Rebind();
        }
        catch (Exception ex)
        {
            e.Canceled = true;
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invoice_Modify", "RadGridQuot_DeleteCommand", ex.ToString(), "Audit");
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

        lblStatus.Text = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "";
        if (typeMasterDetail == "T")
        {
            sql = "select JOBNO,RunningNo,Quotation_trans_Id,Received_Trans_Detail_ID,Equipment_Name,Seriel_No,fee,Remarks FROM Vw_Invoice_Details WHERE Deleted=0 and delivery=1 and Calibration ='complete' and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and invoice_No='" + cboInvoice.Text.ToString() + "' and Orderno=0 and Invoiceflag=1  order by JOBNO,RunningNo";
        }
        if (typeMasterDetail == "Q")
        {
            sql = "select JOBNO,RunningNo,Quotation_trans_Id,Received_Trans_Detail_ID,Quotation_Id,Equipment_Name,Seriel_No,qty,fee,Remarks, Discount,Price FROM Vw_CRMReceiveEquipmentCustTransDetails WHERE Deleted=0  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' andinvoice_No='" + cboInvoice.Text.ToString() + "' and Orderno=0 and Invoiceflag=0  order by JOBNO";
        }

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

    ////////////////////////////Report////////////////////////////////////////////////////

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
        if (Session["sesBranchID"].ToString().Trim() == "8")
            sql = "select * FROM Invoice_AutoNo WHERE BranchId = '6' and Year_Val = '" + strCurrYear.ToString().Trim() + "'";
        else

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



        //string sql1 = "select max(Invoice_Id)as maxval from Invoice";
        //SqlCommand cmd1 = new SqlCommand(sql1, conn);
        //SqlDataReader rd1 = cmd1.ExecuteReader();

        //rd1.Read();
        //if (string.IsNullOrEmpty(rd1["maxval"].ToString()))
        //{
        //    intAutoNoInc = intAutono;
        //}
        //else
        //{
        //    intAutoNoInc = Convert.ToInt32(rd1["maxval"].ToString()) + 1;
        //}

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

                    generateInvoiceNo = "INV/BMCL/" + stryear + "/" + intAutoNoInc;
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
        //SqlConnection conn = BusinessTier.getConnection();
        //try
        //{
        //    if (string.IsNullOrEmpty(cboCustomerId.Text))
        //    {
        //        lblStatus.Text = "Customer Field Cannot be Empty";
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(cboQuotationNo.Text))
        //    {
        //        lblStatus.Text = "Quotation Field Cannot be Empty";
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(cboJobNo.Text))
        //    {
        //        lblStatus.Text = "JobNo Field Cannot be Empty";
        //        return;
        //    }
        //    generateInvoiceNo();
        //    String invdt = DtInvoicedt.SelectedDate.ToString();
        //    DateTime pdt = DateTime.Parse(invdt);
        //    invdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year + " 00:00:00";

        //    conn.Open();

        //    foreach (GridDataItem grdItem in RadGridInvoice.Items)
        //    {
        //        CheckBox ChkSelect = (CheckBox)grdItem.FindControl("ChkSelect");

        //        int intTrackId = Convert.ToInt32(ChkSelect.ToolTip.ToString().Trim());

        //        if (ChkSelect.Checked)
        //        {
        //            BusinessTier.Invoice_Save(conn, cboJobNo.Text.ToString(), lblinvoiceno.Text.ToString(), invdt.ToString(), Convert.ToInt32(cboCustomerId.SelectedValue.ToString()), Convert.ToInt32(cboQuotationNo.SelectedValue.ToString()), Convert.ToInt32(ChkSelect.ToolTip.ToString()), Session["sesUserID"].ToString().Trim());
        //            lblStatus.Text = "Successfully Invoice Inserted";
        //        }
        //        //else
        //        //{
        //        //    BusinessTier.Invoice_Save(conn, cboJobNo.Text.ToString(), lblinvoiceno.Text.ToString(), invdt.ToString(), Convert.ToInt32(cboCustomerId.SelectedValue.ToString()), Convert.ToInt32(cboQuotationNo.SelectedValue.ToString()), Convert.ToInt32(cboJobNo.SelectedValue.ToString()), Session["sesUserID"].ToString().Trim());
        //        //}
        //    }




        //    lblStatus.Text = "Successfully Invoice Inserted";
        //    BusinessTier.DisposeConnection(conn);
        //    btnReport.Enabled = true;
        //}
        //catch (Exception ex)
        //{
        //    lblStatus.Text = ex.Message.ToString();
        //    InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invoice", "btnInvoice_OnClick", ex.ToString(), "Audit");
        //}
        //finally
        //{
        //    BusinessTier.DisposeConnection(conn);
        //}


    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboInvoice.Text))
        {
            lblStatus.Text = "Select Invoice Field";
            return;
        }

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Invoice.aspx?param1=0&param2=" + cboInvoice.Text.ToString().Trim() + "');", true);
    }

}

