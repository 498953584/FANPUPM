<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectFrame.aspx.cs" Inherits="StockManage_ProjectFrame" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/ProjectTree.js" charset="UTF-8"></script>
	<script type="text/javascript">

	</script>
</head>
<body class="easyui-layout">
	<form id="form1" style="overflow: hidden" runat="server">
	<div data-options="region:'west',split:true,title:''" style="width: 250px; padding: 10px;">
		<asp:DropDownList ID="dropYear" AutoPostBack="true" OnSelectedIndexChanged="dropYear_SelectedIndexChanged" runat="server"></asp:DropDownList>
		<asp:TextBox ID="txtPjr" Width="90px" runat="server"></asp:TextBox>
		<asp:Button ID="btnQuery" Text="查询" Width="40px" UseSubmitBehavior="true" OnClick="btnQuery_Click" runat="server" />
		<ul id="prj_tree">
		</ul>
	</div>
	<div data-options="region:'center',title:''">
		<iframe id="ifram" frameborder="0" width="100%" height="98%" runat="server"></iframe>
	</div>
	<asp:HiddenField ID="hfldStateType" Value="1" runat="server" />
	<asp:HiddenField ID="hfldProjectJson" runat="server" />
	<asp:HiddenField ID="hfldSubUrl" runat="server" />
	<asp:HiddenField ID="hfldContainsOrg" Value="0" runat="server" />
	</form>
</body>
</html>
