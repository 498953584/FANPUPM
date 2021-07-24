<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BoardroomApply.aspx.cs" Inherits="BoardroomApply" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title></title>
	<script language="javascript" type="text/javascript">
	<!--
	function ClickRow(FlowGuid, State , ManageCode)
	{
		document.getElementById('hdnRecordID').value = FlowGuid;
		document.getElementById('hdnState').value = State;
		document.getElementById('hdnMangeCode').value = ManageCode;
	
		if ( Number(State) > 0)
	    {
            document.getElementById("btnEdit").disabled = true;
		    document.getElementById("btnDel").disabled = true;
		    document.getElementById("btnApply").disabled = true;
            if ( Number(State) == 1)
                document.getElementById("btnCancelApply").disabled = false;
            else
                document.getElementById("btnCancelApply").disabled = true;
	    }
	    else
	    {
	        document.getElementById("btnEdit").disabled = false;
		    document.getElementById("btnDel").disabled = false;
		    document.getElementById("btnApply").disabled = false;
		    document.getElementById("btnCancelApply").disabled = true;
	    }	
	}
	
	function openWin(op)
	{
		var RID = 0;
		if (op == "edit")
		{
		    RID = document.getElementById('hdnRecordID').value;
		}
		var url= "ApplyEdit.aspx?op=" + op + "&mid=" + RID ;
		var rtnValue = window.showModalDialog(url,window,"dialogHeight:500px;dialogWidth:650px;center:1;help:0;status:0;");
		if (rtnValue == undefined)
	    {
	    }
	    else
	    {
		    if (rtnValue != '0') 
		    {
		        document.getElementById('hdnRecordID').value = rtnValue;
		    }
		}
	}
	-->
</script>
</head>
<body class="body_frame" scroll="no">
    <form id="Form1" method="post" runat="server">
        &nbsp;<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <table id = "table1" border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
        <tr height="60%">
            <td valign="top">
                <table id="table2" border="1" cellpadding="0" cellspacing="0" width="100%" class="table-normal">
                    <tr><td class="td-title">会议室申请信息</td>
                     </tr>
                     <tr><td class="td-toolsbar" style="height: 24px">
                        &nbsp;<input id="btnView" onclick="javascript:window.showModalDialog('ViewBoardroomState.aspx',window,'dialogHeight:630px;dialogWidth:730px;center:1;help:0;status:0;');" type="button" value="查看会议室状态" name="btnView" width="100px" style="width: 105px" class="button-normal" runat="server" />

                        <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
                        <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                        <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                        <asp:Button ID="btnApply" Text="提交申请" Enabled="false" OnClick="btnApply_Click" runat="server" />
                        <asp:Button ID="btnCancelApply" Text="取消申请" Enabled="false" OnClick="btnCancelApply_Click" runat="server" />
                         <asp:Button ID="Button1" Text="刷新下半部分" OnClick="Button1_Click" runat="server" />
                        <asp:HiddenField ID="hdnRecordID" runat="server" />
                        <asp:HiddenField ID="hdnState" runat="server" /><asp:HiddenField ID="hdnMangeCode" runat="server" />
                    </td></tr>
                     <tr><td>
                         <asp:UpdatePanel ID="UpdatePanel1" Mode="Conditional" runat="server">
