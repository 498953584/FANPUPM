<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetUserAuditPwd.aspx.cs" Inherits="EPC_Workflow_SetUserAuditPwd" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>二次验证密码设置</title>
    <base target="_self" />
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <link href="/Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    
    $(document).ready(function(){
        $("#img").tooltip({showURL:false});
    });
    
     //选择人员
    function pickOperater() {
				var p = new Array();
				p[0] = "";
				p[1] = "";
				var url = "/EPC/WorkFlow/SelectAllUser.aspx";
				
				window.showModalDialog(url,p,"dialogHeight:456px;dialogWidth:600px;center:1;help:0;status:0;");
				if (p[0]!=""){
					document.getElementById('hiddenuser').value = p[0];
					document.getElementById('TbLoginName').value = p[1];
				}
			}
	function CheckNull()
	{
       var txtuser=document.getElementById("TbLoginName");
       if(txtuser.value=="")
       {
         alert("用户名称不能为空");
         return false;
       }
	}
    </script>
    
</head>
<body>
    <form id="form1" onsubmit="return CheckNull();" runat="server">
    <div align="center">
    <table id="Table1" width="500" border="1" class="tableAudit">
				<tr >
					<td colspan="2" class="divHeader">二次验证密码设置</td>
				</tr>
				<tr>
					<td class="td-label" style="width:40px">姓 名</td>
					<td class="td-content"><span class="spanSelect" style="width: 320px; float: left">
                        <asp:TextBox ID="TbLoginName" Style="width:300px; height: 15px; border: none; line-height: 16px;
                            margin: 1px 2px;" runat="server"></asp:TextBox>                          
                        <img alt="选择人员" onclick="pickOperater();" src="/images1/iconSelect.gif" />
                    </span>  <input type="hidden" id="hiddenuser" runat="server" />

                  </td>
				</tr>
				<tr>
					<td class="td-label">新密码</td>
					<td class="td-content"><asp:TextBox ID="tbNewPwd1" Width="180px" TextMode="Password" runat="server"></asp:TextBox>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="必填" ControlToValidate="tbNewPwd1" runat="server">*</asp:RequiredFieldValidator><img id="img"  alt="" title='注意：用户参与的所有需要流程密码的地方都一致，此处修改后，该用户统一使用新审核密码' src="../../images/help.jpg" /></td>
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
