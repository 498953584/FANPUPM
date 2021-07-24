<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExcelImport.aspx.cs" Inherits="BudgetManage_Budget_ExcelImport" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>Excel导入</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/jscript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../StockManage/script/ValidateInput.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/Budget/BudgetPait.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			//多选WBS
			var gvBudget = new JustWinTable('gvwWBSData');
			setButton2(gvBudget, 'hfldWBSChecked');
			//多选Res
			var gvBudget = new JustWinTable('gvwResData');
			setButton2(gvBudget, 'hfldResChecked');
			setWidthHeight();

			$('#gvwWBSData select').bind('change', function () {
				var $cSelect = $(this);
				var $cOption = $cSelect.find('option:selected')
				$('#gvwWBSData select').each(function () {
					if ($(this).attr('id') != $cSelect.attr('id')) {
						if ($(this).find('option:selected').val() == $cOption.val()) {
							if ($cOption.val() != 'Invalid')
								$(this).find('option')[0].selected = true;
						}
					}
				});
				getSelectValue('gvwWBSData select', 'hfldExcelColumns');
			});

			$('#gvwResData select').bind('change', function () {
				var $cSelect2 = $(this);
				var $cOption2 = $cSelect2.find('option:selected')
				$('#gvwResData select').each(function () {
					if ($(this).attr('id') != $cSelect2.attr('id')) {
						if ($(this).find('option:selected').val() == $cOption2.val()) {
							if ($cOption2.val() != 'Invalid')
								$(this).find('option')[0].selected = true;
						}
					}
				});
				getSelectValue('gvwResData select', 'hfldResource');
			});
			//导入数据
			$('#btmImport').click(function () {
				var columns = getSelectValue('gvwWBSData select', 'hfldExcelColumns');
				if (columns.length <= $('#gvwWBSData select').length) {
					top.ui.alert('WBS信息：请选择要绑定的列！');
					return false;
				} else if (columns.indexOf('TaskCode') == -1) {
					top.ui.alert('"WBS信息：清单编码"必需选择！');
					return false;
				} else if (columns.indexOf('TaskName') == -1) {
					top.ui.alert('"WBS信息：项目名称"必需选择！');
					return false;
				} else if (columns.indexOf('Unit') == -1) {
					top.ui.alert('"WBS信息：单位"必需选择！');
					return false;
				} else {
					//资源
					if ($('#hfldValidRes').val() != "0") {
						var columnsRe = getSelectValue('gvwResData select', 'hfldResource');
						if (columnsRe.length <= $('#gvwResData select').length) {
							top.ui.alert('资源信息：请选择要绑定的列！');
							return false;
						} else if (columnsRe.indexOf('ResourceName') == -1) {
							top.ui.alert('"资源信息：资源名称"必需选择！');
							return false;
						} else if (columnsRe.indexOf('Specification') == -1) {
							top.ui.alert('"资源信息：规格"必需选择！');
							return false;
						} else if (columnsRe.indexOf('ModelNumber') == -1) {
							top.ui.alert('"资源信息：型号"必需选择！');
							return false;
						} else if (columnsRe.indexOf('Quantity') == -1) {
							top.ui.alert('"资源信息：数量"必需选择！');
							return false;
						} else if (columnsRe.indexOf('UnitPrice') == -1) {
							top.ui.alert('"资源信息：单价"必需选择！');
							return false;
						}
						if ($('#hfldIsWBSRelevance').val() == '1') {
							if (columnsRe.indexOf('SerialNo') == -1) {
								//top.ui.alert('"资源信息：任务编码"必需选择！');
								//return false;
							}
						} else {
							if (columnsRe.indexOf('TechnicalParameter') == -1) {
								top.ui.alert('"资源信息：技术参数"必需选择！');
								return false;
							}
							else if (columnsRe.indexOf('Brand') == -1) {
								top.ui.alert('"资源信息：品牌"必需选择！');
								return false;
							}
							else if (columnsRe.indexOf('Unit') == -1) {
								top.ui.alert('"资源信息：单位"必需选择！');
								return false;
							}
						}
					}
					//if (getRequestParam('taskId').length == 0) {
					//    if (!confirm("系统提示：\n\n没有选择导入Excel的根节点，将会覆盖当前版本!!! \n\n是否继续导入Excel？")) {
					//        return false;
					//    }
					//}
				}
			});
		});

		function setWidthHeight() {
			$('#divWBS').width($(this).width() - 7);
			$('#divWBS').height($(this).height() - 50);
			$('#divRes').width($(this).width() - 7);
			$('#divRes').height($(this).height() - 50);
			$('#divError').width($(this).width() - 7);
			$('#divError').height($(this).height() - 50);
		}

		// 获取select 值
		function getSelectValue(selectType, hfldId) {
			var columns = '';
			$('#' + selectType).each(function () {
				columns = columns + $(this).find('option:selected').val() + ',';
			});
			columns = columns.substring(0, columns.length - 1);
			$('#' + hfldId).val(columns);
			return columns;
		}

		// 设置模板选项卡
		function setTem(num) {
			$('#hfldCurrentTabIndex').val(num);
			if (num == '0') {
				//模板
				$('#SpWBS').attr('class', 'temShow');
				$('#SpResource').attr('class', 'temHide');
				$('#SpError').attr('class', 'temHide');
				$('#WBSData').show();
				$('#ErrorData').hide();
				$('#ResData').hide();
			} else if (num == '1') {
				$('#SpResource').attr('class', 'temShow');
				$('#SpWBS').attr('class', 'temHide');
				$('#SpError').attr('class', 'temHide');
				$('#ResData').show();
				$('#ErrorData').hide();
				$('#WBSData').hide();
			} else if (num == '2') {
				$('#SpError').attr('class', 'temShow');
				$('#SpWBS').attr('class', 'temHide');
				$('#SpResource').attr('class', 'temHide');
				$('#ErrorData').show();
				$('#WBSData').hide();
				$('#ResData').hide();
			}
		}

		function setButton2(jwTable, hdChkId) {
			if (!jwTable.table) return;
			if (jwTable.table.firstChild.childNodes.length == 1) {
				//table中没有数据
				return;
			}
			jwTable.registClickTrListener(function () {
				if (hdChkId != '') {
					document.getElementById(hdChkId).value = this.id;
				}
				if (document.getElementById(hdChkId).value != '')
					document.getElementById('btnDel').removeAttribute('disabled');
			});
			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				if (checkedChk.length == 0) {
					if (hdChkId != '') {
						document.getElementById(hdChkId).value = "";
					}
					document.getElementById('btnDel').setAttribute('disabled', 'disabled')
				}
				else {
					var checkedChks = jwTable.getCheckedChk();
					if (hdChkId != '') {
						var taskId = jwTable.getCheckedChkIdJson(checkedChk);
						document.getElementById(hdChkId).value = taskId;
					}
					document.getElementById('btnDel').removeAttribute('disabled');
				}
			});
			jwTable.registClickAllCHKLitener(function () {
				if (this.checked) {
					if (hdChkId != '')
						document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
					var checkedChks = jwTable.getAllChk();
					document.getElementById('btnDel').removeAttribute('disabled');
				}
				else {
					document.getElementById('btnDel').setAttribute('disabled', 'disabled');
					if (hdChkId != '')
						document.getElementById(hdChkId).value = '';
				}
			});
		}
	</script>
	<style type="text/css">
		.temShow
		{
			height: 23px;
			width: 84px;
			border: 0px solid #aa0000;
			font-size: 12px;
			text-align: center;
			float: left;
			line-height: 25px;
			cursor: pointer;
			background-image: url('/images1/Res1.jpg');
			margin-left: 2px;
		}
		.temHide
		{
			height: 23px;
			width: 84px;
			border: 0px solid #aa0000;
			font-size: 12px;
			text-align: center;
			float: left;
			line-height: 25px;
			cursor: pointer;
			background-image: url('/images1/Res2.jpg');
			margin-left: 2px;
		}
		.tabdivgv
		{
			height: auto;
			border: solid 1px #d4d4d4;
			padding: 2px 2px 0px 2px;
		}
		td
		{
			cursor: default;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader" style="display: none;">
		<table class="tableHeader">
			<tr>
				<td>
					Excel导入
				</td>
			</tr>
		</table>
	</div>
	<table cellpadding="0px" cellspacing="0px" style="width: 100%; vertical-align: top;">
		<tr>
			<td class="divFooter" style="text-align: left;">
				<asp:FileUpload ID="fupExcel" BackColor="#FEFEF4" Height="20px" Width="300px" runat="server" />
				<asp:Button ID="btnConnectExcel" Style="width: 75px; cursor: pointer;" Text="连接数据" OnClientClick="setTem('0')" OnClick="btnConnectExcel_Click" runat="server" />
				<asp:Button ID="btmImport" Text="导入数据" Style="width: 75px; cursor: pointer;" Enabled="false" OnClick="btnImport_Click" runat="server" />
				<asp:Button ID="btnDel" OnClientClick="return confirm('您确定要删除吗?');" Text="删除" disabled="disabled" Style="cursor: pointer;" OnClick="btnDel_Click" runat="server" />
			</td>
		</tr>
		<tr style="vertical-align: top; height: 70%;">
			<td>
				<div style="height: 22px; line-height: 22px;">
					<span id="SpWBS" onclick="setTem('0')" class="temShow" style="margin-left: 0px;" runat="server">
						WBS信息</span> <span id="SpResource" onclick="setTem('1')" class="temHide" runat="server">
							资源信息</span> <span id="SpError" onclick="setTem('2')" class="temHide" runat="server">
								警告信息 </span>
				</div>
				<div id="WBSData" class="tabdivgv">
					<div id="divWBS" style="overflow: auto;">
						<asp:GridView ID="gvwWBSData" CssClass="gvdata" OnRowDataBound="gvwWBSData_RowDataBound" runat="server"><Columns><asp:TemplateField><ItemTemplate>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
										<asp:CheckBox ID="cbAllBox" runat="server" />
									</HeaderTemplate><ItemTemplate>
										<asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
									</ItemTemplate></asp:TemplateField></Columns></asp:GridView>
						<div style="float: left;">
							<webdiyer:AspNetPager ID="AspNetPagerWBS" Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPagerWBS_PageChanged" runat="server">
							</webdiyer:AspNetPager>
						</div>
					</div>
				</div>
				<div id="ResData" class="tabdivgv" style="display: none;">
					<div id="divRes" style="overflow: auto;">
						<asp:GridView ID="gvwResData" CssClass="gvdata" OnRowDataBound="gvwResData_RowDataBound" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
										<asp:CheckBox ID="cbAllBox" runat="server" />
									</HeaderTemplate><ItemTemplate>
										<asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
										<%# (this.AspNetPagerRes.CurrentPageIndex - 1) * this.AspNetPagerRes.PageSize + Container.DataItemIndex + 1 %>
									</ItemTemplate>
</asp:TemplateField></Columns></asp:GridView>
						<div style="float: left;">
							<webdiyer:AspNetPager ID="AspNetPagerRes" UrlPaging="false" Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPagerRes_PageChanged" runat="server">
							</webdiyer:AspNetPager>
						</div>
					</div>
				</div>
				<div id="ErrorData" class="tabdivgv" style="display: none;">
					<div id="divError" style="overflow: auto;">
						<asp:GridView ID="gvwError" CssClass="gvdata" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
										<%# (this.AspNetPagerError.CurrentPageIndex - 1) * this.AspNetPagerError.PageSize + Container.DataItemIndex + 1 %>
									</ItemTemplate>
</asp:TemplateField></Columns></asp:GridView>
						<div style="float: left;">
							<webdiyer:AspNetPager ID="AspNetPagerError" Style="float: left;" Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPagerError_PageChanged" runat="server">
							</webdiyer:AspNetPager>
						</div>
						<div style="clear: left;">
							<asp:Button ID="btnClose" Text="关闭" Visible="false" OnClick="btnClose_Click" runat="server" />
						</div>
					</div>
				</div>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldExcelName" runat="server" />
	<asp:HiddenField ID="hfldExcelColumns" runat="server" />
	<asp:HiddenField ID="hfldResource" runat="server" />
	<asp:HiddenField ID="hfldCurrentTabIndex" runat="server" />
	<asp:HiddenField ID="hfldValidRes" runat="server" />
	
	<asp:HiddenField ID="hfldWBSChecked" runat="server" />
	<asp:HiddenField ID="hfldResChecked" runat="server" />
	
	<asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
	</form>
</body>
</html>
