<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyFlow.aspx.cs" Inherits="UserDefineFlow_MyFlow" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>电子办公</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/ecmascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var type = getRequestParam('type');
			$('input[id$="BtnWFDel"]').click(superDelete);
			var jwTable = new JustWinTable('GVBook');
			replaceEmptyTable('emptyTable', 'GVBook');
			showTooltip('tooltip', 25);
			$('#divinfo').hide();
			//提交审核             
			$('#btnStartWF').click(function () {
				var src = "/EPC/WorkFlow/StartWF.aspx?" + "fid=" + $("#HdnRecordID").val() +
					"&bcode=999&bclass=" + $("#HdnBusinessClass").val() +
					"&prjguid=''&templateid=" + $("#HdnTemplateID").val();
				top.ui._StartWf = window;
				top.ui.openWin({ title: '提交审核', url: src, width: 520, height: 460 });
			});

			//撤回审核
			$('#CancelBt').click(function () {
				$('#divinfo').show().window('open');
			});
		})


		// 撤回时保存按钮方法
		function recall() {
			$.ajax({
				url: '/EPC/UserControl1/CancelAudit.ashx?' + new Date().getTime(),
				data: { fid: $('#HdnRecordID').val(), bcode: "999", bclass: $('#HdnBusinessClass').val() },
				success: function (data) {
					if (data == "1") {
						$('divinfo').window("close");
						window.location.href = window.location.href;
						top.ui.show('撤回成功');
					}
					else {
						$('divinfo').window("close");
						top.ui.show('对方已审核，撤回失败');
					}
				}
			});
		}

		function ClickRow(RecordID, AuditState, BusinessClass) {
			document.getElementById("HdnRecordID").value = RecordID;
			document.getElementById("HdnBusinessClass").value = BusinessClass;
			document.getElementById("HdnBusinessCode").value = "999";
			document.getElementById('BtnView').disabled = false;
			if (AuditState == "-1")  //未提交状态
			{
				if (document.getElementById('btnStartWF') != null)
					document.getElementById('btnStartWF').disabled = false;     //可提交 
				if (document.getElementById('btnStartWFRecord') != null)
					document.getElementById('btnStartWFRecord').disabled = true;   //不可查看流程状态
				if (document.getElementById('btnWFPrint') != null)
					document.getElementById('btnWFPrint').disabled = true;  //不可查看审核记录
			}
			if (AuditState == "-3")   //重报状态
			{
				if (document.getElementById('btnStartWF') != null)
					document.getElementById('btnStartWF').disabled = false;      //可提交
				if (document.getElementById('btnStartWFRecord') != null)
					document.getElementById('btnStartWFRecord').disabled = false;   //可查看流程状态
				if (document.getElementById('btnWFPrint') != null)
					document.getElementById('btnWFPrint').disabled = false;  //可查看审核记录   
			}
			if (AuditState == "1" || AuditState == "0" || AuditState == "-2")   //以通过或者审核中驳回时
			{
				if (document.getElementById('btnStartWF') != null)
					document.getElementById('btnStartWF').disabled = true;
				if (document.getElementById('btnStartWFRecord') != null)
					document.getElementById('btnStartWFRecord').disabled = false;
				if (document.getElementById('btnWFPrint') != null)
					document.getElementById('btnWFPrint').disabled = false;
			}
			if (AuditState == "0")//状态为0时才能撤销
			{
				document.getElementById("CancelBt").disabled = false;
			}
			else {
				document.getElementById("CancelBt").disabled = true;
			}
			if (AuditState == "1" || AuditState == "0" || AuditState == "-3" || AuditState == "-2")   //彻底删除的可用性
			{
				if (document.getElementById('BtnWFDel') != null)
					document.getElementById('BtnWFDel').disabled = false;
				if (AuditState == "-3") {
					if (document.getElementById('btnEdit') != null)
						document.getElementById('btnEdit').disabled = false;
				}
				else {
					if (document.getElementById('btnEdit') != null)
						document.getElementById('btnEdit').disabled = true;
				}
				if (document.getElementById('btnDel') != null)
					document.getElementById('btnDel').disabled = true;

			}
			else {
				if (document.getElementById('BtnWFDel') != null)
					document.getElementById('BtnWFDel').disabled = true;
				if (document.getElementById('btnEdit') != null)
					document.getElementById('btnEdit').disabled = false;
				if (document.getElementById('btnDel') != null)
					document.getElementById('btnDel').disabled = false;
			}
			upAdminPrivilege();     // 提升管理员权限
		}


		function OpenWin(op) {
			var RecordID = "";
			if (op != 'add') {
				RecordID = document.getElementById('HdnRecordID').value;
			}
			var nodeValue = document.getElementById('hdfNodeValue').value;
			var HdnHdnTemplateID = document.getElementById('HdnTemplateID').value;
			var url = "/oa/UserDefineFlow/MyFlowEdit.aspx?t=" + op + "&tid=" + HdnHdnTemplateID + "&rid=" + RecordID + "&value=" + nodeValue;
			top.ui._userdefineflow = window;
			toolbox_oncommand(url, "电子办公信息维护");
		}

		// 流程状态
		function openAudit(BusinessCode) {
			var BusinessClass = document.getElementById('HdnBusinessClass').value;
			var url = "";
			var instanceCode = document.getElementById('HdnRecordID').value
			url = "../../EPC/Workflow/AuditViewWF.aspx?ic=" + instanceCode + '&bc=' + BusinessCode + '&bcl=' + BusinessClass;
			window.open(url, '', "left=150,top=150,width=600px,height=460px,toolbar=no,status=yes,scrollbars=yes,resiz able=no");
		}

		//查看审核记录
		function OpenPrintWF(BusinessCode) {
			var BusinessClass = document.getElementById('HdnBusinessClass').value;
			var rid = document.getElementById('HdnRecordID').value;
			var url = "/EPC/Workflow/AuditViewPrint.aspx?ic=" + rid + '&bc=' + BusinessCode + '&bcl=' + BusinessClass;
			window.open(url, '', "left=150,top=150,width=600px,height=460px,toolbar=no,status=yes,scrollbars=yes,resiz able=no");
		}
		//查看
		function OpenLock() {
			var rid = document.getElementById('HdnRecordID').value;
			var url = "UserDefineFlowAudit.aspx?ic=" + rid;
			return viewopen(url);

		}
		// 超级验证
		function adminDel() {
			url = "/EPC/UserControl1/TwoPassSet.aspx?tt=1"; //../../
			return window.showModalDialog(url, window, "dialogHeight:135px;dialogWidth:260px;center:1;status:no;scroll:no;help:no;");
		}

		// 超级删除
		function superDelete(btn) {
			var thePwd = $('input[id$="hfldPwd"]').val();
			top.ui.prompt2('系统提示', '请输入验证码: ', function (r) {
				// 当点击取消时, r = undefined
				if (r == undefined) return;

				if (r == thePwd) {
					theDelete();
				} else {
					top.ui.alert('验证码错误.');
					return false;
				}
			});
		}

		// 真正的超级删除代码
		function theDelete() {
			var data = {
				guid: $('input[id$="HdnRecordID"]').val(),
				busiCode: $('input[id$="HdnBusinessCode"]').val(),
				busiClass: $('input[id$="HdnBusinessClass"]').val()
			};

			$.ajax({
				type: 'POST',
				url: '/EPC/Workflow/Handler/TheDelete.ashx',
				data: data,
				cache: false,
				success: function (d) {
					if (d == '1') {
						// 超级删除成功
						top.ui.show('删除成功');
						top.ui.reloadTab();
					} else {
						// 超级删除失败
						top.ui.alert('超级删除失败');
					}
				}
			});
		}
	</script>
