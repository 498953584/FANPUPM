//是否显示节点下的资源信息
var displayResource = undefined;
//是否是预算变更
var budgetChange = false;
//页面地址
var pageURL = undefined;
//上移、下移借助下列图标进行判断
var $imgLminus = $('<img style="vertical-align:middle; "; src="/images/tree/lminus.gif" />');
var $imgTminus = $('<img style="vertical-align:middle; "; src="/images/tree/tminus.gif" />');
var $imgWhite = $('<img style="vertical-align:middle;"; src="/images/tree/white.gif" />');
var $imgI = $('<img style="vertical-align:middle;"; src="/images/tree/i.gif" />');
var $imgT = $('<img style="vertical-align:middle;"; src="/images/tree/t.gif" />');
var $imgL = $('<img style="vertical-align:middle;"; src="/images/tree/l.gif" />');
var $imgLplus = $('<img style="vertical-align:middle;"; src="/images/tree/lplus.gif" />');
var $imgTplus = $('<img style="vertical-align:middle;"; src="/images/tree/tplus.gif" />');
$(document).ready(function () {
    showMoreNote();
    var endIndex = window.location.href.indexOf('?');
    if (endIndex == -1) {
        endIndex = window.location.href.length;
    }
    pageURL = window.location.href.substring(window.location.href.lastIndexOf('/') + 1, endIndex);
    if (pageURL == 'BudgetChange.aspx') {
        budgetChange = true;
    }

    //单击行
    $('#gvBudget tr').live('click', function () {
        if (this.id == '') return;
        if (!$(this).attr('subCount')) {
            $('#' + hfldCheckedIds).val('');
            setNoSelected();
            return;
        }
        setSingle(this.id);
        upAdminPrivilege();
        var userCode = getCookie('UserCode');
        if (userCode == '00000000') {
            if (typeof hfldLock != 'undefined') {
                if ($('#' + hfldLock).val() == 'True') {
                    $('#' + btnEdit).attr('disabled', 'disabled');
                    $('#' + btnAdd).attr('disabled', 'disabled');
                }
            }
        }
    });
});
//设置隐藏控件
//user的值必须是编码后的值
//lock 隐藏控件值指定项目是否锁定 true表示锁定false表示未锁定
function setHiddenId(version, prjId, user, lock, editOrAdd) {
    hfldVersion = version == undefined ? undefined : version;
    hfldPrjId = prjId == undefined ? undefined : prjId;
    hfldUser = user == undefined ? undefined : user;
    hfldLock = lock == undefined ? undefined : lock;
    hfldEditOrAdd = editOrAdd == undefined ? undefined : editOrAdd;
}
//设置复选框控制的按钮
//必须存在checkedIds 即复选框选中节点Id保存到此隐藏控件中
//点击行是否显示资源信息
function setControlButton(checkedIds, add, edit, del, importExcel, importTemplate, saveAsTemplate, deployResource, isDisplayResource, btnUP, btnDown) {
    hfldCheckedIds = checkedIds == undefined ? undefined : checkedIds;
    btnAdd = add == undefined ? undefined : add;
    btnEdit = edit == undefined ? undefined : edit;
    //绑定新增和编辑按钮事件
    if (btnAdd != undefined && btnAdd != '' && btnEdit != '' && btnEdit != undefined) {
        $('#' + btnAdd).bind('click', openEdit_Add);
        $('#' + btnEdit).bind('click', openEdit_Add);
    }
    btnDel = del == undefined ? undefined : del;
    btnImportExcel = importExcel == undefined ? undefined : importExcel;
    btnImportTemplate = importTemplate == undefined ? undefined : importTemplate;
    btnSaveAsTemplate = saveAsTemplate == undefined ? undefined : saveAsTemplate;
    btnDeployResource = deployResource == undefined ? undefined : deployResource;
    displayResource = isDisplayResource == undefined ? undefined : isDisplayResource;
    btnMoveUp = btnUP == undefined ? undefined : btnUP;
    btnMoveDown = btnDown == undefined ? undefined : btnDown;
    //绑定上移下移事件
    if (btnMoveUp != undefined && btnMoveUp != '' && btnMoveDown != '' && btnMoveDown != undefined) {
        bindMoveUp_Down();
    }
}
//绑定页面编辑和新增页面按钮事件
function openEdit_Add() {
    var type = 'edit';
    var dialogTitle = '编辑';
    if (this.id.indexOf(btnAdd) != -1) {
        type = 'add';
        dialogTitle = '新增';
    }
    $('#hfldEditOrAdd').val(type);
    var id = $('#hfldCheckedIds').val();
    if ($('#hfldIsWBSRelevance').val() == '1') {
        if (type == "add") {
            if (isHaveResource(id) == 1) {
                alert("系统提示：\n此节点已经配置资源，无法创建子节点！");
                return;
            }
        }
    }
    var layer = '';
    if (id != '') {
        if (type == 'edit')
            layer = $('#' + id).attr('layer');
        else
            layer = getTypeId($('#' + id).attr('layer'));
    }
    var parameters = '&type=' + type + '&layer=' + layer + '&id=' + id + '&page=' + pageURL;
    if (pageURL == 'BudgetChange.aspx' || pageURL == 'BudgetPlaitList.aspx') {
        var prjId = $('#hfldPrjId').val();
        var version = $('#hfldCurrentVersion').val();
        parameters += '&prjId=' + prjId + '&version=' + version;
        dialogTitle += '任务节点';
    }
    else if (pageURL == 'BudTemplateList.aspx') {
        var tempType = $('#hfldTempId').val();
        parameters += '&tempType=' + tempType;
        dialogTitle += '模板节点';
    }
    top.ui._BudTaskEdit = window;
    var url = '/BudgetManage/Budget/BudTaskEdit.aspx?' + new Date().getTime() + parameters;
    top.ui.callback = function (code, name, unit, quantity, note, taskTypeName, startDate, endDate, unitPrice, total, constructionPeriod, bid, description) {
        sendTaskData(code, name, unit, quantity, note, taskTypeName, startDate, endDate, unitPrice, total, constructionPeriod, bid, description);
    };
    $('#editTaskFrame').attr('src', '/BudgetManage/Budget/BudTaskEdit.aspx?' + new Date().getTime() + parameters);
    if ($('#hfldIsWBSRelevance').val() == '1') {
        //如果WBS与资源挂钩，弹出层的高度为400
        top.ui.openWin({ title: dialogTitle, url: url, width: 650, height: 400 });
    } else {
        //如果WBS与资源不挂钩，弹出层的高度为440
        top.ui.openWin({ title: dialogTitle, url: url, width: 650, height: 440 });
    }
}
//保存按钮传送数据进行修改或新增
function sendTaskData(code, name, unit, quantity, note, taskTypeName, startDate, endDate, unitPrice, total, constructionPeriod, bid, description) {
    var prjId = $('#' + hfldPrjId).val();
    var type = $('#' + hfldEditOrAdd).val();
    var checkTaskId = $('#' + hfldCheckedIds).val();
    var pid = checkTaskId;
    var parameters = '';
    var handlerURL = '/BudgetManage/Handler/TempletePlain.ashx?';
    if (typeof hfldVersion != 'undefined') {
        handlerURL = '/BudgetManage/Handler/BudgetPlain.ashx?';
        var version = $('#' + hfldVersion).val();
        var user = $('#' + hfldUser).val();
        parameters = '&user=' + user + '&version=' + version + '&flag=noExecu'
        if (unitPrice != '') {
            parameters += '&unitPrice=' + unitPrice
        }
        if (total != '') {
            parameters += '&total=' + total
        }
        if (startDate != '') {
            parameters += '&start=' + startDate
        }
        if (endDate != '') {
            parameters += '&end=' + endDate
        }
        if (constructionPeriod != '') {
            parameters += '&constructionPeriod=' + constructionPeriod
        }
        if (bid != '') {
            //要增加的Id
            parameters += '&bid=' + bid
        }
        if (description != '') {
            parameters += '&description=' + description
        }
    }
    $.ajax({
        type: 'GET',
        async: false,
        url: handlerURL + new Date().getTime() + '&id=' + checkTaskId + '&type=' + type + '&budgetChange=' + budgetChange
                + '&code=' + escape(code)
                + '&name=' + escape(name)
                + '&unit=' + escape(unit)
                + '&quantity=' + quantity
                + '&prjId=' + prjId
                + '&note=' + escape(note)
                + '&description=' + escape(description)
                + parameters
                + '&pid=' + pid,
        success: function (data) {
            var tabRow;
            if (data == "1") {//编辑
                tabRow = $('#' + $('#' + hfldCheckedIds).val()).get(0);
                if ($('#hfldIsWBSRelevance').val() == '0' && typeof hfldVersion != 'undefined') {
                    tabRow.cells[tabRow.cells.length - 2].innerText = toFixed(parseFloat(total), 2);
                    tabRow.cells[tabRow.cells.length - 3].innerText = toFixed(parseFloat(unitPrice), 2);
                } else {
                    if (quantity > 0)
                        tabRow.cells[tabRow.cells.length - 3].innerText = toFixed(parseFloat(tabRow.cells[tabRow.cells.length - 2].innerText) / toFixed(quantity, 2), 3);
                    else
                        tabRow.cells[tabRow.cells.length - 3].innerText = '0.000';
                }
            }
            //新增
            else {
                var $tabRow = addRowData(checkTaskId);
                if ($tabRow != undefined) {
                    tabRow = $tabRow.get(0);
                }
                else {
                    return;
                }
                tabRow.id = data.Id;
                tabRow.orderNumber = data.OrderNumber;
                tabRow.subCount = 0;
                tabRow.cells[tabRow.cells.length - 1].firstChild.id = tabRow.id + '00';
                tabRow.cells[4].innerText = taskTypeName;
                if ($('#hfldIsWBSRelevance').val() == '0' && typeof hfldVersion != 'undefined') {
                    tabRow.cells[tabRow.cells.length - 2].innerText = toFixed(parseFloat(total), 3);
                    tabRow.cells[tabRow.cells.length - 3].innerText = toFixed(parseFloat(unitPrice), 3);
                } else {
                    tabRow.cells[tabRow.cells.length - 2].innerText = '0.000';
                    tabRow.cells[tabRow.cells.length - 3].innerText = '0.000';
                }
                //更新序号
                setNumber(tabRow);
                //更新复选框的Title
                tabRow.cells[0].childNodes[0].title = data.Id;
            }
            //更新内容
            if (tabRow.cells[2].lastChild.nodeValue != null)
                tabRow.cells[2].lastChild.nodeValue = name;
            else
                tabRow.cells[2].lastChild.innerText = name;
            tabRow.cells[3].innerText = code;
            tabRow.cells[5].innerText = unit;
            tabRow.cells[6].innerText = toFixed(parseFloat(quantity), 3);

            if (typeof hfldVersion != 'undefined') {
                tabRow.cells[7].innerText = formatDate(startDate);
                tabRow.cells[8].innerText = formatDate(endDate);
                tabRow.cells[9].innerText = constructionPeriod;
            }
            var noteSub = undefined;
            if (note.length > 15) {
                noteSub = note.substr(0, 15) + '...';
            }
            else {
                noteSub = note;
            }
            tabRow.cells[tabRow.cells.length - 1].firstChild.title = note;
            tabRow.cells[tabRow.cells.length - 1].firstChild.innerHTML = noteSub;

            //注册toolTip
            //showMoreNote();
            //是否是预算变更
            if (budgetChange != undefined && budgetChange != '' && budgetChange == true) {
                $(tabRow).css('color', 'red');
                //$(tabRow.cells[11].firstChild).css('color', 'red');
                $(tabRow.cells[tabRow.cells.length - 1].firstChild).css('color', 'red');
            }
        }
    });
}
//新增任务节点
function addRowData(parentId) {
    var $newRow;
    //无父节点
    if (parentId == '' || parentId == undefined) {
        if ($('#gvBudget').get(0) != undefined) {
            var $lastUnitProject;
            for (k = $('#gvBudget').get(0).rows.length - 1; k > 0; k--) {
                if ($('#gvBudget tr:eq(' + k + ')').eq(0).attr('layer') == 1) {
                    $lastUnitProject = $('#gvBudget tr:eq(' + k + ')');
                    break;
                }
            }
            //设置最后一个单位工程图标
            $lastUnitProjectImg = $($lastUnitProject.get(0).cells[2].children[0]);
            $newRow = $lastUnitProject.clone();
            $($.getChildren($lastUnitProject.get(0))).hide();
            if ($lastUnitProjectImg.get(0).src == $imgL.get(0).src) {
                $lastUnitProjectImg.replaceWith($imgT.clone());
            } else {
                $lastUnitProjectImg.replaceWith($imgTplus.clone());
                $($newRow.get(0).cells[2].children[0]).replaceWith($imgL.clone());
            }
            //此时设置新增节点的序号
            var prjId = $('#' + hfldPrjId).val();
            var getURL = '/BudgetManage/Handler/TempletePlain.ashx?' + new Date().getTime() + '&prjId=' + prjId + '&type=count';
            if (typeof hfldVersion != 'undefined') {
                var version = $('#' + hfldVersion).val();
                getURL = '/BudgetManage/Handler/BudgetPlain.ashx?' + new Date().getTime() + '&prjId=' + prjId + '&type=count&version=' + version;
            }
            $.ajax({
                type: 'GET',
                async: false,
                url: getURL,
                success: function (data) {
                    $newRow.get(0).cells[1].innerText = parseInt(data) - 1;
                }
            });
            $('#gvBudget tr:last').after($newRow);
        }
        //不存在项目节点
        else {
            if (pageURL == 'BudTemplateList.aspx')
                window.location.href = window.location.href.substring(0, window.location.href.indexOf('?', 0)) + '?tempId=' + $('#hfldTempId').val() + '&tempType=' + $('#hfldTemTypeId').val();
            else if (pageURL == 'BudgetChange.aspx' || pageURL == 'BudgetPlaitList.aspx')
                window.location.href = window.location.href.substring(0, window.location.href.indexOf('?', 0)) + '?prjId=' + $('#hfldPrjId').val() + '&year=' + $('#ddlYear').val();
        }
    }
    //有父节点
    else {
        return;
        var parentRow = $('#' + parentId).get(0);
        //父节点无子节点
        if (parentRow.subCount == 0) {
            $newRow = $(parentRow).clone();
            $newRow.get(0).layer = parseInt(parentRow.layer) + 1;
            $newRow.get(0).orderNumber += '001';
            var lastImageIndex = parseInt(parentRow.layer) - 1;
            var $lastImage = $($newRow.get(0).cells[2].children[lastImageIndex]);
            //更新父节点图标
            var $parentImg;
            if ($lastImage.get(0).src != $imgL.get(0).src) {
                $parentImg = $imgTminus.clone();
                //更新子节点图标
                $lastImage.before($imgI.clone());
            } else {
                $parentImg = $imgLminus.clone();
                $lastImage.before($imgWhite.clone());
            }
            $(parentRow.cells[2].children[lastImageIndex]).replaceWith($parentImg);
            $lastImage.replaceWith($imgL.clone());
            //添加新增节点
            $(parentRow).after($newRow);
        }
        else {//父节点有子节点
            var lastChildRow = getLastTask(parentRow);
            if (lastChildRow == undefined) {
                parentRow.subCount++;
                setNumber(parentRow, false);
                return;
            }
            var $img;
            var lastImagIndex = parseInt(lastChildRow.layer) - 1;
            //最后一个子节点无子节点
            if (lastChildRow.subCount == 0) {
                $img = $imgT.clone();
                $newRow = $(lastChildRow).clone();
            } else {//最后一个子节点有子节点
                $img = $imgTplus.clone();
                $newRow = $(lastChildRow).clone();
                $($.getChildren(lastChildRow)).hide();
                $($newRow.get(0).cells[2].children[lastImagIndex]).replaceWith($imgL.clone());
            }
            //更新最后子节点图标
            $(lastChildRow.cells[2].children[lastImagIndex]).replaceWith($img);
            //添加新增节点
            $(lastChildRow).after($newRow);
        }
        parentRow.subCount++;
    }
    registerImg();
    return $newRow;
}
//父节点下的最后一个子节点
function getLastTask(parentRow) {
    var $lplus = $(parentRow).find('img[src*="lplus.gif"]');
    var $tplus = $(parentRow).find('img[src*="tplus.gif"]');
    if ($lplus.get(0) != undefined) {
        $lplus.click();
    }
    else if ($tplus.get(0) != undefined) {
        $tplus.click();
    }
    var pIndex = parseInt(parentRow.rowIndex);
    var subCount = parentRow.subCount.toString();
    if (subCount.length == 1)
        subCount = '00' + subCount;
    else if (subCount.length == 2)
        subCount = '0' + subCount;

    for (i = pIndex + 1; i < $('#gvBudget').get(0).rows.length; i++) {
        var $tr = $('#gvBudget tr:eq(' + i + ')');
        if ($tr.get(0).orderNumber == parentRow.orderNumber + subCount) {
            return $tr.get(0);
        }
    }
}


