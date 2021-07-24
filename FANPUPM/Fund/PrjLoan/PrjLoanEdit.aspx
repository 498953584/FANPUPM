<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjLoanEdit.aspx.cs" Inherits="PrjLoanEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>编辑项目借款</title><link Href="/Script/jquery.treeview/jquery.treeview.css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
		$(function () {
			Val.validate('form1', 'btnSave');
		});
		function btnCancel_onclick() {
			top.ui.closeTab();
		}

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent2" class="divContent2">
		<table id="tableContent2" class="tableContent2" cellpadding="5px" cellspacing="0">
			<tr>
				<td class="word">
					借款单号
				</td>
				<td class="txt ">
					<asp:TextBox ID="txtLoanCode" Width="96%" CssClass="{required:true, messages:{required:'还款单号必须输入'}}" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					借款人
				</td>
				<td class="txt ">
					<asp:Label ID="labLoanMan" Text="" runat="server"></asp:Label>
					<input id="hdnLoanMan" type="hidden" style="width: 96%;" runat="server" />

				</td>
			</tr>
			<tr>
				<td class="word">
					借款金额
				</td>
				<td class="txt mustInput">
					<input id="txtLoanFund" type="text" style="width: 96%;" class="decimal_input   {required:true,number:true, messages:{required:'借款金额必须输入',number:' 借款金额格式错误'}}" runat="server" />

				</td>
				<td class="word">
					借款用途
				</td>
				<td class="txt">
					<input id="txtLoanCause" type="text" style="width: 96%;" runat="server" />

				</td>
			</tr>
			<tr>
				<td class="word">
					借款日期
				</td>
				<td class="txt  mustInput">
					<asp:TextBox ID="txtLoanDate" Width="96%" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'借款日期必须输入'}}" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					借款利率
				</td>
				<td class="txt ">
					<input id="txtLoanRate" type="text" class="decimal_input   {number:true, messages:{number:' 借款利率格式错误'}}" style="width: 96%;" runat="server" />
%
				</td>
			</tr>
			<tr>
				<td class="word">
					约定归还日期
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtPlanReturnDate" Width="96%" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'约定归还日期必须输入'}}" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					选择项目
				</td>
				<td colspan="3" class="txt mustInput">
					<asp:DropDownList ID="dropPrjCodeList" Width="100%" CssClass="{required:true, messages:{required:'项目必须选择'}}" runat="server"></asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td class="word">
					附件
				</td>
				<td class="txt " colspan="3">
					<MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" FileType="*.*" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="word">
					备注
				</td>
				<td colspan="3" class="txt ">
					<textarea id="txaRemark" cols="20" rows="2" runat="server"></textarea>
				</td>
			</tr>
		</table>
	</div>
	<div id="divFooter" class="divFooter2">
		<table class="tableFooter2">
			<tr>
				<td>
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
					<input type="button" id="btnCancel" value="取消" onclick="return btnCancel_onclick()" />
				</td>
			</tr>
		</table>
	</div>
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	</form>
</body>
</html>
