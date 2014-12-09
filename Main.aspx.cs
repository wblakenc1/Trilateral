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
using Newtonsoft.Json;

namespace TMID
{
    public partial class Main : System.Web.UI.Page
    {
        private UserAuthInfo UserInfo;
        private Int32 itemID;
        string itemName;

        public DataTable SerializeDataTable()
        {
            string t = ParsedArrayString;
            var table = JsonConvert.DeserializeObject<DataTable>(t);
            return table;
        }

        protected void Page_Load(object send, EventArgs args)
        {
            //cboMyLists.Items.Add(new ListItem("My Saved Lists", "1"));
            //cboMyLists.Items.Add(new ListItem("Pending Class 12", "2"));
            //cboMyLists.Items.Add(new ListItem("Pending No JPO Vote", "3"));

            UserInfo = new UserAuthInfo("|".ToCharArray());

            lblUserName.Text = UserInfo.Username;
            lblUserInfo.Text = UserInfo.PartnerID + ", " + UserInfo.UserID;
            String CanVote = (String)Session["CanVote"];
            Session["CanVote"] = CanVote;

            string temp = (string)(Session["CanVote"]);
            valHasPartnerVote.Value = temp;
            valPartnerID.Value = UserInfo.PartnerID.ToString();
            lblCanVote.Text = temp;

            //sqlDS = "All";

            //if (!IsPostBack)
            //{
                //Grid1.DataSource = SqlDataSource1;
                //Grid1.DataBind();
            //}
            //else
            //{
                //Grid1.DataSource = SqlDataSourceMainPending;
                //Grid1.DataBind();
            //}

            if (temp == "Visitor")
            {
                BtnRemove.Visible = false;
                BtnResubmit.Visible = false;
                BtnWithdraw.Visible = false;
            }

            if (temp == "Partner")
            {
                //document.getElementById("BtnRemove").style.visibility = "hidden";
                //document.getElementById("BtnResubmit").style.visibility = "hidden";
                //document.getElementById("BtnWithdraw").style.visibility = "hidden";
            }

            // Get 
            string cItem1 = TMID.GlobalData.cItem;

            // Initialize data
            if (cItem1 == null)
            {
                cItem1 = "Asparagus Tips".ToString();
                TMID.GlobalData.cItem = cItem1;
            }

            Hidden1.Value = "Hello"; 
            //String sql = "SELECT ti.i_Item_ID As [ID], " +
            //              "ti.u_Item_Name As [Item], " +
            //              "ti.i_Class_ID As [Class], " +
            //              "(select u_Code_Meaning " +
            //              "from TRI_Lookup_Status " +
            //              "where ti.i_Status = i_Code_Value) As [Status], " +
            //              "COUNT(tv.i_Item_ID) As [Vote Count], " +
            //              "ISNULL((select top 1 u_vote_name " +
            //              "from Tri_Vote_Types tvt " +
            //              "join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id " +
            //              "where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID " +
            //              "and tvsq.i_Partner_ID = 1 " +
            //              "and tvsq.i_Item_ID = ti.i_Item_ID " +
            //              "order by tvsq.dt_Created),'Pending') As [USPTO Vote], " +

            //              "ISNULL((select top 1 u_vote_name " +
            //              "from Tri_Vote_Types tvt " +
            //              "join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id " +
            //              "where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID " +
            //              "and tvsq.i_Item_ID = ti.i_Item_ID " +
            //              "and tvsq.i_Partner_ID = 3 " +
            //              "order by tvsq.dt_Created),'Pending') As [JPO Vote], " +
            //              "ISNULL((select top 1 u_vote_name " +
            //              "from Tri_Vote_Types tvt " +
            //              "join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id " +
            //              "where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID " +
            //              "and tvsq.i_Item_ID = ti.i_Item_ID " +
            //              "and tvsq.i_Partner_ID = 5 " +
            //              "order by tvsq.dt_Created),'Pending') As [OHIM Vote], " +

            //               "ISNULL((select top 1 u_vote_name " +
            //               "from Tri_Vote_Types tvt " +
            //               "join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id " +
            //               "where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID " +
            //               "and tvsq.i_Item_ID = ti.i_Item_ID " +
            //               "and tvsq.i_Partner_ID = 7 " +
            //               "order by tvsq.dt_Created),'Pending') As [Korea Vote], " +

            //               "ISNULL((select top 1 u_vote_name " +
            //               "from Tri_Vote_Types tvt " +
            //               "join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id " +
            //               "where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID " +
            //               "and tvsq.i_Item_ID = ti.i_Item_ID " +
            //               "and tvsq.i_Partner_ID = 9 " +
            //               "order by tvsq.dt_Created),'Pending') As [Mexico Vote], " +
            //               "ISNULL((select top 1 u_vote_name " +
            //               "from Tri_Vote_Types tvt " +
            //               "join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id " +
            //               "where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID " +
            //               "and tvsq.i_Item_ID = ti.i_Item_ID " +
            //               "and tvsq.i_Partner_ID = 10 " +
            //               "order by tvsq.dt_Created),'Pending') As [Philippines Vote], " +
            //               "ISNULL((select top 1 u_vote_name " +
            //               "from Tri_Vote_Types tvt " +
            //               "join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id " +
            //               "where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID " +
            //               "and tvsq.i_Item_ID = ti.i_Item_ID " +
            //               "and tvsq.i_Partner_ID = 11 " +
            //               "order by tvsq.dt_Created),'Pending') As [Singapore Vote], " +
            //               "ISNULL((select top 1 u_vote_name " +
            //               "from Tri_Vote_Types tvt " +
            //               "join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id " +
            //               "where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID " +
            //               "and tvsq.i_Item_ID = ti.i_Item_ID " +
            //               "and tvsq.i_Partner_ID = 12 " +
            //               "order by tvsq.dt_Created),'Pending') As [Russia Vote], " +
            //               "ISNULL((select top 1 u_vote_name " +
            //               "from Tri_Vote_Types tvt " +
            //               "join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id " +
            //               "where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID " +
            //               "and tvsq.i_Item_ID = ti.i_Item_ID " +
            //               "and tvsq.i_Partner_ID = 14 " +
            //               "order by tvsq.dt_Created),'Pending') As [China Vote], " +
            //               "ti.dt_Created As [Date Created], " +
            //               "ti.dt_Released As [Date Released], " +
            //               "ti.dt_Accepted As [Date Approved], " +
            //               "ti.dt_Rejected As [Date Rejected] " +
            //               "FROM TRI_Items ti " +
            //               "JOIN TRI_Votes tv on ti.i_Item_ID = tv.i_Item_ID " +
            //               "WHERE ti.i_Status = 2 " +
            //               "group by ti.i_Item_ID, ti.u_Item_Name, ti.i_Class_ID, ti.i_Status, ti.dt_Created, ti.dt_Released, ti.dt_Accepted, ti.dt_Rejected";

            if (Grid1.AjaxCallBackMode != AjaxCallBackMode.None)
            {
                // save the last grid state in session - to be used for exporting
                Session["gridFilterPageState"] = Grid1.GetState();
            }
        }

