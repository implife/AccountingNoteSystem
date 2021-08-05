<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<style>
        th {
            text-align: right;
        }
        td{ text-align:left;
            padding: 3px 5px;
        }
	    table {text-align:center}
    	.auto-style3 {
			width: 973px;
		}
		.auto-style4 {
			width: 112px;
		}
    	.auto-style5 {
			width: 196px;
		}
    	.auto-style7 {
			width: 324px;
		}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div style="text-align: left";> <h3 >使用者資訊:</h3>
   
	<table>
        <tr>
            <th class="auto-style4">Account:</th>
            <td class="auto-style5">
                <asp:Literal ID="ltlAccount" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th class="auto-style4">Name:</th>
            <td class="auto-style5">
                <asp:Literal ID="ltlName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th class="auto-style4">Email:</th>
            <td class="auto-style5">
                <asp:Literal ID="ltlEmail" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th class="auto-style4">User Level:</th>
            <td class="auto-style5">
                <asp:Literal ID="ltlUserLevel" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th class="auto-style4">Create Date:</th>
            <td class="auto-style5">
                <asp:Literal ID="ltlCreateDate" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <br />
        <div style="text-align:right" class="auto-style7">
    <asp:Button ID="btnLogout" runat="server" Text="登出" OnClick="btnLogout_Click" Height="37px" Width="85px" Font-Bold="True" Font-Strikeout="False" Font-Underline="False" ToolTip="登出系統" Font-Size="Large" />
            </div>
        </div>
</asp:Content>
