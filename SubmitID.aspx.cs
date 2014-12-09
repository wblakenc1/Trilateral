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

//namespace TMID{ 

//public partial class SubmitID : System.Web.UI.Page

namespace TMID
{
    public partial class SubmitID : System.Web.UI.Page, ICallbackEventHandler
	{
        protected String returnValue;
        public static String gifSrc = "images/tril_flashing.gif";       
        private UserAuthInfo UserInfo;                                    
		string IDText;
		static int dupIDExists;
		static int RcdCount;
		int TmpRcdCount;
		DataTable dtg;
		DataTable dt;
		DataRow dr;
		SqlConnection con;
		protected System.Web.UI.WebControls.ListBox ListBox1;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            String cbReference = Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData", "context");
            String callbackScript = "function CallServer(arg, context)" + "{ " + cbReference + ";}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallServer", callbackScript, true);

			// get logged on user info
			UserInfo = new UserAuthInfo("|".ToCharArray());
            lblUserName.Text = "Need Username Here";
			lblUserName.Text = UserInfo.Username;                 
			IDText = txtTrilateralID.Text;
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", UserInfo.Username.ToString());
			this.lblComment.Visible = true;
			this.txtComment.Visible = true;
			this.lblWarningID.Visible = false;
			this.lblRejectedID.Visible = false;
			this.lblConflictID.Visible = false;
			this.btnContinue.Visible = false;
			this.btnCancel.Visible = false;
			this.txtTrilateralID.Visible = true;
			this.cboClassNumber.Visible = true;
			this.lblCandidateID.Visible = true;
			this.lblClass.Visible = true;

			//Create New Data Table and Row For Submissions
			dtg = new DataTable(); 
			dt = new DataTable();

			//con = new SqlConnection((string)ConfigurationSettings.AppSettings["sqlConnectionString"]);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
			con.Open(); 

