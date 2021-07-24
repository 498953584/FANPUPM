<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatCountEdit.aspx.cs" Inherits="HR_Leave_StatCountEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>请休假统计</title>
    <script type="text/javascript" language="javascript">
    <!--
    window.name = "win";
       -->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" target="win" runat="server">
    <div>
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0" class="table-normal">
        <tr>               
            <td class="td-head" colspan="4">
                请休假编辑</td>
        </tr>
        <tr>
	<td class="td-label">
		迟到
	</td>
	<td >
		<asp:TextBox ID="txtLater" runat="server"></asp:TextBox>
	</td>
	<td class="td-label">
		早退
	</td>
	<td >
		<asp:TextBox ID="txtLeaveEarly" runat="server"></asp:TextBox>
	</td></tr>
	<tr>
	<td class="td-label">
		事假
	</td>
	<td >
		<asp:TextBox ID="txtHoliday1" runat="server"></asp:TextBox>
	</td>
	<td class="td-label">
		带薪婚假
	</td>
	<td >
		<asp:TextBox ID="txtHoliday2" runat="server"></asp:TextBox>
	</td></tr>
	<tr>
	<td class="td-label">
		带薪年假
	</td>
	<td >
		<asp:TextBox ID="txtHoliday3" runat="server"></asp:TextBox>
	</td>
	<td class="td-label">
		工伤
	</td>
	<td >
		<asp:TextBox ID="txtHoliday4" runat="server"></asp:TextBox>
	</td></tr>
	<tr>
	<td class="td-label">
		病假
	</td>
	<td >
		<asp:TextBox ID="txtHoliday5" runat="server"></asp:TextBox>
	</td>
	<td class="td-label">
		产假
	</td>
	<td >
		<asp:TextBox ID="txtHoliday6" runat="server"></asp:TextBox>
	</td></tr>
	<tr>
	<td class="td-label">
		丧假
	</td>
	<td >
		<asp:TextBox ID="txtHoliday7" runat="server"></asp:TextBox>
	</td>
	    <td class="td-label">
	        旷工
	    </td>
	    <td>
	        <asp:TextBox ID="txtHoliday8" runat="server"></asp:TextBox>
	    </td>
	</tr>	
	<tr>
	    <td class="td-label">应到天数</td>
	    <td colspan="3">
	        <asp:TextBox ID="txtFactDay" Width="96%" runat="server"></asp:TextBox>
	    </td>
	</tr>	
	<tr>
			<td  align="center" colspan="4" class="td-submit" style="height: 24px">
					<asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;<input id="BtnClose" onclick="window.close();" type="button" value="关 闭"/>&nbsp;               
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtLater" Display="None" ErrorMessage="输入的数据格式不正确!" ValidationExpression="(^[0-9]*$)|(^[0-9]+(.[0,5]{1})$)" runat="server"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtLater" Display="None" ErrorMessage="不能为空!" runat="server"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtLeaveEarly" Display="None" ErrorMessage="输入的数据格式不正确!" ValidationExpression="(^[0-9]*$)|(^[0-9]+(.[0,5]{1})$)" runat="server"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtLeaveEarly" Display="None" ErrorMessage="不能为空!" runat="server"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtHoliday1" Display="None" ErrorMessage="输入的数据格式不正确!" ValidationExpression="(^[0-9]*$)|(^[0-9]+(.[0,5]{1})$)" runat="server"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtHoliday1" Display="None" ErrorMessage="不能为空!" runat="server"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtHoliday2" Display="None" ErrorMessage="输入的数据格式不正确!" ValidationExpression="(^[0-9]*$)|(^[0-9]+(.[0,5]{1})$)" runat="server"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtHoliday2" Display="None" ErrorMessage="不能为空!" runat="server"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtHoliday3" Display="None" ErrorMessage="输入的数据格式不正确!" ValidationExpression="(^[0-9]*$)|(^[0-9]+(.[0,5]{1})$)" runat="server"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtHoliday3" Display="None" ErrorMessage="不能为空!" runat="server"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtHoliday4" Display="None" ErrorMessage="输入的数据格式不正确!" ValidationExpression="(^[0-9]*$)|(^[0-9]+(.[0,5]{1})$)" runat="server"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtHoliday4" Display="None" ErrorMessage="不能为空!" runat="server"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="txtHoliday5" Display="None" ErrorMessage="输入的数据格式不正确!" ValidationExpression="(^[0-9]*$)|(^[0-9]+(.[0,5]{1})$)" runat="server"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtHoliday5" Display="None" ErrorMessage="不能为空!" runat="server"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="txtHoliday6" Display="None" ErrorMessage="输入的数据格式不正确!" ValidationExpression="(^[0-9]*$)|(^[0-9]+(.[0,5]{1})$)" runat="server"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtHoliday6" Display="None" ErrorMessage="不能为空!" runat="server"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator9" ControlToValidate="txtHoliday7" Display="None" ErrorMessage="输入的数据格式不正确!" ValidationExpression="(^[0-9]*$)|(^[0-9]+(.[0,5]{1})$)" runat="server"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtHoliday7" Display="None" ErrorMessage="不能为空!" runat="server"></asp:RequiredFieldValidator>
                                                
                                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ControlToValidate="txtHoliday8" Display="None" ErrorMessage="输入的数据格式不正确!" ValidationExpression="(^[0-9]*$)|(^[0-9]+(.[0,5]{1})$)" runat="server"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtHoliday8" Display="None" ErrorMessage="不能为空!" runat="server"></asp:RequiredFieldValidator>
                                                
                                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ControlToValidate="txtFactDay" Display="None" ErrorMessage="输入的数据格式不正确!" ValidationExpression="(^[0-9]*$)|(^[0-9]+(.[0,5]{1})$)" runat="server"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtHoliday8" Display="None" ErrorMessage="不能为空!" runat="server"></asp:RequiredFieldValidator>
                                                
                                                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
                
            </td>
		</tr>
        </table>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </div>
    </form>
</body>
</html>
