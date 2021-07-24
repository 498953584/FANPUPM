<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WantplanView.aspx.cs" Inherits="StockManage_basicset_WantplanView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>物资需求计划</title>
	<style type="text/css" media="print">
		.noprint
		{
			display: none;
		}
	</style>
	<style type="text/css">
		#bllProducer
		{
		}
		#bllProducer td
		{
		}
	</style>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../script/Config2.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			setAnnxReadOnly('FileLink1');

			if (!document.getElementById('gvwDiff')) {
				document.getElementById('trDiff').style.display = 'none';
			}
			if (!document.getElementById('gvwTaskDiff')) {
				document.getElementById('trTaskDiff').style.display = 'none';
			}
			registerChkDiffEvent();
			replaceEmptyTable('emptySmWantPlanStock', 'gvSmWantPlanStock');

		});

		function registerChkDiffEvent() {
			if (!document.getElementById('chkDiff')) return false;
			if (!document.getElementById('trDiff')) return false;
			if (!document.getElementById('chkTaskDiff')) return false;
			if (!document.getElementById('trTaskDiff')) return false;
			addEvent(document.getElementById('chkDiff'), 'click', function () {
				if (this.checked) {
					document.getElementById('trDiff').className = '';
				}
				else {
					document.getElementById('trDiff').className = 'noprint';
				}
			});
			addEvent(document.getElementById('chkTaskDiff'), 'click', function () {
				if (this.checked) {
					document.getElementById('trTaskDiff').className = '';
				}
				else {
					document.getElementById('trTaskDiff').className = 'noprint';
				}
			});
		}    
	</script>
</head>
<body id="print1">
	<form id="form1" runat="server">
	<table class="tab" style="vertical-align: top; min-width: 800px;">
		<tr>
			<td class="divHeader">
				物资需求计划
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
				<table cellpadding="0" cellspacing="0" class="viewTable">
					<tr>
						<td class="descTd">
							计划编号
						</td>
						<td class="elemTd">
							<asp:Label ID="lblCode" Text="Label" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							项目名称
						</td>
						<td class="elemTd">
							<asp:Label ID="lblPrjName" Text="Label" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							计划时间
						</td>
						<td class="elemTd">
							<asp:Label ID="dtxtApplyDate" Text="Label" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							录入人
						</td>
						<td class="elemTd">
							<asp:Label ID="lblPerson" Text="Label" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							设备
						</td>
						<td colspan="3">
							<asp:Label ID="lblEquCode" Text="" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							附件
						</td>
						<td colspan="3">
							<MyUserControl:epc_usercontrol1_filelink_ascx ID="FileLink1" runat="server" />
						</td>
					</tr>
					<tr>
						<td class="descTd">
							备注
						</td>
						<td colspan="3">
							<textarea id="txtRemark" disabled="true" cols="20" rows="2" visible="false" runat="server"></textarea>
							<div style="width: 70%; word-break: break-all;">
								<asp:Label ID="lblRemark" Text="" runat="server"></asp:Label>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr style="vertical-align: top; height: 1px;">
			<td style="vertical-align: top; padding-top: 10px;">
				<asp:GridView ID="gvSmWantPlanStock" AutoGenerateColumns="false" CssClass="viewTable" runat="server">
<EmptyDataTemplate>
						<table class="tab" id="emptySmWantPlanStock" width="100%">
							<tr class="header">
								<td style="width: 20px">
									<asp:CheckBox ID="chkAll" runat="server" />
								</td>
								<td style="width: 25px">
									序号
								</td>
								<td style="width: 100px">
									资源编号
								</td>
								<td style="width: 150px">
									资源名称
								</td>
								<td>
									规格
								</td>
								<td>
									型号
								</td>
								<td>
									品牌
								</td>
								<td>
									技术参数
								</td>
								<td>
									单位
								</td>
								<td>
									单价
								</td>
								<td>
									库存量
								</td>
								<td>
									需求数量
								</td>
								<td align="center">
									到货日期
								</td>
								<td align="center">
									设计编码
								</td>
								<td align="center">
									备注
								</td>
							</tr>
						</table>
					</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
								<%# Container.DataItemIndex + 1 %>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="资源编号" HeaderStyle-Width="50px"><ItemTemplate>
								<div style="word-break: break-all;">
									<asp:Label ID="lblCode" Text='<%# System.Convert.ToString(Eval("ResourceCode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
								</div>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源名称" HeaderStyle-Width="80px"><ItemTemplate>
								<div style="word-break: break-all;">
									<%# DataBinder.Eval(Container.DataItem, "ResourceName") %>
								</div>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
								<div style="word-break: break-all;">
									<%# DataBinder.Eval(Container.DataItem, "Specification") %>
								</div>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
								<div style="word-break: break-all;">
									<%# Eval("ModelNumber") %>
								</div>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
								<div style="word-break: break-all;">
									<%# Eval("brand") %>
								</div>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
								<div style="word-break: break-all;">
									<%# Eval("TechnicalParameter") %>
								</div>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位">
