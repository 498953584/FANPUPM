<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConferenceDataSend.aspx.cs" Inherits="ConferenceDataSend" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title></title>
	<script language="javascript">
	<!--
	function ClickRow(recordId,state)
	{
	    document.getElementById('hdnRecordID').value = recordId;
        document.getElementById('TemplateList').src = "SendDataList.aspx?mid=" + recordId;
	}
	-->
	</script>
</head>
<body class="body_frame" scroll="no" >
<form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <table id="table1" border="1" cellpadding="0" cellspacing="0" class="table-form" width="100%" height="100%">
        <tr>
            <td class="td-title" height="24">参与的会议记录列表</td>
        </tr>
        <tr id="btnTr" height="24" runat="server"><td style="height: 24px" class="td-search" runat="server">&nbsp;&nbsp;召开日期
                <JWControl:DateBox ID="txtBeginDate" runat="server"></JWControl:DateBox>至
                <JWControl:DateBox ID="txtEndDate" runat="server"></JWControl:DateBox>
                &nbsp;&nbsp;按
				<asp:DropDownList ID="ddlClass" Width="100px" AutoPostBack="false" runat="server"><asp:ListItem Value="MeetingTitle" Selected="true" Text="会议名称" /><asp:ListItem Value="MeetingPlace" Text="会议地点" /><asp:ListItem Value="Content" Text="备注" /></asp:DropDownList>
				键值
				<asp:TextBox ID="tbKey" Width="120px" runat="server"></asp:TextBox>
				<asp:Button ID="btnSearch" CssClass="button-normal" OnClick="btnSearch_Click" runat="server" />
				<input type="hidden" id="hdnRecordID" name="hdnRecordID" style="WIDTH: 10px" runat="server" />

            </td></tr>
        <tr>
            <td  vAlign="top" height="40%">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                <asp:SqlDataSource ID="SqlMeetingInfo" ConnectionString='<%$ ConnectionStrings:Sql %>' SelectCommand='<%# Convert.ToString(base.GetSelectCommandText()) %>' runat="server"><SelectParameters><asp:SessionParameter Name="UserCode" SessionField="yhdm" Type="String"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
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
                    </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Command" runat="server" /><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /></Triggers></asp:UpdatePanel>
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
