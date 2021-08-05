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
            string accountUID = this.Request.QueryString["UID"];

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
                if (accountUID == null) // 新增模式
                {
                    // 新增模式的話沒有delete和password的按鈕，並可輸入帳號、姓名、Email、會員等級
                    this.btnSave.Text = "下一步";
                    this.btnDelete.Visible = false;
                    this.btnPassword.Visible = false;

                    this.lblAccount.Visible = false;
                    this.lblName.Visible = false;
                    this.lblEmail.Visible = false;
                    this.lblUserLevel.Visible = false;

                    this.txtAccount.Visible = true;
                    this.txtName.Visible = true;
                    this.txtEmail.Visible = true;
                    this.ddlUserLevel.Visible = true;

                    this.ltlTitle.Text = "會員管理 - 新增會員";
                }
                else // 有UID
                {
                    // UID和currentUser的ID不同
                    if (string.Compare(accountUID, currentUser.ID) != 0)
                    {
                        DataRow editUser = UserInfoManager.GetUserInfoByUserID(accountUID);
                        if (editUser == null)
                        {
                            ltlMsg.Text = "Wrong Edit UserUID.";
                            return;
                        }
                        else // 管理者進入別的會員的編輯頁面
                        {
                            
                            this.btnSave.Visible = false;
                            this.btnDelete.Visible = true;
                            this.btnPassword.Visible = false;

                            this.lblAccount.Visible = true;
                            this.lblName.Visible = true;
                            this.lblEmail.Visible = true;
                            this.lblUserLevel.Visible = true;

                            this.txtAccount.Visible = false;
                            this.txtName.Visible = false;
                            this.txtEmail.Visible = false;
                            this.ddlUserLevel.Visible = false;

                            this.lblAccount.Text = editUser["Account"].ToString();
                            this.lblName.Text = editUser["Name"].ToString();
                            this.lblEmail.Text = editUser["Email"].ToString();
                            this.lblUserLevel.Text = ((int)editUser["UserLevel"] == 0) ? "管理員" : "一般會員";
                            this.lblCreateDate.Text = editUser["CreateDate"].ToString();
                        }
                    }
                    else // UID和currentUser的ID相同
                    {
                        this.btnSave.Text = "儲存";
                        this.btnDelete.Visible = false;
                        this.btnPassword.Visible = true;

                        this.lblAccount.Visible = true;
                        this.lblName.Visible = false;
                        this.lblEmail.Visible = false;
                        this.lblUserLevel.Visible = true;

                        this.txtAccount.Visible = false;
                        this.txtName.Visible = true;
                        this.txtEmail.Visible = true;
                        this.ddlUserLevel.Visible = false;

                        this.ltlTitle.Text = "會員管理 - 修改會員資料";

                        this.lblAccount.Text = currentUser.Account;
                        this.txtName.Text = currentUser.Name;
                        this.txtEmail.Text = currentUser.Email;
                        this.lblUserLevel.Text = (currentUser.UserLevel == 0) ? "管理員" : "一般會員";
                        this.lblCreateDate.Text = currentUser.CreateDate;
                    }

                    // 修改模式的話有delete和變更密碼的按鈕，可變更姓名、Email，並顯示建立時間
                  
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
            string accountUID = this.Request.QueryString["UID"];
            UserInfoManager.DeleteUserInfo(accountUID);
            Response.Redirect("UserList.aspx");
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

    }
}