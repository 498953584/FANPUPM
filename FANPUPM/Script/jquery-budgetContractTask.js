
//try {

//是否显示节点下的资源信息
var displayResource = undefined;
////是否是预算变更
//var budgetChange = false;
//页面地址
var pageURL = undefined;
//上移、下移借助下列图标进行判断
var $imgLminus = $('<img style="vertical-align:middle; "; src="/images/tree/lminus.gif" />');
var $imgTminus = $('<img style="vertical-align:middle; "; src="/images/tree/tminus.gif" />');
var $imgWhite = $('<img style="vertical-align:middle;"; src="/images/tree/white.gif" />');
var $imgI = $('<img style="vertical-align:middle;"; src="/images/tree/i.gif" />');
var $imgT = $('<img style="vertical-align:middle;"; src="/images/tree/t.gif" />');
var $imgL = $('<img style="vertical-align:middle;"; src="/images/tree/l.gif" />');
var $imgLplus = $('<img style="vertical-align:middle;"; src="/images/tree/lplus.gif" />');
var $imgTplus = $('<img style="vertical-align:middle;"; src="/images/tree/tplus.gif" />');

$(document).ready(function () {
	showMoreNote();
	pageURL = window.location.href.substring(window.location.href.lastIndexOf('/') + 1, window.location.href.indexOf('?'));
});

//设置隐藏控件
//user的值必须是编码后的值
//lock 隐藏控件值指定项目是否锁定 true表示锁定false表示未锁定
function setHiddenId(prjId, user, lock, editOrAdd) {
	hfldPrjId = prjId == undefined ? undefined : prjId;
	hfldUser = user == undefined ? undefined : user;
	hfldLock = lock == undefined ? undefined : lock;
	hfldEditOrAdd = editOrAdd == undefined ? undefined : editOrAdd;
}

var btnAdd;
var btnEdit;

//设置复选框控制的按钮
//必须存在checkedIds 即复选框选中节点Id保存到此隐藏控件中
//点击行是否显示资源信息
function setControlButton(checkedIds, add, edit, del, importExcel, importTemplate, saveAsTemplate,
    deployResource, isDisplayResource, btnUP, btnDown) {
	hfldCheckedIds = checkedIds == undefined ? undefined : checkedIds;

	btnAdd = add || '';
	btnEdit = edit || '';

	//绑定新增和编辑按钮事件
	if (btnAdd != '' && btnEdit != '') {
		$('#' + btnAdd).bind('click', openEdit_Add);
		$('#' + btnEdit).bind('click', openEdit_Add);
	}

	btnDel = del == undefined ? undefined : del;
	btnImportExcel = importExcel == undefined ? undefined : importExcel;
	btnImportTemplate = importTemplate == undefined ? undefined : importTemplate;
	btnSaveAsTemplate = saveAsTemplate == undefined ? undefined : saveAsTemplate;

	btnDeployResource = deployResource == undefined ? undefined : deployResource;
	displayResource = isDisplayResource == undefined ? undefined : isDisplayResource;

	btnMoveUp = btnUP == undefined ? undefined : btnUP;
	btnMoveDown = btnDown == undefined ? undefined : btnDown;

	//绑定上移下移事件
	if (btnMoveUp != undefined && btnMoveUp != '' && btnMoveDown != '' && btnMoveDown != undefined) {
		bindMoveUp_Down();
	}
}

