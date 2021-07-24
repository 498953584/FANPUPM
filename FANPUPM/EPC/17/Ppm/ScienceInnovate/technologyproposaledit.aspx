<%@ Page Language="C#" AutoEventWireup="true" CodeFile="technologyproposaledit.aspx.cs" Inherits="TechnologyProposalEdit" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>TechnologyProposalEdit</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<base target="_self">
	</head>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
		  <div class="divContent2">
		  <table width="100%">
		       <TR>
					<TD colspan="2" class="divHeader"><FONT face="宋体">
							<asp:Label ID="lb_Surprise" runat="server">Label</asp:Label>评论</FONT></TD>
				</TR>
		  </table>
			<table id="Table1" cellSpacing="0" cellPadding="5px" width="100%" border="0" class="tableContent2">
				
				<TR>
					<TD class="word" width="25%">评论人</TD>
					<TD colspan="3">
						<asp:TextBox ID="txtMan" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="word">评论时间</TD>
					<TD colspan="3"><FONT face="宋体">
							<JWControl:DateBox ID="txtDate" ReadOnly="true" runat="server"></JWControl:DateBox>
							<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></FONT></TD>
				</TR>
				<TR>
					<TD class="word">评论内容</TD>
					<TD colspan="3">
						<asp:TextBox ID="txtContent" TextMode="MultiLine" Width="100%" Height="100px" runat="server"></asp:TextBox></TD>
				</TR>
			</table>
			<div class="divFooter2">
				<table  class="tableFooter2">
				<TR>
					<TD colspan="2">
						<asp:Button ID="btnSave" Text="保 存" OnClick="btnSave_Click" runat="server" /><FONT face="宋体">&nbsp;
						</FONT><input type="button" value="取 消" onclick="javascript:top.frameWorkArea.window.desktop.getActive().close();" id="BtnClose" runat="server" />
</TD>
				</TR>
			   </table>
			  </div>
			</div>
		</form>
	</body>
</HTML>
