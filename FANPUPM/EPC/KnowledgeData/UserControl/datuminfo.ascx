<%@ Control Language="C#" AutoEventWireup="true" CodeFile="datuminfo.ascx.cs" Inherits="DatumInfo" %>

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
<table id="tbBasInfo" height="100%" class="table-form" cellpadding="0" cellspacing="0" border="0" width="98%" runat="server"><tr runat="server"><td colspan="4" class="td-head" runat="server">资 料 管 理
		</td></tr><tr runat="server"><td class="td-label" width="20%" runat="server">标&nbsp;&nbsp;&nbsp;&nbsp;题：</td><td colspan="3" runat="server"><asp:TextBox ID="TxtTitle" CssClass="text-input" Width="280px" MaxLength="50" runat="server"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="标题不能为空" ControlToValidate="TxtTitle" runat="server"></asp:RequiredFieldValidator></td></tr><tr runat="server"><td class="td-label" runat="server"><nobr>所属分类：</nobr></td><td colspan="3" runat="server"><asp:DropDownList ID="DDL_Class" runat="server"></asp:DropDownList></td></tr><tr runat="server"><td class="td-label" width="20%" runat="server"><FONT face="宋体">添加人：</FONT></td><td colspan="3" runat="server"><asp:Label ID="LabAuthor" runat="server">Label</asp:Label></td></tr><tr runat="server"><td class="td-label" runat="server">正&nbsp;&nbsp;&nbsp;&nbsp;文：</td><td colspan="3" runat="server">
			<asp:TextBox ID="TxtText" Width="580px" Height="280px" TextMode="MultiLine" onkeyup="checklen(this,3000);" runat="server"></asp:TextBox></td></tr><tr style="display:none" runat="server"><td class="td-label" runat="server">可&nbsp;&nbsp;&nbsp;&nbsp;见：</td><td colspan="3" runat="server">
			<asp:RadioButton ID="rb_y" GroupName="y" Text="是" Checked="true" runat="server" />
			<asp:RadioButton ID="rb_n" GroupName="y" Text="否" runat="server" /></td></tr></table>
<asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
