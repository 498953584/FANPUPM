$(document).ready(function () {
	Val.validate('form1', 'btnSave');
	$('#btnCancel').click(CancelClick);
});
//选择资源
function SelectRes() {
	var url = '/Equ/Equipment/SelectResource.aspx?' + new Date().getTime();
	var resInfo = { width: 1000, height: 550, url: url, title: '选择资源' };
	top.ui.callback = function (resInfo) {
		$('#hfldResId').val(resInfo.resId);
		$('#txtResName').val(resInfo.resName);
		$('#txtSpecification').val(resInfo.specification);
		$('#hfldCorpId').val(resInfo.corpId)
		$('#txtCorpName').val(resInfo.corpName)
	};
	top.ui.openWin(resInfo);
}
//选择设备类别
function SelectEquType() {
	var url = '/Equ/Equipment/SelectEquipmentType.aspx?' + new Date().getTime();
	var equTypeInfo = { width: 1000, height: 550, url: url, title: '选择设备类别' };
	top.ui.callback = function (equTypeInfo) {
		$('#hfldEquTypeId').val(equTypeInfo.id);
		$('#txtEquTypeName').val(equTypeInfo.name);
	};
	top.ui.openWin(equTypeInfo);
}
//选择制造厂家
function SelectCorp() {
	jw.selectOneCorp({ idinput: 'hfldCorpId', nameinput: 'txtCorpName' });
}
//取消按钮事件
function CancelClick() {
	winclose('EquipmentEdit.aspx', 'EquipmentList.aspx', false);
}