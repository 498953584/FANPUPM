<%@ Page Language="C#" AutoEventWireup="true" CodeFile="repairsearchtop.aspx.cs" Inherits="RepairLook" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>RepairLook</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
			function clickRow(maintainCode)
			{
				var url;
				url = "MaintainView.aspx?mc=" + maintainCode ;
				return window.showModalDialog(url,window,'dialogHeight: 400px; dialogWidth: 600px; center: Yes; help: No; resizable: No; status: No;');
			}
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form2" method="post" runat="server">
			<table id="Table1" cellspacing="1" cellpadding="1" width="100%" height="100%" border="1">
				<tr>
					<td class="td-title">机械器具维修列表<input id="hdnGoWhere" type="hidden" name="hdnGoWhere" style="WIDTH:1px" runat="server" />
</td>
				</tr>
				<tr>
					<td class="td-search">开始时间：
						<JWControl:DateBox ID="dtxtStartDate" Columns="12" runat="server"></JWControl:DateBox>&nbsp;&nbsp;结束时间：
						<JWControl:DateBox ID="dtxtEndDate" Columns="12" runat="server"></JWControl:DateBox>&nbsp;&nbsp;
						<asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" /></td>
				</tr>
				<tr>
					<td valign="top">
						<div style="OVERFLOW:auto;WIDTH:100%;HEIGHT:100%">
							<asp:DataGrid ID="dgdRepairRecord" AutoGenerateColumns="false" Width="100%" DataKeyField="MaintainUniqueCode" CssClass="grid" AllowPaging="true" PageSize="20" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><AlternatingItemStyle CssClass="grid_row"></AlternatingItemStyle><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="EquipmentName" ReadOnly="true" HeaderText="机械器具名称"></asp:BoundColumn><asp:BoundColumn DataField="MaintainType" ReadOnly="true" HeaderText="维修类型"></asp:BoundColumn><asp:BoundColumn DataField="MaintainCost" ReadOnly="true" HeaderText="维修费用"></asp:BoundColumn><asp:BoundColumn DataField="MaintainceMan" ReadOnly="true" HeaderText="维修人员"></asp:BoundColumn><asp:BoundColumn DataField="MaintainDate" ReadOnly="true" HeaderText="维修时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="Appraiser" ReadOnly="true" HeaderText="鉴定人员"></asp:BoundColumn><asp:BoundColumn DataField="AppraisalDate" ReadOnly="true" HeaderText="鉴定时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
