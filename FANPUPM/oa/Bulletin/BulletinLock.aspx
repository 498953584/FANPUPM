<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulletinLock.aspx.cs" Inherits="oa_Bulletin_BulletinLock" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>公告</title>
	<style type="text/css">
		ul, li
		{
			list-style: none;
		}
	</style>
	<script language="javascript" type="text/javascript">
	window.name = "win";
	<!--	
	function UpFile(t)
	{
	    //t=2 为公告管理

	    var RecordCode = document.getElementById('hdnRecordId').value;//编号
		var url = "../../CommonWindow/Annex/AnnexList.aspx?mid="+t+"&rc="+RecordCode+"&at=0&type=2";			
		window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
	}
	-->
	</script>
	<link href="../StyleCss/PM1.css" rel="stylesheet" type="text/css" />
</head>
<body class="body_popup" scroll="auto">
	<form id="form1" target="win" runat="server">
	<table id="TableMain" cellspacing="0" cellpadding="0" width="100%" border="0" align="center"
		class="table-none">
		<tr>
			<td align="left">
				<img src="../../oa/Bulletin/images/announcement.gif" />
			</td>
		</tr>
		<tr valign="top" height="35">
			<td align="center">
				<asp:Label ID="lbTitle" Style="font-size: 23;" Font-Bold="true" Font-Size="12px" ForeColor="Red" runat="server"></asp:Label>
			</td>
		</tr>
		<tr valign="top" height="10">
			<td>
				<hr style="color: #999; width: 90%; height: 1px" />
			</td>
		</tr>
		<tr>
			<td align="right" height="29" class="txtxw">
				<asp:Label ID="lbDate" Text="" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</td>
		</tr>
		<tr height="280">
			<td valign="top" class="ptxw">
				
				<table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
					<tr>
						<td class="txtxw">
							<asp:Label ID="LblCon" CssClass="" runat="server"></asp:Label>
						</td>
					</tr>
				</table>
				
			</td>
		</tr>
	</table>
	<table width="90%" align="center" class="txtxw">
		<tr valign="bottom" height="28">
			<td style="height: 28px">
				<table id="tb" runat="server"><tr runat="server"><td runat="server">
						</td></tr></table>
			</td>
		</tr>
		<tr>
			<td style="height: 21px" align="right">
				<asp:Label ID="lbDept" Text="部门" Width="89px" runat="server"></asp:Label>&nbsp;&nbsp;<asp:Label ID="LbUserName" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td class="td-submit" colspan="1" align="center" style="height: 27px">
				<input type="hidden" id="hdnRecordId" name="hdnRecordId" style="width: 10px" runat="server" />

				<input type="hidden" id="hdnDept" name="hdnRecordId" style="width: 10px" runat="server" />
&nbsp;
				<input onclick="javascript:returnValue=false;window.close();" type="button" value="关 闭"
					style="width: 0; display: none;">
			</td>
		</tr>
	</table>
	<div>
		<ul>
			<asp:Literal ID="Literal1" runat="server"></asp:Literal>
		</ul>
	</div>
	</form>
</body>
</html>
