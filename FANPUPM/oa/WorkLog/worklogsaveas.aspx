<%@ Page Language="C#" AutoEventWireup="true" CodeFile="worklogsaveas.aspx.cs" Inherits="workLogSaveAs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>workLogSaveAs</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="90%" border="0" align="center">
					<TR>
						<TD width="100%">
							<asp:DataGrid ID="dg" AutoGenerateColumns="false" Width="100%" EnableViewState="false" runat="server"><SelectedItemStyle BackColor="Blue"></SelectedItemStyle><HeaderStyle Font-Bold="true" HorizontalAlign="Center"></HeaderStyle><Columns><asp:BoundColumn DataField="dtm_zxsj" HeaderText="撰写时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="txt_rznr" HeaderText="日志内容"></asp:BoundColumn></Columns></asp:DataGrid></TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
