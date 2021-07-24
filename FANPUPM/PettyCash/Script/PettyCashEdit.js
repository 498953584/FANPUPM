// PettyCashEdit.aspx
$(document).ready(function () {
	// 添加验证
	$('#btnSave')[0].onclick = function () {
		if (!$('#form1').form('validate')) {
			return false;
		}
		else {
			jw.preventSubmit2('btnSave');
		}
	}

	// 显示大写金额
	$('#spanCashWords').html(jw.amountInWords($('#txtCash').val()));

	// 自动计算大写金额
	$('#txtCash').bind('keyup blur', function () {
		$('#spanCashWords').html(jw.amountInWords(this.value));
	});

	// 设部分控件为只读
	$('#txtApplicationDate').attr('readOnly', true);
	$('#txtCashDate').attr('readOnly', true);

	// 取消按钮
	$('#btnCancel').click(function () {
		winclose('PettyCashList', 'PettyCashList.aspx', false);
	});

	// 添加 AutoComplete 方法
	addAutoComplete();

	// 动态调整申请理由的高度
	$('#txtApplicationReason').bind('keyup', function (e) {
		var reasonHeight = $('#txtApplicationReason')[0].scrollHeight;
		if (reasonHeight > 100) {
			$('#txtApplicationReason').css('height', reasonHeight + 10);
		}
	});
});

// 选择项目
function selectProject() {
	$("#imgName").src = "../../images/selectopen.gif";
	$('#win').css('display', 'block').dialog({ modal: true });
}

// 选择项目后的回调方法
function retProject(prjJson) {
	try {
		$('#win').dialog('close');
		if (!prjJson) return;
		var prjObj = JSON.parse(prjJson); ;
		$('#txtProject').val(prjObj.prjName);
		$('#hfldPrjTypeCode').val(prjObj.typeCode);
		$("#imgName").src = "../../images/select.gif";
	} catch (ex) { }
}

// 添加 AutoComplete 方法
function addAutoComplete() {
	// 付款单位
	$.ajax({
		type: 'GET',
		url: 'Handler/AutoComplete.ashx?type=payer&date=' + new Date().getTime(),
		dataType: 'json',
		async: true,
		success: function (data) {
			$('#txtPayer').autocomplete(data, { minChars: 0, max: 5 });
		}
	});

	// 申请人账号
	$.ajax({
		type: 'GET',
		url: 'Handler/AutoComplete.ashx?type=account&date=' + new Date().getTime(),
		dataType: 'json',
		async: true,
		success: function (data) {
			$('#txtAccount').autocomplete(data, { minChars: 0, max: 5 });
			$('#txtAccount').result(function () {
				getBankByAccount(this.value)    // 根据帐号查找银行
			});

		}
	});

	// 开户银行
	$.ajax({
		type: 'GET',
		url: 'Handler/AutoComplete.ashx?type=bank&date=' + new Date().getTime(),
		dataType: 'json',
		async: true,
		success: function (data) {
			$('#txtBank').autocomplete(data, { minChars: 0, max: 5 });
		}
	});
}

// 根据帐号查找银行
function getBankByAccount(account) {
	$.ajax({
		type: 'GET',
		url: 'Handler/GetBankByAccount.ashx?account=' + account + '&date=' + new Date().getTime(),
		async: true,
		success: function (data) {
			$('#txtBank').val(data);
		}
	});
}
