<%@ Page Language="C#" AutoEventWireup="true" CodeFile="workloglist.aspx.cs" Inherits="workLogList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>workLogList</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
</head>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<DIV align="center"><IMG height="27" src="img/diary.gif" width="245"></DIV>
				<BR>
				<TABLE width="75%" align="center">
					<TR>
						<TD align="right" colspan="2">从
							<asp:TextBox ID="startDate" Width="100px" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="startDate" ErrorMessage="*" Display="Dynamic" runat="server"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeValidator1" ControlToValidate="startDate" ErrorMessage="无效日期" Type="Date" MinimumValue="1900-01-1" MaximumValue="9999-12-31" Display="Dynamic" runat="server"></asp:RangeValidator><input id="searchFlag" type="hidden" size="1" value="flase" name="serchFlag" runat="server" />

							到
							<asp:TextBox ID="endDate" Width="100px" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="endDate" ErrorMessage="*" Display="Dynamic" runat="server"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeValidator2" ControlToValidate="endDate" ErrorMessage="无效日期" Type="Date" MinimumValue="1900-01-1" MaximumValue="9999-12-31" Display="Dynamic" runat="server"></asp:RangeValidator>
							<asp:Button ID="btnSearch" Text="查询" CssClass="button-normal" OnClick="btnSearch_Click" runat="server" /><input id="hidStart" type="hidden" name="hidStart" runat="server" />

							<input id="hidEnd" type="hidden" name="hidEnd" runat="server" />
</TD>
					</TR>
					<TR>
						<TD align="left" colspan="2"><A oncontextmenu="javascript:window.event.cancelBubble = true;" onclick="javascript:return false;"
								href="workLogSaveAs.aspx">保存本人日志到本地(右键另存为，将文件名更改为*.htm即可)</A>
						</TD>
					</TR>
					<TR>
						<TD width="100%" colspan="2"><asp:DataGrid ID="DataGrid1" Width="100%" AutoGenerateColumns="false" DataKeyField="dtm_zxsj" PageSize="16" AllowPaging="true" CellPadding="5" runat="server"><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="内容摘要">
<ItemTemplate>
											<asp:HyperLink ID="HyperLink1" NavigateUrl='<%# Convert.ToString(Eval("i_gzrz_id", "workLogPreview.aspx?id={0}")) %>' runat="server"><%# Convert.ToString(base.truncString(Eval("txt_rznr").ToString().Trim(), 50)) %></asp:HyperLink>
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="操作命令">
<ItemTemplate>
											<asp:HyperLink ID="HyperLink2" NavigateUrl='<%# Convert.ToString(Eval("i_gzrz_id", "workLogUpdate.aspx?id={0}")) %>' runat="server">已归档
											</asp:HyperLink>
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="dtm_zxsj" HeaderText="撰写时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="dtm_xgsj" HeaderText="修改时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn></Columns><PagerStyle NextPageText="下一页" PrevPageText="上一页" HorizontalAlign="Right"></PagerStyle></asp:DataGrid></TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</BODY>
</HTML>
