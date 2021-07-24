<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowFrame.aspx.cs" Inherits="FlowFrame" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>FlowFrame</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table3" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
				<tr height="50%">
					<td vAlign="top">
						<div class="gridBox"><iframe id="fraTemplate" name="fraTemplate" src="TemplateList.aspx" frameBorder="0" width="100%" scrolling="no" height="100%"></iframe></div>
					</td>
				</tr>
				<tr>
					<td vAlign="top" width="87%"><iframe id="fraNode" name="fraNode" src="" frameBorder="0" width="100%" scrolling="no" height="100%"></iframe>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
