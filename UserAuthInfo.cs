using System;
using System.Data.SqlClient;
using System.Configuration;

namespace TMID
{
	/// <summary>
	/// Summary description for UserAuthenticationInfo.
	/// </summary>
	public class UserAuthInfo
	{
		private string uUsername;
        private string uPartnerName;
		private int iUserID;
		private int iPartnerID;

		public UserAuthInfo(char[] token)
		{
			System.Web.HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies.Get(System.Web.Security.FormsAuthentication.FormsCookieName);
			System.Web.Security.FormsAuthenticationTicket authTicket = System.Web.Security.FormsAuthentication.Decrypt(authCookie.Value);
			ParseUserData(authTicket.UserData, token);
		}

		private void ParseUserData(string UserData, char[] token)
		{
			string[] authCookieSplitData = UserData.Split(token);

			uUsername = authCookieSplitData[0];
			iUserID = Int32.Parse(authCookieSplitData[1]);
			iPartnerID = Int32.Parse(authCookieSplitData[2]);

            //SqlConnection con = new SqlConnection((string)ConfigurationSettings.AppSettings["sqlConnectionString"]);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
            con.Open();
            string sql = "exec Get_Partner_Name_byID " + iPartnerID.ToString();

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
			reader.Read();

            uPartnerName = reader.GetString(0);

		}

		// TODO: add properties to get the 3 different user data pieces (username, userid, partnerid)
		public string Username
		{
			get
			{
				return uUsername;
			}
		}

        public string PartnerName
        {
            get
            {
                return uPartnerName;
            }
        }

		public int UserID
		{
			get
			{
				return iUserID;
			}
		}

		public int PartnerID
		{
			get
			{
				return iPartnerID;
			}
		}
	}
}
