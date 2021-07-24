//当按钮行的按钮比较多时，隐藏多余的按钮   lhy 2012-04-27

var divExpandContentId = undefined; //显示隐藏信息的DIV
var divExpandId = undefined;        //显示和隐藏按钮的图标DIV
var divButtonHideId = undefined;    //存储按钮的DIV

function hideButtons(tdButtonsId) {

	//如果已有divExpand，divExpandContent和divButtonHide三个层，先删除
	if (document.getElementById('divExpand') != null) {
		$('#divExpand').remove();
	}
	if (document.getElementById('divExpandContent') != null) {
		$('#divExpandContent').remove();
	}
	if (document.getElementById('divButtonHide') != null) {
		//删除之前将divButtonHide中的按钮移至tdButtonsId里
		$('#divButtonHide').find('input').each(function () {
			var buttons = document.getElementById(this.id);
			document.getElementById(tdButtonsId).appendChild(buttons);
		});
		$('#divButtonHide').remove();
	}
	var divExpand = '<div id="divExpand" style="width: 15px; height:100%; display: inline; cursor: pointer; position:relative; "';
	divExpand += 'onmouseover="mouseOver();" onmouseout="mouseOut();"  onclick="displayDivExpandContent();">>></div>';
	var divExpandContent = '<div id="divExpandContent" style="display: none;"> </div>';
	var divButtonHide = '<div id="divButtonHide" style="display:none;"></div>';
	var td = $('#' + tdButtonsId);
	td.append(divExpand);                 //创建DIV 用于显示和隐藏按钮的图标
	td.append(divExpandContent);          //创建DIV 用于显示隐藏的信息
	td.append(divButtonHide);             //创建DIV 用于存储按钮

	divExpandContentId = 'divExpandContent';
	divExpandId = 'divExpand';
	divButtonHideId = 'divButtonHide';

	//取得第一个按钮的纵坐标付给一个变量
	var topValue = $('#' + tdButtonsId).find('input').offset().top;
	var secondTopValue = 0;      //定义一个变量，存储大于第一个按钮的坐标
	//循环按钮所在的TR里的按钮
	$('#' + tdButtonsId).find('input').each(function () {
		var type = $(this).attr('type');
		if ((type == 'button' || type == 'submit') && (this.id != 'btnWfRecallSave' && this.id != 'btnWfRecallCancel')) {
			if ($(this).offset().top - topValue > 1) {   // 如果按钮的纵坐标大于第一个按钮的纵坐标 执行
				secondTopValue = $(this).offset().top;
				addHideInfo(this, this.value);
			} else {                                 //纵坐标小于第一个按钮的纵坐标的按钮右边距小于 70 时，此按钮也放入隐藏DIV中
				var widthLeft = $('#' + tdButtonsId).find('input').offset().left;
				var widthButton = $(this).width();
				var leftButton = $(this).offset().left - widthLeft;
				if ($('#' + tdButtonsId).width() - (widthButton + leftButton) < 73) {
					addHideInfo(this, this.value);
				}
			}
		}
	});

	//循环按钮所在的TR里的超链接，用法与循环按钮基本相同
	$('#' + tdButtonsId).find('A').each(function () {
		if ($(this).offset().top - topValue > 1) {
			secondTopValue = $(this).offset().top;
			addHideInfo(this, $(this).html());
		} else {
			var widthLeft = $('#' + tdButtonsId).find('input').offset().left;
			var widthButton = $(this).width();
			var leftButton = $(this).offset().left - widthLeft;
			if ($('#' + tdButtonsId).width() - (widthButton + leftButton) < 62) {
				addHideInfo(this, $(this).html());
			}
		}
	});

	//如果用于存储按钮DIV中的里面的元素个数大于0时 显示此DIV
	if ($('#' + divExpandContentId).children().length == 0) {
		$('#' + divExpandId).attr('style', 'display:none');
	}

	//添加 <li> 的划动鼠标样式
	$('#' + divExpandContentId).find('li').each(function () {
		if ($(this).attr('isValid') != 'false') {
			$(this).mouseover(function () {
				$(this).css("border", "#AAAAAA 1px solid");
			});
			$(this).mouseout(function () {
				$(this).css("border", "none");
			});
		}
	});
}

//将隐藏的的信息放进显示隐藏信息的DIV中
function addHideInfo(button, buttonValue) {
	document.getElementById(divButtonHideId).appendChild(button);      //将此按钮转移到存储按钮的DIV中
	//将此按钮的Value值以 <li> 的模式显示到显示隐藏信息的DIV中
	var mess = "";
	//if ($(button).attr('disabled') == true) {
	if ($(button).attr('disabled') != undefined) {
		mess = '<li style="color:#A0A0A0; cursor:default;" isValid="false">';
	} else {
		mess = '<li onclick="eventClick(' + button.id + ');">';
	}
	mess += buttonValue;
	mess += "</li>"
	var $li = $(mess);
	$li.css('fontSize', '13px');
	$li.css('list-style-type', 'none');
	$li.css('padding', '4px 10px 4px 10px');
	$li.css('cursor', 'pointer');
	$('#' + divExpandContentId).append($li);
}

//显示隐藏信息的DIV
var isDisplay = false;
function displayDivExpandContent() {
	var liCount = 0;
	//计算隐藏信息的高度
	$('#' + divExpandContentId).find('li').each(function () {
		liCount += 15;
	});
	if (isDisplay == false) {
		$('#' + divExpandContentId).attr('style',
        'background-color:white; width:100px; height:' + liCount + '; position:absolute; margin-top:5px; right:5px; border:#AAAAAA 1px solid;');
		isDisplay = true;
	} else {
		$('#' + divExpandContentId).attr('style', 'display: none;');
		isDisplay = false;
	}
}

//鼠标移入图标DIV
function mouseOver() {
	$('#' + divExpandId).attr('style', 'width: 15px; height:100%; display: inline; cursor: pointer; position:relative; border:#AAAAAA 1px solid; ');
}

//鼠标移出图标DIV
function mouseOut() {
	$('#' + divExpandId).attr('style', 'width: 15px; height:100%; display: inline; cursor: pointer; position:relative;');
}

//执行显示隐藏信息的DIV中的事件
function eventClick(button) {
	button.click();
	$('#' + divExpandContentId).attr('style', 'display: none;')
	isDisplay = false;
}