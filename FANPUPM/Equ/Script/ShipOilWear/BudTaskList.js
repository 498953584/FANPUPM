$(document).ready(function () {
    showTooltip('tooltip', 10);
    $('#gvBudget').treetable(false, 'BudTaskList');
    replaceEmptyTable('emptyBudget', 'gvBudget');
    //单击行
    $('#gvBudget tr:gt(0)').live('click', function () {
        var isLeaf;
        //判断选择的是否是子节点，如果是不是子节点禁用保存按钮
        $.ajax({
            type: 'GET',
            async: false,
            url: '/BudgetManage/Handler/IsLeafNode.ashx?' + new Date().getTime() + '&Type=Task&Id=' + this.id,
            success: function (data) {
                isLeaf = data;
            }
        });
        if (isLeaf == '0') {
            //如果选中的节点非叶子节点
            $('#hfldCheckedIds').val('');
            $('#hfldTaskCode').val('');
            $('#btnSave').attr('disabled', 'disabled');
            return;
        } else {
            //如果选中的节点是叶子节点
            $('#hfldCheckedIds').val($(this).attr('id'));
            var taskCode = getTaskCode($(this).attr('id')); //得到任务编号
            $('#hfldTaskCode').val(taskCode);
            $('#btnSave').removeAttr('disabled');
        }
    });
    //取消
    $('#btnCancel').click(function () {
        top.ui.closeWin({ winNo: getRequestParam('winNo') });
    });
    //保存
    $('#btnSave').click(function () {
        var taskInfo = { taskId: $('#hfldCheckedIds').val(), taskCode: $('#hfldTaskCode').val() };
        top.ui.callback(taskInfo);
        top.ui.closeWin({ winNo: getRequestParam('winNo') });
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
    if (getRequestParam('type') == 'taskName') {
        return taskInfo.TaskName;
    }
    else {
        return taskInfo.TaskCode;
    }
}