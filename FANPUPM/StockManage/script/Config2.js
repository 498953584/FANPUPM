// Config2.js
// 依赖于jQuery.js， 部分方法依赖于tootip和jQuery UI
var width = 600;
var height = 456;

// 是否包含jQuery
function _isContainsjQuery() {
	return (typeof jQuery != 'undefined');
}

function winopen(url, options) {
	window.open(url, "", "height=" + height + ", width=" + width +
        ", toolbar =no, menubar=no, scrollbars=no, resizable=no, location=no, status=no");
}

function viewopen(url, newwidth, newheight) {
	var width = isNaN(newwidth) ? 600 : new Number(newwidth);
	var height = isNaN(newheight) ? 456 : new Number(newheight);

	//isNaN('') == false
	width = (width == 0 ? 600 : width);
	height = (height == 0 ? 456 : height);
	window.open(url, "", "height=" + height + ", width=" + width +
        ", toolbar =no, menubar=no, scrollbars=yes, resizable=yes, location=no, status=no");
}

// 关闭方法
function winclose(tobj, url, isRefrsh) {
	if (typeof top.ui != 'undefined' &&
        typeof top.ui.version != 'undefined' &&
        top.ui.version == '2') {
		// 新界面
		if (isRefrsh) {
			top.desktop[tobj].location = url;
		}
		top.ui.closeTab();
	} else {
		// 老界面
		try {
			if (isRefrsh) {
				parent.desktop[tobj].location = url;
			};
			parent.desktop[tobj] = null;
			top.frameWorkArea.window.desktop.getActive().close();
		} catch (ex) {
			window.close();
		}
	}
}

// 根据类名查找元素
function getElementsByClassName(className, tag, parent) {
	parent = parent || document;
	tag = tag || "*";
	// Locate all the matching tags
	var allTags = parent.getElementsByTagName(tag);
	var matchingElements = new Array();

	// Create a regular expression to determine if the className is correct
	className = className.replace(/\-/g, "\\-");
	var regex = new RegExp("(^|\\s)" + className + "(\\s|$)");
	var element;
	// Check each element
	for (var i = 0; i < allTags.length; i++) {
		element = allTags[i];
		if (regex.test(element.className)) {
			matchingElements.push(element);
		}
	}

	// Return any matching elements
	return matchingElements;
};

//注册事件
function addEvent(node, type, listener) {
	// 如果页面包含jQuery,则使用jQuery的绑定方法
	// 解决事件绑定顺序的bug
	if (_isContainsjQuery()) {
		$(node).bind(type, listener);
		return true;
	}

	if (node != null) {
		if (node.addEventListener) {
			// W3C method
			node.addEventListener(type, listener, false);
			return true;
		}
		else
			if (node.attachEvent) {
				// MSIE method
				node['e' + type + listener] = listener;
				node[type + listener] = function () {
					node['e' + type + listener](window.event);
				}
				node.attachEvent('on' + type, node[type + listener]);
				return true;
			}
	}
	return false;
};

// 添加加载事件
function addLoadEvent(func) {
	var oldonload = window.onload;
	if (typeof (oldonload) != 'function') {
		window.onload = func;
	}
	else {
		oldonload;
		func();
	}
}

//打印
function winPrint() {
	window.print();
}


//获取RadioButtonList选中项
function getRadioButtonListValue(id) {
	var rbl = document.getElementById(id);
	if (!rbl)
		return null;
	var inputs = rbl.getElementsByTagName('INPUT');
	var value;
	var text;
	for (var i = 0; i < inputs.length; i++) {
		if (inputs[i].checked) {
			value = inputs[i].value;
			text = inputs[i].nextSibling.firstChild.nodeValue;
			return {
				'value': value,
				'text': text
			};
		}
	}
	return null;
}

