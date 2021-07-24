$(document).ready(function () {
    var jwTable = new JustWinTable('gvEquOilWear');
    replaceEmptyTable('emptyEquOilWear', 'gvEquOilWear');
    jw.tooltip();
    //单击行
    $('#gvEquOilWear tr:gt(0)').live('click', function () {
        $('#hfldIdsChecked').val($(this).attr('id'));
        $('#hfldCode').val($(this).attr('code'));
        var taskId = $(this).attr('taskId');
        $('#hfldTaskCode').val(getTaskCode(taskId)); //根据taskId获得任务编号
        $('#hfldQuotaOilWear').val($(this).attr('quotaOilWear'));
        $('#btnSave').removeAttr('disabled');
    });
    //取消
    $('#btnCancel').click(function () {
        top.ui.closeWin();
    });
    //保存
    $('#btnSave').click(function () {
        var oilWearInfo = {
            id: $('#hfldIdsChecked').val(), code: $('#hfldCode').val(),
            taskCode: $('#hfldTaskCode').val(), quotaOilWear: $('#hfldQuotaOilWear').val()
        };
        top.ui.callback(oilWearInfo);
        top.ui.callback = null;
        top.ui.closeWin();
    });
});
//根据任务Id获得任务编号
function getTaskCode(taskId) {
    var taskInfo;
    $.ajax({
        type: 'GET',
        async: false,
        url: '/BudgetManage/Handler/GetTaskInfo.ashx?' + new Date().getTime() + '&taskId=' + taskId,
        success: function (data) {
            taskInfo = data;
        }
    });
    if (taskInfo == null)
        return '';
    else
        return taskInfo.TaskCode;
}