<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AccountingNote.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登入</title>
    
	<style type="text/css">
		.auto-style1 {
			height: 700px;
			width: 1706px;
			margin-left: 0px;
			margin-right: 542px;
			margin-top: 1px;
			margin-bottom: 0px;
		}
		.auto-style2 {
			margin-top: 0px;
		}
		.auto-style3 {
			height: 366px;
			width: 1484px;
		}
	</style>
    
</head>
<body style="height: 556px">
    <div style="text-align: center; background:radial-gradient(ellipse 500% 50% at center,lightblue,white);" class="auto-style1">

    <form id="form1" runat="server" class="auto-style3">
        <br/>
    <asp:Label ID="Label1" runat="server" Text="登入頁面" Font-Size="XX-Large" BorderColor="White" ForeColor="Black" BorderStyle="None" Font-Bold="True" Font-Italic="False" CssClass="auto-style2" Height="41px" Width="185px"></asp:Label>
    	<br />
    
    <br />
    
            <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
       
    	<br />

            <br /> 

        <asp:PlaceHolder ID="plcLogin" runat="server" Visible="false">

            <asp:Label ID="Label2" runat="server" Text="帳號:" ></asp:Label>&ensp;<asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
            <br /><br />
            <asp:Label ID="Label3" runat="server" Text="密碼:"></asp:Label>&ensp;<asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
            <br />

             </asp:PlaceHolder>
		<br /> 
       
		<br />
       
    	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       
    	<asp:Button ID="btnLogin" runat="server" Text="登入" OnClick="btnLogin_Click" ToolTip="登入系統" Height="40px" Width="82px" Font-Bold="True" Font-Names="Berlin Sans FB" UseSubmitBehavior="False" Font-Size="Large" />
       
    </form>
        </div>
</body>
</html>
