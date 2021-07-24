<%@ Page Language="C#" AutoEventWireup="true" CodeFile="productvalueframe.aspx.cs" Inherits="ProductValueFrame" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_uprojectlist_ascx" Src="~/EPC/UserControl1/uprojectlist.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ItemDistribute</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
			function OpenProduceValue(type)
			{
				var projectCode = document.getElementById('HdnProduceValue').value;
				if(projectCode != "")
				{
					switch(type)
					{
						case 'S':
							InfoList.location.href = "ProductValueMonthStat.aspx?pc="+projectCode;
							break;
						case 'R':
							InfoList.location.href = "ProductValueMonthReport.aspx?pc="+projectCode;
							break;
					}
				}
				return false;
			}
		</script>
	</head>
	<body class="body_frame" bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%" border="0">
				<tr style="display:none;">
					<td>
						<table class="table-none" id="Tablec" height="100%" cellSpacing="0" cellPadding="0" width="100%"
							border="1">
							<tr>
								<td class="td-title">
									<asp:Label ID="LblMsg" runat="server"></asp:Label>
									<input id="HdnProduceValue" type="hidden" style="WIDTH:10px" name="HdnProduceValue" runat="server" />
&nbsp;
									<asp:LinkButton ID="LBtnProductValueStat" Visible="false" runat="server">产值统计</asp:LinkButton>&nbsp;
									<asp:LinkButton ID="LBtnProductValueFill" Visible="false" runat="server">产值填报</asp:LinkButton>&nbsp;
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="100%">
						<table class="table-none" id="Table3" height="100%" cellSpacing="0" cellPadding="0" width="100%"
							border="1">
							<tr>
								<td vAlign="top" width="25%" height="100%"><div class="gridBox">
										<MyUserControl:epc_usercontrol1_uprojectlist_ascx ID="UCProjectList" runat="server" /></div>
								</td>
								<td vAlign="top" width="75%"><iframe id="InfoList" name="InfoList" src="" frameBorder="0" width="100%" scrolling="no"
										height="100%"></iframe>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><FONT face="宋体"></FONT>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
