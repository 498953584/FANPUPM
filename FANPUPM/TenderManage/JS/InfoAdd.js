// 信息立项申请
$(document).ready(function () {
	Val.validate('form1', 'btnSave');
	$('#txtOwner').attr('readOnly', 'readOnly');
	$('#txtAgent').attr('readOnly', 'readOnly');

	$('#txtBusinessManager').attr('readOnly', 'readOnly');
	$('#txtTenderManager').attr('readOnly', 'readOnly');
	$('#txtDutyPerson').attr('readOnly', 'readOnly');
	$('#txtName').attr('readOnly', 'readOnly');             // 不能修改，必须是系统内部人员
	var divHei = document.getElementById('divHeight').value;
	if (divHei == "Add") {
		document.getElementById('divScroll').style.overflowY = 'auto';
		document.getElementById('divScroll').style.height = $(document).height() - 28 + "px";
	}
	else if (divHei == "Update1") {
		document.getElementById('divScroll').style.overflowY = 'auto';
		document.getElementById('divScroll').style.height = $(document).height() - 28 + "px";
	}
	else {
		document.getElementById('divScroll').style.overflowY = 'auto';
		document.getElementById('divScroll').style.height = $(document).height() - $('#prjState').height() - 28 + "px";
	}

	// 绑定多个工程类型
	showEngType();
	// 绑定项目类型
	bindKind();
	// 绑定资质要求
	bindRank();

	// 注册保存按钮事件 
	$('#btnSave').click(save);
	// 保存项目类型的Json数据
	$('#btnSave').click(savePrjKindClass);
	// 保存资质要求的Json数据
	$('#btnSave').click(saveRank);

	// 添加工程类型事件
	$('#btn_editBuildingType').click(editBuildingType);
	// 添加项目类型
	$('#btn_add_PrjKindClass').click(addPrjKindClass);
	// 添加资质要求
	$('#btn_add_rank').click(addRank);
	// 保存项目生成的编码
	$('#hfldPrjCode').val($('#txtPrjCode').val());
	//根据hfldIsEditProjectCode的值判断是否允许编辑项目编码
	if ($('#hfldIsEditProjectCode').val() == '0') {
		$('#txtPrjCode').attr('readonly', 'readonly');
	} else if ($('#hfldIsEditProjectCode').val() == '1') {
		$('#txtPrjCode').removeAttr('readonly');
	}
});

//甲方名称
function pickCorp(param) {
	jw.selectOneCorp({ idinput: 'hfldOwner', nameinput: 'txtOwner', func: function (corp) {
		$.ajax({
			type: "GET",
			async: false,
			url: '/TenderManage/Handler/GetOwnerInfo.ashx?ownerName=' + encodeURI(corp.name),
			success: function (datas) {
				var data = eval(datas)[0];
				if (data != undefined) {
					$('#txtOwnerLinkMan').val(data.LinkMan);
					$('#txtOwnerLinkManPhone').val(data.Telephone);
					$('#txtOwnerAddress').val(data.Address);
				}
			}
		});
	}
	});
}


// 选择立项申请人
function selectApplyUser() {
	selectUser('hfldPrjPeople', 'txtName');
}

// 项目跟踪人
function selectDutyUser() {
	selectUser('hfldDutyPerson', 'txtDutyPerson');
}

// 主要负责人
function selectMainUser() {
	selectUser('hfldWorkUnit', 'txtWorkUnit');
}

// 项目经理
function selectPrjManager() {
	selectUser('hfldPrjManager','txtPrjManager');
}

// 阅知人1
function selectRedone1() {
	selectUser('hfldPrjReadOne', 'txtPrjReadOne')
}

// 经办人
function selectAgentUser() {
	selectUser('hfldAgent','txtAgent');
}

// 阅知人2
function selectRedone2() {
	selectUser('hfldQualificationReadOne', 'txtQualificationReadOne');
}

// 阅知人2
function selectRedone3() {
	selectUser('hfldTenderReadOne', 'txtTenderReadOne');
}


