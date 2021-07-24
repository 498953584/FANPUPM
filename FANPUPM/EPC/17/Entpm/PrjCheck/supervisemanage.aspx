<%@ Page Language="C#" AutoEventWireup="true" CodeFile="supervisemanage.aspx.cs" Inherits="SuperviseManage" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>项目监察</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="0" />

	<!-- 清除页面缓存-->
	<base target="_self">
	
</head>
<body scroll="no">
	<form id="Form1" method="post" runat="server">
	<div class="divContent2">
		<table width="100%">
			<tr>
				<td class="divHeader" align="center" colspan="4">
					效能监察建议书
				</td>
			</tr>
		</table>
		<table class="tableContent2" cellspacing="0" cellpadding="5px" width="100%" border="0">
			<tr>
				<td class="word" style="white-space: nowrap;">
					项目名称
				</td>
				<td colspan="3" class="txt">
					<asp:TextBox ID="TextBox_prjname" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					立项单位
				</td>
				<td colspan="3" class="txt">
					<asp:TextBox ID="TextBox_lxdw" MaxLength="200" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					立项时间
				</td>
				<td colspan="3" class="txt">
					<JWControl:DateBox ID="DateBox_lxsj" runat="server"></JWControl:DateBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					立项名称
				</td>
				<td colspan="3" class="txt">
					<asp:TextBox ID="TextBox_lxmc" MaxLength="300" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					立项案由
				</td>
				<td colspan="3" class="txt">
					<asp:TextBox ID="TextBox_lxay" MaxLength="500" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					领导批示
				</td>
				<td colspan="3" class="txt">
					<asp:TextBox ID="TextBox_ldps" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					结项时间
				</td>
				<td colspan="3" class="txt">
					<JWControl:DateBox ID="DateBox_jxsj" runat="server"></JWControl:DateBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					监察效能
				</td>
				<td colspan="3" class="txt">
					<asp:TextBox ID="TextBox_jcxn" MaxLength="500" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					监察建议
				</td>
				<td colspan="3" class="txt">
					<asp:TextBox ID="TextBox_jcjy" MaxLength="300" TextMode="MultiLine" Height="120px" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					整改措施及效果
				</td>
				<td colspan="3" class="txt">
					<asp:TextBox ID="TextBox_zgcs" MaxLength="300" TextMode="MultiLine" Height="120px" runat="server"></asp:TextBox>
				</td>
			</tr>
		</table>
		<div class="divFooter2">
			<table class="tableFooter2">
				<tr>
					<td>
						<asp:Button ID="Button_save" Text="保  存" OnClick="Button_save_Click" runat="server" />&nbsp;
						<input id="Button1" name="butclose" onclick="top.ui.closeTab();" type="button" value="取 消" runat="server" />
&nbsp;
					</td>
				</tr>
			</table>
		</div>
		<JWControl:JavaScriptControl ID="JavaScriptControl1" runat="server"></JWControl:JavaScriptControl>
	</div>
	</form>
</body>
</html>
