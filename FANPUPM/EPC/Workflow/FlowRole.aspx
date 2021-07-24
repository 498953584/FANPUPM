<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowRole.aspx.cs" Inherits="FlowRole" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>FlowRole</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/ProjectTree.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			//            alert(window.location.href);
			
		});
	</script>

</head>
<body class="body_frame" scroll="no">
    <form id="Form1" method="post" runat="server">
    <table class="table-none" id="Table3" height="100%" cellspacing="0" cellpadding="0"
        width="100%" border="0">
        <tr>
            <td valign="top" width="20%" height="100%" style="border:1px solid rgb(149,184,184)">
                <div>
                    <asp:TreeView ID="TVRole" ShowLines="true" ExpandDepth="1" Font-Underline="true" ForeColor="#5555DD" HorizontalPadding="0px" CssClass="selectTree" VerticalPadding="0px" AutoPostBack="True" OnSelectedNodeChanged="TVRole_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                </div>
            </td>
            <td valign="top" width="87%">
                <iframe id="InfoList" name="InfoList" frameborder="0" width="100%" scrolling="no" height="100%" runat="server"></iframe>
            </td>
        </tr>
    </table>
	<asp:HiddenField ID="hfldSelectedVal" runat="server" />
    </form>
</body>
</html>
