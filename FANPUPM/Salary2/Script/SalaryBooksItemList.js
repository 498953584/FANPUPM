$(document).ready(function () {
	$('#btnDelete')[0].onclick = deleteSalaryItem; 	// 删除
	var jwTable = new JustWinTable('gvSalaryBooksItem');
	setButton2(jwTable, 'btnDelete', 'btnEdit', 'hfldCheckedId');
	replaceEmptyTable('emptySalaryBooksItem', 'gvSalaryBooksItem'); 	// 数据为空时替换表格
	showTooltip('tooltip', 25);                         // 提示
	//启用公式选择事件
	$("#chkIsFormula").change(function () {
		var res;
		if (!$("#chkIsFormula").attr("checked")) {
			//取消启用时，提示
			$.messager.confirm('系统提示', '您确定要删除计算公式吗?', function (data) {
				if (data) {             //选择“确定”
					$('#btnFormula').attr('disabled', 'disabled');
					$('#txtDefaultValue').removeAttr('disabled');
					$('#hfldFormula').val('');
					$('#txtFormula').val('');
				}
				else {                  //选择“取消”
					$('#chkIsFormula').attr('checked', 'true');
					$('#btnFormula').removeAttr('disabled');
					$('#txtDefaultValue').val('0.000');
					$('#txtDefaultValue').attr('disabled', 'disabled');
				}
			});
		} else if ($("#chkIsFormula").attr("checked")) {
			var itemId = $('select#ddlSalaryItem  option:selected').val();
			var isAllowEdit = GetIsAllowEdit(itemId);       //判断是否是税率和扣除数
			if (isAllowEdit == "1") {                       //不是税率和扣除数时，执行
				$('#chkIsFormula').attr('checked', 'true');
				$('#btnFormula').removeAttr('disabled');
			} else {                            //是税率和扣除数时，执行
				jw.alert('系统提示', '此工资项不允许编辑公式和默认值!');
				$('#chkIsFormula').removeAttr('checked');
				$('#btnFormula').attr('disabled', 'disabled')
				$('#hfldFormula').val('');
				$('#txtFormula').val('');
			}
			$('#txtDefaultValue').val('0.000');
			$('#txtDefaultValue').attr('disabled', 'disabled');
		}
	});
	$('#btnFormula').click(formula);

	//按钮事件
	$('#tabOperator').find('input').each(function () {
		$(this).click(function () {
			if ($(this).attr('id') == 'btnClear') {
				$('#txtFormula').val('');
			} else {
				var operate = $(this).val();
				var txtFormula = $('#txtFormula').val();
				txtFormula += operate;
				$('#txtFormula').val(txtFormula);
			}
		});
	})
	//新增明细时，修改工资项
	$('#ddlSalaryItem').change(function () {
		var selectedSaItem = $(this).val();
		setIsAllowEdit(selectedSaItem);
	});
});

function setButton2(jwTable, btnDel, btnUpdate, hdChkId) {
	if (!jwTable.table) return;
	if (jwTable.table.getElementsByTagName('TR').length == 1) {
		//table中没有数据
		return;
	}
	jwTable.registClickTrListener(function () {
		if (hdChkId != '')
			$('#' + hdChkId).val(this.id);
		$('#' + btnUpdate).removeAttr('disabled');
		$('#' + btnDel).removeAttr('disabled');
		controlUpAndDown(this.getAttribute('No'));
	});
	jwTable.registClickSingleCHKListener(function () {
		var checkedChk = jwTable.getCheckedChk();
		if (checkedChk.length == 0) {
			$('#' + btnDel).attr('disabled', 'disabled');
			$('#' + btnUpdate).attr('disabled', 'disabled');
			$('#btnUp').attr('disabled', 'disabled');
			$('#btnDown').attr('disabled', 'disabled');
			$('#' + hdChkId).val('');
		}
		else if (checkedChk.length == 1) {
			$('#' + btnUpdate).removeAttr('disabled');
			var trChecked = getFirstParentElement(checkedChk[0].parentNode, 'TR');
			if (hdChkId != '')
				$('#' + hdChkId).val(trChecked.id);
			$('#' + btnDel).removeAttr('disabled');
			controlUpAndDown(trChecked.getAttribute('No'));
		}
		else {
			if (hdChkId != '')
				$('#' + hdChkId).val(jwTable.getCheckedChkIdJson(checkedChk));
			$('#btnUp').attr('disabled', 'disabled');
			$('#btnDown').attr('disabled', 'disabled');
			$('#' + btnUpdate).attr('disabled', 'disabled');
			$('#' + btnDel).removeAttr('disabled');
		}
	});
	jwTable.registClickAllCHKLitener(function () {
		$('#btnUp').attr('disabled', 'disabled');
		$('#btnDown').attr('disabled', 'disabled');
		$('#' + btnUpdate).attr('disabled', 'disabled');
		var checkedIdArray = new Array();
		if (this.checked) {
			$('#' + btnDel).removeAttr('disabled');
			var checkedIdArray = new Array();
			$('#gvSalaryBooksItem [type=checkbox]').each(function () {
				if (this.id != 'chkAll') {
					var trSelected = getFirstParentElement(this.parentNode, 'TR');
					checkedIdArray.push(trSelected.id);
				}
			});
			if (hdChkId != '')
				$('#' + hdChkId).val(JSON.stringify(checkedIdArray));
		}
		else {
			if (hdChkId != '')
				$('#' + hdChkId).val('');
			$('#' + btnDel).attr('disabled', 'disabled');
		}

	});
}

