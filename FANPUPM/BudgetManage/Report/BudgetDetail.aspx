<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudgetDetail.aspx.cs" Inherits="BudgetManage_Report_BudgetDetail" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#btnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			setWidthHeight();
			var gvBudget = new JustWinTable('rptBudget');
			jw.tooltip();
		});
		function setWidthHeight() {
			$('#divBudget').height($(this).height() - $('#trBudgetTop').height() - $('#tbSearchCondition').height() - 6);
			$('#div_project').height($(this).height() - $('#ddlYear').height() - 2);
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 6);
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
			padding: 1px 4px;
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
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
		<table>
			<tr>
				
				<td style="vertical-align: top; border-left: solid 2px #CADEED;">
					<table id="tbSearchCondition" class="queryTable" cellpadding="3px" cellspacing="0px">
						<tr>
							<td style="white-space: nowrap;">
								任务编码
							</td>
							<td>
								<asp:TextBox ID="txtTaskCode" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
							</td>
							<td style="white-space: nowrap;">
								任务名称
							</td>
							<td>
								<asp:TextBox ID="txtTaskName" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
							</td>
						</tr>
					</table>
					<table class="tab">
						<tr id="trBudgetTop">
							<td class="divFooter" style="text-align: left; padding-top: 2px;">
								<asp:Button ID="btnQuery" Text="查 询" OnClick="btnQuery_Click" runat="server" />
								<asp:Button ID="btnExcel" Text="导出Excel" Width="73px" OnClick="btnExcel_Click" runat="server" />
							</td>
						</tr>
						<tr>
							<td style="vertical-align: top; height: 100%; width: 100%">
								<div id="divBudget" class="pagediv">
									<asp:Repeater ID="rptBudget" OnItemDataBound="rptBudget_ItemDataBound" runat="server">
<HeaderTemplate>
											<table class="gvdata tbrep" id="rptBudget" cellspacing="0" rules="all" border="1"
												style="border-collapse: collapse; width: 100%">
												<tbody>
													<tr class="header headerColor">
														<td rowspan="2">
															序号
														</td>
														<td rowspan="2">
															任务编码
														</td>
														<td rowspan="2">
															任务名称
														</td>
														<td colspan="2">
															合同预算
														</td>
														<td colspan="2">
															目标成本
														</td>
														<td colspan="2">
															实际成本
														</td>
														<td rowspan="2" class="tooltip" title="开累计划利润 = 合同预算 &ndash; 目标成本">
															开累计划利润
														</td>
														<td rowspan="2" class="tooltip" title="开累实际盈亏 = 合同预算 &ndash; 实际成本">
															开累实际盈亏
														</td>
														<td rowspan="2" class="tooltip" title="开累项目部利润 = 目标成本 &ndash; 实际成本">
															开累项目部利润
														</td>
													</tr>
													<tr class="header headerColor">
														<td>
															总价
														</td>
														<td>
															单价
														</td>
														<td>
															总价
														</td>
														<td>
															单价
														</td>
														<td>
															总价
														</td>
														<td>
															单价
														</td>
													</tr>
										</HeaderTemplate>

<ItemTemplate>
											<tr class="rowa" id='<%# Eval("TaskId") %>'>
												<td>
													<%# Container.ItemIndex + 1 + (this.AspNetPager1.CurrentPageIndex - 1) * 15 %>
												</td>
												<td align="left">
													<%# Eval("TaskCode") %>
												</td>
												<td align="left">
													<%# Eval("TaskName") %>
												</td>
												<td class="decimal_input">
													<%# Eval("TenderTotal") %>
												</td>
												<td class="decimal_input">
													<%# Eval("TenderPrice") %>
												</td>
												<td class="decimal_input">
													<%# Eval("TargetTotal") %>
												</td>
												<td class="decimal_input">
													<%# Eval("TargetPrice") %>
												</td>
												<td class="decimal_input">
													<%# Eval("RealityTotal") %>
												</td>
												<td class="decimal_input">
													<%# Eval("RealityPrice") %>
												</td>
												<td class="decimal_input">
													<%# decimal.Parse(Eval("TenderTotal").ToString()) - decimal.Parse(Eval("TargetTotal").ToString()) %>
												</td>
												<td class="decimal_input">
													<%# decimal.Parse(Eval("TenderTotal").ToString()) - decimal.Parse(Eval("RealityTotal").ToString()) %>
												</td>
												<td class="decimal_input">
													<%# decimal.Parse(Eval("TargetTotal").ToString()) - decimal.Parse(Eval("RealityTotal").ToString()) %>
												</td>
											</tr>
										</ItemTemplate>

<AlternatingItemTemplate>
											<tr class="rowb" id='<%# Eval("TaskId") %>'>
												<td>
													<%# Container.ItemIndex + 1 + (this.AspNetPager1.CurrentPageIndex - 1) * 15 %>
												</td>
												<td align="left">
													<%# Eval("TaskCode") %>
												</td>
												<td align="left">
													<%# Eval("TaskName") %>
												</td>
												<td class="decimal_input">
													<%# Eval("TenderTotal") %>
												</td>
												<td class="decimal_input">
													<%# Eval("TenderPrice") %>
												</td>
												<td class="decimal_input">
													<%# Eval("TargetTotal") %>
												</td>
												<td class="decimal_input">
													<%# Eval("TargetPrice") %>
												</td>
												<td class="decimal_input">
													<%# Eval("RealityTotal") %>
												</td>
												<td class="decimal_input">
													<%# Eval("RealityPrice") %>
												</td>
												<td class="decimal_input">
													<%# decimal.Parse(Eval("TenderTotal").ToString()) - decimal.Parse(Eval("TargetTotal").ToString()) %>
												</td>
												<td class="decimal_input">
													<%# decimal.Parse(Eval("TenderTotal").ToString()) - decimal.Parse(Eval("RealityTotal").ToString()) %>
												</td>
												<td class="decimal_input">
													<%# decimal.Parse(Eval("TargetTotal").ToString()) - decimal.Parse(Eval("RealityTotal").ToString()) %>
												</td>
											</tr>
										</AlternatingItemTemplate>

<FooterTemplate>
											<tr>
												<td colspan="3" align="center">
													合计
												</td>
												<td>
													<asp:Label ID="lblTenderTotal" CssClass="decimal_input" runat="server"></asp:Label>
												</td>
												<td>
													<asp:Label ID="lblTenderPrice" CssClass="decimal_input" runat="server"></asp:Label>
												</td>
												<td>
													<asp:Label ID="lblTargetTotal" CssClass="decimal_input" runat="server"></asp:Label>
												</td>
												<td>
													<asp:Label ID="lblTargetPrice" CssClass="decimal_input" runat="server"></asp:Label>
												</td>
												<td>
													<asp:Label ID="lblRealityTotal" CssClass="decimal_input" runat="server"></asp:Label>
												</td>
												<td>
													<asp:Label ID="lblRealityPrice" CssClass="decimal_input" runat="server"></asp:Label>
												</td>
												<td>
													<asp:Label ID="lblkljhlr" CssClass="decimal_input" runat="server"></asp:Label>
												</td>
												<td>
													<asp:Label ID="lblklsjyk" CssClass="decimal_input" runat="server"></asp:Label>
												</td>
												<td>
													<asp:Label ID="lblklxmlr" CssClass="decimal_input" runat="server"></asp:Label>
												</td>
											</tr>
											</tbody> </table>
										</FooterTemplate>
</asp:Repeater>
									<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
									</webdiyer:AspNetPager>
								</div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	
	<asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
	</form>
</body>
</html>
