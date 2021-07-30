<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountingList.aspx.cs" Inherits="AccountingNote.SystemAdmin.AccountingList" %>

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
                    <a href="UserInfo.aspx">使用者資訊</a><br />
                    <a href="AccountingList.aspx">流水帳管理</a>
                </td>
                <td>
                    <asp:Button ID="btnCreate" runat="server" Text="Add" OnClick="btnCreate_Click" />

                    <asp:GridView ID="gvAccountingList" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvAccountingList_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="標題" DataField="Caption" />
                            <asp:BoundField HeaderText="金額" DataField="Amount" />
                            <%--<asp:BoundField HeaderText="In / Out" DataField="ActType" />--%>

                            <asp:TemplateField HeaderText="In / Out">
                                <ItemTemplate>
                                    <%--<%# ((int)Eval("ActType") == 0) ? "支出" : "收入" %>--%>
                                    
                                    <asp:Label ID="lblActType" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField HeaderText="建立日期" DataField="CreateDate" />
                            
                            <asp:TemplateField HeaderText="Act">
                                <ItemTemplate>
                                    <a href="/SystemAdmin/AccountingDetail.aspx?ID=<%# Eval("ID") %>">編輯</a>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                    <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
                        <p>
                            No Data in your Accounting Note.
                        </p>
                    </asp:PlaceHolder>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
