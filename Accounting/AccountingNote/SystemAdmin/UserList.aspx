<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="AccountingNote.SystemAdmin.WebForm1" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>會員管理</h2>
    <div>
        <asp:Button ID="btnAdd" runat="server" Text="新增" OnClick="btnAdd_Click" />
    </div>
    <asp:GridView ID="gvUserList" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvUserList_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="帳號" DataField="Account" />
            <asp:BoundField HeaderText="姓名" DataField="Name" />

            <asp:TemplateField HeaderText="等級">
                <ItemTemplate>
                    <asp:Label ID="lblUserLevel" runat="server" Text="Label"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField HeaderText="建立時間" DataField="CreateDate" />

            <asp:TemplateField HeaderText="Act">
                <ItemTemplate>
                    <asp:HyperLink ID="linkEdit" runat="server">編輯</asp:HyperLink>
                    <%--<a href="/SystemAdmin/UserDetail.aspx?UID=<%# Eval("ID") %>">編輯</a>--%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <%--<uc1:ucPager runat="server" id="ucPager" />--%>
</asp:Content>
