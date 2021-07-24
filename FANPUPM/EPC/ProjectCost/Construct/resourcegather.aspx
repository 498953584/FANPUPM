<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resourcegather.aspx.cs" Inherits="ResourceGather" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>施工报量资源消耗汇总</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
	</head>
	<body class="body_popup" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table_form" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td vAlign="top">
						<div class="gridBox" id="dvRelation"><asp:DataGrid ID="dgdRelation" CssClass="grid" CellPadding="0" AutoGenerateColumns="false" Width="100%" runat="server"><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号"></asp:BoundColumn><asp:BoundColumn DataField="ResourceName" HeaderText="资源名称"></asp:BoundColumn><asp:BoundColumn DataField="UnitName" HeaderText="单位"></asp:BoundColumn><asp:BoundColumn DataField="Quantity" HeaderText="使用量" DataFormatString="{0:0.00}"></asp:BoundColumn></Columns></asp:DataGrid></div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
