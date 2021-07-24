<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VotingOptionEdit.aspx.cs" Inherits="oa_Voting_VotingOptionEdit" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>����ͶƱѡ��</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
    <table class="table-form" id="tablex"  cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colSpan="2" height="20">
                �����ѡ����Ϣ
            </td>
		</tr>  
		<tr>
			<td class="td-label">
                ѡ������</td>
            <td>
                <asp:TextBox ID="txtzdname" CssClass="text-input" runat="server"></asp:TextBox>
            </td>
		</tr>
		<tr>
			<td class="td-label">
                ��ע</td>
            <td>
                <asp:TextBox ID="txtRemark" CssClass="text-input" Rows="3" TextMode="MultiLine" Width="100%" runat="server"></asp:TextBox></td>
		</tr> 
		
		<tr>
			<td class="td-submit" colSpan="2" height="20">
			<input type="hidden" id="hdnRecordId" name="hdnRecordId" style="width : 10px" runat="server" />

			<asp:Button ID="btnAdd" Text="��  ��" OnClick="btnAdd_Click" runat="server" />&nbsp;
			<INPUT id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button" value="��  ��">
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			</td>
		</tr>
	</table>
    </form>
</body>
</html>
