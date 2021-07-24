<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PickSinglePerson.aspx.cs" Inherits="PickSinglePerson" %>

<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server"><title>选择人员...</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="JavaScript" type="text/JavaScript">
			function confirmHuman()
			{
				var humanList = FraHuman.Form1.LBoxHuman;
				var i = humanList.selectedIndex;
				var name="";
				var value="";
				name=humanList.options[i].text;
				value=humanList.options[i].value;
				txt=document.getElementById("Hdntxt").value;
				hid=document.getElementById("Hdnhid").value;
		        window.opener.document.getElementById(txt).value=name;
		        window.opener.document.getElementById(hid).value=value;
		        window.close();
			}
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td height="18">部门：</td>
					<td height="18">人员列表：</td>
				</tr>
				<tr>
					<td bgColor="#ffffff"><iframe id="FraDept" name="FraDept" src="/CommonWindow/DeptList.aspx" width="100%" scrolling="auto" height="100%" runat="server"></iframe>
					</td>
					<td>
					<iframe id="FraHuman" name="FraHuman" src="/CommonWindow/HumanSList.aspx" frameborder="0" width="120" scrolling="auto" height="100%">
							</iframe>
					</td>
				</tr>
				<tr>
					<td  align="right" height="30"><input type="hidden" id="bmdm" runat="server" />
<input type="hidden" id="bmmc" name="bmmc" runat="server" />

                    </td>
						<td  class="td-submit" ><input id="BtnOK" type="button" value="确  定"  onclick="confirmHuman();" />&nbsp;
                       <input id="BtnCancel" type="button" value="取  消" onclick="window.close();""/>
                            <input id="Hdntxt" style="width:2px" type="hidden" runat="server" />
<input id="Hdnhid" style="width:2px" type="hidden" runat="server" />

					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
