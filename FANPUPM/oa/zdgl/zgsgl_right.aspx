<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zgsgl_right.aspx.cs" Inherits="oa_zdgl_zgsgl_right" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>子公司制度列表</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
		function showEditWin(op,rt,rn)
		{
		    var ret = false;
		    if(op == 'add')
		        ret = window.showModalDialog('Broker.aspx?Op=add&rt='+rt+'&rn='+rn,window,'dialogHeight:200px;dialogWidth:300px;center:1;help:0;status:0;');
		    else if(op == 'edit')
		        ret = window.showModalDialog('Broker.aspx?Op=edit&rt='+rt+'&rn='+rn+'&RecordID='+window.document.getElementById('hfRecord').value,window,'dialogHeight:200px;dialogWidth:300px;center:1;help:0;status:0;');
		    else if(op == 'modPriv')
		        window.showModalDialog('Broker.aspx?Op=modPriv&id='+window.document.getElementById('hfRecord').value,window,'dialogHeight:600px;dialogWidth:650px;center:1;help:0;status:0;');

		    return ret;
		}
		
		function getRecordID(RecordID)
		{
		    window.document.getElementById('hfRecord').value = RecordID;
		    document.getElementById('btnEdit').disabled = false;
		    document.getElementById('btnDel').disabled = false;
		    document.getElementById('btnPrivilege').disabled = false;
		}
		</script>
	</HEAD>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" headerstyle-cssclass="grid_head" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				    <TR>
						<td colspan="2" class="td-title"><asp:Label ID="Label1" CssClass="popout-normal" runat="server">Label</asp:Label>集团制度管理</td>
					</TR>
					<TR>
						<td colspan="2" class="td-toolsbar">
                            &nbsp;<asp:Button ID="btnAdd" Text="新 增" runat="server" />
                            <asp:Button ID="btnEdit" Text="修 改" Enabled="false" runat="server" />&nbsp;<asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                            <asp:Button ID="btnStartWF" Enabled="false" Text="提交审核" runat="server" />
                            <asp:HiddenField ID="hfRecord" runat="server" />
                        </td>
					</TR>
					<TR>
						<TD colSpan="2" style="width: 100%; height: 100%" valign="top">
						<div id="Box" style="width:100%;height:100%">
						<asp:DataGrid ID="DataGrid1" DataSourceID="Sql" Width="100%" CssClass="grid" ItemStyle-CssClass="grid_row" HeaderStyle-CssClass="grid_head" AutoGenerateColumns="false" OnItemDataBound="DataGrid1_ItemDataBound" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                        <%# Container.ItemIndex + 1 %>
                                    </ItemTemplate>

<EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="SystemName" HeaderText="制度名称"></asp:BoundColumn><asp:BoundColumn DataField="SignDate" HeaderText="制订日期"></asp:BoundColumn><asp:BoundColumn DataField="SignMan" HeaderText="制订人"></asp:BoundColumn><asp:BoundColumn DataField="AuditState" HeaderText="状态"></asp:BoundColumn><asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn></Columns></asp:DataGrid></div></TD>
					</TR>
				</TABLE><asp:SqlDataSource ID="Sql" SelectCommand="SELECT [SystemName], [SignDate],[SignMan],[AuditState],[Remark] FROM [OA_System_Info]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter DefaultValue="000" Name="RoleTypeCode" QueryStringField="rt" Type="String"></asp:QueryStringParameter><asp:Parameter DefaultValue="1" Name="IsValid" Type="String" /></SelectParameters></asp:SqlDataSource>
                <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			</FONT>
		</form>
	</body>
</HTML>
