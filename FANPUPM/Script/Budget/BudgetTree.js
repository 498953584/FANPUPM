$(document).ready(function() {
    //$('img').ajaxStart($.blockUI).ajaxStop($.unblockUI);
    $imgLminus = $('<img style="vertical-align:middle; "; src="/images/tree/lminus.gif" />');
    $imgTminus = $('<img style="vertical-align:middle; "; src="/images/tree/tminus.gif" />');
    $imgWhite = $('<img style="vertical-align:middle;"; src="/images/tree/white.gif" />');
    $imgI = $('<img style="vertical-align:middle;"; src="/images/tree/i.gif" />');
    $imgT = $('<img style="vertical-align:middle;"; src="/images/tree/t.gif" />');
    $imgL = $('<img style="vertical-align:middle;"; src="/images/tree/l.gif" />');
    $imgLplus = $('<img style="vertical-align:middle;"; src="/images/tree/lplus.gif" />');
    $imgTplus = $('<img style="vertical-align:middle;"; src="/images/tree/tplus.gif" />');
    
    $('#gvBudget tr:gt(0)').each(function() {
        $td = $(this).find('td:eq(2)');
        if (this.layer == '1') {
            if (IsExistNextsibling(this)) {
                //存在下一个兄弟节点
                if (this.subCount == 0) {
                    //叶子节点
                    $td.prepend($imgT.clone());
                } else {
                    //非叶子节点
                    $td.prepend($imgTminus.clone());
                }
            } else {
                //不存在下个兄弟节点
                if (this.subCount == 0) {
                    //叶子节点  
                    $td.prepend($imgL.clone());
                    
                } else {
                    //非叶子节点
                    //alert(this.subCount);
                    $td.prepend($imgLminus.clone());
                }
            }
        } else if (this.layer == '2') {
            if (IsExistNextsibling(this)) {
                //存在下个兄弟节点
                if (this.subCount == 0) {
                    //叶子节点
                    $td.prepend($imgT.clone());
                } else {
                    //非叶子节点
                    $td.prepend($imgTplus.clone());
                }
            } else {
                //不存在下个兄弟节点
                if (this.subCount == 0) {
                    //叶子节点
                    $td.prepend($imgL.clone());
                } else {
                    //非叶子节点
                    $td.prepend($imgLplus.clone());
                }
            }
            if (IsExistParentsibling(this)) {
                //下面存在父节点的兄弟节点
                $td.prepend($imgWhite.clone());
            } else {
                //下面不存在父节点的兄弟节点
                $td.prepend($imgI.clone());
            }
            
        }
        
    });
    
    //鼠标事件
    $('#gvBudget tr:gt(0)').each(function(index) {
        var $tr = $(this);
        $tr.find('img[src*="tminus.gif"]').live('click', function(event) {
            $(getChildren($tr[0])).hide();
            this.src = '/images/tree/tplus.gif';
        });

        $tr.find('img[src*="lminus.gif"]').live('click', function(event) {
            $(getChildren($tr[0])).hide();
            this.src = '/images/tree/lplus.gif';
        });

        $tr.find('img[src*="lplus.gif"]').live('click', function(event) {
            addChildren($tr[0])
            this.src = '/images/tree/lminus.gif';
        });

        $tr.find('img[src*="tplus.gif"]').live('click', function(event) {
            addChildren($tr[0]);
            this.src = '/images/tree/tminus.gif';
        });
    });
});

//是否存在下一个兄弟节点
function IsExistNextsibling(tr) {
    var nextTrAll = $(tr).nextAll();
    for (var i = 0; i < nextTrAll.length; i++) {
        if (parseInt(nextTrAll[i].layer) < parseInt(tr.layer))
            return false;
        if (nextTrAll[i].layer == tr.layer)
            return true;
    }
    return false;
}

//下面是否存在父节点的兄弟节点
function IsExistParentsibling(tr) {
    var nextTrAll = $(tr).nextAll();
    var parentLayer = parseInt(tr.layer) + 1;
    for (var i = 0; i < nextTrAll.length; i++) {
        if (nextTrAll[i].layer == parentLayer) 
            return true;
    }
    return false;
}

//返回当前节点的所有孩子孩子节点
function getChildren(tr) {
    var children = new Array();
    var layer = parseInt(tr.layer);
    var $nextAll = $(tr).nextAll();
    for (var i = 0; i < $nextAll.length; i++) {
        if (parseInt($nextAll[i].layer) > layer) {
            children.push($nextAll[i]);
        } else {
            return children;
        }
    }
    return children;
}

function addChildren(tr) {
    $.blockUI();
    var children = getChildren(tr)
    if (children.length > 0) {
        $(children).show();
        $.unblockUI();
    } else {
    $.ajax({
        url: '/BudgetManage/Handler/GetChildTask.ashx?TaskId=' + tr.id + '&Time=' + new Date().getTime(),
        async: true,
        success: function (data) {
            $.unblockUI();
            for (var j = data.length - 1; j >= 0; j--) {
                var task = data[j];
                var $trChild = $('<tr id=' + task.TaskId + ' orderNumber=' + task.orderNumber +
                        ' layer=' + task.OrderNumber.length / 3 + ' subCount=' + task.SubCount + '></tr>');
                $trChild.append($('<td><span title=' + task.TaskId + '><input type="checkbox" /></span></td>'));
                $trChild.append($('<td align="right">' + task.No + '</td>'));
                $tdName = $('<td>' + task.TaskName + '</td>')
                var layer = task.OrderNumber.length / 3;
                if (task.SubCount == 0) {
                    //没有子节点
                    $tdName.prepend($imgT.clone());
                } else {
                    //有子节点
                    var $img = $imgTplus.clone();
                    $img.click(function (event) {
                        if (this.src.indexOf('tminus') != -1) {
                            this.src = '/images/tree/tplus.gif';
                            $(getChildren($(this).parent().parent().get(0))).hide();
                        } else if (this.src.indexOf('tplus') != -1) {
                            this.src = '/images/tree/tminus.gif';
                            addChildren($(this).parent().parent().get(0));
                        } else if (this.src.indexOf('lplus') != -1) {
                            this.src = '/images/tree/lminus.gif';
                            $(getChildren($(this).parent().parent().get(0))).hide();
                        } else if (this.src.indexOf('lplus') != -1) {
                            this.src = '/images/tree/lminus.gif';
                            addChildren($(this).parent().parent().get(0))
                        }
                    });
                    $tdName.prepend($img);
                }
                for (var i = 0; i < layer - 1; i++) {
                    $tdName.prepend($imgI.clone());
                }
                $trChild.append($tdName);
                $trChild.append($('<td>' + task.TaskCode + '</td>'));
                $trChild.append($('<td>' + task.TypeName + '</td>'));
                $trChild.append($('<td>' + task.Unit + '</td>'));
                $trChild.append($('<td align="right">' + task.Quantity + '</td>'));
                if ($('#hfldIsWBSRelevance').val() == "1") {
                    $trChild.append($('<td>' + task.StartDate || '' + '</td>'));
                    $trChild.append($('<td>' + task.EndDate || '' + '</td>'));
                }
                $trChild.append($('<td align="right">' + task.UnitPrice + '</td>'));
                $trChild.append($('<td align="right">' + task.Total + '</td>'));
                $trChild.append($('<td>' + task.Note + '</td>'));
                if (j % 2 == 0) {
                    $trChild.attr('class', 'rowa');
                } else {
                    $trChild.attr('class', 'rowb');
                }
                $(tr).after($trChild);
            }
        }
    });
    }  
}
