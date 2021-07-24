
$(document).ready(function () {
    var consTaskId;
    var tr;
    $('#gvTask img').click(function () {
        tr = this.parentNode.parentNode;
        if ($('#1').length > 0 && consTaskId != $(this).attr('rowId')) {
            consTaskId = $(this).attr('rowId');
            $('#1').remove();
            GetConsResource(tr, consTaskId);
        }
        else if ($('#1').length > 0 && consTaskId == $(this).attr('rowId')) {
            $('#1').remove();
        }
        else {
            consTaskId = $(this).attr('rowId');
            GetConsResource(tr, consTaskId);
        }
    });

    //资源选择事件
    $('#btnAddResource').live('click', function () {
        var src = '/StockManage/UserControl/SelectResource.aspx?TypeCode=0&ButtonId=btnBindResource';
        $('#ifResouece').attr('src', src);
        $('#divSelectResource').dialog({ width: 600, height: 485, modal: true });
    });

    //选择资源成功执行事件
    $('#btnBindResource').click('click', function () {
        $.ajax({
            type: 'GET',
            url: '/BudgetManage/Handler/AddConsResource.ashx?' + new Date().getTime()
                + '&ResourceIds=' + $('#hfldResourceId').val() + '&ConsTaskId=' + consTaskId,
            success: function (data) {
                if (data == 1) {
                    $('#1').remove();
                    GetConsResource(tr, consTaskId);
                }
            }
        });
    })

    //    //防止事件冒泡
    //    $('#gvTask input').click(function() {
    //        return false;
    //    });

    //设置今日完成量
    $('#gvTask input[id$=txtCompleteQuantity]').bind('keyup', function () {
        var quantity = $(this).val();
        var consTaskId = $(this).parent().parent().attr('id');
        var allQuantity = $(this).parent().prev().text();
        if (Number(quantity) > Number(allQuantity)) {
            //使用alert会阻止保存按钮的onclick事件   alert('系统提示：\n\n今日完成量已大于剩余工作量。')  
            $(this).css('color', 'Red');
        } else {
            $(this).css('color', 'Black');
        }
        if ($('#hfldIsWBSRelevance').val() == '1') {
            //当资源与WBS不挂钩时，不计算资源
            tr = this.parentNode.parentNode;
            $.ajax({
                type: 'GET',
                async: false,
                url: '/BudgetManage/Handler/SetCompleteQuantity.ashx?' + new Date().getTime()
                + '&ConsTaskId=' + consTaskId + '&Quantity=' + quantity,
                success: function (data) {
                    if (data == '1') {
                        //成功
                        if ($('#1').get(0) == undefined) {

                        } else {
                            $('#1').remove();
                            GetConsResource(tr, consTaskId);
                        }
                    }
                }
            });
        }
    });
});


function GetConsResource(tr, consTaskId) {
    $.ajax({
        type: 'GET',
        async: false,
        url: '/BudgetManage/Handler/GetConsResource.ashx?' + new Date().getTime() + '&ConsTaskId=' + consTaskId,
        success: function (data) {
            $(tr).after('<tr id="1"><td align="center" colspan="12" style="border:solid 1px #CADEED;">' + data + '</td></tr>');
        }
    });
}
