<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjBasicSummary.aspx.cs" Inherits="PrjManager_PrjBasicSummary" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>工程基础汇总表</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="../Script/wf.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			var aa = new JustWinTable('gvProject');
			replaceEmptyTable('emptyTable', 'gvProject');
			showTooltip('tooltip', 25);
			formateTable('gvProject', 1);
		});

		//选择人员
		function selectUser(id, name) {
			jw.selectOneUser({ codeinput: id, nameinput: name });
		}


		function viewopen_n(id) {
			viewopen('/PrjManager/PrjInfoView.aspx?ic=' + id, '项目信息查看');
		}

		//甲方名称
		function pickCorp(param) {
			jw.selectOneCorp({ idinput: 'hfldOwner', nameinput: 'txtOwnerName' });
		}
        
	</script>
	<style type="text/css">
		.padding
		{
			margin-left: 3px;
			margin-right: 3px;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr>
			<td>
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							工程编号
						</td>
						<td>
							<asp:TextBox ID="txtCode" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							工程名称
						</td>
						<td>
							<asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							工程地点
						</td>
						<td>
							<input type="text" id="txtPrjPlace" style="height: 15px; width: 120px;
								margin: 1px 0px 1px 2px;" runat="server" />

						</td>
						<td class="descTd">
							项目责任人
						</td>
						<td>
							<span class="spanSelect" style="width: 122px;">
								<input type="text" id="txtPrjDutyPerson" style="height: 15px; width: 97px;
									margin: 1px 0px 1px 2px; border: none;" runat="server" />

								<img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img3" onclick="selectUser('hfldPrjDutyPerson','txtPrjDutyPerson');" />
							</span>
							<asp:HiddenField ID="hfldPrjDutyPerson" runat="server" />
						</td>
					</tr>
					<tr>
						<td class="descTd">
							项目现场负责人
						</td>
						<td>
							<span class="spanSelect" style="width: 122px;">
								<input type="text" id="txtManager" style="height: 15px; width: 97px;
									margin: 1px 0px 1px 2px; border: none;" runat="server" />

								<img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img4" onclick="selectUser('hfldManager','txtManager');" />
							</span>
							<asp:HiddenField ID="hfldManager" runat="server" />
						</td>
						<td class="descTd">
							签订日期
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
				</table>
			</td>
		</tr>
		<tr>
			<td>
				<table border="0" class="tab">
					<tr>
						<td class="divFooter" style="text-align: left">
							<asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
							<asp:Button ID="btnExecl" CssClass="button-normal" Text="导出Excel" Width="80px" OnClick="btnExecl_Click" runat="server" />
						</td>
					</tr>
					<tr>
						<td style="width: 100%;" valign="top">
							<div>
								<asp:GridView ID="gvProject" AutoGenerateColumns="false" CssClass="gvdata" ShowFooter="false" OnRowDataBound="gvProject_RowDataBound" DataKeyNames="PrjGuid,TypeCode" runat="server">
<EmptyDataTemplate>
										
									</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
												<%# Eval("Num") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程名称" HeaderStyle-Width="50px">
<ItemTemplate>
												<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("PrjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
                                                    <span class="link" onclick="viewopen_n('<%# Eval("PrjGuid") %>');">
                                                  <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                                                </span>
												</asp:HyperLink>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="工程编号" HeaderStyle-Width="50px"><ItemTemplate>
												<%# Eval("PrjCode") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程类别" HeaderStyle-Width="80px"><ItemTemplate>
												<%# Eval("PrjType") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="所属子公司" HeaderStyle-Width="150px"><ItemTemplate>
												<%# Eval("ProjPeopleDep") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目责任人" HeaderStyle-Width="80px"><ItemTemplate>
												<%# Eval("PrjDutyName") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目现场负责人" HeaderStyle-Width="80px"><ItemTemplate>
												<%# Eval("PrjMangerName") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="联系电话" HeaderStyle-Width="50px"><ItemTemplate>
												<%# Eval("Telephone") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="业主名称" HeaderStyle-Width="50px"><ItemTemplate>
												<%# Eval("CorpName") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程地点" HeaderStyle-Width="50px"><ItemTemplate>
												<%# Eval("PrjPlace") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="甲方资金来源" HeaderStyle-Width="50px"><ItemTemplate>
												<%# Eval("PrjFundInfo") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同造价" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
												<%# Eval("ContractPrice") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="目标成本价" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
												<%# Eval("BudTotal") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="实际价" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
												<%# Eval("ConsTotal") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同签订日期" HeaderStyle-Width="50px"><ItemTemplate>
												<%# WebUtil.ConvertDateTime(Eval("SignedTime")) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同开工日期" HeaderStyle-Width="50px"><ItemTemplate>
												<%# WebUtil.ConvertDateTime(Eval("StartDate")) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同竣工日期" HeaderStyle-Width="50px"><ItemTemplate>
												<%# WebUtil.ConvertDateTime(Eval("EndDate")) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="实际开工日期" HeaderStyle-Width="50px"><ItemTemplate>
												<%# WebUtil.ConvertDateTime(Eval("ActualRunDate")) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="实际竣工验收日期" HeaderStyle-Width="50px"><ItemTemplate>
												<%# WebUtil.ConvertDateTime(Eval("CompletedDate")) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同养护期" HeaderStyle-Width="50px"><ItemTemplate>
												<%# Eval("QualityPeriod") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同约定付款方式" HeaderStyle-Width="50px"><ItemTemplate>
												<%# Eval("PayMode") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="实际完成工程额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
												<%# System.Convert.ToDecimal(Eval("ConsResTotal")).ToString("0.000") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="按合同应收款" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
												<%# Eval("ClearingPrice") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="按合同应收未收" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
												<%# Eval("SurplusClearingPrice") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="当期回款" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
												<%# Eval("CurrentCllectionPrice") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="累计回款" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
												<%# Eval("CllectionPrice") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="当期支出" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
												<%# Eval("CurrentPaymentMoney") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="累计支出" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
												<%# Eval("PaymentMoney") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="管理费" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
												<%# Eval("ManagementMargin") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="民工质量保证金" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
												<%# Eval("MigrantQualityMarginRate") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预扣税" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
												<%# Eval("WithholdingTaxRate") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="履约保证金" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
												<%# Eval("PerformanceBond") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="其它" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
												<%# Eval("ElseMargin") %>
											</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
	</webdiyer:AspNetPager>
	<asp:HiddenField ID="hdReturnVal" runat="server" />
	<div id="divFramPerson" title="选择人员" style="display: none">
		<iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<asp:HiddenField ID="hfldSummarizingInfo" runat="server" />
	</form>
</body>
</html>
