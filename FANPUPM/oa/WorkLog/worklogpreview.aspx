<%@ Page Language="C#" AutoEventWireup="true" CodeFile="worklogpreview.aspx.cs" Inherits="workLogPreview" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>workLogPreview</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
</head>	
	<body>
		<form id="Form1" method="post" runat="server">
			<table class="table-none" width="50%" align="center" border="0">
				<tr>
					<td align="center" colSpan="4"><IMG height="27" src="img/diary.gif" width="245"></td>
				</tr>
				<tr bgColor="#eeeeee">
					<td align="right" width="20%">日期：</td>
					<td align="left" width="30%"><asp:Label ID="dateLbl" BackColor="White" runat="server"></asp:Label></td>
					<td align="right" width="20%">天气：</td>
					<td align="left" width="30%"><asp:Label ID="weatherLbl" BackColor="White" runat="server"></asp:Label></td>
				</tr>
				<tr bgColor="#eeeeee">
					<td align="center" colSpan="4"><asp:TextBox ID="logContent" style="BACKGROUND-IMAGE: url(img/Txt_bg.jpg); OVERFLOW: auto; LINE-HEIGHT: 1.25" Enabled="false" TextMode="MultiLine" Rows="15" Wrap="true" BorderStyle="None" Width="450px" Height="300px" runat="server"></asp:TextBox></td>
				</tr>
				<tr bgColor="#eeeeee">
					<td align="right" colSpan="3">撰写人：</td>
					<td align="left"><asp:Label ID="writerLbl" BackColor="White" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td colspan="4" align="right">
						<input type="button" class="button-normal" value=" 返回 " onclick="javascript:window.history.go(-1);" id="btnBack" name="btnBack" runat="server" />

					</td>
				</tr>
			</table>
		</form>
	</body>
	
</HTML>