<ItemTemplate>
								<div style="word-break: break-all;">
									<%# DataBinder.Eval(Container.DataItem, "UnitName") %>
								</div>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem, "Price") %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="库存量" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
								<%# WebUtil.GetStockNumberByCode(Eval("ResourceCode").ToString()).ToString() %>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="需求数量" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
								<asp:Label ID="txtNumber" Text='<%# System.Convert.ToString((Eval("number").ToString() == "") ? "0.000" : Eval("number"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="到货日期" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
								<div style="text-align: center; word-break: break-all;">
									<%# Eval("arrivalDate").ToString().Replace("0:00:00", "") %></div>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="设计编码" ItemStyle-HorizontalAlign="Left">
<ItemTemplate>
								<div style="word-break: break-all;">
									<%# Eval("DesignCode") %>
								</div>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Left">
<ItemTemplate>
								<div style="word-break: break-all;">
									<%# Eval("Remark") %>
								</div>
							</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
			</td>
		</tr>
		<tr id="trTaskDiff" style="vertical-align: top; height: 1px;">
			<td>
				<div id="diffTaskTitle" style="position: relative; text-align: center; font-weight: bold;
					padding-top: 15px;">
					分项指标对比表
					<div class="noprint" style="height: 20px; width: 100%; left: 0px; bottom: 0px; text-align: right;
						position: absolute; font-weight: normal;">
						<asp:CheckBox ID="chkTaskDiff" Style="float: right;" Checked="true" Text="打印" runat="server" />
					</div>
				</div>
				<div style="width: 100%;">
					<asp:GridView AutoGenerateColumns="false" Width="100%" ID="gvwTaskDiff" CssClass="gvdata" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="任务编码" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
									<%# Eval("TaskCode") %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="任务名称" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
									<%# Eval("TaskName") %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="材料名称" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
									<%# Eval("ResourceName") %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
									<%# Eval("Specification") %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预算量" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
									<%# Eval("ResourceQuantity") %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="消耗量" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
									<%# Eval("UsedNumber") %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="本次计划量" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
									<%# Eval("WantNumber") %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="剩余量" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
									<%# Eval("RestNumber") %>
								</ItemTemplate></asp:TemplateField></Columns></asp:GridView>
				</div>
			</td>
		</tr>
		<tr id="trDiff" style="vertical-align: top; height: 1px;">
			<td>
				<div id="diffTitle" style="position: relative;">
					总预算对比表
					<div class="noprint" style="height: 20px; width: 100%; left: 0px; bottom: 0px; text-align: right;
						position: absolute; font-weight: normal;">
						<asp:CheckBox ID="chkDiff" Style="float: right;" Checked="true" Text="打印" runat="server" />
					</div>
				</div>
				<div style="width: 100%;">
					<asp:GridView AutoGenerateColumns="false" Width="100%" ID="gvwDiff" CssClass="gvdata" OnRowDataBound="gvwDiff_RowDataBound" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="ResourceName" HeaderText="名称" /><asp:BoundField DataField="Specification" HeaderText="规格" /><asp:BoundField DataField="fNumber" HeaderText="总预算量" ItemStyle-HorizontalAlign="Left" /><asp:BoundField DataField="aNumber" HeaderText="已消耗量" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="eNumber" HeaderText="本次计划量" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="Rate" HeaderText="差额比例" ItemStyle-HorizontalAlign="Right" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					<div style="margin-top: 10px; background-color: Red;" class="noprint">
						<div style="width: 12%; float: left; padding-left: 10px; line-height: 18px;">
							预警级别：
						</div>
						<div style="width: 80%; float: left; height: 70px; line-height: 18px">
							<span class="highLevel">高：红色字体，表示已超； </span>
							<br />
							<span class="midLevel">中：紫色字体； </span>
							<br />
							<span class="lowLevel">低：蓝色字体； </span>
							<br />
							<span>普通：黑色字体，表示未达到预警阀值，正常。 </span>
						</div>
					</div>
				</div>
			</td>
		</tr>
		<tr id="trAudit" style="vertical-align: top;">
			<td>
				<MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="071" BusiClass="001" runat="server" />
			</td>
		</tr>
		<tr>
		</tr>
	</table>
	<asp:HiddenField ID="hfldWantPlanGuid" runat="server" />
	
	<asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
	</form>
</body>
</html>
