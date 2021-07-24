<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractTypeCostAllocation.aspx.cs" Inherits="Construct_ContractTypeCostAllocation" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>合同类型成本归集</title><link type="text/css" rel="Stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			hideControls();
			var typeTable = new JustWinTable('gvwContractType');
			replaceEmptyTable();
			jw.tooltip();
		});

		function CheckType(typeId) {
			parent.desktop.ContractTypeEdit = window;
			var url = '/ContractManage/ContractType/ContractTypeEdit.aspx?Action=Query&TypeID=' + typeId;
			toolbox_oncommand(url, "查看合同类型");
		}

		function replaceEmptyTable() {
			//当数据量为空时，修改样式
			if (!document.getElementById('gvwContractType')) return;
			if (!document.getElementById('emptyContractType')) return;
			var gvwContractType = document.getElementById('gvwContractType');
			var emptyContractType = document.getElementById('emptyContractType');
			if (gvwContractType.firstChild.childNodes.length == 1) {
				gvwContractType.replaceChild(emptyContractType.firstChild, gvwContractType.firstChild);
			}
		}

		function hideControls() {
			if (!document.getElementById('btnDataBind')) return;
			document.getElementById('btnDataBind').style.display = 'none';
		}
        
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divContent2">
		<table style="vertical-align: top; width: 100%; height: 100%;">
			<tr style="display: none;">
				<td class="divHeader" colspan="2">
					合同类型成本归集
				</td>
			</tr>
			<tr>
				<td style="width: 100%; vertical-align: top;">
					<table style="width: 100%">
						<tr>
							<td>
								<table class="queryTable" cellpadding="3px" cellspacing="0px">
									<tr>
										<td class="descTd">
											类型编码
										</td>
										<td>
											<asp:TextBox ID="txtTypeCode" Width="120px" runat="server"></asp:TextBox>
										</td>
										<td class="descTd">
											类型名称
										</td>
										<td>
											<asp:TextBox ID="txtTypeName" Width="120px" runat="server"></asp:TextBox>
										</td>
										<td>
											<asp:Button ID="btnSelect" Text="查询" OnClick="btnSelect_Click" runat="server" />
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
								<asp:GridView ID="gvwContractType" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvwContractType_RowDataBound" DataKeyNames="TypeID,CBSCode" runat="server">
<EmptyDataTemplate>
										<table id="emptyContractType" class="gvdata">
											<tr class="header">
												<th scope="col" style="width: 25px;">
													序号
												</th>
												<th scope="col">
													类型编码
												</th>
												<th scope="col">
													类型名称
												</th>
												<th scope="col">
													直接成本
												</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate></asp:TemplateField><asp:BoundField DataField="TypeCode" HeaderText="类型编码" HeaderStyle-Width="100px" /><asp:TemplateField HeaderText="类型名称" HeaderStyle-Width="200px"><ItemTemplate>
												<span class="tooltip link" title='<%# Eval("TypeName") %>' onclick="CheckType('<%# Eval("TypeID") %>')">
													<%# StringUtility.GetStr(Eval("TypeName").ToString()) %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="直接成本" HeaderStyle-Width="200px"><ItemTemplate>
												<asp:DropDownList ID="ddlCBS" runat="server"></asp:DropDownList>
											</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<div class="divFooter2">
		<table class="tableFooter2">
			<tr>
				<td>
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hfldContractTypeGuid" runat="server" />
	</form>
</body>
</html>
