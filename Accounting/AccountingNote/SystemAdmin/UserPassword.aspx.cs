using AccountingNote.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class UserPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManager.IsLogined())
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            UserInfoModel currentUser = AuthManager.GetCurrentUser();
            string account = Request.QueryString["UID"];

            // 資料庫中沒有該使用者資料，可能被管理者砍帳號
            if (currentUser == null)
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;
            }

            // 確認UID是否正確
            if (string.Compare(account, currentUser.ID) != 0)
            {
                this.ltlMsg.Text = "UID is not correct.";
                return;
            }

            // 判斷是變更密碼還是新增使用者
            if (this.Session["CreateUserInfo"] == null) // 變更密碼模式
            {
                this.lblAccount.Text = currentUser.Account;

                this.plcOriginalPWD.Visible = true;
                this.btnSave.Text = "變更";
                this.ltlTitle.Text = "會員管理 - 變更密碼";

            }
            else // 新增會員模式
            {
                UserInfoModel model = this.Session["CreateUserInfo"] as UserInfoModel;
                this.lblAccount.Text = model.Account;

                this.plcOriginalPWD.Visible = false;
                this.btnSave.Text = "建立";
                this.ltlTitle.Text = "會員管理 - 新增會員";
            }

            // 顯示使用者帳號
        }

        // 變更的button click事件
        // 判斷輸入值是否正確，新密碼跟確認新密碼是否一樣
        // 輸入值正確的話就呼叫UpdateUserInfo(在UserInfoManager新建方法)


        // 取消的button click事件，跳頁至UserDetail.aspx
    }
}