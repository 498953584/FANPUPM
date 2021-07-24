<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostVerifyEdit.aspx.cs" Inherits="BudgetManage_Cost_CostVerifyEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>费用核销</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/DecimalInput.js"></script>
	<script type="text/javascript">

		$(document).ready(function () {
			var detailJtbl = new JustWinTable('gvwDiaryDetails');
		});

		// 选择备用金
		function selectPettyCash() {
			top.ui.callback = function (obj) {
				$('#hfldPettyCashId').val(obj.Id);
				$('#txtConType').val(obj.Matter);
			}
			var url = '/PettyCash/SelectPettyCashaspx.aspx';
			top.ui.openWin({ title: '选择备用金', url: url });
		}
	</script>
</head>
<body class="easyui-layout">
	<form id="form1" runat="server">
	<div data-options="region:'north', title: '预报销',border:false" style="height: 148px">
		<table class="viewTable">
			<tr>
				<td class="descTd">
					费用名称
				</td>
				<td class="elemTd">
					<asp:Label ID="lblName" runat="server"></asp:Label>
				</td>
				<td class="descTd">
					发生时间
				</td>
				<td class="elemTd">
					<asp:Label ID="lblInputDate" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td class="descTd">
					发生单位
				</td>
				<td class="elemTd">
					<asp:Label ID="lblDepartment" runat="server"></asp:Label>
				</td>
				<td class="descTd">
					填报人
				</td>
				<td class="elemTd">
					<asp:Label ID="lblInputUser" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td class="descTd">
					经手人
				</td>
				<td class="elemTd">
					<asp:Label ID="lblIssueBy" runat="server"></asp:Label>
				</td>
				<td class="descTd">
					费用编号
				</td>
				<td class="elemTd">
					<asp:Label ID="lblCode" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td class="descTd">
					<asp:Literal ID="ltlCostTypeName" Text="项目名称" runat="server"></asp:Literal>
				</td>
				<td class="elemTd">
					<asp:Label ID="lblPrjName" runat="server"></asp:Label>
				</td>
				<td class="descTd">
					偿还备用金
				</td>
				<td class="elemTd">
					<asp:Label ID="lblPettyCash" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div data-options="region:'center', title: '预报销明细', split:true">
		<asp:GridView ID="gvwDiaryDetails" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwDiaryDetails_RowDataBound" DataKeyNames="InDiaryDetailsId" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
						<%# Container.DataItemIndex + 1 %>
					</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="Name" HeaderText="名称" HeaderStyle-Width="200px" /><asp:BoundField DataField="Amount" HeaderText="金额" HeaderStyle-Width="100px" HeaderStyle-CssClass="decimal_input" HeaderStyle-HorizontalAlign="Right" /><asp:TemplateField HeaderText="核销金额" HeaderStyle-Width="150px"><ItemTemplate>
						<asp:TextBox ID="txtAuditAmount" CssClass="decimal_input" Width="150px" Text='<%# System.Convert.ToString(Eval("Amount"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
					</ItemTemplate></asp:TemplateField><asp:BoundField DataField="Note" HeaderText="备注" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
	</div>
	<div data-options="region:'south', split:true" style="height: 37px; text-align: right;
		vertical-align: middle; padding-top: 5px;">
		<asp:Button ID="btnOk" Text="确定" OnClick="btnOk_Click" runat="server" />
		<input type="button" id="btnCancel" value="取消" onclick="top.ui.closeTab();" />
	</div>
	</form>
</body>
</html>
