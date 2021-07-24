$(document).ready(function () {
	//上面的GVW
	replaceEmptyTable('emptyResource', 'gvwResource');
	var resourceTable = new JustWinTable('gvwResource');
	jw.tooltip();
	resourceTable.registClickTrListener(function () {
		$('#btnSave').removeAttr('disabled');
		$('#hfldResource').val(this.id);
		$('#hfldResourceCode').val($(this).attr('code'));
		$('#hfldResourceName').val($(this).attr('name'));
		$('#hfldSpecification').val($(this).attr('specification'));
		$('#hfldCorpId').val($(this).attr('corpId'));
		$('#hfldCorpName').val($(this).attr('corpName'));
	});
	$('#btnCancel').click(function () {
		top.ui.closeWin();
	});
	$('#btnSave').click(function () {
		var resInfo = {
			resId: $('#hfldResource').val(), resName: $('#hfldResourceName').val(),
			specification: $('#hfldSpecification').val(), corpId: $('#hfldCorpId').val(),
			corpName: $('#hfldCorpName').val()
		};
		top.ui.callback(resInfo);
		top.ui.callback = null;
		top.ui.closeWin();
	});
});