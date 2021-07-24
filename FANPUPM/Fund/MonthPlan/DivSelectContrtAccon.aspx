<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivSelectContrtAccon.aspx.cs" Inherits="Fund_MonthPlan_DivSelectContrtAccon" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>选择支出合同</title>
	<style type="text/css">
		#spanContractType
		{
			width: 120px;
		}
	</style>
	<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript">
		function clickRow(theCode, theName,theBName) {
			$("#hdName").val(theName);
			$("#hdCode").val(theCode);
			$("#hdBName").val(theBName);
			
		}
		function dbClickRow(theCode, theName, theBName) {
			//var method = getRequestParam('Method'); //方法
			//parent[method](theCode, theName);
			if (typeof top.ui.callback == 'function') {
			    top.ui.callback(theCode, theName, theBName);
				top.ui.callback = null;
			}
			top.ui.closeWin();
			// divClose(parent);
		}
		function rowQueryA(path) {
			parent.parent.desktop.PayoutContractEdit = window;
			toolbox_oncommand(path, "查看收入合同");
		}
		$(function () {
			// 添加验证
			$("#btnSearch")[0].onclick = function () {
				if (!$('#form1').form('validate')) {
					return false;
				}
			}
			replaceEmptyTable('emptyContractType', 'gvwContract');
		});
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="780px">
		<tr id="header">
			<td>
				支出合同
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top;">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							合同编号
						</td>
						<td>
							<asp:TextBox ID="txtContractCode" Width="120px" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[50]'" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							合同名称
						</td>
						<td>
							<asp:TextBox ID="txtContractName" Width="120px" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[50]'" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
						</td>
						<td style="display: none">
							<asp:HiddenField ID="hdnProjectCode" runat="server" />
						</td>
						<td>
							<asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
						</td>
						<td>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top;">
				<table class="tab" style="vertical-align: top;">
					<tr>
						<td>
							
							<div style=" width:100%;height:100%;border:solid 0px red;margin:0px;padding:0px;">
								<asp:GridView ID="gvwContract" CssClass="gvdata" AllowPaging="true" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" OnPageIndexChanging="gvwContract_PageIndexChanging" DataKeyNames="ContractID" runat="server">
<EmptyDataTemplate>
										<table id="emptyContractType" class="gvdata">
											<tr class="header">
												<th scope="col" style="width: 25px;">
													序号
												</th>
												<th scope="col">
													合同编号
												</th>
												<th scope="col">
													合同名称
												</th>
												<th scope="col">
													乙方
												</th>
												<th scope="col">
													合同总额
												</th>
												<th scope="col">
													已结算总额
												</th>
												<th scope="col">
													已付总额
												</th>
												<th scope="col">
													合同类型
												</th>
												<th scope="col">
													签约时间
												</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate></asp:TemplateField><asp:BoundField DataField="ContractCode" HeaderText="合同编号" /><asp:TemplateField HeaderText="合同名称"><ItemTemplate>
												
												<%# Eval("ContractName") %>
												
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同总额" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
												<%# Eval("ModifiedMoney") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="已结算总额" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
												<%# Eval("BalanceMoney") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="已付总额" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
												<%# Eval("PaymentMoney") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同类型"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("TypeName").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签约时间" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
												<%# Common2.GetTime(Eval("SignDate").ToString()) %>
											</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<div id="divFramRole" title="" style="display: none">
		<iframe id="iframeRole" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<asp:HiddenField ID="hfldContract" runat="server" />
	<asp:HiddenField ID="hdName" runat="server" />
	<asp:HiddenField ID="hdCode" runat="server" />
    <asp:HiddenField ID="hdBName" runat="server" />
	</form>
</body>
</html>
