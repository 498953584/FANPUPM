<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetAuditPwd.aspx.cs" Inherits="oa_SysAdmin_UserSet_PasswordSet_SetAuditPwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>二次验证密码设置</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    
    $(document).ready(function(){
        $("#img").tooltip({showURL:false});
    });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table id="Table1" width="600" border="1" class="tableAudit">
				<tr >
					<td colspan="2" class="divHeader">二次验证密码设置</td>
				</tr>
				<tr>
					<td class="td-label" style="width:40px">姓 名</td>
					<td class="td-content"><asp:TextBox ID="tbLoginName" Width="180px" ReadOnly="true" runat="server"></asp:TextBox></td>
				</tr>
                <tr>
					<td class="td-label">旧密码</td>
					<td class="td-content"><asp:TextBox ID="tbOldPwd" Width="180px" TextMode="Password" runat="server"></asp:TextBox>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="必填" ControlToValidate="tbOldPwd" runat="server">*</asp:RequiredFieldValidator><img id="img1" style="vertical-align:middle"  alt="" title='1、“二次验证密码”功能：用于审核时的针对性验证，可以有效防止离开坐位时，或者遭到恶意入侵时的审核权的安全；<br/>2、需要注意的是，只有在“通用流程设置”模块选中“需要二次验证”，本功能才能生效。' src="../../images/help.jpg" /></td>
				</tr>
				<tr>
					<td class="td-label">新密码</td>
					<td class="td-content"><asp:TextBox ID="tbNewPwd1" Width="180px" TextMode="Password" runat="server"></asp:TextBox>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="必填" ControlToValidate="tbNewPwd1" runat="server">*</asp:RequiredFieldValidator><img id="img" style="vertical-align:middle"  alt="" title='1、“二次验证密码”功能：用于审核时的针对性验证，可以有效防止离开坐位时，或者遭到恶意入侵时的审核权的安全；<br/>2、需要注意的是，只有在“通用流程设置”模块选中“需要二次验证”，本功能才能生效。' src="../../images/help.jpg" /></td>
				</tr>
				<tr>
					<td class="td-label">确认新密码</td>
					<td class="td-content">
							<asp:TextBox ID="tbNewPwd2" Width="180px" TextMode="Password" runat="server"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" ControlToCompare="tbNewPwd1" ControlToValidate="tbNewPwd2" ErrorMessage="两次输入密码不一致" runat="server"></asp:CompareValidator>
							</td>
				</tr>
			
				<tr>
					<td colspan="2" align="center">
						<asp:Button ID="btMod" Text=" 修 改 " OnClick="btMod_Click" runat="server" /></td>				
				</tr>
	</table>
    </div>
        <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
