<%@ Page Language="C#" AutoEventWireup="true" CodeFile="technologyjdedit.aspx.cs" Inherits="TechnologyJDEdit" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>工程施工技术交底</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/Watermark/jquery_Watermark.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">

		function openannexpage(RecordCode, type) {
			if (type != "View") {
				window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid=1728&rc=" + RecordCode + "&at=0&type=2", "", 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
			}
			else {
				window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid=1728&rc=" + RecordCode + "&at=0&type=1", "", 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
			}
		}
		//选择人员
		function selectUser() {
			jw.selectOneUser({ codeinput: 'HdnTell', nameinput: 'TxtTellName' });
		}

		$(function () {
			var mark = document.getElementById("hidenMark").value;
			if (mark == 1) {
				GetWaterMarked(window, '/images/yinzh.jpg', 'this');
			}
			var temFl = $("#cbkmark").attr("checked");
			if (temFl) {
				$("#c1").show();
				$("#c2").show();
			} else {
				$("#c1").hide();
				$("#c2").hide();
			}
			var page_type = request("Type");
			if (page_type.toLowerCase() == "add") {
				$("#showText").hide();
			}
			if (page_type.toLowerCase() == "see") {
				$("#FileLink1_But_UpFile").attr("disabled", "disabled");
				//$("#FileLink1_Btn_Upload").attr("disabled", "disabled");
				$("#FileLink1_txtFile").attr("disabled", "disabled");
			}
			if (page_type.toLowerCase() == "view") {
				$("#FileLink1_But_UpFile").attr("disabled", "disabled");
				//$("#FileLink1_Btn_Upload").attr("disabled", "disabled");
				$("#FileLink1_txtFile").attr("disabled", "disabled");
			}
			$("#DDTClass").val($("#hidenClass").val());
			$("#cbkmark").click(function () {
				var flag = $(this).attr("checked");
				if (flag) {
					$("#c1").show();
					$("#c2").show();
				} else {
					$("#c1").hide();
					$("#c2").hide();
				}
			});
			var _mk = $("#hidenMark").val();
			if (_mk == "1") {
				setAllInputDisabled();
			}
			//如果是审核页面,隐藏关闭按钮
			var parentLocation = window.parent.location.href;
			if (parentLocation.indexOf('AuditFrame.aspx') > 0) {
				$('#divBtns').attr('style', 'display:none');
			}
		});
		function request(paras) {
			var url = location.href;
			var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
			var paraObj = {}
			for (i = 0; j = paraString[i]; i++) {
				paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
			}
			var returnValue = paraObj[paras.toLowerCase()];
			if (typeof (returnValue) == "undefined") {
				return "";
			} else {
				return returnValue;
			}
		}
		function setAllInputDisabled() {
			var elements = document.getElementsByTagName('*');
			for (var i = 0; i < elements.length; i++) {
				if (elements[i].nodeName == 'INPUT') {
					if (elements[i].id == 'btnCancel') {
						continue;
					}
					if (elements[i].id == 'btnDy') {
						continue;
					}
					if (elements[i].id.indexOf('Btn_Upload') > -1) {
						elements[i].value = "查看附件";
						try {
							var str = elements[i].onclick.toString();
							var index = str.indexOf('(\'') + 2;
							//GUID的Length为36
							var param1 = str.substring(index, index + 36);
							var param2 = str.substring(index + 38, str.indexOf(');'));
							//			                    elements[i].onclick = function() {
							//			                        //viewAnnex(param1, param2);
							//			                    }
						}
						catch (e) {
						}
						continue;
					}
					elements[i].setAttribute('disabled', 'disabled');
					setDisabled(elements[i]);
				}
				if (elements[i].nodeName == 'TEXTAREA') {
					setReadOnly(elements[i]);
				}
				if (elements[i].nodeName == 'IMG') {
					setDisabled(elements[i]);
				}
				if (elements[i].nodeName == 'OPTION') {
					setDisabled(elements[i]);
				}
				if (elements[i].nodeName == 'SELECT') {
					setDisabled(elements[i]);
				}
				if (elements[i].nodeName == 'SPAN') {
					if (elements[i].className == 'alarm') {
						continue;
					}
					if (elements[i].id == 'lblTitle') {
						//查看页面的标题
						continue;
					}
					setDisabled(elements[i]);
				}
				//控制背景色
				function setDisabled(elem) {
					elem.style.backgroundColor = 'white';
					elem.setAttribute('disabled', 'disabled');
				}

				function setReadOnly(elem) {
					elem.style.backgroundColor = 'white';
					elem.setAttribute('readOnly', 'readOnly');
				}
			}
		}
	</script>
