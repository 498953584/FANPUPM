<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HDdefault.aspx.cs" Inherits="_default" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>欢迎使用建文工程项目管理系统</title>

    <script src="/Script/jquery.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
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

            //if( document.all.tb_Validate.value == "" )
            //{
            //alert('验证码不能为空！');
            //document.all.tb_Validate.focus();
            //return false;
            //}
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
        window.onload = function() {} //setHeight(); }
        //window.onresize = function(){setHeight();}
        //window.onscroll = function(){setHeight();}
        
        //设置记录用户名
        function setCb() {
            if ($("#cbPass").attr("checked")) {
                $("#cbName").attr("checked", "checked");
            }
            else {
                $("#tb_pwd").attr("value", "");
            }

        }
        function setUnChecked() {
            if (!$("#cbName").attr("checked")) {
                if ($("#tb_pwd").attr("value") != "" && $("#cbPass").attr("checked")) {
                    $("#tb_pwd").attr("value", "");
                }
                $("#cbPass").attr("checked", "");
                $("#tb_yhdm").attr("value", "");
            }

        }
    </script>

    <link href="/images/login/css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form runat="server">
<div id="container">
   <div id="header"><img src="/images/login/imgs/huadong01.jpg" /></div>
   <div id="pagebody-1">
      <div id="leftbody"><img src="/images/login/imgs/huadong02.jpg" /></div>
      <div id="rightbody">
         <div id="username">
             <ul>
	          <li><span class="yhm">用户名 ：</span>
	           <input type="text" name="tb_yhdm" id="tb_yhdm" class="name" runat="server" />
</li>
		       <li><span class="yhm">密&nbsp;码 ：</span>
		       <input type="password" id="tb_pwd" name="password" class="name" runat="server" />
</li>
	         </ul>
           </div>
           <div id="button-1">
              <ul>
                 <li>
                    <input name="rempwd" type="checkbox" id="cbPass" onclick="setCb()" value="记住密码" runat="server" />

                        <span>记住密码</span>
                 </li>
                 <li>
                    <input name="remname" type="checkbox" id="cbName" onclick="setUnChecked()" value="记住用户名" runat="server" />

                        <span style="white-space:nowrap;">记住用户名</span>
                 </li>
              </ul>
           </div>
          <div id="button">
             <ul>
	            <li>
	                 <img src="/images/login/imgs/reset.jpg" style="cursor: pointer;" onclick="login_reg()"
                                            alt="建文软件" />
	            </li>
		        <li>
		            <asp:ImageButton ImageUrl="/images/login/imgs/login.jpg" ID="ImageButton1" OnClick="ImageButton1_Click2" runat="server" /> 
		        </li>
	         </ul>
          </div>        
      </div>
       <div id="footer-1"><p>版权所有<span style="font-family:Arial, Helvetica, sans-serif; font-size:12px;">&copy;2003&ndash;2011</span>&nbsp;|&nbsp;建文软件科技有限公司</p></div>
   </div>
</form>
</body>
</html>
