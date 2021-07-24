<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonDrawStatSubTabEdit.aspx.cs" Inherits="oa_WorkManage_SubCompanyManage_PersonDrawStatSubTabEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>��Ʒ�����嵥</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
	<script language ="javascript">
	<!--
	    function checkDecimal(sourObj)
	    {
		    if (sourObj.value=="")
		    {
		    	sourObj.value = 0;
		    }
		    if (!(new RegExp(/^\d+(\.\d+)?$/).test(sourObj.value)))
		    {
		    	alert('�������Ͳ���ȷ��');
		    	sourObj.focus();
		    	return;
		    }
	    }
	    function IsInteger(sourObj)
	    {
		    if (sourObj.value == "")
		    {
		    	sourObj.value = 0;
		    }
		    else
		    {
		       if (!(new RegExp(/^\d+$/).test(sourObj.value)))
		    	{
		    	    alert('�������Ͳ���ȷ��');
		    	    sourObj.focus();
		    	    return;
			     }
		    }
	    }
	    function PickMatterial()
	    {
	        var Matterial = new Array();
	        Matterial[0] = "";
	        Matterial[1] = "";
	        Matterial[2] = "";
	        Matterial[3] = "";
	        Matterial[4] = "";
		    var url= "../StorageManage/MattrialSelect.aspx?dd="+document.getElementById('HdnDepotID').value;
		    var ref = window.showModalDialog(url,Matterial,"dialogHeight:360px;dialogWidth:620px;center:1;help:0;status:0;");
		    if(Matterial[0] != "")
		    {
		        document.getElementById('txtResName').value = Matterial[0];
		        var ddlUseType = document.getElementById('DDLUseType');
		        ddlUseType.selectedIndex = Matterial[1];
		        var ddlGetType = document.getElementById('DDLGetType');
		        ddlGetType.selectedIndex = Matterial[2];
		        document.getElementById('txtUnit').value = Matterial[3];
		        document.getElementById('txtInStoragePrice').value = Matterial[4];
		        document.getElementById('HdnMatterialID').value = Matterial[5];
		    }
	    }
	-->
	</script>
</head>
<body scroll="no" class="body_popup">
    <form id="Formx" runat="server">
        <table class="table-normal" id="tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colSpan="4" height="20">
                ��Ʒ�����嵥<input id="HdnDepotID" value="0" style="width: 20px" type="hidden" runat="server" />

            </td>
		</tr>  
		<tr>
			<td class="td-label" width="15%">�� &nbsp;&nbsp; ��</td>
			<td colSpan="3">
			    <asp:TextBox ID="txtResName" CssClass="text-input" style="width:85%" ReadOnly="true" runat="server"></asp:TextBox>
			    <img src="../../../StyleCss/add.gif" id="imgsel" style="cursor:hand" onclick="PickMatterial();" runat="server" />

			    <input id="HdnMatterialID" value="0" style="width: 20px" type="hidden" runat="server" />

			</td>
		</tr> 
		<tr>
			<td class="td-label" width="15%">
                �� &nbsp;&nbsp; ��</td>
            <td style="width: 30%">
                <asp:DropDownList ID="DDLUseType" Enabled="false" runat="server"><asp:ListItem Value="0" Text="������" /><asp:ListItem Value="1" Text="�Ծɻ���" /></asp:DropDownList></td>
            <td class="td-label" width="15%">
                �������</td>
			<td>
                <asp:DropDownList ID="DDLGetType" Enabled="false" runat="server"><asp:ListItem Value="0" Text="��������" /><asp:ListItem Value="1" Text="���Ź���" /></asp:DropDownList></td>
		</tr>
		<tr>
			<td class="td-label" width="15%">
                �� &nbsp;&nbsp; λ</td>
			<td style="width: 30%">
			    <asp:TextBox ID="txtUnit" CssClass="text-input" ReadOnly="true" runat="server"></asp:TextBox></td>
			<td class="td-label" width="15%">
                �� &nbsp;&nbsp; ��</td>
            <td>
                <asp:TextBox ID="txtInStoragePrice" CssClass="text-input" ReadOnly="true" runat="server"></asp:TextBox></td>
		</tr> 
		<tr>
			<td class="td-label" width="15%">
                �� &nbsp;&nbsp; ��</td>
            <td colspan="3">
                <asp:TextBox ID="txtNumber" CssClass="text-input" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="td-submit" colSpan="4" height="20">
			    <asp:Button ID="btnAdd" Text="��  ��" OnClick="btnAdd_Click" runat="server" />&nbsp;
			    <INPUT id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button" value="��  ��">
			    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			</td>
		</tr>
	</table>
    </form>
</body>
</html>
