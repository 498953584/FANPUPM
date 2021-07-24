<%@ Page Language="C#" AutoEventWireup="true" CodeFile="easyuitree.aspx.cs" Inherits="Z_Demo_easyuitree" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<title>Tree - jQuery EasyUI Demo</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/jquery.easyui.extension.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var data = eval($('#hfldProjectJson').val());
			$('#prj_tree').tree({
				data: data,
				onClick: function (node) {
					if (node.id.length == 36) {
						$('#ifracash').attr('src', "/pettycash/pettycashlist.aspx?type=manager&usercode=" + node.id);
					}
				},
				onBeforeExpand: function (node) {
					// node
					var childLeng = $('#prj_tree').tree('getChildren', node.target).length;
					if (childLeng == 0) {           // 如果没有子节点，则异步加载子节点
						var children = getChildrenJson(node.id);
						$('#prj_tree').tree('append', {
							parent: node.target,
							data: children
						});
					}
				}
			});
		});

		// 返回子节点的JSON对象
		function getChildrenJson(id) {
			var url = '';
			if (id.length == 36) {
				// 按父子项目显示
				url = '/PrjManager/Handler/GetSubProject.ashx?prjId=' + id + '&year=2013&date=' + new Date().getTime();
			}
			else {
				// 按项目状态显示
				url = '/PrjManager/Handler/GetSubProject.ashx?state=' + id + '&year=2013&date=' + new Date().getTime();
			}


			var json;
			$.ajax({
				type: 'GET',
				url: url,
				async: false,
				success: function (data) {
					json = data;
				},
				error: function () {
					alert('error');
				}
			});
			return eval(json);
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div data-options="region:'west',split:true,title:'项目'" style="width: 250px; padding: 10px;">
		<ul id="prj_tree">
		</ul>
	</div>
	<asp:HiddenField ID="hfldProjectJson" runat="server" />
	</form>
</body>
</html>
