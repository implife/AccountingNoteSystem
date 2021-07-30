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
        public static DataTable GetAccountingList(string id)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand = $@"
                SELECT ID, Caption, Amount, ActType, CreateDate
                FROM Accounting
                WHERE UserID = @userID
            ";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userID", id));

            try
            {
                return DBHelper.ReadDataTable(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
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

            if (actType != 0 && actType != 1)
                throw new ArgumentException("ActType must be 0 or 1.");

            string connectionString = DBHelper.GetConnectionString();
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

            string connectionString = DBHelper.GetConnectionString();
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

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@UserId", UserID));
            list.Add(new SqlParameter("@cap", caption));
            list.Add(new SqlParameter("@amount", amount));
            list.Add(new SqlParameter("@type", actType));
            list.Add(new SqlParameter("@createDate", DateTime.Now));
            list.Add(new SqlParameter("@body", body));
            list.Add(new SqlParameter("@id", id));

            try
            {
                int effectRowsCount = DBHelper.ModifyData(connectionString, dbCommandString, list);
                if (effectRowsCount == 0)
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

        public static DataRow GetAccounting(int id, string userID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand = $@"
                SELECT ID, Caption, Amount, ActType, CreateDate, Body
                FROM Accounting
                WHERE ID = @id AND UserID = @userID
            ";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@id", id));
            list.Add(new SqlParameter("@userID", userID));


            try
            {
                return DBHelper.ReadDataRow(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        public static bool DeleteAccounting(int id)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand = $@"
                DELETE FROM Accounting
                WHERE ID = @id
            ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@id", id));

            try
            {
                int effectRowsCount = DBHelper.ModifyData(connStr, dbCommand, list);
                if (effectRowsCount == 0)
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
