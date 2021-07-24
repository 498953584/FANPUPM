<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TypeAndState.aspx.cs" Inherits="oa_Vehicle_TypeAndState_TypeAndState" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
	<script src="../../../Script/jquery.js" type="text/javascript"></script>
	<script src="../../../SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
	<script src="../../../StockManage/script/Config2.js" type="text/javascript"></script>
	<script src="../../../StockManage/script/JustWinTable.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			hideControls();
			var typeTable = new JustWinTable('VehicleType');
			setButton(typeTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldContractTypeGuid')
			typeTable.setBtnStateByJustwinTable('btnUserCodes');
			replaceEmptyTable();
			registerBtnAddEvent();
			registerBtnUpdateEvent();
			registerBtnDeleteEvent();
			registerBtnQueryEvent();
		});

		function registerBtnAddEvent() {
			var c = $("#VehicleType tr").size();
			var d = $("#vac").val();
			var N = $("#NN").val();
			if (N == "N") {
				$("#hfldSelectedPrj").val("00000000-0000-0000-0000-000000000000");
			}
			if (parseInt(c) == 3 && parseInt(d) == 1) {
				$("#btnAdd").attr("disabled", "disabled");
				$("#btnDelete").attr("disabled", "disabled");
			}

			$('#btnAdd').click(function () {
				var url = '/oa/Vehicle/TypeAndState/TypeAndStateEdit.aspx?Action=Add&PrjGuid=' + $('#hfldSelectedPrj').val();
				top.ui.openWin({ title: '新增车辆类型', url: url, width: 380, height: 220 });
			});
		}

		function registerBtnUpdateEvent() {
			$('#btnUpdate').click(function () {
				var url = '/oa/Vehicle/TypeAndState/TypeAndStateEdit.aspx?Action=Update&TypeID=' + $('#hfldContractTypeGuid').val() + '&PrjGuid=' + $('#hfldSelectedPrj').val();
				top.ui.openWin({ title: '编辑车辆类型', url: url, width: 380, height: 220 });
			});
		}

		function registerBtnDeleteEvent() {
			if (!document.getElementById('btnDelete')) return;
			var btnDelete = document.getElementById('btnDelete');
			btnDelete.onclick = function () {
				if (!confirm('系统提示：\n\n确定要删除吗？')) {
					return false;
				}
			}
		}

		function registerBtnQueryEvent() {
			if (!document.getElementById('btnQuery')) return;
			var btnQuery = document.getElementById('btnQuery');
			addEvent(btnQuery, 'click', function () {
				var url = '/oa/Vehicle/TypeAndState/TypeAndStateEdit.aspx?Action=Query&TypeID=' + document.getElementById('hfldContractTypeGuid').value;
				top.ui.openWin({ title: '查看车辆类型', url: url, width: 380, height: 220 });
			})
		}
		function replaceEmptyTable() {
			//当数据量为空时，修改样式
			if (!document.getElementById('VehicleType')) return;
			if (!document.getElementById('emptyContractType')) return;
			var VehicleType = document.getElementById('VehicleType');
			var emptyContractType = document.getElementById('emptyContractType');
			if (VehicleType.firstChild.childNodes.length == 1) {
				VehicleType.replaceChild(emptyContractType.firstChild, VehicleType.firstChild);
			}
		}
		function hideControls() {
			if (!document.getElementById('btnDataBind')) return;
			document.getElementById('btnDataBind').style.display = 'none';
		}
		$(function () {
			$("input[type='checkbox']").click(function () {
				var c = $("#VehicleType tr").size();
				var d = $("#vac").val();
				if (parseInt(c) == 3 && parseInt(d) == 1) {
					$("#btnDelete").attr("disabled", "disabled");
				}
			});

			$("#VehicleType tr").click(function () {
				var c = $("#VehicleType tr").size();
				var d = $("#vac").val();
				if (parseInt(c) == 3 && parseInt(d) == 1) {
					$("#btnDelete").attr("disabled", "disabled");
				}
			});
		});
	</script>
	<link href="/StockManage/skins/sm4.css" rel="stylesheet" type="text/css" />
<link href="/StyleCss/Common.css" rel="stylesheet" type="text/css" />

	<style type="text/css">
		#tred
		{
			margin-top: 5px;
			margin-left: 5px;
		}
		.tred
		{
			margin-top: 4px;
		}
		.newStyle1
		{
			vertical-align: text-top;
			text-align: left;
		}
		#container
		{
			margin: 0 auto;
			width: 100%;
		}
		#sidebar
		{
			float: left;
			width: 20%;
		}
		* html #sidebar
		{
			margin-right: -3px;
		}
		/*使用此方法解决ie 3像素bug可通过验证*/
		#content
		{
			width: 80%;
		}
		.style1
		{
			width: 100%;
		}
		.style2
		{
			width: 251px;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<table class="tab" style="vertical-align: top; width: 100%;">
		<tr>
			<td colspan="2" class="divHeader" style="line-height: 28px;">
				车辆类型和状态
			</td>
		</tr>
		<tr>
			<td class="style2" style="height: 95%; width: 18%;">
				<div style="height: 100%; margin-top: 0px; overflow: auto;">
					<asp:TreeView ID="trvwProject" ShowLines="true" OnSelectedNodeChanged="trvwProject_SelectedNodeChanged" runat="server"><SelectedNodeStyle Font-Underline="true" ForeColor="#5555DD" HorizontalPadding="0px" CssClass="selectTree" VerticalPadding="0px" /></asp:TreeView>
				</div>
			</td>
			<td style="vertical-align: top">
				<div class="divFooter" style="text-align: left">
					<input type="button" disabled="true" id="btnAdd" value="新增" runat="server" />

					<input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
					<asp:Button ID="btnDelete" Text="删除" Enabled="false" OnClick="btnDelete_Click" runat="server" />
					<input type="button" id="btnQuery" value="查看" disabled="disabled" />
				</div>
				<asp:GridView ID="VehicleType" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvwContractType_RowDataBound" DataKeyNames="guid" runat="server">
<EmptyDataTemplate>
						<table id="emptyContractType" class="gvdata">
							<tr class="header">
								<th scope="col" style="width: 20px;">
									<input id="chk1" type="checkbox" />
								</th>
								<th scope="col" style="width: 25px;">
									序号
								</th>
								<th scope="col">
									类型编码
								</th>
								<th scope="col">
									类型名称
								</th>
								<th scope="col">
									性质
								</th>
							</tr>
						</table>
					</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
								<asp:CheckBox ID="chkSelectAll" runat="server" />
							</HeaderTemplate><ItemTemplate>
								<asp:CheckBox ID="chkSelectSingle" runat="server" />
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
								<%# Container.DataItemIndex + 1 %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="编码"><ItemTemplate>
								<%# Eval("code") %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="名称"><ItemTemplate>
								<%# Eval("Name") %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型/状态"><ItemTemplate>
								<%# (Eval("Type") == null) ? "" : ((Eval("Type").ToString() == "0") ? "类型" : "状态") %>
							</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="NN" runat="server" />
	<asp:HiddenField ID="vac" runat="server" />
	<asp:HiddenField ID="hfldContractTypeGuid" runat="server" />
	<asp:HiddenField ID="hfldSelectedPrj" runat="server" />
	</form>
</body>
</html>
<script language="javascript" type="text/javascript">
	$(function () {
		var c = $("#vac").val();
		if (c == "1") {
			$("#btnAdd").attr("disabled", "disabled");
		} else {
			$("#btnAdd").removeAttr("disabled");
		}
	});
</script>
