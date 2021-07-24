<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentAudit.aspx.cs" Inherits="DocumentAudit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>公文审核</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self"></base>
</head>
<body class="body_popup" scroll="no" >
    <form id="form1" runat="server">
    <TABLE class="table-form" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
        <TR height="30%">
            <td>
            <TABLE class="table-form" id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		        <TR>
			        <TD class="td-head" colSpan="4" height="20">
                        公文信息</TD>
		        </TR>
		        <TR>
			        <TD class="td-label" width="20%">公文标题</TD>
			        <TD width="30%"><asp:TextBox ID="txtTitle" Width="80%" CssClass="text-input" TabIndex="2" Enabled="false" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> 
			        <input id="hdnTitle" style="WIDTH: 10px" type="hidden" name="hdnTitle" runat="server" />

			        </TD>
			        <TD class="td-label" width="20%">起草人</TD>
			        <TD><asp:TextBox ID="txtUserName" Width="80%" CssClass="text-input" TabIndex="3" Enabled="false" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> 
			        <input id="hdnUserCode" style="WIDTH: 10px" type="hidden" name="hdnUserCode" runat="server" />

			        </TD>
		        </TR>
		        <TR>
			        <TD class="td-label" width="20%">起草时间</TD>
			        <TD width="30%"><JWControl:DateBox ID="txtRecordDate" Width="80%" TabIndex="4" Enabled="false" runat="server"></JWControl:DateBox><font color="#ff0000">&nbsp;</font> 
			        <input id="hdnRecordDate" style="WIDTH: 10px" type="hidden" name="hdnRecordDate" runat="server" />

			        </TD>
			        <TD class="td-label" width="20%">附件</TD>
			        <TD><asp:TextBox ID="txtOriginalName" CssClass="text-input" Width="80%" TabIndex="9" Enabled="false" runat="server"></asp:TextBox>
			        <input id="hdnFilePath" style="WIDTH: 10px" type="hidden" name="hdnFilePath" runat="server" />
</TD>
		        </TR>
		        <TR>
			        <TD class="td-label" width="20%">备注</TD>
			        <TD colspan="3"><asp:TextBox ID="txtRemark" CssClass="text-input" TabIndex="10" Width="80%" TextMode="MultiLine" Enabled="false" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> </TD>
		        </TR> 
		      </TABLE>
		    </td>
		</TR>
		<TR>
		    <td><div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
		            <iframe id="fmAuditEidt" name="fmAuditEidt" frameborder="0" width="100%" scrolling="no" height="100%" runat="server"></iframe>
			    </div>
			</td>
		</TR>
	</TABLE>
	<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
