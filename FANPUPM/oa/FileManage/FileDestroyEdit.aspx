<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileDestroyEdit.aspx.cs" Inherits="oa_FileManage_FileDestroyEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>管理销毁记录</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
	<script language ="javascript">
	</script>
</head>
<body scroll="no" class="body_popup">
    <form id="Formx" method="post" runat="server">
    <table class="table-form" id="tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colSpan="2" height="20">
                管理销毁记录
            </td>
		</tr>  
		<tr>
			<td class="td-label">
                申请人</td>
            <td>
                <asp:TextBox ID="txtApplyPerson" CssClass="text-input" Enabled="false" runat="server"></asp:TextBox>
            </td>
		</tr>
		<tr>
			<td class="td-label">
                申请时间</td>
            <td>
                <JWControl:DateBox ID="txtApplyDate" runat="server"></JWControl:DateBox>    
            </td>
		</tr>
		<tr>
			<td class="td-label">
                情况说明</td>
            <td>
                <asp:TextBox ID="txtRemark" CssClass="text-input" MaxLength="250" Rows="3" TextMode="MultiLine" Width="99%" runat="server"></asp:TextBox></td>
		</tr> 
		<tr>
			<td class="td-submit" colSpan="2" height="20">
			<asp:Button ID="btnAdd" Text="保  存" OnClick="btnAdd_Click" runat="server" />&nbsp;
			<INPUT id="BtnClose" onclick="javascript:returnValue=true;window.close();" type="button" value="关  闭">
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
            </td>
		</tr>
		<tr height="80%">
			<td colSpan="2">
			    <iframe id="frmLibrary" name="frmLibrary" frameborder="0" width="100%" height="100%" runat="server"></iframe>
			</td>
		</tr> 
	</table>
    </form>
</body>
</html>