//更新序号
function setNumber(row) {
    //限制父节点时不进行修改序号
    if (arguments.length == 1) {
        if ($('#gvBudget tr').get(parseInt(row.rowIndex) + 1) != undefined)
            row.cells[1].innerText = $('#gvBudget tr').get(parseInt(row.rowIndex) + 1).cells[1].innerText;
        else
            row.cells[1].innerText = parseInt(row.cells[1].innerText) + 1;
    }
    $('#gvBudget tr:gt(' + row.rowIndex + ')').each(function () {
        this.cells[1].innerText = parseInt(this.cells[1].innerText) + 1;
    });
}
// 格式化时间yyyy-MM-dd
function formatDate(date) {
    if (date != '') {
        var array = date.split('-');
        if (array[1].length == 1) {
            array[1] = '0' + array[1];
        }
        if (array[2].length == 1) {
            array[2] = '0' + array[2];
        }
        return array.join('-');
    } else {
        return '';
    }
}




//单击复选框
function checkSingle() {
    //选中复选框的节点Id
    var checkedIds = getCheckedIds('gvBudget');
    //选中的个数
    var checkedCount = getCheckedCount(checkedIds);
    //    //是否是锁定项目  
    if (checkedCount == 0) {
        setNoSelected();
    }
    else if (checkedCount == 1) {
        setSingle(checkedIds);
        upAdminPrivilege();
        var userCode = getCookie('UserCode');
        if (userCode == '00000000') {
            if (typeof hfldLock != 'undefined') {
                if ($('#' + hfldLock).val() == 'True') {
                    $('#' + btnEdit).attr('disabled', 'disabled');
                    $('#' + btnAdd).attr('disabled', 'disabled');
                }
            }
        }
    }
    else {//多选择
        setMultiple(checkedIds);
    }
}
//全选复选框
function checkAll() {
    var checkedIds = getCheckedIds('gvBudget');
    var checkedCount = getCheckedCount(checkedIds);
    if (checkedCount == 0) {
        setNoSelected();
    }
    else if (checkedCount == 1) {
        setSingle(checkedIds);
        upAdminPrivilege();
        var userCode = getCookie('UserCode');
        if (userCode == '00000000') {
            if ($('#' + hfldLock).val() == 'True') {
                $('#' + btnEdit).attr('disabled', 'disabled');
                $('#' + btnAdd).attr('disabled', 'disabled');
            }
        }
    }
    else {
        setMultiple(checkedIds);
    }
}
//复选框选中值
function getCheckedIds(tableId) {
    var chkids = new Array();
    var trs = $('#' + tableId).get(0).getElementsByTagName('TR');
    for (var i = 1; i < trs.length; i++) {
        if ($(trs[i]).find("input").get(0) == undefined)
            continue;
        var chk = $(trs[i]).find("input")[0].checked;
        if (chk) {
            chkids.push($(trs[i]).get(0).id);
        }
    }
    var str = '["';
    if (chkids.length == 1) {
        return chkids[0];
    }
    else if (chkids.length == 0) {
        return '';
    }
    for (var i = 0; i < chkids.length; i++) {
        str += chkids[i] + '","';
    }
    return str.substring(0, str.length - 2) + ']';
}
//选中复选框个数
function getCheckedCount(checkedIds) {
    var checkedCount = 0;
    if (checkedIds == '')
        checkedCount = 0;
    else if (checkedIds.indexOf('[', 0) > -1)
        checkedCount = 2;
    else
        checkedCount = 1;
    return checkedCount;
}
//禁用按钮
function disabledButton() {
    if (typeof btnDel != 'undefined' && btnDel != '')
        $('#' + btnDel).attr('disabled', 'disabled');
    if (typeof btnSaveAsTemplate != 'undefined' && btnSaveAsTemplate != '')
        $('#' + btnSaveAsTemplate).attr('disabled', 'disabled');
    if (typeof btnEdit != 'undefined' && btnEdit != '')
        $('#' + btnEdit).attr('disabled', 'disabled');
    //资源配置
    if (typeof btnDeployResource != 'undefined' && btnDeployResource != '')
        $('#' + btnDeployResource).attr('disabled', 'disabled');
    //禁用上移、下移按钮
    if (typeof btnMoveUp != 'undefined' && btnMoveUp != '' && typeof btnMoveDown != 'undefined' && btnMoveDown != '') {
        $('#' + btnMoveUp).attr('disabled', 'disabled');
        $('#' + btnMoveDown).attr('disabled', 'disabled');
    }
}
//针对预算变更未锁定禁用所有按钮
function disabledLockButton() {
    if (budgetChange != undefined && budgetChange != '' && budgetChange == true && $('#' + hfldLock).val() == 'False') {
        $('input[type="button"]').each(function () {
            $(this).attr('disabled', 'disabled');
        });
        $('input[type="submit"]').each(function () {
            $(this).attr('disabled', 'disabled');
        });
    } else if (budgetChange != undefined && budgetChange != '' && budgetChange == true && $('#' + hfldLock).val() == 'True'
               && ($('#hfldFlowstate').val() == '1' || $('#hfldFlowstate').val() == '-2')) {
        $('input[type="button"]').each(function () {
            $(this).attr('disabled', 'disabled');
        });
        $('input[type="submit"]').each(function () {
            $(this).attr('disabled', 'disabled');
        });
        $('#btnChangeApply').removeAttr('disabled');
        $('#btnShowRes').removeAttr('disabled');
        $('#WF1_btnViewWF').removeAttr('disabled');
        $('#WF1_btnWFPrint').removeAttr('disabled');
        $('#WF1_BtnWFDel').removeAttr('disabled');
    }
    else if (budgetChange != undefined && budgetChange != '' && budgetChange == true && $('#' + hfldLock).val() == 'True'
             && $('#hfldFlowstate').val() == '0') {
        $('input[type="button"]').each(function () {
            $(this).attr('disabled', 'disabled');
        });
        $('input[type="submit"]').each(function () {
            $(this).attr('disabled', 'disabled');
        });
        $('#CancelBt').removeAttr('disabled');
        $('#WF1_btnViewWF').removeAttr('disabled');
        $('#WF1_btnWFPrint').removeAttr('disabled');
        $('#WF1_BtnWFDel').removeAttr('disabled');
    }
    $('#btnSaveNewVersion').removeAttr('disabled');
}
//无选择复选框设置
function setNoSelected() {
    disabledButton();
    var isLocked = 'False';
    if (typeof hfldLock != 'undefined')
        isLocked = $('#' + hfldLock).val();
    if (budgetChange != undefined && budgetChange != '' && budgetChange == true && isLocked == 'True')
        isLocked = 'False';
    if (isLocked == 'False') {
        if (typeof btnAdd != 'undefined' && btnAdd != '')
            $('#' + btnAdd).removeAttr('disabled');
        if (typeof btnImportExcel != 'undefined' && btnImportExcel != '')
            $('#' + btnImportExcel).removeAttr('disabled');
        if (typeof btnImportTemplate != 'undefined' && btnImportTemplate != '')
            $('#' + btnImportTemplate).removeAttr('disabled');
    }
    disabledLockButton();
    $('#' + hfldCheckedIds).val('');
}
//复选框单选设置
function setSingle(checkedIds) {
    if (typeof btnSaveAsTemplate != 'undefined' && btnSaveAsTemplate != '')
        $('#' + btnSaveAsTemplate).removeAttr('disabled');
    var isLocked = 'False';
    if (typeof hfldLock != 'undefined')
        isLocked = $('#' + hfldLock).val();
    if (budgetChange != undefined && budgetChange != '' && budgetChange == true && isLocked == 'True')
        isLocked = 'False';
    if (isLocked == 'False') {//未锁定激活按钮
        if (typeof btnDel != 'undefined' && btnDel != '')
            $('#' + btnDel).removeAttr('disabled');
        if (typeof btnEdit != 'undefined' && btnEdit != '')
            $('#' + btnEdit).removeAttr('disabled');
        if (typeof btnImportTemplate != 'undefined' && btnImportTemplate != '')
            $('#' + btnImportTemplate).removeAttr('disabled');
        if (typeof btnImportExcel != 'undefined' && btnImportExcel != '')
            $('#' + btnImportExcel).removeAttr('disabled');
        if (typeof btnAdd != 'undefined' && btnAdd != '')
            $('#' + btnAdd).removeAttr('disabled');
        //资源配置
        if (typeof btnDeployResource != 'undefined' && btnDeployResource != '') {
            //var subCount = $('#' + checkedIds).get(0).subCount;
            var subCount = $('#' + checkedIds).attr('subCount');
            if (subCount == '0') {
                $('#' + btnDeployResource).removeAttr('disabled');
            }
            else {
                $('#' + btnDeployResource).attr('disabled', 'disabled');
            }
        }
        //上移下移
        if (typeof btnMoveUp != 'undefined' && btnMoveUp != '' && typeof btnMoveDown != 'undefined' && btnMoveDown != '') {
            checkMove(checkedIds);
        }


    }
    getIsHaveResource(checkedIds);
    disabledLockButton();
    $('#' + hfldCheckedIds).val(checkedIds);
}
//上移和下移的判断
//全部变量上移和下移行
//上移行在进行上移按钮的判断时，已经赋值，而下移 则需要重新调用方法
var prevTr = undefined;
var nextTr = undefined;
function checkMove(id) {
    var trLen = $('#' + id).get(0).cells[2].childNodes.length;
    var trNo = Trim($('#' + id).get(0).cells[1].innerText);
    var trImg = $('#' + id).get(0).cells[2].childNodes[trLen - 2];
    if (trImg.src == undefined) {
        trImg = $('#' + id).get(0).cells[2].childNodes[trLen - 4];
    }
    //全部不可用
    $('#' + btnMoveUp).attr('disabled', 'disabled');
    $('#' + btnMoveDown).attr('disabled', 'disabled');
    //  上移
    if (trImg.src == $imgL.get(0).src || trImg.src == $imgT[0].src || (trNo != "1" && (trImg.src == $imgLminus[0].src || trImg.src == $imgLplus[0].src || trImg.src == $imgTminus[0].src || trImg.src == $imgTplus[0].src))) {
        //选中行的orderNumber
        var selectTrOrderNumber = $('#' + id).attr('orderNumber');
        prevTr = getPrevTr(selectTrOrderNumber, id);
        if (prevTr != undefined && prevTr != '')
            $('#' + btnMoveUp).removeAttr('disabled');
    }
    //  下移
    if (trImg.src == $imgTminus[0].src || trImg.src == $imgTplus[0].src || trImg.src == $imgT[0].src) {
        $('#' + btnMoveDown).removeAttr('disabled');
    }
    //上移下移
    if (trImg.src == $imgI[0].src) {
        $('#' + btnMoveUp).removeAttr('disabled');
        $('#' + btnMoveDown).removeAttr('disabled');
    }
}
//返回选中行的同级别的行，如果不存在返回null
//compareOrderNumber 选中行的的
//startId 进行向前查询行的起止行Id
function getPrevTr(compareOrderNumber, startId) {
    var prevTr = $('#' + startId).prev().get(0);
    //var prevTrOrderNumber = prevTr.orderNumber;
    var prevTrOrderNumber = $(prevTr).attr('orderNumber')
    if (prevTrOrderNumber != undefined) {
        if (compareOrderNumber.length == 3) {
            if (compareOrderNumber.length == prevTrOrderNumber.length)
                return prevTr;
            else
                return arguments.callee(compareOrderNumber, prevTr.id);
        }
        else if (prevTrOrderNumber.length == compareOrderNumber.toString().length && prevTrOrderNumber.substr(0, prevTrOrderNumber.length - 3) == compareOrderNumber.toString().substr(0, compareOrderNumber.toString().length - 3)) {
            return prevTr;
        }
        else if (prevTrOrderNumber.length + 3 == compareOrderNumber.toString().length && prevTrOrderNumber == compareOrderNumber.toString().substr(0, compareOrderNumber.toString().length - 3)) {
            return null;
        }
        else {
            return arguments.callee(compareOrderNumber, prevTr.id);
        }
    }
}
//返回选中行的同级别的Tr，如果不存在返回null
//compareOrderNumber 选中行的的
//startId 进行向前查询行的起止行Id
function getNextTr(compareOrderNumber, startId) {
    var nextTr = $('#' + startId).next().get(0);
    var nextTrOrderNumber = $(nextTr).attr('orderNumber');
    if (nextTrOrderNumber != undefined) {
        if (compareOrderNumber.length == 3) {
            if (nextTrOrderNumber.length == compareOrderNumber.length)
                return nextTr;
            else
                return arguments.callee(compareOrderNumber, nextTr.id);
        }
        else if (compareOrderNumber.length == nextTrOrderNumber.length && compareOrderNumber.toString().substr(0, compareOrderNumber.length - 3) == nextTrOrderNumber.substr(0, nextTrOrderNumber.length - 3)) {
            return nextTr;
        }
        else {
            return arguments.callee(compareOrderNumber, nextTr.id);
        }
    }
}
//绑定 上移下移点击事件
function bindMoveUp_Down() {
    if (btnMoveUp != undefined && btnMoveUp != '' && btnMoveDown != undefined && btnMoveDown != '') {
		// 上移
        $('#' + btnMoveUp).bind('click', function () {
            //此两个变量不能放到函数外部，因为选中行的Id是不确定的
            var selectTrId = $('#' + hfldCheckedIds).val();
            var selectedTr = $('#' + selectTrId).get(0);
            setImg(prevTr, selectedTr);
            $('#' + selectTrId).replaceWith($(prevTr).clone());
            $(prevTr).replaceWith($(selectedTr).clone());
            //更新移动后的排序号
            setChangTrNo($('#' + prevTr.id).get(0), $('#' + selectTrId).get(0), 'up');
            registerImg();
            $('#' + selectTrId).click();
        });

        // 下移
        $('#' + btnMoveDown).bind('click', function () {
            var selectTrId = $('#' + hfldCheckedIds).val();
            var selectedTr = $('#' + selectTrId).get(0);
            var checkedTr = $('#' + $('#' + hfldCheckedIds).val()).get(0);
            var nextTr = getNextTr($(checkedTr).attr('orderNumber'), $('#' + $('#' + hfldCheckedIds).val()).get(0).id);
            setImg(nextTr, selectedTr);
            $(nextTr).replaceWith($(selectedTr).clone());
            $('#' + selectTrId).replaceWith($(nextTr).clone());
            //更新移动行的排序号
            setChangTrNo($('#' + nextTr.id).get(0), $('#' + selectTrId).get(0), 'down');
            registerImg();
            $('#' + selectTrId).click();

        });
    }
}
//设置节点移动后的Img
//compareTr上移行，下移行
//selectedTr选中行
function setImg(compareTr, selectedTr) {
    var comImg = compareTr.cells[2].childNodes[compareTr.cells[2].childNodes.length - 2];
	
    if (comImg.src == undefined) {
        comImg = compareTr.cells[2].childNodes[compareTr.cells[2].childNodes.length - 4];
    }
    var selImg = selectedTr.cells[2].childNodes[selectedTr.cells[2].childNodes.length - 2];
    if (selImg.src == undefined) {
        selImg = selectedTr.cells[2].childNodes[selectedTr.cells[2].childNodes.length - 4];
    }

    // 关闭已展开
    if (comImg.src == $imgLminus[0].src || comImg.src == $imgTminus[0].src || comImg.src == $imgLplus[0].src || comImg.src == $imgTplus[0].src)
        $($.getChildren(compareTr)).remove();
    if (selImg.src == $imgLminus[0].src || selImg.src == $imgTminus[0].src || selImg.src == $imgLplus[0].src || selImg.src == $imgTplus[0].src)
        $($.getChildren(selectedTr)).remove();

    // 设置图标,可以向上向下延伸的
    if ((comImg.src == $imgT[0].src || comImg.src == $imgTminus[0].src || comImg.src == $imgTplus[0].src) && (selImg.src == $imgT[0].src || selImg.src == $imgTminus[0].src || selImg.src == $imgTplus[0].src)) {
        if (comImg.src == $imgTminus[0].src)
            $(comImg).replaceWith($imgTplus.clone());
        if (selImg.src == $imgTminus[0].src)
            $(selImg).replaceWith($imgTplus.clone());
    }
    else {
        $(selImg).replaceWith(getOppositeImg(selImg));
        $(comImg).replaceWith(getOppositeImg(comImg));
    }
}

