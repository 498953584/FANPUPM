$(document).ready(function () {
    var jwTable = new JustWinTable('gvwEquipmentType');
    replaceEmptyTable('emptyEquipmentType', 'gvwEquipmentType')
    jwTable.registClickTrListener(function () {
        $('#btnSave').removeAttr('disabled');
        $('#hfldId').val(this.id);
        $('#hfldName').val($(this).attr('name'));
    });
    //取消
    $('#btnCancel').click(function () {
        top.ui.closeWin();
    });
    $('#btnSave').click(function () {
        if ($('#hfldNoChildIdList').val().indexOf($('#hfldId').val()) == -1) {
            top.ui.alert("当前选中设备类型还有子类型，请选择其子类型！");
            return;
        }
        var equTypeInfo = {
            id: $('#hfldId').val(), name: $('#hfldName').val()
        };
        top.ui.callback(equTypeInfo);
        top.ui.callback = null;
        top.ui.closeWin();
    });
});