<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceLedger.aspx.cs" Inherits="ContractManage_PayoutInvoice_InvoiceLedger" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>发票台帐</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			// 添加验证
			$("#btnSearch")[0].onclick = function () {
				if (!$('#form1').form('validate')) {
					return false;
				}
			}
			replaceEmptyTable('emptyContract', 'gvwContract');
			var table = new JustWinTable('gvwContract');
			showMoreName();
			formateTable('gvwContract', 3, true);
			addPregressBar('percent');

		});

		//选择项目
		function openProjPicker() {
			jw.selectOneProject({ idinput: 'hdnProjectCode', nameinput: 'txtProject' });
		}

		function pickCorp() {
			jw.selectOneCorp({ idinput: 'hfldBName', nameinput: 'txtBName' });
		}
		//名称信息提示
		function showMoreName() {
			var tab = document.getElementById('gvwContract');
			if (tab != null) {
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
		}

		// 选择合同类型
		function selectConType() {
			jw.selectOneConType({ nameinput: 'txtConType', idinput: 'hfldConType' });
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
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
							<asp:TextBox ID="txtStartDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							至
						</td>
						<td>
							<asp:TextBox ID="txtEndDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							合同类型
						</td>
						<td>
							<span class="spanSelect" style="width: 122px">
								<asp:TextBox ID="txtConType" Style="width: 97px; height: 15px; border: none;
									line-height: 16px; margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[50]'" runat="server"></asp:TextBox>
								<img alt="选择类型" onclick="selectConType();" src="../../images/icon.bmp" style="float: right;" />
							</span>
						</td>
						<td class="descTd">
							项目名称
						</td>
						<td>
							<span class="spanSelect" style="width: 122px">
								<asp:TextBox ID="txtProject" Style="width: 97px; height: 15px; border: none;
									line-height: 16px; margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[50]'" runat="server"></asp:TextBox>
								<img alt="项目名称" onclick="openProjPicker();" src="../../images/icon.bmp" style="float: right;" />
							</span>
							<input id="hdnProjectCode" type="hidden" name="hdnProjectCode" runat="server" />

						</td>
						<td class="descTd">
							乙方
						</td>
						<td>
							<span class="spanSelect" style="width: 122px">
								<asp:TextBox ID="txtBName" Style="width: 97px; height: 15px; border: none;
									line-height: 16px; margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[50]'" runat="server"></asp:TextBox>
								<img alt="乙方" onclick="pickCorp();" src="../../images/icon.bmp" style="float: right;" />
							</span>
							<asp:HiddenField ID="hfldBName" runat="server" />
						</td>
						<td class="descTd">
						</td>
						<td>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="divFooter" style="text-align: left;">
				<asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClick="btnSearch_Click" runat="server" />
				<asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top; height: 82%">
				<div id="report" style="width: 1800px;">
					<asp:GridView ID="gvwContract" CssClass="gvdata" AllowPaging="true" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" OnPageIndexChanging="gvwContract_PageIndexChanging" DataKeyNames="ContractID,IsMainContract" runat="server">
<EmptyDataTemplate>
							<table id="emptyContract" class="gvdata">
								<tr class="header">
									<th scope="col" style="width: 25px;">
										序号
									</th>
									<th scope="col">
										项目名称
									</th>
									<th scope="col">
										合同编号
									</th>
									<th scope="col">
										合同名称
									</th>
									<th scope="col">
										合同类型
									</th>
									<th scope="col">
										签约日期
									</th>
									<th scope="col">
										付款方式
									</th>
									<th scope="col">
										总合同金额
									</th>
									<th scope="col">
										累计付款金额
									</th>
									<th scope="col">
										累计已到发票额
									</th>
									<th scope="col">
										累计未到发票额
									</th>
									<th scope="col">
										已开发票付款比率
									</th>
									<th scope="col">
										乙方
									</th>
									<th scope="col">
										备注
									</th>
								</tr>
							</table>
						</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
									<%# StringUtility.GetStr(Eval("PrjName").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:BoundField DataField="ContractCode" HeaderText="合同编号" HeaderStyle-Width="100px" /><asp:TemplateField HeaderText="合同名称" HeaderStyle-Width="150px"><ItemTemplate>
									<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" ToolTip='<%# Convert.ToString(Eval("ContractName").ToString()) %>' runat="server"><%# StringUtility.GetStr(Eval("ContractName").ToString(), 25) %></asp:HyperLink>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同类型" HeaderStyle-Width="120px"><ItemTemplate>
									<%# StringUtility.GetStr(Eval("TypeName").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签约日期" HeaderStyle-Width="100px"><ItemTemplate>
									<%# string.Format("{0:d}", Eval("SignDate")) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="付款方式"><ItemTemplate>
									<%# StringUtility.GetStr(Eval("CodeName").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:BoundField DataField="ModifiedMoney" ItemStyle-HorizontalAlign="Right" HeaderText="总合同金额" ItemStyle-CssClass="decimal_input" ItemStyle-Width="80px" /><asp:BoundField DataField="PaymentTotal" ItemStyle-HorizontalAlign="Right" HeaderText="累计付款金额" ItemStyle-CssClass="decimal_input" ItemStyle-Width="80px" /><asp:BoundField DataField="InvoiceTotal" ItemStyle-HorizontalAlign="Right" HeaderText="累计已到发票额" ItemStyle-CssClass="decimal_input" ItemStyle-Width="80px" /><asp:BoundField DataField="InvoiceDiff" ItemStyle-HorizontalAlign="Right" HeaderText="累计未到发票额(应付账款)" ItemStyle-CssClass="decimal_input" ItemStyle-Width="80px" /><asp:TemplateField HeaderText="已开发票付款比率" ItemStyle-CssClass="percent"><ItemTemplate>
									<%# string.Format("{0:P2}", Eval("InvoiceRate")) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="乙方" HeaderStyle-Width="120px"><ItemTemplate>
									<%# StringUtility.GetStr(Eval("CorpName").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注" HeaderStyle-Width="120px"><ItemTemplate>
									<%# StringUtility.GetStr(Eval("Notes").ToString()) %>
								</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</div>
			</td>
		</tr>
	</table>
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	</form>
</body>
</html>