//选择人员
function selectUser(id, name) {
	if (id == 'hfldPrjPeople') {
		jw.selectOneUser({ nameinput: name, codeinput: id, func: function () {
			$.ajax({
				type: "GET",
				async: false,
				url: '/TenderManage/Handler/GetUserInfo.ashx?usercode=' + $('#hfldPrjPeople').val(),
				success: function (datas) {
					var data = eval(datas)[0];
					if (data != undefined) {
						$('#txtCorp').val(data.CorpName);
						$('#txtDuty').val(data.Duty);
						$('#txtPhone').val(data.Phone);
					}

				}
			});
		}
		});

	} else if (id == 'hfldWorkUnit') {
		// 选择主要负责人时，自动带出主要负责人联系方式
		jw.selectOneUser({ nameinput: name, codeinput: id, func: function () {
			$.ajax({
				type: "GET",
				async: false,
				url: '/TenderManage/Handler/GetUserPhone.ashx?usercode=' + $('#hfldWorkUnit').val(),
				success: function (data) { $('#txtTelphone').val(data); }
			});
		}
		});
	} else {
		jw.selectOneUser({ nameinput: name, codeinput: id });
	}
}

//取消按钮事件
function CancelClick() {
	winclose('InfoAdd.aspx', 'InfoSetUp.aspx', false);
}

function imageClick(state) {
	var divId = 'div' + state;
	document.getElementById('hfldstate').value = state;
	$('#' + divId).append($('#btnSaveData'));
	$('#' + divId).dialog({
		open: function () { $(this).parent().appendTo("form:first"); },
		width: 600,
		height: 310,
		modal: true,
		buttons: {
			"保存": function () { saveData(state); },
			"取消": function () { $(this).dialog("close"); }
		}
	});
}

//保存数据
function saveData(btnState) {
	if (btnState == 3 && $('#txtProjStartDate').val() == '') {
		alert('启动日期必须输入');
		$('#txtProjStartDate').focus();
		return;
	}
	else if (btnState == 4 && $('#txtTenderBeginDate').val() == '') {
		alert('开标日期必须输入');
		$('#txtTenderBeginDate').focus();
		return;
	}
	else if (btnState == 14 && $('#txtApprovalDate').val() == '') {
		alert('预审日期必须输入');
		$('#txtApprovalDate').focus();
		return;
	}
	$('#btnSaveData').click();

}

// 切换城市
function City_onchange() {
	var city = document.getElementById('dropcity');
	document.getElementById('hfldCity').value = city[city.selectedIndex].text;
	if (document.getElementById('dropcity').value) {
		$.ajax({
			type: 'POST',
			async: false,
			url: '/TenderManage/Handler/GetCodeByProvinceCity.ashx?province=' + encodeURI($('#dropprovince option:selected').text()) + '&city=' + encodeURI($('#dropcity option:selected').text()),
			success: function (str) {
				if (str != "") {
					$('#txtPrjCode').val(str);
				} else {
					$('#txtPrjCode').val($('#hfldPrjCode').val());
				}
			}
		});
	}
}

// 绑定城市
function bindCity() {
	if (document.getElementById('dropprovince').value != '') {
		$.ajax({
			type: 'POST',
			async: false,
			url: '/TenderManage/Handler/GetCity.ashx?province=' + $('#dropprovince').val(), //document.getElementById('dropprovince').value,
			success: function (str) {
				var strTemp = str.split('|');
				for (var i = 0; i < strTemp.length; i++) {
					if (strTemp[i] == "请选择") {
						document.getElementById('dropcity').options[i] = new Option("请选择", "");
					}
					else {
						document.getElementById('dropcity').options[i] = new Option(strTemp[i], i);
					}
				}
				document.getElementById('dropcity').length = strTemp.length;
				var city = document.getElementById('dropcity');
				document.getElementById('hfldCity').value = city[city.selectedIndex].text;
			}
		});
	}
	else {
		document.getElementById('dropcity').options[0] = new Option("请选择", "");
		document.getElementById('dropcity').length = 1;
	}
}

function Province_onchange() {
	bindCity();
}


// 添加项目类型
function addPrjKindClass() {
	$('.prjKindClass').last().after(createKind());              // 添加到页面
}

// 保存项目类型的Json数据
function savePrjKindClass() {
	var kindArr = new Array();
	$('.prjKindClass').each(function (i) {
		var kind = {
			"PrjKind": $(this).find('select option:selected').val(),
			"ProfessionalCost": $(this).find('input:eq(0)').val()
		};
		if (kind.ProfessionalCost == '') {
			kind.ProfessionalCost = '0';
		}
		var ProfessionalCost = kind.ProfessionalCost.split(',').join('');
		kind.ProfessionalCost = ProfessionalCost;
		kind.ProfessionalCost = kind.ProfessionalCost || '0';
		if (kind.PrjKind) kindArr.push(kind);
	})
	var json = kindArr.length == 0 ? '' : JSON.stringify(kindArr);
	$('#hfldPrjKindJson').val(json);
}

