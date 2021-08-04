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
