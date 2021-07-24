<%@ Page Language="C#" AutoEventWireup="true" CodeFile="middle_left.aspx.cs" Inherits="middle_left" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server"><title></title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Expires" content="0" />

	<style>
		td
		{
			cursor: hand;
		}
		.body_frame /*从左侧菜单树上点出来的业务窗口样式*/
		{
			margin-left: 0px;
			margin-top: 0px;
			margin-right: 0px;
			margin-bottom: 4px;
		}
	</style>
	<script type='text/javascript' src='jscript/JsControlMenuTool.js'></script>
	
</head>
<body class="menuBgColor" style="margin-left: 0px; margin-top: 0px; margin-right: 0px;
	margin-bottom: 4px; overflow-x: hidden; overflow-y: auto;">
	<form id="Form1" method="post" runat="server">
	<font face="宋体">
		<table border="0" cellpadding="0" cellspacing="0" width="190">
			<tr>
				<td align="center">
					<div>
						<table id="ModuleList" cellspacing="0" cellpadding="0" width="190" border="0" class="menuFont" cols="190" runat="server"></table>
					</div>
				</td>
			</tr>
		</table>
	</font>
	<asp:HiddenField ID="hdnPath" runat="server" />
	</form>
</body>
<script language="javascript">

	//bincat,2008-3-7,获取skin文件夹
	var skinPath = document.getElementById("hdnPath").value;

	var OldSizeObj;  //用来获取菜单文字所在单元格
	var NewSizeObj;

	var OneTdID = "image1";
	var TwoTdID = "image2";
	//一级菜单的图片路径
	var NormalOneOver = "url(" + skinPath + "/NormalOneOver.gif) no-repeat";
	var NormalOneClicked = "url(" + skinPath + "/NormalOneClicked.gif) no-repeat";
	var NormalOneClickedOver = "url(" + skinPath + "/NormalOneClickedOver.gif) no-repeat";
	var NormalOne = "url(" + skinPath + "/NormalOne.gif) no-repeat";
	//节点菜单的图片路径
	var EndClose = "url(" + skinPath + "/EndClose.gif) no-repeat";
	var EndCloseOver = "url(" + skinPath + "/EndCloseOver.gif) no-repeat";
	var EndLeaf = "url(" + skinPath + "/EndLeaf.gif) no-repeat";
	var EndLeafOver = "url(" + skinPath + "/EndLeafOver.gif) no-repeat";
	var EndOpen = "url(" + skinPath + "/EndOpen.gif) no-repeat";
	var EndOpenOver = "url(" + skinPath + "/EndOpenOver.gif) no-repeat";
	var Leaf = "url(" + skinPath + "/Leaf.gif) no-repeat";
	var LeafOver = "url(" + skinPath + "/LeafOver.gif) no-repeat";
	var NormalClose = "url(" + skinPath + "/NormalClose.gif) no-repeat";
	var NormalCloseOver = "url(" + skinPath + "/NormalCloseOver.gif) no-repeat";
	var NormalOpen = "url(" + skinPath + "/NormalOpen.gif) no-repeat";
	var OpenOver = "url(" + skinPath + "/OpenOver.gif) no-repeat";
	var FontBG = "url(images/FontBG.gif) no-repeat";
	var FontOverBG = "url(images/FontOverBG.gif) no-repeat";
	//火狐浏览器
	if (navigator.userAgent.indexOf('Firefox') >= 0) {
		var EndClose = 'url("skin4/EndClose.gif") no-repeat scroll 0% 0% transparent';
		var EndCloseOver = 'url("skin4/EndCloseOver.gif") no-repeat scroll 0% 0% transparent';
		var EndLeaf = 'url("skin4/EndLeaf.gif") no-repeat scroll 0% 0% transparent';
		var EndLeafOver = 'url("skin4/EndLeafOver.gif") no-repeat scroll 0% 0% transparent';
		var EndOpen = 'url("skin4/EndOpen.gif") no-repeat scroll 0% 0% transparent';
		var EndOpenOver = 'url("skin4/EndOpenOver.gif") no-repeat scroll 0% 0% transparent';
		var Leaf = 'url("skin4/Leaf.gif") no-repeat scroll 0% 0% transparent';
		var LeafOver = 'url("skin4/LeafOver.gif") no-repeat scroll 0% 0% transparent';
		var NormalClose = 'url("skin4/NormalClose.gif") no-repeat scroll 0% 0% transparent';
		var NormalCloseOver = 'url("skin4/NormalCloseOver.gif") no-repeat scroll 0% 0% transparent';
		var NormalOpen = 'url("skin4/NormalOpen.gif") no-repeat scroll 0% 0% transparent';
		var OpenOver = 'url("skin4/OpenOver.gif") no-repeat scroll 0% 0% transparent';
		var FontBG = 'url("skin4/FontBG.gif") no-repeat scroll 0% 0% transparent';
		var FontOverBG = 'url("skin4/FontOverBG.gif") no-repeat scroll 0% 0% transparent';
	}
	//	alert(NormalOne+imageObj.style.background);


	function ClickOneLevelNode(obj, ImageName) {
		var imageObj = document.getElementById(obj.id + OneTdID);
		var objCollection = document.getElementsByTagName("tr");

		NormalOne = "url(" + ImageName + "_1.gif) no-repeat";
		NormalOneOver = "url(" + ImageName + "_3.gif) no-repeat";
		NormalOneClicked = "url(" + ImageName + "_2.gif) no-repeat";
		NormalOneClickedOver = "url(" + ImageName + "_4.gif) no-repeat";
		//火狐浏览器
		if (navigator.userAgent.indexOf('Firefox') >= 0) {
			NormalOne = 'url("skin4/MenuIco/13_1.gif") no-repeat scroll 0% 0% transparent';
			NormalOneOver = 'url("skin4/MenuIco/13_1.gif") no-repeat scroll 0% 0% transparent';
			NormalOneClicked = 'url("skin4/MenuIco/13_2.gif") no-repeat scroll 0% 0% transparent';
			NormalOneClickedOver = 'url("skin4/MenuIco/13_2.gif") no-repeat scroll 0% 0% transparent';
			//url("skin4/MenuIco/13_1.gif) no-repeat") repeat scroll 0% 0% transparent
		}
		//	alert(NormalOne+imageObj.style.background);		
		//当收缩节点时，只处理当前节点下的节点，而展开节点时
		//alert(imageObj.style.background);

		if (imageObj.style.background == NormalOneOver || imageObj.style.background == NormalOne) {
			//切换图片的状态为展开状态
			imageObj.style.background = NormalOneClicked;
			//var HaveChildCount = 0;
			//将当前节点下的一级节点展开

			for (var i = 0; i < objCollection.length; i++) {
				var strId = objCollection[i].id;
				//alert(strId);
				if ((strId.length == (obj.id.length + 2) && strId.substring(0, obj.id.length) == obj.id) || (strId.length == (obj.id.length + 5) && strId.substring(0, obj.id.length) == obj.id)) {
					objCollection[i].style.display = '';
				}
				else if (strId.length >= 2 && strId.substring(0, obj.id.length) != obj.id) {
					if (strId.length == 2) {
						//切换其它一级节点的图片为收缩状态
						var tempObj = document.getElementById(strId + OneTdID);
						var CurrImg = tempObj.style.background;
						CurrImg = CurrImg.substr(0, CurrImg.lastIndexOf("_"));
						//火狐
						//火狐浏览器
						if (navigator.userAgent.indexOf('Firefox') >= 0) {
							CurrBg = NormalOne
						} else {
							CurrBg = CurrImg + "_1.gif) no-repeat";
						}

						//if(i<10)
						//	alert(CurrBg);
						tempObj.style.background = CurrBg;
					}
					else if (strId.length > 2) {
						//隐藏其它的所有的二级以下的节点
						objCollection[i].style.display = 'none';
						var imaObj = document.getElementById(strId + OneTdID);
						if (imaObj != null) {
							//将图片切换为收缩图片
							if (imaObj != null) {
								if (imaObj.style.background == NormalOpen)
									imaObj.style.background = NormalClose;
								else if (imaObj.style.background == EndOpen)
									imaObj.style.background = EndClose;
							}
						}
					}
				}
			}
			//alert(HaveChildCount);
		}
		else if (imageObj.style.background == NormalOneClickedOver || imageObj.style.background == NormalOneClicked) {

			//切换图片的状态
			imageObj.style.background = NormalOne;

			//将所有的节点都隐藏，并恢复子节点的图片状态为收缩
			for (var i = 0; i < objCollection.length; i++) {
				var strId = objCollection[i].id;

				//收缩当前节点以下的所有节点
				if (strId.length > obj.id.length && strId.substring(0, obj.id.length) == obj.id) {
					objCollection[i].style.display = 'none';
					var imaObj = document.getElementById(strId + OneTdID);
					if (imaObj != null) {
						//将图片切换为收缩图片
						if (imaObj != null) {
							if (imaObj.style.background == NormalOpen)
								imaObj.style.background = NormalClose;
							else if (imaObj.style.background == EndOpen)
								imaObj.style.background = EndClose;
						}
					}
				}
			}
		}

		goWhere(obj.id, "", "");

	}

	//点击子节点，根据所点击节点的图片的状态来判断当前是展开还是收缩状态
	function ClickChildNode(obj) {
		var imageObj = document.getElementById(obj.id + OneTdID);
		var objCollection = document.getElementsByTagName("tr");

		if (imageObj != null) {
			//			alert('3'+imageObj.style.background);
			//当前为收缩状态的处理（要进行展开）
			if (imageObj.style.background == NormalClose || imageObj.style.background == EndClose || imageObj.style.background == NormalCloseOver || imageObj.style.background == EndCloseOver) {
				//切换收缩图片为展开图片
				if (imageObj.style.background == NormalClose || imageObj.style.background == NormalCloseOver)
					imageObj.style.background = NormalOpen;
				else
					imageObj.style.background = EndOpen;
				//显示当前节点以下的一级节点
				for (var i = 0; i < objCollection.length; i++) {
					var strId = objCollection[i].id;
					if (strId.length == (obj.id.length + 2) && strId.substring(0, obj.id.length) == obj.id)
						objCollection[i].style.display = '';
				}
			} //当前为展开状态的处理（要进行收缩）
			else if (imageObj.style.background == NormalOpen || imageObj.style.background == EndOpen || imageObj.style.background == OpenOver || imageObj.style.background == EndOpenOver) {
				//					alert('4'+imageObj.style.background);
				//切换图片为收缩图片
				if (imageObj.style.background == NormalOpen || imageObj.style.background == OpenOver)
					imageObj.style.background = NormalClose;
				else
					imageObj.style.background = EndClose;
				//将当前节点以下的所有节点隐藏，并将子节点展开图片切换为收缩图片
				for (var j = 0; j < objCollection.length; j++) {
					var strId = objCollection[j].id;
					if (strId.length >= (obj.id.length + 2) && strId.substring(0, obj.id.length) == obj.id) {
						var modNum = strId.length % 2;
						if (modNum == 0) {
							objCollection[j].style.display = 'none';
						}
						var imaObj = document.getElementById(strId + OneTdID);
						//将图片切换为收缩图片
						if (imaObj != null) {
							if (imaObj.style.background == NormalOpen)
								imaObj.style.background = NormalClose;
							else if (imaObj.style.background == EndOpen)
								imaObj.style.background = EndClose;
						}
					}
				}
			}
		}
		goWhere(obj.id, "", "");
	}

	function OnMouseOverOneLevel(tempObj, BgImage) {
	}

	function OnMouseOutOneLevel(tempObj) {
	}

	function OnMouseOverChild(obj) {
		var obj1 = document.getElementById(obj.id + OneTdID);
		var obj2 = document.getElementById(obj.id + TwoTdID);

		if (obj1.style.background == EndLeaf) {
			obj1.style.background = EndLeafOver;
		}
		if (obj1.style.background == EndClose) {
			obj1.style.background = EndCloseOver;
		}
		if (obj1.style.background == NormalOpen) {
			obj1.style.background = OpenOver;
		}
		if (obj1.style.background == EndOpen) {
			obj1.style.background = EndOpenOver;
		}
		if (obj1.style.background == Leaf) {
			obj1.style.background = LeafOver;
		}
		if (obj1.style.background == NormalClose) {
			obj1.style.background = NormalCloseOver;
		}

		obj2.style.background = FontOverBG;
	}

	function OnMouseOutChild(obj) {
		var obj1 = document.getElementById(obj.id + OneTdID);
		var obj2 = document.getElementById(obj.id + TwoTdID);

		if (obj1.style.background == EndLeafOver)
			obj1.style.background = EndLeaf;
		if (obj1.style.background == EndCloseOver)
			obj1.style.background = EndClose;
		if (obj1.style.background == OpenOver)
			obj1.style.background = NormalOpen;
		if (obj1.style.background == EndOpenOver)
			obj1.style.background = EndOpen;
		if (obj1.style.background == LeafOver)
			obj1.style.background = Leaf;
		if (obj1.style.background == NormalCloseOver)
			obj1.style.background = NormalClose;

		obj2.style.background = FontBG;
	}

	function goWhere(id, url, title) {
		//bincat,2008-3-6

		NewSizeObj = document.getElementById(id + TwoTdID);
		try {
			if (NewSizeObj != OldSizeObj) {
				NewSizeObj.style.color = "#ff0000";
				OldSizeObj.style.color = "#000000";
			}
		}
		catch (e) { }
		OldSizeObj = NewSizeObj;

		if (url.length == 0) {
		}
		else {
			if (navigator.userAgent.indexOf('Firefox') >= 0) {
				top.frameWorkArea.location.href = url;
			} else {
				toolbox_oncommand(url, title);
			}
		}
	}
	//bincat,2008-3-20
	try {
		document.getElementById('28image1').click();
	}
	catch (e) { }
		
</script>
<script language="javascript">	document.body.oncontextmenu = new Function("return false;");</script>
</html>
