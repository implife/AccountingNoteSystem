<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AccountingNote.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>流水帳管理系統</h2>
        <a href="Login.aspx">登入系統</a>
        <br />
        <table>
            <tr>
                <th>初次記帳</th>
                <td>
                    <asp:Label ID="lblFirstDate" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>最後記帳</th>
                <td>
                    <asp:Label ID="lblLastDate" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>帳目數量</th>
                <td>
                    <asp:Label ID="lblAccountQuantity" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>會員數</th>
                <td>
                    <asp:Label ID="lblUserQuantity" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