//获取GET请求参数
function getRequestParam(argname, url) {
	url = url || document.location.href;
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


//设置所有的输入框为Disabled
function setAllInputDisabled() {
	var elements = document.getElementsByTagName('*');
	for (var i = 0; i < elements.length; i++) {
		if (elements[i].nodeName == 'INPUT') {
			if (elements[i].id == 'btnCancel') {
				continue;
			}
			if (elements[i].id == 'btnDy') {
				continue;
			}
			if (elements[i].id.indexOf('Btn_Upload') > -1) {
				elements[i].value = "查看附件";
				try {
					var str = elements[i].onclick.toString();
					var index = str.indexOf('(\'') + 2;
					var param1 = str.substring(index, index + 36);
					var param2 = str.substring(index + 38, str.indexOf(',\''));
					elements[i].onclick = function () {
						viewAnnex(param1, param2);
					}
				}
				catch (e) {
				}
				continue;
			}
			elements[i].setAttribute('disabled', 'disabled');
			setDisabled(elements[i]);
		}
		if (elements[i].nodeName == 'TEXTAREA') {
			setReadOnly(elements[i]);
		}
		if (elements[i].nodeName == 'IMG') {
			setDisabled(elements[i]);
		}
		if (elements[i].nodeName == 'OPTION') {
			setDisabled(elements[i]);
		}
		if (elements[i].nodeName == 'SELECT') {
			setDisabled(elements[i]);
		}
		if (elements[i].nodeName == 'SPAN') {
			if (elements[i].className == 'alarm') {
				continue;
			}
			if (elements[i].id == 'lblTitle') {
				//查看页面的标题
				continue;
			}
			setDisabled(elements[i]);
		}
		//控制背景色
		function setDisabled(elem) {
			elem.style.backgroundColor = 'white';
			elem.setAttribute('disabled', 'disabled');
		}

		function setReadOnly(elem) {
			elem.style.backgroundColor = 'white';
			elem.setAttribute('readOnly', 'readOnly');
		}
	}
}

//阻止冒泡
function stopPropagation(eventObject) {
	eventObject = eventObject || getEventObject(eventObject);
	if (eventObject.stopPropagation) {
		eventObject.stopPropagation();
	}
	else {
		eventObject.cancelBubble = true;
	}
}

function getEventObject(W3CEvent) {
	return W3CEvent || window.event;
}

//获取节点的第一个指定nodeName的父节点
function getFirstParentElement(elem, falg) {
	while (elem.parentNode) {
		elem = elem.parentNode;
		if (elem.nodeName.toUpperCase() == falg.toUpperCase()) {
			return elem;
		}
	}
	return elem;
}

//取消默认动作并阻止冒泡事件
function preventEvent(e) {
	var ev = e || window.event;
	if (ev.preventDefault) ev.preventDefault();
	else ev.returnValue = false;
	if (ev.stopPropagation)
		ev.stopPropagation();
	return false;
}


//去掉空格
function Trim(str) {
	return str.replace(/(^\s*)|(\s*$)/g, '');
}

// 返回指定table中被选中的行Id，需要jQuery
function getCheckedIdJson(tableId) {
	var idArr = new Array();
	$('#' + tableId + ' input:checked').each(function () {
		var tr = getFirstParentElement(this, 'TR');
		if (tr.id) {
			idArr.push(tr.id);
		}
	});

	return jw.array1dToJson(idArr);
}


//设置附件为只读
function setAnnxReadOnly(id) {
	document.getElementById(id + '_But_UpFile').style.display = 'none';
	document.getElementById(id + '_Btn_Upload').style.display = 'none';
}


//获取Cookie
function getCookie(name) {
	var start = document.cookie.indexOf(name + "=");
	var len = start + name.length + 1;
	if ((!start) && (name != document.cookie.substring(0, name.length))) {
		return null;
	}
	if (start == -1) return null;
	var end = document.cookie.indexOf(';', len);
	if (end == -1) end = document.cookie.length;
	return unescape(document.cookie.substring(len, end));
}

//设置Cookie
function setCookie(name, value, expires, path, domain, secure) {
	var today = new Date();
	today.setTime(today.getTime());
	if (expires) {
		expires = expires * 1000 * 60 * 60 * 24;
	}
	var expires_date = new Date(today.getTime() + (expires));
	document.cookie = name + '=' + escape(value) +
		((expires) ? ';expires=' + expires_date.toGMTString() : '') + //expires.toGMTString()
		((path) ? ';path=' + path : '') +
		((domain) ? ';domain=' + domain : '') +
		((secure) ? ';secure' : '');
}

//删除Cookie
function deleteCookie(name, path, domain) {
	if (getCookie(name)) document.cookie = name + '=' +
			((path) ? ';path=' + path : '') +
			((domain) ? ';domain=' + domain : '') +
			';expires=Thu, 01-Jan-1970 00:00:01 GMT';
}


// 设置按钮 jwTable:GridView对象,btnDel:删除按钮ID,
// btnUpdate:更新按钮ID,btnQuery:查询ID,hdChkID:隐藏字段ID
function setButton(jwTable, btnDel, btnUpdate, btnQuery, hdChkId) {
	if (!jwTable.table) return;
	if (jwTable.table.firstChild.childNodes.length == 1) {
		//table中没有数据
		return;
	}
	jwTable.registClickTrListener(function () {
		if (hdChkId != '')
			document.getElementById(hdChkId).value = this.id;
		if (this.getAttribute('guid') && document.getElementById('btnQuery')) {
			document.getElementById(btnQuery).guid = this.getAttribute('guid');
		}
		if (this.id) {
			enabledInput(btnDel);
			enabledInput(btnUpdate);
			enabledInput(btnQuery);
		}

		removeDisabled(this.id, btnUpdate, btnDel);
		upAdminPrivilege();
	});

	jwTable.registClickSingleCHKListener(function () {
		var checkedChk = jwTable.getCheckedChk();
		if (checkedChk.length == 0) {
			disabledInput(btnDel);
			disabledInput(btnUpdate);
			disabledInput(btnQuery);
		}
		else
			if (checkedChk.length == 1) {
				if (hdChkId != '')
					document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);

				enabledInput(btnDel);
				enabledInput(btnUpdate);
				enabledInput(btnQuery);

				//被选中的唯一行的id
				var tr = getTrByChk(checkedChk[0]);
				removeDisabled(tr.id, btnUpdate, btnDel);
				if (tr.getAttribute('guid')) {
					if (document.getElementById(btnQuery))
						document.getElementById(btnQuery).guid = tr.getAttribute('guid');
				}
			}
			else {
				if (hdChkId != '')
					document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);

				enabledInput(btnDel);
				disabledInput(btnUpdate);
				disabledInput(btnQuery);

				var checkedChks = jwTable.getCheckedChk();
				setBtnDeleteState(btnDel, checkedChks);
			}
	});
	jwTable.registClickAllCHKLitener(function () {
		if (this.checked) {
			if (hdChkId != '')
				document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();

			enabledInput(btnDel);
			var checkedChks = jwTable.getAllChk();
			setBtnDeleteState(btnDel, checkedChks);
		}
		else {
			if (hdChkId != '')
				document.getElementById(hdChkId).value = '';
			disabledInput(btnDel);
		}

		disabledInput(btnUpdate);
		disabledInput(btnQuery);
		if (jwTable.table.rows.length == 2 && this.checked == true) {
			if (hdChkId != '')
				document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
			enabledInput(btnDel);
			enabledInput(btnUpdate);
			enabledInput(btnQuery);
			removeDisabled(jwTable.table.rows[1].id, btnUpdate, btnDel);
		}
	});

	function setBtnDeleteState(btnDel, chks) {
		var flowstateIndex = getFlowStateIndex(jwTable);
		if (flowstateIndex > 0) {
			for (var i = 0; i < chks.length; i++) {
				var tr = getTrByChk(chks[i]);
				var state = getFlowState.call(tr, jwTable);
				if (state != '未提交') {
					disabledInput(btnDel);
				}
			}
		}

	}

	function removeDisabled(trId, btnUpdate, btnDel) {
		//Modify Zhang FuJun 2011-8-19
		if (trId == '') return;
		//End
		//根据流程状态state 控制修改和删除按钮
		var spans = $('#' + trId).find('span');
		var state = '-1';
		for (var i = 0; i < spans.length; i++) {
			if ($(spans[i]).attr('state') != undefined) {
				state = $(spans[i]).attr('state');
				break;
			}
		}
		if (state == '1' || state == '0' || state == '-2') {
			disabledInput(btnUpdate);
		}
		if (state != '-1') {
			disabledInput(btnDel);
		}
	}

	function getTrByChk(chk) {
		if (chk.parentNode.parentNode.nodeName == 'TR') {
			return chk.parentNode.parentNode;
		}
		if (chk.parentNode.parentNode.parentNode.nodeName == 'TR') {
			return chk.parentNode.parentNode.parentNode;
		}
		return null;
	}
}


