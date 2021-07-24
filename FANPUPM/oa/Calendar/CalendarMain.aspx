<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalendarMain.aspx.cs" Inherits="oa_Calendar_CalendarMain" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>电子日程</title>
	<script type="text/javascript">
	    //错误方法
		function openwin1(url, uid, dt, flag) {
			url += '?t=Add&dt=' + dt;
			var r = window.showModalDialog(url, window, "dialogHeight:260px;dialogWidth:550px;center:1;help:0;status:0;");
			if (r) {
				window.location.reload(true);
			}
        }
		function openwin(url, uid, dt, flag) {
			top.ui._calendaradd = window;
			var title = '新增日程';
			var url = '/oa/Calendar/CalendarAdd.aspx?t=Add&dt=' + dt;
			top.ui.openWin({ title: title, url: url });
		}

		function openwinSel(url, uid, dt, flag) {
			url += '?dt=' + dt; ;
			var r = window.showModalDialog(url, window, "dialogHeight:560px;dialogWidth:650px;center:1;help:0;status:0;");
			if (r) {
				window.location.reload(true);
			}
		}

		function openCalendarView(rid) {
			var url = "CalendarView.aspx?rid=" + rid;
			return window.showModalDialog(url, window, "dialogHeight:260px;dialogWidth:350px;center:1;help:0;status:0;");
		}
	</script>
</head>
<body class="body_clear" scroll="no">
	<form id="form1" runat="server">
	<div>
		<table cellspacing="0" cellpadding="0" width="100%" align="center" border="0" class="table-normal"
			height="100%">
			<tr>
				<td valign="top" style="width: 232px; height: 232px">
					<table width="210" align="center" bgcolor="white" border="1">
						<tr>
							<td style="height: 202px">
								<table id="TbContent" width="210" bgcolor="#ffffff" border="0">
									<tr bgcolor="silver" height="18">
										<td valign="middle" align="left" bgcolor="#cccccc" colspan="9" height="18">
											<table id="tbTitleContent" width="99%" border="0">
												<tr>
													<td width="7%">
														<font face="宋体">
															<asp:ImageButton ID="ImbMonth" ImageUrl="images/prev.gif" OnClick="ImbMonth_Click" runat="server" /></font>
													</td>
													<td width="10%">
														<font face="宋体">
															<asp:ImageButton ID="ImbYear" ImageUrl="images/prev1.gif" OnClick="ImbYear_Click" runat="server" /></font>
													</td>
													<td id="tdDate" align="center" width="72%">
														<asp:Label ID="LbYear" runat="server"></asp:Label><font face="宋体">年</font>
														<asp:Label ID="LbMonth" runat="server"></asp:Label><font face="宋体">月</font>
													</td>
													<td width="10%">
														<font face="宋体">
															<asp:ImageButton ID="ImbYearAdd" ImageUrl="images/next1.gif" OnClick="ImbYearAdd_Click" runat="server" /></font>
													</td>
													<td width="7%">
														<asp:ImageButton ID="ImbMonthAdd" ImageUrl="images/next.gif" OnClick="ImbMonthAdd_Click" runat="server" />
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td align="center" colspan="7" height="1">
											<img height="1" src="images/line.gif" width="210" border="0">
										</td>
									</tr>
									<tr bgcolor="cornflowerblue">
										<td class="SOME" valign="bottom" align="right" width="30" bgcolor="cornflowerblue"
											height="15">
											<font color="red">日</font>
										</td>
										<td class="SOME" valign="bottom" align="right" width="30" bgcolor="cornflowerblue"
											height="15">
											一
										</td>
										<td class="SOME" valign="bottom" align="right" width="30" bgcolor="cornflowerblue"
											height="15">
											二
										</td>
										<td class="SOME" valign="bottom" align="right" width="30" height="15">
											三
										</td>
										<td class="SOME" valign="bottom" align="right" width="30" height="15">
											四
										</td>
										<td class="SOME" valign="bottom" align="right" width="30" height="15">
											五
										</td>
										<td class="SOME" valign="bottom" align="right" width="30" height="15">
											<font color="red">六</font>
										</td>
									</tr>
									<tr>
										<td colspan="7">
											<table id="tbDay" width="100%" border="0" runat="server"></table>
										</td>
									</tr>
									<tr>
										<td align="center" colspan="7" height="1">
											<img height="1" src="images/line.gif" width="210" border="0">
										</td>
									</tr>
								</table>
								<table width="210" align="center" bgcolor="#eeeeee" border="0">
									<tr>
										<td align="center">
											<table border="0">
												<tr>
													<td>
														<asp:DropDownList ID="DDLMonth" runat="server"><asp:ListItem Value="1" Text="一月" /><asp:ListItem Value="2" Text="二月" /><asp:ListItem Value="3" Text="三月" /><asp:ListItem Value="4" Text="四月" /><asp:ListItem Value="5" Text="五月" /><asp:ListItem Value="6" Text="六月" /><asp:ListItem Value="7" Text="七月" /><asp:ListItem Value="8" Text="八月" /><asp:ListItem Value="9" Text="九月" /><asp:ListItem Value="10" Text="十月" /><asp:ListItem Value="11" Text="十一月" /><asp:ListItem Value="12" Text="十二月" /></asp:DropDownList>
													</td>
													<td>
														<asp:DropDownList ID="DDLyear" runat="server"></asp:DropDownList>
													</td>
													<td valign="bottom">
														<asp:ImageButton ID="Imbgo" ImageUrl="images/go.gif" OnClick="Imbgo_Click" runat="server" />&nbsp;
													</td>
												</tr>
											</table>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
				<td width="*" rowspan="2">
					<iframe id="frmPage" name="frmPage" src="about:blank" frameborder="0" width="100%" height="100%" runat="server"></iframe>
				</td>
			</tr>
			<tr>
				<td valign="top" style="width: 232px">
					<asp:GridView ID="GridView1" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlCalendar" Width="100%" OnRowDataBound="GridView1_RowDataBound" runat="server">
<EmptyDataTemplate>
							<table id="Table1" border="1" cellspacing="0" class="grid" rules="all" style="width: 100%;
								border-collapse: collapse">
								<tr class="grid_head">
									<th align="center" scope="col">
										待办事件
									</th>
									<th align="center" scope="col" style="width: 70px">
										待办时间
									</th>
								</tr>
							</table>
						</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="待办事件" SortExpression="Title"><EditItemTemplate>
									<asp:TextBox ID="TextBox1" Text='<%# Convert.ToString(Eval("Title")) %>' runat="server"></asp:TextBox>
								</EditItemTemplate>
<ItemTemplate>
									<asp:HyperLink ID="HLtitle" Text='<%# Convert.ToString(Eval("Title")) %>' runat="server"></asp:HyperLink>
								</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="RecordDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="待办时间" HtmlEncode="false" SortExpression="RecordDate" /></Columns><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
					<asp:SqlDataSource ID="SqlCalendar" SelectCommand="SELECT top 10 RecordID,[Title],RecordDate FROM [OA_Calendar_Info] where UserCode = @UserCode and datediff(dd,RecordDate,getdate()) <=0 order by RecordDate asc" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:SessionParameter Name="UserCode" SessionField="yhdm"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
				</td>
				<td>
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