// 创建新的项目类型
function createKind() {
	var $kind = $('#tr_PrjKindClass').clone();
	var len = $('.prjKindClass').length;
	$kind.find('*[id]').each(function () { this.id += '_' + len; }); // 初始化ID
	$kind.find('td:even').empty();          // 清空文字说明
	$kind.find('select').val('');     // 清空项目类型
	$kind.find('input').val('');     // 初始化专业造价
	// 删除按钮
	$kind.find(':button').val('删除').click(function () {
		$(this).parent().parent().remove();
	});
	return $kind;
}

// 绑定项目类型
function bindKind() {
	var kindJson = $('#hfldPrjKindJson').val();
	if (!kindJson) return false;

	var kindArr = eval(kindJson);
	for (var i = 0; i < kindArr.length; i++) {
		if (i == 0) {
			$('#dropPrjKindClass').val(kindArr[i].PrjKind);
			$('#txtProfessionalCost').val(kindArr[i].ProfessionalCost);
		} else {
			var $kind = createKind();
			$kind.find('select:eq(0)').val(kindArr[i].PrjKind);
			$kind.find('input:eq(0)').val(kindArr[i].ProfessionalCost);
			$('.prjKindClass').last().after($kind);
		}
	}
}



// 添加资质要求
function addRank() {
	$('.rank').last().after(createRank());              // 添加到页面
}

// 保存资质要求的Json数据
function saveRank() {
	var rankArr = new Array();
	$('.rank').each(function (i) {
		var rank = {
			"Rank": $(this).find('select option:selected').val(),
			"RankLevel": $(this).find('input:eq(0)').val()
		};

		if (rank.Rank && rank.RankLevel) rankArr.push(rank);
	});

	var json = rankArr.length == 0 ? '' : JSON.stringify(rankArr);
	$('#hfldRankJson').val(json);
}

// 绑定资质要求
function bindRank() {
	var rankJson = $('#hfldRankJson').val();
	if (!rankJson) return false;

	var rankArr = eval(rankJson);
	for (var i = 0; i < rankArr.length; i++) {
		if (i == 0) {
			$('#dropRank').val(rankArr[i].Rank);
			$('#txtRankLevel').val(rankArr[i].RankLevel);
		} else {
			var $rank = createRank();
			$rank.find('select:eq(0)').val(rankArr[i].Rank);
			$rank.find('input:eq(0)').val(rankArr[i].RankLevel);
			$('.rank').last().after($rank);
		}
	}
}

// 创建新的资质要求
function createRank() {
	var $rank = $('#tr_rank').clone();
	var len = $('.rank').length;
	$rank.find('*[id]').each(function () { this.id += '_' + len; }); // 初始化ID
	$rank.find('td:even').empty();          // 清空文字说明
	$rank.find('input,select').val('');     // 清空数据
	// 删除按钮
	$rank.find(':button').val('删除').click(function () {
		$(this).parent().parent().remove();
	});
	return $rank;
}


//管理日期
function controlDate(para) {
	var startDates = document.getElementById('txtStartDate').value;
	var startDateList = startDates.split('-');
	var startDate = new Date(startDateList[0], startDateList[1] - 1, startDateList[2]);

	var endDates = document.getElementById('txtEndDate').value;
	var endDatesList = endDates.split('-');
	var endDate = new Date(endDatesList[0], endDatesList[1] - 1, endDatesList[2]);

	if (startDates != '') {
		if (startDate == 'NaN') {
			$.messager.alert('系统提示', '计划开始日期输入日期格式不正确！');
			para.value = '';
			return;
		}
	}
	if (endDates != '') {
		if (endDate == 'NaN') {
			$.messager.alert('系统提示', '计划结束日期输入日期格式不正确！');
			para.value = '';
			return;
		}
	}
	if (startDates != "" && endDates != "") {
		if (endDate - startDate < 0) {
			para.value = "";
			$.messager.alert('系统提示', '计划结束日期不能小于计划开始日期，请重新选择日期！');
		}
	}
}
//限制输入字符长度
function limitTextLenth(txtId) {
	var txtValue = txtId.value;
	if (txtValue.length > 100) {
		txtId.value = txtValue.substring(0, 100);
		$.messager.alert('系统提示', '输入的字数不能大于100个字');
	}
}

// 改变立项申请人
function changeApplicant() {
	alert($(this).val())
}