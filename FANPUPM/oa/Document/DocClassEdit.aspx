<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocClassEdit.aspx.cs" Inherits="DocClassEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>公文分类维护</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
</head>
<body scroll="no" class="body_popup">
    <form id="Form1" method="post" runat="server">
    <TABLE class="table-form" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<TR>
			<TD class="td-head" colSpan="4" height="20">公文分类</TD>
		</TR>
		<TR>
			<TD class="td-label" width="25%">分类编码</TD>
			<TD><asp:TextBox ID="txtClassCode" CssClass="text-input" TabIndex="1" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> 
			<input id="hdnClassCode" style="WIDTH: 10px" type="hidden" name="hdnClassCode" runat="server" />

			</TD>
		</TR>
		<TR>
			<TD class="td-label" width="25%">分类名称</TD>
			<TD><asp:TextBox ID="txtClassName" CssClass="text-input" TabIndex="2" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font>
			<input id="hdnClassName" style="WIDTH: 10px" type="hidden" name="hdnClassName" runat="server" />

			</TD>
		</TR>
		<TR>
			<TD class="td-label" width="25%">是否有效</TD>
			<TD><asp:DropDownList ID="ddltIsValid" runat="server"><asp:ListItem Value="1" Text="有效" /><asp:ListItem Value="0" Text="无效" /></asp:DropDownList>
			<input id="hdnIsValid" style="WIDTH: 10px" type="hidden" name="hdnIsValid" runat="server" />
 </TD>
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
