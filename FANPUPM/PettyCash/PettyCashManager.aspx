<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PettyCashManager.aspx.cs" Inherits="PettyCash_PettyCashManager" %>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>备用金管理</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

	<style type="text/css" type="text/css">
		.tree-node-selected
		{
			color: White;
			background-color: #3399FF;
		}
	</style>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="Script/PettyCashManager.js"></script>
</head>
<body class="easyui-layout">
	<form id="form1" runat="server">
	<div data-options="region:'west',split:true,title:'部门人员'" style="width: 250px; padding: 10px;">
		<ul id="tree">
		</ul>
	</div>
	<div data-options="region:'center',title:'个人借款明细'">
		<iframe id="ifraCash" frameborder="0" width="100%" height="95%"></iframe>
	</div>
	<asp:HiddenField ID="hfldDepEmployeeJson" runat="server" />
	</form>
</body>
</html>
