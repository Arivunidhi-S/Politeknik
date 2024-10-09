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

public partial class CRM_ReceiveEquipment_IH : System.Web.UI.Page
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
        try
        {
            if (string.IsNullOrEmpty(Session["sesUserID"].ToString()))
            {
                //string str = TabContainer1.ActiveTab.HeaderText.ToString();

                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    string strValue = rdoButton.SelectedItem.Value.ToString();
                    Session["intFlagMultiBatch"] = "0";
                    Session["SelectedPO"] = "0";
                    if (rdoButton.Text == "1")
                    {
                        lblQuotationOrContract.Text = "Quotation: ";
                        cboQuotationNo.Visible = true;
                        cboContractNo.Visible = false;
                        lblPO.Text = "";
                        linkDownload.Text = "";
                        btnReportQuotaion.Visible = false;
                    }
                    if (rdoButton.Text == "2")
                    {
                        lblQuotationOrContract.Text = "Contract: ";
                        cboQuotationNo.Visible = false;
                        cboContractNo.Visible = true;
                        linkDownload.Text = "";
                        lblPO.Text = "";
                        btnReportQuotaion.Visible = false;
                    }
                }
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
            if (rdoButton.SelectedItem.Text.ToString().Trim() == "Quotation")
            {
                sql1 = "SELECT [CUSTOMER_ID],[CRM_ID],[CUSTOMER_NAME] FROM [Vw_Quotation_Customer] where Branch_ID=" + Convert.ToInt32(Session["sesBranchID"].ToString().Trim()) + " and Quot_Status<>'COMPLETED' and deleted=0 and [Customer_Name] LIKE '" + txtCustomer.Text.ToString().Trim() + "' + '%' and [Customer_Name] LIKE @text + '%' group by [CUSTOMER_ID],[CUSTOMER_NAME],[CRM_ID]";
            }
            else
            {

                if (string.IsNullOrEmpty(txtCustomer.Text.ToString().Trim()))
                    sql1 = " select Customer_Id,Customer_Name,CRM_ID from Vw_ContractCustomerMaster where Branch_ID=" + Convert.ToInt32(Session["sesBranchID"].ToString().Trim()) + " and DELETED=0  and [Customer_Name] LIKE 'AA' + '%' and [Customer_Name] LIKE @text + '%' group by [CUSTOMER_ID],[CUSTOMER_NAME],[CRM_ID] order by Customer_Name";
                else
                    sql1 = " select Customer_Id,Customer_Name,CRM_ID from Vw_ContractCustomerMaster where Branch_ID=" + Convert.ToInt32(Session["sesBranchID"].ToString().Trim()) + " and DELETED=0  and [Customer_Name] LIKE '" + txtCustomer.Text.ToString().Trim() + "' + '%' and [Customer_Name] LIKE @text + '%' group by [CUSTOMER_ID],[CUSTOMER_NAME],[CRM_ID]  order by Customer_Name";
            }
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

           // ShowMessage(ex.Message.ToString(), "Red");
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingPO", "cboCustomer_OnItemsRequested", ex.ToString(), "Audit");

        }
    }

    protected void rdoButton_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rdoButton = (RadioButtonList)sender;
        string strValue = rdoButton.SelectedItem.Text.ToString();

        if ((rdoButton.Text == "Quotation") || (rdoButton.Text == "1"))
        {
            lblQuotationOrContract.Text = "Quotation: ";
            cboQuotationNo.Visible = true;
            cboContractNo.Visible = false;
            //CboCustomer.Text = "";
            //CboCustomer.Items.Clear();
            //CboCustomer.Attributes.Clear();
            lblPO.Text = "";
            linkDownload.Text = "";
            lblStatus.Text = "";
            btnReportQuotaion.Visible = false;
            RadGrid1.DataSource = DataSourceHelper("NoQuote", "0", "0");
            RadGrid1.Rebind();
        }
        if ((rdoButton.Text == "Contract") || (rdoButton.Text == "2"))
        {
            lblQuotationOrContract.Text = "Contract: ";
            cboQuotationNo.Visible = false;
            cboContractNo.Visible = true;
            //CboCustomer.Text = "";
            //CboCustomer.Items.Clear();
            //CboCustomer.Attributes.Clear();
            lblPO.Text = "";
            linkDownload.Text = "";
            lblStatus.Text = "";
            btnReportQuotaion.Visible = false;
            RadGrid1.DataSource = DataSourceHelper("NoContr", "0", "0");
            RadGrid1.Rebind();
        }

    }

    protected void cboCustomer_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string strCustId = CboCustomer.SelectedValue.ToString().Trim();
            cboQuotationNo.Text = "";
            txtRemarks.Text = "";
            //RadGrid1.DataSource = DataSourceHelper("Cust", Convert.ToInt32(strCustId));
            //RadGrid1.Rebind();
        }
        catch (Exception ex)
        {
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_IH", "cboCustomer_OnSelectedIndexChanged", ex.ToString(), "Audit");
            ShowMessage(ex.Message.ToString(), "Red");
        }
    }

    protected void cboContractNo_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            DateTime dt = DateTime.Now;
            string StrExpDate = dt.Month + "/" + dt.Day + "/" + dt.Year;
            string sql1 = " select MASTER_CONTRACT_ID,Contract_No,Expiry_Date,Contract_Date from Master_Contract where Customer_ID='" + strCustomerID.Trim() + "' and DELETED=0 and Expiry_Date>='" + StrExpDate + "'  and [Contract_No] LIKE @text + '%' and Branch_ID='" + Session["sesBranchID"].ToString().Trim() + "' and MasterContFlag=0 order by Contract_No";

            //string sql1 = " select Contract_Id,Contract_No,Expiry_Date,Contract_Date from Master_ContractEquipment where (Customer_ID='" + strCustomerID.Trim() + "' and DELETED=0 and Expiry_Date>='" + StrExpDate + "') or(Customer_ID=0)  and [Contract_No] LIKE @text + '%' and Branch_ID='" + Session["sesBranchID"].ToString().Trim() + "' order by Contract_No";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Contract_No"].ToString();
                item.Value = row["MASTER_CONTRACT_ID"].ToString();
                item.Attributes.Add("Contract_No", row["Contract_No"].ToString());
                // item.Attributes.Add("Enquiry_Date", row["Enquiry_Date"].ToString());
                if ((row["Contract_Date"].ToString().Trim() == "0") || (string.IsNullOrEmpty(row["Contract_Date"].ToString().Trim())))
                {
                    DateTime ContractDate = DateTime.Now;
                    item.Attributes.Add("Contract_Date", ContractDate.ToString());
                }
                else
                {
                    DateTime ContractDate = (DateTime)row["Contract_Date"];
                    item.Attributes.Add("Contract_Date", ContractDate.ToString());
                }
                if ((row["Expiry_Date"].ToString().Trim() == "0") || (string.IsNullOrEmpty(row["Expiry_Date"].ToString().Trim())))
                {
                    DateTime Expiry_Date = DateTime.Now;
                    item.Attributes.Add("Expiry_Date", Expiry_Date.ToString());
                }
                else
                {
                    DateTime ExpiryDate = (DateTime)row["Expiry_Date"];
                    item.Attributes.Add("Expiry_Date", ExpiryDate.ToString());
                }
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_IH", "cboContractNo_OnItemsRequested", ex.ToString(), "Audit");
            ShowMessage(ex.Message.ToString(), "Red");
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

            string sql1 = " select Quotation_Id,Quotation_No,Quotation_Date from Quotation where DELETED=0  and  Customer_ID='" + strCustomerID + "' and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and  [Quotation_No] LIKE @text + '%' and QUOT_STATUS='PENDING' order by Quotation_No desc";
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
            ShowMessage(ex.Message.ToString(), "Red");
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_IH", "cboQuotation_OnItemsRequested", ex.ToString(), "Audit");
        }

    }

    protected void cboQuotation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblQuatmasterid.Text = cboQuotationNo.SelectedValue;
            lblContactId.Text = "0";
            RadGrid1.DataSource = DataSourceHelper("Quote", lblQuatmasterid.Text.ToString().Trim(), CboCustomer.SelectedValue.ToString());
            RadGrid1.Rebind();

            linkDownload.Text = "";
            lblPO.Text = "";
            //btnReportQuotaion.Visible = false;
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT FILE_PATH from CRM_ReceivePO where Customer_ID='" + CboCustomer.SelectedValue.Trim() + "' and QUOTATION_ID='" + lblQuatmasterid.Text.ToString().Trim() + "' and DELETED=0", conn);
            DataTable g_datatable = new DataTable();
            sqlDataAdapter.Fill(g_datatable);
            BusinessTier.DisposeAdapter(sqlDataAdapter);
            foreach (DataRow reader in g_datatable.Rows)
            {
                linkDownload.Text = (reader["FILE_PATH"]).ToString();
                if (!(string.IsNullOrEmpty((reader["FILE_PATH"]).ToString())))
                    lblPO.Text = "Click To View PO : ";
            }
            string sql="Select Remarks_Detail from Vw_QuotationDetail_Equipment where QUOTATION_ID='" + cboQuotationNo.SelectedValue.ToString().Trim() + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                txtRemarks.Text = rd["Remarks_Detail"].ToString().Trim();
            }
            BusinessTier.DisposeReader(rd);

            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            //lblStatus.Text = ex.Message.ToString();
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_IH", "cboQuotationNo_SelectedIndexChanged", ex.ToString(), "Audit");
        }
    }

    protected void cboContract_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();

        try
        {
            lblContactId.Text = cboContractNo.SelectedValue;
            lblQuatmasterid.Text = "0";
            RadGrid1.DataSource = DataSourceHelper("Contr", lblContactId.Text.ToString().Trim(), CboCustomer.SelectedValue.ToString());
            RadGrid1.Rebind();

            linkDownload.Text = "";
            lblPO.Text = "";
            btnReportQuotaion.Visible = false;
            conn.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT FILE_PATH from  CRM_ReceivePO where Customer_ID='" + CboCustomer.SelectedValue.Trim() + "' and CONTEQUIP_ID='" + lblContactId.Text.ToString().Trim() + "' and DELETED=0", conn);
            DataTable g_datatable = new DataTable();
            sqlDataAdapter.Fill(g_datatable);
            BusinessTier.DisposeAdapter(sqlDataAdapter);
            foreach (DataRow reader in g_datatable.Rows)
            {
                linkDownload.Text = (reader["FILE_PATH"]).ToString();
                lblPO.Text = "Click To View PO : ";
            }
            g_datatable.Dispose();
            g_datatable.Clear();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT [PO_NUMBER], [PO_ID] FROM [CRM_ReceivePO] where CONTEQUIP_ID= " + lblContactId.Text.ToString().Trim() + "", conn);
            DataTable links = new DataTable();
            adapter.Fill(links);
            cboPO.DataTextField = "PO_NUMBER";
            cboPO.DataValueField = "PO_ID";
            cboPO.DataSource = links;
            cboPO.DataBind();
            BusinessTier.DisposeAdapter(adapter);
            links.Clear();
            links.Dispose();

            Session["intFlagMultiBatch"] = "0";
            Session["SelectedPO"] = "0";
            int intFlagMultiBatch = 0;
            SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter("SELECT FlagMultiBatch from  CRM_ContractMultiBatch where CUSTOMER_ID='" + CboCustomer.SelectedValue.Trim() + "' and MASTER_CONTRACT_ID='" + lblContactId.Text.ToString().Trim() + "'", conn);
            DataTable g_datatable1 = new DataTable();
            sqlDataAdapter1.Fill(g_datatable1);
            BusinessTier.DisposeAdapter(sqlDataAdapter1);
            foreach (DataRow reader1 in g_datatable1.Rows)
            {
                if (!(string.IsNullOrEmpty((reader1["FlagMultiBatch"]).ToString())))
                    intFlagMultiBatch = Convert.ToInt32(reader1["FlagMultiBatch"].ToString().Trim());
            }
            g_datatable1.Dispose();
            g_datatable1.Clear();
            if (intFlagMultiBatch > 0)
            {
                cboPO.Visible = true;
                Session["intFlagMultiBatch"] = intFlagMultiBatch.ToString().Trim();
            }
            else
            {
                cboPO.Visible = false;
                Session["intFlagMultiBatch"] = "0";
            }


            BusinessTier.DisposeConnection(conn);


        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message.ToString();
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_IH", "cboContract_SelectedIndexChanged", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void cboPO_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (cboPO.Text.ToString().Trim() == "--Choose PO--")
        //{
        //    lblStatus.Text = "Access Denied, Please choose the PO number to proceed";
        //    return;
        //}
        if (Session["intFlagMultiBatch"].ToString().Trim() == "1")
        {
            if (!(string.IsNullOrEmpty(cboPO.Text.ToString().Trim())))
            {
                Session["SelectedPO"] = cboPO.SelectedItem.Text.ToString().Trim();
            }
            else
            {
                Session["SelectedPO"] = "0";
                cboPO.Text = "--Choose PO--";
            }
        }
        else
        {
            Session["SelectedPO"] = "0";
        }
    }

    protected void cboBranch_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            conn.Open();
            string sql1 = " select Branch_ID,Branch_Name,Branch_Code from Master_Branch where DELETED=0  and [Branch_Name] LIKE @text + '%' order by Branch_Name";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Branch_Code"].ToString();
                item.Value = row["Branch_ID"].ToString();
                item.Attributes.Add("Branch_Name", row["Branch_Name"].ToString());
                item.Attributes.Add("Branch_Code", row["Branch_Code"].ToString());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);
        }


        catch (Exception ex)
        {
            ShowMessage(ex.Message.ToString(), "Red");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    //protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Item is GridDataItem)
    //        {
    //            GridDataItem item = (GridDataItem)e.Item;
    //            TextBox txtCalibrationType = (TextBox)e.Item.FindControl("txtCalibrationType");

    //            //HyperLink editLink = (HyperLink)e.Item.FindControl("EditLink");
    //            //TextBox txtQuote_Contract = (TextBox)item.FindControl("txtQuote_Contract");
    //            //TextBox txtQty = (TextBox)item.FindControl("txtQty");
    //            string strStatus = "";
    //            string strContQuoteID = "";
    //            string strContrQuote = "";
    //            if (rdoButton.Text == "1")//quote
    //            {
    //                strStatus = "Quote";
    //                strContQuoteID = cboQuotationNo.SelectedValue.ToString().Trim();
    //                strContrQuote = cboQuotationNo.Text.ToString();
    //            }
    //            if (rdoButton.Text == "2")//contract
    //            {
    //                strStatus = "Contr";
    //                strContQuoteID = cboContractNo.SelectedValue.ToString().Trim();
    //                strContrQuote = cboContractNo.Text.ToString();
    //            }

    //            //editLink.Attributes["href"] = "";
    //            //editLink.Attributes["onclick"] = String.Format("return ShowEditForm('{0}','{1}','{2}','{3}','{4}','{5}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EQUIPMENT_ID"], CboCustomer.SelectedValue.ToString(), strStatus, strContQuoteID, strContrQuote, CboCustomer.Text.ToString());

    //            //// editLink.Attributes["onclick"] = String.Format("return ShowEditForm('{0}','{1}','{2}','{3}','{4}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EQUIPMENT_ID"], CboCustomer.SelectedValue.ToString(), strStatus, strContQuoteID, strContrQuote);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblStatus.Text = ex.Message.ToString();
    //        ////InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
    //        InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_IH", "RadGrid1_ItemCreated", ex.ToString(), "Audit");
    //    }
    //}

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                GridHeaderItem headerItem = RadGrid1.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
                TextBox txtCalibrationType = (TextBox)e.Item.FindControl("txtCalibrationType");
                RadComboBox cboCalibrationType = (RadComboBox)e.Item.FindControl("cboCalibrationType");
                HyperLink editLink = (HyperLink)e.Item.FindControl("EditLink");
                Label lblEquipmentId = (Label)e.Item.FindControl("lblEquipmentId");
                TextBox txtQuote_Contract = (TextBox)item.FindControl("txtQuote_Contract");
                TextBox txtQty = (TextBox)e.Item.FindControl("txtQty");
                Label lblCalibType_SerielNo = (Label)e.Item.FindControl("lblCalibType_SerielNo");
                TextBox txtContractFlag = (TextBox)e.Item.FindControl("txtContractFlag");
                txtContractFlag.Text = txtContractFlag.Text.ToString().Trim();
                TextBox txtModel = (TextBox)e.Item.FindControl("txtModel");
                TextBox txtMaker = (TextBox)e.Item.FindControl("txtMaker");

                //GridHeaderItem item1 = e.Item as GridHeaderItem;
                //item1["Quantity"].Text = "Test";

                string strStatus = "";
                string strContQuoteID = "";
                string strContrQuote = "";

                string strContratQuotationDetailID = (item.OwnerTableView.DataKeyValues[item.ItemIndex]["ContratQuotationDetailID"]).ToString().Trim();
                string strEquipmentId = lblEquipmentId.Text.ToString();
                //string strID1 = item.Cells[2].Text.ToString().Trim();
                string strCalibType_SerielNo = lblCalibType_SerielNo.Text.ToString().ToString();
                if (string.IsNullOrEmpty(strEquipmentId.ToString().Trim()))
                {
                    ShowMessage(204);
                    return;
                }
                if (rdoButton.Text == "1")//Quotation
                {
                    if (string.IsNullOrEmpty(cboQuotationNo.SelectedValue.ToString().Trim()))
                    {
                        ShowMessage(204);
                        return;
                    }
                    txtQuote_Contract.Text = cboQuotationNo.Text.ToString();
                    txtQuote_Contract.ToolTip = cboQuotationNo.SelectedValue.ToString().Trim();
                    strContQuoteID = cboQuotationNo.SelectedValue.ToString().Trim();
                    strContrQuote = cboQuotationNo.Text.ToString();
                    strStatus = "Quote";
                    txtQty.ToolTip = strContratQuotationDetailID.ToString().Trim();
                    txtCalibrationType.Text = strCalibType_SerielNo.ToString().Trim();
                    cboCalibrationType.Visible = false;
                    txtCalibrationType.Visible = true;
                    headerItem["Quantity"].Text = "Quantity";

                    conn.Open();
                    int intReceived_Trans_ID = 0;
                    SqlCommand cmd = new SqlCommand("Select Received_Trans_ID from CRM_ReceiveEquipment_Trans where QUOTATION_TRANS_ID='" + strContratQuotationDetailID.Trim() + "' and EQUIPMENT_ID='" + strEquipmentId.Trim() + "' and QUOTATION_ID ='" + cboQuotationNo.SelectedValue.ToString().Trim() + "' and Customer_Id=" + CboCustomer.SelectedValue.ToString().Trim() + " and Deleted=0", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (!(string.IsNullOrEmpty(reader["Received_Trans_ID"].ToString().Trim())))
                            intReceived_Trans_ID = Convert.ToInt32(reader["Received_Trans_ID"].ToString().Trim());
                    }
                    BusinessTier.DisposeReader(reader);
                    BusinessTier.DisposeConnection(conn);
                    if (intReceived_Trans_ID > 0)
                    {
                        System.Drawing.ColorConverter colConvert = new ColorConverter();
                        editLink.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString("Red");
                        
                    }
                    else
                    {
                        System.Drawing.ColorConverter colConvert = new ColorConverter();
                        editLink.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString("Green");
                    }
                }
                if (rdoButton.Text == "2")//Contract
                {
                    txtQuote_Contract.Text = cboContractNo.Text.ToString();
                    txtQuote_Contract.ToolTip = cboContractNo.SelectedValue.ToString().Trim();
                    strContQuoteID = cboContractNo.SelectedValue.ToString().Trim();
                    strContrQuote = cboContractNo.Text.ToString();
                    strStatus = "Contr";
                    txtCalibrationType.Visible = false;
                    cboCalibrationType.Visible = true;
                    txtQty.ToolTip = strContratQuotationDetailID.ToString().Trim();
                    headerItem["Quantity"].Text = "Seriel/Maker/Qty";

                    if (txtContractFlag.Text.ToString().Trim() == "S")
                    {
                        txtQty.Text = strCalibType_SerielNo.ToString().Trim();
                    }
                    if (txtContractFlag.Text.ToString().Trim() == "M")
                    {
                        txtQty.Text = txtModel.Text.ToString().Trim() + "/" + txtMaker.Text.ToString().Trim();
                    }
                    if (txtContractFlag.Text.ToString().Trim() == "Q")
                    {
                    }

                    if (string.IsNullOrEmpty(cboContractNo.SelectedValue.ToString().Trim()))
                    {
                        ShowMessage(206);
                        return;
                    }

                    conn.Open();
                    int intReceived_Trans_ID = 0;
                    SqlCommand cmd = new SqlCommand("Select Received_Trans_ID from CRM_ReceiveEquipment_Trans where CONTEQUIP_ID='" + strContratQuotationDetailID.Trim() + "' and EQUIPMENT_ID='" + strEquipmentId.Trim() + "' and MASTER_CONTRACT_ID ='" + cboContractNo.SelectedValue.ToString().Trim() + "' and Customer_Id=" + CboCustomer.SelectedValue.ToString().Trim() + " and Deleted=0", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (!(string.IsNullOrEmpty(reader["Received_Trans_ID"].ToString().Trim())))
                            intReceived_Trans_ID = Convert.ToInt32(reader["Received_Trans_ID"].ToString().Trim());
                    }
                    BusinessTier.DisposeReader(reader);
                    BusinessTier.DisposeConnection(conn);
                    if (intReceived_Trans_ID > 0)
                    {
                        System.Drawing.ColorConverter colConvert = new ColorConverter();
                        editLink.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString("Red");
                    }
                    else
                    {
                        System.Drawing.ColorConverter colConvert = new ColorConverter();
                        editLink.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString("Green");
                    }
                    //conn.Open();
                    //SqlCommand cmd = new SqlCommand("Select CONTRACT_ID,SERIAL_NO from Master_ContractEquipment_Details where EQUIPMENT_ID='" + strID1 + "' and MASTER_CONTRACT_ID ='" + cboContractNo.SelectedValue.ToString().Trim() + "' and Deleted=0", conn);
                    //SqlDataReader reader = cmd.ExecuteReader();
                    //while (reader.Read())
                    //{
                    //    //txtCalibrationType.Text = reader["SERIAL_NO"].ToString().Trim();
                    //    txtCalibrationType.Visible = false;
                    //    cboCalibrationType.Visible = true;
                    //    headerItem["Quantity"].Text = "Seriel No";
                    //    txtQty.Text = reader["SERIAL_NO"].ToString().Trim();
                    //    txtQty.ToolTip = reader["CONTRACT_ID"].ToString().Trim();

                    //    // Need to do validate this seriel number already validated or not.
                    //    //-------------------------------------------------------

                    //    //-------------------------------------------------------------

                    //}
                    //BusinessTier.DisposeReader(reader);
                    //BusinessTier.DisposeConnection(conn);
                }

                string strValue = radioButtonlist.SelectedItem.Text.ToString();
                string strPartialFull = "";
                if ((strValue == "Partial") || (strValue == "1"))
                    strPartialFull = "Partial";

                if ((strValue == "Full") || (strValue == "2"))
                    strPartialFull = "Full";

                editLink.Attributes["href"] = "";
                editLink.Attributes["onclick"] = String.Format("return ShowEditForm('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}');", strEquipmentId.Trim(), CboCustomer.SelectedValue.ToString(), strStatus, strContQuoteID, strContrQuote, CboCustomer.Text.ToString(), strContratQuotationDetailID.Trim(), strCalibType_SerielNo.Trim(), strPartialFull.Trim(), txtContractFlag.Text.ToString().Trim());

                //if ((txtContractFlag.Text.ToString().Trim() != "S") &&  (txtContractFlag.Text.ToString().Trim() != "M"))
                //{
                //    if ((Convert.ToInt32(txtQty.Text.ToString().Trim())) > 0)
                //    else
                //    {
                //        ShowMessage("The Quantity is not sufficient to print barcode", "Red");
                //        return;
                //    }
                //}

            }
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);

            lblStatus.Text = ex.Message.ToString();
            ////InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_IH", "RadGrid1_ItemDataBound", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper(string typeMasterDetail, string strMasterId, string strCustID)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "";
        if ((lblQuatmasterid.Text.ToString().Trim() == "0") || (string.IsNullOrEmpty(lblQuatmasterid.Text.ToString().Trim())))
            lblQuatmasterid.Text = "0";

        if ((lblContactId.Text.ToString().Trim() == "0") || (string.IsNullOrEmpty(lblContactId.Text.ToString().Trim())))
            lblContactId.Text = "0";

        if (typeMasterDetail == "NoQuote")
            sql = "select QUOTATION_ID,QUOTATION_TRANS_ID as ContratQuotationDetailID,EQUIPMENT_ID,EQUIPMENT_NO,EQUIPMENT_NAME,MAKER,MODEL,RANGES,CAL_PROC_NO,MU,(qty-ReceivedQty) as qty, qty as OriginalQty,QUOTATION_NO, Calib_type as CalibType_SerielNo,Calib_type as ContFlag FROM Vw_QuotationDetail_Equipment WHERE Deleted=10  and  branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' order by Created_Date desc";

        else if (typeMasterDetail == "Quote")
            sql = "select QUOTATION_ID,QUOTATION_TRANS_ID as ContratQuotationDetailID,EQUIPMENT_ID,EQUIPMENT_NO,EQUIPMENT_NAME,MAKER,MODEL,RANGES,CAL_PROC_NO,MU,(qty-ReceivedQty) as qty, qty as OriginalQty,QUOTATION_NO, Calib_type as CalibType_SerielNo,Calib_type as ContFlag  FROM Vw_QuotationDetail_Equipment WHERE branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and Deleted=0 and Quotation_Id='" + lblQuatmasterid.Text + "' and DetailQuoteFlag=0 order by Created_Date desc";

        if (typeMasterDetail == "NoContr")
            sql = "select CONTRACT_ID as ContratQuotationDetailID, EQUIPMENT_ID,EQUIPMENT_NO,EQUIPMENT_NAME,MAKER,MODEL,RANGES,CAL_PROC_NO,MU,CONTRACT_QTY as OriginalQty,(CONTRACT_QTY - ReceivedQty) as qty,Contract_No as CONTRACTNO,Serial_No as CalibType_SerielNo,CONTRACT_FLAG as ContFlag from Vw_CRMContractEquipmentNew where deleted=10 and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and Contract_No <> ''";

        else if (typeMasterDetail == "Contr")
            sql = "select CONTRACT_ID as ContratQuotationDetailID, EQUIPMENT_ID,EQUIPMENT_NO,EQUIPMENT_NAME,MAKER,MODEL,RANGES,CAL_PROC_NO,MU,CONTRACT_QTY as OriginalQty,(CONTRACT_QTY - ReceivedQty) as qty,Contract_No as CONTRACTNO,Serial_No as CalibType_SerielNo,CONTRACT_FLAG as ContFlag from Vw_CRMContractEquipmentNew where deleted=0 and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and MASTER_CONTRACT_ID='" + lblContactId.Text.Trim() + "'  and Contract_No <> '' and DetailContFlag=0";

        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            if (rdoButton.Text == "1")
            {
                if (lblQuatmasterid.Text.ToString().Trim() == "0")
                    RadGrid1.DataSource = DataSourceHelper("NoQuote", "0", "0");
                else
                    RadGrid1.DataSource = DataSourceHelper("Quote", lblQuatmasterid.Text.ToString().Trim(), CboCustomer.SelectedValue.ToString().Trim());
            }
            if (rdoButton.Text == "2")
            {
                if (lblContactId.Text.ToString().Trim() == "0")
                    RadGrid1.DataSource = DataSourceHelper("NoContr", "0", "0");
                else
                    RadGrid1.DataSource = DataSourceHelper("Contr", lblContactId.Text.ToString().Trim(), CboCustomer.SelectedValue.ToString().Trim());
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message.ToString();
            ////InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_IH", "RadGrid1_NeedDataSource", ex.ToString(), "Audit");
        }
    }

    protected void linkDownload_OnClick(object sender, EventArgs e)
    {
        try
        {

            //if (!(Session["sesMRVMasterID"].ToString().Trim() == "0"))
            //{
            //link_Preview.Attributes["href"] = "#";
            //linkDownload.Attributes["onclick"] = String.Format("return ShowEditForm('{0}','{1}');", Session["sesMRVMasterID"], 1);
            //link_Preview.Attributes.Add("onclick", "javascript:ShowEditForm('" + lblTest.Text.Trim() + "')");
            //}
            //else
            //{
            //    ShowMessage(86);
            //}
            //Response.Write("<script type='text/javascript'>");
            //ScriptManager.RegisterStartupScript(this, typeof(string), "openNewWindow", "window.open(' New Page Name.aspx')", true);
            //Response.Write("window.open('http://localhost/SirimNew/PO/PO-1081-Serba Dinamik.pdf', '_newtab');");
            //Response.Write("</script>");

            if (!(string.IsNullOrEmpty(linkDownload.Text.ToString().Trim())))
            {
                string strPah = WebConfigurationManager.AppSettings["WC_POGetPath"].ToString();
                //string strLink = "http://localhost/SirimNew/PO/" + linkDownload.Text.Trim();
                string strLink = strPah + linkDownload.Text.Trim();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenNewTab", "window.open('" + strLink + "','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=800,height=1200');", true);
            }
        }
        catch (Exception Ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();

        if (e.CommandName == "RowClick" && e.Item is GridDataItem)
        {
            try
            {
                e.Item.Selected = true;
                string strTakenId = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ContratQuotationDetailID"]).ToString();
                string strStatus = "";
                string strContQuoteID = "";
                string strContrQuote = "";
                conn.Open();
                string strCustomerID = "0";
                SqlDataReader reader1 = BusinessTier_CRM.GetCustomerIDbyName(conn, CboCustomer.Text.ToString(), Convert.ToInt32(CboCustomer.SelectedValue.ToString()));
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
                if (rdoButton.Text == "1")//quote
                {
                    strStatus = "Quote";
                    SqlDataReader reader2 = BusinessTier_CRM.GetQuoteIDbyNo(conn, cboQuotationNo.Text.ToString().Trim(), Convert.ToInt32(strCustomerID.Trim()));
                    if (reader2.Read())
                    {
                        if (!(string.IsNullOrEmpty(reader2["QUOTATION_ID"].ToString())))
                        {
                            strContQuoteID = (reader2["QUOTATION_ID"].ToString());
                            strContrQuote = cboQuotationNo.Text.ToString().Trim();
                        }
                        else
                        {
                            ShowMessage(207);
                            return;
                        }
                    }
                    BusinessTier.DisposeReader(reader2);
                }
                if (rdoButton.Text == "2")//contract
                {
                    strStatus = "Contr";
                    SqlDataReader reader = BusinessTier_CRM.GetContractIDbyNo(conn, cboContractNo.Text.ToString().Trim(), Convert.ToInt32(strCustomerID.Trim()));
                    if (reader.Read())
                    {
                        if (!(string.IsNullOrEmpty(reader["MASTER_CONTRACT_ID"].ToString())))
                        {
                            strContQuoteID = (reader["MASTER_CONTRACT_ID"].ToString());
                            strContrQuote = cboContractNo.Text.ToString().Trim();
                        }
                        else
                        {
                            ShowMessage(206);
                            return;
                        }
                    }
                    BusinessTier.DisposeReader(reader);
                }


                BusinessTier.DisposeConnection(conn);

                //RadWindow window1 = new RadWindow();
                //window1.NavigateUrl = "CRM_ReceivingEquipment_Child.aspx?Equip_ID=" + strTakenId.ToString().Trim() + "&CustID=" + strCustomerID.Trim() + "&Status=" + strStatus + "&ContEquipID=" + strContQuoteID + "&contquip=" + strContrQuote + "&Customer=" + CboCustomer.Text.ToString();
                //window1.VisibleOnPageLoad = true;
                //window1.Width = 1150;
                //window1.Height = 620;

                //RadWindowManager1.Windows.Add(window1);
                ////window1. = true;
                //this.form1.Controls.Add(window1);
                //window.radopen("CRM_ReceivingEquipment_Child.aspx?Equip_ID=" + id + "&CustID=" + CustID + "&Status=" + status + "&ContEquipID=" + contequipid + "&contquip=" + contequip + "&Customer=" + customer, "UserListDialog");

                //editLink.Attributes["onclick"] = String.Format("return ShowEditForm('{0}','{1}','{2}','{3}','{4}','{5}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EQUIPMENT_ID"], CboCustomer.SelectedValue.ToString(), strStatus, strContQuoteID, strContrQuote, CboCustomer.Text.ToString());

                //editLink.Attributes["onclick"] = String.Format("return ShowEditForm('{0}','{1}','{2}','{3}','{4}','{5}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EQUIPMENT_ID"], CboCustomer.SelectedValue.ToString(), strStatus, strContQuoteID, strContrQuote, CboCustomer.Text.ToString());


                //    SqlConnection conn = BusinessTier.getConnection();
                //    conn.Open();
                //    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT dbo.Master_Customer.CUSTOMER_CODE, dbo.Master_Customer.CRM_ID, dbo.Master_Customer.CUSTOMER_NAME,dbo.CRM_ReceivePO.PO_ID, dbo.CRM_ReceivePO.CUSTOMER_ID, dbo.CRM_ReceivePO.QUOTATION_ID, dbo.CRM_ReceivePO.QUOTATION_NO,dbo.CRM_ReceivePO.CONTEQUIP_ID, dbo.CRM_ReceivePO.CONTRACTNO, dbo.CRM_ReceivePO.PO_DATE, dbo.CRM_ReceivePO.PO_AMOUNT, dbo.CRM_ReceivePO.PO_NUMBER, dbo.CRM_ReceivePO.COMMENTS, dbo.CRM_ReceivePO.FILE_PATH, dbo.CRM_ReceivePO.CREATED_BY, dbo.CRM_ReceivePO.CREATED_DATE, dbo.CRM_ReceivePO.DELETED FROM  dbo.CRM_ReceivePO INNER JOIN dbo.Master_Customer ON dbo.CRM_ReceivePO.CUSTOMER_ID = dbo.Master_Customer.CUSTOMER_ID where dbo.CRM_ReceivePO.PO_ID='" + strTakenId.Trim() + "' and dbo.CRM_ReceivePO.DELETED=0", conn);
                //    DataTable g_datatable = new DataTable();
                //    sqlDataAdapter.Fill(g_datatable);
                //    BusinessTier.DisposeAdapter(sqlDataAdapter);
                //    foreach (DataRow reader in g_datatable.Rows)
                //    {
                //        intCustID = Convert.ToInt32(reader["CUSTOMER_ID"].ToString());
                //        intQuoteID = Convert.ToInt32(reader["QUOTATION_ID"].ToString());
                //        QuoteNo = (reader["QUOTATION_NO"].ToString());

                //        intContactID = Convert.ToInt32(reader["CONTEQUIP_ID"].ToString());
                //        strContract = reader["CONTRACTNO"].ToString();
                //        txtPODate.DbSelectedDate = Convert.ToDateTime(reader["PO_DATE"].ToString());
                //        //txtPODate.Text = String.Format("{0:dd/MMM/yyyy}", reader["PO_DATE"].ToString());

                //        txtAmount.Text = (reader["PO_AMOUNT"].ToString());
                //        txtPO.Text = (reader["PO_NUMBER"].ToString());
                //        txtComments.Text = (reader["COMMENTS"].ToString());
                //        strFileName = (reader["FILE_PATH"]).ToString();
                //    }
                //    BusinessTier.DisposeConnection(conn);
                //    if (intQuoteID != 0)
                //    {
                //        cboQuotationNo.SelectedValue = intQuoteID.ToString();
                //        cboQuotationNo.Text = QuoteNo.ToString();
                //    }
                //    if (intContactID != 0)
                //    {
                //        cboContractNo.SelectedValue = intContactID.ToString();
                //        cboContractNo.Text = strContract.ToString().Trim();
                //    }

                //    if (!(string.IsNullOrEmpty(strFileName)))
                //    {
                //        linkDownload.Text = strFileName;
                //        linkDownload.Visible = true;
                //        chkLink.Checked = true;
                //        chkLink.Visible = true;
                //        RadAsyncUpload1.Visible = false;
                //    }

                //    txtPO.ToolTip = strTakenId.ToString().Trim();

                //    btnSubmit.ToolTip = "Update";
                //    btnDelete.Visible = true;

                //    if (!(string.IsNullOrEmpty(strFileName.ToString().Trim())))
                //    {
                //        string strPah = WebConfigurationManager.AppSettings["WC_POGetPath"].ToString();
                //        //string strLink = "http://localhost/SirimNew/PO/" + strFileName.Trim();
                //        string strLink = strPah + strFileName.Trim();

                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenNewTab", "window.open('" + strLink + "','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=800,height=1200');", true);
                //    }
                //    else
                //    {
                //    }
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message.ToString();
                //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_IH", "RadGrid1_ItemCommand", ex.ToString(), "Audit");
            }
            finally
            {
                BusinessTier.DisposeConnection(conn);
            }
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboQuotationNo.Text))
        {
            lblStatus.Text = "Quotation Field Cannot be Empty";
            return;
        }

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Registration.aspx?param1=" + cboQuotationNo.SelectedValue.ToString().Trim() + "&param2=" + 0 + "');", true);
    }

    protected void btnQuotReport_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboQuotationNo.Text))
        {
            lblStatus.Text = "Quotation Field Cannot be Empty";
            return;
        }

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Quotation.aspx?param1=" + cboQuotationNo.SelectedValue.ToString().Trim() + "&param2=" + 0 + "');", true);
    }

    protected void btnRegister_OnClick(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
              foreach (GridDataItem item in RadGrid1.Items)
            {
                //RadNumericTextBox txtAprDiscount = (RadNumericTextBox)item.FindControl("txtAprDiscount");
                //RadTextBox txtAccessories = (RadTextBox)item.FindControl("txtAccessories");
                CheckBox ChkSelect = (CheckBox)item.FindControl("ChkSelect");
                string sql2 = "";
                string ID = item.GetDataKeyValue("ContratQuotationDetailID").ToString();
                // string ID = txtjobno.SelectedValue.ToString();
                if (ChkSelect.Checked)
                {
                    //sql2 = "update [Politecknik].[dbo].[Quotation_Details] set [detailquoteflag]=1,[MODIFIED_BY]='" + Session["sesUserID"].ToString().Trim() + "',[MODIFIED_DATE]='" + DateTime.Now.ToString() + "' where [QUOTATION_TRANS_ID]='" + ID.ToString() + "' and deleted=0";
                    //SqlCommand command1 = new SqlCommand(sql2, conn);
                    //command1.ExecuteNonQuery();

                    BusinessTier.Register_Update(conn, Convert.ToInt32(ID.ToString()), Session["sesUserID"].ToString().Trim(), txtRemarks.Text.ToString());
                    lblStatus.Text = "Successfully Register";
                }
                else
                {
                    lblStatus.Text = "Please Select any Equipment";
                }
                }
             
             
            BusinessTier.DisposeConnection(conn);
            RadGrid1.DataSource = DataSourceHelper("Quote","","");
            RadGrid1.Rebind();
        }
        catch (Exception ex)
        {
           // lblStatus.Text = ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }


    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        if (e.Argument == "Rebind")
        {
            RadGrid1.MasterTableView.SortExpressions.Clear();
            RadGrid1.MasterTableView.GroupByExpressions.Clear();
            RadGrid1.Rebind();
        }
    }

    private void ShowMessage(string message, string color)
    {
        lblStatus.Text = message.ToString();
        System.Drawing.ColorConverter colConvert = new ColorConverter();
        string strColor = color.ToString();
        lblStatus.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString(strColor);
    }

    private void ShowMessage(int errorNo)
    {
        lblStatus.Text = BusinessTier_CRM.g_ErrorMessagesDataTable_CRM.Rows[errorNo - 201]["Message"].ToString();
        System.Drawing.ColorConverter colConvert = new ColorConverter();
        string strColor = BusinessTier_CRM.g_ErrorMessagesDataTable_CRM.Rows[errorNo - 201]["Color"].ToString();
        lblStatus.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString(strColor);
    }

    private void InsertLogAuditTrail(string userid, string module, string activity, string result, string flag)
    {
        SqlConnection connLog = BusinessTier.getConnection();
        connLog.Open();
        BusinessTier.InsertLogAuditTrial(connLog, userid, module, activity, result, flag);
        BusinessTier.DisposeConnection(connLog);
    }
}