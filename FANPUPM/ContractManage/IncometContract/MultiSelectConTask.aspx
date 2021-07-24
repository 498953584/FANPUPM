<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultiSelectConTask.aspx.cs" Inherits="ContractManage_IncometContract_MultiSelectConTask" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>选择中标预算节点</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var table = new JustWinTable('gvwConTask');
			replaceEmptyTable('emptyTable', 'gvwConTask');
			table.registClickSingleCHKListener(function () {
				var checkCHK = table.getCheckedChk();
				if (checkCHK.length > 0) {
					$('#btnOk').removeAttr('disabled');
				} else {
					$('#btnOk').attr('disabled', 'disabled');
				}
			});
			table.registClickTrListener(function () {
				$('#btnOk').removeAttr('disabled');
			});
			table.registClickAllCHKLitener(function () {
				var checkCHK = table.getAllChk();
				if (checkCHK.length > 0) {
					$('#btnOk').removeAttr('disabled');
				} else {
					$('#btnOk').attr('disabled', 'disabled');
				}
			});
			$('#btnBindTask').css('display', 'none');
			// 绑定双击事件
			$('#gvwConTask').delegate('tr', 'dblclick', btnOk_click);

			jw.formatTreegrid('gvwConTask', 2);
		});

		// 确定按钮事件
		function btnOk_click() {
			var idArr = new Array();
			$('#gvwConTask input:checked').each(function () {
				var thisTr = $(this).parents('tr');
				var id = thisTr.attr('id');
				var subCount = thisTr.attr('subCount');
				if (id && subCount ) {
					idArr.push(id);
				}
			});

			// 执行回调方法
			if (top.ui.callback != null && idArr.length > 0) {
				top.ui.callback(jw.array1dToJson(idArr));
				top.ui.callback = null
			}
			top.ui.closeWin();
		}

		// 取消按钮事件
		function btnCancel_click() {
			top.ui.closeWin();
		}

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<div style="width: 100%; height: 375px; overflow: auto; float: left;">
			<asp:GridView ID="gvwConTask" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwConTask_RowDataBound" DataKeyNames="TaskId,OrderNumber" runat="server">
<EmptyDataTemplate>
					<table id="emptyTable">
						<tr>
							<th scope="col" style="width: 25px">
							</th>
							<th scope="col" style="width: 25px">
								序号
							</th>
							<th scope="col">
								任务名称
							</th>
							<th scope="col">
								任务编码
							</th>
							<th scope="col">
								单位
							</th>
							<th scope="col">
								工程量
							</th>
							<th scope="col">
								综合单价
							</th>
							<th scope="col">
								小计
							</th>
							<th scope="col">
								类型
							</th>
						</tr>
					</table>
				</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
							<asp:CheckBox ID="chkAll" runat="server" />
						</HeaderTemplate><ItemTemplate>
							<asp:CheckBox ID="chkSingle" runat="server" />
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
							<%# Container.DataItemIndex + 1 %>
						</ItemTemplate></asp:TemplateField><asp:BoundField DataField="TaskName" HeaderText="任务名称" /><asp:BoundField DataField="TaskCode" HeaderText="任务编码" /><asp:BoundField DataField="Unit" HeaderText="单位" ItemStyle-HorizontalAlign="Right" /><asp:TemplateField HeaderText="工程量" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
							<%# Eval("Quantity") %>
						</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="综合单价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
							<%# Eval("UnitPrice") %>
						</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
							<%# Eval("Total") %>
						</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="类型">
<ItemTemplate>
							<%# GetTypeByOrderNumber(Eval("OrderNumber").ToString()) %>
						</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
		</div>
		<div style="width: 100%; height: 25px; float: left; text-align: right; vertical-align: middle">
			<input type="button" id="btnOk" value="确定" onclick="btnOk_click();" disabled="disabled"
				style="margin: 10px;" />
			<input type="button" id="btnCancel" value="取消" onclick="btnCancel_click();" />
		</div>
	</div>
	<asp:Button ID="btnBindTask" Text="Button" OnClick="btnBindTask_Click" runat="server" />
	<asp:HiddenField ID="hfldprjId" runat="server" />
	<asp:HiddenField ID="hfldContractId" runat="server" />
	</form>
</body>
</html>
