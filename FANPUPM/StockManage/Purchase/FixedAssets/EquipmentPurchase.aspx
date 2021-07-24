<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipmentPurchase.aspx.cs" Inherits="Equ_Purchase_EquipmentPurchase" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>固定资产采购单</title><link rel="Stylesheet" type="text/css" href="../../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../../Script/jquery.easyui/themes/icon.css" />
<link href="../../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="../../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../../Script/jw.js"></script>
	<script type="text/javascript" src="../../../Script/jwJson.js"></script>
	<script type="text/javascript" src="../../../Script/wf.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../../UserControl/FileUpload/GetFiles.js"></script>
	<script type="text/javascript" src="../../../Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#btnSearch')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			$('#btnAdd').click(addPurchase);
			$('#btnQuery').click(queryPurchase);
			$('#btnDelete').click(deletePurchase);
			$('#btnUpdate').click(updatePurchase);
			var jwTable = new JustWinTable('gvwPurchase');
			setButton(jwTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldPurchaseChecked');
			setWfButtonState2(jwTable, 'WF_1');
			displayLookAdjuncts();      //显示附件列
			jw.tooltip();
		});

		//查看附件
		function queryAdjunct(id) {
			var path = $('#hfldAdjunctPath').val() + '/' + id;
			window.showFiles(path, 'divOpenAdjunct');
		}
		//附件显示
		function displayLookAdjuncts() {
			var tab = document.getElementById('gvwPurchase');
			if (tab.rows.length > 0) {
				for (i = 1; i < tab.rows.length; i++) {
					var id = tab.rows[i].id;
					var path = $('#hfldAdjunctPath').val() + '/' + id;
					var showCount = getFilesCount(path);
					if (showCount == 0)
						tab.rows[i].cells[7].innerText = '';
				}
			}
		}
		//读取附件数量
		function getFilesCount(path) {
			var count = 0;
			$.ajax({
				type: "GET",
				url: "/UserControl/FileUpload/GetFiles.ashx?" + new Date().getTime() + '&Path=' + path,
				async: false,
				dataType: "JSON",
				success: function (data) {
					count = data.length;
				}
			});
			return count;
		}
		//新增
		function addPurchase() {
			parent.desktop.EquipmentPurchaseEdit = window;
			var url = "/StockManage/Purchase/FixedAssets/EquipmentPurchaseEdit.aspx?Action=Add&prjId=" + document.getElementById('hfldPrjId').value;
			toolbox_oncommand(url, "新增固定资产采购单");
		}
		//查询
		function queryPurchase() {
			if (!document.getElementById('hfldPurchaseChecked')) return false;
			viewopen('/StockManage/Purchase/FixedAssets/EquipmentPurchaseView.aspx?ic=' + this.guid, 820, 500);
		}
		//删除
		function deletePurchase() {
			if (!document.getElementById('hfldPurchaseChecked')) return false;
			if (!confirm("确定要删除吗？")) {
				return false;
			}
		}
		//编辑
		function updatePurchase() {
			parent.desktop.EquipmentPurchaseEdit = window;
			var url = "/StockManage/Purchase/FixedAssets/EquipmentPurchaseEdit.aspx?Action=Update&pid=" + document.getElementById('hfldPurchaseChecked').value;
			if (!document.getElementById('hfldPurchaseChecked')) return false;
			toolbox_oncommand(url, "编辑固定资产采购单");
		}
		// 选择人员
		function selectUser() {
			jw.selectOneUser({ nameinput: 'txtPeople' });
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr>
			<td style="height: 96%; width: 100%;">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							起始时间
						</td>
						<td>
							<asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							结束时间
						</td>
						<td>
							<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							采购编号
						</td>
						<td>
							<asp:TextBox ID="txtPcode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							录入人
						</td>
						<td>
							<asp:TextBox ID="txtPeople" CssClass="easyui-validatebox select_input" data-options="validType:'validQueryChars[50]'" Style="width: 120px;" imgclick="selectUser" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							合同名称
						</td>
						<td>
							<asp:TextBox ID="txtConName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
						</td>
						<td>
							<asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClick="btnSearch_Click" runat="server" />
						</td>
					</tr>
				</table>
				<table class="tab">
					<tr>
						<td class="divFooter" style="text-align: left;">
							<input type="button" id="btnAdd" value="新增" />
							<input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
							<asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
							<input type="button" id="btnQuery" disabled="disabled" value="查看" />
							<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="141" BusiClass="001" runat="server" />
						</td>
					</tr>
					<tr>
						<td style="height: 100%;">
							<div class="">
								<asp:GridView ID="gvwPurchase" CssClass="gvdata" Width="100%" AllowPaging="true" AutoGenerateColumns="false" OnPageIndexChanging="gvwPurchase_PageIndexChanging" OnRowDataBound="gvwPurchase_RowDataBound" DataKeyNames="pcode,pid" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
												<asp:CheckBox ID="chkSelectAll" runat="server" />
											</HeaderTemplate><ItemTemplate>
												<asp:CheckBox ID="chkSelectSingle" runat="server" />
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购编号"><ItemTemplate>
												<span class="link" onclick="viewopen('EquipmentPurchaseView.aspx?ic=<%# Eval("pid") %>',820,500)">
													<%# Eval("pcode") %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:BoundField DataField="ContractName" HeaderText="采购合同" /><asp:BoundField DataField="PrjName" HeaderText="项目" Visible="false" /><asp:BoundField DataField="person" HeaderText="录入人" /><asp:BoundField DataField="intime" HeaderText="录入时间" /><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
												<%# Common2.GetState(Eval("flowstate").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px"><ItemTemplate>
												<span class="link" onclick="queryAdjunct('<%# Eval("pid") %>')">
													<img src='/images1/icon_att0b3dfa.gif' style='cursor: pointer; border-style: none' />
												</span>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="说明"><ItemTemplate>
												<span class="tooltip" title='<%# Eval("explain") %>'>
													<%# StringUtility.GetStr(Eval("explain").ToString()) %>
												</span>
											</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<div id="divOpenAdjunct" title="查看附件" style="display: none;">
		<table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
						名称
					</td><td style="width: 30%" runat="server">
						大小
					</td><td runat="server">
					</td></tr></table>
	</div>
	
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	
	<asp:HiddenField ID="hfldAdjunctPath" runat="server" />
	
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	</form>
</body>
</html>
