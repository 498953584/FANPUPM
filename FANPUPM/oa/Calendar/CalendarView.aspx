<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalendarView.aspx.cs" Inherits="oa_Calendar_CalendarView" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>电子日程</title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self"/>
</head>
<body>
    <form id="form1" runat="server">
     <div>
    <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
				<tr>
					<td class="td-title" align="center" colspan="2">日程安排
					</td>
				</tr>
				<tr>
					<td class="td-label" width="10%">
                        添加日期:</td>
					<td width="23%">
						<asp:Label ID="lbRecordDate" ReadOnly="True" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td class="td-label">标题:
					</td>
					<td><asp:Label ID="lbTitle" Width="200px" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td class="td-label">
                        详细内容:</td>
					<td><asp:Label ID="lbContent" TextMode="MultiLine" Rows="5" Width="200px" runat="server"></asp:Label></td>
				</tr>
				<tr>
				    <td class="td-label">
                        添加人:</td>
				    <td>
                        <asp:Label ID="lbUserName" runat="server"></asp:Label>
                        <asp:HiddenField ID="HdnUserCode" runat="server" />
                    </td>
				 </tr>
				<tr>
					<td class="td-label">
                        是否提醒:</td>
					<td><asp:CheckBox ID="CBIsRemind" Text="是否提醒" Enabled="false" runat="server" /></td>
				</tr>
				<tr>
					<td  align="center" colspan="2" class="td-submit"><input id="BtnClose" onclick="window.close();" type="button" value="关 闭"/></td>
				</tr>
			</table>
    </div>
    </form>
</body>
</html>
