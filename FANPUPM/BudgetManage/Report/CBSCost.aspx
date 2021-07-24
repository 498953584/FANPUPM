<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CBSCost.aspx.cs" Inherits="BudgetManage_Report_CBSCost" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>项目成本计划分项分类表</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var gvBudget = new JustWinTable('gvCBSCost');
			replaceEmptyTable('empetyFillTable', 'gvCBSCost');
			setWidthHeight();
		});
		function setWidthHeight() {
			$('#divBudget').height($(this).height() - 30);
			$('#div_project').height($(this).height() - $('#ddlYear').height() - 4);
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
					<asp:Label ID="lblTitle" Font-Bold="true" Text="项目成本计划分项分类表" runat="server"></asp:Label>
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
									<tr style="height: 1px;">
										<td class="divFooter" style="text-align: left;">
											<asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
										</td>
									</tr>
									<tr style="height: 1px;">
										<td style="width: 100%; height: 90%;">
											<div id="divBudget" class="pagediv" style="border-bottom: solid 0px red;">
												<asp:GridView ID="gvCBSCost" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvCBSCost_RowDataBound" DataKeyNames="CBSCode" runat="server">
<EmptyDataTemplate>
														<table id="empetyFillTable">
															<tr class="header">
																<th>
																	序号
																</th>
																<th>
																	成本计划内容
																</th>
																<th class="tooltip" title=" 首次计划金额 = 目标成本中资源类型成本归集过的资源或间接成本预算 ">
																	首次计划金额
																</th>
																<th class="tooltip" title=" 开工至上年末累计实际发生额 = 施工报量中上年末之前上报的资源类型成本归集过的资源 ">
																	开工至上年末累计实际发生额
																</th>
																<th class="tooltip" title=" 本年度累计实际发生额 = 施工报量中今年上报的资源类型成本归集过的资源金额或间接成本日记账 ">
																	本年度累计实际发生额
																</th>
																<th class="tooltip" title=" 开工至调整日前实际累计发生额 = 开工至上年末累计实际发生额+本年度累计实际发生额 ">
																	开工至调整日前实际累计发生额
																</th>
															</tr>
														</table>
													</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderText="序号">
<ItemTemplate>
																<%# Eval("Num") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="成本计划内容">
<ItemTemplate>
																<%# Eval("CBSName") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px" HeaderText="首次计划金额" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("BudTotal")).ToString("0.000") %>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px" HeaderText="开工至上年末累计实际发生额" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("LastRealityTotal")).ToString("0.000") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px" HeaderText="本年度累计实际发生额" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("CurrentRealityTotal")).ToString("0.000") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px" HeaderText="开工至调整日前实际累计发生额" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
																<%# Eval("RealityTotal") %>
															</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
											</div>
										</td>
									</tr>
									<tr>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	</form>
</body>
</html>