//绑定页面编辑和新增页面按钮事件
function openEdit_Add() {
	var type = 'edit';
	var dialogTitle = '编辑';
	if (this.id.indexOf(btnAdd) != -1) {
		type = 'add';
		dialogTitle = '新增';
	}
	$('#hfldEditOrAdd').val(type);

	var id = $('#hfldCheckedIds').val();
	var layer = '';
	if (id != '') {
		if (type == 'edit')
			layer = $('#' + id).attr('layer');
		else
			layer = getTypeId($('#' + id).attr('layer'));
	}
	var parameters = '&type=' + type + '&layer=' + layer + '&id=' + id + '&page=' + pageURL;
	var prjId = $('#hfldPrjId').val();
	parameters += '&prjId=' + prjId;
	dialogTitle += '任务节点';
	top.ui._BudContractTaskTaskEdit = window;
	top.ui.callback = function (type, task) {
		sendTaskData(type, task);
	};
	var url = '/BudgetManage/Budget/BudContractTaskTaskEdit.aspx?' + new Date().getTime() + parameters;
	top.ui.openWin({ title: dialogTitle, url: url, width: 600, height: 460 });
	//$('#editTaskFrame').attr('src', '/BudgetManage/Budget/BudContractTaskTaskEdit.aspx?' + new Date().getTime() + parameters);
	//$('#editTask').dialog({ width: 600, height: 330, modal: true, title: dialogTitle });
}

// 保存按钮传送数据进行修改或新增
function sendTaskData(type, task) {
	task = eval(task);
	var checkTaskId = $('#' + hfldCheckedIds).val();
	if (type == 'add') {
		// 新增
		var $row = addRowData(checkTaskId);
		if (!$row) return false;
		var $tdArr = $row.find('TD');
		$row.attr('id', task.TaskId);
		$row.attr('orderNumber', task.OrderNumber);
		$row.attr('subCount', 0);
		$($tdArr[2]).find('SPAN').html(task.TaskName);      // 名称
		$($tdArr[3]).html(task.TaskCode);                   // 编码
		$($tdArr[5]).html(task.Unit);                       // 单位
		$($tdArr[6]).html(toFixed(task.Quantity, 2));       // 工程量
		$($tdArr[7]).html(jw.jsonToDate(task.StartDate));   // 开始时间
		$($tdArr[8]).html(jw.jsonToDate(task.EndDate));     // 结束时间
		$($tdArr[9]).html(task.ConstructionPeriod);         // 工期
		$($tdArr[10]).html(toFixed(task.UnitPrice, 3));     // 综合单价
		$($tdArr[11]).html(toFixed(task.Total, 3));         // 小计
		$($tdArr[12]).html(task.Note);                      // 备注

		//更新序号
		setNumber($row.get(0));
		//更新复选框的Title
		$row.get(0).cells[0].childNodes[0].title = task.TaskId;
	} else if (type == 'edit') {
		// 编辑
		var id = $('#' + hfldCheckedIds).val();
		var $tdArr = $('#' + id).find('TD');

		$($tdArr[2]).find('SPAN').html(task.TaskName);      // 名称
		$($tdArr[5]).html(task.Unit);                       // 单位
		$($tdArr[6]).html(toFixed(task.Quantity, 2));       // 工程量
		$($tdArr[7]).html(jw.jsonToDate(task.StartDate));   // 开始时间
		$($tdArr[8]).html(jw.jsonToDate(task.EndDate));     // 结束时间
		$($tdArr[9]).html(task.ConstructionPeriod);         // 工期
		$($tdArr[10]).html(toFixed(task.UnitPrice, 3));     // 综合单价
		$($tdArr[11]).html(toFixed(task.Total, 3));         // 小计
		$($tdArr[12]).html(task.Note);                      // 备注
	}
	return;
}

