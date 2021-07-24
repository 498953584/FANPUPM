<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContractType.ascx.cs" Inherits="ContractManage_UserControl_ContractType" %>

<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
<script type="text/javascript">
	$(document).ready(function () {
		//        $('span[id$=spanContractType]').width($('input[id$=hfldTypeID]').val());
		$('input[id$=txtTypeName]').width($('span[id$=spanContractType]').width() - 31);
		var inputs = document.getElementsByTagName('INPUT');
		var inputWidth;
		for (var i = 0; i < inputs.length; i++) {
			if (inputs[i].id.indexOf('txtTypeName') >= 0) {
				inputWidth = inputs[i].style.width;
			}
		}
	});

	function selectContractType(element) {
		var typeNameControl = element.id;
		if (element.nodeName == 'IMG') {
			typeNameControl = element.previousSibling.previousSibling.id;
		}
		var typeIdControl = typeNameControl.replace('txtTypeName', 'hfldTypeID');

		var url = '/ContractManage/UserControl/ContractType.aspx?TypeNameControl=' + typeNameControl + '&TypeIdControl=' + typeIdControl;
		top.ui.callback = function (type) {
			$('#' + typeIdControl).val(type.id);
			$('#' + typeNameControl).val(type.name);
		}
		top.ui.openWin({ title: '选择合同类型', url: url, width: 700 });

	}
</script>
<span id="spanContractType" class="spanSelect" runat="server">
	<asp:TextBox ID="txtTypeName" runat="server"></asp:TextBox>
	<img id="img" alt="请选择合同类型" src="~/images/icon.bmp" style="float: right" onclick="selectContractType(this);" runat="server" />

</span>
<div id="divSelectContractType" title="选择合同类型" style="display: none">
	<iframe id="ifrmSelectContractType" frameborder="0" width="100%" height="100%"></iframe>
</div>
<asp:HiddenField ID="hfldTypeID" runat="server" />
