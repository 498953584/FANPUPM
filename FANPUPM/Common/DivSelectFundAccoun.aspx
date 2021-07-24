<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivSelectFundAccoun.aspx.cs" Inherits="Common_DivSelectFundAccoun" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>账户信息</title>
	<script src="/Web_Client/Tree.js" type="text/javascript"></script>
	<script src="/Script/jquery.js" type="text/javascript"></script>
	<script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
	<script src="/StockManage/script/Config2.js" type="text/javascript"></script>
	<script language="JavaScript" type="text/javascript">
		addEvent(window, 'load', function () {
			var aa = new JustWinTable('grdModules');

		});

		function dbClickRow(obj, theCode, theName) {
			if (typeof top.ui.callback != null) {
				top.ui.callback({ id: theCode, name: theName });
			}
			top.ui.closeWin();
		}
       
	</script>
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" scroll="no">
	<form id="Form1" method="post" runat="server">
	<table id="Table2" class="table-nomal" height="100%" cellspacing="0" cellpadding="2"
		width="100%">
		<tr class="td-title">
			<td width="20" style="border: solid 0px red;">
				<input class="input_hide" id="hdnModuleCode" type="hidden" name="hdnModuleCode" runat="server" />

				<asp:HiddenField ID="hdfRQis" runat="server" />
			</td>
		</tr>
		<tr>
			<td style="text-align: left; border: solid 0px red;">
				账户编号：<asp:TextBox ID="AccountCode" Width="120px" runat="server"></asp:TextBox>&nbsp;
				账户名称：<asp:TextBox ID="AccountName" Width="120px" runat="server"></asp:TextBox>&nbsp;
				<asp:Button ID="SearchBt" Text="查 询" OnClick="SearchBt_Click" runat="server" />
			</td>
		</tr>
		<tr>
			<td valign="top" align="center" style="border: solid 0px red; height: 100%; padding-top: 10px;">
				<div id="pagediv" style="width: 100%; border: solid 0px red; text-align: left;" align="center">
					<asp:GridView ID="grdModules" Width="100%" AutoGenerateColumns="false" AllowPaging="true" CssClass="gvdata" ShowFooter="false" OnPageIndexChanging="grdModules_PageIndexChanging" OnRowDataBound="grdModules_RowDataBound" DataKeyNames="AccountID" runat="server"><Columns><asp:TemplateField HeaderText="序号"><ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate></asp:TemplateField><asp:BoundField DataField="accountNum" HeaderText="账户编号" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Left" /><asp:BoundField DataField="acountName" HeaderText="账户名称" HeaderStyle-Width="90px" /><asp:BoundField DataField="initialFund" HeaderText="启动金额" HeaderStyle-Width="65px" /><asp:BoundField DataField="createMan" HeaderText="创建人" HeaderStyle-Width="90px" /><asp:BoundField DataField="creatDate" HeaderText="创建日期" HeaderStyle-Width="65px" /><asp:TemplateField HeaderText="序号"><ItemTemplate>
									<asp:Label ID="lblPrjName" Text='<%# System.Convert.ToString(this.AL.getPrjName(System.Convert.ToString(Eval("PrjGuid"))), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
								</ItemTemplate></asp:TemplateField></Columns><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</div>
			</td>
		</tr>
		<tr class="td-submit">
			<td align="right" style="border: solid 0px red;">
				<input type="hidden" id="hdn1" name="hdn1" style="width: 10px" runat="server" />

				<input type="hidden" id="hdn2" name="hdn2" style="width: 10px" runat="server" />

				<input type="hidden" id="hdn3" name="hdn3" style="width: 10px" runat="server" />

				<input id="btnCancel" type="button" value="取 消" onclick="top.ui.closeWin();" />
			</td>
		</tr>
	</table>
	<input id="hdnModuleCodeList" style="width: 10px" type="hidden" name="hdnModuleCodeList" runat="server" />

	<asp:HiddenField ID="hdName" runat="server" />
	<asp:HiddenField ID="hdCode" runat="server" />
	</form>
</body>
</html>
