<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WarningList.aspx.cs" Inherits="oa_Warning_WarningList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>预警</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/ecmascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript">

	    $(document).ready(function () {
	        replaceEmptyTable('emptyTable', 'gvWarning');
	        var jwTable = new JustWinTable('gvWarning');
	        showTooltip('tooltip', 25);

	        // 点击后删除该信息
	        $('#gvWarning tr').click(function () {
	            var modelId = '2842';
	            var pk = this.id;
	            $.ajax({
	                type: 'GET',
	                async: true,
	                url: '/TableTop/Handler/UpdateTopFlag.ashx?' + new Date().getTime() + '&modelId=' + modelId + '&pk=' + pk
	            });
	        });
	    });

	    //标签页查看
	    function query(url) {
	        toolbox_oncommand(url, "预警提醒 ");
	    }
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr>
			<td style="vertical-align: top">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd" style="white-space: nowrap;">
							预警标题
						</td>
						<td>
							<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							预警内容
						</td>
						<td>
							<asp:TextBox ID="txtContent" runat="server"></asp:TextBox>
						</td>
						<td style="white-space: nowrap;">
							<asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top">
				<asp:GridView ID="gvWarning" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvWarning_RowDataBound" DataKeyNames="WarningId" runat="server">
<EmptyDataTemplate>
						<table id="emptyTable">
							<tr class="header">
								<th scope="col" style="width: 20px">
									序号
								</th>
								<th scope="col">
									预警标题
								</th>
								<th scope="col">
									预警内容
								</th>
								<th scope="col" style="width: 100px">
									预警时间
								</th>
							</tr>
						</table>
					</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" ItemStyle-Width="20px">
<ItemTemplate>
								<%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="预警标题"><ItemTemplate>
								<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" runat="server">
                                <span class="link tooltip" title='<%# Eval("WarningTitle").ToString() %>'  onclick="query('<%# DoWithUrl(Eval("URI").ToString(), Eval("WarningId").ToString()) %>')">
                                    <%# StringUtility.GetStr(Eval("WarningTitle").ToString(), 25) %>
                                </span>
								</asp:HyperLink>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预警内容"><ItemTemplate>
								<span class="tooltip" title='<%# Eval("WarningContent").ToString() %>'>
									<%# StringUtility.GetStr(Eval("WarningContent").ToString(), 25) %>
								</span>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预警时间" ItemStyle-Width="100px"><ItemTemplate>
								<%# Common2.GetTime(Eval("InputDate")) %>
							</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
				</webdiyer:AspNetPager>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
