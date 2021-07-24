$(document).ready(function () {
    var jwTable = new JustWinTable('gvwSaMonth');
    replaceEmptyTable('EmptySaMonth', 'gvwSaMonth');
    var saBooksId = $('select#ddlSaBooks  option:selected').val();
    if ($('#hfldDepartment').val() != '' && saBooksId != '') {
        $('#btnGenerate').removeAttr('disabled');
    } else {
        $('#btnGenerate').attr('disabled', 'disabled');
    }
})

//显示新增信息
function showAddInfo() {
    $('#divAddInfo').css('display', 'block').dialog({
        title: '新增',
        width: 400,
        height: 150,
        modal: true,
        buttons: [{
            text: '新增',
            handler: function () {
                saveData();
            }
        }, {
            text: '取消',
            handler: function () {
                $('#divAddInfo').dialog('close');
            }
        }]
    });
}
//保存数据
function saveData() {
    $('#divAddInfo').parent().appendTo($('form:first'));
    $('#btnAddData').click();
}

//计算新的工资额
function computeValue(txt) {
    var txtControl = txt;
    var trSelected = getFirstParentElement(txtControl.parentNode, 'TR');
    var trId = trSelected.id;
    //文本框itemId属性
    var currentTrItemCost = new Array();
    //循环本TR内的文本框，并获得文本框的信息
    $('#' + trId + ' :text').each(function () {
        var itemCost = {};
        itemCost.itemId = $(this).attr('itemId');   //工资项Id
        if ($(this).attr('disabled') == 'disabled') {
            itemCost.IsFormula = true;              //工资是否被禁用
        }
        //该条工资项的金额
        if ($(this).val() != '') {
            itemCost.Cost = _formatDecimal($(this).val());
        } else {
            itemCost.Cost = 0;
        }
        currentTrItemCost.push(itemCost);
    });
    var booksId = $('#hfldSaBooksId').val();        //帐套Id
    var datas = computeFormula(JSON.stringify(currentTrItemCost), booksId);     //计算新的工资额
    var newTrItem = new Array();
    try {
        newTrItem = eval(datas);
        var hiddens = $('#' + trId + ' :hidden');       //获得本TR内的隐藏表单
        $('#' + trId + ' :text').each(function () {
            var itemVal = "hfld" + this.itemId;        //获得本TD中的隐藏表单ID
            //循环计算后的工资额
            for (var i = 0; i < newTrItem.length; i++) {
                if ($(this).attr('itemId') == newTrItem[i].ItemId) {
                    //将计算后得到的工资额赋给文本框和隐藏表单
                    var newVal = newTrItem[i].Cost.toFixed(3).toString();
                    $(this).val(_formatDecimal(newVal));
                    for (var i = 0; i < hiddens.length; i++) {
                        if (hiddens[i].id.indexOf(itemVal) > 0) {
                            hiddens[i].value = _formatDecimal(newVal);
                            break;
                        }
                    }
                }
            }
        });
    } catch (e) {
        jw.alert('系统提示', '计算工资失败，请确认公式逻辑是否正确或者计算额度是否过大！');
    }
}

//计算
function computeFormula(itemCosts, booksId) {
    var data = '';
    $.ajax({
        type: "GET",
        async: false,
        url: '/Salary2/Handler/ComputeByFormula.ashx?' + new Date().getTime() + '&type=computeFormula&itemCost=' + itemCosts + '&booksId=' + booksId,
        success: function (datas) {
            if (datas != '') {
                data = datas;
            }
        }
    });
    return data;
}