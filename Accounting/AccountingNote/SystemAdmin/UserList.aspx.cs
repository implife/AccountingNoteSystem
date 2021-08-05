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
    public partial class WebForm1 : System.Web.UI.Page
    {
        public object DataViewRow { get; private set; }

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

            if (currentUser.UserLevel == 1)
                this.btnAdd.Enabled = false;
        }

        protected void gvUserList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            UserInfoModel currentUser = AuthManager.GetCurrentUser();

            if (gvRow.RowType == DataControlRowType.DataRow)
            {
                // 將UserLevel的0和1換成管理員和一般會員
                Label lbl = gvRow.FindControl("lblUserLevel") as Label;
                DataRow dr = (gvRow.DataItem as DataRowView).Row;

                int level = dr.Field<int>("UserLevel");
                if (level == 0)
                    lbl.Text = "管理員";
                else
                    lbl.Text = "一般會員";

                // 設定編輯的超連結，並只能編輯自己的資料，管理者可進入所有人的編輯頁面
                HyperLink link = gvRow.FindControl("linkEdit") as HyperLink;

                if(currentUser.UserLevel == 0)
                {
                    link.NavigateUrl = "/SystemAdmin/UserDetail.aspx?UID=" + dr["ID"].ToString();
                }
                else
                {
                    if (string.Compare(dr.Field<string>("Account"), currentUser.Account) == 0)
                        link.NavigateUrl = "/SystemAdmin/UserDetail.aspx?UID=" + dr["ID"].ToString();
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserDetail.aspx");
        }
    }
}