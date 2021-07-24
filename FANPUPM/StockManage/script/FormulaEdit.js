// JScript 文件
addEvent(window, 'load', function () {
	addButtonKeyEvent('txtFormula', 'hfldFormula', 'symbolButton');
	addButtonKeyEvent('txtFormula', 'hfldFormula', 'numberButton');
	addListKeyEvent('txtFormula', 'hfldFormula', 'lstField');
	addBtnDaysClickEvent();
	addBtnActualDaysClickEvent();
	addBtnClearAllClickEvent();
	setTxtFormulaReadOnly();
	var action = getRequestParam('Action');
	if (action == 'Query') {
		setAllInputDisabled();
	}
	$('btnSave').onclick = saveFormulaEvent;
	addEvent($('btnCancel'), 'click', function () {
		window.close();
	});
});

//给数字或操作符添加click事件
function addButtonKeyEvent(formulaId, hfldFormulaId, parentId) {
	var formula = document.getElementById(formulaId);
	var hfld = document.getElementById(hfldFormulaId);
	var div = document.getElementById(parentId);
	if (!formula) return;
	if (!div) return;
	if (!hfld) return;
	var inputs = div.getElementsByTagName('INPUT');
	for (var i = 0; i < inputs.length; i++) {
		addButtonValue(inputs, i);
	}
	function addButtonValue(inputs, i) {
		addEvent(inputs[i], 'click', function () {
			formula.value += Trim(inputs[i].value);
			hfld.value += Trim(inputs[i].value);
		});
	}
}

//给列表框添加click事件
function addListKeyEvent(formulaId, hfldFormulaId, listId) {
	var formula = document.getElementById(formulaId);
	var hfld = document.getElementById(hfldFormulaId);
	var list = document.getElementById(listId);
	if (!formula) return;
	if (!list) return;
	if (!hfld) return;
	var options = list.getElementsByTagName('OPTION');
	addEvent(list, 'click', function () {
		for (var i = 0; i < options.length; i++) {
			if (options[i].getAttribute('selected')) {
				formula.value += '[' + Trim(options[i].firstChild.nodeValue) + ']';
			}
		}
		hfld.value += '[' + Trim(list.value) + ']';
	});
	return;
}

function addBtnDaysClickEvent() {
	var btn = document.getElementById('btnDays');
	addEvent(btn, 'click', function () {
		var formula = document.getElementById('txtFormula');
		var hfld = document.getElementById('hfldFormula');
		hfld.value += '{' + 'Days' + '}';
		formula.value += '{' + '实上天数' + '}';
	});
}

function addBtnActualDaysClickEvent() {
	var btn = document.getElementById('btnActualDays');
	addEvent(btn, 'click', function () {
		var formula = document.getElementById('txtFormula');
		var hfld = document.getElementById('hfldFormula');
		hfld.value += '{' + 'ActualDays' + '}';
		formula.value += '{' + '工日' + '}';
	});
}

//全部清除
function addBtnClearAllClickEvent() {
	var btn = document.getElementById('btnClearAll');
	var txt = document.getElementById('txtFormula');
	var hfld = document.getElementById('hfldFormula');
	if (!btn) return;
	if (!txt) return;
	if (!hfld) return;
	addEvent(btn, 'click', function () {
		txt.value = '';
		hfld.value = '';
	});
}

function saveFormulaEvent() {
	if (!validateInputs()) return false;
	function validateInputs() {
		var formulaName = new ValidateInput('txtFormulaName', '工式名称');
		if (!formulaName.validateMustInput()) return false;
		var formula = new ValidateInput('txtFormula', '计算工式');
		if (!formula.validateMustInput()) return false;
		if (!validateFormula($('hfldFormula').value)) return false;
		return true;
	}
}

function validateFormula(formula) {
	if (formula.indexOf('][') >= 0
        || formula.indexOf(']{') >= 0
        || formula.indexOf('}{') >= 0
        || formula.indexOf('++') >= 0
        || formula.indexOf('+') == 0
        || formula.indexOf('-') == 0) {
		alert('系统提示：\n\n工式格式不正确！');
		return false;
	}
	formula = replaseNameByNumber(formula);
	try {
		var ret = eval(formula);
	}
	catch (e) {
		alert('系统提示：\n\n工式格式不正确！');
		return false;
	}
	return true;
}

//把公式里面的名称替换成数字1
function replaseNameByNumber(formula) {
	formula = formula.replace(/\[\w*\]/g, 1);
	formula = formula.replace(/\{\w*\}/g, 1);
	formula = formula.replace(/x/g, '*');
	formula = formula.replace(/-/g, '+')
	return formula;
}

function setTxtFormulaReadOnly() {
	if (!$('txtFormula')) return;
	$('txtFormula').setAttribute('ReadOnly', 'ReadOnly');
}
