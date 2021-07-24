/*
* 桌面提醒
*/
$(document).ready(function () {
	//首次登陆提醒
	loginPopup();
	//即时提醒
	window.setInterval("loginPopup()", 30000);
});

//登陆提醒
function loginPopup() {
	$.ajax({
		type: 'GET',
		url: '/SysFrame/Handler/Popup.ashx?' + new Date().getTime(),
		success: function (data) {
			if (data != "-1") {
				var popupArr = eval(data);
				for (var i = 0; i < popupArr.length; i++) {
					var item = popupArr[i];
					if (item.Module == "Bulletin")
						registerBulletion(item);
					else if (item.Module == "CompanyNews")
						registerCompanyNews(item);
					else if (item.Module == 'Workflow')
						registerWorkflow(item);
					else if (item.Module == 'chedule')
						registerSchedule(item);
					else if (item.Module == 'Mail')
						registerMail(item);
					else if (item.Module == 'Warning')
						registerWarning(item);
				}
			}
		}
	});
}


//注册系统公告
function registerBulletion(item) {
	$.jGrowl(item.Content, {
		life: jw.poplife,
		header: item.Title,
		footer: '设置',
		click: function () {
			var url = item.HandleUrl + '?ic=' + item.Id + '&mc=' + item.Title;
			toolbox_oncommand(url, '系统公告');
		}
	});
}

//注册公司新闻
function registerCompanyNews(item) {
	$.jGrowl(item.Content, {
		life: jw.poplife,
		header: item.Title,
		footer: '设置',
		click: function () {
			var url = item.HandleUrl + '?ic=' + item.Id;
			toolbox_oncommand(url, '公司新闻');
		}
	});
}

//流程审核
function registerWorkflow(item) {
	$.jGrowl(item.Content, {
		life: jw.poplife,
		header: item.Title,
		footer: '设置',
		click: function () {
			var url = item.HandleUrl + '?ic=' + item.Id + '&id=' + item.NoteId +
				'&pass=' + item.IsAllPass + '&nid=' + item.NodeID +
				'&bc=' + item.BusinessCode + '&bcl=' + item.BusinessClass;
			toolbox_oncommand(url, '流程审核');
		}
	});
}

//代办事项
function registerSchedule(item) {
	$.jGrowl(item.Content, {
		life: jw.poplife,
		header: item.Title,
		footer: '设置',
		click: function () {
			toolbox_oncommand('/' + item.HandleUrl, '代办工作');
		}
	});
}

//邮件
function registerMail(item) {
	$.jGrowl(item.Content, {
		life: jw.poplife,
		header: item.Title,
		footer: '设置',
		click: function () {
			var url = item.HandleUrl + '?mailId=' + item.Id + '&reply=1';
			toolbox_oncommand(url, '邮件');
		}
	});
}

//预警
function registerWarning(item) {
	$.jGrowl(item.Content, {
		life: jw.poplife,
		header: item.Title,
		footer: '设置',
		click: function () {
			var url = item.HandleUrl;
			toolbox_oncommand(url, item.Title);
		}
	});
}
