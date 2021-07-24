<%@ Page Language="C#" AutoEventWireup="true" CodeFile="broker.aspx.cs" Inherits="Broker" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server"><title>Broker</title><meta Name="vs_snapToGrid" Content="True" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
	<script language="JavaScript">
	    function refreshParent() {
	        //			var p = window.dialogArguments;
	        //			p.location.reload();
	    }
	</script>
</head>
<body onunload="refreshParent();">
	<form id="Form1" method="post" runat="server">
	<font face="宋体">
		<iframe id="FraOperate" src="about:blank" width="100%" height="95%" frameborder="0" runat="server"></iframe>
	</font>
	</form>
</body>
</html>
