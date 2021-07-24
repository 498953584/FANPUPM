<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndirectCostAnalysis.aspx.cs" Inherits="BudgetManage_Report_IndirectCostAnalysis" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#btnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			setWidthHeight();
			var gvBudget = new JustWinTable('rptBudget');
		});

		function setWidthHeight() {
			$('#divBudget').height($(this).height() - $('#tbSearchCondition').height() - $('#trBudgetTop').height() - 2);
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 7);
			$('#div_project').height($(this).height() - $('#ddlYear').height() - 3);
        }

        function viewOpen_n(cbsCode, name) {
            parent.parent.desktop.ContractSumReport = window;
            var url = "/BudgetManage/Report/IndirectCostDetail.aspx?prjId=" + $('#hfldPrjid').val() + "&cbsCode=" + cbsCode + "&cbsName=" + name;
            var detailTitle = $("#hfldPrjName").val() + "的" + name + "明细表";
            toolbox_oncommand(url, detailTitle);
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
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
		<table style="width: 100%; height: 91%;">
			<tr>
				<td style="width: 100%; height: 100%;">
					<table style="width: 100%; height: 100%;">
						<tr>
							
							<td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
								<table id="tbSearchCondition" cellpadding="5">
									<tr>
										<td>
											CBS编码
										</td>
										<td>
											<asp:TextBox ID="txtTaskCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
										</td>
										<td>
											成本科目
										</td>
										<td>
											<asp:TextBox ID="txtTaskName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
										</td>
									</tr>
								</table>
								<table class="tab">
									<tr id="trBudgetTop">
										<td>
											<div class="divFooter" style="text-align: left; padding-top: 2px;">
												<asp:Button ID="btnQuery" Text="查 询" OnClick="btnQuery_Click" runat="server" />
												<asp:Button ID="btnExcel" Text="导出Excel" Width="73px" OnClick="btnExcel_Click" runat="server" />
											</div>
										</td>
									</tr>
									<tr>
										<td style="vertical-align: top; height: 100%;">
											<div id="divBudget" class="pagediv">
												<asp:Repeater ID="rptBudget" OnItemDataBound="rptBudget_ItemDataBound" runat="server">
<HeaderTemplate>
														<table class="gvdata tbrep" id="rptBudget" cellspacing="0" rules="all" border="1"
															style="border-collapse: collapse;">
															<tbody>
																<tr class="header headerColor">
																	<td rowspan="2" style="width: 30px;">
																		序号
																	</td>
																	<td rowspan="2">
																		CBS编码
																	</td>
																	<td rowspan="2">
																		成本科目
																	</td>
																	<td colspan="4">
																		本月数
																	</td>
																	<td colspan="4">
																		累计数
																	</td>
																</tr>
																<tr class="header headerColor">
																	<td>
																		目标成本
																	</td>
																	<td>
																		实际成本
																	</td>
																	<td>
																		降低额
																	</td>
																	<td>
																		降低率
																	</td>
																	<td>
																		目标成本
																	</td>
																	<td>
																		实际成本
																	</td>
																	<td>
																		降低额
																	</td>
																	<td>
																		降低率
																	</td>
																</tr>
													</HeaderTemplate>

<ItemTemplate>
														<tr class="rowa" id='<%# Eval("Id") %>'>
															<td>
																<%# Container.ItemIndex + 1 + (this.AspNetPager1.CurrentPageIndex - 1) * 15 %>
															</td>
															<td align="left">
																<%# Eval("CBSCode") %>
															</td>
															<td align="left">
                                                                <span class="link tooltip"  title='<%# Eval("CBSName").ToString() %>' onclick="viewOpen_n('<%# Eval("CBSCode") %>', '<%# Eval("CBSName") %>')">
                                                                    <%# Eval("CBSName") %>
                                                                </span>
															</td>
															<td class="decimal_input">
																<%# Eval("MonthTarget") %>
															</td>
															<td class="decimal_input">
																<%# Eval("MonthReality") %>
															</td>
															<td class="decimal_input">
																<%# decimal.Parse(Eval("MonthTarget").ToString()) - decimal.Parse(Eval("MonthReality").ToString()) %>
															</td>
															<td>
																<%# (decimal.Parse(Eval("MonthTarget").ToString()) > 0m) ? ((decimal.Parse(Eval("MonthTarget").ToString()) - decimal.Parse(Eval("MonthReality").ToString())) / decimal.Parse(Eval("MonthTarget").ToString())).ToString("P2") : 0m.ToString("P2") %>
															</td>
															<td class="decimal_input">
																<%# Eval("TotalTarget") %>
															</td>
															<td class="decimal_input">
																<%# Eval("TotalReality") %>
															</td>
															<td class="decimal_input">
																<%# decimal.Parse(Eval("TotalTarget").ToString()) - decimal.Parse(Eval("TotalReality").ToString()) %>
															</td>
															<td>
																<%# (decimal.Parse(Eval("TotalTarget").ToString()) > 0m) ? ((decimal.Parse(Eval("TotalTarget").ToString()) - decimal.Parse(Eval("TotalReality").ToString())) / decimal.Parse(Eval("TotalTarget").ToString())).ToString("P2") : 0m.ToString("P2") %>
															</td>
														</tr>
													</ItemTemplate>

<AlternatingItemTemplate>
														<tr class="rowb" id='<%# Eval("Id") %>'>
															<td>
																<%# Container.ItemIndex + 1 + (this.AspNetPager1.CurrentPageIndex - 1) * 15 %>
															</td>
															<td align="left">
																<%# Eval("CBSCode") %>
															</td>
															<td align="left">
																<span class="link tooltip"  title='<%# Eval("CBSName").ToString() %>' onclick="viewOpen_n('<%# Eval("CBSCode") %>', '<%# Eval("CBSName") %>')">
                                                                    <%# Eval("CBSName") %>
                                                                </span>
															</td>
															<td class="decimal_input">
																<%# Eval("MonthTarget") %>
															</td>
															<td class="decimal_input">
																<%# Eval("MonthReality") %>
															</td>
															<td class="decimal_input">
																<%# decimal.Parse(Eval("MonthTarget").ToString()) - decimal.Parse(Eval("MonthReality").ToString()) %>
															</td>
															<td>
																<%# (decimal.Parse(Eval("MonthTarget").ToString()) > 0m) ? ((decimal.Parse(Eval("MonthTarget").ToString()) - decimal.Parse(Eval("MonthReality").ToString())) / decimal.Parse(Eval("MonthTarget").ToString())).ToString("P2") : 0m.ToString("P2") %>
															</td>
															<td class="decimal_input">
																<%# Eval("TotalTarget") %>
															</td>
															<td class="decimal_input">
																<%# Eval("TotalReality") %>
															</td>
															<td class="decimal_input">
																<%# decimal.Parse(Eval("TotalTarget").ToString()) - decimal.Parse(Eval("TotalReality").ToString()) %>
															</td>
															<td>
																<%# (decimal.Parse(Eval("TotalTarget").ToString()) > 0m) ? ((decimal.Parse(Eval("TotalTarget").ToString()) - decimal.Parse(Eval("TotalReality").ToString())) / decimal.Parse(Eval("TotalTarget").ToString())).ToString("P2") : 0m.ToString("P2") %>
															</td>
														</tr>
													</AlternatingItemTemplate>

<FooterTemplate>
														<tr>
															<td colspan="3" align="center">
																合计
															</td>
															<td>
																<asp:Label ID="lblMonthTarget" CssClass="decimal_input" runat="server"></asp:Label>
															</td>
															<td>
																<asp:Label ID="lblMonthReality" CssClass="decimal_input" runat="server"></asp:Label>
															</td>
															<td>
																<asp:Label ID="lblMonthLower" CssClass="decimal_input" runat="server"></asp:Label>
															</td>
															<td>
																<asp:Label ID="lblMonthLowerRate" runat="server"></asp:Label>
															</td>
															<td>
																<asp:Label ID="lblTotalTarget" CssClass="decimal_input" runat="server"></asp:Label>
															</td>
															<td>
																<asp:Label ID="lblTotalReality" CssClass="decimal_input" runat="server"></asp:Label>
															</td>
															<td>
																<asp:Label ID="lblTotalLower" CssClass="decimal_input" runat="server"></asp:Label>
															</td>
															<td>
																<asp:Label ID="lblTotalLowerRate" runat="server"></asp:Label>
															</td>
														</tr>
														</tbody> </table>
													</FooterTemplate>
</asp:Repeater>
												<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
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
    
    <asp:HiddenField ID="hfldPrjid" runat="server" />
    
    <asp:HiddenField ID="hfldPrjName" runat="server" />
	</form>
</body>
</html>
