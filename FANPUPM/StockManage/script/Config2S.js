/// <reference path="../../Script/jquery.js" />
//移除$符号

var width = 600;
var height = 456;

function winopen(url) {
	//var iTop = (window.screen.availHeight-30-height)/2;       //获得窗口的垂直位置;
	//var iLeft = (window.screen.availWidth-10-width)/2;        //获得窗口的水平位置;
	window.open(url, "", "height=" + height + ", width=" + width + ", toolbar =no, menubar=no, scrollbars=no, resizable=no, location=no, status=no");
	//+ ",top=" + iTop + ",left=" + iLeft
	//  window.open(url,"", "height=456,width= 600,scrollbars=yes, resizable=yes");    
}

function viewopen(url) {
	window.open(url, "", "height=" + height + ", width=" + width + ", toolbar =no, menubar=no, scrollbars=yes, resizable=yes, location=no, status=no");
}
//关闭方法
function winclose(tobj, url, rebool) {
	if (rebool) {
		// alert(url);
		parent.desktop[tobj].location = url;
	};
	parent.desktop[tobj] = null;

	top.frameWorkArea.window.desktop.getActive().close();

}
//function $(id){
//    return document.getElementById(id);
//}

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
					//GUID的Length为36
					var param1 = str.substring(index, index + 36);
					var param2 = str.substring(index + 38, str.indexOf(');'));
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

