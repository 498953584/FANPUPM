<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameset.aspx.cs" Inherits="FrameSet" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head runat="server"><title>工程项目管理系统（业主版）</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script  type="text/javascript">			
			//enter 变换 tab 健
			function document_onkeydownl()
			{
				keycode = window.event.keyCode; //*得到虚拟键值*/
				elementType = window.event.srcElement.type;//*得到当前焦点对象类型*/
				if(keycode == 78 && event.ctrlKey)//*如果是CTRL+N*/
				{
					return false;
				}
				if(keycode == 116)
				{
					return false;
					alert('af');
				}
			}
			
			document.onkeydown = document_onkeydownl;
		</script>
	</head>
 <frameset border="0" framespacing="0" rows="66,7,*,23,0" frameborder="NO" cols="*" id="topknot">
	    <frameset framespacing='0' frameborder='no' cols='*,0'>
	        <frame name='top' src='top.aspx' noresize scrolling='no' id='top' />
	        <!--frame name='NavigationMenu' src='NavigationMenu.aspx' noresize scrolling='no' id='NavigationMenu' /-->
	    </frameset>
	    <frame name='SpaceLine' src='Nav_left.aspx' noresize scrolling='auto' id='SpaceLine' />
        <frameset id='menu' border='0' framespacing='0' frameborder='NO' cols='190,11,*'>
            <frame name='middle_left' src='middle_left.aspx' noresize scrolling='auto' id='middle_left' />
            <frame name='middle_middle' src='middle_middle.aspx' noresize scrolling='no' id='middle_middle' />
            <frame name='frameWorkArea' src='Desktop.aspx' noresize scrolling='auto' id='frameWorkArea' frameborder='NO' />
        </frameset>
	    <frame name='buttom' src='buttom.aspx' noresize scrolling='no' />
	    <frame name='Refresh' src='Refresh.aspx' noresize scrolling='no' id='Refresh' />
	</frameset> 
</frameset>


</html>
