
//TODO: 重复执行
function initialFileUpload(id) {
	var prefix = id + '_';
	var typeId = prefix + 'hfldType';           //保存"type"值的隐藏控件Id
	var nameId = prefix + 'hfldName';           //保存“Name”值得隐藏控件Id
	var folderId = prefix + 'hfldFolder';       //保存存放路径的隐藏控件Id
	var recordCodeId = prefix + 'hfldRecordCode';   //保存“RecordCode”的隐藏控件Id
	var fileTypeId = prefix + 'hfldFileType';       //保存文件类型的隐藏控件Id
	var scriptId = prefix + 'hfldScript';       //hfldScript控件的Id
	var scriptDataId = prefix + 'hfldScriptData'; //hfldScriptData的控件的Id
	var onClose = prefix + 'hfldOnClose';       //hfldOnClose控件的Id
	var onComplete = prefix + 'hfldOnComplete'; //hfldOnComplete的控件的Id
	var dialogId = prefix + 'dialog';           //弹出DIV的Id

	var manageId = prefix + 'btnManage';        //"管理附件"按钮的Id
	var upId = prefix + 'btnUp';                //上传按钮的Id
	var txtFileId = prefix + 'txtFile';         //显示名称的输入框的Id
	var annexTableId = prefix + 'annexTable';   //显示附件的Table的Id
	var uploadifyId = prefix + 'uploadify';     //上传控件的Id


	// 根据不同类型隐藏不同控件
	var type = $('#' + typeId).val();
	if (type == '1') {
		$('#' + manageId).hide();
	} else if (type == '2') {
		$('#' + txtFileId).hide();
	} else if (type == '3') {
		$('#' + manageId).hide();
		$('#' + txtFileId).hide();
	}

	// Name
	var name = $('#' + nameId).val();
	$('#' + upId).val('上传' + name);
	$('#' + manageId).val('管理' + name);
	$('#' + dialogId).attr('title', $('#' + upId).val());

	// 文件上传路径
	var folder = $('#' + folderId).val();
	if ($('#' + recordCodeId).val()) {
		// 上传路径+=具体单据的ID
		folder += '/' + $('#' + recordCodeId).val();
	}

	setFileName(folder, txtFileId);

	// 上传
	$('#' + upId).click(function () {
		$('#' + dialogId).css('display', '').window({
			width: 420,
			modal: true
		});
		initFileUpload(folder);
	});


	function initFileUpload(folder) {
		if ($("#" + uploadifyId).attr('isInitial') == '1')
			return false;
		$("#" + uploadifyId).attr('isInitial', '1');
		var fileExt = $('#' + fileTypeId).val();
		// 严格限制附件的类型
		//		if (!fileExt) {
		//			fileExt = '*.jpg;*.jpeg;*.gif;*.png;*.bmp;*.txt;*.doc;*.docx;*.xls;*.xlsx';
		//		}

		// Script
		var script = '/UserControl/FileUpload/FileUpload.ashx';
		if ($('#' + scriptId).val()) {
			script = '/UserControl/FileUpload/' + $('#' + scriptId).val();
		}

		// ScriptData
		var scriptData = {};
		if ($('#' + scriptDataId).val()) {
			scriptData = eval($('#' + scriptDataId).val())[0]
		}

		$("#" + uploadifyId).uploadify({
			'uploader': '../../../UserControl/FileUpload/jquery.uploadify-v2.1.4/uploadify.swf',
			'script': script,
			'scriptData': scriptData,
			'method': 'GET',
			'cancelImg': '../../../UserControl/FileUpload/jquery.uploadify-v2.1.4/cancel.png',
			'folder': folder,
			'queueID': prefix + 'fileQueue',
			'auto': false,
			'multi': true,
			'buttonImg': '../../../UserControl/FileUpload/jquery.uploadify-v2.1.4/Browse.jpg',
			'fileExt': fileExt,
			'fileDesc': fileExt,
			'onComplete': fileComplete,
			'onSelect': fileSelect
		});

		//初始化图片大小  
		$('#' + prefix + 'uploadifyUploader').css({ width: '88px', height: '27px' });

		//Close
		$('#' + prefix + 'closeDialog').click(function () {
			$("#" + dialogId).css('display', '').window('close');
			if ($('#' + onClose).val()) {
				var closeScript = $('#' + onClose).val() + '();'
				eval(closeScript);
			}
		});

		//“上传”按钮事件
		$('#' + prefix + 'imgUp').click(function () {
			$('#' + prefix + 'uploadify').uploadifyUpload()
		});
		//“取消”按钮事件
		$('#' + prefix + 'imgCancle').click(function () {
			$('#' + prefix + 'uploadify').uploadifyClearQueue()
		});
	}

	function fileComplete(event, ID, fileObj, response, data) {
		// 图片上传后，把名称显示在文本框中
		var $txtFile = $('#' + txtFileId);
		if (!$txtFile.val()) {
			$txtFile.val(fileObj.name)
		} else {
			if ($txtFile.val().indexOf(fileObj.name) == -1) {
				$txtFile.val($txtFile.val() + ',' + fileObj.name);
			}
		}

		// 添加客户端的OnComplete事件
		if ($('#' + onComplete).val()) {
			var completeFunction = $('#' + onComplete).val() + '(event, ID, fileObj, response, data);';
			eval(completeFunction);
		}
	}

	// 更新上传路径
	function fileSelect(event, queueId, fileObj) {
		var folder2 = $('#' + folderId).val();
		if ($('#' + recordCodeId).val()) {
			// 上传路径+=具体单据的ID
			folder2 += '/' + $('#' + recordCodeId).val();
		}
		$('#' + uploadifyId).uploadifySettings('folder', folder2);
	}

	addManageEvent(manageId, folderId, recordCodeId, txtFileId);
}

