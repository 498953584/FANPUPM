<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowStatus.aspx.cs" Inherits="FlowStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>查看流程流程状态</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<script type="text/javascript">
		function changebkcolor(obj) //单元格选中时改变背景颜色
		{
		    for(i=0;i< document.getElementById("tbFlowChart").rows.length;i++)
            {
                for (var j=0; j< document.getElementById("tbFlowChart").rows[i].cells.length; j++)
                {
                    document.getElementById("tbFlowChart").rows[i].cells[j].style.backgroundColor="#FFFFFF";
                }
            }
		    obj.style.backgroundColor="#EEEEEE";
		}
		</script>
</head>
<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="height:95%" width="100%">
				<tbody>
				    
				    <tr>
					    <td vAlign="middle" height="100%" align="center">
						     <div style="WIDTH: 100%; HEIGHT: 80%">
                                  <table id="tbFlowChart" border="0" cellpadding="0" cellspacing="0" runat="server"></table>    
                             </div>
					    </td>
				    </tr>
				</tbody>
			</table>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
		</form>
	</body>
</html>
