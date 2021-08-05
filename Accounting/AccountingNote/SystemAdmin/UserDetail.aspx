<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<style>
        table.UserEditTable th {
            text-align: right;
        }

        table.UserEditTable td {
            padding: 5px 15px;
        }
    	.auto-style4 {
			width: 318px;
			margin-left: 171px;
			margin-right: 0px;
		}
		.auto-style5 {
			height: 27px;
		}
		.auto-style6 {
			width: 425px;
            text-align:right
		}
    	.auto-style7 {
			width: 396px;
            text-align:right;
		}
    	.auto-style8 {
			width: 217px;
		}
		.auto-style9 {
			height: 27px;
			width: 217px;
		}
		.auto-style10 {
			height: 24px;
		}
		.auto-style11 {
			width: 217px;
			height: 24px;
		}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<h2 align="center" class="auto-style6">
        <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
    </h2>

    <table align="center" class="auto-style4">
        <tr>
            <th>帳號</th>
            <td class="auto-style8">
                <asp:Label ID="lblAccount" runat="server" Text="--"></asp:Label>
                <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <th class="auto-style5">姓名</th>
            <td class="auto-style9">
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>Email</th>
            <td class="auto-style8">
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>等級</th>
            <td class="auto-style8">
                <asp:Label ID="lblUserLevel" runat="server" Text="--"></asp:Label>
                <asp:DropDownList ID="ddlUserLevel" runat="server">
                    <asp:ListItem Value="0">管理員</asp:ListItem>
                    <asp:ListItem Value="1">一般會員</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th class="auto-style10">建立時間</th>
            <td class="auto-style11">
                <asp:Label ID="lblCreateDate" runat="server" Text="--"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <div style="text-align:right" class="auto-style7">
        <asp:Button ID="btnSave" runat="server" Text="儲存" OnClick="btnSave_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click" />
        <asp:Button ID="btnPassword" runat="server" Text="變更密碼" OnClick="btnPassword_Click" Width="76px" />
    </div>
    <div style="text-align:center"><asp:Literal ID="ltlMsg" runat="server"></asp:Literal></div>
    
</asp:Content>
