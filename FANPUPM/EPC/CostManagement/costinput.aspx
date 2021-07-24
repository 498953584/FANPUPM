<%@ Page Language="C#" AutoEventWireup="true" CodeFile="costinput.aspx.cs" Inherits="CostInput" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>CostInput</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<frameset rows="81%,19%">
		<frame src="CostInputTop.aspx?PrjCode=<%# this.PrjCode %>&amp;Type=<%# this.Type %>" name="topFrame" scrolling=auto>
		<frame src="CostInputBottom.aspx?Type=<%# this.Type %>" name="bottomFrame" scrolling=auto>
	</frameset>
</HTML>
