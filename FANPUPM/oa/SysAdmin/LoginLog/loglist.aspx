<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loglist.aspx.cs" Inherits="LogList" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>登录信息列表</title><link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		//选择人员
		function selectUser() {
			jw.selectOneUser({ codeinput: 'hdnPeople', nameinput: 'txtPeople' });
		}
	</script>
</head>
<body>
	<form id="Form1" method="post" runat="server">
	<font face="宋体">
		<table border="0" cellpadding="0" cellspacing="0" width="100%">
			<tr style="height: 20px;">
				<td class="divHeader">
					<b>
						<asp:Label ID="Label1" runat="server">Label</asp:Label></b>
				</td>
			</tr>
			<tr>
				<td>
					<table>
						<tr>
							<td>
								日期：
							</td>
							<td>
								<asp:TextBox ID="mindate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>-
								<asp:TextBox ID="maxdate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
							</td>
							<td>
								选择人员：
							</td>
							<td>
								<span id="span1" class="spanSelect" style="width: 117px; background-color: #FEFEF4;">
									<input type="text" style="width: 90px; background-color: #FEFEF4; height: 15px; border: none;
										line-height: 16px; margin: 1px 0px 1px 2px" id="txtPeople" runat="server" />

									<img id="Img1" src="~/images/icon.bmp" style="float: right;" onclick="selectUser();" alt="选择人员" runat="server" />

									<input id="hdnPeople" type="hidden" name="hdnPeople" runat="server" />

								</span>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td class="divFooter" style="text-align: left">
					<asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="BtnQuery_Click" runat="server" />
					<asp:Button ID="btnexecl" CssClass="button-normal" Text="导出Excel" Width="80px" OnClick="btnexecl_Click" runat="server" />
				</td>
			</tr>
			<tr>
				<td>
					<asp:DataGrid ID="dgLogList" AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center" CellPadding="4" PageSize="20" AllowPaging="true" CssClass="gvdata" runat="server"><AlternatingItemStyle CssClass="rowb" HorizontalAlign="Center"></AlternatingItemStyle><ItemStyle HorizontalAlign="Center" CssClass="rowa"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="header"></HeaderStyle><FooterStyle HorizontalAlign="Right" CssClass="footer"></FooterStyle><Columns><asp:BoundColumn DataField="i_rzid" HeaderText="日志序号"></asp:BoundColumn><asp:BoundColumn DataField="v_xm" HeaderText="登录人姓名"></asp:BoundColumn><asp:BoundColumn DataField="v_dlip" HeaderText="登录IP"></asp:BoundColumn><asp:BoundColumn DataField="dtm_dlsj" HeaderText="登录时间"></asp:BoundColumn></Columns><PagerStyle NextPageText="下一页" PrevPageText="上一页" HorizontalAlign="Right" Position="TopAndBottom" Wrap="false" Mode="NumericPages"></PagerStyle></asp:DataGrid>
				</td>
			</tr>
			<tr>
				<td align="right">
					<asp:Button ID="btnDel" Text=" 删除 " CssClass="button-normal" OnClick="btnDel_Click" runat="server" />
					<asp:TextBox ID="tbDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
					以前的记录
				</td>
			</tr>
		</table>
	</font>
	</form>
</body>
</html>
