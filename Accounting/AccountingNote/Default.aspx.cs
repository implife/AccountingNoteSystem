using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int accountingCount;
            DateTime? first, last;
            AccountingManager.GetAccountingInfo(out accountingCount, out first, out last);

            // 將初次記帳日、最後記帳日、帳目數量、會員數量顯示於畫面上
            if (first != null && last != null)
            {
                this.lblFirstDate.Text = first.ToString();
                this.lblLastDate.Text = last.ToString();
                
            }
            else
            {
                this.lblFirstDate.Text = "--";
                this.lblLastDate.Text = "--";
            }
            this.lblAccountQuantity.Text = accountingCount.ToString();
            this.lblUserQuantity.Text = UserInfoManager.GetUserQuantity().ToString();


        }
    }
}