        public static string SessionValue
        {
            get
            {
                object value = HttpContext.Current.Session["SessionValue"];
                return value == null ? "" : (string)value;
            }
            set
            {
                HttpContext.Current.Session["SessionValue"] = value;
            }
        }

        protected void JQGrid1_Searching(object sender, Trirand.Web.UI.WebControls.JQGridSearchEventArgs e)
        {
            if (e.SearchString == "[All]")
                e.Cancel = true;
        }
        protected DataTable GetData()
        {
            if (Session["EditDialogData"] == null)
            {
                // Create a new Sql Connection and set connection string accordingly
                //SqlConnection sqlConnection = new SqlConnection();
                //sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["SQL2008_449777_fhsConnectionString"].ConnectionString;
                //sqlConnection.Open();

                //string sqlStatement = "SELECT CustomerID, CompanyName, Phone, PostalCode, City FROM Customers";

                // Create a SqlDataAdapter to get the results as DataTable
                //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlStatement, sqlConnection);

                DataSourceSelectArguments args = new DataSourceSelectArguments();
                DataView view = (DataView)SqlDataSource1.Select(args);
                DataTable dtResult = view.ToTable();
                // Create a new DataTable
                //DataTable dtResult = new DataTable();

                // Fill the DataTable with the result of the SQL statement
                //SqlDataSource1.DataBind(dtResult);

                Session["EditDialogData"] = dtResult;

                return dtResult;
            }
            else
            {
                return Session["EditDialogData"] as DataTable;
            }
        }

        [WebMethod]
        public static void YourMethod(string yourParam)
        {
            //your code goes here
        }

