<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyDocumentEdit.aspx.cs" Inherits="MyDocumentEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>我的公文登记</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self"></base>
	<script language ="javascript">
	function pickPerson()
    {
        var p = new Array();
	    p[0] = "";
	    p[1] = "";
	    var url = "";
	    url = "../../CommonWindow/PickSinglePerson.aspx";
	    window.showModalDialog(url,p,"dialogHeight:420px;dialogWidth:430px;center:1;help:0;status:0;");
	    if (p[0]!="")
	    {
	        document.getElementById('hdnUserCode').value = p[0];
		    document.getElementById('txtUserName').value = p[1];
	    }
    }
    function UpFile(RID)
	{			
		var RecordCode = RID;//编号
		var url = "../../commonWindow/Annex/AnnexList.aspx?mid=1&rc="+RecordCode+"&at=0";
		var ref = window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
		//if(ref)
		//{
		//    return true;
		//}
		//return false;
		return true;
	}
	</script>
</head>
<body scroll="no" class="body_popup">
    <form id="Form1" method="post" runat="server">
    <TABLE class="table-form" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<TR>
			<TD class="td-head" colSpan="2" height="20">
                我的公文信息登记<input id="hdnFilePath" style="WIDTH: 10px" type="hidden" name="hdnFilePath" runat="server" />
</TD>
		</TR>		
		<TR>
			<TD class="td-label" width="25%">公文标题</TD>
			<TD><asp:TextBox ID="txtTitle" Width="80%" CssClass="text-input" TabIndex="2" MaxLength="100" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> 
			<input id="hdnTitle" style="WIDTH: 10px" type="hidden" name="hdnTitle" runat="server" />

                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtTitle" Display="None" ErrorMessage="公文标题必须填写" runat="server"></asp:RequiredFieldValidator></TD>
		</TR>
		<!--
		<TR>
			<TD class="td-label" width="25%">起草人</TD>
			<TD><asp:TextBox ID="txtUserName" Width="80%" CssClass="text-input" TabIndex="3" runat="server"></asp:TextBox>
			<img id="imgPick1" src="../../images/contact.gif" align="bottom" onclick="pickPerson();" runat="server" />
<font color="#ff0000">&nbsp;</font> 
			<input id="hdnUserCode" style="WIDTH: 10px" type="hidden" name="hdnUserCode" runat="server" />

			</TD>
		</TR> -->
		<TR>
			<TD class="td-label" width="25%">起草时间</TD>
			<TD><asp:TextBox ID="txtRecordDate" Width="90px" TabIndex="4" ReadOnly="true" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> 
			<input id="hdnRecordDate" style="WIDTH: 10px" type="hidden" name="hdnRecordDate" runat="server" />

			</TD>
		</TR>		
		<TR>
			<TD class="td-label" width="25%">
                审核事项</TD>
			<TD><asp:TextBox ID="txtRemark" CssClass="text-input" TabIndex="10" Width="80%" TextMode="MultiLine" MaxLength="250" Height="90px" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRemark" Display="None" ErrorMessage="审核事项必填" runat="server"></asp:RequiredFieldValidator></TD>
		</TR> 
		<tr runat="server"><td class="td-label" width="25%" runat="server"></td><td runat="server">
                &nbsp;<asp:Button ID="btnAnnex" Text="附件上传" CssClass="button-normal" runat="server" /></td></tr> 
		<TR>
			<TD class="td-submit" colSpan="4" height="20"><asp:Button ID="BtnAdd" Text="保  存" OnClick="BtnAdd_Click" runat="server" />&nbsp;
				<INPUT id="BtnClose" onclick="javascript:window.close();" type="button" value="关  闭" name="BtnClose">
				
                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
			</TD>
		</TR>
	</TABLE><JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
