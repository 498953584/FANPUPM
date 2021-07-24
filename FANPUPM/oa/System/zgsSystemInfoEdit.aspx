<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zgsSystemInfoEdit.aspx.cs" Inherits="oa_System_zgsSystemInfoEdit" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>�ӹ�˾�ƶȹ���ά��</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
	<script language ="javascript">
	function UpFile(t)
	{
	    //t=3 �ƶȹ���
	    var RecordCode = document.getElementById('hdnRecordId').value;//���
		var url = "../../CommonWindow/Annex/AnnexList.aspx?mid="+t+"&rc="+RecordCode+"&at=0&type=2";			
		window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
	}
	</script>
</head>
<body scroll="no" class="body_popup">
    <form id="Formx" method="post" runat="server">
    <table class="table-form" id="tablex"  cellSpacing="0" cellPadding="0" width="100%" border="1" class="table-normal">
		<tr>
			<td class="td-head" colSpan="2" height="20">
                �ӹ�˾�ƶȹ���ά��
            </td>
		</tr>  
		<tr>
			<td class="td-label">
                �ƶ�����</td>
            <td>
                <asp:TextBox ID="txtzdname" CssClass="text-input" runat="server"></asp:TextBox>
            </td>
		</tr>
		<tr>
			<td class="td-label">
                �ƶ�����</td>
            <td>
                <JWControl:DateBox ID="txtDate" runat="server"></JWControl:DateBox>    
            </td>
		</tr>
		<tr>
			<td align="right" class="td-label">������</td>
			<td colspan="2"><asp:TextBox ID="TBoxAnnex" ReadOnly="true" Visible="false" runat="server"></asp:TextBox>
			<asp:Literal ID="Literal1" runat="server"></asp:Literal><input id="BtnAnnex" class="button-normal" style="WIDTH: 100px" onclick="UpFile(3)" type="button" value="�༭����..." runat="server" />
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