//计算公式窗口
function formula() {
	var booksId = $('#hfldSaBooks').val();
	var currentItem = $('select#ddlSalaryItem  option:selected').val();
	if ($('#hfldType').val() == 'add') {
		$('#txtFormula').val('');
	} else {
		var data = GetById($('#hfldCheckedId').val());
		$('#txtFormula').val(data.Formula);
		if (data.Formula != null) {
			var formula = data.Formula;
			if (formula != '') {
				var formulaURl = formula.replace(/\+/g, "%2C"); //替换全部'+'
				var formulaIds = GetReplaceFormula(encodeURI(formulaURl), booksId);  //将工资项名称替换成明细Id
				$('#hfldFormula').val(formulaIds);
			}
		}
	}
	//绑定已经添加到帐套明细中的工资项名称，除去选择的工资项
	var data = GetItemsByBooksId(booksId, currentItem);
	bindSaItems(data);
	$('#divFormula').css('display', 'block').dialog({
		title: '编制公式',
		width: 550,
		height: 390,
		modal: true,
		buttons: [{
			text: '校验',
			handler: function () {
				var isValid = verifyFormula();
				if (isValid == '1') {
					jw.alert('系统提示', '通过校验,表达式正确!')
				} else {
					jw.alert('系统提示', '未通过校验,表达式错误!')
				}
			}
		}, {
			text: '保存',
			handler: function () {
				var isValid = verifyFormula();
				if (isValid == '1') {
					if ($('#txtFormula').val().length > 150) {
						jw.alert('系统提示', '输入的字数不得超过150个');
					} else {
						var formula = $('#txtFormula').val();
						var formulaURl = formula.replace(/\+/g, "%2C"); //替换全部'+'
						var formulaIds = GetReplaceFormula(encodeURI(formulaURl), booksId);  //将工资项名称替换成明细Id
						$('#hfldFormula').val(formulaIds);
						$('#divFormula').dialog('close');
						$('#divFormula').parent().appendTo($('form:first'));
					}
				} else {
					jw.alert('系统提示', '未通过校验,表达式错误!')
				}
			}
		}, {
			text: '取消',
			handler: function () {
				$('#divFormula').dialog('close');
			}
		}]
	});
}



//绑定已经添加到明细中的工资项
function bindSaItems(saItems) {
	var mess = "";
	for (var i = 0; i < saItems.length; i++) {
		if (i == saItems.length - 1) {
			mess += '<li id="' + saItems[i].Id + '" style="border-top: solid 1px #CADEED; border-bottom: solid 1px #CADEED; border-left :solid 1px #CADEED; border-right:solid 1px #CADEED;';
		}
		else {
			mess += '<li id="' + saItems[i].Id + '" style="border-top: solid 1px #CADEED; border-bottom: 0px; border-left :solid 1px #CADEED; border-right:solid 1px #CADEED;';
		}
		if (i % 2 == 0) {
			mess += ' background-color: #fbfbfb;" oldcolor="#fbfbfb">';
		}
		else {
			mess += ' background-color: #ffffff;" oldcolor="#ffffff">';
		}
		mess += saItems[i].ItemName;
		mess += "</li>";
	}
	var $li = $(mess);
	$li.css('fontSize', '13px');
	$li.css('list-style-type', 'none');
	$li.css('padding', '4px 10px 4px 10px');
	$li.css('cursor', 'pointer');
	$('#divSalaryItem').empty();
	$('#divSalaryItem').append($li);
	var liCount = 0;
	//计算隐藏信息的高度
	$('#divSalaryItem').find('li').each(function () {
		liCount += 15;
	});
	$('#divSalaryItem').attr('style',
                        'background-color:white; width:240px; height:130px;overflow-y:auto;');
	//添加 <li> 的划动鼠标样式
	$('#divSalaryItem').find('li').each(function () {
		var oldColor = '#ffffff';
		//改变滑动鼠标样式
		$(this).mouseover(function () {
			oldColor = $(this).attr('oldcolor');
			$(this).css("background", "#EAF4FD");
		});
		$(this).mouseout(function () {
			$(this).css("background", oldColor);
		});
		//单击li事件
		$(this).click(function () {
			var name = '[' + $(this).text() + ']';
			var txtFormula = $('#txtFormula').val();
			txtFormula += name;
			$('#txtFormula').val(txtFormula);
		});
	});

}

