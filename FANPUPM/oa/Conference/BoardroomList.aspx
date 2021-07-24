<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BoardroomList.aspx.cs" Inherits="BoardroomList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title></title>
	<script language="javascript" type="text/javascript">
	<!--
	function ClickRow(FlowGuid)
	{
		document.getElementById('hdnRecordID').value = FlowGuid;
		document.getElementById('btnEdit').disabled = false;
		document.getElementById('btnDel').disabled = false;	
	}
	
	function openWin(op,corpcode)
	{
		var RID = 0;
		if (op == "edit")
		{
		    RID = document.getElementById('hdnRecordID').value;
		}
		var url= "BoardroomEdit.aspx?op=" + op + "&mid=" + RID + "&code=" + corpcode;
		var rtnValue = window.showModalDialog(url,window,"dialogHeight:500px;dialogWidth:650px;center:1;help:0;status:0;");
		if (Boolean(rtnValue)) 
		{
		    document.getElementById("btnRefresh").click();
		}
	}
	-->
</script>
</head>
<body class="body_frame" scroll="no">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <div>
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0" class="table-normal">
            <tr>
                <td class="td-title">
                    会议室信息</td>
            </tr>
            <tr>
                <td class="td-toolsbar" style="height: 24px;">
                    <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                    <asp:Button ID="btnRefresh" Text="刷 新" style="display:none" OnClick="btnRefresh_Click" runat="server" />
                    <asp:HiddenField ID="hdnRecordID" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                    <asp:GridView ID="GridView1" AllowPaging="true" AutoGenerateColumns="false" DataSourceID="sqlBoardroom" PageSize="20" CssClass="grid" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                            <table class="grid" cellspacing="0" rules="all" border="1" id="GridView1" style="border-collapse:collapse;">
		                        <tr class="grid_head">
			                        <th scope="col" style="width:40px;">序号</th><th scope="col">会议室名称</th><th scope="col" style="width:70px;">位置</th><th scope="col" style="width:120px;">容纳人数</th><th scope="col" style="width:70px;">管理员姓名</th><th scope="col" style="width:70px;">联系方式</th><th scope="col" style="width:80px;">备注</th>
		                        </tr>
	                        </table>
                        </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>  
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="MeetingRoom" HeaderText="会议室名称" SortExpression="MeetingRoom" /><asp:BoundField DataField="Location" HeaderText="位置" SortExpression="Location" /><asp:BoundField DataField="HumanNumber" HeaderText="容纳人数" SortExpression="HumanNumber" /><asp:BoundField DataField="ManagerName" HeaderText="管理员姓名" SortExpression="ManagerName" /><asp:BoundField DataField="RelationMode" HeaderText="联系方式" HtmlEncode="false" SortExpression="RelationMode" /><asp:BoundField DataField="Content" HeaderText="备注" SortExpression="Content" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                    <asp:SqlDataSource ID="sqlBoardroom" SelectCommand="SELECT [RecordID], [MeetingRoom], [CorpCode], [Location], [HumanNumber], [ManagerCode],(select v_xm from pt_yhmc where v_yhdm = ManagerCode ) as ManagerName, [RelationMode], [Content], [IsValid] FROM [OA_MeetingRoom_Info] WHERE ([CorpCode] = @CorpCode)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter DefaultValue="00" Name="CorpCode" QueryStringField="code" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                        </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
            </tr>
        </table>
         <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </div>
    </form>
</body>
</html>
