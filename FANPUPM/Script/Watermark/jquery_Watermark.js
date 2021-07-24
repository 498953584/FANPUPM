//引用方法 参数说明  第二个为水印图片地址
//<body onload="GetWaterMarked(window,'watermark.jpg','this')">
//页面加水印js
function   GetWaterMarked(targetObj,jpgUrl,targetStr ) {
		var windowobj=targetObj;
		var waterMarkPicUrl=jpgUrl;
		var controlWindowStr=targetStr;
		if(windowobj.document.getElementById("waterMark") != null)
			return;
		var m = "waterMark";
		var newMark = windowobj.document.createElement("div");
		//设置水印图片的位置
		newMark.id = m;
		newMark.style.position = "absolute";
		newMark.style.zIndex = "9627";
		newMark.style.top = "25px";
		newMark.style.left = "90%";		
		//newMark.style.width = windowobj.document.body.clientWidth;
		newMark.style.width=100;
        
		if( parseInt(windowobj.document.body.scrollHeight) > parseInt(windowobj.document.body.clientHeight) )
		{
			newMark.style.height = windowobj.document.body.scrollHeight;
		}else
		{
			newMark.style.height = windowobj.document.body.clientHeight;
		}
		newMark.style.backgroundImage = "url("+ waterMarkPicUrl +")";
		//document.getElementById('newMark').style.backgroundRepeat='no-repeat';//='norepeat';

		newMark.style.backgroundRepeat="no-repeat";
		//页面水印图片不重复
		newMark.style.filter = "alpha(opacity=50)";
		windowobj.document.body.appendChild(newMark);

			var markStr = "var sobj ="+controlWindowStr+".document.getElementById('waterMark');sobj.style.width ="+controlWindowStr+".document.body.clientWidth;sobj.style.height ="+controlWindowStr+".document.body.clientHeight;";
		if(windowobj.document.body.onresize != null)
		{
			var oldResiae = windowobj.document.body.onresize.toString();
		 	var oldResiaeStr = oldResiae.substr(oldResiae.indexOf("{")+1);
		 	var oldResiaeStr= oldResiaeStr.substr(0,oldResiaeStr.lastIndexOf("}"));
		 	oldResiaeStr+=";"+markStr;
		 	windowobj.document.body.onresize = new Function(oldResiaeStr);
		}
		else
		{
			windowobj.document.body.onresize = new Function(markStr);
		}
 }