<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Nav_right.aspx.cs" Inherits="SysFrame_Nav_right" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>无标题页</title></head>
<body class="body_clear" ccroll="no">
    <form id="form1" runat="server">
    <div>
    <table width="329" border="0" cellspacing="0" cellpadding="0">
        <tr> 
          <td width="25"><img src="<%=this.strSkinPath %>/top1_jiao1.gif" width="24" height="25" /></td>
          <td width="305" align="right" background="top1_bg1.gif">
          <table width="305" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td><a href="#" onClick="javascript:top.NavigationMenu.history.go(-1);"><img src="<%=this.strSkinPath %>/back.gif" width="61" height="25" border="0" /></a></td>
                <td><a href="#" onClick="javascript:top.NavigationMenu.history.go(1);"><img src="<%=this.strSkinPath %>/go.gif" width="61" height="25" border="0" /></a></td>
                <td><a href="#" onClick="javascript:top.frameWorkArea.history.go(0);"><img src="<%=this.strSkinPath %>/ref.gif" width="61" height="25" border="0" /></a></td>
                <td><a href="#" onclick="javascript:top.frameWorkArea.location.href='ShowInfomation.aspx';top.NavigationMenu.location.href='/SysFrame/NavigationMenu.aspx';"><img src="<%=this.strSkinPath %>/home.gif" width="61" height="25" border="0" /></a></td>
                <td><a href="#" onClick="javascript:if(confirm('确定要退出系统吗？')){top.window.close();}else{return false;}"><img src="<%=this.strSkinPath %>/exit.gif" width="61" height="25" border="0" /></a></td>
              </tr>
           </table>
           </td>
        </tr>
      </table>
    </div>
    </form>
</body><script language="javascript">document.body.oncontextmenu=new Function("return false;");</script>
</html>
