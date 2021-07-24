<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectCode.aspx.cs" Inherits="BudgetManage_Cost_SelectCode" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>选择费用编号</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<style type="text/css">
		.query-tbl td
		{
			padding-left: 10px;
		}
	</style>
    <script type="text/javascript">
        $(document).ready(function () {
            var pcJtbl = new JustWinTable('gvwModify');
            pcJtbl.registClickTrListener(function () { $('#hfldModifyId').val(this.id) });
            pcJtbl.registDbClickListener(success);

        });

        function success() {
            var id = $('#hfldModifyId').val();
            var code = $('#' + id).find('td:eq(2) span').attr('title');
            var name = $('#' + id).find('td:eq(1) span').attr('title');
            if (typeof top.ui.callback == 'function') {
                top.ui.callback({ Id: id,Code: code,Name:name});
            }
            top.ui.closeWin();
          }


        
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<table class="query-tbl">
			<tr>
				
				<td>
					申请日期
				</td>
				<td>
					<asp:TextBox ID="txtApplyDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
                <td>
					费用编号
				</td>
				<td>
					<asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
				</td>
				<td>
					<asp:Button ID="btnSelect" Text="查询" OnClick="btnSelect_Click" runat="server" />
				</td>
			</tr>
		</table>
	</div>
	<div style="height: 400px;">
		<asp:GridView ID="gvwModify" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwModify_RowDataBound" DataKeyNames="Id" runat="server"><Columns><asp:TemplateField HeaderText="申请日期" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
						<%# System.Convert.ToDateTime(Eval("ApplyDate")).ToString("yyyy-MM-dd") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="费用名称" HeaderStyle-Width="490px"><ItemTemplate>
						<span title='<%# Eval("Name").ToString() %>'>
							<%# StringUtility.GetStr(Eval("Name").ToString(), 35) %>
						</span>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="费用编号" HeaderStyle-Width="490px"><ItemTemplate>
						<span title='<%# Eval("Code").ToString() %>'>
							<%# StringUtility.GetStr(Eval("Code").ToString(), 35) %>
						</span>
					</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
		<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
		</webdiyer:AspNetPager>
	</div>
	<div style="text-align: right;">
		<input type="button" id="btnOk" value="确定" onclick="success();" />
		<input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
	</div>
	<asp:HiddenField ID="hfldModifyId" runat="server" />
    </form>
</body>
</html>
