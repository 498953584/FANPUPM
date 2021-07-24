<%@ Page Language="C#" AutoEventWireup="true" CodeFile="main.aspx.cs" Inherits="WebForm9" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title></title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Expires" content="0" />

		<script language="javascript" src="../../../Web_Client/Tree.js"></script>
		<style>TD { FONT-WEIGHT: bolder }
		</style>
		<script language="javascript">
		// 备注：切换图片的规则
		// 正常显示：menu+模块名+.gif
		// 滑过显示：menu+模块名+bg.gif
		var imageID = "_image";			
		var fontID = "_font";
		
		function fontOver(obj,moduleID)
		{
			obj.style.color = "#002eb6";
			//obj.style.margin = "1px";
			//obj.style.padding = "1px";
			//obj.style.border = "2px outset";
			var tempObj = document.getElementById(moduleID+imageID);
			if( tempObj != null )
			{
				tempObj.style.margin = "1px";
				tempObj.style.padding = "1px";
				tempObj.style.border = "2px outset";
				//tempObj.style.filter = "progid:DXImageTransform.Microsoft.Glow(Color=blue,Strength=5);";
			}
		}
		
		function fontOut( obj,moduleID )
		{
			obj.style.color = "#000000";
			//obj.style.margin = "0px";
			//obj.style.padding = "0px";
			//obj.style.border = "0px none";
			var tempObj = document.getElementById(moduleID+imageID);
			if( tempObj != null )
			{
				//tempObj.style.filter = "";
				tempObj.style.margin = "0px";
				tempObj.style.padding = "0px";
				tempObj.style.border = "0px none";
			}
		}
		
		function imageOver( obj,moduleID )
		{
			//obj.style.filter = "progid:DXImageTransform.Microsoft.Glow(Color=blue,Strength=5);";
			obj.style.margin = "1px";
			obj.style.padding = "5px";
			obj.style.border = "2px outset";
			//obj.parentElement.innerHTML="<div style=\"width:10px;PADDING:5px 5px 5px 5px; BORDER-TOP: green 1px solid;BORDER-RIGHT: green 1px solid;BORDER-BOTTOM: green 1px solid;BORDER-LEFT: green 1px solid;\">"+obj.parentElement.innerHTML+"</div>";
			
			var tempObj = document.getElementById(moduleID+fontID);
			if( tempObj != null )
			{
				tempObj.style.color = "#002eb6";
				//tempObj.style.margin = "1px";
				//tempObj.style.padding = "1px";
				//tempObj.style.border = "2px outset";
			}
		}
		
		function imageOut( obj,moduleID )
		{
//			obj.style.filter = "";
			obj.style.margin = "0px";
			obj.style.padding = "0px";
			obj.style.border = "0px none";
			//obj.parentElement.parentElement.innerHTML=obj.parentElement.Children[0].innerHTML;
			var tempObj = document.getElementById(moduleID+fontID);
			if( tempObj != null )
			{
				tempObj.style.color = "#000000";
				//tempObj.style.margin = "0px";
				//tempObj.style.padding = "0px";
				//tempObj.style.border = "0px none";
			}
		}
		
		function goWhere(id,url)
		{
			if (url.length==0)
			{
				top.NavigationMenu.location = 'NavigationMenu.aspx?id=' + id;
				top.frameWorkArea.location = 'main.aspx?id=' + id;
			}
			else
			{
				top.NavigationMenu.location = 'NavigationMenu.aspx?id=' + id;
				top.frameWorkArea.location = url;
			}
		}
		</script>
	</head>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="yes" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体"></FONT>
			<br>
			<table cellSpacing="0" cellPadding="0" width="90%" align="center" border="0">
				<tr>
					<td width="54"><IMG height="54" alt="" src="images/middle_tab_3.gif" width="54" border="0" name="middle_tab_3"></td>
					<td width="98%" background="images/middle_tab_4.gif"><strong>&nbsp;<%=this.str_title %></strong></td>
					<td width="8">
						<table cellSpacing="0" cellPadding="0" width="7" align="left" border="0">
							<tr>
								<td><IMG height="1" alt="" src="images/middle_tab_5.gif" width="7" border="0" name="middle_tab_5"></td>
							</tr>
							<tr>
								<td><IMG height="53" alt="" src="images/middle_tab_6.gif" width="7" border="0" name="middle_tab_6"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td width="54" background="images/middle_tab_8.gif"></td>
					<td height="20"></td>
					<td background="images/middle_tab_10.gif"></td>
				</tr>
				<tr>
					<td width="54" background="images/middle_tab_8.gif"></td>
					<td align="center">
						<table id="table_Navigate" cellspacing="0" cols="4" cellpadding="0" width="98%" align="center" border="0" valign="top" runat="server"><tr runat="server"><td Width="25%" runat="server"></td><td Width="25%" runat="server"></td><td Width="25%" runat="server"></td><td Width="25%" runat="server"></td></tr></table>
					</td>
					<td background="images/middle_tab_10.gif"></td>
				</tr>
				<tr>
					<td width="54" background="images/middle_tab_8.gif"></td>
					<td height="30"></td>
					<td background="images/middle_tab_10.gif"></td>
				</tr>
				<tr>
					<td width="54" height="7"><IMG height="7" alt="" src="images/middle_tab_12.gif" width="54" border="0" name="middle_tab_12"></td>
					<td height="7" background="images/middle_tab_13.gif"></td>
					<td height="7"><img name="middle_tab_14" src="images/middle_tab_14.gif" width="7" height="7" border="0"
							alt=""></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
