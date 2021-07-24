// Desktop.aspx中使用，生成代办工作和待审流程。
// 依赖jQuery.js, json2.js, jw.js, Desktop.js

var dbsjId = '2801';                                    // 代办工作id
var waringId = "2842"; 	                                // 预警
var outlinkId = "381705";                               // 收藏夹
var wfId = "283818"; 	                                // 流程审核
var bulletionId = "280305";                             // 公告管理
var newId = "2827"; 									// 内部新闻
var regimeId = "281103"; // 制度查询

var app1Id = "381703"; 	// 内部应用1 
var app2Id = "381707"; 	// 内部应用2 
var app3Id = "381708"; 	// 内部应用3 
var app4Id = "381709"; 	// 内部应用4 
var app5Id = "381710"; 	// 内部应用5 
var app6Id = "381711"; 	// 内部应用6

var winWidth = 1024;
var colNum = 0;             // 列数
var rowNum = 0;             // 行数
var pWidth = 370; 		// panel的宽度
var wrodCount = 20; 		// 截取字数

$(document).ready(function () {

	generateDeskTop(); 					// 构造桌面
	setInterval('refresh();', 30000);      // 30秒自动刷新
	top.ui.generateDeskTop = generateDeskTop; // 重新构造我桌面，重新生成页面布局
	top.ui.refreshDesktop = refresh; 			// 保存此方法，供其他标签页调用, 只是刷新页面的数据
});



// 构造桌面
function generateDeskTop() {
	getColAndRowNum(); 	// 获取用户配置的行数和列数
	calcWordCount(); // 计算显示字符的格式
	createTable();      // 创建table
	createPanels();     // 创建panel
}

// 获得用户配置的行数和列数
function getColAndRowNum() {
	if ($('#tabPanels').html()) {
		$('#tabPanels').html('')
	}

	winWidth = $(window).width(); // 计算分辨率宽度
	$.ajax({
		type: 'GET',
		url: 'Handler/GetUserSetting.ashx?type=1&width=' + winWidth,
		cache: false,
		async: false,
		success: function (json) {
			if (json == -1) return;
			var objArr = eval(json);
			colNum = objArr[0].GirdColNum;
			rowNum = objArr[0].RowInGrid;
			pWidth = objArr[0].GirdWidth;
		}
	});
}

// 计算显示字符的格式
function calcWordCount() {
	if (pWidth >= 450) wrodCount = 28;
	if (pWidth < 450 && pWidth >= 430) wrodCount = 25;
	if (pWidth < 430 && pWidth >= 410) wrodCount = 23;
	if (pWidth < 410 && pWidth >= 390) wrodCount = 22;
	if (pWidth < 390 && pWidth >= 370) wrodCount = 20;
	if (pWidth < 370 && pWidth >= 350) wrodCount = 18;
	if (pWidth < 350 && pWidth >= 330) wrodCount = 17;
	if (pWidth < 330 && pWidth >= 310) wrodCount = 15;
	if (pWidth < 310 && pWidth >= 290) wrodCount = 14;
	if (pWidth < 290 && pWidth >= 270) wrodCount = 12;
	if (pWidth < 270) wrodCount = 10;
}

// 创建table
function createTable() {
	var panelArr;
	$.ajax({
		type: 'GET',
		url: 'Handler/GetUserSetting.ashx?type=2',
		cache: false,
		async: false,
		success: function (json) {
			if (json == -1) return;
			panelArr = eval(json);
		}
	});


	// 存储一般模块
	var pArr2 = new Array();

	// 存储特殊模块
	var pArr3 = new Array();
	for (var k = 0; k < panelArr.length; k++) {
		//		if (panelArr[k].ModelID == '2801' || panelArr[k].ModelID == '2842' ||
		//                panelArr[k].ModelID == '283818' || panelArr[k].ModelID == '280305')
		if (panelArr[k].ModelID == '780501')
			pArr3.push(panelArr[k]);
		else
			pArr2.push(panelArr[k]);
	}

	var tr = '<tr>';

	// 遍历并创建所有模块
	for (var k = 0; k < pArr2.length; k++) {
		var id = pArr2[k].ModelID;
		var name = pArr2[k].MNewName;

		var div = jw.format('<div id="p_{0}" class="easyui-panel" title="{1}"></div>', id, name);
		tr += jw.format('<td>{0}</td>', div);

		// 到达指定列或者循环结束时需要换行
		if ((k + 1) % colNum == 0 || (k + 1) == pArr2.length) {
			tr += '</tr>';
			$('#tabPanels').append(tr);
			tr = '<tr>';
		}
	}
}

// 构造panel
function createPanels() {
	$('#tabPanels div[id^=p_]').each(function (i) {
		$(this).panel({
			width: pWidth,
			//			height: (rowNum + 2) * 25.3 + 6, // 210,
			title: this.title
		})
		// 填充panel
		fillPanel(this.id);
	});
}

