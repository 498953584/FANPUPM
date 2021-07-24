<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DictTree.aspx.cs" Inherits="CodeTypeList" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>��������</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

	<script src="../../../Web_Client/TreeNew.js" type="text/javascript"></script>
	<style>
		.body_clear /*�ıߵ�ҳ�߾�Ϊ0*/
		{
			margin-left: 0px;
			margin-top: 0px;
			margin-right: 0px;
			margin-bottom: 0px;
			font-size: 9pt;
			font-family: ����;
		}
	</style>
</head>
<body class="body_clear" scroll="no" onload="selrow3('TvDictt0')">
	<form id="Form1" method="post" runat="server">
	<table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td>
				<div style="width: 100%; height: 100%; overflow: auto;">
					<asp:TreeView ID="TvDict" Font-Size="12px" ShowLines="true" selectexpands="True" showtooltip="False" expandlevel="1" Height="95%" Width="200px" runat="server"><SelectedNodeStyle BorderWidth="3px" BorderColor="White" BorderStyle="Solid" BackColor="#0099CC" ForeColor="White" /><NodeStyle ForeColor="Black" Font-Names="Tahoma" /></asp:TreeView>
				</div>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
