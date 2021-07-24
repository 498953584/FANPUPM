<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkFlowCountTab.aspx.cs" Inherits="ModuleSet_WorkFlowCountTab" %>


<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>流程模板名称</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			var jwTable1 = new JustWinTable('GVWorkTemp');
		});
	</script>
</head>
<body scroll="no">
	<form id="form1" runat="server">
	<div style="height: 100%;">
		<table id="table4" border="1" cellpadding="0" cellspacing="0" width="100%" height="100%">
			<tr>
				<td class="divHeader">
					流程模板名称
				</td>
			</tr>
			<tr valign="top">
				<td>
					<div id="Div1" style="overflow: auto; width: 100%; height: 100%">
						<asp:GridView ID="GVWorkTemp" Width="100%" AllowPaging="true" AutoGenerateColumns="false" PageSize="5" DataSourceID="SqlWorkTemp" OnRowDataBound="GVWorkTemp_RowDataBound" DataKeyNames="NodeID" runat="server"><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
										
									</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="NodeName" HeaderText="节点名称" SortExpression="NodeName" /><asp:BoundField DataField="Operater" HeaderText="审核人" SortExpression="Operater" /><asp:BoundField DataField="During" HeaderText="时间限额" SortExpression="TemplateID" /><asp:BoundField DataField="AuditMain" HeaderText="审核要点" SortExpression="AuditMain" /></Columns></asp:GridView>
						<asp:SqlDataSource ID="SqlWorkTemp" SelectCommand="SELECT TemplateID, NodeID, NodeName, AuditorType, Operater, FrontNode, ConditionDescription, IsSendMsg, IsAllPass, During, IsSecValidate, IsSelReceiver, AuditMain FROM WF_TemplateNode where TemplateID = @TemplateID" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="TemplateID" QueryStringField="tid"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
					</div>
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
