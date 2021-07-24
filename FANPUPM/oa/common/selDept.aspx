<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selDept.aspx.cs" Inherits="selDept" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>部门选择</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script language="javascript" type="text/javascript">
		$(document).ready(function () {
			$('#tvDepartment :checkbox').click(function () {
				var div_id = this.id.replace("CheckBox", "Nodes");
				if (this.checked) {
					$('#' + div_id).find(':checkbox').attr('checked', true);
				} else {
					$('#' + div_id).find(':checkbox').attr('checked', false);
				}
			});
		});


		function initCheckBoxs() {
			var objs = window.document.getElementsByTagName("INPUT");

			for (i = 0; i < objs.length; i++) {
				if (objs[i].checked) {
					selChilden(objs[i]);
				}
			}
		}

		function selChilden(obj) {
			var d = obj.id; //获得当前checkbox的id;
			var e = d.replace("CheckBox", "Nodes"); //通过查看脚本信息,获得包含所有子节点div的id

			var div = window.document.getElementById(e); //获得div对象
			if (div != null)  //如果不为空则表示,存在子节点
			{
				var check = div.getElementsByTagName("INPUT"); //获得div中所有的已input开始的标记
				for (j = 0; j < check.length; j++) {
					check[j].checked = true;
					check[j].disabled = false;
				}
			}
		}

		function confirmDept(TreeView1) {
			var dept = window.dialogArguments;
			if (typeof (dept) != 'undefined') {
				dept[0] = "";
				dept[1] = "";
			}
		}

		function closeWin(code, name, codeJson, bmdmJson) {
			var winNo = jw.getRequestParam('winNo');
			if (typeof (top.ui) != 'undefined') {
				top.ui.closeWin({ winNo: winNo });
				top.ui.callback({ code: code, name: name, codeJson: codeJson, bmdmJson: bmdmJson });
			} else {
				window.returnValue = code + '*' + name;
				window.close();
			}
		}

		function cancel() {
			var winNo = jw.getRequestParam('winNo');
			if (typeof (top.ui) != 'undefined')
				top.ui.closeWin({ winNo: winNo });
			else
				window.close();
		}
	</script>
</head>
<body class="body_popup" scroll="no" onload="javascript:initCheckBoxs();">
	<form id="form1" runat="server">
	<div>
		<table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="1"
			style="border-collapse: collapse">
			<tr>
				<td>
					<div id="dvDeptBox" class="gridBox">
						<asp:TreeView ID="tvDepartment" ShowCheckBoxes="All" runat="server"></asp:TreeView>
					</div>
				</td>
			</tr>
			<tr>
				<td height="30" align="right">
					<table width="60%" height="100%">
						<tr align="center" valign="middle">
							<td>
								<input type="hidden" id="hdnValue" name="hdnValue" style="width: 10px" runat="server" />

								<input type="hidden" id="hdnText" name="hdnText" style="width: 10px" runat="server" />

								<asp:Button ID="btnSave" Text="确 定" CssClass="button-normal" OnClick="btnSave_Click" runat="server" />
							</td>
							<td>
								<input id="BtnCancel" type="button" value="关 闭" onclick="cancel();" class="button-normal">
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	</div>
	</form>
</body>
</html>
