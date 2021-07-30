using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.DBSource
{
    public class AccountingManager
    {
        public static string GetConnectionString()
        {
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }

        public static DataTable GetAccountingList(string id)
        {
            string connStr = GetConnectionString();
            string dbCommand = $@"
                SELECT ID, Caption, Amount, ActType, CreateDate
                FROM Accounting
                WHERE UserID = @userID
            ";

            using(SqlConnection conn = new SqlConnection(connStr))
            {
                using(SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddWithValue("@userID", id);
                    try
                    {
                        conn.Open();
                        var reader = comm.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        reader.Close();
                        return dt;
                    }
                    catch(Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="caption"></param>
        /// <param name="amount"></param>
        /// <param name="actType"></param>
        /// <param name="body"></param>
        public static void CreateAccounting(string UserID, string caption, int amount, int actType, string body)
        {
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1,000,000");

            if(actType != 0 && actType != 1)
                throw new ArgumentException("ActType must be 0 or 1.");

            string connectionString = GetConnectionString();
            string dbCommandString =
                @"INSERT INTO Accounting 
                    (UserID, Caption, Amount, ActType, createDate, Body)
                    VALUES 
                    (@id, @cap, @amount, @type, @createDate, @body);
                ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand(dbCommandString, conn))
                {
                    comm.Parameters.AddWithValue("@id", UserID);
                    comm.Parameters.AddWithValue("@cap", caption);
                    comm.Parameters.AddWithValue("@amount", amount);
                    comm.Parameters.AddWithValue("@type", actType);
                    comm.Parameters.AddWithValue("@createDate", DateTime.Now);
                    comm.Parameters.AddWithValue("@body", body);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                    }
                }
            }
        }

        public static bool UpdateAccounting(int id, string UserID, string caption, int amount, int actType, string body)
        {
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1,000,000");

            if (actType != 0 && actType != 1)
                throw new ArgumentException("ActType must be 0 or 1.");

            string connectionString = GetConnectionString();
            string dbCommandString =
                @"UPDATE Accounting 
                  SET
                    UserID          = @UserId
                    ,Caption        = @cap
                    ,Amount         = @amount
                    ,ActType        = @type
                    ,createDate     = @createDate
                    ,Body           = @body
                 WHERE
                    ID = @id
                ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand(dbCommandString, conn))
                {
                    comm.Parameters.AddWithValue("@UserId", UserID);
                    comm.Parameters.AddWithValue("@cap", caption);
                    comm.Parameters.AddWithValue("@amount", amount);
                    comm.Parameters.AddWithValue("@type", actType);
                    comm.Parameters.AddWithValue("@createDate", DateTime.Now);
                    comm.Parameters.AddWithValue("@body", body);
                    comm.Parameters.AddWithValue("@id", id);

                    try
                    {
                        conn.Open();
                        int affect = comm.ExecuteNonQuery();
                        if (affect == 0)
                            return false;
                        else
                            return true;
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return false;
                    }
                }
            }
        }

        public static DataRow GetAccounting(int id, string userID)
        {
            string connStr = GetConnectionString();
            string dbCommand = $@"
                SELECT ID, Caption, Amount, ActType, CreateDate, Body
                FROM Accounting
                WHERE ID = @id AND UserID = @userID
            ";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddWithValue("@id", id);
                    comm.Parameters.AddWithValue("@userID", userID);
                    try
                    {
                        conn.Open();
                        var reader = comm.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        reader.Close();

                        if (dt.Rows.Count == 0)
                            return null;

                        return dt.Rows[0];
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return null;
                    }
                }
            }
        }

        public static bool DeleteAccounting(int id)
        {
            string connStr = GetConnectionString();
            string dbCommand = $@"
                DELETE FROM Accounting
                WHERE ID = @id
            ";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddWithValue("@id", id);
                    try
                    {
                        conn.Open();
                        int affect = comm.ExecuteNonQuery();
                        if (affect == 0)
                            return false;
                        else
                            return true;
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return false;
                    }
                }
            }
        }

    }
}
