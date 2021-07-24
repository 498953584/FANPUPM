<%@ Page Language="C#" AutoEventWireup="true" CodeFile="corpsearch.aspx.cs" Inherits="CorpSearch" %>

<!DOCTYPE HTML >
<html>
<head runat="server"><title>CorpSearch</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript">
		function ReturnWhere() {
			// 返回sql的查询条件
			var wherestr = "1=1 ";

			//单位名称			
			var corpName = window.document.getElementById("Txt_CorpName").value;
			if (corpName != "") wherestr += " and CorpName like'%" + corpName + "%'";

			//单位性质
			//			var CorpKindName = window.document.getElementById("DDL_CorpKind");
			//			alert($('#DDL_CorpKind option:selected').val())
			//			var CorpKind = CorpKindName.options(CorpKindName.selectedIndex).value;
			var CorpKind = $('#DDL_CorpKind option:selected').val();
			if (CorpKind != "0") wherestr += " and CorpKind='" + CorpKind + "'";

			//联系人
			var LinkMan = window.document.getElementById("Txt_LinkMan").value;
			if (LinkMan != "") {
				var linkmans = "";
				if (LinkMan.indexOf(' ') != -1) {
					alert('系统提示:\n\n请不要留空格或者输入多人请用逗号隔开!');
					return;
				}
				if (LinkMan.indexOf(',') != -1) {
					linkmans = LinkMan.split(',');
				}
				else if (LinkMan.indexOf('，') != -1) {
					LinkMan = LinkMan.replace(/，/g, ,);
					linkmans = LinkMan.split(,);
				}
				if (LinkMan.indexOf(,) < 0 && LinkMan.indexOf("，") < 0 && LinkMan.length > 8) {
					alert('系统提示:\n\n输入多人请用逗号隔开!');
					linkmans = "";
				}
				if (linkmans.length != 0) {
					for (var i = 0; i < linkmans.length; i++) {
						if (linkmans[i] != "") {
							if (i == 0) {
								wherestr += " and LinkMan like'%" + linkmans[i] + "%' or";
							}
							if (i > 0 && i < linkmans.length - 1) {
								wherestr += " LinkMan like'%" + linkmans[i] + "%' or"
							}
							if (i == linkmans.length - 1) {
								wherestr += " linkman like '%" + linkmans[i] + "%'";
							}
						}
					}
				}
				if (linkmans == "" && LinkMan.indexOf(" ") < 0) {
					wherestr += " and LinkMan like'%" + LinkMan + "%' ";
				}
			}
			window.returnValue = wherestr;
			window.close();
		}
	</script>
</head>
<body>
	<form id="Form1" method="post" runat="server">
	<table class="table-normal" id="Table1" cellspacing="1" cellpadding="1" width="384"
		border="1" style="width: 384px; height: 169px">
		<tr>
			<td colspan="2" class="td-head">
				<font face="宋体">复合查询</font>
			</td>
		</tr>
		<tr>
			<td class="td-label" style="width: 83px">
				<font face="宋体">单位名称</font>
			</td>
			<td>
				<asp:TextBox ID="Txt_CorpName" Width="256px" runat="server"></asp:TextBox>
			</td>
			<font face="宋体"></font>
		</tr>
		<tr>
			<td class="td-label" style="width: 83px; height: 21px">
				<font face="宋体">单位性质</font>
			</td>
			<td style="height: 21px">
				<asp:DropDownList ID="DDL_CorpKind" Width="256px" runat="server"></asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td class="td-label" style="width: 83px">
				<font face="宋体">单位联系人</font>
			</td>
			<td>
				<asp:TextBox ID="Txt_LinkMan" Width="256px" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<input type="button" value="查询" onclick="ReturnWhere()" class="button-normal">&nbsp;&nbsp;<input
					type="button" value="退 出" class="button-normal" onclick="window.close()">
			</td>
		</tr>
	</table>
	<input id="hdnmyuser" type="hidden" runat="server" />

	</form>
</body>
</html>
