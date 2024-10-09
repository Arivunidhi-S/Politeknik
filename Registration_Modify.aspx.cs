using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

public partial class Registration_Modify : System.Web.UI.Page
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
            //cbojobNo_Load();
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

    protected void cboJobno_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "select distinct(jobno) FROM Vw_Quotation_Customer_Registration WHERE Deleted=0 and registration = 1";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Jobno"].ToString();
                //item.Value = row["RegisterAuto_ID"].ToString();
                //item.Attributes.Add("QUOTATION_ID", row["QUOTATION_ID"].ToString());
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
        try
        {
            //string sql = "";
            //sql = "select * FROM Vw_Quotation_Customer_Registration WHERE Deleted=0 and registration = 1 and jobno='" + txtjobno.Text.ToString() + "' order by Created_Date desc";
            //SqlCommand cmd2 = new SqlCommand(sql, conn);
            //SqlDataReader rd2 = cmd2.ExecuteReader();
            //if (rd2.Read())
            //{
            //    cboCustomerId.Text = rd2["CUSTOMER_NAME"].ToString();
            //    cboCustomerId.SelectedValue = rd2["Customer_Id"].ToString();
            //}
            //rd2.Close();
            //BusinessTier.DisposeConnection(conn);
           // cboCustomerId.Text = "";
            cboCustomerId.ClearSelection();
            //cboCustomerId.SelectedItem.Text = "";
            RadGridQuot.DataSource = DataSourceHelper("J", "0");
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
            txtjobno.Text = "";
            RadGridQuot.DataSource = DataSourceHelper("T", "0");
            RadGridQuot.Rebind();

        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation_Modify", "cboQuotationNo_SelectedIndexChanged", ex.ToString(), "Audit");
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
                sql1 = " select Customer_Id,Customer_Name,CRM_ID from Vw_Quotation_Customer where DELETED=0 and [Customer_Name] LIKE @text + '%'  group by Customer_Id,Customer_Name,CRM_ID order by Customer_Name";
            else
                sql1 = " select Customer_Id,Customer_Name,CRM_ID from Vw_Quotation_Customer where DELETED=0 and [Customer_Name] LIKE '" + txtCustomer.Text.ToString().Trim() + "' + '%'  group by Customer_Id,Customer_Name,CRM_ID order by Customer_Name";
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
                item.Attributes.Add("CRM_ID", row["CRM_ID"].ToString().Trim());
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

    protected void RadGridQuot_ItemDataBound(object sender, GridItemEventArgs e)
    {

        if (e.Item is GridDataItem)
        {
            //Get the instance of the right type
            GridDataItem dataBoundItem = e.Item as GridDataItem;
            CheckBox ChkSelect = (CheckBox)e.Item.FindControl("ChkSelect");
            RadNumericTextBox txtAprDiscount = (RadNumericTextBox)e.Item.FindControl("txtAprDiscount");
            RadTextBox txtremarksdiscnt = (RadTextBox)e.Item.FindControl("txtremarksdiscnt");

            //if (Convert.ToDecimal(dataBoundItem["Discount"].Text.ToString().Trim()) > 10)
            //{
            //    ChkSelect.Checked = true;
            //    dataBoundItem.ForeColor = Color.Red;
            //    dataBoundItem.Font.Bold = true;
            //}
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
            RadGridQuot.DataSource = DataSourceHelper("M", "0");
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quoatation", "RadGridQuot_NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper(string typeMasterDetail, string strMasterId)
    {
        //   TabContainer1.ActiveTab = TabContainer1.Tabs[1];
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "";

        if (typeMasterDetail == "M")
        {
            sql = "select * FROM Vw_Quotation_Customer_Registration WHERE Deleted=0 and registration = 1 order by Created_Date desc";
        }

        else if (typeMasterDetail == "T")
        {
            sql = "select * FROM Vw_Quotation_Customer_Registration WHERE Deleted=0 and registration = 1 and Customer_id='" + cboCustomerId.SelectedValue.ToString() + "' order by Created_Date desc";

        }
        else if (typeMasterDetail == "J")
        {
            sql = "select * FROM Vw_Quotation_Customer_Registration WHERE Deleted=0 and registration = 1 and jobno='" + txtjobno.Text.ToString() + "' order by Created_Date desc";

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

    protected void btnReport_Click(object sender, EventArgs e)
    {
        //string strReport = "Quotation";
        //string parameter = "Report_Quotation.aspx?" + "param1=" + lblQuatmasterid.Text.ToString().Trim();
        //Response.Redirect(parameter);
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string quoatStatus = null;
        string sql2 = "select Quot_Status from Quotation where deleted=0";
        SqlCommand command1 = new SqlCommand(sql2, conn);
        SqlDataReader reader1 = command1.ExecuteReader();
        if (reader1.Read())
        {
            quoatStatus = reader1["Quot_Status"].ToString();
        }
        BusinessTier.DisposeReader(reader1);

        if (quoatStatus == "DISCOUNT")
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('You Can not generate the Quotation.Because this quotation is Waiting for the Discount ( " + txtDiscountApprov.Text + " )% approval');", true);
            return;
        }
        else if (quoatStatus == "PENDING")
        {
            // ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Quotation.aspx?param1=" + lblQuatmasterid.Text.ToString().Trim() + "');", true);
        }


    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            foreach (GridDataItem item in RadGridQuot.Items)
            {
                RadNumericTextBox txtAprDiscount = (RadNumericTextBox)item.FindControl("txtAprDiscount");
                RadTextBox txtAccessories = (RadTextBox)item.FindControl("txtAccessories");
                CheckBox ChkSelect = (CheckBox)item.FindControl("ChkSelect");
                string sql2 = "";
                string ID = item.GetDataKeyValue("RegisterAuto_ID").ToString();
                // string ID = txtjobno.SelectedValue.ToString();
                if (ChkSelect.Checked)
                {
                    sql2 = "update [Politecknik].[dbo].[Registration] set [Accessories]='" + txtAccessories.Text.ToString() + "',[Status]='" + radioButtonlist.SelectedItem.Text.ToString() + "',[MODIFIED_BY]='" + Session["sesUserID"].ToString().Trim() + "',[MODIFIED_DATE]='" + DateTime.Now.ToString() + "' where [RegisterAuto_ID]='" + ID.ToString() + "' and deleted=0";
                }

                else
                {
                    // sql2 = "update [Politecknik].[dbo].[Registration] set [Jobno]=null,[Accessories]=null,[Registration]=null,[Status]=null,[CREATED_BY]=null,[CREATED_DATE]=null,[QUOTATION_ID]=null where [RegisterAuto_ID]='" + ID.ToString() + "' and deleted=0";
                    sql2 = "delete from [Politecknik].[dbo].[Registration] where [RegisterAuto_ID]='" + ID.ToString() + "' and deleted=0";
                }

                SqlCommand command1 = new SqlCommand(sql2, conn);
                command1.ExecuteNonQuery();
            }
        }

        catch (Exception ex)
        {
            ShowMessage(5);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation_Detial", "Update", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
        if (txtjobno.Text == "" && cboCustomerId.Text == "")
            RadGridQuot.DataSource = DataSourceHelper("M", "0");
        else if (txtjobno.Text == "")
            RadGridQuot.DataSource = DataSourceHelper("T", "0");
        else
            RadGridQuot.DataSource = DataSourceHelper("J", "0");
            RadGridQuot.Rebind();

    }

    protected void btnEmail_Click(object sender, EventArgs e)
    {
        System.IO.MemoryStream stream = null;
        SmtpClient smtp = new SmtpClient();
        MailMessage item = new MailMessage();

        //   Stimulsoft.Report.StiReport report = default(Stimulsoft.Report.StiReport);
        try
        {
            //Get Report
            //report = StiWebViewer1.Report;
            //Create Mail Message
            MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["FromAddress"].ToString(), "SIRIM SST");
            item.From = fromAddress;
            item.Subject = "SIRIM SST - Add New Equipment";
            item.Body = "Discount Approved Successfully." + "\r\n" + "\r\n" + "  " + "\r\n" + "  " + "\r\n" + "\r\n" + "\r\n" + "Note: " + "\r\n" + "*. Do not reply to this mail" + "\r\n" + "*. This is system generated mail " + "\r\n" + "http://218.111.224.242/SIRIM/login.aspx";
            string strCCMail = BusinessTier.getCCMailID("SIRIM");
            item.To.Add(strCCMail.ToString());
            stream = new System.IO.MemoryStream();

            // report.ExportDocument(Stimulsoft.Report.StiExportFormat.Pdf, stream);
            // stream.Seek(0, SeekOrigin.Begin);
            //  Attachment attachment = new Attachment(stream, "MyReport.pdf", "application/pdf");
            //  item.Attachments.Add(attachment);

            //Create SMTP Client
            smtp.Host = ConfigurationManager.AppSettings["ExchangeServer"].ToString();
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString());


            smtp.Send(item);
            item.Dispose();
            smtp.Dispose();
            //File.Delete(strAttachmentFilename.ToString().Trim());
            // ShowMessage(83);

        }
        catch (Exception ex)
        {
            //ShowMessage(84);
            lblStatus.Text = "An error occured while trying to Send Email.";
        }


    }


}