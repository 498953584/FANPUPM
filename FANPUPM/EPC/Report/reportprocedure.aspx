<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reportprocedure.aspx.cs" Inherits="ReportProcedure" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>OrderQuery</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript">
			function openQuery(reportid)
			{
				var url= "../Query/ReportQuerySet.aspx?reportid="+reportid;				
				return window.showModalDialog(url,window,"dialogHeight:320px;dialogWidth:460px;center:1;help:0;status:0;");
			}
		</script>
	</head>
	<body class="body_popup" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-normal" height="100%" cellSpacing="0" cellPadding="0" width="100%">
				<tr class="td-search">
					<td height="24">&nbsp;&nbsp;
						<asp:Button ID="btnexecl" Text="导 出" CssClass="button-normal" OnClick="btnexecl_Click" runat="server" />&nbsp;
						<asp:Button ID="btnSearch" CssClass="button-normal" OnClick="btnSearch_Click" runat="server" /></td>
				</tr>
				<TR>
					<TD>
						<table class="table-normal" id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" runat="server"><tr class="td-title" id="TRTitle" bgcolor="activeborder" height="24" runat="server"><td align="center" colspan="3" runat="server"><asp:Label ID="Lb_Title" runat="server"></asp:Label></td></tr><tr class="report_head" id="TrHeader" bgcolor="activeborder" height="24" runat="server"><td align="left" width="33%" runat="server"><asp:Label ID="Lb_H1" runat="server"></asp:Label></td><td align="center" width="33%" runat="server"><asp:Label ID="Lb_H2" runat="server"></asp:Label></td><td align="right" width="33%" runat="server"><asp:Label ID="Lb_H3" runat="server"></asp:Label></td></tr><tr runat="server"><td valign="top" colspan="3" runat="server">
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:DataGrid ID="dgdOrder" CssClass="grid" Width="1000px" runat="server"><AlternatingItemStyle CssClass="grid_row"></AlternatingItemStyle><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" BorderColor="ActiveBorder" CssClass="report_grid_head" BackColor="ActiveBorder"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号"></asp:BoundColumn></Columns></asp:DataGrid></div>
								</td></tr><tr class="report_head" id="TrFooter" bgcolor="activeborder" height="24" runat="server"><td align="left" runat="server"><asp:Label ID="Lb_F1" runat="server"></asp:Label></td><td align="center" runat="server"><asp:Label ID="Lb_F2" runat="server"></asp:Label></td><td align="right" runat="server"><asp:Label ID="Lb_F3" runat="server"></asp:Label></td></tr></table>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
