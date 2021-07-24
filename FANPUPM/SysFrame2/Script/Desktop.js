// Desktop.js
// 依赖jQuery.js, top.ui.js
$(document).ready(function () {
	// 为了兼容旧模式
	if (!window.desktop) window.desktop = {};

	// 弹出窗口的回调方法
	if (!window.callback) window.callback = {};

	// 当前用户编码
	var userCode = $('#hfldUserCode').val()

	// 绑定顶层菜单
	bindTopMenu();

	// 添加顶层菜单的单击事件
	addTopMenuClickEvent();

	// 添加上下文菜单
	addContextMenu();

	// 添加标签页事件
	addTabEvent();

	// 添加工具箱事件
	addToolbarEvent();

	// 鼠标提示
	setTimeout("jw.tooltip();", 4000);

	// 预加载子菜单
	preLoadMenu();

	// 设置按钮随滚动条移动
	var $div_setting = $('#div_setting');
	var setting_width = parseInt($div_setting.css('width'));
	var sleft = $div_setting.scrollLeft();
	$('.my-desk').scroll(function () {
		if (sleft == $(this).scrollLeft()) return;
		var width = $(this).scrollLeft() + setting_width;
		$div_setting.css('width', width);
	});
});

var _g_mk = [];

// 绑定顶层菜单
function bindTopMenu() {
	var arr = eval($('#hfldDepEmployeeJson').val());
	for (var i = 0; i < arr.length; i++) {
		$('#nav').accordion('add', {
			id: arr[i].id,
			title: arr[i].text,
			selected: false
		});
	}
}

// 添加顶层菜单的单击事件, accordion
function addTopMenuClickEvent() {
	// 选中时触发
	$('#nav').accordion({
		onSelect: function (title, index) {
			var sel = $(this).find('.accordion-body').get(index); 	// 当前选中的一级菜单
			// 如果没有子菜单则加载
			if (!$(sel).html()) {
				var $tree = $('<ul id="tree"></ul>');

				// 子菜单已经预加载
				if (_g_mk.length > 0) {
					var data;
					for (var i = 0; i < _g_mk.length; i++) {
						if (_g_mk[i]._id == sel.id) {
							data = _g_mk[i]._data;
							break;
						}
					}

					$tree.tree({
						data: data,
						onClick: function (node) { addTab(node, $tree); } // 添加标签页
					});
				} else {
					// 没有预加载，实时读取
					$.ajax({
						url: '../PTMKService.svc/GetAll/' + $('#hfldUserCode').val() + '/' + sel.id,
						contenttype: 'application/json',
						async: false,
						success: function (data) {
							$tree.tree({
								data: data,
								onClick: function (node) { addTab(node, $tree); } // 添加标签页
							});
						},
						error: function (ex) { alert('菜单加载错误.'); }
					});
				}

				$(sel).append($tree);
			}
		},
		animate: false
	});
}

// 预加载子菜单
function preLoadMenu() {
	var userCode = $('#hfldUserCode').val();
	$('#nav div[id]').each(function () {
		var _id = this.id;
		$.ajax({
			url: '../PTMKService.svc/GetAll/' + userCode + '/' + _id,
			contenttype: 'application/json',
			async: true,
			error: function (ex) { alert('菜单加载错误.'); },
			success: function (data) {
				var obj = { _id: _id, _data: data };
				_g_mk.push(obj);
			}
		});
	});
}

// 添加标签页
function addTab(node, $tree) {
	if (node.state) {
		// 非叶子节点
		var cnode = $tree.tree('getSelected');
		$tree.tree('toggle', cnode.target);
	} else {
		// 叶子节点
		var url = eval(node.attributes)[0].url;
		_openTab({ title: node.text, url: url });
	}
}

