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

public partial class JobRegister_View : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();
        
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            //if (!IsPostBack)
            //{
            if (!(string.IsNullOrEmpty(Session["sesUserID"].ToString())))
            {
                SqlConnection connMenu = BusinessTier.getConnection();
                connMenu.Open();
                SqlDataReader readerMenu = BusinessTier.getMenuList(connMenu, Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                dtMenuItems.Load(readerMenu);
                BusinessTier.DisposeReader(readerMenu);
                BusinessTier.DisposeConnection(connMenu);

                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                BusinessTier_CRM.BindErrorMessageDetails(conn);
                BusinessTier.DisposeConnection(conn);


            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            //}
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
           
                    RadGrid1.DataSource = DataSourceHelper();
              
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message.ToString();
            ////InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_IH", "RadGrid1_NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "";
        sql = "select * FROM Vw_CRMReceiveEquipmentCustTransDetails  where DetailQuoteFlag = '1' and orderno=0 and deleted=0 order by RECEIVED_TRANS_DETAIL_ID desc";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

    //protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Item is GridDataItem)
    //        {
    //            //Get the instance of the right type
    //            GridDataItem dataBoundItem = e.Item as GridDataItem;
    //            Label lbljobrun = (Label)e.Item.FindControl("lbljobrun");
    //            Label lbljob = (Label)e.Item.FindControl("lbljob");
    //            Label lblrunno = (Label)e.Item.FindControl("lblrunno");

    //            lbljobrun.Text = lbljob.Text.ToString().Trim() + "-" + lblrunno.Text.ToString().Trim();
    //        }
    //        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
    //        {


    //            GridEditFormItem editedItem = e.Item as GridEditFormItem;
    //            Label lbl = (Label)editedItem.FindControl("lblQuotationtransId");
    //            RadComboBox cboEquipmentId = (RadComboBox)editedItem.FindControl("cboEquipmentId");
    //            RadComboBox cboCalibration = (RadComboBox)editedItem.FindControl("cboCalibration");

    //            if (!(string.IsNullOrEmpty(lbl.Text.ToString())))
    //            {

    //                SqlConnection conn = BusinessTier.getConnection();
    //                conn.Open();
    //                string sql = "select Equipment_ID,Equipment_Name,Equipment_no,calib_type FROM vw_Quotation WHERE Deleted = 0 and Quotation_Trans_ID = '" + lbl.Text.ToString() + "'";
    //                SqlCommand command = new SqlCommand(sql, conn);
    //                SqlDataReader reader = command.ExecuteReader();
    //                if (reader.Read())
    //                {
    //                    cboEquipmentId.Text = reader["Equipment_Name"].ToString();
    //                    cboEquipmentId.SelectedValue = reader["Equipment_ID"].ToString();
    //                    cboCalibration.SelectedItem.Text = reader["calib_type"].ToString();
    //                }
    //                BusinessTier.DisposeReader(reader);
    //                BusinessTier.DisposeConnection(conn);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblStatus.Text = ex.Message.ToString();
    //        //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
    //        InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invoice", "RadGridQuot_ItemDataBound", ex.ToString(), "Audit");

    //    }


    //}

    private void InsertLogAuditTrail(string userid, string module, string activity, string result, string flag)
    {
        SqlConnection connLog = BusinessTier.getConnection();
        connLog.Open();
        BusinessTier.InsertLogAuditTrial(connLog, userid, module, activity, result, flag);
        BusinessTier.DisposeConnection(connLog);
    }
}