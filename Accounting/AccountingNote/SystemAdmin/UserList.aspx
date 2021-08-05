<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="AccountingNote.SystemAdmin.WebForm1" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<asp:GridView ID="gvUserList" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Height="253px" Width="395px">
	<FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
	<HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
	<PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
	<RowStyle BackColor="White" ForeColor="#003399" />
	<SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
	<SortedAscendingCellStyle BackColor="#EDF6F6" />
	<SortedAscendingHeaderStyle BackColor="#0D4AC4" />
	<SortedDescendingCellStyle BackColor="#D6DFDF" />
	<SortedDescendingHeaderStyle BackColor="#002876" />
</asp:GridView>
    <%--<uc1:ucPager runat="server" id="ucPager" />--%>
</asp:Content>
