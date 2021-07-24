<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resourcelist.aspx.cs" Inherits="ResourceList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>材料列表</title><meta http-equiv="Content-Type" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self"></base>
		<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
		<script language="javascript">
			var Noteid;	
			var ResorceName	;
			var ResourceStyle
			function openBindList()
			{
				var url;
				
				url = "ResourceBindList.aspx?ni="+Noteid+"&rn="+escape(ResourceName)+"&rs="+escape(ResourceStyle);									
				var refresh = window.showModalDialog(url,window,"dialogHeight:500px;dialogWidth:700px;center:1;help:0;status:0;");
				if(refresh==true)
				{  	   
					return true;
				}
				else
				{
					return false;
				}
			}
			function doRows(id,name,style)
			{								
				Noteid	= id;	
				ResourceName = name;	
				ResourceStyle = style;				
				document.getElementById("BtnRBind").disabled = false;				
			}
		</script>
	</head>
	<BODY bottomMargin="0" leftMargin="2" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table3" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="1">
				<tr >
					<td class="td-title">资源绑定</td>
				</tr>
				<tr>
					<td class="td-toolsbar"><asp:Button ID="BtnRBind" Enabled="false" Text="资源绑定" OnClick="BtnRBind_Click" runat="server" /></td>
				</tr>
				<tr >
					<td height=400>
						<DIV style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:DataGrid ID="dgd_List" Width="100%" AutoGenerateColumns="false" CssClass="grid" HeaderStyle-HorizontalAlign="Center" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="ResourceCode" HeaderText="资源编号"></asp:BoundColumn><asp:BoundColumn DataField="ResourceName" HeaderText="资源名称"></asp:BoundColumn><asp:BoundColumn DataField="ResourceUnit" HeaderText="单位"></asp:BoundColumn></Columns></asp:DataGrid></DIV>
					</td>
				</tr>
			</table>
		</form>
	</BODY>
</HTML>
