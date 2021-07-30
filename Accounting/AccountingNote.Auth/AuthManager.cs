﻿using AccountingNote.DBSource;
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
        public static bool IsLogined()
        {
            if (HttpContext.Current.Session["UserLoginInfo"] == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 取得已登入的使用者資訊，如果沒有登入就回傳Null
        /// </summary>
        /// <returns></returns>
        public static UserInfoModel GetCurrentUser()
        {
            string account = HttpContext.Current.Session["UserLoginInfo"] as string;

            if (account == null)
                return null;
            DataRow dr = UserInfoManager.GetUserInfoByAccount(account);

            if (dr == null)
            {
                HttpContext.Current.Session["UserLoginInfo"] = null;
                return null;
            }
                

            UserInfoModel model = new UserInfoModel();
            model.ID = dr["ID"].ToString();
            model.Account = dr["Account"].ToString();
            model.Name = dr["Name"].ToString();
            model.Email = dr["Email"].ToString();

            return model;
        }

        public static void Logout()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null;
        }

        public static bool TryLogin(string account, string pwd, out string errmsg)
        {
            if(string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errmsg = "Account / Password is required";
                return false;
            }

            DataRow dr = UserInfoManager.GetUserInfoByAccount(account);

            if(dr == null)
            {
                errmsg = $"Account:{account} doesn't exist.";
                return false;
            }

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
