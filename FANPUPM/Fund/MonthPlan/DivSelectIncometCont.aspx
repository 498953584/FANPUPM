<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivSelectIncometCont.aspx.cs" Inherits="Fund_MonthPlan_DivSelectIncometCont" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>收入合同列表</title><link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/StockManage/script/tab.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<link href="/StyleCss/Common.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript">
		addEvent(window, 'load', function () {
			// 添加验证
			$("#btnSearch")[0].onclick = function () {
				if (!$('#form1').form('validate')) {
					return false;
				}
			}
			var aa = new JustWinTable('gvConract');
			$("#btnSave").click(function () {
				trdbClick();
			});
			$("#btnCancel").click(function () {
				top.ui.closeWin();
			});
			replaceEmptyTable('emptyContractType', 'gvConract');


		});

		//TR单击事件
		function trClick(id, name,bname) {
			$("#hdId").val(id);
			$("#hdName").val(name);
			$("#hdBName").val(bname);
		}
		//TR双击事件
		function trdbClick() {
		    if (typeof top.ui.callback == 'function') {
		        top.ui.callback($("#hdId").val(), $("#hdName").val(), $("#hdBName").val());
				top.ui.callback = null;
			}
			top.ui.closeWin();
		}    
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr style="height: 20px;" class="divHeader2">
			<td class="divHeader2">
				<asp:Label ID="lblTitle" Text="合同列表" runat="server"></asp:Label>
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
						<td>
							<asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="width: 100%; vertical-align: top;">
				<table class="tab">
					<tr>
						<td style="height: 100%; vertical-align: top;">
							<div>
								<asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="ContractId,ContractName,Second" runat="server">
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
													签订日期
												</th>
												<th scope="col">
													合同金额
												</th>
												<th scope="col" style="width: 80px;">
													已结算总额
												</th>
												<th scope="col" style="width: 80px;">
													已收总额
												</th>
												<th scope="col">
													交底状态
												</th>
												<th scope="col">
													合同状态
												</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同编号"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("ContractCode").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同名称"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("ContractName").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签订日期"><ItemTemplate>
												<%# Common2.GetTime(Eval("SignedTime").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同金额" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
												<%# WebUtil.GetEnPrice(Eval("ContractPrice").ToString(), Eval("ContractId").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="已结算总额" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="已收总额" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="交底状态"><ItemTemplate>
												<%# GetFeedbackState(Eval("contractId").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同状态"><ItemTemplate>
												<%# (Eval("sign").ToString() == "0") ? "未签订" : "已签订" %>
											</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<div class="divFooter2" style="background-image: url();">
		<table class="tableFooter2">
			<tr>
				<td>
					<input type="button" id="btnSave" value="确定" runat="server" />

					<input type="button" id="btnCancel" value="取消" />
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hdId" runat="server" />
	<asp:HiddenField ID="hdName" runat="server" />
	<asp:HiddenField ID="hdBName" runat="server" />
	</form>
</body>
</html>
