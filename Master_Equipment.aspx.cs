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

public partial class Master_Equipment : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

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
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
        lblname.Text = "Hi, " + Session["Name"].ToString();
    }

    protected void cboCategoryId_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select Category_ID,Category_Name,remarks from Master_Category where DELETED=0  and [Category_Name] LIKE @text + '%' order by Category_Name";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Category_Name"].ToString();
                item.Value = row["Category_ID"].ToString();
                item.Attributes.Add("Category_Name", row["Category_Name"].ToString());
                item.Attributes.Add("remarks", row["remarks"].ToString());
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

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            //Get the instance of the right type
            GridDataItem dataBoundItem = e.Item as GridDataItem;

            //Check the formatting condition
            if (Convert.ToDecimal(dataBoundItem["Flag_price"].Text.ToString().Trim()) == 1)
            {
                dataBoundItem.BackColor = Color.Red;
                dataBoundItem.Font.Bold = true;
                // dataBoundItem["Discount"].ForeColor = Color.Red;
                // dataBoundItem["Discount"].Font.Bold = true;
                //Customize more...
            }
        }
        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem) // insert item
            {
                GridEditFormInsertItem editedItem1 = e.Item as GridEditFormInsertItem;

                Label lbllab1 = (Label)editedItem1.FindControl("lbllab1");
                Label lbllab2 = (Label)editedItem1.FindControl("lbllab2");
                Label lbllab3 = (Label)editedItem1.FindControl("lbllab3");
                Label lbllab4 = (Label)editedItem1.FindControl("lbllab4");
                Label lbllab5 = (Label)editedItem1.FindControl("lbllab5");
                Label lbllab6 = (Label)editedItem1.FindControl("lbllab6");
                Label lbllab7 = (Label)editedItem1.FindControl("lbllab7");
                Label lbllab8 = (Label)editedItem1.FindControl("lbllab8");
                Label lbllab9 = (Label)editedItem1.FindControl("lbllab9");
                Label lbllab10 = (Label)editedItem1.FindControl("lbllab10");

                CheckBox ChkLab1 = (CheckBox)editedItem1.FindControl("ChkLab1");
                CheckBox ChkLab2 = (CheckBox)editedItem1.FindControl("ChkLab2");
                CheckBox ChkLab3 = (CheckBox)editedItem1.FindControl("ChkLab3");
                CheckBox ChkLab4 = (CheckBox)editedItem1.FindControl("ChkLab4");
                CheckBox ChkLab5 = (CheckBox)editedItem1.FindControl("ChkLab5");
                CheckBox ChkLab6 = (CheckBox)editedItem1.FindControl("ChkLab6");
                CheckBox ChkLab7 = (CheckBox)editedItem1.FindControl("ChkLab7");
                CheckBox ChkLab8 = (CheckBox)editedItem1.FindControl("ChkLab8");
                CheckBox ChkLab9 = (CheckBox)editedItem1.FindControl("ChkLab9");
                CheckBox ChkLab10 = (CheckBox)editedItem1.FindControl("ChkLab10");
                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                string sql = "select Dept_ID,Dept_Name,Dept_Code,Lab,Short_Name FROM Master_Department WHERE Deleted = 0 and lab =1 order by Dept_Code";
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                int i = 1;
                while (reader.Read())
                {

                    if (i == 1)
                    {
                        // lbllab1.Visible = true;
                        ChkLab1.Visible = true;
                        lbllab1.Text = reader["Dept_ID"].ToString();
                        ChkLab1.Text = reader["Short_Name"].ToString();

                    }
                    else if (i == 2)
                    {
                        // lbllab2.Visible = true;
                        ChkLab2.Visible = true;
                        lbllab2.Text = reader["Dept_ID"].ToString();
                        ChkLab2.Text = reader["Short_Name"].ToString();
                    }

                    else if (i == 3)
                    {
                        //  lbllab3.Visible = true;
                        ChkLab3.Visible = true;
                        lbllab3.Text = reader["Dept_ID"].ToString();
                        ChkLab3.Text = reader["Short_Name"].ToString();
                    }

                    else if (i == 4)
                    {
                        //lbllab4.Visible = true;
                        ChkLab4.Visible = true;
                        lbllab4.Text = reader["Dept_ID"].ToString();
                        ChkLab4.Text = reader["Short_Name"].ToString();
                    }
                    else if (i == 5)
                    {
                        //  lbllab5.Visible = true;
                        ChkLab5.Visible = true;
                        lbllab5.Text = reader["Dept_ID"].ToString();
                        ChkLab5.Text = reader["Short_Name"].ToString();
                    }

                    else if (i == 6)
                    {
                        //  lbllab6.Visible = true;
                        ChkLab6.Visible = true;
                        lbllab6.Text = reader["Dept_ID"].ToString();
                        ChkLab6.Text = reader["Short_Name"].ToString();
                    }


                    else if (i == 7)
                    {
                        //  lbllab7.Visible = true;
                        ChkLab7.Visible = true;
                        lbllab7.Text = reader["Dept_ID"].ToString();
                        ChkLab7.Text = reader["Short_Name"].ToString();
                    }

                    else if (i == 8)
                    {
                        //  lbllab8.Visible = true;
                        ChkLab8.Visible = true;
                        lbllab8.Text = reader["Dept_ID"].ToString();
                        ChkLab8.Text = reader["Short_Name"].ToString();
                    }
                    else if (i == 9)
                    {
                        // lbllab9.Visible = true;
                        ChkLab9.Visible = true;
                        lbllab9.Text = reader["Dept_ID"].ToString();
                        ChkLab9.Text = reader["Short_Name"].ToString();
                    }

                    else if (i == 10)
                    {
                        //   lbllab10.Visible = true;
                        ChkLab10.Visible = true;
                        lbllab10.Text = reader["Dept_ID"].ToString();
                        ChkLab10.Text = reader["Short_Name"].ToString();
                    }

                    i++;
                }
                BusinessTier.DisposeReader(reader);
                BusinessTier.DisposeConnection(conn);

            }
            else
            {
                // edit item


                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                Label lbl = (Label)editedItem.FindControl("lblEquipID");
                RadComboBox cboCategoryId = (RadComboBox)editedItem.FindControl("cboCategoryId");

                Label lbllab1 = (Label)editedItem.FindControl("lbllab1");
                Label lbllab2 = (Label)editedItem.FindControl("lbllab2");
                Label lbllab3 = (Label)editedItem.FindControl("lbllab3");
                Label lbllab4 = (Label)editedItem.FindControl("lbllab4");
                Label lbllab5 = (Label)editedItem.FindControl("lbllab5");
                Label lbllab6 = (Label)editedItem.FindControl("lbllab6");
                Label lbllab7 = (Label)editedItem.FindControl("lbllab7");
                Label lbllab8 = (Label)editedItem.FindControl("lbllab8");
                Label lbllab9 = (Label)editedItem.FindControl("lbllab9");
                Label lbllab10 = (Label)editedItem.FindControl("lbllab10");

                CheckBox ChkLab1 = (CheckBox)editedItem.FindControl("ChkLab1");
                CheckBox ChkLab2 = (CheckBox)editedItem.FindControl("ChkLab2");
                CheckBox ChkLab3 = (CheckBox)editedItem.FindControl("ChkLab3");
                CheckBox ChkLab4 = (CheckBox)editedItem.FindControl("ChkLab4");
                CheckBox ChkLab5 = (CheckBox)editedItem.FindControl("ChkLab5");
                CheckBox ChkLab6 = (CheckBox)editedItem.FindControl("ChkLab6");
                CheckBox ChkLab7 = (CheckBox)editedItem.FindControl("ChkLab7");
                CheckBox ChkLab8 = (CheckBox)editedItem.FindControl("ChkLab8");
                CheckBox ChkLab9 = (CheckBox)editedItem.FindControl("ChkLab9");
                CheckBox ChkLab10 = (CheckBox)editedItem.FindControl("ChkLab10");

                SqlConnection conn1 = BusinessTier.getConnection();
                conn1.Open();
                string sql1 = "select Dept_ID,Dept_Name,Dept_Code,Short_Name,Lab FROM Master_Department WHERE Deleted = 0 and lab =1 order by Dept_Code ";
                SqlCommand command1 = new SqlCommand(sql1, conn1);
                SqlDataReader reader1 = command1.ExecuteReader();
                int i = 1;
                while (reader1.Read())
                {

                    if (i == 1)
                    {
                       // lbllab1.Visible = true;
                        ChkLab1.Visible = true;
                        lbllab1.Text = reader1["Dept_ID"].ToString();
                        ChkLab1.Text = reader1["Short_Name"].ToString();
                        
                    }
                    else if (i == 2)
                    {
                       // lbllab2.Visible = true;
                        ChkLab2.Visible = true;
                        lbllab2.Text = reader1["Dept_ID"].ToString();
                        ChkLab2.Text = reader1["Short_Name"].ToString();
                    }

                    else if (i == 3)
                    {
                      //  lbllab3.Visible = true;
                        ChkLab3.Visible = true;
                        lbllab3.Text = reader1["Dept_ID"].ToString();
                        ChkLab3.Text = reader1["Short_Name"].ToString();
                    }

                    else if (i == 4)
                    {
                        //lbllab4.Visible = true;
                        ChkLab4.Visible = true;
                        lbllab4.Text = reader1["Dept_ID"].ToString();
                        ChkLab4.Text = reader1["Short_Name"].ToString();
                    }
                    else if (i == 5)
                    {
                      //  lbllab5.Visible = true;
                        ChkLab5.Visible = true;
                        lbllab5.Text = reader1["Dept_ID"].ToString();
                        ChkLab5.Text = reader1["Short_Name"].ToString();
                    }

                    else if (i == 6)
                    {
                      //  lbllab6.Visible = true;
                        ChkLab6.Visible = true;
                        lbllab6.Text = reader1["Dept_ID"].ToString();
                        ChkLab6.Text = reader1["Short_Name"].ToString();
                    }


                    else if (i == 7)
                    {
                      //  lbllab7.Visible = true;
                        ChkLab7.Visible = true;
                        lbllab7.Text = reader1["Dept_ID"].ToString();
                        ChkLab7.Text = reader1["Short_Name"].ToString();
                    }

                    else if (i == 8)
                    {
                      //  lbllab8.Visible = true;
                        ChkLab8.Visible = true;
                        lbllab8.Text = reader1["Dept_ID"].ToString();
                        ChkLab8.Text = reader1["Short_Name"].ToString();
                    }
                    else if (i == 9)
                    {
                       // lbllab9.Visible = true;
                        ChkLab9.Visible = true;
                        lbllab9.Text = reader1["Dept_ID"].ToString();
                        ChkLab9.Text = reader1["Short_Name"].ToString();
                    }

                    else if (i == 10)
                    {
                     //   lbllab10.Visible = true;
                        ChkLab10.Visible = true;
                        lbllab10.Text = reader1["Dept_ID"].ToString();
                        ChkLab10.Text = reader1["Short_Name"].ToString();
                    }

                    i++;
                }
                BusinessTier.DisposeReader(reader1);
                BusinessTier.DisposeConnection(conn1);

                int intlab1 = 0, intlab2 = 0, intlab3 = 0, intlab4 = 0, intlab5 = 0, intlab6 = 0, intlab7 = 0, intlab8 = 0, intlab9 = 0, intlab10 = 0;
                if (!(string.IsNullOrEmpty(lbl.Text.ToString())))
                {

                    SqlConnection conn = BusinessTier.getConnection();
                    conn.Open();
                    string sql = "select * FROM Vw_Equipment_Category WHERE Deleted = 0 and Equipment_ID = '" + lbl.Text.ToString() + "'";
                    SqlCommand command = new SqlCommand(sql, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        //cboCategoryId.Text = reader["Category_Name"].ToString();
                       // cboCategoryId.SelectedValue = reader["Category_ID"].ToString();
                        intlab1 = Convert.ToInt32(reader["Lab1"].ToString());
                        intlab2 = Convert.ToInt32(reader["Lab2"].ToString());
                        intlab3 = Convert.ToInt32(reader["Lab3"].ToString());
                        intlab4 = Convert.ToInt32(reader["Lab4"].ToString());
                        intlab5 = Convert.ToInt32(reader["Lab5"].ToString());
                        intlab6 = Convert.ToInt32(reader["Lab6"].ToString());
                        intlab7 = Convert.ToInt32(reader["Lab7"].ToString());
                        intlab8 = Convert.ToInt32(reader["Lab8"].ToString());
                        intlab9 = Convert.ToInt32(reader["Lab9"].ToString());
                        intlab10 = Convert.ToInt32(reader["Lab10"].ToString());

                    }
                    BusinessTier.DisposeReader(reader);
                    BusinessTier.DisposeConnection(conn);

                }
                if (intlab1 == 0) { ChkLab1.Checked = false; } else { ChkLab1.Checked = true; }
                if (intlab2 == 0) { ChkLab2.Checked = false; } else { ChkLab2.Checked = true; }
                if (intlab3 == 0) { ChkLab3.Checked = false; } else { ChkLab3.Checked = true; }
                if (intlab4 == 0) { ChkLab4.Checked = false; } else { ChkLab4.Checked = true; }
                if (intlab5 == 0) { ChkLab5.Checked = false; } else { ChkLab5.Checked = true; }
                if (intlab6 == 0) { ChkLab6.Checked = false; } else { ChkLab6.Checked = true; }
                if (intlab7 == 0) { ChkLab7.Checked = false; } else { ChkLab7.Checked = true; }
                if (intlab8 == 0) { ChkLab8.Checked = false; } else { ChkLab8.Checked = true; }
                if (intlab9 == 0) { ChkLab9.Checked = false; } else { ChkLab9.Checked = true; }
                if (intlab10 == 0) { ChkLab10.Checked = false; } else { ChkLab10.Checked = true; }


            }
        }

    }

    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGrid1.DataSource = DataSourceHelper();
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Equipment", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
      
        string sql = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        //if (Session["sesDepartmentType"].ToString().Trim() =="8")
        //{
        //    sql = "select * FROM Vw_Equipment_Lab WHERE Deleted=0 order by flag_Price Desc";
        //   // sql = " select v.*,d.dept_code FROM Vw_Equipment_Category v, Master_Department d WHERE v.Deleted=0 and v.lab1=d.Dept_id or v.lab2=d.Dept_id or v.lab3=d.Dept_id or v.lab4=d.Dept_id or v.lab5=d.Dept_id or v.lab6=d.Dept_id or v.lab7=d.Dept_id or v.lab8=d.Dept_id or v.lab9=d.Dept_id";
        //}
        //else
        //{


        //    sql = "select * FROM Vw_Equipment_Lab v WHERE v.Deleted=0  and (v.lab1='" + Session["sesDepartmentType"].ToString() + "' or v.lab2='" + Session["sesDepartmentType"].ToString() + "' or v.lab3='" + Session["sesDepartmentType"].ToString() + "' or v.lab4='" + Session["sesDepartmentType"].ToString() + "' or  v.lab5='" + Session["sesDepartmentType"].ToString() + "' or v.lab6='" + Session["sesDepartmentType"].ToString() + "' or v.lab7='" + Session["sesDepartmentType"].ToString() + "' or v.lab8='" + Session["sesDepartmentType"].ToString() + "' or v.lab9='" + Session["sesDepartmentType"].ToString() + "' or v.lab10='" + Session["sesDepartmentType"].ToString() + "')  order by v.flag_Price,v.Equipment_Name asc ";
        //}
        sql = "select * FROM Vw_Equipment_Lab WHERE Deleted=0 order by EQUIPMENT_NAME";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }
   
    protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EQUIPMENT_ID"].ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flg = BusinessTier.SaveEquipmentMaster(conn, Convert.ToInt32(ID), "", "", "", "", "", "", "", "", 1,1, "", 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1, Session["sesUserID"].ToString(), "D");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(48);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Equipment", "Delete", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(7);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Equipment", "Delete", ex.ToString(), "Audit");
        }
    }

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            conn.Open();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            TextBox txtEquipNo = (TextBox)editedItem.FindControl("txtEquipNo");
            TextBox txtEquipName = (TextBox)editedItem.FindControl("txtEquipName");
            TextBox txtMaker = (TextBox)editedItem.FindControl("txtMaker");
            TextBox txtModel = (TextBox)editedItem.FindControl("txtModel");
            TextBox txtCalprocno = (TextBox)editedItem.FindControl("txtCalprocno");
            TextBox txtMU = (TextBox)editedItem.FindControl("txtMU");
            TextBox txtRange = (TextBox)editedItem.FindControl("txtRange");
            TextBox txtClass = (TextBox)editedItem.FindControl("txtClass");
            RadNumericTextBox txtNumFee = (RadNumericTextBox)editedItem.FindControl("txtNumFee");
            TextBox txtRemarks = (TextBox)editedItem.FindControl("txtRemarks");
            //RadComboBox cboCategoryId = (RadComboBox)editedItem.FindControl("cboCategoryId");
            RadNumericTextBox txtAdditionalprc = (RadNumericTextBox)editedItem.FindControl("txtAdditionalprc");


            Label lbllab1 = (Label)editedItem.FindControl("lbllab1");
            Label lbllab2 = (Label)editedItem.FindControl("lbllab2");
            Label lbllab3 = (Label)editedItem.FindControl("lbllab3");
            Label lbllab4 = (Label)editedItem.FindControl("lbllab4");
            Label lbllab5 = (Label)editedItem.FindControl("lbllab5");
            Label lbllab6 = (Label)editedItem.FindControl("lbllab6");
            Label lbllab7 = (Label)editedItem.FindControl("lbllab7");
            Label lbllab8 = (Label)editedItem.FindControl("lbllab8");
            Label lbllab9 = (Label)editedItem.FindControl("lbllab9");
            Label lbllab10 = (Label)editedItem.FindControl("lbllab10");
            CheckBox ChkLab1 = (CheckBox)editedItem.FindControl("ChkLab1");
            CheckBox ChkLab2 = (CheckBox)editedItem.FindControl("ChkLab2");
            CheckBox ChkLab3 = (CheckBox)editedItem.FindControl("ChkLab3");
            CheckBox ChkLab4 = (CheckBox)editedItem.FindControl("ChkLab4");
            CheckBox ChkLab5 = (CheckBox)editedItem.FindControl("ChkLab5");
            CheckBox ChkLab6 = (CheckBox)editedItem.FindControl("ChkLab6");
            CheckBox ChkLab7 = (CheckBox)editedItem.FindControl("ChkLab7");
            CheckBox ChkLab8 = (CheckBox)editedItem.FindControl("ChkLab8");
            CheckBox ChkLab9 = (CheckBox)editedItem.FindControl("ChkLab9");
            CheckBox ChkLab10 = (CheckBox)editedItem.FindControl("ChkLab10");

            string strCheckflag = "0";
            SqlDataReader reader = BusinessTier.checkcode(conn, txtEquipName.Text.ToString().Trim(), "E");
            if (reader.Read())
            {
                strCheckflag = reader["checkdup"].ToString();

            }
            BusinessTier.DisposeReader(reader);

            if (strCheckflag.ToString() == "1")
            {
                ShowMessage(39);
                //lblStatus.Text = "Please key in the price";
                return;
            }

            int j = 0;
            if (ChkLab1.Checked != true) { lbllab1.Text = "0"; } else { j = 1; }
            if (ChkLab2.Checked != true) { lbllab2.Text = "0"; } else { j = 1; }
            if (ChkLab3.Checked != true) { lbllab3.Text = "0"; } else { j = 1; }
            if (ChkLab4.Checked != true) { lbllab4.Text = "0"; } else { j = 1; }
            if (ChkLab5.Checked != true) { lbllab5.Text = "0"; } else { j = 1; }
            if (ChkLab6.Checked != true) { lbllab6.Text = "0"; } else { j = 1; }
            if (ChkLab7.Checked != true) { lbllab7.Text = "0"; } else { j = 1; }
            if (ChkLab8.Checked != true) { lbllab8.Text = "0"; } else { j = 1; }
            if (ChkLab9.Checked != true) { lbllab9.Text = "0"; } else { j = 1; }
            if (ChkLab10.Checked != true) { lbllab10.Text = "0"; } else { j = 1; }

          

            if (string.IsNullOrEmpty(txtNumFee.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please key in the price";
                e.Canceled = true;
                return;

            }
            //if (string.IsNullOrEmpty(cboCategoryId.Text.ToString().Trim()))
            //{
            //    lblStatus.Text = "Please Select the Category";
            //    e.Canceled = true;
            //    return;

            //}
            if (j == 0)
            {
                lblStatus.Text = "Please Select the Lab";
                e.Canceled = true;
                return;
            }
            if (string.IsNullOrEmpty(txtAdditionalprc.Text.ToString().Trim()))
            {
                txtAdditionalprc.Text = "0";

            }
            //int intflagprc = 1;
            //if (Convert.ToInt32(txtNumFee.Text.ToString().Trim()) == 0)
            //{
            //    intflagprc = 0;

            //}
          
          

            int flg = BusinessTier.SaveEquipmentMaster(conn, 1, txtEquipNo.Text.ToString().Trim(), txtEquipName.Text.ToString().Trim(), txtMaker.Text.ToString().Trim(), txtModel.Text.ToString().Trim(), txtCalprocno.Text.ToString().Trim(), txtMU.Text.ToString().Trim(),
                 txtRange.Text.ToString().Trim(), txtClass.Text.ToString().Trim(), Convert.ToDouble(txtNumFee.Text.ToString().Trim()), 0, txtRemarks.Text.ToString().Trim(), 1, Convert.ToInt32(lbllab1.Text.ToString().Trim()), Convert.ToInt32(lbllab2.Text.ToString().Trim()), Convert.ToInt32(lbllab3.Text.ToString().Trim()), Convert.ToInt32(lbllab4.Text.ToString().Trim()), Convert.ToInt32(lbllab5.Text.ToString().Trim()), Convert.ToInt32(lbllab6.Text.ToString().Trim()), Convert.ToInt32(lbllab7.Text.ToString().Trim()), Convert.ToInt32(lbllab8.Text.ToString().Trim()), Convert.ToInt32(lbllab9.Text.ToString().Trim()), Convert.ToInt32(lbllab10.Text.ToString().Trim()), Convert.ToDouble(txtAdditionalprc.Text.ToString().Trim()), Session["sesUserID"].ToString(), "N");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(47);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Equipment", "Insert", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Equipment", "Insert", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

        RadGrid1.Rebind();
    }

    protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            conn.Open();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EQUIPMENT_ID"].ToString();
            Label lblExistingname = (Label)editedItem.FindControl("lblExistingname");
            TextBox txtEquipNo = (TextBox)editedItem.FindControl("txtEquipNo");
            TextBox txtEquipName = (TextBox)editedItem.FindControl("txtEquipName");
            TextBox txtMaker = (TextBox)editedItem.FindControl("txtMaker");
            TextBox txtModel = (TextBox)editedItem.FindControl("txtModel");
            TextBox txtCalprocno = (TextBox)editedItem.FindControl("txtCalprocno");
            TextBox txtMU = (TextBox)editedItem.FindControl("txtMU");
            TextBox txtRange = (TextBox)editedItem.FindControl("txtRange");
            TextBox txtClass = (TextBox)editedItem.FindControl("txtClass");
            RadNumericTextBox txtNumFee = (RadNumericTextBox)editedItem.FindControl("txtNumFee");
            TextBox txtRemarks = (TextBox)editedItem.FindControl("txtRemarks");
            //RadComboBox cboCategoryId = (RadComboBox)editedItem.FindControl("cboCategoryId");
            RadNumericTextBox txtAdditionalprc = (RadNumericTextBox)editedItem.FindControl("txtAdditionalprc");

            Label lbllab1 = (Label)editedItem.FindControl("lbllab1");
            Label lbllab2 = (Label)editedItem.FindControl("lbllab2");
            Label lbllab3 = (Label)editedItem.FindControl("lbllab3");
            Label lbllab4 = (Label)editedItem.FindControl("lbllab4");
            Label lbllab5 = (Label)editedItem.FindControl("lbllab5");
            Label lbllab6 = (Label)editedItem.FindControl("lbllab6");
            Label lbllab7 = (Label)editedItem.FindControl("lbllab7");
            Label lbllab8 = (Label)editedItem.FindControl("lbllab8");
            Label lbllab9 = (Label)editedItem.FindControl("lbllab9");
            Label lbllab10 = (Label)editedItem.FindControl("lbllab10");
            CheckBox ChkLab1 = (CheckBox)editedItem.FindControl("ChkLab1");
            CheckBox ChkLab2 = (CheckBox)editedItem.FindControl("ChkLab2");
            CheckBox ChkLab3 = (CheckBox)editedItem.FindControl("ChkLab3");
            CheckBox ChkLab4 = (CheckBox)editedItem.FindControl("ChkLab4");
            CheckBox ChkLab5 = (CheckBox)editedItem.FindControl("ChkLab5");
            CheckBox ChkLab6 = (CheckBox)editedItem.FindControl("ChkLab6");
            CheckBox ChkLab7 = (CheckBox)editedItem.FindControl("ChkLab7");
            CheckBox ChkLab8 = (CheckBox)editedItem.FindControl("ChkLab8");
            CheckBox ChkLab9 = (CheckBox)editedItem.FindControl("ChkLab9");
            CheckBox ChkLab10 = (CheckBox)editedItem.FindControl("ChkLab10");

            string strCheckflag = "0";
            SqlDataReader reader = BusinessTier.checkcode(conn, txtEquipNo.Text.ToString().Trim(), "E");
            if (reader.Read())
            {
                strCheckflag = reader["checkdup"].ToString();

            }
            BusinessTier.DisposeReader(reader);

            if (strCheckflag.ToString() == "1")
            {
                if (!(lblExistingname.Text.ToString().Trim() == txtEquipNo.Text.ToString().Trim()))
                {
                    ShowMessage(46);
                    return;
                }
            }
            int j = 0;
            if (ChkLab1.Checked != true) { lbllab1.Text = "0"; } else { j = 1; }
            if (ChkLab2.Checked != true) { lbllab2.Text = "0"; } else { j = 1; }
            if (ChkLab3.Checked != true) { lbllab3.Text = "0"; } else { j = 1; }
            if (ChkLab4.Checked != true) { lbllab4.Text = "0"; } else { j = 1; }
            if (ChkLab5.Checked != true) { lbllab5.Text = "0"; } else { j = 1; }
            if (ChkLab6.Checked != true) { lbllab6.Text = "0"; } else { j = 1; }
            if (ChkLab7.Checked != true) { lbllab7.Text = "0"; } else { j = 1; }
            if (ChkLab8.Checked != true) { lbllab8.Text = "0"; } else { j = 1; }
            if (ChkLab9.Checked != true) { lbllab9.Text = "0"; } else { j = 1; }
            if (ChkLab10.Checked != true) { lbllab10.Text = "0"; } else { j = 1; }

        
          
            if (string.IsNullOrEmpty(txtNumFee.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please key in the price";
                e.Canceled = true;
                return;

            }
            //if (string.IsNullOrEmpty(cboCategoryId.Text.ToString().Trim()))
            //{
            //    lblStatus.Text = "Please Select the Category";
            //    e.Canceled = true;
            //    return;

            //}
            if (j == 0)
            {
                lblStatus.Text = "Please Select the Lab";
                e.Canceled = true;
                return;
            }

            //int intflagprc = 1;
            //if (Convert.ToInt32(txtNumFee.Text.ToString().Trim()) == 0)
            //{
            //    intflagprc = 0;

            //}
            int flg = BusinessTier.SaveEquipmentMaster(conn, Convert.ToInt32(ID.ToString()), txtEquipNo.Text.ToString().Trim(), txtEquipName.Text.ToString().Trim(), txtMaker.Text.ToString().Trim(), txtModel.Text.ToString().Trim(), txtCalprocno.Text.ToString().Trim(), txtMU.Text.ToString().Trim(), txtRange.Text.ToString().Trim(), txtClass.Text.ToString().Trim(), Convert.ToDouble(txtNumFee.Text.ToString().Trim()), 0, txtRemarks.Text.ToString().Trim(), 1, Convert.ToInt32(lbllab1.Text.ToString().Trim()), Convert.ToInt32(lbllab2.Text.ToString().Trim()), Convert.ToInt32(lbllab3.Text.ToString().Trim()), Convert.ToInt32(lbllab4.Text.ToString().Trim()), Convert.ToInt32(lbllab5.Text.ToString().Trim()), Convert.ToInt32(lbllab6.Text.ToString().Trim()), Convert.ToInt32(lbllab7.Text.ToString().Trim()), Convert.ToInt32(lbllab8.Text.ToString().Trim()), Convert.ToInt32(lbllab9.Text.ToString().Trim()), Convert.ToInt32(lbllab10.Text.ToString().Trim()), Convert.ToDouble(txtAdditionalprc.Text.ToString().Trim()), Session["sesUserID"].ToString(), "U");

            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(49);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Equipment", "Update", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Equipment", "Update", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

        RadGrid1.Rebind();
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
}