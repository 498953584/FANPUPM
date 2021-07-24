<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipmentTypeList.aspx.cs" Inherits="Equ_Type_EquipmentTypeList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>设备类别设置</title><link type="text/css" rel="stylesheet" href="../../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../../Script/jw.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('gvwEquipmentType');
			setButton2(jwTable, 'btnDel', 'btnEdit', 'hfldId');
			$('#btnAdd').click(rowAdd);
			$('#btnEdit').click(rowEdit);
			function rowEdit() {
				var url = '/Equ/Type/EquipmentTypeEdit.aspx?id=' + $('#hfldId').val() + '&parentId=' + $('#hfldParentId').val();
				top.ui.openWin({ title: '编辑设备类别', url: url, width: 600, height: 250 });
			}
			function rowAdd() {
				var url = '/Equ/Type/EquipmentTypeEdit.aspx?parentId=' + $('#hfldParentId').val();
				top.ui.openWin({ title: '新增设备类别', url: url, width: 600, height: 250 });
			}
			// 显示被截取的信息
			jw.tooltip();
		});
		//提升管理员权限方法改为空
		function upAdminPrivilege() { }

		function setButton2(jwTable, btnDel, btnUpdate, hdChkId) {
			if (!jwTable.table) return;
			if (jwTable.table.getElementsByTagName('TR') == 1) {
				//table中没有数据
				return;
			}
			jwTable.registClickTrListener(function () {
				if (hdChkId != '')
					$('#' + hdChkId).val(this.id);

				if ($(this).attr('flag') == '') {
					$('#' + btnDel).removeAttr('disabled');
					$('#' + btnUpdate).removeAttr('disabled');
				}
				else {
					$('#' + btnDel).attr('disabled', 'disabled');
					$('#' + btnUpdate).attr('disabled', 'disabled');
				}
			});
			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				if (checkedChk.length == 0) {
					$('#' + btnDel).attr('disabled', 'disabled');
					$('#' + btnUpdate).attr('disabled', 'disabled');
					$('#btnUp').attr('disabled', 'disabled');
					$('#btnDown').attr('disabled', 'disabled');
				}
				else if (checkedChk.length == 1) {
					var trChecked = getFirstParentElement(checkedChk[0].parentNode, 'TR');
					if (hdChkId != '')
						$('#' + hdChkId).val(trChecked.id);
					if ($(trChecked).attr('flag') == '') {
						$('#' + btnDel).removeAttr('disabled');
						$('#' + btnUpdate).removeAttr('disabled');
					}
					else {
						$('#' + btnDel).attr('disabled', 'disabled');
						$('#' + btnUpdate).attr('disabled', 'disabled');
					}
				}
				else {
					$('#' + btnUpdate).attr('disabled', 'disabled');
					var isAllowDelArray = new Array();
					if (hdChkId != '')
						$('#' + hdChkId).val(jwTable.getCheckedChkIdJson(checkedChk));
					for (var i = 0; i < checkedChk.length; i++) {
						var trChecked = getFirstParentElement(checkedChk[i].parentNode, 'TR');
						isAllowDelArray.push($(trChecked).attr('flag'));
					}
					if (checkIsAllowDel(isAllowDelArray) == '1') {
						$('#' + btnDel).removeAttr('disabled');
					} else {
						$('#' + btnDel).attr('disabled', 'disabled');
					}
				}
			});
			jwTable.registClickAllCHKLitener(function () {
				$('#' + btnUpdate).attr('disabled', 'disabled');
				if (this.checked) {
					var isAllowDelArray = new Array();
					var checkedIdArray = new Array();
					$('#gvwEquipmentType [type=checkbox]').each(function () {
						if (this.id != 'chkAll') {
							var trSelected = getFirstParentElement(this.parentNode, 'TR');
							if ($(trSelected).attr('flag') != undefined) {
								isAllowDelArray.push($(trSelected).attr('flag'));
								checkedIdArray.push(trSelected.id);
							}
						}
					});
					if (hdChkId != '')
						$('#' + hdChkId).val(JSON.stringify(checkedIdArray));
					//判断选中的是否允许删除
					if (checkIsAllowDel(isAllowDelArray) == '1') {
						$('#' + btnDel).removeAttr('disabled');
					} else {
						$('#' + btnDel).attr('disabled', 'disabled');
					}
				}
				else {
					if (hdChkId != '')
						$('#' + hdChkId).val('');
					$('#' + btnDel).attr('disabled', 'disabled');
				}
			});
		}
		//遍历选中的是否允许删除
		function checkIsAllowDel(isAllowDelArray) {
			var isAllowDel = '1';
			for (var i = 0; i < isAllowDelArray.length; i++) {
				if (isAllowDelArray[i] != '') {
					isAllowDel = '0';
					break;
				}
			}
			return isAllowDel;
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table style="width: 100%; height: 89%;">
		<tr>
			<td style="width: 220px; vertical-align: top; height: 100%;">
				<div class="pagediv" style="width: 220px; height: 105%;" id="div1" runat="server">
					<asp:TreeView ID="trvwEquipmentType" ShowLines="true" ExpandDepth="2" OnSelectedNodeChanged="TreeView_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
				</div>
			</td>
			<td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
				<table border="0" class="tab">
					<tr>
						<td class="divFooter" style="text-align: left;">
							<input type="button" id="btnAdd" value="新增" />
							<input type="button" id="btnEdit" disabled="disabled" value="编辑" />
							<asp:Button ID="btnDel" Text="删  除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDel_Click" runat="server" />
						</td>
					</tr>
					<tr>
						<td style="height: 100%; vertical-align: top;">
							<div class="pagediv">
								<asp:GridView ID="gvwEquipmentType" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwEquipmentType_RowDataBound" DataKeyNames="Id,Flag" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
												<asp:CheckBox ID="cbAllBox" runat="server" />
											</HeaderTemplate><ItemTemplate>
												<asp:CheckBox ID="chkBox" ToolTip='<%# System.Convert.ToString(Eval("Id"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备类别编号"><ItemTemplate>
												
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备类别名称"><ItemTemplate>
												<span class="tooltip" title=''>
													
												</span>
											</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
								<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
								</webdiyer:AspNetPager>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	
	<asp:HiddenField ID="hfldParentId" runat="server" />
	
	<asp:HiddenField ID="hfldId" runat="server" />
	</form>
</body>
</html>
