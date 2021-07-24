<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resourcetypeframe.aspx.cs" Inherits="ResourceTypeFrame" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Frameset//EN">
<HTML>
	<HEAD>
		<TITLE>设备类型选择</TITLE>
		<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=utf-8">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Expires" content="0">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta http-equiv="Pragma" content="no-cache">
		<base target="_self">
	</HEAD>
	<frameset>
		<FRAMESET cols="30%,70%">
			<FRAME name="ftree" src="ResourceTypeTree.aspx" scrolling="auto" frameborder="1"
				bordercolor="#cccccc">
			<frame name="fitem" src="ResourceTypeItem.aspx?cc=0&t=3vc=">
		</FRAMESET>
		<noframes>
			<pre id="p2">
================================================================
关于完成此标题和内容框架集的说明
1. 为“banner”框架增加 src="" 页的 URL。
2. 为“contents”框架增加 src="" 页的 URL。
3. 为“main”框架增加 src="" 页的 URL。
4. 将 BASE target="main" 元素增加到“contents”页的 
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
