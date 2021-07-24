<%@ Control Language="C#" AutoEventWireup="true" CodeFile="criterion.ascx.cs" Inherits="Criterion" %>

<head>
<script language="javascript">

    function checklen(e,maxlength)
	    {
			 if(e.value.length > maxlength)
			 {
				alert('输入长度不能大于'+maxlength);
				e.focus();
				 return false;
			 }
	    }
</script>
</head>
<TABLE class="table-form" id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD class="td-head" colSpan="2"><asp:Label ID="LblHead" runat="server"></asp:Label></TD>
	</TR>
	<TR vAlign="middle">
		<TD class="td-label" style="width: 129px">记录名称：</TD>
		<TD><asp:TextBox ID="TxtCriterionName" Width="250px" Columns="21" CssClass="text-input" MaxLength="80" runat="server"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtCriterionName" ErrorMessage="RequiredFieldValidator" runat="server">*(必填)</asp:RequiredFieldValidator></TD>
	</TR>
	<TR vAlign="middle">
		<TD class="td-label" style="width: 129px">发布单位：</TD>
		<TD><asp:TextBox ID="TxtPublisher" Width="250px" CssClass="text-input" ReadOnly="true" style="BACKGROUND-COLOR:#ffffc0" runat="server"></asp:TextBox><img id="IMG1" src="/images/contact.gif" onclick="Getdept();" style="CURSOR: hand" align="absmiddle" runat="server" />

			<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TxtPublisher" ErrorMessage="RequiredFieldValidator" runat="server">*</asp:RequiredFieldValidator></TD>
	</TR>
	<TR vAlign="middle">
		<TD class="td-label" style="width: 129px">记录类型：</TD>
		<TD><asp:TextBox ID="txtClass" ReadOnly="true" Width="250px" style="background-color:#FFFFC0;" runat="server"></asp:TextBox><img alt="" src="/images/contact.gif" onclick="GetClass();" id="IMG2" style="CURSOR: hand" align="absmiddle" runat="server" />

			<asp:RequiredFieldValidator ID="Requiredfieldvalidator3" ControlToValidate="TxtPublisher" ErrorMessage="RequiredFieldValidator" runat="server">*</asp:RequiredFieldValidator></TD>
	</TR>
	<TR vAlign="middle">
		<TD class="td-label" style="width: 129px">发 布 人：</TD>
		<td>
			<asp:Label ID="PublisherYhmc" runat="server"></asp:Label></td>
	<TR>
		<TD class="td-label" style="width: 129px">备&nbsp;&nbsp;&nbsp;&nbsp;注：</TD>
		<TD><asp:TextBox ID="TxtRemark" Width="100%" CssClass="textarea-input" Rows="5" TextMode="MultiLine" onkeyup="checklen(this,400);" runat="server"></asp:TextBox></TD>
	</TR>
</TABLE>
<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
