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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // this.Master會取得System.Web.UI.MasterPage，須轉型成Admin(子類別)才可存取屬性
            Admin adminMaster = this.Master as Admin;


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

            this.gvUserList.DataSource = UserInfoManager.GetUserInfoList();
            this.gvUserList.DataBind();

            // 從資料庫將所有使用者資訊拿出並放進GridView裡，一般會員只能修改自己的資料，並且無法新增會員
            // 管理者可以修改所有人的資料並新增會員
            // 按編輯時跳頁至UserDetail
        }

        // 新增會員的button click事件，跳頁至UserDetail
    }
}