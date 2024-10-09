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



public partial class Calibration : System.Web.UI.Page
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
            //txtjobno_load();
            btnReport.Enabled = false;
            btnStickerReport.Enabled = false;


            DtFromDate.SelectedDate = DateTime.Now;
            DtToDate.SelectedDate = DateTime.Now.AddYears(1);

        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    //protected void txtjobno_load()
    //{
    //    SqlConnection conn = BusinessTier.getConnection();
    //    conn.Open();
    //    try
    //    {

    //        string sql = "select max(CalibrationAuto_ID)as maxval from Calibration";
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

    //        txtcertno.Text = "PSA/BMCL/" + DateTime.Today.ToString("yy/" + val);
    //        txtstickno.Text = val.ToString();
    //        BusinessTier.DisposeReader(rd1);
    //    }
    //    catch (Exception ex)
    //    {
    //        BusinessTier.DisposeConnection(conn);
    //        InsertLogAuditTrail(Session["sesUserID"].ToString(), "OrderForm", "Cbo_Customer_load", ex.ToString(), "Audit");
    //    }
    //}

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

            //string[] files = System.IO.Directory.GetFiles(@"C:\Testing");

           
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Calibration", "Page_Load", ex.ToString(), "Audit");
            Response.Redirect("Login.aspx");
        }
    }

    protected void DtFromDate_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
    {

        DateTime dt = DtFromDate.SelectedDate.Value;
        DateTime dt1 = dt.AddYears(1);
        DtToDate.SelectedDate = dt1;
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

            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingPO", "cboCustomer_OnItemsRequested", ex.ToString(), "Audit");

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

            string sql1 = "select distinct(Quotation_No),Quotation_Date,Quotation_id from VW_QuotCalib where DELETED=0  and  Customer_ID='" + strCustomerID + "' and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and calibration is null and delivery=0 and invoiceflag=0 and  QUOT_STATUS='PENDING' order by Quotation_No";
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

    protected void cboJobno_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "select RECEIVED_TRANS_DETAIL_ID,jobno,runningno FROM Vw_CRMReceiveEquipmentCustTransDetails WHERE Deleted=0 and orderno=0 and Calibration is null and delivery=0 and invoiceflag=0 and assignid='" + Session["sesUserID"].ToString().Trim() + "'  and Quotation_Id = '" + cboQuotationNo.SelectedValue.ToString() + "' group by RECEIVED_TRANS_DETAIL_ID,jobno,runningno ";
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
                txtCustomer.ToolTip = row["Jobno"].ToString();
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

    protected void cboFolder_OnItemsRequested(object sender, EventArgs e)
    {
        cbofolder.Items.Clear();
        string strpath = WebConfigurationManager.AppSettings["WC_POPath"].ToString();
        DirectoryInfo obj = new DirectoryInfo(strpath);//you can set your directory path here
        DirectoryInfo[] folders = obj.GetDirectories();
        cbofolder.DataSource = folders;
        cbofolder.DataValueField = "Name";
        cbofolder.DataBind();
    }

    protected void cboselectCalib_OnItemsRequested(object sender, EventArgs e)
    {
        RadComboBoxItem item = new RadComboBoxItem();

        //cboQuotationNo.Items.Add(files);
        string strpath = WebConfigurationManager.AppSettings["WC_POPath"].ToString() + cbofolder.Text.ToString();
        cboCalibSheet.Items.Clear();
        DirectoryInfo dirInfo = new DirectoryInfo(strpath);
        FileInfo[] fi = dirInfo.GetFiles();
        cboCalibSheet.DataSource = fi;
        cboCalibSheet.DataValueField = "Name";
        cboCalibSheet.DataBind();
    }

    protected void cbotype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtjobno.Text.ToString()))
        {
            lblStatus.Text = "Please Select JobNo";
            return;
        }
        if (string.IsNullOrEmpty(txtcertno.Text.ToString()))
        {
            generateCertificateNo();
        }
        if (cbotype.Text.ToString() == "Accredited")
        {
            txtcertno.Text = txtcertno.ToolTip.ToString();
        }
        else
        {
            txtcertno.Text = txtcertno.ToolTip.ToString() + "-NA";
        }


    }

    protected void cbojobNo_SelectedIndexChanged(object sender, EventArgs e)
    {

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string sql = "";
            // string jobno = txtjobno.SelectedItem.Attributes["Jobno1"].ToString();
            sql = "select EQUIPMENT_ID,EQUIPMENT_NAME,model,maker,seriel_no,DEPT_NAME,ADDR_LINE1,ADDR_LINE2,ADDR_LINE3,CITY,STATE,COUNTRY,POSTAL_CODE FROM Vw_CRMReceiveEquipmentCustTransDetails WHERE Deleted=0 and RECEIVED_TRANS_DETAIL_ID='" + txtjobno.SelectedValue.ToString() + "'";
            SqlCommand cmd2 = new SqlCommand(sql, conn);
            SqlDataReader rd2 = cmd2.ExecuteReader();
            if (rd2.Read())
            {
                txtequipment.Text = rd2["EQUIPMENT_NAME"].ToString();
                txtequipment.ToolTip = rd2["EQUIPMENT_ID"].ToString();
                lblcuseqimodel.ToolTip = rd2["model"].ToString();
                lblcuseqimaker.ToolTip = rd2["maker"].ToString();
                txtModel.Text = rd2["model"].ToString();
                txtMaker.Text = rd2["maker"].ToString();
                lbleqiserialno.ToolTip = rd2["seriel_no"].ToString();
                lblselectCalib.ToolTip = rd2["DEPT_NAME"].ToString();

                lblSelFolder.ToolTip = rd2["ADDR_LINE1"].ToString();
                lblCertificateNo.ToolTip = rd2["ADDR_LINE2"].ToString();
                lblStickerNo.ToolTip = rd2["ADDR_LINE3"].ToString();
                lblType.ToolTip = rd2["POSTAL_CODE"].ToString() + "," + rd2["CITY"].ToString();
                lblEquipment.ToolTip = rd2["STATE"].ToString() + "," + rd2["COUNTRY"].ToString();


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
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Calibration", "cboQuotationNo_SelectedIndexChanged", ex.ToString(), "Audit");
        }
    }

    protected void linkDownload_OnClick(object sender, EventArgs e)
    {
        try
        {


            //if (!(string.IsNullOrEmpty(linkDownload5.Text.ToString().Trim())))
            //{
            //    string strPah = WebConfigurationManager.AppSettings["WC_POGetPath"].ToString();
            //    string strLink = strPah + linkDownload5.Text.Trim();
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenNewTab", "window.open('" + strLink + "','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=800,height=1200');", true);
            //}
        }
        catch (Exception Ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void linkAddNew_OnClick(object sender, EventArgs e)
    {
        btnRegister.Enabled = false;
        Response.Redirect(Request.Url.PathAndQuery, true);
    }

    protected void RadGridQuot_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "RowClick" && e.Item is GridDataItem)
        {
            e.Item.Selected = true;
            GridEditFormItem editedItem = e.Item as GridEditFormItem;
            Label linkDownload5 = (Label)e.Item.FindControl("linkDownload5");

            string strPah = WebConfigurationManager.AppSettings["WC_DOWPath"].ToString();
            string strLink = strPah + linkDownload5.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenNewTab", "window.open('" + strLink + "','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=500,height=500');", true);
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
            RadGridQuot.DataSource = DataSourceHelper();
        }
        catch (Exception ex)
        {
            //ShowMessage(9);
            //InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quoatation", "RadGridQuot_NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        //   TabContainer1.ActiveTab = TabContainer1.Tabs[1];
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "";
        //if (t == "T")
        sql = "select * FROM Vw_CRMReceiveEquipmentCustTransDetails_Calib where RECEIVED_TRANS_DETAIL_ID='" + txtjobno.SelectedValue.ToString().Trim() + "' and deleted=0 order by CalibDate desc ";
        //else
        //    sql = "select * FROM Vw_CRMReceiveEquipmentCustTransDetails_Calib where RECEIVED_TRANS_DETAIL_ID='' and deleted=0 order by CalibDate desc ";
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

    //protected void btnReport_Click(object sender, EventArgs e)
    //{
    //    //string strReport = "Quotation";
    //    //string parameter = "Report_Quotation.aspx?" + "param1=" + lblQuatmasterid.Text.ToString().Trim();
    //    //Response.Redirect(parameter);
    //    SqlConnection conn = BusinessTier.getConnection();
    //    conn.Open();
    //    string quoatStatus = null;
    //    string sql2 = "select Quot_Status from Quotation where deleted=0 and status='Complete'";
    //    SqlCommand command1 = new SqlCommand(sql2, conn);
    //    SqlDataReader reader1 = command1.ExecuteReader();
    //    if (reader1.Read())
    //    {
    //        quoatStatus = reader1["Quot_Status"].ToString();
    //    }
    //    BusinessTier.DisposeReader(reader1);

    //    if (quoatStatus == "DISCOUNT")
    //    {
    //        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('You Can not generate the Quotation.Because this quotation is Waiting for the Discount ( " + txtDiscountApprov.Text + " )% approval');", true);
    //        return;
    //    }
    //    else if (quoatStatus == "PENDING")
    //    {
    //        // ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Quotation.aspx?param1=" + lblQuatmasterid.Text.ToString().Trim() + "');", true);
    //    }


    //}

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtjobno.Text))
        {
            lblStatus.Text = "JobNo Field Cannot be Empty";
            return;
        }
        // string jbno = txtjobno.Text.ToString().Trim().Split('-')[0];// +"/" + cboJobNo.Text.ToString().Trim().Split('/')[1];
        string jbno = txtjobno.SelectedValue.ToString().Trim();
        string rnno = txtequipment.ToolTip.ToString();

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_AfterCalibration.aspx?param1=" + jbno.ToString().Trim() + "&param2=" + rnno.ToString().Trim() + "');", true);
    }

    protected void btnStickerReport_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtjobno.Text))
        {
            lblStatus.Text = "JobNo Field Cannot be Empty";
            return;
        }

        //string jbno = txtjobno.Text.ToString().Trim().Split('-')[0];// +"/" + cboJobNo.Text.ToString().Trim().Split('/')[1];
        string jbno = txtjobno.SelectedValue.ToString().Trim();
        string rnno = txtequipment.ToolTip.ToString();

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Sticker.aspx?param1=" + jbno.ToString().Trim() + "&param2=" + rnno.ToString().Trim() + "');", true);
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtjobno.Text.ToString()))
        {
            lblStatus.Text = "Please Select JobNo";
            return;
        }
        if (string.IsNullOrEmpty(lblCertificate.Text.ToString()))
        {
            lblStatus.Text = "Please Upload Calibration Sheet";
            return;
        }
        if (string.IsNullOrEmpty(txtcertno.Text.ToString()))
        {
            lblStatus.Text = "Could not Empty Certificate Field";
            return;
        }

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string strPah = WebConfigurationManager.AppSettings["WC_POPath"].ToString();
            string CertificateFileName = lblCertificate.Text.ToString(), DatasheetFileName = "", MUSheetFileName = "";
            //DatasheetFileName = txtowner.Text.ToString();
            String caldt = DtFromDate.SelectedDate.ToString();
            DateTime pdt = DateTime.Parse(caldt);
            caldt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year + " 00:00:00";

            String nxtdt = DtToDate.SelectedDate.ToString();
            DateTime idt = DateTime.Parse(nxtdt);
            nxtdt = idt.Month + "/" + idt.Day + "/" + idt.Year + " 00:00:00";

            BusinessTier.Calib_Save(conn, Convert.ToInt32(txtjobno.SelectedValue.ToString()), Convert.ToInt32(cboQuotationNo.SelectedValue.ToString()), Convert.ToInt32(CboCustomer.SelectedValue.ToString()), Convert.ToInt32(txtequipment.ToolTip.ToString()), caldt.ToString(), nxtdt.ToString(), txtcertno.Text.ToString(), txtstickno.Text.ToString(), CertificateFileName.ToString(), DatasheetFileName.ToString(), MUSheetFileName.ToString(), Session["sesUserID"].ToString().Trim());
            BusinessTier.DisposeConnection(conn);
            lblStatus.Text = "Successfully Calibration Inserted";
            lblStatus.ForeColor = Color.Yellow;
            lblCertificate.Visible = false;

            CertificateUpload.Visible = true;

            btnReport.Enabled = true;
            btnRegister.Enabled = false;
            btnStickerReport.Enabled = true;
            RadGridQuot.DataSource = DataSourceHelper();
            RadGridQuot.Rebind();
        }

        catch (Exception ex)
        {
            ShowMessage(5);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Calibration", "Insert", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }



    }

    protected void linkCertificate_Click(object sender, EventArgs e)
    {
        string strPah = WebConfigurationManager.AppSettings["WC_DOPath"].ToString();
        string strLink = strPah + lblCertificate.Text;
        Response.Clear();
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;filename=" + lblCertificate.Text + "");
        Response.WriteFile(strLink);
        Response.End();

    }

    protected void btnCertificate_Click(object sender, EventArgs e)
    {
        try
        {
            string strpath = WebConfigurationManager.AppSettings["WC_DOPath"].ToString(), CertificateFileName = "";

            ////************************>>> CertificateUpload <<<<************************\\\\

            if (CertificateUpload.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile f in CertificateUpload.UploadedFiles)
                {
                    CertificateFileName = f.GetName().ToString().Trim();
                    f.SaveAs(strpath.ToString() + f.GetName(), true);

                    CertificateUpload.Visible = false;
                    lblCertificate.Text = CertificateFileName.ToString();
                    lblCertificate.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Calibration", "btnCertificate_Click", ex.ToString(), "Audit");
        }


    }

    protected void btnCalibSheet_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtjobno.Text.ToString()))
        {
            lblStatus.Text = "Please Select JobNo";
            return;
        }

        Excel.Application oXL;
        Excel._Workbook mWorkBook;
        Excel._Worksheet mWSheet1;
        Excel.Sheets mWorkSheets;

        object misValue = System.Reflection.Missing.Value;

        try
        {
            string strpath = WebConfigurationManager.AppSettings["WC_POPath"].ToString() + cbofolder.Text.ToString()+"\\";
            string path = "";
            path = strpath.ToString() + cboCalibSheet.Text.ToString();
            oXL = new Microsoft.Office.Interop.Excel.Application();

            oXL.DisplayAlerts = false;
            mWorkBook = oXL.Workbooks.Open(path, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            mWorkSheets = mWorkBook.Worksheets;
            mWSheet1 = (Excel.Worksheet)mWorkSheets.get_Item("Data1");

            String caldt = DtFromDate.SelectedDate.ToString();
            DateTime pdt = DateTime.Parse(caldt);
            caldt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year;

            mWSheet1.Cells[9, 5] = caldt.ToString();
            //mWSheet1.Cells[9, 5] = DtFromDate.SelectedDate.ToString();
            mWSheet1.Cells[11, 5] = lblselectCalib.ToolTip.ToString();
            mWSheet1.Cells[14, 5] = CboCustomer.Text.ToString();
            mWSheet1.Cells[9, 14] = txtcertno.Text.ToString();
            mWSheet1.Cells[11, 14] = txtstickno.Text.ToString();
            mWSheet1.Cells[14, 14] = txtjobno.Text.ToString();
            mWSheet1.Cells[21, 5] = txtequipment.Text.ToString();

            mWSheet1.Cells[22, 5] = lblcuseqimaker.ToolTip.ToString();
            mWSheet1.Cells[21, 14] = lblcuseqimodel.ToolTip.ToString();
            mWSheet1.Cells[22, 14] = lbleqiserialno.ToolTip.ToString();

            mWSheet1.Cells[15, 5] = lblSelFolder.ToolTip.ToString();
            mWSheet1.Cells[16, 5] = lblCertificateNo.ToolTip.ToString();
            mWSheet1.Cells[17, 5] = lblStickerNo.ToolTip.ToString();
            mWSheet1.Cells[18, 5] = lblType.ToolTip.ToString();
            mWSheet1.Cells[19, 5] = lblEquipment.ToolTip.ToString();


            mWorkBook.Save();

            mWorkBook.Close(misValue, misValue, misValue);


            mWSheet1 = null;
            mWorkBook = null;
            oXL.Quit();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            string strLink = path;
            string downfile = string.Empty;
            string[] ret = cboCalibSheet.Text.Split('.');
            if (ret[1].ToString() == "xlsx")
            {
                downfile = ret[0].ToString() + "-" + txtstickno.Text.ToString() + ".xlsx";
            }
            else
            {
                downfile = ret[0].ToString() + "-" + txtstickno.Text.ToString() + ".xls";
            }
            //string downfile = cboCalibSheet.Text.ToString();
            //string result = downfile.Substring(0, downfile.Length - downfile.Length);
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=" + downfile.ToString() + "");
            Response.WriteFile(strLink);
            Response.End();
        }
        catch (Exception ex)
        {
            //lblStatus.Text = ex.ToString();
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Calibration", "btnCalibSheet_Click", ex.ToString(), "Audit");
        }
            }

    //protected void Datasheet_Click(object sender, EventArgs e)
    //{
    //    Excel.Application oXL;
    //    Excel._Workbook mWorkBook;
    //    Excel._Worksheet mWSheet1;
    //    Excel.Sheets mWorkSheets;

    //    object misValue = System.Reflection.Missing.Value;

    //    try
    //    {
    //        string strpath = WebConfigurationManager.AppSettings["WC_POPath"].ToString(), CertificateFileName = "";
    //       // string strpath2 = WebConfigurationManager.AppSettings["WC_DOPath"].ToString();

    //        ////************************>>> DatasheetUpload <<<<************************\\\\
    //        string path = "",path2="";
    //        if (DatasheetUpload.UploadedFiles.Count > 0)
    //        {
    //            foreach (UploadedFile f in DatasheetUpload.UploadedFiles)
    //            {
    //                CertificateFileName = f.GetName().ToString().Trim();
    //                f.SaveAs(strpath.ToString() + f.GetName(), true);


    //                path = strpath.ToString() + CertificateFileName.ToString();
    //                oXL = new Microsoft.Office.Interop.Excel.Application();

    //                oXL.DisplayAlerts = false;
    //                mWorkBook = oXL.Workbooks.Open(path, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
    //                mWorkSheets = mWorkBook.Worksheets;
    //                //Get the allready exists sheet
    //                mWSheet1 = (Excel.Worksheet)mWorkSheets.get_Item("Data1");

    //                mWSheet1.Cells[9, 5] = DtFromDate.SelectedDate.ToString();
    //                mWSheet1.Cells[11, 5] = CboCustomer.Text.ToString();
    //                mWSheet1.Cells[9, 14] = txtcertno.Text.ToString();
    //                mWSheet1.Cells[11, 14] = txtstickno.Text.ToString();
    //                mWSheet1.Cells[14, 14] = txtjobno.Text.ToString();
    //                mWSheet1.Cells[21, 5] = txtequipment.Text.ToString();

    //                mWSheet1.Cells[22, 5] = lblcuseqimaker.ToolTip.ToString();
    //                mWSheet1.Cells[21, 14] = lblcuseqimodel.ToolTip.ToString();
    //                mWSheet1.Cells[22, 14] = lbleqiserialno.ToolTip.ToString();
    //                DatasheetUpload.Visible = false;
    //                path2 = txtstickno.Text.ToString() + "Datasheet.xls";
    //                lblDatasheet.Text = path2.ToString();
    //                lblDatasheet.Visible = true;
    //                mWorkBook.SaveAs(path2, Excel.XlFileFormat.xlWorkbookNormal,
    //                misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive,
    //                misValue, misValue, misValue,
    //                misValue, misValue);
    //                mWorkBook.Close(misValue, misValue, misValue);


    //                mWSheet1 = null;
    //                mWorkBook = null;
    //                oXL.Quit();
    //                GC.WaitForPendingFinalizers();
    //                GC.Collect();
    //                GC.WaitForPendingFinalizers();
    //                GC.Collect();


    //            }
    //        }



    //    }
    //    catch (Exception ex)
    //    {
    //       // lblStatus.Text = ex.ToString();
    //        InsertLogAuditTrail(Session["sesUserID"].ToString(), "Calibration", "Datasheet_Click", ex.ToString(), "Audit");
    //    }


    //}


    //protected void MUSheet_Click(object sender, EventArgs e)
    //{
    //    Excel.Application oXL;
    //    Excel._Workbook mWorkBook;
    //    Excel._Worksheet mWSheet1;
    //    Excel.Sheets mWorkSheets;

    //    object misValue = System.Reflection.Missing.Value;

    //    try
    //    {
    //        string strpath = WebConfigurationManager.AppSettings["WC_POPath"].ToString(), CertificateFileName = "";
    //        // string strpath2 = WebConfigurationManager.AppSettings["WC_DOPath"].ToString();

    //        ////************************>>> MUSheetUpload <<<<************************\\\\
    //        string path = "", path2 = "";
    //        if (MUSheetUpload.UploadedFiles.Count > 0)
    //        {
    //            foreach (UploadedFile f in MUSheetUpload.UploadedFiles)
    //            {
    //                CertificateFileName = f.GetName().ToString().Trim();
    //                f.SaveAs(strpath.ToString() + f.GetName(), true);


    //                path = strpath.ToString() + CertificateFileName.ToString();
    //                oXL = new Microsoft.Office.Interop.Excel.Application();

    //                oXL.DisplayAlerts = false;
    //                mWorkBook = oXL.Workbooks.Open(path, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
    //                mWorkSheets = mWorkBook.Worksheets;
    //                //Get the allready exists sheet
    //                mWSheet1 = (Excel.Worksheet)mWorkSheets.get_Item("Data1");

    //                mWSheet1.Cells[9, 5] = DtFromDate.SelectedDate.ToString();
    //                mWSheet1.Cells[11, 5] = CboCustomer.Text.ToString();
    //                mWSheet1.Cells[9, 14] = txtcertno.Text.ToString();
    //                mWSheet1.Cells[11, 14] = txtstickno.Text.ToString();
    //                mWSheet1.Cells[14, 14] = txtjobno.Text.ToString();
    //                mWSheet1.Cells[21, 5] = txtequipment.Text.ToString();

    //                mWSheet1.Cells[22, 5] = lblcuseqimaker.ToolTip.ToString();
    //                mWSheet1.Cells[21, 14] = lblcuseqimodel.ToolTip.ToString();
    //                mWSheet1.Cells[22, 14] = lbleqiserialno.ToolTip.ToString();
    //                MUSheetUpload.Visible = false;
    //                path2 = txtstickno.Text.ToString() + "MUSheet.xls";
    //                lblMUSheet.Text = path2.ToString();
    //                lblMUSheet.Visible = true;
    //                mWorkBook.SaveAs(path2, Excel.XlFileFormat.xlWorkbookNormal,
    //                misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive,
    //                misValue, misValue, misValue,
    //                misValue, misValue);
    //                mWorkBook.Close(misValue, misValue, misValue);


    //                mWSheet1 = null;
    //                mWorkBook = null;
    //                oXL.Quit();
    //                GC.WaitForPendingFinalizers();
    //                GC.Collect();
    //                GC.WaitForPendingFinalizers();
    //                GC.Collect();


    //            }
    //        }



    //    }
    //    catch (Exception ex)
    //    {
    //        //lblStatus.Text = ex.ToString();
    //        InsertLogAuditTrail(Session["sesUserID"].ToString(), "Calibration", "MUSheet_Click", ex.ToString(), "Audit");
    //    }


    //}

    protected void generateCertificateNo()
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

            string StrOutRefid = SaveAutoCertificateTable(strbrnchcode);
            txtcertno.Text = StrOutRefid.ToString().Trim();
            txtcertno.ToolTip = StrOutRefid.ToString().Trim();


        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message.ToString();
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invoice", "Generateinvoiceno", ex.ToString(), "Audit");
        }

    }

    protected string SaveAutoCertificateTable(string strBranchCode)
    {

        //Generate CertificateNo-----------------------------------------------------
        string generateCertificateNo = "";

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
            sql = "select * FROM Certificate_AutoNo WHERE BranchId = '6' and Year_Val = '" + strCurrYear.ToString().Trim() + "'";
        else

            sql = "select * FROM Certificate_AutoNo WHERE BranchId = '" + Session["sesBranchID"].ToString().Trim() + "' and Year_Val = '" + strCurrYear.ToString().Trim() + "'";
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader readergetID = command.ExecuteReader();
        if (readergetID.Read())
        {
            strgetID = readergetID["CertificateAutoId"].ToString().Trim();
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
            SaveAutoCertificateTable(Session["sesBranchID"].ToString().Trim(), "1", strCurrYear.ToString().Trim(), strLastAutoNo.ToString().Trim(), "Insert");
            generateCertificateNo = "PSA/BMCL/" + stryear + "/00" + 1;
            txtstickno.Text = "00" + 1;

        }
        else
        {
            if (strgetYear.ToString() == strCurrYear.ToString())
            {
                Int32 intAutono = Int32.Parse(strgetAutoNo.ToString().Trim());
                Int32 intAutoNoInc = intAutono + 1;

                SaveAutoCertificateTable(Session["sesBranchID"].ToString().Trim(), intAutoNoInc.ToString().Trim(), strCurrYear.ToString().Trim(), strLastAutoNo.ToString().Trim(), "Update");

                string maxid = intAutoNoInc.ToString().Trim();
                if (maxid.Length == 2)
                {

                    generateCertificateNo = "PSA/BMCL/" + stryear + "/0" + intAutoNoInc;
                    txtstickno.Text = "0" + intAutoNoInc.ToString();
                }
                else if (maxid.Length == 1)
                {
                    generateCertificateNo = "PSA/BMCL/" + stryear + "/00" + intAutoNoInc;
                    txtstickno.Text = "00" + intAutoNoInc.ToString();
                }
                else
                {
                    generateCertificateNo = "PSA/BMCL/" + stryear + "/" + intAutoNoInc;
                    txtstickno.Text = intAutoNoInc.ToString();
                }
            }
            else
            {
                SaveAutoCertificateTable(Session["sesBranchID"].ToString().Trim(), "1", strCurrYear.ToString().Trim(), strLastAutoNo.ToString().Trim(), "Insert");

                generateCertificateNo = "PSA/BMCL/" + stryear + "/00" + 1;
                txtstickno.Text = "00" + 1;
            }

        }

        return generateCertificateNo;

    }

    protected void SaveAutoCertificateTable(string strBranchId, string strAutoNo, string strYear, string strLastAutoNo, string saveFlag)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flagMrvAuto = BusinessTier.saveCertificateAuto(conn, strBranchId.ToString().Trim(), strAutoNo.ToString().Trim(), strYear.ToString().Trim(), strLastAutoNo.Trim(), saveFlag.ToString().Trim());
            BusinessTier.DisposeConnection(conn);
        }

        catch (Exception ex)
        {
            lblStatus.Text = ex.Message.ToString();
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invoice", "SaveAutoInvoiceTable1", ex.ToString(), "Audit");
        }

    }

}