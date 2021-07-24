<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepairStock.aspx.cs" Inherits="Equ_Report_Ship_RepairStock" %>
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
														配件编号
													</td>
													<td>
														<asp:TextBox ID="txtResCode" Height="15px" Width="100%" runat="server"></asp:TextBox>
													</td>
													<td class="descTd">
														配件名称
													</td>
													<td>
														<asp:TextBox ID="txtResName" Height="15px" Width="100%" runat="server"></asp:TextBox>
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
																<th scope="col" style="width: 25px;">
																	序号
																</th>
																<th scope="col">
																	领用日期
																</th>
																<th scope="col">
																	配件编号
																</th>
																<th scope="col">
																	配件名称
																</th>
																<th scope="col">
																	规格
																</th>
																<th scope="col">
																	型号
																</th>
																<th scope="col">
																	品牌
																</th>
																<th scope="col">
																	技术参数
																</th>
																<th scope="col">
																	单位
																</th>
																<th scope="col">
																	数量
																</th>
																<th scope="col">
																	采购价格
																</th>
																<th scope="col">
																	小计
																</th>
																<th scope="col">
																	供应商
																</th>
																<th scope="col">
																	领用人
																</th>
															</tr>
														</table>
													</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderText="序号">
<ItemTemplate>
																
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="领用日期">
<ItemTemplate>
																
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="配件编号">
<ItemTemplate>
																
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="配件名称">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位">
<ItemTemplate>
																
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="数量" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="价格" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合价" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="供应商">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="领用人">
<ItemTemplate>
																
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
