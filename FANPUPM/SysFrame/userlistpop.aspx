<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userlistpop.aspx.cs" Inherits="UserListPop" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>当前用户列表</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Expires" content="0" />

		<script language="javascript" src="../web_client/tree.js" type="text/javascript"></script>
	</head>
	<body scroll="no" class="body_popup">
		<form id="Form1" method="post" runat="server">
			<TABLE  class="table-none" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="td-head">当前用户列表</TD>
				</TR>
				<TR>
					<TD width="100%" valign=top>
						 <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
							<asp:DataGrid ID="grdUserList" CssClass="grid" AutoGenerateColumns="false" Width="100%" EnableViewState="false" CellPadding="0" PageSize="5" runat="server"><ItemStyle Wrap="false" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head" VerticalAlign="Middle"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号"></asp:BoundColumn><asp:BoundColumn DataField="loginID" HeaderText="登陆名称"></asp:BoundColumn><asp:BoundColumn DataField="loginTime" HeaderText="登录时间"></asp:BoundColumn><asp:BoundColumn DataField="ip" HeaderText="用户地址"></asp:BoundColumn></Columns></asp:DataGrid></div>
					</TD>
				</TR>
				<TR class=td-submit>
					<TD align="center"><input class="button-normal" onclick="top.ui.closeWin();" type="button" value="关  闭"></TD>
				</TR>
			</TABLE>
			<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></form>
	</body>
</HTML>
