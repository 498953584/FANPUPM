
(function () {
	window['jw'] = {};
	window['jw']['downloadUrl'] = '/Common/DownLoad.aspx';

	//项目状态
	window['jw']['ProjectParameter'] = {};
	window['jw']['ProjectParameter']['PreApproval'] = '1';  //预立项
	window['jw']['ProjectParameter']['Approval'] = '2';     //立项
	window['jw']['ProjectParameter']['Initiate'] = '3';     //启动
	window['jw']['ProjectParameter']['Bid'] = '4';          //投标
	window['jw']['ProjectParameter']['WinBid'] = '5';       //中标
	window['jw']['ProjectParameter']['OutBid'] = '6';       //落标
	window['jw']['ProjectParameter']['Progress'] = '7';     //在建
	window['jw']['ProjectParameter']['Shutdown'] = '8';     //停工
	window['jw']['ProjectParameter']['Check'] = '9';        //验收
	window['jw']['ProjectParameter']['Completed'] = '10';  //竣工
	window['jw']['ProjectParameter']['Inlife'] = '11';     //保内
	window['jw']['ProjectParameter']['Outlife'] = '12';           //保外
	window['jw']['ProjectParameter']['Relieve'] = '13';           //解除
	window['jw']['ProjectParameter']['Prequalification'] = '14';  //资格预审
	window['jw']['ProjectParameter']['QualificationPass'] = '15';  //预审通过
	window['jw']['ProjectParameter']['QualificationFail'] = '16';  //预审失败
	window['jw']['ProjectParameter']['GiveUpState'] = '18';    //放弃
	window['jw']['ProjectParameter']['InitiateFail'] = '19';   //报名不通过

	window['jw']['digitLen'] = 3; 		// 小数位数
	window['jw']['poplife'] = 120000; 	// 120秒 冒泡提醒显示时间，单位（毫秒）

	function trim(str) {
		if (str == null)
			return '';
		else
			return str.replace(/(^\s*)|(\s*$)/g, '');
	}
	window['jw']['trim'] = trim;

	// 模拟C#中的string.format函数
	function format() {
		if (arguments.length == 0)
			return null;

		var str = arguments[0];
		for (var i = 1; i < arguments.length; i++) {
			var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
			str = str.replace(re, arguments[i]);
		}
		return str;
	}
	window['jw']['format'] = format;

	//查找元素在数组中的位置
	function indexOfInArray(e, arr) {
		for (var i = 0; i < arr.length; i++) {
			if (e == arr[i]) {
				return i;
			}
		}
		return -1;
	}
	window['jw']['indexOfInArray'] = indexOfInArray;


	//数组转化为Json
	function array1dToJson(a, p) {
		var i, s = '[';
		for (i = 0; i < a.length; ++i) {
			if (a[i]) {
				if (typeof a[i] == 'string') {
					s += '"' + a[i] + '"';
				} else { // assume number type
					s += a[i];
				}
				if (i < a.length - 1) {
					s += ',';
				}
			}
		}
		s += ']';
		if (p) {
			return '{"' + p + '":' + s + '}';
		}
		return s;
	}
	window['jw']['array1dToJson'] = array1dToJson;

	// 数组转化为Json
	function array2dToJson(a, p, nl) {
		var i, j, s = '{"' + p + '":[';
		nl = nl || '';
		for (i = 0; i < a.length; ++i) {
			s += nl + array1dToJson(a[i]);
			if (i < a.length - 1) {
				s += ',';
			}
		}
		s += nl + ']}';
		return s;
	}
	window['jw']['array2dToJson'] = array2dToJson;

	// 数组转化为CSV格式
	function arrayToCsv(arr) {
		var csv = '';
		for (var i = 0; i < arr.length; i++) {
			csv = csv + ',' + arr[i];
		}
		if (csv.length > 0) {
			csv = csv.substring(1);
		}
		return csv;
	}
	window['jw']['arrayToCsv'] = arrayToCsv;

	//生成GUID
	function newGuid() {
		var guid = "";
		for (var i = 1; i <= 32; i++) {
			var n = Math.floor(Math.random() * 16.0).toString(16);
			guid += n;
			if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
				guid += "-";
		}
		return guid;
	}
	window['jw']['newGuid'] = newGuid;

	// 判断对象是否数组
	function isArray(o) {
		return Object.prototype.toString.call(o) == '[object Array]';
	}
	window['jw']['isArray'] = isArray;

	// 判断对象是否函数
	function isFunction(o) {
		return Object.prototype.toString.call(o) == '[object Function]';
	}
	window['jw']['isFunction'] = isFunction;

	// 判断对象是否正则
	function isRegExp(o) {
		return Object.prototype.toString.call(o) == '[object RegExp]';
	}
	window['jw']['isRegExp'] = isRegExp;

	// 判断浏览器是否为iPad
	function ipad() {
		var ua = navigator.userAgent.toLowerCase();
		if (ua.match(/iPad/i) == "ipad") {
			return true;
		} else {
			return false;
		}
	}
	window['jw']['ipad'] = ipad;

	// 判断浏览器
	function msie() {
		var ua = navigator.userAgent.toLowerCase();
		return /msie/.test(ua);
	}
	window['jw']['msie'] = msie;



	// alert, 废弃
	function _alert(title, msg) {
		if (_isContainsEasyUI()) {
			// 使用EasyUI
			$.messager.alert(title, msg);
		}
		else {
			// 使用window
			alert(msg);
		}
	}
	window['jw']['alert'] = _alert;


	// 同步确认框
	// 两种参数传递方式:
	//      1. top.ui.confirm('系统提示', '确认删除？', 'btnDel');
	//      2. top.ui.confirm({ title: '系统提示', msg: '确认删除？', id: 'btnDel' });
	// 第2种方式中的title和msg有默认值, 分别为 '系统提示' 和 '确认删除？'
	function _confirm(title, msg, id) {
		if (arguments.length == 1 && typeof arguments[0] == 'object') {
			var options = arguments[0];
			title = options.title || '系统提示';
			msg = options.msg || '确认删除？';
			id = options.id;
		}

		if (_isContainsEasyUI() && _isContainsDoPostBack()) {
			// 使用EasyUI
			$.messager.confirm(title, msg, function (r) {
				if (r) { __doPostBack(id, ''); }
			});
			return false;
		}
		else {
			// 使用window
			return confirm(msg);
		}
	}
	window['jw']['confirm'] = _confirm;

	// show
	function _show(title, msg) {
		if (_isContainsEasyUI()) {
			// 使用EasyUI
			$.messager.show({
				title: title,
				msg: msg,
				timeout: 6000,
				showType: 'slide'
			});
		}
		else {
			// 使用window
			alert(msg);
		}
	}
	window['jw']['show'] = _show;

	// 转换为大写金额
	function amountInWords(num) {
		if (num.length == 0) return '';
		num = num.replace(/\,/g, '');
		var strOutput = "";
		var strUnit = '仟佰拾亿仟佰拾万仟佰拾元角分';
		num += "00";
		var intPos = num.indexOf('.');
		if (intPos >= 0)
			num = num.substring(0, intPos) + num.substr(intPos + 1, 2);
		strUnit = strUnit.substr(strUnit.length - num.length);
		for (var i = 0; i < num.length; i++)
			strOutput += '零壹贰叁肆伍陆柒捌玖'.substr(num.substr(i, 1), 1) + strUnit.substr(i, 1);
		return strOutput.replace(/零角零分$/, '整').replace(/零[仟佰拾]/g, '零').replace(/零{2,}/g, '零')
			.replace(/零([亿|万])/g, '$1').replace(/零+元/, '元').replace(/亿零{0,3}万/, '亿').replace(/^元/, "零元");
	}
	window['jw']['amountInWords'] = amountInWords;

	// 格式化Json序列化后的时间格式
	// 例如：jsonToDate('Date(1353579842000+0800)', 'yyyy-MM-dd hh:mm:ss')
	function jsonToDate(json, format) {
		if (!json) return '';
		format = format || 'yyyy-MM-dd';
		var re = /-?\d+/;
		var m = re.exec(json);
		var d = new Date(parseInt(m[0]));
		var o = {
			"M+": d.getMonth() + 1, //month
			"d+": d.getDate(),    //day
			"h+": d.getHours(),   //hour
			"m+": d.getMinutes(), //minute
			"s+": d.getSeconds(), //second
			"q+": Math.floor((d.getMonth() + 3) / 3),  //quarter
			"S": d.getMilliseconds() //millisecond
		}
		if (/(y+)/.test(format))
			format = format.replace(RegExp.$1, (d.getFullYear() + "").substr(4 - RegExp.$1.length));
		for (var k in o) if (new RegExp("(" + k + ")").test(format))
			format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
		return format;
	}
	window['jw']['jsonToDate'] = jsonToDate;

	// 字符串转换为日期
	function strToDate(str, sep) {
		sep = sep || '-';
		var arr = str.split(sep);
		if (arr.length < 3) return;
		var d = new Date(arr[0], arr[1], arr[2]);
		return d;
	}
	window['jw']['strToDate'] = strToDate;

	// 日期转换为字符串
	function dateToStr(date, sep) {
		sep = sep || '-';
		return date.getFullYear() + sep + date.getMonth() + '-' + date.getDate();
	}
	window['jw']['dateToStr'] = dateToStr;

	// 返回指定日期加上几天后的日期
	function addDay(date, d) {
		var time = date.getTime();
		// 毫秒
		var step = d * 24 * 3600 * 1000;
		return new Date(time + step);
	}
	window['jw']['addDay'] = addDay;

	// 添加样式表, 例如:addStyle('css/add.css')
	function addStyle(stylePath) {
		var container = document.getElementsByTagName("head")[0];
		var addStyle = document.createElement("link");
		addStyle.rel = "stylesheet";
		addStyle.type = "text/css";
		addStyle.media = "screen";
		addStyle.href = stylePath;
		container.appendChild(addStyle);
	}
	window['jw']['addStyle'] = addStyle;

	// 关闭弹出窗口
	// 废弃
	function closeWin() {
		if (top._closeWin) {
			top._closeWin();
		} else {
			window.returnValue = true;
			window.close();
		}
	}
	window['jw']['closeWin'] = closeWin;

	// 刷新标签页
	// 废弃
	function reloadTab() {
		if (top._reloadTab) {
			top._reloadTab();
		}
	}
	window['jw']['reloadTab'] = reloadTab;

	// 获取GET请求参数
	function getRequestParam(argname, url) {
		url = url || window.location.href;
		var arrStr = url.substring(url.indexOf('?') + 1).split('&');
		for (var i = 0; i < arrStr.length; i++) {
			var loc = arrStr[i].indexOf(argname + '=');
			if (loc != -1) {
				return arrStr[i].replace(argname + '=', '').replace('?', '');
				break;
			}
		}
		return '';
	}
	window['jw']['getRequestParam'] = getRequestParam;

	// 获取节点的第一个指定nodeName的父节点
	function getParentUntil(elem, falg) {
		while (elem.parentNode) {
			elem = elem.parentNode;
			if (elem.nodeName.toUpperCase() == falg.toUpperCase()) {
				return elem;
			}
		}
		return elem;
	}
	window['jw']['getParentUntil'] = getParentUntil;

	// 修改参数
	function modifyParam(options) {
		var url = options.url || window.location.href;
		var pattern = options.name + '=([^&]*)';
		var replaceText = options.name + '=' + options.value;

		var m = url.match(pattern);
		if (m && m.length > 0) {
			tmp = url.replace(m[0], replaceText);
			return tmp;
		}
		else {
			if (url.match('[\?]')) {
				return url + '&' + replaceText;
			}
			else {
				return url + '?' + replaceText;
			}
		}

		return url + '\n' + options.name + '\n' + options.value;
	}
	window['jw']['modifyParam'] = modifyParam;


	// 构造树形表格
	function formatTreegrid(tableid, index) {
		var $trs = $('#' + tableid).find('tr[layer]');
		var minLayer = $trs.eq(0).attr('layer');

		$trs.each(function () {
			var layer = $(this).attr('layer');
			var $td = $(this).find('td').eq(index);
			var blank = '';
			for (var i = minLayer; i < layer; i++) {
				blank += '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;';
			}
			$td.html(blank + $td.html());
		});
	}
	window['jw']['formatTreegrid'] = formatTreegrid;


	// jw.tooltip 
	// requrire: jQuery, tooltip
	function tooltip() {
		$('.tooltip').each(function () {
			// 如果内容和title不一样,才需要鼠标提示
			if (jw.trim($(this).text()) != $(this).attr('title')) {
				$(this).tooltip({
					track: true,
					delay: 0,
					showURL: false,
					fade: true,
					showBody: " - ",
					extraClass: "solid 1px blue",
					fixPNG: true
				});
			} else {
				$(this).attr('title', '');
			}
		});
	}
	window['jw']['tooltip'] = tooltip;

	// 单选人员
	function selectOneUser(option) {
		option = option || {};
		var url = '/EPC/Workflow/SelectUser.aspx';
		top.ui.openWin({ title: '选择人员', url: url });
		top.ui.callback = function (user) {
			if (option.codeinput) {
				$('#' + option.codeinput).val(user.code);
			}
			if (option.nameinput) {
				$('#' + option.nameinput).val(user.name);
			}
			if (typeof option.func == 'function') {
				option.func();
			}
		}
	}
	window['jw']['selectOneUser'] = selectOneUser;

	// 多选人员
	function selectMultiUser(option) {
		option = option || {};
		var url = '/EPC/Workflow/SelectAllUser.aspx';
		if (option.idcsv) {
			url += '?idcsv=' + option.idcsv;
		}
		top.ui.openWin({ title: '选择人员', url: url });
		top.ui.callback = function (user) {
			if (option.codeinput) {
				$('#' + option.codeinput).val(user.code);
			}
			if (option.nameinput) {
				$('#' + option.nameinput).val(user.name);
			}
			if (typeof option.func == 'function') {
				option.func();
			}
		}
	}
	window['jw']['selectMultiUser'] = selectMultiUser;

	// 单选部门
	function selectOneDep(option) {
		option = option || {};
		var url = '/CommonWindow/DeptSing.aspx';
		top.ui.callback = function (d) {
			if (option.nameinput)
				$('#' + option.nameinput).val(d.name);

			if (option.idinput)
				$('#' + option.idinput).val(d.id);
		}
		top.ui.openWin({ title: '选择部门', url: url, width: 360, height: 500 });
	}
	window['jw']['selectOneDep'] = selectOneDep;

	// 单选往来单位
	function selectOneCorp(option) {
		option = option || {};
		var url = '/Common/SelectOneCorp.aspx';
		var winNo = option.winNo || 1;

		if (winNo == 2 && typeof top.ui.callback == 'function') {
			top.ui._callback = top.ui.callback;
		}

		top.ui.callback = function (corp) {
			if (option.nameinput)
				$('#' + option.nameinput).val(corp.name);

			if (option.idinput)
				$('#' + option.idinput).val(corp.id);

			if (typeof option.func == 'function')
				option.func(corp);

			if (winNo == 2 && typeof top.ui._callback == 'function')
				top.ui.callback = top.ui._callback;
		}
		top.ui.openWin({ title: '选择往来单位', url: url, winNo: winNo });
	}
	window['jw']['selectOneCorp'] = selectOneCorp;

	// 单选项目 
	function selectOneProject(option) {
		option = option || {};
		var url = '/Common/DivSelectProject2.aspx';
		var winNo = option.winNo || 1;

		var _callback = null;
		if (winNo == 2) {
			_callback = top.ui.callback;
		}

		top.ui.callback = function (prj) {
			if (option.nameinput)
				$('#' + option.nameinput).val(prj.prjName);

			if (option.idinput)
				$('#' + option.idinput).val(prj.prjId);

			if (typeof option.func == 'function')
				option.func(prj);

			if (winNo == 2)
				top.ui.callback = _callback;
		}
		top.ui.openWin({ title: '选择项目', url: url, winNo: winNo, width: 700 });
	}
	window['jw']['selectOneProject'] = selectOneProject;

	// 单选合同类型
	function selectOneConType(option) {
		option = option || {};
		var url = '/ContractManage/UserControl/ContractTypeNew.aspx';
		top.ui.callback = function (t) {
			if (option.nameinput)
				$('#' + option.nameinput).val(t.name);

			if (option.idinput)
				$('#' + option.idinput).val(t.id);

			if (typeof option.func == 'function')
				option.func(t);
		}
		top.ui.openWin({ title: '选择合同类型', url: url, width: 700 });
	}
	window['jw']['selectOneConType'] = selectOneConType;

	// 选择收入合同
	function selectInCon(option) {
		option = option || {};
		var url = "/Common/DivSelectIncometCont.aspx";
		top.ui.callback = function (c) {
			if (option.idinput)
				$('#' + option.idinput).val(c.id);
			if (option.nameinput)
				$('#' + option.nameinput).val(c.name);
			if (typeof option.func == 'function')
				option.func(c);
		}
		top.ui.openWin({ title: '选择收入合同', url: url });
	}
	window['jw']['selectInCon'] = selectInCon;

	// 选择支出合同
	function selectOutCon(option) {
		option = option || {};
		var url = "/ContractManage/UserControl/PayoutContract.aspx";
		top.ui.callback = function (c) {
			if (option.idinput)
				$('#' + option.idinput).val(c.id);
			if (option.nameinput)
				$('#' + option.nameinput).val(c.name);
			if (typeof option.func == 'function')
				option.func(c);
		}
		top.ui.openWin({ title: '选择支出合同', url: url });
	}
	window['jw']['selectOutCon'] = selectOutCon;

	// 选择仓库
	function selectTreasury(option) {
		option = option || {};
		// type=2 表示不是采用的用户控件
		var url = "/Common/SelectTreasury.aspx";
		// 添加参数
		if (option.module) {
			url += '?module=' + option.module;
		}
		top.ui.callback = function (c) {
			if (option.codeinput)
				$('#' + option.codeinput).val(c.code);
			if (option.nameinput)
				$('#' + option.nameinput).val(c.name);
			if (typeof option.func == 'function')
				option.func(c);
		}
		top.ui.openWin({ title: '选择仓库', url: url });
	}
	window['jw']['selectTreasury'] = selectTreasury;

	// 执行SQL语句并返回结果
	function runSql(opt) {
		var opt = opt || {};
		var id = opt.id || '';
		var param = opt.param || '';
		var url = '/Handler/RunSql.ashx?id=' + id + param;

		var ret = '';

		$.ajax({
			type: 'GET',
			async: false,
			cache: false,
			url: url,
			success: function (data) { ret = data }
		});

		return ret;
	}
	window['jw']['runSql'] = runSql;

	//    // 在线编辑器，依赖于 jquery.wysiwyg.js 和 jquery.wysiwyg.css
	//    function wysiwyg(expr) {
	//        try {
	//            $(expr).wysiwyg({
	//                controls: {
	//                    undo: { visible: false },
	//                    redo: { visible: false },
	//                    separator04: { visible: false },

	//                    h4mozilla: { visible: false && $.browser.mozilla, className: 'h4', command: 'heading', arguments: ['h4'], tags: ['h4'], tooltip: "Header 4" },
	//                    h5mozilla: { visible: false && $.browser.mozilla, className: 'h5', command: 'heading', arguments: ['h5'], tags: ['h5'], tooltip: "Header 5" },
	//                    h6mozilla: { visible: false && $.browser.mozilla, className: 'h6', command: 'heading', arguments: ['h6'], tags: ['h6'], tooltip: "Header 6" },

	//                    h4: { visible: false && !($.browser.mozilla), className: 'h4', command: 'formatBlock', arguments: ['<H4>'], tags: ['h4'], tooltip: "Header 4" },
	//                    h5: { visible: false && !($.browser.mozilla), className: 'h5', command: 'formatBlock', arguments: ['<H5>'], tags: ['h5'], tooltip: "Header 5" },
	//                    h6: { visible: false && !($.browser.mozilla), className: 'h6', command: 'formatBlock', arguments: ['<H6>'], tags: ['h6'], tooltip: "Header 6" },

	//                    separator05: { visible: false },
	//                    separator06: { visible: false },
	//                    separator07: { visible: false },

	//                    cut: { visible: false },
	//                    copy: { visible: false },
	//                    insertImage: { visible: false },
	//                    paste: { visible: false }
	//                }
	//            });
	//        } catch (ex) { }
	//    }
	//    window['jw']['wysiwyg'] = wysiwyg;


	// 是否包含jQuery
	function _isContainsjQuery() {
		return (typeof jQuery != 'undefined');
	}

	// 是否包含EasyUI
	function _isContainsEasyUI() {
		return _isContainsjQuery() && (typeof jQuery.messager != 'undefined');
	}

	// 是否包含__doPostBack
	function _isContainsDoPostBack() {
		return (typeof __doPostBack != 'undefined');
	}
	window['jw']['isContainsDoPostBack'] = _isContainsDoPostBack;

	function preventSubmit2(id) {
		document.getElementById(id).setAttribute('disabled', 'disabled');
		if (_isContainsDoPostBack())
			__doPostBack(id, '');
	}
	window['jw']['preventSubmit2'] = preventSubmit2;

	// 加载后执行
	$(document).ready(function () {
		// 控制选择框的样式
		var $img = $('<img class="icon_17" src="/images/icon_17.bmp" />');
		$img.css('position', 'absolute').css('cursor', 'pointer');

		createImg();

		$('div').scroll(createImg);
		$(window).resize(createImg);

		// 创建图片
		function createImg() {
			$('.icon_17').remove();
			$('.select_input').each(function () {
				addImg($img.clone(), this);
			});
		}

		// 添加图片
		function addImg($img, input) {
			var top = $(input).offset().top + 1;
			var left = $(input).offset().left + $(input).width() - 16;
			if (msie()) left += 2;
			if (ipad()) { left += 12; top += 2; }
			var funcName = $(input).attr('imgclick');
			$img.offset({ top: top, left: left });
			$img.click(function () {
				window[funcName]();
			});
			$('body').append($img);
		}
	});
})();
