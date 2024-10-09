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

using System.Net.Mail;
public partial class Quotation : System.Web.UI.Page
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
                DtQaotdt.SelectedDate = DateTime.Now;
                txtValidity.Text = "30";
                DateTime dt = DateTime.Now;
                DateTime dt1 = dt.AddDays(30);
                txtValidityDate.SelectedDate = dt1;
                cboContractNo.Visible = false;
                txtQuotationNo.Text = null;
                // txtQuotationNo.text.tostring() = "";

            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            //  }
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


                txtJobDuration.Text = "10";
                txtTerms.Text = "Cash On Collection";

            }

            // lblStatus.Text = "";
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
        lblname.Text = "Hi, " + Session["Name"].ToString();
    }

    protected void rdoButton_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rdoButton = (RadioButtonList)sender;
        string strValue = rdoButton.SelectedItem.Text.ToString();

        if (rdoButton.Text == "2")
        {

            cboContractNo.Visible = false;
        }
        if (rdoButton.Text == "1")
        {
            cboContractNo.Visible = true;
        }

    }

    protected void DtQaotdt_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
    {

        DateTime dt = DtQaotdt.SelectedDate.Value;
        DateTime dt1 = dt.AddDays(Convert.ToInt32(txtValidity.Text));
        txtValidityDate.SelectedDate = dt1;
    }

    //*******************************//OnTextChanged//*******************************//

    public void txtvald_OnTextChanged(object sender, EventArgs e)
    {
        DateTime dt = DtQaotdt.SelectedDate.Value;
        DateTime dt1 = dt.AddDays(Convert.ToInt32(txtValidity.Text));
        txtValidityDate.SelectedDate = dt1;

    }

    public void txtQty_OnTextChanged(object sender, EventArgs e)
    {
        RadNumericTextBox txtbox = (RadNumericTextBox)sender;

        GridEditableItem editedItem = (GridEditableItem)txtbox.NamingContainer;
        //RadComboBox combobox = (RadComboBox)sender;
        //GridEditFormItem editedItem = (GridEditFormItem)combobox.NamingContainer;
        //  GridEditableItem editedItem = e.Item as GridEditableItem;
        RadComboBox cboEquipmentId = (RadComboBox)editedItem.FindControl("cboEquipmentId");
        RadNumericTextBox txtQty = (RadNumericTextBox)editedItem.FindControl("txtQty");
        RadNumericTextBox txtPrice = (RadNumericTextBox)editedItem.FindControl("txtPrice");
        RadNumericTextBox txtDiscount = (RadNumericTextBox)editedItem.FindControl("txtDiscount");
        RadNumericTextBox txttotalprice = (RadNumericTextBox)editedItem.FindControl("txttotalprice");
        RadNumericTextBox txtadditional = (RadNumericTextBox)editedItem.FindControl("txtadditional");
        RadNumericTextBox lbladdnlprice = (RadNumericTextBox)editedItem.FindControl("lbladdnlprice");
        RadNumericTextBox lblprice = (RadNumericTextBox)editedItem.FindControl("lblprice");
        //double dbldiscnt = 0;
        //dbldiscnt = Convert.ToDouble(txtPrice.Value) * Convert.ToDouble(txtDiscount.Value) / 100;

        //dbldiscnt = Math.Round(dbldiscnt, 1, MidpointRounding.AwayFromZero);
        //txtPrice.Value = txtPrice.Value - dbldiscnt;

        // txttotalprice.Value = txtQty.Value * txtPrice.Value + (txtQty.Value * txtaddnlprice.Value * lbladdnlprice.Value);

        txttotalprice.Value = (txtQty.Value * lblprice.Value) + (txtQty.Value * txtadditional.Value * lbladdnlprice.Value);
        double dbldiscnt = 0;
        dbldiscnt = Convert.ToDouble(txtPrice.Value) * Convert.ToDouble(txtDiscount.Value) / 100;

        dbldiscnt = Math.Round(dbldiscnt, 1, MidpointRounding.AwayFromZero);
        txtPrice.Value = txtPrice.Value - dbldiscnt;

        txttotalprice.Value = txtPrice.Value * txtQty.Value;

    }

    public void txtPrice_OnTextChanged(object sender, EventArgs e)
    {

        RadNumericTextBox txtbox = (RadNumericTextBox)sender;
        GridEditableItem editedItem = (GridEditableItem)txtbox.NamingContainer;
        //RadComboBox combobox = (RadComboBox)sender;
        //GridEditFormItem editedItem = (GridEditFormItem)combobox.NamingContainer;
        //  GridEditableItem editedItem = e.Item as GridEditableItem;
        RadComboBox cboEquipmentId = (RadComboBox)editedItem.FindControl("cboEquipmentId");
        RadNumericTextBox txtQty = (RadNumericTextBox)editedItem.FindControl("txtQty");
        RadNumericTextBox txtPrice = (RadNumericTextBox)editedItem.FindControl("txtPrice");
        RadNumericTextBox txtDiscount = (RadNumericTextBox)editedItem.FindControl("txtDiscount");
        RadNumericTextBox txttotalprice = (RadNumericTextBox)editedItem.FindControl("txttotalprice");
        RadNumericTextBox lblprice = (RadNumericTextBox)editedItem.FindControl("lblprice");
        RadNumericTextBox txtadditional = (RadNumericTextBox)editedItem.FindControl("txtadditional");
        RadNumericTextBox lbladdnlprice = (RadNumericTextBox)editedItem.FindControl("lbladdnlprice");
        if (rdoButton.SelectedItem.Text.ToString().Trim() == "Contract")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert(' You dont have permission to give Discount for Contract Equipments');", true);
            txtPrice.Value = lblprice.Value;
            return;
        }
        if (txtPrice.ToolTip != txtPrice.Text)
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();

            if ((cboContractNo.Text.ToString().Trim() == "NO CONTRACT") || (string.IsNullOrEmpty(cboContractNo.Text.ToString().Trim())) || ((cboContractNo.Text.ToString().Trim() == "CONTRACT NO")))
            {

                string sql = "select fee,Additional_Price FROM Master_Equipment WHERE Deleted = 0 and Equipment_ID = '" + cboEquipmentId.SelectedValue + "'";
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    //  txtPrice.Text = reader["fee"].ToString();
                    lbladdnlprice.Text = reader["Additional_Price"].ToString();
                    lblprice.Text = reader["fee"].ToString();

                    //  txtPrice.Value = txtPrice.Value + (txtQty.Value * txtaddnlprice.Value * lbladdnlprice.Value);

                }
                BusinessTier.DisposeReader(reader);
            }
            else
            {
                string sql = "select Contract_Price FROM Vw_ContractEquipment WHERE Deleted = 0 and customer_ID='" + cboCustomerId.SelectedValue + "' and Equipment_ID = '" + cboEquipmentId.SelectedValue + "' and contract_No='" + cboContractNo.Text + "'";
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // txtPrice.Text = reader["Contract_Price"].ToString();
                    lblprice.Text = reader["Contract_Price"].ToString();


                }
                else
                {
                    ShowMessage(78);
                }
                BusinessTier.DisposeReader(reader);

            }

            BusinessTier.DisposeConnection(conn);
            double dbldiscnt = 0;
            dbldiscnt = Convert.ToDouble(lblprice.Value) * Convert.ToDouble(10) / 100;

            dbldiscnt = Math.Round(dbldiscnt, 1, MidpointRounding.AwayFromZero);
            dbldiscnt = Convert.ToDouble(lblprice.Value) - dbldiscnt;
            if (Convert.ToDouble(txtPrice.Value) < dbldiscnt)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('Warning : You need to wait for approval to generate the Quotation.Because You dont have permission to give Discount morethan 10 % .');", true);
                //  lblStatus.Text = "You don't have permission to give Discount morethan 10 % " + "RM (" + dbldiscnt + ")";
                //  txtPrice.Text = lblprice.Text;
            }
            //else
            //{

            txttotalprice.Value = txtPrice.Value * txtQty.Value;

            if (lblprice.Value <= txtPrice.Value)
            {
                txtDiscount.Value = 0;
            }
            else
            {
                double dbldiscnt1 = 0;
                dbldiscnt1 = Convert.ToDouble(lblprice.Value) - Convert.ToDouble(txtPrice.Value);
                dbldiscnt1 = (dbldiscnt1 / Convert.ToDouble(lblprice.Value)) * 100;
                dbldiscnt1 = Math.Round(dbldiscnt1, 1, MidpointRounding.AwayFromZero);
                txtDiscount.Value = dbldiscnt1;
            }

            // }
            txtDiscount.ToolTip = Convert.ToString(txtDiscount.Value);
        }




    }

    public void txtadditional_OnTextChanged(object sender, EventArgs e)
    {
        RadNumericTextBox txtbox = (RadNumericTextBox)sender;

        GridEditableItem editedItem = (GridEditableItem)txtbox.NamingContainer;
        //RadComboBox combobox = (RadComboBox)sender;
        //GridEditFormItem editedItem = (GridEditFormItem)combobox.NamingContainer;
        //  GridEditableItem editedItem = e.Item as GridEditableItem;
        RadComboBox cboEquipmentId = (RadComboBox)editedItem.FindControl("cboEquipmentId");
        RadNumericTextBox txtQty = (RadNumericTextBox)editedItem.FindControl("txtQty");
        RadNumericTextBox txtPrice = (RadNumericTextBox)editedItem.FindControl("txtPrice");
        RadNumericTextBox txtDiscount = (RadNumericTextBox)editedItem.FindControl("txtDiscount");
        RadNumericTextBox txttotalprice = (RadNumericTextBox)editedItem.FindControl("txttotalprice");
        RadNumericTextBox txtadditional = (RadNumericTextBox)editedItem.FindControl("txtadditional");
        RadNumericTextBox lbladdnlprice = (RadNumericTextBox)editedItem.FindControl("lbladdnlprice");
        RadNumericTextBox lblprice = (RadNumericTextBox)editedItem.FindControl("lblprice");
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if ((cboContractNo.Text.ToString().Trim() == "NO CONTRACT") || (string.IsNullOrEmpty(cboContractNo.Text.ToString().Trim())) || ((cboContractNo.Text.ToString().Trim() == "CONTRACT NO")))
            {

                string sql = "select fee,Additional_Price FROM Master_Equipment WHERE Deleted = 0 and Equipment_ID = '" + cboEquipmentId.SelectedValue + "'";
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    lblprice.Text = reader["fee"].ToString();
                }
                BusinessTier.DisposeReader(reader);
            }
            else
            {
                string sql = "select Contract_Price FROM Vw_ContractEquipment WHERE Deleted = 0 and customer_ID='" + cboCustomerId.SelectedValue + "' and Equipment_ID = '" + cboEquipmentId.SelectedValue + "' and contract_No='" + cboContractNo.Text + "'";
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // txtPrice.Text = reader["Contract_Price"].ToString();
                    lblprice.Text = reader["Contract_Price"].ToString();


                }
                else
                {
                    ShowMessage(78);
                }
                BusinessTier.DisposeReader(reader);

            }
            BusinessTier.DisposeConnection(conn);
            txtPrice.Value = lblprice.Value + (txtadditional.Value * lbladdnlprice.Value);

            double dbldiscnt = 0;
            dbldiscnt = Convert.ToDouble(txtPrice.Value) * Convert.ToDouble(txtDiscount.Value) / 100;

            dbldiscnt = Math.Round(dbldiscnt, 1, MidpointRounding.AwayFromZero);
            txtPrice.Value = txtPrice.Value - dbldiscnt;

            txttotalprice.Value = txtPrice.Value * txtQty.Value;
        }
        catch (Exception ex)
        {
            // lblStatus.Text = ex.ToString();
        }
        // txttotalprice.Value = txtQty.Value * lblprice.Value + (txtQty.Value * txtadditional.Value * lbladdnlprice.Value); 

    }

    public void txtDiscount_OnTextChanged(object sender, EventArgs e)
    {


        RadNumericTextBox txtbox = (RadNumericTextBox)sender;

        GridEditableItem editedItem = (GridEditableItem)txtbox.NamingContainer;
        //RadComboBox combobox = (RadComboBox)sender;
        //GridEditFormItem editedItem = (GridEditFormItem)combobox.NamingContainer;
        //  GridEditableItem editedItem = e.Item as GridEditableItem;
        RadComboBox cboEquipmentId = (RadComboBox)editedItem.FindControl("cboEquipmentId");
        RadNumericTextBox txtQty = (RadNumericTextBox)editedItem.FindControl("txtQty");
        RadNumericTextBox txtPrice = (RadNumericTextBox)editedItem.FindControl("txtPrice");
        RadNumericTextBox txtDiscount = (RadNumericTextBox)editedItem.FindControl("txtDiscount");
        RadNumericTextBox txttotalprice = (RadNumericTextBox)editedItem.FindControl("txttotalprice");
        RadNumericTextBox lblprice = (RadNumericTextBox)editedItem.FindControl("lblprice");

        RadNumericTextBox txtadditional = (RadNumericTextBox)editedItem.FindControl("txtadditional");
        RadNumericTextBox lbladdnlprice = (RadNumericTextBox)editedItem.FindControl("lbladdnlprice");

        if (rdoButton.SelectedItem.Text.ToString().Trim() == "Contract")
        {
            txtDiscount.Value = 0;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert(' You dont have permission to give Discount for Contract Equipments');", true);
            return;
        }
        //   if (txtDiscount.ToolTip != txtDiscount.Text)
        //   {
        double dbldiscnt = 0;

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        if ((cboContractNo.Text.ToString().Trim() == "NO CONTRACT") || (string.IsNullOrEmpty(cboContractNo.Text.ToString().Trim())) || ((cboContractNo.Text.ToString().Trim() == "CONTRACT NO")))
        {

            string sql = "select fee,Additional_Price FROM Master_Equipment WHERE Deleted = 0 and Equipment_ID = '" + cboEquipmentId.SelectedValue + "'";
            SqlCommand command = new SqlCommand(sql, conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                txtPrice.Text = reader["fee"].ToString();
                lblprice.Text = reader["fee"].ToString();
                lbladdnlprice.Text = reader["Additional_Price"].ToString();
                txtPrice.Value = lblprice.Value + (txtadditional.Value * lbladdnlprice.Value);
                // txtPrice.Value = txtPrice.Value + (txtQty.Value * txtaddnlprice.Value * lbladdnlprice.Value);
            }
            BusinessTier.DisposeReader(reader);
        }
        else
        {
            string sql = "select Contract_Price FROM Vw_ContractEquipment WHERE Deleted = 0 and customer_ID='" + cboCustomerId.SelectedValue + "' and Equipment_ID = '" + cboEquipmentId.SelectedValue + "' and contract_No='" + cboContractNo.Text + "'";
            SqlCommand command = new SqlCommand(sql, conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                txtPrice.Text = reader["Contract_Price"].ToString();
                lblprice.Text = reader["Contract_Price"].ToString();

            }
            else
            {
                ShowMessage(78);
            }
            BusinessTier.DisposeReader(reader);

        }

        BusinessTier.DisposeConnection(conn);

        if (txtDiscount.Value > 10)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('Warning : You need to wait for approval to generate the Quotation.Because You dont have permission to give Discount morethan 10 % .');", true);
            // lblStatus.Text = "You don't have permission to give morethan 10 %";
            // txtDiscount.Value = 10;
            //  txttotalprice.Value = txtPrice.Value * txtQty.Value;
            // return;
        }


        dbldiscnt = Convert.ToDouble(txtPrice.Value) * Convert.ToDouble(txtDiscount.Value) / 100;

        dbldiscnt = Math.Round(dbldiscnt, 1, MidpointRounding.AwayFromZero);
        txtPrice.Value = txtPrice.Value - dbldiscnt;

        txttotalprice.Value = txtPrice.Value * txtQty.Value;

        txtPrice.ToolTip = Convert.ToString(txtPrice.Value);

        //}


    }

    //*******************************//SelectedIndexChanged & OnItemsRequested//*******************************//

    protected void cboCustomerId_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            txtQuotationNo.Text = null;
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "";
            //  if (!(string.IsNullOrEmpty(cboEnquiryId.Text.ToString().Trim())))
            //sql1 = " select Customer_Id,Customer_Name,CRM_ID from Master_Customer where DELETED=0  and customer_id=(select customer_id FROM Enquiry WHERE Deleted = 0 and Enquiry_ID = '" + cboEnquiryId.SelectedValue + "')";
            if (string.IsNullOrEmpty(txtCustomer.Text.ToString().Trim()))
                sql1 = " select Customer_Id,Customer_Name,CRM_ID from Master_Customer where DELETED=0  and [Customer_Name] LIKE @text + '%' order by Customer_Name";

            else
                sql1 = " select Customer_Id,Customer_Name,CRM_ID from Master_Customer where DELETED=0  and [Customer_Name] LIKE '" + txtCustomer.Text.ToString().Trim() + "' + '%' and [Customer_Name] LIKE @text + '%' order by Customer_Name";
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

            //ShowMessage("Please Select the Installation Name", "Red");
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

    protected void cboEquipmentId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RadComboBox combobox = (RadComboBox)sender;
            GridEditFormItem editedItem = (GridEditFormItem)combobox.NamingContainer;
            //  GridEditableItem editedItem = e.Item as GridEditableItem;
            RadComboBox cboEquipmentId = (RadComboBox)editedItem.FindControl("cboEquipmentId");
            RadNumericTextBox txtQty = (RadNumericTextBox)editedItem.FindControl("txtQty");
            RadNumericTextBox txtDiscount = (RadNumericTextBox)editedItem.FindControl("txtDiscount");
            RadNumericTextBox txttotalprice = (RadNumericTextBox)editedItem.FindControl("txttotalprice");
            RadNumericTextBox txtPrice = (RadNumericTextBox)editedItem.FindControl("txtPrice");
            RadNumericTextBox lblprice = (RadNumericTextBox)editedItem.FindControl("lblprice");
            TextBox txtDesc = (TextBox)editedItem.FindControl("txtDesc");
            RadNumericTextBox txtadditional = (RadNumericTextBox)editedItem.FindControl("txtadditional");
            RadNumericTextBox lbladdnlprice = (RadNumericTextBox)editedItem.FindControl("lbladdnlprice");
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();

            if (rdoButton.SelectedItem.Text.ToString().Trim() == "Contract")
            {
                string sql = "select Contract_Price FROM Vw_ContractEquipment WHERE Deleted = 0 and customer_ID='" + cboCustomerId.SelectedValue + "' and Equipment_ID = '" + cboEquipmentId.SelectedValue + "' and contract_No='" + cboContractNo.Text + "'";
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    txtPrice.Text = reader["Contract_Price"].ToString();
                    lblprice.Text = reader["Contract_Price"].ToString();

                }
                else
                {
                    ShowMessage(78);
                    return;
                }
                BusinessTier.DisposeReader(reader);
            }
            else
            {
                string sql = "select fee,Additional_Price FROM Master_Equipment WHERE Deleted = 0 and Equipment_ID = '" + cboEquipmentId.SelectedValue + "'";
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    txtPrice.Text = reader["fee"].ToString();
                    lblprice.Text = reader["fee"].ToString();
                    lbladdnlprice.Text = reader["Additional_Price"].ToString();

                }
                BusinessTier.DisposeReader(reader);
            }


            BusinessTier.DisposeConnection(conn);



            txtDiscount.Value = 0;
            if (cboEquipmentId.Text == "Transportation")
            {
                txtQty.Value = 0;
                txtDesc.Text = "Transportation";
            }
            else
            {
                txtQty.Value = 1;
            }

            double dbldiscnt = 0;

            dbldiscnt = Convert.ToDouble(txtPrice.Value) * Convert.ToDouble(txtDiscount.Value) / 100;
            dbldiscnt = Math.Round(dbldiscnt, 1, MidpointRounding.AwayFromZero);
            txtPrice.Value = txtPrice.Value - dbldiscnt;
            txttotalprice.Value = txtPrice.Value * txtQty.Value;
            txtadditional.Value = 0;


        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation", "cboEnquiryId_SelectedIndexChanged", ex.ToString(), "Audit");
        }
    }

    protected void cboEquipmentId_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " ";
            RadComboBox combobox = (RadComboBox)sender;
            GridEditFormItem editedItem = (GridEditFormItem)combobox.NamingContainer;
            TextBox txtequpment = (TextBox)editedItem.FindControl("txtequpment");
            if (rdoButton.SelectedItem.Text.ToString().Trim() == "Contract")
            {
                if ((cboContractNo.SelectedValue.ToString().Trim() == "0") || (string.IsNullOrEmpty(cboContractNo.SelectedValue.ToString().Trim())))
                {
                    ShowMessage(86);
                    // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('Please Select the Contract No');", true);

                    // e.Canceled = true;
                    return;
                }

                if (string.IsNullOrEmpty(txtequpment.Text.ToString().Trim()))
                {
                    sql1 = " select Equipment_ID,Equipment_Name,Maker,Model_No as Model,Contract_price as fee,Ranges  from Vw_ContractEquipment where Customer_ID='" + cboCustomerId.SelectedValue + "' and Master_Contract_ID='" + cboContractNo.SelectedValue + "' and DELETED=0 and  Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and [Equipment_Name] LIKE @text + '%' group by Equipment_ID,Equipment_Name,Customer_ID,Maker,Model_No,Ranges,Contract_price order by Equipment_Name";
                }
                else
                {
                    sql1 = " select Equipment_ID,Equipment_Name,Maker,Model_No as Model,Contract_price as fee,Ranges   from Vw_ContractEquipment where Customer_ID='" + cboCustomerId.SelectedValue + "' and Master_Contract_ID='" + cboContractNo.SelectedValue + "' and DELETED=0  and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and [Equipment_Name] LIKE '" + txtequpment.Text.ToString().Trim() + "' + '%' and [Equipment_Name] LIKE @text + '%' group by Equipment_ID,Equipment_Name,Customer_ID,Maker,Model_No,Ranges,Contract_price order by Equipment_Name";
                }

            }
            else
            {
                if (string.IsNullOrEmpty(txtequpment.Text.ToString().Trim()))
                    sql1 = " select Equipment_ID,Equipment_Name,Maker,Model,fee,Ranges from Master_Equipment where DELETED=0 and flag_price=0 and [Equipment_Name] LIKE @text + '%' order by Equipment_Name";
                else
                    sql1 = " select Equipment_ID,Equipment_Name,Maker,Model,fee,Ranges from Master_Equipment where DELETED=0  and flag_price=0 and [Equipment_Name] LIKE '" + txtequpment.Text.ToString().Trim() + "' + '%' and [Equipment_Name] LIKE @text + '%' order by Equipment_Name";
            }

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

    protected void cboEnquiryId_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "";
            if (string.IsNullOrEmpty(cboCustomerId.Text.ToString().Trim()))
            {
                sql1 = " select Enquiry_Id,Enquiry_No,Enquiry_Date from Vw_Enquiry_Customer where branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and DELETED=0 and status='PENDING'  order by Enquiry_No";
            }
            else
            {
                sql1 = " select Enquiry_Id,Enquiry_No,Enquiry_Date from Vw_Enquiry_Customer where Customer_ID='" + cboCustomerId.SelectedValue + "' and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and DELETED=0 and status='PENDING' and [Enquiry_No] LIKE @text + '%' order by Enquiry_No";
            }
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Enquiry_No"].ToString();
                item.Value = row["Enquiry_Id"].ToString();
                item.Attributes.Add("Enquiry_No", row["Enquiry_No"].ToString());
                // item.Attributes.Add("Enquiry_Date", row["Enquiry_Date"].ToString());
                DateTime EnquiryDate = (DateTime)row["Enquiry_Date"];

                item.Attributes.Add("Enquiry_Date", EnquiryDate.ToString());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);
            // Session["rdobutton"] = "";
        }


        catch (Exception ex)
        {

            //ShowMessage("Please Select the Installation Name", "Red");
        }

    }

    protected void cboEnquiryId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (!(cboEnquiryId.Text.ToString() == "Choose Enquiry No"))
            {
                // string strMRVNo = cboEnquiryId.Text.ToString().Trim();
                if (string.IsNullOrEmpty(cboEnquiryId.SelectedValue.ToString().Trim()))
                {
                    //RadGrid1.DataSource = DataSourceHelper("0");
                    //RadGrid1.Rebind();
                    //ShowMessage(25);
                    //return;
                }
                else
                {
                    SqlConnection conn = BusinessTier.getConnection();
                    conn.Open();
                    string sql = " select Customer_Id,Customer_Name,CRM_ID from Master_Customer where DELETED=0  and customer_id=(select customer_id FROM Enquiry WHERE Deleted = 0 and Enquiry_ID = '" + cboEnquiryId.SelectedValue + "')";
                    SqlCommand command = new SqlCommand(sql, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        cboCustomerId.SelectedValue = reader["Customer_Id"].ToString();
                        cboCustomerId.Text = reader["Customer_Name"].ToString();

                    }
                    BusinessTier.DisposeReader(reader);
                    BusinessTier.DisposeConnection(conn);
                    //RadGrid1.DataSource = DataSourceHelper(cboEnquiryId.SelectedValue.ToString().Trim());
                    //RadGrid1.Rebind();
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation", "cboEnquiryId_SelectedIndexChanged", ex.ToString(), "Audit");
        }
    }

    protected void cboContractNo_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            DateTime dt = DtQaotdt.SelectedDate.Value;
            string StrExpDate = dt.Month + "/" + dt.Day + "/" + dt.Year;
            string sql1 = " select MASTER_CONTRACT_ID,Contract_No,Expiry_Date,Contract_Date from Master_Contract where (Customer_ID='" + cboCustomerId.SelectedValue + "' and DELETED=0 and Expiry_Date>='" + StrExpDate + "') or(Customer_ID=0)  and [Contract_No] LIKE @text + '%' order by Contract_No";
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
                    DateTime Expiry_Date = DateTime.Now.AddDays(Convert.ToInt32(txtValidity.Text));
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
            // Session["rdobutton"] = "";
        }


        catch (Exception ex)
        {

            //ShowMessage("Please Select the Installation Name", "Red");
        }

    }

    private object DataSourceHelper(string strEnquiryID)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        // string sql = "select * FROM MRV_Detail WHERE Enquiry_ID = '" + strEnquiryID.ToString().Trim() + "'  and  Status='Pending' and Branch_Id = '1'  and Deleted=0 order by Enquiry_Detail_ID desc";

        string sql = "select * FROM Vw_Enquiry WHERE Enquiry_ID = '" + strEnquiryID.ToString().Trim() + "' and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "'  and Deleted1=0 and Flag='E' order by Enquiry_Detail_ID desc";

        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "RowClick" && e.Item is GridDataItem)
            {
                GridDataItem data = (GridDataItem)e.Item;

                int intqty = Convert.ToInt32(data["Qty"].Text);
                int intEquipmentid = Convert.ToInt32(data["Equipment_ID"].Text);
                e.Item.Selected = true;
                string strDetailId = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Enquiry_Detail_ID"]).ToString();


                double intprice = 0;

                if (RadGridQuot.Items.Count.ToString() == "0" && string.IsNullOrEmpty(txtQuotationNo.Text.ToString()))
                {

                    if (rdoButton.SelectedItem.Text.ToString().Trim() == "Contract")
                    {
                        if ((cboContractNo.Text.ToString().Trim() == "CONTRACT NO") || (cboContractNo.Text.ToString().Trim() == "NO CONTRACT") || (string.IsNullOrEmpty(cboContractNo.SelectedValue.ToString().Trim())))
                        {
                            ShowMessage(86);
                            e.Canceled = true;
                            return;
                        }

                        SqlConnection conn = BusinessTier.getConnection();
                        conn.Open();
                        DateTime dttime = DateTime.Now;
                        string sql = "", sql1 = "";
                        int intinterval = 0, totqty = 0;
                        DateTime cntrctdate = DateTime.Now, expirydate = DateTime.Now;

                        sql1 = "Select Contract_Price as price,Contract_Date,Expiry_Date,interval  from Vw_ContractEquipment where Equipment_Id='" + intEquipmentid + "' and customer_ID='" + cboCustomerId.SelectedValue + "' and contract_No='" + cboContractNo.Text.ToString().Trim() + "' and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 ";
                        SqlCommand command = new SqlCommand(sql1, conn);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            intprice = Convert.ToDouble(reader["price"].ToString());
                            cntrctdate = Convert.ToDateTime(reader["Contract_Date"].ToString());
                            expirydate = Convert.ToDateTime(reader["Expiry_Date"].ToString());
                            intinterval = Convert.ToInt32(reader["interval"].ToString());

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('No Contract for this Equipment ID  " + intEquipmentid + " ');", true);
                            e.Canceled = true;
                            return;

                        }

                        BusinessTier.DisposeReader(reader);
                        String strcntrctdt = null, strexprydt = null;
                        // std2 = System.DateTime.Parse(std);
                        strcntrctdt = cntrctdate.Month + "/" + cntrctdate.Day + "/" + cntrctdate.Year + " 12:00:00 AM";
                        strexprydt = expirydate.Month + "/" + expirydate.Day + "/" + expirydate.Year + " 12:00:00 PM";
                        string sql2 = "select sum(qty) as qty from vw_Quotation where Equipment_Id='" + intEquipmentid + "' and customer_ID='" + cboCustomerId.SelectedValue + "' and contract_No='" + cboContractNo.Text.ToString().Trim() + "' and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and contract_Date >='" + strcntrctdt + "' and Expiry_Date<='" + strexprydt + "' and QD_Deleted=0";
                        SqlCommand command1 = new SqlCommand(sql2, conn);
                        SqlDataReader reader1 = command1.ExecuteReader();
                        if (reader1.Read())
                        {
                            if (string.IsNullOrEmpty(reader1["qty"].ToString()))
                            {
                                totqty = 0;
                            }
                            else
                            {
                                totqty = Convert.ToInt32(reader1["qty"].ToString());
                            }
                        }
                        BusinessTier.DisposeReader(reader1);

                        int diffqty = (intinterval - totqty);
                        if (diffqty < intqty)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('No Contract for this Equipment ID  " + intEquipmentid + " . Please check the Interval');", true);
                            e.Canceled = true;
                            return;

                        }
                        BusinessTier.DisposeConnection(conn);
                    }

                    else
                    {

                        cboContractNo.SelectedValue = "1";


                    }

                    if ((cboContactName.SelectedValue.ToString().Trim() == "0") || (string.IsNullOrEmpty(cboContactName.SelectedValue.ToString().Trim())))
                    {
                        ShowMessage(87);

                        e.Canceled = true;
                        return;
                    }
                    generateQuotationNo("InsertNew");
                }
                else
                {
                    if (rdoButton.SelectedItem.Text.ToString().Trim() == "Contract")
                    {
                        if ((cboContractNo.Text.ToString().Trim() == "CONTRACT NO") || (cboContractNo.Text.ToString().Trim() == "NO CONTRACT") || (string.IsNullOrEmpty(cboContractNo.SelectedValue.ToString().Trim())))
                        {
                            ShowMessage(86);
                            e.Canceled = true;
                            return;
                        }

                        SqlConnection conn = BusinessTier.getConnection();
                        conn.Open();
                        DateTime dttime = DateTime.Now;
                        string sql = "", sql1 = "";

                        int intinterval = 0, totqty = 0;
                        DateTime cntrctdate = DateTime.Now, expirydate = DateTime.Now;

                        sql1 = "Select Contract_Price as price,Contract_Date,Expiry_Date,interval  from Vw_ContractEquipment where Equipment_Id='" + intEquipmentid + "' and customer_ID='" + cboCustomerId.SelectedValue + "' and contract_No='" + cboContractNo.Text.ToString().Trim() + "' and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 ";
                        SqlCommand command = new SqlCommand(sql1, conn);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            intprice = Convert.ToDouble(reader["price"].ToString());
                            cntrctdate = Convert.ToDateTime(reader["Contract_Date"].ToString());
                            expirydate = Convert.ToDateTime(reader["Expiry_Date"].ToString());
                            intinterval = Convert.ToInt32(reader["interval"].ToString());

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('No Contract for this Equipment ID  " + intEquipmentid + " ');", true);
                            e.Canceled = true;
                            return;

                        }

                        BusinessTier.DisposeReader(reader);

                        String strcntrctdt = null, strexprydt = null;
                        // std2 = System.DateTime.Parse(std);
                        strcntrctdt = cntrctdate.Month + "/" + cntrctdate.Day + "/" + cntrctdate.Year + " 12:00:00 AM";
                        strexprydt = expirydate.Month + "/" + expirydate.Day + "/" + expirydate.Year + " 12:00:00 PM";
                        string sql2 = "select sum(qty) as qty from vw_Quotation where Equipment_Id='" + intEquipmentid + "' and customer_ID='" + cboCustomerId.SelectedValue + "' and contract_No='" + cboContractNo.Text.ToString().Trim() + "' and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and contract_Date >='" + strcntrctdt + "' and Expiry_Date<='" + strexprydt + "' and QD_Deleted=0";
                        SqlCommand command1 = new SqlCommand(sql2, conn);
                        SqlDataReader reader1 = command1.ExecuteReader();
                        if (reader1.Read())
                        {
                            if (string.IsNullOrEmpty(reader1["qty"].ToString()))
                            {
                                totqty = 0;
                            }
                            else
                            {
                                totqty = Convert.ToInt32(reader1["qty"].ToString());
                            }
                        }
                        BusinessTier.DisposeReader(reader1);

                        int diffqty = (intinterval - totqty);
                        if (diffqty < intqty)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('No Contract for this Equipment ID  " + intEquipmentid + " . Please check the Interval');", true);
                            e.Canceled = true;
                            return;

                        }
                        BusinessTier.DisposeConnection(conn);
                    }

                    else
                    {

                        cboContractNo.SelectedValue = "1";


                    }

                    if ((cboContactName.SelectedValue.ToString().Trim() == "0") || (string.IsNullOrEmpty(cboContactName.SelectedValue.ToString().Trim())))
                    {
                        ShowMessage(87);

                        e.Canceled = true;
                        return;
                    }

                    if ((lblQuatmasterid.Text.ToString().Trim() == "0") || (string.IsNullOrEmpty(lblQuatmasterid.Text.ToString().Trim())))
                    {
                        ShowMessage(24);
                        e.Canceled = true;
                        return;
                    }
                }

                SqlConnection connsave = BusinessTier.getConnection();
                connsave.Open();
                int flg = 0;
                if (rdoButton.SelectedItem.Text.ToString().Trim() == "Contract")
                {
                    flg = BusinessTier.SaveInsertQuot_Detail(connsave, 1, Convert.ToInt32(lblQuatmasterid.Text), "1 to 2 Weeks", Convert.ToInt32(strDetailId), 3, Convert.ToDecimal(intprice), 0, 0, "Inhouse", "", 0, Convert.ToInt32(cboEnquiryId.SelectedValue), Session["sesUserID"].ToString().Trim(), "C");
                }
                else
                {
                    flg = BusinessTier.SaveInsertQuot_Detail(connsave, 1, Convert.ToInt32(lblQuatmasterid.Text), "1 to 2 Weeks", Convert.ToInt32(strDetailId), 3, 200, 0, 0, "Inhouse", "", 0, Convert.ToInt32(cboEnquiryId.SelectedValue), Session["sesUserID"].ToString().Trim(), "E");

                }
                BusinessTier.DisposeConnection(connsave);
                if (flg >= 1)
                {
                    ShowMessage(69);
                }
                //InsertLogAuditTrial is used to insert the action into MasterLog table
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation_Enquiry_Detail", "Insert", "Success", "Log");
                //RadGrid1.DataSource = DataSourceHelper(cboEnquiryId.SelectedValue.ToString().Trim());
                //RadGrid1.Rebind();
                RadGridQuot.DataSource = DataSourceHelper("T", "0");
                RadGridQuot.Rebind();

            }
        }
        catch (Exception ex)
        {
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation_Enquiry_Detail", "Quotation_Detail", ex.ToString(), "Audit");
            ShowMessage(8);
        }
    }

    protected void cboCalibration_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RadComboBox combobox = (RadComboBox)sender;
            GridEditFormItem editedItem = (GridEditFormItem)combobox.NamingContainer;
            //  GridEditableItem editedItem = e.Item as GridEditableItem;
            RadComboBox cboCalibration = (RadComboBox)editedItem.FindControl("cboCalibration");
            TextBox txtremarksdetails = (TextBox)editedItem.FindControl("txtremarksdetails");
            // RadNumericTextBox txtQty = (RadNumericTextBox)editedItem.FindControl("txtQty");


            if (cboCalibration.Text == "Inhouse")
            {
                txtremarksdetails.Text = "1 to 2 Weeks";
            }
            else if (cboCalibration.Text == "Onsite")
            {
                txtremarksdetails.Text = "3 to 4 Weeks";
            }
            else if (cboCalibration.Text == "SunContract")
            {
                txtremarksdetails.Text = "3 to 4 Weeks or above";
            }
            else if (cboCalibration.Text == "InterBranch")
            {
                txtremarksdetails.Text = "2 to 3 Weeks";
            }




        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation", "cboEnquiryId_SelectedIndexChanged", ex.ToString(), "Audit");
        }
    }

    //protected void generateQuotationNo(string strWorkFrom)
    //{

    //    string strbrnchcode = "";
    //    int intbranchid = 0;
    //    SqlConnection conn = BusinessTier.getConnection();
    //    conn.Open();

    //    string sql11 = " select Branch_ID,BR_Short_Name from VW_Staff_Branch_Department where deleted=0 and Staff_ID='" + Session["sesUserID"].ToString() + "'";
    //    SqlCommand cmd11 = new SqlCommand(sql11, conn);
    //    SqlDataReader reader11 = cmd11.ExecuteReader();
    //    if (reader11.Read())
    //    {
    //        intbranchid = Convert.ToInt32(reader11["Branch_ID"].ToString().Trim());
    //        strbrnchcode = reader11["BR_Short_Name"].ToString().Trim();
    //    }
    //    reader11.Close();

    //    string stryear = System.DateTime.Now.Year.ToString();
    //    stryear = stryear.Substring(2);
    //    string StrOutRefid = "SST/" + strbrnchcode + "/Q/" + stryear + "/0001";


    //    if (strbrnchcode == "0")
    //    {
    //        return;
    //    }
    //    string sql1 = " select * from Quotation where deleted=0 and Branch_id='" + intbranchid + "'";
    //    SqlCommand cmd1 = new SqlCommand(sql1, conn);
    //    SqlDataReader reader1 = cmd1.ExecuteReader();
    //    if (reader1.Read())
    //    {
    //        string sql = " select SUBSTRING(Max(Quotation_No),13,4) as maxOutreffid from Quotation where deleted=0 and Branch_id='" + intbranchid + "' having SUBSTRING(Max(Quotation_No),10,2)='" + stryear + "' ";
    //        reader1.Close();
    //        SqlCommand cmd = new SqlCommand(sql, conn);
    //        SqlDataReader reader = cmd.ExecuteReader();
    //        if (reader.Read())
    //        {
    //            string maxid = reader["maxOutreffid"].ToString().Trim();

    //            int incremaxid = Convert.ToInt32(maxid);
    //            incremaxid = incremaxid + 1;

    //            maxid = incremaxid.ToString().Trim();

    //            if (maxid.Length == 4)
    //            {
    //                StrOutRefid = "SST/" + strbrnchcode + "/Q/" + stryear + "/" + maxid;

    //            }
    //            else if (maxid.Length == 3)
    //            {
    //                StrOutRefid = "SST/" + strbrnchcode + "/Q/" + stryear + "/0" + maxid;

    //            }
    //            else if (maxid.Length == 2)
    //            {
    //                StrOutRefid = "SST/" + strbrnchcode + "/Q/" + stryear + "/00" + maxid;

    //            }
    //            else if (maxid.Length == 1)
    //            {
    //                StrOutRefid = "SST/" + strbrnchcode + "/Q/" + stryear + "/000" + maxid;

    //            }

    //        }
    //        reader.Close();
    //    }
    //    BusinessTier.DisposeConnection(conn);
    //    if (rdoButton.SelectedItem.Text.ToString().Trim() == "Contract")
    //    {
    //        if ((cboContractNo.Text.ToString().Trim() == "CONTRACT NO") || (cboContractNo.Text.ToString().Trim() == "NO CONTRACT") || (string.IsNullOrEmpty(cboContractNo.SelectedValue.ToString().Trim())))
    //        {
    //            ShowMessage(86);
    //            // e.Canceled = true;
    //            return;
    //        }

    //    }

    //    else
    //    {
    //        if ((cboContractNo.SelectedValue.ToString().Trim() == "0") || (string.IsNullOrEmpty(cboContractNo.SelectedValue.ToString().Trim())))
    //        {
    //            cboContractNo.SelectedValue = "1";
    //        }

    //    }
    //    if ((cboContactName.SelectedValue.ToString().Trim() == "0") || (string.IsNullOrEmpty(cboContactName.SelectedValue.ToString().Trim())))
    //    {
    //        ShowMessage(87);

    //        // e.Canceled = true;
    //        return;
    //    }

    //    txtQuotationNo.Text = StrOutRefid.ToString().Trim();
    //    //  "Hello " + userName + ". Today is " + date + ".";

    //    string strjobduation = txtJobDuration.Text + " " + cbojobduration.Text;


    //    SqlConnection connSaveMaster = BusinessTier.getConnection();
    //    connSaveMaster.Open();// NOtes colum need to add
    //    int intgetQuotMasterId = BusinessTier.SaveQuotation_Master(connSaveMaster, 1, intbranchid, StrOutRefid.ToString().Trim(), Convert.ToInt32(cboCustomerId.SelectedValue), Convert.ToInt32(cboContactName.SelectedValue), Convert.ToInt32(cboContractNo.SelectedValue), DtQaotdt.SelectedDate.Value, txtTerms.Text.ToString().Trim(), Convert.ToInt32(txtValidity.Text.ToString().Trim()), strjobduation, "PENDING", cboEnquiryId.SelectedValue, "", txtRemarks.Text.ToString().Trim(), 0, "", Session["sesUserID"].ToString().Trim(), "N");
    //    lblQuatmasterid.Text = intgetQuotMasterId.ToString();
    //    BusinessTier.DisposeConnection(connSaveMaster);

    //    // return StrOutRefid;
    //}

    protected void generateQuotationNo(string strWorkFrom)
    {

        string strbrnchcode = "";
        int intbranchid = 0;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        string sql11 = " select Branch_ID,Short_Name from VW_Staff_Branch_Department where deleted=0 and Staff_ID='" + Session["sesUserID"].ToString() + "'";
        SqlCommand cmd11 = new SqlCommand(sql11, conn);
        SqlDataReader reader11 = cmd11.ExecuteReader();
        if (reader11.Read())
        {
            intbranchid = Convert.ToInt32(reader11["Branch_ID"].ToString().Trim());
            strbrnchcode = reader11["Short_Name"].ToString().Trim();
        }
        reader11.Close();

        string StrOutRefid = SaveAutoQuotationTable(strbrnchcode);

        if (rdoButton.SelectedItem.Text.ToString().Trim() == "Contract")
        {
            if ((cboContractNo.Text.ToString().Trim() == "CONTRACT NO") || (cboContractNo.Text.ToString().Trim() == "NO CONTRACT") || (string.IsNullOrEmpty(cboContractNo.SelectedValue.ToString().Trim())))
            {
                ShowMessage(86);
                // e.Canceled = true;
                return;
            }

        }

        else
        {
            if ((cboContractNo.SelectedValue.ToString().Trim() == "0") || (string.IsNullOrEmpty(cboContractNo.SelectedValue.ToString().Trim())))
            {
                cboContractNo.SelectedValue = "1";
            }

        }
        if ((cboContactName.SelectedValue.ToString().Trim() == "0") || (string.IsNullOrEmpty(cboContactName.SelectedValue.ToString().Trim())))
        {
            ShowMessage(87);

            // e.Canceled = true;
            return;
        }

        txtQuotationNo.Text = StrOutRefid.ToString().Trim();
        //  "Hello " + userName + ". Today is " + date + ".";

        string strjobduation = txtJobDuration.Text + " " + cbojobduration.Text;


        SqlConnection connSaveMaster = BusinessTier.getConnection();
        connSaveMaster.Open();// Notes colum need to add
        int intgetQuotMasterId = BusinessTier.SaveQuotation_Master(connSaveMaster, 1, intbranchid, StrOutRefid.ToString().Trim(), Convert.ToInt32(cboCustomerId.SelectedValue), Convert.ToInt32(cboContactName.SelectedValue), Convert.ToInt32(cboContractNo.SelectedValue), DtQaotdt.SelectedDate.Value, txtTerms.Text.ToString().Trim(), Convert.ToInt32(txtValidity.Text.ToString().Trim()), strjobduation, "PENDING", cboEnquiryId.SelectedValue, "", txtRemarks.Text.ToString().Trim(), 0, "", Session["sesUserID"].ToString().Trim(), "N");
        lblQuatmasterid.Text = intgetQuotMasterId.ToString();
        BusinessTier.DisposeConnection(connSaveMaster);

        // return StrOutRefid;
    }

    protected string SaveAutoQuotationTable(string strBranchCode)
    {
        //Generate Job Number-----------------------------------------------------
        string strgeneratingQuotNo = "";

        DateTime CurrDateTime = DateTime.Now;
        string strCurrYear = CurrDateTime.Year.ToString().Trim();

        string strgetID = "0";
        string strgetAutoNo = "0";
        string strgetYear = "0";
        string strLastAutoNo = "0";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "select * FROM Sales_AutoQuotation WHERE BranchId = '" + Session["sesBranchID"].ToString().Trim() + "' and Year_Val = '" + strCurrYear.ToString().Trim() + "'";
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader readergetID = command.ExecuteReader();
        if (readergetID.Read())
        {
            strgetID = readergetID["QuotAutoId"].ToString().Trim();
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
            SaveAutoQuotationTable(Session["sesBranchID"].ToString().Trim(), "1", strCurrYear.ToString().Trim(), strLastAutoNo.ToString().Trim(), "Insert");
            // strgeneratingQuotNo = strBranchCode.ToString().Trim() + "/" + "1" + "/" + strCurrYear.ToString().Trim();
            strgeneratingQuotNo = "Q/BMCL/" + stryear + "/00" + 1;

        }
        else
        {
            if (strgetYear.ToString() == strCurrYear.ToString())
            {
                Int32 intAutono = Int32.Parse(strgetAutoNo.ToString().Trim());
                Int32 intAutoNoInc = intAutono + 1;
                SaveAutoQuotationTable(Session["sesBranchID"].ToString().Trim(), intAutoNoInc.ToString().Trim(), strCurrYear.ToString().Trim(), strLastAutoNo.ToString().Trim(), "Update");
                // strgeneratingQuotNo =strBranchCode.ToString().Trim() + "/" + intAutoNoInc + "/" + strCurrYear.ToString().Trim();
                string maxid = intAutoNoInc.ToString().Trim();
                if (maxid.Length == 2)
                {

                    strgeneratingQuotNo = "Q/BMCL/" + stryear + "/0" + intAutoNoInc;
                }
                else if (maxid.Length == 1)
                {
                    strgeneratingQuotNo = "Q/BMCL/" + stryear + "/00" + intAutoNoInc;
                }
                else
                {
                    strgeneratingQuotNo = "Q/BMCL/" + stryear + "/" + intAutoNoInc;
                }
            }
            else
            {
                SaveAutoQuotationTable(Session["sesBranchID"].ToString().Trim(), "1", strCurrYear.ToString().Trim(), strLastAutoNo.ToString().Trim(), "Insert");
                // strgeneratingQuotNo = strBranchCode.ToString().Trim() + "/" + "1" + "/" + strCurrYear.ToString().Trim();
                strgeneratingQuotNo = "Q/BMCL/" + stryear + "/00" + 1;
            }

        }

        return strgeneratingQuotNo;
    }

    protected void SaveAutoQuotationTable(string strBranchId, string strAutoNo, string strYear, string strLastAutoNo, string saveFlag)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        int flagMrvAuto = BusinessTier.saveQuotationAuto(conn, strBranchId.ToString().Trim(), strAutoNo.ToString().Trim(), strYear.ToString().Trim(), strLastAutoNo.Trim(), saveFlag.ToString().Trim());
        BusinessTier.DisposeConnection(conn);
    }

    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {

        if (!e.IsFromDetailTable)
        {

            // RadGrid1.DataSource = GetDataTable("SELECT * FROM Vw_Enquiry where deleted=0 order by Enquiry_ID Asc");


        }

    }

    //*******************************//RadGridQuot//*******************************//

    protected void RadGridQuot_ItemDataBound(object sender, GridItemEventArgs e)
    {

        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {

            GridEditFormItem editedItem = e.Item as GridEditFormItem;
            Label lbl = (Label)editedItem.FindControl("lblQuotationtransId");
            RadComboBox cboEquipmentId = (RadComboBox)editedItem.FindControl("cboEquipmentId");
            RadComboBox cboCalibration = (RadComboBox)editedItem.FindControl("cboCalibration");
            RadNumericTextBox txttotalprice = (RadNumericTextBox)editedItem.FindControl("txttotalprice");
            if (!(string.IsNullOrEmpty(lbl.Text.ToString())))
            {

                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                string sql = "select Equipment_ID,Equipment_Name,Equipment_no,calib_type,sum(price * Qty) as price FROM vw_Quotation WHERE Deleted = 0 and Quotation_Trans_ID = '" + lbl.Text.ToString() + "' group by Equipment_ID,Equipment_Name,Equipment_no,calib_type ";
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    cboEquipmentId.Text = reader["Equipment_Name"].ToString();
                    cboEquipmentId.SelectedValue = reader["Equipment_ID"].ToString();
                    cboCalibration.SelectedItem.Text = reader["calib_type"].ToString();
                    txttotalprice.Value = Convert.ToDouble(reader["price"].ToString());


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
                ShowMessage(70);
                if (!(string.IsNullOrEmpty(cboEnquiryId.Text.ToString().Trim())))
                {
                    //RadGrid1.DataSource = DataSourceHelper(cboEnquiryId.SelectedValue.ToString().Trim());
                    //RadGrid1.Rebind();
                }
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

    protected void RadGridQuot_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            GridEditableItem editedItem = e.Item as GridEditableItem;
            RadComboBox cboEquipmentId = (RadComboBox)editedItem.FindControl("cboEquipmentId");
            TextBox txtDesc = (TextBox)editedItem.FindControl("txtDesc");
            TextBox txtremarksdetails = (TextBox)editedItem.FindControl("txtremarksdetails");
            RadNumericTextBox txtQty = (RadNumericTextBox)editedItem.FindControl("txtQty");
            RadNumericTextBox txtPrice = (RadNumericTextBox)editedItem.FindControl("txtPrice");
            RadNumericTextBox txtDiscount = (RadNumericTextBox)editedItem.FindControl("txtDiscount");
            RadComboBox cboCalibration = (RadComboBox)editedItem.FindControl("cboCalibration");
            RadNumericTextBox lblprice = (RadNumericTextBox)editedItem.FindControl("lblprice");

            RadNumericTextBox txtadditional = (RadNumericTextBox)editedItem.FindControl("txtadditional");
            RadNumericTextBox lbladdnlprice = (RadNumericTextBox)editedItem.FindControl("lbladdnlprice");


            double intprice = 0;
            int intqty = Convert.ToInt32(txtQty.Text);
            int intEquipmentid = Convert.ToInt32(cboEquipmentId.SelectedValue);
            //if (string.IsNullOrEmpty(cboEnquiryId.SelectedValue.ToString().Trim()))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert(' Please Select the Enquiry No');", true);
            //    e.Canceled = true;
            //    return;

            //}

            if (RadGridQuot.Items.Count.ToString() == "0")
            {


                if (RadGridQuot.Items.Count.ToString() == "0" && string.IsNullOrEmpty(txtQuotationNo.Text.ToString()))
                {

                    if (rdoButton.SelectedItem.Text.ToString().Trim() == "Contract")
                    {
                        if (txtDiscount.Text != "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert(' You dont have permission to give Discount for Contract Equipments');", true);
                            txtDiscount.Value = 0;
                            e.Canceled = true;
                            return;
                        }
                        if (lblprice.Text != txtPrice.Text)
                        {
                            txtPrice.Value = lblprice.Value;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert(' You dont have permission to give Discount for Contract Equipments');", true);

                            e.Canceled = true;
                            return;

                        }
                        if ((cboContractNo.Text.ToString().Trim() == "CONTRACT NO") || (cboContractNo.Text.ToString().Trim() == "NO CONTRACT") || (string.IsNullOrEmpty(cboContractNo.SelectedValue.ToString().Trim())))
                        {
                            ShowMessage(86);
                            e.Canceled = true;
                            return;
                        }

                        DateTime dttime = DateTime.Now;
                        string sql = "", sql1 = "";
                        int intinterval = 0, totqty = 0;
                        DateTime cntrctdate = DateTime.Now, expirydate = DateTime.Now;

                        sql1 = "Select Contract_Price as price,Contract_Date,Expiry_Date,interval  from Vw_ContractEquipment where Equipment_Id='" + intEquipmentid + "' and customer_ID='" + cboCustomerId.SelectedValue + "' and contract_No='" + cboContractNo.Text.ToString().Trim() + "' and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 ";
                        SqlCommand command = new SqlCommand(sql1, conn);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            intprice = Convert.ToDouble(reader["price"].ToString());
                            cntrctdate = Convert.ToDateTime(reader["Contract_Date"].ToString());
                            expirydate = Convert.ToDateTime(reader["Expiry_Date"].ToString());
                            intinterval = Convert.ToInt32(reader["interval"].ToString());

                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('No Contract for this Equipment ID  " + intEquipmentid + " ');", true);
                            //e.Canceled = true;
                            //return;

                        }

                        BusinessTier.DisposeReader(reader);
                        String strcntrctdt = null, strexprydt = null;
                        // std2 = System.DateTime.Parse(std);
                        strcntrctdt = cntrctdate.Month + "/" + cntrctdate.Day + "/" + cntrctdate.Year + " 12:00:00 AM";
                        strexprydt = expirydate.Month + "/" + expirydate.Day + "/" + expirydate.Year + " 12:00:00 PM";
                        string sql2 = "select sum(qty) as qty from vw_Quotation where Equipment_Id='" + intEquipmentid + "' and customer_ID='" + cboCustomerId.SelectedValue + "' and contract_No='" + cboContractNo.Text.ToString().Trim() + "' and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and contract_Date >='" + strcntrctdt + "' and Expiry_Date<='" + strexprydt + "' and QD_Deleted=0";
                        SqlCommand command1 = new SqlCommand(sql2, conn);
                        SqlDataReader reader1 = command1.ExecuteReader();
                        if (reader1.Read())
                        {
                            if (string.IsNullOrEmpty(reader1["qty"].ToString()))
                            {
                                totqty = 0;
                            }
                            else
                            {
                                totqty = Convert.ToInt32(reader1["qty"].ToString());
                            }
                        }
                        BusinessTier.DisposeReader(reader1);

                        int diffqty = (intinterval - totqty);
                        if (diffqty < intqty)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('No Contract for this Equipment ID  " + intEquipmentid + " . Please check the Interval');", true);
                            e.Canceled = true;
                            return;

                        }

                    }

                    else
                    {

                        cboContractNo.SelectedValue = "1";


                    }

                    if ((cboContactName.SelectedValue.ToString().Trim() == "0") || (string.IsNullOrEmpty(cboContactName.SelectedValue.ToString().Trim())))
                    {
                        ShowMessage(87);

                        e.Canceled = true;
                        return;
                    }
                    generateQuotationNo("InsertNew");
                    // txtQuotationNo1.Text = txtQuotationNo.Text;

                }
            }
            else
            {
                if (rdoButton.SelectedItem.Text.ToString().Trim() == "Contract")
                {
                    if ((cboContractNo.Text.ToString().Trim() == "CONTRACT NO") || (cboContractNo.Text.ToString().Trim() == "NO CONTRACT") || (string.IsNullOrEmpty(cboContractNo.SelectedValue.ToString().Trim())))
                    {
                        ShowMessage(86);
                        e.Canceled = true;
                        return;
                    }
                    if (txtDiscount.Text != "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert(' You dont have permission to give Discount for Contract Equipments');", true);
                        txtDiscount.Value = 0;
                        e.Canceled = true;

                        return;
                    }
                    if (lblprice.Text != txtPrice.Text)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert(' You dont have permission to give Discount for Contract Equipments');", true);
                        txtPrice.Value = lblprice.Value;
                        e.Canceled = true;

                        return;

                    }

                    //SqlConnection conn = BusinessTier.getConnection();
                    //conn.Open();
                    DateTime dttime = DateTime.Now;
                    string sql = "", sql1 = "";

                    int intinterval = 0, totqty = 0;
                    DateTime cntrctdate = DateTime.Now, expirydate = DateTime.Now;

                    sql1 = "Select Contract_Price as price,Contract_Date,Expiry_Date,interval  from Vw_ContractEquipment where Equipment_Id='" + intEquipmentid + "' and customer_ID='" + cboCustomerId.SelectedValue + "' and contract_No='" + cboContractNo.Text.ToString().Trim() + "' and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 ";
                    SqlCommand command = new SqlCommand(sql1, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        intprice = Convert.ToDouble(reader["price"].ToString());
                        cntrctdate = Convert.ToDateTime(reader["Contract_Date"].ToString());
                        expirydate = Convert.ToDateTime(reader["Expiry_Date"].ToString());
                        intinterval = Convert.ToInt32(reader["interval"].ToString());

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('No Contract for this Equipment ID  " + intEquipmentid + " ');", true);
                        e.Canceled = true;
                        return;

                    }

                    BusinessTier.DisposeReader(reader);

                    String strcntrctdt = null, strexprydt = null;
                    // std2 = System.DateTime.Parse(std);
                    strcntrctdt = cntrctdate.Month + "/" + cntrctdate.Day + "/" + cntrctdate.Year + " 12:00:00 AM";
                    strexprydt = expirydate.Month + "/" + expirydate.Day + "/" + expirydate.Year + " 12:00:00 PM";
                    string sql2 = "select sum(qty) as qty from vw_Quotation where Equipment_Id='" + intEquipmentid + "' and customer_ID='" + cboCustomerId.SelectedValue + "' and contract_No='" + cboContractNo.Text.ToString().Trim() + "' and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and contract_Date >='" + strcntrctdt + "' and Expiry_Date<='" + strexprydt + "' and QD_Deleted=0";
                    SqlCommand command1 = new SqlCommand(sql2, conn);
                    SqlDataReader reader1 = command1.ExecuteReader();
                    if (reader1.Read())
                    {
                        if (string.IsNullOrEmpty(reader1["qty"].ToString()))
                        {
                            totqty = 0;
                        }
                        else
                        {
                            totqty = Convert.ToInt32(reader1["qty"].ToString());
                        }
                    }
                    BusinessTier.DisposeReader(reader1);

                    int diffqty = (intinterval - totqty);
                    if (diffqty < intqty)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('No Contract for this Equipment ID  " + intEquipmentid + " . Please check the Interval');", true);
                        e.Canceled = true;
                        return;

                    }
                    // BusinessTier.DisposeConnection(conn);
                }

                else
                {
                    cboContractNo.SelectedValue = "1";
                }

                if ((cboContactName.SelectedValue.ToString().Trim() == "0") || (string.IsNullOrEmpty(cboContactName.SelectedValue.ToString().Trim())))
                {
                    ShowMessage(87);

                    e.Canceled = true;
                    return;
                }

                if ((lblQuatmasterid.Text.ToString().Trim() == "0") || (string.IsNullOrEmpty(lblQuatmasterid.Text.ToString().Trim())))
                {
                    ShowMessage(24);
                    e.Canceled = true;
                    return;
                }
            }



            if ((lblQuatmasterid.Text.ToString().Trim() == "0") || (string.IsNullOrEmpty(lblQuatmasterid.Text.ToString().Trim())))
            {
                ShowMessage(24);
                e.Canceled = true;
                return;
            }
            if ((txtadditional.Text.ToString().Trim() == "0") || (string.IsNullOrEmpty(txtadditional.Text.ToString().Trim())))
            {
                txtadditional.Text = "0";
            }
            if (cboCalibration.Text.ToString().Trim() == "--Select--")
            {
                cboCalibration.Text = "Inhouse";
                txtremarksdetails.Text = "1 to 2 Weeks";

            }
            //   conn.Open();
            int aprvfalg = 0;
            if (Convert.ToDecimal(txtDiscount.Text.ToString().Trim()) > 10)
            {
                int intgetQuotMasterId = BusinessTier.Quotation_Discount(conn, Convert.ToInt32(lblQuatmasterid.Text), "DISCOUNT", Convert.ToDecimal(txtDiscount.Text), txtRemarks.Text, Session["sesUserID"].ToString().Trim(), "M");
                aprvfalg = 1;
            }

            int flg = BusinessTier.SaveInsertQuot_Detail(conn, 1, Convert.ToInt32(lblQuatmasterid.Text), txtremarksdetails.Text, Convert.ToInt32(cboEquipmentId.SelectedValue), Convert.ToInt32(txtQty.Text), Convert.ToDecimal(txtPrice.Text), Convert.ToInt32(txtadditional.Text), Convert.ToDecimal(txtDiscount.Text), cboCalibration.Text.ToString().Trim(), txtDesc.Text.ToString().Trim(), aprvfalg, 0, Session["sesUserID"].ToString(), "N");


            // BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(69);
            }
            // InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation_Detial", "Insert", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation_Detial", "Insert", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

        RadGridQuot.DataSource = DataSourceHelper("T", "0");
        RadGridQuot.Rebind();

    }

    protected void RadGridQuot_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Quotation_trans_Id"].ToString();
            // Label lblExistingname = (Label)editedItem.FindControl("lblExistingname");
            RadComboBox cboEquipmentId = (RadComboBox)editedItem.FindControl("cboEquipmentId");
            TextBox txtDesc = (TextBox)editedItem.FindControl("txtDesc");
            TextBox txtremarksdetails = (TextBox)editedItem.FindControl("txtremarksdetails");
            RadNumericTextBox txtQty = (RadNumericTextBox)editedItem.FindControl("txtQty");
            RadNumericTextBox txtPrice = (RadNumericTextBox)editedItem.FindControl("txtPrice");
            RadNumericTextBox txtDiscount = (RadNumericTextBox)editedItem.FindControl("txtDiscount");
            RadComboBox cboCalibration = (RadComboBox)editedItem.FindControl("cboCalibration");
            RadNumericTextBox lblprice = (RadNumericTextBox)editedItem.FindControl("lblprice");


            RadNumericTextBox txtadditional = (RadNumericTextBox)editedItem.FindControl("txtadditional");
            RadNumericTextBox lbladdnlprice = (RadNumericTextBox)editedItem.FindControl("lbladdnlprice");



            int intqty = Convert.ToInt32(txtQty.Text);
            int intEquipmentid = Convert.ToInt32(cboEquipmentId.SelectedValue);
            //   e.Item.Selected = true;
            // string strDetailId = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Enquiry_Detail_ID"]).ToString();


            double intprice = 0;
            if (rdoButton.SelectedItem.Text.ToString().Trim() == "Contract")
            {
                if (txtDiscount.Text != "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert(' You dont have permission to give Discount for Contract Equipments');", true);
                    e.Canceled = true;
                    txtDiscount.Value = 0;
                    return;

                }
                if (lblprice.Text != txtPrice.Text)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert(' You dont have permission to give Discount for Contract Equipments');", true);
                    e.Canceled = true;
                    txtPrice.Value = lblprice.Value;
                    return;

                }

                if ((cboContractNo.Text.ToString().Trim() == "CONTRACT NO") || (cboContractNo.Text.ToString().Trim() == "NO CONTRACT") || (string.IsNullOrEmpty(cboContractNo.SelectedValue.ToString().Trim())))
                {
                    ShowMessage(86);
                    e.Canceled = true;
                    return;
                }

                DateTime dttime = DateTime.Now;
                string sql = "", sql1 = "";
                int intinterval = 0, totqty = 0;
                DateTime cntrctdate = DateTime.Now, expirydate = DateTime.Now;

                sql1 = "Select Contract_Price as price,Contract_Date,Expiry_Date,interval  from Vw_ContractEquipment where Equipment_Id='" + cboEquipmentId.SelectedValue + "' and customer_ID='" + cboCustomerId.SelectedValue + "' and contract_No='" + cboContractNo.Text.ToString().Trim() + "' and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 ";
                SqlCommand command = new SqlCommand(sql1, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    intprice = Convert.ToDouble(reader["price"].ToString());
                    cntrctdate = Convert.ToDateTime(reader["Contract_Date"].ToString());
                    expirydate = Convert.ToDateTime(reader["Expiry_Date"].ToString());
                    intinterval = Convert.ToInt32(reader["interval"].ToString());

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('No Contract for this Equipment ID  " + intEquipmentid + " ');", true);
                    e.Canceled = true;
                    return;

                }

                BusinessTier.DisposeReader(reader);
                String strcntrctdt = null, strexprydt = null;
                // std2 = System.DateTime.Parse(std);
                strcntrctdt = cntrctdate.Month + "/" + cntrctdate.Day + "/" + cntrctdate.Year + " 12:00:00 AM";
                strexprydt = expirydate.Month + "/" + expirydate.Day + "/" + expirydate.Year + " 12:00:00 PM";
                string sql2 = "select sum(qty) as qty from vw_Quotation where Equipment_Id='" + cboEquipmentId.SelectedValue + "' and customer_ID='" + cboCustomerId.SelectedValue + "' and contract_No='" + cboContractNo.Text.ToString().Trim() + "' and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and contract_Date >='" + strcntrctdt + "' and Expiry_Date<='" + strexprydt + "' and QD_Deleted=0 and Quotation_Trans_ID<>'" + ID + "'";
                SqlCommand command1 = new SqlCommand(sql2, conn);
                SqlDataReader reader1 = command1.ExecuteReader();
                if (reader1.Read())
                {
                    if (string.IsNullOrEmpty(reader1["qty"].ToString()))
                    {
                        totqty = 0;
                    }
                    else
                    {
                        totqty = Convert.ToInt32(reader1["qty"].ToString());
                    }
                }
                BusinessTier.DisposeReader(reader1);

                int diffqty = (intinterval - totqty);
                if (diffqty < intqty)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('No Contract for this Equipment ID  " + intEquipmentid + " . Please check the Interval');", true);
                    e.Canceled = true;
                    return;

                }

            }

            else
            {

                cboContractNo.SelectedValue = "1";


            }

            if ((cboContactName.SelectedValue.ToString().Trim() == "0") || (string.IsNullOrEmpty(cboContactName.SelectedValue.ToString().Trim())))
            {
                ShowMessage(87);

                e.Canceled = true;
                return;
            }


            if ((lblQuatmasterid.Text.ToString().Trim() == "0") || (string.IsNullOrEmpty(lblQuatmasterid.Text.ToString().Trim())))
            {
                ShowMessage(24);
                e.Canceled = true;
                return;
            }

            int aprvfalg = 0;
            if (Convert.ToDecimal(txtDiscount.Text.ToString().Trim()) > 10)
            {
                int intgetQuotMasterId = BusinessTier.Quotation_Discount(conn, Convert.ToInt32(lblQuatmasterid.Text), "DISCOUNT", Convert.ToDecimal(txtDiscount.Text), txtRemarks.Text, Session["sesUserID"].ToString().Trim(), "M");

                aprvfalg = 1;
            }

            if ((txtadditional.Text.ToString().Trim() == "0") || (string.IsNullOrEmpty(txtadditional.Text.ToString().Trim())))
            {
                txtadditional.Text = "0";
            }
            if (cboCalibration.Text.ToString().Trim() == "--Select--")
            {
                cboCalibration.Text = "Inhouse";
                txtremarksdetails.Text = "1 to 2 Weeks";

            }

            int flg = BusinessTier.SaveInsertQuot_Detail(conn, Convert.ToInt32(ID.ToString()), Convert.ToInt32(lblQuatmasterid.Text), txtremarksdetails.Text, Convert.ToInt32(cboEquipmentId.SelectedValue), Convert.ToInt32(txtQty.Text), Convert.ToDecimal(txtPrice.Text), Convert.ToInt32(txtadditional.Text), Convert.ToDecimal(txtDiscount.Text), cboCalibration.Text.ToString().Trim(), txtDesc.Text.ToString().Trim(), aprvfalg, 1, Session["sesUserID"].ToString(), "U");

            if (flg >= 1)
            {
                ShowMessage(71);
            }
            // InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation_Detial", "Update", "Success", "Log");
        }



        catch (Exception ex)
        {
            ShowMessage(5);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation_Detial", "Update", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

        RadGridQuot.DataSource = DataSourceHelper("T", "0");
        RadGridQuot.Rebind();

    }

    //Quatation Grid///////////////////////////////////////////////////////////////////////////

    protected void RadGridQuot_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGridQuot.DataSource = DataSourceHelper("T", "0");
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quoatation", "RadGridQuot_NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper(string typeMasterDetail, string strMasterId)
    {
        //   TabContainer1.ActiveTab = TabContainer1.Tabs[1];
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "";
        if ((lblQuatmasterid.Text.ToString().Trim() == "0") || (string.IsNullOrEmpty(lblQuatmasterid.Text.ToString().Trim())))
        {
            lblQuatmasterid.Text = "0";
        }
        if (typeMasterDetail == "M")
        {
            sql = "select * FROM Vw_QuotationDetail_Equipment WHERE Deleted=0 order by Created_Date desc";
        }

        else if (typeMasterDetail == "T")
        {
            sql = "select * FROM Vw_QuotationDetail_Equipment WHERE Deleted=0 and Quotation_Id='" + lblQuatmasterid.Text + "'  order by Created_Date desc";

        }
        else if (typeMasterDetail == "F")
        {
            sql = "select * FROM Vw_QuotationDetail_Equipment WHERE Deleted=0 ";

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

    //////////////////UPLOAD////////////////////////////////////////////////////////////////////////////

    private void ImporttoDatatable()
    {

        try
        {

            if (FlUploadcsv.HasFile)
            {

                string FileName = FlUploadcsv.FileName;

                string path = string.Concat(Server.MapPath("~/Document/" + FlUploadcsv.FileName));

                FlUploadcsv.PostedFile.SaveAs(path);

                OleDbConnection OleDbcon = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;");

                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", OleDbcon);

                OleDbDataAdapter objAdapter1 = new OleDbDataAdapter(cmd);

                ds = new DataSet();

                objAdapter1.Fill(ds);

                Dt = ds.Tables[0];

            }


        }

        catch (Exception ex)
        {



        }

    }

    private bool ValidateDate(string date)
    {

        try
        {

            string[] dateParts = date.Split('/');

            DateTime testDate = new DateTime(Convert.ToInt32(dateParts[2]), Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[0]));

            return true;

        }

        catch
        {

            return false;

        }

    }

    private void InsertData()
    {
        try
        {
            for (int i = 0; i < Dt.Rows.Count; i++)
            {

                DataRow row = Dt.Rows[i];

                int columnCount = Dt.Columns.Count;

                string[] columns = new string[columnCount];

                for (int j = 0; j < columnCount; j++)
                {

                    columns[j] = row[j].ToString();

                }
                double intprice = 0;
                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                DateTime dttime = DateTime.Now;
                string sql1 = "";

                if (rdoButton.SelectedItem.Text.ToString().Trim() == "Contract")
                {
                    sql1 = "Select Contract_Price as price from Vw_ContractEquipment where Equipment_Id='" + Convert.ToInt32(columns[0]) + "' and customer_ID='" + cboCustomerId.SelectedValue + "' and contract_No='" + cboContractNo.Text.ToString().Trim() + "' and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 ";

                }
                else
                {
                    sql1 = "Select fee as price from Master_Equipment where Equipment_Id='" + Convert.ToInt32(columns[0]) + "'  and deleted=0 ";
                }

                SqlCommand command = new SqlCommand(sql1, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    intprice = Convert.ToDouble(reader["price"].ToString());

                }
                else
                {
                    ShowMessage(78);
                    return;
                }
                BusinessTier.DisposeReader(reader);

                // DateTime dttime = DateTime.Now;
                string sql = "INSERT INTO Quotation_Details(Quotation_id,Equipment_ID,Qty,Price,Discount,Calib_type,Description,Created_By)";

                sql += "VALUES('" + Convert.ToInt32(lblQuatmasterid.Text.ToString().Trim()) + "','" + Convert.ToInt32(columns[0]) + "','" + Convert.ToInt32(columns[1]) + "','" + intprice + "','" + 0 + "','" + columns[2] + "','" + columns[3] + "','" + Convert.ToInt32(Session["sesUserID"].ToString()) + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            ShowMessage(73);
        }
        catch (Exception ex)
        {
            ShowMessage(88);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation_Upload", "Upload", ex.ToString(), "Audit");
        }


    }

    protected void btnIpload_Click(object sender, EventArgs e)
    {
        try
        {

            if ((cboContactName.SelectedValue.ToString().Trim() == "0") || (string.IsNullOrEmpty(cboContactName.SelectedValue.ToString().Trim())))
            {
                ShowMessage(87);

                // e.Canceled = true;
                return;
            }

            if (FlUploadcsv.HasFile)
            {

                ImporttoDatatable();

                //   CheckData();
                for (int i = 0; i < Dt.Rows.Count; i++)
                {

                    DataRow row = Dt.Rows[i];

                    int columnCount = Dt.Columns.Count;

                    string[] columns = new string[columnCount];

                    for (int j = 0; j < columnCount; j++)
                    {

                        columns[j] = row[j].ToString();

                        if (Dt.Rows[i][j].ToString() == "")
                        {
                            int RowNo = i + 2;
                            int colm = j + 1;

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('Please Enter the Value in Row " + RowNo + " column " + colm + "');", true);

                            return;
                        }

                    }
                    double intprice = 0;
                    SqlConnection conn = BusinessTier.getConnection();
                    conn.Open();
                    DateTime dttime = DateTime.Now;
                    string sql1 = "";
                    int ij = 0, intinterval = 0, totqty = 0;
                    DateTime cntrctdate = DateTime.Now, expirydate = DateTime.Now;

                    if (rdoButton.SelectedItem.Text.ToString().Trim() == "Contract")
                    {
                        if ((cboContractNo.SelectedValue.ToString().Trim() == "0") || (string.IsNullOrEmpty(cboContractNo.SelectedValue.ToString().Trim())))
                        {
                            ShowMessage(86);

                            // e.Canceled = true;
                            return;
                        }
                        sql1 = "Select Contract_Price as price,Contract_Date,Expiry_Date,interval  from Vw_ContractEquipment where Equipment_Id='" + Convert.ToInt32(columns[0]) + "' and customer_ID='" + cboCustomerId.SelectedValue + "' and contract_No='" + cboContractNo.Text.ToString().Trim() + "' and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 ";
                        ij = 2;

                    }
                    else
                    {
                        sql1 = "Select fee as price from Master_Equipment where Equipment_Id='" + Convert.ToInt32(columns[0]) + "'  and deleted=0 ";
                        ij = 1;
                    }

                    SqlCommand command = new SqlCommand(sql1, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        if (ij == 1)
                        {
                            intprice = Convert.ToDouble(reader["price"].ToString());
                        }
                        else if (ij == 2)
                        {
                            intprice = Convert.ToDouble(reader["price"].ToString());
                            cntrctdate = Convert.ToDateTime(reader["Contract_Date"].ToString());
                            expirydate = Convert.ToDateTime(reader["Expiry_Date"].ToString());
                            intinterval = Convert.ToInt32(reader["interval"].ToString());
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('No Contract For this Equipment ID " + columns[0] + "');", true);
                        // lblStatus.Text = "Please Check your Excel Template. Equipment No " + Convert.ToInt32(columns[1]);
                        // ShowMessage(78);
                        return;
                        // break;
                    }
                    BusinessTier.DisposeReader(reader);
                    if (ij == 2)
                    {
                        String strcntrctdt = null, strexprydt = null;
                        // std2 = System.DateTime.Parse(std);
                        strcntrctdt = cntrctdate.Month + "/" + cntrctdate.Day + "/" + cntrctdate.Year + " 12:00:00 AM";
                        strexprydt = expirydate.Month + "/" + expirydate.Day + "/" + expirydate.Year + " 12:00:00 PM";
                        string sql2 = "select sum(qty) as qty from vw_Quotation where Equipment_Id='" + Convert.ToInt32(columns[0]) + "' and customer_ID='" + cboCustomerId.SelectedValue + "' and contract_No='" + cboContractNo.Text.ToString().Trim() + "' and Branch_id='" + Session["sesBranchID"].ToString().Trim() + "' and contract_Date >='" + strcntrctdt + "' and Expiry_Date<='" + strexprydt + "' and QD_Deleted=0";
                        SqlCommand command1 = new SqlCommand(sql2, conn);
                        SqlDataReader reader1 = command1.ExecuteReader();
                        if (reader1.Read())
                        {
                            if (string.IsNullOrEmpty(reader1["qty"].ToString()))
                            {
                                totqty = 0;
                            }
                            else
                            {
                                totqty = Convert.ToInt32(reader1["qty"].ToString());
                            }
                        }
                        BusinessTier.DisposeReader(reader1);
                        int diffqty = (intinterval - totqty);
                        if (diffqty < Convert.ToInt32(columns[2]))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('No Contract for this Equipment ID  " + columns[0] + " . Please check the Interval');", true);
                            return;

                        }
                    }
                    ij = 0;


                }
                if (RadGridQuot.Items.Count.ToString() == "0")
                {

                    generateQuotationNo("InsertNew");
                }

                InsertData();
                RadGridQuot.DataSource = DataSourceHelper("T", "0");
                RadGridQuot.Rebind();
            }
            else
            {
                lblStatus.Text = "Please select the Excel File";
                return;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(88);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation_Upload", "uploadCheckData", ex.ToString(), "Audit");
        }
        //   BindGrid();

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
        string sql2 = "select Quot_Status from Quotation where Quotation_ID='" + lblQuatmasterid.Text.ToString().Trim() + "' and deleted=0";
        SqlCommand command1 = new SqlCommand(sql2, conn);
        SqlDataReader reader1 = command1.ExecuteReader();
        if (reader1.Read())
        {
            quoatStatus = reader1["Quot_Status"].ToString();
        }
        BusinessTier.DisposeReader(reader1);

        if (quoatStatus == "DISCOUNT")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('You Can not generate the Quotation.Because this quotation is Waiting for the Discount ( " + txtDiscountApprov.Text + " )% approval');", true);
            return;
        }
        else if (quoatStatus == "PENDING")
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Quotation.aspx?param1=" + lblQuatmasterid.Text.ToString().Trim() + "');", true);
        }


    }

    protected void btnnewEquipment_Click(object sender, EventArgs e)
    {
        //   ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.radopen( 'Enquiry_Equipment.aspx', null, 'height=500,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'Enquiry_Equipment.aspx', null, 'height=500,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
    }

    protected void btnnewjobcosting_Click(object sender, EventArgs e)
    {
        //        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'Quotation_JobCosting.aspx, null, 'height=500,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);


        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        int cntclbtype = 0;
        string sql2 = "select count (CALIB_TYPE) as Calib_type from Quotation_Details where Quotation_ID='" + lblQuatmasterid.Text.ToString().Trim() + "' and deleted=0 and calib_type='Onsite'";
        SqlCommand command1 = new SqlCommand(sql2, conn);
        SqlDataReader reader1 = command1.ExecuteReader();
        if (reader1.Read())
        {
            cntclbtype = Convert.ToInt32(reader1["Calib_type"].ToString());
        }
        BusinessTier.DisposeReader(reader1);

        if (cntclbtype == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('You can not do the Jobcosting.');", true);
            return;
        }
        else if (cntclbtype >= 1)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open('Quotation_JobCosting.aspx?Quotation_ID=" + lblQuatmasterid.Text.ToString().Trim() + "');", true);
        }


    }

    protected void btnDiscount_Click(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        try
        {
            int ApprvFlag = 0;
            if (rdoButton.SelectedItem.Text.ToString().Trim() == "Contract")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert(' You dont have permission to give Discount for Contract Equipments');", true);
                return;
            }

            int k = 0;
            if (string.IsNullOrEmpty(txtDiscountApprov.Text.ToString().Trim()) || (txtDiscountApprov.Text.ToString().Trim() == "") || (Convert.ToDecimal(txtDiscountApprov.Text.ToString().Trim()) <= 0) || (Convert.ToDecimal(txtDiscountApprov.Text.ToString().Trim()) >= 100))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('Please keyin the Correct Discount Value');", true);
                return;
            }
            else if (Convert.ToDecimal(txtDiscountApprov.Text.ToString().Trim()) > 10)
            {
                ApprvFlag = 1;

                k = 1;
            }
            else if (Convert.ToDecimal(txtDiscountApprov.Text.ToString().Trim()) <= 10)
            {
                ApprvFlag = 0;
                k = 2;
            }

            if (k == 1)
            {

                int Quotdiscount = BusinessTier.Quotation_Discount(conn, Convert.ToInt32(lblQuatmasterid.Text), "DISCOUNT", Convert.ToDecimal(txtDiscountApprov.Text), txtRemarks.Text, Session["sesUserID"].ToString().Trim(), "M");
                //  BusinessTier.DisposeConnection(conn);
            }
            else if (k == 2)
            {
                int Quotdiscount = BusinessTier.Quotation_Discount(conn, Convert.ToInt32(lblQuatmasterid.Text), "PENDING", Convert.ToDecimal(txtDiscountApprov.Text), txtRemarks.Text, Session["sesUserID"].ToString().Trim(), "M");
            }

            foreach (GridDataItem item in RadGridQuot.Items)
            {

                string ID = item.GetDataKeyValue("Quotation_trans_Id").ToString();


                double dbldiscnt = 0;
                double dcmlprice = 0;
                double dcmladntlprc = 0, dcmladdnlrange = 0;
                string sql = "select fee,Additional_Price FROM master_Equipment WHERE Deleted = 0 and Equipment_ID=(select Equipment_ID FROM Quotation_Details WHERE Deleted = 0  and Quotation_Trans_ID = '" + ID.ToString() + "')";
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dcmlprice = Convert.ToDouble(reader["fee"].ToString());
                    dcmladntlprc = Convert.ToDouble(reader["Additional_Price"].ToString());
                }
                BusinessTier.DisposeReader(reader);

                string sql2 = "select ADDITIONAL_RANGE FROM Quotation_Details WHERE Deleted = 0  and Quotation_Trans_ID = '" + ID.ToString() + "'";
                SqlCommand command2 = new SqlCommand(sql2, conn);
                SqlDataReader reader2 = command2.ExecuteReader();
                if (reader2.Read())
                {
                    dcmladdnlrange = Convert.ToDouble(reader2["ADDITIONAL_RANGE"].ToString());
                }
                BusinessTier.DisposeReader(reader2);

                if ((txtDiscountApprov.Text.ToString().Trim() == "0") || (string.IsNullOrEmpty(txtDiscountApprov.Text.ToString().Trim())))
                {
                    ShowMessage(24);
                    return;
                }

                dcmlprice = (dcmladdnlrange * dcmladntlprc) + dcmlprice;
                dbldiscnt = Convert.ToDouble(dcmlprice) * Convert.ToDouble(txtDiscountApprov.Value) / 100;
                dbldiscnt = Math.Round(dbldiscnt, 1, MidpointRounding.AwayFromZero);
                dcmlprice = dcmlprice - dbldiscnt;
                // conn.Open();

                int flg = BusinessTier.SaveInsertQuot_Detail(conn, Convert.ToInt32(ID.ToString()), Convert.ToInt32(lblQuatmasterid.Text), "", 1, 1, Convert.ToDecimal(dcmlprice), 1, Convert.ToDecimal(txtDiscountApprov.Text), "", "", ApprvFlag, Convert.ToInt32(cboEnquiryId.SelectedValue), Session["sesUserID"].ToString(), "A");

                if (flg >= 1)
                {
                    ShowMessage(84);
                }
                // InsertLogAuditTrial is used to insert the action into MasterLog table
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation_Detial_Discount", "Dicount", "Success", "Log");
            }
            // BusinessTier.DisposeConnection(conn);
        }



        catch (Exception ex)
        {
            ShowMessage(5);
            // e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Quotation_Detial", "Update", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

        RadGridQuot.DataSource = DataSourceHelper("T", "0");
        RadGridQuot.Rebind();



    }

    protected void linkAddNew_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery, true);
    }

}