// 添加panel内容
function fillPanel(pId) {
	var $panel = $('#' + pId);
	var modelId = pId.substr(2);

	// 用于比较是否需要更新
	var oldid = '';
	var newid = '';

	$panel.find('tr').each(function () { oldid += this.id; });
	$.ajax({
		type: 'GET',
		url: 'Handler/GetDesktopData.ashx?id=' + modelId + '&rowNum=' + rowNum,
		cache: false,
		success: function (json) {
			if (!json) return;
			var objArr = eval(json);
			// 构造table
			var tab = '<table class="tab-panel">';
			for (var i = 0; i < objArr.length; i++) {
				var id = objArr[i].Id;
				var content = jw.trim(objArr[i].Content);
				var url = objArr[i].Url;
				var date = objArr[i].Date;

				newid += id;

				if (modelId == wfId && objArr[i].cs > 0) {
					content = '<span>【超时】</span>' + content; // 前面部分共17位长度
				}

				var vision = content;

				if (content.indexOf('超时') > -1 && content.length > wrodCount + 13) {
					// 超时	
					vision = '--' + content.substr(0, wrodCount + 13) + '...';
				}
				else if (content.indexOf('span') == -1 && content.length > wrodCount) {
					vision = content.substr(0, wrodCount) + '...'
				}
				var tr = jw.format('<tr id="{0}" pid="{1}" subtitle="{2}" url="{3}" class="tr-data"><td class="td-content tooltip" width="{6}" title="{2}" onclick="clickData(\'{1}\', \'{0}\');">{5}</td><td class="td-date">{4}</td></tr>',
                    id, pId, content, url, date, vision, pWidth - 80);
				tab += tr;
			}

			// 添加空行
			for (var j = 0; j < rowNum - objArr.length; j++) {
				var emptr = '<tr class="tr-empty"><td colspan="2"></td></tr>';
				tab += emptr;
			}

			// 添加“更多”
			var span = createSpan(modelId);
			var more = jw.format('<div class="div-more">{0}</div>', span);
			var tr_more = jw.format('<tr><td colspan="2">{0}</td></tr>', more);
			tab += tr_more;

			tab += '</table>';

			// 若不相等或者为空, 则需要更新
			if (newid != oldid || newid == '') {
				$panel.empty();
				$panel.append(tab);         // 添加到页面
			}
		}
	});
}

// 创建"更多"的span
function createSpan(modelId) {
	var txt = '更多';
	var url = '';
	var title = '';

	if (modelId == dbsjId) {
		url = '/SysFrame/PTDBSJList.aspx';
		title = '代办工作';
	} else if (modelId == waringId) {
		url = '/oa/Warning/WarningList.aspx';
		title = '预警提醒';
	} else if (modelId == wfId) {
		url = '/EPC/WorkFlow/PTAuditList.aspx';
		title = '待审流程列表';
	} else if (modelId == bulletionId) {
		url = '/oa/Bulletin/BulletinManage.aspx?type=see'
		title = '公告管理';
	} else if (modelId == newId) {
		url = '/WEB/WebManagerList.aspx?c_xwlxdm=99&c_xwlxmc=公司新闻&browse=true';
		title = '内部新闻';
	} else if (modelId == outlinkId) {
		url = '/TableTop/WebLink.aspx?';
		title = '收藏夹';
		txt = '设置';
	} else if (modelId == regimeId) {
		url = '/oa/System/Institution/InstitutionListSearch.aspx';
		title = '制度查询';
	} else if (modelId == app1Id) {
		txt = '设置';
		url = '/TableTop/menuList.aspx?op=1';
		title = '内部应用1';
	} else if (modelId == app2Id) {
		txt = '设置';
		url = '/TableTop/menuList.aspx?op=2';
		title = '内部应用2';
	} else if (modelId == app3Id) {
		txt = '设置';
		url = '/TableTop/menuList.aspx?op=3';
		title = '内部应用3';
	} else if (modelId == app4Id) {
		txt = '设置';
		url = '/TableTop/menuList.aspx?op=4';
		title = '内部应用4';
	} else if (modelId == app5Id) {
		txt = '设置';
		url = '/TableTop/menuList.aspx?op=5';
		title = '内部应用5';
	} else if (modelId == app6Id) {
		txt = '设置';
		url = '/TableTop/menuList.aspx?op=6';
		title = '内部应用6';
	}

	var span = jw.format('<span onclick="top.ui.createTab(\'{0}\', \'{1}\');">{2}</span>', title, url, txt);
	return span;
}

// 单击事件 
function clickData(pId, key) {
	var modelId = pId.substr(2);
	var $tr = $('#' + key);
	var url = $tr.attr('url');
	var title = $tr.attr('subtitle');

	if (title.indexOf('超时') > -1)
		title = title.substr(17);

	// 限制标签页的标题字长
	if (title.length > 8)
		title = title.substr(0, 8) + '...';

	// 代办工作
	if (modelId == dbsjId) {
		_createTab(title, url, 0);
		$.ajax({
			type: 'GET',
			url: '/TableTop/Handler/UpdateTopFlag.ashx?modelId=' + modelId + '&pk=' + key,
			cache: false,
			success: function () {
				fillPanel(pId);
			}
		});
	} else if (modelId == outlinkId) {
		// 收藏夹不在框架内打开
		window.open(url);
	} else if (modelId == wfId && jw.ipad()) {
		window.open(url);
	}
	else {
		_createTab(title, url, 0);
	}
}

// 自动刷新panel
function refresh() {
	fillPanel('p_2801');
	fillPanel('p_2842');
	fillPanel('p_381705');
	fillPanel('p_283818');
	fillPanel('p_280305');
	fillPanel('p_2827');
	fillPanel('p_281103');
	fillPanel('p_281103');
	fillPanel('381703');
	fillPanel('381707');
	fillPanel('381708');
	fillPanel('381709');
	fillPanel('381710');
	fillPanel('381711');
}

// 设置桌面布局
function settingPanel() {
	top.ui.myTab = window;
	var url = '/TableTop/UserSet.aspx?deskwidth=' + width + $(window).width();
	top.ui.openWin({ title: '设置桌面布局', url: url, width: 320, height: 200 });
}

// 桌面栏目管理
function mangerPanel() {
	var url = '/TableTop/ModelList.aspx';
	top.ui.openTab({ title: '桌面栏目设置', url: url });
}