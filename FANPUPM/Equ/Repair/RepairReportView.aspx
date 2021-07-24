<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepairReportView.aspx.cs" Inherits="Equ_Repair_RepairReportView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>设备维修查看</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			replaceEmptyTable('emptyRepairStock', 'gvRepairStock')
		});
	</script>
</head>
<body id="print1">
	<form id="form1" runat="server">
	<table class="tab" style="vertical-align: top;">
		<tr style="height: 1px;">
			<td class="divHeader">
				设备维修查看
				<input type="button" id="btnDy" style="float: right;" class="noprint" onclick="winPrint()"
					value=" 打 印 " />
			</td>
		</tr>
		<tr style="height: 1px;">
			<td>
				<table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;"
					class="viewTable">
					<tr>
						<td style="border-style: none;">
							制单人:&nbsp;&nbsp;<asp:Label ID="lblBllProducer" runat="server"></asp:Label>
						</td>
						<td style="border-style: none; text-align: right">
							打印日期:&nbsp;&nbsp;<asp:Label ID="lblPrintDate" runat="server"></asp:Label>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr style="height: 1px;">
			<td style="vertical-align: top;">
				<table class="viewTable" cellpadding="0px" cellspacing="0">
					<tr>
						<td class="descTd">
							上报日期
						</td>
						<td class="elemTd">
							<asp:Label ID="lblReportDate" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							维修申请
						</td>
						<td class="elemTd">
							<asp:Label ID="lblApplyCode" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							设备编号
						</td>
						<td class="elemTd">
							<asp:Label ID="lblEquCode" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							设备名称
						</td>
						<td class="elemTd">
							<asp:Label ID="lblEquName" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							规格
						</td>
						<td class="elemTd">
							<asp:Label ID="lblSpecification" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							维修方式
						</td>
						<td class="elemTd">
							<asp:Label ID="lblRepairType" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							故障简介
						</td>
						<td class="elemTd">
							<asp:Label ID="lblFaultDescription" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							维修项目
						</td>
						<td class="elemTd">
							<asp:Label ID="lblRepairContent" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							维修开始时间
						</td>
						<td class="elemTd">
							<asp:Label ID="lblRepairStartDate" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							维修结束时间
						</td>
						<td class="elemTd">
							<asp:Label ID="lblRepairEndDate" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							维修人员
						</td>
						<td class="elemTd">
							<asp:Label ID="lblRepairPerson" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							原因分析
						</td>
						<td class="elemTd">
							<asp:Label ID="lblReason" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							委外维修公司
						</td>
						<td class="elemTd">
							<asp:Label ID="lblOutCompany" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							委外维修部门
						</td>
						<td class="elemTd">
							<asp:Label ID="lblOutDepartment" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							委外维修合同
						</td>
						<td class="elemTd">
							<asp:Label ID="lblContract" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							委外分包商
						</td>
						<td class="elemTd">
							<asp:Label ID="lblOutSubContractor" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							人工费
						</td>
						<td class="elemTd">
							<asp:Label ID="lblLaborCost" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							材料费
						</td>
						<td class="elemTd">
							<asp:Label ID="lblStuffCost" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							维修费用
						</td>
						<td class="elemTd">
							<asp:Label ID="lblRepairCost" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							验收人
						</td>
						<td class="elemTd">
							<asp:Label ID="lblAcceptor" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							附件
						</td>
						<td class="elemTd" colspan="3">
							<asp:Label ID="lblAccessory" Text="" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							备注
						</td>
						<td colspan="3" style="word-break: break-all;">
							<asp:Label ID="lblNotes" runat="server"></asp:Label>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr style="height: 1px;">
			<td>
				<table style="width: 100%; height: 50%; margin-top: 10px;" border="0">
					<tr style="vertical-align: top;">
						<td>
							<asp:GridView ID="gvRepairStock" Width="100%" AutoGenerateColumns="false" CssClass="viewTable" DataKeyNames="ResourceId" runat="server">
<EmptyDataTemplate>
									<table class="tab" id="emptyRepairStock">
										<tr class="header">
											<th scope="col" style="width: 25px;">
												序号
											</th>
											<th scope="col">
												物资编号
											</th>
											<th scope="col">
												物资名称
											</th>
											<th scope="col">
												规格
											</th>
											<th scope="col">
												型号
											</th>
											<th scope="col">
												品牌
											</th>
											<th scope="col">
												技术参数
											</th>
											<th scope="col">
												单位
											</th>
											<th scope="col">
												数量
											</th>
											<th scope="col">
												采购价格
											</th>
											<th scope="col">
												小计
											</th>
											<th scope="col">
												供应商
											</th>
											<th scope="col">
												领用人
											</th>
											<th scope="col">
												领用日期
											</th>
										</tr>
										</tr>
									</table>
								</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px"><ItemTemplate>
											
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号" HeaderStyle-Width="50px"><ItemTemplate>
											<div style="word-break: break-all;">
												
											</div>
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资名称" HeaderStyle-Width="80px">
<ItemTemplate>
											<div style="word-break: break-all;">
												
											</div>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
											<div style="word-break: break-all;">
												
											</div>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
											<div style="word-break: break-all;">
												
											</div>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
											<div style="word-break: break-all;">
												
											</div>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
											<div style="word-break: break-all;">
												
											</div>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
											<div style="word-break: break-all;">
												
											</div>
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
											
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="价格" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
											
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
											
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="供应商">
<ItemTemplate>
											<div style="word-break: break-all;">
												
											</div>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="领用人">
<ItemTemplate>
											<div style="word-break: break-all;">
												
											</div>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="领用日期" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
											<div style="text-align: center; word-break: break-all;">
												
											</div>
										</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr id="trAudit" style="vertical-align: top;">
			<td>
				<MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiClass="001" BusiCode="148" runat="server" />
			</td>
		</tr>
		<tr>
		</tr>
	</table>
	</form>
</body>
</html>
