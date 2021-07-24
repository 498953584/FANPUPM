<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConferenceTemplate.aspx.cs" Inherits="ConferenceTemplate" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head id="Head1" runat="server"><title></title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />
</head>
<body class="body_frame" scroll="no">
    <form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
				<tr>
					<td vAlign="top" width="15%" height="100%">
						<div class="gridBox">
                            <iewc:TreeView ID="TVClass" SelectExpands="true" ExpandLevel="1" SelectedStyle="color:red;background-color:#ffffff;" runat="server"></iewc:TreeView></div>
                            
					</td>
					<td vAlign="top" width="87%"><iframe id="TemplateList" name="TemplateList" src="" frameBorder="0" width="100%" scrolling="no" height="100%"></iframe>
					</td>
				</tr>
			</table>
		</form>
</body>
</html>
