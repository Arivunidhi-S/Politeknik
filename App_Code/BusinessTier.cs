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
/// Summary description for Class1
/// </summary>
public class BusinessTier
{
    public BusinessTier()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable g_ErrorMessagesDataTable;

    public static SqlConnection getConnection()
    {
        string conString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        SqlConnection conn = new SqlConnection(conString);
        return conn;
    }

    public static string getConnection1()
    {
        string conString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        return conString;
    }

    public static void DisposeConnection(SqlConnection conn)
    {
        conn.Close();
        conn.Dispose();
    }

    public static void DisposeReader(SqlDataReader reader)
    {
        reader.Close();
        reader.Dispose();
    }

    public static void DisposeAdapter(SqlDataAdapter adapter)
    {
        adapter.Dispose();
    }

    public static SqlDataReader VaildateUserLogin(SqlConnection connec, string Logind, string Password)
    {
        SqlCommand cmd = new SqlCommand("sp_Validate_UserLogin", connec);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Useridp", Logind);
        cmd.Parameters.AddWithValue("@Passp", Password);
        SqlDataReader reader1 = cmd.ExecuteReader();
        return reader1;
    }

    //--------------------------< Methods For Master Platform />-----------------------------------
   
    public static SqlDataReader getPlatformInfoById(SqlConnection getPlatformconn, string platformid)
    {
        int delval = 0;
        string sql = "select * FROM MasterPlatform WHERE PlatformId='" + platformid + "' and  Deleted='" + delval + "' ORDER BY PlatformName";
        SqlCommand cmd = new SqlCommand(sql, getPlatformconn);
        SqlDataReader getPlatformreader = cmd.ExecuteReader();
        return getPlatformreader;
    }

    public static int DeletePlatformGrid(SqlConnection conn, string id)
    {
        SqlCommand dCmd = new SqlCommand("[sp_MasterPlatform_Delete]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@platformidp", id);
        return dCmd.ExecuteNonQuery();
    }

    public static int SavePlatformMaster(SqlConnection conn, string strName, string strDesc, string strUserID, string saveFlg, string strId)
    {
        string sp_Name;
        string RowValue = "0";
        if (saveFlg.ToString() == "N")
        {
            sp_Name = "sp_MasterPlatform_Insert";
        }
        else
        {
            sp_Name = "sp_MasterPlatform_Update";
        }
        SqlCommand dCmd = new SqlCommand(sp_Name, conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (saveFlg.ToString() == "U")
        {
            dCmd.Parameters.AddWithValue("@idp", strId);
            dCmd.Parameters.AddWithValue("@Rowp", RowValue);
        }

        dCmd.Parameters.AddWithValue("@namep ", strName);
        dCmd.Parameters.AddWithValue("@descriptionp", strDesc);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        return dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader checkPlatformName(SqlConnection connCheck, string platformname)
    {
        SqlCommand cmd = new SqlCommand("sp_MasterPlatform_IsDuplicate", connCheck);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@platformnamep", platformname);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader gePlatformIdByPlatformName(SqlConnection connGetId, string platformName)
    {
        SqlCommand cmd = new SqlCommand("[sp_MasterPlatform_GetIDByName]", connGetId);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@platformnamep", platformName);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    //------------------------------------------------------------------------------------------------
    //--------------------------< Methods For Master Check IS Duplicate >--------------------------------------

    public static SqlDataReader checkcode(SqlConnection connCheck, string Branchcode, string strflag)
    {
        SqlCommand cmd = new SqlCommand("sp_Master_IsDuplicate", connCheck);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@codep", Branchcode);
        cmd.Parameters.AddWithValue("@Flagp", strflag);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    //------------------------------------------------------------------------------------------------
    //--------------------------< Methods For Master branch >--------------------------------------

    public static int SaveBranchMaster(SqlConnection conn, int intid, string strcode, string strName, string Addrline1, string city, int postcode, string strstate, string strcountry, string strDesc, string strcontactno, string strfaxno, string stremail, string strUserID, string saveFlg)
    {
        SqlCommand dCmd = new SqlCommand("sp_Master_Branch", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@BranchIdp", intid);
        dCmd.Parameters.AddWithValue("@Branchcodep", strcode);
        dCmd.Parameters.AddWithValue("@Branchnamep", strName);
        dCmd.Parameters.AddWithValue("@address1p", Addrline1);
        dCmd.Parameters.AddWithValue("@cityp", city);
        dCmd.Parameters.AddWithValue("@postcodep", postcode);
        dCmd.Parameters.AddWithValue("@statep", strstate);
        dCmd.Parameters.AddWithValue("@countryp", strcountry);
        dCmd.Parameters.AddWithValue("@descriptionp", strDesc);
        dCmd.Parameters.AddWithValue("@contactnop", strcontactno);
        dCmd.Parameters.AddWithValue("@faxnop", strfaxno);
        dCmd.Parameters.AddWithValue("@emailp", stremail);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //---------------------------------------------------------------------------------------------
    //--------------------------< Methods For Master Department >--------------------------------------

    public static int SaveDepartmentMaster(SqlConnection conn, int intid, string strcode, string strName, bool intlab, string strshortname, string strDesc, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_Department", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@DeptIdp", intid);
        dCmd.Parameters.AddWithValue("@Deptcodep", strcode);
        dCmd.Parameters.AddWithValue("@Deptnamep", strName);
        dCmd.Parameters.AddWithValue("@labp", intlab);
        dCmd.Parameters.AddWithValue("@shortnameP", strshortname);
        dCmd.Parameters.AddWithValue("@descriptionp", strDesc);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }
  
    //---------------------------------------------------------------------------------------------
    //--------------------------< Methods For Master Staff >--------------------------------------

    public static int SaveStaffMaster(SqlConnection conn, int intstaffid, string strstaffNo, string strStaffName, string strposition, string strcontactno, string stremail, int intdeptid, string intbranchid, bool isHod, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_Staff", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@StaffIdp", intstaffid);
        dCmd.Parameters.AddWithValue("@StaffNOp", strstaffNo);
        dCmd.Parameters.AddWithValue("@Staffnamep", strStaffName);
        dCmd.Parameters.AddWithValue("@Positionp", strposition);
        dCmd.Parameters.AddWithValue("@contactnop", strcontactno);
        dCmd.Parameters.AddWithValue("@emailp", stremail);
        dCmd.Parameters.AddWithValue("@DeptIdp", intdeptid);
        dCmd.Parameters.AddWithValue("@BranchIdp", intbranchid);
        dCmd.Parameters.AddWithValue("@IsHODp", isHod);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }
  
    //---------------------------------------------------------------------------------------------


    //--------------------------< Methods For Master Instrument >--------------------------------------

    public static int SaveInstrumentMaster(SqlConnection conn, int intstdcalibid, int intdeptid, string strinstrname, string strserialno, string strCertfctno, DateTime duedate, string strtrace, string strdesc, string strRemarks, DateTime CalibDate, int intvalidue, int intbranchid, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_Instrument", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@StdCalibIdp", intstdcalibid);
        dCmd.Parameters.AddWithValue("@DeptIdp", intdeptid);
        dCmd.Parameters.AddWithValue("@instrnamep", strinstrname);
        dCmd.Parameters.AddWithValue("@Serialnop", strserialno);
        dCmd.Parameters.AddWithValue("@CertfctNop", strCertfctno);
        dCmd.Parameters.AddWithValue("@DueDatep", duedate);
        dCmd.Parameters.AddWithValue("@Tracebltyp", strtrace);
        dCmd.Parameters.AddWithValue("@Descp", strdesc);
        dCmd.Parameters.AddWithValue("@Remarksp", strRemarks);
        dCmd.Parameters.AddWithValue("@CalibDate", CalibDate);
        dCmd.Parameters.AddWithValue("@validuep", intvalidue);
        dCmd.Parameters.AddWithValue("@BranchIDp", intbranchid);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }
   
    //---------------------------------------------------------------------------------------------
    //--------------------------< Methods For Master Equipment >--------------------------------------

    public static int SaveEquipmentMaster(SqlConnection conn, int intequipid, string strequipno, string strequipname, string strmaker, string strmodel, string strcalproc, string strmu, string strRange, string strClass, double dblfee, int intflag, string strRemarks, int intcategory, int intlab1, int intlab2, int intlab3, int intlab4, int intlab5, int intlab6, int intlab7, int intlab8, int intlab9, int intlab10, double dbladtnlprice, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_Equipment", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@EquipIdp", intequipid);
        dCmd.Parameters.AddWithValue("@Equipnop", strequipno);
        dCmd.Parameters.AddWithValue("@EquipNamep", strequipname);
        dCmd.Parameters.AddWithValue("@Makerp", strmaker);
        dCmd.Parameters.AddWithValue("@Modelp", strmodel);
        dCmd.Parameters.AddWithValue("@Calprocp", strcalproc);
        dCmd.Parameters.AddWithValue("@MUp", strmu);
        dCmd.Parameters.AddWithValue("@Rangep", strRange);
        dCmd.Parameters.AddWithValue("@Classp", strClass);
        dCmd.Parameters.AddWithValue("@Feep", dblfee);
        dCmd.Parameters.AddWithValue("@Flagpricep", intflag);
        dCmd.Parameters.AddWithValue("@Remarksp", strRemarks);
        dCmd.Parameters.AddWithValue("@Catgryp", intcategory);
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
        dCmd.Parameters.AddWithValue("@adtnlpricep", dbladtnlprice);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }
   
    //---------------------------------------------------------------------------------------------
    //--------------------------< Methods For Master Country >--------------------------------------

    public static int SaveCountryMaster(SqlConnection conn, int intcountryid, string strcountryName, string strRemarks, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_Country", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@CountryIdp", intcountryid);
        dCmd.Parameters.AddWithValue("@Countrynamp", strcountryName);
        dCmd.Parameters.AddWithValue("@Remarksp", strRemarks);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }
   
    //---------------------------------------------------------------------------------------------
    //--------------------------< Methods For Master State >--------------------------------------

    public static int SaveStateMaster(SqlConnection conn, int intstateid, string strStateName, string strRemarks, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_State", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@StateIdp", intstateid);
        dCmd.Parameters.AddWithValue("@Statenamp", strStateName);
        dCmd.Parameters.AddWithValue("@Remarksp", strRemarks);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }
   
    //---------------------------------------------------------------------------------------------
    //--------------------------< Methods For Master Category >--------------------------------------

    public static int SaveCategoryMaster(SqlConnection conn, int intCategoryid, string strCatryName, string strRemarks, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_Category", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@CategoryIdp", intCategoryid);
        dCmd.Parameters.AddWithValue("@Categorynamp", strCatryName);
        dCmd.Parameters.AddWithValue("@Remarksp", strRemarks);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }
    
