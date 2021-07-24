<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrganizationUpdateEdit.aspx.cs" Inherits="HR_Organization_OrganizationUpdateEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>资料管理</title>
	<script language ="javascript">
	<!--
	window.name = "win";
	function UpFile()
	{			
		var RecordCode = document.getElementById('HdnRecordCode').value;//编号
		var url = "../../../commonWindow/Annex/AnnexList.aspx?mid=28&rc="+RecordCode+"&at=0";
		var ref = window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
		//if(ref)
		//{
		//    return true;
		//}
		//return false;
		return true;
	}
	  function checklen(e,maxlength)
	    {
			 if(e.value.length > maxlength)
			 {
				alert('输入长度不能大于'+maxlength);
				e.focus();
				 return false;
			 }
	    }
	-->
	</script>
</head>
<body scroll="no" class="body_popup">
    <form id="Formx" target="win" runat="server">
        <table class="table-normal" id="tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colSpan="4" height="20">
                人力资源年度规划
            </td>
		</tr>  
		<tr>
			<td class="td-label" width="20%">
                调整时间</td>
			<td>
			    <JWControl:DateBox ID="txtRecordDate" ReadOnly="false" CssClass="text-input" style="width:99%" runat="server"></JWControl:DateBox>
			</td>
			<td class="td-label" width="20%">
                申请部门</td>
			<td>
			    <asp:TextBox ID="txtCorpCode" Enabled="false" CssClass="text-input" style="width:99%" runat="server"></asp:TextBox>
			</td>
		</tr> 
		<tr>
			<td class="td-label" width="20%">
                调整内容</td>
            <td colSpan="3">
                   <asp:TextBox ID="txtAdjustContent" CssClass="text-input" onkeyup="checklen(this,250);" style="width:99%" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
            </td>
		</tr>
		<tr>
			<td class="td-label" width="20%">
                调整原因
            </td>
            <td colSpan="3">
                <asp:TextBox ID="txtAdjustReason" CssClass="text-input" onkeyup="checklen(this,250);" style="width:99%" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
            </td>
		</tr>   
		<tr>
			<td class="td-label" width="20%">
                情况说明</td>
            <td colSpan="3">
                <asp:TextBox ID="txtRemark" CssClass="text-input" onkeyup="checklen(this,200);" style="width:99%" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
            </td>
		</tr> 
		<tr>
			<td class="td-submit" colSpan="4" height="20">
			<asp:Button ID="btnAdd" Text="保  存" OnClick="btnAdd_Click" runat="server" />&nbsp;
			<INPUT id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button" value="关  闭">
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			</td>
		</tr>
	</table>
    </form>
</body>
</html>
