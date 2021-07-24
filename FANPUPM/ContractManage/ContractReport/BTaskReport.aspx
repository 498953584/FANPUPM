<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BTaskReport.aspx.cs" Inherits="ContractManage_ContractReport_BTaskReport" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>


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
    <script src="../../Script/jw.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/ProjectTree.js"></script>
	<link href="/Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$("#btnQuery")[0].onclick = function () {
				if (!$('#form1').form('validate')) {
					return false;
				}
			}
			var jwTable = new JustWinTable('gvwBTaskRep');
			replaceEmptyTable('emptyTask', 'gvwBTaskRep');
			//清单外，字体红色显示
			$('#gvwBTaskRep').find("tr").each(function () {
				var type = $(this).attr('ModifyType');
				if (type == "0") {
					$(this).css("color", "Red");
				}
			});
		});
	</script>
	<style type="text/css">
		.color
		{
			color: Red;
		}
	</style>
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
								<asp:GridView ID="gvwBTaskRep" Width="100%" AutoGenerateColumns="false" CssClass="gvdata" ShowFooter="false" OnRowDataBound="gvwBTaskRep_RowDataBound" DataKeyNames="TaskId,ModifyType,Quantity,Total2" runat="server">
<EmptyDataTemplate>
										<table id="emptyTask" class="tab" width="100%">
											<tr class="header" style="width: 100%;">
												<td style="width: 25px;">
													序号
												</td>
												<td align="center" style="width: 150px;">
													清单编号
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
													变更前工程量
												</td>
												<td align="center" style="width: 50px;">
													变更前小计(元)
												</td>
												<td>
													变更后工程量
												</td>
												<td align="center" style="width: 50px;">
													变更后小计(元)
												</td>
												<td style="width: 50px;">
													增减工程量
												</td>
												<td style="border-right: 0px;" style="width: 50px;">
													增减金额（元）
												</td>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="清单编号" DataField="TaskCode" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" /><asp:BoundField HeaderText="名称" DataField="TaskName" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" /><asp:BoundField HeaderText="单位" DataField="Unit" HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="Left" /><asp:BoundField HeaderText="单价" DataField="UnitPrice" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="decimal_input" /><asp:BoundField HeaderText="变更前工程量" DataField="oQty" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="decimal_input" /><asp:BoundField HeaderText="变更前小计(元)" DataField="oTotal2" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="decimal_input" /><asp:BoundField HeaderText="变更后工程量" DataField="CQuantity" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="变更后小计(元)" DataField="CTotal" HeaderStyle-Width="50px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="增减工程量" DataField="Quantity" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="decimal_input" /><asp:BoundField HeaderText="增减金额(元)" DataField="Total2" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="decimal_input" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
								<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
								</webdiyer:AspNetPager>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hfldPreTotal" Value="0" runat="server" />
	<asp:HiddenField ID="hfldCurTotal" Value="0" runat="server" />
	<asp:HiddenField ID="hfldChangedTotal" Value="0" runat="server" />
	</form>
</body>
</html>
