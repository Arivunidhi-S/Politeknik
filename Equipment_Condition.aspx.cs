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
using System.Data.Common;
using System.Web.Configuration;
using System.Net;

public partial class Equipment_Condition : System.Web.UI.Page
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

                DateTime dt = DateTime.Now;
                DateTime dt1 = dt.AddDays(30);
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
        cboQuotationNo.Text= string.Empty;
        //cboJobNo.Text = string.Empty;
        txtowner.Text = "";
        txtphysical.Text = "";
        txtfuctional.Text = "";
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

            sql1 = "  select Quotation_Id,Quotation_No,Quotation_Date from Quotation where QUOT_STATUS='PENDING' and DELETED=0  and  Customer_ID='" + cboCustomerId.SelectedValue + "' Group by Quotation_Id,Quotation_No,Quotation_Date  order by Quotation_No desc";
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
        //cboJobNo.Text = "";
        txtowner.Text = "";
        txtphysical.Text = "";
        txtfuctional.Text = "";
    }

    protected void cboJobNo_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "select RECEIVED_TRANS_DETAIL_ID,jobno,runningno FROM Vw_CRMReceiveEquipmentCustTransDetails WHERE Deleted=0 and orderno=0 and Calibration is null and invoiceflag=0 and delivery=0 and assignid='" + Session["sesUserID"].ToString().Trim() + "' and Quotation_Id = '" + cboQuotationNo.SelectedValue.ToString() + "' group by RECEIVED_TRANS_DETAIL_ID,jobno,runningno";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Jobno"].ToString() + "-" + row["runningno"].ToString();
                txtfuctional.ToolTip = row["Jobno"].ToString();
                item.Value = row["runningno"].ToString();
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
        lblStatus.Text = "";
        txtfuctional.Text = "";
        txtphysical.Text = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string sql = "";
            string jbno = cboJobNo.Text.ToString().Trim().Split('-')[0];
            sql = "select functional,physical,ownerofitems FROM Vw_CRMReceiveEquipmentCustTransDetails WHERE Deleted=0 and Jobno='" + jbno.ToString() + "'and runningno='" + cboJobNo.SelectedValue.ToString() + "'";
            SqlCommand cmd2 = new SqlCommand(sql, conn);
            SqlDataReader rd2 = cmd2.ExecuteReader();
            if (rd2.Read())
            {
                if (!(string.IsNullOrEmpty(rd2["functional"].ToString())))
                {
                    txtfuctional.Text = rd2["functional"].ToString();
                }
                if (!(string.IsNullOrEmpty(rd2["physical"].ToString())))
                {
                    txtphysical.Text = rd2["physical"].ToString();
                }
                if (!(string.IsNullOrEmpty(rd2["ownerofitems"].ToString())))
                {
                    txtowner.Text = rd2["ownerofitems"].ToString();
                }
            }
            rd2.Close();
            BusinessTier.DisposeConnection(conn);

        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Equipment_Condition", "cboJobNo_SelectedIndexChanged", ex.ToString(), "Audit");
        }
    }

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
        if (string.IsNullOrEmpty(cboJobNo.Text))
        {
            lblStatus.Text = "JobNo Field Cannot be Empty";
            return;
        }
        string jbno = cboJobNo.Text.ToString().Trim().Split('-')[0];// +"/" + cboJobNo.Text.ToString().Trim().Split('/')[1];

        string rnno = cboJobNo.SelectedValue.ToString();

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_JobRegister.aspx?param1=" + jbno.ToString().Trim() + "&param2=" + rnno.ToString().Trim() + "');", true);
    }

    protected void btnCondition_OnClick(object sender, EventArgs e)
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

            if (string.IsNullOrEmpty(txtphysical.Text))
            {
                lblStatus.Text = "Physical Condition Field Cannot be Empty";
                return;
            }
            if (string.IsNullOrEmpty(txtfuctional.Text))
            {
                lblStatus.Text = "Functional Condition Field Cannot be Empty";
                return;
            }


            int invid = 0;
            conn.Open();
            string jbno = cboJobNo.Text.ToString().Trim().Split('-')[0];

            BusinessTier.Equip_Condition(conn, jbno.ToString(), Convert.ToInt32(cboJobNo.SelectedValue.ToString()), txtowner.Text.ToString(), txtphysical.Text.ToString(), txtfuctional.Text.ToString());

            // txtjobno_load();
            lblStatus.Text = cboJobNo.Text + " is Successfully Updated";
            lblStatus.ForeColor = Color.Yellow;
                        BusinessTier.DisposeConnection(conn);

            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_JobRejection.aspx?param1=" + cboJobNo.SelectedValue.ToString().Trim() + "&param2=" + 0 + "');", true);
            // cboJobNo.ClearSelection();
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message.ToString();
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Equipment_Condition", "btnCondition_OnClick", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }


    }

    protected void RadGridAccCalibItems_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGridAccCalibItems.DataSource = DataSourceHelper();
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invocie", "RadGridQuot_NeedDataSource", ex.ToString(), "Audit");
        }
    }

    protected void RadGridAccCalibItem_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataBoundItem = e.Item as GridDataItem;
                Label lbljobrun = (Label)e.Item.FindControl("lbljobrun");
                Label lbljob = (Label)e.Item.FindControl("lbljob");
                Label lblrunno = (Label)e.Item.FindControl("lblrunno");

                lbljobrun.Text = lbljob.Text.ToString().Trim() + "-" + lblrunno.Text.ToString().Trim();
            }
            
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message.ToString();
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invoice", "RadGridQuot_ItemDataBound", ex.ToString(), "Audit");

        }


    }

    public DataTable DataSourceHelper()
    {
        //   TabContainer1.ActiveTab = TabContainer1.Tabs[1];

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "";
        sql = "select * FROM Vw_CRMReceiveEquipmentCustTransDetails  where physical<>'' or functional <>'' or ownerofitems<>'' order by RECEIVED_TRANS_DETAIL_ID desc";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

}