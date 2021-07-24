$(document).ready(function () {
    var jwTable = new JustWinTable('gvwUserInfo');
    replaceEmptyTable('EmptyUserInfo', 'gvwUserInfo');
    setButton(jwTable, 'btnAssign', '', '', 'hfldCheckedId');
    showTooltip('tooltip', 25);                         // 提示
    $('#gvwUserInfo').find('select').each(function () {
        $(this).change(function () {
            var saBooksId = $(this).val();
            var user = $('#hfldCheckedId').val();
            singleExecte(user, saBooksId);
        })
    });
    $('#btnAssign').click(AssignSaBooks);
})
//指定帐套
function AssignSaBooks() {
    $('#divSaBooks').css('display', 'block').dialog({
        title: '选择帐套',
        width: 400,
        height: 150,
        modal: true,
        buttons: [{
            text: '保存',
            handler: function () {
                saveData();
            }
        }, {
            text: '取消',
            handler: function () {
                $('#divSaBooks').dialog('close');
            }
        }]
    });

}
//保存
function saveData() {
    $('#divSaBooks').parent().appendTo($('form:first'));
    $('#hfldSelectedSaBooks').val($('#ddlSaUserBooks').val());
    $('#btnSave').click();
}
//单独操作
function singleExecte(user, booksId) {
    $.ajax({
        type: "GET",
        async: false,
        url: '/Salary2/Handler/SAUserBooks.ashx?' + new Date().getTime() + '&user=' + user + '&booksId=' + booksId,
        success: function (datas) {
            if (datas != '') {
            }
        }
    });
}