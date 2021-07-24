<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClassEdit.aspx.cs" Inherits="oa_BooksManage_ClassEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html>
<head runat="server"><title></title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
</head>
<body scroll="no" class="body_popup">
    <form id="Formx" method="post" runat="server">
    <table class="table-form" id="tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colSpan="2" height="20">
                <asp:Label ID="LblMsg" runat="server"></asp:Label></td>
		</tr>  
		<tr>
			<td class="td-label" width="20%">
                分类名称</td>
			<td><asp:TextBox ID="txtClassName" CssClass="text-input" style="width:99%" MaxLength="10" runat="server"></asp:TextBox>
			</td>
		</tr> 
		<tr>
			<td class="td-label" width="20%">
                备注</td>
			<td><asp:TextBox ID="txtRemark" CssClass="text-input" MaxLength="100" style="width:99%" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox></td>
		</tr> 
		<tr>
			<td class="td-submit" colSpan="2" height="20" class=td-submit>
			<asp:Button ID="btnAdd" Text="保 存" OnClick="btnAdd_Click" runat="server" />&nbsp;
			<INPUT id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button" value="取 消">&nbsp;
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			</td>
		</tr>
	</table>
    </form>
</body>
</html>
