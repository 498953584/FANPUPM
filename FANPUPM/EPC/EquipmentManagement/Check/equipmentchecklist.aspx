<%@ Page Language="C#" AutoEventWireup="true" CodeFile="equipmentchecklist.aspx.cs" Inherits="EquipmentCheckList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
  <head runat="server"><title>EquipmentCheckList</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
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
				border="1">
				<tr>
					<td class="td-title">机械器具检定查询</td>
				</tr>
				<tr>
					<td class="td-search" align="right">检定类型：
						<asp:DropDownList ID="DropDownList1" runat="server"><asp:ListItem Value="2" Selected="true" Text="全部" /><asp:ListItem Value="1" Text="内检" /><asp:ListItem Value="0" Text="外检" /></asp:DropDownList>&nbsp;检定时间：从
						<JWControl:DateBox ID="txtStar" runat="server"></JWControl:DateBox>&nbsp; 到
						<JWControl:DateBox ID="txtEnd" runat="server"></JWControl:DateBox>&nbsp;
						<asp:Button ID="btnSearch" TabIndex="1" CssClass="button-search" OnClick="btnSearch_Click" runat="server" />&nbsp;&nbsp;
					</td>
				</tr>
				<tr>
					<td valign="top"><asp:DataGrid ID="GrdEquipmentCheckup" CssClass="grid" Width="100%" DataKeyField="NoteSequenceID" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><AlternatingItemStyle Wrap="false" CssClass="grid_row"></AlternatingItemStyle><ItemStyle Wrap="false" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head" VerticalAlign="Middle"></HeaderStyle><Columns><asp:BoundColumn DataField="EquipmentName" HeaderText="机械器具名称"></asp:BoundColumn><asp:BoundColumn DataField="CheckBillCode" HeaderText="检定编号"></asp:BoundColumn><asp:BoundColumn DataField="CheckDate" HeaderText="检定日期" DataFormatString="{0:d}"></asp:BoundColumn><asp:TemplateColumn HeaderText="检定类型">
<ItemTemplate>
										<asp:Label Text='<%# Convert.ToString(Eval("InOrOut").Equals(1) ? "外检" : "内检") %>' runat="server"></asp:Label>
									</ItemTemplate>

<EditItemTemplate>
										<asp:TextBox Text='<%# Convert.ToString(Eval("InOrOut").Equals(1) ? "外检" : "内检") %>' runat="server"></asp:TextBox>
									</EditItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="CheckDept" HeaderText="检定单位"></asp:BoundColumn><asp:BoundColumn DataField="CheckPerson" HeaderText="检定人"></asp:BoundColumn><asp:TemplateColumn HeaderText="检定结果">
<ItemTemplate>
										<asp:Label Text='<%# Convert.ToString(Eval("Result").Equals(1) ? "合格" : "不合格") %>' runat="server"></asp:Label>
									</ItemTemplate>

<EditItemTemplate>
										<asp:TextBox Text='<%# Convert.ToString(Eval("Result").Equals(1) ? "合格" : "不合格") %>' runat="server"></asp:TextBox>
									</EditItemTemplate>
</asp:TemplateColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></td>
				</tr>
			</table>
		</form>
	</body>
</html>
