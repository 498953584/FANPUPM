<%@ Page Language="C#" AutoEventWireup="true" CodeFile="projectcostapprove.aspx.cs" Inherits="ProjectCostApprove" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_uprojectlist_ascx" Src="~/EPC/UserControl1/uprojectlist.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ItemDistribute</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
			function OpenResourcePlanWin(type)
			{
				var projectCode = document.getElementById('HdnProjectCode').value;
				if(projectCode != "")
				{
					switch(type)
					{
						case "Frame":
							InfoList.self.location = "CostFrameAnalyze.aspx?pc="+projectCode;
							break;
						case "Approve":
							InfoList.self.location = "CostApproveGather.aspx?pc="+projectCode;
							break;
						case "Month":
							InfoList.self.location = "MonthCostApprove.aspx?pc="+projectCode;
							break;
					}
				}
				return false;
			}
		</script>
	</head>
	<body class="body_frame" bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0"   onload="selrow3('UCProjectList_tvProjectt1')">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%" border="0">
				<tr>
					<td>
						<table class="table-none" id="Tablec" height="100%" cellSpacing="0" cellPadding="0" width="100%"
							border="1">
							<tr class="td-toolsbar">
								<td align=left>成本核算</td>	
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="100%">
						<table class="table-none" id="Table3" height="100%" cellSpacing="0" cellPadding="0" width="100%"
							border="1">
							<tr>
								<td vAlign="top" width="15%" height="100%"><div style="width:200px;height:100%;overflow:auto;">
										<MyUserControl:epc_usercontrol1_uprojectlist_ascx ID="UCProjectList" runat="server" /></div>
								</td>
								<td vAlign="top" width="85%"><iframe id="InfoList" name="InfoList" src="" frameBorder="0" width="100%" scrolling="no"
										height="100%"></iframe>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
