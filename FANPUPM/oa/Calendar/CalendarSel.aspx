<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalendarSel.aspx.cs" Inherits="oa_Calendar_CalendarSel" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>电子日程</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript">
	    $(document).ready(function () {
	        $('#btnCalendar').hide();
	        $('#BtnView').removeAttr('display');
	    });

		function ClickRow(rid, RDate, ig) {
			document.getElementById('hdnRecordID').value = rid;
			document.getElementById('HdnRecordDate').value = RDate;
			document.getElementById('HdnInfoGuid').value = ig;
			document.getElementById('btnEdit').disabled = false;
			document.getElementById('btnDel').disabled = false;
			document.getElementById('BtnView').disabled = false;
			document.getElementById('btnCalendar').disabled = false;
		}
		function openCalendarEdit(t) {
			var RDate = document.getElementById('HdnRecordDate').value;
			var rid = document.getElementById('hdnRecordID').value;
			var ig = document.getElementById('HdnInfoGuid').value;
			var title = ''
			if (t == 'Add') {
				var url = '/oa/Calendar/CalendarAdd.aspx?t=' + t + '&dt=' + RDate + '&rid=0';
				title = '新增日程'
			}
			else {
				var url = '/oa/Calendar/CalendarAdd.aspx?t=' + t + '&dt=' + RDate + '&rid=' + rid + '&ig=' + ig;
				title = '编辑日程'
			}
			top.ui._calendaradd = window;
			top.ui.openWin({ title: title, url: url });
			//			return window.showModalDialog(url, window, "dialogHeight:260px;dialogWidth:550px;center:1;help:0;status:0;");
		}
		function openCalendarView() {
			var rid = document.getElementById('hdnRecordID').value;
			var url = "CalendarView.aspx?rid=" + rid;
			return window.showModalDialog(url, window, "dialogHeight:260px;dialogWidth:350px;center:1;help:0;status:0;");
		}

		function openCalendar() {
			var RDate = document.getElementById('HdnRecordDate').value;
			var ig = document.getElementById('HdnInfoGuid').value;
			var rid = document.getElementById('hdnRecordID').value;
			var url = 'CalendarRemindSetAdd.aspx?rid=' + rid + "&ig=" + ig + "&dt=" + RDate;
			return window.showModalDialog(url, window, "dialogHeight:200px;dialogWidth:360px;center:1;help:0;status:0;");
		}
		function getContent(rid) {
			var url = '/oa/Calendar/CalendarContent.aspx?id=' + rid;
			top.ui.openWin({ url: url });
		}
	</script>
</head>
<body class="body_clear" scroll="no">
	<form id="form1" runat="server">
	<div>
		<table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center"
			border="0" class="table-normal">
			<tr>
				<td class="td-title">
					日程列表
				</td>
			</tr>
			<tr>
				<td class="td-toolsbar" style="height: 24px">
					<asp:Button ID="btnCalendar" Enabled="false" Text="提示周期" Width="0px" runat="server" />&nbsp;
					<asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />&nbsp;
					<asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />&nbsp;
					<asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />&nbsp;
					<asp:Button ID="btnClose" Text="关 闭" Style="display: none;" runat="server" />&nbsp;
					<input id="BtnView" disabled="disabled" type="button" value="查   看" onclick="openCalendarView();" />&nbsp;
					<asp:HiddenField ID="HdnRecordDate" runat="server" />
					<asp:HiddenField ID="HdnInfoGuid" runat="server" />
					&nbsp; &nbsp;&nbsp;
					<input id="hdnRecordID" type="hidden" runat="server" />

				</td>
			</tr>
			<tr style="height: 50%" valign="top">
				<td>
					<div id="dvDeptBox" class="div-scroll" style="width: 100%; height: 100%">
						<asp:GridView ID="GridView1" AllowPaging="true" AutoGenerateColumns="false" DataSourceID="sqlBulletin" CssClass="grid" Width="100%" OnRowDataBound="GridView1_RowDataBound" runat="server">
<EmptyDataTemplate>
								<table class="grid" cellspacing="0" rules="all" border="1" id="GridView1" style="border-collapse: collapse;">
									<tr class="grid_head">
										<th scope="col" style="width: 70px;">
											添加日期
										</th>
										<th scope="col">
											标 题
										</th>
										<th scope="col" style="width: 70px;">
											详细内容
										</th>
										<th scope="col" style="width: 120px;">
											是否提醒
										</th>
									</tr>
								</table>
							</EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField DataField="RecordDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="添加日期" SortExpression="RecordDate" HtmlEncode="false" /><asp:BoundField DataField="Title" HeaderText="标 题" SortExpression="Title" /><asp:TemplateField HeaderText="详细内容" SortExpression="Content"><ItemTemplate>
										<a href="javascript:void(0);">
											<asp:Label ID="Label1" Text='<%# Convert.ToString(Eval("Content")) %>' runat="server"></asp:Label></a>
									</ItemTemplate></asp:TemplateField><asp:BoundField DataField="IsRemind" HeaderText="是否提醒" SortExpression="IsRemind" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
					</div>
				</td>
			</tr>
			<tr style="display: none">
				<td>
					&nbsp;<asp:SqlDataSource ID="sqlBulletin" SelectCommand="SELECT [RecordID], [InfoGuid], [UserCode], [RecordDate], [Title], [IsRemind], [Content] FROM [OA_Calendar_Info] WHERE (datediff(dd,[RecordDate] , @RecordDate)=0 AND ([UserCode] = @UserCode))" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="RecordDate" QueryStringField="dt" Type="DateTime"></asp:QueryStringParameter><asp:SessionParameter Name="UserCode" SessionField="yhdm" Type="String"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
				</td>
			</tr>
			<tr>
				<td valign="top">
					
				</td>
			</tr>
		</table>
		<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
		<div id="divCont" style="display: none">
			<asp:Label ID="labCont" Text="Label" runat="server"></asp:Label>
		</div>
	</div>
	</form>
</body>
</html>
