<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Purchaseplan.aspx.cs" Inherits="StockManage_Report_Purchaseplan" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>采购计划报表</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#btn_Search')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			var aa = new JustWinTable('gvPurchaseplan');
			showTooltip('tooltip', 10);
		});

		//选择项目
		function openProjPicker() {
			jw.selectOneProject({ nameinput: 'txtProjectName' });
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
							计划编号
						</td>
						<td>
							<asp:TextBox ID="txtCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							资源类别
						</td>
						<td>
							<asp:TextBox ID="txtCategory" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
						</td>
                        <td class="descTd">
							资源名称
						</td>
						<td>
							<asp:TextBox ID="txtResourceNames" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							资源编号
						</td>
						<td>
							<asp:TextBox ID="txtResourceCodes" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							项目名称
						</td>
						<td>
							<input type="text" style="width: 120px;" id="txtProjectName" class="easyui-validatebox select_input" data-options="validType:'validQueryChars[50]'" imgclick="openProjPicker" runat="server" />

						</td>
                        <td class="descTd">
                            规格
						</td>
						<td>
                            <asp:TextBox ID="txtSpecification" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
                            品牌
						</td>
						<td>
                            <asp:TextBox ID="txtBrand" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
                            型号
						</td>
						<td>
                            <asp:TextBox ID="txtModelNumber" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
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
						<td style="vertical-align: top;">
							<div class="">
								<asp:GridView ID="gvPurchaseplan" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" DataKeyNames="ppcode" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源编号"><ItemTemplate>
												<%# Eval("ResourceCode") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划编号"><ItemTemplate>
												<span class="al" onclick="viewopen('/StockManage/SmPurchaseplan/ViewSmPurchaseplan.aspx?ic=<%# Eval("ppid") %>')">
													<%# Eval("ppcode") %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源名称"><ItemTemplate>
												<span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
													<%# StringUtility.GetStr(System.Convert.ToString(Eval("ResourceName")), 10) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源类别"><ItemTemplate>
												<%# StringUtility.GetStr(System.Convert.ToString(Eval("ResourceTypeName"))) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格"><ItemTemplate>
												<span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
													<%# StringUtility.GetStr(System.Convert.ToString(Eval("Specification")), 10) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="品牌"><ItemTemplate>
												<%# Eval("Brand") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="型号"><ItemTemplate>
												<%# Eval("ModelNumber") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="技术参数"><ItemTemplate>
												<%# Eval("TechnicalParameter") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
												<%# Eval("number") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目编号"><ItemTemplate>
												<%# Eval("prjCode") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
												<span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
													<%# StringUtility.GetStr(Eval("prjName").ToString(), 10) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入日期"><ItemTemplate>
												<%# Common2.GetTime(Eval("intime").ToString()) %>
											</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	</form>
</body>
</html>
