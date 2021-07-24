$(document).ready(function () { 
    var data = eval($('#hfldDepEmployeeJson').val());
    $('#tree').tree({
        data: data,
        onClick: function(node) {
            if (node.id.length == 8) {
                $('#ifraCash').attr('src',"/PettyCash/PettyCashList.aspx?type=manager&userCode=" + node.id);
            }
        },
        onBeforeExpand: function(node) {
            // node
            var childLeng = $('#tree').tree('getChildren', node.target).length;
            if (childLeng == 0) {           // 如果没有子节点，则异步加载子节点
                var children = getChildrenJson(node.id);
                $('#tree').tree('append', { 
                    parent: node.target,
                    data: children
                });
            }
        }
    });
});

// 返回子节点的JSON对象
function getChildrenJson(id) {
    var json;
    $.ajax({
        type: 'GET',
        url: 'Handler/GetDepEmpChildren.ashx?id=' + id + '&date=' + new Date().getTime(),
        async: false,
        success: function(data) {
            json = data;
        },
        error: function() {
            alert('error');       
        }
    });
    return eval(json);
}

