<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>會員管理</title>
    <style>
        table.UserEditTable th {
            text-align: right;
        }

        table.UserEditTable td {
            padding: 5px 15px;
        }
    	.auto-style7 {
			width: 396px;
            text-align:right;
		}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<h2 align="center" class="auto-style6">
        <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
    </h2>

    <table align="center">
        <tr>
            <th>帳號</th>
            <td >
                <asp:Label ID="lblAccount" runat="server" Text="--"></asp:Label>
                <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <th>姓名</th>
            <td>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <asp:Label ID="lblName" runat="server" Text="--"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Email</th>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                <asp:Label ID="lblEmail" runat="server" Text="--"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>等級</th>
            <td>
                <asp:Label ID="lblUserLevel" runat="server" Text="--"></asp:Label>
                <asp:DropDownList ID="ddlUserLevel" runat="server">
                    <asp:ListItem Value="0">管理員</asp:ListItem>
                    <asp:ListItem Value="1">一般會員</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th><asp:Label ID="lblCreateDateTitle" runat="server" Text="建立時間"></asp:Label></th>
            <td>
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
