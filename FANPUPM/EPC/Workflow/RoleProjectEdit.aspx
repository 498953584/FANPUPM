<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleProjectEdit.aspx.cs" Inherits="ModuleSet_Workflow_RoleProjectEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目相关角色设置</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			window.name = "win";
			var jwTable = new JustWinTable('GVProjectList');
			replaceEmptyTable('emptyTable', 'GVProjectList');
			$('#btnSelProject').css('display', 'none');
		});

		// 选择人员
		function pickPerson() {
			var url = "";
			url = "/EPC/Workflow/SelectUser.aspx";
			top.ui.openWin({ title: '选择任意', url: url });
			top.ui.callback = function (user) {
				$('#hdnUserCode').val(user.code);
				$('#txtUserName').val(user.name);
			}
		}

		// 选择项目
		function selproject() {
			var projectcode = document.getElementById('hdnProjectCode').value;
			var url = "/EPC/Workflow/SelProject.aspx?pc=" + projectcode;
			top.ui.openWin({ title: '选择项目', url: url, width: 400, height: 490 });
			top.ui.callback = function (pro) {
				$('#hdnProjectCode').val(pro.code);
				$('#btnSelProject').click();
			}
		}
		function ClickRow(pid) {
			document.getElementById('hdnProjectId').value = pid;
			document.getElementById('btnDel').disabled = false;
		}
	</script>
</head>
<body scroll="no">
	<form id="form1" target="win" runat="server">
	<div>
		<table cellspacing="0" width="100%" class="tableAudit">
			<tr>
				<td class="divHeader" colspan="4">
					项目相关角色设置
				</td>
			</tr>
			<tr>
				<td class="td-label" style="width: 40px">
					姓名
				</td>
				<td>
					<asp:TextBox ID="txtUserName" Width="50%" runat="server"></asp:TextBox>
					<asp:HiddenField ID="hdnUserCode" runat="server" />
					<asp:Button ID="btnEmployeeCode" Text="选 择" runat="server" />
				</td>
				<td class="td-label">
					所负责的项目
				</td>
				<td>
					<input type="button" id="btnPickProject" value="选择项目" style="width: 80px;" onclick="selproject();" />
				</td>
			</tr>
			<tr>
				<td align="right" colspan="4">
					<asp:HiddenField ID="hdnProjectId" runat="server" />
					<asp:HiddenField ID="hdnProjectCode" runat="server" />
					<asp:Button ID="btnDel" Enabled="false" Text="删 除" OnClick="btnDel_Click" runat="server" />
				</td>
			</tr>
			<tr>
				<td colspan="4">
					<div style="overflow: auto; width: 100%;">
						<asp:GridView ID="GVProjectList" AllowPaging="true" AutoGenerateColumns="false" CssClass="gvdata" DataSourceID="SqlProjectList" Width="100%" OnRowDataBound="GVProjectList_RowDataBound" DataKeyNames="i_xh" runat="server">
<EmptyDataTemplate>
								<table width="100%" id="emptyTable">
									<tr class="header">
										<td>
											序号
										</td>
										<td>
											项目名称
										</td>
										<td>
											项目类别
										</td>
									</tr>
								</table>
							</EmptyDataTemplate>
<Columns><asp:BoundField DataField="i_xh" HeaderText="序 号" ReadOnly="true" SortExpression="i_xh" /><asp:BoundField DataField="PrjName" HeaderText="项目名称" SortExpression="PrjName" /><asp:BoundField DataField="ProjectClassCode" HeaderText="项目类别" SortExpression="ProjectClassCode" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
						<asp:SqlDataSource ID="SqlProjectList" SelectCommand="SELECT i_xh, PrjName, (SELECT CodeName FROM XPM_Basic_CodeList WHERE   (TypeID = 3) AND (IsValid = 1) and (CodeID = dbo.PT_PrjInfo.PrjKindClass)) AS ProjectClassCode FROM dbo.PT_PrjInfo" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
					</div>
				</td>
			</tr>
			<tr>
				<td colspan="4" style="text-align: right">
					<asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;<input
						id="BtnClose" onclick="top.ui.closeTab();" type="button" value="关 闭" />
				</td>
			</tr>
		</table>
	</div>
	<asp:Button ID="btnSelProject" Width="80px" Text="选择项目" OnClick="btnSelProject_Click" runat="server" />
	<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	</form>
</body>
</html>
