$(document).ready(function () {
    var jwTable = new JustWinTable('gvEquOilWear');
    replaceEmptyTable('emptyEquOilWear', 'gvEquOilWear');
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
    //查看油耗管理
    $('#btnQuery').click(function () {
        var url = '/Equ/ShipOilWear/BudOilWearView.aspx?ic=' + $('#hfldIdsChecked').val();
        viewopen(url, '查看油耗预算');
    });
    setWfButtonState2(jwTable, 'WF1');
});

// 选择项目
function openProjPicker() {
    jw.selectOneProject({ idinput: 'hfldPrjId', nameinput: 'txtPrjName' });
}
//打开新的标签页 新增/编辑
function openNewTab(action) {
    var title = '新增油耗预算';
    parent.desktop.BudOilWearEdit = window;
    var urlStr = '/Equ/ShipOilWear/BudOilWearEdit.aspx?' + new Date().getTime() + '&action=' + action;
    if (action == 'edit') {
        urlStr = urlStr + '&id=' + $('#hfldIdsChecked').val();
        title = '编辑油耗预算';
    }
    toolbox_oncommand(urlStr, title);
}