$(document).ready(function () {
	$('#btnDelete')[0].onclick = deleteSalaryItem; 	// 删除
	var jwTable = new JustWinTable('gvSalaryItem');
	setButton2(jwTable, 'btnDelete', 'btnEdit', 'hfldCheckedId');
	replaceEmptyTable('emptySalaryItem', 'gvSalaryItem'); 	// 数据为空时替换表格
	showTooltip('tooltip', 25);                         // 提示
});

//新增编辑
function edit(type) {
	$('#hfldType').val(type);
	var id = $('#hfldCheckedId').val();
	var titleName = '新增工资项';
	$('#txtSalaryName').val('');
	$('#txtNote').val('');
	if (type == 'edit') {
		titleName = '编辑工资项';
		if (id != '') {
			$.ajax({
				type: "GET",
				async: false,
				url: '/Salary2/Handler/GetSalaryInfo.ashx?' + new Date().getTime() + '&type=edit&page=salaryItem&id=' + id,
				success: function (datas) {
					if (datas != '') {
						var data = datas;
						$('#txtSalaryName').val(data.Name);
						$('#txtNote').val(data.Note);
					}
				}
			});
		}
	}
	$('#divSalaryItemEdit').css('display', 'block').dialog({
		title: titleName,
		width: 500,
		height: 210,
		modal: true,
		buttons: [{
			text: '保存',
			handler: function () {
				saveData();
			}
		}, {
			text: '取消',
			handler: function () {
				$('#divSalaryItemEdit').dialog('close');
			}
		}]
	});
}
//保存数据
function saveData() {
	if ($('#txtSalaryName').validatebox('isValid') == true && $('#txtNote').validatebox('isValid') == true) {
		$('#divSalaryItemEdit').parent().appendTo($('form:first'));
		$('#btnSaveData').click();
	}
}
//删除工资项
function deleteSalaryItem() {
	return jw.confirm('系统提示', '您确定要删除吗?', 'btnDelete');
}

function setButton2(jwTable, btnDel, btnUpdate, hdChkId) {
	if (!jwTable.table) return;
	if (jwTable.table.getElementsByTagName('TR').length == 1) {
		//table中没有数据
		return;
	}
	jwTable.registClickTrListener(function () {
		if (hdChkId != '')
			$('#' + hdChkId).val(this.id);
		if (this.getAttribute('IsAllowDel') == 'True') {
			$('#' + btnDel).removeAttr('disabled');
			$('#' + btnUpdate).removeAttr('disabled');
		}
		else {
			$('#' + btnDel).attr('disabled', 'disabled');
			$('#' + btnUpdate).attr('disabled', 'disabled');
		}
		controlUpAndDown(this.getAttribute('No'));
	});
	jwTable.registClickSingleCHKListener(function () {
		var checkedChk = jwTable.getCheckedChk();
		if (checkedChk.length == 0) {
			$('#' + btnDel).attr('disabled', 'disabled');
			$('#' + btnUpdate).attr('disabled', 'disabled');
			$('#btnUp').attr('disabled', 'disabled');
			$('#btnDown').attr('disabled', 'disabled');
		}
		else if (checkedChk.length == 1) {
			var trChecked = getFirstParentElement(checkedChk[0].parentNode, 'TR');
			if (hdChkId != '')
				$('#' + hdChkId).val(trChecked.id);
			if (trChecked.getAttribute('IsAllowDel') == 'True') {
				$('#' + btnDel).removeAttr('disabled');
				$('#' + btnUpdate).removeAttr('disabled');
			}
			else {
				$('#' + btnDel).attr('disabled', 'disabled');
				$('#' + btnUpdate).attr('disabled', 'disabled');
			}
			controlUpAndDown(trChecked.getAttribute('No'));
		}
		else {
			$('#btnUp').attr('disabled', 'disabled');
			$('#btnDown').attr('disabled', 'disabled');
			$('#' + btnUpdate).attr('disabled', 'disabled');
			var isAllowDelArray = new Array();
			if (hdChkId != '')
				$('#' + hdChkId).val(jwTable.getCheckedChkIdJson(checkedChk));
			for (var i = 0; i < checkedChk.length; i++) {
				var trChecked = getFirstParentElement(checkedChk[i].parentNode, 'TR');
				isAllowDelArray.push(trChecked.getAttribute('IsAllowDel'));
			}
			if (checkIsAllowDel(isAllowDelArray) == '1') {
				$('#' + btnDel).removeAttr('disabled');
			} else {
				$('#' + btnDel).attr('disabled', 'disabled');
			}
		}
	});
	jwTable.registClickAllCHKLitener(function () {
		$('#btnUp').attr('disabled', 'disabled');
		$('#btnDown').attr('disabled', 'disabled');
		$('#' + btnUpdate).attr('disabled', 'disabled');
		if (this.checked) {
			var isAllowDelArray = new Array();
			var checkedIdArray = new Array();
			$('#gvSalaryItem [type=checkbox]').each(function () {
				if (this.id != 'chkAll') {
					var trSelected = getFirstParentElement(this.parentNode, 'TR');
					isAllowDelArray.push(trSelected.checkedIdArray);
					checkedIdArray.push(trSelected.id);
				}
			});
			if (hdChkId != '')
				$('#' + hdChkId).val(JSON.stringify(checkedIdArray));
			//判断选中的是否允许删除
			if (checkIsAllowDel(isAllowDelArray) == '1') {
				$('#' + btnDel).removeAttr('disabled');
			} else {
				$('#' + btnDel).attr('disabled', 'disabled');
			}
		}
		else {
			if (hdChkId != '')
				$('#' + hdChkId).val('');
			$('#' + btnDel).attr('disabled', 'disabled');
		}
	});
}
//遍历选中的是否允许删除
function checkIsAllowDel(isAllowDelArray) {
	var isAllowDel = '1';
	for (var i = 0; i < isAllowDelArray.length; i++) {
		if (isAllowDelArray[i] != 'True')
			isAllowDel = '0';
		break;
	}
	return isAllowDel;
}
//获取最大的No
function GetMaxNo() {
	var data = '';
	$.ajax({
		type: "GET",
		async: false,
		url: '/Salary2/Handler/GetSalaryInfo.ashx?' + new Date().getTime() + '&type=GetMaxNo&page=salaryItem',
		success: function (datas) {
			data = datas;
		}
	});
	return data;
}

