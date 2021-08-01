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
        /// <summary>
        /// 傳入使用者的UserID(一長串GUID)，從AccountingNote的資料表取得該使用者所建立的所有帳目
        /// </summary>
        /// <param name="id">使用者的UserID(一長串GUID)</param>
        /// <returns>回傳該使用者所建立的所有帳目的DataTable</returns>
        public static DataTable GetAccountingList(string id)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand = $@"
                SELECT ID, Caption, Amount, ActType, CreateDate
                FROM Accounting
                WHERE UserID = @userID
            ";

            // 將參數查詢時需要的參數放進SqlParameter物件，再放進list裡面
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userID", id));

            try
            {
                // 要回傳多筆資料，使用ReadDataTable
                return DBHelper.ReadDataTable(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 新增一筆流水帳
        /// </summary>
        /// <param name="userID">使用者的UserID(一長串GUID)</param>
        /// <param name="caption">標題</param>
        /// <param name="amount">金額</param>
        /// <param name="actType">支出或收入，支出是0，收入是1</param>
        /// <param name="body">帳目說明</param>
        public static void CreateAccounting(string userID, string caption, int amount, int actType, string body)
        {
            // 檢查輸入的金額，限制在0~1,000,000
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1,000,000");

            // 行為必須只能是支出或收入，只能是0或1
            if (actType != 0 && actType != 1)
                throw new ArgumentException("ActType must be 0 or 1.");

            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                @"INSERT INTO Accounting 
                    (UserID, Caption, Amount, ActType, createDate, Body)
                    VALUES 
                    (@id, @cap, @amount, @type, @createDate, @body);
                ";

            // 將參數查詢時需要的參數放進SqlParameter物件，再放進list裡面
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@id", userID));
            list.Add(new SqlParameter("@cap", caption));
            list.Add(new SqlParameter("@amount", amount));
            list.Add(new SqlParameter("@type", actType));
            list.Add(new SqlParameter("@createDate", DateTime.Now));
            list.Add(new SqlParameter("@body", body));

            try
            {
                DBHelper.ModifyData(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }

        /// <summary>
        /// 變更已存在的帳目，需要該帳目的id以及使用者的UserID，目的是不讓不同使用者利用帳目的id互相存取
        /// </summary>
        /// <param name="id">帳目的id(自料庫中自動增值的流水號)</param>
        /// <param name="serID">使用者的UserID(一長串GUID)</param>
        /// <param name="caption">標題</param>
        /// <param name="amount">金額</param>
        /// <param name="actType">支出或收入，支出是0，收入是1</param>
        /// <param name="body">帳目說明</param>
        /// <returns>布林值，，變更成功回傳true，不成功false</returns>
        public static bool UpdateAccounting(int id, string userID, string caption, int amount, int actType, string body)
        {
            // 檢查輸入的金額，限制在0~1,000,000
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1,000,000");

            // 行為必須只能是支出或收入，只能是0或1
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

            // 將參數查詢時需要的參數放進SqlParameter物件，再放進list裡面
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@UserId", userID));
            list.Add(new SqlParameter("@cap", caption));
            list.Add(new SqlParameter("@amount", amount));
            list.Add(new SqlParameter("@type", actType));
            list.Add(new SqlParameter("@createDate", DateTime.Now));
            list.Add(new SqlParameter("@body", body));
            list.Add(new SqlParameter("@id", id));

            try
            {
                // 呼叫ModifyData，回傳更動筆數
                int effectRowsCount = DBHelper.ModifyData(connectionString, dbCommandString, list);

                // 如果更動筆數為0，表示不成功，回傳false
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

        /// <summary>
        /// 取得使用者所建立的其中一筆資料，需要該帳目的id以及使用者的UserID，不讓不同使用者利用帳目的id互相存取
        /// </summary>
        /// <param name="id">帳目的id(自料庫中自動增值的流水號)</param>
        /// <param name="userID">使用者的UserID(一長串GUID)</param>
        /// <returns>回傳該帳目的DataRow</returns>
        public static DataRow GetAccounting(int id, string userID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand = $@"
                SELECT ID, Caption, Amount, ActType, CreateDate, Body
                FROM Accounting
                WHERE ID = @id AND UserID = @userID
            ";

            // 將參數查詢時需要的參數放進SqlParameter物件，再放進list裡面
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

        /// <summary>
        /// 刪除一筆流水帳
        /// </summary>
        /// <param name="id">帳目的id(自料庫中自動增值的流水號)</param>
        /// <returns>布林值，刪除成功回傳true，不成功回傳false</returns>
        public static bool DeleteAccounting(int id)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand = $@"
                DELETE FROM Accounting
                WHERE ID = @id
            ";

            // 將參數查詢時需要的參數放進SqlParameter物件，再放進list裡面
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@id", id));

            try
            {
                // 呼叫ModifyData，回傳更動筆數
                int effectRowsCount = DBHelper.ModifyData(connStr, dbCommand, list);

                // 若更動筆數為0表示刪除不成功，回傳false
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
