<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserPassword.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table.UserPasswordEdit th{
            text-align: right;
        }
        table.UserPasswordEdit td{
            padding: 5px 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
    </h2>
    <table class="UserPasswordEdit">
        <tr>
            <th>帳號</th>
            <td>
                <asp:Label ID="lblAccount" runat="server" Text="--"></asp:Label>
            </td>
        </tr>

        <asp:PlaceHolder ID="plcOriginalPWD" runat="server">
            <tr>
                <th>原密碼</th>
                <td>
                    <asp:TextBox ID="txtOriginPWD" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
        </asp:PlaceHolder>

        <tr>
            <th>新密碼</th>
            <td>
                <asp:TextBox ID="txtNewPWD" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <th>確認新密碼</th>
            <td>
                <asp:TextBox ID="txtNewPWDAgain" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnSave" runat="server" Text="儲存" OnClick="btnSave_Click" />
    <br />
    <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>


</asp:Content>
