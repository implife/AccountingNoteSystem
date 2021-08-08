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

            // 資料庫中沒有currentUser資料，可能被管理者砍帳號
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

                // 判斷UID是否有值，沒有值必定為新增使用者模式
                if (accountUID == null) // 新增使用者模式
                {
                    // 新增使用者模式的話沒有delete和password按鈕，並可輸入帳號、姓名、Email、會員等級
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

                    this.lblCreateDateTitle.Visible = false;
                    this.lblCreateDate.Visible = false;
                }
                else // 有UID
                {
                    DataRow editUser = UserInfoManager.GetUserInfoByUserID(accountUID);
                    // 到資料庫尋找該UID的使用者是否存在
                    if (editUser == null)
                    {
                        ltlMsg.Text = "Wrong Edit_UserUID.";
                        return;
                    }

                    // 判斷UID和currentUser的ID是否相同，相同表示是修改自己的會員資料，不同表示是管理者編輯別人的資料
                    if (string.Compare(accountUID, currentUser.ID) != 0) // 管理者進入別的會員的編輯頁面
                    {
                        // 管理者編輯別人的資料時只能刪除，不能修改任何資料或密碼
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

                        // 將該UID的會員資料顯示
                        this.lblAccount.Text = editUser["Account"].ToString();
                        this.lblName.Text = editUser["Name"].ToString();
                        this.lblEmail.Text = editUser["Email"].ToString();
                        this.lblUserLevel.Text = ((int)editUser["UserLevel"] == 0) ? "管理員" : "一般會員";
                        this.lblCreateDate.Text = editUser["CreateDate"].ToString();
                        
                    }
                    else // UID和currentUser的ID相同，表示登入者修改自己的資料
                    {
                        // 使用者不能刪除自己的會員，可修該姓名和Email，並可進入修改密碼的頁面
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

            if (this.txtAccount.Visible) // 新增會員模式，建立UserInfoModel並放入Session，接著進入UserPassword頁面設定密碼
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
            else // 編輯自己的資料模式
            {
                UserInfoManager.UpdateUserInfo(currentUser.ID, currentUser.Account, name, email);
                Response.Redirect("UserList.aspx");
            }

        }

        /// <summary>
        /// btnDelete只有在管理者進入別人的頁面時會出現，所以就刪除該UID的會員
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string accountUID = this.Request.QueryString["UID"];
            UserInfoManager.DeleteUserInfo(accountUID);
            Response.Redirect("UserList.aspx");
        }

        /// <summary>
        /// btnPassword只有在使用者編輯自己的資料時出現，進入UserPassword頁面時UID為currentUser的ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPassword_Click(object sender, EventArgs e)
        {
            UserInfoModel currentUser = AuthManager.GetCurrentUser();
            Response.Redirect("UserPassword.aspx?UID=" + currentUser.ID);
        }

        private bool CheckInput(out List<string> errMsgList)
        {
            List<string> msgList = new List<string>();

            // visible = true表示為新增會員模式
            if (this.txtAccount.Visible)
            {
                if (string.IsNullOrWhiteSpace(this.txtAccount.Text))
                    msgList.Add("帳號不可為空");
            }

            if (string.IsNullOrWhiteSpace(this.txtName.Text))
                msgList.Add("姓名不可為空");

            if (string.IsNullOrWhiteSpace(this.txtEmail.Text))
                msgList.Add("Email不可為空");

            // visible = true表示為新增會員模式
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