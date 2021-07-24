<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LagAnalysis.aspx.cs" Inherits="ProgressManagement_Track_LagAnalysis" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>进度滞后分析</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('tblRpt');
			showTooltip("tooltip", 25);
		});

		//版本查询 
		function lookVersion(prjId) {
			parent.desktop.versionQuery = window;
			var url = '/ProgressManagement/Modify/QueryVersion.aspx?prjId=' + prjId;
			var titleText = '进度版本查询';
			toolbox_oncommand(url, titleText);
		}
	</script>
	<style type="text/css">
		.tbrep
		{
			text-align: right;
		}
		.tbrep tr td
		{
			border: solid 1px #CADEED;
			padding: 1px 4px 1px 4px;
		}
		.headerColor
		{
			color: #727faa;
			text-align: center;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<table cellpadding="5" cellspacing="5">
		<tr>
			<td>
				项目名称
			</td>
			<td>
				<asp:TextBox ID="txtPrjName" runat="server"></asp:TextBox>
			</td>
			<td>
			</td>
		</tr>
	</table>
	<div class="divFooter" style="text-align: left;">
		<asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
	</div>
	<div class="pagediv" style="height: 80%;">
		<asp:Repeater ID="rptAnalysis" runat="server">
<HeaderTemplate>
				<table id="tblRpt" class="gvdata tbrep" cellspacing="0" rules="all" border="1" style="border-collapse: collapse;">
					<tr class="header headerColor">
						<th width="20" rowspan="2">
							序号
						</th>
						<th rowspan="2">
							项目名称
						</th>
						<th colspan="2">
							任务没有开工，且晚于最迟开始时间开工
						</th>
						<th colspan="2">
							任务没有开工，且晚于计划时间开工
						</th>
						<th colspan="2">
							任务没有完工，且晚于最迟结束时间完工
						</th>
						<th colspan="2">
							任务没有完工，且晚于计划时间完工
						</th>
					</tr>
					<tr class="header headerColor">
						<th>
							滞后项
						</th>
						<th>
							所占比例
						</th>
						<th>
							滞后项
						</th>
						<th>
							所占比例
						</th>
						<th>
							滞后项
						</th>
						<th>
							所占比例
						</th>
						<th>
							滞后项
						</th>
						<th>
							所占比例
						</th>
					</tr>
			</HeaderTemplate>

<ItemTemplate>
				<tr class="rowa" id='<%# Eval("prjId") %>'>
					<td width="20" style="text-align: left;">
						<%# Eval("No") %>
					</td>
					<td style="text-align: left;">
						<a class="link tooltip" title='<%# Eval("PrjName") %>' onclick="lookVersion('<%# Eval("prjId") %>')">
							<%# StringUtility.GetStr(Eval("PrjName"), 25) %></a>
					</td>
					<td>
						<%# Eval("LagStart") %>
					</td>
					<td>
						<%# string.Format("{0:p2}", Eval("LagStartRate")) %>
					</td>
					<td>
						<%# Eval("LagConsStart") %>
					</td>
					<td>
						<%# string.Format("{0:p2}", Eval("LagConsStartRate")) %>
					</td>
					<td>
						<%# Eval("LagFinish") %>
					</td>
					<td>
						<%# string.Format("{0:p2}", Eval("LagFinishRate")) %>
					</td>
					<td>
						<%# Eval("LagConsFinish") %>
					</td>
					<td>
						<%# string.Format("{0:p2}", Eval("LagConsFinishRate")) %>
					</td>
				</tr>
			</ItemTemplate>

<AlternatingItemTemplate>
				<tr class="rowb" id='<%# Eval("prjId") %>'>
					<td width="20" style="text-align: left;">
						<%# Eval("No") %>
					</td>
					<td style="text-align: left;">
						<a class="link tooltip" title='<%# Eval("PrjName") %>' onclick="lookVersion('<%# Eval("prjId") %>')">
							<%# StringUtility.GetStr(Eval("PrjName"), 25) %></a>
					</td>
					<td>
						<%# Eval("LagStart") %>
					</td>
					<td>
						<%# string.Format("{0:p2}", Eval("LagStartRate")) %>
					</td>
					<td>
						<%# Eval("LagConsStart") %>
					</td>
					<td>
						<%# string.Format("{0:p2}", Eval("LagConsStartRate")) %>
					</td>
					<td>
						<%# Eval("LagFinish") %>
					</td>
					<td>
						<%# string.Format("{0:p2}", Eval("LagFinishRate")) %>
					</td>
					<td>
						<%# Eval("LagConsFinish") %>
					</td>
					<td>
						<%# string.Format("{0:p2}", Eval("LagConsFinishRate")) %>
					</td>
				</tr>
			</AlternatingItemTemplate>

<FooterTemplate>
				</table>
			</FooterTemplate>
</asp:Repeater>
		<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
		</webdiyer:AspNetPager>
	</div>
	</form>
</body>
</html>
