<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateFraeList.aspx.cs" Inherits="TemplateFraeList" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title></title>
	<script language="javascript">
	<!--
	function ClickRow(op,recordId)
	{
	    if (op=="0")
	    {
	        document.getElementById('btnEdit').disabled = false;
	        document.getElementById('btnDel').disabled = false;
	        document.getElementById('hdnTemplateID').value = recordId;
	    }
	    else
	    {
            document.getElementById('btnEdit'+op).disabled = false;
	        document.getElementById('btnDel'+op).disabled = false;
	        document.getElementById('hdnTemplateID'+op).value = recordId;
	    }
	}
	function openTemplateFraeEdit(types,op,templateId)
	{
	    var recordId = 0;
	    var stateParm = "";
		if (op==2)
		{
		    if (types == "0")
		        recordId = document.getElementById('hdnTemplateID').value;
		    else
			    recordId = document.getElementById('hdnTemplateID'+types).value;
		}
		if (types == "0")
		{
		    var url= "AttendManEdit.aspx?t=" + op + "&tid=" + templateId + "&rid="+ recordId;
		    stateParm = "dialogHeight:220px;dialogWidth:400px;center:1;help:0;status:0;";
		}
		else if (types == "1")
		{
		    var url= "WaiterEdit.aspx?t=" + op + "&tid=" + templateId + "&rid="+ recordId;
		    stateParm = "dialogHeight:220px;dialogWidth:400px;center:1;help:0;status:0;";
		}
		else if (types == "2")
		{
		    var url= "EquipmentEdit.aspx?t=" + op + "&tid=" + templateId + "&rid="+ recordId;
		    stateParm = "dialogHeight:220px;dialogWidth:400px;center:1;help:0;status:0;";
		}
		else
		{
		    var url= "ProjectEdit.aspx?t=" + op + "&tid=" + templateId + "&rid="+ recordId;
		    stateParm = "dialogHeight:220px;dialogWidth:400px;center:1;help:0;status:0;";
		}
		return window.showModalDialog(url,window,stateParm);
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
        <iewc:TabStrip ID="TabStrip1" TargetID="MultiPage1" TabDefaultStyle="background-image:url(../../../images/2_2.gif);border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 1px;font-family:verdana;font-weight:bold;font-size:12px;color:#000000;width:120;height:20;text-align:center;" TabHoverStyle="background-color:#777777;" TabSelectedStyle="background-image:url(../../../images/2_1.gif);color:#000000;border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 0px;width:120;height:20;font-size:12px;text-align:center;" SepDefaultStyle="border-top:#999AB5 solid 0px;border-left:#999AB5 solid 0px;border-right:#999AB5 solid 0px;border-bottom:#999AB5 solid 1px;" TabIndex="-1" runat="server"><Items><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="到会人员" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="服务人员" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="会议设备" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="会议方案" runat="server"></iewc:Tab><iewc:TabSeparator DefaultStyle="width:100%;" runat="server"></iewc:TabSeparator></Items></iewc:TabStrip>
        </td>
    </tr>
    <tr>
        <td vAlign="top">
            <iewc:MultiPage ID="MultiPage1" Height="100%" runat="server"><iewc:PageView runat="server">
                    <table id="table2" border="1" cellpadding="0" cellspacing="0" class="table-form" width="100%">
                        <tr>
                            <td class="td-head">到会人员</td>
                        </tr>
                        <tr id="btnTr" height="24" runat="server"><td class="td-toolsbar" runat="server">
						        <input type="hidden" id="hdnTemplateID" name="hdnTemplateID" style="WIDTH: 10px" runat="server" />

						        <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
						        <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
						        <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" /></td></tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                                <asp:GridView ID="gvAttendMan" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlAttendMan" Width="100%" OnRowDataBound="gvAttendMan_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                                                            <table class="grid" cellspacing="0" rules="all" border="1" id="gvAttendMan" style="border-collapse:collapse;">
                                                                <tr class="grid_head">
                                                                    <th scope="col" style="width:5%">序号</th><th scope="col">主题</th><th scope="col" style="width:30%">参会人员</th><th scope="col" style="width:10%">参会人数</th><th scope="col" style="width:8%">是否短信通知</th><th scope="col" style="width:20%">备注</th>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>  
                                                                    </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="Topic" HeaderText="主题" SortExpression="Topic" /><asp:BoundField DataField="AttendManNames" HeaderText="参会人员" SortExpression="AttendManNames" /><asp:BoundField DataField="Number" HeaderText="参会人数" SortExpression="Number" /><asp:TemplateField HeaderText="是否短信通知" SortExpression="IsSms">
<ItemTemplate>
                                                                    <asp:CheckBox ID="ckbIsSms" BorderStyle="None" Enabled="false" BorderWidth="0px" Checked='<%# (DataBinder.Eval(Container.DataItem, "IsSms").ToString() == "1") %>' runat="server" />
                                                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="Content" HeaderText="备注" SortExpression="Content" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                                                    <asp:SqlDataSource ID="SqlAttendMan" SelectCommand="SELECT [RecordID], [TempletID], [Topic], [AttendManCodes], [AttendManNames], [Number], [IsSms], [Content] FROM [OA_Meeting_Templet_AttendMan] WHERE ([TempletID] = @TempletID)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="TempletID" QueryStringField="tid" Type="Int32"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                                    </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </iewc:PageView><iewc:PageView runat="server">
                    <table id="table3" border="1" cellpadding="0" cellspacing="0" class="table-form" width="100%">
                        <tr>
                            <td class="td-head">服务人员</td>
                        </tr>
                        <tr id="btnTr1" height="24" runat="server"><td class="td-toolsbar" runat="server">
						        <input type="hidden" id="hdnTemplateID1" name="hdnTemplateID1" style="WIDTH: 10px" runat="server" />

						        <asp:Button ID="btnAdd1" Text="新 增" OnClick="btnAdd1_Click" runat="server" />
						        <asp:Button ID="btnEdit1" Text="修 改" Enabled="false" OnClick="btnEdit1_Click" runat="server" />
						        <asp:Button ID="btnDel1" Text="删 除" Enabled="false" OnClick="btnDel1_Click" runat="server" /></td></tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
                                <asp:GridView ID="gvWaiter" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlWaiter" Width="100%" OnRowDataBound="gvWaiter_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                                        <table class="grid" cellspacing="0" rules="all" border="1" id="gvWaiter" style="border-collapse:collapse;">
                                            <tr class="grid_head">
                                                <th scope="col" style="width:10%">序号</th><th scope="col" style="width:40%">姓名</th><th scope="col">服务事项</th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>  
                                            </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="WaiterName" HeaderText="姓名" SortExpression="WaiterName" /><asp:BoundField DataField="Content" HeaderText="服务事项" SortExpression="Content" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                                <asp:SqlDataSource ID="SqlWaiter" SelectCommand="SELECT [RecordID], [TempletID], [WaiterCode], [WaiterName], [Content] FROM [OA_Meeting_Templet_Waiter] WHERE ([TempletID] = @TempletID)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="TempletID" QueryStringField="tid" Type="Int32"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                                </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnAdd1" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnEdit1" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDel1" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </iewc:PageView><iewc:PageView runat="server">
                    <table id="table4" border="1" cellpadding="0" cellspacing="0" class="table-form" width="100%">
                        <tr>
                            <td class="td-head">会议设备</td>
                        </tr>
                        <tr id="btnTr2" height="24" runat="server"><td class="td-toolsbar" runat="server">
						        <input type="hidden" id="hdnTemplateID2" name="hdnTemplateID2" style="WIDTH: 10px" runat="server" />

						        <asp:Button ID="btnAdd2" Text="新 增" OnClick="btnAdd2_Click" runat="server" />
						        <asp:Button ID="btnEdit2" Text="修 改" Enabled="false" OnClick="btnEdit2_Click" runat="server" />
						        <asp:Button ID="btnDel2" Text="删 除" Enabled="false" OnClick="btnDel2_Click" runat="server" /></td></tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
<ContentTemplate>
                                <asp:GridView ID="gvEquipment" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlEquipment" Width="100%" OnRowDataBound="gvEquipment_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                                        <table class="grid" cellspacing="0" rules="all" border="1" id="gvEquipment" style="border-collapse:collapse;">
                                            <tr class="grid_head">
                                                <th scope="col" style="width:10%">序号</th><th scope="col">设备名称</th><th scope="col" style="width:10%">设备数量</th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>  
                                            </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="EquipmentName" HeaderText="设备名称" SortExpression="EquipmentName" /><asp:BoundField DataField="Number" HeaderText="设备数量" SortExpression="Number" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                                <asp:SqlDataSource ID="SqlEquipment" SelectCommand="SELECT [RecordID], [TempletID], [EquipmentName], [Number] FROM [OA_Meeting_Templet_Equipment] WHERE ([TempletID] = @TempletID)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="TempletID" QueryStringField="tid" Type="Int32"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                                </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnAdd2" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnEdit2" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDel2" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </iewc:PageView><iewc:PageView runat="server">
                    <table id="table5" border="1" cellpadding="0" cellspacing="0" class="table-form" width="100%">
                        <tr>
                            <td class="td-head">会议方案</td>
                        </tr>
                        <tr id="btnTr3" height="24" runat="server"><td class="td-toolsbar" runat="server">
						        <input type="hidden" id="hdnTemplateID3" name="hdnTemplateID3" style="WIDTH: 10px" runat="server" />

						        <asp:Button ID="btnAdd3" Text="新 增" OnClick="btnAdd3_Click" runat="server" />
						        <asp:Button ID="btnEdit3" Text="修 改" Enabled="false" OnClick="btnEdit3_Click" runat="server" />
						        <asp:Button ID="btnDel3" Text="删 除" Enabled="false" OnClick="btnDel3_Click" runat="server" /></td></tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
<ContentTemplate>
                                <asp:GridView ID="gvProject" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlProject" Width="100%" OnRowDataBound="gvProject_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                                        <table class="grid" cellspacing="0" rules="all" border="1" id="gvProject" style="border-collapse:collapse;">
                                            <tr class="grid_head">
                                                <th scope="col" style="width:10%">序号</th><th scope="col" style="width:30%">项目名称</th><th scope="col">备注</th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>  
                                            </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="ProjectName" HeaderText="项目名称" SortExpression="ProjectName" /><asp:BoundField DataField="Content" HeaderText="备注" SortExpression="Content" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                                <asp:SqlDataSource ID="SqlProject" SelectCommand="SELECT [RecordID], [TempletID], [ProjectName], [Content], [OriginalName], [FilePath] FROM [OA_Meeting_Templet_Project] WHERE ([TempletID] = @TempletID)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="TempletID" QueryStringField="tid" Type="Int32"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                                </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnAdd3" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnEdit3" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDel3" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
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
