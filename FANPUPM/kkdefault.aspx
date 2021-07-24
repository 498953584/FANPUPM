<%@ Page Language="C#" AutoEventWireup="true" CodeFile="kkdefault.aspx.cs" Inherits="_default" %>


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
        window.onload = function() { setHeight(); }
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

    <link href="/Images/Login/css/kcss.css" rel="stylesheet" type="text/css" />
</head>
<body scroll="no">
   <div class="container">
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
                    <h3 class="key"></h3>
                    <div>
                    <span class="yhm">用户名 ：</span>
                    <input type="text" name="tb_yhdm" class="name" id="tb_yhdm" runat="server" />

                    </div>
                    <div>
                    <span class="yhm">密&nbsp;&nbsp;码 ：</span>
                    <input name="password" type="password" class="name" id="tb_pwd" runat="server" />

                    </div>
                    <div class="jz">
                        <input name="checkbox" type="checkbox" id="cbName" onclick="setUnChecked()" value="记住用户名" runat="server" />

                        <span>记住用户名</span>
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
                                            alt="建文软件" /></div>
                                </td>
                            </tr>
                        </table>
                    </div>
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
        <span class="copyright"><a href="http://www.justwin.cn/" target="_blank">
            <img src="/Images/Login/imgs/slogo.jpg" alt="建文软件" /></a>&nbsp;&nbsp;<span style="font-family:Arial">版权所有&copy;2003-2011</span>
            | <a href="http://www.justwin.cn/" target="_blank">建文软件科技有限公司</a></span>
    </div>
   </div>
      <div class="footer">
          <img src="images/login/imgs/footerbg.gif" />
    </div>
</body>
</html>
