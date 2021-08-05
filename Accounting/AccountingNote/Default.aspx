<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AccountingNote.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {text-align:center;background:radial-gradient(ellipse 200% 80% at center,lightblue,white);
        }
        h1{background:radial-gradient(ellipse 200% 100% at center,lightblue,white);}
       
    	.auto-style1 {
			width: 763px;
		}
		.auto-style4 {
			width: 763px;
			height: 1px;
		}
		       
    	th {text-align:right;}
        td {text-align:left;}
       
    	.auto-style {
			width: 423px;
			height: 165px;
			margin-left: 383px;
		}
       
    	.auto-style13 {
			height: 63px;
			width: 1473px;
			margin-left: 0px;
			margin-right: 0px;
			margin-top: 0px;
		}
       
    	.auto-style15 {
			width: 763px;
			height: 34px;
		}
		.auto-stylemainpage {
			width: 456px;
			height: 186px;
			margin-left:auto;
            margin-right:auto;
            
		}
		.auto-style20 {
			width: 622px;
			height: 1px;
		}
		.auto-style21 {
			width: 622px;
			height: 34px;
		}
		.auto-style22 {
			width: 622px;
		}
       
    	.auto-style23 {
			width: 525px;
			height: 186px;
			margin-left: auto;
			margin-right: auto;
		}
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
       

        <h1 style="text-align:center; font-size: 45px;"class="auto-style13">流水帳管理系統</h1>

        <br />
          
        <table  class="auto-style23">
            <tr>
                <th class="auto-style20" rowspan="1" style="font-size: 26px">初次記帳時間:</th>
                <td class="auto-style4" style="font-size: 26px">
                    &ensp;<asp:Label ID="lblFirstDate" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th class="auto-style20" style="font-size: 26px">&nbsp;</th>
                <td class="auto-style4" style="font-size: 26px">
                    &nbsp;</td>
            </tr>
            <tr>
                <th class="auto-style21" style="font-size: 26px">最後記帳時間:</th>
                <td class="auto-style15" style="font-size: 26px">
                     &ensp;<asp:Label ID="lblLastDate" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th class="auto-style21" style="font-size: 26px">&nbsp;</th>
                <td class="auto-style15" style="font-size: 26px">
                     &nbsp;</td>
            </tr>
            <tr>
                <th class="auto-style22" style="font-size: 26px">帳目數量: </th>
                <td class="auto-style1" style="font-size: 26px">
                     &ensp;<asp:Label ID="lblAccountQuantity" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th class="auto-style22" style="font-size: 26px">&nbsp;</th>
                <td class="auto-style1" style="font-size: 26px">
                     &nbsp;</td>
            </tr>
            <tr>
                <th class="auto-style22" style="font-size: 26px">總會員數:</th>
                <td class="auto-style1" style="font-size: 26px">
                    &ensp;<asp:Label ID="lblUserQuantity" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th class="auto-style22" style="font-size: 26px">&nbsp;</th>
                <td class="auto-style1" style="font-size: 26px">
                    &nbsp;</td>
            </tr>
        </table>
            <br />
            <a href="Login.aspx" aria-checked="undefined" aria-disabled="False" style="padding: 0px; font-size: 30px; background:radial-gradient(ellipse 200% 3000% at center,lightblue,white); background-color:lightblue;">登入系統</a>
            <br />

        
    </form>
</body>
</html>