// 得到对应的节点图标
function getOppositeImg(argImg) {
    if (argImg.src == $imgT[0].src)
        return $imgL;
    else if (argImg.src == $imgL[0].src) {
        return $imgT;
    }
    else if (argImg.src == $imgLminus[0].src || argImg.src == $imgLplus[0].src)
        return $imgTplus;
    else if (argImg.src == $imgTminus[0].src || argImg.src == $imgTplus[0].src)
        return $imgLplus;
}

// 获取移动后新的排序号
// compareId  上移行，下移行
// selectedId 选中行
// moveup_down 上移up 下移down
function setChangTrNo(compareTr, selectedTr, moveup_down) {
    var URL = '/BudgetManage/Handler/TaskMove.ashx?' + new Date().getTime();
    var parameters = '&comId=' + compareTr.id + '&selId=' + selectedTr.id + '&comOrderNumber=' + $(compareTr).attr('orderNumber') + '&selOrderNumber=' + $(selectedTr).attr('orderNumber');
    if (pageURL == 'BudgetChange.aspx' || pageURL == 'BudgetPlaitList.aspx') {
        var prjId = $('#' + hfldPrjId).val();
        parameters += '&prjId=' + prjId + '&version=1&type=budget';
    }
    else if (pageURL == 'BudTemplateList.aspx') {
        var tempType = $('#hfldTempId').val();
        parameters += '&tempType=' + tempType + '&type=templete';
    }
    //请求获取移动后的新的排序号
    $.ajax({
        type: 'GET',
        async: false,
        url: URL + parameters,
        success: function (data) {
            if (data.comId != '0000') {
                selectedTr.cells[1].innerText = moveup_down == 'down' ? data.selId : data.comId;
                compareTr.cells[1].innerText = moveup_down == 'down' ? data.comId : data.selId;
                var selOrderNumber = selectedTr.orderNumber;
                selectedTr.orderNumber = compareTr.orderNumber;
                compareTr.orderNumber = selOrderNumber;
            }
            else {
                alert("系统提示：\n进行移动的节点已不存在，请刷新！");
            }
        }
    });
}
//复选框多选设置
function setMultiple(checkedIds) {
    var isLocked = 'False';
    var userCode = getCookie('UserCode');
    if (typeof hfldLock != 'undefined')
        isLocked = $('#' + hfldLock).val();
    if (budgetChange != undefined && budgetChange != '' && budgetChange == true && isLocked == 'True')
        isLocked = 'False';

    if ((isLocked == 'False' || isLocked == 'True') && userCode == '00000000') {
        if (btnDel != undefined && btnDel != '')
            $('#' + btnDel).removeAttr('disabled');
    }
    if (typeof btnSaveAsTemplate != 'undefined' && btnSaveAsTemplate != '')
        $('#' + btnSaveAsTemplate).removeAttr('disabled');
    if (typeof btnEdit != undefined && btnEdit != '')
        $('#' + btnEdit).attr('disabled', 'disabled');
    if (typeof btnImportTemplate != 'undefined' && btnImportTemplate != '')
        $('#' + btnImportTemplate).attr('disabled', 'disabled');
    if (typeof btnAdd != 'undefined' && btnAdd != '')
        $('#' + btnAdd).attr('disabled', 'disabled');
    if (typeof btnImportExcel != 'undefined' && btnImportExcel != '')
        $('#' + btnImportExcel).attr('disabled', 'disabled');
    //资源配置
    if (typeof btnDeployResource != 'undefined' && btnDeployResource != '') {
        $('#' + btnDeployResource).attr('disabled', 'disabled');
    }
    disabledLockButton();
    //多选禁用上移、下移按钮
    if (typeof btnMoveUp != 'undefined' && btnMoveUp != '' && typeof btnMoveDown != 'undefined' && btnMoveDown != '') {
        $('#' + btnMoveUp).attr('disabled', 'disabled');
        $('#' + btnMoveDown).attr('disabled', 'disabled');
    }
    $('#' + hfldCheckedIds).val(checkedIds);
}

