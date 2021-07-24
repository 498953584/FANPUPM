<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRLayoutEdit.aspx.cs" Inherits="HR_Organization_HRLayoutEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>资料管理</title>
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
			<td class="td-head" colSpan="2" height="20">
                人力资源年度规划<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
            </td>
		</tr>  
		<tr>
			<td class="td-label" width="20%">
                规划名称</td>
			<td>
			    <asp:TextBox ID="txtLayoutName" CssClass="text-input" style="width:99%" MaxLength="100" runat="server"></asp:TextBox>
			</td>
		</tr> 
		<tr>
			<td class="td-label" width="20%">
                添加日期</td>
            <td>
                <JWControl:DateBox ID="txtLayoutDate" CssClass="text-input" runat="server"></JWControl:DateBox>    
            </td>
		</tr>
		<tr>
			<td class="td-label" width="20%">
                添加人</td>
			<td>
                <asp:Label ID="lblAddPerson" runat="server"></asp:Label>
            </td>
		</tr>
		<tr>
			<td class="td-label" width="20%">
                附件
            </td>
            <td noWrap="true">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                <asp:Label ID="lblAnnex" runat="server"></asp:Label>
                    </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="btnAnnex" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
                <asp:Button ID="btnAnnex" Text="附件上传" OnClick="btnAnnex_Click" runat="server" />
            </td>
		</tr>   
		<tr>
			<td class="td-label" width="20%">
                情况说明</td>
            <td>
                <asp:TextBox ID="txtRemark" CssClass="text-input" onkeyup="checklen(this,250);" style="width:99%" TextMode="MultiLine" Rows="6" runat="server"></asp:TextBox>
            </td>
		</tr> 
		<tr>
			<td class="td-submit" colSpan="2" height="20">
			<asp:Button ID="btnAdd" Text="保  存" OnClick="btnAdd_Click" runat="server" />&nbsp;
			<INPUT id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button" value="关  闭">
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			<input id="HdnRecordCode" type="hidden" style="width:20px" runat="server" />

			</td>
		</tr>
	</table>
    </form>
</body>
</html>
