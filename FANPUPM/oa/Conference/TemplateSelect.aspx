<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateSelect.aspx.cs" Inherits="TemplateSelect" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>会议模板列表</title>
	<script language="javascript" type="text/javascript">
	<!--
	function ClickRow(templateId)
	{
		document.getElementById('BtnSave').disabled = false;
		document.getElementById('hdnTemplateID').value = templateId;
	}
	-->
	</script>
</head>
<body class="body_frame" scroll="no" >
    <form id="form1" runat="server">
        <table width="100%"  height="100%" border="0" id="table1" class="table-normal">
    <tr>
        <TD class="td-title" align="center" height="28">会议模板列表</TD>
    </tr>
    <tr>
        <TD vAlign="top">
			<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
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
			</div>
		</TD>
    </tr>
    <tr>
        <TD class="td-submit" height="20"><asp:Button ID="BtnSave" Enabled="false" Text="确  定" OnClick="BtnSave_Click" runat="server" />&nbsp;
            <INPUT id="BtnClose" onclick="javascript:window.close();" type="button" value="关  闭" name="BtnClose">
            <input type="hidden" id="hdnTemplateID" name="hdnTemplateID" style="WIDTH: 10px" runat="server" />

        </TD>
    </tr>
    </table>
    </form>
</body>
</html>
