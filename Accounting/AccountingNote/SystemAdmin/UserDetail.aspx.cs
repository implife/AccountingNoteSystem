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
    public partial class UserDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManager.IsLogined())
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            UserInfoModel currentUser = AuthManager.GetCurrentUser();

            // 資料庫中沒有該使用者資料，可能被管理者砍帳號
            if (currentUser == null)
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;
            }

            // 判斷是否為post back
            if (!this.IsPostBack)
            {
                this.Session["CreateUserInfo"] = null;

                // 判斷是新增模式還是修改模式
                if (this.Request.QueryString["UID"] == null) // 新增模式
                {
                    // 新增模式的話沒有delete和password的按鈕，並可輸入帳號、姓名、Email、會員等級
                    this.btnDelete.Visible = false;
                    this.btnPassword.Visible = false;

                    this.lblAccount.Visible = false;
                    this.txtAccount.Visible = true;
                    this.lblUserLevel.Visible = false;
                    this.ddlUserLevel.Visible = true;

                    this.btnSave.Text = "下一步";
                    this.ltlTitle.Text = "會員管理 - 新增會員";
                }
                else // 修改模式
                {
                    // 修改模式的話有delete和變更密碼的按鈕，可變更姓名、Email，並顯示建立時間
                    this.btnDelete.Visible = true;
                    this.btnPassword.Visible = true;

                    this.lblAccount.Visible = true;
                    this.txtAccount.Visible = false;
                    this.lblUserLevel.Visible = true;
                    this.ddlUserLevel.Visible = false;

                    this.btnSave.Text = "儲存";
                    this.ltlTitle.Text = "會員管理 - 修改會員資料";

                    string userID = this.Request.QueryString["UID"];

                    if(string.Compare(userID, currentUser.ID) == 0)
                    {
                        this.lblAccount.Text = currentUser.Account;
                        this.txtName.Text = currentUser.Name;
                        this.txtEmail.Text = currentUser.Email;
                        this.lblUserLevel.Text = (currentUser.UserLevel == 0) ? "管理員" : "一般會員";
                        this.lblCreateDate.Text = currentUser.CreateDate;
                    }
                    else
                    {
                        this.ltlMsg.Text = "UID is not correct.";
                        this.btnDelete.Visible = false;
                        this.btnPassword.Visible = false;

                        this.lblAccount.Visible = true;
                        this.txtAccount.Visible = false;
                        this.lblUserLevel.Visible = true;
                        this.ddlUserLevel.Visible = false;

                        this.btnSave.Text = "儲存";
                    }
                }
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
            if (currentUser == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }


            // 取得使用者輸入的值
            string name = this.txtName.Text;
            string email = this.txtEmail.Text;

            if (this.txtAccount.Visible) // 新增模式
            {
                string account = this.txtAccount.Text;
                int userLevel = Convert.ToInt32(this.ddlUserLevel.SelectedValue);

                UserInfoModel model = new UserInfoModel();
                model.Account = account;
                model.Name = name;
                model.Email = email;
                model.UserLevel = userLevel;

                this.Session["CreateUserInfo"] = model;
                Response.Redirect("UserPassword.aspx?UID=" + currentUser.ID);
            }
            else // 修改模式
            {
                UserInfoManager.UpdateUserInfo(currentUser.ID, currentUser.Account, name, email);
                Response.Redirect("UserList.aspx");
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void btnPassword_Click(object sender, EventArgs e)
        {
            UserInfoModel currentUser = AuthManager.GetCurrentUser();
            Response.Redirect("UserPassword.aspx?UID=" + currentUser.ID);
        }


        private bool CheckInput(out List<string> errMsgList)
        {
            List<string> msgList = new List<string>();
            if (this.txtAccount.Visible)
            {
                if (string.IsNullOrWhiteSpace(this.txtAccount.Text))
                    msgList.Add("帳號不可為空");
            }

            if (string.IsNullOrWhiteSpace(this.txtName.Text))
                msgList.Add("姓名不可為空");

            if (string.IsNullOrWhiteSpace(this.txtEmail.Text))
                msgList.Add("Email不可為空");

            if (this.ddlUserLevel.Visible)
            {
                if (this.ddlUserLevel.SelectedValue != "0"
                && this.ddlUserLevel.SelectedValue != "1")
                {
                    msgList.Add("UserLevel must be 0 or 1.");
                }
            }

            errMsgList = msgList;

            if (errMsgList.Count == 0)
                return true;
            else
                return false;
            
        }

        // 儲存的button click事件
        // 檢查輸入值是否正確(可建立方法)
        // 正確的話取得輸入值

        // 判斷是新增模式還是修改模式
        // 新增模式的話呼叫CreateUserInfo(在UserInfoManager新建方法)
        // 修改模式的話呼叫UpdateUserInfo(在UserInfoManager新建方法)


        // 刪除的button click事件
        // 呼叫DeleteUserInfo(在UserInfoManager新建方法)



        // 修改密碼button click事件，跳頁至UserPassword.aspx
    }
}