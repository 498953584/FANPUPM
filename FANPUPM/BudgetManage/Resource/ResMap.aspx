<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResMap.aspx.cs" Inherits="BudgetManage_Resource_ResMap" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_usercontrol_selectresource_ascx" Src="~/StockManage/UserControl/SelectResource.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>资源映射</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="../../Script/json2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 隐藏控件
			$('#btnDataBindRes').css('display', 'none');

			replaceEmptyTable('empty_gvwResMap', 'gvwChildRes'); // 替换
			$('#txtParentName').attr('readOnly', 'true');

			var jtRes = new JustWinTable('gvwChildRes');
			setButton(jtRes, 'btnDelete', '', '', 'hfldCheckedResId');

			$('#btnDelete').click(function () {
				return jw.confirm('系统提示', '您确定要删除吗？', 'btnDelete');
			});
		});

		function selectResource1(id, name) {
			var src = '/StockManage/UserControl/SelectResourceTemp.aspx?type=2&resourceName=&TypeCode=0002&callback=callback1&time=' + new Date().getTime();
			top.ui.openWin({ title: '选择资源', url: src, width: 1010, height: 499 });
		}

		// 选择资源后回调方法
		function callback1(json) {
			var res = JSON.parse(json);
			$('#hfldResourceId').val(res.id);
			$('#txtResourceName').val(res.name);
		}

		function selectResource2() {
			top.ui.callback = function (o) {
			    $('#hfldParentId').val(o.resId);
			    $('#txtParentName').val(o.resName);
			}
			var src = '/StockManage/UserControl/SelectResourceTemp.aspx?type=2&resourceName=&TypeCode=0002&callback=callback2&time=' + new Date().getTime();
			top.ui.openWin({ title: '选择资源', url: src, width: 1010, height: 499 });
		}

		// 选择资源后回调方法
		function callback2(json) {
			var res = JSON.parse(json);
			$('#hfldParentId').val(res.id);
			$('#txtParentName').val(res.name);
		}

		// 取消事件
		function cancel() {
			winclose('ResMap.aspx', 'ResMapList.aspx', false);
		}

		function add2() {
			var src = '/StockManage/UserControl/SelectResource.aspx?type=2&TypeCode=0';
			top.ui.callback = function (o) {
				$('#hfldReturnResId').val(o.id);
				$('#btnDataBindRes').click();
			}

			top.ui.openWin({ title: '选择资源', url: src, width: 1010, height: 499 });
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table class="tableContent2" style="width: 95%;" cellpadding="5px" cellspacing="0">
			<tr>
				<td style="width: 100px;">
					父资源
				</td>
				<td class="">
					<span class="spanSelect" style="width: 90%">
						<asp:TextBox ID="txtParentName" Style="width: 89%; height: 15px; border: none; line-height: 16px;
							margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
						<img alt="选择资源" onclick="selectResource2();" src="../../images/icon.bmp" style="float: right;" id="img1" runat="server" />

					</span>
					<asp:HiddenField ID="hfldParentId" runat="server" />
				</td>
			</tr>
			<tr>
				<td colspan="2">
					<hr class="sp" />
					
					<input type="button" id="btnAdd2" onclick="add2();" value="新增" />
					<asp:Button ID="btnDelete" Text="删除" Width="65px" OnClick="btnDelete_Click" runat="server" />
				</td>
			</tr>
			<tr>
				<td colspan="2">
					<asp:GridView ID="gvwChildRes" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwChildRes_RowDataBound" DataKeyNames="ResourceId" runat="server">
<EmptyDataTemplate>
							<table id="empty_gvwResMap" class="gvdata">
								<tr class="header">
									<th scope="col" style="width: 20px;">
										<input id="chk1" type="checkbox" />
									</th>
									<th scope="col" style="width: 25px;">
										序号
									</th>
									<th scope="col" style="width: 300px">
										资源编号
									</th>
									<th scope="col">
										资源名称
									</th>
									<th scope="col">
										型号
									</th>
									<th scope="col">
										品牌
									</th>
								</tr>
							</table>
						</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
									<asp:CheckBox ID="chkSelectAll" runat="server" />
								</HeaderTemplate>

<ItemTemplate>
									<asp:CheckBox ID="chkSelectSingle" runat="server" />
								</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="序号" DataField="No" HeaderStyle-Width="25px" /><asp:BoundField HeaderText="资源编号" DataField="ResourceCode" /><asp:BoundField HeaderText="资源名称" DataField="ResourceName" /><asp:BoundField HeaderText="型号" DataField="ModelNumber" /><asp:BoundField HeaderText="品牌" DataField="Brand" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</td>
			</tr>
		</table>
	</div>
	<div id="divSelectResource" title="选择" style="display: none;">
		<iframe id="ifResouece" frameborder="0" width="100%" height="100%"></iframe>
	</div>
	<div id="Div1" class="divFooter2" style="width: 100%" runat="server">
		<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
		<input type="button" id="btnCancel" value="取消" onclick="cancel()" />
	</div>
	<asp:Button ID="btnDataBindRes" Text="Button" OnClick="btnDataBindRes_Click" runat="server" />
	<!-- 存放所有子资源Id-->
	<asp:HiddenField ID="hfldChildResId" runat="server" />
	<!-- 存放选中的子资源Id-->
	<asp:HiddenField ID="hfldCheckedResId" runat="server" />
	<!-- 存放返回的子资源Id-->
	<asp:HiddenField ID="hfldReturnResId" runat="server" />
	</form>
	
</body>
</html>
