<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupSetting.aspx.cs" Inherits="oa_SysAdmin_UserSet_PasswordSet_PopupSetting" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>冒泡提醒</title>
	<style type="text/css">
		#gvwPopup
		{
			width: 240px;
			margin: auto;
			border-collapse: collapse;
		}
		
		.tdChk
		{
			width: 60px;
			text-align: right;
		}
		
		.tdModule
		{
			text-align: left;
			padding-left: 20px;
		}
	</style>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript">

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="pagediv" style="text-align: center;">
		<br />
		<table class="gvdata" cellspacing="0px" border="1px" style="width: 240px; margin: auto;
			border-collapse: collapse;">
			<tr id="tr_bulletin" class="rowa" runat="server"><td style="width: 60px; text-align: right;" runat="server">
					<asp:CheckBox ID="chkBulletin" runat="server" />
				</td><td style="text-align: left; padding-left: 20px;" runat="server">
					<asp:Label ID="lblBulletin" runat="server"></asp:Label>
				</td></tr>
			<tr id="tr_companyNews" class="rowb" runat="server"><td style="text-align: right;" runat="server">
					<asp:CheckBox ID="chkCompanyNews" runat="server" />
				</td><td style="text-align: left; padding-left: 20px;" runat="server">
					<asp:Label ID="lblCompanyNews" runat="server"></asp:Label>
				</td></tr>
			<tr id="tr_workflow" class="rowa" runat="server"><td style="text-align: right;" runat="server">
					<asp:CheckBox ID="chkWorkflow" runat="server" />
				</td><td style="text-align: left; padding-left: 20px;" runat="server">
					<asp:Label ID="lblWorkflow" runat="server"></asp:Label>
				</td></tr>
			<tr id="tr_schedule" class="rowb" runat="server"><td style="text-align: right;" runat="server">
					<asp:CheckBox ID="chkSchedule" runat="server" />
				</td><td style="text-align: left; padding-left: 20px;" runat="server">
					<asp:Label ID="lblSchedule" runat="server"></asp:Label>
				</td></tr>
			<tr id="tr_mail" class="rowa" runat="server"><td style="text-align: right;" runat="server">
					<asp:CheckBox ID="chkMail" runat="server" />
				</td><td style="text-align: left; padding-left: 20px;" runat="server">
					<asp:Label ID="lblMail" runat="server"></asp:Label>
				</td></tr>
			<tr id="tr_warning" class="rowb" runat="server"><td style="text-align: right;" runat="server">
					<asp:CheckBox ID="chkWarning" runat="server" />
				</td><td style="text-align: left; padding-left: 20px;" runat="server">
					<asp:Label ID="lblWarning" runat="server"></asp:Label>
				</td></tr>
			<tr>
				<td colspan="2" style="text-align: right;">
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
				</td>
			</tr>
		</table>
		
	</div>
	</form>
</body>
</html>
