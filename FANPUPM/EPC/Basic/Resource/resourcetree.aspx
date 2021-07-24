<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resourcetree.aspx.cs" Inherits="ResourceTree" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ResourceTree</title>
		<META http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript">
		<!--
			function  initTree()
			{
				TVResource.onexpand = function()
				{
					if (this.clickedNodeIndex != null)
					{
						if (TVResource.getTreeNode(this.clickedNodeIndex).getAttribute("NodeData")!="1")
						{
							this.queueEvent('onexpand', this.clickedNodeIndex);
							window.setTimeout('__doPostBack(\'TVResource\',\'\')', 0, 'JavaScript')
						}
					}
				}
			}
		// -->
		</script>
	</HEAD>
	<body class="body_frame" scroll="no" onload="initTree();">
		<form id="Form1" method="post" runat="server">
			<input type="hidden" name="__EVENTTARGET"> <input type="hidden" name="__EVENTARGUMENT">
			<script language="javascript" type="text/javascript">
		<!--
			function __doPostBack(eventTarget, eventArgument) {
				var theform;
				if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
					theform = document.Form1;
				}
				else {
					theform = document.forms["Form1"];
				}
				theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
				theform.__EVENTARGUMENT.value = eventArgument;
				theform.submit();
			}
		// -->
			</script>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td>
						<div id="dvHumanBox" class="gridBox">
							<asp:TreeView ID="TVResource" Indent="22" ShowLines="true" selectexpands="True" showtooltip="False" expandlevel="1" Height="100%" Width="200px" ExpandDepth="1" runat="server"></asp:TreeView>
						</div>
					</td>
				</tr>
			</table>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl></form>
	</body>
</HTML>
