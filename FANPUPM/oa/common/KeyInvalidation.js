<!--
function document_onkeydown()
{
 /*
        //8   退格键
        //78  Ctrl+N
        //37  Alt+ 方向键 ← 
        //39  Alt+ 方向键 →
        //116       F5 刷新键
        //82  Ctrl + R
        //121       shift+F10
        //115       屏蔽Alt+F4
        //屏蔽 shift 加鼠标左键新开一网页
        if (window.event.keyCode==8
            ||event.keyCode==78
            ||event.keyCode==37
            ||event.keyCode==39
            ||event.keyCode==116
            ||event.keyCode==82
            ||event.keyCode==121
            ||event.keyCode==115
            ||(window.event.srcElement.tagName == "A" && window.event.shiftKey))
            {
                    //alert('请通过事物代码进行操作！');
                    event.keyCode = 0;
					window.event.cancelBubble=true;
					window.event.returnValue=false;
					return false;
            }   
*/
		keycode = window.event.keyCode; //*得到虚拟键值*/
		elementType = window.event.srcElement.type;//*得到当前焦点对象类型*/

		if(keycode == 78 && event.ctrlKey)//*如果是CTRL+N*/
		{
			event.keyCode = 0;
            window.event.cancelBubble=true;
            window.event.returnValue=false;
            return false;
		}    
		if((keycode == 116) || (keycode == 82 && window.event.ctrlKey))  //*如果是F5或者Ctrl+R刷新
		{
			event.keyCode = 0;
            window.event.cancelBubble=true;
            window.event.returnValue=false;
            return false;
		}
		if(keycode == 121 && (window.event.shiftKey || window.event.shiftLeft))  //*屏蔽shift+F10右键菜单
		{
			event.keyCode = 0;
            window.event.cancelBubble=true;
            window.event.returnValue=false;
            return false;
		}
		if(keycode == 122 || keycode == 121)   //*屏蔽F11/F10
		{
			event.keyCode = 0;
            window.event.cancelBubble=true;
            window.event.returnValue=false;
            return false;
		}
}

document.onkeydown = document_onkeydown;
//-->