//新增任务节点
function addRowData(parentId) {
	var $newRow;
	//无父节点
	if (parentId == '' || parentId == undefined) {
		if ($('#gvBudget').get(0) != undefined) {
			var $lastUnitProject;
			for (k = $('#gvBudget').get(0).rows.length - 1; k > 0; k--) {
				if ($('#gvBudget tr:eq(' + k + ')').get(0).layer == 1) {
					$lastUnitProject = $('#gvBudget tr:eq(' + k + ')');
					break;
				}
			}
			//设置最后一个单位工程图标
			$lastUnitProjectImg = $($lastUnitProject.get(0).cells[2].children[0]);
			$newRow = $lastUnitProject.clone();
			$($.getChildren($lastUnitProject.get(0))).hide();
			if ($lastUnitProjectImg.get(0).src == $imgL.get(0).src) {
				$lastUnitProjectImg.replaceWith($imgT.clone());
			} else {
				$lastUnitProjectImg.replaceWith($imgTplus.clone());
				$($newRow.get(0).cells[2].children[0]).replaceWith($imgL.clone());
			}
			//此时设置新增节点的序号
			var prjId = $('#' + hfldPrjId).val();
			getURL = '/BudgetManage/Handler/BudgetContractTask.ashx?' + new Date().getTime() + '&prjId=' + prjId + '&type=count';
			$.ajax({
				type: 'GET',
				async: false,
				url: getURL,
				success: function (data) {
					$newRow.get(0).cells[1].innerText = parseInt(data) - 1;
				}
			});
			$('#gvBudget tr:last').after($newRow);
		}
		//不存在项目节点
		else {
			window.location.href = window.location.href.substring(0, window.location.href.indexOf('?', 0)) +
                '?prjId=' + $('#hfldPrjId').val() + '&year=' + $('#ddlYear').val();
		}
	}
	//有父节点
	else {
		var parentRow = $('#' + parentId).get(0);
		//父节点无子节点
		if (parentRow.subCount == 0) {
			$newRow = $(parentRow).clone();
			$newRow.get(0).layer = parseInt(parentRow.layer) + 1;
			$newRow.get(0).orderNumber += '001';
			var lastImageIndex = parseInt(parentRow.layer) - 1;
			var $lastImage = $($newRow.get(0).cells[2].children[lastImageIndex]);
			//更新父节点图标
			var $parentImg;
			if ($lastImage.get(0).src != $imgL.get(0).src) {
				$parentImg = $imgTminus.clone();
				//更新子节点图标
				$lastImage.before($imgI.clone());
			} else {
				$parentImg = $imgLminus.clone();
				$lastImage.before($imgWhite.clone());
			}
			$lastImage.replaceWith($imgL.clone());
			//添加新增节点
			$(parentRow).after($newRow);
			$(parentRow.cells[2].lastChild).remove();   //删掉父节点中的图标
		}
		else {//父节点有子节点
			var lastChildRow = getLastTask(parentRow);
			if (lastChildRow == undefined) {
				parentRow.subCount++;
				setNumber(parentRow, false);
				return;
			}
			var $img;
			var lastImagIndex = parseInt(lastChildRow.layer) - 1;
			//最后一个子节点无子节点
			if (lastChildRow.subCount == 0) {
				$img = $imgT.clone();
				$newRow = $(lastChildRow).clone();
			} else {//最后一个子节点有子节点
				$img = $imgTplus.clone();
				$newRow = $(lastChildRow).clone();
				$($.getChildren(lastChildRow)).hide();
				$($newRow.get(0).cells[2].children[lastImagIndex]).replaceWith($imgL.clone());
			}
			//更新最后子节点图标
			$(lastChildRow.cells[2].children[lastImagIndex]).replaceWith($img);
			//添加新增节点
			$(lastChildRow).after($newRow);
		}
		parentRow.subCount++;
	}
	registerImg();
	return $newRow;
}

//父节点下的最后一个子节点
function getLastTask(parentRow) {
	var $lplus = $(parentRow).find('img[src*="lplus.gif"]');
	var $tplus = $(parentRow).find('img[src*="tplus.gif"]');
	if ($lplus.get(0) != undefined) {
		$lplus.click();
	}
	else if ($tplus.get(0) != undefined) {
		$tplus.click();
	}
	var pIndex = parseInt(parentRow.rowIndex);
	var subCount = parentRow.subCount.toString();
	if (subCount.length == 1)
		subCount = '00' + subCount;
	else if (subCount.length == 2)
		subCount = '0' + subCount;

	for (i = pIndex + 1; i < $('#gvBudget').get(0).rows.length; i++) {
		var $tr = $('#gvBudget tr:eq(' + i + ')');
		if ($tr.get(0).orderNumber == parentRow.orderNumber + subCount) {
			return $tr.get(0);
		}
	}
}