        protected void JQGrid2_RowAdding(object sender, Trirand.Web.UI.WebControls.JQGridRowAddEventArgs e)
        {
            //DataTable dt = GetData();
            //DataRow row = dt.NewRow();

            string IDForAdd = e.ParentRowKey;
            string selTranslation = e.RowData["Translation"];
            string loggedonUser = UserInfo.UserID.ToString();

            SqlDataSource2.InsertCommandType = SqlDataSourceCommandType.Text;
            SqlDataSource2.InsertCommand = "Insert into TRI_Translations (u_Translation,i_User_ID_Created_By,i_Item_ID,dt_Created) VALUES (@u_Translation, @i_User_ID_Created_By, @i_Item_ID, @dt_Created)";
            SqlDataSource2.InsertParameters.Add("u_Translation", selTranslation);
            SqlDataSource2.InsertParameters.Add("i_User_ID_Created_By", loggedonUser);
            SqlDataSource2.InsertParameters.Add("i_Item_ID", IDForAdd);
            SqlDataSource2.InsertParameters.Add("dt_Created", DateTime.Now.ToString());
            SqlDataSource2.Insert();
        }

        protected void JQGrid3_RowAdding(object sender, Trirand.Web.UI.WebControls.JQGridRowAddEventArgs e)
        {
            //DataTable dt = GetCommentData();
            //DataRow row = dt.NewRow();

            string IDForAdd = e.ParentRowKey;
            string selComment = e.RowData["Comment"];
            string loggedonUser = UserInfo.UserID.ToString();

            SqlDataSource3.InsertCommandType = SqlDataSourceCommandType.Text;
            SqlDataSource3.InsertCommand = "Insert into TRI_Comments (u_Comment,i_User_ID_Created_By,i_Item_ID,dt_Created) VALUES (@u_Comment, @i_User_ID_Created_By, @i_Item_ID, @dt_Created)";
            SqlDataSource3.InsertParameters.Add("u_Comment", selComment);
            SqlDataSource3.InsertParameters.Add("i_User_ID_Created_By", loggedonUser);
            SqlDataSource3.InsertParameters.Add("i_Item_ID", IDForAdd);
            SqlDataSource3.InsertParameters.Add("dt_Created", DateTime.Now.ToString());
            SqlDataSource3.Insert();
        }

        protected DataTable GetTranslationData()
        {
            if (Session["AddNewRowDialogData"] == null)
            {
                // Create a new Sql Connection and set connection string accordingly
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
                //SqlConnection sqlConnection = new SqlConnection();
                //sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["SQL2008_449777_fhsConnectionString"].ConnectionString;
                con.Open();

                string sqlStatement = "SELECT i_Item_ID, u_Translation, i_User_ID_Created_By, dt_Created FROM TRI_Translations";

                // Create a SqlDataAdapter to get the results as DataTable
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlStatement, con);

                // Create a new DataTable
                DataTable dtResult = new DataTable();

                // Fill the DataTable with the result of the SQL statement
                sqlDataAdapter.Fill(dtResult);

                Session["AddNewRowDialogData"] = dtResult;

                return dtResult;
                //con.Close();
            }
            else
            {
                return Session["AddNewRowDialogData"] as DataTable;
            }
        }

