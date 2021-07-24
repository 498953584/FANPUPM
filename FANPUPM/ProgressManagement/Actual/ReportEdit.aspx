<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="ReportEdit.aspx.cs" Inherits="ProgressManagement_Actual_ReportEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_usercontrol_selectrestask_ascx" Src="~/StockManage/UserControl/SelectResTask.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>实际进度上报/</title><link href="../../StockManage/Skins/jquery.wysiwyg.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/Budget/ConstructTask.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var gvTask = new JustWinTable('gvwTask');
			$('#btnBindResTask').hide();
			setButton2(gvTask, 'btnDel', 'btnEdit', 'hfldCheckIds');
			replaceEmptyTable('gvwEmpty', 'gvwTask');
			if ($('#gvwTask').get(0).rows.length <= 1) {
				$('#btnUpdate').attr('disabled', 'disabled');
			}
			else {
				$('#btnUpdate').removeAttr('disabled');

			}
		});
		function setButton2(jwTable, btnDel, btnUpdate, hdChkId) {

			if (!jwTable.table) return;
			if (jwTable.table.firstChild.childNodes.length == 1) {
				//table中没有数据
				return;
			}
			jwTable.registClickTrListener(function () {
				if (hdChkId != '')
					document.getElementById(hdChkId).value = this.id;
				var checkedChk = jwTable.getCheckedChk();
				document.getElementById(btnDel).removeAttribute('disabled');
			});
			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				if (checkedChk.length == 0) {
					document.getElementById(btnDel).setAttribute('disabled', 'disabled');
				}
				else if (checkedChk.length == 1) {
					if (hdChkId != '')
						document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
					document.getElementById(btnDel).removeAttribute('disabled');
				}
				else {
					if (hdChkId != '')
						document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
					var checkedChks = jwTable.getCheckedChk();
				}
			});
			jwTable.registClickAllCHKLitener(function () {
				if (this.checked) {
					if (hdChkId != '')
						document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
					var checkedChks = jwTable.getAllChk();
					document.getElementById(btnDel).removeAttribute('disabled');
				}
				else {
					if (hdChkId != '')
						document.getElementById(hdChkId).value = '';
					document.getElementById(btnDel).setAttribute('disabled', 'disabled');
				}

				if (jwTable.table.rows.length == 2 && this.checked == true) {
					if (hdChkId != '')
						document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();

				}
			});
		};
		//选择进度任务
		function addTask() {
			var proVerId = getRequestParam("id");
			document.getElementById('SelectResource1_btnSelectResource').onclick('PMultiTask', proVerId);
		}

		//选择人员
		function selectUser(id, name) {
			jw.selectOneUser({ nameinput: 'txtInputUser', codeinput: 'hfldInputUser' });
		}

		//校验完成百分比
		function checkData(txt) {
			if (txt.value) {
				var regex = /^\d+(\.\d+)?$/;
				if (!regex.test(txt.value)) {
					txt.value = 0;
				}
				else {
					var _value = parseInt(txt.value);
					if (_value < 0 || _value > 100) {
						txt.value = 0;
					}
					else {
						txt.value = parseInt(txt.value);
					}
				}
			}
		}

		// 关闭此标签
		function CloseTab() {
			top.ui.tabSuccess({ parentName: '_ReportEdit' });
		}
		//保存提示
		function save() {
			alert('系统提示：\n\n保存成功');
		}
	</script>
	<style type="text/css">
		.style2
		{
			width: 40px;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 99%; overflow: hidden;">
		<table style="border: 0px; width: 100%; height: 100%;">
			<tr style="width: 100%; height: 5%;">
				<td style="text-align: left; width:25%; ">
					<span>上报日期</span>
					<asp:TextBox ID="txtDate" Width="80px" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
				<td style="text-align: right; width:25%;  padding:0px; margin:0px;"  >
					<span >记录人</span>
				</td>
				<td style="text-align: left; width:25%; ">
					<span class="spanSelect" style="width: 122px;">
						<asp:TextBox ReadOnly="true" ID="txtInputUser" Style="width: 97px;
							border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
						<img src="../../images/icon.bmp" style="float: right;" alt="选择" id="imgName" onclick="selectUser('hfldInputUser','txtInputUser');" runat="server" />

					</span>
					<input id="hfldInputUser" type="hidden" style="width: 1px" runat="server" />

				</td>
				<td style="text-align: right; width:25%; ">
					<input id="Button2" type="button" value="选择任务" style="width: 80px;" onclick="addTask();" runat="server" />

					<asp:Button Width="80px" ID="btnDel" Text="删除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDelete_Click" runat="server" />
					<asp:Button ID="btnUpdate" Width="80px" Text="保存" OnClick="btnSave_Click" runat="server" />
					<MyUserControl:stockmanage_usercontrol_selectrestask_ascx ID="SelectResource1" Text="选择进度任务" Width="75.0" ButtonId="btnBindResTask" TypeCode="0" runat="server" />
				</td>
			</tr>
			<tr>
				<td colspan="4" style="width: 100%; vertical-align: top; height: 61%;">
					<div id="divBudget" class="pagediv" style="border-bottom: solid 0px red;">
						<asp:GridView ID="gvwTask" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwTask_RowDataBound" DataKeyNames="Id,TaskUID" runat="server">
<EmptyDataTemplate>
								<table id="gvwEmpty">
									<tr class="header">
										<th rowspan="2" scope="col" style="width: 25px;">
											序号
										</th>
										<th rowspan="2" scope="col">
											任务名称
										</th>
										<th rowspan="2" scope="col">
											工期
										</th>
										<th scope="col">
											计划开始日期
										</th>
										<th scope="col">
											计划结束日期
										</th>
										<th scope="col">
											实际开始日期
										</th>
										<th scope="col">
											实际结束日期
										</th>
										<th rowspan="2" scope="col" colspan="2">
											完成百分比
										</th>
										<th rowspan="2" scope="col">
											说明
										</th>
									</tr>
								</table>
							</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
										<asp:CheckBox ID="cbAllBox" runat="server" />
									</HeaderTemplate>

<ItemTemplate>
										<asp:CheckBox ID="cbBox" runat="server" />
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="任务名称"><ItemTemplate>
										<%# Eval("TaskName") %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工期">
<ItemTemplate>
										<%# Eval("DURATION_") %>
									</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="计划开始日期" HeaderStyle-Width="80px" DataField="START_" DataFormatString="{0:yyy-M-d}" /><asp:BoundField HeaderText="计划结束日期" HeaderStyle-Width="80px" DataField="FINISH_" DataFormatString="{0:yyy-M-d}" /><asp:TemplateField HeaderText="实际开始日期" HeaderStyle-Width="80px"><ItemTemplate>
										<asp:TextBox ID="txtStartDate" Width="80px" onclick="WdatePicker()" Text='<%# System.Convert.ToString(base.GetTime(Eval("Start")), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="实际结束日期" HeaderStyle-Width="80px"><ItemTemplate>
										<asp:TextBox ID="txtFinshDate" Width="80px" onclick="WdatePicker()" Text='<%# System.Convert.ToString(base.GetTime(Eval("Finish")), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="完成百分比">
<ItemTemplate>
										<asp:TextBox ID="txtCompleteQuantity" Style="text-align: right;" onblur="checkData(this)" Text='<%# System.Convert.ToString(Eval("Completed"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
										说明
									</HeaderTemplate>

<ItemTemplate>
										<asp:TextBox ID="txtNoet" Text='<%# System.Convert.ToString(Eval("Note"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
									</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					</div>
				</td>
			</tr>
			<tr style="height: 25%;">
				<td colspan="4" align="left">
					<table cellspacing="0" cellpadding="0" border="1px"  style="width: 100%; border:1px solid rgb(204,204,204); ">
						<tr>
							<td class="td-label">
								附件:
							</td>
							<td class="td-content">
								<MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" FileType="*.*" Class="ProgressReport" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="td-label">
								备注：
							</td>
							<td class="td-content">
								<textarea id="txtWorkCard" class="wysiwyg" cols="80" style="width: 100%; height:150px;" runat="server"></textarea>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<asp:HiddenField ID="hfldIsPostBack" runat="server" />
		<script type="text/javascript" src="../../StockManage/script/jquery.wysiwyg.js"></script>
		<script type="text/javascript">
			if ($('#hfldIsPostBack').val() != 'true') {
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
			}
		</script>
	</div>
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	<asp:HiddenField ID="hfldReportId" runat="server" />
	<asp:Button ID="btnBindResTask" OnClick="btnBindResTask_Click" runat="server" />
	<asp:HiddenField ID="hfldCheckIds" runat="server" />
	<asp:HiddenField ID="hfldAddOrEdit" runat="server" />
	<!-- 保存选择的资源-->
	<asp:HiddenField ID="hfldTaskId" runat="server" />
	<div id="divSelectResource" title="选择资源" style="display: none">
		<iframe id="ifResouece" frameborder="0" width="100%" height="100%"></iframe>
	</div>
	<div id="divFramPerson" title="选择人员" style="display: none">
		<iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<asp:HiddenField ID="hdReturnVal" runat="server" />
	<input type="button" id="btnBindResource" value="btnBindResource" style="display: none" />
	</form>
</body>
</html>
