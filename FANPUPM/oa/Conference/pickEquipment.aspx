<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pickEquipment.aspx.cs" Inherits="pickEquipment" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>选择设备信息</title>
	<script language="javascript">
	window.name = "win";
    <!--
  
    -->
    </script>
</head>
<body class="body_popup" scroll="no">
    <form id="form1" target="win" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <TABLE class="table-normal" id="Tablew" cellSpacing="0" cellPadding="0" width="100%" height="100%" border="0">
        <TR>
            <TD class="td-head" height="20">
                会议室设备信息</TD>
        </TR>
        <TR>
            <TD colSpan="2" valign="top">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
            <asp:GridView ID="gvEquipment" AllowPaging="true" AutoGenerateColumns="false" DataSourceID="SqlEquipment" CssClass="grid" Width="100%" OnRowDataBound="gvEquipment_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                <table class="grid" cellspacing="0" rules="all" border="1" id="gvEquipment" style="border-collapse:collapse;">
                    <tr class="grid_head">
                        <th align="center" scope="col" style="width:15%;"><input id="cbSelAll" type="checkbox" name="cbSelAll"/><label>全选</label></th>
                    <th scope="col" style="width:10%;">序号</th><th scope="col">设备名称</th><th scope="col" style="width:10%;">型号</th><th scope="col" style="width:10%;">数量</th><th scope="col" style="width:25%;">备注</th>
                    </tr>
                </table>
            </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField>
<HeaderTemplate> 
                        <asp:CheckBox ID="cbSelAll" Text="全选" AutoPostBack="true" OnCheckedChanged="cbSelAll_CheckedChanged" runat="server" /> 
                    </HeaderTemplate>

<ItemTemplate> 
                        <asp:CheckBox ID="cbSel" runat="server" /> 
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
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
        <asp:SqlDataSource ID="SqlEquipment" SelectCommand="SELECT [RecordID], [MeetingRoomID], [EquipmentName], [Model], [Number], [Content], [IsValid] FROM [OA_MeetingRoom_Equipment] WHERE ([MeetingRoomID] = @MeetingRoomID)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="MeetingRoomID" QueryStringField="mid" Type="Int32"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>   
                    </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /></Triggers></asp:UpdatePanel>
	        </TD>
            </TR>
            <tr>
                <TD class="td-submit" height="20"><asp:Button ID="BtnSave" Text="确  定" OnClick="BtnSave_Click" runat="server" />&nbsp;
                    <INPUT id="BtnClose" onclick="javascript:window.returnValue ='0';window.close();" type="button" value="关  闭" name="BtnClose">
                </TD>
            </tr>
        </TABLE>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