function addManageEvent(manageId, folderId, recordCodeId, txtFileId) {
	if ($('#' + manageId).attr('isBindClick') == 1)
		return false;
	$('#' + manageId).attr('isBindClick', '1');

	$('#' + manageId).click(function () {
		// 文件上传路径
		var folder = $('#' + folderId).val();
		if ($('#' + recordCodeId).val()) {
			// 上传路径+=具体单据的ID
			folder += '/' + $('#' + recordCodeId).val();
		}
		// 合并文件的路径
		var prefix = manageId.substring(0, manageId.indexOf('_') + 1);
		var mergerFolderId = prefix + 'hfldMergerFolder'; //保存合并目录隐藏控件Id
		var mergerFolder = $('#' + mergerFolderId).val(); //合并目录
		$('table[id$=annexTable] tr[class!=header]').remove();
		$.getJSON('/UserControl/FileUpload/GetFiles.ashx?' + new Date().getTime(), { Path: folder, merger: mergerFolder }, function (data) {
			$.each(data, function (intex, annex) {
				var $tdName = $('<td></td>')
				var $a = $('<a target="_blank" style="cursor:pointer" class="linkAnnex"></a>')
				$a.attr('href', jw.downloadUrl + "?path=" + annex.Path + '/' + annex.Name);
				$a.append(decodeURIComponent(annex.Name));
				$tdName.append($a);
				var $tdLength = $('<td></td>')
				$tdLength.append(annex.Length);
				var readOnly = annex.ReadOnly;
				var $td = $('<td></td>');
				var $tr = $('<tr></tr>');
				if (!readOnly) {
					var $img = $('<img class="deleteAnnex" style="cursor:pointer" src="/images/cancel_2.png" alt="删除" />')
					$tr.attr('id', intex);
					$img.click(function () {
						var filePath = annex.Path;
						deleteAnnex(this, filePath, txtFileId);
					});
					$td.append($img);
				}
				$tr.append($tdName);
				$tr.append($tdLength);
				$tr.append($td);
				if (intex % 2 == 0) {
					$tr.addClass('rowa');
				} else {
					$tr.addClass('rowb');
				}
				$('table[id$=annexTable]').append($tr);
			});
		});
		$('#annexManage').css('display', '').window({ width: 420, height: 385, modal: true, title: $('#' + manageId).val() });

		function deleteAnnex(img, folder, txtFileId) {
			if (!confirm('系统提示：\n\n确定要删除吗！'))
				return false;
			var $tr = $(img).parent().parent();
			var annexName = $tr.find('a').html();
			$.get('/UserControl/FileUpload/DeleteFile.ashx?' + new Date().getTime(), { File: folder + '/' + annexName }, function (data) {
				if (data == '1') {
					$tr.remove();
					$('tr[id=' + $tr.attr('id') + ']').remove();
					var $txtFile = $('#' + txtFileId);
					if ($txtFile.val().indexOf(annexName + ',') > -1) {
						$txtFile.val($txtFile.val().replace(annexName + ',', ''));
					}
					else if ($txtFile.val().indexOf(annexName) > -1) {
						$txtFile.val($txtFile.val().replace(annexName, ''));
					}
				}
				else {
					alert('系统提示：\n\n删除失败！');
				}
			});
		}
	});
}

function updateRecordCode(id, recordCode) {
	var prefix = id + '_';
	var uploadifyId = prefix + 'uploadify';     //上传控件的Id
	var folderId = prefix + 'hfldFolder';       //保存存放路径的隐藏控件Id
	var txtFileId = prefix + 'txtFile';         //显示名称的输入框的Id
	var recordCodeId = prefix + 'hfldRecordCode';   //保存“RecordCode”的隐藏控件Id

	$('#' + recordCodeId).val(recordCode);
	// 文件上传路径
	var folder = $('#' + folderId).val();
	if ($('#' + recordCodeId).val()) {
		// 上传路径+=具体单据的ID
		folder += '/' + $('#' + recordCodeId).val();
	}
	setFileName(folder, txtFileId);
}

// 设置文本框里的文件名称
function setFileName(folder, txtFileId) {
	if ($('#' + txtFileId).css('display') != 'none') {
		$.get(
        '/UserControl/FileUpload/GetFileNames.ashx?' + new Date().getTime(),
        { Folder: folder },
        function (data) {
        	if (data && typeof data == 'string')		// FF显示object html
        		$('#' + txtFileId).val(data)
        }
     );
	}
}