//获取任务类型的下级类型
function getTypeId(layer) {
    var typeId = undefined;
    $.ajax({
        type: 'GET',
        async: false,
        url: '/BudgetManage/Handler/GetNextTaskType.ashx?' + new Date().getTime() + '&layer=' + layer,
        success: function (data) {
            typeId = data;
        }
    });
    return typeId;
}

//异步请求
function getIsHaveResource(taskId) {
    var isHanveResource = null;
    $.ajax({
        type: 'GET',
        async: false,
        url: '/BudgetManage/Handler/IsLeafNode.ashx?' + new Date().getTime() + '&Type=Resource&Id=' + taskId,
        success: function (data) {
            if (data == "0") {
                if ($('#hfldIsWBSRelevance').val() == '1') {
                    if (btnAdd != undefined && btnAdd != '')
                        $('#' + btnAdd).attr('disabled', 'disabled');
                }
                if (btnImportTemplate != undefined && btnImportTemplate != '')
                    $('#' + btnImportTemplate).attr('disabled', 'disabled');
                if (btnImportExcel != undefined && btnImportExcel != '')
                    $('#' + btnImportExcel).attr('disabled', 'disabled');
            }
        }
    });


}
//判断此节点下是否配置过资源
function isHaveResource(taskId) {
    var isHaveResource = 0;
    $.ajax({
        type: 'GET',
        async: false,
        url: '/BudgetManage/Handler/IsLeafNode.ashx?' + new Date().getTime() + '&Type=Resource&Id=' + taskId,
        success: function (data) {
            if (data == 0) {
                isHaveResource = 1;
            } else {
                isHaveResource = 0;
            }
        }
    });
    return isHaveResource;
}

