<%@ Page Language="C#" AutoEventWireup="true" CodeFile="monthcostapprove.aspx.cs" Inherits="MonthCostApprove" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ResourcePlanGather</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
			function OpenCostApprove(type,month,projectCode)			
			{
				//type操作类型 核算/分摊 month年月份 projectCode项目编号
				switch(type)
				{
					case "Approve":
						window.location.href = "CostApprove.aspx?m="+month+"&pc="+projectCode;
						break;
					case "Approtion":
						window.location.href = "CostApportion.aspx?m="+month+"&pc="+projectCode;
						break;
					case "JobOut":
						window.location.href = "CostJobOut.aspx?m="+month+"&pc="+projectCode;
						break;
				}
				return false;
			}
			function OpenResourcePlanWin(type)
			{
				switch(type)
				{
					case "BranchSub":
						window.parent.self.location = "BranchSubResourcePlan.aspx";
						break;
					case "ResGather":
						window.parent.self.location = "ResourcePlan.aspx";
						break;
				}
				return false;
			}
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Formx" method="post" runat="server">
			<TABLE id="Tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<DIV class="gridBox">
							<asp:DataGrid ID="DGrdMonthPlan" Width="100%" AutoGenerateColumns="false" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号"></asp:BoundColumn><asp:BoundColumn HeaderText="时间"></asp:BoundColumn><asp:TemplateColumn HeaderText="核算成本">
<ItemTemplate>
											<asp:LinkButton ID="LBtnApprove" CommandName="LBtnApprove" Text="核  算" runat="server"></asp:LinkButton>
											<asp:LinkButton ID="LBtnApproveDel" CommandName="LBtnApproveDel" Text="删  除" runat="server"></asp:LinkButton>
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="其它成本">
<ItemTemplate>
											<asp:LinkButton ID="LBtnApprotion" CommandName="LBtnApprotion" Text="分  摊" runat="server">查 看</asp:LinkButton>
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="分包成本" Visible="false">
<ItemTemplate>
											<asp:LinkButton ID="LBtnJobOut" Text="分  摊" CommandName="LBtnJobOut" runat="server">查 看</asp:LinkButton>
										</ItemTemplate>
</asp:TemplateColumn></Columns></asp:DataGrid></DIV>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
