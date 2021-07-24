<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MinorStuffAnalysis.aspx.cs" Inherits="BudgetManage_Report_MinorStuffAnalysis" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>零星材料费分析表</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#btnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			replaceEmptyTable('emptyMinorStuff', 'gvMinorStuff');
			formateTable('gvMinorStuff', 2, true);
			setWidthHeight();
			jw.tooltip();
		});
		function setWidthHeight() {
			$('#divBudget').height($(this).height() - 61);
			$('#div_project').height($(this).height() - 20);
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 5);
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
		<table>
			<tr>
				<td style="vertical-align: top; border-left: solid 2px #CADEED; height: 100%; width: 100%">
					<table class="queryTable" cellpadding="3px" cellspacing="0px">
						<tr>
							<td>
								辅材编号
							</td>
							<td>
								<asp:TextBox ID="txtCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
							</td>
							<td>
								辅材名称
							</td>
							<td>
								<asp:TextBox ID="txtName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
							</td>
						</tr>
					</table>
					<table class="tab">
						<tr>
							<td class="divFooter" style="text-align: left; padding-top: 2px;">
								<asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
								<asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
							</td>
						</tr>
						<tr>
							<td style="vertical-align: top; height: 100%; width: 100%">
								<div id="divBudget" class="pagediv">
									<asp:GridView ID="gvMinorStuff" AutoGenerateColumns="false" CssClass="gvdata" Width="100%" ShowFooter="true" OnRowDataBound="gvMinorStuff_RowDataBound" runat="server">
<EmptyDataTemplate>
											<table class="gvdata" cellspacing="0" rules="all" border="1" id="emptyMinorStuff"
												style="width: 100%; border-collapse: collapse;">
												<tr>
													<th class="header" rowspan="2">
														序号
													</th>
													<th class="header" rowspan="2">
														材料编号
													</th>
													<th class="header" rowspan="2">
														材料名称
													</th>
													<th class="header" rowspan="2">
														规格
													</th>
													<th class="header" rowspan="2">
														品牌
													</th>
													<th class="header" rowspan="2">
														型号
													</th>
													<th class="header" rowspan="2">
														技术参数
													</th>
													<th class="header" colspan="4">
														本月数
													</th>
													<th class="header" colspan="4">
														累计数
													</th>
												</tr>
												<tr class='header'>
													<th rowspan="1">
														目标成本
													</th>
													<th rowspan="1">
														实际成本
													</th>
													<th class="tooltip" title=" 降低额 = 目标成本 &ndash; 实际成本">
														降低额
													</th>
													<th class="tooltip" title=" 降低率 = 降低额 &divide; 目标成本">
														降低率
													</th>
													<th rowspan="1">
														目标成本
													</th>
													<th rowspan="1">
														实际成本
													</th>
													<th class="tooltip" title=" 降低额 = 目标成本 &ndash; 实际成本">
														降低额
													</th>
													<th class="tooltip" title=" 降低率 = 降低额 &divide; 目标成本">
														降低率
													</th>
												</tr>
											</table>
										</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<ItemTemplate>
													<%# Container.DataItemIndex + 1 + (this.AspNetPager1.CurrentPageIndex - 1) * 15 %></ItemTemplate>
</asp:TemplateField><asp:TemplateField><ItemTemplate>
													<span class="tooltip" title='<%# Eval("ResourceCode").ToString() %>'>
														<%# StringUtility.GetStr(Eval("ResourceCode").ToString(), 10) %>
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate>
													<span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
														<%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate>
													<span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
														<%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
													</span>
												</ItemTemplate></asp:TemplateField><asp:BoundField DataField="Brand" /><asp:BoundField DataField="ModelNumber" /><asp:TemplateField><ItemTemplate>
													<span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
														<%# StringUtility.GetStr(Eval("TechnicalParameter").ToString(), 10) %>
													</span>
												</ItemTemplate></asp:TemplateField><asp:BoundField DataField="MonthTotalBud" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="MonthTotalAcc" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="MonthChazhi" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="Monthbilu" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="TotalBud" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="TotalAcc" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="Chazhi" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="Bilu" ItemStyle-HorizontalAlign="Right" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
	</form>
</body>
</html>
