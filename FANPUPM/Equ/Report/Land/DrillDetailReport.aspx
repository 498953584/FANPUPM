<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DrillDetailReport.aspx.cs" Inherits="Equ_Report_Land_DrillDetailReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>钻孔机产量明细表</title><link rel="Stylesheet" type="text/css" href="../../../Script/jquery.easyui/themes/default/easyui.css" />
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
			var gvBudget = new JustWinTable('gvRepair');
			replaceEmptyTable('empetyFillTable', 'gvRepair');
			setWidthHeight();
			jw.tooltip();
		});
		function setWidthHeight() {
			$('#divBudget').height($(this).height() - 50);
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
										<td class="divFooter" style="text-align: left;">
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
																	施工日期
																</th>
																<th scope="col">
																	钻孔机编号
																</th>
																<th scope="col">
																	钻孔机名称
																</th>
																<th scope="col">
																	规格
																</th>
																<th scope="col">
																	制造厂家
																</th>
																<th scope="col">
																	孔数
																</th>
																<th scope="col">
																	总长
																</th>
																<th>
																	项目
																</th>
																<th>
																	位置
																</th>
																<th>
																	计量单位
																</th>
																<th>
																	施工地点
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
</asp:TemplateField><asp:TemplateField HeaderText="施工日期">
<ItemTemplate>
																
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="钻孔机编号">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="钻孔机名称">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="制造厂家">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="孔数" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="总长" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
																
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="位置">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="计量单位">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="施工地点">
<ItemTemplate>
																<span class="tooltip" title=''>
																	
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注">
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
									<tr></tr>
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
