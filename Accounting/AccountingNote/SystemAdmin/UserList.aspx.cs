using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 檢查是否登入

            // 檢查資料庫中有沒有該使用者

            // 從資料庫將所有使用者資訊拿出並放進GridView裡，一般會員只能修改自己的資料，並且無法新增會員
            // 管理者可以修改所有人的資料並新增會員
            // 按編輯時跳頁至UserDetail

        }

        // 新增會員的button click事件，跳頁至UserDetail
    }
}