$(document).ready(function () {
	var jwTable = new JustWinTable('gvEquipment');
	replaceEmptyTable('emptyEquipment', 'gvEquipment')
	jwTable.registClickTrListener(function () {
		$('#hfldId').val($(this).attr('id'));
		$('#hfldCode').val($(this).attr('code'));
		$('#hfldName').val($(this).attr('name'));
		$('#hfldSpecification').val($(this).attr('specification'));
		$('#hfldPurchasePrice').val($(this).attr('PurchasePrice'));
		$('#hfldPurchaseDate').val($(this).attr('purchaseDate'));
		$('#btnSave').removeAttr('disabled');
	});
	//取消
	$('#btnCancel').click(function () {
		top.ui.closeWin();
	});
	$('#btnSave').click(function () {
		var equInfo = {
			id: $('#hfldId').val(), code: $('#hfldCode').val(), name: $('#hfldName').val(),
			specification: $('#hfldSpecification').val(), PurchasePrice: $('#hfldPurchasePrice').val(),
			PurchaseDate: $('#hfldPurchaseDate').val()
		};
		top.ui.callback(equInfo);
		top.ui.callback = null;
		top.ui.closeWin();
	});
});