//获取当前行的流程状态
function getFlowState(jwTable) {
	var index = getFlowStateIndex(jwTable);
	var state = null;
	if (this.childNodes[index].firstChild.firstChild) {
		state = this.childNodes[index].firstChild.firstChild.nodeValue;
	}
	else
		if (this.childNodes[index].firstChild) {
			state = this.childNodes[index].firstChild.nodeValue;
		}
	return state;
}

//获取审核流程在Table中列的索引
function getFlowStateIndex(jwTable) {
	//获得第一行的所有td
	var tds = $($(jwTable.table).find('tr')[0]).find('td');
	if (tds.length == 0) {
		tds = $($(jwTable.table).find('tr')[0]).find('th');
	}
	for (var i = 1; i < tds.length; i++) {
		///2011年9月29日8:43:23
		///增加了对 tds[i].firstChild.nodeValue 值不能为null 的判断
		///修改之前的原因是 在资料归档时  增加的是归档按钮 到这里会寻找流程状态信息
		///修改它不为空的情况则能
		if (tds[i].firstChild.nodeValue != null) {
			if (tds[i].firstChild.nodeValue.indexOf('流程状态') >= 0) {
				return i;
			}
			if (tds[i].firstChild.nodeValue.indexOf('流程状态') >= 0) {
				return i;
			}
		}
	}
	return 0;
}

