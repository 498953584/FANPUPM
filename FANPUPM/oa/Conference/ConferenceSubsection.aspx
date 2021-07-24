<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConferenceSubsection.aspx.cs" Inherits="ConferenceSubsection" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title></title>
	<script language="javascript">
	<!--
	function ClickRow(recordId)
	{
	    document.getElementById('hdnRecordID').value = recordId;
        document.getElementById('btnEdit').disabled = false;
        document.getElementById('btnDel').disabled = false;        
	}
	function openSubsectionEdit(op,meetingInfoID)
	{
	    var recordId = 0;
	    if (op == 2)
	    {
	        recordId = document.getElementById('hdnRecordID').value;
	    }
	    var url = "SubsectionEdit.aspx?t=" + op + "&rid=" + recordId + "&mid=" + meetingInfoID ;
	    return window.showModalDialog(url,window,"dialogHeight:200px;dialogWidth:400px;center:1;help:0;status:0;");
	}
	-->
	</script>
</head>
<body class="body_frame" scroll="no" >
<form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <table id="table1" border="1" cellpadding="0" cellspacing="0" class="table-form" width="100%" height="100%">
        <tr>
            <td class="td-title" height="24">会议分段列表</td>
        </tr>
        <tr id="btnTr" height="24" runat="server"><td class="td-toolsbar" runat="server">
		        <input type="hidden" id="hdnRecordID" name="hdnRecordID" style="WIDTH: 10px" runat="server" />

		        <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
		        <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
		        <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" /></td></tr>
        <tr>
            <td  vAlign="top">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                <asp:GridView ID="gvSubsection" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlSubsection" Width="100%" OnRowDataBound="gvSubsection_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                        <table class="grid" cellspacing="0" rules="all" border="1" id="gvMeetinInfo" style="border-collapse:collapse;">
                            <tr class="grid_head">
                                <th scope="col" style="width:5%">序号</th><th scope="col" style="width:8%">开始时间(时)</th><th scope="col" style="width:8%">开始时间(分)</th><th scope="col">会议议题</th><th scope="col" style="width:20%">参会人员</th><th scope="col" style="width:8%">参会人数</th><th scope="col" style="width:8%">是否短信通知</th><th scope="col" style="width:15%">备注</th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>  
                            </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="CallHour" HeaderText="开始时间(时)" SortExpression="CallHour" /><asp:BoundField DataField="CallMinute" HeaderText="开始时间(分)" SortExpression="CallMinute" /><asp:BoundField DataField="Topic" HeaderText="会议议题" SortExpression="Topic" /><asp:BoundField DataField="AttendManNames" HeaderText="参会人员" SortExpression="AttendManNames" /><asp:BoundField DataField="Number" HeaderText="参会人数" SortExpression="Number" /><asp:TemplateField HeaderText="是否短信通知" SortExpression="IsSms">
<ItemTemplate>
                                <asp:CheckBox ID="ckbIsSms" BorderStyle="None" Enabled="false" BorderWidth="0px" Checked='<%# (DataBinder.Eval(Container.DataItem, "IsSms").ToString() == "1") %>' runat="server" />
                            </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="Content" HeaderText="备注" SortExpression="Content" /></Columns></asp:GridView>
                <asp:SqlDataSource ID="SqlSubsection" SelectCommand="SELECT [RecordID], [MeetingInfoID], [CallHour], [CallMinute], [Topic], [AttendManCodes], [AttendManNames], [Number], [IsSms], [Content] FROM [OA_Meeting_AttendMan] WHERE ([MeetingInfoID] = @MeetingInfoID)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="MeetingInfoID" QueryStringField="mid" Type="Int32"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                    </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
