<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Employee.aspx.cs" Inherits="Employee" EnableEventValidation="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title></title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#btnSearch')[0].onclick = function () { if (!$('#Form1').form('validate')) return false; }

			var decCode = jw.getRequestParam('depCode');
		});

		function ClickRow(UserCode, deptID) {
			document.getElementById('btnEdit').disabled = false;
			document.getElementById('btnDel').disabled = false;
			document.getElementById('hdnUserCode').value = UserCode;
			document.getElementById('hdnDeptID').value = deptID;
			var userCode = getCookie('UserCode');
			if (userCode == "00000000") {
			    document.getElementById('btnEdit').disabled = false;
			    document.getElementById('btnDel').disabled = false;
			    //document.getElementById('btnReDel').disabled = false;
			}
			document.getElementById('btnRe').disabled = false;
		}

		// 新增或编辑员工
		function openEmployeeEdit(op) {
			var userCode = '';
			var url = '';
			var str = '';
			var title = '';
			var deptID = document.getElementById('hdnDeptID').value;
			if (deptID != "") {
				if (op == 2) {
				    userCode = document.getElementById('hdnUserCode').value;
					url = "/HR/Personnel/EmployeeEdit.aspx?t=" + op + "&cc=" + deptID + "&uc=" + userCode;
					str = "dialogHeight:500px;dialogWidth:800px;center:1;help:0;status:0;";
					title = '编辑员工信息';
				}
				else if (op == 3) {
					userCode = document.getElementById('hdnUserCode').value;
					url = "/HR/Personnel/EmployeeView.aspx?t=" + op + "&cc=" + deptID + "&uc=" + userCode;
					str = "dialogHeight:600px;dialogWidth:900px;center:1;help:0;status:0;";
					title = '查看员工信息';
				}
				else {
					url = "/HR/Personnel/EmployeeAdd.aspx?t=" + op + "&cc=" + deptID + "&uc=" + userCode;
					str = "dialogHeight:500px;dialogWidth:800px;center:1;help:0;status:0;";
					title = '新增员工信息';
				}

				top.ui._Employee = window;
				toolbox_oncommand(url, title)
			}
			else {
				alert('请选择部门!');
			}
		}
		function aa(op) {
			if (op == "2") {
				document.getElementById('btnAdd').disabled = false;
			}
			else {
				document.getElementById('btnAdd').disabled = true;
			}
		}
		function selrow() {
			try {
				window.document.getElementById('TVCorpt0').click();
			}
			catch (e) { }
		}
	</script>
</head>
<body class="body_frame" scroll="no">
	<form id="Form1" method="post" runat="server">
	<table class="table-none" id="Table1" height="100%" cellspacing="0" cellpadding="0"
		width="100%" border="1">
		<tr>
			<td valign="top" width="20%" height="100%">
				<div class="gridBox">
					<asp:TreeView ID="TVCorp" ShowLines="true" ExpandDepth="1" Width="100%" OnSelectedNodeChanged="TVCorp_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
				</div>
			</td>
			<td valign="top" width="80%">
				<table width="100%" height="100%" border="0" id="table2" class="table-normal">
					<tr>
						<td class="td-title" align="center" height="28">
							<asp:Label ID="LblTitle" Text="Label" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td align="left" height="28">
							<table class="queryTable" cellpadding="3px" cellspacing="0px">
								<tr>
									<td class="descTd">
										姓名
									</td>
									<td>
										<asp:TextBox ID="txtUserName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
									</td>
									<td>
										<asp:Button ID="btnSearch" Style="cursor: pointer;" OnClick="btnSearch_Click" runat="server" />
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr id="btnTr" height="24" runat="server"><td class="td-toolsbar" colspan="3" runat="server">
							<input type="hidden" id="hdnUserCode" name="hdnUserCode" style="width: 10px" runat="server" />

							<asp:Button ID="btnSelect" Text="选 择" Style="display: none" OnClick="btnSelect_Click" runat="server" />
							<asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
							<asp:Button ID="btnEdit" Text="修 改" disabled="False" OnClick="btnEdit_Click" runat="server" />
							<asp:Button ID="btnDel" Text="离 职" disabled="False" OnClick="btnDel_Click" runat="server" />
							<asp:Button ID="btnRe" Text="恢 复" ToolTip="回到职位" disabled="False" OnClick="btnRe_Click" runat="server" />
							
							<asp:Button ID="btnExp" CssClass="button-normal" Text="导 出" OnClick="btnExp_Click1" runat="server" />
						</td></tr>
					<tr>
						<td valign="top">
							<div style="overflow: auto; width: 100%; height: 100%">
								<asp:GridView ID="gvEmployeelist" AutoGenerateColumns="false" CssClass="grid" Width="100%" OnRowDataBound="gvEmployeelist_RowDataBound" DataKeyNames="v_yhdm" runat="server">
<EmptyDataTemplate>
										<table class="grid" cellspacing="0" rules="all" border="1" id="gvEmployeelist" style="border-collapse: collapse;">
											<tr class="grid_head">
												<th scope="col" style="width: 40px">
													序号
												</th>
												<th scope="col" style="width: 25%">
													部门名称
												</th>
												<th scope="col">
													姓名
												</th>
												<th scope="col" style="width: 15%">
													岗位
												</th>
												<th scope="col" style="width: 10%">
													入司日期
												</th>
												<th scope="col" style="width: 8%">
													年龄
												</th>
												<th scope="col" style="width: 10%">
													状态
												</th>
												<th scope="col" style="width: 10%">
													部门负责人
												</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>	
											</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="bmmc" HeaderText="部门名称" SortExpression="bmmc" /><asp:HyperLinkField DataTextField="v_xm" HeaderText="姓名" /><asp:BoundField DataField="DutyName" HeaderText="岗位" SortExpression="DutyName" /><asp:BoundField DataField="EnterCorpDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="入司日期" HtmlEncode="false" SortExpression="EnterCorpDate" /><asp:BoundField DataField="Age" HeaderText="年龄" SortExpression="Age" /><asp:BoundField DataField="StateName" HeaderText="状态" SortExpression="StateName" /><asp:BoundField DataField="IsChargeManName" HeaderText="部门负责人" SortExpression="StateName" /><asp:TemplateField HeaderText="离职日期" HeaderStyle-Width="70px"><ItemTemplate>
												
											</ItemTemplate></asp:TemplateField></Columns></asp:GridView>
								<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
								</webdiyer:AspNetPager>
								<input type="hidden" id="hdnDeptID" name="hdnDeptID" style="width: 10px" runat="server" />

							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldDepatId" runat="server" />
    <asp:HiddenField ID="hfldUserCode" runat="server" />
	<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	</form>
</body>
</html>
