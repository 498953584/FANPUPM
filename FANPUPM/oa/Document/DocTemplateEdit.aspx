<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocTemplateEdit.aspx.cs" Inherits="DocTemplateEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>公文模板维护</title><meta Name="CODE_LANGUAGE" Content="C#" />
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
		    document.getElementById('txtUserCode').value = p[1];
	    }
    }
    function annexEdit()
    {
//        var url = "http://";
//        
//        url += encodeURI(window.location.host);
//        url += document.getElementById('hdnFilePath').value;
//        openDocObj = new ActiveXObject("SharePoint.OpenDocuments.2");
//        openDocObj.EditDocument(url);
        
        var url = "";
        var filepath = document.getElementById('hdnFilePath').value;
        url = "../common/EditOnLine.aspx?filepath="+filepath;
        window.showModalDialog(url,window,"dialogHeight:800px;dialogWidth:1050px;center:1;help:0;status:0;");
    }
	</script>
</head>
<body scroll="no" class="body_popup">
    <form id="Form1" method="post" runat="server">
    <TABLE class="table-form" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<TR>
			<TD class="td-head" colSpan="4" height="20">公文模板</TD>
		</TR>
		<TR>
			<TD class="td-label" width="25%">模板名称</TD>
			<TD><asp:TextBox ID="txtTemplatName" CssClass="text-input" TabIndex="1" MaxLength="50" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> 
			<input id="hdnTemplatName" style="WIDTH: 10px" type="hidden" name="hdnTemplatName" runat="server" />

			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTemplatName" Display="None" ErrorMessage="模板名称不能为空！" runat="server"></asp:RequiredFieldValidator>
			</TD>
		</TR>
		<TR>
			<TD class="td-label" width="25%">上传人</TD>
			<TD><asp:TextBox ID="txtUserCode" CssClass="text-input" TabIndex="2" ReadOnly="true" runat="server"></asp:TextBox>
			<img id="imgPick1" src="../../images/contact.gif" align="bottom" onclick="pickPerson();" runat="server" />
<font color="#ff0000">&nbsp;</font>
			<input id="hdnUserCode" style="WIDTH: 10px" type="hidden" name="hdnUserCode" runat="server" />

			</TD>
		</TR>
		<TR>
			<TD class="td-label" width="25%">上传时间</TD>
			<TD>
                <JWControl:DateBox ID="DBoxUploadTime" runat="server"></JWControl:DateBox>
			<input id="hdnUploadTime" style="WIDTH: 10px" type="hidden" name="hdnUploadTime" runat="server" />
</TD>
		</TR>
		<tr id="tr_add" runat="server"><td class="td-label" width="25%" runat="server">上传模板</td><td runat="server"><asp:FileUpload ID="txtFilePath" runat="server" /><font color="#ff0000">&nbsp;</font>
			<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ErrorMessage="文档类型错误，只能上传.doc文件!" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.doc)$|^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.DOC)$" ControlToValidate="txtFilePath" runat="server"></asp:RegularExpressionValidator>
                </td></tr>
        <tr id="tr_edit" runat="server"><td class="td-label" width="25%" runat="server">上传模板</td><td runat="server"><a id="annexName" href="#" onclick="annexEdit();" style="cursor : hand" runat="server" />

			<asp:TextBox ID="txtOriginalName" CssClass="text-input" TabIndex="4" Enabled="false" Visible="false" runat="server"></asp:TextBox>
			<asp:ImageButton ID="ImageBtn" ImageUrl="~/images/del.gif" Visible="false" OnClick="ImageBtn_Click" runat="server" /><font color="#ff0000">&nbsp;</font>
			<input id="hdnFilePath" style="WIDTH: 10px" type="hidden" name="hdnFilePath" runat="server" />
</td></tr>
        <TR>
			<TD class="td-label" width="25%">备注</TD>
			<TD><asp:TextBox ID="txtRemark" CssClass="text-input" TabIndex="5" TextMode="MultiLine" MaxLength="250" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font>
		</TR>
		<TR>
			<TD class="td-submit" colSpan="4" height="20"><asp:Button ID="BtnAdd" Text="保  存" OnClick="BtnAdd_Click" runat="server" />&nbsp;
				<INPUT id="BtnClose" onclick="javascript:window.close();" type="button" value="关  闭" name="BtnClose">
				<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			</TD>
		</TR>
	</TABLE>
    </form>
</body>
</html>
