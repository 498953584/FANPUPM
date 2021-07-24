<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryConstructTask.aspx.cs" Inherits="BudgetManage_Construct_ConstructMain" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_usercontrol_selectrestask_ascx" Src="~/StockManage/UserControl/SelectResTask.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>施工报量</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />
<link href="../../StockManage/Skins/jquery.wysiwyg.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script src="/Script/jquery.js" type="text/javascript"></script>
	<script type="text/javascript" src="../../Script/Budget/BudgetPait.js"></script>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var gvTask = new JustWinTable('gvTask');
			$('#btnBindResTask').hide();
			showTooltip('tooltip', 25);
		})
		function addTask() {
			document.getElementById('SelectResource1_btnSelectResource').onclick('MultiTask', document.getElementById('hfldPrjId').value);
		}
		function selectResource(consTaskId) {
			parent.desktop.ResourceList = window;
			var url = "/BudgetManage/Construct/ResourceList.aspx?consTaskId=" + consTaskId + "&type=" + document.getElementById("hfldAddOrEdit").value + "&prjId=" + document.getElementById("hfldPrjId").value + "&reportId=" + document.getElementById("hfldReportId").value;
			toolbox_oncommand(url, "资源清单");
		}
		function theMonenyChange(index) {
			tb = document.getElementById('gvTask');
			var cells = tb.rows[parseFloat(index) + 1].cells;
			var Quantity = parseFloat(cells[5].children[0].value);
			cells[5].children[0].value = getFormat(Quantity);
		}
		//格式化三位小数
		function getFormat(num) {
			var numFormat;
			if (num.toFixed) {
				numFormat = num.toFixed(3);
			} else {
				var f3 = Math.pow(10, 3);
				numFormat = Math.round(num * f3) / f3;
			}
			return numFormat;
		}

		//添加行进行显示资源信息
		var prevId;
		function showInfo(id, taskId) {
			var tab_Resource = null;
			$.ajax({
				type: 'GET',
				async: false,
				url: '/BudgetManage/Handler/GetConsResource.ashx?' + new Date().getTime() + '&ConsTaskId=' + id + '&TaskId=' + taskId + '&type=Query',
				success: function (data) {
					tab_Resource = data;
				}
			});
			var isDisplay = $('#' + id + '1').get(0);
			if (isDisplay == undefined) {
				$('#' + id).after('<tr id="' + id + '1"><td align="center" colspan="12" style="border:solid 1px #CADEED;">' + tab_Resource + '</td></tr>');
				if (prevId != undefined && prevId != id) {
					$('#' + prevId + '1').remove();
				}
				prevId = id;
			}
			else {
				$('#' + id + '1').remove();
				isDisplay = undefined;
			}
		}
	</script>
	<style type="text/css">
		.style1
		{
			width: 80px;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 99%; overflow: auto;">
		<table style="border: 0px; width: 100%;" cellspacing="0" cellpadding="0">
			<tr style="width: 100%; height: 20%;">
				<td style="text-align: left; width: 80px;" class="divFooter">
					上报日期:
					
					<asp:TextBox ID="txtDate" disabled="disabled" Width="80px" runat="server"></asp:TextBox>
				</td>
				<td class="divFooter">
					<span class="tooltip" id="spPrjName" runat="server">项目名称:<asp:Label ID="lblPrjName" runat="server"></asp:Label>
					</span>
				</td>
				<td class="divFooter" style="text-align: right; width: 80px;">
					记录人:<asp:TextBox ID="txtInputUser" disabled="disabled" Width="80px" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td colspan="3" style="width: 100%; vertical-align: top; height: 1%;">
					<div id="divBudget" style="border-bottom: solid 0px red;">
						<asp:GridView ID="gvTask" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvTask_RowDataBound" DataKeyNames="TaskId,ConsTaskId" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px" Visible="false">
<HeaderTemplate>
										<asp:CheckBox ID="cbAllBox" runat="server" />
									</HeaderTemplate>

<ItemTemplate>
										<asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("ConsTaskId")) %>' runat="server" />
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
										任务编码
									</HeaderTemplate>

<ItemTemplate>
										<%# Eval("TaskCode") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
										分项工作
									</HeaderTemplate>

<ItemTemplate>
										<%# Eval("TaskName") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
										总工作量
									</HeaderTemplate>

