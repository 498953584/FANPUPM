<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddIncometPaymentApply.aspx.cs" Inherits="ContractManage_PaymentApply_AddIncometPaymentApply" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>收入合同资金支付</title>
	<style type="text/css">
		.groupInfo
		{
			text-align: left;
			padding-left: 50px;
			font-weight: bold;
		}
		
		.tableState
		{
			width: 100%;
			text-align: center; /*table-layout: fixed;*/
			border-collapse: separate;
			border-spacing: 0px 0px;
			border-right: solid 1px;
			border-bottom: solid 1px;
			border-color: #CADEED;
		}
		
		
		.tableState td
		{
			white-space: nowrap; /*overflow: hidden;*/
			padding-left: 1px;
			border-left: solid 1px;
			border-top: solid 1px;
			font-weight: normal;
			border-color: #CADEED;
		}
	</style>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script src="../../Script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script src="/Script/jquery.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			registerCancelEvent();
			var action = document.getElementById('hlfdAction').value;
			if (action == 'Query' || action == '') {
				setAllInputDisabled();
			}
			if (action == 'Update') {
				document.getElementById('txtPaymentCode').readOnly = "readOnly";
				document.getElementById('txtPaymentCode').setAttribute('disabled', 'disabled');
			}
			Val.validate('form1');
			setAuditState();
		});

		function rowQueryA(para) {
			if (para == 'IncometPayment') {
				parent.desktop.ViewPaymentList = window;
				var path = '/ContractManage/IncometPayment/ViewPaymentList.aspx?contractId=' + document.getElementById('hlfdContractId').value;
				toolbox_oncommand(path, "查看收入合同收款信息");
			} else if (para == 'PaymentApply') {
				parent.desktop.ViewPaymentApplyList = window;
				var path = '/ContractManage/PaymentApply/ViewPaymentApplyList.aspx?contractId=' + document.getElementById('hlfdContractId').value;
				toolbox_oncommand(path, "查看收入合同资金支付信息");
			}
		}

		function registerCancelEvent() {
			var btnCancel = document.getElementById('btnCancel');
			addEvent(btnCancel, 'click', function () {
				winclose('PaymentEdit', 'PayoutPaymentEdit.aspx', false)
			})
		}

		function ConvertRMB(para) {
			var strRMB;
			$.ajax({
				type: 'GET',
				async: false,
				url: '/ContractManage/Handler/ConvertRMB.ashx?' + new Date().getTime() + '&para=' + document.getElementById('txtPaymentMoney').value,
				success: function (data) {
					strRMB = data;
				}
			});
			document.getElementById('TxtCapitalNumber').value = strRMB;
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader2">
		<table class="tableHeader2">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="支出合同付款" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div id="divContent" class="divContent2">
		<table id="tableContent" class="tableContent2" cellpadding="5px" cellspacing="0">
			<tr>
				<td colspan="4" class="groupInfo">
					合同基本信息
				</td>
			</tr>
			<tr>
				<td class="word">
					合同编号
				</td>
				<td class="elemTd readonly">
					<asp:TextBox ID="txtContractCode" ReadOnly="true" Height="15px" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					合同名称
				</td>
				<td class="elemTd readonly">
					<asp:TextBox ID="txtContractName" Height="15px" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					合同最终金额
				</td>
				<td class="elemTd readonly">
					<asp:TextBox ID="txtContractMoney" Height="15px" CssClass="decimal_input" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					签订时间
				</td>
				<td class="elemTd readonly">
					<asp:TextBox ID="txtSignedDate" Height="15px" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td colspan="4" class="groupInfo">
					付款单信息
				</td>
			</tr>
			<tr>
				<td class="word">
					收款累计
				</td>
				<td class="elemTd">
					<a href="#" class="al" onclick="rowQueryA('IncometPayment');">
						<asp:Label ID="lblPaymentSum" CssClass="decimal_input" runat="server"></asp:Label>
					</a>
				</td>
				<td class="word">
					付款累计
				</td>
				<td class="elemTd">
					<a href="#" class="al" onclick="rowQueryA('PaymentApply');">
						<asp:Label ID="lblPaySum" CssClass="decimal_input" runat="server"></asp:Label>
					</a>
				</td>
			</tr>
			<tr>
				<td class="word">
					差额
				</td>
				<td class="elemTd readonly">
					<asp:TextBox ID="txtDiff" Height="15px" CssClass="decimal_input" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					<asp:CheckBox ID="chkContainPending" AutoPostBack="true" OnCheckedChanged="chkContainPendint_CheckedChanged" runat="server" />
				</td>
				<td class="elemTd">
					包含待审数据
				</td>
			</tr>
			<tr>
				<td class="word">
					支付编号
				</td>
				<td class="elemTd mustInput">
					
					<input type="text" id="txtPaymentCode" class="{required:true, messages:{required:'支付编号必须输入!'}}" runat="server" />

				</td>
				<td class="word">
					申请人
				</td>
				<td class="elemTd mustInput">
					<asp:TextBox ID="txtPaymentPerson" CssClass="{required:true, messages:{required:'申请人必须输入!'}}" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					本次支付金额
				</td>
				<td class="elemTd mustInput">
					<asp:TextBox ID="txtPaymentMoney" CssClass="decimal_input" Text="0.000" onblur="ConvertRMB();" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					大写金额
				</td>
				<td class="elemTd mustInput readonly">
					
					<input type="text" id="TxtCapitalNumber" readonly="readonly" runat="server" />

				</td>
			</tr>
			<tr>
				<td class="word">
					要求支付日期
				</td>
				<td class="elemTd mustInput">
					
					<asp:TextBox ID="txtPaymentDate" onClick="WdatePicker()" CssClass="{required:true, messages:{required:'要求支付日期必须输入'}}" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					支付方式
				</td>
				<td>
					<asp:RadioButtonList ID="RblPayType" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server"><asp:ListItem Value="0" Text="现金" /><asp:ListItem Value="1" Text="支票" /><asp:ListItem Value="2" Text="转账" /></asp:RadioButtonList>
				</td>
			</tr>
			<tr>
				<td class="word">
					备注
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtNotes" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					附件
				</td>
				<td colspan="3" style="padding-right: 0px;">
					<MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="word">
					录入人
				</td>
				<td class="elemTd mustInput readonly">
					<asp:TextBox ID="txtInputPerson" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					录入时间
				</td>
				<td class="elemTd mustInput readonly">
					<asp:TextBox ReadOnly="true" ID="txtInputDate" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr id="trSate" runat="server"><td colspan="4" style="text-align: center; padding-left: 30px" runat="server">
					<table class="tableState" cellpadding="0px" cellspacing="0px">
						<tr>
							<td colspan="5">
								指标对比表
							</td>
						</tr>
						<tr>
							<td>
								状态
							</td>
							<td>
								已结算金额
							</td>
							<td>
								已支付金额
							</td>
							<td>
								本次支付金额
							</td>
							<td>
								差额比例
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label ID="lblState" CssClass="alarm" Text="" runat="server"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblBalancedAmount" CssClass="alarm" Text="" runat="server"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblPaymentedAmount" CssClass="alarm" Text="" runat="server"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblPaymentAmount" CssClass="alarm" Text="" runat="server"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblRate" CssClass="alarm" Text="" runat="server"></asp:Label>
							</td>
						</tr>
						<tr>
							<td colspan="5" style="text-align: left">
								<div style="width: 12%; float: left">
									预警级别：
								</div>
								<div style="width: 88%; float: left">
									高：红色字体，表示已超；中：紫色字体；低：蓝色字体；
									<br />
									普通：黑色字体，表示未达到预警阀值，正常。
								</div>
							</td>
						</tr>
					</table>
				</td></tr>
		</table>
	</div>
	<div id="divFooter" class="divFooter2">
		<table class="tableFooter2">
			<tr>
				<td>
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
					<asp:HiddenField ID="hlfdAction" runat="server" />
					<asp:HiddenField ID="hlfdContractId" runat="server" />
					<input type="button" id="btnCancel" value="取消" />
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
