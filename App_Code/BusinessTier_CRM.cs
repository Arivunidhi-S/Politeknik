using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Net;

using System.Globalization;
using System.Collections;
using System.Data.OleDb;
using System.Drawing;

/// <summary>
/// Summary description for BusinessTier_CRM
/// </summary>
public class BusinessTier_CRM
{
    public static DataTable g_ErrorMessagesDataTable_CRM;

    public BusinessTier_CRM()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static SqlDataReader GetCustomerIDbyName(SqlConnection conn, string strCustomer, int strCustID)
    {
        SqlCommand cmd = new SqlCommand("[sp_CRM_MasterCustomer_GetIDByCustName]", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Customernamep", strCustomer);
        cmd.Parameters.AddWithValue("@CustIDp", strCustID);
        SqlDataReader reader1 = cmd.ExecuteReader();
        return reader1;
    }

    public static SqlDataReader GetContractIDbyNo(SqlConnection conn, string strContractName, int strCustID)
    {
        SqlCommand cmd = new SqlCommand("[sp_CRM_MasterContract_GetIDByContName]", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Contractnamep", strContractName.Trim());
        cmd.Parameters.AddWithValue("@CustIDp", strCustID);
        SqlDataReader reader1 = cmd.ExecuteReader();
        return reader1;
    }

    public static SqlDataReader GetQuoteIDbyNo(SqlConnection conn, string strQuoteNo, int strCustID)
    {
        SqlCommand cmd = new SqlCommand("[sp_CRM_Quotation_GetIDByQuote]", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@QuoteNop", strQuoteNo.Trim());
        cmd.Parameters.AddWithValue("@CustIDp", strCustID);
        SqlDataReader reader1 = cmd.ExecuteReader();
        return reader1;
    }

    public static int CRM_SaveReceivingPO(SqlConnection conn, int intID, string strCustId, int intQuoteId, string strQuoteNo, int intContractId, string strContractNo, string strPO, DateTime PODate, string strAmount, string strComments, string strFileName, int intUserID, int intBranchID, string strFlag, string strBatch)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_ReceivePO_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@POIdp", intID);
        dCmd.Parameters.AddWithValue("@CustIDp", strCustId);
        dCmd.Parameters.AddWithValue("@QuoteIDp", intQuoteId);
        dCmd.Parameters.AddWithValue("@Quotation", strQuoteNo);
        dCmd.Parameters.AddWithValue("@ContractId", intContractId);
        dCmd.Parameters.AddWithValue("@Contract", strContractNo);
        dCmd.Parameters.AddWithValue("@PODate", PODate);
        dCmd.Parameters.AddWithValue("@PONo", strPO);
        dCmd.Parameters.AddWithValue("@POAmount", strAmount);
        dCmd.Parameters.AddWithValue("@Comments", strComments);
        dCmd.Parameters.AddWithValue("@FileName", strFileName);
        dCmd.Parameters.AddWithValue("@BranchID", intBranchID);
        dCmd.Parameters.AddWithValue("@UserId", intUserID);
        dCmd.Parameters.AddWithValue("@flagp", strFlag);

        dCmd.Parameters.AddWithValue("@strBatchp", strBatch.Trim());
        return dCmd.ExecuteNonQuery();
    }


    public static void BindErrorMessageDetails(SqlConnection connError)
    {
        string sql = "select * FROM MasterErrorMessage where orderno >= 201 order by OrderNo";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, connError);
        g_ErrorMessagesDataTable_CRM = new DataTable();
        sqlDataAdapter.Fill(g_ErrorMessagesDataTable_CRM);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
    }

    public static int saveJobAuto(SqlConnection connMRVAuto, string strBranchId, string strAutoNo, string strYear, string strLastAutoNo, string saveFlag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_AutoJob_Save]", connMRVAuto);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@BranchIDp", strBranchId);
        dCmd.Parameters.AddWithValue("@strAutonop", strAutoNo);
        dCmd.Parameters.AddWithValue("@strYearp", strYear);
        dCmd.Parameters.AddWithValue("@strLastAutoNop", strLastAutoNo);
        dCmd.Parameters.AddWithValue("@saveflagp", saveFlag);
        return dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader getBranchInfo_ByID(SqlConnection conn, string strBranchID)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_MasterBranch_getBranchDetail]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@idp", strBranchID);
        SqlDataReader reader1 = dCmd.ExecuteReader();
        return reader1;
    }
    public static int CRM_ReceivingEquipment_Trans(SqlConnection conn, int ID, int intCustID, int intQuoteId, int intContEquipId, int intEquipID, int intQty, int intUserID, int intBranch, string strGuid, int intQuoteDetailID, int intContDetailId, string strPartialFull, string strStatus, string strCategory)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_ReceiveEqui_Trans_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter oParam = dCmd.Parameters.AddWithValue("@IDp", ID);
        oParam = dCmd.Parameters.AddWithValue("@custId", intCustID);
        oParam = dCmd.Parameters.AddWithValue("@QuoteId", intQuoteId);
        oParam = dCmd.Parameters.AddWithValue("@ContIDp", intContEquipId);
        oParam = dCmd.Parameters.AddWithValue("@intEquipIDp", intEquipID);

        oParam = dCmd.Parameters.AddWithValue("@Qtyp", intQty);
        oParam = dCmd.Parameters.AddWithValue("@UserIDp", intUserID);
        oParam = dCmd.Parameters.AddWithValue("@BranchIDp", intBranch);
        oParam = dCmd.Parameters.AddWithValue("@GUIDp", strGuid.Trim());
        oParam = dCmd.Parameters.AddWithValue("@flagp", strStatus.Trim());

        oParam = dCmd.Parameters.AddWithValue("@QuoteDetailIDp", intQuoteDetailID);
        oParam = dCmd.Parameters.AddWithValue("@ContDetailIDp", intContDetailId);
        oParam = dCmd.Parameters.AddWithValue("@PartialFullp", strPartialFull.Trim());
        oParam = dCmd.Parameters.AddWithValue("@strCategoryp", strCategory.Trim());

        oParam = dCmd.Parameters.AddWithValue("@ReceiveTransID", 0);
        oParam.Direction = ParameterDirection.Output;
        dCmd.ExecuteNonQuery();
        int nTheNewId = Convert.ToInt32(dCmd.Parameters["@ReceiveTransID"].Value);
        return nTheNewId;
    }

    public static int CRM_ReceivingEquipment_TransDetails(SqlConnection conn, int ID, string strAcces, string strAcces_Seriel, string strSerielNo, int intEquipID, string strJobNo, int intReceivedID, int intReceivedTransID, int intUserID, int intBranch, string strGuid, DateTime dtECD, int intOrder, int intDiscrip, string strRemarks, string strFileName, string strStatus, int intRunningNo, int intIntervalNo, string strIntervalTime, string physical, string  funtional)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_ReceiveEqui_TransDetail_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@IDp", ID);
        dCmd.Parameters.AddWithValue("@Accessp", strAcces.Trim());
        dCmd.Parameters.AddWithValue("@Acces_Serielp", strAcces_Seriel.Trim());
        dCmd.Parameters.AddWithValue("@SerielNop", strSerielNo.Trim());
        dCmd.Parameters.AddWithValue("@intEquipIDp", intEquipID);
        dCmd.Parameters.AddWithValue("@Remarksp", strRemarks.Trim());

        dCmd.Parameters.AddWithValue("@Jobnop", strJobNo.Trim());
        dCmd.Parameters.AddWithValue("@intReceivedIDp", intReceivedID);
        dCmd.Parameters.AddWithValue("@intReceivedTransIDp", intReceivedTransID);
        dCmd.Parameters.AddWithValue("@UserIDp", intUserID);
        dCmd.Parameters.AddWithValue("@BranchIDp", intBranch);
        dCmd.Parameters.AddWithValue("@FileName", strFileName.Trim());
        dCmd.Parameters.AddWithValue("@flagp", strStatus.Trim());

        dCmd.Parameters.AddWithValue("@Ecdp", dtECD);
        dCmd.Parameters.AddWithValue("@OrderNo", intOrder);
        dCmd.Parameters.AddWithValue("@discripency", intDiscrip);

        dCmd.Parameters.AddWithValue("@GUIDp", strGuid.Trim());
        dCmd.Parameters.AddWithValue("@RunningNop", intRunningNo);

        dCmd.Parameters.AddWithValue("@IntervalNop", intIntervalNo);
        dCmd.Parameters.AddWithValue("@ntervalTimep", strIntervalTime.Trim());
      
         dCmd.Parameters.AddWithValue("@physical", physical);
         dCmd.Parameters.AddWithValue("@funtional", funtional);

       
        return dCmd.ExecuteNonQuery();
    }

    public static int CRM_ReceivingEquipment_TransDetails_Onsite(SqlConnection conn, int ID, string strAcces, string strAcces_Seriel, string strSerielNo, int intEquipID, string strJobNo, int intReceivedID, int intReceivedTransID, int intUserID, int intBranch, string strGuid, DateTime dtECD, int intOrder, int intDiscrip, string strRemarks, string strFileName, string strStatus, int intRunningNo, int intIntervalNo, string strIntervalTime, DateTime NextRenewalOn)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_ReceiveEqui_TransDetail_Onsite_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@IDp", ID);
        dCmd.Parameters.AddWithValue("@Accessp", strAcces.Trim());
        dCmd.Parameters.AddWithValue("@Acces_Serielp", strAcces_Seriel.Trim());
        dCmd.Parameters.AddWithValue("@SerielNop", strSerielNo.Trim());
        dCmd.Parameters.AddWithValue("@intEquipIDp", intEquipID);
        dCmd.Parameters.AddWithValue("@Remarksp", strRemarks.Trim());

        dCmd.Parameters.AddWithValue("@Jobnop", strJobNo.Trim());
        dCmd.Parameters.AddWithValue("@intReceivedIDp", intReceivedID);
        dCmd.Parameters.AddWithValue("@intReceivedTransIDp", intReceivedTransID);
        dCmd.Parameters.AddWithValue("@UserIDp", intUserID);
        dCmd.Parameters.AddWithValue("@BranchIDp", intBranch);
        dCmd.Parameters.AddWithValue("@FileName", strFileName.Trim());
        dCmd.Parameters.AddWithValue("@flagp", strStatus.Trim());

        dCmd.Parameters.AddWithValue("@Ecdp", dtECD);
        dCmd.Parameters.AddWithValue("@OrderNo", intOrder);
        dCmd.Parameters.AddWithValue("@discripency", intDiscrip);

        dCmd.Parameters.AddWithValue("@GUIDp", strGuid.Trim());
        dCmd.Parameters.AddWithValue("@RunningNop", intRunningNo);

        dCmd.Parameters.AddWithValue("@IntervalNop", intIntervalNo);
        dCmd.Parameters.AddWithValue("@ntervalTimep", strIntervalTime.Trim());
        dCmd.Parameters.AddWithValue("@NextRenewalOnp", NextRenewalOn);
        return dCmd.ExecuteNonQuery();
    }


    public static int CRM_ReceivingEquipment_TransLab(SqlConnection conn, int intEquipID, int intReceivedTransID, int intUserID, int intBranch, string strGuid, DateTime dtECD, string strRemarks, string strBilling, string strDelivery, string strCert, string strFileName, int intlab1, int intlab2, int intlab3, int intlab4, int intlab5, int intlab6, int intlab7, int intlab8, int intlab9, int intlab10, string strBilConName, string strBilConNo, string strBilConMail, string strDelConName, string strDelConNo, string strDelConMail, string strCertConName, string strCertConNo, string strCertConMail, int intIntervalNo, string strIntervalTime, DateTime NextRenewalOn, int intContID, int intQuoteId, int intCustID, string strStatus)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_ReceiveEqui_TransLab_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@intEquipIDp", intEquipID);
        dCmd.Parameters.AddWithValue("@Remarksp", strRemarks.Trim());

        dCmd.Parameters.AddWithValue("@intReceivedTransIDp", intReceivedTransID);
        dCmd.Parameters.AddWithValue("@UserIDp", intUserID);
        dCmd.Parameters.AddWithValue("@BranchIDp", intBranch);
        dCmd.Parameters.AddWithValue("@FileName", strFileName.Trim());
        dCmd.Parameters.AddWithValue("@flagp", strStatus.Trim());

        dCmd.Parameters.AddWithValue("@Ecdp", dtECD);
        dCmd.Parameters.AddWithValue("@BillingAddp", strBilling.Trim());
        dCmd.Parameters.AddWithValue("@DeliveryAddp", strDelivery.Trim());
        dCmd.Parameters.AddWithValue("@CertAddp", strCert.Trim());

        dCmd.Parameters.AddWithValue("@BilConNamep", strBilConName.Trim());
        dCmd.Parameters.AddWithValue("@BilConNop", strBilConNo.Trim());
        dCmd.Parameters.AddWithValue("@BilConMailp", strBilConMail.Trim());

        dCmd.Parameters.AddWithValue("@DelConNamep", strDelConName.Trim());
        dCmd.Parameters.AddWithValue("@DelConNop", strDelConNo.Trim());
        dCmd.Parameters.AddWithValue("@DelConMailp", strDelConMail.Trim());

        dCmd.Parameters.AddWithValue("@CertConNamep", strCertConName.Trim());
        dCmd.Parameters.AddWithValue("@CertConNop", strCertConNo.Trim());
        dCmd.Parameters.AddWithValue("@CertConMailp", strCertConMail.Trim());

        dCmd.Parameters.AddWithValue("@ContIdp", intContID);
        dCmd.Parameters.AddWithValue("@QuoteIdp", intQuoteId);
        dCmd.Parameters.AddWithValue("@CustIdp", intCustID);

        dCmd.Parameters.AddWithValue("@GUIDp", strGuid.Trim());

        dCmd.Parameters.AddWithValue("@Lab1p", intlab1);
        dCmd.Parameters.AddWithValue("@Lab2p", intlab2);
        dCmd.Parameters.AddWithValue("@Lab3p", intlab3);
        dCmd.Parameters.AddWithValue("@Lab4p", intlab4);
        dCmd.Parameters.AddWithValue("@Lab5p", intlab5);
        dCmd.Parameters.AddWithValue("@Lab6p", intlab6);
        dCmd.Parameters.AddWithValue("@Lab7p", intlab7);
        dCmd.Parameters.AddWithValue("@Lab8p", intlab8);
        dCmd.Parameters.AddWithValue("@Lab9p", intlab9);
        dCmd.Parameters.AddWithValue("@Lab10p", intlab10);
        return dCmd.ExecuteNonQuery();
    }


    public static int CRM_ReceivingEquipmentTrans_AddQty(SqlConnection conn, int intRECEIVED_TRANS_ID, int intCustId, int intQuoteID, int intContID, int intEquipID, int intQty, int intUserId, int intBranchId, int intQuoteDetailID, int intContDetailId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_ReceiveEquiTrans_AddQty]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@IDp", intRECEIVED_TRANS_ID);
        dCmd.Parameters.AddWithValue("@custId", intCustId);
        dCmd.Parameters.AddWithValue("@QuoteId", intQuoteID);
        dCmd.Parameters.AddWithValue("@ContIDp", intContID);
        dCmd.Parameters.AddWithValue("@intEquipIDp", intEquipID);

        dCmd.Parameters.AddWithValue("@Qtyp", intQty);
        dCmd.Parameters.AddWithValue("@UserIDp", intUserId);
        dCmd.Parameters.AddWithValue("@BranchIDp", intBranchId);
        dCmd.Parameters.AddWithValue("@QuoteDetailIDp", intQuoteDetailID);
        dCmd.Parameters.AddWithValue("@ContDetailIDp", intContDetailId);
        return dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader getAllByQuote_CRMReceiveEquipmentTrans(SqlConnection conn, int intQuoteID, int intContID, int intEquipId, int intBranchID, string strStatus, int intQuoteDetailID, int intContDetailId, string strTableFrom)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_ReceiveEquipTrans_GetDetailByQuote]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@QuoteIdp", intQuoteID);
        dCmd.Parameters.AddWithValue("@ContIDp", intContID);
        dCmd.Parameters.AddWithValue("@EquipIDp", intEquipId);
        dCmd.Parameters.AddWithValue("@BranchID", intBranchID);
        dCmd.Parameters.AddWithValue("@Flagp", strStatus.Trim());
        dCmd.Parameters.AddWithValue("@QuoteDetailIDp", intQuoteDetailID);
        dCmd.Parameters.AddWithValue("@ContDetailIDp", intContDetailId);
        dCmd.Parameters.AddWithValue("@FromTablep", strTableFrom.Trim());

        SqlDataReader reader = dCmd.ExecuteReader();
        return reader;
    }

    public static int SaveBarcode_Guid(SqlConnection conn, int intEquipID, int intCustID, int intContQuoteID, int intContratQuotationDetailID, int intUserId, string strStatus, string strIsInnerBranch, int intBranchId, string strPriority)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_BarcodePrint_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter oParam = dCmd.Parameters.AddWithValue("@EquipIDp", intEquipID);
        oParam = dCmd.Parameters.AddWithValue("@CustIDp", intCustID);
        oParam = dCmd.Parameters.AddWithValue("@ContQuoteIDp", intContQuoteID);
        oParam = dCmd.Parameters.AddWithValue("@ContratQuotationDetailIDp", intContratQuotationDetailID);
        oParam = dCmd.Parameters.AddWithValue("@UserIdp", intUserId);
        oParam = dCmd.Parameters.AddWithValue("@Statusp", strStatus);

        oParam = dCmd.Parameters.AddWithValue("@IsInnerBranchp", strIsInnerBranch.Trim());
        oParam = dCmd.Parameters.AddWithValue("@ProcesBranchp", intBranchId);
        oParam = dCmd.Parameters.AddWithValue("@strPriorityp", strPriority.Trim());
        oParam = dCmd.Parameters.AddWithValue("@ReceiveTransID_Out", 0);
        oParam.Direction = ParameterDirection.Output;
        dCmd.ExecuteNonQuery();
        int nTheNewId = Convert.ToInt32(dCmd.Parameters["@ReceiveTransID_Out"].Value);
        return nTheNewId;
    }

    public static int SaveBarcode_SubCon(SqlConnection conn, int intEquipID, int intCustID, int intContQuoteID, int intContratQuotationDetailID, int intUserId, string strStatus, string strSubConTo, string strAttention, string strPriority)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_BarcodePrint_SubCon]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter oParam = dCmd.Parameters.AddWithValue("@EquipIDp", intEquipID);
        oParam = dCmd.Parameters.AddWithValue("@CustIDp", intCustID);
        oParam = dCmd.Parameters.AddWithValue("@ContQuoteIDp", intContQuoteID);
        oParam = dCmd.Parameters.AddWithValue("@ContratQuotationDetailIDp", intContratQuotationDetailID);
        oParam = dCmd.Parameters.AddWithValue("@UserIdp", intUserId);
        oParam = dCmd.Parameters.AddWithValue("@Statusp", strStatus.Trim());

        oParam = dCmd.Parameters.AddWithValue("@Subcontop", strSubConTo.Trim());
        oParam = dCmd.Parameters.AddWithValue("@Attentionp", strAttention);
        oParam = dCmd.Parameters.AddWithValue("@strPriorityp", strPriority.Trim());

        oParam = dCmd.Parameters.AddWithValue("@ReceiveTransID_Out", 0);
        oParam.Direction = ParameterDirection.Output;
        dCmd.ExecuteNonQuery();
        int nTheNewId = Convert.ToInt32(dCmd.Parameters["@ReceiveTransID_Out"].Value);
        return nTheNewId;

    }

    public static int SaveBarcode_OnSite(SqlConnection conn, int intEquipID, int intCustID, int intContQuoteID, int intContratQuotationDetailID, int intUserId, string strStatus, DateTime dtOnsiteOn, string strAttention, int BranchId, string strPartialFull, int intQty, string strbatch, int intGetRunningNo, int intlab1, int intlab2, int intlab3, int intlab4, int intlab5, int intlab6, int intlab7, int intlab8, int intlab9, int intlab10)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_BarcodePrint_OnSite]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter oParam = dCmd.Parameters.AddWithValue("@EquipIDp", intEquipID);
        oParam = dCmd.Parameters.AddWithValue("@CustIDp", intCustID);
        oParam = dCmd.Parameters.AddWithValue("@ContQuoteIDp", intContQuoteID);
        oParam = dCmd.Parameters.AddWithValue("@ContratQuotationDetailIDp", intContratQuotationDetailID);
        oParam = dCmd.Parameters.AddWithValue("@UserIdp", intUserId);
        oParam = dCmd.Parameters.AddWithValue("@Statusp", strStatus.Trim());

        oParam = dCmd.Parameters.AddWithValue("@OnsiteOnp", dtOnsiteOn);
        oParam = dCmd.Parameters.AddWithValue("@Attentionp", strAttention);
        oParam = dCmd.Parameters.AddWithValue("@BranchIdp", BranchId);

        oParam = dCmd.Parameters.AddWithValue("@PartialFullp", strPartialFull);
        oParam = dCmd.Parameters.AddWithValue("@Qtyp", intQty);

        oParam = dCmd.Parameters.AddWithValue("@BatchNo", strbatch);
        oParam = dCmd.Parameters.AddWithValue("@GetRunningNo", intGetRunningNo);

        oParam = dCmd.Parameters.AddWithValue("@intlab1p", intlab1);
        oParam = dCmd.Parameters.AddWithValue("@intlab2p", intlab2);
        oParam = dCmd.Parameters.AddWithValue("@intlab3p", intlab3);
        oParam = dCmd.Parameters.AddWithValue("@intlab4p", intlab4);
        oParam = dCmd.Parameters.AddWithValue("@intlab5p", intlab5);
        oParam = dCmd.Parameters.AddWithValue("@intlab6p", intlab6);
        oParam = dCmd.Parameters.AddWithValue("@intlab7p", intlab7);
        oParam = dCmd.Parameters.AddWithValue("@intlab8p", intlab8);
        oParam = dCmd.Parameters.AddWithValue("@intlab9p", intlab9);
        oParam = dCmd.Parameters.AddWithValue("@intlab10p", intlab10);

        oParam = dCmd.Parameters.AddWithValue("@ReceiveTransID_Out", 0);
        oParam.Direction = ParameterDirection.Output;
        dCmd.ExecuteNonQuery();
        int nTheNewId = Convert.ToInt32(dCmd.Parameters["@ReceiveTransID_Out"].Value);
        return nTheNewId;

    }

    public static int SaveBarcode_OnSite_Edit(SqlConnection conn, int intEquipID, int intCustID, int intContQuoteID, int intContratQuotationDetailID, int intUserId, string strStatus, DateTime dtOnsiteOn, string strAttention, int BranchId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_BarcodePrint_OnSite_Edit]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter oParam = dCmd.Parameters.AddWithValue("@EquipIDp", intEquipID);
        oParam = dCmd.Parameters.AddWithValue("@CustIDp", intCustID);
        oParam = dCmd.Parameters.AddWithValue("@ContQuoteIDp", intContQuoteID);
        oParam = dCmd.Parameters.AddWithValue("@ContratQuotationDetailIDp", intContratQuotationDetailID);
        oParam = dCmd.Parameters.AddWithValue("@UserIdp", intUserId);
        oParam = dCmd.Parameters.AddWithValue("@Statusp", strStatus.Trim());

        oParam = dCmd.Parameters.AddWithValue("@OnsiteOnp", dtOnsiteOn);
        oParam = dCmd.Parameters.AddWithValue("@Attentionp", strAttention);
        oParam = dCmd.Parameters.AddWithValue("@BranchIdp", BranchId);
        oParam = dCmd.Parameters.AddWithValue("@ReceiveTransID_Out", 0);
        oParam.Direction = ParameterDirection.Output;
        dCmd.ExecuteNonQuery();
        int nTheNewId = Convert.ToInt32(dCmd.Parameters["@ReceiveTransID_Out"].Value);
        return nTheNewId;

    }
    public static SqlDataReader SaveBarcode_OnSite_GetRunningNo(SqlConnection conn, int intUserID, int intBranchId, string strBatchNo)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_BarcodePrint_OnSite_GetRunningNo]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@BranchIdp", intBranchId);
        dCmd.Parameters.AddWithValue("@UserIdp", intUserID);
        dCmd.Parameters.AddWithValue("@BatchNo", strBatchNo);
        SqlDataReader reader = dCmd.ExecuteReader();
        return reader;
    }


    //public static int CRM_ReceivingEquipment(SqlConnection conn, int intId, int intCustId, int intQuoteID, int intContID, int intUserId, int intBranchId, string strBatchNo, string strFlag, string strJobType)
    //{
    //    SqlCommand dCmd = new SqlCommand("[sp_CRM_ReceiveEquipment]", conn);
    //    dCmd.CommandType = CommandType.StoredProcedure;
    //    SqlParameter oParam = dCmd.Parameters.AddWithValue("@IDp", intId);
    //    oParam = dCmd.Parameters.AddWithValue("@CustIDp", intCustId);
    //    oParam = dCmd.Parameters.AddWithValue("@QuoteIDp", intQuoteID);
    //    oParam = dCmd.Parameters.AddWithValue("@ContIDp", intContID);
    //    oParam = dCmd.Parameters.AddWithValue("@UserIdp", intUserId);
    //    oParam = dCmd.Parameters.AddWithValue("@BranchIdp", intBranchId);

    //    oParam = dCmd.Parameters.AddWithValue("@BatchNop", strBatchNo.Trim());
    //    oParam = dCmd.Parameters.AddWithValue("@strFlagp", strFlag.Trim());
    //    oParam = dCmd.Parameters.AddWithValue("@strJobTypep", strJobType.Trim());

    //    oParam = dCmd.Parameters.AddWithValue("@ReceivedID_Out", 0);
    //    oParam.Direction = ParameterDirection.Output;
    //    dCmd.ExecuteNonQuery();
    //    int nTheNewId = Convert.ToInt32(dCmd.Parameters["@ReceivedID_Out"].Value);
    //    return nTheNewId;
    //}

    public static int CRM_ReceivingEquipment(SqlConnection conn, int intId, int intCustId, int intQuoteID, int intContID, int intUserId, int intBranchId, string strBatchNo, string strFlag, string strJobType, string strPO)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_ReceiveEquipment]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter oParam = dCmd.Parameters.AddWithValue("@IDp", intId);
        oParam = dCmd.Parameters.AddWithValue("@CustIDp", intCustId);
        oParam = dCmd.Parameters.AddWithValue("@QuoteIDp", intQuoteID);
        oParam = dCmd.Parameters.AddWithValue("@ContIDp", intContID);
        oParam = dCmd.Parameters.AddWithValue("@UserIdp", intUserId);
        oParam = dCmd.Parameters.AddWithValue("@BranchIdp", intBranchId);

        oParam = dCmd.Parameters.AddWithValue("@BatchNop", strBatchNo.Trim());
        oParam = dCmd.Parameters.AddWithValue("@strFlagp", strFlag.Trim());
        oParam = dCmd.Parameters.AddWithValue("@strJobTypep", strJobType.Trim());
        oParam = dCmd.Parameters.AddWithValue("@strPO", strPO.Trim());

        oParam = dCmd.Parameters.AddWithValue("@ReceivedID_Out", 0);
        oParam.Direction = ParameterDirection.Output;
        dCmd.ExecuteNonQuery();
        int nTheNewId = Convert.ToInt32(dCmd.Parameters["@ReceivedID_Out"].Value);
        return nTheNewId;
    }

    public static int SaveStoreTrack(SqlConnection conn, int intUserID, int intReceivedTransDetailId, int intBranchID, string strJob, int intTrackID, string strRemarks, string strStatus)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_StoreTrack_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@IDp", intReceivedTransDetailId);
        dCmd.Parameters.AddWithValue("@intTrackIDp", intTrackID);
        dCmd.Parameters.AddWithValue("@UserIdp", intUserID);
        dCmd.Parameters.AddWithValue("@BranchIdp", intBranchID);
        dCmd.Parameters.AddWithValue("@flagp", strStatus);
        dCmd.Parameters.AddWithValue("@Jobp", strJob);
        dCmd.Parameters.AddWithValue("@Remarksp", strRemarks);
        return dCmd.ExecuteNonQuery();
    }

    public static int SaveStoreTrack_AssignJob(SqlConnection conn, int intUserID, int intReceivedTransDetailId, int intBranchID, int intTrackId, int intStaffID, string strStatus, DateTime dtAssignJobDdate)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_StoreTrack_AssignJob]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@IDp", intReceivedTransDetailId);
        dCmd.Parameters.AddWithValue("@intTrackIDp", intTrackId);
        dCmd.Parameters.AddWithValue("@UserIdp", intUserID);
        dCmd.Parameters.AddWithValue("@BranchIdp", intBranchID);
        dCmd.Parameters.AddWithValue("@flagp", strStatus);
        dCmd.Parameters.AddWithValue("@StaffIdp", intStaffID);
        dCmd.Parameters.AddWithValue("@dtAssignJobDdatep", dtAssignJobDdate);
        return dCmd.ExecuteNonQuery();
    }

    public static int SaveStoreTrack_AssignJobOnsite(SqlConnection conn, int intUserID, int intReceivedTransDetailId, int intBranchID, int intTrackId, int intStaffID, string strStatus, int intLabId, DateTime dtAssignJobDdate)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_StoreTrack_AssignJobOnsite]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@IDp", intReceivedTransDetailId);
        dCmd.Parameters.AddWithValue("@intTrackIDp", intTrackId);
        dCmd.Parameters.AddWithValue("@UserIdp", intUserID);
        dCmd.Parameters.AddWithValue("@BranchIdp", intBranchID);
        dCmd.Parameters.AddWithValue("@flagp", strStatus);
        dCmd.Parameters.AddWithValue("@StaffIdp", intStaffID);
        dCmd.Parameters.AddWithValue("@LabIdp", intLabId);
        dCmd.Parameters.AddWithValue("@dtAssignJobDdatep", dtAssignJobDdate);
        return dCmd.ExecuteNonQuery();
    }

    public static int CRM_ReceivingEquipmentTrans_UpdateAcces(SqlConnection conn, int intReceivedTransID, int intReceivedTransDetailID, int intUserID, int intBranch, string strAccesList, string strAccesSerielList, string strStatus)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_ReceivingEquipmentTrans_UpdateAcces]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@intReceivedTransIDp", intReceivedTransID);
        dCmd.Parameters.AddWithValue("@intReceivedTransDetailIDp", intReceivedTransDetailID);
        dCmd.Parameters.AddWithValue("@UserIDp", intUserID);
        dCmd.Parameters.AddWithValue("@BranchIDp", intBranch);
        dCmd.Parameters.AddWithValue("@strAccesListp", strAccesList.Trim());
        dCmd.Parameters.AddWithValue("@strAccesSerielListp", strAccesSerielList);
        dCmd.Parameters.AddWithValue("@strStatusp", strStatus.Trim());
        return dCmd.ExecuteNonQuery();

    }

    public static int saveSerialIDAuto(SqlConnection connMRVAuto, string strBranchId, string strAutoNo, string strYear, string saveFlag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_AutoGenerateID_Save]", connMRVAuto);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@BranchIDp", strBranchId);
        dCmd.Parameters.AddWithValue("@strAutonop", strAutoNo);
        dCmd.Parameters.AddWithValue("@strYearp", strYear);
        dCmd.Parameters.AddWithValue("@saveflagp", saveFlag);
        return dCmd.ExecuteNonQuery();
    }


    public static int saveDOAuto(SqlConnection connMRVAuto, string strBranchId, string strAutoNo, string strYear, string strLastAutoNo, string saveFlag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_AutoDO_Save]", connMRVAuto);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@BranchIDp", strBranchId);
        dCmd.Parameters.AddWithValue("@strAutonop", strAutoNo);
        dCmd.Parameters.AddWithValue("@strYearp", strYear);
        dCmd.Parameters.AddWithValue("@strLastAutoNop", strLastAutoNo);
        dCmd.Parameters.AddWithValue("@saveflagp", saveFlag);
        return dCmd.ExecuteNonQuery();
    }
    public static int CreateOnSiteDO(SqlConnection conn, int intEquipID, int intCustID, int intContQuoteID, int intContratQuotationDetailID, int intUserId, string strStatus, string strAttention, int BranchId, string strBatchNo, string strDONumber, string strRemarks, int intTransDetailID, string strPO)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_DeliveryOrderOnsite]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter oParam = dCmd.Parameters.AddWithValue("@EquipIDp", intEquipID);
        oParam = dCmd.Parameters.AddWithValue("@CustIDp", intCustID);
        oParam = dCmd.Parameters.AddWithValue("@ContQuoteIDp", intContQuoteID);
        oParam = dCmd.Parameters.AddWithValue("@ContratQuotationDetailIDp", intContratQuotationDetailID);
        oParam = dCmd.Parameters.AddWithValue("@UserIdp", intUserId);
        oParam = dCmd.Parameters.AddWithValue("@Statusp", strStatus.Trim());

        oParam = dCmd.Parameters.AddWithValue("@Attentionp", strAttention);
        oParam = dCmd.Parameters.AddWithValue("@BranchIdp", BranchId);
        oParam = dCmd.Parameters.AddWithValue("@BatchNop", strBatchNo.Trim());
        oParam = dCmd.Parameters.AddWithValue("@DONop", strDONumber.Trim());
        oParam = dCmd.Parameters.AddWithValue("@Remarksp", strRemarks.Trim());
        oParam = dCmd.Parameters.AddWithValue("@intTransDetailIDp", intTransDetailID);
        oParam = dCmd.Parameters.AddWithValue("@PONop", strPO.Trim());

        oParam = dCmd.Parameters.AddWithValue("@ReceiveTransID_Out", 0);
        oParam.Direction = ParameterDirection.Output;
        dCmd.ExecuteNonQuery();
        int nTheNewId = Convert.ToInt32(dCmd.Parameters["@ReceiveTransID_Out"].Value);
        return nTheNewId;

    }


    //public static int SaveStoreTrack_AssignJobOnsite(SqlConnection conn, int p, int p_2, short p_3, int intTrackId, int intStaffID, string p_4)
    //{
    //    throw new NotImplementedException();
    //}
    //intTransID = BusinessTier_CRM.SaveDeliveryOrder_Final(conn, int intTransDetailId, int intTrackID, int intCustID, int intContQuoteID, int intContDetailID, int intQuoteDetailId, int intUserID, string strStatus, string strRemarks, Datetime dtDoFinal, string strPO, int intBranchId);

    public static int SaveDeliveryOrder_Final(SqlConnection conn, int intTransDetailId, int intTrackID, int intCustID, int intContQuoteID, int intContDetailID, int intQuoteDetailId, int intUserID, string strStatus, string strRemarks, DateTime dtDoFinal, string strPO, int intBranchId, int intEquipmentId, string strJobNo, string strDO_Number)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_DeliveryOrderFinal]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter oParam = dCmd.Parameters.AddWithValue("@intCRMTransDetailIdp", intTransDetailId);
        oParam = dCmd.Parameters.AddWithValue("@EquipIDp", intEquipmentId);
        oParam = dCmd.Parameters.AddWithValue("@TrackIdp", intTrackID);
        oParam = dCmd.Parameters.AddWithValue("@CustIDp", intCustID);
        oParam = dCmd.Parameters.AddWithValue("@ContQuoteIDp", intContQuoteID);
        oParam = dCmd.Parameters.AddWithValue("@ContratDetailIDp", intContDetailID);
        oParam = dCmd.Parameters.AddWithValue("@QuoteDetailIdp", intQuoteDetailId);

        oParam = dCmd.Parameters.AddWithValue("@UserIdp", intUserID);
        oParam = dCmd.Parameters.AddWithValue("@Statusp", strStatus.Trim());

        oParam = dCmd.Parameters.AddWithValue("@Remarksp", strRemarks.Trim());
        oParam = dCmd.Parameters.AddWithValue("@DoDatep", dtDoFinal);
        oParam = dCmd.Parameters.AddWithValue("@BranchIdp", intBranchId);
        oParam = dCmd.Parameters.AddWithValue("@BatchNop", strJobNo.Trim());
        oParam = dCmd.Parameters.AddWithValue("@DONop", strDO_Number.Trim());
        oParam = dCmd.Parameters.AddWithValue("@PONop", strPO.Trim());

        oParam = dCmd.Parameters.AddWithValue("@ReceiveTransID_Out", 0);
        oParam.Direction = ParameterDirection.Output;
        dCmd.ExecuteNonQuery();
        int nTheNewId = Convert.ToInt32(dCmd.Parameters["@ReceiveTransID_Out"].Value);
        return nTheNewId;
    }

    public static int JOB_COMPLETE(SqlConnection conn, int intrecdtrnsid, int intquotid, int intQuottrndid, string strStatus, int intbranchid, int intperfrmbrnchid, string strjobrunningNO, string intUserId, string saveflag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Job_Complete]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;

        dCmd.Parameters.AddWithValue("@intjobnop", intrecdtrnsid);
        dCmd.Parameters.AddWithValue("@QUOTATION_IDp", intquotid);
        dCmd.Parameters.AddWithValue("@QUOTATION_TRANS_IDp", intQuottrndid);
        dCmd.Parameters.AddWithValue("@BranchIDp", intbranchid);
        dCmd.Parameters.AddWithValue("@PerformBranchIDp", intperfrmbrnchid);
        dCmd.Parameters.AddWithValue("@JobRunningNOp", strjobrunningNO);
        dCmd.Parameters.AddWithValue("@StrStatusp", strStatus);

        dCmd.Parameters.AddWithValue("@useridp", intUserId);
        dCmd.Parameters.AddWithValue("@flagp", saveflag);
        //Branchid,Performingbranchid,Jobno,labid,
        return dCmd.ExecuteNonQuery();
    }
    public static int JOB_DELETE(SqlConnection conn, int intrecdtrnsid, int intquotid, int intQuottrndid, string strStatus, int intbranchid, int intperfrmbrnchid, string strjobrunningNO, string intUserId, string saveflag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Job_Delete]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;

        dCmd.Parameters.AddWithValue("@intjobnop", intrecdtrnsid);
        dCmd.Parameters.AddWithValue("@QUOTATION_IDp", intquotid);
        dCmd.Parameters.AddWithValue("@QUOTATION_TRANS_IDp", intQuottrndid);
        dCmd.Parameters.AddWithValue("@BranchIDp", intbranchid);
        dCmd.Parameters.AddWithValue("@PerformBranchIDp", intperfrmbrnchid);
        dCmd.Parameters.AddWithValue("@JobRunningNOp", strjobrunningNO);
        dCmd.Parameters.AddWithValue("@StrStatusp", strStatus);

        dCmd.Parameters.AddWithValue("@useridp", intUserId);
        dCmd.Parameters.AddWithValue("@flagp", saveflag);
        //Branchid,Performingbranchid,Jobno,labid,
        return dCmd.ExecuteNonQuery();
    }

    public static int JOB_REJECT(SqlConnection conn, int intrecdtrnsid, int intquotid, int intQuottrndid, string strStatus, int intbranchid, int intperfrmbrnchid, string strjobrunningNO, string intUserId, string saveflag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Job_Reject]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;

        dCmd.Parameters.AddWithValue("@intjobnop", intrecdtrnsid);
        dCmd.Parameters.AddWithValue("@QUOTATION_IDp", intquotid);
        dCmd.Parameters.AddWithValue("@QUOTATION_TRANS_IDp", intQuottrndid);
        dCmd.Parameters.AddWithValue("@BranchIDp", intbranchid);
        dCmd.Parameters.AddWithValue("@PerformBranchIDp", intperfrmbrnchid);
        dCmd.Parameters.AddWithValue("@JobRunningNOp", strjobrunningNO);
        dCmd.Parameters.AddWithValue("@StrStatusp", strStatus);

        dCmd.Parameters.AddWithValue("@useridp", intUserId);
        dCmd.Parameters.AddWithValue("@flagp", saveflag);
        //Branchid,Performingbranchid,Jobno,labid,
        return dCmd.ExecuteNonQuery();
    }


    public static int JobReject_ByLab(SqlConnection conn, int intUserID, int intReceivedTransDetailId, int intTrackId, int intBranchID, string strRemarks)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CRM_JobReject_ByLab]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@IDp", intReceivedTransDetailId);
        dCmd.Parameters.AddWithValue("@intTrackIDp", intTrackId);
        dCmd.Parameters.AddWithValue("@UserIdp", intUserID);
        dCmd.Parameters.AddWithValue("@BranchIdp", intBranchID);
        dCmd.Parameters.AddWithValue("@strRemarksp", strRemarks.Trim());
        return dCmd.ExecuteNonQuery();
    }
}