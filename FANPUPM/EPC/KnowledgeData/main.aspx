<%@ Page Language="C#" AutoEventWireup="true" CodeFile="main.aspx.cs" Inherits="Main" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>Main</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
	</head>
		<FRAMESET class="body_frame" frameborder="1" bordercolor="#cccccc" framespacing="3" cols="20%,80%">
		<FRAME name=lframe src="<%=this.strTree %>" width="100%" scrolling=no frameborder=0 height="100%">
			<FRAME name="InfoList" src="<%=this.strRur %>">
		</FRAMESET>
</HTML>
