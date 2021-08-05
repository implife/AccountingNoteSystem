<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        th {
            text-align: right;
        }
        td{
            padding: 3px 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <th>Account</th>
            <td>
                <asp:Literal ID="ltlAccount" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>Name</th>
            <td>
                <asp:Literal ID="ltlName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>Email</th>
            <td>
                <asp:Literal ID="ltlEmail" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>User Level</th>
            <td>
                <asp:Literal ID="ltlUserLevel" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>Create Date</th>
            <td>
                <asp:Literal ID="ltlCreateDate" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnLogout" runat="server" Text="Log Out" OnClick="btnLogout_Click" Visible="false"  />
</asp:Content>
