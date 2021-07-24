<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleCorpEdit.aspx.cs" Inherits="EPC_Workflow_RoleCorpEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
		window.name = "win";
		addEvent(window, 'load', function () {
			$('#btnBindData').hide();
			var table = new JustWinTable('gvCorpList');
			replaceEmptyTable('emptyCorpList', 'gvCorpList');
		});

		function selectCorp() {
			var url = "/oa/common/selDept.aspx?bmdmJson=" + $('#hfldCorpCode').val();
			var depInfo = { url: url, title: '选择部门', width: 350, height: 500, winNo: 2 };
			top.ui.callback = function (o) {
				$('#hfldCorpCode').val(o.bmdmJson);
				$('#btnBindData').click();
			};
			top.ui.openWin(depInfo);
		}

		function pickPerson() {
			var url = "/EPC/workFlow/SelectUser.aspx";
			top.ui.openWin({ title: '选择人员', url: url });
			top.ui.callback = function (user) {
				document.getElementById('hfldUserCode').value = user.code;
				document.getElementById('txtUserName').value = user.name;
			}
		}

		function ClickRow(pid) {
			document.getElementById('hdnCorpId').value = pid;
			document.getElementById('btnDelete').disabled = false;
		}
	</script>
</head>
<body>
	<form id="form1" target="win" runat="server">
	<div>
		<table cellspacing="0" width="100%" class="tableAudit">
			<tr>
				<td class="divHeader" colspan="4">
					部门相关角色设置
				</td>
			</tr>
			<tr>
				<td class="td-label" style="width: 40px">
					姓名
				</td>
				<td>
					<asp:TextBox ID="txtUserName" Width="50%" runat="server"></asp:TextBox>
					<asp:HiddenField ID="hfldUserCode" runat="server" />
					<input type="button" id="btnEmployeeCode" value="选 择" onclick="pickPerson()" runat="server" />

				</td>
				<td class="td-label">
					所负责的部门
				</td>
				<td>
					<input type="button" id="btnSelCorp" value="选择部门" style="width: 75px;" onclick="selectCorp()" runat="server" />

				</td>
			</tr>
			<tr>
				<td align="right" colspan="4">
					<asp:HiddenField ID="hdnCorpId" runat="server" />
					<asp:Button ID="btnDelete" Enabled="false" Text="删 除" OnClick="btnDelete_Click" runat="server" />
				</td>
			</tr>
			<tr>
				<td colspan="4">
					<asp:GridView ID="gvCorpList" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvCorpList_RowDataBound" DataKeyNames="i_bmdm" runat="server">
<EmptyDataTemplate>
							<table id="emptyCorpList" class="tab">
								<tr class="header">
									<th scope="col" style="width: 25px;">
										序号
									</th>
									<th scope="col">
										部门名称
									</th>
								</tr>
							</table>
						</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
									
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="部门名称">
<ItemTemplate>
									
								</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</td>
			</tr>
			<tr>
				<td colspan="4" style="text-align: right">
					<asp:Button ID="btnSave" Text="保 存" OnClick="btnSave_Click" runat="server" />&nbsp;<input
						id="BtnClose" onclick="top.ui.closeTab();" type="button" value="关 闭" />
				</td>
			</tr>
		</table>
	</div>
	<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	<asp:HiddenField ID="hfldCorpCode" runat="server" />
	<asp:Button ID="btnBindData" OnClick="btnBindData_Click" runat="server" />
	</form>
</body>
</html>
