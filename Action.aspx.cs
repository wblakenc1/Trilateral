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

    public partial class Action : System.Web.UI.Page
    {
        private UserAuthInfo UserInfo;
        SqlConnection con;

    public class DataArray
    {
        public int grID { get; set; }
        public string grItem { get; set; }
        public int grClass { get; set; }
        public string grStatus { get; set; }
        public string grComment { get; set; }
        public string grTranslation { get; set; } 
    }

public DataTable SerializeDataTable()
{
    string t = PreviousPage.ParsedArrayString;
    var table = JsonConvert.DeserializeObject<DataTable>(t);
    return table;
}
        protected void Page_Load(object sender, System.EventArgs e)
        {

                lblArray.Text = PreviousPage.ParsedArrayString;
                Button btnacceptButton = (Button)Page.PreviousPage.FindControl("BtnAccept");
                HiddenField btnClickedVal = (HiddenField)Page.PreviousPage.FindControl("valButtonClicked");
                lblMessage.Text = "Click Submit to " + btnClickedVal.Value;
            

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
            con.Open();

            string[,] arr2D = { 
                    { "55509", "Toilet cisterns", "21", "Pending", "Comment", "Translation" }, 
                    { "55523", "Transmission oils", "33", "Pending", "Comment", "Translation" }, 
                    { "55529", "Fresh currants", "15", "Pending", "Comment", "Translation" }, 
                    { "55510", "Grated potato nuggets", "29", "Pending", "Comment", "Translation" }, 
                    { "55539", "Fresh pine mushrooms", "31", "Pending", "Comment", "Translation" }
                 };

            ArrayList arrList = new ArrayList();

            for (int i = 0; i < 5; i++)
            {
                //arrList.Add(new ListItem(arr2D[i, 0], arr2D[i, 1]));
            }

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

            Grid2D.DataSource = SerializeDataTable();
            Grid2D.DataBind();            

            if (!IsPostBack)
            {

                if (Page.PreviousPage != null)
                {
                    lblArray.Text = PreviousPage.ParsedArrayString;
                }
            }
        }
        protected void Grid2D_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
        }
        protected void Grid2D_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            Response.Write(row.Cells[0].Text);
            Response.Write(row.Cells[1].Text);
        }
        //protected void GetSelectedRecords(object sender, EventArgs e)
        //{
        //    //DataTable dt = new DataTable();
        //    //dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Name"), new DataColumn("Country") });
        //    foreach (GridViewRow row in Grid2D.Rows)
        //    {
        //        if (row.RowType == DataControlRowType.DataRow)
        //        {
        //            CheckBox chkRow = (row.Cells[0].FindControl("SelectCheckBox") as CheckBox);
        //            if (chkRow.Checked)
        //            {
        //                string celID = row.Cells[0].Text;
        //                string celName = row.Cells[1].Text;
        //                string celClass = row.Cells[2].Text;
        //                string celComment = (row.Cells[4].FindControl("txtComment") as TextBox).Text;
        //                string celTranslation = (row.Cells[5].FindControl("txtTranslation") as TextBox).Text;

        //                //dt.Rows.Add(name, country);
        //            }
        //        }
        //    }
        //    //lblSelected.Text = celID;
        //    string message = "Hello! Mudassar.";
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    sb.Append("<script type = 'text/javascript'>");
        //    sb.Append("window.onload=function(){");
        //    sb.Append("alert('");
        //    sb.Append(message);
        //    sb.Append("')};");
        //    sb.Append("</script>");
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        //    //gvSelected.DataSource = dt;
        //    //gvSelected.DataBind();
        //}
    }
}
