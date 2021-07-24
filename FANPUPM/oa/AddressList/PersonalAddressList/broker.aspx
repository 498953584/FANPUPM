<%@ Page Language="C#" AutoEventWireup="true" CodeFile="broker.aspx.cs" Inherits="Broker" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>Broker</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
			
		<SCRIPT language="javascript">
			function refreshParent()
			{
//				var p = window.dialogArguments;
//				p.location.reload();
			}
		</SCRIPT>
	</head>
	<body onunload=refreshParent();>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体"><IFRAME id="FraOperate" src="about:blank" width="100%" height="100%" frameborder="0" runat="server">
				</IFRAME></FONT>
		</form>
	</body>
</HTML>
