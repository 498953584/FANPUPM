// JScript 文件
<!--

function ajaxGetReloadLoaction( url )
{
    url += "" ;
    var now = new Date() ;
    if( url.indexOf( "reftime=begin" ) == -1 )
    {
        var prifix = url.indexOf( "?" ) == -1 ? "?" : "&" ;
        url += prifix + "reftime=begin" + now.getTime() + "end";
    }
    else
    {
        url = url.replace( /reftime=begin[1234567890]+end/, "reftime=begin" + now.getTime() + "end" ) ;
    }
    return url ;
}

/// <summary>
/// 对给定的地址进行异步调用，返回响应内容
/// </summary>
/// <param name="url">String类型，异步调用的目标地址</param>
/// <param name="submitDate">String类型，提交的目标的数据，将以post方式发送</param>
/// <returns>目标地址的响应内容</returns>
function ajaxSubmit( url, submitDate )
{    
    var httpRequest ;

    if( typeof XMLHttpRequest != 'undefined' )
        httpRequest = new XMLHttpRequest() ;
    else if( typeof ActiveXObject != 'undefined' )
        httpRequest = new ActiveXObject( 'Microsoft.XMLHTTP' ) ;

    if( httpRequest )
    {
        var date = "&date=" + submitDate ;

        httpRequest.open( 'POST', url, false ) ;
        httpRequest.setRequestHeader( "content-length", date.length ) ;
        httpRequest.setRequestHeader( "Content-Type", "application/x-www-form-urlencoded; charset=UTF-8" ) ;
        httpRequest.send( date ) ;

        var result = null ;
        if( httpRequest.status == 200 )
            result = httpRequest.responseText.toString() ;
  
        return result ;
    }
}

function toolbox1_oncommand(cmd)
{	
	 var desktop = top.document.frames[1].window.frames["view1"].window.desktop ; 
        var isopen = desktop.isOpen( loc( ajaxGetReloadLoaction( "navigationmenu.aspx?code=" + cmd.command ) )); 
       // alert(isopen)
        if ( isopen == -1)
        {
            wnd = desktop.newWindow() ;         
            if( wnd != null )
            {
                var title = cmd.title;
                wnd.setCaption( title ) ;
                
                var url = cmd.command;
                wnd.setLinkPath( ajaxGetReloadLoaction( "navigationmenu.aspx?code=" + url ) ) ;
            }
            ShowActiveNewButton();
        }
        else
        {          
            desktop.setActive(isopen);
            ShowActiveOldButton(desktop.getTaskBar().getBtnList()[isopen].getBtn());
        }
}

function toolbox_oncommand(cmd,title) {	
	// 使用新的界面
	if (top._openTab) {
		top._openTab({ title: title, url:cmd });
		return ;
	}
	//火狐浏览器
	if (navigator.userAgent.indexOf('Firefox') >= 0)
	{
		window.open(cmd,title);
	}else
	{
		var desktop = top.frameWorkArea.window.desktop ; 
		var isopen = desktop.isOpen( loc( ajaxGetReloadLoaction( cmd ) )); 
		// alert(isopen)
		if ( isopen == -1)
		{
			wnd = desktop.newWindow() ;         
			if( wnd != null )
			{
                   
				wnd.setCaption( title ) ;
                    
				var url = cmd;
				wnd.setLinkPath( ajaxGetReloadLoaction(  url ) ) ;
			}
			ShowActiveNewButton();
		}
		else
		{          
			desktop.setActive(isopen);
			ShowActiveOldButton(desktop.getTaskBar().getBtnList()[isopen].getBtn());
		}
	}     
}
function loc(src)
    {
        var pos;
		pos = src.indexOf("reftime");
		var temp;
		temp = src.substring(0,pos -1); 
		return temp;
    }
 var getFFVersion=navigator.userAgent.substring(navigator.userAgent.indexOf("Firefox")).split("/")[1]
    var FFextraHeight=getFFVersion>=0.1? 16 : 0 

    function dyniframesize(iframename)
    {
       var pTar = null;
       if (document.getElementById)
       {
         pTar = document.getElementById(iframename);
       }
       else
       {
         eval('pTar = ' + iframename + ';');
       }
       if (pTar && !window.opera)
       {

         pTar.style.display="block"
        
         if (pTar.contentDocument && pTar.contentDocument.body.offsetHeight)
         {
	       pTar.height=document.body.clientHeight
         }
         else if (pTar.Document && pTar.Document.body.scrollHeight)
         {
	       pTar.height=document.body.clientHeight;
         }
       }
    }
    
    //ck.显示最新打开窗口
    function ShowActiveNewButton()
    {
        var obj = top.frameWorkArea.document.getElementById("ToolBarTd").childNodes(0);
        //alert(obj.innerHTML);
        var nlen = obj.childNodes(0).offsetWidth;
        var wlen = obj.offsetWidth;
        if( nlen-wlen > 0 )
        {
            obj.scrollLeft = nlen - wlen ;
        }
    }
    
    //ck.显示已经打开的窗口
    function ShowActiveOldButton( obj )    //ck.change
    {
        if( obj.offsetLeft < obj.parentNode.parentNode.scrollLeft )
        {
            obj.parentNode.parentNode.scrollLeft=obj.offsetLeft-10;        
        }
        
        if(obj.offsetLeft > obj.parentNode.parentNode.offsetWidth + obj.parentNode.parentNode.scrollLeft )
        {
            obj.parentNode.parentNode.scrollLeft = obj.offsetLeft + 125 -obj.parentNode.parentNode.offsetWidth ;
        }
    }
//-->

