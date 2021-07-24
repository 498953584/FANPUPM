<%@ Control Language="C#" AutoEventWireup="true" CodeFile="addmodule.ascx.cs" Inherits="AddModule" %>

<table width="100%" border="0">
	<tr>
		<td align="right">模块代码：</td>
		<td>
			<asp:TextBox ID="tbxPreCode" Columns="12" runat="server"></asp:TextBox>
			<asp:TextBox ID="tbxModuleCode" Columns="4" runat="server"></asp:TextBox></td>
	</tr>
	<tr>
		<td align="right" width="100">模块名称：</td>
		<td>
			<asp:TextBox ID="tbxModuleName" Columns="30" runat="server"></asp:TextBox></td>
	</tr>
	<tr>
		<td align="right">序号：</td>
		<td>
			<asp:TextBox ID="tbxOrder" runat="server"></asp:TextBox></td>
	</tr>
	<tr>
		<td align="right">菜单路径：</td>
		<td>
			<asp:TextBox ID="tbxPath" Columns="45" runat="server"></asp:TextBox></td>
	</tr>
	<tr>
		<td align="right">对应图标：</td>
		<td>
			<asp:TextBox ID="tbxIcon" Columns="45" runat="server"></asp:TextBox></td>
	</tr>
</table>
