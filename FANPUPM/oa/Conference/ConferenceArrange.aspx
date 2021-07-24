<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConferenceArrange.aspx.cs" Inherits="ConferenceArrange" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title></title>
	<script language="javascript">
	<!--
	function ClickRow(recordId,state)
	{
	    document.getElementById('hdnRecordID').value = recordId;
	    if (state != "0") //已召开和未召开的
		{
		    document.getElementById('btnEdit').disabled = true;
            document.getElementById('btnDel').disabled = true;
            document.getElementById('btnLaunch').disabled = true;
            document.getElementById('btnPutOff').disabled = true;
            document.getElementById('btnCancel').disabled = true;
		}
		else //未提交的
		{
	        document.getElementById('btnEdit').disabled = false;
            document.getElementById('btnDel').disabled = false;
            document.getElementById('btnLaunch').disabled = false;
            document.getElementById('btnPutOff').disabled = false;
            document.getElementById('btnCancel').disabled = false;
        }
//        document.getElementById('TemplateList').src = "ConferenceSubsection.aspx?mid=" + recordId;
        document.getElementById('TemplateList').src = "ConferenceArrangeFraeList.aspx?mid=" + recordId;
	}
	function openArrangeEdit(op,userCode)
	{
	    var recordId = 0;
	    if (op != 1)
	    {
	        recordId = document.getElementById('hdnRecordID').value;
	    }
	    var url = "ArrangeEdit.aspx?t=" + op + "&rid=" + recordId + "&code=" + userCode ;
	    return window.showModalDialog(url,window,"dialogHeight:200px;dialogWidth:400px;center:1;help:0;status:0;");
	}
	function addFromTemplate()
	{
	    var url = "AddFromTemplate.aspx?flt=meeting";
	    window.showModalDialog(url,window,"dialogHeight:400px;dialogWidth:500px;center:1;help:0;status:0;");
	}
	-->
	</script>
</head>
<body class="body_frame" scroll="no" >
<form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <table id="table1" border="1" cellpadding="0" cellspacing="0" class="table-form" width="100%" height="100%">
        <tr>
            <td class="td-title" height="24">会议安排</td>
        </tr>
        <tr id="btnTr" height="24" runat="server"><td class="td-toolsbar" runat="server">
		        <input type="hidden" id="hdnRecordID" name="hdnRecordID" style="WIDTH: 10px" runat="server" />

		        <asp:Button ID="btnFromTemplate" Text="从模板增加" Width="80px" OnClick="btnFromTemplate_Click" runat="server" />
		        <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
		        <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
		        <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
		        <asp:Button ID="btnLaunch" Text="发 起" Enabled="false" OnClick="btnLaunch_Click" runat="server" />
		        <asp:Button ID="btnPutOff" Text="推 迟" Enabled="false" OnClick="btnPutOff_Click" runat="server" />
		        <asp:Button ID="btnCancel" Text="取 消" Enabled="false" OnClick="btnCancel_Click" runat="server" /></td></tr>
        <tr>
            <td  vAlign="top" height="40%">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                <asp:GridView ID="gvMeetinInfo" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlMeetingInfo" Width="100%" OnRowDataBound="gvMeetinInfo_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                        <table class="grid" cellspacing="0" rules="all" border="1" id="gvMeetinInfo" style="border-collapse:collapse;">
                            <tr class="grid_head">
                                <th scope="col" style="width:5%">序号</th><th scope="col">会议名称</th><th scope="col" style="width:20%">会议地点</th><th scope="col" style="width:10%">召开日期</th><th scope="col" style="width:8%">开始时间(时)</th><th scope="col" style="width:8%">开始时间(分)</th><th scope="col" style="width:8%">状态</th><th scope="col" style="width:15%">备注</th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>  
                            </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="MeetingTitle" HeaderText="会议名称" SortExpression="MeetingTitle" /><asp:BoundField DataField="MeetingPlace" HeaderText="会议地点" SortExpression="MeetingPlace" /><asp:BoundField DataField="CallDate" HeaderText="召开日期" SortExpression="CallDate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="CallTime" HeaderText="开始时间(时)" SortExpression="CallTime" /><asp:BoundField DataField="CallMinute" HeaderText="开始时间(分)" SortExpression="CallMinute" /><asp:BoundField DataField="StateName" HeaderText="状态" SortExpression="StateName" /><asp:BoundField DataField="Content" HeaderText="备注" SortExpression="Content" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                <asp:SqlDataSource ID="SqlMeetingInfo" SelectCommand="SELECT [RecordID], [UserCode], [RecordDate], [MeetingTitle], [MeetingPlace], [CallDate], [CallTime], [CallMinute], [Content], [State],(case State when 0 then '未发起' when 1 then '未召开' when 2 then '已召开' end) as StateName, [PigeonholeState], [SummaryName], [FilePath] FROM [OA_Meeting_Info] WHERE ([UserCode] = @UserCode)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:SessionParameter Name="UserCode" SessionField="yhdm" Type="String"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
                    </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnFromTemplate" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnLaunch" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnPutOff" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /></Triggers></asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td vAlign="top"><iframe id="TemplateList" name="TemplateList" src="" frameBorder="0" width="100%" scrolling="no" height="100%"></iframe></td>
        </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
