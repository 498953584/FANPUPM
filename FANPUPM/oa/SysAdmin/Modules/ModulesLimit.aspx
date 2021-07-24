<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModulesLimit.aspx.cs" Inherits="ModuleView" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>ModuleView</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script src="../../../web_client/tree.js" language="javascript" type="text/javascript"></script>
	<script language="JavaScript" type="text/javascript">
		function clickRow(obj, moduleCode, isLeaf) {
			document.getElementById('hdnModuleCode').value = moduleCode;
			doClick(obj, 'gvModules'); //调用样式设置
		}

		function outRow(obj) {
			doMouseOut(obj); //调用样式设置
		}

		function overRow(obj) {
			doMouseOver(obj); //调用样式设置
		}

		function switchDisplay(obj, t1, t2) {
			doSwitchDisplay(obj, 'gvModules', 'hdnModuleCodeList', t1, t2, '../../../'); //调用样式设置
		}

		// 分配人员
		function allocUser(mk) {
			var url = '/Common/AllocUser.aspx?type=mk&idJson=' + mk;
			top.ui.openWin({ title: '选择人员', url: url });
		}


		// 分配角色
		function allocRole(mk) {
			var url = '/Common/AllocRole.aspx?tableName=PT_MK&dataId=' + mk;
			top.ui.openWin({ title: '选择角色', url: url });
		}

			
	</script>
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" scroll="no">
	<form id="Form1" method="post" runat="server">
	<table id="Table2" class="table-normal" height="100%" cellspacing="0" cellpadding="0"
		width="100%">
		<tr>
			<td valign="top" align="center" colspan="2">
				<div id="dvModulesBox" class="gridBox">
					<asp:GridView ID="gvModules" AutoGenerateColumns="false" CellPadding="0" Width="100%" CssClass="grid" OnRowDataBound="gvModules_RowDataBound" DataKeyNames="c_mkdm" runat="server"><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="名称" HeaderStyle-Width="120px"><ItemTemplate>
									<asp:Label ID="Label1" name="Label1" Text='<%# Convert.ToString(Eval("HeadImg")) %>' runat="server"></asp:Label>
									<asp:Label ID="Label2" name="Label2" Text='<%# Convert.ToString(Eval("v_mkmc")) %>' runat="server"></asp:Label></ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="人员" HeaderStyle-Width="45%" DataField="UserName" HeaderStyle-Wrap="true" /><asp:BoundField HeaderText="角色" HeaderStyle-Width="30%" DataField="Role" HeaderStyle-Wrap="true" /><asp:TemplateField ItemStyle-Width="80px"><ItemTemplate>
									<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" runat="server">
										<span  style="cursor: pointer;	text-decoration: underline;	color: Blue;" onclick="allocUser('<%# Eval("c_mkdm").ToString() %>')">
										分配人员
										</span>
									</asp:HyperLink>
								</ItemTemplate></asp:TemplateField><asp:TemplateField ItemStyle-Width="80px"><ItemTemplate>
									<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" runat="server">
										<span  style="cursor: pointer;	text-decoration: underline;	color: Blue;" onclick="allocRole('<%# Eval("c_mkdm").ToString() %>')">
										分配角色
										</span>
									</asp:HyperLink>
								</ItemTemplate></asp:TemplateField></Columns></asp:GridView>
				</div>
			</td>
		</tr>
	</table>
	<input class="input_hide" id="hdnModuleCode" type="hidden" name="hdnModuleCode" runat="server" />

	<input id="hdnModuleCodeList" style="width: 10px" type="hidden" name="hdnModuleCodeList" runat="server" />

	<JWControl:PersistScrollPosition ID="PersistScrollPosition2" ControlToPersist="dvModulesBox" runat="server">
	</JWControl:PersistScrollPosition>
	</form>
</body>
</html>
