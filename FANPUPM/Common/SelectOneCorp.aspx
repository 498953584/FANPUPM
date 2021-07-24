<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectOneCorp.aspx.cs" Inherits="Common_SelectOneSupplier" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>选择单个往来单位</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/json2.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('gvwCodeList');

			jwTable.registClickTrListener(function () {
				var corp = { id: $(this).attr('id'), name: $(this).attr('name') };
				$('#hfldCorpInfo').val(JSON.stringify(corp));
				$('#btnSave').removeAttr('disabled');
			});

			jwTable.registDbClickListener(saveEvent);
		});

		function saveEvent() {
			if (typeof top.ui.callback == 'function') {
				top.ui.callback(JSON.parse($('#hfldCorpInfo').val()));
			}
			top.ui.closeWin({ winNo: getRequestParam('winNo') });
		}

		function cancelEvent() {
			top.ui.closeWin({ winNo: getRequestParam('winNo') });
		}
		 
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<asp:DropDownList ID="dropType" DataValueField="CodeId" DataTextField="CodeName" AutoPostBack="true" OnSelectedIndexChanged="dropType_SelectedIndexChanged" runat="server"></asp:DropDownList>
		单位名称：<asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox>
		<asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
		<input id="btnSave" type="button" value="保存" onclick="saveEvent();" disabled="disabled" />
		<input id="Button1" type="button" value="取消" onclick="cancelEvent();" />
	</div>
	<div>
		<asp:GridView ID="gvwCodeList" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwCodeList_RowDataBound" DataKeyNames="CorpID,CorpName" runat="server"><Columns><asp:TemplateField HeaderText="序号" ItemStyle-Width="25px">
<ItemTemplate>
						<%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
					</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="CorpName" HeaderText="单位名称" /><asp:BoundField DataField="LinkMan" HeaderText="联系人" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
		</webdiyer:AspNetPager>
	</div>
	<asp:HiddenField ID="hfldCorpInfo" runat="server" />
	</form>
</body>
</html>
