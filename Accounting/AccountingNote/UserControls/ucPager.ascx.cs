using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.UserControls
{
    public partial class ucPager : System.Web.UI.UserControl
    {
        public string Url { get; set; }
        public int TotalSize { get; set; }
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;


        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void Bind()
        {
            this.CurrentPage = this.GetCurrentPage();
            int totalPages = this.GetTotalPage();

            this.lblPageInfo.Text = $"共 {this.TotalSize} 筆，共 {this.GetTotalPage()} 頁，目前在第 {this.CurrentPage} 頁<br>";

            // 設定LinkFirst和LinkLast
            if (this.CurrentPage > 1)
                this.HLinkFirst.Attributes.Add("href", $"{this.Url}?Page=1");

            if (this.CurrentPage < totalPages)
                this.HLinkLast.Attributes.Add("href", $"{this.Url}?Page={totalPages}");

            // 迴圈的startPage & endPage
            int startPage, endPage;
            if (this.CurrentPage == 1 || this.CurrentPage == 2)
                startPage = 1;
            else
                startPage = this.CurrentPage - 2;

            if (this.CurrentPage == totalPages || this.CurrentPage + 1 == totalPages)
                endPage = totalPages;
            else
                endPage = this.CurrentPage + 2;

            // 建立 Pager
            for (int i = startPage; i <= endPage; i++)
            {
                if (i == this.CurrentPage)
                    this.ltPager.Text += $"<a>{i}</a>&nbsp";
                else
                    this.ltPager.Text += $"<a href='{this.Url}?Page={i}'>{i}</a>&nbsp";
            }
        }

        private int GetTotalPage()
        {
            int pages = this.TotalSize / this.PageSize;
            if (this.TotalSize % this.PageSize > 0)
                pages += 1;
            return pages;
        }

        public int GetCurrentPage()
        {
            string pageText = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(pageText))
                return 1;

            int intPage;
            if (!int.TryParse(pageText, out intPage))
                return 1;

            if (intPage <= 0)
                return 1;

            return intPage;
        }

    }
}