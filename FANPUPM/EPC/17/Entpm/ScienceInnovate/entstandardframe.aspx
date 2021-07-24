<%@ Page Language="C#" AutoEventWireup="true" CodeFile="entstandardframe.aspx.cs" Inherits="EntStandardFrame" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>公司企业技术标准</TITLE>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<frameset cols="200,*" frameborder="1" bordercolor="#CADEED" framespacing="2">
		<frame src='EntStandardTree.aspx?PrjCode=<%# PrjCode %>&amp;type=<%# type %>&amp;Levels=<%# Levels %>' width="100%" scrolling="no" frameborder="0" height="100%"
			name="leftFrame">
		<frame src="" name="Project" frameborder="0">
	</frameset>
</HTML>
