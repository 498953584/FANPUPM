$(document).ready(function () {
    var jwTable = new JustWinTable('gvwSaMonth');
    replaceEmptyTable('EmptySaMonth', 'gvwSaMonth');
    setButton2(jwTable, 'btnPayoff', 'hfldCheckedId');
})
function setButton2(jwTable, btnPayoff, hdChkId) {
    if (!jwTable.table) return;
    if (jwTable.table.getElementsByTagName('TR').length == 1) {
        //table中没有数据
        return;
    }
    jwTable.registClickTrListener(function () {
        if (hdChkId != '')
            $('#' + hdChkId).val(this.id);
        if (this.getAttribute('isPayoff') == 'False')
            $('#' + btnPayoff).removeAttr('disabled');
        else
            $('#' + btnPayoff).attr('disabled', 'disabled');
    });
    jwTable.registClickSingleCHKListener(function () {
        var checkedChk = jwTable.getCheckedChk();
        if (checkedChk.length == 0) {
            $('#' + btnPayoff).attr('disabled', 'disabled');
        }
        else if (checkedChk.length == 1) {
            var trChecked = getFirstParentElement(checkedChk[0].parentNode, 'TR');
            if (hdChkId != '')
                $('#' + hdChkId).val(trChecked.id);
            if (trChecked.getAttribute('isPayoff') == 'False')
                $('#' + btnPayoff).removeAttr('disabled');
            else
                $('#' + btnPayoff).attr('disabled', 'disabled');
        }
        else {
            var isAllowPayoffArray = new Array();
            if (hdChkId != '')
                $('#' + hdChkId).val(jwTable.getCheckedChkIdJson(checkedChk));
            for (var i = 0; i < checkedChk.length; i++) {
                var trChecked = getFirstParentElement(checkedChk[i].parentNode, 'TR');
                isAllowPayoffArray.push(trChecked.getAttribute('isPayoff'));
            }
            if (checkIsAllowPayoff(isAllowPayoffArray) == '1') {
                $('#' + btnPayoff).removeAttr('disabled');
            } else {
                $('#' + btnPayoff).attr('disabled', 'disabled');
            }
        }
    });
    jwTable.registClickAllCHKLitener(function () {
        if (this.checked) {
            var isAllowPayoffArray = new Array();
            var checkedIdArray = new Array();
            $('#gvwSaMonth [type=checkbox]').each(function () {
                if (this.id.indexOf('chbAll') < 0) {
                    var trSelected = getFirstParentElement(this.parentNode, 'TR');
                    isAllowPayoffArray.push(trSelected.getAttribute('isPayoff'));
                    checkedIdArray.push(trSelected.id);
                }
            });
            if (hdChkId != '')
                $('#' + hdChkId).val(JSON.stringify(checkedIdArray));
            //判断选中的是否允许删除
            if (checkIsAllowPayoff(isAllowPayoffArray) == '1') {
                $('#' + btnPayoff).removeAttr('disabled');
            } else {
                $('#' + btnPayoff).attr('disabled', 'disabled');
            }
        }
        else {
            if (hdChkId != '')
                $('#' + hdChkId).val('');
            $('#' + btnPayoff).attr('disabled', 'disabled');
        }
    });
}

//遍历选中的是否允许删除
function checkIsAllowPayoff(isAllowPayoffArray) {
    var isAllowPayoff = '1';
    for (var i = 0; i < isAllowPayoffArray.length; i++) {
        if (isAllowPayoffArray[i] != 'False') {
            isAllowPayoff = '0';
            break;
        }
    }
    return isAllowPayoff;
}