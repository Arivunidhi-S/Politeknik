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

public partial class AssetManagement : System.Web.UI.Page
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
                DateTime dtECDte = DateTime.Now;
                DtNewDate.DbSelectedDate = dtECDte.ToString();


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

    protected void RadGridInvoice_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGridInvoice.DataSource = DataSourceHelper();
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
     
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "";

         sql = "select  * FROM VW_AssetManagement WHERE Deleted=0 order by assetid desc";

        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

    protected void cboEquipmentId_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " ";

            if (string.IsNullOrEmpty(txtequpment.Text.ToString().Trim()))
                sql1 = " select Equipment_ID,Equipment_Name,Maker,Model,fee,Ranges from Master_Equipment where DELETED=0 and flag_price=0 and [Equipment_Name] LIKE @text + '%' order by Equipment_Name";
            else
                sql1 = " select Equipment_ID,Equipment_Name,Maker,Model,fee,Ranges from Master_Equipment where DELETED=0  and flag_price=0 and [Equipment_Name] LIKE '" + txtequpment.Text.ToString().Trim() + "' + '%' and [Equipment_Name] LIKE @text + '%' order by Equipment_Name";
                        SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Equipment_Name"].ToString();
                item.Value = row["Equipment_ID"].ToString();
                item.Attributes.Add("Equipment_Name", row["Equipment_Name"].ToString());

                item.Attributes.Add("Maker", row["Maker"].ToString());
                item.Attributes.Add("Model", row["Model"].ToString());
                item.Attributes.Add("Ranges", row["Ranges"].ToString());
                item.Attributes.Add("fee", row["fee"].ToString());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            comboBox.Text = "--Select--";
            BusinessTier.DisposeConnection(conn);
            // Session["rdobutton"] = "";
        }


        catch (Exception ex)
        {

            //ShowMessage("Please Select the Installation Name", "Red");
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
        //if (string.IsNullOrEmpty(cboJobNo.Text))
        //{
        //    lblStatus.Text = "JobNo Field Cannot be Empty";
        //    return;
        //}

        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_JobRejection.aspx?param1=" + cboJobNo.SelectedValue.ToString().Trim() + "&param2=" + 0 + "');", true);
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            if (string.IsNullOrEmpty(txtNewPrice.Text))
            {
                lblStatus.Text = "NewPrice field Cannot be Empty";
                return;
            }

            String invdt = DtNewDate.SelectedDate.ToString();
            DateTime pdt = DateTime.Parse(invdt);
            invdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year + " 00:00:00";

            conn.Open();

            BusinessTier.saveAssetManagement(conn, Convert.ToInt32(cboEquipmentId.SelectedValue.ToString()), txtAssetNumber.Text.ToString(), txtSerialNumber.Text.ToString(), invdt.ToString(), Convert.ToDouble(txtNewPrice.Text.ToString()), txtRemarks.Text.ToString(), Session["sesUserID"].ToString().Trim());

            lblStatus.Text = "Successfully Value Inserted";
            BusinessTier.DisposeConnection(conn);
            txtRemarks.Text = "";
            txtNewPrice.Text = "";
            RadGridInvoice.DataSource = DataSourceHelper();
            RadGridInvoice.Rebind();

        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message.ToString();
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Assetmanagement", "btnSave_OnClick", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    
    }

}