<%@ Control Language="C#" AutoEventWireup="true" CodeFile="fileinfo.ascx.cs" Inherits="FileInfo" %>

<script language="javascript">
			function clickRow(obj,obj2,OpType)
			{
				
				document.getElementById('FileInfo_hdnFileCode').value = obj;
				document.getElementById('FileInfo_hdnFilePath').value = obj2;
				if(OpType != "See")
				{
					document.getElementById('FileInfo_btnDel').disabled = false;
					//document.getElementById('FileInfo_btnDel').className = "button_del";	
					document.getElementById('FileInfo_btnDel').className ="button-normal";
   
				}
			}
			function OpfinFile(obj)
			{
				var url = obj;
				window.open(url);
			}
</script>
<table cellpadding="0" cellspacing="0" border="0" width="100%" height="100%">
	<tr>
		<td align="center" Class="td-head" colspan="2">附&nbsp;&nbsp;件</td>
	</tr>
	<tr>
	</tr>
	<tr>
		<td valign="top" height="100%" colspan="2"><FONT face="宋体">
				<asp:DataGrid ID="DataGrid1" AutoGenerateColumns="false" Width="100%" CssClass="grid" DataKeyField="AnnexCode" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
								<%# Container.ItemIndex + 1 %>
							</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="OriginalName" HeaderText="文件原名"></asp:BoundColumn><asp:BoundColumn DataField="AnnexName" HeaderText="文件新名"></asp:BoundColumn><asp:BoundColumn DataField="AddDate" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="FilePath1" ReadOnly="true"></asp:BoundColumn></Columns></asp:DataGrid></FONT>
			<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></td>
	</tr>
	<tr>
		<td height="22" width="60%">
			<asp:Label ID="lbFileCount" runat="server"></asp:Label></td>
		<td width="40%" align="right">
			<asp:Button ID="btnDel" Enabled="false" CausesValidation="false" CssClass="button-normal" Text="删 除" OnClick="btnDel_Click" runat="server" />
			<asp:Button ID="btnAdd" CausesValidation="false" CssClass="button-normal" Text="新 增" OnClick="btnAdd_Click" runat="server" /></td>
	</tr>
</table>
<input type="hidden" id="hdnFilePath" name="hdnFilePath" runat="server" />
<input type="hidden" id="hdnFileCode" name="hdnFileCode" runat="server" />
