using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
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
                if (!AuthManager.IsLogined())
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }

                UserInfoModel currentUser = AuthManager.GetCurrentUser();

                // 可能被管理者砍帳號
                if (currentUser == null)
                {
                    this.Session["UserLoginInfo"] = null;
                    Response.Redirect("/Login.aspx");
                    return;
                }

                this.ltlAccount.Text = currentUser.Account;
                this.ltlName.Text = currentUser.Name;
                this.ltlEmail.Text = currentUser.Email;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            this.Session["UserLoginInfo"] = null;
            Response.Redirect("/Login.aspx");
        }
    }
}