</head>
<body>
	<form id="formx" style="width: 100%; height: 100%; border: solid 0px blue;
	margin: 0px; padding: 0px;" runat="server">
	<table cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
		<tr>
			<td valign="top" width="20%" height="100%">
				<div style="margin: 0px; padding: 0px; overflow: scroll; border: solid 0px red; height: 100%;">
					<asp:TreeView ID="TVDept" ShowLines="true" OnSelectedNodeChanged="TVDept_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
				</div>
			</td>
			<td style="vertical-align: top">
				<table style="width: 100%; vertical-align: top">
					<tr>
						<td class="divHeader">
							电子办公
						</td>
					</tr>
					<tr>
						<td height="22px" width="100%" style="vertical-align: top; white-space: nowrap;">
							<input type="button" value="新增" id="btnAdd" disabled="true" runat="server" />

							<input type="button" value="编辑" id="btnEdit" disabled="true" runat="server" />

							<asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
							<asp:Button ID="BtnView" Enabled="false" Text="查 看" OnClientClick="OpenLock()" runat="server" />
							<input type="button" value="提交审核" style="width: 80px" id="btnStartWF" disabled="disabled" />
							<input type="button" value="撤回" style="width: 80px" id="CancelBt" disabled="disabled" />
							<input id="btnStartWFRecord" type="button" value="流程状态" style="width: 90px" disabled="true" runat="server" />

							<asp:Button ID="btnWFPrint" Text="审核记录" Enabled="false" Width="100px" runat="server" />
							<input type="button" id="BtnWFDel" value="超级删除" style="width: 75px; display: none;"
								disabled="disabled" />
							<input id="HdnRecordID" style="width: 20px" type="hidden" runat="server" />

							<input id="HdnTemplateID" value="" style="width: 20px" type="hidden" runat="server" />

							<input id="HdnBusinessClass" value="" style="width: 20px" type="hidden" runat="server" />

							<input id="HdnBusinessCode" value="" style="width: 20px" type="hidden" runat="server" />

						</td>
					</tr>
					<tr>
						<td valign="top" width="100%" id="td-gvbook">
							<div id="dvDeptBox" style="overflow: auto; width: 100%;">
								<asp:GridView CssClass="gvdata" ID="GVBook" AllowPaging="true" AutoGenerateColumns="false" Width="100%" PageSize="25" OnRowDataBound="GVBook_RowDataBound" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" runat="server">
