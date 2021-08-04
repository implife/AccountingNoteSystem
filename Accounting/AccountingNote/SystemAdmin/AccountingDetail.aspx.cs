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
    public partial class AccountingDetail : System.Web.UI.Page
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


            // 判斷是否為Post back，是的話不執行，否則會把輸入的東西蓋掉
            if (!IsPostBack)
            {
                // 判斷是新增模式還是修改模式，URL中沒有參數表示是新增模式
                if (this.Request.QueryString["ID"] == null) // 新增模式
                {
                    this.btnDelete.Visible = false;
                }
                else // 修改模式
                {
                    this.btnDelete.Visible = true;

                    string idText = this.Request.QueryString["ID"];

                    // 試著將id轉為整數
                    int id;
                    if (int.TryParse(idText, out id))
                    {

                        DataRow drAccounting = AccountingManager.GetAccounting(id, currentUser.ID);

                        if (drAccounting == null)
                        {
                            this.ltlMsg.Text = "Data doesn't exist.";
                            this.btnSave.Visible = false;
                            this.btnDelete.Visible = false;
                        }
                        else
                        {
                            // 將該使用者的該筆帳目顯示在畫面中
                            this.ddlType.SelectedValue = drAccounting["ActType"].ToString();
                            this.txtAmount.Text = drAccounting["Amount"].ToString();
                            this.txtCaption.Text = drAccounting["Caption"].ToString();
                            this.txtDesc.Text = drAccounting["Body"].ToString();
                        }
                    }
                    else
                    {
                        this.ltlMsg.Text = "ID is required.";
                        this.btnSave.Visible = false;
                        this.btnDelete.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// 儲存紐按下時的事件。如果是新增模式就新建一筆帳目到資料庫，修改模式就變更資料庫裡的該筆帳目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // 檢查輸入值是否正確
            this.ltlMsg.Text = "";
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltlMsg.Text = string.Join("<br/>", msgList);
                return;
            }

            UserInfoModel currentUser = AuthManager.GetCurrentUser();
            if (currentUser == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            string userID = currentUser.ID;

            // 取得輸入值
            string actTypeText = this.ddlType.SelectedValue;
            string amountText = this.txtAmount.Text;
            string caption = this.txtCaption.Text;
            string body = this.txtDesc.Text;

            // 將輸入的金額和行為轉型成int
            int amount = Convert.ToInt32(amountText);
            int actType = Convert.ToInt32(actTypeText);

            // 判斷是新增模式還是修改模式，URL中沒有參數表示是新增模式
            string idText = this.Request.QueryString["ID"];
            if (string.IsNullOrWhiteSpace(idText)) // 新增模式
            {
                AccountingManager.CreateAccounting(userID, caption, amount, actType, body);
            }
            else // 修改模式
            {
                int id;
                if (int.TryParse(idText, out id))
                {
                    AccountingManager.UpdateAccounting(id, userID, caption, amount, actType, body);
                }
            }
            Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idText = this.Request.QueryString["ID"];

            if (string.IsNullOrWhiteSpace(idText))
                return;

            int id;
            if (int.TryParse(idText, out id))
            {
                AccountingManager.DeleteAccounting(id);
            }
            Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }

        /// <summary>
        /// 檢查輸入直是否正確
        /// </summary>
        /// <param name="errMsgList">List傳出呼叫，裡面可放複數個錯誤訊息</param>
        /// <returns>布林值，都沒有錯誤訊息回傳true，反之回傳false</returns>
        private bool CheckInput(out List<string> errMsgList)
        {
            List<string> msgList = new List<string>();

            // 檢查行為是否為0或1(支出或收入)
            if (this.ddlType.SelectedValue != "0"
                && this.ddlType.SelectedValue != "1")
            {
                msgList.Add("Type must be 0 or 1.");
            }

            // 檢查金額
            if (string.IsNullOrWhiteSpace(this.txtAmount.Text))
            {
                msgList.Add("Amount is required." + this.txtAmount.Text);
            }
            else
            {
                int tempInt;
                if (!int.TryParse(this.txtAmount.Text, out tempInt))
                {
                    msgList.Add("Amount must be a number.");
                }
                if (tempInt < 0 || tempInt > 1000000)
                {
                    msgList.Add("Amount must between 0 and 1,000,000");
                }
            }
            errMsgList = msgList;

            if (msgList.Count == 0)
                return true;
            else
                return false;
        }
    }
}