			if(!Page.IsPostBack)
			{
				PopulateClassList();
				Bind_DataGrid1("i.dt_Released ASC", 2);
				reqClass.MinimumValue = this.cboClassNumber.Items[1].Value;
				reqClass.MaximumValue = this.cboClassNumber.Items[this.cboClassNumber.Items.Count - 1].Value;
				dtg = (DataTable) Session["SampleDataTable"];
				DataGrid1.DataSource=dtg;
				DataGrid1.PageSize+=1;

				DataGrid1.DataBind();

				if (DataGrid1.Items.Count == 0)
				{
					DataGrid1.Visible = false;
				}


				if (DataGrid1.Items.Count != 0)
				{
					dupIDExists = 0;
					btnSubmitAll.Visible = true;
					rdoSubmitFirstMonth.Visible = true;
					rdoSubmitNow.Visible = true;
					lblMessage.Text = "";

					for(int itemNum = 0; itemNum < DataGrid1.Items.Count; itemNum++)
					{
						if (DataGrid1.Items[itemNum].Cells[5].Text.Substring(0,9) == "Duplicate")
						{
							dupIDExists = 1;
							btnSubmitAll.Visible = false;
							rdoSubmitFirstMonth.Visible = false;
							rdoSubmitNow.Visible = false;
							lblMessage.Text = "- Duplicate Items cannot be submitted.  Delete/Edit before submitting." + "<br>";
						}
					}

					//Check for repeat items in list
					int dupCount = 0;

					for(int itemNum = 0; itemNum < DataGrid1.Items.Count; itemNum++)
					{
						for(int itemNumComp = 0; itemNumComp < DataGrid1.Items.Count; itemNumComp++)
						{
							if (DataGrid1.Items[itemNum].Cells[2].Text == DataGrid1.Items[itemNumComp].Cells[2].Text  && DataGrid1.Items[itemNum].Cells[3].Text == DataGrid1.Items[itemNumComp].Cells[3].Text)
								dupCount = dupCount + 1;
						}
					}
					if (dupCount != DataGrid1.Items.Count)
					{
						lblRepeatIDs.Text = "- Item(s) repeated in list.  Delete/Edit before submitting." + "<br>";
						btnSubmitAll.Visible = false;
						rdoSubmitFirstMonth.Visible = false;
						rdoSubmitNow.Visible = false;
						lblDupIDForComparison.Text = dupCount.ToString();
					}
					if (dupCount == DataGrid1.Items.Count)
					{
						lblRepeatIDs.Text = "" + "<br>";
						//btnSubmitAll.Visible = true;
						lblRepeatIDs.Text = "";
						lblDupIDForComparison.Text = dupCount.ToString();
					}
				}	
			}
			con.Close();
		}

		public void Bind_DataGrid1(string sortExpression, int status)
		{
			RcdCount = 0;
			int day_today = DateTime.Today.Day;
			int logged_on_partner_ID = UserInfo.PartnerID;                              
			//this.lblLogged_On_Partner_ID.Text = UserInfo.PartnerID.ToString("000");
			//SqlConnection con1 = new SqlConnection((string)ConfigurationSettings.AppSettings["sqlConnectionString"]);
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
			string sql1;

			sql1 = "Select *"
                + "	, ISNULL((SELECT u_Vote_Name FROM TRI_Votes v join tri_vote_types t on v.i_vote_type_id = t.i_vote_type_id WHERE i_Item_ID = i.i_Item_ID AND i_Partner_ID = 1), 'Pending')"
                + " , ISNULL((SELECT u_Vote_Name FROM TRI_Votes v join tri_vote_types t on v.i_vote_type_id = t.i_vote_type_id WHERE i_Item_ID = i.i_Item_ID AND i_Partner_ID = 5), 'Pending')"
                + " , ISNULL((SELECT u_Vote_Name FROM TRI_Votes v join tri_vote_types t on v.i_vote_type_id = t.i_vote_type_id WHERE i_Item_ID = i.i_Item_ID AND i_Partner_ID = 3), 'Pending')"
                + " , ISNULL((SELECT u_Vote_Name FROM TRI_Votes v join tri_vote_types t on v.i_vote_type_id = t.i_vote_type_id WHERE i_Item_ID = i.i_Item_ID AND i_Partner_ID = 7), 'Pending')"
                + " , ISNULL((SELECT u_Vote_Name FROM TRI_Votes v join tri_vote_types t on v.i_vote_type_id = t.i_vote_type_id WHERE i_Item_ID = i.i_Item_ID AND i_Partner_ID = 14), 'Pending')"
				+ "FROM TRI_Items i"
				+ " JOIN TRI_Users u ON u.i_User_ID = i.i_User_ID_Created_By"
				+ " LEFT JOIN TRI_Partners p ON p.i_Partner_ID = u.i_Partner_ID"
				+ " WHERE p.i_Partner_ID = u.i_Partner_ID AND i.i_Status = " + status
				+ " AND i.dt_Released > DATEADD" + "(day, -" + day_today + ", getdate())"
				+ " AND i.f_Released = 1"
				+ " AND p.i_Partner_ID = " + logged_on_partner_ID;

			con1.Open();
			SqlCommand cmd1 = new SqlCommand(sql1, con1);
			SqlDataAdapter myAdapter1 = new SqlDataAdapter(cmd1);
			DataSet ds1 = new DataSet();
			myAdapter1.Fill(ds1,"Goods");
			RcdCount = ds1.Tables["Goods"].Rows.Count;
			lblSubmissionCount.Text = RcdCount.ToString();
			if (RcdCount >= 30)
			{
				lblInstructions.Text = "You have reached the submittal limit of 30 Proposed Items for this month.";
				btnAdd.Visible = false;
				lblCandidateID.Visible = false;
				lblClass.Visible = false;
				lblComment.Visible = false;
				txtTrilateralID.Visible = false;
				cboClassNumber.Visible = false;
				txtComment.Visible = false;
				btnSubmitAll.Visible = false;
				rdoSubmitFirstMonth.Visible = false;
				rdoSubmitNow.Visible = false;
				DataGrid1.Visible = false;
				lblMultipleMessage.Visible = false;
			}
			con1.Close();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
			this.DataGrid1.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_DeleteCommand);

		}
		#endregion

		protected void btnEnterNewID_Click(object sender, System.EventArgs e)
		{
			if(Page.IsValid)
			{
				// Check for existing items
				string sql = "SELECT i.i_Item_ID, c.u_Description, u_Item_Name, s.u_Code_Meaning, i.i_Class_ID, i.i_Status"
					+ " FROM TRI_Items i"
					+ " LEFT JOIN TRI_Classes c ON i.i_Class_ID = c.i_Class_ID"
					+ " LEFT JOIN TRI_Lookup_Status s ON i.i_Status = s.i_Code_Value"
					+ " WHERE u_Item_Name = N'" + IDText + "'";
                SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand(sql, con2);
				con2.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				Int32 dupID;
				Int32 dupClassID;
				Int32 dupStatusID;
				string dupClass;
				string dupStatus;
				string dupName;

				bool conflict = false;
				bool rejected = false;
				bool classDiff = false;

				if(reader.HasRows)
				{
					this.lblConflictID.Text = "The Item you submitted conflicts with the following Trilateral Approved Items:<br><table border=1 cellspacing=0 style='background-color:White;border-color:#222222;border-width:1px;border-style:solid;border-collapse:collapse;' class=normalfont><tr bgcolor=#6F0000 class=normalfont style=color:#FFFFFF><td><b>Item</b></td><td><b>Class</b></td><td><b>Status</b></td></tr>";
					this.lblRejectedID.Text = "<br><br>The submitted Item already exists, but has been rejected or removed from the Trilateral Approved List. If you wish to resubmit this item, view Item details and click the Resubmit button.<br><table border=1 cellspacing=0 style='background-color:White;border-color:#222222;border-width:1px;border-style:solid;border-collapse:collapse;' class=normalfont><tr bgcolor=#6F0000 class=normalfont style=color:#FFFFFF><td><b>Item</b></td><td><b>Class</b></td><td><b>Status</b></td></tr>";
					this.lblWarningID.Text = "<br><br>The following Items with similar names exist in different classes:<br><table border=1 cellspacing=0 style='background-color:White;border-color:#222222;border-width:1px;border-style:solid;border-collapse:collapse;' class=normalfont><tr bgcolor=#6F0000 class=normalfont style=color:#FFFFFF><td><b>Item</b></td><td><b>Class</b></td><td><b>Status</b></td></tr>";

					while(reader.Read())
					{
						dupID = reader.GetInt32(0);
						dupClass = reader.GetString(1);
						dupName = reader.GetString(2);
						dupStatus = reader.GetString(3);
						dupClassID = reader.GetInt32(4);
						dupStatusID = reader.GetInt32(5);

						// If existing items exist as Pending or Accepted, reject submission (exact match)
						if( (dupStatusID == 1 || dupStatusID == 2) && dupClassID == Convert.ToInt32(this.cboClassNumber.SelectedValue) )
						{
							conflict = true;
							this.lblConflictID.Text += "<tr><td><a href='Details.aspx?i=" + dupID.ToString() + "'>" + dupName + "</a></td><td>" + dupClass + "</td><td>" + dupStatus + "</td></tr>";
							this.btnBack.Visible = true;
						}

						// If existing items exist as Rejected or Removed, give option to resubmit duplicate or recreate new copy (exact match).
						if( (dupStatusID == 0 || dupStatusID == 3) && dupClassID == Convert.ToInt32(this.cboClassNumber.SelectedValue) )
						{
							rejected = true;
							this.lblRejectedID.Visible = true;
							this.lblRejectedID.Text += "<tr><td><a href='Details.aspx?i=" + dupID.ToString() + "'>" + dupName + "</a></td><td>" + dupClass + "</td><td>" + dupStatus + "</td></tr>";
							this.btnBack.Visible = true;
						}

                        // If duplicate item names exist in different classes
						if(dupClassID != Convert.ToInt32(this.cboClassNumber.SelectedValue) )
						{
							classDiff = true;
							this.lblWarningID.Visible = true;
							this.lblWarningID.Text += "<tr><td><a href='Details.aspx?i=" + dupID.ToString() + "'>" + dupName + "</a></td><td>" + dupClass + "</td><td>" + dupStatus + "</td></tr>";
							// Confirm with Continue / Cancel buttons
						}
					}

					this.lblConflictID.Text += "</table>";
					this.lblRejectedID.Text += "</table>";
					this.lblWarningID.Text += "</table>";

					if(classDiff && !conflict && !rejected)
					{
						this.lblWarningID.Text += "<br><i><b>Do you want to submit this Item?</b></i><br>";
						this.btnContinue.Visible = true;
						this.btnCancel.Visible = true;
					}

					this.btnEnterNewID.Visible = false;
					this.txtTrilateralID.Visible = false;
					this.cboClassNumber.Visible = false;
					this.lblCandidateID.Visible = false;
					this.lblClass.Visible = false;
					this.lblComment.Visible = false;
					this.txtComment.Visible = false;
				}
				else
				{
					reader.Close();

					this.btnContinue.Visible = true;
					this.btnCancel.Visible = true;
					
					this.btnEnterNewID.Visible = false;
					this.txtTrilateralID.Visible = false;
					this.cboClassNumber.Visible = false;
					this.lblCandidateID.Visible = false;
					this.lblClass.Visible = false;
					this.lblComment.Visible = false;
					this.txtComment.Visible = false;
				}
				con2.Close();
			}			
		}

		private Int32 InsertItem(int iClassID, string uItemName, int iStatus, bool bReleased)
		{
			int iReleased;

			if(bReleased)
				iReleased = 1;
			else
				iReleased = 0;

			// insert new item
			string sql = "INSERT INTO TRI_Items (i_Class_ID, u_Item_Name, i_Status, dt_Released, f_Released, i_User_ID_Created_By)"
				+ " VALUES (" + iClassID.ToString() + ", N'" + uItemName/*.Replace("'", "''")*/ + "', " + iStatus.ToString() + ", convert(varchar, GETDATE(), 101), " + iReleased.ToString() + ", " + UserInfo.UserID.ToString() + ")";
            SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, con3);
			con3.Open();
			cmd.ExecuteNonQuery(); 

			// Get item id
			sql = "SELECT i_Item_ID FROM TRI_Items WHERE u_Item_Name = N'" + uItemName/*.Replace("'", "''")*/ + "' AND i_Class_ID = " + iClassID.ToString() + " AND i_Status = " + iStatus.ToString();
			cmd.CommandText = sql;

			System.Int32 itemID = Convert.ToInt32(cmd.ExecuteScalar());

			// Add vote
			sql = "INSERT INTO TRI_Votes (i_Item_ID, i_User_ID, i_Partner_ID, i_vote_type_id)"
				+ " VALUES (" + itemID.ToString()
				+ ", " + UserInfo.UserID.ToString()
				+ ", " + UserInfo.PartnerID.ToString()
				+ ", 1"
				+ ")";

			cmd.CommandText = sql;
			cmd.ExecuteNonQuery();
            //Trilateral.TrilateralItem.FlagViewedItem(itemID, UserInfo.UserID);    Add back in
            //Trilateral.TrilateralItem.FlagViewedItem(itemID, UserInfo.UserID);
			con3.Close();
			return itemID;
		}

		private Int32 InsertItemFirstOfMonth(int iClassID, string uItemName, int iStatus, bool bReleased)
		{
			int iReleased;

			if(bReleased)
				iReleased = 1;
			else
				iReleased = 0;

			// insert new item
			string sql = "INSERT INTO TRI_Items (i_Class_ID, u_Item_Name, i_Status, dt_Released, f_Released, i_User_ID_Created_By)"
				+ " VALUES (" + iClassID.ToString() + ", N'" + uItemName.Replace("'", "''") + "', " + iStatus.ToString() + ", dateadd(ms,-0,DATEADD(mm, DATEDIFF(m,0,(convert(varchar, GETDATE(), 101))  )+1, 0)), " + iReleased.ToString() + ", " + UserInfo.UserID.ToString() + ")";
            SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, con4);
			con4.Open();
			cmd.ExecuteNonQuery(); 

			// get item id
			sql = "SELECT i_Item_ID FROM TRI_Items WHERE u_Item_Name = N'" + uItemName.Replace("'", "''") + "' AND i_Class_ID = " + iClassID.ToString() + " AND i_Status = " + iStatus.ToString();
			cmd.CommandText = sql;

			System.Int32 itemID = Convert.ToInt32(cmd.ExecuteScalar());

			// add vote
			sql = "INSERT INTO TRI_Votes (i_Item_ID, i_User_ID, i_Partner_ID, i_vote_type_id)"
				+ " VALUES (" + itemID.ToString()
				+ ", " + UserInfo.UserID.ToString()
				+ ", " + UserInfo.PartnerID.ToString()
				+ ", 1"
				+ ")";

			cmd.CommandText = sql;
			cmd.ExecuteNonQuery();
			//Trilateral.TrilateralItem.FlagViewedItem(itemID, UserInfo.UserID);    Add back in
			con4.Close();
			return itemID;
		}

		private void PopulateClassList()
		{
			this.cboClassNumber.AutoPostBack = false;
            SqlConnection con5 = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
            con5.Open();

			string sql = "SELECT i_Class_ID, u_Description FROM TRI_Classes ORDER BY i_Class_ID";
			SqlCommand cmd = new SqlCommand(sql, con5);
			SqlDataAdapter myAdapter = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
            myAdapter.Fill(ds,"Classes");                             
			
            this.cboClassNumber.DataSource = ds;
            this.cboClassNumber.DataValueField = "i_Class_ID";
            this.cboClassNumber.DataTextField = "u_Description";
            this.cboClassNumber.DataBind();
            con5.Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Main.aspx", true);
		}

		private void btnContinue_Click(object sender, System.EventArgs e)
		{
			InsertComment
			(
				InsertItem(
					Convert.ToInt32(cboClassNumber.SelectedValue), 
					this.txtTrilateralID.Text.Replace("'", "''"), 
					2, 
					true)
			);

			this.btnEnterNewID.Visible = false;
			this.txtTrilateralID.Visible = false;
			this.cboClassNumber.Visible = false;
			this.lblCandidateID.Visible = false;
			this.lblClass.Visible = false;
			this.lblComment.Visible = false;
			this.txtComment.Visible = false;
		}

		private void InsertComment(Int32 itemID)
		{
			if(this.txtComment.Text != null && this.txtComment.Text != "")
			{
				//SqlConnection con = new SqlConnection((string)ConfigurationSettings.AppSettings["sqlConnectionString"]);
                SqlConnection con6 = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
				con6.Open(); 
				
				string sql = "INSERT INTO TRI_Comments"
					+ " (i_Item_ID, dt_Created, i_User_ID_Created_By, u_Comment)"
					+ " VALUES (" + itemID.ToString().Trim() + ", getdate(), " + UserInfo.UserID.ToString() + ", N'" + this.txtComment.Text.Replace("'", "''") + "')";

				SqlCommand cmd = new SqlCommand(sql, con6);
				cmd.ExecuteNonQuery();
				con6.Close();
				this.txtComment.Text = "";
			}
			else if (lbldgComment.Text != null && lbldgComment.Text != "" && lbldgComment.Text != "&nbsp;")
			{				
				//SqlConnection con = new SqlConnection((string)ConfigurationSettings.AppSettings["sqlConnectionString"]);
                SqlConnection con7 = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
				con7.Open(); 
					
				string sql = "INSERT INTO TRI_Comments"
					+ " (i_Item_ID, dt_Created, i_User_ID_Created_By, u_Comment)"
					+ " VALUES (" + itemID.ToString().Trim() + ", getdate(), " + UserInfo.UserID.ToString() + ", N'" + this.lbldgComment.Text.Replace("'", "''") + "')";

				SqlCommand cmd = new SqlCommand(sql, con7);
				cmd.ExecuteNonQuery();
				con7.Close();
				this.txtComment.Text = "";				
			}
		}

		public void btnAdd_Click(object sender, System.EventArgs e)
		{
			// Make confirmation buttons invisible
			btnYes.Visible = false;
			btnNo.Visible = false;
			lblConfirmation.Text = "";

			// Duplicate Checking 
			IDText = txtTrilateralID.Text.Trim();
			lblConflictID.Text = "         ";

			// Check for duplicate items in list
			for(int itemNum = 0; itemNum < DataGrid1.Items.Count; itemNum++)
			{
				if (DataGrid1.Items[itemNum].Cells[2].Text == IDText && DataGrid1.Items[itemNum].Cells[3].Text == cboClassNumber.SelectedValue) //&& DataGrid1.Items[itemNum].Cells[3].Text == 
				{
					dupIDExists = 1;
					btnSubmitAll.Visible = false;
					rdoSubmitFirstMonth.Visible = false;
					rdoSubmitNow.Visible = false;
					lblRepeatIDs.Text = "- Item(s) repeated in list!  Delete/Edit before submitting." + "<br>";
				}
			}

			if(Page.IsValid)
			{
				// Check for existing items in DB
				string sql = "SELECT i.i_Item_ID, c.u_Description, u_Item_Name, s.u_Code_Meaning, i.i_Class_ID, i.i_Status"
					+ " FROM TRI_Items i"
					+ " LEFT JOIN TRI_Classes c ON i.i_Class_ID = c.i_Class_ID"
					+ " LEFT JOIN TRI_Lookup_Status s ON i.i_Status = s.i_Code_Value"
					+ " WHERE u_Item_Name = N'" + IDText.Replace("'", "''") + "'";
                SqlConnection con8 = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand(sql, con8);
				con8.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				Int32 dupID;
				Int32 dupClassID;
				Int32 dupStatusID;
				string dupClass;
				string dupStatus;
				string dupName;

				lblInstructions.Visible = false;

				if(reader.HasRows)
				{
					this.lblRejectedID.Text = "<br><br>The submitted Item already exists, but has been rejected or removed from the Trilateral Approved List. If you wish to resubmit this item, view Item details and click the Resubmit button.<br><table border=1 cellspacing=0 style='background-color:White;border-color:#222222;border-width:1px;border-style:solid;border-collapse:collapse;' class=normalfont><tr bgcolor=#6F0000 class=normalfont style=color:#FFFFFF><td><b>Item</b></td><td><b>Class</b></td><td><b>Status</b></td></tr>";
					this.lblWarningID.Text = "<br><br>The following Items with similar names exist in different classes:<br><table border=1 cellspacing=0 style='background-color:White;border-color:#222222;border-width:1px;border-style:solid;border-collapse:collapse;' class=normalfont><tr bgcolor=#6F0000 class=normalfont style=color:#FFFFFF><td><b>Item</b></td><td><b>Class</b></td><td><b>Status</b></td></tr>";
					
					while(reader.Read())
					{
						dupID = reader.GetInt32(0);
						dupClass = reader.GetString(1);
						dupName = reader.GetString(2);
						dupStatus = reader.GetString(3);
						dupClassID = reader.GetInt32(4);
						dupStatusID = reader.GetInt32(5);

						// If existing items exist as Pending or Accepted, reject submission (exact match)
						if( (dupStatusID == 1 || dupStatusID == 2) && dupClassID == Convert.ToInt32(this.cboClassNumber.SelectedValue) )
						{
							this.lblConflictID.Text = "";
							this.lblConflictID.Text = "Duplicate Item";
							this.lblConflictID.Text += ": " + "<a href='Details.aspx?i=" + dupID.ToString() + "'>" + "<SPAN TITLE=" + "Duplicate-Inbox/Approved" + ">" + "Details" + "</SPAN>" + "</a>" + "<br>";
							dupIDExists = 1;
							lblSubmissionError.Text = dupIDExists.ToString();
							lblMessage.Visible = true;
							lblMessage.Text = "- Duplicate Items cannot be submitted.  Delete/Edit before submitting." + "<br>";
							btnSubmitAll.Visible = false;
							rdoSubmitFirstMonth.Visible = false;
							rdoSubmitNow.Visible = false;
						}

						// If existing items exist as Rejected or Removed, give option to resubmit duplicate or recreate new copy (exact match).
						if( (dupStatusID == 0 || dupStatusID == 3) && dupClassID == Convert.ToInt32(this.cboClassNumber.SelectedValue) )
						{
							this.lblConflictID.Text += "Warning";
							lblConflictID.Text += ": " + "<a href='Details.aspx?i=" + dupID.ToString() + "'>" + "<SPAN TITLE=" + "Duplicate-Rejected/Removed" + ">" + "Details" + "</SPAN>" + "</a>" + "<br>";
						}

						// if duplicate item names exist in different classes
						if(dupClassID != Convert.ToInt32(this.cboClassNumber.SelectedValue) && (dupStatusID == 1 || dupStatusID == 2) )
						{
							this.lblConflictID.Text += "Warning";
							lblConflictID.Text += ": " + "<a href='Details.aspx?i=" + dupID.ToString() + "'>" + "<SPAN TITLE=" + "Warning" + ">" + "Details" + "</SPAN>" + "</a>" + "<br>";
						}
					}
				}
				con8.Close();
			}
			// Duplicate Checking 

			// Make Submit button visible only if no duplicate items exist
			if (dupIDExists != 1)
				btnSubmitAll.Visible = true;
				rdoSubmitFirstMonth.Visible = true;
				rdoSubmitNow.Visible = true;
			
			if (dupIDExists != 1)
				btnSubmitAll.Visible = true;
				rdoSubmitFirstMonth.Visible = true;
				rdoSubmitNow.Visible = true;

			if (Session["SampleDataTable"]==null)
			{ 
				DataColumn dc; 

				dc = new DataColumn("Proposed Item",System.Type.GetType("System.String")); 
				dtg.Columns.Add(dc); 
				dc = new DataColumn("Class Number",System.Type.GetType("System.String")); 
				dtg.Columns.Add(dc); 
				dc = new DataColumn("Comment",System.Type.GetType("System.String")); 
				dtg.Columns.Add(dc); 
				dc = new DataColumn("DB Status",System.Type.GetType("System.String")); 
				dtg.Columns.Add(dc); 
			} 
			else {dtg = (DataTable) Session["SampleDataTable"];} 

			dr = dtg.NewRow();

			dr["Proposed Item"]=txtTrilateralID.Text.Trim();
			dr["Class Number"]=cboClassNumber.SelectedValue;
			dr["Comment"]=txtComment.Text;
			dr["DB Status"]=lblConflictID.Text;
			dtg.Rows.Add(dr);

			DataGrid1.DataSource=dtg;
			DataGrid1.PageSize+=1;
			DataGrid1.DataBind();
			Session["SampleDataTable"]=dtg;
			DataGrid1.Visible = true;

			Response.Redirect("SubmitID.aspx");
		}

		protected void btnSubmitAll_Click(object sender, System.EventArgs e)
		{
			btnYes.Visible = true;
			btnNo.Visible = true;
			lblConfirmation.Text = "Are you sure you want to SUBMIT the Item(s) above?<br>";
			btnSubmitAll.Visible = false;
			rdoSubmitFirstMonth.Visible = false;
			rdoSubmitNow.Visible = false;
			DataGrid1.Columns[0].Visible = false;
			DataGrid1.Columns[1].Visible = false;
		}

		protected void cboClassNumber_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			btnAdd.Visible = true;
		}

		private void DataGrid1_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) 
		{
            if (DataGrid1.Items.Count > e.Item.ItemIndex)
            {
                dtg = (DataTable)Session["SampleDataTable"];
                DataGrid1.DataSource = dtg;
                int rowToDelete = e.Item.ItemIndex;
                lblRowNum.Text = rowToDelete.ToString();
                DataGrid1.EditItemIndex = -1;
                dtg.Rows[rowToDelete].Delete();
                DataGrid1.PageSize += 1;
                DataGrid1.DataBind();
                Session["SampleDataTable"] = dtg;

                // Check for duplicate ID(s)
                dupIDExists = 0;
                btnSubmitAll.Visible = true;
                rdoSubmitFirstMonth.Visible = true;
                rdoSubmitNow.Visible = true;
                lblMessage.Text = "";

                for (int itemNum = 0; itemNum < DataGrid1.Items.Count; itemNum++)
                {
                    if (DataGrid1.Items[itemNum].Cells[5].Text.Substring(0, 9) == "Duplicate")
                    {
                        dupIDExists = 1;
                        btnSubmitAll.Visible = false;
                        rdoSubmitFirstMonth.Visible = false;
                        rdoSubmitNow.Visible = false;
                        lblMessage.Text = "- Duplicate Items cannot be submitted.  Delete/Edit before submitting." + "<br>";
                    }
                }

                // Check for repeat items in list
                int dupCount = 0;

                for (int itemNum = 0; itemNum < DataGrid1.Items.Count; itemNum++)
                {
                    for (int itemNumComp = 0; itemNumComp < DataGrid1.Items.Count; itemNumComp++)
                    {
                        if (DataGrid1.Items[itemNum].Cells[2].Text == DataGrid1.Items[itemNumComp].Cells[2].Text && DataGrid1.Items[itemNum].Cells[3].Text == DataGrid1.Items[itemNumComp].Cells[3].Text)
                            dupCount = dupCount + 1;
                    }
                }
                if (dupCount != DataGrid1.Items.Count)
                {
                    lblRepeatIDs.Text = "- Item(s) repeated in list.  Delete/Edit before submitting." + "<br>";
                    btnSubmitAll.Visible = false;
                    rdoSubmitFirstMonth.Visible = false;
                    rdoSubmitNow.Visible = false;
                    lblDupIDForComparison.Text = dupCount.ToString();
                }
                if (dupCount == DataGrid1.Items.Count)
                {
                    lblRepeatIDs.Text = "" + "<br>";
                    lblRepeatIDs.Text = "";
                    lblDupIDForComparison.Text = dupCount.ToString();
                }
                Response.Redirect("SubmitID.aspx");
            }
		}

		private void DataGrid1_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
            if (DataGrid1.Items.Count > e.Item.ItemIndex)
            {

                dtg = (DataTable)Session["SampleDataTable"];

                DataGrid1.EditItemIndex = e.Item.ItemIndex;
                DataGrid1.DataSource = dtg;
                DataGrid1.DataBind();
                Session["SampleDataTable"] = dtg;

                // Turn off Update Button validation
                LinkButton updateBtn = (LinkButton)this.DataGrid1.Items[e.Item.ItemIndex].Cells[0].Controls[0];
                updateBtn.CausesValidation = false;

                // Make Submit button invisible until item is updated
                btnSubmitAll.Visible = false;
                lblMessage.Text = "Click Update link for Submit button.";
                lblMessage.Visible = true;
            }
		}

		private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
            if (DataGrid1.Items.Count > e.Item.ItemIndex)
            {
                dtg = (DataTable)Session["SampleDataTable"];

                lblConflictID.Text = "         ";

                // Retrieves ID from Text Box Control
                System.Web.UI.WebControls.TextBox cID = new System.Web.UI.WebControls.TextBox();
                cID = (System.Web.UI.WebControls.TextBox)e.Item.Cells[2].Controls[0];

                // Retrieves Class Number from Text Box Control
                System.Web.UI.WebControls.TextBox cClassNum = new System.Web.UI.WebControls.TextBox();
                cClassNum = (System.Web.UI.WebControls.TextBox)e.Item.Cells[3].Controls[0];

                // Retrieves Comment from Text Box Control
                System.Web.UI.WebControls.TextBox cComment = new System.Web.UI.WebControls.TextBox();
                cComment = (System.Web.UI.WebControls.TextBox)e.Item.Cells[4].Controls[0];

                // Retrieves DB Status from Text Box Control
                System.Web.UI.WebControls.TextBox cDBStatus = new System.Web.UI.WebControls.TextBox();
                cDBStatus = (System.Web.UI.WebControls.TextBox)e.Item.Cells[5].Controls[0];

                // Enables editing
                DataGrid1.EditItemIndex = -1;

                // Deletes row - pre edit
                int rowToDelete = e.Item.ItemIndex;
                dtg.Rows[rowToDelete].Delete();

                // Check for duplicates
                dupIDExists = 0;
                string sql = "SELECT i.i_Item_ID, c.u_Description, u_Item_Name, s.u_Code_Meaning, i.i_Class_ID, i.i_Status"
                    + " FROM TRI_Items i"
                    + " LEFT JOIN TRI_Classes c ON i.i_Class_ID = c.i_Class_ID"
                    + " LEFT JOIN TRI_Lookup_Status s ON i.i_Status = s.i_Code_Value"
                    + " WHERE u_Item_Name = '" + cID.Text + "'";
                SqlConnection con9 = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand(sql, con9);
                con9.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Int32 dupID;
                Int32 dupClassID;
                Int32 dupStatusID;
                string dupClass;
                string dupStatus;
                string dupName;
                lblInstructions.Visible = false;

                if (reader.HasRows)
                {
                    this.lblRejectedID.Text = "<br><br>The submitted Item already exists, but has been rejected or removed from the Trilateral Approved List. If you wish to resubmit this item, view Item details and click the Resubmit button.<br><table border=1 cellspacing=0 style='background-color:White;border-color:#222222;border-width:1px;border-style:solid;border-collapse:collapse;' class=normalfont><tr bgcolor=#6F0000 class=normalfont style=color:#FFFFFF><td><b>Item</b></td><td><b>Class</b></td><td><b>Status</b></td></tr>";
                    this.lblWarningID.Text = "<br><br>The following Items with similar names exist in different classes:<br><table border=1 cellspacing=0 style='background-color:White;border-color:#222222;border-width:1px;border-style:solid;border-collapse:collapse;' class=normalfont><tr bgcolor=#6F0000 class=normalfont style=color:#FFFFFF><td><b>Item</b></td><td><b>Class</b></td><td><b>Status</b></td></tr>";

                    while (reader.Read())
                    {
                        dupID = reader.GetInt32(0);
                        dupClass = reader.GetString(1);
                        dupName = reader.GetString(2);
                        dupStatus = reader.GetString(3);
                        dupClassID = reader.GetInt32(4);
                        dupStatusID = reader.GetInt32(5);

                        // If existing items exist as Pending or Accepted, reject submission (exact match)
                        if ((dupStatusID == 1 || dupStatusID == 2) && dupClassID == Convert.ToInt32(cClassNum.Text))//Convert.ToInt32(this.cboClassNumber.SelectedValue) )
                        {
                            // List duplicate item info
                            this.lblConflictID.Text = "";
                            this.lblConflictID.Text = "Duplicate Item";
                            this.lblConflictID.Text += ": " + "<a href='Details.aspx?i=" + dupID.ToString() + "'>" + "<SPAN TITLE=" + "Duplicate-Inbox/Approved" + ">" + "Details" + "</SPAN>" + "</a>" + "<br>";
                            dupIDExists = 1;
                            lblSubmissionError.Text = dupIDExists.ToString();
                            lblMessage.Visible = true;
                            lblMessage.Text = "- Duplicate Items cannot be submitted.  Delete/Edit before submitting." + "<br>";
                            btnSubmitAll.Visible = false;
                            rdoSubmitFirstMonth.Visible = false;
                            rdoSubmitNow.Visible = false;
                        }
                        // Iif existing items exist as Rejected or Removed, give option to resubmit duplicate or recreate new copy (exact match).
                        if ((dupStatusID == 0 || dupStatusID == 3) && dupClassID == Convert.ToInt32(cClassNum.Text))//Convert.ToInt32(this.cboClassNumber.SelectedValue) )
                        {
                            this.lblConflictID.Text += "Warning";
                            lblConflictID.Text += ": " + "<a href='Details.aspx?i=" + dupID.ToString() + "'>" + "<SPAN TITLE=" + "Duplicate-Rejected/Removed" + ">" + "Details" + "</SPAN>" + "</a>" + "<br>";
                            btnSubmitAll.Visible = true;
                            rdoSubmitFirstMonth.Visible = true;
                            rdoSubmitNow.Visible = true;
                        }
                        // If duplicate item names exist in different classes
                        if (dupClassID != Convert.ToInt32(cClassNum.Text) && (dupStatusID == 1 || dupStatusID == 2))
                        {
                            this.lblConflictID.Text += "Warning";
                            lblConflictID.Text += ": " + "<a href='Details.aspx?i=" + dupID.ToString() + "'>" + "<SPAN TITLE=" + "Warning" + ">" + "Details" + "</SPAN>" + "</a>" + "<br>";
                            btnSubmitAll.Visible = true;
                            rdoSubmitFirstMonth.Visible = true;
                            rdoSubmitNow.Visible = true;
                        }
                        // If item is not a duplicate
                        if (dupIDExists != 1)
                            lblMessage.Text = "";
                        lblTest.Text = Convert.ToString(dupIDExists);
                    }
                }
                con9.Close();

                // Adds new row - post edit
                dr = dtg.NewRow();

                // Assign updated conflict status
                cDBStatus.Text = this.lblConflictID.Text;

                // Write updated values to Datagrid
                dr["Proposed Item"] = cID.Text;
                dr["Class Number"] = cClassNum.Text;
                dr["Comment"] = cComment.Text;
                dr["DB Status"] = cDBStatus.Text;
                dtg.Rows.Add(dr);

                DataGrid1.DataSource = dtg;
                DataGrid1.DataBind();

                lblConflictID.Text = "No Conflicts";

                // Check for repeat items in list
                int dupCount = 0;

                for (int itemNum = 0; itemNum < DataGrid1.Items.Count; itemNum++)
                {
                    for (int itemNumComp = 0; itemNumComp < DataGrid1.Items.Count; itemNumComp++)
                    {
                        if (DataGrid1.Items[itemNum].Cells[2].Text == DataGrid1.Items[itemNumComp].Cells[2].Text && DataGrid1.Items[itemNum].Cells[3].Text == DataGrid1.Items[itemNumComp].Cells[3].Text)
                            dupCount = dupCount + 1;
                    }
                }
                if (dupCount != DataGrid1.Items.Count)
                {
                    lblRepeatIDs.Text = "- Item(s) repeated in list.  Delete/Edit before submitting." + "<br>";
                    btnSubmitAll.Visible = false;
                    rdoSubmitFirstMonth.Visible = false;
                    rdoSubmitNow.Visible = false;
                    lblDupIDForComparison.Text = dupCount.ToString();
                }
                if (dupCount == DataGrid1.Items.Count)
                {
                    lblRepeatIDs.Text = "" + "<br>";
                    lblRepeatIDs.Text = "";
                    lblDupIDForComparison.Text = dupCount.ToString();
                }
                // Make Submit button visible only if no duplicate items exist
                if (dupIDExists != 1 && dupCount == DataGrid1.Items.Count)
                {
                    btnSubmitAll.Visible = true;
                    rdoSubmitFirstMonth.Visible = true;
                    rdoSubmitNow.Visible = true;
                    lblMessage.Text = "";
                }
                if (dupIDExists != 1)
                    lblMessage.Text = "";

                for (int itemNum = 0; itemNum < DataGrid1.Items.Count; itemNum++)
                {
                    if (DataGrid1.Items[itemNum].Cells[5].Text.Substring(0, 9) == "Duplicate")
                    {
                        dupIDExists = 1;
                        btnSubmitAll.Visible = false;
                        rdoSubmitFirstMonth.Visible = false;
                        rdoSubmitNow.Visible = false;
                        lblMessage.Text = "- Duplicate Items cannot be submitted.  Delete/Edit before submitting." + "<br>";
                    }
                }
            }
		}
		
		private void checkDupes()
		{


		}

		private void DataGrid1_CancelCommand(object source, 
			System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			dtg = (DataTable) Session["SampleDataTable"];
			DataGrid1.EditItemIndex = -1;
			DataGrid1.DataSource=dtg;
			DataGrid1.DataBind();
		}

		protected void btnYes_Click(object sender, System.EventArgs e)
		{
			DataGrid1.Columns[0].Visible = true;
			DataGrid1.Columns[1].Visible = true;

			dtg = (DataTable) Session["SampleDataTable"];
			// Code for duplicate checking
			for(int itemNum = 0; itemNum < DataGrid1.Items.Count; itemNum++)
			{
				lbldgID.Text = DataGrid1.Items[itemNum].Cells[2].Text.Trim();
				lbldgClass.Text = DataGrid1.Items[itemNum].Cells[3].Text.Trim();
				
				string sql = "SELECT i.i_Item_ID, c.u_Description, u_Item_Name, s.u_Code_Meaning, i.i_Class_ID, i.i_Status"
					+ " FROM TRI_Items i"
					+ " LEFT JOIN TRI_Classes c ON i.i_Class_ID = c.i_Class_ID"
					+ " LEFT JOIN TRI_Lookup_Status s ON i.i_Status = s.i_Code_Value"
					+ " WHERE u_Item_Name = N'" + lbldgID.Text.Replace("'", "''") + "'";
                SqlConnection con10 = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
				SqlCommand cmd = new SqlCommand(sql, con10);
				con10.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				Int32 dupID;
				Int32 dupClassID;
				Int32 dupStatusID;
				string dupClass;
				string dupStatus;
				string dupName;

				lblInstructions.Visible = false;
				if(reader.HasRows)
				{
					this.lblRejectedID.Text = "<br><br>The submitted Item already exists, but has been rejected or removed from the Trilateral Approved List. If you wish to resubmit this item, view Item details and click the Resubmit button.<br><table border=1 cellspacing=0 style='background-color:White;border-color:#222222;border-width:1px;border-style:solid;border-collapse:collapse;' class=normalfont><tr bgcolor=#6F0000 class=normalfont style=color:#FFFFFF><td><b>Item</b></td><td><b>Class</b></td><td><b>Status</b></td></tr>";
					this.lblWarningID.Text = "<br><br>The following Items with similar names exist in different classes:<br><table border=1 cellspacing=0 style='background-color:White;border-color:#222222;border-width:1px;border-style:solid;border-collapse:collapse;' class=normalfont><tr bgcolor=#6F0000 class=normalfont style=color:#FFFFFF><td><b>Item</b></td><td><b>Class</b></td><td><b>Status</b></td></tr>";
	
					while(reader.Read())
					{
						dupID = reader.GetInt32(0);
						dupClass = reader.GetString(1);
						dupName = reader.GetString(2);
						dupStatus = reader.GetString(3);
						dupClassID = reader.GetInt32(4);
						dupStatusID = reader.GetInt32(5);

						// If existing items exist as Pending or Accepted, reject submission (exact match)
						if( (dupStatusID == 1 || dupStatusID == 2) && dupClassID == Convert.ToInt32(lbldgClass.Text))
						{
							// List duplicate item info
							this.lblConflictID.Text = "";
							this.lblConflictID.Text = "Duplicate Item";
							this.lblConflictID.Text += ": " + "<a href='Details.aspx?i=" + dupID.ToString() + "'>" + "<SPAN TITLE=" + "Duplicate-Inbox/Approved" + ">" + "Details" + "</SPAN>" + "</a>" + "<br>";
							dupIDExists = 1;
							lblSubmissionError.Text = dupIDExists.ToString();
							lblMessage.Visible = true;
							lblMessage.Text = "- Duplicate Items cannot be submitted.  Delete/Edit before submitting." + "<br>";
							btnSubmitAll.Visible = false;
							rdoSubmitFirstMonth.Visible = false;
							rdoSubmitNow.Visible = false;
						}
						// If existing items exist as Rejected or Removed, give option to resubmit duplicate or recreate new copy (exact match).
						if( (dupStatusID == 0 || dupStatusID == 3) && dupClassID == Convert.ToInt32(lbldgClass.Text))
						{

							this.lblConflictID.Text += "Warning";
							lblConflictID.Text += ": " + "<a href='Details.aspx?i=" + dupID.ToString() + "'>" + "<SPAN TITLE=" + "Duplicate-Rejected/Removed" + ">" + "Details" + "</SPAN>" + "</a>" + "<br>";
							btnSubmitAll.Visible = true;
							rdoSubmitFirstMonth.Visible = false;
							rdoSubmitNow.Visible = false;
						}
						// If duplicate item names exist in different classes
						if(dupClassID != Convert.ToInt32(lbldgClass.Text) && (dupStatusID == 1 || dupStatusID == 2) )
						{
							this.lblConflictID.Text += "Warning";
							lblConflictID.Text += ": " + "<a href='Details.aspx?i=" + dupID.ToString() + "'>" + "<SPAN TITLE=" + "Warning" + ">" + "Details" + "</SPAN>" + "</a>" + "<br>";
							btnSubmitAll.Visible = true;
							rdoSubmitFirstMonth.Visible = false;
							rdoSubmitNow.Visible = false;
						}
						// If item is not a duplicate
						if(dupIDExists != 1)
							lblMessage.Text = "";
						lblTest.Text = Convert.ToString(dupIDExists);
					}					
				}
				reader.Close();
				con10.Close();
			} // End for-loop for bug checking

			TmpRcdCount = RcdCount + DataGrid1.Items.Count;
			if (TmpRcdCount < 51)
			{
				if (rdoSubmitNow.Checked)
				{								   
					for(int itemNum = 0; itemNum < DataGrid1.Items.Count; itemNum++)
					{
						lbldgID.Text = DataGrid1.Items[itemNum].Cells[2].Text;
						lbldgClass.Text = DataGrid1.Items[itemNum].Cells[3].Text;
						lbldgComment.Text = DataGrid1.Items[itemNum].Cells[4].Text;
						InsertComment
							(
							InsertItem(
							Convert.ToInt32(lbldgClass.Text), 
							this.lbldgID.Text.Replace("'", "''"), 
							2, 
							true)
							);
					}
				}

				Bind_DataGrid1("i.dt_Released ASC", 2);
				DataGrid1.DataSource=dtg;
				DataGrid1.PageSize+=1;
				DataGrid1.DataBind();
				dtg.Clear();
				dtg.Dispose();
				DataGrid1.Dispose();
				DataGrid1.Visible = false;
				btnAdd.Visible = false;
				
				if (rdoSubmitNow.Checked)
				{						
					// Redirect to Inbox upon successful Submittal
					dtg.Clear();
					Response.Redirect("Main.aspx", true);
				}

				if (rdoSubmitFirstMonth.Checked)
				{		
					for(int itemNum = 0; itemNum < DataGrid1.Items.Count; itemNum++)
					{
						lbldgID.Text = DataGrid1.Items[itemNum].Cells[2].Text;
						lbldgClass.Text = DataGrid1.Items[itemNum].Cells[3].Text;
						lbldgComment.Text = DataGrid1.Items[itemNum].Cells[4].Text;
						InsertComment
							(
							InsertItemFirstOfMonth(
							Convert.ToInt32(lbldgClass.Text), 
							this.lbldgID.Text.Replace("'", "''"), 
							2, 
							true)
							);
					}

					// Redirect to Inbox upon successful Submittal
					dtg.Clear();
					lblConfirmation.Text = "Item(s) will be submitted the first day of next month.";
					btnYes.Visible = false;
					btnNo.Visible = false;
					InboxReturnLink.Visible = true;
				}
			}

			if (TmpRcdCount > 50)
			{
				lblMessage.Text = "- Monthly submittal limit will be exceeded.  Remove item(s) from list." + "<br>";
				lblConfirmation.Text = "";
				btnSubmitAll.Visible = false;
				rdoSubmitFirstMonth.Visible = false;
				rdoSubmitNow.Visible = false;
				btnYes.Visible = false;
				btnNo.Visible = false;
			}
			lblListRecdCount.Text = RcdCount.ToString();
		}

		protected void btnNo_Click(object sender, System.EventArgs e)
		{
			DataGrid1.Columns[0].Visible = true;
			DataGrid1.Columns[1].Visible = true;

			btnSubmitAll.Visible = true;
			rdoSubmitFirstMonth.Visible = true;
			rdoSubmitNow.Visible = true;
			btnYes.Visible = false;
			btnNo.Visible = false;
			lblConfirmation.Text = "";
		}

		protected void rdoSubmitNow_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void rdoSubmitFirstMonth_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}

        private void parseDropDown(String eventArgument)
        {
            var parsed = new ArrayList();
            string trim = eventArgument.Substring(0, eventArgument.Length - 5).Substring(21);
            string[] delimiters = new string[] { "<LI>", "<li>" };
            string[] split = trim.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in split)
            {
                if (str.IndexOf("</A>") > 0)
                    parsed.Add(str.Remove(str.IndexOf("</A>")).Substring(str.IndexOf('>') + 1));
                if (str.IndexOf("</a>") > 0)
                    parsed.Add(str.Remove(str.IndexOf("</a>")).Substring(str.IndexOf('>') + 1));
            }

            usersLoggedIn(parsed);
        }

        private void usersLoggedIn(ArrayList currentUsers)
        {
            var users = new ArrayList();
            var emails = new ArrayList();
            bool newUsers = false;
            string sql = "SELECT u_username, u_email_address FROM Tri_Users WHERE u_logged_in = 1";

            //SqlConnection conn = new SqlConnection((string)ConfigurationSettings.AppSettings["sqlConnectionString"]);
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();


            if (reader.HasRows && UserInfo != null)
            {
                while (reader.Read())
                {
                    if (reader.GetString(0) != UserInfo.Username)
                    {
                        users.Add(reader.GetString(0));
                        emails.Add(reader.GetString(1));
                    }
                }
                reader.Close();
            }

            if (users.Count != currentUsers.Count && users.Count > currentUsers.Count)
            {
                newUsers = true;
            }
            else
            {
                foreach (var s in users)
                {
                    if (!currentUsers.Contains(s))
                        newUsers = true;
                    else
                        newUsers = false;
                }
            }

            if (newUsers)
                gifSrc = "images/tril_flashing.gif";
            else if (users.Count > 0)
                gifSrc = "images/tril_active.gif";
            else
                gifSrc = "images/tril_inactive.gif";

            conn.Close();

            returnValue = writeHtmlDropdown(users, emails) + "|" + gifSrc;

        }

        private string writeHtmlDropdown(ArrayList users, ArrayList emails)
        {

            string html = "";

            if (users.Count > 0)
            {
                html = "<UL class=bluetabs>\r\n";

                for (int i = 0; i < users.Count; i++)
                {
                    html += ("<LI><A style=\"BORDER-TOP-WIDTH: 0px\" href=\"mailto:" + emails[i] + "\">" + users[i] + "</A> \r\n<LI>");
                }

                html += "</UL>";
            }
            else
                html = "<UL class=bluetabs>\r\n<LI><A href=\"#\">No Users Currently Online</A>\r\n</LI></UL>";

            return html;
        }

        public void RaiseCallbackEvent(String eventArgument)
        {
            parseDropDown(eventArgument);
        }

        public String GetCallbackResult()
        {
            return returnValue;
        }

	}
}
