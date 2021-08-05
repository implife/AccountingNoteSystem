<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AccountingList.aspx.cs" Inherits="AccountingNote.SystemAdmin.AccountingList" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>帳目管理</title>
	<style type="text/css">
		.auto-stylem {
			width: 419px;
			height: 25px;
			margin-left: 4px;
		}
		.auto-style8 {
			margin-left:90px;
			
		}
		.auto-stylenew {
			width: 415px;
			margin-left: 88px;
		}
		.auto-style10 {
			margin-left: 0px;
		}
		.auto-stylepage {
			width: 425px;
			margin-left: 80px;
			text-align:center
		}
		.auto-style12 {
			width: 509px;
			height: 25px;
			margin-left: 4px;
		}
	</style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


	<asp:GridView ID="gvAccountingList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvAccountingList_RowDataBound" CellPadding="4" CssClass="auto-style8" Width="423px" ForeColor="#333333" GridLines="None">
		<AlternatingRowStyle BackColor="White" />
		<columns>
			<asp:BoundField HeaderText="標題" DataField="Caption" />
			<asp:BoundField HeaderText="金額" DataField="Amount" />

			<asp:TemplateField HeaderText="In / Out">
				<itemtemplate>
					<asp:Label ID="lblActType" runat="server" Text=""></asp:Label>
				</itemtemplate>
			</asp:TemplateField>

			<asp:BoundField HeaderText="建立日期" DataField="CreateDate" />

			<asp:TemplateField HeaderText="Act">
				<itemtemplate>
					<a href="/SystemAdmin/AccountingDetail.aspx?ID=<%# Eval("ID") %>">編輯</a>
				</itemtemplate>
			</asp:TemplateField>

		</columns>

		<EditRowStyle BackColor="#2461BF" />

		<footerstyle backcolor="#507CD1" forecolor="White" Font-Bold="True" />
		<headerstyle backcolor="#507CD1" font-bold="True" forecolor="White" />
		<pagerstyle backcolor="#2461BF" forecolor="White" horizontalalign="Center" />
		<rowstyle backcolor="#EFF3FB" />
		<selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
		<sortedascendingcellstyle backcolor="#F5F7FB" />
		<sortedascendingheaderstyle backcolor="#6D95E1" />
		<sorteddescendingcellstyle backcolor="#E9EBEF" />
		<sorteddescendingheaderstyle backcolor="#4870BE" />

	</asp:GridView>

	<div style="text-align: right" class="auto-style12">
		<asp:Label ID="lblTotalAmount" runat="server" Text="小記: 元" Font-Bold="False"></asp:Label>
	</div>


	<div style="text-align: left" class="auto-stylenew">
		<asp:Button ID="btnCreate" runat="server" Text="新增" OnClick="btnCreate_Click" Height="33px" Width="65px" CssClass="auto-style10" Font-Bold="True" ToolTip="新增帳目" Font-Size="Medium" />
	</div>


	<br />


	<br />
	<div class="auto-stylepage">
		<uc1:ucpager runat="server" id="ucPager" url="AccountingList.aspx" pagesize="8" totalsize="10" currentpage="1" />
	</div>
	<div style="text-align: center" class="auto-style11">
		<asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
			<p>
				No Data in your Accounting Note.
			</p>
		</asp:PlaceHolder>
		</div>

	

	
</asp:Content>
