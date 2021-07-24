<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectByBud.aspx.cs" Inherits="StockManage_basicset_SelectByBud" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="../script/Config2.js"></script>
	<script type="text/javascript" src="../../Script/json2.js"></script>
	<script type="text/javascript" src="../script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#btnSertch')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			var arrRes = new Array();
			var gvResourceBud = new JustWinTable('gvResourceBud');
			showTooltip('tooltip', 25);
			//已经选中的材料获取以选择状态
			if ($('#hdTsid').val()) {
				$('#gvResourceBud tr:gt(0)').each(function () {
					arrRes = eval($('#hdTsid').val());
					for (var i = 0; i < arrRes.length; i++) {
					    if (arrRes[i].id == this.id && arrRes[i].resCode == this.resCode && arrRes[i].price == this.price && arrRes[i].number == this.number && arrRes[i].taskName == this.TaskName) {
							$(this).find('input[type=checkbox]').attr('checked', true);
							break;
						}
					}
				});
			}
			if (arrRes.length > 0) {
				$('#btnSave').removeAttr('disabled');
			} else {
				$('#btnSave').attr('disabled', 'disabled');
			}

			//单击行
			gvResourceBud.registClickTrListener(function () {
				arrRes.splice(0, arrRes.length);  //清空材料数组
				//将选中的材料数据保存在数组中
				var res = {};
				res.id = $(this).attr('id');
				res.resCode = $(this).attr('resCode');
				res.price = $(this).attr('price');
				res.number = $(this).attr('number');
				res.taskId = $(this).attr('TaskId');
				res.taskName = $(this).attr('TaskName');
				arrRes.push(res);
				var resInfos = JSON.stringify(arrRes);
				$('#hdTsid').val(resInfos);
				$('#btnSave').removeAttr('disabled');
			});

			//点击复选框
			gvResourceBud.registClickSingleCHKListener(function () {
				var checkedChk = gvResourceBud.getCheckedChk();
				//上一页选择的数据保存到材料数组中
				var checkedRes = $('#hdTsid').val();
				if (checkedRes != '') {
					arrRes = eval(checkedRes);
				}
				var tr = getFirstParentElement(this, 'TR');
				//如果是选择状态保存到材料数组
				if (this.checked) {
					var res = {};
					res.id = $(tr).attr('id');
					res.resCode = $(tr).attr('resCode');
					res.price = $(tr).attr('price');
					res.number = $(tr).attr('number');
					res.taskId = $(tr).attr('TaskId');
					res.taskName = $(tr).attr('TaskName');
					arrRes.push(res);
				} else if (this.checked == false) {
					//如果是取消选择状态从材料数组中删除
					var newArrRes = new Array();
					for (var i = 0; i < arrRes.length; i++) {
						if (arrRes[i].id == tr.id && arrRes[i].resCode == tr.resCode && arrRes[i].price == tr.price && arrRes[i].number == tr.number) {
						} else {
							var res = {};
							res.id = arrRes[i].id;
							res.resCode = arrRes[i].resCode;
							res.price = arrRes[i].price;
							res.number = arrRes[i].number;
							res.taskId = arrRes[i].taskId;
							res.taskName = arrRes[i].taskName;
							newArrRes.push(res);
						}
					}
					arrRes = newArrRes;
				}
				$('#hdTsid').val(JSON.stringify(arrRes));
				if (arrRes.length > 0) {
					$('#btnSave').removeAttr('disabled');
				} else {
					$('#btnSave').attr('disabled', 'disabled');
				}
			});

			//点击全选复选框
			gvResourceBud.registClickAllCHKLitener(function () {
				if (this.checked) {
					$('#gvResourceBud tr:gt(0)').each(function () {
						var res = {};
						res.id = $(this).attr('id');
						res.resCode = $(this).attr('resCode');
						res.price = $(this).attr('price');
						res.number = $(this).attr('number');
						res.taskId = $(this).attr('TaskId');
						res.taskName = $(this).attr('TaskName');
						arrRes.push(res);
						$('#hdTsid').val(JSON.stringify(arrRes));
					})
					$('#btnSave').removeAttr('disabled');
				} else {
					$('#btnSave').attr('disabled', 'disabled');
				}
			});
		});

		// 保存方法
		function getMaterialCode() {
			var resObj = eval($("#hdTsid").val());
			if (typeof top.ui.callback == 'function') {
				top.ui.callback(resObj);
				top.ui.callback = null;
			}
			top.ui.closeWin();
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table class="tab" cellspacing="0">
		<tr style="display: none;">
			<th class="headerrow">
				资源选择
			</th>
		</tr>
		<tr style="height: 30px;">
			<td>
				任务编码<asp:TextBox ID="txtTaskCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
				材料编码<asp:TextBox ID="txtResCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
				材料名称<asp:TextBox ID="txtResName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
				规格<asp:TextBox ID="txtSpecification" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
				品牌<asp:TextBox ID="txtBrand" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
				<asp:Button ID="btnSertch" Text="查询" OnClick="btnSertch_Click" runat="server" />
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top; height: 374px;">
				<div class="pagediv" style="height: 382px; width: 1000px;">
					<asp:GridView ID="gvResourceBud" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvResourceBud_RowDataBound" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
									<asp:CheckBox ID="chkSelectAll" runat="server" />
								</HeaderTemplate><ItemTemplate>
									<asp:CheckBox ID="chkSelectSingle" runat="server" />
									<asp:HiddenField ID="hdScode" Value='<%# System.Convert.ToString(Eval("ResourceCode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
									<asp:HiddenField ID="hdSprice" Value='<%# System.Convert.ToString(Eval("ResourcePrice"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
									<asp:HiddenField ID="hdNumber" Value='<%# System.Convert.ToString(Eval("ResourceQuantity"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
									<asp:HiddenField ID="hdResourceId" Value='<%# System.Convert.ToString(Eval("ResourceId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
									<%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="任务编码"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("Note").ToString() %>'>
										<%# StringUtility.GetStr(Eval("TaskCode").ToString()) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="任务名称"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("TaskName").ToString() %>'>
										<%# StringUtility.GetStr(Eval("TaskName").ToString()) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="材料编码"><ItemTemplate>
									<asp:Label ID="Label1" Text='<%# System.Convert.ToString(Eval("ResourceCode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="材料名称"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
										<%# StringUtility.GetStr(Eval("ResourceName").ToString()) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
										<%# StringUtility.GetStr(Eval("Specification").ToString()) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate>
									<asp:Label ID="lblUnit" Text='<%# System.Convert.ToString(Eval("Name"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="品牌"><ItemTemplate>
									<asp:Label ID="lblBrand" Text='<%# System.Convert.ToString(Eval("Brand"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="型号"><ItemTemplate>
									<asp:Label ID="lblType" Text='<%# System.Convert.ToString(Eval("ModelNumber"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="技术参数"><ItemTemplate>
									<asp:Label ID="lblTechnicalParameter" Text='<%# System.Convert.ToString(Eval("TechnicalParameter"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="价格" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
									<asp:Label ID="txtUnitPrice" Width="50px" Text='<%# System.Convert.ToString(Eval("ResourcePrice"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
									<asp:Label ID="txNumber" Width="50px" Text='<%# System.Convert.ToString(Eval("ResourceQuantity"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注" ItemStyle-Width="100px"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("Note").ToString() %>'>
										<%# StringUtility.GetStr(Eval("Note").ToString()) %>
									</span>
								</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
					</webdiyer:AspNetPager>
				</div>
			</td>
		</tr>
		<tr>
			<td class="bottonrow2" style="height: 30px" align="right">
				<input id="btnSave" type="button" value="保 存" style="cursor: pointer;" onclick="getMaterialCode();" disabled="true" runat="server" />

				<input id="btnCancel" type="button" value="取 消" style="cursor: pointer;" onclick="top.ui.closeWin();" />
				<input id="Hdnacode" type="hidden" style="width: 1px" runat="server" />

				<input id="HdnOperator" type="hidden" style="width: 1px" runat="server" />

			</td>
		</tr>
	</table>
	<input id="HdnIsPage" type="hidden" value="" style="width: 1px;" runat="server" />

	<asp:HiddenField ID="hdTsid" runat="server" />
	<asp:HiddenField ID="hdtp" Value="0" runat="server" />
	
	<asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
	</form>
</body>
</html>
