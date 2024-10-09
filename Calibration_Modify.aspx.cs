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

public partial class Calibration_Modify : System.Web.UI.Page
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
            btnDelete.Enabled = false;


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

            // RadComboBoxItem item = new RadComboBoxItem();

            //  cboQuotationNo.Items.Add(files);
            string strpath = WebConfigurationManager.AppSettings["WC_POPath"].ToString();
            cboCalibSheet.Items.Clear();
            DirectoryInfo dirInfo = new DirectoryInfo(strpath);
            FileInfo[] fi = dirInfo.GetFiles();
            cboCalibSheet.DataSource = fi;
            cboCalibSheet.DataValueField = "Name";
            cboCalibSheet.DataBind();

        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void DtFromDate_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
    {

        DateTime dt = DtFromDate.SelectedDate.Value;
        DateTime dt1 = dt.AddYears(1);
        DtToDate.SelectedDate = dt1;
    }

    protected void cboCertificateNo_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "select RECEIVED_TRANS_DETAIL_ID,CertNo FROM Vw_CRMReceiveEquipmentCustTransDetails_Calib WHERE Deleted=0 and Calibration ='Complete' and delivery=0 and invoiceflag=0 group by RECEIVED_TRANS_DETAIL_ID,CertNo order by RECEIVED_TRANS_DETAIL_ID desc";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["CertNo"].ToString();
                item.Value = row["RECEIVED_TRANS_DETAIL_ID"].ToString();
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

    protected void cboCertificateNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string sql = "";
            // string jobno = txtjobno.SelectedItem.Attributes["Jobno1"].ToString();
            sql = "select * FROM Vw_CRMReceiveEquipmentCustTransDetails_Calib WHERE Deleted=0 and CertNo='" + cboCertificateNo.Text.ToString() + "'";
            SqlCommand cmd2 = new SqlCommand(sql, conn);
            SqlDataReader rd2 = cmd2.ExecuteReader();
            if (rd2.Read())
            {
                txtCustomer.Text = rd2["Customer_Name"].ToString();
                txtQuotation.Text = rd2["Quotation_No"].ToString();
                txtequipment.Text = rd2["EQUIPMENT_NAME"].ToString();
                txtjobno.Text = rd2["Jobno"].ToString() + "-" + rd2["runningno"].ToString();
                txtstickno.Text = rd2["stickno"].ToString();
                txtjobno.ToolTip = rd2["Jobno"].ToString();
                txtstickno.ToolTip = rd2["runningno"].ToString();
                txtModel.Text = rd2["model"].ToString();
                txtMaker.Text = rd2["maker"].ToString();
                DtFromDate.SelectedDate = Convert.ToDateTime(rd2["Calibdate"].ToString());
                DtToDate.SelectedDate = Convert.ToDateTime(rd2["NextCalibdate"].ToString());
            }
            rd2.Close();
            BusinessTier.DisposeConnection(conn);
            RadGridQuot.DataSource = DataSourceHelper();
            RadGridQuot.Rebind();
            btnReport.Enabled = true;
            btnDelete.Enabled = true;
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation_Modify", "cboQuotationNo_SelectedIndexChanged", ex.ToString(), "Audit");
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
            //string strLink = strPah + "Murali.pdf";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenNewTab", "window.open('" + strLink + "','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=500,height=500');", true);

            // Response.Clear();
            // Response.ContentType = "application/vnd.ms-excel";
            // Response.AddHeader("content-disposition", "attachment;filename=" + linkDownload5.Text + "");
            // Response.WriteFile(strLink);
            //// lblStatus.Text = strLink.ToString();
            // Response.End();

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
            // ShowMessage(9);
            //InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quoatation", "RadGridQuot_NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        //   TabContainer1.ActiveTab = TabContainer1.Tabs[1];
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "";
        sql = "select * FROM Vw_CRMReceiveEquipmentCustTransDetails_Calib where RECEIVED_TRANS_DETAIL_ID='" + cboCertificateNo.SelectedValue.ToString().Trim() + "' and deleted=0 order by CalibDate desc ";
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboCertificateNo.Text))
        {
            lblStatus.Text = "Select Certificate No";
            return;
        }
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        BusinessTier.Calib_Upd_Del(conn, Convert.ToInt32(cboCertificateNo.SelectedValue.ToString()), "", "", "", Session["sesUserID"].ToString().Trim(), "D");
        BusinessTier.DisposeConnection(conn);
        lblStatus.Text = "Successfully Calibration Deleted";
        RadGridQuot.DataSource = DataSourceHelper();
        RadGridQuot.Rebind();
        txtCustomer.Text = string.Empty;
        txtQuotation.Text = string.Empty;
        txtequipment.Text = string.Empty;
        txtjobno.Text = string.Empty;
        txtstickno.Text = string.Empty;
        txtModel.Text = string.Empty;
        txtMaker.Text = string.Empty;

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboCertificateNo.Text))
        {
            lblStatus.Text = "Select Certificate No";
            return;
        }

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Sticker.aspx?param1=" + cboCertificateNo.SelectedValue.ToString().Trim() + "&param2=0');", true);
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
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string strPah = WebConfigurationManager.AppSettings["WC_POPath"].ToString();
            string CertificateFileName = lblCertificate.Text.ToString();

            String caldt = DtFromDate.SelectedDate.ToString();
            DateTime pdt = DateTime.Parse(caldt);
            caldt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year + " 00:00:00";

            String nxtdt = DtToDate.SelectedDate.ToString();
            DateTime idt = DateTime.Parse(nxtdt);
            nxtdt = idt.Month + "/" + idt.Day + "/" + idt.Year + " 00:00:00";

            BusinessTier.Calib_Upd_Del(conn, Convert.ToInt32(cboCertificateNo.SelectedValue.ToString()), caldt.ToString(), nxtdt.ToString(), CertificateFileName.ToString(), Session["sesUserID"].ToString().Trim(),"U");
            BusinessTier.DisposeConnection(conn);
            lblStatus.Text = "Successfully Calibration Updated";
            lblStatus.ForeColor = Color.Yellow;
            lblCertificate.Visible = false;
          
            CertificateUpload.Visible = true;
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

        RadGridQuot.DataSource = DataSourceHelper();
        RadGridQuot.Rebind();

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
            //generateCertificateNo();
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
            string strpath = WebConfigurationManager.AppSettings["WC_POPath"].ToString();

            string path = "";



            path = strpath.ToString() + cboCalibSheet.Text.ToString();
            oXL = new Microsoft.Office.Interop.Excel.Application();

            oXL.DisplayAlerts = false;
            mWorkBook = oXL.Workbooks.Open(path, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            mWorkSheets = mWorkBook.Worksheets;

            mWSheet1 = (Excel.Worksheet)mWorkSheets.get_Item("Data1");

            String caldt = DtFromDate.SelectedDate.ToString();
            DateTime pdt = DateTime.Parse(caldt);
            caldt = pdt.Day + "/" + pdt.Month + "/" + pdt.Year;

            // mWSheet1.Cells[9, 5] = caldt.ToString();
            mWSheet1.Cells[9, 5] = DtFromDate.SelectedDate.ToString();
            mWSheet1.Cells[11, 5] = txtCustomer.Text.ToString();
            mWSheet1.Cells[9, 14] = cboCertificateNo.Text.ToString();
            mWSheet1.Cells[11, 14] = txtstickno.Text.ToString();
            mWSheet1.Cells[14, 14] = txtjobno.Text.ToString();
            mWSheet1.Cells[21, 5] = txtequipment.Text.ToString();

            mWSheet1.Cells[22, 5] = lblcuseqimaker.ToolTip.ToString();
            mWSheet1.Cells[21, 14] = lblcuseqimodel.ToolTip.ToString();
            mWSheet1.Cells[22, 14] = lbleqiserialno.ToolTip.ToString();


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
            string downfile = cboCalibSheet.Text.ToString() + "-" + txtstickno.Text.ToString() + ".xlsx";
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


}