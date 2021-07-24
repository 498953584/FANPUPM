<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="AuditMain.aspx.cs" Inherits="EPC_Workflow_AuditInfo" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>流程审核</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/jquery.easyui.extension.js" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />
<link href="../../StockManage/Skins/jquery.wysiwyg.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../Script/json2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			showTooltip('tooltip', 0);

			$('#btnSubmit')[0].onclick = function () { jw.preventSubmit2('btnSubmit'); }
		});
		window.name = "win";

		// 多选
		function pickMulPerson() {
			jw.selectMultiUser({ nameinput: 'txtAnnouncer', codeinput: 'hdnAnnouncer' });
		}

		// 单选人员
		function pickPerson(op) {
			url = "/EPC/WorkFlow/SelectUser.aspx?op=" + op;
			top.ui.callback = function (user) {
				setUserInfo({ op: op, code: user.code, name: user.name })
			}
			top.ui.openWin({ url: url, title: '选择人员' });

			$('#btnSubmit').removeAttr('disabled');
		}

		function ckeck(op) {
			var nodeId = "";
			nodeId = document.getElementById("hdnNodeID").value;
			if (nodeId != "") //当前节点非前插或后插节点
			{
				if (op == 0) {
					document.getElementById('btnAfter').style.display = 'none';
					document.getElementById('trFront').style.display = '';
					document.getElementById("btnRet").style.display = "";
					document.getElementById('trResult').style.display = '';
					document.getElementById('trAuditInfo').style.display = 'none';
					document.getElementById('trAnnouncer').style.display = 'none';
					document.getElementById('trContent').style.display = 'none';
					document.getElementById('trAfter').style.display = 'none';
					document.getElementById('trSend').style.display = '';
					document.getElementById('hdnType').value = '前插';
					document.getElementById('Lbresult').style.display = 'none';  //前插审核人时不需要显示
					document.getElementById('tr_selector').style.display = 'none';
					document.getElementById('RBLRoleType').style.display = 'none';  //前插审核人时不需要
					document.getElementById('trUpFiles').style.display = 'none';
					document.getElementById('trAuditMain').style.display = 'none';   //前插或者后插审核时不需要显示当前节点审核要点，节点审核要点
					document.getElementById('trAuditRemark').style.display = '';
				}
				else {
					document.getElementById('btnFront').style.display = 'none';
					document.getElementById('trFront').style.display = 'none';
					document.getElementById("btnRet").style.display = "";
					document.getElementById('trResult').style.display = '';
					document.getElementById('trAuditInfo').style.display = 'none';
					document.getElementById('trAnnouncer').style.display = 'none';
					document.getElementById('trContent').style.display = 'none';
					document.getElementById('trAfter').style.display = '';
					document.getElementById('trSend').style.display = '';
					document.getElementById('hdnType').value = '后插';
					document.getElementById('Lbresult').style.display = '';
					document.getElementById('tr_selector').style.display = 'none';
					document.getElementById('RBLRoleType').style.display = '';
					$('input[type$=radio]').each(function () {
						$(this).attr('disabled', 'disabled');
					});
					var rad1 = $('input[type$=radio]').get(0);
					$(rad1).attr('CHECKED', 'checked');
					document.getElementById('trUpFiles').style.display = 'none';
					document.getElementById('trAuditMain').style.display = 'none';
					document.getElementById('trAuditRemark').style.display = '';
				}
			}
		}
		function openAudit(op, BusinessCode, BusinessClass) {
			var url = "";
			var instanceCode = document.getElementById('hdnInstanceCode').value
			if (op == 0) {
				url = "AuditViewWF.aspx?ic=" + instanceCode + '&bc=' + BusinessCode + '&bcl=' + BusinessClass;
			}
			else {
				url = "AuditViewPrint.aspx?ic=" + instanceCode + '&bc=' + BusinessCode + '&bcl=' + BusinessClass;

			}
			window.open(url, '', "left=150,top=5,width=800,height=560,toolbar=no,status=yes,scrollbars=yes,resiz able=no");
			//            window.showModalDialog(url, window, "dialogHeight:400px;dialogWidth:800px;center:1;help:0;status:0;");
		}

		//还原
		function ReturnPage() {
			document.getElementById("btnFront").style.display = "";
			document.getElementById("btnAfter").style.display = "";
			document.getElementById('trFront').style.display = 'none';
			document.getElementById("btnRet").style.display = "none";
			document.getElementById('trResult').style.display = '';
			document.getElementById('trAuditInfo').style.display = '';
			document.getElementById('trAnnouncer').style.display = '';
			document.getElementById('trContent').style.display = '';
			document.getElementById('trAfter').style.display = 'none';
			document.getElementById('trSend').style.display = '';
			document.getElementById('hdnType').value = '';
			document.getElementById('RBLRoleType').disabled = false;
			document.getElementById('btnSubmit').disabled = false;
			document.getElementById('trUpFiles').style.display = '';
			document.getElementById('trAuditRemark').style.display = 'none';
			document.getElementById('trAuditMain').style.display = 'none';
			document.getElementById('trResult').style.display = '';
			document.getElementById('RBLRoleType').style.display = '';
			//safari,chrome,firefox 适用
			$('input[type$=radio]').each(function () {
				$(this).removeAttr('disabled');
			});

			var AuditorType = document.getElementById('NodeType').value;
			if (AuditorType != "")
				document.getElementById('tr_selector').style.display = '';
			else
				document.getElementById('tr_selector').style.display = 'none';
		}

		// 保证审核结果成功后执行
		function auditSuccess() {
			if (jw.ipad()) {
				alert('保存审核结果成功');
				window.parent.close();
				return;
			}

			top.ui.refreshDesktop(); 	// 刷新我的桌面
			top.ui.show('保存审核结果成功');
			top.ui.closeTab();
		}

		// 选择人员
		function SelectPerson() {
			var url = "";
			var AuditorType = $('#NodeType').val();
			var nextDepCode = $('#hfldNextAuditDepCode').val(); 		// 下一个审核人的部门

			// 单人
			if (AuditorType == "1") {
				// 如果部门编码非空
				if (nextDepCode.length > 0)
					url = "/Common/SelectOneUser.aspx?depCode=" + nextDepCode;
				else
					url = "/EPC/WorkFLow/SelectUser.aspx";
			}

			// 多人
			if (AuditorType == "2") {
				if (nextDepCode.length > 0)
					url = "/Common/SelectOneUser.aspx?type=multi&depCode=" + nextDepCode;
				else
					url = "/EPC/WorkFLow/SelectAllUser.aspx?UserCode=" + document.getElementById('hdnNextPerson').value + "&UserName=" + encodeURI(document.getElementById('txtnextperson').value);
			}

			if (AuditorType == "3" || AuditorType == "4" || AuditorType == "5") {
				url = "/EPC/WorkFLow/PickRole.aspx?tp=" + AuditorType;
			}
			top.ui._AuditMain = window;
			top.ui.callback = function (user) {
				$('#hdnNextPerson').val(user.code);
				$('#txtnextperson').val(user.name);
			}
			top.ui.openWin({ url: url, title: '选择人员' });
		}

		//选择人员后给人员控件赋值
		function setUserInfo(obj) {
			if (obj.op == 1) {
				$('#hdnFrontPerson').val(obj.code);
				$('#txtFrontPerson').val(obj.name);
			}
			else if (obj.op == 2) {
				$('#hdnAnnouncer').val(obj.code);
				$('#txtAnnouncer').val(obj.name);
			}
			else if (obj.op == 3) {
				$('#hdnNextPerson').val(obj.code);
				$('#txtnextperson').val(obj.name);
			}
			else {
				$('#hdnAfterPerson').val(obj.code);
				$('#txtAfterPerson').val(obj.name);
			}
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	
	<asp:HiddenField ID="NodeType" runat="server" />
	<table style="height: 95%; width: 100%" class="tableAudit">
		<tr>
			<td valign="top" align="right" id="td-audit" style="height: 52%">
				<table id="tableVersion" cellspacing="0" cellpadding="0" width="100%" border="1"
					style="height: 100%">
					<tr>
						<td colspan="2" style="text-align: left">
							<input type="hidden" id="hdnNodeID" name="hdnNodeID" style="width: 10px" runat="server" />

							<input type="hidden" id="hdnType" name="hdnType" style="width: 10px" runat="server" />

							<input type="hidden" id="hdnInstanceCode" name="hdnInstanceCode" style="width: 10px" runat="server" />

							<input id="btnRet" type="button" value="还原" onclick="ReturnPage();" style="width: 60px;
								display: none" />
							<input id="btnFront" type="button" value="前插审核人" onclick="ckeck(0);" style="width: 80px" runat="server" />

							<input id="btnAfter" type="button" value="后插审核人" onclick="ckeck(1);" style="width: 80px" runat="server" />

							<input id="btnFlowStatus" type="button" value="查看流程状态" style="width: 100px" runat="server" />

							<input id="btnAuditrecord" type="button" value="查看审核记录" style="width: 100px" runat="server" />

						</td>
					</tr>
					<tr id="trResult">
						<td class="td-label" colspan="2" style="white-space: nowrap; width: 90%;">
							<asp:Label ID="Lbresult" Text="审核结果" runat="server"></asp:Label>
							<asp:RadioButtonList ID="RBLRoleType" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server"><asp:ListItem Selected="true" Value="1" Text="<span class='tooltip' title='通过'>通过</span>" /><asp:ListItem Value="-3" Text="<span class='tooltip' title='重报的数据可以修改后再次提交审核'>重报</span>" /><asp:ListItem Value="-2" Text="<span class='tooltip' title='驳回的数据不可以再次提交'>驳回</span>" /></asp:RadioButtonList>
							&nbsp; &nbsp; &nbsp;
							<asp:ImageButton ID="btnSubmit" Style="vertical-align: middle" Text="确 定" ImageUrl="~/images/auditimg.jpg" OnClick="btnSubmit_Click" runat="server" />
							&nbsp; &nbsp; &nbsp; 审核时限&nbsp; &nbsp; &nbsp;<asp:Label ID="LbDuring" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>&nbsp;小时 &nbsp; &nbsp; &nbsp;
							<asp:Label ID="LbDuringInfo" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
						</td>
					</tr>
					<tr id="trFront" style="display: none">
						<td class="td-label" id="tdFront">
							选择前插人员
						</td>
						<td class="td-content">
							<span class="spanSelect" style="width: 180px; float: left">
								<asp:TextBox ID="txtFrontPerson" Style="width: 155px; height: 15px; border: none;
									line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
								<img alt="选择人员" onclick="pickPerson(1);" src="../../images/icon.bmp" style="float: right" />
								<input type="hidden" id="hdnFrontPerson" name="hdnFrontPerson" style="width: 10px" runat="server" />

							</span>&nbsp;&nbsp;
							<img id="img1" src="../../images/help.jpg" alt="" title="前插审核人时，当前无需审核" style="vertical-align: middle" />
						</td>
					</tr>
					<tr id="trAfter" style="display: none">
						<td class="td-label">
							选择后插人员
						</td>
						<td class="td-content">
							<span class="spanSelect" style="width: 180px; float: left">
								<asp:TextBox ID="txtAfterPerson" Style="width: 155px; height: 15px; border: none;
									line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
								<img alt="选择人员" onclick="pickPerson(4);" src="../../images/icon.bmp" style="float: right" />
								<input type="hidden" id="hdnAfterPerson" name="hdnAnnouncer" style="width: 10px" runat="server" />

							</span>&nbsp;&nbsp;
							<img id="img2" src="../../images/help.jpg" alt="" title='1、“后插审核人”，会改变本次流程正常的审核流向；<br />2、选择“后插审核人”，系统默认流程状态是通过状态。'
								style="vertical-align: middle" />
						</td>
					</tr>
					<tr id="tr_selector" style="display: none" runat="server"><td class="td-label" style="white-space: nowrap" runat="server">
							请选择流程下一个审核人
						</td><td class="td-content" style="white-space: nowrap;" runat="server">
							<span class="spanSelect" style="width: 180px; float: left;">
								<asp:TextBox ID="txtnextperson" Style="width: 155px; height: 15px; border: none;
									line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
								<img alt="选择人员" id="IBPick" src="../../images/icon.bmp" style="float: right; cursor: pointer" runat="server" />

								<input type="hidden" id="hdnNextPerson" name="hdnNextPerson" style="width: 10px" runat="server" />

								<asp:Label ID="lblMessage" Visible="false" Style="color: Red; margin-left: 25px" runat="server"></asp:Label>
							</span>
						</td></tr>
					<tr id="trPass" visible="false" runat="server"><td class="td-label" runat="server">
							<asp:Label ID="LbAduPass" Text="二次验证密码" runat="server"></asp:Label>
						</td><td class="td-content" runat="server">
							<asp:TextBox ID="txtAuditPwd" TextMode="Password" Width="180px" runat="server"></asp:TextBox>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAuditPwd" Display="None" ErrorMessage="请填写二次验证密码！" runat="server"></asp:RequiredFieldValidator>
						</td></tr>
					<tr id="trAuditMain" style="display: ''; width: 100%;" runat="server"><td class="td-label" runat="server">
							审核要点
						</td><td class="td-content" style="padding-top: 2px; width: 80%;" runat="server">
							<div id="divAuditMain" runat="server">
							</div>
						</td></tr>
					<tr id="trAuditRemark" style="display: none; width: 100%">
						<td class="td-label">
							审核要点
							<img id="img3" src="../../images/help.jpg" alt="" title='1、	由于是临时插入审核人，改变了流程正常的审核流向；一般情况下，需要对临时插入的审核人说明该流程的“审核要点”；<br />2、	“审核要点”仅对该插入的审核人有效；<br />3、	“审核要点”不是必填项。'
								style="vertical-align: middle" />
						</td>
						<td class="td-content" style="padding-top: 2px; height: 120px; width: 80%;">
							<textarea id="txtAuditRemark" class="wysiwyg" cols="80" runat="server"></textarea>
						</td>
					</tr>
					<tr id="trAuditInfo">
						<td class="td-label">
							审核意见
						</td>
						<td class="td-content" style="padding-top: 2px; height: 120px">
							<textarea id="txtAuditInfo" class="wysiwyg" cols="80" runat="server"></textarea>
						</td>
					</tr>
					<tr id="trUpFiles">
						<td class="td-label">
							相关附件
						</td>
						<td class="td-content" style="padding-top: 3px">
							<MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" runat="server" />
						</td>
					</tr>
					<tr id="trAnnouncer">
						<td class="td-label">
							告知人
						</td>
						<td class="td-content">
							<span class="spanSelect" style="width: 320px">
								<asp:TextBox ID="txtAnnouncer" Style="width: 295px; height: 15px; border: none; line-height: 16px;
									margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
								<img alt="选择人员" onclick="pickMulPerson();" src="/images/icon.bmp" style="float: right;" />
								<input type="hidden" id="hdnAnnouncer" name="hdnAnnouncer" style="width: 10px" runat="server" />

							</span>
						</td>
					</tr>
					<tr id="trContent">
						<td class="td-label">
							告知内容
						</td>
						<td class="td-content" style="padding-top: 2px; height: 120px; width: 80%">
							<textarea id="txtContent" class="wysiwyg" cols="80" runat="server"></textarea>
						</td>
					</tr>
					<tr id="trSend">
						<td>
						</td>
						<td class="td-content">
							<asp:CheckBox ID="ckbIsSendMsg" Text="短信通知下一个审核人" runat="server" />&nbsp;
						</td>
					</tr>
				</table>
				<asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
			</td>
		</tr>
	</table>
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
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$("#img1").tooltip({ showURL: false });
			$("#img2").tooltip({ showURL: false });
			$("#img3").tooltip({ showURL: false });
			$('#txtAfterPerson').attr('readOnly', 'readOnly');
			$('#txtFrontPerson').attr('readOnly', 'readOnly');
			$('#txtnextperson').attr('readOnly', 'readOnly');
			$('#txtAnnouncer').attr('readOnly', 'readOnly');
			if ($('#hfldIsAllowRebut').val() == "0") {
				//禁用驳回
				var roleTypeNames = document.getElementsByName('RBLRoleType');
				roleTypeNames[3].disabled = "disabled";
			}
		});
	</script>
	<iframe src="" id="fileFrame" height="0px" width="0px"></iframe>
	<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	
	<asp:HiddenField ID="hfldIsAllowRebut" runat="server" />
	
	<asp:HiddenField ID="hfldNextAuditDepCode" runat="server" />
	</form>
</body>
</html>
