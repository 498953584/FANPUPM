<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FundPayPlanALL.aspx.cs" Inherits="Fund_MonthPlan_FundPayPlanALL" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_querydatectrl_ascx" Src="~/UserControl/QueryDateCtrl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>支出合同结算报表</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script src="../../Web_Client/TreeNew.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var ReturnTable = new JustWinTable('gvPayoutPlan');
			var ReturnTable1 = new JustWinTable('GridView1');
		});

		// 选择合同
		function selectContract() {
			document.getElementById("ifrmSelectContract").src = "/ContractManage/UserControl/PayoutContract.aspx?ContractId=hfldContract&ContractName=txtContract";
			selectnEvent('divSelectContract');

		}

		// 选择项目
		function openProjPicker() {
			jw.selectOneProject({nameinput: 'txtProject' });
		}
		//过滤html字符
		function stripScript(id) {
			str = $("#" + id).val();
			str = str.replace(/<\/?[^>]*>/g, ''); //去除HTML tag
			str = str.replace(/[ | ]*\n/g, '\n'); //去除行尾空白
			str = str.replace(/[<>\/\,.?@#%&*!~$\^:]/g, '');
			str = str.replace(/\'/g, '');
			str = str.replace(/["\^{}+();:``_|\-\=\\\[\]]/g, '');
			//过滤中文字符
			str = str.replace(/[\《\》：“|｛｝+——（*&……%￥#@！~？“……）”【】、‘；。，、’·%]/g, ''); 
			$("#" + id).val(str);
		}
	</script>
	<style type="text/css">
		.style2
		{
			height: 18px;
		}
		.style3
		{
			width: 100%;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: auto;">
		<table border="0" cellpadding="0" cellspacing="0" width="100%">
			<tr id="header">
				<td>
					<asp:Label ID="labTitle" Text="" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td>
					<table class="queryTable" cellpadding="3px" cellspacing="0px">
						<tr>
							<td class="descTd">
								项目
							</td>
							<td>
								<span class="spanSelect" style="width: 122px">
									<asp:TextBox ID="txtProject" Style="width: 97px; height: 15px; border: none; line-height: 16px;
										margin: 1px 0px 1px 2px" onblur="stripScript('txtProject')" runat="server"></asp:TextBox>
									<img alt="选择录入人" onclick="openProjPicker();" src="../../images/icon.bmp" style="float: right;" />
								</span>
							</td>
							<td>
								月份
							</td>
							<td class="style2">
								<asp:TextBox ID="txtDate" Width="120px" onclick="WdatePicker({dateFmt:'yyyy-MM'})" runat="server"></asp:TextBox>
							</td>
							<td class="style2">
								<asp:Button ID="btn_Search" Text="查询" OnClick="btn_Search_Click" runat="server" />
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td class="divFooter" style="text-align: left;">
					<%if (base.Request["PlanType"].ToString() == "allplan")
{%><asp:Button ID="btnExcela" Width="80px" Text="导出Excel" OnClick="btnExcela_Click" runat="server" />
					<%}
else
{%><asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
					<%}%>
				</td>
			</tr>
			<tr>
				<td style="vertical-align: top;">
					<div class="pagediv">
						<%if (base.Request["PlanType"].ToString() == "allplan")
{%><asp:GridView ID="GridView1" CssClass="gvdata" Width="100%" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="gvPayoutPlan_RowDataBound" DataKeyNames="prjGuid" runat="server">
<EmptyDataTemplate>
								<table class="style3 header">
									<tr>
										<td width="20">
											序号
										</td>
										<td>
											项目
										</td>
										<td width="200">
											合同
										</td>
										<td width="90">
											上月计划金额
										</td>
										<td width="90">
											本月计划金额
										</td>
										<td width="90">
											上月实际完成
										</td>
										<td width="120">
											计划执行完成情况
										</td>
										<td width="90">
											计划执行差异
										</td>
										<td width="200">
											说明
										</td>
									</tr>
								</table>
							</EmptyDataTemplate>
<Columns><asp:BoundField DataField="PrjName" HeaderText="项目名称" /><asp:BoundField DataField="lastplanincome" HeaderText="收入" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="lastplanpayout" HeaderText="支出" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="lastactuallyincome" HeaderText="收入" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="lastincomecpl" HeaderText="完成情况" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="lastactuallypayout" HeaderText="支出" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="lastpayoutcpl" HeaderText="完成情况" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="thisincome" HeaderText="收入" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="thispayout" HeaderText="支出" ItemStyle-HorizontalAlign="Right" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
						<%}
else
{%><asp:GridView ID="gvPayoutPlan" CssClass="gvdata" Width="100%" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" OnPageIndexChanging="gvPayoutPlan_PageIndexChanging" OnRowDataBound="gvPayoutPlan_RowDataBound" DataKeyNames="prjguid" runat="server">
<EmptyDataTemplate>
								<table class="style3 header">
									<tr>
										<td width="20">
											序号
										</td>
										<td>
											项目
										</td>
										<td width="200">
											合同
										</td>
										<td width="90">
											上月计划金额
										</td>
										<td width="90">
											本月计划金额
										</td>
										<td width="90">
											上月实际完成
										</td>
										<td width="120">
											计划执行完成情况
										</td>
										<td width="90">
											计划执行差异
										</td>
										<td width="200">
											说明
										</td>
									</tr>
								</table>
							</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="PrjName" HeaderText="项目" /><asp:BoundField HeaderText="上期计划金额" DataField="planMoney" /><asp:BoundField HeaderText="上期实际发生金额" DataField="conPayMoney" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="本期计划金额">
<ItemTemplate>
										<%# Eval("NewPlanMoney") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上期计划执行完成情况">
<ItemTemplate>
										<%# Eval("ratio") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="说明">
<ItemTemplate>
										<%# StringUtility.GetStr(Eval("Remark"), 15) %>
									</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
						<%}%>
					</div>
				</td>
			</tr>
		</table>
	</div>
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<div id="divSelectContract" title="选择合同" style="display: none">
		<iframe id="ifrmSelectContract" frameborder="0" width="100%" height="100%" src="../../StockManage/Purchase/PurchaseplanList.aspx">
		</iframe>
	</div>
	</form>
</body>
</html>
