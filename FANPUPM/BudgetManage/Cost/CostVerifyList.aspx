<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostVerifyList.aspx.cs" Inherits="BudgetManage_Cost_CostVerifyList" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>费用核销</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var cvJtbl = new JustWinTable('gvwCostVerify');
			cvJtbl.registClickTrListener(function () {
				$('#hfldDiaryId').val(this.id);
				$('#btnVerify').attr('disabled', false)
			});

			addLinkEvent(); 	// 添加超链接事件
			replaceEmptyTable('emptyCostVerify', 'gvwCostVerify');
		});

		// 添加超链接事件
		function addLinkEvent() {
			$('.cost-name').click(function () {
				var $tr = $(this).parents('tr')
				var id = $tr.attr('id');
				var costType = $tr.attr('costType');

				var url = '/BudgetManage/Cost/CostVerifyRecord.aspx?costType=p&ic=';

				url += id;

				top.ui.openTab({ title: '预报销明细', url: url });
			});
		}

		// 核销
		function costVerify() {
			var url = '/BudgetManage/Cost/CostVerifyEdit.aspx?diaryId=' + $('#hfldDiaryId').val();
			top.ui._costverifylist = window;
			top.ui.openTab({ title: '费用核销', url: url });
		}

	</script>
</head>
<body style="height: 98%;">
	<form id="form1" runat="server">
	<div>
		<table class="queryTable" cellpadding="3px" cellspacing="0px">
			<tr>
				<td>
					费用名称
				</td>
				<td>
					<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
				</td>
				<td>
					经手人
				</td>
				<td>
					<asp:TextBox ID="txtIssuedBy" runat="server"></asp:TextBox>
				</td>
				<td>
					填报人
				</td>
				<td>
					<asp:TextBox ID="txtInputUser" runat="server"></asp:TextBox>
				</td>
				<td>
					费用编码
				</td>
				<td>
					<asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					发生时间
				</td>
				<td>
					<asp:TextBox ID="txtInputDate1" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
				<td>
					至
				</td>
				<td>
					<asp:TextBox ID="txtInputDate2" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
				<td>
					类别
				</td>
				<td>
					<asp:DropDownList ID="dropCostType" runat="server"><asp:ListItem Value="" Text="" /><asp:ListItem Value="P" Text="项目" /><asp:ListItem Value="O" Text="组织机构" /></asp:DropDownList>
				</td>
				<td>
					<asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
				</td>
				<td>
				</td>
			</tr>
		</table>
		<div class="divFooter" style="text-align: left; vertical-align: middle;">
			<input type="button" id="btnVerify" value="核销" onclick="costVerify()" disabled="disabled" />
		</div>
		<asp:GridView ID="gvwCostVerify" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwCostVerify_RowDataBound" DataKeyNames="InDiaryId,CostType" runat="server">
<EmptyDataTemplate>
				<table id="emptyCostVerify" class="gvdata">
					<tr class="header">
						<th scope="col" style="width: 25px;">
							序号
						</th>
						<th scope="col" style="width: 220px;">
							费用名称
						</th>
						<th scope="col" style="width: 220px;">
							发生单位
						</th>
						<th scope="col" style="width: 60px;">
							经手人
						</th>
						<th scope="col" style="width: 60px;">
							填报人
						</th>
						<th scope="col" style="width: 220px;">
							费用编码
						</th>
						<th scope="col" style="width: 80px;">
							发生时间
						</th>
						<th scope="col" style="width: 80px;">
							偿还备用金
						</th>
						<th scope="col" style="width: 40px;">
							类别
						</th>
						<th scope="col">
							项目/部门
						</th>
					</tr>
				</table>
			</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
						<%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="费用名称" HeaderStyle-Width="220px"><ItemTemplate>
						<span class="cost-name link">
							<%# Eval("Name") %>
						</span>
					</ItemTemplate></asp:TemplateField><asp:BoundField DataField="Department" HeaderText="发生单位" HeaderStyle-Width="220px" /><asp:BoundField DataField="IssuedBy" HeaderText="经手人" HeaderStyle-Width="60px" /><asp:BoundField DataField="InputUser" HeaderText="填报人" HeaderStyle-Width="60px" /><asp:BoundField DataField="IndireCode" HeaderText="费用编码" HeaderStyle-Width="220px" /><asp:TemplateField HeaderText="发生时间" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
						<%# System.Convert.ToDateTime(Eval("InputDate")).ToString("yyyy-MM-dd") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备用金事项" HeaderStyle-Width="150px"><ItemTemplate>
						<%# GetPettyCashMatter(Eval("PettyCashId")) %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类别" HeaderStyle-Width="40px"><ItemTemplate>
						<%# (Eval("CostType").ToString() == "P") ? "项目" : "组织机构" %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目/部门"><ItemTemplate>
						<%# GetPrjOrOrg(Eval("InDiaryId")) %>
					</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
		<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
    </webdiyer:AspNetPager>
	</div>
	<asp:HiddenField ID="hfldDiaryId" runat="server" />
	</form>
</body>
</html>
