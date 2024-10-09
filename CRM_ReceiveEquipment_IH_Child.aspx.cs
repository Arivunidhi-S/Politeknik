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
using System.Web.Configuration;

public partial class CRM_ReceiveEquipment_IH_Child : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    protected void Page_Init(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        BusinessTier_CRM.BindErrorMessageDetails(conn);
        BusinessTier.DisposeConnection(conn);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SqlConnection conn = BusinessTier.getConnection();
            try
            {
                string strStatus = (Request.QueryString.Get("Status").ToString());
                string strCalibType_SerielNo = (Request.QueryString.Get("CalibType_SerielNo").ToString());
                string strPartialFull = (Request.QueryString.Get("PartialFull").ToString());
                string strContractFlag = "";
                if (strStatus.ToString().Trim() == "Quote")
                    lblContQuote1.Text = "Quotation :";

                if (strStatus.ToString().Trim() == "Contr")
                    lblContQuote1.Text = "Contract :";

                txtQuantity.Text = "";
                lblEquipmentNo.Text = "";
                lblEquipment.Text = "";
                txtModelMaker.Text = "";
                txtSerielNo.Text = "";
                txtSerielNo.ToolTip = "";

                lblContQuote2.Text = "";
                lblContQuote2.Text = Request.QueryString.Get("contquip").ToString().Trim();//Contract Number or Quotation No
                lblCust.Text = "";
                lblCust.Text = Request.QueryString.Get("Customer").ToString().Trim();
                lblContQuoteID.Text = "";
                lblContQuoteID.Text = Request.QueryString.Get("ContEquipID").ToString().Trim();//Contract Id or Quotation ID
                lblCustID.Text = "";
                lblCustID.Text = Request.QueryString.Get("CustID").ToString().Trim();
                lblEquip_ID.Text = "";
                lblEquip_ID.Text = Request.QueryString.Get("Equip_ID").ToString().Trim();
                lblContQuoteDetailId.Text = "";
                lblContQuoteDetailId.Text = Request.QueryString.Get("ContratQuotationDetailID").ToString();
                lblPartialFull.Text = "";
                lblPartialFull.Text = strPartialFull.Trim();
                //btnAdd.Enabled = false;
                //txtAcessories.Enabled = false;
                //txtAccesSeriel.Enabled = false;
                //txtECDDate.Enabled = false;

                txtJob.ToolTip = "";

                if ((Convert.ToInt32(lblCustID.Text.ToString().Trim())) <= 0)
                {
                    ShowMessage(216);
                    return;
                }
                if ((Convert.ToInt32(lblEquip_ID.Text.ToString().Trim())) <= 0)
                {
                    ShowMessage(217);
                    return;
                }
                if ((Convert.ToInt32(lblContQuoteID.Text.ToString().Trim())) <= 0)
                {
                    ShowMessage(218);
                    return;
                }
                if ((Convert.ToInt32(lblContQuoteDetailId.Text.ToString().Trim())) <= 0)
                {
                    ShowMessage(219);
                    return;
                }
                conn.Open();
                if (strStatus.ToString().Trim() == "Contr")
                {

                }

                string strQuotationTransID = "0";
                if (strStatus.ToString().Trim() == "Quote")
                {
                    //Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim())
                    SqlCommand cmd1 = new SqlCommand("select QUOTATION_TRANS_ID,(Qty - ReceivedQty) as Qty,EQUIPMENT_NO,EQUIPMENT_NAME,ExCompltDays,MODEL,MAKER,Addr_Line1,Addr_Line2,POSTAL_CODE,STATE, COUNTRY, CONTACT_PERSON, CONTACT_NO_person from Vw_QuotationDetail_Equipment where CUSTOMER_ID='" + lblCustID.Text.ToString().Trim() + "' and EQUIPMENT_ID='" + Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()) + "' and QUOTATION_TRANS_ID='" + lblContQuoteDetailId.Text.ToString().Trim() + "' and BRANCH_ID=" + Convert.ToInt32(Session["sesBranchID"].ToString().Trim()) + " and Deleted=0", conn);
                    SqlDataReader reader1 = cmd1.ExecuteReader();
                    if (reader1.Read())
                    {
                        lblEquipmentNo.Text = reader1["EQUIPMENT_NO"].ToString().Trim();
                        lblEquipment.Text = reader1["EQUIPMENT_NAME"].ToString().Trim();
                        strQuotationTransID = reader1["QUOTATION_TRANS_ID"].ToString().Trim();
                        txtModelMaker.Text = reader1["MODEL"].ToString().Trim() + " / " + reader1["MAKER"].ToString().Trim();
                        lblJobList.Visible = false;

                        lblQuantity_SerielNo.Text = "Quantity :";
                        txtQuantity.Text = reader1["Qty"].ToString().Trim();
                        txtQuantityReceived.Text = reader1["Qty"].ToString().Trim();
                        txtQuantityReceived.Visible = true;
                        btnReceivedQty.Visible = true;

                        txtSerielNo.Enabled = true;
                        System.Drawing.ColorConverter colConvert = new ColorConverter();
                        txtSerielNo.BackColor = (System.Drawing.Color)colConvert.ConvertFromString("White");

                        txtECDDate.SelectedDate = DateTime.Now;
                    }
                    BusinessTier.DisposeReader(reader1);
                }

                int intRECEIVED_TRANS_ID = 0;
                lblContQuote1.ToolTip = "0"; // TO store Received Trans ID
                Session["sesGuid"] = "";
                int intQuoteID = 0;
                int intContID = 0;
                int intContDetailId = 0;
                int intQuoteDetailID = 0;
                if (strStatus.ToString().Trim() == "Quote")
                {
                    intQuoteID = Convert.ToInt32(lblContQuoteID.Text.ToString().Trim());
                    intQuoteDetailID = Convert.ToInt32(lblContQuoteDetailId.Text.ToString().Trim());
                }

                if (strStatus.ToString().Trim() == "Contr")
                {
                    intContID = Convert.ToInt32(lblContQuoteID.Text.ToString().Trim());
                    intContDetailId = Convert.ToInt32(lblContQuoteDetailId.Text.ToString().Trim());
                }


                SqlDataReader readerTrans = BusinessTier_CRM.getAllByQuote_CRMReceiveEquipmentTrans(conn, intQuoteID, intContID, Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), strStatus.ToString().Trim(), intQuoteDetailID, intContDetailId, "Trans");
                if (readerTrans.Read())
                {
                    if (!(string.IsNullOrEmpty(readerTrans["RECEIVED_TRANS_ID"].ToString().TrimEnd())))
                    {
                        intRECEIVED_TRANS_ID = Convert.ToInt32(readerTrans["RECEIVED_TRANS_ID"].ToString().TrimEnd());
                        lblContQuote1.ToolTip = intRECEIVED_TRANS_ID.ToString();
                    }
                    if (!(string.IsNullOrEmpty(readerTrans["GUID"].ToString().TrimEnd())))
                        Session["sesGuid"] = (readerTrans["GUID"].ToString().TrimEnd());
                }
                BusinessTier.DisposeReader(readerTrans);

                if (intRECEIVED_TRANS_ID > 0)
                {
                    // Need to generate job number for contract quantity and quantity...

                }

                if (intRECEIVED_TRANS_ID <= 0) // To check if the quotation number does not exist, we need to create new record 
                {
                    if ((Convert.ToInt32(lblContQuote1.ToolTip.ToString())) <= 0) // Should use this...this the same for above validation
                    {
                        Session["sesGuid"] = Guid.NewGuid().ToString();
                        string strBranchCode = Session["sesBranchCode"].ToString().Trim();
                        DateTime CurrDateTime = DateTime.Now;
                        string strCurrYear = CurrDateTime.Year.ToString().Trim();
                        string strDateFrom = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + " 12:00:00 AM";
                        string strDateTo = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + " 11:59:59 PM";

                    }
                }

                //--------------------------------------------------------------------------------------------------------------------
                string strFilePath = "";
                string strQuery = "";
                if (strStatus.ToString().Trim() == "Quote")
                    strQuery = "select * FROM CRM_ReceiveEquipment WHERE CUSTOMER_ID = " + Convert.ToInt32(lblCustID.Text.ToString().Trim()) + " and  QUOTATION_ID=" + intQuoteID + " and Deleted=0 and Status <> 'COMPLETED'";

                SqlCommand command1 = new SqlCommand(strQuery, conn);
                SqlDataReader readerAdd = command1.ExecuteReader();
                if (readerAdd.Read())  // Need to get latest record // Need to have another validation also
                {
                    if (!(string.IsNullOrEmpty(readerAdd["Bill_Address"].ToString().Trim())))
                        txtBillingAdd.Text = readerAdd["Bill_Address"].ToString().Trim();
                }
                BusinessTier.DisposeReader(readerAdd);

                //-----------------------------------------------------------------------------------------------------------------

                //SqlCommand cmd11 = new SqlCommand("select ECD,FILE_PATH,Lab1,Lab2,Lab3,Lab4,Lab5,Lab6,Lab7,Lab8,Lab9,Lab10,IntervalNo,IntervalByMonthYear,DISCRIPENCY,REMARKS FROM CRM_ReceiveEquipment_Trans WHERE RECEIVED_TRANS_ID=" + Convert.ToInt32(lblContQuote1.ToolTip) + " and Deleted=0 and Status <> 'COMPLETED'", conn);

                SqlCommand cmd11 = new SqlCommand("select ECD,FILE_PATH,Lab1,Lab2,Lab3,Lab4,Lab5,Lab6,Lab7,Lab8,Lab9,Lab10,IntervalNo,IntervalByMonthYear,DISCRIPENCY,REMARKS FROM Vw_CRM_ReceiveEquipmentTransDetails WHERE RECEIVED_TRANS_ID=" + Convert.ToInt32(lblContQuote1.ToolTip) + " and Deleted=0 and STATUS <> 'COMPLETED'", conn);
                SqlDataReader readerAdd1 = cmd11.ExecuteReader();
                if (readerAdd1.Read())
                {
                    if (!(string.IsNullOrEmpty(readerAdd1["REMARKS"].ToString().Trim())))
                        txtRemarks.Text = readerAdd1["REMARKS"].ToString().Trim();
                }
                BusinessTier.DisposeReader(readerAdd1);

                //---------------------------------------------------------------------------------------------------------------------





                if (!(string.IsNullOrEmpty(strFilePath.ToString().Trim())))
                {
                    linkDownload.Text = strFilePath.ToString().Trim();
                    linkDownload.Visible = true;
                    chkLink.Visible = true;
                    chkLink.Checked = true;
                    RadAsyncUpload1.Visible = false;
                }
                else
                {
                    linkDownload.Text = "";
                    linkDownload.Visible = false;
                    chkLink.Visible = false;
                    chkLink.Checked = false;
                    RadAsyncUpload1.Visible = true;
                }
                BusinessTier.DisposeConnection(conn);
            }
            catch (Exception ex)
            {
                //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_Child", "Page_Load", ex.ToString(), "Audit");

                ShowMessage(ex.Message.ToString(), "Red");
            }
            finally
            {
                BusinessTier.DisposeConnection(conn);
            }
        }
    }

    protected void btnReceivedQty_OnClick(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            lblStatus.Text = "";
            string strStatus = "";
            if (lblContQuote1.Text.ToString().Trim() == "Contract :")
                strStatus = "Contr";
            if (lblContQuote1.Text.ToString().Trim() == "Quotation :")
                strStatus = "Quote";
            Session["sesGuid"] = Guid.NewGuid().ToString();
            //string strBranchCode = Session["sesBranchCode"].ToString().Trim();
            string strBranchCode = "1";
            DateTime CurrDateTime = DateTime.Now;
            string strCurrYear = CurrDateTime.Year.ToString().Trim();
            string strDateFrom = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + " 12:00:00 AM";
            string strDateTo = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + " 11:59:59 PM";

            if ((string.IsNullOrEmpty(strBranchCode.Trim())))
            {
                ShowMessage("Branch Code Can Not Be Empty", "Red");
                return;
            }
            int intQuoteID = 0;
            int intContID = 0;
            int intContDetailId = 0;
            int intQuoteDetailID = 0;
            if (strStatus.ToString().Trim() == "Quote")
            {
                intQuoteID = Convert.ToInt32(lblContQuoteID.Text.ToString().Trim());
                intQuoteDetailID = Convert.ToInt32(lblContQuoteDetailId.Text.ToString().Trim());
            }

            if (strStatus.ToString().Trim() == "Contr")
            {
                intContID = Convert.ToInt32(lblContQuoteID.Text.ToString().Trim());
                intContDetailId = Convert.ToInt32(lblContQuoteDetailId.Text.ToString().Trim());
            }
            if ((Convert.ToInt32(lblCustID.Text.ToString().Trim())) <= 0)
            {
                ShowMessage(216);
                return;
            }
            if ((Convert.ToInt32(lblEquip_ID.Text.ToString().Trim())) <= 0)
            {
                ShowMessage(217);
                return;
            }
            if ((Convert.ToInt32(lblContQuoteID.Text.ToString().Trim())) <= 0)
            {
                ShowMessage(218);
                return;
            }
            if ((Convert.ToInt32(lblContQuoteDetailId.Text.ToString().Trim())) <= 0)
            {
                ShowMessage(219);
                return;
            }
            if ((Convert.ToInt32(txtQuantityReceived.Text.ToString().Trim())) <= 0)
            {
                ShowMessage(220);
                return;
            }
            if (string.IsNullOrEmpty(txtQuantityReceived.Text.ToString().Trim()))
            {
                ShowMessage(220);
                return;
            }
            int intGridCountFlag = 0;
            if (RadGridJob.Items.Count <= 0)
            {
                if ((Convert.ToInt32(txtQuantityReceived.Text.ToString().Trim())) > (Convert.ToInt32(txtQuantity.Text.ToString().Trim())))
                {
                    ShowMessage(215);
                    return;
                }
                intGridCountFlag = 0;
            }
            if (RadGridJob.Items.Count > 0)
            {
                if ((Convert.ToInt32(txtQuantityReceived.Text.ToString())) > ((Convert.ToInt32(lblQuantityReceived.Text.ToString())) + (Convert.ToInt32(txtQuantity.Text.ToString().Trim()))))
                {
                    ShowMessage(215);
                    return;
                }
                intGridCountFlag = 1;
            }

            conn.Open();
            int intRECEIVED_TRANS_ID = 0;
            SqlDataReader readerTrans = BusinessTier_CRM.getAllByQuote_CRMReceiveEquipmentTrans(conn, intQuoteID, intContID, Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), strStatus.ToString().Trim(), intQuoteDetailID, intContDetailId, "Trans");
            if (readerTrans.Read())
            {
                if (!(string.IsNullOrEmpty(readerTrans["RECEIVED_TRANS_ID"].ToString().TrimEnd())))
                {
                    intRECEIVED_TRANS_ID = Convert.ToInt32(readerTrans["RECEIVED_TRANS_ID"].ToString().TrimEnd());
                    lblContQuote1.ToolTip = intRECEIVED_TRANS_ID.ToString();
                }
                if (!(string.IsNullOrEmpty(readerTrans["GUID"].ToString().TrimEnd())))
                    Session["sesGuid"] = (readerTrans["GUID"].ToString().TrimEnd());
            }
            BusinessTier.DisposeReader(readerTrans);



            string strJobnumber = "";

            if (strStatus.ToString().Trim() == "Quote")
            {
                if (intGridCountFlag == 0)
                {
                    if (intRECEIVED_TRANS_ID > 0)
                    {
                        if ((Convert.ToInt32(txtQuantityReceived.Text.ToString().Trim())) > (Convert.ToInt32(txtQuantity.Text.ToString().Trim())))
                        {
                            ShowMessage(215);
                            return;
                        }
                        strJobnumber = generateJobNos((Convert.ToInt32(txtQuantityReceived.Text.ToString().Trim())), strBranchCode, "0", strStatus, intQuoteID, intContID, Convert.ToInt32(lblCustID.Text.ToString().Trim()));
                        int intGetRunning = 0;
                        string sql1 = "select max(RunningNo) as runningno from Vw_CRM_ReceiveEquipmentTransDetails where JobNo='" + strJobnumber.ToString().Trim() + "' and status='IH' and MASTER_CONTRACT_ID=0 and  QUOTATION_ID= " + intQuoteID + "  and Deleted=0 and Status_Trans <> 'COMPLETED'";
                        SqlCommand command11 = new SqlCommand(sql1, conn);
                        SqlDataReader readergetBatch = command11.ExecuteReader();
                        if (readergetBatch.Read())
                        {
                            if (!(string.IsNullOrEmpty(readergetBatch["runningno"].ToString().Trim())))
                                intGetRunning = Convert.ToInt32(readergetBatch["runningno"].ToString().Trim());
                        }
                        BusinessTier.DisposeReader(readergetBatch);
                        for (int j = 1; j <= (Convert.ToInt32(txtQuantityReceived.Text.ToString().Trim())); j++)
                        {
                            int flg = BusinessTier_CRM.CRM_ReceivingEquipment_TransDetails(conn, 0, "", "", "", Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()), strJobnumber.ToString().Trim(), 0, intRECEIVED_TRANS_ID, Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), Session["sesGuid"].ToString().Trim(), DateTime.Now, 0, 0, "", "", "Insert", intGetRunning + j, 0, "", txtphysical.Text.ToString().Trim(), txtfuctional.Text.ToString().Trim());
                            int intFlag = BusinessTier_CRM.CRM_ReceivingEquipmentTrans_AddQty(conn, intRECEIVED_TRANS_ID, Convert.ToInt32(lblCustID.Text.ToString().Trim()), intQuoteID, intContID, Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()), 1, Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), intQuoteDetailID, intContDetailId);
                            if (intFlag > 0)
                                txtQuantity.Text = ((Convert.ToInt32(txtQuantity.Text.ToString().Trim())) - 1).ToString();

                        }
                    }

                    if (intRECEIVED_TRANS_ID <= 0) // To check if the quotation number does not exist, we need to create new record 
                    {
                        if ((Convert.ToInt32(txtQuantityReceived.Text.ToString().Trim())) > (Convert.ToInt32(txtQuantity.Text.ToString().Trim())))
                        {
                            ShowMessage(215);
                            return;
                        }
                        if ((Convert.ToInt32(lblContQuote1.ToolTip.ToString())) <= 0) // Should use this...this the same for above validation
                        {
                            strJobnumber = generateJobNos((Convert.ToInt32(txtQuantityReceived.Text.ToString().Trim())), strBranchCode, "0", strStatus, intQuoteID, intContID, Convert.ToInt32(lblCustID.Text.ToString().Trim()));

                            intRECEIVED_TRANS_ID = BusinessTier_CRM.CRM_ReceivingEquipment_Trans(conn, 0, Convert.ToInt32(lblCustID.Text.ToString().Trim()), intQuoteID, intContID, Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()), (Convert.ToInt32(txtQuantityReceived.Text.ToString().Trim())), Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), Session["sesGuid"].ToString().Trim(), intQuoteDetailID, intContDetailId, lblPartialFull.Text.ToString(), "Insert", "IH");
                            lblContQuote1.ToolTip = intRECEIVED_TRANS_ID.ToString();
                            if (intRECEIVED_TRANS_ID > 0)
                                txtQuantity.Text = ((Convert.ToInt32(txtQuantity.Text.ToString().Trim())) - (Convert.ToInt32(txtQuantityReceived.Text.ToString().Trim()))).ToString();
                        }
                        int intGetRunning = 0;
                        string sql1 = "select max(RunningNo) as runningno from Vw_CRM_ReceiveEquipmentTransDetails where  JobNo='" + strJobnumber.ToString().Trim() + "' and status='IH' and  MASTER_CONTRACT_ID=0 and  QUOTATION_ID= " + intQuoteID + " and Deleted=0 and Status_Trans <> 'COMPLETED'";
                        SqlCommand command11 = new SqlCommand(sql1, conn);
                        SqlDataReader readergetBatch = command11.ExecuteReader();
                        if (readergetBatch.Read())
                        {
                            if (!(string.IsNullOrEmpty(readergetBatch["runningno"].ToString().Trim())))
                                intGetRunning = Convert.ToInt32(readergetBatch["runningno"].ToString().Trim());
                        }
                        BusinessTier.DisposeReader(readergetBatch);


                        for (int j = 1; j <= (Convert.ToInt32(txtQuantityReceived.Text.ToString().Trim())); j++)
                        {
                            int flg = BusinessTier_CRM.CRM_ReceivingEquipment_TransDetails(conn, 0, "", "", "", Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()), strJobnumber.ToString().Trim(), 0, intRECEIVED_TRANS_ID, Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), Session["sesGuid"].ToString().Trim(), DateTime.Now, 0, 0, "", "", "Insert", intGetRunning + j, 0, "", txtphysical.Text.ToString().Trim(), txtfuctional.Text.ToString().Trim());
                            //int intFlag = BusinessTier_CRM.CRM_ReceivingEquipmentTrans_AddQty(conn, intRECEIVED_TRANS_ID, Convert.ToInt32(lblCustID.Text.ToString().Trim()), intQuoteID, intContID, Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()), 1, Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), intQuoteDetailID, intContDetailId);
                        }
                    }
                }
                if (intGridCountFlag == 1)
                {
                    if (intRECEIVED_TRANS_ID > 0)
                    {
                        if ((Convert.ToInt32(txtQuantityReceived.Text.ToString())) > (Convert.ToInt32(lblQuantityReceived.Text.ToString())))
                        {
                            // Need to update the received quantity.......
                            if ((Convert.ToInt32(txtQuantityReceived.Text.ToString())) > (Convert.ToInt32(lblQuantityReceived.Text.ToString())) + (Convert.ToInt32(txtQuantity.Text.ToString())))
                            {
                                ShowMessage(215);
                                return;
                            }
                            if (((Convert.ToInt32(txtQuantityReceived.Text.ToString())) - (Convert.ToInt32(lblQuantityReceived.Text.ToString()))) > (Convert.ToInt32(txtQuantity.Text.ToString())))
                            {
                                ShowMessage(215);
                                return;
                            }

                            if ((Convert.ToInt32(lblContQuote1.ToolTip.ToString())) > 0) // Should use this...this the same for above validation
                            {
                                // Need to generate job number for contract quantity and quantity...
                                strJobnumber = generateJobNos((Convert.ToInt32(txtQuantityReceived.Text.ToString().Trim())), strBranchCode, "0", strStatus, intQuoteID, intContID, Convert.ToInt32(lblCustID.Text.ToString().Trim()));
                            }

                            int intGetRunning = 0;
                            string sql1 = "select max(RunningNo) as runningno from Vw_CRM_ReceiveEquipmentTransDetails where status='IH' and MASTER_CONTRACT_ID=0 and  QUOTATION_ID= " + intQuoteID + " and Deleted=0 and Status_Trans <> 'COMPLETED'";
                            SqlCommand command11 = new SqlCommand(sql1, conn);
                            SqlDataReader readergetBatch = command11.ExecuteReader();
                            if (readergetBatch.Read())
                            {
                                if (!(string.IsNullOrEmpty(readergetBatch["runningno"].ToString().Trim())))
                                    intGetRunning = Convert.ToInt32(readergetBatch["runningno"].ToString().Trim());
                            }
                            BusinessTier.DisposeReader(readergetBatch);
                            for (int j = 1; j <= ((Convert.ToInt32(txtQuantityReceived.Text.ToString().Trim())) - (Convert.ToInt32(lblQuantityReceived.Text.ToString()))); j++)
                            {
                                if (((Convert.ToInt32(txtQuantityReceived.Text.ToString())) - (Convert.ToInt32(lblQuantityReceived.Text.ToString()))) <= (Convert.ToInt32(txtQuantity.Text.ToString())))
                                {
                                    int flg = BusinessTier_CRM.CRM_ReceivingEquipment_TransDetails(conn, 0, "", "", "", Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()), strJobnumber.ToString().Trim(), 0, intRECEIVED_TRANS_ID, Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), Session["sesGuid"].ToString().Trim(), DateTime.Now, 0, 0, "", "", "Insert", intGetRunning + j, 0, "", txtphysical.Text.ToString().Trim(), txtfuctional.Text.ToString().Trim());
                                    int intFlag = BusinessTier_CRM.CRM_ReceivingEquipmentTrans_AddQty(conn, intRECEIVED_TRANS_ID, Convert.ToInt32(lblCustID.Text.ToString().Trim()), intQuoteID, intContID, Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()), 1, Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), intQuoteDetailID, intContDetailId);
                                    if (intFlag > 0)
                                        txtQuantity.Text = ((Convert.ToInt32(txtQuantity.Text.ToString().Trim())) - 1).ToString();
                                }
                            }
                        }
                        if ((Convert.ToInt32(txtQuantityReceived.Text.ToString())) < (Convert.ToInt32(lblQuantityReceived.Text.ToString())))
                        {
                            // Need to delete from the table ....go against radgrid...
                            ShowMessage("This Screen Have No Permission To Perform This Task", "Red");
                        }
                    }
                }
            }
            RadGridJob.DataSource = new string[] { };
            RadGridJob.DataSource = DataSourceHelper("RECEIVEDTRANS", (Convert.ToInt32(lblContQuote1.ToolTip.ToString())));
            RadGridJob.Rebind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message.ToString(), "Red");
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_Child", "btnReceivedQty_OnClick", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);

        }
    }

    protected void btnSave_Close_Click(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            if (RadGridJob.Items.Count > 0)
            {
                conn.Open();
                int nullSerielflag = 0;
                int nullLabFlag = 0;
                foreach (GridDataItem grdItem in RadGridJob.Items)
                {
                    RadButton btnJOBNO = (RadButton)grdItem.FindControl("btnJOBNO");
                    Label lblRECEIVED_TRANS_ID = (Label)grdItem.FindControl("lblRECEIVED_TRANS_ID");
                    Label lblSerielNo = (Label)grdItem.FindControl("lblSerielNo");
                    Label lblRemarks = (Label)grdItem.FindControl("lblRemarks");
                    Label lblDiscre = (Label)grdItem.FindControl("lblDiscre");
                    Label lblRunningNo = (Label)grdItem.FindControl("lblRunningNo");
                    lblSerielNo.Text = lblSerielNo.Text.ToString().Trim();
                    if ((string.IsNullOrEmpty(lblSerielNo.Text.ToString().Trim())))
                    {
                        nullSerielflag = 1;
                    }
                }

                if (ChkLab1.Checked == true) { nullLabFlag = 1; }
                if (ChkLab2.Checked == true) { nullLabFlag = 1; }
                if (ChkLab3.Checked == true) { nullLabFlag = 1; }
                if (ChkLab4.Checked == true) { nullLabFlag = 1; }
                if (ChkLab5.Checked == true) { nullLabFlag = 1; }
                if (ChkLab6.Checked == true) { nullLabFlag = 1; }
                if (ChkLab7.Checked == true) { nullLabFlag = 1; }
                if (ChkLab8.Checked == true) { nullLabFlag = 1; }
                if (ChkLab9.Checked == true) { nullLabFlag = 1; }
                if (ChkLab10.Checked == true) { nullLabFlag = 1; }

                if (ChkLab1.Checked != true) { lbllab1.Text = "0"; }
                if (ChkLab2.Checked != true) { lbllab2.Text = "0"; }
                if (ChkLab3.Checked != true) { lbllab3.Text = "0"; }
                if (ChkLab4.Checked != true) { lbllab4.Text = "0"; }
                if (ChkLab5.Checked != true) { lbllab5.Text = "0"; }
                if (ChkLab6.Checked != true) { lbllab6.Text = "0"; }
                if (ChkLab7.Checked != true) { lbllab7.Text = "0"; }
                if (ChkLab8.Checked != true) { lbllab8.Text = "0"; }
                if (ChkLab9.Checked != true) { lbllab9.Text = "0"; }
                if (ChkLab10.Checked != true) { lbllab10.Text = "0"; }
                if (nullSerielflag == 1)
                {
                    ShowMessage("Please Enter The Serial No/ID No", "Red");
                    return;
                }
                if (nullLabFlag == 0)
                {
                    ShowMessage("Please Click The Performing Lab", "Red");
                    return;
                }
                string btnSaveClose = ((Button)sender).ID.ToString().Trim();
                //    Page.ClientScript.RegisterClientScriptBlock(GetType(),
                //"CloseScript", "refreshParentPage()", true);
                //---------------------------------------------this updation no need to have here......user can use the save button......
                int flg1 = BusinessTier_CRM.CRM_ReceivingEquipment_TransLab(conn, Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()), (Convert.ToInt32(lblContQuote1.ToolTip.ToString())), Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), Session["sesGuid"].ToString().Trim(), Convert.ToDateTime(txtECDDate.SelectedDate), txtRemarks.Text.ToString().Trim(), txtBillingAdd.Text.ToString().Trim(), txtDeliveryAdd.Text.ToString().Trim(), txtCertAddress.Text.ToString().Trim(), "", Convert.ToInt32(lbllab1.Text.ToString().Trim()), Convert.ToInt32(lbllab2.Text.ToString().Trim()), Convert.ToInt32(lbllab3.Text.ToString().Trim()), Convert.ToInt32(lbllab4.Text.ToString().Trim()), Convert.ToInt32(lbllab5.Text.ToString().Trim()), Convert.ToInt32(lbllab6.Text.ToString().Trim()), Convert.ToInt32(lbllab7.Text.ToString().Trim()), Convert.ToInt32(lbllab8.Text.ToString().Trim()), Convert.ToInt32(lbllab9.Text.ToString().Trim()), Convert.ToInt32(lbllab10.Text.ToString().Trim()), txtBilContactName.Text.ToString().Trim(), txtBillContactNo.Text.ToString().Trim(), txtBillContactMail.Text.ToString().Trim(), txtDelContactName.Text.ToString().Trim(), txtDelContactNo.Text.ToString().Trim(), txtDelContactMail.Text.ToString().Trim(), txtCertContactName.Text.ToString().Trim(), txtCertContactNo.Text.ToString().Trim(), txtCertContactMail.Text.ToString().Trim(), 0, cboIntervalTime.Text.ToString().Trim(), DateTime.Now, 0, 0, 0, "UpdateLab");
                //------------------------------------------------------
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "closeScript", "CloseDialog('');", true);
                BusinessTier.DisposeConnection(conn);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "closeScript", "CloseDialog('');", true);
            }

        }
        catch (Exception ex)
        {
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_Child", "btnSave_Close_Click", ex.ToString(), "Audit");
            ShowMessage(ex.Message.ToString(), "Red");
        }
        finally
        {
            // ScriptManager.RegisterStartupScript(Page, typeof(Page), "closeScript", "CloseDialog('');", true);
            BusinessTier.DisposeConnection(conn);
        }
    }


    protected string generateJobNos(int Quantity, string strBranchCode, string strLastAutoNo, string strStatus, int intQuoteID, int intContID, int intCustId)
    {
        string strgeneratingJobNo = "";
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            //Generate Job Number-----------------------------------------------------

            if (Quantity > 0)
            {
                DateTime CurrDateTime = DateTime.Now;
                string strCurrYear = CurrDateTime.Year.ToString().Trim();
                string strDateFrom = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + " 12:00:00 AM";
                string strDateTo = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + " 11:59:59 PM";
                conn.Open();

                string sql1 = "";
                int intGetReceivedID = 0;
                //----------------Need to chek the batch number, whether the its already received---------------------------------------------------------------------
                string strBatchNo = "";
                if (strStatus.ToString().Trim() == "Quote")
                {
                    sql1 = "select RECEIVED_ID,BatchNo FROM CRM_ReceiveEquipment WHERE MASTER_CONTRACT_ID=0 and  QUOTATION_ID = " + intQuoteID + " and Deleted=0 and Status <> 'COMPLETED'";
                }
                SqlCommand command1 = new SqlCommand(sql1, conn);
                SqlDataReader readergetBatch = command1.ExecuteReader();
                if (readergetBatch.Read())
                {
                    if (!(string.IsNullOrEmpty(readergetBatch["RECEIVED_ID"].ToString().Trim())))
                        intGetReceivedID = Convert.ToInt32(readergetBatch["RECEIVED_ID"].ToString().Trim());
                    if (!(string.IsNullOrEmpty(readergetBatch["BatchNo"].ToString().Trim())))
                        strBatchNo = (readergetBatch["BatchNo"].ToString().Trim());
                }
                BusinessTier.DisposeReader(readergetBatch);
                //-------------------------------------------------------------------------------------------

                int intgetID = 0;
                int intgetAutoNo = 0;
                string strgetYear = "0";
                string sql = "select * FROM CRM_AutoJob WHERE BranchId = '" + Session["sesBranchID"].ToString().Trim() + "' and Year_Val = '" + strCurrYear.ToString().Trim() + "'";
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader readergetID = command.ExecuteReader();
                if (readergetID.Read())
                {
                    if (!(string.IsNullOrEmpty(readergetID["JobAutoId"].ToString().Trim())))
                        intgetID = Convert.ToInt32(readergetID["JobAutoId"].ToString().Trim());

                    if (!(string.IsNullOrEmpty(readergetID["AutoNo"].ToString().Trim())))
                        intgetAutoNo = Convert.ToInt32(readergetID["AutoNo"].ToString().Trim());

                    strgetYear = readergetID["Year_Val"].ToString().Trim();
                }
                BusinessTier.DisposeReader(readergetID);

                //---------------------------------------------------------------------------------------------------
                Int32 intAutono = Int32.Parse(intgetAutoNo.ToString().Trim());
                Int32 intAutoNoInc = 0;
                if (intGetReceivedID == 0)//Insert new record into received table
                {
                    if ((intgetID == 0) || (string.IsNullOrEmpty(intgetID.ToString())))
                    {
                        intAutoNoInc = 1;
                        strgeneratingJobNo = "BMCL/" + strCurrYear.Substring(2) + "/00" + intAutoNoInc.ToString().Trim();
                        int intRECEIVEDID = BusinessTier_CRM.CRM_ReceivingEquipment(conn, 0, intCustId, intQuoteID, intContID, Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), strgeneratingJobNo, "Insert", "IH", Session["SelectedPO"].ToString().Trim());
                        SaveAutoJobTable(Session["sesBranchID"].ToString().Trim(), intAutoNoInc.ToString().Trim(), strCurrYear.ToString().Trim(), strLastAutoNo.ToString().Trim(), "Insert");
                    }
                    else//if ((intgetID > 0))
                    {
                        if (strgetYear.ToString() == strCurrYear.ToString())
                        {
                            intAutoNoInc = intAutono + 1;
                            string maxid = intAutoNoInc.ToString().Trim();
                            if (maxid.Length == 2)
                            {
                                strgeneratingJobNo = "BMCL/" + strCurrYear.Substring(2) + "/0" + intAutoNoInc.ToString();
                            }
                            else if (maxid.Length == 1)
                            {
                                strgeneratingJobNo = "BMCL/" + strCurrYear.Substring(2) + "/00" + intAutoNoInc.ToString();
                            }
                            else
                            {
                                strgeneratingJobNo = "BMCL/" + strCurrYear.Substring(2) + "/" + intAutoNoInc.ToString();
                            }
                            int intRECEIVEDID = BusinessTier_CRM.CRM_ReceivingEquipment(conn, 0, intCustId, intQuoteID, intContID, Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), strgeneratingJobNo, "Insert", "IH", Session["SelectedPO"].ToString().Trim());
                            SaveAutoJobTable(Session["sesBranchID"].ToString().Trim(), intAutoNoInc.ToString().Trim(), strCurrYear.ToString().Trim(), strLastAutoNo.ToString().Trim(), "Update");
                        }
                        else
                        {
                            intAutoNoInc = 1;
                            strgeneratingJobNo = "BMCL/" + strCurrYear.Substring(2) + "/00" + intAutoNoInc.ToString().Trim();
                            int intRECEIVEDID = BusinessTier_CRM.CRM_ReceivingEquipment(conn, 0, intCustId, intQuoteID, intContID, Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), strgeneratingJobNo, "Insert", "IH", Session["SelectedPO"].ToString().Trim());
                            SaveAutoJobTable(Session["sesBranchID"].ToString().Trim(), intAutoNoInc.ToString().Trim(), strCurrYear.ToString().Trim(), strLastAutoNo.ToString().Trim(), "Insert");
                        }
                    }
                }
                if (intGetReceivedID > 0)
                {
                    strgeneratingJobNo = strBatchNo.Trim();
                }




                //if ((intgetID == 0))
                //{
                //    SaveAutoJobTable(Session["sesBranchID"].ToString().Trim(), "1", strCurrYear.ToString().Trim(), strLastAutoNo.ToString().Trim(), "Insert");
                //    strgeneratingJobNo = strBranchCode.ToString().Trim() + strCurrYear.ToString().Trim() + "-" + "1";

                //    int intRECEIVEDID = BusinessTier_CRM.CRM_ReceivingEquipment(conn, 0, intCustId, intQuoteID, intContID, Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), strgeneratingJobNo, "Insert");

                //}
                //else
                //{
                //    if (strgetYear.ToString() == strCurrYear.ToString())   // validation should be reverse....... it will start with below one
                //    {

                //    }
                //    else
                //    {
                //        SaveAutoJobTable(Session["sesBranchID"].ToString().Trim(), "1", strCurrYear.ToString().Trim(), strLastAutoNo.ToString().Trim(), "Insert");
                //        strgeneratingJobNo = strBranchCode.ToString().Trim() + strCurrYear.ToString().Trim() + "-" + "1";

                //        int intRECEIVEDID = BusinessTier_CRM.CRM_ReceivingEquipment(conn, 0, intCustId, intQuoteID, intContID, Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), strgeneratingJobNo, "Insert");

                //    }

                //}
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Method:generateJobNo: " + ex.Message.ToString();
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_Child", "generateJobNos", ex.ToString(), "Audit");

        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
        return strgeneratingJobNo;
    }

    protected void SaveAutoJobTable(string strBranchId, string strAutoNo, string strYear, string strLastAutoNo, string saveFlag)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        int flagMrvAuto = BusinessTier_CRM.saveJobAuto(conn, strBranchId.ToString().Trim(), strAutoNo.ToString().Trim(), strYear.ToString().Trim(), strLastAutoNo.Trim(), saveFlag.ToString().Trim());
        BusinessTier.DisposeConnection(conn);
    }

    protected void RadGridJob_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGridJob.DataSource = new string[] { };
            if (string.IsNullOrEmpty(lblContQuote1.ToolTip.ToString()))
                RadGridJob.DataSource = DataSourceHelper("Job", 0);
            else
                RadGridJob.DataSource = DataSourceHelper("RECEIVEDTRANS", (Convert.ToInt32(lblContQuote1.ToolTip.ToString())));
        }
        catch (Exception ex)
        {
            if (string.IsNullOrEmpty(lblStatus.Text.ToString().Trim()))
                ShowMessage(ex.Message.ToString(), "Red");
        }
    }

    private object DataSourceHelper(string status, int id)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "";

        if (status == "RECEIVEDTRANS")
        {
            //((STATUS_TRANS='NO') or (STATUS_TRANS='YES'))
            sql = "select  File_Path,RECEIVED_TRANS_DETAIL_ID,SERIEL_NO,JOBNO,GUID,RECEIVED_TRANS_ID,RunningNo,REMARKS,DISCRIPENCY from Vw_CRM_ReceiveEquipmentTransDetails where RECEIVED_TRANS_ID=" + id + " and deleted =0 and OrderNo=0 and EQUIPMENT_ID='" + Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()) + "' and BRANCH_ID='" + Convert.ToInt32(Session["sesBranchID"].ToString().Trim()) + "' and  GUID='" + Session["sesGuid"].ToString().Trim() + "' and ((STATUS_TRANS='NO') or (STATUS_TRANS='YES'))  order by RECEIVED_TRANS_DETAIL_ID asc";
        }
        if (status == "Job")
        {
            sql = "select  File_Path,RECEIVED_TRANS_DETAIL_ID,SERIEL_NO,JOBNO,GUID,RECEIVED_TRANS_ID,RunningNo,REMARKS,DISCRIPENCY from Vw_CRM_ReceiveEquipmentTransDetails where RECEIVED_TRANS_ID=" + id + " and deleted =10 and OrderNo=0 and EQUIPMENT_ID='" + Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()) + "' and BRANCH_ID='" + Convert.ToInt32(Session["sesBranchID"].ToString().Trim()) + "' and  GUID='" + Session["sesGuid"].ToString().Trim() + "' and ((STATUS_TRANS='NO') or (STATUS_TRANS='YES'))  order by RECEIVED_TRANS_DETAIL_ID asc";
        }
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        txtQuantityReceived.Text = g_datatable.Rows.Count.ToString();
        lblQuantityReceived.Text = g_datatable.Rows.Count.ToString(); //This is for internal use inside validation
        //if (g_datatable.Rows.Count > 0)
        //    btnReceivedQty.Visible = false;
        return g_datatable;
    }

    protected void RadGridJob_ItemCommand(object source, GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();

        try
        {
            if (e.CommandName == "RowClick" && e.Item is GridDataItem)
            {
                listAccessories.Items.Clear();
                e.Item.Selected = true;
                if (e.Item.Selected == true)
                {
                    txtRemarks.Text = "";
                    string strTransDetailId = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RECEIVED_TRANS_DETAIL_ID"]).ToString();
                    RadButton btnJOBNO = (RadButton)e.Item.FindControl("btnJOBNO");
                    Label lblRECEIVED_TRANS_ID = (Label)e.Item.FindControl("lblRECEIVED_TRANS_ID");
                    Label lblSerielNo = (Label)e.Item.FindControl("lblSerielNo");
                    Label lblRemarks = (Label)e.Item.FindControl("lblRemarks");
                    Label lblDiscre = (Label)e.Item.FindControl("lblDiscre");
                    Label lblRunningNo = (Label)e.Item.FindControl("lblRunningNo");
                    //Label lblphysial = (Label)e.Item.FindControl("lblphysial");
                    //Label lblfuntional = (Label)e.Item.FindControl("lblfuntional");
                    chkDiscripency.Checked = false;
                    chkDiscripency.Enabled = true;
                    lblDiscrepancy.Visible = false;
                    txtRemarks.Visible = false;

                    //jbno = btnJOBNO.Text.ToString().Trim();
                    txtphysical.ToolTip = lblRunningNo.Text.ToString().Trim();

                    txtSerielNo.Text = lblSerielNo.Text.ToString().Trim();
                    txtSerielNo.ToolTip = lblSerielNo.Text.ToString().Trim();
                    //txtphysical.Text = lblphysial.Text.ToString().Trim();
                    //txtfuctional.Text = lblfuntional.Text.ToString().Trim();

                    if (lblDiscre.Text.ToString().Trim() == "True")
                    {
                        lblDiscrepancy.Visible = true;
                        chkDiscripency.Checked = true;
                        chkDiscripency.Enabled = true;
                        txtRemarks.Visible = true;
                    }
                    txtRemarks.Text = "";
                    txtRemarks.Text = lblRemarks.Text.ToString().Trim();

                    if (btnReceivedQty.ToolTip.ToString() == "Q")
                    {
                        txtSerielNo.Enabled = true;
                    }



                    lblContQuote1.ToolTip = lblRECEIVED_TRANS_ID.Text.ToString().Trim();
                    txtJob.Text = btnJOBNO.Text.ToString().Trim();
                    txtJob.ToolTip = strTransDetailId.Trim();
                    conn.Open();


                    if (!(string.IsNullOrEmpty(txtJob.ToolTip.ToString())))
                    {
                        listAccessories.Items.Clear();

                        int intOrderNo = 0;
                        SqlCommand cmd1 = new SqlCommand("select RECEIVED_TRANS_DETAIL_ID,ACCESSORIES,ACCESSORIES_SERIEL,OrderNo,Seriel_No,DISCRIPENCY,REMARKS from CRM_ReceiveEquipment_Trans_Details where AccessMasterID='" + strTransDetailId + "' and RECEIVED_TRANS_ID='" + (Convert.ToInt32(lblContQuote1.ToolTip.ToString())) + "' and BRANCH_ID=" + Convert.ToInt32(Session["sesBranchID"].ToString().Trim()) + " and Deleted=0 and OrderNo >= 1 and JOBNO='" + txtJob.Text.ToString().Trim().Split('-')[0] + "-" + txtJob.Text.ToString().Trim().Split('-')[1] + "' ", conn);
                        SqlDataReader reader = cmd1.ExecuteReader();
                        while (reader.Read())
                        {
                            intOrderNo = intOrderNo + 1;
                            //= Convert.ToInt32(reader["OrderNo"].ToString()) - 1;
                            string strText = intOrderNo.ToString() + " |" + reader["ACCESSORIES_SERIEL"].ToString().Trim() + " | " + reader["ACCESSORIES"].ToString().Trim();
                            string strid = (reader["RECEIVED_TRANS_DETAIL_ID"].ToString());
                            listAccessories.Items.Add(new RadListBoxItem(strText.ToString().Trim(), strid.ToString()));
                        }
                        BusinessTier.DisposeReader(reader);

                        SqlCommand cmd11 = new SqlCommand("select IntervalNo,IntervalByMonthYear,functional,physical FROM Vw_CRM_ReceiveEquipmentTransDetails WHERE RECEIVED_TRANS_DETAIL_ID=" + Convert.ToInt32(txtJob.ToolTip.ToString()) + " and Deleted=0 and STATUS <> 'COMPLETED'", conn);
                        SqlDataReader readerAdd1 = cmd11.ExecuteReader();
                        if (readerAdd1.Read())
                        {

                            if (!(string.IsNullOrEmpty(readerAdd1["IntervalNo"].ToString().Trim())))
                                cboIntervalNo.SelectedItem.Text = readerAdd1["IntervalNo"].ToString().Trim();
                            if (!(string.IsNullOrEmpty(readerAdd1["IntervalByMonthYear"].ToString().Trim())))
                                cboIntervalTime.SelectedItem.Text = readerAdd1["IntervalByMonthYear"].ToString().Trim();
                        }
                        BusinessTier.DisposeReader(readerAdd1);

                    }
                    else
                    {
                        ShowMessage(208);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            ShowMessage(ex.Message.ToString(), "Red");
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_Child", "RadGridJob_OnItemDataBound", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    public string jbno = string.Empty, rnno = string.Empty;

    protected void RadGridJob_OnItemDataBound(object source, Telerik.Web.UI.GridItemEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                GridHeaderItem headerItem = RadGridJob.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                string strTransDetailId = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RECEIVED_TRANS_DETAIL_ID"]).ToString();
                RadButton btnJOBNO = (RadButton)e.Item.FindControl("btnJOBNO");
                Label lblRECEIVED_TRANS_ID = (Label)e.Item.FindControl("lblRECEIVED_TRANS_ID");
                Label lblSerielNo = (Label)e.Item.FindControl("lblSerielNo");
                Label lblRunningNo = (Label)e.Item.FindControl("lblRunningNo");
                txtSerielNo.Text = lblSerielNo.Text.ToString().Trim();
                txtSerielNo.ToolTip = lblSerielNo.Text.ToString().Trim();
                jbno = btnJOBNO.Text.ToString().Trim();
                rnno = lblRunningNo.Text.ToString().Trim();
                btnJOBNO.Text = btnJOBNO.Text.ToString().Trim() + "-" + lblRunningNo.Text.ToString().Trim();
            }

        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message.ToString();
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_Child", "RadGridJob_OnItemDataBound", ex.ToString(), "Audit");
        }
    }


    protected void chkDiscripency_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkDiscripency.Checked)
        {
            txtRemarks.Visible = true;
            lblDiscrepancy.Visible = true;

        }
    }

    protected void btnReport_OnClick(object sender, EventArgs e)
    {
        //e.Item.Selected = true;
        //GridEditFormItem editedItem = e.Item as GridEditFormItem;
        //Label lblQuoID = (Label)e.Item.FindControl("lblQuoID");
        //string strTakenId = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Quotation_ID"]).ToString();
        if (string.IsNullOrEmpty(txtJob.Text.ToString()))
        {
            lblStatus.Text = "Please select Jobno";
        }
        else
        {
            jbno = txtJob.Text.ToString().Trim().Split('-')[0] ;

            rnno = txtphysical.ToolTip.ToString();

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_RegisterSticker.aspx?param1=" + jbno.ToString().Trim() + "&param2=" + rnno.ToString().Trim() + "');", true);
        }
    }

    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            if (lblContQuote1.Text.ToString().Trim() == "Quotation :")
            {
                if ((string.IsNullOrEmpty(txtJob.ToolTip.ToString())))
                {
                    ShowMessage(208);
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtSerielNo.Text.ToString().Trim()))
                    {
                        ShowMessage(211);
                        return;
                    }
                }
            }
            if (lblContQuote1.Text.ToString().Trim() == "Contract :")
            {
                if (btnReceivedQty.ToolTip.ToString().Trim() == "Q")
                {
                    if ((string.IsNullOrEmpty(txtJob.ToolTip.ToString())))
                    {
                        ShowMessage(208);
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtSerielNo.Text.ToString().Trim()))
                        {
                            ShowMessage(211);
                            return;
                        }
                    }
                }
            }
            if (!(string.IsNullOrEmpty(txtJob.ToolTip.ToString())))
            {
                if ((string.IsNullOrEmpty(txtAcessories.Text.ToString().Trim())))
                {
                    ShowMessage("Accessories Can Not Empty", "Red");
                    return;
                }

                if (ChkLab1.Checked != true) { lbllab1.Text = "0"; }
                if (ChkLab2.Checked != true) { lbllab2.Text = "0"; }
                if (ChkLab3.Checked != true) { lbllab3.Text = "0"; }
                if (ChkLab4.Checked != true) { lbllab4.Text = "0"; }
                if (ChkLab5.Checked != true) { lbllab5.Text = "0"; }
                if (ChkLab6.Checked != true) { lbllab6.Text = "0"; }
                if (ChkLab7.Checked != true) { lbllab7.Text = "0"; }
                if (ChkLab8.Checked != true) { lbllab8.Text = "0"; }
                if (ChkLab9.Checked != true) { lbllab9.Text = "0"; }
                if (ChkLab10.Checked != true) { lbllab10.Text = "0"; }

                string strStatus = "";
                if (lblContQuote1.Text.ToString().Trim() == "Contract :")
                    strStatus = "Contr";
                if (lblContQuote1.Text.ToString().Trim() == "Quotation :")
                    strStatus = "Quote";
                int intQuoteID = 0;
                int intContID = 0;
                int intCustId = 0;
                if ((Convert.ToInt32(lblCustID.Text.ToString().Trim())) <= 0)
                {
                    ShowMessage(216);
                    return;
                }
                else
                    intCustId = Convert.ToInt32(lblCustID.Text.ToString().Trim());

                if ((Convert.ToInt32(lblContQuoteID.Text.ToString().Trim())) <= 0)
                {
                    ShowMessage(218);
                    return;
                }
                if (strStatus.ToString().Trim() == "Quote")
                    intQuoteID = Convert.ToInt32(lblContQuoteID.Text.ToString().Trim());

                if (strStatus.ToString().Trim() == "Contr")
                    intContID = Convert.ToInt32(lblContQuoteID.Text.ToString().Trim());

                conn.Open();

                int intID = 0;
                if ((lblContQuote1.Text.ToString().Trim() == "Quotation :") || (btnReceivedQty.ToolTip.ToString().Trim() == "Q"))
                {
                    if (txtSerielNo.Text.ToString().Trim() != txtSerielNo.ToolTip.ToString().Trim())
                    {
                        SqlCommand cmd_GetDetailID = new SqlCommand("select RECEIVED_TRANS_DETAIL_ID from CRM_ReceiveEquipment_Trans_Details where SERIEL_NO='" + txtSerielNo.Text.ToString() + "' and ((Calibration <> 'COMPLETE') or (Calibration is null)) and BRANCH_ID=" + Convert.ToInt32(Session["sesBranchID"].ToString().Trim()) + " and Deleted=0 and STATUS <> 'YES' and OrderNo = 0", conn);//and JOBNO='" + txtJob.Text.ToString().Trim() + "'
                        SqlDataReader reader_GetDetailID = cmd_GetDetailID.ExecuteReader();
                        if (reader_GetDetailID.Read())
                        {
                            if (!(string.IsNullOrEmpty(reader_GetDetailID["RECEIVED_TRANS_DETAIL_ID"].ToString().Trim())))
                            {
                                intID = Convert.ToInt32(reader_GetDetailID["RECEIVED_TRANS_DETAIL_ID"].ToString().Trim());
                            }
                        }
                        BusinessTier.DisposeReader(reader_GetDetailID);

                        //if (intID < 0)
                        //{
                        //    ShowMessage("Access Denied, Please save the serial number first", "Red");
                        //    return;
                        //}
                        //if (intID != (Convert.ToInt32(txtJob.ToolTip.ToString())))
                        //{
                        //    ShowMessage("Access Denied, Please save the serial number first", "Red");
                        //    return;
                        //}
                    }
                }
                int intDiscripency = 0;
                if (chkDiscripency.Checked)
                    intDiscripency = 1;

                string strCurrDateTime = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
                string strFileName = "";
                //if (RadAsyncUpload1.UploadedFiles.Count > 0)
                //{
                //    foreach (UploadedFile f in RadAsyncUpload1.UploadedFiles)
                //    {
                //        string strPah = WebConfigurationManager.AppSettings["WC_ServiceNotePath"].ToString();
                //        string strType = f.GetName().Split('.')[1].ToString();
                //        string strName = f.GetName().Split('.')[0].ToString();
                //        if (strName.ToString().Trim().Length > 10)
                //        {
                //            f.SaveAs(strPah.ToString() + strCurrDateTime + "_" + f.GetName().ToString().Substring(0, 10).ToString().Trim() + "." + strType.Trim(), true);
                //            strFileName = strCurrDateTime + "_" + f.GetName().ToString().Substring(0, 10).ToString().Trim() + "." + strType.Trim();
                //        }
                //        else
                //        {
                //            f.SaveAs(strPah.ToString() + strCurrDateTime + "_" + f.GetName().ToString().Trim(), true);
                //            strFileName = strCurrDateTime + "_" + f.GetName().ToString().Trim();
                //        }
                //    }
                //}
                //if (string.IsNullOrEmpty(strFileName))
                //    strFileName = linkDownload.Text;

                DateTime dtNextRenewalOn = DateTime.Now;
                if (cboIntervalTime.Text.ToString().Trim() == "Month")
                {
                    int intDays = (Convert.ToInt32(cboIntervalNo.Text.ToString().Trim()) * 30);
                    dtNextRenewalOn = DateTime.Now.AddDays(Convert.ToDouble(intDays));
                }
                if (cboIntervalTime.Text.ToString().Trim() == "Year")
                {
                    int intDays = (Convert.ToInt32(cboIntervalNo.Text.ToString().Trim()) * 364);
                    dtNextRenewalOn = DateTime.Now.AddDays(Convert.ToDouble(intDays));
                }
                int flg = 0;
                int intRunningNo = 0;
                intRunningNo = Convert.ToInt32(txtJob.Text.ToString().Trim().Split('/')[1].ToString());
                if (listAccessories.Items.Count <= 0)
                    flg = BusinessTier_CRM.CRM_ReceivingEquipment_TransDetails(conn, Convert.ToInt32(txtJob.ToolTip.ToString()), txtAcessories.Text.ToString().Trim(), txtAccesSeriel.Text.ToString().Trim(), txtSerielNo.Text.ToString().Trim(), Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()), txtJob.Text.ToString().Trim().Split('-')[0].ToString() + "-" + txtJob.Text.ToString().Trim().Split('-')[1].ToString(), 0, (Convert.ToInt32(lblContQuote1.ToolTip.ToString())), Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), Session["sesGuid"].ToString().Trim(), Convert.ToDateTime(txtECDDate.SelectedDate), 1, intDiscripency, txtRemarks.Text.ToString().Trim(), strFileName.Trim(), "Update", intRunningNo, Convert.ToInt32(cboIntervalNo.Text.ToString().Trim()), cboIntervalTime.Text.ToString().Trim(), txtphysical.Text.ToString().Trim(), txtfuctional.Text.ToString().Trim());
                else
                    flg = BusinessTier_CRM.CRM_ReceivingEquipment_TransDetails(conn, Convert.ToInt32(txtJob.ToolTip.ToString()), txtAcessories.Text.ToString().Trim(), txtAccesSeriel.Text.ToString().Trim(), txtSerielNo.Text.ToString().Trim(), Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()), txtJob.Text.ToString().Trim().Split('-')[0].ToString() + "-" + txtJob.Text.ToString().Trim().Split('-')[1].ToString(), 0, (Convert.ToInt32(lblContQuote1.ToolTip.ToString())), Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), Session["sesGuid"].ToString().Trim(), Convert.ToDateTime(txtECDDate.SelectedDate), listAccessories.Items.Count + 1, intDiscripency, txtRemarks.Text.ToString().Trim(), strFileName.Trim(), "InsertAccess", intRunningNo, Convert.ToInt32(cboIntervalNo.Text.ToString().Trim()), cboIntervalTime.Text.ToString().Trim(),  txtphysical.Text.ToString().Trim(), txtfuctional.Text.ToString().Trim());

                // int flg1 = BusinessTier_CRM.CRM_ReceivingEquipment_TransLab(conn, Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()), (Convert.ToInt32(lblContQuote1.ToolTip.ToString())), Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), Session["sesGuid"].ToString().Trim(), Convert.ToDateTime(txtECDDate.SelectedDate), txtRemarks.Text.ToString().Trim(), txtBillingAdd.Text.ToString().Trim(), txtDeliveryAdd.Text.ToString().Trim(), txtCertAddress.Text.ToString().Trim(), strFileName.Trim(), Convert.ToInt32(lbllab1.Text.ToString().Trim()), Convert.ToInt32(lbllab2.Text.ToString().Trim()), Convert.ToInt32(lbllab3.Text.ToString().Trim()), Convert.ToInt32(lbllab4.Text.ToString().Trim()), Convert.ToInt32(lbllab5.Text.ToString().Trim()), Convert.ToInt32(lbllab6.Text.ToString().Trim()), Convert.ToInt32(lbllab7.Text.ToString().Trim()), Convert.ToInt32(lbllab8.Text.ToString().Trim()), Convert.ToInt32(lbllab9.Text.ToString().Trim()), Convert.ToInt32(lbllab10.Text.ToString().Trim()), txtBilContactName.Text.ToString().Trim(), txtBillContactNo.Text.ToString().Trim(), txtBillContactMail.Text.ToString().Trim(), txtDelContactName.Text.ToString().Trim(), txtDelContactNo.Text.ToString().Trim(), txtDelContactMail.Text.ToString().Trim(), txtCertContactName.Text.ToString().Trim(), txtCertContactNo.Text.ToString().Trim(), txtCertContactMail.Text.ToString().Trim(), Convert.ToInt32(cboIntervalNo.Text.ToString().Trim()), cboIntervalTime.Text.ToString().Trim(), dtNextRenewalOn, intContID, intQuoteID, intCustId, "TransOnly");

                //if ((flg1 > 0) && (flg == 0))
                //    ShowMessage("Successfully Saved Contact and Lab Details", "Yellow");
                if ((flg > 0)) //(flg1 > 0) && 
                    ShowMessage("Successfully Saved The Details", "Yellow");

                listAccessories.Items.Clear();
                int intOrderNo = 0;

                string strAccessList = "";
                string strAccessSerielList = "";
                SqlCommand cmd1 = new SqlCommand("select RECEIVED_TRANS_DETAIL_ID,ACCESSORIES,ACCESSORIES_SERIEL,OrderNo from CRM_ReceiveEquipment_Trans_Details where AccessMasterID = " + Convert.ToInt32(txtJob.ToolTip.ToString()) + " and BRANCH_ID=" + Convert.ToInt32(Session["sesBranchID"].ToString().Trim()) + " and Deleted=0 and OrderNo >= 1", conn);//and JOBNO='" + txtJob.Text.ToString().Trim() + "'
                SqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    intOrderNo = intOrderNo + 1; //= Convert.ToInt32(reader["OrderNo"].ToString()) - 1;
                    string strText = intOrderNo.ToString() + " |" + reader["ACCESSORIES_SERIEL"].ToString().Trim() + " | " + reader["ACCESSORIES"].ToString().Trim();
                    string strid = (reader["RECEIVED_TRANS_DETAIL_ID"].ToString());
                    listAccessories.Items.Add(new RadListBoxItem(strText.ToString().Trim(), strid.ToString()));
                }
                BusinessTier.DisposeReader(reader);

                intOrderNo = 0;
                SqlCommand cmd_UpdateAcc = new SqlCommand("select RECEIVED_TRANS_DETAIL_ID,ACCESSORIES,ACCESSORIES_SERIEL,OrderNo from CRM_ReceiveEquipment_Trans_Details where AccessMasterID = " + Convert.ToInt32(txtJob.ToolTip.ToString()) + " and BRANCH_ID=" + Convert.ToInt32(Session["sesBranchID"].ToString().Trim()) + " and Deleted=0 and OrderNo >= 1", conn);//and JOBNO='" + txtJob.Text.ToString().Trim() + "'
                SqlDataReader reader_UpdateAcc = cmd_UpdateAcc.ExecuteReader();
                while (reader_UpdateAcc.Read())
                {
                    intOrderNo = intOrderNo + 1; //= Convert.ToInt32(reader["OrderNo"].ToString()) - 1;
                    if (intOrderNo == 1)
                    {
                        strAccessList = reader_UpdateAcc["ACCESSORIES"].ToString().Trim();
                        strAccessSerielList = reader_UpdateAcc["ACCESSORIES_SERIEL"].ToString().Trim();
                    }
                    else
                    {
                        strAccessList = strAccessList + "," + reader_UpdateAcc["ACCESSORIES"].ToString().Trim();
                        strAccessSerielList = strAccessSerielList + "," + reader_UpdateAcc["ACCESSORIES_SERIEL"].ToString().Trim();
                    }
                }
                BusinessTier.DisposeReader(reader_UpdateAcc);



                if (string.IsNullOrEmpty(strAccessList.Trim()))
                {
                    strAccessList = "None";
                    strAccessSerielList = "None";
                }
                int flg2 = BusinessTier_CRM.CRM_ReceivingEquipmentTrans_UpdateAcces(conn, (Convert.ToInt32(lblContQuote1.ToolTip.ToString())), Convert.ToInt32(txtJob.ToolTip.ToString()), Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), strAccessList.Trim(), strAccessSerielList.Trim(), "btnAdd");

                //RadGridJob.DataSource = new string[] { };
                //RadGridJob.DataSource = DataSourceHelper("RECEIVEDTRANS", (Convert.ToInt32(lblContQuote1.ToolTip.ToString())));
                //RadGridJob.Rebind();
                RadAsyncUpload1.Visible = false;
                chkLink.Checked = true;
                chkLink.Visible = true;
                linkDownload.Visible = true;
                linkDownload.Text = strFileName.Trim();
                //string strText = intRowCount.ToString() + " |" + txtAccesSeriel.Text.ToString().Trim() + " | " + txtAcessories.Text.ToString().Trim();
                //listAccessories.Items.Add(new RadListBoxItem(strText.Trim(), "1"));

            }
            else
            {
                ShowMessage(208);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message.ToString(), "Red");
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_Child", "btnAdd_OnClick", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void btnDelete_OnClick(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            string strSelectedItem = listAccessories.SelectedValue.ToString().Trim();
            if (string.IsNullOrEmpty(strSelectedItem.Trim()))
            {
                ShowMessage(212);
                return;
            }
            if (!(string.IsNullOrEmpty(txtJob.ToolTip.ToString())))
            {
                conn.Open();
                int flg = BusinessTier_CRM.CRM_ReceivingEquipment_TransDetails(conn, Convert.ToInt32(strSelectedItem.ToString().Trim()), "", "", "", 0, "", 0, (Convert.ToInt32(lblContQuote1.ToolTip.ToString())), Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), Session["sesGuid"].ToString().Trim(), DateTime.Now, 0, 0, txtRemarks.Text.ToString().Trim(), "", "Delete", 0, 0, "", txtphysical.Text.ToString().Trim(), txtfuctional.Text.ToString().Trim());
                listAccessories.Items.Clear();
                int intOrderNo = 0;

                DataTable dtAccessories = new DataTable();
                SqlCommand cmd1 = new SqlCommand("select RECEIVED_TRANS_DETAIL_ID,ACCESSORIES,ACCESSORIES_SERIEL,OrderNo from CRM_ReceiveEquipment_Trans_Details where RECEIVED_TRANS_ID='" + (Convert.ToInt32(lblContQuote1.ToolTip.ToString())) + "' and BRANCH_ID=" + Convert.ToInt32(Session["sesBranchID"].ToString().Trim()) + " and Deleted=0 and OrderNo >= 1", conn);//and JOBNO='" + txtJob.Text.ToString().Trim() + "'
                SqlDataReader reader = cmd1.ExecuteReader();
                dtAccessories.Load(reader);
                BusinessTier.DisposeReader(reader);
                for (int i = 0; i < dtAccessories.Rows.Count; i++)
                {
                    intOrderNo = intOrderNo + 1; //= Convert.ToInt32(reader["OrderNo"].ToString()) - 1;
                    string strText = intOrderNo.ToString() + " |" + dtAccessories.Rows[i][2].ToString().Trim() + " | " + dtAccessories.Rows[i][1].ToString().Trim();
                    string strId = dtAccessories.Rows[i][0].ToString().Trim();

                    listAccessories.Items.Add(new RadListBoxItem(strText.ToString().Trim(), strId.ToString()));

                    int flg1 = BusinessTier_CRM.CRM_ReceivingEquipment_TransDetails(conn, Convert.ToInt32(strId.ToString().Trim()), "", "", "", 0, "", 0, (Convert.ToInt32(lblContQuote1.ToolTip.ToString())), Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), Session["sesGuid"].ToString().Trim(), DateTime.Now, intOrderNo, 0, "", "", "ReOrder", 0, 0, "", txtphysical.Text.ToString().Trim(), txtfuctional.Text.ToString().Trim());
                }
            }
            else
            {
                ShowMessage(208);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message.ToString(), "Red");
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_Child", "btnDelete_OnClick", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnSave_Onclick(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            //if (lblContQuote1.Text.ToString().Trim() == "Quotation :")
            //{
            //    if ((string.IsNullOrEmpty(txtJob.ToolTip.ToString())))
            //    {
            //        ShowMessage(208);
            //        return;
            //    }
            //    else
            //    {
            //        if (string.IsNullOrEmpty(txtSerielNo.Text.ToString().Trim()))
            //        {
            //            ShowMessage(211);
            //            return;
            //        }
            //    }
            //}
            //if (lblContQuote1.Text.ToString().Trim() == "Contract :")
            //{
            //    if (btnReceivedQty.ToolTip.ToString().Trim() == "Q")
            //    {
            //        if ((string.IsNullOrEmpty(txtJob.ToolTip.ToString())))
            //        {
            //            ShowMessage(208);
            //            return;
            //        }
            //        else
            //        {
            //            if (string.IsNullOrEmpty(txtSerielNo.Text.ToString().Trim()))
            //            {
            //                ShowMessage(211);
            //                return;
            //            }
            //        }
            //    }
            //}
            if ((string.IsNullOrEmpty(lblContQuote1.ToolTip.ToString())))
            {
                ShowMessage("ReceivedTransId Does Not Exist To Proceed, Pease Contact Admin", "Red");
                return;
            }

            int nullLabFlag = 0;
            if (ChkLab1.Checked == true) { nullLabFlag = 1; }
            if (ChkLab2.Checked == true) { nullLabFlag = 1; }
            if (ChkLab3.Checked == true) { nullLabFlag = 1; }
            if (ChkLab4.Checked == true) { nullLabFlag = 1; }
            if (ChkLab5.Checked == true) { nullLabFlag = 1; }
            if (ChkLab6.Checked == true) { nullLabFlag = 1; }
            if (ChkLab7.Checked == true) { nullLabFlag = 1; }
            if (ChkLab8.Checked == true) { nullLabFlag = 1; }
            if (ChkLab9.Checked == true) { nullLabFlag = 1; }
            if (ChkLab10.Checked == true) { nullLabFlag = 1; }

            if (nullLabFlag == 1)
            {
                ShowMessage("Please Click The Performing Lab", "Red");
                return;
            }

            if (ChkLab1.Checked != true) { lbllab1.Text = "0"; }
            if (ChkLab2.Checked != true) { lbllab2.Text = "0"; }
            if (ChkLab3.Checked != true) { lbllab3.Text = "0"; }
            if (ChkLab4.Checked != true) { lbllab4.Text = "0"; }
            if (ChkLab5.Checked != true) { lbllab5.Text = "0"; }
            if (ChkLab6.Checked != true) { lbllab6.Text = "0"; }
            if (ChkLab7.Checked != true) { lbllab7.Text = "0"; }
            if (ChkLab8.Checked != true) { lbllab8.Text = "0"; }
            if (ChkLab9.Checked != true) { lbllab9.Text = "0"; }
            if (ChkLab10.Checked != true) { lbllab10.Text = "0"; }

            string strStatus = "";
            //if (lblContQuote1.Text.ToString().Trim() == "Contract :")
            //    strStatus = "Contr";
            if (lblContQuote1.Text.ToString().Trim() == "Quotation :")
                strStatus = "Quote";
            int intQuoteID = 0;
            int intContID = 0;
            int intCustId = 0;
            if (strStatus.ToString().Trim() == "Quote")
                intQuoteID = Convert.ToInt32(lblContQuoteID.Text.ToString().Trim());

            //if (strStatus.ToString().Trim() == "Contr")
            //    intContID = Convert.ToInt32(lblContQuoteID.Text.ToString().Trim());

            if (!(string.IsNullOrEmpty(lblContQuote1.ToolTip.ToString())))
            {
                conn.Open();

                if ((Convert.ToInt32(lblCustID.Text.ToString().Trim())) <= 0)
                {
                    ShowMessage(216);
                    return;
                }
                else
                    intCustId = Convert.ToInt32(lblCustID.Text.ToString().Trim());


                if ((Convert.ToInt32(lblContQuoteID.Text.ToString().Trim())) <= 0)
                {
                    ShowMessage(218);
                    return;
                }
                int intDiscripency = 0;
                if (chkDiscripency.Checked)
                    intDiscripency = 1;

                string strFileName = "";
                string strCurrDateTime = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();

                if (RadAsyncUpload1.UploadedFiles.Count > 0)
                {
                    foreach (UploadedFile f in RadAsyncUpload1.UploadedFiles)
                    {
                        string strPah = WebConfigurationManager.AppSettings["WC_ServiceNotePath"].ToString();
                        string strType = f.GetName().Split('.')[1].ToString();
                        string strName = f.GetName().Split('.')[0].ToString();
                        if (strName.ToString().Trim().Length > 10)
                        {

                            f.SaveAs(strPah.ToString() + strCurrDateTime + "_" + f.GetName().ToString().Substring(0, 10).ToString().Trim() + "." + strType.Trim(), true);
                            strFileName = strCurrDateTime + "_" + f.GetName().ToString().Substring(0, 10).ToString().Trim() + "." + strType.Trim();
                        }
                        else
                        {
                            f.SaveAs(strPah.ToString() + strCurrDateTime + "_" + f.GetName().ToString().Trim(), true);
                            strFileName = strCurrDateTime + "_" + f.GetName().ToString().Trim();
                        }
                    }
                }
                if ((string.IsNullOrEmpty(strFileName)))
                    strFileName = linkDownload.Text;

                int intDetailID = 0;
                if (!(string.IsNullOrEmpty(txtJob.ToolTip.ToString())))
                    intDetailID = Convert.ToInt32(txtJob.ToolTip.ToString());

                if (cboIntervalNo.Text.ToString().Trim() == "--Select--")
                {
                    ShowMessage("Please choose Interval No To Proceed", "Red");
                    return;
                }
                if (cboIntervalTime.Text.ToString().Trim() == "--Select--")
                {
                    ShowMessage("Please choose Interval By Months/Years To Proceed", "Red");
                    return;
                }

                DateTime dtNextRenewalOn = DateTime.Now;
                if (cboIntervalTime.Text.ToString().Trim() == "Month")
                {
                    if (cboIntervalNo.Text.ToString().Trim() != "0")
                    {
                        int intDays = (Convert.ToInt32(cboIntervalNo.Text.ToString().Trim()) * 30);
                        dtNextRenewalOn = DateTime.Now.AddDays(Convert.ToDouble(intDays));
                    }
                }
                if (cboIntervalTime.Text.ToString().Trim() == "Year")
                {
                    if (cboIntervalNo.Text.ToString().Trim() != "0")
                    {
                        int intDays = (Convert.ToInt32(cboIntervalNo.Text.ToString().Trim()) * 364);
                        dtNextRenewalOn = DateTime.Now.AddDays(Convert.ToDouble(intDays));
                    }
                }

                if ((lblContQuote1.Text.ToString().Trim() == "Quotation :") || (btnReceivedQty.ToolTip.ToString().Trim() == "Q"))
                {
                    if (txtSerielNo.Text.ToString().Trim() != txtSerielNo.ToolTip.ToString().Trim())
                    {
                        int intID = 0;//and STATUS <> 'YES'
                        SqlCommand cmd1 = new SqlCommand("select RECEIVED_TRANS_DETAIL_ID from CRM_ReceiveEquipment_Trans_Details where SERIEL_NO='" + txtSerielNo.Text.ToString() + "' and ((Calibration <> 'COMPLETE') or (Calibration is null)) and BRANCH_ID=" + Convert.ToInt32(Session["sesBranchID"].ToString().Trim()) + " and Deleted=0  and OrderNo = 0", conn);//and JOBNO='" + txtJob.Text.ToString().Trim() + "'
                        SqlDataReader reader = cmd1.ExecuteReader();
                        if (reader.Read())
                        {
                            if (!(string.IsNullOrEmpty(reader["RECEIVED_TRANS_DETAIL_ID"].ToString().Trim())))
                            {
                                intID = Convert.ToInt32(reader["RECEIVED_TRANS_DETAIL_ID"].ToString().Trim());
                            }
                        }
                        BusinessTier.DisposeReader(reader);
                        if (intID > 0)
                        {
                            ShowMessage("Access Denied, This serial Number Is Already Exist", "Red");
                            return;
                        }
                    }
                }
                //if (string.IsNullOrEmpty(txtphysical.Text.ToString()))
                //{
                //    lblStatus.Text = "Please Enter Physical Condition";
                //    return;
                //}
                //if (string.IsNullOrEmpty(txtfuctional.Text.ToString()))
                //{
                //    lblStatus.Text = "Please Enter Functional Condition";
                //    return;
                //}
                //string strsdsd= txtCertAddress.Text.ToString();


                int flg = 0;
                if (intDetailID > 0)
                {
                    flg = BusinessTier_CRM.CRM_ReceivingEquipment_TransDetails(conn, Convert.ToInt32(txtJob.ToolTip.ToString()), "", "", txtSerielNo.Text.ToString(), Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()), "", 0, (Convert.ToInt32(lblContQuote1.ToolTip.ToString())), Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), Session["sesGuid"].ToString().Trim(), Convert.ToDateTime(txtECDDate.SelectedDate), 1, intDiscripency, txtRemarks.Text.ToString().Trim(), strFileName.Trim(), "TransOnly", 0, Convert.ToInt32(cboIntervalNo.Text.ToString().Trim()), cboIntervalTime.Text.ToString().Trim(), txtphysical.Text.ToString().Trim(), txtfuctional.Text.ToString().Trim());
                }


                //  int flg1 = BusinessTier_CRM.CRM_ReceivingEquipment_TransLab(conn, Convert.ToInt32(lblEquip_ID.Text.ToString().Trim()), (Convert.ToInt32(lblContQuote1.ToolTip.ToString())), Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), Session["sesGuid"].ToString().Trim(), Convert.ToDateTime(txtECDDate.SelectedDate), txtRemarks.Text.ToString().Trim(), txtBillingAdd.Text.ToString().Trim(), txtDeliveryAdd.Text.ToString().Trim(), txtCertAddress.Text.ToString().Trim(), strFileName.Trim(), Convert.ToInt32(lbllab1.Text.ToString().Trim()), Convert.ToInt32(lbllab2.Text.ToString().Trim()), Convert.ToInt32(lbllab3.Text.ToString().Trim()), Convert.ToInt32(lbllab4.Text.ToString().Trim()), Convert.ToInt32(lbllab5.Text.ToString().Trim()), Convert.ToInt32(lbllab6.Text.ToString().Trim()), Convert.ToInt32(lbllab7.Text.ToString().Trim()), Convert.ToInt32(lbllab8.Text.ToString().Trim()), Convert.ToInt32(lbllab9.Text.ToString().Trim()), Convert.ToInt32(lbllab10.Text.ToString().Trim()), txtBilContactName.Text.ToString().Trim(), txtBillContactNo.Text.ToString().Trim(), txtBillContactMail.Text.ToString().Trim(), txtDelContactName.Text.ToString().Trim(), txtDelContactNo.Text.ToString().Trim(), txtDelContactMail.Text.ToString().Trim(), txtCertContactName.Text.ToString().Trim(), txtCertContactNo.Text.ToString().Trim(), txtCertContactMail.Text.ToString().Trim(), Convert.ToInt32(cboIntervalNo.Text.ToString().Trim()), cboIntervalTime.Text.ToString().Trim(), dtNextRenewalOn, intContID, intQuoteID, intCustId, "TransOnly");

                //if ((flg1 > 0) && (flg == 0))
                //    ShowMessage("Successfully Saved Contact and Lab Details", "Yellow");
                if ((flg > 0)) //(flg1 > 0) &&
                    ShowMessage("Successfully Saved The Details", "Yellow");


                //foreach (GridDataItem grdItem in RadGridJob.Items)
                //{
                //    RadButton btnJOBNO = (RadButton)grdItem.FindControl("btnJOBNO");
                //    Label lblRECEIVED_TRANS_ID = (Label)grdItem.FindControl("lblRECEIVED_TRANS_ID");
                //    Label lblSerielNo = (Label)grdItem.FindControl("lblSerielNo");
                //    Label lblRunningNo = (Label)grdItem.FindControl("lblRunningNo");

                //    if (!string.IsNullOrEmpty(lblRECEIVED_TRANS_ID.Text.ToString()))
                //    {
                //        //SqlCommand cmd1 = new SqlCommand("select RECEIVED_TRANS_DETAIL_ID,ACCESSORIES,ACCESSORIES_SERIEL,OrderNo from CRM_ReceiveEquipment_Trans_Details where RECEIVED_TRANS_ID='" + (Convert.ToInt32(lblContQuote1.ToolTip.ToString())) + "' and AccessMasterID=" + Convert.ToInt32(lblRECEIVED_TRANS_ID.Text.ToString()) + " and BRANCH_ID=" + Convert.ToInt32(Session["sesBranchID"].ToString().Trim()) + " and Deleted=0 and OrderNo >= 1", conn);//and JOBNO='" + txtJob.Text.ToString().Trim() + "'
                //        //SqlDataReader reader = cmd1.ExecuteReader();
                //        //while (reader.Read())
                //        //{
                //        //    intOrderNo = intOrderNo + 1; //= Convert.ToInt32(reader["OrderNo"].ToString()) - 1;
                //        //    string strText = intOrderNo.ToString() + " |" + reader["ACCESSORIES_SERIEL"].ToString().Trim() + " | " + reader["ACCESSORIES"].ToString().Trim();
                //        //    string strid = (reader["RECEIVED_TRANS_DETAIL_ID"].ToString());
                //        //    listAccessories.Items.Add(new RadListBoxItem(strText.ToString().Trim(), strid.ToString()));

                //        //    strAccessList = strAccessList + reader["ACCESSORIES"].ToString().Trim();
                //        //    strAccessSerielList = strAccessSerielList + reader["ACCESSORIES_SERIEL"].ToString().Trim();

                //        //}
                //        //BusinessTier.DisposeReader(reader);
                //    }
                //    int flg2 = BusinessTier_CRM.CRM_ReceivingEquipmentTrans_UpdateAcces(conn, (Convert.ToInt32(lblContQuote1.ToolTip.ToString())), Convert.ToInt32(txtJob.ToolTip.ToString()), Convert.ToInt32(Session["sesUserID"].ToString().Trim()), Convert.ToInt32(Session["sesBranchID"].ToString().Trim()), strAccessList.Trim(), strAccessSerielList.Trim(), "btnAdd");
                //}

                //LoadLabs(lblEquip_ID.Text.ToString().Trim());
                //int intlab1 = 0, intlab2 = 0, intlab3 = 0, intlab4 = 0, intlab5 = 0, intlab6 = 0, intlab7 = 0, intlab8 = 0, intlab9 = 0, intlab10 = 0;
                //string strFilePath = "";
                //SqlCommand command1 = new SqlCommand("select ECD,FILE_PATH,Lab1,Lab2,Lab3,Lab4,Lab5,Lab6,Lab7,Lab8,Lab9,Lab10 FROM CRM_ReceiveEquipment_Trans WHERE RECEIVED_TRANS_ID=" + Convert.ToInt32(lblContQuote1.ToolTip) + "", conn);
                //SqlDataReader readerAdd = command1.ExecuteReader();
                //if (readerAdd.Read())
                //{
                //    if (!(string.IsNullOrEmpty(readerAdd["FILE_PATH"].ToString().Trim())))
                //        strFilePath = readerAdd["FILE_PATH"].ToString().Trim();
                //    if (!(string.IsNullOrEmpty(readerAdd["ECD"].ToString().Trim())))
                //        txtECDDate.SelectedDate = (Convert.ToDateTime(readerAdd["ECD"].ToString().Trim()));
                //    else
                //        txtECDDate.SelectedDate = DateTime.Now;
                //    if (!(string.IsNullOrEmpty(readerAdd["IntervalNo"].ToString().Trim())))
                //        cboIntervalNo.SelectedItem.Text = readerAdd["IntervalNo"].ToString().Trim();
                //    if (!(string.IsNullOrEmpty(readerAdd["IntervalByMonthYear"].ToString().Trim())))
                //        cboIntervalTime.SelectedItem.Text = readerAdd["IntervalByMonthYear"].ToString().Trim();

                //    intlab1 = Convert.ToInt32(readerAdd["Lab1"].ToString());
                //    intlab2 = Convert.ToInt32(readerAdd["Lab2"].ToString());
                //    intlab3 = Convert.ToInt32(readerAdd["Lab3"].ToString());
                //    intlab4 = Convert.ToInt32(readerAdd["Lab4"].ToString());
                //    intlab5 = Convert.ToInt32(readerAdd["Lab5"].ToString());
                //    intlab6 = Convert.ToInt32(readerAdd["Lab6"].ToString());
                //    intlab7 = Convert.ToInt32(readerAdd["Lab7"].ToString());
                //    intlab8 = Convert.ToInt32(readerAdd["Lab8"].ToString());
                //    intlab9 = Convert.ToInt32(readerAdd["Lab9"].ToString());
                //    intlab10 = Convert.ToInt32(readerAdd["Lab10"].ToString());
                //}
                //BusinessTier.DisposeReader(readerAdd);

                //if (intlab1 == 0) { ChkLab1.Checked = false; } else { ChkLab1.Checked = true; }
                //if (intlab2 == 0) { ChkLab2.Checked = false; } else { ChkLab2.Checked = true; }
                //if (intlab3 == 0) { ChkLab3.Checked = false; } else { ChkLab3.Checked = true; }
                //if (intlab4 == 0) { ChkLab4.Checked = false; } else { ChkLab4.Checked = true; }
                //if (intlab5 == 0) { ChkLab5.Checked = false; } else { ChkLab5.Checked = true; }
                //if (intlab6 == 0) { ChkLab6.Checked = false; } else { ChkLab6.Checked = true; }
                //if (intlab7 == 0) { ChkLab7.Checked = false; } else { ChkLab7.Checked = true; }
                //if (intlab8 == 0) { ChkLab8.Checked = false; } else { ChkLab8.Checked = true; }
                //if (intlab9 == 0) { ChkLab9.Checked = false; } else { ChkLab9.Checked = true; }
                //if (intlab10 == 0) { ChkLab10.Checked = false; } else { ChkLab10.Checked = true; }

                if (!(string.IsNullOrEmpty(strFileName.ToString().Trim())))
                {
                    linkDownload.Text = strFileName.ToString().Trim();
                    linkDownload.Visible = true;
                    chkLink.Visible = true;
                    chkLink.Checked = true;
                    RadAsyncUpload1.Visible = false;
                }
                else
                {
                    linkDownload.Text = "";
                    linkDownload.Visible = false;
                    chkLink.Visible = false;
                    chkLink.Checked = false;
                    RadAsyncUpload1.Visible = true;
                }
                BusinessTier.DisposeConnection(conn);
                RadGridJob.DataSource = new string[] { };
                RadGridJob.DataSource = DataSourceHelper("RECEIVEDTRANS", (Convert.ToInt32(lblContQuote1.ToolTip.ToString())));
                RadGridJob.Rebind();

            }

            else
            {
                ShowMessage(210);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message.ToString(), "Red");
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_Child", "btnSave_OnClick", ex.ToString(), "Audit");

        }
        finally
        {
            BusinessTier.DisposeConnection(conn);

        }
    }


    protected void listAccessories_OnDeleted(object sender, RadListBoxEventArgs e)
    {
        RadListBox radlist = (RadListBox)sender;
        foreach (RadListBoxItem item in e.Items)
        {
            string str = item.Text;
        }
        string strDel = radlist.ID.ToString();
    }

    public void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
    {
        Label fileName = new Label();
        fileName.Text = e.File.FileName;
        //ValidFiles.Visible = true;
        //ValidFiles.Controls.Add(fileName);
        //Session["sesFiles"] = Session["sesFiles"] + "*" + fileName.Text.ToString();
    }

    protected void chkLink_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkLink.Checked == false)
        {
            linkDownload.Visible = false;
            linkDownload.Text = "";
            chkLink.Visible = false;
            chkLink.Checked = false;
            RadAsyncUpload1.Visible = true;
        }
    }

    protected void linkDownload_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (!(string.IsNullOrEmpty(linkDownload.Text.ToString().Trim())))
            {
                string strPah = WebConfigurationManager.AppSettings["WC_ServiceNoteGetPath"].ToString();
                //string strLink = "http://localhost/SirimNew/ServiceNote/" + linkDownload.Text.Trim();
                string strLink = strPah + linkDownload.Text.Trim();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenNewTab", "window.open('" + strLink + "','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=800,height=1200');", true);
            }
        }
        catch (Exception Ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btnGenerate_OnClick(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            int intDetailID = 0;
            if (!(string.IsNullOrEmpty(txtJob.ToolTip.ToString())))
                intDetailID = Convert.ToInt32(txtJob.ToolTip.ToString());

            if ((string.IsNullOrEmpty(lblContQuote1.ToolTip.ToString())))
            {
                ShowMessage("ReceivedTransId Does Not Exist To Proceed, Pease Contact Admin", "Red");
                return;
            }
            if (intDetailID <= 0)
            {
                ShowMessage("Please Choose The Job Number Before Create ID Number", "Red");
                return;
            }
            DateTime CurrDateTime = DateTime.Now;
            string strCurrYear = CurrDateTime.Year.ToString().Trim();
            string strDateFrom = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + " 12:00:00 AM";
            string strDateTo = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + " 11:59:59 PM";
            conn.Open();

            string strBranchCode = Session["sesBranchCode"].ToString().Trim();

            int intgetID = 0;
            int intgetAutoNo = 0;
            string strgetYear = "0";
            string sql = "select * FROM CRM_AutoSerialID WHERE BranchId = '" + Session["sesBranchID"].ToString().Trim() + "' and Year_Val = '" + strCurrYear.ToString().Trim() + "'";
            SqlCommand command = new SqlCommand(sql, conn);
            SqlDataReader readergetID = command.ExecuteReader();
            if (readergetID.Read())
            {
                if (!(string.IsNullOrEmpty(readergetID["AutoId"].ToString().Trim())))
                    intgetID = Convert.ToInt32(readergetID["AutoId"].ToString().Trim());

                if (!(string.IsNullOrEmpty(readergetID["AutoNo"].ToString().Trim())))
                    intgetAutoNo = Convert.ToInt32(readergetID["AutoNo"].ToString().Trim());

                strgetYear = readergetID["Year_Val"].ToString().Trim();
            }
            BusinessTier.DisposeReader(readergetID);

            //---------------------------------------------------------------------------------------------------
            Int32 intAutono = Int32.Parse(intgetAutoNo.ToString().Trim());
            Int32 intAutoNoInc = 0;
            string strgeneratingIDNo = "";
            if ((intgetID == 0))
            {
                intAutoNoInc = 1;
                strgeneratingIDNo = strBranchCode.ToString().Trim() + strCurrYear.ToString().Trim() + "-ID-" + intAutoNoInc.ToString().Trim();
                txtSerielNo.Text = strgeneratingIDNo.Trim();
                SaveAutoSerialIDTable(Session["sesBranchID"].ToString().Trim(), intAutoNoInc.ToString().Trim(), strCurrYear.ToString().Trim(), "Insert");
            }
            if ((intgetID > 0))
            {
                intAutoNoInc = intAutono + 1;
                strgeneratingIDNo = strBranchCode.ToString().Trim() + strCurrYear.ToString().Trim() + "-ID-" + intAutoNoInc.ToString();
                txtSerielNo.Text = strgeneratingIDNo.Trim();
                SaveAutoSerialIDTable(Session["sesBranchID"].ToString().Trim(), intAutoNoInc.ToString().Trim(), strCurrYear.ToString().Trim(), "Update");
            }


        }
        catch (Exception ex)
        {
            lblStatus.Text = "Method:generateIDNo: " + ex.Message.ToString();
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "CRM_ReceivingEquipment_IH_Child", "generateID", ex.ToString(), "Audit");

        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    private void SaveAutoSerialIDTable(string strBranchId, string strAutoNo, string strYear, string saveFlag)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        int flagMrvAuto = BusinessTier_CRM.saveSerialIDAuto(conn, strBranchId.ToString().Trim(), strAutoNo.ToString().Trim(), strYear.ToString().Trim(), saveFlag.ToString().Trim());
        BusinessTier.DisposeConnection(conn);
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