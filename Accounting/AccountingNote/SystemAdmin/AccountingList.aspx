<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AccountingList.aspx.cs" Inherits="AccountingNote.SystemAdmin.AccountingList" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnCreate" runat="server" Text="Add" OnClick="btnCreate_Click" />

    <asp:Label ID="lblTotalAmount" runat="server" Text="小記: 元"></asp:Label>

    <asp:GridView ID="gvAccountingList" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvAccountingList_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="標題" DataField="Caption" />
            <asp:BoundField HeaderText="金額" DataField="Amount" />

            <asp:TemplateField HeaderText="In / Out">
                <ItemTemplate>

                    <asp:Label ID="lblActType" runat="server" Text="Label"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField HeaderText="建立日期" DataField="CreateDate" />

            <asp:TemplateField HeaderText="Act">
                <ItemTemplate>
                    <a href="/SystemAdmin/AccountingDetail.aspx?ID=<%# Eval("ID") %>">編輯</a>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <uc1:ucPager runat="server" ID="ucPager" url="AccountingList.aspx" pagesize="3" totalsize="10" currentpage="1"/>

    <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
        <p>
            No Data in your Accounting Note.
        </p>
    </asp:PlaceHolder>
</asp:Content>
