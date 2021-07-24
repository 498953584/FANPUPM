<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndividualDrawApplyEdit.aspx.cs" Inherits="oa_WorkManage_StorageManage_IndividualDrawApplyEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>���������¼</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
        <table class="table-normal" id="tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colSpan="2" height="20">
                ���������¼
            </td>
		</tr>
		<tr>
		    <td class="td-label" width="15%">��������</td>
		    <td>
                <JWControl:DateBox ID="txtApplyDate" CssClass="text-input" runat="server"></JWControl:DateBox></td>
		</tr>  
		<tr>
			<td class="td-label" width="15%">������</td>
			<td>
			    <asp:TextBox ID="txtApplyPerson" CssClass="text-input" ReadOnly="true" runat="server"></asp:TextBox>
			    <img src="../../../StyleCss/add.gif" id="imgsel1" style="cursor:hand" onclick="SelectPerson('a');" runat="server" />

			    <input id="HdnApplyPerson" value="0" style="width: 20px" type="hidden" runat="server" />

			</td>
		</tr>
		<tr>
			<td class="td-label" width="15%">
                ʹ����</td>
            <td style="width: 25%">
                <asp:TextBox ID="txtUserPerson" CssClass="text-input" ReadOnly="true" runat="server"></asp:TextBox>
                <img src="../../../StyleCss/add.gif" id="imgsel2" style="cursor:hand" onclick="SelectPerson('u');" runat="server" />

			    <input id="HdnUserPerson" value="0" style="width: 20px" type="hidden" runat="server" />

            </td>
		</tr>
		<tr>
			<td class="td-submit" colSpan="2" height="20">
			    <asp:Button ID="btnAdd" Text="��  ��" OnClick="btnAdd_Click" runat="server" />&nbsp;
			    <INPUT id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button" value="��  ��">
			    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			</td>
		</tr>
	</table>
    </form>
</body>
</html>
