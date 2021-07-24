// PettyCashList.aspx
$(document).ready(function () {
	// 添加验证
	$('#btnSearch')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }

	var gvwPettyCash = new JustWinTable('gvwPettyCash');
	replaceEmptyTable('gvwPettyCashEmpty', 'gvwPettyCash'); 	// 数据为空时替换表格

	showTooltip('tooltip', 20);                         // 提示

	// 设置只读
	$('#txtStartDate').attr('readonly', 'readonly');
	$('#txtEndDate').attr('readonly', 'readonly');

	if (getRequestParam('type') == 'manager') {
		// 如果是财务人员查看
		$('#span_buttons').empty();
		$('#span_user').html($('#hfldUserName').val() + '借款明细');
	} else {
		// 个人页面
		setButton(gvwPettyCash, 'btnDelete', 'btnEdit', 'btnQuery', 'hfldPettyCashIds');
		setWfButtonState2(gvwPettyCash, 'WF1');             // 控制流程按钮
		$('#btnAdd').click(addPettyCash); 				// 新增
		$('#btnDelete')[0].onclick = deletePettyCash; 	// 删除
		$('#btnEdit').click(editPettyCash); 				// 编辑
		$('#btnQuery').click(queryPettyCash); 			// 查看
	}

	// 添加查看事件
	$('.link').click(function () {
		var id = $(this).parent().parent().attr('id');
		var url = '/PettyCash/PettyCashDetail.aspx?showAudit=1&ic=' + id;
		toolbox_oncommand(url, '备用金查看');
	});
});

// 添加备用金
function addPettyCash() {
	top.ui._pettycashlist = window;
	var url = "/PettyCash/PettyCashEdit.aspx?type=Add";
	toolbox_oncommand(url, "新增备用金");
}

// 删除备用金
function deletePettyCash() {
	return jw.confirm('系统提示', '您确定要删除吗？', 'btnDelete');
}

// 编辑备用金
function editPettyCash() {
	var id = $('#hfldPettyCashIds').val();
	top.ui._pettycashlist = window;
	var url = '/PettyCash/PettyCashEdit.aspx?type=Edit&id=' + id;
	toolbox_oncommand(url, '编辑金申请');
}

// 查看备用金
function queryPettyCash() {
	var id = $('#hfldPettyCashIds').val();
	window.open("/PettyCash/PettyCashDetail.aspx?showAudit=1&ic=" + id);
}
