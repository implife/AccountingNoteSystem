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
    class DBHelper
    {
        public static string GetConnectionString()
        {
            // 在WebForm中的Web.config裡新增XML格式的<connectionStrings>
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }

        /// <summary>
        /// 傳入連線字串、SQL指令字串、參數的List，取得從資料庫撈到的DataTable
        /// </summary>
        /// <param name="connStr">連線字串</param>
        /// <param name="dbCommand">SQL指令字串</param>
        /// <param name="list">SQL指令字串中參數化查詢的參數，List裡的元素是SqlParameter</param>
        /// <returns>回傳DataTable，沒讀到資料就回傳null</returns>
        public static DataTable ReadDataTable(string connStr, string dbCommand, List<SqlParameter> list)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddRange(list.ToArray()); // AddRange方法裡必須放陣列，所以將list轉成陣列
                    try
                    {
                        conn.Open();
                        var reader = comm.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        reader.Close();

                        return dt;
                    }
                    catch (Exception ex)
                    {
                        // 有出現例外呼叫WriteLog寫進Log檔裡
                        Logger.WriteLog(ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// 傳入連線字串、SQL指令字串、參數的List，取得從資料庫撈到的DataTable中的第一列資料(如GetAccounting理論上只會取得一筆資料)
        /// </summary>
        /// <param name="connStr">連線字串</param>
        /// <param name="dbCommand">SQL指令字串</param>
        /// <param name="list">SQL指令字串中參數化查詢的參數，List裡的元素是SqlParameter</param>
        /// <returns>回傳DataRow，沒讀到資料就回傳null</returns>
        public static DataRow ReadDataRow(string connStr, string dbCommand, List<SqlParameter> list)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddRange(list.ToArray());
                    try
                    {
                        conn.Open();
                        var reader = comm.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        reader.Close();

                        // 如果沒有讀到任何資料就回傳null
                        if (dt.Rows.Count == 0)
                            return null;
                        return dt.Rows[0];
                    }
                    catch (Exception ex)
                    {
                        // 有出現例外呼叫WriteLog寫進Log檔裡
                        Logger.WriteLog(ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// 當SQL指令字串是如同Update、Delete、Insert Into這種變更資料類型的指令
        /// </summary>
        /// <param name="connStr">連線字串</param>
        /// <param name="dbCommand">SQL指令字串</param>
        /// <param name="list">SQL指令字串中參數化查詢的參數，List裡的元素是SqlParameter</param>
        /// <returns>回傳更動的資料筆數</returns>
        public static int ModifyData(string connStr, string dbCommand, List<SqlParameter> list)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddRange(list.ToArray());

                    conn.Open();
                    int effectRowsCount = comm.ExecuteNonQuery();
                    return effectRowsCount;
                }
            }
        }
    }
}