//更新序号
function setNumber(row) {
	//限制父节点时不进行修改序号
	if (arguments.length == 1) {
		if ($('#gvBudget tr').get(parseInt(row.rowIndex) + 1) != undefined)
			row.cells[1].innerText = $('#gvBudget tr').get(parseInt(row.rowIndex) + 1).cells[1].innerText;
		else
			row.cells[1].innerText = parseInt(row.cells[1].innerText) + 1;
	}
	$('#gvBudget tr:gt(' + row.rowIndex + ')').each(function () {
		this.cells[1].innerText = parseInt(this.cells[1].innerText) + 1;
	});
}

// 格式化时间yyyy-MM-dd
function formatDate(date) {
	var array = date.split('-');
	if (array[1].length == 1) {
		array[1] = '0' + array[1];
	}
	if (array[2].length == 1) {
		array[2] = '0' + array[2];
	}
	return array.join('-');
}

//单击行
$('#gvBudget tr').live('click', function () {
	if (this.id == '') return;
	var $Tr = $('#' + this.id);
	if ($Tr.attr('subCount') == undefined) {
		$('#' + hfldCheckedIds).val('');
		setNoSelected();
		return;
	}
	setSingle(this.id);
	upAdminPrivilege();
	var userCode = getCookie('UserCode');
	if (userCode == '00000000') {
		if (typeof hfldLock != 'undefined') {
			if ($('#' + hfldLock).val() == 'True') {

				$('#' + btnEdit).attr('disabled', 'disabled');
				$('#' + btnAdd).attr('disabled', 'disabled');
			}
		}
	}
});

//单击复选框
function checkSingle() {
	//选中复选框的节点Id
	var checkedIds = getCheckedIds('gvBudget');
	//选中的个数
	var checkedCount = getCheckedCount(checkedIds);
	//    //是否是锁定项目  
	if (checkedCount == 0) {
		setNoSelected();
	}
	else if (checkedCount == 1) {
		setSingle(checkedIds);
		upAdminPrivilege();
		var userCode = getCookie('UserCode');
		if (userCode == '00000000') {
			if (typeof hfldLock != 'undefined') {
				if ($('#' + hfldLock).val() == 'True') {
					$('#' + btnEdit).attr('disabled', 'disabled');
					$('#' + btnAdd).attr('disabled', 'disabled');
				}
			}
		}
	}
	else {//多选择
		setMultiple(checkedIds);
	}

}

//全选复选框
function checkAll() {
	var checkedIds = getCheckedIds('gvBudget');
	var checkedCount = getCheckedCount(checkedIds);
	if (checkedCount == 0) {
		setNoSelected();
	}
	else if (checkedCount == 1) {
		setSingle(checkedIds);
		upAdminPrivilege();
		var userCode = getCookie('UserCode');
		if (userCode == '00000000') {
			if (typeof hfldLock != 'undefined') {
				if ($('#' + hfldLock).val() == 'True') {
					$('#' + btnEdit).attr('disabled', 'disabled');
					$('#' + btnAdd).attr('disabled', 'disabled');
				}
			}
		}
	}
	else {
		setMultiple(checkedIds);
	}

}

//复选框选中值
function getCheckedIds(tableId) {
	var chkids = new Array();
	var trs = $('#' + tableId).get(0).getElementsByTagName('TR');
	for (var i = 1; i < trs.length; i++) {
		if ($(trs[i]).find("input").get(0) == undefined)
			continue;
		var chk = $(trs[i]).find("input")[0].checked;
		if (chk) {
			chkids.push($(trs[i]).get(0).id);
		}
	}
	var str = '["';
	if (chkids.length == 1) {
		return chkids[0];
	}
	else if (chkids.length == 0) {
		return '';
	}
	for (var i = 0; i < chkids.length; i++) {
		str += chkids[i] + '","';
	}
	return str.substring(0, str.length - 2) + ']';
}

