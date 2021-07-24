var prjId = '';
$(document).ready(function () {
    Val.validate('form1', 'btnSave');
    //取消按钮事件
    $('#btnCancel').click(function () {
        winclose('BudOilWearEdit', 'BudOilWearList.aspx', false);
    });
    prjId = $('#hfldPrjId').val();
});
// 选择项目
function openProjPicker() {
    jw.selectOneProject({ idinput: 'hfldPrjId', nameinput: 'txtPrjName' });
}
//选择任务
function selectTask() {
    var prjId = $('#hfldPrjId').val();
    if (prjId == '') {
        top.ui.alert('项目不能为空！');
        return;
    }
    var url = '/Equ/ShipOilWear/BudTaskList.aspx?' + new Date().getTime() + '&prjId=' + prjId;
    var taskDivInfo = { url: url, title: '选择任务', width: 1000, height: 500 };
    top.ui.callback = function (taskInfo) {
        $('#hfldTaskId').val(taskInfo.taskId);
        $('#txtTaskCode').val(taskInfo.taskCode);
    };
    top.ui.openWin(taskDivInfo);
}
//设置分部分项为空
function setEmpty() {
    var newPrjId = $('#hfldPrjId').val();
    if (newPrjId != prjId) {
        prjId = $('#hfldPrjId').val();
        $('#txtTaskCode').val('');
        $('#hfldTaskId').val('');
    }
}