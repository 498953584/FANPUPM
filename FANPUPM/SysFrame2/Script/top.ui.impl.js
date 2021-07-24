// top.ui.impl.js
// 实现 top.ui.js
// require: jQuery.js, jw.js

// 封装EasyUI的show方法
function _show() {
	var title = '系统提示';
	var msg = arguments[0];

	$.messager.show({
		title: title,
		msg: msg,
		timeout: 6000,
		showType: 'slide'
	});
}

// 封装EasyUI的alert方法
function _alert() {
	var title = '系统提示';
	var msg = arguments[0];

	$.messager.alert(title, msg);
}

// 创建一个新标签
function _createTab(title, url) {
	$('#tabs').tabs('add', {
		id: url,
		title: title,
		content: createFrame(url),
		iconCls: 'icon-reload',
		closable: true
	});
}

// 打开一个标签页
function _openTab(opt) {
	opt = opt || {};
	// 如果已经存在,则选中, 否则创建一个
	var isContains = false;
	if ($('#tabs').tabs('exists', opt.title)) {
		// 若title相同，接着验证id
		var panels = $('#tabs').tabs('tabs');
		for (var i = 0; i < panels.length; i++) {
			if (opt.url == $(panels[i]).attr('id')) {
				isContains = true;
				$('#tabs').tabs('select', i);
			}
		}
	}

	if (!isContains) {
		_createTab(opt.title, opt.url);
	}
}

// 刷新选中的标签页
function _reloadTab(options) {
	options = options || {};

	if (top.ui.version == 1) {
		if (desktop.getActive() != null) {
			desktop.getActive().getWindow().getElementsByTagName("iframe")[0].src =
					options.url || desktop.getActive().getWindow().getElementsByTagName("iframe")[0].src;
		}
		else {
			parent.frameWorkArea.document.getElementById("deskframe").src =
					options.url || parent.frameWorkArea.document.getElementById("deskframe").src;
		}
	}
	else if (top.ui.version == 2) {
		var currTab = options.tab || $('#tabs').tabs('getSelected');
		var url = options.url || $(currTab.panel('options').content).attr('src');
		// 刷新
		if (!url) {
			// 我的桌面, MyPanel.refresh()
			refresh();
		} else {
			// 不是我的桌面
			$('#tabs').tabs('update', {
				tab: currTab,
				options: {
					content: createFrame(url)
				}
			});
		}
	}
}

// 关闭标签页
// falg = 1, 表示要刷新下一个显示的标签页
function _closeTab(options) {
	options = options || {};

	if (top.ui.version == 1) {
		// 需要刷新父窗口
		if (options && options.refresh == '1') {
			window.desktop[options.parent].location.href =
                    window.desktop[options.parent].location.href;
		}
		top.frameWorkArea.window.desktop.getActive().close();
	} else if (top.ui.version == 2) {
		// 要关闭的标签页
		var tab = $('#tabs').tabs('getSelected');
		var title = tab.panel('options').title;

		// 需要刷新下一个标签
		if (options.refresh == '1') {
			// 找到下一个要显示的tab
			var len = $('#tabs .tabs>li').length;
			var nextTab = $('#tabs').tabs('getTab', len - 1);
			if (nextTab.panel('options').title == title) {
				nextTab = $('#tabs').tabs('getTab', len - 2)
			}
			var url = options.url || $(nextTab.panel('options').content).attr('src');
			_reloadTab({ tab: nextTab, url: url });
		}

		// 关闭当前标签
		$('#tabs').tabs('close', title);
	}
}

// 打开新窗口
function _openWin(opt) {
	// 弹出框的序号， 1 表示在页面中直接弹出，2 表示在弹出框中再弹出一个
	var winNo = opt.winNo || 1;
	var divId = winNo == 1 ? 'div_win' : 'div_win2';

	if (opt.url) {
		// 如果存在url
		var url = jw.modifyParam({ url: opt.url, name: 'winNo', value: winNo });
		$('#' + divId + ' iframe').attr('src', url);
	} else if (opt.content) {
		$('#' + divId).html(opt.content);
	}

	$('#' + divId).window({
		title: opt.title || '系统窗口',
		width: opt.width || 600,
		height: opt.height || 500,
		modal: opt.modal || true
	});
}

// 关闭窗口
function _closeWin(opt) {
	var opt = opt || {};
	var winNo = opt.winNo || 1;
	if (winNo == 1) {
		$('#div_win iframe').attr('src', '');
		$('#div_win').window('close');
	}
	else if (winNo == 2) {
		if (typeof top.ui._callback == 'function') {
			top.ui.callback = top.ui._callback;
		}
		$('#div_win2 iframe').attr('src', '');
		$('#div_win2').window('close');
	}
}

// 操作成功, 调用closeWin, 并刷新tab
function _winSuccess(opt) {
	opt = opt || {};
	var msg = '保存成功' || opt.msg

	// 刷新父页面
	if (opt.parentName) {
		if (opt.url) {
			this[opt.parentName].location.href = opt.url;
		} else {
			this[opt.parentName].location.href = this[opt.parentName].location.href;
		}
		this[opt.parentName] = null;
	}

	_show(msg);
	_closeWin();
}

// 子标签页操作成功
function _tabSuccess(opt) {
	opt = opt || {};
	var msg = '保存成功' || opt.msg;
	// 刷新父页面
	if (opt.parentName) {
		if (opt.url) {
			this[opt.parentName].location.href = opt.url;
		} else {
			this[opt.parentName].location.href = this[opt.parentName].location.href;
		}
		this[opt.parentName] = null;
	}

	_show(msg);
	_closeTab();
}

// 子标签页操作失败
function _tabError() {
	option = option || {};
	var msg = '保存失败' || option.msg;
	_show(msg);
	_closeTab();
}
