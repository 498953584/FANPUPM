<%@ Page Language="C#" AutoEventWireup="true" CodeFile="productvalueplanfrm.aspx.cs" Inherits="ProductValuePlanFrm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ProductValuePlanFrm</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
			function MonthProductValueEdit(type,month,sf,projectCode)			
			{
				//type操作类型 编辑/查看 month年月份 projectCode项目编号 sf 统计操作/填报操作
				switch(sf)
				{
					case 1:
						self.location.href = "ProductValueStat.aspx?t="+type+"&m="+month+"&pc="+projectCode+"&IsExam="+'<%=Request["t"] %>';
						break;
					case 2:
						self.location.href = "ProductValueMonthPlan.aspx?t="+type+"&m="+month+"&pc="+projectCode+"&IsExam="+'<%=Request["t"] %>';
						break;
				}
				return false;
			}
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Formx" method="post" runat="server">
			<input id="HdnProjectCode" style="WIDTH: 10px" type="hidden" name="HdnProjectCode" runat="server" />

			<table style="TABLE-LAYOUT: fixed" height="100%" cellSpacing="0" cellPadding="0" width="100%">
				<tr class="td-toolsbar">
					<td height="20" align="left">
						<asp:Label ID="LblMsg" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td vAlign="top">
						<div class="gridBox"><asp:DataGrid ID="DGrdMonthProductValue" AutoGenerateColumns="false" Width="100%" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn HeaderText="日期"></asp:BoundColumn><asp:TemplateColumn HeaderText="产值月度统计">
<ItemTemplate>
											<asp:LinkButton ID="LBtnStatWeave" Text="编  制" CommandName="LBtnStatWeave" runat="server"></asp:LinkButton>
											<asp:LinkButton ID="LBtnStatEdit" Visible="false" Text="编  辑" CommandName="LBtnStatEdit" runat="server"></asp:LinkButton>
											<asp:LinkButton ID="LBtnStatDel" Visible="false" Text="删  除" CommandName="LBtnStatDel" runat="server"></asp:LinkButton>
											<asp:LinkButton ID="LBtnStatView" Visible="false" Text="查  看" CommandName="LBtnStatView" runat="server"></asp:LinkButton>
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="产值月度计划">
<ItemTemplate>
											<asp:LinkButton ID="LBtnReprotWeave" Text="编  制" CommandName="LBtnReprotWeave" runat="server"></asp:LinkButton>
											<asp:LinkButton ID="LBtnReprotEdit" Visible="false" Text="编  辑" CommandName="LBtnReprotEdit" runat="server"></asp:LinkButton>
											<asp:LinkButton ID="LBtnReprotDel" Visible="false" Text="删  除" CommandName="LBtnReprotDel" runat="server"></asp:LinkButton>
											<asp:LinkButton ID="LBtnReprotView" Visible="false" Text="查  看" CommandName="LBtnReprotView" runat="server"></asp:LinkButton>
											<asp:LinkButton ID="LBtnReprotExamine" Visible="false" Text="审  核" CommandName="LBtnReprotExamine" runat="server"></asp:LinkButton>
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn HeaderText="计划产值汇总"></asp:BoundColumn></Columns></asp:DataGrid></div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
