<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthSituationReport.aspx.cs" Inherits="Equ_Report_Land_MonthSituationReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>钻孔机月汇总表</title><link rel="Stylesheet" type="text/css" href="../../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../../Script/jquery.easyui/themes/icon.css" />
<link href="../../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script src="/Script/jquery.js" type="text/javascript"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../../Script/jw.js"></script>
	<script type="text/javascript" src="../../../Script/DecimalInput.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#btn_Search')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			var gvBudget = new JustWinTable('gvReport');
			replaceEmptyTable('empetyFillTable', 'gvReport');
			setWidthHeight();
			jw.tooltip();
		});
		function setWidthHeight() {
			$('#divBudget').height($(this).height() - 70);
			$('#divBudget').width($('#divContent').width() - 2);
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
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
														年份
													</td>
													<td>
														<asp:DropDownList ID="ddlYear" Width="120px" runat="server"></asp:DropDownList>
													</td>
													<td class="descTd">
														月份
													</td>
													<td>
														<asp:DropDownList ID="ddlMonth" Width="120px" runat="server"></asp:DropDownList>
													</td>
													<td class="descTd">
														设备编号
													</td>
													<td>
														<asp:TextBox ID="txtEquCode" Height="15px" Width="120px" runat="server"></asp:TextBox>
													</td>
													<td class="descTd">
														设备名称
													</td>
													<td>
														<asp:TextBox ID="txtEquName" Height="15px" Width="120px" runat="server"></asp:TextBox>
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
												<asp:GridView ID="gvReport" AutoGenerateColumns="false" DataKeyNames="" CssClass="gvdata" ShowFooter="true" OnRowDataBound="gvReport_RowDataBound" runat="server">
<EmptyDataTemplate>
														<table id="empetyFillTable">
															<tr class="header">
																<th scope="col" style="width: 25px;">
																	序号
																</th>
																<th scope="col">
																	设备编号
																</th>
																<th scope="col">
																	名称名称
																</th>
																<th scope="col">
																	规格
																</th>
																<th scope="col">
																	施工项目
																</th>
																<th scope="col">
																	设备人员
																</th>
																<th scope="col">
																	设备原值
																</th>
																<th scope="col">
																	计量单位
																</th>
																<th scope="col">
																	单价
																</th>
																<th scope="col">
																	产量上报量
																</th>
																<th scope="col">
																	台班上报量
																</th>
																<th scope="col">
																	收入合计金额
																</th>
																<th scope="col">
																	支出合计金额
																</th>
																<th scope="col">
																	设备月实作天数
																</th>
																<th scope="col">
																	设备利用率(%)
																</th>
																<th scope="col">
																	设备月完好天数
																</th>
																<th scope="col">
																	设备月制度台日数
																</th>
																<th scope="col">
																	设备完好率(%)
																</th>
																<th scope="col">
																	单位产量油耗(L/单位)
																</th>
																<th scope="col">
																	单位产量油耗(元/单位)
																</th>
																<th scope="col">
																	单位产量成本(元/m³)
																</th>
																<th scope="col">
																	单位变动成本(元/m³)
																</th>
																<th scope="col">
																	员工工资占总成本比重(%)
																</th>
																<th scope="col">
																	固定成本占总成本比重(%)
																</th>
																<th scope="col">
																	变动成本占总成本比重(%)
																</th>
																<th scope="col">
																	利息支出占总成本比重(%)
																</th>
																<th scope="col">
																	整体盈亏(元)
																</th>
																<th scope="col">
																	盈亏平衡点
																</th>
															</tr>
														</table>
													</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderText="序号">
<ItemTemplate>
																
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="设备编号" HeaderStyle-Width="50px">
<ItemTemplate>
																
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="设备名称" HeaderStyle-Width="80px">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格" HeaderStyle-Width="80px">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="施工项目" HeaderStyle-Width="80px">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="设备人员" HeaderStyle-Width="80px">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="设备原值" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计量单位" HeaderStyle-Width="50px">
<ItemTemplate>
																
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单价" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="产量上报量" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="台班上报量" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收入合计金额" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="支出合计金额" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备月实作天数" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备利用率(%)" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备月完好天数" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备月制度台日数" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备完好率(%)" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位<Br>产量油耗<Br>(L/单位)" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位<Br>产量油耗<Br>(元/单位)" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位<Br>产量成本<Br>(元/单位)" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位<Br>变动成本<Br>(元/单位)" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="员工工资<Br>占总成本<Br>比重(%)" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="固定成本<Br>占总成本<Br>比重(%)" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变动成本<Br>占总成本<Br>比重(%)" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="利息支出<Br>占总成本<Br>比重(%)" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="整体盈亏<Br>(元)" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="盈亏平衡点" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
	</form>
</body>
</html>
