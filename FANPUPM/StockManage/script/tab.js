// JScript 文件
function setSp(hdId, param) {
	var spans = getControlId('sp', 'span'); 	// 查找所有以sp开头的span控件
	var ids = spans.split(',');
	for (i = 0; i < ids.length; i++) {
		if (ids[i] != "") {
			if (!param) {
				document.getElementById(ids[i]).setAttribute("onclick", "setbkImg(this.id,'" + hdId + "')");

			}
			else {
				document.getElementById(ids[i]).setAttribute("onclick", "setbkImg(this.id,'" + hdId + "','" + param + "')");
			}
		}

	}

}
//获取查看页面URL
function getSrc(id) {
	var url = "";
	switch (id) {
		case "spModify":
			url = "/ContractManage/IncometModify/ViewModifyList.aspx?" + Math.random();
			break;
		case "spBalance":
			url = "/ContractManage/IncometBalance/ViewBalanceList.aspx?" + Math.random();
			break;
		case "spPayment":
			url = "/ContractManage/IncometPayment/ViewPaymentList.aspx?" + Math.random();
			break;
		case "spPaymentApply":
			url = "/ContractManage/PaymentApply/ViewPaymentApplyList.aspx?" + Math.random();
			break;
		case "spInvoice":
			url = "/ContractManage/IncometInvoice/ViewInvoiceList.aspx?" + Math.random();
			break;
		case "spPayoutModify":
			url = "/ContractManage/PayoutModify/PayoutModify.aspx?" + Math.random();
			break;
		case "spPayoutBalance":
			url = "/ContractManage/PayoutBalance/PayoutBalance.aspx?" + Math.random();
			break;
		case "spPayoutPayment":
			url = "/ContractManage/PayoutPayment/PayoutPayment.aspx?" + Math.random();
			break;
		case "spPayoutInvoice":
			url = "/ContractManage/PayoutInvoice/PayoutInvoice.aspx?" + Math.random();
			break;
		case "spMeasure":
			url = "/ContractManage/IncometContract/ContractMeasureList.aspx?" + Math.random();
			break;
		case "spPayoutCalc":
			url = "/ContractManage/PayoutPayment/PayoutCalcList.aspx?" + Math.random() + "&type=view";
			break;
	}
	return url;
}
//获取编辑页面URL
function getEditSrc(id) {
	var url = "";
	switch (id) {
		case "spModify":
			url = "/ContractManage/IncometModify/ShowModifyList.aspx?" + Math.random();
			break;
		case "spBalance":
			url = "/ContractManage/IncometBalance/ShowBalanceList.aspx?" + Math.random();
			break;
		case "spPayment":
			url = "/ContractManage/IncometPayment/ShowPaymentList.aspx?" + Math.random();
			break;
		case "spPaymentApply":
			url = "/ContractManage/PaymentApply/ShowPaymentApplyList.aspx?" + Math.random();
			break;
		case "spInvoice":
			url = "/ContractManage/IncometInvoice/ShowInvoiceList.aspx?" + Math.random();
			break;
		case "spPayoutModify":
			url = "/ContractManage/PayoutModify/PayoutModifyEdit.aspx?" + Math.random();
			break;
		case "spPayoutBalance":
			url = "/ContractManage/PayoutBalance/PayoutBalanceEdit.aspx?" + Math.random();
			break;
		case "spPayoutPayment":
			url = "/ContractManage/PayoutPayment/PayoutPaymentEdit.aspx?" + Math.random();
			break;
		case "spPayoutInvoice":
			url = "/ContractManage/PayoutInvoice/PayoutInvoiceEdit.aspx?" + Math.random();
			break;
		case "spMeasure":
			url = "/ContractManage/IncometContract/ContractMeasureList.aspx?" + Math.random();
			break;
		case "spPayoutCalc":
			url = "/ContractManage/PayoutPayment/PayoutCalcList.aspx?" + Math.random();
			break;
	}
	return url;
}

function setbkImg(objId, hdId, param) {
	var spans = getControlId('sp', 'span'); 	// 查找所有以sp开头的span控件
	var ids = spans.split(',');
	for (i = 0; i < ids.length; i++) {
		if (ids[i] != "")
			document.getElementById(ids[i]).setAttribute("class", "xxk");
	}
	if (objId != "") document.getElementById(objId).setAttribute("class", "xxkd");
	var spIds = getRequestParam("spId");
	var src = "";
	if (spIds == objId)
		src = getEditSrc(objId);
	else
		src = getSrc(objId);
	if (!param) {
		document.getElementById("framContract").src = src + "&ContractID=" + document.getElementById(hdId).value;
	}
	else {
		document.getElementById("framContract").src = src + "&ContractID=" + document.getElementById(hdId).value + "&" + param;
	}
}
   