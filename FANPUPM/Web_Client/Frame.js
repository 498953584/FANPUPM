//显示隐藏菜单
var showIden = true; //是否显示菜单标识
var showLink = true; //是否显示相关链接标识
var tempMenuFrame = "";
var tempLinkMenu = "";

function ShowMenu()
{
	if( showIden == true )
	{
		tempMenuFrame = top.menu.cols;
		top.menu.cols="0,*,0";
		document.all.handler.src = "images/handlerback.gif";
		showIden = false;
	}	
	else
	{
		top.menu.cols=tempMenuFrame;
		document.all.handler.src = "images/middle_middle.gif";
		showIden = true;
	}
	var obj = top.middle_left_top.shjw_mainWindow;
	if( obj != null )
	{
		obj.setBounds(0,0,top.document.all.main.width,"100%");
	}	
}

function ShowLinkMenu()
{
	if( showLink == true )
	{
		top.middle_link.rows="535,20";
		
		showLink = false;
	}	
	else
	{
		top.middle_link.rows="445,115";
		showLink = true;
	}
}


function MessagePrompt()
{
	alert('您有新的消息，请注意查收');
}

function OnMouseOverChangeImage()
{
	document.all.image.src = 'images/top_left_OnMouseOver.gif';
}

function OnMouseOutResumeImage()
{
	document.all.image.src = 'images/top_left.gif';
}
