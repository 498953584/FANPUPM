<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelProject.aspx.cs" Inherits="ModuleSet_Workflow_SelProject" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>选择项目</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script language="javascript" type="text/javascript">
		window.name = "win";

		// 保存事件
		function success() {
			var chks = $('#TVProject input:checked');
			var len = chks.length;
			var codes = '';
			for (var i = 0; i < len; i++) {
				codes += $(chks[i]).attr('title') + ',';
			}
			if (codes.length > 0)
				codes = codes.substr(0, codes.length - 1);

			if (typeof top.ui.callback == 'function') {
				top.ui.callback({ code: codes });
				top.ui.callback = null;
			}
			top.ui.closeWin();
		}

		function public_GetParentByTagName(element, tagName) {
			var parent = element.parentNode;
			var upperTagName = tagName.toUpperCase();
			//如果这个元素还不是想要的tag就继续上溯 
			while (parent && (parent.tagName.toUpperCase() != upperTagName)) {
				parent = parent.parentNode ? parent.parentNode : parent.parentElement;
			}
			return parent;
		}

		//设置节点的父节点Cheched——该节点可访问，则他的父节点也必能访问 
		function setParentChecked(objNode) {
			var objParentDiv = public_GetParentByTagName(objNode, "div");
			if (objParentDiv == null || objParentDiv == "undefined") {
				return;
			}
			var objID = objParentDiv.getAttribute("ID");
			objID = objID.substring(0, objID.indexOf("Nodes"));
			objID = objID + "CheckBox";
			var objParentCheckBox = document.getElementById(objID);
			if (objParentCheckBox == null || objParentCheckBox == "undefined") {
				return;
			}
			if (objParentCheckBox.tagName != "INPUT" && objParentCheckBox.type == "checkbox")
				return;
			objParentCheckBox.checked = true;
			setParentChecked(objParentCheckBox);
		}

		//设置节点的子节点uncheched——该节点不可访问，则他的子节点也不能访问 
		function setChildUnChecked(divID) {
			var objchild = divID.children;
			var count = objchild.length;
			for (var i = 0; i < objchild.length; i++) {
				var tempObj = objchild[i];
				if (tempObj.tagName == "INPUT" && tempObj.type == "checkbox") {
					tempObj.checked = false;
				}
				setChildUnChecked(tempObj);
			}
		}

		//设置节点的子节点cheched——该节点可以访问，则他的子节点也都能访问 
		function setChildChecked(divID) {
			var objchild = divID.children;
			var count = objchild.length;
			for (var i = 0; i < objchild.length; i++) {
				var tempObj = objchild[i];
				if (tempObj.tagName == "INPUT" && tempObj.type == "checkbox") {
					tempObj.checked = true;
				}
				setChildChecked(tempObj);
			}
		}
		function CheckEvent() {

			var objNode = event.srcElement;

			if (objNode.tagName != "INPUT" || objNode.type != "checkbox")
				return;

			if (objNode.checked == true) {
				setParentChecked(objNode);
				var objID = objNode.getAttribute("ID");
				var objID = objID.substring(0, objID.indexOf("CheckBox"));
				var objParentDiv = document.getElementById(objID + "Nodes");
				if (objParentDiv == null || objParentDiv == "undefined") {
					return;
				}
				setChildChecked(objParentDiv);
			}
			else {
				var objID = objNode.getAttribute("ID");
				var objID = objID.substring(0, objID.indexOf("CheckBox"));
				var objParentDiv = document.getElementById(objID + "Nodes");
				if (objParentDiv == null || objParentDiv == "undefined") {
					return;
				}
				setChildUnChecked(objParentDiv);
			}
		}

		function btnOk_click() {
			$('#TVProject input:checked').each(function () {
				alert($(this).parent().parent().get(0).nodeName);
			})
		}
	</script>
</head>
<body>
	<form id="form1" target="win" runat="server">
	<table class="tab">
		<tr>
			<td style="width: 200px;" valign="top" height="20px">
				&nbsp;项目分类
				<asp:DropDownList ID="DDL_ProClass" AutoPostBack="true" OnSelectedIndexChanged="DDL_ProClass_SelectedIndexChanged" runat="server"></asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td valign="top" style="height: 400px; width: 200px;">
				<div class="pagediv">
					<asp:TreeView ID="TVProject" ShowCheckBoxes="All" ShowLines="true" Width="200px" runat="server"><SelectedNodeStyle ForeColor="Red" /></asp:TreeView>
				</div>
			</td>
		</tr>
		<tr align="left">
			<td style="text-align: right">
				
				
				<input type="button" id="btnok" value="保存" onclick="success();" />
				<input type="button" id="btnclose" value="关闭" onclick="top.ui.closeWin();" />
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="HdnProjectCodes" runat="server" />
	</form>
	<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
</body>
</html>
