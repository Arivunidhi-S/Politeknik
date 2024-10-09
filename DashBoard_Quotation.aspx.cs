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
//using System.Web.UI.DataVisualization.Charting;
public partial class DashBoard_Quotation : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                var yr = DateTime.Today.Year;
                var mth = DateTime.Today.Month;
                var firstDay = new DateTime(yr, mth, 1);
                DtFromDate.SelectedDate = firstDay;
                DtToDate.SelectedDate = DateTime.Now;
                txtyear.Text = Convert.ToString(DateTime.Now.Year);
                generateReport();

                //    if (string.IsNullOrEmpty(Session["sesUserID"].ToString()))
                //    {
                //        Response.Redirect("Login.aspx");
                //    }
                //    else
                //    {
                //      //  lblStatus.Text = "";

                //    }
                //}

            }

        }
        catch (Exception ex)
        {
            // Response.Redirect("Login.aspx");
        }
    }

    //protected void CreateChart();
    //{
    //   // Create some dummy Data
    //    Random random = new Random();
    //    //for (int pointIndex = 0; pointIndex < 10; pointIndex++)"Series1"].Points.AddY(random.Next(20, 100));//Set the chart
    //    //Chart1.Series["Series1"].ChartType = SeriesChartType.Pie;// Set the bar width
    //    //Chart1.Series["Series1"]["PointWidth"] = "0.5";// Show data points 
    //    //Chart1.Series["Series1"].IsValueShownAsLabel = true;// Set data points label style
    //    //Chart1.Series["Series1"]["BarLabelStyle"] = "Center";// Show chart as 3D
    //    //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;// Draw chart as 3D Cylinder
    //    //Chart1.Series["Series1"]["DrawingStyle"] = "Cylinder";
    //}
    protected void btnJan_OnClick(object sender, EventArgs e)
    {
        try
        {
            var yr = Convert.ToInt32(txtyear.Text);
            var mth = DateTime.Today.Month;
            var firstDay = new DateTime(yr, 1, 1);
            var lastDay = new DateTime(yr, 1, 31);
            DtFromDate.SelectedDate = firstDay;
            DtToDate.SelectedDate = lastDay;
            generateReport();
        }

        catch (Exception ex)
        {

        }
    }

    protected void btnFeb_OnClick(object sender, EventArgs e)
    {
        try
        {
            var yr = Convert.ToInt32(txtyear.Text);
            var mth = DateTime.Today.Month;
            var firstDay = new DateTime(yr, 2, 1);
            var lastDay = new DateTime(yr, 2, 28);
            DtFromDate.SelectedDate = firstDay;
            DtToDate.SelectedDate = lastDay;
            generateReport();
        }

        catch (Exception ex)
        {

        }
    }
    protected void btnMar_OnClick(object sender, EventArgs e)
    {
        try
        {
            var yr = Convert.ToInt32(txtyear.Text);
            var mth = DateTime.Today.Month;
            var firstDay = new DateTime(yr, 3, 1);
            var lastDay = new DateTime(yr, 3, 31);
            DtFromDate.SelectedDate = firstDay;
            DtToDate.SelectedDate = lastDay;
            generateReport();
        }

        catch (Exception ex)
        {

        }
    }

    protected void btnApr_OnClick(object sender, EventArgs e)
    {
        try
        {
            var yr = Convert.ToInt32(txtyear.Text);
            var mth = DateTime.Today.Month;
            var firstDay = new DateTime(yr, 4, 1);
            var lastDay = new DateTime(yr, 4, 30);
            DtFromDate.SelectedDate = firstDay;
            DtToDate.SelectedDate = lastDay;
            generateReport();
        }

        catch (Exception ex)
        {

        }
    }

    protected void btnMay_OnClick(object sender, EventArgs e)
    {
        try
        {
            var yr = Convert.ToInt32(txtyear.Text);
            var mth = DateTime.Today.Month;
            var firstDay = new DateTime(yr, 5, 1);
            var lastDay = new DateTime(yr, 5, 31);
            DtFromDate.SelectedDate = firstDay;
            DtToDate.SelectedDate = lastDay;
            generateReport();
        }

        catch (Exception ex)
        {

        }
    }

    protected void btnJun_OnClick(object sender, EventArgs e)
    {
        try
        {
            var yr = Convert.ToInt32(txtyear.Text);
            var mth = DateTime.Today.Month;
            var firstDay = new DateTime(yr, 6, 1);
            var lastDay = new DateTime(yr, 6, 30);
            DtFromDate.SelectedDate = firstDay;
            DtToDate.SelectedDate = lastDay;
            generateReport();
        }

        catch (Exception ex)
        {

        }
    }


    protected void btnJuly_OnClick(object sender, EventArgs e)
    {
        try
        {
            var yr = Convert.ToInt32(txtyear.Text);
            var mth = DateTime.Today.Month;
            var firstDay = new DateTime(yr, 7, 1);
            var lastDay = new DateTime(yr, 7, 31);
            DtFromDate.SelectedDate = firstDay;
            DtToDate.SelectedDate = lastDay;
            generateReport();
        }

        catch (Exception ex)
        {

        }
    }

    protected void btnAug_OnClick(object sender, EventArgs e)
    {
        try
        {
            var yr = Convert.ToInt32(txtyear.Text);
            var mth = DateTime.Today.Month;
            var firstDay = new DateTime(yr, 8, 1);
            var lastDay = new DateTime(yr, 8, 31);
            DtFromDate.SelectedDate = firstDay;
            DtToDate.SelectedDate = lastDay;
            generateReport();
        }

        catch (Exception ex)
        {

        }
    }

    protected void btnSep_OnClick(object sender, EventArgs e)
    {
        try
        {
            var yr = Convert.ToInt32(txtyear.Text);
            var mth = DateTime.Today.Month;
            var firstDay = new DateTime(yr, 9, 1);
            var lastDay = new DateTime(yr, 9, 30);
            DtFromDate.SelectedDate = firstDay;
            DtToDate.SelectedDate = lastDay;
            generateReport();
        }

        catch (Exception ex)
        {

        }
    }

    protected void btnOct_OnClick(object sender, EventArgs e)
    {
        try
        {
            var yr = Convert.ToInt32(txtyear.Text);
            var mth = DateTime.Today.Month;
            var firstDay = new DateTime(yr, 10, 1);
            var lastDay = new DateTime(yr, 10, 31);
            DtFromDate.SelectedDate = firstDay;
            DtToDate.SelectedDate = lastDay;
            generateReport();
        }

        catch (Exception ex)
        {

        }
    }
    protected void btnNov_OnClick(object sender, EventArgs e)
    {
        try
        {
            var yr = Convert.ToInt32(txtyear.Text);
            var mth = DateTime.Today.Month;
            var firstDay = new DateTime(yr, 11, 1);
            var lastDay = new DateTime(yr, 11, 30);
            DtFromDate.SelectedDate = firstDay;
            DtToDate.SelectedDate = lastDay;
            generateReport();
        }

        catch (Exception ex)
        {

        }
    }

    protected void btnDec_OnClick(object sender, EventArgs e)
    {
        try
        {
            var yr = Convert.ToInt32(txtyear.Text);
            var mth = DateTime.Today.Month;
            var firstDay = new DateTime(yr, 12, 1);
            var lastDay = new DateTime(yr, 12, 31);
            DtFromDate.SelectedDate = firstDay;
            DtToDate.SelectedDate = lastDay;
            generateReport();
        }

        catch (Exception ex)
        {

        }
    }


    protected void btnsubmit_OnClick(object sender, EventArgs e)
    {
        generateReport();
    }
    protected void generateReport()
    {
        try
        {
            String strfrmdate = null, strtodate = null, strlessthanthree = null, strlfourtoseven = null, strgrtrthnseven = null;
            DateTime dtFromDatecon = DtFromDate.SelectedDate.Value;
            strfrmdate = dtFromDatecon.Month + "/" + dtFromDatecon.Day + "/" + dtFromDatecon.Year + " 12:00:00 AM";

            DateTime dtToDatecon = DtToDate.SelectedDate.Value;
            strtodate = dtToDatecon.Month + "/" + dtToDatecon.Day + "/" + dtToDatecon.Year + " 12:00:00 PM";

            DateTime dtToDatecon1 = dtToDatecon.AddDays(-3);
            DateTime dtToDatecon2 = dtToDatecon.AddDays(-7);
            strlessthanthree = dtToDatecon1.Month + "/" + dtToDatecon1.Day + "/" + dtToDatecon1.Year + " 12:00:00 AM";
            strgrtrthnseven = dtToDatecon2.Month + "/" + dtToDatecon2.Day + "/" + dtToDatecon2.Year + " 12:00:00 PM";

            //  strlfourtoseven = dtToDatecon.Month + "/" + dtToDatecon.AddDays(-7) + "/" + dtToDatecon.Year + " 12:00:00 PM";

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();

            // *****************************Quotation************************************************

            // select count (Enquiry_id) as Enquiry_id, Dept_Name from Vw_Enquiry_Staff_Department where  branch_Id='6' and deleted=0 and Enquiry_Date between '06/1/2014 12:00:00 AM' and '10/11/2014 12:00:00 PM' group by Dept_id,Dept_Name

            //select count (Enquiry_id) as Enquiry_id, Staff_Name from Vw_Enquiry_Staff_Department where  branch_Id='6' and deleted=0 and Enquiry_Date between '06/1/2014 12:00:00 AM' and '10/11/2014 12:00:00 PM' group by Dept_id,Staff_Name

            string sql = "Select totQuotid=(select count(Quotation_ID) as Quotation_ID from Quotation where branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "'), pendgquot=(select count(Quotation_ID) as Quotation_ID from Quotation where  Quot_status='PENDING'  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "'), CompleteQuot=(select count(Quotation_ID) as Quotation_ID from Quotation where  Quot_status='COMPLETE'  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' ),unsuccesQuot=(select count(Quotation_ID) as Quotation_ID from Quotation where  Quot_status='UNSUCCESSFULL'  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' ),totQuotidRM=(select sum(price * Qty) as Quotation_ID from vw_Quotation where branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "'), pendgquotRM=(select sum(price * Qty) as Quotation_ID from vw_Quotation where  Quot_status='PENDING'  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "'), CompleteQuotRM=(select sum(price * Qty) as Quotation_ID from vw_Quotation where  Quot_status='COMPLETE'  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' ),unsuccesQuotRM=(select sum(price * Qty) as Quotation_ID from vw_Quotation where  Quot_status='UNSUCCESSFULL'  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' ) ";

            SqlCommand command = new SqlCommand(sql, conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                lblQuotsttus.Text = reader["totQuotid"].ToString();
                lblQuotPending.Text = reader["pendgquot"].ToString();
                lblQuotComplete.Text = reader["CompleteQuot"].ToString();
                lblQuotUnsucces.Text = reader["unsuccesQuot"].ToString();

                lblQuotsttusRM.Text = reader["totQuotidRM"].ToString();
                lblQuotPendingRM.Text = reader["pendgquotRM"].ToString();
                lblQuotCompleteRM.Text = reader["CompleteQuotRM"].ToString();
                lblQuotUnsuccesRM.Text = reader["unsuccesQuotRM"].ToString();

                if (string.IsNullOrEmpty(lblQuotsttusRM.Text))
                {
                    lblQuotsttusRM.Text = "0";
                }
                if (string.IsNullOrEmpty(lblQuotPendingRM.Text))
                {
                    lblQuotPendingRM.Text = "0";
                }
                if (string.IsNullOrEmpty(lblQuotCompleteRM.Text))
                {
                    lblQuotCompleteRM.Text = "0";
                }
                if (string.IsNullOrEmpty(lblQuotUnsuccesRM.Text))
                {
                    lblQuotUnsuccesRM.Text = "0";
                }

                lblpendsts.Text = reader["pendgquot"].ToString();
                lblUnSusts.Text = reader["CompleteQuot"].ToString();
                lblCmplsts.Text = reader["unsuccesQuot"].ToString();

                int inttotsts = Convert.ToInt32(lblQuotPending.Text) + Convert.ToInt32(lblQuotComplete.Text) + Convert.ToInt32(lblQuotUnsucces.Text);
                lblTotstatus.Text = Convert.ToString(inttotsts);


                double dblpending = ((Convert.ToDouble(lblQuotPending.Text) / Convert.ToDouble(lblQuotsttus.Text)) * 100);
                lblenqrypendingPercn.Text = Convert.ToString(Math.Round(dblpending, 0));
                lblpendstspercn.Text = Convert.ToString(Math.Round(dblpending, 0));
                double dblComplete = ((Convert.ToDouble(lblQuotComplete.Text) / Convert.ToDouble(lblQuotsttus.Text)) * 100);
                lblenqryCompletePercn.Text = Convert.ToString(Math.Round(dblComplete, 0));
                lblCmplstspercn.Text = Convert.ToString(Math.Round(dblComplete, 0));
                double dblunsucess = ((Convert.ToDouble(lblQuotUnsucces.Text) / Convert.ToDouble(lblQuotsttus.Text)) * 100);
                lblenqryunsuccesPercn.Text = Convert.ToString(Math.Round(dblunsucess, 0));
                lblUnSustspercn.Text = Convert.ToString(Math.Round(dblunsucess, 0));






            }
            BusinessTier.DisposeReader(reader);

            //Calibration 

            string sql2 = "Select totQuotid=(select count(Quotation_ID) as Quotation_ID from Quotation where branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "'), Calib=(select count(Quotation_ID) as Quotation_ID from Quotation INNER JOIN Enquiry ON Quotation.ENQUIRY_NO = Enquiry.Enquiry_ID where Enquiry.Enquiry_Type='Calibration' and  Quotation.branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and Quotation.deleted=0 and Quotation.QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' ),Inspec=(select count(Quotation_ID) as Quotation_ID from Quotation INNER JOIN Enquiry ON Quotation.ENQUIRY_NO = Enquiry.Enquiry_ID where Enquiry.Enquiry_Type='Inspection' and  Quotation.branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and Quotation.deleted=0 and Quotation.QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' ),Training=(select count(Quotation_ID) as Quotation_ID from Quotation INNER JOIN Enquiry ON Quotation.ENQUIRY_NO = Enquiry.Enquiry_ID where Enquiry.Enquiry_Type='Training' and  Quotation.branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and Quotation.deleted=0 and Quotation.QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "'),Others=(select count(Quotation_ID) as Quotation_ID from Quotation INNER JOIN Enquiry ON Quotation.ENQUIRY_NO = Enquiry.Enquiry_ID where Enquiry.Enquiry_Type='Others' and  Quotation.branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and Quotation.deleted=0 and Quotation.QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "'), totunits=(select count(Quotation_ID) as Quotation_ID from Vw_Quotation_Enquiry where   branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' ), calibunits=(select count(Quotation_ID) as Quotation_ID from Vw_Quotation_Enquiry where  Enquiry_Type='Calibration'  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' ), Inspecnunits=(select count(Quotation_ID) as Quotation_ID from Vw_Quotation_Enquiry where  Enquiry_Type='Inspection'  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' ),trainunits=(select count(Quotation_ID) as Quotation_ID from Vw_Quotation_Enquiry where  Enquiry_Type='Training'  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' ),Otherunits=(select count(Quotation_ID) as Quotation_ID from Vw_Quotation_Enquiry where  Enquiry_Type='Others'  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' ),totRM=(select sum(price * Qty) as Quotation_ID from Vw_Quotation_Enquiry where   branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' ), calibRM=(select sum(price * Qty) as Quotation_ID from Vw_Quotation_Enquiry where  Enquiry_Type='Calibration'  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' ), InspecRM=(select sum(price * Qty) as Quotation_ID from Vw_Quotation_Enquiry where  Enquiry_Type='Inspection'  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' ),trainRM=(select sum(price * Qty) as Quotation_ID from Vw_Quotation_Enquiry where  Enquiry_Type='Training'  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' ),OtherRM=(select sum(price * Qty) as Quotation_ID from Vw_Quotation_Enquiry where  Enquiry_Type='Others'  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' ), DiscontQuot=(select count(Quotation_ID) as Quotation_ID from Quotation where  Quot_status='DISCOUNT'  and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' )  ";

            SqlCommand command2 = new SqlCommand(sql2, conn);
            SqlDataReader reader2 = command2.ExecuteReader();
            if (reader2.Read())
            {

                lbltotalrecd.Text = reader2["totQuotid"].ToString();
                lblcalib.Text = reader2["Calib"].ToString();
                lblinspec.Text = reader2["Inspec"].ToString();
                lblTraining.Text = reader2["Training"].ToString();
                lblOthers.Text = reader2["Others"].ToString();

                lblclibrcdind.Text = reader2["Calib"].ToString();
                lblInsprcdind.Text = reader2["Inspec"].ToString();
                lbltrngrcdind.Text = reader2["Training"].ToString();
                lblothrrcdind.Text = reader2["Others"].ToString();

                lbltotalrecdunits.Text = reader2["totunits"].ToString();
                lblcalibunits.Text = reader2["calibunits"].ToString();
                lblinspecunits.Text = reader2["Inspecnunits"].ToString();
                lblTrainingunits.Text = reader2["trainunits"].ToString();
                lblOthersunits.Text = reader2["Otherunits"].ToString();

                //double dblval = Convert.ToDouble(reader2["totRM"].ToString());
                // lbltotalrecdRM.Text = dblval.ToString("#,##0,K", CultureInfo.InvariantCulture);
                //lbltotalrecdRM.Text = reader2["totRM"].ToString("#,##0,K", CultureInfo.InvariantCulture);

                lbltotalrecdRM.Text = reader2["totRM"].ToString();
                lblcalibunitsRM.Text = reader2["calibRM"].ToString();
                lblinspecRM.Text = reader2["InspecRM"].ToString();
                lblTrainingRM.Text = reader2["trainRM"].ToString();
                lblOthersRM.Text = reader2["OtherRM"].ToString();

                if(string.IsNullOrEmpty(lbltotalrecdRM.Text))
                {
                    lbltotalrecdRM.Text = "0";
                }
                if (string.IsNullOrEmpty(lblcalibunitsRM.Text))
                {
                    lblcalibunitsRM.Text = "0";
                }
                if (string.IsNullOrEmpty(lblinspecRM.Text))
                {
                    lblinspecRM.Text = "0";
                }
                if (string.IsNullOrEmpty(lblTrainingRM.Text))
                {
                    lblTrainingRM.Text = "0";
                }
                if (string.IsNullOrEmpty(lblOthersRM.Text))
                {
                    lblOthersRM.Text = "0";
                }

                int inttot = Convert.ToInt32(lblclibrcdind.Text) + Convert.ToInt32(lblInsprcdind.Text) + Convert.ToInt32(lbltrngrcdind.Text) + Convert.ToInt32(lblothrrcdind.Text);
                lbltotRecd.Text = Convert.ToString(inttot);

                double dblcalib = ((Convert.ToDouble(lblcalib.Text) / Convert.ToDouble(lblQuotsttus.Text)) * 100);
                lblcalibPercn.Text = Convert.ToString(Math.Round(dblcalib, 0));
                lblclibrcdindperc.Text = Convert.ToString(Math.Round(dblcalib, 0));
                double dblinspec = ((Convert.ToDouble(lblinspec.Text) / Convert.ToDouble(lblQuotsttus.Text)) * 100);
                lblinspecPercn.Text = Convert.ToString(Math.Round(dblinspec, 0));
                lblInsprcdindperc.Text = Convert.ToString(Math.Round(dblinspec, 0));
                double dblTraining = ((Convert.ToDouble(lblTraining.Text) / Convert.ToDouble(lblQuotsttus.Text)) * 100);
                lblTrainingPercn.Text = Convert.ToString(Math.Round(dblTraining, 0));
                lbltrngrcdindperc.Text = Convert.ToString(Math.Round(dblTraining, 0));
                double dblOthers = ((Convert.ToDouble(lblOthers.Text) / Convert.ToDouble(lblQuotsttus.Text)) * 100);
                lblOthersPercn.Text = Convert.ToString(Math.Round(dblOthers, 0));
                lblOthrrcdindperc.Text = Convert.ToString(Math.Round(dblOthers, 0));
            }
            BusinessTier.DisposeReader(reader2);


            //Received CAL

            string sql1 = "Select totQuotidRecd=(select count(Quotation_Trans_ID) as Quotation_ID from vw_Quotation where branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' and qty<>0),Inlab=(select count(Quotation_Trans_ID) as Quotation_ID from vw_Quotation where branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and calib_type='Inhouse' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "'),Onsite=(select count(Quotation_Trans_ID) as Quotation_ID from vw_Quotation where branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and calib_type='Onsite' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "'),Subconrt=(select count(Quotation_Trans_ID) as Quotation_ID from vw_Quotation where branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and calib_type='SubContract' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "'),Interbrnch=(select count(Quotation_Trans_ID) as Quotation_ID from vw_Quotation where branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and calib_type='InterBranch' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "'),    totrcdRM=(select sum(price * Qty) as Quotation_ID from vw_Quotation where branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' and qty<>0),InlabRM=(select sum(price * Qty) as Quotation_ID from vw_Quotation where branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and calib_type='Inhouse' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "'),OnsiteRM=(select sum(price * Qty) as Quotation_ID from vw_Quotation where branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and calib_type='Onsite' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "'),SubconrtRM=(select sum(price * Qty) as Quotation_ID from vw_Quotation where branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and calib_type='SubContract' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "'),InterbrnchRM=(select sum(price * Qty) as Quotation_ID from vw_Quotation where branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and calib_type='InterBranch' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "') ";

            SqlCommand command1 = new SqlCommand(sql1, conn);
            SqlDataReader reader1 = command1.ExecuteReader();
            if (reader1.Read())
            {
                //received CAL
                lbltotrcdcal.Text = reader1["totQuotidRecd"].ToString();
                lbltotrcdinlab.Text = reader1["Inlab"].ToString();
                lbltotrcdonsit.Text = reader1["Onsite"].ToString();
                lbltotrcdExtcal.Text = reader1["Subconrt"].ToString();
                lbltotrcdNC.Text = reader1["Interbrnch"].ToString();



                lbltotrcdinlab1.Text = reader1["Inlab"].ToString();
                lbltotrcdonsit1.Text = reader1["Onsite"].ToString();
                lbltotrcdExtcal1.Text = reader1["Subconrt"].ToString();
                lbltotrcdNC1.Text = reader1["Interbrnch"].ToString();

                lbltotrcdcalunits.Text = reader1["totQuotidRecd"].ToString();
                lbltotrcdinlabunits.Text = reader1["Inlab"].ToString();
                lbltotrcdonsitunits.Text = reader1["Onsite"].ToString();
                lbltotrcdExtcalunits.Text = reader1["Subconrt"].ToString();
                lbltotrcdNCunits.Text = reader1["Interbrnch"].ToString();

                lbltotrcdcalRM.Text = reader1["totrcdRM"].ToString();
                lbltotrcdinlabRM.Text = reader1["InlabRM"].ToString();
                lbltotrcdonsitRM.Text = reader1["OnsiteRM"].ToString();
                lbltotrcdExtcalRM.Text = reader1["SubconrtRM"].ToString();
                lbltotrcdNCRM.Text = reader1["InterbrnchRM"].ToString();


                if (string.IsNullOrEmpty(lbltotrcdcalRM.Text))
                {
                    lbltotrcdcalRM.Text = "0";
                }
                if (string.IsNullOrEmpty(lbltotrcdinlabRM.Text))
                {
                    lbltotrcdinlabRM.Text = "0";
                }
                if (string.IsNullOrEmpty(lbltotrcdonsitRM.Text))
                {
                    lbltotrcdonsitRM.Text = "0";
                }
                if (string.IsNullOrEmpty(lbltotrcdExtcalRM.Text))
                {
                    lbltotrcdExtcalRM.Text = "0";
                }
                if (string.IsNullOrEmpty(lbltotrcdNCRM.Text))
                {
                    lbltotrcdNCRM.Text = "0";
                }
                //  int inttotsts = Convert.ToInt32(lblQuotPending.Text) + Convert.ToInt32(lblQuotComplete.Text) + Convert.ToInt32(lblQuotUnsucces.Text);
                //  lblTotstatus.Text = Convert.ToString(inttotsts);
                lbltotrcd1.Text = lbltotrcdcal.Text;
                double dblinlab = ((Convert.ToDouble(lbltotrcdinlab.Text) / Convert.ToDouble(lbltotrcdcal.Text)) * 100);
                lbltotrcdinlabprcn.Text = Convert.ToString(Math.Round(dblinlab, 0));
                lbltotrcdinlabprcn1.Text = Convert.ToString(Math.Round(dblinlab, 0));
                double dblonsit = ((Convert.ToDouble(lbltotrcdonsit.Text) / Convert.ToDouble(lbltotrcdcal.Text)) * 100);
                lbltotrcdonsitprcn.Text = Convert.ToString(Math.Round(dblonsit, 0));
                lbltotrcdonsitprcn1.Text = Convert.ToString(Math.Round(dblonsit, 0));
                double dblextcal = ((Convert.ToDouble(lbltotrcdExtcal.Text) / Convert.ToDouble(lbltotrcdcal.Text)) * 100);
                lbltotrcdExtcalprcn.Text = Convert.ToString(Math.Round(dblextcal, 0));
                lbltotrcdExtcalprcn1.Text = Convert.ToString(Math.Round(dblextcal, 0));

                double dblnc = ((Convert.ToDouble(lbltotrcdNC.Text) / Convert.ToDouble(lbltotrcdcal.Text)) * 100);
                lbltotrcdNCprcn.Text = Convert.ToString(Math.Round(dblnc, 0));
                lbltotrcdNCprcn1.Text = Convert.ToString(Math.Round(dblnc, 0));

                

            }
            BusinessTier.DisposeReader(reader1);


            //Ageing

            string sql3A = "Select Lessthree=(select count (Quotation_Id) as Quotation_Id from Quotation where QUOT_STATUS='PENDING' and  branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and Quotation_Date between '" + strlessthanthree + "' and '" + strtodate + "') ,LessFourSeven=(select count (Quotation_Id) as Quotation_Id from Quotation where QUOT_STATUS='PENDING' and  branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and Quotation_Date between '" + strgrtrthnseven + "' and '" + strlessthanthree + "'),GrtrSeven=(select count (Quotation_Id) as Quotation_Id from Quotation where QUOT_STATUS='PENDING' and  branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and Quotation_Date < = '" + strgrtrthnseven + "' ),LessthreeRM=(select sum(price * Qty) as Quotation_ID from vw_Quotation where QUOT_STATUS='PENDING' and  branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and Quotation_Date   between '" + strlessthanthree + "' and '" + strtodate + "'),LessFourSevenRM=(select sum(price * Qty) as Quotation_ID from vw_Quotation where QUOT_STATUS='PENDING' and  branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and Quotation_Date between '" + strgrtrthnseven + "' and '" + strlessthanthree + "'),GrtrSevenRM=(select sum(price * Qty) as Quotation_ID from vw_Quotation where QUOT_STATUS='PENDING' and  branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and Quotation_Date < = '" + strgrtrthnseven + "' ) ";

            SqlCommand command3A = new SqlCommand(sql3A, conn);
            SqlDataReader reader3A = command3A.ExecuteReader();
            if (reader3A.Read())
            {
                lblaging3.Text = reader3A["Lessthree"].ToString();
                lblaging4to7.Text = reader3A["LessFourSeven"].ToString();
                lblaging7.Text = reader3A["GrtrSeven"].ToString();
                lblaging3Ind.Text = reader3A["Lessthree"].ToString();
                lblaging4to7Ind.Text = reader3A["LessFourSeven"].ToString();
                lblaging7Ind.Text = reader3A["GrtrSeven"].ToString();

                lblaging3RM.Text = reader3A["LessthreeRM"].ToString();
                lblaging4to7RM.Text = reader3A["LessFourSevenRM"].ToString();
                lblaging7RM.Text = reader3A["GrtrSevenRM"].ToString();

                if (string.IsNullOrEmpty(lblaging3RM.Text))
                {
                    lblaging3RM.Text = "0";
                }
                if (string.IsNullOrEmpty(lblaging4to7RM.Text))
                {
                    lblaging4to7RM.Text = "0";
                }
                if (string.IsNullOrEmpty(lblaging7RM.Text))
                {
                    lblaging7RM.Text = "0";
                }


                int inttotAgeing = Convert.ToInt32(lblaging3.Text) + Convert.ToInt32(lblaging4to7.Text) + Convert.ToInt32(lblaging7.Text);
                lblagingtot.Text = Convert.ToString(inttotAgeing);


                double inttotAgeingrm = Convert.ToDouble(lblaging3RM.Text) + Convert.ToDouble(lblaging4to7RM.Text) + Convert.ToDouble(lblaging7RM.Text);
                lblagingtotRM.Text = Convert.ToString(inttotAgeingrm);

                double dblaging3 = ((Convert.ToDouble(lblaging3.Text) / Convert.ToDouble(lblagingtot.Text)) * 100);
                lblaging3percn.Text = Convert.ToString(Math.Round(dblaging3, 0));
                lblaging3IndPercn.Text = Convert.ToString(Math.Round(dblaging3, 0));
                double dblagng4to7 = ((Convert.ToDouble(lblaging4to7.Text) / Convert.ToDouble(lblagingtot.Text)) * 100);
                lblaging4to7percn.Text = Convert.ToString(Math.Round(dblagng4to7, 0));
                lblaging4to7Indpercn.Text = Convert.ToString(Math.Round(dblagng4to7, 0));
                double dblagng7 = ((Convert.ToDouble(lblaging7.Text) / Convert.ToDouble(lblagingtot.Text)) * 100);
                lblaging7percn.Text = Convert.ToString(Math.Round(dblagng7, 0));
                lblaging7Indpercn.Text = Convert.ToString(Math.Round(dblagng7, 0));



            }
            BusinessTier.DisposeReader(reader3A);

            // Discount

            //Received CAL

            string sql6 = "Select nodisc=(select count(Quotation_ID) as Quotation_ID from Quotation where (Quot_Discount=0 or quot_discount is null) and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "'),disc10=(select count(Quotation_ID) as Quotation_ID from Quotation where Quot_Discount<=10 and Quot_Discount>=1 and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "'),disc11to20=(select count(Quotation_ID) as Quotation_ID from Quotation where Quot_Discount>=11 and Quot_Discount<=20 and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "'),discgrt20=(select count(Quotation_ID) as Quotation_ID from Quotation where Quot_Discount>=21 and branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' )  ";

            SqlCommand command6 = new SqlCommand(sql6, conn);
            SqlDataReader reader6 = command6.ExecuteReader();
            if (reader6.Read())
            {
                //received CAL
                lblnodis.Text = reader6["nodisc"].ToString();
                lbldisc10.Text = reader6["disc10"].ToString();
                lbldisc11to20.Text = reader6["disc11to20"].ToString();
                lbldisc20.Text = reader6["discgrt20"].ToString();

                int inttotdisc = Convert.ToInt32(lblnodis.Text) + Convert.ToInt32(lbldisc10.Text) + Convert.ToInt32(lbldisc11to20.Text) + Convert.ToInt32(lbldisc20.Text);

                lbltotdisc.Text = Convert.ToString(inttotdisc);

                double dblinlab = ((Convert.ToDouble(lblnodis.Text) / Convert.ToDouble(lbltotdisc.Text)) * 100);
                lblnodiscprcn.Text = Convert.ToString(Math.Round(dblinlab, 0));
                
                double dblonsit = ((Convert.ToDouble(lbldisc10.Text) / Convert.ToDouble(lbltotdisc.Text)) * 100);
                lbl10percn.Text = Convert.ToString(Math.Round(dblonsit, 0));
              
                double dblextcal = ((Convert.ToDouble(lbldisc11to20.Text) / Convert.ToDouble(lbltotdisc.Text)) * 100);
                lbl11to20percn.Text = Convert.ToString(Math.Round(dblextcal, 0));
         

                double dblnc = ((Convert.ToDouble(lbldisc20.Text) / Convert.ToDouble(lbltotdisc.Text)) * 100);
                lbl20prcn.Text = Convert.ToString(Math.Round(dblnc, 0));
              



            }
            BusinessTier.DisposeReader(reader6);

            string appPath = Request.PhysicalApplicationPath;
            string strAttachmentpath = ConfigurationManager.AppSettings["WC_AttachementPath"].ToString();


            // Chart Enquiry Received 
            string con = BusinessTier.getConnection1();
            Stimulsoft.Report.StiReport stiReport1;
            string strsql = "";

            if (string.IsNullOrEmpty(lblcalibPercn.Text) || (lblcalibPercn.Text=="NaN"))
            {
                lblcalibPercn.Text = "0";
            }
            if (string.IsNullOrEmpty(lblinspecPercn.Text) || (lblinspecPercn.Text == "NaN"))
            {
                lblinspecPercn.Text = "0";
            }
            if (string.IsNullOrEmpty(lblTrainingPercn.Text) || (lblTrainingPercn.Text == "NaN"))
            {
                lblTrainingPercn.Text = "0";
            }
            if (string.IsNullOrEmpty(lblOthersPercn.Text) || (lblOthersPercn.Text == "NaN"))
            {
                lblOthersPercn.Text = "0";
            }

            strsql = "select Calib='" + lblcalibPercn.Text + "',Inspec='" + lblinspecPercn.Text + "',Training='" + lblTrainingPercn.Text + "',Others='" + lblOthersPercn.Text + "' ";

            SqlDataAdapter ad = new SqlDataAdapter(strsql, con);

            DataSet ds = new DataSet();
            ds.DataSetName = "DynamicDataSource";

            ds.Tables.Add("DataSource1");
            ad.Fill(ds, "DataSource1");
            stiReport1 = new StiReport();
            stiReport1.Dictionary.DataStore.Clear();
            stiReport1.Load(appPath + "\\Reports\\ChartDashboard_Received.mrt");
            stiReport1.Dictionary.Databases.Clear();
            stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
            stiReport1.Dictionary.DataSources.Clear();
            stiReport1.RegData("DataSource1", ds);
            stiReport1.Dictionary.Synchronize();
            stiReport1.Compile();
            StiWebViewer1.Report = stiReport1;
            StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;


            // Chart Quotation Received CAL
            Stimulsoft.Report.StiReport stiReport5;
            string strsql5 = "";
            if (string.IsNullOrEmpty(lbltotrcdinlabprcn1.Text) || (lbltotrcdinlabprcn1.Text == "NaN"))
            {
                lbltotrcdinlabprcn1.Text = "0";
            }
            if (string.IsNullOrEmpty(lbltotrcdonsitprcn1.Text) || (lbltotrcdonsitprcn1.Text == "NaN"))
            {
                lbltotrcdonsitprcn1.Text = "0";
            }
            if (string.IsNullOrEmpty(lbltotrcdExtcalprcn1.Text) || (lbltotrcdExtcalprcn1.Text == "NaN"))
            {
                lbltotrcdExtcalprcn1.Text = "0";
            }
            if (string.IsNullOrEmpty(lbltotrcdNCprcn1.Text) || (lbltotrcdNCprcn1.Text == "NaN"))
            {
                lbltotrcdNCprcn1.Text = "0";
            }
            strsql5 = "select inlab='" + lbltotrcdinlabprcn1.Text + "',onsit='" + lbltotrcdonsitprcn1.Text + "',Extcal='" + lbltotrcdExtcalprcn1.Text + "',NC='" + lbltotrcdNCprcn1.Text + "'";
            SqlDataAdapter ad5 = new SqlDataAdapter(strsql5, con);
            DataSet ds5 = new DataSet();
            ds5.DataSetName = "DynamicDataSource";
            ds5.Tables.Add("DataSource1");
            ad5.Fill(ds5, "DataSource1");
            stiReport5 = new StiReport();
            stiReport5.Dictionary.DataStore.Clear();
            stiReport5.Load(appPath + "\\Reports\\ChartDashboard_CAL.mrt");
            stiReport5.Dictionary.Databases.Clear();
            stiReport5.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
            stiReport5.Dictionary.DataSources.Clear();
            stiReport5.RegData("DataSource1", ds5);
            stiReport5.Dictionary.Synchronize();
            stiReport5.Compile();
            StiWebViewer5.Report = stiReport5;
            StiWebViewer5.ViewMode = StiWebViewMode.WholeReport;


            // Chart Enquiry Status
            Stimulsoft.Report.StiReport stiReport2;
            string strsql2 = "";
            if (string.IsNullOrEmpty(lblenqrypendingPercn.Text) || (lblenqrypendingPercn.Text == "NaN"))
            {
                lblenqrypendingPercn.Text = "0";
            }
            if (string.IsNullOrEmpty(lblenqryCompletePercn.Text) || (lblenqryCompletePercn.Text == "NaN"))
            {
                lblenqryCompletePercn.Text = "0";
            }
            if (string.IsNullOrEmpty(lblenqryunsuccesPercn.Text) || (lblenqryunsuccesPercn.Text == "NaN"))
            {
                lblenqryunsuccesPercn.Text = "0";
            }
            strsql2 = "select pndgenq='" + lblenqrypendingPercn.Text + "',compltenq='" + lblenqryCompletePercn.Text + "',unsuccesenq='" + lblenqryunsuccesPercn.Text + "'";
            SqlDataAdapter ad2 = new SqlDataAdapter(strsql2, con);
            DataSet ds2 = new DataSet();
            ds2.DataSetName = "DynamicDataSource";
            ds2.Tables.Add("DataSource1");
            ad2.Fill(ds2, "DataSource1");
            stiReport2 = new StiReport();
            stiReport2.Dictionary.DataStore.Clear();
            stiReport2.Load(appPath + "\\Reports\\ChartDashboard_Status.mrt");
            stiReport2.Dictionary.Databases.Clear();
            stiReport2.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
            stiReport2.Dictionary.DataSources.Clear();
            stiReport2.RegData("DataSource1", ds2);
            stiReport2.Dictionary.Synchronize();
            stiReport2.Compile();
            StiWebViewer2.Report = stiReport2;
            StiWebViewer2.ViewMode = StiWebViewMode.WholeReport;

            // Chart Enquiry Ageing
            Stimulsoft.Report.StiReport stiReport3;
            string strsql3 = "";
            if (string.IsNullOrEmpty(lblaging3percn.Text) || (lblaging3percn.Text == "NaN"))
            {
                lblaging3percn.Text = "0";
            }
            if (string.IsNullOrEmpty(lblaging4to7percn.Text) || (lblaging4to7percn.Text == "NaN"))
            {
                lblaging4to7percn.Text = "0";
            }
            if (string.IsNullOrEmpty(lblaging7percn.Text) || (lblaging7percn.Text == "NaN"))
            {
                lblaging7percn.Text = "0";
            }
            strsql3 = "select Lessthree='" + lblaging3percn.Text + "',LessFourSeven='" + lblaging4to7percn.Text + "',GrtrSeven='" + lblaging7percn.Text + "'";
            SqlDataAdapter ad3 = new SqlDataAdapter(strsql3, con);
            DataSet ds3 = new DataSet();
            ds3.DataSetName = "DynamicDataSource";
            ds3.Tables.Add("DataSource1");
            ad3.Fill(ds3, "DataSource1");
            stiReport3 = new StiReport();
            stiReport3.Dictionary.DataStore.Clear();
            stiReport3.Load(appPath + "\\Reports\\ChartDashboard_Ageing.mrt");
            stiReport3.Dictionary.Databases.Clear();
            stiReport3.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
            stiReport3.Dictionary.DataSources.Clear();
            stiReport3.RegData("DataSource1", ds3);
            stiReport3.Dictionary.Synchronize();
            stiReport3.Compile();
            StiWebViewer3.Report = stiReport3;
            StiWebViewer3.ViewMode = StiWebViewMode.WholeReport;


            //Quotation BY Staff
           

            
            // select count (Quotation_id) as Quotation_id, Dept_Name from Vw_Enquiry_Staff_Department where  branch_Id='6' and deleted=0 and Enquiry_Date between '06/1/2014 12:00:00 AM' and '10/11/2014 12:00:00 PM' group by Dept_id,Dept_Name

            //double dblinlab = ((Convert.ToDouble(lbltotrcdinlab.Text) / Convert.ToDouble(lbltotrcdcal.Text)) * 100);
            //lbltotrcdinlabprcn.Text = Convert.ToString(Math.Round(dblinlab, 0));
            DataSet dsstff = new DataSet();
            string cmdstr = "select Staff_Name as Staff, count (Quotation_id) as Nos from Vw_Quotation_Staff_Customer where  branch_Id='" + Session["sesBranchID"].ToString().Trim() + "'  and deleted=0 and QUOTATION_DATE between '" + strfrmdate + "' and '" + strtodate + "' group by staff_name ";

            SqlDataAdapter adp = new SqlDataAdapter(cmdstr, conn);

            adp.Fill(dsstff);

            Grdbystaff.DataSource = dsstff;

            Grdbystaff.DataBind();

            dsstff.Dispose();

           // conn.Close();
            
           // BusinessTier.DisposeConnection(con);
            //********************************Recent Quotation Activites************************************************

            string sql3 = "select top 5 Customer_Name,Quot_Status as status, Created_Date  from Vw_Quotation_Customer where branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' order by Quotation_Id Desc";
            SqlCommand command3 = new SqlCommand(sql3, conn);
            SqlDataReader reader3 = command3.ExecuteReader();
            int j = 1;
            while (reader3.Read())
            {
                DateTime dttime = Convert.ToDateTime(reader3["Created_Date"].ToString());
                strtodate = dttime.Day + "/" + dttime.Month + "/" + dttime.Year + " " + dttime.ToShortTimeString();
                if (j == 1)
                {
                    lblrcntactvtsQuot1.Text = reader3["Customer_Name"].ToString();
                    lblrcntstatusQuot1.Text = reader3["Status"].ToString();
                    lblrcntdateQuot1.Text = strtodate;

                    if (lblrcntstatusQuot1.Text == "PENDING")
                    {
                        lblrcntstatusQuot1.BackColor = Color.Blue;
                    }
                    else if (lblrcntstatusQuot1.Text == "COMPLETE")
                    {
                        lblrcntstatusQuot1.BackColor = Color.Green;
                    }
                    else if (lblrcntstatusQuot1.Text == "UNSUCCESSFUL")
                    {
                        lblrcntstatusQuot1.BackColor = Color.Red;
                    }
                }
                else if (j == 2)
                {
                    lblrcntactvtsQuot2.Text = reader3["Customer_Name"].ToString();
                    lblrcntstatusQuot2.Text = reader3["Status"].ToString();
                    lblrcntdateQuot2.Text = strtodate;
                    if (lblrcntstatusQuot2.Text == "PENDING")
                    {
                        lblrcntstatusQuot2.BackColor = Color.Blue;
                    }
                    else if (lblrcntstatusQuot2.Text == "COMPLETE")
                    {
                        lblrcntstatusQuot2.BackColor = Color.Green;
                    }
                    else if (lblrcntstatusQuot2.Text == "UNSUCCESSFUL")
                    {
                        lblrcntstatusQuot2.BackColor = Color.Red;
                    }
                }
                else if (j == 3)
                {
                    lblrcntactvtsQuot3.Text = reader3["Customer_Name"].ToString();
                    lblrcntstatusQuot3.Text = reader3["Status"].ToString();
                    lblrcntdateQuot3.Text = strtodate;
                    if (lblrcntstatusQuot3.Text == "PENDING")
                    {
                        lblrcntstatusQuot3.BackColor = Color.Blue;
                    }
                    else if (lblrcntstatusQuot3.Text == "COMPLETE")
                    {
                        lblrcntstatusQuot3.BackColor = Color.Green;
                    }
                    else if (lblrcntstatusQuot3.Text == "UNSUCCESSFUL")
                    {
                        lblrcntstatusQuot3.BackColor = Color.Red;
                    }
                }
                else if (j == 4)
                {
                    lblrcntactvtsQuot4.Text = reader3["Customer_Name"].ToString();
                    lblrcntstatusQuot4.Text = reader3["Status"].ToString();
                    lblrcntdateQuot4.Text = strtodate;
                    if (lblrcntstatusQuot4.Text == "PENDING")
                    {
                        lblrcntstatusQuot4.BackColor = Color.Blue;
                    }
                    else if (lblrcntstatusQuot4.Text == "COMPLETE")
                    {
                        lblrcntstatusQuot4.BackColor = Color.Green;
                    }
                    else if (lblrcntstatusQuot4.Text == "UNSUCCESSFUL")
                    {
                        lblrcntstatusQuot4.BackColor = Color.Red;
                    }
                }
                else if (j == 5)
                {
                    lblrcntactvtsQuot5.Text = reader3["Customer_Name"].ToString();
                    lblrcntstatusQuot5.Text = reader3["Status"].ToString();
                    lblrcntdateQuot5.Text = strtodate;
                    if (lblrcntstatusQuot5.Text == "PENDING")
                    {
                        lblrcntstatusQuot5.BackColor = Color.Blue;
                    }
                    else if (lblrcntstatusQuot5.Text == "COMPLETE")
                    {
                        lblrcntstatusQuot5.BackColor = Color.Green;
                    }
                    else if (lblrcntstatusQuot5.Text == "UNSUCCESSFUL")
                    {
                        lblrcntstatusQuot5.BackColor = Color.Red;
                    }
                }

                j = j + 1;
            }
            j = 0;


            BusinessTier.DisposeReader(reader3);



            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception Ex)
        {
            lblStatus.Text = Ex.Message.ToString();
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "PageInit", "QuotationDashboard", Ex.ToString(), "Audit");
        }
    }


    protected void btneQuot_Onclick(object sender, EventArgs e)
    {
        String strfrmdate = null, strtodate = null;
        DateTime dtFromDatecon = DtFromDate.SelectedDate.Value;
        strfrmdate = dtFromDatecon.Month + "/" + dtFromDatecon.Day + "/" + dtFromDatecon.Year + " 12:00:00 AM";

        DateTime dtToDatecon = DtToDate.SelectedDate.Value;
        strtodate = dtToDatecon.Month + "/" + dtToDatecon.Day + "/" + dtToDatecon.Year + " 12:00:00 PM";

        // ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Enquiry.aspx?param1=" + strfrmdate + "',?param2=" + strtodate + "');", true);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Quotationdetails.aspx?param1=" + strfrmdate + "&param2=" + strtodate + "&param3=" + 1 + "');", true);
    }
    protected void btneQuotrecdcal_Onclick(object sender, EventArgs e)
    {
        String strfrmdate = null, strtodate = null;
        DateTime dtFromDatecon = DtFromDate.SelectedDate.Value;
        strfrmdate = dtFromDatecon.Month + "/" + dtFromDatecon.Day + "/" + dtFromDatecon.Year + " 12:00:00 AM";

        DateTime dtToDatecon = DtToDate.SelectedDate.Value;
        strtodate = dtToDatecon.Month + "/" + dtToDatecon.Day + "/" + dtToDatecon.Year + " 12:00:00 PM";

        // ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Enquiry.aspx?param1=" + strfrmdate + "',?param2=" + strtodate + "');", true);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Quotationdetails.aspx?param1=" + strfrmdate + "&param2=" + strtodate + "&param3=" + 4 + "');", true);
    }
    protected void btnenqyststs_Onclick(object sender, EventArgs e)
    {
        String strfrmdate = null, strtodate = null;
        DateTime dtFromDatecon = DtFromDate.SelectedDate.Value;
        strfrmdate = dtFromDatecon.Month + "/" + dtFromDatecon.Day + "/" + dtFromDatecon.Year + " 12:00:00 AM";

        DateTime dtToDatecon = DtToDate.SelectedDate.Value;
        strtodate = dtToDatecon.Month + "/" + dtToDatecon.Day + "/" + dtToDatecon.Year + " 12:00:00 PM";

        // ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Enquiry.aspx?param1=" + strfrmdate + "',?param2=" + strtodate + "');", true);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Quotationdetails.aspx?param1=" + strfrmdate + "&param2=" + strtodate + "&param3=" + 2 + "');", true);
    }
    protected void btnenqryAging_Onclick(object sender, EventArgs e)
    {
        String strfrmdate = null, strtodate = null, strlessthanthree = null, strlfourtoseven = null, strgrtrthnseven = null;
        DateTime dtFromDatecon = DtFromDate.SelectedDate.Value;
        strfrmdate = dtFromDatecon.Month + "/" + dtFromDatecon.Day + "/" + dtFromDatecon.Year + " 12:00:00 AM";

        DateTime dtToDatecon = DtToDate.SelectedDate.Value;
        strtodate = dtToDatecon.Month + "/" + dtToDatecon.Day + "/" + dtToDatecon.Year + " 12:00:00 PM";

        DateTime dtToDatecon1 = dtToDatecon.AddDays(-3);
        DateTime dtToDatecon2 = dtToDatecon.AddDays(-7);
        strlessthanthree = dtToDatecon1.Month + "/" + dtToDatecon1.Day + "/" + dtToDatecon1.Year + " 12:00:00 AM";
        strgrtrthnseven = dtToDatecon2.Month + "/" + dtToDatecon2.Day + "/" + dtToDatecon2.Year + " 12:00:00 PM";


        // ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Enquiry.aspx?param1=" + strfrmdate + "',?param2=" + strtodate + "');", true);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Quotationdetails.aspx?param1=" + strgrtrthnseven + "&param3=" + 3 + "');", true);
    }


    protected void cboreporttype_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboreporttype.SelectedValue == "0")
        {
            DtFromDate.SelectedDate = DateTime.Now;
            DtToDate.SelectedDate = DateTime.Now;
            generateReport();

        }
        else if (cboreporttype.SelectedValue == "1")
        {
            DateTime dt1 = DateTime.Now.AddDays(-1);
            DtFromDate.SelectedDate = dt1;
            DtToDate.SelectedDate = dt1;

            generateReport();
        }
        else if (cboreporttype.SelectedValue == "2")
        {
            DateTime dt1 = DateTime.Now.AddDays(-7);
            DtFromDate.SelectedDate = dt1;
            DtToDate.SelectedDate = DateTime.Now;
            generateReport();


        }
        else if (cboreporttype.SelectedValue == "3")
        {
            DateTime today = DateTime.Today;

            DtFromDate.SelectedDate = new DateTime(today.Year, today.Month, 1);
            DtToDate.SelectedDate = DateTime.Now;
            generateReport();
        }
        else if (cboreporttype.SelectedValue == "4")
        {
            var yr = DateTime.Today.Year;
            var mth = DateTime.Today.Month;
            var firstDay = new DateTime(yr, mth, 1).AddMonths(-1);
            var lastDay = new DateTime(yr, mth, 1).AddDays(-1);
            DtFromDate.SelectedDate = firstDay;
            DtToDate.SelectedDate = lastDay;
            generateReport();
        }
        else if (cboreporttype.SelectedValue == "5")
        {
            DateTime today = DateTime.Today;

            DtFromDate.SelectedDate = new DateTime(today.Year, 1, 1);

            DtToDate.SelectedDate = DateTime.Now;
            generateReport();
        }
        else if (cboreporttype.SelectedValue == "6")
        {
            var yr = DateTime.Today.Year;
            var mth = DateTime.Today.Month;
            var firstDay = new DateTime(yr, 1, 1).AddYears(-1);
            var lastDay = new DateTime(yr, 12, 31).AddYears(-1);
            DtFromDate.SelectedDate = firstDay;
            DtToDate.SelectedDate = lastDay;
            generateReport();
        }

    }



    private void InsertLogAuditTrail(string userid, string module, string activity, string result, string flag)
    {
        SqlConnection connLog = BusinessTier.getConnection();
        connLog.Open();
        BusinessTier.InsertLogAuditTrial(connLog, userid, module, activity, result, flag);
        BusinessTier.DisposeConnection(connLog);
    }
}