/* 
* 必须包含jquery-1.4.4.js,jw.js和jquery.ui.dialog.js的引用
* 2011-07-28 16:52 Bery
*/
function showFiles(folder, divId) {
	$('#' + divId + ' table tr:gt(0)').remove();
	$.getJSON('/UserControl/FileUpload/GetFiles.ashx?' + new Date().getTime(),
		{ Path: folder },
		function (data) {
			$('#' + divId + ' table').empty();
			$.each(data, function (intex, annex) {
				var $tdName = $('<td></td>')
				var $a = $('<a target="_blank" style="cursor:pointer" class="linkAnnex"></a>')
				$a.attr('href', jw.downloadUrl + "?path=" + folder + '/' + annex.Name);
				$a.append(decodeURIComponent(annex.Name));
				$tdName.append($a);
				var $tdLength = $('<td></td>')
				$tdLength.append(annex.Length);
				var $tr = $('<tr></tr>');
				$tr.append($tdName);
				$tr.append($tdLength);
				if (intex % 2 == 0) {
					$tr.addClass('rowa');
				} else {
					$tr.addClass('rowb');
				}
				$('#' + divId + ' table').append($tr);
			});
			$('#' + divId).show().dialog({ width: 420, height: 385, modal: true });
		});
}
