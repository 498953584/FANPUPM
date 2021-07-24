<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WF.ascx.cs" Inherits="EPC_UserControl1_WF" %>



<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />
<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>

<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
<script language="javascript" type="text/javascript">
	$(document).ready(function () {
		$('#divinfo').hide();
		// 超级删除
		$('input[id$="BtnWFDel"]').css('display', 'none');
		$('input[id$="BtnWFDel"]').click(adminDel);


		//提交审核
		$('#btnStartWF').click(function () {
			var parentURL = getURl();
			var src = "/EPC/WorkFlow/StartWF.aspx?"
				+ "fid=" + $("#WF1_FID").val()
				+ "&bcode=" + $("#WF1_BusinessCode").val()
				+ "&bclass=" + $("#WF1_BusinessClass").val()
				+ "&prjguid=" + $("#WF1_PrjGuid").val()
			if (parentURL != '')
				src += "&purl=" + escape(parentURL);

			top.ui._StartWf = window;
			top.ui.openWin({ title: '提交审核', url: src, width: 520, height: 460 });
		});

		//撤回审核  
		$('#CancelBt').click(function () {
			$('#divinfo').show().window('open');
		});
	})

	// 获取URL
	function getURl() {
		// 添加参数
		var locationHref = '';
		var parameter = $('#WF1_hfldURLParameter').val();
		if (parameter != '') {
			var subIndex = window.location.href.indexOf('?');
			if (subIndex == -1) {
				locationHref = window.location.href + '?' + parameter;
			}
			else {
				locationHref = window.location.href.substring(0, subIndex + 1) + parameter;
			}
		}
		return locationHref;
	}

	// 查看审核
	function OpenViewWF() {
		var rid = document.getElementById('WF1_FID').value;
		var BusinessCode = document.getElementById('WF1_BusinessCode').value;
		var BusinessClass = document.getElementById('WF1_BusinessClass').value;
		var url = "/epc/Workflow/AuditViewWF.aspx?ic=" + rid + '&bc=' + BusinessCode + '&bcl=' + BusinessClass;
		window.open(url, '', "left=150,top=150,width=600px,height=460px,toolbar=no,status=yes,scrollbars=yes,resiz able=no");
	}

	// 查看审核记录
	function OpenPrintWF() {
		var rid = document.getElementById('WF1_FID').value;
		var BusinessCode = document.getElementById('WF1_BusinessCode').value;
		var BusinessClass = document.getElementById('WF1_BusinessClass').value;
		var content = document.getElementById("WF1_hidcontent").value;
		var url = "/epc/Workflow/AuditViewPrint.aspx?ic=" + rid + '&bc=' + BusinessCode + '&bcl=' + BusinessClass + "&content=" + encodeURI(content);
		window.open(url, '', "left=150,top=5,width=800,height=560,toolbar=no,status=yes,scrollbars=yes,resiz able=no");
	}

	// 超级删除
	function adminDel(btn) {
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
			guid: $('input[id$="FID"]').val(),
			busiCode: $('input[id$="BusinessCode"]').val(),
			busiClass: $('input[id$="BusinessClass"]').val()
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
					top.ui.alert('超级删除失败.');
				}
			}
		});
	}

	// 撤回
	function recall() {
		$.ajax({
			url: '/EPC/UserControl1/CancelAudit.ashx?' + new Date().getTime(),
			data: { fid: $('#WF1_FID').val(), bcode: $('#WF1_BusinessCode').val(), bclass: $('#WF1_BusinessClass').val() },
			success: function (data) {
				if (data == "1") {
					$('divinfo').window("close");

					var purl = getURl();
					if (purl != '')
						window.location.href = purl;
					else
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

</script>
<asp:HiddenField ID="FID" runat="server" />
<asp:HiddenField ID="BusinessCode" runat="server" />
<asp:HiddenField ID="BusinessClass" runat="server" />
<asp:HiddenField ID="PrjGuid" runat="server" />
<asp:HiddenField ID="hidcontent" runat="server" />

<asp:HiddenField ID="hfldURLParameter" runat="server" />
<input type="button" value="提交审核" style="width: 75px;" id="btnStartWF" disabled="disabled" />
<input type="button" value="撤回" id="CancelBt" disabled="disabled" />&nbsp;
<input id="btnViewWF" type="button" value="流程状态" disabled="true" style="width: 75px" onclick="javascript:return OpenViewWF();" runat="server" />

<input id="btnWFPrint" type="button" value="审核记录" disabled="true" style="width: 75px" onclick="javascript:return OpenPrintWF();" runat="server" />

<input type="button" id="BtnWFDel" value="超级删除" style="width: 75px;" disabled="disabled" />
<div id="divinfo" title="撤回审核" class="easyui-window" data-options="modal:true,closed:true,iconCls:'icon-save'"
	style="width: 350px; height: 220px; padding: 10px; display: none;">
	<img alt="" src="/images/help.jpg" />&nbsp; <span style="font-size: 15px; font-weight: bold">
		确定撤回此审核吗？</span>
	<br />
	<br />
	<div style="line-height: 20px">
		详细说明：<br />
		1.如果对方已经审核，将不予撤回<br />
		2.只有审核中的单据才能撤回<br />
		3.撤回后的单据为未提交状态<br />
		4.如果该单据重报过，撤回后为重报状态<br />
	</div>
	<div style="text-align: right;">
		<input type="button" id="btnWfRecallSave" value="保存" onclick="recall();" />
		<input type="button" id="btnWfRecallCancel" value="取消" onclick="$('#divinfo').window('close')" />
	</div>
</div>
<!-- 超级删除密码 -->
<asp:HiddenField ID="hfldPwd" runat="server" />
