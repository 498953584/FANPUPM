<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="CompletedManage.aspx.cs" Inherits="PrjManager_Completed_CompletedManage" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>竣工管理</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />
<link href="../../StockManage/skins/jquery.wysiwyg.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/ecmascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<style type="text/css">
		#queryTable tr td
		{
			white-space: nowrap;
		}
	</style>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			$('#btnSaveData').hide();
			replaceEmptyTable('emptyProject', 'gvwPrjInfo');
			var jwTable = new JustWinTable('gvwPrjInfo');
			showTooltip('tooltip', 25);

			jwTable.registClickTrListener(function () {
				document.getElementById('hfldCheckedIds').value = this.id;
				document.getElementById('btnQuery').removeAttribute('disabled');
				var isApproval = document.getElementById('isCompletedApprove').value;
				if ($(this).attr('isPrimit') == "1") {
					if (isApproval == "0") {
						var prjState = $(this).attr('prjState')
						if (prjState != "10") {
							document.getElementById('btnCompleted').removeAttribute('disabled');
							// 保内,保外,解除
							if (prjState == '11' || prjState == '12' || prjState == '13') {
								document.getElementById('btnStateChange').removeAttribute('disabled');
							} else {
								document.getElementById('btnStateChange').setAttribute('disabled', 'disabled');
							}
						}
						else {
							document.getElementById('btnCompleted').setAttribute('disabled', 'disabled');
							document.getElementById('btnStateChange').removeAttribute('disabled');
						}
					}
					if (isApproval == "1") {
						if ($(this).attr('wfState') == "1") {
							var prjState = $(this).attr('prjState')
							if (prjState != "10") {
								document.getElementById('btnCompleted').removeAttribute('disabled');
								// 保内,保外,解除
								if (prjState == '11' || prjState == '12' || prjState == '13') {
									document.getElementById('btnStateChange').removeAttribute('disabled');
								} else {
									document.getElementById('btnStateChange').setAttribute('disabled', 'disabled');
								}
							}
							else {
								document.getElementById('btnCompleted').setAttribute('disabled', 'disabled');
								document.getElementById('btnStateChange').removeAttribute('disabled');
							}
						}
						else {
							document.getElementById('btnCompleted').setAttribute('disabled', 'disabled');
						}
					}
				}
				if ($(this).attr('isPrimit') == "0") {
					document.getElementById('btnCompleted').setAttribute('disabled', 'disabled');
					document.getElementById('btnStateChange').setAttribute('disabled', 'disabled');
				}
				if ($(this).attr('prjState') != "9") {
					document.getElementById('btnCompleted').setAttribute('disabled', 'disabled');
				}
			});
			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				var btnQuery = document.getElementById('btnQuery');
				var btnCompleted = document.getElementById('btnCompleted');
				if (checkedChk.length == "1") {
					var tr1 = getFirstParentElement(checkedChk[0].parentNode, 'TR');
					document.getElementById('hfldCheckedIds').value = tr1.id;
					btnQuery.removeAttribute('disabled');

					var isApproval = document.getElementById('isCompletedApprove').value;
					if ($(tr1).attr('isPrimit') == "1") {
						if (isApproval == "0") {
							var prjState = $(tr1).attr('prjState')
							if (prjState != "10") {
								document.getElementById('btnCompleted').removeAttribute('disabled');
								// 保内,保外,解除
								if (prjState == '11' || prjState == '12' || prjState == '13') {
									document.getElementById('btnStateChange').removeAttribute('disabled');
								} else {
									document.getElementById('btnStateChange').setAttribute('disabled', 'disabled');
								}
							}
							else {
								document.getElementById('btnCompleted').setAttribute('disabled', 'disabled');
								document.getElementById('btnStateChange').removeAttribute('disabled');
							}
						}
						if (isApproval == "1") {
							if ($(tr1).attr('wfState') == "1") {
								var prjState = $(tr1).attr('prjState')
								if (prjState != "10") {
									document.getElementById('btnCompleted').removeAttribute('disabled');
									//document.getElementById('btnStateChange').setAttribute('disabled', 'disabled');
									if (prjState == '11' || prjState == '12' || prjState == '13') {
										document.getElementById('btnStateChange').removeAttribute('disabled');
									} else {
										document.getElementById('btnStateChange').setAttribute('disabled', 'disabled');
									}
								}
								else {
									document.getElementById('btnCompleted').setAttribute('disabled', 'disabled');
									document.getElementById('btnStateChange').removeAttribute('disabled');
								}
							}
							else {
								document.getElementById('btnCompleted').setAttribute('disabled', 'disabled');
							}
						}
					}
					if ($(tr1).attr('isPrimit') == "0") {
						document.getElementById('btnCompleted').setAttribute('disabled', 'disabled');
						document.getElementById('btnStateChange').setAttribute('disabled', 'disabled');
					}
					if ($(tr1).attr('prjState') != "9") {
						document.getElementById('btnCompleted').setAttribute('disabled', 'disabled');
					}
				}
				else {
					btnQuery.setAttribute('disabled', 'disabled');
					btnCompleted.setAttribute('disabled', 'disabled');
					document.getElementById('btnStateChange').setAttribute('disabled', 'disabled');
				}
			});

			jwTable.registClickAllCHKLitener(function () {
				document.getElementById('btnQuery').setAttribute('disabled', 'disabled');
				document.getElementById('btnCompleted').setAttribute('disabled', 'disabled');
				document.getElementById('btnStateChange').setAttribute('disabled', 'disabled');
			});

			registerCompletedEvent();
			//			formateTable('gvwPrjInfo', 4);
		});

		function registerCompletedEvent() {
			var btnCompleted = document.getElementById('btnCompleted');
			var hfldPrjId = document.getElementById('hfldCheckedIds');
			addEvent(btnCompleted, 'click', function () {
				$('#divCompleted').show().window('open');
			});
		}

		//保存数据
		function saveData() {
			if ($('#txtCompletedDate').val() == '') {
				top.ui.alert('竣工日期必须输入');
				$('#txtCompletedDate').focus();
				return;
			}
			$('#btnSaveData').click();
		}
		function Query(prjId) {
			var isApproval = document.getElementById('isCompletedApprove').value;
			var prjGuid = "";
			if (prjId == undefined) {
				prjGuid = document.getElementById('hfldCheckedIds').value;
			}
			else {
				prjGuid = prjId;
			}
			var url = "";
			if (isApproval == "1") {
				url = "/PrjManager/Completed/PrjCompletedQuery.aspx?ic=" + prjGuid;

			}
			if (isApproval == "0") {
				url = "/PrjManager/Completed/PrjCompletedView.aspx?ic=" + prjGuid;
			}
			viewopen(url, '查看项目竣工验收单');
		}

		//选择人员
		function selectUser(id, name) {
			jw.selectOneUser({ codeinput: id, nameinput: name });
		}
		//甲方名称
		function pickCorp(param) {
			jw.selectOneCorp({ idinput: 'hfldOwner', nameinput: 'txtOwner' });
		}

		// 变更状态
		function changeState() {
			$.ajax({
				type: "GET",
				url: "/PrjManager/Handler/ModifyPrjState.ashx?PrjGuid=" + $('#hfldCheckedIds').val() +
                                "&PrjState=" + $('#dropModifyPrjState').val() + "&Time=" + new Date().getTime(),
				success: function () {
					window.location.href = window.location.href;
				}

			});
		}

		//状态变更
		function modifyState() {
			$('#div_modify_state').show().window('open');
			return false;
			$('#div_modify_state').dialog({
				modal: true,
				buttons: {
					"确定": function () {
						$.ajax({
							type: "GET",
							url: "/PrjManager/Handler/ModifyPrjState.ashx?PrjGuid=" + $('#hfldCheckedIds').val() +
                                "&PrjState=" + $('#dropModifyPrjState').val() + "&Time=" + new Date().getTime(),
							success: function () {
								window.location.href = window.location.href;
							}

						});
						$(this).dialog("close");

					},
					"取消": function () {
						$(this).dialog("close");
					}
				}
			});
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table width="100%" cellpadding="0" cellspacing="0">
		<tr>
			<td>
				<table class="queryTable" id="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							项目编号
						</td>
						<td>
							<asp:TextBox ID="txtPrjCode" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							项目名称
						</td>
						<td>
							<asp:TextBox ID="txtPrjName" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							项目经理
						</td>
						<td>
							<span class="spanSelect" style="width: 122px;">
								<asp:TextBox ID="txtPrjManager" Style="width: 97px; height: 15px;
									border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
								<img src="../../images/icon.bmp" style="float: right; height: 18px;" alt="选择" id="img2"
									onclick="selectUser('hfldPrjManager','txtPrjManager');" />
							</span>
							
							<input id="hfldPrjManager" type="hidden" style="width: 1px" runat="server" />

						</td>
						<td class="descTd">
							项目状态
						</td>
						<td>
							<asp:DropDownList ID="dropPrjState" Width="120px" runat="server"></asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							立项申请日期
						</td>
						<td>
							<asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							至
						</td>
						<td>
							<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							竣工日期
						</td>
						<td>
							<asp:TextBox ID="txtCompletedTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td colspan="2">
							<asp:Button ID="brnQuery" Text="查询" OnClick="brnQuery_Click" runat="server" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="divFooter" style="text-align: left; padding-left: 2px;">
				<input type="button" id="btnQuery" value="查看" disabled="disabled" onclick="Query()" />
				<input type="button" id="btnCompleted" value="设置竣工日期" disabled="disabled" style="width: 100px" />
				<input type="button" id="btnStateChange" disabled="disabled" value="状态变更" onclick="modifyState();"
					style="width: 80px;" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:GridView ID="gvwPrjInfo" CssClass="gvdata" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="gvwPrjInfo_RowDataBound" DataKeyNames="PrjGuid,TypeCode,Primit,CompletedFlowState,PrjState" runat="server">
<EmptyDataTemplate>
						<table class="gvdata" cellspacing="0" id="emptyProject" rules="all" border="1" style="width: 100%;
							border-collapse: collapse;">
							<tr>
								<th class="header">
									序号
								</th>
								<th class="header">
									项目状态
								</th>
								<th class="header">
									项目编号
								</th>
								<th class="header">
									项目名称
								</th>
								<th class="header">
									项目经理
								</th>
								<th class="header">
									竣工日期
								</th>
								<th class="header">
									竣工说明
								</th>
								<th class="header">
									立项申请日期
								</th>
							</tr>
						</table>
					</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
								<asp:CheckBox ID="cbAllBox" runat="server" />
							</HeaderTemplate><ItemTemplate>
								<asp:CheckBox ID="cbBox" runat="server" />
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
								<%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
							</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="项目状态" DataField="PrjStateName" /><asp:BoundField DataField="PrjCode" HeaderText="项目编号" /><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="150px"><ItemTemplate>
								<asp:HyperLink ID="hlinkShowName" CssClass="tooltip" Style="text-decoration: none;
									color: Black;" ToolTip='<%# System.Convert.ToString(Eval("PrjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
                        <span class="link" onclick="Query('<%# Eval("PrjGuid").ToString() %>' )">
                        <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                       </span></asp:HyperLink>
							</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="项目经理" DataField="PrjMangerName" /><asp:TemplateField HeaderText="竣工日期"><ItemTemplate>
								<%# Common2.GetTime(Eval("CompletedDate")) %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
								<%# Common2.GetState(Eval("CompletedFlowState").ToString()) %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="竣工说明"><ItemTemplate>
								<%# StringUtility.StripTagsCharArray(Eval("CompletedNote").ToString()) %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="立项申请日期"><ItemTemplate>
								<%# Common2.GetTime(Eval("InputDate")) %>
							</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
				</webdiyer:AspNetPager>
			</td>
		</tr>
	</table>
	<div id="divCompleted" class="easyui-window" title="设置竣工日期" style="width: 600px;
		height: 320px;" data-options="modal:true,closed:true,iconCls:'icon-save'" runat="server">
		<table id="table1" class="tableContent2" cellpadding="5px" cellspacing="0">
			<tr>
				<td class="word">
					竣工日期
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtCompletedDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					竣工说明
				</td>
				<td class="txt" style="padding-right: 3px">
					<textarea id="txtCompletedNote" class="wysiwyg" cols="55" rows="5" runat="server"></textarea>
				</td>
			</tr>
			<tr>
				<td colspan="2" style="text-align: right;">
					<input type="button" id="btnCompleteDate" value="保存" onclick="saveData();" />
				</td>
			</tr>
		</table>
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
	
	<div id="div_modify_state" title="项目状态变更" class="easyui-window" style="text-align: center;
		width: 300px; height: 120px;" data-options="modal:true,closed:true,iconCls:'icon-save'">
		项目状态：
		<asp:DropDownList ID="dropModifyPrjState" Width="100px" runat="server"></asp:DropDownList>
		<input type="button" id="btnPrjState" value="保存" onclick="changeState();" />
	</div>
	<asp:Button ID="btnSaveData" Text="保存" OnClick="btnSaveData_Click" runat="server" />
	<asp:HiddenField ID="hdReturnVal" runat="server" />
	<asp:HiddenField ID="hfldCheckedIds" runat="server" />
	<asp:HiddenField ID="isCompletedApprove" runat="server" />
	</form>
</body>
</html>
