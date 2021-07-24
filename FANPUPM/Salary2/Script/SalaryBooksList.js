$(document).ready(function () {
    var jwTable = new JustWinTable('gvSalaryBooks');
    setButton(jwTable, 'btnDelete', 'btnEdit', 'btnQuery', 'hfldCheckedId');
    replaceEmptyTable('emptySalaryBooks', 'gvSalaryBooks');
    $('#btnDelete')[0].onclick = deleteSalaryBooks; 	// 删除
    showTooltip("tooltip", 25);
});
//新增编辑
function edit(type) {
    $('#hfldType').val(type);
    var id = $('#hfldCheckedId').val();
    var titleName = '新增帐套';
    $('#txtSABookName').val('');
    $('#chkIsValid').attr('checked', 'true');
    $('#txtNote').val('');
    if (type == 'edit') {
        titleName = '编辑帐套';
        if (id != '') {
            var data = GetById(id);
            if (data != '') {
                $('#txtSABookName').val(data.Name);
                $('#txtNote').val(data.Note);
                if (data.IsValid == true) {
                    $('#chkIsValid').attr('checked', 'true');
                } else {
                    $('#chkIsValid').removeAttr('checked');
                }
            }
        }
    }

    $('#divSalaryBooksEdit').css('display', 'block').dialog({
        title: titleName,
        width: 500,
        height: 210,
        modal: true,
        buttons: [{
            text: '保存',
            handler: function () {
                saveData();
            }
        }, {
            text: '取消',
            handler: function () {
                $('#divSalaryBooksEdit').dialog('close');
            }
        }]
    });
}

//保存数据
function saveData() {
    if ($('#txtSABookName').validatebox('isValid') == true && $('#txtNote').validatebox('isValid') == true) {
        $('#divSalaryBooksEdit').parent().appendTo($('form:first'));
        $('#btnSaveData').click();
    }
}
//删除工资项
function deleteSalaryBooks() {
    var aBooksId = new Array();
    var sBooksNameWarning = '';
    if ($('#hfldCheckedId').val().indexOf('[') == 0) {  
        //判断选中的如果是多个帐套
        aBooksId = eval($('#hfldCheckedId').val()); //获得以选中的帐套Id数组
        //循环此数组
        for (var i = 0; i < aBooksId.length; i++) {
            var sBooksId = aBooksId[i];
            //判断此帐套是否存在已生成未发放的工资，如果存在获得此帐套的名称并存储在sBooksNameWarning变量中
            var countSaMonthNoPayoff = getCountSaMonthNoPayoff(sBooksId);
            if (countSaMonthNoPayoff > 0) {
                var data = GetById(sBooksId);   //根据帐套Id获得账套信息
                if (data != '') {
                    sBooksNameWarning += data.Name + '、';
                }
            }
        }
    } else {
        //判断选中的如果是单个帐套
        var countSaMonthNoPayoff = getCountSaMonthNoPayoff($('#hfldCheckedId').val());
        if (countSaMonthNoPayoff > 0) {
            var data = GetById($('#hfldCheckedId').val());   //根据帐套Id获得账套信息
            if (data != '') {
                sBooksNameWarning += data.Name + '、';
            }
        }
    }

    //编制提示信息
    var sContentWarning = '';
    //如果选择的帐套中存在已生成未发放的工资,提示出帐套明细
    if (sBooksNameWarning != '') {
        sBooksNameWarning = sBooksNameWarning.substr(0, sBooksNameWarning.length - 1)
        sContentWarning = "帐套名称为" + sBooksNameWarning + '的帐套在工资管理中存在已生成未发放的工资，点击“确定”将会删除那些工资！';
    } else {
        sContentWarning = '您确定要删除吗?';
    }

    return jw.confirm('系统提示', sContentWarning, 'btnDelete');    //弹出选择窗口
}
//选择帐套明细
function editSaBooksItem(id) {
    //编辑明细前加提示，执行删除根据帐套Id删除根据此帐套已生成未发放的工资，
    //是为了防止在工资管理页面出现相同帐套不同工资项出现的问题，如果出现此问题，保存时会出现数据混乱
    var countSaMonthNoPayoff = getCountSaMonthNoPayoff(id);
    if (countSaMonthNoPayoff > 0) {
        $.messager.confirm('系统提示', '此帐套在工资管理中存在已生成未发放的工资，点击“确定”将会删除那些工资！', function (data) {
            if (data) {
                //选择“确定”
                DelCountSaMonthNoPayoff(id); //根据帐套Id删除根据此帐套已生成未发放的工资
                openSaBooksItem(id);         //打开编辑帐套明细的页面
            }
            else {
                //选择“取消”
                return false;
            }
        });
    } else {
        openSaBooksItem(id);            //打开编辑帐套明细的页面
    }
}

//打开编辑帐套明细的页面
function openSaBooksItem(id) {
    var data = GetById(id);
    if (data != '') {
        parent.desktop.SalaryBooksItem = window;
        var url = '/Salary2/SalaryBooksItem.aspx?saBooksId=' + id;
        toolbox_oncommand(url, '帐套明细(' + data.Name + ')');
    }
}

//根据帐套Id获得根据此帐套已生成未发放的工资条数
function getCountSaMonthNoPayoff(saBooksId) {
    return ajaxExecute('&type=GetCountSaMonthNoPayoff&saBookId=' + saBooksId);
}
//根据帐套Id删除根据此帐套已生成未发放的工资
function DelCountSaMonthNoPayoff(saBooksId) {
    return ajaxExecute('&type=DelCountSaMonthNoPayoff&saBookId=' + saBooksId);
}

//根据Id获取帐套信息
function GetById(id) {
    var data = '';
    $.ajax({
        type: "GET",
        async: false,
        url: '/Salary2/Handler/GetSalaryInfo.ashx?' + new Date().getTime() + '&page=salaryBooks&type=edit&id=' + id,
        success: function (datas) {
            if (datas != '') {
                data = datas;
            }
        }
    });
    return data;
}

//执行ajax
function ajaxExecute(paramUrl) {
    var data = '';
    var urlPath = '/Salary2/Handler/GetSalaryInfoPlain.ashx?' + new Date().getTime() + paramUrl;
    $.ajax({
        type: "GET",
        async: false,
        url: urlPath,
        success: function (datas) {
            if (datas != '') {
                data = datas;
            }
        }
    });
    return data;
}
//打开查看页面
function viewopen(id) {
    parent.desktop.SalaryBooksList = window;
    var url = '/Salary2/ViewSalaryBooksItem.aspx?&&saBooksId=' + id;
    toolbox_oncommand(url, "查看帐套明细");
}