<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendFileEdit.aspx.cs" Inherits="SendFileEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>发文登记</title><meta Name="CODE_LANGUAGE" Content="C#" />
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
	        document.getElementById('hdnSignUserName').value = p[0];
		    document.getElementById('txtSignUserName').value = p[1];
	    }
    }
    function annexEdit()
    {
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
			<TD class="td-head" colSpan="4" height="20">
                发文信息登记</TD>
		</TR>
		<TR>
			<TD class="td-label" width="20%">文件号</TD>
			<TD width = "30%"><asp:TextBox ID="txtFileNumber" CssClass="text-input" TabIndex="1" MaxLength="30" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> 
			<input id="hdnFileNumber" style="WIDTH: 10px" type="hidden" name="hdnFileNumber" runat="server" />

			</TD>
			<TD class="td-label" width="20%">公文标题</TD>
			<TD><asp:TextBox ID="txtTitle" CssClass="text-input" TabIndex="2" Enabled="false" MaxLength="100" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> 
			<input id="hdnTitle" style="WIDTH: 10px" type="hidden" name="hdnTitle" runat="server" />

			</TD>
		</TR>
		<TR>
			<TD class="td-label" width="20%">签发人</TD>
			<TD width = "30%"><asp:TextBox ID="txtSignUserName" CssClass="text-input" TabIndex="3" runat="server"></asp:TextBox>
			<img id="imgPick1" src="../../images/contact.gif" align="bottom" onclick="pickPerson();" visible="false" runat="server" />
<font color="#ff0000">&nbsp;</font> 
			<input id="hdnSignUserName" style="WIDTH: 10px" type="hidden" name="hdnSignUserName" runat="server" />

			</TD>
			<TD class="td-label" width="20%">拟搞部门</TD>
			<TD><asp:TextBox ID="txtDraftDept" CssClass="text-input" TabIndex="4" MaxLength="50" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> 
			<input id="hdnDraftDept" style="WIDTH: 10px" type="hidden" name="hdnDraftDept" runat="server" />

			</TD>
		</TR>
		<TR>
			<TD class="td-label" width="20%">发放范围</TD>
			<TD width = "30%"><asp:TextBox ID="txtIssueRange" CssClass="text-input" TabIndex="5" MaxLength="100" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> 
			<input id="hdnIssueRange" style="WIDTH: 10px" type="hidden" name="hdnIssueRange" runat="server" />

			</TD>
			<TD class="td-label" width="20%">发文页码</TD>
			<TD><asp:TextBox ID="txtIssuePage" CssClass="text-input" TabIndex="6" MaxLength="30" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> 
			<input id="hdnIssuePage" style="WIDTH: 10px" type="hidden" name="hdnIssuePage" runat="server" />

			</TD>
		</TR>  
		<TR>
			<TD class="td-label" width="20%">印刷份数</TD>
			<TD width = "30%"><asp:TextBox ID="txtShareNumber" CssClass="text-input" TabIndex="7" MaxLength="30" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> 
			<input id="hdnShareNumber" style="WIDTH: 10px" type="hidden" name="hdnShareNumber" runat="server" />

			</TD>
			<TD class="td-label" width="20%">发文日期</TD>
			<TD><JWControl:DateBox ID="txtIssueDate" TabIndex="8" runat="server"></JWControl:DateBox><font color="#ff0000">&nbsp;</font> 
			<input id="hdnIssueDate" style="WIDTH: 10px" type="hidden" name="hdnIssueDate" runat="server" />

			</TD>
		</TR> 
		<tr id="tr_add" runat="server"><td class="td-label" width="20%" runat="server">附件</td><td colspan="3" runat="server"><asp:FileUpload ID="FileUpload1" Width="90%" TabIndex="9" runat="server" />
			<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ErrorMessage="文档类型错误，只能上传.doc文件!" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.doc)$|^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.DOC)$" ControlToValidate="FileUpload1" runat="server"></asp:RegularExpressionValidator></td></tr> 
		<tr id="tr_edit" runat="server"><td class="td-label" width="20%" runat="server">附件</td><td colspan="3" runat="server"><a id="annexName" href="#" onclick="annexEdit();" style="cursor : hand" runat="server" />

			<asp:TextBox ID="txtOriginalName" CssClass="text-input" Visible="false" Enabled="false" Width="90%" TabIndex="9" runat="server"></asp:TextBox>
			<input id="hdnFilePath" style="WIDTH: 10px" type="hidden" name="hdnFilePath" runat="server" />
</td></tr> 
		<TR>
			<TD class="td-label" width="20%">备注</TD>
			<TD colSpan="3"><asp:TextBox ID="txtRemark" CssClass="text-input" TabIndex="10" Width="90%" TextMode="MultiLine" MaxLength="250" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> </TD>
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
