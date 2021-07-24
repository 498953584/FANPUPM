<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WinWebMail.aspx.cs" Inherits="oa_MailAdmin_WinWebMail" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>外部邮箱</title>
    <script language="javascript" type="text/javascript">
		function validatorSpace()
		{		
		    
			//document.frmLoad.action="";
			if( document.all.tb_yhdm.value == "" )
			{
				alert('用户名不能为空！');
				document.all.tb_yhdm.focus();
				return false;
			}
			
			if( document.all.tb_pwd.value == "" )
			{
				alert('密码不能为空！');
				document.all.tb_pwd.focus();
				return false;
			}
			
		
			///邮件登陆
			var yhdm =  document.frmLoad.tb_yhdm.value;
			var pwd = document.frmLoad.tb_pwd.value;						
			//var objTmp = document.getElementById("seljwmail");			
			//if(objTmp.checked==true)
			//{
				document.frmLoad.username.value		=	yhdm;
				//document.frmLoad.pwhidden.value	=	encode(pwd,parseInt(document.frmLoad.picnum.value));
				document.frmLoad.pwhidden.value	=	encode(pwd,parseInt('2323'));
				document.frmLoad.target="_parent";
				document.frmLoad.action="/WinWebMail/default.asp";
				document.frmLoad.tb_yhdm.value	= "";
				document.frmLoad.tb_pwd.value	= "";	
				document.frmLoad.submit();		
				//return false;		
			//}						
			//return true;
		}	
		
	
		///邮件登陆
		function encode(datastr, bassnum)
		{
			var tempstr;
			var tchar;
			var newdata = "";

			for (var i = 0; i < datastr.length; i++)
			{
				tchar = 65535 + bassnum - datastr.charCodeAt(i);
				tchar = tchar.toString();

				while(tchar.length < 5)
				{
					tchar = "0" + tchar;
				}

				newdata = newdata + tchar;
			}

			return newdata;
		}
		</script>
</head>
<body  class="body_clear" scroll="no">
    <form id="frmLoad" name="frmLoad" method="post" runat="server">
    <div>
      <table id="table1" width="100%" height="100%" cellpadding="0" cellspacing="0" border="1" class="table-normal">
        <tr>
            <td class="td-title">
            外部邮箱连接通道
            </td>
        </tr>
        <tr>
            <td style="height:30px">
                <asp:Label ID="lbAlert" Font-Bold="true" Font-Size="Large" ForeColor="Red" runat="server"></asp:Label>
              
            </td>
         </tr>
        <tr><td>
                   <input id="tb_yhdm" name="tb_yhdm" size="16" type="text" style="display:none" runat="server" />
 
           
             <input id="tb_pwd" type="text" size="16" name="tb_pwd" style="display:none" runat="server" />

            <input  type="hidden" maxlength="16" name="pwhidden"/> 
            <input  type="hidden" maxlength="20" size="20"	name="username" />
             <input type="hidden" value="2323" name="picnum"/>
          </td>
         </tr>
        </table>
        </div>
   </form>
 </body>
</html>
\0