<EmptyDataTemplate>
										<table id="emptyTable" style="width: 100%">
											<tr align="center" class="header">
												<th align="center" nowrap="nowrap" scope="col" style="width: 20px">
													序号
												</th>
												<th align="center" nowrap="nowrap" scope="col">
													申请标题
												</th>
												<th align="center" nowrap="nowrap" scope="col">
													审核事项
												</th>
												<th align="center" nowrap="nowrap" scope="col">
													流程状态
												</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><Columns><asp:BoundField HeaderText="序号" HtmlEncode="false" /><asp:TemplateField HeaderText="申请标题">
<ItemTemplate>
												<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" runat="server">
                                                    <span class="link tooltip" title='<%# Eval("Title").ToString() %>'  onclick="viewopen('UserDefineFlowAudit.aspx?ic=<%# Eval("RecordID") %>')">
                                                    <%# StringUtility.GetStr(Eval("Title").ToString(), 25) %>
                                                   </span>
												</asp:HyperLink>
											</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="Remark" HeaderText="审核事项" HtmlEncode="false" /><asp:TemplateField HeaderText="流程状态">
<ItemTemplate>
												<%# Common2.GetState(Eval("AuditState").ToString()) %>
											</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="申请人" DataField="v_xm" /></Columns><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hdflisName" runat="server" />
	<asp:HiddenField ID="hdflisDuty" runat="server" />
	<asp:HiddenField ID="hdfNodeValue" runat="server" />
	<!-- 超级删除密码 -->
	<asp:HiddenField ID="hfldPwd" runat="server" />
	</form>
	<div id="divinfo" title="撤回审核" class="easyui-window" data-options="modal:true,closed:true,iconCls:'icon-save'"
		style="width: 350px; height: 220px; padding: 10px; display: none;">
		<img alt="" src="../../images/help.jpg" />&nbsp;<span style="font-size: 15px; font-weight: bold">确定撤回此审核吗？</span><br />
		<br />
		<div style="line-height: 20px">
			详细说明：<br />
			1.如果对方已经审核，将不予撤回<br />
			2.只有审核中的单据才能撤回<br />
			3.撤回后的单据为未提交状态<br />
		</div>
		<div style="text-align: right;">
			<input type="button" id="btnWfRecallSave" value="保存" onclick="recall();" />
			<input type="button" id="btnWfRecallCancel" value="取消" onclick="$('#divinfo').window('close')" />
		</div>
	</div>
	<div id="divStartWF" title="提交审核" style="display: none; overflow: hidden">
		<iframe id="iframeWf" width="100%" height="100%" frameborder="0" src="" style="overflow: hidden">
		</iframe>
	</div>
</body>
</html>
