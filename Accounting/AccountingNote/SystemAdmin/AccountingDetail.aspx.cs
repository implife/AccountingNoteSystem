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
    public partial class AccountingDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 先判斷是否是登入狀態
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


            // 判斷是否為Post back，是的話不執行，否則會把輸入的東西蓋掉
            if (!IsPostBack)
            {
                // Check is Create or Edit mode.
                if (this.Request.QueryString["ID"] == null) // Create mode
                {
                    this.btnDelete.Visible = false;
                }
                else // Edit mode
                {
                    this.btnDelete.Visible = true;

                    string idText = this.Request.QueryString["ID"];
                    int id;
                    if (int.TryParse(idText, out id))
                    {
                        var drAccounting = AccountingManager.GetAccounting(id, currentUser.ID);

                        if (drAccounting == null)
                        {
                            this.ltlMsg.Text = "Data doesn't exist.";
                            this.btnSave.Visible = false;
                            this.btnDelete.Visible = false;
                        }
                        else
                        {
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

            // 取得使用者資料(drUserInfo)
            string account = this.Session["UserLoginInfo"] as string;
            var drUserInfo = UserInfoManager.GetUserInfoByAccount(account);

            if (drUserInfo == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            // 取得輸入值
            string userID = drUserInfo["ID"].ToString();
            string actTypeText = this.ddlType.SelectedValue;
            string amountText = this.txtAmount.Text;
            string caption = this.txtCaption.Text;
            string body = this.txtDesc.Text;

            int amount = Convert.ToInt32(amountText);
            int actType = Convert.ToInt32(actTypeText);

            // Check Create mode or Edit mode.
            string idText = this.Request.QueryString["ID"];

            if (string.IsNullOrWhiteSpace(idText)) // Create
            {
                AccountingManager.CreateAccounting(userID, caption, amount, actType, body);
            }
            else // Edit
            {
                int id;
                if (int.TryParse(idText, out id)) 
                {
                    AccountingManager.UpdateAccounting(id, userID, caption, amount, actType, body);
                }
            }
            Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }

        private bool CheckInput(out List<string> errMsgList)
        {
            List<string> msgList = new List<string>();

            // type
            if(this.ddlType.SelectedValue != "0" 
                && this.ddlType.SelectedValue != "1")
            {
                msgList.Add("Type must be 0 or 1.");
            }

            // Amount
            if (string.IsNullOrWhiteSpace(this.txtAmount.Text))
            {
                msgList.Add("Amount is required." + this.txtAmount.Text);
            }
            else
            {
                int tempInt;
                if(!int.TryParse(this.txtAmount.Text, out tempInt))
                {
                    msgList.Add("Amount must be a number.");
                }
                if(tempInt < 0 || tempInt > 1000000)
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
    }
}