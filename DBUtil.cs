using System;
using System.Data;
using System.Data.SqlClient;

namespace TMID
{
	/// <summary>
	/// Summary description for DBUtil.
	/// </summary>
	public class DBUtil
	{

		private static string connString;
		//private static SqlConnection conn;

		public DBUtil(string connectionString)
		{
			//
			// TODO: Add constructor logic here
			//

			if(connString == null || connString == "")
				connString = connectionString;
		}

		public DBUtil()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/*public static SqlConnection GetConnection
		{
			if(connString == null || connString == "")
				return null;
			
			if(conn == null)
				conn = new SqlConnection();
		}*/
	
	}
}
