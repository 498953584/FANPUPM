$(document).ready(function () {
	var jwTable = new JustWinTable('gvRefuelApply');
	replaceEmptyTable('emptyRefuelApply', 'gvRefuelApply');
	setButton(jwTable, 'btnDel', 'btnEdit', 'btnQuery', 'hfldIdsChecked');
	jw.tooltip();
	//新增
	$('#btnAdd').click(function () {
		openNewTab('add');
	});
	//编辑
	$('#btnEdit').click(function () {
		openNewTab('edit');
	});
	//查看加油申请
	$('#btnQuery').click(function () {
		var url = '/Equ/ShipOilWear/RefuelApplyView.aspx?ic=' + $('#hfldIdsChecked').val();
		viewopen(url, '查看加油申请');
	});
	setWfButtonState2(jwTable, 'WF1');
});

// 选择项目
function openProjPicker() {
	jw.selectOneProject({ idinput: 'hfldPrjId', nameinput: 'txtPrjName' });
}
//打开新的标签页 新增/编辑
function openNewTab(action) {
	var title = '新增加油申请';
	parent.desktop.RefuelApplyEdit = window;
	var urlStr = '/Equ/ShipOilWear/RefuelApplyEdit.aspx?' + new Date().getTime() + '&action=' + action;
	if (action == 'edit') {
		urlStr = urlStr + '&id=' + $('#hfldIdsChecked').val();
		title = '编辑加油申请';
	}
	toolbox_oncommand(urlStr, title);
}