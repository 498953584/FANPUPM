<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="pickresource.aspx.cs" Inherits="PickResource" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ѡȡ��Դ��</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="-1" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self"></base>
		<script language="javascript" src="../../../Web_Client/Tree.js" type="text/javascript"></script>

        <script src="../../../Script/jquery.js" type="text/javascript"></script>
		<script language="javascript">
		    function checkThis(obj, objValue, noteObj) {
		        var exists = false;
		        var nodeCode = document.getElementById(noteObj);
		        var nodeCodeList = nodeCode.value.split(',');
		        if (nodeCode.value == "") {
		            if (obj.checked) {
		                nodeCodeList.splice(0, 1);
		            }
		            else {
		                return;
		            }
		        }
		        if (obj.checked) {
		            for (var i = 0; i < nodeCodeList.length; i++) {
		                if (nodeCodeList[i] == objValue) {
		                    exists = true;
		                    break;
		                }
		            }
		            if (!exists) {
		                nodeCodeList.push(objValue);
		            }
		        }
		        else {
		            for (var i = 0; i < nodeCodeList.length; i++) {
		                if (nodeCodeList[i] == objValue) {
		                    nodeCodeList.splice(i, 1);
		                }
		            }
		        }
		        document.getElementById(noteObj).value = nodeCodeList.join(',');
		    }

		    function isNoPromt() {
		        var obj = document.getElementById('HdnOperate');
		        if (obj.value == "1") {
		            if (confirm('���ȡ����ǰ�������ݽ����ᱣ�棬ȷ��ȡ����') == true) {
		                window.close();
		            }
		        }
		        else {
		            //window.returnValue = true;   //ģ̬���巵��ֵ
		            window.close();
		            return true;
		        }
		    }
		</script>
	</head>
	<body class="body_popup" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td valign="bottom" height="20">
						<iewc:TabStrip ID="TabResource" TabIndex="-1" TargetID="mpResource" SepDefaultStyle="border-top:#999AB5 solid 0px;border-left:#999AB5 solid 0px;border-right:#999AB5 solid 0px;border-bottom:#999AB5 solid 1px;" TabDefaultStyle="background-image:url(../../images/2_2.gif);border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 1px;font-family:verdana;font-weight:bold;font-size:12px;color:#000000;width:80;height:20;text-align:center;" TabHoverStyle="background-color:#777777;" TabSelectedStyle="background-image:url(../../images/2_1.gif);color:#000000;border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 0px;width:80;height:20;font-size:12px;text-align:center;" runat="server"><Items><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="������Դ" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="������Դ" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="��е��Դ" runat="server"></iewc:Tab><iewc:TabSeparator DefaultStyle="width:100%;" runat="server"></iewc:TabSeparator></Items></iewc:TabStrip></td>
				</tr>
				<tr>
					<td class="mp_frame_column" style="PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; PADDING-TOP: 3px"
						valign="top">
						<table width="100%" height="100%" cellpadding="0" cellspacing="0" style="TABLE-LAYOUT: fixed">
							<tr>
								<td width="180" valign="top">
									<iewc:MultiPage ID="MpResource" Width="100%" Height="100%" runat="server"><iewc:PageView runat="server">
											<iframe id="FraHuman" name="FraHuman" width="100%" height="100%" src="about:blank" runat="server">
											</iframe>
										</iewc:PageView><iewc:PageView runat="server">
											<iframe id="FraMaterial" name="FraMaterial" width="100%" height="100%" src="about:blank" runat="server"></iframe>
										</iewc:PageView><iewc:PageView runat="server">
											<iframe id="FraMachine" name="FraMachine" width="100%" height="100%" src="about:blank" runat="server">
											</iframe>
										</iewc:PageView></iewc:MultiPage></td>
								<td width="4"></td>
								<td><iframe id="fraResource" name="fraResource" width="100%" height="100%" src="about:blank"></iframe>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="mp_frame_top" align="right" colspan="3" height="30">
						<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
						<table id="Table8" height="100%" width="40%">
							<tr align=right>
								<td>
								<!--<textarea id="ttest"></textarea>-->
								<INPUT id="HdnOperate" style="WIDTH: 10px" type="hidden" name="HdnOperate" value="0">
								
									<input type="button" value="��   ��" class="button-normal" onclick="al()" id="Button1">
									<input type="button" value="ȡ   ��" class="button-normal" onclick="isNoPromt()">
                                    &nbsp;
                                </td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		
	</body>
    <script>       
        function al() {
            window.returnValue = true; window.close();
        }
    </script>
</HTML>
