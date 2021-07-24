<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selectDatumClass.aspx.cs" Inherits="EPC_QuaitySafety_selectDatumClass" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server"><title>选择类型</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

	
	<style type="text/css">
		.tree-node-selected
		{
			color: White;
			background-color: #3399FF;
		}
	</style>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var data = eval($('#hfldDatumClassJson').val());
			$('#tree').tree({
				data: data,
				onClick: function (node) {
					$('#hfldDatumClassId').val(node.id);
					$('#hfldDatumClassName').val(node.text);
				},
				onDblClick: okEvent
			});
		});

		function okEvent() {
			if (typeof top.ui.callback == 'function') {
				top.ui.callback({ id: $('#hfldDatumClassId').val(), name: $('#hfldDatumClassName').val() })
			}
			top.ui.closeWin();
		}
	</script>
</head>
<body>
	<form id="Form1" method="post" runat="server">
	<div id="div_tree" style="height: 430px; overflow: auto;">
		<ul id="tree" style="">
		</ul>
	</div>
	<div style="text-align: right;">
		<input type="button" id="btnOk" onclick="okEvent();" value="保存" />
		<input type="button" id="Button1" onclick="top.ui.closeWin();;" value="取消" />
	</div>
	<asp:HiddenField ID="hfldDatumClassJson" runat="server" />
	<asp:HiddenField ID="hfldDatumClassId" runat="server" />
	<asp:HiddenField ID="hfldDatumClassName" runat="server" />
	</form>
</body>
</html>
