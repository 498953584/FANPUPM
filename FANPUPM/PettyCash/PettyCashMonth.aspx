<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PettyCashMonth.aspx.cs" Inherits="PettyCash_PettyCashMonth" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>借款月度汇总</title><link rel="Stylesheet" type="text/css" href="CSS/PettyCash.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/DecimalInput.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<div style="text-align: center; width: 100%;">
			<asp:Label ID="lblTitle" CssClass="title" runat="server"></asp:Label></div>
		<div class="divFooter" style="text-align: left;">
			<asp:DropDownList ID="dropYear" DataTextField="name" DataValueField="value" AutoPostBack="true" OnSelectedIndexChanged="dropYear_SelectedIndexChanged" runat="server"></asp:DropDownList>
			<asp:Button ID="btnExportExcel" Text="导出Excel" Width="70px" OnClick="btnExportExcel_Click" runat="server" />
		</div>
		<asp:GridView ID="gvwMonthCash" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="gvwMonthCash_RowDataBound" runat="server"><Columns><asp:BoundField DataField="No" HeaderText="序号" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="Date" HeaderText="日期" ItemStyle-Width="80px" /><asp:TemplateField HeaderText="事项"><ItemTemplate>
						<%# Eval("Matter") %>
					</ItemTemplate></asp:TemplateField><asp:BoundField DataField="Cash" HeaderText="借款金额" ItemStyle-CssClass="decimal_input" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Right" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
	</div>
	</form>
</body>
</html>