// 添加标签页事件
function addTabEvent() {
	addTabContextMenu(); 	// 绑定右键菜单事件

	// 给标签页绑定刷新事件
	$('.icon-reload').live('click', function () { _reloadTab(); });

	// 双击关闭标签页
	$(".tabs-inner").live('dblclick', function () {
		$('#tabs').tabs('close', $(this).children(".tabs-closable").text());
	});

	// 不关闭我的桌面
	$('#tabs').tabs({
		onBeforeClose: function (title) {
			return title != '我的桌面';
		}
	});
}



// 添加上下文菜单
function addContextMenu() {
	$(".tabs-inner").live('contextmenu', function (e) {
		$('#cm').menu('show', {
			left: e.pageX,
			top: e.pageY
		});

		var subtitle = $(this).children(".tabs-closable").text();
		$('#cm').data("currtab", subtitle);
		$('#tabs').tabs('select', subtitle);
		return false;
	});
}

// 绑定右键菜单事件
function addTabContextMenu() {
	// 刷新
	$('#cm-tabupdate').click(function () {
		_reloadTab();
	});
	// 关闭当前
	$('#cm-tabclose').click(function () {
		var currtab_title = $('#cm').data("currtab");
		$('#tabs').tabs('close', currtab_title);
	});
	// 全部关闭
	$('#cm-tabcloseall').click(function () {
		$('.tabs-inner span').each(function (i, n) {
			var t = $(n).text();
			$('#tabs').tabs('close', t);
		});
	});
	// 关闭除当前之外的TAB
	$('#cm-tabcloseother').click(function () {
		var thisText = $('.tabs-selected').text();
		$('.tabs-inner span').each(function (i, n) {
			var t = $(n).text();
			if (t != thisText)
				$('#tabs').tabs('close', t);
		});
	});
}

// 创建iframe
function createFrame(url) {
	// 解决一些路径不是以‘/’开头
	if (url[0] != '/' && url.indexOf('http') == -1)
		url = '/' + url;
	var s = '<iframe scrolling="auto" frameborder="0"  src="' +
		url + '" style="width:100%;height:98%;"></iframe>';
	return s;
}

// 添加工具箱事件
function addToolbarEvent() {
	// 当前在线人数
	$('#lblOnlineNum, #sp_onlineNum').click(function () {
		var url = '/SysFrame/UserListPop.aspx';
		top.ui.openWin({ title: '当前在线人数', url: url });
	});

	// 退出
	$('#sp_exit').click(function () {
		$.messager.confirm('系统提示', '确定要退出系统吗？', function (r) {
			if (r) {
				$('#btn_exit').click();
				//				window.location.href = '../';
			}
		});
	});

	// 我的桌面
	$('#sp_mydesktop').click(function () {
		_openTab({ title: '我的桌面', url: '/SysFrame2/Desktop.aspx' });
	});

	// 我的邮箱
	$('#sp_myemail').click(function () {
		_openTab({ title: '我的邮箱', url: '/OA2/Mail/MailFrame.aspx' });
	});

	// 流程监控
	$('#sp_wfmonitor').click(function () {
		_openTab({ title: '流程监控', url: '/oa/UserDefineFlow/MyFlowManage.aspx' });
	});

	// 员工自助
	$('#sp_myhelp').click(function () {
		_openTab({ title: '员工自助', url: '/oa/Buffet/BuffetFrame.aspx' })
	});

	// 关于
	$('#sp_about').click(function () {
		$('#div_about').window({
			width: 410,
			height: 300,
			modal: true
		});
	});

	// 帮助
	$('#sp_help').click(function () {
		var tab = $('#tabs').tabs('getSelected');

		var url = '../Help/help.aspx?url=';
		if (tab.find('iframe').length > 0) {
			// 新界面
			url += tab.find('iframe').attr('src');
		} else {
			// 老界面
			url += '../TableTop/tableMain.aspx';
		}
		window.open(url, '',
            'left=150,top=150,width=800,height=500,toolbar=no,status=yes,scrollbars=yes,resizable=yes');
	});
}

// 关于中的确定按钮
function btn_about_ok() {
	$('#div_about').window('close');
}