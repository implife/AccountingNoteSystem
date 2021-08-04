using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.Auth
{
    /// <summary>
    /// 利用UserInfoModel類別就不用一直使用DataRow存取使用者資訊
    /// </summary>
    public class UserInfoModel
    {
        public string ID { get; set; }
        public string Account { get; set; }
        public string PWD { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int UserLevel { get; set; }
        public string CreateDate { get; set; }

    }
}
