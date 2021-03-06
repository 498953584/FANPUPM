<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LaborCostAnalysis.aspx.cs" Inherits="BudgetManage_Report_LaborCostAnalysis" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>人工费分析</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script src="/Script/jquery.js" type="text/javascript"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#btn_Search')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			var gvBudget = new JustWinTable('gvLaborCost');
			replaceEmptyTable('empetyFillTable', 'gvLaborCost');
			setWidthHeight();
			jw.tooltip();
		});
		function setWidthHeight() {
			$('#divBudget').height($(this).height() - 61);
			$('#div_project').height($(this).height() - 20);
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 8);
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader" style="display: none;">
		<table class="tableHeader">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="人工费分析" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
		<table>
			<tr>
				<td style="width: 100%; height: 100%;">
					<table style="width: 100%; height: 100%;">
						<tr>
							<td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
								<table style="border: 0px; width: 100%; height: 100%;">
									<tr>
										<td>
											<table class="queryTable" cellpadding="3px" cellspacing="0px">
												<tr>
													<td class="descTd">
														编码
													</td>
													<td>
														<asp:TextBox ID="txtResourceCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
													</td>
													<td class="descTd">
														&nbsp;工种
													</td>
													<td>
														<asp:TextBox ID="txtResourceName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td class="divFooter" style="text-align: left;">
											<asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
											<asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
										</td>
									</tr>
									<tr>
										<td style="width: 100%; height: 90%;">
											<div id="divBudget" class="pagediv" style="border-bottom: solid 0px red;">
												<asp:GridView ID="gvLaborCost" AutoGenerateColumns="false" DataKeyNames="" CssClass="gvdata" ShowFooter="true" OnRowDataBound="gvLaborCost_RowDataBound" runat="server">
<EmptyDataTemplate>
														<table id="empetyFillTable">
															<tr class="header">
																<th rowspan="2">
																	序号
																</th>
																<th rowspan="2">
																	编号
																</th>
																<th rowspan="2">
																	工种
																</th>
																<th colspan="6">
																	本月数
																</th>
																<th colspan="6">
																	累计数
																</th>
															</tr>
															<tr class="header">
																<th>
																	目标成本数量
																</th>
																<th>
																	目标成本金额
																</th>
																<th>
																	实际成本数量
																</th>
																<th>
																	实际成本金额
																</th>
																<th class="tooltip" title=" 降低额 = 目标成本金额 &ndash; 实际成本金额">
																	降低额
																</th>
																<th class="tooltip" title=" 降低率 = 降低额 &divide; 目标成本金额">
																	降低率
																</th>
																<th>
																	目标成本数量
																</th>
																<th>
																	目标成本金额
																</th>
																<th>
																	实际成本数量
																</th>
																<th>
																	实际成本金额
																</th>
																<th class="tooltip" title=" 降低额 = 目标成本金额 &ndash; 实际成本金额">
																	降低额
																</th>
																<th class="tooltip" title=" 降低率 = 降低额 &divide; 目标成本金额">
																	降低率
																</th>
															</tr>
														</table>
													</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<ItemTemplate>
																<%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<ItemTemplate>
																<%# Eval("ResourceCode") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<ItemTemplate>
																<span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
																	<%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("CurrentBudQuantity")).ToString("0.000") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("CurrentBudCost")).ToString("0.000") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("CurrentConsQuantity")).ToString("0.000") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("CurrentConsCost")).ToString("0.000") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("CurrentReductionAmount")).ToString("0.000") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<ItemTemplate>
																<%# Eval("CurrentReductionRate") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("BudQuantity")).ToString("0.000") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("BudCost")).ToString("0.000") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("ConsQuantity")).ToString("0.000") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("ConsCost")).ToString("0.000") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("ReductionAmount")).ToString("0.000") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<ItemTemplate>
																<%# Eval("ReductionRate") %>
															</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
												<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
												</webdiyer:AspNetPager>
											</div>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	<asp:HiddenField ID="hfldYear" runat="server" />
	</form>
</body>
</html>
