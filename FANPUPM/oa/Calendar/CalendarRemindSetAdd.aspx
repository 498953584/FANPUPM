<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalendarRemindSetAdd.aspx.cs" Inherits="oa_Calendar_CalendarRemindSetAdd" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>提醒设置</title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self"/>
	<script language="javascript" type="text/javascript">
	function Exppress(obj)
	{
	    if (obj.value!="")
		{
			if (!(new RegExp(/^\d+$/).test(obj.value)))
			{
				alert('完成度应该是整数！');
				obj.focus();
				return;
			}
			else
			{
				if (parseInt(obj.value)>23)
				{
					alert('完成度应该小于等于23！');
					obj.focus();
					return;
				}
			}
		}
		else
		{
			obj.value = 0;
		}
	}
	
	function Exppress2(obj)
	{
	    if (obj.value!="")
		{
			if (!(new RegExp(/^\d+$/).test(obj.value)))
			{
				alert('完成度应该是整数！');
				obj.focus();
				return;
			}
			else
			{
				if (parseInt(obj.value)>59)
				{
					alert('完成度应该小于等于59！');
					obj.focus();
					return;
				}
			}
		}
		else
		{
			obj.value = 0;
		}
	}
	
	</script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
    <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
          <tr>
            <td colspan="2" class="td-title">提示周期设置</td>
          </tr>
          <tr>
            <td width="30%" class="td-label">     
              </td>
            <td width="70%">
                <asp:CheckBox ID="CBIsSms" Text="短信" runat="server" />
                <asp:CheckBox ID="CBIsMessage" Text="消息" runat="server" />
             </td>
          </tr>
          <tr>
            <td class="td-label">提醒方式:</td>
            <td>
               <asp:DropDownList ID="DDLRemindType" runat="server"><asp:ListItem Value="2" Text="每 天" /><asp:ListItem Value="3" Text="每 周" /><asp:ListItem Value="4" Text="每 月" /></asp:DropDownList>
               </td>
          </tr>
          <tr>
          <td class="td-label">
              提醒时间:</td>
            <td  >            
                 <asp:TextBox ID="TxtHours" Width="60px" onblur="Exppress(this);" Text="0" runat="server"></asp:TextBox>
                时<asp:TextBox ID="TxtMinutes" Width="60px" onblur="Exppress2(this);" Text="0" runat="server"></asp:TextBox>分        		  
	        </td>
          </tr>
          <tr>
            <td class="td-label">结束时间:</td>
          
            <td>
              <JWControl:DateBox ID="DBEndDate" Width="90px" runat="server"></JWControl:DateBox>结束
            </td>
          </tr>
          <tr>
					<td  align="center" colspan="2" class="td-submit"><asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;<input id="BtnClose" onclick="window.close();" type="button" value="关 闭"/>&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="DBEndDate" Display="None" ErrorMessage="结束时间不能为空!" runat="server"></asp:RequiredFieldValidator>&nbsp;
                        <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
					</td>
				</tr>
</table><JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    
    </div>
    </form>
</body>
</html>
