<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DBMailView.aspx.cs" Inherits="oa_MailAdmin_DBMailView" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>邮件查看  </title>
    <script language="javascript">
		function download(filepath,OriginalName)
	    {
	        var url = "/EPC/uploadfile/down.aspx?fileName=" + escape(OriginalName) + "&filePath=" + escape(filepath) ;
            window.location.href = url;
	    }
		</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="1" class="table-normal">
				<tr>
				    <td class="td-title" colspan="2">电子邮件查看</td></tr>				
			<tr>
				<td style="width:25%" class="td-label">发信人：</td>
				<td ><asp:Label ID="LabSender" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td class="td-label" style="width:25%">收信人：</td>
				<td><asp:Label ID="LabConsignee" runat="server"></asp:Label></td>
			</tr>
			<tr>
			    <td class="td-label">抄送人：</td>
			    <td><asp:Label ID="LbCSR" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td class="td-label" >主&nbsp;&nbsp;题：</td>
				<td><asp:Label ID="LabTitle" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td class="td-label" >日&nbsp;&nbsp;期：</td>
				<td><asp:Label ID="LabDateTime" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td class="td-label" style="width:25%">附&nbsp;&nbsp;件：</td>
				<td id="tr_fj" runat="server">
					<div id="annexBlock" runat="server"></div>
					<iframe height="0" width="0" name="fileload" id="fileload"></iframe>
				</td>
			</tr>
			<tr>
				<td colspan="2" style="line-height: 15pt; height: 22px;" >
                        <asp:Label ID="LblCon" runat="server"></asp:Label>&nbsp;
				</td>
			</tr>
			<tr>
				<td colspan="2" align="center" height="22">
					</td>
			</tr>
		</table>
    </div>
    </form>
</body>
</html>
