<%@ Page Language="C#" AutoEventWireup="true" CodeFile="showDetailMonthPlan.aspx.cs" Inherits="Fund_MonthPlan_showDetailMonthPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/UserControl/FileUpload/GetFiles.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<script language="javascript" type="text/javascript">
		$(function () {
			replaceEmptyTable('emptyMonthPlanType', 'gvMonthPlanList');
			var jwTable = new JustWinTable('gvMonthPlanList');
			//handleTD();
		});
		function handleTD() {
			$("#gvMonthPlanList tr").each(function (i) {
				var _leg = $(this).find("td").last().innerWidth();
				var _strValue = $(this).find("td").last().html();
				if (_strValue != null) {
					if (_strValue.toString().length > parseInt(_leg)) {
						_strValue = _strValue.substr(0, _leg - 10) + "......";
						$(this).find("td").last().html(_strValue);
					}
				}
			});
		}
		//查看附件
		function queryAdjunct(id) {
			var path = $('#hfldAdjunctPath').val() + id;
			window.parent.showFiles(path, 'divOpenAdjunct');
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divOpenAdjunct" title="查看附件" style="display: none;">
		<table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
						名称
					</td><td style="width: 30%" runat="server">
						大小
					</td><td runat="server">
					</td></tr></table>
	</div>
	<asp:Literal ID="Literal1" runat="server"></asp:Literal>
	<div id="showGV" runat="server">
		<asp:GridView ID="gvMonthPlanList" CssClass="gvdata" AutoGenerateColumns="false" AllowPaging="true" OnRowDataBound="gvMonthPlanList_RowDataBound" OnPageIndexChanging="gvMonthPlanList_PageIndexChanging" DataKeyNames="UID" runat="server">
<EmptyDataTemplate>
				<table class="gvdata" cellspacing="0" rules="all" border="1" id="emptyMonthPlanType"
					style="border-collapse: collapse;">
					<tr class="header">
						<th scope="col" style="width: 20px;">
							序号
						</th>
						<th scope="col" style="width: 100px;">
							合同
						</th>
						<th scope="col" style="width: 80px;">
							上期计划金额
						</th>
						<th scope="col" style="width: 80px;">
							上期实际发生金额<img title="上期通过审核完成的金额" style="cursor: pointer;" src="/images/help.jpg" />
						</th>
						<th scope="col" style="width: 80px;">
							上期计划执行完成情况<img title="上期实际发生金额/上期计划金额" style="cursor: pointer;" src="/images/help.jpg" />
						</th>
						<th scope="col" style="width: 80px;">
							本期计划金额
						</th>
                        <th scope="col" style="width: 50px;">
							乙方
						</th>
						<th scope="col" style="width: 30px;">
							附件
						</th>
						<th scope="col">
							备注
						</th>
                        
					</tr>
				</table>
			</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-HorizontalAlign="Center">
<ItemTemplate>
						<%# Container.DataItemIndex + 1 %>
					</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合同" ItemStyle-Width="100px"><ItemTemplate>
						<%# Eval("ContractName") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="上期计划金额" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
						<%# DataBinder.Eval(Container.DataItem, "Shangqi", "{0:F3}").Replace(",","") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="上期实际发生金额" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
						<%# DataBinder.Eval(Container.DataItem, "shijifasheng", "{0:F3}").Replace(",","") %>
					</ItemTemplate><HeaderTemplate>
						上期实际发生金额"<img title="上期通过审核完成的金额" style="cursor: pointer;" src="/images/help.jpg" />
					</HeaderTemplate></asp:TemplateField><asp:TemplateField HeaderText="上期计划执行完成情况" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
						<%# Math.Round(Convert.ToDecimal(Eval("BL").ToString()), 3) %>
					</ItemTemplate><HeaderTemplate>
						上期计划执行完成情况<img title="上期实际发生金额/上期计划金额" style="cursor: pointer;" src="/images/help.jpg" />
					</HeaderTemplate></asp:TemplateField><asp:TemplateField HeaderText="本期计划金额" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
						<%# Eval("PlanMoney") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="乙方" ItemStyle-Width="50px"><ItemTemplate>
						<%# Eval("corpName") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
						<div style="overflow: hidden; width: 310px;">
							<%# DataBinder.Eval(Container.DataItem, "ReMark") %>
						</div>
					</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
	</div>
	<asp:HiddenField ID="hfldAdjunctPath" runat="server" />
	</form>
</body>
</html>
