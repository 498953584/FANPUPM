
$(document).ready(function () {
    var type = jw.getRequestParam('type');
    // 编辑
    if (type == 'edit') {
        //		var modifyTaskId = jw.getRequestParam('modifyTaskId');
        // 不可以编辑变更类型
        $('#ModifyType').attr('readOnly', true);
        $('#txtTaskCode').attr('readOnly', true);
        //$('#txtTaskName').attr('readOnly', true);
        $('#img').attr('disabled', true);
    }
});

// 变更类型发生变化
function modifyTypeChange() {
    var opt = $('#ModifyType').val();
    $('#hfldModifyType').val(opt);
    if (opt == '0') {
        $('#lbltaskName').text('上级任务');
        $('#lblModifyTaskName').text('节点名称');
        $('#lblTaskCode').text('清单编号');
        $('#txtTaskCode').parent().removeClass('readonly');
        $('#txtTaskCode').removeAttr('disabled');
        //        $('#txtUnitPrice').parent().removeClass('readonly');
        $('#txtUnitPrice').removeAttr('disabled');
        $('#txtStartDate').parent().removeClass('readonly');
        $('#txtStartDate').removeAttr('disabled');
        $('#txtEndDate').parent().removeClass('readonly');
        $('#txtEndDate').removeAttr('disabled');
        $('#txtConstructPeriod').parent().removeClass('readonly');
        $('#txtConstructPeriod').removeAttr('disabled');
    } else {
        $('#lbltaskName').text('变更任务');
        $('#lblModifyTaskName').text('变更内容');
        $('#lblTaskCode').text('节点名称');
        $('#txtTaskCode').parent().addClass('readonly'); //.attr('disabled', 'disabled');
        $('#txtTaskCode').attr('disabled', 'disabled');
        $('#txtUnitPrice').parent().addClass('readonly');
        //$('#txtUnitPrice').attr('disabled', 'disabled');
        //$('#txtStartDate').parent().addClass('readonly');
        //$('#txtStartDate').attr('disabled', 'disabled');
        //$('#txtEndDate').parent().addClass('readonly');
        //$('#txtEndDate').attr('disabled', 'disabled');
        //$('#txtConstructPeriod').parent().addClass('readonly');
        //$('#txtConstructPeriod').attr('disabled', 'disabled');
    }
}

// 选择任务
function selectBudTask() {
    var opt = $('#ModifyType').val();
    var contractId = $('#hfldContractId').val();
    var type;
    if (contractId != "") {
        type = "con";
    }

    if (typeof (_callback) != 'function')
        _callback = top.ui.callback;
    top.ui.callback = function (t) {
        try {
            if (validateTaskId(t.taskId) == false)
                return false; // 验证

            initTask(t.taskId); // 初始化任务
        } catch (ex) {
            alert(ex);
        }
        top.ui.callback = _callback;
        _callback = null;
    }
    var url = "/ContractManage/PayoutContract/PayoutTarget.aspx?prjId=" + jw.getRequestParam('prjId') + "&mType=" + opt + "&contractId=" + contractId + "&type=" + type;
    top.ui.openWin({ title: '选择目标成本', url: url, winNo: 2, width: 1000, height: 500 });
}

// 验证选择的任务是否合法
function validateTaskId(taskId) {
    if ($('#ModifyType').val() == '0') {
        // 清单外, 不允许变更已经配置资源的节点
        var param = '&taskId=' + taskId;
        var resCount = jw.runSql({ id: 'GetTaskResCount', param: param });
        if (resCount > 0) {
            top.ui.alert('已经配置资源的任务不能进行清单外变更');
            return false;
        }
    } else {
        // 清单内，不能对清单外的节点进行清单内变更
        var task = getTaskInfo(taskId);
    }
    return true;
}


//获得选中的任务节点信息
function getTaskInfo(taskId, modifyType) {
    var data = '';
    $.ajax({
        type: "GET",
        async: false,
        url: '/BudgetManage/Handler/GetTaskInfo.ashx?' + new Date().getTime() + '&taskId=' + taskId,
        success: function (task) {
            data = task;
        }
    });
    return data;
}

// 初始化任务信息
function initTask(taskId) {
    $('#hfldOriginalTaskId').val(taskId); 	// 保存将要进行变更节点的TaskId
    var task = getTaskInfo(taskId);

    if ($('#ModifyType').val() == '0') {
        // 清单外
        $('#txtTaskName').val(task.TaskCode);
    } else {
        // 请单内
        $('#txtTaskName').val(task.TaskCode);
        $('#txtTaskCode').val(task.TaskName);
        //$('#txtUnit').val(task.Unit);
        //$('#txtUnitPrice').val(task.UnitPrice);
        //$('#txtTotal').val(task.Total2);
        //$('#txtStartDate').val(jw.jsonToDate(task.StartDate));
        //$('#txtEndDate').val(jw.jsonToDate(task.EndDate));
        //$('#txtConstructPeriod').val(task.ConstructionPeriod);
    }
}

// 验证开始时间和结束时间并设置工期
function validateDate() {
    var startDateArr = $('#txtStartDate').val().split('-');
    var startDate = new Date(startDateArr[0], startDateArr[1] - 1, startDateArr[2]);
    var endDateArr = $('#txtEndDate').val().split('-');
    var endDate = new Date(endDateArr[0], endDateArr[1] - 1, endDateArr[2]);
    if (endDate - startDate < 0) {
        top.ui.alert('结束时间不能早于开始时间');
        $('#txtEndDate').val('')
    }

    // 计算工期
    if (startDate && endDate) {
        var sd = new Date(startDate).getTime();
        var ed = new Date(endDate).getTime();
        var cp = (ed - sd) / (1000 * 3600 * 24) + 1;

        if (!isNaN(cp) && cp > 0)
            $('#txtConstructPeriod').val(cp);
    }
}


// 计算合计
function calcTotal() {
    var qty = parseFloat($('#txtQuantity').val().replace(/\,/g, ''));
    var uprice = parseFloat($('#txtUnitPrice').val().replace(/\,/g, ''));
    if (isNaN(qty) || isNaN(uprice)) return false;

    var total = qty * uprice;
    $('#txtTotal').val(total);
}

