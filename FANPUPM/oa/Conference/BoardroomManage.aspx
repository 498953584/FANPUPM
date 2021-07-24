<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BoardroomManage.aspx.cs" Inherits="BoardroomManage" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title></title>
	<script language="javascript" type="text/javascript">
	<!--
	function ClickRow(FlowGuid, State, ManageCode)
	{
		document.getElementById('hdnRecordID').value = FlowGuid;
		document.getElementById('hdnState').value = State;
		document.getElementById('hdnMangeCode').value = ManageCode;
        if ( Number(State) == 1)
        {
            document.getElementById("btnArrange").disabled = false;
            document.getElementById("btnDealWith").disabled = false;
            document.getElementById("BtnCancel").disabled = true;
        }
        else
        {
            document.getElementById("btnArrange").disabled = true;
            document.getElementById("btnDealWith").disabled = true;
            document.getElementById("BtnCancel").disabled = false;
        }
	}
	
	function openWin(op)
	{
		var url= "" ;
		var recordId = document.getElementById('hdnRecordID').value;
		if (op == "Arrange") //会议室安排
		{
            url = "ViewApply.aspx?mid=" + recordId ;
		}
		else //处理申请
		{
		    url= "DealWithApply.aspx?mid=" + recordId ;
		}
		var rtnValue = window.showModalDialog(url,window,"dialogHeight:340px;dialogWidth:450px;center:1;help:0;status:0;");
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
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <table id = "table1" border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
        <tr height="60%">
            <td valign="top">
                <table id="table2" border="1" cellpadding="0" cellspacing="0" width="100%" class="table-normal">
                    <tr><td class="td-title">会议室管理</td>
                     </tr>
                     <tr><td class="td-toolsbar" style="height: 24px">
                        &nbsp; <asp:Button ID="btnArrange" Text="会议室安排" Enabled="false" Width="80px" OnClick="btnArrange_Click" runat="server" />
                        <asp:Button ID="btnDealWith" Text="退回申请" Enabled="false" OnClick="btnDealWith_Click" runat="server" />
                         <asp:Button ID="BtnCancel" Text="取消会议安排" Enabled="false" Width="80px" OnClick="BtnCancel_Click" runat="server" />
                        <asp:HiddenField ID="hdnRecordID" runat="server" />
                        <asp:HiddenField ID="hdnState" runat="server" /><asp:HiddenField ID="hdnMangeCode" runat="server" />
                         &nbsp;<br />
                        <br />
                         &nbsp;&nbsp; &nbsp;
                         <asp:LinkButton ID="LbtnNotPlan" CssClass="firstpage" OnClick="LbtnNotPlan_Click" runat="server">［未安排情况］</asp:LinkButton>&nbsp;
                         <asp:LinkButton ID="LbtnYesPlan" CssClass="firstpage" OnClick="LbtnYesPlan_Click" runat="server">［已安排情况］</asp:LinkButton>&nbsp;
                         <asp:LinkButton ID="LbtnUntread" CssClass="firstpage" OnClick="LbtnUntread_Click" runat="server">［退回申请情况］</asp:LinkButton></td></tr>
                     <tr><td>
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                        <asp:GridView ID="GridView1" AutoGenerateColumns="false" DataSourceID="SqlBoardroomApply" AllowPaging="true" CssClass="grid" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                                <table id="Table4" border="1" cellspacing="0" class="grid" rules="all" style="border-collapse: collapse">
                                    <tr class="grid_head">
                                        <th scope="col" style="display: none">
                                            &nbsp;</th>
                                        <th scope="col" style="width: 8%">
                                            序号</th>
                                        <th scope="col" style="width: 10%">
                                            会议室名称</th>
                                        <th scope="col" style="width: 8%">
                                            召开日期</th>
                                        <th scope="col" style="width: 8%">
                                            会议开始(时)</th>
                                        <th scope="col" style="width: 8%">
                                            会议开始(分)</th>
                                        <th scope="col" style="width: 8%">
                                            会议结束(时)</th>
                                        <th scope="col" style="width: 8%">
                                            会议结束(分)</th>
                                        <th scope="col">
                                            主题</th>
                                        <th scope="col" style="width: 5%">
                                            参与人数</th>
                                        <th align="center" scope="col" style="width: 70px">
                                            申请人</th>
                                        <th scope="col" style="width: 12%">
                                            备注</th>
                                        <th scope="col" style="width: 6%">
                                            状态</th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:CommandField ShowSelectButton="true" /><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="MeetingRoom" HeaderText="会议室名称" SortExpression="MeetingRoom" /><asp:BoundField DataField="UserDate" HeaderText="召开日期" SortExpression="UserDate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="BeginHour" HeaderText="会议开始(时)" SortExpression="BeginHour" /><asp:BoundField DataField="BeginMinute" HeaderText="会议开始(分)" SortExpression="BeginMinute" /><asp:BoundField DataField="EndHour" HeaderText="会议结束(时)" SortExpression="EndHour" /><asp:BoundField DataField="EndMinute" HeaderText="会议结束(分)" SortExpression="EndMinute" /><asp:BoundField DataField="Title" HeaderText="主题" SortExpression="Title" /><asp:BoundField DataField="HumanNumber" HeaderText="参与人数" SortExpression="HumanNumber" /><asp:BoundField DataField="UserCode" HeaderText="申请人" SortExpression="UserCode" /><asp:BoundField DataField="Content" HeaderText="备注" SortExpression="Content" /><asp:BoundField DataField="ApplyState" HeaderText="状态" SortExpression="ApplyState" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                        <asp:SqlDataSource ID="SqlBoardroomApply" SelectCommand="SELECT [RecordID], [MeetingRoomID],(select MeetingRoom from OA_MeetingRoom_Info where RecordID = MeetingRoomID AND ManagerCode = @UserCode) as MeetingRoom,(select ManagerCode from OA_MeetingRoom_Info where RecordID = MeetingRoomID AND ManagerCode = @UserCode) as ManagerCode, [HumanNumber], [Title], [UserDate], [BeginHour], [BeginMinute], [EndHour], [EndMinute], [Content], [State],[UserCode],(case when State=-1 then '退回' when state = 0 then '未提交' when state = 1 then '未安排' when state = 2 then '已安排' end) as ApplyState FROM [OA_MeetingRoom_Apply] WHERE State > 0 AND MeetingRoomID IN (SELECT RecordID FROM OA_MeetingRoom_Info WHERE ManagerCode = @UserCode)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:SessionParameter SessionField="yhdm" Name="UserCode" Type="String"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
                             </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnArrange" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDealWith" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="LbtnNotPlan" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="LbtnUntread" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="LbtnYesPlan" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
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
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                    <asp:SqlDataSource ID="SqlEquipment" SelectCommand="SELECT [RecordID], [MeetingRoomID], [EquipmentName], [Model], [Number], [Content], [IsValid] FROM [OA_MeetingRoom_Equipment] a,[OA_MeetingRoom_ApplyDetail] b WHERE (a.[RecordID] = b.[EquipmentIRecordID]) and (b.[ApplyRecordID] = @ApplyRecordID)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:ControlParameter ControlID="hdnRecordID" Name="ApplyRecordID" PropertyName="Value" Type="Int32" runat="server" /></SelectParameters></asp:SqlDataSource>               
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
