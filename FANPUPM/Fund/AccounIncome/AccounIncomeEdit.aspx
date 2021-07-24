<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccounIncomeEdit.aspx.cs" Inherits="Fund_AccounIncome_AccounIncomeEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>入账编辑</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/DecimalInput.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			registerCancelEvent();
			setAuditState();
			document.getElementById('btnSave').onclick = btnSaveClick;
			TypeChange();
		});

		function btnSaveClick() {
			if (!validate()) {
				return false;
			}
		}

		function validate() {
			if (!document.getElementById('txtAccCode').value) {
				top.ui.alert('入账单编码不能为空');
				return false;
			}
			if (!document.getElementById('hdnProjectCode').value) {
				top.ui.alert('项目不能为空');
				return false;
			}

			if (!document.getElementById('DaGet').value) {
				top.ui.alert('入账日期不能为空');
				return false;
			}
			if ($('#ddlType').val() == "0") {
				if (!$('#txtGetMoney').val() || $('#txtGetMoney').val() <= 0) {
					top.ui.alert('回款金额不能为空或小于等于零！');
					return false;
				}
			}
			if (!document.getElementById('txtInMoney').value || document.getElementById('txtInMoney').value <= 0) {
				top.ui.alert('入账金额不能为空或小于等于零!');
				return false;
			}

			return true;
		}

		function TypeChange() {
			var DDL = document.getElementById("ddlType");
			var index = DDL.selectedIndex; //这里selectedIndex中Index的I一定为大写

			var Text = DDL.options[index].text;
			var Value = DDL.options[index].value;
			//alert(Text);
			if (Value != 0) {
				document.getElementById("td_word").style.display = "none";
				document.getElementById("td_txt").style.display = "none";
				document.getElementById("TR_Cont").style.display = "none";
				document.getElementById("hdnContCode").value = "";
				document.getElementById("hdnPlanUid").value = "";
				document.getElementById("TR_Money").style.display = "none";
			}
			else {
				document.getElementById("td_word").style.display = "";
				document.getElementById("td_txt").style.display = "";
				document.getElementById("TR_Cont").style.display = "";
				document.getElementById("TR_Money").style.display = "";
			}
		}

		function selectContr() {
			if ($('#hdnProjectCode').val() == '') {
				top.ui.alert('请先选择项目');
				return false;
			}
			var url = "/Fund/AccountPayOut/SelectIncomePay.aspx?prjguid=" + $('#hdnProjectCode').val();
			top.ui.callback = function (o) {
				$('#hdnContCode').val(o.id);
				$('#txtContName').val(o.code);
				$('#btnPlan').click();
			}
			top.ui.openWin({ title: '选择收入合同', url: url, width: 800, height: 500 });
		}

		function registerCancelEvent() {
			var btnCancel = document.getElementById('btnCancel');
			addEvent(btnCancel, 'click', function () {
				winclose('ModifyEdit', 'AccounIncome.aspx', false)
			})
		}

		//选择项目
		function openProjPicker() {
			var url = '/Fund/AccounIncome/DivSelectProject.aspx?ZHID=' + $('#hdnZHID').val();
			top.ui.callback = function (o) {
				$('#hdnProjectCode').val(o.id);
				$('#txtPrjName').val(o.name);
			}
			top.ui.openWin({ title: '选择项目', url: url, width: 700 });
			return false;
		}

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader2">
		<table class="tableHeader2">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="入账单编辑" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div id="divContent2" class="divContent2">
		<table id="tableContent" class="tableContent2" cellpadding="5px" cellspacing="0">
			<tr>
				<td class="word">
					入账单编号
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtAccCode" MaxLength="50" Height="15px" CssClass="{required:true, messages:{required:'入账单编号必须输入'}}" Width="100%" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					项目
				</td>
				<td class="txt mustInput">
					<span class="spanSelect" style="width: 100%;">
						<asp:TextBox ID="txtPrjName" ContentEditable="false" CssClass="{required:true, messages:{required:'项目必须输入'}}" Style="width: 90%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
						<img alt="选择项目" onclick="openProjPicker();" src="../../images/icon.bmp" style="float: right;" />
					</span>
					<asp:HiddenField ID="hdnProjectCode" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="word">
					入账类型
				</td>
				<td class="txt">
					<asp:DropDownList ID="ddlType" ContentEditable="false" Width="101%" runat="server"><asp:ListItem Value="0" Text="合同入账" /><asp:ListItem Value="1" Text="启动资金" /><asp:ListItem Value="2" Text="其他入账" /></asp:DropDownList>
				</td>
				<td class="word" id="td_word">
					合同回款单
				</td>
				<td class="txt mustInput" id="td_txt">
					<span class="spanSelect" style="width: 100%">
						<asp:TextBox ID="txtContName" ContentEditable="false" CssClass="{required:true, messages:{required:'依据合同必须输入'}}" Style="width: 90%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
						<img alt="选择合同收入" onclick="selectContr();" src="/images/icon.bmp" style="float: right;" />
					</span>
					<asp:HiddenField ID="hdnContCode" runat="server" />
				</td>
			</tr>
			<tr id="TR_Cont">
				<td class="word">
					所属计划
				</td>
				<td class="txt readonly">
					<asp:TextBox ID="txtPlan" ContentEditable="false" Height="15px" Width="100%" runat="server"></asp:TextBox>
					<input type="hidden" id="hdnUID" runat="server" />

					<input type="hidden" id="hdnPlanUid" runat="server" />

				</td>
				<td class="word">
					计划金额
				</td>
				<td class="txt readonly">
					<asp:TextBox ID="txtPlanMoney" ContentEditable="false" Height="15px" Width="100%" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr id="TR_Money">
				<td class="word">
					回款金额
				</td>
				<td class="txt readonly">
					<asp:TextBox ID="txtGetMoney" ContentEditable="false" CssClass=" decimal_input  {required:true, number:true, messages:{required:'收账金额必须输入', number:'收账金额格式错误'}}" Height="15px" Width="100%" runat="server"></asp:TextBox>
				</td>
				<td class="word">
				</td>
				<td class="txt">
				</td>
			</tr>
			<tr>
				<td class="word">
					入账日期
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="DaGet" Width="100%" CssClass="{required:true, messages:{required:'到款日期必须输入'}}" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					入账金额
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtInMoney" CssClass=" decimal_input  {required:true, number:true, messages:{required:'入账金额必须输入', number:'入账金额格式错误'}}" Height="15px" Width="100%" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					附件
				</td>
				<td colspan="3" style="padding-right: 0px;">
					<MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="word">
					备注
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtRemark" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					入账人
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtInPeople" ContentEditable="false" Height="15px" Width="100%" runat="server"></asp:TextBox>
					<input type="hidden" id="hdnPeopleCode" runat="server" />

				</td>
				<td class="word">
					入账时间
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtInDate" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
			</tr>
		</table>
	</div>
	<div id="divFooter" class="divFooter2">
		<table class="tableFooter2">
			<tr>
				<td>
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
					<input type="button" id="btnCancel" value="取消" />
				</td>
			</tr>
		</table>
	</div>
	<div id="divSelectFromPlan" title="从计划中选择" style="display: none">
		<iframe id="ifSelectFromPlan" frameborder="0" width="100%" height="100%" runat="server">
		</iframe>
	</div>
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<asp:HiddenField ID="hdnAccountID" runat="server" />
	<input id="hdnZHID" type="hidden" runat="server" />

	<asp:Button ID="btnPlan" Width="0px" Text="Button" OnClick="btnPlan_Click" runat="server" />
	</form>
</body>
</html>
