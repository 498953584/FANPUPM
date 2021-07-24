<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateList.aspx.cs" Inherits="TemplateList" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>会议模板列表</title>
	<script language="javascript" type="text/javascript">
	<!--
	function ClickRow(templateId)
	{
		document.getElementById('btnEdit').disabled = false;
		document.getElementById('btnDel').disabled = false;
		document.getElementById('hdnTemplateID').value = templateId;
		document.getElementById('Template_FraeList').src = "TemplateFraeList.aspx?tid="+templateId;
	}
	function openTemplateEdit(op,classId)
	{				
		var templateId = 0;
		if (op==2)
		{
			templateId = document.getElementById('hdnTemplateID').value;
		}	
		var url= "TemplateEdit.aspx?t=" + op + "&cid=" + classId + "&tid="+ templateId;
		return window.showModalDialog(url,window,"dialogHeight:140px;dialogWidth:400px;center:1;help:0;status:0;");
	}
	-->
	</script>
</head>
<body class="body_frame" scroll="no" >
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
       <table width="100%"  height="100%" border="0" id="table1" class="table-normal">
    <tr>
        <TD class="td-title" align="center" height="28">会议模板列表</TD>
    </tr>
    <tr id="btnTr" height="24" runat="server"><td class="td-toolsbar" colspan="3" runat="server">
						<input type="hidden" id="hdnTemplateID" name="hdnTemplateID" style="WIDTH: 10px" runat="server" />

						<asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
						<asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
						<asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                    </td></tr>
    <tr height="40%">
        <TD vAlign="top">
			<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                <asp:GridView ID="gvTemplatelist" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlTemplate" Width="100%" OnRowDataBound="gvTemplatelist_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                            <table class="grid" cellspacing="0" rules="all" border="1" id="GridView1" style="border-collapse:collapse;">
		                        <tr class="grid_head">
			                        <th scope="col" style="width:10%">序号</th><th scope="col" style="width:40%">模板名称</th><th scope="col">备注</th>
		                        </tr>
	                        </table>
                        </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>  
                            </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="TempletName" HeaderText="模板名称" SortExpression="TempletName" /><asp:BoundField DataField="Content" HeaderText="备注" SortExpression="Content" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                <asp:SqlDataSource ID="SqlTemplate" SelectCommand="SELECT [RecordID], [ClassID], [TempletName], [Content] FROM [OA_Meeting_Templet] WHERE ([ClassID] = @ClassID)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="ClassID" QueryStringField="cid" Type="Int32"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>		
                    </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
			</div>
		</TD>
    </tr>
    <tr>
        <td>
            <iframe id="Template_FraeList" name="Template_FraeList" src="" frameBorder="0" width="100%" scrolling="no" height="100%"></iframe>
        </td>
    </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
