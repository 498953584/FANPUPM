<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutRepairCost.aspx.cs" Inherits="Equ_Report_Land_OutRepairCost" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>维修保养费一览表</title><link rel="Stylesheet" type="text/css" href="../../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../../Script/jquery.easyui/themes/icon.css" />
<link href="../../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script src="/Script/jquery.js" type="text/javascript"></script>
	<script type="text/javascript" src="../../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../../Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="../../../Script/jw.js"></script>
	<script type="text/javascript" src="../../../Script/DecimalInput.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#btn_Search')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			var gvBudget = new JustWinTable('gvRepair');
			replaceEmptyTable('empetyFillTable', 'gvRepair');
			setWidthHeight();
			jw.tooltip();
		});
		function setWidthHeight() {
			$('#divBudget').height($(this).height() - 70);
			$('#divBudget').width($('#divContent').width() - 2);
		}
		//查看配件信息
		function Query(id) {
			parent.desktop.RepairStock = window;
			var url = '/Equ/Report/ship/RepairStock.aspx?reportId=' + id;
			toolbox_oncommand(url, "查看配件信息");
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
														上报日期
													</td>
													<td>
														<asp:TextBox ID="txtStartDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
													</td>
													<td class="descTd">
														至
													</td>
													<td>
														<asp:TextBox ID="txtEndDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
													</td>
													<td class="descTd">
														设备编号
													</td>
													<td>
														<asp:TextBox ID="txtEquCode" Height="15px" Width="100%" runat="server"></asp:TextBox>
													</td>
													<td class="descTd">
														设备名称
													</td>
													<td>
														<asp:TextBox ID="txtEquName" Height="15px" Width="100%" runat="server"></asp:TextBox>
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
												<asp:GridView ID="gvRepair" AutoGenerateColumns="false" DataKeyNames="" CssClass="gvdata" ShowFooter="true" OnRowDataBound="gvRepair_RowDataBound" runat="server">
<EmptyDataTemplate>
														<table id="empetyFillTable">
															<tr class="header">
																<th>
																	序号
																</th>
																<th>
																	上报日期
																</th>
																<th>
																	设备编号
																</th>
																<th>
																	设备名称
																</th>
																<th>
																	规格型号
																</th>
																<th>
																	申请部门
																</th>
																<th>
																	故障简介
																</th>
																<th>
																	委外维修公司
																</th>
																<th>
																	委外维修部门
																</th>
																<th>
																	委外维修开始日期
																</th>
																<th>
																	委外维修结束日期
																</th>
																<th>
																	人工费
																</th>
																<th>
																	材料费
																</th>
																<th>
																	维修总费用
																</th>
																<th>
																	验收人
																</th>
																<th>
																	备注
																</th>
															</tr>
														</table>
													</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderText="序号">
<ItemTemplate>
																
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上报日期">
<ItemTemplate>
																<span class="link" onclick="Query('')">
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="设备编号">
<ItemTemplate>
																
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="设备名称">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="申请部门">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="故障简介">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="委外维修公司">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="委外维修部门">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px" HeaderText="委外维修开始日期">
<ItemTemplate>
																
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px" HeaderText="委外维修结束日期">
<ItemTemplate>
																
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px" HeaderText="人工费" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
																
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px" HeaderText="材料费" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
																
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px" HeaderText="维修总费用" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
																
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px" HeaderText="验收人">
<ItemTemplate>
																
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px" HeaderText="备注">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
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
