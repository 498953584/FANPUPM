/*
* wf.js 
* bery 
* 2011-2-14 10:25:59
* 使用此文件必须包含jQuery和JustWinTable.js
*
* state = -1 未提交(“提交审核”可用，其余不可用)
* state = 0 审核中(“提交审核”不可用，其余可用)
* state = 1 已审核(“提交审核”，“撤回”不可用，其余可用)
* state = -2 驳回(“提交审核”，“撤回”不可用，其余可用)
* state = -3 重报状态(“撤回”不可用，其余可用)
*/

//设置工作流按钮的状态
function setWfButtonState2(jwTable, wfId) {
    if (!jwTable.table) return false;
    //table中没有数据则返回
    if (jwTable.table.getElementsByTagName('TR').length == 1) return;

    var stateIndex = getFlowStateIndex(jwTable);
    //点击TR
    jwTable.registClickTrListener(function () {
        $('input[id$=FID]').val(this.guid || $(this).attr('guid'));
        //项目相关
        $('input[id$=PrjGuid]').val(this.prjGuid || $(this).attr('prjGuid'));
        //审核内容
        $('input[id$=hidcontent]').val(this.auditContent || $(this).attr('auditContent'));
        var state = $(this).find('TD:eq(' + stateIndex + ') SPAN').attr('state');
        if (!state) return;
        _setButtonState(state);

    });

    jwTable.registClickSingleCHKListener(function () {
        var tr = getFirstParentElement(this, 'TR');
        $('input[id$=FID]').val(tr.guid);
        //项目相关
        $('input[id$=PrjGuid]').val(tr.prjGuid);
        //审核内容
        $('input[id$=hidcontent]').val(tr.auditContent);
        var state = $(tr).find('TD:eq(' + stateIndex + ') SPAN').attr('state')
        if (!state) return;
        if (this.checked) {
            //选中
            if (jwTable.getCheckedChk().length == 1) {
                _setButtonState(state);
            }
            else {
                var btnStartWf = document.getElementById('btnStartWF'); //提交审核
                var btnCancel = document.getElementById('CancelBt'); //撤回
                var btnViewWF = $('input[id$=btnViewWF]').get(0); //流程状态
                var btnWFPrint = $('input[id$=btnWFPrint]').get(0); //审核记录
                var btnWFDel = $('input[id$=BtnWFDel]').get(0); //超级删除
                btnStartWf.setAttribute('disabled', 'disabled');
                btnCancel.setAttribute('disabled', 'disabled');
                btnViewWF.setAttribute('disabled', 'disabled');
                btnWFPrint.setAttribute('disabled', 'disabled');
                btnWFDel.setAttribute('disabled', 'disabled');
            }
        }
        else {
            //取消
            if (jwTable.getCheckedChk().length == 1) {
                var tr1 = getFirstParentElement(jwTable.getCheckedChk()[0].parentNode, 'TR');
                _setButtonState($(tr1).find('TD:eq(' + stateIndex + ') SPAN').attr('state'));
            }
            else {
                var btnStartWf = document.getElementById('btnStartWF'); //提交审核
                var btnCancel = document.getElementById('CancelBt'); //撤回
                var btnViewWF = $('input[id$=btnViewWF]').get(0); //流程状态
                var btnWFPrint = $('input[id$=btnWFPrint]').get(0); //审核记录
                var btnWFDel = $('input[id$=BtnWFDel]').get(0); //超级删除
                btnStartWf.setAttribute('disabled', 'disabled');
                btnCancel.setAttribute('disabled', 'disabled');
                btnViewWF.setAttribute('disabled', 'disabled');
                btnWFPrint.setAttribute('disabled', 'disabled');
                btnWFDel.setAttribute('disabled', 'disabled');
            }
        }
    });

    //全选时所有按钮都为不可用状态
    jwTable.registClickAllCHKLitener(function () {
        var btnStartWf = document.getElementById('btnStartWF'); //提交审核
        var btnCancel = document.getElementById('CancelBt'); //撤回
        var btnViewWF = $('input[id$=btnViewWF]').get(0); //流程状态
        var btnWFPrint = $('input[id$=btnWFPrint]').get(0); //审核记录
        var btnWFDel = $('input[id$=BtnWFDel]').get(0); //超级删除
        btnStartWf.setAttribute('disabled', 'disabled');
        btnCancel.setAttribute('disabled', 'disabled');
        btnViewWF.setAttribute('disabled', 'disabled');
        btnWFPrint.setAttribute('disabled', 'disabled');
        btnWFDel.setAttribute('disabled', 'disabled');
    });


}

