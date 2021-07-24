<%@ Page Language="C#" AutoEventWireup="true" CodeFile="criterionlist.aspx.cs" Inherits="CriterionList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>CriterionList</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script src="/StockManage/script/Config2.js" type="text/javascript"></script>
	<script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
	<script language="javascript" type="text/javascript">
		addEvent(window, 'load', function () {
			replaceEmptyTable('emptyTable', 'GridViewCriterion');
			var purchasePlanTable = new JustWinTable('GridViewCriterion');
			if (request("Type").toUpperCase() == "EDIT") {
				setButton(purchasePlanTable, 'BtnDelete', 'BtnModify', 'BtnSee', 'Hidden1')
			}
		});
		function OpType(caseType, ClassID, Id) {
			var QS = document.getElementById("QS").value;
			if (Id == "") {
				Id = document.getElementById('Hidden1').value;
			}
			var Class = document.getElementById('hdnClass').value;
			var titleStr = "";
			if (QS == "S") {
				titleStr = "安全规范记录";
			} else if (QS == "Q") {
				titleStr = "质量规范记录";
			}
			var url = "";
			switch (caseType) {
				case "Edit":
					if (QS != "" && Id != "" && ClassID != "") {
						url = "/EPC/QuaitySafety/CriterionEdit.aspx?Flage=" + QS + "&Type=Update&Code=" + Id + "&DatumClass=" + Class + "&ClassID=" + ClassID;
					}
					break;
				case "See":
					if (QS != "" && Id != "" && ClassID != "") {
						url = "/EPC/QuaitySafety/CriterionEdit.aspx?Flage=" + QS + "&Type=See&Code=" + Id + "&DatumClass=" + Class + "&ClassID=" + ClassID;
					}
					break;
				case "ADD":
					if (QS != "" && Class != "" && ClassID != "") {
						url = "/EPC/QuaitySafety/CriterionEdit.aspx?Flage=" + QS + "&Type=Add&DatumClass=" + Class + "&ClassID=" + ClassID;
					}
					break;
			}
			if (url != "") {
				top.ui._criterionlist = window;
				toolbox_oncommand(url, titleStr);
			}
		}
       
	</script>
	<style type="text/css">
		.dgheader
		{
			background-color: #EEF2F5;
			white-space: nowrap;
			overflow: hidden;
			font-weight: normal;
			text-align: center;
			border-color: #CADEED;
			color: #727FAA;
			padding-left: 6px;
			padding-right: 6px;
		}
	</style>
</head>
<body>
	<form id="Form1" method="post" runat="server">
	<table id="Table1" width="100%" cellpadding="0" cellspacing="0">
		<tr>
			<td class="divHeader" style="height: 28px;">
				<%=this.strName %>
			</td>
		</tr>
		<tr>
			<td class="" style="text-align: left; width: 100%; padding-left: 5px; padding-top: 5px;
				padding-bottom: 5px;">
				<asp:DropDownList ID="DDLLookup" runat="server"><asp:ListItem Value="1" Text="规范名称" /><asp:ListItem Value="2" Text="发布单位" /><asp:ListItem Value="3" Text="备注" /></asp:DropDownList>
				<asp:HiddenField ID="Hidden1" runat="server" />
				<asp:TextBox ID="TxtLookup" Width="200px" runat="server"></asp:TextBox>&nbsp; &nbsp;
				<asp:Button ID="btn_Search" Text="查询" OnClick="btn_Search_Click" runat="server" />
				&nbsp;&nbsp;
			</td>
		</tr>
		<tr id="tb" runat="server"><td id="Td_Btn" class="divFooter" style="text-align: left; width: 100%" runat="server">
				&nbsp;<asp:Button ID="BtnAdd" Text="新增" CssClass="button-normal" OnClick="BtnAdd_Click" runat="server" />
				<asp:Button ID="BtnModify" CssClass="button-normal" Text="编辑" Enabled="false" OnClick="BtnModify_Click" runat="server" />
				<asp:Button ID="BtnDelete" Text="删除" CssClass="button-normal" Enabled="false" OnClick="BtnDelete_Click" runat="server" />&nbsp;
				<asp:Button ID="BtnSee" Text="查看" CssClass="button-normal" Enabled="false" runat="server" />&nbsp;
			</td></tr>
		<tr>
			<td colspan="2" height="100%" valign="top">
				<div class="gridBox">
					<asp:GridView ID="GridViewCriterion" AllowPaging="true" AutoGenerateColumns="false" PageSize="20" CssClass="gvdata" OnPageIndexChanging="GridViewCriterion_PageIndexChanging" OnRowDataBound="GridViewCriterion_RowDataBound" DataKeyNames="CriterionCode,mark" runat="server">
<EmptyDataTemplate>
							<table id="emptyTable" class="gvdata" border="0" cellspacing="0" cellpadding="0"
								width="100%">
								<tr class="header" id="ba">
									<th scope="col" scope="col" style="width: 20px;">
										<input id="chk1" type="checkbox" disabled="disabled" />
									</th>
									<th scope="col" style="width: 30px; white-space: nowrap;">
										序号
									</th>
									<th scope="col">
										质量规范名称
									</th>
									<th scope="col" style="width: 100px;">
										发布日期
									</th>
									<th scope="col" style="width: 100px;">
										发布单位
									</th>
									<th scope="col" style="width: 130px;">
										备注
									</th>
									<th scope="col" style="width: 17px;">
										附件
									</th>
								</tr>
							</table>
						</EmptyDataTemplate>
<Columns><asp:TemplateField>
<HeaderTemplate>
									<input id="chkAll" type="checkbox" />
								</HeaderTemplate>

<ItemTemplate>
									<asp:Label ID="Label3" Text='<%# Convert.ToString(Eval("CriterionCode")) %>' runat="server"></asp:Label>
									<asp:CheckBox ID="chk" runat="server" />
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="安全事故名称"><ItemTemplate>
									<asp:Label ID="Label1" CssClass="link" Text='<%# Convert.ToString(Eval("CriterionName")) %>' runat="server"></asp:Label>
								</ItemTemplate></asp:TemplateField><asp:BoundField DataField="PublishDate" HeaderText="发布日期" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" /><asp:BoundField HeaderText="发布单位" DataField="Publisher" ItemStyle-Width="100px" /><asp:BoundField DataField="Remark" HeaderText="备注" ItemStyle-Width="130px" /><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
									<%# GetAnnx(Convert.ToString(Eval("CriterionCode"))) %>
								</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><HeaderStyle CssClass="header"></HeaderStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</div>
				<input id="hdnClass" type="hidden" name="hdnClass" runat="server" />

				<asp:HiddenField ID="hdnClassID" runat="server" />
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="QS" runat="server" />
	</form>
	<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
</body>
</html>
<script language="javascript" type="text/javascript">
	$(function () {
		if (request("Type").toUpperCase() == "LIST") {
			$("#tb").hide();
		}
	});
	function clickRow(sc) {

	}
	function request(paras) {
		var url = location.href;
		var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
		var paraObj = {}
		for (i = 0; j = paraString[i]; i++) {
			paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
		}
		var returnValue = paraObj[paras.toLowerCase()];
		if (typeof (returnValue) == "undefined") {
			return "";
		} else {
			return returnValue;
		}
	}
</script>
