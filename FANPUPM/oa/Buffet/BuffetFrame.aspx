<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuffetFrame.aspx.cs" Inherits="oa_Buffet_BuffetFrame" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>员工自助</title></head>
<body  class="body_popup" scroll="no">
    <form id="form1" runat="server">
    <div>
<table id="table1" width="100%" height="100%" cellpadding="0" cellspacing="0" border="1" class="table-normal">
    <tr>
    <td width="120px" valign="top">
             <table width="100%"  cellpadding="0" cellspacing="0" border="1" class="table-normal">
                <tr>
                    <td style="height:22px;" align="center">
                        <a class="firstpage" href="PersonalInfo.aspx" target="iframeBuffet">个人信息查看 </a>
                    </td>
                </tr>
				<tr>
					<td style="height:22px" align="center"><a class="firstpage" href="/HR/Leave/ApplicationList.aspx" target="iframeBuffet">请假申请 </a></td>
				</tr>
			
			</table>
	</td>
    <td valign="top">
        <iframe id="iframeBuffet" name="iframeBuffet" width="100%" height="100%" frameborder="0" src="PersonalInfo.aspx"></iframe></td>
    </tr>
</table>
    </div>
    </form>
</body>
</html>
