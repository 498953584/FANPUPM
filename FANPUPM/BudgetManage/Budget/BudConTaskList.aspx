<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="BudConTaskList.aspx.cs" Inherits="BudgetManage_Budget_BudConTaskList" MaintainScrollPositionOnPostBack="true" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>任务选择</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jwJson.js"></script>
	<script type="text/javascript" src="../../Script/jquery.treetable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.blockUI.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			showTooltip('tooltip', 10);
			var spType = getRequestParam('spType');
			$('#gvBudget').treetable({
				handlerUrl: '/BudgetManage/Handler/GetChildConstractTask.ashx'
			})
			replaceEmptyTable('emptyBudget', 'gvBudget');
			$('#gvBudget tr:gt(0)').live('click', function () {
				var resCount;
				var $tr = $('<TR id=' + this.id + ' code=' + this.code + '>' + this.innerHTML + '</TR>');
				if (spType == 'in') {
					$('#btnSave').removeAttr('disabled');
					$('#hfldCheckedIds').val(this.id);
				}
				else {
					//资源与WBS挂钩时，如果是清单外的，判断选择的是否增加过资源，如果是配置过资源的节点，不能增加子节点，如果没有，可以增加子节点
					$.ajax({
						type: 'GET',
						async: false,
						url: '/BudgetManage/Handler/IsLeafNode.ashx?' + new Date().getTime() + '&Type=ContractTask&Id=' + this.id,
						success: function (data) {
							resCount = data;
						}
					});
					//resCount = 0， 表示没有配置资源，可以变更
					if (resCount == 0) {
						$('#btnSave').removeAttr('disabled');
						$('#hfldCheckedIds').val(this.id);
					} else {
						$('#btnSave').attr('disabled', 'disabled');
					}
				}
			});

			$('#btnCancel').click(function () {
				top.ui.closeWin();
			});
			$('#btnSave').click(save);
			// 绑定双击事件
			$('#gvBudget').delegate('tr', 'dblclick', save);
		});

		function save() {
			var taskId = $('#hfldCheckedIds').val();
			top.ui.callback(taskId);
			top.ui.closeWin();
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div style="width: 100%;">
		<div style="width: 100%; height: 430px; overflow: auto; float: left;">
			<asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwResource_RowDataBound" DataKeyNames="TaskId,SubCount,OrderNumber" runat="server">
<EmptyDataTemplate>
					<table id="emptyBudget" class="gvdata">
						<tr class="header">
							<th scope="col" style="width: 25px;">
								序号
							</th>
							<th scope="col">
								名称
							</th>
							<th scope="col">
								编码
							</th>
							<th scope="col">
								类型
							</th>
							<th scope="col">
								单位
							</th>
							<th scope="col">
								工程量
							</th>
							<th scope="col">
								开始时间
							</th>
							<th scope="col">
								结束时间
							</th>
							<th scope="col">
								综合单价
							</th>
							<th scope="col">
								小计
							</th>
							<th scope="col">
								备注
							</th>
							<td id="Th1" scope="col" visible="false" runat="server">
								排序
							</td>
						</tr>
					</table>
				</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
							<%# Eval("No") %>
						</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="编码"><ItemTemplate>
							<span class="tooltip" title='<%# Eval("TaskCode") %>'>
								<%# StringUtility.GetStr(Eval("TaskCode").ToString(), 20) %>
							</span>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="名称"><ItemTemplate>
							<span class="tooltip" title='<%# Eval("TaskName") %>'>
								<%# StringUtility.GetStr(Eval("TaskName").ToString(), 20) %>
							</span>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型"><ItemTemplate>
							<%# Eval("TypeName") %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
							<%# Eval("Unit") %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程量" HeaderStyle-Width="70px">
<ItemTemplate>
							<%# Eval("Quantity") %>
						</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="综合单价" HeaderStyle-Width="70px">
<ItemTemplate>
							<%# System.Convert.ToDecimal(Eval("UnitPrice")).ToString("0.000") %>
						</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="小计" HeaderStyle-Width="70px">
<ItemTemplate>
							<%# System.Convert.ToDecimal(Eval("Total")).ToString("0.000") %>
						</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="50px"><ItemTemplate>
							<%# Common2.GetTime(Eval("StartDate")) %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="50px"><ItemTemplate>
							<%# Common2.GetTime(Eval("EndDate")) %>
						</ItemTemplate></asp:TemplateField><asp:BoundField DataField="ConstructionPeriod" HeaderText="工期(天)" /><asp:TemplateField HeaderText="备注"><ItemTemplate>
							<span class="tooltip" title='<%# Eval("Note") %>'>
								<%# StringUtility.GetStr(Eval("Note").ToString()) %>
							</span>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="排序" Visible="false"><ItemTemplate>
							<%# StringUtility.GetStr(System.Convert.ToString(Eval("OrderNumber"))) %>
						</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
		</div>
		<div id="divFooter2" class="divFooter2">
			<table class="tableFooter2">
				<tr>
					<td>
						<input type="button" id="btnSave" disabled="disabled" value="保存" />
						<input type="button" id="btnCancel" value="取消" />
					</td>
				</tr>
			</table>
		</div>
	</div>
	<asp:HiddenField ID="hfldCheckedIds" runat="server" />
	<asp:HiddenField ID="hfldType" runat="server" />
	</form>
</body>
</html>
