using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using Trirand.Web.UI.WebControls;

namespace TMID{ 

public partial class Logon : System.Web.UI.Page
{
    protected System.Web.UI.HtmlControls.HtmlForm Form1;

    public static Int32 iUserID;
    public static Int32 iPartnerID;

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
        con.Open();
        string sql;
        int status = 2;

        sql = "SELECT i.i_Item_ID, i.i_Status, i.u_Item_Name, i.i_Class_ID"
                 + " FROM TRI_Items i"
                + " WHERE i.i_Status = " + status
                ;
        con.Close();
    }

    protected void btnSubmitLogon_Click(object sender, System.EventArgs e)
    {
        string sql;
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;

        Session["test"] = "Nealus Fauntus";

        /*
        sql = "SELECT u_Username, u_Password_Hash, i_User_ID, i_Partner_ID"
            + " FROM TRI_Users u"
            + " WHERE u_Username = '" 
            + this.txtUserName.Text.Replace("'", "''") 
            + "' AND u_Password_Hash = '" 
            + FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtPwd.Text, "sha1") + "'";
			
        // SL - 2/13/2009 revised sql to accomodate Visitor logon
        sql = "SELECT u_Username, u_Password_Hash, i_User_ID, u.i_Partner_ID, x.f_Can_Vote"
            + " FROM TRI_Users u LEFT JOIN TRI_Partners x on u.i_Partner_ID = x.i_Partner_ID"
            + " WHERE u_Username = '" 
            + this.txtUserName.Text.Replace("'", "''") 
            + "' AND u_Password_Hash = '" 
            + FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtPwd.Text, "sha1") + "'";
        */
        // BV - 10/27/2011 revised sql to accomodate active user
        sql = "SELECT u_Username, u_Password_Hash, i_User_ID, u.i_Partner_ID, x.f_Can_Vote, u_Active"
            + " FROM TRI_Users u LEFT JOIN TRI_Partners x on u.i_Partner_ID = x.i_Partner_ID"
            + " WHERE u_Username = '"
            + this.txtUserName.Text.Replace("'", "''")
            + "' AND u_Password_Hash = '"
            + FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtPwd.Text, "sha1") + "'";

        conn = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
        conn.Open();

        cmd = new SqlCommand(sql, conn);
        reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            reader.Read();

            //set authentication cookie
            System.Web.Security.FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
            (
                1,
                "Trilateral",
                System.DateTime.Now,
                System.DateTime.Now.AddMinutes(60),
                false,
                reader.GetString(0) + "|" + reader.GetInt32(2).ToString() + "|" + reader.GetInt32(3).ToString()
            );
            string encryptedTicket = System.Web.Security.FormsAuthentication.Encrypt(authTicket);
            System.Web.HttpCookie authCookie = new HttpCookie(System.Web.Security.FormsAuthentication.FormsCookieName, encryptedTicket);
            System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);

            //this.lblMessageLabel.Text = reader.GetString(0) + "|" + reader.GetInt32(2).ToString() + "|" + reader.GetInt32(3).ToString();

            //FormsAuthentication.RedirectFromLoginPage(reader.GetString(0), false);
            //Response.Redirect("Default.aspx", true);
            // SL - 2/13/2009 revised to accomodate for Visitor logon
            // BV - 11/1/2011 revised to check if user is active.

            if (reader.GetBoolean(5))
            {
                if (reader.GetBoolean(4))
                {
                    markUserLoggedIn(this.txtUserName.Text);

                    string strRedirect = Request["ReturnUrl"];
                    if (strRedirect == null)
                        Session["CanVote"] = "Partner";
                        strRedirect = "Main.aspx";
                    Response.Redirect(strRedirect, true);
                }
                else
                {
                    markUserLoggedIn(this.txtUserName.Text);

                    string strRedirect = Request["ReturnUrl"];
                    if (strRedirect == null)
                        Session["CanVote"] = "Visitor";
                        strRedirect = "Main.aspx";
                    Response.Redirect(strRedirect, true);
                }
            }
            else
            {
                this.lblMainMessageLabel.Text = "Restricted access. Invalid Username.";
                this.lblMessageLabel.Text = "Enter your username and password for access.";
            }

        }
        else
        {
            this.lblMainMessageLabel.Text = "Restricted access. The username and password combination you entered is not valid.";
            this.lblMessageLabel.Text = "Enter your username and password for access.";
        }

        conn.Close();
    }

    protected void markUserLoggedIn(string userName)
    {
        string sql2 = "UPDATE TRI_Users SET u_Logged_In = '1', u_Last_Log_In_Time = GETDATE() WHERE u_Username = '" + userName + "'";

        //BK 11/27/2012 revised sql to allow logged in user tracking
        SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
        conn2.Open();
        SqlCommand cmd2 = new SqlCommand(sql2, conn2);
        SqlDataReader reader2 = cmd2.ExecuteReader();
        conn2.Close();
    }
    
    public struct s_GridResult
    {
        public int page;
        public int total;
        public int record;
        public s_RowData[] rows;

    }
    public struct s_RowData
    {
        public int id;
        public string[] cell;
    }


    public static string TestSessionValue
    {
        get
        {
            object value = HttpContext.Current.Session["TestSessionValue"];
            return value == null ? "" : (string)value;
        }
        set
        {
            HttpContext.Current.Session["TestSessionValue"] = value;
        }
    }





}  
}