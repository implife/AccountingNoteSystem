<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AccountingDetail.aspx.cs" Inherits="AccountingNote.SystemAdmin.AccountingDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<style type="text/css">
		.auto-style4 {
			margin-left: 0px;
		}
		.auto-style5 {
			margin-left: 3px;
		}
	</style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div style="text-align: left;">
		Type:&ensp;
    <asp:DropDownList ID="ddlType" runat="server" ToolTip="選擇為收入或支出">
		<asp:ListItem Value="0">支出</asp:ListItem>
		<asp:ListItem Value="1">收入</asp:ListItem>
	</asp:DropDownList>
		<br />
		<br />
		Amount:&ensp;
    <asp:TextBox ID="txtAmount" runat="server" TextMode="Number"></asp:TextBox>
		<br />
		<br />
		Caption:&ensp;
    <asp:TextBox ID="txtCaption" runat="server" CssClass="auto-style5" Width="178px"></asp:TextBox>
		<br />

		<br /><div>Description:&ensp;</div> 
    <asp:TextBox ID="txtDesc" runat="server" Rows="5" TextMode="MultiLine" CssClass="auto-style4" Width="260px" Height="126px"></asp:TextBox>&ensp;
    <br />
		<br />
		<br />
		<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="61px" Font-Bold="True" ToolTip="儲存" />&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" Font-Bold="True" ToolTip="刪除" />
		<br />
		<br />


		<asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
	</div>
</asp:Content>
