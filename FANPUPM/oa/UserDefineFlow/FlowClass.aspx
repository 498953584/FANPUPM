<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowClass.aspx.cs" Inherits="UserDefineFlow_FlowClass" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>自定义流程信息管理</title>
	<base target="_self" />
	<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('GVBook');
			replaceEmptyTable('emptyTable', 'GVBook');
			setButton(jwTable, 'btnDel', 'btnEdit', '', 'hfldId');
			setHeight();

			var BtnPrivilege = document.getElementById('BtnPrivilege');
			jwTable.registClickTrListener(function () {
				$('#hfldCheckedIds').val(this.id);
				$('#hfldBusiClass').val($(this).attr('busiClass'));
				if (this.id != '') {
					BtnPrivilege.removeAttribute('disabled');
				}
				else {
					BtnPrivilege.setAttribute('disabled', 'disabled');
				}
			});

			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				if (checkedChk.length == 1) {
					var tr1 = getFirstParentElement(checkedChk[0].parentNode, 'TR');
					BtnPrivilege.removeAttribute('disabled');
					$('#hfldCheckedIds').val(tr1.id);
					$('#hfldBusiClass').val($(tr1).attr('busiClass'));
				}
				else if (checkedChk.length > 1) {
					var ids = "";
					var classArr = new Array();
					for (var i = 0; i < checkedChk.length; i++) {
						var trs = getFirstParentElement(checkedChk[i].parentNode, 'TR');
						ids += trs.id + ',';
						classArr.push($(trs).attr('busiClass'));
					}
					$('#hfldCheckedIds').val(ids);
					$('#hfldBusiClass').val(jw.array1dToJson(classArr));
					BtnPrivilege.removeAttribute('disabled');
				}
				else {
					BtnPrivilege.setAttribute('disabled', 'disabled');
				}
			});

			jwTable.registClickAllCHKLitener(function () {
				if (jwTable.isCheckedAll()) {
					BtnPrivilege.removeAttribute('disabled');
					var checkedChk = jwTable.getAllChk();
					var ids = "";
					var classArr = new Array();
					for (var i = 0; i < checkedChk.length; i++) {
						var trs = getFirstParentElement(checkedChk[i].parentNode, 'TR');
						ids += trs.id + ',';
						classArr.push($(trs).attr('busiClass'));
					}
					document.getElementById('hfldCheckedIds').value = ids;
					$('#hfldBusiClass').val(jw.array1dToJson(classArr));
					BtnPrivilege.removeAttribute('disabled');
				}
				else {
					BtnPrivilege.setAttribute('disabled', 'disabled');
				}
			});
		});

		function ClickRow(BusinessCode, BusinessClass) {
			document.getElementById('btnEdit').disabled = false;
			document.getElementById('btnDel').disabled = false;
			document.getElementById('BtnPrivilege').disabled = false;
			document.getElementById('HdnBusinessCode').value = BusinessCode;
			document.getElementById('HdnBusinessClass').value = BusinessClass;
		}

		// 新标签形式打开
		function EditorAdd(op) {
			var Code = document.getElementById('HdnBusinessCode').value;
			var bclass = document.getElementById('HdnBusinessClass').value;
			var url = "/oa/UserDefineFlow/FlowClassEdit.aspx?t=" + op + "&cc=" + Code + "&cl=" + bclass + "&id=" + $('#hfldId').val();
			parent.desktop.flowclass = window;
			top.ui.openWin({ title: '自定义流程类别维护', url: url, width: 400, height: 220 });
		}

		// 分配人员
		function allocUser() {
			var url = '/Common/AllocUser.aspx?type=userDefineFlow&idJson=' + $('#hfldBusiClass').val();
			top.ui.openWin({ title: '设置权限', url: url, width: 650, height: 500 });
		}

		function setHeight() {
			var height = document.getElementById("td-fclass").clientHeight;
			document.getElementById("fclassdiv").style.height = height;
		}
	</script>
</head>
<body>
	<form id="form" runat="server">
	<table width="100%" style="height: 99%;">
		<tr>
			<td height="22px" class="divHeader">
				自定义流程
			</td>
		</tr>
		<tr>
			<td height="22px" style="text-align: left;">
				<input type="button" value="新增" onclick="EditorAdd('add');" id="btnAdd" />
				<input type="button" value="修改" onclick="EditorAdd('upd');" id="btnEdit" disabled="disabled" />
				<asp:Button ID="btnDel" Text="删 除" Enabled="false" Height="21px" OnClick="btnDel_Click" runat="server" />
				<input type="button" value="权限设定" onclick="allocUser();" id="BtnPrivilege" style="width: 70px" />
				<input id="HdnBusinessCode" style="width: 20px" value="999" type="hidden" runat="server" />

				<input id="HdnBusinessClass" style="width: 20px" type="hidden" runat="server" />

			</td>
		</tr>
		<tr>
			<td valign="top" id="td-fclass">
				<input id="hdnid" type="hidden" runat="server" />

				<div style="overflow: auto; width: 100%; height: 100%" id="fclassdiv">
					<asp:SqlDataSource ID="SQLDataSource" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [WF_Business_Class] WHERE ([BusinessCode] = @BusinessCode) ORDER BY [BusinessClass]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:Parameter DefaultValue="999" Name="BusinessCode" Type="String" /></SelectParameters></asp:SqlDataSource>
					<asp:GridView ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" PageSize="25" CssClass="gvdata" DataSourceID="SQLDataSource" OnRowDataBound="GVBook_RowDataBound" DataKeyNames="Id,BusinessClass,BusinessCode" runat="server">
<EmptyDataTemplate>
							<table id="emptyTable">
								<tr align="center" class="header">
									<th align="center" nowrap="nowrap" scope="col" style="width: 40px">
										序号
									</th>
									<th align="center" nowrap="nowrap" scope="col" style="width: 70px">
										流程编号
									</th>
									<th align="center" nowrap="nowrap" scope="col">
										流程名称
									</th>
								</tr>
							</table>
						</EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center"></HeaderStyle><HeaderStyle CssClass="header"></HeaderStyle><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
									<asp:CheckBox ID="cbAllBox" runat="server" />
								</HeaderTemplate><ItemTemplate>
									<asp:CheckBox ID="cbBox" runat="server" />
								</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="序号" HtmlEncode="false" /><asp:BoundField DataField="BusinessClass" HeaderText="流程编号" HtmlEncode="false" /><asp:BoundField DataField="BusinessClassName" HeaderText="流程名称" HtmlEncode="false" /><asp:TemplateField HeaderText="用户权限列表"><ItemTemplate>
									<asp:Label ID="lbName" Width="100%" runat="server"></asp:Label>
								</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><FooterStyle CssClass="footer"></FooterStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
				</div>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldId" runat="server" />
	<asp:HiddenField ID="hfldCheckedIds" runat="server" />
	<asp:HiddenField ID="hiddenuserCode" runat="server" />
	<asp:HiddenField ID="hiddenuserName" runat="server" />
	<asp:HiddenField ID="hfldBusiClass" runat="server" />
	</form>
</body>
</html>