// Remove trailing and leading whitespace 
if (!String.trim) {
	String.prototype.trim = function () {
		return this.replace(/^\s+|\s+$/g, '');
	}
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


///设置按钮(jwTable:GridView对象,btnDel:删除按钮ID,btnUpdate:更新按钮ID,btnQuery:查询ID,hdChkID:隐藏字段ID（主要编辑时使用)
function setButton(jwTable, btnDel, btnUpdate, btnQuery, hdChkId) {
	if (!jwTable.table) return;
	if (jwTable.table.firstChild.childNodes.length == 1) {
		//table中没有数据
		return;
	} else if (jwTable.table.firstChild.childNodes.length == 2) {
		//table中没有数据
		var te = $(".GD-Pager");
		if ($(".GD-Pager") != null) {
			var ec = $(".GD-Pager td").eq(0).attr("colspan");
			if (ec != null) {
				$(".GD-Pager").hide();
				$(jwTable.table).find("tr").eq(0).find("td").eq(0).find("input").attr("disabled", "disabled");
				return;
			}
		}
	}
	jwTable.registClickTrListener(function () {
		if (hdChkId != '')
			document.getElementById(hdChkId).value = this.id;
		if (this.getAttribute('guid')) {
			document.getElementById(btnQuery).guid = this.getAttribute('guid');
		}

		if (document.getElementById("btnRepeal") != null) {
			document.getElementById("btnRepeal").removeAttribute('disabled');
		}
		if (document.getElementById(btnDel) != null) {
			document.getElementById(btnDel).removeAttribute('disabled');
		}
		if (document.getElementById(btnUpdate) != null) {
			document.getElementById(btnUpdate).removeAttribute('disabled');
		}
		if (document.getElementById(btnQuery) != null) {
			document.getElementById(btnQuery).removeAttribute('disabled');
		}
		removeDisabled(this.id, btnUpdate, btnDel);
	});

	jwTable.registClickSingleCHKListener(function () {
		var checkedChk = jwTable.getCheckedChk();
		if (checkedChk.length == 0) {
			if (document.getElementById(btnDel) != null) {
				document.getElementById(btnDel).setAttribute('disabled', 'disabled');
			}
			if (document.getElementById(btnUpdate) != null) {
				document.getElementById(btnUpdate).setAttribute('disabled', 'disabled');
			}
			if (document.getElementById(btnQuery) != null) {
				document.getElementById(btnQuery).setAttribute('disabled', 'disabled');
			}
			if (document.getElementById("btnRepeal") != null) {
				document.getElementById("btnRepeal").setAttribute('disabled', 'disabled');
			}
		}
		else
			if (checkedChk.length == 1) {
				if (hdChkId != '')
					if (document.getElementById("btnRepeal") != null) {
						document.getElementById("btnRepeal").removeAttribute('disabled');
					}
				document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
				if (document.getElementById(btnDel) != null) {
					document.getElementById(btnDel).removeAttribute('disabled');
				}
				if (document.getElementById(btnUpdate) != null) {
					document.getElementById(btnUpdate).removeAttribute('disabled');
				} if (document.getElementById(btnQuery) != null) {
					document.getElementById(btnQuery).removeAttribute('disabled');
				}
				//被选中的唯一行的id
				var tr = getTrByChk(checkedChk[0]);
				removeDisabled(tr.id, btnUpdate, btnDel);

				if (tr.getAttribute('guid')) {
					document.getElementById(btnQuery).guid = tr.getAttribute('guid');
				}
			}
			else {
				if (hdChkId != '')
					document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
				if (document.getElementById(btnDel) != null) {
					document.getElementById(btnDel).removeAttribute('disabled');
				}
				if (document.getElementById(btnUpdate) != null) {
					document.getElementById(btnUpdate).setAttribute('disabled', 'disabled');
				}
				if (document.getElementById(btnQuery) != null) {
					document.getElementById(btnQuery).setAttribute('disabled', 'disabled');
				}
				var checkedChks = jwTable.getCheckedChk();
				var tr = "";
				setBtnDeleteState(btnDel, checkedChks);
			}
	});
	jwTable.registClickAllCHKLitener(function () {
		if (this.checked) {
			if (hdChkId != '')
				document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
			var checkedChks = jwTable.getAllChk();
			setBtnDeleteState(btnDel, checkedChks);
			if (document.getElementById("btnRepeal") != null) {
				document.getElementById("btnRepeal").removeAttribute('disabled');
			}
		}
		else {
			if (hdChkId != '')
				document.getElementById(hdChkId).value = '';
			if (document.getElementById(btnDel) != null) {
				document.getElementById(btnDel).setAttribute('disabled', 'disabled');
			}
			if (document.getElementById("btnRepeal") != null) {
				document.getElementById("btnRepeal").setAttribute('disabled', 'disabled');
			}
		}
		if (document.getElementById(btnUpdate) != null) {
			document.getElementById(btnUpdate).setAttribute('disabled', 'disabled');
		}
		if (document.getElementById(btnQuery) != null) {
			document.getElementById(btnQuery).setAttribute('disabled', 'disabled');
		}
		if (jwTable.table.rows.length == 2 && this.checked == true) {
			if (hdChkId != '')
				document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
			if (document.getElementById(btnDel) != null) {
				document.getElementById(btnDel).removeAttribute('disabled');
			}
			if (document.getElementById(btnUpdate) != null) {
				document.getElementById(btnUpdate).removeAttribute('disabled');
			}
			if (document.getElementById(btnQuery) != null) {
				document.getElementById(btnQuery).removeAttribute('disabled');
			}
			removeDisabled(jwTable.table.rows[1].id, btnUpdate, btnDel);
		}
	});

	function setBtnDeleteState(btnDel, chks) {
		var flowstateIndex = getFlowStateIndex(jwTable);
		var list = new Array(0, 0, 0);
		if (flowstateIndex > 0) {
			for (var i = 0; i < chks.length; i++) {
				var tr = getTrByChk(chks[i]);
				var m_ark = $(tr).attr("mark");
				if (m_ark == "1") {
					list[0] = "1";
				} else
					if (m_ark == "2") {
						list[1] = "2";
					} else
						if (m_ark == "3") {
							list[2] = "3";
						}
				checkAllState(list);
				var state = getFlowState.call(tr, jwTable);
				if (state != '未提交') {
					document.getElementById(btnDel).setAttribute('disabled', 'disabled');
				}
			}
		} else {
			for (var i = 0; i < chks.length; i++) {
				var tr = getTrByChk(chks[i]);
				var state = $(tr).attr("mark");
				if (state == "1") {
					list[0] = "1";
				} else
					if (state == "2") {
						list[1] = "2";
					} else
						if (state == "3") {
							list[2] = "3";
						}
			}
			checkAllState(list);
		}
	}

	function removeDisabled(trId, btnUpdate, btnDel) {
		var flowstateIndex = getFlowStateIndex(jwTable);
		var _mark = $("[id='" + trId + "']").attr("mark");
		var list = new Array(0, 0, 0);
		switch (_mark) {
			case "1":
				list[0] = "1";
				break;
			case "2":
				list[1] = "2";
				break;
			case "3":
				list[2] = "3";
				break;
		}
		checkAllState(list);

		if (flowstateIndex == 0)
			return;

		if (trId == '') return;

		var spans = $('#' + trId).find('span');
		var state = '-1';
		for (var i = 0; i < spans.length; i++) {
			if ($(spans[i]).attr('state') != undefined) {
				state = $(spans[i]).attr('state');
				break;
			}
		}
		if (state == '1' || state == '0' || state == '-2') {
			document.getElementById(btnUpdate).setAttribute('disabled', 'disabled');
		}
		if (state != '-1') {
			document.getElementById(btnDel).setAttribute('disabled', 'disabled');
		}
	}

	function checkAllState(list) {
		//全部是未作为归档资料的数据 可以删除 但不可以归档
		if (list[0].toString() == "0" && list[1].toString() == "2" && list[2].toString() == "0") {
			if (document.getElementById(btnDel) != null) {
				document.getElementById(btnDel).removeAttribute('disabled');
			}
			if (document.getElementById("btnFiles") != null) {
				document.getElementById("btnFiles").setAttribute('disabled', 'disabled');
			}
		}
		//全部是已选择作为归档资料的数据 不可以删除 但可以归档 
		else if (list[0].toString() == "0" && list[1].toString() == "0" && list[2].toString() == "3") {
			if (document.getElementById(btnDel) != null) {
				document.getElementById(btnDel).setAttribute('disabled', 'disabled');
			}
			if (document.getElementById("btnFiles") != null) {
				document.getElementById("btnFiles").removeAttribute('disabled');
			}
		} else {
			//其中含有 
			//1：已选择作为归档资料的数据 
			//2：未作为归档资料的,
			//3：或者已经归档的数据 
			//【不可以删除】【 不可以归档】 
			if (document.getElementById(btnDel) != null) {
				document.getElementById(btnDel).setAttribute('disabled', 'disabled');
			}
			if (document.getElementById("btnFiles") != null) {
				document.getElementById("btnFiles").setAttribute('disabled', 'disabled');
			}
			if (list[0].toString() == "1" && list[1].toString() == "0" && list[2].toString() == "0") {
				if (document.getElementById(btnUpdate) != null) {
					document.getElementById(btnUpdate).setAttribute('disabled', 'disabled');
				}
			}
		}
	}
}

//////////////////////////////////////////////////////////////////////////
function getTrByChk(chk) {
	if (chk.parentNode.parentNode.nodeName == 'TR') {
		return chk.parentNode.parentNode;
	}
	if (chk.parentNode.parentNode.parentNode.nodeName == 'TR') {
		return chk.parentNode.parentNode.parentNode;
	}
	return null;
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
	var tds = jwTable.table.firstChild.firstChild.childNodes;
	for (var i = 1; i < tds.length; i++) {
		///2011年9月29日8:43:23
		///增加了对 tds[i].firstChild.nodeValue 值不能为null 的判断
		///修改之前的原因是 在资料归档时  增加的是归档按钮 到这里会寻找流程状态信息
		///修改它不为空的情况则能
		if (tds[i].firstChild != null) {

			if (tds[i].firstChild.nodeValue != null) {
				if (tds[i].firstChild.nodeValue.indexOf('流程状态') >= 0) {
					return i;
				}
				if (tds[i].firstChild.nodeValue.indexOf('流程状态') >= 0) {
					return i;
				}
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
		var trs = table.firstChild.childNodes;
		if (trs.length == 0) return false;
		var len;
		if (ignoreFoot) len = trs.length - 1;
		else len = trs.length;
		for (var i = 1; i < len; i++) {
			if (trs[i].getAttribute('isMainContract') == 'True') {
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
				var td = trs[i].childNodes[index];
				td.insertBefore(img, td.lastChild);
			}
			else {
				//如果当前行没有该属性，比如说页脚，总计行
				if (!trs[i].getAttribute('isMainContract')) continue;

				var img1 = document.createElement('IMG');
				img1.setAttribute('src', '/images/tree/i.gif');
				img1.style.verticalAlign = 'middle';
				var img2 = document.createElement('IMG');
				img2.setAttribute('src', '/images/tree/l.gif');
				img2.style.verticalAlign = 'middle';
				var td = trs[i].childNodes[index];
				td.insertBefore(img1, td.lastChild);
				td.insertBefore(img2, td.lastChild);
			}
		}
	}
	catch (ex)
    { }
}

function replaceEmptyTable(newTable, oldTable) {
	//当数据量为空时，修改样式
	if (!document.getElementById(newTable)) return;
	if (!document.getElementById(oldTable)) return;
	var gvwContractType = document.getElementById(oldTable);
	var emptyContractType = document.getElementById(newTable);
	if (gvwContractType.firstChild.childNodes.length == 1) {
		gvwContractType.replaceChild(emptyContractType.firstChild, gvwContractType.firstChild);
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
//DIv弹出IFrame
//页面中必须包含jQuery
function selectnEvent(divId) {
	$('#' + divId).dialog({ width: 600, height: 485, modal: true });
}
//Div关闭方法
//页面中必须包含jQuery
function divClose(obj) {
	$(obj.document).find('.ui-icon-closethick').each(function () {
		this.click();
	});
}
///根据URL参数名称 获得到所对应的值
function request(paras) {
	var url = location.href;
	var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
	var paraObj = {}
	for (i = 0; j = paraString[i]; i++) {
		paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
	}
	var returnValue = paraObj[paras.toLowerCase()];
	if (typeof (returnValue) == "undefined") {
		return "";
	} else {
		return returnValue;
	}
}
