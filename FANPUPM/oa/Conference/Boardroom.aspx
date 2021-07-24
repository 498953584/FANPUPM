<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Boardroom.aspx.cs" Inherits="Boardroom" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title></title><meta Name="CODE_LANGUAGE" Content="C#" />
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
                            <iewc:TreeView ID="TVCorp" SelectExpands="true" ExpandLevel="1" SelectedStyle="color:red;background-color:#ffffff;" runat="server"></iewc:TreeView></div>
                            
					</td>
					<td vAlign="top" width="87%"><iframe id="BoardroomList" name="BoardroomList" src="" frameBorder="0" width="100%" scrolling="no" height="100%"></iframe>
					</td>
				</tr>
			</table>
		</form>
</body>
</html>
