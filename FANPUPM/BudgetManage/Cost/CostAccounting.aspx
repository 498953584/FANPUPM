<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostAccounting.aspx.cs" Inherits="BudgetManage_Cost_CostAccounting" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/Budget/BudgetPait.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript">
		$(document).ready(function () {
			addEvent(document.getElementById('btnAdd'), 'click', addCost);
			addEvent(document.getElementById('btnUpdate'), 'click', updateCost);
			var gvBudget = new JustWinTable('gvBudget');

			showTooltip('tooltip', 25);

			setWidthHeight();

			gvBudget.registClickTrListener(function () {
				$('#hfldChecked').val(this.id);

				var len = $(this).find('.cbscode').html().length;
				if (len == 3) {
					$('#btnUpdate, #btnDel, #btnAdd').attr('disabled', 'disabled');
				}
				if (len == 6) {
					$('#btnAdd').removeAttr('disabled');
					$('#btnUpdate, #btnDel').attr('disabled', 'disabled');
				}
				if (len > 6) {
					$('#btnAdd').removeAttr('disabled');

					// 检查该CBS是否可以编辑或者删除
					var code = $(this).attr('code')
					var isEdit = checkCBSCode(code);

					if (isEdit == '1') {
						$('#btnUpdate, #btnDel').removeAttr('disabled');
					} else {
						$('#btnUpdate, #btnDel').attr('disabled', 'disabled');
					}
				}
			});
		});

		function setWidthHeight() {
			$('#divBudget').height($(this).height() - $('#divHead').height() - 5);
		}

		// 新增
		function addCost() {
			top.ui._costaccounting = window;
			var url = "/BudgetManage/Cost/CostAccountingEdit.aspx?Action=add&parent=" + $('#hfldChecked').val();
			toolbox_oncommand(url, "新增核算成本");
		}

		// 编辑
		function updateCost() {
			top.ui._costaccounting = window;
			var url = "/BudgetManage/Cost/CostAccountingEdit.aspx?Action=update&parent=" + $('#hfldChecked').val();
			toolbox_oncommand(url, "修改核算成本");
		}

		// 检查该CBS是否允许编辑
		function checkCBSCode(CBSCode) {
			var isEdit = null;
			$.ajax({
				type: 'GET',
				async: false,
				url: '/BudgetManage/Handler/IsDeleteCBSCode.ashx?' + new Date().getTime() + '&CBSCode=' + CBSCode,
				success: function (data) {
					isEdit = data;
				}
			});
			return isEdit;
		}

		// 本页面不需要提升管理员权限
		function upAdminPrivilege() {

		}

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table style="width: 100%;">
		<tr>
			<td>
				<div class="divFooter" style="text-align: left;" id="divHead">
					<input type="button" id="btnAdd" value="新 增" disabled="disabled" />
					<input type="button" id="btnUpdate" value="编 辑" disabled="disabled" />
					<asp:Button ID="btnDel" Text="删 除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDel_Click" runat="server" />
				</div>
			</td>
		</tr>
		<tr>
			<td>
				<div id="divBudget" class="pagediv">
					<asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gv_Budget_RowDataBound" DataKeyNames="Id,Code" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="成本名称"><ItemTemplate>
									<span style="vertical-align: middle;">
										<%# Eval("Name") %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:BoundField DataField="Code" HeaderText="CBS编码" ItemStyle-CssClass="cbscode" ItemStyle-Width="100px" /><asp:BoundField DataField="Type" HeaderText="成本类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" /><asp:TemplateField HeaderText="备注"><ItemTemplate>
									<asp:HyperLink ID="hlinkShowNote" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("Note").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(Eval("Note").ToString(), 25) %></asp:HyperLink>
								</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					
					<asp:HiddenField ID="hfldChecked" runat="server" />
				</div>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
