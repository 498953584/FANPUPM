<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PettyCashList.aspx.cs" Inherits="PettyCash_PettyCashList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>备用金</title><link href="../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="../Script/jquery.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../Script/jw.js"></script>
	<script type="text/javascript" src="../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../Script/DecimalInput.js"></script>
	<script type="text/javascript" src="../Script/wf.js"></script>
	<script type="text/javascript" src="Script/PettyCashList.js"></script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table style="width: 100%; height: 100%;">
			<tr>
				<td>
					<table class="queryTable" cellpadding="3px" cellspacing="0px">
						<tr>
							<td class="descTd" style="width: 50px;">
								申请日期
							</td>
							<td>
								<asp:TextBox ID="txtStartDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
							</td>
							<td class="descTd" style="width: 50px;">
								至
							</td>
							<td>
								<asp:TextBox ID="txtEndDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
							</td>
							<td class="descTd" style="width: 50px;">
								申请金额
							</td>
							<td>
								<asp:TextBox ID="txtStartCash" CssClass="easyui-numberbox" data-options="min:0,max:9999999999,precision:3,groupSeparator:','" runat="server"></asp:TextBox>
							</td>
							<td class="descTd" style="width: 50px;">
								至
							</td>
							<td>
								<asp:TextBox ID="txtEndCash" CssClass="easyui-numberbox" data-options="min:0,max:9999999999,precision:3,groupSeparator:','" runat="server"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="descTd">
								事项
							</td>
							<td>
								<asp:TextBox ID="txtMatter" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
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
				<td id="td_buttons" class="divFooter" style="text-align: left;">
					<span id="span_buttons">
						<input type="button" id="btnAdd" value="新增" />
						<input type="button" id="btnEdit" value="编辑" disabled="disabled" />
						<asp:Button ID="btnDelete" Text="删除" Enabled="false" OnClick="btnDelete_Click" runat="server" />
						<input type="button" id="btnQuery" value="查看" disabled="disabled" />
						<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="129" BusiClass="001" runat="server" />
						<asp:Label ID="lblUserName" Visible="false" runat="server"></asp:Label>
					</span>
					<asp:Button ID="btnExportExcel" Text="导出Excel" Width="70px" OnClick="btnExportExcel_Click" runat="server" />
					<span id="span_user"></span>
				</td>
			</tr>
			<tr>
				<td>
					<asp:GridView ID="gvwPettyCash" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvwPettyCash_RowDataBound" DataKeyNames="Id" runat="server">
<EmptyDataTemplate>
							<table class="gvdata" cellspacing="0" rules="all" border="1" id="gvwPettyCashEmpty"
								style="width: 100%; border-collapse: collapse;">
								<tr class="header">
									<th scope="col" style="width: 20px;">
										<input id="chkAll" type="checkbox" />
									</th>
									<th scope="col" style="width: 25px;">
										序号
									</th>
									<th scope="col" style="width: 80px;">
										申请日期
									</th>
									<th scope="col" style="width: 200px;">
										申请金额
									</th>
									<th scope="col" style="width: 100px;">
										事项
									</th>
									<th scope="col" style="width: 80px;">
										用款日期
									</th>
									<th scope="col" style="width: 80px;">
										流程状态
									</th>
									<th scope="col">
										项目
									</th>
								</tr>
							</table>
						</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
									<asp:CheckBox ID="chkSelectAll" runat="server" />
								</HeaderTemplate><ItemTemplate>
									<asp:CheckBox ID="chkSelectSingle" runat="server" />
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
									<%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请人" HeaderStyle-Width="80px"><ItemTemplate>
									<%# GetUserName(Eval("Applicant").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请日期" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
									<span class="link">
										<%# System.Convert.ToDateTime(Eval("ApplicationDate")).ToString("yyyy-MM-dd") %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:BoundField DataField="Cash" HeaderText="申请金额" ItemStyle-Width="120px" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="Matter" HeaderText="事项" /><asp:TemplateField HeaderText="用款日期" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
									<%# System.Convert.ToDateTime(Eval("CashDate")).ToString("yyyy-MM-dd") %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
									<%# Common2.GetState(Eval("FlowState").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目"><ItemTemplate>
									<span class="tooltip" title='<%# (Eval("Project") == null) ? "" : Eval("Project.PrjName").ToString() %>'>
										<%# (Eval("Project") == null) ? "" : StringUtility.GetStr(Eval("Project.PrjName"), 20) %>
									</span>
								</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
					</webdiyer:AspNetPager>
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hfldUserName" runat="server" />
	<asp:HiddenField ID="hfldPettyCashIds" runat="server" />
	</form>
	<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
	<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
</body>
</html>