//控制上移下移按钮
function controlUpAndDown(currentNo) {
	var maxNo = parseInt(GetMaxNo());
	var No = parseInt(currentNo);
	if (No == 1) {
		$('#btnUp').attr('disabled', 'disabled');
		$('#btnDown').removeAttr('disabled');
	} else if (No == maxNo) {
		$('#btnUp').removeAttr('disabled');
		$('#btnDown').attr('disabled', 'disabled');
	} else if (No > 1 && No < maxNo) {
		$('#btnUp').removeAttr('disabled');
		$('#btnDown').removeAttr('disabled');
	} else {
		$('#btnUp').attr('disabled', 'disabled');
		$('#btnDown').attr('disabled', 'disabled');
	}
}


function MoveTREvent(type) {
	var id = $('#hfldCheckedId').val();
	var currentTr = $('#' + id).get(0);
	var targetTr = GetTargetTr(id, type);
	MoveTR(targetTr, currentTr);
}

//移动TR
function MoveTR(targetTr, currentTr) {
	//获取移动之前的TR样式
	var oldCurrentTrCss = currentTr.className;
	var oldTargetTrCss = targetTr.className;


	//获取移动之前TR的No
	var oldCurrentNo = currentTr.getAttribute('No');
	var oldTargetNo = targetTr.getAttribute('No');
	//获取移动之前TR的Num
	var oldCurrentNum = currentTr.getAttribute('Num');
	var oldTargetNum = targetTr.getAttribute('Num');
	//更改table序号
	//	currentTr.cells[1].innerText = oldTargetNum;
	currentTr.getElementsByTagName('TD')[1].innerText = oldTargetNum;
	//	targetTr.cells[1].innerText = oldCurrentNum;
	targetTr.getElementsByTagName('TD')[1].innerText = oldCurrentNum;
	//更改TR中的No值
	currentTr.setAttribute('No', oldTargetNo);
	targetTr.setAttribute('No', oldCurrentNo);
	//更改TR中的Num值
	currentTr.setAttribute('Num', oldTargetNum);
	targetTr.setAttribute('Num', oldCurrentNum);
	//去除选中TR的 style 样式
	$(currentTr).removeAttr('style');
	//更改样式
	currentTr.className = oldTargetTrCss;
	targetTr.className = oldCurrentTrCss;
	//替换TR
	$(currentTr).replaceWith($(targetTr).clone());
	$(targetTr).replaceWith($(currentTr).clone());
	//修改序号
	UpdateNo(targetTr, currentTr);
	var jwTable = new JustWinTable('gvSalaryItem');
	setButton2(jwTable, 'btnDelete', 'btnEdit', 'hfldCheckedId');
	//重新选中TR
	var id = $('#hfldCheckedId').val();
	$('#' + id).click();
}
//获取目标TR
function GetTargetTr(id, type) {
	var currentTr;
	var targetTr;
	var trs = $('#gvSalaryItem tr');
	if (type == 'Up') {             //上移
		trs.each(function () {
			targetTr = currentTr;
			currentTr = this;
			if (this.id == id) {
				return false;
			}
		});
	} else if (type == 'Down') {    //下移
		//倒序遍历TR
		for (var i = trs.length - 1; i > 0; i--) {
			targetTr = currentTr;
			currentTr = trs[i];
			if (trs[i].id == id) {
				break;
			}
		}
	}
	return targetTr;
}

//修改数据库No
function UpdateNo(targetTr, currentTr) {
	$.ajax({
		type: "GET",
		async: false,
		url: '/Salary2/Handler/SalaryInfoNoMove.ashx?' + new Date().getTime() + '&page=salaryItem&targetTr=' + targetTr.id + '&currentTr=' + currentTr.id,
		success: function (datas) {
			if (datas != '') {
			}
		}
	});
}