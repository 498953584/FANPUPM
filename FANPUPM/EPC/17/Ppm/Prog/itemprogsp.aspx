<%@ Page Language="C#" AutoEventWireup="true" CodeFile="itemprogsp.aspx.cs" Inherits="ItemProgSp" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>项目奖罚审核</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="0" />
 <!-- 清除页面缓存--><base target="_self">
	</head>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
		<div class="divContent2">
		    <table width="100%">
		      <tr>
					<td class="divHeader" align="center" colSpan="5">项目奖罚审核</td>
				</tr>
		    </table>
			<table class="tableContent2" cellSpacing="0" cellPadding="5px" width="100%" border="0">
				
				<tr>
					<td class="word" style="white-space:nowrap">
                        审核人</td>
					<td class="txt" colspan="3"><asp:TextBox ID="TextBox_spr" MaxLength="20" runat="server"></asp:TextBox></td>
					
				</tr>
				<tr>
				<td class="word" style="white-space:nowrap">
                    是否同意</td>
				<td class="txt" colspan="3"><asp:RadioButton ID="RadioButton_ok" Text="通过" GroupName="gr1" Checked="true" BorderStyle="None" runat="server" /><asp:RadioButton ID="RadioButton_no" Text="不通过" GroupName="gr1" BorderStyle="None" runat="server" /></td>
				</tr>
				<tr>
				<td class="word" style="white-space:nowrap">
                    审核日期</td>
				<td class="txt" colspan="3"><JWControl:DateBox ID="DateBox_sprq" runat="server"></JWControl:DateBox></td>
				</tr>
				
				<tr>
					<td class="word" style="white-space:nowrap">
                        审核意见</td>
					<td class="txt" colspan="3"><asp:TextBox ID="TextBox_spyj" TextMode="MultiLine" Height="72px" MaxLength="500" runat="server"></asp:TextBox></td>
				</tr>
				
			</table>
	     <div class="divFooter2">
			 <table class="tableFooter2">
			<tr>
					<td>
						<asp:Button ID="Button_save" Text="保 存" OnClick="Button_save_Click" runat="server" />&nbsp;
						<input type="button" onclick='javascript:top.frameWorkArea.window.desktop.getActive().close();' value='取 消'>&nbsp;&nbsp;&nbsp;</td>
				</tr>
			</table>
			</div>
			<JWControl:JavaScriptControl ID="JavaScriptControl1" runat="server"></JWControl:JavaScriptControl>
		  </div>
		</form>
	</body>
</HTML>
