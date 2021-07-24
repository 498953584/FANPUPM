// 扩展validatebox
$.extend($.fn.validatebox.defaults.rules, {
	minLength: {
		validator: function (value, param) {
			return value.length >= param[0];
		},
		message: '请至少输入 {0} 个字符。'
	}, 
    validChars: {
        validator: function (value, param) {
			value = _trim(value);				// 过滤空格
            return !/[<>?/'~`\^《》？‘·￥]/.test(value)				// 特殊字符
                && value.length <= param[0] && value.length > 0;		// 字数范围
        }, 
        message: '请输入有效字符且不得超过 {0} 个字符。'
    },
	validQueryChars: {
		validator: function (value, param) {
			value = _trim(value);				// 过滤空格
			return !/[<>?/'~`%\^《》？‘·￥]/.test(value)				// 特殊字符
				&& value.length <= param[0] && value.length > 0;		// 字数范围
        }, 
        message: '请输入有效字符且不得超过 {0} 个字符。'
	},
	regExp: {
		validator: function(value, param) {
			value = _trim(value);
			var re = new RegExp(param[0])
			return !re.test(value);
		},
		message: '请输入有效字符。'
	}
});


// 扩展 messager
$.extend($.messager, {
	// 输入密码框
	prompt2: function (_22e, msg, fn) {
		var _22f = "<div class=\"messager-icon messager-question\"></div>" + "<div>" + msg + "</div>" + "<br/>" + "<input class=\"messager-input\" type=\"password\"/>" + "<div style=\"clear:both;\"/>";
		var _230 = {};
		_230[$.messager.defaults.ok] = function () {
			win.window("close");
			if (fn) {
				fn($(".messager-input", win).val());
				return false;
			}
		};
		_230[$.messager.defaults.cancel] = function () {
			win.window("close");
			if (fn) {
				fn();
				return false;
			}
		};
		var win = _222(_22e, _22f, _230);
		win.children("input.messager-input").focus();
		return win;
	}
})


// 从easuy中拷贝
function _222(_223, _224, _225) {
	var win = $("<div class=\"messager-body\"></div>").appendTo("body");
	win.append(_224);
	if (_225) {
		var tb = $("<div class=\"messager-button\"></div>").appendTo(win);
		for (var _226 in _225) {
			$("<a></a>").attr("href", "javascript:void(0)").text(_226).css("margin-left", 10).bind("click", eval(_225[_226])).appendTo(tb).linkbutton();
		}
	}
	win.window({ title: _223, noheader: (_223 ? false : true), width: 300, height: "auto", modal: true, collapsible: false, minimizable: false, maximizable: false, resizable: false, onClose: function () {
		setTimeout(function () {
			win.window("destroy");
		}, 100);
	}
	});
	win.window("window").addClass("messager-window");
	win.children("div.messager-button").children("a:first").focus();
	return win;
};

//去掉空格
function _trim(str) {
	return str.replace(/(^\s*)|(\s*$)/g, '');
}