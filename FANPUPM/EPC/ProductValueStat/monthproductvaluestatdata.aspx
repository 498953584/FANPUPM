<%@ Page Language="C#" AutoEventWireup="true" CodeFile="monthproductvaluestatdata.aspx.cs" Inherits="MonthProductValueStatData" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>月度产值统计</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Formx" method="post" runat="server">
			<table class="table_form" cellSpacing="0" cellPadding="0" width="100%" border="1" height="100%">
				<tr>
					<td valign="top">
						<div id="dvRelation" class="gridBox">
							<asp:DataGrid ID="DGrdProductValue" Width="100%" AutoGenerateColumns="false" CellPadding="0" CssClass="grid" runat="server"><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="StaticDate" HeaderText="日期" DataFormatString="{0:yyyy年MM月}"></asp:BoundColumn><asp:BoundColumn DataField="Quantity" HeaderText="工程量"></asp:BoundColumn><asp:BoundColumn DataField="MonthOverQuantity" HeaderText="当月完成"></asp:BoundColumn><asp:BoundColumn DataField="AccumulativeQuantity" HeaderText="累计完成"></asp:BoundColumn><asp:BoundColumn DataField="ReportQuantity" HeaderText="上报量"></asp:BoundColumn><asp:BoundColumn DataField="SuperQuantity" HeaderText="监理批量"></asp:BoundColumn><asp:BoundColumn DataField="AccSuperQuantity" HeaderText="累计监理批量"></asp:BoundColumn><asp:BoundColumn DataField="ProductValue" HeaderText="产值"></asp:BoundColumn></Columns></asp:DataGrid></div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
