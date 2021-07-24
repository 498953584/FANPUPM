<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutCalcEdit.aspx.cs" Inherits="ContractManage_PayoutPayment_PayoutCalcEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {

		});

		// 选择任务
		function addTask() {
			var contractId = jw.getRequestParam('contractId');
			var url = '/ContractManage/PayoutContract/PayoutTarget.aspx?type=con'
				+ '&prjId=' + $('#hfldPrjId').val()
				+ '&contractId=' + contractId;

			top.ui.callback = function (t) {
				$('#hfldRetTaskId').val(t.taskId);
				$('#btnRefreshPage').click();
			}

			top.ui.openWin({ title: '选择目标成本', url: url, winNo: 2, width: 1000, height: 500 });
			var url = '';
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		合同名称：
		<asp:Label ID="lblContractName" Width="120px" runat="server"></asp:Label>
		上报日期：
		<asp:TextBox ID="TextBox1" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
		<input type="button" id="btnAddTask" value="选择任务" style="width: 70px;" onclick="addTask();" />
		<input type="button" id="btnDelTask" value="删除" onclick="delTask();" />
		<asp:GridView ID="gvwTask" CssClass="gvdata" runat="server"></asp:GridView>
	</div>
	<!-- 项目Id-->
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	<!-- 返回的任务Id-->
	<asp:HiddenField ID="hfldRetTaskId" runat="server" />
	<!-- 刷新页面-->
	<asp:Button ID="btnRefreshPage" Style="display: none;" OnClick="btnRefreshPage_Click" runat="server" />
	</form>
</body>
</html>
