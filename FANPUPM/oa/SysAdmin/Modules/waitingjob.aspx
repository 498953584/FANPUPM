<%@ Page Language="C#" AutoEventWireup="true" CodeFile="waitingjob.aspx.cs" Inherits="WaitingJob" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>待办工作</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" align="center" cellSpacing="1" cellPadding="1" width="100%" border="1"
				class="table-form">
				<TR>
					<TD colspan="2" class="td-head">待办工作设置</TD>
				</TR>
				<TR>
					<TD class="td-label">模块代码</TD>
					<TD>
						<asp:TextBox ID="tbx_mkdm" ReadOnly="true" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="td-label">模块名称</TD>
					<TD>
						<asp:TextBox ID="tbx_title" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="td-label">导航地址</TD>
					<TD>
						<asp:TextBox ID="tbx_url" ReadOnly="true" Width="350px" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="td-label">备注</TD>
					<TD>
						<asp:TextBox ID="tbx_memo" Width="350px" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="td-label">检测的sql语句</TD>
					<TD>
						<asp:TextBox ID="tbx_sql" TextMode="MultiLine" Height="100px" Width="350px" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="td-label">是否有效</TD>
					<TD>
						<asp:RadioButton ID="rdi_Y" GroupName="myradio" Text="有效" runat="server" />
						<asp:RadioButton ID="rdi_N" GroupName="myradio" Text="无效" runat="server" /></TD>
				</TR>
				<TR>
					<TD colSpan="2" class="td-submit">
						<asp:Button Text="保存" ID="btn_Save" OnClick="btn_Save_Click" runat="server" />
						<asp:Button Text="删除" ID="btn_Delete" OnClick="btn_Delete_Click" runat="server" />
						&nbsp;<button onclick="window.close();" type="button" id="btn_Cancel" class="button-normal">关闭</button>
					</TD>
				</TR>
			</TABLE>
			<JWControl:JavaScriptControl ID="JavaScriptControl1" runat="server"></JWControl:JavaScriptControl>
		</form>
	</body>
</HTML>