//选中复选框个数
function getCheckedCount(checkedIds) {
	var checkedCount = 0;
	if (checkedIds == '')
		checkedCount = 0;
	else if (checkedIds.indexOf('[', 0) > -1)
		checkedCount = 2;
	else
		checkedCount = 1;
	return checkedCount;
}

//禁用按钮
function disabledButton() {
	if (typeof btnDel != 'undefined' && btnDel != '')
		$('#' + btnDel).attr('disabled', 'disabled');
	if (typeof btnSaveAsTemplate != 'undefined' && btnSaveAsTemplate != '')
		$('#' + btnSaveAsTemplate).attr('disabled', 'disabled');
	if (typeof btnEdit != 'undefined' && btnEdit != '')
		$('#' + btnEdit).attr('disabled', 'disabled');
	//资源配置
	if (typeof btnDeployResource != 'undefined' && btnDeployResource != '')
		$('#' + btnDeployResource).attr('disabled', 'disabled');
	//禁用上移、下移按钮
	if (typeof btnMoveUp != 'undefined' && btnMoveUp != '' && typeof btnMoveDown != 'undefined' && btnMoveDown != '') {
		$('#' + btnMoveUp).attr('disabled', 'disabled');
		$('#' + btnMoveDown).attr('disabled', 'disabled');
	}
}

//无选择复选框设置
function setNoSelected() {
	disabledButton();
	var isLocked = 'False';
	if (typeof hfldLock != 'undefined')
		isLocked = $('#' + hfldLock).val();
	if (isLocked == 'False') {
		if (typeof btnAdd != 'undefined' && btnAdd != '') {
			$('#' + btnAdd).removeAttr('disabled');
		}
		if (typeof btnImportExcel != 'undefined' && btnImportExcel != '')
			$('#' + btnImportExcel).removeAttr('disabled');
		if (typeof btnImportTemplate != 'undefined' && btnImportTemplate != '')
			$('#' + btnImportTemplate).removeAttr('disabled');
	}
	$('#' + hfldCheckedIds).val('');
}

//复选框单选设置
function setSingle(checkedIds) {
	if (typeof btnSaveAsTemplate != 'undefined' && btnSaveAsTemplate != '')
		$('#' + btnSaveAsTemplate).removeAttr('disabled');
	var isLocked = 'False';
	if (typeof hfldLock != 'undefined')
		isLocked = $('#' + hfldLock).val();
	if (isLocked == 'False') {//未锁定激活按钮
		if (typeof btnDel != 'undefined' && btnDel != '')
			$('#' + btnDel).removeAttr('disabled');
		if (typeof btnEdit != 'undefined' && btnEdit != '')
			$('#' + btnEdit).removeAttr('disabled');
		if (typeof btnImportTemplate != 'undefined' && btnImportTemplate != '')
			$('#' + btnImportTemplate).removeAttr('disabled');
		if (typeof btnImportExcel != 'undefined' && btnImportExcel != '')
			$('#' + btnImportExcel).removeAttr('disabled');
		if (typeof btnAdd != 'undefined' && btnAdd != '') {
			$('#' + btnAdd).removeAttr('disabled');
		}
		//资源配置
		if (typeof btnDeployResource != 'undefined' && btnDeployResource != '') {
			var subCount = $('#' + checkedIds).get(0).subCount;
			if (subCount == '0') {
				$('#' + btnDeployResource).removeAttr('disabled');
			}
			else {
				$('#' + btnDeployResource).attr('disabled', 'disabled');
			}
		}
	}
	getIsHaveResource(checkedIds);
	$('#' + hfldCheckedIds).val(checkedIds);
}

