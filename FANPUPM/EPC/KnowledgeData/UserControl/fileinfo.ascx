<%@ Control Language="C#" AutoEventWireup="true" CodeFile="fileinfo.ascx.cs" Inherits="FileInfo" %>

<script language="javascript">

function clickRow(obj,obj2,obj3)
{	
	document.getElementById('FileInfo_hdnFileCode').value = obj;
	document.getElementById('FileInfo_hdnFilePath').value = obj2;
	if(obj3 == 'ADD' || obj3 == 'EDIT')
	{
		document.getElementById('FileInfo_btnDel').disabled = false;
		document.getElementById('FileInfo_btnDel').className = "button_del";
	}
	
}
function OpfinFile(obj)
{
	var url = obj;
	window.open(url);
}
</script>
<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="td-head" align="center" colSpan="2">附&nbsp;&nbsp;件</td>
	</tr>
	<tr>
	</tr>
	<tr>
		<td vAlign="top" colSpan="2" height="100%"><FONT face="宋体"><asp:DataGrid ID="DataGrid1" DataKeyField="AnnexCode" CssClass="grid" Width="100%" AutoGenerateColumns="false" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
								<%# Container.ItemIndex + 1 %>
							</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="OriginalName" HeaderText="文件原名"></asp:BoundColumn><asp:BoundColumn DataField="AnnexName" HeaderText="文件新名"></asp:BoundColumn><asp:BoundColumn DataField="AddDate" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="FilePath1" ReadOnly="true"></asp:BoundColumn></Columns></asp:DataGrid></FONT><JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></td>
	</tr>
	<tr>
		<td width="80%" height="22"><asp:Label ID="lbFileCount" runat="server"></asp:Label></td>
		<td align="right" width="20%"><asp:Button ID="btnDel" CssClass="button_delu" CausesValidation="false" Enabled="false" OnClick="btnDel_Click" runat="server" /><asp:Button ID="btnAdd" CssClass="button_add" CausesValidation="false" OnClick="btnAdd_Click" runat="server" /></td>
	</tr>
</table>
<input id="hdnFilePath" type="hidden" name="hdnFilePath" runat="server" />
<input id="hdnFileCode" type="hidden" name="hdnFileCode" runat="server" />