<ItemTemplate>
										<%# Eval("Quantity") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
										单位
									</HeaderTemplate>

<ItemTemplate>
										<%# Eval("Unit") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
										累计审核量
									</HeaderTemplate>

<ItemTemplate>
										<%# Eval("SumAccountingQuantity") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
										剩余工作量
									</HeaderTemplate>

<ItemTemplate>
										<%# Eval("SurplusQuantity") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<HeaderTemplate>
										今日完成量
									</HeaderTemplate>

<ItemTemplate>
										<%# Eval("CompleteQuantity") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
										形象进度
									</HeaderTemplate>

<ItemTemplate>
										<%# Eval("WorkContent") %>
									</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					</div>
				</td>
			</tr>
		</table>
		<div>
			<table cellspacing="0" cellpadding="0" border="1px" style="width: 100%; height: 20%;">
				<tbody style="height: 100%;">
					<tr style="height: 30px;">
						<td class="td-label">
							附件:
						</td>
						<td class="td-content" id="FileUpload1" runat="server">
						</td>
					</tr>
					<tr style="vertical-align: top;">
						<td class="td-label">
							技术质量<br />
							安全工作纪录:
						</td>
						<td class="td-content" style="vertical-align: top;">
							<div style="overflow: auto; height: 76px;">
								<textarea id="txtWorkCard" disabled="true" cols="150" rows="150" style="width: 99%; height: 70px" runat="server"></textarea>
							</div>
						</td>
					</tr>
				</tbody>
			</table>
		</div>
	</div>
	<script type="text/javascript" src="../../StockManage/script/jquery.wysiwyg.js"></script>
	<script type="text/javascript">
		(function ($) {
			$('.wysiwyg').wysiwyg({
				controls: {
					strikeThrough: { visible: true },
					underline: { visible: true },

					separator00: { visible: true },

					justifyLeft: { visible: true },
					justifyCenter: { visible: true },
					justifyRight: { visible: true },
					justifyFull: { visible: true },

					separator01: { visible: true },

					indent: { visible: true },
					outdent: { visible: true },

					separator02: { visible: true },

					subscript: { visible: true },
					superscript: { visible: true },

					separator03: { visible: true },

					undo: { visible: false },
					redo: { visible: false },

					separator04: { visible: false },

					insertOrderedList: { visible: true },
					insertUnorderedList: { visible: true },
					insertHorizontalRule: { visible: true },

					h4mozilla: { visible: false && $.browser.mozilla, className: 'h4', command: 'heading', arguments: ['h4'], tags: ['h4'], tooltip: "Header 4" },
					h5mozilla: { visible: false && $.browser.mozilla, className: 'h5', command: 'heading', arguments: ['h5'], tags: ['h5'], tooltip: "Header 5" },
					h6mozilla: { visible: false && $.browser.mozilla, className: 'h6', command: 'heading', arguments: ['h6'], tags: ['h6'], tooltip: "Header 6" },

					h4: { visible: false && !($.browser.mozilla), className: 'h4', command: 'formatBlock', arguments: ['<H4>'], tags: ['h4'], tooltip: "Header 4" },
					h5: { visible: false && !($.browser.mozilla), className: 'h5', command: 'formatBlock', arguments: ['<H5>'], tags: ['h5'], tooltip: "Header 5" },
					h6: { visible: false && !($.browser.mozilla), className: 'h6', command: 'formatBlock', arguments: ['<H6>'], tags: ['h6'], tooltip: "Header 6" },

					separator05: { visible: false },
					separator06: { visible: false },
					separator07: { visible: false },

					cut: { visible: false },
					copy: { visible: false },
					insertImage: { visible: false },
					paste: { visible: false }
				}
			});
		})(jQuery);
	</script>
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	<asp:HiddenField ID="hfldReportId" runat="server" />
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	<asp:HiddenField ID="hfldAddOrEdit" runat="server" />
	<!-- 保存选择的资源-->
	<asp:HiddenField ID="hfldResourceId" runat="server" />
	<div id="divSelectResource" title="选择资源" style="display: none">
		<iframe id="ifResouece" frameborder="0" width="100%" height="100%"></iframe>
	</div>
	<input type="button" id="btnBindResource" value="btnBindResource" style="display: none" />
	
	<asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
	</form>
</body>
</html>
