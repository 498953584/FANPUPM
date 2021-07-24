// 给页面添加金额输入框控制
// 该文件依赖于jw.js
// Bery 2011-06-21 09:35
_addEvent(window, 'load', function () {
    var inputs = getElementsByClassName('decimal_input', '*');
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].nodeName == 'SPAN' || inputs[i].nodeName == 'TD') {
            inputs[i].innerHTML = _formatDecimal(inputs[i].innerHTML, 0);
        }
        else {
            inputs[i].value = _formatDecimal(inputs[i].value, 1);
            addDecimalInputEvent(inputs[i]);
        }
    }
});

// 添加事件
// 避免与公共方法冲突, 名称前添加下划线
function _addEvent(node, type, listener) {
    if (node.addEventListener) {
        //W3C
        node.addEventListener(type, listener, false);
        return true;
    } else if (node.attachEvent) {
        //MS
        node['e' + type + listener] = listener;
        node[type + listener] = function () { node['e' + type + listener](window.event); }
        node.attachEvent('on' + type, node[type + listener]);
        return true;
    }
    return false;
}

//根据ClassName返回元素数组
function getElementsByClassName(className, tag, parent) {
    parent = parent || document;
    var allTags = (tag == "*" && parent.all) ? parent.all : parent.getElementsByTagName(tag);
    var matchingElements = new Array();

    className = className.replace(/\-/g, '\\-');
    var regex = new RegExp('(^|\\s)' + className + '(\\s|$)');

    var element;
    for (var i = 0; i < allTags.length; i++) {
        element = allTags[i];
        if (regex.test(element.className)) {
            matchingElements.push(element);
        }
    }
    return matchingElements;
}

//格式小数
function toFixed(number, n) {
    if (n == undefined) {
        n = (typeof jw == 'undefined') ? 2 : jw.digitLen; // 小数位数
    }
    var numFormat;
    if (number.toFixed) {
        numFormat = number.toFixed(n);
    } else {
        var f3 = Math.pow(10, n);
        numFormat = Math.round(number * f3) / f3;
    }
    return numFormat;
}

//添加验证
function addDecimalInputEvent(e) {
    // 得到焦点
    _addEvent(e, 'focus', function () {
        e.value = e.value.replace(/\,/g, ''); 	// 过滤到分隔符
    })

    // 失去焦点
    _addEvent(e, 'blur', function () {
        e.value = e.value.replace(/\,/g, ''); 	// 过滤到分隔符
        var regex = /^[\+\-]?\d*?\.?\d*?$/;
        if (regex.test(e.value)) {
            if (e.value != '') {
                var number = new Number(e.value);
                e.value = toFixed(number);
            }
            else {
                e.value = toFixed(0.000000);
            }
        } else {
            e.value = toFixed(0.000000);
        }
        e.value = _formatDecimal(e.value, 1);
    });
}

// 将字符串转换成Decimal类型，并去掉分隔符
function _strToDecimal(str) {
    if (typeof str != 'string') str + '';
    return parseFloat(str.replace(/\,/g, ''));
}

// 格式化显示方式, type == 1表示需要截断处理
function _formatDecimal(decimalVal, type) {
    if (typeof decimalVal != 'string') decimalVal += '';
    // 去掉空格, 在ff下必须这样
    if (typeof jw != 'undefined') decimalVal = jw.trim(decimalVal);
    if (typeof Trim == 'function') decimalVal = Trim(decimalVal);

    var symbol = ''; 												// 正负号
    if (decimalVal[0] == '-') {
        symbol = '-';
        decimalVal = decimalVal.substr(1, decimalVal.length); 	// 去掉负号
    }
    var digitLen = (typeof jw == 'undefined') ? 2 : jw.digitLen; // 小数位数
    if (!decimalVal) return ''; 									// 检查是否输入
    if (decimalVal.indexOf(',') > -1) decimalVal = decimalVal.replace(/\,/g, ''); 	// 过滤到分隔符
    var pos = decimalVal.indexOf('.'); 								// 小数点位置
    if (pos == -1) pos = decimalVal.length;

    var intVal = decimalVal.substring(0, pos); 						// 整数部分
    var degintVal = decimalVal.substr(pos + 1) || '000000'; 		// 小数部分

    degintVal = degintVal.substr(0, digitLen); 						// 格式骨干小数部分
    // 检查输入是否过大
    if (intVal.length > 11 && type == 1) {
        alert('请检查您的输入是否过大！');
        intVal = intVal.substr(0, 11);
    }

    // 每3位存到数组中
    var arr = new Array();
    for (var i = intVal.length; i--; i > 0) {
        if ((intVal.length - i) % 3 == 0) {
            arr.push(intVal.substr(i, 3));
        }
    }

    // 前面不足3位的部分
    var firstPart = intVal.substr(0, intVal.length - 3 * arr.length);
    if (firstPart.length > 0) {
        arr.push(firstPart);
    }

    // 初始化为正负号
    var value = symbol;
    for (var i = arr.length; i--; i > 0) {
        value += arr[i] + ',';
    }

    return value.substr(0, value.length - 1) + '.' + degintVal;
}