<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loglistmanage.aspx.cs" Inherits="logListManage" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>logListManage</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></head>
	<body>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" align="center" border="0">
					<TR>
						<TD width="100%"><asp:Label ID="headLbl" runat="server"></asp:Label></TD>
					</TR>
					<TR>
						<TD><asp:DataGrid ID="DataGrid1" AllowPaging="true" PageSize="16" AutoGenerateColumns="false" Width="100%" DataKeyField="i_gzrz_id" runat="server"><HeaderStyle HorizontalAlign="Center"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="状态">
<ItemTemplate>
											<asp:Image runat="server" />&nbsp;
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="内容摘要">
<ItemTemplate>
											<asp:HyperLink runat="server"></asp:HyperLink>
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="dtm_zxsj" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="dtm_xgsj" HeaderText="最近修改时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:ButtonColumn Text="剔除" HeaderText="无效剔除" CommandName="Delete"></asp:ButtonColumn></Columns><PagerStyle NextPageText="下一页" PrevPageText="上一页" HorizontalAlign="Right"></PagerStyle></asp:DataGrid></TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
