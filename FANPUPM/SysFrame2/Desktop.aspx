<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Desktop.aspx.cs" Inherits="SysFrame2_Desktop" %>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>塔站共享项目管理系统</title><link rel="shortcut icon" href="" type="image/x-icon" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/jquery.easyui.extension.css" />
<link rel="Stylesheet" type="text/css" href="../Script/jquery.tooltip/jquery.tooltip.css" />
<link rel="Stylesheet" href="/Script/jquery.jgrowl/jquery.jgrowl.css" />
<link rel="Stylesheet" type="text/css" href="Style/Desktop.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/json2.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="Script/top.ui.js"></script>
	<script type="text/javascript" src="Script/top.ui.impl.js"></script>
	<script type="text/javascript" src="Script/Desktop.js"></script>
	<script type="text/javascript" src="Script/MyPanel.js"></script>
	<script type="text/javascript" src="/Script/jquery.jgrowl/jquery.jgrowl.js"></script>
	<script type="text/javascript" src="/Script/Popup.js"></script>
	<script type="text/javascript" src="/Script/jquery.blockUI.js"></script>
</head>
<body class="easyui-layout" style="text-align: center;" onselectstart="return false;">
	<script type="text/javascript">
		initialize();
		function initialize() {
			addcloud(); //为页面添加遮罩
			document.onreadystatechange = subSomething; //监听加载状态改变
		}

		function addcloud() {
			var $div = $('<div id="__div" style="position:absolute; top: 0; left: 0; background: #d3d3d3; z-index:1000; padding-top: 200px;"></div>');
			$div.width($(document).width());
			$div.height($(document).height());
			var $img = $('<img id="__img" style=""></img>')
			$img.attr('src', '/images/waiting.gif');
			$div.append($img);
			$('body').append($div)
		}

		function removecloud() {
			$("#__div").remove();
		}

		function subSomething() {
			if (document.readyState == "complete") //当页面加载完毕移除页面遮罩，移除loading动画-
				removecloud();
		}
	</script>
	<form id="form1" runat="server">
	<!-- 头部-->
	<div id="top" data-options="region:'north',title:'',split:true">
		<div id="logo" style="border: 0px solid Red;">
			<img src="../UploadFiles\UserLogo\logo.jpg?time=<%=this.timeStr %>>" alt="泛普工程项目管理软件" />
		</div>
		<div id="toolbar" style="border: 0px solid Blue;">
			<div>
				<span>
					<asp:Label ID="lblPhoneNumber" runat="server"></asp:Label>
				</span>&nbsp;
				<img src="../images2/desktop/admin.jpg" />
				<span>当前用户：<asp:Label ID="lblCurrentUser" runat="server"></asp:Label></span>&nbsp;
				<img src="../images2/desktop/on-line.jpg" />
				<span id="sp_onlineNum">在线人数：</span>
				<asp:Label ID="lblOnlineNum" runat="server">0 </asp:Label>&nbsp;
				<img src="../images2/desktop/del.jpg" />
				<span id="sp_exit" runat="server">退出</span>
				<asp:Button ID="btn_exit" Text="" Style="display: none;" OnClick="btn_exit_Click" runat="server" />
			</div>
			<div>
				<img src="../images2/desktop/home.jpg" />
				<span id="sp_mydesktop">我的桌面</span>&nbsp;
				<img src="../images2/desktop/e-mail.jpg" />
				<span id="sp_myemail">我的邮箱</span>&nbsp;
				<img src="../images2/desktop/Process.jpg" />
				<span id="sp_wfmonitor">流程监控</span>&nbsp;
				<img src="../images2/desktop/people.jpg" />
				<span id="sp_myhelp">员工自助</span>&nbsp;
				<img src="../images2/desktop/about-justwin.jpg" />
				<span id="sp_about">关于软件</span>&nbsp;
				<img src="../images2/desktop/help.jpg" />
				<span id="sp_help">帮助</span>
			</div>
		</div>
	</div>
	<!-- 菜单-->
	<div data-options="region:'west',split:true,title:' '" style="width: 200px; padding: 0px;">
		<div id='nav' class="easyui-accordion" border="false" style="overflow: auto">
		</div>
	</div>
	<!-- 内容部分-->
	<div data-options="region:'center',title:''">
		<div id="tabs" class="easyui-tabs" fit="true" border="false">
			<div class='my-desk' title="我的桌面" data-options="iconCls:'icon-reload'" style="overflow: auto;" id="/SysFrame2/Desktop.aspx">
				<div id='div_setting' style="height: 20px; text-align: right; padding-right: 6px;">
					<img id="Img_set" alt="设置桌面布局" title="设置桌面布局" src="/TableTop/image/bg_set.png" style="width: 20px;
						height: 20px; cursor: pointer" onclick="settingPanel();" />
					<img id="Img_add" alt="桌面栏目管理" title="桌面栏目管理" src="/TableTop/image/bg_add.png" style="width: 20px;
						height: 20px; cursor: pointer" onclick="mangerPanel();" class="header-cursor" /></div>
				<table id="tabPanels">
				</table>
			</div>
		</div>
	</div>
	<!-- 关于软件-->
	<div id="div_about" title="关于工程项目管理软件" style="display:none;font-size: 12px; color: #212121;" data-options="modal:true, collapsible:false, maximizable:false, minimizable:false">
		<div style="padding-left: 30px; padding-top: 20px;">
			<img alt="泛普软件有限公司" src="/images/justwin.jpg" />
		</div>
		<div style="padding-left: 30px; padding-top: 20px;">
			版本信息：<asp:Label ID="lblVersion" Style="font-weight: bold; font-family: Arial;" runat="server"></asp:Label>
			
		</div>
		<div style="padding-left: 30px; padding-top: 25px;">
			警告：本软件受版权法保护，任何单位或者个人未经合法授权，<br />
			擅自复制或者散播本程序的部分或者全部，将承受严厉的民事<br />
			刑事处罚，对已知的违反者将给法律范围内的全面制裁。
		</div>
		<%--<div style="padding-left: 30px; padding-top: 50px; color: #666; width: 250px; float: left;">
			版权所有 <span style="font-family: Arial;">&copy;2003-2015 </span>| <a id="a_about_jw"
				target="_blank" href="http://www.fanpusoft.com">泛普软件有限公司</a>
		</div>--%>
		<div id="div_btnok">
			<input id="btn_about_ok" type="button" onclick="btn_about_ok();" value="确定" />
		</div>
	</div>
	<div id="div_win" data-options="modal:true, minimizable:false">
		<iframe frameborder="0" style="width: 100%; height: 98%;"></iframe>
	</div>
	<div id="div_win2" data-options="modal:true, minimizable:false">
		<iframe frameborder="0" style="width: 100%; height: 98%;"></iframe>
	</div>
	<!-- 标签页弹出菜单-->
	<div id="cm" class="easyui-menu" style="width: 150px;">
		<div id="cm-tabupdate">
			刷新</div>
		<div class="menu-sep">
		</div>
		<div id="cm-tabclose">
			关闭</div>
		<div id="cm-tabcloseall">
			全部关闭</div>
		<div id="cm-tabcloseother">
			除此之外全部关闭</div>
	</div>
	<!-- 隐藏控件-->
	<asp:HiddenField ID="hfldDepEmployeeJson" runat="server" />
	<asp:HiddenField ID="hfldMyModel" runat="server" />
	<asp:HiddenField ID="hfldMySet" runat="server" />
	<asp:HiddenField ID="hfldUserCode" runat="server" />
	<asp:HiddenField ID="hfldChildMk" runat="server" />
	</form>
</body>
</html>
