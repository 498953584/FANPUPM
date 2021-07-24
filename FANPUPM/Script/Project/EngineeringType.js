
var typeId = "dropBuildingType_";
var subTypeId = "dropBuildingSubType_";
var gradeId = 'txtDetailGrade_';

//绑定工程类型
function bindEngSubType(typeId, select_subTypeId) {
    $.ajax({
        type: 'GET',
        async: false,
        url: '/TenderManage/GetSubType.ashx?'+new Date().getTime()+'&Type=' + typeId,
        success: function(str) {
            var select_sub = document.getElementById(select_subTypeId);
            if (!select_sub) return false;
            var strTemp = str.split('|');
            for (var i = 0; i < strTemp.length; i++) {
                var typeTemp = strTemp[i].split(',');
                select_sub.options[i] = new Option(typeTemp[1], typeTemp[0]);
                select_sub.length = strTemp.length;
            }
        }
    });
}

//显示工程类型
function showEngType() {
    //至少包含一个
    $('#dropBuildingType_0').change(function() {
        send_request('dropBuildingType_0', 'dropBuildingSubType_0');
    });

    var json = $('#hfldEngTypeJson').val();
    if (!json) return false;
    var types = eval(json);
    var len = types.length;             //工程类型的数量
    if (len > 0) {
        var type = types[0];
        //初始化第一个工程类型
        $('#dropBuildingType_0').val(type.EngineeringTypeID);
        //绑定子工程类型
        bindEngSubType(type.EngineeringTypeID, 'dropBuildingSubType_0');
        $('#dropBuildingSubType_0').val(type.EngineeringSubTypeID);

        $('#txtDetailGrade_0').val(type.Grade);


        //初始化其余的工程的类型
        for (var i = 1; i < len; i++) {
            var type = types[i];
            var $newtr = $('#tr_engType').clone();
            //移除“工程类型”说明
            $newtr.find('td:eq(0)').empty();

            //工程类型
            $newtr.find('select[id^=' + typeId + ']').attr('id', typeId + i)
                .val(type.EngineeringTypeID);

            //子工程类型                    
            $newtr.find('select[id^=' + subTypeId + ']').attr('id', subTypeId + i);

            //等级
            $newtr.find('input[id^=' + gradeId + ']').attr('id', gradeId + i)
                .val(type.Grade);

            //添加删除事件
            var btn_delete = $newtr.find('input').get(1)
            btn_delete.value = "删除";
            $(btn_delete).click(function() {
                $(this).parent().parent().remove();
            });
            //添加新行
            $('#tr_engType').after($newtr);
            //绑定子工程类型
            bindEngSubType(type.EngineeringTypeID, $newtr.find('select[id^=' + subTypeId + ']').attr('id'));
            $newtr.find('select[id^=' + subTypeId + ']').val(type.EngineeringSubTypeID);
            //添加联动事件
            registerDropChangeEvent(typeId + i, subTypeId + i);
        }
    }
}

//注册工程类型联动事件
function registerDropChangeEvent(typeId, subTypeId) {
    $('#' + typeId).change(function() {
        send_request(typeId, subTypeId);
    });
}

//修改工程类型
//engTypeId: 工程类型控件ID
//subEngTypeId: 子工程类型控件ID
function send_request(engTypeId, subEngTypeId) {
    if (document.getElementById(engTypeId).value != '') {
        //工程类型非空
        bindEngSubType(document.getElementById(engTypeId).value, subEngTypeId);
    }
    else {
        //工程类型为空
        var subEngType = document.getElementById(subEngTypeId);
        subEngType.options[0] = new Option("", "");
        subEngType.length = 1;
        //清空级别
        $(subEngType).parent().find('input').get(0).setAttribute('value', '');
    }
}
function change() {
    if (document.getElementById(subEngTypeId).value != "")
        document.getElementById('dropvalue').value = document.getElementById(subEngTypeId).value;
}

//保存方法
function save() {
    var typeArr = new Array();            //工程类型集合
    var $trs = $('.tr_engType');        //所有的工程类型的行
    $trs.each(function(i) {
        var id = jw.newGuid();
        var prjGuid = $('#hfldPrjGuid').val();
        var engineeringTypeID = $($trs[i]).find('select:eq(0)').val();
        var engineeringSubTypeID = $($trs[i]).find('select:eq(1)').val();
        var grade = $($trs[i]).find('input:eq(0)').val()
        var type = { "ID": id,
            "PrjGuid": prjGuid,
            "EngineeringTypeID": engineeringTypeID,
            "EngineeringTypeName": "",
            "EngineeringSubTypeID": engineeringSubTypeID,
            "EngineeringSubTypeName": "",
            "Grade": grade
        };
        if (type.EngineeringSubTypeID)
            typeArr.push(type);
    });
    //保存到隐藏控件
    var json = typeArr.length == 0 ? '' : JSON.stringify(typeArr);
    $('#hfldEngTypeJson').val(json);
}

//添加工程类型事件
$('#btn_editBuildingType').click(editBuildingType)

function editBuildingType() {
    var len = $('.tr_engType').length;
    var $newtr = $('#tr_engType').clone();          //复制首行
    $newtr.find('td:eq(0)').empty();                //移除“工程类型”说明
    //修改ID和值
    $newtr.find('select[id^=' + typeId + ']').attr('id', typeId + len).val('');
    $newtr.find('select[id^=' + subTypeId + ']').attr('id', subTypeId + len)
                    .val('').find('option').remove();
    $newtr.find('input[id^=' + gradeId + ']').attr('id', gradeId + len).val('');
    var btn_delete = $newtr.find('input').get(1);
    btn_delete.value = "删除";
    //添加删除事件
    $(btn_delete).click(function() {
        $(this).parent().parent().remove();
    });
    //添加新行
    $('.tr_engType').last().after($newtr);

    //添加联动事件
    $newtr.find('select[id^=' + typeId + ']').change(function() {
        send_request(typeId + len, subTypeId + len);
    });
}
