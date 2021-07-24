/**
* Validation library
* @author bery
* @version 1.0.1
* @namespace Validation
*/
(function () {
    if (!window['Validation']) {
        window['Validation'] = {};
    }

    // 主方法
    function validate(formId, submitId) {
        var submit = _getSubmit(formId, submitId);
        if (!submit) return false;

        var inputs = new Array(); 		// 文本框
        var lists = new Array(); 		// 选择框
        var textareas = new Array(); // 文本域

        // 如果初始化失败，则退出
        if (!_init()) return false;

        submit.onclick = function () {
            // 验证文本框
            for (var i = 0; i < inputs.length; i++) {
                var v = _getClassInfo(inputs[i]);
                if (!v)
                    continue;

                if (v.required && _trim(inputs[i].value) == "") {
                    _alert(v.messages.required);
                    inputs[i].focus();
                    submit.retVal = false;
                    return false;
                }
                else submit.retVal = true;

                // 如果是数字，则先过滤掉逗号
                if (v.number && isNaN(inputs[i].value.replace(/\,/g, ''))) {
                    _alert(v.messages.number);
                    inputs[i].focus();
                    submit.retVal = false;
                    return false;
                }
                else submit.retVal = true;
            }

            // 验证选择框
            for (var i = 0; i < lists.length; i++) {
                var v = _getClassInfo(lists[i]);
                if (!v)
                    continue;
                if (v.required && !lists[i].value) {
                    _alert(v.messages.required);
                    lists[i].focus();
                    submit.retVal = false;
                    return false;
                }
                else submit.retVal = true;
            }

            // 验证文本域
            for (var i = 0; i < textareas.length; i++) {
                var v = _getClassInfo(textareas[i]);
                if (!v)
                    continue;
                if (v.required && !textareas[i].value) {
                    _alert(v.messages.required);
                    textareas[i].focus();
                    submit.retVal = false;
                    return false;
                }
                else submit.retVal = true;
            }
        }

        function _init() {
            if (!document.getElementById(formId)) {
                return false;
            }
            var form = document.getElementById(formId);
            var elements = form.getElementsByTagName('INPUT');
            for (var i = 0; i < elements.length; i++) {
                if (elements[i].type == 'text') {
                    inputs.push(elements[i])
                }
            }
            lists = _getTagArray('SELECT');
            textareas = _getTagArray('TEXTAREA');
            return true;
        }

        function _getSubmit(formId, id) {
            if (!document.getElementById(formId)) {
                return false;
            }
            var form = document.getElementById(formId);
            var submit;
            if (id && document.getElementById(id)) {
                return document.getElementById(id);
            }
            else {
                var elements = form.getElementsByTagName('INPUT');
                for (var i = 0; i < elements.length; i++) {
                    if (elements[i].type == 'submit') {
                        if (elements[i].value.indexOf('@') > -1) {
                            continue;
                        }
                        if (elements[i].id.indexOf('Up') > -1) {
                            continue;
                        }
                        return elements[i];
                    }
                }
            }
        }
        submit.retVal = true;
    }
    window['Validation']['validate'] = validate;

    function _getClassInfo(element) {
        if (element.className.indexOf('{') == -1) {
            return null;
        }
        //var c = '[' + element.className + ']';
        var index = element.className.indexOf('{');
        var c = '[' + element.className.substring(index, element.className.length) + ']';
        var v = null;
        try {
            v = eval(c)[0];
        }
        catch (ex) {
            return null;
        }
        return v;
    }

    //根据标签名称获取所有包含className属性的元素
    function _getTagArray(tagName) {
        var tags = new Array();
        var elements = document.getElementsByTagName(tagName);
        for (var i = 0; i < elements.length; i++) {
            if (elements[i].className) {
                tags.push(elements[i])
            }
        }
        return tags;
    }

    //去掉空格
    function _trim(str) {
        return str.replace(/(^\s*)|(\s*$)/g, '');
    }

    window['Val'] = window['Validation'];


    function _alert(msg) {
        if (typeof top.ui.alert == 'undefined')
            alert(msg);
        else
            top.ui.alert(msg);
    }
})()

