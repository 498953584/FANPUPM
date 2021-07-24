<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BorroweFileSelectMainTree.aspx.cs" Inherits="oa_eFile_BorroweFileSelectMainTree" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>档案选择窗口</title>
	<script type="text/javascript" language="javascript">
    <!--
		window.name = "win";
    
    -->
	</script>
</head>
<body class="body_clear" scroll="no">
	<form id="form1" method="post" target="win" runat="server">
	<div>
		<table style="height: 100%; width: 100%" cellpadding="0" cellspacing="0" border="1"
			id="tablex" class="table-normal">
			<tr>
				<td valign="top" style="width: 25%">
					
					<iewc:TreeView ID="FillTrees1" ExpandLevel="3" SelectedStyle="color:red;background-color:#ffffff;" runat="server"></iewc:TreeView>
				</td>
				<td valign="top">
					<iframe id="frmMain" name="frmMain" frameborder="0" width="100%" height="100%" runat="server">
					</iframe>
				</td>
			</tr>
		</table>
		
	</div>
	</form>
</body>
</html>
