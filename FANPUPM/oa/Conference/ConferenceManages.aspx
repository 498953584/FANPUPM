<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConferenceManages.aspx.cs" Inherits="ConferenceManages" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title></title>
	<script language="javascript">
	<!--
	function ClickRow(recordId,state,pigestate)
	{
	    document.getElementById('hdnRecordID').value = recordId;
	    if (state != "2" || pigestate == "1") //未召开或已归档
		{
		    document.getElementById('btnSendSummary').disabled = true;
            document.getElementById('btnPigeOnHole').disabled = true;
		}
		else 
		{
	        document.getElementById('btnSendSummary').disabled = false;
            document.getElementById('btnPigeOnHole').disabled = false;
        }
	}
	function sendsummary()
	{
	    var meetingInfoId = document.getElementById('hdnRecordID').value;
	    var url = "SendSummary.aspx?mid=" + meetingInfoId;
	    window.showModalDialog(url,window,"dialogHeight:140px;dialogWidth:400px;center:1;help:0;status:0;");
	}
	function download(filepath,OriginalName)
	{
	    var url = "../../EPC/uploadfile/down.aspx?fileName=" + OriginalName + "&filePath=" + filepath ;
        window.location.href = url;
        //window.open(url,'','height=1,width=1,left=500,top=300');
	}
	-->
	</script>
</head>
<body class="body_frame" scroll="no" >
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <table id="table1" border="1" cellpadding="0" cellspacing="0" class="table-form" width="100%" height="100%">
        <tr>
            <td class="td-title" height="24">会议管理</td>
        </tr>
        <tr id="btnTr" height="24" runat="server"><td class="td-toolsbar" runat="server">
		        <input type="hidden" id="hdnRecordID" name="hdnRecordID" style="WIDTH: 10px" runat="server" />

		        <input type="hidden" id="hdnFilePath" name="hdnFilePath" style="WIDTH: 10px" runat="server" />

		        <input type="hidden" id="hdnFileName" name="hdnFileName" style="WIDTH: 10px" runat="server" />

		        <asp:Button ID="btnSendSummary" Text="上传纪要" Enabled="false" OnClick="btnSendSummary_Click" runat="server" />
		        <asp:Button ID="btnPigeOnHole" Text="归 档" Enabled="false" OnClick="btnPigeOnHole_Click" runat="server" /></td></tr>
        <tr>
            <td  vAlign="top" height="40%">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                <asp:GridView ID="gvMeetinInfo" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlMeetingInfo" Width="100%" OnRowDataBound="gvMeetinInfo_RowDataBound" OnSelectedIndexChanged="gvMeetinInfo_SelectedIndexChanged" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                        <table class="grid" cellspacing="0" rules="all" border="1" id="gvMeetinInfo" style="border-collapse:collapse;">
                            <tr class="grid_head">
                                <th scope="col" style="width:5%">序号</th><th scope="col">会议名称</th><th scope="col" style="width:20%">会议地点</th><th scope="col" style="width:10%">召开日期</th><th scope="col" style="width:8%">开始时间(时)</th><th scope="col" style="width:8%">开始时间(分)</th><th scope="col" style="width:8%">状态</th><th scope="col" style="width:10%">会议纪要</th><th scope="col" style="width:8%">归档状态</th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:CommandField ShowSelectButton="true" /><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>  
                            </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="MeetingTitle" HeaderText="会议名称" SortExpression="MeetingTitle" /><asp:BoundField DataField="MeetingPlace" HeaderText="会议地点" SortExpression="MeetingPlace" /><asp:BoundField DataField="CallDate" HeaderText="召开日期" SortExpression="CallDate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="CallTime" HeaderText="开始时间(时)" SortExpression="CallTime" /><asp:BoundField DataField="CallMinute" HeaderText="开始时间(分)" SortExpression="CallMinute" /><asp:BoundField DataField="StateName" HeaderText="状态" SortExpression="StateName" /><asp:ButtonField DataTextField="SummaryName" Text="选择" HeaderText="会议纪要" CommandName="ShowCommand" ButtonType="Link" /><asp:BoundField DataField="PigeonholeStateName" HeaderText="归档状态" SortExpression="PigeonholeStateName" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                <asp:SqlDataSource ID="SqlMeetingInfo" SelectCommand="SELECT [RecordID], [UserCode], [RecordDate], [MeetingTitle], [MeetingPlace], [CallDate], [CallTime], [CallMinute], [Content], [State],(case State when 0 then '未发起' when 1 then '未召开' when 2 then '已召开' end) as StateName, [PigeonholeState],(case PigeonholeState when 0 then '未归档' when 1 then '已归档' end) as PigeonholeStateName, [SummaryName], [OriginalName], [FilePath] FROM [OA_Meeting_Info] WHERE ([State] > 0) AND ([UserCode] = @UserCode)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:SessionParameter Name="UserCode" SessionField="yhdm" Type="String"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
                    </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnPigeOnHole" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnSendSummary" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td vAlign="top">
                <table id="table2" border="1" cellpadding="0" cellspacing="0" class="table-form" width="100%" height="100%">
                    <tr>
                        <td class="td-head" height="24">上传资料列表</td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
                            <asp:GridView ID="gvConferenceData" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlConferenceData" Width="100%" OnRowDataBound="gvConferenceData_RowDataBound" DataKeyNames="AnnexCode" runat="server">
<EmptyDataTemplate>
                                            <table class="grid" cellspacing="0" rules="all" border="1" id="gvConferenceData" style="border-collapse:collapse;">
                                                <tr class="grid_head">
                                                    <th scope="col" style="width:10%">序号</th><th scope="col" style="width:20%">上报人</th><th scope="col">资料名称</th><th scope="col" style="width:15%">上报时间</th>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>  
                                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="HumanName" HeaderText="上报人" SortExpression="HumanName" /><asp:BoundField DataField="OriginalName" HeaderText="资料名称" SortExpression="OriginalName" /><asp:BoundField DataField="AddDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="上报时间" HtmlEncode="false" SortExpression="AddDate" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                                    <asp:SqlDataSource ID="SqlConferenceData" SelectCommand="SELECT [AnnexCode], [HumanCode],(select v_xm from pt_yhmc where v_yhdm = HumanCode) as HumanName,[OriginalName],[AddDate] FROM XPM_Basic_AnnexList WHERE ([RecordCode] = @MeetingInfoID)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:ControlParameter ControlID="hdnRecordID" Name="MeetingInfoID" PropertyName="value" runat="server" /></SelectParameters></asp:SqlDataSource>
                                </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /></Triggers></asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
