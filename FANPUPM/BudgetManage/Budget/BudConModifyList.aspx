<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudConModifyList.aspx.cs" Inherits="BudgetManage_Budget_BudConModifyList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link type="text/css" rel="Stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="../Script/BudModifyList.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			if ($('#hfldIsAllowModify').val() == '0') {
				$('#btnAdd').attr('disabled', 'disabled')
			} else {
				$('#btnAdd').removeAttr('disabled');
			}
			var jwBudget = new JustWinTable('gvConBudget');
			setButton(jwBudget, 'btnDel', 'btnEdit', 'btnView', 'hfldModifyId');
			replaceEmptyTable('emptyConBudget', 'gvConBudget');
			showTooltip('tooltip', 10);
			setWidthHeight();
			//新增
			$('#btnAdd').click(function () {
				openNewTab('Add');
			});
			//编辑
			$('#btnEdit').click(function () {
				openNewTab('Edit');
			});
			setWfButtonState2(jwBudget, 'WF1');
		});
		//设置各元素的长度/宽度
		function setWidthHeight() {
			$('#div_project').height($(this).height() - $('#ddlYear').height() - 3);
			$('#divBudget').height($(this).height() - $('#tr_Buttons').height() - 47);
			$('#divBudget').width($(this).width() - $('#td_Left').width() - 10);
		}
		//打开新的标签页
		function openNewTab(action) {
			var title = '新增变更信息';
			top.ui._EditConModify = window;
			// parent.desktop.EditConModify = window;
			var urlStr = '/BudgetManage/Budget/EditConModify2.aspx?' + new Date().getTime() + '&action=' + action + '&prjId=' + $('#hfldPrjId').val() + '&year=' + $('#hfldYear').val();
			if (action == 'Edit') {
				urlStr = urlStr + '&id=' + $('#hfldModifyId').val();
				title = '编辑变更信息';
			}
			toolbox_oncommand(urlStr, title);
		}
		//查看页面
		function Query() {
			var url = '/BudgetManage/Budget/BudConModifyView.aspx?' + new Date().getTime() + '&ic=' + $('#hfldModifyId').val();
			viewopen(url, 820, 500);
		}
		//标签页查看
		function QueryTab(modifyTaskId) {
			//			parent.desktop.BudModifyView = window;
			var url = '/BudgetManage/Budget/BudConModifyView.aspx?' + new Date().getTime() + '&ic=' + modifyTaskId;
			toolbox_oncommand(url, "合同预算变更查看");
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent2" class="divContent2" style="height: 100%; padding: 0px; overflow: hidden;">
		<table style="width: 100%; vertical-align: top;">
			<tr>
				<td style="width: 100%; vertical-align: top;">
					<table style="width: 100%;">
						<tr>
							
							<td style="vertical-align: top; border-left: solid 2px #CADEED;">
								<table border="0" class="tab">
									<tr id="tr_Buttons">
										<td class="divFooter" style="text-align: left" id='td_Buttons'>
											<input type="button" id="btnAdd" value="新增" runat="server" />

											<input type="button" id="btnEdit" value="编辑" disabled="disabled" />
											<asp:Button ID="btnDel" Text="删除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗？')" OnClick="btnDel_Click" runat="server" />
											<input type="button" id="btnView" value="查看" disabled="disabled" onclick="Query();" />
											<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="134" BusiClass="001" runat="server" />
										</td>
									</tr>
									<tr>
										<td style="vertical-align: top; height: 100%;">
											<div id="divBudget" class="pagediv" style="overflow:scroll;">
												<asp:GridView ID="gvConBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvConBudget_RowDataBound" DataKeyNames="ModifyId" runat="server">
<EmptyDataTemplate>
														<table id="emptyConBudget" class="tab" width="100%">
															<tr class="header">
																<td style="width: 20px">
																	<asp:CheckBox ID="chkAll" runat="server" />
																</td>
																<td style="width: 25px;">
																	序号
																</td>
																<td align="center">
																	编号
																</td>
																<td align="center">
																	变更内容
																</td>
																<td align="center">
																	变更文件编号
																</td>
																<td align="center">
																	预算成本
																</td>
																<td align="center">
																	报审价
																</td>
																<td align="center">
																	核准价
																</td>
																<td align="center">
																	核准日期
																</td>
																<td align="center">
																	备注
																</td>
																<td align="center">
																	流程状态
																</td>
																<td align="center">
																	变更日期
																</td>
															</tr>
														</table>
													</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
																<asp:CheckBox ID="cbAllBox" runat="server" />
															</HeaderTemplate><ItemTemplate>
																<asp:CheckBox ID="cbBox" runat="server" />
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
																<%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="编号"><ItemTemplate>
																<span class="tooltip link" title='<%# Eval("ModifyCode") %>' onclick="QueryTab('<%# Eval("ModifyId") %>')">
																	<%# StringUtility.GetStr(Eval("ModifyCode").ToString(), 10) %>
																</span>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更内容"><ItemTemplate>
																<span class="tooltip" title="<%# Eval("ModifyContent") %>">
																	<%# StringUtility.GetStr(Eval("ModifyContent").ToString(), 10) %>
																</span>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更文件编号"><ItemTemplate>
																<span class="tooltip" title="<%# Eval("ModifyFileCode") %>">
																	<%# StringUtility.GetStr(Eval("ModifyFileCode").ToString(), 10) %>
																</span>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预算成本" HeaderStyle-Width="80px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
																<%# GetBudAmount(Eval("ModifyId").ToString()) %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="报审价" HeaderStyle-Width="80px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
																<%# Eval("ReportAmount") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="核准价" HeaderStyle-Width="80px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
																<%# Eval("ApprovalAmount") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="核准日期" HeaderStyle-Width="70px"><ItemTemplate>
																<%# Common2.GetTime(Eval("ApprovalDate")) %>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注" HeaderStyle-Width="70px">
<ItemTemplate>
																<span class="tooltip" title="<%# Eval("Note") %>">
																	<%# StringUtility.GetStr(Eval("Note").ToString(), 10) %>
																</span>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="50px">
<ItemTemplate>
																<%# Common2.GetState(Eval("Flowstate").ToString()) %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="变更日期" HeaderStyle-Width="70px">
<ItemTemplate>
																<%# Common2.GetTime(Eval("InputDate")) %>
															</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
												<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
												</webdiyer:AspNetPager>
											</div>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hfldModifyId" runat="server" />
	
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	
	<asp:HiddenField ID="hfldYear" runat="server" />
	
	<asp:HiddenField ID="hfldIsAllowModify" Value="0" runat="server" />
	</form>
</body>
</html>
