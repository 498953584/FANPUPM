<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectRoomList.aspx.cs" Inherits="SelectRoomList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title></title>
	<script language="javascript">
	<!--
	function ClickRow(roomId,room)
	{
		document.getElementById('hdnMeetingRoomID').value = roomId;
		document.getElementById("hdnMeetingRoom").value = room;
	}
	function confirmselect()
	{
		var meetingroom = window.dialogArguments;
		meetingroom[0] = "";
		meetingroom[1] = "";
		meetingroom[0] = document.getElementById('hdnMeetingRoomID').value;
		meetingroom[1] = document.getElementById("hdnMeetingRoom").value;
		window.returnvalue= meetingroom;
		window.close();
	}
	-->
	</script>
</head>
<body class="body_frame" scroll="no">
    <form id="Form1" method="post" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <table id = "table1" border ="1" cellpadding="0" cellspacing="0" width="100%" height="100%" class="table-form">
        <tr>
            <td class="td-title"  height="27px">会议室信息
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                <asp:GridView ID="GridView1" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlMeetingRoom" Width="100%" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                        <table class="grid" cellspacing="0" rules="all" border="1" id="GridView1" style="border-collapse:collapse;">
	                        <tr class="grid_head">
		                        <th scope="col" style="width:10%;">序号</th><th scope="col">会议室名称</th><th scope="col" style="width:20%;">容纳人数</th><th scope="col" style="width:30%;">备注</th>
	                        </tr>
                        </table>
                    </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="MeetingRoom" HeaderText="会议室名称" SortExpression="MeetingRoom" /><asp:BoundField DataField="HumanNumber" HeaderText="容纳人数" SortExpression="HumanNumber" /><asp:BoundField DataField="Content" HeaderText="备注" SortExpression="Content" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                <asp:SqlDataSource ID="SqlMeetingRoom" SelectCommand="SELECT [RecordID], [MeetingRoom], [CorpCode], [Location], [HumanNumber], [ManagerCode], [RelationMode], [Content], [IsValid] FROM [OA_MeetingRoom_Info] WHERE ([CorpCode] = @CorpCode)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter DefaultValue="00" Name="CorpCode" QueryStringField="code" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                    </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /><asp:AsyncPostBackTrigger ControlID="BtnSave" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
            </td>
        </tr>
         <tr>
            <TD class="td-submit" height="20"><asp:Button ID="BtnSave" Text="保  存" OnClick="BtnSave_Click" runat="server" />&nbsp;
		            <INPUT id="BtnClose" onclick="javascript:window.close();" type="button" value="关  闭" name="BtnClose">
		            <input id="hdnMeetingRoom" style="WIDTH: 10px" type="hidden" name="hdnMeetingRoom" runat="server" />

		            <input id="hdnMeetingRoomID" style="WIDTH: 10px" type="hidden" name="hdnMeetingRoomID" runat="server" />

	            </TD>
        </tr>
     </table>
     <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
