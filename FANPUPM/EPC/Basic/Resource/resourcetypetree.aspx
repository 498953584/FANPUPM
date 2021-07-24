<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resourcetypetree.aspx.cs" Inherits="ResourceTypeTree" %>
<%@ Register TagPrefix="ie" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ResourceTree</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Expires" content="0">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta http-equiv="Pragma" content="no-cache">
		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="JavaScript">
		<!--
			function  initTree()
			{
			//	window.alert('ok');
				TVResource.onexpand = function()
				{
					if (this.clickedNodeIndex != null)
					{
						this.queueEvent('onexpand', this.clickedNodeIndex);
						window.setTimeout('__doPostBack(\'TVResource\',\'\')', 0, 'JavaScript')
					}
				}
			}
		// -->
		</script>
	</HEAD>
	<body class="body_frame" onload="initTree();" bottomMargin="2" leftMargin="2" topMargin="2"
		rightMargin="2">
		<form id="Form1" method="post" runat="server">
			<input type="hidden" name="__EVENTTARGET"> <input type="hidden" name="__EVENTARGUMENT">
			<script language="javascript" type="text/javascript">
			<!--
				function __doPostBack(eventTarget, eventArgument)
				{
					var theform;
					if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1)
					{
						theform = document.Form1;
					}
					else
					{
						theform = document.forms["Form1"];
					}
					
					theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
					theform.__EVENTARGUMENT.value = eventArgument;
					
					//window.alert(theform.__EVENTTARGET.value);
					
					theform.submit();
				}
			// -->
			</script>
			<ie:TreeView ID="TVResource" ExpandLevel="1" ShowToolTip="false" Indent="22" runat="server"></ie:TreeView>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			<P></P>
		</form>
	</body>
</HTML>
