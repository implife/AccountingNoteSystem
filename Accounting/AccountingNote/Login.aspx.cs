using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingNote.DBSource;

namespace AccountingNote
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(this.Session["UserLoginInfo"] != null)
            {
                this.plcLogin.Visible = false;
                Response.Redirect("/SystemAdmin/UserInfo.aspx");
            }
            else
            {
                this.plcLogin.Visible = true;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            

            string inp_account = txtAccount.Text;
            string inp_PWD = txtPwd.Text;

            if (string.IsNullOrWhiteSpace(inp_account) || string.IsNullOrWhiteSpace(inp_PWD))
            {
                this.ltlMsg.Text = "Account or Password is required.";
                return;
            }

            var dr = UserInfoManager.GetUserInfoByAccount(inp_account);
            
            if(dr != null && string.Compare(dr["PWD"].ToString(), inp_PWD) == 0)
            {
                this.Session["UserLoginInfo"] = dr["Account"];
                Response.Redirect("/SystemAdmin/UserInfo.aspx");
            }
            else
            {
                this.ltlMsg.Text = "Login failed. Please check your account or password.";
            }
        }
    }
}