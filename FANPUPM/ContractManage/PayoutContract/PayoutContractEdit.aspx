<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutContractEdit.aspx.cs" Inherits="ContractManage_PayoutContract_PayoutContractEdit" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>支出合同</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
		    $('#btnBindResource').hide();
		    $('#BtnShowPrjTypeName').hide();
			$('#btnSave')[0].onclick = function () { jw.preventSubmit2('btnSave'); }
			var gvBudget = new JustWinTable('gvBudget');
			setButton2(gvBudget, 'hdCheckedIds');
			jw.tooltip();
			document.getElementById('txtBName').setAttribute('readOnly', 'readOnly');
			if (getRequestParam('Action') == 'Query' || !getRequestParam('Action')) {
				setAllInputDisabled();
			}
			setTxtCorpDisabled();

			// 从采购计划中增加采购单
			addEvent(document.getElementById('btnSelectPurchaseplan'), 'click', selectPurchaseplanEvent);
			registerBtnCancelEvent();
			Val.validate('form1', 'btnSave');
			setAuditState();
			if ($('#hfldIsTarPur').val() == 'Purchase') {
				document.getElementById('divTarget').style.display = 'none';
				document.getElementById('divPurchase').style.display = '';
				document.getElementById('spTarget').className = 'xxk';
				document.getElementById('spPurchase').className = 'xxkd';
				//document.getElementById('hfldIsTarPur').value = '';
			}
			var jwTable = new JustWinTable('gvwPurchaseStock');
			jwTable.registClickTrListener(function () {
				$('#hfldPurchaseChecked').val('["' + this.id + '"]');
				$('#hfldPurchaseIds').val('["' + this.purchaseId + '"]');
			});

			// 当为补充协议时生成补充合同编码
			var action = getRequestParam('Action');
			if (action == 'AddProtocol') {
				$.ajax({
					type: 'GET',
					async: false,
					url: '/ContractManage/Handler/GetContractPayoutCodeAdd.ashx?contractID=' + getRequestParam('ContractId') + "&reftime=" + (new Date()).toString(),
					success: function (data) {
						$('#txtContractCode').val(data);
					}
				});
			}

			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				var purchaseIds = '';
				if (checkedChk.length == 0) {
					$('#btnDelete').attr('disabled', 'disabled');
				}
				else if (checkedChk.length == 1) {
					$('#hfldPurchaseChecked').val(jwTable.getCheckedChkIdJson(checkedChk));
					var tr = getFirstParentElement(checkedChk[0].parentNode, 'TR');
					purchaseIds = '"' + tr.purchaseId + '"';
					$('#btnDelete').removeAttr('disabled');
				}
				else {
					$('#hfldPurchaseChecked').val(jwTable.getCheckedChkIdJson(checkedChk));
					$('#btnDelete').removeAttr('disabled');
					for (var i = 0; i < checkedChk.length; i++) {
						var trs = getFirstParentElement(checkedChk[i].parentNode, 'TR');
						purchaseIds += '"' + trs.purchaseId + '",';
					}
					purchaseIds = purchaseIds.substring(0, purchaseIds.length - 1);
				}
				$('#hfldPurchaseIds').val('[' + purchaseIds + ']');
			});

			jwTable.registClickAllCHKLitener(function () {
				if (this.checked) {
					$('#hfldPurchaseChecked').val(jwTable.getCheckedChkIdJson());
					$('#btnDelete').removeAttr('disabled');
					var checkedChk = jwTable.getAllChk();
					var purchaseIds = '';
					if (checkedChk.length > 0) {
						if (checkedChk.length == 1) {
							var tr = getFirstParentElement(checkedChk[0].parentNode, 'TR');
							purchaseIds = '"' + tr.purchaseId + '"';
						} else {
							for (var i = 0; i < checkedChk.length; i++) {
								var trs = getFirstParentElement(checkedChk[i].parentNode, 'TR');
								purchaseIds += '"' + trs.purchaseId + '",';
							}
							purchaseIds = purchaseIds.substring(0, purchaseIds.length - 1);
						}
					} else {
						purchaseIds = '';
					}
					$('#hfldPurchaseIds').val('[' + purchaseIds + ']');
				}
				else {
					$('#hfldPurchaseChecked').val('');
					$('#hfldPurchaseIds').val('');
					$('#btnDelete').attr('disabled', 'disabled');
				}
			});
			replaceEmptyTable('emptyStock', 'gvwPurchaseStock');
			// 计算小计
			calcTotal();

			jw.formatTreegrid('gvBudget', 1);
		});

		function setTxtCorpDisabled() {
			var inputs = document.getElementsByTagName('INPUT');
			for (var i = 0; i < inputs.length; i++) {
				if (inputs[i].id.indexOf('txtCorp') > 0) {
					inputs[i].setAttribute('readonly', 'readonly');
				}
			}
		}

		// 计算小计
		function calcTotal() {
			$('#gvwPurchaseStock .num, #gvwPurchaseStock .price').blur(function () {
				var tr = getFirstParentElement(this, 'TR');
				var num = _strToDecimal($(tr).find('.num').val());
				var price = _strToDecimal($(tr).find('.price').val());
				var total = _formatDecimal(num * price);
				$(tr).find('.total').html(total);
				calcSumTotal();
			});
		}

		// 计算合计
		function calcSumTotal() {
			var sumTotal = 0.0;
			$('#gvwPurchaseStock .total').each(function () {
				sumTotal += _strToDecimal($(this).html());
			});
			$('#gvwPurchaseStock ._total_').html(_formatDecimal(sumTotal));
		}


		// 选择项目
		function openProjPicker() {
			jw.selectOneProject({
				func: function (p) {
					$('#hdnProjectCode').val(p.prjId);
					$('#txtProject').val(p.prjName);
					$('#BtnShowPrjTypeName').click();

					if ($('#txtContractCode').val() == "" || $('#txtContractCode').val() == $('#hfldPrjCode').val()) {
						$('#txtContractCode').val(p.prjCode);
					}
					$("#hfldPrjCode").val(p.prjCode);
					var action = getRequestParam('Action');
					if (action != 'AddProtocol') {
						$.ajax({
							type: 'GET',
							async: false,
							url: '/ContractManage/Handler/GetPayoutCountractCode.ashx?PrjCode=' + $('#hdnProjectCode').val() + '&ContractTypeID=' + $('#hfldTypeID').val() + "&reftime=" + (new Date()).toString(),
							success: function (data) {
								$('#txtContractCode').val(data);
							}
						});
					}
				}
			});
		}

		function registerBtnCancelEvent() {
			var btnCancel = document.getElementById('btnCancel');
			addEvent(btnCancel, 'click', function () {
				winclose('PayoutContractEdit', 'PayoutContract.aspx', false)
			});
		}

		// 往来单位
		function pickCorp(param) {
			if (param == undefined) {
				//选择乙方
				jw.selectOneCorp({ idinput: 'hfldBName', nameinput: 'txtBName', func: function (corp) {
					$('#txtBName').val(corp.name);
					$('#hfldBName').val(corp.id);
					$('#btnPartBSelect').click();
				}
				});
			} else {
				//选择采购单中的供应商
				var txtID = param.id.replace('txt', 'hfld');
				jw.selectOneCorp({ idinput: txtID, nameinput: param.id });
			}
		}

		// 选择收入合同
		function selectIncomeContract() {
			jw.selectInCon({
				func: function (c) {
					$('#hfldIncomeContractId').val(c.id);
					$('#txtIncomeContract').val(c.name);
				}
			});
		}

		// 选择合同类型
		function selectContractType() {
			jw.selectOneConType({
				func: function (ConType) {
					$('#hfldTypeID').val(ConType.id);
					$('#txtTypeName').val(ConType.name);
					var action = getRequestParam('Action');
					if (action != 'AddProtocol') {
						$.ajax({
							type: 'GET',
							async: false,
							url: '/ContractManage/Handler/GetPayoutCountractCode.ashx?PrjCode=' + $('#hdnProjectCode').val() + '&ContractTypeID=' + $('#hfldTypeID').val() + "&reftime=" + (new Date()).toString(),
							success: function (data) {
								$('#txtContractCode').val(data);
							}
						});
					}
				}
			});

		}

		// 选择签订人
		function selectSignPerson() {
			jw.selectOneUser({ codeinput: "hfldSignPerson", nameinput: "txtSignPerson" })
		}

		// 添加控制指标
		function addTarget() {
			var prjId = $('#hdnProjectCode').val();
			if (prjId == '') {
				alert("请选择项目!");
			}
			else {
				top.ui._payoutContract = window;
				top.ui.callback = function (t) {
					$('#hfldTaskIds').val(t.taskId);
					$('#btnBindTarget')[0].click();
				}
				var url = "/ContractManage/PayoutContract/PayoutTarget.aspx?prjId=" + prjId;
				toolbox_oncommand(url, "添加控制指标");
			}
		}

		//全选效果
		function setButton2(jwTable, hdChkId) {
			if (!jwTable.table) return;
			if ($(jwTable.table).find('TR').length == 1) return;

			jwTable.registClickTrListener(function () {
				if (hdChkId != '') {
					document.getElementById(hdChkId).value = this.id;
				}
			});
			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				if (checkedChk.length == 0) {
					if (hdChkId != '') {
						document.getElementById(hdChkId).value = "";
					}
				}
				else {
					var checkedChks = jwTable.getCheckedChk();
					if (hdChkId != '') {
						var taskId = jwTable.getCheckedChkIdJson(checkedChk);
						document.getElementById(hdChkId).value = taskId;
					}
				}
			});
			jwTable.registClickAllCHKLitener(function () {
				if (this.checked) {
					if (hdChkId != '')
						document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
					var checkedChks = jwTable.getAllChk();
				}
				else {
					if (hdChkId != '')
						document.getElementById(hdChkId).value = '';
				}
			});
			replaceEmptyTable('emptyStock', 'gvwPurchaseStock');
		}


		//项目改变
		function prjChange() {
			var altKey = document.getElementById('txtProject').accessKey;
			if (altKey == '1') {
				var tb = document.getElementById('gvBudget');
				if (tb != null) {
					var rowsCount = tb.rows;
					if (parseInt(rowsCount.length) > 0) {
						var prevId = document.getElementById('hdPrevPrjId').value;
						var current = document.getElementById('hdnProjectCode').value;
						if (prevId != current)
							document.getElementById('btnPrjChange').click();
					}
				}
			}
		}

		function ManageTargetPurchase(param) {
			if (param == 'Target') {
				document.getElementById('divPurchase').style.display = 'none';
				document.getElementById('divTarget').style.display = '';
				document.getElementById('spTarget').className = 'xxkd';
				document.getElementById('spPurchase').className = 'xxk';
				$('#hfldIsTarPur').val('Target');
			} else if (param == 'Purchase') {
				document.getElementById('divTarget').style.display = 'none';
				document.getElementById('divPurchase').style.display = '';
				document.getElementById('spTarget').className = 'xxk';
				document.getElementById('spPurchase').className = 'xxkd';
				$('#hfldIsTarPur').val('Purchase');
			}
		}


		// 从采购计划中选择
		function selectPurchaseplanEvent() {
			var url = '/StockManage/Purchase/PurchaseplanList.aspx';

			top.ui.callback = function (obj) {
				$('#hfldResourceId').val(obj.id);
				$('#hfldppcode').val(obj.number);
				$('#btnBindResource').click();
			}
			top.ui.openWin({ title: '从采购计划中选择', url: url, width: 800, height: 485 });
		}

		// 从材料库中选择资源
		function selectResource() {
			var src = '/StockManage/UserControl/SelectResource.aspx?type=2&TypeCode=0002,0003';
			top.ui.callback = function (obj) {
				alert('从材料库中选择资源');
				$('#hfldResourceId').val(obj.id);
				$('#btnBindResource').click();
			}
			top.ui.openWin({ title: '选择资源', url: src, width: 1010, height: 500 });
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader2">
		<table class="tableHeader2">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="支出合同" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div id="divContent2" class="divContent2">
		<table id="tableContent2" class="tableContent2" cellpadding="5px" cellspacing="0">
			<tr>
				<td class="word" style="white-space: nowrap;">
					附件
				</td>
				<td colspan="3" style="padding-right: 0px;">
					<MyUserControl:epc_usercontrol1_filelink2_ascx ID="flAnnx" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					合同编号
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtContractCode" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'合同编号必须输入'}}" runat="server"></asp:TextBox>
				</td>
				<td class="word" style="white-space: nowrap;">
					项目
				</td>
				<td class="txt mustInput" style="padding-right: 3px;">
					<input type="text" readonly="readonly" id="txtProject" class="select_input {required:true, messages:{required:'项目必须输入'}}" onfocus="prjChange()" imgclick="openProjPicker" style="width: 100%;" runat="server" />

					<input id="hdnProjectCode" type="hidden" name="hdnProjectCode" runat="server" />

				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					合同名称
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtContractName" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'合同名称必须输入'}}" runat="server"></asp:TextBox>
				</td>
				<td class="word" style="white-space: nowrap;">
					合同类型
				</td>
				<td class="txt mustInput" style="padding-right: 3px;">
					<input type="text" style="width: 100%;" class="select_input {required:true, messages:{required:'合同类型必须输入'}}" readonly="readonly" id="txtTypeName" imgclick="selectContractType" runat="server" />

				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					合同金额
				</td>
				<td class="mustInput">
					<asp:TextBox ID="txtContractMoney" CssClass="decimal_input  {required:true,number:true, messages:{required:'合同金额必须输入',number:'合同金额格式错误'}}" Height="15px" Width="100%" runat="server"></asp:TextBox>
				</td>
				<td class="word" style="white-space: nowrap;">
					收入合同
				</td>
				<td class="txt" style="padding-right: 3px;">
					<input type="text" style="width: 100%;" readonly="readonly" id="txtIncomeContract" imgclick="selectIncomeContract" class="select_input" runat="server" />

					<input id="hfldIncomeContractId" type="hidden" runat="server" />

				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					甲方
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtAName" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'甲方必须输入'}}" runat="server"></asp:TextBox>
				</td>
				<td class="word" style="white-space: nowrap;">
					乙方
				</td>
				<td class="txt mustInput" style="padding-right: 3px;">
					<input type="text" style="width: 100%;" class="select_input {required:true, messages:{required:'乙方必须输入'}}" readonly="readonly" id="txtBName" imgclick="pickCorp" runat="server" />

					<asp:HiddenField ID="hfldBName" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					预付金额
				</td>
				<td>
					<asp:TextBox ID="txtPrepayMoney" CssClass="decimal_input" Height="15px" Width="100%" Text="0.0" runat="server"></asp:TextBox>
				</td>
				<td class="word">
				</td>
				<td>
					<asp:CheckBox ID="chkContaint" Text="包含待审数据" AutoPostBack="true" OnCheckedChanged="chkContaint_CheckedChanged" runat="server" />
					&nbsp;&nbsp;&nbsp;
					<asp:CheckBox ID="chkFictitious" Text="虚拟合同" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					签约时间
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtSignDate" Width="100%" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'签约时间必须输入'}}" runat="server"></asp:TextBox>
				</td>
				<td class="word" style="white-space: nowrap;">
					签约地点
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtAddress" CssClass="{required:true, messages:{required:'签约地点必须输入'}}" Height="15px" Width="100%" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					结算方式
				</td>
				<td class="txt mustInput" style="padding-right: 1px;">
					<asp:DropDownList ID="dropBalanceMode" Width="100%" Height="19px" CssClass="{required:true, messages:{required:'结算方式必须输入'}}" DataValueField="NoteID" DataTextField="CodeName" runat="server"></asp:DropDownList>
				</td>
				<td class="word" style="white-space: nowrap;">
					付款方式
				</td>
				<td class="txt mustInput" style="padding-right: 1px;">
					<asp:DropDownList ID="dropPayMode" Width="100%" Height="19px" CssClass="{required:true, messages:{required:'付款方式必须输入'}}" DataValueField="NoteID" DataTextField="CodeName" runat="server"></asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					开始时间
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtStartDate" Width="100%" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'开始时间必须输入'}}" runat="server"></asp:TextBox>
				</td>
				<td class="word" style="white-space: nowrap;">
					结束时间
				</td>
				<td class="txt">
					<asp:TextBox ID="txtEndDate" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					签订人
				</td>
				<td class="txt" style="padding-right: 3px;">
					<input type="text" style="width: 100%;" readonly="readonly" id="txtSignPerson" imgclick="selectSignPerson" class="select_input" runat="server" />

					<asp:HiddenField ID="hfldSignPerson" runat="server" />
				</td>
				<td class="word" style="white-space: nowrap;">
					项目类型
				</td>
				<td class="txt readonly">
					<input id="txtPrjType" type="text" style="height: 15px; width: 100%;" readonly="readOnly" runat="server" />

				</td>
			</tr>
			<tr style="display: none">
				<td class="word">
					财务项目编号
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtfinanceNumber" Height="15px" Width="100%" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					财务合同编号
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtfinanceProject" Height="15px" Width="100%" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr style="display: none;">
				<td class="word">
					大写金额
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtCapitalNumber" Height="15px" Width="100%" runat="server"></asp:TextBox>
				</td>
				<td class="word">
				</td>
				<td class="txt mustInput">
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					支付条件
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtPaymentCondition" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					主要条款
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtMainItem" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					备注
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtNotes" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					录入人
				</td>
				<td class="txt readonly">
					<asp:TextBox ID="txtInputPerson" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
				<td class="word" style="white-space: nowrap;">
					录入时间
				</td>
				<td class="txt readonly">
					<asp:TextBox Width="100%" ReadOnly="true" ID="txtInputDate" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td colspan="4">
					<div id="div">
						<span id="spTarget" style="margin-left: 0px;" class="xxkd" onclick="ManageTargetPurchase('Target')" runat="server">
							控制指标</span> <span id="spPurchase" class="xxk" onclick="ManageTargetPurchase('Purchase')" runat="server">
								采购单</span>
					</div>
				</td>
			</tr>
		</table>
		<div id='divTarget'>
			<table class="tableContent2" cellpadding="5px" cellspacing="0">
				<tr>
					<td>
						<input type="button" id="btn_addTarget" value="添加控制指标" style="width: 90px;" onclick="addTarget()" />
						<asp:Button ID="btnDelTarget" Text="删除控制指标" Width="90px" OnClick="btnDelTarget_Click1" runat="server" />
					</td>
				</tr>
				<tr>
					<td>
						<asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="TaskId,OrderNumber" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
										<%# Eval("No") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="名称"><ItemTemplate>
										<span style="vertical-align: middle;">
											<%# Eval("TaskName") %>
										</span>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="编码"><ItemTemplate>
										<%# Eval("TaskCode") %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型" HeaderStyle-Width="50px"><ItemTemplate>
										<%# Common2.GetTaskTypeName(Eval("OrderNumber").ToString()) %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
										<%# Eval("Unit") %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程量" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
										<%# Eval("Quantity") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="50px"><ItemTemplate>
										<%# Common2.GetTime(Eval("StartDate")) %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="50px"><ItemTemplate>
										<%# Common2.GetTime(Eval("EndDate")) %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="综合单价" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
										<%# Common2.FormatDecimal(Eval("UnitPrice")) %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="小计" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
										<%# Common2.FormatDecimal(Eval("Total2")) %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
										<asp:HyperLink ID="hlinkShowNote" Style="text-decoration: none; color: Black;" ToolTip='<%# System.Convert.ToString(Eval("Note").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(Eval("Note").ToString()) %></asp:HyperLink>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="排序" Visible="false"><ItemTemplate>
										<%# StringUtility.GetStr(System.Convert.ToString(Eval("OrderNumber"))) %>
									</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					</td>
				</tr>
			</table>
		</div>
		<div id='divPurchase' style="display: none;">
			<table class="tableContent2" cellpadding="5px" cellspacing="0">
				<tr>
					<td>
						<input type="button" id="btnSelectPurchaseplan" style="width: 110px" value="从采购计划中选择" />
						<input type="button" id="btnPickResource" style="width: 150px" value="从材料库中选择" onclick="selectResource();" />
						<asp:Button ID="btnDelete" Text="删除" OnClick="btnDelete_Click" runat="server" />
					</td>
				</tr>
				<tr style="vertical-align: top">
					<td style="width: 100%;">
						<asp:GridView ID="gvwPurchaseStock" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwPurchaseStock_RowDataBound" DataKeyNames="ResourceCode,PsId" runat="server">
<EmptyDataTemplate>
								<table id="emptyStock" class="tab">
									<tr class="header">
										<th scope="col" style="width: 20px;">
											<asp:CheckBox ID="chkSelect" runat="server" />
										</th>
										<th scope="col" style="width: 25px;">
											序号
										</th>
										<th scope="col">
											物资编号
										</th>
										<th scope="col">
											物资名称
										</th>
										<th scope="col">
											规格
										</th>
										<th scope="col">
											单位
										</th>
										<th scope="col">
											数量
										</th>
										<th scope="col">
											采购价格
										</th>
										<th scope="col">
											小计
										</th>
										<th scope="col">
											供应商
										</th>
										<th scope="col">
											型号
										</th>
										<th scope="col">
											品牌
										</th>
										<th scope="col">
											技术参数
										</th>
									</tr>
								</table>
							</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
										<asp:CheckBox ID="chkSelectAll" runat="server" />
									</HeaderTemplate><ItemTemplate>
										<asp:CheckBox ID="chkSelectSingle" runat="server" />
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号" HeaderStyle-Width="100px">
<ItemTemplate>
										<%# Eval("ResourceCode") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="物资名称">
<ItemTemplate>
										<span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
											<%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
										</span>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
										<span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
											<%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
										</span>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px">
<ItemTemplate>
										<%# Eval("Name") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="库存量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
										<%# WebUtil.GetStockNumberByCode(Eval("ResourceCode").ToString()).ToString() %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量" HeaderStyle-Width="70px"><ItemTemplate>
										<asp:TextBox ID="txtNumber" Style="text-align: right;" Width="60px" CssClass="decimal_input num" Text='<%# System.Convert.ToString((Eval("number").ToString() == "") ? "0.000" : System.Convert.ToDecimal(Eval("number")).ToString("0.000"), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购价格" HeaderStyle-Width="70px"><ItemTemplate>
									<asp:TextBox ID="txtSprice" Style="text-align: right;" Width="60px" CssClass="decimal_input price" Text='<%# System.Convert.ToString((Eval("sprice").ToString() == "") ? "0.000" : System.Convert.ToDecimal(Eval("sprice")).ToString("0.000"), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="小计" HeaderStyle-Width="70px" ItemStyle-CssClass="total">
<ItemTemplate>
										<%# System.Convert.ToDecimal(Eval("Total")).ToString("0.000") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="供应商" HeaderStyle-Width="100px">
<ItemTemplate>
										<asp:TextBox ID="txtCorp" Width="90px" CssClass="{required:true, messages:{required:'供应商必须输入'}}" ondblclick="pickCorp(this)" Style="background-color: #ffffc0;" Text='<%# System.Convert.ToString(Eval("CorpName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
										<asp:HiddenField ID="hfldCorp" Value='<%# System.Convert.ToString(Eval("corp"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
										<span class="tooltip" title='<%# Eval("ModelNumber").ToString() %>'>
											<%# StringUtility.GetStr(Eval("ModelNumber").ToString(), 10) %>
										</span>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
										<span class="tooltip" title='<%# Eval("brand").ToString() %>'>
											<%# StringUtility.GetStr(Eval("brand").ToString(), 10) %>
										</span>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
										<span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
											<%# StringUtility.GetStr(Eval("TechnicalParameter").ToString(), 10) %>
										</span>
									</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					</td>
				</tr>
			</table>
		</div>
	</div>
	<div id="divFooter" class="divFooter2">
		<table class="tableFooter2">
			<tr>
				<td>
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
					<input type="button" id="btnCancel" value="取消" />
				</td>
			</tr>
		</table>
	</div>
	<div id="divSelectFromPurchase" title="选择采购计划" style="display: none">
		<iframe id="ifResoueceFromPurchase" frameborder="0" width="100%" height="100%" src="../../StockManage/Purchase/PurchaseplanList.aspx">
		</iframe>
	</div>
	<asp:HiddenField ID="hfldContractId" runat="server" />
	<asp:HiddenField ID="hfldTypeID" runat="server" />
	
	<asp:HiddenField ID="hfldTaskIds" runat="server" />
	<asp:Button ID="btnBindTarget" Style="display: none;" OnClick="btnBindTarget_Click" runat="server" />
	<asp:Button ID="btnPrjChange" Style="display: none;" OnClick="btnPrjChange_Click" runat="server" />
	
	<asp:Button ID="btnPartBSelect" Style="display: none;" OnClick="btnPartBSelect_click" runat="server" />
	
	<asp:HiddenField ID="hdCheckedIds" runat="server" />
	<asp:HiddenField ID="hdPrevPrjId" runat="server" />
	
	<asp:HiddenField ID="hdTheMoneys" runat="server" />
	<!--从采购计划中选择物资的物资ID-->
	<asp:HiddenField ID="hfldResourceId" runat="server" />
	<!--存放所有被选中物质采购物资编号-->
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	
	<asp:HiddenField ID="hfldppcode" runat="server" />
	
	<asp:HiddenField ID="hfldIsTarPur" runat="server" />
	<!--从采购计划中选择物资后执行-->
	<asp:Button ID="btnBindResource" Text="Button" OnClick="btnBindResource_Click" runat="server" />
	
	<asp:HiddenField ID="hfldPurchaseIds" runat="server" />
	
	<asp:HiddenField ID="hfldPrjCode" runat="server" />
	
	<asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    <asp:Button ID="BtnShowPrjTypeName" OnClick="BtnShowPrjTypeName_Click" runat="server" />
	</form>
</body>
</html>
