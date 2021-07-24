<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewmail_2.aspx.cs" Inherits="oa_MailAdmin_viewmail_2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>阅读邮件</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

    <style type="text/css">
        html,body {
        margin-left:0px;
        margin-top:0px;
        margin-right:0px;
        margin-bottom:0px;
        PADDING: 0px;
        width:100%;
        height:100%;
        color:#000000;
        background-color:#F2F7FD;
        }
        </style>
    <link href="../../StyleCss/PM1.css" rel="stylesheet" type="text/css" />


    <script language="javascript">
		function download(filepath,OriginalName)
	    {
	        var url = "/EPC/uploadfile/down.aspx?fileName=" + escape(OriginalName) + "&filePath=" + escape(filepath) ;
            window.location.href = url;
	    }
    </script>

    <style type="text/css">
<!--
.STYLE1 {font-family: "幼圆"}
-->
    </style>
</head>
<body>
    <table cellspacing="0" cellpadding="0" width="100%" height="100%" align="center"
        border="0">
        <tr>
            <td colspan="2" valign="top" bgcolor="aliceblue">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="5%">
                            &nbsp;</td>
                        <td width="92%" height="50">
                            &nbsp;</td>
                        <td width="3%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td height="50">
                            <span style="font-family: '幼圆'; font-size: 18px; font-weight: bold">
                                <img src="../../images/sendmail_title.gif" width="24" height="24" align="absmiddle">
                                您的邮件已经发送成功</span>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td height="1px">
                        </td>
                        <td bgcolor="#666666">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td height="40">
                            <form name="form1" method="post" action="" runat="server">
                                <a href="../../oa/MailAdmin/inbox.aspx">&lt;&lt; 返回收件箱</a>
                                <a href="../../oa/MailAdmin/writemail.aspx">再写一封 &gt;&gt;</a>
                                
                               
                            </form>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>
