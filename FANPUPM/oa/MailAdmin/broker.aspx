<%@ Page Language="C#" AutoEventWireup="true" CodeFile="broker.aspx.cs" Inherits="Broker" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>附件管理</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<SCRIPT language="JavaScript" type="text/JavaScript">
			function confirmHuman()
			{
				var annexList = fraAnnex.Form1.LBoxAnnex;
				var annex=window.dialogArguments;
				for(var i=0;i<annexList.length;i++)
				{
					//human[0][i] = humanList.options[i].value;
					annex[i] = annexList.options[i].text;
				}
				window.returnvalue= annex;
				window.close();
			}
		</SCRIPT>
	</head>
	<BODY scroll="no" onunload="confirmHuman()">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体"><IFRAME name="fraAnnex" id="fraAnnex" src="about:blank" frameborder="0" height="100%" width="100%" scrolling="no" runat="server"></IFRAME></FONT>
		</form>
	</BODY>
</HTML>
