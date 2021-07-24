<%@ Page Language="C#" AutoEventWireup="true" CodeFile="moduleview.aspx.cs" Inherits="ModuleView" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>ModuleView</title>
	<script type="text/javascript" src="../../../Script/jquery.js"></script>
	<script src="../../../web_client/tree.js" language="javascript" type="text/javascript"></script>
	<script language="JavaScript" type="text/javascript">
		function clickRow(obj, moduleCode, isLeaf) {
			document.getElementById('hdnModuleCode').value = moduleCode;
			document.getElementById('btnAdd').disabled = false;
			document.getElementById('btnDel').disabled = !isLeaf;
			document.getElementById('btnEdit').disabled = (moduleCode.length == 0);
			//待办按钮
			document.getElementById("btnWaitingJob").disabled = !isLeaf;
			/*在这之前添加你的处理代码*/
			doClick(obj, 'grdModules'); //调用样式设置
		}
		function outRow(obj) {
			/*在这之前添加你的处理代码*/
			doMouseOut(obj); //调用样式设置
		}
		function overRow(obj) {
			/*在这之前添加你的处理代码*/
			doMouseOver(obj); //调用样式设置
		}

		function switchDisplay(obj, t1, t2) {
			/*在这之前添加你的处理代码*/
			doSwitchDisplay(obj, 'grdModules', 'hdnModuleCodeList', t1, t2, '../../../'); //调用样式设置
		}

		function ClickBtn(op) {
			var moduleCode = document.getElementById('hdnModuleCode').value;

			if (moduleCode.length > 12) {
				top.ui.alert('菜单嵌套太多，请重新划分菜单');
				return false;
			}

			var url = ""
			var re = true;
			switch (op.toLowerCase()) {
				case "add":
					url = "/oa/SysAdmin/Modules/editmodule.aspx?" + new Date().getTime() + "&mkdm=" + moduleCode + "&op=add";
					var obj = {
						url: url,
						width: 600,
						height: 300
					};
					top._openWin(obj);
					break;
				case "upd":
					url = "/oa/SysAdmin/Modules/editmodule.aspx?" + new Date().getTime() + "&mkdm=" + moduleCode + "&op=upd";
					var obj = {
						url: url,
						width: 600,
						height: 300
					};
					top._openWin(obj);
					break;
				case "del":
					re = confirm("确定要删除该模块吗？");
					break;
			}
			return re;
		}
		//
		function OpenUpAdd() {
			var MKDM = document.getElementById('hdnModuleCode').value;
			if (MKDM == "") {
				alert("请选择模块！");
			}
			else {
				var url = "/UploadFiles/uploadfile/AnnexList.aspx?" + new Date().getTime() + "&mid=59&rc=" + MKDM + "&at=0&type=2";
				window.showModalDialog(url, window, 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
			}
		}
		//弹出待办工作页面
		function WaitingJob() {
			var MKDM = document.getElementById('hdnModuleCode').value;
			if (MKDM == "") {
				alert("请选择模块！");
			}
			else {
				window.open("WaitingJob.aspx?" + new Date().getTime() + "&mkdm=" + MKDM, "", "left=200,top=200,width=700,height=300,titlebar=0,menubar=0,location=0,scrollbars=1,resizable=1;");

			}
		}
	</script>
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" scroll="no">
	<form id="Form1" method="post" runat="server">
	<table id="Table2" class="table-normal" height="100%" cellspacing="0" cellpadding="0"
		width="100%">
		<tr>
			<td width="20">
				<input class="input_hide" id="hdnModuleCode" type="hidden" name="hdnModuleCode" runat="server" />

			</td>
			<td valign="middle" align="right" height="24">
				<input type="button" id="btnAdd" class="button-normal" value="新 增" disabled="disabled" onclick="ClickBtn('add');" />
				&nbsp;
				<input type="button" id="btnEdit" class="button-normal" value="编 辑" disabled="disabled" onclick="ClickBtn('upd');"/>
				&nbsp;<asp:Button ID="btnDel" CssClass="button-normal" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />&nbsp;
				<button id="btnWaitingJob" disabled onclick="WaitingJob();" type="button" style="display: none;"
					class="button-normal">
					待办工作</button>
			</td>
		</tr>
		<tr>
			<td valign="top" align="center" colspan="2">
				<div id="dvModulesBox" class="gridBox">
					<asp:DataGrid ID="grdModules" DataKeyField="c_mkdm" AutoGenerateColumns="false" CellPadding="0" Width="100%" CssClass="grid" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="名称">
<ItemTemplate>
									<asp:Label ID="Label1" name="Label1" Text='<%# Convert.ToString(Eval("HeadImg")) %>' runat="server"></asp:Label>
									<asp:Label ID="Label2" name="Label2" Text='<%# Convert.ToString(Eval("v_mkmc")) %>' runat="server"></asp:Label>
								</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="c_mkdm" HeaderText="代码"></asp:BoundColumn><asp:BoundColumn DataField="v_cdlj" HeaderText="路径"></asp:BoundColumn><asp:BoundColumn DataField="IsBasic" HeaderText="基本权限"></asp:BoundColumn><asp:BoundColumn DataField="IsMaintainable" HeaderText="维护页面" Visible="false"></asp:BoundColumn></Columns></asp:DataGrid></div>
			</td>
		</tr>
	</table>
	<input id="hdnModuleCodeList" style="width: 10px" type="hidden" name="hdnModuleCodeList" runat="server" />

	<JWControl:PersistScrollPosition ID="PersistScrollPosition2" ControlToPersist="dvModulesBox" runat="server">
	</JWControl:PersistScrollPosition>
	</form>
</body>
</html>
