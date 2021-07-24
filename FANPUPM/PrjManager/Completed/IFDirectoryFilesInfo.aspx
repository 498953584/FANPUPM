<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IFDirectoryFilesInfo.aspx.cs" Inherits="PrjManager_Completed_IFDirectoryFilesInfo" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>目录下的文件信息</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('gvFilesInfo');
			replaceEmptyTable('gvEmpty', 'gvFilesInfo')
			showTooltip('tooltip', 25);
		});
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<asp:GridView ID="gvFilesInfo" class="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvFilesInfo_RowDataBound" runat="server">
<EmptyDataTemplate>
			<table id="gvEmpty" class="gvdata">
				<tr class="header">
					<th scope="col" style="width: 20px;">
						序号
					</th>
					<th scope="col" style="width: 200px;">
						文件名称
					</th>
					<th scope="col" style="width: 100px;">
						文件类型
					</th>
					<th scope="col" style="width: 80px;">
						创建日期
					</th>
				</tr>
			</table>
		</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px">
<ItemTemplate>
					<%# Container.DataItemIndex + 1 %>
				</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="文件名称" HeaderStyle-Width="200px"><ItemTemplate>
					<a class="tooltip" style="text-decoration: none; color: Black;" title='<%# Eval("FileName") %>'>
						<%# StringUtility.GetStr(Eval("FileName"), 25) %>
					</a>
				</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="文件类型" HeaderStyle-Width="100px"><ItemTemplate>
					<%# GetFileType(Eval("FileName").ToString(), Eval("FileType").ToString()) %>
				</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="创建日期" DataField="CreateTime" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
	</form>
</body>
</html>
