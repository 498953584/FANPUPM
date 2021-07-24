<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inputreceiptstable.aspx.cs" Inherits="InputReceiptsTable" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>InputReceiptsTable</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<base target="_self">
		<script src="../../../Web_Client/Tree.js"></script>
		<script language="javascript">
		function controlRow()
		{
			var iCount = document.getElementById("hidPrjRowCount").value;
			for(var i=1;i<=iCount;i++)
			{
				//alert(document.getElementById(i).style.display);
				if(document.getElementById(i).style.display =="none")
				{
					document.getElementById(i).style.display ="block";
					document.getElementById(i).style.backgroundColor = "aliceblue";
					//alert(document.getElementById(i).style.display);
				}
				else
					document.getElementById(i).style.display ="none";
			}
		}
		</script>
	</head>
	<body class="body-normal">
		<form id="Form1" method="post" runat="server">
			<table class="table-normal" id="tbReport" bordercolor="gray" cellspacing="0" cellpadding="0" width="100%" border="1" runat="server"><tr runat="server"><td class="td-title" colspan="9" runat="server"><FONT face="宋体"><asp:Label ID="lbPlanYear" runat="server"></asp:Label>年度技术进步收益额及科技开发投入统计表</FONT></td></tr><tr runat="server"><td align="center" height="25" rowspan="2" runat="server">序号</td><td align="center" height="25" rowspan="2" runat="server">单位</td><td align="center" colspan="3" height="25" runat="server">技术进步收益额</td><td align="center" colspan="3" height="25" runat="server">科技开发投入</td><td align="center" height="25" rowspan="2" runat="server">备 注</td></tr><tr runat="server"><td height="22" runat="server">年计划(万元)</td><td align="center" height="22" runat="server">完成量(万元)</td><td align="center" height="22" runat="server">完成年计(%)</td><td align="center" height="22" runat="server">年计划(万元)</td><td align="center" height="22" runat="server">完成量(万元)</td><td align="center" height="22" runat="server">完成年计(%)</td></tr></table>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td align="center" height="10"><input class="button-normal" onclick="window.close();" type="button" value="关  闭"></td>
				</tr>
			</table>
			<input id="hidPrjRowCount" type="hidden" value="0" name="Hidden1" runat="server" />
</form>
	</body>
</HTML>
