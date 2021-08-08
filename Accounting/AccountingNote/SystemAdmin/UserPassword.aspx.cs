using AccountingNote.Auth;
using AccountingNote.DBSource;
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
            string accountUID = Request.QueryString["UID"];

            // 資料庫中沒有該使用者資料，可能被管理者砍帳號
            if (currentUser == null)
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;
            }

            // 確認UID是否正確
            if (string.Compare(accountUID, currentUser.ID) != 0)
            {
                this.ltlMsg.Text = "UID is not correct.";
                return;
            }

            // 判斷是變更密碼還是新增使用者
            if (this.Session["CreateUserInfo"] == null) // 變更密碼模式
            {
                this.lblAccount.Text = currentUser.Account;

                this.ltlTitle.Text = "會員管理 - 變更密碼";
                this.plcOriginalPWD.Visible = true;
                this.lblNewPWD.Text = "新密碼";
                this.lblNewPWDAgain.Text = "確認新密碼";
                this.btnSave.Text = "變更";
            }
            else // 新增會員模式
            {
                UserInfoModel model = this.Session["CreateUserInfo"] as UserInfoModel;
                this.lblAccount.Text = model.Account;

                this.ltlTitle.Text = "會員管理 - 新增會員 - 設定密碼";
                this.plcOriginalPWD.Visible = false;
                this.lblNewPWD.Text = "密碼";
                this.lblNewPWDAgain.Text = "確認密碼";
                this.btnSave.Text = "建立";
            }

            // PostBack的話還是將輸入的密碼還原
            if (IsPostBack)
            {
                this.txtOriginPWD.Attributes.Add("value", this.txtOriginPWD.Text);
                this.txtNewPWD.Attributes.Add("value", this.txtNewPWD.Text);
                this.txtNewPWDAgain.Attributes.Add("value", this.txtNewPWDAgain.Text);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.ltlMsg.Text = string.Empty;

            // 檢查輸入值是否正確
            List<string> errMsgList = new List<string>();
            if (!CheckInput(out errMsgList))
            {
                this.ltlMsg.Text = string.Join("<br/>", errMsgList);
                return;
            }


            UserInfoModel currentUser = AuthManager.GetCurrentUser();
            if (this.Session["CreateUserInfo"] == null) // 變更密碼模式
            {
                UserInfoManager.UpdateUserPWD(currentUser.ID, currentUser.Account, this.txtNewPWD.Text);
                Response.Redirect("UserList.aspx");
            }
            else // 新增會員模式
            {
                UserInfoModel model = this.Session["CreateUserInfo"] as UserInfoModel;
                UserInfoManager.CreateUserInfo(model.Account, this.txtNewPWD.Text, 
                    model.Name, model.Email, model.UserLevel);
                Response.Redirect("UserList.aspx");
            }
        }

        private bool CheckInput(out List<string> errMsgList)
        {
            List<string> msgList = new List<string>();
            UserInfoModel currentUser = AuthManager.GetCurrentUser();

            if (this.Session["CreateUserInfo"] == null) // 變更密碼模式
            {
                if(string.Compare(this.txtOriginPWD.Text, currentUser.PWD) != 0)
                    msgList.Add("原密碼錯誤");
            }

            if (string.Compare(this.txtNewPWD.Text, this.txtNewPWDAgain.Text) != 0)
                msgList.Add("請再確認兩次新密碼是否正確");
            else if(string.IsNullOrEmpty(this.txtNewPWD.Text))
                msgList.Add("新密碼不可為空");

            errMsgList = msgList;
            if (errMsgList.Count == 0)
                return true;
            else
                return false;
        }

    }
}