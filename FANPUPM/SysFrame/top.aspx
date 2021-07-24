<%@ Page Language="C#" AutoEventWireup="true" CodeFile="top.aspx.cs" Inherits="top" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>top</title>
	<script type='text/javascript' language='javascript' src='jscript/JsControlMenuTool.js'></script>
	<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
	<link href="../StyleCss/PM1.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript">

		function lock() {
			window.top.topknot.rows = "0,0,0,*";
		}

		var NewSizeObj, OldSizeObj;
		function doWithNav(str, obj, url) {
			window.top.frames('NavigationMenu').document.getElementById('l_title').innerText = str;

			window.top.frames('frameWorkArea').location = url;

			//bincat,2008-3-6
			NewSizeObj = obj;
			try {
				if (NewSizeObj != OldSizeObj) {
					NewSizeObj.style.fontWeight = "bold";
					OldSizeObj.style.fontWeight = "";
				}
			}
			catch (e) { }
			OldSizeObj = NewSizeObj;
		}
		function doWith(str, obj) {
			//bincat,2008-3-6
			NewSizeObj = obj;
			try {
				if (NewSizeObj != OldSizeObj) {
					NewSizeObj.style.fontWeight = "bold";
					OldSizeObj.style.fontWeight = "";
				}
			}
			catch (e) { }
			OldSizeObj = NewSizeObj;

			var desktop = top.frameWorkArea.window.desktop;

			if (str == "1") {
				top.frameWorkArea.history.go(-1);
			}
			else if (str == "2") {

				top.frameWorkArea.history.go(1);

			}
			else if (str == "3") {

				if (desktop.getActive() != null) {
					desktop.getActive().getWindow().getElementsByTagName("iframe")[0].src = desktop.getActive().getWindow().getElementsByTagName("iframe")[0].src;
				}
				else {
					parent.frameWorkArea.document.getElementById("deskframe").src = parent.frameWorkArea.document.getElementById("deskframe").src;
				}

			}
			else if (str == "4") {
				top.frameWorkArea.location.href = 'ShowInfomation.aspx';
				top.NavigationMenu.location.href = '/SysFrame/NavigationMenu.aspx';

			}
			else if (str == "5") {
				if (confirm('确定要退出系统吗？')) {
					var b_version = navigator.appVersion
					var version = b_version.split(";");
					var trim_Version = version[1].replace(/[ ]/g, "");
					if (trim_Version == 'MSIE9.0') {		// 判断浏览器版本
						window.open('../default.aspx');
						document.all.siframe.src = "exit.aspx";
					} else {
						document.all.siframe.src = "exit.aspx";
					}
				}
				else {
					return false;
				}
			}
			else if (str == "6") {
				var url = "";
				if (desktop.getActive() != null) {
					url = desktop.getActive().getWindow().getElementsByTagName("iframe")[0].src;
				}
				else {
					url = parent.frameWorkArea.document.getElementById("deskframe").src;
				}
				url = url.replace('&', '@');
				// alert(url);
				window.open('../Help/help.aspx?url=' + url, '', 'left=150,top=150,width=800,height=500,toolbar=no,status=yes,scrollbars=yes,resizable=yes');
			}

			else if (str == "7") {
				desktop.getTaskBar().getBtnList()[-100].getBtn().parentNode.parentNode.scrollLeft = 0;
				desktop.minimizeAll();
			}
		}
		    
		    
	</script>