//返回选中行的同级别的行，如果不存在返回null
//compareOrderNumber 选中行的的
//startId 进行向前查询行的起止行Id
function getPrevTr(compareOrderNumber, startId) {
	var prevTr = $('#' + startId).prev().get(0);
	var prevTrOrderNumber = prevTr.orderNumber;
	if (prevTrOrderNumber != undefined) {
		if (compareOrderNumber.length == 3) {
			if (compareOrderNumber.length == prevTrOrderNumber.length)
				return prevTr;
			else
				return arguments.callee(compareOrderNumber, prevTr.id);
		}
		else if (prevTrOrderNumber.length == compareOrderNumber.toString().length &&
            prevTrOrderNumber.substr(0, prevTrOrderNumber.length - 3) ==
                compareOrderNumber.toString().substr(0, compareOrderNumber.toString().length - 3)) {
			return prevTr;
		}
		else if (prevTrOrderNumber.length + 3 == compareOrderNumber.toString().length &&
            prevTrOrderNumber == compareOrderNumber.toString().substr(0, compareOrderNumber.toString().length - 3)) {
			return null;
		}
		else {
			return arguments.callee(compareOrderNumber, prevTr.id);
		}
	}
}

//返回选中行的同级别的Tr，如果不存在返回null
//compareOrderNumber 选中行的的
//startId 进行向前查询行的起止行Id
function getNextTr(compareOrderNumber, startId) {
	var nextTr = $('#' + startId).next().get(0);
	var nextTrOrderNumber = nextTr.orderNumber;
	if (nextTrOrderNumber != undefined) {
		if (compareOrderNumber.length == 3) {
			if (nextTrOrderNumber.length == compareOrderNumber.length)
				return nextTr;
			else
				return arguments.callee(compareOrderNumber, nextTr.id);
		}
		else if (compareOrderNumber.length == nextTrOrderNumber.length &&
            compareOrderNumber.toString().substr(0, compareOrderNumber.length - 3) ==
                nextTrOrderNumber.substr(0, nextTrOrderNumber.length - 3)) {
			return nextTr;
		}
		else {
			return arguments.callee(compareOrderNumber, nextTr.id);
		}
	}
}

//得到对应的节点图标
function getOppositeImg(argImg) {
	if (argImg.src == $imgT[0].src)
		return $imgL;
	else if (argImg.src == $imgL[0].src) {
		return $imgT;
	}
	else if (argImg.src == $imgLminus[0].src || argImg.src == $imgLplus[0].src)
		return $imgTplus;
	else if (argImg.src == $imgTminus[0].src || argImg.src == $imgTplus[0].src)
		return $imgLplus;
}

//复选框多选设置
function setMultiple(checkedIds) {
	var isLocked = 'False';
	var userCode = getCookie('UserCode');
	if (typeof hfldLock != 'undefined')
		isLocked = $('#' + hfldLock).val();
	if ((isLocked == 'False' || isLocked == 'True') && userCode == '00000000') {
		if (btnDel != undefined && btnDel != '')
			$('#' + btnDel).removeAttr('disabled');
	}
	if (typeof btnSaveAsTemplate != 'undefined' && btnSaveAsTemplate != '')
		$('#' + btnSaveAsTemplate).removeAttr('disabled');
	if (typeof btnEdit != undefined && btnEdit != '')
		$('#' + btnEdit).attr('disabled', 'disabled');
	if (typeof btnImportTemplate != 'undefined' && btnImportTemplate != '')
		$('#' + btnImportTemplate).attr('disabled', 'disabled');
	if (typeof btnAdd != 'undefined' && btnAdd != '') {
		$('#' + btnAdd).attr('disabled', 'disabled');
		
	}
	if (typeof btnImportExcel != 'undefined' && btnImportExcel != '')
		$('#' + btnImportExcel).attr('disabled', 'disabled');
	//资源配置
	if (typeof btnDeployResource != 'undefined' && btnDeployResource != '') {
		$('#' + btnDeployResource).attr('disabled', 'disabled');
	}

	$('#' + hfldCheckedIds).val(checkedIds);
}

