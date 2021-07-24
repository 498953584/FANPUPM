<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selectcbssengle.aspx.cs" Inherits="SelectCBSSengle" %>


<HTML>
	<head runat="server"><title>CBS节点选择</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript">
		function SureData()
		{
			var Code = document.getElementById("hidCBSCode").value;
			var Name = document.getElementById("hidCBSName").value;
			var ArrReturn = new Array(2);
			if(Code != "")
			{
				ArrReturn[0] = Code;
				ArrReturn[1] = Name;
				window.returnValue = ArrReturn;
				window.close();
			}
			else
			{
				alert("请选择子节点！\n退出请点击\"关  闭\"");
			}
		}
		</script>
	</head>
	<body class="body-frame" scroll=no>
		<form id="Form1" method="post" runat="server">
			<table border="0" class="table-normal" cellpadding="0" cellspacing="0" width="100%" height="100%">
				<tr>
					<td class="td-title" height="1">选择CBS节点</td>
				</tr>
				<tr>
					<td height="100%" valign="top">
						<iframe src="CostcbsTypeSelect.aspx?Action=View" frameborder="0" width="100%" height="100%">
						</iframe>
					</td>
				</tr>
				<tr>
					<td class="td-submit" height="1">
						<INPUT type="button" value="确  定" onclick="SureData();"><FONT face="宋体">&nbsp; </FONT>
						<INPUT type="button" value="关  闭" onclick="window.returnValue=null;window.close();">
						<INPUT id="hidCBSName" type="hidden"><INPUT id="hidCBSCode" type="hidden"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
