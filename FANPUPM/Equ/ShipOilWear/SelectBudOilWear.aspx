<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectBudOilWear.aspx.cs" Inherits="Equ_ShipOilWear_SelectBudOilWear" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/icon.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="../Script/ShipOilWear/BudOilWearList.js"></script>
	<script type="text/javascript" src="../Script/ShipOilWear/SelectBudOilWear.js"></script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table border="0" cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;
			height: 420px; vertical-align: top;">
			<tr style="height: 1px;">
				<td style="vertical-align: top; height: 1px;">
					<table class="queryTable" cellpadding="3px" cellspacing="0px">
						<tr>
							<td class="descTd">
								编号
							</td>
							<td>
								<asp:TextBox ID="txtCode" Width="120px" runat="server"></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td style="width: 100%; height: 100%;">
					<table class="tab">
						<tr>
							<td style="height: 100%; vertical-align: top;">
								<div class="pagediv">
									<asp:GridView ID="gvEquOilWear" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvEquOilWear_RowDataBound" DataKeyNames="Id,Code,TaskId,QuotaOilWear" runat="server">
<EmptyDataTemplate>
											<table id="emptyEquOilWear" class="gvdata">
												<tr class="header">
													<th scope="col" style="width: 25px;">
														序号
													</th>
													<th scope="col">
														编号
													</th>
													<th scope="col">
														项目
													</th>
													<th scope="col">
														需求船型
													</th>
													<th scope="col">
														施工地点
													</th>
													<th scope="col">
														是否对外租赁
													</th>
													<th scope="col">
														预计开工日期
													</th>
													<th scope="col">
														预计完工日期
													</th>
													<th scope="col">
														备注
													</th>
												</tr>
											</table>
										</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
													
												</ItemTemplate></asp:TemplateField><asp:BoundField DataField="Code" HeaderText="编号" /><asp:TemplateField HeaderText="所需船型"><ItemTemplate>
													<span class="tooltip" title=''>
														
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="施工地点"><ItemTemplate>
													<span class="tooltip" title=''>
														
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="是否对外租赁" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"><ItemTemplate>
													
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预计开工日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"><ItemTemplate>
													
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预计完工日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"><ItemTemplate>
													
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
													<span class="tooltip" title=''>
														
													</span>
												</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
									<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
									</webdiyer:AspNetPager>
								</div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<div style="width: 98%; height: 25px; float: left; text-align: right; vertical-align: middle">
		<input type="button" id="btnSave" disabled="disabled" value="保存" />
		<input type="button" id="btnCancel" value="取消" />
	</div>
	
	<asp:HiddenField ID="hfldIdsChecked" runat="server" />
	<asp:HiddenField ID="hfldCode" runat="server" />
	<asp:HiddenField ID="hfldTaskCode" runat="server" />
	<asp:HiddenField ID="hfldQuotaOilWear" runat="server" />
	</form>
</body>
</html>
