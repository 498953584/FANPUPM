<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulletinManage.aspx.cs" Inherits="BulletinManage" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.pm.entpm.action"%>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>公告审核</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/DecimalInput.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script language="javascript" type="text/javascript">
	    $(document).ready(function () {
	        replaceEmptyTable();
	        var contractTable = new JustWinTable('GridView1');
	        setButton(contractTable, 'btnDel', 'btnEdit', 'BtnView', 'hdnRecordID')
	        setWfButtonState2(contractTable, 'WF1');
	        showTooltip('tooltip', 25);
	        $('#btnAudit').hide();
	    });

	    function openWin(op) {
	        var RID = "";
	        var title = "新增";
	        if (op == "edit") {
	            RID = document.getElementById('hdnRecordID').value;
	            title = "编辑";
	        }
	        var urlStr = "/oa/Bulletin/EditBulletin.aspx?op=" + op + "&rid=" + RID;
	        top.ui._BulletinManage = window;
	        toolbox_oncommand(urlStr, title);

	    }

	    //查看
	    function OpenLock() {
	        var rid = document.getElementById('hdnRecordID').value;
	        var url = "BulletinLock.aspx?rid=" + rid + "&ic=" + rid;
	        top.ui._BulletinManage = window;
	        toolbox_oncommand(url, '查看');
	        top.ui.callback = refresh;
	    }
	    //审核
	    function OpenAudit() {
	        var rid = document.getElementById('hdnRecordID').value;
	        var url = "/HR/Leave/ApplicationReq.aspx?ic=" + rid + "&mid=bg";
	        top.ui._BulletinManage = window;
	        toolbox_oncommand(url, '审核');
	        top.ui.callback = refresh;
	    }

	    function viewopen_n(url) {
	        viewopen(url);
	    }
	    //重新绑定公告
	    function refresh() {
	        document.getElementById('btnRefresh').click();
	    }
	</script>
</head>
<body class="body_frame">
	<form id="form1" runat="server">
	<div>
		<table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0"
			class="table-normal">
			<tr>
				<td class="td-title">
					公告列表
				</td>
			</tr>
			<tr id="tr_Button" runat="server"><td id="Td1" class="td-toolsbar" style="height: 24px" runat="server">
					&nbsp;<asp:DropDownList ID="DropDownList1" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" runat="server"><asp:ListItem Selected="true" Value="%" Text="全部" /><asp:ListItem Value="1" Text="已审核" /><asp:ListItem Value="-1" Text="未提交" /><asp:ListItem Value="0" Text="审核中" /><asp:ListItem Value="-2" Text="驳回" /></asp:DropDownList>
					<input type="button" value="新 增" id="btnAdd" onclick="openWin('add');" runat="server" />

					<input type="button" value="编 辑" id="btnEdit" onclick="openWin('edit');" disabled="true" runat="server" />

					<asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />&nbsp;
					<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="002" BusiClass="001" runat="server" />
					<asp:Button ID="btnAudit" Text="审 核" Enabled="false" OnClick="btnAudit_Click" runat="server" />
					<asp:Button ID="btnRefresh" Text="刷 新" Style="display: none" OnClick="btnRefresh_Click" runat="server" />
					<asp:Button ID="BtnView" Enabled="false" Text="查 看" runat="server" />
					<asp:HiddenField ID="hdnRecordID" runat="server" />
					&nbsp;
				</td></tr>
			<tr>
				<td>
					<div style="overflow-x: scroll; overflow-y: scroll">
						<asp:GridView ID="GridView1" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" CssClass="grid" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="I_BULLETINID" runat="server">
<EmptyDataTemplate>
								<table class="grid" cellspacing="0" rules="all" border="1" id="GridView1" style="border-collapse: collapse;">
									<tr class="grid_head">
										<th scope="col" style="width: 25px;">
											序号
										</th>
										<th scope="col" style="width: 150px;">
											公告标题
										</th>
										
										<th scope="col">
											发布范围
										</th>
										<th scope="col" style="width: 70px;">
											发布人
										</th>
										<th scope="col" style="width: 70px;">
											发布时间
										</th>
										<th scope="col" style="width: 80px;">
											流程状态
										</th>
									</tr>
								</table>
							</EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head" Font-Bold="false"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="公告标题">
<ItemTemplate>
										<span class="tooltip" title='<%# Eval("V_TITLE") %>'><a href="BulletinLock.aspx?rid=<%# Eval("I_BULLETINID") %>&ic=<%# Eval("I_BULLETINID") %>"
											target="_blank">
											<%# StringUtility.GetStr(Eval("V_TITLE").ToString(), 25) %>
										</a></span>
									</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="V_TITLE" HeaderText="公告标题" Visible="false" /><asp:TemplateField HeaderText="发布范围" SortExpression="CorpCode"><ItemTemplate>
										<span class="tooltip" title='<%# BulletionAction.ReturnCropName(Eval("CorpCode").ToString()) %>'>
											<%# StringUtility.GetStr(BulletionAction.ReturnCropName(Eval("CorpCode").ToString()), 25) %>
										</span>
									</ItemTemplate><EditItemTemplate>
										<asp:TextBox ID="TextBox1" Text='<%# System.Convert.ToString(Eval("CorpName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
									</EditItemTemplate></asp:TemplateField><asp:BoundField DataField="V_RELEASEUSER" HeaderText="发布人" SortExpression="V_RELEASEUSER" /><asp:BoundField DataField="DTM_RELEASETIME" DataFormatString="{0:yyyy-MM-dd}" HeaderText="发布时间" HtmlEncode="false" SortExpression="DTM_RELEASETIME" /><asp:TemplateField HeaderText="流程状态">
<ItemTemplate>
										<%# Common2.GetState(Eval("AuditState").ToString()) %>
									</ItemTemplate>
</asp:TemplateField></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
					</div>
				</td>
			</tr>
		</table>
		<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	</div>
	</form>
</body>
</html>
