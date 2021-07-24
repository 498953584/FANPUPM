<%@ Page Language="C#" AutoEventWireup="true" CodeFile="equipmentattemperlist.aspx.cs" Inherits="EquipmentAttemperList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>EquipmentAttemperList</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%"
				border="0">
				<tr>
					<td class="td-title">机械器具调度查询</td>
				</tr>
				<tr>
					<td class="td-search" align="right">开始时间：
						<JWControl:DateBox ID="txtStar" ReadOnly="true" Columns="12" runat="server"></JWControl:DateBox>&nbsp; 
						结束时间：
						<JWControl:DateBox ID="txtEnd" ReadOnly="true" Columns="12" runat="server"></JWControl:DateBox>&nbsp;
						<asp:Button ID="btnSearch" TabIndex="1" CssClass="button-search" OnClick="btnSearch_Click" runat="server" />&nbsp;&nbsp;
					</td>
				</tr>
				<tr>
					<td valign="top"><asp:DataGrid ID="GrdAttemper" CssClass="grid" Width="100%" DataKeyField="NoteSequenceID" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><AlternatingItemStyle Wrap="false" CssClass="grid_row"></AlternatingItemStyle><ItemStyle Wrap="false" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head" VerticalAlign="Middle"></HeaderStyle><Columns><asp:BoundColumn DataField="Equipmentname" HeaderText="机械器具名称"></asp:BoundColumn><asp:BoundColumn DataField="AttemperBillCode" HeaderText="调度单号"></asp:BoundColumn><asp:BoundColumn DataField="ProjectName" HeaderText="调往项目"></asp:BoundColumn><asp:BoundColumn Visible="false" HeaderText="调度类型"></asp:BoundColumn><asp:BoundColumn DataField="Suite" HeaderText="随员"></asp:BoundColumn><asp:BoundColumn DataField="IntendingTime" HeaderText="台班"></asp:BoundColumn><asp:BoundColumn DataField="BeginDate" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="EndDate" HeaderText="结束时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn Visible="false" HeaderText="调度时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
