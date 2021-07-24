<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default4.aspx.cs" Inherits="_default" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title>欢迎使用泛普工程项目管理系统</title>
	<link rel="Shortcut Icon" href="favicon.ico">
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" language="javascript">
		$(document).ready(function () {
			// 自动登录
			var id = getRequestParam('id');
			var pwd = getRequestParam('pwd');
			if (!!id && !!pwd && $('#hfldIsAutoLogin').val() == '0') {
				// 如果URL中包含uid和pwd, 并且没有进行自动登录过，则自动进行登录
				$('#tb_yhdm').val(id);
				$('#tb_pwd').val(pwd);
				$('#hfldIsAutoLogin').val('1');
				$('#ImageButton1').click();
			}
		});

		function validatorSpace() {
			if (document.all.tb_yhdm.value == "") {
				alert('用户名不能为空！');
				document.all.tb_yhdm.focus();
				return false;
			}

			if (document.all.tb_pwd.value == "") {
				alert('密码不能为空！');
				document.all.tb_pwd.focus();
				return false;
			}
			return true;
		}

		function IsRuleByText() {
			if (event.keyCode == 45 || event.keyCode == 47 || event.keyCode == 39)
				event.keyCode = null;
		}

		function CovertLarge(strObj) {
			strObj.value = strObj.value.toUpperCase();
		}
		function openForm() {
			var w = screen.availWidth - 10;
			var h = screen.availHeight;

			//window.open("FrameSet.aspx,_blank,left=0,top=0,width=" + w + ",height=" + (h - 29) + ",status=no,toolbar=no,menubar=no,location=no");
			window.open("FrameSet.aspx", "_blank", "left=0,top=0,width=" + w + ",height=" + (h - 29) + ",toolbar=no,location=yes,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,copyhistory=yes");
			//window.location.href = "../default.aspx";
			//window.close();
		}
		function login_reg() {
			//重置
			if (document.getElementById('tb_pwd') != null)
				document.getElementById('tb_pwd').value = "";
			if (document.getElementById('tb_yhdm') != null)
				document.getElementById('tb_yhdm').value = "";
			//注册
			//	        var w = screen.availWidth-10;
			//		    var h = screen.availHeight;	
			//	        window.open("http://www.justwin.cn/service/service_1_1.asp,_blank,left=0,top=0,width=" + w + ",height=" + (h - 29) + ",toolbar=yes,location=yes,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes,copyhistory=yes");


		}
		function getScren() {//zyg 08-09
			if ((screen.width >= 1024) && (screen.height >= 768))
			{ }
			else {
				alert('为了显示最佳效果,建议您采用 1024 * 768 以上的屏幕分辨率！');
			}
		}

		function setHeight() {
			var oHeight, cHeight, bottomMain;
			bottomMain = document.getElementById("divcopy");
			var topheight = document.getElementById("divtop").clientHeight;
			oHeight = document.body.offsetHeight;
			cHeight = document.documentElement.clientHeight;
			if (oHeight > cHeight) {
				bottomMain.style.height = oHeight - topheight;
			}
			else {
				bottomMain.style.height = cHeight - topheight;
			}
		}
		window.onload = function () { setHeight(); }
		//window.onresize = function(){setHeight();}
		//window.onscroll = function(){setHeight();}

		//设置记录密码
		function setCb() {
			//设置是否记住密码
			//取消事件，zfj  2013-2-1  
			//原因：取消记住密码不应该删除已输入的密码
			//            if ($("#cbPass").attr("checked")) {
			//                $("#cbName").attr("checked", "checked");
			//            }
			//            else {
			//                $("#tb_pwd").attr("value", "");
			//            }
		}
		function setUnChecked() {
			//设置是否记住用户名
			//取消事件，zfj  2013-2-1  
			//原因：取消记住用户名不应该删除原用户名
			//            if (!$("#cbName").attr("checked")) {
			//                if ($("#tb_pwd").attr("value") != "" && $("#cbPass").attr("checked")) {
			//                    $("#tb_pwd").attr("value", "");
			//                }
			//                $("#cbPass").attr("checked", "");
			//                $("#tb_yhdm").attr("value", "");
			//            }

		}

		//用户名改变，如果当前是记住密码的，清楚密码
		function checkChange() {
			if ($('#cbPass').attr('checked') != undefined) {
				var name = $('#tb_yhdm').val();
				if (getCookie('cbName') != name) {
					$('#tb_pwd').val('');
				}
			}
		}
		//获取GET请求参数
		function getRequestParam(argname, url) {
			url = url || document.location.href;
			var arrStr = url.substring(url.indexOf('?') + 1).split('&');
			for (var i = 0; i < arrStr.length; i++) {
				var loc = arrStr[i].indexOf(argname + '=');
				if (loc != -1) {
					return arrStr[i].replace(argname + '=', '').replace('?', '');
					break;
				}
			}
			return '';
		}

	</script>
	<link href="/Images/Login/css/css.css" rel="stylesheet" type="text/css" />
</head>
<body style="overflow: hidden;">
	<div id="divtop">
		<div class="cp_1">
			<h1 id="logo">
			</h1>
		</div>
		<div class="cp_2">
			<div class="box">
				<div class="left">
					<h2 class="logo3 bottom">
					</h2>
					
				</div>
				<div class="right">
					<form id="Form1" runat="server">
					<h3 class="key">
					</h3>
					<div>
						<span class="yhm">用户名 ：</span>
						<input type="text" name="tb_yhdm" class="name" id="tb_yhdm" onkeyup="checkChange()" runat="server" />

					</div>
					<div>
						<span class="yhm">密&nbsp;&nbsp;码 ：</span>
						<input name="password" type="password" class="name" id="tb_pwd" runat="server" />

					</div>
					<div class="jz" style="padding-top: 0px;">
						<input name="checkbox" type="checkbox" id="cbName" onclick="setUnChecked()" value="记住用户名" visible="false" checked="true" runat="server" />

						
						<input name="" type="checkbox" id="cbPass" onclick="setCb()" value="记住密码" runat="server" />

						<span>记住密码</span></div>
					<div class="dl">
						<table>
							<tr>
								<td>
									<asp:ImageButton ImageUrl="/images/login/imgs/login.jpg" ID="ImageButton1" OnClick="ImageButton1_Click2" runat="server" />
								</td>
								<td>
									<div>
										<img src="/images/login/imgs/reset.jpg" style="cursor: pointer;" onclick="login_reg()"
											alt="泛普软件" /></div>
								</td>
							</tr>
						</table>
					</div>
					<asp:HiddenField ID="hfldIsAutoLogin" Value="0" runat="server" />
					</form>
				</div>
			</div>
		</div>
		<div class="cp_3">
			<div class="box2">
				<ul class="ulright">
					<li>不要在公共场合保存登录信息。</li>
					<li>为保证您的账号安全，退出系统时请注销登录。</li>
					<li>本系统建议使用IE8.0以上版本浏览器，计算机屏幕分辨率1024*768或以上。</li>
				</ul>
			</div>
		</div>
	</div>
	<div class="copy" id="divcopy">
		<span class="copyright"><a href="http://www.fanpusoft.com/" target="_blank">
			<img src="/Images/Login/imgs/slogo.jpg" alt="泛普软件" /></a>&nbsp;&nbsp;<span style="font-family: Arial">版权所有&copy;<asp:Label ID="labYear" Text="" runat="server"></asp:Label></span> | <a href="http://www.fanpusoft.com/"
					target="_blank">泛普软件有限公司</a></span>
	</div>
</body>
</html>
