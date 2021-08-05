<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AccountingList.aspx.cs" Inherits="AccountingNote.SystemAdmin.AccountingList" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>帳目管理</title>
	<style type="text/css">
		.auto-style4 {
			width: 419px;
			height: 25px;
			margin-left: 4px;
		}
		.auto-style8 {
			margin-left: 4px;
		}
		.auto-style9 {
			width: 415px;
			margin-left: 6px;
		}
		.auto-style10 {
			margin-left: 0px;
		}
		.auto-style11 {
			width: 425px;
			margin-left: 0px;
		}
	</style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


	<div style="text-align: left" class="auto-style9">
		<asp:Button ID="btnCreate" runat="server" Text="新增" OnClick="btnCreate_Click" Height="33px" Width="65px" CssClass="auto-style10" Font-Bold="True" ToolTip="新增帳目" Font-Size="Medium" />
	</div>


	<asp:GridView ID="gvAccountingList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvAccountingList_RowDataBound" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="auto-style8" Width="423px">
		<columns>
			<asp:BoundField HeaderText="標題" DataField="Caption" />
			<asp:BoundField HeaderText="金額" DataField="Amount" />

			<asp:TemplateField HeaderText="In / Out">
				<itemtemplate>
					<asp:Label ID="lblActType" runat="server" Text="Label"></asp:Label>
				</itemtemplate>
			</asp:TemplateField>

			<asp:BoundField HeaderText="建立日期" DataField="CreateDate" />

			<asp:TemplateField HeaderText="Act">
				<itemtemplate>
					<a href="/SystemAdmin/AccountingDetail.aspx?ID=<%# Eval("ID") %>">編輯</a>
				</itemtemplate>
			</asp:TemplateField>

		</columns>

		<footerstyle backcolor="#99CCCC" forecolor="#003399" />
		<headerstyle backcolor="#003399" font-bold="True" forecolor="#CCCCFF" />
		<pagerstyle backcolor="#99CCCC" forecolor="#003399" horizontalalign="Left" />
		<rowstyle backcolor="White" forecolor="#003399" />
		<selectedrowstyle backcolor="#009999" font-bold="True" forecolor="#CCFF99" />
		<sortedascendingcellstyle backcolor="#EDF6F6" />
		<sortedascendingheaderstyle backcolor="#0D4AC4" />
		<sorteddescendingcellstyle backcolor="#D6DFDF" />
		<sorteddescendingheaderstyle backcolor="#002876" />

	</asp:GridView>

	<div style="text-align: right" class="auto-style4">
		<asp:Label ID="lblTotalAmount" runat="server" Text="小記: 元" Font-Bold="False"></asp:Label>
	</div>
	<br />


	<br />
	<div class="auto-style11">
		<uc1:ucpager runat="server" id="ucPager" url="AccountingList.aspx" pagesize="8" totalsize="10" currentpage="1" />
	</div>
	<br />
	<div style="text-align: center" class="auto-style11">

		<asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
			<p>
				No Data in your Accounting Note.
			</p>
		</asp:PlaceHolder>
	</div>
</asp:Content>
