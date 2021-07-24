<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddFromTemplate.aspx.cs" Inherits="AddFromTemplate" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>从模板增加会议</title>
    <script language="javascript">
    <!--
    
    -->
    </script>
</head>
<body class="body_frame" scroll="no">
    <form id="Form1" method="post" runat="server">
        <table class="table-none" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
				<tr>
					<td vAlign="top" width="15%" height="100%">
						<div class="gridBox">
                            <iewc:TreeView ID="TVClass" SelectExpands="true" ExpandLevel="1" runat="server"></iewc:TreeView>
                        </div>
					</td>
					<td vAlign="top" width="87%">
					     <iframe id="TemplateSelectList" name="TemplateSelectList" src="" frameBorder="0" width="100%" scrolling="no" height="100%"></iframe>
					</td>
				</tr>
			</table>
			
		</form>
</body>
</html>
