<%@ Page Language="C#" AutoEventWireup="true" CodeFile="navigationmenu.aspx.cs" Inherits="NavigationMenu" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>NavigationMenu</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />


    <script language="javascript">
		<!--
		    function help(){
		        //var daohang=top.daohang.document.location.href;
		        url='about.htm';//help_path.aspx?mklx='+window.myhelp.value;
		        window.showModalDialog (url,'', 'dialogHeight:340px;dialogWidth:480px;center:1;help:0;status:0;'); 
            }
            
            var NewSizeObj,OldSizeObj;
            
            function doWith(str,obj)
		    {
		        //bincat,2008-3-6
			    NewSizeObj = obj;
			    try{
			        if(NewSizeObj!=OldSizeObj)
			        {
			            NewSizeObj.style.fontWeight="bold";
			            OldSizeObj.style.fontWeight="";
			        }
			    }
			    catch(e){}
			    OldSizeObj = NewSizeObj;
			    
			    if(str=="1")
			    {
			        top.NavigationMenu.history.go(-1);
			    }
			    else if(str=="2")
			    {
			        top.NavigationMenu.history.go(1);
			    }
			    else if(str=="3")
			    {
			        top.frameWorkArea.history.go(0);
			    }
			    else if(str=="4")
			    {
			        top.frameWorkArea.location.href='ShowInfomation.aspx';
			        top.NavigationMenu.location.href='/SysFrame/NavigationMenu.aspx';
			    }
			    else if(str=="5")
			    {
			        if(confirm('确定要退出系统吗？')){top.window.close();}else{return false;}
			    }
		    }
    function keyDown()
    {
    var keycode=event.keyCode;                 
    var keychar=String.fromCharCode(keycode);     
    alert('SCII='+keycode+'\nKeyChar='+keychar); 
    }
		//-->
    </script>
</head>
<body leftmargin="0" topmargin="0" marginheight="0" marginwidth="0" class="no">
    <form id="Form1" method="post" onkeydown="keydown()" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" >
            <tr style="background-color:#E7F3FE">
                <td height="23px" bgcolor="#E7F3FE">
                    <img src="<%=this.strSkinPath %>/adress_dot.gif" width="22" height="17" align="absmiddle">您当前所在位置：<asp:Label ID="l_title" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
<script language="javascript">document.body.oncontextmenu=new Function("return false;");</script>
</html>
