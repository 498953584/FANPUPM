<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fileview.aspx.cs" Inherits="FileViewWeb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>查看记录</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
	</head>
	<body class="body_frame">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-none" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="1">
				<TR>
					<TD class="td-head" align="center" colSpan="4" height="24">上传文件浏览</TD>
				</TR>
				<TR>
					<TD vAlign="top" height="100%">
						<div class="gridBox"><asp:DataGrid ID="dgFileView" Width="100%" AutoGenerateColumns="false" DataKeyField="Id" CssClass="grid" DESIGNTIMEDRAGDROP="18" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
											<%# Container.ItemIndex + 1 %>
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="title" HeaderText="文件名称"></asp:BoundColumn><asp:BoundColumn DataField="bz" HeaderText="备注"></asp:BoundColumn><asp:BoundColumn DataField="uploaddate" HeaderText="上传日期" DataFormatString="{0:d}"></asp:BoundColumn><asp:HyperLinkColumn Text="查看" Target="_blank" DataNavigateUrlField="urlpath"></asp:HyperLinkColumn></Columns></asp:DataGrid></div>
					</TD>
				</TR>
				<TR>
					<TD class="td-page"><JWControl:PaginationControl ID="PaginationControl1" OnPageIndexChange="PaginationControl1_PageIndexChange" runat="server"></JWControl:PaginationControl><input id="hdnClass" type="hidden" name="hdnClass" runat="server" />
</TD>
				</TR>
			</TABLE>
		</form>
		<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	</body>
</HTML>
