﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Reflection;
using System.Net.Mail;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Data.Common;

using Stimulsoft.Report;
using Stimulsoft.Report.Web;
using Stimulsoft.Report.Viewer;
using Stimulsoft.Report.SaveLoad;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Print;
using Stimulsoft.Base;
using Stimulsoft.Controls;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Controls;

using System.IO.MemoryMappedFiles;

public partial class Report_Quotation : System.Web.UI.Page
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
            string appPath = Request.PhysicalApplicationPath;
            string srtParamval1 = "";
            srtParamval1 = Request.QueryString.Get("param1").ToString();
            Int64 lngMasterId = Int64.Parse(srtParamval1.ToString());
            string strAttachmentpath = ConfigurationManager.AppSettings["WC_AttachementPath"].ToString();
            string strMessage = "";
            string strSubject = "";

            string con = BusinessTier.getConnection1();
            Stimulsoft.Report.StiReport stiReport1;
            string strsql = "";


            strsql = "SELECT *,transport=(Select sum(price) as Price from Vw_QuotationDetail_Equipment where Qty=0 and deleted=0 and Quotation_Id='" + srtParamval1 + "') from Vw_QuotationDetail_Equipment where Quotation_Id='" + srtParamval1 + "' and deleted=0 and Qty<>0 ";

            SqlDataAdapter ad = new SqlDataAdapter(strsql, con);


            DataSet ds = new DataSet();
            ds.DataSetName = "DynamicDataSource";

            ds.Tables.Add("Vw_QuotationDetail_Equipment");
            ad.Fill(ds, "Vw_QuotationDetail_Equipment");
            stiReport1 = new StiReport();
            stiReport1.Dictionary.DataStore.Clear();
            stiReport1.Load(appPath + "\\Reports\\Quotation.mrt");
            stiReport1.Dictionary.Databases.Clear();
            stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
            stiReport1.Dictionary.DataSources.Clear();
            stiReport1.RegData("Vw_QuotationDetail_Equipment", ds);
            stiReport1.Dictionary.Synchronize();
            stiReport1.Compile();
            StiWebViewer1.Report = stiReport1;
            StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;


        }
        catch (Exception Ex)
        {
            lblStatus.Text = Ex.Message.ToString();
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "PageInit", "Report_Quotation", Ex.ToString(), "Audit");
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
    }


    protected void btnEmail_Click(object sender, EventArgs e)
    {
        System.IO.MemoryStream stream = null;
        SmtpClient smtp = new SmtpClient();
        MailMessage item = new MailMessage();

        Stimulsoft.Report.StiReport report = default(Stimulsoft.Report.StiReport);
        try
        {
            //Get Report
            report = StiWebViewer1.Report;
            //Create Mail Message

            MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["FromAddress"].ToString(), "SST");
            item.From = fromAddress;


                item.Subject = "SST - New Quotation";

                item.Body = "Please Find herewith Enclosed the New Quotation. " + "\r\n" + "\r\n" + "  " + "\r\n" + "  " + "\r\n" + "\r\n" + "\r\n" + "Note: " + "\r\n" + "*. Do not reply to this mail" + "\r\n" + "*. This is system generated mail " + "\r\n" + "http://218.111.224.242/SIRIM/Login.aspx";

          

            string strCCMail = BusinessTier.getCCMailID("SIRIM");
            item.To.Add(strCCMail.ToString());
            stream = new System.IO.MemoryStream();
            report.ExportDocument(Stimulsoft.Report.StiExportFormat.Pdf, stream);
            stream.Seek(0, SeekOrigin.Begin);
            Attachment attachment = new Attachment(stream, "MyReport.pdf", "application/pdf");
            item.Attachments.Add(attachment);

            //Create SMTP Client
            smtp.Host = ConfigurationManager.AppSettings["ExchangeServer"].ToString();
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString());


            smtp.Send(item);
            item.Dispose();
            smtp.Dispose();
     
           lblStatus.Text="Email Send Successfully";

        }
        catch (Exception ex)
        {
            lblStatus.Text="An error occured while trying to generate report, Please try again/Contact your administrator";
            // lblStatus.Text = "An error occured while trying to generate report.";
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


}