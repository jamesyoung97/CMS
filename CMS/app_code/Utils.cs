using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CMS.app_code
{
	public static class Utils
	{
		/// <summary>
		/// The current SQL connection
		/// </summary>
		private static SqlConnection connection;

		/// <summary>
		/// Gets an Integer from the DB
		/// </summary>
		/// <param name="query">SQL Query</param>
		/// <returns>Integer</returns>
		public static int iGetSingleIntFromDB(String query)
		{
			openConnection();

			SqlDataReader reader = null;
			SqlCommand command = new SqlCommand(query, connection);

			try
			{
				reader = command.ExecuteReader();
				reader.Read();
			} 
			catch (SqlException ex) 
			{
				Console.WriteLine("SQL Exception: Error executing return query: " + ex.ToString());
			}

			closeConnection();
			return reader.GetInt32(0);
		}

		/// <summary>
		/// Gets list of integers from the DB
		/// </summary>
		/// <param name="query">SQL query</param>
		/// <returns>List<int></returns>
		public static List<int> iGetListIntFromDB(String query) 
		{
			List<int> list = new List<int>();

			openConnection();

			SqlDataReader reader = null;
			SqlCommand command = new SqlCommand(query, connection);

			try
			{
				reader = command.ExecuteReader();
				int count = 0;
				while(reader.Read()) 
				{
					list.Add(reader.GetInt32(count));
					count++;
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine("SQL Exception: Error executing return query: " + ex.ToString());
			}

			closeConnection();
			return list;
		}

		/// <summary>
		/// Get a string from DB
		/// </summary>
		/// <param name="query">SQL Query</param>
		/// <returns>String</returns>
		public static String sGetSingleStringFromDB(String query)
		{
			openConnection();

			SqlDataReader reader = null;
			SqlCommand command = new SqlCommand(query, connection);

			try
			{
				reader = command.ExecuteReader();
				reader.Read();
			}
			catch (SqlException ex)
			{
				Console.WriteLine("SQL Exception: Error executing return query: " + ex.ToString());
			}

			closeConnection();
			return reader.ToString();
		}

		/// <summary>
		/// Gets list of strings from the DB
		/// </summary>
		/// <param name="query">SQL query</param>
		/// <returns>List<String></returns>
		public static List<String> sGetListStringFromDB(String query)
		{
			List<String> list = new List<String>();

			openConnection();

			SqlDataReader reader = null;
			SqlCommand command = new SqlCommand(query, connection);

			try
			{
				reader = command.ExecuteReader();
				int count = 0;
				while (reader.Read())
				{
					list.Add(reader.ToString());
					count++;
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine("SQL Exception: Error executing return query: " + ex.ToString());
			}

			closeConnection();
			return list;
		}

		/// <summary>
		/// Pulls a datatable from the DB
		/// </summary>
		/// <param name="query">SQL Query</param>
		/// <returns>DataTable</returns>
		public static DataTable getPullDataFromDB(String query)
		{
			DataTable table = new DataTable();
			openConnection();

			SqlCommand command = new SqlCommand(query, connection);
			SqlDataAdapter da = new SqlDataAdapter(command);

			try
			{
				da.Fill(table);
			}
			catch (SqlException ex)
			{
				Console.WriteLine("SQL Exception: Error filling data table: " + ex.ToString());
			}

			da.Dispose();
			closeConnection();
			return table;
		}

		/// <summary>
		/// Executes an SQL query
		/// </summary>
		/// <param name="query">SQL Query</param>
		public static void executeQuery(String query)
		{
			openConnection();

			SqlCommand command = new SqlCommand(query, connection);
			command.ExecuteNonQuery();

			closeConnection();
		} 

		/// <summary>
		/// Opens DB connection
		/// </summary>
		private static void openConnection()
		{
			connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);

			try
			{
				connection.Open();
			}
			catch (SqlException ex)
			{
				Console.WriteLine("SQL Exception: Error opening connection: " + ex.ToString());
			}
		}

		/// <summary>
		/// Closes the DB connection
		/// </summary>
		private static void closeConnection()
		{
			try
			{
				connection.Close();
			} 
			catch (SqlException ex )
			{
				Console.WriteLine("SQL Exception: Error closing connection: " + ex.ToString());
			}
			
			connection = null;
		}
	}
}