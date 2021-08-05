<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AccountingNote.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>流水帳管理系統</title>
    <style>
        h1{background:radial-gradient(ellipse 300% 150% at center,lightblue,white);}
       
    	.auto-style1 {
			width: 804px;
		}
		.auto-style4 {
			width: 804px;
			height: 1px;
		}
		       
    	th {text-align:right;}
        td {text-align:left;}
       
    	.auto-style10 {
			width: 1037px;
			height: 134px;
			margin-left: 383px;
		}
       
    	.auto-style12 {
			height: 1010px;
			width: 1578px;
			margin-right: 0px;
			margin-top: 0px;
		}
       
    	.auto-style13 {
			height: 63px;
			width: 1576px;
			margin-left: 0px;
			margin-right: 0px;
			margin-top: 0px;
		}
       
    	.auto-style15 {
			width: 804px;
			height: 34px;
		}
		.auto-style16 {
			width: 175px;
			height: 1px;
		}
		.auto-style17 {
			width: 175px;
			height: 34px;
		}
		.auto-style18 {
			width: 175px;
		}
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 0px; text-align:center;background:radial-gradient(ellipse 200% 100% at center,lightblue,white);" class="auto-style12">

        <h1 style="text-align:center; font-size: 45px;"class="auto-style13">流水帳管理系統</h1>

        <br />
          
        <table  class="auto-style10">
            <tr>
                <th class="auto-style16" rowspan="1" style="font-size: 26px">初次記帳時間:</th>
                <td class="auto-style4" style="font-size: 26px">
                    &ensp;<asp:Label ID="lblFirstDate" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th class="auto-style16" style="font-size: 26px">&nbsp;</th>
                <td class="auto-style4" style="font-size: 26px">
                    &nbsp;</td>
            </tr>
            <tr>
                <th class="auto-style17" style="font-size: 26px">最後記帳時間:</th>
                <td class="auto-style15" style="font-size: 26px">
                     &ensp;<asp:Label ID="lblLastDate" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th class="auto-style17" style="font-size: 26px">&nbsp;</th>
                <td class="auto-style15" style="font-size: 26px">
                     &nbsp;</td>
            </tr>
            <tr>
                <th class="auto-style18" style="font-size: 26px">帳目數量: </th>
                <td class="auto-style1" style="font-size: 26px">
                     &ensp;<asp:Label ID="lblAccountQuantity" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th class="auto-style18" style="font-size: 26px">&nbsp;</th>
                <td class="auto-style1" style="font-size: 26px">
                     &nbsp;</td>
            </tr>
            <tr>
                <th class="auto-style18" style="font-size: 26px">總會員數:</th>
                <td class="auto-style1" style="font-size: 26px">
                    &ensp;<asp:Label ID="lblUserQuantity" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th class="auto-style18" style="font-size: 26px">&nbsp;</th>
                <td class="auto-style1" style="font-size: 26px">
                    &nbsp;</td>
            </tr>
        </table>
            <br />
            <a href="Login.aspx" aria-checked="undefined" aria-disabled="False" style="padding: 0px; font-size: 30px; background:radial-gradient(ellipse 200% 3000% at center,lightblue,white); background-color:lightblue;">登入系統</a>
            <br />
</div>
        
    </form>
</body>
</html>