        protected DataTable GetCommentData()
        {
            if (Session["AddNewRowDialogData"] == null)
            {
                // Create a new Sql Connection and set connection string accordingly
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
                con.Open();

                string sqlStatement = "SELECT i_Item_ID, u_Comment, i_User_ID_Created_By, dt_Created FROM TRI_Comments";

                // Create a SqlDataAdapter to get the results as DataTable
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlStatement, con);

                // Create a new DataTable
                DataTable dtResult = new DataTable();

                // Fill the DataTable with the result of the SQL statement
                sqlDataAdapter.Fill(dtResult);

                Session["AddNewRowDialogData"] = dtResult;

                return dtResult;
                //con.Close();
            }
            else
            {
                return Session["AddNewRowDialogData"] as DataTable;
            }
        }    

        protected void Grid1_RowEditing(object sender, Trirand.Web.UI.WebControls.JQGridRowEditEventArgs e)
        {
            DataTable dt = GetData();
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID"] };
            DataRow rowEdited = dt.Rows.Find(e.RowKey);

            rowEdited["Item"] = e.RowData["Item"];
            rowEdited["Class"] = e.RowData["Class"];
            rowEdited["Status"] = e.RowData["Status"];
            //rowEdited["USPTO Vote"] = e.RowData["USPTO Vote"];

        }
        protected void ExportToExcelButton_Click(object sender, EventArgs e)
        {
            string exportTitle = "TMID " + DateTime.Now.ToString("yyyy-mm-dd_hh_mm_ss") + ".xls";
            JQGridState gridState = Session["gridFilterPageState"] as JQGridState;
            Grid1.ExportSettings.ExportDataRange = ExportDataRange.All;
            Grid1.ExportToExcel(exportTitle, gridState);
        }
        public void Grid1_DataRequesting(object sender, JQGridDataRequestEventArgs e)
        {

            if (Session["GridDataSource"] != null)
            {
                string dataSource = Session["GridDataSource"] as string;
                if (dataSource == "Pending")
                    Grid1.DataSourceID = "SqlDataSourceMainPending";
            }
            else
            {
                Grid1.DataSourceID = "SqlDataSource1";
            }

            //string Days = "50";
            //SqlDataSource1.SelectParameters.Add("DaysLeft", Days);

        }
        public void JQGrid2_DataRequesting(object sender, JQGridDataRequestEventArgs e)
        {
            SqlDataSource2.SelectParameters["ID"].DefaultValue = e.ParentRowKey;
        }
        public void JQGrid3_DataRequesting(object sender, JQGridDataRequestEventArgs e)
        {
            SqlDataSource3.SelectParameters["ID"].DefaultValue = e.ParentRowKey;
        }
        protected void Grid1_RowSelecting(object sender, Trirand.Web.UI.WebControls.JQGridRowSelectEventArgs e)
        {
            var grdRow = Grid1.SelectedRow;
            var grdItem = Session["Item"] = Grid1.SelectedRow;
            var grdStatus = Session["Status"] = Grid1.SelectedRow;
            Hidden1.Value = "Hello";
        }

        protected void GetHiddenRowID_Click(object sender, EventArgs e)
        {
            //txtRowData.Text = grdItem.ToString();
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            //txtRowData.Text = grdItem.ToString();
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {

        }
        
        protected void btnReject_Click(object sender, EventArgs e)
        {
            //valButtonClicked.Value = BtnReject.Text;
            //Server.Transfer("Action.aspx");
            
        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            //valButtonClicked.Value = BtnRemove.Text;
            //Server.Transfer("Action.aspx");
        }
        protected void btnResubmit_Click(object sender, EventArgs e)
        {
            //valButtonClicked.Value = BtnResubmit.Text;
            //Server.Transfer("Action.aspx");
        }
        protected void btnWithdraw_Click(object sender, EventArgs e)
        {
            //valButtonClicked.Value = BtnWithdraw.Text;
            //Server.Transfer("Action.aspx");
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {

            string[,] arr2D = { 
                    { "55509", "Toilet cisterns", "21", "Pending", "Comment", "Translation" }, 
                    { "55523", "Transmission oils", "33", "Pending", "Comment", "Translation" }, 
                    { "55529", "Fresh currants", "15", "Pending", "Comment", "Translation" }, 
                    { "55510", "Grated potato nuggets", "29", "Pending", "Comment", "Translation" }, 
                    { "55539", "Fresh pine mushrooms", "31", "Pending", "Comment", "Translation" }
                 };

            DataTable dt = new DataTable();

            dt.Columns.Add("ID", Type.GetType("System.String"));
            dt.Columns.Add("Item", Type.GetType("System.String"));
            dt.Columns.Add("Class", Type.GetType("System.String"));
            dt.Columns.Add("Status", Type.GetType("System.String"));
            dt.Columns.Add("Comment", Type.GetType("System.String"));
            dt.Columns.Add("Translation", Type.GetType("System.String"));

            for (int i = 0; i < 5; i++)
            {
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1]["ID"] = arr2D[i, 0];
                dt.Rows[dt.Rows.Count - 1]["Item"] = arr2D[i, 1];
                dt.Rows[dt.Rows.Count - 1]["Class"] = arr2D[i, 2];
                dt.Rows[dt.Rows.Count - 1]["Status"] = arr2D[i, 3];
                dt.Rows[dt.Rows.Count - 1]["Comment"] = arr2D[i, 4];
                dt.Rows[dt.Rows.Count - 1]["Translation"] = arr2D[i, 5];
            }

            //Grid2D.DataSource = SerializeDataTable();
            //Grid2D.DataBind();            
        }

        protected void lnkButtonPending_Click(object sender, EventArgs e)
        {
            Session["GridDataSource"] = "Pending";
        }

        protected void lnkButtonAll_Click(object sender, EventArgs e)
        {
            Session["GridDataSource"] = null;
        }

        public void btnSimpleDialog_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Show Modal Popup", "createDialog();", true);
        }

        //[System.Web.Services.WebMethod]
        //public static void btnInsertVotes_Click(string name, string email, string message)

        public void btnInsertVotes_Click(object sender, EventArgs e)
        {

            //String sql = "INSERT INTO TRI_Votes (i_Item_ID, i_User_ID, i_Partner_ID, i_vote_type_id)"
            //+ " VALUES (" + itemID.ToString();
            
            //+ ", " + UserInfo.UserID.ToString()
            //+ ", " + UserInfo.PartnerID.ToString()
            
            //+ ", 1"
            //+ ")";

            //int day_today = DateTime.Today.Day;
            //SqlConnection conVote = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
            //SqlCommand cmdVote = new SqlCommand(sql, conVote);
            //conVote.Open();

            //cmdVote.CommandText = sql;
            //cmdVote.ExecuteNonQuery();
            //conVote.Close();

            DataTable dt = new DataTable();
            dt = SerializeDataTable();

            string SelItems = ItemPkgArray.Value;


            //DataTable dtValues = (DataTable)JsonConvert.DeserializeObject(SelItems, (typeof(DataTable)));

            if (dt.Rows.Count > 0)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerts", "javascript:alert('DataTable Has Rows')", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerts", "javascript:alert('" + dt.Rows[0].ItemArray[0] + "')", true);
            }


            string ActionValue = valButtonClicked.Value;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerts", "javascript:alert('" + SelItems + "')", true);

            // Compiler needs initialization (999). Need escape.

            int Vote = 999;
            string itemID;

            switch (ActionValue)
            {
                case "Reject":
                    Vote = 0;
                    break;
                case "Accept":
                    Vote = 1;
                    break;
                case "Remove":
                    Vote = 3;
                    break;
                case "Resubmit":
                    Vote = 4;
                    break;
                case "Withdraw":
                    Vote = 5;
                    break;
            }

            // *************************************************Connect to Database*************************************************
            
            SqlCommand cmd;
            string sql;
            SqlDataReader reader;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
            con.Open();

            // **********************************************Loop Through DataTable*************************************************

            for (int itemNum = 0; itemNum < dt.Rows.Count; itemNum++) 
            {
                itemID = dt.Rows[itemNum].ItemArray[0].ToString();

                sql = "SELECT * FROM TRI_Votes WHERE i_Item_ID = " + itemID.ToString() + " AND i_Partner_ID = " + UserInfo.PartnerID.ToString();
                cmd = new SqlCommand(sql, con);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    // Update Vote
                    sql = "UPDATE TRI_Votes"
                    + " SET i_User_ID = " + UserInfo.UserID.ToString()
                    + ", i_vote_type_id = " + Vote
                    + " WHERE i_Item_ID = " + itemID.ToString()
                    + " AND i_Partner_ID = " + UserInfo.PartnerID.ToString();
                    reader.Close();
                    cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    // Insert Vote
                    sql = "INSERT INTO TRI_Votes (i_Item_ID, i_User_ID, i_Partner_ID, i_Vote_type_id)"
                        + " VALUES (" + itemID.ToString()
                        + ", " + UserInfo.UserID.ToString()
                        + ", " + UserInfo.PartnerID.ToString()
                        + ", " + Vote + ")";
                    cmd = new SqlCommand(sql, con);
                    reader.Close();
                    cmd.ExecuteNonQuery();
                }
                // Update Approved Status "WHERE" Five Accept Votes
                sql = "UPDATE TRI_Items"
                + " SET i_Status = 1,"
                + " dt_Accepted = getdate()"
                + " WHERE i_Item_ID = " + itemID.ToString()
                + " AND EXISTS (SELECT i_Item_id, COUNT(*) FROM TRI_Votes tv Join TRI_Partners tp on tv.i_Partner_ID = tp.i_Partner_ID WHERE tv.i_vote_type_id = 1 AND tp.f_Can_Vote = 1 AND i_Item_ID = TRI_Items.i_Item_ID GROUP BY i_Item_ID HAVING COUNT(*) = 5)";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                // Update Release Date "WHERE" None
                sql = "UPDATE TRI_Items"
                + " SET dt_Released = getdate(),"
                + " f_Released = 1"
                + " WHERE i_Item_ID = " + itemID.ToString()
                + " AND f_Released = 0";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                // Update Status on Reject Vote
                //sql = "UPDATE TRI_Items"
                //+ " SET i_Status = CASE i_Status WHEN 2 THEN 0,"
                //+ " dt_Rejected = getdate()"
                //+ " WHERE i_Item_ID = " + itemID.ToString()
                //+ " AND EXISTS (SELECT i_Item_id FROM TRI_Votes tv Join TRI_Partners tp on tv.i_Partner_ID = tp.i_Partner_ID  join tri_vote_types t on tv.i_vote_type_id = t.i_vote_type_id WHERE t.i_Primary_Vote_Type in (0)  AND tp.f_Can_Vote = 1 AND i_Item_ID = TRI_Items.i_Item_ID)"
                
                //// Keep Item in Inbox Until Full Partner Vote Complement
                //+ " AND (SELECT COUNT(*) from TRI_Votes v JOIN TRI_Partners p on v.i_Partner_id = p.i_Partner_ID where v.i_Item_ID = TRI_Items.i_Item_ID and p.f_Can_Vote = 1) >= (SELECT COUNT(*) from TRI_Partners where f_Can_Vote = 1)";
                //cmd = new SqlCommand(sql, con);
                //cmd.ExecuteNonQuery();

                // Update Approved Status "WHERE" Five Votes With 1+ Reject Votes
                //sql = "UPDATE TRI_Items"
                //+ " SET i_Status = 0,"
                //+ " dt_Rejected = getdate()"
                //+ " WHERE i_Item_ID = " + itemID.ToString()
                //+ " AND i_Status = 2"
                //+ " AND EXISTS (SELECT i_Item_id, COUNT(*) FROM TRI_Votes tv Join TRI_Partners tp on tv.i_Partner_ID = tp.i_Partner_ID WHERE tv.i_vote_type_id IN (0,1) AND tp.f_Can_Vote = 1 AND i_Item_ID = TRI_Items.i_Item_ID GROUP BY i_Item_ID HAVING COUNT(*) = 5)";
                //cmd = new SqlCommand(sql, con);
                //cmd.ExecuteNonQuery();

                // Update Approved Status "WHERE" Five Votes With 1+ Reject Votes
                sql = "UPDATE TRI_Items"
                + " SET i_Status = 0,"
                + " dt_Rejected = getdate()"
                + " WHERE i_Item_ID = " + itemID.ToString()
                + " AND i_Status = 2"
                + " AND EXISTS (SELECT i_Item_id, COUNT(*) FROM TRI_Votes tv Join TRI_Partners tp on tv.i_Partner_ID = tp.i_Partner_ID WHERE tv.i_vote_type_id IN (0,1) AND tp.f_Can_Vote = 1 AND i_Item_ID = TRI_Items.i_Item_ID GROUP BY i_Item_ID HAVING COUNT(*) = 5)";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();



                // Update Removed Status
                if (Vote == 3)
                {
                    sql = "UPDATE TRI_Items"
                    + " SET i_Status = 3,"
                    + " dt_Removed = getdate()"
                    + " WHERE i_Item_ID = " + itemID.ToString()
                    + " and exists (select * from tri_partners where f_can_vote = 1 and i_Partner_ID = " + UserInfo.PartnerID.ToString() + ")";
                    cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                }
            } 
            // *************************************************End of Loop****************************************************
            con.Close();
        }
          // *************************************************End of Function****************************************************

        public String clID
        {
            get
            {
                return ItemPkgArray.ClientID;
            }
        }

        public String lblID
        {
            get
            {
                return lblUserName.Text;
            }
        }

        public String ParsedArrayString
        {
            get
            {
                return ItemPkgArray.Value;
            }
        }
        //[WebMethod]
        //public static string RegisterUser(string email, string password, string arraystring)
        //{
        //    string result = "Congratulations!!! your account has been created.";
        //    if (email.Length == 0)//Zero length check
        //    {
        //        result = "Email Address cannot be blank";
        //    }
        //    else if (!email.Contains(".") || !email.Contains("@")) //some other basic checks
        //    {
        //        result = "Not a valid email address";
        //    }
        //    else if (!email.Contains(".") || !email.Contains("@")) //some other basic checks
        //    {
        //        result = "Not a valid email address";
        //    }

        //    else if (password.Length == 0)
        //    {
        //        result = "Password cannot be blank";
        //    }
        //    else if (password.Length < 5)
        //    {
        //        result = "Password canonot be less than 5 chars";
        //    }

        //    return result;
        //}
    }
}