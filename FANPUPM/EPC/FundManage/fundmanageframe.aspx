<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fundmanageframe.aspx.cs" Inherits="FundManageFrame" %>

<HTML>
	<HEAD>
		<TITLE>内容框架集</TITLE>
		<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=utf-8">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Expires" content="0">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta http-equiv="Pragma" content="no-cache">
	</HEAD>
	<frameset cols="25%,75%" frameborder="1" bordercolor="#cccccc" framespacing="3">
		<frame name="ProjectTree" src="ProjectTree.aspx?pt=<%=Request["pt"] %>&Type=<%=Request["Type"] %>"  frameborder="0">
		<frame name="List" src="<%=this.strUrl %>" frameborder="0">
		<noframes>
			<pre id="p2">
================================================================
关于完成此内容框架集的说明
1. 为“contents”框架增加 src="" 页的 URL。
2. 为“main”框架增加 src="" 页的 URL。
3. 将 BASE target="main" 元素增加到“contents”页的 
	HEAD，以将“main”设置为默认框架，“contents”页的链接将
	在该框架中显示其他页。
================================================================
</pre>
			<p id="p1">
				此 HTML 框架集显示多个 Web 页。若要查看此框架集，请使用支持 HTML 4.0 及更高版本的 Web 浏览器。
			</p>
		</noframes>
	</frameset>
</HTML>