<ContentTemplate>
                        <asp:GridView ID="GridView1" AutoGenerateColumns="false" DataSourceID="SqlBoardroomApply" AllowPaging="true" CssClass="grid" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                                <table class="grid" cellspacing="0" rules="all" border="1" id="GridView1" style="border-collapse:collapse;">
		                            <tr class="grid_head">
			                            <th scope="col" style="width:8%;">序号</th><th scope="col" style="width:10%;">会议室名称</th><th scope="col" style="width:8%;">召开日期</th><th scope="col" style="width:8%;">会议开始(时)</th><th scope="col" style="width:8%;">会议开始(分)</th><th scope="col" style="width:8%;">会议结束(时)</th><th scope="col" style="width:8%;">会议结束(分)</th><th scope="col">主题</th><th scope="col" style="width:5%;">参与人数</th><th scope="col" style="width:12%;">备注</th><th scope="col" style="width:6%;">状态</th>
		                            </tr>
	                            </table>
                            </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:CommandField ShowSelectButton="true" /><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="MeetingRoom" HeaderText="会议室名称" SortExpression="MeetingRoom" /><asp:BoundField DataField="UserDate" HeaderText="召开日期" SortExpression="UserDate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="BeginHour" HeaderText="会议开始(时)" SortExpression="BeginHour" /><asp:BoundField DataField="BeginMinute" HeaderText="会议开始(分)" SortExpression="BeginMinute" /><asp:BoundField DataField="EndHour" HeaderText="会议结束(时)" SortExpression="EndHour" /><asp:BoundField DataField="EndMinute" HeaderText="会议结束(分)" SortExpression="EndMinute" /><asp:BoundField DataField="Title" HeaderText="主题" SortExpression="Title" /><asp:BoundField DataField="HumanNumber" HeaderText="参与人数" SortExpression="HumanNumber" /><asp:BoundField DataField="Content" HeaderText="备注" SortExpression="Content" /><asp:BoundField DataField="ApplyState" HeaderText="状态" SortExpression="ApplyState" /><asp:BoundField DataField="UserCode" HeaderText="申请人" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                        <asp:SqlDataSource ID="SqlBoardroomApply" SelectCommand="SELECT [RecordID], [MeetingRoomID],(select MeetingRoom from OA_MeetingRoom_Info where RecordID = MeetingRoomID) as MeetingRoom, (select ManagerCode from OA_MeetingRoom_Info where RecordID = MeetingRoomID) as ManagerCode, [HumanNumber], [Title], [UserDate], [BeginHour], [BeginMinute], [EndHour], [EndMinute], [Content], [State],[UserCode],(case when State=-1 then '退回' when state = 0 then '未提交' when state = 1 then '未安排' when state = 2 then '已安排' end) as ApplyState FROM [OA_MeetingRoom_Apply] WHERE [UserCode] = @UserCode" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:SessionParameter SessionField="yhdm" Name="UserCode" Type="String"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
                             </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnApply" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnCancelApply" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
                </td></tr> 
              </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table id="Table3" cellspacing="0" cellpadding="0" width="100%" align="center" border="0" class="table-normal">
            <tr>
                <td class="td-title">
                    使用设备清单</td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" Mode="Conditional" runat="server">
<ContentTemplate>
                    <asp:GridView ID="GridView2" AllowPaging="true" AutoGenerateColumns="false" DataSourceID="SqlEquipment" CssClass="grid" Width="100%" OnRowDataBound="GridView2_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                            <table class="grid" cellspacing="0" rules="all" border="1" id="GridView2" style="border-collapse:collapse;">
		                        <tr class="grid_head">
			                        <th scope="col" style="width:10%;">序号</th><th scope="col">设备名称</th><th scope="col" style="width:20%;">型号</th><th scope="col" style="width:15%;">数量</th><th scope="col" style="width:25%;">备注</th>
		                        </tr>
	                        </table>
                        </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="设备名称">
<ItemTemplate>
                                    <asp:Label ID="lbEquipmentName" Text='<%# Convert.ToString(Eval("EquipmentName")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
                                    <asp:Label ID="lbModel" Text='<%# Convert.ToString(Eval("Model")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="数量">
<ItemTemplate>
                                    <asp:Label ID="lbNumber" Text='<%# Convert.ToString(Eval("Number")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注">
<ItemTemplate>
                                    <asp:Label ID="lbContent" Text='<%# Convert.ToString(Eval("Content")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                    <asp:SqlDataSource ID="SqlEquipment" SelectCommand="SELECT [RecordID], [MeetingRoomID], [EquipmentName], [Model], [Number], [Content], [IsValid] FROM [OA_MeetingRoom_Equipment] a,[OA_MeetingRoom_ApplyDetail] b WHERE (a.[RecordID] = b.[EquipmentIRecordID]) and (b.[ApplyRecordID] = @ApplyRecordID)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:ControlParameter ControlID="hdnRecordID" Name="ApplyRecordID" PropertyName="value" runat="server" /></SelectParameters></asp:SqlDataSource>               
                        </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
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
