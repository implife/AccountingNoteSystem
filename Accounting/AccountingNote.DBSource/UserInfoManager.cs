using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AccountingNote.DBSource
{
    
    public class UserInfoManager
    {
        /// <summary>
        /// 輸入使用者帳戶名稱取得該使用者的DataRow
        /// </summary>
        /// <param name="account"></param>
        /// <returns>找不到該用戶時回傳null</returns>
        public static DataRow GetUserInfoByAccount(string account)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                @"SELECT [ID], [Account], [PWD], [Name], [Email], [UserLevel], [CreateDate]
                    FROM UserInfo
                    WHERE [Account] = @account
                ";
            // 將參數查詢時需要的參數放進SqlParameter物件，再放進list裡面
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@account", account));


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
        /// 取得所有會員的資料
        /// </summary>
        /// <returns>所有會員的DataTable</returns>
        public static DataTable GetUserInfoList()
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                @"SELECT [ID], [Account], [PWD], [Name], [Email], [UserLevel], [CreateDate]
                    FROM UserInfo
                ";

            try
            {
                // 要回傳多筆資料，使用ReadDataTable
                return DBHelper.ReadDataTable(connStr, dbCommand, new List<SqlParameter>());
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 修改會員資料，只能修改名字和email
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="account"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool UpdateUserInfo(string userID, string account, string name, string email)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                @"UPDATE UserInfo 
                  SET
                    Name    = @Name
                    ,Email  = @Email
                 WHERE
                    ID = @userID AND Account = @Account
                ";

            // 將參數查詢時需要的參數放進SqlParameter物件，再放進list裡面
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@Name", name));
            list.Add(new SqlParameter("@Email", email));
            list.Add(new SqlParameter("@UserID", userID));
            list.Add(new SqlParameter("@Account", account));

            try
            {
                // 呼叫ModifyData，回傳更動筆數
                int effectRowsCount = DBHelper.ModifyData(connStr, dbCommand, list);

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
        /// 取得所有會員的數量
        /// </summary>
        /// <returns>所有會員的數量</returns>
        public static int GetUserQuantity()
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                @"SELECT [ID], [Account], [PWD], [Name], [Email], [UserLevel], [CreateDate]
                    FROM UserInfo
                ";
            try
            {
                DataTable dt = DBHelper.ReadDataTable(connStr, dbCommand, new List<SqlParameter>());

                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return 0;
            }
        }
    }
}
