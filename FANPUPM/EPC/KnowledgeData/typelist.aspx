<%@ Page Language="C#" AutoEventWireup="true" CodeFile="typelist.aspx.cs" Inherits="TypeList" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>TypeList</title>
	<script src="/Script/jquery.js" type="text/javascript"></script>
	<script src="../../web_client/tree.js" type="text/javascript"></script>
	<script src="../../../SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var purchasePlanTable = new JustWinTable('DG_List');
			if ($("#HiddenField1").val() == "EN") {
				$("#BtnDel").attr("disabled", "disabled");
				$("#BtnEdit").attr("disabled", "disabled");
			}
			if ($(".gvdata tr").size() == 1) {
				$("#BtnDel").attr("disabled", "disabled");
				$("#BtnEdit").attr("disabled", "disabled");
			}
		});

		function clickRow(obj, obj2) {
			document.getElementById('hdnType').value = obj;
			if (document.getElementById('BtnEdit') != null) {
				document.getElementById('BtnEdit').disabled = false;
			}
			if (obj2 == "True") {
				document.getElementById('BtnDel').disabled = false;
			}
			else {
				document.getElementById('BtnDel').disabled = true;
			}
			$.ajax({
				type: "POST",
				url: "Handler.ashx",
				data: "TYPE_ID=" + obj + "&r=" + Math.random(),
				success: function (msg) {
					var s = msg.split("|");
					if (s[0].toString() == "false") {
						document.getElementById('BtnDel').disabled = true;
					} else if (s[0].toString() == "true") {
						document.getElementById('BtnDel').disabled = false;
					}
				}
			});
		}
		function OpType(obj) {
			var TypeId = document.getElementById('hdnType').value;
			var ParentId = document.getElementById('hdnParentId').value;
			var title;
			var url;
			var refresh = false;
			var type = obj
			switch (type) {
				case "SEE":
					url = "/EPC/KnowledgeData/ClassAdd.aspx?ID=" + TypeId + "&optype=SEE";
					title = "查看资料类别";
					break;
				case "EDIT":
					url = "/EPC/KnowledgeData/ClassAdd.aspx?ID=" + TypeId + "&optype=EDIT";
					title = "编辑资料类别";
					break;
				case "ADD":
					url = "/EPC/KnowledgeData/ClassAdd.aspx?optype=ADD&ParId=" + ParentId + "";
					title = "新增资料类别";
					break;
			}
			top.ui._knowlegedatatype = window;
			toolbox_oncommand(url, title); //引用js
			if (refresh == true) {
				return true;
			}
			else {
				return false;
			}
		}		
	</script>
	<style type="text/css">
		.dgheader
		{
			background-color: #EEF2F5;
			white-space: nowrap;
			overflow: hidden;
			font-weight: normal;
			text-align: center;
			border-color: #CADEED;
			color: #727FAA;
			padding-left: 6px;
			padding-right: 6px;
		}
	</style>
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
	<form id="Form1" method="post" runat="server">
	<asp:HiddenField ID="HiddenField1" runat="server" />
	<table border="0" cellpadding="0" cellspacing="0" id="Table1" width="100%">
		
		<tr>
			<td class="divFooter" style="text-align: left;">
				<input id="hdnParentId" type="hidden" runat="server" />

				<input id="hdnType" type="hidden" name="hdnType" runat="server" />

				&nbsp;&nbsp;&nbsp;
				<asp:Button ID="BtnAdd" Text="新  增" Enabled="false" runat="server" />&nbsp;
				<asp:Button ID="BtnEdit" Text="编  辑" Enabled="false" OnClick="BtnEdit_Click" runat="server" />&nbsp;
				<asp:Button ID="BtnDel" Text="删  除" Enabled="false" OnClick="BtnDel_Click" runat="server" />
			</td>
		</tr>
		<tr>
			<td colspan="2" height="100%" valign="top">
				<div>
					<asp:DataGrid ID="DG_List" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" DataKeyField="TypeId" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="dgheader"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号"></asp:TemplateColumn><asp:BoundColumn DataField="TypeName" HeaderText="分类名"></asp:BoundColumn><asp:BoundColumn DataField="Remark" HeaderText="说明"></asp:BoundColumn><asp:BoundColumn DataField="IsVisible" HeaderText="可见性" Visible="false"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="IsDelete" ReadOnly="true"></asp:BoundColumn></Columns></asp:DataGrid></div>
				<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