//获取审核流程在Table中列的索引
function getFlowIndex(jwTable, strState) {
	var tds = jwTable.table.firstChild.firstChild.childNodes;
	for (var i = 1; i < tds.length; i++) {
		if (tds[i].firstChild.nodeValue.indexOf(strState) >= 0) {
			return i;
		}
	}
	return 0;
}

//格式化Table，显示父子结构
function formateTable(tableId, index, ignoreFoot) {
	try {
		ignoreFoot = ignoreFoot || false;
		var table = document.getElementById(tableId);
		if (!table) return false;
		var trs = table.getElementsByTagName('TR');

		var len = trs.length;
		if (ignoreFoot) len -= 1;
		if (len <= 0) return;

		for (var i = 1; i < len; i++) {
			if (trs[i].getAttribute('isMainContract') == 'True') {
				// 主项目
				var img = document.createElement('IMG');
				if (trs[i + 1] && trs[i + 1].getAttribute('isMainContract') == 'False') {
					img.setAttribute('src', '/images/tree/tminus.gif');
				}
				else {
					img.setAttribute('src', '/images/tree/t.gif');
				}
				if (i == len - 1) {
					img.setAttribute('src', '/images/tree/l.gif');
				}
				img.style.verticalAlign = 'middle';

				var td = trs[i].getElementsByTagName('TD')[index];
				var first = td.firstChild;
				while (first.nodeType != 1) {
					first = first.nextSibling;
				}
				first.parentNode.insertBefore(img, first);
			}
			else {
				// 子项目
				// 如果当前行没有该属性，比如说页脚，总计行
				if (!trs[i].getAttribute('isMainContract')) continue;

				var img1 = document.createElement('img');
				img1.setAttribute('src', '/images/tree/i.gif');
				img1.style.verticalAlign = 'middle';
				var img2 = document.createElement('img');
				img2.setAttribute('src', '/images/tree/l.gif');
				img2.style.verticalAlign = 'middle';

				var td = trs[i].getElementsByTagName('TD')[index];

				var first = td.firstChild;
				while (first.nodeType != 1) {
					first = first.nextSibling;
				}
				first.parentNode.insertBefore(img1, first);
				first.parentNode.insertBefore(img2, first);
			}
		}
	}
	catch (ex) {
		top.ui.show(ex);
	}
}

