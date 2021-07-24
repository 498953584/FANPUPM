// top.ui.js
// 与旧模式抽象出共同的接口
// require: jQuery.js, jw.js
$(document).ready(function () {
    if (!top.ui) top.ui = {};

    top.ui.version = '1';
    top.ui.callback = {};

    top.ui.alert = _alert;
    top.ui.show = _show;
    top.ui.prompt = $.messager.prompt;
    top.ui.prompt2 = $.messager.prompt2; 		// 密码框
    top.ui.confirm = $.messager.confirm;

    top.ui.createTab = function () { };
    top.ui.openTab = function () { };
    top.ui.reloadTab = _reloadTab; 		        // 刷新
    top.ui.closeTab = _closeTab; 				// 关闭
    top.ui.openWin = _openWin;
    top.ui.closeWin = _closeWin;
    top.ui.winSuccess = _winSuccess;
    top.ui.tabSuccess = _tabSuccess;
    top.ui.tabError = _tabError;
});