//校验
function verifyFormula() {
	var formula = $('#txtFormula').val();
	var currentBookItemId = '';
	var saBooksId = $('#hfldSaBooks').val();
	if ($('#hfldType').val() == 'edit') {
		currentBookItemId = $('#hfldCheckedId').val();
	}
	formula = formula.replace(/\+/g, "%2C"); //替换全部'+'
	return ajaxExecute('&type=verify&formula=' + encodeURI(formula) + '&saBooksId=' + saBooksId + '&saBooksItemId=' + currentBookItemId);
}



//删除工资项
function deleteSalaryItem() {
	return jw.confirm('系统提示', '您确定要删除吗?', 'btnDelete');
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
	//	    currentTr.cells[1].innerText = oldTargetNum;
	$(currentTr).find('TD').eq(1).text(oldTargetNum);
	//	    targetTr.cells[1].innerText = oldCurrentNum;
	$(targetTr).find('TD').eq(1).text(oldCurrentNum);
	//更改TR中的No值
	currentTr.setAttribute('No', oldTargetNo);
	targetTr.setAttribute('No', oldCurrentNo);
	//更改TR中的Num值
	currentTr.Num = oldTargetNum;
	targetTr.Num = oldCurrentNum;
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
	var jwTable = new JustWinTable('gvSalaryBooksItem');
	setButton2(jwTable, 'btnDelete', 'btnEdit', 'hfldCheckedId');
	//重新选中TR
	var id = $('#hfldCheckedId').val();
	$('#' + id).click();
}
//获取目标TR
function GetTargetTr(id, type) {
	var currentTr;
	var targetTr;
	var trs = $('#gvSalaryBooksItem tr');
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
		url: '/Salary2/Handler/SalaryInfoNoMove.ashx?' + new Date().getTime() + '&page=salaryBooksItem&targetTr=' + targetTr.id + '&currentTr=' + currentTr.id,
		success: function (datas) {
			if (datas != '') {
			}
		}
	});
}

//获取最大的No
function GetMaxNo() {
	return ajaxExecute('&type=GetMaxNo');
}

//根据帐套Id和工资项Id查询帐套明细的个数
function GetCountByItemId(saBooksItem, itemId) {
	return ajaxExecute('&type=GetCountByItemId&saBooksId=' + saBooksItem + '&saItemId=' + itemId);
}
//根据id获得帐套明细信息
function GetById(id) {
	return ajaxExecute('&type=GetById&id=' + id);
}
//根据帐套获取帐套明细中的工资项(去除currentItem)
function GetItemsByBooksId(booksId, currentItem) {
	return ajaxExecute('&type=GetItemIdsByBooksId&saBooksId=' + booksId + '&saItemId=' + currentItem);
}
//替换公式(从名称替换成Id)
function GetReplaceFormula(formula, saBooksId) {
	var data = '';
	$.ajax({
		type: "GET",
		async: false,
		url: '/Salary2/Handler/ConvertFormula.ashx?' + new Date().getTime() + '&type=ReplaceFormula&formula=' + formula + '&saBooksId=' + saBooksId,
		success: function (datas) {
			if (datas != '') {
				data = datas;
			}
		}
	});
	return data;
}

//执行ajax
function ajaxExecute(paramUrl) {
	var data = '';
	var urlPath = '/Salary2/Handler/GetSalaryInfo.ashx?' + new Date().getTime() + '&page=salaryBooksItem' + paramUrl;
	$.ajax({
		type: "GET",
		async: false,
		url: urlPath,
		success: function (datas) {
			if (datas != '') {
				data = datas;
			}
		}
	});
	return data;
}

//设置是否允许编辑
function setIsAllowEdit(selectedSaItem) {
	var isAllowEdit = GetIsAllowEdit(selectedSaItem);
	$('#txtDefaultValue').val('0.000');
	if (isAllowEdit == "0") {
		$('#txtDefaultValue').attr('disabled', 'disabled');
	} else {
		$('#txtDefaultValue').removeAttr('disabled');
		$('#chkIsFormula').removeAttr('checked');
		$('#chkIsDisplay').attr('checked', 'true');
		$('#btnFormula').attr('disabled', 'disabled');
		$('#txtFormula').val('');
		$('#hfldFormula').val('');
	}
}

