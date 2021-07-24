<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rolemanage.aspx.cs" Inherits="roleManage" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Frameset//EN">
<HTML>
	<head runat="server"><title>垂直拆分框架集</title><meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
</head>
	<frameset cols="150,*" border="1" frameborder"0" frameSpacing="0">
		<frame name="left" src="roleType.aspx" noResize scrolling="auto">
		<frame name="right" src="roleList.aspx" noResize scrolling="auto">
		<noframes>
			<pre id="p2">
================================================================
关于完成此垂直拆分框架集的说明
1. 为“left”框架增加 src="" 页的 URL。
2. 为“right”框架增加 src="" 页的 URL。
================================================================
</pre>
			<p id="p1">
				此 HTML 框架集显示多个 Web 页。若要查看此框架集，请使用支持 HTML 4.0 及更高版本的 Web 浏览器。
			</p>
		</noframes>
	</frameset>
</HTML>
