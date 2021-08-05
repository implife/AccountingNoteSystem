using AccountingNote.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 如果不是登入狀態就導回Login頁面
                if (!AuthManager.IsLogined())
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }

                UserInfoModel currentUser = AuthManager.GetCurrentUser();

                // 資料庫中沒有該使用者資料，可能被管理者砍帳號
                if (currentUser == null)
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }

                // 畫面上顯示使用者資訊
                this.ltlAccount.Text = currentUser.Account;
                this.ltlName.Text = currentUser.Name;
                this.ltlEmail.Text = currentUser.Email;
                this.ltlUserLevel.Text = (currentUser.UserLevel == 0) ? "管理者" : "一般會員";
                this.ltlCreateDate.Text = currentUser.CreateDate;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            AuthManager.Logout();
            Response.Redirect("/Login.aspx");

        }
    }
}