//备注信息提示
function showMoreNote() {
    var tab = document.getElementById('gvBudget');
    if (tab != null) {
        for (i = 1; i < tab.rows.length; i++) {
            var cells = tab.rows[i].cells;
            if (cells.length == 1) return;
            if (cells[cells.length - 1].children.length == 0) return;
            var imgId = cells[cells.length - 1].children[0].id;
            var altLength = document.getElementById(imgId).title.length;
            if (altLength > 15) {
                $('#' + imgId).css('display', '');
                $('#' + imgId).tooltip({
                    track: true,
                    delay: 0,
                    showURL: false,
                    fade: true,
                    showBody: " - ",
                    extraClass: "solid 1px blue",
                    fixPNG: true,
                    left: event.offsetX
                });
            } else {
                document.getElementById(imgId).title = '';
            }
        }
    }
}
//添加行进行显示资源信息
var prevId;
function showInfo(id) {
    var tab_Resource = null;
    $.ajax({
        type: 'GET',
        async: false,
        url: '/BudgetManage/Handler/ReturnResource.ashx?' + new Date().getTime() + '&taskId=' + id + '&type=deploy',
        success: function (data) {
            tab_Resource = data;
        }
    });
    var isDisplay = $('#' + id + '1').get(0);
    if (isDisplay == undefined) {
        $('#' + id).after('<tr id="' + id + '1"><td align="center" colspan="14" style="border:solid 1px #CADEED;">' + tab_Resource + '</td></tr>');
        if (prevId != undefined && prevId != id) {
            $('#' + prevId + '1').remove();
        }
        prevId = id;

    }
    else {
        $('#' + id + '1').remove();
        isDisplay = undefined;
    }
}
//注册图标点击事件
function registerImg() {
    $('#gvBudget tr:gt(0)').each(function (index) {
        var $tr = $(this);
        $tr.find('img[src*="tminus.gif"]').die();
        $tr.find('img[src*="lminus.gif"]').die();
        $tr.find('img[src*="lplus.gif"]').die();
        $tr.find('img[src*="tplus.gif"]').die();
        $tr.find('img[src*="tminus.gif"]').live('click', function (event) {
            $($.getChildren($tr[0])).hide();
            this.src = '/images/tree/tplus.gif';
        });

        $tr.find('img[src*="lminus.gif"]').live('click', function (event) {
            $($.getChildren($tr[0])).hide();
            this.src = '/images/tree/lplus.gif';
        });

        $tr.find('img[src*="lplus.gif"]').live('click', function (event) {
            $.addChildren($tr[0])
            this.src = '/images/tree/lminus.gif';
        });

        $tr.find('img[src*="tplus.gif"]').live('click', function (event) {
            $.addChildren($tr[0]);
            this.src = '/images/tree/tminus.gif';
        });
    });
}