</head>
<body leftmargin="0" topmargin="0" class="no">
	<form name="Form1" method="post" action="aspx" id="Form1" runat="server">
	<input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="/wEPDwUJMTA4NDMxMjYzZGQ2IihbXD+otARXvrQBdcDgsnUTTg==" />
	<script language='javascript' type='text/javascript' src='../Web_Client/Common.js'></script>
	<table width="100%" border="0" cellspacing="0" cellpadding="0" height="63">
		<tr>
			<td width="234" height="71" background="<%=this.strSkinPath %>/top_bg.gif">
				<img id="Image1" src="<%=this.strSkinPath %>/logo.gif" border="0" />
			</td>
			<td background="<%=this.strSkinPath %>/top_bg.gif" style="background-repeat: repeat-x;
				text-align: right; font-weight: bold; vertical-align: bottom; padding-bottom: 4px;">
				<span class="char"></span>
				<asp:Label ID="lblPhoneNumber" CssClass="num" runat="server"></asp:Label>
			</td>
			<td width="440" valign="bottom" background="<%=this.strSkinPath %>/top_fixation.gif">
				<table width="100%" border="0" cellspacing="0" cellpadding="0">
					<tr>
						<td height="35" align="right">
							<table border="0" cellspacing="0" cellpadding="0">
								<tr>
									<td width="64" height="24" align="center" onclick="javascript:doWith('1',this);"
										onmouseover="this.style.backgroundImage='url(<%=this.strSkinPath %>/topmenu_bg.gif)'"
										onmouseout="this.style.backgroundImage='url()'" onmousedown="this.style.backgroundImage='url(<%=this.strSkinPath %>/topmenu_bg1.gif)'"
										onmouseup="this.style.backgroundImage='url(<%=this.strSkinPath %>/topmenu_bg.gif)'">
										<img src="<%=this.strSkinPath %>/top_back.gif" width="49" height="14" border="0">
									</td>
									<td width="64" height="24" align="center" onclick="javascript:doWith('2',this);"
										onmouseover="this.style.backgroundImage='url(<%=this.strSkinPath %>/topmenu_bg.gif)'"
										onmouseout="this.style.backgroundImage='url()'" onmousedown="this.style.backgroundImage='url(<%=this.strSkinPath %>/topmenu_bg1.gif)'"
										onmouseup="this.style.backgroundImage='url(<%=this.strSkinPath %>/topmenu_bg.gif)'">
										<img src="<%=this.strSkinPath %>/top_go.gif" width="48" height="14" border="0">
									</td>
									<td width="64" height="24" align="center" onclick="javascript:doWith('3',this);"
										onmouseover="this.style.backgroundImage='url(<%=this.strSkinPath %>/topmenu_bg.gif)'"
										onmouseout="this.style.backgroundImage='url()'" onmousedown="this.style.backgroundImage='url(<%=this.strSkinPath %>/topmenu_bg1.gif)'"
										onmouseup="this.style.backgroundImage='url(<%=this.strSkinPath %>/topmenu_bg.gif)'">
										<img src="<%=this.strSkinPath %>/top_ref.gif" width="48" height="16" border="0">
									</td>
									<td width="64" height="24" align="center" onclick="javascript:doWith('6',this);"
										onmouseover="this.style.backgroundImage='url(<%=this.strSkinPath %>/topmenu_bg.gif)'"
										onmouseout="this.style.backgroundImage='url()'" onmousedown="this.style.backgroundImage='url(<%=this.strSkinPath %>/topmenu_bg1.gif)'"
										onmouseup="this.style.backgroundImage='url(<%=this.strSkinPath %>/topmenu_bg.gif)'">
										<img src="<%=this.strSkinPath %>/top_help.gif" width="48" height="16" border="0">
									</td>
									<td width="64" height="24" align="center" onclick="javascript:doWith('5',this);"
										onmouseover="this.style.backgroundImage='url(<%=this.strSkinPath %>/topmenu_bg.gif)'"
										onmouseout="this.style.backgroundImage='url()'" onmousedown="this.style.backgroundImage='url(<%=this.strSkinPath %>/topmenu_bg1.gif)'"
										onmouseup="this.style.backgroundImage='url(<%=this.strSkinPath %>/topmenu_bg.gif)'">
										<img src="<%=this.strSkinPath %>/top_exit.gif" width="48" height="16" border="0" id="IMG1"
											language="javascript" onclick="return IMG1_onclick()">
										<iframe id="siframe" name="siframe" src="" style="display: none;"></iframe>
									</td>
									<td align="center" width="10">
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td height="30" align="right" valign="bottom">
							<table width="425" border="0" cellspacing="0" cellpadding="0">
								<tr>
									<td width="132" height="23" align="center" onclick="javascript:doWith('7',this);">
										<img src="<%=this.strSkinPath %>/top_home.gif" align="absmiddle" />
										<a href="#" class="index_right">我的桌面</a>
									</td>
									<td width="132" align="center" onclick="javascript:toolbox_oncommand('/OA2/Mail/MailFrame.aspx','我的邮箱');">
										<img src="<%=this.strSkinPath %>/top_mail.gif" align="absmiddle" />
										<a href="#" class="index_right">我的邮箱</a>
									</td>
									<td width="132" align="center" onclick="javascript:toolbox_oncommand('/oa/Calendar/CalendarMain.aspx','我的日程');">
										<img src="<%=this.strSkinPath %>/top_cal.gif" align="absmiddle" />
										<a href="#" class="index_right">我的日程</a>
									</td>
									
									<td width="132" align="center" onclick="javascript:toolbox_oncommand('../oa/UserDefineFlow/MyFlowManage.ASPX','流程监控');">
										<img src="<%=this.strSkinPath %>/top_set.gif" align="absmiddle" />
										<a href="#" class="index_right">流程监控</a>
									</td>
									<td width="111" align="center" onclick="javascript:toolbox_oncommand('/oa/Buffet/BuffetFrame.aspx','员工自助');">
										<img src="<%=this.strSkinPath %>/top_person.gif" align="absmiddle" /><a href="#" class="index_right">
											员工自助</a>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	</table>
	</form>
</body>
<script language="javascript">
	document.body.oncontextmenu = new Function("return false;");
	function IMG1_onclick() { }
</script>
</html>
