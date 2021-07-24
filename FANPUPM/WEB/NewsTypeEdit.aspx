<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsTypeEdit.aspx.cs" Inherits="WEB_NewsTypeEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>新闻类型编辑</title>
    <script language="javascript">
    <!--
        window.name = "win";
    -->
    </script>
</head>
<body scroll="no" class="body_popup">
    <form id="form1" target="win" runat="server">
    <div>
    <table class="table-form" id="tablex" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colSpan="2" height="20">
                新闻分类维护</td>
		</tr>  
		<tr>
			<td class="td-label" width="25%">
                上级分类名称</td>
			<td><asp:TextBox ID="txtParentClass" CssClass="text-input" style="width:99%" MaxLength="25" ReadOnly="true" Enabled="false" runat="server"></asp:TextBox>
			</td>
		</tr> 
		<tr>
			<td class="td-label" width="25%">
                分类名称</td>
			<td><asp:TextBox ID="txtClassName" CssClass="text-input" style="width:99%" MaxLength="25" runat="server"></asp:TextBox>
			</td>
		</tr> 
		<tr>
			<td class="td-submit" colSpan="2" height="20">
			<asp:Button ID="btnAdd" Text="保 存" OnClick="btnAdd_Click" runat="server" />&nbsp;
			<INPUT id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button" value="取 消">&nbsp;
			</td>
		</tr>
	</table>
    </div>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
