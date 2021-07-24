<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectElectronFiles.aspx.cs" Inherits="ProjectElectronFiles" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_uprojectlist_ascx" Src="~/EPC/UserControl1/uprojectlist.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>电子归档</title>
	<script src="/Script/jquery.js" type="text/javascript"></script>
	<script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
	<script src="/StockManage/script/Config2.js" type="text/javascript"></script>
	<script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
	<script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
	<script language="javascript" type="text/javascript">
		$(function () {
			$("#GridView1 tr").each(function () {
				$(this).find('td').eq(1).find("img").attr("src", "/images/over.gif");
			});

			var tbl = new JustWinTable('GridView1');
			setButton(tbl, 'Button1', 'Button1', 'btnQuery', 'hidenID')

			replaceEmptyTable('emptyContractType', 'GridView1')

			tbl.registClickTrListener(function () {
				$('#btnRepeal').attr('disabled', false)
			});
		});

		function viewopen_n(url, titleStr) {
			if (url != "") {
				parent.parent.desktop.flowclass = window;
				toolbox_oncommand(url, titleStr);
			}
		}
	</script>
</head>
<body>
	<form id="from1" runat="server">
	<div id="header" style="height: 28px; line-height: 28px;">
		电子归档
	</div>
	<div>
		<table class="queryTable" cellpadding="3px" cellspacing="0px">
			<tr>
				<td class="descTd">
					名称
				</td>
				<td class="">
					<asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox>
				</td>
				<td class="descTd">
					内容
				</td>
				<td class="">
					<asp:TextBox ID="txtContext" Width="120px" runat="server"></asp:TextBox>
				</td>
				<td class="descTd">
					备注
				</td>
				<td class="">
					<asp:TextBox ID="txtInfo" Width="120px" runat="server"></asp:TextBox>
				</td>
				<td>
					<asp:Button ID="btnQueryList" Text="查询" OnClick="btnQueryList_Click" runat="server" />
				</td>
			</tr>
		</table>
	</div>
	<div class="divFooter" style="text-align: left;">
		<input id="Button1" type="button" value="button" style="display: none" />
		<input id="Button2" type="button" value="button" style="display: none" />
		<asp:Button ID="btnQuery" Text="查看" OnClick="btnQuery_Click" runat="server" />
		<asp:Button ID="btnRepeal" Text="撤销归档" Width="70px" OnClick="btnRepeal_Click" runat="server" />
	</div>
	<div>
		<asp:GridView ID="GridView1" CssClass="gvdata" AllowPaging="true" AutoGenerateColumns="false" PageSize="20" Width="100%" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="ID" runat="server">
<EmptyDataTemplate>
				<table id="emptyContractType" class="gvdata">
					<tr class="header">
						<th scope="col" style="width: 20px;">
							<input id="chk1" type="checkbox" />
						</th>
						<th scope="col" style="width: 20px;">
						</th>
						<th scope="col">
							序号
						</th>
						<th scope="col">
							名称
						</th>
						<th scope="col">
							信息描述
						</th>
						<th scope="col">
							附件
						</th>
					</tr>
				</table>
			</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
<HeaderTemplate>
						<input id="chkAll" type="checkbox" />
					</HeaderTemplate>

<ItemTemplate>
						<asp:HiddenField ID="HiddenField1" Value='<%# Convert.ToString(Eval("ID")) %>' runat="server" />
						<asp:CheckBox ID="chk" runat="server" />
					</ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-Width="20px" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
<ItemTemplate>
						<asp:Image ID="Image1" Width="12px" Height="12px" runat="server" />
					</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
<ItemTemplate>
						<%# Container.DataItemIndex + 1 %>
					</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="file_name" HeaderText="名称" ItemStyle-Width="100px" /><asp:BoundField DataField="Content" HeaderText="内容" /><asp:BoundField DataField="file_info" HeaderText="备注" /><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderStyle-Width="30px">
<ItemTemplate>
						<%# GetAnnxA(Convert.ToString(Eval("file_sid")), Convert.ToString(Eval("Original_table"))) %>
					</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
	</div>
	<asp:HiddenField ID="hidenID" runat="server" />
	</form>
</body>
</html>
