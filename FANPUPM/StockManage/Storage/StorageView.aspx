<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageView.aspx.cs" Inherits="StockManage_Storage_StorageView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>入库单</title>
	<style type="text/css" media="print">
		.noprint
		{
			display: none;
		}
	</style>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../script/Config2.js"></script>
	<script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			setAnnxReadOnly('flAnnx');
			replaceEmptyTable('emptyStorageStock', 'gvwStorageStock')
		});
	</script>
</head>
<body id="print1">
	<form id="form1" runat="server">
	<table class="tab" style="min-width: 800px;">
		<tr>
			<td class="divHeader">
				<asp:Label ID="lblTitle" Text="" runat="server"></asp:Label>
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
		<tr >
			<td style="vertical-align: top;">
				<table cellpadding="0" cellspacing="0" class="viewTable">
					<tr>
						<td class="descTd">
							入库编号
						</td>
						<td class="elemTd">
							<asp:Label ID="lblScode" Text="" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							入库日期
						</td>
						<td class="elemTd">
							<asp:Label ID="DateInTime" Text="" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							仓库名称
						</td>
						<td class="elemTd">
							<asp:Label ID="lblTreasury" Text="" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							项目
						</td>
						<td class="elemTd">
							<asp:Label ID="lblProject" Text="" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							验收人
						</td>
						<td class="elemTd">
							<asp:Label ID="lblPerson" Text="" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							附件
						</td>
						<td class="elemTd">
							<MyUserControl:epc_usercontrol1_filelink_ascx ID="flAnnx" runat="server" />
						</td>
					</tr>
					<tr id="trd" runat="server"><td class="descTd" runat="server">
							移交人
						</td><td class="elemTd" runat="server">
							<asp:Literal ID="Literal1" runat="server"></asp:Literal>
						</td><td class="descTd" runat="server">
							监理
						</td><td class="elemTd" runat="server">
							<asp:Literal ID="Literal2" runat="server"></asp:Literal>
						</td></tr>
					<tr>
						<td class="descTd">
							说明
						</td>
						<td colspan="3" style="word-break: break-all;">
							<asp:Label ID="lblExplain" runat="server"></asp:Label>
						</td>
					</tr>
				</table>
				<table cellpadding="0" cellspacing="0" style="margin-top: 10px;" border="0">
					
					<tr>
						<td style="vertical-align: top;">
							<asp:GridView ID="gvwStorageStock" CssClass="viewTable" AutoGenerateColumns="false" runat="server">
<EmptyDataTemplate>
									<table class="gvdata" id="emptyStorageStock">
										<tr class="header">
											<td style="width: 59px">
												序号
											</td>
											<td style="width: 160px;">
												物资编号
											</td>
											<td style="width: 160px">
												物资名称
											</td>
											<td style="width: 160px">
												规格
											</td>
											<td style="width: 80px">
												型号
											</td>
											<td style="width: 80px">
												品牌
											</td>
											<td style="width: 80px">
												技术参数
											</td>
											<td style="width: 70px">
												单位
											</td>
											<td style="width: 25px">
												合同数量
											</td>
											<td style="width: 25px">
												累计已入库数量
											</td>
											<td style="width: 70px">
												数量
											</td>
											<td style="width: 25px">
												价格
											</td>
											<td style="width: 20px">
												小计
											</td>
											<td style="width: 120px">
												供应商
											</td>
                                            <td style="width: 30px">
												验收情况
											</td>
										</tr>
									</table>
								</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
											<%# Container.DataItemIndex + 1 %>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="物资编号">
<ItemTemplate>
											<div style="word-break: break-all;">
												<%# Eval("ResourceCode") %>
											</div>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="物资名称"><ItemTemplate>
											<div style="word-break: break-all; min-width: 100px;">
												<%# Eval("ResourceName") %>
											</div>
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
											<div style="word-break: break-all;">
												<%# Eval("Specification") %>
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
												<%# Eval("UnitName") %>
											</div>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合同数量" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
											<%# System.Convert.ToDecimal(Eval("ContractNumber")).ToString("0.000") %>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="累计已入库数量" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
											<%# System.Convert.ToDecimal(Eval("AllInNumber")).ToString("0.000") %>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="数量" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
											<%# System.Convert.ToDecimal(Eval("number")).ToString("0.000") %>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="价格" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
											<%# System.Convert.ToDecimal(Eval("sprice")).ToString("0.000") %>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
											<%# System.Convert.ToDecimal(Eval("Total")).ToString("0.000").Trim() %>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="供应商">
<ItemTemplate>
											<div style="word-break: break-all;">
												<%# Eval("CorpName") %>
											</div>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="验收情况">
<ItemTemplate>
											<div style="word-break: break-all;">
												<%# Eval("checkCondition") %>
											</div>
										</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr id="trAudit" style="vertical-align: top; width:100%;">
			<td  style=" width:100%;">
				<MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="074" BusiClass="001" runat="server" />
			</td>
		</tr>
		<tr>
			<td>
			</td>
		</tr>
	</table>
	<!--项目编码-->
	<asp:HiddenField ID="hfldPid" runat="server" />
	<asp:HiddenField ID="hfldProject" runat="server" />
	<asp:HiddenField ID="hfldPurchaseCode" runat="server" />
	<asp:HiddenField ID="hfldResourceCode" runat="server" />
	</form>
</body>
</html>
