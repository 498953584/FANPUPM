<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frame.aspx.cs" Inherits="Frame" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>Frame</TITLE>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<frameset cols="200,78%" frameborder="1" bordercolor="#CADEED" framespacing="2">
		<frame src="PrjTrv.aspx?Url=<%# Url %>&amp;Type=<%# this.Type %>&amp;PrjState=<%# PrjState %>&amp;Levels=<%# Levels %>&amp;WaitingWorkPrjs=<%=Request.QueryString["WaitingWorkPrjs"] %>" width="100%" frameborder="0" height="100%" name="leftFrame">
		
		<frame src="../UserControl1/webTreeTS.aspx" name="mainFrame" frameborder=0>
	</frameset>
</HTML>
