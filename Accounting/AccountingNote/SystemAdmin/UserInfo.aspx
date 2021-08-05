<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<style>
	    th {
	        text-align: right;
	    }

	    td {
	        text-align: left;
	        padding: 3px 5px;
	    }

	    table {
	        text-align: center
	    }

	    .auto-style3 {
	        width: 973px;
	    }

	    .auto-style4 {
	        width: 112px;
            
	    }

	    .auto-style5 {
	        width: 196px;
	    }

	    .auto-style8 {
			width: 317px;
			margin-left: 178px;
		}
		.auto-style9 {
			height: 23px;
			width: 594px;
		}
		.auto-style10 {
			width: 348px;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="auto-style9"><h3 align="right" class="auto-style10"> 使用者資訊</h3></div>
   <br />
	<br />
	<table align="center" class="auto-style8">
        <tr>
            <th class="auto-style4">帳戶:</th>
            <td class="auto-style5">
                <asp:Literal ID="ltlAccount" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th class="auto-style4">姓名:</th>
            <td class="auto-style5">
                <asp:Literal ID="ltlName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th class="auto-style4">電子信箱:</th>
            <td class="auto-style5">
                <asp:Literal ID="ltlEmail" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th class="auto-style4">用戶等級:</th>
            <td class="auto-style5">
                <asp:Literal ID="ltlUserLevel" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th class="auto-style4">創建日期:</th>
            <td class="auto-style5">
                <asp:Literal ID="ltlCreateDate" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>