//新增编辑
function edit(type) {
	$('#hfldType').val(type);
	var booksId = $('#hfldSaBooks').val();
	var id = $('#hfldCheckedId').val();
	var titleName = '新增帐套明细';
	$('select#ddlSalaryItem option:first').attr('selected', 'true');
	$('#txtDefaultValue').val('0.000');
	$('#txtDefaultValue').removeAttr('disabled');
	$('#chkIsFormula').removeAttr('checked');
	$('#chkIsDisplay').attr('checked', 'true');
	$('#btnFormula').attr('disabled', 'disabled');
	$('#txtFormula').val('');
	$('#hfldFormula').val('');
	if (type == 'edit') {
		titleName = '编辑帐套明细';
		if (id != '') {
			var data = GetById(id);
			if (data == '') {
				jw.alert('系统提示', '没有找到该帐套明细的信息');
			} else {
				//绑定工资项
				var selectedIndex = 0;
				$('select#ddlSalaryItem option ').each(function (index, domEle) {
					if (domEle.value == data.ItemId) {
						selectedIndex = index;
						return false;
					}
				});
				$('select#ddlSalaryItem')[0].selectedIndex = selectedIndex;
				$('#txtDefaultValue').val(_formatDecimal(data.DefaultValue.toString()));
				//是否启用公式
				if (data.IsFormula == true) {
					$('#chkIsFormula').attr('checked', 'true');
					$('#btnFormula').removeAttr('disabled');
					$('#txtDefaultValue').val('0.000');
					$('#txtDefaultValue').attr('disabled', 'disabled');
				} else {
					$('#chkIsFormula').removeAttr('checked');
					$('#btnFormula').attr('disabled', 'disabled');
					$('#txtDefaultValue').removeAttr('disabled');
				}
				//是否显示
				if (data.IsShow == true) {
					$('#chkIsDisplay').attr('checked', 'true');
				} else {
					$('#chkIsDisplay').removeAttr('checked');
				}
				if (data.Formula != null) {
					var formula = data.Formula;
					if (formula != '') {
						var formulaURl = formula.replace(/\+/g, "%2C"); //替换全部'+'
						var formulaIds = GetReplaceFormula(encodeURI(formulaURl), booksId);  //将工资项名称替换成明细Id
						$('#hfldFormula').val(formulaIds);
					}
				}
			}
		}
	}
	$('#divSalaryItemEdit').css('display', 'block').dialog({
		title: titleName,
		width: 450,
		height: 250,
		modal: true,
		buttons: [{
			text: '保存',
			handler: function () {
				saveData(type);
			}
		}, {
			text: '取消',
			handler: function () {
				$('#divSalaryItemEdit').dialog('close');
			}
		}]
	});
	var itemId = $('select#ddlSalaryItem  option:selected').val();
	var isAllowEdit = GetIsAllowEdit(itemId);
	if (isAllowEdit == "0") {
		$('#txtDefaultValue').attr('disabled', 'disabled');
	}
}


//保存数据
function saveData(type) {
	var itemId = $('select#ddlSalaryItem  option:selected').val();
	var saBooks = $('#hfldSaBooks').val();
	var count = GetCountByItemId(saBooks, itemId);
	if (type == 'add') {                //新增
		if (count > 0) {
			jw.alert('系统提示', '该工资项重复,请重新选择!');
			return false;
		} else {
			$('#divSalaryItemEdit').parent().appendTo($('form:first'));
			$('#btnSaveData').click();
		}
	} else if (type == 'edit') {        //编辑
		var id = $('#hfldCheckedId').val();
		var saBooksItem = GetById(id);
		if (saBooksItem != '') {
			if (saBooksItem.ItemId != itemId && count > 0) {
				jw.alert('系统提示', '该工资项重复,请重新选择!');
			} else {
				$('#divSalaryItemEdit').parent().appendTo($('form:first'));
				$('#btnSaveData').click();
			}
		}
	}
}
//根据选择的工资项获得是否编辑此明细
function GetIsAllowEdit(itemId) {
	var data = '';
	$.ajax({
		type: "GET",
		async: false,
		url: '/Salary2/Handler/GetSalaryInfoPlain.ashx?' + new Date().getTime() + '&type=IsAllowEdit&saItemId=' + itemId,
		success: function (datas) {
			if (datas != '') {
				data = datas;
			}
		}
	});
	return data;
}