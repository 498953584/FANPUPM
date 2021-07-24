<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PettyCashEdit.aspx.cs" Inherits="PettyCash_PettyCashEdit" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>备用金申请</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="../Script/jquery.autocomplete/jquery.autocomplete.css" />
<link rel="Stylesheet" type="text/css" href="CSS/PettyCash.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/Script/json2.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="/Script/DecimalInput.js"></script>
	<script type="text/javascript" src="../Script/jquery.autocomplete/jquery.autocomplete.min.js"></script>
	<script type="text/javascript" src="Script/PettyCashEdit.js"></script>
    
</head>
<body>
	<form id="form1" runat="server">
	<div style="overflow: auto; height: 95%;">
		<table class="tabHeader" style="width: 670px;">
			<tr>
				<td colspan="3">
					<asp:Label ID="lblTitle" CssClass="title" Text="" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="text-align: left; margin: 0px, 10px,0px, 10px;">
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;付款单位:
					<asp:TextBox ID="txtPayer" Width="300px" CssClass="easyui-validatebox" data-options="required:true, validType:'validChars[50]'" runat="server"></asp:TextBox>
					申请日期:
					<asp:TextBox ID="txtApplicationDate" onclick="WdatePicker({isShowClear: false})" runat="server"></asp:TextBox>
				</td>
				<td>
				</td>
			</tr>
		</table>
		<table class="tab2">
			<tr>
				<td class="descTd">
					&nbsp;&nbsp;&nbsp;申请部门
				</td>
				<td>
					<asp:TextBox ID="txtDepart" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
				<td class="descTd">
					&nbsp;&nbsp;&nbsp;申请人
				</td>
				<td>
					<asp:TextBox ID="txtApplicant" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="descTd" style="">
					<span>* </span>申请人账号
				</td>
				<td>
					<asp:TextBox ID="txtAccount" CssClass="easyui-validatebox" data-options="required:true, validType:'validChars[50]'" runat="server"></asp:TextBox>
				</td>
				<td class="descTd">
					<span>* </span>开户行
				</td>
				<td>
					<asp:TextBox ID="txtBank" CssClass="easyui-validatebox" data-options="required:true, validType:'validChars[50]'" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="descTd">
					<span>* </span>事项
				</td>
				<td>
					<asp:TextBox ID="txtMatter" CssClass="easyui-validatebox" data-options="required:true, validType:'validChars[50]'" runat="server"></asp:TextBox>
				</td>
				<td class="descTd">
					&nbsp;&nbsp;&nbsp;项目
				</td>
				<td>
					<asp:TextBox ID="txtProject" Width="180px" ReadOnly="true" runat="server"></asp:TextBox>
					<asp:HiddenField ID="hfldPrjTypeCode" runat="server" />
					<img src="../../images/select.gif" style="float: right; cursor: pointer" alt="选择" id="imgName" onclick="selectProject();" runat="server" />

				</td>
			</tr>
			<tr style="height: 40px;">
				<td class="descTd">
					<span>* </span>本次申请金额
				</td>
				<td style="vertical-align: top;">
					<asp:TextBox ID="txtCash" CssClass="decimal_input" runat="server"></asp:TextBox><br />
					<span id="spanCashWords"></span>
				</td>
				<td class="descTd">
					<span>* </span>用款日期
				</td>
				<td>
					<asp:TextBox ID="txtCashDate" onclick="WdatePicker({isShowClear: false})" CssClass="easyui-validatebox" data-options="required:true" runat="server"></asp:TextBox>
				</td>
			</tr>
            <tr>
               <td>收款单位</td>
               <td colspan="3">
                   <asp:TextBox ID="txtpayee" runat="server"></asp:TextBox>
               </td>
            </tr>
			<tr id="trResson" style="min-height: 100px;">
				<td>
					&nbsp;&nbsp;&nbsp;申请事由
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtApplicationReason" TextMode="MultiLine" CssClass="easyui-validatebox" data-options="validType:'validChars[4000]'" Style="min-height: 300px;" runat="server"></asp:TextBox>
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
	<div id="win" class="eui_dlg" title="选择项目" style="width: 700px; height: 480px; display: none">
		<iframe frameborder="0" width="100%" height="98%" src="/Common/DivSelectProject.aspx?CallBack=retProject">
		</iframe>
	</div>

   
	</form>
	<script type="text/javascript">
		// 动态调整申请理由的高度
		var reasonHeight = $('#txtApplicationReason')[0].scrollHeight;
		if (reasonHeight > 100) {
			$('#txtApplicationReason').css('height', reasonHeight + 20);
		}
	</script>
</body>
</html>
