using System;
using System.Collections.Generic;
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
            // 檢查是否登入

            // 檢查資料庫中有沒有該使用者，有的話建立UserInfoModel

            // 判斷是否為post back
                // 判斷是新增模式還是修改模式，URL中沒有UID的參數表示是新增模式
                // 新增模式的話沒有delete的按鈕，並可輸入帳號、姓名、Email、會員等級
                // 修改模式的話有delete和變更密碼的按鈕，可變更姓名、Email，並顯示建立時間

        }

        // 儲存的button click事件
            // 檢查輸入值是否正確(可建立方法)
            // 正確的話取得輸入值

            // 判斷是新增模式還是修改模式
            // 新增模式的話呼叫CreateUserInfo(在UserInfoManager新建方法)
            // 修改模式的話呼叫UpdateUserInfo(在UserInfoManager新建方法)


        // 刪除的button click事件
            // 呼叫DeleteUserInfo(在UserInfoManager新建方法)



        // 修改密碼button click事件，跳頁至UserPassword.aspx
    }
}