function replaceEmptyTable(newTable, oldTable) {
	// 当数据量为空时，修改样式
	if (!document.getElementById(newTable)) return;
	if (!document.getElementById(oldTable)) return;

	var oTable = document.getElementById(oldTable);
	var nTable = document.getElementById(newTable);

	// 兼容IE和W3C，所以使用lastChild
	if (nTable.childNodes.length > 0) {
		oTable.replaceChild(nTable.lastChild, oTable.lastChild);
	}
}

//添加进度条
//className: 要添加进度条的类名称
//width: 进度条的宽度，默认为60px
//height：进度条的长度，默认为10px
function addPregressBar(className, width, height) {
	className = className || 'percent';
	width = width || "60px";
	height = height || "10px";
	var elements = getElementsByClassName(className);

	// 暂时不支持非IE浏览器
	if (!$.browser.msie) {
		for (var i = 0; i < elements.length; i++) {
			elements[i].style.width = '100px';
		}
		return;
	}

	for (var i = 0; i < elements.length; i++) {
		elements[i].style.width = '130px';
		var div1 = document.createElement('DIV');
		div1.style.float = 'left';
		var div2 = document.createElement('DIV');
		div2.style.float = 'left';

		var value = elements[i].lastChild.nodeValue;
		var outerSpan = document.createElement('SPAN');
		outerSpan.style.display = 'block';
		outerSpan.style.width = width;
		outerSpan.style.height = height;
		outerSpan.style.border = 'solid 1px #AAAAAA';

		var innerSpan = document.createElement('SPAN');
		innerSpan.style.display = 'block';
		try {
			innerSpan.style.width = value;
		}
		catch (ex) {
			innerSpan.style.width = "100%";
		}
		if (parseInt(innerSpan.style.width) > 100) {
			innerSpan.style.width = "100%";
		}
		innerSpan.style.height = "100%";
		innerSpan.style.backgroundColor = '#D4D4D4';

		innerSpan.innerHTML += "<span style='margin-left:65px; float:left;'>" + value + "</span>";
		outerSpan.appendChild(innerSpan);
		div1.appendChild(elements[i].lastChild);
		div2.appendChild(outerSpan);
		div1.style.width = '40px';
		div2.style.width = width;
		elements[i].style.display = 'inline';
		//elements[i].appendChild(div2);
		//elements[i].appendChild(div1);
		elements[i].appendChild(outerSpan);
	}
}
//根据部分ID获取所有该控与之相似的ID
function getControlId(id, control) {
	var a = document.getElementsByTagName(control);
	var ids = "";
	for (i = 0; i < a.length; i++) {
		if (a[i].id.indexOf(id) == 0) {
			ids += a[i].id + ",";
		}
	}
	return ids;
}
//打开管理附件窗口
function openannexpage(fid, mid) {
	if (fid != "") {
		window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid=" + mid + "&rc=" + escape(fid) + "&at=0&type=0", "", 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
	}
	else {
		alert("请先保存！");
	}
}
//查看附件
function viewAnnex(fid, mid) {
	if (fid != "") {
		window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid=" + mid + "&rc=" + escape(fid) + "&at=0&type=0", "", 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
	}
	else {
		alert("请先保存！");
	}
}

//主页面查看审核时
function setAuditState() {
	if (!getRequestParam('ic')) return;
	var table = document.getElementById('tableContent');
	if (table != null) {
		table.style['marginLeft'] = 'auto';
	}
	var divContent = document.getElementById('divContent');
	if (divContent != null) {
		divContent.style['overflow'] = 'visible';
	}
	var divFooter = document.getElementById('divFooter');
	if (divFooter != null) {
		divFooter.style['display'] = 'none';
	}
}

// 根据数据转换为数据库in()指定的格式
function GetArrayToInStr(str) {
	var returnVal = "";
	for (var i = 0; i < str.length; i++) {
		returnVal += "'" + str[i] + "',";
	}
	if (returnVal.length > 1)
		returnVal = returnVal.substring(0, returnVal.length - 1);
	else
		returnVal = "''";
	return returnVal;
}

