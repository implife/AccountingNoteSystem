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
            // 檢查是否登入

            // 檢查資料庫中有沒有該使用者，有的話建立UserInfoModel

            // 顯示使用者帳號
        }

        // 變更的button click事件
            // 判斷輸入值是否正確，新密碼跟確認新密碼是否一樣
            // 輸入值正確的話就呼叫UpdateUserInfo(在UserInfoManager新建方法)


        // 取消的button click事件，跳頁至UserDetail.aspx
    }
}