<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddContractPayend.aspx.cs" Inherits="ContractManage_ContractPayend_AddContractPayend" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>收入合同交底</title>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<style type="text/css">
		#FileLink1_But_UpFile
		{
			width: auto;
		}
		#FileLink1_Btn_Upload
		{
			width: auto;
		}
	</style>
	<script type="text/javascript">
		$(document).ready(function () {
			if (getRequestParam('t') == '1') {
				setAllInputDisabled();
			}
			Val.validate('form1');
		});

		//选择人员返回值
		function returnUser(id, name) {
			document.getElementById('hdBWasPerson').value = id;
			document.getElementById('txtBWasPerson').value = name;
		}
		//选择人员
		function selectUser() {
			jw.selectMultiUser({ codeinput: 'hdBWasPerson', nameinput: 'txtBWasPerson', idcsv: $('#hdBWasPerson').val() });
		}
		//选择合同返回值
		function returnContract(id, name) {
			document.getElementById('hdContractId').value = id;
			document.getElementById('txtContractName').value = name;
			document.getElementById("btnRe").click();
		}
		//选择合同
		function selectContract() {
			jw.selectInCon({ func: function (con) {
				$('#hdContractId').val(con.id);
				$('#txtContractName').val(con.name);
				$('#txtContractCode').val(con.code);
				$('#txtContractPrice').val(con.price);
				$('#txtSignedTime').val(con.signDate);
			}
			});
		}

		//表单验证
		function valForm() {
			if (document.getElementById("hdBWasPerson").value == "") {
				top.ui.alert("被交底人必须输入");
				return false;
			}
			if (document.getElementById("hdContractId").value == "") {
				top.ui.alert("请选择合同");
				return false;
			}
			return true;
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader2">
		<table class="tableHeader2">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div class="divContent2">
		<table class="tableContent2" cellpadding="5px" cellspacing="0">
			<tr>
				<td class="word" style="text-align: right;">
					被交底人
				</td>
				<td colspan="3" style="padding-right: 3px;" class="txt mustInput">
					<asp:TextBox Width="100%" ID="txtBWasPerson" CssClass="select_input {required:true, messages:{required:'被交底人必须输入'}}" ReadOnly="true" title="双击选择被交底人" ondblclick="selectUser()" imgclick="selectUser" runat="server"></asp:TextBox>
					<asp:HiddenField ID="hdBWasPerson" runat="server" />
				</td>
			</tr>
			<tr>
				<td align="right">
					交底主题
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtPayendTopics" Width="100%" MaxLength="64" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 80px;">
					合同名称
				</td>
				<td style="width: 33%; padding-right: 3px;" class="txt mustInput">
					<asp:TextBox ID="txtContractName" CssClass="select_input {required:true, messages:{required:'请选择合同'}}" ReadOnly="true" imgclick="selectContract" runat="server"></asp:TextBox>
					<asp:HiddenField ID="hdContractId" runat="server" />
				</td>
				<td style="text-align: right; width: 80px;">
					合同编号
				</td>
				<td style="width: 33%">
					<asp:TextBox ID="txtContractCode" class="readonly" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="text-align: right;">
					合同金额
				</td>
				<td>
					<asp:TextBox ID="txtContractPrice" class="readonly" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
				<td style="text-align: right;">
					签订时间
				</td>
				<td>
					<asp:TextBox class="readonly" ID="txtSignedTime" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="text-align: right;" id="td1">
					附件
				</td>
				<td colspan="3" style="text-align: left; padding-right: 0px;">
					<MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
				</td>
			</tr>
			<tr>
				<td align="right">
					主要条款及注意事项
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtProvisionMatter" TextMode="MultiLine" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td align="right">
					支付条件和履约保函
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtProjectCondition" TextMode="MultiLine" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td align="right">
					其他说明
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtOtherExplain" TextMode="MultiLine" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="text-align: right;">
					录入人
				</td>
				<td>
					<asp:TextBox ID="txtInputPerson" class="readonly" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
				<td style="text-align: right;">
					录入时间
				</td>
				<td>
					<asp:TextBox class="readonly" Width="100%" ReadOnly="true" ID="txtInputDate" runat="server"></asp:TextBox>
				</td>
			</tr>
		</table>
	</div>
	<div class="divFooter2">
		<table class="tableFooter2" style="width: 100%;">
			<tr>
				<td>
					<asp:Button ID="btnSub" Text="提交" OnClick="btnSub_Click" runat="server" />
					<asp:Button ID="btnSaves" Text="暂存" OnClientClick="return valForm();" OnClick="btnSave_Click" runat="server" />
					<input type="button" id="btnCancel" value="取消" style="display: none;" onclick="window.close()" />
					
					<input type="button" id="btnReturn" onclick="winclose('AddContractPayend','PayendSend.aspx',false)"
						value="返回" />
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hdGuid" runat="server" />
	<asp:HiddenField ID="hdCode" runat="server" />
	<asp:HiddenField ID="hdTitle" runat="server" />
	<asp:HiddenField ID="hdModifyState" runat="server" />
	<asp:HiddenField ID="hdContractEditId" runat="server" />
	<asp:Button ID="btnRe" Style="display: none;" OnClick="btnRe_Click" runat="server" />
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<div id="divFramContract" title="" style="display: none">
		<iframe id="iframeContract" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	</form>
</body>
</html>
