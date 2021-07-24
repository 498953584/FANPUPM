<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userlist.aspx.cs" Inherits="UserList" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server"><title>UserList</title><JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
	<style type="text/css">
		.link
		{
			color: Blue;
			text-decoration: underline;
			cursor: pointer;
		}
	</style>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script language="javascript" type="text/javascript">
	    $(document).ready(function () {
	        $('.edit-priv').click(function () {
	            var tr = jw.getParentUntil(this, 'TR');
	            var url = '/oa/SysAdmin/UserManage/UserPriv.aspx?Op=modPriv&amp&yhdm=' + tr.id;
	            top.ui._userlist = window;
	            toolbox_oncommand(url, "修改权限");
	        });

	        $('.edit').click(function () {
	            var tr = jw.getParentUntil(this, 'TR');
	            var url = '/oa/SysAdmin/UserManage/Broker.aspx?Op=mod&amp&id=' + tr.id;
	            top.ui._userlist = window;
	            top.ui.openWin({ title: '修改', url: url });
	        });

	    });

	    function changeall(id) {
	        var inputs = document.forms[0].elements;
	        for (var i = 0; i < inputs.length; i++) {
	            if (inputs[i].type == 'checkbox') {
	                if (id.checked) {
	                    inputs[i].checked = true;

	                }
	                else {
	                    inputs[i].checked = false;

	                }
	            }
	        }
	    }
	</script>
</head>
<body class="body_frame" scroll="no">
	<form id="Form1" method="post" runat="server">
	<font face="宋体">
		<table class="table-normal" id="tr_Button" cellspacing="0" cellpadding="0" width="100%"
			height="100%" align="center" border="0">
			<tr>
				<td class="td-toolsbar" width="60%" height="30" style="text-align: left;">
					<asp:Label ID="lblCaption" runat="server"></asp:Label>
				</td>
				<td class="td-toolsbar" style="height: 24px">
					<asp:LinkButton ID="lbRecycle" OnClick="lbRecycle_Click" runat="server">冻结账号列表</asp:LinkButton>&nbsp;&nbsp;
					<asp:Button ID="btnSure" Text="启用" Visible="false" OnClick="btnSure_Click" runat="server" />
					<asp:LinkButton ID="lbUserList" Visible="false" OnClick="lbUserList_Click" runat="server">正常用户列表</asp:LinkButton>
				</td>
			</tr>
			<tr>
				<td colspan="2">
					<div id="Box" class="gridBox">
						<asp:DataGrid ID="dgUserList" CellPadding="5" AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center" CssClass="grid" DataKeyField="v_yhdm" runat="server"><SelectedItemStyle BackColor="Maroon"></SelectedItemStyle><AlternatingItemStyle HorizontalAlign="Center"></AlternatingItemStyle><ItemStyle HorizontalAlign="Center" CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="i_xh" HeaderStyle-Width="40px" HeaderText="序号" DataFormatString="{0}"></asp:BoundColumn><asp:BoundColumn HeaderStyle-Width="100px" DataField="v_yhdm" HeaderText="用户代码" DataFormatString="{0}"></asp:BoundColumn><asp:BoundColumn DataField="v_xm" HeaderText="姓名" DataFormatString="{0}"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderStyle-Width="80px"><ItemTemplate>
										<span class="edit-priv link">修改权限</span>
									</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderStyle-Width="60px"><ItemTemplate>
										<span class="edit link">修改</span>
									</ItemTemplate></asp:TemplateColumn><asp:ButtonColumn Text="冻结" CommandName="Delete"></asp:ButtonColumn></Columns></asp:DataGrid>
						<asp:DataGrid ID="dgInvalidUser" Visible="false" CellPadding="5" AutoGenerateColumns="false" Width="100%" CssClass="grid" DataKeyField="v_yhdm" HorizontalAlign="Center" runat="server"><SelectedItemStyle BackColor="Maroon"></SelectedItemStyle><AlternatingItemStyle HorizontalAlign="Center"></AlternatingItemStyle><ItemStyle HorizontalAlign="Center" CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn>
<HeaderTemplate>
										<input id="Checkbox1" type="checkbox" onclick="changeall(this);" />
									</HeaderTemplate>

<ItemTemplate>
										<asp:CheckBox ID="chbSure" runat="server" />
									</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn HeaderText="序号"></asp:BoundColumn><asp:BoundColumn DataField="v_yhdm" HeaderText="用户代码" DataFormatString="{0}"></asp:BoundColumn><asp:BoundColumn DataField="v_xm" HeaderText="姓名" DataFormatString="{0}"></asp:BoundColumn><asp:ButtonColumn Text="启用" CommandName="Delete"></asp:ButtonColumn></Columns></asp:DataGrid></div>
				</td>
			</tr>
		</table>
	</font>
	</form>
</body>
</html>