</head>
<body scroll="auto">
	<form id="Form1" method="post" runat="server">
	<asp:HiddenField ID="hidenMark" runat="server" />
	<font face="宋体">
		<div class="divContent2">
			<table width="100%">
				<tr>
					<td class="divHeader">
						<asp:Label ID="lb_change" Font-Bold="true" runat="server">label</asp:Label>
					</td>
				</tr>
			</table>
			<table class="tableContent2" id="Table1" cellspacing="0" cellpadding="5px" width="100%"
				border="0">
				<tr>
					<td class="word" style="white-space: nowrap;">
						编号：
					</td>
					<td class="txt">
						<asp:TextBox ID="TxtId" runat="server"></asp:TextBox>
					</td>
					<td class="word" style="white-space: nowrap;">
						工程名称：
					</td>
					<td class="txt">
						<asp:TextBox ID="TxtPrjName" ReadOnly="true" runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="word" style="white-space: nowrap;">
						施工单位：
					</td>
					<td class="txt">
						<asp:TextBox ID="TxtConUnit" runat="server"></asp:TextBox>
					</td>
					<td class="word" style="white-space: nowrap;">
						被交底单位：
					</td>
					<td class="txt">
						<asp:TextBox ID="TxtByTellUnit" runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="word" style="white-space: nowrap;">
						交底人：
					</td>
					<td class="txt">
						<span id="span1" class="spanSelect" style="width: 99%; background-color: #FEFEF4;">
							<input type="text" style="width: 89%; background-color: #FEFEF4; height: 15px; border: none;
								line-height: 16px; margin: 1px 2px" id="TxtTellName" runat="server" />

							<img id="Img1" src="~/images/icon.bmp" style="float: right;" onclick="selectUser()" alt="选择编制人" runat="server" />

							<input id="HdnTell" type="hidden" name="Hidden2" runat="server" />

						</span>
					</td>
					<td class="word" style="white-space: nowrap;">
						被交底人：
					</td>
					<td class="txt">
						<asp:TextBox ID="TxtByTellName" runat="server"></asp:TextBox><input id="HdnByTell" type="hidden" name="Hidden1" runat="server" />

					</td>
				</tr>
				<tr>
					<td class="word" style="white-space: nowrap;">
						交底日期：
					</td>
					<td class="txt">
						<asp:TextBox ID="DateTellTime" onclick="WdatePicker()" runat="server"></asp:TextBox>
					</td>
					<td>
					</td>
					<td>
					</td>
				</tr>
				<tr>
					<td class="word" style="white-space: nowrap;">
						交底地点：
					</td>
					<td colspan="3" class="txt">
						<asp:TextBox ID="TxtTellLocus" runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="word">
						作为归档资料：
					</td>
					<td class="txt">
						<asp:CheckBox ID="cbkmark" runat="server" />
					</td>
					<td class="word">
						<div id="c1">
							归档类别：
						</div>
					</td>
					<td class="txt">
						<div id="c2">
							<asp:HiddenField ID="hidenClass" runat="server" />
							<JWControl:DropDownTree ID="DDTClass" Width="100%" runat="server"></JWControl:DropDownTree>
						</div>
					</td>
				</tr>
				<tr>
					<td class="word" style="white-space: nowrap;">
						附件：
					</td>
					<td colspan="3" class="txt">
						<MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
						<asp:Literal ID="Literal1" runat="server"></asp:Literal>
					</td>
				</tr>
				<tr>
					<td class="word" style="white-space: nowrap;">
						交底内容提要：
					</td>
					<td colspan="3" class="txt">
						<asp:TextBox ID="TxtContentAbstract" Rows="5" TextMode="MultiLine" runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="word" style="white-space: nowrap;">
						备注：
					</td>
					<td colspan="3" class="txt">
						<asp:TextBox ID="TxtRemark" Rows="3" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="word" style="white-space: nowrap;">
						填报人：
					</td>
					<td class="txt">
						<asp:TextBox ID="TxtFillName" ReadOnly="true" runat="server"></asp:TextBox><input id="HdnFill" type="hidden" name="Hidden2" runat="server" />

					</td>
					<td class="word" style="white-space: nowrap;">
						填报日期：
					</td>
					<td class="txt" width="35%">
						<asp:TextBox ID="DateFill" ReadOnly="true" runat="server"></asp:TextBox>
					</td>
				</tr>
			</table>
			<div class="divFooter2" id="divBtns">
				<table class="tableFooter2">
					<tr>
						<td class="td-submit" colspan="4">
							<asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;
							<input onclick="top.ui.closeTab();" type="button" value="取 消" id="BunClose" runat="server" />

							<JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl>
						</td>
					</tr>
				</table>
			</div>
		</div>
	</font>
	<asp:HiddenField ID="hdnTechGuid" runat="server" />
	</form>
</body>
</html>
