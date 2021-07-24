<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BMonthCompletedStatistics.aspx.cs" Inherits="ContractManage_ContractReport_BMonthCompletedStatistics" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/ProjectTree.js"></script>
	<link href="/Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript" src="/Script/DecimalInput.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$("#btnQuery")[0].onclick = function () {
				if (!$('#form1').form('validate')) {
					return false;
				}
			}
			var jwTable = new JustWinTable('gvwPrjBudgetStatistics');
			replaceEmptyTable('emptyTask', 'gvwPrjBudgetStatistics');
			//清单外，字体红色显示
			$('#gvwPrjBudgetStatistics').find("tr").each(function () {
				var type = $(this).attr('ModifyType');
				$(this).css("text-align", "Right");
			});
		});
	</script>
</head>
<body>
	<form id="form1" style="overflow: hidden" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%;">
		<table width="100%" cellpadding="0" cellspacing="0">
			<tr>
				<td>
					<table class="queryTable" cellpadding="3px" cellspacing="0px">
						<tr>
							<td class="descTd">
								清单编号
							</td>
							<td>
								<asp:TextBox ID="txtCode" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[50]'" runat="server"></asp:TextBox>
							</td>
							<td class="descTd">
								名称
							</td>
							<td>
								<asp:TextBox ID="txtName" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[50]'" runat="server"></asp:TextBox>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
					<table border="0" class="tab">
						<tr>
							<td class="divFooter" style="text-align: left">
								<asp:Button ID="btnQuery" Text="查询" Style="cursor: pointer;" OnClick="btnQuery_Click" runat="server" />
								<asp:Button ID="btnExecl" CssClass="button-normal" Text="导出Excel" Width="80px" OnClick="btnExecl_Click" runat="server" />
							</td>
						</tr>
						<tr>
							<td style="width: 100%; background-color: rgb(238,242,242);" valign="top">
								<asp:GridView ID="gvwPrjBudgetStatistics" Width="100%" AutoGenerateColumns="false" AllowPaging="true" CssClass="gvdata" ShowFooter="true" OnRowDataBound="gvwPrjBudgetStatistics_RowDataBound" OnPageIndexChanging="gvwPrjBudgetStatistics_PageIndexChanging" DataKeyNames="TaskID,ModifyType" runat="server">
<EmptyDataTemplate>
										<table id="emptyTask" class="tab" width="100%">
											<tr class="header" style="width: 100%; color: rgb(114,127,127)">
												<td style="width: 25px;">
													序号
												</td>
												<td align="center" style="width: 150px;">
													编号
												</td>
												<td align="center" style="width: 150px;">
													名称
												</td>
												<td style="width: 30px;">
													单位
												</td>
												<td align="center" style="width: 150px;">
													单价(元)
												</td>
												<td align="center" style="width: 50px;">
													工程量
												</td>
												<td align="center" style="width: 50px;">
													小计(元)
												</td>
												<td>
													变更后工程量
												</td>
												<td align="center" style="width: 50px;">
													变更后小计(元)
												</td>
												<td style="width: 50px;">
													本月完成工程量
												</td>
												<td style="width: 50px;">
													累计完成工程量
												</td>
												<td style="width: 50px;">
													累计完成产值（元）
												</td>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="编号" DataField="TaskCode" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" /><asp:BoundField HeaderText="名称" DataField="TaskName" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" /><asp:BoundField HeaderText="单位" DataField="Unit" HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="Left" /><asp:BoundField HeaderText="单价" DataField="UnitPrice" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="decimal_input" /><asp:BoundField HeaderText="工程量" DataField="Quantity" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="小计(元)" DataField="Total" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="变更后工程量" DataField="MQuantity" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="变更后小计(元)" DataField="MTotal" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="本月产值(元)" DataField="CAmount" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="累计完成产值(元)" DataField="TAmount" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hfldPreTotal" Value="0" runat="server" />
	<asp:HiddenField ID="hfldCurTotal" Value="0" runat="server" />
	<asp:HiddenField ID="hfldCueMonthTotal" Value="0" runat="server" />
	<asp:HiddenField ID="hfldAggregateTotal" Value="0" runat="server" />
	</form>
</body>
</html>
