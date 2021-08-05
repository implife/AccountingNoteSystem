using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AccountingNote.Auth
{
    public class AuthManager
    {
        /// <summary>
        /// 檢查Session中"UserLoginInfo"是否存在，存在表示是有人登入的狀態
        /// </summary>
        /// <returns>布林值，有登入狀態回傳true，反之回傳false</returns>
        public static bool IsLogined()
        {
            if (HttpContext.Current.Session["UserLoginInfo"] == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 取得已登入的使用者資訊
        /// </summary>
        /// <returns>登入的使用者的UserInfoModel物件，沒有人登入回傳null</returns>
        public static UserInfoModel GetCurrentUser()
        {
            string account = HttpContext.Current.Session["UserLoginInfo"] as string;

            // account為null表示沒有人登入，回傳null
            if (account == null)
                return null;

            DataRow dr = UserInfoManager.GetUserInfoByAccount(account);

            // Session裡有使用者名稱但資料庫裡找不到，把Session清空並回傳null
            if (dr == null)
            {
                HttpContext.Current.Session["UserLoginInfo"] = null;
                return null;
            }

            // 建立USerInfoModel物件並回傳
            UserInfoModel model = new UserInfoModel();
            model.ID = dr["ID"].ToString();
            model.Account = dr["Account"].ToString();
            model.PWD = dr["PWD"].ToString();
            model.Name = dr["Name"].ToString();
            model.Email = dr["Email"].ToString();
            model.UserLevel = int.Parse(dr["UserLevel"].ToString());
            model.CreateDate = dr["CreateDate"].ToString();

            return model;
        }

        /// <summary>
        /// 登出。將Session中的"UserLoginInfo"清空
        /// </summary>
        public static void Logout()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null;
        }

        /// <summary>
        /// 登入功能。若帳號密碼都是對的就將Session中"UserLoginInfo"設為使用者名稱
        /// </summary>
        /// <param name="account">使用者名稱</param>
        /// <param name="pwd">密碼</param>
        /// <param name="errmsg">字串傳值呼叫，傳回錯誤訊息</param>
        /// <returns>布林值，登入成功回傳true，失敗回傳false</returns>
        public static bool TryLogin(string account, string pwd, out string errmsg)
        {
            // 檢查帳號密碼是否空白
            if(string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errmsg = "Account / Password is required";
                return false;
            }

            DataRow dr = UserInfoManager.GetUserInfoByAccount(account);

            // 檢查資料庫裡是否有該使用者的資料
            if(dr == null)
            {
                errmsg = $"Account:{account} doesn't exist.";
                return false;
            }

            // 比對從資料庫裡取得的密碼，若正確，errmsg設為空字串並回傳true
            if(string.Compare(dr["PWD"].ToString(), pwd) == 0)
            {
                HttpContext.Current.Session["UserLoginInfo"] = dr["Account"].ToString();

                errmsg = string.Empty;
                return true;
            }
            else
            {
                errmsg = "Login failed, please check Account / password.";
                return false;
            }
            
        }
    }
}
