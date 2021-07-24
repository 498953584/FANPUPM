<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uscheduleplan.ascx.cs" Inherits="USchedulePlan" %>

<TABLE class="table-none" id="Tablexx" cellSpacing="0" cellPadding="0" width="100%" border="1">
	<TR>
		<TD class="td-label" width="15%">编码</TD>
		<TD width="35%"><asp:TextBox ID="TxtTaskCode" Enabled="false" runat="server"></asp:TextBox></TD>
		<TD class="td-label" width="15%">任务名称</TD>
		<TD><asp:TextBox ID="TxtTaskName" runat="server"></asp:TextBox></TD>
	</TR>
	<TR>
		<TD class="td-label">开始时间</TD>
		<TD><JWControl:DateBox ID="DtxStartDate" runat="server"></JWControl:DateBox></TD>
		<TD class="td-label">结束时间</TD>
		<TD><JWControl:DateBox ID="DtxEndDate" runat="server"></JWControl:DateBox></TD>
	</TR>
	<TR>
		<TD class="td-label">任务类型</TD>
		<TD><LABEL><asp:DropDownList ID="DDLTaskType" Enabled="false" runat="server"><asp:ListItem Value="3" Text="分项工程" /><asp:ListItem Value="1" Text="单位工程" /><asp:ListItem Value="2" Text="分部工程" /></asp:DropDownList></LABEL></TD>
		<TD class="td-label">工期</TD>
		<TD><asp:TextBox onkeypress="funChkInt();" ID="TxtWorkDay" runat="server"></asp:TextBox>天</TD>
	</TR>
	<TR>
		<TD class="td-label">单位</TD>
		<TD><asp:TextBox ID="TxtUnit" runat="server"></asp:TextBox></TD>
		<TD class="td-label">工程量</TD>
		<TD><asp:TextBox ID="TxtQuantity" ReadOnly="true" runat="server"></asp:TextBox></TD>
	</TR>
	<TR>
		<TD class="td-label">是否关键任务</TD>
		<TD><LABEL><asp:CheckBox ID="ChkPivotal" runat="server" /></LABEL></TD>
		<TD class="td-label">任务状态</TD>
		<TD><asp:DropDownList ID="DDLTaskState" runat="server"><asp:ListItem Value="4" Text="在建" /><asp:ListItem Value="6" Text="停建" /><asp:ListItem Value="7" Text="竣工" /></asp:DropDownList></TD>
	</TR>
	<TR>
		<TD class="td-label">安全措施</TD>
		<TD colSpan="3"><LABEL><asp:TextBox ID="TxtSafety" Rows="5" Width="100%" TextMode="MultiLine" runat="server"></asp:TextBox></LABEL></TD>
	</TR>
	<TR>
		<TD class="td-label">质量规范</TD>
		<TD colSpan="3"><asp:TextBox ID="TxtQuality" Rows="5" Width="100%" TextMode="MultiLine" runat="server"></asp:TextBox></TD>
	</TR>
	<TR>
		<TD class="td-label">备注</TD>
		<TD colSpan="3"><asp:TextBox ID="TxtRemark" Width="100%" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox></TD>
	</TR>
</TABLE>
<asp:RequiredFieldValidator ID="RFValStartDate" ControlToValidate="DtxStartDate" Display="None" ErrorMessage="开始时间不能为空!" runat="server"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RFValEndDate" ControlToValidate="DtxEndDate" Display="None" ErrorMessage="结束时间不能为空!" runat="server"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RFValTaskCode" ErrorMessage="任务编号不能为空!" Display="None" ControlToValidate="TxtTaskCode" runat="server"></asp:RequiredFieldValidator>
<asp:ValidationSummary ID="ValSErrMsg" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" runat="server"></asp:ValidationSummary>
