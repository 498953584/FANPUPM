<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceLedger.aspx.cs" Inherits="ContractManage_IncometInvoice_InvoiceLedger" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>发票台账</title><link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />

	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript">
	    addEvent(window, "load", function () {
	        // 添加验证
	        $("#btn_Search")[0].onclick = function () {
	            if (!$('#form2').form('validate')) {
	                return false;
	            }
	        }
			var tab = new JustWinTable('gvConract');
			addPregressBar('percent');
			showMoreName();
			formateTable('gvConract', 3, true);
		});

		//往来单位
		function pickCorp() {
			jw.selectOneCorp({ idinput: 'hdParty', nameinput: 'txtParty' });
		}
		//选择项目
		function openProjPicker() {
			jw.selectOneProject({ idinput: 'hdPrjCode', nameinput: 'txtPrjName' });
		}

		//名称信息提示
		function showMoreName() {
			var tab = document.getElementById('gvConract');
			if (tab == null) return false;

			for (i = 1; i < tab.rows.length - 1; i++) {
				var cells = tab.rows[i].cells;
				if (cells.length == 1) return;
				if (cells[3].children.length == 0) return;
				var imgId = cells[3].children[0].id;
				var altLength = document.getElementById(imgId).title.length;
				if (altLength > 25) {
					$('#' + imgId).css('display', '');
					$('#' + imgId).tooltip({
						track: true,
						delay: 0,
						showURL: false,
						fade: true,
						showBody: " - ",
						extraClass: "solid 1px blue",
						fixPNG: true,
						left: -200
					});
				} else {
					document.getElementById(imgId).title = '';
				}
			}

		}

		// 选择合同类型
		function selectConType() {
			jw.selectOneConType({ nameinput: 'txtConType', idinput: 'hfldConType' });
		}
	</script>
</head>
<body>
	<form id="form2" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr>
			<td>
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
							签约时间
						</td>
						<td>
							<asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							至
						</td>
						<td>
							<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							合同类型
						</td>
						<td>
							<asp:TextBox ID="txtConType" Width="120px" CssClass="select_input easyui-validatebox" data-options="required:false, validType:'validChars[50]'" imgclick="selectConType" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							项目名称
						</td>
						<td>
							<input type="text" style="width: 120px;" id="txtPrjName" class="select_input easyui-validatebox" data-options="required:false, validType:'validChars[50]'" imgclick="openProjPicker" runat="server" />

							<asp:HiddenField ID="hdPrjCode" runat="server" />
						</td>
						<td class="descTd">
							甲 方
						</td>
						<td>
							<input type="text" readonly="readonly" title="双击选择甲方" style="width: 120px;" id="txtParty" class="select_input easyui-validatebox" imgclick="pickCorp" data-options="required:false, validType:'validChars[50]'" runat="server" />

							<asp:HiddenField ID="hdParty" runat="server" />
						</td>
						<td>
						</td>
						<td>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td>
				<table border="0" class="tab">
					<tr>
						<td class="divFooter" style="text-align: left">
							<asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
							<asp:Button ID="btnexecl" CssClass="button-normal" Text="导出Excel" Width="80px" OnClick="btnexecl_Click" runat="server" />
						</td>
					</tr>
					<tr>
						<td style="width: 100%;" valign="top">
							<div style="width: 1800px;">
								<asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="ContractId,isFContract" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
												<asp:Label ID="Label1" ToolTip='<%# Convert.ToString(Eval("prjName").ToString()) %>' Text='<%# Convert.ToString(StringUtility.GetStr(Eval("prjName").ToString())) %>' runat="server"></asp:Label>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同编号"><ItemTemplate>
												<%# Eval("ContractCode") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同名称" HeaderStyle-Width="25px"><ItemTemplate>
												
												<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" ToolTip='<%# Convert.ToString(Eval("ContractName").ToString()) %>' runat="server"><%# StringUtility.GetStr(Eval("ContractName").ToString(), 25) %></asp:HyperLink>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同类型"><ItemTemplate>
												<%# Eval("TypeName") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签约日期"><ItemTemplate>
												<%# Common2.GetTime(Eval("SignedTime").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="付款方式"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("CodeName").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="总合同金额" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
												<%# Eval("EndPrice") %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="累计收款金额" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
												<%# WebUtil.GetPaymentSum(Eval("ContractId").ToString(), "Con_Incomet_Payment", "CllectionPrice") %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="累计已开发票额" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
												<%# Eval("InvoicePrice") %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="累计未开发票额(应收账款)" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
												<%# GetBlInvoice(Eval("ContractId").ToString()) %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="已开发票收款比率" ItemStyle-CssClass="percent"><ItemTemplate>
												<%# WebUtil.GetInvoiceContrast(WebUtil.GetPaymentSum(Eval("ContractID").ToString(), "Con_Incomet_Payment", "CllectionPrice"), Eval("InvoicePrice").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="甲方" HeaderStyle-Width="120px"><ItemTemplate>
												<asp:Label ID="Label1" ToolTip='<%# Convert.ToString(base.GetParty(Eval("Party").ToString())) %>' Text='<%# Convert.ToString(StringUtility.GetStr(base.GetParty(Eval("Party").ToString()))) %>' runat="server"></asp:Label>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注" HeaderStyle-Width="120px"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("Notes").ToString()) %>
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
	</form>
</body>
</html>