// 在DataGrid只有一页的情况下, 隐藏页码 
// DataGrid生成的页脚的Class=GD-Pager, 如果有多页的话保护a标签
// IE8+,FF,Chrome下可用
function hideFirstPageNo() {
	var arrPager;
	if (document.querySelectorAll)
		arrPager = document.querySelectorAll('.GD-Pager');
	else
		arrPager = getElementsByClassName('GD-Pager');

	for (var i = 0; i < arrPager.length; i++) {
		var currPager = arrPager[i];
		var anchors = currPager.getElementsByTagName('a');
		if (anchors.length == 0)
			currPager.style.display = 'none';
	}
}

//DIv弹出IFrame
//页面中必须包含jQuery
function selectnEvent(divId, width, height) {
	width = width || 600;
	height = height || 485;
	var title = $('#' + divId).attr('title');
	$('#' + divId).dialog({ width: width, height: height, modal: true, title: title });
}

//Div关闭方法
//页面中必须包含jQuery
function divClose(obj) {
	try {
		// ui
		var dgCount = $(obj.document).find('.ui-icon-closethick').length;
		if (dgCount >= 1) {
			$(obj.document).find('.ui-icon-closethick').each(function (i) {
				if (i + 1 == dgCount)
					this.click();
			});
		}

		window.close();
	} catch (ex) { }
}

//页面中必须包含jQuery
function showTooltip(className, len) {
	var $tooltip = $('.' + className);
	for (var i = 0; i < $tooltip.length; i++) {
		var item = $tooltip.get(i);
		if (item.title.length > len) {
			$(item).css('display', '');
			$(item).tooltip({
				track: true,
				delay: 0,
				showURL: false,
				fade: true,
				showBody: " - ",
				extraClass: "solid 1px blue",
				fixPNG: true//,
				//left: event.offsetX
			});
		} else {
			item.title = '';
		}
	}
}

//重复点击相同的树节点
function clickTree(tree, prjId) {
	$('#' + tree + ' a').click(function () {
		if (this.href.indexOf($('#' + prjId + '').val()) >= 0 && this.href.indexOf($('#' + prjId + '').val() + '\\') < 0) {
			return false;
		}
	})
}

// 修改控件为可用
function enabledInput(id) {
	if (id && document.getElementById(id))
		document.getElementById(id).removeAttribute('disabled');
}

// 禁用控件
function disabledInput(id) {
	if (id && document.getElementById(id))
		document.getElementById(id).setAttribute('disabled', 'disabled');
}

// 提示管理员权限
function upAdminPrivilege() {
	var userCode = getCookie('UserCode');
	if (userCode == '00000000') {
		// 新增
		var btnAdd = document.getElementById('btnAdd');
		if (btnAdd) {
			var upPriv = btnAdd.getAttribute('upPriv');
			if (upPriv != '0') {
				btnAdd.removeAttribute('disabled');
			}
		}

		// 编辑
		var btnEdit = document.getElementById('btnEdit');
		if (btnEdit) btnEdit.removeAttribute('disabled');
		var btnUpdate = document.getElementById('btnUpdate');
		if (btnUpdate) btnUpdate.removeAttribute('disabled');

		// 删除
		var btnDel = document.getElementById('btnDel');
		if (btnDel) btnDel.removeAttribute('disabled');
		var btnDelete = document.getElementById('btnDelete');
		if (btnDelete) btnDelete.removeAttribute('disabled');
		var btnDelTask = document.getElementById('btnDelTask');
		if (btnDelTask) btnDelTask.removeAttribute('disabled');

		// 成员编制
		var btnMember = document.getElementById('btnMember');
		if (btnMember) btnMember.removeAttribute('disabled');

		// 编制进度计划
		var btnplaitWBS = document.getElementById('btnplaitWBS');
		if (btnplaitWBS) btnplaitWBS.removeAttribute('disabled');

		//        // 开工报告
		//        var btnStartWork = document.getElementById('btnStartWork');
		//        if (btnStartWork) btnStartWork.removeAttribute('disabled');
	}
}