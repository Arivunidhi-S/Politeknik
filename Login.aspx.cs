using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using System.Drawing;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginTxt.Focus();
        Session["sesUserID"] = 0;
        Session["sesBranchID"] = 0;
        Session["sesUserType"] = 0;
        Session["sesDepartmentType"] = 0;
        Session["sesBranchCode"] = 0;

    }
    protected void Page_Init(object sender, EventArgs e)
    {
        Session.Contents.Clear();
        LoginTxt.Text = "";
        PwdTxt.Text = "";
    }

    protected void LoginBtn_Click(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();

        try
        {
            conn.Open();
            BusinessTier.BindErrorMessageDetails(conn);

            int flag = 0;
            string strId = "0";
            int intValidation = 0;
            SqlDataReader reader1 = BusinessTier.VaildateUserLogin(conn, LoginTxt.Text.ToString(), PwdTxt.Text.ToString());
            if (reader1.Read())
            {
                if (!(string.IsNullOrEmpty(reader1["Staff_ID"].ToString())))
                {
                    flag = 1;
                    strId = (reader1["Staff_ID"].ToString());
                    Session["sesLoginID"] = (reader1["ID"].ToString());
                    Session["sesBranchID"] = (reader1["Branch_ID"].ToString());
                    Session["sesDepartmentType"] = (reader1["Dept_ID"].ToString());
                    Session["Name"] = (reader1["STAFF_NAME"].ToString());
                }
                else
                {
                    intValidation = 1;
                }
            }
            BusinessTier.DisposeReader(reader1);

            if (intValidation == 1)
            {
                ShowMessage(16);
                return;
            }

            if (flag >= 1)
            {
                Session["sesUserID"] = strId.ToString();

                //...............................................................................................................

                string strBranchCode = "";
                SqlDataReader readerBrach = BusinessTier_CRM.getBranchInfo_ByID(conn, Session["sesBranchID"].ToString().Trim());
                if (readerBrach.Read())
                {
                    strBranchCode = (readerBrach["BR_SHORT_NAME"].ToString()).TrimEnd();
                }
                BusinessTier.DisposeReader(readerBrach);
                Session["sesBranchCode"] = strBranchCode.ToString().Trim();
                //...................................................................................................


                //if ((Session["sesDepartmentType"].ToString().Trim() == "6") || (Session["sesDepartmentType"].ToString().Trim() == "8"))
                //{
                //    //Response.Redirect("Enquiry.aspx", false);
                //    Response.Redirect("Main.aspx", false);
                //}
                //else if ((Session["sesDepartmentType"].ToString().Trim() == "16"))
                //{
                //    Response.Redirect("CRM_Dashboard.aspx", false);
                //}
                //else
                //{
                //    //  Response.Redirect("Master_Enquiry.aspx", false);
                //    Response.Redirect("Enquiry.aspx", false);
                //}

                Response.Redirect("Main.aspx", false);
            }
            else
            {
                ShowMessage(16);
                return;
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterLogin", "SignIn", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterLogin", "SignIn", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
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
