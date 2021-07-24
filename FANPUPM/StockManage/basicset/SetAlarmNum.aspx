<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetAlarmNum.aspx.cs" Inherits="StockManage_basicset_SetAlarmNum" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_usercontrol_selectresource_ascx" Src="~/StockManage/UserControl/SelectResource.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>设置数量</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var aa = new JustWinTable('gvNeed');
		});


		function pickBindResource() {
			if (document.getElementById("lblTitle").innerHTML == "") {
				top.ui.alert("请选择仓库!");
				return false;
			}
			return true;
		}

		function selectResource() {
			var tcode = jw.getRequestParam('tcode');
			if (tcode == '') {
				top.ui.alert('请选择仓库');
				return false;
			}
			var typeCode = '0002';
			var src = '/StockManage/UserControl/SelectResource.aspx?type=2&TypeCode=' + typeCode;
			top.ui.callback = function (obj) {
				$('#hfldResourceCode').val(obj.code);
				$('#btnShowList').click();
			}
			top.ui.openWin({ title: '选择资源', url: src, width: 1010, height: 500 });
		}
        
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table class="tab">
		<tr style="display: none;">
			<td class="divHeader2">
				<asp:Label ID="lblTitle" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td class="divFooter" style="height: 20px; text-align: left;">
				<input type="button" id="btnSelect" style="width: 100px;" onclick="selectResource()"
					value="从材料库选择" />
				<asp:Button ID="btnDel" Style="width: 100px;" OnClientClick="return confirm('您确定要删除吗？？')" Text="删除资源" OnClick="btnDel_Click" runat="server" />
			</td>
		</tr>
		<tr style="width: 100%; vertical-align: top;">
			<td style="border: solid 0px red; height: 92%;">
				<div class="pagediv" style="height: 100%;">
					<asp:GridView ID="gvNeed" Width="100%" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" OnPageIndexChanging="gvNeed_PageIndexChanging" runat="server"><Columns><asp:TemplateField>
<HeaderTemplate>
									<asp:CheckBox ID="chkAll" runat="server" />
								</HeaderTemplate>

<ItemTemplate>
									<asp:CheckBox ID="chkBox" ToolTip='<%# System.Convert.ToString(Eval("resourceCode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="资源编号"><ItemTemplate>
									<%# Eval("resourceCode") %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源名称"><ItemTemplate>
									<%# Eval("ResourceName") %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格"><ItemTemplate>
									<%# Eval("Specification") %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate>
									<%# Eval("Name") %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量"><ItemTemplate>
									<asp:TextBox Width="50px" ID="txtAlarmNum" CssClass="decimal_input" ToolTip='<%# System.Convert.ToString(Eval("resourceCode"), System.Globalization.CultureInfo.CurrentCulture) %>' Text='<%# System.Convert.ToString(Eval("AlarmNum"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
								</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</div>
			</td>
		</tr>
		<tr>
			<td class="divFooter" style="height: 20px; text-align: right;">
				<asp:Button ID="btnCanel" Text="确定" OnClick="btnCanel_Click" runat="server" />
				<input type="button" id="btnCancel" onclick="top.frameWorkArea.window.desktop.getActive().close();"
					value="取消" />
				<asp:Button ID="btnShowList" Text="显示" Style="display: none;" OnClick="btnShowList_Click" runat="server" />
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hdnCodeList" runat="server" />
	<asp:HiddenField ID="hfldResourceCode" runat="server" />
	</form>
</body>
</html>
