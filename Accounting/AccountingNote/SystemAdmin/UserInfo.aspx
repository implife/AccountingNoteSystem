﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td colspan="2">
                    <h2>流水帳管理系統</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="UserInfo.aspx">使用者資訊</a> <br />
                    <a href="AccountingList.aspx">流水帳管理</a>
                </td>
                <td>
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
                    </table>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnLogout" runat="server" Text="Log Out" OnClick="btnLogout_Click" />
    </form>
</body>
</html>