<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutBalance.aspx.cs" Inherits="ContractManage_ContractReport_PayoutBalance" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>支出合同结算报表</title><link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
        <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript">
	    $(document).ready(function () {
	        replaceEmptyTable('emptyContract', 'gvwContract');
	        var table = new JustWinTable('gvwContract');
	        formateTable('gvwContract', 2, true);
	        addPregressBar('percent');
	        showTooltip('tooltip', 10);
	        // 添加验证
	        $('#btnSearch')[0].onclick = function () {
	            if (!$('#form1').form('validate')) {
	                return false;
                }
	        }
	    });

		// 选择项目
		function openProjPicker() {
			jw.selectOneProject({nameinput: 'txtProject' });
		}

		// 选择乙方
		function pickCorp() {
			jw.selectOneCorp({ idinput: 'hfldBName', nameinput: 'txtBName' });
		}

		// 名称信息提示
		function showMoreName() {
			var tab = document.getElementById('gvwContract');
			if (tab != null) {
				for (i = 1; i < tab.rows.length - 1; i++) {
					var cells = tab.rows[i].cells;
					if (cells.length == 1) return;
					if (cells[2].children.length == 0) return;
					var imgId = cells[2].children[1].id;
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
			jw.selectOneConType({ nameinput: 'txtConType' });
		}

		// 选择签订人
		function selectSignPerson() {
			jw.selectOneUser({ nameinput: 'txtSignPersonName' });
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
							合同类型
						</td>
						<td>
							<asp:TextBox ID="txtConType" Width="120px" CssClass="select_input easyui-validatebox" data-options="validType:'validChars[50]'" imgclick="selectConType" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							项目名称
						</td>
						<td>
							<asp:TextBox ID="txtProject" Style="width: 120px;" CssClass="select_input easyui-validatebox" data-options="validType:'validChars[50]'" imgclick="openProjPicker" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							合同编号
						</td>
						<td>
							<asp:TextBox ID="txtContractCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							合同名称
						</td>
						<td>
							<asp:TextBox ID="txtContractName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
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
						<td class="descTd">
							乙方
						</td>
						<td>
							<asp:TextBox ID="txtBName" CssClass="easyui-validatebox select_input {required:true, messages:{required:'乙方必须输入'}}" data-options="validType:'validChars[50]'" Style="width: 120px;" imgclick="pickCorp" runat="server"></asp:TextBox>
							<asp:HiddenField ID="hfldBName" runat="server" />
						</td>
						<td class="descTd" style="white-space: nowrap;">
							项目类型
						</td>
						<td>
							<asp:DropDownList ID="dropPrjKindClass" Width="100%" runat="server"></asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							签订人
						</td>
						<td>
							<asp:TextBox ID="txtSignPersonName" Width="120px" CssClass="select_input easyui-validatebox" data-options="validType:'validChars[50]'" imgclick="selectSignPerson" runat="server"></asp:TextBox>
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
			<td style="vertical-align: top;">
				<div class="pagediv" style="width: 1800px">
					<asp:GridView ID="gvwContract" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" DataKeyNames="ContractID,IsMainContract" runat="server">
<EmptyDataTemplate>
							<table id="emptyContract" class="gvdata">
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
										签订人
									</th>
									<th scope="col">
										最终金额
									</th>
									<th scope="col">
										结算累计
									</th>
									<th scope="col">
										百分比
									</th>
									<th scope="col">
										合同状态
									</th>
									<th scope="col">
										签订时间
									</th>
									<th scope="col">
										合同类型
									</th>
									<th scope="col">
										项目
									</th>
									<th scope="col">
										项目类型
									</th>
								</tr>
							</table>
						</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
									<%# StringUtility.GetStr(Eval("Num").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:BoundField DataField="ContractCode" HeaderText="合同编号" HeaderStyle-Width="100px" /><asp:TemplateField HeaderText="合同名称" HeaderStyle-Width="100px"><ItemTemplate>
									<span class="link tooltip" title='<%# Eval("ContractName").ToString() %>' onclick="viewopen('/ContractManage/PayoutContract/ParyoutContractQuery.aspx?bc=081&bcl=001&ic=<%# Eval("ContractID") %>', '支出合同查看')">
										<%# StringUtility.GetStr(Eval("ContractName").ToString(), 10) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="乙方" HeaderStyle-Width="200px"><ItemTemplate>
									<%# StringUtility.GetStr(Eval("CorpName").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="签订人" DataField="SignPersonName" /><asp:BoundField DataField="ModifiedMoney" ItemStyle-HorizontalAlign="Right" HeaderText="最终金额" ItemStyle-CssClass="decimal_input" ItemStyle-Width="80px" /><asp:BoundField DataField="BalanceTotal" ItemStyle-HorizontalAlign="Right" HeaderText="结算累计" ItemStyle-CssClass="decimal_input" ItemStyle-Width="80px" /><asp:TemplateField HeaderText="百分比" ItemStyle-CssClass="percent"><ItemTemplate>
									<%# string.Format("{0:P}", Eval("Rate")) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px"><ItemTemplate>
									<%# WebUtil.GetConState(Eval("conState").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签订时间" HeaderStyle-Width="80px"><ItemTemplate>
									<%# string.Format("{0:d}", Eval("SignDate")) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同类型" HeaderStyle-Width="200px"><ItemTemplate>
									<%# StringUtility.GetStr(Eval("TypeName").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目" HeaderStyle-Width="150px"><ItemTemplate>
									<%# StringUtility.GetStr(Eval("PrjName").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目类型" HeaderStyle-Width="200px"><ItemTemplate>
									<%# StringUtility.GetStr(Eval("CodeName").ToString()) %>
								</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
					</webdiyer:AspNetPager>
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
