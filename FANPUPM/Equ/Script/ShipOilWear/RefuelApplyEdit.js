$(document).ready(function () {
	Val.validate('form1', 'btnSave');
	//取消按钮事件
	$('#btnCancel').click(function () {
		winclose('RefurlApplyEdit', 'RefuelApplyList.aspx', false);
	});
	$('#txtRefuelTotal').blur(compute); 		//修改累计加油数量时，重新计算差异
	$('#txtCompletedQuantity').blur(compute); //修改累计完成量时，重新计算应该加油量和差异
});
// 选择项目
function openProjPicker() {
	jw.selectOneProject({ idinput: 'hfldPrjId', nameinput: 'txtPrjName' });
}
//选择申请加油船
function selectEqu() {
	var url = '/Equ/Equipment/SelectEquipment.aspx?' + new Date().getTime();
	var taskDivInfo = { width: 800, height: 500, url: url, title: '选择申请加油船' };
	top.ui.callback = function (equInfo) {
		$('#hfldApplyEquId').val(equInfo.id);
		$('#txtApplyEquCode').val(equInfo.code);
	};
	top.ui.openWin(taskDivInfo);
}
//选择油耗预算
function selectBudOilWear() {
	var prjId = $('#hfldPrjId').val();
	if (prjId == '') {
		top.ui.alert('项目不能为空！');
		return;
	}
	var url = '/Equ/ShipOilWear/SelectBudOilWear.aspx?' + new Date().getTime() + '&prjId=' + prjId;
	var oilWearDivInfo = { url: url, title: '选择油耗预算', width: 900, height: 550 };
	top.ui.callback = function (budOilWearInfo) {
		$('#hfldBudOilWearId').val(budOilWearInfo.id);
		$('#txtBudOilWearCode').val(budOilWearInfo.code);
		$('#txtQuotaOilWear').val(_formatDecimal(budOilWearInfo.quotaOilWear));
		$('#txtTaskCode').val(budOilWearInfo.taskCode);
		compute(); 	//计算应该加油量和差异
	};
	top.ui.openWin(oilWearDivInfo);
}
//计算应该加油量和差异
function compute() {
	var quotaOilWear = 0.000; 	//定额油耗
	if (Trim($('#txtQuotaOilWear').val()) != '') {
		quotaOilWear = parseFloat($('#txtQuotaOilWear').val().replace(/\,/g, ''));
	}
	var refuelTotal = 0.000; 		//开工至今累计加油量
	if (Trim($('#txtRefuelTotal').val()) != '') {
		refuelTotal = parseFloat($('#txtRefuelTotal').val().replace(/\,/g, ''));
	}
	var completedQuantity = 0.000; //累计完成量
	if (Trim($('#txtCompletedQuantity').val()) != '') {
		completedQuantity = parseFloat($('#txtCompletedQuantity').val().replace(/\,/g, ''));
	}
	var total = parseFloat(quotaOilWear) * completedQuantity / 1000; 		//计算应该加油量，单位换算为吨
	var difference = refuelTotal - total; 				//差异=累计加油量-应该加油量
	$('#txtShouldRefuel').val(_formatDecimal(total.toString()));
	$('#txtDifference').val(_formatDecimal(difference.toString()));
}
//选择现场负责人
function selectUser() {
	jw.selectOneUser({ nameinput: 'txtLocaleLeader', codeinput: 'hfldLocaleLeader' });
}