    //---------------------------------------------------------------------------------------------
    //--------------------------< Methods For Master Customer >--------------------------------------

    public static int SaveCustomerMaster(SqlConnection conn, int intid, string strcrmid, int strcustcode, string strcustName, string strRecno, string Addrline1, string Addrline2, string strstate, string strcountry, int intpostcode, string strcontactno, string strfaxno, string stremail, string strwebsite, string strcontrctno, bool isbranch, bool iscontract, int intExcompldays, int intpercn, int intAcntmngr, DateTime dtregdate, string straddrs3, string strcity, string strphonecode, string strphonecode1, string strphone1, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_Customer", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@CustomerIdp", intid);
        dCmd.Parameters.AddWithValue("@CRMIDp", strcrmid);
        dCmd.Parameters.AddWithValue("@Customercodep", strcustcode);
        dCmd.Parameters.AddWithValue("@Customernamep", strcustName);
        dCmd.Parameters.AddWithValue("@CustomerRecNop", strRecno);
        dCmd.Parameters.AddWithValue("@address1p", Addrline1);
        dCmd.Parameters.AddWithValue("@address2p", Addrline2);
        dCmd.Parameters.AddWithValue("@statep", strstate);
        dCmd.Parameters.AddWithValue("@countryp", strcountry);
        dCmd.Parameters.AddWithValue("@Postalcodep", intpostcode);
        dCmd.Parameters.AddWithValue("@contactnop", strcontactno);
        dCmd.Parameters.AddWithValue("@faxnop", strfaxno);
        dCmd.Parameters.AddWithValue("@emailp", stremail);
        dCmd.Parameters.AddWithValue("@Websitep", strwebsite);
        dCmd.Parameters.AddWithValue("@Contractnop", strcontrctno);
        dCmd.Parameters.AddWithValue("@IsBranchp", isbranch);
        dCmd.Parameters.AddWithValue("@IsContractp", iscontract);
        dCmd.Parameters.AddWithValue("@Excompldays", intExcompldays);
        dCmd.Parameters.AddWithValue("@percentagep", intpercn);
        dCmd.Parameters.AddWithValue("@AcntMngrp", intAcntmngr);

