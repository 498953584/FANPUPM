<%@ Page Language="C#" AutoEventWireup="true" CodeFile="quantitygather.aspx.cs" Inherits="QuantityGather" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>查看工程量</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
	</head>
	<body class="body_popup" scroll="no">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<table class="table_form" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td vAlign="top">
							<div class="gridBox" id="dvRelation">
								<asp:DataGrid ID="dgTaskList" AutoGenerateColumns="false" CssClass="grid" Width="100%" runat="server"><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号"></asp:BoundColumn><asp:BoundColumn DataField="TaskName" HeaderText="分项工程"></asp:BoundColumn><asp:BoundColumn DataField="TotalQuantity" HeaderText="总工程量"></asp:BoundColumn><asp:BoundColumn DataField="LeavlQuantity" HeaderText="剩余工程量"></asp:BoundColumn><asp:BoundColumn DataField="TodayComplete" HeaderText="本期报量"></asp:BoundColumn></Columns></asp:DataGrid></div>
						</td>
					</tr>
				</table>
			</FONT>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
		</form>
	</body>
</HTML>
