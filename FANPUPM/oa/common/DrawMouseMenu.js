
function DrawMouseMenu(){
	DivH=40;
	oSelection = document.selection;
	//横线
	var HrStr="<tr><td align=center valign=middle height=2>";
	HrStr += "<TABLE border=0 cellpadding=0 cellspacing=0 width=110 height=2>";
	HrStr += "<tr><td height=1 bgcolor=buttonshadow><\/td><\/tr>";
	HrStr += "<tr><td height=1 bgcolor=buttonhighlight><\/td><\/tr>";
	HrStr += "<\/TABLE>";
	HrStr += "<\/td><\/tr>";
	var MenuStr1="<tr><td align=center valign=middle height=20>";
	MenuStr1 += "<TABLE border=0 cellpadding=0 cellspacing=0 width=132>";
	MenuStr1 += "<tr><td valign=middle height=16 class=Mout onMouseOver=this.className='Mover'; onMouseOut=this.className='Mout'; onclick=\"";
	var MenuStr2="<\/td><\/tr><\/TABLE><\/td><\/tr>";
	//菜单项数组
	var XiciMenu=["window.history.back()\">后退","window.history.forward()\">前进","window.location.reload()\">刷新"];
	//菜单项数组
//	var SysMenu=["document.execCommand('SelectAll')\">关于长城信息"];title
//	var SysMenu=["MouseMenu.style.visibility='hidden';top.NavigationMenu.help();\">关于上海建文"];
	var MenuStr="";
	for(i=0;i<XiciMenu.length;i++){
		MenuStr+=MenuStr1+XiciMenu[i]+MenuStr2;
		DivH+=8;
	}
	MenuStr+=HrStr;
	for(i=0;i<arguments.length;i++){
		MenuStr+=MenuStr1+arguments[i]+MenuStr2;
		DivH+=20;
	}
	/*
	if(arguments.length>0){
		MenuStr+=HrStr;
		DivH+=3;
	}
	for(i=0;i<SysMenu.length;i++){
		MenuStr+=MenuStr1+SysMenu[i]+MenuStr2;
		DivH+=20;
	}
	*/

	var DivStr1="";
	DivStr1 = "<DIV id=MouseMenu class=div1 style=\"position:absolute; left:0px; top:0px; width=150;height="+DivH+"; z-index:1; visibility:hidden;\"><TABLE border=0 cellpadding=0 cellspacing=0 class=div2><tr><td  width=\"20\" valign=bottom align=center  bgcolor=buttonface><\/td><td bgcolor=buttonface><TABLE border=0 cellpadding=0 cellspacing=0>";
	var DivStr2="<\/TABLE><\/td><\/tr><\/TABLE><\/DIV>";
	document.write(DivStr1+MenuStr+DivStr2);
	
	document.body.oncontextmenu=new Function("return ShowMouseMenu();");
	document.body.onclick=new Function("MouseMenu.style.visibility='hidden';");
	document.body.onscroll=new Function("MouseMenu.style.visibility='hidden';");
	document.body.onselectstart=new Function("MouseMenu.style.visibility='hidden';");
	window.onresizestart=new Function("MouseMenu.style.visibility='hidden';");
}

function ShowMouseMenu(){
    //2004-7-7日，bincat修改
    //功能：分帧的页面同一时刻只有一个右键菜单
	try{
		//首先将main帧里所有的右键菜单隐藏
		var e = top.main.frames;
		for(i=0;i<e.length;i++)
		{
			e[i].MouseMenu.style.visibility = 'hidden';
		}
	}
	catch(e)
	{
	}
	
	//2007-9-21,bincat
	//如果不是在框架祯里打开的，说明是弹出窗口
	try
	{
	    var obj = dialogArguments;
	}
	catch(e)
	{	    
//	    if(typeof(top.NavigationMenu) != 'undefined')
//	    {               	
	        //原有生成右键菜单功能
	        if(MouseMenu.style.visibility=='visible')MouseMenu.style.visibility='hidden';
	        if(event.srcElement.tagName=="IMG"&&event.srcElement.id!="menugif"||event.srcElement.tagName=="A"||event.srcElement.tagName=="TEXTAREA"||event.srcElement.tagName=="INPUT"||event.srcElement.tagName=="SELECT"||oSelection.type!="None")
		        return true;
	        else{
		        if(event.clientX+150 > document.body.clientWidth)MouseMenu.style.left=event.clientX+document.body.scrollLeft-150;
		        else MouseMenu.style.left=event.clientX+document.body.scrollLeft;
		        //if(DivH <= 167)DivH = 164;
		        if(event.clientY+DivH > document.body.clientHeight)MouseMenu.style.top=event.clientY+document.body.scrollTop-DivH;
		        else MouseMenu.style.top=event.clientY+document.body.scrollTop;
		        MouseMenu.style.visibility='visible';
	        }
//	    }
	}    
	    
	return false; 
}
