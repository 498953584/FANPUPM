<%@ Page Language="C#" AutoEventWireup="true" CodeFile="delloglist.aspx.cs" Inherits="delloglist" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>超级删除信息列表</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></head>
	<body>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE class="table-nomal" id="Table1" cellSpacing="1" cellPadding="1" width="95%" align="center"
					border="1">
					<TR>
						<TD align="center" class="td-title"><asp:Label ID="Label1" runat="server">Label</asp:Label></TD>
					</TR>
					<TR>
						<TD><asp:DataGrid ID="dgLogList" AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center" CellPadding="4" PageSize="20" AllowPaging="true" runat="server"><AlternatingItemStyle HorizontalAlign="Center" BackColor="Gainsboro"></AlternatingItemStyle><ItemStyle HorizontalAlign="Center"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><FooterStyle HorizontalAlign="Right"></FooterStyle><Columns><asp:BoundColumn DataField="记录ID" HeaderText="记录ID"></asp:BoundColumn><asp:BoundColumn DataField="yhmc" HeaderText="删除人"></asp:BoundColumn><asp:BoundColumn DataField="ywmk" HeaderText="业务模块"></asp:BoundColumn><asp:BoundColumn DataField="删除时间" HeaderText="删除时间"></asp:BoundColumn></Columns><PagerStyle NextPageText="下一页" PrevPageText="上一页" HorizontalAlign="Right" Position="TopAndBottom" Wrap="false" Mode="NumericPages"></PagerStyle></asp:DataGrid></TD>
					</TR>
					<tr>
						<td align="right">
							<asp:Button ID="btnDel" Text=" 删除 " CssClass="button-normal" OnClick="btnDel_Click" runat="server" />
							<JWControl:DateBox ID="tbDate" runat="server"></JWControl:DateBox>以前的记录</td>
					</tr>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
