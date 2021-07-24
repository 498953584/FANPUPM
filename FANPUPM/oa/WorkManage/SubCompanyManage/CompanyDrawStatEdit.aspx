<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyDrawStatEdit.aspx.cs" Inherits="oa_WorkManage_SubCompanyManage_CompanyDrawStatEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>���������¼</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
	    function SelectPerson(t)
	    {
	        var human = new Array();
	        human[0] = "";
		    var url= "../../../CommonWindow/PickSinglePerson.aspx";
		    window.showModalDialog(url,human,"dialogHeight:380px;dialogWidth:320px;center:1;help:0;status:0;");
		    if(human[0] != "")
		    {
		        if(t == "a")
		        {
		            document.getElementById('HdnApplyPerson').value = human[0];
		            document.getElementById('txtApplyPerson').value = human[1];
		        }
		        if(t == "u")
		        {
		            document.getElementById('HdnUserPerson').value = human[0];
		            document.getElementById('txtUserPerson').value = human[1];
		        }
		    }
	}
	-->
	</script>
</head>
<body scroll="no" class="body_popup">
    <form id="Formx" runat="server">
        <table class="table-form" id="tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colSpan="4" height="20">
                ���������¼
            </td>
		</tr>  
		<tr>
			<td class="td-label" width="20%">
                ���</td>
			<td><asp:TextBox ID="txtYear" CssClass="text-input" Enabled="false" runat="server"></asp:TextBox>
			</td>
			<td class="td-label" width="20%">
                �·�</td>
			<td><asp:TextBox ID="txtMonth" CssClass="text-input" runat="server"></asp:TextBox>
			</td>
		</tr> 
		<tr>
			<td class="td-label" width="20%">
                ��������</td>
            <td colspan="3">
                <asp:DropDownList ID="DDLApplyType" Enabled="false" runat="server"><asp:ListItem Value="P" Text="����" /><asp:ListItem Value="D" Text="����" /></asp:DropDownList></td>
		</tr>
		<tr>
			<td class="td-label">
                ���˵��</td>
            <td colspan="3">
                <asp:TextBox ID="txtRemark" CssClass="text-input" MaxLength="250" style="width:100%" TextMode="MultiLine" Rows="6" runat="server"></asp:TextBox></td>
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
