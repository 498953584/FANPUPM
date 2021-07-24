$(document).ready(function () {
	var jwTable = new JustWinTable('gvEquipment');
	replaceEmptyTable('emptyEquipment', 'gvEquipment');
	setButton(jwTable, 'btnDel', 'btnEdit', 'btnQuery', 'hfldIdsChecked');
	jw.tooltip();
	$('#btnAdd').click(function () {
		openNewTab('add');
	});
	$('#btnEdit').click(function () {
		openNewTab('edit');
	});
	$('#btnQuery').click(checkEqu);
	var action = jw.getRequestParam('action');
	if (action == 'Query') {
		$('#btnAdd').hide();
		$('#btnEdit').hide();
		$('#btnDel').hide();
	}
});
//打开新的标签页 新增/编辑
function openNewTab(action) {
	var title = '新增设备台账';
	parent.desktop.EquipmentEdit = window;
	var urlStr = '/Equ/Equipment/EquipmentEdit.aspx?' + new Date().getTime() + '&action=' + action;
	if (action == 'edit') {
		urlStr = urlStr + '&id=' + $('#hfldIdsChecked').val();
		title = '编辑设备台账';
	}
	toolbox_oncommand(urlStr, title);
}
//打开新的标签页
function checkEqu(action) {
	var title = '查看设备台账';
	parent.desktop.EquipmentEdit = window;
	var urlStr = '/Equ/Equipment/EquipmentView.aspx?' + new Date().getTime() + '&id=' + $('#hfldIdsChecked').val();
	toolbox_oncommand(urlStr, title);
}