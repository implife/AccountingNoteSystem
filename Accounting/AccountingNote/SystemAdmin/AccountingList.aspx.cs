using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class AccountingList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
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
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;
            }

            // 取得該使用者所建立的所有帳目，存放於dt
            DataTable dt = AccountingManager.GetAccountingList(currentUser.ID);

            // 檢查帳目數量是否為0
            if(dt.Rows.Count > 0)
            {
                // 設定GridView
                this.gvAccountingList.DataSource = dt;
                this.gvAccountingList.DataBind();
            }
            else
            {
                this.gvAccountingList.Visible = false;
                this.plcNoData.Visible = true;  // 將No Data資訊的PlaceHolder顯示出來
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/AccountingDetail.aspx");
        }

        /// <summary>
        /// GridView中的DataBound()被呼叫時，將收入支出的下拉選單選項設為"支出"或"收入"，而非0或1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">型態為GridViewRowEventArgs，可取得GridView下的每一列資料</param>
        protected void gvAccountingList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // 取得GridView的每一列，資料型態為GridViewRow
            GridViewRow row = e.Row;

            
            if(row.RowType == DataControlRowType.DataRow) // GridView中的列有可能是Header、Footer、DataRow等等
            {
                Label lbl = row.FindControl("lblActType") as Label;
                DataRowView dr = row.DataItem as DataRowView;

                int actType = dr.Row.Field<int>("ActType");

                // 將 In / Out欄位的下拉選單選項設為"支出"或"收入"，而非0或1
                if (actType == 0)
                    lbl.Text = "支出";
                else
                    lbl.Text = "收入";

                // 將金額部分超過1500的資料設為紅字
                if(dr.Row.Field<int>("Amount") > 1500)
                {
                    lbl.ForeColor = Color.Red;
                }

            }
        }
    }
}