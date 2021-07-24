<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" %>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>欢迎使用泛普工程项目管理系统</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link href="images2/login/css/login.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="Script/jw.js"></script>
	<script type="text/javascript" src="StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="Script/PM2/Default.js"></script>
</head>
<body>
	<form runat="server">
	<div class="login-box">
		<div class="login-left">
			<span>
				<img src="images2/login/images/logo.jpg" /></span> <span class="login-pic">
					<img src="images2/login/images/pic.jpg" /></span></div>
		<div class="login-right">
			<span>
				<img src="images2/login/images/login.jpg" width="85" height="22" /></span>
			<dl>
				<dd>
					用户名：</dd>
				<dt>
					<input id="txtName" class="input_out" name="" type="text" onfocus="this.className='input_on';this.onmouseout=''" onblur="this.className='input_off';this.onmouseout=function(){this.className='input_out'};" onmousemove="this.className='input_move'" onmouseout="this.className='input_out'" runat="server" />
</dt>
				<dd>
					密 码：</dd>
				<dt>
					<input id="txtPwd" class="input_out" name="" type="password" onfocus="this.className='input_on';this.onmouseout=''" onblur="this.className='input_off';this.onmouseout=function(){this.className='input_out'};" onmousemove="this.className='input_move'" onmouseout="this.className='input_out'" runat="server" />
</dt>
				<dd class="checkbox">
					<input type="checkbox" id="chkPwd" class="check" runat="server" />
</dd>
				<dd>
					记住密码</dd>
			</dl>
			<div class="btn-box">
				<asp:Button ID="btnOk" class="login-btn" Text="登录" onmouseover="this.className='login-btn_down'" onmousedown="this.className='login-btn_down'" onmouseout="this.className='login-btn'" OnClick="btnOk_Click" runat="server" />
				<input id="imgReset" type="button" class="login-btn" value="重置" onmouseover="this.className='login-btn_down'"
					onmousedown="this.className='login-btn_down'" onmouseout="this.className='login-btn'" />
			</div>
			<ul>
				<li>不要在公共场合保存登录信息;</li>
				<li>为保证您的账号安全，退出系统时请注销登录;</li>
				<li>系统建议使用IE8.0以上版本浏览器，计算机屏幕分辨</li>
				<li class="xx">率1024*768或以上。</li>
			</ul>
		</div>
		<div class="login-bottom">
			版权所有&copy;2003-2015 | 泛普软件有限公司</div>
	</div>
	</form>
</body>
</html>
