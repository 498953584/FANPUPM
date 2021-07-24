<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendDataList.aspx.cs" Inherits="SendDataList" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title></title>
	<script language="javascript">
	<!--
	function ClickRow(op,recordId)
	{
	    if (op=="0")
	    {
	        document.getElementById('hdnRecordId').value = recordId;
	    }
	    else
	    {
            document.getElementById('btnEdit').disabled = false;
	        document.getElementById('btnDel').disabled = false;
	        document.getElementById('hdnTemplateID').value = recordId;
	    }
	}
	function openTopicEdit(op,meetingInfoId)
	{
	    var recordId = 0;
		if (op==2)
		{
		    recordId = document.getElementById('hdnTemplateID').value;
		}
		var url= "TopicEdit.aspx?t=" + op + "&mid=" + meetingInfoId + "&rid="+ recordId;
		return window.showModalDialog(url,window,"dialogHeight:140px;dialogWidth:400px;center:1;help:0;status:0;");
	}
	function UpFile(t,RecordCode)
	{
	    //t=4 为会议管理
	    //var RecordCode = document.getElementById('hdnRecordId').value;//编号
		var url = "../../CommonWindow/Annex/AnnexList.aspx?mid="+t+"&rc="+RecordCode+"&at=0&type=2";			
		window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
	}
	-->
	</script>
</head>
<body class="body_frame" scroll="no" >
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <div>
    <table id="table1" border="1" cellpadding="0" cellspacing="0" class="table-form" width="100%">
    <tr><td style="height: 41px">
        <iewc:TabStrip ID="TabStrip1" TargetID="MultiPage1" TabDefaultStyle="background-image:url(../../../images/2_2.gif);border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 1px;font-family:verdana;font-weight:bold;font-size:12px;color:#000000;width:120;height:20;text-align:center;" TabHoverStyle="background-color:#777777;" TabSelectedStyle="background-image:url(../../../images/2_1.gif);color:#000000;border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 0px;width:120;height:20;font-size:12px;text-align:center;" SepDefaultStyle="border-top:#999AB5 solid 0px;border-left:#999AB5 solid 0px;border-right:#999AB5 solid 0px;border-bottom:#999AB5 solid 1px;" TabIndex="-1" runat="server"><Items><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="会议资料上报" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="会议议题上报" runat="server"></iewc:Tab><iewc:TabSeparator DefaultStyle="width:100%;" runat="server"></iewc:TabSeparator></Items></iewc:TabStrip>
        </td>
    </tr>
    <tr>
        <td vAlign="top">
            <iewc:MultiPage ID="MultiPage1" Height="100%" runat="server"><iewc:PageView runat="server">
                    <table id="table2" border="1" cellpadding="0" cellspacing="0" class="table-form" width="100%">
                        <tr>
                            <td class="td-head">会议资料列表</td>
                        </tr>
                        <tr id="btnTr" height="24" runat="server"><td class="td-toolsbar" runat="server">
					        <input type="hidden" id="hdnRecordId" name="hdnRecordId" style="WIDTH: 10px" runat="server" />

					        <asp:Button ID="btnAnnex" style="WIDTH: 100px" Text="资料上传..." OnClick="btnAnnex_Click" runat="server" />
						        </td></tr>
                        <tr>
                            <td>
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
                                    <asp:SqlDataSource ID="SqlConferenceData" SelectCommand="SELECT [AnnexCode], [HumanCode],(select v_xm from pt_yhmc where v_yhdm = HumanCode) as HumanName,[OriginalName],[AddDate] FROM XPM_Basic_AnnexList WHERE ([RecordCode] = @MeetingInfoID)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="MeetingInfoID" QueryStringField="mid" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                                </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnAnnex" EventName="Command" runat="server" /></Triggers></asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </iewc:PageView><iewc:PageView runat="server">
                    <table id="table3" border="1" cellpadding="0" cellspacing="0" class="table-form" width="100%">
                        <tr>
                            <td class="td-head">上传会议议题列表</td>
                        </tr>
                        <tr id="btnTr1" height="24" runat="server"><td class="td-toolsbar" runat="server">
						        <input type="hidden" id="hdnTemplateID" name="hdnTemplateID" style="WIDTH: 10px" runat="server" />

						        <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
						        <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
						        <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" /></td></tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                                    <asp:SqlDataSource ID="SqlTopic" SelectCommand="SELECT [RecordID], [MeetingInfoID], [UserCode],(select v_xm from pt_yhmc where v_yhdm = UserCode) as UserName, [RecordDate], [CorpCode],(select CorpName from PT_d_CorpCode b where b.CorpCode = a.CorpCode) as CorpName, [Topic], [Content] FROM [OA_Meeting_Topic] a WHERE ([MeetingInfoID] = @MeetingInfoID)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="MeetingInfoID" QueryStringField="mid" Type="Int32"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                                    <asp:GridView ID="gvTopic" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlTopic" Width="100%" OnRowDataBound="gvTopic_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                                                <table class="grid" cellspacing="0" rules="all" border="1" id="gvTopic" style="border-collapse:collapse;">
                                                    <tr class="grid_head">
                                                        <th scope="col" style="width:10%">序号</th><th scope="col">所属公司</th><th scope="col" style="width:15%">上报人</th><th scope="col" style="width:25%">议题名称</th><th scope="col" style="width:10%">上报时间</th><th scope="col" style="width:15%">备注</th>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>  
                                                    </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="CorpName" HeaderText="所属公司" SortExpression="CorpName" /><asp:BoundField DataField="UserName" HeaderText="上报人" SortExpression="UserName" /><asp:BoundField DataField="Topic" HeaderText="议题名称" SortExpression="Topic" /><asp:BoundField DataField="RecordDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="上报时间" HtmlEncode="false" SortExpression="RecordDate" /><asp:BoundField DataField="Content" HeaderText="备注" SortExpression="Content" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                                    </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Command" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Command" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Command" runat="server" /></Triggers></asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </iewc:PageView></iewc:MultiPage>
        </td>
    </tr>
    </table>
    </div>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
