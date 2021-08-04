<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPager.ascx.cs" Inherits="AccountingNote.UserControls.ucPager" %>
<div>
    <asp:Label ID="lblPageInfo" runat="server" Text="Label"></asp:Label>

    <asp:HyperLink ID="HLinkFirst" runat="server">First</asp:HyperLink>
    <asp:Literal ID="ltPager" runat="server"></asp:Literal>
    <asp:HyperLink ID="HLinkLast" runat="server">Last</asp:HyperLink>
</div>