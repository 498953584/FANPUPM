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
			//sm是定义的一个变量，用来标识窗体是用window.open();或是用Window.showModelDialog()的形式打开
			    var humanList = FraHuman.Form1.LBoxHuman;
				var i = humanList.selectedIndex;
				var name="";
				var value="";
				if(window.location.href.indexOf('sm')!=-1)
				{
				    name=humanList.options[i].text;
				    value=humanList.options[i].value;
				    window.opener.document.getElementById('txtPeople').value=name;
				    if(window.opener.document.getElementById('hdnPeople'))
				    {
				        window.opener.document.getElementById('hdnPeople').value=value;
				    }
			        window.close();
				}
				else
				{
				    var human = window.dialogArguments;
                    if(i>-1) 
                    {
 				        human[0] = humanList.options[i].value;
				        human[1] = humanList.options[i].text;
				        human[2] = document.getElementById('bmdm').value;
				        human[3] = document.getElementById("bmmc").value;	//部门名称
				        window.returnvalue= human;
				        window.close();
				    }
				    else
				    {
				        alert('请选择人员');
				    }
				}		
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
					<td bgColor="#ffffff"><iframe id="FraDept" name="FraDept" src="DeptList.aspx" width="100%" scrolling="auto" height="100%" runat="server"></iframe>
					</td>
					<td>
					<iframe id="FraHuman" name="FraHuman" src="HumanSList.aspx" frameborder="0" width="120" scrolling="auto" 
							height="100%">
							</iframe>
					</td>
				</tr>
				<tr>
					<td  align="right" height="30"><input type="hidden" id="bmdm" runat="server" />
<input type="hidden" id="bmmc" name="bmmc" runat="server" />

                    </td>
						<td  class="td-submit" ><input id="BtnOK" type="button" value="确  定"  onclick="confirmHuman();" />&nbsp;
                       <input id="BtnCancel" type="button" value="取  消" onclick="window.close();""/>

					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