//控制流程按钮可用状态
function _setButtonState(state) {
    var btnStartWf = document.getElementById('btnStartWF'); //提交审核
    var btnCancel = document.getElementById('CancelBt'); //撤回
    var btnViewWF = $('input[id$=btnViewWF]').get(0); //流程状态
    var btnWFPrint = $('input[id$=btnWFPrint]').get(0); //审核记录
    var btnWFDel = $('input[id$=BtnWFDel]').get(0); //超级删除
    if (!btnStartWf || !btnCancel || !btnViewWF || !btnWFPrint || !btnWFDel) {
        return false;
    }
    if (state == '-1') {
        btnStartWf.removeAttribute('disabled');
        btnCancel.setAttribute('disabled', 'disabled');
        btnViewWF.setAttribute('disabled', 'disabled');
        btnWFPrint.setAttribute('disabled', 'disabled');
        btnWFDel.setAttribute('disabled', 'disabled');
    }
    else if (state == '0') {
        btnStartWf.setAttribute('disabled', 'disabled');
        btnCancel.removeAttribute('disabled');
        btnViewWF.removeAttribute('disabled');
        btnWFPrint.removeAttribute('disabled');
        btnWFDel.removeAttribute('disabled');
    }
    else if (state == '1' || state == '-2') {
        btnStartWf.setAttribute('disabled', 'disabled');
        btnCancel.setAttribute('disabled', 'disabled');
        btnViewWF.removeAttribute('disabled');
        btnWFPrint.removeAttribute('disabled');
        btnWFDel.removeAttribute('disabled');
    }
    else if (state == '-3') {
        btnStartWf.removeAttribute('disabled');
        btnCancel.setAttribute('disabled', 'disabled');
        btnViewWF.removeAttribute('disabled');
        btnWFPrint.removeAttribute('disabled');
        btnWFDel.removeAttribute('disabled');
    }
    else if (state == '-5') {
        btnStartWf.setAttribute('disabled', 'disabled');
        btnCancel.setAttribute('disabled', 'disabled');
        btnViewWF.setAttribute('disabled', 'disabled');
        btnWFPrint.setAttribute('disabled', 'disabled');
        btnWFDel.setAttribute('disabled', 'disabled');
    }

    // 下面页面在页面由于审核比较特殊（整个页面作为一条业务数据进行审核），在页面首次加载时不需要提升权限
    if (window.location.href.indexOf('ContractTask.aspx') > -1) return; 		// 合同预算导入
    if (window.location.href.indexOf('BudgetPlaitList.aspx') > -1) return; 		// 目标成本变种

    upAdminPrivilege();     // 提示管理员权限
}
//获取审核流程在Table中列的索引
function getFlowStateIndex(jwTable) {
    var tr = $(jwTable.table).find('tr').get(0);
    var tds = $(tr).find('td');
    if (tds.length == 0)
        tds = $(tr).find('th')
    for (var i = 1; i < tds.length; i++) {
        if ($(tds[i]).html().indexOf('流程状态') > -1)
            return i;
    }
    return 0;
}

//独立的流程审核  2012-4-20    Zhang Fujun
/*
guid：审核项主键
prjGuid：项目相关审核时用到，允许为空
state：审核项当前的流程状态（以此判断流程按钮的可用性）
enable: 是否启用审核控件
*/
function setWFButtonState(guid, prjGuid, state, enable) {
    $('input[id$=FID]').val(guid);
    $('input[id$=PrjGuid]').val(prjGuid); //项目相关
    //流程按钮控制
    if (state == undefined || state.toString() == '') {
        state = -1;
    }
    if (!enable) {
        state = -5;
    }
    _setButtonState(state);
}