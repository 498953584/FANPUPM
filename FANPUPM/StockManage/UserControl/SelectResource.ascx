<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectResource.ascx.cs" Inherits="StockManage_UserControl_SelectResource" %>

<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
<script type="text/javascript">
	$(document).ready(function () {
		//弹出DIV
		$('input[id$=btnSelectResource]').get(0).onclick = function () {
			if (!$('#ifResouece').attr('src')) {
				var src = '/StockManage/UserControl/SelectResource.aspx?TypeCode=';
				//添加TypeCode
				var typeCode = $('input[id$=hfldTypeCode]').val() || '0';
				src += typeCode;
				//添加ButtonID
				var buttonId = $('input[id$=hfldButtonId]').val() || '0';
				src += ('&ButtonId=' + buttonId);
				$('#ifResouece').attr('src', src);
			}
			$('#divSelectResource').dialog({ width: 1010, height: 485, modal: true });
			return false;
		}
	});
</script>
<asp:Button ID="btnSelectResource" UseSubmitBehavior="false" Width="150px" Text="选择资源" runat="server" />
<div id="divSelectResource" title="选择资源" style="display: none">
	<iframe id="ifResouece" frameborder="0" width="100%" height="100%"></iframe>
</div>
<asp:HiddenField ID="hfldTypeCode" runat="server" />
<asp:HiddenField ID="hfldButtonId" runat="server" />
<asp:HiddenField ID="hfldResourceId" runat="server" />
<asp:HiddenField ID="hfldResourceCode" runat="server" />