        dCmd.Parameters.AddWithValue("@regdatep", dtregdate);
        dCmd.Parameters.AddWithValue("@address3p", straddrs3);
        dCmd.Parameters.AddWithValue("@cityp", strcity);
        dCmd.Parameters.AddWithValue("@phonecodep", strphonecode);
        dCmd.Parameters.AddWithValue("@phonecode1p", strphonecode1);
        dCmd.Parameters.AddWithValue("@phone1p", strphone1);



        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }
   
    //---------------------------------------------------------------------------------------------
    //--------------------------< Methods For Master Contact >--------------------------------------

    public static int SaveContactMaster(SqlConnection conn, int intconatctid, int intcustomerid, string strContactName, string strcontactno, string stremailid, string strDepartment, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_Contact", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ContatcIdp", intconatctid);
        dCmd.Parameters.AddWithValue("@CustomerIdp", intcustomerid);
        dCmd.Parameters.AddWithValue("@ContactNamep", strContactName);
        dCmd.Parameters.AddWithValue("@ContactNop", strcontactno);
        dCmd.Parameters.AddWithValue("@Departmentp", strDepartment);
        dCmd.Parameters.AddWithValue("@Emailp", stremailid);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }
   
    //---------------------------------------------------------------------------------------------
    //--------------------------< Enquiry >--------------------------------------

    public static int SaveEnquiry(SqlConnection conn, int intEnquiryid, string strenquiryno, string strEquiryby, int intcustmrid, string strenqrytype, DateTime dtenquiryft, string strstatus, string strremarks, int strbranchid, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Enquiry", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@EnquiryIdp", intEnquiryid);
        dCmd.Parameters.AddWithValue("@EnquiryNop", strenquiryno);
        dCmd.Parameters.AddWithValue("@EnquiryByp", strEquiryby);
        dCmd.Parameters.AddWithValue("@Customeridp", intcustmrid);
        dCmd.Parameters.AddWithValue("@EnquiryTypep", strenqrytype);
        dCmd.Parameters.AddWithValue("@EnquiryDatep", dtenquiryft);
        dCmd.Parameters.AddWithValue("@Statusp", strstatus);
        dCmd.Parameters.AddWithValue("@Remarks", strremarks);
        dCmd.Parameters.AddWithValue("@strbranchidp", strbranchid);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }
  
    //---------------------------------------------------------------------------------------------
    //--------------------------< Enquiry Details>--------------------------------------

    public static int SaveEnquiryDetails(SqlConnection conn, int intEnquiryDetailid, int intEnquiryid, int intLabid, int intEquipmntid, string strdesc, int intQty, string strremarks, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Enquiry_Details", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@EnquiryDetailIdp", intEnquiryDetailid);
        dCmd.Parameters.AddWithValue("@EnquiryIdp", intEnquiryid);
        dCmd.Parameters.AddWithValue("@Labidp", intLabid);
        dCmd.Parameters.AddWithValue("@Equipmentidp", intEquipmntid);
        dCmd.Parameters.AddWithValue("@Descp", strdesc);
        dCmd.Parameters.AddWithValue("@Qtyp", intQty);
        dCmd.Parameters.AddWithValue("@Remarks", strremarks);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }
   
    //---------------------------------------------------------------------------------------------
    //--------------------------< Quotation Master>--------------------------------------

    public static int SaveQuotation_Master(SqlConnection conn, int intQuotationid, int strbranchid, string strQuotationno, int intcustmrid, int intcontactid, int intcontractid, DateTime DtQuotationdt, string strterms, int txtValidity, string strjobduration, string strstatus, string strenquiryno, string strNotes, string strremarks, decimal intdiscount, string strfeedback, string strUserID, string saveFlg)
    {
        SqlCommand dCmd = new SqlCommand();
        if (saveFlg == "N")
        {
            dCmd = new SqlCommand("sp_Quotation", conn);
        }
        else
        {
            dCmd = new SqlCommand("sp_Quotation_Update", conn);
        }

        // SqlCommand dCmd = new SqlCommand("sp_Quotation", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@Quotationidp", intQuotationid);
        dCmd.Parameters.AddWithValue("@QuotationNop", strQuotationno);
        dCmd.Parameters.AddWithValue("@Customeridp", intcustmrid);
        dCmd.Parameters.AddWithValue("@Contactidp", intcontactid);
        dCmd.Parameters.AddWithValue("@Contractidp", intcontractid);
        dCmd.Parameters.AddWithValue("@branchidp", strbranchid);
        dCmd.Parameters.AddWithValue("@QuoteDatep", DtQuotationdt);
        dCmd.Parameters.AddWithValue("@termsp", strterms);
        dCmd.Parameters.AddWithValue("@Validitiyp", txtValidity);
        dCmd.Parameters.AddWithValue("@jobdurtnp", strjobduration);
        dCmd.Parameters.AddWithValue("@Enquirynop", strenquiryno);
        dCmd.Parameters.AddWithValue("@Notesp", strNotes);
        dCmd.Parameters.AddWithValue("@Remarks", strremarks);
        dCmd.Parameters.AddWithValue("@Status", strstatus);
        dCmd.Parameters.AddWithValue("@Quotdiscountp", intdiscount);
        dCmd.Parameters.AddWithValue("@FeedBackp", strfeedback);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);

        if (saveFlg == "N")
        {

            SqlParameter oParam = dCmd.Parameters.AddWithValue("@intQuotMasterID", 0);
            oParam.Direction = ParameterDirection.Output;
            dCmd.ExecuteNonQuery();
            int nTheNewId = Convert.ToInt32(dCmd.Parameters["@intQuotMasterID"].Value);
            return nTheNewId;
        }
        else
        {
            return dCmd.ExecuteNonQuery();
        }

    }
   
    //---------------------------------------------------------------------------------------------
    //--------------------------< Quotation Approve>--------------------------------------
    //From Enquiry
   
    public static int SaveQuotation_Master_Approve(SqlConnection conn, int intQuotationid, string strQuotationno, int intRevision, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Quotation_Approve", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@Quotationidp", intQuotationid);
        dCmd.Parameters.AddWithValue("@QuotationNop", strQuotationno);
        dCmd.Parameters.AddWithValue("@Revisionp", intRevision);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //---------------------------------------------------------------------------------------------
    //--------------------------< Quotation Details>--------------------------------------
    //From Enquiry
   
    public static int SaveInsertQuot_Detail(SqlConnection conn, int intQuotationtransid, int intQuotationid, string strremarksdetail, int intEquipmentId, int intqty, decimal dcmlprice, int intaddnlrange, decimal intdiscount, string strcalibtype, string strDesc, int aprvflag, int intenqryid, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Quotation_Details", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@QuotationTransidp", intQuotationtransid);
        dCmd.Parameters.AddWithValue("@Quotationidp", intQuotationid);
        dCmd.Parameters.AddWithValue("@Remarksp", strremarksdetail);
        dCmd.Parameters.AddWithValue("@EquipmentIdp", intEquipmentId);
        dCmd.Parameters.AddWithValue("@Qtyp", intqty);
        dCmd.Parameters.AddWithValue("@Pricep", dcmlprice);
        dCmd.Parameters.AddWithValue("@Addntlrangep", intaddnlrange);
        dCmd.Parameters.AddWithValue("@Discountp", intdiscount);
        dCmd.Parameters.AddWithValue("@Calibtypep", strcalibtype);
        dCmd.Parameters.AddWithValue("@Descp", strDesc);
        dCmd.Parameters.AddWithValue("@ApprvFlagp", aprvflag);
        dCmd.Parameters.AddWithValue("@Enqryidp", intenqryid);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);

        return dCmd.ExecuteNonQuery();

    }
   
    //----------------------------qUOTATION dETAILS aPPROVE----------------------------
   
    public static int Quot_Detail_Approve(SqlConnection conn, int intQuotationtransid, int intQuotationid, int intlabid, int intEquipmentId, int intqty, decimal dcmlprice, decimal intdiscount, string strcalibtype, string strDesc, int aprvflag, decimal aprvdiscount, string straprremarks, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Quotation_Details_Approve", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@QuotationTransidp", intQuotationtransid);
        dCmd.Parameters.AddWithValue("@Quotationidp", intQuotationid);
        dCmd.Parameters.AddWithValue("@LabIdp", intlabid);
        dCmd.Parameters.AddWithValue("@EquipmentIdp", intEquipmentId);
        dCmd.Parameters.AddWithValue("@Qtyp", intqty);
        dCmd.Parameters.AddWithValue("@Pricep", dcmlprice);
        dCmd.Parameters.AddWithValue("@Discountp", intdiscount);
        dCmd.Parameters.AddWithValue("@Calibtypep", strcalibtype);
        dCmd.Parameters.AddWithValue("@Descp", strDesc);
        dCmd.Parameters.AddWithValue("@ApprvFlagp", aprvflag);
        dCmd.Parameters.AddWithValue("@AprvDiscount", aprvdiscount);
        dCmd.Parameters.AddWithValue("@ApprvRemarks", straprremarks);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);

        return dCmd.ExecuteNonQuery();

    }

    //--------------------------< Quotation Details>--------------------------------------
    //From Enquiry
   
    public static int SaveInsertQuot_Detail_Jobcosting(SqlConnection conn, int jobcostingid, int intQuotationid, int intmanhrs, int intnoofstfmnhrs, decimal dcmluntcstmnhrs, decimal dcmltotmanhrs, int intmileage, decimal dcmlmileagerate, decimal dcmlmlgcost, decimal dcmltol, decimal dcmlptrl, decimal dcmltottrans, int intnofofrooms, int intacmdsnoofdays, decimal dcmluntcstaccmds, decimal dcmltotaccmds, int intnoofstfmls, int intnoofdaysmls, decimal dcmluntcstmls, decimal dcmltotmls, decimal dcmldiscount, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Quotation_Details_JobCosting", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@Jobcostingidp", jobcostingid);
        dCmd.Parameters.AddWithValue("@Quotationidp", intQuotationid);

        dCmd.Parameters.AddWithValue("@txtmanhoursp", intmanhrs);
        dCmd.Parameters.AddWithValue("@txtnoofstf_manhrsp", intnoofstfmnhrs);
        dCmd.Parameters.AddWithValue("@txtunotcost_manrsp", dcmluntcstmnhrs);
        dCmd.Parameters.AddWithValue("@txttotmanhoursp", dcmltotmanhrs);
        dCmd.Parameters.AddWithValue("@txtmileagep", intmileage);
        dCmd.Parameters.AddWithValue("@txtmileageratep", dcmlmileagerate);
        dCmd.Parameters.AddWithValue("@txtmileagecostp", dcmlmlgcost);
        dCmd.Parameters.AddWithValue("@txttolp", dcmltol);
        dCmd.Parameters.AddWithValue("@txtpetrolp", dcmlptrl);
        dCmd.Parameters.AddWithValue("@txtTotaltransportp", dcmltottrans);
        dCmd.Parameters.AddWithValue("@txtnoofroomsp", intnofofrooms);
        dCmd.Parameters.AddWithValue("@txtnoofdays_Acmdsp", intacmdsnoofdays);
        dCmd.Parameters.AddWithValue("@txtunitcost_acmdsp", dcmluntcstaccmds);
        dCmd.Parameters.AddWithValue("@txttotalAccmdnsp", dcmltotaccmds);
        dCmd.Parameters.AddWithValue("@txtnoofstaff_mealsp", intnoofstfmls);
        dCmd.Parameters.AddWithValue("@txtnoofdays_mealsp", intnoofdaysmls);
        dCmd.Parameters.AddWithValue("@txtunotcost_mealsp", dcmluntcstmls);
        dCmd.Parameters.AddWithValue("@txtTotalmealsp", dcmltotmls);
        dCmd.Parameters.AddWithValue("@TotolaDiscountp", dcmldiscount);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);

        return dCmd.ExecuteNonQuery();

    }
   
    public static int SaveInsertQuot_Detail_Contract(SqlConnection conn, int intQuotationtransid, int intQuotationid, int intlabid, int intEquipmentId, int intqty, decimal dcmlprice, decimal intdiscount, string strcalibtype, string strDesc, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("[sp_Quotation_Details_Contract]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@QuotationTransidp", intQuotationtransid);
        dCmd.Parameters.AddWithValue("@Quotationidp", intQuotationid);
        dCmd.Parameters.AddWithValue("@LabIdp", intlabid);
        dCmd.Parameters.AddWithValue("@EquipmentIdp", intEquipmentId);
        dCmd.Parameters.AddWithValue("@Qtyp", intqty);
        dCmd.Parameters.AddWithValue("@Pricep", dcmlprice);
        dCmd.Parameters.AddWithValue("@Discountp", intdiscount);
        dCmd.Parameters.AddWithValue("@Calibtypep", strcalibtype);
        dCmd.Parameters.AddWithValue("@Descp", strDesc);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);




        dCmd.Parameters.AddWithValue("@flagp", saveFlg);

        return dCmd.ExecuteNonQuery();

    }

    //---------------------------------Quotation------------------------------------------------------------

    public static int Quotation_Delete(SqlConnection conn, int Quotationidp, string strUserID)
    {

        SqlCommand dCmd = new SqlCommand("sp_Quotation_Delete", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@Quotationidp", Quotationidp);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        return dCmd.ExecuteNonQuery();

    }

    //----------------------------------------DIscount---------------------------------------------

    public static int Quotation_Discount(SqlConnection conn, int intQuotationid, string strstatus, decimal discount, string strfeedback, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Quotation_Discount", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@Quotationidp", intQuotationid);
        dCmd.Parameters.AddWithValue("@Status", strstatus);
        dCmd.Parameters.AddWithValue("@Quotdiscountp", discount);
        dCmd.Parameters.AddWithValue("@FeedBackp", strfeedback);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }


    //---------------------------------Calib_Save------------------------------------------------------------

    public static int Calib_Save(SqlConnection conn, int RECEIVED_TRANS_DETAIL_ID, int QUOTATION_ID, int CUSTOMER_ID, int EQUIPMENT_ID, string CalibDate, string NextCalibDate, string CertNo, string StickNo, string CertificateFile, string DataSheetFile, string MUSheetFile, string userid)
    {

        SqlCommand dCmd = new SqlCommand("sp_Calib_Save", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@RECEIVED_TRANS_DETAIL_ID", RECEIVED_TRANS_DETAIL_ID);
        dCmd.Parameters.AddWithValue("@QUOTATION_ID", QUOTATION_ID);
        dCmd.Parameters.AddWithValue("@CUSTOMER_ID", CUSTOMER_ID);
        dCmd.Parameters.AddWithValue("@EQUIPMENT_ID", EQUIPMENT_ID);
        dCmd.Parameters.AddWithValue("@CalibDate", CalibDate);
        dCmd.Parameters.AddWithValue("@NextCalibDate", NextCalibDate);
        dCmd.Parameters.AddWithValue("@CertNo", CertNo);
        dCmd.Parameters.AddWithValue("@StickNo", StickNo);
        dCmd.Parameters.AddWithValue("@CertificateFile", CertificateFile);
        dCmd.Parameters.AddWithValue("@DataSheetFile", DataSheetFile);
        dCmd.Parameters.AddWithValue("@MUSheetFile", MUSheetFile);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        return dCmd.ExecuteNonQuery();

    }

    public static int Calib_Upd_Del(SqlConnection conn, int RECEIVED_TRANS_DETAIL_ID, string CalibDate, string NextCalibDate, string CertificateFile,string userid, string saveFlag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Calibration_Upadte]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@RECEIVED_TRANS_DETAIL_ID", RECEIVED_TRANS_DETAIL_ID);
        dCmd.Parameters.AddWithValue("@CalibDate", CalibDate);
        dCmd.Parameters.AddWithValue("@NextCalibDate", NextCalibDate);
        dCmd.Parameters.AddWithValue("@CertificateFile", CertificateFile);
        dCmd.Parameters.AddWithValue("@userid", userid);
        dCmd.Parameters.AddWithValue("@saveflag", saveFlag);
        return dCmd.ExecuteNonQuery();
    }

    //---------------------------------Delivery_Save------------------------------------------------------------

    public static int Delivery_Save(SqlConnection conn, int RECEIVED_TRANS_DETAIL_ID, int Delivery, string DeliveryNo)
    {

        SqlCommand dCmd = new SqlCommand("sp_Delivery_Save", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@RECEIVED_TRANS_DETAIL_ID", RECEIVED_TRANS_DETAIL_ID);
        dCmd.Parameters.AddWithValue("@Delivery", Delivery);
        dCmd.Parameters.AddWithValue("@DeliveryNo", DeliveryNo);

        return dCmd.ExecuteNonQuery();

    }

    //---------------------------------invoice_Save------------------------------------------------------------

    public static int Invoice_Save(SqlConnection conn, string jobno, string Invoice_No, string Invoice_Date, int Customer_Id, int Quotation_Id, int RECEIVED_TRANS_DETAIL_ID, string userid)
    {

        SqlCommand dCmd = new SqlCommand("sp_Invoice_Save", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@jobno", jobno);
        dCmd.Parameters.AddWithValue("@Invoice_No", Invoice_No);
        dCmd.Parameters.AddWithValue("@Invoice_Date", Invoice_Date);
        dCmd.Parameters.AddWithValue("@Customer_Id", Customer_Id);
        dCmd.Parameters.AddWithValue("@Quotation_Id", Quotation_Id);
        dCmd.Parameters.AddWithValue("@RECEIVED_TRANS_DETAIL_ID", RECEIVED_TRANS_DETAIL_ID);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        return dCmd.ExecuteNonQuery();

    }
   
    //---------------------------------invoice_Delete------------------------------------------------------------

    public static int Invoice_Delete(SqlConnection conn, int RECEIVED_TRANS_DETAIL_ID, int Quotation_Id)
    {
        SqlCommand dCmd = new SqlCommand("sp_Invoice_Delete", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@RECEIVED_TRANS_DETAIL_ID", RECEIVED_TRANS_DETAIL_ID);
        dCmd.Parameters.AddWithValue("@Quotation_Id", Quotation_Id);
        return dCmd.ExecuteNonQuery();
    }

    //---------------------------------Job_Reject------------------------------------------------------------

    public static int Job_Reject(SqlConnection conn, int RECEIVED_TRANS_DETAIL_ID, string Reject, string RejectionDate, string ToReject, string YourRef)
    {

        SqlCommand dCmd = new SqlCommand("sp_Job_Reject", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@RECEIVED_TRANS_DETAIL_ID", RECEIVED_TRANS_DETAIL_ID);
        dCmd.Parameters.AddWithValue("@Reject", Reject);
        dCmd.Parameters.AddWithValue("@RejectionDate", RejectionDate);
        dCmd.Parameters.AddWithValue("@ToReject", ToReject);
        dCmd.Parameters.AddWithValue("@YourRef", YourRef);
        return dCmd.ExecuteNonQuery();

    }

    //---------------------------------AssignJob------------------------------------------------------------

    public static int AssignJob(SqlConnection conn, int RECEIVED_TRANS_DETAIL_ID, int AssignID)
    {

        SqlCommand dCmd = new SqlCommand("sp_AssignJob", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@RECEIVED_TRANS_DETAIL_ID", RECEIVED_TRANS_DETAIL_ID);
        dCmd.Parameters.AddWithValue("@AssignID", AssignID);
        return dCmd.ExecuteNonQuery();

    }

    //---------------------------------Equip_Condition------------------------------------------------------------

    public static int Equip_Condition(SqlConnection conn, String JOBNO, int RunningNo, String OwnerofItems, String physical, String functional)
    {

        SqlCommand dCmd = new SqlCommand("sp_Equip_Condition", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@JOBNO", JOBNO);
        dCmd.Parameters.AddWithValue("@RunningNo", RunningNo);
        dCmd.Parameters.AddWithValue("@OwnerofItems", OwnerofItems);
        dCmd.Parameters.AddWithValue("@physical", physical);
        dCmd.Parameters.AddWithValue("@functional", functional);

        return dCmd.ExecuteNonQuery();

    }

    public static int saveAssetManagement(SqlConnection conn, int EQUIPMENT_ID, string AssetNumber, string SerialNumber, string NewDate, double NewPrice, string Remarks, string userid)
    {
        SqlCommand dCmd = new SqlCommand("[sp_AssetManagement]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@EQUIPMENT_ID", EQUIPMENT_ID);
        dCmd.Parameters.AddWithValue("@AssetNumber", AssetNumber);
        dCmd.Parameters.AddWithValue("@SerialNumber", SerialNumber);
        dCmd.Parameters.AddWithValue("@NewDate", NewDate);
        dCmd.Parameters.AddWithValue("@NewPrice", NewPrice);
        dCmd.Parameters.AddWithValue("@Remarks", Remarks);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        return dCmd.ExecuteNonQuery();
    }

    //---------------------------------Register_Update------------------------------------------------------------

    public static int Register_Update(SqlConnection conn, int Quotation_Trans_ID, string userid, string Remarks_Detail)
    {

        SqlCommand dCmd = new SqlCommand("sp_Register", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@Quotation_Trans_ID", Quotation_Trans_ID);
        dCmd.Parameters.AddWithValue("@Remarks_Detail", Remarks_Detail);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        return dCmd.ExecuteNonQuery();

    }

    //--------------------------< Contract Details>--------------------------------------

    public static int SaveInsertContract(SqlConnection conn, int intcontractid, int intcustomerid, string strcontractno, DateTime dtcontrctdat, DateTime Dtexpirydate, string strremarks, string strbranchid, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Contract", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@Contrctidp", intcontractid);
        dCmd.Parameters.AddWithValue("@ContractNop", strcontractno);
        dCmd.Parameters.AddWithValue("@Customeridp", intcustomerid);
        dCmd.Parameters.AddWithValue("@Contractdatep", dtcontrctdat);
        dCmd.Parameters.AddWithValue("@Expirydatep", Dtexpirydate);
        dCmd.Parameters.AddWithValue("@Remarksp", strremarks);
        dCmd.Parameters.AddWithValue("@strbranchidp", strbranchid);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //---------------------------------------------------------------------------------------------
    //--------------------------< Contract Equipment Details>--------------------------------------

    public static int SaveInsertContractEquipmnet(SqlConnection conn, int intcontrctequipod, int intmastercntrctid, int intEquipmentId, int intinterval, string strserilano, string strmodelno, string strMaker, int intQty, string strBasedon, string strremarks, decimal dcmlprice, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_ContractEquipment", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ContrctEquipidp", intcontrctequipod);
        dCmd.Parameters.AddWithValue("@MasterContrctidp", intmastercntrctid);

        dCmd.Parameters.AddWithValue("@EquipmentIdp", intEquipmentId);
        dCmd.Parameters.AddWithValue("@intervalp", intinterval);
        dCmd.Parameters.AddWithValue("@Pricep", dcmlprice);

        dCmd.Parameters.AddWithValue("@Serialnop", strserilano);
        dCmd.Parameters.AddWithValue("@Modelnop", strmodelno);
        dCmd.Parameters.AddWithValue("@Remarksp", strremarks);

        dCmd.Parameters.AddWithValue("@MakerNop", strMaker);
        dCmd.Parameters.AddWithValue("@ContrctQtyp", intQty);
        dCmd.Parameters.AddWithValue("@ContrctFlagp", strBasedon);


        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //---------------------------------------------------------------------------------------------
    //--------------------------< Methods For Master Module >--------------------------------------

    public static SqlDataReader getMasterModule(SqlConnection conn)
    {
        int delval = 0;
        string sql = "select * FROM Menulist ";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getMasterModuleById(SqlConnection connect, string strModuleId)
    {
        int delval = 0;
        string sql = "select * FROM MasterModule WHERE Deleted='" + delval + "' and ModuleId='" + strModuleId + "' ORDER BY ModuleId";
        SqlCommand cmd = new SqlCommand(sql, connect);
        SqlDataReader reader1 = cmd.ExecuteReader();
        return reader1;

    }

    public static int DeleteModuleGrid(SqlConnection conn, string id)
    {
        SqlCommand dCmd = new SqlCommand("sp_MasterModule_Delete", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@moduleidp", id);
        return dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader checkModuleName(SqlConnection connCheck, string name)
    {
        SqlCommand cmd = new SqlCommand("sp_MasterModule_IsDuplicate", connCheck);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@modulenamep", name);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static int SaveModuleMaster(SqlConnection conn, string name, string desc, string appflag, string userid, string saveflag, string modid)
    {
        string sp_Name;
        string RowValue = "0";
        if (saveflag.ToString() == "N")
        {
            sp_Name = "[sp_MasterModule_Insert]";
        }
        else
        {
            sp_Name = "[sp_MasterModule_Update]";
        }
        SqlCommand dCmd = new SqlCommand(sp_Name, conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (saveflag.ToString() == "U")
        {
            dCmd.Parameters.AddWithValue("@idp", modid);
            dCmd.Parameters.AddWithValue("@Rowp", RowValue);
        }
        dCmd.Parameters.AddWithValue("@namep", name);
        dCmd.Parameters.AddWithValue("@descriptionp", desc);
        dCmd.Parameters.AddWithValue("@approvalflag", appflag);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        return dCmd.ExecuteNonQuery();
    }

    //---------------------------------------------------------------------------------------------
    //--------------------------< Methods For Master User >--------------------------------------

    public static SqlDataReader getMasterUserInfo(SqlConnection conn)
    {
        int delval = 0;
        string sql = "select * FROM Vw_MasterUser_Staff WHERE Deleted='" + delval + "'";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getMasterUserByID(SqlConnection conn, string strID)
    {
        int delval = 0;
        string sql = "select * FROM Vw_MasterUser_Staff WHERE ID='" + strID + "' and  Deleted='" + delval + "' ";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getUserNameByID(SqlConnection conn, string strID)
    {
        SqlCommand cmd = new SqlCommand("[sp_MasterUser_getUserName]", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@idp", strID);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static string getMasterUserIDByName(SqlConnection conn, string strName)
    {
        int delval = 0;
        string sql = "select ID FROM Vw_MasterUser_Staff WHERE UserName like '%" + strName + "%' and  Deleted='" + delval + "'";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        string ret = reader[0].ToString();
        BusinessTier.DisposeReader(reader);
        //BusinessTier.DisposeConnection(conn);
        return ret;
    }

    public static SqlDataReader getMasterUserByLoginId(SqlConnection conn, string strLoginId)
    {
        int delval = 0;
        string sql = "select * FROM Vw_MasterUser_Staff WHERE Deleted='" + delval + "' and LoginId='" + strLoginId + "'";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getMasterModulePermisnByUserId(SqlConnection connModulePermission, string strUserId)
    {
        int delval = 0;
        string sql = "select * FROM vw_MasterModulePermission_MasterModuleByModuleID WHERE Deleted='" + delval + "' and UserId='" + strUserId.ToString() + "' order by modulename";
        SqlCommand cmd = new SqlCommand(sql, connModulePermission);
        SqlDataReader readerModulePermission = cmd.ExecuteReader();
        return readerModulePermission;
    }

    public static int DeleteUserGrid(SqlConnection conn, string id)
    {
        SqlCommand dCmd = new SqlCommand("[sp_MasterUser_Delete]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@masteruseridp", id);
        return dCmd.ExecuteNonQuery();
    }

    public static int SaveUserMaster(SqlConnection connSave, int intstaffid, string strloginid, string strpass, string strCreatedByID, string strSaveFlag, string strCurrUserId)
    {
        string sp_Name;
        if (strSaveFlag.ToString() == "Insert")
        {
            sp_Name = "[sp_MasterUser_Insert]";
        }
        else
        {
            sp_Name = "[sp_MasterUser_Update]";
        }
        SqlCommand dCmd = new SqlCommand(sp_Name, connSave);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (strSaveFlag.ToString() == "Update")
        {
            dCmd.Parameters.AddWithValue("@idp", strCurrUserId);
        }
        dCmd.Parameters.AddWithValue("@loginidp", strloginid);
        dCmd.Parameters.AddWithValue("@passp", strpass);
        dCmd.Parameters.AddWithValue("@Staffidp", intstaffid);
        // dCmd.Parameters.AddWithValue("@isapprovalrqrdp", strapprqrd);
        //  dCmd.Parameters.AddWithValue("@isnotifyrqrd", strnotifyrqrd);
        dCmd.Parameters.AddWithValue("@useridp", strCreatedByID);
        return dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader checkUserLoginId(SqlConnection connCheck, string strLoginId)
    {
        SqlCommand cmd = new SqlCommand("[sp_MasterUser_IsDuplicate]", connCheck);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@loginidp", strLoginId);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader checkUserApprovalByUserId(SqlConnection connectUserAprvl, long lnguserid)
    {
        SqlCommand cmd = new SqlCommand("sp_MasterUserApproval_CheckUserId", connectUserAprvl);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@useridp", lnguserid);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static int SaveUserMasterApproval(SqlConnection connSave, long intloginid, long intlinebyline, string struserid)
    {
        SqlCommand dCmd = new SqlCommand("[sp_MasterUserApproval_Save]", connSave);
        dCmd.CommandType = CommandType.StoredProcedure;

        dCmd.Parameters.AddWithValue("@loginidp", intloginid);
        dCmd.Parameters.AddWithValue("@approvalp", intlinebyline);
        dCmd.Parameters.AddWithValue("@useridp", struserid);
        return dCmd.ExecuteNonQuery();
    }

    public static int SaveUserMasterModulePermission(SqlConnection connSave, long intloginid, long intlinebyline, string struserid)
    {
        SqlCommand dCmd = new SqlCommand("[sp_MasterUserModulePermission_Save]", connSave);
        dCmd.CommandType = CommandType.StoredProcedure;

        dCmd.Parameters.AddWithValue("@loginidp", intloginid);
        dCmd.Parameters.AddWithValue("@moduleidp", intlinebyline);
        // dCmd.Parameters.AddWithValue("@appflag", "Y");
        dCmd.Parameters.AddWithValue("@useridp", struserid);
        return dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader getPlatformInfo_ByUserID(SqlConnection connGetID, string id)
    {
        SqlCommand dCmd = new SqlCommand("sp_MasterPlatform_GetAllByUserId", connGetID);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@UserIDp", id);
        SqlDataReader reader = dCmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getMail_ByPlatformID(SqlConnection conn, string platformid)
    {
        SqlCommand dCmd = new SqlCommand("[sp_MasterUser_getEmailByPlatformID]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@Platformidp", platformid);
        SqlDataReader reader = dCmd.ExecuteReader();
        return reader;
    }

    //---------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------

    public static void BindErrorMessageDetails(SqlConnection connError)
    {
        string sql = "select * FROM MasterErrorMessage order by OrderNo";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, connError);
        g_ErrorMessagesDataTable = new DataTable();
        sqlDataAdapter.Fill(g_ErrorMessagesDataTable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
    }

    public static void InsertLogAuditTrial(SqlConnection connLog, string userid, string module, string activity, string result, string flag)
    {
        string sp_Name;
        if (flag.ToString() == "Log")
        {
            sp_Name = "[sp_Master_Insert_Log]";
        }
        else
        {
            sp_Name = "[sp_Master_Insert_AuditTrail]";
        }

        SqlCommand dCmd = new SqlCommand(sp_Name, connLog);

        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@modulep", module);
        dCmd.Parameters.AddWithValue("@activityp", activity);
        dCmd.Parameters.AddWithValue("@resultp", result);
        dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader getMenuList(SqlConnection conn)
    {
        string sql = "select Category, seqCategory from MenuList group by Category,seqCategory order by seqCategory";

        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getMenuList(SqlConnection conn, string strUserId, string usertype)
    {
        string sql = "";
        //  if (usertype.ToString().Trim() == "admin")
        sql = "select Category, seqCategory FROM MenuList group by Category,seqCategory order by seqCategory";
        //  else
        //   sql = "select Category, seqCategory from vw_MenuList_MasterModulePermission  where UserId='" + strUserId + "' group by Category,seqCategory order by seqCategory";

        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static DataTable getSubMenuItems(string category, string uid, string usertype)
    {
        DataTable ret = new DataTable();
        SqlConnection conn = getConnection();
        conn.Open();
        string sql = "";
        //if (usertype.ToString().Trim() == "admin")
        //{
        //    sql = "select ModuleID, Href, Menulist FROM MenuList where Category = '" + category + "' order by seqMenu";
        //}
        //else
        //{
        sql = "select ModuleID, Href, Menulist FROM vw_MenuList_MasterModulePermission where Category = '" + category + "' and staff_id='" + uid + "' and Deleted=0 order by seqMenu";
        //}
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        ret.Load(reader);
        BusinessTier.DisposeConnection(conn);
        return ret;
    }

    //------------------------AutoQuotation----------------------------
   
    public static int saveQuotationAuto(SqlConnection connMRVAuto, string strBranchId, string strAutoNo, string strYear, string strLastAutoNo, string saveFlag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_AutoQuotation_Save]", connMRVAuto);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@BranchIDp", strBranchId);
        dCmd.Parameters.AddWithValue("@strAutonop", strAutoNo);
        dCmd.Parameters.AddWithValue("@strYearp", strYear);
        dCmd.Parameters.AddWithValue("@strLastAutoNop", strLastAutoNo);
        dCmd.Parameters.AddWithValue("@saveflagp", saveFlag);
        return dCmd.ExecuteNonQuery();
    }

    public static int saveInvoiceAuto(SqlConnection connMRVAuto, string strBranchId, string strAutoNo, string strYear, string strLastAutoNo, string saveFlag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_InvoiceAuto_Save]", connMRVAuto);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@BranchIDp", strBranchId);
        dCmd.Parameters.AddWithValue("@strAutonop", strAutoNo);
        dCmd.Parameters.AddWithValue("@strYearp", strYear);
        dCmd.Parameters.AddWithValue("@strLastAutoNop", strLastAutoNo);
        dCmd.Parameters.AddWithValue("@saveflagp", saveFlag);
        return dCmd.ExecuteNonQuery();
    }

    public static int saveEnquiryAuto(SqlConnection connMRVAuto, string strBranchId, string strAutoNo, string strYear, string strLastAutoNo, string saveFlag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_AutoEnquiry_Save]", connMRVAuto);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@BranchIDp", strBranchId);
        dCmd.Parameters.AddWithValue("@strAutonop", strAutoNo);
        dCmd.Parameters.AddWithValue("@strYearp", strYear);
        dCmd.Parameters.AddWithValue("@strLastAutoNop", strLastAutoNo);
        dCmd.Parameters.AddWithValue("@saveflagp", saveFlag);
        return dCmd.ExecuteNonQuery();
    }

    public static int saveCertificateAuto(SqlConnection connMRVAuto, string strBranchId, string strAutoNo, string strYear, string strLastAutoNo, string saveFlag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_AutoCertificate_Save]", connMRVAuto);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@BranchIDp", strBranchId);
        dCmd.Parameters.AddWithValue("@strAutonop", strAutoNo);
        dCmd.Parameters.AddWithValue("@strYearp", strYear);
        dCmd.Parameters.AddWithValue("@strLastAutoNop", strLastAutoNo);
        dCmd.Parameters.AddWithValue("@saveflagp", saveFlag);
        return dCmd.ExecuteNonQuery();
    }

    //---------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------
    //----------------------MISC------------------------------------------------------------------

    public static string getCCMailID(string strModule)
    {
        //string strEmailFile = ConfigurationManager.AppSettings["Email_CC_FilePath"].ToString();
        //string strMailCC = "fahimy@sirim.my";

        string strMailCC = "sara@e-serbadk.com";

        //if (File.Exists(strEmailFile))
        //{
        //    string strLine = "";
        //    string[] strLine1 = new string[1];
        //    int counter = 0;
        //    StreamReader reader = new StreamReader(strEmailFile);
        //    while ((strLine = reader.ReadLine()) != null)
        //    {
        //        if (counter == 0)
        //        {
        //            strLine1 = strLine.Split(':');

        //            if (strLine1[0].ToString().Trim() == strModule.ToString().Trim())
        //            {
        //                strMailCC = strLine1[1].ToString().Trim();
        //                counter = 1;
        //            }
        //        }
        //    }
        //    reader.Close();
        //    reader.Dispose();
        //}
        return strMailCC.ToString().Trim();
    }

    public static void SendMail(string strSubject, string strBody, string strToAddress, string strApprovarMail, string strAttachmentFilename)
    {
        //SmtpClient smtpClient = new SmtpClient();
        //MailMessage message = new MailMessage();
        //if (!(strAttachmentFilename.ToString().Trim() == "NoAttach"))
        //{
        //    Attachment attachment = new Attachment(strAttachmentFilename.ToString().Trim());
        //    message.Attachments.Add(attachment);
        //}
        //MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["FromAddress"].ToString(), "LSB Asset Tracking System");
        //smtpClient.Host = ConfigurationManager.AppSettings["ExchangeServer"].ToString();
        //smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString());

        //message.Priority = MailPriority.High;
        //message.From = fromAddress;
        //message.Subject = strSubject.ToString();
        //message.To.Add(strToAddress.ToString());
        //message.CC.Add(strApprovarMail.ToString());
        ////message.CC.Add("bala@e-serbadk.com");
        ////message.CC.Add("karthi@e-serbadk.com");
        ////message.CC.Add("fadzli_mzain@yahoo.com");
        ////message.CC.Add("zuhaifi.mghani@iperintis.com");
        ////message.CC.Add("nurlisa_ahmad@petronas.com.my");
        //message.Body = strBody;
        ////smtpClient.EnableSsl = true;
        ////smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        ////smtpClient.UseDefaultCredentials = false;
        //smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FromAddress"].ToString(), ConfigurationManager.AppSettings["Password"].ToString().Trim());
        ////smtpClient.Send(message);
        //message.Dispose();
        //smtpClient.Dispose();
        //File.Delete(strAttachmentFilename.ToString().Trim());
    }

}