//获取任务类型的下级类型
function getTypeId(layer) {
	var typeId = undefined;
	$.ajax({
		type: 'GET',
		async: false,
		url: '/BudgetManage/Handler/GetNextTaskType.ashx?' + new Date().getTime() + '&layer=' + layer,
		success: function (data) {
			typeId = data;
		}
	});
	return typeId;
}

//异步请求
function getIsHaveResource(taskId) {
	var isHanveResource = null;
	$.ajax({
		type: 'GET',
		async: false,
		url: '/BudgetManage/Handler/IsLeafNode.ashx?' + new Date().getTime() + '&id=' + taskId + '&type=ContractTask',
		success: function (data) {
			// data表示该节点的资源数量
			if (data != "0") {
				if (btnAdd != undefined && btnAdd != '') {
					$('#' + btnAdd).attr('disabled', 'disabled');
				}
				if (btnImportExcel != undefined && btnImportExcel != '')
					$('#' + btnImportExcel).attr('disabled', 'disabled');
			}
		}
	});
}

//备注信息提示
function showMoreNote() {
	var tab = document.getElementById('gvBudget');
	if (tab != null) {
		for (i = 1; i < tab.rows.length; i++) {
			var cells = tab.rows[i].cells;
			if (cells.length == 1) return;
			if (cells[cells.length - 1].children.length == 0) return;
			var imgId = cells[cells.length - 1].children[0].id;
			var altLength = document.getElementById(imgId).title.length;
			if (altLength > 15) {
				$('#' + imgId).css('display', '');
				$('#' + imgId).tooltip({
					track: true,
					delay: 0,
					showURL: false,
					fade: true,
					showBody: " - ",
					extraClass: "solid 1px blue",
					fixPNG: true,
					left: event.offsetX
				});
			} else {
				document.getElementById(imgId).title = '';
			}
		}
	}
}

//添加行进行显示资源信息
var prevId;
function showInfo(id) {
	var tab_Resource = null;
	$.ajax({
		type: 'GET',
		async: false,
		url: '/BudgetManage/Handler/ReturnResource.ashx?' + new Date().getTime() + '&taskId=' + id + '&type=deploy',
		success: function (data) {
			tab_Resource = data;
		}
	});
	var isDisplay = $('#' + id + '1').get(0);
	if (isDisplay == undefined) {
		$('#' + id).after('<tr id="' + id + '1"><td align="center" colspan="12" style="border:solid 1px #CADEED;">' + tab_Resource + '</td></tr>');
		if (prevId != undefined && prevId != id) {
			$('#' + prevId + '1').remove();
		}
		prevId = id;

	}
	else {
		$('#' + id + '1').remove();
		isDisplay = undefined;
	}
}

//注册图标点击事件
function registerImg() {
	$('#gvBudget tr:gt(0)').each(function (index) {
		var $tr = $(this);
		$tr.find('img[src*="tminus.gif"]').die();
		$tr.find('img[src*="lminus.gif"]').die();
		$tr.find('img[src*="lplus.gif"]').die();
		$tr.find('img[src*="tplus.gif"]').die();
		$tr.find('img[src*="tminus.gif"]').live('click', function (event) {
			$($.getChildren($tr[0])).hide();
			this.src = '/images/tree/tplus.gif';
		});

		$tr.find('img[src*="lminus.gif"]').live('click', function (event) {
			$($.getChildren($tr[0])).hide();
			this.src = '/images/tree/lplus.gif';
		});

		$tr.find('img[src*="lplus.gif"]').live('click', function (event) {
			$.addChildren($tr[0])
			this.src = '/images/tree/lminus.gif';
		});

		$tr.find('img[src*="tplus.gif"]').live('click', function (event) {
			$.addChildren($tr[0]);
			this.src = '/images/tree/tminus.gif';
		});
	});
}
//} catch (er) {
//    alert(er);
//}