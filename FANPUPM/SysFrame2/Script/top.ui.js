// top.ui.js
// 与旧模式抽象出共同的接口
// require: jQuery.js, jw.js
$(document).ready(function () {
    if (!window.ui) window.ui = {};

    window.ui.version = '2';
    window.ui.callback = {};

    window.ui.alert = _alert
    window.ui.show = _show;
    window.ui.prompt = $.messager.prompt;
    window.ui.confirm = $.messager.confirm;
    window.ui.prompt2 = $.messager.prompt2;     // 密码框

    window.ui.createTab = _createTab;
    window.ui.openTab = _openTab;
    window.ui.reloadTab = _reloadTab;
    window.ui.closeTab = _closeTab;
    window.ui.openWin = _openWin;
    window.ui.closeWin = _closeWin;
    window.ui.winSuccess = _winSuccess;
    window.ui.tabSuccess = _tabSuccess;
    window.ui.tabError = _tabError;
});