<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HumanSList.aspx.cs" Inherits="HumanSList" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server"><title>HumanSList</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript" type="text/javascript">
			function pickOnPerson(obj)
			{
			   /*原来的版本，这个没有判断是用window.open()还是window.showModelDialog()两个方法传值形式不同，要引用同一个页面需要判断一下
				var humanList = Form1.LBoxHuman;
				var i = humanList.selectedIndex;
				var human = parent.window.dialogArguments;
				if (i>-1)
				{
				human[0] = humanList.options[i].value;
				human[1] = humanList.options[i].text;
				human[2] = document.getElementById("bmdm").value;  //部门编码
				human[3] = document.getElementById("bmmc").value;	//部门名称
				parent.window.returnvalue= human;
				window.close();
				}else
				{
				alert('请选择人员!');
				}
				*/
				//sm是定义的一个变量，用来标识窗体是用window.open();或是用Window.showModelDialog()的形式打开
			    var humanList =Form1.LBoxHuman;
				var i = humanList.selectedIndex;
				var name="";
				var value="";
				if(parent.window.location.href.indexOf('sm')!=-1)
				{
				    name=humanList.options[i].text;
				    value=humanList.options[i].value;
				    parent.window.opener.document.getElementById('txtPeople').value=name;
			        if(parent.opener.document.getElementById('hdnPeople'))
				    {
				         parent.opener.document.getElementById('hdnPeople').value=value;
				    }
			        parent.window.close();
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
				parent.window.returnvalue= human;
				parent.parent.window.close();
				}
				else
				{
				alert('请选择人员');
				}
				}		
			}
			function selOnPerson()
			{
				parent.document.getElementById('BtnOK').disabled = false;
				parent.document.getElementById('bmdm').value = document.getElementById("bmdm").value;
				parent.document.getElementById('bmmc').value = document.getElementById("bmmc").value;
			}
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><asp:ListBox ID="LBoxHuman" Width="120px" Rows="27" runat="server"></asp:ListBox></td>
				</tr>
			</table>
			<input id="bmdm" type="hidden" runat="server" />
 <input id="bmmc" type="hidden" name="bmmc" runat="server" />

			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl></form>
	</body>
</html>
