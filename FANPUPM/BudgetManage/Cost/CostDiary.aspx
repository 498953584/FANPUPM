<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostDiary.aspx.cs" Inherits="BudgetManage_Cost_CostDiary" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>间接成本日记账-费用录入</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="../../UserControl/FileUpload/GetFiles.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/ecmascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript">
		$(document).ready(function () {
			setWidthHeight();
			var jwTable = new JustWinTable('gvBudget');

			setButton(jwTable, 'btnDel', 'btnEdit', 'btnLook', 'hfldPurchaseChecked');
			showDetails();
			setWfButtonState2(jwTable, 'WF_1');
			showTooltip('tooltip', 20);

			//添加点击行事件
			$('#gvBudget tr').live('click', function () {
				showDetails();
			});
		});
		function addCostDiary(action) {
			top.ui._CostDiaryEdit = window;
			var id = document.getElementById('hfldPurchaseChecked').value;
			var year = document.getElementById('hfldYear').value;
			var prjId = document.getElementById('hfldPrjId').value;
			var url = '/BudgetManage/Cost/CostDiaryEdit.aspx?year=' + year + '&prjId=' + prjId;
			var type = 'P';
			var typeCost = '间接成本';
			if (year == 'zzjg') {
				type = 'O'
				typeCost = '组织机构';
			}
			var titleText = '新增' + typeCost + '预报销';
			if (action == 'edit') {
				titleText = '编辑' + typeCost + '预报销';
				url = url + '&id=' + id;
			}
			url += '&costType=' + type;
			toolbox_oncommand(url, titleText);
		}
		function setWidthHeight() {
			$('#divBudget').height($(this).height() - 25);
			$('#divDetails').height(180);
			$('#divDiaries').height($('#divBudget').height() - $('#divDetails').height());
			$('#div_project').height($(this).height() - 20);
		}
		//点击行绑定日记明细
		function showDetails() {
			var src = 'IFCostDetails.aspx?id=' + $('#hfldPurchaseChecked').val() + '&year=' + $('#hfldYear').val();
			$('#ifDetails').attr('src', src);
		}
		//查看日记明细
		function view(id) {
			if (id == undefined) {
				var id = document.getElementById('hfldPurchaseChecked').value;
			}
			var url = '/BudgetManage/Cost/CostVerifyRecord.aspx?ic=' + id; 
			viewopen(url);
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
		<table style="width: 100%; height: 100%;">
			<tr>
				<td style="width: 100%; height: 100%;">
					<table style="width: 100%; height: 100%;">
						<tr>
							<td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
								<table class="tab">
									<tr>
										<td class="divFooter" style="text-align: left;">
											<input type="button" value="新 增" id="btnAdd" onclick="addCostDiary('add')" runat="server" />

											<input type="button" value="编 辑" id="btnEdit" onclick="addCostDiary('edit')" disabled="disabled" />
											<asp:Button ID="btnDel" Text="删 除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗??');" OnClick="btnDel_Click" runat="server" />
											<input type="button" value="查 看" id="btnLook" disabled="disabled" onclick="view()" />
											<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="087" BusiClass="001" runat="server" />
										</td>
									</tr>
									<tr>
										<td style="vertical-align: top; height: 100%;">
											<div id="divBudget" style="overflow: hidden;">
												<div id="divDiaries" style="overflow: auto;">
													<asp:GridView ID="gvBudget" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="InDiaryId" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
																	<asp:CheckBox ID="cbBoxAll" runat="server" />
																</HeaderTemplate>

<ItemTemplate>
																	<asp:CheckBox ID="cbBox" runat="server" />
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
																	<%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="费用名称" HeaderStyle-Width="150px"><ItemTemplate>
																	<span class="link tooltip" title='<%# Eval("Name").ToString() %>' onclick="view('<%# Eval("InDiaryId").ToString() %>')">
																		<%# StringUtility.GetStr(Eval("Name").ToString(), 20) %>
																	</span>
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发生单位" HeaderStyle-Width="150px"><ItemTemplate>
																	<span class="tooltip" title='<%# Eval("Department").ToString() %>'>
																		<%# StringUtility.GetStr(Eval("Department").ToString(), 20) %>
																	</span>
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发生时间"><ItemTemplate>
																	<%# Common2.GetTime(Eval("InputDate2")) %>
																</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="填报人" DataField="InputUser" /><asp:BoundField HeaderText="经手人" DataField="IssuedBy" /><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
																	<%# Common2.GetState(Eval("FlowState").ToString()) %>
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合计" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
																	<%# GetSumCost(Eval("InDiaryId").ToString()).ToString("#,#0.000") %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="实际合计金额"><ItemTemplate>
																	<%# WebUtil.SapRealCostDiary(Eval("IndireCode"), Eval("FlowState").ToString()) %>
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="所属项目"><ItemTemplate>
																	<span class="tooltip" title='<%# this.hfldPrjName.Value %>'>
																		<%# StringUtility.GetStr(this.hfldPrjName.Value, 20) %>
																	</span>
																</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
													<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
													</webdiyer:AspNetPager>
												</div>
												<div id="divDetails" style="overflow: auto; border-top: solid 2px #CADEED;">
													<iframe id="ifDetails" style="width: 100%; height: 98%;" frameborder="none" border="0">
													</iframe>
												</div>
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
	<div id="divOpenAdjunct" title="查看附件" style="display: none;">
		<table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
						名称
					</td><td style="width: 30%" runat="server">
						大小
					</td><td runat="server">
					</td></tr></table>
	</div>
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	<asp:HiddenField ID="hfldYear" runat="server" />
	<asp:HiddenField ID="hfldPrjName" runat="server" />
	</form>
</body>
</html>
